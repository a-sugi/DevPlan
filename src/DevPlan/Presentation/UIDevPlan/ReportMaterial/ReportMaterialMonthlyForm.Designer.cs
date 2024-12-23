namespace DevPlan.Presentation.UIDevPlan.ReportMaterial
{
    partial class ReportMaterialMonthlyForm
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.InfoListDataGridView = new System.Windows.Forms.DataGridView();
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentSituation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FutureSchedule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.EntryButton = new System.Windows.Forms.Button();
            this.AllCheckBox = new System.Windows.Forms.CheckBox();
            this.ContentsPanel.SuspendLayout();
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
            this.ContentsPanel.Controls.Add(this.MessageLabel);
            this.ContentsPanel.Controls.Add(this.AllCheckBox);
            this.ContentsPanel.Controls.Add(this.MainPanel);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.InfoListDataGridView);
            this.MainPanel.Location = new System.Drawing.Point(0, 29);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1184, 513);
            this.MainPanel.TabIndex = 8;
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
            this.SectionCode,
            this.Category,
            this.CurrentSituation,
            this.FutureSchedule});
            this.InfoListDataGridView.Location = new System.Drawing.Point(12, 3);
            this.InfoListDataGridView.Name = "InfoListDataGridView";
            this.InfoListDataGridView.ReadOnly = true;
            this.InfoListDataGridView.RowHeadersVisible = false;
            this.InfoListDataGridView.RowTemplate.Height = 21;
            this.InfoListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InfoListDataGridView.Size = new System.Drawing.Size(1159, 500);
            this.InfoListDataGridView.StandardTab = true;
            this.InfoListDataGridView.TabIndex = 9;
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
            this.Date.Visible = false;
            // 
            // SectionCode
            // 
            this.SectionCode.DataPropertyName = "SECTION_CODE";
            this.SectionCode.HeaderText = "課名";
            this.SectionCode.Name = "SectionCode";
            this.SectionCode.ReadOnly = true;
            this.SectionCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            this.CurrentSituation.HeaderText = "進度状況";
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
            this.FutureSchedule.HeaderText = "見通し・対応策・日程";
            this.FutureSchedule.Name = "FutureSchedule";
            this.FutureSchedule.ReadOnly = true;
            this.FutureSchedule.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MessageLabel
            // 
            this.MessageLabel.ForeColor = System.Drawing.Color.Red;
            this.MessageLabel.Location = new System.Drawing.Point(10, 4);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(272, 22);
            this.MessageLabel.TabIndex = 11;
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(13, 605);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 10;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // AllCheckBox
            // 
            this.AllCheckBox.AutoSize = true;
            this.AllCheckBox.Location = new System.Drawing.Point(1025, 5);
            this.AllCheckBox.Name = "AllCheckBox";
            this.AllCheckBox.Size = new System.Drawing.Size(15, 14);
            this.AllCheckBox.TabIndex = 9;
            this.AllCheckBox.TabStop = false;
            this.AllCheckBox.UseVisualStyleBackColor = true;
            this.AllCheckBox.Visible = false;
            this.AllCheckBox.CheckedChanged += new System.EventHandler(this.AllCheckBox_CheckedChanged);
            // 
            // ReportMaterialMonthlyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMinSize = new System.Drawing.Size(1160, 570);
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.EntryButton);
            this.Name = "ReportMaterialMonthlyForm";
            this.Text = "ReportMaterialMonthlyForm";
            this.Load += new System.EventHandler(this.InfoListMonthlyForm_Load);
            this.Shown += new System.EventHandler(this.InfoListMonthlyForm_Shown);            
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InfoListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.DataGridView InfoListDataGridView;
        private System.Windows.Forms.CheckBox AllCheckBox;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentSituation;
        private System.Windows.Forms.DataGridViewTextBoxColumn FutureSchedule;
    }
}