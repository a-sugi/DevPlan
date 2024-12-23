namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    partial class FixedTimeDaySettingForm
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
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle4 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarTemplate calendarTemplate1 = new GrapeCity.Win.CalendarGrid.CalendarTemplate();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FixedTimeDaySettingForm));
            GrapeCity.Win.CalendarGrid.CalendarMonthView calendarMonthView1 = new GrapeCity.Win.CalendarGrid.CalendarMonthView();
            GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyle calendarConditionalCellStyle2 = new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle5 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle6 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle7 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle8 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarTemplate calendarTemplate2 = new GrapeCity.Win.CalendarGrid.CalendarTemplate();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType1 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType2 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            GrapeCity.Win.CalendarGrid.CalendarCheckBoxCellType calendarCheckBoxCellType1 = new GrapeCity.Win.CalendarGrid.CalendarCheckBoxCellType();
            this.FixedTimeDaySettingCalendarGrid = new GrapeCity.Win.CalendarGrid.GcCalendarGrid();
            this.calendarTitleCaption3 = new GrapeCity.Win.CalendarGrid.CalendarTitleCaption();
            this.GotoTodayCalendarTitleButton = new GrapeCity.Win.CalendarGrid.CalendarTitleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FixedTimeCheckedListBox = new DevPlan.Presentation.UIDevPlan.TruckSchedule.CustomCheckedList();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.UnpaidCheckBox = new System.Windows.Forms.CheckBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.EntryButton = new System.Windows.Forms.Button();
            this.FixedTimeDaySettingInputCalendarGrid = new GrapeCity.Win.CalendarGrid.GcCalendarGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.WorkingDayCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.HolidayCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TuesdayCheckBox = new System.Windows.Forms.CheckBox();
            this.FridayCheckBox = new System.Windows.Forms.CheckBox();
            this.MondayCheckBox = new System.Windows.Forms.CheckBox();
            this.ThursdayCheckBox = new System.Windows.Forms.CheckBox();
            this.WednesdayCheckBox = new System.Windows.Forms.CheckBox();
            this.SundayCheckBox = new System.Windows.Forms.CheckBox();
            this.SaturdayCheckBox = new System.Windows.Forms.CheckBox();
            this.TruckComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NendoComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixedTimeDaySettingCalendarGrid)).BeginInit();
            this.FixedTimeDaySettingCalendarGrid.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixedTimeDaySettingInputCalendarGrid)).BeginInit();
            this.FixedTimeDaySettingInputCalendarGrid.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(882, 613);
            this.CloseButton.TabIndex = 0;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.label3);
            this.ListFormMainPanel.Controls.Add(this.NendoComboBox);
            this.ListFormMainPanel.Controls.Add(this.FixedTimeDaySettingCalendarGrid);
            this.ListFormMainPanel.Controls.Add(this.label1);
            this.ListFormMainPanel.Controls.Add(this.groupBox3);
            this.ListFormMainPanel.Controls.Add(this.TruckComboBox);
            this.ListFormMainPanel.Size = new System.Drawing.Size(997, 603);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.TruckComboBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.groupBox3, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.FixedTimeDaySettingCalendarGrid, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.NendoComboBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label3, 0);
            // 
            // FixedTimeDaySettingCalendarGrid
            // 
            this.FixedTimeDaySettingCalendarGrid.AllowClipboard = false;
            this.FixedTimeDaySettingCalendarGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FixedTimeDaySettingCalendarGrid.AutoZoomMode = GrapeCity.Win.CalendarGrid.AutoZoomMode.Both;
            this.FixedTimeDaySettingCalendarGrid.BackColor = System.Drawing.Color.Gainsboro;
            calendarListView1.DayCount = 365;
            calendarListView1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.FixedTimeDaySettingCalendarGrid.CalendarView = calendarListView1;
            this.FixedTimeDaySettingCalendarGrid.Commands.AddRange(new GrapeCity.Win.CalendarGrid.CalendarGridCommand[] {
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
            this.FixedTimeDaySettingCalendarGrid.Location = new System.Drawing.Point(9, 72);
            this.FixedTimeDaySettingCalendarGrid.Name = "FixedTimeDaySettingCalendarGrid";
            this.FixedTimeDaySettingCalendarGrid.Protected = true;
            this.FixedTimeDaySettingCalendarGrid.ShowGrippersInEditingStatus = false;
            this.FixedTimeDaySettingCalendarGrid.Size = new System.Drawing.Size(397, 525);
            calendarCellStyle1.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle2.ForeColor = System.Drawing.Color.Blue;
            calendarCellStyle3.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            calendarCellStyle4.ForeColor = System.Drawing.Color.Gray;
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle1, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSunday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle2, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSaturday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle3, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsHoliday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle4, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsTrailingDay));
            calendarConditionalCellStyle1.Name = "defaultStyle";
            this.FixedTimeDaySettingCalendarGrid.Styles.Add(calendarConditionalCellStyle1);
            this.FixedTimeDaySettingCalendarGrid.TabIndex = 2;
            calendarTemplate1.ColumnCount = 4;
            calendarTemplate1.ColumnHeaderRowCount = 0;
            calendarTemplate1.RowHeaderColumnCount = 0;
            calendarTemplate1.ColumnHeader.CellStyleName = "defaultStyle";
            calendarTemplate1.Content.CellStyleName = "defaultStyle";
            calendarTemplate1.Content.GetColumn(0).Width = 103;
            calendarTemplate1.Content.GetColumn(1).Width = 51;
            calendarTemplate1.Content.GetColumn(2).Width = 35;
            calendarTemplate1.Content.GetColumn(3).Width = 262;
            calendarTemplate1.Content.GetCell(0, 0).DateFormat = "{YearMonth}";
            calendarTemplate1.Content.GetCell(0, 0).Value = "";
            calendarTemplate1.Content.GetCell(0, 0).CanFocus = true;
            calendarTemplate1.Content.GetCell(0, 0).AutoMergeMode = GrapeCity.Win.CalendarGrid.AutoMergeMode.Vertical;
            calendarTemplate1.Content.GetCell(0, 1).DateFormat = "d日";
            calendarTemplate1.Content.GetCell(0, 1).DateFormatType = GrapeCity.Win.CalendarGrid.CalendarDateFormatType.DotNet;
            calendarTemplate1.Content.GetCell(0, 2).DateFormat = "{AbbreviatedDayOfWeek}";
            this.FixedTimeDaySettingCalendarGrid.Template = calendarTemplate1;
            this.FixedTimeDaySettingCalendarGrid.TitleHeader.Children.Add(this.calendarTitleCaption3);
            this.FixedTimeDaySettingCalendarGrid.TitleHeader.Children.Add(this.GotoTodayCalendarTitleButton);
            // 
            // calendarTitleCaption3
            // 
            this.calendarTitleCaption3.DateFormat = "yyyy年M月";
            this.calendarTitleCaption3.DateFormatType = GrapeCity.Win.CalendarGrid.CalendarDateFormatType.InputMan;
            this.calendarTitleCaption3.Name = "calendarTitleCaption3";
            this.calendarTitleCaption3.Text = "{0}";
            // 
            // GotoTodayCalendarTitleButton
            // 
            this.GotoTodayCalendarTitleButton.ButtonBehavior = GrapeCity.Win.CalendarGrid.CalendarTitleButtonBehavior.GotoToday;
            this.GotoTodayCalendarTitleButton.HorizontalAlignment = GrapeCity.Win.CalendarGrid.CalendarHorizontalAlignment.Right;
            this.GotoTodayCalendarTitleButton.Name = "GotoTodayCalendarTitleButton";
            this.GotoTodayCalendarTitleButton.Text = "今日へ移動";
            this.GotoTodayCalendarTitleButton.ToolTipText = "{0}";
            this.GotoTodayCalendarTitleButton.Click += new System.EventHandler(this.GotoTodayCalendarTitleButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 1026;
            this.label1.Text = "車両名・・";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.DeleteButton);
            this.groupBox3.Controls.Add(this.EntryButton);
            this.groupBox3.Controls.Add(this.FixedTimeDaySettingInputCalendarGrid);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Location = new System.Drawing.Point(419, 72);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(568, 525);
            this.groupBox3.TabIndex = 1027;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "定時間日登録";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FixedTimeCheckedListBox);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.UnpaidCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(393, 265);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(145, 205);
            this.groupBox2.TabIndex = 1021;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "運休登録帯";
            // 
            // FixedTimeCheckedListBox
            // 
            this.FixedTimeCheckedListBox.CheckOnClick = true;
            this.FixedTimeCheckedListBox.DisabledIndices = ((System.Collections.Generic.List<int>)(resources.GetObject("FixedTimeCheckedListBox.DisabledIndices")));
            this.FixedTimeCheckedListBox.FormattingEnabled = true;
            this.FixedTimeCheckedListBox.Location = new System.Drawing.Point(6, 68);
            this.FixedTimeCheckedListBox.Name = "FixedTimeCheckedListBox";
            this.FixedTimeCheckedListBox.Size = new System.Drawing.Size(133, 123);
            this.FixedTimeCheckedListBox.TabIndex = 1018;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(6, 51);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(125, 10);
            this.groupBox4.TabIndex = 1017;
            this.groupBox4.TabStop = false;
            // 
            // UnpaidCheckBox
            // 
            this.UnpaidCheckBox.AutoSize = true;
            this.UnpaidCheckBox.Location = new System.Drawing.Point(8, 28);
            this.UnpaidCheckBox.Name = "UnpaidCheckBox";
            this.UnpaidCheckBox.Size = new System.Drawing.Size(121, 19);
            this.UnpaidCheckBox.TabIndex = 0;
            this.UnpaidCheckBox.Text = "全時間帯 運休";
            this.UnpaidCheckBox.UseVisualStyleBackColor = true;
            this.UnpaidCheckBox.CheckedChanged += new System.EventHandler(this.UnpaidCheckBox_CheckedChanged);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Location = new System.Drawing.Point(482, 476);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(71, 30);
            this.DeleteButton.TabIndex = 3;
            this.DeleteButton.Text = "解除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EntryButton
            // 
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(399, 476);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(71, 30);
            this.EntryButton.TabIndex = 2;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // FixedTimeDaySettingInputCalendarGrid
            // 
            this.FixedTimeDaySettingInputCalendarGrid.AllowClipboard = false;
            this.FixedTimeDaySettingInputCalendarGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FixedTimeDaySettingInputCalendarGrid.AutoZoomMode = GrapeCity.Win.CalendarGrid.AutoZoomMode.Both;
            calendarMonthView1.AllowTrailingDayPageScroll = false;
            calendarMonthView1.Dimensions = new System.Drawing.Size(1, 12);
            calendarMonthView1.ShowTrailingDays = false;
            this.FixedTimeDaySettingInputCalendarGrid.CalendarView = calendarMonthView1;
            this.FixedTimeDaySettingInputCalendarGrid.Commands.AddRange(new GrapeCity.Win.CalendarGrid.CalendarGridCommand[] {
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
            this.FixedTimeDaySettingInputCalendarGrid.Location = new System.Drawing.Point(6, 32);
            this.FixedTimeDaySettingInputCalendarGrid.Name = "FixedTimeDaySettingInputCalendarGrid";
            this.FixedTimeDaySettingInputCalendarGrid.Size = new System.Drawing.Size(373, 487);
            calendarCellStyle5.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle6.ForeColor = System.Drawing.Color.Blue;
            calendarCellStyle7.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            calendarCellStyle8.ForeColor = System.Drawing.Color.Gray;
            calendarConditionalCellStyle2.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle5, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSunday));
            calendarConditionalCellStyle2.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle6, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSaturday));
            calendarConditionalCellStyle2.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle7, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsHoliday));
            calendarConditionalCellStyle2.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle8, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsTrailingDay));
            calendarConditionalCellStyle2.Name = "defaultStyle";
            this.FixedTimeDaySettingInputCalendarGrid.Styles.Add(calendarConditionalCellStyle2);
            this.FixedTimeDaySettingInputCalendarGrid.TabIndex = 1012;
            calendarTemplate2.ColumnHeaderRowCount = 2;
            calendarTemplate2.RowCount = 2;
            calendarTemplate2.RowHeaderColumnCount = 0;
            calendarTemplate2.ColumnHeader.CellStyleName = "defaultStyle";
            calendarTemplate2.ColumnHeader.GetRow(0).Height = 23;
            calendarTemplate2.ColumnHeader.GetRow(1).Height = 23;
            calendarHeaderCellType1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            calendarHeaderCellType1.SupportLocalization = true;
            calendarTemplate2.ColumnHeader.GetCell(0, 0).CellType = calendarHeaderCellType1;
            calendarTemplate2.ColumnHeader.GetCell(0, 0).DateFormat = "{YearMonth}";
            calendarTemplate2.ColumnHeader.GetCell(0, 0).AutoMergeMode = GrapeCity.Win.CalendarGrid.AutoMergeMode.Horizontal;
            calendarTemplate2.ColumnHeader.GetCell(0, 0).CellStyle.BackColor = System.Drawing.Color.Yellow;
            calendarTemplate2.ColumnHeader.GetCell(0, 0).CellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            calendarHeaderCellType2.SupportLocalization = true;
            calendarTemplate2.ColumnHeader.GetCell(1, 0).CellType = calendarHeaderCellType2;
            calendarTemplate2.ColumnHeader.GetCell(1, 0).DateFormat = "{AbbreviatedDayOfWeek}";
            calendarTemplate2.ColumnHeader.GetCell(1, 0).CellStyle.Alignment = GrapeCity.Win.CalendarGrid.CalendarGridContentAlignment.MiddleCenter;
            calendarTemplate2.Content.CellStyleName = "defaultStyle";
            calendarTemplate2.Content.GetColumn(0).Width = 49;
            calendarTemplate2.Content.GetRow(1).Height = 28;
            calendarTemplate2.Content.GetCell(0, 0).DateFormat = "%d日";
            calendarTemplate2.Content.GetCell(0, 0).DateFormatType = GrapeCity.Win.CalendarGrid.CalendarDateFormatType.DotNet;
            calendarTemplate2.Content.GetCell(0, 0).CellStyle.Alignment = GrapeCity.Win.CalendarGrid.CalendarGridContentAlignment.MiddleCenter;
            calendarCheckBoxCellType1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarCheckBoxCellType1.SupportLocalization = true;
            calendarTemplate2.Content.GetCell(1, 0).CellType = calendarCheckBoxCellType1;
            calendarTemplate2.Content.GetCell(1, 0).Value = false;
            calendarTemplate2.Content.GetCell(1, 0).CellStyle.Alignment = GrapeCity.Win.CalendarGrid.CalendarGridContentAlignment.MiddleCenter;
            this.FixedTimeDaySettingInputCalendarGrid.Template = calendarTemplate2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.WorkingDayCheckBox);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.HolidayCheckBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TuesdayCheckBox);
            this.groupBox1.Controls.Add(this.FridayCheckBox);
            this.groupBox1.Controls.Add(this.MondayCheckBox);
            this.groupBox1.Controls.Add(this.ThursdayCheckBox);
            this.groupBox1.Controls.Add(this.WednesdayCheckBox);
            this.groupBox1.Controls.Add(this.SundayCheckBox);
            this.groupBox1.Controls.Add(this.SaturdayCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(393, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 210);
            this.groupBox1.TabIndex = 1017;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日付選択";
            // 
            // WorkingDayCheckBox
            // 
            this.WorkingDayCheckBox.AutoSize = true;
            this.WorkingDayCheckBox.Location = new System.Drawing.Point(6, 21);
            this.WorkingDayCheckBox.Name = "WorkingDayCheckBox";
            this.WorkingDayCheckBox.Size = new System.Drawing.Size(139, 19);
            this.WorkingDayCheckBox.TabIndex = 0;
            this.WorkingDayCheckBox.Text = "稼働日を全て選択";
            this.WorkingDayCheckBox.UseVisualStyleBackColor = true;
            this.WorkingDayCheckBox.CheckedChanged += new System.EventHandler(this.WorkingDayCheckBox_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(6, 64);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(140, 10);
            this.groupBox5.TabIndex = 1017;
            this.groupBox5.TabStop = false;
            // 
            // HolidayCheckBox
            // 
            this.HolidayCheckBox.AutoSize = true;
            this.HolidayCheckBox.Location = new System.Drawing.Point(6, 46);
            this.HolidayCheckBox.Name = "HolidayCheckBox";
            this.HolidayCheckBox.Size = new System.Drawing.Size(154, 19);
            this.HolidayCheckBox.TabIndex = 1;
            this.HolidayCheckBox.Text = "非稼働日を全て選択";
            this.HolidayCheckBox.UseVisualStyleBackColor = true;
            this.HolidayCheckBox.CheckedChanged += new System.EventHandler(this.HolidayCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "曜日を選択";
            // 
            // TuesdayCheckBox
            // 
            this.TuesdayCheckBox.AutoSize = true;
            this.TuesdayCheckBox.Location = new System.Drawing.Point(6, 133);
            this.TuesdayCheckBox.Name = "TuesdayCheckBox";
            this.TuesdayCheckBox.Size = new System.Drawing.Size(71, 19);
            this.TuesdayCheckBox.TabIndex = 1;
            this.TuesdayCheckBox.Text = "火曜日";
            this.TuesdayCheckBox.UseVisualStyleBackColor = true;
            this.TuesdayCheckBox.CheckedChanged += new System.EventHandler(this.TuesdayCheckBox_CheckedChanged);
            // 
            // FridayCheckBox
            // 
            this.FridayCheckBox.AutoSize = true;
            this.FridayCheckBox.Location = new System.Drawing.Point(83, 133);
            this.FridayCheckBox.Name = "FridayCheckBox";
            this.FridayCheckBox.Size = new System.Drawing.Size(71, 19);
            this.FridayCheckBox.TabIndex = 4;
            this.FridayCheckBox.Text = "金曜日";
            this.FridayCheckBox.UseVisualStyleBackColor = true;
            this.FridayCheckBox.CheckedChanged += new System.EventHandler(this.FridayCheckBox_CheckedChanged);
            // 
            // MondayCheckBox
            // 
            this.MondayCheckBox.AutoSize = true;
            this.MondayCheckBox.Location = new System.Drawing.Point(6, 108);
            this.MondayCheckBox.Name = "MondayCheckBox";
            this.MondayCheckBox.Size = new System.Drawing.Size(71, 19);
            this.MondayCheckBox.TabIndex = 0;
            this.MondayCheckBox.Text = "月曜日";
            this.MondayCheckBox.UseVisualStyleBackColor = true;
            this.MondayCheckBox.CheckedChanged += new System.EventHandler(this.MondayCheckBox_CheckedChanged);
            // 
            // ThursdayCheckBox
            // 
            this.ThursdayCheckBox.AutoSize = true;
            this.ThursdayCheckBox.Location = new System.Drawing.Point(83, 108);
            this.ThursdayCheckBox.Name = "ThursdayCheckBox";
            this.ThursdayCheckBox.Size = new System.Drawing.Size(71, 19);
            this.ThursdayCheckBox.TabIndex = 3;
            this.ThursdayCheckBox.Text = "木曜日";
            this.ThursdayCheckBox.UseVisualStyleBackColor = true;
            this.ThursdayCheckBox.CheckedChanged += new System.EventHandler(this.ThursdayCheckBox_CheckedChanged);
            // 
            // WednesdayCheckBox
            // 
            this.WednesdayCheckBox.AutoSize = true;
            this.WednesdayCheckBox.Location = new System.Drawing.Point(6, 158);
            this.WednesdayCheckBox.Name = "WednesdayCheckBox";
            this.WednesdayCheckBox.Size = new System.Drawing.Size(71, 19);
            this.WednesdayCheckBox.TabIndex = 2;
            this.WednesdayCheckBox.Text = "水曜日";
            this.WednesdayCheckBox.UseVisualStyleBackColor = true;
            this.WednesdayCheckBox.CheckedChanged += new System.EventHandler(this.WednesdayCheckBox_CheckedChanged);
            // 
            // SundayCheckBox
            // 
            this.SundayCheckBox.AutoSize = true;
            this.SundayCheckBox.Location = new System.Drawing.Point(83, 187);
            this.SundayCheckBox.Name = "SundayCheckBox";
            this.SundayCheckBox.Size = new System.Drawing.Size(71, 19);
            this.SundayCheckBox.TabIndex = 2;
            this.SundayCheckBox.Text = "日曜日";
            this.SundayCheckBox.UseVisualStyleBackColor = true;
            this.SundayCheckBox.CheckedChanged += new System.EventHandler(this.SundayCheckBox_CheckedChanged);
            // 
            // SaturdayCheckBox
            // 
            this.SaturdayCheckBox.AutoSize = true;
            this.SaturdayCheckBox.Location = new System.Drawing.Point(6, 187);
            this.SaturdayCheckBox.Name = "SaturdayCheckBox";
            this.SaturdayCheckBox.Size = new System.Drawing.Size(71, 19);
            this.SaturdayCheckBox.TabIndex = 2;
            this.SaturdayCheckBox.Text = "土曜日";
            this.SaturdayCheckBox.UseVisualStyleBackColor = true;
            this.SaturdayCheckBox.CheckedChanged += new System.EventHandler(this.SaturdayCheckBox_CheckedChanged);
            // 
            // TruckComboBox
            // 
            this.TruckComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TruckComboBox.FormattingEnabled = true;
            this.TruckComboBox.Location = new System.Drawing.Point(254, 43);
            this.TruckComboBox.Name = "TruckComboBox";
            this.TruckComboBox.Size = new System.Drawing.Size(268, 23);
            this.TruckComboBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 15);
            this.label3.TabIndex = 1029;
            this.label3.Text = "対象年度・・";
            // 
            // NendoComboBox
            // 
            this.NendoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NendoComboBox.FormattingEnabled = true;
            this.NendoComboBox.Location = new System.Drawing.Point(97, 43);
            this.NendoComboBox.Name = "NendoComboBox";
            this.NendoComboBox.Size = new System.Drawing.Size(66, 23);
            this.NendoComboBox.TabIndex = 1028;
            // 
            // FixedTimeDaySettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1008, 647);
            this.Name = "FixedTimeDaySettingForm";
            this.Text = "FixedTimeDaySettingForm";
            this.Load += new System.EventHandler(this.FixedTimeDaySettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixedTimeDaySettingCalendarGrid)).EndInit();
            this.FixedTimeDaySettingCalendarGrid.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixedTimeDaySettingInputCalendarGrid)).EndInit();
            this.FixedTimeDaySettingInputCalendarGrid.ResumeLayout(false);
            this.FixedTimeDaySettingInputCalendarGrid.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GrapeCity.Win.CalendarGrid.GcCalendarGrid FixedTimeDaySettingCalendarGrid;
        private GrapeCity.Win.CalendarGrid.CalendarTitleCaption calendarTitleCaption3;
        private GrapeCity.Win.CalendarGrid.CalendarTitleButton GotoTodayCalendarTitleButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox HolidayCheckBox;
        private System.Windows.Forms.CheckBox WorkingDayCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox UnpaidCheckBox;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button EntryButton;
        private GrapeCity.Win.CalendarGrid.GcCalendarGrid FixedTimeDaySettingInputCalendarGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox TuesdayCheckBox;
        private System.Windows.Forms.CheckBox FridayCheckBox;
        private System.Windows.Forms.CheckBox MondayCheckBox;
        private System.Windows.Forms.CheckBox ThursdayCheckBox;
        private System.Windows.Forms.CheckBox WednesdayCheckBox;
        private System.Windows.Forms.CheckBox SundayCheckBox;
        private System.Windows.Forms.CheckBox SaturdayCheckBox;
        private System.Windows.Forms.ComboBox TruckComboBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox NendoComboBox;
        private CustomCheckedList FixedTimeCheckedListBox;
    }
}