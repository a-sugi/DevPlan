namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class SectionMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.PointOfDepartureComboBoxCell = new GrapeCity.Win.MultiRow.ComboBoxCell();
            this.labelCell2 = new GrapeCity.Win.MultiRow.LabelCell();
            this.EmptyCheckBoxCell = new GrapeCity.Win.MultiRow.CheckBoxCell();
            this.DestinationComboBoxCell = new GrapeCity.Win.MultiRow.ComboBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.PointOfDepartureComboBoxCell);
            this.Row.Cells.Add(this.labelCell2);
            this.Row.Cells.Add(this.EmptyCheckBoxCell);
            this.Row.Cells.Add(this.DestinationComboBoxCell);
            this.Row.Height = 27;
            this.Row.Width = 307;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 307;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(122, 20);
            cellStyle4.BackColor = System.Drawing.Color.Blue;
            cellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell1.Style = cellStyle4;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "出発";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(148, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(122, 20);
            cellStyle5.BackColor = System.Drawing.Color.Blue;
            cellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell2.Style = cellStyle5;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "到着";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.Location = new System.Drawing.Point(270, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(37, 20);
            cellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.Red;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell3.Style = cellStyle6;
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "空荷";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.Location = new System.Drawing.Point(122, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(26, 20);
            cellStyle7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.columnHeaderCell4.Style = cellStyle7;
            this.columnHeaderCell4.TabIndex = 3;
            // 
            // PointOfDepartureComboBoxCell
            // 
            this.PointOfDepartureComboBoxCell.DropDownStyle = GrapeCity.Win.MultiRow.MultiRowComboBoxStyle.DropDown;
            this.PointOfDepartureComboBoxCell.Location = new System.Drawing.Point(0, 0);
            this.PointOfDepartureComboBoxCell.Name = "PointOfDepartureComboBoxCell";
            this.PointOfDepartureComboBoxCell.Size = new System.Drawing.Size(122, 27);
            cellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PointOfDepartureComboBoxCell.Style = cellStyle1;
            this.PointOfDepartureComboBoxCell.TabIndex = 0;
            this.PointOfDepartureComboBoxCell.Tag = "Required;Byte(20);ItemName(運行区間出発)";
            // 
            // labelCell2
            // 
            this.labelCell2.Location = new System.Drawing.Point(122, 0);
            this.labelCell2.Name = "labelCell2";
            this.labelCell2.Size = new System.Drawing.Size(26, 27);
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.labelCell2.Style = cellStyle2;
            this.labelCell2.TabIndex = 1;
            this.labelCell2.Value = "～";
            // 
            // EmptyCheckBoxCell
            // 
            this.EmptyCheckBoxCell.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.EmptyCheckBoxCell.Location = new System.Drawing.Point(270, 0);
            this.EmptyCheckBoxCell.Name = "EmptyCheckBoxCell";
            this.EmptyCheckBoxCell.Size = new System.Drawing.Size(37, 27);
            this.EmptyCheckBoxCell.TabIndex = 2;
            // 
            // DestinationComboBoxCell
            // 
            this.DestinationComboBoxCell.DropDownStyle = GrapeCity.Win.MultiRow.MultiRowComboBoxStyle.DropDown;
            this.DestinationComboBoxCell.Location = new System.Drawing.Point(148, 0);
            this.DestinationComboBoxCell.Name = "DestinationComboBoxCell";
            this.DestinationComboBoxCell.Size = new System.Drawing.Size(122, 27);
            cellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DestinationComboBoxCell.Style = cellStyle3;
            this.DestinationComboBoxCell.TabIndex = 1;
            this.DestinationComboBoxCell.Tag = "Required;Byte(20);ItemName(運行区間到着)";
            // 
            // SectionMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 47;
            this.Width = 307;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.LabelCell labelCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        internal GrapeCity.Win.MultiRow.ComboBoxCell PointOfDepartureComboBoxCell;
        internal GrapeCity.Win.MultiRow.ComboBoxCell DestinationComboBoxCell;
        internal GrapeCity.Win.MultiRow.CheckBoxCell EmptyCheckBoxCell;
    }
}
