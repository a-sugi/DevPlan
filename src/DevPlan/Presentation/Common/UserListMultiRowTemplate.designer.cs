namespace DevPlan.Presentation
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class UserListMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.DepartmentCodeColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionCodeColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionGroupCodeColomn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.PersonelNameColomn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.PersonelIDColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DepartmentCodeColumn);
            this.Row.Cells.Add(this.SectionCodeColumn);
            this.Row.Cells.Add(this.SectionGroupCodeColomn);
            this.Row.Cells.Add(this.PersonelNameColomn);
            this.Row.Cells.Add(this.PersonelIDColumn);
            cellStyle12.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle12;
            this.Row.Height = 21;
            this.Row.Width = 508;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 508;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell1.Size = new System.Drawing.Size(93, 20);
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "部";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(93, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell2.Size = new System.Drawing.Size(90, 20);
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "課";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(183, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell3.Size = new System.Drawing.Size(120, 20);
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "担当";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.Location = new System.Drawing.Point(303, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell4.Size = new System.Drawing.Size(173, 20);
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "名前";
            // 
            // DepartmentCodeColumn
            // 
            this.DepartmentCodeColumn.DataField = "DEPARTMENT_CODE";
            this.DepartmentCodeColumn.Location = new System.Drawing.Point(0, 0);
            this.DepartmentCodeColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.DepartmentCodeColumn.Name = "DepartmentCodeColumn";
            this.DepartmentCodeColumn.ReadOnly = true;
            this.DepartmentCodeColumn.Size = new System.Drawing.Size(93, 21);
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            this.DepartmentCodeColumn.Style = cellStyle7;
            this.DepartmentCodeColumn.TabIndex = 0;
            // 
            // SectionCodeColumn
            // 
            this.SectionCodeColumn.DataField = "SECTION_CODE";
            this.SectionCodeColumn.Location = new System.Drawing.Point(93, 0);
            this.SectionCodeColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionCodeColumn.Name = "SectionCodeColumn";
            this.SectionCodeColumn.ReadOnly = true;
            this.SectionCodeColumn.Size = new System.Drawing.Size(90, 21);
            cellStyle8.Padding = new System.Windows.Forms.Padding(0);
            this.SectionCodeColumn.Style = cellStyle8;
            this.SectionCodeColumn.TabIndex = 1;
            // 
            // SectionGroupCodeColomn
            // 
            this.SectionGroupCodeColomn.DataField = "SECTION_GROUP_CODE";
            this.SectionGroupCodeColomn.Location = new System.Drawing.Point(183, 0);
            this.SectionGroupCodeColomn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionGroupCodeColomn.Name = "SectionGroupCodeColomn";
            this.SectionGroupCodeColomn.ReadOnly = true;
            this.SectionGroupCodeColomn.Size = new System.Drawing.Size(120, 21);
            cellStyle9.Padding = new System.Windows.Forms.Padding(0);
            this.SectionGroupCodeColomn.Style = cellStyle9;
            this.SectionGroupCodeColomn.TabIndex = 2;
            // 
            // PersonelNameColomn
            // 
            this.PersonelNameColomn.DataField = "NAME";
            this.PersonelNameColomn.Location = new System.Drawing.Point(303, 0);
            this.PersonelNameColomn.MinimumSize = new System.Drawing.Size(5, 0);
            this.PersonelNameColomn.Name = "PersonelNameColomn";
            this.PersonelNameColomn.ReadOnly = true;
            this.PersonelNameColomn.Size = new System.Drawing.Size(173, 21);
            cellStyle10.Padding = new System.Windows.Forms.Padding(0);
            this.PersonelNameColomn.Style = cellStyle10;
            this.PersonelNameColomn.TabIndex = 3;
            // 
            // PersonelIDColumn
            // 
            this.PersonelIDColumn.DataField = "PERSONEL_ID";
            this.PersonelIDColumn.Location = new System.Drawing.Point(565, 0);
            this.PersonelIDColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.PersonelIDColumn.Name = "PersonelIDColumn";
            this.PersonelIDColumn.ReadOnly = true;
            this.PersonelIDColumn.Size = new System.Drawing.Size(100, 21);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle11.Border = border2;
            cellStyle11.Padding = new System.Windows.Forms.Padding(0);
            this.PersonelIDColumn.Style = cellStyle11;
            this.PersonelIDColumn.TabIndex = 4;
            this.PersonelIDColumn.Visible = false;
            // 
            // UserListMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 508;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        private GrapeCity.Win.MultiRow.TextBoxCell DepartmentCodeColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SectionCodeColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SectionGroupCodeColomn;
        private GrapeCity.Win.MultiRow.TextBoxCell PersonelNameColomn;
        private GrapeCity.Win.MultiRow.TextBoxCell PersonelIDColumn;
        
    }
}
