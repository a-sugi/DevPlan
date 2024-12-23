namespace DevPlan.Presentation
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class SinkPersonelMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.SinkPersonelRollIdColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SinkPersonelSyokubanColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.SinkPersonelShimeiColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.SinkPersonelRollIdColumn);
            this.Row.Cells.Add(this.SinkPersonelSyokubanColumn);
            this.Row.Cells.Add(this.SinkPersonelShimeiColumn);
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle4;
            this.Row.Height = 21;
            this.Row.Width = 278;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 278;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(1, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell1.Size = new System.Drawing.Size(1, 20);
            this.columnHeaderCell1.SortMode = GrapeCity.Win.MultiRow.SortMode.Automatic;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "ID";
            this.columnHeaderCell1.Visible = false;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell2.Size = new System.Drawing.Size(90, 20);
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "職番";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(90, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell3.Size = new System.Drawing.Size(157, 20);
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "氏名";
            // 
            // SinkPersonelRollIdColumn
            // 
            this.SinkPersonelRollIdColumn.DataField = "ROLL_ID";
            this.SinkPersonelRollIdColumn.Enabled = false;
            this.SinkPersonelRollIdColumn.Location = new System.Drawing.Point(1, 0);
            this.SinkPersonelRollIdColumn.Name = "SinkPersonelRollIdColumn";
            this.SinkPersonelRollIdColumn.ReadOnly = true;
            this.SinkPersonelRollIdColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkPersonelRollIdColumn.Size = new System.Drawing.Size(1, 21);
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            this.SinkPersonelRollIdColumn.Style = cellStyle1;
            this.SinkPersonelRollIdColumn.TabIndex = 0;
            this.SinkPersonelRollIdColumn.Visible = false;
            // 
            // SinkPersonelSyokubanColumn
            // 
            this.SinkPersonelSyokubanColumn.DataField = "PERSONEL_ID";
            this.SinkPersonelSyokubanColumn.Location = new System.Drawing.Point(0, 0);
            this.SinkPersonelSyokubanColumn.Name = "SinkPersonelSyokubanColumn";
            this.SinkPersonelSyokubanColumn.ReadOnly = true;
            this.SinkPersonelSyokubanColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkPersonelSyokubanColumn.Size = new System.Drawing.Size(90, 21);
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle2.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SinkPersonelSyokubanColumn.Style = cellStyle2;
            this.SinkPersonelSyokubanColumn.TabIndex = 1;
            // 
            // SinkPersonelShimeiColumn
            // 
            this.SinkPersonelShimeiColumn.DataField = "NAME";
            this.SinkPersonelShimeiColumn.Location = new System.Drawing.Point(90, 0);
            this.SinkPersonelShimeiColumn.Name = "SinkPersonelShimeiColumn";
            this.SinkPersonelShimeiColumn.ReadOnly = true;
            this.SinkPersonelShimeiColumn.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.SinkPersonelShimeiColumn.Size = new System.Drawing.Size(157, 21);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle3.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.SinkPersonelShimeiColumn.Style = cellStyle3;
            this.SinkPersonelShimeiColumn.TabIndex = 2;
            // 
            // SinkPersonelMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 278;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkPersonelRollIdColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkPersonelSyokubanColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell SinkPersonelShimeiColumn;
        
    }
}
