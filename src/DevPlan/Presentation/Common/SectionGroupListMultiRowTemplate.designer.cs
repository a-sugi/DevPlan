namespace DevPlan.Presentation
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class SectionGroupListMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.DepartmentColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionGroupColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.DepartmentIDColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionIDColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionGroupIDColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.DepartmentNameColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionNameColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionGroupNameColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.EstablishmentColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DepartmentColumn);
            this.Row.Cells.Add(this.SectionColumn);
            this.Row.Cells.Add(this.SectionGroupColumn);
            this.Row.Cells.Add(this.DepartmentIDColumn);
            this.Row.Cells.Add(this.SectionIDColumn);
            this.Row.Cells.Add(this.SectionGroupIDColumn);
            this.Row.Cells.Add(this.DepartmentNameColumn);
            this.Row.Cells.Add(this.SectionNameColumn);
            this.Row.Cells.Add(this.SectionGroupNameColumn);
            this.Row.Cells.Add(this.EstablishmentColumn);
            cellStyle11.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle11;
            this.Row.Height = 21;
            this.Row.Width = 368;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 368;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell1.Size = new System.Drawing.Size(100, 20);
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "部";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(100, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell2.Size = new System.Drawing.Size(100, 20);
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "課";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(200, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell3.Size = new System.Drawing.Size(137, 20);
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "担当";
            // 
            // DepartmentColumn
            // 
            this.DepartmentColumn.DataField = "DEPARTMENT_CODE";
            this.DepartmentColumn.Location = new System.Drawing.Point(0, 0);
            this.DepartmentColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.DepartmentColumn.Name = "DepartmentColumn";
            this.DepartmentColumn.ReadOnly = true;
            this.DepartmentColumn.Size = new System.Drawing.Size(100, 21);
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            this.DepartmentColumn.Style = cellStyle1;
            this.DepartmentColumn.TabIndex = 0;
            // 
            // SectionColumn
            // 
            this.SectionColumn.DataField = "SECTION_CODE";
            this.SectionColumn.Location = new System.Drawing.Point(100, 0);
            this.SectionColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionColumn.Name = "SectionColumn";
            this.SectionColumn.ReadOnly = true;
            this.SectionColumn.Size = new System.Drawing.Size(100, 21);
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            this.SectionColumn.Style = cellStyle2;
            this.SectionColumn.TabIndex = 1;
            // 
            // SectionGroupColumn
            // 
            this.SectionGroupColumn.DataField = "SECTION_GROUP_CODE";
            this.SectionGroupColumn.Location = new System.Drawing.Point(200, 0);
            this.SectionGroupColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionGroupColumn.Name = "SectionGroupColumn";
            this.SectionGroupColumn.ReadOnly = true;
            this.SectionGroupColumn.Size = new System.Drawing.Size(137, 21);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            this.SectionGroupColumn.Style = cellStyle3;
            this.SectionGroupColumn.TabIndex = 2;
            // 
            // DepartmentIDColumn
            // 
            this.DepartmentIDColumn.DataField = "DEPARTMENT_ID";
            this.DepartmentIDColumn.Location = new System.Drawing.Point(425, 0);
            this.DepartmentIDColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.DepartmentIDColumn.Name = "DepartmentIDColumn";
            this.DepartmentIDColumn.ReadOnly = true;
            this.DepartmentIDColumn.Size = new System.Drawing.Size(100, 21);
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle4.Border = border1;
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            this.DepartmentIDColumn.Style = cellStyle4;
            this.DepartmentIDColumn.TabIndex = 3;
            this.DepartmentIDColumn.Visible = false;
            // 
            // SectionIDColumn
            // 
            this.SectionIDColumn.DataField = "SECTION_ID";
            this.SectionIDColumn.Location = new System.Drawing.Point(525, 0);
            this.SectionIDColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionIDColumn.Name = "SectionIDColumn";
            this.SectionIDColumn.ReadOnly = true;
            this.SectionIDColumn.Size = new System.Drawing.Size(100, 21);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle5.Border = border2;
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            this.SectionIDColumn.Style = cellStyle5;
            this.SectionIDColumn.TabIndex = 4;
            this.SectionIDColumn.Visible = false;
            // 
            // SectionGroupIDColumn
            // 
            this.SectionGroupIDColumn.DataField = "SECTION_GROUP_ID";
            this.SectionGroupIDColumn.Location = new System.Drawing.Point(625, 0);
            this.SectionGroupIDColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionGroupIDColumn.Name = "SectionGroupIDColumn";
            this.SectionGroupIDColumn.ReadOnly = true;
            this.SectionGroupIDColumn.Size = new System.Drawing.Size(100, 21);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle6.Border = border3;
            cellStyle6.Padding = new System.Windows.Forms.Padding(0);
            this.SectionGroupIDColumn.Style = cellStyle6;
            this.SectionGroupIDColumn.TabIndex = 5;
            this.SectionGroupIDColumn.Visible = false;
            // 
            // DepartmentNameColumn
            // 
            this.DepartmentNameColumn.DataField = "DEPARTMENT_NAME";
            this.DepartmentNameColumn.Location = new System.Drawing.Point(725, 0);
            this.DepartmentNameColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.DepartmentNameColumn.Name = "DepartmentNameColumn";
            this.DepartmentNameColumn.ReadOnly = true;
            this.DepartmentNameColumn.Size = new System.Drawing.Size(100, 21);
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle7.Border = border4;
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            this.DepartmentNameColumn.Style = cellStyle7;
            this.DepartmentNameColumn.TabIndex = 6;
            this.DepartmentNameColumn.Visible = false;
            // 
            // SectionNameColumn
            // 
            this.SectionNameColumn.DataField = "SECTION_NAME";
            this.SectionNameColumn.Location = new System.Drawing.Point(825, 0);
            this.SectionNameColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionNameColumn.Name = "SectionNameColumn";
            this.SectionNameColumn.ReadOnly = true;
            this.SectionNameColumn.Size = new System.Drawing.Size(100, 21);
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle8.Border = border5;
            cellStyle8.Padding = new System.Windows.Forms.Padding(0);
            this.SectionNameColumn.Style = cellStyle8;
            this.SectionNameColumn.TabIndex = 7;
            this.SectionNameColumn.Visible = false;
            // 
            // SectionGroupNameColumn
            // 
            this.SectionGroupNameColumn.DataField = "SECTION_GROUP_NAME";
            this.SectionGroupNameColumn.Location = new System.Drawing.Point(925, 0);
            this.SectionGroupNameColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionGroupNameColumn.Name = "SectionGroupNameColumn";
            this.SectionGroupNameColumn.ReadOnly = true;
            this.SectionGroupNameColumn.Size = new System.Drawing.Size(100, 21);
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle9.Border = border6;
            cellStyle9.Padding = new System.Windows.Forms.Padding(0);
            this.SectionGroupNameColumn.Style = cellStyle9;
            this.SectionGroupNameColumn.TabIndex = 8;
            this.SectionGroupNameColumn.Visible = false;
            // 
            // EstablishmentColumn
            // 
            this.EstablishmentColumn.Location = new System.Drawing.Point(1025, 0);
            this.EstablishmentColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.EstablishmentColumn.Name = "EstablishmentColumn";
            this.EstablishmentColumn.ReadOnly = true;
            this.EstablishmentColumn.Size = new System.Drawing.Size(100, 21);
            border7.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle10.Border = border7;
            cellStyle10.Padding = new System.Windows.Forms.Padding(0);
            this.EstablishmentColumn.Style = cellStyle10;
            this.EstablishmentColumn.TabIndex = 9;
            this.EstablishmentColumn.Visible = false;
            // 
            // SectionGroupListMultiRowTemplate
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
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.TextBoxCell DepartmentColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SectionColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SectionGroupColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell DepartmentIDColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SectionIDColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SectionGroupIDColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell DepartmentNameColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SectionNameColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SectionGroupNameColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell EstablishmentColumn;
        
    }
}
