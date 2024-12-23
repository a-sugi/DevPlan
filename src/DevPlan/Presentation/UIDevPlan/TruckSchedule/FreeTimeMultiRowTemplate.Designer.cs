namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class FreeTimeMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.FreeTimeTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.FreeTimeTimeTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.FreeTimeCheckBoxCell = new GrapeCity.Win.MultiRow.CheckBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.FreeTimeTextBoxCell);
            this.Row.Cells.Add(this.FreeTimeTimeTextBoxCell);
            this.Row.Cells.Add(this.FreeTimeCheckBoxCell);
            this.Row.Height = 21;
            this.Row.Width = 217;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Height = 27;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 217;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(84, 27);
            cellStyle3.BackColor = System.Drawing.Color.Blue;
            cellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ForeColor = System.Drawing.Color.White;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell1.Style = cellStyle3;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "日付";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(84, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(58, 27);
            cellStyle4.BackColor = System.Drawing.Color.Blue;
            cellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell2.Style = cellStyle4;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "時間";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.Location = new System.Drawing.Point(142, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(75, 27);
            cellStyle5.BackColor = System.Drawing.Color.Blue;
            cellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell3.Style = cellStyle5;
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "貸出可能";
            // 
            // FreeTimeTextBoxCell
            // 
            this.FreeTimeTextBoxCell.Location = new System.Drawing.Point(0, 0);
            this.FreeTimeTextBoxCell.Mergeable = true;
            this.FreeTimeTextBoxCell.Name = "FreeTimeTextBoxCell";
            this.FreeTimeTextBoxCell.ReadOnly = true;
            this.FreeTimeTextBoxCell.Selectable = false;
            this.FreeTimeTextBoxCell.Size = new System.Drawing.Size(84, 21);
            cellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.Padding = new System.Windows.Forms.Padding(2, 4, 3, 3);
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.FreeTimeTextBoxCell.Style = cellStyle1;
            this.FreeTimeTextBoxCell.TabIndex = 0;
            // 
            // FreeTimeTimeTextBoxCell
            // 
            this.FreeTimeTimeTextBoxCell.Location = new System.Drawing.Point(84, 0);
            this.FreeTimeTimeTextBoxCell.Name = "FreeTimeTimeTextBoxCell";
            this.FreeTimeTimeTextBoxCell.ReadOnly = true;
            this.FreeTimeTimeTextBoxCell.Selectable = false;
            this.FreeTimeTimeTextBoxCell.Size = new System.Drawing.Size(58, 21);
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FreeTimeTimeTextBoxCell.Style = cellStyle2;
            this.FreeTimeTimeTextBoxCell.TabIndex = 1;
            // 
            // FreeTimeCheckBoxCell
            // 
            this.FreeTimeCheckBoxCell.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FreeTimeCheckBoxCell.Location = new System.Drawing.Point(142, 0);
            this.FreeTimeCheckBoxCell.Name = "FreeTimeCheckBoxCell";
            this.FreeTimeCheckBoxCell.Size = new System.Drawing.Size(75, 21);
            this.FreeTimeCheckBoxCell.TabIndex = 3;
            // 
            // FreeTimeMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 48;
            this.Width = 217;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        internal GrapeCity.Win.MultiRow.CheckBoxCell FreeTimeCheckBoxCell;
        internal GrapeCity.Win.MultiRow.TextBoxCell FreeTimeTextBoxCell;
        internal GrapeCity.Win.MultiRow.TextBoxCell FreeTimeTimeTextBoxCell;
    }
}
