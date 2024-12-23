namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    partial class DesignCheckDetailListForm
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
            this.components = new System.ComponentModel.Container();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager2 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            this.DocumentTypeTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenDayLabel = new System.Windows.Forms.Label();
            this.DesignCheckNameLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.RowAddButton = new System.Windows.Forms.Button();
            this.RowDeleteButton = new System.Windows.Forms.Button();
            this.EntryButton = new System.Windows.Forms.Button();
            this.DesignCheckUserListButton = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.PointMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.ListDataLabel = new System.Windows.Forms.Label();
            this.CompletionScheduleContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DayInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DayCompletionInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SituationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Copybutton = new System.Windows.Forms.Button();
            this.RowCountLabel = new System.Windows.Forms.Label();
            this.PictNotice = new System.Windows.Forms.PictureBox();
            this.MsgPicture = new System.Windows.Forms.PictureBox();
            this.AutoFitLinkLabel = new System.Windows.Forms.LinkLabel();
            this.ContentsPanel.SuspendLayout();
            this.DocumentTypeTableLayoutPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PointMultiRow)).BeginInit();
            this.CompletionScheduleContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictNotice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MsgPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.TabIndex = 100;
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.AutoFitLinkLabel);
            this.ContentsPanel.Controls.Add(this.MainPanel);
            this.ContentsPanel.Controls.Add(this.MsgPicture);
            this.ContentsPanel.Controls.Add(this.RowCountLabel);
            this.ContentsPanel.Controls.Add(this.Copybutton);
            this.ContentsPanel.Controls.Add(this.ListDataLabel);
            this.ContentsPanel.Controls.Add(this.RowDeleteButton);
            this.ContentsPanel.Controls.Add(this.RowAddButton);
            this.ContentsPanel.Controls.Add(this.SearchButton);
            this.ContentsPanel.Controls.Add(this.DocumentTypeTableLayoutPanel);
            this.ContentsPanel.Controls.Add(this.PictNotice);
            this.ContentsPanel.TabIndex = 0;
            // 
            // DocumentTypeTableLayoutPanel
            // 
            this.DocumentTypeTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DocumentTypeTableLayoutPanel.ColumnCount = 4;
            this.DocumentTypeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.DocumentTypeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.DocumentTypeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.DocumentTypeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DocumentTypeTableLayoutPanel.Controls.Add(this.label2, 2, 0);
            this.DocumentTypeTableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.DocumentTypeTableLayoutPanel.Controls.Add(this.OpenDayLabel, 1, 0);
            this.DocumentTypeTableLayoutPanel.Controls.Add(this.DesignCheckNameLabel, 3, 0);
            this.DocumentTypeTableLayoutPanel.Location = new System.Drawing.Point(10, 13);
            this.DocumentTypeTableLayoutPanel.Name = "DocumentTypeTableLayoutPanel";
            this.DocumentTypeTableLayoutPanel.RowCount = 1;
            this.DocumentTypeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DocumentTypeTableLayoutPanel.Size = new System.Drawing.Size(475, 33);
            this.DocumentTypeTableLayoutPanel.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Aquamarine;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(163, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "設計チェック名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "開催日";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OpenDayLabel
            // 
            this.OpenDayLabel.AutoSize = true;
            this.OpenDayLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenDayLabel.Location = new System.Drawing.Point(65, 1);
            this.OpenDayLabel.Name = "OpenDayLabel";
            this.OpenDayLabel.Size = new System.Drawing.Size(94, 31);
            this.OpenDayLabel.TabIndex = 4;
            this.OpenDayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DesignCheckNameLabel
            // 
            this.DesignCheckNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DesignCheckNameLabel.Location = new System.Drawing.Point(267, 1);
            this.DesignCheckNameLabel.Name = "DesignCheckNameLabel";
            this.DesignCheckNameLabel.Size = new System.Drawing.Size(204, 31);
            this.DesignCheckNameLabel.TabIndex = 5;
            this.DesignCheckNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(10, 52);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 30);
            this.SearchButton.TabIndex = 10;
            this.SearchButton.Text = "検索/更新";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // RowAddButton
            // 
            this.RowAddButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowAddButton.Location = new System.Drawing.Point(136, 52);
            this.RowAddButton.Name = "RowAddButton";
            this.RowAddButton.Size = new System.Drawing.Size(120, 30);
            this.RowAddButton.TabIndex = 20;
            this.RowAddButton.Text = "指摘追加";
            this.RowAddButton.UseVisualStyleBackColor = false;
            this.RowAddButton.Click += new System.EventHandler(this.RowAddButton_Click);
            // 
            // RowDeleteButton
            // 
            this.RowDeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.RowDeleteButton.Location = new System.Drawing.Point(388, 52);
            this.RowDeleteButton.Name = "RowDeleteButton";
            this.RowDeleteButton.Size = new System.Drawing.Size(120, 30);
            this.RowDeleteButton.TabIndex = 40;
            this.RowDeleteButton.Text = "指摘削除";
            this.RowDeleteButton.UseVisualStyleBackColor = false;
            this.RowDeleteButton.Click += new System.EventHandler(this.RowDeleteButton_Click);
            // 
            // EntryButton
            // 
            this.EntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EntryButton.BackColor = System.Drawing.SystemColors.Control;
            this.EntryButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.EntryButton.Location = new System.Drawing.Point(12, 616);
            this.EntryButton.Name = "EntryButton";
            this.EntryButton.Size = new System.Drawing.Size(120, 20);
            this.EntryButton.TabIndex = 70;
            this.EntryButton.Text = "登録";
            this.EntryButton.UseVisualStyleBackColor = false;
            this.EntryButton.Click += new System.EventHandler(this.EntryButton_Click);
            // 
            // DesignCheckUserListButton
            // 
            this.DesignCheckUserListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DesignCheckUserListButton.BackColor = System.Drawing.SystemColors.Control;
            this.DesignCheckUserListButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.DesignCheckUserListButton.Location = new System.Drawing.Point(138, 616);
            this.DesignCheckUserListButton.Name = "DesignCheckUserListButton";
            this.DesignCheckUserListButton.Size = new System.Drawing.Size(120, 20);
            this.DesignCheckUserListButton.TabIndex = 80;
            this.DesignCheckUserListButton.Text = "参加者登録";
            this.DesignCheckUserListButton.UseVisualStyleBackColor = false;
            this.DesignCheckUserListButton.Click += new System.EventHandler(this.DesignCheckUserListButton_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel.Controls.Add(this.PointMultiRow);
            this.MainPanel.Location = new System.Drawing.Point(0, 88);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1184, 493);
            this.MainPanel.TabIndex = 50;
            // 
            // PointMultiRow
            // 
            this.PointMultiRow.AllowAutoExtend = true;
            this.PointMultiRow.AllowUserToAddRows = false;
            this.PointMultiRow.AllowUserToDeleteRows = false;
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            this.PointMultiRow.AlternatingRowsDefaultCellStyle = cellStyle7;
            this.PointMultiRow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PointMultiRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            cellStyle8.BackColor = System.Drawing.SystemColors.Control;
            cellStyle8.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle8.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle8.Padding = new System.Windows.Forms.Padding(0);
            cellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle8.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.PointMultiRow.ColumnHeadersDefaultCellStyle = cellStyle8;
            cellStyle9.BackColor = System.Drawing.SystemColors.Control;
            cellStyle9.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle9.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle9.Padding = new System.Windows.Forms.Padding(0);
            cellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle9.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.PointMultiRow.ColumnHeadersDefaultHeaderCellStyle = cellStyle9;
            cellStyle10.BackColor = System.Drawing.SystemColors.Window;
            cellStyle10.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle10.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle10.Padding = new System.Windows.Forms.Padding(0);
            cellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle10.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            cellStyle10.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.PointMultiRow.DefaultCellStyle = cellStyle10;
            this.PointMultiRow.FreezeLines = new GrapeCity.Win.MultiRow.FreezeLines(new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.None, System.Drawing.Color.Empty));
            this.PointMultiRow.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.PointMultiRow.Location = new System.Drawing.Point(9, 3);
            this.PointMultiRow.MultiSelect = false;
            this.PointMultiRow.Name = "PointMultiRow";
            cellStyle11.Padding = new System.Windows.Forms.Padding(0);
            this.PointMultiRow.RowsDefaultCellStyle = cellStyle11;
            cellStyle12.BackColor = System.Drawing.SystemColors.Control;
            cellStyle12.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle12.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle12.Padding = new System.Windows.Forms.Padding(0);
            cellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle12.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle12.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.PointMultiRow.RowsDefaultHeaderCellStyle = cellStyle12;
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftLeft)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftRight)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.EndEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CancelEdit)), System.Windows.Forms.Keys.Escape));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ComponentActions.SelectPreviousControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ComponentActions.SelectNextControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ReverseSelectCurrentRow)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToPreviousPage)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToNextPage)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.EndEdit)), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CancelEdit)), System.Windows.Forms.Keys.Escape));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCellThenControl)), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCellThenControl)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Return));
            this.PointMultiRow.ShortcutKeyManager = shortcutKeyManager2;
            this.PointMultiRow.Size = new System.Drawing.Size(1168, 487);
            this.PointMultiRow.TabIndex = 60;
            this.PointMultiRow.Tag = "";
            this.PointMultiRow.Text = "gcMultiRow1";
            this.PointMultiRow.VerticalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.PointMultiRow.VerticalScrollMode = GrapeCity.Win.MultiRow.ScrollMode.Pixel;
            this.PointMultiRow.DataError += new System.EventHandler<GrapeCity.Win.MultiRow.DataErrorEventArgs>(this.PointMultiRow_DataError);
            this.PointMultiRow.CellValueChanged += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.PointMultiRow_CellValueChanged);
            this.PointMultiRow.CellBeginEdit += new System.EventHandler<GrapeCity.Win.MultiRow.CellBeginEditEventArgs>(this.PointMultiRow_CellBeginEdit);
            this.PointMultiRow.CellEndEdit += new System.EventHandler<GrapeCity.Win.MultiRow.CellEndEditEventArgs>(this.PointMultiRow_CellEndEdit);
            this.PointMultiRow.CellMouseEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.PointMultiRow_CellMouseEnter);
            this.PointMultiRow.CellMouseLeave += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.PointMultiRow_CellMouseLeave);
            this.PointMultiRow.CellMouseDown += new System.EventHandler<GrapeCity.Win.MultiRow.CellMouseEventArgs>(this.PointMultiRow_CellMouseDown);
            this.PointMultiRow.CellEditedFormattedValueChanged += new System.EventHandler<GrapeCity.Win.MultiRow.CellEditedFormattedValueChangedEventArgs>(this.PointMultiRow_CellEditedFormattedValueChanged);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadButton.BackColor = System.Drawing.SystemColors.Control;
            this.DownloadButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.DownloadButton.Location = new System.Drawing.Point(926, 616);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(120, 20);
            this.DownloadButton.TabIndex = 90;
            this.DownloadButton.Text = "Excel出力";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // ListDataLabel
            // 
            this.ListDataLabel.AutoSize = true;
            this.ListDataLabel.ForeColor = System.Drawing.Color.Red;
            this.ListDataLabel.Location = new System.Drawing.Point(491, 23);
            this.ListDataLabel.Name = "ListDataLabel";
            this.ListDataLabel.Size = new System.Drawing.Size(0, 15);
            this.ListDataLabel.TabIndex = 1023;
            // 
            // CompletionScheduleContextMenuStrip
            // 
            this.CompletionScheduleContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DayInputToolStripMenuItem,
            this.DayCompletionInputToolStripMenuItem});
            this.CompletionScheduleContextMenuStrip.Name = "CompletionScheduleContextMenuStrip";
            this.CompletionScheduleContextMenuStrip.Size = new System.Drawing.Size(167, 48);
            // 
            // DayInputToolStripMenuItem
            // 
            this.DayInputToolStripMenuItem.Name = "DayInputToolStripMenuItem";
            this.DayInputToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DayInputToolStripMenuItem.Text = "日付入力";
            // 
            // DayCompletionInputToolStripMenuItem
            // 
            this.DayCompletionInputToolStripMenuItem.Name = "DayCompletionInputToolStripMenuItem";
            this.DayCompletionInputToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DayCompletionInputToolStripMenuItem.Text = "日付付き済み入力";
            // 
            // SituationToolTip
            // 
            this.SituationToolTip.AutoPopDelay = 2147483647;
            this.SituationToolTip.InitialDelay = 0;
            this.SituationToolTip.ReshowDelay = 0;
            this.SituationToolTip.ShowAlways = true;
            // 
            // Copybutton
            // 
            this.Copybutton.BackColor = System.Drawing.SystemColors.Control;
            this.Copybutton.Location = new System.Drawing.Point(262, 52);
            this.Copybutton.Name = "Copybutton";
            this.Copybutton.Size = new System.Drawing.Size(120, 30);
            this.Copybutton.TabIndex = 30;
            this.Copybutton.Text = "指摘コピー";
            this.Copybutton.UseVisualStyleBackColor = false;
            this.Copybutton.Click += new System.EventHandler(this.Copybutton_Click);
            // 
            // RowCountLabel
            // 
            this.RowCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RowCountLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.5F);
            this.RowCountLabel.Location = new System.Drawing.Point(1032, 67);
            this.RowCountLabel.Name = "RowCountLabel";
            this.RowCountLabel.Size = new System.Drawing.Size(145, 18);
            this.RowCountLabel.TabIndex = 1032;
            this.RowCountLabel.Text = "表示件数： 999/999 件";
            this.RowCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PictNotice
            // 
            this.PictNotice.Location = new System.Drawing.Point(505, 49);
            this.PictNotice.Name = "PictNotice";
            this.PictNotice.Size = new System.Drawing.Size(232, 32);
            this.PictNotice.TabIndex = 1034;
            this.PictNotice.TabStop = false;
            // 
            // MsgPicture
            // 
            this.MsgPicture.BackColor = System.Drawing.Color.Transparent;
            this.MsgPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MsgPicture.Location = new System.Drawing.Point(677, 9);
            this.MsgPicture.Name = "MsgPicture";
            this.MsgPicture.Size = new System.Drawing.Size(248, 89);
            this.MsgPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MsgPicture.TabIndex = 1035;
            this.MsgPicture.TabStop = false;
            // 
            // AutoFitLinkLabel
            // 
            this.AutoFitLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoFitLinkLabel.AutoSize = true;
            this.AutoFitLinkLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.5F);
            this.AutoFitLinkLabel.Location = new System.Drawing.Point(936, 70);
            this.AutoFitLinkLabel.Name = "AutoFitLinkLabel";
            this.AutoFitLinkLabel.Size = new System.Drawing.Size(90, 13);
            this.AutoFitLinkLabel.TabIndex = 1036;
            this.AutoFitLinkLabel.TabStop = true;
            this.AutoFitLinkLabel.Text = "内容を展開する";
            this.AutoFitLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AutoFitLinkLabel_LinkClicked);
            // 
            // DesignCheckDetailListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.DesignCheckUserListButton);
            this.Controls.Add(this.EntryButton);
            this.Name = "DesignCheckDetailListForm";
            this.Text = "DesignCheckDetailList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesignCheckDetailListForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DesignCheckDetailListForm_FormClosed);
            this.Load += new System.EventHandler(this.DesignCheckDetailListForm_Load);
            this.Shown += new System.EventHandler(this.DesignCheckDetailListForm_Shown);
            this.Resize += new System.EventHandler(this.DesignCheckDetailListForm_Resize);
            this.Controls.SetChildIndex(this.EntryButton, 0);
            this.Controls.SetChildIndex(this.DesignCheckUserListButton, 0);
            this.Controls.SetChildIndex(this.DownloadButton, 0);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.DocumentTypeTableLayoutPanel.ResumeLayout(false);
            this.DocumentTypeTableLayoutPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PointMultiRow)).EndInit();
            this.CompletionScheduleContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictNotice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MsgPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel DocumentTypeTableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label OpenDayLabel;
        private System.Windows.Forms.Button RowDeleteButton;
        private System.Windows.Forms.Button RowAddButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button EntryButton;
        private System.Windows.Forms.Button DesignCheckUserListButton;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Label ListDataLabel;
        private System.Windows.Forms.ContextMenuStrip CompletionScheduleContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DayInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DayCompletionInputToolStripMenuItem;
        private System.Windows.Forms.ToolTip SituationToolTip;
        private System.Windows.Forms.Label DesignCheckNameLabel;
        private System.Windows.Forms.Button Copybutton;
        private GrapeCity.Win.MultiRow.GcMultiRow PointMultiRow;
        private System.Windows.Forms.Label RowCountLabel;
        private System.Windows.Forms.PictureBox PictNotice;
        private System.Windows.Forms.PictureBox MsgPicture;
        private System.Windows.Forms.LinkLabel AutoFitLinkLabel;
    }
}