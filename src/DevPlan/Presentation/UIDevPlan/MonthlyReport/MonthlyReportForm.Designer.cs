namespace DevPlan.Presentation.UIDevPlan.MonthlyReport
{
    partial class MonthlyReportForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.CondTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LastDayNullableDateTimePicker = new UC.NullableDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.AffiliationLabel = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.AffiliationComboBox = new System.Windows.Forms.ComboBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.AddButton = new System.Windows.Forms.Button();
            this.SortButton = new System.Windows.Forms.Button();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.MonthlyReportDataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ManagerNameLabel = new System.Windows.Forms.Label();
            this.CompareDataGridView = new System.Windows.Forms.DataGridView();
            this.MonthlyReportLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.PreviewButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.EntryButton = new System.Windows.Forms.Button();
            this.SumCheckBox = new System.Windows.Forms.CheckBox();
            this.GridBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentSituation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FutureSchedule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SortNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonthFirstDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SortNoBefore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryCompare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentSituationCompare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FutureScheduleCompare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDCompare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SortNoCompare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonthFirstDayCompare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContentsPanel.SuspendLayout();
            this.CondTableLayoutPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonthlyReportDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompareDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.SumCheckBox);
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
            this.label2.TabIndex = 0;
            this.label2.Text = "検索条件";
            // 
            // CondTableLayoutPanel
            // 
            this.CondTableLayoutPanel.AutoSize = true;
            this.CondTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.CondTableLayoutPanel.ColumnCount = 4;
            this.CondTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.CondTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.CondTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.CondTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 233F));
            this.CondTableLayoutPanel.Controls.Add(this.panel2, 3, 0);
            this.CondTableLayoutPanel.Controls.Add(this.label5, 2, 0);
            this.CondTableLayoutPanel.Controls.Add(this.AffiliationLabel, 0, 0);
            this.CondTableLayoutPanel.Controls.Add(this.panel7, 1, 0);
            this.CondTableLayoutPanel.Location = new System.Drawing.Point(12, 29);
            this.CondTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CondTableLayoutPanel.Name = "CondTableLayoutPanel";
            this.CondTableLayoutPanel.RowCount = 1;
            this.CondTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.CondTableLayoutPanel.Size = new System.Drawing.Size(678, 35);
            this.CondTableLayoutPanel.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.LastDayNullableDateTimePicker);
            this.panel2.Location = new System.Drawing.Point(444, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(219, 29);
            this.panel2.TabIndex = 2;
            // 
            // LastDayNullableDateTimePicker
            // 
            this.LastDayNullableDateTimePicker.CustomFormat = "yyyy/MM";
            this.LastDayNullableDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.LastDayNullableDateTimePicker.Location = new System.Drawing.Point(58, 5);
            this.LastDayNullableDateTimePicker.Name = "LastDayNullableDateTimePicker";
            this.LastDayNullableDateTimePicker.Size = new System.Drawing.Size(114, 22);
            this.LastDayNullableDateTimePicker.TabIndex = 2;
            this.LastDayNullableDateTimePicker.CloseUp += new System.EventHandler(this.LastDayNullableDateTimePicker_CloseUp);
            this.LastDayNullableDateTimePicker.Validated += new System.EventHandler(this.LastDayNullableDateTimePicker_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Aquamarine;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(323, 1);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 33);
            this.label5.TabIndex = 0;
            this.label5.Text = "指定月";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AffiliationLabel
            // 
            this.AffiliationLabel.AutoSize = true;
            this.AffiliationLabel.BackColor = System.Drawing.Color.Aquamarine;
            this.AffiliationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AffiliationLabel.Location = new System.Drawing.Point(1, 1);
            this.AffiliationLabel.Margin = new System.Windows.Forms.Padding(0);
            this.AffiliationLabel.Name = "AffiliationLabel";
            this.AffiliationLabel.Size = new System.Drawing.Size(120, 33);
            this.AffiliationLabel.TabIndex = 0;
            this.AffiliationLabel.Text = "課名";
            this.AffiliationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel7.Controls.Add(this.AffiliationComboBox);
            this.panel7.Location = new System.Drawing.Point(122, 1);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(200, 29);
            this.panel7.TabIndex = 1;
            // 
            // AffiliationComboBox
            // 
            this.AffiliationComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AffiliationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AffiliationComboBox.FormattingEnabled = true;
            this.AffiliationComboBox.Location = new System.Drawing.Point(3, 3);
            this.AffiliationComboBox.Name = "AffiliationComboBox";
            this.AffiliationComboBox.Size = new System.Drawing.Size(194, 23);
            this.AffiliationComboBox.TabIndex = 1;
            this.AffiliationComboBox.SelectedIndexChanged += new System.EventHandler(this.AffiliationComboBox_SelectedIndexChanged);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.AddButton);
            this.MainPanel.Controls.Add(this.SortButton);
            this.MainPanel.Controls.Add(this.MessageLabel);
            this.MainPanel.Controls.Add(this.DeleteButton);
            this.MainPanel.Controls.Add(this.MonthlyReportDataGridView);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Controls.Add(this.SearchButton);
            this.MainPanel.Location = new System.Drawing.Point(0, 64);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1184, 470);
            this.MainPanel.TabIndex = 8;
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.SystemColors.Control;
            this.AddButton.Location = new System.Drawing.Point(139, 1);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(120, 30);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "行追加";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // SortButton
            // 
            this.SortButton.BackColor = System.Drawing.SystemColors.Control;
            this.SortButton.Location = new System.Drawing.Point(505, 9);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(200, 22);
            this.SortButton.TabIndex = 0;
            this.SortButton.TabStop = false;
            this.SortButton.Text = "項目(ｿｰﾄ)";
            this.SortButton.UseVisualStyleBackColor = false;
            this.SortButton.Visible = false;
            // 
            // MessageLabel
            // 
            this.MessageLabel.Location = new System.Drawing.Point(390, 5);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(734, 22);
            this.MessageLabel.TabIndex = 0;
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Location = new System.Drawing.Point(265, 1);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(120, 30);
            this.DeleteButton.TabIndex = 3;
            this.DeleteButton.Text = "行削除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // MonthlyReportDataGridView
            // 
            this.MonthlyReportDataGridView.AllowDrop = true;
            this.MonthlyReportDataGridView.AllowUserToAddRows = false;
            this.MonthlyReportDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MonthlyReportDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MonthlyReportDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.MonthlyReportDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MonthlyReportDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Category,
            this.CurrentSituation,
            this.FutureSchedule,
            this.ID,
            this.SortNo,
            this.MonthFirstDay,
            this.SortNoBefore});
            this.MonthlyReportDataGridView.Location = new System.Drawing.Point(18, 74);
            this.MonthlyReportDataGridView.MultiSelect = false;
            this.MonthlyReportDataGridView.Name = "MonthlyReportDataGridView";
            this.MonthlyReportDataGridView.RowHeadersVisible = false;
            this.MonthlyReportDataGridView.RowTemplate.Height = 21;
            this.MonthlyReportDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MonthlyReportDataGridView.Size = new System.Drawing.Size(1146, 392);
            this.MonthlyReportDataGridView.TabIndex = 4;
            this.MonthlyReportDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MonthlyReportDataGridView_CellClick);
            this.MonthlyReportDataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.MonthlyReportDataGridView_CellPainting);
            this.MonthlyReportDataGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.WeeklyReportDataGridView_DragDrop);
            this.MonthlyReportDataGridView.DragOver += new System.Windows.Forms.DragEventHandler(this.WeeklyReportDataGridView_DragOver);
            this.MonthlyReportDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WeeklyReportDataGridView_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ManagerNameLabel);
            this.panel1.Controls.Add(this.CompareDataGridView);
            this.panel1.Controls.Add(this.MonthlyReportLabel);
            this.panel1.Location = new System.Drawing.Point(12, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1160, 437);
            this.panel1.TabIndex = 25;
            // 
            // ManagerNameLabel
            // 
            this.ManagerNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ManagerNameLabel.Location = new System.Drawing.Point(900, 6);
            this.ManagerNameLabel.Name = "ManagerNameLabel";
            this.ManagerNameLabel.Size = new System.Drawing.Size(250, 30);
            this.ManagerNameLabel.TabIndex = 29;
            this.ManagerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CompareDataGridView
            // 
            this.CompareDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CompareDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.CompareDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CompareDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CategoryCompare,
            this.CurrentSituationCompare,
            this.FutureScheduleCompare,
            this.IDCompare,
            this.SortNoCompare,
            this.MonthFirstDayCompare});
            this.CompareDataGridView.Location = new System.Drawing.Point(3, 10);
            this.CompareDataGridView.Name = "CompareDataGridView";
            this.CompareDataGridView.RowHeadersVisible = false;
            this.CompareDataGridView.RowTemplate.Height = 21;
            this.CompareDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CompareDataGridView.Size = new System.Drawing.Size(83, 24);
            this.CompareDataGridView.TabIndex = 0;
            this.CompareDataGridView.TabStop = false;
            this.CompareDataGridView.Visible = false;
            // 
            // MonthlyReportLabel
            // 
            this.MonthlyReportLabel.Location = new System.Drawing.Point(155, 6);
            this.MonthlyReportLabel.Name = "MonthlyReportLabel";
            this.MonthlyReportLabel.Size = new System.Drawing.Size(727, 30);
            this.MonthlyReportLabel.TabIndex = 0;
            this.MonthlyReportLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(13, 1);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 30);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "行読込";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // PreviewButton
            // 
            this.PreviewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviewButton.BackColor = System.Drawing.SystemColors.Control;
            this.PreviewButton.Location = new System.Drawing.Point(800, 604);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.Size = new System.Drawing.Size(120, 30);
            this.PreviewButton.TabIndex = 9;
            this.PreviewButton.Text = "プレビュー";
            this.PreviewButton.UseVisualStyleBackColor = false;
            this.PreviewButton.Visible = false;
            // 
            // DownloadButton
            // 
            this.DownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadButton.BackColor = System.Drawing.SystemColors.Control;
            this.DownloadButton.Location = new System.Drawing.Point(926, 604);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(120, 30);
            this.DownloadButton.TabIndex = 10;
            this.DownloadButton.Text = "ダウンロード";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(12, 604);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 8;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // SumCheckBox
            // 
            this.SumCheckBox.AutoSize = true;
            this.SumCheckBox.Location = new System.Drawing.Point(716, 36);
            this.SumCheckBox.Name = "SumCheckBox";
            this.SumCheckBox.Size = new System.Drawing.Size(189, 19);
            this.SumCheckBox.TabIndex = 2;
            this.SumCheckBox.Text = "各課月報を一つにまとめる。";
            this.SumCheckBox.UseVisualStyleBackColor = true;
            this.SumCheckBox.CheckedChanged += new System.EventHandler(this.SumCheckBox_CheckedChanged);
            // 
            // GridBackgroundWorker
            // 
            this.GridBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GridBackgroundWorker_DoWork);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn1.HeaderText = "項目";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 200;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.HeaderText = "進度状況";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 1000;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 400;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn3.HeaderText = "見通し・対応策・日程";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 1000;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "ID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "ソート番号";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "対象月";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "ソート前インデックス";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "進度状況";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 200;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "見通し・対応策・日程";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 200;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "ID";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "ソート番号";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "対象月";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // Category
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Category.DefaultCellStyle = dataGridViewCellStyle2;
            this.Category.HeaderText = "項目";
            this.Category.MaxInputLength = 200;
            this.Category.Name = "Category";
            this.Category.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Category.Width = 250;
            // 
            // CurrentSituation
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CurrentSituation.DefaultCellStyle = dataGridViewCellStyle3;
            this.CurrentSituation.HeaderText = "進度状況";
            this.CurrentSituation.MaxInputLength = 1000;
            this.CurrentSituation.Name = "CurrentSituation";
            this.CurrentSituation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CurrentSituation.Width = 400;
            // 
            // FutureSchedule
            // 
            this.FutureSchedule.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FutureSchedule.DefaultCellStyle = dataGridViewCellStyle4;
            this.FutureSchedule.HeaderText = "見通し・対応策・日程";
            this.FutureSchedule.MaxInputLength = 1000;
            this.FutureSchedule.Name = "FutureSchedule";
            this.FutureSchedule.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID.Visible = false;
            // 
            // SortNo
            // 
            this.SortNo.HeaderText = "ソート番号";
            this.SortNo.Name = "SortNo";
            this.SortNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SortNo.Visible = false;
            // 
            // MonthFirstDay
            // 
            this.MonthFirstDay.HeaderText = "対象月";
            this.MonthFirstDay.Name = "MonthFirstDay";
            this.MonthFirstDay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MonthFirstDay.Visible = false;
            // 
            // SortNoBefore
            // 
            this.SortNoBefore.HeaderText = "ソート前インデックス";
            this.SortNoBefore.Name = "SortNoBefore";
            this.SortNoBefore.Visible = false;
            // 
            // CategoryCompare
            // 
            this.CategoryCompare.HeaderText = "";
            this.CategoryCompare.Name = "CategoryCompare";
            // 
            // CurrentSituationCompare
            // 
            this.CurrentSituationCompare.HeaderText = "進度状況";
            this.CurrentSituationCompare.Name = "CurrentSituationCompare";
            this.CurrentSituationCompare.Width = 200;
            // 
            // FutureScheduleCompare
            // 
            this.FutureScheduleCompare.HeaderText = "見通し・対応策・日程";
            this.FutureScheduleCompare.Name = "FutureScheduleCompare";
            this.FutureScheduleCompare.Width = 200;
            // 
            // IDCompare
            // 
            this.IDCompare.HeaderText = "ID";
            this.IDCompare.Name = "IDCompare";
            // 
            // SortNoCompare
            // 
            this.SortNoCompare.HeaderText = "ソート番号";
            this.SortNoCompare.Name = "SortNoCompare";
            // 
            // MonthFirstDayCompare
            // 
            this.MonthFirstDayCompare.HeaderText = "対象月";
            this.MonthFirstDayCompare.Name = "MonthFirstDayCompare";
            // 
            // MonthlyReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMinSize = new System.Drawing.Size(1160, 570);
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.PreviewButton);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.EntryButton);
            this.Name = "MonthlyReportForm";
            this.Text = "MonthlyReportForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MonthlyReportForm_FormClosing);
            this.Load += new System.EventHandler(this.MonthlyReportForm_Load);
            this.Shown += new System.EventHandler(this.MonthlyReportForm_Shown);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.DownloadButton, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.Controls.SetChildIndex(this.PreviewButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.CondTableLayoutPanel.ResumeLayout(false);
            this.CondTableLayoutPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MonthlyReportDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CompareDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel CondTableLayoutPanel;
        private System.Windows.Forms.Label AffiliationLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.ComboBox AffiliationComboBox;
        private System.Windows.Forms.Button PreviewButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView MonthlyReportDataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label MonthlyReportLabel;
        private System.Windows.Forms.DataGridView CompareDataGridView;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.CheckBox SumCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryCompare;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentSituationCompare;
        private System.Windows.Forms.DataGridViewTextBoxColumn FutureScheduleCompare;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDCompare;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortNoCompare;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonthFirstDayCompare;
        private System.Windows.Forms.Button SortButton;
        private System.Windows.Forms.Label ManagerNameLabel;
        private System.ComponentModel.BackgroundWorker GridBackgroundWorker;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentSituation;
        private System.Windows.Forms.DataGridViewTextBoxColumn FutureSchedule;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonthFirstDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortNoBefore;
        private UC.NullableDateTimePicker  LastDayNullableDateTimePicker;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
    }
}