namespace DevPlan.Presentation.UITestCar.ControlSheet
{
    partial class ControlSheetIssueEntryForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.IssueButton = new System.Windows.Forms.Button();
            this.HistoryTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.HistoryReceiptDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HistoryControlSheetIssueDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.HistoryTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryReceiptDayDateTimePicker)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryControlSheetIssueDayDateTimePicker)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(167, 113);
            this.CloseButton.TabIndex = 1005;
            this.CloseButton.Text = "取消";
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.ListFormMainPanel.Controls.Add(this.HistoryTableLayoutPanel);
            this.ListFormMainPanel.Size = new System.Drawing.Size(283, 103);
            this.ListFormMainPanel.TabIndex = 100;
            this.ListFormMainPanel.TabStop = true;
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.HistoryTableLayoutPanel, 0);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 398);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // IssueButton
            // 
            this.IssueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.IssueButton.BackColor = System.Drawing.SystemColors.Control;
            this.IssueButton.Location = new System.Drawing.Point(5, 113);
            this.IssueButton.Name = "IssueButton";
            this.IssueButton.Size = new System.Drawing.Size(120, 30);
            this.IssueButton.TabIndex = 1000;
            this.IssueButton.Text = "発行";
            this.IssueButton.UseVisualStyleBackColor = false;
            this.IssueButton.Click += new System.EventHandler(this.IssueButton_Click);
            // 
            // HistoryTableLayoutPanel
            // 
            this.HistoryTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.HistoryTableLayoutPanel.ColumnCount = 2;
            this.HistoryTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.HistoryTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.HistoryTableLayoutPanel.Controls.Add(this.HistoryReceiptDayDateTimePicker, 1, 0);
            this.HistoryTableLayoutPanel.Controls.Add(this.panel2, 0, 1);
            this.HistoryTableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.HistoryTableLayoutPanel.Controls.Add(this.HistoryControlSheetIssueDayDateTimePicker, 1, 1);
            this.HistoryTableLayoutPanel.Location = new System.Drawing.Point(2, 43);
            this.HistoryTableLayoutPanel.Name = "HistoryTableLayoutPanel";
            this.HistoryTableLayoutPanel.RowCount = 2;
            this.HistoryTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.HistoryTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.HistoryTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.HistoryTableLayoutPanel.Size = new System.Drawing.Size(276, 55);
            this.HistoryTableLayoutPanel.TabIndex = 1;
            this.HistoryTableLayoutPanel.TabStop = true;
            // 
            // HistoryReceiptDayDateTimePicker
            // 
            this.HistoryReceiptDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.HistoryReceiptDayDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HistoryReceiptDayDateTimePicker.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.HistoryReceiptDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HistoryReceiptDayDateTimePicker.Location = new System.Drawing.Point(115, 4);
            this.HistoryReceiptDayDateTimePicker.Name = "HistoryReceiptDayDateTimePicker";
            this.HistoryReceiptDayDateTimePicker.Size = new System.Drawing.Size(157, 19);
            this.HistoryReceiptDayDateTimePicker.TabIndex = 1;
            this.HistoryReceiptDayDateTimePicker.Tag = "Required;ItemName(受領日)";
            this.HistoryReceiptDayDateTimePicker.Value = new System.DateTime(2018, 11, 27, 0, 0, 0, 0);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Aquamarine;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.panel2.Location = new System.Drawing.Point(1, 27);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 27);
            this.panel2.TabIndex = 1016;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(68, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 12);
            this.label4.TabIndex = 1014;
            this.label4.Text = "(※)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 1013;
            this.label5.Text = "発行年月日";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Aquamarine;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(110, 25);
            this.panel1.TabIndex = 1015;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(42, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 12);
            this.label2.TabIndex = 1014;
            this.label2.Text = "(※)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1013;
            this.label3.Text = "受領日";
            // 
            // HistoryControlSheetIssueDayDateTimePicker
            // 
            this.HistoryControlSheetIssueDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.HistoryControlSheetIssueDayDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HistoryControlSheetIssueDayDateTimePicker.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.HistoryControlSheetIssueDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HistoryControlSheetIssueDayDateTimePicker.Location = new System.Drawing.Point(115, 30);
            this.HistoryControlSheetIssueDayDateTimePicker.Name = "HistoryControlSheetIssueDayDateTimePicker";
            this.HistoryControlSheetIssueDayDateTimePicker.Size = new System.Drawing.Size(157, 19);
            this.HistoryControlSheetIssueDayDateTimePicker.TabIndex = 2;
            this.HistoryControlSheetIssueDayDateTimePicker.Tag = "Required;ItemName(発行年月日)";
            this.HistoryControlSheetIssueDayDateTimePicker.Value = new System.DateTime(2018, 11, 27, 0, 0, 0, 0);
            // 
            // ControlSheetIssueEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(293, 147);
            this.Controls.Add(this.IssueButton);
            this.Name = "ControlSheetIssueEntryForm";
            this.Text = "ControlSheetIssueEntryForm";
            this.Load += new System.EventHandler(this.ControlSheetIssueEntryForm_Load);
            this.Controls.SetChildIndex(this.IssueButton, 0);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.HistoryTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HistoryReceiptDayDateTimePicker)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryControlSheetIssueDayDateTimePicker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button IssueButton;
        private System.Windows.Forms.TableLayoutPanel HistoryTableLayoutPanel;
        private UC.NullableDateTimePicker HistoryControlSheetIssueDayDateTimePicker;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private UC.NullableDateTimePicker HistoryReceiptDayDateTimePicker;
    }
}