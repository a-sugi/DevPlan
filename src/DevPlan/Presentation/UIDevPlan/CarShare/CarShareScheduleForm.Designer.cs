namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    partial class CarShareScheduleForm
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
            GrapeCity.Win.CalendarGrid.CalendarAppointmentCellType calendarAppointmentCellType1 = new GrapeCity.Win.CalendarGrid.CalendarAppointmentCellType();
            this.ExcelPrintButton = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.CarShareCalendarGrid = new GrapeCity.Win.CalendarGrid.GcCalendarGrid();
            this.ClearButton = new System.Windows.Forms.Button();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.FavoriteEntryButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchConditionButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SearchConditionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.CarGroupComboBox = new System.Windows.Forms.ComboBox();
            this.ItemStatusPanel = new System.Windows.Forms.Panel();
            this.StatusCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.StatusOpenCheckBox = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.BlankCarFromDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.BlankCarToDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.BlankCarToComboBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.BlankCarFromComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SkcLinkLabel = new System.Windows.Forms.LinkLabel();
            this.GunmaLinkLabel = new System.Windows.Forms.LinkLabel();
            this.LegendButton = new System.Windows.Forms.Button();
            this.ContentsPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CarShareCalendarGrid)).BeginInit();
            this.CarShareCalendarGrid.SuspendLayout();
            this.SearchConditionTableLayoutPanel.SuspendLayout();
            this.ItemStatusPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlankCarFromDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlankCarToDateTimePicker)).BeginInit();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.TabIndex = 5;
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.label5);
            this.ContentsPanel.Controls.Add(this.label1);
            this.ContentsPanel.Controls.Add(this.SkcLinkLabel);
            this.ContentsPanel.Controls.Add(this.GunmaLinkLabel);
            this.ContentsPanel.Controls.Add(this.LegendButton);
            this.ContentsPanel.Controls.Add(this.MainPanel);
            this.ContentsPanel.Controls.Add(this.ClearButton);
            this.ContentsPanel.Controls.Add(this.MessageLabel);
            this.ContentsPanel.Controls.Add(this.SearchConditionTableLayoutPanel);
            this.ContentsPanel.Controls.Add(this.SearchConditionButton);
            this.ContentsPanel.Controls.Add(this.FavoriteEntryButton);
            this.ContentsPanel.Controls.Add(this.label2);
            this.ContentsPanel.Controls.Add(this.SearchButton);
            // 
            // ExcelPrintButton
            // 
            this.ExcelPrintButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExcelPrintButton.BackColor = System.Drawing.SystemColors.Control;
            this.ExcelPrintButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ExcelPrintButton.Location = new System.Drawing.Point(926, 616);
            this.ExcelPrintButton.Name = "ExcelPrintButton";
            this.ExcelPrintButton.Size = new System.Drawing.Size(120, 20);
            this.ExcelPrintButton.TabIndex = 4;
            this.ExcelPrintButton.Text = "Excel出力";
            this.ExcelPrintButton.UseVisualStyleBackColor = false;
            this.ExcelPrintButton.Click += new System.EventHandler(this.ExcelPrintButton_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.CarShareCalendarGrid);
            this.MainPanel.Location = new System.Drawing.Point(0, 102);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1184, 481);
            this.MainPanel.TabIndex = 1000;
            // 
            // CarShareCalendarGrid
            // 
            this.CarShareCalendarGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            calendarListView1.DateAlignment = GrapeCity.Win.CalendarGrid.DateAlignment.Month;
            calendarListView1.DayCount = 90;
            this.CarShareCalendarGrid.CalendarView = calendarListView1;
            this.CarShareCalendarGrid.Commands.AddRange(new GrapeCity.Win.CalendarGrid.CalendarGridCommand[] {
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
            this.CarShareCalendarGrid.CurrentDate = new System.DateTime(2017, 2, 7, 0, 0, 0, 0);
            this.CarShareCalendarGrid.DateField = " ";
            this.CarShareCalendarGrid.FirstDateInView = new System.DateTime(2017, 2, 1, 0, 0, 0, 0);
            this.CarShareCalendarGrid.Location = new System.Drawing.Point(3, 3);
            this.CarShareCalendarGrid.Name = "CarShareCalendarGrid";
            this.CarShareCalendarGrid.Protected = true;
            this.CarShareCalendarGrid.Size = new System.Drawing.Size(1178, 475);
            calendarCellStyle1.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle2.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle3.ForeColor = System.Drawing.Color.Blue;
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle1, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsHoliday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle2, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSunday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle3, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSaturday));
            calendarConditionalCellStyle1.Name = "defaultStyle";
            this.CarShareCalendarGrid.Styles.Add(calendarConditionalCellStyle1);
            this.CarShareCalendarGrid.TabIndex = 3;
            calendarTemplate1.ColumnHeaderRowCount = 2;
            calendarHeaderCellType1.SupportLocalization = true;
            calendarTemplate1.CornerHeader.GetCell(0, 0).CellType = calendarHeaderCellType1;
            calendarTemplate1.CornerHeader.GetCell(0, 0).RowSpan = 2;
            calendarTemplate1.CornerHeader.GetCell(0, 0).CellStyle.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            calendarHeaderCellType2.SupportLocalization = true;
            calendarTemplate1.CornerHeader.GetCell(1, 0).CellType = calendarHeaderCellType2;
            calendarTemplate1.ColumnHeader.CellStyleName = "defaultStyle";
            calendarTemplate1.ColumnHeader.GetRow(0).Height = 21;
            calendarHeaderCellType3.SupportLocalization = true;
            calendarTemplate1.ColumnHeader.GetCell(0, 0).CellType = calendarHeaderCellType3;
            calendarTemplate1.ColumnHeader.GetCell(0, 0).DateFormat = "yyyy/MM";
            calendarTemplate1.ColumnHeader.GetCell(0, 0).DateFormatType = GrapeCity.Win.CalendarGrid.CalendarDateFormatType.DotNet;
            calendarTemplate1.ColumnHeader.GetCell(0, 0).AutoMergeMode = GrapeCity.Win.CalendarGrid.AutoMergeMode.Horizontal;
            calendarTemplate1.ColumnHeader.GetCell(0, 0).CellStyle.Alignment = GrapeCity.Win.CalendarGrid.CalendarGridContentAlignment.TopLeft;
            calendarTemplate1.ColumnHeader.GetCell(0, 0).CellStyle.BottomBorder = new GrapeCity.Win.CalendarGrid.CalendarBorderLine(System.Drawing.Color.Black, GrapeCity.Win.CalendarGrid.BorderLineStyle.Thin);
            calendarTemplate1.ColumnHeader.GetCell(0, 0).CellStyle.LeftBorder = new GrapeCity.Win.CalendarGrid.CalendarBorderLine(System.Drawing.Color.Black, GrapeCity.Win.CalendarGrid.BorderLineStyle.Thin);
            calendarTemplate1.ColumnHeader.GetCell(0, 0).CellStyle.RightBorder = new GrapeCity.Win.CalendarGrid.CalendarBorderLine(System.Drawing.Color.Black, GrapeCity.Win.CalendarGrid.BorderLineStyle.Thin);
            calendarTemplate1.ColumnHeader.GetCell(0, 0).CellStyle.TopBorder = new GrapeCity.Win.CalendarGrid.CalendarBorderLine(System.Drawing.Color.Black, GrapeCity.Win.CalendarGrid.BorderLineStyle.Thin);
            calendarTemplate1.ColumnHeader.GetCell(0, 0).CellStyleName = "";
            calendarHeaderCellType4.SupportLocalization = true;
            calendarTemplate1.ColumnHeader.GetCell(1, 0).CellType = calendarHeaderCellType4;
            calendarTemplate1.ColumnHeader.GetCell(1, 0).DateFormat = "{Day}";
            calendarTemplate1.ColumnHeader.GetCell(1, 0).DateFormatType = GrapeCity.Win.CalendarGrid.CalendarDateFormatType.CalendarGrid;
            calendarTemplate1.ColumnHeader.GetCell(1, 0).AutoMergeMode = GrapeCity.Win.CalendarGrid.AutoMergeMode.Horizontal;
            calendarTemplate1.ColumnHeader.GetCell(1, 0).CellStyle.Alignment = GrapeCity.Win.CalendarGrid.CalendarGridContentAlignment.MiddleCenter;
            calendarTemplate1.ColumnHeader.GetCell(1, 0).CellStyle.BottomBorder = new GrapeCity.Win.CalendarGrid.CalendarBorderLine(System.Drawing.Color.Black, GrapeCity.Win.CalendarGrid.BorderLineStyle.Thin);
            calendarTemplate1.ColumnHeader.GetCell(1, 0).CellStyle.LeftBorder = new GrapeCity.Win.CalendarGrid.CalendarBorderLine(System.Drawing.Color.Black, GrapeCity.Win.CalendarGrid.BorderLineStyle.Thin);
            calendarTemplate1.ColumnHeader.GetCell(1, 0).CellStyle.RightBorder = new GrapeCity.Win.CalendarGrid.CalendarBorderLine(System.Drawing.Color.Black, GrapeCity.Win.CalendarGrid.BorderLineStyle.Thin);
            calendarTemplate1.ColumnHeader.GetCell(1, 0).CellStyle.TopBorder = new GrapeCity.Win.CalendarGrid.CalendarBorderLine(System.Drawing.Color.Black, GrapeCity.Win.CalendarGrid.BorderLineStyle.Thin);
            calendarTemplate1.RowHeader.GetColumn(0).Width = 165;
            calendarTemplate1.RowHeader.GetColumn(0).MinWidth = 165;
            calendarTemplate1.RowHeader.GetColumn(0).MaxWidth = 165;
            calendarHeaderCellType5.SupportLocalization = true;
            calendarTemplate1.RowHeader.GetCell(0, 0).CellType = calendarHeaderCellType5;
            calendarTemplate1.RowHeader.GetCell(0, 0).AutoMergeMode = GrapeCity.Win.CalendarGrid.AutoMergeMode.Vertical;
            calendarTemplate1.RowHeader.GetCell(0, 0).CellStyle.BottomBorder = new GrapeCity.Win.CalendarGrid.CalendarBorderLine(System.Drawing.Color.Black, GrapeCity.Win.CalendarGrid.BorderLineStyle.Dotted);
            calendarTemplate1.Content.CellStyleName = "defaultStyle";
            calendarTemplate1.Content.GetColumn(0).Width = 40;
            calendarTemplate1.Content.GetRow(0).Height = 40;
            calendarAppointmentCellType1.Multiline = true;
            calendarAppointmentCellType1.SupportLocalization = true;
            calendarTemplate1.Content.GetCell(0, 0).CellType = calendarAppointmentCellType1;
            calendarTemplate1.Content.GetCell(0, 0).CellStyle.BottomBorder = new GrapeCity.Win.CalendarGrid.CalendarBorderLine(System.Drawing.Color.Black, GrapeCity.Win.CalendarGrid.BorderLineStyle.Dotted);
            calendarTemplate1.Content.GetCell(0, 0).CellStyleName = "";
            this.CarShareCalendarGrid.Template = calendarTemplate1;
            this.CarShareCalendarGrid.TitleHeader.AutoHeight = false;
            this.CarShareCalendarGrid.VerticalScrollMode = GrapeCity.Win.CalendarGrid.CalendarScrollMode.Pixel;
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.Control;
            this.ClearButton.Location = new System.Drawing.Point(139, 67);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(120, 30);
            this.ClearButton.TabIndex = 25;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.ForeColor = System.Drawing.Color.Red;
            this.MessageLabel.Location = new System.Drawing.Point(136, 10);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(0, 15);
            this.MessageLabel.TabIndex = 24;
            // 
            // FavoriteEntryButton
            // 
            this.FavoriteEntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.FavoriteEntryButton.Location = new System.Drawing.Point(265, 67);
            this.FavoriteEntryButton.Name = "FavoriteEntryButton";
            this.FavoriteEntryButton.Size = new System.Drawing.Size(120, 30);
            this.FavoriteEntryButton.TabIndex = 1;
            this.FavoriteEntryButton.Text = "条件登録";
            this.FavoriteEntryButton.UseVisualStyleBackColor = false;
            this.FavoriteEntryButton.Click += new System.EventHandler(this.FavoriteEntryButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(13, 67);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 30);
            this.SearchButton.TabIndex = 0;
            this.SearchButton.Text = "検索 ／ 更新";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchConditionButton
            // 
            this.SearchConditionButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchConditionButton.Location = new System.Drawing.Point(86, 6);
            this.SearchConditionButton.Name = "SearchConditionButton";
            this.SearchConditionButton.Size = new System.Drawing.Size(20, 23);
            this.SearchConditionButton.TabIndex = 10;
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
            this.label2.TabIndex = 11;
            this.label2.Text = "検索条件";
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
            // SearchConditionTableLayoutPanel
            // 
            this.SearchConditionTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SearchConditionTableLayoutPanel.ColumnCount = 8;
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 276F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label4, 0, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.CarGroupComboBox, 1, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.ItemStatusPanel, 5, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label3, 2, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label15, 4, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.panel1, 3, 0);
            this.SearchConditionTableLayoutPanel.Location = new System.Drawing.Point(12, 32);
            this.SearchConditionTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchConditionTableLayoutPanel.Name = "SearchConditionTableLayoutPanel";
            this.SearchConditionTableLayoutPanel.RowCount = 1;
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.SearchConditionTableLayoutPanel.Size = new System.Drawing.Size(1064, 32);
            this.SearchConditionTableLayoutPanel.TabIndex = 12;
            // 
            // CarGroupComboBox
            // 
            this.CarGroupComboBox.DisplayMember = "CAR_GROUP";
            this.CarGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CarGroupComboBox.FormattingEnabled = true;
            this.CarGroupComboBox.Location = new System.Drawing.Point(95, 4);
            this.CarGroupComboBox.Name = "CarGroupComboBox";
            this.CarGroupComboBox.Size = new System.Drawing.Size(114, 23);
            this.CarGroupComboBox.TabIndex = 0;
            this.CarGroupComboBox.Tag = "Required;ItemName(車系)";
            this.CarGroupComboBox.ValueMember = "CAR_GROUP";
            this.CarGroupComboBox.SelectedValueChanged += new System.EventHandler(this.CarGroupComboBox_SelectedValueChanged);
            // 
            // ItemStatusPanel
            // 
            this.ItemStatusPanel.Controls.Add(this.StatusCloseCheckBox);
            this.ItemStatusPanel.Controls.Add(this.StatusOpenCheckBox);
            this.ItemStatusPanel.Location = new System.Drawing.Point(930, 1);
            this.ItemStatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ItemStatusPanel.Name = "ItemStatusPanel";
            this.ItemStatusPanel.Size = new System.Drawing.Size(133, 30);
            this.ItemStatusPanel.TabIndex = 26;
            // 
            // StatusCloseCheckBox
            // 
            this.StatusCloseCheckBox.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.StatusCloseCheckBox.Location = new System.Drawing.Point(68, 6);
            this.StatusCloseCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.StatusCloseCheckBox.Name = "StatusCloseCheckBox";
            this.StatusCloseCheckBox.Size = new System.Drawing.Size(58, 19);
            this.StatusCloseCheckBox.TabIndex = 1;
            this.StatusCloseCheckBox.Text = "Close";
            this.StatusCloseCheckBox.UseVisualStyleBackColor = true;
            // 
            // StatusOpenCheckBox
            // 
            this.StatusOpenCheckBox.Checked = true;
            this.StatusOpenCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.StatusOpenCheckBox.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.StatusOpenCheckBox.Location = new System.Drawing.Point(8, 6);
            this.StatusOpenCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.StatusOpenCheckBox.Name = "StatusOpenCheckBox";
            this.StatusOpenCheckBox.Size = new System.Drawing.Size(60, 19);
            this.StatusOpenCheckBox.TabIndex = 0;
            this.StatusOpenCheckBox.Text = "Open";
            this.StatusOpenCheckBox.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Aquamarine;
            this.label15.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label15.Location = new System.Drawing.Point(823, 1);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(106, 30);
            this.label15.TabIndex = 25;
            this.label15.Text = "項目ステータス";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.SearchConditionTableLayoutPanel.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.BlankCarFromDateTimePicker);
            this.panel1.Controls.Add(this.BlankCarToDateTimePicker);
            this.panel1.Controls.Add(this.BlankCarToComboBox);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.BlankCarFromComboBox);
            this.panel1.Location = new System.Drawing.Point(304, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 30);
            this.panel1.TabIndex = 12;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("MS UI Gothic", 11.25F);
            this.label17.Location = new System.Drawing.Point(385, 7);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(127, 15);
            this.label17.TabIndex = 25;
            this.label17.Text = "※条件登録対象外";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(357, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 15);
            this.label14.TabIndex = 22;
            this.label14.Text = "時";
            // 
            // BlankCarFromDateTimePicker
            // 
            this.BlankCarFromDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.BlankCarFromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.BlankCarFromDateTimePicker.Location = new System.Drawing.Point(3, 4);
            this.BlankCarFromDateTimePicker.Name = "BlankCarFromDateTimePicker";
            this.BlankCarFromDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.BlankCarFromDateTimePicker.TabIndex = 0;
            this.BlankCarFromDateTimePicker.Tag = "ItemName(空車期間Fromの日付)";
            this.BlankCarFromDateTimePicker.Value = new System.DateTime(2019, 12, 24, 0, 0, 0, 0);
            // 
            // BlankCarToDateTimePicker
            // 
            this.BlankCarToDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.BlankCarToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.BlankCarToDateTimePicker.Location = new System.Drawing.Point(204, 4);
            this.BlankCarToDateTimePicker.Name = "BlankCarToDateTimePicker";
            this.BlankCarToDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.BlankCarToDateTimePicker.TabIndex = 2;
            this.BlankCarToDateTimePicker.Tag = "ItemName(空車期間Toの日付)";
            this.BlankCarToDateTimePicker.Value = new System.DateTime(2019, 12, 24, 0, 0, 0, 0);
            // 
            // BlankCarToComboBox
            // 
            this.BlankCarToComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BlankCarToComboBox.FormattingEnabled = true;
            this.BlankCarToComboBox.Items.AddRange(new object[] {
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
            this.BlankCarToComboBox.Location = new System.Drawing.Point(310, 4);
            this.BlankCarToComboBox.Name = "BlankCarToComboBox";
            this.BlankCarToComboBox.Size = new System.Drawing.Size(41, 23);
            this.BlankCarToComboBox.TabIndex = 3;
            this.BlankCarToComboBox.Tag = "ItemName(空車期間Fromの時刻)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(156, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 15);
            this.label13.TabIndex = 17;
            this.label13.Text = "時 ～";
            // 
            // BlankCarFromComboBox
            // 
            this.BlankCarFromComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BlankCarFromComboBox.FormattingEnabled = true;
            this.BlankCarFromComboBox.Items.AddRange(new object[] {
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
            this.BlankCarFromComboBox.Location = new System.Drawing.Point(109, 4);
            this.BlankCarFromComboBox.Name = "BlankCarFromComboBox";
            this.BlankCarFromComboBox.Size = new System.Drawing.Size(41, 23);
            this.BlankCarFromComboBox.TabIndex = 1;
            this.BlankCarFromComboBox.Tag = "ItemName(空車期間Fromの時刻)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(901, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 15);
            this.label5.TabIndex = 1007;
            this.label5.Text = "・";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(715, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 15);
            this.label1.TabIndex = 1008;
            this.label1.Text = "駐車場地図はこちら";
            // 
            // SkcLinkLabel
            // 
            this.SkcLinkLabel.AutoSize = true;
            this.SkcLinkLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SkcLinkLabel.Location = new System.Drawing.Point(918, 10);
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
            this.GunmaLinkLabel.Location = new System.Drawing.Point(856, 10);
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
            this.LegendButton.Location = new System.Drawing.Point(987, 4);
            this.LegendButton.Name = "LegendButton";
            this.LegendButton.Size = new System.Drawing.Size(88, 26);
            this.LegendButton.TabIndex = 1004;
            this.LegendButton.Text = "凡例";
            this.LegendButton.UseVisualStyleBackColor = false;
            this.LegendButton.Click += new System.EventHandler(this.LegendButton_Click);
            // 
            // CarShareScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.ExcelPrintButton);
            this.Name = "CarShareScheduleForm";
            this.Text = "CarShareScheduleForm";
            this.Load += new System.EventHandler(this.CarShareScheduleForm_Load);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.Controls.SetChildIndex(this.ExcelPrintButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CarShareCalendarGrid)).EndInit();
            this.CarShareCalendarGrid.ResumeLayout(false);
            this.SearchConditionTableLayoutPanel.ResumeLayout(false);
            this.ItemStatusPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlankCarFromDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlankCarToDateTimePicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExcelPrintButton;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label MessageLabel;
        private GrapeCity.Win.CalendarGrid.GcCalendarGrid CarShareCalendarGrid;
        private System.Windows.Forms.Button FavoriteEntryButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button SearchConditionButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TableLayoutPanel SearchConditionTableLayoutPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CarGroupComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel SkcLinkLabel;
        private System.Windows.Forms.LinkLabel GunmaLinkLabel;
        private System.Windows.Forms.Button LegendButton;
        private System.Windows.Forms.Panel ItemStatusPanel;
        private System.Windows.Forms.CheckBox StatusCloseCheckBox;
        private System.Windows.Forms.CheckBox StatusOpenCheckBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private UC.NullableDateTimePicker BlankCarFromDateTimePicker;
        private UC.NullableDateTimePicker BlankCarToDateTimePicker;
        private System.Windows.Forms.ComboBox BlankCarToComboBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox BlankCarFromComboBox;
    }
}