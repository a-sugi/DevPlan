namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class FrequentlyUsedDestinationsMultiRowTemplate
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
            this.FrequentlyUsedTextBoxCell = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.FrequentlyUsedTextBoxCell);
            this.Row.Height = 21;
            this.Row.Width = 165;
            // 
            // FrequentlyUsedTextBoxCell
            // 
            this.FrequentlyUsedTextBoxCell.Location = new System.Drawing.Point(0, 0);
            this.FrequentlyUsedTextBoxCell.Name = "FrequentlyUsedTextBoxCell";
            this.FrequentlyUsedTextBoxCell.Size = new System.Drawing.Size(165, 21);
            this.FrequentlyUsedTextBoxCell.TabIndex = 0;
            // 
            // FrequentlyUsedDestinationsMultiRowTemplate
            // 
            this.Height = 21;
            this.Width = 165;

        }


        #endregion

        internal GrapeCity.Win.MultiRow.TextBoxCell FrequentlyUsedTextBoxCell;
    }
}
