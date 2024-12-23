namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    partial class RegularMailDetailForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RegularMailDetailMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.regularMailDetailMultiRowTemplate1 = new DevPlan.Presentation.UIDevPlan.TruckSchedule.RegularMailDetailMultiRowTemplate();
            this.label3 = new System.Windows.Forms.Label();
            this.LoadButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TypeComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label27 = new System.Windows.Forms.Label();
            this.MailContentTextBox = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.MailTitleTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CarNameLabel = new System.Windows.Forms.Label();
            this.EntryButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegularMailDetailMultiRow)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(904, 470);
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
            this.ListFormMainPanel.Controls.Add(this.groupBox1);
            this.ListFormMainPanel.Controls.Add(this.LoadButton);
            this.ListFormMainPanel.Controls.Add(this.label2);
            this.ListFormMainPanel.Controls.Add(this.TypeComboBox);
            this.ListFormMainPanel.Controls.Add(this.tableLayoutPanel1);
            this.ListFormMainPanel.Size = new System.Drawing.Size(1019, 460);
            this.ListFormMainPanel.TabIndex = 0;
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.TypeComboBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label2, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.LoadButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.groupBox1, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RegularMailDetailMultiRow);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(834, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 400);
            this.groupBox1.TabIndex = 1023;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "特殊文字";
            // 
            // RegularMailDetailMultiRow
            // 
            this.RegularMailDetailMultiRow.AllowAutoExtend = true;
            this.RegularMailDetailMultiRow.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.RegularMailDetailMultiRow.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.RegularMailDetailMultiRow.Location = new System.Drawing.Point(9, 141);
            this.RegularMailDetailMultiRow.Name = "RegularMailDetailMultiRow";
            this.RegularMailDetailMultiRow.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.RegularMailDetailMultiRow.Size = new System.Drawing.Size(159, 252);
            this.RegularMailDetailMultiRow.TabIndex = 1;
            this.RegularMailDetailMultiRow.Template = this.regularMailDetailMultiRowTemplate1;
            this.RegularMailDetailMultiRow.Text = "gcMultiRow1";
            this.RegularMailDetailMultiRow.EditingControlShowing += new System.EventHandler<GrapeCity.Win.MultiRow.EditingControlShowingEventArgs>(this.RegularMailDetailMultiRow_EditingControlShowing);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 122);
            this.label3.TabIndex = 0;
            this.label3.Text = "下記特殊文字は、メール送信時それぞれのデータに置き換わります。\r\n\r\nメール本文に積込日等のデータを取り込みたい時にご利用下さい。";
            // 
            // LoadButton
            // 
            this.LoadButton.BackColor = System.Drawing.SystemColors.Control;
            this.LoadButton.Location = new System.Drawing.Point(236, 45);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "再読込";
            this.LoadButton.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1021;
            this.label2.Text = "予約種別";
            // 
            // TypeComboBox
            // 
            this.TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeComboBox.FormattingEnabled = true;
            this.TypeComboBox.Location = new System.Drawing.Point(79, 44);
            this.TypeComboBox.Name = "TypeComboBox";
            this.TypeComboBox.Size = new System.Drawing.Size(141, 23);
            this.TypeComboBox.TabIndex = 0;
            this.TypeComboBox.Tag = "ItemName(予約種別)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label27, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.MailContentTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label26, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.MailTitleTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CarNameLabel, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 74);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(827, 374);
            this.tableLayoutPanel1.TabIndex = 1019;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Aquamarine;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Location = new System.Drawing.Point(1, 63);
            this.label27.Margin = new System.Windows.Forms.Padding(0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(150, 310);
            this.label27.TabIndex = 0;
            this.label27.Text = "本文";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MailContentTextBox
            // 
            this.MailContentTextBox.Location = new System.Drawing.Point(155, 66);
            this.MailContentTextBox.Multiline = true;
            this.MailContentTextBox.Name = "MailContentTextBox";
            this.MailContentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MailContentTextBox.Size = new System.Drawing.Size(668, 304);
            this.MailContentTextBox.TabIndex = 1;
            this.MailContentTextBox.Tag = "Required;ItemName(本文)";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Aquamarine;
            this.label26.Location = new System.Drawing.Point(1, 32);
            this.label26.Margin = new System.Windows.Forms.Padding(0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(150, 30);
            this.label26.TabIndex = 0;
            this.label26.Text = "件名";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MailTitleTextBox
            // 
            this.MailTitleTextBox.Location = new System.Drawing.Point(155, 35);
            this.MailTitleTextBox.Name = "MailTitleTextBox";
            this.MailTitleTextBox.Size = new System.Drawing.Size(668, 22);
            this.MailTitleTextBox.TabIndex = 0;
            this.MailTitleTextBox.Tag = "Required;Byte(100);ItemName(件名)";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "車両名";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CarNameLabel
            // 
            this.CarNameLabel.AutoSize = true;
            this.CarNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CarNameLabel.Location = new System.Drawing.Point(155, 1);
            this.CarNameLabel.Name = "CarNameLabel";
            this.CarNameLabel.Size = new System.Drawing.Size(668, 30);
            this.CarNameLabel.TabIndex = 1016;
            this.CarNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EntryButton
            // 
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Location = new System.Drawing.Point(5, 469);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 30);
            this.EntryButton.TabIndex = 1;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // RegularMailDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1030, 504);
            this.Controls.Add(this.EntryButton);
            this.Name = "RegularMailDetailForm";
            this.Text = "RegularMailDetailForm";
            this.Load += new System.EventHandler(this.RegularMailDetailForm_Load);
            this.Controls.SetChildIndex(this.ListFormMainPanel, 0);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RegularMailDetailMultiRow)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private GrapeCity.Win.MultiRow.GcMultiRow RegularMailDetailMultiRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox TypeComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox MailContentTextBox;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox MailTitleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CarNameLabel;
        private System.Windows.Forms.Button EntryButton;
        private RegularMailDetailMultiRowTemplate regularMailDetailMultiRowTemplate1;
    }
}