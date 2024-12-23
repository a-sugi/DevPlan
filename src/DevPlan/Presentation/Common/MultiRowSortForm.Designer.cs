namespace DevPlan.Presentation.Common
{
    partial class MultiRowSortForm
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
            this.SortButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Sort1DescRadioButton = new System.Windows.Forms.RadioButton();
            this.Sort1AscRadioButton = new System.Windows.Forms.RadioButton();
            this.Sort1ComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Sort2DescRadioButton = new System.Windows.Forms.RadioButton();
            this.Sort2AscRadioButton = new System.Windows.Forms.RadioButton();
            this.Sort2ComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Sort3DescRadioButton = new System.Windows.Forms.RadioButton();
            this.Sort3AscRadioButton = new System.Windows.Forms.RadioButton();
            this.Sort3ComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(168, 217);
            this.CloseButton.TabIndex = 2;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.groupBox3);
            this.ListFormMainPanel.Controls.Add(this.groupBox2);
            this.ListFormMainPanel.Controls.Add(this.groupBox1);
            this.ListFormMainPanel.Size = new System.Drawing.Size(283, 207);
            this.ListFormMainPanel.TabIndex = 0;
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.groupBox1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.groupBox2, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.groupBox3, 0);
            // 
            // SortButton
            // 
            this.SortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SortButton.BackColor = System.Drawing.SystemColors.Control;
            this.SortButton.Location = new System.Drawing.Point(5, 217);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(120, 30);
            this.SortButton.TabIndex = 1;
            this.SortButton.Text = "並び替え";
            this.SortButton.UseVisualStyleBackColor = false;
            this.SortButton.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Sort1DescRadioButton);
            this.groupBox1.Controls.Add(this.Sort1AscRadioButton);
            this.groupBox1.Controls.Add(this.Sort1ComboBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "第1ソートキー";
            // 
            // Sort1DescRadioButton
            // 
            this.Sort1DescRadioButton.AutoSize = true;
            this.Sort1DescRadioButton.Location = new System.Drawing.Point(213, 22);
            this.Sort1DescRadioButton.Name = "Sort1DescRadioButton";
            this.Sort1DescRadioButton.Size = new System.Drawing.Size(55, 19);
            this.Sort1DescRadioButton.TabIndex = 2;
            this.Sort1DescRadioButton.Text = "降順";
            this.Sort1DescRadioButton.UseVisualStyleBackColor = true;
            // 
            // Sort1AscRadioButton
            // 
            this.Sort1AscRadioButton.AutoSize = true;
            this.Sort1AscRadioButton.Checked = true;
            this.Sort1AscRadioButton.Location = new System.Drawing.Point(152, 22);
            this.Sort1AscRadioButton.Name = "Sort1AscRadioButton";
            this.Sort1AscRadioButton.Size = new System.Drawing.Size(55, 19);
            this.Sort1AscRadioButton.TabIndex = 1;
            this.Sort1AscRadioButton.TabStop = true;
            this.Sort1AscRadioButton.Text = "昇順";
            this.Sort1AscRadioButton.UseVisualStyleBackColor = true;
            // 
            // Sort1ComboBox
            // 
            this.Sort1ComboBox.DisplayMember = "Name";
            this.Sort1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Sort1ComboBox.FormattingEnabled = true;
            this.Sort1ComboBox.Location = new System.Drawing.Point(6, 21);
            this.Sort1ComboBox.Name = "Sort1ComboBox";
            this.Sort1ComboBox.Size = new System.Drawing.Size(140, 23);
            this.Sort1ComboBox.TabIndex = 0;
            this.Sort1ComboBox.Tag = "Required;ItemName(第1ソートキー)";
            this.Sort1ComboBox.ValueMember = "DataPropertyName";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Sort2DescRadioButton);
            this.groupBox2.Controls.Add(this.Sort2AscRadioButton);
            this.groupBox2.Controls.Add(this.Sort2ComboBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 50);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "第2ソートキー";
            // 
            // Sort2DescRadioButton
            // 
            this.Sort2DescRadioButton.AutoSize = true;
            this.Sort2DescRadioButton.Location = new System.Drawing.Point(213, 22);
            this.Sort2DescRadioButton.Name = "Sort2DescRadioButton";
            this.Sort2DescRadioButton.Size = new System.Drawing.Size(55, 19);
            this.Sort2DescRadioButton.TabIndex = 2;
            this.Sort2DescRadioButton.Text = "降順";
            this.Sort2DescRadioButton.UseVisualStyleBackColor = true;
            // 
            // Sort2AscRadioButton
            // 
            this.Sort2AscRadioButton.AutoSize = true;
            this.Sort2AscRadioButton.Checked = true;
            this.Sort2AscRadioButton.Location = new System.Drawing.Point(152, 22);
            this.Sort2AscRadioButton.Name = "Sort2AscRadioButton";
            this.Sort2AscRadioButton.Size = new System.Drawing.Size(55, 19);
            this.Sort2AscRadioButton.TabIndex = 1;
            this.Sort2AscRadioButton.TabStop = true;
            this.Sort2AscRadioButton.Text = "昇順";
            this.Sort2AscRadioButton.UseVisualStyleBackColor = true;
            // 
            // Sort2ComboBox
            // 
            this.Sort2ComboBox.DisplayMember = "Name";
            this.Sort2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Sort2ComboBox.FormattingEnabled = true;
            this.Sort2ComboBox.Location = new System.Drawing.Point(6, 21);
            this.Sort2ComboBox.Name = "Sort2ComboBox";
            this.Sort2ComboBox.Size = new System.Drawing.Size(140, 23);
            this.Sort2ComboBox.TabIndex = 0;
            this.Sort2ComboBox.Tag = "ItemName(第2ソートキー)";
            this.Sort2ComboBox.ValueMember = "DataPropertyName";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Sort3DescRadioButton);
            this.groupBox3.Controls.Add(this.Sort3AscRadioButton);
            this.groupBox3.Controls.Add(this.Sort3ComboBox);
            this.groupBox3.Location = new System.Drawing.Point(3, 152);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(275, 50);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "第3ソートキー";
            // 
            // Sort3DescRadioButton
            // 
            this.Sort3DescRadioButton.AutoSize = true;
            this.Sort3DescRadioButton.Location = new System.Drawing.Point(213, 22);
            this.Sort3DescRadioButton.Name = "Sort3DescRadioButton";
            this.Sort3DescRadioButton.Size = new System.Drawing.Size(55, 19);
            this.Sort3DescRadioButton.TabIndex = 2;
            this.Sort3DescRadioButton.Text = "降順";
            this.Sort3DescRadioButton.UseVisualStyleBackColor = true;
            // 
            // Sort3AscRadioButton
            // 
            this.Sort3AscRadioButton.AutoSize = true;
            this.Sort3AscRadioButton.Checked = true;
            this.Sort3AscRadioButton.Location = new System.Drawing.Point(152, 22);
            this.Sort3AscRadioButton.Name = "Sort3AscRadioButton";
            this.Sort3AscRadioButton.Size = new System.Drawing.Size(55, 19);
            this.Sort3AscRadioButton.TabIndex = 1;
            this.Sort3AscRadioButton.TabStop = true;
            this.Sort3AscRadioButton.Text = "昇順";
            this.Sort3AscRadioButton.UseVisualStyleBackColor = true;
            // 
            // Sort3ComboBox
            // 
            this.Sort3ComboBox.DisplayMember = "Name";
            this.Sort3ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Sort3ComboBox.FormattingEnabled = true;
            this.Sort3ComboBox.Location = new System.Drawing.Point(6, 21);
            this.Sort3ComboBox.Name = "Sort3ComboBox";
            this.Sort3ComboBox.Size = new System.Drawing.Size(140, 23);
            this.Sort3ComboBox.TabIndex = 0;
            this.Sort3ComboBox.Tag = "ItemName(第3ソートキー)";
            this.Sort3ComboBox.ValueMember = "DataPropertyName";
            // 
            // DataGridViewSortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(294, 251);
            this.Controls.Add(this.SortButton);
            this.Name = "DataGridViewSortForm";
            this.Text = "タイトルが設定されていません - 開発計画表システム";
            this.Load += new System.EventHandler(this.DataGridViewSortForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.SortButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SortButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Sort1DescRadioButton;
        private System.Windows.Forms.RadioButton Sort1AscRadioButton;
        private System.Windows.Forms.ComboBox Sort1ComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton Sort2DescRadioButton;
        private System.Windows.Forms.RadioButton Sort2AscRadioButton;
        private System.Windows.Forms.ComboBox Sort2ComboBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton Sort3DescRadioButton;
        private System.Windows.Forms.RadioButton Sort3AscRadioButton;
        private System.Windows.Forms.ComboBox Sort3ComboBox;
    }
}
