namespace DevPlan.Presentation.UIDevPlan.CapAndProduct
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class NDriveLinkListMultiRowTemplate
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region MultiRow Template Designer generated code

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
        private void InitializeComponent()
        {
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell5 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell6 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.GridTitle = new GrapeCity.Win.MultiRow.LinkLabelCell();
            this.GridUpdater = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.GridUpdate = new GrapeCity.Win.MultiRow.DateTimePickerCell();
            this.GridId = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.GridTitle);
            this.Row.Cells.Add(this.GridUpdate);
            this.Row.Cells.Add(this.GridUpdater);
            this.Row.Cells.Add(this.GridId);
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            this.Row.DefaultCellStyle = cellStyle5;
            this.Row.Height = 21;
            this.Row.Width = 529;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell5);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell6);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 529;
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(242, 20);
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "タイトル";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.Location = new System.Drawing.Point(242, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(135, 20);
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Tag = "DateTime";
            this.columnHeaderCell4.Value = "更新日時";
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.Location = new System.Drawing.Point(377, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.Size = new System.Drawing.Size(144, 20);
            this.columnHeaderCell5.TabIndex = 4;
            this.columnHeaderCell5.Value = "更新者";
            // 
            // columnHeaderCell6
            // 
            this.columnHeaderCell6.Location = new System.Drawing.Point(1015, 0);
            this.columnHeaderCell6.Name = "columnHeaderCell6";
            this.columnHeaderCell6.Size = new System.Drawing.Size(0, 20);
            this.columnHeaderCell6.TabIndex = 5;
            this.columnHeaderCell6.TabStop = false;
            this.columnHeaderCell6.Value = "ID";
            // 
            // GridTitle
            // 
            this.GridTitle.DataField = "TITLE";
            this.GridTitle.Location = new System.Drawing.Point(0, 0);
            this.GridTitle.Name = "GridTitle";
            this.GridTitle.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.GridTitle.Size = new System.Drawing.Size(242, 21);
            cellStyle1.Padding = new System.Windows.Forms.Padding(0);
            this.GridTitle.Style = cellStyle1;
            this.GridTitle.TabIndex = 2;
            // 
            // GridUpdater
            // 
            this.GridUpdater.DataField = "NAME";
            this.GridUpdater.Location = new System.Drawing.Point(377, 0);
            this.GridUpdater.Name = "GridUpdater";
            this.GridUpdater.ReadOnly = true;
            this.GridUpdater.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.GridUpdater.Size = new System.Drawing.Size(144, 21);
            cellStyle3.Padding = new System.Windows.Forms.Padding(0);
            this.GridUpdater.Style = cellStyle3;
            this.GridUpdater.TabIndex = 4;
            // 
            // GridUpdate
            // 
            this.GridUpdate.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.GridUpdate.DataField = "INPUT_DATETIME";
            this.GridUpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.GridUpdate.Location = new System.Drawing.Point(242, 0);
            this.GridUpdate.Name = "GridUpdate";
            this.GridUpdate.ReadOnly = true;
            this.GridUpdate.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Horizontal;
            this.GridUpdate.ShowDropDownButton = GrapeCity.Win.MultiRow.CellButtonVisibility.ShowForCurrentCell;
            this.GridUpdate.Size = new System.Drawing.Size(135, 21);
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.GridUpdate.Style = cellStyle2;
            this.GridUpdate.TabIndex = 3;
            // 
            // GridId
            // 
            this.GridId.DataField = "ID";
            this.GridId.Location = new System.Drawing.Point(1015, 0);
            this.GridId.Name = "GridId";
            this.GridId.ReadOnly = true;
            this.GridId.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.Vertical;
            this.GridId.Size = new System.Drawing.Size(0, 21);
            cellStyle4.Padding = new System.Windows.Forms.Padding(0);
            this.GridId.Style = cellStyle4;
            this.GridId.TabIndex = 5;
            this.GridId.TabStop = false;
            // 
            // NDriveLinkListMultiRowTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 529;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell5;
        private GrapeCity.Win.MultiRow.LinkLabelCell GridTitle;
        private GrapeCity.Win.MultiRow.DateTimePickerCell GridUpdate;
        private GrapeCity.Win.MultiRow.TextBoxCell GridUpdater;
        private GrapeCity.Win.MultiRow.TextBoxCell GridId;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell6;
    }
}
