namespace DevPlan.Presentation.UIDevPlan.FunctionAuthority
{
    partial class FunctionAuthorityRollSettingForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CompletionScheduleContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DayInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DayCompletionInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SituationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MainPanel = new System.Windows.Forms.Panel();
            this.RollSettingDataGridView = new System.Windows.Forms.DataGridView();
            this.RollIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReadFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UpdateFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ExportFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ManagementFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PrintScreenFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CarshareOfficeFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AllGeneralCodeFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SksFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OwnPartUpdateFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OwnPartExportFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OwnPartManagementFlgColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SearchConditionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.RollComboBox = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.EntryButton = new System.Windows.Forms.Button();
            this.RollAddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.ContentsPanel.SuspendLayout();
            this.CompletionScheduleContextMenuStrip.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RollSettingDataGridView)).BeginInit();
            this.SearchConditionTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.Location = new System.Drawing.Point(852, 585);
            this.RightButton.TabIndex = 1009;
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.ClearButton);
            this.ContentsPanel.Controls.Add(this.RollAddButton);
            this.ContentsPanel.Controls.Add(this.SearchConditionTableLayoutPanel);
            this.ContentsPanel.Controls.Add(this.MainPanel);
            this.ContentsPanel.Size = new System.Drawing.Size(984, 554);
            this.ContentsPanel.TabIndex = 0;
            // 
            // CompletionScheduleContextMenuStrip
            // 
            this.CompletionScheduleContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DayInputToolStripMenuItem,
            this.DayCompletionInputToolStripMenuItem});
            this.CompletionScheduleContextMenuStrip.Name = "CompletionScheduleContextMenuStrip";
            this.CompletionScheduleContextMenuStrip.Size = new System.Drawing.Size(167, 48);
            // 
            // DayInputToolStripMenuItem
            // 
            this.DayInputToolStripMenuItem.Name = "DayInputToolStripMenuItem";
            this.DayInputToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DayInputToolStripMenuItem.Text = "日付入力";
            // 
            // DayCompletionInputToolStripMenuItem
            // 
            this.DayCompletionInputToolStripMenuItem.Name = "DayCompletionInputToolStripMenuItem";
            this.DayCompletionInputToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DayCompletionInputToolStripMenuItem.Text = "日付付き済み入力";
            // 
            // SituationToolTip
            // 
            this.SituationToolTip.AutoPopDelay = 2147483647;
            this.SituationToolTip.InitialDelay = 0;
            this.SituationToolTip.ReshowDelay = 0;
            this.SituationToolTip.ShowAlways = true;
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.RollSettingDataGridView);
            this.MainPanel.Location = new System.Drawing.Point(0, 45);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(984, 506);
            this.MainPanel.TabIndex = 20;
            // 
            // RollSettingDataGridView
            // 
            this.RollSettingDataGridView.AllowUserToAddRows = false;
            this.RollSettingDataGridView.AllowUserToDeleteRows = false;
            this.RollSettingDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RollSettingDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.RollSettingDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RollSettingDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.RollSettingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RollSettingDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RollIdColumn,
            this.FunctionIdColumn,
            this.FunctionNameColumn,
            this.ReadFlgColumn,
            this.UpdateFlgColumn,
            this.ExportFlgColumn,
            this.ManagementFlgColumn,
            this.PrintScreenFlgColumn,
            this.CarshareOfficeFlgColumn,
            this.AllGeneralCodeFlgColumn,
            this.SksFlgColumn,
            this.OwnPartUpdateFlgColumn,
            this.OwnPartExportFlgColumn,
            this.OwnPartManagementFlgColumn});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RollSettingDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.RollSettingDataGridView.Location = new System.Drawing.Point(10, 4);
            this.RollSettingDataGridView.MultiSelect = false;
            this.RollSettingDataGridView.Name = "RollSettingDataGridView";
            this.RollSettingDataGridView.RowHeadersVisible = false;
            this.RollSettingDataGridView.RowTemplate.Height = 21;
            this.RollSettingDataGridView.ShowCellToolTips = false;
            this.RollSettingDataGridView.Size = new System.Drawing.Size(962, 499);
            this.RollSettingDataGridView.TabIndex = 100;
            this.RollSettingDataGridView.Tag = "";
            this.RollSettingDataGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RollSettingDataGridView_CellMouseEnter);
            this.RollSettingDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.RollSettingDataGridView_CellValueChanged);
            this.RollSettingDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.RollSettingDataGridView_CurrentCellDirtyStateChanged);
            // 
            // RollIdColumn
            // 
            this.RollIdColumn.HeaderText = "ロールID";
            this.RollIdColumn.Name = "RollIdColumn";
            this.RollIdColumn.Visible = false;
            this.RollIdColumn.Width = 62;
            // 
            // FunctionIdColumn
            // 
            this.FunctionIdColumn.DataPropertyName = "FUNCTION_ID";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FunctionIdColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.FunctionIdColumn.HeaderText = "機能ID";
            this.FunctionIdColumn.MinimumWidth = 100;
            this.FunctionIdColumn.Name = "FunctionIdColumn";
            this.FunctionIdColumn.ReadOnly = true;
            this.FunctionIdColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FunctionIdColumn.Visible = false;
            // 
            // FunctionNameColumn
            // 
            this.FunctionNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FunctionNameColumn.DataPropertyName = "FUNCTION_NAME";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FunctionNameColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.FunctionNameColumn.HeaderText = "機能";
            this.FunctionNameColumn.MinimumWidth = 120;
            this.FunctionNameColumn.Name = "FunctionNameColumn";
            this.FunctionNameColumn.ReadOnly = true;
            this.FunctionNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FunctionNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ReadFlgColumn
            // 
            this.ReadFlgColumn.DataPropertyName = "READ_FLG";
            this.ReadFlgColumn.FalseValue = "0";
            this.ReadFlgColumn.HeaderText = "参照";
            this.ReadFlgColumn.IndeterminateValue = "0";
            this.ReadFlgColumn.MinimumWidth = 76;
            this.ReadFlgColumn.Name = "ReadFlgColumn";
            this.ReadFlgColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReadFlgColumn.TrueValue = "1";
            this.ReadFlgColumn.Width = 76;
            // 
            // UpdateFlgColumn
            // 
            this.UpdateFlgColumn.DataPropertyName = "UPDATE_FLG";
            this.UpdateFlgColumn.FalseValue = "0";
            this.UpdateFlgColumn.HeaderText = "更新";
            this.UpdateFlgColumn.IndeterminateValue = "0";
            this.UpdateFlgColumn.MinimumWidth = 76;
            this.UpdateFlgColumn.Name = "UpdateFlgColumn";
            this.UpdateFlgColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UpdateFlgColumn.TrueValue = "1";
            this.UpdateFlgColumn.Width = 76;
            // 
            // ExportFlgColumn
            // 
            this.ExportFlgColumn.DataPropertyName = "EXPORT_FLG";
            this.ExportFlgColumn.FalseValue = "0";
            this.ExportFlgColumn.HeaderText = "出力";
            this.ExportFlgColumn.IndeterminateValue = "0";
            this.ExportFlgColumn.MinimumWidth = 76;
            this.ExportFlgColumn.Name = "ExportFlgColumn";
            this.ExportFlgColumn.TrueValue = "1";
            this.ExportFlgColumn.Width = 76;
            // 
            // ManagementFlgColumn
            // 
            this.ManagementFlgColumn.DataPropertyName = "MANAGEMENT_FLG";
            this.ManagementFlgColumn.FalseValue = "0";
            this.ManagementFlgColumn.HeaderText = "管理";
            this.ManagementFlgColumn.IndeterminateValue = "0";
            this.ManagementFlgColumn.MinimumWidth = 76;
            this.ManagementFlgColumn.Name = "ManagementFlgColumn";
            this.ManagementFlgColumn.TrueValue = "1";
            this.ManagementFlgColumn.Width = 76;
            // 
            // PrintScreenFlgColumn
            // 
            this.PrintScreenFlgColumn.DataPropertyName = "PRINTSCREEN_FLG";
            this.PrintScreenFlgColumn.FalseValue = "0";
            this.PrintScreenFlgColumn.HeaderText = "PrtScr";
            this.PrintScreenFlgColumn.IndeterminateValue = "0";
            this.PrintScreenFlgColumn.MinimumWidth = 76;
            this.PrintScreenFlgColumn.Name = "PrintScreenFlgColumn";
            this.PrintScreenFlgColumn.TrueValue = "1";
            this.PrintScreenFlgColumn.Width = 76;
            // 
            // CarshareOfficeFlgColumn
            // 
            this.CarshareOfficeFlgColumn.DataPropertyName = "CARSHARE_OFFICE_FLG";
            this.CarshareOfficeFlgColumn.FalseValue = "0";
            this.CarshareOfficeFlgColumn.HeaderText = "ｶｰｼｪｱ\\n事務所";
            this.CarshareOfficeFlgColumn.IndeterminateValue = "0";
            this.CarshareOfficeFlgColumn.MinimumWidth = 76;
            this.CarshareOfficeFlgColumn.Name = "CarshareOfficeFlgColumn";
            this.CarshareOfficeFlgColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CarshareOfficeFlgColumn.TrueValue = "1";
            this.CarshareOfficeFlgColumn.Width = 117;
            // 
            // AllGeneralCodeFlgColumn
            // 
            this.AllGeneralCodeFlgColumn.DataPropertyName = "ALL_GENERAL_CODE_FLG";
            this.AllGeneralCodeFlgColumn.FalseValue = "0";
            this.AllGeneralCodeFlgColumn.HeaderText = "全閲覧";
            this.AllGeneralCodeFlgColumn.IndeterminateValue = "0";
            this.AllGeneralCodeFlgColumn.MinimumWidth = 76;
            this.AllGeneralCodeFlgColumn.Name = "AllGeneralCodeFlgColumn";
            this.AllGeneralCodeFlgColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AllGeneralCodeFlgColumn.TrueValue = "1";
            this.AllGeneralCodeFlgColumn.Width = 76;
            // 
            // SksFlgColumn
            // 
            this.SksFlgColumn.DataPropertyName = "SKS_FLG";
            this.SksFlgColumn.FalseValue = "0";
            this.SksFlgColumn.HeaderText = "SKS";
            this.SksFlgColumn.IndeterminateValue = "0";
            this.SksFlgColumn.MinimumWidth = 76;
            this.SksFlgColumn.Name = "SksFlgColumn";
            this.SksFlgColumn.TrueValue = "1";
            this.SksFlgColumn.Width = 76;
            // 
            // OwnPartUpdateFlgColumn
            // 
            this.OwnPartUpdateFlgColumn.DataPropertyName = "JIBU_UPDATE_FLG";
            this.OwnPartUpdateFlgColumn.FalseValue = "0";
            this.OwnPartUpdateFlgColumn.HeaderText = "自部\\n編集";
            this.OwnPartUpdateFlgColumn.IndeterminateValue = "0";
            this.OwnPartUpdateFlgColumn.MinimumWidth = 76;
            this.OwnPartUpdateFlgColumn.Name = "OwnPartUpdateFlgColumn";
            this.OwnPartUpdateFlgColumn.TrueValue = "1";
            this.OwnPartUpdateFlgColumn.Visible = false;
            this.OwnPartUpdateFlgColumn.Width = 89;
            // 
            // OwnPartExportFlgColumn
            // 
            this.OwnPartExportFlgColumn.DataPropertyName = "JIBU_EXPORT_FLG";
            this.OwnPartExportFlgColumn.FalseValue = "0";
            this.OwnPartExportFlgColumn.HeaderText = "自部\\n出力";
            this.OwnPartExportFlgColumn.IndeterminateValue = "0";
            this.OwnPartExportFlgColumn.MinimumWidth = 76;
            this.OwnPartExportFlgColumn.Name = "OwnPartExportFlgColumn";
            this.OwnPartExportFlgColumn.TrueValue = "1";
            this.OwnPartExportFlgColumn.Width = 89;
            // 
            // OwnPartManagementFlgColumn
            // 
            this.OwnPartManagementFlgColumn.DataPropertyName = "JIBU_MANAGEMENT_FLG";
            this.OwnPartManagementFlgColumn.FalseValue = "0";
            this.OwnPartManagementFlgColumn.HeaderText = "自部\\n管理";
            this.OwnPartManagementFlgColumn.IndeterminateValue = "0";
            this.OwnPartManagementFlgColumn.MinimumWidth = 76;
            this.OwnPartManagementFlgColumn.Name = "OwnPartManagementFlgColumn";
            this.OwnPartManagementFlgColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OwnPartManagementFlgColumn.TrueValue = "1";
            this.OwnPartManagementFlgColumn.Width = 89;
            // 
            // SearchConditionTableLayoutPanel
            // 
            this.SearchConditionTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SearchConditionTableLayoutPanel.ColumnCount = 2;
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 518F));
            this.SearchConditionTableLayoutPanel.Controls.Add(this.RollComboBox, 1, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label17, 0, 0);
            this.SearchConditionTableLayoutPanel.Location = new System.Drawing.Point(12, 10);
            this.SearchConditionTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchConditionTableLayoutPanel.Name = "SearchConditionTableLayoutPanel";
            this.SearchConditionTableLayoutPanel.RowCount = 1;
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.SearchConditionTableLayoutPanel.Size = new System.Drawing.Size(533, 32);
            this.SearchConditionTableLayoutPanel.TabIndex = 10;
            // 
            // RollComboBox
            // 
            this.RollComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RollComboBox.DisplayMember = "ROLL_NAME";
            this.RollComboBox.FormattingEnabled = true;
            this.RollComboBox.Location = new System.Drawing.Point(125, 4);
            this.RollComboBox.MaxLength = 50;
            this.RollComboBox.Name = "RollComboBox";
            this.RollComboBox.Size = new System.Drawing.Size(404, 23);
            this.RollComboBox.TabIndex = 4;
            this.RollComboBox.Tag = "Required;ItemName(ロール名)";
            this.RollComboBox.ValueMember = "ROLL_ID";
            this.RollComboBox.SelectedIndexChanged += new System.EventHandler(this.RollComboBox_SelectedIndexChanged);
            this.RollComboBox.TextChanged += new System.EventHandler(this.RollComboBox_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(1, 1);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.MaximumSize = new System.Drawing.Size(120, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 30);
            this.label17.TabIndex = 0;
            this.label17.Text = "ロール名";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.EntryButton.Location = new System.Drawing.Point(10, 585);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 20);
            this.EntryButton.TabIndex = 1005;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // RollAddButton
            // 
            this.RollAddButton.BackColor = System.Drawing.SystemColors.Control;
            this.RollAddButton.Location = new System.Drawing.Point(575, 11);
            this.RollAddButton.Name = "RollAddButton";
            this.RollAddButton.Size = new System.Drawing.Size(120, 30);
            this.RollAddButton.TabIndex = 5;
            this.RollAddButton.Text = "ﾛｰﾙ新規登録";
            this.RollAddButton.UseVisualStyleBackColor = false;
            this.RollAddButton.Click += new System.EventHandler(this.RollAddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.DeleteButton.Location = new System.Drawing.Point(137, 585);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(120, 20);
            this.DeleteButton.TabIndex = 1006;
            this.DeleteButton.Text = "削除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.Control;
            this.ClearButton.Location = new System.Drawing.Point(701, 11);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(120, 30);
            this.ClearButton.TabIndex = 6;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // FunctionAuthorityRollSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(984, 631);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EntryButton);
            this.Name = "FunctionAuthorityRollSettingForm";
            this.Text = "FunctionAuthorityRollSetting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FunctionAuthorityRollSettingForm_FormClosing);
            this.Load += new System.EventHandler(this.FunctionAuthorityRollSettingForm_Load);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.DeleteButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.CompletionScheduleContextMenuStrip.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RollSettingDataGridView)).EndInit();
            this.SearchConditionTableLayoutPanel.ResumeLayout(false);
            this.SearchConditionTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip CompletionScheduleContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DayInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DayCompletionInputToolStripMenuItem;
        private System.Windows.Forms.ToolTip SituationToolTip;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.DataGridView RollSettingDataGridView;
        private System.Windows.Forms.TableLayoutPanel SearchConditionTableLayoutPanel;
        private System.Windows.Forms.ComboBox RollComboBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Button RollAddButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn RollIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionNameColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ReadFlgColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UpdateFlgColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ExportFlgColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ManagementFlgColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PrintScreenFlgColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CarshareOfficeFlgColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AllGeneralCodeFlgColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SksFlgColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OwnPartUpdateFlgColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OwnPartExportFlgColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OwnPartManagementFlgColumn;
    }
}