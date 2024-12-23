namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class DesignCheckMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            this.OpenCountColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.CloseCountColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.BaseInfoLinkColumn = new GrapeCity.Win.MultiRow.LinkLabelCell();
            this.IdColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.NameColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.TimesColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell7 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.DesignCheckNameLinkColumn = new GrapeCity.Win.MultiRow.LinkLabelCell();
            this.OpenDateColumn = new DevPlan.Presentation.UC.MultiRow.NullableDateTimePickerCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.OpenDateColumn);
            this.Row.Cells.Add(this.DesignCheckNameLinkColumn);
            this.Row.Cells.Add(this.OpenCountColumn);
            this.Row.Cells.Add(this.CloseCountColumn);
            this.Row.Cells.Add(this.BaseInfoLinkColumn);
            this.Row.Cells.Add(this.IdColumn);
            this.Row.Cells.Add(this.NameColumn);
            this.Row.Cells.Add(this.TimesColumn);
            cellStyle9.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle9;
            this.Row.Height = 21;
            this.Row.Width = 949;
            // 
            // OpenCountColumn
            // 
            this.OpenCountColumn.DataField = "OPEN_COUNT";
            this.OpenCountColumn.Location = new System.Drawing.Point(617, 0);
            this.OpenCountColumn.Name = "OpenCountColumn";
            this.OpenCountColumn.ReadOnly = true;
            this.OpenCountColumn.Size = new System.Drawing.Size(100, 21);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.OpenCountColumn.Style = cellStyle3;
            this.OpenCountColumn.TabIndex = 2;
            // 
            // CloseCountColumn
            // 
            this.CloseCountColumn.DataField = "CLOSE_COUNT";
            this.CloseCountColumn.Location = new System.Drawing.Point(717, 0);
            this.CloseCountColumn.Name = "CloseCountColumn";
            this.CloseCountColumn.ReadOnly = true;
            this.CloseCountColumn.Size = new System.Drawing.Size(100, 21);
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.CloseCountColumn.Style = cellStyle4;
            this.CloseCountColumn.TabIndex = 3;
            // 
            // BaseInfoLinkColumn
            // 
            this.BaseInfoLinkColumn.Location = new System.Drawing.Point(817, 0);
            this.BaseInfoLinkColumn.Name = "BaseInfoLinkColumn";
            this.BaseInfoLinkColumn.Size = new System.Drawing.Size(101, 21);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.BaseInfoLinkColumn.Style = cellStyle5;
            this.BaseInfoLinkColumn.TabIndex = 6;
            this.BaseInfoLinkColumn.TrackVisitedState = false;
            this.BaseInfoLinkColumn.Value = "表示";
            // 
            // IdColumn
            // 
            this.IdColumn.DataField = "ID";
            this.IdColumn.Location = new System.Drawing.Point(1117, 0);
            this.IdColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.IdColumn.Name = "IdColumn";
            this.IdColumn.ReadOnly = true;
            this.IdColumn.Size = new System.Drawing.Size(30, 21);
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle6.Border = border1;
            cellStyle6.Padding = new System.Windows.Forms.Padding(0);
            this.IdColumn.Style = cellStyle6;
            this.IdColumn.TabIndex = 7;
            this.IdColumn.Visible = false;
            // 
            // NameColumn
            // 
            this.NameColumn.DataField = "名称";
            this.NameColumn.Location = new System.Drawing.Point(1147, 0);
            this.NameColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Size = new System.Drawing.Size(43, 21);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle7.Border = border2;
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            this.NameColumn.Style = cellStyle7;
            this.NameColumn.TabIndex = 8;
            this.NameColumn.Visible = false;
            // 
            // TimesColumn
            // 
            this.TimesColumn.DataField = "回";
            this.TimesColumn.Location = new System.Drawing.Point(1190, 0);
            this.TimesColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.TimesColumn.Name = "TimesColumn";
            this.TimesColumn.ReadOnly = true;
            this.TimesColumn.Size = new System.Drawing.Size(28, 21);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle8.Border = border3;
            cellStyle8.Padding = new System.Windows.Forms.Padding(0);
            this.TimesColumn.Style = cellStyle8;
            this.TimesColumn.TabIndex = 9;
            this.TimesColumn.Visible = false;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell7);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 949;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(120, 20);
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "開催日";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(120, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(497, 20);
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "設計チェック名";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(617, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(100, 20);
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "オープン件数";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.Location = new System.Drawing.Point(717, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(100, 20);
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "クローズ件数";
            // 
            // columnHeaderCell7
            // 
            this.columnHeaderCell7.Location = new System.Drawing.Point(817, 0);
            this.columnHeaderCell7.Name = "columnHeaderCell7";
            this.columnHeaderCell7.Size = new System.Drawing.Size(101, 20);
            this.columnHeaderCell7.TabIndex = 6;
            this.columnHeaderCell7.Value = "基本情報変更";
            // 
            // DesignCheckNameLinkColumn
            // 
            this.DesignCheckNameLinkColumn.DataField = "設計チェック名";
            this.DesignCheckNameLinkColumn.Location = new System.Drawing.Point(120, 0);
            this.DesignCheckNameLinkColumn.Name = "DesignCheckNameLinkColumn";
            this.DesignCheckNameLinkColumn.Size = new System.Drawing.Size(497, 21);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.DesignCheckNameLinkColumn.Style = cellStyle2;
            this.DesignCheckNameLinkColumn.TabIndex = 1;
            this.DesignCheckNameLinkColumn.Value = "設計チェック名";
            // 
            // OpenDateColumn
            // 
            this.OpenDateColumn.DataField = "開催日";
            this.OpenDateColumn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.OpenDateColumn.Location = new System.Drawing.Point(0, 0);
            this.OpenDateColumn.Name = "OpenDateColumn";
            this.OpenDateColumn.ReadOnly = true;
            this.OpenDateColumn.ShowDropDownButton = GrapeCity.Win.MultiRow.CellButtonVisibility.ShowForCurrentCell;
            this.OpenDateColumn.Size = new System.Drawing.Size(120, 21);
            cellStyle1.Format = "yyyy/MM/dd";
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.OpenDateColumn.Style = cellStyle1;
            this.OpenDateColumn.TabIndex = 0;
            // 
            // DesignCheckMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 949;

        }
        

        #endregion
        private GrapeCity.Win.MultiRow.TextBoxCell OpenCountColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell CloseCountColumn;
        private GrapeCity.Win.MultiRow.LinkLabelCell BaseInfoLinkColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell IdColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell NameColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell TimesColumn;
        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell7;
        private UC.MultiRow.NullableDateTimePickerCell OpenDateColumn;
        private GrapeCity.Win.MultiRow.LinkLabelCell DesignCheckNameLinkColumn;
    }
}
