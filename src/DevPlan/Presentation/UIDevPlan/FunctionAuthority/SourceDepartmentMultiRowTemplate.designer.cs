namespace DevPlan.Presentation
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class SourceDepartmentMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell5 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell6 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.SourceDepartmentBuIdColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourceDepartmentKaIdColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourceDepartmentTantoIdColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourceDepartmentBuColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourceDepartmentKaColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourceDepartmentTantoColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.SourceDepartmentBuIdColumn);
            this.Row.Cells.Add(this.SourceDepartmentKaIdColumn);
            this.Row.Cells.Add(this.SourceDepartmentTantoIdColumn);
            this.Row.Cells.Add(this.SourceDepartmentBuColumn);
            this.Row.Cells.Add(this.SourceDepartmentKaColumn);
            this.Row.Cells.Add(this.SourceDepartmentTantoColumn);
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle7;
            this.Row.Height = 21;
            this.Row.Width = 411;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell5);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell6);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 411;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(1, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell1.Size = new System.Drawing.Size(1, 20);
            this.columnHeaderCell1.SortMode = GrapeCity.Win.MultiRow.SortMode.Automatic;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "部ID";
            this.columnHeaderCell1.Visible = false;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(2, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell2.Size = new System.Drawing.Size(1, 20);
            this.columnHeaderCell2.SortMode = GrapeCity.Win.MultiRow.SortMode.Automatic;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "課ID";
            this.columnHeaderCell2.Visible = false;
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(3, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell3.Size = new System.Drawing.Size(1, 20);
            this.columnHeaderCell3.SortMode = GrapeCity.Win.MultiRow.SortMode.Automatic;
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "担当ID";
            this.columnHeaderCell3.Visible = false;
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell4.Size = new System.Drawing.Size(130, 20);
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "部";
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.Location = new System.Drawing.Point(130, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell5.Size = new System.Drawing.Size(115, 20);
            this.columnHeaderCell5.TabIndex = 4;
            this.columnHeaderCell5.Value = "課";
            // 
            // columnHeaderCell6
            // 
            this.columnHeaderCell6.Location = new System.Drawing.Point(245, 0);
            this.columnHeaderCell6.Name = "columnHeaderCell6";
            this.columnHeaderCell6.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell6.Size = new System.Drawing.Size(134, 20);
            this.columnHeaderCell6.TabIndex = 5;
            this.columnHeaderCell6.Value = "担当";
            // 
            // SourceDepartmentBuIdColumn
            // 
            this.SourceDepartmentBuIdColumn.DataField = "DEPARTMENT_ID";
            this.SourceDepartmentBuIdColumn.Location = new System.Drawing.Point(426, 0);
            this.SourceDepartmentBuIdColumn.Name = "SourceDepartmentBuIdColumn";
            this.SourceDepartmentBuIdColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourceDepartmentBuIdColumn.Size = new System.Drawing.Size(1, 21);
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            this.SourceDepartmentBuIdColumn.Style = cellStyle1;
            this.SourceDepartmentBuIdColumn.TabIndex = 0;
            this.SourceDepartmentBuIdColumn.Visible = false;
            // 
            // SourceDepartmentKaIdColumn
            // 
            this.SourceDepartmentKaIdColumn.DataField = "SECTION_ID";
            this.SourceDepartmentKaIdColumn.Location = new System.Drawing.Point(413, 0);
            this.SourceDepartmentKaIdColumn.Name = "SourceDepartmentKaIdColumn";
            this.SourceDepartmentKaIdColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourceDepartmentKaIdColumn.Size = new System.Drawing.Size(1, 21);
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            this.SourceDepartmentKaIdColumn.Style = cellStyle2;
            this.SourceDepartmentKaIdColumn.TabIndex = 1;
            this.SourceDepartmentKaIdColumn.Visible = false;
            // 
            // SourceDepartmentTantoIdColumn
            // 
            this.SourceDepartmentTantoIdColumn.DataField = "SECTION_GROUP_ID";
            this.SourceDepartmentTantoIdColumn.Location = new System.Drawing.Point(444, 0);
            this.SourceDepartmentTantoIdColumn.Name = "SourceDepartmentTantoIdColumn";
            this.SourceDepartmentTantoIdColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourceDepartmentTantoIdColumn.Size = new System.Drawing.Size(1, 21);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            this.SourceDepartmentTantoIdColumn.Style = cellStyle3;
            this.SourceDepartmentTantoIdColumn.TabIndex = 2;
            this.SourceDepartmentTantoIdColumn.Visible = false;
            // 
            // SourceDepartmentBuColumn
            // 
            this.SourceDepartmentBuColumn.DataField = "DEPARTMENT_CODE";
            this.SourceDepartmentBuColumn.Location = new System.Drawing.Point(0, 0);
            this.SourceDepartmentBuColumn.Name = "SourceDepartmentBuColumn";
            this.SourceDepartmentBuColumn.ReadOnly = true;
            this.SourceDepartmentBuColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourceDepartmentBuColumn.Size = new System.Drawing.Size(130, 21);
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle4.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SourceDepartmentBuColumn.Style = cellStyle4;
            this.SourceDepartmentBuColumn.TabIndex = 3;
            // 
            // SourceDepartmentKaColumn
            // 
            this.SourceDepartmentKaColumn.DataField = "SECTION_CODE";
            this.SourceDepartmentKaColumn.Location = new System.Drawing.Point(130, 0);
            this.SourceDepartmentKaColumn.Name = "SourceDepartmentKaColumn";
            this.SourceDepartmentKaColumn.ReadOnly = true;
            this.SourceDepartmentKaColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourceDepartmentKaColumn.Size = new System.Drawing.Size(115, 21);
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle5.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SourceDepartmentKaColumn.Style = cellStyle5;
            this.SourceDepartmentKaColumn.TabIndex = 4;
            // 
            // SourceDepartmentTantoColumn
            // 
            this.SourceDepartmentTantoColumn.DataField = "SECTION_GROUP_CODE";
            this.SourceDepartmentTantoColumn.Location = new System.Drawing.Point(245, 0);
            this.SourceDepartmentTantoColumn.Name = "SourceDepartmentTantoColumn";
            this.SourceDepartmentTantoColumn.ReadOnly = true;
            this.SourceDepartmentTantoColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourceDepartmentTantoColumn.Size = new System.Drawing.Size(134, 21);
            cellStyle6.Padding = new System.Windows.Forms.Padding(0);
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle6.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SourceDepartmentTantoColumn.Style = cellStyle6;
            this.SourceDepartmentTantoColumn.TabIndex = 5;
            // 
            // SourceDepartmentMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 411;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell5;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell6;
        private GrapeCity.Win.MultiRow.TextBoxCell SourceDepartmentBuIdColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourceDepartmentKaIdColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourceDepartmentTantoIdColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourceDepartmentBuColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourceDepartmentKaColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourceDepartmentTantoColumn;
        
    }
}
