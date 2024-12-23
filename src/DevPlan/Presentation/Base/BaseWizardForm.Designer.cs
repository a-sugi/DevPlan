namespace DevPlan.Presentation.Base
{
    partial class BaseWizardForm
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
            this.WizardFormMainPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.BeforeButton = new System.Windows.Forms.Button();
            this.ForwardButton = new System.Windows.Forms.Button();
            this.EntryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WizardFormMainPanel
            // 
            this.WizardFormMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WizardFormMainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.WizardFormMainPanel.Location = new System.Drawing.Point(0, 0);
            this.WizardFormMainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.WizardFormMainPanel.Name = "WizardFormMainPanel";
            this.WizardFormMainPanel.Size = new System.Drawing.Size(444, 252);
            this.WizardFormMainPanel.TabIndex = 1009;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseButton.BackColor = System.Drawing.SystemColors.Control;
            this.CloseButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.CloseButton.Location = new System.Drawing.Point(12, 255);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(84, 24);
            this.CloseButton.TabIndex = 1001;
            this.CloseButton.Text = "中止";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // BeforeButton
            // 
            this.BeforeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BeforeButton.BackColor = System.Drawing.SystemColors.Control;
            this.BeforeButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.BeforeButton.Location = new System.Drawing.Point(162, 255);
            this.BeforeButton.Name = "BeforeButton";
            this.BeforeButton.Size = new System.Drawing.Size(84, 24);
            this.BeforeButton.TabIndex = 1011;
            this.BeforeButton.Text = "← 戻る";
            this.BeforeButton.UseVisualStyleBackColor = false;
            // 
            // ForwardButton
            // 
            this.ForwardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ForwardButton.BackColor = System.Drawing.SystemColors.Control;
            this.ForwardButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.ForwardButton.Location = new System.Drawing.Point(245, 255);
            this.ForwardButton.Name = "ForwardButton";
            this.ForwardButton.Size = new System.Drawing.Size(84, 24);
            this.ForwardButton.TabIndex = 1012;
            this.ForwardButton.Text = "次へ →";
            this.ForwardButton.UseVisualStyleBackColor = false;
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.EntryButton.Location = new System.Drawing.Point(348, 255);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(84, 24);
            this.EntryButton.TabIndex = 1013;
            this.EntryButton.Text = "完了";
            this.EntryButton.UseVisualStyleBackColor = false;
            // 
            // BaseWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(444, 291);
            this.Controls.Add(this.EntryButton);
            this.Controls.Add(this.ForwardButton);
            this.Controls.Add(this.BeforeButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.WizardFormMainPanel);
            this.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseWizardForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BaseWizardForm";
            this.Load += new System.EventHandler(this.BaseWizardForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Button CloseButton;
        protected System.Windows.Forms.Panel WizardFormMainPanel;
        protected System.Windows.Forms.Button BeforeButton;
        protected System.Windows.Forms.Button ForwardButton;
        protected System.Windows.Forms.Button EntryButton;
    }
}