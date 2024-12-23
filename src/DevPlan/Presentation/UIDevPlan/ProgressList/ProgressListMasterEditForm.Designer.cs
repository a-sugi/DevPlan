namespace DevPlan.Presentation.UIDevPlan.ProgressList
{
    partial class ProgressListMasterEditForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.EntryButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.RowDeleteButton = new System.Windows.Forms.Button();
            this.RowAddButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PerformanceNameComboBox = new System.Windows.Forms.ComboBox();
            this.TypePanel = new System.Windows.Forms.Panel();
            this.EditModeSortNumRadioButton = new System.Windows.Forms.RadioButton();
            this.EditModeNoramlRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ProgressListMasterGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListDataLabel = new System.Windows.Forms.Label();
            this.NoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LargeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiddleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SmallColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.TypePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressListMasterGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(758, 527);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.ListDataLabel);
            this.ListFormMainPanel.Controls.Add(this.tableLayoutPanel1);
            this.ListFormMainPanel.Controls.Add(this.RowDeleteButton);
            this.ListFormMainPanel.Controls.Add(this.RowAddButton);
            this.ListFormMainPanel.Controls.Add(this.label2);
            this.ListFormMainPanel.Controls.Add(this.ProgressListMasterGridView);
            this.ListFormMainPanel.Size = new System.Drawing.Size(873, 517);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ProgressListMasterGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label2, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RowAddButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RowDeleteButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListDataLabel, 0);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 528);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 1012;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1011;
            this.label2.Text = "検索条件";
            // 
            // RowDeleteButton
            // 
            this.RowDeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowDeleteButton.Location = new System.Drawing.Point(131, 92);
            this.RowDeleteButton.Name = "RowDeleteButton";
            this.RowDeleteButton.Size = new System.Drawing.Size(120, 30);
            this.RowDeleteButton.TabIndex = 1013;
            this.RowDeleteButton.Text = "行削除";
            this.RowDeleteButton.UseVisualStyleBackColor = false;
            this.RowDeleteButton.Click += new System.EventHandler(this.RowDeleteButton_Click);
            // 
            // RowAddButton
            // 
            this.RowAddButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowAddButton.Location = new System.Drawing.Point(6, 92);
            this.RowAddButton.Name = "RowAddButton";
            this.RowAddButton.Size = new System.Drawing.Size(120, 30);
            this.RowAddButton.TabIndex = 1012;
            this.RowAddButton.Text = "行追加";
            this.RowAddButton.UseVisualStyleBackColor = false;
            this.RowAddButton.Click += new System.EventHandler(this.RowAddButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.PerformanceNameComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TypePanel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 54);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(570, 32);
            this.tableLayoutPanel1.TabIndex = 1014;
            // 
            // PerformanceNameComboBox
            // 
            this.PerformanceNameComboBox.DisplayMember = "性能名";
            this.PerformanceNameComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PerformanceNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PerformanceNameComboBox.FormattingEnabled = true;
            this.PerformanceNameComboBox.Location = new System.Drawing.Point(85, 4);
            this.PerformanceNameComboBox.MaxLength = 20;
            this.PerformanceNameComboBox.Name = "PerformanceNameComboBox";
            this.PerformanceNameComboBox.Size = new System.Drawing.Size(194, 23);
            this.PerformanceNameComboBox.TabIndex = 1019;
            this.PerformanceNameComboBox.Tag = "";
            this.PerformanceNameComboBox.ValueMember = "ID";
            this.PerformanceNameComboBox.SelectedValueChanged += new System.EventHandler(this.PerformanceNameComboBox_SelectedValueChanged);
            // 
            // TypePanel
            // 
            this.TypePanel.Controls.Add(this.EditModeSortNumRadioButton);
            this.TypePanel.Controls.Add(this.EditModeNoramlRadioButton);
            this.TypePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TypePanel.Location = new System.Drawing.Point(364, 1);
            this.TypePanel.Margin = new System.Windows.Forms.Padding(0);
            this.TypePanel.Name = "TypePanel";
            this.TypePanel.Size = new System.Drawing.Size(200, 30);
            this.TypePanel.TabIndex = 1018;
            this.TypePanel.Tag = "Required";
            // 
            // EditModeSortNumRadioButton
            // 
            this.EditModeSortNumRadioButton.AutoSize = true;
            this.EditModeSortNumRadioButton.Location = new System.Drawing.Point(99, 7);
            this.EditModeSortNumRadioButton.Name = "EditModeSortNumRadioButton";
            this.EditModeSortNumRadioButton.Size = new System.Drawing.Size(100, 19);
            this.EditModeSortNumRadioButton.TabIndex = 2;
            this.EditModeSortNumRadioButton.Tag = "2";
            this.EditModeSortNumRadioButton.Text = "表示順変更";
            this.EditModeSortNumRadioButton.UseVisualStyleBackColor = true;
            this.EditModeSortNumRadioButton.CheckedChanged += new System.EventHandler(this.EditModeRadioButton_CheckedChanged);
            // 
            // EditModeNoramlRadioButton
            // 
            this.EditModeNoramlRadioButton.AutoSize = true;
            this.EditModeNoramlRadioButton.Checked = true;
            this.EditModeNoramlRadioButton.Location = new System.Drawing.Point(4, 6);
            this.EditModeNoramlRadioButton.Name = "EditModeNoramlRadioButton";
            this.EditModeNoramlRadioButton.Size = new System.Drawing.Size(89, 19);
            this.EditModeNoramlRadioButton.TabIndex = 1;
            this.EditModeNoramlRadioButton.TabStop = true;
            this.EditModeNoramlRadioButton.Tag = "1";
            this.EditModeNoramlRadioButton.Text = "データ編集";
            this.EditModeNoramlRadioButton.UseVisualStyleBackColor = true;
            this.EditModeNoramlRadioButton.CheckedChanged += new System.EventHandler(this.EditModeRadioButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(283, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 30);
            this.label1.TabIndex = 1017;
            this.label1.Text = "編集モード";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 30);
            this.label3.TabIndex = 1016;
            this.label3.Text = "性能名";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProgressListMasterGridView
            // 
            this.ProgressListMasterGridView.AllowUserToAddRows = false;
            this.ProgressListMasterGridView.AllowUserToDeleteRows = false;
            this.ProgressListMasterGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressListMasterGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProgressListMasterGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ProgressListMasterGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProgressListMasterGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoColumn,
            this.LargeColumn,
            this.MiddleColumn,
            this.SmallColumn,
            this.GoalColumn,
            this.Column1,
            this.Column2,
            this.IdColumn});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProgressListMasterGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.ProgressListMasterGridView.Location = new System.Drawing.Point(3, 128);
            this.ProgressListMasterGridView.MultiSelect = false;
            this.ProgressListMasterGridView.Name = "ProgressListMasterGridView";
            this.ProgressListMasterGridView.RowHeadersVisible = false;
            this.ProgressListMasterGridView.RowTemplate.Height = 21;
            this.ProgressListMasterGridView.Size = new System.Drawing.Size(865, 384);
            this.ProgressListMasterGridView.TabIndex = 1015;
            this.ProgressListMasterGridView.Tag = "Required;ItemName(目標進度リストマスタ)";
            this.ProgressListMasterGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.ProgressListMasterGridView_CellPainting);
            this.ProgressListMasterGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProgressListMasterGridView_CellValueChanged);
            this.ProgressListMasterGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ProgressListMasterGridView_DragDrop);
            this.ProgressListMasterGridView.DragOver += new System.Windows.Forms.DragEventHandler(this.ProgressListMasterGridView_DragOver);
            this.ProgressListMasterGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProgressListMasterGridView_KeyDown);
            this.ProgressListMasterGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProgressListMasterGridView_MouseDown);
            this.ProgressListMasterGridView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProgressListMasterGridView_MouseMove);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SORT_NO";
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "No.";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "大項目";
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "大項目";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 100;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "中項目";
            this.dataGridViewTextBoxColumn3.Frozen = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "中項目";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 100;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "小項目";
            this.dataGridViewTextBoxColumn4.Frozen = true;
            this.dataGridViewTextBoxColumn4.HeaderText = "小項目";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 100;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "目標値";
            this.dataGridViewTextBoxColumn5.Frozen = true;
            this.dataGridViewTextBoxColumn5.HeaderText = "目標値";
            this.dataGridViewTextBoxColumn5.MaxInputLength = 200;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "編集日時";
            this.dataGridViewTextBoxColumn6.Frozen = true;
            this.dataGridViewTextBoxColumn6.HeaderText = "編集日時";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "編集者";
            this.dataGridViewTextBoxColumn7.Frozen = true;
            this.dataGridViewTextBoxColumn7.HeaderText = "編集者";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn8.HeaderText = "Id";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // ListDataLabel
            // 
            this.ListDataLabel.AutoSize = true;
            this.ListDataLabel.ForeColor = System.Drawing.Color.Red;
            this.ListDataLabel.Location = new System.Drawing.Point(258, 100);
            this.ListDataLabel.Name = "ListDataLabel";
            this.ListDataLabel.Size = new System.Drawing.Size(0, 15);
            this.ListDataLabel.TabIndex = 1022;
            // 
            // NoColumn
            // 
            this.NoColumn.DataPropertyName = "NO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NoColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.NoColumn.FillWeight = 40F;
            this.NoColumn.HeaderText = "No.";
            this.NoColumn.MinimumWidth = 40;
            this.NoColumn.Name = "NoColumn";
            this.NoColumn.ReadOnly = true;
            this.NoColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NoColumn.Width = 40;
            // 
            // LargeColumn
            // 
            this.LargeColumn.DataPropertyName = "大項目";
            this.LargeColumn.FillWeight = 120F;
            this.LargeColumn.HeaderText = "大項目";
            this.LargeColumn.MaxInputLength = 50;
            this.LargeColumn.MinimumWidth = 120;
            this.LargeColumn.Name = "LargeColumn";
            this.LargeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LargeColumn.Width = 120;
            // 
            // MiddleColumn
            // 
            this.MiddleColumn.DataPropertyName = "中項目";
            this.MiddleColumn.FillWeight = 120F;
            this.MiddleColumn.HeaderText = "中項目";
            this.MiddleColumn.MaxInputLength = 50;
            this.MiddleColumn.MinimumWidth = 120;
            this.MiddleColumn.Name = "MiddleColumn";
            this.MiddleColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MiddleColumn.Width = 120;
            // 
            // SmallColumn
            // 
            this.SmallColumn.DataPropertyName = "小項目";
            this.SmallColumn.FillWeight = 120F;
            this.SmallColumn.HeaderText = "小項目";
            this.SmallColumn.MaxInputLength = 50;
            this.SmallColumn.MinimumWidth = 120;
            this.SmallColumn.Name = "SmallColumn";
            this.SmallColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SmallColumn.Width = 120;
            // 
            // GoalColumn
            // 
            this.GoalColumn.DataPropertyName = "目標値";
            this.GoalColumn.FillWeight = 120F;
            this.GoalColumn.HeaderText = "目標値";
            this.GoalColumn.MaxInputLength = 200;
            this.GoalColumn.MinimumWidth = 120;
            this.GoalColumn.Name = "GoalColumn";
            this.GoalColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GoalColumn.Width = 120;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "編集日時";
            dataGridViewCellStyle3.Format = "yyyy/MM/dd HH:mm:ss";
            dataGridViewCellStyle3.NullValue = null;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.FillWeight = 150F;
            this.Column1.HeaderText = "編集日時";
            this.Column1.MinimumWidth = 150;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "編集者_NAME";
            this.Column2.FillWeight = 170F;
            this.Column2.HeaderText = "編集者";
            this.Column2.MinimumWidth = 170;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 170;
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
            // ProgressListMasterEditForm
            // 
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.EntryButton);
            this.Name = "ProgressListMasterEditForm";
            this.Text = "タイトルが設定されていません - 開発計画表システム";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressListMasterEditForm_FormClosing);
            this.Load += new System.EventHandler(this.ProgressListMasterEditForm_Load);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.TypePanel.ResumeLayout(false);
            this.TypePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressListMasterGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RowDeleteButton;
        private System.Windows.Forms.Button RowAddButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel TypePanel;
        private System.Windows.Forms.RadioButton EditModeSortNumRadioButton;
        private System.Windows.Forms.RadioButton EditModeNoramlRadioButton;
        private System.Windows.Forms.ComboBox PerformanceNameComboBox;
        private System.Windows.Forms.DataGridView ProgressListMasterGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Label ListDataLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LargeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiddleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SmallColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdColumn;
    }
}
