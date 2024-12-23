namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    partial class CarShareManagementForm
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
            this.DownloadButton = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ListConfigPictureBox = new System.Windows.Forms.PictureBox();
            this.ListSortPictureBox = new System.Windows.Forms.PictureBox();
            this.RowCountLabel = new System.Windows.Forms.Label();
            this.CarDataManagementMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.label14 = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchConditionButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchConditionTopTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.ListRadioButtonPanel = new System.Windows.Forms.Panel();
            this.TommorowRendListRadioButton = new System.Windows.Forms.RadioButton();
            this.SearchDateNullableDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ReturnListRadioButton = new System.Windows.Forms.RadioButton();
            this.RendListRadioButton = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.LocationPanel = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SkcRadioButton = new System.Windows.Forms.RadioButton();
            this.GunmaRadioButton = new System.Windows.Forms.RadioButton();
            this.panel11 = new System.Windows.Forms.Panel();
            this.ParkingNoTextBox = new System.Windows.Forms.TextBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.RefuelingOnCheckBox = new System.Windows.Forms.CheckBox();
            this.RefuelingOffCheckBox = new System.Windows.Forms.CheckBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.ReservationTextBox = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.ReturnOnCheckBox = new System.Windows.Forms.CheckBox();
            this.ReturnOffCheckBox = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.radioButton12 = new System.Windows.Forms.RadioButton();
            this.radioButton13 = new System.Windows.Forms.RadioButton();
            this.PreparationOnCheckBox = new System.Windows.Forms.CheckBox();
            this.PreparationOffCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.RendOnCheckBox = new System.Windows.Forms.CheckBox();
            this.RendOffCheckBox = new System.Windows.Forms.CheckBox();
            this.EntryButton = new System.Windows.Forms.Button();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.CsvSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OperatingRateButton = new System.Windows.Forms.Button();
            this.SearchConditionBottomTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ListConfigToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ListSortToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.WorkHistoryButton = new System.Windows.Forms.Button();
            this.ContentsPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListConfigPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListSortPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarDataManagementMultiRow)).BeginInit();
            this.SearchConditionTopTableLayoutPanel.SuspendLayout();
            this.ListRadioButtonPanel.SuspendLayout();
            this.LocationPanel.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SearchConditionBottomTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.TabIndex = 1015;
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.MainPanel);
            this.ContentsPanel.Controls.Add(this.MessageLabel);
            this.ContentsPanel.Controls.Add(this.ClearButton);
            this.ContentsPanel.Controls.Add(this.label2);
            this.ContentsPanel.Controls.Add(this.SearchButton);
            this.ContentsPanel.Controls.Add(this.SearchConditionButton);
            this.ContentsPanel.Controls.Add(this.SearchConditionTopTableLayoutPanel);
            this.ContentsPanel.Controls.Add(this.SearchConditionBottomTableLayoutPanel);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadButton.BackColor = System.Drawing.SystemColors.Control;
            this.DownloadButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DownloadButton.Location = new System.Drawing.Point(926, 616);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(120, 20);
            this.DownloadButton.TabIndex = 1014;
            this.DownloadButton.Text = "Excel出力";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.ListConfigPictureBox);
            this.MainPanel.Controls.Add(this.ListSortPictureBox);
            this.MainPanel.Controls.Add(this.RowCountLabel);
            this.MainPanel.Controls.Add(this.CarDataManagementMultiRow);
            this.MainPanel.Controls.Add(this.label14);
            this.MainPanel.Location = new System.Drawing.Point(0, 162);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1184, 421);
            this.MainPanel.TabIndex = 1010;
            // 
            // ListConfigPictureBox
            // 
            this.ListConfigPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ListConfigPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ListConfigPictureBox.Image = global::DevPlan.Presentation.Properties.Resources.CommonConfigButton;
            this.ListConfigPictureBox.Location = new System.Drawing.Point(1141, 7);
            this.ListConfigPictureBox.Name = "ListConfigPictureBox";
            this.ListConfigPictureBox.Size = new System.Drawing.Size(16, 16);
            this.ListConfigPictureBox.TabIndex = 1033;
            this.ListConfigPictureBox.TabStop = false;
            this.ListConfigToolTip.SetToolTip(this.ListConfigPictureBox, "列の表示や非表示、表示順を設定します。");
            this.ListConfigPictureBox.Click += new System.EventHandler(this.SearchConfigPictureBox_Click);
            // 
            // ListSortPictureBox
            // 
            this.ListSortPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ListSortPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ListSortPictureBox.Image = global::DevPlan.Presentation.Properties.Resources.CommonSortButton;
            this.ListSortPictureBox.Location = new System.Drawing.Point(1159, 7);
            this.ListSortPictureBox.Name = "ListSortPictureBox";
            this.ListSortPictureBox.Size = new System.Drawing.Size(16, 16);
            this.ListSortPictureBox.TabIndex = 1032;
            this.ListSortPictureBox.TabStop = false;
            this.ListSortToolTip.SetToolTip(this.ListSortPictureBox, "表示データを列ごとに並び替えます。");
            this.ListSortPictureBox.Click += new System.EventHandler(this.SearchSortPictureBox_Click);
            // 
            // RowCountLabel
            // 
            this.RowCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RowCountLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.5F);
            this.RowCountLabel.Location = new System.Drawing.Point(930, 5);
            this.RowCountLabel.Name = "RowCountLabel";
            this.RowCountLabel.Size = new System.Drawing.Size(205, 18);
            this.RowCountLabel.TabIndex = 112;
            this.RowCountLabel.Text = "表示件数： 0/0 件";
            this.RowCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CarDataManagementMultiRow
            // 
            this.CarDataManagementMultiRow.AllowAutoExtend = true;
            this.CarDataManagementMultiRow.AllowUserToAddRows = false;
            this.CarDataManagementMultiRow.AllowUserToDeleteRows = false;
            this.CarDataManagementMultiRow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CarDataManagementMultiRow.AutoFitContent = GrapeCity.Win.MultiRow.AutoFitContent.All;
            this.CarDataManagementMultiRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CarDataManagementMultiRow.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.CarDataManagementMultiRow.Location = new System.Drawing.Point(9, 26);
            this.CarDataManagementMultiRow.MultiSelect = false;
            this.CarDataManagementMultiRow.Name = "CarDataManagementMultiRow";
            this.CarDataManagementMultiRow.Size = new System.Drawing.Size(1166, 392);
            this.CarDataManagementMultiRow.TabIndex = 111;
            this.CarDataManagementMultiRow.VerticalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.CarDataManagementMultiRow.VerticalScrollMode = GrapeCity.Win.MultiRow.ScrollMode.Pixel;
            this.CarDataManagementMultiRow.CellDoubleClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.CarDataManagementMultiRow_CellDoubleClick);
            // 
            // label14
            // 
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(13, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(328, 20);
            this.label14.TabIndex = 0;
            this.label14.Text = "※ダブルクリックで対象車の予約画面を開きます。";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.Control;
            this.ClearButton.Location = new System.Drawing.Point(138, 128);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(120, 30);
            this.ClearButton.TabIndex = 101;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(12, 128);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 30);
            this.SearchButton.TabIndex = 100;
            this.SearchButton.Text = "検索/更新";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchConditionButton
            // 
            this.SearchConditionButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchConditionButton.Location = new System.Drawing.Point(86, 6);
            this.SearchConditionButton.Name = "SearchConditionButton";
            this.SearchConditionButton.Size = new System.Drawing.Size(20, 23);
            this.SearchConditionButton.TabIndex = 1;
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
            this.label2.TabIndex = 0;
            this.label2.Text = "検索条件";
            // 
            // SearchConditionTopTableLayoutPanel
            // 
            this.SearchConditionTopTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SearchConditionTopTableLayoutPanel.ColumnCount = 8;
            this.SearchConditionTopTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.13889F));
            this.SearchConditionTopTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.13889F));
            this.SearchConditionTopTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.13889F));
            this.SearchConditionTopTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.13889F));
            this.SearchConditionTopTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.13889F));
            this.SearchConditionTopTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.76852F));
            this.SearchConditionTopTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.76852F));
            this.SearchConditionTopTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.76852F));
            this.SearchConditionTopTableLayoutPanel.Controls.Add(this.label3, 0, 0);
            this.SearchConditionTopTableLayoutPanel.Controls.Add(this.ListRadioButtonPanel, 5, 0);
            this.SearchConditionTopTableLayoutPanel.Controls.Add(this.label10, 4, 0);
            this.SearchConditionTopTableLayoutPanel.Controls.Add(this.LocationPanel, 1, 0);
            this.SearchConditionTopTableLayoutPanel.Location = new System.Drawing.Point(10, 32);
            this.SearchConditionTopTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchConditionTopTableLayoutPanel.Name = "SearchConditionTopTableLayoutPanel";
            this.SearchConditionTopTableLayoutPanel.RowCount = 1;
            this.SearchConditionTopTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.SearchConditionTopTableLayoutPanel.Size = new System.Drawing.Size(1107, 32);
            this.SearchConditionTopTableLayoutPanel.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 30);
            this.label3.TabIndex = 32;
            this.label3.Text = "所在地";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListRadioButtonPanel
            // 
            this.SearchConditionTopTableLayoutPanel.SetColumnSpan(this.ListRadioButtonPanel, 3);
            this.ListRadioButtonPanel.Controls.Add(this.TommorowRendListRadioButton);
            this.ListRadioButtonPanel.Controls.Add(this.SearchDateNullableDateTimePicker);
            this.ListRadioButtonPanel.Controls.Add(this.ReturnListRadioButton);
            this.ListRadioButtonPanel.Controls.Add(this.RendListRadioButton);
            this.ListRadioButtonPanel.Location = new System.Drawing.Point(616, 1);
            this.ListRadioButtonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ListRadioButtonPanel.Name = "ListRadioButtonPanel";
            this.ListRadioButtonPanel.Size = new System.Drawing.Size(490, 30);
            this.ListRadioButtonPanel.TabIndex = 1;
            // 
            // TommorowRendListRadioButton
            // 
            this.TommorowRendListRadioButton.AutoSize = true;
            this.TommorowRendListRadioButton.Location = new System.Drawing.Point(339, 6);
            this.TommorowRendListRadioButton.Name = "TommorowRendListRadioButton";
            this.TommorowRendListRadioButton.Size = new System.Drawing.Size(159, 19);
            this.TommorowRendListRadioButton.TabIndex = 5;
            this.TommorowRendListRadioButton.Tag = "3";
            this.TommorowRendListRadioButton.Text = "検索日+1の貸出リスト";
            this.TommorowRendListRadioButton.UseVisualStyleBackColor = true;
            this.TommorowRendListRadioButton.CheckedChanged += new System.EventHandler(this.ListRadioButton_CheckedChanged);
            // 
            // SearchDateNullableDateTimePicker
            // 
            this.SearchDateNullableDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.SearchDateNullableDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SearchDateNullableDateTimePicker.Location = new System.Drawing.Point(5, 4);
            this.SearchDateNullableDateTimePicker.Name = "SearchDateNullableDateTimePicker";
            this.SearchDateNullableDateTimePicker.Size = new System.Drawing.Size(140, 22);
            this.SearchDateNullableDateTimePicker.TabIndex = 4;
            // 
            // ReturnListRadioButton
            // 
            this.ReturnListRadioButton.AutoSize = true;
            this.ReturnListRadioButton.Location = new System.Drawing.Point(250, 6);
            this.ReturnListRadioButton.Name = "ReturnListRadioButton";
            this.ReturnListRadioButton.Size = new System.Drawing.Size(86, 19);
            this.ReturnListRadioButton.TabIndex = 3;
            this.ReturnListRadioButton.Tag = "2";
            this.ReturnListRadioButton.Text = "返却リスト";
            this.ReturnListRadioButton.UseVisualStyleBackColor = true;
            this.ReturnListRadioButton.CheckedChanged += new System.EventHandler(this.ListRadioButton_CheckedChanged);
            // 
            // RendListRadioButton
            // 
            this.RendListRadioButton.AutoSize = true;
            this.RendListRadioButton.Checked = true;
            this.RendListRadioButton.Location = new System.Drawing.Point(154, 6);
            this.RendListRadioButton.Name = "RendListRadioButton";
            this.RendListRadioButton.Size = new System.Drawing.Size(86, 19);
            this.RendListRadioButton.TabIndex = 3;
            this.RendListRadioButton.TabStop = true;
            this.RendListRadioButton.Tag = "1";
            this.RendListRadioButton.Text = "貸出リスト";
            this.RendListRadioButton.UseVisualStyleBackColor = true;
            this.RendListRadioButton.CheckedChanged += new System.EventHandler(this.ListRadioButton_CheckedChanged);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Aquamarine;
            this.label10.Location = new System.Drawing.Point(493, 1);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 30);
            this.label10.TabIndex = 17;
            this.label10.Text = "検索日付";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LocationPanel
            // 
            this.SearchConditionTopTableLayoutPanel.SetColumnSpan(this.LocationPanel, 3);
            this.LocationPanel.Controls.Add(this.radioButton1);
            this.LocationPanel.Controls.Add(this.radioButton2);
            this.LocationPanel.Controls.Add(this.SkcRadioButton);
            this.LocationPanel.Controls.Add(this.GunmaRadioButton);
            this.LocationPanel.Location = new System.Drawing.Point(124, 1);
            this.LocationPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LocationPanel.Name = "LocationPanel";
            this.LocationPanel.Size = new System.Drawing.Size(357, 30);
            this.LocationPanel.TabIndex = 0;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(809, 6);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(146, 19);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.Tag = "7";
            this.radioButton1.Text = "全保有車両（参考）";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(739, 6);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(55, 19);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Tag = "6";
            this.radioButton2.Text = "美深";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // SkcRadioButton
            // 
            this.SkcRadioButton.AutoSize = true;
            this.SkcRadioButton.Location = new System.Drawing.Point(69, 6);
            this.SkcRadioButton.Name = "SkcRadioButton";
            this.SkcRadioButton.Size = new System.Drawing.Size(53, 19);
            this.SkcRadioButton.TabIndex = 2;
            this.SkcRadioButton.Tag = "";
            this.SkcRadioButton.Text = "SKC";
            this.SkcRadioButton.UseVisualStyleBackColor = true;
            // 
            // GunmaRadioButton
            // 
            this.GunmaRadioButton.AutoSize = true;
            this.GunmaRadioButton.Checked = true;
            this.GunmaRadioButton.Location = new System.Drawing.Point(9, 6);
            this.GunmaRadioButton.Name = "GunmaRadioButton";
            this.GunmaRadioButton.Size = new System.Drawing.Size(55, 19);
            this.GunmaRadioButton.TabIndex = 2;
            this.GunmaRadioButton.TabStop = true;
            this.GunmaRadioButton.Tag = "";
            this.GunmaRadioButton.Text = "群馬";
            this.GunmaRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel11
            // 
            this.SearchConditionBottomTableLayoutPanel.SetColumnSpan(this.panel11, 3);
            this.panel11.Controls.Add(this.ParkingNoTextBox);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel11.Location = new System.Drawing.Point(616, 32);
            this.panel11.Margin = new System.Windows.Forms.Padding(0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(476, 30);
            this.panel11.TabIndex = 5;
            // 
            // ParkingNoTextBox
            // 
            this.ParkingNoTextBox.Location = new System.Drawing.Point(5, 4);
            this.ParkingNoTextBox.MaxLength = 20;
            this.ParkingNoTextBox.Name = "ParkingNoTextBox";
            this.ParkingNoTextBox.Size = new System.Drawing.Size(328, 22);
            this.ParkingNoTextBox.TabIndex = 14;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.RefuelingOnCheckBox);
            this.panel10.Controls.Add(this.RefuelingOffCheckBox);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel10.Location = new System.Drawing.Point(922, 1);
            this.panel10.Margin = new System.Windows.Forms.Padding(0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(184, 30);
            this.panel10.TabIndex = 3;
            // 
            // RefuelingOnCheckBox
            // 
            this.RefuelingOnCheckBox.AutoSize = true;
            this.RefuelingOnCheckBox.Checked = true;
            this.RefuelingOnCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RefuelingOnCheckBox.Location = new System.Drawing.Point(59, 7);
            this.RefuelingOnCheckBox.Name = "RefuelingOnCheckBox";
            this.RefuelingOnCheckBox.Size = new System.Drawing.Size(41, 19);
            this.RefuelingOnCheckBox.TabIndex = 12;
            this.RefuelingOnCheckBox.Text = "済";
            this.RefuelingOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // RefuelingOffCheckBox
            // 
            this.RefuelingOffCheckBox.AutoSize = true;
            this.RefuelingOffCheckBox.Checked = true;
            this.RefuelingOffCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RefuelingOffCheckBox.Location = new System.Drawing.Point(11, 7);
            this.RefuelingOffCheckBox.Name = "RefuelingOffCheckBox";
            this.RefuelingOffCheckBox.Size = new System.Drawing.Size(41, 19);
            this.RefuelingOffCheckBox.TabIndex = 11;
            this.RefuelingOffCheckBox.Text = "未";
            this.RefuelingOffCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.SearchConditionBottomTableLayoutPanel.SetColumnSpan(this.panel9, 3);
            this.panel9.Controls.Add(this.ReservationTextBox);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(124, 32);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(365, 30);
            this.panel9.TabIndex = 4;
            // 
            // ReservationTextBox
            // 
            this.ReservationTextBox.Location = new System.Drawing.Point(6, 4);
            this.ReservationTextBox.MaxLength = 20;
            this.ReservationTextBox.Name = "ReservationTextBox";
            this.ReservationTextBox.Size = new System.Drawing.Size(326, 22);
            this.ReservationTextBox.TabIndex = 13;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.ReturnOnCheckBox);
            this.panel7.Controls.Add(this.ReturnOffCheckBox);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(616, 1);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(181, 30);
            this.panel7.TabIndex = 2;
            // 
            // ReturnOnCheckBox
            // 
            this.ReturnOnCheckBox.AutoSize = true;
            this.ReturnOnCheckBox.Checked = true;
            this.ReturnOnCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ReturnOnCheckBox.Location = new System.Drawing.Point(59, 7);
            this.ReturnOnCheckBox.Name = "ReturnOnCheckBox";
            this.ReturnOnCheckBox.Size = new System.Drawing.Size(41, 19);
            this.ReturnOnCheckBox.TabIndex = 10;
            this.ReturnOnCheckBox.Text = "済";
            this.ReturnOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReturnOffCheckBox
            // 
            this.ReturnOffCheckBox.AutoSize = true;
            this.ReturnOffCheckBox.Checked = true;
            this.ReturnOffCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ReturnOffCheckBox.Location = new System.Drawing.Point(11, 7);
            this.ReturnOffCheckBox.Name = "ReturnOffCheckBox";
            this.ReturnOffCheckBox.Size = new System.Drawing.Size(41, 19);
            this.ReturnOffCheckBox.TabIndex = 9;
            this.ReturnOffCheckBox.Text = "未";
            this.ReturnOffCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.radioButton12);
            this.panel4.Controls.Add(this.radioButton13);
            this.panel4.Controls.Add(this.PreparationOnCheckBox);
            this.panel4.Controls.Add(this.PreparationOffCheckBox);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(124, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(121, 30);
            this.panel4.TabIndex = 0;
            // 
            // radioButton12
            // 
            this.radioButton12.AutoSize = true;
            this.radioButton12.Location = new System.Drawing.Point(809, 6);
            this.radioButton12.Name = "radioButton12";
            this.radioButton12.Size = new System.Drawing.Size(146, 19);
            this.radioButton12.TabIndex = 3;
            this.radioButton12.Tag = "7";
            this.radioButton12.Text = "全保有車両（参考）";
            this.radioButton12.UseVisualStyleBackColor = true;
            // 
            // radioButton13
            // 
            this.radioButton13.AutoSize = true;
            this.radioButton13.Location = new System.Drawing.Point(739, 6);
            this.radioButton13.Name = "radioButton13";
            this.radioButton13.Size = new System.Drawing.Size(55, 19);
            this.radioButton13.TabIndex = 2;
            this.radioButton13.Tag = "6";
            this.radioButton13.Text = "美深";
            this.radioButton13.UseVisualStyleBackColor = true;
            // 
            // PreparationOnCheckBox
            // 
            this.PreparationOnCheckBox.AutoSize = true;
            this.PreparationOnCheckBox.Checked = true;
            this.PreparationOnCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PreparationOnCheckBox.Location = new System.Drawing.Point(59, 7);
            this.PreparationOnCheckBox.Name = "PreparationOnCheckBox";
            this.PreparationOnCheckBox.Size = new System.Drawing.Size(41, 19);
            this.PreparationOnCheckBox.TabIndex = 6;
            this.PreparationOnCheckBox.Text = "済";
            this.PreparationOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // PreparationOffCheckBox
            // 
            this.PreparationOffCheckBox.AutoSize = true;
            this.PreparationOffCheckBox.Checked = true;
            this.PreparationOffCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PreparationOffCheckBox.Location = new System.Drawing.Point(11, 7);
            this.PreparationOffCheckBox.Name = "PreparationOffCheckBox";
            this.PreparationOffCheckBox.Size = new System.Drawing.Size(41, 19);
            this.PreparationOffCheckBox.TabIndex = 5;
            this.PreparationOffCheckBox.Text = "未";
            this.PreparationOffCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Aquamarine;
            this.label6.Location = new System.Drawing.Point(799, 1);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 30);
            this.label6.TabIndex = 22;
            this.label6.Text = "給油状況";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Location = new System.Drawing.Point(493, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 30);
            this.label1.TabIndex = 21;
            this.label1.Text = "返却状況";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Aquamarine;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(247, 1);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(122, 30);
            this.label13.TabIndex = 20;
            this.label13.Text = "貸出状況";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Aquamarine;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(493, 32);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 30);
            this.label9.TabIndex = 16;
            this.label9.Text = "駐車場番号";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Aquamarine;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(1, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 30);
            this.label5.TabIndex = 5;
            this.label5.Text = "予約者";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Aquamarine;
            this.label4.Location = new System.Drawing.Point(1, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 30);
            this.label4.TabIndex = 1;
            this.label4.Text = "準備状況";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.RendOnCheckBox);
            this.panel5.Controls.Add(this.RendOffCheckBox);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(370, 1);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(121, 30);
            this.panel5.TabIndex = 1;
            // 
            // RendOnCheckBox
            // 
            this.RendOnCheckBox.AutoSize = true;
            this.RendOnCheckBox.Checked = true;
            this.RendOnCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RendOnCheckBox.Location = new System.Drawing.Point(59, 7);
            this.RendOnCheckBox.Name = "RendOnCheckBox";
            this.RendOnCheckBox.Size = new System.Drawing.Size(41, 19);
            this.RendOnCheckBox.TabIndex = 8;
            this.RendOnCheckBox.Text = "済";
            this.RendOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // RendOffCheckBox
            // 
            this.RendOffCheckBox.AutoSize = true;
            this.RendOffCheckBox.Checked = true;
            this.RendOffCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RendOffCheckBox.Location = new System.Drawing.Point(11, 7);
            this.RendOffCheckBox.Name = "RendOffCheckBox";
            this.RendOffCheckBox.Size = new System.Drawing.Size(41, 19);
            this.RendOffCheckBox.TabIndex = 7;
            this.RendOffCheckBox.Text = "未";
            this.RendOffCheckBox.UseVisualStyleBackColor = true;
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EntryButton.Location = new System.Drawing.Point(12, 616);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 20);
            this.EntryButton.TabIndex = 1011;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(134, 10);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(0, 15);
            this.MessageLabel.TabIndex = 31;
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CsvSaveFileDialog
            // 
            this.CsvSaveFileDialog.Filter = "CSV(*.csv)|*.*\"";
            // 
            // OperatingRateButton
            // 
            this.OperatingRateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OperatingRateButton.BackColor = System.Drawing.SystemColors.Control;
            this.OperatingRateButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OperatingRateButton.Location = new System.Drawing.Point(800, 616);
            this.OperatingRateButton.Name = "OperatingRateButton";
            this.OperatingRateButton.Size = new System.Drawing.Size(120, 20);
            this.OperatingRateButton.TabIndex = 1013;
            this.OperatingRateButton.Text = "稼働率算出";
            this.OperatingRateButton.UseVisualStyleBackColor = false;
            this.OperatingRateButton.Click += new System.EventHandler(this.OperatingRateButton_Click);
            // 
            // SearchConditionBottomTableLayoutPanel
            // 
            this.SearchConditionBottomTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SearchConditionBottomTableLayoutPanel.ColumnCount = 8;
            this.SearchConditionBottomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.14F));
            this.SearchConditionBottomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.14F));
            this.SearchConditionBottomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.14F));
            this.SearchConditionBottomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.14F));
            this.SearchConditionBottomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.14F));
            this.SearchConditionBottomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.58F));
            this.SearchConditionBottomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.14F));
            this.SearchConditionBottomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.58F));
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.label4, 0, 0);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.panel4, 1, 0);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.panel11, 5, 1);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.label9, 4, 1);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.label13, 2, 0);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.panel9, 1, 1);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.panel10, 7, 0);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.panel5, 3, 0);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.label5, 0, 1);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.panel7, 5, 0);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.label1, 4, 0);
            this.SearchConditionBottomTableLayoutPanel.Controls.Add(this.label6, 6, 0);
            this.SearchConditionBottomTableLayoutPanel.Location = new System.Drawing.Point(10, 63);
            this.SearchConditionBottomTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchConditionBottomTableLayoutPanel.Name = "SearchConditionBottomTableLayoutPanel";
            this.SearchConditionBottomTableLayoutPanel.RowCount = 2;
            this.SearchConditionBottomTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.SearchConditionBottomTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.SearchConditionBottomTableLayoutPanel.Size = new System.Drawing.Size(1107, 63);
            this.SearchConditionBottomTableLayoutPanel.TabIndex = 2;
            // 
            // WorkHistoryButton
            // 
            this.WorkHistoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.WorkHistoryButton.BackColor = System.Drawing.SystemColors.Control;
            this.WorkHistoryButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.WorkHistoryButton.Location = new System.Drawing.Point(674, 616);
            this.WorkHistoryButton.Name = "WorkHistoryButton";
            this.WorkHistoryButton.Size = new System.Drawing.Size(120, 20);
            this.WorkHistoryButton.TabIndex = 1016;
            this.WorkHistoryButton.Text = "使用履歴簡易入力";
            this.WorkHistoryButton.UseVisualStyleBackColor = false;
            this.WorkHistoryButton.Click += new System.EventHandler(this.WorkHistoryButton_Click);
            // 
            // CarShareManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.WorkHistoryButton);
            this.Controls.Add(this.EntryButton);
            this.Controls.Add(this.OperatingRateButton);
            this.Controls.Add(this.DownloadButton);
            this.Name = "CarShareManagementForm";
            this.Text = "CarShareManagementForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CarShareManagementForm_FormClosing);
            this.Load += new System.EventHandler(this.CarShareForm_Load);
            this.Shown += new System.EventHandler(this.CarShareManagementForm_Shown);
            this.Controls.SetChildIndex(this.DownloadButton, 0);
            this.Controls.SetChildIndex(this.OperatingRateButton, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.WorkHistoryButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ListConfigPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListSortPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarDataManagementMultiRow)).EndInit();
            this.SearchConditionTopTableLayoutPanel.ResumeLayout(false);
            this.ListRadioButtonPanel.ResumeLayout(false);
            this.ListRadioButtonPanel.PerformLayout();
            this.LocationPanel.ResumeLayout(false);
            this.LocationPanel.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.SearchConditionBottomTableLayoutPanel.ResumeLayout(false);
            this.SearchConditionBottomTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.TableLayoutPanel SearchConditionTopTableLayoutPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button SearchConditionButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.CheckBox PreparationOnCheckBox;
        private System.Windows.Forms.CheckBox PreparationOffCheckBox;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel LocationPanel;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton SkcRadioButton;
        private System.Windows.Forms.RadioButton GunmaRadioButton;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.CheckBox RefuelingOnCheckBox;
        private System.Windows.Forms.CheckBox RefuelingOffCheckBox;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.CheckBox ReturnOnCheckBox;
        private System.Windows.Forms.CheckBox ReturnOffCheckBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton radioButton12;
        private System.Windows.Forms.RadioButton radioButton13;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox RendOnCheckBox;
        private System.Windows.Forms.CheckBox RendOffCheckBox;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Panel ListRadioButtonPanel;
        private System.Windows.Forms.RadioButton ReturnListRadioButton;
        private System.Windows.Forms.RadioButton RendListRadioButton;
        private System.Windows.Forms.TextBox ParkingNoTextBox;
        private System.Windows.Forms.TextBox ReservationTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker SearchDateNullableDateTimePicker;
        private System.Windows.Forms.SaveFileDialog CsvSaveFileDialog;
        private System.Windows.Forms.Button OperatingRateButton;
        private GrapeCity.Win.MultiRow.GcMultiRow CarDataManagementMultiRow;
        private System.Windows.Forms.Label RowCountLabel;
        private System.Windows.Forms.TableLayoutPanel SearchConditionBottomTableLayoutPanel;
        private System.Windows.Forms.PictureBox ListConfigPictureBox;
        private System.Windows.Forms.PictureBox ListSortPictureBox;
        private System.Windows.Forms.ToolTip ListConfigToolTip;
        private System.Windows.Forms.ToolTip ListSortToolTip;
        private System.Windows.Forms.RadioButton TommorowRendListRadioButton;
        private System.Windows.Forms.Button WorkHistoryButton;
    }
}