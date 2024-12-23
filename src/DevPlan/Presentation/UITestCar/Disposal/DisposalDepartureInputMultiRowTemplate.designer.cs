namespace DevPlan.Presentation.UITestCar.Disposal
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class DisposalDepartureInputMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell5 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell6 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.管理票NO = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.車体番号 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.固定資産NO = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.廃却見積額 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.データID = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.履歴NO = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.変更 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.車両搬出日変更 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.廃却見積日 = new DevPlan.Presentation.UC.MultiRow.NullableDateTimePickerCell();
            this.車両搬出日 = new DevPlan.Presentation.UC.MultiRow.NullableDateTimePickerCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.管理票NO);
            this.Row.Cells.Add(this.車体番号);
            this.Row.Cells.Add(this.固定資産NO);
            this.Row.Cells.Add(this.廃却見積額);
            this.Row.Cells.Add(this.廃却見積日);
            this.Row.Cells.Add(this.車両搬出日);
            this.Row.Cells.Add(this.データID);
            this.Row.Cells.Add(this.履歴NO);
            this.Row.Cells.Add(this.変更);
            this.Row.Cells.Add(this.車両搬出日変更);
            cellStyle11.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle11;
            this.Row.Height = 21;
            this.Row.Width = 918;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell5);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell6);
            this.columnHeaderSection1.Height = 38;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 918;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell1.Size = new System.Drawing.Size(100, 38);
            this.columnHeaderCell1.TabIndex = 1;
            this.columnHeaderCell1.TabStop = false;
            this.columnHeaderCell1.Value = "管理票NO";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(100, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell2.Size = new System.Drawing.Size(180, 38);
            this.columnHeaderCell2.TabIndex = 2;
            this.columnHeaderCell2.TabStop = false;
            this.columnHeaderCell2.Value = "車体番号";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(280, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell3.Size = new System.Drawing.Size(110, 38);
            this.columnHeaderCell3.TabIndex = 3;
            this.columnHeaderCell3.TabStop = false;
            this.columnHeaderCell3.Value = "固定資産NO";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.Location = new System.Drawing.Point(390, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell4.Size = new System.Drawing.Size(110, 38);
            cellStyle12.BackColor = System.Drawing.Color.LightBlue;
            cellStyle12.ForeColor = System.Drawing.Color.Black;
            this.columnHeaderCell4.Style = cellStyle12;
            this.columnHeaderCell4.TabIndex = 4;
            this.columnHeaderCell4.TabStop = false;
            this.columnHeaderCell4.Value = "廃却見積額";
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell5.Location = new System.Drawing.Point(500, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell5.Size = new System.Drawing.Size(120, 38);
            cellStyle13.BackColor = System.Drawing.Color.LightBlue;
            cellStyle13.ForeColor = System.Drawing.Color.Black;
            this.columnHeaderCell5.Style = cellStyle13;
            this.columnHeaderCell5.TabIndex = 5;
            this.columnHeaderCell5.TabStop = false;
            this.columnHeaderCell5.Value = "廃却見積日";
            // 
            // columnHeaderCell6
            // 
            this.columnHeaderCell6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell6.Location = new System.Drawing.Point(620, 0);
            this.columnHeaderCell6.Name = "columnHeaderCell6";
            this.columnHeaderCell6.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell6.Size = new System.Drawing.Size(120, 38);
            cellStyle14.BackColor = System.Drawing.Color.LightBlue;
            cellStyle14.ForeColor = System.Drawing.Color.Black;
            this.columnHeaderCell6.Style = cellStyle14;
            this.columnHeaderCell6.TabIndex = 6;
            this.columnHeaderCell6.TabStop = false;
            this.columnHeaderCell6.Value = "車両搬出日";
            // 
            // 管理票NO
            // 
            this.管理票NO.DataField = "管理票NO";
            this.管理票NO.Location = new System.Drawing.Point(0, 0);
            this.管理票NO.Name = "管理票NO";
            this.管理票NO.ReadOnly = true;
            this.管理票NO.Size = new System.Drawing.Size(100, 21);
            cellStyle1.Format = "";
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            this.管理票NO.Style = cellStyle1;
            this.管理票NO.TabIndex = 1;
            // 
            // 車体番号
            // 
            this.車体番号.DataField = "車体番号";
            this.車体番号.Location = new System.Drawing.Point(100, 0);
            this.車体番号.Name = "車体番号";
            this.車体番号.ReadOnly = true;
            this.車体番号.Size = new System.Drawing.Size(180, 21);
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            this.車体番号.Style = cellStyle2;
            this.車体番号.TabIndex = 2;
            // 
            // 固定資産NO
            // 
            this.固定資産NO.DataField = "固定資産NO";
            this.固定資産NO.Location = new System.Drawing.Point(280, 0);
            this.固定資産NO.Name = "固定資産NO";
            this.固定資産NO.ReadOnly = true;
            this.固定資産NO.Size = new System.Drawing.Size(110, 21);
            cellStyle3.Format = "";
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            this.固定資産NO.Style = cellStyle3;
            this.固定資産NO.TabIndex = 3;
            // 
            // 廃却見積額
            // 
            this.廃却見積額.DataField = "廃却見積額";
            this.廃却見積額.Location = new System.Drawing.Point(390, 0);
            this.廃却見積額.MaxLength = 8;
            this.廃却見積額.Name = "廃却見積額";
            this.廃却見積額.Size = new System.Drawing.Size(110, 21);
            cellStyle4.Format = "C";
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            this.廃却見積額.Style = cellStyle4;
            this.廃却見積額.TabIndex = 4;
            // 
            // データID
            // 
            this.データID.DataField = "データID";
            this.データID.Location = new System.Drawing.Point(946, 0);
            this.データID.Name = "データID";
            this.データID.ReadOnly = true;
            this.データID.Size = new System.Drawing.Size(100, 21);
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle7.Border = border1;
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            this.データID.Style = cellStyle7;
            this.データID.TabIndex = 7;
            this.データID.TabStop = false;
            // 
            // 履歴NO
            // 
            this.履歴NO.DataField = "履歴NO";
            this.履歴NO.Location = new System.Drawing.Point(1046, 0);
            this.履歴NO.Name = "履歴NO";
            this.履歴NO.ReadOnly = true;
            this.履歴NO.Size = new System.Drawing.Size(100, 21);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle8.Border = border2;
            cellStyle8.Padding = new System.Windows.Forms.Padding(0);
            this.履歴NO.Style = cellStyle8;
            this.履歴NO.TabIndex = 8;
            this.履歴NO.TabStop = false;
            // 
            // 変更
            // 
            this.変更.DataField = "変更";
            this.変更.Location = new System.Drawing.Point(1146, 0);
            this.変更.Name = "変更";
            this.変更.ReadOnly = true;
            this.変更.Size = new System.Drawing.Size(100, 21);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle9.Border = border3;
            cellStyle9.Padding = new System.Windows.Forms.Padding(0);
            this.変更.Style = cellStyle9;
            this.変更.TabIndex = 9;
            this.変更.TabStop = false;
            // 
            // 車両搬出日変更
            // 
            this.車両搬出日変更.DataField = "車両搬出日変更";
            this.車両搬出日変更.Location = new System.Drawing.Point(1246, 0);
            this.車両搬出日変更.Name = "車両搬出日変更";
            this.車両搬出日変更.ReadOnly = true;
            this.車両搬出日変更.Size = new System.Drawing.Size(100, 21);
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.ControlDark);
            cellStyle10.Border = border4;
            cellStyle10.Padding = new System.Windows.Forms.Padding(0);
            this.車両搬出日変更.Style = cellStyle10;
            this.車両搬出日変更.TabIndex = 10;
            this.車両搬出日変更.TabStop = false;
            // 
            // 廃却見積日
            // 
            this.廃却見積日.DataField = "廃却見積日";
            this.廃却見積日.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.廃却見積日.Location = new System.Drawing.Point(500, 0);
            this.廃却見積日.Name = "廃却見積日";
            this.廃却見積日.ShowDropDownButton = GrapeCity.Win.MultiRow.CellButtonVisibility.ShowForCurrentCell;
            this.廃却見積日.Size = new System.Drawing.Size(120, 21);
            cellStyle5.Format = "yyyy/MM/dd";
            this.廃却見積日.Style = cellStyle5;
            this.廃却見積日.TabIndex = 5;
            // 
            // 車両搬出日
            // 
            this.車両搬出日.DataField = "車両搬出日";
            this.車両搬出日.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.車両搬出日.Location = new System.Drawing.Point(620, 0);
            this.車両搬出日.Name = "車両搬出日";
            this.車両搬出日.ShowDropDownButton = GrapeCity.Win.MultiRow.CellButtonVisibility.ShowForCurrentCell;
            this.車両搬出日.Size = new System.Drawing.Size(120, 21);
            cellStyle6.Format = "yyyy/MM/dd";
            this.車両搬出日.Style = cellStyle6;
            this.車両搬出日.TabIndex = 6;
            // 
            // DisposalDepartureInputMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 59;
            this.Width = 918;

        }


        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell5;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell6;
        private GrapeCity.Win.MultiRow.TextBoxCell 管理票NO;
        private GrapeCity.Win.MultiRow.TextBoxCell 車体番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 固定資産NO;
        private GrapeCity.Win.MultiRow.TextBoxCell 廃却見積額;
        private UC.MultiRow.NullableDateTimePickerCell 廃却見積日;
        private UC.MultiRow.NullableDateTimePickerCell 車両搬出日;
        private GrapeCity.Win.MultiRow.TextBoxCell データID;
        private GrapeCity.Win.MultiRow.TextBoxCell 履歴NO;
        private GrapeCity.Win.MultiRow.TextBoxCell 変更;
        private GrapeCity.Win.MultiRow.TextBoxCell 車両搬出日変更;
    }
}
