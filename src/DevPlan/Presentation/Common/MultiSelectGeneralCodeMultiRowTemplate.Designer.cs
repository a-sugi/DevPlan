namespace DevPlan.Presentation.Common
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class MultiSelectGeneralCodeMultiRowTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.CheckHeaderCell = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.CheckBoxColumn = new GrapeCity.Win.MultiRow.CheckBoxCell();
            this.CarGroupColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.GeneralCodeColumn = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.CheckBoxColumn);
            this.Row.Cells.Add(this.CarGroupColumn);
            this.Row.Cells.Add(this.GeneralCodeColumn);
            this.Row.Height = 21;
            this.Row.Width = 367;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.CheckHeaderCell);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 367;
            // 
            // CheckHeaderCell
            // 
            this.CheckHeaderCell.Location = new System.Drawing.Point(0, 0);
            this.CheckHeaderCell.Name = "CheckHeaderCell";
            this.CheckHeaderCell.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.None;
            this.CheckHeaderCell.Size = new System.Drawing.Size(38, 20);
            this.CheckHeaderCell.TabIndex = 0;
            this.CheckHeaderCell.Value = "";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.Location = new System.Drawing.Point(38, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(128, 20);
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "車系";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(166, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(170, 20);
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "開発符号";
            // 
            // CheckBoxColumn
            // 
            this.CheckBoxColumn.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxColumn.FalseValue = "false";
            this.CheckBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.CheckBoxColumn.Location = new System.Drawing.Point(0, 0);
            this.CheckBoxColumn.MinimumSize = new System.Drawing.Size(18, 0);
            this.CheckBoxColumn.Name = "CheckBoxColumn";
            this.CheckBoxColumn.Size = new System.Drawing.Size(38, 21);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle1.NullValue = false;
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.CheckBoxColumn.Style = cellStyle1;
            this.CheckBoxColumn.TabIndex = 0;
            this.CheckBoxColumn.TrueValue = "true";
            // 
            // CarGroupColumn
            // 
            this.CarGroupColumn.DataField = "CAR_GROUP";
            this.CarGroupColumn.Location = new System.Drawing.Point(38, 0);
            this.CarGroupColumn.MaxLength = 20;
            this.CarGroupColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.CarGroupColumn.Name = "CarGroupColumn";
            this.CarGroupColumn.ReadOnly = true;
            this.CarGroupColumn.Size = new System.Drawing.Size(128, 21);
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            this.CarGroupColumn.Style = cellStyle2;
            this.CarGroupColumn.TabIndex = 1;
            // 
            // GeneralCodeColumn
            // 
            this.GeneralCodeColumn.DataField = "GENERAL_CODE";
            this.GeneralCodeColumn.Location = new System.Drawing.Point(166, 0);
            this.GeneralCodeColumn.MaxLength = 20;
            this.GeneralCodeColumn.MinimumSize = new System.Drawing.Size(5, 0);
            this.GeneralCodeColumn.Name = "GeneralCodeColumn";
            this.GeneralCodeColumn.ReadOnly = true;
            this.GeneralCodeColumn.Size = new System.Drawing.Size(170, 21);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            this.GeneralCodeColumn.Style = cellStyle3;
            this.GeneralCodeColumn.TabIndex = 2;
            // 
            // MultiSelectGeneralCodeMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 367;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell CheckHeaderCell;
        private GrapeCity.Win.MultiRow.CheckBoxCell CheckBoxColumn;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.TextBoxCell CarGroupColumn;
        private GrapeCity.Win.MultiRow.TextBoxCell GeneralCodeColumn;
    }
}
