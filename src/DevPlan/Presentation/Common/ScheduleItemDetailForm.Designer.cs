namespace DevPlan.Presentation.Common
{
    partial class ScheduleItemDetailForm<item, schedule>
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
            this.TestCarListButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.label101 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.ReflectionLinkLabel = new System.Windows.Forms.LinkLabel();
            this.EntryCopyButton = new System.Windows.Forms.Button();
            this.OutsideCarListButton = new System.Windows.Forms.Button();
            this.InsideCarListButton = new System.Windows.Forms.Button();
            this.SjsbReservationCheckBox = new System.Windows.Forms.CheckBox();
            this.LastReservationDateLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label188 = new System.Windows.Forms.Label();
            this.LastReservationDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SinkCarClassLabel = new System.Windows.Forms.Label();
            this.SourceCarClassLabel = new System.Windows.Forms.Label();
            this.TransferButton = new System.Windows.Forms.Button();
            this.ListConfigPictureBox = new System.Windows.Forms.PictureBox();
            this.ScheduleItemDataGridView = new System.Windows.Forms.DataGridView();
            this.CarDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.LastReservationDateLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LastReservationDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListConfigPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleItemDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.CloseButton.Location = new System.Drawing.Point(698, 791);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(5);
            this.CloseButton.Size = new System.Drawing.Size(150, 30);
            this.CloseButton.TabIndex = 1012;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.ListFormTitleLabel.Size = new System.Drawing.Size(298, 24);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListFormMainPanel.Controls.Add(this.CarDataGridView);
            this.ListFormMainPanel.Controls.Add(this.ScheduleItemDataGridView);
            this.ListFormMainPanel.Controls.Add(this.ListConfigPictureBox);
            this.ListFormMainPanel.Controls.Add(this.SourceCarClassLabel);
            this.ListFormMainPanel.Controls.Add(this.SinkCarClassLabel);
            this.ListFormMainPanel.Controls.Add(this.ClearButton);
            this.ListFormMainPanel.Controls.Add(this.LastReservationDateLayoutPanel);
            this.ListFormMainPanel.Controls.Add(this.SjsbReservationCheckBox);
            this.ListFormMainPanel.Controls.Add(this.OutsideCarListButton);
            this.ListFormMainPanel.Controls.Add(this.InsideCarListButton);
            this.ListFormMainPanel.Controls.Add(this.ReflectionLinkLabel);
            this.ListFormMainPanel.Controls.Add(this.label102);
            this.ListFormMainPanel.Controls.Add(this.label101);
            this.ListFormMainPanel.Controls.Add(this.TestCarListButton);
            this.ListFormMainPanel.Margin = new System.Windows.Forms.Padding(5);
            this.ListFormMainPanel.Size = new System.Drawing.Size(841, 780);
            this.ListFormMainPanel.Controls.SetChildIndex(this.TestCarListButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label101, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label102, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ReflectionLinkLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.InsideCarListButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.OutsideCarListButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SjsbReservationCheckBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.LastReservationDateLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ClearButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SinkCarClassLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SourceCarClassLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListConfigPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ScheduleItemDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.CarDataGridView, 0);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.EntryButton.Location = new System.Drawing.Point(6, 791);
            this.EntryButton.Margin = new System.Windows.Forms.Padding(4);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(150, 30);
            this.EntryButton.TabIndex = 1001;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // TestCarListButton
            // 
            this.TestCarListButton.BackColor = System.Drawing.SystemColors.Control;
            this.TestCarListButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.TestCarListButton.Location = new System.Drawing.Point(438, 42);
            this.TestCarListButton.Margin = new System.Windows.Forms.Padding(4);
            this.TestCarListButton.Name = "TestCarListButton";
            this.TestCarListButton.Size = new System.Drawing.Size(112, 30);
            this.TestCarListButton.TabIndex = 10;
            this.TestCarListButton.Text = "試験車リスト";
            this.TestCarListButton.UseVisualStyleBackColor = false;
            this.TestCarListButton.Visible = false;
            this.TestCarListButton.Click += new System.EventHandler(this.TestCarListButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.DeleteButton.Location = new System.Drawing.Point(164, 791);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(4);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(150, 30);
            this.DeleteButton.TabIndex = 1003;
            this.DeleteButton.Text = "削除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label101.Location = new System.Drawing.Point(11, 80);
            this.label101.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(76, 17);
            this.label101.TabIndex = 1015;
            this.label101.Text = "車輌情報";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label102.Location = new System.Drawing.Point(434, 80);
            this.label102.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(199, 17);
            this.label102.TabIndex = 1016;
            this.label102.Text = "試験車管理システム（参照）";
            // 
            // ReflectionLinkLabel
            // 
            this.ReflectionLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReflectionLinkLabel.AutoSize = true;
            this.ReflectionLinkLabel.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.ReflectionLinkLabel.Location = new System.Drawing.Point(748, 80);
            this.ReflectionLinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ReflectionLinkLabel.Name = "ReflectionLinkLabel";
            this.ReflectionLinkLabel.Size = new System.Drawing.Size(76, 17);
            this.ReflectionLinkLabel.TabIndex = 1019;
            this.ReflectionLinkLabel.TabStop = true;
            this.ReflectionLinkLabel.Text = "一括反映";
            this.ReflectionLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ReflectionLinkLabel_LinkClicked);
            // 
            // EntryCopyButton
            // 
            this.EntryCopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryCopyButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryCopyButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.EntryCopyButton.Location = new System.Drawing.Point(164, 791);
            this.EntryCopyButton.Margin = new System.Windows.Forms.Padding(4);
            this.EntryCopyButton.Name = "EntryCopyButton";
            this.EntryCopyButton.Size = new System.Drawing.Size(150, 30);
            this.EntryCopyButton.TabIndex = 1002;
            this.EntryCopyButton.Text = "登録（コピー）";
            this.EntryCopyButton.UseVisualStyleBackColor = false;
            this.EntryCopyButton.Click += new System.EventHandler(this.EntryCopyButton_Click);
            // 
            // OutsideCarListButton
            // 
            this.OutsideCarListButton.BackColor = System.Drawing.SystemColors.Control;
            this.OutsideCarListButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.OutsideCarListButton.Location = new System.Drawing.Point(678, 42);
            this.OutsideCarListButton.Margin = new System.Windows.Forms.Padding(4);
            this.OutsideCarListButton.Name = "OutsideCarListButton";
            this.OutsideCarListButton.Size = new System.Drawing.Size(112, 30);
            this.OutsideCarListButton.TabIndex = 12;
            this.OutsideCarListButton.Text = "外製車リスト";
            this.OutsideCarListButton.UseVisualStyleBackColor = false;
            this.OutsideCarListButton.Visible = false;
            this.OutsideCarListButton.Click += new System.EventHandler(this.OutsideCarListButton_Click);
            // 
            // InsideCarListButton
            // 
            this.InsideCarListButton.BackColor = System.Drawing.SystemColors.Control;
            this.InsideCarListButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.InsideCarListButton.Location = new System.Drawing.Point(558, 42);
            this.InsideCarListButton.Margin = new System.Windows.Forms.Padding(4);
            this.InsideCarListButton.Name = "InsideCarListButton";
            this.InsideCarListButton.Size = new System.Drawing.Size(112, 30);
            this.InsideCarListButton.TabIndex = 11;
            this.InsideCarListButton.Text = "内製車リスト";
            this.InsideCarListButton.UseVisualStyleBackColor = false;
            this.InsideCarListButton.Visible = false;
            this.InsideCarListButton.Click += new System.EventHandler(this.InsideCarListButton_Click);
            // 
            // SjsbReservationCheckBox
            // 
            this.SjsbReservationCheckBox.AutoSize = true;
            this.SjsbReservationCheckBox.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.SjsbReservationCheckBox.Location = new System.Drawing.Point(591, 700);
            this.SjsbReservationCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.SjsbReservationCheckBox.Name = "SjsbReservationCheckBox";
            this.SjsbReservationCheckBox.Size = new System.Drawing.Size(200, 21);
            this.SjsbReservationCheckBox.TabIndex = 201;
            this.SjsbReservationCheckBox.Text = "SJSBの予約許可が必要";
            this.SjsbReservationCheckBox.UseVisualStyleBackColor = true;
            this.SjsbReservationCheckBox.Visible = false;
            // 
            // LastReservationDateLayoutPanel
            // 
            this.LastReservationDateLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LastReservationDateLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.LastReservationDateLayoutPanel.ColumnCount = 2;
            this.LastReservationDateLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.LastReservationDateLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.LastReservationDateLayoutPanel.Controls.Add(this.label188, 0, 0);
            this.LastReservationDateLayoutPanel.Controls.Add(this.LastReservationDateTimePicker, 1, 0);
            this.LastReservationDateLayoutPanel.Location = new System.Drawing.Point(435, 730);
            this.LastReservationDateLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.LastReservationDateLayoutPanel.Name = "LastReservationDateLayoutPanel";
            this.LastReservationDateLayoutPanel.RowCount = 1;
            this.LastReservationDateLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.LastReservationDateLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.LastReservationDateLayoutPanel.Size = new System.Drawing.Size(394, 41);
            this.LastReservationDateLayoutPanel.TabIndex = 1025;
            this.LastReservationDateLayoutPanel.Visible = false;
            // 
            // label188
            // 
            this.label188.AutoSize = true;
            this.label188.BackColor = System.Drawing.Color.Aquamarine;
            this.label188.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label188.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label188.Location = new System.Drawing.Point(1, 1);
            this.label188.Margin = new System.Windows.Forms.Padding(0);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(150, 39);
            this.label188.TabIndex = 2;
            this.label188.Text = "最終予約可能日";
            this.label188.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LastReservationDateTimePicker
            // 
            this.LastReservationDateTimePicker.CalendarFont = new System.Drawing.Font("MS UI Gothic", 10F);
            this.LastReservationDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.LastReservationDateTimePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.LastReservationDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.LastReservationDateTimePicker.Location = new System.Drawing.Point(156, 7);
            this.LastReservationDateTimePicker.Margin = new System.Windows.Forms.Padding(4, 6, 0, 0);
            this.LastReservationDateTimePicker.Name = "LastReservationDateTimePicker";
            this.LastReservationDateTimePicker.Size = new System.Drawing.Size(230, 26);
            this.LastReservationDateTimePicker.TabIndex = 202;
            this.LastReservationDateTimePicker.Tag = "ItemName(最終予約可能日)";
            this.LastReservationDateTimePicker.Value = new System.DateTime(2022, 3, 22, 0, 0, 0, 0);
            this.LastReservationDateTimePicker.Visible = false;
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.Control;
            this.ClearButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.ClearButton.Location = new System.Drawing.Point(12, 42);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(4);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(150, 30);
            this.ClearButton.TabIndex = 9;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SinkCarClassLabel
            // 
            this.SinkCarClassLabel.AutoSize = true;
            this.SinkCarClassLabel.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.SinkCarClassLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.SinkCarClassLabel.Location = new System.Drawing.Point(98, 80);
            this.SinkCarClassLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SinkCarClassLabel.Name = "SinkCarClassLabel";
            this.SinkCarClassLabel.Size = new System.Drawing.Size(0, 17);
            this.SinkCarClassLabel.TabIndex = 1026;
            // 
            // SourceCarClassLabel
            // 
            this.SourceCarClassLabel.AutoSize = true;
            this.SourceCarClassLabel.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.SourceCarClassLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.SourceCarClassLabel.Location = new System.Drawing.Point(645, 80);
            this.SourceCarClassLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SourceCarClassLabel.Name = "SourceCarClassLabel";
            this.SourceCarClassLabel.Size = new System.Drawing.Size(0, 17);
            this.SourceCarClassLabel.TabIndex = 1027;
            // 
            // TransferButton
            // 
            this.TransferButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TransferButton.BackColor = System.Drawing.SystemColors.Control;
            this.TransferButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.TransferButton.Location = new System.Drawing.Point(321, 791);
            this.TransferButton.Margin = new System.Windows.Forms.Padding(4);
            this.TransferButton.Name = "TransferButton";
            this.TransferButton.Size = new System.Drawing.Size(150, 30);
            this.TransferButton.TabIndex = 1004;
            this.TransferButton.Text = "管理移譲";
            this.TransferButton.UseVisualStyleBackColor = false;
            this.TransferButton.Click += new System.EventHandler(this.TransferButton_Click);
            // 
            // ListConfigPictureBox
            // 
            this.ListConfigPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ListConfigPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ListConfigPictureBox.Image = global::DevPlan.Presentation.Properties.Resources.CommonConfigButton;
            this.ListConfigPictureBox.Location = new System.Drawing.Point(385, 78);
            this.ListConfigPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.ListConfigPictureBox.Name = "ListConfigPictureBox";
            this.ListConfigPictureBox.Size = new System.Drawing.Size(20, 20);
            this.ListConfigPictureBox.TabIndex = 1037;
            this.ListConfigPictureBox.TabStop = false;
            this.ListConfigPictureBox.Click += new System.EventHandler(this.ListConfigPictureBox_Click);
            // 
            // ScheduleItemDataGridView
            // 
            this.ScheduleItemDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScheduleItemDataGridView.Location = new System.Drawing.Point(9, 101);
            this.ScheduleItemDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.ScheduleItemDataGridView.Name = "ScheduleItemDataGridView";
            this.ScheduleItemDataGridView.RowTemplate.Height = 21;
            this.ScheduleItemDataGridView.Size = new System.Drawing.Size(398, 670);
            this.ScheduleItemDataGridView.TabIndex = 1038;
            this.ScheduleItemDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScheduleItemDataGridView_CellValueChanged);
            // 
            // CarDataGridView
            // 
            this.CarDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CarDataGridView.Location = new System.Drawing.Point(435, 101);
            this.CarDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.CarDataGridView.Name = "CarDataGridView";
            this.CarDataGridView.RowTemplate.Height = 21;
            this.CarDataGridView.Size = new System.Drawing.Size(398, 591);
            this.CarDataGridView.TabIndex = 1039;
            // 
            // ScheduleItemDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(862, 826);
            this.Controls.Add(this.TransferButton);
            this.Controls.Add(this.EntryCopyButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EntryButton);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(620, 363);
            this.Name = "ScheduleItemDetailForm";
            this.Text = "TestCarItemAddForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScheduleItemDetailForm_FormClosing);
            this.Load += new System.EventHandler(this.ScheduleItemDetailForm_Load);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.DeleteButton, 0);
            this.Controls.SetChildIndex(this.EntryCopyButton, 0);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.TransferButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.LastReservationDateLayoutPanel.ResumeLayout(false);
            this.LastReservationDateLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LastReservationDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListConfigPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleItemDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Button TestCarListButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.LinkLabel ReflectionLinkLabel;
        private System.Windows.Forms.Button EntryCopyButton;
        private System.Windows.Forms.Button OutsideCarListButton;
        private System.Windows.Forms.Button InsideCarListButton;
        private System.Windows.Forms.CheckBox SjsbReservationCheckBox;
        private System.Windows.Forms.TableLayoutPanel LastReservationDateLayoutPanel;
        private System.Windows.Forms.Label label188;
        private UC.NullableDateTimePicker LastReservationDateTimePicker;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Label SourceCarClassLabel;
        private System.Windows.Forms.Label SinkCarClassLabel;
        private System.Windows.Forms.Button TransferButton;
        private System.Windows.Forms.PictureBox ListConfigPictureBox;
        private System.Windows.Forms.DataGridView ScheduleItemDataGridView;
        private System.Windows.Forms.DataGridView CarDataGridView;
    }
}