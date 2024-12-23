namespace DevPlan.Presentation.UIDevPlan.FunctionAuthority
{
    partial class FunctionAuthorityRollPersonelForm
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
            this.RegistButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PersonelIdLabel = new System.Windows.Forms.Label();
            this.PersonelNameLabel = new System.Windows.Forms.Label();
            this.RollDataGridView = new System.Windows.Forms.DataGridView();
            this.DataCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RollIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RollNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllSelectCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RollDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(418, 567);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.AllSelectCheckBox);
            this.ListFormMainPanel.Controls.Add(this.RollDataGridView);
            this.ListFormMainPanel.Controls.Add(this.tableLayoutPanel3);
            this.ListFormMainPanel.Size = new System.Drawing.Size(533, 557);
            this.ListFormMainPanel.Controls.SetChildIndex(this.tableLayoutPanel3, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RollDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.AllSelectCheckBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            // 
            // RegistButton
            // 
            this.RegistButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RegistButton.BackColor = System.Drawing.SystemColors.Control;
            this.RegistButton.Location = new System.Drawing.Point(5, 567);
            this.RegistButton.Name = "RegistButton";
            this.RegistButton.Size = new System.Drawing.Size(100, 30);
            this.RegistButton.TabIndex = 1010;
            this.RegistButton.Text = "登録";
            this.RegistButton.UseVisualStyleBackColor = false;
            this.RegistButton.Click += new System.EventHandler(this.RegistButton_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.46912F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.53088F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.PersonelIdLabel, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.PersonelNameLabel, 1, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 39);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(520, 65);
            this.tableLayoutPanel3.TabIndex = 1011;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "職番";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(1, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "氏名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PersonelIdLabel
            // 
            this.PersonelIdLabel.AutoSize = true;
            this.PersonelIdLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PersonelIdLabel.Location = new System.Drawing.Point(157, 1);
            this.PersonelIdLabel.Name = "PersonelIdLabel";
            this.PersonelIdLabel.Size = new System.Drawing.Size(359, 30);
            this.PersonelIdLabel.TabIndex = 7;
            this.PersonelIdLabel.Text = "ユーザーID";
            this.PersonelIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PersonelNameLabel
            // 
            this.PersonelNameLabel.AutoSize = true;
            this.PersonelNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PersonelNameLabel.Location = new System.Drawing.Point(157, 32);
            this.PersonelNameLabel.Name = "PersonelNameLabel";
            this.PersonelNameLabel.Size = new System.Drawing.Size(359, 32);
            this.PersonelNameLabel.TabIndex = 8;
            this.PersonelNameLabel.Text = "ユーザー名";
            this.PersonelNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RollDataGridView
            // 
            this.RollDataGridView.AllowUserToAddRows = false;
            this.RollDataGridView.AllowUserToDeleteRows = false;
            this.RollDataGridView.AllowUserToResizeColumns = false;
            this.RollDataGridView.AllowUserToResizeRows = false;
            this.RollDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.RollDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RollDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataCheckBoxColumn,
            this.RollIdColumn,
            this.RollNameColumn});
            this.RollDataGridView.Location = new System.Drawing.Point(6, 110);
            this.RollDataGridView.Name = "RollDataGridView";
            this.RollDataGridView.RowHeadersVisible = false;
            this.RollDataGridView.RowTemplate.Height = 21;
            this.RollDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RollDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.RollDataGridView.Size = new System.Drawing.Size(520, 442);
            this.RollDataGridView.TabIndex = 1013;
            // 
            // DataCheckBoxColumn
            // 
            this.DataCheckBoxColumn.FalseValue = "false";
            this.DataCheckBoxColumn.FillWeight = 45F;
            this.DataCheckBoxColumn.HeaderText = "";
            this.DataCheckBoxColumn.MinimumWidth = 45;
            this.DataCheckBoxColumn.Name = "DataCheckBoxColumn";
            this.DataCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DataCheckBoxColumn.TrueValue = "true";
            this.DataCheckBoxColumn.Width = 45;
            // 
            // RollIdColumn
            // 
            this.RollIdColumn.DataPropertyName = "ROLL_ID";
            this.RollIdColumn.HeaderText = "ロールID";
            this.RollIdColumn.Name = "RollIdColumn";
            this.RollIdColumn.Visible = false;
            // 
            // RollNameColumn
            // 
            this.RollNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RollNameColumn.DataPropertyName = "ROLL_NAME";
            this.RollNameColumn.FillWeight = 120F;
            this.RollNameColumn.HeaderText = "ロール";
            this.RollNameColumn.MaxInputLength = 20;
            this.RollNameColumn.Name = "RollNameColumn";
            this.RollNameColumn.ReadOnly = true;
            this.RollNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // AllSelectCheckBox
            // 
            this.AllSelectCheckBox.AutoSize = true;
            this.AllSelectCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.AllSelectCheckBox.Location = new System.Drawing.Point(23, 116);
            this.AllSelectCheckBox.Name = "AllSelectCheckBox";
            this.AllSelectCheckBox.Size = new System.Drawing.Size(15, 14);
            this.AllSelectCheckBox.TabIndex = 1014;
            this.AllSelectCheckBox.UseVisualStyleBackColor = false;
            this.AllSelectCheckBox.CheckedChanged += new System.EventHandler(this.AllSelectCheckBox_CheckedChanged);
            // 
            // FunctionAuthorityRollPersonelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(544, 601);
            this.Controls.Add(this.RegistButton);
            this.Name = "FunctionAuthorityRollPersonelForm";
            this.Text = "タイトルが設定されていません - 開発計画表システム";
            this.Load += new System.EventHandler(this.FunctionAuthorityRollPersonelForm_Load);
            this.Controls.SetChildIndex(this.RegistButton, 0);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RollDataGridView)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView RollDataGridView;
        private System.Windows.Forms.CheckBox AllSelectCheckBox;
        private System.Windows.Forms.Button RegistButton;
        private System.Windows.Forms.Label PersonelIdLabel;
        private System.Windows.Forms.Label PersonelNameLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DataCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RollIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RollNameColumn;
    }
}