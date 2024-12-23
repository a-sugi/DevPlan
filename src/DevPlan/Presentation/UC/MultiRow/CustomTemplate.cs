using DevPlan.Presentation.Properties;
using DevPlan.UICommon;
using DevPlan.UICommon.Config;
using DevPlan.UICommon.Dto;
using GrapeCity.Win.MultiRow;
using GrapeCity.Win.MultiRow.InputMan;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.UC.MultiRow
{
    /// <summary>
    /// MultiRowカスタムテンプレート作成。
    /// </summary>
    public class CustomTemplate : Template
    {
        #region インスタンス

        /// <summary>カスタム比較演算クラス</summary>
        private CustomFilterComparer _customFilterComparer { get; set; } = new CustomFilterComparer();

        #endregion

        #region メンバ変数

        /// <summary>ソートリスト</summary>
        private IEnumerable<MultiRowSortModel> sortList = null;

        #endregion

        #region 定義

        /// <summary>MultiRowのタイトルヘッダの高さ</summary>
        public const int TemplateHeaderHeight = 40;

        /// <summary>MultiRowの行の高さ（複数行あり）</summary>
        public const int TemplateRowHeight = 25;

        /// <summary>選択項目数の上限</summary>
        public const int FilterItemMaxCount = 10000;

        /// <summary>行ヘッダの幅</summary>
        public const int RowHeaderWidth = 30;

        /// <summary>フィルター背景色</summary>
        private const string FilterRowBackColor = "FILTER_ROW_BACKCOLOR";

        /// <summary>フィルター文字色</summary>
        private const string FilterRowForeColor = "FILTER_ROW_FORECOLOR";

        /// <summary>コーナヘッダ名</summary>
        private const string CornerHeaderName = "CornerHeader";

        /// <summary>コーナヘッダ値</summary>
        private const string CornerHeaderValue = "No.";

        /// <summary>行ヘッダ名</summary>
        private const string RowHeaderName = "RowHeader";

        /// <summary>行ヘッダ値フォーマット</summary>
        private const string RowHeaderValueFormat = "%1%";

        /// <summary>フィルタリングテキスト名フォーマット</summary>
        private const string FilteringTextBoxCellNameFormat = "FilteringTextBoxCell{0}";

        /// <summary>ヘッダタグプロパティ</summary>
        private const string HeaderTagPropertyFormat = "DisplayIndex({0});DataField({1})";

        #endregion

        #region プロパティ

        /// <summary>MultiRow</summary>
        public GcMultiRow MultiRow { get; set; } = null;
        
        /// <summary>行ヘッダの表示有無</summary>
        public bool IsRowHeader { get; set; } = true;

        /// <summary>セル値検証の有無</summary>
        public bool IsValidate { get; set; } = false;

        /// <summary>列ヘッダの高さ（未設定の場合は既定値が使用されます）</summary>
        public int? ColumnHeaderHeight { get; set; } = null;

        /// <summary>行カウントラベル</summary>
        public Label RowCountLabel { get; set; } = null;

        /// <summary>表示件数フォーマット</summary>
        public string RowCountFormat { get; set; } = "表示件数： {0:#,0}/{1:#,0} 件";

        /// <summary>グループカウントラベル</summary>
        public Label GroupCountLabel { get; set; } = null;

        /// <summary>グループ件数フォーマット</summary>
        public string GroupCountFormat { get; set; } = "グループ件数： {0:#,0}/{1:#,0} 件";

        /// <summary>グループセル名</summary>
        public string GroupCountCellName { get; set; } = null;
        
        /// <summary>グループセルインデックス</summary>
        public int GroupCountCellIndex { get; set; } = 0;

        /// <summary>表示設定リスト</summary>
        public List<MultiRowDisplayModel> ConfigDispLayList { get; set; } = null;

        /// <summary>表示</summary>
        public IEnumerable<MultiRowDisplayModel> Display
        {
            get
            {
                return this.GetDisplay();
            }
            set
            {
                if (value == null || value.Any() == false)
                {
                    return;
                }

                // 表示反映
                this.ApplyDisplay(value, MultiRow.Template);
            }
        }

        /// <summary>ソート</summary>
        public IEnumerable<MultiRowSortModel> Sort
        {
            get
            {
                return this.sortList;
            }
            set
            {
                if (value == null || value.Any() == false)
                {
                    this.sortList = null;

                    return;
                }

                this.sortList = value;

                // ソート反映
                this.ApplySort(value);
            }
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// デフォルトコンストラクタ。
        /// </summary>
        /// <remarks>
        /// MultiRowの仕様上、必ず定義が必要です。
        /// </remarks>
        public CustomTemplate()
        {
            // プロパティの初期化
            Display = null;
            Sort = null;
        }

        /// <summary>
        /// カスタムコンストラクタ。
        /// </summary>
        /// <remarks>
        /// 渡された表示対象データを元にテンプレートを作成します。
        /// </remarks>
        /// <param name="colList">表示するデータ</param>
        public CustomTemplate(Dictionary<string, CustomMultiRowCellStyle> colList)
            : this(colList, true, null, null, null)
        {

        }

        /// <summary>
        /// カスタムコンストラクタ。
        /// </summary>
        /// <remarks>
        /// 渡された表示対象データを元にテンプレートを作成します。
        /// </remarks>
        /// <param name="colList">表示するデータ</param>
        /// <param name="isRowHeader">行ヘッダ使用有無</param>
        public CustomTemplate(Dictionary<string, CustomMultiRowCellStyle> colList, Boolean isRowHeader)
            : this(colList, isRowHeader, null, null, null)
        {

        }

        /// <summary>
        /// カスタムコンストラクタ。
        /// </summary>
        /// <remarks>
        /// 渡された表示対象データを元にテンプレートを作成します。
        /// </remarks>
        /// <param name="colList">表示するデータ</param>
        /// <param name="isRowHeader">行ヘッダ使用有無</param>
        /// <param name="multiRow">MultiRow</param>
        public CustomTemplate(Dictionary<string, CustomMultiRowCellStyle> colList, Boolean isRowHeader, GcMultiRow multiRow)
            : this(colList, isRowHeader, multiRow, null, null)
        {

        }

        /// <summary>
        /// カスタムコンストラクタ。
        /// </summary>
        /// <remarks>
        /// 渡された表示対象データを元にテンプレートを作成します。
        /// </remarks>
        /// <param name="colList">表示するデータ</param>
        /// <param name="isRowHeader">行ヘッダ使用有無</param>
        /// <param name="multiRow">MultiRow</param>
        /// <param name="rowCountLabel">行カウント表示ラベル</param>
        public CustomTemplate(Dictionary<string, CustomMultiRowCellStyle> colList, Boolean isRowHeader, GcMultiRow multiRow, Label rowCountLabel)
            : this(colList, isRowHeader, multiRow, rowCountLabel, null)
        {

        }

        /// <summary>
        /// カスタムコンストラクタ。
        /// </summary>
        /// <remarks>
        /// 渡された表示対象データを元にテンプレートを作成します。
        /// </remarks>
        /// <param name="colList">表示するデータ</param>
        /// <param name="isRowHeader">行ヘッダ使用有無</param>
        /// <param name="multiRow">MultiRow</param>
        /// <param name="rowCountLabel">行カウント表示ラベル</param>
        /// <param name="configDisplayList">表示設定リスト</param>
        public CustomTemplate(Dictionary<string, CustomMultiRowCellStyle> colList, Boolean isRowHeader, GcMultiRow multiRow, Label rowCountLabel, List<MultiRowDisplayModel> configDisplayList)
        {
            // プロパティのセット
            IsRowHeader = isRowHeader;
            MultiRow = multiRow;
            RowCountLabel = rowCountLabel;
            ConfigDispLayList = configDisplayList;

            // プロパティの初期化
            Display = null;
            Sort = null;

            // ヘッダセクション
            var columnHeaderSection1 = new ColumnHeaderSection()
            {
                Selectable = true,
                ReadOnly = false,
                Height = TemplateHeaderHeight + TemplateRowHeight,
                BackColor = Color.Transparent
            };

            // データセルセクション
            Row.Height = TemplateRowHeight;
            Row.BackColor = Color.Transparent;

            int pointX = 0;

            // 行ヘッダ
            if (IsRowHeader)
            {
                #region コーナヘッダ＆行ヘッダ作成

                var cornerHeaderCell = new ColumnHeaderCell()
                {
                    Name = CornerHeaderName,
                    Value = CornerHeaderValue,
                    Location = new Point(pointX, 0),
                    Size = new Size(RowHeaderWidth, TemplateHeaderHeight + TemplateRowHeight),
                    FlatStyle = FlatStyle.Flat
                };
                cornerHeaderCell.Style.BackColor = Color.WhiteSmoke;
                cornerHeaderCell.Style.WordWrap = MultiRowTriState.False;
                cornerHeaderCell.Style.Border = new Border() { Outline = new GrapeCity.Win.MultiRow.Line(LineStyle.Thin, Color.LightGray) };

                var rowHeaderCell = new RowHeaderCell()
                {
                    Name = RowHeaderName,
                    Ellipsis = MultiRowEllipsisMode.EllipsisEnd,
                    Style = new CustomMultiRowCellStyle().DataCellStyle,
                    ShowIndicator = false,
                    ValueFormat = RowHeaderValueFormat,
                    Size = new Size(RowHeaderWidth, TemplateRowHeight),
                    FlatStyle = FlatStyle.Flat
                };
                rowHeaderCell.Style.BackColor = Color.WhiteSmoke;
                rowHeaderCell.Style.WordWrap = MultiRowTriState.False;
                rowHeaderCell.Style.Border = new Border() { Outline = new GrapeCity.Win.MultiRow.Line(LineStyle.Thin, Color.LightGray) };

                columnHeaderSection1.Cells.Add(cornerHeaderCell);
                Row.Cells.Add(rowHeaderCell);

                #endregion

                pointX = RowHeaderWidth;
            }

            var outputList = colList.OrderBy(x => x.Value.DisplayIndex);

            foreach (var data in outputList)
            {
                var filterName = data.Key;

                if (data.Value.Visible)
                {
                    #region ヘッダ＆ヘッダフィルタ作成

                    var columnFilterHeaderCell = new ColumnHeaderCell()
                    {
                        FilterCellName = filterName,
                        Location = new Point(pointX, 0),
                        Name = "Header" + data.Key,
                        ShowDropDownButtonImages = true,
                        HideDropDownButtonVisualEffect = true,
                        Size = new Size(data.Value.Width, TemplateHeaderHeight),
                        Style = data.Value.HeaderCellStyle,
                        Value = data.Value.HeaderText,
                        Tag = string.Format(HeaderTagPropertyFormat, data.Value.DisplayIndex, data.Key)
                    };

                    // ドロップダウン画像
                    columnFilterHeaderCell.DropDownButtonImages.Normal = Resources.SortGlyphDefaultImageWhite;
                    columnFilterHeaderCell.DropDownButtonImages.Filtered = Resources.SortGlyphFilterdDefaultImageWhite;
                    columnFilterHeaderCell.DropDownButtonImages.AscendingSorted = Resources.SortGlyphAscendingImageWhite;
                    columnFilterHeaderCell.DropDownButtonImages.DescendingSorted = Resources.SortGlyphDescendingImageWhite;
                    columnFilterHeaderCell.DropDownButtonImages.FilteredAndAscendingSorted = Resources.SortGlyphFilterdAscendingImageWhite;
                    columnFilterHeaderCell.DropDownButtonImages.FilteredAndDescendingSorted = Resources.SortGlyphFilterdDescendingImageWhite;

                    if (data.Value.FilterItem == null)
                    {
                        var commonContextMenu = new HeaderDropDownContextMenu();
                        commonContextMenu.Items.AddRange(new List<ToolStripItem>
                        {
                            new SortToolStripItem(),
                            new SortToolStripItem() { SortOrder = SortOrder.Descending },
                            new ToolStripSeparator(),
                            new ShowAllToolStripItem(),
                            new ToolStripSeparator(),
                            new AutoFilterToolStripItem() { MaxCount = FilterItemMaxCount }
                        }
                        .ToArray());
                        commonContextMenu.Closed += HeaderDropDownContextMenu_Closed;
                        commonContextMenu.Items[3].Click += ShowAllToolStripItemItem_Click;

                        var datetimeContextMenu = new HeaderDropDownContextMenu();
                        datetimeContextMenu.Items.AddRange(new List<ToolStripItem>()
                        {
                            new SortToolStripItem(),
                            new SortToolStripItem() { SortOrder = SortOrder.Descending },
                            new ToolStripSeparator(),
                            new ShowAllToolStripItem(),
                            new ToolStripSeparator(),
                            new DateTimeFilterItem() { MaxCount = FilterItemMaxCount }
                        }
                        .ToArray());
                        datetimeContextMenu.Closed += HeaderDropDownContextMenu_Closed;
                        datetimeContextMenu.Items[3].Click += ShowAllToolStripItemItem_Click;

                        var datetimeHourContextMenu = new HeaderDropDownContextMenu();
                        datetimeHourContextMenu.Items.AddRange(new List<ToolStripItem>()
                        {
                            new SortToolStripItem(),
                            new SortToolStripItem() { SortOrder = SortOrder.Descending },
                            new ToolStripSeparator(),
                            new ShowAllToolStripItem(),
                            new ToolStripSeparator(),
                            new DateTimeHourFilterItem() { MaxCount = FilterItemMaxCount }
                        }
                        .ToArray());
                        datetimeHourContextMenu.Closed += HeaderDropDownContextMenu_Closed;
                        datetimeHourContextMenu.Items[3].Click += ShowAllToolStripItemItem_Click;

                        //ロング型のフィルタには現時点で未対応
                        var datetimeLongContextMenu = new HeaderDropDownContextMenu();
                        datetimeLongContextMenu.Items.AddRange(new List<ToolStripItem>()
                        {
                            new SortToolStripItem(),
                            new SortToolStripItem() { SortOrder = SortOrder.Descending },
                            new ToolStripSeparator(),
                            new ShowAllToolStripItem(),
                            new ToolStripSeparator(),
                            new AutoFilterToolStripItem() { MaxCount = FilterItemMaxCount }
                        }
                        .ToArray());
                        datetimeLongContextMenu.Closed += HeaderDropDownContextMenu_Closed;
                        datetimeLongContextMenu.Items[3].Click += ShowAllToolStripItemItem_Click;

                        if (data.Value.Type == MultiRowCellType.DATETIME)
                        {
                            columnFilterHeaderCell.DropDownContextMenuStrip = datetimeContextMenu;
                        }
                        else if (data.Value.Type == MultiRowCellType.DATETIME_HOUR)
                        {
                            columnFilterHeaderCell.DropDownContextMenuStrip = datetimeHourContextMenu;
                        }
                        else if(data.Value.Type == MultiRowCellType.DATETIME_LONG)
                        {
                            columnFilterHeaderCell.DropDownContextMenuStrip = datetimeLongContextMenu;
                        }
                        else
                        {
                            columnFilterHeaderCell.DropDownContextMenuStrip = commonContextMenu;
                        }
                    }
                    else
                    {
                        // 共通化したいが、commonMenuを使いまわせない様子（フィルタが表示されなくなる）。対応策検討後に修正。
                        var customContextMenu = new HeaderDropDownContextMenu();
                        customContextMenu.Items.AddRange(new List<ToolStripItem>()
                        {
                            new SortToolStripItem(),
                            new SortToolStripItem() { SortOrder = SortOrder.Descending },
                            new ToolStripSeparator(),
                            new ShowAllToolStripItem(),
                            new ToolStripSeparator(),
                            data.Value.FilterItem
                        }
                        .ToArray());
                        customContextMenu.Closed += HeaderDropDownContextMenu_Closed;
                        customContextMenu.Items[3].Click += ShowAllToolStripItemItem_Click;

                        columnFilterHeaderCell.DropDownContextMenuStrip = customContextMenu;
                    }

                    columnFilterHeaderCell.FlatStyle = FlatStyle.Flat;
                    columnHeaderSection1.Cells.Add(columnFilterHeaderCell);

                    #endregion

                    #region フィルタテキストボックス作成

                    var filteringTextBox = new FilteringTextBoxCell()
                    {
                        CustomComparisonOperator = _customFilterComparer,
                        FilterComparisonOperator = FilterComparisonOperator.Custom,
                        FilteringCellName = filterName,
                        Location = new Point(pointX, TemplateHeaderHeight),
                        Name = string.Format(FilteringTextBoxCellNameFormat, columnFilterHeaderCell.Name),
                        Size = new Size(data.Value.Width, TemplateRowHeight),
                        Style = new CellStyle()
                        {
                            BackColor = GetMultiRowColor(FilterRowBackColor),
                            ForeColor = GetMultiRowColor(FilterRowForeColor),
                            TextAlign = MultiRowContentAlignment.MiddleLeft
                        }
                    };

                    if (data.Value.Type == MultiRowCellType.CHECKBOX || data.Value.Type == MultiRowCellType.COMBOBOX)
                    {
                        filteringTextBox.ReadOnly = true;
                        filteringTextBox.Style = new CellStyle()
                        {
                            BackColor = Color.WhiteSmoke,
                            ForeColor = GetMultiRowColor(FilterRowForeColor)
                        };
                    }

                    columnHeaderSection1.Cells.Add(filteringTextBox);

                    #endregion

                    #region データセル作成

                    Cell item;

                    if (data.Value.Type == MultiRowCellType.TEXT)
                    {
                        item = new TextBoxCell()
                        {
                            Ellipsis = MultiRowEllipsisMode.EllipsisEnd,
                            EllipsisString = "..."
                        };
                    }
                    else if (data.Value.Type == MultiRowCellType.CHECKBOX)
                    {
                        item = new CheckBoxCell()
                        {
                            CheckAlign = ContentAlignment.MiddleCenter
                        };
                    }
                    else if (data.Value.Type == MultiRowCellType.DATETIME || data.Value.Type == MultiRowCellType.DATETIME_HOUR || data.Value.Type == MultiRowCellType.DATETIME_LONG)
                    {
                        item = new DateTimePickerCell()
                        {
                            CustomFormat = data.Value.CustomFormat,
                            Format = DateTimePickerFormat.Custom,
                            ShowDropDownButton = CellButtonVisibility.ShowForCurrentCell
                        };
                    }
                    else if (data.Value.Type == MultiRowCellType.COMBOBOX)
                    {
                        item = new ComboBoxCell()
                        {
                            DropDownStyle = MultiRowComboBoxStyle.DropDownList,
                            ShowDropDownButton = CellButtonVisibility.ShowForCurrentCell,
                            ValueMember = data.Value.ValueMember,
                            DisplayMember = data.Value.DisplayMember,
                            DataSource = data.Value.ComboBoxDataSource,
                        };
                    }
                    else if (data.Value.Type == MultiRowCellType.LINKLABEL)
                    {
                        item = new LinkLabelCell()
                        {
                            Ellipsis = MultiRowEllipsisMode.EllipsisEnd,
                            EllipsisString = "..."
                        };
                    }
                    else if (data.Value.Type == MultiRowCellType.BUTTON)
                    {
                        item = new ButtonCell()
                        {
                            UseVisualStyleBackColor = true
                        };
                    }
                    else
                    {
                        throw new Exception("実装されていないMultiRowのセルタイプが渡されました。");
                    }

                    item.Name = filterName;
                    item.DataField = data.Key;
                    item.Size = new Size(data.Value.Width, TemplateRowHeight);
                    item.Location = new Point(pointX, 0);
                    item.TabIndex = data.Value.DisplayIndex;
                    item.ReadOnly = data.Value.ReadOnly;
                    item.Style = data.Value.DataCellStyle;
                    item.Tag = data.Value.Tag;

                    Row.Cells.Add(item);

                    #endregion

                    pointX += columnFilterHeaderCell.Size.Width;
                }
            }

            columnHeaderSection1.Width = pointX + 1;
            Row.Width = pointX + 1;

            ColumnHeaders.AddRange(new ColumnHeaderSection[] { columnHeaderSection1 });

            // 表示設定の反映
            ApplyDisplay(ConfigDispLayList, columnHeaderSection1, Row);

            // MultiRow共通マウスホイールのセット
            SetMultiRowMouseWheel();

            // MultiRow共通イベントのセット
            SetMultiRowEvent();
        }

        #endregion

        #region テンプレート共通化

        /// <summary>
        /// テンプレート共通化
        /// </summary>
        /// <param name="tmpl">元となるテンプレート</param>
        /// <returns>テンプレート</returns>
        public Template SetContextMenuTemplate(Template tmpl)
        {
            // MultiRowのタイトルヘッダの高さ
            var templHeaderHeight = this.ColumnHeaderHeight == null ? TemplateHeaderHeight : this.ColumnHeaderHeight.Value;

            // ヘッダセクション
            tmpl.ColumnHeaders[0].Selectable = true;
            tmpl.ColumnHeaders[0].ReadOnly = false;
            tmpl.ColumnHeaders[0].Height = templHeaderHeight + TemplateRowHeight;
            tmpl.ColumnHeaders[0].BackColor = Color.Transparent;

            // データセルセクション
            tmpl.Row.Height = TemplateRowHeight;
            tmpl.Row.BackColor = Color.Transparent;

            var diffX = 0;
            var displayIndex = 0;

            // 行ヘッダ
            if (IsRowHeader)
            {
                #region コーナヘッダ＆行ヘッダの作成

                var cornerHeaderCell = new ColumnHeaderCell()
                {
                    Name = CornerHeaderName,
                    Value = CornerHeaderValue,
                    Location = new Point(diffX, 0),
                    Size = new Size(RowHeaderWidth, templHeaderHeight + TemplateRowHeight),
                    FlatStyle = FlatStyle.Flat
                };
                cornerHeaderCell.Style.BackColor = Color.WhiteSmoke;
                cornerHeaderCell.Style.WordWrap = MultiRowTriState.False;
                cornerHeaderCell.Style.Border = new Border() { Outline = new GrapeCity.Win.MultiRow.Line(LineStyle.Thin, Color.LightGray) };

                var rowHeaderCell = new RowHeaderCell()
                {
                    Name = RowHeaderName,
                    Ellipsis = MultiRowEllipsisMode.EllipsisEnd,
                    Style = new CustomMultiRowCellStyle().DataCellStyle,
                    ShowIndicator = false,
                    ValueFormat = RowHeaderValueFormat,
                    Size = new Size(RowHeaderWidth, TemplateRowHeight),
                    FlatStyle = FlatStyle.Flat,
                };
                rowHeaderCell.Style.BackColor = Color.WhiteSmoke;
                rowHeaderCell.Style.WordWrap = MultiRowTriState.False;
                rowHeaderCell.Style.Border = new Border() { Outline = new GrapeCity.Win.MultiRow.Line(LineStyle.Thin, Color.LightGray) };

                tmpl.ColumnHeaders[0].Cells.Add(cornerHeaderCell);
                tmpl.Row.Cells.Add(rowHeaderCell);

                #endregion

                diffX = RowHeaderWidth;
            }

            foreach (var cell in tmpl.ColumnHeaders[0].Cells.OrderBy(x => x.CellIndex))
            {
                // コーナヘッダの場合は未処理
                if (IsRowHeader && cell.Name == CornerHeaderName)
                {
                    continue;
                }

                // カラムヘッダがない場合は未処理
                if (!(cell is ColumnHeaderCell))
                {
                    cell.Visible = false;

                    continue;
                }

                // カラムヘッダが非表示の場合は未処理
                if (cell.Visible == false)
                {
                    continue;
                }

                var headerCell = cell as ColumnHeaderCell;
                var dataCell = tmpl.Row.Cells[headerCell.CellIndex];

                #region ヘッダ＆ヘッダフィルタ作成

                var contextMenu = new HeaderDropDownContextMenu();

                if ((dataCell is DateTimePickerCell || dataCell is GcDateTimeCell) && dataCell.DataField != "INPUT_DATETIME")
                {
                    contextMenu.Items.AddRange(new List<ToolStripItem>()
                    {
                        new SortToolStripItem(),
                        new SortToolStripItem() { SortOrder = SortOrder.Descending },
                        new ToolStripSeparator(),
                        new ShowAllToolStripItem(),
                        new ToolStripSeparator(),
                        //UPDATE Start 2021/04/26 杉浦 CAP一覧にて日付のソートがおかしい
                        //new DateTimeFilterItem() { MaxCount = FilterItemMaxCount }
                        new DateTimeHourFilterItem() { MaxCount = FilterItemMaxCount }
                        //UPDATE End 2021/04/26 杉浦 CAP一覧にて日付のソートがおかしい
                    }
                    .ToArray());
                }
                else
                {
                    contextMenu.Items.AddRange(new List<ToolStripItem>
                    {
                        new SortToolStripItem(),
                        new SortToolStripItem() { SortOrder = SortOrder.Descending },
                        new ToolStripSeparator(),
                        new ShowAllToolStripItem(),
                        new ToolStripSeparator(),
                        new AutoFilterToolStripItem() { MaxCount = FilterItemMaxCount }
                    }
                    .ToArray());
                }
                contextMenu.Closed += HeaderDropDownContextMenu_Closed;
                contextMenu.Items[3].Click += ShowAllToolStripItemItem_Click;

                headerCell.ShowDropDownButtonImages = true;
                headerCell.HideDropDownButtonVisualEffect = true;
                headerCell.DropDownContextMenuStrip = contextMenu;

                headerCell.FilterCellName = dataCell.Name;

                headerCell.Size = new Size(headerCell.Width, templHeaderHeight);
                headerCell.Location = new Point(diffX + headerCell.Location.X, headerCell.Location.Y);
                headerCell.FlatStyle = FlatStyle.Flat;

                // テンプレート設定値の退避
                var templateHeaderCellStyle = headerCell.Style;

                headerCell.Style = new CustomMultiRowCellStyle().HeaderCellStyle;

                // テンプレート設定値の再設定
                if (templateHeaderCellStyle.BackColor.IsEmpty == false)
                {
                    // 背景色
                    headerCell.Style.BackColor = templateHeaderCellStyle.BackColor;
                }
                if (templateHeaderCellStyle.ForeColor.IsEmpty == false)
                {
                    // 文字色
                    headerCell.Style.ForeColor = templateHeaderCellStyle.ForeColor;
                }

                // ドロップダウン画像
                headerCell.DropDownButtonImages.Normal = Resources.SortGlyphDefaultImageWhite;
                headerCell.DropDownButtonImages.Filtered = Resources.SortGlyphFilterdDefaultImageWhite;
                headerCell.DropDownButtonImages.AscendingSorted = Resources.SortGlyphAscendingImageWhite;
                headerCell.DropDownButtonImages.DescendingSorted = Resources.SortGlyphDescendingImageWhite;
                headerCell.DropDownButtonImages.FilteredAndAscendingSorted = Resources.SortGlyphFilterdAscendingImageWhite;
                headerCell.DropDownButtonImages.FilteredAndDescendingSorted = Resources.SortGlyphFilterdDescendingImageWhite;

                headerCell.Tag = string.Format(HeaderTagPropertyFormat, displayIndex, dataCell.DataField);

                #endregion

                #region フィルタテキストボックス作成

                var filteringTextBox = new FilteringTextBoxCell()
                {
                    CustomComparisonOperator = _customFilterComparer,
                    FilterComparisonOperator = FilterComparisonOperator.Custom,
                    FilteringCellName = dataCell.Name,
                    Location = new Point(headerCell.Location.X, templHeaderHeight),
                    Name = string.Format(FilteringTextBoxCellNameFormat, headerCell.Name),
                    Size = new Size(headerCell.Width, TemplateRowHeight),
                    Style = new CellStyle()
                    {
                        BackColor = GetMultiRowColor(FilterRowBackColor),
                        ForeColor = GetMultiRowColor(FilterRowForeColor),
                        TextAlign = MultiRowContentAlignment.MiddleLeft
                    }
                };

                if (dataCell is CheckBoxCell || dataCell is ComboBoxCell)
                {
                    filteringTextBox.ReadOnly = true;
                    filteringTextBox.Style = new CellStyle()
                    {
                        BackColor = Color.WhiteSmoke,
                        ForeColor = GetMultiRowColor(FilterRowForeColor)
                    };
                }

                tmpl.ColumnHeaders[0].Cells.Add(filteringTextBox);

                displayIndex++;

                #endregion

                #region データセル作成

                if (dataCell is TextBoxCell || dataCell is CheckBoxCell || dataCell is ComboBoxCell || dataCell is DateTimePickerCell ||
                    dataCell is LinkLabelCell || dataCell is ButtonCell || dataCell is LabelCell)
                {
                    dataCell.Size = new Size(dataCell.Width, TemplateRowHeight);
                    dataCell.Location = new Point(diffX + dataCell.Location.X, dataCell.Location.Y);

                    // テンプレート設定値の退避
                    var templateDataCellStyle = dataCell.Style;

                    var style = new CustomMultiRowCellStyle().DataCellStyle;

                    dataCell.Style.Font = style.Font;
                    dataCell.Style.BackColor = templateDataCellStyle.BackColor.IsEmpty == false ? templateDataCellStyle.BackColor : style.BackColor;
                    dataCell.Style.ForeColor = templateDataCellStyle.ForeColor.IsEmpty == false ? dataCell.Style.ForeColor = templateDataCellStyle.ForeColor : style.ForeColor;
                }

                #endregion
            }

            // 表示設定の反映
            ApplyDisplay(ConfigDispLayList, tmpl.ColumnHeaders[0], tmpl.Row);

            // MultiRow共通マウスホイールのセット
            SetMultiRowMouseWheel();

            // MultiRow共通イベントのセット
            SetMultiRowEvent();

            return tmpl;
        }

        /// <summary>
        /// MultiRow共通マウスホイールのセット
        /// </summary>
        private void SetMultiRowMouseWheel()
        {
            if (MultiRow == null)
            {
                return;
            }

            MultiRow.MouseWheelCount = new GridAppConfigAccessor().GetGridMouseWheelCount();
            MultiRow.VerticalScrollCount = MultiRow.MouseWheelCount;
        }

        /// <summary>
        /// MultiRow共通イベントのセット
        /// </summary>
        private void SetMultiRowEvent()
        {
            if (MultiRow == null)
            {
                return;
            }

            if (this.RowCountLabel != null || this.GroupCountLabel != null)
            {
                // カウントラベルの初期化
                SetCountLabel();

                // MultiRow編集セル値変更イベント追加
                MultiRow.CellEditedFormattedValueChanged += MultiRow_CellEditedFormattedValueChanged;
            }

            // MultiRowクリックイベント追加
            MultiRow.Click += MultiRow_Click;

            // エラーハンドリングを行う場合
            if (IsValidate)
            {
                // MultiRowセル値検証イベント追加
                MultiRow.CellValidating += MultiRow_CellValidating;

                // MultiRow編集セル表示イベント追加
                MultiRow.EditingControlShowing += MultiRow_EditingControlShowing;
            }

            // MultiRowデータエラーイベント追加
            MultiRow.DataError += MultiRow_DataError;
        }

        #endregion

        #region 共通操作

        /// <summary>
        /// データバインド
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="bindingSource"></param>
        /// <remarks>カスタムテンプレートを適用したMultiRowにデータをバインドし、共通処理を実行します</remarks>
        public void SetDataSource(object dataSource, BindingSource bindingSource = null)
        {
            // フィルタリングのクリア
            MultiRow.ClearAllFilters();

            // データバインド
            if (MultiRow.DataSource is BindingSource)
            {
                // バインディングデータソース
                bindingSource.DataSource = dataSource;
            }
            else
            {
                // MultiRowデータソース
                MultiRow.DataSource = dataSource;
            }

            // ソートの反映
            ApplySort(this.sortList);

            // カウントラベルのセット
            SetCountLabel();
        }

        #endregion

        #region 表示設定

        /// <summary>
        /// 表示の取得
        /// </summary>
        /// <param name="flg"></param>
        private IEnumerable<MultiRowDisplayModel> GetDisplay(bool flg = false)
        {
            return MultiRow.ColumnHeaders[0].Cells
                .Where(x => string.IsNullOrWhiteSpace((string)x.Tag) == false && string.IsNullOrWhiteSpace((string)x.Value) == flg && x.Width > 0)
                .Select(x => new MultiRowDisplayModel
            {
                // 列名
                Name = x.Name,

                // ヘッダ名
                HeaderText = (string)x.Value,

                // データ名
                DataPropertyName = GetTagValue((string)x.Tag, Const.DataField),

                // 表示
                Visible = x.Visible,

                // 表示順
                DisplayIndex = Convert.ToInt16(GetTagValue((string)x.Tag, Const.DisplayIndex))

            }).ToArray();
        } 

        /// <summary>
        /// 表示の反映
        /// </summary>
        /// <param name="list"></param>
        /// <param name="template"></param>
        private void ApplyDisplay(IEnumerable<MultiRowDisplayModel> list, Template template)
        {
            // 表示設定対象外列の取得
            var mergeList = GetDisplay(true).ToList();

            foreach (var col in list)
            {
                // 表示順の調整
                col.DisplayIndex = col.DisplayIndex + mergeList.Where(x => x.DisplayIndex <= col.DisplayIndex).Count();
            }

            // リストのマージ
            mergeList.AddRange(list.ToList());

            // 表示の反映
            ApplyDisplay(mergeList, template.ColumnHeaders[0], template.Row);

            // テンプレートの再割り当て
            MultiRow.Template = template;
        }

        /// <summary>
        /// 表示の反映
        /// </summary>
        /// <param name="list"></param>
        /// <param name="header"></param>
        /// <param name="row"></param>
        private void ApplyDisplay(IEnumerable<MultiRowDisplayModel> list, ColumnHeaderSection header, Row row)
        {
            if (MultiRow == null)
            {
                return;
            }

            if (list == null || list.Any() == false)
            {
                return;
            }

            var headerCellList = header.Cells.ToList();
            var dataCellList = row.Cells.ToList();

            var pointX = IsRowHeader ? RowHeaderWidth : 0;

            //Update Start 2022/04/05 杉浦 管理票検索の表示順がおかしい
            //foreach (var item in list.OrderBy(x => x.DisplayIndex))
            //{
            //    var headerCell = headerCellList.First(x => x.Name == item.Name);
            //    var filterCell = headerCellList.First(x => x.Name == string.Format(FilteringTextBoxCellNameFormat, item.Name));

            //    var dataCell = dataCellList.First(x => x.DataField == item.DataPropertyName);

            //    // 表示
            //    headerCell.Visible = filterCell.Visible = dataCell.Visible = item.Visible;

            //    if (item.Visible)
            //    {
            //        // 表示位置
            //        headerCell.Location = new Point(pointX, headerCell.Location.Y);
            //        filterCell.Location = new Point(pointX, filterCell.Location.Y);

            //        dataCell.Location = new Point(pointX, dataCell.Location.Y);

            //        pointX += headerCell.Size.Width;
            //    }

            //    // タグ
            //    headerCell.Tag = string.Format(HeaderTagPropertyFormat, Convert.ToInt16(item.DisplayIndex), item.DataPropertyName);
            //}

            foreach (var item in headerCellList.Where(x => !x.Name.Contains("FilteringTextBoxCell")))
            {
                var listCell = list.FirstOrDefault(x => x.Name == item.Name);
                var filterCell = headerCellList.FirstOrDefault(x => x.Name == string.Format(FilteringTextBoxCellNameFormat, item.Name));

                var dataCell = dataCellList.FirstOrDefault(x => x.CellIndex == item.CellIndex);

                if (listCell != null)
                {
                    // 表示
                    item.Visible = filterCell.Visible = dataCell.Visible = listCell.Visible;

                    if (item.Visible)
                    {
                        // 表示位置
                        item.Location = new Point(pointX, item.Location.Y);
                        filterCell.Location = new Point(pointX, filterCell.Location.Y);

                        dataCell.Location = new Point(pointX, dataCell.Location.Y);

                        pointX += item.Size.Width;
                    }

                    // タグ
                    item.Tag = string.Format(HeaderTagPropertyFormat, Convert.ToInt16(listCell.DisplayIndex), listCell.DataPropertyName);
                }
                else
                {
                    if (filterCell != null && dataCell != null)
                    {
                        item.Visible = filterCell.Visible = dataCell.Visible = false;
                    }
                }
            }
            //Update Start 2022/04/05 杉浦 管理票検索の表示順がおかしい
        }

        #endregion

        #region ソート指定

        /// <summary>
        /// ソートの反映
        /// </summary>
        /// <param name="list"></param>
        private void ApplySort(IEnumerable<MultiRowSortModel> list)
        {
            if (MultiRow == null || MultiRow.RowCount <= 0)
            {
                return;
            }

            if (list == null || list.Any() == false)
            {
                return;
            }

            var items = new SortItem[list.Count()];

            for (var i = 0; i < list.Count(); i++)
            {
                items[i] = new SortItem(list.ElementAt(i).Name, list.ElementAt(i).IsDesc ? SortOrder.Descending : SortOrder.Ascending);
            }

            MultiRow.Sort(items, 0, MultiRow.RowCount - 1);
        }

        /// <summary>
        /// ソート対象を取得
        /// </summary>
        /// <returns></returns>
        public List<MultiRowSortModel> GetSortTarget()
        {
            if (MultiRow == null)
            {
                return null;
            }

            var map = new Dictionary<MultiRowSortModel, int>();

            foreach (var cell in MultiRow.Template.ColumnHeaders[0].Cells)
            {
                // タグがない場合
                if (string.IsNullOrWhiteSpace((string)cell.Tag))
                {
                    continue;
                }

                var dataField = GetTagValue((string)cell.Tag, Const.DataField);
                var displayIndex = Convert.ToInt16(GetTagValue((string)cell.Tag, Const.DisplayIndex));

                if (cell.Width > 0 && cell.Visible == true && string.IsNullOrWhiteSpace((string)cell.Value) == false && string.IsNullOrWhiteSpace(dataField) == false)
                {
                    map[new MultiRowSortModel { Name = (string)cell.Value, DataPropertyName = dataField, IsDesc = false }] = displayIndex;
                }
            }

            return map.OrderBy(x => x.Value).Select(x => x.Key).ToList();
        }

        #endregion

        #region カウントラベル

        /// <summary>
        /// カウントラベルのセット
        /// </summary>
        public void SetCountLabel()
        {
            if (MultiRow == null)
            {
                return;
            }

            // 行カウントラベルが設定されている場合
            if (RowCountLabel != null)
            {
                // 行カウントラベルのセット
                RowCountLabel.Text = string.Format(RowCountFormat,
                    MultiRow.Rows.GetRowCount(MultiRowElementStates.Visible).ToString(),
                    MultiRow.Rows.GetRowCount(MultiRowElementStates.None).ToString());
            }

            // グループカウントラベルが設定されている場合
            if (GroupCountLabel != null)
            {
                // グループカウントラベルのセット
                if (GroupCountCellName != null)
                {
                    // セル名でグループ化
                    GroupCountLabel.Text = string.Format(GroupCountFormat,
                        MultiRow.Rows.Where(x => x.State.HasFlag(MultiRowElementStates.Visible)).GroupBy(x => x.Cells[GroupCountCellName].Value).Count().ToString(),
                        MultiRow.Rows.GroupBy(x => x.Cells[GroupCountCellName].Value).Count().ToString());
                }
                else
                {
                    // セルインデックスでグループ化
                    GroupCountLabel.Text = string.Format(GroupCountFormat,
                        MultiRow.Rows.Where(x => x.State.HasFlag(MultiRowElementStates.Visible)).GroupBy(x => x.Cells[GroupCountCellIndex].Value).Count().ToString(),
                        MultiRow.Rows.GroupBy(x => x.Cells[GroupCountCellIndex].Value).Count().ToString());
                }
            }
        }

        #endregion

        #region MultiRowイベント

        /// <summary>
        /// MultiRowクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiRow_Click(object sender, EventArgs e)
        {
            if (((GcMultiRow)sender).CurrentCell != null && ((GcMultiRow)sender).CurrentCell is FilteringTextBoxCell)
            {
                ((GcMultiRow)sender).BeginEdit(true);
            }
        }

        /// <summary>
        /// MultiRow編集セル値変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiRow_CellEditedFormattedValueChanged(object sender, CellEditedFormattedValueChangedEventArgs e)
        {
            if (((GcMultiRow)sender).CurrentCell is FilteringTextBoxCell)
            {
                // カウントラベルのセット
                SetCountLabel();
            }
        }

        /// <summary>
        /// MultiRowセル値検証イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiRow_CellValidating(object sender, CellValidatingEventArgs e)
        {
            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = MultiRow.Rows[e.RowIndex];
            var cell = row.Cells[e.CellIndex];
            var type = cell.ValueType;

            // 読み取り専用なら検証なし
            if (MultiRow.ReadOnly == true || row.ReadOnly == true || cell.ReadOnly == true)
            {
                return;
            }
            // テキストボックスの列以外は検証なし
            else if (cell is TextBoxCell == false)
            {
                return;
            }
            // 日付型は検証なし
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return;
            }
            // 文字列型は検証なし
            else if (type == typeof(string))
            {
                return;
            }

            var edit = MultiRow.EditingControl;

            // 編集コントロールが取得できなければ終了
            if (edit == null)
            {
                return;
            }

            var name = MultiRow.ColumnHeaders[0].Cells[e.CellIndex].Value?.ToString();
            var value = e.FormattedValue;
            var str = value == null ? string.Empty : value.ToString();

            if (string.IsNullOrWhiteSpace(str) == false)
            {
                var i = 0;

                // 数値に変換できない場合
                if (int.TryParse(str, out i) == false)
                {
                    // 背景色変更
                    edit.BackColor = Const.ErrorBackColor;

                    // 入力エラー文言表示
                    Messenger.Warn(string.Format(UICommon.Properties.Resources.KKM00025, name.Replace(Const.CrLf, string.Empty)));

                    // キャンセル可否
                    e.Cancel = true;
                }
            }

            // TextBoxCell int?型DBNull対応
            if (value == null)
            {
                cell.Value = value;
            }
        }

        /// <summary>
        /// データグリッド編集コントロール表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiRow_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            var cell = MultiRow.CurrentCell;
            var type = cell.ValueType;

            // テキストボックスの列以外は検証なし
            if (cell is TextBoxCell == false)
            {
                return;
            }
            // 日付型は検証なし
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return;
            }
            // 文字列型は検証なし
            else if (type == typeof(string))
            {
                return;
            }
            //書式が設定されてなければ終了
            else if (string.IsNullOrWhiteSpace(cell.Style.Format) == true)
            {
                return;
            }

            var edit = MultiRow.EditingControl;

            // 編集コントロールが取得できなければ終了
            if (edit == null)
            {
                return;
            }

            // 書式適用前の値を設定
            edit.Text = cell.Value == null ? string.Empty : cell.Value.ToString();
        }

        /// <summary>
        /// MultiRowデータエラーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiRow_DataError(object sender, DataErrorEventArgs e)
        {
            // MultiRowセルコントローラ不具合対応
            if (e.Exception.HResult == -2147024809 ||
                e.Exception.HResult == -2146233033 ||
                e.Exception.HResult == -2146233086)
            {
                // 例外は無視
                e.ThrowException = false;

                return;
            }

            e.ThrowException = true;
        }

        /// <summary>
        /// ヘッダコンテキストメニューClosedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeaderDropDownContextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            // カウントラベルのセット
            SetCountLabel();
        }

        /// <summary>
        /// フィルタクリアアイテムクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAllToolStripItemItem_Click(object sender, EventArgs e)
        {
            // カウントラベルのセット
            SetCountLabel();
        }

        #endregion

        #region メンバメソッド

        /// <summary>
        /// Configより各Color名を取得
        /// </summary>
        /// <param name="colorName"></param>
        /// <returns></returns>
        private static Color GetMultiRowColor(string colorName)
        {
            var cc = new ColorConverter();
            var selectColor = (Color)cc.ConvertFromString(ConfigurationManager.AppSettings[colorName]);
            return selectColor;
        }

        /// <summary>
        /// HeaderCellタグ値の取得
        /// </summary>
        /// <param name="val"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetTagValue(string val, string name)
        {
            return Regex.Replace(val.Split(';').FirstOrDefault(x => x.Contains(name) == true), @"^.+?\((.+)\)$", "$1")
            .Replace("DisplayIndex()", string.Empty)
            .Replace("DataField()", string.Empty);
        }

        #endregion
    }
}