namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    partial class TruckScheduleForm
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
            GrapeCity.Win.CalendarGrid.CalendarListView calendarListView1 = new GrapeCity.Win.CalendarGrid.CalendarListView();
            GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyle calendarConditionalCellStyle1 = new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle1 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle2 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle3 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarTemplate calendarTemplate1 = new GrapeCity.Win.CalendarGrid.CalendarTemplate();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType1 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType2 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType3 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType4 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType5 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            this.YoyakuTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ReservationsNotAcceptedCheckBox = new System.Windows.Forms.CheckBox();
            this.ReservationAvailableCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LegendButton = new System.Windows.Forms.Button();
            this.SearchConditionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.EmptyEndTimeComboBox = new System.Windows.Forms.ComboBox();
            this.EmptyEndDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.EmptyStartTimeComboBox = new System.Windows.Forms.ComboBox();
            this.EmptyStartDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.SearchConditionButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.TruckScheduleCalendarGrid = new GrapeCity.Win.CalendarGrid.GcCalendarGrid();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ContentsPanel.SuspendLayout();
            this.YoyakuTableLayoutPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SearchConditionTableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmptyEndDayDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmptyStartDayDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TruckScheduleCalendarGrid)).BeginInit();
            this.TruckScheduleCalendarGrid.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.label1);
            this.ContentsPanel.Controls.Add(this.MessageLabel);
            this.ContentsPanel.Controls.Add(this.MainPanel);
            this.ContentsPanel.Controls.Add(this.LegendButton);
            this.ContentsPanel.Controls.Add(this.SearchConditionTableLayoutPanel);
            this.ContentsPanel.Controls.Add(this.SearchConditionButton);
            this.ContentsPanel.Controls.Add(this.SearchButton);
            this.ContentsPanel.Controls.Add(this.label2);
            this.ContentsPanel.Controls.Add(this.ClearButton);
            this.ContentsPanel.TabIndex = 0;
            // 
            // YoyakuTableLayoutPanel
            // 
            this.YoyakuTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.YoyakuTableLayoutPanel.ColumnCount = 2;
            this.YoyakuTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.YoyakuTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.YoyakuTableLayoutPanel.Controls.Add(this.panel3, 1, 0);
            this.YoyakuTableLayoutPanel.Controls.Add(this.label4, 0, 0);
            this.YoyakuTableLayoutPanel.Location = new System.Drawing.Point(433, -1);
            this.YoyakuTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.YoyakuTableLayoutPanel.Name = "YoyakuTableLayoutPanel";
            this.YoyakuTableLayoutPanel.RowCount = 1;
            this.YoyakuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.YoyakuTableLayoutPanel.Size = new System.Drawing.Size(314, 32);
            this.YoyakuTableLayoutPanel.TabIndex = 1018;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ReservationsNotAcceptedCheckBox);
            this.panel3.Controls.Add(this.ReservationAvailableCheckBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(73, 1);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(1);
            this.panel3.Size = new System.Drawing.Size(240, 30);
            this.panel3.TabIndex = 0;
            // 
            // ReservationsNotAcceptedCheckBox
            // 
            this.ReservationsNotAcceptedCheckBox.AutoSize = true;
            this.ReservationsNotAcceptedCheckBox.Location = new System.Drawing.Point(98, 6);
            this.ReservationsNotAcceptedCheckBox.Name = "ReservationsNotAcceptedCheckBox";
            this.ReservationsNotAcceptedCheckBox.Size = new System.Drawing.Size(86, 19);
            this.ReservationsNotAcceptedCheckBox.TabIndex = 1;
            this.ReservationsNotAcceptedCheckBox.Text = "予約不可";
            this.ReservationsNotAcceptedCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReservationAvailableCheckBox
            // 
            this.ReservationAvailableCheckBox.AutoSize = true;
            this.ReservationAvailableCheckBox.Location = new System.Drawing.Point(7, 6);
            this.ReservationAvailableCheckBox.Name = "ReservationAvailableCheckBox";
            this.ReservationAvailableCheckBox.Size = new System.Drawing.Size(86, 19);
            this.ReservationAvailableCheckBox.TabIndex = 0;
            this.ReservationAvailableCheckBox.Text = "予約可能";
            this.ReservationAvailableCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Aquamarine;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(1, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "予約可否";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LegendButton
            // 
            this.LegendButton.BackColor = System.Drawing.SystemColors.Control;
            this.LegendButton.Location = new System.Drawing.Point(763, 4);
            this.LegendButton.Name = "LegendButton";
            this.LegendButton.Size = new System.Drawing.Size(88, 26);
            this.LegendButton.TabIndex = 1;
            this.LegendButton.Text = "凡例";
            this.LegendButton.UseVisualStyleBackColor = false;
            this.LegendButton.Click += new System.EventHandler(this.LegendButton_Click);
            // 
            // SearchConditionTableLayoutPanel
            // 
            this.SearchConditionTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SearchConditionTableLayoutPanel.ColumnCount = 2;
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 786F));
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label3, 0, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.panel1, 1, 0);
            this.SearchConditionTableLayoutPanel.Location = new System.Drawing.Point(12, 32);
            this.SearchConditionTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchConditionTableLayoutPanel.Name = "SearchConditionTableLayoutPanel";
            this.SearchConditionTableLayoutPanel.RowCount = 1;
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.SearchConditionTableLayoutPanel.Size = new System.Drawing.Size(839, 32);
            this.SearchConditionTableLayoutPanel.TabIndex = 1013;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "空車期間";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.EmptyEndTimeComboBox);
            this.panel1.Controls.Add(this.EmptyEndDayDateTimePicker);
            this.panel1.Controls.Add(this.YoyakuTableLayoutPanel);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.EmptyStartTimeComboBox);
            this.panel1.Controls.Add(this.EmptyStartDayDateTimePicker);
            this.panel1.Location = new System.Drawing.Point(92, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 30);
            this.panel1.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(348, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 15);
            this.label14.TabIndex = 19;
            this.label14.Text = "時";
            // 
            // EmptyEndTimeComboBox
            // 
            this.EmptyEndTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EmptyEndTimeComboBox.FormattingEnabled = true;
            this.EmptyEndTimeComboBox.Items.AddRange(new object[] {
            "",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22"});
            this.EmptyEndTimeComboBox.Location = new System.Drawing.Point(306, 4);
            this.EmptyEndTimeComboBox.Name = "EmptyEndTimeComboBox";
            this.EmptyEndTimeComboBox.Size = new System.Drawing.Size(41, 23);
            this.EmptyEndTimeComboBox.TabIndex = 3;
            this.EmptyEndTimeComboBox.Tag = "ItemName(空車期間Toの時刻)";
            // 
            // EmptyEndDayDateTimePicker
            // 
            this.EmptyEndDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EmptyEndDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EmptyEndDayDateTimePicker.Location = new System.Drawing.Point(200, 4);
            this.EmptyEndDayDateTimePicker.Name = "EmptyEndDayDateTimePicker";
            this.EmptyEndDayDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.EmptyEndDayDateTimePicker.TabIndex = 2;
            this.EmptyEndDayDateTimePicker.Tag = "ItemName(空車期間Toの日付)";
            this.EmptyEndDayDateTimePicker.Value = new System.DateTime(2020, 6, 2, 0, 0, 0, 0);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(152, 7);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 15);
            this.label15.TabIndex = 18;
            this.label15.Text = "時 ～";
            // 
            // EmptyStartTimeComboBox
            // 
            this.EmptyStartTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EmptyStartTimeComboBox.FormattingEnabled = true;
            this.EmptyStartTimeComboBox.Items.AddRange(new object[] {
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22"});
            this.EmptyStartTimeComboBox.Location = new System.Drawing.Point(109, 4);
            this.EmptyStartTimeComboBox.Name = "EmptyStartTimeComboBox";
            this.EmptyStartTimeComboBox.Size = new System.Drawing.Size(41, 23);
            this.EmptyStartTimeComboBox.TabIndex = 1;
            this.EmptyStartTimeComboBox.Tag = "ItemName(空車期間Fromの時刻)";
            // 
            // EmptyStartDayDateTimePicker
            // 
            this.EmptyStartDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EmptyStartDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EmptyStartDayDateTimePicker.Location = new System.Drawing.Point(3, 4);
            this.EmptyStartDayDateTimePicker.Name = "EmptyStartDayDateTimePicker";
            this.EmptyStartDayDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.EmptyStartDayDateTimePicker.TabIndex = 0;
            this.EmptyStartDayDateTimePicker.Tag = "ItemName(空車期間Fromの日付)";
            this.EmptyStartDayDateTimePicker.Value = new System.DateTime(2020, 6, 2, 0, 0, 0, 0);
            // 
            // SearchConditionButton
            // 
            this.SearchConditionButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchConditionButton.Location = new System.Drawing.Point(86, 6);
            this.SearchConditionButton.Name = "SearchConditionButton";
            this.SearchConditionButton.Size = new System.Drawing.Size(20, 23);
            this.SearchConditionButton.TabIndex = 0;
            this.SearchConditionButton.Text = "-";
            this.SearchConditionButton.UseVisualStyleBackColor = false;
            this.SearchConditionButton.Click += new System.EventHandler(this.SearchConditionButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(13, 67);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(126, 30);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "検索 ／ 更新";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1011;
            this.label2.Text = "検索条件";
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.Control;
            this.ClearButton.Location = new System.Drawing.Point(145, 67);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(120, 30);
            this.ClearButton.TabIndex = 3;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // TruckScheduleCalendarGrid
            // 
            calendarListView1.DayCount = 90;
            this.TruckScheduleCalendarGrid.CalendarView = calendarListView1;
            this.TruckScheduleCalendarGrid.Commands.AddRange(new GrapeCity.Win.CalendarGrid.CalendarGridCommand[] {
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveLeft, System.Windows.Forms.Keys.Left),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveRight, System.Windows.Forms.Keys.Right),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveUp, System.Windows.Forms.Keys.Up),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveDown, System.Windows.Forms.Keys.Down),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToFirstCell, System.Windows.Forms.Keys.Home),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToLastCell, System.Windows.Forms.Keys.End),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToPreviousPage, System.Windows.Forms.Keys.PageUp),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToNextPage, System.Windows.Forms.Keys.Next),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToPreviousCell, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToNextCell, System.Windows.Forms.Keys.Tab),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToPreviousDate, ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                            | System.Windows.Forms.Keys.Tab)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToNextDate, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectAll, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectLeft, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectRight, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectUp, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectDown, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectToFirstCell, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectToLastCell, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectToPreviousPage, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectToNextPage, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.BeginEdit, System.Windows.Forms.Keys.F2),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.ToggleEdit, System.Windows.Forms.Keys.Return),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Clear, System.Windows.Forms.Keys.Delete),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Copy, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Copy, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Cut, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Cut, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Paste, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Paste, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.CancelEdit, System.Windows.Forms.Keys.Escape),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.ShowDropDown, System.Windows.Forms.Keys.F4),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.ShowDropDown, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridMouseCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Zoom(0.1F), GrapeCity.Win.CalendarGrid.CalendarMouseGesture.CtrlWheel),
            new GrapeCity.Win.CalendarGrid.CalendarGridTouchCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToNextPage, GrapeCity.Win.CalendarGrid.CalendarTouchGesture.SwipeLeft),
            new GrapeCity.Win.CalendarGrid.CalendarGridTouchCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToPreviousPage, GrapeCity.Win.CalendarGrid.CalendarTouchGesture.SwipeRight),
            new GrapeCity.Win.CalendarGrid.CalendarGridTouchCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToNextPage, GrapeCity.Win.CalendarGrid.CalendarTouchGesture.SwipeUp),
            new GrapeCity.Win.CalendarGrid.CalendarGridTouchCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToPreviousPage, GrapeCity.Win.CalendarGrid.CalendarTouchGesture.SwipeDown)});
            this.TruckScheduleCalendarGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TruckScheduleCalendarGrid.Font = new System.Drawing.Font("MS UI Gothic", 11.25F);
            this.TruckScheduleCalendarGrid.Location = new System.Drawing.Point(0, 0);
            this.TruckScheduleCalendarGrid.Name = "TruckScheduleCalendarGrid";
            this.TruckScheduleCalendarGrid.Protected = true;
            this.TruckScheduleCalendarGrid.Size = new System.Drawing.Size(1184, 481);
            calendarCellStyle1.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle2.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle3.ForeColor = System.Drawing.Color.Blue;
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle1, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsHoliday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle2, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSunday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle3, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSaturday));
            calendarConditionalCellStyle1.Name = "defaultStyle";
            this.TruckScheduleCalendarGrid.Styles.Add(calendarConditionalCellStyle1);
            this.TruckScheduleCalendarGrid.TabIndex = 1016;
            calendarTemplate1.ColumnHeaderRowCount = 2;
            calendarHeaderCellType1.SupportLocalization = true;
            calendarTemplate1.CornerHeader.GetCell(0, 0).CellType = calendarHeaderCellType1;
            calendarHeaderCellType2.SupportLocalization = true;
            calendarTemplate1.CornerHeader.GetCell(1, 0).CellType = calendarHeaderCellType2;
            calendarTemplate1.ColumnHeader.GetRow(0).Height = 38;
            calendarHeaderCellType3.SupportLocalization = true;
            calendarTemplate1.ColumnHeader.GetCell(0, 0).CellType = calendarHeaderCellType3;
            calendarHeaderCellType4.SupportLocalization = true;
            calendarTemplate1.ColumnHeader.GetCell(1, 0).CellType = calendarHeaderCellType4;
            calendarHeaderCellType5.SupportLocalization = true;
            calendarTemplate1.RowHeader.GetCell(0, 0).CellType = calendarHeaderCellType5;
            this.TruckScheduleCalendarGrid.Template = calendarTemplate1;
            this.TruckScheduleCalendarGrid.TitleFooter.Visible = false;
            this.TruckScheduleCalendarGrid.TitleHeader.AutoHeight = false;
            this.TruckScheduleCalendarGrid.TitleHeader.Height = 40;
            this.TruckScheduleCalendarGrid.VerticalScrollMode = GrapeCity.Win.CalendarGrid.CalendarScrollMode.Pixel;
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.TruckScheduleCalendarGrid);
            this.MainPanel.Location = new System.Drawing.Point(0, 102);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1184, 481);
            this.MainPanel.TabIndex = 1019;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.ForeColor = System.Drawing.Color.Red;
            this.MessageLabel.Location = new System.Drawing.Point(128, 9);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(0, 15);
            this.MessageLabel.TabIndex = 1020;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(857, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 26);
            this.label1.TabIndex = 1021;
            this.label1.Text = "開発部門における運転者は\r\n研実トラックライセンス保持者に限る";
            // 
            // TruckScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Name = "TruckScheduleForm";
            this.Text = "TruckScheduleForm";
            this.Load += new System.EventHandler(this.TruckScheduleForm_Load);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.YoyakuTableLayoutPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.SearchConditionTableLayoutPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmptyEndDayDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmptyStartDayDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TruckScheduleCalendarGrid)).EndInit();
            this.TruckScheduleCalendarGrid.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel YoyakuTableLayoutPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox ReservationsNotAcceptedCheckBox;
        private System.Windows.Forms.CheckBox ReservationAvailableCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button LegendButton;
        private System.Windows.Forms.TableLayoutPanel SearchConditionTableLayoutPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox EmptyEndTimeComboBox;
        private UC.NullableDateTimePicker EmptyEndDayDateTimePicker;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox EmptyStartTimeComboBox;
        private UC.NullableDateTimePicker EmptyStartDayDateTimePicker;
        private System.Windows.Forms.Button SearchConditionButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label MessageLabel;
        internal GrapeCity.Win.CalendarGrid.GcCalendarGrid TruckScheduleCalendarGrid;
        private System.Windows.Forms.Label label1;
    }
}