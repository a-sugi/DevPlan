namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class RegularMailMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.cornerHeaderCell1 = new GrapeCity.Win.MultiRow.CornerHeaderCell();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.MailCheckBoxCell = new GrapeCity.Win.MultiRow.CheckBoxCell();
            this.TransportVehicleNameTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.LoadingTimeTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.ShipperTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.RecipientTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.MailCheckBoxCell);
            this.Row.Cells.Add(this.TransportVehicleNameTextBoxCell);
            this.Row.Cells.Add(this.LoadingTimeTextBoxCell);
            this.Row.Cells.Add(this.ShipperTextBoxCell);
            this.Row.Cells.Add(this.RecipientTextBoxCell);
            this.Row.Height = 41;
            this.Row.Width = 893;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.cornerHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 893;
            // 
            // cornerHeaderCell1
            // 
            this.cornerHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.cornerHeaderCell1.Name = "cornerHeaderCell1";
            this.cornerHeaderCell1.Size = new System.Drawing.Size(29, 20);
            this.cornerHeaderCell1.TabIndex = 0;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(29, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(131, 20);
            cellStyle5.BackColor = System.Drawing.Color.Blue;
            cellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle5;
            this.columnHeaderCell1.TabIndex = 1;
            this.columnHeaderCell1.Value = "積込時間";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(160, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(223, 20);
            cellStyle6.BackColor = System.Drawing.Color.Blue;
            cellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell2.Style = cellStyle6;
            this.columnHeaderCell2.TabIndex = 2;
            this.columnHeaderCell2.Value = "搬送車両名";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.Location = new System.Drawing.Point(383, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(255, 20);
            cellStyle7.BackColor = System.Drawing.Color.Blue;
            cellStyle7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle7;
            this.columnHeaderCell3.TabIndex = 3;
            this.columnHeaderCell3.Value = "発送者";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.Location = new System.Drawing.Point(638, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(255, 20);
            cellStyle8.BackColor = System.Drawing.Color.Blue;
            cellStyle8.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell4.Style = cellStyle8;
            this.columnHeaderCell4.TabIndex = 4;
            this.columnHeaderCell4.Value = "受領者";
            // 
            // MailCheckBoxCell
            // 
            this.MailCheckBoxCell.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MailCheckBoxCell.Location = new System.Drawing.Point(0, 0);
            this.MailCheckBoxCell.Name = "MailCheckBoxCell";
            this.MailCheckBoxCell.Size = new System.Drawing.Size(29, 41);
            this.MailCheckBoxCell.TabIndex = 0;
            // 
            // TransportVehicleNameTextBoxCell
            // 
            this.TransportVehicleNameTextBoxCell.Location = new System.Drawing.Point(160, 0);
            this.TransportVehicleNameTextBoxCell.Name = "TransportVehicleNameTextBoxCell";
            this.TransportVehicleNameTextBoxCell.ReadOnly = true;
            this.TransportVehicleNameTextBoxCell.Size = new System.Drawing.Size(223, 41);
            cellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            cellStyle1.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.TransportVehicleNameTextBoxCell.Style = cellStyle1;
            this.TransportVehicleNameTextBoxCell.TabIndex = 2;
            // 
            // LoadingTimeTextBoxCell
            // 
            this.LoadingTimeTextBoxCell.Location = new System.Drawing.Point(29, 0);
            this.LoadingTimeTextBoxCell.Name = "LoadingTimeTextBoxCell";
            this.LoadingTimeTextBoxCell.ReadOnly = true;
            this.LoadingTimeTextBoxCell.Size = new System.Drawing.Size(131, 41);
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LoadingTimeTextBoxCell.Style = cellStyle2;
            this.LoadingTimeTextBoxCell.TabIndex = 1;
            // 
            // ShipperTextBoxCell
            // 
            this.ShipperTextBoxCell.Location = new System.Drawing.Point(383, 0);
            this.ShipperTextBoxCell.Name = "ShipperTextBoxCell";
            this.ShipperTextBoxCell.ReadOnly = true;
            this.ShipperTextBoxCell.Size = new System.Drawing.Size(255, 41);
            cellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.ShipperTextBoxCell.Style = cellStyle3;
            this.ShipperTextBoxCell.TabIndex = 3;
            // 
            // RecipientTextBoxCell
            // 
            this.RecipientTextBoxCell.Location = new System.Drawing.Point(638, 0);
            this.RecipientTextBoxCell.MinimumSize = new System.Drawing.Size(255, 41);
            this.RecipientTextBoxCell.Name = "RecipientTextBoxCell";
            this.RecipientTextBoxCell.ReadOnly = true;
            this.RecipientTextBoxCell.Size = new System.Drawing.Size(255, 41);
            cellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.RecipientTextBoxCell.Style = cellStyle4;
            this.RecipientTextBoxCell.TabIndex = 4;
            // 
            // RegularMailMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 61;
            this.Width = 893;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.CornerHeaderCell cornerHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        internal GrapeCity.Win.MultiRow.TextBoxCell TransportVehicleNameTextBoxCell;
        internal GrapeCity.Win.MultiRow.TextBoxCell LoadingTimeTextBoxCell;
        internal GrapeCity.Win.MultiRow.CheckBoxCell MailCheckBoxCell;
        internal GrapeCity.Win.MultiRow.TextBoxCell ShipperTextBoxCell;
        internal GrapeCity.Win.MultiRow.TextBoxCell RecipientTextBoxCell;
    }
}
