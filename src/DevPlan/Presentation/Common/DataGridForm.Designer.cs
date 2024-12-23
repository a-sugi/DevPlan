namespace DevPlan.Presentation.Common
{
    partial class DataGridForm
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
            this.SectionListDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(560, 466);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.SectionListDataGridView);
            this.ListFormMainPanel.Size = new System.Drawing.Size(675, 455);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SectionListDataGridView, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            // 
            // SectionListDataGridView
            // 
            this.SectionListDataGridView.AllowUserToAddRows = false;
            this.SectionListDataGridView.AllowUserToDeleteRows = false;
            this.SectionListDataGridView.AllowUserToOrderColumns = true;
            this.SectionListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.SectionListDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SectionListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SectionListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SectionListDataGridView.Location = new System.Drawing.Point(3, 39);
            this.SectionListDataGridView.MultiSelect = false;
            this.SectionListDataGridView.Name = "SectionListDataGridView";
            this.SectionListDataGridView.ReadOnly = true;
            this.SectionListDataGridView.RowHeadersVisible = false;
            this.SectionListDataGridView.RowTemplate.Height = 21;
            this.SectionListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SectionListDataGridView.Size = new System.Drawing.Size(667, 411);
            this.SectionListDataGridView.TabIndex = 1013;
            this.SectionListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SectionListDataGridView_CellClick);
            // 
            // DataGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 498);
            this.MinimumSize = new System.Drawing.Size(410, 520);
            this.Name = "DataGridForm";
            this.Text = "タイトルが設定されていません - 開発計画表システム";
            this.Load += new System.EventHandler(this.SectionListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionListDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView SectionListDataGridView;
    }
}