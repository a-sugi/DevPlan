namespace DevPlan.Presentation.UIDevPlan.BrowsingAuthority
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class GeneralCodeAuthorityTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell5 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell6 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell7 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell8 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell9 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.BuColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.KaColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.TantoColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SyokubanColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.ShimeiColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.CarGroupColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.GeneralCodeColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.StartDateColumn = new DevPlan.Presentation.UC.MultiRow.NullableDateTimePickerCell();
            this.EndDateColumn = new DevPlan.Presentation.UC.MultiRow.NullableDateTimePickerCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.BuColumn);
            this.Row.Cells.Add(this.KaColumn);
            this.Row.Cells.Add(this.TantoColumn);
            this.Row.Cells.Add(this.SyokubanColumn);
            this.Row.Cells.Add(this.ShimeiColumn);
            this.Row.Cells.Add(this.CarGroupColumn);
            this.Row.Cells.Add(this.GeneralCodeColumn);
            this.Row.Cells.Add(this.StartDateColumn);
            this.Row.Cells.Add(this.EndDateColumn);
            cellStyle8.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle8;
            this.Row.Height = 21;
            this.Row.Width = 1150;
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
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell9);
            this.columnHeaderSection1.Height = 23;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 1150;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(146, 23);
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "部";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(146, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(146, 23);
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "課";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(292, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(146, 23);
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "担当";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.Location = new System.Drawing.Point(438, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(100, 23);
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "職番";
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.Location = new System.Drawing.Point(538, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.Size = new System.Drawing.Size(159, 23);
            this.columnHeaderCell5.TabIndex = 4;
            this.columnHeaderCell5.Value = "氏名";
            // 
            // columnHeaderCell6
            // 
            this.columnHeaderCell6.Location = new System.Drawing.Point(697, 0);
            this.columnHeaderCell6.Name = "columnHeaderCell6";
            this.columnHeaderCell6.Size = new System.Drawing.Size(80, 23);
            this.columnHeaderCell6.TabIndex = 5;
            this.columnHeaderCell6.Value = "車系";
            // 
            // columnHeaderCell7
            // 
            this.columnHeaderCell7.Location = new System.Drawing.Point(777, 0);
            this.columnHeaderCell7.Name = "columnHeaderCell7";
            this.columnHeaderCell7.Size = new System.Drawing.Size(80, 23);
            this.columnHeaderCell7.TabIndex = 6;
            this.columnHeaderCell7.Value = "車種";
            // 
            // columnHeaderCell8
            // 
            this.columnHeaderCell8.Location = new System.Drawing.Point(857, 0);
            this.columnHeaderCell8.Name = "columnHeaderCell8";
            this.columnHeaderCell8.Size = new System.Drawing.Size(131, 23);
            this.columnHeaderCell8.TabIndex = 7;
            this.columnHeaderCell8.Value = "許可期間(開始)";
            // 
            // columnHeaderCell9
            // 
            this.columnHeaderCell9.Location = new System.Drawing.Point(988, 0);
            this.columnHeaderCell9.Name = "columnHeaderCell9";
            this.columnHeaderCell9.Size = new System.Drawing.Size(131, 23);
            this.columnHeaderCell9.TabIndex = 8;
            this.columnHeaderCell9.Value = "許可期間(終了)";
            // 
            // BuColumn
            // 
            this.BuColumn.DataField = "DEPARTMENT_CODE";
            this.BuColumn.Location = new System.Drawing.Point(0, 0);
            this.BuColumn.Mergeable = true;
            this.BuColumn.Name = "BuColumn";
            this.BuColumn.ReadOnly = true;
            this.BuColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.BuColumn.Size = new System.Drawing.Size(146, 21);
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            cellStyle1.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.BuColumn.Style = cellStyle1;
            this.BuColumn.TabIndex = 0;
            // 
            // KaColumn
            // 
            this.KaColumn.DataField = "SECTION_CODE";
            this.KaColumn.Location = new System.Drawing.Point(146, 0);
            this.KaColumn.Mergeable = true;
            this.KaColumn.Name = "KaColumn";
            this.KaColumn.ReadOnly = true;
            this.KaColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.KaColumn.Size = new System.Drawing.Size(146, 21);
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            cellStyle2.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.KaColumn.Style = cellStyle2;
            this.KaColumn.TabIndex = 1;
            // 
            // TantoColumn
            // 
            this.TantoColumn.DataField = "SECTION_GROUP_CODE";
            this.TantoColumn.Location = new System.Drawing.Point(292, 0);
            this.TantoColumn.Mergeable = true;
            this.TantoColumn.Name = "TantoColumn";
            this.TantoColumn.ReadOnly = true;
            this.TantoColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.TantoColumn.Size = new System.Drawing.Size(146, 21);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            cellStyle3.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.TantoColumn.Style = cellStyle3;
            this.TantoColumn.TabIndex = 2;
            // 
            // SyokubanColumn
            // 
            this.SyokubanColumn.DataField = "PERSONEL_ID";
            this.SyokubanColumn.Location = new System.Drawing.Point(438, 0);
            this.SyokubanColumn.Mergeable = true;
            this.SyokubanColumn.Name = "SyokubanColumn";
            this.SyokubanColumn.ReadOnly = true;
            this.SyokubanColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SyokubanColumn.Size = new System.Drawing.Size(100, 21);
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopRight;
            cellStyle4.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SyokubanColumn.Style = cellStyle4;
            this.SyokubanColumn.TabIndex = 3;
            // 
            // ShimeiColumn
            // 
            this.ShimeiColumn.DataField = "NAME";
            this.ShimeiColumn.Location = new System.Drawing.Point(538, 0);
            this.ShimeiColumn.Mergeable = true;
            this.ShimeiColumn.Name = "ShimeiColumn";
            this.ShimeiColumn.ReadOnly = true;
            this.ShimeiColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.ShimeiColumn.Size = new System.Drawing.Size(159, 21);
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            cellStyle5.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.ShimeiColumn.Style = cellStyle5;
            this.ShimeiColumn.TabIndex = 4;
            // 
            // CarGroupColumn
            // 
            this.CarGroupColumn.DataField = "CAR_GROUP";
            this.CarGroupColumn.Location = new System.Drawing.Point(697, 0);
            this.CarGroupColumn.Mergeable = true;
            this.CarGroupColumn.Name = "CarGroupColumn";
            this.CarGroupColumn.ReadOnly = true;
            this.CarGroupColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            cellStyle6.Padding = new System.Windows.Forms.Padding(0);
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            cellStyle6.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.CarGroupColumn.Style = cellStyle6;
            this.CarGroupColumn.TabIndex = 5;
            // 
            // GeneralCodeColumn
            // 
            this.GeneralCodeColumn.DataField = "GENERAL_CODE";
            this.GeneralCodeColumn.Location = new System.Drawing.Point(777, 0);
            this.GeneralCodeColumn.Name = "GeneralCodeColumn";
            this.GeneralCodeColumn.ReadOnly = true;
            this.GeneralCodeColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle7.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.GeneralCodeColumn.Style = cellStyle7;
            this.GeneralCodeColumn.TabIndex = 6;
            // 
            // StartDateColumn
            // 
            this.StartDateColumn.CustomFormat = "yyyy/MM/dd";
            this.StartDateColumn.DataField = "PERMISSION_PERIOD_START";
            this.StartDateColumn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDateColumn.Location = new System.Drawing.Point(857, 0);
            this.StartDateColumn.Name = "StartDateColumn";
            this.StartDateColumn.ShowDropDownButton = GrapeCity.Win.MultiRow.CellButtonVisibility.ShowForCurrentCell;
            this.StartDateColumn.Size = new System.Drawing.Size(131, 21);
            this.StartDateColumn.TabIndex = 7;
            // 
            // EndDateColumn
            // 
            this.EndDateColumn.CustomFormat = "yyyy/MM/dd";
            this.EndDateColumn.DataField = "PERMISSION_PERIOD_END";
            this.EndDateColumn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDateColumn.Location = new System.Drawing.Point(988, 0);
            this.EndDateColumn.Name = "EndDateColumn";
            this.EndDateColumn.ShowDropDownButton = GrapeCity.Win.MultiRow.CellButtonVisibility.ShowForCurrentCell;
            this.EndDateColumn.Size = new System.Drawing.Size(131, 21);
            this.EndDateColumn.TabIndex = 8;
            // 
            // GeneralCodeAuthorityTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 44;
            this.Width = 1150;

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
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell9;
        private GrapeCity.Win.MultiRow.TextBoxCell BuColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell KaColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell TantoColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SyokubanColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell ShimeiColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell CarGroupColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell GeneralCodeColumn;
        private UC.MultiRow.NullableDateTimePickerCell StartDateColumn;
        private UC.MultiRow.NullableDateTimePickerCell EndDateColumn;
    }
}
