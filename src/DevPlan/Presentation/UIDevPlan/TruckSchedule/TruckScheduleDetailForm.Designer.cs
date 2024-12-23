namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    partial class TruckScheduleDetailForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FreeTimeMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.freeTimeMultiRowTemplate1 = new DevPlan.Presentation.UIDevPlan.TruckSchedule.FreeTimeMultiRowTemplate();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.RowDeleteButton = new System.Windows.Forms.Button();
            this.RowAddButton = new System.Windows.Forms.Button();
            this.FrequentlyUsedDestinationsButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SectionMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.sectionMultiRowTemplate1 = new DevPlan.Presentation.UIDevPlan.TruckSchedule.SectionMultiRowTemplate();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.NoRadioButton = new System.Windows.Forms.RadioButton();
            this.YesRadioButton = new System.Windows.Forms.RadioButton();
            this.CarNameLabel = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.DriverAComboBox = new System.Windows.Forms.ComboBox();
            this.DriverATextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.DriverBComboBox = new System.Windows.Forms.ComboBox();
            this.DriverBTextBox = new System.Windows.Forms.TextBox();
            this.DatePanel = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.EndTimeComboBox = new System.Windows.Forms.ComboBox();
            this.EndDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.StartTimeComboBox = new System.Windows.Forms.ComboBox();
            this.StartDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ReservedPersonComboBox = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.EditUserLabel = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.PurposeOfUseTextBox = new System.Windows.Forms.TextBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.EntryButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FreeTimeMultiRow)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionMultiRow)).BeginInit();
            this.DetailTableLayoutPanel.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.DatePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(546, 561);
            this.CloseButton.TabIndex = 3;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.TabIndex = 1;
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.groupBox2);
            this.ListFormMainPanel.Controls.Add(this.groupBox1);
            this.ListFormMainPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ListFormMainPanel.Size = new System.Drawing.Size(661, 551);
            this.ListFormMainPanel.TabIndex = 0;
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DetailTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.groupBox1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.groupBox2, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FreeTimeMultiRow);
            this.groupBox2.Location = new System.Drawing.Point(388, 271);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 273);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "＜空き時間＞";
            // 
            // FreeTimeMultiRow
            // 
            this.FreeTimeMultiRow.AllowAutoExtend = true;
            this.FreeTimeMultiRow.Location = new System.Drawing.Point(6, 23);
            this.FreeTimeMultiRow.MultiSelect = false;
            this.FreeTimeMultiRow.Name = "FreeTimeMultiRow";
            this.FreeTimeMultiRow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FreeTimeMultiRow.Size = new System.Drawing.Size(240, 242);
            this.FreeTimeMultiRow.TabIndex = 13;
            this.FreeTimeMultiRow.TabStop = false;
            this.FreeTimeMultiRow.Template = this.freeTimeMultiRowTemplate1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.RowDeleteButton);
            this.groupBox1.Controls.Add(this.RowAddButton);
            this.groupBox1.Controls.Add(this.FrequentlyUsedDestinationsButton);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.SectionMultiRow);
            this.groupBox1.Location = new System.Drawing.Point(10, 271);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 274);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "＜運行区間＞";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(97, -7);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(35, 31);
            this.label21.TabIndex = 5;
            this.label21.Text = "(※)";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RowDeleteButton
            // 
            this.RowDeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowDeleteButton.Location = new System.Drawing.Point(291, 23);
            this.RowDeleteButton.Name = "RowDeleteButton";
            this.RowDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.RowDeleteButton.TabIndex = 2;
            this.RowDeleteButton.Text = "行削除";
            this.RowDeleteButton.UseVisualStyleBackColor = false;
            this.RowDeleteButton.Click += new System.EventHandler(this.RowDeleteButton_Click);
            // 
            // RowAddButton
            // 
            this.RowAddButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowAddButton.Location = new System.Drawing.Point(210, 23);
            this.RowAddButton.Name = "RowAddButton";
            this.RowAddButton.Size = new System.Drawing.Size(75, 23);
            this.RowAddButton.TabIndex = 1;
            this.RowAddButton.Text = "行追加";
            this.RowAddButton.UseVisualStyleBackColor = false;
            this.RowAddButton.Click += new System.EventHandler(this.RowAddButton_Click);
            // 
            // FrequentlyUsedDestinationsButton
            // 
            this.FrequentlyUsedDestinationsButton.BackColor = System.Drawing.SystemColors.Control;
            this.FrequentlyUsedDestinationsButton.Location = new System.Drawing.Point(8, 23);
            this.FrequentlyUsedDestinationsButton.Name = "FrequentlyUsedDestinationsButton";
            this.FrequentlyUsedDestinationsButton.Size = new System.Drawing.Size(124, 23);
            this.FrequentlyUsedDestinationsButton.TabIndex = 0;
            this.FrequentlyUsedDestinationsButton.Text = "よく使う目的地";
            this.FrequentlyUsedDestinationsButton.UseVisualStyleBackColor = false;
            this.FrequentlyUsedDestinationsButton.Click += new System.EventHandler(this.FrequentlyUsedDestinationsButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(6, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(360, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "※積荷のない区間は「空荷」にチェックを入れてください。";
            // 
            // SectionMultiRow
            // 
            this.SectionMultiRow.AllowAutoExtend = true;
            this.SectionMultiRow.AllowUserToAddRows = false;
            this.SectionMultiRow.Location = new System.Drawing.Point(8, 52);
            this.SectionMultiRow.MultiSelect = false;
            this.SectionMultiRow.Name = "SectionMultiRow";
            this.SectionMultiRow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SectionMultiRow.Size = new System.Drawing.Size(358, 195);
            this.SectionMultiRow.TabIndex = 12;
            this.SectionMultiRow.Tag = "ItemName(運行区間)";
            this.SectionMultiRow.Template = this.sectionMultiRowTemplate1;
            this.SectionMultiRow.CellEditedFormattedValueChanged += new System.EventHandler<GrapeCity.Win.MultiRow.CellEditedFormattedValueChangedEventArgs>(this.SectionMultiRow_CellEditedFormattedValueChanged);
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.Controls.Add(this.panel8, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.panel7, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.panel6, 0, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.panel5, 0, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.panel1, 1, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.label5, 0, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.panel2, 1, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.DatePanel, 1, 5);
            this.DetailTableLayoutPanel.Controls.Add(this.label15, 0, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.panel3, 1, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.panel9, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.panel4, 0, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.EditUserLabel, 1, 6);
            this.DetailTableLayoutPanel.Controls.Add(this.panel10, 1, 4);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(6, 39);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 7;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(648, 225);
            this.DetailTableLayoutPanel.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Aquamarine;
            this.panel8.Controls.Add(this.label2);
            this.panel8.Controls.Add(this.label23);
            this.panel8.Controls.Add(this.label24);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(1, 1);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(150, 30);
            this.panel8.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(92, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 31);
            this.label2.TabIndex = 13;
            this.label2.Text = "(※)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(0, 2);
            this.label23.Margin = new System.Windows.Forms.Padding(0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(82, 31);
            this.label23.TabIndex = 14;
            this.label23.Text = "トラック名";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label24.Location = new System.Drawing.Point(4, 37);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(144, 67);
            this.label24.TabIndex = 15;
            this.label24.Text = "<電話番号記入例>\r\n\r\n内線：8-22-○○○ or\r\n携帯：090-○○○-○○○";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label22);
            this.panel7.Controls.Add(this.NoRadioButton);
            this.panel7.Controls.Add(this.YesRadioButton);
            this.panel7.Controls.Add(this.CarNameLabel);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(152, 1);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(496, 30);
            this.panel7.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label22.Location = new System.Drawing.Point(236, 7);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(111, 15);
            this.label22.TabIndex = 17;
            this.label22.Text = "機密車ですか？";
            // 
            // NoRadioButton
            // 
            this.NoRadioButton.AutoSize = true;
            this.NoRadioButton.Location = new System.Drawing.Point(408, 6);
            this.NoRadioButton.Name = "NoRadioButton";
            this.NoRadioButton.Size = new System.Drawing.Size(61, 19);
            this.NoRadioButton.TabIndex = 1;
            this.NoRadioButton.TabStop = true;
            this.NoRadioButton.Tag = "ItemName(機密車)";
            this.NoRadioButton.Text = "いいえ";
            this.NoRadioButton.UseVisualStyleBackColor = true;
            // 
            // YesRadioButton
            // 
            this.YesRadioButton.AutoSize = true;
            this.YesRadioButton.Location = new System.Drawing.Point(352, 6);
            this.YesRadioButton.Name = "YesRadioButton";
            this.YesRadioButton.Size = new System.Drawing.Size(50, 19);
            this.YesRadioButton.TabIndex = 0;
            this.YesRadioButton.TabStop = true;
            this.YesRadioButton.Tag = "ItemName(機密車)";
            this.YesRadioButton.Text = "はい";
            this.YesRadioButton.UseVisualStyleBackColor = true;
            // 
            // CarNameLabel
            // 
            this.CarNameLabel.Location = new System.Drawing.Point(3, 0);
            this.CarNameLabel.Name = "CarNameLabel";
            this.CarNameLabel.Size = new System.Drawing.Size(227, 30);
            this.CarNameLabel.TabIndex = 20;
            this.CarNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Aquamarine;
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.label20);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(1, 132);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(150, 30);
            this.panel6.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(92, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 31);
            this.label8.TabIndex = 22;
            this.label8.Text = "(※)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(0, 2);
            this.label19.Margin = new System.Windows.Forms.Padding(0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(82, 31);
            this.label19.TabIndex = 23;
            this.label19.Text = "使用目的";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label20.Location = new System.Drawing.Point(4, 37);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(144, 67);
            this.label20.TabIndex = 24;
            this.label20.Text = "<電話番号記入例>\r\n\r\n内線：8-22-○○○ or\r\n携帯：090-○○○-○○○";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Aquamarine;
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(1, 163);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(150, 30);
            this.panel5.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(92, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 31);
            this.label12.TabIndex = 26;
            this.label12.Text = "(※)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(0, 2);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 31);
            this.label16.TabIndex = 27;
            this.label16.Text = "利用時間";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.Location = new System.Drawing.Point(4, 37);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(144, 67);
            this.label18.TabIndex = 28;
            this.label18.Text = "<電話番号記入例>\r\n\r\n内線：8-22-○○○ or\r\n携帯：090-○○○-○○○";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.DriverAComboBox);
            this.panel1.Controls.Add(this.DriverATextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(152, 63);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(496, 34);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(297, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "TEL";
            // 
            // DriverAComboBox
            // 
            this.DriverAComboBox.FormattingEnabled = true;
            this.DriverAComboBox.Location = new System.Drawing.Point(3, 5);
            this.DriverAComboBox.Name = "DriverAComboBox";
            this.DriverAComboBox.Size = new System.Drawing.Size(288, 23);
            this.DriverAComboBox.TabIndex = 3;
            this.DriverAComboBox.Tag = "Required;Byte(50);ItemName(運転者A)";
            this.DriverAComboBox.DropDown += new System.EventHandler(this.DriverAComboBox_DropDown);
            // 
            // DriverATextBox
            // 
            this.DriverATextBox.Location = new System.Drawing.Point(335, 5);
            this.DriverATextBox.Name = "DriverATextBox";
            this.DriverATextBox.Size = new System.Drawing.Size(141, 22);
            this.DriverATextBox.TabIndex = 4;
            this.DriverATextBox.Tag = "Required;Byte(30);ItemName(運転者ATEL)";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Aquamarine;
            this.label5.Location = new System.Drawing.Point(1, 98);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 33);
            this.label5.TabIndex = 32;
            this.label5.Text = "運転者B";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.DriverBComboBox);
            this.panel2.Controls.Add(this.DriverBTextBox);
            this.panel2.Location = new System.Drawing.Point(152, 98);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(496, 33);
            this.panel2.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(297, 7);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 15);
            this.label17.TabIndex = 12;
            this.label17.Text = "TEL";
            // 
            // DriverBComboBox
            // 
            this.DriverBComboBox.FormattingEnabled = true;
            this.DriverBComboBox.Location = new System.Drawing.Point(2, 4);
            this.DriverBComboBox.Name = "DriverBComboBox";
            this.DriverBComboBox.Size = new System.Drawing.Size(289, 23);
            this.DriverBComboBox.TabIndex = 5;
            this.DriverBComboBox.Tag = "Byte(50);ItemName(運転者B)";
            this.DriverBComboBox.DropDown += new System.EventHandler(this.DriverBComboBox_DropDown);
            // 
            // DriverBTextBox
            // 
            this.DriverBTextBox.Location = new System.Drawing.Point(335, 5);
            this.DriverBTextBox.Name = "DriverBTextBox";
            this.DriverBTextBox.Size = new System.Drawing.Size(141, 22);
            this.DriverBTextBox.TabIndex = 6;
            this.DriverBTextBox.Tag = "Byte(30);ItemName(運転者BTEL)";
            // 
            // DatePanel
            // 
            this.DatePanel.Controls.Add(this.label14);
            this.DatePanel.Controls.Add(this.EndTimeComboBox);
            this.DatePanel.Controls.Add(this.EndDayDateTimePicker);
            this.DatePanel.Controls.Add(this.label13);
            this.DatePanel.Controls.Add(this.StartTimeComboBox);
            this.DatePanel.Controls.Add(this.StartDayDateTimePicker);
            this.DatePanel.Location = new System.Drawing.Point(152, 163);
            this.DatePanel.Margin = new System.Windows.Forms.Padding(0);
            this.DatePanel.Name = "DatePanel";
            this.DatePanel.Size = new System.Drawing.Size(487, 30);
            this.DatePanel.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(454, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 15);
            this.label14.TabIndex = 37;
            this.label14.Text = "時";
            // 
            // EndTimeComboBox
            // 
            this.EndTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EndTimeComboBox.FormattingEnabled = true;
            this.EndTimeComboBox.Location = new System.Drawing.Point(408, 3);
            this.EndTimeComboBox.Name = "EndTimeComboBox";
            this.EndTimeComboBox.Size = new System.Drawing.Size(41, 23);
            this.EndTimeComboBox.TabIndex = 11;
            this.EndTimeComboBox.Tag = "Required;ItemName(利用時間終了時間)";
            this.EndTimeComboBox.SelectionChangeCommitted += new System.EventHandler(this.EndTimeComboBox_SelectionChangeCommitted);
            // 
            // EndDayDateTimePicker
            // 
            this.EndDayDateTimePicker.CustomFormat = "yyyy/MM/dd (ddd)";
            this.EndDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDayDateTimePicker.Location = new System.Drawing.Point(253, 4);
            this.EndDayDateTimePicker.Name = "EndDayDateTimePicker";
            this.EndDayDateTimePicker.Size = new System.Drawing.Size(149, 22);
            this.EndDayDateTimePicker.TabIndex = 10;
            this.EndDayDateTimePicker.Tag = "Required;ItemName(利用時間終了)";
            this.EndDayDateTimePicker.Value = new System.DateTime(2019, 7, 25, 0, 0, 0, 0);
            this.EndDayDateTimePicker.ValueChanged += new System.EventHandler(this.EndDayDateTimePicker_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(205, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 15);
            this.label13.TabIndex = 40;
            this.label13.Text = "時 ～";
            // 
            // StartTimeComboBox
            // 
            this.StartTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StartTimeComboBox.FormattingEnabled = true;
            this.StartTimeComboBox.Location = new System.Drawing.Point(158, 3);
            this.StartTimeComboBox.Name = "StartTimeComboBox";
            this.StartTimeComboBox.Size = new System.Drawing.Size(41, 23);
            this.StartTimeComboBox.TabIndex = 9;
            this.StartTimeComboBox.Tag = "Required;ItemName(利用時間開始時間)";
            this.StartTimeComboBox.SelectionChangeCommitted += new System.EventHandler(this.StartTimeComboBox_SelectionChangeCommitted);
            // 
            // StartDayDateTimePicker
            // 
            this.StartDayDateTimePicker.CustomFormat = "yyyy/MM/dd (ddd)";
            this.StartDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDayDateTimePicker.Location = new System.Drawing.Point(3, 3);
            this.StartDayDateTimePicker.Name = "StartDayDateTimePicker";
            this.StartDayDateTimePicker.Size = new System.Drawing.Size(149, 22);
            this.StartDayDateTimePicker.TabIndex = 8;
            this.StartDayDateTimePicker.Tag = "Required;ItemName(利用時間開始)";
            this.StartDayDateTimePicker.Value = new System.DateTime(2019, 7, 25, 0, 0, 0, 0);
            this.StartDayDateTimePicker.ValueChanged += new System.EventHandler(this.StartDayDateTimePicker_ValueChanged);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Aquamarine;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(1, 194);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(150, 30);
            this.label15.TabIndex = 43;
            this.label15.Text = "編集者 / 編集日時";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ReservedPersonComboBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(152, 32);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(496, 30);
            this.panel3.TabIndex = 0;
            // 
            // ReservedPersonComboBox
            // 
            this.ReservedPersonComboBox.FormattingEnabled = true;
            this.ReservedPersonComboBox.Location = new System.Drawing.Point(3, 4);
            this.ReservedPersonComboBox.Name = "ReservedPersonComboBox";
            this.ReservedPersonComboBox.Size = new System.Drawing.Size(288, 23);
            this.ReservedPersonComboBox.TabIndex = 2;
            this.ReservedPersonComboBox.Tag = "Required;Byte(50);ItemName(予約者)";
            this.ReservedPersonComboBox.DropDown += new System.EventHandler(this.ReservedPersonComboBox_DropDown);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Aquamarine;
            this.panel9.Controls.Add(this.label3);
            this.panel9.Controls.Add(this.label11);
            this.panel9.Controls.Add(this.label7);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(1, 32);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(150, 30);
            this.panel9.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(92, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 31);
            this.label3.TabIndex = 48;
            this.label3.Text = "(※)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(0, 2);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 31);
            this.label11.TabIndex = 49;
            this.label11.Text = "予約者";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(4, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 67);
            this.label7.TabIndex = 50;
            this.label7.Text = "<電話番号記入例>\r\n\r\n内線：8-22-○○○ or\r\n携帯：090-○○○-○○○";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Aquamarine;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(1, 63);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(150, 34);
            this.panel4.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(92, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 31);
            this.label4.TabIndex = 52;
            this.label4.Text = "(※)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 2);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 31);
            this.label9.TabIndex = 53;
            this.label9.Text = "運転者A";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(4, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 67);
            this.label10.TabIndex = 54;
            this.label10.Text = "<電話番号記入例>\r\n\r\n内線：8-22-○○○ or\r\n携帯：090-○○○-○○○";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EditUserLabel
            // 
            this.EditUserLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditUserLabel.Location = new System.Drawing.Point(155, 194);
            this.EditUserLabel.Name = "EditUserLabel";
            this.EditUserLabel.Size = new System.Drawing.Size(490, 30);
            this.EditUserLabel.TabIndex = 1;
            this.EditUserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.PurposeOfUseTextBox);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(152, 132);
            this.panel10.Margin = new System.Windows.Forms.Padding(0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(496, 30);
            this.panel10.TabIndex = 3;
            // 
            // PurposeOfUseTextBox
            // 
            this.PurposeOfUseTextBox.Location = new System.Drawing.Point(2, 3);
            this.PurposeOfUseTextBox.Name = "PurposeOfUseTextBox";
            this.PurposeOfUseTextBox.Size = new System.Drawing.Size(370, 22);
            this.PurposeOfUseTextBox.TabIndex = 7;
            this.PurposeOfUseTextBox.Tag = "Required;Byte(50);ItemName(使用目的)";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Location = new System.Drawing.Point(131, 561);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(120, 30);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "削除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 561);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 1;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // TruckScheduleDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(672, 595);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EntryButton);
            this.Name = "TruckScheduleDetailForm";
            this.Text = "TruckScheduleDetailForm";
            this.Load += new System.EventHandler(this.TruckScheduleDetailForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.DeleteButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FreeTimeMultiRow)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionMultiRow)).EndInit();
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.DatePanel.ResumeLayout(false);
            this.DatePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private GrapeCity.Win.MultiRow.GcMultiRow FreeTimeMultiRow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button RowDeleteButton;
        private System.Windows.Forms.Button RowAddButton;
        private System.Windows.Forms.Button FrequentlyUsedDestinationsButton;
        private System.Windows.Forms.Label label6;
        private GrapeCity.Win.MultiRow.GcMultiRow SectionMultiRow;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox DriverATextBox;
        private System.Windows.Forms.Label CarNameLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox DriverBTextBox;
        private System.Windows.Forms.Panel DatePanel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox EndTimeComboBox;
        private UC.NullableDateTimePicker EndDayDateTimePicker;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox StartTimeComboBox;
        private UC.NullableDateTimePicker StartDayDateTimePicker;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.ComboBox DriverAComboBox;
        private System.Windows.Forms.ComboBox ReservedPersonComboBox;
        private System.Windows.Forms.TextBox PurposeOfUseTextBox;
        private System.Windows.Forms.ComboBox DriverBComboBox;
        private System.Windows.Forms.Label EditUserLabel;
        private FreeTimeMultiRowTemplate freeTimeMultiRowTemplate1;
        private SectionMultiRowTemplate sectionMultiRowTemplate1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.RadioButton NoRadioButton;
        private System.Windows.Forms.RadioButton YesRadioButton;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label17;
    }
}