using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UC;
using DevPlan.Presentation.UIDevPlan.TruckSchedule;
using DevPlan.UICommon;
using DevPlan.UICommon.Config;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Util;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Utils.Calendar;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.OuterCar
{
    /// <summary>
    /// 外製車日程
    /// </summary>
    public partial class OuterCarForm : BaseForm
    {
        #region メンバ変数
        private const string StringDateFormat = "yyyy/MM";

        private const int CondHeight = 70;

        private const string FavoriteClassData = Const.FavoriteOuterCar;

        private const string SyaryouYoyakuKigen = "本車両の使用期限を過ぎています";
        private const int SyaryouYoyakuKigenSpan = 4;

        CalendarGridUtil<OuterCarScheduleItemGetOutModel, OuterCarScheduleGetOutModel> gridUtil;

        private Func<DateTime?, string, DateTime?> getDateTime = (dt, time) => dt == null ? null : (DateTime?)dt.Value.AddHours(int.Parse(time == string.Empty ? "0" : time));

        private Func<DateTime?, string> getYMDH = dt => { return dt != null ? ((DateTime)dt).ToString("yyyy/MM/dd H時") : string.Empty; };
        private List<CarManagerModel> carManagerModelList;
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "外製車日程"; } }

        /// <summary>お気に入りID</summary>
        public long? FavoriteID { get; set; }

        /// <summary>スケジュール項目リスト</summary>
        private IEnumerable<OuterCarScheduleItemGetOutModel> ScheduleItemList { get; set; }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>外製車スケジュール検索条件</summary>
        public OuterCarScheduleItemGetInModel OuterCarScheduleSearchCond { get; set; }

        private DateTime? _calendarFirstDate = null;

        private TruckScheduleForm TruckForm;

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
        public OuterCarForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 画面のセット

        #region 画面ロード
        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuterCarForm_Load(object sender, EventArgs e)
        {
            // 権限の取得
            this.UserAuthority = this.GetFunction(FunctionID.OuterCar);

            // カレンダーグリッドの初期化
            this.InitCalendarGrid();

            // 画面の初期化
            this.InitForm();

            // お気に入りから遷移時
            if (this.FavoriteID != null)
            {
                FormControlUtil.FormWait(this, () =>
                {
                    // お気に入り検索条件の設定
                    if (!this.SetFavoriteCondition()) return;

                    // スケジュール設定（項目含む）
                    this.SetScheduleAll(isMonthFirst: true);

                });
            }
            //検索条件があるかどうか
            else if (this.OuterCarScheduleSearchCond != null)
            {
                this.SetScheduleAll(isMonthFirst: true);
            }

            if (this.CalendarCategoryId != null)
            {
                this.gridUtil.SetScheduleRowHeaderFirst(this.CalendarCategoryId);
                this.gridUtil.SetScheduleMostDayFirst(this.CalendarFirstDate);

                this.CalendarCategoryId = null;
            }

            if (this.OuterCarScheduleSearchCond == null && this.FavoriteID == null)
            {
                //車系ドロップダウン活性化
                this.CarGroupComboBox.DroppedDown = true;
            }

            if (this.IsFunctionEnable(FunctionID.Truck) == false)
            {
                this.TruckScheduleButton.Visible = false;
                this.TruckScheduleCheckBox.Visible = false;
            }
        }
        #endregion

        /// <summary>
        /// マウスホイール処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.TruckForm == null)
            {
                base.OnMouseWheel(e);
            }
        }

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // 空車期間
            this.EmptyStartDayDateTimePicker.Value = null;
            this.EmptyStartTimeComboBox.SelectedIndex = 0;
            this.EmptyEndDayDateTimePicker.Value = null;
            this.EmptyEndTimeComboBox.SelectedIndex = this.EmptyEndTimeComboBox.Items.Count - 1;

            // 車系
            FormControlUtil.SetComboBoxItem(this.CarGroupComboBox, GetCarGroupList());
            if (this.OuterCarScheduleSearchCond != null) this.CarGroupComboBox.SelectedValue = this.OuterCarScheduleSearchCond.車系;
            else this.CarGroupComboBox.SelectedIndex = 0;
            this.CarGroupComboBox.SelectedValueChanged += CarGroupComboBox_SelectedValueChanged;

            // ダウンロード
            this.ExcelPrintButton.Visible = isExport;

        }
        #endregion

        #region カレンダーグリッドの初期化
        /// <summary>
        /// カレンダーグリッドの初期化
        /// </summary>
        private void InitCalendarGrid()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            
            var mainMap = new Dictionary<string, Action<ScheduleItemModel<OuterCarScheduleItemGetOutModel>>>();
            var contactMap = new Dictionary<string, Action<ScheduleItemModel<OuterCarScheduleItemGetOutModel>>>();

            //管理権限があるかどうか
            if (isManagement == true)
            {
                mainMap["項目追加"] = item => this.OpenScheduleItemForm(item, ScheduleItemEditType.Insert);
                mainMap["項目編集"] = item => this.OpenScheduleItemForm(item, ScheduleItemEditType.Update);
                mainMap["項目削除"] = this.ScheduleItemDelete;
                mainMap["項目移動"] = (item => this.ShowScheduleItemMoveForm(item));
                mainMap["管理者追加"] = (item => this.ShowScheduleItemContactInfoForm(item, ScheduleItemEditType.Insert));

                contactMap["項目追加"] = item => this.OpenScheduleItemForm(item, ScheduleItemEditType.Insert);
                contactMap["項目削除"] = this.ScheduleItemDelete;
                contactMap["管理者追加"] = (item => this.ShowScheduleItemContactInfoForm(item, ScheduleItemEditType.Insert));
                contactMap["管理者編集"] = (item => this.ShowScheduleItemContactInfoForm(item, ScheduleItemEditType.Update));
            }

            var config = new CalendarGridConfigModel<OuterCarScheduleItemGetOutModel, OuterCarScheduleGetOutModel>(this.OuterCarCalendarGrid, isManagement, isUpdate,
            new Dictionary<string, Action>
            {
                { "項目追加", () => this.OpenScheduleItemForm(new ScheduleItemModel<OuterCarScheduleItemGetOutModel>(), ScheduleItemEditType.Insert) },
                { "管理者編集", () => this.ShowScheduleItemContactInfoForm() }
            },
            mainMap,
            new Dictionary<string, Action<ScheduleModel<OuterCarScheduleGetOutModel>>>
            {
                { "削除", schedule => this.ScheduleDelete(schedule, true) }
            });

            //スケジュール行ヘッダーのコンテキストメニューの項目を取得するデリゲート
            config.GetRowHeaderContexMenuItems = (item) =>
            {
                var i = item.ScheduleItem;
                var list = new List<ToolStripItem>();

                var map = carManagerModelList.Any(x => x.CATEGORY_ID == item.ID.ToString()) ? contactMap : mainMap;

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

            //スケジュールの背景色取得
            config.GetScheduleBackColor = (schedule, colorEnum) =>
            {
                var s = schedule.Schedule;
                var isHonyoyaku = s.予約種別 == "本予約";
                
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

            //スケジュールのツールチップテキストのタイトルを取得するデリゲート
            config.GetScheduleToolTipTitle = schedule => schedule.Schedule.DESCRIPTION;

            //スケジュールの追加するツールチップテキストを取得するデリゲート
            config.GetScheduleAddToolTipText = schedule => string.Format("{0} {1}({2})", schedule.Schedule.予約者_SECTION_CODE, schedule.Schedule.予約者_NAME, schedule.Schedule.TEL);
            
            //その他スケジュール
            config.OtherSchedule = new OtherScheduleModel<OuterCarScheduleItemGetOutModel>(CalendarScheduleColorEnum.NoneReservation.MainColor, CalendarScheduleColorEnum.ReservationOver.MainColor, SyaryouYoyakuKigenSpan, SyaryouYoyakuKigen, (item => item.ScheduleItem.最終予約可能日 == null ? null : (DateTime?)item.ScheduleItem.最終予約可能日.Value.AddDays(1).Date));

            //スケジュール項目の背景色を取得するデリゲート
            config.GetScheduleItemBackColor = x => SetScheduleItemBackColor(x.ScheduleItem);

            //試験車管理SYS情報フィルタ利用可否
            config.IsRowHeaderSysFilter = isManagement;

            //カレンダーの設定情報
            config.CalendarSettings = new CalendarSettings(Properties.Settings.Default.OuterCarCalendarStyle);

            //カレンダーグリッドの初期設定
            this.gridUtil = new CalendarGridUtil<OuterCarScheduleItemGetOutModel, OuterCarScheduleGetOutModel>(config);

            //スケジュール表示期間の変更後
            this.gridUtil.ScheduleViewPeriodChangedAfter += (start, end) => FormControlUtil.FormWait(this, () => this.SetSchedule(start, end));

            //スケジュール項目のダブルクリック
            this.gridUtil.ScheduleRowHeaderDoubleClick += this.OpenProgressListForm;

            //スケジュール項目の並び順変更後
            this.gridUtil.ScheduleItemSortChangedAfter += (sourceItem, destItem) => FormControlUtil.FormWait(this, () => this.ChangeScheduleItemSort(sourceItem, destItem));

            //スケジュールのダブルクリック
            this.gridUtil.ScheduleDoubleClick += schedule => this.OpenScheduleForm(schedule, ScheduleEditType.Update);

            //スケジュールの日付範囲変更後
            this.gridUtil.ScheduleDayRangeChangedAfter += schedule => FormControlUtil.FormWait(this, () => this.SchedulePut(schedule));

            //スケジュールの空白のドラッグ後
            this.gridUtil.ScheduleEmptyDragAfter += schedule => this.OpenScheduleForm(schedule, ScheduleEditType.Insert);

            //スケジュールの空白ダブルクリック
            this.gridUtil.ScheduleEmptyDoubleClick += schedule => this.OpenScheduleForm(schedule, ScheduleEditType.Insert);

            //スケジュールの削除（deleteキー）
            this.gridUtil.ScheduleDelete += schedule => FormControlUtil.FormWait(this, () => this.ScheduleDelete(schedule, false));

            //スケジュールの貼り付け（Ctrl C→V）
            this.gridUtil.SchedulePaste += (copySchedule, schedule) => FormControlUtil.FormWait(this, () => this.SchedulePost(schedule));

            //カレンダーレイアウト状態保存デリゲート
            this.gridUtil.CalendarSetting.SaveCalendarUserData += this.SaveCalendarUserData;

            //カレンダーの表示期間変更
            var date = CalendarFirstDate;
            this.gridUtil.SetCalendarViewPeriod(new DateTime(date.Year, date.Month, 1));

            this.OuterCarCalendarGrid.HorizontalScrollBarOffsetChanged += OuterCarCalendarGrid_HorizontalScrollBarOffsetChanged;
            this.TruckScheduleCheckBox.CheckedChanged += TruckScheduleCheckBox_CheckedChanged;
            this.gridUtil.UndoRedo += (schedule) => this.UndoRedo(schedule);
        }

        /// <summary>
        /// トラック予約同期チェックボックス変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckScheduleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.TruckForm == null)
            {
                return;
            }

            this.TruckForm.SyncCheck = this.TruckScheduleCheckBox.Checked;

            if (this.TruckScheduleCheckBox.Checked)
            {
                this.TruckForm.SyncCalendarStyle = Properties.Settings.Default.OuterCarCalendarStyle;
                this.TruckForm.RefreshCalendarTemplate();
            }
        }

        /// <summary>
        /// カレンダーグリッド水平スクロールバー変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuterCarCalendarGrid_HorizontalScrollBarOffsetChanged(object sender, EventArgs e)
        {
            if (IsTruckSync())
            {
                if (this.TruckForm.TruckScheduleCalendarGrid.HorizontalScrollBarOffset != this.OuterCarCalendarGrid.HorizontalScrollBarOffset)
                {
                    this.TruckForm.TruckCalendarGridEventDelete();
                    this.TruckForm.TruckScheduleCalendarGrid.HorizontalScrollBarOffset = this.OuterCarCalendarGrid.HorizontalScrollBarOffset;
                    this.TruckForm.TruckCalendarGridEventAdd();
                }
            }
        }

        /// <summary>
        /// スケジュール項目背景色算出処理。
        /// </summary>
        /// <param name="scheduleItem"></param>
        /// <returns></returns>
        private CalendarScheduleColorEnum SetScheduleItemBackColor(OuterCarScheduleItemGetOutModel scheduleItem)
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
            if (IsTruckSync())
            {
                this.TruckForm.SyncCalendarStyle = style;
                this.TruckForm.RefreshCalendarTemplate();
            }

            if (this.TruckForm == null)
            {
                Properties.Settings.Default.OuterCarCalendarStyle = style;
                Properties.Settings.Default.Save();
            }
        }
        #endregion

        #region お気に入り検索条件のセット
        /// <summary>
        /// お気に入り検索条件のセット
        /// </summary>
        private bool SetFavoriteCondition()
        {
            var cond = this.GetFavoriteData();

            if (cond == null) return false;

            // 車系
            this.CarGroupComboBox.SelectedValue = cond.CAR_GROUP;

            // OPEN/CLOSE
            this.StatusOpenCheckBox.Checked = cond.STATUS_OPEN_FLG == "1";
            this.StatusCloseCheckBox.Checked = cond.STATUS_CLOSE_FLG == "1";


            return true;
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

            var start = this.GetDateTime(this.EmptyStartDayDateTimePicker, this.EmptyStartTimeComboBox);
            var end = this.GetDateTime(this.EmptyEndDayDateTimePicker, this.EmptyEndTimeComboBox);

            //空車期間のチェック
            map[this.EmptyStartDayDateTimePicker] = (c, name) =>
            {
                //空車期間Toのみ入力かどうか
                if (this.EmptyStartDayDateTimePicker.SelectedDate == null && this.EmptyEndDayDateTimePicker.SelectedDate != null)
                {
                    //背景色を変更
                    this.EmptyStartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.EmptyEndDayDateTimePicker.BackColor = Const.ErrorBackColor;

                    // エラーメッセージ
                    return Resources.KKM03015;
                }

                //空車期間Fromのみ入力かどうか
                if (this.EmptyStartDayDateTimePicker.SelectedDate != null && this.EmptyEndDayDateTimePicker.SelectedDate == null)
                {
                    //背景色を変更
                    this.EmptyStartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.EmptyEndDayDateTimePicker.BackColor = Const.ErrorBackColor;

                    // エラーメッセージ
                    return Resources.KKM03015;
                }

                //期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー
                if (this.EmptyStartDayDateTimePicker.SelectedDate != null && this.EmptyEndDayDateTimePicker.SelectedDate != null && start > end)
                {
                    //背景色を変更
                    this.EmptyStartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    FormControlUtil.SetComboBoxBackColor(this.EmptyStartTimeComboBox, Const.ErrorBackColor);
                    this.EmptyEndDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    FormControlUtil.SetComboBoxBackColor(this.EmptyEndTimeComboBox, Const.ErrorBackColor);

                    // エラーメッセージ
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

        #region 検索条件のクリア
        /// <summary>
        /// 検索条件のクリア
        /// </summary>
        private void ClearCondition()
        {
            this.MessageLabel.Text = Resources.KKM00047;

            //車系
            this.CarGroupComboBox.SelectedIndex = 0;
            FormControlUtil.SetComboBoxBackColor(this.CarGroupComboBox, Const.DefaultBackColor);

            // 空車期間(From)
            this.EmptyStartDayDateTimePicker.Value = null;
            this.EmptyStartDayDateTimePicker.BackColor = Const.DefaultBackColor;
            this.EmptyStartTimeComboBox.SelectedIndex = 0;
            FormControlUtil.SetComboBoxBackColor(this.EmptyStartTimeComboBox, Const.DefaultBackColor);

            // 空車期間(To)
            this.EmptyEndDayDateTimePicker.Value = null;
            this.EmptyEndDayDateTimePicker.BackColor = Const.DefaultBackColor;
            this.EmptyEndTimeComboBox.SelectedIndex = this.EmptyEndTimeComboBox.Items.Count - 1;
            FormControlUtil.SetComboBoxBackColor(this.EmptyEndTimeComboBox, Const.DefaultBackColor);

            // 項目ステータス
            this.StatusOpenCheckBox.Checked = true;
            this.StatusCloseCheckBox.Checked = false;
        }
        #endregion

        #region スケジュール設定
        /// <summary>
        /// スケジュール設定（期間指定）
        /// </summary>
        /// <param name="start">開始日</param>
        /// <param name="end">終了日</param>
        /// <remarks>カレンダーグリッドから日付の範囲を取得するので引数は未使用</remarks>
        private void SetSchedule(DateTime start, DateTime end)
        {
            //入力がOKかどうか
            if (!this.IsSearchSchedule()) return;

            // スケジュール設定
            this.SetSchedule();

            //空車期間が設定されているかどうか
            var date = this.EmptyStartDayDateTimePicker.SelectedDate;
            if (date != null)
            {
                //スケジュールの先頭を設定
                this.gridUtil.SetScheduleMostDayFirst(start <= date && date <= end ? date.Value : start);
            }

            if (this.TruckForm != null)
            {
                this.TruckForm.RefreshCalendarTemplate();
            }
        }

        /// <summary>
        /// スケジュール設定
        /// </summary>
        private void SetSchedule(bool isNewSearch = true)
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            Func<OuterCarScheduleGetOutModel, string> getSubTitle =
                schedule => string.Format("{0}{1}", (schedule.予約種別 == Const.Kariyoyaku ? Const.Kari : ""), schedule.DESCRIPTION);
            Func<OuterCarScheduleGetOutModel, bool> isEdit = schedule =>
                isManagement == true ? isUpdate :
                (schedule.予約種別 == ReservationStautsEnum.HON_YOYAKU.Key && schedule.FLAG_要予約許可 == 1) ? false :
                (schedule.END_DATE.Value.Date < DateTime.Now.Date) ? false :
                isUpdate == true && schedule.予約者_ID == SessionDto.UserId;

            // 検索条件のチェック
            if (!this.IsSearchSchedule()) return;

            //検索結果文言
            if (isNewSearch)
            {
                this.MessageLabel.Text = this.ScheduleItemList == null || this.ScheduleItemList.Any() == false ? Resources.KKM00005 : Resources.KKM00047;
            }

            //カレンダーのコーナーヘッダー
            this.gridUtil.CornerHeaderText = this.GetCarManager();

            //スケジュール取得
            var scheduleList = this.GetScheduleList();

            // カレンダーにデータバインド
            this.gridUtil.Bind(this.ScheduleItemList, scheduleList, x => x.CATEGORY_ID, y => y.CATEGORY_ID,
                x => new ScheduleItemModel<OuterCarScheduleItemGetOutModel>(x.CATEGORY_ID, x.GENERAL_CODE, x.CATEGORY, (x.FLAG_要予約許可 == 1) ? "予約許可必要" : "予約許可不要", x.PARALLEL_INDEX_GROUP, x.SORT_NO.Value, x.管理票NO, x),
                y => new ScheduleModel<OuterCarScheduleGetOutModel>(y.SCHEDULE_ID, y.CATEGORY_ID, y.PARALLEL_INDEX_GROUP, getSubTitle(y), y.START_DATE, y.END_DATE, y.INPUT_DATETIME, y.SYMBOL, false, isEdit(y), y, y.予約種別));

        }

        /// <summary>
        /// スケジュール設定（項目含む）
        /// </summary>
        private void SetScheduleAll()
        {
            this.SetScheduleAll(false, true);
        }
        /// <summary>
        /// スケジュール設定（項目含む）
        /// </summary>
        /// <param name="isMonthFirst">基準月先頭可否</param>
        /// <param name="isNewSearch">新規検索可否</param>
        private void SetScheduleAll(bool isMonthFirst = false, bool isNewSearch = true)
        {
            // 検索条件のチェック
            if (!this.IsSearchSchedule()) return;

            // スケジュール項目の取得
            this.ScheduleItemList = this.GetScheduleItemList();
            
            // スケジュール設定
            this.SetSchedule(isNewSearch);

            //基準月先頭可否
            if (isMonthFirst == true)
            {
                //空車期間の開始日が設定してあるかどうか
                var start = this.EmptyStartDayDateTimePicker.SelectedDate;
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
        #endregion

        #endregion

        #region 画面イベント

        #region 車系コンボボックスの変更
        /// <summary>
        /// 車系コンボボックスの変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarGroupComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.CarGroupComboBox.SelectedValue?.ToString())
                || this.CarGroupComboBox.SelectedIndex == 0) return;

            SearchSchedule(true);
            this.OuterCarCalendarGrid.VerticalScrollBarOffset = 0;
        }
        #endregion

        #region 検索条件ボタン変更
        /// <summary>
        /// 検索条件ボタン変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            //検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionLayoutPanel, this.MainPanel, CondHeight);

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
            SearchSchedule(this.EmptyStartDayDateTimePicker.SelectedDate != null);
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
            if (this.EmptyStartDayDateTimePicker.SelectedDate != null)
            {
                var date = this.EmptyStartDayDateTimePicker.SelectedDate.Value;
                this.gridUtil.SetCalendarViewPeriod(new DateTime(date.Year, date.Month, 1));
            }

            // スケジュール設定（項目含む）
            this.gridUtil.ResetFilter();
            FormControlUtil.FormWait(this, () => this.SetScheduleAll(isMonthFirst: isMonthFirst));

            this.ActiveControl = OuterCarCalendarGrid;
        }

        #region 条件登録ボタンクリック
        /// <summary>
        /// 条件登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionRegistButton_Click(object sender, EventArgs e)
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;

            }

            var list = GetFavoriteList();

            if (list != null && list.Count() >= Const.FavoriteEntryMax)
            {
                //最大登録数以上の場合はエラー
                Messenger.Info(Resources.KKM00016);

                return;
            }

            // パラメータ設定
            var favorite = new OuterCarFavoriteItemModel
            {
                //車系
                CAR_GROUP = this.CarGroupComboBox.SelectedValue?.ToString(),
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
            this.ClearCondition();

        }
        #endregion

        #region 閉じるボタンクリック
        /// <summary>
        /// 閉じるボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #endregion

        #region カレンダーグリッドイベント

        #region 作業履歴の起動
        /// <summary>
        /// 作業履歴の起動
        /// </summary>
        private void OpenProgressListForm(ScheduleItemModel<OuterCarScheduleItemGetOutModel> item)
        {
            // スケジュール項目のチェック
            if (!this.IsEntryScheduleItem(ScheduleItemEditType.Update, item)) return;

            //作業履歴画面表示
            new FormUtil(new OuterCarHistoryForm { ScheduleItem = item.ScheduleItem, UserAuthority = this.UserAuthority, Reload = SetScheduleAll, IsItemNameEdit = !carManagerModelList.Any(x => x.CATEGORY_ID == item.ID.ToString()) }).SingleFormShow(this);
        }
        #endregion

        #region スケジュール項目詳細の起動
        /// <summary>
        /// スケジュール項目詳細の起動
        /// </summary>
        private void OpenScheduleItemForm(ScheduleItemModel<OuterCarScheduleItemGetOutModel> item, ScheduleItemEditType type)
        {
            // スケジュール項目のチェック
            if (!this.IsEntryScheduleItem(type, item)) return;

            item.ScheduleItemEdit = type;

            //追加かどうか
            if (type == ScheduleItemEditType.Insert)
            {
                //スケジュール項目初期化
                var scheduleItem = new OuterCarScheduleItemGetOutModel
                {
                    //開発符号
                    GENERAL_CODE = this.CarGroupComboBox.SelectedValue?.ToString(),

                    //車系
                    CAR_GROUP = this.CarGroupComboBox.SelectedValue?.ToString(),

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

            using (var form = new ScheduleItemDetailForm<OuterCarScheduleItemGetOutModel, OuterCarScheduleGetOutModel>()
            {
                FormSubTitle = "外製車",
                FunctionId = FunctionID.OuterCar,
                Item = item
            })
            {
                var temp = item.ScheduleItem.FLAG_要予約許可;

                // 項目編集
                if (form.ShowDialog().Equals(DialogResult.OK) || form.IsReload)
                {
                    var newItem = this.ScheduleItemGet(item.ScheduleItem.CATEGORY_ID);
                    if (temp == 1 && newItem.FLAG_要予約許可 != 1)
                    {
                        var schecule = this.ScheduleListGet(new OuterCarScheduleGetInModel()
                        {
                            DATE_START = DateTime.Now.Date,
                            CATEGORY_ID = item.ID
                        });
                        this.checkScheduleList = schecule.Where(x => x.予約種別 == Const.Kariyoyaku)?.Select(x => x.SCHEDULE_ID)?.ToList();

                        if (this.checkScheduleList != null && this.checkScheduleList.Any())
                        {
                            Messenger.Warn(string.Format(Resources.KKM02014, this.FormTitle));
                        }
                    }

                    // スケジュール設定（項目含む）
                    this.SetScheduleAll();

                }

            }

        }
        #endregion

        /// <summary>
        /// 項目移動フォーム表示
        /// </summary>
        /// <param name="item"></param>
        private void ShowScheduleItemMoveForm(ScheduleItemModel<OuterCarScheduleItemGetOutModel> item)
        {
            if (this.IsEntryScheduleItem(ScheduleItemEditType.Update, item) == false)
            {
                return;
            }

            using (var form = new ScheduleItemCarGroupMoveForm<OuterCarScheduleItemGetOutModel>() { FormSubTitle = "外製車", ScheduleItem = item, FormSubType = ScheduleItemType.OuterCar })
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    this.SetScheduleAll();
                }
            }
        }

        #region スケジュール項目の並び順変更
        /// <summary>
        /// スケジュール項目の並び順変更
        /// </summary>
        private void ChangeScheduleItemSort(ScheduleItemModel<OuterCarScheduleItemGetOutModel> sitem, ScheduleItemModel<OuterCarScheduleItemGetOutModel> ditem)
        {
            // スケジュール項目のチェック
            if (!this.IsEntryScheduleItem(ScheduleItemEditType.Update, sitem)) return;
            if (!this.IsEntryScheduleItem(ScheduleItemEditType.Update, ditem)) return;

            this.ScheduleItemPut(sitem, ScheduleItemEditType.Update);
        }
        #endregion

        #region スケジュールのチェック
        /// <summary>
        /// スケジュールのチェック
        /// </summary>
        /// <returns>bool</returns>
        private bool IsEntryScheduleItem(ScheduleItemEditType type, ScheduleItemModel<OuterCarScheduleItemGetOutModel> item)
        {
            //スケジュール編集区分ごとの分岐
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
                    //データが存在しているかどうか
                    var scheduleitem = this.ScheduleItemGet(item.ID);
                    if (scheduleitem == null)
                    {
                        //存在していない場合はエラー
                        Messenger.Info(Resources.KKM00021);

                        // スケジュール設定（項目含む）
                        this.SetScheduleAll();

                        return false;

                    }

                    // 最新の再設定
                    item.ScheduleItem = scheduleitem;
                    break;
            }

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //削除
                case ScheduleItemEditType.Delete:
                    //対象項目にスケジュールがある場合はエラー
                    if (this.ScheduleListGet(new OuterCarScheduleGetInModel { CATEGORY_ID = item.ID })?.Count() > 0)
                    {
                        Messenger.Info(Resources.KKM00033);

                        return false;
                    }
                    break;
            }

            return true;
        }
        #endregion

        #region スケジュール項目の取得
        /// <summary>
        /// スケジュール項目の取得
        /// </summary>
        /// <returns>OutercarScheduleItemGetOutModel</returns>
        private OuterCarScheduleItemGetOutModel ScheduleItemGet(long scheduleid)
        {
            OuterCarScheduleItemGetOutModel item = null;

            //検索条件
            var cond = new OuterCarScheduleItemGetInModel { SCHEDULE_ID = scheduleid };

            //APIで取得
            var res = HttpUtil.GetResponse<OuterCarScheduleItemGetInModel, OuterCarScheduleItemGetOutModel>(ControllerType.OuterCarScheduleItem, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                item = res.Results.FirstOrDefault();
            }

            return item;
        }
        #endregion

        #region スケジュール項目の更新
        /// <summary>
        /// スケジュール項目の更新
        /// </summary>
        private void ScheduleItemPut(ScheduleItemModel<OuterCarScheduleItemGetOutModel> item, ScheduleItemEditType type)
        {
            // スケジュール項目更新
            if (item != null && item.ID > 0)
            {
                // スケジュール項目のチェック
                if (!this.IsEntryScheduleItem(type, item)) return;

                var data = new OuterCarScheduleItemPutInModel
                {
                    // カテゴリーID
                    CATEGORY_ID = item.ID,
                    // 開発符号
                    GENERAL_CODE = item.ScheduleItem.GENERAL_CODE,
                    // 管理票番号
                    管理票番号 = item.ScheduleItem.管理票NO,
                    // カテゴリー
                    CATEGORY = item.ScheduleItem.CATEGORY,
                    // 並び順
                    SORT_NO = item.SortNo,
                    // 行数
                    PARALLEL_INDEX_GROUP = item.RowCount,
                    // パーソナルID
                    PERSONEL_ID = SessionDto.UserId,
                    //要予約許可
                    FLAG_要予約許可 = item.ScheduleItem.FLAG_要予約許可,
                    //最終予約可能日
                    最終予約可能日 = item.ScheduleItem.最終予約可能日
                };

                var res = HttpUtil.PutResponse<OuterCarScheduleItemPutInModel>(ControllerType.OuterCarScheduleItem, data);

                if (res.Status.Equals(Const.StatusSuccess))
                {
                    // 更新メッセージ
                    this.MessageLabel.Text = Resources.KKM00002;

                    // スケジュール設定（項目含む）
                    this.SetScheduleAll(isNewSearch: false);
                }
            }
        }
        #endregion

        #region スケジュール項目の削除
        /// <summary>
        /// スケジュール項目の削除
        /// </summary>
        private void ScheduleItemDelete(ScheduleItemModel<OuterCarScheduleItemGetOutModel> item)
        {
            if (Messenger.Confirm(Resources.KKM00007)   // 削除確認
             .Equals(DialogResult.No)) return;

            // スケジュール項目削除
            if (item != null && item.ID > 0)
            {
                // スケジュール項目のチェック
                if (!this.IsEntryScheduleItem(ScheduleItemEditType.Delete, item)) return;

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

                var data = new OuterCarScheduleItemDeleteInModel
                {
                    // カテゴリーID
                    CATEGORY_ID = item.ID
                };

                var res = HttpUtil.DeleteResponse<OuterCarScheduleItemDeleteInModel>(ControllerType.OuterCarScheduleItem, data);

                if (res.Status.Equals(Const.StatusSuccess))
                {
                    // 削除メッセージ
                    Messenger.Info(Resources.KKM00003);

                    // スケジュール設定（項目含む）
                    this.SetScheduleAll();
                }
            }
        }
        #endregion

        #region スケジュール詳細の起動
        /// <summary>
        /// スケジュール詳細の起動
        /// </summary>
        private void OpenScheduleForm(ScheduleModel<OuterCarScheduleGetOutModel> schedule, ScheduleEditType type)
        {
            // スケジュールのチェック
            var checkSameLine = (type == ScheduleEditType.Update) ? false : true;
            //Update Start 2022/03/08 杉浦 不具合修正
            //if (!this.IsEntrySchedule(type, schedule, checkSameLine)) return;
            if (!this.IsEntrySchedule(type, schedule, false, checkSameLine)) return;
            //Update End 2022/03/08 杉浦 不具合修正

            if (ShowNoteDialog(type, schedule) != DialogResult.OK) { return; }

            schedule.ScheduleEdit = type;

            //OKの場合は再検索
            if (ShowDetailForm(schedule) == DialogResult.OK)
            {
                //スケジュール一覧設定
                this.SetSchedule(false);
            }

        }

        /// <summary>
        /// スケジュール詳細画面表示処理。
        /// </summary>
        /// <param name="schedule"></param>
        private DialogResult ShowDetailForm(ScheduleModel<OuterCarScheduleGetOutModel> schedule)
        {
            DialogResult result = DialogResult.Cancel;
            using (var form = new OuterCarScheduleForm())
            {
                // パラメータ設定
                form.Schedule = schedule;
                form.IsManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
                form.IsUpdate = this.UserAuthority.UPDATE_FLG == '1';

                result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // 更新・削除メッセージ
                    this.MessageLabel.Text = form.ReturnMessage;

                    if (schedule.ScheduleEdit != ScheduleEditType.Update)
                    {
                        this.gridUtil.UndoRedoManager.Do(schedule);
                    }

                    if (schedule.ScheduleEdit == ScheduleEditType.Insert && this.TruckForm == null && this.IsFunctionEnable(FunctionID.Truck))
                    {
                        if (Messenger.Confirm(Resources.KKM01016) == DialogResult.Yes)
                        {
                            foreach (Form frm in Application.OpenForms)
                            {
                                if (frm.GetType() == typeof(TruckScheduleForm))
                                {
                                    frm.Close();
                                    break;
                                }
                            }
                            TruckScheduleButton_Click(null, null);
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        /// <summary>
        /// 注意喚起ダイアログ表示
        /// </summary>
        /// <param name="type"></param>
        /// <param name="schedule"></param>
        private DialogResult ShowNoteDialog(ScheduleEditType type, ScheduleModel<OuterCarScheduleGetOutModel> schedule)
        {
            if (this.UserAuthority.MANAGEMENT_FLG == '1')
            {
                return DialogResult.OK;
            }
            else
            {
                var item = this.ScheduleItemGet(schedule.CategoryID);

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

                    if (item.FLAG_要予約許可 == 1)
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
                    if (item.FLAG_要予約許可 == 1 && schedule.IsEdit && schedule.Schedule.予約種別 == Const.Kariyoyaku)
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

        #region スケジュールのチェック
        /// <summary>
        /// スケジュールのチェック
        /// </summary>
        /// <returns>bool</returns>
        //Update Start 2022/03/08 杉浦 不具合修正
        //private bool IsEntrySchedule(ScheduleEditType type, ScheduleModel<OuterCarScheduleGetOutModel> schedule, bool checkSameLine = true)
        private bool IsEntrySchedule(ScheduleEditType type, ScheduleModel<OuterCarScheduleGetOutModel> schedule, bool plusCheck = false, bool checkSameLine = true)
        //Update Start 2022/03/08 杉浦 不具合修正
        {
            //スケジュール項目が存在しているかどうか
            var item = this.ScheduleItemGet(schedule.CategoryID);

            if (item == null)
            {
                //存在していない場合はエラー
                Messenger.Info(Resources.KKM00021);

                // スケジュール設定（項目含む）
                this.SetScheduleAll();

                return false;
            }

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                case ScheduleEditType.Update:   //更新
                case ScheduleEditType.Delete:   //削除

                    if (this.ScheduleGet(schedule.ID) == null)
                    {
                        //存在していない場合はエラー
                        Messenger.Warn(Resources.KKM00021);

                        //スケジュール設定
                        this.SetSchedule();

                        return false;
                    }

                    break;
            }

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //追加
                //更新
                case ScheduleEditType.Insert:
                case ScheduleEditType.Update:
                    var errList = new List<string>();

                    var start = schedule.StartDate;
                    var end = schedule.EndDate;

                    //最終予約日が設定されているかどうか
                    if (item != null && item.最終予約可能日 != null)
                    {
                        var limit = item.最終予約可能日.Value.Date.AddDays(1);

                        //開始日か終了日が最終予約日を超過しているかどうか
                        if (start != null && start > limit || end != null && end > limit)
                        {
                            Messenger.Warn(Resources.KKM03010);
                            this.SetSchedule();
                            return false;
                        }
                    }

                    //検索条件
                    var cond = new OuterCarScheduleGetInModel
                    {
                        //カテゴリーID
                        CATEGORY_ID = schedule.CategoryID,
                        //期間(From)
                        DATE_START = this.OuterCarCalendarGrid.FirstDateInView.Date,
                        //期間(To)
                        DATE_END = this.OuterCarCalendarGrid.LastDateInView.Date
                    };

                    // チェックデータの取得
                    var list = this.ScheduleListGet(cond);

                    //同一行でスケジュールで重複した日付の期間が存在する場合はエラー
                    if (list.Where(x => x.SCHEDULE_ID != schedule.ID && x.PARALLEL_INDEX_GROUP == schedule.RowNo).Any(x =>
                            (x.START_DATE.Value.Date <= start.Value.Date && start.Value.Date <= x.END_DATE.Value.Date) ||
                            (x.START_DATE.Value.Date <= end.Value.Date && end.Value.Date <= x.END_DATE.Value.Date) ||
                            (start.Value.Date <= x.START_DATE.Value.Date && x.START_DATE.Value.Date <= end.Value.Date) ||
                            (start.Value.Date <= x.END_DATE.Value.Date && x.END_DATE.Value.Date <= end.Value.Date)) == true && checkSameLine)
                    {
                        errList.Add(Resources.KKM03017);
                    }

                    //Delete Start 2022/03/16 杉浦 チェック削除
                    //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
                    //Append Start 2022/03/08 杉浦 不具合修正
                    //if (plusCheck && !string.IsNullOrEmpty(item.管理票NO))
                    //{
                    //    //Append End 2022/03/08 杉浦 不具合修正
                    //    //検索条件
                    //    var cond2 = new AllScheduleSearchModel
                    //    {
                    //        //管理票番号
                    //        管理票番号 = item.管理票NO,

                    //        //期間(From)
                    //        START_DATE = schedule.StartDate?.Date,

                    //        //期間(To)
                    //        END_DATE = schedule.EndDate?.Date,

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

                    //    //Append Start 2022/03/08 杉浦 不具合修正
                    //}
                    ////Append End 2022/03/08 杉浦 不具合修正
                    //Delete End 2022/03/16 杉浦 チェック削除

                    //エラーがある場合は終了
                    if (errList.Any() == true)
                    {
                        Messenger.Warn(string.Join(Const.CrLf, errList.ToArray()));

                        //スケジュール設定
                        this.SetSchedule();

                        return false;
                    }

                    if (this.UserAuthority.MANAGEMENT_FLG == '0' && schedule.IsEdit && (end.Value.Date - start.Value.Date).Days >= 5)
                    {
                        Messenger.Info(Resources.KKM01001);
                        this.SetSchedule();
                        return false;
                    }

                    break;
            }

            return true;
        }
        #endregion

        #region スケジュールの取得
        /// <summary>
        /// スケジュールの取得（スケジュールID）
        /// </summary>
        /// <returns>OutercarScheduleGetOutModel</returns>
        private OuterCarScheduleGetOutModel ScheduleGet(long scheduleid)
        {
            OuterCarScheduleGetOutModel schedule = null;

            //パラメータ設定
            var cond = new OuterCarScheduleGetInModel
            {
                // スケジュールID
                SCHEDULE_ID = scheduleid
            };

            //APIで取得
            var res = HttpUtil.GetResponse<OuterCarScheduleGetInModel, OuterCarScheduleGetOutModel>(ControllerType.OuterCarSchedule, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                schedule = res.Results.FirstOrDefault();
            }

            return schedule;
        }
        /// <summary>
        /// スケジュールの取得（検索条件）
        /// </summary>
        /// <returns>OutercarScheduleGetOutModel</returns>
        private List<OuterCarScheduleGetOutModel> ScheduleListGet(OuterCarScheduleGetInModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<OuterCarScheduleGetInModel, OuterCarScheduleGetOutModel>(ControllerType.OuterCarSchedule, cond);

            return (res.Results).ToList();
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

        #region スケジュールの登録
        /// <summary>
        /// スケジュールの登録
        /// </summary>
        private void SchedulePost(ScheduleModel<OuterCarScheduleGetOutModel> schedule)
        {
            if (schedule == null) return;

            // スケジュールのチェック
            //Update Start 2022/03/08 杉浦 不具合修正
            //if (!this.IsEntrySchedule(ScheduleEditType.Insert, schedule)) return;
            if (!this.IsEntrySchedule(ScheduleEditType.Insert, schedule, true)) return;
            //Update End 2022/03/08 杉浦 不具合修正

            if (ShowNoteDialog(ScheduleEditType.Insert, schedule) == DialogResult.OK)
            {
                schedule.ScheduleEdit = ScheduleEditType.Insert;

                //コピー元のスケジュール情報を書き換え

                //カテゴリーーID
                schedule.Schedule.CATEGORY_ID = schedule.CategoryID;

                //行番号
                schedule.Schedule.PARALLEL_INDEX_GROUP = schedule.RowNo;

                //開始日
                schedule.Schedule.START_DATE = schedule.StartDate;

                //終了日
                schedule.Schedule.END_DATE = schedule.EndDate;

                var item = this.ScheduleItemGet(schedule.CategoryID);
                schedule.Schedule.FLAG_要予約許可 = (short)item.FLAG_要予約許可;

                ShowDetailForm(schedule);
            }

            //スケジュール一覧設定
            this.SetSchedule(false);
        }
        #endregion

        #region スケジュールの更新
        /// <summary>
        /// スケジュールの更新
        /// </summary>
        private void SchedulePut(ScheduleModel<OuterCarScheduleGetOutModel> schedule)
        {
            // スケジュール登録
            if (schedule != null && schedule.ID > 0)
            {
                // スケジュールのチェック
                //Update Start 2022/03/08 杉浦 不具合修正
                //if (!this.IsEntrySchedule(ScheduleEditType.Update, schedule)) return;
                if (!this.IsEntrySchedule(ScheduleEditType.Update, schedule, true)) return;
                //Update End 2022/03/08 杉浦 不具合修正

                var data = new OuterCarSchedulePutInModel
                {
                    // スケジュールID
                    SCHEDULE_ID = schedule.ID,
                    // 行番号
                    PARALLEL_INDEX_GROUP = schedule.RowNo,
                    // 期間（開始）
                    START_DATE = schedule.StartDate,
                    // 期間（終了）
                    END_DATE = schedule.EndDate,
                    // スケジュール区分
                    SYMBOL = schedule.Schedule.SYMBOL,
                    // 説明
                    DESCRIPTION = schedule.Schedule.DESCRIPTION,
                    // パーソナルID
                    PERSONEL_ID = SessionDto.UserId,
                    // 予約種別
                    予約種別 = string.IsNullOrEmpty(schedule.Schedule.予約種別) ? "仮予約" : schedule.Schedule.予約種別,
                    // 目的
                    目的 = schedule.Schedule.目的,
                    // 行先
                    行先 = schedule.Schedule.行先,
                    // 使用者TEL
                    TEL = schedule.Schedule.TEL,
                    // 空時間貸出可フラグ
                    FLAG_空時間貸出可 = schedule.Schedule.FLAG_空時間貸出可 == 1 ? 1 : 0,
                    // 予約者ID
                    予約者_ID = SessionDto.UserId,
                    // 駐車場番号
                    駐車場番号 = schedule.Schedule.駐車場番号
                };

                var res = HttpUtil.PutResponse<OuterCarSchedulePutInModel>(ControllerType.OuterCarSchedule, data);

                if (res.Status.Equals(Const.StatusSuccess))
                {
                    // 更新メッセージ
                    this.MessageLabel.Text = string.Format(Resources.KKM01019, getYMDH(schedule.StartDate), getYMDH(schedule.EndDate), Resources.KKM00002);

                    // スケジュール設定
                    this.SetSchedule(false);
                }
            }
        }
        #endregion

        #region スケジュールの削除
        /// <summary>
        /// スケジュールの削除
        /// </summary>
        private void ScheduleDelete(ScheduleModel<OuterCarScheduleGetOutModel> schedule, bool confirm)
        {
            if (confirm)
                if (Messenger.Confirm(Resources.KKM00007)   // 削除確認
                .Equals(DialogResult.No)) return;

            // スケジュール削除
            if (schedule != null && schedule.ID > 0)
            {
                // スケジュールのチェック
                if (!this.IsEntrySchedule(ScheduleEditType.Delete, schedule)) return;

                var data = new OuterCarScheduleDeleteInModel
                {
                    // スケジュールID
                    SCHEDULE_ID = schedule.ID
                };

                var res = HttpUtil.DeleteResponse<OuterCarScheduleDeleteInModel>(ControllerType.OuterCarSchedule, data);

                if (res.Status.Equals(Const.StatusSuccess))
                {
                    // 削除メッセージ
                    this.MessageLabel.Text = string.Format(Resources.KKM01019, getYMDH(schedule.StartDate), getYMDH(schedule.EndDate), Resources.KKM00003);

                    // スケジュール設定
                    this.SetSchedule(false);
                    schedule.ScheduleEdit = ScheduleEditType.Delete;
                    this.gridUtil.UndoRedoManager.Do(schedule);
                }
            }
        }
        #endregion

        #endregion

        #region データの検索・取得
        #region お気に入り検索
        /// <summary>
        /// お気に入り検索
        /// </summary>
        private List<FavoriteSearchOutModel> GetFavoriteList()
        {
            //パラメータ設定
            var itemCond = new FavoriteSearchInModel
            {
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,
                // データ区分
                CLASS_DATA = FavoriteClassData
            };

            //Get実行
            var res = HttpUtil.GetResponse<FavoriteSearchInModel, FavoriteSearchOutModel>(ControllerType.Favorite, itemCond);

            return (res.Results).ToList();
        }
        #endregion

        #region お気に入り（検索条件）の取得
        /// <summary>
        /// お気に入り（検索条件）の取得
        /// </summary>
        private OuterCarFavoriteSearchOutModel GetFavoriteData()
        {
            var list = new List<OuterCarFavoriteSearchOutModel>();

            //パラメータ設定
            var itemCond = new OuterCarFavoriteSearchInModel
            {
                // お気に入りID
                ID = Convert.ToInt64(this.FavoriteID),
            };

            //Get実行
            var res = HttpUtil.GetResponse<OuterCarFavoriteSearchInModel, OuterCarFavoriteSearchOutModel>(ControllerType.OuterCarFavorite, itemCond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                list.AddRange(res.Results);
            }

            return list.FirstOrDefault();
        }
        #endregion

        #region 車系検索
        /// <summary>
        /// 車系検索
        /// </summary>
        private List<CarGroupSearchOutModel> GetCarGroupList()
        {
            //パラメータ設定
            var itemCond = new CarGroupSearchInModel
            {
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                //開発フラグ
                UNDER_DEVELOPMENT = 1

            };

            //Get実行
            var res = HttpUtil.GetResponse<CarGroupSearchInModel, CarGroupSearchOutModel>(ControllerType.CarGroup, itemCond);

            return (res.Results).ToList();
        }
        #endregion

        #region 車両管理担当取得
        /// <summary>
        /// 車両管理担当取得
        /// </summary>
        /// <returns></returns>
        private string GetCarManager()
        {
            var res = HttpUtil.GetResponse<CarManagerSearchModel, CarManagerModel>(ControllerType.CarManager,
                new CarManagerSearchModel { GENERAL_CODE = this.CarGroupComboBox.SelectedValue.ToString(), FUNCTION_ID = (int)FunctionID.OuterCar });

            carManagerModelList = new List<CarManagerModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                carManagerModelList.AddRange(res.Results);
            }

            string managerName = "";
            var managerList = carManagerModelList.Where(x => x.CATEGORY_ID == null).OrderBy(x => x.STATUS).ToList();

            foreach (var item in managerList)
            {
                managerName += string.Format("(" + item.STATUS + "){0} {1}\n ({2})\n", item.SECTION_CODE, item.NAME, item.TEL);
            }

            managerName += managerList.FirstOrDefault()?.REMARKS;

            return managerName;
        }
        #endregion

        #region カレンダーグリッドデータの取得
        /// <summary>
        /// スケジュール項目取得
        /// </summary>
        /// <returns>IEnumerable</returns>
        private IEnumerable<OuterCarScheduleItemGetOutModel> GetScheduleItemList()
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
                var itemRes = HttpUtil.GetResponse<OuterCarScheduleItemGetInModel, OuterCarScheduleItemGetOutModel>(ControllerType.OuterCarScheduleItem,
                    new OuterCarScheduleItemGetInModel
                    {
                        ID = this.CalendarCategoryId
                    });
                var data = itemRes.Results;
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

            var cond = new OuterCarScheduleItemGetInModel
            {
                //Openフラグ
                OPEN_FLG = openFlg,
                //空車期間FROM
                EMPTY_START_DATE = getDateTime(this.EmptyStartDayDateTimePicker.SelectedDate, this.EmptyStartTimeComboBox.Text),
                //空車期間TO
                EMPTY_END_DATE = getDateTime(this.EmptyEndDayDateTimePicker.SelectedDate, this.EmptyEndTimeComboBox.Text),
                //車系
                車系 = this.CarGroupComboBox.SelectedValue?.ToString(),
                //予約者ID
                INPUT_PERSONEL_ID = SessionDto.UserId
            };

            //APIで取得
            var res = HttpUtil.GetResponse<OuterCarScheduleItemGetInModel, OuterCarScheduleItemGetOutModel>(ControllerType.OuterCarScheduleItem, cond);

            //返却
            return res.Results;
        }

        /// <summary>
        /// スケジュール取得
        /// </summary>
        /// <returns>IEnumerable</returns>
        private IEnumerable<OuterCarScheduleGetOutModel> GetScheduleList()
        {
            var itemList = this.ScheduleItemList;

            var cond = new OuterCarScheduleGetInModel
            {
                //空車期間FROM
                EMPTY_START_DATE = getDateTime(this.EmptyStartDayDateTimePicker.SelectedDate, this.EmptyStartTimeComboBox.Text),
                //空車期間TO
                EMPTY_END_DATE = getDateTime(this.EmptyEndDayDateTimePicker.SelectedDate, this.EmptyEndTimeComboBox.Text),
                //車系
                車系 = this.CarGroupComboBox.SelectedValue?.ToString(),
                // 期間(From)
                DATE_START = this.OuterCarCalendarGrid.FirstDateInView.Date,
                // 期間(To)
                DATE_END = this.OuterCarCalendarGrid.LastDateInView.Date
            };

            //APIで取得
            var res = HttpUtil.GetResponse<OuterCarScheduleGetInModel, OuterCarScheduleGetOutModel>(ControllerType.OuterCarSchedule, cond);

            //返却
            return res.Results;
        }
        #endregion

        #endregion

        #region その他

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
            string formName = typeof(LegendForm).Name + "_OuterCarForm";

            var openform = Application.OpenForms[formName];

            if (openform == null)
            {
                var frm = new LegendForm();
                frm.Name = formName;

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

            var cond = new OuterCarScheduleGetInModel
            {
                //空車期間FROM
                EMPTY_START_DATE = getDateTime(this.EmptyStartDayDateTimePicker.SelectedDate, this.EmptyStartTimeComboBox.Text),
                //空車期間TO
                EMPTY_END_DATE = getDateTime(this.EmptyEndDayDateTimePicker.SelectedDate, this.EmptyEndTimeComboBox.Text),
                //車系
                車系 = this.CarGroupComboBox.SelectedValue?.ToString()
            };

            using (var form = new CalendarPrintExcelForm(
                this.ScheduleItemList.ToList(), cond, this.gridUtil.CalendarSetting.CalendarMode))
            {
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// トラック予約フォーム表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckScheduleButton_Click(object sender, EventArgs e)
        {
            if (this.TruckForm == null)
            {
                this.TruckForm = new TruckScheduleForm();
                this.TruckForm.SyncFormRefreshEvent += TruckForm_SyncFormRefreshEvent;
                this.TruckForm.SyncFormStyleSaveEvent += TruckForm_SyncFormStyleSaveEvent;
                this.TruckScheduleButton.Text = "トラック予約状況非表示";
                this.TruckForm.SyncCalendarGrid = this.OuterCarCalendarGrid;
                this.TruckForm.SyncCalendarStyle = Properties.Settings.Default.OuterCarCalendarStyle;
                this.TruckScheduleCheckBox.Checked = true;
                this.TruckForm.SyncCheck = this.TruckScheduleCheckBox.Checked;
                this.TruckForm.Show(this);
                this.TruckForm.TruckCalendarGridEventAdd();
                this.ActiveControl = OuterCarCalendarGrid;
            }
            else
            {
                this.TruckForm.Close();
                TruckForm_SyncFormRefreshEvent(null, null);
            }

            if (this.SearchConditionLayoutPanel.Visible == true)
            {
                SearchConditionButton_Click(null, null);
            }
        }

        /// <summary>
        /// トラック予約表同期処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckForm_SyncFormStyleSaveEvent(object sender, EventArgs e)
        {
            if (this.gridUtil == null) { return; }

            this.TruckForm.SyncFlag = false;
            this.gridUtil.CalendarSetting.SaveCalendarUserData -= this.SaveCalendarUserData;

            var oldFirstDateInView = this.OuterCarCalendarGrid.FirstDateInView;
            this.OuterCarCalendarGrid.FirstDateInView = this.TruckForm.TruckScheduleCalendarGrid.FirstDateInView;

            if (this.gridUtil.ChangeTemplateSettings(new CalendarSettings((StringCollection)sender)) || oldFirstDateInView != this.TruckForm.TruckScheduleCalendarGrid.FirstDateInView)
            {
                this.gridUtil.SetCalendarViewPeriod(new DateTime(this.OuterCarCalendarGrid.FirstDateInView.Year, this.OuterCarCalendarGrid.FirstDateInView.Month, 1));
                this.gridUtil.SetTemplateHeader();
                SetSchedule(this.OuterCarCalendarGrid.FirstDateInView, this.TruckForm.TruckScheduleCalendarGrid.LastDateInView);
            }
            this.gridUtil.SetScheduleMostDayFirst(this.TruckForm.TruckScheduleCalendarGrid.FirstDisplayedCellPosition.Date);
            this.OuterCarCalendarGrid.HorizontalScrollBarOffset = this.TruckForm.TruckScheduleCalendarGrid.HorizontalScrollBarOffset;

            this.gridUtil.CalendarSetting.SaveCalendarUserData += this.SaveCalendarUserData;
            this.TruckForm.SyncFlag = true;
        }

        /// <summary>
        /// 連携フォームリフレッシュ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckForm_SyncFormRefreshEvent(object sender, EventArgs e)
        {
            this.TruckForm = null;
            this.TruckScheduleButton.Text = "トラック予約状況表示";
        }

        /// <summary>
        /// トラック予約同期チェック
        /// </summary>
        /// <returns></returns>
        private bool IsTruckSync()
        {
            return TruckForm != null && this.TruckScheduleCheckBox.Checked && this.TruckForm.SyncFlag;
        }

        /// <summary>
        /// スクロールオフセット再設定
        /// </summary>
        /// <remarks>
        /// スクロールのトリガーのコントロールがカレンダーコントロールに存在する
        /// コントロールの場合、スクロールをしないよう制御します。
        /// </remarks>
        /// <param name="activeControl"></param>
        /// <returns></returns>
        protected override Point ScrollToControl(Control activeControl)
        {
            if (this.gridUtil.CanScrollToControl(activeControl.GetType()) == false)
            {
                return new Point(-this.HorizontalScroll.Value, -this.VerticalScroll.Value);
            }
            else
            {
                return base.ScrollToControl(activeControl);
            }
        }

        /// <summary>
        /// Xeyeページ遷移
        /// </summary>
        /// <param name="item">スケジュール項目</param>
        private void RunXeyeWebBrowser(ScheduleItemModel<OuterCarScheduleItemGetOutModel> item)
        {
            var list = new[] { item.ScheduleItem };

            var list2 = list.Select(x => x.管理票NO).ToList();

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
        /// 連絡先編集フォーム表示
        /// </summary>
        private void ShowScheduleItemContactInfoForm()
        {
            using (var form = new ScheduleItemContactInfoForm<OuterCarScheduleItemGetOutModel>()
            { InfoType = ContactInfoType.All, Code = this.CarGroupComboBox.SelectedValue?.ToString(), FunctionId = FunctionID.OuterCar })
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
        private void ShowScheduleItemContactInfoForm(ScheduleItemModel<OuterCarScheduleItemGetOutModel> selectScheduleItem, ScheduleItemEditType type)
        {
            if (type == ScheduleItemEditType.Insert)
            {
                var scheduleItem = new OuterCarScheduleItemGetOutModel
                {
                    GENERAL_CODE = this.CarGroupComboBox.SelectedValue?.ToString(),
                    CAR_GROUP = this.CarGroupComboBox.SelectedValue?.ToString(),
                    SORT_NO = 0,                    
                    PARALLEL_INDEX_GROUP = 1
                };

                if (selectScheduleItem.ScheduleItem != null)
                {
                    scheduleItem.SORT_NO = selectScheduleItem.SortNo + 0.1D;
                }
                selectScheduleItem.ScheduleItem = scheduleItem;                
            }
            selectScheduleItem.ScheduleItemEdit = type;

            using (var form = new ScheduleItemContactInfoForm<OuterCarScheduleItemGetOutModel>()
            {
                InfoType = ContactInfoType.Item,
                Code = this.CarGroupComboBox.SelectedValue?.ToString(),
                Item = selectScheduleItem,
                FunctionId = FunctionID.OuterCar
            })
            {
                var ret = form.ShowDialog(this);

                if (ret == DialogResult.OK)
                {
                    SetScheduleAll();
                }
            }
        }

        /// <summary>
        /// 元に戻す・やり直し処理
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private bool UndoRedo(ScheduleModel<OuterCarScheduleGetOutModel> schedule)
        {
            if (schedule.ScheduleEdit == ScheduleEditType.Update)
            {
                var data = new OuterCarSchedulePutInModel()
                {
                    DESCRIPTION = schedule.Schedule.DESCRIPTION,
                    START_DATE = schedule.StartDate,
                    END_DATE = schedule.EndDate,
                    FLAG_空時間貸出可 = schedule.Schedule.FLAG_空時間貸出可.Value,
                    PARALLEL_INDEX_GROUP = schedule.Schedule.PARALLEL_INDEX_GROUP,
                    SCHEDULE_ID = schedule.Schedule.SCHEDULE_ID,
                    予約者_ID = schedule.Schedule.予約者_ID,
                    SYMBOL = schedule.Schedule.SYMBOL,
                    TEL = schedule.Schedule.TEL,
                    予約種別 = schedule.Schedule.予約種別,
                    目的 = schedule.Schedule.目的,
                    駐車場番号 = schedule.Schedule.駐車場番号,
                    行先 = schedule.Schedule.行先,
                    PERSONEL_ID = SessionDto.UserId
                };
                var res = HttpUtil.PutResponse<OuterCarSchedulePutInModel>(ControllerType.OuterCarSchedule, data);
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    this.SetSchedule(false);
                    return true;
                }
                return false;
            }
            else if (schedule.ScheduleEdit == ScheduleEditType.Delete)
            {
                var data = new OuterCarSchedulePostInModel
                {
                    CATEGORY_ID = schedule.Schedule.CATEGORY_ID,
                    START_DATE = schedule.Schedule.START_DATE,
                    END_DATE = schedule.Schedule.END_DATE,
                    PARALLEL_INDEX_GROUP = schedule.Schedule.PARALLEL_INDEX_GROUP,
                    SYMBOL = schedule.Schedule.SYMBOL,
                    DESCRIPTION = schedule.Schedule.DESCRIPTION,
                    予約種別 = schedule.Schedule.予約種別,
                    SECTION_GROUP_ID = SessionDto.SectionGroupID,
                    PERSONEL_ID = SessionDto.UserId,
                    目的 = schedule.Schedule.目的,
                    行先 = schedule.Schedule.行先,
                    TEL = schedule.Schedule.TEL,
                    FLAG_空時間貸出可 = schedule.Schedule.FLAG_空時間貸出可.Value,
                    予約者_ID = schedule.Schedule.予約者_ID,
                    駐車場番号 = schedule.Schedule.駐車場番号
                };

                var res = HttpUtil.PostResponse<OuterCarSchedulePostInModel>(ControllerType.OuterCarSchedule, data);
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    schedule.Schedule = res.Results.OfType<OuterCarScheduleGetOutModel>().FirstOrDefault();
                    this.SetSchedule(false);
                    return true;
                }
                return false;
            }
            else if (schedule.ScheduleEdit == ScheduleEditType.Insert || schedule.ScheduleEdit == ScheduleEditType.Paste)
            {
                var model = new OuterCarScheduleDeleteInModel();
                model.SCHEDULE_ID = schedule.Schedule.SCHEDULE_ID;
                var res = HttpUtil.DeleteResponse<OuterCarScheduleDeleteInModel>(ControllerType.OuterCarSchedule, model);
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    this.SetSchedule(false);
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
