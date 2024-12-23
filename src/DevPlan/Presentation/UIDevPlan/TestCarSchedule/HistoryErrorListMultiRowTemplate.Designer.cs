namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class HistoryErrorListMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell20 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell5 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell6 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.CarNameTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.StartEndDateTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.DescriptionTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.rowHeaderCell1 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.GeneralCodeTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.CarNameTextBoxCell);
            this.Row.Cells.Add(this.StartEndDateTextBoxCell);
            this.Row.Cells.Add(this.DescriptionTextBoxCell);
            this.Row.Cells.Add(this.rowHeaderCell1);
            this.Row.Cells.Add(this.GeneralCodeTextBoxCell);
            this.Row.Height = 21;
            this.Row.Width = 783;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell20);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell5);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell6);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Height = 27;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 783;
            // 
            // columnHeaderCell20
            // 
            this.columnHeaderCell20.Location = new System.Drawing.Point(760, 21);
            this.columnHeaderCell20.Name = "columnHeaderCell20";
            this.columnHeaderCell20.Size = new System.Drawing.Size(80, 21);
            this.columnHeaderCell20.TabIndex = 19;
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.Location = new System.Drawing.Point(107, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(179, 27);
            cellStyle6.BackColor = System.Drawing.Color.Blue;
            cellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell4.Style = cellStyle6;
            this.columnHeaderCell4.TabIndex = 21;
            this.columnHeaderCell4.Value = "車両名";
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell5.Location = new System.Drawing.Point(286, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.Size = new System.Drawing.Size(215, 27);
            cellStyle7.BackColor = System.Drawing.Color.Blue;
            cellStyle7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell5.Style = cellStyle7;
            this.columnHeaderCell5.TabIndex = 22;
            this.columnHeaderCell5.Value = "予約期間";
            // 
            // columnHeaderCell6
            // 
            this.columnHeaderCell6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell6.Location = new System.Drawing.Point(501, 0);
            this.columnHeaderCell6.Name = "columnHeaderCell6";
            this.columnHeaderCell6.Size = new System.Drawing.Size(282, 27);
            cellStyle8.BackColor = System.Drawing.Color.Blue;
            cellStyle8.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell6.Style = cellStyle8;
            this.columnHeaderCell6.TabIndex = 23;
            this.columnHeaderCell6.Value = "予約コメント";
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(34, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(73, 27);
            cellStyle9.BackColor = System.Drawing.Color.Blue;
            cellStyle9.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell1.Style = cellStyle9;
            this.columnHeaderCell1.TabIndex = 24;
            this.columnHeaderCell1.Value = "開発符号";
            // 
            // CarNameTextBoxCell
            // 
            this.CarNameTextBoxCell.HighlightText = true;
            this.CarNameTextBoxCell.Location = new System.Drawing.Point(107, 0);
            this.CarNameTextBoxCell.Name = "CarNameTextBoxCell";
            this.CarNameTextBoxCell.Size = new System.Drawing.Size(179, 21);
            cellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.CarNameTextBoxCell.Style = cellStyle1;
            this.CarNameTextBoxCell.TabIndex = 1;
            // 
            // StartEndDateTextBoxCell
            // 
            this.StartEndDateTextBoxCell.HighlightText = true;
            this.StartEndDateTextBoxCell.Location = new System.Drawing.Point(286, 0);
            this.StartEndDateTextBoxCell.Name = "StartEndDateTextBoxCell";
            this.StartEndDateTextBoxCell.Size = new System.Drawing.Size(215, 21);
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StartEndDateTextBoxCell.Style = cellStyle2;
            this.StartEndDateTextBoxCell.TabIndex = 2;
            // 
            // DescriptionTextBoxCell
            // 
            this.DescriptionTextBoxCell.HighlightText = true;
            this.DescriptionTextBoxCell.Location = new System.Drawing.Point(501, 0);
            this.DescriptionTextBoxCell.Name = "DescriptionTextBoxCell";
            this.DescriptionTextBoxCell.Size = new System.Drawing.Size(282, 21);
            cellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DescriptionTextBoxCell.Style = cellStyle3;
            this.DescriptionTextBoxCell.TabIndex = 3;
            // 
            // rowHeaderCell1
            // 
            this.rowHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.rowHeaderCell1.Name = "rowHeaderCell1";
            this.rowHeaderCell1.ShowIndicator = false;
            this.rowHeaderCell1.Size = new System.Drawing.Size(34, 21);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            cellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.rowHeaderCell1.Style = cellStyle4;
            this.rowHeaderCell1.TabIndex = 3;
            this.rowHeaderCell1.ValueFormat = "%1%";
            // 
            // GeneralCodeTextBoxCell
            // 
            this.GeneralCodeTextBoxCell.HighlightText = true;
            this.GeneralCodeTextBoxCell.Location = new System.Drawing.Point(34, 0);
            this.GeneralCodeTextBoxCell.Name = "GeneralCodeTextBoxCell";
            this.GeneralCodeTextBoxCell.Size = new System.Drawing.Size(73, 21);
            cellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.GeneralCodeTextBoxCell.Style = cellStyle5;
            this.GeneralCodeTextBoxCell.TabIndex = 0;
            // 
            // HistoryErrorListMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 48;
            this.Width = 783;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell20;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell5;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell6;
        internal GrapeCity.Win.MultiRow.TextBoxCell CarNameTextBoxCell;
        internal GrapeCity.Win.MultiRow.TextBoxCell StartEndDateTextBoxCell;
        internal GrapeCity.Win.MultiRow.TextBoxCell DescriptionTextBoxCell;
        private GrapeCity.Win.MultiRow.RowHeaderCell rowHeaderCell1;
        internal GrapeCity.Win.MultiRow.TextBoxCell GeneralCodeTextBoxCell;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
    }
}
