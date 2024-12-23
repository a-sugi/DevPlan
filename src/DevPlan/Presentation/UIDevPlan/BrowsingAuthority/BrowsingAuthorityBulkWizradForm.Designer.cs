namespace DevPlan.Presentation.UIDevPlan.BrowsingAuthority
{
    partial class BrowsingAuthorityBulkWizardForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.WizardControl = new System.Windows.Forms.TabControl();
            this.WizardPage1 = new System.Windows.Forms.TabPage();
            this.WizardPanel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.WizardPage2 = new System.Windows.Forms.TabPage();
            this.WizardPanel2 = new System.Windows.Forms.Panel();
            this.GeneralCodeDataGridView = new System.Windows.Forms.DataGridView();
            this.CarGroupColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GeneralCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnderDevelopmentCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.WizardPage3 = new System.Windows.Forms.TabPage();
            this.WizardPanel3 = new System.Windows.Forms.Panel();
            this.PersonelDataGridView = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.WizardPage4 = new System.Windows.Forms.TabPage();
            this.WizardPanel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.EndDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.StartDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.WizardPage5 = new System.Windows.Forms.TabPage();
            this.WizardPanel5 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.SectionGroupNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PersonelIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WizardFormMainPanel.SuspendLayout();
            this.WizardControl.SuspendLayout();
            this.WizardPage1.SuspendLayout();
            this.WizardPanel1.SuspendLayout();
            this.WizardPage2.SuspendLayout();
            this.WizardPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GeneralCodeDataGridView)).BeginInit();
            this.WizardPage3.SuspendLayout();
            this.WizardPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PersonelDataGridView)).BeginInit();
            this.WizardPage4.SuspendLayout();
            this.WizardPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).BeginInit();
            this.WizardPage5.SuspendLayout();
            this.WizardPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(12, 305);
            // 
            // WizardFormMainPanel
            // 
            this.WizardFormMainPanel.Controls.Add(this.WizardControl);
            this.WizardFormMainPanel.Size = new System.Drawing.Size(444, 302);
            // 
            // BeforeButton
            // 
            this.BeforeButton.Location = new System.Drawing.Point(162, 305);
            this.BeforeButton.Click += new System.EventHandler(this.BeforeButton_Click);
            // 
            // ForwardButton
            // 
            this.ForwardButton.Location = new System.Drawing.Point(245, 305);
            this.ForwardButton.Click += new System.EventHandler(this.ForwardButton_Click);
            // 
            // EntryButton
            // 
            this.EntryButton.Location = new System.Drawing.Point(348, 305);
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // WizardControl
            // 
            this.WizardControl.Controls.Add(this.WizardPage1);
            this.WizardControl.Controls.Add(this.WizardPage2);
            this.WizardControl.Controls.Add(this.WizardPage3);
            this.WizardControl.Controls.Add(this.WizardPage4);
            this.WizardControl.Controls.Add(this.WizardPage5);
            this.WizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardControl.Location = new System.Drawing.Point(0, 0);
            this.WizardControl.Name = "WizardControl";
            this.WizardControl.SelectedIndex = 0;
            this.WizardControl.Size = new System.Drawing.Size(444, 302);
            this.WizardControl.TabIndex = 0;
            this.WizardControl.TabStop = false;
            // 
            // WizardPage1
            // 
            this.WizardPage1.Controls.Add(this.WizardPanel1);
            this.WizardPage1.Location = new System.Drawing.Point(4, 25);
            this.WizardPage1.Name = "WizardPage1";
            this.WizardPage1.Padding = new System.Windows.Forms.Padding(3);
            this.WizardPage1.Size = new System.Drawing.Size(436, 273);
            this.WizardPage1.TabIndex = 0;
            this.WizardPage1.Text = "Page1";
            this.WizardPage1.UseVisualStyleBackColor = true;
            // 
            // WizardPanel1
            // 
            this.WizardPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.WizardPanel1.Controls.Add(this.label4);
            this.WizardPanel1.Controls.Add(this.label3);
            this.WizardPanel1.Controls.Add(this.label2);
            this.WizardPanel1.Controls.Add(this.label1);
            this.WizardPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardPanel1.Location = new System.Drawing.Point(3, 3);
            this.WizardPanel1.Name = "WizardPanel1";
            this.WizardPanel1.Size = new System.Drawing.Size(430, 267);
            this.WizardPanel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(5, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "全て同一期間となりますので、ご注意ください。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(5, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "<<注意>>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(5, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(329, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "複数開発符号の閲覧許可期間を複数の課員に設定できます。";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(5, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "他部署情報閲覧権を一括設定します。";
            // 
            // WizardPage2
            // 
            this.WizardPage2.Controls.Add(this.WizardPanel2);
            this.WizardPage2.Location = new System.Drawing.Point(4, 25);
            this.WizardPage2.Name = "WizardPage2";
            this.WizardPage2.Padding = new System.Windows.Forms.Padding(3);
            this.WizardPage2.Size = new System.Drawing.Size(436, 273);
            this.WizardPage2.TabIndex = 1;
            this.WizardPage2.Text = "Page2";
            this.WizardPage2.UseVisualStyleBackColor = true;
            // 
            // WizardPanel2
            // 
            this.WizardPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.WizardPanel2.Controls.Add(this.GeneralCodeDataGridView);
            this.WizardPanel2.Controls.Add(this.UnderDevelopmentCheckBox);
            this.WizardPanel2.Controls.Add(this.label5);
            this.WizardPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardPanel2.Location = new System.Drawing.Point(3, 3);
            this.WizardPanel2.Name = "WizardPanel2";
            this.WizardPanel2.Size = new System.Drawing.Size(430, 267);
            this.WizardPanel2.TabIndex = 0;
            // 
            // GeneralCodeDataGridView
            // 
            this.GeneralCodeDataGridView.AllowUserToAddRows = false;
            this.GeneralCodeDataGridView.AllowUserToDeleteRows = false;
            this.GeneralCodeDataGridView.AllowUserToResizeColumns = false;
            this.GeneralCodeDataGridView.AllowUserToResizeRows = false;
            this.GeneralCodeDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GeneralCodeDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GeneralCodeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GeneralCodeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CarGroupColumn,
            this.GeneralCodeColumn});
            this.GeneralCodeDataGridView.Location = new System.Drawing.Point(22, 62);
            this.GeneralCodeDataGridView.Name = "GeneralCodeDataGridView";
            this.GeneralCodeDataGridView.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GeneralCodeDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GeneralCodeDataGridView.RowHeadersVisible = false;
            this.GeneralCodeDataGridView.RowTemplate.Height = 21;
            this.GeneralCodeDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GeneralCodeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GeneralCodeDataGridView.Size = new System.Drawing.Size(378, 185);
            this.GeneralCodeDataGridView.TabIndex = 5;
            // 
            // CarGroupColumn
            // 
            this.CarGroupColumn.DataPropertyName = "CAR_GROUP";
            this.CarGroupColumn.HeaderText = "車系";
            this.CarGroupColumn.Name = "CarGroupColumn";
            this.CarGroupColumn.ReadOnly = true;
            // 
            // GeneralCodeColumn
            // 
            this.GeneralCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GeneralCodeColumn.DataPropertyName = "GENERAL_CODE";
            this.GeneralCodeColumn.FillWeight = 120F;
            this.GeneralCodeColumn.HeaderText = "開発符号";
            this.GeneralCodeColumn.MaxInputLength = 20;
            this.GeneralCodeColumn.Name = "GeneralCodeColumn";
            this.GeneralCodeColumn.ReadOnly = true;
            this.GeneralCodeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // UnderDevelopmentCheckBox
            // 
            this.UnderDevelopmentCheckBox.AutoSize = true;
            this.UnderDevelopmentCheckBox.Checked = true;
            this.UnderDevelopmentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UnderDevelopmentCheckBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UnderDevelopmentCheckBox.Location = new System.Drawing.Point(22, 39);
            this.UnderDevelopmentCheckBox.Name = "UnderDevelopmentCheckBox";
            this.UnderDevelopmentCheckBox.Size = new System.Drawing.Size(250, 17);
            this.UnderDevelopmentCheckBox.TabIndex = 4;
            this.UnderDevelopmentCheckBox.Text = "開発中の開発符号のみを対象とする";
            this.UnderDevelopmentCheckBox.UseVisualStyleBackColor = true;
            this.UnderDevelopmentCheckBox.CheckedChanged += new System.EventHandler(this.UnderDevelopmentCheckBox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.label5.Location = new System.Drawing.Point(5, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(287, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "① 一括設定したい開発符号を全て選んでください。";
            // 
            // WizardPage3
            // 
            this.WizardPage3.Controls.Add(this.WizardPanel3);
            this.WizardPage3.Location = new System.Drawing.Point(4, 25);
            this.WizardPage3.Name = "WizardPage3";
            this.WizardPage3.Padding = new System.Windows.Forms.Padding(3);
            this.WizardPage3.Size = new System.Drawing.Size(436, 273);
            this.WizardPage3.TabIndex = 2;
            this.WizardPage3.Text = "Page3";
            this.WizardPage3.UseVisualStyleBackColor = true;
            // 
            // WizardPanel3
            // 
            this.WizardPanel3.BackColor = System.Drawing.SystemColors.Control;
            this.WizardPanel3.Controls.Add(this.PersonelDataGridView);
            this.WizardPanel3.Controls.Add(this.label6);
            this.WizardPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardPanel3.Location = new System.Drawing.Point(3, 3);
            this.WizardPanel3.Name = "WizardPanel3";
            this.WizardPanel3.Size = new System.Drawing.Size(430, 267);
            this.WizardPanel3.TabIndex = 0;
            // 
            // PersonelDataGridView
            // 
            this.PersonelDataGridView.AllowUserToAddRows = false;
            this.PersonelDataGridView.AllowUserToDeleteRows = false;
            this.PersonelDataGridView.AllowUserToResizeColumns = false;
            this.PersonelDataGridView.AllowUserToResizeRows = false;
            this.PersonelDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.PersonelDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PersonelDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SectionGroupNameColumn,
            this.NameColumn,
            this.PersonelIdColumn});
            this.PersonelDataGridView.Location = new System.Drawing.Point(22, 62);
            this.PersonelDataGridView.Name = "PersonelDataGridView";
            this.PersonelDataGridView.ReadOnly = true;
            this.PersonelDataGridView.RowHeadersVisible = false;
            this.PersonelDataGridView.RowTemplate.Height = 21;
            this.PersonelDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PersonelDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PersonelDataGridView.Size = new System.Drawing.Size(378, 185);
            this.PersonelDataGridView.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.label6.Location = new System.Drawing.Point(5, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(275, 12);
            this.label6.TabIndex = 1015;
            this.label6.Text = "② 一括設定したい課員名を全て選んでください。";
            // 
            // WizardPage4
            // 
            this.WizardPage4.Controls.Add(this.WizardPanel4);
            this.WizardPage4.Location = new System.Drawing.Point(4, 25);
            this.WizardPage4.Name = "WizardPage4";
            this.WizardPage4.Padding = new System.Windows.Forms.Padding(3);
            this.WizardPage4.Size = new System.Drawing.Size(436, 273);
            this.WizardPage4.TabIndex = 3;
            this.WizardPage4.Text = "Page4";
            this.WizardPage4.UseVisualStyleBackColor = true;
            // 
            // WizardPanel4
            // 
            this.WizardPanel4.BackColor = System.Drawing.SystemColors.Control;
            this.WizardPanel4.Controls.Add(this.label10);
            this.WizardPanel4.Controls.Add(this.label9);
            this.WizardPanel4.Controls.Add(this.EndDayDateTimePicker);
            this.WizardPanel4.Controls.Add(this.label20);
            this.WizardPanel4.Controls.Add(this.StartDayDateTimePicker);
            this.WizardPanel4.Controls.Add(this.label8);
            this.WizardPanel4.Controls.Add(this.label7);
            this.WizardPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardPanel4.Location = new System.Drawing.Point(3, 3);
            this.WizardPanel4.Name = "WizardPanel4";
            this.WizardPanel4.Size = new System.Drawing.Size(430, 267);
            this.WizardPanel4.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(24, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 12);
            this.label10.TabIndex = 1022;
            this.label10.Text = "　 指定してください。";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(24, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(335, 12);
            this.label9.TabIndex = 1021;
            this.label9.Text = "※ 権限を一括キャンセルしたい場合は、ここで過去の期間を";
            // 
            // EndDayDateTimePicker
            // 
            this.EndDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EndDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDayDateTimePicker.Location = new System.Drawing.Point(189, 57);
            this.EndDayDateTimePicker.Name = "EndDayDateTimePicker";
            this.EndDayDateTimePicker.Size = new System.Drawing.Size(129, 22);
            this.EndDayDateTimePicker.TabIndex = 8;
            this.EndDayDateTimePicker.Tag = "Required;ItemName(許可期間(終了))";
            this.EndDayDateTimePicker.Value = new System.DateTime(2019, 2, 18, 0, 0, 0, 0);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(161, 63);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(22, 15);
            this.label20.TabIndex = 1018;
            this.label20.Text = "～";
            // 
            // StartDayDateTimePicker
            // 
            this.StartDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.StartDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDayDateTimePicker.Location = new System.Drawing.Point(26, 57);
            this.StartDayDateTimePicker.Name = "StartDayDateTimePicker";
            this.StartDayDateTimePicker.Size = new System.Drawing.Size(129, 22);
            this.StartDayDateTimePicker.TabIndex = 7;
            this.StartDayDateTimePicker.Tag = "Required;ItemName(許可期間(開始))";
            this.StartDayDateTimePicker.Value = new System.DateTime(2019, 2, 18, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label8.Location = new System.Drawing.Point(23, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 1017;
            this.label8.Text = "閲覧許可期間";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.label7.Location = new System.Drawing.Point(5, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(167, 12);
            this.label7.TabIndex = 1016;
            this.label7.Text = "③ 期間を指定してください。";
            // 
            // WizardPage5
            // 
            this.WizardPage5.BackColor = System.Drawing.SystemColors.Control;
            this.WizardPage5.Controls.Add(this.WizardPanel5);
            this.WizardPage5.Location = new System.Drawing.Point(4, 25);
            this.WizardPage5.Name = "WizardPage5";
            this.WizardPage5.Padding = new System.Windows.Forms.Padding(3);
            this.WizardPage5.Size = new System.Drawing.Size(436, 273);
            this.WizardPage5.TabIndex = 4;
            this.WizardPage5.Text = "Page5";
            // 
            // WizardPanel5
            // 
            this.WizardPanel5.Controls.Add(this.label11);
            this.WizardPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardPanel5.Location = new System.Drawing.Point(3, 3);
            this.WizardPanel5.Name = "WizardPanel5";
            this.WizardPanel5.Size = new System.Drawing.Size(430, 267);
            this.WizardPanel5.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.label11.Location = new System.Drawing.Point(5, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(347, 12);
            this.label11.TabIndex = 1017;
            this.label11.Text = "　 設定内容を確認し、良ければ完了をクリックしてください。";
            // 
            // SectionGroupNameColumn
            // 
            this.SectionGroupNameColumn.DataPropertyName = "SECTION_GROUP_CODE";
            this.SectionGroupNameColumn.HeaderText = "担当";
            this.SectionGroupNameColumn.MinimumWidth = 160;
            this.SectionGroupNameColumn.Name = "SectionGroupNameColumn";
            this.SectionGroupNameColumn.ReadOnly = true;
            this.SectionGroupNameColumn.Width = 200;
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.DataPropertyName = "NAME";
            this.NameColumn.FillWeight = 120F;
            this.NameColumn.HeaderText = "氏名";
            this.NameColumn.MaxInputLength = 20;
            this.NameColumn.MinimumWidth = 180;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // PersonelIdColumn
            // 
            this.PersonelIdColumn.DataPropertyName = "PERSONEL_ID";
            this.PersonelIdColumn.HeaderText = "ユーザーID";
            this.PersonelIdColumn.Name = "PersonelIdColumn";
            this.PersonelIdColumn.ReadOnly = true;
            this.PersonelIdColumn.Visible = false;
            // 
            // BrowsingAuthorityBulkWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(444, 341);
            this.Name = "BrowsingAuthorityBulkWizardForm";
            this.Text = "タイトルが設定されていません - 開発計画表システム";
            this.Load += new System.EventHandler(this.BrowsingAuthorityBulkWizardForm_Load);
            this.WizardFormMainPanel.ResumeLayout(false);
            this.WizardControl.ResumeLayout(false);
            this.WizardPage1.ResumeLayout(false);
            this.WizardPanel1.ResumeLayout(false);
            this.WizardPanel1.PerformLayout();
            this.WizardPage2.ResumeLayout(false);
            this.WizardPanel2.ResumeLayout(false);
            this.WizardPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GeneralCodeDataGridView)).EndInit();
            this.WizardPage3.ResumeLayout(false);
            this.WizardPanel3.ResumeLayout(false);
            this.WizardPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PersonelDataGridView)).EndInit();
            this.WizardPage4.ResumeLayout(false);
            this.WizardPanel4.ResumeLayout(false);
            this.WizardPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).EndInit();
            this.WizardPage5.ResumeLayout(false);
            this.WizardPanel5.ResumeLayout(false);
            this.WizardPanel5.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TabControl WizardControl;
        private System.Windows.Forms.TabPage WizardPage1;
        private System.Windows.Forms.Panel WizardPanel1;
        private System.Windows.Forms.TabPage WizardPage2;
        private System.Windows.Forms.Panel WizardPanel2;
        private System.Windows.Forms.TabPage WizardPage3;
        private System.Windows.Forms.Panel WizardPanel3;
        private System.Windows.Forms.TabPage WizardPage4;
        private System.Windows.Forms.Panel WizardPanel4;
        private System.Windows.Forms.TabPage WizardPage5;
        private System.Windows.Forms.Panel WizardPanel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox UnderDevelopmentCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView GeneralCodeDataGridView;
        private System.Windows.Forms.DataGridView PersonelDataGridView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private UC.NullableDateTimePicker EndDayDateTimePicker;
        private System.Windows.Forms.Label label20;
        private UC.NullableDateTimePicker StartDayDateTimePicker;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarGroupColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeneralCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionGroupNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonelIdColumn;
    }
}