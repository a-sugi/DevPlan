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
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// 項目詳細（トラック）画面
    /// </summary>
    public partial class TruckScheduleItemDetailForm : BaseSubForm
    {
        /// <summary>
        /// 定期便予約時間帯マスタリスト
        /// </summary>
        private List<TruckRegularTimeModel> truckRegularTimeList;

        /// <summary>
        /// 画面名
        /// </summary>
        public override string FormTitle { get { return "項目詳細（トラック）"; } }

        /// <summary>
        /// スケジュール項目
        /// </summary>
        public ScheduleItemModel<TruckScheduleItemModel> ScheduleItem { get; internal set; }

        /// <summary>
        /// スケジュール項目編集データ
        /// </summary>
        private TruckScheduleItemModel editItem { get; set; }

        /// <summary>
        /// 定時間日を更新した場合True
        /// </summary>
        public bool UpdateCheck { get; private set; }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TruckScheduleItemDetailForm()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckScheduleItemDetailForm_Load(object sender, EventArgs e)
        {
            FirstStationComboBox.Items.Add("");
            FirstStationComboBox.Items.Add("群馬");
            FirstStationComboBox.Items.Add("SKC");
            FirstStationComboBox.SelectedIndex = 0;

            //定期便時間帯取得
            var headerRes = HttpUtil.GetResponse<TruckRegularTimeModel>(ControllerType.TruckRegularTime);
            truckRegularTimeList = new List<TruckRegularTimeModel>();
            if (headerRes != null && headerRes.Status == Const.StatusSuccess)
            {
                truckRegularTimeList.AddRange(headerRes.Results);
            }

            {
                var masterData = truckRegularTimeList.Select(x => new { Id = x.REGULAR_ID, Name = x.DISPLAY_NAME }).Distinct();
                var src = new List<ComboBoxSetting>();
                src.Add(new ComboBoxSetting("", ""));
                foreach (var data in masterData)
                {
                    src.Add(new ComboBoxSetting(data.Id.ToString(), data.Name + "～"));
                }
                ComboBoxSetting.SetComboBox(TimezoneComboBox, src);
            }
            {
                var src = new List<ComboBoxSetting>();
                src.Add(new ComboBoxSetting("", ""));
                src.Add(new ComboBoxSetting("1", "適合"));
                src.Add(new ComboBoxSetting("0", "不適合"));
                ComboBoxSetting.SetComboBox(DieselRegulationComboBox, src);
                this.DieselRegulationComboBox.SelectedIndex = 0;
            }
            
            // 新規登録時のみ、定期便関係は最初は非活性。
            if (ScheduleItem.ScheduleItemEdit == ScheduleItemEditType.Insert)
            {
                editItem = new TruckScheduleItemModel
                {
                    SORT_NO = 0,
                    FLAG_ディーゼル規制 = 0,
                    予約可能開始日 = DateTime.Now.Date
                };

                if (ScheduleItem.ScheduleItem != null)
                {
                    editItem.SORT_NO = ScheduleItem.SortNo + 0.1D;
                }

                ChangeRegularServiceControls(false);

                this.ReservationTrackRadioButton.Checked = true;
                this.CarNameTextBox.Text = "";
                this.StorageLocationTextBox.Text = "";
                this.ReservationStartDateTimePicker.Value = DateTime.Now;
                this.RegistrationNumberTextBox.Text = "";
                this.TypeTextBox.Text = "";
                this.RemarksTextBox.Text = "";
                this.FixedTimeDaySettingGroupBox.Visible = false;
                this.DeleteButton.Visible = false;
            }
            else
            {
                editItem = ScheduleItem.ScheduleItem;
                ChangeRegularServiceControls(editItem.FLAG_定期便 == 1);
                SetControlData();
                if (editItem.FLAG_定期便 == 1)
                {
                    ResetNendoDate();
                    SetFixTimeGrid();
                }
            }
            
            this.ActiveControl = CarNameTextBox;

            if (FixedTimeDaySettingGroupBox.Visible == false)
            {
                this.Height = this.Height - FixedTimeDaySettingGroupBox.Height;
                this.EntryButton.Location = new Point(this.EntryButton.Location.X, this.CloseButton.Location.Y);
                this.DeleteButton.Location = new Point(this.DeleteButton.Location.X, this.CloseButton.Location.Y);
            }
        }

        /// <summary>
        /// 定期便休止時間帯リセット処理
        /// </summary>
        private void ResetNendoDate()
        {
            var nowDate = DateTime.Now.Date;

            var nendo = nowDate.AddMonths(-3);

            this.NendoComboBox.Items.Clear();
            this.NendoComboBox.Items.Add(nendo.AddYears(-2).Year);
            this.NendoComboBox.Items.Add(nendo.AddYears(-1).Year);
            this.NendoComboBox.Items.Add(nendo.Year);
            this.NendoComboBox.Items.Add(nendo.AddYears(+1).Year);

            this.NendoComboBox.TextChanged -= NendoComboBox_TextChanged;
            this.NendoComboBox.SelectedItem = nendo.Year;
            this.NendoComboBox.TextChanged += NendoComboBox_TextChanged;
        }

        /// <summary>
        /// コントロール値設定処理
        /// </summary>
        private void SetControlData()
        {
            this.DeleteButton.Visible = true;

            this.ReservationTrackRadioButton.Checked = editItem.FLAG_定期便 == 1 ? false : true;
            this.RegularServiceRadioButton.Checked = editItem.FLAG_定期便 == 1 ? true : false;

            this.StorageLocationTextBox.Text = editItem.保管場所;

            this.ReservationTrackRadioButton.Enabled = false;
            this.RegularServiceRadioButton.Enabled = false;

            TimezoneComboBox.SelectedValue = editItem.REGULAR_TIME_ID.ToString();

            this.CarNameTextBox.Text = editItem.車両名;
            this.FirstStationComboBox.SelectedItem = editItem.始発場所;

            this.DieselRegulationComboBox.SelectedValue = editItem.FLAG_ディーゼル規制.ToString();

            this.ReservationStartDateTimePicker.Value = editItem.予約可能開始日.Value;

            this.RegistrationNumberTextBox.Text = editItem.登録番号;
            this.TypeTextBox.Text = editItem.種類;
            this.RemarksTextBox.Text = editItem.備考;

            this.PreOpeningCheckCheckBox.Checked = (editItem.FLAG_CHECK == 1);

            if (editItem.FLAG_定期便 == 1)
            {
                PreOpeningCheckCheckBox.Visible = false;
            }
        }

        /// <summary>
        /// 対象年度切り替え処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NendoComboBox_TextChanged(object sender, EventArgs e)
        {
            SetFixTimeGrid();
        }

        /// <summary>
        /// 定期便定時間日カレンダーグリッド設定処理
        /// </summary>
        private void SetFixTimeGrid()
        {   
            FixedTimeCalendarGrid.ClearAll();
            FixedTimeCalendarGrid.FirstDateInViewChanged -= FixedTimeCalendarGrid_FirstDateInViewChanged;

            var startDate = new DateTime(int.Parse(NendoComboBox.SelectedItem.ToString()), 04, 01);
            
            var cv = FixedTimeCalendarGrid.CalendarView as CalendarListView;
            cv.DayCount = Enumerable.Range(0, 12).Select(x => startDate.AddMonths(x + 1)).ToList().Sum(x => x.AddDays(-1).Day);

            var fixres = HttpUtil.GetResponse<FixedTimeDaySettingSearchModel, FixedTimeDaySettingModel>(
                ControllerType.FixedTimeDaySetting, new FixedTimeDaySettingSearchModel()
                {
                    START_DATE = startDate,
                    END_DATE = new DateTime(startDate.AddYears(+1).Year, 3, 31),
                    トラック_ID = editItem.ID
                });
            var fixList = new List<FixedTimeDaySettingModel>();
            if (fixres != null && fixres.Status == Const.StatusSuccess)
            {
                fixList.AddRange(fixres.Results);
            }
            FixedTimeCalendarGrid.FirstDateInView = startDate;
            
            foreach (var fixtime in fixList)
            {
                if (fixtime.FLAG_運休日 == 1)
                {
                    FixedTimeCalendarGrid[fixtime.DATE].Rows[0].Cells[1].Value = "運休";
                }
                else
                {
                    var jikanList = fixtime.時間帯.Split(',').ToList();
                    foreach (var jikan in jikanList)
                    {
                        if (string.IsNullOrWhiteSpace(jikan) ||
                            Expansion2CalendarTemplate.timeHeaderList.Contains(int.Parse(jikan)) == false) { continue; }

                        var d = truckRegularTimeList.Where(x =>
                            x.TIME_ID == int.Parse(jikan) &&
                            x.REGULAR_ID == editItem.REGULAR_TIME_ID);
                        FixedTimeCalendarGrid[fixtime.DATE].Rows[0].Cells[1].Value += d.First().DEPARTURE_TIME + "発 ";
                    }
                }
            }
            
            FixedTimeCalendarGrid.AddSelection(FixedTimeCalendarGrid.FirstDateInView, 0, 1);
            FixedTimeCalendarGrid.ScrollIntoView(FixedTimeCalendarGrid.FirstDateInView);

            FixedTimeCalendarGrid.FirstDateInViewChanged += FixedTimeCalendarGrid_FirstDateInViewChanged;
        }

        /// <summary>
        /// 定期便クリック状態変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegularServiceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ChangeRegularServiceControls(true);
        }

        /// <summary>
        /// 各トラック予約状態変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservationTrackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ChangeRegularServiceControls(false);
        }

        /// <summary>
        /// 定期便関連コントロール状態変更処理
        /// </summary>
        /// <param name="check">チェック状態</param>
        private void ChangeRegularServiceControls(bool check)
        {
            this.FirstStationComboBox.Enabled = check;
            this.TimezoneComboBox.Enabled = check;
            if (this.ScheduleItem.ScheduleItemEdit != ScheduleItemEditType.Insert)
            {
                this.FixedTimeDaySettingGroupBox.Visible = check;
            }
            this.PreOpeningCheckCheckBox.Visible = !check;
        }

        /// <summary>
        /// 定時間日の設定フォーム表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FixedTimeDaySettingOpenButton_Click(object sender, EventArgs e)
        {
            if (CheckEdit(GetFormData(), editItem))
            {
                if (Messenger.Confirm(Resources.KKM00006) == DialogResult.Yes)
                {
                    FormControlUtil.FormWait(this, () =>
                    {
                        if (IsEntrySchedule(ScheduleItem.ScheduleItemEdit))
                        {
                            if(EntryScheduleItem(ScheduleItem.ScheduleItemEdit) == false)
                            {
                                return;
                            }
                        }
                    });
                }
                
                var modelList = this.GetScheduleItem(new TruckScheduleItemSearchModel()
                {
                    ID = editItem.ID
                });

                this.editItem = modelList.FirstOrDefault();

                ChangeRegularServiceControls(editItem.FLAG_定期便 == 1);
                SetControlData();
            }

            using (var form = new FixedTimeDaySettingForm(this.ScheduleItem.ID, int.Parse(NendoComboBox.SelectedItem.ToString())))
            {
                form.NendoComboBoxItems = NendoComboBox.Items.Cast<object>().ToList();
                form.ShowDialog(this);
                UpdateCheck = form.UpdateCheck;
                SetFixTimeGrid();
            }
        }

        /// <summary>
        /// 項目詳細保存処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                if (IsEntrySchedule(this.ScheduleItem.ScheduleItemEdit))
                {
                    if (EntryScheduleItem(this.ScheduleItem.ScheduleItemEdit))
                    {
                        if (this.ScheduleItem.ScheduleItemEdit == ScheduleItemEditType.Insert 
                        || this.ScheduleItem.ScheduleItemEdit == ScheduleItemEditType.Update)
                        {
                            Messenger.Info(Resources.KKM00002);
                        }
                        if (this.ScheduleItem.ScheduleItemEdit == ScheduleItemEditType.Delete)
                        {
                            Messenger.Info(Resources.KKM00003);
                        }

                        base.FormOkClose();
                    }
                }
            });
        }

        /// <summary>
        /// 項目詳細保存
        /// </summary>
        private bool EntryScheduleItem(ScheduleItemEditType type)
        {
            ResponseDto<TruckScheduleItemModel> res = null;

            TruckScheduleItemModel data = GetFormData();

            if (data.FLAG_CHECK == 1 && type == ScheduleItemEditType.Update)
            {
                var scheduleData = TruckData.GetSchedule(new TruckScheduleSearchModel()
                {
                    START_DATE = DateTime.Now.Date,
                    TruckId = this.editItem.ID
                });

                if (scheduleData.Count() > 0)
                {
                    var start = scheduleData.Min(x => x.予約開始時間);
                    var end = scheduleData.Max(x => x.予約終了時間);

                    var list = new List<TruckScheduleItemModel>();
                    list.Add(data);
                    var ckList = TruckData.GetCKList(start.Value, end.Value, list);

                    foreach (var schedule in scheduleData)
                    {
                        if (ckList.Any(x => x.トラック_ID == schedule.トラック_ID &&
                         ((x.予約開始時間 <= schedule.予約開始時間 && schedule.予約開始時間 < x.予約終了時間) ||
                         (x.予約開始時間 < schedule.予約終了時間 && schedule.予約終了時間 <= x.予約終了時間) ||
                         (schedule.予約開始時間 <= x.予約開始時間 && x.予約開始時間 < schedule.予約終了時間) ||
                         (schedule.予約開始時間 < x.予約終了時間 && x.予約終了時間 <= schedule.予約終了時間))
                         )
                         )
                        {
                            Messenger.Warn(Resources.KKM02012);
                            return false;
                        };
                    }
                }
            }

            if (data.FLAG_定期便 == 1)
            {
                var regList = this.GetScheduleItem(new TruckScheduleItemSearchModel()
                {
                    REGULAR_TIME_ID = data.REGULAR_TIME_ID
                });
                if(regList.Any() != false)
                {
                    var maxSortNo = regList.Max(x => x.SORT_NO);
                    data.SORT_NO = maxSortNo + 0.1D;
                }
            }
            else
            {
                if(editItem != null && editItem.FLAG_定期便 == 1 && type == ScheduleItemEditType.Insert)
                {
                    var regList = this.GetScheduleItem(new TruckScheduleItemSearchModel()
                    {
                        REGULAR_TIME_ID = editItem.REGULAR_TIME_ID
                    });
                    if(regList.Any() != false)
                    {
                        var maxSortNo = regList.Max(x => x.SORT_NO);
                        data.SORT_NO = maxSortNo + 0.1D;
                    }
                }
            }

            switch (type)
            {
                case ScheduleItemEditType.Insert:
                    res = HttpUtil.PostResponse(ControllerType.TruckScheduleItem, new[] { data });
                    break;

                case ScheduleItemEditType.Update:
                    res = HttpUtil.PutResponse(ControllerType.TruckScheduleItem, new[] { data });
                    break;

                case ScheduleItemEditType.Delete:
                    res = HttpUtil.DeleteResponse(ControllerType.TruckScheduleItem, new[] { data });
                    break;
            }

            if (res != null && res.Status == Const.StatusSuccess)
            {
                return true;
            }else
            {
                return false;
            }
        }

        /// <summary>
        /// フォームデータ取得処理
        /// </summary>
        /// <returns></returns>
        private TruckScheduleItemModel GetFormData()
        {
            var data = new TruckScheduleItemModel();

            data.車両名 = this.CarNameTextBox.Text;
            data.保管場所 = this.StorageLocationTextBox.Text;
            int flg = 0;
            if (int.TryParse(this.DieselRegulationComboBox.SelectedValue?.ToString(), out flg))
            {
                data.FLAG_ディーゼル規制 = flg;
            }
            data.予約可能開始日 = this.ReservationStartDateTimePicker.Value;
            data.登録番号 = this.RegistrationNumberTextBox.Text;
            data.種類 = this.TypeTextBox.Text;
            data.始発場所 = this.FirstStationComboBox.SelectedItem?.ToString();

            if (RegularServiceRadioButton.Checked)
            {
                int timeid = 0;
                if (int.TryParse(this.TimezoneComboBox.SelectedValue?.ToString(), out timeid))
                {
                    data.REGULAR_TIME_ID = timeid;
                }
            }

            data.備考 = this.RemarksTextBox.Text;
            data.SORT_NO = this.ScheduleItem == null ? 0 : float.Parse(this.ScheduleItem.SortNo.ToString());

            if (ReservationTrackRadioButton.Checked)
            {
                data.FLAG_定期便 = 0;
                data.FLAG_CHECK = (this.PreOpeningCheckCheckBox.Checked) ? 1 : 0;
            }
            else
            {
                data.FLAG_定期便 = 1;
            }

            if (this.ScheduleItem != null)
            {
                data.ID = this.ScheduleItem.ID;
            }

            return data;
        }

        /// <summary>
        /// 項目情報変更確認処理
        /// </summary>
        /// <param name="data"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool CheckEdit(TruckScheduleItemModel data, TruckScheduleItemModel item)
        {
            if (data.FLAG_ディーゼル規制 != item.FLAG_ディーゼル規制) { return true; }
            if (data.予約可能開始日 != item.予約可能開始日) { return true; }
            if ((data.保管場所 ?? string.Empty) != (item.保管場所 ?? string.Empty)) { return true; }
            if ((data.備考 ?? string.Empty) != (item.備考 ?? string.Empty)) { return true; }
            if ((data.始発場所 ?? string.Empty) != (item.始発場所 ?? string.Empty)) { return true; }
            if (data.REGULAR_TIME_ID != item.REGULAR_TIME_ID) { return true; }
            if ((data.登録番号 ?? string.Empty) != (item.登録番号 ?? string.Empty)) { return true; }
            if ((data.種類 ?? string.Empty) != (item.種類 ?? string.Empty)) { return true; }
            if ((data.車両名 ?? string.Empty) != (item.車両名 ?? string.Empty)) { return true; }
            if (data.FLAG_CHECK != item.FLAG_CHECK) { return true; }

            return false;
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <param name="scheduleItemEdit"></param>
        /// <returns></returns>
        private bool IsEntrySchedule(ScheduleItemEditType scheduleItemEdit)
        {
            if (scheduleItemEdit == ScheduleItemEditType.Delete)
            {
                var res = HttpUtil.GetResponse<TruckScheduleSearchModel, TruckScheduleModel>(ControllerType.TruckSchedule,
                    new TruckScheduleSearchModel()
                    {
                        TruckId = editItem.ID
                    });

                var modelList = new List<TruckScheduleModel>();
                if (res != null && res.Status == Const.StatusSuccess && res.Results.Count() > 0)
                {
                    Messenger.Warn(Resources.KKM00033);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                var msg = Validator.GetFormInputErrorMessage(this);
                                
                if (msg != "")
                {
                    Messenger.Warn(msg);
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 項目詳細削除処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                if (Messenger.Confirm(Resources.KKM00007) == DialogResult.Yes)
                {
                    if (IsEntrySchedule(ScheduleItemEditType.Delete))
                    {
                        if (EntryScheduleItem(ScheduleItemEditType.Delete))
                        {
                            base.FormOkClose();
                        }
                    }
                }
            });
        }
        
        /// <summary>
        /// ビューの最初の日付変更時処理
        /// </summary>
        /// <remarks>
        /// 今日へ移動ボタンが押下された場合は年度を再設定します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FixedTimeCalendarGrid_FirstDateInViewChanged(object sender, EventArgs e)
        {
            var today = DateTime.Today;

            if (FixedTimeCalendarGrid.FirstDateInView.Date == today ||
                FixedTimeCalendarGrid.LastDateInView.Date == today)
            {
                ResetNendoDate();
                SetFixTimeGrid();
            }
        }

        /// <summary>
        /// 閉じるボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void CloseButton_Click(object sender, EventArgs e)
        {
            if (CheckEdit(GetFormData(), editItem))
            {
                if (Messenger.Confirm(Resources.KKM00006) == DialogResult.Yes)
                {
                    EntryButton_Click(null, null);
                }
                else
                {
                    base.CloseButton_Click(sender, e);
                }
            }
            else
            {
                base.CloseButton_Click(sender, e);
            }
        }

        /// <summary>
        /// スケジュール項目データ取得処理
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        private List<TruckScheduleItemModel> GetScheduleItem(TruckScheduleItemSearchModel cond)
        {
            var res = HttpUtil.GetResponse<TruckScheduleItemSearchModel, TruckScheduleItemModel>(ControllerType.TruckScheduleItem, cond);

            var modelList = new List<TruckScheduleItemModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                modelList.AddRange(res.Results);
            }
            return modelList;
        }
    }
}
