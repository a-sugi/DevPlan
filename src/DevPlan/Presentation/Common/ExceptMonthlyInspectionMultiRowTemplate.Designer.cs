namespace DevPlan.Presentation.Common
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class ExceptMonthlyInspectionMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.CheckHeaderCell = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.CheckBoxColumn = new GrapeCity.Win.MultiRow.CheckBoxCell();
            this.DepartmentColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionIDColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.CheckBoxColumn);
            this.Row.Cells.Add(this.DepartmentColumn);
            this.Row.Cells.Add(this.SectionColumn);
            this.Row.Cells.Add(this.SectionIDColumn);
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle5;
            this.Row.Height = 21;
            this.Row.Width = 376;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.CheckHeaderCell);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Height = 23;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 376;
            // 
            // CheckHeaderCell
            // 
            this.CheckHeaderCell.Location = new System.Drawing.Point(0, 0);
            this.CheckHeaderCell.Name = "CheckHeaderCell";
            this.CheckHeaderCell.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.None;
            this.CheckHeaderCell.Size = new System.Drawing.Size(38, 23);
            this.CheckHeaderCell.TabIndex = 0;
            this.CheckHeaderCell.Value = "";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(38, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(145, 23);
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "部";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(183, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(153, 23);
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "課";
            // 
            // CheckBoxColumn
            // 
            this.CheckBoxColumn.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxColumn.FalseValue = "false";
            this.CheckBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.CheckBoxColumn.Location = new System.Drawing.Point(0, 0);
            this.CheckBoxColumn.MinimumSize = new System.Drawing.Size(18, 0);
            this.CheckBoxColumn.Name = "CheckBoxColumn";
            this.CheckBoxColumn.Size = new System.Drawing.Size(38, 21);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle1.NullValue = false;
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.CheckBoxColumn.Style = cellStyle1;
            this.CheckBoxColumn.TabIndex = 0;
            this.CheckBoxColumn.TrueValue = "true";
            // 
            // DepartmentColumn
            // 
            this.DepartmentColumn.DataField = "DEPARTMENT_CODE";
            this.DepartmentColumn.Location = new System.Drawing.Point(38, 0);
            this.DepartmentColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.DepartmentColumn.Name = "DepartmentColumn";
            this.DepartmentColumn.ReadOnly = true;
            this.DepartmentColumn.Size = new System.Drawing.Size(145, 21);
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            this.DepartmentColumn.Style = cellStyle2;
            this.DepartmentColumn.TabIndex = 1;
            // 
            // SectionColumn
            // 
            this.SectionColumn.DataField = "SECTION_CODE";
            this.SectionColumn.Location = new System.Drawing.Point(183, 0);
            this.SectionColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionColumn.Name = "SectionColumn";
            this.SectionColumn.ReadOnly = true;
            this.SectionColumn.Size = new System.Drawing.Size(153, 21);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            this.SectionColumn.Style = cellStyle3;
            this.SectionColumn.TabIndex = 2;
            // 
            // SectionIDColumn
            // 
            this.SectionIDColumn.DataField = "SECTION_ID";
            this.SectionIDColumn.Location = new System.Drawing.Point(656, 0);
            this.SectionIDColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionIDColumn.Name = "SectionIDColumn";
            this.SectionIDColumn.ReadOnly = true;
            this.SectionIDColumn.Size = new System.Drawing.Size(153, 21);
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            this.SectionIDColumn.Style = cellStyle4;
            this.SectionIDColumn.TabIndex = 4;
            // 
            // ExceptMonthlyInspectionMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 44;
            this.Width = 376;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell CheckHeaderCell;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.CheckBoxCell CheckBoxColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell DepartmentColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SectionColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SectionIDColumn;
    }
}
