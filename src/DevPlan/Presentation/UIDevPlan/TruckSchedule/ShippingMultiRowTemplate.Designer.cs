namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ShippingMultiRowTemplate
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
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.ShippingTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.ShippingTelTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.ShippingButtonCell = new GrapeCity.Win.MultiRow.ButtonCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.ShippingTextBoxCell);
            this.Row.Cells.Add(this.ShippingTelTextBoxCell);
            this.Row.Cells.Add(this.ShippingButtonCell);
            this.Row.Height = 21;
            this.Row.Width = 397;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Height = 18;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 397;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(226, 18);
            cellStyle4.BackColor = System.Drawing.Color.Blue;
            cellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle4;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "発送者";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(226, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(171, 18);
            cellStyle5.BackColor = System.Drawing.Color.Blue;
            cellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell2.Style = cellStyle5;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "電話番号";
            // 
            // ShippingTextBoxCell
            // 
            this.ShippingTextBoxCell.Location = new System.Drawing.Point(0, 0);
            this.ShippingTextBoxCell.Name = "ShippingTextBoxCell";
            this.ShippingTextBoxCell.Size = new System.Drawing.Size(202, 21);
            cellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ShippingTextBoxCell.Style = cellStyle1;
            this.ShippingTextBoxCell.TabIndex = 2;
            this.ShippingTextBoxCell.Tag = "Required;Byte(50);ItemName(発送者)";
            // 
            // ShippingTelTextBoxCell
            // 
            this.ShippingTelTextBoxCell.Location = new System.Drawing.Point(226, 0);
            this.ShippingTelTextBoxCell.Name = "ShippingTelTextBoxCell";
            this.ShippingTelTextBoxCell.Size = new System.Drawing.Size(171, 21);
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ShippingTelTextBoxCell.Style = cellStyle2;
            this.ShippingTelTextBoxCell.TabIndex = 3;
            this.ShippingTelTextBoxCell.Tag = "Required;Byte(30);ItemName(発送者TEL)";
            // 
            // ShippingButtonCell
            // 
            this.ShippingButtonCell.Location = new System.Drawing.Point(202, 0);
            this.ShippingButtonCell.Name = "ShippingButtonCell";
            this.ShippingButtonCell.Size = new System.Drawing.Size(24, 21);
            cellStyle3.BackColor = System.Drawing.SystemColors.Control;
            cellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.ShippingButtonCell.Style = cellStyle3;
            this.ShippingButtonCell.TabIndex = 4;
            this.ShippingButtonCell.Value = "▼";
            // 
            // ShippingMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 39;
            this.Width = 397;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        internal GrapeCity.Win.MultiRow.TextBoxCell ShippingTextBoxCell;
        internal GrapeCity.Win.MultiRow.TextBoxCell ShippingTelTextBoxCell;
        internal GrapeCity.Win.MultiRow.ButtonCell ShippingButtonCell;
    }
}
