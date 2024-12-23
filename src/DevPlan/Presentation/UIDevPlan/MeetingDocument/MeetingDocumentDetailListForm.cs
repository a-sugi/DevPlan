using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.UIDevPlan.MeetingDocument
{
    /// <summary>
    /// 検討会資料詳細
    /// </summary>
    public partial class MeetingDocumentDetailListForm : BaseForm
    {
        #region メンバ変数
        private const int CondHeight = 105;

        private const int MeetingDocumentRowCount = 1;

        private const string StatusOpen = "open";
        private const string StatusClose = "close";

        private readonly string[] Jyoukyou = { "", "○", "△", "□" };

        private readonly Color SituationColumnHeaderColor = Color.FromArgb(0xFF, 0xF7, 0xAD);

        private const string KousinKikanFormat = "{0:yyyy/MM/dd}～{1:yyyy/MM/dd}";

        private readonly Dictionary<string, string> StatusMap = new Dictionary<string, string>
        {
            { "open", "OPEN" },
            { "close", "CLOSE" }

        };

        private const string JyoukyouHelpMsg = @"○(開発完了)
原理原則に基づき課題が解決され、バラつきがあっても目標達成できる具体的な手段、数値が図面に織込まれ、出図が完了している

△(設計責任)
課題解決のメカニズムが明確になっており、図面に落とし込める具体的な手段、数値が実験より提示されているが、出図、設変されていない

□(実験責任)
課題解決のメカニズムが明確になっていない、メカニズムが明確になっても、図面に落とし込める具体的な手段、数値が提示できていない";

        private long siryouID = -1;

        private Dictionary<string, bool> gridViewColumnMap = new Dictionary<string, bool>();

        private List<string> eikyouBuhinSyutuzuNitteiShowColumn;

        private BindingSource meetingDocumentDetailDataSource = new BindingSource();

        private Dictionary<string, string> headerNameMap = new Dictionary<string, string>();

        private List<DataGridViewComboBoxColumn> comboBoxList = new List<DataGridViewComboBoxColumn>();
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "検討会資料詳細"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>検討会資料</summary>
        public MeetingDocumentModel MeetingDocument { get; set; }

        /// <summary>検討会資料詳細検索条件</summary>
        private MeetingDocumentDetailSearchModel MeetingDocumentDetailSearchCond { get; set; }

        /// <summary>検討会資料詳細リスト</summary>
        private List<MeetingDocumentDetailModel> MeetingDocumentDetailList { get; set; } = new List<MeetingDocumentDetailModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MeetingDocumentDetailListForm()
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
        private void MeetingDocumentDetailListForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //権限
                this.UserAuthority = base.GetFunction(FunctionID.ConsiderationDocument);

                //データグリッドビューの初期化
                this.InitDataGridView();

                //画面初期化
                this.InitForm();

                //検討会資料設定
                this.SetMeetingDocument();

                //検討会資料詳細一覧設定
                this.SearcMeetingDocumentDetailList();

            });

        }

        /// <summary>
        /// データグリッドビューの初期化
        /// </summary>
        private void InitDataGridView()
        {
            //列の自動生成可否
            this.MeetingDocumentDetailDataGridView.AutoGenerateColumns = false;

            //データーソース
            this.MeetingDocumentDetailDataGridView.DataSource = this.meetingDocumentDetailDataSource;

            //ダブルバァッファリング有効化
            var type = typeof(DataGridView);
            var pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.MeetingDocumentDetailDataGridView, true);

            foreach (DataGridViewColumn col in this.MeetingDocumentDetailDataGridView.Columns)
            {
                //ヘッダーの改行コードを置き換え
                var headerText = col.HeaderText;
                col.HeaderText = headerText.Replace(@"\n", Const.CrLf);

                //ヘッダー名を保持
                this.headerNameMap[col.Name] = col.HeaderText;

                //改行がある場合は折り返し
                if (col.HeaderText != headerText)
                {
                    col.HeaderCell.Style.WrapMode = DataGridViewTriState.True;

                }

                //コンボボックスの列かどうか
                if (col is DataGridViewComboBoxColumn)
                {
                    this.comboBoxList.Add(col as DataGridViewComboBoxColumn);

                }

            }

            //列ヘッダーの高さを再設定
            var height = this.MeetingDocumentDetailDataGridView.ColumnHeadersHeight;
            this.MeetingDocumentDetailDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.MeetingDocumentDetailDataGridView.ColumnHeadersHeight = height;

            //列ヘッダーの幅で再設定
            this.MeetingDocumentDetailDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);

            //ステータス
            this.StatusColumn.ValueMember = "Key";
            this.StatusColumn.DisplayMember = "Value";
            this.StatusColumn.DataSource = this.StatusMap.ToArray();

            //項目番号(部記号)
            this.DepartmentSymbolColumn.ValueMember = "部記号";
            this.DepartmentSymbolColumn.DisplayMember = "部記号";
            this.DepartmentSymbolColumn.DataSource = (new[] { new MeetingDocumentDeptModel() }).Concat(HttpUtil.GetResponse<MeetingDocumentDeptModel>(ControllerType.MeetingDocumentDept)?.Results).ToList();

            //状況
            this.SituationColumn.DataSource = Jyoukyou;
            this.SituationColumn.HeaderCell.Style.BackColor = SituationColumnHeaderColor;

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            //検討会資料一覧ボタン
            this.MeetingDocumentListButton.Visible = isManagement;

            //ダウンロードボタン
            this.DownloadButton.Visible = isExport;

            //検討会資料一覧の列ごとの編集可否取得
            foreach (DataGridViewColumn col in this.MeetingDocumentDetailDataGridView.Columns)
            {
                this.gridViewColumnMap[col.Name] = col.ReadOnly;

            }

            //影響部品・出図日程入力画面表示対象の列
            this.eikyouBuhinSyutuzuNitteiShowColumn = new List<string>
            {
                //設計部署(部)
                this.DesignDepartmentColumn.Name,

                //影響する(しそうな)部品
                this.AffectedPartsColumn.Name,

                //出図日程
                this.DrawingScheduleColumn.Name,

                //コスト変動見通し(円)
                this.CostColumn.Name,

                //質量変動見通し(g)
                this.MassColumn.Name,

                //投資変動見通し(百万円)
                this.InvestmentColumn.Name

            };

        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailListForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.MeetingDocumentDetailDataGridView.CurrentCell = null;

        }
        #endregion

        #region 検索条件ボタンクリック
        /// <summary>
        /// 検索条件ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            //検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, CondHeight);

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

            //開発符号セット
            this.SetGeneralCode(this.ShowGeneralCodeListForm());

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

        }
        #endregion

        #region 担当課マウスクリック
        /// <summary>
        /// 担当課マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            using (var form = new SectionGroupListForm { DEPARTMENT_ID = SessionDto.DepartmentID, SECTION_ID = SessionDto.SectionID })
            {
                //担当検索画面がOKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //担当課をセット
                    this.SetDepartment(form.SectionGroup);

                }

            }

        }

        /// <summary>
        /// 担当課を設定
        /// </summary>
        /// <param name="sectionGroup">担当</param>
        private void SetDepartment(SectionGroupModel sectionGroup)
        {
            var value = new ComboBoxDto
            {
                CODE = sectionGroup.SECTION_CODE,
                NAME = string.Format("{0} {1} {2}", sectionGroup.DEPARTMENT_CODE, sectionGroup.SECTION_CODE, sectionGroup.SECTION_GROUP_CODE)

            };

            //担当をセット
            FormControlUtil.SetComboBoxItem(this.DepartmentComboBox, new[] { value }, false);
            this.DepartmentComboBox.SelectedIndex = 0;

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
            FormControlUtil.FormWait(this, () =>
            {
                //入力がOKかどうか
                var msg = Validator.GetFormInputErrorMessage(this);
                if (msg != "")
                {
                    Messenger.Warn(msg);
                    return;

                }

                //画面を変更していて登録するかどうか
                if (this.IsEditMeetingDocumentDetailEntry() == true)
                {
                    //検討会資料詳細一覧設定
                    this.SearcMeetingDocumentDetailList();

                }

            });

        }
        #endregion

        #region 行読込ボタンクリック
        /// <summary>
        /// 行読込ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowReadButton_Click(object sender, EventArgs e)
        {
            using (var form = new ReportMaterialListForm())
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //検討会資料詳細追加
                    this.AddMeetingDocumentDetail(form.MeetingDocumentDetailList);
                    
                }

            }

        }
        #endregion

        #region 行追加ボタンクリック
        /// <summary>
        /// 行追加ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowAddButton_Click(object sender, EventArgs e)
        {
            //検討会資料詳細追加
            this.AddMeetingDocumentDetail();

        }
        #endregion

        #region 行削除ボタンクリック
        /// <summary>
        /// 行削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowDeleteButton_Click(object sender, EventArgs e)
        {
            var cell = this.MeetingDocumentDetailDataGridView.CurrentCell;

            var rows = this.MeetingDocumentDetailDataGridView.SelectedRows;

            //一覧を選択しているかどうか
            if (cell == null && rows.Count == 0)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            //行選択なら行からセルを取得
            if (cell == null)
            {
                cell = rows[0].Cells[0];

            }

            //削除可否を問い合わせ
            if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
            {
                return;

            }

            var detail = GetMeetingDocumentDetailByDataGridView(cell.RowIndex);

            //削除フラグと編集フラグを有効化
            detail.EDIT_FLG = true;
            detail.DELETE_FLG = true;

            //再バインド
            this.SetMeetingDocumentDetailList();

            //削除完了メッセージ
            Messenger.Info(Resources.KKM00003);

        }
        #endregion

        #region メール作成ボタンクリック
        /// <summary>
        /// メール作成ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailCreateButton_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:");

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
            //開発符号
            FormControlUtil.ClearComboBoxDataSource(this.GeneralCodeComboBox);

            //ステータス
            this.StatusOpenCheckBox.Checked = true;
            this.StatusCloseCheckBox.Checked = false;

            //担当課
            FormControlUtil.ClearComboBoxDataSource(this.DepartmentComboBox);

            //状況
            this.SituationBlankCheckBox.Checked = true;
            this.SituationDevelopmentCompleteCheckBox.Checked = true;
            this.SituationDesignResponsibilityCheckBox.Checked = true;
            this.SituationExperimentResponsibilityCheckBox.Checked = true;

        }
        #endregion

        #region 検討会資料詳細一覧
        /// <summary>
        /// 検討会資料詳細一覧マウスイン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //行ヘッダー以外は終了
            if (e.RowIndex != -1)
            {
                return;
            }

            //ツールチップを表示
            if (e.ColumnIndex == this.SituationColumn.Index)
            {
                this.SituationToolTip.Active = false;
                this.SituationToolTip.Active = true;
                this.SituationToolTip.Show(JyoukyouHelpMsg, this.MeetingDocumentDetailDataGridView);
            }
            else
            {
                this.SituationToolTip.Active = false;
            }

            if (e.ColumnIndex == this.ConfirmationMethodColumn.Index)
            {
                this.ConfirmationMethodToolTip.Active = false;
                this.ConfirmationMethodToolTip.Active = true;
                this.ConfirmationMethodToolTip.Show("「確認方法」には、何で確認したかを記入してください。\n\n例）台車、Ｋ３車　等々", this.MeetingDocumentDetailDataGridView);
            }
            else
            {
                this.ConfirmationMethodToolTip.Active = false;
            }
        }

        /// <summary>
        /// 検討会資料詳細一覧マウスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailDataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            //ツールチップを非表示
            this.SituationToolTip.Hide(this.MeetingDocumentDetailDataGridView);
            this.ConfirmationMethodToolTip.Hide(this.MeetingDocumentDetailDataGridView);
        }

        /// <summary>
        /// 検討会資料詳細一覧マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            //右クリック以外は終了
            if (e.Button != MouseButtons.Right)
            {
                return;

            }

            //編集不可なら終了
            if (this.MeetingDocumentDetailDataGridView.ReadOnly == true)
            {
                return;

            }

            //無効な行か列の場合は終了
            var point = new Point(e.X, e.Y);
            var hitTestInfo = this.MeetingDocumentDetailDataGridView.HitTest(point.X, point.Y);
            if (hitTestInfo.Type == DataGridViewHitTestType.None || hitTestInfo.RowIndex < 0 || hitTestInfo.ColumnIndex < 0)
            {
                return;

            }

            //確認完了日以外の列なら終了
            if (this.MeetingDocumentDetailDataGridView.Columns[hitTestInfo.ColumnIndex].Name != this.CompletionScheduleColumn.Name)
            {
                return;

            }

            //ステータスがクローズなら終了
            var detail = this.GetMeetingDocumentDetailByDataGridView(hitTestInfo.RowIndex);
            if (detail.OPEN_CLOSE == StatusClose)
            {
                return;

            }

            //右クリックしたセルを選択
            this.MeetingDocumentDetailDataGridView.CurrentCell = this.MeetingDocumentDetailDataGridView[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];

            //コンテキストメニュー表示
            this.CompletionScheduleContextMenuStrip.Tag = hitTestInfo.RowIndex;
            this.CompletionScheduleContextMenuStrip.Show(this.MeetingDocumentDetailDataGridView, point);

        }

        /// <summary>
        /// 検討会資料詳細一覧セルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            //編集不可なら終了
            if (this.MeetingDocumentDetailDataGridView.ReadOnly == true)
            {
                return;

            }

            var isEdit = false;

            var col = this.MeetingDocumentDetailDataGridView.Columns[e.ColumnIndex];
            var name = col.Name;

            var detail = this.GetMeetingDocumentDetailByDataGridView(e.RowIndex);

            //ステータスがクローズなら終了
            if (detail.OPEN_CLOSE == StatusClose)
            {
                return;

            }

            //開発符号
            if (name == this.GeneralCodeColumn.Name)
            {
                //開発符号を選択しているなら設定
                var generalCode = this.ShowGeneralCodeListForm();
                if (string.IsNullOrWhiteSpace(generalCode) == false)
                {
                    //開発符号
                    detail.GENERAL_CODE = generalCode;

                    isEdit = true;

                }

            }
            //課
            else if (name == this.DepartmentColumn.Name)
            {
                using (var form = new MultiSelectSectionForm { Sections = detail.試験部署_CODE })
                {
                    //OKかどうか
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        var kaList = (form.Sections ?? "").Split(',').Select(x => x.Trim()).Where(x => string.IsNullOrWhiteSpace(x) == false).ToArray();

                        //課
                        detail.試験部署_CODE = string.Join("," + Const.CrLf, kaList);

                        isEdit = true;

                    }

                }

            }
            //影響部品・出図日程入力画面表示対象の列
            else if (this.eikyouBuhinSyutuzuNitteiShowColumn.Contains(name) == true)
            {
                //影響部品・出図日程入力画面表示
                using (var form = new InfluencePartsExitScheduleForm { MeetingDocumentDetai = detail })
                {
                    //OKかどうか
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        //検討会資料詳細一覧のヘッダーに合計を設定
                        this.SetHeaderSumMeetingDocumentDetailList();

                        isEdit = true;

                    }

                }

            }
            else
            {
                return;

            }

            //編集したかどうか
            if (isEdit == true)
            {
                //編集フラグを有効化
                detail.EDIT_FLG = true;

                //変更を通知
                this.ResetItemMeetingDocumentDetailList(detail.ID);

            }

        }

        /// <summary>
        /// 検討会資料詳細一覧セルコンテンツクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            //履歴以外の列なら終了
            if (this.MeetingDocumentDetailDataGridView.Columns[e.ColumnIndex].Name != this.HistoryColumn.Name)
            {
                return;

            }

            //履歴が無い場合は終了
            var detail = this.GetMeetingDocumentDetailByDataGridView(e.RowIndex);
            if (detail.CATEGORY_ID_COUNT <= 1)
            {
                return;

            }

            using (var form = new MeetingDocumentHistoryForm { MeetingDocumentDetai = detail })
            {
                //検討会課題履歴画面表示
                form.ShowDialog(this);

            }

        }

        /// <summary>
        /// 検討会資料詳細一覧セル値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //コミット済みなら終了
            if (this.MeetingDocumentDetailDataGridView.IsCurrentCellDirty == false)
            {
                return;

            }

            //確認の列以外なら終了
            var col = this.MeetingDocumentDetailDataGridView.Columns[this.MeetingDocumentDetailDataGridView.CurrentCell.ColumnIndex];
            if (col.Name != this.ConfirmColumn.Name)
            {
                return;

            }

            //編集内容をコミット
            this.MeetingDocumentDetailDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

        }

        /// <summary>
        /// 検討会資料詳細一覧値検証
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            //部別項目番号以外の列なら終了
            if (this.MeetingDocumentDetailDataGridView.Columns[e.ColumnIndex].Name != this.DepartmentItemNoColumn.Name)
            {
                return;

            }

            var cell = this.MeetingDocumentDetailDataGridView[e.ColumnIndex, e.RowIndex];
            var edit = this.MeetingDocumentDetailDataGridView.EditingControl;

            //背景色をデフォルトに設定
            cell.Style.BackColor = Const.DefaultBackColor;

            //入力されているかどうか
            var value = e.FormattedValue;
            var s = value == null ? "" : value.ToString();
            if (string.IsNullOrWhiteSpace(s) == false)
            {
                //数値に変換できるかどうか
                var i = 0;
                var flg = int.TryParse(s, out i);
                if (flg == false)
                {
                    //背景色変更
                    edit.BackColor = Const.ErrorBackColor;
                    cell.Style.BackColor = Const.ErrorBackColor;

                    //入力エラー文言表示
                    Messenger.Warn(string.Format(Resources.KKM00025, this.DepartmentItemNoColumn.HeaderText.Replace(Const.CrLf, "")));

                }

                //キャンセル可否
                e.Cancel = !flg;

            }

        }

        /// <summary>
        /// 検討会資料詳細一覧値変更後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            var col = this.MeetingDocumentDetailDataGridView.Columns[e.ColumnIndex];
            var name = col.Name;

            var row = this.MeetingDocumentDetailDataGridView.Rows[e.RowIndex];

            var cell = row.Cells[name];

            var detail = this.GetMeetingDocumentDetailByDataGridView(row);

            //編集フラグを有効化
            detail.EDIT_FLG = true;

            //ステータスの列かどうか
            if (name == this.StatusColumn.Name)
            {
                //ステータスがクローズならステータス以外の列は編集不可
                var readOnly = detail.OPEN_CLOSE == StatusClose;
                foreach (var kv in this.gridViewColumnMap.Where(x => x.Key != this.StatusColumn.Name))
                {
                    row.Cells[kv.Key].ReadOnly = readOnly == true ? true : kv.Value;

                }

            }

            //確認の列かどうか
            if (name == this.ConfirmColumn.Name)
            {
                var busyoList = (detail.メンテ部署_CODE ?? "").Split(',').Select(x => x.Trim()).Where(x => string.IsNullOrWhiteSpace(x) == false).ToList();

                //選択しているかどうか
                if (Convert.ToBoolean(cell.Value) == true)
                {
                    //ユーザーの課が存在していなければ終了
                    if (busyoList.Contains(SessionDto.SectionCode) == false)
                    {
                        busyoList.Add(SessionDto.SectionCode);

                    }

                }
                else
                {
                    //未選択ならユーザーの課は全て削除
                    busyoList.RemoveAll(x => x == SessionDto.SectionCode);

                }

                //メンテ済部署
                detail.メンテ部署_CODE = string.Join("," + Const.CrLf , busyoList.ToArray());

                //変更を通知
                this.ResetItemMeetingDocumentDetailList(detail.ID);

            }

        }

        /// <summary>
        /// 検討会資料詳細一覧値エラー時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //コンボボックスの列かどうか
            var col = this.MeetingDocumentDetailDataGridView.Columns[e.ColumnIndex];
            if (this.comboBoxList.Any(x => x.Name == col.Name) == true)
            {
                //例外は無視
                e.ThrowException = false;

                //キャンセル
                e.Cancel = true;

            }
            else
            {
                //例外はスロー
                e.ThrowException = true;

            }

        }
        #endregion

        #region 確認完了日コンテキストメニュー
        /// <summary>
        /// 日付入力クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //確認完了予定日 / 完了日入力画面表示
            this.ShowCompletionScheduleForm(false);

        }

        /// <summary>
        /// 日付付き済み入力クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayCompletionInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //確認完了予定日 / 完了日入力画面表示
            this.ShowCompletionScheduleForm(true);

        }

        /// <summary>
        /// 確認完了日入力画面表示
        /// </summary>
        /// <param name="isCompletion"></param>
        private void ShowCompletionScheduleForm(bool isCompletion)
        {
            var detail = this.GetMeetingDocumentDetailByDataGridView((int)this.CompletionScheduleContextMenuStrip.Tag);

            using (var form = new CompletionScheduleForm { MeetingDocumentDetai = detail, IsCompletion = isCompletion })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //変更を通知
                    this.ResetItemMeetingDocumentDetailList(detail.ID);

                }

            }

        }
        #endregion

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {

                //検討会資料詳細が登録できたかどうか
                if (this.EntryMeetingDocumentDetail() == true)
                {
                    //検討会資料詳細一覧設定
                    this.SearcMeetingDocumentDetailList();

                }

            });

        }
        #endregion

        #region 検討会資料一覧ボタンクリック
        /// <summary>
        /// 検討会資料一覧ボタンクリック
        /// </summary>
        /// <remarks>
        /// 当フォームはクローズされるため、親フォームを設定後に
        /// 検討会資料一覧画面を表示します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentListButton_Click(object sender, EventArgs e)
        {
            //フォームクローズ
            this.Close();

            //検討会資料一覧画面表示
            var form = new MeetingDocumentListForm();

            //タスクバーに表示する
            form.ShowInTaskbar = true;
            form.Show(this.Owner);
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

        #region フォームクローズ前
        /// <summary>
        /// フォームクローズ前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentDetailListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //検討会資料詳細の編集は終了
                this.MeetingDocumentDetailDataGridView.EndEdit();

                //画面を変更していて登録するかどうか
                if (this.IsEditMeetingDocumentDetailEntry() == false)
                {
                    //登録に失敗した場合は閉じさせない
                    e.Cancel = true;

                }

            });

        }
        #endregion

        #region 開発符号検索画面表示
        /// <summary>
        /// 開発符号検索画面表示
        /// </summary>
        /// <returns></returns>
        private string ShowGeneralCodeListForm()
        {
            var generalCode = "";

            using (var form = new GeneralCodeListForm())
            {
                //開発符号検索画面がOKで開発符号が同じかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    generalCode = form.GENERAL_CODE;

                }

            }

            return generalCode;

        }
        #endregion

        #region 検討会資料詳細追加
        /// <summary>
        /// 検討会資料詳細追加
        /// </summary>
        private void AddMeetingDocumentDetail()
        {
            this.AddMeetingDocumentDetail(new[] { new MeetingDocumentDetailModel() });

        }

        /// <summary>
        /// 検討会資料詳細追加
        /// </summary>
        /// <param name="list">検討会資料詳細</param>
        private void AddMeetingDocumentDetail(IEnumerable<MeetingDocumentDetailModel> list)
        {
            var index = 0;

            //一覧で2行目以降を選択しているかどうか
            var cell = this.MeetingDocumentDetailDataGridView.CurrentCell;
            if (cell != null)
            {
                //選択行の上に追加
                index = cell.RowIndex;

            }

            var target = list.ToArray();

            foreach (var detail in target)
            {
                //ID
                detail.ID = this.siryouID--;

                //資料_ID
                detail.資料_ID = this.MeetingDocumentDetailSearchCond.資料_ID;

                //カテゴリーID
                detail.CATEGORY_ID = 0;

                //ステータス
                detail.OPEN_CLOSE = StatusOpen;

                //編集フラグ
                detail.EDIT_FLG = true;

            }

            //行追加して再バインド
            this.MeetingDocumentDetailList.InsertRange(index, target);
            this.SetMeetingDocumentDetailList();

            //追加行を選択状態に設定
            this.MeetingDocumentDetailDataGridView.CurrentCell = null;
            this.MeetingDocumentDetailDataGridView.Rows[index].Selected = true;

        }
        #endregion

        #region 検討会資料設定
        /// <summary>
        /// 検討会資料設定
        /// </summary>
        private void SetMeetingDocument()
        {
            //検討会資料が設定されてなければ取得
            if (this.MeetingDocument == null)
            {
                this.MeetingDocument = this.GetMeetingDocument(); 

            }

            //検討会資料設定
            this.SetMeetingDocument(this.MeetingDocument);

        }

        /// <summary>
        /// 検討会資料設定
        /// </summary>
        /// <param name="kentoukai">検討会資料</param>
        private void SetMeetingDocument(MeetingDocumentModel kentoukai)
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            var toDay = DateTime.Today;

            //管理権限なしで更新権限ありかどうか
            if (isManagement == false && isUpdate == true)
            {
                var start = kentoukai.EDIT_TERM_START;
                var end = kentoukai.EDIT_TERM_END;

                //更新開始日と更新終了日があるかどうか
                if (start != null && end != null)
                {
                    isUpdate = start.Value.Date <= toDay && toDay <= end.Value.Date;

                }
                //更新開始日があるかどうか
                else if (start != null)
                {
                    isUpdate = start.Value.Date <= toDay;

                }
                //更新終了日があるかどうか
                else if (end != null)
                {
                    isUpdate = end.Value.Date >= toDay;

                }

            }

            //資料種別
            this.DocumentTypeLabel.Text = kentoukai?.MEETING_NAME;

            //資料名
            this.DocumentNameLabel.Text = kentoukai?.NAME;

            //開催日
            this.HeldDayLabel.Text = DateTimeUtil.ConvertDateString(kentoukai?.MONTH);

            //更新期間
            var kousinKikan = (kentoukai?.EDIT_TERM_START == null && kentoukai?.EDIT_TERM_END == null) ? "" : string.Format(KousinKikanFormat, kentoukai?.EDIT_TERM_START, kentoukai?.EDIT_TERM_END);
            this.UpdatePeriodLabel.Text = kousinKikan;

            //検討会資料一覧
            this.MeetingDocumentDetailDataGridView.ReadOnly = !isUpdate;

            //登録ボタン
            this.EntryButton.Visible = isUpdate;

            //行読込ボタン
            this.RowReadButton.Visible = isUpdate;

            //行追加ボタン
            this.RowAddButton.Visible = isUpdate;

            //行削除ボタン
            this.RowDeleteButton.Visible = isUpdate;

        }
        #endregion

        #region 検討会資料詳細一覧設定
        /// <summary>
        /// 検討会資料詳細検索
        /// </summary>
        private void SearcMeetingDocumentDetailList()
        {
            //検討会資料詳細取得
            this.MeetingDocumentDetailList = this.GetMeetingDocumentDetailList();

            //検討会資料詳細一覧設定
            this.SetMeetingDocumentDetailList();

            //検討会資料詳細が取得できたかどうか
            this.ListDataLabel.Text = this.MeetingDocumentDetailList.Any() == true ? "" : Resources.KKM00005;

        }

        /// <summary>
        /// 検討会資料詳細一覧設定
        /// </summary>
        private void SetMeetingDocumentDetailList()
        {
            //描画停止
            this.MeetingDocumentDetailDataGridView.SuspendLayout();

            //検討会資料詳細一覧初期化
            this.meetingDocumentDetailDataSource.DataSource = null;

            //削除フラグが無効のデータがあればバインド
            var target = this.MeetingDocumentDetailList.Where(x => x.DELETE_FLG == false).ToArray();
            if (target.Any() == true)
            {
                this.meetingDocumentDetailDataSource.DataSource = target;

            }

            //行の変更
            foreach (DataGridViewRow row in this.MeetingDocumentDetailDataGridView.Rows)
            {
                var detail = this.GetMeetingDocumentDetailByDataGridView(row);

                //ステータスがクローズならステータス以外の列は編集不可
                if (detail.OPEN_CLOSE == StatusClose)
                {
                    foreach (var kv in this.gridViewColumnMap.Where(x => x.Key != this.StatusColumn.Name))
                    {
                        row.Cells[kv.Key].ReadOnly = true;

                    }

                }

                //履歴が無い場合は履歴はリンクを解除
                if (detail.CATEGORY_ID_COUNT <= 1)
                {
                    row.Cells[this.HistoryColumn.Name] = new DataGridViewTextBoxCell();

                }

            }

            //検討会資料詳細一覧のヘッダーに合計を設定
            this.SetHeaderSumMeetingDocumentDetailList();

            //一覧を未選択状態に設定
            this.MeetingDocumentDetailDataGridView.CurrentCell = null;

            //描画再開
            this.MeetingDocumentDetailDataGridView.ResumeLayout(true);

        }
        #endregion

        #region 検討会資料詳細一覧に変更を通知
        /// <summary>
        /// 検討会資料詳細一覧に変更を通知
        /// </summary>
        /// <param name="id">ID</param>
        private void ResetItemMeetingDocumentDetailList(long id)
        {
            //変更を通知
            var list = this.meetingDocumentDetailDataSource.DataSource as MeetingDocumentDetailModel[];
            var index = list.Select((x, y) => new { x.ID, Index = y }).First(x => x.ID == id).Index;
            this.meetingDocumentDetailDataSource.ResetItem(index);

        }
        #endregion

        #region 検討会資料詳細一覧のヘッダーに合計を設定
        /// <summary>
        /// 検討会資料詳細一覧のヘッダーに合計を設定
        /// </summary>
        private void SetHeaderSumMeetingDocumentDetailList()
        {
            var costList = new List<decimal> { 0 };
            var situryouList = new List<decimal> { 0 };
            var tousiList = new List<decimal> { 0 };

            Action<DataGridViewRow, DataGridViewColumn, List<decimal>> setList = (row, col, list) =>
            {
                var cell = row.Cells[col.Name];

                var value = cell.Value;

                var add = 0M;

                if (value != null)
                {
                    var s = value.ToString().Trim().Replace(Const.CrLf, Const.Lf).Replace(Const.Cr, Const.Lf);
                    if (string.IsNullOrWhiteSpace(s) == false)
                    {
                        var line = s.Split(new[] { Const.Lf }, StringSplitOptions.None);
                        var last = line.LastOrDefault(x => x.StartsWith("計"));

                        if (string.IsNullOrWhiteSpace(last) == false)
                        {
                            decimal.TryParse(last.Substring(1).Trim(), out add);

                        }

                    }

                }

                list.Add(add);

            };

            Action<DataGridViewColumn, List<decimal>> setSum = (col, list) =>
            {
                var sum = list.Sum();
                var headerSum = sum == Math.Truncate(sum) ? sum.ToString("0") : sum.ToString();

                col.HeaderText = string.Format("{0}{1}", this.headerNameMap[col.Name], headerSum);

            };

            foreach (DataGridViewRow row in this.MeetingDocumentDetailDataGridView.Rows)
            {
                //コスト変動見通し(円)
                setList(row, this.CostColumn, costList);

                //質量変動見通し(g)
                setList(row, this.MassColumn, situryouList);

                //投資変動見通し(百万円)
                setList(row, this.InvestmentColumn, tousiList);

            }

            //描画停止
            this.MeetingDocumentDetailDataGridView.SuspendLayout();

            //垂直スクロール位置取得
            VScrollBar vs = null;
            var offset = 0;
            foreach (Control c in this.MeetingDocumentDetailDataGridView.Controls)
            {
                if (c is VScrollBar)
                {
                    vs = c as VScrollBar;
                    offset = vs.Value;
                    break;

                }

            }

            //コスト変動見通し(円)
            setSum(this.CostColumn, costList);

            //質量変動見通し(g)
            setSum(this.MassColumn, situryouList);

            //投資変動見通し(百万円)
            setSum(this.InvestmentColumn, tousiList);

            //垂直スクロール位置再設定
            vs.Value = offset;

            //描画再開
            this.MeetingDocumentDetailDataGridView.ResumeLayout();

        }
        #endregion

        #region 変更検討会資料詳細登録可否
        /// <summary>
        /// 変更検討会資料詳細登録可否
        /// </summary>
        /// <returns></returns>
        private bool IsEditMeetingDocumentDetailEntry()
        {
            //討会資料詳細を編集しているかどうか
            var isEdit = this.MeetingDocumentDetailList.Any(x => x.EDIT_FLG == true);

            //画面を変更していないか登録するかどうか
            if (isEdit == false || (isEdit == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                return true;

            }

            //検討会資料詳細の登録
            return this.EntryMeetingDocumentDetail();

        }
        #endregion

        #region データの取得
        /// <summary>
        /// 検討会資料の取得
        /// </summary>
        /// <returns></returns>
        private MeetingDocumentModel GetMeetingDocument()
        {
            var cond = new MeetingDocumentSearchModel
            {
                //行数
                ROW_COUNT = MeetingDocumentRowCount

            };

            return this.GetMeetingDocumentList(cond).FirstOrDefault();

        }

        /// <summary>
        /// 検討会資料の取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<MeetingDocumentModel> GetMeetingDocumentList(MeetingDocumentSearchModel cond)
        {
            var list = new List<MeetingDocumentModel>();

            //APIで取得
            var res = HttpUtil.GetResponse<MeetingDocumentSearchModel, MeetingDocumentModel>(ControllerType.MeetingDocument, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// 検討会資料詳細の取得
        /// </summary>
        /// <returns></returns>
        private List<MeetingDocumentDetailModel> GetMeetingDocumentDetailList()
        {
            var list = new List<MeetingDocumentDetailModel>();

            //検討会資料が無ければ終了
            if (this.MeetingDocument == null)
            {
                return list;

            }

            var status = "";
            var departmentCode = "";
            var jyoukyouList = new List<string>();

            //Openのみ選択かどうか
            if (this.StatusOpenCheckBox.Checked == true && this.StatusCloseCheckBox.Checked == false)
            {
                status = this.StatusOpenCheckBox.Tag.ToString();

            }
            //Closeのみ選択かどうか
            else if (this.StatusOpenCheckBox.Checked == false && this.StatusCloseCheckBox.Checked == true)
            {
                status = this.StatusCloseCheckBox.Tag.ToString();

            }

            //担当課を選択しているかどうか
            if (this.DepartmentComboBox.SelectedIndex >= 0 && this.DepartmentComboBox.SelectedValue != null)
            {
                departmentCode = this.DepartmentComboBox.SelectedValue.ToString();

            }

            //状況の取得
            foreach (var c in this.SituationPanel.Controls)
            {
                //チェックボックスかどうか
                if (c is CheckBox)
                {
                    //選択しているかどうか
                    var check = c as CheckBox;
                    if (check.Checked == true)
                    {
                        jyoukyouList.Add(check.Tag.ToString());

                    }

                }

            }

            //パラメータ設定
            var cond = new MeetingDocumentDetailSearchModel
            {
                //資料_ID
                資料_ID = this.MeetingDocument?.ID,

                //開発符号
                GENERAL_CODE = this.GeneralCodeComboBox.Text,

                //ステータス
                OPEN_CLOSE = status,

                //担当課
                試験部署_CODE = departmentCode,

                //状況
                状況 = jyoukyouList.ToArray()

            };

            //検討会資料詳細検索条件
            this.MeetingDocumentDetailSearchCond = cond;

            //APIで取得
            var res = HttpUtil.GetResponse<MeetingDocumentDetailSearchModel, MeetingDocumentDetailModel>(ControllerType.MeetingDocumentDetail, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion

        #region 検討会資料詳細の登録
        /// <summary>
        /// 検討会資料詳細の登録
        /// </summary>
        /// <returns></returns>
        private bool EntryMeetingDocumentDetail()
        {
            //検討会資料詳細のチェック
            if (this.IsEntryMeetingDocumentDetail() == false)
            {
                return false;

            }

            //登録対象があるかどうか
            var list = this.GetEntryMeetingDocumentDetail();
            if (list == null || list.Any() == false)
            {
                return false;

            }

            //検討会資料詳細登録
            var res = HttpUtil.PutResponse(ControllerType.MeetingDocumentDetail, list);

            //レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;

            }

            //登録後メッセージ
            Messenger.Info(Resources.KKM00002);

            return true;

        }

        /// <summary>
        /// 登録する検討会資料詳細を取得
        /// </summary>
        /// <returns></returns>
        private List<MeetingDocumentDetailModel> GetEntryMeetingDocumentDetail()
        {
            var list = this.MeetingDocumentDetailList.Where(x => x.ID > 0 && x.DELETE_FLG == true).ToList();

            var now = DateTime.Now;

            var sortNo = 1;

            for (var i = 0; i < this.MeetingDocumentDetailDataGridView.RowCount; i++)
            {
                var detail = this.GetMeetingDocumentDetailByDataGridView(i);

                var isEdit = detail.EDIT_FLG;

                //変更している行かどうか
                if (isEdit == true)
                {
                    //登録済の検討会資料かどうか
                    if (detail.ID < 0)
                    {
                        //ID
                        detail.ID = 0;

                    }

                    //課グループID
                    detail.SECTION_GROUP_ID = SessionDto.SectionGroupID;

                    //入力者パーソナルID
                    detail.INPUT_PERSONEL_ID = SessionDto.UserId;

                    //入力者名
                    detail.INPUT_NAME = SessionDto.UserName;

                    //入力日時
                    detail.INPUT_DATETIME = now;

                }

                //ソート順
                detail.SORT_NO = sortNo++;

                //登録対象を追加
                list.Add(detail);

            }

            return list;

        }
        #endregion

        #region 検討会資料詳細のチェック
        /// <summary>
        /// 検討会資料詳細のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsEntryMeetingDocumentDetail()
        {
            //登録対象が無ければ終了
            if (this.MeetingDocumentDetailList.Any() == false)
            {
                return false;

            }

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            //検討会資料が存在しているかどうか
            if (this.GetMeetingDocumentList(new MeetingDocumentSearchModel { ID = new[] { this.MeetingDocumentDetailSearchCond.資料_ID } }).Any() == false)
            {
                //存在していない場合はエラー
                Messenger.Warn(Resources.KKM00021);
                return false;

            }

            return true;

        }
        #endregion

        #region 検討会資料詳細を検討会資料詳細一覧から取得
        /// <summary>
        /// 検討会資料詳細を検討会資料詳細一覧から取得
        /// </summary>
        /// <param name="index">行番号</param>
        /// <returns></returns>
        private MeetingDocumentDetailModel GetMeetingDocumentDetailByDataGridView(int index)
        {
            return this.GetMeetingDocumentDetailByDataGridView(this.MeetingDocumentDetailDataGridView.Rows[index]);

        }

        /// <summary>
        /// 検討会資料詳細を検討会資料詳細一覧から取得
        /// </summary>
        /// <param name="row">行</param>
        /// <returns></returns>
        private MeetingDocumentDetailModel GetMeetingDocumentDetailByDataGridView(DataGridViewRow row)
        {
            var id = Convert.ToInt64(row.Cells[this.IdColumn.Name].Value);

            return this.MeetingDocumentDetailList.First(x => x.ID == id);

        }
        #endregion
    }
}