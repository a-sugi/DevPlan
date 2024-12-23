namespace DevPlan.Presentation.Base
{
    partial class BaseSubForm
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
            this.ListFormMainPanel = new System.Windows.Forms.Panel();
            this.ListFormTitleLabel = new System.Windows.Forms.Label();
            this.ListFormPictureBox = new System.Windows.Forms.PictureBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListFormMainPanel.BackColor = System.Drawing.Color.White;
            this.ListFormMainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListFormMainPanel.Controls.Add(this.ListFormTitleLabel);
            this.ListFormMainPanel.Controls.Add(this.ListFormPictureBox);
            this.ListFormMainPanel.Location = new System.Drawing.Point(5, 5);
            this.ListFormMainPanel.Name = "ListFormMainPanel";
            this.ListFormMainPanel.Size = new System.Drawing.Size(123, 67);
            this.ListFormMainPanel.TabIndex = 1009;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.AutoSize = true;
            this.ListFormTitleLabel.BackColor = System.Drawing.Color.White;
            this.ListFormTitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ListFormTitleLabel.ForeColor = System.Drawing.Color.Black;
            this.ListFormTitleLabel.Location = new System.Drawing.Point(38, 10);
            this.ListFormTitleLabel.Name = "ListFormTitleLabel";
            this.ListFormTitleLabel.Size = new System.Drawing.Size(69, 19);
            this.ListFormTitleLabel.TabIndex = 1010;
            this.ListFormTitleLabel.Text = "タイトル";
            this.ListFormTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListFormPictureBox
            // 
            this.ListFormPictureBox.BackColor = System.Drawing.Color.White;
            this.ListFormPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ListFormPictureBox.Image = global::DevPlan.Presentation.Properties.Resources.lists;
            this.ListFormPictureBox.Location = new System.Drawing.Point(3, 3);
            this.ListFormPictureBox.Name = "ListFormPictureBox";
            this.ListFormPictureBox.Size = new System.Drawing.Size(30, 30);
            this.ListFormPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ListFormPictureBox.TabIndex = 1009;
            this.ListFormPictureBox.TabStop = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.BackColor = System.Drawing.SystemColors.Control;
            this.CloseButton.Location = new System.Drawing.Point(8, 77);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(120, 30);
            this.CloseButton.TabIndex = 1002;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // BaseSubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(134, 111);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ListFormMainPanel);
            this.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseSubForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BaseListForm";
            this.Load += new System.EventHandler(this.BaseListForm_Load);
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.PictureBox ListFormPictureBox;
        protected System.Windows.Forms.Button CloseButton;
        protected System.Windows.Forms.Label ListFormTitleLabel;
        protected System.Windows.Forms.Panel ListFormMainPanel;
    }
}