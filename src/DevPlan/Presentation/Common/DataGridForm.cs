using System;

using System.Windows.Forms;
using DevPlan.Presentation.Base;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// DataGrid表示
    /// </summary>
    public partial class DataGridForm : BaseSubForm
    {
        #region メンバ変数
        /// <summary>タイトル</summary>
        private string title;
        /// <summary>データソース</summary>
        private object DataSource;
        /// <summary>返却値</summary>
        private DataGridViewRow ReturnValue;
        /// <summary>グリッドのクリック画面遷移</summary>
        private bool GridviewClick = true;
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return title; } }

        /// <summary>タイトル</summary>
        public string TITLE { get { return title; } set { title = value; } }
        /// <summary>データソース</summary>
        public object DATASOURCE { get { return DataSource; } set { DataSource = value; } }

        /// <summary>返却値</summary>
        public DataGridViewRow RETURNVALUE { get { return ReturnValue; } set { ReturnValue = value; } }

        /// <summary>グリッドのクリック画面遷移</summary>
        public bool GRIDVIEW_CLICK { get { return GridviewClick; } set { GridviewClick = value; } }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataGridForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初期表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionListForm_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            //タイトル設定
            base.Text = this.FormTitle;
            base.ListFormTitleLabel.Text = this.FormTitle;

            //データ設定
            this.SectionListDataGridView.DataSource = DataSource;

            //初期表示時はグリッドの選択なし
            this.SectionListDataGridView.CurrentCell = null;
        }

        #region グリッドのクリック
        /// <summary>
        /// グリッドのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (GridviewClick == false) return;

            //選択されたデータを返却値に設定
            var grid = this.SectionListDataGridView;
            this.ReturnValue = grid.CurrentRow;

            base.FormOkClose();
        }
        #endregion
    }
}
