namespace DevPlan.Presentation.Common
{
    partial class PasswordChangeForm
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
            this.EntryButton = new System.Windows.Forms.Button();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTitleLabel = new System.Windows.Forms.Label();
            this.NewPasswordTextBox = new System.Windows.Forms.TextBox();
            this.NewPasswordTitleLabel = new System.Windows.Forms.Label();
            this.NewPasswordConfTextBox = new System.Windows.Forms.TextBox();
            this.NewPasswordConfTitleLabel = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.SupplementPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.SupplementPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListFormPictureBox
            // 
            this.ListFormPictureBox.Visible = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(258, 198);
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
            this.ListFormMainPanel.Controls.Add(this.NewPasswordConfTextBox);
            this.ListFormMainPanel.Controls.Add(this.NewPasswordConfTitleLabel);
            this.ListFormMainPanel.Controls.Add(this.NewPasswordTextBox);
            this.ListFormMainPanel.Controls.Add(this.NewPasswordTitleLabel);
            this.ListFormMainPanel.Controls.Add(this.PasswordTextBox);
            this.ListFormMainPanel.Controls.Add(this.PasswordTitleLabel);
            this.ListFormMainPanel.Location = new System.Drawing.Point(5, 66);
            this.ListFormMainPanel.Size = new System.Drawing.Size(373, 126);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.PasswordTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.PasswordTextBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.NewPasswordTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.NewPasswordTextBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.NewPasswordConfTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.NewPasswordConfTextBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DescriptionLabel, 0);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 198);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 1001;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PasswordTextBox.Location = new System.Drawing.Point(157, 44);
            this.PasswordTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PasswordTextBox.MaxLength = 128;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(210, 22);
            this.PasswordTextBox.TabIndex = 1;
            this.PasswordTextBox.Tag = "Require;ItemName(古いパスワード);Byte(13)";
            // 
            // PasswordTitleLabel
            // 
            this.PasswordTitleLabel.AutoSize = true;
            this.PasswordTitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PasswordTitleLabel.Location = new System.Drawing.Point(5, 47);
            this.PasswordTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PasswordTitleLabel.Name = "PasswordTitleLabel";
            this.PasswordTitleLabel.Size = new System.Drawing.Size(91, 15);
            this.PasswordTitleLabel.TabIndex = 1018;
            this.PasswordTitleLabel.Text = "古いパスワード";
            // 
            // NewPasswordTextBox
            // 
            this.NewPasswordTextBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NewPasswordTextBox.Location = new System.Drawing.Point(157, 70);
            this.NewPasswordTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.NewPasswordTextBox.MaxLength = 20;
            this.NewPasswordTextBox.Name = "NewPasswordTextBox";
            this.NewPasswordTextBox.PasswordChar = '*';
            this.NewPasswordTextBox.Size = new System.Drawing.Size(210, 22);
            this.NewPasswordTextBox.TabIndex = 2;
            this.NewPasswordTextBox.Tag = "Require;ItemName(新しいパスワード);Byte(13)";
            // 
            // NewPasswordTitleLabel
            // 
            this.NewPasswordTitleLabel.AutoSize = true;
            this.NewPasswordTitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NewPasswordTitleLabel.Location = new System.Drawing.Point(5, 73);
            this.NewPasswordTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NewPasswordTitleLabel.Name = "NewPasswordTitleLabel";
            this.NewPasswordTitleLabel.Size = new System.Drawing.Size(102, 15);
            this.NewPasswordTitleLabel.TabIndex = 1020;
            this.NewPasswordTitleLabel.Text = "新しいパスワード";
            // 
            // NewPasswordConfTextBox
            // 
            this.NewPasswordConfTextBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NewPasswordConfTextBox.Location = new System.Drawing.Point(157, 96);
            this.NewPasswordConfTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.NewPasswordConfTextBox.MaxLength = 20;
            this.NewPasswordConfTextBox.Name = "NewPasswordConfTextBox";
            this.NewPasswordConfTextBox.PasswordChar = '*';
            this.NewPasswordConfTextBox.Size = new System.Drawing.Size(210, 22);
            this.NewPasswordConfTextBox.TabIndex = 3;
            this.NewPasswordConfTextBox.Tag = "Require;ItemName(新しいパスワード(確認));Byte(13)";
            // 
            // NewPasswordConfTitleLabel
            // 
            this.NewPasswordConfTitleLabel.AutoSize = true;
            this.NewPasswordConfTitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NewPasswordConfTitleLabel.Location = new System.Drawing.Point(5, 99);
            this.NewPasswordConfTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NewPasswordConfTitleLabel.Name = "NewPasswordConfTitleLabel";
            this.NewPasswordConfTitleLabel.Size = new System.Drawing.Size(142, 15);
            this.NewPasswordConfTitleLabel.TabIndex = 1022;
            this.NewPasswordConfTitleLabel.Text = "新しいパスワード(確認)";
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DescriptionLabel.Location = new System.Drawing.Point(16, 13);
            this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(289, 15);
            this.DescriptionLabel.TabIndex = 1023;
            this.DescriptionLabel.Text = "パスワードを変更して下さい。 (半角8～13文字)";
            // 
            // SupplementPanel
            // 
            this.SupplementPanel.BackColor = System.Drawing.Color.SkyBlue;
            this.SupplementPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SupplementPanel.Controls.Add(this.label6);
            this.SupplementPanel.Controls.Add(this.label3);
            this.SupplementPanel.Controls.Add(this.label1);
            this.SupplementPanel.ForeColor = System.Drawing.Color.White;
            this.SupplementPanel.Location = new System.Drawing.Point(5, 4);
            this.SupplementPanel.Name = "SupplementPanel";
            this.SupplementPanel.Size = new System.Drawing.Size(373, 56);
            this.SupplementPanel.TabIndex = 1013;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(249, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 15);
            this.label6.TabIndex = 1029;
            this.label6.Text = "・操安DB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(67, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 15);
            this.label3.TabIndex = 1026;
            this.label3.Text = "・設備管理マネージャー";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 15);
            this.label1.TabIndex = 1024;
            this.label1.Text = "下記ソフトのパスワードも同時に変更されます。";
            // 
            // PasswordChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(384, 232);
            this.Controls.Add(this.SupplementPanel);
            this.Controls.Add(this.EntryButton);
            this.Name = "PasswordChangeForm";
            this.Text = "FavoriteEntryForm";
            this.Load += new System.EventHandler(this.PasswordChangeForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.SupplementPanel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.SupplementPanel.ResumeLayout(false);
            this.SupplementPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label PasswordTitleLabel;
        private System.Windows.Forms.TextBox NewPasswordConfTextBox;
        private System.Windows.Forms.Label NewPasswordConfTitleLabel;
        private System.Windows.Forms.TextBox NewPasswordTextBox;
        private System.Windows.Forms.Label NewPasswordTitleLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Panel SupplementPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
    }
}