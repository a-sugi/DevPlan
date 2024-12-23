namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ReceiptUserMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.RecipientNameTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.RecipientTelTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.RecipientButtonCell = new GrapeCity.Win.MultiRow.ButtonCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.RecipientNameTextBoxCell);
            this.Row.Cells.Add(this.RecipientTelTextBoxCell);
            this.Row.Cells.Add(this.RecipientButtonCell);
            this.Row.Height = 21;
            this.Row.Width = 397;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Height = 18;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 397;
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(225, 18);
            cellStyle4.BackColor = System.Drawing.Color.Blue;
            cellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle4;
            this.columnHeaderCell3.TabIndex = 0;
            this.columnHeaderCell3.Value = "受領者";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.Location = new System.Drawing.Point(225, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(172, 18);
            cellStyle5.BackColor = System.Drawing.Color.Blue;
            cellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell4.Style = cellStyle5;
            this.columnHeaderCell4.TabIndex = 1;
            this.columnHeaderCell4.Value = "電話番号";
            // 
            // RecipientNameTextBoxCell
            // 
            this.RecipientNameTextBoxCell.Location = new System.Drawing.Point(0, 0);
            this.RecipientNameTextBoxCell.Name = "RecipientNameTextBoxCell";
            this.RecipientNameTextBoxCell.Size = new System.Drawing.Size(201, 21);
            cellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RecipientNameTextBoxCell.Style = cellStyle1;
            this.RecipientNameTextBoxCell.TabIndex = 2;
            this.RecipientNameTextBoxCell.Tag = "Required;Byte(50);ItemName(受領者)";
            // 
            // RecipientTelTextBoxCell
            // 
            this.RecipientTelTextBoxCell.Location = new System.Drawing.Point(226, 0);
            this.RecipientTelTextBoxCell.Name = "RecipientTelTextBoxCell";
            this.RecipientTelTextBoxCell.Size = new System.Drawing.Size(171, 21);
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RecipientTelTextBoxCell.Style = cellStyle2;
            this.RecipientTelTextBoxCell.TabIndex = 3;
            this.RecipientTelTextBoxCell.Tag = "Required;Byte(30);ItemName(受領者TEL)";
            // 
            // RecipientButtonCell
            // 
            this.RecipientButtonCell.Location = new System.Drawing.Point(201, 0);
            this.RecipientButtonCell.Name = "RecipientButtonCell";
            this.RecipientButtonCell.Size = new System.Drawing.Size(24, 21);
            cellStyle3.BackColor = System.Drawing.SystemColors.Control;
            cellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.RecipientButtonCell.Style = cellStyle3;
            this.RecipientButtonCell.TabIndex = 4;
            this.RecipientButtonCell.Value = "▼";
            // 
            // ReceiptUserMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 39;
            this.Width = 397;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        internal GrapeCity.Win.MultiRow.TextBoxCell RecipientNameTextBoxCell;
        internal GrapeCity.Win.MultiRow.TextBoxCell RecipientTelTextBoxCell;
        internal GrapeCity.Win.MultiRow.ButtonCell RecipientButtonCell;
    }
}
