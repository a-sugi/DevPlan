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

using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 担当検索
    /// </summary>
    public partial class SectionGroupListForm : BaseSubForm
    {
        #region インスタンス
        /// <summary>カスタムテンプレート</summary>
        private CustomTemplate CustomTemplate;
        #endregion

        #region メンバ変数
        /// <summary>部ID</summary>
        private string DepartmentID;
        /// <summary>課ID</summary>
        private string SectionID;
        /// <summary>課グループID</summary>
        private string SectionGroupID;
        /// <summary>部コード</summary>
        private string DepartmentCode;
        /// <summary>課コード</summary>
        private string SectionCode;
        /// <summary>課グループコード</summary>
        private string SectionGroupCode;
        /// <summary>部名称</summary>
        private string DepartmentName;
        /// <summary>課名称</summary>
        private string SectionName;
        /// <summary>課グループ名称</summary>
        private string SectionGroupName;
        /// <summary>所属</summary>
        private string Establishment;
        /// <summary>部コンボボックス操作</summary>
        private bool DepartmentUse = true;
        /// <summary>課コンボボックス操作</summary>
        private bool SectionUse = true;
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "担当検索"; } }

        /// <summary>部ID</summary>
        public string DEPARTMENT_ID　{ get { return DepartmentID; } set { DepartmentID = value; } }
        /// <summary>課ID</summary>
        public string SECTION_ID { get { return SectionID; } set { SectionID = value; } }
        /// <summary>担当ID</summary>
        public string SECTION_GROUP_ID { get { return SectionGroupID; } set { SectionGroupID = value; } }
        /// <summary>部コード</summary>
        public string DEPARTMENT_CODE { get { return DepartmentCode; } set { DepartmentCode = value; } }
        /// <summary>課コード</summary>
        public string SECTION_CODE { get { return SectionCode; } set { SectionCode = value; } }
        /// <summary>担当コード</summary>
        public string SECTION_GROUP_CODE { get { return SectionGroupCode; } set { SectionGroupCode = value; } }
        /// <summary>部名称</summary>
        public string DEPARTMENT_NAME { get { return DepartmentName; } set { DepartmentName = value; } }
        /// <summary>課名称</summary>
        public string SECTION_NAME { get { return SectionName; } set { SectionName = value; } }
        /// <summary>担当名称</summary>
        public string SECTION_GROUP_NAME { get { return SectionGroupName; } set { SectionGroupName = value; } }
        /// <summary>所属</summary>
        public string ESTABLISHMENT { get { return Establishment; } set { Establishment = value; } }
        /// <summary>部コンボボックス操作</summary>
        public bool DEPARTMENT_COMBOBOX { get { return DepartmentUse; } set { DepartmentUse = value; } }
        /// <summary>課コンボボックス操作</summary>
        public bool SECTION_COMBOBOX { get { return SectionUse; } set { SectionUse = value; } }
        /// <summary>担当リスト</summary>
        private List<SectionGroupModel> SectionGroupList { get; set ; }
        /// <summary>担当</summary>
        public SectionGroupModel SectionGroup { get; private set; }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SectionGroupListForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// SectionGroupListForm_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupListForm_Load(object sender, EventArgs e)
        {
            this.Init();

        }

        /// <summary>
        /// SectionListForm_Shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupListForm_Shown(object sender, EventArgs e)
        {
            this.SectionGroupListMultiRow.CurrentCell = null;
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            this.CustomTemplate = new CustomTemplate() { MultiRow = this.SectionGroupListMultiRow, RowCountLabel = this.RowCountLabel };

            this.SectionGroupListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(new SectionGroupListMultiRowTemplate());

            this.SectionGroupListMultiRow.CellClick += this.SectionGroupListMultiRow_CellClick;

            base.Text = this.FormTitle;
            base.ListFormTitleLabel.Text = this.FormTitle;

            //部コンボボックス使用可／不可設定
            this.DepartmentComboBox.Enabled = this.DepartmentUse;

            //課コンボボックス使用可／不可設定
            this.SectionComboBox.Enabled = this.SectionUse;

            this.SetSearchCondition();

            this.SetGridData();
        }

        #region 検索条件のセット
        /// <summary>
        /// 検索条件のセット
        /// </summary>
        private void SetSearchCondition()
        {
            //初期表示フォーカス
            this.ActiveControl = SectionGroupTextBox;

            // 部
            this.DepartmentComboBox.SelectedValueChanged -= this.DepartmentComboBox_SelectedValueChanged;
            this.DepartmentComboBox.DataSource = GetDepartmentList();
            if (DepartmentID != null)
            {
                this.DepartmentComboBox.SelectedValue = DepartmentID;

            }
            else
            {
                this.DepartmentComboBox.SelectedIndex = -1;

            }
            this.DepartmentComboBox.SelectedValueChanged += this.DepartmentComboBox_SelectedValueChanged;

            // 課
            this.SectionComboBox.DataSource = GetSectionList();
            if (SectionID != null)
            {
                this.SectionComboBox.SelectedValue = SectionID;

            }
            else
            {
                this.SectionComboBox.SelectedIndex = -1;

            }
        }
        #endregion

        #region グリッドデータのセット
        /// <summary>
        /// グリッドデータのセット
        /// </summary>
        private void SetGridData()
        {
            var list = SectionGroupList = GetGridDataList();

            //検索結果文言
            this.SearchResultLabel.Text = (list == null || list.Any() == false) ? Resources.KKM00005 : string.Empty;

            this.SectionGroupListMultiRow.ColumnHeadersDefaultCellStyle.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;

            // データバインド
            this.CustomTemplate.SetDataSource(list);

            this.SectionGroupListMultiRow.CurrentCell = null;

        }
        #endregion

        #region グリッドデータの取得
        /// <summary>
        /// グリッドデータの取得
        /// </summary>
        private List<SectionGroupModel> GetGridDataList()
        {
            // パラメータ設定
            var itemCond = new SectionGroupSearchModel
            {
                // 部ID
                DEPARTMENT_ID = (string)this.DepartmentComboBox.SelectedValue?.ToString(),

                // 所属
                ESTABLISHMENT = this.ESTABLISHMENT,

                // 部コード
                DEPARTMENT_CODE = (string)this.DepartmentComboBox.Text,

                // 課ID
                SECTION_ID = (string)this.SectionComboBox.SelectedValue?.ToString(),

                // 課コード
                SECTION_CODE = (string)this.SectionComboBox.Text,

                // 担当コード
                SECTION_GROUP_CODE = (string)this.SectionGroupTextBox.Text
            };

            // Get実行
            var res = HttpUtil.GetResponse<SectionGroupSearchModel, SectionGroupModel>(ControllerType.SectionGroup, itemCond);

            return (res.Results).ToList();
        }
        #endregion

        #region 部検索
        /// <summary>
        /// 部検索
        /// </summary>
        private List<DepartmentModel> GetDepartmentList()
        {
            //パラメータ設定
            var itemCond = new DepartmentSearchModel
            {
                // 所属
                ESTABLISHMENT = this.ESTABLISHMENT
            };

            //Get実行
            var res = HttpUtil.GetResponse<DepartmentSearchModel, DepartmentModel>(ControllerType.Department, itemCond);

            return (res.Results).ToList();
        }
        #endregion

        #region 課検索
        /// <summary>
        /// 課検索
        /// </summary>
        private List<SectionModel> GetSectionList()
        {
            //部が未選択なら終了
            if (this.DepartmentComboBox.SelectedValue == null)
            {
                return null;

            }

            //パラメータ設定
            var itemCond = new SectionSearchModel
            {
                //部ID
                DEPARTMENT_ID = this.DepartmentComboBox.SelectedValue.ToString(),

                // 所属
                ESTABLISHMENT = this.ESTABLISHMENT,
            };

            //Get実行
            var res = HttpUtil.GetResponse<SectionSearchModel, SectionModel>(ControllerType.Section, itemCond);

            return (res.Results).ToList();
        }
        #endregion

        #region 検索ボタンのクリック
        /// <summary>
        /// 検索ボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListSearchButton_Click(object sender, EventArgs e)
        {
            this.SetGridData();
        }
        #endregion

        #region グリッドのセルクリック
        /// <summary>
        /// グリッドのセルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupListMultiRow_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = this.SectionGroupListMultiRow;

            this.DepartmentCode = (string)grid.CurrentRow.Cells[0].Value;
            this.SectionCode = (string)grid.CurrentRow.Cells[1].Value;
            this.SectionGroupCode = (string)grid.CurrentRow.Cells[2].Value;
            this.DepartmentID = (string)grid.CurrentRow.Cells[3].Value;
            this.SectionID = (string)grid.CurrentRow.Cells[4].Value;
            this.SectionGroupID = (string)grid.CurrentRow.Cells[5].Value;
            this.DepartmentName = (string)grid.CurrentRow.Cells[6].Value;
            this.SectionName = (string)grid.CurrentRow.Cells[7].Value;
            this.SectionGroupName = (string)grid.CurrentRow.Cells[8].Value;
            this.Establishment = (string)grid.CurrentRow.Cells[9].Value;
            this.SectionGroup = this.SectionGroupList.FirstOrDefault(x => x.DEPARTMENT_ID == this.DEPARTMENT_ID && x.SECTION_ID == this.SECTION_ID && x.SECTION_GROUP_ID == this.SECTION_GROUP_ID);

            base.FormOkClose();
        }
        #endregion

        #region 部コンボボックスの変更
        /// <summary>
        /// 部コンボボックスの変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.SectionComboBox.DataSource = GetSectionList();
            if (SectionID != null) this.SectionComboBox.SelectedValue = SectionID;
        }
        #endregion
    }
}
