namespace DevPlan.Presentation.UITestCar.Othe.ApplicationApprovalCar
{
    partial class TransferApplicationForm
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
            this.OkButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.SectionGroupComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RunningDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.ManagementNoLabel = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(260, 357);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(5);
            this.CloseButton.TabIndex = 4;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.ListFormTitleLabel.Size = new System.Drawing.Size(298, 24);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.label2);
            this.ListFormMainPanel.Controls.Add(this.label4);
            this.ListFormMainPanel.Controls.Add(this.label3);
            this.ListFormMainPanel.Controls.Add(this.tableLayoutPanel2);
            this.ListFormMainPanel.Margin = new System.Windows.Forms.Padding(5);
            this.ListFormMainPanel.Size = new System.Drawing.Size(403, 341);
            this.ListFormMainPanel.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label3, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label4, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label2, 0);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkButton.BackColor = System.Drawing.SystemColors.Control;
            this.OkButton.Location = new System.Drawing.Point(6, 357);
            this.OkButton.Margin = new System.Windows.Forms.Padding(4);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(150, 38);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.SectionGroupComboBox, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.RunningDistanceTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label18, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ManagementNoLabel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label17, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 45);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(395, 115);
            this.tableLayoutPanel2.TabIndex = 1014;
            // 
            // SectionGroupComboBox
            // 
            this.SectionGroupComboBox.DisplayMember = "NAME";
            this.SectionGroupComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SectionGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SectionGroupComboBox.FormattingEnabled = true;
            this.SectionGroupComboBox.Location = new System.Drawing.Point(168, 79);
            this.SectionGroupComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.SectionGroupComboBox.Name = "SectionGroupComboBox";
            this.SectionGroupComboBox.Size = new System.Drawing.Size(222, 27);
            this.SectionGroupComboBox.TabIndex = 2;
            this.SectionGroupComboBox.Tag = "Required;ItemName(移管先部署)";
            this.SectionGroupComboBox.ValueMember = "CODE";
            this.SectionGroupComboBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SectionGroupComboBox_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 39);
            this.label1.TabIndex = 53;
            this.label1.Text = "移管先部署";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RunningDistanceTextBox
            // 
            this.RunningDistanceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RunningDistanceTextBox.Location = new System.Drawing.Point(168, 44);
            this.RunningDistanceTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.RunningDistanceTextBox.MaxLength = 25;
            this.RunningDistanceTextBox.Name = "RunningDistanceTextBox";
            this.RunningDistanceTextBox.Size = new System.Drawing.Size(222, 26);
            this.RunningDistanceTextBox.TabIndex = 1;
            this.RunningDistanceTextBox.Tag = "Regex(^[0-9]{1,25}$);Required;ItemName(実走行距離)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Aquamarine;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(1, 40);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(162, 34);
            this.label18.TabIndex = 51;
            this.label18.Text = "実走行距離";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ManagementNoLabel
            // 
            this.ManagementNoLabel.AutoSize = true;
            this.ManagementNoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ManagementNoLabel.Location = new System.Drawing.Point(168, 1);
            this.ManagementNoLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ManagementNoLabel.Name = "ManagementNoLabel";
            this.ManagementNoLabel.Size = new System.Drawing.Size(222, 38);
            this.ManagementNoLabel.TabIndex = 49;
            this.ManagementNoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Aquamarine;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(1, 1);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(162, 38);
            this.label17.TabIndex = 47;
            this.label17.Text = "管理票NO";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 236);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(356, 95);
            this.label4.TabIndex = 1017;
            this.label4.Text = "車両を引き渡す際、以下の物も必ず渡すこと。\r\n・スペアキー\r\n・キー№プレート\r\n・セキュリティーＩＤ№プレート\r\n（イモビライザー付車）";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(2, 208);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(395, 28);
            this.label3.TabIndex = 1016;
            this.label3.Text = "<<注意>>";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(49, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(324, 28);
            this.label2.TabIndex = 1018;
            this.label2.Text = "認証車を移管する場合は、今後の利用予定がないか\r\nNHGB及びSKSへ確認しましたか？";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TransferApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.ClientSize = new System.Drawing.Size(418, 399);
            this.Controls.Add(this.OkButton);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "TransferApplicationForm";
            this.Text = "タイトルが設定されていません - 開発計画表システム";
            this.Load += new System.EventHandler(this.DisposalApplicationForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.OkButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox RunningDistanceTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label ManagementNoLabel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SectionGroupComboBox;
        private System.Windows.Forms.Label label2;
    }
}
