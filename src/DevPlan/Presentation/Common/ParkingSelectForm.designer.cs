namespace DevPlan.Presentation.Common
{
    partial class ParkingSelectForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AreaButton = new System.Windows.Forms.Button();
            this.SectionButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.LocationComboBox = new System.Windows.Forms.ComboBox();
            this.AreaComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RadioButtonPanel = new System.Windows.Forms.Panel();
            this.AreaRadioButton = new System.Windows.Forms.RadioButton();
            this.UseRadioButton = new System.Windows.Forms.RadioButton();
            this.FreeRadioButton = new System.Windows.Forms.RadioButton();
            this.AllRadioButton = new System.Windows.Forms.RadioButton();
            this.SectionDataGridView = new System.Windows.Forms.DataGridView();
            this.エリア = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.駐車場NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.状態 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LOCATION_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AREA_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SECTION_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHANGE_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.データID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.管理票NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.ModeButton = new System.Windows.Forms.Button();
            this.LocationLinkLabel = new System.Windows.Forms.LinkLabel();
            this.AreaLinkLabel = new System.Windows.Forms.LinkLabel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AreaCountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.RadioButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(1058, 627);
            this.CloseButton.TabIndex = 1013;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.label2);
            this.ListFormMainPanel.Controls.Add(this.AreaCountLabel);
            this.ListFormMainPanel.Controls.Add(this.AreaLinkLabel);
            this.ListFormMainPanel.Controls.Add(this.LocationLinkLabel);
            this.ListFormMainPanel.Controls.Add(this.ModeButton);
            this.ListFormMainPanel.Controls.Add(this.SectionDataGridView);
            this.ListFormMainPanel.Controls.Add(this.RadioButtonPanel);
            this.ListFormMainPanel.Controls.Add(this.AreaComboBox);
            this.ListFormMainPanel.Controls.Add(this.label1);
            this.ListFormMainPanel.Controls.Add(this.LocationComboBox);
            this.ListFormMainPanel.Controls.Add(this.label13);
            this.ListFormMainPanel.Size = new System.Drawing.Size(1173, 618);
            this.ListFormMainPanel.TabIndex = 0;
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label13, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.LocationComboBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.AreaComboBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RadioButtonPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SectionDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ModeButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.LocationLinkLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.AreaLinkLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.AreaCountLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label2, 0);
            // 
            // AreaButton
            // 
            this.AreaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AreaButton.BackColor = System.Drawing.SystemColors.Control;
            this.AreaButton.Location = new System.Drawing.Point(5, 627);
            this.AreaButton.Name = "AreaButton";
            this.AreaButton.Size = new System.Drawing.Size(150, 30);
            this.AreaButton.TabIndex = 1010;
            this.AreaButton.Text = "エリアを選択";
            this.AreaButton.UseVisualStyleBackColor = false;
            this.AreaButton.Click += new System.EventHandler(this.AreaButton_Click);
            // 
            // SectionButton
            // 
            this.SectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SectionButton.BackColor = System.Drawing.SystemColors.Control;
            this.SectionButton.Location = new System.Drawing.Point(161, 627);
            this.SectionButton.Name = "SectionButton";
            this.SectionButton.Size = new System.Drawing.Size(150, 30);
            this.SectionButton.TabIndex = 1011;
            this.SectionButton.Text = "駐車場NOを選択";
            this.SectionButton.UseVisualStyleBackColor = false;
            this.SectionButton.Click += new System.EventHandler(this.SectionButton_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 44);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 15);
            this.label13.TabIndex = 1011;
            this.label13.Text = "所在地";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LocationComboBox
            // 
            this.LocationComboBox.DisplayMember = "NAME";
            this.LocationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LocationComboBox.DropDownWidth = 306;
            this.LocationComboBox.FormattingEnabled = true;
            this.LocationComboBox.Location = new System.Drawing.Point(71, 41);
            this.LocationComboBox.Name = "LocationComboBox";
            this.LocationComboBox.Size = new System.Drawing.Size(300, 23);
            this.LocationComboBox.TabIndex = 1;
            this.LocationComboBox.Tag = "";
            this.LocationComboBox.ValueMember = "LOCATION_NO";
            this.LocationComboBox.SelectedIndexChanged += new System.EventHandler(this.LocationComboBox_SelectedIndexChanged);
            // 
            // AreaComboBox
            // 
            this.AreaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AreaComboBox.DropDownWidth = 300;
            this.AreaComboBox.FormattingEnabled = true;
            this.AreaComboBox.Location = new System.Drawing.Point(430, 41);
            this.AreaComboBox.Name = "AreaComboBox";
            this.AreaComboBox.Size = new System.Drawing.Size(300, 23);
            this.AreaComboBox.TabIndex = 2;
            this.AreaComboBox.SelectedIndexChanged += new System.EventHandler(this.AreaComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 1013;
            this.label1.Text = "エリア";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RadioButtonPanel
            // 
            this.RadioButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioButtonPanel.Controls.Add(this.AreaRadioButton);
            this.RadioButtonPanel.Controls.Add(this.UseRadioButton);
            this.RadioButtonPanel.Controls.Add(this.FreeRadioButton);
            this.RadioButtonPanel.Controls.Add(this.AllRadioButton);
            this.RadioButtonPanel.Location = new System.Drawing.Point(845, 68);
            this.RadioButtonPanel.Name = "RadioButtonPanel";
            this.RadioButtonPanel.Size = new System.Drawing.Size(197, 104);
            this.RadioButtonPanel.TabIndex = 3;
            // 
            // AreaRadioButton
            // 
            this.AreaRadioButton.AutoSize = true;
            this.AreaRadioButton.Location = new System.Drawing.Point(8, 78);
            this.AreaRadioButton.Name = "AreaRadioButton";
            this.AreaRadioButton.Size = new System.Drawing.Size(81, 19);
            this.AreaRadioButton.TabIndex = 4;
            this.AreaRadioButton.Text = "エリアのみ";
            this.AreaRadioButton.UseVisualStyleBackColor = true;
            this.AreaRadioButton.CheckedChanged += new System.EventHandler(this.AreaRadioButton_CheckedChanged);
            // 
            // UseRadioButton
            // 
            this.UseRadioButton.AutoSize = true;
            this.UseRadioButton.Location = new System.Drawing.Point(8, 53);
            this.UseRadioButton.Name = "UseRadioButton";
            this.UseRadioButton.Size = new System.Drawing.Size(70, 19);
            this.UseRadioButton.TabIndex = 3;
            this.UseRadioButton.Text = "使用中";
            this.UseRadioButton.UseVisualStyleBackColor = true;
            this.UseRadioButton.CheckedChanged += new System.EventHandler(this.UseRadioButton_CheckedChanged);
            // 
            // FreeRadioButton
            // 
            this.FreeRadioButton.AutoSize = true;
            this.FreeRadioButton.Location = new System.Drawing.Point(8, 28);
            this.FreeRadioButton.Name = "FreeRadioButton";
            this.FreeRadioButton.Size = new System.Drawing.Size(51, 19);
            this.FreeRadioButton.TabIndex = 2;
            this.FreeRadioButton.Text = "空き";
            this.FreeRadioButton.UseVisualStyleBackColor = true;
            this.FreeRadioButton.CheckedChanged += new System.EventHandler(this.FreeRadioButton_CheckedChanged);
            // 
            // AllRadioButton
            // 
            this.AllRadioButton.AutoSize = true;
            this.AllRadioButton.Checked = true;
            this.AllRadioButton.Location = new System.Drawing.Point(8, 3);
            this.AllRadioButton.Name = "AllRadioButton";
            this.AllRadioButton.Size = new System.Drawing.Size(52, 19);
            this.AllRadioButton.TabIndex = 1;
            this.AllRadioButton.TabStop = true;
            this.AllRadioButton.Text = "全て";
            this.AllRadioButton.UseVisualStyleBackColor = true;
            this.AllRadioButton.CheckedChanged += new System.EventHandler(this.AllRadioButton_CheckedChanged);
            // 
            // SectionDataGridView
            // 
            this.SectionDataGridView.AllowUserToAddRows = false;
            this.SectionDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SectionDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SectionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SectionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.エリア,
            this.駐車場NO,
            this.状態,
            this.LOCATION_NO,
            this.AREA_NO,
            this.SECTION_NO,
            this.STATUS,
            this.CHANGE_STATUS,
            this.データID,
            this.管理票NO});
            this.SectionDataGridView.EnableHeadersVisualStyles = false;
            this.SectionDataGridView.Location = new System.Drawing.Point(845, 178);
            this.SectionDataGridView.MultiSelect = false;
            this.SectionDataGridView.Name = "SectionDataGridView";
            this.SectionDataGridView.RowHeadersVisible = false;
            this.SectionDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.SectionDataGridView.RowTemplate.Height = 21;
            this.SectionDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SectionDataGridView.Size = new System.Drawing.Size(320, 439);
            this.SectionDataGridView.TabIndex = 5;
            this.SectionDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SectionDataGridView_CellClick);
            this.SectionDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SectionDataGridView_CellDoubleClick);
            this.SectionDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.SectionDataGridView_CellFormatting);
            this.SectionDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.SectionDataGridView_CellValueChanged);
            // 
            // エリア
            // 
            this.エリア.DataPropertyName = "AREA_NAME";
            this.エリア.HeaderText = "エリア";
            this.エリア.Name = "エリア";
            this.エリア.ReadOnly = true;
            this.エリア.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // 駐車場NO
            // 
            this.駐車場NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.駐車場NO.DataPropertyName = "NAME";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.駐車場NO.DefaultCellStyle = dataGridViewCellStyle2;
            this.駐車場NO.HeaderText = "駐車場NO";
            this.駐車場NO.Name = "駐車場NO";
            this.駐車場NO.ReadOnly = true;
            this.駐車場NO.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // 状態
            // 
            this.状態.DataPropertyName = "STATUS_CODE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.状態.DefaultCellStyle = dataGridViewCellStyle3;
            this.状態.HeaderText = "状態";
            this.状態.Items.AddRange(new object[] {
            "空き",
            "使用中"});
            this.状態.MinimumWidth = 70;
            this.状態.Name = "状態";
            this.状態.ReadOnly = true;
            this.状態.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.状態.Width = 70;
            // 
            // LOCATION_NO
            // 
            this.LOCATION_NO.DataPropertyName = "LOCATION_NO";
            this.LOCATION_NO.HeaderText = "LOCATION_NO";
            this.LOCATION_NO.Name = "LOCATION_NO";
            this.LOCATION_NO.ReadOnly = true;
            this.LOCATION_NO.Visible = false;
            // 
            // AREA_NO
            // 
            this.AREA_NO.DataPropertyName = "AREA_NO";
            this.AREA_NO.HeaderText = "AREA_NO";
            this.AREA_NO.Name = "AREA_NO";
            this.AREA_NO.ReadOnly = true;
            this.AREA_NO.Visible = false;
            // 
            // SECTION_NO
            // 
            this.SECTION_NO.DataPropertyName = "SECTION_NO";
            this.SECTION_NO.HeaderText = "SECTION_NO";
            this.SECTION_NO.Name = "SECTION_NO";
            this.SECTION_NO.ReadOnly = true;
            this.SECTION_NO.Visible = false;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.HeaderText = "STATUS";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Visible = false;
            // 
            // CHANGE_STATUS
            // 
            this.CHANGE_STATUS.HeaderText = "CHANGE_STATUS";
            this.CHANGE_STATUS.Name = "CHANGE_STATUS";
            this.CHANGE_STATUS.ReadOnly = true;
            this.CHANGE_STATUS.Visible = false;
            // 
            // データID
            // 
            this.データID.DataPropertyName = "データID";
            this.データID.HeaderText = "データID";
            this.データID.Name = "データID";
            this.データID.ReadOnly = true;
            this.データID.Visible = false;
            // 
            // 管理票NO
            // 
            this.管理票NO.DataPropertyName = "管理票NO";
            this.管理票NO.HeaderText = "管理票NO";
            this.管理票NO.Name = "管理票NO";
            this.管理票NO.ReadOnly = true;
            this.管理票NO.Visible = false;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UpdateButton.BackColor = System.Drawing.SystemColors.Control;
            this.UpdateButton.Enabled = false;
            this.UpdateButton.Location = new System.Drawing.Point(932, 627);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(120, 30);
            this.UpdateButton.TabIndex = 1012;
            this.UpdateButton.Text = "状態更新";
            this.UpdateButton.UseVisualStyleBackColor = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // ModeButton
            // 
            this.ModeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModeButton.BackColor = System.Drawing.SystemColors.Control;
            this.ModeButton.Location = new System.Drawing.Point(1046, 140);
            this.ModeButton.Name = "ModeButton";
            this.ModeButton.Size = new System.Drawing.Size(120, 30);
            this.ModeButton.TabIndex = 4;
            this.ModeButton.Text = "編集モードへ";
            this.ModeButton.UseVisualStyleBackColor = false;
            this.ModeButton.Click += new System.EventHandler(this.ModeButton_Click);
            // 
            // LocationLinkLabel
            // 
            this.LocationLinkLabel.AutoSize = true;
            this.LocationLinkLabel.Location = new System.Drawing.Point(475, 44);
            this.LocationLinkLabel.Name = "LocationLinkLabel";
            this.LocationLinkLabel.Size = new System.Drawing.Size(0, 15);
            this.LocationLinkLabel.TabIndex = 1015;
            // 
            // AreaLinkLabel
            // 
            this.AreaLinkLabel.AutoSize = true;
            this.AreaLinkLabel.Location = new System.Drawing.Point(475, 74);
            this.AreaLinkLabel.Name = "AreaLinkLabel";
            this.AreaLinkLabel.Size = new System.Drawing.Size(0, 15);
            this.AreaLinkLabel.TabIndex = 1016;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "NAME";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn1.HeaderText = "駐車場NO";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // AreaCountLabel
            // 
            this.AreaCountLabel.AutoSize = true;
            this.AreaCountLabel.Location = new System.Drawing.Point(736, 44);
            this.AreaCountLabel.Name = "AreaCountLabel";
            this.AreaCountLabel.Size = new System.Drawing.Size(0, 15);
            this.AreaCountLabel.TabIndex = 1017;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(842, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 1018;
            this.label2.Text = "区画";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ParkingSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.SectionButton);
            this.Controls.Add(this.AreaButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ParkingSelectForm";
            this.Text = "ParkingSelect";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ParkingSelectForm_FormClosing);
            this.Load += new System.EventHandler(this.ItemCopyMoveForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.AreaButton, 0);
            this.Controls.SetChildIndex(this.SectionButton, 0);
            this.Controls.SetChildIndex(this.UpdateButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.RadioButtonPanel.ResumeLayout(false);
            this.RadioButtonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button AreaButton;
        private System.Windows.Forms.Button SectionButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox AreaComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox LocationComboBox;
        private System.Windows.Forms.DataGridView SectionDataGridView;
        private System.Windows.Forms.Panel RadioButtonPanel;
        private System.Windows.Forms.RadioButton UseRadioButton;
        private System.Windows.Forms.RadioButton FreeRadioButton;
        private System.Windows.Forms.RadioButton AllRadioButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button ModeButton;
        private System.Windows.Forms.LinkLabel AreaLinkLabel;
        private System.Windows.Forms.LinkLabel LocationLinkLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Label AreaCountLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn エリア;
        private System.Windows.Forms.DataGridViewTextBoxColumn 駐車場NO;
        private System.Windows.Forms.DataGridViewComboBoxColumn 状態;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCATION_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn AREA_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SECTION_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHANGE_STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn データID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 管理票NO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton AreaRadioButton;
    }
}