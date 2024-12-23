namespace DevPlan.Presentation.UIDevPlan.WorkProgress
{
    partial class WorkProgressHistroryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.FavoriteEditButton = new System.Windows.Forms.Button();
            this.FavoriteTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.FavoriteComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ItemNameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RowAddButton = new System.Windows.Forms.Button();
            this.RowDeleteButton = new System.Windows.Forms.Button();
            this.FavoriteEntryButton = new System.Windows.Forms.Button();
            this.EntryButton = new System.Windows.Forms.Button();
            this.HistoryDataGridView = new System.Windows.Forms.DataGridView();
            this.ListedDateColumn = new DevPlan.Presentation.UC.DataGridViewCalendarColumn();
            this.CurrentSituationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FutureScheduleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputDateTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.CloseRadioButton = new System.Windows.Forms.RadioButton();
            this.OpenRadioButton = new System.Windows.Forms.RadioButton();
            this.ListDataLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.FavoriteTableLayoutPanel.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryDataGridView)).BeginInit();
            this.StatusTableLayoutPanel.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(956, 647);
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
            this.ListFormMainPanel.Controls.Add(this.ListDataLabel);
            this.ListFormMainPanel.Controls.Add(this.StatusTableLayoutPanel);
            this.ListFormMainPanel.Controls.Add(this.HistoryDataGridView);
            this.ListFormMainPanel.Controls.Add(this.RowDeleteButton);
            this.ListFormMainPanel.Controls.Add(this.RowAddButton);
            this.ListFormMainPanel.Controls.Add(this.label3);
            this.ListFormMainPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ListFormMainPanel.Controls.Add(this.FavoriteEditButton);
            this.ListFormMainPanel.Controls.Add(this.FavoriteTableLayoutPanel);
            this.ListFormMainPanel.Size = new System.Drawing.Size(1071, 637);
            this.ListFormMainPanel.Controls.SetChildIndex(this.FavoriteTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.FavoriteEditButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DetailTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label3, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RowAddButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RowDeleteButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.HistoryDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.StatusTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListDataLabel, 0);
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
            this.DetailTableLayoutPanel.Controls.Add(this.ItemNameLabel, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.label2, 0, 0);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(3, 77);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 1;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(400, 136);
            this.DetailTableLayoutPanel.TabIndex = 1013;
            // 
            // ItemNameLabel
            // 
            this.ItemNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.ItemNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemNameLabel.Location = new System.Drawing.Point(122, 1);
            this.ItemNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ItemNameLabel.Name = "ItemNameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(277, 134);
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
            this.label2.Size = new System.Drawing.Size(120, 134);
            this.label2.TabIndex = 0;
            this.label2.Text = "項目名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(3, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 1015;
            this.label3.Text = "進捗状況";
            // 
            // RowAddButton
            // 
            this.RowAddButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowAddButton.Location = new System.Drawing.Point(3, 238);
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
            this.RowDeleteButton.Location = new System.Drawing.Point(129, 238);
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
            this.FavoriteEntryButton.Location = new System.Drawing.Point(135, 647);
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
            this.EntryButton.Location = new System.Drawing.Point(9, 647);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 8;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // HistoryDataGridView
            // 
            this.HistoryDataGridView.AllowUserToAddRows = false;
            this.HistoryDataGridView.AllowUserToDeleteRows = false;
            this.HistoryDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HistoryDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.HistoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HistoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ListedDateColumn,
            this.CurrentSituationColumn,
            this.FutureScheduleColumn,
            this.Column3,
            this.InputDateTimeColumn,
            this.IdColumn});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HistoryDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.HistoryDataGridView.Location = new System.Drawing.Point(3, 275);
            this.HistoryDataGridView.MultiSelect = false;
            this.HistoryDataGridView.Name = "HistoryDataGridView";
            this.HistoryDataGridView.RowHeadersVisible = false;
            this.HistoryDataGridView.RowTemplate.Height = 21;
            this.HistoryDataGridView.Size = new System.Drawing.Size(1063, 357);
            this.HistoryDataGridView.TabIndex = 7;
            this.HistoryDataGridView.Tag = "Required;ItemName(進捗状況)";
            this.HistoryDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.HistoryDataGridView_CellValueChanged);
            // 
            // ListedDateColumn
            // 
            this.ListedDateColumn.DataPropertyName = "LISTED_DATE";
            this.ListedDateColumn.HeaderText = "日付";
            this.ListedDateColumn.MinimumWidth = 100;
            this.ListedDateColumn.Name = "ListedDateColumn";
            this.ListedDateColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ListedDateColumn.Width = 120;
            // 
            // CurrentSituationColumn
            // 
            this.CurrentSituationColumn.DataPropertyName = "CURRENT_SITUATION";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CurrentSituationColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.CurrentSituationColumn.HeaderText = "現状";
            this.CurrentSituationColumn.MaxInputLength = 2000;
            this.CurrentSituationColumn.Name = "CurrentSituationColumn";
            this.CurrentSituationColumn.Width = 400;
            // 
            // FutureScheduleColumn
            // 
            this.FutureScheduleColumn.DataPropertyName = "FUTURE_SCHEDULE";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FutureScheduleColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.FutureScheduleColumn.HeaderText = "今後の予定";
            this.FutureScheduleColumn.MaxInputLength = 2000;
            this.FutureScheduleColumn.Name = "FutureScheduleColumn";
            this.FutureScheduleColumn.Width = 400;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "INPUT_NAME";
            this.Column3.FillWeight = 180F;
            this.Column3.HeaderText = "編集者";
            this.Column3.MinimumWidth = 100;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // InputDateTimeColumn
            // 
            this.InputDateTimeColumn.DataPropertyName = "INPUT_DATETIME";
            dataGridViewCellStyle4.Format = "yyyy/MM/dd HH:mm:ss";
            this.InputDateTimeColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.InputDateTimeColumn.FillWeight = 150F;
            this.InputDateTimeColumn.HeaderText = "編集日時";
            this.InputDateTimeColumn.MinimumWidth = 100;
            this.InputDateTimeColumn.Name = "InputDateTimeColumn";
            this.InputDateTimeColumn.ReadOnly = true;
            this.InputDateTimeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IdColumn
            // 
            this.IdColumn.DataPropertyName = "ID";
            this.IdColumn.HeaderText = "ID";
            this.IdColumn.Name = "IdColumn";
            this.IdColumn.ReadOnly = true;
            this.IdColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IdColumn.Visible = false;
            // 
            // StatusTableLayoutPanel
            // 
            this.StatusTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.StatusTableLayoutPanel.ColumnCount = 2;
            this.StatusTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.StatusTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.StatusTableLayoutPanel.Controls.Add(this.label5, 0, 0);
            this.StatusTableLayoutPanel.Controls.Add(this.StatusPanel, 1, 0);
            this.StatusTableLayoutPanel.Location = new System.Drawing.Point(409, 78);
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
            // ListDataLabel
            // 
            this.ListDataLabel.AutoSize = true;
            this.ListDataLabel.ForeColor = System.Drawing.Color.Red;
            this.ListDataLabel.Location = new System.Drawing.Point(255, 246);
            this.ListDataLabel.Name = "ListDataLabel";
            this.ListDataLabel.Size = new System.Drawing.Size(0, 15);
            this.ListDataLabel.TabIndex = 1022;
            // 
            // WorkProgressHistroryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 698);
            this.Controls.Add(this.FavoriteEntryButton);
            this.Controls.Add(this.EntryButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(1100, 720);
            this.Name = "WorkProgressHistroryForm";
            this.Text = "WorkProgressHistroryForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorkProgressHistoryForm_FormClosing);
            this.Load += new System.EventHandler(this.WorkProgressHistoryForm_Load);
            this.Shown += new System.EventHandler(this.WorkProgressHistoryForm_Shown);
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
            ((System.ComponentModel.ISupportInitialize)(this.HistoryDataGridView)).EndInit();
            this.StatusTableLayoutPanel.ResumeLayout(false);
            this.StatusTableLayoutPanel.PerformLayout();
            this.StatusPanel.ResumeLayout(false);
            this.StatusPanel.PerformLayout();
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
        private System.Windows.Forms.DataGridView HistoryDataGridView;
        private System.Windows.Forms.TableLayoutPanel StatusTableLayoutPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.RadioButton CloseRadioButton;
        private System.Windows.Forms.RadioButton OpenRadioButton;
        private System.Windows.Forms.Label ListDataLabel;
        private UC.DataGridViewCalendarColumn ListedDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentSituationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FutureScheduleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputDateTimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdColumn;
    }
}