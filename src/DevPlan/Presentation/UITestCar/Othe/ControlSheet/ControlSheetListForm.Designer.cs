namespace DevPlan.Presentation.UITestCar.Othe.ControlSheet
{
    partial class ControlSheetListForm
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
            this.components = new System.ComponentModel.Container();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager2 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchConditionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.AffiliationComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.AllUserCheckBox = new System.Windows.Forms.CheckBox();
            this.SearchConditionComboBox = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.SearchConditionButton = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ListSortPictureBox = new System.Windows.Forms.PictureBox();
            this.RowCountLabel = new System.Windows.Forms.Label();
            this.TestCarListMultiRow = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.label14 = new System.Windows.Forms.Label();
            this.template11 = new DevPlan.Presentation.UITestCar.Othe.ControlSheet.ControlSheetListMultiRowTemplate();
            this.ConfigurationSaveButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.SearchResultLabel = new System.Windows.Forms.Label();
            this.TestCarHistoryButton = new System.Windows.Forms.Button();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.ListConfigToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ListSortToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ContentsPanel.SuspendLayout();
            this.SearchConditionTableLayoutPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListSortPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestCarListMultiRow)).BeginInit();
            this.ButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.TabIndex = 1015;
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.SearchResultLabel);
            this.ContentsPanel.Controls.Add(this.SearchConditionButton);
            this.ContentsPanel.Controls.Add(this.label27);
            this.ContentsPanel.Controls.Add(this.ButtonPanel);
            this.ContentsPanel.Controls.Add(this.MainPanel);
            this.ContentsPanel.Controls.Add(this.SearchConditionTableLayoutPanel);
            this.ContentsPanel.TabIndex = 0;
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(0, 0);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(120, 30);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "検索画面";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchConditionTableLayoutPanel
            // 
            this.SearchConditionTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.SearchConditionTableLayoutPanel.ColumnCount = 4;
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.SearchConditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.SearchConditionTableLayoutPanel.Controls.Add(this.AffiliationComboBox, 0, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label1, 2, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.panel4, 3, 0);
            this.SearchConditionTableLayoutPanel.Controls.Add(this.label17, 0, 0);
            this.SearchConditionTableLayoutPanel.Location = new System.Drawing.Point(12, 29);
            this.SearchConditionTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchConditionTableLayoutPanel.Name = "SearchConditionTableLayoutPanel";
            this.SearchConditionTableLayoutPanel.RowCount = 1;
            this.SearchConditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SearchConditionTableLayoutPanel.Size = new System.Drawing.Size(791, 32);
            this.SearchConditionTableLayoutPanel.TabIndex = 1;
            // 
            // AffiliationComboBox
            // 
            this.AffiliationComboBox.DisplayMember = "NAME";
            this.AffiliationComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AffiliationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AffiliationComboBox.FormattingEnabled = true;
            this.AffiliationComboBox.Location = new System.Drawing.Point(135, 4);
            this.AffiliationComboBox.Name = "AffiliationComboBox";
            this.AffiliationComboBox.Size = new System.Drawing.Size(154, 23);
            this.AffiliationComboBox.TabIndex = 0;
            this.AffiliationComboBox.ValueMember = "CODE";
            this.AffiliationComboBox.SelectedValueChanged += new System.EventHandler(this.AffiliationComboBox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(293, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 30);
            this.label1.TabIndex = 50;
            this.label1.Text = "保存済み検索条件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.AllUserCheckBox);
            this.panel4.Controls.Add(this.SearchConditionComboBox);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(424, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(366, 30);
            this.panel4.TabIndex = 1;
            // 
            // AllUserCheckBox
            // 
            this.AllUserCheckBox.AutoSize = true;
            this.AllUserCheckBox.Location = new System.Drawing.Point(192, 7);
            this.AllUserCheckBox.Name = "AllUserCheckBox";
            this.AllUserCheckBox.Size = new System.Drawing.Size(172, 19);
            this.AllUserCheckBox.TabIndex = 1;
            this.AllUserCheckBox.Text = "他ユーザーの条件も表示";
            this.AllUserCheckBox.UseVisualStyleBackColor = true;
            this.AllUserCheckBox.CheckedChanged += new System.EventHandler(this.AllUserCheckBox_CheckedChanged);
            // 
            // SearchConditionComboBox
            // 
            this.SearchConditionComboBox.DisplayMember = "条件名";
            this.SearchConditionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchConditionComboBox.FormattingEnabled = true;
            this.SearchConditionComboBox.Location = new System.Drawing.Point(3, 4);
            this.SearchConditionComboBox.Name = "SearchConditionComboBox";
            this.SearchConditionComboBox.Size = new System.Drawing.Size(183, 23);
            this.SearchConditionComboBox.TabIndex = 0;
            this.SearchConditionComboBox.ValueMember = "条件名";
            this.SearchConditionComboBox.SelectedIndexChanged += new System.EventHandler(this.SearchConditionComboBox_SelectedIndexChanged);
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
            this.label17.Text = "管理所在地";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchConditionButton
            // 
            this.SearchConditionButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchConditionButton.Location = new System.Drawing.Point(88, 5);
            this.SearchConditionButton.Name = "SearchConditionButton";
            this.SearchConditionButton.Size = new System.Drawing.Size(20, 23);
            this.SearchConditionButton.TabIndex = 0;
            this.SearchConditionButton.Text = "-";
            this.SearchConditionButton.UseVisualStyleBackColor = false;
            this.SearchConditionButton.Click += new System.EventHandler(this.SearchConditionButton_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(15, 10);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(67, 15);
            this.label27.TabIndex = 1018;
            this.label27.Text = "検索条件";
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.ListSortPictureBox);
            this.MainPanel.Controls.Add(this.RowCountLabel);
            this.MainPanel.Controls.Add(this.TestCarListMultiRow);
            this.MainPanel.Controls.Add(this.label14);
            this.MainPanel.Location = new System.Drawing.Point(0, 75);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1184, 506);
            this.MainPanel.TabIndex = 6;
            // 
            // ListSortPictureBox
            // 
            this.ListSortPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ListSortPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ListSortPictureBox.Image = global::DevPlan.Presentation.Properties.Resources.CommonSortButton;
            this.ListSortPictureBox.Location = new System.Drawing.Point(1156, 4);
            this.ListSortPictureBox.Name = "ListSortPictureBox";
            this.ListSortPictureBox.Size = new System.Drawing.Size(16, 16);
            this.ListSortPictureBox.TabIndex = 1041;
            this.ListSortPictureBox.TabStop = false;
            this.ListSortToolTip.SetToolTip(this.ListSortPictureBox, "表示データを列ごとに並び替えます。");
            this.ListSortPictureBox.Click += new System.EventHandler(this.ListSortPictureBox_Click);
            // 
            // RowCountLabel
            // 
            this.RowCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RowCountLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.5F);
            this.RowCountLabel.Location = new System.Drawing.Point(969, 4);
            this.RowCountLabel.Name = "RowCountLabel";
            this.RowCountLabel.Size = new System.Drawing.Size(181, 18);
            this.RowCountLabel.TabIndex = 1040;
            this.RowCountLabel.Text = "表示件数： 9999/9999 件";
            this.RowCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TestCarListMultiRow
            // 
            this.TestCarListMultiRow.AllowAutoExtend = true;
            this.TestCarListMultiRow.AllowUserToAddRows = false;
            this.TestCarListMultiRow.AllowUserToDeleteRows = false;
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            this.TestCarListMultiRow.AlternatingRowsDefaultCellStyle = cellStyle7;
            this.TestCarListMultiRow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestCarListMultiRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            cellStyle8.BackColor = System.Drawing.SystemColors.Control;
            cellStyle8.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle8.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle8.Padding = new System.Windows.Forms.Padding(0);
            cellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle8.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.TestCarListMultiRow.ColumnHeadersDefaultCellStyle = cellStyle8;
            cellStyle9.BackColor = System.Drawing.SystemColors.Control;
            cellStyle9.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle9.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle9.Padding = new System.Windows.Forms.Padding(0);
            cellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle9.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.TestCarListMultiRow.ColumnHeadersDefaultHeaderCellStyle = cellStyle9;
            cellStyle10.BackColor = System.Drawing.SystemColors.Window;
            cellStyle10.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle10.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle10.Padding = new System.Windows.Forms.Padding(0);
            cellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle10.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle10.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.TestCarListMultiRow.DefaultCellStyle = cellStyle10;
            this.TestCarListMultiRow.FreezeLines = new GrapeCity.Win.MultiRow.FreezeLines(new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.None, System.Drawing.Color.Empty));
            this.TestCarListMultiRow.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.TestCarListMultiRow.Location = new System.Drawing.Point(12, 23);
            this.TestCarListMultiRow.MultiSelect = false;
            this.TestCarListMultiRow.Name = "TestCarListMultiRow";
            cellStyle11.Padding = new System.Windows.Forms.Padding(0);
            this.TestCarListMultiRow.RowsDefaultCellStyle = cellStyle11;
            cellStyle12.BackColor = System.Drawing.SystemColors.Control;
            cellStyle12.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle12.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle12.Padding = new System.Windows.Forms.Padding(0);
            cellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle12.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle12.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.TestCarListMultiRow.RowsDefaultHeaderCellStyle = cellStyle12;
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
            this.TestCarListMultiRow.ShortcutKeyManager = shortcutKeyManager2;
            this.TestCarListMultiRow.Size = new System.Drawing.Size(1160, 480);
            this.TestCarListMultiRow.TabIndex = 100;
            this.TestCarListMultiRow.Text = "gcMultiRow1";
            this.TestCarListMultiRow.VerticalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.TestCarListMultiRow.VerticalScrollMode = GrapeCity.Win.MultiRow.ScrollMode.Pixel;
            this.TestCarListMultiRow.CellDoubleClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.TestCarListMultiRow_CellDoubleClick);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(635, 2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(328, 20);
            this.label14.TabIndex = 113;
            this.label14.Text = "※ダブルクリックで試験車情報画面を開きます。";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfigurationSaveButton
            // 
            this.ConfigurationSaveButton.BackColor = System.Drawing.SystemColors.Control;
            this.ConfigurationSaveButton.Location = new System.Drawing.Point(126, 0);
            this.ConfigurationSaveButton.Name = "ConfigurationSaveButton";
            this.ConfigurationSaveButton.Size = new System.Drawing.Size(120, 30);
            this.ConfigurationSaveButton.TabIndex = 3;
            this.ConfigurationSaveButton.Text = "設定の保存";
            this.ConfigurationSaveButton.UseVisualStyleBackColor = false;
            this.ConfigurationSaveButton.Click += new System.EventHandler(this.ConfigurationSaveButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadButton.BackColor = System.Drawing.SystemColors.Control;
            this.DownloadButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.DownloadButton.Location = new System.Drawing.Point(926, 616);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(120, 20);
            this.DownloadButton.TabIndex = 1014;
            this.DownloadButton.Text = "Excel出力";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // SearchResultLabel
            // 
            this.SearchResultLabel.AutoSize = true;
            this.SearchResultLabel.ForeColor = System.Drawing.Color.Red;
            this.SearchResultLabel.Location = new System.Drawing.Point(123, 9);
            this.SearchResultLabel.Name = "SearchResultLabel";
            this.SearchResultLabel.Size = new System.Drawing.Size(0, 15);
            this.SearchResultLabel.TabIndex = 1024;
            // 
            // TestCarHistoryButton
            // 
            this.TestCarHistoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TestCarHistoryButton.BackColor = System.Drawing.SystemColors.Control;
            this.TestCarHistoryButton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.TestCarHistoryButton.Location = new System.Drawing.Point(12, 616);
            this.TestCarHistoryButton.Name = "TestCarHistoryButton";
            this.TestCarHistoryButton.Size = new System.Drawing.Size(120, 20);
            this.TestCarHistoryButton.TabIndex = 1013;
            this.TestCarHistoryButton.Text = "試験車使用履歴";
            this.TestCarHistoryButton.UseVisualStyleBackColor = false;
            this.TestCarHistoryButton.Click += new System.EventHandler(this.TestCarHistoryButton_Click);
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Controls.Add(this.SearchButton);
            this.ButtonPanel.Controls.Add(this.ConfigurationSaveButton);
            this.ButtonPanel.Location = new System.Drawing.Point(12, 63);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(247, 30);
            this.ButtonPanel.TabIndex = 1025;
            // 
            // ControlSheetListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.TestCarHistoryButton);
            this.Controls.Add(this.DownloadButton);
            this.Name = "ControlSheetListForm";
            this.Text = "タイトルが設定されていません - 試験車管理システム";
            this.Load += new System.EventHandler(this.ControlSheetForm_Load);
            this.Shown += new System.EventHandler(this.ControlSheetListForm_Shown);
            this.Controls.SetChildIndex(this.ContentsPanel, 0);
            this.Controls.SetChildIndex(this.RightButton, 0);
            this.Controls.SetChildIndex(this.DownloadButton, 0);
            this.Controls.SetChildIndex(this.TestCarHistoryButton, 0);
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.SearchConditionTableLayoutPanel.ResumeLayout(false);
            this.SearchConditionTableLayoutPanel.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ListSortPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestCarListMultiRow)).EndInit();
            this.ButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TableLayoutPanel SearchConditionTableLayoutPanel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button SearchConditionButton;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button ConfigurationSaveButton;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox AllUserCheckBox;
        private System.Windows.Forms.ComboBox SearchConditionComboBox;
        private System.Windows.Forms.ComboBox AffiliationComboBox;
        private System.Windows.Forms.Label SearchResultLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button TestCarHistoryButton;
        private GrapeCity.Win.MultiRow.GcMultiRow TestCarListMultiRow;
        private ControlSheetListMultiRowTemplate template11;
        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.PictureBox ListSortPictureBox;
        private System.Windows.Forms.Label RowCountLabel;
        private System.Windows.Forms.ToolTip ListConfigToolTip;
        private System.Windows.Forms.ToolTip ListSortToolTip;
    }
}
