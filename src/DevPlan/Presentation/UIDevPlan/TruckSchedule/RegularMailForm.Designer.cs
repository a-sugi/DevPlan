namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    partial class RegularMailForm
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
            this.LoadingDateLabel = new System.Windows.Forms.Label();
            this.RegularMailMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.regularMailMultiRowTemplate1 = new DevPlan.Presentation.UIDevPlan.TruckSchedule.RegularMailMultiRowTemplate();
            this.OpenMailButton = new System.Windows.Forms.Button();
            this.LoadingDateLabel2 = new System.Windows.Forms.Label();
            this.LoadingDateLabel3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegularMailMultiRow)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(844, 423);
            this.CloseButton.TabIndex = 2;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.LoadingDateLabel3);
            this.ListFormMainPanel.Controls.Add(this.LoadingDateLabel2);
            this.ListFormMainPanel.Controls.Add(this.LoadingDateLabel);
            this.ListFormMainPanel.Controls.Add(this.RegularMailMultiRow);
            this.ListFormMainPanel.Size = new System.Drawing.Size(959, 413);
            this.ListFormMainPanel.TabIndex = 0;
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RegularMailMultiRow, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.LoadingDateLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.LoadingDateLabel2, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.LoadingDateLabel3, 0);
            // 
            // LoadingDateLabel
            // 
            this.LoadingDateLabel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LoadingDateLabel.Location = new System.Drawing.Point(6, 47);
            this.LoadingDateLabel.Name = "LoadingDateLabel";
            this.LoadingDateLabel.Size = new System.Drawing.Size(113, 19);
            this.LoadingDateLabel.TabIndex = 1014;
            this.LoadingDateLabel.Text = "【積込日・・・";
            // 
            // RegularMailMultiRow
            // 
            this.RegularMailMultiRow.AllowAutoExtend = true;
            this.RegularMailMultiRow.AllowUserToDeleteRows = false;
            this.RegularMailMultiRow.AutoFitContent = GrapeCity.Win.MultiRow.AutoFitContent.All;
            this.RegularMailMultiRow.Location = new System.Drawing.Point(6, 78);
            this.RegularMailMultiRow.MultiSelect = false;
            this.RegularMailMultiRow.Name = "RegularMailMultiRow";
            this.RegularMailMultiRow.Size = new System.Drawing.Size(945, 327);
            this.RegularMailMultiRow.TabIndex = 0;
            this.RegularMailMultiRow.Template = this.regularMailMultiRowTemplate1;
            // 
            // OpenMailButton
            // 
            this.OpenMailButton.BackColor = System.Drawing.SystemColors.Control;
            this.OpenMailButton.Location = new System.Drawing.Point(5, 423);
            this.OpenMailButton.Name = "OpenMailButton";
            this.OpenMailButton.Size = new System.Drawing.Size(120, 30);
            this.OpenMailButton.TabIndex = 1;
            this.OpenMailButton.Text = "送信確認";
            this.OpenMailButton.UseVisualStyleBackColor = false;
            this.OpenMailButton.Click += new System.EventHandler(this.OpenMailButton_Click);
            // 
            // LoadingDateLabel2
            // 
            this.LoadingDateLabel2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LoadingDateLabel2.Location = new System.Drawing.Point(125, 47);
            this.LoadingDateLabel2.Name = "LoadingDateLabel2";
            this.LoadingDateLabel2.Size = new System.Drawing.Size(127, 19);
            this.LoadingDateLabel2.TabIndex = 1014;
            this.LoadingDateLabel2.Text = "yyyy/MM/dd";
            // 
            // LoadingDateLabel3
            // 
            this.LoadingDateLabel3.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LoadingDateLabel3.Location = new System.Drawing.Point(258, 47);
            this.LoadingDateLabel3.Name = "LoadingDateLabel3";
            this.LoadingDateLabel3.Size = new System.Drawing.Size(59, 19);
            this.LoadingDateLabel3.TabIndex = 1014;
            this.LoadingDateLabel3.Text = "】";
            // 
            // RegularMailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(970, 457);
            this.Controls.Add(this.OpenMailButton);
            this.Name = "RegularMailForm";
            this.Text = "RegularMailForm";
            this.Load += new System.EventHandler(this.RegularMailForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.OpenMailButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegularMailMultiRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LoadingDateLabel;
        private GrapeCity.Win.MultiRow.GcMultiRow RegularMailMultiRow;
        private System.Windows.Forms.Button OpenMailButton;
        private RegularMailMultiRowTemplate regularMailMultiRowTemplate1;
        private System.Windows.Forms.Label LoadingDateLabel3;
        private System.Windows.Forms.Label LoadingDateLabel2;
    }
}