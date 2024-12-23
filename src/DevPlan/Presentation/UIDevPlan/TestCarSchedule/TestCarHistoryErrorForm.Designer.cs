namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    partial class TestCarHistoryErrorForm
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
            this.ErrorMessageLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HistoryErrorListMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.historyErrorListMultiRowTemplate1 = new DevPlan.Presentation.UIDevPlan.TestCarSchedule.HistoryErrorListMultiRowTemplate();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryErrorListMultiRow)).BeginInit();
            this.SuspendLayout();
            // 
            // ListFormPictureBox
            // 
            this.ListFormPictureBox.Visible = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(733, 420);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            this.ListFormTitleLabel.Visible = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.DescriptionLabel);
            this.ListFormMainPanel.Controls.Add(this.HistoryErrorListMultiRow);
            this.ListFormMainPanel.Controls.Add(this.panel1);
            this.ListFormMainPanel.Size = new System.Drawing.Size(848, 410);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.panel1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.HistoryErrorListMultiRow, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DescriptionLabel, 0);
            // 
            // ErrorMessageLabel
            // 
            this.ErrorMessageLabel.AutoSize = true;
            this.ErrorMessageLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ErrorMessageLabel.Location = new System.Drawing.Point(6, 10);
            this.ErrorMessageLabel.Name = "ErrorMessageLabel";
            this.ErrorMessageLabel.Size = new System.Drawing.Size(625, 60);
            this.ErrorMessageLabel.TabIndex = 1011;
            this.ErrorMessageLabel.Text = "下記リストは、{0}さんが予約者となっていて、使用履歴・作業完了の記録が無い予約の一覧です。\r\n予約期間を過ぎていますので、速やかに入力下さい。\r\n\r\n尚､予約し" +
    "た覚えがないものがありましたら､{1}までご連絡下さい｡";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ErrorMessageLabel);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(835, 81);
            this.panel1.TabIndex = 1012;
            // 
            // HistoryErrorListMultiRow
            // 
            this.HistoryErrorListMultiRow.AllowAutoExtend = true;
            this.HistoryErrorListMultiRow.AllowClipboard = false;
            this.HistoryErrorListMultiRow.AllowUserToAddRows = false;
            this.HistoryErrorListMultiRow.AllowUserToDeleteRows = false;
            this.HistoryErrorListMultiRow.ClipboardCopyMode = GrapeCity.Win.MultiRow.ClipboardCopyMode.Disable;
            this.HistoryErrorListMultiRow.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.HistoryErrorListMultiRow.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.HistoryErrorListMultiRow.Location = new System.Drawing.Point(3, 108);
            this.HistoryErrorListMultiRow.MultiSelect = false;
            this.HistoryErrorListMultiRow.Name = "HistoryErrorListMultiRow";
            this.HistoryErrorListMultiRow.ReadOnly = true;
            this.HistoryErrorListMultiRow.Size = new System.Drawing.Size(838, 297);
            this.HistoryErrorListMultiRow.TabIndex = 1013;
            this.HistoryErrorListMultiRow.Template = this.historyErrorListMultiRowTemplate1;
            this.HistoryErrorListMultiRow.Text = "gcMultiRow1";
            this.HistoryErrorListMultiRow.CellDoubleClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.HistoryErrorListMultiRow_CellDoubleClick);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.ForeColor = System.Drawing.Color.Red;
            this.DescriptionLabel.Location = new System.Drawing.Point(6, 90);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(289, 15);
            this.DescriptionLabel.TabIndex = 1029;
            this.DescriptionLabel.Text = "ダブルクリックで対象のスケジュールに遷移します。";
            // 
            // TestCarHistoryErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(859, 454);
            this.Name = "TestCarHistoryErrorForm";
            this.Text = "TestCarHistoryErrorForm";
            this.Load += new System.EventHandler(this.TestCarHistoryErrorForm_Load);
            this.Shown += new System.EventHandler(this.TestCarHistoryErrorForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryErrorListMultiRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow HistoryErrorListMultiRow;
        private HistoryErrorListMultiRowTemplate historyErrorListMultiRowTemplate1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ErrorMessageLabel;
        private System.Windows.Forms.Label DescriptionLabel;
    }
}