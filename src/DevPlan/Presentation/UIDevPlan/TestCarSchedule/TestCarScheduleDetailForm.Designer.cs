namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    partial class TestCarScheduleDetailForm
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
            this.CarUserComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.DatePanel = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.EndTimeComboBox = new System.Windows.Forms.ComboBox();
            this.EndDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.StartTimeComboBox = new System.Windows.Forms.ComboBox();
            this.StartDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.KeyStorageLocationTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CarStorageLocationTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ChangePlaceTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.DesorptionPpartsTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TestContentTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.OdometerTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TypePanel = new System.Windows.Forms.Panel();
            this.TriangleRadioButton = new System.Windows.Forms.RadioButton();
            this.DoubleCircleRadioButton = new System.Windows.Forms.RadioButton();
            this.SquareRadioButton = new System.Windows.Forms.RadioButton();
            this.DefaultRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ReservationPanel = new System.Windows.Forms.Panel();
            this.ReservationRadioButton = new System.Windows.Forms.RadioButton();
            this.TentativeReservationRadioButton = new System.Windows.Forms.RadioButton();
            this.ScheduleNameTextBox = new System.Windows.Forms.TextBox();
            this.WorkCompletionDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.ReservedPersonLabel = new System.Windows.Forms.Label();
            this.EntryButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            this.DatePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).BeginInit();
            this.TypePanel.SuspendLayout();
            this.ReservationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkCompletionDateTimePicker)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(525, 619);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.CloseButton.TabIndex = 21;
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
            this.ListFormMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.ListFormMainPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ListFormMainPanel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ListFormMainPanel.Size = new System.Drawing.Size(668, 604);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DetailTableLayoutPanel, 0);
            // 
            // CarUserComboBox
            // 
            this.CarUserComboBox.DisplayMember = "DISPLAY_NAME";
            this.CarUserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CarUserComboBox.FormattingEnabled = true;
            this.CarUserComboBox.Location = new System.Drawing.Point(194, 510);
            this.CarUserComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CarUserComboBox.Name = "CarUserComboBox";
            this.CarUserComboBox.Size = new System.Drawing.Size(459, 27);
            this.CarUserComboBox.TabIndex = 18;
            this.CarUserComboBox.Tag = "Required;ItemName(車両使用者)";
            this.CarUserComboBox.ValueMember = "PERSONEL_ID";
            this.CarUserComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CarUserComboBox_MouseClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 398);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 188F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.Controls.Add(this.CarUserComboBox, 1, 12);
            this.DetailTableLayoutPanel.Controls.Add(this.label17, 0, 12);
            this.DetailTableLayoutPanel.Controls.Add(this.label15, 0, 11);
            this.DetailTableLayoutPanel.Controls.Add(this.DatePanel, 1, 10);
            this.DetailTableLayoutPanel.Controls.Add(this.label12, 0, 10);
            this.DetailTableLayoutPanel.Controls.Add(this.KeyStorageLocationTextBox, 1, 9);
            this.DetailTableLayoutPanel.Controls.Add(this.label11, 0, 9);
            this.DetailTableLayoutPanel.Controls.Add(this.CarStorageLocationTextBox, 1, 8);
            this.DetailTableLayoutPanel.Controls.Add(this.label10, 0, 8);
            this.DetailTableLayoutPanel.Controls.Add(this.ChangePlaceTextBox, 1, 7);
            this.DetailTableLayoutPanel.Controls.Add(this.label9, 0, 7);
            this.DetailTableLayoutPanel.Controls.Add(this.DesorptionPpartsTextBox, 1, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.label8, 0, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.TestContentTextBox, 1, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.label7, 0, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.OdometerTextBox, 1, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.label6, 0, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.label5, 0, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.label4, 0, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.TypePanel, 1, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.label3, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.label2, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.ReservationPanel, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.ScheduleNameTextBox, 1, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.WorkCompletionDateTimePicker, 1, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.ReservedPersonLabel, 1, 11);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(4, 50);
            this.DetailTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 13;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(659, 544);
            this.DetailTableLayoutPanel.TabIndex = 1011;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Aquamarine;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(1, 506);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(188, 38);
            this.label17.TabIndex = 24;
            this.label17.Text = "車両使用者";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Aquamarine;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(1, 467);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(188, 38);
            this.label15.TabIndex = 22;
            this.label15.Text = "予約者";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DatePanel
            // 
            this.DatePanel.Controls.Add(this.label14);
            this.DatePanel.Controls.Add(this.EndTimeComboBox);
            this.DatePanel.Controls.Add(this.EndDayDateTimePicker);
            this.DatePanel.Controls.Add(this.label13);
            this.DatePanel.Controls.Add(this.StartTimeComboBox);
            this.DatePanel.Controls.Add(this.StartDayDateTimePicker);
            this.DatePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.DatePanel.Location = new System.Drawing.Point(190, 428);
            this.DatePanel.Margin = new System.Windows.Forms.Padding(0);
            this.DatePanel.Name = "DatePanel";
            this.DatePanel.Size = new System.Drawing.Size(468, 38);
            this.DatePanel.TabIndex = 21;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(435, 9);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(28, 19);
            this.label14.TabIndex = 13;
            this.label14.Text = "時";
            // 
            // EndTimeComboBox
            // 
            this.EndTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EndTimeComboBox.FormattingEnabled = true;
            this.EndTimeComboBox.Items.AddRange(new object[] {
            "",
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
            this.EndTimeComboBox.Location = new System.Drawing.Point(382, 5);
            this.EndTimeComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EndTimeComboBox.Name = "EndTimeComboBox";
            this.EndTimeComboBox.Size = new System.Drawing.Size(50, 27);
            this.EndTimeComboBox.TabIndex = 17;
            this.EndTimeComboBox.Tag = "Required;ItemName(期間Toの時刻)";
            // 
            // EndDayDateTimePicker
            // 
            this.EndDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EndDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDayDateTimePicker.Location = new System.Drawing.Point(250, 5);
            this.EndDayDateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EndDayDateTimePicker.Name = "EndDayDateTimePicker";
            this.EndDayDateTimePicker.Size = new System.Drawing.Size(124, 26);
            this.EndDayDateTimePicker.TabIndex = 16;
            this.EndDayDateTimePicker.Tag = "Required;ItemName(期間Toの日付)";
            this.EndDayDateTimePicker.Value = new System.DateTime(2024, 11, 15, 0, 0, 0, 0);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(190, 9);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 19);
            this.label13.TabIndex = 10;
            this.label13.Text = "時 ～";
            // 
            // StartTimeComboBox
            // 
            this.StartTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StartTimeComboBox.FormattingEnabled = true;
            this.StartTimeComboBox.Items.AddRange(new object[] {
            "",
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
            this.StartTimeComboBox.Location = new System.Drawing.Point(131, 5);
            this.StartTimeComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartTimeComboBox.Name = "StartTimeComboBox";
            this.StartTimeComboBox.Size = new System.Drawing.Size(50, 27);
            this.StartTimeComboBox.TabIndex = 15;
            this.StartTimeComboBox.Tag = "Required;ItemName(期間Fromの時刻)";
            // 
            // StartDayDateTimePicker
            // 
            this.StartDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.StartDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDayDateTimePicker.Location = new System.Drawing.Point(4, 5);
            this.StartDayDateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartDayDateTimePicker.Name = "StartDayDateTimePicker";
            this.StartDayDateTimePicker.Size = new System.Drawing.Size(124, 26);
            this.StartDayDateTimePicker.TabIndex = 14;
            this.StartDayDateTimePicker.Tag = "Required;ItemName(期間Fromの日付)";
            this.StartDayDateTimePicker.Value = new System.DateTime(2024, 11, 15, 0, 0, 0, 0);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Aquamarine;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(1, 428);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(188, 38);
            this.label12.TabIndex = 20;
            this.label12.Text = "期間";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KeyStorageLocationTextBox
            // 
            this.KeyStorageLocationTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KeyStorageLocationTextBox.Location = new System.Drawing.Point(194, 393);
            this.KeyStorageLocationTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.KeyStorageLocationTextBox.MaxLength = 100;
            this.KeyStorageLocationTextBox.Name = "KeyStorageLocationTextBox";
            this.KeyStorageLocationTextBox.Size = new System.Drawing.Size(460, 26);
            this.KeyStorageLocationTextBox.TabIndex = 13;
            this.KeyStorageLocationTextBox.Tag = "Byte(100);ItemName(鍵保管場所)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Aquamarine;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(1, 389);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(188, 38);
            this.label11.TabIndex = 18;
            this.label11.Text = "鍵保管場所";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CarStorageLocationTextBox
            // 
            this.CarStorageLocationTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CarStorageLocationTextBox.Location = new System.Drawing.Point(194, 354);
            this.CarStorageLocationTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CarStorageLocationTextBox.MaxLength = 150;
            this.CarStorageLocationTextBox.Name = "CarStorageLocationTextBox";
            this.CarStorageLocationTextBox.Size = new System.Drawing.Size(460, 26);
            this.CarStorageLocationTextBox.TabIndex = 12;
            this.CarStorageLocationTextBox.Tag = "Byte(150);ItemName(車両保管場所)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Aquamarine;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(1, 350);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(188, 38);
            this.label10.TabIndex = 16;
            this.label10.Text = "車両保管場所";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChangePlaceTextBox
            // 
            this.ChangePlaceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangePlaceTextBox.Location = new System.Drawing.Point(194, 315);
            this.ChangePlaceTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ChangePlaceTextBox.MaxLength = 500;
            this.ChangePlaceTextBox.Name = "ChangePlaceTextBox";
            this.ChangePlaceTextBox.Size = new System.Drawing.Size(460, 26);
            this.ChangePlaceTextBox.TabIndex = 11;
            this.ChangePlaceTextBox.Tag = "Byte(500);ItemName(変更箇所有無（ソフト含む）と内容)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Aquamarine;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label9.Location = new System.Drawing.Point(1, 311);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(188, 38);
            this.label9.TabIndex = 14;
            this.label9.Text = "変更箇所有無（ソフト含む）と内容";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DesorptionPpartsTextBox
            // 
            this.DesorptionPpartsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DesorptionPpartsTextBox.Location = new System.Drawing.Point(194, 276);
            this.DesorptionPpartsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DesorptionPpartsTextBox.MaxLength = 500;
            this.DesorptionPpartsTextBox.Name = "DesorptionPpartsTextBox";
            this.DesorptionPpartsTextBox.Size = new System.Drawing.Size(460, 26);
            this.DesorptionPpartsTextBox.TabIndex = 10;
            this.DesorptionPpartsTextBox.Tag = "Byte(500);ItemName(脱着部品と復元有無)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Aquamarine;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(1, 272);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(188, 38);
            this.label8.TabIndex = 12;
            this.label8.Text = "脱着部品と復元有無";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TestContentTextBox
            // 
            this.TestContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestContentTextBox.Location = new System.Drawing.Point(194, 237);
            this.TestContentTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestContentTextBox.MaxLength = 400;
            this.TestContentTextBox.Name = "TestContentTextBox";
            this.TestContentTextBox.Size = new System.Drawing.Size(460, 26);
            this.TestContentTextBox.TabIndex = 9;
            this.TestContentTextBox.Tag = "Byte(400);ItemName(試験実施内容)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Aquamarine;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(1, 233);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 38);
            this.label7.TabIndex = 10;
            this.label7.Text = "試験実施内容";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OdometerTextBox
            // 
            this.OdometerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OdometerTextBox.Location = new System.Drawing.Point(194, 198);
            this.OdometerTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OdometerTextBox.MaxLength = 20;
            this.OdometerTextBox.Name = "OdometerTextBox";
            this.OdometerTextBox.Size = new System.Drawing.Size(460, 26);
            this.OdometerTextBox.TabIndex = 8;
            this.OdometerTextBox.Tag = "Byte(20);ItemName(オドメータ)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Aquamarine;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(1, 194);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 38);
            this.label6.TabIndex = 8;
            this.label6.Text = "オドメータ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Aquamarine;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(1, 155);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 38);
            this.label5.TabIndex = 6;
            this.label5.Text = "作業完了日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Aquamarine;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(1, 79);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 75);
            this.label4.TabIndex = 4;
            this.label4.Text = "スケジュール名";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TypePanel
            // 
            this.TypePanel.Controls.Add(this.TriangleRadioButton);
            this.TypePanel.Controls.Add(this.DoubleCircleRadioButton);
            this.TypePanel.Controls.Add(this.SquareRadioButton);
            this.TypePanel.Controls.Add(this.DefaultRadioButton);
            this.TypePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TypePanel.Location = new System.Drawing.Point(190, 40);
            this.TypePanel.Margin = new System.Windows.Forms.Padding(0);
            this.TypePanel.Name = "TypePanel";
            this.TypePanel.Size = new System.Drawing.Size(468, 38);
            this.TypePanel.TabIndex = 3;
            this.TypePanel.Tag = "Required";
            // 
            // TriangleRadioButton
            // 
            this.TriangleRadioButton.AutoSize = true;
            this.TriangleRadioButton.Location = new System.Drawing.Point(364, 8);
            this.TriangleRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TriangleRadioButton.Name = "TriangleRadioButton";
            this.TriangleRadioButton.Size = new System.Drawing.Size(110, 23);
            this.TriangleRadioButton.TabIndex = 4;
            this.TriangleRadioButton.Tag = "3";
            this.TriangleRadioButton.Text = "▲(その他)";
            this.TriangleRadioButton.UseVisualStyleBackColor = true;
            // 
            // DoubleCircleRadioButton
            // 
            this.DoubleCircleRadioButton.AutoSize = true;
            this.DoubleCircleRadioButton.Location = new System.Drawing.Point(231, 8);
            this.DoubleCircleRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DoubleCircleRadioButton.Name = "DoubleCircleRadioButton";
            this.DoubleCircleRadioButton.Size = new System.Drawing.Size(137, 23);
            this.DoubleCircleRadioButton.TabIndex = 5;
            this.DoubleCircleRadioButton.Tag = "4";
            this.DoubleCircleRadioButton.Text = "◎(イベント等)";
            this.DoubleCircleRadioButton.UseVisualStyleBackColor = true;
            // 
            // SquareRadioButton
            // 
            this.SquareRadioButton.AutoSize = true;
            this.SquareRadioButton.Location = new System.Drawing.Point(118, 8);
            this.SquareRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SquareRadioButton.Name = "SquareRadioButton";
            this.SquareRadioButton.Size = new System.Drawing.Size(118, 23);
            this.SquareRadioButton.TabIndex = 3;
            this.SquareRadioButton.Tag = "2";
            this.SquareRadioButton.Text = "■(改修等)";
            this.SquareRadioButton.UseVisualStyleBackColor = true;
            // 
            // DefaultRadioButton
            // 
            this.DefaultRadioButton.AutoSize = true;
            this.DefaultRadioButton.Checked = true;
            this.DefaultRadioButton.Location = new System.Drawing.Point(5, 8);
            this.DefaultRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DefaultRadioButton.Name = "DefaultRadioButton";
            this.DefaultRadioButton.Size = new System.Drawing.Size(121, 23);
            this.DefaultRadioButton.TabIndex = 2;
            this.DefaultRadioButton.TabStop = true;
            this.DefaultRadioButton.Tag = "1";
            this.DefaultRadioButton.Text = "スケジュール";
            this.DefaultRadioButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(1, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 38);
            this.label3.TabIndex = 2;
            this.label3.Text = "区分";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Aquamarine;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 38);
            this.label2.TabIndex = 0;
            this.label2.Text = "予約状態";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ReservationPanel
            // 
            this.ReservationPanel.Controls.Add(this.ReservationRadioButton);
            this.ReservationPanel.Controls.Add(this.TentativeReservationRadioButton);
            this.ReservationPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ReservationPanel.Location = new System.Drawing.Point(190, 1);
            this.ReservationPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ReservationPanel.Name = "ReservationPanel";
            this.ReservationPanel.Size = new System.Drawing.Size(468, 38);
            this.ReservationPanel.TabIndex = 1;
            this.ReservationPanel.Tag = "Required";
            // 
            // ReservationRadioButton
            // 
            this.ReservationRadioButton.AutoSize = true;
            this.ReservationRadioButton.Location = new System.Drawing.Point(100, 8);
            this.ReservationRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ReservationRadioButton.Name = "ReservationRadioButton";
            this.ReservationRadioButton.Size = new System.Drawing.Size(87, 23);
            this.ReservationRadioButton.TabIndex = 1;
            this.ReservationRadioButton.Tag = "本予約";
            this.ReservationRadioButton.Text = "本予約";
            this.ReservationRadioButton.UseVisualStyleBackColor = true;
            // 
            // TentativeReservationRadioButton
            // 
            this.TentativeReservationRadioButton.AutoSize = true;
            this.TentativeReservationRadioButton.Checked = true;
            this.TentativeReservationRadioButton.Location = new System.Drawing.Point(5, 8);
            this.TentativeReservationRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TentativeReservationRadioButton.Name = "TentativeReservationRadioButton";
            this.TentativeReservationRadioButton.Size = new System.Drawing.Size(87, 23);
            this.TentativeReservationRadioButton.TabIndex = 0;
            this.TentativeReservationRadioButton.TabStop = true;
            this.TentativeReservationRadioButton.Tag = "仮予約";
            this.TentativeReservationRadioButton.Text = "仮予約";
            this.TentativeReservationRadioButton.UseVisualStyleBackColor = true;
            // 
            // ScheduleNameTextBox
            // 
            this.ScheduleNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScheduleNameTextBox.Location = new System.Drawing.Point(194, 83);
            this.ScheduleNameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ScheduleNameTextBox.MaxLength = 200;
            this.ScheduleNameTextBox.Multiline = true;
            this.ScheduleNameTextBox.Name = "ScheduleNameTextBox";
            this.ScheduleNameTextBox.Size = new System.Drawing.Size(460, 67);
            this.ScheduleNameTextBox.TabIndex = 6;
            this.ScheduleNameTextBox.Tag = "Required;Wide(200);ItemName(スケジュール名)";
            // 
            // WorkCompletionDateTimePicker
            // 
            this.WorkCompletionDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.WorkCompletionDateTimePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.WorkCompletionDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.WorkCompletionDateTimePicker.Location = new System.Drawing.Point(194, 159);
            this.WorkCompletionDateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.WorkCompletionDateTimePicker.Name = "WorkCompletionDateTimePicker";
            this.WorkCompletionDateTimePicker.Size = new System.Drawing.Size(124, 26);
            this.WorkCompletionDateTimePicker.TabIndex = 7;
            this.WorkCompletionDateTimePicker.Tag = "ItemName(作業完了日)";
            this.WorkCompletionDateTimePicker.Value = new System.DateTime(2024, 11, 15, 0, 0, 0, 0);
            this.WorkCompletionDateTimePicker.ValueChanged += new System.EventHandler(this.WorkCompletionDateTimePicker_ValueChanged);
            // 
            // ReservedPersonLabel
            // 
            this.ReservedPersonLabel.AutoSize = true;
            this.ReservedPersonLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReservedPersonLabel.Location = new System.Drawing.Point(194, 467);
            this.ReservedPersonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ReservedPersonLabel.Name = "ReservedPersonLabel";
            this.ReservedPersonLabel.Size = new System.Drawing.Size(460, 38);
            this.ReservedPersonLabel.TabIndex = 28;
            this.ReservedPersonLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(6, 619);
            this.EntryButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(150, 38);
            this.EntryButton.TabIndex = 19;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Location = new System.Drawing.Point(164, 619);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(150, 38);
            this.DeleteButton.TabIndex = 20;
            this.DeleteButton.Text = "削除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // TestCarScheduleDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(680, 664);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EntryButton);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "TestCarScheduleDetailForm";
            this.Text = "TestCarScheduleDetailForm";
            this.Load += new System.EventHandler(this.TestCarScheduleDetailForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.DeleteButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            this.DatePanel.ResumeLayout(false);
            this.DatePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).EndInit();
            this.TypePanel.ResumeLayout(false);
            this.TypePanel.PerformLayout();
            this.ReservationPanel.ResumeLayout(false);
            this.ReservationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkCompletionDateTimePicker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel ReservationPanel;
        private System.Windows.Forms.RadioButton ReservationRadioButton;
        private System.Windows.Forms.RadioButton TentativeReservationRadioButton;
        private System.Windows.Forms.Panel TypePanel;
        private System.Windows.Forms.RadioButton DoubleCircleRadioButton;
        private System.Windows.Forms.RadioButton SquareRadioButton;
        private System.Windows.Forms.RadioButton DefaultRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox KeyStorageLocationTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox CarStorageLocationTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ChangePlaceTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox DesorptionPpartsTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TestContentTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox OdometerTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ScheduleNameTextBox;
        private UC.NullableDateTimePicker WorkCompletionDateTimePicker;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel DatePanel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox EndTimeComboBox;
        private UC.NullableDateTimePicker EndDayDateTimePicker;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox StartTimeComboBox;
        private UC.NullableDateTimePicker StartDayDateTimePicker;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Label ReservedPersonLabel;
        private System.Windows.Forms.ComboBox CarUserComboBox;
        private System.Windows.Forms.RadioButton TriangleRadioButton;
    }
}