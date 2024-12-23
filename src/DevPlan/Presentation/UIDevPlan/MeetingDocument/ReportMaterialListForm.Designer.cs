namespace DevPlan.Presentation.UIDevPlan.MeetingDocument
{
    partial class ReportMaterialListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.EntryButton = new System.Windows.Forms.Button();
            this.ReportMaterialDataGridView = new System.Windows.Forms.DataGridView();
            this.SelectedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ListedDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GeneralCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FutureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllSelectCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchConditionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TypePanel = new System.Windows.Forms.Panel();
            this.MonthlyReportRadioButton = new System.Windows.Forms.RadioButton();
            this.ProgressSituationtRadioButton = new System.Windows.Forms.RadioButton();
            this.WeeklyReportRadioButton = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.PeriodDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ListDataLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportMaterialDataGridView)).BeginInit();
            this.SearchConditionTableLayoutPanel.SuspendLayout();
            this.TypePanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(658, 527);
            this.CloseButton.TabIndex = 10;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.SearchButton);
            this.ListFormMainPanel.Controls.Add(this.ListDataLabel);
            this.ListFormMainPanel.Controls.Add(this.SearchConditionTableLayoutPanel);
            this.ListFormMainPanel.Controls.Add(this.label2);
            this.ListFormMainPanel.Controls.Add(this.AllSelectCheckBox);
            this.ListFormMainPanel.Controls.Add(this.ReportMaterialDataGridView);
            this.ListFormMainPanel.Size = new System.Drawing.Size(773, 517);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ReportMaterialDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.AllSelectCheckBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label2, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SearchConditionTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListDataLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SearchButton, 0);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 528);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 8;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // ReportMaterialDataGridView
            // 
            this.ReportMaterialDataGridView.AllowUserToAddRows = false;
            this.ReportMaterialDataGridView.AllowUserToDeleteRows = false;
            this.ReportMaterialDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReportMaterialDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ReportMaterialDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ReportMaterialDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReportMaterialDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedColumn,
            this.ListedDateColumn,
            this.SectionColumn,
            this.GeneralCodeColumn,
            this.CategoryColumn,
            this.CurrentColumn,
            this.FutureColumn,
            this.IdColumn});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReportMaterialDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ReportMaterialDataGridView.Location = new System.Drawing.Point(3, 153);
            this.ReportMaterialDataGridView.MultiSelect = false;
            this.ReportMaterialDataGridView.Name = "ReportMaterialDataGridView";
            this.ReportMaterialDataGridView.RowHeadersVisible = false;
            this.ReportMaterialDataGridView.RowTemplate.Height = 21;
            this.ReportMaterialDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ReportMaterialDataGridView.Size = new System.Drawing.Size(763, 359);
            this.ReportMaterialDataGridView.TabIndex = 7;
            // 
            // SelectedColumn
            // 
            this.SelectedColumn.FillWeight = 30F;
            this.SelectedColumn.Frozen = true;
            this.SelectedColumn.HeaderText = "";
            this.SelectedColumn.MinimumWidth = 30;
            this.SelectedColumn.Name = "SelectedColumn";
            this.SelectedColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SelectedColumn.Width = 30;
            // 
            // ListedDateColumn
            // 
            this.ListedDateColumn.DataPropertyName = "LISTED_DATE";
            dataGridViewCellStyle2.Format = "yyyy/MM/dd";
            this.ListedDateColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListedDateColumn.HeaderText = "日付";
            this.ListedDateColumn.MinimumWidth = 100;
            this.ListedDateColumn.Name = "ListedDateColumn";
            this.ListedDateColumn.ReadOnly = true;
            this.ListedDateColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ListedDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SectionColumn
            // 
            this.SectionColumn.DataPropertyName = "SECTION_CODE";
            this.SectionColumn.HeaderText = "担当";
            this.SectionColumn.MaxInputLength = 100;
            this.SectionColumn.MinimumWidth = 100;
            this.SectionColumn.Name = "SectionColumn";
            this.SectionColumn.ReadOnly = true;
            this.SectionColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SectionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GeneralCodeColumn
            // 
            this.GeneralCodeColumn.DataPropertyName = "GENERAL_CODE";
            this.GeneralCodeColumn.HeaderText = "開発符号";
            this.GeneralCodeColumn.MaxInputLength = 100;
            this.GeneralCodeColumn.MinimumWidth = 100;
            this.GeneralCodeColumn.Name = "GeneralCodeColumn";
            this.GeneralCodeColumn.ReadOnly = true;
            this.GeneralCodeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GeneralCodeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CategoryColumn
            // 
            this.CategoryColumn.DataPropertyName = "CATEGORY";
            this.CategoryColumn.FillWeight = 120F;
            this.CategoryColumn.HeaderText = "項目";
            this.CategoryColumn.MinimumWidth = 120;
            this.CategoryColumn.Name = "CategoryColumn";
            this.CategoryColumn.ReadOnly = true;
            this.CategoryColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CategoryColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CategoryColumn.Width = 120;
            // 
            // CurrentColumn
            // 
            this.CurrentColumn.DataPropertyName = "CURRENT_SITUATION";
            this.CurrentColumn.FillWeight = 150F;
            this.CurrentColumn.HeaderText = "現状";
            this.CurrentColumn.MinimumWidth = 150;
            this.CurrentColumn.Name = "CurrentColumn";
            this.CurrentColumn.ReadOnly = true;
            this.CurrentColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CurrentColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CurrentColumn.Width = 150;
            // 
            // FutureColumn
            // 
            this.FutureColumn.DataPropertyName = "FUTURE_SCHEDULE";
            this.FutureColumn.FillWeight = 150F;
            this.FutureColumn.HeaderText = "今後の予定";
            this.FutureColumn.MinimumWidth = 150;
            this.FutureColumn.Name = "FutureColumn";
            this.FutureColumn.ReadOnly = true;
            this.FutureColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FutureColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FutureColumn.Width = 150;
            // 
            // IdColumn
            // 
            this.IdColumn.DataPropertyName = "ID";
            this.IdColumn.HeaderText = "ID";
            this.IdColumn.Name = "IdColumn";
            this.IdColumn.ReadOnly = true;
            this.IdColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IdColumn.Visible = false;
            // 
            // AllSelectCheckBox
            // 
            this.AllSelectCheckBox.AutoSize = true;
            this.AllSelectCheckBox.Location = new System.Drawing.Point(13, 159);
            this.AllSelectCheckBox.Name = "AllSelectCheckBox";
            this.AllSelectCheckBox.Size = new System.Drawing.Size(15, 14);
            this.AllSelectCheckBox.TabIndex = 1011;
            this.AllSelectCheckBox.UseVisualStyleBackColor = true;
            this.AllSelectCheckBox.CheckedChanged += new System.EventHandler(this.AllSelectCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1012;
            this.label2.Text = "検索条件";
            // 
            // SearchConditionTableLayoutPanel
            // 
            this.SearchConditionTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SearchConditionTableLayoutPanel.ColumnCount = 2;
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.SearchConditionTableLayoutPanel.Controls.Add(this.TypePanel, 1, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.panel2, 1, 1);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label4, 0, 1);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label3, 0, 0);
            this.SearchConditionTableLayoutPanel.Location = new System.Drawing.Point(3, 51);
            this.SearchConditionTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchConditionTableLayoutPanel.Name = "SearchConditionTableLayoutPanel";
            this.SearchConditionTableLayoutPanel.RowCount = 2;
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SearchConditionTableLayoutPanel.Size = new System.Drawing.Size(330, 63);
            this.SearchConditionTableLayoutPanel.TabIndex = 1013;
            // 
            // TypePanel
            // 
            this.TypePanel.Controls.Add(this.MonthlyReportRadioButton);
            this.TypePanel.Controls.Add(this.ProgressSituationtRadioButton);
            this.TypePanel.Controls.Add(this.WeeklyReportRadioButton);
            this.TypePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TypePanel.Location = new System.Drawing.Point(102, 1);
            this.TypePanel.Margin = new System.Windows.Forms.Padding(0);
            this.TypePanel.Name = "TypePanel";
            this.TypePanel.Size = new System.Drawing.Size(220, 30);
            this.TypePanel.TabIndex = 11;
            // 
            // MonthlyReportRadioButton
            // 
            this.MonthlyReportRadioButton.AutoSize = true;
            this.MonthlyReportRadioButton.Location = new System.Drawing.Point(156, 6);
            this.MonthlyReportRadioButton.Name = "MonthlyReportRadioButton";
            this.MonthlyReportRadioButton.Size = new System.Drawing.Size(55, 19);
            this.MonthlyReportRadioButton.TabIndex = 12;
            this.MonthlyReportRadioButton.Tag = "5";
            this.MonthlyReportRadioButton.Text = "月報";
            this.MonthlyReportRadioButton.UseVisualStyleBackColor = true;
            // 
            // ProgressSituationtRadioButton
            // 
            this.ProgressSituationtRadioButton.AutoSize = true;
            this.ProgressSituationtRadioButton.Location = new System.Drawing.Point(4, 6);
            this.ProgressSituationtRadioButton.Name = "ProgressSituationtRadioButton";
            this.ProgressSituationtRadioButton.Size = new System.Drawing.Size(85, 19);
            this.ProgressSituationtRadioButton.TabIndex = 10;
            this.ProgressSituationtRadioButton.Tag = "1";
            this.ProgressSituationtRadioButton.Text = "進捗状況";
            this.ProgressSituationtRadioButton.UseVisualStyleBackColor = true;
            // 
            // WeeklyReportRadioButton
            // 
            this.WeeklyReportRadioButton.AutoSize = true;
            this.WeeklyReportRadioButton.Checked = true;
            this.WeeklyReportRadioButton.Location = new System.Drawing.Point(95, 6);
            this.WeeklyReportRadioButton.Name = "WeeklyReportRadioButton";
            this.WeeklyReportRadioButton.Size = new System.Drawing.Size(55, 19);
            this.WeeklyReportRadioButton.TabIndex = 11;
            this.WeeklyReportRadioButton.TabStop = true;
            this.WeeklyReportRadioButton.Tag = "3";
            this.WeeklyReportRadioButton.Text = "週報";
            this.WeeklyReportRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.PeriodDateTimePicker);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(102, 32);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 30);
            this.panel2.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label15.Location = new System.Drawing.Point(127, 9);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 14);
            this.label15.TabIndex = 4;
            this.label15.Text = "以降";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PeriodDateTimePicker
            // 
            this.PeriodDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.PeriodDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PeriodDateTimePicker.Location = new System.Drawing.Point(4, 5);
            this.PeriodDateTimePicker.Name = "PeriodDateTimePicker";
            this.PeriodDateTimePicker.Size = new System.Drawing.Size(120, 22);
            this.PeriodDateTimePicker.TabIndex = 5;
            this.PeriodDateTimePicker.Tag = "Required;ItemName(期間)";
            this.PeriodDateTimePicker.Value = new System.DateTime(2017, 4, 3, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Aquamarine;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(1, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 30);
            this.label4.TabIndex = 1;
            this.label4.Text = "期間";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "種別";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListDataLabel
            // 
            this.ListDataLabel.AutoSize = true;
            this.ListDataLabel.ForeColor = System.Drawing.Color.Red;
            this.ListDataLabel.Location = new System.Drawing.Point(129, 125);
            this.ListDataLabel.Name = "ListDataLabel";
            this.ListDataLabel.Size = new System.Drawing.Size(0, 15);
            this.ListDataLabel.TabIndex = 1023;
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(3, 117);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 30);
            this.SearchButton.TabIndex = 1024;
            this.SearchButton.Text = "検索";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // ReportMaterialListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.EntryButton);
            this.Name = "ReportMaterialListForm";
            this.Text = "ReportMaterialForm";
            this.Load += new System.EventHandler(this.ReportMaterialListForm_Load);
            this.Shown += new System.EventHandler(this.ReportMaterialListForm_Shown);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportMaterialDataGridView)).EndInit();
            this.SearchConditionTableLayoutPanel.ResumeLayout(false);
            this.SearchConditionTableLayoutPanel.PerformLayout();
            this.TypePanel.ResumeLayout(false);
            this.TypePanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.DataGridView ReportMaterialDataGridView;
        private System.Windows.Forms.CheckBox AllSelectCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel SearchConditionTableLayoutPanel;
        private System.Windows.Forms.Panel TypePanel;
        private System.Windows.Forms.RadioButton MonthlyReportRadioButton;
        private System.Windows.Forms.RadioButton ProgressSituationtRadioButton;
        private System.Windows.Forms.RadioButton WeeklyReportRadioButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker PeriodDateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ListDataLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListedDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeneralCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FutureColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdColumn;
        private System.Windows.Forms.Button SearchButton;
    }
}