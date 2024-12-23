namespace DevPlan.Presentation.UIDevPlan.OuterCar
{
    partial class OuterCarForm
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
            this.calendarTitleCaption1 = new GrapeCity.Win.CalendarGrid.CalendarTitleCaption();
            this.SearchConditionButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.OuterCarCalendarGrid = new GrapeCity.Win.CalendarGrid.GcCalendarGrid();
            this.ClearButton = new System.Windows.Forms.Button();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.ConditionRegistButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ExcelPrintButton = new System.Windows.Forms.Button();
            this.SearchConditionLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ItemStatusPanel = new System.Windows.Forms.Panel();
            this.StatusCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.StatusOpenCheckBox = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.EmptyEndTimeComboBox = new System.Windows.Forms.ComboBox();
            this.EmptyEndDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.EmptyStartTimeComboBox = new System.Windows.Forms.ComboBox();
            this.EmptyStartDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.CarGroupComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.SkcLinkLabel = new System.Windows.Forms.LinkLabel();
            this.GunmaLinkLabel = new System.Windows.Forms.LinkLabel();
            this.LegendButton = new System.Windows.Forms.Button();
            this.TruckScheduleButton = new System.Windows.Forms.Button();
            this.TruckScheduleCheckBox = new System.Windows.Forms.CheckBox();
            this.ContentsPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OuterCarCalendarGrid)).BeginInit();
            this.OuterCarCalendarGrid.SuspendLayout();
            this.SearchConditionLayoutPanel.SuspendLayout();
            this.ItemStatusPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmptyEndDayDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmptyStartDayDateTimePicker)).BeginInit();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.TruckScheduleCheckBox);
            this.ContentsPanel.Controls.Add(this.label1);
            this.ContentsPanel.Controls.Add(this.label16);
            this.ContentsPanel.Controls.Add(this.SkcLinkLabel);
            this.ContentsPanel.Controls.Add(this.GunmaLinkLabel);
            this.ContentsPanel.Controls.Add(this.TruckScheduleButton);
            this.ContentsPanel.Controls.Add(this.LegendButton);
            this.ContentsPanel.Controls.Add(this.MainPanel);
            this.ContentsPanel.Controls.Add(this.MessageLabel);
            this.ContentsPanel.Controls.Add(this.SearchConditionLayoutPanel);
            this.ContentsPanel.Controls.Add(this.ConditionRegistButton);
            this.ContentsPanel.Controls.Add(this.SearchConditionButton);
            this.ContentsPanel.Controls.Add(this.SearchButton);
            this.ContentsPanel.Controls.Add(this.label2);
            this.ContentsPanel.Controls.Add(this.ClearButton);
            // 
            // calendarTitleCaption1
            // 
            this.calendarTitleCaption1.DateFormat = "yyyy年M月 (ggge年)";
            this.calendarTitleCaption1.DateFormatType = GrapeCity.Win.CalendarGrid.CalendarDateFormatType.InputMan;
            this.calendarTitleCaption1.Name = "calendarTitleCaption1";
            this.calendarTitleCaption1.Text = "{0}";
            // 
            // SearchConditionButton
            // 
            this.SearchConditionButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchConditionButton.Location = new System.Drawing.Point(81, 6);
            this.SearchConditionButton.Name = "SearchConditionButton";
            this.SearchConditionButton.Size = new System.Drawing.Size(20, 23);
            this.SearchConditionButton.TabIndex = 24;
            this.SearchConditionButton.Text = "-";
            this.SearchConditionButton.UseVisualStyleBackColor = false;
            this.SearchConditionButton.Click += new System.EventHandler(this.SearchConditionButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "検索条件";
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.OuterCarCalendarGrid);
            this.MainPanel.Location = new System.Drawing.Point(1, 103);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1183, 480);
            this.MainPanel.TabIndex = 25;
            // 
            // OuterCarCalendarGrid
            // 
            this.OuterCarCalendarGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            calendarListView1.DayCount = 90;
            this.OuterCarCalendarGrid.CalendarView = calendarListView1;
            this.OuterCarCalendarGrid.Commands.AddRange(new GrapeCity.Win.CalendarGrid.CalendarGridCommand[] {
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
            this.OuterCarCalendarGrid.DateField = "StartDate";
            this.OuterCarCalendarGrid.Font = new System.Drawing.Font("MS UI Gothic", 11.25F);
            this.OuterCarCalendarGrid.Location = new System.Drawing.Point(3, 3);
            this.OuterCarCalendarGrid.Name = "OuterCarCalendarGrid";
            this.OuterCarCalendarGrid.ResizeMode = GrapeCity.Win.CalendarGrid.CalendarResizeMode.None;
            this.OuterCarCalendarGrid.Size = new System.Drawing.Size(1177, 474);
            calendarCellStyle1.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle2.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle3.ForeColor = System.Drawing.Color.Blue;
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle1, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsHoliday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle2, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSunday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle3, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSaturday));
            calendarConditionalCellStyle1.Name = "defaultStyle";
            this.OuterCarCalendarGrid.Styles.Add(calendarConditionalCellStyle1);
            this.OuterCarCalendarGrid.TabIndex = 103;
            calendarTemplate1.ColumnHeaderRowCount = 2;
            calendarHeaderCellType1.SupportLocalization = true;
            calendarTemplate1.CornerHeader.GetCell(0, 0).CellType = calendarHeaderCellType1;
            calendarHeaderCellType2.SupportLocalization = true;
            calendarTemplate1.CornerHeader.GetCell(1, 0).CellType = calendarHeaderCellType2;
            calendarHeaderCellType3.SupportLocalization = true;
            calendarTemplate1.ColumnHeader.GetCell(0, 0).CellType = calendarHeaderCellType3;
            calendarHeaderCellType4.SupportLocalization = true;
            calendarTemplate1.ColumnHeader.GetCell(1, 0).CellType = calendarHeaderCellType4;
            calendarHeaderCellType5.SupportLocalization = true;
            calendarTemplate1.RowHeader.GetCell(0, 0).CellType = calendarHeaderCellType5;
            this.OuterCarCalendarGrid.Template = calendarTemplate1;
            this.OuterCarCalendarGrid.TitleFooter.Visible = false;
            this.OuterCarCalendarGrid.TitleHeader.AutoHeight = false;
            this.OuterCarCalendarGrid.TitleHeader.Height = 40;
            this.OuterCarCalendarGrid.VerticalScrollMode = GrapeCity.Win.CalendarGrid.CalendarScrollMode.Pixel;
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.Control;
            this.ClearButton.Location = new System.Drawing.Point(139, 67);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(120, 30);
            this.ClearButton.TabIndex = 102;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.ForeColor = System.Drawing.Color.Red;
            this.MessageLabel.Location = new System.Drawing.Point(108, 10);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(0, 15);
            this.MessageLabel.TabIndex = 28;
            // 
            // ConditionRegistButton
            // 
            this.ConditionRegistButton.BackColor = System.Drawing.SystemColors.Control;
            this.ConditionRegistButton.Location = new System.Drawing.Point(265, 67);
            this.ConditionRegistButton.Name = "ConditionRegistButton";
            this.ConditionRegistButton.Size = new System.Drawing.Size(120, 30);
            this.ConditionRegistButton.TabIndex = 101;
            this.ConditionRegistButton.Text = "条件登録";
            this.ConditionRegistButton.UseVisualStyleBackColor = false;
            this.ConditionRegistButton.Click += new System.EventHandler(this.ConditionRegistButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(13, 67);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 30);
            this.SearchButton.TabIndex = 100;
            this.SearchButton.Text = "検索 ／ 更新";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // ExcelPrintButton
            // 
            this.ExcelPrintButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExcelPrintButton.BackColor = System.Drawing.SystemColors.Control;
            this.ExcelPrintButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ExcelPrintButton.Location = new System.Drawing.Point(926, 616);
            this.ExcelPrintButton.Name = "ExcelPrintButton";
            this.ExcelPrintButton.Size = new System.Drawing.Size(120, 20);
            this.ExcelPrintButton.TabIndex = 1004;
            this.ExcelPrintButton.Text = "Excel出力";
            this.ExcelPrintButton.UseVisualStyleBackColor = false;
            this.ExcelPrintButton.Click += new System.EventHandler(this.ExcelPrintButton_Click);
            // 
            // SearchConditionLayoutPanel
            // 
            this.SearchConditionLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SearchConditionLayoutPanel.ColumnCount = 8;
            this.SearchConditionLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.SearchConditionLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.SearchConditionLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.SearchConditionLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.SearchConditionLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.SearchConditionLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 315F));
            this.SearchConditionLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.SearchConditionLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.SearchConditionLayoutPanel.Controls.Add(this.ItemStatusPanel, 5, 0);
            this.SearchConditionLayoutPanel.Controls.Add(this.label18, 4, 0);
            this.SearchConditionLayoutPanel.Controls.Add(this.panel1, 3, 0);
            this.SearchConditionLayoutPanel.Controls.Add(this.label3, 2, 0);
            this.SearchConditionLayoutPanel.Controls.Add(this.CarGroupComboBox, 1, 0);
            this.SearchConditionLayoutPanel.Controls.Add(this.label4, 0, 0);
            this.SearchConditionLayoutPanel.Location = new System.Drawing.Point(12, 32);
            this.SearchConditionLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchConditionLayoutPanel.Name = "SearchConditionLayoutPanel";
            this.SearchConditionLayoutPanel.RowCount = 1;
            this.SearchConditionLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.SearchConditionLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.SearchConditionLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.SearchConditionLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.SearchConditionLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.SearchConditionLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.SearchConditionLayoutPanel.Size = new System.Drawing.Size(1160, 32);
            this.SearchConditionLayoutPanel.TabIndex = 26;
            // 
            // ItemStatusPanel
            // 
            this.ItemStatusPanel.Controls.Add(this.StatusCloseCheckBox);
            this.ItemStatusPanel.Controls.Add(this.StatusOpenCheckBox);
            this.ItemStatusPanel.Location = new System.Drawing.Point(990, 1);
            this.ItemStatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ItemStatusPanel.Name = "ItemStatusPanel";
            this.ItemStatusPanel.Size = new System.Drawing.Size(157, 30);
            this.ItemStatusPanel.TabIndex = 36;
            // 
            // StatusCloseCheckBox
            // 
            this.StatusCloseCheckBox.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.StatusCloseCheckBox.Location = new System.Drawing.Point(73, 7);
            this.StatusCloseCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.StatusCloseCheckBox.Name = "StatusCloseCheckBox";
            this.StatusCloseCheckBox.Size = new System.Drawing.Size(58, 19);
            this.StatusCloseCheckBox.TabIndex = 25;
            this.StatusCloseCheckBox.Text = "Close";
            this.StatusCloseCheckBox.UseVisualStyleBackColor = true;
            // 
            // StatusOpenCheckBox
            // 
            this.StatusOpenCheckBox.Checked = true;
            this.StatusOpenCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.StatusOpenCheckBox.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.StatusOpenCheckBox.Location = new System.Drawing.Point(13, 7);
            this.StatusOpenCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.StatusOpenCheckBox.Name = "StatusOpenCheckBox";
            this.StatusOpenCheckBox.Size = new System.Drawing.Size(60, 19);
            this.StatusOpenCheckBox.TabIndex = 24;
            this.StatusOpenCheckBox.Text = "Open";
            this.StatusOpenCheckBox.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Aquamarine;
            this.label18.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label18.Location = new System.Drawing.Point(862, 1);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(127, 30);
            this.label18.TabIndex = 35;
            this.label18.Text = "項目ステータス";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.SearchConditionLayoutPanel.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.EmptyEndTimeComboBox);
            this.panel1.Controls.Add(this.EmptyEndDayDateTimePicker);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.EmptyStartTimeComboBox);
            this.panel1.Controls.Add(this.EmptyStartDayDateTimePicker);
            this.panel1.Location = new System.Drawing.Point(304, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 30);
            this.panel1.TabIndex = 12;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("MS UI Gothic", 11.25F);
            this.label17.Location = new System.Drawing.Point(376, 7);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(127, 15);
            this.label17.TabIndex = 24;
            this.label17.Text = "※条件登録対象外";
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
            this.EmptyEndTimeComboBox.TabIndex = 23;
            this.EmptyEndTimeComboBox.Tag = "ItemName(空車期間Fromの時刻)";
            // 
            // EmptyEndDayDateTimePicker
            // 
            this.EmptyEndDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EmptyEndDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EmptyEndDayDateTimePicker.Location = new System.Drawing.Point(200, 4);
            this.EmptyEndDayDateTimePicker.Name = "EmptyEndDayDateTimePicker";
            this.EmptyEndDayDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.EmptyEndDayDateTimePicker.TabIndex = 22;
            this.EmptyEndDayDateTimePicker.Tag = "ItemName(空車期間Toの日付)";
            this.EmptyEndDayDateTimePicker.Value = new System.DateTime(2019, 12, 24, 0, 0, 0, 0);
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
            this.EmptyStartTimeComboBox.TabIndex = 21;
            this.EmptyStartTimeComboBox.Tag = "ItemName(空車期間Fromの時刻)";
            // 
            // EmptyStartDayDateTimePicker
            // 
            this.EmptyStartDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EmptyStartDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EmptyStartDayDateTimePicker.Location = new System.Drawing.Point(3, 4);
            this.EmptyStartDayDateTimePicker.Name = "EmptyStartDayDateTimePicker";
            this.EmptyStartDayDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.EmptyStartDayDateTimePicker.TabIndex = 20;
            this.EmptyStartDayDateTimePicker.Tag = "ItemName(空車期間Fromの日付)";
            this.EmptyStartDayDateTimePicker.Value = new System.DateTime(2019, 12, 24, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Location = new System.Drawing.Point(213, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "空車期間";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CarGroupComboBox
            // 
            this.CarGroupComboBox.DisplayMember = "CAR_GROUP";
            this.CarGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CarGroupComboBox.FormattingEnabled = true;
            this.CarGroupComboBox.Location = new System.Drawing.Point(95, 4);
            this.CarGroupComboBox.Name = "CarGroupComboBox";
            this.CarGroupComboBox.Size = new System.Drawing.Size(114, 23);
            this.CarGroupComboBox.TabIndex = 19;
            this.CarGroupComboBox.Tag = "Required;ItemName(車系)";
            this.CarGroupComboBox.ValueMember = "CAR_GROUP";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Aquamarine;
            this.label4.Location = new System.Drawing.Point(1, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 30);
            this.label4.TabIndex = 1;
            this.label4.Text = "車系";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1020, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 1007;
            this.label1.Text = "・";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.Location = new System.Drawing.Point(843, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(135, 15);
            this.label16.TabIndex = 1008;
            this.label16.Text = "駐車場地図はこちら";
            // 
            // SkcLinkLabel
            // 
            this.SkcLinkLabel.AutoSize = true;
            this.SkcLinkLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SkcLinkLabel.Location = new System.Drawing.Point(1034, 10);
            this.SkcLinkLabel.Name = "SkcLinkLabel";
            this.SkcLinkLabel.Size = new System.Drawing.Size(43, 15);
            this.SkcLinkLabel.TabIndex = 1006;
            this.SkcLinkLabel.TabStop = true;
            this.SkcLinkLabel.Text = "ＳＫＣ";
            this.SkcLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SkcLinkLabel_LinkClicked);
            // 
            // GunmaLinkLabel
            // 
            this.GunmaLinkLabel.AutoSize = true;
            this.GunmaLinkLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GunmaLinkLabel.Location = new System.Drawing.Point(978, 10);
            this.GunmaLinkLabel.Name = "GunmaLinkLabel";
            this.GunmaLinkLabel.Size = new System.Drawing.Size(42, 15);
            this.GunmaLinkLabel.TabIndex = 1005;
            this.GunmaLinkLabel.TabStop = true;
            this.GunmaLinkLabel.Text = "ぐんま";
            this.GunmaLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GunmaLinkLabel_LinkClicked);
            // 
            // LegendButton
            // 
            this.LegendButton.BackColor = System.Drawing.SystemColors.Control;
            this.LegendButton.Location = new System.Drawing.Point(1084, 4);
            this.LegendButton.Name = "LegendButton";
            this.LegendButton.Size = new System.Drawing.Size(88, 26);
            this.LegendButton.TabIndex = 1004;
            this.LegendButton.Text = "凡例";
            this.LegendButton.UseVisualStyleBackColor = false;
            this.LegendButton.Click += new System.EventHandler(this.LegendButton_Click);
            // 
            // TruckScheduleButton
            // 
            this.TruckScheduleButton.BackColor = System.Drawing.SystemColors.Control;
            this.TruckScheduleButton.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TruckScheduleButton.Location = new System.Drawing.Point(686, 4);
            this.TruckScheduleButton.Name = "TruckScheduleButton";
            this.TruckScheduleButton.Size = new System.Drawing.Size(151, 26);
            this.TruckScheduleButton.TabIndex = 1004;
            this.TruckScheduleButton.Text = "トラック予約状況表示";
            this.TruckScheduleButton.UseVisualStyleBackColor = false;
            this.TruckScheduleButton.Click += new System.EventHandler(this.TruckScheduleButton_Click);
            // 
            // TruckScheduleCheckBox
            // 
            this.TruckScheduleCheckBox.AutoSize = true;
            this.TruckScheduleCheckBox.Checked = true;
            this.TruckScheduleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TruckScheduleCheckBox.Location = new System.Drawing.Point(551, 10);
            this.TruckScheduleCheckBox.Name = "TruckScheduleCheckBox";
            this.TruckScheduleCheckBox.Size = new System.Drawing.Size(135, 19);
            this.TruckScheduleCheckBox.TabIndex = 1009;
            this.TruckScheduleCheckBox.Text = "トラック予約と同期";
            this.TruckScheduleCheckBox.UseVisualStyleBackColor = true;
            // 
            // OuterCarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.ExcelPrintButton);
            this.Name = "OuterCarForm";
            this.Text = "OuterCarForm";
            this.Load += new System.EventHandler(this.OuterCarForm_Load);
            this.Controls.SetChildIndex(this.ExcelPrintButton, 0);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OuterCarCalendarGrid)).EndInit();
            this.OuterCarCalendarGrid.ResumeLayout(false);
            this.SearchConditionLayoutPanel.ResumeLayout(false);
            this.ItemStatusPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmptyEndDayDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmptyStartDayDateTimePicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GrapeCity.Win.CalendarGrid.CalendarTitleCaption calendarTitleCaption1;
        private System.Windows.Forms.Button SearchConditionButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button ConditionRegistButton;
        private System.Windows.Forms.Button SearchButton;
        private GrapeCity.Win.CalendarGrid.GcCalendarGrid OuterCarCalendarGrid;
        private System.Windows.Forms.Button ExcelPrintButton;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.TableLayoutPanel SearchConditionLayoutPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CarGroupComboBox;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.LinkLabel SkcLinkLabel;
        private System.Windows.Forms.LinkLabel GunmaLinkLabel;
        private System.Windows.Forms.Button LegendButton;
        private System.Windows.Forms.Panel ItemStatusPanel;
        private System.Windows.Forms.CheckBox StatusCloseCheckBox;
        private System.Windows.Forms.CheckBox StatusOpenCheckBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox EmptyEndTimeComboBox;
        private UC.NullableDateTimePicker EmptyEndDayDateTimePicker;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox EmptyStartTimeComboBox;
        private UC.NullableDateTimePicker EmptyStartDayDateTimePicker;
        private System.Windows.Forms.Button TruckScheduleButton;
        private System.Windows.Forms.CheckBox TruckScheduleCheckBox;
    }
}