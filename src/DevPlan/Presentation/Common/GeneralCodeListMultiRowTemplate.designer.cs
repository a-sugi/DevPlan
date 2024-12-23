namespace DevPlan.Presentation
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class GeneralCodeListMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.CarGroupDataGridViewTextBoxColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.GeneralCodeDataGridViewTextBoxColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.CarGroupDataGridViewTextBoxColumn);
            this.Row.Cells.Add(this.GeneralCodeDataGridViewTextBoxColumn);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle3;
            this.Row.Height = 21;
            this.Row.Width = 368;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 368;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(140, 20);
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "車系";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(140, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(197, 20);
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "開発符号";
            // 
            // CarGroupDataGridViewTextBoxColumn
            // 
            this.CarGroupDataGridViewTextBoxColumn.DataField = "CAR_GROUP";
            this.CarGroupDataGridViewTextBoxColumn.Location = new System.Drawing.Point(0, 0);
            this.CarGroupDataGridViewTextBoxColumn.MaxLength = 20;
            this.CarGroupDataGridViewTextBoxColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.CarGroupDataGridViewTextBoxColumn.Name = "CarGroupDataGridViewTextBoxColumn";
            this.CarGroupDataGridViewTextBoxColumn.ReadOnly = true;
            this.CarGroupDataGridViewTextBoxColumn.Size = new System.Drawing.Size(140, 21);
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            this.CarGroupDataGridViewTextBoxColumn.Style = cellStyle1;
            this.CarGroupDataGridViewTextBoxColumn.TabIndex = 0;
            // 
            // GeneralCodeDataGridViewTextBoxColumn
            // 
            this.GeneralCodeDataGridViewTextBoxColumn.DataField = "GENERAL_CODE";
            this.GeneralCodeDataGridViewTextBoxColumn.Location = new System.Drawing.Point(140, 0);
            this.GeneralCodeDataGridViewTextBoxColumn.MaxLength = 20;
            this.GeneralCodeDataGridViewTextBoxColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.GeneralCodeDataGridViewTextBoxColumn.Name = "GeneralCodeDataGridViewTextBoxColumn";
            this.GeneralCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.GeneralCodeDataGridViewTextBoxColumn.Size = new System.Drawing.Size(197, 21);
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            this.GeneralCodeDataGridViewTextBoxColumn.Style = cellStyle2;
            this.GeneralCodeDataGridViewTextBoxColumn.TabIndex = 1;
            // 
            // GeneralCodeListMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 368;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.TextBoxCell CarGroupDataGridViewTextBoxColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell GeneralCodeDataGridViewTextBoxColumn;
        
    }
}
