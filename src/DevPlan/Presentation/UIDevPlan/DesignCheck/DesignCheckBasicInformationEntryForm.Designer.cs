namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    partial class DesignCheckBasicInformationEntryForm
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
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HoldingTimesTextBox = new System.Windows.Forms.TextBox();
            this.EventDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DesignCheckNameTextBox = new System.Windows.Forms.TextBox();
            this.EntryButton = new System.Windows.Forms.Button();
            this.RegisteredCarAddButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AllSelectCheckBox = new System.Windows.Forms.CheckBox();
            this.ListDataGridView = new System.Windows.Forms.DataGridView();
            this.SelectedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ManagementNoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GeneralCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrototypeTimingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventDateIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestCarIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SystemRegisteredCarAddButton = new System.Windows.Forms.Button();
            this.IncrementCheckBox = new System.Windows.Forms.CheckBox();
            this.ImprotButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EventDateTimePicker)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(458, 456);
            this.CloseButton.TabIndex = 100;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListFormMainPanel.Controls.Add(this.ImprotButton);
            this.ListFormMainPanel.Controls.Add(this.IncrementCheckBox);
            this.ListFormMainPanel.Controls.Add(this.SystemRegisteredCarAddButton);
            this.ListFormMainPanel.Controls.Add(this.AllSelectCheckBox);
            this.ListFormMainPanel.Controls.Add(this.ListDataGridView);
            this.ListFormMainPanel.Controls.Add(this.DeleteButton);
            this.ListFormMainPanel.Controls.Add(this.AddButton);
            this.ListFormMainPanel.Controls.Add(this.RegisteredCarAddButton);
            this.ListFormMainPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ListFormMainPanel.Size = new System.Drawing.Size(573, 445);
            this.ListFormMainPanel.TabIndex = 0;
            this.ListFormMainPanel.Controls.SetChildIndex(this.DetailTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RegisteredCarAddButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.AddButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DeleteButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.AllSelectCheckBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SystemRegisteredCarAddButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.IncrementCheckBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ImprotButton, 0);
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DetailTableLayoutPanel.ColumnCount = 4;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.Controls.Add(this.panel3, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.panel2, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.HoldingTimesTextBox, 3, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.EventDateTimePicker, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.label1, 2, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.panel1, 1, 1);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(6, 39);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 2;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(506, 63);
            this.DetailTableLayoutPanel.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Aquamarine;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 32);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(130, 30);
            this.panel3.TabIndex = 1029;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(95, 0);
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
            this.label5.Size = new System.Drawing.Size(95, 31);
            this.label5.TabIndex = 1013;
            this.label5.Text = "設計チェック名";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Aquamarine;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(130, 30);
            this.panel2.TabIndex = 1028;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(55, -1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 31);
            this.label2.TabIndex = 1014;
            this.label2.Text = "(※)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, -1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 31);
            this.label3.TabIndex = 1013;
            this.label3.Text = "開催日";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HoldingTimesTextBox
            // 
            this.HoldingTimesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HoldingTimesTextBox.Location = new System.Drawing.Point(377, 4);
            this.HoldingTimesTextBox.MaxLength = 5;
            this.HoldingTimesTextBox.Name = "HoldingTimesTextBox";
            this.HoldingTimesTextBox.Size = new System.Drawing.Size(126, 22);
            this.HoldingTimesTextBox.TabIndex = 30;
            this.HoldingTimesTextBox.Tag = "Regex(^[0-9]{1,5}$);ItemName(開催回)";
            this.HoldingTimesTextBox.TextChanged += new System.EventHandler(this.Controller_Changed);
            // 
            // EventDateTimePicker
            // 
            this.EventDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EventDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EventDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EventDateTimePicker.Location = new System.Drawing.Point(135, 4);
            this.EventDateTimePicker.Name = "EventDateTimePicker";
            this.EventDateTimePicker.Size = new System.Drawing.Size(114, 22);
            this.EventDateTimePicker.TabIndex = 20;
            this.EventDateTimePicker.Tag = "Required;ItemName(開催日)";
            this.EventDateTimePicker.Value = new System.DateTime(2021, 7, 15, 0, 0, 0, 0);
            this.EventDateTimePicker.ValueChanged += new System.EventHandler(this.Controller_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(253, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 30);
            this.label1.TabIndex = 19;
            this.label1.Text = "開催回";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.DetailTableLayoutPanel.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.DesignCheckNameTextBox);
            this.panel1.Location = new System.Drawing.Point(132, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 30);
            this.panel1.TabIndex = 35;
            // 
            // DesignCheckNameTextBox
            // 
            this.DesignCheckNameTextBox.Location = new System.Drawing.Point(3, 5);
            this.DesignCheckNameTextBox.MaxLength = 50;
            this.DesignCheckNameTextBox.Name = "DesignCheckNameTextBox";
            this.DesignCheckNameTextBox.Size = new System.Drawing.Size(368, 22);
            this.DesignCheckNameTextBox.TabIndex = 40;
            this.DesignCheckNameTextBox.Tag = "Required;ItemName(設計チェック名)";
            this.DesignCheckNameTextBox.TextChanged += new System.EventHandler(this.Controller_Changed);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 456);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 90;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // RegisteredCarAddButton
            // 
            this.RegisteredCarAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RegisteredCarAddButton.BackColor = System.Drawing.SystemColors.Control;
            this.RegisteredCarAddButton.Location = new System.Drawing.Point(132, 143);
            this.RegisteredCarAddButton.Name = "RegisteredCarAddButton";
            this.RegisteredCarAddButton.Size = new System.Drawing.Size(120, 30);
            this.RegisteredCarAddButton.TabIndex = 51;
            this.RegisteredCarAddButton.Text = "登録済車両";
            this.RegisteredCarAddButton.UseVisualStyleBackColor = false;
            this.RegisteredCarAddButton.Click += new System.EventHandler(this.RegisteredCarAddButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddButton.BackColor = System.Drawing.SystemColors.Control;
            this.AddButton.Location = new System.Drawing.Point(380, 143);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(90, 30);
            this.AddButton.TabIndex = 60;
            this.AddButton.Text = "行追加";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Location = new System.Drawing.Point(476, 143);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(90, 30);
            this.DeleteButton.TabIndex = 70;
            this.DeleteButton.Text = "行削除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AllSelectCheckBox
            // 
            this.AllSelectCheckBox.AutoSize = true;
            this.AllSelectCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.AllSelectCheckBox.Location = new System.Drawing.Point(15, 185);
            this.AllSelectCheckBox.Name = "AllSelectCheckBox";
            this.AllSelectCheckBox.Size = new System.Drawing.Size(15, 14);
            this.AllSelectCheckBox.TabIndex = 7;
            this.AllSelectCheckBox.TabStop = false;
            this.AllSelectCheckBox.UseVisualStyleBackColor = false;
            this.AllSelectCheckBox.CheckedChanged += new System.EventHandler(this.AllSelectCheckBox_CheckedChanged);
            // 
            // ListDataGridView
            // 
            this.ListDataGridView.AllowUserToAddRows = false;
            this.ListDataGridView.AllowUserToDeleteRows = false;
            this.ListDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ListDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedColumn,
            this.ManagementNoColumn,
            this.GeneralCodeColumn,
            this.PrototypeTimingColumn,
            this.VehicleColumn,
            this.IdColumn,
            this.EventDateIdColumn,
            this.TestCarIdColumn});
            this.ListDataGridView.Location = new System.Drawing.Point(6, 179);
            this.ListDataGridView.MultiSelect = false;
            this.ListDataGridView.Name = "ListDataGridView";
            this.ListDataGridView.RowHeadersVisible = false;
            this.ListDataGridView.RowTemplate.Height = 21;
            this.ListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ListDataGridView.Size = new System.Drawing.Size(560, 261);
            this.ListDataGridView.TabIndex = 80;
            this.ListDataGridView.Tag = "ItemName(試験車一覧)";
            // 
            // SelectedColumn
            // 
            this.SelectedColumn.DataPropertyName = "Selected";
            this.SelectedColumn.FillWeight = 30F;
            this.SelectedColumn.Frozen = true;
            this.SelectedColumn.HeaderText = "";
            this.SelectedColumn.MinimumWidth = 30;
            this.SelectedColumn.Name = "SelectedColumn";
            this.SelectedColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SelectedColumn.Width = 30;
            // 
            // ManagementNoColumn
            // 
            this.ManagementNoColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ManagementNoColumn.DataPropertyName = "管理票NO";
            this.ManagementNoColumn.FillWeight = 80F;
            this.ManagementNoColumn.HeaderText = "管理票No";
            this.ManagementNoColumn.MinimumWidth = 40;
            this.ManagementNoColumn.Name = "ManagementNoColumn";
            this.ManagementNoColumn.ReadOnly = true;
            // 
            // GeneralCodeColumn
            // 
            this.GeneralCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GeneralCodeColumn.DataPropertyName = "開発符号";
            this.GeneralCodeColumn.HeaderText = "開発符号";
            this.GeneralCodeColumn.MaxInputLength = 20;
            this.GeneralCodeColumn.MinimumWidth = 50;
            this.GeneralCodeColumn.Name = "GeneralCodeColumn";
            // 
            // PrototypeTimingColumn
            // 
            this.PrototypeTimingColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PrototypeTimingColumn.DataPropertyName = "試作時期";
            this.PrototypeTimingColumn.HeaderText = "試作時期";
            this.PrototypeTimingColumn.MaxInputLength = 20;
            this.PrototypeTimingColumn.MinimumWidth = 50;
            this.PrototypeTimingColumn.Name = "PrototypeTimingColumn";
            // 
            // VehicleColumn
            // 
            this.VehicleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VehicleColumn.DataPropertyName = "号車";
            this.VehicleColumn.HeaderText = "号車";
            this.VehicleColumn.MaxInputLength = 50;
            this.VehicleColumn.MinimumWidth = 100;
            this.VehicleColumn.Name = "VehicleColumn";
            // 
            // IdColumn
            // 
            this.IdColumn.DataPropertyName = "ID";
            this.IdColumn.HeaderText = "ID";
            this.IdColumn.Name = "IdColumn";
            this.IdColumn.ReadOnly = true;
            this.IdColumn.Visible = false;
            this.IdColumn.Width = 46;
            // 
            // EventDateIdColumn
            // 
            this.EventDateIdColumn.DataPropertyName = "開催日_ID";
            this.EventDateIdColumn.HeaderText = "開催日_ID";
            this.EventDateIdColumn.Name = "EventDateIdColumn";
            this.EventDateIdColumn.ReadOnly = true;
            this.EventDateIdColumn.Visible = false;
            this.EventDateIdColumn.Width = 96;
            // 
            // TestCarIdColumn
            // 
            this.TestCarIdColumn.DataPropertyName = "試験車_ID";
            this.TestCarIdColumn.HeaderText = "試験車_ID";
            this.TestCarIdColumn.Name = "TestCarIdColumn";
            this.TestCarIdColumn.ReadOnly = true;
            this.TestCarIdColumn.Visible = false;
            this.TestCarIdColumn.Width = 96;
            // 
            // SystemRegisteredCarAddButton
            // 
            this.SystemRegisteredCarAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SystemRegisteredCarAddButton.BackColor = System.Drawing.SystemColors.Control;
            this.SystemRegisteredCarAddButton.Location = new System.Drawing.Point(7, 143);
            this.SystemRegisteredCarAddButton.Name = "SystemRegisteredCarAddButton";
            this.SystemRegisteredCarAddButton.Size = new System.Drawing.Size(120, 30);
            this.SystemRegisteredCarAddButton.TabIndex = 50;
            this.SystemRegisteredCarAddButton.Text = "Sys登録済車両";
            this.SystemRegisteredCarAddButton.UseVisualStyleBackColor = false;
            this.SystemRegisteredCarAddButton.Click += new System.EventHandler(this.SystemRegisteredCarAddButton_Click);
            // 
            // IncrementCheckBox
            // 
            this.IncrementCheckBox.AutoSize = true;
            this.IncrementCheckBox.Checked = true;
            this.IncrementCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IncrementCheckBox.Location = new System.Drawing.Point(293, 150);
            this.IncrementCheckBox.Name = "IncrementCheckBox";
            this.IncrementCheckBox.Size = new System.Drawing.Size(81, 19);
            this.IncrementCheckBox.TabIndex = 1011;
            this.IncrementCheckBox.Text = "ｲﾝｸﾘﾒﾝﾄ";
            this.IncrementCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImprotButton
            // 
            this.ImprotButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ImprotButton.BackColor = System.Drawing.SystemColors.Control;
            this.ImprotButton.Location = new System.Drawing.Point(380, 108);
            this.ImprotButton.Name = "ImprotButton";
            this.ImprotButton.Size = new System.Drawing.Size(186, 30);
            this.ImprotButton.TabIndex = 1012;
            this.ImprotButton.Text = "試作車不具合情報取込み";
            this.ImprotButton.UseVisualStyleBackColor = false;
            this.ImprotButton.Click += new System.EventHandler(this.ImprotButton_Click);
            // 
            // DesignCheckBasicInformationEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(584, 496);
            this.Controls.Add(this.EntryButton);
            this.Name = "DesignCheckBasicInformationEntryForm";
            this.Text = "DesignCheckBasicInformationEntryForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesignCheckBasicInformationEntryForm_FormClosing);
            this.Load += new System.EventHandler(this.DesignCheckBasicInformationEntryForm_Load);
            this.Shown += new System.EventHandler(this.DesignCheckBasicInformationEntryForm_Shown);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EventDateTimePicker)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Label label1;
        private UC.NullableDateTimePicker EventDateTimePicker;
        private System.Windows.Forms.TextBox HoldingTimesTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox DesignCheckNameTextBox;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button RegisteredCarAddButton;
        private System.Windows.Forms.CheckBox AllSelectCheckBox;
        private System.Windows.Forms.DataGridView ListDataGridView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SystemRegisteredCarAddButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ManagementNoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeneralCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrototypeTimingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventDateIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestCarIdColumn;
        private System.Windows.Forms.CheckBox IncrementCheckBox;
        private System.Windows.Forms.Button ImprotButton;
    }
}