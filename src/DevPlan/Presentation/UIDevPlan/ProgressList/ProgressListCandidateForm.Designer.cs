namespace DevPlan.Presentation.UIDevPlan.ProgressList
{
    partial class ProgressListCandidateForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.NameListDataGridView = new System.Windows.Forms.DataGridView();
            this.リスト名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.開発符号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.性能名ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.EntryButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NameListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(264, 451);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.MessageLabel);
            this.ListFormMainPanel.Controls.Add(this.NameListDataGridView);
            this.ListFormMainPanel.Size = new System.Drawing.Size(384, 440);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.NameListDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.MessageLabel, 0);
            // 
            // NameListDataGridView
            // 
            this.NameListDataGridView.AllowUserToAddRows = false;
            this.NameListDataGridView.AllowUserToDeleteRows = false;
            this.NameListDataGridView.AllowUserToOrderColumns = true;
            this.NameListDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NameListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.NameListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NameListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.リスト名,
            this.開発符号,
            this.性能名ID});
            this.NameListDataGridView.Location = new System.Drawing.Point(3, 63);
            this.NameListDataGridView.MultiSelect = false;
            this.NameListDataGridView.Name = "NameListDataGridView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NameListDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.NameListDataGridView.RowHeadersVisible = false;
            this.NameListDataGridView.RowTemplate.Height = 21;
            this.NameListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.NameListDataGridView.Size = new System.Drawing.Size(376, 372);
            this.NameListDataGridView.TabIndex = 1013;
            // 
            // リスト名
            // 
            this.リスト名.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.リスト名.HeaderText = "目標進度リスト名";
            this.リスト名.Name = "リスト名";
            this.リスト名.ReadOnly = true;
            this.リスト名.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.リスト名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 開発符号
            // 
            this.開発符号.HeaderText = "開発符号";
            this.開発符号.Name = "開発符号";
            this.開発符号.ReadOnly = true;
            this.開発符号.Visible = false;
            // 
            // 性能名ID
            // 
            this.性能名ID.HeaderText = "性能名ID";
            this.性能名ID.Name = "性能名ID";
            this.性能名ID.ReadOnly = true;
            this.性能名ID.Visible = false;
            // 
            // MessageLabel
            // 
            this.MessageLabel.ForeColor = System.Drawing.Color.Red;
            this.MessageLabel.Location = new System.Drawing.Point(6, 38);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(372, 22);
            this.MessageLabel.TabIndex = 1014;
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 451);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 1013;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // ProgressListCandidateForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(404, 498);
            this.Controls.Add(this.EntryButton);
            this.MinimumSize = new System.Drawing.Size(410, 520);
            this.Name = "ProgressListCandidateForm";
            this.Text = "タイトルが設定されていません - 開発計画表システム";
            this.Load += new System.EventHandler(this.ProgressListCandidateForm_Load);
            this.Shown += new System.EventHandler(this.ProgressListCandidateForm_Shown);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NameListDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView NameListDataGridView;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn リスト名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 開発符号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 性能名ID;
        private System.Windows.Forms.Button EntryButton;
    }
}