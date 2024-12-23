namespace DevPlan.Presentation.Common
{
    partial class InformationListForm
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
            this.InformationPanel = new System.Windows.Forms.Panel();
            this.AnnouceDataGridView = new System.Windows.Forms.DataGridView();
            this.InformationTitleLabel = new System.Windows.Forms.Label();
            this.InformationEyecatchPictureBox = new System.Windows.Forms.PictureBox();
            this.GridDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridLink = new System.Windows.Forms.DataGridViewLinkColumn();
            this.InformationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnnouceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InformationEyecatchPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // InformationPanel
            // 
            this.InformationPanel.BackColor = System.Drawing.Color.White;
            this.InformationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InformationPanel.Controls.Add(this.AnnouceDataGridView);
            this.InformationPanel.Controls.Add(this.InformationTitleLabel);
            this.InformationPanel.Controls.Add(this.InformationEyecatchPictureBox);
            this.InformationPanel.Location = new System.Drawing.Point(12, 12);
            this.InformationPanel.Name = "InformationPanel";
            this.InformationPanel.Size = new System.Drawing.Size(370, 400);
            this.InformationPanel.TabIndex = 1;
            // 
            // AnnouceDataGridView
            // 
            this.AnnouceDataGridView.AllowUserToAddRows = false;
            this.AnnouceDataGridView.AllowUserToDeleteRows = false;
            this.AnnouceDataGridView.AllowUserToResizeColumns = false;
            this.AnnouceDataGridView.AllowUserToResizeRows = false;
            this.AnnouceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnnouceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridDate,
            this.GridLink});
            this.AnnouceDataGridView.Location = new System.Drawing.Point(3, 39);
            this.AnnouceDataGridView.MultiSelect = false;
            this.AnnouceDataGridView.Name = "AnnouceDataGridView";
            this.AnnouceDataGridView.ReadOnly = true;
            this.AnnouceDataGridView.RowHeadersVisible = false;
            this.AnnouceDataGridView.RowTemplate.Height = 21;
            this.AnnouceDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AnnouceDataGridView.Size = new System.Drawing.Size(362, 356);
            this.AnnouceDataGridView.TabIndex = 1011;
            this.AnnouceDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AnnouceDataGridView_CellContentClick);
            // 
            // InformationTitleLabel
            // 
            this.InformationTitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InformationTitleLabel.Location = new System.Drawing.Point(39, 3);
            this.InformationTitleLabel.Name = "InformationTitleLabel";
            this.InformationTitleLabel.Size = new System.Drawing.Size(308, 30);
            this.InformationTitleLabel.TabIndex = 1010;
            this.InformationTitleLabel.Text = "お知らせ";
            this.InformationTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InformationEyecatchPictureBox
            // 
            this.InformationEyecatchPictureBox.BackColor = System.Drawing.Color.White;
            this.InformationEyecatchPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InformationEyecatchPictureBox.Image = global::DevPlan.Presentation.Properties.Resources.irc_protocol;
            this.InformationEyecatchPictureBox.Location = new System.Drawing.Point(3, 3);
            this.InformationEyecatchPictureBox.Name = "InformationEyecatchPictureBox";
            this.InformationEyecatchPictureBox.Size = new System.Drawing.Size(30, 30);
            this.InformationEyecatchPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.InformationEyecatchPictureBox.TabIndex = 1009;
            this.InformationEyecatchPictureBox.TabStop = false;
            // 
            // GridDate
            // 
            this.GridDate.DataPropertyName = "INPUT_DATETIME";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "yyyy/MM/dd";
            this.GridDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.GridDate.HeaderText = "日付";
            this.GridDate.Name = "GridDate";
            this.GridDate.ReadOnly = true;
            this.GridDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDate.Width = 110;
            // 
            // GridLink
            // 
            this.GridLink.DataPropertyName = "TITLE";
            this.GridLink.HeaderText = "タイトル";
            this.GridLink.Name = "GridLink";
            this.GridLink.ReadOnly = true;
            this.GridLink.Width = 249;
            // 
            // InformationListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(394, 459);
            this.Controls.Add(this.InformationPanel);
            this.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InformationListForm";
            this.ShowInTaskbar = false;
            this.Text = "InformationListForm";
            this.Load += new System.EventHandler(this.InformationListForm_Load);
            this.InformationPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AnnouceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InformationEyecatchPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel InformationPanel;
        private System.Windows.Forms.Label InformationTitleLabel;
        protected System.Windows.Forms.PictureBox InformationEyecatchPictureBox;
        private System.Windows.Forms.DataGridView AnnouceDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDate;
        private System.Windows.Forms.DataGridViewLinkColumn GridLink;
    }
}