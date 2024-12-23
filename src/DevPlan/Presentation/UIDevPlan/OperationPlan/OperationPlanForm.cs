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
using DevPlan.Presentation.UIDevPlan.WorkProgress;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;
using DevPlan.Presentation.Common;

namespace DevPlan.Presentation.UIDevPlan.OperationPlan
{
    /// <summary>
    /// 業務計画表
    /// </summary>
    public partial class OperationPlanForm : BaseForm
    {
        #region メンバ変数
        private const string CornerHeaderText = "項目";
        private const string StringDateFormat = "yyyy/MM";

        private const int CondHeight = 130;

        private const string FavoriteClassData = Const.FavoriteWork;

        CalendarGridUtil<WorkScheduleItemGetOutModel, WorkScheduleGetOutModel> gridUtil;

        /// <summary>お気に入りID</summary>
        private long? FavoriteID;
        /// <summary>車系</summary>
        private string CarGroup;
        /// <summary>部ID</summary>
        private string DepartmentID;
        /// <summary>課ID</summary>
        private string SectionID;
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "業務計画表"; } }

        /// <summary>お気に入りID</summary>
        public long? FAVORITE_ID { get { return FavoriteID; } set { FavoriteID = value; } }

        /// <summary>スケジュール項目リスト</summary>
        private IEnumerable<WorkScheduleItemGetOutModel> ScheduleItemList { get; set; }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OperationPlanForm()
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
        private void OperationPlanForm_Load(object sender, EventArgs e)
        {
            // 権限の取得
            this.UserAuthority = base.GetFunction(FunctionID.Plan);

            // カレンダーグリッドの初期化
            this.InitCalendarGrid();

            // 画面の初期化
            this.InitForm();

            // お気に入りから遷移時
            if (FavoriteID != null)
            {
                this.FavoriteComboBox.SelectedValue = FavoriteID;

            }

        }
        #endregion

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            var isExport = this.UserAuthority.EXPORT_FLG == '1';

            var today = DateTime.Today;

            // お気に入り
            FormControlUtil.SetComboBoxItem(this.FavoriteComboBox, GetFavoriteList());
            this.FavoriteComboBox.SelectedValueChanged += this.FavoriteComboBox_SelectedValueChanged;

            // 基準月
            this.BaseMonthDateTimePicker.Value = DateTimeUtil.GetMostMondayFirst();

            this.StartMonthLabel.Text = today.AddMonths(-1).ToString(StringDateFormat);
            this.EndMonthLabel.Text = today.AddMonths(1).ToString(StringDateFormat);

            // 所属
            this.AffiliationComboBox.DataSource = new List<ComboBoxDto>{ new ComboBoxDto()
            {
                CODE = SessionDto.DepartmentCode + "　" + SessionDto.SectionCode + "　" + SessionDto.SectionGroupCode,
                ID = SessionDto.SectionGroupID
            }};
            this.AffiliationComboBox.SelectedValue = SessionDto.SectionGroupID;
            this.DepartmentID = SessionDto.DepartmentID;
            this.SectionID = SessionDto.SectionID;

            // ステータス
            this.ClosedStartMonthDateTimePicker.Value = null;
            this.ClosedEndMonthDateTimePicker.Value = null;

            // ダウンロード
            if (!isExport)
            {
                this.DownloadButton.Visible = false;
            }
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
            var isScheduleToolTipDateTimeFormat = false;

            var map = new Dictionary<string, Action<ScheduleItemModel<WorkScheduleItemGetOutModel>>>();

            //管理権限があるかどうか
            if (isManagement == true)
            {
                map["項目追加"] = item => this.OpenScheduleItemForm(item, ScheduleItemEditType.Insert);
                map["項目編集"] = item => this.OpenScheduleItemForm(item, ScheduleItemEditType.Update);
                map["項目削除"] = this.ScheduleItemDelete;
            }

            //更新権限があるかどうか
            if (isUpdate == true)
            {
                map["行追加"] = item => this.ScheduleItemPut(item, ScheduleItemEditType.RowAdd);
                map["行削除"] = item => this.ScheduleItemPut(item, ScheduleItemEditType.RowDelete);
            }

            //管理権限があるかどうか
            if (isManagement == true)
            {
                map["コピー・移動"] = this.OpenItemCopyMoveForm;
            }

            // TODO : ■テストを行えない機能（2018年上期リリース対象外）につきaddRowHeaderColumnConfig,isScheduleToolTipDateTimeFormatはコメントアウト。

            var config = new CalendarGridConfigModel<WorkScheduleItemGetOutModel, WorkScheduleGetOutModel>(
            this.OperationPlanCalendarGrid, isManagement, isUpdate, 
            //isScheduleToolTipDateTimeFormat,
            CornerHeaderText,
            new Dictionary<string, Action>
            {
                { "項目追加", () => this.OpenScheduleItemForm(new ScheduleItemModel<WorkScheduleItemGetOutModel>(), ScheduleItemEditType.Insert) }
            },
            map,
            new Dictionary<string, Action<ScheduleModel<WorkScheduleGetOutModel>>>
            {
                { "削除", schedule => this.ScheduleDelete(schedule, true) }
            }
            //new Dictionary<string, RowHeaderModel>
            //{
            //    { "担当", new RowHeaderModel { Width = 100, MinWidth = 100, MaxWidth = 100 } }
            //},
            //this.GetRowHeaderOtherValue
            );

            //スケジュールダブルクリックテキスト
            config.ScheduleDoubleClickLabelText = "※項目ダブルクリックで進捗履歴を表示します。";

            //カレンダーグリッドの初期設定
            this.gridUtil = new CalendarGridUtil<WorkScheduleItemGetOutModel, WorkScheduleGetOutModel>(config);

            //スケジュール表示期間の変更後
            this.gridUtil.ScheduleViewPeriodChangedAfter += this.SetSchedule;

            //スケジュール項目のダブルクリック
            this.gridUtil.ScheduleRowHeaderDoubleClick += this.OpenProgressListForm;

            //スケジュール項目の並び順変更後
            this.gridUtil.ScheduleItemSortChangedAfter += (sourceItem, destItem) => this.ChangeScheduleItemSort(sourceItem, destItem);

            //スケジュールのダブルクリック
            this.gridUtil.ScheduleDoubleClick += schedule => this.OpenScheduleForm(schedule, ScheduleEditType.Update);

            //スケジュールの日付範囲変更後
            this.gridUtil.ScheduleDayRangeChangedAfter += this.SchedulePut;

            //スケジュールの空白のドラッグ後
            this.gridUtil.ScheduleEmptyDragAfter += schedule => this.OpenScheduleForm(schedule, ScheduleEditType.Insert);

            //スケジュールの空白ダブルクリック
            this.gridUtil.ScheduleEmptyDoubleClick += schedule => this.OpenScheduleForm(schedule, ScheduleEditType.Insert);

            //スケジュールの削除（deleteキー）
            this.gridUtil.ScheduleDelete += schedule => this.ScheduleDelete(schedule, false);

            ////スケジュールの貼り付け（Ctrl C→V）
            //this.gridUtil.SchedulePaste += this.SchedulePost;
        }
        #endregion

        #region お気に入り検索条件のセット
        /// <summary>
        /// お気に入り検索条件のセット
        /// </summary>
        private bool SetFavoriteCondition()
        {
            if (this.FavoriteID == null) return false;

            if (!this.FavoriteIsEnable(this.FavoriteID))
            {
                Messenger.Warn(Resources.KKM00020);

                return false;
            }

            var cond = this.GetFavoriteData();

            if (cond == null) return false;
            
            // 開発符号
            if (!string.IsNullOrWhiteSpace(cond.GENERAL_CODE))
            {
                var data = this.GetGeneralCodeData(cond.GENERAL_CODE);
                var code = (data != null && data.GENERAL_CODE != null)
                    ? data.CAR_GROUP + " " + data.GENERAL_CODE : cond.GENERAL_CODE;
                var id = cond.GENERAL_CODE;
                this.GeneralCodeComboBox.DataSource 
                    = new List<ComboBoxDto>{ new ComboBoxDto() { CODE = code, ID = id } };
                this.GeneralCodeComboBox.SelectedIndex = 0;
                this.CarGroup = data.CAR_GROUP;
            }

            // 所属（0:担当、1:所属、9:全部署）
            if (cond.CLASS_KBN != null)
            if (cond.CLASS_KBN.Equals("0") && !string.IsNullOrWhiteSpace(cond.CLASS_ID))
            {
                this.AffiliSectionGroupRadioButton.Checked = true;
                var data = GetSectionGroupData(cond.CLASS_ID);
                var code = (data != null && data.SECTION_GROUP_CODE != null)
                    ? data.DEPARTMENT_CODE + " " + data.SECTION_CODE + " " + data.SECTION_GROUP_CODE : cond.CLASS_ID;
                var id = cond.CLASS_ID;
                this.AffiliationComboBox.DataSource 
                    = new List<ComboBoxDto>{ new ComboBoxDto() { CODE = code, ID = cond.CLASS_ID } };
                this.AffiliationComboBox.SelectedValue = cond.CLASS_ID;
                this.DepartmentID = data.DEPARTMENT_ID;
                this.SectionID = data.SECTION_ID;
                }
                else if (cond.CLASS_KBN.Equals("1") && !string.IsNullOrWhiteSpace(cond.CLASS_ID))
            {
                this.AffiliSectionRadioButton.Checked = true;
                var data = GetSectionData(cond.CLASS_ID);
                var code = (data != null && data.SECTION_CODE != null)
                    ? data.DEPARTMENT_CODE + " " + data.SECTION_CODE : cond.CLASS_ID;
                var id = cond.CLASS_ID;
                this.AffiliationComboBox.DataSource 
                    = new List<ComboBoxDto>{ new ComboBoxDto() { CODE = code, ID = cond.CLASS_ID } };
                this.AffiliationComboBox.SelectedValue = cond.CLASS_ID;
                this.DepartmentID = data.DEPARTMENT_ID;
                this.SectionID = data.SECTION_ID;
            }
            else if (cond.CLASS_KBN.Equals("9"))
            {
                this.AffiliAllRadioButton.Checked = true;
                this.AffiliationComboBox.Visible = false;
            }

            // ステータス（1:Openを検索対象とする）
                this.StatusOpenCheckBox.Checked = cond.STATUS_OPEN_FLG == "1";

            // ステータス（1:Closeを検索対象とする）
            this.StatusCloseCheckBox.Checked = cond.STATUS_CLOSE_FLG == "1";
            this.ClosedStartMonthDateTimePicker.Enabled = cond.STATUS_CLOSE_FLG == "1";
            this.ClosedEndMonthDateTimePicker.Enabled = cond.STATUS_CLOSE_FLG == "1";

            return true;
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
            //スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;

            }

            // スケジュール設定
            FormControlUtil.FormWait(this, () => this.SetSchedule());

            //スケジュールの先頭を設定
            var date = this.BaseMonthDateTimePicker.SelectedDate.Value;
            this.gridUtil.SetScheduleMostDayFirst(start <= date && date <= end ? date : start);

        }

        /// <summary>
        /// スケジュール設定
        /// </summary>
        private void SetSchedule()
        {
            if (this.BaseMonthDateTimePicker.Value == null)
                this.BaseMonthDateTimePicker.Value = DateTimeUtil.GetMostMondayFirst();

            //検索結果文言
            this.SearchResultLabel.Text = (this.ScheduleItemList == null || this.ScheduleItemList.Any() == false) ? Resources.KKM00005 : string.Empty;

            //スケジュール取得
            var scheduleList = this.GetScheduleList();

            // カレンダーにデータバインド
            this.gridUtil.Bind(this.ScheduleItemList, scheduleList, x => x.CATEGORY_ID, y => y.CATEGORY_ID,
                x => new ScheduleItemModel<WorkScheduleItemGetOutModel>(x.CATEGORY_ID, x.GENERAL_CODE, x.CATEGORY, x.PARALLEL_INDEX_GROUP, x.SORT_NO.Value, null, x),
                y => new ScheduleModel<WorkScheduleGetOutModel>(y.SCHEDULE_ID, y.CATEGORY_ID, y.PARALLEL_INDEX_GROUP, y.DESCRIPTION, y.START_DATE, y.END_DATE, y.INPUT_DATETIME, y.SYMBOL, (y.END_FLAG == 1), true, y));

        }

        /// <summary>
        /// スケジュール設定（項目含む）
        /// </summary>
        /// <param name="isMonthFirst">基準月先頭可否</param>
        private void SetScheduleAll(bool isMonthFirst = false)
        {
            //スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;

            }

            // スケジュール項目の取得
            this.ScheduleItemList = this.GetScheduleItemList();

            //カレンダーの表示期間変更
            var date = this.BaseMonthDateTimePicker.SelectedDate.Value;
            var start = new DateTime(date.Year, date.Month, 1).AddMonths(-1);
            this.gridUtil.SetCalendarViewPeriod(start);

            //基準月先頭可否
            if (isMonthFirst == true)
            {
                //スケジュールの先頭を基準月に設定
                this.gridUtil.SetScheduleMostDayFirst(date);

            }

            // スケジュール設定
            this.SetSchedule();

        }
        #endregion

        #endregion

        #region 画面イベント

        #region お気に入りコンボボックスの変更
        /// <summary>
        /// お気に入りコンボボックスの変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.FavoriteComboBox.SelectedValue?.ToString()) 
                || this.FavoriteComboBox.SelectedIndex == 0) return;

            this.FavoriteID = Convert.ToInt64(this.FavoriteComboBox.SelectedValue.ToString());

            // お気に入り検索条件の設定
            if (!this.SetFavoriteCondition()) return;

            // スケジュール設定（項目含む）
            FormControlUtil.FormWait(this, () => this.SetScheduleAll(isMonthFirst: true));
        }
        #endregion

        #region お気に入り編集ボタンクリック
        /// <summary>
        /// お気に入り編集ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteEditButton_Click(object sender, EventArgs e)
        {
            var list = GetFavoriteList();

            if (list == null && list.Count() <= 0)
            {
                // お気に入りなしはエラー
                Messenger.Info(Resources.KKM00036);

                return;
            }

            var favorite = new FavoriteSearchInModel();

            // データ区分
            favorite.CLASS_DATA = FavoriteClassData;

            // お気に入り検索
            using (var form = new FavoriteListForm(favorite))
            {
                var Functions = form.GetGridDataList();
                //お気に入りデータがない場合はメッセージ表示後終了
                if (Functions.Count == 0)
                {
                    Messenger.Info(Resources.KKM00036);
                }
                else
                {
                    //OKの場合はお気に入り再設定
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        //お気に入りコンボボックス再設定
                        this.FavoriteComboBox.SelectedValueChanged -= this.FavoriteComboBox_SelectedValueChanged;
                        FormControlUtil.SetComboBoxItem(this.FavoriteComboBox, GetFavoriteList());
                        this.FavoriteComboBox.SelectedValueChanged += this.FavoriteComboBox_SelectedValueChanged;
                    }
                }
            }
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
            var flg = !this.SearchConditionLayoutPanel.Visible;

            //検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionLayoutPanel, this.MainPanel, CondHeight);

            // ボタン
            this.SearchButton.Visible = flg;
            this.ConditionRegistButton.Visible = flg;

            // 検索結果ラベル
            this.SearchResultLabel.Visible = flg;
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
            // スケジュール設定（項目含む）
            FormControlUtil.FormWait(this, () => this.SetScheduleAll(isMonthFirst: true));

        }
        #endregion

        #region 条件登録ボタンクリック
        /// <summary>
        /// 条件登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionRegistButton_Click(object sender, EventArgs e)
        {
            //スケジュール検索のチェック
            if (this.IsSearchSchedule() == false)
            {
                return;

            }

            var list = GetFavoriteList();

            if (list != null && list.Count() >= Const.FavoriteEntryMax)
            {
                //最大登録数以上の場合はエラー
                Messenger.Info(Resources.KKM00016);

                return;
            }

            var form = new OperationPlanFavoriteForm();

            // 所属区分（0：担当、1：課、9：全部署）
            var classkbn = '9';
            if (this.AffiliSectionGroupRadioButton.Checked) classkbn = '0';
            if (this.AffiliSectionRadioButton.Checked) classkbn = '1';

            // パラメータ設定
            form.FAVORITE = new WorkFavoriteItemModel
            {
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,
                // タイトル
                TITLE = string.Empty,
                // 開発符号
                GENERAL_CODE = this.GeneralCodeComboBox.SelectedValue.ToString(),
                // 所属区分（1：担当、2：課、9：全部署）
                CLASS_KBN = Convert.ToChar(classkbn),
                // 所属ID
                CLASS_ID = this.AffiliationComboBox.SelectedValue.ToString(),
                // ステータス_OPENフラグ（1：チェック有）
                STATUS_OPEN_FLG = this.StatusOpenCheckBox.Checked ? '1' : '0',
                // ステータス_CLOSEフラグ（1：チェック有）
                STATUS_CLOSE_FLG = this.StatusCloseCheckBox.Checked ? '1' : '0'
            };

            // お気に入り登録
            if (form.ShowDialog().Equals(DialogResult.OK))
            {
                var newlist = GetFavoriteList();

                this.FavoriteComboBox.SelectedValueChanged -= this.FavoriteComboBox_SelectedValueChanged;
                FormControlUtil.SetComboBoxItem(this.FavoriteComboBox, newlist);
                this.FavoriteComboBox.SelectedValueChanged += this.FavoriteComboBox_SelectedValueChanged;

                base.FavoriteList = newlist;

                // スケジュール設定（項目含む）
                FormControlUtil.FormWait(this, () => this.SetScheduleAll());
            }

            form.Dispose();
        }
        #endregion

        #region Closeチェックボックス変更
        /// <summary>
        /// Closeチェックボックス変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusCloseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ClosedStartMonthDateTimePicker.Enabled = 
                !this.ClosedStartMonthDateTimePicker.Enabled;
            this.ClosedEndMonthDateTimePicker.Enabled = 
                !this.ClosedEndMonthDateTimePicker.Enabled;
        }
        #endregion

        #region 担当ラジオボタンクリック
        /// <summary>
        /// 担当ラジオボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffiliSectionGroupRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.AffiliationComboBox.Visible)
                this.AffiliationComboBox.Visible = true;

            this.AffiliationComboBox.DataSource = new List<ComboBoxDto>{ new ComboBoxDto()
            {
                CODE = SessionDto.DepartmentCode + "　" + SessionDto.SectionCode + "　" + SessionDto.SectionGroupCode,
                ID = SessionDto.SectionGroupID
            }};

            this.DepartmentID = SessionDto.DepartmentID;
            this.SectionID = SessionDto.SectionID;
        }
        #endregion

        #region 所属ラジオボタンクリック
        /// <summary>
        /// 所属ラジオボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffiliSectionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.AffiliationComboBox.Visible)
                this.AffiliationComboBox.Visible = true;

            this.AffiliationComboBox.DataSource = new List<ComboBoxDto>{ new ComboBoxDto()
            {
                CODE = SessionDto.DepartmentCode + "　" + SessionDto.SectionCode,
                ID = SessionDto.SectionID
            }};

            this.DepartmentID = SessionDto.DepartmentID;
            this.SectionID = SessionDto.SectionID;
        }
        #endregion

        #region 全部署ラジオボタンクリック
        /// <summary>
        /// 全部署ラジオボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffiliAllRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AffiliationComboBox.Visible)
                this.AffiliationComboBox.Visible = false;
        }
        #endregion

        #region 開発符号コンボボックスクリック
        /// <summary>
        /// 開発符号コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeComboBox_Click(object sender, EventArgs e)
        {
            this.label3.Focus();

            var form = new GeneralCodeListForm { UNDER_DEVELOPMENT = "1" };

            // パラメータ設定
            form.CAR_GROUP = this.CarGroup;

            // 開発符号検索
            if (form.ShowDialog().Equals(DialogResult.OK) && form.GENERAL_CODE != null)
            {
                // パラメータ取得
                var code = form.CAR_GROUP + "　" + form.GENERAL_CODE;
                var id = form.GENERAL_CODE;

                // コンボボックス設定
                this.GeneralCodeComboBox.DataSource =
                    new List<ComboBoxDto> { new ComboBoxDto() { CODE = code, ID = id } };
                this.GeneralCodeComboBox.SelectedValue = id;

                // 車系の退避
                this.CarGroup = form.CAR_GROUP;

                // スケジュール設定（項目含む）
                FormControlUtil.FormWait(this, () => this.SetScheduleAll(isMonthFirst: true));
            }

            form.Dispose();
        }
        #endregion

        #region 所属コンボボックスクリック
        /// <summary>
        /// 所属コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffiliationComboBox_Click(object sender, EventArgs e)
        {
            // 担当チェック時
            if (this.AffiliSectionGroupRadioButton.Checked)
            {
                this.AffiliSectionGroupRadioButton.Focus();

                using (var form = new SectionGroupListForm { DEPARTMENT_ID = SessionDto.DepartmentID, SECTION_ID = SessionDto.SectionID })
                {
                    // 担当検索
                    if (form.ShowDialog().Equals(DialogResult.OK) && form.SECTION_GROUP_ID != null)
                    {
                        // パラメータ取得
                        var code = form.DEPARTMENT_CODE + "　" + form.SECTION_CODE + "　" + form.SECTION_GROUP_CODE;
                        var id = form.SECTION_GROUP_ID;

                        // コンボボックス設定
                        this.AffiliationComboBox.DataSource =
                            new List<ComboBoxDto> { new ComboBoxDto() { CODE = code, ID = id } };
                        this.AffiliationComboBox.SelectedValue = id;

                        this.DepartmentID = form.DEPARTMENT_ID;
                        this.SectionID = form.SECTION_ID;
                    }

                }

            }
            // 課選択時
            else if (this.AffiliSectionRadioButton.Checked)
            {
                this.AffiliSectionRadioButton.Focus();

                var form = new Common.SectionListForm();

                // パラメータ設定
                form.DEPARTMENT_ID = this.DepartmentID;

                // 課検索
                if (form.ShowDialog().Equals(DialogResult.OK) && form.SECTION_ID != null)
                {

                    // パラメータ取得
                    var code = form.DEPARTMENT_CODE + "　" + form.SECTION_CODE;
                    var id = form.SECTION_ID;

                    // コンボボックス設定
                    this.AffiliationComboBox.DataSource =
                        new List<ComboBoxDto> { new ComboBoxDto() { CODE = code, ID = id } };
                    this.AffiliationComboBox.SelectedValue = id;

                    this.DepartmentID = form.DEPARTMENT_ID;
                    this.SectionID = form.SECTION_ID;
                }

                form.Dispose();
            }
            else
            {
                this.AffiliAllRadioButton.Focus();
            }
        }
        #endregion

        #region 基準月変更
        /// <summary>
        /// 基準月変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseMonthDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            //日付を選択されているかどうか
            var value = this.BaseMonthDateTimePicker.Value;

            if (value != null)
            {
                var day = (DateTime)value;
                var baseDay = new DateTime(day.Year, day.Month, 1);
                var startDay = baseDay.AddMonths(-1);

                var calendarStartDay = this.OperationPlanCalendarGrid.FirstDateInView;

                //ラベルの表示変更
                this.StartMonthLabel.Text = startDay.ToString(StringDateFormat);
                this.EndMonthLabel.Text = baseDay.AddMonths(1).ToString(StringDateFormat);
            }
        }
        #endregion

        #region 進捗状況一覧ボタンクリック
        /// <summary>
        /// 進捗状況一覧ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListButton_Click(object sender, EventArgs e)
        {
            var frm = new WorkProgressForm();

            frm.CAR_GROUP = this.CarGroup;
            frm.GENERAL_CODE = this.GeneralCodeComboBox?.SelectedValue?.ToString();

            frm.Show();
        }
        #endregion

        #region ダウンロードボタンクリック
        /// <summary>
        /// ダウンロードボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {

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

        #region スケジュールの日付範囲変更
        /// <summary>
        /// スケジュールの日付範囲変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperationPlanCalendarGrid_FirstDateInViewChanged(object sender, EventArgs e)
        {
            //スケジュール設定
            FormControlUtil.FormWait(this, () => this.SetSchedule());
        }
        #endregion

        #endregion

        #region カレンダーグリッドイベント

        #region 進捗履歴の起動
        /// <summary>
        /// 進捗履歴の起動
        /// </summary>
        private void OpenProgressListForm(ScheduleItemModel<WorkScheduleItemGetOutModel> item)
        {
            // スケジュール項目のチェック
            if (!this.IsEntryScheduleItem(ScheduleItemEditType.Update, item.ID)) return;

            // 進捗履歴画面表示
            using (var form = new WorkProgressHistroryForm { ScheduleItem = new WorkProgressItemModel { ID = item.ID }, UserAuthority = this.UserAuthority })
            {
                form.ShowDialog(this);
            }
        }
        #endregion

        #region スケジュール項目詳細の起動
        /// <summary>
        /// スケジュール項目詳細の起動
        /// </summary>
        private void OpenScheduleItemForm(ScheduleItemModel<WorkScheduleItemGetOutModel> item, ScheduleItemEditType type)
        {
            // スケジュール項目のチェック
            if (!this.IsEntryScheduleItem(type, item.ID)) return;

            item.ScheduleItemEdit = type;

            var form = new OperationPlanItemForm();

            // パラメータ設定
            form.ITEM = item;
            form.GENERAL_CODE = this.GeneralCodeComboBox.SelectedValue.ToString();
            form.SECTION_GROUP_ID = this.AffiliSectionGroupRadioButton.Checked == true
                ? this.AffiliationComboBox.SelectedValue.ToString() : null;
            form.IS_UPDATE = this.UserAuthority.UPDATE_FLG == '1';

            // 項目編集
            if (form.ShowDialog().Equals(DialogResult.OK))
            {
                // スケジュール設定（項目含む）
                FormControlUtil.FormWait(this, () => this.SetScheduleAll());

                //更新かどうか
                if (type == ScheduleItemEditType.Update)
                {
                    ////変更したスケジュール項目へ縦スクロールを設定→廃止しました
                    //this.gridUtil.SetScheduleRowHeaderFirst(item.ID);
                }
            }

            form.Dispose();
        }
        #endregion

        #region スケジュール項目のコピー・移動の起動
        /// <summary>
        /// スケジュール項目のコピー・移動の起動
        /// </summary>
        private void OpenItemCopyMoveForm(ScheduleItemModel<WorkScheduleItemGetOutModel> item)
        {
            // スケジュール項目のチェック
            if (!this.IsEntryScheduleItem(ScheduleItemEditType.Update, item.ID)) return;

            var form = new Common.ItemCopyMoveForm();

            // パラメータ設定
            form.GeneralCode = item.GeneralCode;
            form.SectionGroupID = item.ScheduleItem.SECTION_GROUP_ID;
            form.ScheduleID = item.ID;
            form.TableNumber = 1;

            // 項目編集
            if (form.ShowDialog().Equals(DialogResult.OK))
            {
                // スケジュール設定（項目含む）
                FormControlUtil.FormWait(this, () => this.SetScheduleAll());
            }

            form.Dispose();
        }
        #endregion

        #region スケジュール項目の並び順変更
        /// <summary>
        /// スケジュール項目の並び順変更
        /// </summary>
        private void ChangeScheduleItemSort(ScheduleItemModel<WorkScheduleItemGetOutModel> sitem, ScheduleItemModel<WorkScheduleItemGetOutModel> ditem)
        {
            // スケジュール項目のチェック
            if (!this.IsEntryScheduleItem(ScheduleItemEditType.Update, sitem.ID)) return;
            if (!this.IsEntryScheduleItem(ScheduleItemEditType.Update, ditem.ID)) return;

            sitem.SortNo = ditem.SortNo + 0.1;

            this.ScheduleItemPut(sitem, ScheduleItemEditType.Update);
        }
        #endregion

        #region スケジュール項目のチェック
        /// <summary>
        /// スケジュール項目のチェック
        /// </summary>
        /// <returns>bool</returns>
        private bool IsEntryScheduleItem(ScheduleItemEditType type, long scheduleid)
        {
            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //追加
                case ScheduleItemEditType.Insert:
                    //入力がOKかどうか
                    if (this.IsSearchSchedule() == false)
                    {
                        return false;

                    }
                    break;

                case ScheduleItemEditType.Update:       //更新
                case ScheduleItemEditType.Delete:       //削除
                case ScheduleItemEditType.RowAdd:       //行追加
                case ScheduleItemEditType.RowDelete:    //行削除

                    //データが存在しているかどうか
                    var item = this.ScheduleItemGet(scheduleid);

                    if (item == null)
                    {
                        //存在していない場合はエラー
                        Messenger.Info(Resources.KKM00021);

                        // スケジュール設定（項目含む）
                        FormControlUtil.FormWait(this, () => this.SetScheduleAll());

                        return false;
                    }
                    break;
            }

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                case ScheduleItemEditType.Delete:       //削除

                    //対象項目にスケジュールがある場合はエラー
                    if (this.ScheduleListGet(new WorkScheduleGetInModel { CATEGORY_ID = scheduleid })?.Count() > 0)
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
        /// <returns>WorkScheduleItemGetOutModel</returns>
        private WorkScheduleItemGetOutModel ScheduleItemGet(long scheduleid)
        {
            WorkScheduleItemGetOutModel item = null;

            //検索条件
            var cond = new WorkScheduleItemGetInModel { SCHEDULE_ID = scheduleid };

            //APIで取得
            var res = HttpUtil.GetResponse<WorkScheduleItemGetInModel, WorkScheduleItemGetOutModel>(ControllerType.WorkScheduleItem, cond);

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
        private void ScheduleItemPut(ScheduleItemModel<WorkScheduleItemGetOutModel> item, ScheduleItemEditType type)
        {
            // スケジュール項目更新
            if (item != null && item.ID > 0)
            {
                // スケジュール項目のチェック
                if (!this.IsEntryScheduleItem(type, item.ID)) return;

                // 行数調整
                if (type.Equals(ScheduleItemEditType.RowAdd))
                {
                    //最大行数以上の場合はエラー
                    if (item.RowCount >= Const.ScheduleItemRowMax 
                        || item.ScheduleItem.PARALLEL_INDEX_GROUP >= Const.ScheduleItemRowMax)
                    {
                        Messenger.Info(Resources.KKM00035);

                        return;
                    }

                    item.RowCount++;
                }
                else if (type.Equals(ScheduleItemEditType.RowDelete))
                {
                    //1行しかない場合はエラー
                    if (item.RowCount == 1 || item.ScheduleItem.PARALLEL_INDEX_GROUP == 1)
                    {
                        Messenger.Info(Resources.KKM00034);

                        return;
                    }

                    //対象行にスケジュールがある場合はエラー
                    if (this.ScheduleListGet(new WorkScheduleGetInModel { CATEGORY_ID = item.ID, PARALLEL_INDEX_GROUP = item.RowCount })?.Count() > 0)
                    {
                        Messenger.Info(Resources.KKM00033);

                        return;
                    }

                    if (Messenger.Confirm(Resources.KKM00007)   // 削除確認
                        .Equals(DialogResult.No)) return;

                    item.RowCount--;
                }

                var data = new WorkScheduleItemPutInModel
                {
                    // カテゴリーID
                    CATEGORY_ID = item.ID,
                    // カテゴリー
                    CATEGORY = item.ScheduleItem.CATEGORY,
                    // 所属グループID
                    SECTION_GROUP_ID = item.ScheduleItem.SECTION_GROUP_ID,
                    // 並び順
                    SORT_NO = item.SortNo,
                    // 行数
                    PARALLEL_INDEX_GROUP = item.RowCount,
                    // パーソナルID
                    PERSONEL_ID = SessionDto.UserId
                };

                var res = HttpUtil.PutResponse<WorkScheduleItemPutInModel>(ControllerType.WorkScheduleItem, data);

                if (res.Status.Equals(Const.StatusSuccess))
                {
                    if (type.Equals(ScheduleItemEditType.RowDelete))
                    {
                        Messenger.Info(Resources.KKM00003); // 削除完了
                    }

                    // スケジュール設定（項目含む）
                    FormControlUtil.FormWait(this, () => this.SetScheduleAll());

                    ////変更したスケジュール項目へ縦スクロールを設定→廃止しました
                    //this.gridUtil.SetScheduleRowHeaderFirst(item.ID);
                }
            }
        }
        #endregion

        #region スケジュール項目の削除
        /// <summary>
        /// スケジュール項目の削除
        /// </summary>
        private void ScheduleItemDelete(ScheduleItemModel<WorkScheduleItemGetOutModel> item)
        {
            if (Messenger.Confirm(Resources.KKM00007)   // 削除確認
             .Equals(DialogResult.No)) return;

            // スケジュール項目削除
            if (item != null && item.ID > 0)
            {
                // スケジュール項目のチェック
                if (!this.IsEntryScheduleItem(ScheduleItemEditType.Delete, item.ID)) return;

                var data = new WorkScheduleItemDeleteInModel
                {
                    // カテゴリーID
                    CATEGORY_ID = item.ID
                };

                var res = HttpUtil.DeleteResponse<WorkScheduleItemDeleteInModel>(ControllerType.WorkScheduleItem, data);

                if (res.Status.Equals(Const.StatusSuccess))
                {
                    Messenger.Info(Resources.KKM00003); // 削除完了

                    // スケジュール設定（項目含む）
                    FormControlUtil.FormWait(this, () => this.SetScheduleAll());
                }
            }
        }
        #endregion

        #region スケジュール詳細の起動
        /// <summary>
        /// スケジュール詳細の起動
        /// </summary>
        private void OpenScheduleForm(ScheduleModel<WorkScheduleGetOutModel> schedule, ScheduleEditType type)
        {
            // スケジュールのチェック
            if (!this.IsEntrySchedule(type, schedule)) return;

            schedule.ScheduleEdit = type;

            var form = new OperationPlanScheduleForm();

            // パラメータ設定
            form.SCHEDULE = schedule;
            form.IS_UPDATE = this.UserAuthority.UPDATE_FLG == '1';

            // 開発符号検索
            if (form.ShowDialog().Equals(DialogResult.OK))
            {
                // スケジュール設定
                FormControlUtil.FormWait(this, () => this.SetSchedule());

                ////変更したスケジュール項目へ縦スクロールを設定→廃止しました
                //this.gridUtil.SetScheduleRowHeaderFirst(schedule.CategoryID);
            }

            form.Dispose();
        }
        #endregion

        #region スケジュールのチェック
        /// <summary>
        /// スケジュールのチェック
        /// </summary>
        /// <returns>bool</returns>
        private bool IsEntrySchedule(ScheduleEditType type, ScheduleModel<WorkScheduleGetOutModel> schedule)
        {
            //スケジュール項目が存在しているかどうか
            var item = this.ScheduleItemGet(schedule.CategoryID);

            if (item == null)
            {
                //存在していない場合はエラー
                Messenger.Info(Resources.KKM00021);

                // スケジュール設定（項目含む）
                FormControlUtil.FormWait(this, () => this.SetScheduleAll());

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
                        FormControlUtil.FormWait(this, () => this.SetSchedule());

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

                    var start = schedule.StartDate;
                    var end = schedule.EndDate;

                    //検索条件
                    var cond = new WorkScheduleGetInModel
                    {
                        //カテゴリーID
                        CATEGORY_ID = schedule.CategoryID,
                        //期間(From)
                        DATE_START = this.OperationPlanCalendarGrid.FirstDateInView.Date,
                        //期間(To)
                        DATE_END = this.OperationPlanCalendarGrid.LastDateInView.Date,
                        //行番号
                        PARALLEL_INDEX_GROUP = schedule.RowNo
                    };

                    //スケジュールで重複した期間が存在する場合はエラー
                    if (this.ScheduleListGet(cond).Where(x => x.SCHEDULE_ID != schedule.ID).Any(x =>
                        (x.START_DATE <= start && start <= x.END_DATE) || (x.START_DATE <= end && end <= x.END_DATE) ||
                        (start <= x.START_DATE && x.START_DATE <= end) || (start <= x.END_DATE && x.END_DATE <= end)) == true)
                    {
                        Messenger.Warn(Resources.KKM03005);

                        //スケジュール設定
                        FormControlUtil.FormWait(this, () => this.SetSchedule());

                        ////スケジュール項目へ縦スクロールを設定→廃止しました
                        //this.gridUtil.SetScheduleRowHeaderFirst(schedule.CategoryID);

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
        /// <returns>WorkScheduleGetOutModel</returns>
        private WorkScheduleGetOutModel ScheduleGet(long scheduleid)
        {
            WorkScheduleGetOutModel schedule = null;

            //パラメータ設定
            var cond = new WorkScheduleGetInModel
            {
                // スケジュールID
                SCHEDULE_ID = scheduleid
            };

            //APIで取得
            var res = HttpUtil.GetResponse<WorkScheduleGetInModel, WorkScheduleGetOutModel>(ControllerType.WorkSchedule, cond);

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
        /// <returns>WorkScheduleGetOutModel</returns>
        private List<WorkScheduleGetOutModel> ScheduleListGet(WorkScheduleGetInModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<WorkScheduleGetInModel, WorkScheduleGetOutModel>(ControllerType.WorkSchedule, cond);

            return (res.Results).ToList();
        }
        #endregion

        #region スケジュールの登録
        /// <summary>
        /// スケジュールの登録
        /// </summary>
        private void SchedulePost(ScheduleModel<WorkScheduleGetOutModel> schedule)
        {
            if (schedule == null) return;

            // スケジュールのチェック
            if (!this.IsEntrySchedule(ScheduleEditType.Insert, schedule)) return;

            var data = new WorkSchedulePostInModel
            {
                // カテゴリーID
                CATEGORY_ID = schedule.CategoryID,
                // 作業完了
                END_FLAG = schedule.Schedule.END_FLAG != 1 ? 0 : 1,
                // 期間（開始）
                START_DATE = schedule.StartDate,
                // 期間（終了）
                END_DATE = schedule.EndDate,
                // 行番号
                PARALLEL_INDEX_GROUP = schedule.RowNo,
                // スケジュール区分
                SYMBOL = schedule.Schedule.SYMBOL,
                // 説明
                DESCRIPTION = schedule.Schedule.DESCRIPTION,
                // パーソナルID
                PERSONEL_ID = SessionDto.UserId
            };

            var res = HttpUtil.PostResponse<WorkSchedulePostInModel>(ControllerType.WorkSchedule, data);

            if (res.Status.Equals(Const.StatusSuccess))
            {
                //更新メッセージ
                Messenger.Info(Resources.KKM00002);

                // スケジュール設定
                FormControlUtil.FormWait(this, () => this.SetSchedule());

                ////変更したスケジュール項目へ縦スクロールを設定→廃止しました
                //this.gridUtil.SetScheduleRowHeaderFirst(schedule.CategoryID);
            }
        }
        #endregion

        #region スケジュールの更新
        /// <summary>
        /// スケジュールの更新
        /// </summary>
        private void SchedulePut(ScheduleModel<WorkScheduleGetOutModel> schedule)
        {
            // スケジュール登録
            if (schedule != null && schedule.ID > 0)
            {
                // スケジュールのチェック
                if (!this.IsEntrySchedule(ScheduleEditType.Update, schedule)) return;

                var data = new WorkSchedulePutInModel
                {
                    // スケジュールID
                    SCHEDULE_ID = schedule.ID,
                    // 行番号
                    PARALLEL_INDEX_GROUP = schedule.RowNo,
                    // 作業完了
                    END_FLAG = schedule.Schedule.END_FLAG != 1 ? 0 : 1,
                    // 期間（開始）
                    START_DATE = schedule.StartDate,
                    // 期間（終了）
                    END_DATE = schedule.EndDate,
                    // スケジュール区分
                    SYMBOL = schedule.Schedule.SYMBOL,
                    // 説明
                    DESCRIPTION = schedule.Schedule.DESCRIPTION,
                    // パーソナルID
                    PERSONEL_ID = SessionDto.UserId
                };

                var res = HttpUtil.PutResponse<WorkSchedulePutInModel>(ControllerType.WorkSchedule, data);

                if (res.Status.Equals(Const.StatusSuccess))
                {
                    // スケジュール設定
                    FormControlUtil.FormWait(this, () => this.SetSchedule());

                    ////変更したスケジュール項目へ縦スクロールを設定→廃止しました
                    //this.gridUtil.SetScheduleRowHeaderFirst(schedule.CategoryID);
                }
            }
        }
        #endregion

        #region スケジュールの削除
        /// <summary>
        /// スケジュールの削除
        /// </summary>
        private void ScheduleDelete(ScheduleModel<WorkScheduleGetOutModel> schedule, bool confirm)
        {
            if (confirm)
                if (Messenger.Confirm(Resources.KKM00007)   // 削除確認
                .Equals(DialogResult.No)) return;

            // スケジュール削除
            if (schedule != null && schedule.ID > 0)
            {
                // スケジュールのチェック
                if (!this.IsEntrySchedule(ScheduleEditType.Delete, schedule)) return;

                var data = new WorkScheduleDeleteInModel
                {
                    // スケジュールID
                    SCHEDULE_ID = schedule.ID
                };

                var res = HttpUtil.DeleteResponse<WorkScheduleDeleteInModel>(ControllerType.WorkSchedule, data);

                if (res.Status.Equals(Const.StatusSuccess))
                {
                    Messenger.Info(Resources.KKM00003); // 削除完了

                    // スケジュール設定
                    FormControlUtil.FormWait(this, () => this.SetSchedule());

                    ////変更したスケジュール項目へ縦スクロールを設定→廃止しました
                    //this.gridUtil.SetScheduleRowHeaderFirst(schedule.CategoryID);
                }
            }
        }
        #endregion

        #region 追加行ヘッダの値を取得
        /// <summary>
        /// 追加行ヘッダの値を取得
        /// </summary>
        private string GetRowHeaderOtherValue(ScheduleItemModel<WorkScheduleItemGetOutModel> item, int j)
        {
            // 列「担当」：担当コード
            if (j == 1) return item.ScheduleItem.SECTION_GROUP_CODE;

            return string.Empty;
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
        private WorkFavoriteSearchOutModel GetFavoriteData()
        {
            var list = new List<WorkFavoriteSearchOutModel>();

            //パラメータ設定
            var itemCond = new WorkFavoriteSearchInModel
            {
                // お気に入りID
                ID = Convert.ToInt64(this.FavoriteID),
            };

            //Get実行
            var res = HttpUtil.GetResponse<WorkFavoriteSearchInModel, WorkFavoriteSearchOutModel>(ControllerType.WorkFavorite, itemCond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                list.AddRange(res.Results);
            }

            return list.FirstOrDefault();
        }
        #endregion

        #region 開発符号データの取得
        /// <summary>
        /// 開発符号データの取得
        /// </summary>
        private GeneralCodeSearchOutModel GetGeneralCodeData(string generalcode)
        {
            var list = new List<GeneralCodeSearchOutModel>();

            //パラメータ設定
            var itemCond = new GeneralCodeSearchInModel
            {
                // 完全一致
                CLASS_DATA = 1,
                // 開発符号
                GENERAL_CODE = generalcode,
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId
            };

            //Get実行
            var res = HttpUtil.GetResponse<GeneralCodeSearchInModel, GeneralCodeSearchOutModel>(ControllerType.GeneralCode, itemCond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                list.AddRange(res.Results);
            }

            return list.FirstOrDefault();
        }
        #endregion

        #region 課データの取得
        /// <summary>
        /// 課データの取得
        /// </summary>
        private SectionModel GetSectionData(string sectionid)
        {
            var list = new List<SectionModel>();

            //パラメータ設定
            var itemCond = new SectionSearchModel
            {
                //課ID
                SECTION_ID = sectionid
            };

            //Get実行
            var res = HttpUtil.GetResponse<SectionSearchModel, SectionModel>(ControllerType.Section, itemCond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                list.AddRange(res.Results);
            }

            return list.FirstOrDefault();
        }
        #endregion

        #region 課グループデータの取得
        /// <summary>
        /// 課グループデータの取得
        /// </summary>
        private SectionGroupModel GetSectionGroupData(string sectiongroupid)
        {
            var list = new List<SectionGroupModel>();

            //パラメータ設定
            var itemCond = new SectionGroupSearchModel
            {
                //課グループID
                SECTION_GROUP_ID = sectiongroupid
            };

            //Get実行
            var res = HttpUtil.GetResponse<SectionGroupSearchModel, SectionGroupModel>(ControllerType.SectionGroup, itemCond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                list.AddRange(res.Results);
            }

            return list.FirstOrDefault();
        }
        #endregion

        #region カレンダーグリッドデータの取得
        /// <summary>
        /// スケジュール項目取得
        /// </summary>
        /// <returns>IEnumerable<WorkScheduleItemGetOutModel></returns>
        private IEnumerable<WorkScheduleItemGetOutModel> GetScheduleItemList()
        {
            // 開発符号
            var generalCode = this.GeneralCodeComboBox.SelectedValue.ToString();

            // 所属
            var sectionId = this.AffiliSectionRadioButton.Checked ? this.AffiliationComboBox.SelectedValue.ToString() : null;
            var sectionGroupId = this.AffiliSectionGroupRadioButton.Checked ? this.AffiliationComboBox.SelectedValue.ToString() : null;

            // 状態（0：Openのみチェック、1：Closeのみチェック、2：Open,Close両方チェック）
            int? flg = null;
            if (this.StatusOpenCheckBox.Checked && this.StatusCloseCheckBox.Checked)
            {
                flg = 2;
            }
            else if (this.StatusOpenCheckBox.Checked)
            {
                flg = 0;
            }
            else if (this.StatusCloseCheckBox.Checked)
            {
                flg = 1;
            }

            // Close日
            var start = this.ClosedStartMonthDateTimePicker.SelectedDate;
            var end = this.ClosedEndMonthDateTimePicker.SelectedDate;

            var cond = new WorkScheduleItemGetInModel
            {
                // 開発符号
                GENERAL_CODE = generalCode,

                // 所属
                SECTION_ID = sectionId,

                // 所属グループID
                SECTION_GROUP_ID = sectionGroupId,
                
                // 状態（Open/Close）
                OPEN_CLOSE_FLAG = flg,
                
                // Close日（開始）
                CLOSED_DATE_START = start,

                // Close日（終了）
                CLOSED_DATE_END = start

            };

            //APIで取得
            var res = HttpUtil.GetResponse<WorkScheduleItemGetInModel, WorkScheduleItemGetOutModel>(ControllerType.WorkScheduleItem, cond);

            //返却
            return res.Results;
        }

        /// <summary>
        /// スケジュール取得
        /// </summary>
        /// <returns>IEnumerable<WorkScheduleGetOutModel></returns>
        private IEnumerable<WorkScheduleGetOutModel> GetScheduleList()
        {
            var itemList = this.ScheduleItemList;

            // 開発符号
            var generalCode = this.GeneralCodeComboBox.SelectedValue.ToString();

            // 所属
            var sectionId = this.AffiliSectionRadioButton.Checked ? this.AffiliationComboBox.SelectedValue.ToString() : null;
            var sectionGroupId = this.AffiliSectionGroupRadioButton.Checked ? this.AffiliationComboBox.SelectedValue.ToString() : null;

            // 状態（0：Openのみチェック、1：Closeのみチェック、2：Open,Close両方チェック）
            int? flg = null;
            if (this.StatusOpenCheckBox.Checked && this.StatusCloseCheckBox.Checked)
            {
                flg = 2;
            }
            else if (this.StatusOpenCheckBox.Checked)
            {
                flg = 0;
            }
            else if (this.StatusCloseCheckBox.Checked)
            {
                flg = 1;
            }

            // Close日
            var start = this.ClosedStartMonthDateTimePicker.SelectedDate;
            var end = this.ClosedEndMonthDateTimePicker.SelectedDate;

            var cond = new WorkScheduleGetInModel
            {
                // 開発符号
                GENERAL_CODE = generalCode,

                // 所属
                SECTION_ID = sectionId,

                // 所属グループID
                SECTION_GROUP_ID = sectionGroupId,

                // 状態（Open/Close）
                OPEN_CLOSE_FLAG = flg,

                // Close日（開始）
                CLOSED_DATE_START = start,

                // Close日（終了）
                CLOSED_DATE_END = end,

                // 期間(From)
                DATE_START = this.OperationPlanCalendarGrid.FirstDateInView.Date,

                // 期間(To)
                DATE_END = this.OperationPlanCalendarGrid.LastDateInView.Date,
            };

            //APIで取得
            var res = HttpUtil.GetResponse<WorkScheduleGetInModel, WorkScheduleGetOutModel>(ControllerType.WorkSchedule, cond);

            //返却
            return res.Results;
        }
        #endregion

        #endregion

        #region その他
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

        /// <summary>
        /// お気に入りチェック
        /// </summary>
        /// <returns></returns>
        private bool FavoriteIsEnable(long? FavoriteID)
        {
            foreach (FavoriteSearchOutModel fsom in base.FavoriteList)
            {
                if (fsom.CLASS_DATA == FavoriteClassData && fsom.ID == FavoriteID && fsom.PERMIT_FLG == 1)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
