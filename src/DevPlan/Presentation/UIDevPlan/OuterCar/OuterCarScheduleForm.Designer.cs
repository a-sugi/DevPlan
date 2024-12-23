namespace DevPlan.Presentation.UIDevPlan.OuterCar
{
    partial class OuterCarScheduleForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.EntryButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DestinationComboBox = new System.Windows.Forms.ComboBox();
            this.PurposeComboBox = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.RentalNGRadioButton = new System.Windows.Forms.RadioButton();
            this.RentalOKRadioButton = new System.Windows.Forms.RadioButton();
            this.ParkingNoLabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.TelTextBox = new System.Windows.Forms.TextBox();
            this.ReservationPanel = new System.Windows.Forms.Panel();
            this.ReservationRadioButton = new System.Windows.Forms.RadioButton();
            this.TentativeReservationRadioButton = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CarNameLabel = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TypePanel = new System.Windows.Forms.Panel();
            this.TriangleRadioButton = new System.Windows.Forms.RadioButton();
            this.SquareRadioButton = new System.Windows.Forms.RadioButton();
            this.DefaultRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.車両名 = new System.Windows.Forms.Label();
            this.ReservedPersonLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.DatePanel = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.EndTimeComboBox = new System.Windows.Forms.ComboBox();
            this.EndDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.StartTimeComboBox = new System.Windows.Forms.ComboBox();
            this.StartDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.TitleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            this.panel6.SuspendLayout();
            this.ReservationPanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.TypePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.DatePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(448, 399);
            this.CloseButton.TabIndex = 1005;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.ListFormMainPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ListFormMainPanel.Size = new System.Drawing.Size(563, 389);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DetailTableLayoutPanel, 0);
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
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 399);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 1000;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Location = new System.Drawing.Point(131, 399);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(120, 30);
            this.DeleteButton.TabIndex = 1001;
            this.DeleteButton.Text = "削除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.Controls.Add(this.DestinationComboBox, 1, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.PurposeComboBox, 1, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.panel6, 1, 7);
            this.DetailTableLayoutPanel.Controls.Add(this.ParkingNoLabel, 1, 10);
            this.DetailTableLayoutPanel.Controls.Add(this.label16, 0, 10);
            this.DetailTableLayoutPanel.Controls.Add(this.TelTextBox, 1, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.ReservationPanel, 1, 8);
            this.DetailTableLayoutPanel.Controls.Add(this.panel5, 0, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.panel4, 0, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.panel3, 0, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.panel2, 0, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.CarNameLabel, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.label17, 0, 8);
            this.DetailTableLayoutPanel.Controls.Add(this.label15, 0, 9);
            this.DetailTableLayoutPanel.Controls.Add(this.label11, 0, 7);
            this.DetailTableLayoutPanel.Controls.Add(this.TypePanel, 1, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.label3, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.車両名, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.ReservedPersonLabel, 1, 9);
            this.DetailTableLayoutPanel.Controls.Add(this.panel1, 0, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.DatePanel, 1, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.TitleLabel, 1, 2);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(3, 39);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 11;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(557, 344);
            this.DetailTableLayoutPanel.TabIndex = 1013;
            // 
            // DestinationComboBox
            // 
            this.DestinationComboBox.DisplayMember = "行先";
            this.DestinationComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.DestinationComboBox.FormattingEnabled = true;
            this.DestinationComboBox.Location = new System.Drawing.Point(155, 159);
            this.DestinationComboBox.MaxLength = 100;
            this.DestinationComboBox.Name = "DestinationComboBox";
            this.DestinationComboBox.Size = new System.Drawing.Size(144, 23);
            this.DestinationComboBox.TabIndex = 28;
            this.DestinationComboBox.Tag = "Required;Byte(100);ItemName(行先)";
            this.DestinationComboBox.ValueMember = "行先";
            // 
            // PurposeComboBox
            // 
            this.PurposeComboBox.DisplayMember = "目的";
            this.PurposeComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.PurposeComboBox.FormattingEnabled = true;
            this.PurposeComboBox.Location = new System.Drawing.Point(155, 128);
            this.PurposeComboBox.MaxLength = 100;
            this.PurposeComboBox.Name = "PurposeComboBox";
            this.PurposeComboBox.Size = new System.Drawing.Size(144, 23);
            this.PurposeComboBox.TabIndex = 27;
            this.PurposeComboBox.Tag = "Required;Byte(100);ItemName(目的)";
            this.PurposeComboBox.ValueMember = "目的";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.RentalNGRadioButton);
            this.panel6.Controls.Add(this.RentalOKRadioButton);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(152, 218);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(404, 30);
            this.panel6.TabIndex = 1024;
            this.panel6.Tag = "Required";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label19.Location = new System.Drawing.Point(99, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(284, 14);
            this.label19.TabIndex = 2;
            this.label19.Text = "※試験都合などで空き時間中に貸出可能かどうか";
            // 
            // RentalNGRadioButton
            // 
            this.RentalNGRadioButton.AutoSize = true;
            this.RentalNGRadioButton.Location = new System.Drawing.Point(46, 6);
            this.RentalNGRadioButton.Name = "RentalNGRadioButton";
            this.RentalNGRadioButton.Size = new System.Drawing.Size(55, 19);
            this.RentalNGRadioButton.TabIndex = 31;
            this.RentalNGRadioButton.Tag = "";
            this.RentalNGRadioButton.Text = "不可";
            this.RentalNGRadioButton.UseVisualStyleBackColor = true;
            this.RentalNGRadioButton.CheckedChanged += new System.EventHandler(this.RentalNGRadioButton_CheckedChanged);
            // 
            // RentalOKRadioButton
            // 
            this.RentalOKRadioButton.AutoSize = true;
            this.RentalOKRadioButton.Checked = true;
            this.RentalOKRadioButton.Location = new System.Drawing.Point(4, 6);
            this.RentalOKRadioButton.Name = "RentalOKRadioButton";
            this.RentalOKRadioButton.Size = new System.Drawing.Size(40, 19);
            this.RentalOKRadioButton.TabIndex = 30;
            this.RentalOKRadioButton.TabStop = true;
            this.RentalOKRadioButton.Tag = "1";
            this.RentalOKRadioButton.Text = "可";
            this.RentalOKRadioButton.UseVisualStyleBackColor = true;
            this.RentalOKRadioButton.CheckedChanged += new System.EventHandler(this.RentalOKRadioButton_CheckedChanged);
            // 
            // ParkingNoLabel
            // 
            this.ParkingNoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParkingNoLabel.Location = new System.Drawing.Point(155, 311);
            this.ParkingNoLabel.Name = "ParkingNoLabel";
            this.ParkingNoLabel.Size = new System.Drawing.Size(398, 32);
            this.ParkingNoLabel.TabIndex = 1022;
            this.ParkingNoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Aquamarine;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(1, 311);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(150, 32);
            this.label16.TabIndex = 1020;
            this.label16.Text = "駐車場番号";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TelTextBox
            // 
            this.TelTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.TelTextBox.Location = new System.Drawing.Point(155, 190);
            this.TelTextBox.MaxLength = 100;
            this.TelTextBox.Name = "TelTextBox";
            this.TelTextBox.Size = new System.Drawing.Size(144, 22);
            this.TelTextBox.TabIndex = 29;
            this.TelTextBox.Tag = "Required;Byte(100);ItemName(使用者TEL)";
            // 
            // ReservationPanel
            // 
            this.ReservationPanel.Controls.Add(this.ReservationRadioButton);
            this.ReservationPanel.Controls.Add(this.TentativeReservationRadioButton);
            this.ReservationPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ReservationPanel.Location = new System.Drawing.Point(152, 249);
            this.ReservationPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ReservationPanel.Name = "ReservationPanel";
            this.ReservationPanel.Size = new System.Drawing.Size(404, 30);
            this.ReservationPanel.TabIndex = 1018;
            this.ReservationPanel.Tag = "Required";
            // 
            // ReservationRadioButton
            // 
            this.ReservationRadioButton.AutoSize = true;
            this.ReservationRadioButton.Location = new System.Drawing.Point(80, 6);
            this.ReservationRadioButton.Name = "ReservationRadioButton";
            this.ReservationRadioButton.Size = new System.Drawing.Size(70, 19);
            this.ReservationRadioButton.TabIndex = 33;
            this.ReservationRadioButton.Tag = "本予約";
            this.ReservationRadioButton.Text = "本予約";
            this.ReservationRadioButton.UseVisualStyleBackColor = true;
            // 
            // TentativeReservationRadioButton
            // 
            this.TentativeReservationRadioButton.AutoSize = true;
            this.TentativeReservationRadioButton.Checked = true;
            this.TentativeReservationRadioButton.Location = new System.Drawing.Point(4, 6);
            this.TentativeReservationRadioButton.Name = "TentativeReservationRadioButton";
            this.TentativeReservationRadioButton.Size = new System.Drawing.Size(70, 19);
            this.TentativeReservationRadioButton.TabIndex = 32;
            this.TentativeReservationRadioButton.TabStop = true;
            this.TentativeReservationRadioButton.Tag = "仮予約";
            this.TentativeReservationRadioButton.Text = "仮予約";
            this.TentativeReservationRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Aquamarine;
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Location = new System.Drawing.Point(1, 187);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(150, 30);
            this.panel5.TabIndex = 1017;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(82, -1);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 31);
            this.label10.TabIndex = 1014;
            this.label10.Text = "(※)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(0, -1);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 31);
            this.label12.TabIndex = 1013;
            this.label12.Text = "使用者TEL";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Aquamarine;
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(1, 156);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(150, 30);
            this.panel4.TabIndex = 1016;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(45, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 31);
            this.label8.TabIndex = 1014;
            this.label8.Text = "(※)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, -1);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 31);
            this.label9.TabIndex = 1013;
            this.label9.Text = "行先";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Aquamarine;
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(1, 125);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(150, 30);
            this.panel3.TabIndex = 1015;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(45, -1);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 31);
            this.label6.TabIndex = 1014;
            this.label6.Text = "(※)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, -1);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 31);
            this.label7.TabIndex = 1013;
            this.label7.Text = "目的";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Aquamarine;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(1, 94);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 30);
            this.panel2.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(45, -1);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 31);
            this.label4.TabIndex = 1014;
            this.label4.Text = "(※)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, -1);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 31);
            this.label5.TabIndex = 1013;
            this.label5.Text = "期間";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CarNameLabel
            // 
            this.CarNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CarNameLabel.Location = new System.Drawing.Point(155, 1);
            this.CarNameLabel.Name = "CarNameLabel";
            this.CarNameLabel.Size = new System.Drawing.Size(398, 30);
            this.CarNameLabel.TabIndex = 29;
            this.CarNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Aquamarine;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(1, 249);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(150, 30);
            this.label17.TabIndex = 24;
            this.label17.Text = "ステータス";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Aquamarine;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(1, 280);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(150, 30);
            this.label15.TabIndex = 22;
            this.label15.Text = "予約者";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Aquamarine;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(1, 218);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(150, 30);
            this.label11.TabIndex = 18;
            this.label11.Text = "空き時間貸出";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TypePanel
            // 
            this.TypePanel.Controls.Add(this.TriangleRadioButton);
            this.TypePanel.Controls.Add(this.SquareRadioButton);
            this.TypePanel.Controls.Add(this.DefaultRadioButton);
            this.TypePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TypePanel.Location = new System.Drawing.Point(152, 32);
            this.TypePanel.Margin = new System.Windows.Forms.Padding(0);
            this.TypePanel.Name = "TypePanel";
            this.TypePanel.Size = new System.Drawing.Size(404, 30);
            this.TypePanel.TabIndex = 3;
            this.TypePanel.Tag = "Required";
            // 
            // TriangleRadioButton
            // 
            this.TriangleRadioButton.AutoSize = true;
            this.TriangleRadioButton.Location = new System.Drawing.Point(205, 6);
            this.TriangleRadioButton.Name = "TriangleRadioButton";
            this.TriangleRadioButton.Size = new System.Drawing.Size(88, 19);
            this.TriangleRadioButton.TabIndex = 22;
            this.TriangleRadioButton.Tag = "3";
            this.TriangleRadioButton.Text = "▲(その他)";
            this.TriangleRadioButton.UseVisualStyleBackColor = true;
            // 
            // SquareRadioButton
            // 
            this.SquareRadioButton.AutoSize = true;
            this.SquareRadioButton.Location = new System.Drawing.Point(104, 6);
            this.SquareRadioButton.Name = "SquareRadioButton";
            this.SquareRadioButton.Size = new System.Drawing.Size(95, 19);
            this.SquareRadioButton.TabIndex = 21;
            this.SquareRadioButton.Tag = "2";
            this.SquareRadioButton.Text = "■(改修等)";
            this.SquareRadioButton.UseVisualStyleBackColor = true;
            // 
            // DefaultRadioButton
            // 
            this.DefaultRadioButton.AutoSize = true;
            this.DefaultRadioButton.Checked = true;
            this.DefaultRadioButton.Location = new System.Drawing.Point(4, 6);
            this.DefaultRadioButton.Name = "DefaultRadioButton";
            this.DefaultRadioButton.Size = new System.Drawing.Size(94, 19);
            this.DefaultRadioButton.TabIndex = 20;
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
            this.label3.Location = new System.Drawing.Point(1, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "区分";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 車両名
            // 
            this.車両名.AutoSize = true;
            this.車両名.BackColor = System.Drawing.Color.Aquamarine;
            this.車両名.Dock = System.Windows.Forms.DockStyle.Fill;
            this.車両名.Location = new System.Drawing.Point(1, 1);
            this.車両名.Margin = new System.Windows.Forms.Padding(0);
            this.車両名.Name = "車両名";
            this.車両名.Size = new System.Drawing.Size(150, 30);
            this.車両名.TabIndex = 0;
            this.車両名.Text = "外製車名";
            this.車両名.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ReservedPersonLabel
            // 
            this.ReservedPersonLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReservedPersonLabel.Location = new System.Drawing.Point(155, 280);
            this.ReservedPersonLabel.Name = "ReservedPersonLabel";
            this.ReservedPersonLabel.Size = new System.Drawing.Size(398, 30);
            this.ReservedPersonLabel.TabIndex = 28;
            this.ReservedPersonLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Aquamarine;
            this.panel1.Controls.Add(this.label13);
            this.panel1.Location = new System.Drawing.Point(1, 63);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 30);
            this.panel1.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(0, 7);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 15);
            this.label13.TabIndex = 1013;
            this.label13.Text = "タイトル";
            // 
            // DatePanel
            // 
            this.DatePanel.Controls.Add(this.label14);
            this.DatePanel.Controls.Add(this.EndTimeComboBox);
            this.DatePanel.Controls.Add(this.EndDayDateTimePicker);
            this.DatePanel.Controls.Add(this.label20);
            this.DatePanel.Controls.Add(this.StartTimeComboBox);
            this.DatePanel.Controls.Add(this.StartDayDateTimePicker);
            this.DatePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.DatePanel.Location = new System.Drawing.Point(152, 94);
            this.DatePanel.Margin = new System.Windows.Forms.Padding(0);
            this.DatePanel.Name = "DatePanel";
            this.DatePanel.Size = new System.Drawing.Size(404, 30);
            this.DatePanel.TabIndex = 21;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(348, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 15);
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
            this.EndTimeComboBox.Location = new System.Drawing.Point(306, 4);
            this.EndTimeComboBox.Name = "EndTimeComboBox";
            this.EndTimeComboBox.Size = new System.Drawing.Size(41, 23);
            this.EndTimeComboBox.TabIndex = 26;
            this.EndTimeComboBox.Tag = "Required;ItemName(期間Toの時刻)";
            // 
            // EndDayDateTimePicker
            // 
            this.EndDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EndDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDayDateTimePicker.Location = new System.Drawing.Point(200, 4);
            this.EndDayDateTimePicker.Name = "EndDayDateTimePicker";
            this.EndDayDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.EndDayDateTimePicker.TabIndex = 25;
            this.EndDayDateTimePicker.Tag = "Required;ItemName(期間Toの日付)";
            this.EndDayDateTimePicker.Value = new System.DateTime(2021, 10, 29, 0, 0, 0, 0);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(152, 7);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(42, 15);
            this.label20.TabIndex = 10;
            this.label20.Text = "時 ～";
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
            this.StartTimeComboBox.Location = new System.Drawing.Point(105, 4);
            this.StartTimeComboBox.Name = "StartTimeComboBox";
            this.StartTimeComboBox.Size = new System.Drawing.Size(41, 23);
            this.StartTimeComboBox.TabIndex = 24;
            this.StartTimeComboBox.Tag = "Required;ItemName(期間Fromの時刻)";
            // 
            // StartDayDateTimePicker
            // 
            this.StartDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.StartDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDayDateTimePicker.Location = new System.Drawing.Point(3, 4);
            this.StartDayDateTimePicker.Name = "StartDayDateTimePicker";
            this.StartDayDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.StartDayDateTimePicker.TabIndex = 23;
            this.StartDayDateTimePicker.Tag = "Required;ItemName(期間Fromの日付)";
            this.StartDayDateTimePicker.Value = new System.DateTime(2021, 10, 29, 0, 0, 0, 0);
            // 
            // TitleLabel
            // 
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleLabel.Location = new System.Drawing.Point(155, 63);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(398, 30);
            this.TitleLabel.TabIndex = 1027;
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OuterCarScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(574, 433);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EntryButton);
            this.Name = "OuterCarScheduleForm";
            this.Text = "OuterCarScheduleForm";
            this.Load += new System.EventHandler(this.OuterCarDetailForm_Load);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.DeleteButton, 0);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ReservationPanel.ResumeLayout(false);
            this.ReservationPanel.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.TypePanel.ResumeLayout(false);
            this.TypePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.DatePanel.ResumeLayout(false);
            this.DatePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.ComboBox DestinationComboBox;
        private System.Windows.Forms.ComboBox PurposeComboBox;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RadioButton RentalNGRadioButton;
        private System.Windows.Forms.RadioButton RentalOKRadioButton;
        private System.Windows.Forms.Label ParkingNoLabel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox TelTextBox;
        private System.Windows.Forms.Panel ReservationPanel;
        private System.Windows.Forms.RadioButton ReservationRadioButton;
        private System.Windows.Forms.RadioButton TentativeReservationRadioButton;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label CarNameLabel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel TypePanel;
        private System.Windows.Forms.RadioButton SquareRadioButton;
        private System.Windows.Forms.RadioButton DefaultRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label 車両名;
        private System.Windows.Forms.Label ReservedPersonLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel DatePanel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox EndTimeComboBox;
        private UC.NullableDateTimePicker EndDayDateTimePicker;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox StartTimeComboBox;
        private UC.NullableDateTimePicker StartDayDateTimePicker;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.RadioButton TriangleRadioButton;
    }
}