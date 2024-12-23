using DevPlan.Presentation.Base;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Utils.Calendar.Templates;
using GrapeCity.Win.CalendarGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// 定時間日の設定画面
    /// </summary>
    public partial class FixedTimeDaySettingForm : BaseSubForm
    {
        /// <summary>
        /// 画面名
        /// </summary>
        public override string FormTitle { get { return "定時間日の設定"; } }

        /// <summary>
        /// 年度開始年月
        /// </summary>
        private DateTime StartDate { get { return new DateTime(int.Parse(NendoComboBox.SelectedItem.ToString()), 4, 1); } }

        /// <summary>
        /// 定期便リスト
        /// </summary>
        public List<TruckScheduleItemModel> TruckItemList { get; private set; }

        /// <summary>
        /// 現在選択されているトラックID
        /// </summary>
        public long SelectedTruckId { get; private set; }

        /// <summary>
        /// 定時間日を更新した場合True
        /// </summary>
        public bool UpdateCheck { get; private set; }

        /// <summary>
        /// 定時間日更新対象日付リスト
        /// </summary>
        private List<DateTime> UpdateDateList;

        /// <summary>
        /// 定時間日更新対象日付チェック内部保持リスト
        /// </summary>
        private List<DateTime> CheckDateList;

        private List<TruckRegularTimeModel> _truckRegularTimeList;
        /// <summary>
        /// トラック定期便予約時間帯マスタ
        /// </summary>
        private List<TruckRegularTimeModel> TruckRegularTimeList
        {
            get
            {
                return _truckRegularTimeList.Where(x => x.IS_RESERVATION == 1).ToList();
            }
            set
            {
                _truckRegularTimeList = value;
            }
        }

        /// <summary>
        /// 年度コンボボックスアイテムリスト
        /// </summary>
        public List<object> NendoComboBoxItems { get; internal set; }
        
        /// <summary>
        /// 初期選択を行う年度
        /// </summary>
        private int DefaultNendo;

        /// <summary>
        /// 初期選択を行うトラックID
        /// </summary>
        private long DefaultTruckId;

        /// <summary>
        /// システム日付内部保持フィールド
        /// </summary>
        private DateTime today;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected FixedTimeDaySettingForm()
        {
            InitializeComponent();

            today = DateTime.Today;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="defaultTruckId"></param>
        /// <param name="defaultNendo"></param>
        public FixedTimeDaySettingForm(long defaultTruckId, int defaultNendo)
        {
            this.DefaultTruckId = defaultTruckId;
            this.DefaultNendo = defaultNendo;
            this.UpdateDateList = new List<DateTime>();
            this.CheckDateList = new List<DateTime>();

            today = DateTime.Today;

            InitializeComponent();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FixedTimeDaySettingForm_Load(object sender, EventArgs e)
        {
            #region 定期便時間帯取得

            var headerRes = HttpUtil.GetResponse<TruckRegularTimeModel>(ControllerType.TruckRegularTime);
            _truckRegularTimeList = new List<TruckRegularTimeModel>();
            if (headerRes != null && headerRes.Status == Const.StatusSuccess)
            {
                _truckRegularTimeList.AddRange(headerRes.Results);
            }

            #endregion
            
            this.NendoComboBox.Items.AddRange(NendoComboBoxItems.ToArray());
            this.NendoComboBox.SelectedItem = this.DefaultNendo;

            #region 定期便リスト

            TruckScheduleItemSearchModel cond = new TruckScheduleItemSearchModel()
            {
                IsRegular = true
            };
            var res = HttpUtil.GetResponse<TruckScheduleItemSearchModel, TruckScheduleItemModel>(ControllerType.TruckScheduleItem, cond);
            TruckItemList = new List<TruckScheduleItemModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                TruckItemList.AddRange(res.Results);

                List<ComboBoxSetting> src = new List<ComboBoxSetting>();
                foreach (var item in TruckItemList)
                {
                    src.Add(new ComboBoxSetting(item.ID.ToString(), item.車両名));
                }
                ComboBoxSetting.SetComboBox(TruckComboBox, src);
            }
            if (this.DefaultTruckId != 0)
            {
                this.TruckComboBox.SelectedValue = this.DefaultTruckId.ToString();
            }
            #endregion

            this.FixedTimeDaySettingCalendarGrid.CurrentCellPositionChanged += FixedTimeDaySettingCalendarGrid_CurrentCellPositionChanged;
            this.FixedTimeDaySettingInputCalendarGrid.CurrentCellPositionChanged += FixedTimeDaySettingInputCalendarGrid_CurrentCellPositionChanged;

            this.FixedTimeDaySettingInputCalendarGrid.CellEditingValueChanged += FixedTimeDaySettingInputCalendarGrid_CellEditingValueChanged;
            this.NendoComboBox.DropDownClosed += TruckComboBox_DropDownClosed;
            this.TruckComboBox.DropDownClosed += TruckComboBox_DropDownClosed;

            SetCalendarGrid(null, null);

            List<FixedTimeDaySettingModel> fixList = GetFixedTimeDaySettingList();
            if (fixList.Any() == false && HolidayCheckBox.Enabled == true)
            {
                HolidayCheckBox.Checked = true;
            }

            SetCalendarGridStartDate(FixedTimeDaySettingCalendarGrid.FirstDateInView);
           
            this.ActiveControl = FixedTimeDaySettingInputCalendarGrid;
        }

        /// <summary>
        /// 入力カレンダーグリッドアクティブセル変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FixedTimeDaySettingInputCalendarGrid_CurrentCellPositionChanged(object sender, CalendarCellMoveEventArgs e)
        {
            this.FixedTimeDaySettingCalendarGrid.CurrentCellPositionChanged -= FixedTimeDaySettingCalendarGrid_CurrentCellPositionChanged;

            FixedTimeDaySettingCalendarGrid.CurrentCellPosition = new CalendarCellPosition(e.CellPosition.Date, 0, 1);

            this.FixedTimeDaySettingCalendarGrid.CurrentCellPositionChanged += FixedTimeDaySettingCalendarGrid_CurrentCellPositionChanged;
        }

        /// <summary>
        /// 参照カレンダーグリッドアクティブセル変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FixedTimeDaySettingCalendarGrid_CurrentCellPositionChanged(object sender, CalendarCellMoveEventArgs e)
        {
            this.FixedTimeDaySettingInputCalendarGrid.CurrentCellPositionChanged -= FixedTimeDaySettingInputCalendarGrid_CurrentCellPositionChanged;

            FixedTimeDaySettingInputCalendarGrid.CurrentCellPosition = new CalendarCellPosition(e.CellPosition.Date, 0, 0);

            this.FixedTimeDaySettingInputCalendarGrid.CurrentCellPositionChanged += FixedTimeDaySettingInputCalendarGrid_CurrentCellPositionChanged;
        }

        /// <summary>
        /// トラックコンボボックスクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckComboBox_DropDownClosed(object sender, EventArgs e)
        {
            SetCalendarGrid(null, null);
            SetCalendarGridStartDate(FixedTimeDaySettingCalendarGrid.FirstDateInView);
        }

        /// <summary>
        /// カレンダーグリッド初期日付設定処理
        /// </summary>
        /// <param name="startDate"></param>
        private void SetCalendarGridStartDate(DateTime startDate)
        {
            FixedTimeDaySettingCalendarGrid.PerformRender();
            FixedTimeDaySettingCalendarGrid.ClearSelection();
            FixedTimeDaySettingCalendarGrid.CurrentCellPosition = new CalendarCellPosition(startDate, 0, 1);

            FixedTimeDaySettingInputCalendarGrid.PerformRender();
            FixedTimeDaySettingInputCalendarGrid.ClearSelection();
            FixedTimeDaySettingInputCalendarGrid.CurrentCellPosition = new CalendarCellPosition(startDate, 0, 0);
        }

        /// <summary>
        /// ビューの最初の日付変更時処理
        /// </summary>
        /// <remarks>今日へ移動ボタンが押下された場合は年度を再設定します。</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FixedTimeDaySettingCalendarGrid_FirstDateInViewChanged(object sender, EventArgs e)
        {
            if (FixedTimeDaySettingCalendarGrid.FirstDateInView.Date == today)
            {
                this.DefaultNendo = today.AddMonths(-3).Year;
                this.NendoComboBox.SelectedItem = this.DefaultNendo;
                SetCalendarGrid(null, null);
                SetCalendarGridStartDate(today);               
            }
        }

        /// <summary>
        /// 画面情報更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetCalendarGrid(object sender, EventArgs e)
        {
            FixedTimeDaySettingCalendarGrid.FirstDateInViewChanged -= FixedTimeDaySettingCalendarGrid_FirstDateInViewChanged;

            var src = new List<ComboBoxSetting>();

            if (TruckComboBox.SelectedValue != null)
            {
                #region 定期便チェックボックス更新

                SelectedTruckId = int.Parse(TruckComboBox.SelectedValue.ToString());
                foreach (var item in TruckRegularTimeList.Where(x => x.REGULAR_ID == TruckItemList.Where(y => y.ID == SelectedTruckId).First().REGULAR_TIME_ID))
                {
                    src.Add(new ComboBoxSetting(item.TIME_ID.ToString(), item.DEPARTURE_TIME + "発"));
                }

                #endregion
            }
            ComboBoxSetting.SetComboBox(FixedTimeCheckedListBox, src);

            FixedTimeDaySettingCalendarGrid.ClearAll();
            
            var cv = FixedTimeDaySettingCalendarGrid.CalendarView as CalendarListView;
            cv.DayCount = Enumerable.Range(0, 12).Select(x => StartDate.AddMonths(x + 1)).ToList().Sum(x => x.AddDays(-1).Day);

            this.FixedTimeDaySettingInputCalendarGrid.FirstDateInView = StartDate;
            this.FixedTimeDaySettingCalendarGrid.FirstDateInView = StartDate;

            #region 選択された定期便の定時情報設定
            
            List<FixedTimeDaySettingModel> fixList = GetFixedTimeDaySettingList();

            if (fixList.Count() > 0)
            {
                foreach (var fixtime in fixList)
                {
                    if (fixtime.FLAG_運休日 == 1)
                    {
                        FixedTimeDaySettingCalendarGrid[fixtime.DATE].Rows[0].Cells[3].Value = "運休";
                    }
                    else
                    {
                        var jikanList = fixtime.時間帯.Split(',').ToList();
                        foreach (var jikan in jikanList)
                        {
                            if (string.IsNullOrWhiteSpace(jikan) ||
                                Expansion2CalendarTemplate.timeHeaderList.Contains(int.Parse(jikan)) == false) { continue; }

                            var d = TruckRegularTimeList.Where(x =>
                                x.TIME_ID == int.Parse(jikan) &&
                                x.REGULAR_ID == TruckItemList.Where(y => y.ID == SelectedTruckId).First().REGULAR_TIME_ID);
                            FixedTimeDaySettingCalendarGrid[fixtime.DATE].Rows[0].Cells[3].Value += d.First().DEPARTURE_TIME + "発 ";
                        }
                    }
                }
            }

            #endregion

            var kadouRes = HttpUtil.GetResponse<CalendarKadouGetInModel, CalendarKadouGetOutModel>(ControllerType.CalendarKadou,
            new CalendarKadouGetInModel()
            {
                FIRST_DATE = FixedTimeDaySettingInputCalendarGrid.FirstDateInView,
                LAST_DATE = FixedTimeDaySettingInputCalendarGrid.LastDateInView
            });            
            if (kadouRes != null && kadouRes.Status == Const.StatusSuccess && kadouRes.Results.ToList().Count > 0)
            {
                this.groupBox1.Size = new Size(this.groupBox1.Size.Width, SaturdayCheckBox.Location.Y);
                WorkingDayCheckBox.Enabled = true;
                HolidayCheckBox.Enabled = true;
            }
            else
            {
                this.groupBox1.Size = new Size(this.groupBox1.Size.Width, 
                    SaturdayCheckBox.Location.Y + SaturdayCheckBox.Size.Height + 5);
                WorkingDayCheckBox.Enabled = false;
                HolidayCheckBox.Enabled = false;
                HolidayCheckBox.Checked = false;
            }

            FixedTimeDaySettingCalendarGrid.FirstDateInViewChanged += FixedTimeDaySettingCalendarGrid_FirstDateInViewChanged;
        }

        /// <summary>
        /// 定時間日取得処理
        /// </summary>
        /// <returns></returns>
        private List<FixedTimeDaySettingModel> GetFixedTimeDaySettingList()
        {
            var res = HttpUtil.GetResponse<FixedTimeDaySettingSearchModel, FixedTimeDaySettingModel>(
                ControllerType.FixedTimeDaySetting, new FixedTimeDaySettingSearchModel()
                {
                    START_DATE = StartDate,
                    END_DATE = new DateTime(StartDate.AddYears(+1).Year, 3, 31),
                    トラック_ID = SelectedTruckId
                });
            var list = new List<FixedTimeDaySettingModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            return list;
        }

        /// <summary>
        /// 登録ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            if (IsEntiryCheck() == false)
            {
                return;
            }

            var datalist = new List<FixedTimeDaySettingModel>();

            if (this.FixedTimeCheckedListBox.Enabled)
            {
                #region 運休以外

                var items = FixedTimeCheckedListBox.CheckedItems;
                
                foreach (var date in this.UpdateDateList)
                {
                    var dateList = new List<string>();

                    foreach (var check in items)
                    {                        
                        dateList.Add(((ComboBoxSetting)check).Key);
                    }

                    datalist.Add(new FixedTimeDaySettingModel()
                    {
                        DATE = date,
                        トラック_ID = SelectedTruckId,
                        時間帯 = string.Join(",", dateList),
                        FLAG_定時間日 = 1                        
                    });
                }

                #endregion
            }
            else
            {
                if (this.UnpaidCheckBox.Checked)
                {
                    #region 運休

                    foreach (var date in this.UpdateDateList)
                    {
                        datalist.Add(new FixedTimeDaySettingModel()
                        {
                            DATE = date,
                            トラック_ID = SelectedTruckId,
                            FLAG_運休日 = 1
                        });
                    }

                    #endregion
                }
            }

            var res = HttpUtil.PutResponse(ControllerType.FixedTimeDaySetting, datalist);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                Messenger.Info(Resources.KKM00002);
            }

            Init();
        }

        /// <summary>
        /// 日付・運休登録帯チェック処理
        /// </summary>
        /// <returns></returns>
        private bool IsEntiryCheck()
        {
            bool checkUnkyu = UnpaidCheckBox.Checked == false && FixedTimeCheckedListBox.CheckedItems.Count <= 0;
            bool checkDate = this.UpdateDateList.Count <= 0;
            if (checkUnkyu && checkDate)
            {
                Messenger.Warn(string.Format(Resources.KKM02011, "日付および運休登録帯"));
                return false;
            }
            else if (checkUnkyu)
            {
                Messenger.Warn(string.Format(Resources.KKM02011, "運休登録帯"));
                return false;
            }
            else if (checkDate)
            {
                Messenger.Warn(string.Format(Resources.KKM02011, "日付"));
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// データ更新後の初期化処理
        /// </summary>
        private void Init()
        {
            this.FixedTimeCheckedListBox.ClearSelected();
            SetCalendarGrid(null, null);
            this.UpdateCheck = true;
        }

        /// <summary>
        /// 解除ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (IsEntiryCheck() == false)
            {
                return;
            }

            var deleteList = new List<FixedTimeDaySettingModel>();
            
            foreach (var date in this.UpdateDateList)
            {
                if (FixedTimeDaySettingCalendarGrid[date].Rows[0].Cells[3].Value != null)
                {
                    deleteList.Add(new FixedTimeDaySettingModel()
                    {
                        DATE = date,
                        トラック_ID = SelectedTruckId
                    });
                }
            }

            if (deleteList.Any() == false)
            {
                Messenger.Info("解除対象がありませんでした。");
                return;
            }
            else
            {
                var res = HttpUtil.DeleteResponse(ControllerType.FixedTimeDaySetting, deleteList);
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    Messenger.Info("解除しました。");
                    Init();
                    return;
                }
            }
        }

        /// <summary>
        /// 稼働日をすべてチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkingDayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckKadoubi("1", WorkingDayCheckBox.Checked);
        }

        /// <summary>
        /// 非稼働日をすべてチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HolidayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckKadoubi("0", HolidayCheckBox.Checked);
        }

        /// <summary>
        /// 稼働日・非稼働日チェック処理
        /// </summary>
        /// <param name="kadouFlg">取得したい稼働フラグ</param>
        /// <param name="check">チェックボックスチェック状態</param>
        private void SetCheckKadoubi(string kadouFlg, bool check)
        {
            var cond = new CalendarKadouGetInModel()
            {
                FIRST_DATE = FixedTimeDaySettingInputCalendarGrid.FirstDateInView,
                LAST_DATE = FixedTimeDaySettingInputCalendarGrid.LastDateInView
            };
            var res = HttpUtil.GetResponse<CalendarKadouGetInModel, CalendarKadouGetOutModel>(ControllerType.CalendarKadou, cond);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                var holidays = res.Results.ToList().Where(x => x.KADOBI_KBN == kadouFlg).ToList();

                foreach (var item in holidays)
                {
                    this.FixedTimeDaySettingInputCalendarGrid[DateTime.ParseExact(item.CALENDAR_DATE, "yyyyMMdd", null)]
                        .Rows[1].Cells[0].Value = check;

                    if (check)
                    {
                        this.UpdateDateList.Add(DateTime.ParseExact(item.CALENDAR_DATE, "yyyyMMdd", null));
                    }
                }
            }

            if (check == false)
            {
                CheckDelete();
            }
        }

        #region 曜日設定

        /// <summary>
        /// 月曜日チェックボックスクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MondayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckDayOfWeek(DayOfWeek.Monday, this.MondayCheckBox.Checked);
        }

        /// <summary>
        /// 火曜日チェックボックスクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TuesdayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckDayOfWeek(DayOfWeek.Tuesday, this.TuesdayCheckBox.Checked);
        }

        /// <summary>
        /// 水曜日チェックボックスクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WednesdayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckDayOfWeek(DayOfWeek.Wednesday, this.WednesdayCheckBox.Checked);
        }

        /// <summary>
        /// 木曜日チェックボックスクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThursdayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckDayOfWeek(DayOfWeek.Thursday, this.ThursdayCheckBox.Checked);
        }

        /// <summary>
        /// 金曜日チェックボックスクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FridayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckDayOfWeek(DayOfWeek.Friday, this.FridayCheckBox.Checked);
        }

        /// <summary>
        /// 土曜日チェックボックスクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaturdayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckDayOfWeek(DayOfWeek.Saturday, this.SaturdayCheckBox.Checked);
        }

        /// <summary>
        /// 日曜日チェックボックスクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SundayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckDayOfWeek(DayOfWeek.Sunday, this.SundayCheckBox.Checked);
        }

        #endregion

        /// <summary>
        /// 入力用カレンダーグリッド更新処理
        /// </summary>
        /// <param name="week">チェック対象曜日</param>
        /// <param name="check">チェックボックスにチェックをする場合はTrue、外す場合はFalse</param>
        private void CheckDayOfWeek(DayOfWeek week, bool check)
        {
            var grid = FixedTimeDaySettingInputCalendarGrid;

            for (DateTime d = grid.FirstDateInView; d <= grid.LastDateInView; d = d.AddDays(1))
            {
                if (d.DayOfWeek == week)
                {
                    grid.Content[d].Rows[1].Cells[0].Value = check;
                    if (check)
                    {
                        this.UpdateDateList.Add(d);
                    }
                }
            }

            if (check == false)
            {
                CheckDelete();
            }
        }

        /// <summary>
        /// 入力用カレンダーグリッド変更検知処理
        /// </summary>
        /// <remarks>
        /// ユーザーがチェックをした場合に検知されるイベントです。
        /// （曜日チェックなど自動チェックの場合は動作しません）
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FixedTimeDaySettingInputCalendarGrid_CellEditingValueChanged(object sender, GrapeCity.Win.CalendarGrid.CalendarCellEditingValueChangedEventArgs e)
        {
            if ((bool)e.Value)
            {
                this.CheckDateList.Add(e.CellPosition.Date);
                this.UpdateDateList.Add(e.CellPosition.Date);
            }
            else
            {
                this.CheckDateList.Remove(e.CellPosition.Date);
                this.UpdateDateList.Remove(e.CellPosition.Date);
            }
        }

        /// <summary>
        /// 再チェック処理。一括で解除を行い、再度設定を行う。
        /// </summary>
        private void CheckDelete()
        {
            this.UpdateDateList.RemoveRange(0, this.UpdateDateList.Count);

            if (MondayCheckBox.Checked) { MondayCheckBox_CheckedChanged(null, null); }
            if (TuesdayCheckBox.Checked) { TuesdayCheckBox_CheckedChanged(null, null); }
            if (WednesdayCheckBox.Checked) { WednesdayCheckBox_CheckedChanged(null, null); }
            if (ThursdayCheckBox.Checked) { ThursdayCheckBox_CheckedChanged(null, null); }
            if (FridayCheckBox.Checked) { FridayCheckBox_CheckedChanged(null, null); }

            if (WorkingDayCheckBox.Checked) { WorkingDayCheckBox_CheckedChanged(null, null); }
            if (HolidayCheckBox.Checked) { HolidayCheckBox_CheckedChanged(null, null); }

            var grid = FixedTimeDaySettingInputCalendarGrid;
            foreach (var date in CheckDateList)
            {
                grid.Content[date].Rows[1].Cells[0].Value = true;
                this.UpdateDateList.Add(date);
            }
        }
        
        /// <summary>
        /// 運休チェックボックス押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnpaidCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.FixedTimeCheckedListBox.Enabled = !UnpaidCheckBox.Checked;

            if (this.FixedTimeCheckedListBox.Enabled == false)
            {
                for (int i = 0; i < this.FixedTimeCheckedListBox.Items.Count; i++)
                {
                    FixedTimeCheckedListBox.DisabledIndices.Add(i);
                }
            }
            else
            {
                FixedTimeCheckedListBox.DisabledIndices = new List<int>();
            }
    
            FixedTimeCheckedListBox.Refresh();
        }

        /// <summary>
        /// 今日へ移動ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GotoTodayCalendarTitleButton_Click(object sender, EventArgs e)
        {
            this.FixedTimeDaySettingCalendarGrid.FirstDateInView = today;
        }
    }
}
