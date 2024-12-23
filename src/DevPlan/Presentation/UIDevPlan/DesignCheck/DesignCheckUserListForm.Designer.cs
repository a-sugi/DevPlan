namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    partial class DesignCheckUserListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DesignCheckNameLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.UnregisteredUserAddbutton = new System.Windows.Forms.Button();
            this.UserDeletebutton = new System.Windows.Forms.Button();
            this.UserAddButton = new System.Windows.Forms.Button();
            this.ListDataGridView = new System.Windows.Forms.DataGridView();
            this.AllSelectCheckBox = new System.Windows.Forms.CheckBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.部 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.課 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.担当 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.氏名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventDateIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContentsPanel.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.Location = new System.Drawing.Point(552, 450);
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.AllSelectCheckBox);
            this.ContentsPanel.Controls.Add(this.ListDataGridView);
            this.ContentsPanel.Controls.Add(this.UnregisteredUserAddbutton);
            this.ContentsPanel.Controls.Add(this.UserDeletebutton);
            this.ContentsPanel.Controls.Add(this.UserAddButton);
            this.ContentsPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ContentsPanel.Size = new System.Drawing.Size(684, 418);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadButton.BackColor = System.Drawing.SystemColors.Control;
            this.DownloadButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DownloadButton.Location = new System.Drawing.Point(426, 450);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(120, 20);
            this.DownloadButton.TabIndex = 1013;
            this.DownloadButton.Text = "Excel出力";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.Controls.Add(this.label5, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.label3, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.DesignCheckNameLabel, 1, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.DateLabel, 1, 0);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(12, 10);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 2;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(485, 63);
            this.DetailTableLayoutPanel.TabIndex = 1014;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Aquamarine;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(1, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 30);
            this.label5.TabIndex = 1031;
            this.label5.Text = "設計チェック名";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 30);
            this.label3.TabIndex = 1030;
            this.label3.Text = "開催日";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DesignCheckNameLabel
            // 
            this.DesignCheckNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DesignCheckNameLabel.Location = new System.Drawing.Point(122, 32);
            this.DesignCheckNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.DesignCheckNameLabel.Name = "DesignCheckNameLabel";
            this.DesignCheckNameLabel.Size = new System.Drawing.Size(362, 30);
            this.DesignCheckNameLabel.TabIndex = 1014;
            this.DesignCheckNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DateLabel
            // 
            this.DateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateLabel.Location = new System.Drawing.Point(122, 1);
            this.DateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(362, 30);
            this.DateLabel.TabIndex = 1014;
            this.DateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UnregisteredUserAddbutton
            // 
            this.UnregisteredUserAddbutton.BackColor = System.Drawing.SystemColors.Control;
            this.UnregisteredUserAddbutton.Location = new System.Drawing.Point(137, 78);
            this.UnregisteredUserAddbutton.Name = "UnregisteredUserAddbutton";
            this.UnregisteredUserAddbutton.Size = new System.Drawing.Size(140, 30);
            this.UnregisteredUserAddbutton.TabIndex = 1016;
            this.UnregisteredUserAddbutton.Text = "未登録参加者追加";
            this.UnregisteredUserAddbutton.UseVisualStyleBackColor = false;
            this.UnregisteredUserAddbutton.Visible = false;
            this.UnregisteredUserAddbutton.Click += new System.EventHandler(this.UnregisteredUserAddbutton_Click);
            // 
            // UserDeletebutton
            // 
            this.UserDeletebutton.BackColor = System.Drawing.SystemColors.Control;
            this.UserDeletebutton.Location = new System.Drawing.Point(282, 78);
            this.UserDeletebutton.Name = "UserDeletebutton";
            this.UserDeletebutton.Size = new System.Drawing.Size(120, 30);
            this.UserDeletebutton.TabIndex = 1017;
            this.UserDeletebutton.Text = "参加者削除";
            this.UserDeletebutton.UseVisualStyleBackColor = false;
            this.UserDeletebutton.Visible = false;
            this.UserDeletebutton.Click += new System.EventHandler(this.UserDeletebutton_Click);
            // 
            // UserAddButton
            // 
            this.UserAddButton.BackColor = System.Drawing.SystemColors.Control;
            this.UserAddButton.Location = new System.Drawing.Point(12, 78);
            this.UserAddButton.Name = "UserAddButton";
            this.UserAddButton.Size = new System.Drawing.Size(120, 30);
            this.UserAddButton.TabIndex = 1015;
            this.UserAddButton.Text = "参加者追加";
            this.UserAddButton.UseVisualStyleBackColor = false;
            this.UserAddButton.Visible = false;
            this.UserAddButton.Click += new System.EventHandler(this.UserAddButton_Click);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedColumn,
            this.部,
            this.課,
            this.担当,
            this.氏名,
            this.IdColumn,
            this.EventDateIdColumn,
            this.UserIdColumn});
            this.ListDataGridView.Location = new System.Drawing.Point(9, 114);
            this.ListDataGridView.MultiSelect = false;
            this.ListDataGridView.Name = "ListDataGridView";
            this.ListDataGridView.RowHeadersVisible = false;
            this.ListDataGridView.RowTemplate.Height = 21;
            this.ListDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ListDataGridView.Size = new System.Drawing.Size(668, 298);
            this.ListDataGridView.TabIndex = 1018;
            // 
            // AllSelectCheckBox
            // 
            this.AllSelectCheckBox.AutoSize = true;
            this.AllSelectCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.AllSelectCheckBox.Location = new System.Drawing.Point(22, 120);
            this.AllSelectCheckBox.Name = "AllSelectCheckBox";
            this.AllSelectCheckBox.Size = new System.Drawing.Size(15, 14);
            this.AllSelectCheckBox.TabIndex = 1019;
            this.AllSelectCheckBox.TabStop = false;
            this.AllSelectCheckBox.UseVisualStyleBackColor = false;
            this.AllSelectCheckBox.Visible = false;
            this.AllSelectCheckBox.CheckedChanged += new System.EventHandler(this.AllSelectCheckBox_CheckedChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "DEPARTMENT_CODE";
            this.dataGridViewTextBoxColumn1.HeaderText = "部";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 28;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "SECTION_CODE";
            this.dataGridViewTextBoxColumn2.HeaderText = "課";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 28;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "SECTION_GROUP_CODE";
            this.dataGridViewTextBoxColumn3.HeaderText = "担当";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 43;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "NAME";
            this.dataGridViewTextBoxColumn4.HeaderText = "氏名";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 43;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn5.HeaderText = "参加者テーブルID";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 137;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "開催日_ID";
            this.dataGridViewTextBoxColumn6.HeaderText = "開催日_ID";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn6.Width = 96;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "PERSONEL_ID";
            this.dataGridViewTextBoxColumn7.HeaderText = "参加者_ID";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            this.dataGridViewTextBoxColumn7.Width = 96;
            // 
            // SelectedColumn
            // 
            this.SelectedColumn.FillWeight = 30F;
            this.SelectedColumn.HeaderText = "";
            this.SelectedColumn.MinimumWidth = 30;
            this.SelectedColumn.Name = "SelectedColumn";
            this.SelectedColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SelectedColumn.Visible = false;
            this.SelectedColumn.Width = 30;
            // 
            // 部
            // 
            this.部.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.部.DataPropertyName = "DEPARTMENT_CODE";
            this.部.FillWeight = 20F;
            this.部.HeaderText = "部";
            this.部.MinimumWidth = 30;
            this.部.Name = "部";
            this.部.ReadOnly = true;
            this.部.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 課
            // 
            this.課.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.課.DataPropertyName = "SECTION_CODE";
            this.課.FillWeight = 20F;
            this.課.HeaderText = "課";
            this.課.MinimumWidth = 30;
            this.課.Name = "課";
            this.課.ReadOnly = true;
            this.課.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 担当
            // 
            this.担当.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.担当.DataPropertyName = "SECTION_GROUP_CODE";
            this.担当.FillWeight = 20F;
            this.担当.HeaderText = "担当";
            this.担当.MinimumWidth = 30;
            this.担当.Name = "担当";
            this.担当.ReadOnly = true;
            this.担当.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 氏名
            // 
            this.氏名.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.氏名.DataPropertyName = "NAME";
            this.氏名.FillWeight = 40F;
            this.氏名.HeaderText = "氏名";
            this.氏名.MinimumWidth = 40;
            this.氏名.Name = "氏名";
            this.氏名.ReadOnly = true;
            this.氏名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IdColumn
            // 
            this.IdColumn.DataPropertyName = "ID";
            this.IdColumn.HeaderText = "参加者テーブルID";
            this.IdColumn.Name = "IdColumn";
            this.IdColumn.ReadOnly = true;
            this.IdColumn.Visible = false;
            this.IdColumn.Width = 137;
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
            // UserIdColumn
            // 
            this.UserIdColumn.DataPropertyName = "PERSONEL_ID";
            this.UserIdColumn.HeaderText = "参加者_ID";
            this.UserIdColumn.Name = "UserIdColumn";
            this.UserIdColumn.ReadOnly = true;
            this.UserIdColumn.Visible = false;
            this.UserIdColumn.Width = 96;
            // 
            // DesignCheckUserListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(684, 495);
            this.Controls.Add(this.DownloadButton);
            this.Name = "DesignCheckUserListForm";
            this.Text = "";
            this.Activated += new System.EventHandler(this.DesignCheckUserListForm_Activated);
            this.Load += new System.EventHandler(this.DesignCheckUserListForm_Load);
            this.Shown += new System.EventHandler(this.DesignCheckUserListForm_Shown);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.Controls.SetChildIndex(this.DownloadButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label DesignCheckNameLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Button UnregisteredUserAddbutton;
        private System.Windows.Forms.Button UserDeletebutton;
        private System.Windows.Forms.Button UserAddButton;
        private System.Windows.Forms.DataGridView ListDataGridView;
        private System.Windows.Forms.CheckBox AllSelectCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 部;
        private System.Windows.Forms.DataGridViewTextBoxColumn 課;
        private System.Windows.Forms.DataGridViewTextBoxColumn 担当;
        private System.Windows.Forms.DataGridViewTextBoxColumn 氏名;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventDateIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserIdColumn;
    }
}