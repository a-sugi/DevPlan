namespace DevPlan.Presentation.UIDevPlan.ReportMaterial
{
    partial class ReportMaterialWeeklyForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.CondTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.FirstDayNullableDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.TrialProductionSeasonTextBox = new System.Windows.Forms.TextBox();
            this.LastDayNullableDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.InfoListDataGridView = new System.Windows.Forms.DataGridView();
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionGroupCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GeneralCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentSituation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FutureSchedule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EntryButton = new System.Windows.Forms.Button();
            this.AllCheckBox = new System.Windows.Forms.CheckBox();
            this.ContentsPanel.SuspendLayout();
            this.CondTableLayoutPanel.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstDayNullableDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LastDayNullableDateTimePicker)).BeginInit();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.Location = new System.Drawing.Point(1052, 605);
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.AllCheckBox);
            this.ContentsPanel.Controls.Add(this.MainPanel);
            this.ContentsPanel.Controls.Add(this.CondTableLayoutPanel);
            this.ContentsPanel.Controls.Add(this.label2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "検索条件";
            // 
            // CondTableLayoutPanel
            // 
            this.CondTableLayoutPanel.AutoSize = true;
            this.CondTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.CondTableLayoutPanel.ColumnCount = 2;
            this.CondTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.CondTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 468F));
            this.CondTableLayoutPanel.Controls.Add(this.StatusPanel, 1, 0);
            this.CondTableLayoutPanel.Controls.Add(this.label3, 0, 0);
            this.CondTableLayoutPanel.Location = new System.Drawing.Point(12, 29);
            this.CondTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CondTableLayoutPanel.Name = "CondTableLayoutPanel";
            this.CondTableLayoutPanel.RowCount = 1;
            this.CondTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CondTableLayoutPanel.Size = new System.Drawing.Size(591, 33);
            this.CondTableLayoutPanel.TabIndex = 0;
            // 
            // StatusPanel
            // 
            this.StatusPanel.Controls.Add(this.FirstDayNullableDateTimePicker);
            this.StatusPanel.Controls.Add(this.TrialProductionSeasonTextBox);
            this.StatusPanel.Controls.Add(this.LastDayNullableDateTimePicker);
            this.StatusPanel.Location = new System.Drawing.Point(122, 1);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(434, 30);
            this.StatusPanel.TabIndex = 0;
            // 
            // FirstDayNullableDateTimePicker
            // 
            this.FirstDayNullableDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.FirstDayNullableDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FirstDayNullableDateTimePicker.Location = new System.Drawing.Point(22, 4);
            this.FirstDayNullableDateTimePicker.Name = "FirstDayNullableDateTimePicker";
            this.FirstDayNullableDateTimePicker.Size = new System.Drawing.Size(140, 22);
            this.FirstDayNullableDateTimePicker.TabIndex = 7;
            this.FirstDayNullableDateTimePicker.Value = new System.DateTime(2017, 7, 20, 0, 0, 0, 0);
            this.FirstDayNullableDateTimePicker.CloseUp += new System.EventHandler(this.FirstDayNullableDateTimePicker_CloseUp);
            this.FirstDayNullableDateTimePicker.ValueChanged += new System.EventHandler(this.FirstDayNullableDateTimePicker_ValueChanged);
            // 
            // TrialProductionSeasonTextBox
            // 
            this.TrialProductionSeasonTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TrialProductionSeasonTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TrialProductionSeasonTextBox.Location = new System.Drawing.Point(179, 8);
            this.TrialProductionSeasonTextBox.Name = "TrialProductionSeasonTextBox";
            this.TrialProductionSeasonTextBox.Size = new System.Drawing.Size(50, 15);
            this.TrialProductionSeasonTextBox.TabIndex = 0;
            this.TrialProductionSeasonTextBox.TabStop = false;
            this.TrialProductionSeasonTextBox.Text = "～";
            this.TrialProductionSeasonTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LastDayNullableDateTimePicker
            // 
            this.LastDayNullableDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.LastDayNullableDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.LastDayNullableDateTimePicker.Location = new System.Drawing.Point(246, 4);
            this.LastDayNullableDateTimePicker.Name = "LastDayNullableDateTimePicker";
            this.LastDayNullableDateTimePicker.Size = new System.Drawing.Size(140, 22);
            this.LastDayNullableDateTimePicker.TabIndex = 8;
            this.LastDayNullableDateTimePicker.Value = new System.DateTime(2017, 7, 20, 0, 0, 0, 0);
            this.LastDayNullableDateTimePicker.CloseUp += new System.EventHandler(this.LastDayNullableDateTimePicker_CloseUp);
            this.LastDayNullableDateTimePicker.ValueChanged += new System.EventHandler(this.LastDayNullableDateTimePicker_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "期間";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.SearchButton);
            this.MainPanel.Controls.Add(this.MessageLabel);
            this.MainPanel.Controls.Add(this.InfoListDataGridView);
            this.MainPanel.Location = new System.Drawing.Point(0, 65);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1184, 468);
            this.MainPanel.TabIndex = 8;
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(12, 2);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 30);
            this.SearchButton.TabIndex = 9;
            this.SearchButton.Text = "検索";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // MessageLabel
            // 
            this.MessageLabel.ForeColor = System.Drawing.Color.Red;
            this.MessageLabel.Location = new System.Drawing.Point(140, 5);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(1015, 22);
            this.MessageLabel.TabIndex = 9;
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InfoListDataGridView
            // 
            this.InfoListDataGridView.AllowUserToAddRows = false;
            this.InfoListDataGridView.AllowUserToDeleteRows = false;
            this.InfoListDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoListDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InfoListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.InfoListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InfoListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckBox,
            this.Date,
            this.SectionGroupCode,
            this.GeneralCode,
            this.Category,
            this.CurrentSituation,
            this.FutureSchedule});
            this.InfoListDataGridView.Location = new System.Drawing.Point(12, 35);
            this.InfoListDataGridView.Name = "InfoListDataGridView";
            this.InfoListDataGridView.ReadOnly = true;
            this.InfoListDataGridView.RowHeadersVisible = false;
            this.InfoListDataGridView.RowTemplate.Height = 21;
            this.InfoListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InfoListDataGridView.Size = new System.Drawing.Size(1160, 439);
            this.InfoListDataGridView.StandardTab = true;
            this.InfoListDataGridView.TabIndex = 10;
            this.InfoListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InfoListDataGridView_CellClick);
            this.InfoListDataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.InfoListDataGridView_CellPainting);
            // 
            // CheckBox
            // 
            this.CheckBox.FalseValue = "false";
            this.CheckBox.HeaderText = "";
            this.CheckBox.IndeterminateValue = "CHECK_BOX";
            this.CheckBox.Name = "CheckBox";
            this.CheckBox.ReadOnly = true;
            this.CheckBox.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CheckBox.TrueValue = "true";
            this.CheckBox.Width = 30;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "LISTED_DATE";
            dataGridViewCellStyle2.Format = "yyyy/MM/dd";
            dataGridViewCellStyle2.NullValue = null;
            this.Date.DefaultCellStyle = dataGridViewCellStyle2;
            this.Date.HeaderText = "日付";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SectionGroupCode
            // 
            this.SectionGroupCode.DataPropertyName = "SECTION_GROUP_ID";
            this.SectionGroupCode.HeaderText = "担当";
            this.SectionGroupCode.Name = "SectionGroupCode";
            this.SectionGroupCode.ReadOnly = true;
            this.SectionGroupCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GeneralCode
            // 
            this.GeneralCode.DataPropertyName = "GENERAL_CODE";
            this.GeneralCode.HeaderText = "開発符号";
            this.GeneralCode.Name = "GeneralCode";
            this.GeneralCode.ReadOnly = true;
            this.GeneralCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "CATEGORY";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Category.DefaultCellStyle = dataGridViewCellStyle3;
            this.Category.HeaderText = "項目";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            this.Category.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Category.Width = 150;
            // 
            // CurrentSituation
            // 
            this.CurrentSituation.DataPropertyName = "CURRENT_SITUATION";
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CurrentSituation.DefaultCellStyle = dataGridViewCellStyle4;
            this.CurrentSituation.HeaderText = "現状";
            this.CurrentSituation.Name = "CurrentSituation";
            this.CurrentSituation.ReadOnly = true;
            this.CurrentSituation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CurrentSituation.Width = 300;
            // 
            // FutureSchedule
            // 
            this.FutureSchedule.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FutureSchedule.DataPropertyName = "FUTURE_SCHEDULE";
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FutureSchedule.DefaultCellStyle = dataGridViewCellStyle5;
            this.FutureSchedule.HeaderText = "今後の予定";
            this.FutureSchedule.Name = "FutureSchedule";
            this.FutureSchedule.ReadOnly = true;
            this.FutureSchedule.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(13, 605);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 11;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // AllCheckBox
            // 
            this.AllCheckBox.AutoSize = true;
            this.AllCheckBox.Location = new System.Drawing.Point(634, 18);
            this.AllCheckBox.Name = "AllCheckBox";
            this.AllCheckBox.Size = new System.Drawing.Size(15, 14);
            this.AllCheckBox.TabIndex = 9;
            this.AllCheckBox.TabStop = false;
            this.AllCheckBox.UseVisualStyleBackColor = true;
            this.AllCheckBox.Visible = false;
            this.AllCheckBox.CheckedChanged += new System.EventHandler(this.AllCheckBox_CheckedChanged);
            // 
            // ReportMaterialWeeklyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMinSize = new System.Drawing.Size(1160, 570);
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.EntryButton);
            this.Name = "ReportMaterialWeeklyForm";
            this.Text = "ReportMaterialWeeklyForm";
            this.Load += new System.EventHandler(this.InfoListWeeklyForm_Load);
            this.Shown += new System.EventHandler(this.InfoListWeeklyForm_Shown);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.CondTableLayoutPanel.ResumeLayout(false);
            this.CondTableLayoutPanel.PerformLayout();
            this.StatusPanel.ResumeLayout(false);
            this.StatusPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstDayNullableDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LastDayNullableDateTimePicker)).EndInit();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InfoListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel CondTableLayoutPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.TextBox TrialProductionSeasonTextBox;
        private UC.NullableDateTimePicker LastDayNullableDateTimePicker;
        private System.Windows.Forms.DataGridView InfoListDataGridView;
        private UC.NullableDateTimePicker FirstDayNullableDateTimePicker;
        private System.Windows.Forms.CheckBox AllCheckBox;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionGroupCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeneralCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentSituation;
        private System.Windows.Forms.DataGridViewTextBoxColumn FutureSchedule;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button SearchButton;
    }
}