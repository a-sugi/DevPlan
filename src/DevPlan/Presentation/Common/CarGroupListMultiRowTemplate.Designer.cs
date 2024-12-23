namespace DevPlan.Presentation.Common
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class CarGroupListMultiRowTemplate
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region MultiRow Template Designer generated code

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.CarGroupDataGridViewTextBoxColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.CarGroupDataGridViewTextBoxColumn);
            this.Row.Height = 21;
            this.Row.Width = 260;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 260;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(229, 20);
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "車系";
            // 
            // CarGroupDataGridViewTextBoxColumn
            // 
            this.CarGroupDataGridViewTextBoxColumn.DataField = "CAR_GROUP";
            this.CarGroupDataGridViewTextBoxColumn.Location = new System.Drawing.Point(0, 0);
            this.CarGroupDataGridViewTextBoxColumn.MaxLength = 20;
            this.CarGroupDataGridViewTextBoxColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.CarGroupDataGridViewTextBoxColumn.Name = "CarGroupDataGridViewTextBoxColumn";
            this.CarGroupDataGridViewTextBoxColumn.ReadOnly = true;
            this.CarGroupDataGridViewTextBoxColumn.Size = new System.Drawing.Size(229, 21);
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            this.CarGroupDataGridViewTextBoxColumn.Style = cellStyle1;
            this.CarGroupDataGridViewTextBoxColumn.TabIndex = 0;
            // 
            // CarGroupListMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 260;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.TextBoxCell CarGroupDataGridViewTextBoxColumn;
    }
}
