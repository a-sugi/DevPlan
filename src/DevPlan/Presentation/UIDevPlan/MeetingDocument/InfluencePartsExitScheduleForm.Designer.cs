namespace DevPlan.Presentation.UIDevPlan.MeetingDocument
{
    partial class InfluencePartsExitScheduleForm
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
            this.RowAddButton = new System.Windows.Forms.Button();
            this.RowDeleteButton = new System.Windows.Forms.Button();
            this.EntryButton = new System.Windows.Forms.Button();
            this.AffectedPartsDrawingScheduleDataGridView = new System.Windows.Forms.DataGridView();
            this.NoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DesignDepartmentColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.AffectedPartsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawingScheduleColumn = new DevPlan.Presentation.UC.DataGridViewCalendarColumn();
            this.CostColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MassColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvestmentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.InvestmentLabel = new System.Windows.Forms.Label();
            this.MassLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CostLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AffectedPartsDrawingScheduleDataGridView)).BeginInit();
            this.SumTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(718, 367);
            this.CloseButton.TabIndex = 10;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.SumTableLayoutPanel);
            this.ListFormMainPanel.Controls.Add(this.AffectedPartsDrawingScheduleDataGridView);
            this.ListFormMainPanel.Controls.Add(this.RowDeleteButton);
            this.ListFormMainPanel.Controls.Add(this.RowAddButton);
            this.ListFormMainPanel.Size = new System.Drawing.Size(833, 357);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RowAddButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RowDeleteButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.AffectedPartsDrawingScheduleDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SumTableLayoutPanel, 0);
            // 
            // RowAddButton
            // 
            this.RowAddButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowAddButton.Location = new System.Drawing.Point(3, 39);
            this.RowAddButton.Name = "RowAddButton";
            this.RowAddButton.Size = new System.Drawing.Size(120, 30);
            this.RowAddButton.TabIndex = 5;
            this.RowAddButton.Text = "行追加";
            this.RowAddButton.UseVisualStyleBackColor = false;
            this.RowAddButton.Click += new System.EventHandler(this.RowAddButton_Click);
            // 
            // RowDeleteButton
            // 
            this.RowDeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowDeleteButton.Location = new System.Drawing.Point(129, 39);
            this.RowDeleteButton.Name = "RowDeleteButton";
            this.RowDeleteButton.Size = new System.Drawing.Size(120, 30);
            this.RowDeleteButton.TabIndex = 6;
            this.RowDeleteButton.Text = "行削除";
            this.RowDeleteButton.UseVisualStyleBackColor = false;
            this.RowDeleteButton.Click += new System.EventHandler(this.RowDeleteButton_Click);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 368);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 8;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // AffectedPartsDrawingScheduleDataGridView
            // 
            this.AffectedPartsDrawingScheduleDataGridView.AllowUserToAddRows = false;
            this.AffectedPartsDrawingScheduleDataGridView.AllowUserToDeleteRows = false;
            this.AffectedPartsDrawingScheduleDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AffectedPartsDrawingScheduleDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AffectedPartsDrawingScheduleDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.AffectedPartsDrawingScheduleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AffectedPartsDrawingScheduleDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoColumn,
            this.DesignDepartmentColumn,
            this.AffectedPartsColumn,
            this.DrawingScheduleColumn,
            this.CostColumn,
            this.MassColumn,
            this.InvestmentColumn});
            this.AffectedPartsDrawingScheduleDataGridView.Location = new System.Drawing.Point(3, 75);
            this.AffectedPartsDrawingScheduleDataGridView.MultiSelect = false;
            this.AffectedPartsDrawingScheduleDataGridView.Name = "AffectedPartsDrawingScheduleDataGridView";
            this.AffectedPartsDrawingScheduleDataGridView.RowHeadersVisible = false;
            this.AffectedPartsDrawingScheduleDataGridView.RowTemplate.Height = 21;
            this.AffectedPartsDrawingScheduleDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AffectedPartsDrawingScheduleDataGridView.Size = new System.Drawing.Size(825, 243);
            this.AffectedPartsDrawingScheduleDataGridView.TabIndex = 7;
            this.AffectedPartsDrawingScheduleDataGridView.Tag = "Required;ItemName(進捗状況)";
            this.AffectedPartsDrawingScheduleDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.AffectedPartsDrawingScheduleDataGridView_CellValueChanged);
            this.AffectedPartsDrawingScheduleDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.AffectedPartsDrawingScheduleDataGridView_DataError);
            this.AffectedPartsDrawingScheduleDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.AffectedPartsDrawingScheduleDataGridView_RowsAdded);
            this.AffectedPartsDrawingScheduleDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.AffectedPartsDrawingScheduleDataGridView_RowsRemoved);
            // 
            // NoColumn
            // 
            this.NoColumn.FillWeight = 40F;
            this.NoColumn.HeaderText = "No.";
            this.NoColumn.MinimumWidth = 40;
            this.NoColumn.Name = "NoColumn";
            this.NoColumn.ReadOnly = true;
            this.NoColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NoColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NoColumn.Width = 40;
            // 
            // DesignDepartmentColumn
            // 
            this.DesignDepartmentColumn.DisplayStyleForCurrentCellOnly = true;
            this.DesignDepartmentColumn.HeaderText = "設計部署\\n(部)";
            this.DesignDepartmentColumn.MinimumWidth = 100;
            this.DesignDepartmentColumn.Name = "DesignDepartmentColumn";
            this.DesignDepartmentColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // AffectedPartsColumn
            // 
            this.AffectedPartsColumn.FillWeight = 170F;
            this.AffectedPartsColumn.HeaderText = "影響する(しそうな)部品";
            this.AffectedPartsColumn.MaxInputLength = 250;
            this.AffectedPartsColumn.MinimumWidth = 170;
            this.AffectedPartsColumn.Name = "AffectedPartsColumn";
            this.AffectedPartsColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AffectedPartsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AffectedPartsColumn.Width = 170;
            // 
            // DrawingScheduleColumn
            // 
            this.DrawingScheduleColumn.HeaderText = "出図日程";
            this.DrawingScheduleColumn.MinimumWidth = 100;
            this.DrawingScheduleColumn.Name = "DrawingScheduleColumn";
            this.DrawingScheduleColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // CostColumn
            // 
            this.CostColumn.FillWeight = 130F;
            this.CostColumn.HeaderText = "コスト変動\\n見通し(円)";
            this.CostColumn.MaxInputLength = 75;
            this.CostColumn.MinimumWidth = 130;
            this.CostColumn.Name = "CostColumn";
            this.CostColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CostColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CostColumn.Width = 130;
            // 
            // MassColumn
            // 
            this.MassColumn.FillWeight = 130F;
            this.MassColumn.HeaderText = "質量変動\\n見通し(g)";
            this.MassColumn.MaxInputLength = 75;
            this.MassColumn.MinimumWidth = 130;
            this.MassColumn.Name = "MassColumn";
            this.MassColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MassColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MassColumn.Width = 130;
            // 
            // InvestmentColumn
            // 
            this.InvestmentColumn.FillWeight = 130F;
            this.InvestmentColumn.HeaderText = "投資変動\\n見通し(百万円)";
            this.InvestmentColumn.MaxInputLength = 75;
            this.InvestmentColumn.MinimumWidth = 130;
            this.InvestmentColumn.Name = "InvestmentColumn";
            this.InvestmentColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InvestmentColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InvestmentColumn.Width = 130;
            // 
            // SumTableLayoutPanel
            // 
            this.SumTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SumTableLayoutPanel.ColumnCount = 4;
            this.SumTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.SumTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.SumTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.SumTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.SumTableLayoutPanel.Controls.Add(this.InvestmentLabel, 3, 0);
            this.SumTableLayoutPanel.Controls.Add(this.MassLabel, 2, 0);
            this.SumTableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.SumTableLayoutPanel.Controls.Add(this.CostLabel, 1, 0);
            this.SumTableLayoutPanel.Location = new System.Drawing.Point(314, 320);
            this.SumTableLayoutPanel.Name = "SumTableLayoutPanel";
            this.SumTableLayoutPanel.RowCount = 1;
            this.SumTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SumTableLayoutPanel.Size = new System.Drawing.Size(491, 32);
            this.SumTableLayoutPanel.TabIndex = 1011;
            // 
            // InvestmentLabel
            // 
            this.InvestmentLabel.AutoSize = true;
            this.InvestmentLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvestmentLabel.Location = new System.Drawing.Point(364, 1);
            this.InvestmentLabel.Name = "InvestmentLabel";
            this.InvestmentLabel.Size = new System.Drawing.Size(123, 30);
            this.InvestmentLabel.TabIndex = 3;
            this.InvestmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MassLabel
            // 
            this.MassLabel.AutoSize = true;
            this.MassLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MassLabel.Location = new System.Drawing.Point(234, 1);
            this.MassLabel.Name = "MassLabel";
            this.MassLabel.Size = new System.Drawing.Size(123, 30);
            this.MassLabel.TabIndex = 2;
            this.MassLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "合計";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CostLabel
            // 
            this.CostLabel.AutoSize = true;
            this.CostLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CostLabel.Location = new System.Drawing.Point(104, 1);
            this.CostLabel.Name = "CostLabel";
            this.CostLabel.Size = new System.Drawing.Size(123, 30);
            this.CostLabel.TabIndex = 1;
            this.CostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // InfluencePartsExitScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 401);
            this.Controls.Add(this.EntryButton);
            this.MinimumSize = new System.Drawing.Size(860, 400);
            this.Name = "InfluencePartsExitScheduleForm";
            this.Text = "InfluencePartsExitScheduleForm";
            this.Load += new System.EventHandler(this.InfluencePartsExitScheduleForm_Load);
            this.Shown += new System.EventHandler(this.InfluencePartsExitScheduleForm_Shown);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AffectedPartsDrawingScheduleDataGridView)).EndInit();
            this.SumTableLayoutPanel.ResumeLayout(false);
            this.SumTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button RowAddButton;
        private System.Windows.Forms.Button RowDeleteButton;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.DataGridView AffectedPartsDrawingScheduleDataGridView;
        private System.Windows.Forms.TableLayoutPanel SumTableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label InvestmentLabel;
        private System.Windows.Forms.Label MassLabel;
        private System.Windows.Forms.Label CostLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn DesignDepartmentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AffectedPartsColumn;
        private UC.DataGridViewCalendarColumn DrawingScheduleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MassColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvestmentColumn;
    }
}