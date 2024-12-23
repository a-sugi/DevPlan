namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    partial class CalendarPrintExcelForm
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
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExcelOutputButton
            // 
            this.ExcelOutputButton.Click += new System.EventHandler(this.ExcelOutputButton_Click);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(98, 19);
            this.ListFormTitleLabel.Text = "Excel出力";
            // 
            // CalendarPrintExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(461, 123);
            this.Name = "CalendarPrintExcelForm";
            this.Text = "CalendarPrintExcelForm";
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}