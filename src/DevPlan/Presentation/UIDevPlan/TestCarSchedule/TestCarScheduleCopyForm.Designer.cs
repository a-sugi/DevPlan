namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    partial class TestCarScheduleCopyForm
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
            this.RegistButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.EndDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.StartDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.CopySoucePanel = new System.Windows.Forms.Panel();
            this.AjustRadioButton = new System.Windows.Forms.RadioButton();
            this.RequestRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateTimePicker)).BeginInit();
            this.CopySoucePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(366, 166);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.tableLayoutPanel1);
            this.ListFormMainPanel.Size = new System.Drawing.Size(481, 156);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            // 
            // RegistButton
            // 
            this.RegistButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RegistButton.BackColor = System.Drawing.SystemColors.Control;
            this.RegistButton.Location = new System.Drawing.Point(5, 166);
            this.RegistButton.Name = "RegistButton";
            this.RegistButton.Size = new System.Drawing.Size(120, 30);
            this.RegistButton.TabIndex = 1001;
            this.RegistButton.Text = "登録";
            this.RegistButton.UseVisualStyleBackColor = false;
            this.RegistButton.Click += new System.EventHandler(this.RegistButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 42);
            this.label1.TabIndex = 1011;
            this.label1.Text = "対象期間";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.78818F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.21182F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CopySoucePanel, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(466, 86);
            this.tableLayoutPanel1.TabIndex = 1012;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.EndDateTimePicker);
            this.panel1.Controls.Add(this.StartDateTimePicker);
            this.panel1.Location = new System.Drawing.Point(147, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 36);
            this.panel1.TabIndex = 1013;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 15);
            this.label2.TabIndex = 1014;
            this.label2.Text = "～";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EndDateTimePicker
            // 
            this.EndDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EndDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDateTimePicker.Location = new System.Drawing.Point(173, 7);
            this.EndDateTimePicker.Name = "EndDateTimePicker";
            this.EndDateTimePicker.Size = new System.Drawing.Size(136, 22);
            this.EndDateTimePicker.TabIndex = 4;
            this.EndDateTimePicker.Tag = "Required;ItemName(対象期間開始日)";
            this.EndDateTimePicker.Value = new System.DateTime(2018, 11, 27, 0, 0, 0, 0);
            // 
            // StartDateTimePicker
            // 
            this.StartDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.StartDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDateTimePicker.Location = new System.Drawing.Point(9, 7);
            this.StartDateTimePicker.Name = "StartDateTimePicker";
            this.StartDateTimePicker.Size = new System.Drawing.Size(136, 22);
            this.StartDateTimePicker.TabIndex = 3;
            this.StartDateTimePicker.Tag = "Required;ItemName(対象期間開始日)";
            this.StartDateTimePicker.Value = new System.DateTime(2018, 11, 27, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 41);
            this.label3.TabIndex = 1014;
            this.label3.Text = "コピー元";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CopySoucePanel
            // 
            this.CopySoucePanel.Controls.Add(this.AjustRadioButton);
            this.CopySoucePanel.Controls.Add(this.RequestRadioButton);
            this.CopySoucePanel.Location = new System.Drawing.Point(147, 4);
            this.CopySoucePanel.Name = "CopySoucePanel";
            this.CopySoucePanel.Size = new System.Drawing.Size(315, 35);
            this.CopySoucePanel.TabIndex = 1015;
            // 
            // AjustRadioButton
            // 
            this.AjustRadioButton.AutoSize = true;
            this.AjustRadioButton.Location = new System.Drawing.Point(183, 8);
            this.AjustRadioButton.Name = "AjustRadioButton";
            this.AjustRadioButton.Size = new System.Drawing.Size(106, 19);
            this.AjustRadioButton.TabIndex = 2;
            this.AjustRadioButton.Tag = "１次調整";
            this.AjustRadioButton.Text = "SJSB調整案";
            this.AjustRadioButton.UseVisualStyleBackColor = true;
            // 
            // RequestRadioButton
            // 
            this.RequestRadioButton.AutoSize = true;
            this.RequestRadioButton.Location = new System.Drawing.Point(18, 8);
            this.RequestRadioButton.Name = "RequestRadioButton";
            this.RequestRadioButton.Size = new System.Drawing.Size(130, 19);
            this.RequestRadioButton.TabIndex = 1;
            this.RequestRadioButton.Tag = "要望";
            this.RequestRadioButton.Text = "使用部署要望案";
            this.RequestRadioButton.UseVisualStyleBackColor = true;
            // 
            // TestCarScheduleCopyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(492, 200);
            this.Controls.Add(this.RegistButton);
            this.Name = "TestCarScheduleCopyForm";
            this.Text = "TestCarScheduleCopyForm";
            this.Load += new System.EventHandler(this.TestCarScheduleCopyForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.RegistButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateTimePicker)).EndInit();
            this.CopySoucePanel.ResumeLayout(false);
            this.CopySoucePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RegistButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private UC.NullableDateTimePicker StartDateTimePicker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private UC.NullableDateTimePicker EndDateTimePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel CopySoucePanel;
        private System.Windows.Forms.RadioButton AjustRadioButton;
        private System.Windows.Forms.RadioButton RequestRadioButton;
    }
}