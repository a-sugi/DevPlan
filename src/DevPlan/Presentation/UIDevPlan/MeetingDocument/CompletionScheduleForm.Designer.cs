namespace DevPlan.Presentation.UIDevPlan.MeetingDocument
{
    partial class CompletionScheduleForm
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
            this.label11 = new System.Windows.Forms.Label();
            this.CompletionScheduleDateTimePicker = new DevPlan.Presentation.UC.NullableDateTimePicker();
            this.DetailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.EntryButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompletionScheduleDateTimePicker)).BeginInit();
            this.DetailTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(158, 87);
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.DetailTableLayoutPanel);
            this.ListFormMainPanel.Size = new System.Drawing.Size(273, 77);
            this.ListFormMainPanel.Controls.SetChildIndex(this.DetailTableLayoutPanel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Aquamarine;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(1, 1);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 30);
            this.label11.TabIndex = 18;
            this.label11.Text = "確認完了日";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CompletionScheduleDateTimePicker
            // 
            this.CompletionScheduleDateTimePicker.CustomFormat = "yyyy/MM/dd";
            this.CompletionScheduleDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompletionScheduleDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.CompletionScheduleDateTimePicker.Location = new System.Drawing.Point(125, 4);
            this.CompletionScheduleDateTimePicker.Name = "CompletionScheduleDateTimePicker";
            this.CompletionScheduleDateTimePicker.Size = new System.Drawing.Size(133, 22);
            this.CompletionScheduleDateTimePicker.TabIndex = 3;
            this.CompletionScheduleDateTimePicker.Tag = "Required;ItemName(確認完了日)";
            this.CompletionScheduleDateTimePicker.Value = new System.DateTime(2017, 4, 21, 0, 0, 0, 0);
            // 
            // DetailTableLayoutPanel
            // 
            this.DetailTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DetailTableLayoutPanel.ColumnCount = 2;
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.DetailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DetailTableLayoutPanel.Controls.Add(this.CompletionScheduleDateTimePicker, 0, 0);
            this.DetailTableLayoutPanel.Controls.Add(this.label11, 0, 0);
            this.DetailTableLayoutPanel.Location = new System.Drawing.Point(6, 39);
            this.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel";
            this.DetailTableLayoutPanel.RowCount = 1;
            this.DetailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DetailTableLayoutPanel.Size = new System.Drawing.Size(262, 32);
            this.DetailTableLayoutPanel.TabIndex = 1013;
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 87);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 1010;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // CompletionScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 121);
            this.Controls.Add(this.EntryButton);
            this.Name = "CompletionScheduleForm";
            this.Text = "CompletionScheduleForm";
            this.Load += new System.EventHandler(this.CompletionScheduleForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompletionScheduleDateTimePicker)).EndInit();
            this.DetailTableLayoutPanel.ResumeLayout(false);
            this.DetailTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel DetailTableLayoutPanel;
        private UC.NullableDateTimePicker CompletionScheduleDateTimePicker;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button EntryButton;
    }
}