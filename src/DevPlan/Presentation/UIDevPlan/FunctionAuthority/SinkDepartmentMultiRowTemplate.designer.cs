namespace DevPlan.Presentation
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class SinkDepartmentMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle27 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle19 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle20 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle21 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle22 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle23 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle24 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle25 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle26 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell5 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell6 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell7 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell8 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.SinkDepartmentRollIdColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SinkDepartmentBuIdColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SinkDepartmentKaIdColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SinkDepartmentTantoIdColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SinkDepartmentBuColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SinkDepartmentKaColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SinkDepartmentTantoColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SinkDepartmentYakusyokuColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.SinkDepartmentRollIdColumn);
            this.Row.Cells.Add(this.SinkDepartmentBuIdColumn);
            this.Row.Cells.Add(this.SinkDepartmentKaIdColumn);
            this.Row.Cells.Add(this.SinkDepartmentTantoIdColumn);
            this.Row.Cells.Add(this.SinkDepartmentBuColumn);
            this.Row.Cells.Add(this.SinkDepartmentKaColumn);
            this.Row.Cells.Add(this.SinkDepartmentTantoColumn);
            this.Row.Cells.Add(this.SinkDepartmentYakusyokuColumn);
            cellStyle27.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle27;
            this.Row.Height = 21;
            this.Row.Width = 475;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell5);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell6);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell7);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell8);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 475;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(1, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell1.Size = new System.Drawing.Size(1, 20);
            this.columnHeaderCell1.SortMode = GrapeCity.Win.MultiRow.SortMode.Automatic;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "ロールID";
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
            this.columnHeaderCell2.Value = "部ID";
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
            this.columnHeaderCell3.Value = "課ID";
            this.columnHeaderCell3.Visible = false;
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.Location = new System.Drawing.Point(4, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell4.Size = new System.Drawing.Size(1, 20);
            this.columnHeaderCell4.SortMode = GrapeCity.Win.MultiRow.SortMode.Automatic;
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "担当ID";
            this.columnHeaderCell4.Visible = false;
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell5.Size = new System.Drawing.Size(115, 20);
            this.columnHeaderCell5.TabIndex = 4;
            this.columnHeaderCell5.Value = "部";
            // 
            // columnHeaderCell6
            // 
            this.columnHeaderCell6.Location = new System.Drawing.Point(115, 0);
            this.columnHeaderCell6.Name = "columnHeaderCell6";
            this.columnHeaderCell6.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell6.Size = new System.Drawing.Size(115, 20);
            this.columnHeaderCell6.TabIndex = 5;
            this.columnHeaderCell6.Value = "課";
            // 
            // columnHeaderCell7
            // 
            this.columnHeaderCell7.Location = new System.Drawing.Point(230, 0);
            this.columnHeaderCell7.Name = "columnHeaderCell7";
            this.columnHeaderCell7.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell7.Size = new System.Drawing.Size(115, 20);
            this.columnHeaderCell7.TabIndex = 6;
            this.columnHeaderCell7.Value = "担当";
            // 
            // columnHeaderCell8
            // 
            this.columnHeaderCell8.Location = new System.Drawing.Point(345, 0);
            this.columnHeaderCell8.Name = "columnHeaderCell8";
            this.columnHeaderCell8.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell8.Size = new System.Drawing.Size(98, 20);
            this.columnHeaderCell8.TabIndex = 7;
            this.columnHeaderCell8.Value = "役職";
            // 
            // SinkDepartmentRollIdColumn
            // 
            this.SinkDepartmentRollIdColumn.DataField = "ROLL_ID";
            this.SinkDepartmentRollIdColumn.Location = new System.Drawing.Point(536, 0);
            this.SinkDepartmentRollIdColumn.Name = "SinkDepartmentRollIdColumn";
            this.SinkDepartmentRollIdColumn.ReadOnly = true;
            this.SinkDepartmentRollIdColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkDepartmentRollIdColumn.Size = new System.Drawing.Size(1, 21);
            cellStyle19.Padding = new System.Windows.Forms.Padding(0);
            this.SinkDepartmentRollIdColumn.Style = cellStyle19;
            this.SinkDepartmentRollIdColumn.TabIndex = 0;
            this.SinkDepartmentRollIdColumn.Visible = false;
            // 
            // SinkDepartmentBuIdColumn
            // 
            this.SinkDepartmentBuIdColumn.DataField = "DEPARTMENT_ID";
            this.SinkDepartmentBuIdColumn.Location = new System.Drawing.Point(504, 0);
            this.SinkDepartmentBuIdColumn.Name = "SinkDepartmentBuIdColumn";
            this.SinkDepartmentBuIdColumn.ReadOnly = true;
            this.SinkDepartmentBuIdColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkDepartmentBuIdColumn.Size = new System.Drawing.Size(1, 21);
            cellStyle20.Padding = new System.Windows.Forms.Padding(0);
            this.SinkDepartmentBuIdColumn.Style = cellStyle20;
            this.SinkDepartmentBuIdColumn.TabIndex = 1;
            this.SinkDepartmentBuIdColumn.Visible = false;
            // 
            // SinkDepartmentKaIdColumn
            // 
            this.SinkDepartmentKaIdColumn.DataField = "SECTION_ID";
            this.SinkDepartmentKaIdColumn.Location = new System.Drawing.Point(522, 0);
            this.SinkDepartmentKaIdColumn.Name = "SinkDepartmentKaIdColumn";
            this.SinkDepartmentKaIdColumn.ReadOnly = true;
            this.SinkDepartmentKaIdColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkDepartmentKaIdColumn.Size = new System.Drawing.Size(1, 21);
            cellStyle21.Padding = new System.Windows.Forms.Padding(0);
            this.SinkDepartmentKaIdColumn.Style = cellStyle21;
            this.SinkDepartmentKaIdColumn.TabIndex = 2;
            this.SinkDepartmentKaIdColumn.Visible = false;
            // 
            // SinkDepartmentTantoIdColumn
            // 
            this.SinkDepartmentTantoIdColumn.DataField = "SECTION_GROUP_ID";
            this.SinkDepartmentTantoIdColumn.Location = new System.Drawing.Point(547, 0);
            this.SinkDepartmentTantoIdColumn.Name = "SinkDepartmentTantoIdColumn";
            this.SinkDepartmentTantoIdColumn.ReadOnly = true;
            this.SinkDepartmentTantoIdColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkDepartmentTantoIdColumn.Size = new System.Drawing.Size(1, 21);
            cellStyle22.Padding = new System.Windows.Forms.Padding(0);
            this.SinkDepartmentTantoIdColumn.Style = cellStyle22;
            this.SinkDepartmentTantoIdColumn.TabIndex = 3;
            this.SinkDepartmentTantoIdColumn.Visible = false;
            // 
            // SinkDepartmentBuColumn
            // 
            this.SinkDepartmentBuColumn.DataField = "DEPARTMENT_CODE";
            this.SinkDepartmentBuColumn.Location = new System.Drawing.Point(0, 0);
            this.SinkDepartmentBuColumn.Name = "SinkDepartmentBuColumn";
            this.SinkDepartmentBuColumn.ReadOnly = true;
            this.SinkDepartmentBuColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkDepartmentBuColumn.Size = new System.Drawing.Size(115, 21);
            cellStyle23.Padding = new System.Windows.Forms.Padding(0);
            cellStyle23.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle23.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SinkDepartmentBuColumn.Style = cellStyle23;
            this.SinkDepartmentBuColumn.TabIndex = 4;
            // 
            // SinkDepartmentKaColumn
            // 
            this.SinkDepartmentKaColumn.DataField = "SECTION_CODE";
            this.SinkDepartmentKaColumn.Location = new System.Drawing.Point(115, 0);
            this.SinkDepartmentKaColumn.Name = "SinkDepartmentKaColumn";
            this.SinkDepartmentKaColumn.ReadOnly = true;
            this.SinkDepartmentKaColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkDepartmentKaColumn.Size = new System.Drawing.Size(115, 21);
            cellStyle24.Padding = new System.Windows.Forms.Padding(0);
            cellStyle24.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SinkDepartmentKaColumn.Style = cellStyle24;
            this.SinkDepartmentKaColumn.TabIndex = 5;
            // 
            // SinkDepartmentTantoColumn
            // 
            this.SinkDepartmentTantoColumn.DataField = "SECTION_GROUP_CODE";
            this.SinkDepartmentTantoColumn.Location = new System.Drawing.Point(230, 0);
            this.SinkDepartmentTantoColumn.Name = "SinkDepartmentTantoColumn";
            this.SinkDepartmentTantoColumn.ReadOnly = true;
            this.SinkDepartmentTantoColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkDepartmentTantoColumn.Size = new System.Drawing.Size(115, 21);
            cellStyle25.Padding = new System.Windows.Forms.Padding(0);
            cellStyle25.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SinkDepartmentTantoColumn.Style = cellStyle25;
            this.SinkDepartmentTantoColumn.TabIndex = 6;
            // 
            // SinkDepartmentYakusyokuColumn
            // 
            this.SinkDepartmentYakusyokuColumn.DataField = "OFFICIAL_POSITION";
            this.SinkDepartmentYakusyokuColumn.Location = new System.Drawing.Point(345, 0);
            this.SinkDepartmentYakusyokuColumn.Name = "SinkDepartmentYakusyokuColumn";
            this.SinkDepartmentYakusyokuColumn.ReadOnly = true;
            this.SinkDepartmentYakusyokuColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkDepartmentYakusyokuColumn.Size = new System.Drawing.Size(98, 21);
            cellStyle26.Padding = new System.Windows.Forms.Padding(0);
            cellStyle26.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SinkDepartmentYakusyokuColumn.Style = cellStyle26;
            this.SinkDepartmentYakusyokuColumn.TabIndex = 7;
            // 
            // SinkDepartmentMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 475;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell5;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell6;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell7;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell8;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkDepartmentRollIdColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkDepartmentBuIdColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkDepartmentKaIdColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkDepartmentTantoIdColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkDepartmentBuColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkDepartmentKaColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkDepartmentTantoColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkDepartmentYakusyokuColumn;
        
    }
}
