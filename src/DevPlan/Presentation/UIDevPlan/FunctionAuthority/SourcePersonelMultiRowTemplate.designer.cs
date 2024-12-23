namespace DevPlan.Presentation
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class SourcePersonelMultiRowTemplate
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
            this.SourcePersonelSyokubanColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourcePersonelShimeiColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourcePersonelSyugyoColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourcePersonelBuColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourcePersonelKaColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourcePersonelTantoColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SourcePersonelYakusyokuColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.SourcePersonelSyokubanColumn);
            this.Row.Cells.Add(this.SourcePersonelShimeiColumn);
            this.Row.Cells.Add(this.SourcePersonelSyugyoColumn);
            this.Row.Cells.Add(this.SourcePersonelBuColumn);
            this.Row.Cells.Add(this.SourcePersonelKaColumn);
            this.Row.Cells.Add(this.SourcePersonelTantoColumn);
            this.Row.Cells.Add(this.SourcePersonelYakusyokuColumn);
            cellStyle8.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle8;
            this.Row.Height = 21;
            this.Row.Width = 783;
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
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 783;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell1.Size = new System.Drawing.Size(100, 20);
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "職番";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(100, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell2.Size = new System.Drawing.Size(131, 20);
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "氏名";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(231, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell3.Size = new System.Drawing.Size(71, 20);
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "就業状況";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.Location = new System.Drawing.Point(302, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell4.Size = new System.Drawing.Size(111, 20);
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "部";
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.Location = new System.Drawing.Point(413, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell5.Size = new System.Drawing.Size(111, 20);
            this.columnHeaderCell5.TabIndex = 4;
            this.columnHeaderCell5.Value = "課";
            // 
            // columnHeaderCell6
            // 
            this.columnHeaderCell6.Location = new System.Drawing.Point(524, 0);
            this.columnHeaderCell6.Name = "columnHeaderCell6";
            this.columnHeaderCell6.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell6.Size = new System.Drawing.Size(122, 20);
            this.columnHeaderCell6.TabIndex = 5;
            this.columnHeaderCell6.Value = "担当";
            // 
            // columnHeaderCell7
            // 
            this.columnHeaderCell7.Location = new System.Drawing.Point(646, 0);
            this.columnHeaderCell7.Name = "columnHeaderCell7";
            this.columnHeaderCell7.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell7.Size = new System.Drawing.Size(105, 20);
            this.columnHeaderCell7.TabIndex = 6;
            this.columnHeaderCell7.Value = "役職";
            // 
            // SourcePersonelSyokubanColumn
            // 
            this.SourcePersonelSyokubanColumn.DataField = "PERSONEL_ID";
            this.SourcePersonelSyokubanColumn.Location = new System.Drawing.Point(0, 0);
            this.SourcePersonelSyokubanColumn.Name = "SourcePersonelSyokubanColumn";
            this.SourcePersonelSyokubanColumn.ReadOnly = true;
            this.SourcePersonelSyokubanColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourcePersonelSyokubanColumn.Size = new System.Drawing.Size(100, 21);
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle1.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SourcePersonelSyokubanColumn.Style = cellStyle1;
            this.SourcePersonelSyokubanColumn.TabIndex = 0;
            // 
            // SourcePersonelShimeiColumn
            // 
            this.SourcePersonelShimeiColumn.DataField = "NAME";
            this.SourcePersonelShimeiColumn.Location = new System.Drawing.Point(100, 0);
            this.SourcePersonelShimeiColumn.Name = "SourcePersonelShimeiColumn";
            this.SourcePersonelShimeiColumn.ReadOnly = true;
            this.SourcePersonelShimeiColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourcePersonelShimeiColumn.Size = new System.Drawing.Size(131, 21);
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle2.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SourcePersonelShimeiColumn.Style = cellStyle2;
            this.SourcePersonelShimeiColumn.TabIndex = 1;
            // 
            // SourcePersonelSyugyoColumn
            // 
            this.SourcePersonelSyugyoColumn.DataField = "STATUS_CODE";
            this.SourcePersonelSyugyoColumn.Location = new System.Drawing.Point(231, 0);
            this.SourcePersonelSyugyoColumn.Name = "SourcePersonelSyugyoColumn";
            this.SourcePersonelSyugyoColumn.ReadOnly = true;
            this.SourcePersonelSyugyoColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourcePersonelSyugyoColumn.Size = new System.Drawing.Size(71, 21);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle3.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SourcePersonelSyugyoColumn.Style = cellStyle3;
            this.SourcePersonelSyugyoColumn.TabIndex = 2;
            // 
            // SourcePersonelBuColumn
            // 
            this.SourcePersonelBuColumn.DataField = "DEPARTMENT_CODE";
            this.SourcePersonelBuColumn.Location = new System.Drawing.Point(302, 0);
            this.SourcePersonelBuColumn.Name = "SourcePersonelBuColumn";
            this.SourcePersonelBuColumn.ReadOnly = true;
            this.SourcePersonelBuColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourcePersonelBuColumn.Size = new System.Drawing.Size(111, 21);
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle4.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SourcePersonelBuColumn.Style = cellStyle4;
            this.SourcePersonelBuColumn.TabIndex = 3;
            // 
            // SourcePersonelKaColumn
            // 
            this.SourcePersonelKaColumn.DataField = "SECTION_CODE";
            this.SourcePersonelKaColumn.Location = new System.Drawing.Point(413, 0);
            this.SourcePersonelKaColumn.Name = "SourcePersonelKaColumn";
            this.SourcePersonelKaColumn.ReadOnly = true;
            this.SourcePersonelKaColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourcePersonelKaColumn.Size = new System.Drawing.Size(111, 21);
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle5.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SourcePersonelKaColumn.Style = cellStyle5;
            this.SourcePersonelKaColumn.TabIndex = 4;
            // 
            // SourcePersonelTantoColumn
            // 
            this.SourcePersonelTantoColumn.DataField = "SECTION_GROUP_CODE";
            this.SourcePersonelTantoColumn.Location = new System.Drawing.Point(524, 0);
            this.SourcePersonelTantoColumn.Name = "SourcePersonelTantoColumn";
            this.SourcePersonelTantoColumn.ReadOnly = true;
            this.SourcePersonelTantoColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourcePersonelTantoColumn.Size = new System.Drawing.Size(122, 21);
            cellStyle6.Padding = new System.Windows.Forms.Padding(0);
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle6.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SourcePersonelTantoColumn.Style = cellStyle6;
            this.SourcePersonelTantoColumn.TabIndex = 5;
            // 
            // SourcePersonelYakusyokuColumn
            // 
            this.SourcePersonelYakusyokuColumn.DataField = "OFFICIAL_POSITION";
            this.SourcePersonelYakusyokuColumn.Location = new System.Drawing.Point(646, 0);
            this.SourcePersonelYakusyokuColumn.Name = "SourcePersonelYakusyokuColumn";
            this.SourcePersonelYakusyokuColumn.ReadOnly = true;
            this.SourcePersonelYakusyokuColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SourcePersonelYakusyokuColumn.Size = new System.Drawing.Size(105, 21);
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle7.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SourcePersonelYakusyokuColumn.Style = cellStyle7;
            this.SourcePersonelYakusyokuColumn.TabIndex = 6;
            // 
            // SourcePersonelMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 783;

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
        private GrapeCity.Win.MultiRow.TextBoxCell SourcePersonelSyokubanColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourcePersonelShimeiColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourcePersonelSyugyoColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourcePersonelBuColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourcePersonelKaColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourcePersonelTantoColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SourcePersonelYakusyokuColumn;
        
    }
}
