namespace DevPlan.Presentation.Common
{
    partial class ScheduleItemGeneralCodeMoveForm<item>
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
            this.EntryButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CarGroupBox = new System.Windows.Forms.GroupBox();
            this.UnderDevelopmentCheckBox = new System.Windows.Forms.CheckBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.ScheduleItemMoveMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.generalCodeListMultiRowTemplate1 = new DevPlan.Presentation.GeneralCodeListMultiRowTemplate();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.CarGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleItemMoveMultiRow)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(293, 459);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Size = new System.Drawing.Size(408, 449);
            // 
            // EntryButton
            // 
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 459);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 1014;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 15);
            this.label2.TabIndex = 1016;
            this.label2.Text = "移動先の車種を選択してください。";
            // 
            // CarGroupBox
            // 
            this.CarGroupBox.Controls.Add(this.UnderDevelopmentCheckBox);
            this.CarGroupBox.Controls.Add(this.MessageLabel);
            this.CarGroupBox.Controls.Add(this.ScheduleItemMoveMultiRow);
            this.CarGroupBox.Location = new System.Drawing.Point(13, 79);
            this.CarGroupBox.Name = "CarGroupBox";
            this.CarGroupBox.Size = new System.Drawing.Size(393, 359);
            this.CarGroupBox.TabIndex = 1015;
            this.CarGroupBox.TabStop = false;
            this.CarGroupBox.Text = "車種";
            // 
            // UnderDevelopmentCheckBox
            // 
            this.UnderDevelopmentCheckBox.AutoSize = true;
            this.UnderDevelopmentCheckBox.Checked = true;
            this.UnderDevelopmentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UnderDevelopmentCheckBox.Location = new System.Drawing.Point(6, 21);
            this.UnderDevelopmentCheckBox.Name = "UnderDevelopmentCheckBox";
            this.UnderDevelopmentCheckBox.Size = new System.Drawing.Size(136, 19);
            this.UnderDevelopmentCheckBox.TabIndex = 1012;
            this.UnderDevelopmentCheckBox.Text = "開発中も表示する";
            this.UnderDevelopmentCheckBox.UseVisualStyleBackColor = true;
            this.UnderDevelopmentCheckBox.CheckedChanged += new System.EventHandler(this.UnderDevelopmentCheckBox_CheckedChanged);
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(6, 333);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(209, 15);
            this.MessageLabel.TabIndex = 1013;
            this.MessageLabel.Text = "日程、作業履歴({0})も移動します";
            // 
            // ScheduleItemMoveMultiRow
            // 
            this.ScheduleItemMoveMultiRow.AllowAutoExtend = true;
            this.ScheduleItemMoveMultiRow.AllowClipboard = false;
            this.ScheduleItemMoveMultiRow.AllowMouseWheelAutoScrolling = false;
            this.ScheduleItemMoveMultiRow.AllowUserToAddRows = false;
            this.ScheduleItemMoveMultiRow.AllowUserToDeleteRows = false;
            this.ScheduleItemMoveMultiRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScheduleItemMoveMultiRow.ClipboardCopyMode = GrapeCity.Win.MultiRow.ClipboardCopyMode.Disable;
            this.ScheduleItemMoveMultiRow.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.ScheduleItemMoveMultiRow.Location = new System.Drawing.Point(9, 46);
            this.ScheduleItemMoveMultiRow.MultiSelect = false;
            this.ScheduleItemMoveMultiRow.Name = "ScheduleItemMoveMultiRow";
            this.ScheduleItemMoveMultiRow.Size = new System.Drawing.Size(378, 282);
            this.ScheduleItemMoveMultiRow.TabIndex = 1011;
            this.ScheduleItemMoveMultiRow.Text = "gcMultiRow1";
            this.ScheduleItemMoveMultiRow.VerticalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.ScheduleItemMoveMultiRow.VerticalScrollMode = GrapeCity.Win.MultiRow.ScrollMode.Pixel;
            this.ScheduleItemMoveMultiRow.CellContentClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.ScheduleItemMoveMultiRow_CellContentClick);
            // 
            // ScheduleItemGeneralCodeMoveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(419, 493);
            this.Controls.Add(this.EntryButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CarGroupBox);
            this.Name = "ScheduleItemGeneralCodeMoveForm";
            this.Text = "ScheduleItemGeneralCodeMoveForm";
            this.Load += new System.EventHandler(this.ScheduleItemGeneralCodeMoveForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CarGroupBox, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.CarGroupBox.ResumeLayout(false);
            this.CarGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleItemMoveMultiRow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox CarGroupBox;
        private System.Windows.Forms.CheckBox UnderDevelopmentCheckBox;
        private System.Windows.Forms.Label MessageLabel;
        private GrapeCity.Win.MultiRow.GcMultiRow ScheduleItemMoveMultiRow;
        private GeneralCodeListMultiRowTemplate generalCodeListMultiRowTemplate1;
    }
}