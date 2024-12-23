namespace DevPlan.Presentation.UIDevPlan.ReportMaterial
{
    partial class ReportMaterialForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.CondTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.FirstDayNullableDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.TrialProductionSeasonTextBox = new System.Windows.Forms.TextBox();
            this.LastDayNullableDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AffiliationComboBox = new System.Windows.Forms.ComboBox();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.FollowListRadioButton = new System.Windows.Forms.RadioButton();
            this.DepartmentRadioButton = new System.Windows.Forms.RadioButton();
            this.SectionGroupRadioButton = new System.Windows.Forms.RadioButton();
            this.SectionRadioButton = new System.Windows.Forms.RadioButton();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.InfoListDataGridView = new System.Windows.Forms.DataGridView();
            this.EntryButton = new System.Windows.Forms.Button();
            this.AllCheckBox = new System.Windows.Forms.CheckBox();
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionGroupCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GeneralCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentSituation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FutureSchedule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectKeyword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PersonelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputDatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContentsPanel.SuspendLayout();
            this.CondTableLayoutPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstDayNullableDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LastDayNullableDateTimePicker)).BeginInit();
            this.panel2.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.Location = new System.Drawing.Point(1052, 605);
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.AllCheckBox);
            this.ContentsPanel.Controls.Add(this.MainPanel);
            this.ContentsPanel.Controls.Add(this.CondTableLayoutPanel);
            this.ContentsPanel.Controls.Add(this.label2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "検索条件";
            // 
            // CondTableLayoutPanel
            // 
            this.CondTableLayoutPanel.AutoSize = true;
            this.CondTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.CondTableLayoutPanel.ColumnCount = 2;
            this.CondTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.CondTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 472F));
            this.CondTableLayoutPanel.Controls.Add(this.panel3, 1, 2);
            this.CondTableLayoutPanel.Controls.Add(this.label5, 0, 2);
            this.CondTableLayoutPanel.Controls.Add(this.label4, 0, 1);
            this.CondTableLayoutPanel.Controls.Add(this.label3, 0, 0);
            this.CondTableLayoutPanel.Controls.Add(this.panel2, 1, 1);
            this.CondTableLayoutPanel.Controls.Add(this.StatusPanel, 1, 0);
            this.CondTableLayoutPanel.Location = new System.Drawing.Point(12, 29);
            this.CondTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CondTableLayoutPanel.Name = "CondTableLayoutPanel";
            this.CondTableLayoutPanel.RowCount = 3;
            this.CondTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CondTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CondTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.CondTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.CondTableLayoutPanel.Size = new System.Drawing.Size(595, 96);
            this.CondTableLayoutPanel.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.FirstDayNullableDateTimePicker);
            this.panel3.Controls.Add(this.TrialProductionSeasonTextBox);
            this.panel3.Controls.Add(this.LastDayNullableDateTimePicker);
            this.panel3.Location = new System.Drawing.Point(122, 63);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(434, 32);
            this.panel3.TabIndex = 2;
            // 
            // FirstDayNullableDateTimePicker
            // 
            this.FirstDayNullableDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.FirstDayNullableDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FirstDayNullableDateTimePicker.Location = new System.Drawing.Point(22, 4);
            this.FirstDayNullableDateTimePicker.Name = "FirstDayNullableDateTimePicker";
            this.FirstDayNullableDateTimePicker.Size = new System.Drawing.Size(140, 22);
            this.FirstDayNullableDateTimePicker.TabIndex = 7;
            this.FirstDayNullableDateTimePicker.Value = new System.DateTime(2017, 7, 19, 0, 0, 0, 0);
            // 
            // TrialProductionSeasonTextBox
            // 
            this.TrialProductionSeasonTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TrialProductionSeasonTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TrialProductionSeasonTextBox.Location = new System.Drawing.Point(179, 8);
            this.TrialProductionSeasonTextBox.Name = "TrialProductionSeasonTextBox";
            this.TrialProductionSeasonTextBox.Size = new System.Drawing.Size(50, 15);
            this.TrialProductionSeasonTextBox.TabIndex = 0;
            this.TrialProductionSeasonTextBox.TabStop = false;
            this.TrialProductionSeasonTextBox.Text = "～";
            this.TrialProductionSeasonTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LastDayNullableDateTimePicker
            // 
            this.LastDayNullableDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.LastDayNullableDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.LastDayNullableDateTimePicker.Location = new System.Drawing.Point(246, 4);
            this.LastDayNullableDateTimePicker.Name = "LastDayNullableDateTimePicker";
            this.LastDayNullableDateTimePicker.Size = new System.Drawing.Size(140, 22);
            this.LastDayNullableDateTimePicker.TabIndex = 8;
            this.LastDayNullableDateTimePicker.Value = new System.DateTime(2018, 4, 17, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Aquamarine;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(1, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 32);
            this.label5.TabIndex = 0;
            this.label5.Text = "期間";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Aquamarine;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(1, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "部署";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.label3.TabIndex = 0;
            this.label3.Text = "種別";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.AffiliationComboBox);
            this.panel2.Location = new System.Drawing.Point(122, 32);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(434, 30);
            this.panel2.TabIndex = 1;
            // 
            // AffiliationComboBox
            // 
            this.AffiliationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AffiliationComboBox.FormattingEnabled = true;
            this.AffiliationComboBox.Location = new System.Drawing.Point(23, 3);
            this.AffiliationComboBox.Name = "AffiliationComboBox";
            this.AffiliationComboBox.Size = new System.Drawing.Size(228, 23);
            this.AffiliationComboBox.TabIndex = 6;
            this.AffiliationComboBox.Click += new System.EventHandler(this.AffiliationComboBox_Click);
            // 
            // StatusPanel
            // 
            this.StatusPanel.Controls.Add(this.FollowListRadioButton);
            this.StatusPanel.Controls.Add(this.DepartmentRadioButton);
            this.StatusPanel.Controls.Add(this.SectionGroupRadioButton);
            this.StatusPanel.Controls.Add(this.SectionRadioButton);
            this.StatusPanel.Location = new System.Drawing.Point(122, 1);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(434, 30);
            this.StatusPanel.TabIndex = 0;
            // 
            // FollowListRadioButton
            // 
            this.FollowListRadioButton.AutoSize = true;
            this.FollowListRadioButton.Checked = true;
            this.FollowListRadioButton.Location = new System.Drawing.Point(23, 6);
            this.FollowListRadioButton.Name = "FollowListRadioButton";
            this.FollowListRadioButton.Size = new System.Drawing.Size(85, 19);
            this.FollowListRadioButton.TabIndex = 2;
            this.FollowListRadioButton.TabStop = true;
            this.FollowListRadioButton.Tag = "1";
            this.FollowListRadioButton.Text = "進捗履歴";
            this.FollowListRadioButton.UseVisualStyleBackColor = true;
            this.FollowListRadioButton.CheckedChanged += new System.EventHandler(this.FollowListRadioButton_CheckedChanged);
            // 
            // DepartmentRadioButton
            // 
            this.DepartmentRadioButton.AutoSize = true;
            this.DepartmentRadioButton.Location = new System.Drawing.Point(126, 6);
            this.DepartmentRadioButton.Name = "DepartmentRadioButton";
            this.DepartmentRadioButton.Size = new System.Drawing.Size(70, 19);
            this.DepartmentRadioButton.TabIndex = 3;
            this.DepartmentRadioButton.Tag = "2";
            this.DepartmentRadioButton.Text = "部週報";
            this.DepartmentRadioButton.UseVisualStyleBackColor = true;
            this.DepartmentRadioButton.CheckedChanged += new System.EventHandler(this.DepartmentRadioButton_CheckedChanged);
            // 
            // SectionGroupRadioButton
            // 
            this.SectionGroupRadioButton.AutoSize = true;
            this.SectionGroupRadioButton.Location = new System.Drawing.Point(306, 6);
            this.SectionGroupRadioButton.Name = "SectionGroupRadioButton";
            this.SectionGroupRadioButton.Size = new System.Drawing.Size(85, 19);
            this.SectionGroupRadioButton.TabIndex = 5;
            this.SectionGroupRadioButton.Tag = "4";
            this.SectionGroupRadioButton.Text = "担当週報";
            this.SectionGroupRadioButton.UseVisualStyleBackColor = true;
            this.SectionGroupRadioButton.CheckedChanged += new System.EventHandler(this.SectionGroupRadioButton_CheckedChanged);
            // 
            // SectionRadioButton
            // 
            this.SectionRadioButton.AutoSize = true;
            this.SectionRadioButton.Location = new System.Drawing.Point(216, 6);
            this.SectionRadioButton.Name = "SectionRadioButton";
            this.SectionRadioButton.Size = new System.Drawing.Size(70, 19);
            this.SectionRadioButton.TabIndex = 4;
            this.SectionRadioButton.Tag = "3";
            this.SectionRadioButton.Text = "課週報";
            this.SectionRadioButton.UseVisualStyleBackColor = true;
            this.SectionRadioButton.CheckedChanged += new System.EventHandler(this.SectionRadioButton_CheckedChanged);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.SearchButton);
            this.MainPanel.Controls.Add(this.MessageLabel);
            this.MainPanel.Controls.Add(this.InfoListDataGridView);
            this.MainPanel.Location = new System.Drawing.Point(0, 128);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1184, 482);
            this.MainPanel.TabIndex = 9;
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(12, 1);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 30);
            this.SearchButton.TabIndex = 9;
            this.SearchButton.Text = "検索";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // MessageLabel
            // 
            this.MessageLabel.ForeColor = System.Drawing.Color.Red;
            this.MessageLabel.Location = new System.Drawing.Point(140, 5);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(560, 22);
            this.MessageLabel.TabIndex = 0;
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InfoListDataGridView
            // 
            this.InfoListDataGridView.AllowUserToAddRows = false;
            this.InfoListDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoListDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InfoListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.InfoListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InfoListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckBox,
            this.Date,
            this.SectionGroupCode,
            this.GeneralCode,
            this.Category,
            this.CurrentSituation,
            this.FutureSchedule,
            this.OpenClose,
            this.SelectKeyword,
            this.PersonelName,
            this.InputDatetime});
            this.InfoListDataGridView.Location = new System.Drawing.Point(12, 33);
            this.InfoListDataGridView.Name = "InfoListDataGridView";
            this.InfoListDataGridView.RowHeadersVisible = false;
            this.InfoListDataGridView.RowTemplate.Height = 21;
            this.InfoListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InfoListDataGridView.Size = new System.Drawing.Size(1159, 373);
            this.InfoListDataGridView.StandardTab = true;
            this.InfoListDataGridView.TabIndex = 10;
            this.InfoListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InfoListDataGridView_CellClick);
            this.InfoListDataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.InfoListDataGridView_CellPainting);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(12, 605);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 11;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // AllCheckBox
            // 
            this.AllCheckBox.AutoSize = true;
            this.AllCheckBox.Location = new System.Drawing.Point(637, 71);
            this.AllCheckBox.Name = "AllCheckBox";
            this.AllCheckBox.Size = new System.Drawing.Size(15, 14);
            this.AllCheckBox.TabIndex = 9;
            this.AllCheckBox.TabStop = false;
            this.AllCheckBox.UseVisualStyleBackColor = true;
            this.AllCheckBox.Visible = false;
            this.AllCheckBox.CheckedChanged += new System.EventHandler(this.AllCheckBox_CheckedChanged);
            // 
            // CheckBox
            // 
            this.CheckBox.FalseValue = "false";
            this.CheckBox.HeaderText = "";
            this.CheckBox.IndeterminateValue = "CHECK_BOX";
            this.CheckBox.Name = "CheckBox";
            this.CheckBox.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CheckBox.TrueValue = "true";
            this.CheckBox.Width = 30;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "LISTED_DATE";
            dataGridViewCellStyle2.Format = "yyyy/MM/dd";
            dataGridViewCellStyle2.NullValue = null;
            this.Date.DefaultCellStyle = dataGridViewCellStyle2;
            this.Date.HeaderText = "日付";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SectionGroupCode
            // 
            this.SectionGroupCode.DataPropertyName = "SECTION_GROUP_ID";
            this.SectionGroupCode.HeaderText = "担当";
            this.SectionGroupCode.Name = "SectionGroupCode";
            this.SectionGroupCode.ReadOnly = true;
            this.SectionGroupCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GeneralCode
            // 
            this.GeneralCode.DataPropertyName = "GENERAL_CODE";
            this.GeneralCode.HeaderText = "車種";
            this.GeneralCode.Name = "GeneralCode";
            this.GeneralCode.ReadOnly = true;
            this.GeneralCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GeneralCode.Width = 70;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "CATEGORY";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Category.DefaultCellStyle = dataGridViewCellStyle3;
            this.Category.HeaderText = "項目名";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            this.Category.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Category.Width = 150;
            // 
            // CurrentSituation
            // 
            this.CurrentSituation.DataPropertyName = "CURRENT_SITUATION";
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CurrentSituation.DefaultCellStyle = dataGridViewCellStyle4;
            this.CurrentSituation.HeaderText = "現状";
            this.CurrentSituation.Name = "CurrentSituation";
            this.CurrentSituation.ReadOnly = true;
            this.CurrentSituation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CurrentSituation.Width = 280;
            // 
            // FutureSchedule
            // 
            this.FutureSchedule.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FutureSchedule.DataPropertyName = "FUTURE_SCHEDULE";
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FutureSchedule.DefaultCellStyle = dataGridViewCellStyle5;
            this.FutureSchedule.HeaderText = "今後の予定";
            this.FutureSchedule.Name = "FutureSchedule";
            this.FutureSchedule.ReadOnly = true;
            this.FutureSchedule.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // OpenClose
            // 
            this.OpenClose.DataPropertyName = "OPEN_CLOSE";
            this.OpenClose.HeaderText = "OPENCLOSE";
            this.OpenClose.Name = "OpenClose";
            this.OpenClose.ReadOnly = true;
            this.OpenClose.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OpenClose.Width = 50;
            // 
            // SelectKeyword
            // 
            this.SelectKeyword.DataPropertyName = "SELECT_KEYWORD";
            this.SelectKeyword.HeaderText = "選択キーワード";
            this.SelectKeyword.Name = "SelectKeyword";
            this.SelectKeyword.ReadOnly = true;
            this.SelectKeyword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SelectKeyword.Width = 110;
            // 
            // PersonelName
            // 
            this.PersonelName.DataPropertyName = "PERSONEL_NAME";
            this.PersonelName.HeaderText = "編集者";
            this.PersonelName.Name = "PersonelName";
            this.PersonelName.ReadOnly = true;
            this.PersonelName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PersonelName.Width = 110;
            // 
            // InputDatetime
            // 
            this.InputDatetime.DataPropertyName = "INPUT_DATETIME";
            this.InputDatetime.HeaderText = "編集日時";
            this.InputDatetime.Name = "InputDatetime";
            this.InputDatetime.ReadOnly = true;
            this.InputDatetime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InputDatetime.Width = 140;
            // 
            // ReportMaterialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMinSize = new System.Drawing.Size(1160, 570);
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.EntryButton);
            this.Name = "ReportMaterialForm";
            this.Text = "ReportMaterialForm";
            this.Load += new System.EventHandler(this.InfoListForm_Load);
            this.Shown += new System.EventHandler(this.InfoListForm_Shown);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.CondTableLayoutPanel.ResumeLayout(false);
            this.CondTableLayoutPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstDayNullableDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LastDayNullableDateTimePicker)).EndInit();
            this.panel2.ResumeLayout(false);
            this.StatusPanel.ResumeLayout(false);
            this.StatusPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InfoListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel CondTableLayoutPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.ComboBox AffiliationComboBox;
        private System.Windows.Forms.RadioButton SectionGroupRadioButton;
        private System.Windows.Forms.RadioButton DepartmentRadioButton;
        private System.Windows.Forms.RadioButton SectionRadioButton;
        private System.Windows.Forms.TextBox TrialProductionSeasonTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private UC.NullableDateTimePicker LastDayNullableDateTimePicker;
        private System.Windows.Forms.DataGridView InfoListDataGridView;
        private UC.NullableDateTimePicker FirstDayNullableDateTimePicker;
        private System.Windows.Forms.RadioButton FollowListRadioButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox AllCheckBox;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionGroupCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeneralCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentSituation;
        private System.Windows.Forms.DataGridViewTextBoxColumn FutureSchedule;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectKeyword;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputDatetime;
    }
}