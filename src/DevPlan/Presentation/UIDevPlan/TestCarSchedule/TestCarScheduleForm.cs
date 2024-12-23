using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UIDevPlan.TruckSchedule;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Util;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Utils.Calendar;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    /// <summary>
    /// 試験車日程
    /// </summary>
    public partial class TestCarScheduleForm : BaseForm
    {
        #region メンバ変数
        private const int CondHeight = 105;

        private const int SymbolDefault = 1;

        CalendarGridUtil<TestCarScheduleItemModel, TestCarScheduleModel> gridUtil;

        private Func<DateTime?, string> getYMDH = dt => { return dt != null ? ((DateTime)dt).ToString("yyyy/MM/dd H時") : string.Empty; };

        private TruckScheduleForm TruckForm;

        private List<CarManagerModel> carManagerModelList;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "試験車日程"; } }

        /// <summary>スケジュール項目リスト</summary>
        private IEnumerable<TestCarScheduleItemModel> ScheduleItemList { get; set; }

        /// <summary>試験車スケジュール検索条件</summary>
        private TestCarScheduleSearchModel TestCarScheduleSearchCond { get; set; }

        /// <summary>開発符号初期化中可否</summary>
        private bool IsGeneralCodeInit { get; set; }

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; } = false;

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>お気に入りID</summary>
        public long? FavoriteID { get; set; }

        public string setCarGroup { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCarScheduleForm()
        {
            InitializeComponent();
            this.FormClosing += TestCarScheduleForm_FormClosing;
        }
        #endregion

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarScheduleForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //権限
                this.UserAuthority = base.GetFunction(FunctionID.TestCar);

                //カレンダーグリッド初期化
                this.InitCalendarGrid();

                //画面初期化
                this.InitForm();

            });

            if (this.IsFunctionEnable(FunctionID.Truck) == false)
            {
                this.TruckScheduleButton.Visible = false;
                this.TruckScheduleCheckBox.Visible = false;
            }
        }

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

        /// <summary>
        /// カレンダーグリッド初期化
        /// </summary>
        private void InitCalendarGrid()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            var mainMap = new Dictionary<string, Action<ScheduleItemModel<TestCarScheduleItemModel>>>();
            var contactMap = new Dictionary<string, Action<ScheduleItemModel<TestCarScheduleItemModel>>>();

            //管理権限があるかどうか
            if (isManagement == true)
            {
                mainMap["項目追加"] = (item => this.ShowScheduleItemDetailForm(item, ScheduleItemEditType.Insert));
                mainMap["項目編集"] = (item => this.ShowScheduleItemDetailForm(item, ScheduleItemEditType.Update));
                mainMap["項目削除"] = this.DeleteScheduleItem;
                mainMap["項目移動"] = (item => this.ShowScheduleItemMoveForm(item));
                mainMap["管理者追加"] = (item => this.ShowScheduleItemContactInfoForm(item, ScheduleItemEditType.Insert));

                contactMap["項目追加"] = (item => this.ShowScheduleItemDetailForm(item, ScheduleItemEditType.Insert));
                contactMap["項目削除"] = this.DeleteScheduleItem;
                contactMap["管理者追加"] = (item => this.ShowScheduleItemContactInfoForm(item, ScheduleItemEditType.Insert));
                contactMap["管理者編集"] = (item => this.ShowScheduleItemContactInfoForm(item, ScheduleItemEditType.Update));
            }

            //カレンダーグリッド設定
            var config = new CalendarGridConfigModel<TestCarScheduleItemModel, TestCarScheduleModel>(this.TestCarCalendarGrid, isManagement, isUpdate,
            new Dictionary<string, Action>
            {
                { "項目追加", () => this.ShowScheduleItemDetailForm(new ScheduleItemModel<TestCarScheduleItemModel>(), ScheduleItemEditType.Insert) },
                { "管理者編集", () => this.ShowScheduleItemContactInfoForm() }
            },
            mainMap,
            new Dictionary<string, Action<ScheduleModel<TestCarScheduleModel>>>
            {
                { "削除",  (schedule => this.DeleteSchedule(schedule, true)) }

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
                //Append Start 2021/05/26 杉浦 試験車日程_管理票の情報表示機能追加
                if (item.ScheduleItem.管理票番号 != null)
                {
                    var menu = new ToolStripMenuItem { Text = "試験車情報" };
                    menu.Click += (sender2, e2) =>
                    {
                        ShowTestCarForm(item.ScheduleItem.管理票番号);
                    };
                    list.Add(menu);
                }
                //Append End 2021/05/26 杉浦 試験車日程_管理票の情報表示機能追加

                if (list.Any()) { list.Add(new ToolStripSeparator()); }
                return list.ToArray();
            };

            //スケジュールのツールチップテキストのタイトルを取得するデリゲート
            config.GetScheduleToolTipTitle = schedule => schedule.Schedule.DESCRIPTION;

            //スケジュールの追加するツールチップテキストを取得するデリゲート
            config.GetScheduleAddToolTipText = schedule => string.Format("{0} {1}", schedule.Schedule.設定者_SECTION_CODE, schedule.Schedule.設定者_NAME);
            
            //試験車管理SYS情報フィルタ利用可否
            config.IsRowHeaderSysFilter = isManagement;

            //カレンダーの設定情報
            config.CalendarSettings = new CalendarSettings(Properties.Settings.Default.TestCarCalendarStyle);

            //カレンダーグリッドの初期設定
            this.gridUtil = new CalendarGridUtil<TestCarScheduleItemModel, TestCarScheduleModel>(config);

            //スケジュール表示期間の変更後のデリゲート
            this.gridUtil.ScheduleViewPeriodChangedAfter += (start, end) => FormControlUtil.FormWait(this, () => this.SetSchedule(start, end));

            //スケジュール行ヘッダーダブルクリックのデリゲート
            this.gridUtil.ScheduleRowHeaderDoubleClick += this.ShowHistoryScheduleItem;

            //スケジュール項目の背景色を取得するデリゲート
            config.GetScheduleItemBackColor = x => SetScheduleItemBackColor(x.ScheduleItem);

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
            this.gridUtil.SchedulePaste += (copySchedule, schedule) => FormControlUtil.FormWait(this, () => this.PasteSchedule(copySchedule, schedule));

            //カレンダーレイアウト状態保存デリゲート
            this.gridUtil.CalendarSetting.SaveCalendarUserData += this.SaveCalendarUserData;

            this.TestCarCalendarGrid.HorizontalScrollBarOffsetChanged += TestCarCalendarGrid_HorizontalScrollBarOffsetChanged;
            this.TruckScheduleCheckBox.CheckedChanged += TruckScheduleCheckBox_CheckedChanged;
            this.gridUtil.UndoRedo += (schedule) => this.UndoRedo(schedule);
        }

        /// <summary>
        /// スケジュール項目背景色算出処理。
        /// </summary>
        /// <param name="scheduleItem"></param>
        /// <returns></returns>
        private CalendarScheduleColorEnum SetScheduleItemBackColor(TestCarScheduleItemModel scheduleItem)
        {
            if (carManagerModelList.Any(x => x.CATEGORY_ID == scheduleItem.CATEGORY_ID.ToString()))
            {
                return CalendarScheduleColorEnum.ContactInfoItemColor;
            }
            else
            {
                return CalendarScheduleColorEnum.NomalColor;
            }
        }

        /// <summary>
        /// トラック同期チェックボックス変更時処理
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
                this.TruckForm.SyncCalendarStyle = Properties.Settings.Default.TestCarCalendarStyle;
                this.TruckForm.RefreshCalendarTemplate();
            }
        }

        /// <summary>
        /// カレンダーグリッド水平スクロールバー変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarCalendarGrid_HorizontalScrollBarOffsetChanged(object sender, EventArgs e)
        {
            if (IsTruckSync())
            {
                if (this.TruckForm.TruckScheduleCalendarGrid.HorizontalScrollBarOffset != this.TestCarCalendarGrid.HorizontalScrollBarOffset)
                {
                    this.TruckForm.TruckCalendarGridEventDelete();
                    this.TruckForm.TruckScheduleCalendarGrid.HorizontalScrollBarOffset = this.TestCarCalendarGrid.HorizontalScrollBarOffset;
                    this.TruckForm.TruckCalendarGridEventAdd();
                }
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
                Properties.Settings.Default.TestCarCalendarStyle = style;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isTestCarRead = base.GetFunction(FunctionID.TestCarManagement)?.READ_FLG == '1';

            //試験車スケジュール検索条件
            this.TestCarScheduleSearchCond = null;

            //検索条件
            this.SearchConditionTableLayoutPanel.Visible = true;

            //基準月
            this.MonthDateTimePicker.Value = this.MonthDateTimePicker.MinDate;
            this.MonthDateTimePicker.Value = DateTimeUtil.GetMostMondayFirst();

            //仮予約注意喚起
            this.ReservationAlertPanel.Visible = isManagement;

            //使用履歴簡易入力
            this.TestCarUseHistoryInputFormButton.Visible = isTestCarRead;

            //要望案コピーボタン
            this.RequestCopyButton.Visible = isManagement;

            //一括本予約ボタン
            this.BulkReservationButton.Visible = isManagement;

            //ダウンロードボタン
            this.ExcelPrintButton.Visible = isExport;

            //お気に入りIDがあるかどうか
            if (this.FavoriteID == null)
            {
                //スケジュールの先頭を基準月に設定
                this.gridUtil.SetScheduleMostDayFirst(this.MonthDateTimePicker.Value);

            }
            else
            {
                //お気に入りを設定できなければ終了
                if (this.SetFavoriteCondition() == false)
                {
                    return;
                }

                //スケジュール一覧設定
                this.SetScheduleList(isMonthFirst: true);
            }

            this.TruckScheduleButton.Visible = false;
            this.TruckScheduleCheckBox.Visible = false;
        }
        #endregion

        #region お気に入り選択

        /// <summary>
        /// お気に入りの検索条件を設定
        /// </summary>
        private bool SetFavoriteCondition()
        {
            //お気に入りが取得できたかどうか
            var favorite = this.GetFavorite(this.FavoriteID.Value);
            if (favorite == null)
            {
                return false;
            }

            // 開発符号
            this.SetGeneralCode(favorite.GENERAL_CODE);

            this.IsBind = true;

            try
            {
                //ステータス
                if (favorite.STATUS_KBN == "1")
                {
                    // 使用部署要望案
                    this.StatusRequestRadioButton.Checked = true;
                }
                else if (favorite.STATUS_KBN == "2")
                {
                    // SJSB調整案
                    this.StatusAdjustmentRadioButton.Checked = true;
                }
                else if (favorite.STATUS_KBN == "3")
                {
                    // 最終調整結果
                    this.StatusFinalRadioButton.Checked = true;
                }

                //OPEN/CLOSE
                this.StatusOpenCheckBox.Checked = favorite.STATUS_OPEN_FLG == "1";
                this.StatusCloseCheckBox.Checked = favorite.STATUS_CLOSE_FLG == "1";
            }
            finally
            {
                this.IsBind = false;
            }

            return true;

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
            var inControl = new List<Control>() { this.ButtonPanel, this.ReservationAlertPanel, this.EasyInputPanel };

            //検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, null, inControl, CondHeight);
        }
        #endregion

        #region 開発符号マウスクリック
        /// <summary>
        /// 開発附号マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            ShowGeneralCodeListForm();
        }

        /// <summary>
        /// 開発符号選択フォーム表示処理。
        /// </summary>
        /// <remarks>
        /// 開発符号選択フォームを表示します。表示権限がない場合はメッセージを表示します。
        /// </remarks>
        private void ShowGeneralCodeListForm()
        {
            // 全閲覧権限
            var isAllGeneralCode = this.UserAuthority.ALL_GENERAL_CODE_FLG == '1';

            string selectGeneralCode = string.Empty;
            using (var form = new GeneralCodeListForm { UNDER_DEVELOPMENT = "1", FUNCTION_CLASS = isAllGeneralCode ? "00" : "03" })
            {
                //開発符号検索画面がOKで開発符号が同じかどうか
                if (form.ShowDialog(this) == DialogResult.OK && this.GeneralCodeComboBox.Text != form.GENERAL_CODE)
                {
                    if (isAllGeneralCode || form.PERMIT_FLG == 1)
                    {
                        //開発符号セット
                        this.SetGeneralCode(form.GENERAL_CODE);
                        SearchSchedule();
                        this.TestCarCalendarGrid.VerticalScrollBarOffset = 0;
                        selectGeneralCode = form.GENERAL_CODE;
                        //Append Start 2022/03/10 杉浦
                        this.setCarGroup = form.CAR_GROUP;
                        //Append End 2022/03/10 杉浦
                    }
                    else
                    {
                        Messenger.Info(Resources.KKM01002);
                    }
                }
            }
        }

        /// <summary>
        /// 開発符号セット
        /// </summary>
        /// <param name="generalCode">開発符号</param>
        private void SetGeneralCode(string generalCode)
        {
            //開発符号があるかどうか
            if (string.IsNullOrWhiteSpace(generalCode) == true)
            {
                return;

            }

            //開発符号をセット
            FormControlUtil.SetComboBoxItem(this.GeneralCodeComboBox, new[] { new GeneralCodeSearchOutModel { GENERAL_CODE = generalCode } }, false);
            this.GeneralCodeComboBox.SelectedIndex = 0;

            //仮予約注意喚起のラジオボタンを未選択に変更
            this.ReservationAlertOnRadioButton.Checked = false;
            this.ReservationAlertOffRadioButton.Checked = false;

            //開発符号初期化中ON
            this.IsGeneralCodeInit = true;

            //注意喚起取得
            var tyuuiKanki = this.GetTestCarReminder(generalCode);
            if (tyuuiKanki != null)
            {
                //注意喚起をセット
                FormControlUtil.SetRadioButtonValue(this.ReservationAlertPanel, tyuuiKanki.ALERT_FLAG);

            }
            else
            {
                //データがない場合は注意喚起はOFF
                this.ReservationAlertOffRadioButton.Checked = true;

            }

            //開発符号初期化中OFF
            this.IsGeneralCodeInit = false;

        }
        #endregion

        #region ステータスのラジオボタン変更
        /// <summary>
        /// ステータスのラジオボタン変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // バインド中は終了
            if (this.IsBind)
            {
                return;
            }

            //選択されている場合は検索
            if ((sender as RadioButton).Checked == true)
            {
                if (FormControlUtil.GetRadioButtonValue(this.StatusPanel) != Const.Youbou)
                {
                    this.TruckScheduleButton.Visible = false;
                    this.TruckScheduleCheckBox.Visible = false;
                }
                else
                {
                    this.TruckScheduleButton.Visible = true;
                    this.TruckScheduleCheckBox.Visible = true;
                }

                // 簡易入力ボタン
                this.TestCarUseHistoryInputFormButton.Visible 
                    = this.TestCarHistoryCompleteInputFormButton.Visible 
                    = FormControlUtil.GetRadioButtonValue(this.StatusPanel) == Const.Kettei;

                if (this.IsFunctionEnable(FunctionID.Truck) == false)
                {
                    this.TruckScheduleButton.Visible = false;
                    this.TruckScheduleCheckBox.Visible = false;
                }

                // 一番上に表示されているスケジュール項目の退避
                var item = this.gridUtil.GetFirstDispItem();

                DateTime left = new DateTime();
                if (item != null)
                {
                    //左側へ表示されている日付の保持を行う
                    this.TestCarCalendarGrid.ClearAll();
                    this.TestCarCalendarGrid.ColumnHeader.ClearAll();
                    this.TestCarCalendarGrid.PerformRender();
                    if (this.TestCarCalendarGrid.FirstDisplayedCellPosition.IsEmpty == false)
                    {
                        left = this.TestCarCalendarGrid.FirstDisplayedCellPosition.Date;
                    }
                }

                //スケジュール一覧設定
                this.SetScheduleList();

                if (item != null)
                {
                    // スクロールバーの調整
                    this.gridUtil.SetScheduleRowHeaderFirst(item.ID);

                    //左側へ表示する日付の再設定
                    this.gridUtil.SetScheduleMostDayFirst(left);
                }

            }

            //要望案コピーボタン
            this.RequestCopyButton.Enabled = !this.StatusRequestRadioButton.Checked;

            //一括本予約ボタン
            this.BulkReservationButton.Enabled = this.StatusFinalRadioButton.Checked;

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
            SearchSchedule();
        }

        /// <summary>
        /// 検索実行。
        /// </summary>
        /// <remarks>
        /// 設定された検索条件を元に検索を実行します。
        /// </remarks>
        private void SearchSchedule()
        {
            this.gridUtil.ResetFilter();
            this.SetScheduleList(isMonthFirst: true);
            this.ActiveControl = this.TestCarCalendarGrid;
        }

        #endregion

        #region 条件登録ボタンクリック
        /// <summary>
        /// 条件登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteEntryButton_Click(object sender, EventArgs e)
        {
            //スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;

            }

            //お気に入りが上限件数まで登録済かどうか
            if (this.GetFavoriteList().Count() >= Const.FavoriteEntryMax)
            {
                Messenger.Warn(Resources.KKM00016);
                return;

            }

            var statuskbn = this.StatusAdjustmentRadioButton.Checked ? "2" : "1";
            statuskbn = this.StatusFinalRadioButton.Checked ? "3" : statuskbn;

            var favorite = new TestCarFavoriteItemModel
            {
                //開発符号
                GENERAL_CODE = this.GeneralCodeComboBox.Text,
                //ステータス区分(1:使用部署要望案, 2:SJSB調整案, 3:最終調整結果)
                STATUS_KBN = statuskbn,
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

            //開発符号
            FormControlUtil.ClearComboBoxDataSource(this.GeneralCodeComboBox);

            //基準月
            this.MonthDateTimePicker.Value = DateTimeUtil.GetMostMondayFirst();

            // 項目ステータス
            this.StatusOpenCheckBox.Checked = true;
            this.StatusCloseCheckBox.Checked = false;
        }
        #endregion

        #region 仮予約注意喚起のラジオボタン変更
        /// <summary>
        /// 仮予約注意喚起のラジオボタン変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservationAlertRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //開発符号初期化中か仮予約注意喚起が非表示か開発符号未選択なら終了
            if (this.IsGeneralCodeInit == true || this.ReservationAlertPanel.Visible == false || this.GeneralCodeComboBox.SelectedIndex < 0)
            {
                return;

            }

            //選択されている場合は更新
            if ((sender as RadioButton).Checked == true)
            {
                //注意喚起の更新
                this.UpdateCallAlert();

            }

        }
        #endregion

        #region 要望案コピーボタンクリック
        /// <summary>
        /// 要望案コピーボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RequestCopyButton_Click(object sender, EventArgs e)
        {
            //スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;

            }

            using (var form = new TestCarScheduleCopyForm { GeneralCode = this.TestCarScheduleSearchCond.GENERAL_CODE, TargetStatus = this.TestCarScheduleSearchCond.試験車日程種別 })
            {
                //OKの場合は再検索
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //スケジュール一覧設定
                    this.SetScheduleList();

                }

            }

        }
        #endregion

        #region 一括本予約ボタンクリック
        /// <summary>
        /// 一括本予約ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BulkReservationButton_Click(object sender, EventArgs e)
        {
            //スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;

            }

            using (var form = new TestCarScheduleReserveForm { GeneralCode = this.TestCarScheduleSearchCond.GENERAL_CODE })
            {
                //OKの場合は再検索
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //スケジュール一覧設定
                    this.SetScheduleList();

                }

            }

        }
        #endregion

        #region 基準月変更
        /// <summary>
        /// 基準月変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            //日付を選択されているかどうか
            var value = this.MonthDateTimePicker.Value;
            if (value != null)
            {
                var day = (DateTime)value;
                var baseDay = new DateTime(day.Year, day.Month, 1);
                var startDay = baseDay.AddMonths(-1);

                var calendarStartDay = this.TestCarCalendarGrid.FirstDateInView;

                //ラベルの表示変更
                this.MonthPrevLabel.Text = DateTimeUtil.ConvertMonthString(startDay);
                this.MonthNextLabel.Text = DateTimeUtil.ConvertMonthString(baseDay.AddMonths(1));

            }

        }
        #endregion

        #region スケジュール設定
        /// <summary>
        /// スケジュール一覧設定
        /// </summary>
        private void SetScheduleList()
        {
            this.SetScheduleList(null, false, true);
        }
        /// <summary>
        /// スケジュール一覧設定
        /// </summary>
        /// <param name="firstID">先頭ID</param>
        /// <param name="isMonthFirst">基準月先頭可否</param>
        /// <param name="isNewSearch">初回検索フラグ</param>
        private void SetScheduleList(long? firstID = null, bool isMonthFirst = false, bool isNewSearch = true)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //スケジュール検索のチェック
                if (this.IsSearchSchedule() == false)
                {
                    return;

                }

                //スケジュール項目取得
                this.ScheduleItemList = this.GetScheduleItemList();
                
                var date = this.MonthDateTimePicker.Value.Date;
                if (isMonthFirst == true)
                {
                    //基準月先頭指定があった場合のみ、カレンダーの表示期間を変更
                    //※現在カレンダーの表示期間はそのまま検索条件となっているため、SetScheduleの前に実行する必要あり。
                    var start = new DateTime(date.Year, date.Month, 1).AddMonths(-1);
                    this.gridUtil.SetCalendarViewPeriod(start);
                }

                //スケジュール設定
                this.SetSchedule(isNewSearch);

                //基準月先頭可否
                if (isMonthFirst == true)
                {
                    //もし基準月がカレンダー表示範囲内に無い場合はdateで再度スケジュール設定
                    if ((this.TestCarCalendarGrid.FirstDateInView <= date && date <= this.TestCarCalendarGrid.LastDateInView) == false)
                    {
                        this.gridUtil.SetCalendarViewPeriod(new DateTime(date.Year, date.Month, 1));
                        this.SetSchedule();
                    }

                    //スケジュールの先頭を基準月に設定
                    this.gridUtil.SetScheduleMostDayFirst(date);
                }
            });

        }

        /// <summary>
        /// スケジュール設定
        /// </summary>
        /// <param name="start">開始日</param>
        /// <param name="end">終了日</param>
        /// <remarks>カレンダーグリッドから日付の範囲を取得するので引数は未使用</remarks>
        private void SetSchedule(DateTime start, DateTime end)
        {
            //スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;

            }

            //スケジュール設定
            this.SetSchedule();

            //スケジュールの先頭を設定
            var date = this.MonthDateTimePicker.Value.Date;
            this.gridUtil.SetScheduleMostDayFirst(start <= date && date <= end ? date : start);

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
            var isUpdate = (this.StatusRequestRadioButton.Checked == true && this.UserAuthority.UPDATE_FLG == '1') || (this.StatusRequestRadioButton.Checked == false && isManagement == true);

            Func<TestCarScheduleModel, string> getSubTitle =
                schedule => string.Format("{0}{1}", (schedule.予約種別 == Const.Kariyoyaku ? Const.Kari : ""), schedule.DESCRIPTION);
            //Update Start 2023/09/11 杉浦 仮予約者が本予約の編集を不可とするよう修正
            //Func<TestCarScheduleModel, bool> isEdit =
            //    schedule => (this.gridUtil.CalendarGridConfig.ReadOnlyRowCount != 0 && schedule.PARALLEL_INDEX_GROUP >= this.gridUtil.CalendarGridConfig.ReadOnlyRowCount) ? false : isManagement == true ? isUpdate : isUpdate == true && schedule.INPUT_PERSONEL_ID == SessionDto.UserId;
            Func<TestCarScheduleModel, bool> isEdit =
                  schedule => (this.gridUtil.CalendarGridConfig.ReadOnlyRowCount != 0 && schedule.PARALLEL_INDEX_GROUP >= this.gridUtil.CalendarGridConfig.ReadOnlyRowCount) ? false : isManagement == true ? isUpdate : isUpdate == true && (!schedule.本予約済_FLG && (schedule.INPUT_PERSONEL_ID == SessionDto.UserId || (schedule.予約種別 == Const.Kariyoyaku && schedule.予約者_ID == SessionDto.UserId)));
            //Update End 2023/09/11 杉浦 仮予約者が本予約の編集を不可とするよう修正
            Func<TestCarScheduleModel, bool> isDelete =
                schedule => schedule.試験車日程種別 == Const.Kettei && schedule.INPUT_PERSONEL_ID == SessionDto.UserId;

            //検索結果文言
            if (isNewSearch)
            {
                this.MessageLabel.Text = (this.ScheduleItemList == null || this.ScheduleItemList.Any() == false) ? Resources.KKM00005 : Resources.KKM00047;
            }

            //カレンダーのコーナーヘッダー
            this.gridUtil.CornerHeaderText = this.GetCarManager();

            //スケジュール編集可否
            this.gridUtil.CalendarGridConfig.IsScheduleEdit = isUpdate;

            //スケジュール取得
            var scheduleList = this.GetScheduleList();

            #region 最大行数以上のデータについては仮登録の下へまとめて表示する。
            this.gridUtil.CalendarGridConfig.ReadOnlyRowCount = 0;
            if (FormControlUtil.GetRadioButtonValue(this.StatusPanel) != Const.Kettei)
            {
                var list = scheduleList.ToList();
                if (list.Count() > 0)
                {
                    this.gridUtil.CalendarGridConfig.ReadOnlyRowCount = list.Max(x => x.PARALLEL_INDEX_GROUP).Value + Const.ScheduleItemRowMax;
                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (list[i].試験車日程種別 == Const.Kettei) //最終調整結果については全て読み取り専用行以降とする。
                        {
                            list[i].PARALLEL_INDEX_GROUP = list[i].PARALLEL_INDEX_GROUP + this.gridUtil.CalendarGridConfig.ReadOnlyRowCount;
                        }
                    }
                }
            }
            #endregion

            //カレンダーにデータバインド
            this.gridUtil.Bind(this.ScheduleItemList, scheduleList, x => x.ID, y => y.CATEGORY_ID.Value,
                x => new ScheduleItemModel<TestCarScheduleItemModel>(x.ID, x.GENERAL_CODE, x.CATEGORY, x.PARALLEL_INDEX_GROUP.Value, x.SORT_NO, x.管理票番号, x),
                y => new ScheduleModel<TestCarScheduleModel>(y.ID, y.CATEGORY_ID.Value, y.PARALLEL_INDEX_GROUP.Value, getSubTitle(y), y.START_DATE, y.END_DATE, y.INPUT_DATETIME, y.SYMBOL, (y.完了日 != null), isEdit(y), isDelete(y), y, y.予約種別));

        }
        #endregion

        #region スケジュール検索のチェック
        /// <summary>
        /// スケジュール検索のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchSchedule()
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            return true;

        }
        #endregion

        #region 注意喚起の更新
        /// <summary>
        /// 注意喚起の更新
        /// </summary>
        private void UpdateCallAlert()
        {
            var list = new[]
            {
                new TestCarReminderSearchOutModel
                {
                    //開発符号
                    GENERAL_CODE = this.GeneralCodeComboBox.Text,

                    //注意喚起
                    ALERT_FLAG = int.Parse(FormControlUtil.GetRadioButtonValue(this.ReservationAlertPanel))

                }

            };

            //レスポンスが取得できたかどうか
            var res = HttpUtil.PutResponse(ControllerType.TestCarReminder, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //更新メッセージ
                Messenger.Info(Resources.KKM00002);

            }

        }
        #endregion

        #region スケジュール項目のイベント
        /// <summary>
        /// 作業履歴(試験車)表示
        /// </summary>
        /// <param name="item">スケジュール項目</param>
        private void ShowHistoryScheduleItem(ScheduleItemModel<TestCarScheduleItemModel> item)
        {
            //スケジュール項目のチェック
            if (this.IsEntryScheduleItem(ScheduleItemEditType.Update, item) == false)
            {
                return;

            }

            //作業履歴画面表示
            new FormUtil(new TestCarHistoryForm { ScheduleItem = item.ScheduleItem, UserAuthority = this.UserAuthority, Reload = SetScheduleList, IsItemNameEdit = !carManagerModelList.Any(x => x.CATEGORY_ID == item.ID.ToString()) }).SingleFormShow(this);
        }

        /// <summary>
        /// スケジュール項目詳細画面表示
        /// </summary>
        /// <param name="item">スケジュール項目</param>
        /// <param name="type">編集モード</param>
        private void ShowScheduleItemDetailForm(ScheduleItemModel<TestCarScheduleItemModel> item, ScheduleItemEditType type)
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
                var scheduleItem = new TestCarScheduleItemModel
                {
                    //開発符号
                    GENERAL_CODE = this.TestCarScheduleSearchCond.GENERAL_CODE,

                    //Append Start 2022/03/10 杉浦
                    //車系
                    車系 = this.setCarGroup,
                    //Append End 2022/03/10 杉浦

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

            }else
            {
                item.ScheduleItem.車系 = this.setCarGroup;
            }

            using (var form = new ScheduleItemDetailForm<TestCarScheduleItemModel, TestCarScheduleModel>()
            {
                FormSubTitle = "試験車",
                FunctionId = FunctionID.TestCar,
                Item = item
            })
            {
                //OKの場合は再検索
                if (form.ShowDialog(this) == DialogResult.OK || form.IsReload)
                {
                    //スケジュール一覧設定
                    this.SetScheduleList();
                }

            }

        }

        /// <summary>
        /// 項目移動フォーム表示
        /// </summary>
        /// <param name="item"></param>
        private void ShowScheduleItemMoveForm(ScheduleItemModel<TestCarScheduleItemModel> item)
        {
            if (this.IsEntryScheduleItem(ScheduleItemEditType.Update, item) == false)
            {
                return;
            }

            using (var form = new ScheduleItemGeneralCodeMoveForm<TestCarScheduleItemModel>() { FormSubTitle = "試験車", ScheduleItem = item, FormSubType = ScheduleItemType.TestCar })
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
        private void DeleteScheduleItem(ScheduleItemModel<TestCarScheduleItemModel> item)
        {
            //削除可否を問い合わせ
            if (Messenger.Confirm(Resources.KKM00007) != DialogResult.Yes)
            {
                return;
            }

            //スケジュール項目のチェック
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
            var res = HttpUtil.DeleteResponse(ControllerType.TestCarScheduleItem, list);
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
        private void UpdateScheduleItemSort(ScheduleItemModel<TestCarScheduleItemModel> sourceItem, ScheduleItemModel<TestCarScheduleItemModel> destItem)
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
            var res = HttpUtil.PutResponse(ControllerType.TestCarScheduleItem, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //更新メッセージ
                this.MessageLabel.Text = Resources.KKM00002;

                //スケジュール一覧設定
                this.SetScheduleList(sourceItem.ID, isNewSearch: false);

            }

        }
        #endregion

        #region スケジュール項目のチェック
        /// <summary>
        /// スケジュール項目のチェック
        /// </summary>
        /// <param name="item">スケジュール項目</param>
        /// <returns>登録可否</returns>
        private bool IsEntryScheduleItem(ScheduleItemEditType type, ScheduleItemModel<TestCarScheduleItemModel> item)
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
                    var list = this.GetScheduleItemList(new TestCarScheduleSearchModel { ID = item.ID, GENERAL_CODE = item.GeneralCode });
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
            var cond = new TestCarScheduleSearchModel
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
        private void ShowScheduleDetailForm(ScheduleModel<TestCarScheduleModel> schedule, ScheduleEditType type)
        {
            //編集モード
            schedule.ScheduleEdit = type;

            var checkSameLine = (type == ScheduleEditType.Update) ? false : true;
            //スケジュールのチェック
            //Update Start 2022/03/08 杉浦 不具合修正
            //if (this.IsEntrySchedule(schedule, checkSameLine) == false)
            if (this.IsEntrySchedule(schedule, false, checkSameLine) == false)
            //Update End 2022/03/08 杉浦 不具合修正
            {
                //スケジュール一覧設定
                this.SetScheduleList(schedule.CategoryID);
                return;

            }

            //編集モードが追加の場合はスケジュールの初期化
            if (type == ScheduleEditType.Insert)
            {
                schedule.Schedule = new TestCarScheduleModel
                {
                    //開発符号
                    GENERAL_CODE = this.TestCarScheduleSearchCond.GENERAL_CODE,

                    //シンボル
                    SYMBOL = SymbolDefault,

                    //カテゴリーーID
                    CATEGORY_ID = schedule.CategoryID,

                    //試験車日程種別
                    試験車日程種別 = this.TestCarScheduleSearchCond.試験車日程種別,

                    //行番号
                    PARALLEL_INDEX_GROUP = schedule.RowNo,

                    //開始日
                    START_DATE = schedule.StartDate,

                    //終了日
                    END_DATE = schedule.EndDate

                };

                if (this.UserAuthority.MANAGEMENT_FLG == '0')
                {
                    var generalCode = this.TestCarScheduleSearchCond.GENERAL_CODE;

                    var callAlert = this.GetTestCarReminder(generalCode);
                    if (callAlert?.ALERT_FLAG == 1)
                    {
                        using (var alertForm = new ReservationAlertForm())
                        {
                            alertForm.ShowDialog(this);
                        }
                    }
                }
            }
            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
            var kanriNo = this.ScheduleItemList.Where(x => x.ID == schedule.CategoryID).Select(x => x.管理票番号).ToList();
            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する

            //Update Start 2022/02/03 杉浦 試験車日程の車も登録する
            //using (var form = new TestCarScheduleDetailForm { Schedule = schedule, UserAuthority = this.UserAuthority })
            using (var form = new TestCarScheduleDetailForm { Schedule = schedule, UserAuthority = this.UserAuthority, KanriNo = kanriNo[0] })
            //Update End 2022/02/03 杉浦 試験車日程の車も登録する
            {
                //OKの場合は再検索
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // 更新・削除メッセージ
                    if (!string.IsNullOrWhiteSpace(form.ReturnMessage)) this.MessageLabel.Text = form.ReturnMessage;

                    //スケジュール一覧設定
                    this.SetScheduleList(schedule.CategoryID, false, false);

                    if (form.Schedule.ScheduleEdit != ScheduleEditType.Update)
                    {
                        this.gridUtil.UndoRedoManager.Do(form.Schedule);
                    }

                    //注意喚起フォームがある場合は開き直す                    
                    var errorFormList = new List<string>();
                    foreach (var f in Application.OpenForms.Cast<Form>().Where(f => f is TestCarHistoryErrorForm))
                    {
                        errorFormList.Add(f.Name);
                    }
                    foreach (var name in errorFormList)
                    {
                        Application.OpenForms[name].Close();
                    }
                    if (errorFormList.Any()) { ShowTestCarHistoryErrorForm(); }

                    if (schedule.ScheduleEdit == ScheduleEditType.Insert && schedule.Schedule.試験車日程種別 == Const.Youbou &&
                        this.TruckForm == null && this.IsFunctionEnable(FunctionID.Truck))
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
        }

        /// <summary>
        /// スケジュール日付範囲変更
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        private void RangeChangedSchedule(ScheduleModel<TestCarScheduleModel> schedule)
        {
            //編集モード
            schedule.ScheduleEdit = ScheduleEditType.Update;

            //スケジュールのチェック
            //Update Start 2022/03/08 杉浦 不具合修正
            //if (this.IsEntrySchedule(schedule) == false)
            if (this.IsEntrySchedule(schedule, true) == false)
            //Update End 2022/03/08 杉浦 不具合修正
            {
                //スケジュール一覧設定
                this.SetScheduleList(schedule.CategoryID);
                return;

            }

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
            var res = HttpUtil.PutResponse(ControllerType.TestCarSchedule, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //更新メッセージ
                this.MessageLabel.Text = string.Format(Resources.KKM01019, getYMDH(schedule.StartDate), getYMDH(schedule.EndDate), Resources.KKM00002);

                //スケジュール一覧設定
                this.SetScheduleList(schedule.CategoryID, false, false);

            }

        }

        /// <summary>
        /// スケジュール削除
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        /// <param name="isConfirm">確認可否</param>
        private void DeleteSchedule(ScheduleModel<TestCarScheduleModel> schedule, bool isConfirm)
        {
            //削除しない場合は終了
            if (isConfirm == true && Messenger.Confirm(Resources.KKM00007) != DialogResult.Yes)
            {
                return;

            }

            //編集モード
            schedule.ScheduleEdit = ScheduleEditType.Delete;

            //スケジュールのチェック
            if (this.IsEntrySchedule(schedule) == false)
            {
                //スケジュール一覧設定
                this.SetScheduleList(schedule.CategoryID);
                return;

            }

            var list = new[] { schedule.Schedule };

            //レスポンスが取得できたかどうか
            var res = HttpUtil.DeleteResponse(ControllerType.TestCarSchedule, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //削除メッセージ
                this.MessageLabel.Text = string.Format(Resources.KKM01019, getYMDH(schedule.StartDate), getYMDH(schedule.EndDate), Resources.KKM00003);

                //スケジュール一覧設定
                this.SetScheduleList(schedule.CategoryID, false, false);
                
                this.gridUtil.UndoRedoManager.Do(schedule);
            }

        }

        /// <summary>
        /// スケジュール貼り付け
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        private void PasteSchedule(ScheduleModel<TestCarScheduleModel> copySchedule, ScheduleModel<TestCarScheduleModel> schedule)
        {
            //編集モード
            schedule.ScheduleEdit = ScheduleEditType.Paste;

            //スケジュールのチェック
            //Update Start 2022/03/08 杉浦 不具合修正
            //if (this.IsEntrySchedule(schedule) == false)
            if (this.IsEntrySchedule(schedule, true) == false)
            //Update End 2022/03/08 杉浦 不具合修正
            {
                //スケジュール一覧設定
                this.SetScheduleList(schedule.CategoryID);
                return;

            }

            //コピー＆ペースト時も、コピー元の作成者を編集した人へ更新する。
            copySchedule.Schedule.INPUT_PERSONEL_ID = SessionDto.UserId;
            HttpUtil.PutResponse(ControllerType.TestCarSchedule, new[] { copySchedule.Schedule });

            //コピー元のスケジュール情報を書き換え
            var data = new TestCarScheduleModel
            {
                //開発符号
                GENERAL_CODE = schedule.Schedule.GENERAL_CODE,

                //予約種別
                予約種別 = schedule.Schedule.予約種別,

                //区分
                SYMBOL = schedule.Schedule.SYMBOL,

                //スケジュール名
                DESCRIPTION = schedule.Schedule.DESCRIPTION,

                //カテゴリーーID
                CATEGORY_ID = schedule.CategoryID,

                //試験車日程種別
                試験車日程種別 = schedule.Schedule.試験車日程種別,

                //行番号
                PARALLEL_INDEX_GROUP = schedule.RowNo,

                //開始日
                START_DATE = schedule.StartDate,

                //終了日
                END_DATE = schedule.EndDate,

                //設定者ID
                設定者_ID = schedule.Schedule.設定者_ID
            };

            //ユーザー情報設定
            data.SetUserInfo();

            //レスポンスが取得できたかどうか
            var res = HttpUtil.PostResponse(ControllerType.TestCarSchedule, new[] { data });
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //登録後メッセージ
                this.MessageLabel.Text = string.Format(Resources.KKM01019, getYMDH(schedule.StartDate), getYMDH(schedule.EndDate), Resources.KKM00002);

                //スケジュール一覧設定
                this.SetScheduleList(schedule.CategoryID, false, false);

                schedule.Schedule = res.Results.OfType<TestCarScheduleModel>().FirstOrDefault();
                this.gridUtil.UndoRedoManager.Do(schedule);
            }

        }
        #endregion

        #region スケジュールのチェック
        /// <summary>
        /// スケジュールのチェック
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        /// <param name="plusCheck">他のスケジュールとかぶっているかのチェックをする場合はtrue</param>
        /// <param name="checkSameLine">同じ行のスケジュールかチェックを行う場合はTrue</param>
        /// <returns>登録可否</returns>
        //Update Start 2022/03/08 杉浦 不具合修正
        //private bool IsEntrySchedule(ScheduleModel<TestCarScheduleModel> schedule, bool checkSameLine = true)
        private bool IsEntrySchedule(ScheduleModel<TestCarScheduleModel> schedule, bool plusCheck = false, bool checkSameLine = true)
        //Update End 2022/03/08 杉浦 不具合修正
        {
            //スケジュール項目が存在しているかどうか
            var itemList = this.GetScheduleItemList(new TestCarScheduleSearchModel { GENERAL_CODE = schedule.GeneralCode, ID = schedule.CategoryID });
            if (itemList == null || itemList.Any() == false)
            {
                //存在していない場合はエラー
                Messenger.Warn(Resources.KKM00021);
                return false;

            }

            //スケジュール編集区分ごとの分岐
            switch (schedule.ScheduleEdit)
            {
                //更新
                //削除
                case ScheduleEditType.Update:
                case ScheduleEditType.Delete:
                    //スケジュールが存在しているかどうか
                    var scheduleList = this.GetScheduleList(new TestCarScheduleSearchModel { ID = schedule.ID });
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
            switch (schedule.ScheduleEdit)
            {
                //追加
                //更新
                //貼り付け
                case ScheduleEditType.Insert:
                case ScheduleEditType.Update:
                case ScheduleEditType.Paste:
                    //検索条件
                    var cond = new TestCarScheduleSearchModel
                    {
                        //カテゴリーID
                        CATEGORY_ID = schedule.CategoryID,

                        //試験車日程種別
                        試験車日程種別 = this.TestCarScheduleSearchCond.試験車日程種別,

                        //期間(From)
                        START_DATE = schedule.StartDate?.Date,

                        //期間(To)
                        END_DATE = schedule.EndDate?.Date,

                        //行番号
                        PARALLEL_INDEX_GROUP = schedule.RowNo

                    };

                    //スケジュールで重複した期間が存在する場合はエラー
                    if (this.GetScheduleList(cond).Any(x => x.ID != schedule.ID) == true && checkSameLine)
                    {
                        Messenger.Warn(Resources.KKM03017);
                        return false;

                    }
                    //Delete Start 2022/03/16 杉浦 チェック削除
                    //Append Start 2022/03/08 杉浦 不具合修正
                    //var itemList2 = itemList.ToList();
                    //if (plusCheck && !string.IsNullOrEmpty(itemList2[0].管理票番号))
                    ////Append End 2022/03/08 杉浦 不具合修正
                    //{
                    //    //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
                    //    //検索条件
                    //    var cond2 = new AllScheduleSearchModel
                    //    {
                    //        //管理票番号
                    //        管理票番号 = itemList2[0].管理票番号,

                    //        //期間(From)
                    //        START_DATE = schedule.StartDate?.Date,

                    //        //期間(To)
                    //        END_DATE = schedule.EndDate?.Date,

                    //    };

                    //    //スケジュールで重複した期間が存在する場合はエラー
                    //    var list = this.GetAllScheduleList(cond2).Where(x => x.ID != schedule.ID).ToList();
                    //    if (list.Any() == true)
                    //    {
                    //        var start = schedule.StartDate.Value;
                    //        var end = schedule.EndDate.Value;
                    //        var scheduleType = list.Where(x => (x.START_DATE.Value <= start && start <= x.END_DATE.Value) ||
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
                    //            //Messenger.Warn(("同時刻に" + text + "に既に登録がある為、登録出来ません。"));
                    //            Messenger.Warn((text + "の同時刻に既に登録がある為、登録出来ません。"));
                    //            //Update End 2022/03/08 杉浦 不具合修正
                    //            return false;
                    //        }

                    //    }
                    //    //Append End 2022/02/03 杉浦 試験車日程の車も登録する
                    //    //Append Start 2022/03/08 杉浦 不具合修正
                    //}
                    ////Append End 2022/03/08 杉浦 不具合修正
                    //Delete End 2022/03/16 杉浦 チェック削除
                    break;

            }

            return true;

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
                CLASS_DATA = Const.FavoriteTestCar

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
        private TestCarFavoriteSearchOutModel GetFavorite(long id)
        {
            //パラメータ設定
            var cond = new TestCarFavoriteSearchInModel { ID = id };

            //APIで取得
            var res = HttpUtil.GetResponse<TestCarFavoriteSearchInModel, TestCarFavoriteSearchOutModel>(ControllerType.TestCarFavorite, cond);

            //レスポンスが取得できたかどうか
            var list = new List<TestCarFavoriteSearchOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list.FirstOrDefault();

        }

        /// <summary>
        /// お気に入り(作業履歴)の取得
        /// </summary>
        private IEnumerable<HistoryFavoriteSearchOutModel> GetHistoryFavoriteList()
        {
            //パラメータ設定
            var cond = new HistoryFavoriteSearchInModel
            {
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                // 履歴区分
                HISTORY_CODE = Const.HistoryTestCar

            };

            //APIで取得
            var res = HttpUtil.GetResponse<HistoryFavoriteSearchInModel, HistoryFavoriteSearchOutModel>(ControllerType.HistoryFavorite, cond);

            //レスポンスが取得できたかどうか
            var list = new List<HistoryFavoriteSearchOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// スケジュール項目取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<TestCarScheduleItemModel> GetScheduleItemList()
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

            var cond = new TestCarScheduleSearchModel
            {
                //開発符号
                GENERAL_CODE = this.GeneralCodeComboBox.Text,

                //Openフラグ
                OPEN_FLG = openFlg

            };

            //試験車スケジュール検索条件
            this.TestCarScheduleSearchCond = cond;

            //返却
            return this.GetScheduleItemList(cond);

        }

        /// <summary>
        /// スケジュール項目取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private IEnumerable<TestCarScheduleItemModel> GetScheduleItemList(TestCarScheduleSearchModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<TestCarScheduleSearchModel, TestCarScheduleItemModel>(ControllerType.TestCarScheduleItem, cond);

            //レスポンスが取得できたかどうか
            var list = new List<TestCarScheduleItemModel>();
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
        private IEnumerable<TestCarScheduleModel> GetScheduleList()
        {
            //試験車スケジュール検索条件
            var cond = this.TestCarScheduleSearchCond;

            //期間(From)
            cond.START_DATE = this.TestCarCalendarGrid.FirstDateInView;

            //期間(To)
            cond.END_DATE = this.TestCarCalendarGrid.LastDateInView;

            //試験車日程種別
            cond.試験車日程種別 = FormControlUtil.GetRadioButtonValue(this.StatusPanel);

            //決定以外の場合、本予約も表示する
            cond.IsGetKettei = (cond.試験車日程種別 != Const.Kettei);

            //返却
            return this.GetScheduleList(cond);

        }

        /// <summary>
        /// スケジュール取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private IEnumerable<TestCarScheduleModel> GetScheduleList(TestCarScheduleSearchModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<TestCarScheduleSearchModel, TestCarScheduleModel>(ControllerType.TestCarSchedule, cond);

            //レスポンスが取得できたかどうか
            var list = new List<TestCarScheduleModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            //Append Start 2023/09/11 杉浦 仮予約者が本予約の編集を不可とするよう修正
            foreach (var item in list)
            {
                if (item.試験車日程種別 == Const.Youbou && item.予約種別 == Const.Kariyoyaku)
                {
                    item.本予約済_FLG = list.Any(x => x.CATEGORY_ID == item.CATEGORY_ID && x.START_DATE == item.START_DATE && x.END_DATE == item.END_DATE && x.ID != item.ID);
                }
            }
            //Append End 2023/09/11 杉浦 仮予約者が本予約の編集を不可とするよう修正

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

        /// <summary>
        /// 注意喚起取得
        /// </summary>
        /// <param name="generalCode">開発符号</param>
        /// <returns></returns>
        private TestCarReminderSearchOutModel GetTestCarReminder(string generalCode)
        {
            // APIで取得
            var res = HttpUtil.GetResponse<TestCarReminderSearchInModel, TestCarReminderSearchOutModel>(ControllerType.TestCarReminder, new TestCarReminderSearchInModel { GENERAL_CODE = generalCode });

            //レスポンスが取得できたかどうか
            var list = new List<TestCarReminderSearchOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            //返却
            return list.FirstOrDefault();

        }

        /// <summary>
        /// 車両管理担当取得
        /// </summary>
        /// <returns></returns>
        private string GetCarManager()
        {
            var res = HttpUtil.GetResponse<CarManagerSearchModel, CarManagerModel>(ControllerType.CarManager,
                new CarManagerSearchModel { GENERAL_CODE = this.TestCarScheduleSearchCond.GENERAL_CODE, FUNCTION_ID = (int)FunctionID.TestCar });

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

        /// <summary>
        /// Excel出力ボタン押下処理。
        /// </summary>
        /// <remarks>
        /// Excel出力ウィンドウを呼び出します。
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

            using (var form = new CalendarPrintExcelForm(this.ScheduleItemList.ToList(), this.TestCarScheduleSearchCond, this.gridUtil.CalendarSetting.CalendarMode))
            {
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// フォームShown処理。
        /// </summary>
        /// <remarks>
        /// 開発符号選択フォームを起動します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarScheduleForm_Shown(object sender, EventArgs e)
        {
            List<HistoryError> errorList = ShowTestCarHistoryErrorForm();

            if (this.TestCarScheduleSearchCond == null && this.FavoriteID == null && errorList.Any() == false)
            {
                ShowGeneralCodeListForm();
            }
        }

        /// <summary>
        /// 未記録一覧フォーム表示処理。
        /// </summary>
        /// <returns></returns>
        private List<HistoryError> ShowTestCarHistoryErrorForm()
        {
            var errorList = new List<HistoryError>();
            if (this.UserAuthority.MANAGEMENT_FLG != '1')
            {
                var search = new TestCarCompleteScheduleSearchModel()
                {
                    FromDate = DateTime.Today.AddMonths(-3),
                    PERSONEL_ID = SessionDto.UserId,
                    GeneralCodeFlg = this.UserAuthority.ALL_GENERAL_CODE_FLG == '1',
                    設定者_ID = SessionDto.UserId
                };

                var res = HttpUtil.GetResponse<TestCarCompleteScheduleSearchModel, TestCarCompleteScheduleModel>(
                    ControllerType.TestCarHistoryComplete, search);

                var checkList = new List<TestCarCompleteScheduleModel>();
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    checkList.AddRange(res.Results);

                    foreach (var item in checkList)
                    {
                        errorList.Add(new HistoryError()
                        {
                            CategoryId = item.CATEGORY_ID,
                            GeneralCode = item.GENERAL_CODE,
                            ScheduleId = item.ID,
                            StartDateTime = item.START_DATE,
                            CarName = item.CATEGORY,
                            StartEndDate = item.START_DATE.ToString("yy/MM/dd HH:mm") + " ～ " + item.END_DATE.ToString("yy/MM/dd HH:mm"),
                            Description = item.DESCRIPTION
                        });
                    }
                    if (errorList.Any())
                    {
                        var form = new TestCarHistoryErrorForm { HistoryErrorList = errorList, Reload = SelectSchedule };
                        form.Show();
                    }
                }
            }

            return errorList;
        }

        /// <summary>
        /// スケジュールへ遷移する
        /// </summary>
        /// <param name="result"></param>
        private void SelectSchedule(Result result)
        {
            var date = result.FirstDateTime;
            this.gridUtil.SetCalendarViewPeriod(new DateTime(date.Year, date.Month, 1));

            this.StatusOpenCheckBox.Checked = true;
            this.StatusCloseCheckBox.Checked = true;

            this.gridUtil.ResetFilter();
            this.SetGeneralCode(result.GeneralCode);
            this.SetScheduleList(isMonthFirst: false);
            this.gridUtil.SetScheduleRowHeaderFirst(result.CategoryId);

            this.gridUtil.SetScheduleMostDayFirst(date);

            this.Activate();
            this.ActiveControl = this.TestCarCalendarGrid;
        }

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
            string formName = typeof(LegendForm).Name + "_TestCarScheduleForm";
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
                this.TruckForm.SyncCalendarGrid = this.TestCarCalendarGrid;
                this.TruckForm.SyncCalendarStyle = Properties.Settings.Default.TestCarCalendarStyle;
                this.TruckScheduleCheckBox.Checked = true;
                this.TruckForm.SyncCheck = this.TruckScheduleCheckBox.Checked;
                this.TruckForm.Show(this);
                this.TruckForm.TruckCalendarGridEventAdd();
                this.ActiveControl = TestCarCalendarGrid;
            }
            else
            {
                this.TruckForm.Close();
                TruckForm_SyncFormRefreshEvent(null, null);
            }

            if (this.SearchConditionTableLayoutPanel.Visible == true)
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

            var oldFirstDateInView = this.TestCarCalendarGrid.FirstDateInView;
            this.TestCarCalendarGrid.FirstDateInView = this.TruckForm.TruckScheduleCalendarGrid.FirstDateInView;

            if (this.gridUtil.ChangeTemplateSettings(new CalendarSettings((StringCollection)sender)) || oldFirstDateInView != this.TruckForm.TruckScheduleCalendarGrid.FirstDateInView)
            {
                this.gridUtil.SetCalendarViewPeriod(new DateTime(this.TestCarCalendarGrid.FirstDateInView.Year, this.TestCarCalendarGrid.FirstDateInView.Month, 1));
                this.gridUtil.SetTemplateHeader();
                SetSchedule(this.TestCarCalendarGrid.FirstDateInView, this.TruckForm.TruckScheduleCalendarGrid.LastDateInView);
            }
            this.gridUtil.SetScheduleMostDayFirst(this.TruckForm.TruckScheduleCalendarGrid.FirstDisplayedCellPosition.Date);
            this.TestCarCalendarGrid.HorizontalScrollBarOffset = this.TruckForm.TruckScheduleCalendarGrid.HorizontalScrollBarOffset;

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
        private void RunXeyeWebBrowser(ScheduleItemModel<TestCarScheduleItemModel> item)
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
        /// 作業完了簡易入力フォーム表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarHistoryCompleteInputFormButton_Click(object sender, EventArgs e)
        {
            // スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                using (var form = new TestCarHistoryCompleteInputForm() { GeneralCode = this.GeneralCodeComboBox.Text, UserAuthority = this.UserAuthority })
                {
                    if (form.ShowDialog(this) == DialogResult.OK || form.ResultScheduleInfo != null)
                    {
                        var date = form.ResultScheduleInfo.FirstDateTime;
                        this.gridUtil.SetCalendarViewPeriod(new DateTime(date.Year, date.Month, 1));

                        this.StatusOpenCheckBox.Checked = true;
                        this.StatusCloseCheckBox.Checked = true;

                        this.gridUtil.ResetFilter();
                        this.SetGeneralCode(form.ResultScheduleInfo.GeneralCode);
                        this.SetScheduleList(isMonthFirst: false);
                        this.gridUtil.SetScheduleRowHeaderFirst(form.ResultScheduleInfo.CategoryId);

                        this.gridUtil.SetScheduleMostDayFirst(date);

                        this.ActiveControl = this.TestCarCalendarGrid;
                    }

                    if (form.ResultScheduleInfo == null && form.isRequiredReload)
                    {
                        this.SetSchedule();
                    }
                }
            });
        }

        /// <summary>
        /// 使用履歴簡易入力フォーム表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarUseHistoryInputFormButton_Click(object sender, EventArgs e)
        {
            // スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                using (var form = new TestCarUseHistoryInputForm() { GeneralCode = this.GeneralCodeComboBox.Text, UserAuthority = this.UserAuthority })
                {
                    form.ShowDialog(this);
                }
            });
        }

        /// <summary>
        /// 連絡先編集フォーム表示
        /// </summary>
        private void ShowScheduleItemContactInfoForm()
        {
            using (var form = new ScheduleItemContactInfoForm<TestCarScheduleItemModel>() {
                InfoType = ContactInfoType.All, Code = this.TestCarScheduleSearchCond.GENERAL_CODE, FunctionId = FunctionID.TestCar })
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
        private void ShowScheduleItemContactInfoForm(ScheduleItemModel<TestCarScheduleItemModel> selectScheduleItem, ScheduleItemEditType type)
        {
            if(type == ScheduleItemEditType.Insert)
            {
                var scheduleItem = new TestCarScheduleItemModel
                {
                    GENERAL_CODE = this.TestCarScheduleSearchCond.GENERAL_CODE,
                    SORT_NO = 0,
                    PARALLEL_INDEX_GROUP = 1
                };

                if(selectScheduleItem.ScheduleItem != null)
                {
                    scheduleItem.SORT_NO = selectScheduleItem.SortNo + 0.1D;
                    scheduleItem.SetUserInfo();
                }

                selectScheduleItem.ScheduleItem = scheduleItem;
            }
            selectScheduleItem.ScheduleItemEdit = type;

            using (var form = new ScheduleItemContactInfoForm<TestCarScheduleItemModel>()
            {
                InfoType = ContactInfoType.Item,
                Code = this.TestCarScheduleSearchCond.GENERAL_CODE,
                Item = selectScheduleItem,
                FunctionId = FunctionID.TestCar
            })
            {
                var ret = form.ShowDialog(this);

                if (ret == DialogResult.OK)
                {
                    SearchSchedule();
                }
            }
        }

        /// <summary>
        /// 元に戻す・やり直し処理
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private bool UndoRedo(ScheduleModel<TestCarScheduleModel> schedule)
        {
            if (schedule.ScheduleEdit == ScheduleEditType.Update)
            {
                var res = HttpUtil.PutResponse(ControllerType.TestCarSchedule, new[] { schedule.Schedule });
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    this.SetScheduleList(schedule.CategoryID, false, false);
                    return true;
                }
                return false;
            }
            else if (schedule.ScheduleEdit == ScheduleEditType.Delete)
            {
                //削除の時はinsert、insertの時はdelete。
                var res = HttpUtil.PostResponse(ControllerType.TestCarSchedule, new[] { schedule.Schedule });
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    schedule.Schedule = res.Results.OfType<TestCarScheduleModel>().FirstOrDefault();
                    this.SetScheduleList(schedule.CategoryID, false, false);
                    return true;
                }
                return false;
            }
            else if (schedule.ScheduleEdit == ScheduleEditType.Insert || schedule.ScheduleEdit == ScheduleEditType.Paste)
            {
                var list = new[] { schedule.Schedule };
                
                var res = HttpUtil.DeleteResponse(ControllerType.TestCarSchedule, list);
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    this.SetScheduleList(schedule.CategoryID, false, false);
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// フォームクローズ前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarScheduleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO : 現在エラーフォームは日程表が消えると動作しないため共に閉じる
            var errorFormList = new List<string>();
            foreach (var f in Application.OpenForms.Cast<Form>().Where(f => f is TestCarHistoryErrorForm))
            {
                errorFormList.Add(f.Name);
            }
            foreach (var name in errorFormList)
            {
                Application.OpenForms[name].Close();
            }
        }

        //Append Start 2021/05/26 杉浦 試験車日程_管理票の情報表示機能追加
        /// <summary>
        /// 試験車情報画面表示
        /// </summary>
        /// <param name="no">管理表番号</param>
        private void ShowTestCarForm(string no)
        {
            var testCar = GetCommonCarInfo(no);

            FormControlUtil.FormWait(this, () =>
            {
                new FormUtil(new UITestCar.ControlSheet.ControlSheetIssueForm { TestCar = testCar, UserAuthority = this.UserAuthority }).SingleFormShow(this);
            });
        }

        /// <summary>
        /// 車情報の取得
        /// </summary>
        /// <params name="no">管理票NO</params>
        /// <returns>TestCarCommonModel</returns>
        private TestCarCommonModel GetCommonCarInfo(string no)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<TestCarCommonSearchModel, TestCarCommonModel>
                (ControllerType.ControlSheetTestCar, new TestCarCommonSearchModel { 管理票NO = no });

            return (res.Results).FirstOrDefault();
        }
        //Append End 2021/05/26 杉浦 試験車日程_管理票の情報表示機能追加
    }
}
