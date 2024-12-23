namespace DevPlan.Presentation.UITestCar.Othe.ControlSheet
{
    partial class ControlSheetSaveForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.OkButton = new System.Windows.Forms.Button();
            this.HiddenDataGridView = new System.Windows.Forms.DataGridView();
            this.HiddenNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.RightButton = new System.Windows.Forms.Button();
            this.LeftButton = new System.Windows.Forms.Button();
            this.DisplayDataGridView = new System.Windows.Forms.DataGridView();
            this.DisplayNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SortColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DownButton = new System.Windows.Forms.Button();
            this.UpButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchConditionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SearchConditionNameTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HiddenDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayDataGridView)).BeginInit();
            this.SearchConditionTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(468, 367);
            this.CloseButton.TabIndex = 9;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.SearchConditionTableLayoutPanel);
            this.ListFormMainPanel.Controls.Add(this.label2);
            this.ListFormMainPanel.Controls.Add(this.DownButton);
            this.ListFormMainPanel.Controls.Add(this.UpButton);
            this.ListFormMainPanel.Controls.Add(this.DisplayDataGridView);
            this.ListFormMainPanel.Controls.Add(this.LeftButton);
            this.ListFormMainPanel.Controls.Add(this.RightButton);
            this.ListFormMainPanel.Controls.Add(this.label1);
            this.ListFormMainPanel.Controls.Add(this.HiddenDataGridView);
            this.ListFormMainPanel.Size = new System.Drawing.Size(583, 357);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.HiddenDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RightButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.LeftButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DisplayDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.UpButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DownButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label2, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SearchConditionTableLayoutPanel, 0);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkButton.BackColor = System.Drawing.SystemColors.Control;
            this.OkButton.Location = new System.Drawing.Point(5, 367);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(120, 30);
            this.OkButton.TabIndex = 8;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // HiddenDataGridView
            // 
            this.HiddenDataGridView.AllowUserToAddRows = false;
            this.HiddenDataGridView.AllowUserToDeleteRows = false;
            this.HiddenDataGridView.AllowUserToResizeColumns = false;
            this.HiddenDataGridView.AllowUserToResizeRows = false;
            this.HiddenDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HiddenDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.HiddenDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HiddenDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.HiddenDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HiddenDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HiddenNameColumn,
            this.Column1});
            this.HiddenDataGridView.Location = new System.Drawing.Point(3, 98);
            this.HiddenDataGridView.MultiSelect = false;
            this.HiddenDataGridView.Name = "HiddenDataGridView";
            this.HiddenDataGridView.ReadOnly = true;
            this.HiddenDataGridView.RowHeadersVisible = false;
            this.HiddenDataGridView.RowTemplate.Height = 21;
            this.HiddenDataGridView.Size = new System.Drawing.Size(180, 254);
            this.HiddenDataGridView.TabIndex = 2;
            // 
            // HiddenNameColumn
            // 
            this.HiddenNameColumn.DataPropertyName = "Name";
            this.HiddenNameColumn.HeaderText = "名前";
            this.HiddenNameColumn.Name = "HiddenNameColumn";
            this.HiddenNameColumn.ReadOnly = true;
            this.HiddenNameColumn.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "HeaderText";
            this.Column1.HeaderText = "列名";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 1012;
            this.label1.Text = "○表示しない列";
            // 
            // RightButton
            // 
            this.RightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RightButton.BackColor = System.Drawing.SystemColors.Control;
            this.RightButton.Location = new System.Drawing.Point(189, 176);
            this.RightButton.Name = "RightButton";
            this.RightButton.Size = new System.Drawing.Size(45, 30);
            this.RightButton.TabIndex = 3;
            this.RightButton.Text = ">>";
            this.RightButton.UseVisualStyleBackColor = false;
            this.RightButton.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // LeftButton
            // 
            this.LeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LeftButton.BackColor = System.Drawing.SystemColors.Control;
            this.LeftButton.Location = new System.Drawing.Point(189, 212);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Size = new System.Drawing.Size(45, 30);
            this.LeftButton.TabIndex = 4;
            this.LeftButton.Text = "<<";
            this.LeftButton.UseVisualStyleBackColor = false;
            this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // DisplayDataGridView
            // 
            this.DisplayDataGridView.AllowUserToAddRows = false;
            this.DisplayDataGridView.AllowUserToDeleteRows = false;
            this.DisplayDataGridView.AllowUserToResizeColumns = false;
            this.DisplayDataGridView.AllowUserToResizeRows = false;
            this.DisplayDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DisplayDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DisplayDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DisplayDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DisplayDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DisplayDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DisplayNameColumn,
            this.dataGridViewTextBoxColumn1,
            this.SortColumn,
            this.Column2});
            this.DisplayDataGridView.Location = new System.Drawing.Point(240, 98);
            this.DisplayDataGridView.MultiSelect = false;
            this.DisplayDataGridView.Name = "DisplayDataGridView";
            this.DisplayDataGridView.RowHeadersVisible = false;
            this.DisplayDataGridView.RowTemplate.Height = 21;
            this.DisplayDataGridView.Size = new System.Drawing.Size(287, 254);
            this.DisplayDataGridView.TabIndex = 5;
            this.DisplayDataGridView.Tag = "ItemName(表示する列)";
            this.DisplayDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DisplayDataGridView_CellClick);
            // 
            // DisplayNameColumn
            // 
            this.DisplayNameColumn.DataPropertyName = "Name";
            this.DisplayNameColumn.HeaderText = "名前";
            this.DisplayNameColumn.Name = "DisplayNameColumn";
            this.DisplayNameColumn.Visible = false;
            this.DisplayNameColumn.Width = 62;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "HeaderText";
            this.dataGridViewTextBoxColumn1.HeaderText = "列名";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SortColumn
            // 
            this.SortColumn.DataPropertyName = "SortNo";
            this.SortColumn.HeaderText = "ソート順";
            this.SortColumn.MaxInputLength = 3;
            this.SortColumn.Name = "SortColumn";
            this.SortColumn.Width = 81;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "IsAsc";
            this.Column2.HeaderText = "昇順";
            this.Column2.Name = "Column2";
            this.Column2.Width = 43;
            // 
            // DownButton
            // 
            this.DownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DownButton.BackColor = System.Drawing.SystemColors.Control;
            this.DownButton.Location = new System.Drawing.Point(533, 322);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(45, 30);
            this.DownButton.TabIndex = 7;
            this.DownButton.Text = "下へ";
            this.DownButton.UseVisualStyleBackColor = false;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // UpButton
            // 
            this.UpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpButton.BackColor = System.Drawing.SystemColors.Control;
            this.UpButton.Location = new System.Drawing.Point(533, 286);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(45, 30);
            this.UpButton.TabIndex = 6;
            this.UpButton.Text = "上へ";
            this.UpButton.UseVisualStyleBackColor = false;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 1018;
            this.label2.Text = "●表示する列";
            // 
            // SearchConditionTableLayoutPanel
            // 
            this.SearchConditionTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SearchConditionTableLayoutPanel.ColumnCount = 2;
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.SearchConditionTableLayoutPanel.Controls.Add(this.SearchConditionNameTextBox, 0, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label17, 0, 0);
            this.SearchConditionTableLayoutPanel.Location = new System.Drawing.Point(3, 36);
            this.SearchConditionTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchConditionTableLayoutPanel.Name = "SearchConditionTableLayoutPanel";
            this.SearchConditionTableLayoutPanel.RowCount = 1;
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SearchConditionTableLayoutPanel.Size = new System.Drawing.Size(524, 32);
            this.SearchConditionTableLayoutPanel.TabIndex = 1020;
            // 
            // SearchConditionNameTextBox
            // 
            this.SearchConditionNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchConditionNameTextBox.Location = new System.Drawing.Point(135, 4);
            this.SearchConditionNameTextBox.MaxLength = 30;
            this.SearchConditionNameTextBox.Name = "SearchConditionNameTextBox";
            this.SearchConditionNameTextBox.Size = new System.Drawing.Size(385, 22);
            this.SearchConditionNameTextBox.TabIndex = 1;
            this.SearchConditionNameTextBox.Tag = "Required;Regex(^(?!.*\\*).+$);Wide(30);ItemName(検索条件名)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Aquamarine;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(1, 1);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(130, 30);
            this.label17.TabIndex = 47;
            this.label17.Text = "検索条件名";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ControlSheetSaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(594, 401);
            this.Controls.Add(this.OkButton);
            this.Name = "ControlSheetSaveForm";
            this.Text = "タイトルが設定されていません - 開発計画表システム";
            this.Load += new System.EventHandler(this.ControlSheetSaveForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.OkButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HiddenDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayDataGridView)).EndInit();
            this.SearchConditionTableLayoutPanel.ResumeLayout(false);
            this.SearchConditionTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView HiddenDataGridView;
        private System.Windows.Forms.Button LeftButton;
        private System.Windows.Forms.Button RightButton;
        private System.Windows.Forms.DataGridView DisplayDataGridView;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn HiddenNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.TableLayoutPanel SearchConditionTableLayoutPanel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox SearchConditionNameTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
    }
}
