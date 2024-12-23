namespace DevPlan.Presentation.Common
{
    partial class GeneralCodeListForm
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager1 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GeneralCodeTextBox = new System.Windows.Forms.TextBox();
            this.CarGroupTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ListSearchButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.GeneralCodeListMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.SearchResultLabel = new System.Windows.Forms.Label();
            this.RowCountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).BeginInit();
            this.ListFormMainPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GeneralCodeListMultiRow)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(280, 466);
            this.CloseButton.TabIndex = 4;
            // 
            // ListFormTitleLabel
            // 
            this.ListFormTitleLabel.Size = new System.Drawing.Size(240, 19);
            this.ListFormTitleLabel.Text = "タイトルが設定されていません";
            this.ListFormTitleLabel.UseMnemonic = false;
            // 
            // ListFormMainPanel
            // 
            this.ListFormMainPanel.Controls.Add(this.RowCountLabel);
            this.ListFormMainPanel.Controls.Add(this.SearchResultLabel);
            this.ListFormMainPanel.Controls.Add(this.GeneralCodeListMultiRow);
            this.ListFormMainPanel.Controls.Add(this.label3);
            this.ListFormMainPanel.Controls.Add(this.ListSearchButton);
            this.ListFormMainPanel.Controls.Add(this.tableLayoutPanel1);
            this.ListFormMainPanel.Size = new System.Drawing.Size(395, 455);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormPictureBox, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListFormTitleLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.ListSearchButton, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.label3, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.GeneralCodeListMultiRow, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.SearchResultLabel, 0);
            this.ListFormMainPanel.Controls.SetChildIndex(this.RowCountLabel, 0);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.39F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.61F));
            this.tableLayoutPanel1.Controls.Add(this.GeneralCodeTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.CarGroupTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 61);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(387, 65);
            this.tableLayoutPanel1.TabIndex = 1011;
            // 
            // GeneralCodeTextBox
            // 
            this.GeneralCodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralCodeTextBox.Location = new System.Drawing.Point(121, 36);
            this.GeneralCodeTextBox.MaxLength = 15;
            this.GeneralCodeTextBox.Name = "GeneralCodeTextBox";
            this.GeneralCodeTextBox.Size = new System.Drawing.Size(262, 22);
            this.GeneralCodeTextBox.TabIndex = 2;
            // 
            // CarGroupTextBox
            // 
            this.CarGroupTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CarGroupTextBox.Location = new System.Drawing.Point(121, 4);
            this.CarGroupTextBox.MaxLength = 10;
            this.CarGroupTextBox.Name = "CarGroupTextBox";
            this.CarGroupTextBox.Size = new System.Drawing.Size(262, 22);
            this.CarGroupTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(1, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 31);
            this.label2.TabIndex = 1012;
            this.label2.Text = "開発符号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 31);
            this.label1.TabIndex = 1012;
            this.label1.Text = "車系";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListSearchButton
            // 
            this.ListSearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.ListSearchButton.Location = new System.Drawing.Point(3, 132);
            this.ListSearchButton.Name = "ListSearchButton";
            this.ListSearchButton.Size = new System.Drawing.Size(75, 23);
            this.ListSearchButton.TabIndex = 3;
            this.ListSearchButton.Text = "検索";
            this.ListSearchButton.UseVisualStyleBackColor = false;
            this.ListSearchButton.Click += new System.EventHandler(this.ListSearchButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 1014;
            this.label3.Text = "検索条件";
            // 
            // GeneralCodeListMultiRow
            // 
            this.GeneralCodeListMultiRow.AllowAutoExtend = true;
            this.GeneralCodeListMultiRow.AllowClipboard = false;
            this.GeneralCodeListMultiRow.AllowUserToAddRows = false;
            this.GeneralCodeListMultiRow.AllowUserToDeleteRows = false;
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            this.GeneralCodeListMultiRow.AlternatingRowsDefaultCellStyle = cellStyle1;
            this.GeneralCodeListMultiRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GeneralCodeListMultiRow.ClipboardCopyMode = GrapeCity.Win.MultiRow.ClipboardCopyMode.Disable;
            cellStyle2.BackColor = System.Drawing.SystemColors.Control;
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle2.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle2.Padding = new System.Windows.Forms.Padding(0);
            cellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle2.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.GeneralCodeListMultiRow.ColumnHeadersDefaultCellStyle = cellStyle2;
            cellStyle3.BackColor = System.Drawing.SystemColors.Control;
            cellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle3.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            cellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle3.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.GeneralCodeListMultiRow.ColumnHeadersDefaultHeaderCellStyle = cellStyle3;
            cellStyle4.BackColor = System.Drawing.SystemColors.Window;
            cellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle4.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            cellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle4.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.GeneralCodeListMultiRow.DefaultCellStyle = cellStyle4;
            this.GeneralCodeListMultiRow.FreezeLines = new GrapeCity.Win.MultiRow.FreezeLines(new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.None, System.Drawing.Color.Empty));
            this.GeneralCodeListMultiRow.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.GeneralCodeListMultiRow.Location = new System.Drawing.Point(3, 161);
            this.GeneralCodeListMultiRow.MultiSelect = false;
            this.GeneralCodeListMultiRow.Name = "GeneralCodeListMultiRow";
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            this.GeneralCodeListMultiRow.RowsDefaultCellStyle = cellStyle5;
            cellStyle6.BackColor = System.Drawing.SystemColors.Control;
            cellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle6.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle6.Padding = new System.Windows.Forms.Padding(0);
            cellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle6.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.GeneralCodeListMultiRow.RowsDefaultHeaderCellStyle = cellStyle6;
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftLeft)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftRight)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.EndEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CancelEdit)), System.Windows.Forms.Keys.Escape));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ComponentActions.SelectPreviousControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ComponentActions.SelectNextControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ReverseSelectCurrentRow)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToPreviousPage)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToNextPage)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.EndEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CancelEdit)), System.Windows.Forms.Keys.Escape));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            this.GeneralCodeListMultiRow.ShortcutKeyManager = shortcutKeyManager1;
            this.GeneralCodeListMultiRow.Size = new System.Drawing.Size(387, 289);
            this.GeneralCodeListMultiRow.TabIndex = 1013;
            this.GeneralCodeListMultiRow.Text = "gcMultiRow1";
            this.GeneralCodeListMultiRow.VerticalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.GeneralCodeListMultiRow.VerticalScrollMode = GrapeCity.Win.MultiRow.ScrollMode.Pixel;
            // 
            // SearchResultLabel
            // 
            this.SearchResultLabel.AutoSize = true;
            this.SearchResultLabel.ForeColor = System.Drawing.Color.Red;
            this.SearchResultLabel.Location = new System.Drawing.Point(87, 142);
            this.SearchResultLabel.Name = "SearchResultLabel";
            this.SearchResultLabel.Size = new System.Drawing.Size(0, 15);
            this.SearchResultLabel.TabIndex = 1018;
            // 
            // RowCountLabel
            // 
            this.RowCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RowCountLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.5F);
            this.RowCountLabel.Location = new System.Drawing.Point(219, 141);
            this.RowCountLabel.Name = "RowCountLabel";
            this.RowCountLabel.Size = new System.Drawing.Size(171, 18);
            this.RowCountLabel.TabIndex = 1029;
            this.RowCountLabel.Text = "表示件数： 99999/99999 件";
            this.RowCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GeneralCodeListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(404, 498);
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "GeneralCodeListForm";
            this.Text = "GeneralCodeListForm";
            this.Load += new System.EventHandler(this.GeneralCodeListForm_Load);
            this.Shown += new System.EventHandler(this.GeneralCodeListForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ListFormPictureBox)).EndInit();
            this.ListFormMainPanel.ResumeLayout(false);
            this.ListFormMainPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GeneralCodeListMultiRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ListSearchButton;
        private System.Windows.Forms.TextBox GeneralCodeTextBox;
        private System.Windows.Forms.TextBox CarGroupTextBox;
        private System.Windows.Forms.Label label3;
        private GrapeCity.Win.MultiRow.GcMultiRow GeneralCodeListMultiRow;
        private System.Windows.Forms.Label SearchResultLabel;
        private System.Windows.Forms.Label RowCountLabel;
    }
}