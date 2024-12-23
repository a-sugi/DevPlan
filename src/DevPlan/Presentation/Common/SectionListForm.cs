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
    /// 課検索
    /// </summary>
    public partial class SectionListForm : BaseSubForm
    {
        #region インスタンス
        /// <summary>カスタムテンプレート</summary>
        private CustomTemplate CustomTemplate;
        #endregion

        #region メンバ変数
        /// <summary>部ID</summary>
        private String DepartmentID;
        /// <summary>課ID</summary>
        private String SectionID;
        /// <summary>部コード</summary>
        private String DepartmentCode;
        /// <summary>課コード</summary>
        private String SectionCode;
        /// <summary>部名称</summary>
        private string DepartmentName;
        /// <summary>課名称</summary>
        private string SectionName;
        /// <summary>所属</summary>
        private string Establishment;
        /// <summary>部コンボボックス操作</summary>
        private bool DepartmentUse = true;
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "課検索"; } }

        /// <summary>部ID</summary>
        public String DEPARTMENT_ID　{ get { return DepartmentID; } set { DepartmentID = value; } }
        /// <summary>課ID</summary>
        public String SECTION_ID { get { return SectionID; } set { SectionID = value; } }
        /// <summary>部コード</summary>
        public String DEPARTMENT_CODE { get { return DepartmentCode; } set { DepartmentCode = value; } }
        /// <summary>課コード</summary>
        public String SECTION_CODE { get { return SectionCode; } set { SectionCode = value; } }
        /// <summary>部名称</summary>
        public String DEPARTMENT_NAME { get { return DepartmentName; } set { DepartmentName = value; } }
        /// <summary>課名称</summary>
        public String SECTION_NAME { get { return SectionName; } set { SectionName = value; } }
        /// <summary>所属</summary>
        public String ESTABLISHMENT { get { return Establishment; } set { Establishment = value; } }
        /// <summary>部コンボボックス操作</summary>
        public bool DEPARTMENT_COMBOBOX { get { return DepartmentUse; } set { DepartmentUse = value; } }
        /// <summary>初期表示で全件表示するか？</summary>
        public bool IS_ALL { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SectionListForm()
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
        private void SectionListForm_Load(object sender, EventArgs e)
        {
            if (IS_ALL == false)
            {
                // 指定がなければ、ログインユーザーの所属部
                if (DepartmentID == null) DepartmentID = SessionDto.DepartmentID;
                if (SectionID == null) SectionID = SessionDto.SectionID;
            }

            this.Init();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            this.CustomTemplate = new CustomTemplate() { MultiRow = SectionListMultirow };

            this.SectionListMultirow.Template = this.CustomTemplate.SetContextMenuTemplate(new SectionListMultiRowTemplate());

            this.SectionListMultirow.CellClick += SectionListDataMultirow_CellClick;

            base.Text = this.FormTitle;
            base.ListFormTitleLabel.Text = this.FormTitle;

            //部コンボボックス使用可／不可設定
            this.DepartmentComboBox.Enabled = DepartmentUse;

            this.SetSearchCondition();

            this.SetGridData();
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionListForm_Shown(object sender, EventArgs e)
        {
            this.SectionListMultirow.CurrentCell = null;

        }
        #endregion

        #region 検索条件のセット
        /// <summary>
        /// 検索条件のセット
        /// </summary>
        private void SetSearchCondition()
        {
            //初期表示フォーカス
            this.ActiveControl = SectionTextBox;

            // 部
            this.DepartmentComboBox.DataSource = GetDepartmentList();

            if (DepartmentID == null)
            {
                this.DepartmentComboBox.SelectedIndex = -1;
            }
            else
            {
                this.DepartmentComboBox.SelectedValue = DepartmentID;
            }
        }
        #endregion

        #region グリッドデータのセット
        /// <summary>
        /// グリッドデータのセット
        /// </summary>
        private void SetGridData()
        {
            var list = GetGridDataList();

            //検索結果文言
            this.SearchResultLabel.Text = (list == null || list.Any() == false) ? Resources.KKM00005 : string.Empty;

            this.SectionListMultirow.ColumnHeadersDefaultCellStyle.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;

            // データバインド
            this.CustomTemplate.SetDataSource(list);

            this.SectionListMultirow.CurrentCell = null;

        }
        #endregion

        #region グリッドデータの取得
        /// <summary>
        /// グリッドデータの取得
        /// </summary>
        private List<SectionModel> GetGridDataList()
        {
            // パラメータ設定
            var itemCond = new SectionSearchModel
            {
                // 部コード
                DEPARTMENT_ID = (string)this.DepartmentComboBox.SelectedValue?.ToString(),

                // 部コード
                DEPARTMENT_CODE = (string)this.DepartmentComboBox.Text,

                // 課コード
                SECTION_CODE = (string)this.SectionTextBox.Text
            };

            // Get実行
            var res = HttpUtil.GetResponse<SectionSearchModel, SectionModel>(ControllerType.Section, itemCond);

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
                //部ID
                DEPARTMENT_ID = null,
            };

            //Get実行
            var res = HttpUtil.GetResponse<DepartmentSearchModel, DepartmentModel>(ControllerType.Department, itemCond);

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
        private void SectionListDataMultirow_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = this.SectionListMultirow;

            this.DepartmentCode = (string)grid.CurrentRow.Cells[0].Value;
            this.SectionCode = (string)grid.CurrentRow.Cells[1].Value;
            this.DepartmentID = (string)grid.CurrentRow.Cells[2].Value;
            this.SectionID = (string)grid.CurrentRow.Cells[3].Value;
            this.DepartmentName = (string)grid.CurrentRow.Cells[4].Value;
            this.SectionName = (string)grid.CurrentRow.Cells[5].Value;
            this.Establishment = (string)grid.CurrentRow.Cells[6].Value;

            base.FormOkClose();
        }
        #endregion
    }
}
