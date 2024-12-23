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
using System.Diagnostics;

namespace DevPlan.Presentation.UIDevPlan.MonthlyPlan
{
    /// <summary>
    /// 月次計画表
    /// </summary>
    public partial class MonthlyPlanForm : BaseForm
    {
        #region メンバ変数
        private const string CornerHeaderText = "項目";

        private const string MonthFirstText = "月頭";
        private const string MonthEndText = "月末";
        private const string ApprovalText = "承認";
        private const string ReleaseText = "解除";
        private const string ApplicationText = "申請";

        private const string MailAddress = "xxx@xxx.xx";
        private const string MailBodyApproval = "{0}月度月次計画をご確認頂き､承認を宜しくお願いします｡";
        private const string MailBodyRelease = "{0}月度月次計画の承認解除を宜しくお願いします｡";

        private const int CondHeight = 105;

        private const string FavoriteClassData = Const.FavoriteMonthlyWork;

        CalendarGridUtil<MonthlyWorkScheduleItemGetOutModel, MonthlyWorkScheduleGetOutModel> gridUtil;

        /// <summary>お気に入りID</summary>
        private long? FavoriteID;

        /// <summary>対象月</summary>
        private DateTime BaseMonth;
        /// <summary>月頭月末</summary>
        private int MonthFirstEnd;
        /// <summary>所属ID</summary>
        private string SectionID;
        /// <summary>所属グループID</summary>
        private string SectionGroupID;
        /// <summary>所属文言</summary>
        private string AffiliationText;
        /// <summary>所属文言（退避）</summary>
        private string AffiliationTextTemp;
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "月次計画表"; } }

        /// <summary>お気に入りID</summary>
        public long? FAVORITE_ID { get { return FavoriteID; } set { FavoriteID = value; } }

        /// <summary>スケジュール項目リスト</summary>
        private IEnumerable<MonthlyWorkScheduleItemGetOutModel> ScheduleItemList { get; set; }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>月次計画承認</summary>
        private List<MonthlyWorkApprovalGetOutModel> MonthlyWorkApprovalList { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MonthlyPlanForm()
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
        private void MonthlyPlanForm_Load(object sender, EventArgs e)
        {
            // 権限の取得
            this.UserAuthority = this.GetFunction(FunctionID.Plan);

            // カレンダーグリッドの初期化
            this.InitCalendarGrid();

            // 画面の初期化
            this.InitForm();

            // お気に入りから遷移時
            if (FavoriteID != null)
            {
                this.FavoriteComboBox.SelectedValue = FavoriteID;

                return;
            }

            // スケジュール設定（項目含む）
            FormControlUtil.FormWait(this, () => this.SetScheduleAll());
        }
        #endregion

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';

            var today = DateTime.Today;

            // お気に入り
            FormControlUtil.SetComboBoxItem(this.FavoriteComboBox, GetFavoriteList());
            this.FavoriteComboBox.SelectedValueChanged += this.FavoriteComboBox_SelectedValueChanged;

            // 基準月
            this.BaseMonthDateTimePicker.Value = today;

            // 所属
            this.AffiliationComboBox.DataSource = new List<ComboBoxDto>{ new ComboBoxDto()
            {
                CODE = SessionDto.DepartmentCode + "　" + SessionDto.SectionCode + "　" + SessionDto.SectionGroupCode,
                ID = SessionDto.SectionGroupID
            }};
            this.AffiliationComboBox.SelectedValue = SessionDto.SectionGroupID;
            this.AffiliationTextTemp = SessionDto.DepartmentName + "　" + SessionDto.SectionName + "　" + SessionDto.SectionGroupName;

            // 同期
            this.SynchronismButton.Visible = isManagement;

            // ダウンロード
            this.DownloadButton.Visible = isExport;
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

            var map = new Dictionary<string, Action<ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel>>>();

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

            // TODO : ■テストを行えない機能（2018年上期リリース対象外）につきaddRowHeaderColumnConfig,isScheduleToolTipDateTimeFormatはコメントアウト。

            var config = new CalendarGridConfigModel<MonthlyWorkScheduleItemGetOutModel, MonthlyWorkScheduleGetOutModel>(
            this.MonthlyPlanCalendarGrid, isManagement, isUpdate, 
            //isScheduleToolTipDateTimeFormat, 
            CornerHeaderText,
            new Dictionary<string, Action>
            {
                { "項目追加", () => this.OpenScheduleItemForm(new ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel>(), ScheduleItemEditType.Insert) }
            },
            map,
            new Dictionary<string, Action<ScheduleModel<MonthlyWorkScheduleGetOutModel>>>
            {
                { "削除", schedule => this.ScheduleDelete(schedule, true) }
            }
            //new Dictionary<string, RowHeaderModel>
            //{
            //    { "開発符号", new RowHeaderModel { Width = 80, MinWidth = 30, MaxWidth = 200 } },
            //    { "担当", new RowHeaderModel { Width = 80, MinWidth = 30, MaxWidth = 200 } },
            //    { "担当者", new RowHeaderModel { Width = 80, MinWidth = 30, MaxWidth = 200 } },
            //    { "備考", new RowHeaderModel { Width = 80, MinWidth = 30, MaxWidth = 400 } },
            //},
            //this.GetRowHeaderOtherValue
            );

            // スケジュール表示期間変更可否（月次は変更不可）
            config.IsScheduleViewPeriodChange = false;

            // 行ヘッダーの列幅設定
            config.BaseRowHeader = new RowHeaderModel { Width = 80, MinWidth = 30, MaxWidth = 400 };

            //スケジュールダブルクリックテキスト
            config.ScheduleDoubleClickLabelText = "※項目ダブルクリックで進捗履歴を表示します。";

            //スケジュール項目の背景色を取得するデリゲート
            //config.GetScheduleItemBackColor = x => x.ScheduleItem.FLAG_月報専用項目 == 1 ? Color.Azure : this.gridUtil.GetScheduleItemBackColor(null);

            //カレンダーグリッドの初期設定
            this.gridUtil = new CalendarGridUtil<MonthlyWorkScheduleItemGetOutModel, MonthlyWorkScheduleGetOutModel>(config);

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
            
            // 所属（0:担当、1:所属）
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

                this.AffiliationTextTemp = (data != null && data.SECTION_GROUP_CODE != null) 
                    ? data.DEPARTMENT_NAME + "　" + data.SECTION_NAME + "　" + data.SECTION_GROUP_NAME : string.Empty;
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

                this.AffiliationTextTemp = (data != null && data.SECTION_CODE != null)
                    ? data.DEPARTMENT_NAME + "　" + data.SECTION_NAME : string.Empty;
            }

            // ステータス（1:Openを検索対象とする）
            this.StatusOpenCheckBox.Checked = cond.STATUS_OPEN_FLG == "1";

            // ステータス（1:Closeを検索対象とする）
            this.StatusCloseCheckBox.Checked = cond.STATUS_CLOSE_FLG == "1";

            // 指定月
            this.BaseMonthDateTimePicker.Value = cond.TARGET_MONTH;

            // 表示対象（1:月頭、2:月末）
            if (cond.DISPLAY_KBN.Equals("2"))
                this.MonthEndRadioButton.Checked = true;
            else
                this.MonthFirstRadioButton.Checked = true;
       
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
            // 月次計画承認の取得（最新）
            this.MonthlyWorkApprovalList = this.GetApprovalList();

            var map = new Dictionary<Control, Func<Control, string, string>>();

            var isEndApprovalSearch = this.MonthEndRadioButton.Checked;
            var isFirstApproval = this.MonthlyWorkApprovalList?.Where(x => x.FLAG_月頭月末 == 1)?.FirstOrDefault()?.FLAG_承認 == 1;
            var isEndApproval = this.MonthlyWorkApprovalList?.Where(x => x.FLAG_月頭月末 == 2)?.FirstOrDefault()?.FLAG_承認 == 1;

            // 月頭承認チェック（月頭未承認は月末データの利用不可）
            if (!isFirstApproval && isEndApprovalSearch)
            {
                //エラーメッセージ
                Messenger.Warn(Resources.KKM03006);

                //背景色を変更
                this.ReservationPanel.BackColor = Const.ErrorBackColor;

                return false;
            }
            else
            {
                this.ReservationPanel.BackColor = Color.FromArgb(0xE6, 0xFF, 0xFF);
            }

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
            // スケジュール設定
            FormControlUtil.FormWait(this, () => this.SetSchedule());
        }
        /// <summary>
        /// スケジュール設定
        /// </summary>
        private void SetSchedule(bool itemflg = false)
        {
            if (this.BaseMonthDateTimePicker.Value == null)
                this.BaseMonthDateTimePicker.Value = DateTime.Today;

            var date = Convert.ToDateTime(this.BaseMonthDateTimePicker.Value);

            // 検索条件の退避
            this.BaseMonth = new DateTime(date.Year, date.Month, 1);
            this.MonthFirstEnd = this.MonthFirstRadioButton.Checked ? 1 : 2;
            this.SectionID = this.AffiliSectionRadioButton.Checked
                ? this.AffiliationComboBox.SelectedValue.ToString() : null;
            this.SectionGroupID = this.AffiliSectionGroupRadioButton.Checked
                ? this.AffiliationComboBox.SelectedValue.ToString() : null;
            this.AffiliationText = this.AffiliationTextTemp;

            // 検索条件のチェック
            if (!this.IsSearchSchedule()) return;

            // スケジュール項目の設定
            if (itemflg)
            {
                // スケジュール項目の取得
                this.ScheduleItemList = this.GetScheduleItemList();

                //カレンダーの表示期間変更
                //this.gridUtil.SetCalendarViewPeriod(new DateTime(date.Year, date.Month, 1), 1);
            }

            // 動的フォームの設定
            this.SetForm();

            // 検索結果文言
            this.SearchResultLabel.Text = (this.ScheduleItemList == null || this.ScheduleItemList.Any() == false) ? Resources.KKM00005 : string.Empty;

            //スケジュール取得
            var scheduleList = this.GetScheduleList();

            // カレンダーにデータバインド
            this.gridUtil.Bind(this.ScheduleItemList, scheduleList, x => x.CATEGORY_ID, y => y.CATEGORY_ID,
                x => new ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel>(x.CATEGORY_ID, x.GENERAL_CODE, x.CATEGORY, x.PARALLEL_INDEX_GROUP, x.SORT_NO.Value, null, x),
                y => new ScheduleModel<MonthlyWorkScheduleGetOutModel>(y.SCHEDULE_ID, y.CATEGORY_ID, y.PARALLEL_INDEX_GROUP, y.DESCRIPTION, y.START_DATE, y.END_DATE, y.INPUT_DATETIME, y.SYMBOL, (y.END_FLAG == 1), true, y));

        }

        /// <summary>
        /// スケジュール設定（項目含む）
        /// </summary>
        private void SetScheduleAll()
        {
            var date = Convert.ToDateTime(this.BaseMonthDateTimePicker.Value);

            // スケジュール設定
            this.SetSchedule(true);

            //スケジュールの先頭を強制的に1日に設定
            this.gridUtil.SetScheduleMostDayFirst(new DateTime(date.Year, date.Month, 1));
        }
        #endregion

        #region 動的フォーム設定
        /// <summary>
        /// 動的フォーム設定
        /// </summary>
        /// <remarks>検索後・承認後の動的フォームの設定</remarks>
        private void SetForm()
        {
            // 月次計画承認の取得（最新）
            this.MonthlyWorkApprovalList = this.GetApprovalList();

            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            var isFirstApproval = this.MonthlyWorkApprovalList?.Where(x => x.FLAG_月頭月末 == 1)?.FirstOrDefault()?.FLAG_承認 == 1;
            var isEndApproval = this.MonthlyWorkApprovalList?.Where(x => x.FLAG_月頭月末 == 2)?.FirstOrDefault()?.FLAG_承認 == 1;

            // 見出し（パネル）
            this.SubTitlePanel.Visible = true;

            // 見出し（月度）
            this.NowMonthLabel.Text = DateTimeUtil.ConvertMonthString(this.BaseMonth);

            // 見出し（所属）
            this.AffiliationTextLabel.Text = this.AffiliationText;

            // 承認欄（ボタン）
            this.ApprovalFirstButton.Enabled = this.MonthFirstEnd == 1;
            this.ApprovalEndButton.Enabled = this.MonthFirstEnd == 2;

            var firston = isManagement
                // 月頭承認
                ? string.Format("{0}{1}", MonthFirstText, ApprovalText)
                // 月頭承認申請
                : string.Format("{0}{1}{2}", MonthFirstText, ApprovalText, ApplicationText);

            var firstoff = isManagement
                // 月頭承認解除
                ? string.Format("{0}{1}{2}", MonthFirstText, ApprovalText, ReleaseText)
                // 承認解除申請
                : string.Format("{0}{1}{2}", ApprovalText, ReleaseText, ApplicationText);

            var endon = isManagement
                // 月末承認
                ? string.Format("{0}{1}", MonthEndText, ApprovalText)
                // 月末承認申請
                : string.Format("{0}{1}{2}", MonthEndText, ApprovalText, ApplicationText);

            var endoff = isManagement
                // 月末承認解除
                ? string.Format("{0}{1}{2}", MonthEndText, ApprovalText, ReleaseText)
                // 承認解除申請
                : string.Format("{0}{1}{2}", ApprovalText, ReleaseText, ApplicationText);

            this.ApprovalFirstButton.Text = isFirstApproval ? firstoff : firston;
            this.ApprovalEndButton.Text = isEndApproval ? endoff : endon;

            // 承認欄（テーブル）
            this.ApprovalFirstLayoutPanel.Visible = !string.IsNullOrWhiteSpace(this.SectionGroupID);
            this.ApprovalFirstLayoutPanel.BackColor = this.MonthFirstEnd == 1 ? Color.White : Color.LightGray;
            this.ApprovalEndLayoutPanel.Visible = !string.IsNullOrWhiteSpace(this.SectionGroupID);
            this.ApprovalEndLayoutPanel.BackColor = this.MonthFirstEnd == 2 ? Color.White : Color.LightGray;
            this.ApprovalSubLayoutPanel.Visible = !string.IsNullOrWhiteSpace(this.SectionGroupID);

            // 承認欄：月頭（捺印）
            this.SetSealPicture(this.ApprovalFirstPictureBox, this.ApprovalFirstDateTextBox, this.ApprovalFirstSectionTextBox, this.ApprovalFirstLayoutPanel, 1);
            this.ApprovalFirstPictureBox.Visible = isFirstApproval;
            this.ApprovalFirstDateTextBox.Visible = isFirstApproval;
            this.ApprovalFirstSectionTextBox.Visible = isFirstApproval;

            // 承認欄：月末（捺印）
            this.SetSealPicture(this.ApprovalEndPictureBox, this.ApprovalEndDateTextBox, this.ApprovalEndSectionTextBox, this.ApprovalEndLayoutPanel, 2);
            this.ApprovalEndPictureBox.Visible = isEndApproval;
            this.ApprovalEndDateTextBox.Visible = isEndApproval;
            this.ApprovalEndSectionTextBox.Visible = isEndApproval;

            // 権限なし OR 課検索 
            if ((!isManagement && !isUpdate) || string.IsNullOrWhiteSpace(this.SectionGroupID))
            {
                this.ApprovalFirstLayoutPanel.Visible = false;
                this.ApprovalEndLayoutPanel.Visible = false;
                this.ApprovalSubLayoutPanel.Visible = false;

                this.ApprovalFirstPictureBox.Visible = false;
                this.ApprovalFirstDateTextBox.Visible = false;
                this.ApprovalFirstSectionTextBox.Visible = false;
                this.ApprovalEndPictureBox.Visible = false;
                this.ApprovalEndDateTextBox.Visible = false;
                this.ApprovalEndSectionTextBox.Visible = false;
            }
        }
        #endregion

        #region 承認印の表示
        /// <summary>
        /// 承認印の表示
        /// </summary>
        /// <returns></returns>
        private void SetSealPicture(PictureBox picturebox, TextBox datetextbox, TextBox sectiontextbox, TableLayoutPanel panel, int monthfirstend)
        {
            picturebox.Image = this.MonthFirstEnd == monthfirstend ? Resources.DateStamp : Resources.DateStampGray;
            picturebox.BackColor = this.MonthFirstEnd == 2 ? Color.White : Color.LightGray;

            sectiontextbox.Text = this.MonthlyWorkApprovalList?.Where(x => x.FLAG_月頭月末 == monthfirstend)?.FirstOrDefault()?.承認者_SECTION_CODE;
            sectiontextbox.Location = new Point(
                panel.Location.X + picturebox.Location.X + (picturebox.Size.Width / 5),
                panel.Location.Y + picturebox.Location.Y + (picturebox.Size.Height / 6));
            sectiontextbox.BackColor = this.MonthFirstEnd == monthfirstend ? Color.White : Color.LightGray;
            sectiontextbox.BringToFront();

            datetextbox.Text = DateTimeUtil.ConvertDateString(this.MonthlyWorkApprovalList?.FirstOrDefault(x => x.FLAG_月頭月末 == monthfirstend)?.承認日時);
            datetextbox.Location = new Point(
                panel.Location.X + picturebox.Location.X + (picturebox.Size.Width / 16),
                panel.Location.Y + picturebox.Location.Y + (picturebox.Size.Height / 3) + (picturebox.Size.Height / 8));
            datetextbox.BackColor = this.MonthFirstEnd == monthfirstend ? Color.White : Color.LightGray;
            datetextbox.BringToFront();
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
            FormControlUtil.FormWait(this, () => this.SetScheduleAll());
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
            FormControlUtil.FormWait(this, () => this.SetScheduleAll());
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
            var list = GetFavoriteList();

            if (list != null && list.Count() >= Const.FavoriteEntryMax)
            {
                //最大登録数以上の場合はエラー
                Messenger.Info(Resources.KKM00016);

                return;
            }

            var form = new MonthlyPlanFavoriteForm();

            // 所属区分（0：担当、1：課）
            var classkbn = '0';
            if (this.AffiliSectionRadioButton.Checked) classkbn = '1';

            // 表示対象（1：月頭、2：月末）
            var displaykbn = '1';
            if (this.MonthEndRadioButton.Checked) displaykbn = '2';

            var date = Convert.ToDateTime(this.BaseMonthDateTimePicker.Value);

            // パラメータ設定
            form.FAVORITE = new MonthlyWorkFavoriteItemModel
            {
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,
                // タイトル
                TITLE = string.Empty,
                // 所属区分（1：担当、2：課）
                CLASS_KBN = Convert.ToChar(classkbn),
                // 所属ID
                CLASS_ID = this.AffiliationComboBox.SelectedValue.ToString(),
                // ステータス_OPENフラグ（1：チェック有）
                STATUS_OPEN_FLG = this.StatusOpenCheckBox.Checked ? '1' : '0',
                // ステータス_CLOSEフラグ（1：チェック有）
                STATUS_CLOSE_FLG = this.StatusCloseCheckBox.Checked ? '1' : '0',
                // 指定月
                TARGET_MONTH = new DateTime(date.Year, date.Month, 1),
                // 表示対象（1：月頭、2：月末）
                DISPLAY_KBN = Convert.ToChar(displaykbn)
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

            this.AffiliationTextTemp = SessionDto.DepartmentName + "　" + SessionDto.SectionName + "　" + SessionDto.SectionGroupName;
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

            this.AffiliationTextTemp = SessionDto.DepartmentName + "　" + SessionDto.SectionName;
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

            this.AffiliationTextTemp = string.Empty;
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
                    if (form.ShowDialog() == DialogResult.OK && form.SECTION_GROUP_ID != null)
                    {
                        // パラメータ取得
                        var code = form.DEPARTMENT_CODE + "　" + form.SECTION_CODE + "　" + form.SECTION_GROUP_CODE;
                        var id = form.SECTION_GROUP_ID;

                        // コンボボックス設定
                        this.AffiliationComboBox.DataSource =
                            new List<ComboBoxDto> { new ComboBoxDto() { CODE = code, ID = id } };
                        this.AffiliationComboBox.SelectedValue = id;

                        this.AffiliationTextTemp = form.DEPARTMENT_NAME + "　" + form.SECTION_NAME + "　" + form.SECTION_GROUP_NAME;

                    }

                }

            }
            // 課選択時
            else if (this.AffiliSectionRadioButton.Checked)
            {
                this.AffiliSectionRadioButton.Focus();

                var form = new Common.SectionListForm();

                // パラメータ設定
                form.DEPARTMENT_ID = SessionDto.DepartmentID;

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

                    this.AffiliationTextTemp = form.DEPARTMENT_NAME + "　" + form.SECTION_NAME;
                }

                form.Dispose();
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
                var startDay = baseDay;
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
        private void MonthlyPlanCalendarGrid_FirstDateInViewChanged(object sender, EventArgs e)
        {
            //スケジュール設定
            FormControlUtil.FormWait(this, () => this.SetSchedule());
        }
        #endregion

        #region 月頭承認（月頭承認解除）ボタンのクリック
        /// <summary>
        /// 月頭承認（月頭承認解除）ボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApprovalFirstButton_Click(object sender, EventArgs e)
        {
            // 月次計画承認の取得（最新）
            this.MonthlyWorkApprovalList = this.GetApprovalList();

            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            var isFirstApproval = this.MonthlyWorkApprovalList?.Where(x => x.FLAG_月頭月末 == 1)?.FirstOrDefault()?.FLAG_承認 == 1;
            var isEndApproval = this.MonthlyWorkApprovalList?.Where(x => x.FLAG_月頭月末 == 2)?.FirstOrDefault()?.FLAG_承認 == 1;

            var on = isManagement
                // 月頭承認
                ? string.Format("{0}{1}", MonthFirstText, ApprovalText)
                // 月頭承認申請
                : string.Format("{0}{1}{2}", MonthFirstText, ApprovalText, ApplicationText);

            var off = isManagement
                // 月頭承認解除
                ? string.Format("{0}{1}{2}", MonthFirstText, ApprovalText, ReleaseText)
                // 承認解除申請
                : string.Format("{0}{1}{2}", ApprovalText, ReleaseText, ApplicationText);

            // 管理権限あり
            if (isManagement)
            {
                var ret = false;

                // 月頭承認処理
                if (this.ApprovalFirstButton.Text.Equals(on))
                {
                    if (Messenger.Confirm(Resources.KKM00010)   // 承認確認
                        .Equals(DialogResult.No)) return;

                    // 実行
                    FormControlUtil.FormWait(this, () => ret = this.ApprovalPost(new MonthlyWorkApprovalPostInModel
                    {
                        FLAG_承認 = 1,    // 1：承認
                        FLAG_月頭月末 = 1,  // 1：月頭
                        SECTION_GROUP_ID = this.SectionGroupID,
                        対象月 = this.BaseMonth,
                        承認者_PERSONEL_ID = SessionDto.UserId
                    }
                    ));

                    if (!ret) return;
                }
                // 月頭承認解除処理
                else
                {
                    // 月末がすでに承認されている場合はエラー
                    if (isEndApproval)
                    {
                        Messenger.Warn(Resources.KKM03019);

                        return;
                    }

                    if (Messenger.Confirm(Resources.KKM00012)   // 解除確認
                        .Equals(DialogResult.No)) return;

                    // 実行
                    FormControlUtil.FormWait(this, () => ret = this.ApprovalPost(new MonthlyWorkApprovalPostInModel
                    {
                        FLAG_承認 = 0,    // 0：解除
                        FLAG_月頭月末 = 1,  // 1：月頭
                        SECTION_GROUP_ID = this.SectionGroupID,
                        対象月 = this.BaseMonth,
                        承認者_PERSONEL_ID = SessionDto.UserId
                    }
                    ));

                    if (!ret) return;
                }
            }
            // 管理権限なし
            else
            {
                // 月頭承認申請処理
                if (this.ApprovalFirstButton.Text.Equals(on))
                {
                    // メーラー起動
                    Process.Start(string.Format(string.Format("mailto:{0}?body={1}", MailAddress, MailBodyApproval), this.BaseMonth.Month));
                }
                // 承認解除申請処理
                else
                {
                    // 月末がすでに承認されている場合はエラー
                    if (isEndApproval)
                    {
                        Messenger.Warn(Resources.KKM03019);

                        return;
                    }

                    // メーラー起動
                    Process.Start(string.Format(string.Format("mailto:{0}?body={1}", MailAddress, MailBodyRelease), this.BaseMonth.Month));
                }
            }

            // 動的フォームの再読込
            this.SetForm();
        }
        #endregion

        #region 月末承認（月末承認解除）ボタンのクリック
        /// <summary>
        /// 月末承認（月末承認解除）ボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApprovalEndButton_Click(object sender, EventArgs e)
        {
            // 月次計画承認の取得（最新）
            this.MonthlyWorkApprovalList = this.GetApprovalList();

            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            var isFirstApproval = this.MonthlyWorkApprovalList?.Where(x => x.FLAG_月頭月末 == 1)?.FirstOrDefault()?.FLAG_承認 == 1;
            var isEndApproval = this.MonthlyWorkApprovalList?.Where(x => x.FLAG_月頭月末 == 2)?.FirstOrDefault()?.FLAG_承認 == 1;

            var on = isManagement 
                // 月末承認
                ? string.Format("{0}{1}", MonthEndText, ApprovalText)
                // 月末承認申請
                : string.Format("{0}{1}{2}", MonthEndText, ApprovalText, ApplicationText);

            var off = isManagement
                // 月末承認解除
                ? string.Format("{0}{1}{2}", MonthEndText, ApprovalText, ReleaseText)
                // 承認解除申請
                : string.Format("{0}{1}{2}", ApprovalText, ReleaseText, ApplicationText);

            // 管理権限あり
            if (isManagement)
            {
                var ret = false;

                // 月末承認処理
                if (this.ApprovalEndButton.Text.Equals(on))
                {
                    if (Messenger.Confirm(Resources.KKM00010)   // 承認確認
                        .Equals(DialogResult.No)) return;

                    // 実行
                    FormControlUtil.FormWait(this, () => ret = this.ApprovalPost(new MonthlyWorkApprovalPostInModel
                    {
                        FLAG_承認 = 1,    // 1：承認
                        FLAG_月頭月末 = 2,  // 2：月末
                        SECTION_GROUP_ID = this.SectionGroupID,
                        対象月 = this.BaseMonth,
                        承認者_PERSONEL_ID = SessionDto.UserId
                    }
                    ));

                    if (!ret) return;
                }
                // 月末承認解除処理
                else
                {
                    if (Messenger.Confirm(Resources.KKM00012)   // 解除確認
                        .Equals(DialogResult.No)) return;

                    // 実行
                    FormControlUtil.FormWait(this, () => ret = this.ApprovalPost(new MonthlyWorkApprovalPostInModel
                    {
                        FLAG_承認 = 0,    // 0：解除
                        FLAG_月頭月末 = 2,  // 2：月末
                        SECTION_GROUP_ID = this.SectionGroupID,
                        対象月 = this.BaseMonth,
                        承認者_PERSONEL_ID = SessionDto.UserId
                    }
                    ));

                    if (!ret) return;
                }
            }
            // 管理権限なし
            else
            {
                // 月末承認申請処理
                if (this.ApprovalEndButton.Text.Equals(on))
                {
                    // メーラー起動
                    Process.Start(string.Format(string.Format("mailto:{0}?body={1}", MailAddress, MailBodyApproval), this.BaseMonth.Month));
                }
                // 承認解除申請処理
                else
                {
                    // メーラー起動
                    Process.Start(string.Format(string.Format("mailto:{0}?body={1}", MailAddress, MailBodyRelease), this.BaseMonth.Month));
                }
            }

            // 動的フォームの再読込
            this.SetForm();
        }
        #endregion

        #region 承認・解除処理
        /// <summary>
        /// 承認・解除処理
        /// </summary>
        private bool ApprovalPost(MonthlyWorkApprovalPostInModel data)
        {
            if (data == null) return false;

            // API 実行
            var res = HttpUtil.PostResponse<MonthlyWorkSchedulePostInModel>(ControllerType.MonthlyWorkApproval, data);

            if (res.Status.Equals(Const.StatusSuccess))
            {
                // 完了メッセージ
                if (data.FLAG_承認 == 1)
                {
                    Messenger.Info(Resources.KKM00011); // 承認完了
                }
                else
                {
                    Messenger.Info(Resources.KKM00013); // 解除完了
                }

                return true;
            }

            return false;
        }
        #endregion

        #region 業務計画同期ボタンのクリック
        /// <summary>
        /// 業務計画同期ボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SynchronismButton_Click(object sender, EventArgs e)
        {
            // 確認メッセージ
            if (Messenger.Confirm(Resources.KKM00014)   // 同期確認
                .Equals(DialogResult.No)) return;

            var ret = false;

            // 同期処理
            FormControlUtil.FormWait(this, () => ret = this.SynchronizePost());

            if (ret)
            {
                // スケジュール設定（項目含む）
                FormControlUtil.FormWait(this, () => this.SetScheduleAll());
            }
        }
        #endregion

        #region 業務計画同期処理
        /// <summary>
        /// 業務計画同期処理
        /// </summary>
        private bool SynchronizePost()
        {
            if (string.IsNullOrWhiteSpace(this.SectionGroupID))
            {
                // 課選択の場合は同期不可
                Messenger.Info(Resources.KKM03024);

                return false;
            }

            if (!string.IsNullOrWhiteSpace(this.SectionGroupID))
            {
                // 承認済みかどうか
                var appdata = this.GetApprovalData(this.SectionGroupID, this.BaseMonth, this.MonthFirstEnd);

                if (appdata?.FLAG_承認 == 1)
                {
                    // 承認済みの場合は同期不可
                    Messenger.Info(Resources.KKM03023);

                    return false;
                }
            }

            var data = new MonthlyWorkSynchronizePostInModel
            {
                // 所属ID
                SECTION_ID = this.SectionID,
                // 所属グループID
                SECTION_GROUP_ID = this.SectionGroupID,
                // 対象月
                対象月 = this.BaseMonth,
                // 月頭月末
                FLAG_月頭月末 = this.MonthFirstEnd,
                // パーソナルID
                PERSONEL_ID = SessionDto.UserId
            };

            // API 実行
            var res = HttpUtil.PostResponse<MonthlyWorkSchedulePostInModel>(ControllerType.MonthlyWorkSynchronize, data);

            if (res.Status.Equals(Const.StatusSuccess))
            {
                // 完了メッセージ
                Messenger.Info(Resources.KKM00015); // 同期完了

                return true;
            }

            return false;
        }
        #endregion

        #endregion

        #region カレンダーグリッドイベント

        #region 進捗履歴の起動
        /// <summary>
        /// 進捗履歴の起動
        /// </summary>
        private void OpenProgressListForm(ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel> item)
        {
            if (item.ScheduleItem?.FLAG_月報専用項目 == 1)
            {
                // 月次の場合は進捗履歴なし
                Messenger.Warn(Resources.KKM03011);

                return;
            }

            // スケジュール項目のチェック
            if (!this.IsEntryScheduleItem(ScheduleItemEditType.Update, item.ID)) return;

            // 進捗履歴画面表示
            using (var form = new WorkProgressHistroryForm { ScheduleItem = new WorkProgressItemModel { ID = (long)item.ScheduleItem.DEV_CATEGORY_ID }, IsMonthlyPlan = true, UserAuthority = this.UserAuthority })
            {
                form.ShowDialog(this);
            }
        }
        #endregion

        #region スケジュール項目詳細の起動
        /// <summary>
        /// スケジュール項目詳細の起動
        /// </summary>
        private void OpenScheduleItemForm(ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel> item, ScheduleItemEditType type)
        {
            // スケジュール項目のチェック
            if (!this.IsEntryScheduleItem(type, item.ID)) return;

            item.ScheduleItemEdit = type;

            var form = new MonthlyPlanItemForm();

            // パラメータ設定
            form.ITEM = item;
            form.BASE_MONTH = this.BaseMonth;
            form.MONTH_FIRST_END = this.MonthFirstEnd;
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
        private void OpenItemCopyMoveForm(ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel> item)
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
        private void ChangeScheduleItemSort(ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel> sitem, ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel> ditem)
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

                    // 承認済みかどうか
                    var data = this.GetApprovalData(item.SECTION_GROUP_ID, this.BaseMonth, this.MonthFirstEnd);

                    if (data?.FLAG_承認 == 1)
                    {
                        //承認済みの場合は変更不可
                        Messenger.Info(Resources.KKM03020);

                        return false;
                    }

                    break;
            }

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                case ScheduleItemEditType.Delete:       //削除

                    //対象項目にスケジュールがある場合はエラー
                    if (this.ScheduleListGet(new MonthlyWorkScheduleGetInModel { CATEGORY_ID = scheduleid })?.Count() > 0)
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
        /// <returns>MonthlyWorkScheduleItemGetOutModel</returns>
        private MonthlyWorkScheduleItemGetOutModel ScheduleItemGet(long scheduleid)
        {
            MonthlyWorkScheduleItemGetOutModel item = null;

            //検索条件
            var cond = new MonthlyWorkScheduleItemGetInModel { SCHEDULE_ID = scheduleid };

            //APIで取得
            var res = HttpUtil.GetResponse<MonthlyWorkScheduleItemGetInModel, MonthlyWorkScheduleItemGetOutModel>(ControllerType.MonthlyWorkScheduleItem, cond);

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
        private void ScheduleItemPut(ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel> item, ScheduleItemEditType type)
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
                    if (this.ScheduleListGet(new MonthlyWorkScheduleGetInModel { CATEGORY_ID = item.ID, PARALLEL_INDEX_GROUP = item.RowCount })?.Count() > 0)
                    {
                        Messenger.Info(Resources.KKM00033);

                        return;
                    }

                    if (Messenger.Confirm(Resources.KKM00007)   // 削除確認
                        .Equals(DialogResult.No)) return;

                    item.RowCount--;
                }

                var data = new MonthlyWorkScheduleItemPutInModel
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
                    PERSONEL_ID = SessionDto.UserId,
                    // 担当者
                    担当者 = item.ScheduleItem.担当者,
                    // 備考
                    備考 = item.ScheduleItem.備考
                };

                var res = HttpUtil.PutResponse<MonthlyWorkScheduleItemPutInModel>(ControllerType.MonthlyWorkScheduleItem, data);

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
        private void ScheduleItemDelete(ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel> item)
        {
            if (Messenger.Confirm(Resources.KKM00007)   // 削除確認
             .Equals(DialogResult.No)) return;

            // スケジュール項目削除
            if (item != null && item.ID > 0)
            {
                // スケジュール項目のチェック
                if (!this.IsEntryScheduleItem(ScheduleItemEditType.Delete, item.ID)) return;

                var data = new MonthlyWorkScheduleItemDeleteInModel
                {
                    // カテゴリーID
                    CATEGORY_ID = item.ID
                };

                var res = HttpUtil.DeleteResponse<MonthlyWorkScheduleItemDeleteInModel>(ControllerType.MonthlyWorkScheduleItem, data);

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
        private void OpenScheduleForm(ScheduleModel<MonthlyWorkScheduleGetOutModel> schedule, ScheduleEditType type)
        {
            // スケジュールのチェック
            if (!this.IsEntrySchedule(type, schedule)) return;

            schedule.ScheduleEdit = type;

            var form = new MonthlyPlanScheduleForm();

            // パラメータ設定
            form.SCHEDULE = schedule;
            form.IS_UPDATE = this.UserAuthority.UPDATE_FLG == '1';
            form.BASE_MONTH = this.BaseMonth;
            form.MONTH_FIRST_END = this.MonthFirstEnd;

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
        private bool IsEntrySchedule(ScheduleEditType type, ScheduleModel<MonthlyWorkScheduleGetOutModel> schedule)
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

            // 承認済みかどうか
            var data = this.GetApprovalData(item.SECTION_GROUP_ID, this.BaseMonth, this.MonthFirstEnd);

            if (data?.FLAG_承認 == 1)
            {
                //承認済みの場合は変更不可
                Messenger.Info(Resources.KKM03020);

                //スケジュール設定
                FormControlUtil.FormWait(this, () => this.SetSchedule());

                ////変更したスケジュール項目へ縦スクロールを設定→廃止しました
                //this.gridUtil.SetScheduleRowHeaderFirst(item.CATEGORY_ID);

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
                    var cond = new MonthlyWorkScheduleGetInModel
                    {
                        //カテゴリーID
                        CATEGORY_ID = schedule.CategoryID,
                        //期間(From)
                        DATE_START = this.MonthlyPlanCalendarGrid.FirstDateInView.Date,
                        //期間(To)
                        DATE_END = this.MonthlyPlanCalendarGrid.LastDateInView.Date,
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
        /// <returns>MonthlyWorkScheduleGetOutModel</returns>
        private MonthlyWorkScheduleGetOutModel ScheduleGet(long scheduleid)
        {
            MonthlyWorkScheduleGetOutModel schedule = null;

            //パラメータ設定
            var cond = new MonthlyWorkScheduleGetInModel
            {
                // スケジュールID
                SCHEDULE_ID = scheduleid
            };

            //APIで取得
            var res = HttpUtil.GetResponse<MonthlyWorkScheduleGetInModel, MonthlyWorkScheduleGetOutModel>(ControllerType.MonthlyWorkSchedule, cond);

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
        /// <returns>MonthlyWorkScheduleGetOutModel</returns>
        private List<MonthlyWorkScheduleGetOutModel> ScheduleListGet(MonthlyWorkScheduleGetInModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<MonthlyWorkScheduleGetInModel, MonthlyWorkScheduleGetOutModel>(ControllerType.MonthlyWorkSchedule, cond);

            return (res.Results).ToList();
        }
        #endregion

        #region スケジュールの登録
        /// <summary>
        /// スケジュールの登録
        /// </summary>
        private void SchedulePost(ScheduleModel<MonthlyWorkScheduleGetOutModel> schedule)
        {
            if (schedule == null) return;

            // スケジュールのチェック
            if (!this.IsEntrySchedule(ScheduleEditType.Insert, schedule)) return;

            var data = new MonthlyWorkSchedulePostInModel
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

            var res = HttpUtil.PostResponse<MonthlyWorkSchedulePostInModel>(ControllerType.MonthlyWorkSchedule, data);

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
        private void SchedulePut(ScheduleModel<MonthlyWorkScheduleGetOutModel> schedule)
        {
            // スケジュール登録
            if (schedule != null && schedule.ID > 0)
            {
                // スケジュールのチェック
                if (!this.IsEntrySchedule(ScheduleEditType.Update, schedule)) return;

                var data = new MonthlyWorkSchedulePutInModel
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

                var res = HttpUtil.PutResponse<MonthlyWorkSchedulePutInModel>(ControllerType.MonthlyWorkSchedule, data);

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
        private void ScheduleDelete(ScheduleModel<MonthlyWorkScheduleGetOutModel> schedule, bool confirm)
        {
            if (confirm)
                if (Messenger.Confirm(Resources.KKM00007)   // 削除確認
                .Equals(DialogResult.No)) return;

            // スケジュール削除
            if (schedule != null && schedule.ID > 0)
            {
                // スケジュールのチェック
                if (!this.IsEntrySchedule(ScheduleEditType.Delete, schedule)) return;

                var data = new MonthlyWorkScheduleDeleteInModel
                {
                    // スケジュールID
                    SCHEDULE_ID = schedule.ID
                };

                var res = HttpUtil.DeleteResponse<MonthlyWorkScheduleDeleteInModel>(ControllerType.MonthlyWorkSchedule, data);

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
        private string GetRowHeaderOtherValue(ScheduleItemModel<MonthlyWorkScheduleItemGetOutModel> item, int col)
        {
            // 列「開発符号」：開発符号
            if (col == 1) return item.ScheduleItem.GENERAL_CODE;
            // 列「担当」：担当コード
            if (col == 2) return item.ScheduleItem.SECTION_GROUP_CODE;
            // 列「担当者」：担当者
            if (col == 3) return item.ScheduleItem.担当者;
            // 列「備考」：備考
            if (col == 4) return item.ScheduleItem.備考;

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
        private MonthlyWorkFavoriteSearchOutModel GetFavoriteData()
        {
            var list = new List<MonthlyWorkFavoriteSearchOutModel>();

            //パラメータ設定
            var itemCond = new MonthlyWorkFavoriteSearchInModel
            {
                // お気に入りID
                ID = Convert.ToInt64(this.FavoriteID),
            };

            //Get実行
            var res = HttpUtil.GetResponse<MonthlyWorkFavoriteSearchInModel, MonthlyWorkFavoriteSearchOutModel>(ControllerType.MonthlyWorkFavorite, itemCond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                list.AddRange(res.Results);
            }

            return list.FirstOrDefault();
        }
        #endregion

        #region 開発符号検索
        /// <summary>
        /// 開発符号検索
        /// </summary>
        private List<GeneralCodeSearchOutModel> GetGeneralCodeList()
        {
            //パラメータ設定
            var itemCond = new GeneralCodeSearchInModel
            {
                // 開発中フラグ
                UNDER_DEVELOPMENT = "1",
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId
            };

            //Get実行
            var res = HttpUtil.GetResponse<GeneralCodeSearchInModel, GeneralCodeSearchOutModel>(ControllerType.GeneralCode, itemCond);

            return (res.Results).ToList();
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
        /// <returns>IEnumerable<MonthlyWorkScheduleItemGetOutModel></returns>
        private IEnumerable<MonthlyWorkScheduleItemGetOutModel> GetScheduleItemList()
        {
            // 所属
            string sectionid = this.AffiliSectionRadioButton.Checked
                ? this.AffiliationComboBox.SelectedValue.ToString() : null;
            string sectiongroupid = this.AffiliSectionGroupRadioButton.Checked
                ? this.AffiliationComboBox.SelectedValue.ToString() : null;

            // 状態（0：Openのみチェック、1：Closeのみチェック、2：Open,Close両方チェック）
            int? opencloseflag = null;
            if (this.StatusOpenCheckBox.Checked && this.StatusCloseCheckBox.Checked)
            {
                opencloseflag = 2;
            }
            else if (this.StatusOpenCheckBox.Checked)
            {
                opencloseflag = 0;
            }
            else if (this.StatusCloseCheckBox.Checked)
            {
                opencloseflag = 1;
            }

            var cond = new MonthlyWorkScheduleItemGetInModel
            {
                // 所属
                SECTION_ID = sectionid,
                // 所属グループID
                SECTION_GROUP_ID = sectiongroupid,
                // 状態（Open/Close）
                OPEN_CLOSE_FLAG = opencloseflag,
                // 対象月
                対象月 = this.BaseMonth,
                // 表示対象
                FLAG_月頭月末 = this.MonthFirstRadioButton.Checked ? 1 : 2
            };

            //APIで取得
            var res = HttpUtil.GetResponse<MonthlyWorkScheduleItemGetInModel, MonthlyWorkScheduleItemGetOutModel>(ControllerType.MonthlyWorkScheduleItem, cond);

            //返却
            return res.Results;
        }

        /// <summary>
        /// スケジュール取得
        /// </summary>
        /// <returns>IEnumerable<MonthlyWorkScheduleGetOutModel></returns>
        private IEnumerable<MonthlyWorkScheduleGetOutModel> GetScheduleList()
        {
            var itemList = this.ScheduleItemList;

            // 所属
            string sectionid = this.AffiliSectionRadioButton.Checked
                ? this.AffiliationComboBox.SelectedValue.ToString() : null;
            string sectiongroupid = this.AffiliSectionGroupRadioButton.Checked
                ? this.AffiliationComboBox.SelectedValue.ToString() : null;

            // 状態（0：Openのみチェック、1：Closeのみチェック、2：Open,Close両方チェック）
            int? opencloseflag = null;
            if (this.StatusOpenCheckBox.Checked && this.StatusCloseCheckBox.Checked)
            {
                opencloseflag = 2;
            }
            else if (this.StatusOpenCheckBox.Checked)
            {
                opencloseflag = 0;
            }
            else if (this.StatusCloseCheckBox.Checked)
            {
                opencloseflag = 1;
            }

            var cond = new MonthlyWorkScheduleGetInModel
            {
                // 所属
                SECTION_ID = sectionid,
                // 所属グループID
                SECTION_GROUP_ID = sectiongroupid,
                // 状態（Open/Close）
                OPEN_CLOSE_FLAG = opencloseflag,
                // 対象月
                対象月 = this.BaseMonth,
                // 表示対象
                FLAG_月頭月末 = this.MonthFirstRadioButton.Checked ? 1 : 2,
                // 期間(From)
                DATE_START = this.MonthlyPlanCalendarGrid.FirstDateInView.Date,
                // 期間(To)
                DATE_END = this.MonthlyPlanCalendarGrid.LastDateInView.Date
            };

            //APIで取得
            var res = HttpUtil.GetResponse<MonthlyWorkScheduleGetInModel, MonthlyWorkScheduleGetOutModel>(ControllerType.MonthlyWorkSchedule, cond);

            //返却
            return res.Results;
        }
        #endregion

        #region 承認検索
        /// <summary>
        /// 承認検索
        /// </summary>
        private List<MonthlyWorkApprovalGetOutModel> GetApprovalList()
        {
            //パラメータ設定
            var itemCond = new MonthlyWorkApprovalGetInModel
            {
                // 所属グループID
                SECTION_GROUP_ID = !string.IsNullOrWhiteSpace(this.SectionGroupID)
                    ? this.SectionGroupID : SessionDto.SectionGroupID,
                // 対象月
                対象月 = this.BaseMonth != null && this.BaseMonth > DateTime.MinValue
                    ? this.BaseMonth : new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                // 月頭月末
                FLAG_月頭月末 = null
            };

            //Get実行
            var res = HttpUtil.GetResponse<MonthlyWorkApprovalGetInModel, MonthlyWorkApprovalGetOutModel>(ControllerType.MonthlyWorkApproval, itemCond);

            return (res.Results).ToList();
        }
        #endregion

        #region 承認データの取得
        /// <summary>
        /// 承認データの取得
        /// </summary>
        private MonthlyWorkApprovalGetOutModel GetApprovalData(string sectiongroupid, DateTime basemonth, int monthfirstend)
        {
            var list = new List<MonthlyWorkApprovalGetOutModel>();

            //パラメータ設定
            var itemCond = new MonthlyWorkApprovalGetInModel
            {
                // 所属グループID
                SECTION_GROUP_ID = sectiongroupid,
                // 対象月
                対象月 = basemonth,
                // 月頭月末
                FLAG_月頭月末 = monthfirstend
            };

            //Get実行
            var res = HttpUtil.GetResponse<MonthlyWorkApprovalGetInModel, MonthlyWorkApprovalGetOutModel>(ControllerType.MonthlyWorkApproval, itemCond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                list.AddRange(res.Results);
            }

            return list.FirstOrDefault();
        }
        #endregion

        #endregion

        #region その他

        #region お気に入りチェック
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

        #endregion
    }
}
