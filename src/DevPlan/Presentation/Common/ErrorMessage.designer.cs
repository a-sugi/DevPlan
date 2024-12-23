namespace DevPlan.Presentation.Common
{
    partial class ErrorMessage
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
            this.components = new System.ComponentModel.Container();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.DetailButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.DetailTextBox = new System.Windows.Forms.TextBox();
            this.ExportButton = new System.Windows.Forms.Button();
            this.ReportButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(12, 9);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(58, 48);
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            // 
            // MessageLabel
            // 
            this.MessageLabel.Location = new System.Drawing.Point(76, 9);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(396, 73);
            this.MessageLabel.TabIndex = 1;
            this.MessageLabel.Text = "MessageLabel";
            // 
            // DetailButton
            // 
            this.DetailButton.Location = new System.Drawing.Point(12, 82);
            this.DetailButton.Name = "DetailButton";
            this.DetailButton.Size = new System.Drawing.Size(70, 23);
            this.DetailButton.TabIndex = 2;
            this.DetailButton.Text = "Hide";
            this.DetailButton.UseVisualStyleBackColor = true;
            this.DetailButton.Click += new System.EventHandler(this.DetailButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(402, 82);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(70, 23);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // DetailTextBox
            // 
            this.DetailTextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.DetailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DetailTextBox.Location = new System.Drawing.Point(12, 111);
            this.DetailTextBox.Multiline = true;
            this.DetailTextBox.Name = "DetailTextBox";
            this.DetailTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DetailTextBox.Size = new System.Drawing.Size(460, 185);
            this.DetailTextBox.TabIndex = 6;
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(226, 82);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(170, 23);
            this.ExportButton.TabIndex = 4;
            this.ExportButton.Text = "Export to ApplicationLogFile";
            this.toolTip2.SetToolTip(this.ExportButton, "アプリケーションログを圧縮し、エクスポートします。");
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ReportButton
            // 
            this.ReportButton.Location = new System.Drawing.Point(116, 82);
            this.ReportButton.Name = "ReportButton";
            this.ReportButton.Size = new System.Drawing.Size(104, 23);
            this.ReportButton.TabIndex = 3;
            this.ReportButton.Text = "Report an Error";
            this.toolTip1.SetToolTip(this.ReportButton, "メーラーを起動し、管理者にエラーをレポートします。");
            this.ReportButton.UseVisualStyleBackColor = true;
            this.ReportButton.Click += new System.EventHandler(this.ReportButton_Click);
            // 
            // ErrorMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 304);
            this.Controls.Add(this.ReportButton);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.DetailTextBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.DetailButton);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.PictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorMessage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ErrorMessage";
            this.Load += new System.EventHandler(this.ErrorMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button DetailButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.TextBox DetailTextBox;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button ReportButton;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}