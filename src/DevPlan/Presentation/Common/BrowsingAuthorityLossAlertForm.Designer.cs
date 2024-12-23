namespace DevPlan.Presentation.Common
{
    partial class BrowsingAuthorityLossAlertForm
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
            this.MessageLabel = new System.Windows.Forms.Label();
            this.CautionLabel = new System.Windows.Forms.Label();
            this.NotMessageCheckBox = new System.Windows.Forms.CheckBox();
            this.SubTitleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListFormPictureBox
            // 
            this.ListFormPictureBox.Visible = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(254, 298);
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
            this.ListFormMainPanel.Controls.Add(this.CautionLabel);
            this.ListFormMainPanel.Controls.Add(this.MessageLabel);
            this.ListFormMainPanel.Location = new System.Drawing.Point(5, 43);
            this.ListFormMainPanel.Size = new System.Drawing.Size(369, 215);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.MessageLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.CautionLabel, 0);
            // 
            // MessageLabel
            // 
            this.MessageLabel.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.MessageLabel.Location = new System.Drawing.Point(6, 10);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(356, 165);
            this.MessageLabel.TabIndex = 1013;
            this.MessageLabel.Text = "{0}が解除されました。\r\n\r\n閲覧権限を再設定する必要がある場合は、所属長に依頼してください。\r\n\r\n● 権限が解除されるケースは以下の通りです。\r\n\r\n　- " +
    "所属が変更された（課内異動含む）\r\n　- 出向した\r\n　- 休職した";
            // 
            // CautionLabel
            // 
            this.CautionLabel.AutoSize = true;
            this.CautionLabel.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.CautionLabel.ForeColor = System.Drawing.Color.Red;
            this.CautionLabel.Location = new System.Drawing.Point(6, 188);
            this.CautionLabel.Name = "CautionLabel";
            this.CautionLabel.Size = new System.Drawing.Size(352, 14);
            this.CautionLabel.TabIndex = 1014;
            this.CautionLabel.Text = "このメッセージが不要の場合は、以下にチェックを入れてください。\r\n";
            // 
            // NotMessageCheckBox
            // 
            this.NotMessageCheckBox.AutoSize = true;
            this.NotMessageCheckBox.Location = new System.Drawing.Point(15, 264);
            this.NotMessageCheckBox.Name = "NotMessageCheckBox";
            this.NotMessageCheckBox.Size = new System.Drawing.Size(242, 19);
            this.NotMessageCheckBox.TabIndex = 10;
            this.NotMessageCheckBox.Text = "次回からこのメッセージを表示しない。";
            this.NotMessageCheckBox.UseVisualStyleBackColor = true;
            // 
            // SubTitleLabel
            // 
            this.SubTitleLabel.AutoSize = true;
            this.SubTitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold);
            this.SubTitleLabel.Location = new System.Drawing.Point(5, 9);
            this.SubTitleLabel.Name = "SubTitleLabel";
            this.SubTitleLabel.Size = new System.Drawing.Size(209, 22);
            this.SubTitleLabel.TabIndex = 1011;
            this.SubTitleLabel.Text = "【権限解除のご連絡】";
            // 
            // BrowsingAuthorityLossAlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(380, 332);
            this.Controls.Add(this.SubTitleLabel);
            this.Controls.Add(this.NotMessageCheckBox);
            this.Name = "BrowsingAuthorityLossAlertForm";
            this.Text = "GeneralCodeAuthorityLossAlertForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BrowsingAuthorityLossAlertForm_FormClosed);
            this.Load += new System.EventHandler(this.BrowsingAuthorityLossAlertForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.NotMessageCheckBox, 0);
            this.Controls.SetChildIndex(this.SubTitleLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Label CautionLabel;
        private System.Windows.Forms.CheckBox NotMessageCheckBox;
        private System.Windows.Forms.Label SubTitleLabel;
    }
}