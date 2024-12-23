namespace DevPlan.Presentation.UIDevPlan.WeeklyReport
{
    partial class WeeklyReportApprovalForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ApproveHistoryListDataGridView = new System.Windows.Forms.DataGridView();
            this.FlagColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionGroupColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApproveHistoryListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(532, 290);
            this.CloseButton.TabIndex = 1;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.MessageLabel);
            this.ListFormMainPanel.Controls.Add(this.ApproveHistoryListDataGridView);
            this.ListFormMainPanel.Location = new System.Drawing.Point(12, 12);
            this.ListFormMainPanel.Size = new System.Drawing.Size(639, 272);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ApproveHistoryListDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.MessageLabel, 0);
            // 
            // ApproveHistoryListDataGridView
            // 
            this.ApproveHistoryListDataGridView.AllowUserToAddRows = false;
            this.ApproveHistoryListDataGridView.AllowUserToDeleteRows = false;
            this.ApproveHistoryListDataGridView.AllowUserToOrderColumns = true;
            this.ApproveHistoryListDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ApproveHistoryListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.ApproveHistoryListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ApproveHistoryListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FlagColumn,
            this.StatusColumn,
            this.dateColumn,
            this.SectionGroupColumn,
            this.NameColumn});
            this.ApproveHistoryListDataGridView.Location = new System.Drawing.Point(19, 62);
            this.ApproveHistoryListDataGridView.Name = "ApproveHistoryListDataGridView";
            this.ApproveHistoryListDataGridView.ReadOnly = true;
            this.ApproveHistoryListDataGridView.RowHeadersVisible = false;
            this.ApproveHistoryListDataGridView.RowTemplate.Height = 21;
            this.ApproveHistoryListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ApproveHistoryListDataGridView.Size = new System.Drawing.Size(600, 200);
            this.ApproveHistoryListDataGridView.StandardTab = true;
            this.ApproveHistoryListDataGridView.TabIndex = 2;
            // 
            // FlagColumn
            // 
            this.FlagColumn.DataPropertyName = "FLAG_承認";
            this.FlagColumn.HeaderText = "状態";
            this.FlagColumn.Name = "FlagColumn";
            this.FlagColumn.ReadOnly = true;
            this.FlagColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FlagColumn.Visible = false;
            // 
            // StatusColumn
            // 
            this.StatusColumn.HeaderText = "状態";
            this.StatusColumn.Name = "StatusColumn";
            this.StatusColumn.ReadOnly = true;
            this.StatusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dateColumn
            // 
            this.dateColumn.DataPropertyName = "承認日時";
            dataGridViewCellStyle6.Format = "yyyy/MM/dd HH:mm:ss";
            dataGridViewCellStyle6.NullValue = null;
            this.dateColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.dateColumn.HeaderText = "日時";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            this.dateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dateColumn.Width = 200;
            // 
            // SectionGroupColumn
            // 
            this.SectionGroupColumn.DataPropertyName = "SECTION_CODE";
            this.SectionGroupColumn.HeaderText = "担当";
            this.SectionGroupColumn.Name = "SectionGroupColumn";
            this.SectionGroupColumn.ReadOnly = true;
            this.SectionGroupColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SectionGroupColumn.Width = 150;
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.DataPropertyName = "NAME";
            this.NameColumn.HeaderText = "氏名";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MessageLabel
            // 
            this.MessageLabel.ForeColor = System.Drawing.Color.Red;
            this.MessageLabel.Location = new System.Drawing.Point(39, 35);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(517, 22);
            this.MessageLabel.TabIndex = 1011;
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WeeklyReportApprovalForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(664, 323);
            this.MinimumSize = new System.Drawing.Size(680, 360);
            this.Name = "WeeklyReportApprovalForm";
            this.Text = "";
            this.Load += new System.EventHandler(this.WeeklyReportApproveForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApproveHistoryListDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView ApproveHistoryListDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlagColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionGroupColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.Label MessageLabel;
    }
}