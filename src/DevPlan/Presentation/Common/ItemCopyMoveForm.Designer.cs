namespace DevPlan.Presentation.Common
{
    partial class ItemCopyMoveForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CopyRadioButton = new System.Windows.Forms.RadioButton();
            this.MoveRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GeneralCodeComboBox = new System.Windows.Forms.ComboBox();
            this.SectionGroupIDComboBox = new System.Windows.Forms.ComboBox();
            this.RegistButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(382, 202);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.tableLayoutPanel1);
            this.ListFormMainPanel.Size = new System.Drawing.Size(497, 191);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.44444F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.55556F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.GeneralCodeComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.SectionGroupIDComboBox, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 39);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 147);
            this.tableLayoutPanel1.TabIndex = 1011;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CopyRadioButton);
            this.panel1.Controls.Add(this.MoveRadioButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(119, 87);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 59);
            this.panel1.TabIndex = 1013;
            // 
            // CopyRadioButton
            // 
            this.CopyRadioButton.AutoSize = true;
            this.CopyRadioButton.Checked = true;
            this.CopyRadioButton.Location = new System.Drawing.Point(3, 4);
            this.CopyRadioButton.Name = "CopyRadioButton";
            this.CopyRadioButton.Size = new System.Drawing.Size(318, 19);
            this.CopyRadioButton.TabIndex = 1012;
            this.CopyRadioButton.TabStop = true;
            this.CopyRadioButton.Text = "コピー（スケジュール、進捗履歴はコピーされません）";
            this.CopyRadioButton.UseVisualStyleBackColor = true;
            // 
            // MoveRadioButton
            // 
            this.MoveRadioButton.AutoSize = true;
            this.MoveRadioButton.Location = new System.Drawing.Point(3, 29);
            this.MoveRadioButton.Name = "MoveRadioButton";
            this.MoveRadioButton.Size = new System.Drawing.Size(298, 19);
            this.MoveRadioButton.TabIndex = 1012;
            this.MoveRadioButton.Text = "移動（スケジュール、進捗履歴も移動されます）";
            this.MoveRadioButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(1, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 59);
            this.label3.TabIndex = 1013;
            this.label3.Text = "コピー／移動";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "開発符号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(1, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 42);
            this.label2.TabIndex = 1012;
            this.label2.Text = "担当";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GeneralCodeComboBox
            // 
            this.GeneralCodeComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GeneralCodeComboBox.DisplayMember = "NAME";
            this.GeneralCodeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GeneralCodeComboBox.FormattingEnabled = true;
            this.GeneralCodeComboBox.Location = new System.Drawing.Point(126, 12);
            this.GeneralCodeComboBox.Name = "GeneralCodeComboBox";
            this.GeneralCodeComboBox.Size = new System.Drawing.Size(350, 23);
            this.GeneralCodeComboBox.TabIndex = 1014;
            this.GeneralCodeComboBox.ValueMember = "CODE";
            this.GeneralCodeComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GeneralCodeComboBox_MouseClick);
            // 
            // SectionGroupIDComboBox
            // 
            this.SectionGroupIDComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SectionGroupIDComboBox.DisplayMember = "NAME";
            this.SectionGroupIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SectionGroupIDComboBox.FormattingEnabled = true;
            this.SectionGroupIDComboBox.Location = new System.Drawing.Point(126, 55);
            this.SectionGroupIDComboBox.Name = "SectionGroupIDComboBox";
            this.SectionGroupIDComboBox.Size = new System.Drawing.Size(350, 23);
            this.SectionGroupIDComboBox.TabIndex = 1015;
            this.SectionGroupIDComboBox.ValueMember = "CODE";
            this.SectionGroupIDComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SectionGroupIDComboBox_MouseClick);
            // 
            // RegistButton
            // 
            this.RegistButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RegistButton.BackColor = System.Drawing.SystemColors.Control;
            this.RegistButton.Location = new System.Drawing.Point(5, 202);
            this.RegistButton.Name = "RegistButton";
            this.RegistButton.Size = new System.Drawing.Size(120, 30);
            this.RegistButton.TabIndex = 1010;
            this.RegistButton.Text = "登録";
            this.RegistButton.UseVisualStyleBackColor = false;
            this.RegistButton.Click += new System.EventHandler(this.RegistButton_Click);
            // 
            // ItemCopyMoveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 236);
            this.Controls.Add(this.RegistButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ItemCopyMoveForm";
            this.Text = "ItemCopyMove";
            this.Load += new System.EventHandler(this.ItemCopyMoveForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.RegistButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton CopyRadioButton;
        private System.Windows.Forms.RadioButton MoveRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox GeneralCodeComboBox;
        private System.Windows.Forms.ComboBox SectionGroupIDComboBox;
        private System.Windows.Forms.Button RegistButton;
    }
}