namespace DevPlan.Presentation.Common
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class UserListMultiSelectMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.CheckHeaderCell = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.DepartmentCodeColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionCodeColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SectionGroupCodeColomn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.PersonelNameColomn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.PersonelIDColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.CheckBoxColumn = new GrapeCity.Win.MultiRow.CheckBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.CheckBoxColumn);
            this.Row.Cells.Add(this.DepartmentCodeColumn);
            this.Row.Cells.Add(this.SectionCodeColumn);
            this.Row.Cells.Add(this.SectionGroupCodeColomn);
            this.Row.Cells.Add(this.PersonelNameColomn);
            this.Row.Cells.Add(this.PersonelIDColumn);
            this.Row.Height = 21;
            this.Row.Width = 506;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.CheckHeaderCell);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 506;
            // 
            // CheckHeaderCell
            // 
            this.CheckHeaderCell.Location = new System.Drawing.Point(0, 0);
            this.CheckHeaderCell.Name = "CheckHeaderCell";
            this.CheckHeaderCell.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.None;
            this.CheckHeaderCell.Size = new System.Drawing.Size(38, 23);
            this.CheckHeaderCell.TabIndex = 5;
            this.CheckHeaderCell.Value = "";
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(38, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell1.Size = new System.Drawing.Size(93, 20);
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "部";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(131, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell2.Size = new System.Drawing.Size(90, 20);
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "課";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(221, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell3.Size = new System.Drawing.Size(120, 20);
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "担当";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.Location = new System.Drawing.Point(341, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell4.Size = new System.Drawing.Size(133, 20);
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "名前";
            // 
            // DepartmentCodeColumn
            // 
            this.DepartmentCodeColumn.DataField = "DEPARTMENT_CODE";
            this.DepartmentCodeColumn.Location = new System.Drawing.Point(38, 0);
            this.DepartmentCodeColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.DepartmentCodeColumn.Name = "DepartmentCodeColumn";
            this.DepartmentCodeColumn.ReadOnly = true;
            this.DepartmentCodeColumn.Size = new System.Drawing.Size(93, 21);
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            this.DepartmentCodeColumn.Style = cellStyle2;
            this.DepartmentCodeColumn.TabIndex = 0;
            // 
            // SectionCodeColumn
            // 
            this.SectionCodeColumn.DataField = "SECTION_CODE";
            this.SectionCodeColumn.Location = new System.Drawing.Point(131, 0);
            this.SectionCodeColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionCodeColumn.Name = "SectionCodeColumn";
            this.SectionCodeColumn.ReadOnly = true;
            this.SectionCodeColumn.Size = new System.Drawing.Size(90, 21);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            this.SectionCodeColumn.Style = cellStyle3;
            this.SectionCodeColumn.TabIndex = 1;
            // 
            // SectionGroupCodeColomn
            // 
            this.SectionGroupCodeColomn.DataField = "SECTION_GROUP_CODE";
            this.SectionGroupCodeColomn.Location = new System.Drawing.Point(221, 0);
            this.SectionGroupCodeColomn.MinimumSize = new System.Drawing.Size(5, 0);
            this.SectionGroupCodeColomn.Name = "SectionGroupCodeColomn";
            this.SectionGroupCodeColomn.ReadOnly = true;
            this.SectionGroupCodeColomn.Size = new System.Drawing.Size(120, 21);
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            this.SectionGroupCodeColomn.Style = cellStyle4;
            this.SectionGroupCodeColomn.TabIndex = 2;
            // 
            // PersonelNameColomn
            // 
            this.PersonelNameColomn.DataField = "NAME";
            this.PersonelNameColomn.Location = new System.Drawing.Point(341, 0);
            this.PersonelNameColomn.MinimumSize = new System.Drawing.Size(5, 0);
            this.PersonelNameColomn.Name = "PersonelNameColomn";
            this.PersonelNameColomn.ReadOnly = true;
            this.PersonelNameColomn.Size = new System.Drawing.Size(133, 21);
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            this.PersonelNameColomn.Style = cellStyle5;
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
            cellStyle6.Border = border2;
            cellStyle6.Padding = new System.Windows.Forms.Padding(0);
            this.PersonelIDColumn.Style = cellStyle6;
            this.PersonelIDColumn.TabIndex = 4;
            this.PersonelIDColumn.Visible = false;
            // 
            // CheckBoxColumn
            // 
            this.CheckBoxColumn.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxColumn.DataField = "CHECKBOX";
            this.CheckBoxColumn.FalseValue = "false";
            this.CheckBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.CheckBoxColumn.Location = new System.Drawing.Point(0, 0);
            this.CheckBoxColumn.MinimumSize = new System.Drawing.Size(18, 0);
            this.CheckBoxColumn.Name = "CheckBoxColumn";
            this.CheckBoxColumn.Size = new System.Drawing.Size(38, 21);
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle1.Border = border1;
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle1.NullValue = false;
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.CheckBoxColumn.Style = cellStyle1;
            this.CheckBoxColumn.TabIndex = 5;
            this.CheckBoxColumn.TrueValue = "true";
            // 
            // UserListMultiSelectMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 506;

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
        private GrapeCity.Win.MultiRow.ColumnHeaderCell CheckHeaderCell;
        private GrapeCity.Win.MultiRow.CheckBoxCell CheckBoxColumn;
    }
}
