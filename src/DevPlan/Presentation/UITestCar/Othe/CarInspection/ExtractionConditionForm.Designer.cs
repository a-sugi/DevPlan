namespace DevPlan.Presentation.UITestCar.Othe.CarInspection
{
    partial class ExtractionConditionForm
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
            this.SearchCheckBox = new System.Windows.Forms.CheckBox();
            this.InspectionGroupBox = new System.Windows.Forms.GroupBox();
            this.CarInspectionCheckBox = new System.Windows.Forms.CheckBox();
            this.Inspection12CheckBox = new System.Windows.Forms.CheckBox();
            this.Inspection6CheckBox = new System.Windows.Forms.CheckBox();
            this.CarGroupGroupBox = new System.Windows.Forms.GroupBox();
            this.NoneCheckBox = new System.Windows.Forms.CheckBox();
            this.LargeCargoCheckBox = new System.Windows.Forms.CheckBox();
            this.NormalRidingCheckBox = new System.Windows.Forms.CheckBox();
            this.SmallCargoCheckBox = new System.Windows.Forms.CheckBox();
            this.SmallRidingCheckBox = new System.Windows.Forms.CheckBox();
            this.MiddleRidingCheckBox = new System.Windows.Forms.CheckBox();
            this.LightCargoCheckBox = new System.Windows.Forms.CheckBox();
            this.LightRidingCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.InspectionGroupBox.SuspendLayout();
            this.CarGroupGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(248, 242);
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
            this.ListFormMainPanel.Controls.Add(this.SearchCheckBox);
            this.ListFormMainPanel.Controls.Add(this.InspectionGroupBox);
            this.ListFormMainPanel.Controls.Add(this.CarGroupGroupBox);
            this.ListFormMainPanel.Controls.Add(this.label1);
            this.ListFormMainPanel.Size = new System.Drawing.Size(363, 232);
            this.ListFormMainPanel.TabIndex = 0;
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.CarGroupGroupBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.InspectionGroupBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SearchCheckBox, 0);
            // 
            // SearchCheckBox
            // 
            this.SearchCheckBox.AutoSize = true;
            this.SearchCheckBox.Location = new System.Drawing.Point(9, 207);
            this.SearchCheckBox.Name = "SearchCheckBox";
            this.SearchCheckBox.Size = new System.Drawing.Size(212, 19);
            this.SearchCheckBox.TabIndex = 2;
            this.SearchCheckBox.Text = "設定終了後リストを作成し直す";
            this.SearchCheckBox.UseVisualStyleBackColor = true;
            // 
            // InspectionGroupBox
            // 
            this.InspectionGroupBox.Controls.Add(this.CarInspectionCheckBox);
            this.InspectionGroupBox.Controls.Add(this.Inspection12CheckBox);
            this.InspectionGroupBox.Controls.Add(this.Inspection6CheckBox);
            this.InspectionGroupBox.Location = new System.Drawing.Point(3, 156);
            this.InspectionGroupBox.Name = "InspectionGroupBox";
            this.InspectionGroupBox.Size = new System.Drawing.Size(353, 45);
            this.InspectionGroupBox.TabIndex = 1;
            this.InspectionGroupBox.TabStop = false;
            this.InspectionGroupBox.Tag = "ItemName(点検区分)";
            this.InspectionGroupBox.Text = "点検区分";
            // 
            // CarInspectionCheckBox
            // 
            this.CarInspectionCheckBox.AutoSize = true;
            this.CarInspectionCheckBox.Location = new System.Drawing.Point(205, 21);
            this.CarInspectionCheckBox.Name = "CarInspectionCheckBox";
            this.CarInspectionCheckBox.Size = new System.Drawing.Size(56, 19);
            this.CarInspectionCheckBox.TabIndex = 2;
            this.CarInspectionCheckBox.Tag = "1";
            this.CarInspectionCheckBox.Text = "車検";
            this.CarInspectionCheckBox.UseVisualStyleBackColor = true;
            // 
            // Inspection12CheckBox
            // 
            this.Inspection12CheckBox.AutoSize = true;
            this.Inspection12CheckBox.Location = new System.Drawing.Point(102, 21);
            this.Inspection12CheckBox.Name = "Inspection12CheckBox";
            this.Inspection12CheckBox.Size = new System.Drawing.Size(97, 19);
            this.Inspection12CheckBox.TabIndex = 1;
            this.Inspection12CheckBox.Tag = "2";
            this.Inspection12CheckBox.Text = "12ヶ月点検";
            this.Inspection12CheckBox.UseVisualStyleBackColor = true;
            // 
            // Inspection6CheckBox
            // 
            this.Inspection6CheckBox.AutoSize = true;
            this.Inspection6CheckBox.Location = new System.Drawing.Point(7, 22);
            this.Inspection6CheckBox.Name = "Inspection6CheckBox";
            this.Inspection6CheckBox.Size = new System.Drawing.Size(89, 19);
            this.Inspection6CheckBox.TabIndex = 0;
            this.Inspection6CheckBox.Tag = "3";
            this.Inspection6CheckBox.Text = "6ヶ月点検";
            this.Inspection6CheckBox.UseVisualStyleBackColor = true;
            // 
            // CarGroupGroupBox
            // 
            this.CarGroupGroupBox.Controls.Add(this.NoneCheckBox);
            this.CarGroupGroupBox.Controls.Add(this.LargeCargoCheckBox);
            this.CarGroupGroupBox.Controls.Add(this.NormalRidingCheckBox);
            this.CarGroupGroupBox.Controls.Add(this.SmallCargoCheckBox);
            this.CarGroupGroupBox.Controls.Add(this.SmallRidingCheckBox);
            this.CarGroupGroupBox.Controls.Add(this.MiddleRidingCheckBox);
            this.CarGroupGroupBox.Controls.Add(this.LightCargoCheckBox);
            this.CarGroupGroupBox.Controls.Add(this.LightRidingCheckBox);
            this.CarGroupGroupBox.Location = new System.Drawing.Point(3, 55);
            this.CarGroupGroupBox.Name = "CarGroupGroupBox";
            this.CarGroupGroupBox.Size = new System.Drawing.Size(353, 95);
            this.CarGroupGroupBox.TabIndex = 0;
            this.CarGroupGroupBox.TabStop = false;
            this.CarGroupGroupBox.Tag = "ItemName(車系)";
            this.CarGroupGroupBox.Text = "車系";
            // 
            // NoneCheckBox
            // 
            this.NoneCheckBox.AutoSize = true;
            this.NoneCheckBox.Location = new System.Drawing.Point(6, 71);
            this.NoneCheckBox.Name = "NoneCheckBox";
            this.NoneCheckBox.Size = new System.Drawing.Size(81, 19);
            this.NoneCheckBox.TabIndex = 7;
            this.NoneCheckBox.Text = "(未入力)";
            this.NoneCheckBox.UseVisualStyleBackColor = true;
            // 
            // LargeCargoCheckBox
            // 
            this.LargeCargoCheckBox.AutoSize = true;
            this.LargeCargoCheckBox.Location = new System.Drawing.Point(175, 46);
            this.LargeCargoCheckBox.Name = "LargeCargoCheckBox";
            this.LargeCargoCheckBox.Size = new System.Drawing.Size(86, 19);
            this.LargeCargoCheckBox.TabIndex = 6;
            this.LargeCargoCheckBox.Tag = "大型貨物";
            this.LargeCargoCheckBox.Text = "大型貨物";
            this.LargeCargoCheckBox.UseVisualStyleBackColor = true;
            // 
            // NormalRidingCheckBox
            // 
            this.NormalRidingCheckBox.AutoSize = true;
            this.NormalRidingCheckBox.Location = new System.Drawing.Point(175, 21);
            this.NormalRidingCheckBox.Name = "NormalRidingCheckBox";
            this.NormalRidingCheckBox.Size = new System.Drawing.Size(86, 19);
            this.NormalRidingCheckBox.TabIndex = 2;
            this.NormalRidingCheckBox.Tag = "普通乗用";
            this.NormalRidingCheckBox.Text = "普通乗用";
            this.NormalRidingCheckBox.UseVisualStyleBackColor = true;
            // 
            // SmallCargoCheckBox
            // 
            this.SmallCargoCheckBox.AutoSize = true;
            this.SmallCargoCheckBox.Location = new System.Drawing.Point(83, 46);
            this.SmallCargoCheckBox.Name = "SmallCargoCheckBox";
            this.SmallCargoCheckBox.Size = new System.Drawing.Size(86, 19);
            this.SmallCargoCheckBox.TabIndex = 5;
            this.SmallCargoCheckBox.Tag = "小型貨物";
            this.SmallCargoCheckBox.Text = "小型貨物";
            this.SmallCargoCheckBox.UseVisualStyleBackColor = true;
            // 
            // SmallRidingCheckBox
            // 
            this.SmallRidingCheckBox.AutoSize = true;
            this.SmallRidingCheckBox.Location = new System.Drawing.Point(83, 21);
            this.SmallRidingCheckBox.Name = "SmallRidingCheckBox";
            this.SmallRidingCheckBox.Size = new System.Drawing.Size(86, 19);
            this.SmallRidingCheckBox.TabIndex = 1;
            this.SmallRidingCheckBox.Tag = "小型乗用";
            this.SmallRidingCheckBox.Text = "小型乗用";
            this.SmallRidingCheckBox.UseVisualStyleBackColor = true;
            // 
            // MiddleRidingCheckBox
            // 
            this.MiddleRidingCheckBox.AutoSize = true;
            this.MiddleRidingCheckBox.Location = new System.Drawing.Point(261, 21);
            this.MiddleRidingCheckBox.Name = "MiddleRidingCheckBox";
            this.MiddleRidingCheckBox.Size = new System.Drawing.Size(86, 19);
            this.MiddleRidingCheckBox.TabIndex = 3;
            this.MiddleRidingCheckBox.Tag = "中間乗用";
            this.MiddleRidingCheckBox.Text = "中間乗用";
            this.MiddleRidingCheckBox.UseVisualStyleBackColor = true;
            // 
            // LightCargoCheckBox
            // 
            this.LightCargoCheckBox.AutoSize = true;
            this.LightCargoCheckBox.Location = new System.Drawing.Point(6, 46);
            this.LightCargoCheckBox.Name = "LightCargoCheckBox";
            this.LightCargoCheckBox.Size = new System.Drawing.Size(71, 19);
            this.LightCargoCheckBox.TabIndex = 4;
            this.LightCargoCheckBox.Tag = "軽貨物";
            this.LightCargoCheckBox.Text = "軽貨物";
            this.LightCargoCheckBox.UseVisualStyleBackColor = true;
            // 
            // LightRidingCheckBox
            // 
            this.LightRidingCheckBox.AutoSize = true;
            this.LightRidingCheckBox.Location = new System.Drawing.Point(6, 21);
            this.LightRidingCheckBox.Name = "LightRidingCheckBox";
            this.LightRidingCheckBox.Size = new System.Drawing.Size(71, 19);
            this.LightRidingCheckBox.TabIndex = 0;
            this.LightRidingCheckBox.Tag = "軽乗用";
            this.LightRidingCheckBox.Text = "軽乗用";
            this.LightRidingCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 15);
            this.label1.TabIndex = 1016;
            this.label1.Text = "●車検リストに載せる対象を設定してください。";
            // 
            // SetButton
            // 
            this.SetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SetButton.BackColor = System.Drawing.SystemColors.Control;
            this.SetButton.Location = new System.Drawing.Point(5, 242);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(120, 30);
            this.SetButton.TabIndex = 1;
            this.SetButton.Text = "設定";
            this.SetButton.UseVisualStyleBackColor = false;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // ExtractionConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(374, 276);
            this.Controls.Add(this.SetButton);
            this.Name = "ExtractionConditionForm";
            this.Text = "タイトルが設定されていません";
            this.Load += new System.EventHandler(this.ExtractionConditionForm_Load);
            this.Controls.SetChildIndex(this.SetButton, 0);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.InspectionGroupBox.ResumeLayout(false);
            this.InspectionGroupBox.PerformLayout();
            this.CarGroupGroupBox.ResumeLayout(false);
            this.CarGroupGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox SearchCheckBox;
        private System.Windows.Forms.GroupBox InspectionGroupBox;
        private System.Windows.Forms.CheckBox CarInspectionCheckBox;
        private System.Windows.Forms.CheckBox Inspection12CheckBox;
        private System.Windows.Forms.CheckBox Inspection6CheckBox;
        private System.Windows.Forms.GroupBox CarGroupGroupBox;
        private System.Windows.Forms.CheckBox NoneCheckBox;
        private System.Windows.Forms.CheckBox LargeCargoCheckBox;
        private System.Windows.Forms.CheckBox NormalRidingCheckBox;
        private System.Windows.Forms.CheckBox SmallCargoCheckBox;
        private System.Windows.Forms.CheckBox SmallRidingCheckBox;
        private System.Windows.Forms.CheckBox LightCargoCheckBox;
        private System.Windows.Forms.CheckBox LightRidingCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SetButton;
        private System.Windows.Forms.CheckBox MiddleRidingCheckBox;
    }
}
