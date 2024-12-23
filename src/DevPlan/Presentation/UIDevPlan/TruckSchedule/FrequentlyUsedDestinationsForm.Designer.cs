namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    partial class FrequentlyUsedDestinationsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FrequentlyUsedDestinationsMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.frequentlyUsedDestinationsMultiRowTemplate1 = new DevPlan.Presentation.UIDevPlan.TruckSchedule.FrequentlyUsedDestinationsMultiRowTemplate();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrequentlyUsedDestinationsMultiRow)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(169, 342);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.FrequentlyUsedDestinationsMultiRow);
            this.ListFormMainPanel.Size = new System.Drawing.Size(284, 332);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.FrequentlyUsedDestinationsMultiRow, 0);
            // 
            // FrequentlyUsedDestinationsMultiRow
            // 
            this.FrequentlyUsedDestinationsMultiRow.AllowAutoExtend = true;
            this.FrequentlyUsedDestinationsMultiRow.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.FrequentlyUsedDestinationsMultiRow.Location = new System.Drawing.Point(6, 39);
            this.FrequentlyUsedDestinationsMultiRow.MultiSelect = false;
            this.FrequentlyUsedDestinationsMultiRow.Name = "FrequentlyUsedDestinationsMultiRow";
            this.FrequentlyUsedDestinationsMultiRow.ReadOnly = true;
            this.FrequentlyUsedDestinationsMultiRow.Size = new System.Drawing.Size(272, 284);
            this.FrequentlyUsedDestinationsMultiRow.TabIndex = 1012;
            this.FrequentlyUsedDestinationsMultiRow.Template = this.frequentlyUsedDestinationsMultiRowTemplate1;
            this.FrequentlyUsedDestinationsMultiRow.CellContentClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.FrequentlyUsedDestinationsMultiRow_CellContentClick);
            // 
            // FrequentlyUsedDestinationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(295, 376);
            this.Name = "FrequentlyUsedDestinationsForm";
            this.Text = "FrequentlyUsedDestinationsForm";
            this.Load += new System.EventHandler(this.FrequentlyUsedDestinationsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrequentlyUsedDestinationsMultiRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow FrequentlyUsedDestinationsMultiRow;
        private FrequentlyUsedDestinationsMultiRowTemplate frequentlyUsedDestinationsMultiRowTemplate1;
    }
}