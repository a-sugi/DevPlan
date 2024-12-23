using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UC;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Config;
using System.Collections.Specialized;
using System.Diagnostics;
using DevPlan.UICommon.Utils.Calendar;
using DevPlan.UICommon.Util;

namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    /// <summary>
    /// カーシェア日程
    /// </summary>
    public partial class CarShareScheduleForm : BaseForm
    {
        #region メンバ変数
        private const int CondHeight = 70;

        private const int SymbolDefault = 1;

        private const string Honyoyaku = "本予約";

        private const int SyaryouYoyakuKigenSpan = 4;
        private const string SyaryouYoyakuKigen = "本車両の使用期限を過ぎています";

        CalendarGridUtil<CarShareScheduleItemModel, CarShareScheduleModel> gridUtil;

        private Func<DateTime?, string> getYMDH = dt => { return dt != null ? ((DateTime)dt).ToString("yyyy/MM/dd H時") : string.Empty; };
        private List<CarManagerModel> carManagerModelList;
        #endregion

        #region プロパティ
        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }

        /// <summary>画面名</summary>
        public override string FormTitle { get { return "カーシェア日程"; } }

        /// <summary>スケジュール項目リスト</summary>
        private IEnumerable<CarShareScheduleItemModel> ScheduleItemList { get; set; }

        /// <summary>カーシェアスケジュール検索条件</summary>
        public CarShareScheduleSearchModel CarShareScheduleSearchCond { get; set; }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>お気に入りID</summary>
        public long? FavoriteID { get; set; }

        private DateTime? _calendarFirstDate = null;

        /// <summary>警告があるスケジュールIDリスト</summary>
        private List<long> checkScheduleList;

        /// <summary>
        /// カレンダーコントロールの左側へ設定したい日付（デフォルトはシステム日付）
        /// </summary>
        public DateTime CalendarFirstDate
        {
            private get
            {
                return (_calendarFirstDate == null) ? DateTime.Now.Date : _calendarFirstDate.Value;
            }
            set
            {
                _calendarFirstDate = value;
            }
        }

        /// <summary>
        /// カレンダーコントロールの一番上へ設定したい項目（ID）
        /// </summary>
        public long? CalendarCategoryId { get; internal set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CarShareScheduleForm()
        {
            InitializeComponent();

        }
        #endregion

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareScheduleForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //権限
                this.UserAuthority = base.GetFunction(FunctionID.CarShare);

                //カレンダーグリッド初期化
                this.InitCalendarGrid();

                //画面初期化
                this.InitForm();

                //検索条件があるかどうか
                if (this.CarShareScheduleSearchCond != null)
                {
                    //スケジュール一覧設定
                    this.SetScheduleList(isMonthFirst: true);
                }

                if (this.CalendarCategoryId != null)
                {
                    this.gridUtil.SetScheduleRowHeaderFirst(this.CalendarCategoryId);
                    this.gridUtil.SetScheduleMostDayFirst(this.CalendarFirstDate);

                    this.CalendarCategoryId = null;
                }

                if (this.CarShareScheduleSearchCond == null && this.FavoriteID == null)
                {
                    //車系ドロップダウン活性化
                    this.CarGroupComboBox.DroppedDown = true;
                }
            });

        }

        /// <summary>
        /// カレンダーグリッド初期化
        /// </summary>
        private void InitCalendarGrid()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isJibuManagement = this.UserAuthority.JIBU_MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            //Append Start 2021/07/20 矢作
            var isCarShareOffice = this.UserAuthority.CARSHARE_OFFICE_FLG == '1';
            //Append Start 2021/07/20 矢作

            var mapItemAdmin = new Dictionary<string, Action<ScheduleItemModel<CarShareScheduleItemModel>>>();
            var mapItemSubAdmin = new Dictionary<string, Action<ScheduleItemModel<CarShareScheduleItemModel>>>();
            var contactMap = new Dictionary<string, Action<ScheduleItemModel<CarShareScheduleItemModel>>>();

            //管理権限あり(自部管理あり)
            if (isManagement == true || isJibuManagement == true)
            {
                mapItemAdmin["項目追加"] = (item => this.ShowScheduleItemDetailForm(item, ScheduleItemEditType.Insert));
                mapItemAdmin["項目編集"] = (item => this.ShowScheduleItemDetailForm(item, ScheduleItemEditType.Update));
                mapItemAdmin["項目削除"] = this.DeleteScheduleItem;
                mapItemAdmin["項目移動"] = (item => this.ShowScheduleItemMoveForm(item));
                mapItemAdmin["管理者追加"] = (item => this.ShowScheduleItemContactInfoForm(item, ScheduleItemEditType.Insert));

                contactMap["項目追加"] = (item => this.ShowScheduleItemDetailForm(item, ScheduleItemEditType.Insert));
                contactMap["項目削除"] = this.DeleteScheduleItem;
                contactMap["管理者追加"] = (item => this.ShowScheduleItemContactInfoForm(item, ScheduleItemEditType.Insert));
                contactMap["管理者編集"] = (item => this.ShowScheduleItemContactInfoForm(item, ScheduleItemEditType.Update));
            }

            //管理権限なし(自部管理あり)
            if (isManagement == false && isJibuManagement == true)
            {
                mapItemSubAdmin["項目追加"] = (item => this.ShowScheduleItemDetailForm(item, ScheduleItemEditType.Insert));
            }

            //カレンダーグリッド設定
            var config = new CalendarGridConfigModel<CarShareScheduleItemModel, CarShareScheduleModel>(this.CarShareCalendarGrid, isManagement || isJibuManagement, isUpdate,
            new Dictionary<string, Action>
            {
                { "項目追加", () => this.ShowScheduleItemDetailForm(new ScheduleItemModel<CarShareScheduleItemModel>(), ScheduleItemEditType.Insert) },
                { "管理者編集", () => this.ShowScheduleItemContactInfoForm() }
            },
            mapItemAdmin,
            new Dictionary<string, Action<ScheduleModel<CarShareScheduleModel>>>
            {
                { "削除",  (schedule => this.DeleteSchedule(schedule, true)) }
            });

            //スケジュール行ヘッダーのコンテキストメニューの項目を取得するデリゲート
            config.GetRowHeaderContexMenuItems = (item) =>
            {
                var i = item.ScheduleItem;
                var list = new List<ToolStripItem>();

                var map = mapItemSubAdmin.Any() && i.INPUT_SECTION_ID == null ? mapItemSubAdmin : mapItemAdmin;
                map = carManagerModelList.Any(x => x.CATEGORY_ID == item.ID.ToString()) ? contactMap : map;

                foreach (var kv in map)
                {
                    //追加するメニューの設定
                    var menu = new ToolStripMenuItem { Text = kv.Key };

                    // イベント追加
                    menu.Click += (sender, e) => kv.Value?.Invoke(item);

                    //メニューを追加
                    list.Add(menu);
                }
                if (item.ScheduleItem.XEYE_EXIST != null)
                {
                    var menu = new ToolStripMenuItem { Text = "GPS車両検索" };
                    menu.Click += (sender2, e2) => {
                        RunXeyeWebBrowser(item);
                    };
                    list.Add(menu);
                }

                if (list.Any()) { list.Add(new ToolStripSeparator()); }
                return list.ToArray();
            };
            
            config.GetRowHeaderCommonContexMenuItems = (item) =>
            {
                var i = item.ScheduleItem;
                var list = new List<ToolStripItem>();

                list.Add(new ToolStripSeparator());

                //項目作成元をメニューへ追加
                if (!string.IsNullOrWhiteSpace(i.INPUT_SECTION_ID))
                {
                    list.Add(new ToolStripMenuItem("[作成元:SKCKA]"));
                }
                else
                {
                    list.Add(new ToolStripMenuItem("[作成元:SJSB]"));
                }

                return list.ToArray();
            };

            //Update Start 2021/07/20 矢作
            //スケジュール表示期間変更可否
            //config.IsScheduleViewPeriodChange = (isManagement || isJibuManagement);
            config.IsScheduleViewPeriodChange = (isManagement || isJibuManagement || isCarShareOffice);
            //Update End 2021/07/20 矢作

            //スケジュールの背景色取得
            config.GetScheduleBackColor = (schedule, colorEnum) =>
            {
                var s = schedule.Schedule;

                var isHonyoyaku = s.予約種別 == Honyoyaku;
                var now = DateTime.Now;

                //予約許可が必要かどうか
                if (s.FLAG_要予約許可 == 1)
                {
                    //本予約かどうか
                    colorEnum = isHonyoyaku ? CalendarScheduleColorEnum.YoyakuKyokaHonyoyaku : CalendarScheduleColorEnum.YoyakuKyokaKariyoyaku;
                }
                //予約種別が本予約かどうか
                else if (isHonyoyaku == true)
                {
                    //開始日が過去の日時かどうか
                    if (s.START_DATE < now)
                    {
                        //利用実績があるかどうか
                        if (s.FLAG_実使用 == 1)
                        {
                            //返却済かどうか
                            colorEnum = s.FLAG_返却済 == 1 ? CalendarScheduleColorEnum.HonyoyakuSiyouHenkyaku : CalendarScheduleColorEnum.HonyoyakuSiyouMihenkyaku;
                        }
                        else
                        {
                            //現在日時が終了日以降かどうか
                            colorEnum = now < s.END_DATE ? CalendarScheduleColorEnum.HonyoyakuSiyoutyuu : CalendarScheduleColorEnum.HonyoyakuEnd;
                        }

                    }
                    else
                    {
                        colorEnum = CalendarScheduleColorEnum.HonyoyakuFuture;
                    }

                }
                //該当なし
                else
                {
                    colorEnum = CalendarScheduleColorEnum.Other;
                }
                
                if (this.checkScheduleList != null && this.checkScheduleList.Contains(schedule.ID))
                {
                    if (isHonyoyaku == true)
                    {
                        this.checkScheduleList.RemoveAll(x => x == schedule.ID);
                    }
                    else
                    {
                        colorEnum = CalendarScheduleColorEnum.CheckItemColor;
                    }
                }

                return colorEnum;

            };

            //セルの背景色を取得するデリゲート
            config.GetCellBackColor = (item, day, colorEnum) =>
            {
                var scheduleItem = item.ScheduleItem;

                var last = scheduleItem.最終予約可能日;

                //現在日が最終予約可能日を超過しているかどうか
                if (last != null && day > last.Value.Date)
                {
                    colorEnum = CalendarScheduleColorEnum.NoneReservation;

                }

                return colorEnum;
            };

            //スケジュールのツールチップテキストのタイトルを取得するデリゲート
            //Update Start 2022/02/21 杉浦 入れ替え中車両の処理
            //config.GetScheduleToolTipTitle = schedule => schedule.Schedule.DESCRIPTION;
            config.GetScheduleToolTipTitle = schedule => string.IsNullOrEmpty(schedule.Schedule.REPLACEMENT_TEXT) ? schedule.Schedule.DESCRIPTION : schedule.Schedule.DESCRIPTION + "【車両入替発生】";
            //Update End 2022/02/21 杉浦 入れ替え中車両の処理

            //スケジュールの追加するツールチップテキストを取得するデリゲート
            config.GetScheduleAddToolTipText = schedule => string.Format("{0} {1}({2})", schedule.Schedule.予約者_SECTION_CODE, schedule.Schedule.予約者_NAME, schedule.Schedule.TEL);
            
            //その他スケジュール
            config.OtherSchedule = new OtherScheduleModel<CarShareScheduleItemModel>(CalendarScheduleColorEnum.NoneReservation.MainColor, CalendarScheduleColorEnum.ReservationOver.MainColor, SyaryouYoyakuKigenSpan, SyaryouYoyakuKigen, (item => item.ScheduleItem.最終予約可能日 == null ? null : (DateTime?)item.ScheduleItem.最終予約可能日.Value.AddDays(1).Date));

            //スケジュール項目の背景色を取得するデリゲート
            config.GetScheduleItemBackColor = x => SetScheduleItemBackColor(x.ScheduleItem);

            //カレンダーの設定情報
            config.CalendarSettings = new CalendarSettings(Properties.Settings.Default.CarShareCalendarStyle);

            //試験車管理SYS情報フィルタ利用可否
            config.IsRowHeaderSysFilter = isManagement;

            //Update Start 2021/10/15 杉浦 一般権限の予約可能期間を操作日から2ヶ月になるようにしてほしい
            ////Append Start 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理
            ////カーシェア日程表示期間設定
            //config.ViewRange = 2;
            config.ViewRange = 3;
            ////Append End 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理
            //Update End 2021/10/15 杉浦 一般権限の予約可能期間を操作日から2ヶ月になるようにしてほしい

            //カレンダーグリッドの初期設定
            this.gridUtil = new CalendarGridUtil<CarShareScheduleItemModel, CarShareScheduleModel>(config);

            //スケジュール表示期間の変更後のデリゲート
            this.gridUtil.ScheduleViewPeriodChangedAfter += (start, end) => FormControlUtil.FormWait(this, () => this.SetSchedule(start, end));

            //スケジュール行ヘッダーダブルクリックのデリゲート
            this.gridUtil.ScheduleRowHeaderDoubleClick += this.ShowHistoryScheduleItem;

            //スケジュール項目の並び順変更後のデリゲート
            this.gridUtil.ScheduleItemSortChangedAfter += (sourceItem, destItem) => FormControlUtil.FormWait(this, () => this.UpdateScheduleItemSort(sourceItem, destItem));

            //スケジュールダブルクリックのデリゲート
            this.gridUtil.ScheduleDoubleClick += schedule => this.ShowScheduleDetailForm(schedule, ScheduleEditType.Update);

            //スケジュールの日付範囲の変更後のデリゲート
            this.gridUtil.ScheduleDayRangeChangedAfter += schedule => FormControlUtil.FormWait(this, () => this.RangeChangedSchedule(schedule));

            //スケジュールの空白領域をドラッグ後のデリゲート
            this.gridUtil.ScheduleEmptyDragAfter += schedule => this.ShowScheduleDetailForm(schedule, ScheduleEditType.Insert);

            //スケジュール空白領域をダブルクリックのデリゲート
            this.gridUtil.ScheduleEmptyDoubleClick += schedule => this.ShowScheduleDetailForm(schedule, ScheduleEditType.Insert);

            //スケジュール削除のデリゲート
            this.gridUtil.ScheduleDelete += schedule => FormControlUtil.FormWait(this, () => this.DeleteSchedule(schedule, false));

            //スケジュール貼り付けのデリゲート
            this.gridUtil.SchedulePaste += (copySchedule, schedule) => FormControlUtil.FormWait(this, () => this.PasteSchedule(schedule));

            //カレンダーレイアウト状態保存デリゲート
            this.gridUtil.CalendarSetting.SaveCalendarUserData += this.SaveCalendarUserData;

            //カレンダーの表示期間変更（管理権限がない場合はシステム日付）
            DateTime toDay;
            if (isManagement || this.gridUtil.CalendarSetting.CurrentStyle.Range == 1)
            {
                toDay = this.CalendarFirstDate;
            }
            else
            {
                toDay = DateTime.Now;
            }
            this.gridUtil.SetCalendarViewPeriod(new DateTime(toDay.Year, toDay.Month, 1));

            this.gridUtil.UndoRedo += (schedule) => this.UndoRedo(schedule);
        }

        /// <summary>
        /// スケジュール項目背景色算出処理。
        /// </summary>
        /// <param name="scheduleItem"></param>
        /// <returns></returns>
        private CalendarScheduleColorEnum SetScheduleItemBackColor(CarShareScheduleItemModel scheduleItem)
        {
            if (carManagerModelList.Any(x => x.CATEGORY_ID == scheduleItem.CATEGORY_ID.ToString()))
            {
                return CalendarScheduleColorEnum.ContactInfoItemColor;
            }

            if (this.CalendarCategoryId != null && scheduleItem.CATEGORY_ID == this.CalendarCategoryId)
            {
                return CalendarScheduleColorEnum.CheckItemColor;
            }

            if (scheduleItem.FLAG_要予約許可 == 1)
            {
                return CalendarScheduleColorEnum.TentativeReservationColor;
            }
            else
            {
                return CalendarScheduleColorEnum.NoneReservationColor;
            }
        }

        /// <summary>
        /// カレンダー状態保存実行。
        /// </summary>
        /// <remarks>
        /// カレンダーの状態を取得し、Settingsへ保存します。
        /// </remarks>
        /// <param name="style">保存を行うカレンダー設定情報</param>
        private void SaveCalendarUserData(StringCollection style)
        {
            Properties.Settings.Default.CarShareCalendarStyle = style;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //バインド中ON
            this.IsBind = true;

            try
            {
                var isExport = this.UserAuthority.EXPORT_FLG == '1';
                var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
                var isJibuManagement = this.UserAuthority.JIBU_MANAGEMENT_FLG == '1';
                //Append Start 2021/07/20 矢作
                var isCarShareOffice = this.UserAuthority.CARSHARE_OFFICE_FLG == '1';
                //Append Start 2021/07/20 矢作

                Action<NullableDateTimePicker> setDataMinMax = dtp =>
                {
                    //管理権限がなければ表示期間内のみの入力制限を設定
                    //Update Start 2021/07/20 矢作
                    //if (isManagement == false && isJibuManagement == false)
                    if (isManagement == false && isJibuManagement == false && isCarShareOffice == false)
                    //Update Start 2021/07/20 矢作
                    {
                        //当月を含む３ヶ月のみ予約が可能。カレンダー共通にも同様の制限が入っている。
                        DateTime toDay = DateTime.Today;
                        DateTime start = new DateTime(toDay.Year, toDay.Month, 1);
                        //Update Start 2021/09/27 杉浦　一般権限表示期間変更(３→２ヶ月)
                        //DateTime end = start.AddMonths(3).AddDays(-1);
                        DateTime end = start.AddMonths(2).AddDays(-1);
                        //Update End 2021/09/27 杉浦　一般権限表示期間変更

                        dtp.MinDate = start;
                        dtp.MaxDate = end;
                    }
                };

                //検索条件
                this.SearchConditionTableLayoutPanel.Visible = true;

                //空車期間(From)日付
                this.BlankCarFromDateTimePicker.Value = null;
                setDataMinMax(this.BlankCarFromDateTimePicker);

                //空車期間(From)時刻
                this.BlankCarFromComboBox.SelectedIndex = 0;

                //空車期間(From)日付
                this.BlankCarToDateTimePicker.Value = null;
                setDataMinMax(this.BlankCarToDateTimePicker);

                //空車期間(From)時刻
                this.BlankCarToComboBox.SelectedIndex = this.BlankCarToComboBox.Items.Count - 1;

                //車系
                FormControlUtil.SetComboBoxItem(this.CarGroupComboBox, HttpUtil.GetResponse<CarGroupSearchInModel, CarGroupSearchOutModel>(ControllerType.CarGroup, new CarGroupSearchInModel
                {
                    // ユーザーID
                    PERSONEL_ID = SessionDto.UserId,

                    //開発フラグ
                    UNDER_DEVELOPMENT = 1

                }).Results);
                this.CarGroupComboBox.SelectedIndex = 0;

                //ダウンロードボタン
                this.ExcelPrintButton.Visible = isExport;

            }
            finally
            {
                //バインド中OFF
                this.IsBind = false;

            }

            //お気に入りIDがあるかどうか
            if (this.FavoriteID != null)
            {
                FormControlUtil.FormWait(this, () =>
                {
                    //バインド中かお気に入りを設定できなければ終了
                    if (this.IsBind == true || this.SetFavoriteCondition() == false)
                    {
                        return;

                    }

                    //スケジュール一覧設定
                    this.SetScheduleList(isMonthFirst: true);

                });
            }
            else if (this.CarShareScheduleSearchCond != null)
            {
                this.CarGroupComboBox.Text = this.CarShareScheduleSearchCond.CAR_GROUP;
            }
        }
        #endregion

        #region お気に入り選択

        /// <summary>
        /// お気に入りの検索条件を設定
        /// </summary>
        private bool SetFavoriteCondition()
        {
            //バインド中ON
            this.IsBind = true;

            try
            {
                //お気に入りが取得できたかどうか
                var favorite = this.GetFavorite(this.FavoriteID.Value);
                if (favorite == null)
                {
                    return false;
                }

                //車系
                this.CarGroupComboBox.Text = favorite.CAR_GROUP;

                //OPEN/CLOSE
                this.StatusOpenCheckBox.Checked = favorite.STATUS_OPEN_FLG == "1";
                this.StatusCloseCheckBox.Checked = favorite.STATUS_CLOSE_FLG == "1";

                return true;

            }
            finally
            {
                //バインド中OFF
                this.IsBind = false;
            }
        }
        #endregion

        #region 車系選択
        /// <summary>
        /// 車系選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarGroupComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //バインド中か先頭の項目を選択なら終了
            if (this.IsBind == true || this.CarGroupComboBox.SelectedIndex == 0)
            {
                return;
            }

            SearchSchedule(true);
            this.CarShareCalendarGrid.VerticalScrollBarOffset = 0;
        }
        #endregion

        #region 検索条件ボタンクリック
        /// <summary>
        /// 検索条件ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            //検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, CondHeight);

        }
        #endregion

        #region 検索ボタンクリック
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchSchedule(this.BlankCarFromDateTimePicker.SelectedDate != null);
        }
        #endregion

        /// <summary>
        /// 検索実行。
        /// </summary>
        /// <remarks>
        /// 設定された検索条件を元に検索を実行します。
        /// </remarks>
        private void SearchSchedule(bool isMonthFirst)
        {
            //空車期間が設定されている場合はカレンダーも再設定
            if (this.IsSearchSchedule() == false) { return; }
            if (this.BlankCarFromDateTimePicker.SelectedDate != null)
            {
                var date = this.BlankCarFromDateTimePicker.SelectedDate.Value;
                this.gridUtil.SetCalendarViewPeriod(new DateTime(date.Year, date.Month, 1));
            }

            //スケジュール一覧設定
            this.gridUtil.ResetFilter();
            FormControlUtil.FormWait(this, () => this.SetScheduleList(isMonthFirst: isMonthFirst));

            this.ActiveControl = CarShareCalendarGrid;
        }

        #region 条件登録ボタンクリック
        /// <summary>
        /// 条件登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteEntryButton_Click(object sender, EventArgs e)
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;

            }

            //お気に入りが上限件数まで登録済かどうか
            if (this.GetFavoriteList().Count() >= Const.FavoriteEntryMax)
            {
                Messenger.Warn(Resources.KKM00016);
                return;

            }

            var favorite = new CarShareFavoriteItemModel
            {
                //車系
                CAR_GROUP = this.CarGroupComboBox.Text,
                //OPEN
                STATUS_OPEN_FLG = this.StatusOpenCheckBox.Checked ? "1" : "0",
                //CLOSE
                STATUS_CLOSE_FLG = this.StatusCloseCheckBox.Checked ? "1" : "0"
            };

            using (var form = new FavoriteEntryForm(favorite))
            {
                form.ShowDialog(this);
            }

        }
        #endregion

        #region クリアボタンクリック
        /// <summary>
        /// クリアボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.MessageLabel.Text = Resources.KKM00047;

            //車系
            this.CarGroupComboBox.SelectedIndex = 0;
            FormControlUtil.SetComboBoxBackColor(this.CarGroupComboBox, Const.DefaultBackColor);

            //空車期間(From)
            this.BlankCarFromDateTimePicker.Value = null;
            this.BlankCarFromDateTimePicker.BackColor = Const.DefaultBackColor;
            this.BlankCarFromComboBox.SelectedIndex = 0;
            FormControlUtil.SetComboBoxBackColor(this.BlankCarFromComboBox, Const.DefaultBackColor);

            //空車期間(To)
            this.BlankCarToDateTimePicker.Value = null;
            this.BlankCarToDateTimePicker.BackColor = Const.DefaultBackColor;
            this.BlankCarToComboBox.SelectedIndex = this.BlankCarToComboBox.Items.Count - 1;
            FormControlUtil.SetComboBoxBackColor(this.BlankCarToComboBox, Const.DefaultBackColor);

            // 項目ステータス
            this.StatusOpenCheckBox.Checked = true;
            this.StatusCloseCheckBox.Checked = false;
        }
        #endregion

        #region スケジュール設定
        /// <summary>
        /// スケジュール一覧設定
        /// </summary>
        private void SetScheduleList()
        {
            this.SetScheduleList(false, true);
        }
        /// <summary>
        /// スケジュール一覧設定
        /// </summary>
        /// <param name="isMonthFirst">基準月先頭可否</param>
        /// <param name="isNewSearch">初回検索フラグ</param>
        private void SetScheduleList(bool isMonthFirst = false, bool isNewSearch = true)
        {
            //スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;

            }

            //スケジュール項目取得
            this.ScheduleItemList = this.GetScheduleItemList();
            
            //スケジュール設定
            this.SetSchedule(isNewSearch);

            //基準月先頭可否
            if (isMonthFirst == true)
            {
                //空車期間の開始日が設定してあるかどうか
                var start = this.BlankCarFromDateTimePicker.SelectedDate;
                if (start != null)
                {
                    //空車期間の開始日を横スクロールの開始日に設定
                    this.gridUtil.SetScheduleMostDayFirst(start.Value);
                }
                else
                {
                    this.gridUtil.SetScheduleMostDayFirst(this.CalendarFirstDate);
                }
            }

        }

        /// <summary>
        /// スケジュール設定
        /// </summary>
        /// <param name="start">開始日</param>
        /// <param name="end">終了日</param>
        /// <remarks></remarks>
        private void SetSchedule(DateTime start, DateTime end)
        {
            //入力がOKかどうか
            if (!this.IsSearchSchedule()) return;

            //スケジュール設定
            this.SetSchedule();

            //空車期間が設定されているかどうか
            var date = this.BlankCarFromDateTimePicker.SelectedDate;
            if (date != null)
            {
                //スケジュールの先頭を設定
                this.gridUtil.SetScheduleMostDayFirst(start <= date && date <= end ? date.Value : start);

            }
            else
            {
                //スケジュールの先頭を設定
                this.gridUtil.SetScheduleMostDayFirst(start);

            }

        }

        /// <summary>
        /// スケジュール設定
        /// </summary>
        private void SetSchedule(bool isNewSearch = true)
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isJibuManagement = this.UserAuthority.JIBU_MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isCarShareOffice = this.UserAuthority.CARSHARE_OFFICE_FLG == '1';

            Func<CarShareScheduleModel, string> getSubTitle =
                schedule => string.Format("{0}{1}", (schedule.予約種別 == Const.Kariyoyaku ? Const.Kari : ""), schedule.DESCRIPTION);
            Func<CarShareScheduleItemModel, CarShareScheduleModel, bool> isEdit = (item, schedule) =>
            (isManagement == true || ((isCarShareOffice == true || isJibuManagement == true) && !string.IsNullOrWhiteSpace(item.INPUT_SECTION_ID))) ? isUpdate :
            (schedule.予約種別 == ReservationStautsEnum.HON_YOYAKU.Key && schedule.FLAG_要予約許可 == 1) ? false :
            (schedule.END_DATE.Value.Date < DateTime.Now.Date) ? false :
            isUpdate == true && schedule.予約者_ID == SessionDto.UserId;

            // 検索条件のチェック
            if (!this.IsSearchSchedule()) return;

            //検索結果文言
            if (isNewSearch)
            {
                this.MessageLabel.Text = (this.ScheduleItemList == null || this.ScheduleItemList.Any() == false) ? Resources.KKM00005 : Resources.KKM00047;
            }

            //カレンダーのコーナーヘッダー
            this.gridUtil.CornerHeaderText = this.GetCarManager();
            
            //スケジュール取得
            var scheduleList = this.GetScheduleList();

            //カレンダーにデータバインド
            this.gridUtil.Bind(this.ScheduleItemList, scheduleList, x => x.ID, y => y.CATEGORY_ID.Value,
                x => new ScheduleItemModel<CarShareScheduleItemModel>(x.ID, x.GENERAL_CODE, x.CATEGORY, (x.FLAG_要予約許可 == 1) ? "予約許可必要" : "予約許可不要", x.PARALLEL_INDEX_GROUP.Value, x.SORT_NO, x.管理票番号, x),
               (y, x) => new ScheduleModel<CarShareScheduleModel>(y.ID, y.CATEGORY_ID.Value, y.PARALLEL_INDEX_GROUP.Value, getSubTitle(y), y.START_DATE, y.END_DATE, y.INPUT_DATETIME, y.SYMBOL, false, isEdit(x.ScheduleItem, y), y));

        }
        #endregion

        #region スケジュール検索のチェック
        /// <summary>
        /// スケジュール検索のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchSchedule()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            var start = this.GetDateTime(this.BlankCarFromDateTimePicker, this.BlankCarFromComboBox);
            var end = this.GetDateTime(this.BlankCarToDateTimePicker, this.BlankCarToComboBox);

            //空車期間の必須チェック
            map[this.BlankCarFromDateTimePicker] = (c, name) =>
            {
                //空車期間Toのみ入力かどうか
                if (this.BlankCarFromDateTimePicker.SelectedDate == null && this.BlankCarToDateTimePicker.SelectedDate != null)
                {
                    //背景色を変更
                    this.BlankCarFromDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.BlankCarToDateTimePicker.BackColor = Const.ErrorBackColor;

                    //エラーメッセージ
                    return Resources.KKM03015;
                }

                //空車期間Fromのみ入力かどうか
                if (this.BlankCarFromDateTimePicker.SelectedDate != null && this.BlankCarToDateTimePicker.SelectedDate == null)
                {
                    //背景色を変更
                    this.BlankCarFromDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.BlankCarToDateTimePicker.BackColor = Const.ErrorBackColor;

                    //エラーメッセージ
                    return Resources.KKM03015;
                }

                //期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー
                if (this.BlankCarFromDateTimePicker.SelectedDate != null && this.BlankCarToDateTimePicker.SelectedDate != null && start > end)
                {
                    //背景色を変更
                    this.BlankCarFromDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.BlankCarToDateTimePicker.BackColor = Const.ErrorBackColor;
                    FormControlUtil.SetComboBoxBackColor(this.BlankCarFromComboBox, Const.ErrorBackColor);
                    FormControlUtil.SetComboBoxBackColor(this.BlankCarToComboBox, Const.ErrorBackColor);

                    //エラーメッセージ
                    return Resources.KKM00018;
                }

                return string.Empty;
            };

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            return true;

        }
        #endregion

        #region スケジュール項目のイベント
        /// <summary>
        /// 作業履歴(カーシェア)表示
        /// </summary>
        /// <param name="item">スケジュール項目</param>
        private void ShowHistoryScheduleItem(ScheduleItemModel<CarShareScheduleItemModel> item)
        {
            //スケジュール項目のチェック
            if (this.IsEntryScheduleItem(ScheduleItemEditType.Update, item) == false)
            {
                return;

            }

            //作業履歴画面表示
            new FormUtil(new CarShareHistoryForm { ScheduleItem = item.ScheduleItem, UserAuthority = this.UserAuthority, Reload = SetScheduleList, IsItemNameEdit = !carManagerModelList.Any(x => x.CATEGORY_ID == item.ID.ToString()) }).SingleFormShow(this);
        }

        /// <summary>
        /// スケジュール項目詳細画面表示
        /// </summary>
        /// <param name="item">スケジュール項目</param>
        /// <param name="type">編集モード</param>
        private void ShowScheduleItemDetailForm(ScheduleItemModel<CarShareScheduleItemModel> item, ScheduleItemEditType type)
        {
            //スケジュール項目のチェック
            if (this.IsEntryScheduleItem(type, item) == false)
            {
                return;

            }

            //編集モード
            item.ScheduleItemEdit = type;

            //追加かどうか
            if (type == ScheduleItemEditType.Insert)
            {
                //スケジュール項目初期化
                var scheduleItem = new CarShareScheduleItemModel
                {
                    //車系
                    CAR_GROUP = this.CarShareScheduleSearchCond.CAR_GROUP,

                    //ソート順
                    SORT_NO = 0,

                    //行数
                    PARALLEL_INDEX_GROUP = 1

                };

                //スケジュール項目がある場合はソート順を設定
                if (item.ScheduleItem != null)
                {
                    //ソート順
                    scheduleItem.SORT_NO = item.SortNo + 0.1D;

                }

                //Append Start 2022/03/10 杉浦 重複登録回避処理
                item.ID = 0;
                //Append End 2022/03/10 杉浦 重複登録回避処理

                //スケジュール項目再設定
                item.ScheduleItem = scheduleItem;

            }

            // 更新かどうか
            if (type == ScheduleItemEditType.Update)
            {
                item.ScheduleItem.CAR_GROUP = this.CarShareScheduleSearchCond.CAR_GROUP;
            }

            using (var form = new ScheduleItemDetailForm<CarShareScheduleItemModel, CarShareScheduleModel>()
            {
                FormSubTitle = "カーシェア",
                FunctionId = FunctionID.CarShare,
                Item = item
            })
            {
                var temp = item.ScheduleItem.FLAG_要予約許可;

                //OKの場合は再検索
                if (form.ShowDialog(this) == DialogResult.OK || form.IsReload)
                {
                    if (temp == 1 && item.ScheduleItem.FLAG_要予約許可 == 0)
                    {
                        var schecule = this.GetScheduleList(new CarShareScheduleSearchModel()
                        {
                            START_DATE = DateTime.Now.Date,
                            CATEGORY_ID = item.ID
                        });
                        this.checkScheduleList = schecule.Where(x => x.予約種別 == Const.Kariyoyaku)?.Select(x => x.ID)?.ToList();

                        if (this.checkScheduleList != null && this.checkScheduleList.Any())
                        {
                            Messenger.Warn(string.Format(Resources.KKM02014, this.FormTitle));
                        }
                    }

                    //スケジュール一覧設定
                    this.SetScheduleList();
                }
            }

        }

        /// <summary>
        /// 項目移動フォーム表示
        /// </summary>
        /// <param name="item"></param>
        private void ShowScheduleItemMoveForm(ScheduleItemModel<CarShareScheduleItemModel> item)
        {
            if (this.IsEntryScheduleItem(ScheduleItemEditType.Update, item) == false)
            {
                return;
            }

            using (var form = new ScheduleItemCarGroupMoveForm<CarShareScheduleItemModel>() { FormSubTitle = "カーシェア", ScheduleItem = item, FormSubType = ScheduleItemType.CarShare })
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    this.SetScheduleList();
                }
            }
        }

        /// <summary>
        /// スケジュール項目削除
        /// </summary>
        /// <param name="item">スケジュール項目</param>
        private void DeleteScheduleItem(ScheduleItemModel<CarShareScheduleItemModel> item)
        {
            //削除可否を問い合わせ
            if (Messenger.Confirm(Resources.KKM00007) != DialogResult.Yes)
            {
                return;

            }

            //スケジュール項目ののチェック
            if (this.IsEntryScheduleItem(ScheduleItemEditType.Delete, item) == false)
            {
                return;

            }

            //編集モード
            item.ScheduleItemEdit = ScheduleItemEditType.Delete;

            //管理者連絡先項目の場合連絡先も削除する
            if (carManagerModelList.Any(x => x.ID == item.ID.ToString()))
            {
                var resManager = HttpUtil.GetResponse<CarManagerSearchModel, CarManagerModel>(ControllerType.CarManager,
                    new CarManagerSearchModel { CATEGORY_ID = item.ID.ToString() });

                List<CarManagerModel> resManagerList = null;
                if (resManager != null && resManager.Status == Const.StatusSuccess)
                {
                    resManagerList.AddRange(resManager.Results);
                }
                HttpUtil.DeleteResponse(ControllerType.CarManager, resManagerList);
            }

            var list = new[] { item.ScheduleItem };

            //レスポンスが取得できたかどうか
            var res = HttpUtil.DeleteResponse(ControllerType.CarShareScheduleItem, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //削除メッセージ
                Messenger.Info(Resources.KKM00003);

                //スケジュール一覧設定
                this.SetScheduleList();

            }


        }

        /// <summary>
        /// スケジュール項目の並び順変更
        /// </summary>
        /// <param name="sourceItem">変更対象のスケジュール項目</param>
        /// <param name="destItem">ドラッグ先のスケジュール項目</param>
        private void UpdateScheduleItemSort(ScheduleItemModel<CarShareScheduleItemModel> sourceItem, ScheduleItemModel<CarShareScheduleItemModel> destItem)
        {
            //スケジュール項目のチェック
            if (this.IsEntryScheduleItem(ScheduleItemEditType.Update, sourceItem) == false ||
                this.IsEntryScheduleItem(ScheduleItemEditType.Update, destItem) == false)
            {
                return;

            }

            //編集モード
            sourceItem.ScheduleItemEdit = destItem.ScheduleItemEdit = ScheduleItemEditType.Update;

            var list = new[] { sourceItem.ScheduleItem };

            //行数
            sourceItem.ScheduleItem.SORT_NO = sourceItem.SortNo;

            //ユーザー情報設定
            sourceItem.ScheduleItem.SetUserInfo();

            //レスポンスが取得できたかどうか
            var res = HttpUtil.PutResponse(ControllerType.CarShareScheduleItem, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //更新メッセージ
                this.MessageLabel.Text = Resources.KKM00002;

                //スケジュール一覧設定
                this.SetScheduleList(isNewSearch: false);

            }

        }
        #endregion

        #region スケジュール項目のチェック
        /// <summary>
        /// スケジュール項目のチェック
        /// </summary>
        /// <param name="item">スケジュール項目</param>
        /// <returns>登録可否</returns>
        private bool IsEntryScheduleItem(ScheduleItemEditType type, ScheduleItemModel<CarShareScheduleItemModel> item)
        {
            //スケジュール項目編集区分ごとの分岐
            switch (type)
            {
                //追加
                case ScheduleItemEditType.Insert:
                    //入力がOKかどうか
                    var msg = Validator.GetFormInputErrorMessage(this);
                    if (msg != "")
                    {
                        Messenger.Warn(msg);
                        return false;

                    }
                    break;

                //更新
                //削除
                //行追加
                //行削除
                case ScheduleItemEditType.Update:
                case ScheduleItemEditType.Delete:
                    //スケジュール項目が存在しているかどうか
                    var list = this.GetScheduleItemList(new CarShareScheduleSearchModel { CAR_GROUP = item.ScheduleItem.CAR_GROUP, ID = item.ID });
                    if (list == null || list.Any() == false)
                    {
                        //存在していない場合はエラー
                        Messenger.Warn(Resources.KKM00021);
                        return false;

                    }
                    else
                    {
                        //スケジュール項目再設定
                        item.ScheduleItem = list.First();

                    }
                    break;

            }

            //スケジュールの検索条件
            var cond = new CarShareScheduleSearchModel
            {
                //カテゴリーID
                CATEGORY_ID = item?.ScheduleItem?.CATEGORY_ID

            };

            //スケジュール項目編集区分ごとの分岐
            switch (type)
            {
                //削除
                case ScheduleItemEditType.Delete:
                    //スケジュールがある場合はエラー
                    if (this.GetScheduleList(cond).Any() == true)
                    {
                        Messenger.Warn(Resources.KKM00033);
                        return false;

                    }
                    break;
            }

            return true;

        }
        #endregion

        #region スケジュールのイベント
        /// <summary>
        /// スケジュール詳細画面表示
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        /// <param name="type">編集モード</param>
        private void ShowScheduleDetailForm(ScheduleModel<CarShareScheduleModel> schedule, ScheduleEditType type)
        {
            //項目ごとのスケジュール登録確認
            if (type == ScheduleEditType.Insert)
            {
                if (this.IsRestrictionSchedule(schedule) == false)
                {
                    this.SetScheduleList();
                    return;
                }
            }

            //スケジュールのチェック
            var checkSameLine = (type == ScheduleEditType.Update) ? false : true;
            //Update Start 2022/03/07 杉浦 スケジュール系不具合修正
            //if (this.IsEntrySchedule(type, schedule, checkSameLine) == false)
            if (this.IsEntrySchedule(type, schedule, false, checkSameLine) == false)
            //Update End 2022/03/07 杉浦 スケジュール系不具合修正
            {
                //スケジュール一覧設定
                this.SetScheduleList();
                return;

            }

            if (ShowNoteDialog(type, schedule) != DialogResult.OK) { return; }

            //編集モード
            schedule.ScheduleEdit = type;

            //編集モードが追加の場合はスケジュールの初期化
            if (type == ScheduleEditType.Insert)
            {
                schedule.Schedule = new CarShareScheduleModel
                {
                    //車系
                    CAR_GROUP = this.CarShareScheduleSearchCond.CAR_GROUP,

                    //開発符号
                    GENERAL_CODE = schedule.GeneralCode,

                    //シンボル
                    SYMBOL = SymbolDefault,

                    //カテゴリーーID
                    CATEGORY_ID = schedule.CategoryID,

                    //行番号
                    PARALLEL_INDEX_GROUP = schedule.RowNo,

                    //開始日
                    START_DATE = schedule.StartDate,

                    //終了日
                    END_DATE = schedule.EndDate

                };

            }

            //OKの場合は再検索
            if (ShowDetailForm(schedule) == DialogResult.OK)
            {
                //スケジュール一覧設定
                this.SetScheduleList(false, false);

                if (schedule.ScheduleEdit != ScheduleEditType.Update)
                {
                    this.gridUtil.UndoRedoManager.Do(schedule);
                }
            }
        }

        /// <summary>
        /// スケジュール詳細画面表示処理。
        /// </summary>
        /// <param name="schedule"></param>
        private DialogResult ShowDetailForm(ScheduleModel<CarShareScheduleModel> schedule)
        {
            DialogResult result = DialogResult.Cancel;
            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
            var kanriNo = this.ScheduleItemList.Where(x => x.ID == schedule.CategoryID).Select(x => x.管理票番号).ToList();
            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する

            //Update Start 2022/02/03 杉浦 試験車日程の車も登録する
            //using (var form = new CarShareScheduleDetailForm { Schedule = schedule, UserAuthority = this.UserAuthority })
            using (var form = new CarShareScheduleDetailForm { Schedule = schedule, UserAuthority = this.UserAuthority, KanriNo = kanriNo[0] })
            //Update End 2022/02/03 杉浦 試験車日程の車も登録する
            {
                //自部署フラグの場合も制限解除の可能性はあるが、項目によるため詳細画面へチェックをまかせる。
                var date = GetChangeDateTime();

                var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
                if (isManagement)
                {
                    form.SelectMaxDate = this.CarShareCalendarGrid.MaxDate;
                    form.SelectMinDate = this.CarShareCalendarGrid.MinDate;
                }
                else
                {
                    form.SelectMaxDate = date[1];
                    form.SelectMinDate = date[0];
                }

                //OKの場合は再検索
                result = form.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // 更新・削除メッセージ
                    this.MessageLabel.Text = form.ReturnMessage;

                    if (schedule.ScheduleEdit != ScheduleEditType.Update)
                    {
                        this.gridUtil.UndoRedoManager.Do(schedule);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 注意喚起ダイアログ表示
        /// </summary>
        /// <param name="type"></param>
        /// <param name="schedule"></param>
        private DialogResult ShowNoteDialog(ScheduleEditType type, ScheduleModel<CarShareScheduleModel> schedule)
        {
            if (this.UserAuthority.MANAGEMENT_FLG == '1')
            {
                return DialogResult.OK;
            }
            else
            {
                var list = this.GetScheduleItemList(
                    new CarShareScheduleSearchModel { CAR_GROUP = this.CarShareScheduleSearchCond.CAR_GROUP, ID = schedule.CategoryID });

                //項目編集権限がある場合は許可
                var edit = CheckManagementEdit(list.First().INPUT_SECTION_ID);
                if (edit)
                {
                    return DialogResult.OK;
                }

                if (type == ScheduleEditType.Insert || type == ScheduleEditType.Paste)
                {
                    using (var usageNotesForm = new UsageNotesForm())
                    {
                        var ret = usageNotesForm.ShowDialog(this);
                        if (ret != DialogResult.OK)
                        {
                            return ret;
                        }
                    }

                    if (list.First().FLAG_要予約許可 == 1)
                    {
                        using (var alertForm = new ReservationAlertForm())
                        {
                            return alertForm.ShowDialog(this);
                        }
                    }
                    else
                    {
                        return DialogResult.OK;
                    }
                }
                else
                {
                    if (list.First().FLAG_要予約許可 == 1 && schedule.IsEdit && schedule.Schedule.予約種別 == Const.Kariyoyaku)
                    {
                        using (var alertForm = new ReservationAlertForm())
                        {
                            return alertForm.ShowDialog(this);
                        }
                    }
                    else
                    {
                        return DialogResult.OK;
                    }
                }
            }
        }

        /// <summary>
        /// スケジュール日付範囲変更
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        private void RangeChangedSchedule(ScheduleModel<CarShareScheduleModel> schedule)
        {
            if (this.IsRestrictionSchedule(schedule) == false)
            {
                this.SetScheduleList();
                return;
            }

            //スケジュールのチェック
            //Update Start 2022/03/07 杉浦 スケジュール系不具合修正
            //if (this.IsEntrySchedule(ScheduleEditType.Update, schedule) == false)
            if (this.IsEntrySchedule(ScheduleEditType.Update, schedule, true) == false)
            //Update End 2022/03/07 杉浦 スケジュール系不具合修正
            {
                //スケジュール一覧設定
                this.SetScheduleList();
                return;

            }

            //編集モード
            schedule.ScheduleEdit = ScheduleEditType.Update;

            var list = new[] { schedule.Schedule };

            //行番号
            schedule.Schedule.PARALLEL_INDEX_GROUP = schedule.RowNo;

            //開始日
            schedule.Schedule.START_DATE = schedule.StartDate;

            //終了日
            schedule.Schedule.END_DATE = schedule.EndDate;

            //ユーザー情報設定
            schedule.Schedule.SetUserInfo();

            //レスポンスが取得できたかどうか
            var res = HttpUtil.PutResponse(ControllerType.CarShareSchedule, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //更新メッセージ
                this.MessageLabel.Text = string.Format(Resources.KKM01019, getYMDH(schedule.StartDate), getYMDH(schedule.EndDate), Resources.KKM00002);

                //スケジュール一覧設定
                this.SetScheduleList(false, false);

            }

        }

        /// <summary>
        /// スケジュール削除
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        /// <param name="isConfirm">確認可否</param>
        private void DeleteSchedule(ScheduleModel<CarShareScheduleModel> schedule, bool isConfirm)
        {
            //削除しない場合は終了
            if (isConfirm == true && Messenger.Confirm(Resources.KKM00007) != DialogResult.Yes)
            {
                return;

            }

            //スケジュールのチェック
            if (this.IsEntrySchedule(ScheduleEditType.Delete, schedule) == false)
            {
                //スケジュール一覧設定
                this.SetScheduleList();
                return;

            }

            //編集モード
            schedule.ScheduleEdit = ScheduleEditType.Delete;

            var list = new[] { schedule.Schedule };

            //レスポンスが取得できたかどうか
            var res = HttpUtil.DeleteResponse(ControllerType.CarShareSchedule, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //削除メッセージ
                this.MessageLabel.Text = string.Format(Resources.KKM01019, getYMDH(schedule.StartDate), getYMDH(schedule.EndDate), Resources.KKM00003);

                //スケジュール一覧設定
                this.SetScheduleList(false, false);

                this.gridUtil.UndoRedoManager.Do(schedule);
            }

        }

        /// <summary>
        /// スケジュール貼り付け
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        private void PasteSchedule(ScheduleModel<CarShareScheduleModel> schedule)
        {
            if (this.IsRestrictionSchedule(schedule) == false)
            {
                this.SetScheduleList();
                return;
            }

            //スケジュールのチェック
            //Update Start 2022/03/07 杉浦 スケジュール系不具合修正
            //if (this.IsEntrySchedule(ScheduleEditType.Paste, schedule) == false)
            if (this.IsEntrySchedule(ScheduleEditType.Paste, schedule, true) == false)
            //Update End 2022/03/07 杉浦 スケジュール系不具合修正
            {
                //スケジュール一覧設定
                this.SetScheduleList();
                return;

            }

            if (ShowNoteDialog(ScheduleEditType.Paste, schedule) == DialogResult.OK)
            {
                //編集モード（ペーストはスケジュール新規作成につきInsertへ）
                schedule.ScheduleEdit = ScheduleEditType.Insert;

                //カテゴリーIDから取得を行い、予約許可の更新をする。
                var list = this.GetScheduleItemList(
                   new CarShareScheduleSearchModel { CAR_GROUP = this.CarShareScheduleSearchCond.CAR_GROUP, ID = schedule.CategoryID });

                var model = new CarShareScheduleModel
                {
                    //車系
                    CAR_GROUP = this.CarShareScheduleSearchCond.CAR_GROUP,

                    //開発符号
                    GENERAL_CODE = schedule.GeneralCode,

                    //シンボル
                    SYMBOL = schedule.Schedule.SYMBOL,

                    //カテゴリーーID
                    CATEGORY_ID = schedule.CategoryID,

                    //行番号
                    PARALLEL_INDEX_GROUP = schedule.RowNo,

                    //開始日
                    START_DATE = schedule.StartDate,

                    //終了日
                    END_DATE = schedule.EndDate,

                    DESCRIPTION = schedule.Schedule.DESCRIPTION,

                    FLAG_空時間貸出可 = schedule.Schedule.FLAG_空時間貸出可,

                    FLAG_要予約許可 = list.First().FLAG_要予約許可,

                    TEL = schedule.Schedule.TEL,

                    目的 = schedule.Schedule.目的,

                    行先 = schedule.Schedule.行先
                };

                schedule.Schedule = model;

                ShowDetailForm(schedule);
            }

            //スケジュール一覧設定
            this.SetScheduleList(false, false);
        }
        #endregion

        #region スケジュールのチェック

        /// <summary>
        /// 期間を踏まえたスケジュール登録可不可チェック
        /// </summary>
        /// <remarks>
        /// 表示制限がかかっているユーザーの場合、
        /// 渡されたスケジュールの開始～終了期間が３ヶ月以内であるか確認を行います。
        /// </remarks>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private bool IsRestrictionSchedule(ScheduleModel<CarShareScheduleModel> schedule)
        {
            DateTime maxDate;
            DateTime minDate;

            var date = GetChangeDateTime();

            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            if (isManagement)
            {
                maxDate = this.CarShareCalendarGrid.MaxDate;
                minDate = this.CarShareCalendarGrid.MinDate;
            }
            else
            {
                maxDate = date[1];
                minDate = date[0];
            }

            DateTime startDate = schedule.StartDate.Value.Date;
            DateTime endDate = schedule.EndDate.Value.Date;

            //項目編集許可されているかどうか確認をする
            var list = this.GetScheduleItemList(
                    new CarShareScheduleSearchModel { CAR_GROUP = this.CarShareScheduleSearchCond.CAR_GROUP, ID = schedule.CategoryID });
            var edit = CheckManagementEdit(list.First().INPUT_SECTION_ID);

            if (edit == false && (startDate < minDate || startDate > maxDate || endDate < minDate || endDate > maxDate))
            {
                Messenger.Warn(string.Format(Resources.KKM01009, 2));
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 日付制限取得処理。
        /// </summary>
        /// <remarks>
        /// ３ヶ月制限用の日付を算出します。
        /// Minが配列の[0]、Maxが[1]へ設定されます。
        /// </remarks>
        /// <returns></returns>
        private DateTime[] GetChangeDateTime()
        {
            var array = new DateTime[2];

            DateTime toDay = DateTime.Today;
            DateTime start = new DateTime(toDay.Year, toDay.Month, 1);
            //Update Start 2021/10/15 杉浦 一般権限の予約可能期間を操作日から2ヶ月になるようにしてほしい
            ////Update Start 2021/09/27 杉浦　一般権限表示期間変更
            ////DateTime end = start.AddMonths(3).AddDays(-1);
            //DateTime end = start.AddMonths(2).AddDays(-1);
            ////Update End 2021/09/27 杉浦　一般権限表示期間変更
            DateTime end = toDay.AddMonths(2);
            //Update Start 2021/10/15 杉浦 一般権限の予約可能期間を操作日から2ヶ月になるようにしてほしい

            array[0] = start;
            array[1] = end;

            return array;
        }

        /// <summary>
        /// スケジュールのチェック
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        /// <returns>登録可否</returns>
        //Update Start 2022/03/07 杉浦 スケジュール系不具合修正
        //private bool IsEntrySchedule(ScheduleEditType type, ScheduleModel<CarShareScheduleModel> schedule, bool checkSameLine = true)
        private bool IsEntrySchedule(ScheduleEditType type, ScheduleModel<CarShareScheduleModel> schedule, bool plusCheck = false, bool checkSameLine = true)
        //Update End 2022/03/07 杉浦 スケジュール系不具合修正
        {
            //スケジュール項目が存在しているかどうか
            var itemList = this.GetScheduleItemList(new CarShareScheduleSearchModel { CAR_GROUP = this.CarShareScheduleSearchCond.CAR_GROUP, ID = schedule.CategoryID });
            if (itemList == null || itemList.Any() == false)
            {
                //存在していない場合はエラー
                Messenger.Warn(Resources.KKM00021);
                return false;

            }

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //更新
                //削除
                case ScheduleEditType.Update:
                case ScheduleEditType.Delete:
                    //スケジュールが存在しているかどうか
                    var scheduleList = this.GetScheduleList(new CarShareScheduleSearchModel { ID = schedule.ID });
                    if (scheduleList == null || scheduleList.Any() == false)
                    {
                        //存在していない場合はエラー
                        Messenger.Warn(Resources.KKM00021);
                        return false;

                    }
                    else
                    {
                        //スケジュール再設定
                        schedule.Schedule = scheduleList.First();

                    }
                    break;

            }

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //追加
                //更新
                //貼り付け
                case ScheduleEditType.Insert:
                case ScheduleEditType.Update:
                case ScheduleEditType.Paste:
                    var errList = new List<string>();

                    var start = schedule.StartDate;
                    var end = schedule.EndDate;

                    //最終予約日が設定されているかどうか
                    var item = this.GetScheduleItemList(new CarShareScheduleSearchModel { CAR_GROUP = this.CarShareScheduleSearchCond.CAR_GROUP, ID = schedule.CategoryID })?.FirstOrDefault();
                    if (item != null && item.最終予約可能日 != null)
                    {
                        var limit = item.最終予約可能日.Value.Date.AddDays(1);

                        //開始日か終了日が最終予約日を超過しているかどうか
                        if (start != null && start >= limit || end != null && end >= limit)
                        {
                            Messenger.Warn(Resources.KKM03010);
                            return false;
                        }

                    }

                    //検索条件
                    var cond = new CarShareScheduleSearchModel
                    {
                        //カテゴリーID
                        CATEGORY_ID = schedule.CategoryID,

                        //期間(From)
                        START_DATE = start?.Date,

                        //期間(To)
                        END_DATE = end?.Date

                    };

                    var list = this.GetScheduleList(cond);

                    // スケジュール詳細のほうの条件を直すのも忘れず。
                    //同一行でスケジュールで重複した日付の期間が存在する場合はエラー
                    if (list.Where(x => x.ID != schedule.ID && x.PARALLEL_INDEX_GROUP == schedule.RowNo).Any(x =>
                            (x.START_DATE.Value.Date <= start.Value.Date && start.Value.Date <= x.END_DATE.Value.Date) ||
                            (x.START_DATE.Value.Date <= end.Value.Date && end.Value.Date <= x.END_DATE.Value.Date) ||
                            (start.Value.Date <= x.START_DATE.Value.Date && x.START_DATE.Value.Date <= end.Value.Date) ||
                            (start.Value.Date <= x.END_DATE.Value.Date && x.END_DATE.Value.Date <= end.Value.Date)) == true && checkSameLine)
                    {
                        errList.Add(Resources.KKM03017);
                    }
                    else if (list.Where(x => x.ID != schedule.ID).Any(x =>
                         (x.START_DATE <= start && start < x.END_DATE) || (x.START_DATE < end && end <= x.END_DATE) ||
                         (start <= x.START_DATE && x.START_DATE < end) || (start < x.END_DATE && x.END_DATE <= end)) == true)
                    {
                        //スケジュールで重複した期間が存在する場合はエラー
                        errList.Add(Resources.KKM03005);
                    }

                    //Append Start 2022/02/03 杉浦 試験車日程の車も登録する

                    //Delete Start 2022/03/16 杉浦 チェック削除
                    ////Append Start 2022/03/07 杉浦 スケジュール系不具合修正
                    //var itemList2 = itemList.ToList();
                    //if (plusCheck && !string.IsNullOrEmpty(itemList2[0].管理票番号))
                    //{
                    //    //Append End 2022/03/07 杉浦 スケジュール系不具合修正
                    //    //検索条件
                    //    var cond2 = new AllScheduleSearchModel
                    //    {
                    //        //管理票番号
                    //        管理票番号 = itemList2[0].管理票番号,

                    //        //期間(From)
                    //        START_DATE = start?.Date,

                    //        //期間(To)
                    //        END_DATE = end?.Date

                    //    };
                    //    //スケジュールで重複した期間が存在する場合はエラー
                    //    var allList = this.GetAllScheduleList(cond2).Where(x => x.ID != schedule.ID).ToList();
                    //    if (allList.Any() == true)
                    //    {
                    //        var scheduleType = allList.Where(x => (x.START_DATE.Value <= start && start <= x.END_DATE.Value) ||
                    //            (x.START_DATE.Value <= end && end <= x.END_DATE.Value) ||
                    //            (start <= x.START_DATE.Value && x.START_DATE.Value <= end) ||
                    //            (start <= x.END_DATE.Value && x.END_DATE.Value <= end)).ToList();

                    //        string text = string.Empty;
                    //        if (scheduleType != null && scheduleType.Count != 0)
                    //        {
                    //            var scheduleStringList = (scheduleType.Select(x => x.SCHEDULE_TYPE)).Distinct().OrderBy(x => x);

                    //            foreach (var d in scheduleStringList)
                    //            {
                    //                var d2 = d == "1" ? "試験車日程" : d == "2" ? "外製車日程" : "カーシェア日程";
                    //                text += d2 + " ";
                    //            }
                    //            //Update Start 2022/03/08 杉浦 不具合修正
                    //            //errList.Add("同時間に" + text + "に既に登録がある為、登録出来ません。");
                    //            errList.Add(text + "の同時刻に既に登録がある為、登録出来ません。");
                    //            //Update End 2022/03/08 杉浦 不具合修正
                    //        }
                    //    }
                    //    //Append End 2022/02/03 杉浦 試験車日程の車も登録する
                    //}
                    //Delete End 2022/03/16 杉浦 チェック削除

                    //エラーがある場合は終了
                    if (errList.Any() == true)
                    {
                        Messenger.Warn(string.Join(Const.CrLf, errList.ToArray()));
                        return false;

                    }
                    if (CheckManagementEdit(item.INPUT_SECTION_ID) == false && schedule.IsEdit && (end.Value.Date - start.Value.Date).Days >= 5)
                    {
                        Messenger.Info(Resources.KKM01001);
                        return false;
                    }

                    break;

            }

            return true;

        }

        /// <summary>
        /// 項目作成権限確認
        /// </summary>
        /// <param name="inputSectionId"></param>
        /// <returns></returns>
        private bool CheckManagementEdit(string inputSectionId)
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isJibuManagement = this.UserAuthority.JIBU_MANAGEMENT_FLG == '1';
            var managementEdit = false;
            if (string.IsNullOrWhiteSpace(inputSectionId))//ＳＪＳＢ項目
            {
                if (!isManagement)
                {
                    //管理権限なし
                    managementEdit = false;
                }
                else
                {
                    //管理権限あり
                    managementEdit = true;
                }
            }
            else
            {
                if (!isManagement && !isJibuManagement)
                {
                    //管理権限なし
                    managementEdit = false;
                }
                else
                {
                    //管理権限あり
                    managementEdit = true;
                }
            }

            return managementEdit;
        }
        #endregion

        #region データの取得
        /// <summary>
        /// お気に入りの取得
        /// </summary>
        private IEnumerable<FavoriteSearchOutModel> GetFavoriteList()
        {
            //パラメータ設定
            var cond = new FavoriteSearchInModel
            {
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                // データ区分
                CLASS_DATA = Const.FavoriteCarShare

            };

            //APIで取得
            var res = HttpUtil.GetResponse<FavoriteSearchInModel, FavoriteSearchOutModel>(ControllerType.Favorite, cond);

            //レスポンスが取得できたかどうか
            var list = new List<FavoriteSearchOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// お気に入りの取得
        /// </summary>
        /// <param name="id">お気に入りID</param>
        private CarShareFavoriteItemModel GetFavorite(long id)
        {
            //パラメータ設定
            var cond = new CarShareFavoriteSearchInModel { ID = id };

            //APIで取得
            var res = HttpUtil.GetResponse<CarShareFavoriteSearchInModel, CarShareFavoriteItemModel>(ControllerType.CarShareFavorite, cond);

            //レスポンスが取得できたかどうか
            var list = new List<CarShareFavoriteItemModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list.FirstOrDefault();

        }

        /// <summary>
        /// スケジュール項目取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<CarShareScheduleItemModel> GetScheduleItemList()
        {
            bool? openFlg = null;

            //Openのみ選択かどうか
            if (this.StatusOpenCheckBox.Checked == true && this.StatusCloseCheckBox.Checked == false)
            {
                openFlg = true;

            }
            //Closeのみ選択かどうか
            else if (this.StatusOpenCheckBox.Checked == false && this.StatusCloseCheckBox.Checked == true)
            {
                openFlg = false;

            }

            if (this.CalendarCategoryId != null)
            {
                var data = this.GetScheduleItemList(new CarShareScheduleSearchModel
                {
                    ID = this.CalendarCategoryId
                });

                if (data.Where(x => x.CATEGORY_ID == this.CalendarCategoryId).ToList()[0].CLOSED_DATE != null)
                {
                    openFlg = false;
                }
                else
                {
                    openFlg = true;
                }
                if (openFlg != null)
                {
                    this.StatusOpenCheckBox.Checked = openFlg.Value;
                    this.StatusCloseCheckBox.Checked = !openFlg.Value;
                }
            }

            var cond = new CarShareScheduleSearchModel
            {
                //空車期間(From)
                BLANK_START_DATE = this.GetDateTime(this.BlankCarFromDateTimePicker, this.BlankCarFromComboBox),

                //空車期間(To)
                BLANK_END_DATE = this.GetDateTime(this.BlankCarToDateTimePicker, this.BlankCarToComboBox),

                //Openフラグ
                OPEN_FLG = openFlg,

                //車系
                CAR_GROUP = this.CarGroupComboBox.Text,

                INPUT_PERSONEL_ID = SessionDto.UserId

            };

            //カーシェアスケジュール検索条件
            this.CarShareScheduleSearchCond = cond;

            //返却
            return this.GetScheduleItemList(cond);

        }

        /// <summary>
        /// スケジュール項目取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private IEnumerable<CarShareScheduleItemModel> GetScheduleItemList(CarShareScheduleSearchModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<CarShareScheduleSearchModel, CarShareScheduleItemModel>(ControllerType.CarShareScheduleItem, cond);

            //レスポンスが取得できたかどうか
            var list = new List<CarShareScheduleItemModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            //返却
            return list;

        }

        /// <summary>
        /// スケジュール取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<CarShareScheduleModel> GetScheduleList()
        {
            //試験車スケジュール検索条件
            var cond = this.CarShareScheduleSearchCond;

            //期間(From)
            cond.START_DATE = this.CarShareCalendarGrid.FirstDateInView;

            //期間(To)
            cond.END_DATE = this.CarShareCalendarGrid.LastDateInView;

            //返却
            return this.GetScheduleList(cond);

        }

        /// <summary>
        /// スケジュール取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private IEnumerable<CarShareScheduleModel> GetScheduleList(CarShareScheduleSearchModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<CarShareScheduleSearchModel, CarShareScheduleModel>(ControllerType.CarShareSchedule, cond);

            //レスポンスが取得できたかどうか
            var list = new List<CarShareScheduleModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            //返却
            return list;

        }

        //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
        /// <summary>
        /// 全スケジュールの取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<AllScheduleModel> GetAllScheduleList(AllScheduleSearchModel cond)
        {
            var list = new List<AllScheduleModel>();

            //APIで取得
            var res = HttpUtil.GetResponse<AllScheduleSearchModel, AllScheduleModel>(ControllerType.AllSchedule, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        //Append End 2022/02/03 杉浦 試験車日程の車も登録する
        #endregion

        #region 日時の取得
        /// <summary>
        /// 日時の取得
        /// </summary>
        /// <param name="dtp">日付</param>
        /// <param name="cmb">時刻</param>
        /// <returns>日時</returns>
        private DateTime? GetDateTime(NullableDateTimePicker dtp, ComboBox cmb)
        {
            //日時が入力されているかどうか
            if (dtp.SelectedDate == null || string.IsNullOrWhiteSpace(cmb.Text) == true)
            {
                return null;

            }

            return dtp.SelectedDate.Value.AddHours(int.Parse(cmb.Text));

        }
        #endregion

        /// <summary>
        /// 凡例フォーム表示。
        /// </summary>
        /// <remarks>
        /// 凡例フォームを表示します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegendButton_Click(object sender, EventArgs e)
        {
            var openform = Application.OpenForms[typeof(CarChareLegendForm).Name];

            if (openform == null)
            {
                var frm = new CarChareLegendForm();

                var bs = this.LegendButton.PointToScreen(new Point(0, 0));
                var cp = new Point(bs.X - frm.Width, bs.Y);
                frm.Location = cp;
                frm.StartPosition = FormStartPosition.Manual;
                frm.Show(this);
            }
            else
            {
                openform.Close();
            }
        }

        /// <summary>
        /// 群馬ファイルリンク。
        /// </summary>
        /// <remarks>
        /// AppConfigからSKCの駐車場PDFのURLを取得し、開きます。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GunmaLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filePath = new AppConfigAccessor().GetAppSetting("gunmaPdfFileUrl");
            try
            {
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                Messenger.Error(string.Format(Resources.KKM00029, filePath), null);
            }
        }

        /// <summary>
        /// SKCファイルリンク。
        /// </summary>
        /// <remarks>
        /// AppConfigからSKCの駐車場PDFのURLを取得し、開きます。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkcLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filePath = new AppConfigAccessor().GetAppSetting("skcPdfFileUrl");
            try
            {
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                Messenger.Error(string.Format(Resources.KKM00029, filePath), null);
            }
        }

        /// <summary>
        /// Excel出力ボタン押下処理。
        /// </summary>
        /// <remarks>
        /// Excel出力ウィンドウを表示します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelPrintButton_Click(object sender, EventArgs e)
        {
            //スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;
            }

            using (var form = new CalendarPrintExcelForm(this.ScheduleItemList.ToList(), this.CarShareScheduleSearchCond, this.gridUtil.CalendarSetting.CalendarMode))
            {
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// Xeyeページ遷移
        /// </summary>
        /// <param name="item">スケジュール項目</param>
        private void RunXeyeWebBrowser(ScheduleItemModel<CarShareScheduleItemModel> item)
        {
            var list = new[] { item.ScheduleItem };

            var list2 = list.Select(x => x.管理票番号).ToList();

            //管理票Noを取得する
            ScheduleToXeyeSearchModel searchModel = new ScheduleToXeyeSearchModel();
            searchModel.物品名2 = list2[0];

            // XeyeのIDを取得する
            var res = HttpUtil.GetResponse<ScheduleToXeyeSearchModel, ScheduleToXeyeOutModel>(ControllerType.Xeye, searchModel);
            var xeyeId = new List<ScheduleToXeyeOutModel>();
            if (res.Results.Count() != 0) xeyeId.AddRange(res.Results);


            var frm = new WebBrowserForm(xeyeId[0].備考);
            frm.Show();
        }
        
        /// <summary>
        /// 車両管理担当取得
        /// </summary>
        /// <returns></returns>
        private string GetCarManager()
        {
            var res = HttpUtil.GetResponse<CarManagerSearchModel, CarManagerModel>(ControllerType.CarManager,
                new CarManagerSearchModel { GENERAL_CODE = this.CarShareScheduleSearchCond.CAR_GROUP, FUNCTION_ID = (int)FunctionID.CarShare });

            carManagerModelList = new List<CarManagerModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                carManagerModelList.AddRange(res.Results);
            }

            string managerName = string.Empty;
            var managerList = carManagerModelList.Where(x => x.CATEGORY_ID == null).OrderBy(x => x.STATUS).ToList();

            foreach (var item in managerList)
            {
                managerName += string.Format("(" + item.STATUS + "){0} {1}\n ({2})\n", item.SECTION_CODE, item.NAME, item.TEL);
            }

            managerName += managerList.FirstOrDefault()?.REMARKS;

            return managerName;
        }

        /// <summary>
        /// 連絡先編集フォーム表示
        /// </summary>
        private void ShowScheduleItemContactInfoForm()
        {
            using (var form = new ScheduleItemContactInfoForm<CarShareScheduleItemModel>() { InfoType = ContactInfoType.All, Code = this.CarShareScheduleSearchCond.CAR_GROUP, FunctionId = FunctionID.CarShare })
            {
                var ret = form.ShowDialog(this);

                if (ret == DialogResult.OK)
                {
                    this.gridUtil.CornerHeaderText = this.GetCarManager();
                }
            }
        }

        /// <summary>
        /// 連絡先編集フォーム表示
        /// </summary>
        private void ShowScheduleItemContactInfoForm(ScheduleItemModel<CarShareScheduleItemModel> selectScheduleItem, ScheduleItemEditType type)
        {
            if (type == ScheduleItemEditType.Insert)
            {
                var scheduleItem = new CarShareScheduleItemModel
                {
                    GENERAL_CODE = this.CarShareScheduleSearchCond.CAR_GROUP,
                    CAR_GROUP = this.CarShareScheduleSearchCond.CAR_GROUP,
                    SORT_NO = 0,
                    PARALLEL_INDEX_GROUP = 1
                };

                if (selectScheduleItem.ScheduleItem != null)
                {
                    scheduleItem.SORT_NO = selectScheduleItem.SortNo + 0.1D;
                    scheduleItem.SetUserInfo();
                }
                selectScheduleItem.ScheduleItem = scheduleItem;
            }
            selectScheduleItem.ScheduleItemEdit = type;

            using (var form = new ScheduleItemContactInfoForm<CarShareScheduleItemModel>()
            {
                InfoType = ContactInfoType.Item,
                Code = this.CarShareScheduleSearchCond.CAR_GROUP,
                Item = selectScheduleItem,
                FunctionId = FunctionID.CarShare
            })
            {
                var ret = form.ShowDialog(this);

                if (ret == DialogResult.OK)
                {
                    SearchSchedule(true);
                }
            }
        }

        /// <summary>
        /// 元に戻す・やり直し処理
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private bool UndoRedo(ScheduleModel<CarShareScheduleModel> schedule)
        {
            if (schedule.ScheduleEdit == ScheduleEditType.Update)
            {
                var res = HttpUtil.PutResponse(ControllerType.CarShareSchedule, new[] { schedule.Schedule });
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    this.SetScheduleList(false, false);
                    return true;
                }
                return false;
            }
            else if (schedule.ScheduleEdit == ScheduleEditType.Delete)
            {
                //削除の時はinsert、insertの時はdelete。
                var res = HttpUtil.PostResponse(ControllerType.CarShareSchedule, new[] { schedule.Schedule });
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    schedule.Schedule = res.Results.OfType<CarShareScheduleModel>().FirstOrDefault();
                    this.SetScheduleList(false, false);
                    return true;
                }
                return false;
            }
            else if (schedule.ScheduleEdit == ScheduleEditType.Insert || schedule.ScheduleEdit == ScheduleEditType.Paste)
            {
                var list = new[] { schedule.Schedule };

                var res = HttpUtil.DeleteResponse(ControllerType.CarShareSchedule, list);
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    this.SetScheduleList(false, false);
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
