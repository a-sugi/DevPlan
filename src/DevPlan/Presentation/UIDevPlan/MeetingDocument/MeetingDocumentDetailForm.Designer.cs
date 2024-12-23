namespace DevPlan.Presentation.UIDevPlan.MeetingDocument
{
    partial class MeetingDocumentDetailForm
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
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DocumentNameComboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HeldDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.StartDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.EndDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.DocumentTypePanel = new System.Windows.Forms.Panel();
            this.RegularRadioButton = new System.Windows.Forms.RadioButton();
            this.ConsiderationRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeldDayDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).BeginInit();
            this.DocumentTypePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(258, 207);
            this.CloseButton.TabIndex = 8;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListFormMainPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ListFormMainPanel.Size = new System.Drawing.Size(373, 198);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DetailTableLayoutPanel, 0);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 207);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 7;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.Controls.Add(this.DocumentNameComboBox, 1, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.panel2, 0, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.panel1, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.HeldDayDateTimePicker, 1, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.StartDayDateTimePicker, 1, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.label18, 0, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.EndDayDateTimePicker, 1, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.label16, 0, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.DocumentTypePanel, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.label3, 0, 0);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(3, 39);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 5;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(367, 154);
            this.DetailTableLayoutPanel.TabIndex = 1012;
            // 
            // DocumentNameComboBox
            // 
            this.DocumentNameComboBox.DisplayMember = "資料名";
            this.DocumentNameComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentNameComboBox.FormattingEnabled = true;
            this.DocumentNameComboBox.Location = new System.Drawing.Point(125, 66);
            this.DocumentNameComboBox.MaxLength = 25;
            this.DocumentNameComboBox.Name = "DocumentNameComboBox";
            this.DocumentNameComboBox.Size = new System.Drawing.Size(238, 23);
            this.DocumentNameComboBox.TabIndex = 1029;
            this.DocumentNameComboBox.Tag = "Required;Wide(25);ItemName(資料名)";
            this.DocumentNameComboBox.ValueMember = "ID";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Aquamarine;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(1, 63);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(120, 30);
            this.panel2.TabIndex = 1028;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(46, -1);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 31);
            this.label4.TabIndex = 1014;
            this.label4.Text = "(※)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, -1);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 31);
            this.label5.TabIndex = 1013;
            this.label5.Text = "資料名";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Aquamarine;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(1, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 30);
            this.panel1.TabIndex = 1027;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(46, -1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 31);
            this.label1.TabIndex = 1014;
            this.label1.Text = "(※)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, -1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 31);
            this.label2.TabIndex = 1013;
            this.label2.Text = "開催日";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HeldDayDateTimePicker
            // 
            this.HeldDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.HeldDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HeldDayDateTimePicker.Location = new System.Drawing.Point(125, 35);
            this.HeldDayDateTimePicker.Name = "HeldDayDateTimePicker";
            this.HeldDayDateTimePicker.Size = new System.Drawing.Size(120, 22);
            this.HeldDayDateTimePicker.TabIndex = 3;
            this.HeldDayDateTimePicker.Tag = "Required;ItemName(開催日)";
            this.HeldDayDateTimePicker.Value = new System.DateTime(2017, 4, 26, 0, 0, 0, 0);
            // 
            // StartDayDateTimePicker
            // 
            this.StartDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.StartDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDayDateTimePicker.Location = new System.Drawing.Point(125, 97);
            this.StartDayDateTimePicker.Name = "StartDayDateTimePicker";
            this.StartDayDateTimePicker.Size = new System.Drawing.Size(120, 22);
            this.StartDayDateTimePicker.TabIndex = 5;
            this.StartDayDateTimePicker.Tag = "ItemName(更新開始日)";
            this.StartDayDateTimePicker.Value = new System.DateTime(2017, 4, 26, 0, 0, 0, 0);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Aquamarine;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(1, 125);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(120, 28);
            this.label18.TabIndex = 1021;
            this.label18.Text = "更新終了日";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EndDayDateTimePicker
            // 
            this.EndDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EndDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDayDateTimePicker.Location = new System.Drawing.Point(125, 128);
            this.EndDayDateTimePicker.Name = "EndDayDateTimePicker";
            this.EndDayDateTimePicker.Size = new System.Drawing.Size(120, 22);
            this.EndDayDateTimePicker.TabIndex = 6;
            this.EndDayDateTimePicker.Tag = "ItemName(更新終了日)";
            this.EndDayDateTimePicker.Value = new System.DateTime(2017, 4, 26, 0, 0, 0, 0);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Aquamarine;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(1, 94);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(120, 30);
            this.label16.TabIndex = 1020;
            this.label16.Text = "更新開始日";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocumentTypePanel
            // 
            this.DocumentTypePanel.AutoSize = true;
            this.DocumentTypePanel.Controls.Add(this.RegularRadioButton);
            this.DocumentTypePanel.Controls.Add(this.ConsiderationRadioButton);
            this.DocumentTypePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentTypePanel.Location = new System.Drawing.Point(122, 1);
            this.DocumentTypePanel.Margin = new System.Windows.Forms.Padding(0);
            this.DocumentTypePanel.Name = "DocumentTypePanel";
            this.DocumentTypePanel.Size = new System.Drawing.Size(244, 30);
            this.DocumentTypePanel.TabIndex = 3;
            this.DocumentTypePanel.Tag = "Required";
            // 
            // RegularRadioButton
            // 
            this.RegularRadioButton.AutoSize = true;
            this.RegularRadioButton.Location = new System.Drawing.Point(80, 6);
            this.RegularRadioButton.Name = "RegularRadioButton";
            this.RegularRadioButton.Size = new System.Drawing.Size(70, 19);
            this.RegularRadioButton.TabIndex = 2;
            this.RegularRadioButton.Tag = "1";
            this.RegularRadioButton.Text = "定例会";
            this.RegularRadioButton.UseVisualStyleBackColor = true;
            // 
            // ConsiderationRadioButton
            // 
            this.ConsiderationRadioButton.AutoSize = true;
            this.ConsiderationRadioButton.Checked = true;
            this.ConsiderationRadioButton.Location = new System.Drawing.Point(4, 6);
            this.ConsiderationRadioButton.Name = "ConsiderationRadioButton";
            this.ConsiderationRadioButton.Size = new System.Drawing.Size(70, 19);
            this.ConsiderationRadioButton.TabIndex = 1;
            this.ConsiderationRadioButton.TabStop = true;
            this.ConsiderationRadioButton.Tag = "0";
            this.ConsiderationRadioButton.Text = "検討会";
            this.ConsiderationRadioButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "資料種別";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MeetingDocumentDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 241);
            this.Controls.Add(this.EntryButton);
            this.Name = "MeetingDocumentDetailForm";
            this.Text = "MeetingDocumentDetailForm";
            this.Load += new System.EventHandler(this.CarShareScheduleDetailForm_Load);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeldDayDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).EndInit();
            this.DocumentTypePanel.ResumeLayout(false);
            this.DocumentTypePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private UC.NullableDateTimePicker StartDayDateTimePicker;
        private UC.NullableDateTimePicker EndDayDateTimePicker;
        private System.Windows.Forms.Panel DocumentTypePanel;
        private System.Windows.Forms.RadioButton RegularRadioButton;
        private System.Windows.Forms.RadioButton ConsiderationRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private UC.NullableDateTimePicker HeldDayDateTimePicker;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox DocumentNameComboBox;
    }
}