﻿namespace DevPlan.Presentation.UITestCar.Othe.ApplicationApprovalCar
{
    partial class DisposalApplicationForm
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
            this.RunningDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.ManagementNoLabel = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(208, 117);
            this.CloseButton.TabIndex = 3;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.tableLayoutPanel2);
            this.ListFormMainPanel.Size = new System.Drawing.Size(323, 107);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkButton.BackColor = System.Drawing.SystemColors.Control;
            this.OkButton.Location = new System.Drawing.Point(5, 117);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(120, 30);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.RunningDistanceTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label18, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ManagementNoLabel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label17, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 36);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(316, 63);
            this.tableLayoutPanel2.TabIndex = 1014;
            // 
            // RunningDistanceTextBox
            // 
            this.RunningDistanceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RunningDistanceTextBox.Location = new System.Drawing.Point(135, 35);
            this.RunningDistanceTextBox.MaxLength = 25;
            this.RunningDistanceTextBox.Name = "RunningDistanceTextBox";
            this.RunningDistanceTextBox.Size = new System.Drawing.Size(177, 22);
            this.RunningDistanceTextBox.TabIndex = 1;
            this.RunningDistanceTextBox.Tag = "Regex(^[0-9]{1,25}$);Required;ItemName(実走行距離)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Aquamarine;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(1, 32);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(130, 30);
            this.label18.TabIndex = 51;
            this.label18.Text = "実走行距離";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ManagementNoLabel
            // 
            this.ManagementNoLabel.AutoSize = true;
            this.ManagementNoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ManagementNoLabel.Location = new System.Drawing.Point(135, 1);
            this.ManagementNoLabel.Name = "ManagementNoLabel";
            this.ManagementNoLabel.Size = new System.Drawing.Size(177, 30);
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
            this.label17.Size = new System.Drawing.Size(130, 30);
            this.label17.TabIndex = 47;
            this.label17.Text = "管理票NO";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DisposalApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(334, 151);
            this.Controls.Add(this.OkButton);
            this.Name = "DisposalApplicationForm";
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
    }
}