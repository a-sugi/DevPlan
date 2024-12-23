namespace DevPlan.Presentation.UIDevPlan.OperationPlan
{
    partial class OperationPlanScheduleForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.EndDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.StartDayDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TypePanel = new System.Windows.Forms.Panel();
            this.DoubleCircleRadioButton = new System.Windows.Forms.RadioButton();
            this.TriangleRadioButton = new System.Windows.Forms.RadioButton();
            this.SquareRadioButton = new System.Windows.Forms.RadioButton();
            this.DefaultRadioButton = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.StatusCheckBox = new System.Windows.Forms.CheckBox();
            this.EntryButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.DetailTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).BeginInit();
            this.TypePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseButton.Location = new System.Drawing.Point(310, 226);
            this.CloseButton.Size = new System.Drawing.Size(80, 30);
            this.CloseButton.TabIndex = 1005;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.ListFormMainPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ListFormMainPanel.Size = new System.Drawing.Size(385, 215);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DetailTableLayoutPanel, 0);
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
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.DetailTableLayoutPanel.Controls.Add(this.label14, 0, 4);
            this.DetailTableLayoutPanel.Controls.Add(this.EndDayDateTimePicker, 1, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.StartDayDateTimePicker, 1, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.label13, 0, 3);
            this.DetailTableLayoutPanel.Controls.Add(this.label12, 0, 2);
            this.DetailTableLayoutPanel.Controls.Add(this.label11, 0, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.TypePanel, 1, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.label10, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.TitleTextBox, 1, 1);
            this.DetailTableLayoutPanel.Controls.Add(this.StatusCheckBox, 1, 4);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(3, 53);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 5;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(377, 157);
            this.DetailTableLayoutPanel.TabIndex = 1011;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Aquamarine;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(1, 125);
            this.label14.Margin = new System.Windows.Forms.Padding(0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(112, 31);
            this.label14.TabIndex = 12;
            this.label14.Text = "ステータス";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EndDayDateTimePicker
            // 
            this.EndDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.EndDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDayDateTimePicker.Location = new System.Drawing.Point(117, 97);
            this.EndDayDateTimePicker.Name = "EndDayDateTimePicker";
            this.EndDayDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.EndDayDateTimePicker.TabIndex = 26;
            this.EndDayDateTimePicker.Tag = "Required;Required;ItemName(終了日)";
            this.EndDayDateTimePicker.Value = new System.DateTime(2017, 3, 31, 0, 0, 0, 0);
            // 
            // StartDayDateTimePicker
            // 
            this.StartDayDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.StartDayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDayDateTimePicker.Location = new System.Drawing.Point(117, 66);
            this.StartDayDateTimePicker.Name = "StartDayDateTimePicker";
            this.StartDayDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.StartDayDateTimePicker.TabIndex = 25;
            this.StartDayDateTimePicker.Tag = "Required;ItemName(開始日)";
            this.StartDayDateTimePicker.Value = new System.DateTime(2017, 3, 31, 0, 0, 0, 0);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Aquamarine;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(1, 94);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 30);
            this.label13.TabIndex = 8;
            this.label13.Text = "終了日";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Aquamarine;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(1, 63);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 30);
            this.label12.TabIndex = 6;
            this.label12.Text = "開始日";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Aquamarine;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(1, 32);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 30);
            this.label11.TabIndex = 4;
            this.label11.Text = "タイトル";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TypePanel
            // 
            this.TypePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TypePanel.Controls.Add(this.DoubleCircleRadioButton);
            this.TypePanel.Controls.Add(this.TriangleRadioButton);
            this.TypePanel.Controls.Add(this.SquareRadioButton);
            this.TypePanel.Controls.Add(this.DefaultRadioButton);
            this.TypePanel.Location = new System.Drawing.Point(114, 1);
            this.TypePanel.Margin = new System.Windows.Forms.Padding(0);
            this.TypePanel.Name = "TypePanel";
            this.TypePanel.Size = new System.Drawing.Size(262, 30);
            this.TypePanel.TabIndex = 3;
            // 
            // DoubleCircleRadioButton
            // 
            this.DoubleCircleRadioButton.AutoSize = true;
            this.DoubleCircleRadioButton.Location = new System.Drawing.Point(153, 6);
            this.DoubleCircleRadioButton.Name = "DoubleCircleRadioButton";
            this.DoubleCircleRadioButton.Size = new System.Drawing.Size(40, 19);
            this.DoubleCircleRadioButton.TabIndex = 23;
            this.DoubleCircleRadioButton.Tag = "4";
            this.DoubleCircleRadioButton.Text = "◎";
            this.DoubleCircleRadioButton.UseVisualStyleBackColor = true;
            // 
            // TriangleRadioButton
            // 
            this.TriangleRadioButton.AutoSize = true;
            this.TriangleRadioButton.Location = new System.Drawing.Point(107, 6);
            this.TriangleRadioButton.Name = "TriangleRadioButton";
            this.TriangleRadioButton.Size = new System.Drawing.Size(40, 19);
            this.TriangleRadioButton.TabIndex = 22;
            this.TriangleRadioButton.Tag = "3";
            this.TriangleRadioButton.Text = "▲";
            this.TriangleRadioButton.UseVisualStyleBackColor = true;
            // 
            // SquareRadioButton
            // 
            this.SquareRadioButton.AutoSize = true;
            this.SquareRadioButton.Location = new System.Drawing.Point(61, 6);
            this.SquareRadioButton.Name = "SquareRadioButton";
            this.SquareRadioButton.Size = new System.Drawing.Size(40, 19);
            this.SquareRadioButton.TabIndex = 21;
            this.SquareRadioButton.Tag = "2";
            this.SquareRadioButton.Text = "■";
            this.SquareRadioButton.UseVisualStyleBackColor = true;
            // 
            // DefaultRadioButton
            // 
            this.DefaultRadioButton.AutoSize = true;
            this.DefaultRadioButton.Checked = true;
            this.DefaultRadioButton.Location = new System.Drawing.Point(4, 6);
            this.DefaultRadioButton.Name = "DefaultRadioButton";
            this.DefaultRadioButton.Size = new System.Drawing.Size(51, 19);
            this.DefaultRadioButton.TabIndex = 20;
            this.DefaultRadioButton.TabStop = true;
            this.DefaultRadioButton.Tag = "1";
            this.DefaultRadioButton.Text = "無し";
            this.DefaultRadioButton.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Aquamarine;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(1, 1);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 30);
            this.label10.TabIndex = 2;
            this.label10.Text = "区分";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleTextBox.Location = new System.Drawing.Point(117, 35);
            this.TitleTextBox.MaxLength = 30;
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(256, 22);
            this.TitleTextBox.TabIndex = 24;
            this.TitleTextBox.Tag = "Required;Wide(30);ItemName(タイトル)";
            // 
            // StatusCheckBox
            // 
            this.StatusCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusCheckBox.AutoSize = true;
            this.StatusCheckBox.Location = new System.Drawing.Point(117, 128);
            this.StatusCheckBox.Name = "StatusCheckBox";
            this.StatusCheckBox.Size = new System.Drawing.Size(86, 25);
            this.StatusCheckBox.TabIndex = 27;
            this.StatusCheckBox.Text = "作業完了";
            this.StatusCheckBox.UseVisualStyleBackColor = true;
            // 
            // EntryButton
            // 
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 226);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(80, 30);
            this.EntryButton.TabIndex = 1000;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Location = new System.Drawing.Point(91, 226);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(80, 30);
            this.DeleteButton.TabIndex = 1001;
            this.DeleteButton.Text = "削除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // OperationPlanScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 263);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EntryButton);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "OperationPlanScheduleForm";
            this.Text = "OperationPlanDetailForm";
            this.Load += new System.EventHandler(this.OperationPlanDetailForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.DeleteButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDayDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDayDateTimePicker)).EndInit();
            this.TypePanel.ResumeLayout(false);
            this.TypePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private System.Windows.Forms.Panel TypePanel;
        private System.Windows.Forms.RadioButton DoubleCircleRadioButton;
        private System.Windows.Forms.RadioButton TriangleRadioButton;
        private System.Windows.Forms.RadioButton SquareRadioButton;
        private System.Windows.Forms.RadioButton DefaultRadioButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TitleTextBox;
        private UC.NullableDateTimePicker EndDayDateTimePicker;
        private UC.NullableDateTimePicker StartDayDateTimePicker;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox StatusCheckBox;
    }
}