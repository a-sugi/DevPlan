namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class RegularMailDetailMultiRowTemplate
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
            this.RegularMailTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.RegularMailTextBoxCell);
            this.Row.Height = 22;
            this.Row.Width = 135;
            // 
            // RegularMailTextBoxCell
            // 
            this.RegularMailTextBoxCell.HighlightText = true;
            this.RegularMailTextBoxCell.Location = new System.Drawing.Point(0, 0);
            this.RegularMailTextBoxCell.Name = "RegularMailTextBoxCell";
            this.RegularMailTextBoxCell.Size = new System.Drawing.Size(135, 22);
            cellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RegularMailTextBoxCell.Style = cellStyle1;
            this.RegularMailTextBoxCell.TabIndex = 0;
            // 
            // RegularMailDetailMultiRowTemplate
            // 
            this.Height = 22;
            this.Width = 135;

        }

        #endregion

        internal GrapeCity.Win.MultiRow.TextBoxCell RegularMailTextBoxCell;
    }
}
