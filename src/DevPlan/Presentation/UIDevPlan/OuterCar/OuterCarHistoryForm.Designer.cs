namespace DevPlan.Presentation.UIDevPlan.OuterCar
{
    partial class OuterCarHistoryForm
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
            this.FavoriteEditButton = new System.Windows.Forms.Button();
            this.FavoriteTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.FavoriteComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.ItemNameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LastReservationLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RowAddButton = new System.Windows.Forms.Button();
            this.RowDeleteButton = new System.Windows.Forms.Button();
            this.FavoriteEntryButton = new System.Windows.Forms.Button();
            this.EntryButton = new System.Windows.Forms.Button();
            this.StatusTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.CloseRadioButton = new System.Windows.Forms.RadioButton();
            this.OpenRadioButton = new System.Windows.Forms.RadioButton();
            this.SjsbReservationLabel = new System.Windows.Forms.Label();
            this.ScheduleItemEditButton = new System.Windows.Forms.Button();
            this.ColorChangeButton = new System.Windows.Forms.Button();
            this.HistoryMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.GpsMapButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.FavoriteTableLayoutPanel.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            this.StatusTableLayoutPanel.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryMultiRow)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(848, 627);
            this.CloseButton.TabIndex = 10;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.GpsMapButton);
            this.ListFormMainPanel.Controls.Add(this.HistoryMultiRow);
            this.ListFormMainPanel.Controls.Add(this.ColorChangeButton);
            this.ListFormMainPanel.Controls.Add(this.ScheduleItemEditButton);
            this.ListFormMainPanel.Controls.Add(this.SjsbReservationLabel);
            this.ListFormMainPanel.Controls.Add(this.StatusTableLayoutPanel);
            this.ListFormMainPanel.Controls.Add(this.RowDeleteButton);
            this.ListFormMainPanel.Controls.Add(this.RowAddButton);
            this.ListFormMainPanel.Controls.Add(this.label3);
            this.ListFormMainPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ListFormMainPanel.Controls.Add(this.FavoriteEditButton);
            this.ListFormMainPanel.Controls.Add(this.FavoriteTableLayoutPanel);
            this.ListFormMainPanel.Size = new System.Drawing.Size(963, 617);
            this.ListFormMainPanel.Controls.SetChildIndex(this.FavoriteTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.FavoriteEditButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DetailTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label3, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RowAddButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RowDeleteButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.StatusTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SjsbReservationLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ScheduleItemEditButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ColorChangeButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.HistoryMultiRow, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.GpsMapButton, 0);
            // 
            // FavoriteEditButton
            // 
            this.FavoriteEditButton.BackColor = System.Drawing.SystemColors.Control;
            this.FavoriteEditButton.Location = new System.Drawing.Point(409, 40);
            this.FavoriteEditButton.Name = "FavoriteEditButton";
            this.FavoriteEditButton.Size = new System.Drawing.Size(120, 30);
            this.FavoriteEditButton.TabIndex = 2;
            this.FavoriteEditButton.Text = "編集";
            this.FavoriteEditButton.UseVisualStyleBackColor = false;
            this.FavoriteEditButton.Click += new System.EventHandler(this.FavoriteEditButton_Click);
            // 
            // FavoriteTableLayoutPanel
            // 
            this.FavoriteTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.FavoriteTableLayoutPanel.ColumnCount = 2;
            this.FavoriteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.FavoriteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.FavoriteTableLayoutPanel.Controls.Add(this.FavoriteComboBox, 1, 0);
            this.FavoriteTableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.FavoriteTableLayoutPanel.Location = new System.Drawing.Point(3, 39);
            this.FavoriteTableLayoutPanel.Name = "FavoriteTableLayoutPanel";
            this.FavoriteTableLayoutPanel.RowCount = 1;
            this.FavoriteTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.FavoriteTableLayoutPanel.Size = new System.Drawing.Size(400, 32);
            this.FavoriteTableLayoutPanel.TabIndex = 1012;
            // 
            // FavoriteComboBox
            // 
            this.FavoriteComboBox.DisplayMember = "TITLE";
            this.FavoriteComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FavoriteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FavoriteComboBox.FormattingEnabled = true;
            this.FavoriteComboBox.Location = new System.Drawing.Point(125, 4);
            this.FavoriteComboBox.Name = "FavoriteComboBox";
            this.FavoriteComboBox.Size = new System.Drawing.Size(271, 23);
            this.FavoriteComboBox.TabIndex = 1;
            this.FavoriteComboBox.ValueMember = "ID";
            this.FavoriteComboBox.SelectedValueChanged += new System.EventHandler(this.FavoriteComboBox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "お気に入り";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.Controls.Add(this.label4, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.ItemNameLabel, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.label2, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.LastReservationLabel, 1, 1);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(3, 92);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 2;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(400, 164);
            this.DetailTableLayoutPanel.TabIndex = 1013;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Aquamarine;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(1, 132);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 31);
            this.label4.TabIndex = 3;
            this.label4.Text = "最終予約可能日";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ItemNameLabel
            // 
            this.ItemNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.ItemNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemNameLabel.Location = new System.Drawing.Point(122, 1);
            this.ItemNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ItemNameLabel.Name = "ItemNameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(277, 130);
            this.ItemNameLabel.TabIndex = 1;
            this.ItemNameLabel.Tag = "";
            this.ItemNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Aquamarine;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 130);
            this.label2.TabIndex = 0;
            this.label2.Text = "車両名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LastReservationLabel
            // 
            this.LastReservationLabel.AutoSize = true;
            this.LastReservationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LastReservationLabel.Location = new System.Drawing.Point(122, 132);
            this.LastReservationLabel.Margin = new System.Windows.Forms.Padding(0);
            this.LastReservationLabel.Name = "LastReservationLabel";
            this.LastReservationLabel.Size = new System.Drawing.Size(277, 31);
            this.LastReservationLabel.TabIndex = 4;
            this.LastReservationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(3, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 1015;
            this.label3.Text = "進捗状況";
            // 
            // RowAddButton
            // 
            this.RowAddButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowAddButton.Location = new System.Drawing.Point(3, 277);
            this.RowAddButton.Name = "RowAddButton";
            this.RowAddButton.Size = new System.Drawing.Size(120, 30);
            this.RowAddButton.TabIndex = 5;
            this.RowAddButton.Text = "行追加";
            this.RowAddButton.UseVisualStyleBackColor = false;
            this.RowAddButton.Click += new System.EventHandler(this.RowAddButton_Click);
            // 
            // RowDeleteButton
            // 
            this.RowDeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowDeleteButton.Location = new System.Drawing.Point(129, 277);
            this.RowDeleteButton.Name = "RowDeleteButton";
            this.RowDeleteButton.Size = new System.Drawing.Size(120, 30);
            this.RowDeleteButton.TabIndex = 6;
            this.RowDeleteButton.Text = "行削除";
            this.RowDeleteButton.UseVisualStyleBackColor = false;
            this.RowDeleteButton.Click += new System.EventHandler(this.RowDeleteButton_Click);
            // 
            // FavoriteEntryButton
            // 
            this.FavoriteEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FavoriteEntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.FavoriteEntryButton.Location = new System.Drawing.Point(135, 627);
            this.FavoriteEntryButton.Name = "FavoriteEntryButton";
            this.FavoriteEntryButton.Size = new System.Drawing.Size(120, 30);
            this.FavoriteEntryButton.TabIndex = 9;
            this.FavoriteEntryButton.Text = "お気に入り登録";
            this.FavoriteEntryButton.UseVisualStyleBackColor = false;
            this.FavoriteEntryButton.Click += new System.EventHandler(this.FavoriteEntryButton_Click);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(9, 627);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 8;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // StatusTableLayoutPanel
            // 
            this.StatusTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.StatusTableLayoutPanel.ColumnCount = 2;
            this.StatusTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.StatusTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.StatusTableLayoutPanel.Controls.Add(this.label5, 0, 0);
            this.StatusTableLayoutPanel.Controls.Add(this.StatusPanel, 1, 0);
            this.StatusTableLayoutPanel.Location = new System.Drawing.Point(409, 92);
            this.StatusTableLayoutPanel.Name = "StatusTableLayoutPanel";
            this.StatusTableLayoutPanel.RowCount = 1;
            this.StatusTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.StatusTableLayoutPanel.Size = new System.Drawing.Size(250, 80);
            this.StatusTableLayoutPanel.TabIndex = 1019;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Aquamarine;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(1, 1);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 78);
            this.label5.TabIndex = 0;
            this.label5.Text = "ステータス";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.Red;
            this.StatusPanel.Controls.Add(this.CloseRadioButton);
            this.StatusPanel.Controls.Add(this.OpenRadioButton);
            this.StatusPanel.Location = new System.Drawing.Point(122, 1);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(128, 78);
            this.StatusPanel.TabIndex = 1;
            // 
            // CloseRadioButton
            // 
            this.CloseRadioButton.AutoSize = true;
            this.CloseRadioButton.ForeColor = System.Drawing.Color.White;
            this.CloseRadioButton.Location = new System.Drawing.Point(27, 44);
            this.CloseRadioButton.Name = "CloseRadioButton";
            this.CloseRadioButton.Size = new System.Drawing.Size(71, 19);
            this.CloseRadioButton.TabIndex = 4;
            this.CloseRadioButton.Tag = "close";
            this.CloseRadioButton.Text = "CLOSE";
            this.CloseRadioButton.UseVisualStyleBackColor = true;
            this.CloseRadioButton.CheckedChanged += new System.EventHandler(this.StatusRadioButton_CheckedChanged);
            // 
            // OpenRadioButton
            // 
            this.OpenRadioButton.AutoSize = true;
            this.OpenRadioButton.Checked = true;
            this.OpenRadioButton.ForeColor = System.Drawing.Color.White;
            this.OpenRadioButton.Location = new System.Drawing.Point(27, 19);
            this.OpenRadioButton.Name = "OpenRadioButton";
            this.OpenRadioButton.Size = new System.Drawing.Size(63, 19);
            this.OpenRadioButton.TabIndex = 3;
            this.OpenRadioButton.TabStop = true;
            this.OpenRadioButton.Tag = "open";
            this.OpenRadioButton.Text = "OPEN";
            this.OpenRadioButton.UseVisualStyleBackColor = true;
            this.OpenRadioButton.CheckedChanged += new System.EventHandler(this.StatusRadioButton_CheckedChanged);
            // 
            // SjsbReservationLabel
            // 
            this.SjsbReservationLabel.AutoSize = true;
            this.SjsbReservationLabel.Location = new System.Drawing.Point(4, 74);
            this.SjsbReservationLabel.Name = "SjsbReservationLabel";
            this.SjsbReservationLabel.Size = new System.Drawing.Size(157, 15);
            this.SjsbReservationLabel.TabIndex = 1020;
            this.SjsbReservationLabel.Text = "SJSBの予約許可が必要";
            // 
            // ScheduleItemEditButton
            // 
            this.ScheduleItemEditButton.BackColor = System.Drawing.SystemColors.Control;
            this.ScheduleItemEditButton.Font = new System.Drawing.Font("MS UI Gothic", 11.25F);
            this.ScheduleItemEditButton.Location = new System.Drawing.Point(409, 193);
            this.ScheduleItemEditButton.Name = "ScheduleItemEditButton";
            this.ScheduleItemEditButton.Size = new System.Drawing.Size(120, 30);
            this.ScheduleItemEditButton.TabIndex = 1022;
            this.ScheduleItemEditButton.Text = "車両名の編集";
            this.ScheduleItemEditButton.UseVisualStyleBackColor = false;
            this.ScheduleItemEditButton.Click += new System.EventHandler(this.ScheduleItemEditButton_Click);
            // 
            // ColorChangeButton
            // 
            this.ColorChangeButton.BackColor = System.Drawing.SystemColors.Control;
            this.ColorChangeButton.Location = new System.Drawing.Point(255, 277);
            this.ColorChangeButton.Name = "ColorChangeButton";
            this.ColorChangeButton.Size = new System.Drawing.Size(148, 30);
            this.ColorChangeButton.TabIndex = 1027;
            this.ColorChangeButton.Text = "文字色　黒⇔赤";
            this.ColorChangeButton.UseVisualStyleBackColor = false;
            this.ColorChangeButton.Click += new System.EventHandler(this.ColorChangeButton_Click);
            // 
            // HistoryMultiRow
            // 
            this.HistoryMultiRow.AllowAutoExtend = true;
            this.HistoryMultiRow.AllowUserToAddRows = false;
            this.HistoryMultiRow.AllowUserToDeleteRows = false;
            this.HistoryMultiRow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryMultiRow.AutoFitContent = GrapeCity.Win.MultiRow.AutoFitContent.All;
            this.HistoryMultiRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HistoryMultiRow.Location = new System.Drawing.Point(3, 313);
            this.HistoryMultiRow.MultiSelect = false;
            this.HistoryMultiRow.Name = "HistoryMultiRow";
            this.HistoryMultiRow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HistoryMultiRow.Size = new System.Drawing.Size(953, 299);
            this.HistoryMultiRow.SplitMode = GrapeCity.Win.MultiRow.SplitMode.None;
            this.HistoryMultiRow.TabIndex = 1029;
            this.HistoryMultiRow.VerticalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.HistoryMultiRow.VerticalScrollMode = GrapeCity.Win.MultiRow.ScrollMode.Pixel;
            this.HistoryMultiRow.DataError += new System.EventHandler<GrapeCity.Win.MultiRow.DataErrorEventArgs>(this.HistoryMultiRow_DataError);
            this.HistoryMultiRow.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.HistoryMultiRow_CellValidating);
            // 
            // GpsMapButton
            // 
            this.GpsMapButton.BackColor = System.Drawing.SystemColors.Control;
            this.GpsMapButton.Location = new System.Drawing.Point(409, 277);
            this.GpsMapButton.Name = "GpsMapButton";
            this.GpsMapButton.Size = new System.Drawing.Size(120, 30);
            this.GpsMapButton.TabIndex = 1030;
            this.GpsMapButton.Text = "Map";
            this.GpsMapButton.UseVisualStyleBackColor = false;
            this.GpsMapButton.Click += new System.EventHandler(this.GpsMapButton_Click);
            // 
            // OuterCarHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(974, 661);
            this.Controls.Add(this.FavoriteEntryButton);
            this.Controls.Add(this.EntryButton);
            this.MinimumSize = new System.Drawing.Size(990, 700);
            this.Name = "OuterCarHistoryForm";
            this.Text = "OuterCarHistoryForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OuterCarHistoryForm_FormClosing);
            this.Load += new System.EventHandler(this.OuterCarHistoryForm_Load);
            this.Shown += new System.EventHandler(this.OuterCarHistoryForm_Shown);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.FavoriteEntryButton, 0);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.FavoriteTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            this.StatusTableLayoutPanel.ResumeLayout(false);
            this.StatusTableLayoutPanel.PerformLayout();
            this.StatusPanel.ResumeLayout(false);
            this.StatusPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryMultiRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FavoriteEditButton;
        private System.Windows.Forms.TableLayoutPanel FavoriteTableLayoutPanel;
        private System.Windows.Forms.ComboBox FavoriteComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.Label ItemNameLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RowAddButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RowDeleteButton;
        private System.Windows.Forms.Button FavoriteEntryButton;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.TableLayoutPanel StatusTableLayoutPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.RadioButton CloseRadioButton;
        private System.Windows.Forms.RadioButton OpenRadioButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LastReservationLabel;
        private System.Windows.Forms.Label SjsbReservationLabel;
        private System.Windows.Forms.Button ScheduleItemEditButton;
        private System.Windows.Forms.Button ColorChangeButton;
        private GrapeCity.Win.MultiRow.GcMultiRow HistoryMultiRow;
        private System.Windows.Forms.Button GpsMapButton;
    }
}