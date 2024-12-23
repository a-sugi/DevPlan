namespace DevPlan.Presentation.Common
{
    partial class BookmarkListForm
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
            this.InformationPanel = new System.Windows.Forms.Panel();
            this.InformationDataGridView = new System.Windows.Forms.DataGridView();
            this.InformationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InformationTitleLabel = new System.Windows.Forms.Label();
            this.InformationEyecatchPictureBox = new System.Windows.Forms.PictureBox();
            this.LeftButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.InformationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InformationDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InformationEyecatchPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // InformationPanel
            // 
            this.InformationPanel.BackColor = System.Drawing.Color.White;
            this.InformationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InformationPanel.Controls.Add(this.InformationDataGridView);
            this.InformationPanel.Controls.Add(this.InformationTitleLabel);
            this.InformationPanel.Controls.Add(this.InformationEyecatchPictureBox);
            this.InformationPanel.Location = new System.Drawing.Point(12, 12);
            this.InformationPanel.Name = "InformationPanel";
            this.InformationPanel.Size = new System.Drawing.Size(370, 400);
            this.InformationPanel.TabIndex = 1;
            // 
            // InformationDataGridView
            // 
            this.InformationDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.InformationDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InformationDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.InformationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InformationDataGridView.ColumnHeadersVisible = false;
            this.InformationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InformationColumn});
            this.InformationDataGridView.Location = new System.Drawing.Point(4, 54);
            this.InformationDataGridView.Name = "InformationDataGridView";
            this.InformationDataGridView.RowHeadersVisible = false;
            this.InformationDataGridView.RowHeadersWidth = 60;
            this.InformationDataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.InformationDataGridView.RowTemplate.Height = 60;
            this.InformationDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InformationDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InformationDataGridView.Size = new System.Drawing.Size(361, 341);
            this.InformationDataGridView.TabIndex = 1011;
            // 
            // InformationColumn
            // 
            this.InformationColumn.HeaderText = "お知らせ内容";
            this.InformationColumn.Name = "InformationColumn";
            this.InformationColumn.ReadOnly = true;
            this.InformationColumn.Width = 360;
            // 
            // InformationTitleLabel
            // 
            this.InformationTitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InformationTitleLabel.Location = new System.Drawing.Point(39, 3);
            this.InformationTitleLabel.Name = "InformationTitleLabel";
            this.InformationTitleLabel.Size = new System.Drawing.Size(308, 30);
            this.InformationTitleLabel.TabIndex = 1010;
            this.InformationTitleLabel.Text = "お気に入り";
            this.InformationTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InformationEyecatchPictureBox
            // 
            this.InformationEyecatchPictureBox.BackColor = System.Drawing.Color.White;
            this.InformationEyecatchPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InformationEyecatchPictureBox.Image = global::DevPlan.Presentation.Properties.Resources.ksmiletris;
            this.InformationEyecatchPictureBox.Location = new System.Drawing.Point(3, 3);
            this.InformationEyecatchPictureBox.Name = "InformationEyecatchPictureBox";
            this.InformationEyecatchPictureBox.Size = new System.Drawing.Size(30, 30);
            this.InformationEyecatchPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.InformationEyecatchPictureBox.TabIndex = 1009;
            this.InformationEyecatchPictureBox.TabStop = false;
            // 
            // LeftButton
            // 
            this.LeftButton.Location = new System.Drawing.Point(262, 418);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Size = new System.Drawing.Size(120, 30);
            this.LeftButton.TabIndex = 1002;
            this.LeftButton.Text = "削除";
            this.LeftButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 418);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 30);
            this.button1.TabIndex = 1003;
            this.button1.Text = "追加";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // BookmarkListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(394, 459);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LeftButton);
            this.Controls.Add(this.InformationPanel);
            this.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BookmarkListForm";
            this.ShowInTaskbar = false;
            this.Text = "InformationListForm";
            this.InformationPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InformationDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InformationEyecatchPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel InformationPanel;
        private System.Windows.Forms.DataGridView InformationDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn InformationColumn;
        private System.Windows.Forms.Label InformationTitleLabel;
        protected System.Windows.Forms.PictureBox InformationEyecatchPictureBox;
        protected System.Windows.Forms.Button LeftButton;
        protected System.Windows.Forms.Button button1;
    }
}