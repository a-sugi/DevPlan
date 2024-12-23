namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    partial class TestCarHistoryCompleteInputForm
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
            this.HistoryMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.historyMultiRowTemplate1 = new DevPlan.Presentation.UIDevPlan.TestCarSchedule.HistoryMultiRowTemplate();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchCheckBox = new System.Windows.Forms.CheckBox();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.KeyStorageLocationTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CarStorageLocationTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ChangePlaceTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.DesorptionPpartsTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TestContentTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.OdometerTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.WorkCompletionDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.EntryButton = new System.Windows.Forms.Button();
            this.StartDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.AutoFitLinkLabel = new System.Windows.Forms.LinkLabel();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.SearchUserCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryMultiRow)).BeginInit();
            this.DetailTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkCompletionDateTimePicker)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(895, 567);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.DescriptionLabel);
            this.ListFormMainPanel.Controls.Add(this.AutoFitLinkLabel);
            this.ListFormMainPanel.Controls.Add(this.StartDateDateTimePicker);
            this.ListFormMainPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ListFormMainPanel.Controls.Add(this.SearchUserCheckBox);
            this.ListFormMainPanel.Controls.Add(this.SearchCheckBox);
            this.ListFormMainPanel.Controls.Add(this.label2);
            this.ListFormMainPanel.Controls.Add(this.label1);
            this.ListFormMainPanel.Controls.Add(this.HistoryMultiRow);
            this.ListFormMainPanel.Size = new System.Drawing.Size(1023, 556);
            this.ListFormMainPanel.Controls.SetChildIndex(this.HistoryMultiRow, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label2, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SearchCheckBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SearchUserCheckBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DetailTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.StartDateDateTimePicker, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.AutoFitLinkLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DescriptionLabel, 0);
            // 
            // HistoryMultiRow
            // 
            this.HistoryMultiRow.AllowAutoExtend = true;
            this.HistoryMultiRow.AllowUserToAddRows = false;
            this.HistoryMultiRow.AllowUserToDeleteRows = false;
            this.HistoryMultiRow.AutoFitContent = GrapeCity.Win.MultiRow.AutoFitContent.All;
            this.HistoryMultiRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HistoryMultiRow.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.HistoryMultiRow.Location = new System.Drawing.Point(8, 93);
            this.HistoryMultiRow.MultiSelect = false;
            this.HistoryMultiRow.Name = "HistoryMultiRow";
            this.HistoryMultiRow.Size = new System.Drawing.Size(1006, 232);
            this.HistoryMultiRow.TabIndex = 1011;
            this.HistoryMultiRow.Text = "gcMultiRow1";
            this.HistoryMultiRow.VerticalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.HistoryMultiRow.VerticalScrollMode = GrapeCity.Win.MultiRow.ScrollMode.Pixel;
            this.HistoryMultiRow.RowEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.HistoryMultiRow_RowEnter);
            this.HistoryMultiRow.RowLeave += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.HistoryMultiRow_RowLeave);
            this.HistoryMultiRow.CellDoubleClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.HistoryMultiRow_CellDoubleClick);
            this.HistoryMultiRow.CellContentClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.HistoryMultiRow_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 1013;
            this.label1.Text = "日程表示期間";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 1013;
            this.label2.Text = "以降";
            // 
            // SearchCheckBox
            // 
            this.SearchCheckBox.AutoSize = true;
            this.SearchCheckBox.Location = new System.Drawing.Point(310, 70);
            this.SearchCheckBox.Name = "SearchCheckBox";
            this.SearchCheckBox.Size = new System.Drawing.Size(136, 19);
            this.SearchCheckBox.TabIndex = 1014;
            this.SearchCheckBox.Text = "他車系も表示する";
            this.SearchCheckBox.UseVisualStyleBackColor = true;
            this.SearchCheckBox.CheckedChanged += new System.EventHandler(this.SearchCheckBox_CheckedChanged);
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.Controls.Add(this.KeyStorageLocationTextBox, 1, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.label11, 0, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.CarStorageLocationTextBox, 1, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.label10, 0, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.ChangePlaceTextBox, 1, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.label9, 0, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.DesorptionPpartsTextBox, 1, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.label8, 0, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.TestContentTextBox, 1, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.label7, 0, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.OdometerTextBox, 1, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.label6, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.label5, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.WorkCompletionDateTimePicker, 1, 0);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(10, 331);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 7;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(672, 219);
            this.DetailTableLayoutPanel.TabIndex = 1015;
            // 
            // KeyStorageLocationTextBox
            // 
            this.KeyStorageLocationTextBox.Location = new System.Drawing.Point(225, 190);
            this.KeyStorageLocationTextBox.MaxLength = 100;
            this.KeyStorageLocationTextBox.Name = "KeyStorageLocationTextBox";
            this.KeyStorageLocationTextBox.Size = new System.Drawing.Size(440, 22);
            this.KeyStorageLocationTextBox.TabIndex = 13;
            this.KeyStorageLocationTextBox.Tag = "Byte(100);ItemName(鍵保管場所)";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Aquamarine;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(1, 187);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(220, 31);
            this.label11.TabIndex = 18;
            this.label11.Text = "鍵保管場所";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CarStorageLocationTextBox
            // 
            this.CarStorageLocationTextBox.Location = new System.Drawing.Point(225, 159);
            this.CarStorageLocationTextBox.MaxLength = 150;
            this.CarStorageLocationTextBox.Name = "CarStorageLocationTextBox";
            this.CarStorageLocationTextBox.Size = new System.Drawing.Size(440, 22);
            this.CarStorageLocationTextBox.TabIndex = 12;
            this.CarStorageLocationTextBox.Tag = "Byte(150);ItemName(車両保管場所)";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Aquamarine;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(1, 156);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(220, 30);
            this.label10.TabIndex = 16;
            this.label10.Text = "車両保管場所";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChangePlaceTextBox
            // 
            this.ChangePlaceTextBox.Location = new System.Drawing.Point(225, 128);
            this.ChangePlaceTextBox.MaxLength = 500;
            this.ChangePlaceTextBox.Name = "ChangePlaceTextBox";
            this.ChangePlaceTextBox.Size = new System.Drawing.Size(440, 22);
            this.ChangePlaceTextBox.TabIndex = 11;
            this.ChangePlaceTextBox.Tag = "Byte(500);ItemName(変更箇所有無（ソフト含む）と内容)";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Aquamarine;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(1, 125);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(220, 30);
            this.label9.TabIndex = 14;
            this.label9.Text = "変更箇所有無（ソフト含む）と内容";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DesorptionPpartsTextBox
            // 
            this.DesorptionPpartsTextBox.Location = new System.Drawing.Point(225, 97);
            this.DesorptionPpartsTextBox.MaxLength = 500;
            this.DesorptionPpartsTextBox.Name = "DesorptionPpartsTextBox";
            this.DesorptionPpartsTextBox.Size = new System.Drawing.Size(440, 22);
            this.DesorptionPpartsTextBox.TabIndex = 10;
            this.DesorptionPpartsTextBox.Tag = "Byte(500);ItemName(脱着部品と復元有無)";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Aquamarine;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(1, 94);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(220, 30);
            this.label8.TabIndex = 12;
            this.label8.Text = "脱着部品と復元有無";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TestContentTextBox
            // 
            this.TestContentTextBox.Location = new System.Drawing.Point(225, 66);
            this.TestContentTextBox.MaxLength = 400;
            this.TestContentTextBox.Name = "TestContentTextBox";
            this.TestContentTextBox.Size = new System.Drawing.Size(440, 22);
            this.TestContentTextBox.TabIndex = 9;
            this.TestContentTextBox.Tag = "Byte(400);ItemName(試験実施内容)";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Aquamarine;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(1, 63);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 30);
            this.label7.TabIndex = 10;
            this.label7.Text = "試験実施内容";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OdometerTextBox
            // 
            this.OdometerTextBox.Location = new System.Drawing.Point(225, 35);
            this.OdometerTextBox.MaxLength = 20;
            this.OdometerTextBox.Name = "OdometerTextBox";
            this.OdometerTextBox.Size = new System.Drawing.Size(440, 22);
            this.OdometerTextBox.TabIndex = 8;
            this.OdometerTextBox.Tag = "Byte(20);ItemName(オドメータ)";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Aquamarine;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(1, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(220, 30);
            this.label6.TabIndex = 8;
            this.label6.Text = "オドメータ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Aquamarine;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(1, 1);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(220, 30);
            this.label5.TabIndex = 6;
            this.label5.Text = "作業完了日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WorkCompletionDateTimePicker
            // 
            this.WorkCompletionDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.WorkCompletionDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.WorkCompletionDateTimePicker.Location = new System.Drawing.Point(225, 4);
            this.WorkCompletionDateTimePicker.Name = "WorkCompletionDateTimePicker";
            this.WorkCompletionDateTimePicker.Size = new System.Drawing.Size(125, 22);
            this.WorkCompletionDateTimePicker.TabIndex = 7;
            this.WorkCompletionDateTimePicker.Tag = "ItemName(作業完了日)";
            this.WorkCompletionDateTimePicker.Value = new System.DateTime(2020, 5, 14, 0, 0, 0, 0);
            // 
            // EntryButton
            // 
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(16, 567);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 1010;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // StartDateDateTimePicker
            // 
            this.StartDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.StartDateDateTimePicker.Location = new System.Drawing.Point(114, 66);
            this.StartDateDateTimePicker.Name = "StartDateDateTimePicker";
            this.StartDateDateTimePicker.Size = new System.Drawing.Size(138, 22);
            this.StartDateDateTimePicker.TabIndex = 1017;
            this.StartDateDateTimePicker.CloseUp += new System.EventHandler(this.StartDateDateTimePicker_CloseUp);
            this.StartDateDateTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartDateDateTimePicker_KeyPress);
            // 
            // AutoFitLinkLabel
            // 
            this.AutoFitLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoFitLinkLabel.AutoSize = true;
            this.AutoFitLinkLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.5F);
            this.AutoFitLinkLabel.Location = new System.Drawing.Point(919, 73);
            this.AutoFitLinkLabel.Name = "AutoFitLinkLabel";
            this.AutoFitLinkLabel.Size = new System.Drawing.Size(90, 13);
            this.AutoFitLinkLabel.TabIndex = 1029;
            this.AutoFitLinkLabel.TabStop = true;
            this.AutoFitLinkLabel.Text = "内容を展開する";
            this.AutoFitLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AutoFitLinkLabel_LinkClicked);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.ForeColor = System.Drawing.Color.Red;
            this.DescriptionLabel.Location = new System.Drawing.Point(14, 41);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(289, 15);
            this.DescriptionLabel.TabIndex = 1030;
            this.DescriptionLabel.Text = "ダブルクリックで対象のスケジュールに遷移します。";
            // 
            // SearchUserCheckBox
            // 
            this.SearchUserCheckBox.AutoSize = true;
            this.SearchUserCheckBox.Location = new System.Drawing.Point(452, 70);
            this.SearchUserCheckBox.Name = "SearchUserCheckBox";
            this.SearchUserCheckBox.Size = new System.Drawing.Size(151, 19);
            this.SearchUserCheckBox.TabIndex = 1014;
            this.SearchUserCheckBox.Text = "他予約者も表示する";
            this.SearchUserCheckBox.UseVisualStyleBackColor = true;
            this.SearchUserCheckBox.CheckedChanged += new System.EventHandler(this.SearchUserCheckBox_CheckedChanged);
            // 
            // TestCarHistoryCompleteInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1040, 604);
            this.Controls.Add(this.EntryButton);
            this.Name = "TestCarHistoryCompleteInputForm";
            this.Text = "TestCarHistoryCompleteInputForm";
            this.Load += new System.EventHandler(this.TestCarHistoryCompleteInputForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryMultiRow)).EndInit();
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkCompletionDateTimePicker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private GrapeCity.Win.MultiRow.GcMultiRow HistoryMultiRow;
        private HistoryMultiRowTemplate historyMultiRowTemplate1;
        private System.Windows.Forms.CheckBox SearchCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.TextBox KeyStorageLocationTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox CarStorageLocationTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ChangePlaceTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox DesorptionPpartsTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TestContentTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox OdometerTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private UC.NullableDateTimePicker WorkCompletionDateTimePicker;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.DateTimePicker StartDateDateTimePicker;
        private System.Windows.Forms.LinkLabel AutoFitLinkLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.CheckBox SearchUserCheckBox;
    }
}