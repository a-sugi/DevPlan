using DevPlan.UICommon.Config;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils.Calendar;
using DevPlan.UICommon.Utils.Calendar.Templates;
using GrapeCity.Win.CalendarGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevPlan.UICommon.Utils
{
    /// <summary>
    /// ClendarGridUtil
    /// </summary>
    public class CalendarGridUtil<Item, Schedule>
        where Item : class, new()
        where Schedule : class, new()
    {
        #region メンバ変数
        private GcCalendarGrid scheduleCalendarGrid;

        private ToolTip toolTip;
        private ToolTip orgToolTip;

        private TrackBar zoomTrackBar;
        private Button noZoomButton;

        private CalendarTitleLabel schedulePeriodLabel;
        private CalendarTitleButton schedulePrevButton;
        private CalendarTitleButton scheduleNextButton;

        private ContextMenuStrip cornerHeaderContexMenu;
        private ContextMenuStrip rowHeaderContexMenu;
        private ContextMenuStrip scheduleContexMenu;

        private CalendarCellPosition selectCellPosition;

        private Point dragStartPosition = Point.Empty;

        private ScheduleModel<Schedule> copySchedule = null;

        private DateTime[] holidays = null;
        private DateTime[] workdays = null;

        private Dictionary<long, List<int>> ScheduleRowNoDic;

        /// <summary>マウス位置保持内部フィールド</summary>
        private Point location = default(Point);

        /// <summary>ツールチップ表示フラグ</summary>
        private bool showToolTip = false;

        /// <summary>やり直し/元に戻す管理クラス</summary>
        public UndoManager<ScheduleModel<Schedule>> UndoRedoManager = new UndoManager<ScheduleModel<Schedule>>();

        #endregion

        #region 定数

        /// <summary>下線</summary>
        private readonly CalendarBorderLine BottomBorder = new CalendarBorderLine(Color.Black, BorderLineStyle.Thin);

        /// <summary>スケジュールのタイトルヘッダーの高さ</summary>
        private const int ScheduleTitleHeaderHeight = 40;

        /// <summary>カレンダーコーナーヘッダの１行の高さ</summary>
        private const int ScheduleCornerHeaderHeight = 21;

        /// <summary>スケジュールの行の高さ（複数行あり）※複数行ある場合の１行の高さ</summary>
        private const int ScheduleRowHeight = 40;

        /// <summary>スケジュールの行の高さ（複数行なし）※複数行ない場合の１行の高さ</summary>
        private const int ScheduleNoneRowHeight = 58;

        /// <summary>スケジュール日時フォーマット</summary>
        private const string ScheduleDateTimeFormat = "{0:yyyy/MM/dd HH:mm}";

        /// <summary>スケジュール日付フォーマット</summary>
        private const string ScheduleDateFormat = "{0:yyyy/MM/dd}";

        #endregion

        #region プロパティ
        /// <summary>カレンダーグリッド設定</summary>
        public CalendarGridConfigModel<Item, Schedule> CalendarGridConfig { get; set; }

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }

        /// <summary>バインドカレンダーアイテムリスト</summary>
        private List<CalendarItemModel<Item, Schedule>> BindCalendarItemList { get; set; }

        // <summary>スケジュール表示期間の変更後のデリゲート</summary>
        public Action<DateTime, DateTime> ScheduleViewPeriodChangedAfter { get; set; }

        /// <summary>スケジュール行ヘッダーダブルクリックのデリゲート</summary>
        public Action<ScheduleItemModel<Item>> ScheduleRowHeaderDoubleClick { get; set; }

        /// <summary>スケジュールシングルのデリゲート</summary>
        public Action<ScheduleModel<Schedule>, MouseButtons> ScheduleSingleClick { get; set; }

        /// <summary>スケジュールダブルクリックのデリゲート</summary>
        public Action<ScheduleModel<Schedule>> ScheduleDoubleClick { get; set; }

        /// <summary>スケジュールの日付範囲の変更後のデリゲート</summary>
        public Action<ScheduleModel<Schedule>> ScheduleDayRangeChangedAfter { get; set; }

        /// <summary>スケジュールの空白領域をドラッグ後のデリゲート</summary>
        public Action<ScheduleModel<Schedule>> ScheduleEmptyDragAfter { get; set; }

        /// <summary>スケジュール空白領域をダブルクリックのデリゲート</summary>
        public Action<ScheduleModel<Schedule>> ScheduleEmptyDoubleClick { get; set; }

        /// <summary>スケジュール削除のデリゲート</summary>
        public Action<ScheduleModel<Schedule>> ScheduleDelete { get; set; }

        /// <summary>スケジュール貼り付けのデリゲート</summary>
        public Action<ScheduleModel<Schedule>, ScheduleModel<Schedule>> SchedulePaste { get; set; }

        /// <summary>スケジュール項目の並び順変更後のデリゲート</summary>
        public Action<ScheduleItemModel<Item>, ScheduleItemModel<Item>> ScheduleItemSortChangedAfter { get; set; }

        /// <summary>カレンダーテンプレートセル描画処理のデリゲート</summary>
        public Action<CalendarCell, CalendarItemModel<Item, Schedule>, int, int> CalendarCellPaint { get; set; }

        /// <summary>カレンダーセル項目ごとの描画後の処理のデリゲート</summary>
        public Action<ScheduleItemModel<Item>> CalendarScheduleCellPaint { get; set; }

        /// <summary>カレンダーUNDOREDO機能</summary>
        public Func<ScheduleModel<Schedule>, bool> UndoRedo { get; set; }

        private string _cornerHeaderText = "";
        /// <summary>コーナーヘッダー</summary>
        public string CornerHeaderText
        {
            get
            {
                var value = _cornerHeaderText;
                return value == null ? "" : value.ToString();
            }
            set
            {
                _cornerHeaderText = value;
                this.scheduleCalendarGrid.Template.CornerHeader.CellStyle.Image = null;

                //コーナーヘッダ画像化（高DPI対応）
                int imageHeight = 0;
                if (_cornerHeaderText != null && string.IsNullOrEmpty(_cornerHeaderText) == false)
                {
                    var font = new Font(ControlFont.DefaultFont.Font.Name, float.Parse(this.CalendarSetting.CurrentStyle.FontSize));
                    var width = this.scheduleCalendarGrid.Template.CornerHeader.Columns[0].Width + this.scheduleCalendarGrid.Template.CornerHeader.Columns[1].Width;
                    var cellStyle = this.scheduleCalendarGrid.Template.CornerHeader.CellStyle;

                    using (var checkCanvas = new Bitmap(1, 1))
                    using (var g = Graphics.FromImage(checkCanvas))
                    {
                        imageHeight = (int)g.MeasureString(
                                CornerHeaderText, font, width, StringFormat.GenericDefault).Height + 2;
                    }
                    SolidBrush b = new SolidBrush(Color.Black);
                    var bmp = new Bitmap(width, imageHeight);
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.DrawString(
                                CornerHeaderText, font, b, new Rectangle(1, 1, width, imageHeight));
                    }
                    cellStyle.Image = bmp;
                    cellStyle.ImageAlign = CalendarGridContentAlignment.MiddleCenter;
                }

                int headerRowHeight = ScheduleCornerHeaderHeight * this.scheduleCalendarGrid.Template.ColumnHeaderRowCount;

                if (headerRowHeight < imageHeight)
                {
                    for (int i = 0; i < this.scheduleCalendarGrid.Template.ColumnHeaderRowCount; i++)
                    {
                        this.scheduleCalendarGrid.Template.ColumnHeader.Rows[i].Height =
                            (imageHeight / this.scheduleCalendarGrid.Template.ColumnHeaderRowCount);
                    }
                }
                else
                {
                    for (int i = 0; i < this.scheduleCalendarGrid.Template.ColumnHeaderRowCount; i++)
                    {
                        this.scheduleCalendarGrid.Template.ColumnHeader.Rows[i].Height = ScheduleCornerHeaderHeight;
                    }
                }
            }
        }

        /// <summary>
        /// カレンダー設定情報。
        /// </summary>
        public CalendarSettings CalendarSetting;

        /// <summary>
        /// 昇順メニューコントロール。
        /// </summary>
        private ToolStripMenuItem AscMenu;

        /// <summary>
        /// 降順メニューコントロール。
        /// </summary>
        private ToolStripMenuItem DescMenu;

        /// <summary>
        /// フィルタテキストボックス。
        /// </summary>
        private TextBox FilterTextBox;

        /// <summary>
        /// フィルタメニューコントロール。
        /// </summary>
        private ToolStripMenuItem TextFilterItem;

        /// <summary>
        /// 紐づけ無し車両検索フィルタアイテム。
        /// </summary>
        public ToolStripMenuItem SikenSysFilterItem { get; private set; }

        /// <summary>
        /// テンプレート切り替えラジオボタン格納パネル
        /// </summary>
        public Panel CalendarTypeRadioButtonPanel { get; private set; }

        /// <summary>
        /// 共通コンテキストメニュー。
        /// </summary>
        private ToolStripItem[] CommonContextMenu;

        /// <summary>
        /// カレンダーリストデータ（検索当時）
        /// </summary>
        private List<CalendarItemModel<Item, Schedule>> orgCalendarList;

        /// <summary>
        /// フォント変更コンボボックス。
        /// </summary>
        private FontComboBox fontComboBox;

        /// <summary>
        /// スケジュール空白エリアドラッグイベント呼び出しフラグ。
        /// </summary>
        private bool callScheduleEmptyDragAfter;

        /// <summary>
        /// カレンダーの左側へ表示する日付
        /// </summary>
        private DateTime LeftDate = DateTime.Now;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="config">カレンダーグリッド設定</param>
        public CalendarGridUtil(CalendarGridConfigModel<Item, Schedule> config)
        {
            //カレンダーグリッド設定
            this.CalendarGridConfig = config;

            //カレンダーグリッド初期化
            this.InitCalendarGrid();

            //スケジュールの期間変更初期化
            this.InitSchedulePeriod();

            //カレンダー上のコントロールの初期化
            this.InitControl();

            //カレンダーのコンテキストメニューの初期化
            this.InitContextMenu();

            //カレンダーのコンテキストメニュー（共通）の初期化          
            this.CommonContextMenu = CreateCommonContextMenu();

            //デリゲートの初期化
            this.InitDelegate();

            //カレンダー表示の設定
            this.SetCalendarViewPeriod(this.scheduleCalendarGrid.FirstDateInView);

            //カレンダーグリッドに休日を設定
            this.SetHoliday();

            this.SetTemplateHeader();

        }

        /// <summary>
        /// カレンダーグリッド共通コンテキストメニュー
        /// </summary>
        /// <returns></returns>
        private ToolStripItem[] CreateCommonContextMenu()
        {
            var list = new List<ToolStripItem>();

            this.AscMenu = new ToolStripMenuItem("昇順");
            this.AscMenu.Click += AscMenu_Click;
            list.Add(this.AscMenu);

            this.DescMenu = new ToolStripMenuItem("降順");
            this.DescMenu.Click += DescMenu_Click;
            list.Add(this.DescMenu);

            this.AscMenu.Visible = this.CalendarGridConfig.IsSortContextMenuVisible;
            this.DescMenu.Visible = this.CalendarGridConfig.IsSortContextMenuVisible;
            if (this.CalendarGridConfig.IsSortContextMenuVisible)
            {
                list.Add(new ToolStripSeparator());
            }

            Panel textFilterPanel = new Panel();
            textFilterPanel.BackColor = Color.Transparent;

            this.FilterTextBox = new TextBox()
            {
                Size = new Size(150, 25),
                Font = new Font(ControlFont.DefaultFont.Font.Name, 12)
            };
            textFilterPanel.Controls.Add(this.FilterTextBox);

            var filterButton = new Button()
            {
                Text = "適用",
                Location = new Point(150, 0)
            };
            filterButton.Size = new Size(filterButton.Width, FilterTextBox.Height);
            filterButton.Click += FilterButton_Click;
            textFilterPanel.Controls.Add(filterButton);

            ToolStripControlHost textFilterHost = new ToolStripControlHost(textFilterPanel);
            textFilterHost.Margin = Padding.Empty;
            textFilterHost.Padding = Padding.Empty;

            this.TextFilterItem = new ToolStripMenuItem("テキストフィルター");
            this.TextFilterItem.DropDownItems.Add(textFilterHost);
            list.Add(this.TextFilterItem);

            this.SikenSysFilterItem = new ToolStripMenuItem("紐づけ無し車両検索");
            SikenSysFilterItem.Click += SikenSysFilterItem_Click;
            list.Add(this.SikenSysFilterItem);
            this.SikenSysFilterItem.Visible = this.CalendarGridConfig.IsRowHeaderSysFilter;

            var filterClear = new ToolStripMenuItem("クリア");
            filterClear.Click += FilterClear_Click;
            list.Add(filterClear);

            return list.ToArray();
        }

        /// <summary>
        /// 紐づけ無し（試験車SYS紐づけ無し）車両検索。
        /// </summary>
        /// <remarks>
        /// 現在の表示データで管理票番号を所持していない項目で絞り込みを行います。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SikenSysFilterItem_Click(object sender, EventArgs e)
        {
            if (this.BindCalendarItemList == null) { return; }
            this.SikenSysFilterItem.Checked = true;
            BindFilter();
        }

        /// <summary>
        /// フィルタ適用ボタンクリック処理。
        /// </summary>
        /// <remarks>
        /// フィルタのテキストが入力されている場合、フィルタを適用したバインド処理を実施します。
        /// ※バインド中の場合はなにも実施されません。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterButton_Click(object sender, EventArgs e)
        {
            if (this.FilterTextBox.Text == "" || this.IsBind || this.BindCalendarItemList == null) { return; }
            BindFilter();
        }

        /// <summary>
        /// フィルタークリア処理。
        /// </summary>
        /// <remarks>
        /// ソートおよびフィルタで利用しているコントロールを解除後、
        /// 検索当初に保持しているデータにてバインドを実施します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterClear_Click(object sender, EventArgs e)
        {
            ResetFilter();
            Bind(this.orgCalendarList);
        }

        /// <summary>
        /// フィルタコントロールリセット処理。
        /// </summary>
        /// <remarks>
        /// フィルタ、ソートで利用しているコントロール状態をリセットします。
        /// </remarks>
        public void ResetFilter()
        {
            //例えば検索ボタンを押下時などにもリセットしたい場合は当メソッドを呼び出してください。

            this.AscMenu.Checked = false;
            this.DescMenu.Checked = false;
            this.FilterTextBox.Text = "";
            this.TextFilterItem.Checked = false;
            this.SikenSysFilterItem.Checked = false;
        }

        /// <summary>
        /// 降順メニュークリック処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescMenu_Click(object sender, EventArgs e)
        {
            if (this.BindCalendarItemList == null) { return; }

            var menu = (ToolStripMenuItem)sender;

            if (menu.Checked == false)
            {
                this.AscMenu.Checked = false;
                this.DescMenu.Checked = true;
            }
            else
            {
                this.AscMenu.Checked = false;
                this.DescMenu.Checked = false;
            }
            BindFilter();
        }

        /// <summary>
        /// 昇順メニュークリック処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AscMenu_Click(object sender, EventArgs e)
        {
            if (this.BindCalendarItemList == null) { return; }

            var menu = (ToolStripMenuItem)sender;

            if (menu.Checked == false)
            {
                this.AscMenu.Checked = true;
                this.DescMenu.Checked = false;
            }
            else
            {
                this.AscMenu.Checked = false;
                this.DescMenu.Checked = false;
            }
            BindFilter();
        }

        /// <summary>
        /// バインド処理（フィルタ、ソート適用）
        /// </summary>
        /// <remarks>
        /// フィルタ、ソートを適用後にBind処理を実施します。
        /// フィルタテキストがクリアされている場合は、検索時に保持したデータにて再ロードを行います。
        /// フィルタテキストが入力されている場合はフィルタのテキストにてフィルタを実施します。
        /// フィルタを実施した後にソートをかけ、Bindを実施します。
        /// </remarks>
        private void BindFilter()
        {
            List<CalendarItemModel<Item, Schedule>> data;

            if (this.FilterTextBox.Text == "")
            {
                this.TextFilterItem.Checked = false;
                data = new List<CalendarItemModel<Item, Schedule>>(orgCalendarList);
            }
            else
            {
                this.TextFilterItem.Checked = true;
                data = this.orgCalendarList.FindAll(x => x.ScheduleItem.Title.Contains(this.FilterTextBox.Text) == true);
            }

            if (this.SikenSysFilterItem.Checked)
            {
                data = data.FindAll(x => x.ScheduleItem.KanriNo == "" || x.ScheduleItem.KanriNo == null);
            }

            //ソートは再度実施する必要がある。する必要がなければそのまま。
            if (this.AscMenu.Checked)
            {
                data = data.OrderBy(x => x.ScheduleItem.Title).ToList();
            }
            else if (this.DescMenu.Checked)
            {
                data = data.OrderByDescending(x => x.ScheduleItem.Title).ToList();
            }

            Bind(data);
        }

        /// <summary>
        /// カレンダー上のコントロールの初期化。
        /// </summary>
        /// <remarks>
        /// カレンダー上のコントロールの初期化を行います。
        /// </remarks>
        private void InitControl()
        {
            #region Windows スケールの取得
            var scl = new DeviceUtil().GetScalingFactor();
            #endregion

            #region ズーム

            this.zoomTrackBar = new TrackBar()
            {
                AutoSize = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor,
                LargeChange = 10,
                SmallChange = 10,
                Maximum = 200,
                Minimum = 10,
                Location = new Point(this.scheduleCalendarGrid.Width - 165, 2),
                Size = new Size(160, 25),
                TickFrequency = 10,
                Value = 100
            };
            this.zoomTrackBar.Scroll += ScheduleZoomTrackBar_Scroll;
            this.scheduleCalendarGrid.Controls.Add(this.zoomTrackBar);
            this.scheduleCalendarGrid.Controls.Add(new Label()
            {
                AutoSize = false,
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor,
                Text = "10%              100%               200%",
                Font = ControlFont.GetNumberFont(8.25f / scl),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.scheduleCalendarGrid.Width - 165, 29),
                Size = new System.Drawing.Size(158, 11)
            });

            this.noZoomButton = new Button()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor,
                Text = (this.scheduleCalendarGrid.ZoomFactor * 100).ToString() + "%",
                Font = ControlFont.GetNumberFont(10.5f / scl),
                Location = new Point(this.scheduleCalendarGrid.Width - 213, 6),
                Size = new Size(45, 30),

            };
            this.noZoomButton.Click += NoZoomButton_Click;
            this.scheduleCalendarGrid.Controls.Add(this.noZoomButton);

            this.scheduleCalendarGrid.ZoomFactor = this.CalendarSetting.CurrentStyle.Zoom;
            #endregion

            #region 列幅
            this.scheduleCalendarGrid.Controls.Add(new WidthChangeLabel()
            {
                Text = "列幅",
                Font = ControlFont.GetNumberFont(9.5f / scl),
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor,
                Location = new Point(this.scheduleCalendarGrid.Width - 409, 16)
            });

            var colWidthPlusButton = new WidthChangeButton()
            {
                Text = "＋",
                Font = ControlFont.GetNumberFont(9.5f / scl),
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor,
                Location = new Point(this.scheduleCalendarGrid.Width - 373, 10)
            };
            colWidthPlusButton.Click += ColWidthPlusButton_Click;
            this.scheduleCalendarGrid.Controls.Add(colWidthPlusButton);

            var colWidthMinusButton = new WidthChangeButton()
            {
                Text = "－",
                Font = ControlFont.GetNumberFont(9.5f / scl),
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor,
                Location = new Point(this.scheduleCalendarGrid.Width - 343, 10)
            };
            colWidthMinusButton.Click += ColWidthMinusButton_Click;
            this.scheduleCalendarGrid.Controls.Add(colWidthMinusButton);
            #endregion

            #region 行幅
            this.scheduleCalendarGrid.Controls.Add(new WidthChangeLabel()
            {
                Text = "行幅",
                Font = ControlFont.GetNumberFont(9.5f / scl),
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor,
                Location = new Point(this.scheduleCalendarGrid.Width - 315, 16)
            });

            var rowWidthPlusButton = new WidthChangeButton()
            {
                Text = "＋",
                Font = ControlFont.GetNumberFont(9.5f / scl),
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor,
                Location = new Point(this.scheduleCalendarGrid.Width - 278, 10)
            };
            rowWidthPlusButton.Click += RowWidthPlusButton_Click;
            this.scheduleCalendarGrid.Controls.Add(rowWidthPlusButton);

            var rowWidthMinusButton = new WidthChangeButton()
            {
                Text = "－",
                Font = ControlFont.GetNumberFont(9.5f / scl),
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor,
                Location = new Point(this.scheduleCalendarGrid.Width - 248, 10)
            };
            rowWidthMinusButton.Click += RowWidthMinusButton_Click;
            this.scheduleCalendarGrid.Controls.Add(rowWidthMinusButton);
            #endregion

            #region レイアウト変更
            var standardRadioButton = new LayoutRadioButton(CalendarTemplateTypeSafeEnum.DEFAULT)
            {
                Width = 55,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "標準",
                Font = ControlFont.GetNumberFont(9.0f / scl),
                Location = new Point(11, 3),
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor
            };
            standardRadioButton.Checked = (this.CalendarSetting.CalendarMode == CalendarTemplateTypeSafeEnum.DEFAULT);
            standardRadioButton.Click += LayoutRadioButton_Click;

            var expansion1RadioButton = new LayoutRadioButton(CalendarTemplateTypeSafeEnum.EXPANSION1)
            {
                Width = 65,
                Text = "拡大１",
                TextAlign = ContentAlignment.MiddleLeft,
                Font = ControlFont.GetNumberFont(9.0f / scl),
                Location = new Point(68, 3),
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor
            };
            expansion1RadioButton.Checked = (this.CalendarSetting.CalendarMode == CalendarTemplateTypeSafeEnum.EXPANSION1);
            expansion1RadioButton.Click += LayoutRadioButton_Click;

            var expansion2RadioButton = new LayoutRadioButton(CalendarTemplateTypeSafeEnum.EXPANSION2)
            {
                Width = 65,
                Text = "拡大２",
                TextAlign = ContentAlignment.MiddleLeft,
                Font = ControlFont.GetNumberFont(9.0f / scl),
                Location = new Point(135, 3),
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor
            };
            expansion2RadioButton.Checked = (this.CalendarSetting.CalendarMode == CalendarTemplateTypeSafeEnum.EXPANSION2);
            expansion2RadioButton.Click += LayoutRadioButton_Click;
            #endregion

            CalendarTypeRadioButtonPanel = new Panel()
            {
                BackColor = scheduleCalendarGrid.TitleHeader.BackColor,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.scheduleCalendarGrid.Width - 620, 7),
                Size = new Size(208, 28),
                BorderStyle = BorderStyle.FixedSingle
            };
            CalendarTypeRadioButtonPanel.Controls.Add(standardRadioButton);
            CalendarTypeRadioButtonPanel.Controls.Add(expansion1RadioButton);
            CalendarTypeRadioButtonPanel.Controls.Add(expansion2RadioButton);
            this.scheduleCalendarGrid.Controls.Add(CalendarTypeRadioButtonPanel);

            fontComboBox = new FontComboBox() { Location = new Point(this.scheduleCalendarGrid.Width - 682, 10), Font = new Font("Arial", 9.0f / scl) };
            fontComboBox.SelectedIndex = fontComboBox.Items.IndexOf(this.CalendarSetting.CurrentStyle.FontSize);
            if (fontComboBox.SelectedIndex < 0) { fontComboBox.Text = this.CalendarSetting.CurrentStyle.FontSize; }
            fontComboBox.TextChanged += FontComboBox_TextChanged;
            this.scheduleCalendarGrid.Controls.Add(fontComboBox);
        }

        /// <summary>
        /// レイアウトラジオボタンクリック処理。
        /// </summary>
        /// <remarks>
        /// レイアウトラジオボタンがクリックされた際に処理されます。
        /// クリックされたラジオボタンが持っているTypeを元にテンプレートを初期化します。
        /// ただし、一番左へ表示している日付はテンプレートを切り替えた瞬間変わるため
        /// あらかじめ一番左へ表示されている日付は事前に保持をし、
        /// レイアウトを切り替えた後で再度設定します。
        /// （縦スクロールもカレンダーグリッドのセルが選択されている場合切替時にクリアされるため、
        /// 保持および再設定しています）
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutRadioButton_Click(object sender, EventArgs e)
        {
            var selectRadioButton = ((LayoutRadioButton)sender);

            if (selectRadioButton.Checked)
            {
                var verticalScrollOffset = this.scheduleCalendarGrid.VerticalScrollBarOffset;

                this.scheduleCalendarGrid.ClearAll();
                this.scheduleCalendarGrid.ColumnHeader.ClearAll();
                this.scheduleCalendarGrid.PerformRender();

                if (this.scheduleCalendarGrid.FirstDisplayedCellPosition.IsEmpty == false)
                {
                    this.LeftDate = this.scheduleCalendarGrid.FirstDisplayedCellPosition.Date;
                }

                this.CalendarSetting.CalendarMode = selectRadioButton.Type;
                this.scheduleCalendarGrid.Template = new CalendarTemplateFactory().CreateTemplate(this.CalendarSetting);

                //テンプレート切替時にも期間切り替えが必要になったので、バインドの代わりに以下を呼び出し。
                //期間変更制限があり、１ヶ月表示ではない場合は強制的にシステム日付へ戻す。
                if (this.CalendarGridConfig.IsScheduleViewPeriodChange == false && this.CalendarSetting.CurrentStyle.Range != 1)
                {
                    DateTime toDay = DateTime.Today;
                    this.SetCalendarViewPeriod(new DateTime(toDay.Year, toDay.Month, 1));
                }
                else
                {
                    this.SetCalendarViewPeriod(this.LeftDate);
                }

                SetTemplateHeader();

                this.ScheduleViewPeriodChangedAfter?.Invoke(this.scheduleCalendarGrid.FirstDateInView.Date, this.scheduleCalendarGrid.LastDateInView.Date);

                this.scheduleCalendarGrid.VerticalScrollBarOffset = verticalScrollOffset;

                if (this.selectCellPosition != null)
                {
                    this.SetScheduleMostDayFirst(this.selectCellPosition.Date);
                }
                else
                {
                    this.SetScheduleMostDayFirst(this.LeftDate);
                }
            }
        }

        /// <summary>
        /// 列幅＋ボタンクリックイベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColWidthPlusButton_Click(object sender, EventArgs e)
        {
            SetScheduleRowContentsHeight(+5);
        }

        /// <summary>
        /// 列幅－ボタンクリックイベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColWidthMinusButton_Click(object sender, EventArgs e)
        {
            SetScheduleRowContentsHeight(-5);
        }

        /// <summary>
        /// 列の幅再設定。
        /// </summary>
        /// <remarks>
        /// 列の幅をプログラムにより再設定します。
        /// ただし、幅が0以下になる列が発生する場合はそこでストップし、更新を行いません。
        /// </remarks>
        /// <param name="length">変更値</param>
        private void SetScheduleRowContentsHeight(int length)
        {
            for (var i = 0; i < this.scheduleCalendarGrid.Template.Content.Columns.Count; i++)
            {
                var width = this.scheduleCalendarGrid.Template.Content.Columns[i].Width + length;
                if (width <= 0) { return; }
                if (width > 150) { return; }
            }

            for (var i = 0; i < this.scheduleCalendarGrid.Template.Content.Columns.Count; i++)
            {
                this.scheduleCalendarGrid.Template.Content.Columns[i].Width += length;
            }

            this.CalendarSetting.CurrentStyle.HorizontalLengthUpdate += length;
        }

        /// <summary>
        /// 行の高さ＋ボタンクリックイベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowWidthPlusButton_Click(object sender, EventArgs e)
        {
            SetScheduleColContentsWidth(+5);
        }

        /// <summary>
        /// 行の高さ－ボタンクリックイベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowWidthMinusButton_Click(object sender, EventArgs e)
        {
            SetScheduleColContentsWidth(-5);
        }

        /// <summary>
        /// 行の高さ再設定。
        /// </summary>
        /// <remarks>
        /// 行の高さをプログラムにより再設定します。
        /// レイアウトによりColumnHeaderRowCountが異なるため、ColumnHeaderRowCountを除いた以降の高さを再設定します。
        /// ただし、幅が0以下になる行が発生する場合はそこでストップし、更新を行いません。
        /// </remarks>
        /// <param name="length">変更値</param>
        private void SetScheduleColContentsWidth(int length)
        {
            for (var i = 0; i < this.scheduleCalendarGrid.Template.RowCount; i++)
            {
                var height = this.scheduleCalendarGrid.Template.Content.Rows[i].Height + length;
                if (height <= 0) { return; }
                if (height > 300) { return; }
            }

            for (var i = 0; i < this.scheduleCalendarGrid.Template.RowCount; i++)
            {
                this.scheduleCalendarGrid.Template.Content.Rows[i].Height += length;
            }

            this.CalendarSetting.CurrentStyle.VerticalLengthUpdate += length;
        }

        /// <summary>
        /// フォントコンボボックス変更イベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontComboBox_TextChanged(object sender, EventArgs e)
        {
            var fontComboBox = ((FontComboBox)sender);

            if (fontComboBox.IsWithinFontSize())
            {
                ChangeFontSize(fontComboBox.SelectFontSize.ToString());
            }
        }

        /// <summary>
        /// フォントサイズ変更処理
        /// </summary>
        /// <param name="size"></param>
        private void ChangeFontSize(string size)
        {
            this.scheduleCalendarGrid.Template.Content.CellStyle.Font = new Font(ControlFont.DefaultFont.Font.Name, float.Parse(size));
            this.CalendarSetting.CurrentStyle.FontSize = size;

            int rowHeight = 0;
            for (var i = 0; i < this.scheduleCalendarGrid.Template.RowCount; i++)
            {
                var rowHeader = this.scheduleCalendarGrid.Template.RowHeader[i, 1];

                #region 項目名イメージが設定されているRowHeaderのみ再設定実施。
                if (rowHeader.CellStyle.Image != null)
                {
                    var item = rowHeader.Tag as ScheduleItemModel<Item>;

                    this.CalendarSetting.CurrentStyle.VerticalLengthUpdate = 0;//フォントサイズ変更時は幅リセットを実施
                    rowHeight = GetRowHeaderImageHeight(rowHeader.CellStyle, ScheduleRowNoDic[item.ID].Count(), item.Title,
                        this.scheduleCalendarGrid.Template.RowHeader.Columns[1].Width);
                }
                #endregion

                this.scheduleCalendarGrid.Template.Content.Rows[i].Height = rowHeight;
            }
        }

        /// <summary>
        /// カレンダーグリッド初期化
        /// </summary>
        private void InitCalendarGrid()
        {
            var toDay = DateTime.Today;

            var baseDay = new DateTime(toDay.Year, toDay.Month, 1);

            //対象のカレンダーグリッド
            this.scheduleCalendarGrid = this.CalendarGridConfig.CalendarGrid;

            // カレンダーグリッドの設定
            this.scheduleCalendarGrid.CalendarView = new CalendarListView { DateAlignment = DateAlignment.Day, DayCount = 90 };
            this.scheduleCalendarGrid.AllowClipboard = false;
            this.scheduleCalendarGrid.AllowDragPageScroll = false;
            this.scheduleCalendarGrid.AllowDrop = true;
            this.scheduleCalendarGrid.FirstDateInView = baseDay.AddMonths(-1);
            this.scheduleCalendarGrid.CurrentDate = toDay;
            this.scheduleCalendarGrid.Protected = true;
            this.scheduleCalendarGrid.ResizeMode = CalendarResizeMode.Horizontal;
            this.scheduleCalendarGrid.ShowCellToolTips = false;
            this.scheduleCalendarGrid.MaxDate = new DateTime(9998, 12, 31);
            this.scheduleCalendarGrid.MinDate = new DateTime(1753, 01, 01);
            this.scheduleCalendarGrid.VerticalScrollCount = new GridAppConfigAccessor().GetCalendarGridButtonScrollCount();

            //カレンダーグリッドのタイトルヘッダーの設定
            this.scheduleCalendarGrid.TitleHeader.Height = ScheduleTitleHeaderHeight;

            //カレンダーグリッドのタイトルフッターの設定
            this.scheduleCalendarGrid.TitleFooter.Visible = false;

            //カレンダーグリッドのスタイルを追加
            this.scheduleCalendarGrid.Styles.Add(new CalendarDynamicCellStyle
            {
                Name = "dynamicCellStyle",
                Condition = new DynamicCellStyleCondition(this.GetDynamicCellStyle)

            });
            this.scheduleCalendarGrid.Styles.Add(new CalendarConditionalCellStyle
            {
                Name = "headerStyle",
                ForeColor = Color.Black,
                BottomBorder = BottomBorder

            });

            this.CalendarSetting = this.CalendarGridConfig.CalendarSettings;
            this.scheduleCalendarGrid.Template = new CalendarTemplateFactory().CreateTemplate(this.CalendarSetting);

            #region 2018年上期リリース対象外
            // TODO : ■テストを行えない機能（2018年上期リリース対象外）につきコメントアウト。業務計画表、月次計画表リリースと共に修正。
            //////行ヘッダー（追加）の設定
            ////var addRowHeaderColumnMap = config.AddRowHeaderColumnMap;
            ////if (addRowHeaderColumnMap != null && addRowHeaderColumnMap.Any() == true)
            ////{
            ////    //行ヘッダー列数の設定
            ////    template.RowHeaderColumnCount += addRowHeaderColumnMap.Count();

            ////    foreach (var item in addRowHeaderColumnMap.Select((v, i) => new { val = v, idx = i + 1 }))
            ////    {
            ////        var addCornerHeaderCell = template.CornerHeader.Rows[0].Cells[item.idx];
            ////        addCornerHeaderCell.Value = item.val.Key;
            ////        addCornerHeaderCell.RowSpan = 2;

            ////        var addRowHeaderColumn = template.RowHeader.Columns[item.idx];
            ////        addRowHeaderColumn.Width = item.val.Value.Width > 0 ? item.val.Value.Width : ScheduleRowHeaderWidth;
            ////        addRowHeaderColumn.MinWidth = item.val.Value.MinWidth > 0 ? item.val.Value.MinWidth : ScheduleRowHeaderWidth;
            ////        addRowHeaderColumn.MaxWidth = item.val.Value.MaxWidth > 0 ? item.val.Value.MaxWidth : ScheduleRowHeaderWidth;
            ////        addRowHeaderColumn.CellStyle.Font = ControlFont.DefaultFont.Font;
            ////        addRowHeaderColumn.CellStyle.Alignment = CalendarGridContentAlignment.TopLeft;
            ////        addRowHeaderColumn.AllowResize = !(addRowHeaderColumn.Width == addRowHeaderColumn.MinWidth && addRowHeaderColumn.Width == addRowHeaderColumn.MaxWidth);
            ////    }
            ////}
            #endregion

            //ツールチップの初期化
            this.orgToolTip = new ToolTip();
            this.toolTip = this.orgToolTip;

            //コピーとペーストと削除のショートカットを削除
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.Copy);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.Cut);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.Paste);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.Clear);

            //セル操作のショートカットを削除
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveDown);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveLeft);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveRight);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveUp);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveToFirstCell);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveToLastCell);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveToNextCell);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveToNextDate);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveToNextPage);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveToPreviousCell);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveToPreviousPage);
            this.scheduleCalendarGrid.Commands.Remove(CalendarGridActions.MoveToToday);

            //イベントの紐づけ
            this.scheduleCalendarGrid.MouseDown += ScheduleCalendarGrid_MouseDown;
            this.scheduleCalendarGrid.MouseMove += ScheduleCalendarGrid_MouseMove;
            this.scheduleCalendarGrid.DragDrop += ScheduleCalendarGrid_DragDrop;
            this.scheduleCalendarGrid.DragOver += ScheduleCalendarGrid_DragOver;
            this.scheduleCalendarGrid.CellEnter += ScheduleCalendarGrid_CellEnter;
            this.scheduleCalendarGrid.CellValueChanged += ScheduleCalendarGrid_CellValueChanged;
            this.scheduleCalendarGrid.CellMouseUp += ScheduleCalendarGrid_CellMouseUp;
            this.scheduleCalendarGrid.MouseUp += ScheduleCalendarGrid_MouseUp;
            this.scheduleCalendarGrid.KeyDown += ScheduleCalendarGrid_KeyDown;
            this.scheduleCalendarGrid.CellMouseClick += ScheduleCalendarGrid_CellMouseClick;
            this.scheduleCalendarGrid.MouseWheel += ScheduleCalendarGrid_MouseWheel;

            this.scheduleCalendarGrid.FirstDateInViewChanged += (s, e) => SetTemplateHeader();
            this.scheduleCalendarGrid.ZoomFactorChanged += (s, e) => SetZoom();
        }

        /// <summary>
        /// マウスホイールイベント。
        /// </summary>
        /// <remarks>
        /// マウスホイールが移動された場合、スクロールの位置を増減させます。
        /// ※CalendarGridにはマウスホイールでのスクロール量を制御できないため。
        /// Ctl+マウスホイールの場合は処理を行いません。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_MouseWheel(object sender, MouseEventArgs e)
        {
            this.toolTip.Hide(sender as GcCalendarGrid);
            showToolTip = false;

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                return;
            }

            int count = this.CalendarGridConfig.MouseWheelCount;
            if (e.Delta > 0)
            {
                this.scheduleCalendarGrid.VerticalScrollBarOffset -= count;
            }
            else
            {
                this.scheduleCalendarGrid.VerticalScrollBarOffset += count;
            }
        }

        /// <summary>
        /// セルがマウスでクリックされたときに発生します。
        /// </summary>
        /// <remarks>
        /// コンテキストメニューを表示します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_CellMouseClick(object sender, CalendarCellMouseEventArgs e)
        {
            //右クリック以外は終了
            var isRight = e.Button == MouseButtons.Right;
            //カレンダーがデータで初期化されていない場合も何もできないため終了。
            if (isRight == false || (this.BindCalendarItemList == null))
            {
                return;
            }

            if (e.CellPosition.Scope == CalendarTableScope.CornerHeader)
            {
                //共通項目再設定
                foreach (var common in this.CommonContextMenu)
                {
                    this.rowHeaderContexMenu.Items.Remove(common);
                    this.cornerHeaderContexMenu.Items.Remove(common);
                    this.cornerHeaderContexMenu.Items.Add(common);
                }

                this.cornerHeaderContexMenu.Show(this.scheduleCalendarGrid, this.scheduleCalendarGrid.PointToClient(Control.MousePosition));
            }
            else if (e.CellPosition.Scope == CalendarTableScope.RowHeader)
            {
                var item = this.scheduleCalendarGrid.Template.RowHeader[e.CellPosition.RowIndex, 0].Tag as ScheduleItemModel<Item>;

                // コンテキストメニュー再設定
                if (this.CalendarGridConfig.GetRowHeaderContexMenuItems != null)
                {
                    this.rowHeaderContexMenu.Items.Clear();

                    foreach (var row in this.CalendarGridConfig.GetRowHeaderContexMenuItems(item))
                    {
                        this.rowHeaderContexMenu.Items.Add(row);
                    }
                }

                //コンテキストメニュー表示
                this.rowHeaderContexMenu.Tag = item;

                //共通項目再設定
                foreach (var common in this.CommonContextMenu)
                {
                    this.cornerHeaderContexMenu.Items.Remove(common);
                    this.rowHeaderContexMenu.Items.Remove(common);
                    this.rowHeaderContexMenu.Items.Add(common);
                }

                if (this.CalendarGridConfig.GetRowHeaderCommonContexMenuItems != null)
                {
                    foreach (var row in this.CalendarGridConfig.GetRowHeaderCommonContexMenuItems(item))
                    {
                        this.rowHeaderContexMenu.Items.Add(row);
                    }
                }

                this.rowHeaderContexMenu.Show(this.scheduleCalendarGrid, this.scheduleCalendarGrid.PointToClient(Control.MousePosition));
            }
            else if (e.CellPosition.Scope == CalendarTableScope.Content)
            {
                //コンテンツエリアコンテキストメニュー再設定（もともとマウスダウンイベントにあった処理を移動）
                var cp = e.CellPosition;

                var item = this.scheduleCalendarGrid.Template.RowHeader[e.CellPosition.RowIndex, 0].Tag as ScheduleItemModel<Item>;
                var schedule = this.scheduleCalendarGrid.Content[cp.Date].Rows[cp.RowIndex].Cells[cp.ColumnIndex].Tag as ScheduleModel<Schedule>;

                if (schedule != null)
                {
                    this.ScheduleSingleClick?.Invoke(schedule, e.Button);
                }

                //スケジュール編集権限がなければ終了（削除権限(特殊)があれば継続）
                if ((this.CalendarGridConfig.IsScheduleEdit == false || schedule?.IsEdit == false) && schedule?.IsDelete == false)
                {
                    return;
                }

                if (this.CalendarGridConfig.GetContentContexMenuItems != null)
                {
                    if (schedule != null)
                    {
                        this.scheduleContexMenu.Items.Clear();

                        foreach (var row in this.CalendarGridConfig.GetContentContexMenuItems(schedule))
                        {
                            this.scheduleContexMenu.Items.Add(row);
                        }

                        this.scheduleContexMenu.Tag = schedule;
                        this.scheduleContexMenu.Show(this.scheduleCalendarGrid, this.scheduleCalendarGrid.PointToClient(Control.MousePosition));
                    }
                }
                else
                {
                    if (schedule != null)
                    {
                        this.scheduleContexMenu.Tag = schedule;
                        this.scheduleContexMenu.Show(this.scheduleCalendarGrid, this.scheduleCalendarGrid.PointToClient(Control.MousePosition));
                    }
                }
            }
        }

        /// <summary>
        /// カレンダーズームファクターチェンジ処理。
        /// </summary>
        private void SetZoom()
        {
            if (this.scheduleCalendarGrid.ZoomFactor > 2.0f)
            {
                this.scheduleCalendarGrid.ZoomFactor = 2.0f;
            }
            else if (this.scheduleCalendarGrid.ZoomFactor < 0.1f)
            {
                this.scheduleCalendarGrid.ZoomFactor = 0.1f;
            }

            this.noZoomButton.Text = (this.scheduleCalendarGrid.ZoomFactor * 100).ToString() + "%";
            this.zoomTrackBar.Value = (int)(this.scheduleCalendarGrid.ZoomFactor * 100f);
            this.CalendarSetting.CurrentStyle.Zoom = this.scheduleCalendarGrid.ZoomFactor;
        }

        /// <summary>
        /// テンプレートヘッダー設定。
        /// </summary>
        /// <remarks>
        /// 設定されているテンプレートによりヘッダーカスタマイズを実施します。
        /// </remarks>
        public void SetTemplateHeader()
        {
            this.fontComboBox.SelectedIndex = this.fontComboBox.Items.IndexOf(this.CalendarSetting.CurrentStyle.FontSize);
            this.scheduleCalendarGrid.ZoomFactor = this.CalendarSetting.CurrentStyle.Zoom;

            if (this.scheduleCalendarGrid.Template.GetType() == typeof(DefaultCalendarTemplate))
            {
                ((DefaultCalendarTemplate)this.scheduleCalendarGrid.Template).SetBorder(
                    this.scheduleCalendarGrid.FirstDateInView,
                    this.scheduleCalendarGrid.LastDateInView,
                    this.scheduleCalendarGrid.ColumnHeader, this.scheduleCalendarGrid.Content);
            }
        }

        /// <summary>
        /// スケジュールの期間変更初期化
        /// </summary>
        private void InitSchedulePeriod()
        {
            //スケジュールの表示期間
            this.schedulePeriodLabel = new CalendarTitleLabel
            {
                AutoSize = true,
                Name = "SchedulePeriodLabel",
                TextImageRelation = TextImageRelation.ImageAboveText,
                Padding = new Padding(3, 0, 6, 0),
                Size = new Size(150, 30),

            };
            this.scheduleCalendarGrid.TitleHeader.Children.Add(this.schedulePeriodLabel);

            //スケジュール表示期間変更(前月)
            this.schedulePrevButton = new CalendarTitleButton
            {
                AutoSize = true,
                ButtonBehavior = CalendarTitleButtonBehavior.None,
                Name = "SchedulePrevButton",
                Text = "<<",

            };
            //Update Start 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理
            //this.schedulePrevButton.Click += (sender, e) => this.ScheduleViewPeriodChanged(this.schedulePrevButton.GotoDate);
            this.schedulePrevButton.Click += (sender, e) => this.ScheduleViewPeriodChanged(this.schedulePrevButton.GotoDate, this.CalendarGridConfig.ViewRange);
            //Update End 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理

            //スケジュール表示期間変更(翌月)
            this.scheduleNextButton = new CalendarTitleButton
            {
                AutoSize = true,
                ButtonBehavior = CalendarTitleButtonBehavior.None,
                Name = "ScheduleNextButton",
                Text = ">>",

            };
            //Update Start 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理
            //this.scheduleNextButton.Click += (sender, e) => this.ScheduleViewPeriodChanged(this.scheduleNextButton.GotoDate);
            this.scheduleNextButton.Click += (sender, e) => this.ScheduleViewPeriodChanged(this.scheduleNextButton.GotoDate, this.CalendarGridConfig.ViewRange);
            //Update End 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理

            this.scheduleCalendarGrid.TitleHeader.Children.Add(this.schedulePrevButton);
            this.scheduleCalendarGrid.TitleHeader.Children.Add(this.scheduleNextButton);

            //スケジュールの作業履歴表示の案内
            this.scheduleCalendarGrid.TitleHeader.Children.Add(new CalendarTitleLabel
            {
                Name = "ScheduleDoubleClickLabel",
                TextImageRelation = TextImageRelation.ImageAboveText,
                AutoSize = true,
                ForeColor = Color.Red,
                Text = this.CalendarGridConfig.ScheduleDoubleClickLabelText

            });
        }

        /// <summary>
        /// カレンダーのコンテキストメニューの初期化
        /// </summary>
        private void InitContextMenu()
        {
            var config = this.CalendarGridConfig;

            #region コーナーヘッダーの右クリックのコンテキストメニュー

            this.cornerHeaderContexMenu = new ContextMenuStrip();

            //設定するコンテキストメニューがあるかどうか
            if (config.CornerHeaderContextMenuMap != null && config.CornerHeaderContextMenuMap.Any() == true)
            {
                foreach (var kv in config.CornerHeaderContextMenuMap)
                {
                    //追加するメニューの設定
                    var menu = new ToolStripMenuItem { Text = kv.Key };
                    menu.Click += (sender, e) =>
                    {
                        //アクションを実行
                        var action = kv.Value;
                        if (action != null)
                        {
                            action();
                        }
                    };

                    //メニューを追加
                    this.cornerHeaderContexMenu.Items.Add(menu);
                }
            }

            #endregion

            #region 行ヘッダーの右クリックのコンテキストメニュー

            this.rowHeaderContexMenu = new ContextMenuStrip();

            //設定するコンテキストメニューがあるかどうか
            if (config.ScheduleRowHeaderContextMenuMap != null && config.ScheduleRowHeaderContextMenuMap.Any() == true)
            {
                foreach (var kv in config.ScheduleRowHeaderContextMenuMap)
                {
                    //追加するメニューの設定
                    var menu = new ToolStripMenuItem { Text = kv.Key };
                    menu.Click += (sender, e) =>
                    {
                        //タグからスケジュール項目取得
                        var item = this.rowHeaderContexMenu.Tag as ScheduleItemModel<Item>;

                        //タグを初期化
                        this.rowHeaderContexMenu.Tag = null;

                        //アクションを実行
                        var action = kv.Value;
                        if (action != null)
                        {
                            action(item);
                        }
                    };

                    //メニューを追加
                    this.rowHeaderContexMenu.Items.Add(menu);
                }
            }
            #endregion

            #region スケジュールのの右クリックのコンテキストメニュー

            this.scheduleContexMenu = new ContextMenuStrip();

            //設定するコンテキストメニューがあるかどうか
            if (config.ScheduleContextMenuMap != null && config.ScheduleContextMenuMap.Any() == true)
            {
                foreach (var kv in config.ScheduleContextMenuMap)
                {
                    //追加するメニューの設定
                    var menu = new ToolStripMenuItem { Text = kv.Key };
                    menu.Click += (sender, e) =>
                    {
                        var schedule = this.scheduleContexMenu.Tag as ScheduleModel<Schedule>;
                        if (schedule != null)
                        {
                            //タグを初期化
                            this.scheduleContexMenu.Tag = null;

                            //アクションを実行
                            var action = kv.Value;
                            if (action != null)
                            {
                                action(schedule);

                            }


                        }

                    };

                    //メニューを追加
                    this.scheduleContexMenu.Items.Add(menu);

                }

            }

            #endregion

        }

        /// <summary>
        /// デリゲートの初期化
        /// </summary>
        private void InitDelegate()
        {
            //スケジュール表示期間の変更後のデリゲート
            this.ScheduleViewPeriodChangedAfter = (start, end) => { };

            //スケジュール行ヘッダーダブルクリックのデリゲート
            this.ScheduleRowHeaderDoubleClick = item => { };

            //スケジュールシングルクリックのデリゲート
            this.ScheduleSingleClick = (schedule, mouseButtons) => { };

            //スケジュールダブルクリックのデリゲート
            this.ScheduleDoubleClick = schedule => { };

            //スケジュールの日付範囲の変更後のデリゲート
            this.ScheduleDayRangeChangedAfter = schedule => { };

            //スケジュールの空白領域をドラッグ後のデリゲート
            this.ScheduleEmptyDragAfter = schedule => { };

            //スケジュール空白領域をダブルクリックのデリゲート
            this.ScheduleEmptyDoubleClick = schedule => { };

            //スケジュール削除のデリゲート
            this.ScheduleDelete = schedule => { };

            //スケジュール貼り付けのデリゲート
            this.SchedulePaste = (copySchedule, schedule) => { };

            //スケジュール項目の並び順変更後のデリゲート
            this.ScheduleItemSortChangedAfter = (sourceItem, destItem) => { };

            //スケジュールテンプレート処理のデリゲート
            this.CalendarCellPaint = (editCell, scheduleModel, colIndex, rowIndex) => { };

            //スケジュール各項目描画後のデリゲート
            this.CalendarScheduleCellPaint = (item) => { };

        }
        #endregion

        #region イベント
        /// <summary>
        /// スケジュールマウス押下
        /// </summary>
        /// <remarks>
        /// カレンダーグリッド内でマウスを押下した際に処理が行われます。
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_MouseDown(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            var isLeft = e.Button == MouseButtons.Left;
            //カレンダーがデータで初期化されていない場合も何もできないため終了。
            if (isLeft == false || (this.BindCalendarItemList == null))
            {
                return;
            }

            var isDoubleClick = e.Clicks == 2;

            var grid = sender as GcCalendarGrid;

            callScheduleEmptyDragAfter = false;

            //座標からセル位置を取得
            var hitTestInfo = grid.HitTest(new Point(e.X, e.Y));
            var cp = hitTestInfo.CellPosition;

            ScheduleItemModel<Item> item = null;

            //セルのタイプごとの分岐
            switch (hitTestInfo.HitTestType)
            {
                //コーナーヘッダー、グレイエリア
                case CalendarHitTestType.CornerHeader:
                case CalendarHitTestType.GrayArea:

                    //保持座標をクリアする。
                    this.dragStartPosition = Point.Empty;
                    break;

                //行ヘッダー
                case CalendarHitTestType.RowHeader:
                    item = grid.Template.RowHeader[cp.RowIndex, 0].Tag as ScheduleItemModel<Item>;

                    if (isLeft == true)
                    {
                        #region 左クリック

                        //シングルクリックかどうか
                        if (isDoubleClick == false)
                        {
                            //行ヘッダー編集権限がなければ終了
                            if (this.CalendarGridConfig.IsRowHeaderEdit == false)
                            {
                                return;
                            }

                            //ドラッグアンドドロップ開始位置を取得
                            this.dragStartPosition = e.Location;
                        }
                        else
                        {
                            //行ヘッダーをダブルクリックしているかどうか
                            if (item != null && this.ScheduleRowHeaderDoubleClick != null)
                            {
                                this.ScheduleRowHeaderDoubleClick(item);
                            }
                        }

                        #endregion
                    }
                    break;

                //コンテンツ
                case CalendarHitTestType.Content:
                    item = grid.Template.RowHeader[cp.RowIndex, 0].Tag as ScheduleItemModel<Item>;

                    if (IsReadOnlyRowIndex(item, cp.RowIndex)) { return; }

                    var schedule = grid.Content[cp.Date].Rows[cp.RowIndex].Cells[cp.ColumnIndex].Tag as ScheduleModel<Schedule>;

                    if (isLeft == true)
                    {
                        if (isDoubleClick == false)
                        {
                            #region シングルクリック

                            if (schedule != null)
                            {
                                this.ScheduleSingleClick?.Invoke(schedule, e.Button);
                            }

                            //スケジュール編集権限がなければ終了
                            if (this.CalendarGridConfig.IsScheduleEdit == false || schedule?.IsEdit == false)
                            {
                                if (schedule != null)
                                {
                                    var cell = grid.Content[cp.Date].Rows[cp.RowIndex].Cells[cp.ColumnIndex].CellType as CalendarAppointmentCellType;
                                    cell.ResizeHandlerVisibility = ResizeHandlerVisibility.NotShown;
                                }

                                return;
                            }
                            else
                            {
                                if (schedule != null)
                                {
                                    var cell = grid.Content[cp.Date].Rows[cp.RowIndex].Cells[cp.ColumnIndex].CellType as CalendarAppointmentCellType;
                                    cell.ResizeHandlerVisibility = ResizeHandlerVisibility.ShowOnFocus;
                                }
                            }

                            //スケジュールが設定されているセルかどうか
                            if (schedule != null)
                            {
                                //ドラッグアンドドロップ開始位置を取得
                                this.dragStartPosition = e.Location;
                            }
                            else
                            {
                                //スケジュールが設定されていない場合はクリアを行う
                                this.dragStartPosition = Point.Empty;

                                //スケジュール空白エリアクリックON
                                callScheduleEmptyDragAfter = true;
                            }
                            #endregion
                        }
                        else
                        {
                            if (schedule != null)
                            {
                                #region スケジュールあり

                                //スケジュールダブルクリックのデリゲート
                                if (this.ScheduleDoubleClick != null)
                                {
                                    this.ScheduleDoubleClick(schedule);
                                }

                                #endregion
                            }
                            else
                            {
                                #region スケジュール無し

                                //スケジュール編集権限がなければ終了
                                if (this.CalendarGridConfig.IsScheduleEdit == false)
                                {
                                    return;
                                }

                                //スケジュール空白領域をダブルクリックのデリゲート
                                if (this.ScheduleEmptyDoubleClick != null)
                                {
                                    schedule = new ScheduleModel<Schedule>
                                    {
                                        //カテゴリーID
                                        CategoryID = item.ID,

                                        //開発符号
                                        GeneralCode = item.GeneralCode,

                                        //行番号
                                        RowNo = this.GetScheduleRowNo(item, cp.RowIndex)
                                    };

                                    var date = ((ICalendarTemplate)this.scheduleCalendarGrid.Template).CreateCalendarDate(cp);
                                    schedule.StartDate = date.StartDate;
                                    schedule.EndDate = date.EndDate;

                                    this.ScheduleEmptyDoubleClick(schedule);

                                }

                                #endregion
                            }
                        }
                    }
                    break;

                default:
                    //それ以外の場合、座標保持していたらクリアする。
                    this.dragStartPosition = Point.Empty;
                    break;

            }

        }

        /// <summary>
        /// 行読み取り専用チェック。
        /// </summary>
        /// <param name="item"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private bool IsReadOnlyRowIndex(ScheduleItemModel<Item> item, int rowIndex)
        {
            if (this.CalendarGridConfig.ReadOnlyRowCount == 0) { return false; }
            return this.GetScheduleRowNo(item, rowIndex) >= this.CalendarGridConfig.ReadOnlyRowCount;
        }

        /// <summary>
        /// スケジュールマウス移動
        /// </summary>
        /// <remarks>
        /// カレンダーグリッド上でマウスを移動した際に処理が行われます。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_MouseMove(object sender, MouseEventArgs e)
        {
            var grid = sender as GcCalendarGrid;

            if (e.Button == MouseButtons.None && location != e.Location)
            {
                var cp = this.scheduleCalendarGrid.HitTest(e.Location).CellPosition;

                if (cp.IsEmpty == true || cp.Scope == CalendarTableScope.ColumnHeader || this.BindCalendarItemList == null || this.BindCalendarItemList.Count() <= 0)
                {
                    this.toolTip.Hide(grid);
                    showToolTip = false;
                }
                else
                {
                    var oldcp = this.scheduleCalendarGrid.HitTest(location).CellPosition;
                    var cellIndex = 0;
                    if (this.CalendarSetting.CalendarMode == CalendarTemplateTypeSafeEnum.DEFAULT)
                    {
                        cellIndex = 0;
                    }
                    else
                    {
                        cellIndex = cp.ColumnIndex;
                    }

                    var schedule = grid.Content[cp.Date].Rows[cp.RowIndex].Cells[cellIndex].Tag as ScheduleModel<Schedule>;
                    var item = grid.Template.RowHeader[cp.RowIndex, 0].Tag as ScheduleItemModel<Item>;

                    string text = "";

                    if (oldcp.RowIndex != cp.RowIndex || oldcp.ColumnIndex != cp.ColumnIndex || oldcp.Scope != cp.Scope || oldcp.Date != cp.Date)
                    {
                        this.toolTip.Hide(grid);
                        showToolTip = false;
                    }

                    if (showToolTip == false)
                    {
                        switch (cp.Scope)
                        {
                            //コーナーヘッダー
                            case CalendarTableScope.CornerHeader:
                                text = this.CornerHeaderText;
                                this.toolTip = this.orgToolTip;
                                break;

                            //行ヘッダー
                            case CalendarTableScope.RowHeader:
                                if (item.ToolTip != null)
                                {
                                    this.toolTip = item.ToolTip;
                                    text = "*";
                                }
                                else
                                {
                                    text = item.ToolTipText;
                                    this.toolTip = this.orgToolTip;
                                }
                                break;

                            //コンテンツ
                            case CalendarTableScope.Content:
                                if (schedule == null)
                                {
                                    if (!string.IsNullOrWhiteSpace(grid.Content[cp.Date].Rows[cp.RowIndex].Cells[cp.ColumnIndex].ToolTipText))
                                    {
                                        //スケジュールが無くてもツールチップテキストがある場合は設定を行う。
                                        text = grid.Content[cp.Date].Rows[cp.RowIndex].Cells[cp.ColumnIndex].ToolTipText;
                                        this.toolTip = this.orgToolTip;
                                    }
                                }
                                else if (schedule.ToolTip != null)
                                {
                                    this.toolTip = schedule.ToolTip;
                                    text = "*";
                                }
                                else
                                {
                                    text = schedule.ToolTipText;
                                    this.toolTip = this.orgToolTip;
                                }
                                break;
                        }

                        var point = grid.PointToClient(Control.MousePosition);
                        point.Y += 20;
                        point.X += 35;

                        this.toolTip.InitialDelay = 0;
                        this.toolTip.ReshowDelay = 0;
                        this.toolTip.AutoPopDelay = int.MaxValue;
                        this.toolTip.ShowAlways = true;
                        this.toolTip.UseAnimation = false;
                        this.toolTip.UseFading = false;

                        this.toolTip.Show(text, grid, point);

                        location = e.Location;
                        showToolTip = true;
                    }
                }
            }

            if (this.dragStartPosition != Point.Empty)
            {
                //座標からセル位置を取得
                var cp = grid.HitTest(this.dragStartPosition).CellPosition;

                #region 選択範囲チェック

                //空のセルを選択しているなら終了
                if (cp.Scope != CalendarTableScope.RowHeader && grid.Content[cp.Date].Rows[cp.RowIndex].Cells[cp.ColumnIndex].Tag == null)
                {
                    //ドラッグ開始位置を初期化
                    this.dragStartPosition = Point.Empty;
                    return;
                }

                //マウスカーソルがデフォルト以外は終了
                if (Cursor.Current != Cursors.Default)
                {
                    //ドラッグ開始位置を初期化
                    this.dragStartPosition = Point.Empty;
                    return;
                }

                #endregion

                var moveRect = new Rectangle(this.dragStartPosition.X - SystemInformation.DragSize.Width / 2, this.dragStartPosition.Y - SystemInformation.DragSize.Height / 2,
                        SystemInformation.DragSize.Width, SystemInformation.DragSize.Height);

                //ドラッグ非開始位置を超えているかどうか
                if (moveRect.Contains(e.Location) == false && e.Button == MouseButtons.Left)
                {
                    //ドラッグアンドドロップ開始
                    grid.DoDragDrop(cp, DragDropEffects.Move);

                    //ドラッグ開始位置を初期化
                    this.dragStartPosition = Point.Empty;
                }
            }
        }

        /// <summary>
        /// カレンダーコントロール親フォーム自動スクロール可能チェック
        /// </summary>
        /// <param name="type">自動スクロール可・不可チェック対象アクティブコントロールタイプ</param>
        /// <returns></returns>
        public bool CanScrollToControl(Type type)
        {
            if (type == typeof(WidthChangeButton) ||
               type == typeof(LayoutRadioButton) ||
               type == typeof(FontComboBox) ||
               type == typeof(Button) ||
               type == typeof(TrackBar))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// スケジュールドラッグアンドドラップ完了
        /// </summary>
        /// <remarks>
        /// カレンダーグリッド上でドラッグアンドドロップが完了した際に処理が行われます。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_DragDrop(object sender, DragEventArgs e)
        {
            //スケジュール編集権限がなければ終了
            if (this.CalendarGridConfig.IsScheduleEdit == false)
            {
                return;
            }

            var grid = sender as GcCalendarGrid;
            var clientPoint = grid.PointToClient(new Point(e.X, e.Y));
            var sourceCellPosition = (CalendarCellPosition)e.Data.GetData(typeof(CalendarCellPosition));
            var destCellPosition = grid.HitTest(new Point(clientPoint.X, clientPoint.Y)).CellPosition;

            if (sourceCellPosition.Scope == CalendarTableScope.Content && destCellPosition.Scope == CalendarTableScope.Content)
            {
                this.MoveSchedule(grid, sourceCellPosition, destCellPosition);
            }
            else if (sourceCellPosition.Scope == CalendarTableScope.RowHeader &&
                (destCellPosition.Scope == CalendarTableScope.Content || destCellPosition.Scope == CalendarTableScope.RowHeader))
            {
                this.MoveScheduleItem(grid, sourceCellPosition, destCellPosition);
            }
        }

        /// <summary>
        /// スケジュール移動
        /// </summary>
        /// <remarks>
        /// 指定された情報を元にスケジュールの移動を行います。
        /// </remarks>
        /// <param name="grid">カレンダーグリッド</param>
        /// <param name="source">移動元</param>
        /// <param name="dest">移動先</param>
        private void MoveSchedule(GcCalendarGrid grid, CalendarCellPosition source, CalendarCellPosition dest)
        {
            //移動先が空かコンテンツ以外か同じセル（同一行・列・日）の場合処理を行わない。
            if (dest.IsEmpty == true || source.Scope != CalendarTableScope.Content || dest.Scope != CalendarTableScope.Content ||
                (source.RowIndex == dest.RowIndex && source.ColumnIndex == dest.ColumnIndex && source.Date == dest.Date))
            {
                return;
            }

            var sourceCell = grid.Content[source.Date].Rows[source.RowIndex].Cells[source.ColumnIndex];
            var destCell = grid.Content[dest.Date].Rows[dest.RowIndex].Cells[dest.ColumnIndex];

            //別項目へスケジュールを移動の場合は警告を表示
            var sourceItem = grid.Template.RowHeader[source.RowIndex, 0].Tag as ScheduleItemModel<Item>;
            var destItem = grid.Template.RowHeader[dest.RowIndex, 0].Tag as ScheduleItemModel<Item>;
            if (sourceItem.ID != destItem.ID)
            {
                Messenger.Warn(Resources.KKM03004);
                return;
            }

            //スケジュールの変更
            var schedule = sourceCell.Tag as ScheduleModel<Schedule>;
            var copySchedule = schedule.Clone();

            schedule.GeneralCode = sourceItem.GeneralCode;

            var date = ((ICalendarTemplate)this.scheduleCalendarGrid.Template).CreateCalendarDateMove(dest, schedule.StartDate.Value, schedule.EndDate.Value);
            schedule.StartDate = date.StartDate;
            schedule.EndDate = date.EndDate;

            schedule.RowNo = this.GetScheduleRowNo(destItem, dest.RowIndex);
            schedule.ToolTipText = this.GetScheduleToolTipText(schedule);

            //移動先のセルに設定
            destCell.Tag = schedule;
            destCell.ColumnSpan = ((ICalendarTemplate)this.scheduleCalendarGrid.Template).CalculationColumnSpan(schedule.StartDate.Value, schedule.EndDate.Value);
            destCell.Value = sourceCell.Value;

            // 元のセルを初期化
            sourceCell.Tag = null;
            sourceCell.ColumnSpan = 1;
            sourceCell.Value = null;

            //スケジュールの日付範囲の変更後のデリゲート実行
            if (this.ScheduleDayRangeChangedAfter != null)
            {
                this.ScheduleDayRangeChangedAfter(schedule);
                UndoRedoManager.Do(copySchedule, schedule);
            }
        }

        /// <summary>
        /// スケジュール項目移動
        /// </summary>
        /// <remarks>
        /// 指定された情報を元にスケジュールの項目の移動を行います。
        /// </remarks>
        /// <param name="grid">カレンダーグリッド</param>
        /// <param name="source">移動元</param>
        /// <param name="dest">移動先</param>
        private void MoveScheduleItem(GcCalendarGrid grid, CalendarCellPosition source, CalendarCellPosition dest)
        {
            //移動先が空か移動元が行ヘッダー以外か移動元と移動先が同じなら終了
            if (dest.IsEmpty == true || source.Scope != CalendarTableScope.RowHeader || source == dest)
            {
                return;
            }

            var template = grid.Template;

            var sourceItem = template.RowHeader[source.RowIndex, 0].Tag as ScheduleItemModel<Item>;
            var destItem = template.RowHeader[dest.RowIndex, 0].Tag as ScheduleItemModel<Item>;

            var destIndex = dest.RowIndex;

            //移動先のタイプごとの分岐
            switch (dest.Scope)
            {
                //行ヘッダー
                case CalendarTableScope.RowHeader:
                    break;

                //コンテンツ
                case CalendarTableScope.Content:
                    //行ヘッダーの行インデックスを取得
                    destIndex = 0;
                    foreach (var row in template.RowHeader.Rows)
                    {
                        var header = row.Cells[0];
                        var item = header.Tag as ScheduleItemModel<Item>;

                        //IDが同じかどうか
                        if (destItem.ID == item.ID)
                        {
                            break;
                        }

                        destIndex++;
                    }
                    break;

                //対象外の移動先なら終了
                default:
                    return;
            }

            //移動先と移動元が同一項目なら終了
            if (source.RowIndex == destIndex)
            {
                return;
            }

            //変更前の並び順取得
            var list = this.BindCalendarItemList.ToList();
            var sourceSort = sourceItem.SortNo;

            var destScheduleItem = list.First(x => x.ScheduleItem.ID == destItem.ID).ScheduleItem;
            var sourceScheduleItem = list.First(x => x.ScheduleItem.ID == sourceItem.ID).ScheduleItem;

            //ソート順を設定。
            if (sourceScheduleItem.SortNo < destScheduleItem.SortNo)
            {
                sourceScheduleItem.SortNo = destScheduleItem.SortNo + 0.1D;
            }
            else if (sourceScheduleItem.SortNo > destScheduleItem.SortNo)
            {
                sourceScheduleItem.SortNo = (destScheduleItem.SortNo - 1) + 0.1D;
            }
            else
            {
                return;
            }

            //データバインド
            this.Bind(list.OrderBy(x => x.ScheduleItem.SortNo));

            //スケジュール項目の並び順変更後のデリゲート
            this.ScheduleItemSortChangedAfter?.Invoke(sourceScheduleItem, destScheduleItem);
        }

        /// <summary>
        /// スケジュールドラッグ開始
        /// </summary>
        /// <remarks>
        /// オブジェクトがコントロールの境界内にドラッグされたときに発生します。
        /// ドロップ効果を設定します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_DragOver(object sender, DragEventArgs e)
        {
            //スケジュール編集権限がなければ終了
            if (this.CalendarGridConfig.IsScheduleEdit == false)
            {
                return;
            }

            var flg = false;

            var grid = sender as GcCalendarGrid;

            var clientPoint = grid.PointToClient(new Point(e.X, e.Y));
            var cp = grid.HitTest(new Point(clientPoint.X, clientPoint.Y)).CellPosition;

            var sourceCellPosition = (CalendarCellPosition)e.Data.GetData(typeof(CalendarCellPosition));

            //セルのタイプごとの分岐
            switch (cp.Scope)
            {
                //行ヘッダー
                case CalendarTableScope.RowHeader:
                    flg = sourceCellPosition.Scope == CalendarTableScope.RowHeader;
                    break;

                //コンテンツ
                case CalendarTableScope.Content:
                    flg = (sourceCellPosition.Scope == CalendarTableScope.RowHeader || sourceCellPosition.Scope == CalendarTableScope.Content);
                    if (IsReadOnlyRowIndex(grid.Template.RowHeader[cp.RowIndex, 0].Tag as ScheduleItemModel<Item>, cp.RowIndex)) { flg = false; }
                    break;
            }

            //ドロップ不可かどうか
            e.Effect = flg == true ? DragDropEffects.Move : DragDropEffects.None;

        }

        /// <summary>
        /// スケジュール選択
        /// </summary>
        /// <remarks>
        /// 現在のセルが変更されたとき、またはコントロールが入力フォーカスを受け取ったときに発生します。
        /// コンテンツを選択している場合は選択したスケジュールを選択中として設定します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_CellEnter(object sender, CalendarCellMoveEventArgs e)
        {
            //スケジュール編集権限がなければ終了
            if (this.CalendarGridConfig.IsScheduleEdit == false)
            {
                return;
            }

            //コンテンツを選択しているかどうか
            var cp = e.CellPosition;
            if (cp.Scope == CalendarTableScope.Content)
            {
                //選択中以外の日付をすべてクリアする
                var grid = sender as GcCalendarGrid;
                var selectCellsList = grid.SelectedCells.Where(x =>
                                            (x.Date == e.CellPosition.Date &&
                                            x.ColumnIndex == e.CellPosition.ColumnIndex &&
                                            x.RowIndex == e.CellPosition.RowIndex) == false);
                foreach (var item in selectCellsList)
                {
                    grid.RemoveSelection(item.Date, item.RowIndex, item.ColumnIndex);
                }

                //タグが存在するかどうか
                var cell = grid.Content[cp.Date].Rows[cp.RowIndex].Cells[cp.ColumnIndex];
                if (cell.Tag != null)
                {
                    //選択したスケジュールを選択中のスケジュールとして設定
                    this.selectCellPosition = cp;
                }
                else
                {
                    this.selectCellPosition = CalendarCellPosition.Empty;
                }
            }
        }

        /// <summary>
        /// スケジュール変更
        /// </summary>
        /// <remarks>
        /// セル値が変更されたときに発生します。
        /// セルの設定を値なしに更新します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_CellValueChanged(object sender, CalendarCellEventArgs e)
        {
            //バインド中か編集権限がなければ終了
            if (this.IsBind == true || this.CalendarGridConfig.IsScheduleEdit == false)
            {
                return;
            }

            //コンテンツを選択しているかどうか
            var cp = e.CellPosition;
            if (cp.Scope == CalendarTableScope.Content)
            {
                Action<CalendarCell, CalendarCell> set = (setCell, clearCell) =>
                {
                    //セルの設定
                    setCell.Tag = clearCell.Tag;

                    //セルを初期化
                    clearCell.Tag = null;
                    clearCell.ColumnSpan = 1;
                    clearCell.Value = null;

                    //選択しているセルの箇所を変更
                    this.scheduleCalendarGrid.CurrentCellPosition = cp;
                };

                var grid = sender as GcCalendarGrid;
                var currentCell = grid.Content[cp.Date].Rows[cp.RowIndex].Cells[cp.ColumnIndex];
                var selectCell = grid.Content[this.selectCellPosition.Date].Rows[this.selectCellPosition.RowIndex].Cells[this.selectCellPosition.ColumnIndex];

                if (currentCell.Tag == null && selectCell.Tag != null)
                {
                    //現在のセルのタグが未設定で選択中のセルのタグが未設定かどうか
                    set(currentCell, selectCell);
                }
                else if (selectCell.Tag == null && currentCell.Tag != null)
                {
                    //選択中のセルのタグが未設定で現在のセルのタグが未設定かどうか
                    set(selectCell, currentCell);
                }
            }
        }

        /// <summary>
        /// スケジュールマウスアップ
        /// </summary>
        /// <remarks>
        /// ユーザーがセルの上でマウスボタンを離すと発生します。
        /// スケジュールが設定されている場合は開始日と終了日を取得し、スケジュールデータへ設定します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_CellMouseUp(object sender, CalendarCellMouseEventArgs e)
        {
            //スケジュール編集権限がなければ終了
            if (this.CalendarGridConfig.IsScheduleEdit == false)
            {
                return;
            }

            var grid = sender as GcCalendarGrid;
            var cp = grid.CurrentCellPosition;

            //左クリック以外かコンテンツ以外、アクティブセル無しなら終了
            if (e.Button != MouseButtons.Left || cp.Scope != CalendarTableScope.Content || cp.IsEmpty)
            {
                return;

            }

            //コンテンツを選択しているかどうか
            if (cp.Scope == CalendarTableScope.Content)
            {
                //タグが設定されているかどうか
                var currentCell = grid.Content[cp.Date].Rows[cp.RowIndex].Cells[cp.ColumnIndex];
                if (currentCell.Tag != null)
                {
                    var schedule = currentCell.Tag as ScheduleModel<Schedule>;
                    var copySchedule = schedule.Clone();

                    var template = (ICalendarTemplate)this.scheduleCalendarGrid.Template;

                    #region 開始日と終了日の取得

                    CalendarDate date = template.CreateCalendarDate(cp, currentCell.ColumnSpan, schedule.StartDate.Value, schedule.EndDate.Value);

                    if (date.StartDate >= date.EndDate)
                    {
                        Messenger.Warn(Resources.KKM02004);
                        BindFilter();
                        return;
                    }

                    if (date.StartDate != schedule.StartDate.Value || date.EndDate != schedule.EndDate.Value)
                    {
                        schedule.StartDate = date.StartDate;
                        schedule.EndDate = date.EndDate;
                        schedule.ToolTipText = this.GetScheduleToolTipText(schedule);

                        //スケジュールの日付範囲の変更後のデリゲート実行
                        if (this.ScheduleDayRangeChangedAfter != null)
                        {
                            this.ScheduleDayRangeChangedAfter(schedule);
                            this.UndoRedoManager.Do(copySchedule, schedule);
                        }
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// カレンダーマウスアップ
        /// </summary>
        /// <remarks>
        /// マウス ポインターがコントロール上にあり、マウス ボタンが離されると発生します。
        /// スケジュールが設定されていない場合は選択されているスケジュール範囲を確認し、
        /// 新規のスケジュールデータとしてデータを作成します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_MouseUp(object sender, MouseEventArgs e)
        {
            var grid = sender as GcCalendarGrid;
            var currentCp = grid.CurrentCellPosition;

            //スケジュール編集権限がなければ終了・左クリック以外かコンテンツ以外を選択時は無効
            if (this.CalendarGridConfig.IsScheduleEdit == false || e.Button != MouseButtons.Left || currentCp.IsEmpty)
            {
                this.callScheduleEmptyDragAfter = false;
                return;
            }

            //スケジュールを選択しているなら終了
            var currentSchedule = grid.Content[currentCp.Date].Rows[currentCp.RowIndex].Cells[currentCp.ColumnIndex].Tag as ScheduleModel<Schedule>;
            if (currentCp.IsEmpty == true || currentSchedule != null)
            {
                this.callScheduleEmptyDragAfter = false;
                return;
            }

            if (grid.SelectedCells.Count > 1 && this.callScheduleEmptyDragAfter)
            {
                this.callScheduleEmptyDragAfter = false;
                bool check = true;
                foreach (CalendarCellPosition pos in grid.SelectedCells)
                {
                    if (pos.RowIndex != currentCp.RowIndex) { check = false; }
                }

                if (check)
                {
                    if (this.ScheduleEmptyDragAfter != null)
                    {
                        #region スケジュールの空白領域をドラッグ後のデリゲート

                        var item = grid.Template.RowHeader[currentCp.RowIndex, 0].Tag as ScheduleItemModel<Item>;

                        if (IsReadOnlyRowIndex(item, currentCp.RowIndex)) { return; }

                        var schedule = new ScheduleModel<Schedule>
                        {
                            //カテゴリーID
                            CategoryID = item.ID,

                            //開発符号
                            GeneralCode = item.GeneralCode,

                            //行番号
                            RowNo = this.GetScheduleRowNo(item, currentCp.RowIndex)
                        };

                        var date = ((ICalendarTemplate)this.scheduleCalendarGrid.Template).CreateCalendarDate(this.scheduleCalendarGrid.SelectedCells);
                        schedule.StartDate = date.StartDate;
                        schedule.EndDate = date.EndDate;

                        this.ScheduleEmptyDragAfter(schedule);

                        #endregion
                    }
                }
            }
            else
            {
                this.callScheduleEmptyDragAfter = false;
                return;
            }
        }

        /// <summary>
        /// スケジュールキーダウン
        /// </summary>
        /// <remarks>
        /// コントロールにフォーカスがあるときにキーが押されると発生します。
        /// スケジュール削除またはスケジュール貼り付けを行います。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCalendarGrid_KeyDown(object sender, KeyEventArgs e)
        {
            var grid = sender as GcCalendarGrid;
            var currentCp = grid.CurrentCellPosition;

            //スケジュール編集権限が無い場合は終了
            if (this.CalendarGridConfig.IsScheduleEdit == false)
            {
                return;
            }

            var isUndo = (e.Control == true && e.KeyCode == Keys.Z);
            var isRedo = (e.Control == true && e.KeyCode == Keys.Y);

            if (isUndo || isRedo)
            {
                ScheduleModel<Schedule> model;
                if (isUndo)
                {
                    model = UndoRedoManager.PrevObj;
                }
                else
                {
                    model = UndoRedoManager.NextObj;
                }

                if (model != null)
                {
                    var ret = this.UndoRedo?.Invoke(model);

                    if (ret == true)
                    {
                        if (model.ScheduleEdit == ScheduleEditType.Insert || model.ScheduleEdit == ScheduleEditType.Paste)
                        {
                            model.ScheduleEdit = ScheduleEditType.Delete;
                        }
                        else if (model.ScheduleEdit == ScheduleEditType.Delete)
                        {
                            model.ScheduleEdit = ScheduleEditType.Insert;
                        }

                        if (isUndo)
                        {
                            UndoRedoManager.Undo();
                        }
                        else
                        {
                            UndoRedoManager.Redo();
                        }
                    }
                }
                return;
            }

            //コンテンツ以外の場合は終了
            if (currentCp.IsEmpty == true || currentCp.Scope != CalendarTableScope.Content)
            {
                return;
            }

            var isDelete = e.KeyCode == Keys.Delete;
            var isCopy = e.Control == true && (e.KeyCode == Keys.C || e.KeyCode == Keys.Insert);
            var isPaste = (e.Control == true && e.KeyCode == Keys.V) || (e.Shift == true && e.KeyCode == Keys.Insert);

            //スケジュールを選択しているかどうか
            var currentCell = grid.Content[currentCp.Date].Rows[currentCp.RowIndex].Cells[currentCp.ColumnIndex];
            var schedule = currentCell.Tag as ScheduleModel<Schedule>;
            if (schedule != null)
            {
                if (schedule.IsEdit == false) { return; }
                if (isDelete == true)
                {
                    #region スケジュール削除確認と削除

                    if (Messenger.Confirm(Resources.KKM00007) == DialogResult.Yes)
                    {
                        if (this.ScheduleDelete != null)
                        {
                            this.ScheduleDelete(schedule);
                        }
                    }

                    #endregion
                }
                else if (isCopy == true)
                {
                    this.copySchedule = schedule;
                }
            }

            if (isPaste == true && this.copySchedule != null)
            {
                #region スケジュール貼り付け

                var startDay = currentCp.Date.Date;

                //貼り付けするスケジュールの設定
                var currentItem = grid.Template.RowHeader[currentCp.RowIndex, 0].Tag as ScheduleItemModel<Item>;
                if (IsReadOnlyRowIndex(currentItem, currentCp.RowIndex)) { return; }

                var pasteSchedule = this.copySchedule.Clone();
                pasteSchedule.ID = 0;
                pasteSchedule.CategoryID = currentItem.ID;
                pasteSchedule.GeneralCode = currentItem.GeneralCode;
                pasteSchedule.RowNo = this.GetScheduleRowNo(currentItem, currentCp.RowIndex);

                var date = ((ICalendarTemplate)this.scheduleCalendarGrid.Template).CreateCalendarDatePaste(currentCp, this.copySchedule.StartDate.Value, this.copySchedule.EndDate.Value);

                pasteSchedule.EndDate = date.EndDate;
                pasteSchedule.StartDate = date.StartDate;

                pasteSchedule.ToolTipText = this.GetScheduleToolTipText(pasteSchedule);

                //貼り付けするセルの設定
                currentCell.Tag = pasteSchedule;
                currentCell.ColumnSpan = ((ICalendarTemplate)this.scheduleCalendarGrid.Template).CalculationColumnSpan(pasteSchedule.StartDate.Value, pasteSchedule.EndDate.Value);
                currentCell.Value = pasteSchedule.SubTitle;

                //スケジュール貼り付けのデリゲート
                if (this.SchedulePaste != null)
                {
                    this.SchedulePaste(this.copySchedule, pasteSchedule);
                }

                #endregion
            }
        }
        #endregion

        #region データバインド
        /// <summary>
        /// データバインド
        /// </summary>
        /// <typeparam name="Item">スケジュール項目の型</typeparam>
        /// <typeparam name="Schedule">スケジュールの型</typeparam>
        /// <typeparam name="Key">結合条件の型</typeparam>
        /// <param name="itemList">スケジュール項目リスト</param>
        /// <param name="scheduleList">スケジュールリスト</param>
        /// <param name="itemKey">スケジュール項目のスケジュール項目の結合条件</param>
        /// <param name="scheduleKey">スケジュールの結合条件</param>
        /// <param name="convItem">スケジュール項目の変換</param>
        /// <param name="convSchedule">スケジュールの変換</param>
        public void Bind<Key>(IEnumerable<Item> itemList, IEnumerable<Schedule> scheduleList,
            Func<Item, Key> itemKey, Func<Schedule, Key> scheduleKey,
            Func<Item, ScheduleItemModel<Item>> convItem, Func<Schedule, ScheduleModel<Schedule>> convSchedule)
        {
            var list = itemList.GroupJoin(scheduleList, itemKey, scheduleKey, (x, y) =>
            {
                var item = convItem(x);
                var schedule = y.Select(z =>
                {
                    var s = convSchedule(z);
                    s.GeneralCode = item.GeneralCode;

                    return s;
                });

                return new CalendarItemModel<Item, Schedule>
                {
                    ScheduleItem = item,
                    ScheduleList = schedule
                };

            });

            this.orgCalendarList = new List<CalendarItemModel<Item, Schedule>>(list);
            BindFilter();
        }

        /// <summary>
        /// データバインド
        /// </summary>
        /// <typeparam name="Item">スケジュール項目の型</typeparam>
        /// <typeparam name="Schedule">スケジュールの型</typeparam>
        /// <typeparam name="Key">結合条件の型</typeparam>
        /// <param name="itemList">スケジュール項目リスト</param>
        /// <param name="scheduleList">スケジュールリスト</param>
        /// <param name="itemKey">スケジュール項目のスケジュール項目の結合条件</param>
        /// <param name="scheduleKey">スケジュールの結合条件</param>
        /// <param name="convItem">スケジュール項目の変換</param>
        /// <param name="convSchedule">スケジュールの変換</param>
        public void Bind<Key>(IEnumerable<Item> itemList, IEnumerable<Schedule> scheduleList,
            Func<Item, Key> itemKey, Func<Schedule, Key> scheduleKey,
            Func<Item, ScheduleItemModel<Item>> convItem, Func<Schedule, ScheduleItemModel<Item>, ScheduleModel<Schedule>> convSchedule)
        {
            var list = itemList.GroupJoin(scheduleList, itemKey, scheduleKey, (x, y) =>
            {
                var item = convItem(x);
                var schedule = y.Select(z =>
                {
                    var s = convSchedule(z, item);
                    s.GeneralCode = item.GeneralCode;

                    return s;
                });

                return new CalendarItemModel<Item, Schedule>
                {
                    ScheduleItem = item,
                    ScheduleList = schedule
                };

            });

            this.orgCalendarList = new List<CalendarItemModel<Item, Schedule>>(list);
            BindFilter();
        }

        /// <summary>
        /// データバインド
        /// </summary>
        /// <typeparam name="Item">スケジュール項目の型</typeparam>
        /// <typeparam name="Schedule">スケジュールの型</typeparam>
        /// <param name="list">カレンダー項目リスト</param>
        public void Bind(IEnumerable<CalendarItemModel<Item, Schedule>> list)
        {
            if (list == null) { return; }

            //表示している左端の日付
            var horizontalScrollBarOffset = this.scheduleCalendarGrid.HorizontalScrollBarOffset;
            var verticalScrollOffset = this.scheduleCalendarGrid.VerticalScrollBarOffset;

            //列情報
            var columnInfoList = new List<LayoutInfo>();

            try
            {
                //バインド中フラグON
                this.IsBind = true;

                //レンダリング中断
                this.scheduleCalendarGrid.SuspendRender();

                //コピー対象のスケジュール初期化
                this.copySchedule = null;

                //ドラッグ開始位置を初期化
                this.dragStartPosition = Point.Empty;

                //カレンダー項目を実体化
                var calendarList = this.BindCalendarItemList = list.ToList();

                //カレンダーのテンプレートを取得
                var template = this.scheduleCalendarGrid.Template;

                //行ヘッダーの列情報を取得
                columnInfoList.AddRange(this.scheduleCalendarGrid.LayoutSettings.HorizontalLayoutInfo.Take(template.RowHeaderColumnCount).ToArray());

                //行があるなら削除
                if (template.RowCount > 0)
                {
                    template.RemoveRow(0, template.RowCount);
                }

                //データがなければ終了
                if (calendarList.Any() == false)
                {
                    return;
                }

                ScheduleRowNoDic = new Dictionary<long, List<int>>();

                var scheduleRowNo = 0;
                var currentRowIndex = 0;

                var rowNum = 0;

                //項目ごと
                foreach (var item in calendarList)
                {
                    if (ScheduleRowNoDic.ContainsKey(item.ScheduleItem.ID) == false)
                    {
                        ScheduleRowNoDic.Add(item.ScheduleItem.ID, new List<int>());

                        if (item.ScheduleList.Count() == 0)
                        {
                            ScheduleRowNoDic[item.ScheduleItem.ID].Add(1);
                        }
                        else
                        {
                            ScheduleRowNoDic[item.ScheduleItem.ID] = item.ScheduleList.Select(v => v.RowNo).Distinct().ToList();

                            if (item.ScheduleItem.IsInputNewRow)
                            {
                                #region 空行の追加（読み取り専用行がある場合はその上につくる）
                                int maxCount = 1;
                                if (this.CalendarGridConfig.ReadOnlyRowCount != 0)
                                {
                                    var d = item.ScheduleList.Where(v => v.RowNo < this.CalendarGridConfig.ReadOnlyRowCount).ToList();
                                    maxCount = d.Count() > 0 ? d.Select(v => v.RowNo).Max() + 1 : 1;
                                }
                                else
                                {
                                    maxCount = ScheduleRowNoDic[item.ScheduleItem.ID].Max() + 1;
                                }
                                #endregion

                                if (maxCount > Const.ScheduleItemRowMax)
                                {
                                    Messenger.Info(Resources.KKM00035);
                                }
                                else
                                {
                                    ScheduleRowNoDic[item.ScheduleItem.ID].Add(maxCount > 0 ? maxCount : 1);
                                }
                            }
                        }
                    }

                    var rowCount = ScheduleRowNoDic[item.ScheduleItem.ID].Count();
                    ScheduleRowNoDic[item.ScheduleItem.ID].Sort();

                    //各項目が持っている行をまとめて追加
                    template.AddRow(rowCount);
                    rowNum++;

                    //項目ごとの行番号
                    scheduleRowNo = 0;

                    int rowHeight = 0;
                    var font = new Font(ControlFont.DefaultFont.Font.Name, float.Parse(this.CalendarSetting.CurrentStyle.FontSize));
                    for (var i = currentRowIndex; i < template.RowCount; i++)
                    {
                        #region 追加した行の設定（項目ごとの行）

                        //行ヘッダ―
                        var rowHeaderNum = template.RowHeader[i, 0];
                        var rowHeader = template.RowHeader[i, 1];
                        rowHeaderNum.Name = string.Format("RowNum_{0}", i);
                        rowHeaderNum.Tag = item.ScheduleItem;

                        rowHeader.Name = string.Format("RowHeader_{0}", i);
                        rowHeader.Tag = item.ScheduleItem;

                        var rowHeaderCellType = rowHeader.CellType as CalendarHeaderCellType;
                        rowHeaderCellType.FlatStyle = FlatStyle.Flat;
                        rowHeaderCellType.UseVisualStyleBackColor = false;
                        rowHeaderCellType.Ellipsis = CalendarGridEllipsisMode.EllipsisEnd;

                        var rowHeaderNumCellType = rowHeaderNum.CellType as CalendarHeaderCellType;
                        rowHeaderNumCellType.FlatStyle = FlatStyle.Flat;
                        rowHeaderNumCellType.UseVisualStyleBackColor = false;
                        rowHeaderNumCellType.Ellipsis = CalendarGridEllipsisMode.EllipsisEnd;

                        //対象の最初の行かどうか
                        if (i == currentRowIndex)
                        {
                            #region 行ヘッダ―内容設定

                            var color = CalendarScheduleColorEnum.NomalColor;

                            //スケジュール項目の背景色を取得するデリゲート
                            if (this.CalendarGridConfig.GetScheduleItemBackColor != null)
                            {
                                color = this.CalendarGridConfig.GetScheduleItemBackColor(item.ScheduleItem);
                            }

                            rowHeaderCellType.FlatAppearance.MouseOverBackColor = color.MainColor;
                            rowHeaderCellType.FlatAppearance.MouseDownBackColor = color.MainColor;

                            //行ヘッダーとしての設定
                            rowHeader.RowSpan = rowCount;
                            rowHeaderNum.RowSpan = rowCount;

                            if (color.BorderStyle != BorderLineStyle.None)
                            {
                                var style = new CalendarBorderLine(Color.Black, color.BorderStyle);
                                rowHeader.CellStyle.BottomBorder = style;
                                rowHeader.CellStyle.LeftBorder = style;
                                rowHeader.CellStyle.RightBorder = style;
                                rowHeader.CellStyle.TopBorder = style;

                                rowHeaderNum.CellStyle.BottomBorder = style;
                                rowHeaderNum.CellStyle.LeftBorder = style;
                                rowHeaderNum.CellStyle.RightBorder = style;
                                rowHeaderNum.CellStyle.TopBorder = style;
                            }
                            else
                            {
                                rowHeader.CellStyle.BottomBorder = BottomBorder;
                                rowHeaderNum.CellStyle.BottomBorder = BottomBorder;
                            }

                            rowHeader.CellStyle.BackColor = color.MainColor;
                            rowHeader.CellStyle.ForeColor = color.FontColor;
                            rowHeaderNum.CellStyle.Font = font;
                            rowHeaderNum.CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;

                            rowHeight = GetRowHeaderImageHeight(rowHeader.CellStyle, rowCount, item.ScheduleItem.Title, template.RowHeader.Columns[1].Width);
                            rowHeaderNum.Value = rowNum;

                            //行ヘッダーに項目名以外の列があるかどうか
                            if ((template.RowHeaderColumnCount > 1) == true)
                            {
                                #region 2018年上期リリース対象外
                                // TODO : ■テストを行えない機能（2018年上期リリース対象外）につきコメントアウト。業務計画表、月次計画表リリースと共に修正。
                                ////for (var j = 1; j < template.RowHeaderColumnCount; j++)
                                ////{
                                ////    var rowHeaderOther = template.RowHeader[i, j];

                                ////    var cellTypeOther = rowHeaderOther.CellType as CalendarHeaderCellType;
                                ////    cellTypeOther.FlatStyle = FlatStyle.Standard;
                                ////    cellTypeOther.UseVisualStyleBackColor = false;
                                ////    cellTypeOther.Ellipsis = CalendarGridEllipsisMode.EllipsisEnd;

                                ////    rowHeaderOther.Name = string.Format("RowHeader_{0}_{1}", i, j);
                                ////    rowHeaderOther.RowSpan = rowCount;
                                ////    rowHeaderOther.CellStyle.BottomBorder = BottomBorder;
                                ////    rowHeaderOther.Tag = item.ScheduleItem;
                                ////    rowHeaderOther.CellStyle.BackColor = color;

                                ////    var rowImage2 = GetRowHeaderImage(template.RowHeader.Columns[j].Width,
                                ////        this.CalendarGridConfig.GetRowHeaderOtherValue != null ? this.CalendarGridConfig.GetRowHeaderOtherValue(item.ScheduleItem, j) : string.Empty, 9f);
                                ////    rowHeaderOther.CellStyle.Image = rowImage2;
                                ////    rowHeaderOther.CellStyle.ImageAlign = CalendarGridContentAlignment.TopLeft;

                                ////    // 以下は必要時に要調整。
                                ////    //if (ScheduleRowHeight * rowCount < rowImage2.Height + 10)
                                ////    //    rowHeight = ((rowImage2.Height + 10) / rowCount);                                    
                                ////}
                                #endregion
                            }

                            #endregion
                        }

                        //行の設定
                        var row = template.Content.Rows[i];
                        row.Tag = item.ScheduleItem;//Tagへ項目を設定する
                        row.Height = rowHeight;

                        //スケジュールセルの作成/////////////////
                        for (var colIndex = 0; colIndex < row.Cells.Count; colIndex++)
                        {
                            var cell = row.Cells[colIndex];
                            cell.Name = string.Format("AppointmentCell{0}_{1}_{2}", item.ScheduleItem.ID, i, colIndex);
                            cell.CellType = new CalendarAppointmentCellType
                            {
                                Multiline = true,
                                Renderer = new AngleBracketShapeRenderer
                                {
                                    ArrowLength = 3,
                                    FillColor = CalendarScheduleColorEnum.DefaultColor.MainColor
                                }
                            };

                            if (CalendarCellPaint != null)
                            {
                                this.CalendarCellPaint(cell, item, colIndex, GetScheduleRowNo(item.ScheduleItem, cell.RowIndex));
                            }
                        }

                        #region カラー設定
                        if (this.CalendarGridConfig.ReadOnlyRowCount != 0 &&
                            ScheduleRowNoDic[item.ScheduleItem.ID][scheduleRowNo] >= this.CalendarGridConfig.ReadOnlyRowCount)
                        {
                            row.CellStyle.BackColor = CalendarScheduleColorEnum.PastColor.MainColor;
                        }
                        else
                        {
                            row.CellStyleName = "dynamicCellStyle";
                        }
                        #endregion

                        //その他スケジュール情報取得
                        DateTime? targetDay;
                        OtherScheduleModel<Item> otherSchedule = this.CalendarGridConfig.OtherSchedule;
                        if (otherSchedule != null)
                        {
                            targetDay = otherSchedule.GetTargetDay == null ? null : otherSchedule.GetTargetDay(item.ScheduleItem);
                        }
                        else
                        {
                            targetDay = null;
                        }

                        //対象の最初の行かどうか
                        if (i == currentRowIndex)
                        {
                            // TODO : ここは真っ黒行専用。時間があれば、「GetCellBackColor」と共に整理する（おそらくGetCellBackColorのほうが不要。こちらへまとめてよいかと）
                            //対象日があるかどうか
                            if (targetDay != null)
                            {
                                //対象日が表示範囲の開始日より前の日付かどうか
                                var day = targetDay.Value.Date;
                                if (this.scheduleCalendarGrid.FirstDateInView.Date > day)
                                {
                                    day = this.scheduleCalendarGrid.FirstDateInView.Date;
                                }

                                //カレンダー表示範囲外となる場合は特に処理を行わない。
                                if (this.scheduleCalendarGrid.LastDateInView.Date >= day)
                                {
                                    var calendarCell = this.scheduleCalendarGrid[day][i, 0];

                                    calendarCell.CellType = new CalendarAppointmentCellType
                                    {
                                        Multiline = false,
                                        ResizeHandlerVisibility = ResizeHandlerVisibility.NotShown,
                                        Renderer = new CalendarRectangleShapeRenderer
                                        {
                                            FillColor = otherSchedule.BackColor
                                        }
                                    };

                                    //セルの設定
                                    calendarCell.CellStyle.ForeColor = otherSchedule.ForeColor;
                                    TimeSpan span = this.scheduleCalendarGrid.LastDateInView.Date - day;
                                    calendarCell.ColumnSpan = this.scheduleCalendarGrid.Template.ColumnCount * (span.Days + 1);
                                    calendarCell.Value = otherSchedule.Value;
                                }
                            }
                        }

                        if (item.ScheduleList != null && item.ScheduleList.Any() == true)
                        {
                            var scheduleList = item.ScheduleList.Where(x => x.StartDate != null && x.RowNo == ScheduleRowNoDic[item.ScheduleItem.ID][scheduleRowNo]).ToArray();

                            foreach (var schedule in scheduleList)
                            {
                                schedule.StartDate = CheckErrDate(schedule.StartDate, true);
                                schedule.EndDate = CheckErrDate(schedule.EndDate, false);

                                if (targetDay == null || (schedule.StartDate.Value.Date < targetDay.Value.Date))//最終予約可能日までスケジュールを表示する。それ以外は表示しない（黒帯となる）
                                {
                                    #region スケジュールの設定

                                    var startDate = schedule.StartDate.Value;

                                    //ツールチップ
                                    schedule.ToolTipText = this.GetScheduleToolTipText(schedule);

                                    //スケジュールのタイトル
                                    var subTitle = new StringBuilder();

                                    //シンボルの記号に該当があれば設定
                                    if (SymbolMapTypeSafeEnum.KeyOf(schedule.Status) != null)
                                    {
                                        subTitle.Append(SymbolMapTypeSafeEnum.KeyOf(schedule.Status).Mark);

                                    }
                                    subTitle.Append(schedule.SubTitle);

                                    CalendarCell calendarCell;
                                    calendarCell = ((ICalendarTemplate)this.scheduleCalendarGrid.Template).GetCalendarCell(startDate, this.scheduleCalendarGrid[startDate].Rows[i]);

                                    var colorType = GetScheduleCellFillColor(schedule);
                                    var renderer = new AngleBracketShapeRenderer()
                                    {
                                        ArrowLength = 3,
                                        FillColor = (schedule.IsEdit == false) ? colorType.MainReadOnlyColor : colorType.MainColor
                                    };

                                    if (schedule.ReservationStauts == ReservationStautsEnum.HON_YOYAKU)
                                    {
                                        renderer.LineColor = Color.Red;
                                        renderer.LineWidth = 2f;
                                    };

                                    calendarCell.CellStyle.ForeColor = (schedule.IsEdit == false) ? colorType.FontReadOnlyColor : colorType.FontColor;

                                    calendarCell.CellType = new CalendarAppointmentCellType()
                                    {
                                        Multiline = true,
                                        Renderer = renderer,
                                        ResizeHandlerVisibility =
                                        (this.CalendarGridConfig.IsScheduleEdit == true && schedule.IsResizeHandler == true) ? ResizeHandlerVisibility.ShowOnFocus : ResizeHandlerVisibility.NotShown
                                    };

                                    calendarCell.ColumnSpan = ((ICalendarTemplate)this.scheduleCalendarGrid.Template).CalculationColumnSpan(startDate, schedule.EndDate.Value);

                                    calendarCell.Tag = schedule;
                                    calendarCell.Value = subTitle.ToString();

                                    #endregion
                                }
                            }
                        }

                        #endregion

                        scheduleRowNo++;
                    }

                    if (CalendarScheduleCellPaint != null)
                    {
                        this.CalendarScheduleCellPaint(item.ScheduleItem);
                    }

                    //現在の行位置を設定
                    currentRowIndex += rowCount;

                    //最終行の設定
                    var lastRow = template.Content.Rows[currentRowIndex - 1];
                    lastRow.CellStyle.BottomBorder = BottomBorder;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                //行ヘッダーの列幅を設定
                for (var i = 0; i < columnInfoList.Count(); i++)
                {
                    this.scheduleCalendarGrid.LayoutSettings.SetHorizontalLength(i, columnInfoList[i].Length);
                }

                //レンダリング再開
                this.scheduleCalendarGrid.ResumeRender();

                //レンダリング
                this.scheduleCalendarGrid.PerformRender();

                this.scheduleCalendarGrid.HorizontalScrollBarOffset = horizontalScrollBarOffset;
                this.scheduleCalendarGrid.VerticalScrollBarOffset = verticalScrollOffset;

                //バインド中フラグOFF
                this.IsBind = false;
            }
        }

        /// <summary>
        /// 時間例外カスタマイズ。
        /// </summary>
        /// <remarks>
        /// 時間例外をカスタマイズします。当システムへ刷新する前のシステムのキーボード入力ミスによる措置です。
        /// ・２２時０１分以上０時未満の場合は２２時
        /// ・０時以上６時未満の場合は６時
        /// ・０時の場合は強制的に６時（開始時間の場合）または２２時（終了時間の場合）にする（現行試験車日程同様）
        /// ※ただし業務計画表、月次計画表修正時はまたカスタマイズがいる。
        /// </remarks>
        /// <param name="date">チェックを行う日付</param>
        /// <param name="isStartDateTime">開始時間を設定する場合True</param>
        /// <returns></returns>
        private DateTime CheckErrDate(DateTime? date, bool isStartDateTime)
        {
            var checkDate = date.Value;

            //0時
            if (checkDate.Hour == 0)
            {
                return (isStartDateTime == true) ? checkDate.Date.AddHours(6) : checkDate.Date.AddHours(22);
            }

            //範囲外の時間
            DateTime returnDate;
            if (checkDate.TimeOfDay >= new TimeSpan(22, 00, 01) && checkDate.TimeOfDay <= new TimeSpan(23, 59, 59))
            {
                returnDate = checkDate.Date.AddHours(22);
            }
            else if (checkDate.TimeOfDay >= new TimeSpan(00, 00, 00) && checkDate.TimeOfDay <= new TimeSpan(05, 59, 59))
            {
                returnDate = checkDate.Date.AddHours(6);
            }
            else
            {
                returnDate = checkDate;
            }

            //開始時間が22時あるいは終了時間が6時データの対応
            if (isStartDateTime == true && returnDate.Hour == 22)
            {
                return returnDate.Date.AddHours(21);
            }
            else if (isStartDateTime == false && returnDate.Hour == 6)
            {
                return returnDate.Date.AddHours(7);
            }
            else
            {
                return returnDate;
            }
        }
        #endregion

        #region 拡大関係のイベント
        /// <summary>
        /// 拡大縮小率表示ボタンクリックイベント。
        /// </summary>
        /// <remarks>
        /// カレンダーの表示倍率を100%に設定します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoZoomButton_Click(object sender, System.EventArgs e)
        {
            this.scheduleCalendarGrid.ZoomFactor = 1;
        }

        /// <summary>
        /// スケジュールズームトラックバー移動時イベント。
        /// </summary>
        /// <remarks>
        /// 現在のトラックバーのValueを取得し、拡大縮小率をズームリセットボタンへ表示します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleZoomTrackBar_Scroll(object sender, System.EventArgs e)
        {
            this.scheduleCalendarGrid.ZoomFactor = this.zoomTrackBar.Value / 100f;
        }
        #endregion

        #region カレンダーグリッドに休日を設定
        /// <summary>
        /// カレンダーグリッドに休日を設定
        /// </summary>
        /// <returns></returns>
        private void SetHoliday()
        {
            //休日クリア
            this.scheduleCalendarGrid.Holidays.Clear();

            //休日をDBから読み込み
            var cond = new CalendarKadouGetInModel();
            cond.FIRST_DATE = this.scheduleCalendarGrid.FirstDateInView;
            cond.LAST_DATE = this.scheduleCalendarGrid.LastDateInView;
            var res = HttpUtil.GetResponse<CalendarKadouGetInModel, CalendarKadouGetOutModel>(ControllerType.CalendarKadou, cond);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                var list = res.Results.ToList();

                var holidays = list.Where(x => x.KADOBI_KBN == "0").ToList();
                foreach (var data in holidays)
                {
                    this.scheduleCalendarGrid.Holidays.Add(DateTime.ParseExact(data.CALENDAR_DATE, "yyyyMMdd", null), data.CALENDAR_DATE);
                }

                this.workdays = list.Where(x => x.KADOBI_KBN == "1").Select(x => DateTime.ParseExact(x.CALENDAR_DATE, "yyyyMMdd", null)).ToArray();
            }

            //追加された休日の日付を取得
            this.holidays = this.scheduleCalendarGrid.Holidays.Select(x => x.Date.Date).ToArray();
        }
        #endregion

        #region 動的セルスタイルの取得
        /// <summary>
        /// 動的セルスタイルの取得
        /// </summary>
        /// <param name="context">コンテキスト</param>
        /// <returns></returns>
        private CalendarCellStyle GetDynamicCellStyle(CellStyleContext context)
        {
            var style = new CalendarCellStyle();

            var toDay = DateTime.Today;

            var cp = context.CellPosition;
            var cellDate = cp.Date.Date;

            CalendarScheduleColorEnum backColor;

            if (toDay == cellDate)
            {
                backColor = CalendarScheduleColorEnum.TodayColor;
            }
            else if (this.workdays != null && this.workdays.Contains(cellDate))
            {
                backColor = CalendarScheduleColorEnum.WeekdayColor;
            }
            else if ((this.holidays != null && this.holidays.Contains(cellDate)) || cellDate.DayOfWeek == DayOfWeek.Saturday || cellDate.DayOfWeek == DayOfWeek.Sunday)
            {
                backColor = CalendarScheduleColorEnum.HolidayColor;
            }
            else
            {
                backColor = CalendarScheduleColorEnum.WeekdayColor;
            }

            //過去日かどうか
            if (toDay > cellDate)
            {
                backColor = CalendarScheduleColorEnum.PastColor;
            }

            var schedule = this.scheduleCalendarGrid.Content[cellDate].Rows[cp.RowIndex].Cells[cp.ColumnIndex].Tag as ScheduleModel<Schedule>;
            if (schedule == null)
            {
                //セルの背景色を取得するデリゲート
                if (this.CalendarGridConfig.GetCellBackColor != null)
                {
                    var item = this.scheduleCalendarGrid.Template.RowHeader[cp.RowIndex, 0].Tag as ScheduleItemModel<Item>;
                    backColor = this.CalendarGridConfig.GetCellBackColor(item, cellDate, backColor);
                }
            }

            style.BackColor = backColor.MainColor;
            style.ForeColor = backColor.FontColor;

            return style;
        }
        #endregion

        #region カレンダーの表示期間の設定
        /// <summary>
        /// カレンダーの表示期間の設定
        /// </summary>
        /// <param name="day">基準日</param>
        /// <param name="range">カレンダーの範囲</param>
        public void SetCalendarViewPeriod(DateTime day)
        {
            //テンプレートによりrangeの値を変える。
            int range = this.CalendarSetting.CurrentStyle.Range;

            var startDay = new DateTime(day.Year, day.Month, 1);

            var max = new DateTime(this.scheduleCalendarGrid.MaxDate.Year, this.scheduleCalendarGrid.MaxDate.Month, 1).AddMonths(-1 * (range - 1));
            var min = new DateTime(this.scheduleCalendarGrid.MinDate.Year, this.scheduleCalendarGrid.MinDate.Month, 1);

            if (startDay >= max) { startDay = max; }
            if (startDay <= min) { startDay = min; }

            var dayCount = Enumerable.Range(0, range).Select(x => startDay.AddMonths(x + 1)).ToList().Sum(x => x.AddDays(-1).Day);

            //カレンダー表示の設定
            var cv = this.scheduleCalendarGrid.CalendarView as CalendarListView;
            cv.DayCount = dayCount;

            //期間変更の表示
            this.schedulePrevButton.GotoDate = startDay.AddMonths(-1);
            this.scheduleNextButton.GotoDate = startDay.AddMonths(1);
            this.schedulePeriodLabel.Text = (range > 1)
                ? string.Format("{0} - {1}", DateTimeUtil.ConvertMonthString(startDay), DateTimeUtil.ConvertMonthString(startDay.AddMonths(range - 1)))
                : string.Format("{0}", DateTimeUtil.ConvertMonthString(startDay));

            //スケジュールの開始日変更
            this.scheduleCalendarGrid.FirstDateInView = startDay;

            //休日データ取得及び再設定
            this.SetHoliday();
        }
        #endregion

        #region カレンダーの表示期間の変更
        /// <summary>
        /// カレンダーの表示期間の変更
        /// </summary>
        /// <param name="day">基準日</param>
        //Update Start 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理
        //public void ScheduleViewPeriodChanged(DateTime day)
        public void ScheduleViewPeriodChanged(DateTime day, int range)
        //Update End 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理
        {
            //許可されていない場合はメッセージを表示する。
            if (this.CalendarGridConfig.IsScheduleViewPeriodChange == false)
            {
                //当月を含む３ヶ月のみ予約が可能
                DateTime toDay = DateTime.Today;
                DateTime start = new DateTime(toDay.Year, toDay.Month, 1);
                //Update Start 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理
                //DateTime end = start.AddMonths(3).AddDays(-1);
                //DateTime changeEnd = this.CalendarSetting.CurrentStyle.Range == 1 ? day : day.AddMonths(3).AddDays(-1);
                DateTime end = start.AddMonths(range).AddDays(-1);
                DateTime changeEnd = this.CalendarSetting.CurrentStyle.Range == 1 ? day : day.AddMonths(range).AddDays(-1);
                //Update End 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理

                if (changeEnd > end || day < start)
                {
                    Messenger.Warn(CalendarGridConfig.IsScheduleViewPeriodChangeMessage);
                    return;
                }
            }

            //カレンダーの表示期間の設定
            this.SetCalendarViewPeriod(day);

            //スケジュール表示期間の変更後のデリゲートが設定されている場合は実行
            if (this.ScheduleViewPeriodChangedAfter != null)
            {
                this.ScheduleViewPeriodChangedAfter(this.scheduleCalendarGrid.FirstDateInView.Date, this.scheduleCalendarGrid.LastDateInView.Date);
            }
        }
        #endregion

        #region スケジュールのツールチップテキストの取得
        /// <summary>
        /// スケジュールのツールチップテキストの取得
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        /// <returns></returns>
        private string GetScheduleToolTipText(ScheduleModel<Schedule> schedule)
        {
            //var dateFormat = this.CalendarGridConfig.IsScheduleToolTipDateTimeFormat == true ? ScheduleDateTimeFormat : ScheduleDateFormat;
            var dateFormat = ScheduleDateTimeFormat;

            var sb = new StringBuilder();

            //シンボルの記号に該当があれば設定
            if (SymbolMapTypeSafeEnum.KeyOf(schedule.Status) != null)
            {
                sb.Append(SymbolMapTypeSafeEnum.KeyOf(schedule.Status).Mark);
            }

            //スケジュールのツールチップテキストのタイトルを取得するデリゲートがあるかどうか
            if (this.CalendarGridConfig.GetScheduleToolTipTitle != null)
            {
                sb.AppendLine(this.CalendarGridConfig.GetScheduleToolTipTitle(schedule));
            }
            else
            {
                sb.AppendLine(schedule.SubTitle);
            }

            //ツールチップの文言
            sb.AppendFormat(dateFormat, schedule.StartDate);
            sb.Append("～");
            sb.AppendFormat(dateFormat, schedule.EndDate).AppendLine();

            //スケジュールのツールチップテキストを取得するデリゲートがあるかどうか
            if (this.CalendarGridConfig.GetScheduleAddToolTipText != null)
            {
                sb.AppendLine(this.CalendarGridConfig.GetScheduleAddToolTipText(schedule));
            }

            //登録日時
            sb.AppendFormat(ScheduleDateTimeFormat, schedule.InputDate);

            return sb.ToString();

        }
        #endregion

        #region 一番上に表示されているスケジュール項目を取得
        /// <summary>
        /// 一番上（または２番め）に表示されているスケジュール項目を取得
        /// </summary>
        /// <remarks>
        /// 一番上に表示されているセルから現在一番上に表示されている
        /// 項目を取得、表示行が各項目の１行目にあたる場合その該当項目オブジェクトを返却します。
        /// </remarks>
        /// <returns></returns>
        public ScheduleItemModel<Item> GetFirstDispItem()
        {
            // 表示行が存在しない場合はnull
            if (this.scheduleCalendarGrid.Template.RowHeader.RowCount <= 0)
            {
                return null;
            }

            var rowIndex = this.scheduleCalendarGrid.FirstDisplayedCellPosition.RowIndex;

            for (int i = rowIndex; i < this.scheduleCalendarGrid.Template.RowHeader.RowCount; i++)
            {
                var rows = this.scheduleCalendarGrid.Template.RowHeader.Rows[i];

                var item = rows.Cells[0].Tag as ScheduleItemModel<Item>;

                var rowNo = 1;
                var itemIndex = 0;

                //行ヘッダーの行インデックスを取得して行番号を計算
                foreach (var row in this.scheduleCalendarGrid.Template.RowHeader.Rows)
                {
                    var rowItem = row.Cells[0].Tag as ScheduleItemModel<Item>;

                    //IDが同じかどうか
                    if (item.ID == rowItem.ID)
                    {
                        rowNo = i - itemIndex + 1;
                        break;
                    }

                    itemIndex++;
                }
                if (rowNo == 1)
                {
                    return item;//1行目にあたる場合は返却。
                }
            }

            //該当しない場合は最初に取得したrowIndexを元にする（一番下の項目対応）
            return this.scheduleCalendarGrid.Template.RowHeader.Rows[rowIndex].Cells[0].Tag as ScheduleItemModel<Item>;
        }
        #endregion

        #region スケジュールの行番号を取得
        /// <summary>
        /// スケジュールの行番号を取得
        /// </summary>
        /// <param name="item">スケジュール項目</param>
        /// <param name="rowIndex">行インデックス</param>
        /// <returns></returns>
        private int GetScheduleRowNo(ScheduleItemModel<Item> item, int rowIndex)
        {
            var rowNo = 1;
            var itemIndex = 0;

            //行ヘッダーの行インデックスを取得して行番号を計算
            foreach (var row in this.scheduleCalendarGrid.Template.RowHeader.Rows)
            {
                var rowItem = row.Cells[0].Tag as ScheduleItemModel<Item>;

                //IDが同じかどうか
                if (item.ID == rowItem.ID)
                {
                    rowNo = rowIndex - itemIndex + 1;
                    break;
                }

                itemIndex++;
            }

            return this.ScheduleRowNoDic[item.ID][rowNo - 1];
        }
        #endregion

        #region スケジュールの先頭を設定(縦スクロール)
        /// <summary>
        /// スケジュールの先頭を指定したIDに設定
        /// </summary>
        /// <remarks>
        /// 指定された項目IDを元にTagを検索し、該当するIDがあった場合はそのRowIndexへ遷移します。
        /// （VerticalScrollBarOffsetプロパティは拡大縮小率により誤差が発生するため非採用）
        /// </remarks>
        /// <param name="id">ID</param>
        public void SetScheduleRowHeaderFirst(long? id)
        {
            if (id == null) { return; }

            var isScroll = false;

            int rowIndex = 0;

            //行ヘッダーの行インデックスを取得して行番号を計算
            foreach (var row in this.scheduleCalendarGrid.Template.RowHeader.Rows)
            {
                var item = row.Cells[0].Tag as ScheduleItemModel<Item>;

                if (item.ID == id)
                {
                    isScroll = true;
                    rowIndex = row.Cells[0].RowIndex;
                    break;
                }
            }

            if (isScroll)
            {
                //FirstDisplayedCellPositionはあくまでも現在カレンダーグリッドに表示されている情報が基準となる。
                //そのため、Positionを指定する際もカレンダーの情報で行う。
                this.scheduleCalendarGrid.FirstDisplayedCellPosition =
                    new CalendarCellPosition(this.scheduleCalendarGrid.FirstDateInView.Date, rowIndex, 0);
            }
            else
            {
                this.scheduleCalendarGrid.VerticalScrollBarOffset = 0;
            }
        }
        #endregion

        #region スケジュールの先頭を設定(横スクロール)
        /// <summary>
        /// スケジュールの先頭を直近の月曜に設定
        /// </summary>
        public void SetScheduleMostMondayFirst()
        {
            this.SetScheduleMostMondayFirst(DateTime.Today);
        }

        /// <summary>
        /// スケジュールの先頭を直近の月曜に設定
        /// </summary>
        /// <param name="day">基準日</param>
        public void SetScheduleMostMondayFirst(DateTime day)
        {
            this.SetScheduleMostWeekFirst(day, DayOfWeek.Monday);
        }

        /// <summary>
        /// スケジュールの先頭を直近の指定した曜日に設定
        /// </summary>
        /// <param name="day">基準日</param>
        /// <param name="week">曜日</param>
        public void SetScheduleMostWeekFirst(DateTime day, DayOfWeek week)
        {
            //スケジュールのスクロール位置を設定
            this.SetScheduleMostDayFirst(DateTimeUtil.GetMostWeekFirst(day, week));
        }

        /// <summary>
        /// スケジュールの先頭を指定した日付に設定
        /// </summary>
        /// <param name="setDate">設定日</param>
        public void SetScheduleMostDayFirst(DateTime setDate)
        {
            var setDay = setDate.Date;

            var grid = this.scheduleCalendarGrid;

            var startDay = grid.FirstDateInView.Date;
            var endDay = grid.LastDateInView.Date;

            var moveOffset = 0;

            //1日分のセル幅取得。カレンダーグリッドの仕様上、ここでは倍率を考えない。
            int dayWidth = 0;

            foreach (var col in this.scheduleCalendarGrid.Template.ColumnHeader.Columns)
            {
                dayWidth += col.Width;
            }

            //表示期間の範囲外の日付なら左端までスクロール
            if (setDay < startDay || endDay < setDay)
            {
                moveOffset = 0;
            }
            //設定日までスクロール
            else
            {
                moveOffset = (int)(setDay - startDay).TotalDays * dayWidth;
            }

            //スクロールの移動させるオフセットを設定
            grid.HorizontalScrollBarOffset = (int)Math.Ceiling(moveOffset * this.scheduleCalendarGrid.ZoomFactor);
        }
        #endregion

        #region スケジュールの1日分の列幅を取得
        /// <summary>
        /// スケジュールの1日分の列幅を取得
        /// </summary>
        /// <returns></returns>
        public int GetScheduleColumnDayWidth()
        {
            int dayWidth = 0;

            foreach (var col in this.scheduleCalendarGrid.Template.ColumnHeader.Columns)
            {
                dayWidth += col.Width;
            }

            return (int)Math.Round((dayWidth * this.scheduleCalendarGrid.ZoomFactor), 0, MidpointRounding.AwayFromZero);
        }
        #endregion

        /// <summary>
        /// 行ヘッダ項目名画像生成処理。
        /// </summary>
        /// <remarks>
        /// 行ヘッダへ表示する項目名の画像を作成します。
        /// （カレンダーグリッドが行ヘッダの改行に対応していないため）
        /// 渡されたテキストを元にして高さを算出後、イメージを作成します。
        /// </remarks>
        /// <param name="width">生成したい行ヘッダの幅</param>
        /// <param name="text">画像化するテキスト</param>
        /// <returns>画像化後行ヘッダ項目名</returns>
        private int GetRowHeaderImageHeight(CalendarCellStyle cellStyle, int rowCount, string text, int width)
        {
            if (string.IsNullOrEmpty(text)) { text = " "; }

            int height = 0;

            var font = new Font(ControlFont.DefaultFont.Font.Name, float.Parse(this.CalendarSetting.CurrentStyle.FontSize));

            using (var checkCanvas = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(checkCanvas))
            {
                height = (int)g.MeasureString(
                        text, font, width, StringFormat.GenericDefault).Height;
            }

            if (height == 0) { throw new Exception("項目画像生成が出来ませんでした。該当テキスト…" + text); }

            SolidBrush b = new SolidBrush(cellStyle.ForeColor);
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.DrawString(
                        text, font, b, new Rectangle(0, 0, width, height));
            }

            cellStyle.Image = bmp;
            cellStyle.ImageAlign = CalendarGridContentAlignment.TopLeft;

            //折返しの文字列がある場合まだやや文字が欠けるため、使い分け。
            var rowHeight = (rowCount > 1) ? ScheduleRowHeight : ScheduleNoneRowHeight;

            if (rowHeight * rowCount < bmp.Height)
                rowHeight = bmp.Height / rowCount;

            var ret = rowHeight + this.CalendarSetting.CurrentStyle.VerticalLengthUpdate;

            if (ret <= 0) { throw new Exception("カレンダーを表示できません。行の高さが0以下になる不正な値が設定されています。"); }

            return ret;
        }

        /// <summary>
        /// スケジュール色設定。
        /// </summary>
        /// <remarks>
        /// 指定されたスケジュールを元に、設定するべきスケジュールの色を決定します。
        /// </remarks>
        /// <param name="schedule">色決定を行うスケジュール</param>
        /// <returns>色</returns>
        private CalendarScheduleColorEnum GetScheduleCellFillColor(ScheduleModel<Schedule> schedule)
        {
            var colorType = CalendarScheduleColorEnum.DefaultColor;

            //クローズしたスケジュールかどうか
            if (schedule.IsClose == true)
            {
                colorType = CalendarScheduleColorEnum.CloseColor;
            }
            else
            {
                if (SymbolMapTypeSafeEnum.KeyOf(schedule.Status) != null)
                {
                    colorType = SymbolMapTypeSafeEnum.KeyOf(schedule.Status).ColorType;
                }
            }

            //スケジュールの背景色取得のデリゲート
            if (this.CalendarGridConfig.GetScheduleBackColor != null)
            {
                colorType = this.CalendarGridConfig.GetScheduleBackColor(schedule, colorType);
            }

            return colorType;

        }

        /// <summary>
        /// カレンダーグリッドテンプレートデザイン変更処理。
        /// </summary>
        /// <remarks>
        /// カレンダーのテンプレートを指定された設定情報で初期化します。
        /// </remarks>
        /// <param name="settings">カレンダー設定情報</param>
        public bool ChangeTemplateSettings(CalendarSettings settings)
        {
            bool isBind = false;
            if (settings.CalendarMode != this.CalendarSetting.CalendarMode)
            {
                this.scheduleCalendarGrid.ClearAll();
                this.scheduleCalendarGrid.ColumnHeader.ClearAll();
                this.scheduleCalendarGrid.PerformRender();
                this.scheduleCalendarGrid.Template = new CalendarTemplateFactory().CreateTemplate(settings);

                foreach (Control ctl in CalendarTypeRadioButtonPanel.Controls)
                {
                    if (ctl.GetType() == typeof(LayoutRadioButton))
                    {
                        var radio = (LayoutRadioButton)ctl;
                        radio.Checked = (radio.Type == settings.CalendarMode);
                    }
                }

                isBind = true;
            }
            else
            {
                this.scheduleCalendarGrid.ZoomFactor = settings.CurrentStyle.Zoom;
                SetZoom();
                fontComboBox.SelectedIndex = fontComboBox.Items.IndexOf(settings.CurrentStyle.FontSize);
                SetScheduleColContentsWidth(settings.CurrentStyle.VerticalLengthUpdate - this.CalendarSetting.CurrentStyle.VerticalLengthUpdate);
                SetScheduleRowContentsHeight(settings.CurrentStyle.HorizontalLengthUpdate - this.CalendarSetting.CurrentStyle.HorizontalLengthUpdate);
            }

            this.CalendarSetting = settings;

            return isBind;
        }
    }

    /// <summary>
    /// 幅変更ボタンクラス。
    /// </summary>
    public class WidthChangeButton : Button
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// 初期値をボタンに設定して初期化します。
        /// </remarks>
        public WidthChangeButton() : base()
        {
            Width = 23;
            Height = 23;
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TextAlign = ContentAlignment.MiddleCenter;
        }
    }

    /// <summary>
    /// 幅変更ラベルクラス。
    /// </summary>
    public class WidthChangeLabel : Label
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// 初期値をラベルに設定して初期化します。
        /// </remarks>
        public WidthChangeLabel() : base()
        {
            Width = 35;
            Height = 20;
            Font = ControlFont.GetDefaultFont(9.5f);
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }
    }

    /// <summary>
    /// レイアウトラジオボタンクラス。
    /// </summary>
    public class LayoutRadioButton : RadioButton
    {
        public CalendarTemplateTypeSafeEnum Type { get; private set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// 初期値をラジオボタンに設定して初期化します。
        /// </remarks>
        public LayoutRadioButton(CalendarTemplateTypeSafeEnum type) : base()
        {
            Type = type;
            Height = 20;
            Font = ControlFont.GetDefaultFont(9f);
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }
    }

    /// <summary>
    /// フォントコンボボックスクラス。
    /// </summary>
    public class FontComboBox : ComboBox
    {
        /// <summary>
        /// フォントサイズ定義内部フィールド
        /// </summary>
        private readonly List<string> FontsizeList =
            new List<string>() { "8", "9", "10", "10.5", "11", "11.25", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };

        /// <summary>
        /// 指定可能最大フォントサイズ
        /// </summary>
        private const float MAX_FONTSIZE = 72;

        /// <summary>
        /// 現在入力されているフォントサイズ。
        /// </summary>
        public float SelectFontSize { get; private set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// 初期値をコンボボックスに設定して初期化します。
        /// </remarks>
        public FontComboBox() : base()
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Width = 60;
            Items.AddRange(FontsizeList.ToArray());
            SelectedIndex = 5;
        }

        /// <summary>
        /// フォントサイズ範囲内チェック。
        /// </summary>
        /// <remarks>
        /// 現在入力されているフォントサイズがフォントコンボボックスに存在する範囲内のフォントサイズか確認を行います。
        /// </remarks>
        /// <returns>存在する場合はTrue</returns>
        public bool IsWithinFontSize()
        {
            float number;
            var check = float.TryParse(this.Text, out number);

            if (check && number > 0 && number <= MAX_FONTSIZE)
            {
                SelectFontSize = number;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// フォントクラス。
    /// </summary>
    public class ControlFont
    {
        /// <summary>内部定義インスタンス保持ディクショナリ</summary>
        private static readonly Dictionary<string, ControlFont> map = new Dictionary<string, ControlFont>();

        /// <summary>デフォルトフォント</summary>
        public static readonly ControlFont DefaultFont =
            new ControlFont("DefaultFont", "ＭＳ ゴシック", 9F, FontStyle.Regular);

        /// <summary>英数値記号フォント</summary>
        public static readonly ControlFont NumberFont =
            new ControlFont("NumberFont", "MS UI Gothic", 10, FontStyle.Regular);

        /// <summary>デフォルトフォントのPタイプ</summary>
        public static readonly ControlFont DefaultPFont =
            new ControlFont("DefaultPFont", "ＭＳ Ｐゴシック", 9F, FontStyle.Regular);

        /// <summary>名称</summary>
        public string Name { get; private set; }

        /// <summary>フォントファミリ名称</summary>
        public string FamilyName { get; private set; }

        /// <summary>サイズ</summary>
        public float Size { get; private set; }

        /// <summary>フォントスタイル</summary>
        public FontStyle FontStyle { get; private set; }

        /// <summary>
        /// 定義情報に応じた Font オブジェクト。
        /// </summary>
        public Font Font { get; private set; }

        /// <summary>内部インスタンスの一覧コレクションへの参照を戻す。</summary>
        /// <returns>Dictionary＜string, TextBoxColorPalette＞</returns>
        public static Dictionary<string, ControlFont> GetValues()
        {
            return map;
        }

        /// <summary>サーチメソッド</summary>
        /// <param name="name">名称</param>
        /// <exception cref="ArgumentException">
        /// 引数が未定義コードの場合例外をスローします。
        /// </exception>
        /// <returns>System.String</returns>
        public static string FamilyNameOf(string name)
        {
            ControlFont value;
            if (map.TryGetValue(name, out value))
            {
                return value.FamilyName;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 指定されたサイズでデフォルトフォントを生成します。
        /// </summary>
        /// <param name="size">生成サイズ</param>
        /// <returns></returns>
        public static Font GetDefaultFont(float size)
        {
            return new Font(DefaultFont.Font.Name, size, FontStyle.Regular);
        }

        /// <summary>
        /// 指定されたサイズで英数字記号用フォントを生成します。
        /// </summary>
        /// <param name="size">生成サイズ</param>
        /// <returns></returns>
        public static Font GetNumberFont(float size)
        {
            return new Font(NumberFont.Font.Name, size, FontStyle.Regular);
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="name">名称</param>
        /// <param name="familyname">フォントファミリ名称</param>
        /// <param name="size">サイズ</param>
        /// <param name="fontStyle">フォントスタイル</param>
        private ControlFont(string name, string familyname, float size, FontStyle fontStyle)
        {
            this.Name = name;
            this.FamilyName = familyname;
            this.Size = size;
            this.FontStyle = fontStyle;
            this.Font = new Font(this.FamilyName, this.Size, this.FontStyle);

            map.Add(name, this);
        }
    }

    /// <summary>
    /// やり直し/元に戻す管理クラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UndoManager<T>
    {
        /// <summary>現在表示されているT</summary>
        private T node;

        /// <summary>
        /// 前のノード
        /// </summary>
        private LinkedList<T> previousNode;

        /// <summary>
        /// 次のノード
        /// </summary>
        private LinkedList<T> nextNode;

        /// <summary>
        /// 元に戻せる最大値
        /// </summary>
        private int maxCount;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="maxCount">元に戻せる最大値</param>
        public UndoManager(int maxCount = 1)
        {
            this.maxCount = maxCount;
            this.previousNode = new LinkedList<T>();
            this.nextNode = new LinkedList<T>();
        }

        /// <summary>
        /// 保存を実行
        /// </summary>
        /// <param name="obj">保存するオブジェクト</param>
        public void Do(T prevObj, T newObj)
        {
            this.nextNode.Clear();
            this.node = newObj;

            this.previousNode.AddLast(prevObj);

            if (this.previousNode.Count > this.maxCount)
            {
                this.previousNode.RemoveFirst();
            }
        }

        /// <summary>
        /// 保存を実行
        /// </summary>
        /// <param name="obj">保存するオブジェクト</param>
        public void Do(T newObj)
        {
            this.nextNode.Clear();
            this.node = newObj;

            this.previousNode.AddLast(newObj);

            if (this.previousNode.Count > this.maxCount)
            {
                this.previousNode.RemoveFirst();
            }
        }

        /// <summary>
        /// やり直す
        /// </summary>
        /// <returns></returns>
        public T Redo()
        {
            if (this.nextNode.Count > 0)
            {
                var next = this.nextNode.Last();
                this.nextNode.RemoveLast();

                if (node != null)
                {
                    this.previousNode.AddLast(node);
                }
                else
                {
                    this.previousNode.AddLast(next);
                }
                this.node = next;

                return next;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 元に戻す
        /// </summary>
        /// <returns></returns>
        public T Undo()
        {
            if (this.previousNode.Count > 0)
            {
                var prev = this.previousNode.Last();
                this.previousNode.RemoveLast();

                if (node != null)
                {
                    this.nextNode.AddLast(node);
                }
                else
                {
                    this.nextNode.AddLast(prev);
                }
                this.node = prev;

                return prev;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 前のオブジェクト
        /// </summary>
        public T PrevObj
        {
            get
            {
                if (this.previousNode.Count > 0)
                {
                    return this.previousNode.Last();
                }
                else
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// 次のオブジェクト
        /// </summary>
        public T NextObj
        {
            get
            {
                if (this.nextNode.Count > 0)
                {
                    return this.nextNode.Last();
                }
                else
                {
                    return default(T);
                }
            }
        }
    }
}
