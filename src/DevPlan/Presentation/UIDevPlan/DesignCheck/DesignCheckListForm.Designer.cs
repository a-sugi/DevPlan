namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    partial class DesignCheckListForm
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
            this.components = new System.ComponentModel.Container();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager1 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            this.label6 = new System.Windows.Forms.Label();
            this.SearchConditionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DesignCheckNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.OpenEndDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.OpenStartDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.ItemStatusPanel = new System.Windows.Forms.Panel();
            this.StatusCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.StatusOpenCheckBox = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.DesignCheckMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.CompletionScheduleContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DayInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DayCompletionInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SituationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ListDataLabel = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.RowCountLabel = new System.Windows.Forms.Label();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.SearchConditionButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.ScreenFlowLabel = new System.Windows.Forms.Label();
            this.ContentsPanel.SuspendLayout();
            this.SearchConditionTableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpenEndDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenStartDateTimePicker)).BeginInit();
            this.ItemStatusPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DesignCheckMultiRow)).BeginInit();
            this.CompletionScheduleContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.Location = new System.Drawing.Point(858, 616);
            this.RightButton.TabIndex = 170;
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.MainPanel);
            this.ContentsPanel.Controls.Add(this.ClearButton);
            this.ContentsPanel.Controls.Add(this.SearchConditionButton);
            this.ContentsPanel.Controls.Add(this.DeleteButton);
            this.ContentsPanel.Controls.Add(this.ListDataLabel);
            this.ContentsPanel.Controls.Add(this.SearchButton);
            this.ContentsPanel.Controls.Add(this.SearchConditionTableLayoutPanel);
            this.ContentsPanel.Controls.Add(this.label6);
            this.ContentsPanel.Controls.Add(this.ScreenFlowLabel);
            this.ContentsPanel.Controls.Add(this.RowCountLabel);
            this.ContentsPanel.Size = new System.Drawing.Size(984, 584);
            this.ContentsPanel.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "検索条件";
            // 
            // SearchConditionTableLayoutPanel
            // 
            this.SearchConditionTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SearchConditionTableLayoutPanel.ColumnCount = 2;
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 655F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.SearchConditionTableLayoutPanel.Controls.Add(this.panel1, 1, 1);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.panel5, 1, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.ItemStatusPanel, 1, 2);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label13, 0, 2);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label16, 0, 1);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label17, 0, 0);
            this.SearchConditionTableLayoutPanel.Location = new System.Drawing.Point(10, 32);
            this.SearchConditionTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchConditionTableLayoutPanel.Name = "SearchConditionTableLayoutPanel";
            this.SearchConditionTableLayoutPanel.RowCount = 3;
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.SearchConditionTableLayoutPanel.Size = new System.Drawing.Size(686, 95);
            this.SearchConditionTableLayoutPanel.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.DesignCheckNameTextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(123, 33);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 28);
            this.panel1.TabIndex = 60;
            // 
            // DesignCheckNameTextBox
            // 
            this.DesignCheckNameTextBox.Location = new System.Drawing.Point(4, 3);
            this.DesignCheckNameTextBox.MaxLength = 50;
            this.DesignCheckNameTextBox.Name = "DesignCheckNameTextBox";
            this.DesignCheckNameTextBox.Size = new System.Drawing.Size(270, 22);
            this.DesignCheckNameTextBox.TabIndex = 70;
            this.DesignCheckNameTextBox.Tag = "Wide(50);ItemName(設計チェック名)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(280, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(237, 13);
            this.label4.TabIndex = 1038;
            this.label4.Text = "※入力例：ab､AB､A､B　(全/半角区別なし)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.OpenEndDateTimePicker);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.OpenStartDateTimePicker);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(122, 1);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(290, 30);
            this.panel5.TabIndex = 30;
            // 
            // OpenEndDateTimePicker
            // 
            this.OpenEndDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.OpenEndDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.OpenEndDateTimePicker.Location = new System.Drawing.Point(155, 4);
            this.OpenEndDateTimePicker.Name = "OpenEndDateTimePicker";
            this.OpenEndDateTimePicker.Size = new System.Drawing.Size(120, 22);
            this.OpenEndDateTimePicker.TabIndex = 50;
            this.OpenEndDateTimePicker.Tag = "ItemName(開催日To)";
            this.OpenEndDateTimePicker.Value = new System.DateTime(2020, 6, 10, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(129, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "～";
            // 
            // OpenStartDateTimePicker
            // 
            this.OpenStartDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.OpenStartDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.OpenStartDateTimePicker.Location = new System.Drawing.Point(5, 4);
            this.OpenStartDateTimePicker.Name = "OpenStartDateTimePicker";
            this.OpenStartDateTimePicker.Size = new System.Drawing.Size(120, 22);
            this.OpenStartDateTimePicker.TabIndex = 40;
            this.OpenStartDateTimePicker.Tag = "Required;ItemName(開催日)";
            this.OpenStartDateTimePicker.Value = new System.DateTime(2020, 6, 10, 0, 0, 0, 0);
            // 
            // ItemStatusPanel
            // 
            this.ItemStatusPanel.Controls.Add(this.StatusCloseCheckBox);
            this.ItemStatusPanel.Controls.Add(this.StatusOpenCheckBox);
            this.ItemStatusPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ItemStatusPanel.Location = new System.Drawing.Point(122, 63);
            this.ItemStatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ItemStatusPanel.Name = "ItemStatusPanel";
            this.ItemStatusPanel.Size = new System.Drawing.Size(290, 31);
            this.ItemStatusPanel.TabIndex = 80;
            // 
            // StatusCloseCheckBox
            // 
            this.StatusCloseCheckBox.AutoSize = true;
            this.StatusCloseCheckBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F);
            this.StatusCloseCheckBox.Location = new System.Drawing.Point(68, 7);
            this.StatusCloseCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.StatusCloseCheckBox.Name = "StatusCloseCheckBox";
            this.StatusCloseCheckBox.Size = new System.Drawing.Size(62, 19);
            this.StatusCloseCheckBox.TabIndex = 100;
            this.StatusCloseCheckBox.Tag = "close";
            this.StatusCloseCheckBox.Text = "Close";
            this.StatusCloseCheckBox.UseVisualStyleBackColor = true;
            // 
            // StatusOpenCheckBox
            // 
            this.StatusOpenCheckBox.AutoSize = true;
            this.StatusOpenCheckBox.Checked = true;
            this.StatusOpenCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.StatusOpenCheckBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F);
            this.StatusOpenCheckBox.Location = new System.Drawing.Point(8, 7);
            this.StatusOpenCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.StatusOpenCheckBox.Name = "StatusOpenCheckBox";
            this.StatusOpenCheckBox.Size = new System.Drawing.Size(60, 19);
            this.StatusOpenCheckBox.TabIndex = 90;
            this.StatusOpenCheckBox.Tag = "open";
            this.StatusOpenCheckBox.Text = "Open";
            this.StatusOpenCheckBox.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Aquamarine;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(1, 63);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 31);
            this.label13.TabIndex = 29;
            this.label13.Text = "ステータス";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Aquamarine;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(1, 32);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(120, 30);
            this.label16.TabIndex = 1;
            this.label16.Text = "設計チェック名";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Aquamarine;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(1, 1);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 30);
            this.label17.TabIndex = 0;
            this.label17.Text = "開催日";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(10, 134);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 30);
            this.SearchButton.TabIndex = 110;
            this.SearchButton.Text = "検索";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddButton.BackColor = System.Drawing.SystemColors.Control;
            this.AddButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.AddButton.Location = new System.Drawing.Point(9, 616);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(120, 20);
            this.AddButton.TabIndex = 140;
            this.AddButton.Text = "新規作成";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.DesignCheckMultiRow);
            this.MainPanel.Location = new System.Drawing.Point(0, 170);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(981, 411);
            this.MainPanel.TabIndex = 125;
            // 
            // DesignCheckMultiRow
            // 
            this.DesignCheckMultiRow.AllowAutoExtend = true;
            this.DesignCheckMultiRow.AllowUserToAddRows = false;
            this.DesignCheckMultiRow.AllowUserToDeleteRows = false;
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            this.DesignCheckMultiRow.AlternatingRowsDefaultCellStyle = cellStyle1;
            this.DesignCheckMultiRow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DesignCheckMultiRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            cellStyle2.BackColor = System.Drawing.SystemColors.Control;
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle2.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            cellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle2.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.DesignCheckMultiRow.ColumnHeadersDefaultCellStyle = cellStyle2;
            cellStyle3.BackColor = System.Drawing.SystemColors.Control;
            cellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle3.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            cellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle3.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.DesignCheckMultiRow.ColumnHeadersDefaultHeaderCellStyle = cellStyle3;
            cellStyle4.BackColor = System.Drawing.SystemColors.Window;
            cellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle4.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            cellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            cellStyle4.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DesignCheckMultiRow.DefaultCellStyle = cellStyle4;
            this.DesignCheckMultiRow.FreezeLines = new GrapeCity.Win.MultiRow.FreezeLines(new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.None, System.Drawing.Color.Empty));
            this.DesignCheckMultiRow.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.DesignCheckMultiRow.Location = new System.Drawing.Point(9, 3);
            this.DesignCheckMultiRow.MultiSelect = false;
            this.DesignCheckMultiRow.Name = "DesignCheckMultiRow";
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            this.DesignCheckMultiRow.RowsDefaultCellStyle = cellStyle5;
            cellStyle6.BackColor = System.Drawing.SystemColors.Control;
            cellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle6.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle6.Padding = new System.Windows.Forms.Padding(0);
            cellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle6.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DesignCheckMultiRow.RowsDefaultHeaderCellStyle = cellStyle6;
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftLeft)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftRight)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.EndEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CancelEdit)), System.Windows.Forms.Keys.Escape));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ComponentActions.SelectPreviousControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ComponentActions.SelectNextControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ReverseSelectCurrentRow)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToPreviousPage)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToNextPage)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.EndEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CancelEdit)), System.Windows.Forms.Keys.Escape));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            this.DesignCheckMultiRow.ShortcutKeyManager = shortcutKeyManager1;
            this.DesignCheckMultiRow.Size = new System.Drawing.Size(968, 405);
            this.DesignCheckMultiRow.TabIndex = 130;
            this.DesignCheckMultiRow.Tag = "";
            this.DesignCheckMultiRow.Text = "gcMultiRow1";
            this.DesignCheckMultiRow.VerticalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.DesignCheckMultiRow.VerticalScrollMode = GrapeCity.Win.MultiRow.ScrollMode.Pixel;
            this.DesignCheckMultiRow.CellContentClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.DesignCheckMultiRow_CellContentClick);
            this.DesignCheckMultiRow.CellMouseLeave += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.DesignCheckMultiRow_CellMouseLeave);
            // 
            // CompletionScheduleContextMenuStrip
            // 
            this.CompletionScheduleContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DayInputToolStripMenuItem,
            this.DayCompletionInputToolStripMenuItem});
            this.CompletionScheduleContextMenuStrip.Name = "CompletionScheduleContextMenuStrip";
            this.CompletionScheduleContextMenuStrip.Size = new System.Drawing.Size(167, 48);
            // 
            // DayInputToolStripMenuItem
            // 
            this.DayInputToolStripMenuItem.Name = "DayInputToolStripMenuItem";
            this.DayInputToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DayInputToolStripMenuItem.Text = "日付入力";
            // 
            // DayCompletionInputToolStripMenuItem
            // 
            this.DayCompletionInputToolStripMenuItem.Name = "DayCompletionInputToolStripMenuItem";
            this.DayCompletionInputToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DayCompletionInputToolStripMenuItem.Text = "日付付き済み入力";
            // 
            // SituationToolTip
            // 
            this.SituationToolTip.AutoPopDelay = 2147483647;
            this.SituationToolTip.InitialDelay = 0;
            this.SituationToolTip.ReshowDelay = 0;
            this.SituationToolTip.ShowAlways = true;
            // 
            // ListDataLabel
            // 
            this.ListDataLabel.AutoSize = true;
            this.ListDataLabel.ForeColor = System.Drawing.Color.Red;
            this.ListDataLabel.Location = new System.Drawing.Point(132, 10);
            this.ListDataLabel.Name = "ListDataLabel";
            this.ListDataLabel.Size = new System.Drawing.Size(0, 15);
            this.ListDataLabel.TabIndex = 1023;
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Location = new System.Drawing.Point(262, 134);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(120, 30);
            this.DeleteButton.TabIndex = 120;
            this.DeleteButton.Text = "設計ﾁｪｯｸ削除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // RowCountLabel
            // 
            this.RowCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RowCountLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.5F);
            this.RowCountLabel.Location = new System.Drawing.Point(833, 152);
            this.RowCountLabel.Name = "RowCountLabel";
            this.RowCountLabel.Size = new System.Drawing.Size(145, 18);
            this.RowCountLabel.TabIndex = 1025;
            this.RowCountLabel.Text = "表示件数： 999/999 件";
            this.RowCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DownloadButton
            // 
            this.DownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadButton.BackColor = System.Drawing.SystemColors.Control;
            this.DownloadButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.DownloadButton.Location = new System.Drawing.Point(732, 616);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(120, 20);
            this.DownloadButton.TabIndex = 160;
            this.DownloadButton.Text = "Excel出力";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportButton.BackColor = System.Drawing.SystemColors.Control;
            this.ExportButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.ExportButton.Location = new System.Drawing.Point(606, 616);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(120, 20);
            this.ExportButton.TabIndex = 150;
            this.ExportButton.Text = "指摘エクスポート";
            this.ExportButton.UseVisualStyleBackColor = false;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // SearchConditionButton
            // 
            this.SearchConditionButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchConditionButton.Location = new System.Drawing.Point(76, 6);
            this.SearchConditionButton.Name = "SearchConditionButton";
            this.SearchConditionButton.Size = new System.Drawing.Size(20, 23);
            this.SearchConditionButton.TabIndex = 10;
            this.SearchConditionButton.Text = "-";
            this.SearchConditionButton.UseVisualStyleBackColor = false;
            this.SearchConditionButton.Click += new System.EventHandler(this.SearchConditionButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.Control;
            this.ClearButton.Location = new System.Drawing.Point(136, 134);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(120, 30);
            this.ClearButton.TabIndex = 111;
            this.ClearButton.Text = "検索条件ｸﾘｱ";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // ScreenFlowLabel
            // 
            this.ScreenFlowLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScreenFlowLabel.AutoSize = true;
            this.ScreenFlowLabel.ForeColor = System.Drawing.Color.Red;
            this.ScreenFlowLabel.Location = new System.Drawing.Point(492, 153);
            this.ScreenFlowLabel.Name = "ScreenFlowLabel";
            this.ScreenFlowLabel.Size = new System.Drawing.Size(286, 15);
            this.ScreenFlowLabel.TabIndex = 1026;
            this.ScreenFlowLabel.Text = "※設計チェック名のクリックで{0}画面を開きます。";
            this.ScreenFlowLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // DesignCheckListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.AddButton);
            this.Name = "DesignCheckListForm";
            this.Text = "DesignCheckList";
            this.Load += new System.EventHandler(this.DesignCheckListForm_Load);
            this.Shown += new System.EventHandler(this.DesignCheckListForm_Shown);
            this.Controls.SetChildIndex(this.AddButton, 0);
            this.Controls.SetChildIndex(this.DownloadButton, 0);
            this.Controls.SetChildIndex(this.ExportButton, 0);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.SearchConditionTableLayoutPanel.ResumeLayout(false);
            this.SearchConditionTableLayoutPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpenEndDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenStartDateTimePicker)).EndInit();
            this.ItemStatusPanel.ResumeLayout(false);
            this.ItemStatusPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DesignCheckMultiRow)).EndInit();
            this.CompletionScheduleContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel SearchConditionTableLayoutPanel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel ItemStatusPanel;
        private System.Windows.Forms.CheckBox StatusCloseCheckBox;
        private System.Windows.Forms.CheckBox StatusOpenCheckBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ContextMenuStrip CompletionScheduleContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DayInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DayCompletionInputToolStripMenuItem;
        private System.Windows.Forms.ToolTip SituationToolTip;
        private System.Windows.Forms.Panel panel5;
        private UC.NullableDateTimePicker OpenEndDateTimePicker;
        private System.Windows.Forms.Label label8;
        private UC.NullableDateTimePicker OpenStartDateTimePicker;
        private System.Windows.Forms.TextBox DesignCheckNameTextBox;
        private System.Windows.Forms.Label ListDataLabel;
        private System.Windows.Forms.Button DeleteButton;
        private GrapeCity.Win.MultiRow.GcMultiRow DesignCheckMultiRow;
        private System.Windows.Forms.Label RowCountLabel;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button SearchConditionButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Label ScreenFlowLabel;
    }
}