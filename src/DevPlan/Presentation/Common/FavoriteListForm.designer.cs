namespace DevPlan.Presentation.Common
{
    partial class FavoriteListForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.FavoriteListDataGridView = new System.Windows.Forms.DataGridView();
            this.DeleteFlagCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FavoriteNameTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputDateTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FavoriteIDTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FavoriteClassIDTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FavoriteListUpdateButton = new System.Windows.Forms.Button();
            this.FavoriteListDeleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FavoriteListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(453, 188);
            this.CloseButton.TabIndex = 1;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.FavoriteListDataGridView);
            this.ListFormMainPanel.Size = new System.Drawing.Size(568, 175);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.FavoriteListDataGridView, 0);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 398);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // FavoriteListDataGridView
            // 
            this.FavoriteListDataGridView.AllowUserToAddRows = false;
            this.FavoriteListDataGridView.AllowUserToOrderColumns = false;
            this.FavoriteListDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.FavoriteListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FavoriteListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeleteFlagCheckBoxColumn,
            this.FavoriteNameTextBoxColumn,
            this.InputDateTextBoxColumn,
            this.FavoriteIDTextBoxColumn,
            this.FavoriteClassIDTextBoxColumn});
            this.FavoriteListDataGridView.Location = new System.Drawing.Point(3, 39);
            this.FavoriteListDataGridView.Name = "FavoriteListDataGridView";
            this.FavoriteListDataGridView.RowHeadersVisible = false;
            this.FavoriteListDataGridView.RowTemplate.Height = 21;
            this.FavoriteListDataGridView.Size = new System.Drawing.Size(560, 130);
            this.FavoriteListDataGridView.TabIndex = 1;
            // 
            // DeleteFlagCheckBoxColumn
            // 
            this.DeleteFlagCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DeleteFlagCheckBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.DeleteFlagCheckBoxColumn.FalseValue = "false";
            this.DeleteFlagCheckBoxColumn.FillWeight = 65F;
            this.DeleteFlagCheckBoxColumn.HeaderText = "削除";
            this.DeleteFlagCheckBoxColumn.IndeterminateValue = "false";
            this.DeleteFlagCheckBoxColumn.MinimumWidth = 65;
            this.DeleteFlagCheckBoxColumn.Name = "DeleteFlagCheckBoxColumn";
            this.DeleteFlagCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DeleteFlagCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DeleteFlagCheckBoxColumn.TrueValue = "true";
            this.DeleteFlagCheckBoxColumn.Width = 65;
            // 
            // FavoriteNameTextBoxColumn
            // 
            this.FavoriteNameTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FavoriteNameTextBoxColumn.DataPropertyName = "FAVORITE_NAME";
            this.FavoriteNameTextBoxColumn.FillWeight = 374F;
            this.FavoriteNameTextBoxColumn.HeaderText = "お気に入り名";
            this.FavoriteNameTextBoxColumn.MaxInputLength = 30;
            this.FavoriteNameTextBoxColumn.MinimumWidth = 274;
            this.FavoriteNameTextBoxColumn.Name = "FavoriteNameTextBoxColumn";
            // 
            // InputDateTextBoxColumn
            // 
            this.InputDateTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.InputDateTextBoxColumn.DataPropertyName = "INPUT_DATE";
            this.InputDateTextBoxColumn.FillWeight = 120F;
            this.InputDateTextBoxColumn.HeaderText = "登録日";
            this.InputDateTextBoxColumn.MaxInputLength = 10;
            this.InputDateTextBoxColumn.MinimumWidth = 120;
            this.InputDateTextBoxColumn.Name = "InputDateTextBoxColumn";
            this.InputDateTextBoxColumn.Width = 120;
            // 
            // FavoriteIDTextBoxColumn
            // 
            this.FavoriteIDTextBoxColumn.HeaderText = "ID";
            this.FavoriteIDTextBoxColumn.MaxInputLength = 10;
            this.FavoriteIDTextBoxColumn.Name = "FavoriteIDTextBoxColumn";
            this.FavoriteIDTextBoxColumn.Visible = false;
            // 
            // FavoriteClassIDTextBoxColumn
            // 
            this.FavoriteClassIDTextBoxColumn.HeaderText = "データ区分";
            this.FavoriteClassIDTextBoxColumn.MaxInputLength = 2;
            this.FavoriteClassIDTextBoxColumn.Name = "FavoriteClassIDTextBoxColumn";
            this.FavoriteClassIDTextBoxColumn.Visible = false;
            // 
            // FavoriteListUpdateButton
            // 
            this.FavoriteListUpdateButton.BackColor = System.Drawing.SystemColors.Control;
            this.FavoriteListUpdateButton.Location = new System.Drawing.Point(5, 188);
            this.FavoriteListUpdateButton.Name = "FavoriteListUpdateButton";
            this.FavoriteListUpdateButton.Size = new System.Drawing.Size(120, 30);
            this.FavoriteListUpdateButton.TabIndex = 0;
            this.FavoriteListUpdateButton.Text = "更新";
            this.FavoriteListUpdateButton.UseVisualStyleBackColor = false;
            this.FavoriteListUpdateButton.Click += new System.EventHandler(this.FavoriteListUpdateButton_Click);
            // 
            // FavoriteListDeleteButton
            // 
            this.FavoriteListDeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.FavoriteListDeleteButton.Location = new System.Drawing.Point(131, 188);
            this.FavoriteListDeleteButton.Name = "FavoriteListDeleteButton";
            this.FavoriteListDeleteButton.Size = new System.Drawing.Size(120, 30);
            this.FavoriteListDeleteButton.TabIndex = 1010;
            this.FavoriteListDeleteButton.Text = "削除";
            this.FavoriteListDeleteButton.UseVisualStyleBackColor = false;
            this.FavoriteListDeleteButton.Click += new System.EventHandler(this.FavoriteListDeleteButton_Click);
            // 
            // FavoriteListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(577, 226);
            this.Controls.Add(this.FavoriteListDeleteButton);
            this.Controls.Add(this.FavoriteListUpdateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FavoriteListForm";
            this.Text = "FavoriteListForm";
            this.Load += new System.EventHandler(this.FavoriteListForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.FavoriteListUpdateButton, 0);
            this.Controls.SetChildIndex(this.FavoriteListDeleteButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FavoriteListDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView FavoriteListDataGridView;
        private System.Windows.Forms.Button FavoriteListUpdateButton;
        private System.Windows.Forms.Button FavoriteListDeleteButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DeleteFlagCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FavoriteNameTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputDateTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FavoriteIDTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FavoriteClassIDTextBoxColumn;
    }
}