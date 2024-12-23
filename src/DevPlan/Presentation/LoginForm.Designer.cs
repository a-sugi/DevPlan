namespace DevPlan.Presentation
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.AutoLoginCheckBox = new System.Windows.Forms.CheckBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTitleLabel = new System.Windows.Forms.Label();
            this.UserIDTextBox = new System.Windows.Forms.TextBox();
            this.UserIDTitleLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SystemNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LoginButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PasswordChangeButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AutoLoginCheckBox
            // 
            this.AutoLoginCheckBox.AutoSize = true;
            this.AutoLoginCheckBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AutoLoginCheckBox.Location = new System.Drawing.Point(93, 85);
            this.AutoLoginCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.AutoLoginCheckBox.Name = "AutoLoginCheckBox";
            this.AutoLoginCheckBox.Size = new System.Drawing.Size(225, 19);
            this.AutoLoginCheckBox.TabIndex = 3;
            this.AutoLoginCheckBox.Text = "次回以降は自動的にログインする";
            this.AutoLoginCheckBox.UseVisualStyleBackColor = true;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PasswordTextBox.Location = new System.Drawing.Point(93, 50);
            this.PasswordTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PasswordTextBox.MaxLength = 13;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(225, 22);
            this.PasswordTextBox.TabIndex = 2;
            this.PasswordTextBox.Tag = "Require";
            // 
            // PasswordTitleLabel
            // 
            this.PasswordTitleLabel.AutoSize = true;
            this.PasswordTitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PasswordTitleLabel.Location = new System.Drawing.Point(25, 53);
            this.PasswordTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PasswordTitleLabel.Name = "PasswordTitleLabel";
            this.PasswordTitleLabel.Size = new System.Drawing.Size(64, 15);
            this.PasswordTitleLabel.TabIndex = 1016;
            this.PasswordTitleLabel.Text = "パスワード";
            // 
            // UserIDTextBox
            // 
            this.UserIDTextBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UserIDTextBox.Location = new System.Drawing.Point(93, 16);
            this.UserIDTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.UserIDTextBox.MaxLength = 50;
            this.UserIDTextBox.Name = "UserIDTextBox";
            this.UserIDTextBox.Size = new System.Drawing.Size(225, 22);
            this.UserIDTextBox.TabIndex = 1;
            this.UserIDTextBox.Tag = "Require";
            // 
            // UserIDTitleLabel
            // 
            this.UserIDTitleLabel.AutoSize = true;
            this.UserIDTitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UserIDTitleLabel.Location = new System.Drawing.Point(19, 19);
            this.UserIDTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UserIDTitleLabel.Name = "UserIDTitleLabel";
            this.UserIDTitleLabel.Size = new System.Drawing.Size(70, 15);
            this.UserIDTitleLabel.TabIndex = 1014;
            this.UserIDTitleLabel.Text = "ユーザーＩＤ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Location = new System.Drawing.Point(0, 150);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(340, 22);
            this.statusStrip1.TabIndex = 1011;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // SystemNameLabel
            // 
            this.SystemNameLabel.BackColor = System.Drawing.SystemColors.Control;
            this.SystemNameLabel.Name = "SystemNameLabel";
            this.SystemNameLabel.Size = new System.Drawing.Size(158, 17);
            this.SystemNameLabel.Text = "開発計画表システム Ver.0.0.0.0";
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.SystemColors.Control;
            this.LoginButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.LoginButton.Location = new System.Drawing.Point(11, 5);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(2);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(100, 30);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "ログイン";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(65)))));
            this.panel1.Controls.Add(this.PasswordChangeButton);
            this.panel1.Controls.Add(this.CloseButton);
            this.panel1.Controls.Add(this.LoginButton);
            this.panel1.Location = new System.Drawing.Point(0, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 40);
            this.panel1.TabIndex = 1020;
            // 
            // PasswordChangeButton
            // 
            this.PasswordChangeButton.BackColor = System.Drawing.SystemColors.Control;
            this.PasswordChangeButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.PasswordChangeButton.Location = new System.Drawing.Point(115, 5);
            this.PasswordChangeButton.Margin = new System.Windows.Forms.Padding(2);
            this.PasswordChangeButton.Name = "PasswordChangeButton";
            this.PasswordChangeButton.Size = new System.Drawing.Size(100, 30);
            this.PasswordChangeButton.TabIndex = 5;
            this.PasswordChangeButton.Text = "パスワードの変更";
            this.PasswordChangeButton.UseVisualStyleBackColor = false;
            this.PasswordChangeButton.Click += new System.EventHandler(this.PasswordChangeButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.Control;
            this.CloseButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.CloseButton.Location = new System.Drawing.Point(249, 5);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(2);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(80, 30);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "終了";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.LoginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(340, 172);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.AutoLoginCheckBox);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UserIDTextBox);
            this.Controls.Add(this.PasswordTitleLabel);
            this.Controls.Add(this.UserIDTitleLabel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ログイン - 開発計画表システム";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginForm_KeyPress);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label PasswordTitleLabel;
        private System.Windows.Forms.TextBox UserIDTextBox;
        private System.Windows.Forms.Label UserIDTitleLabel;
        private System.Windows.Forms.CheckBox AutoLoginCheckBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel SystemNameLabel;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button PasswordChangeButton;
    }
}