using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UITestCar.ControlSheet;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Util;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;

namespace DevPlan.Presentation.UITestCar.Othe.DesignatedMonthNumber
{
    /// <summary>
    /// 指定月台数リスト
    /// </summary>
    public partial class DesignatedMonthNumberForm : BaseTestCarForm
    {
        #region メンバ変数
        private const int CondHeight = 120;

        private Dictionary<TabPage, CustomTemplate> CustomuTemplateMap = new Dictionary<TabPage, CustomTemplate>();

        private bool isBind = false;

        private DesignatedMonthNumberSearchModel searchCondition = null;

        private readonly Dictionary<DesignatedMonthNumberTargetType, string> TargetMap = new Dictionary<DesignatedMonthNumberTargetType, string>
        {
            { DesignatedMonthNumberTargetType.Possession, "保有台数" },
            { DesignatedMonthNumberTargetType.New, "新規作製" },
            { DesignatedMonthNumberTargetType.Refurbishment, "改修" },
            { DesignatedMonthNumberTargetType.Disposal, "廃却" }

        };

        private readonly Dictionary<AssetType, string> AssetMap = new Dictionary<AssetType, string>
        {
            { AssetType.All, "全て" },
            { AssetType.WhiteNumber, "固定資産:白ナンバー" },
            { AssetType.NoNumber, "固定資産:ナンバー無し" },
            { AssetType.OutAsset, "資産外" },
            { AssetType.Lease, "リース" }

        };
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "指定月台数リスト"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignatedMonthNumberForm()
        {
            InitializeComponent();

        }
        #endregion

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignatedMonthNumberForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // バインド可否
                this.isBind = true;

                // 権限
                this.UserAuthority = base.GetFunction(FunctionID.TestCarManagement);

                // 画面初期化
                this.InitForm();

                // MultiRow初期化
                this.InitMultiRow();

                // バインド可否
                this.isBind = false;

                // 試験車一覧設定
                this.SetTestCarList();
            });
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isExport = this.UserAuthority.EXPORT_FLG == '1';

            //期間
            this.StartDateTimePicker.Value = DateTimeUtil.GetFirstDay();
            this.EndDateTimePicker.Value = DateTimeUtil.GetLastDay();
            this.ActiveControl = this.StartDateTimePicker;

            //管理所在地
            FormControlUtil.SetComboBoxItem(this.AffiliationComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.Affiliation).Results, false);
            this.AffiliationComboBox.SelectedValue = SessionDto.Affiliation;

            //開発符号
            FormControlUtil.SetComboBoxItem(this.GeneralCodeComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.GeneralCodeInfo).Results);

            //仕向地
            FormControlUtil.SetComboBoxItem(this.DestinationComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.DestinationInfo).Results);

            //取得種別
            FormControlUtil.SetComboBoxItem(this.TargetComboBox, this.TargetMap, false);
            this.TargetComboBox.SelectedValue = DesignatedMonthNumberTargetType.Possession;

            //資産種別
            FormControlUtil.SetComboBoxItem(this.AssetComboBox, this.AssetMap, false);
            this.AssetComboBox.SelectedValue = AssetType.All;

            //一旦すべてのタブを表示
            foreach (TabPage tab in this.TestCaeTabControl.TabPages)
            {
                tab.Show();

            }

            //台数集計結果を選択
            this.TestCaeTabControl.SelectedTab = this.SumTabPage;

            //表示設定
            this.ListConfigPictureBox.Visible = false;

            //ダウンロードボタン
            this.DownloadButton.Visible = isExport;

        }

        /// <summary>
        /// MultiRow初期化
        /// </summary>
        private void InitMultiRow()
        {
            var template = new Template();
            var multiRow = new GcMultiRow();

            // 台数集計結果
            if (this.TestCaeTabControl.SelectedTab == this.SumTabPage)
            {
                template = new DesignatedMonthNumberTab0MultiRowTemplate();
                multiRow = this.TestCarList0MultiRow;
            }
            // 保有台数
            if (this.TestCaeTabControl.SelectedTab == this.PossessionTabPage)
            {
                template = new DesignatedMonthNumberTab1MultiRowTemplate();
                multiRow = this.TestCarList1MultiRow;
            }
            // 新規作成
            if (this.TestCaeTabControl.SelectedTab == this.NewTabPage)
            {
                template = new DesignatedMonthNumberTab2MultiRowTemplate();
                multiRow = this.TestCarList2MultiRow;
            }
            // 改修
            if (this.TestCaeTabControl.SelectedTab == this.RefurbishmentTabPage)
            {
                template = new DesignatedMonthNumberTab3MultiRowTemplate();
                multiRow = this.TestCarList3MultiRow;
            }
            // 廃却
            if (this.TestCaeTabControl.SelectedTab == this.DisposalTabPage)
            {
                template = new DesignatedMonthNumberTab4MultiRowTemplate();
                multiRow = this.TestCarList4MultiRow;
            }

            var customuTemplate = this.CustomuTemplateMap[this.TestCaeTabControl.SelectedTab] = new CustomTemplate();

            // カスタムテンプレート適用
            customuTemplate.RowCountLabel = this.RowCountLabel;
            customuTemplate.MultiRow = multiRow;
            customuTemplate.ConfigDispLayList = base.GetUserDisplayConfiguration(this.FormTitle + this.TestCaeTabControl.SelectedIndex, template);
            multiRow.Template = customuTemplate.SetContextMenuTemplate(template);
        }
        #endregion

        #region 検索条件ボタンクリック
        /// <summary>
        /// 検索条件ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            // 検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, null, new List<Control> { this.ButtonPanel }, CondHeight);
        }
        #endregion

        #region 管理所在地選択
        /// <summary>
        /// 管理所在地選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffiliationComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //バインド中なら終了
            if (this.isBind == true)
            {
                return;

            }

            //試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());

        }
        #endregion

        #region 課マウスクリック
        /// <summary>
        /// 課マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            using (var form = new SectionListForm { DEPARTMENT_ID = SessionDto.DepartmentID })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var value = new ComboBoxDto
                    {
                        CODE = form.SECTION_ID,
                        NAME = form.SECTION_CODE
                    };

                    //担当をセット
                    FormControlUtil.SetComboBoxItem(this.SectionComboBox, new[] { value }, false);
                    this.SectionComboBox.SelectedIndex = 0;

                }

            }

        }
        #endregion

        #region 担当マウスクリック
        /// <summary>
        /// 担当マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            using (var form = new SectionGroupListForm { DEPARTMENT_ID = SessionDto.DepartmentID, SECTION_ID = SessionDto.SectionID })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var value = new ComboBoxDto
                    {
                        CODE = form.SECTION_GROUP_ID,
                        NAME = form.SECTION_GROUP_CODE
                    };

                    //担当をセット
                    FormControlUtil.SetComboBoxItem(this.SectionGroupComboBox, new[] { value }, false);
                    this.SectionGroupComboBox.SelectedIndex = 0;

                }

            }

        }
        #endregion

        #region 検索ボタンクリック
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            //試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
        }
        #endregion

        #region 取得種別選択
        /// <summary>
        /// 取得種別選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //バインド中なら終了
            if (this.isBind == true)
            {
                return;

            }

            FormControlUtil.FormWait(this, () =>
            {
                //画面の表示設定
                this.SetFormVisible();

                //試験車一覧設定
                this.SetTestCarList();
            });
        }
        #endregion

        #region 資産種別選択
        /// <summary>
        /// 資産種別選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssetComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //バインド中なら終了
            if (this.isBind == true)
            {
                return;

            }

            //試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
        }
        #endregion

        #region 集計種別選択
        /// <summary>
        /// 集計種別選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AggregateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //未選択なら終了
            var radio = sender as RadioButton;
            if (radio == null || radio.Checked == false)
            {
                return;

            }

            FormControlUtil.FormWait(this, () =>
            {
                //画面の表示設定
                this.SetFormVisible();

                //試験車一覧設定
                this.SetTestCarList();
            });
        }
        #endregion

        #region クリアボタンクリック
        /// <summary>
        /// クリアボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            //期間
            this.StartDateTimePicker.Value = DateTimeUtil.GetFirstDay();
            this.EndDateTimePicker.Value = DateTimeUtil.GetLastDay();

            //管理票NO
            this.ManagementNoTextBox.Text = "";

            //課
            FormControlUtil.ClearComboBoxDataSource(this.SectionComboBox);

            //担当
            FormControlUtil.ClearComboBoxDataSource(this.SectionGroupComboBox);

            //開発符号
            this.GeneralCodeComboBox.Text = "";

            //仕向地
            this.DestinationComboBox.SelectedIndex = 0;

            //試作時期
            this.TrialProductionSeasonTextBox.Text = "";

            //号車
            this.CarTextBox.Text = "";

            //車体番号
            this.CarBodyNoTextBox.Text = "";

            //固定資産NO
            this.FixedAssetTextBox.Text = "";

        }
        #endregion

        #region 試験車選択
        /// <summary>
        /// 試験車選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCaeTabControl_Selected(object sender, TabControlEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                if (!this.CustomuTemplateMap.ContainsKey(this.TestCaeTabControl.SelectedTab))
                {
                    // MultiRow初期化
                    this.InitMultiRow();
                }

                //画面の表示設定
                this.SetFormVisible();

                //試験車一覧設定
                this.SetTestCarList();
            });
        }
        #endregion

        #region 試験車一覧
        /// <summary>
        /// 試験車一覧セルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarListMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            //無効な行の場合は終了
            if (e.RowIndex < 0)
            {
                return;

            }

            //試験車情報画面表示
            this.ShowTestCarForm(this.GetTestCarByMultiRow(e.RowIndex));

        }

        /// <summary>
        /// MultiRowから試験車を取得
        /// </summary>
        /// <param name="rowIndex">行インデックス</param>
        /// <returns></returns>
        private TestCarCommonModel GetTestCarByMultiRow(int rowIndex)
        {
            return this.CustomuTemplateMap[this.TestCaeTabControl.SelectedTab].MultiRow.Rows[rowIndex].DataBoundItem as TestCarCommonModel;
        }

        /// <summary>
        /// 試験車情報画面表示
        /// </summary>
        /// <param name="testCar">試験車</param>
        private void ShowTestCarForm(TestCarCommonModel testCar)
        {
            FormControlUtil.FormWait(this, () =>
            {
                new FormUtil(new ControlSheetIssueForm { TestCar = testCar, UserAuthority = this.UserAuthority }).SingleFormShow(this);
            });
        }
        #endregion

        #region エクセル出力ボタンクリック
        /// <summary>
        /// エクセル出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                new MultiRowUtil(this.CustomuTemplateMap[this.TestCaeTabControl.SelectedTab].MultiRow).Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
            });
        }
        #endregion

        #region 画面の表示設定
        /// <summary>
        /// 画面の表示設定
        /// </summary>
        private void SetFormVisible()
        {
            //描画停止
            this.SuspendLayout();

            //台数集計結果のタブを選択しているかどうか
            var tab = this.TestCaeTabControl.SelectedTab;
            if (tab == this.SumTabPage)
            {
                //保有台数を選択しているかどうか
                if ((DesignatedMonthNumberTargetType)this.TargetComboBox.SelectedValue == DesignatedMonthNumberTargetType.Possession)
                {
                    //期間
                    this.RangeLabel.Visible = false;
                    this.EndDateTimePicker.Visible = false;

                }
                else
                {
                    //期間
                    this.RangeLabel.Visible = true;
                    this.EndDateTimePicker.Visible = true;

                }

                //取得種別
                this.TargetComboBox.Visible = true;

                //資産種別
                this.AssetComboBox.Visible = false;

                //集計種別
                this.AggregatePanel.Visible = true;

                //表示設定
                this.ListConfigPictureBox.Visible = false;

                //文言
                this.DoubleClickLabel.Visible = false;

                //MultiRow
                this.SetMultiRowVisible();
            }
            else
            {
                //保有台数のタブを選択しているかどうか
                if (tab == this.PossessionTabPage)
                {
                    //期間
                    this.RangeLabel.Visible = false;
                    this.EndDateTimePicker.Visible = false;

                }
                else
                {
                    //期間
                    this.RangeLabel.Visible = true;
                    this.EndDateTimePicker.Visible = true;

                }

                //取得種別
                this.TargetComboBox.Visible = false;

                //資産種別
                this.AssetComboBox.Visible = true;

                //集計種別
                this.AggregatePanel.Visible = false;

                //表示設定
                this.ListConfigPictureBox.Visible = true;

                //文言
                this.DoubleClickLabel.Visible = true;

            }

            //描画再開
            this.ResumeLayout();

            //再描画
            this.Refresh();

        }

        /// <summary>
        /// MultiRowの表示設定
        /// </summary>
        private void SetMultiRowVisible()
        {
            var type = this.GetAggregateType();

            var template = this.CustomuTemplateMap[this.TestCaeTabControl.SelectedTab].MultiRow.Template;
            var headers = template.ColumnHeaders[0];
            var datas = template.Row;

            var sectionWidth = headers.Cells["columnHeaderCell2"].Width;
            var groupWidth = headers.Cells["columnHeaderCell3"].Width;

            // 集計別ごとの分岐
            if (type == AggregateType.Department)
            {
                sectionWidth = headers.Cells["columnHeaderCell2"].Visible ? sectionWidth : 0;
                groupWidth = headers.Cells["columnHeaderCell3"].Visible ? groupWidth : 0;

                // 見出し
                headers.Cells["columnHeaderCell2"].Visible = false;
                headers.Cells["columnHeaderCell3"].Visible = false;

                // フィルタリング
                headers.Cells["FilteringTextBoxCellcolumnHeaderCell2"].Visible = false;
                headers.Cells["FilteringTextBoxCellcolumnHeaderCell3"].Visible = false;

                // データ
                datas.Cells["課"].Visible = false;
                datas.Cells["担当"].Visible = false;
            }
            if (type == AggregateType.Section)
            {
                sectionWidth = headers.Cells["columnHeaderCell2"].Visible ? 0 : - sectionWidth;
                groupWidth = headers.Cells["columnHeaderCell3"].Visible ? groupWidth : 0;

                headers.Cells["columnHeaderCell2"].Visible = true;
                headers.Cells["columnHeaderCell3"].Visible = false;

                // フィルタリング
                headers.Cells["FilteringTextBoxCellcolumnHeaderCell2"].Visible = true;
                headers.Cells["FilteringTextBoxCellcolumnHeaderCell3"].Visible = false;

                // データ
                datas.Cells["課"].Visible = true;
                datas.Cells["担当"].Visible = false;
            }
            if (type == AggregateType.SectionGroup)
            {
                sectionWidth = headers.Cells["columnHeaderCell2"].Visible ? 0 : - sectionWidth;
                groupWidth = headers.Cells["columnHeaderCell3"].Visible ? 0 : - groupWidth;

                headers.Cells["columnHeaderCell2"].Visible = true;
                headers.Cells["columnHeaderCell3"].Visible = true;

                // フィルタリング
                headers.Cells["FilteringTextBoxCellcolumnHeaderCell2"].Visible = true;
                headers.Cells["FilteringTextBoxCellcolumnHeaderCell3"].Visible = true;

                // データ
                datas.Cells["課"].Visible = true;
                datas.Cells["担当"].Visible = true;
            }

            var diff = sectionWidth + groupWidth;
            var minIndex = headers.Cells["columnHeaderCell2"].CellIndex;

            // 表示位置調整
            foreach (var header in headers.Cells.Where(x => x.CellIndex > minIndex && x is ColumnHeaderCell && x.Name != "CornerHeader").OrderBy(x => x.CellIndex))
            {
                var filter = headers.Cells[string.Concat("FilteringTextBoxCell", header.Name)];
                var data = datas.Cells[header.CellIndex];

                header.Location = new Point(header.Location.X - diff, header.Location.Y);
                filter.Location = new Point(filter.Location.X - diff, filter.Location.Y);
                data.Location = new Point(data.Location.X - diff, data.Location.Y);
            }

            this.CustomuTemplateMap[this.TestCaeTabControl.SelectedTab].MultiRow.Template = template;
        }
        #endregion

        #region 集計種別取得
        /// <summary>
        /// 集計種別取得
        /// </summary>
        /// <returns></returns>
        private AggregateType GetAggregateType()
        {
            return (AggregateType)Enum.Parse(typeof(AggregateType), FormControlUtil.GetRadioButtonValue(this.AggregatePanel));
        }
        #endregion

        #region 試験車一覧設定
        /// <summary>
        /// 試験車一覧設定
        /// </summary>
        /// <param name="isKeepScroll"></param>
        private void SetTestCarList(bool isKeepScroll = false)
        {
            //検索条件がOKかどうか
            if (this.IsSearch() == false)
            {
                return;
            }

            // 検索条件のセット
            this.SetSearchCondition();

            var customTemplate = this.CustomuTemplateMap[this.TestCaeTabControl.SelectedTab];
            var multiRow = customTemplate.MultiRow;

            // 描画停止
            multiRow.SuspendLayout();

            //台数集計結果のタブを選択しているかどうか
            if (this.TestCaeTabControl.SelectedTab == this.SumTabPage)
            {
                // データバインド
                customTemplate.SetDataSource(this.GetSumList());
            }
            else
            {
                // データバインド
                customTemplate.SetDataSource(this.GetTestCarList());
            }

            // 描画再開
            multiRow.ResumeLayout();

            // 検索結果文言
            this.SearchResultLabel.Text = (multiRow == null || multiRow.Rows.Count == 0) ? Resources.KKM00005 : "";

            if (isKeepScroll)
            {
                return;
            }

            // スクロールの初期化
            multiRow.FirstDisplayedLocation = new Point(0, 0);

            // 一覧を未選択状態に設定
            multiRow.CurrentCell = null;
        }

        /// <summary>
        /// 検索のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearch()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            //期間の大小チェック
            map[this.StartDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                //期間Toが表示されていて期間Fromと期間Toが入力してある場合のみチェック
                if (this.EndDateTimePicker.Visible == true && this.StartDateTimePicker.Value != null && this.EndDateTimePicker.Value != null)
                {
                    //開始日が終了日より大きい場合はエラー
                    if (this.StartDateTimePicker.SelectedDate > this.EndDateTimePicker.SelectedDate)
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM00018;

                        //背景色を変更
                        this.StartDateTimePicker.BackColor = Const.ErrorBackColor;
                        this.EndDateTimePicker.BackColor = Const.ErrorBackColor;

                    }

                }

                return errMsg;

            };

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            return true;

        }

        /// <summary>
        /// 検索条件のセット
        /// </summary>
        /// <returns></returns>
        private void SetSearchCondition()
        {
            Func<ComboBox, string> getValue = cmb => cmb.SelectedIndex < 0 || cmb.SelectedValue == null ? null : cmb.SelectedValue.ToString();

            var target = (DesignatedMonthNumberTargetType)this.TargetComboBox.SelectedValue;

            //台数集計結果を選択しているかどうか
            if (this.TestCaeTabControl.SelectedTab != this.SumTabPage)
            {
                target = (DesignatedMonthNumberTargetType)this.TestCaeTabControl.SelectedIndex;
            }

            //検索条件
            this.searchCondition = new DesignatedMonthNumberSearchModel
            {
                //期間(From)
                START_DATE = this.StartDateTimePicker.SelectedDate,

                //期間(To)
                END_DATE = this.EndDateTimePicker.SelectedDate,

                //管理所在地
                ESTABLISHMENT = getValue(this.AffiliationComboBox),

                //管理票NO
                管理票NO = this.ManagementNoTextBox.Text,

                //課ID
                SECTION_ID = getValue(this.SectionComboBox),

                //担当ID
                SECTION_GROUP_ID = getValue(this.SectionGroupComboBox),

                //開発符号
                開発符号 = this.GeneralCodeComboBox.Text,

                //仕向地
                仕向地 = getValue(this.DestinationComboBox),

                //試作時期
                試作時期 = this.TrialProductionSeasonTextBox.Text,

                //号車
                号車 = this.CarTextBox.Text,

                //車体番号
                車体番号 = this.CarBodyNoTextBox.Text,

                //固定資産NO
                固定資産NO = this.FixedAssetTextBox.Text,

                //取得種別
                TARGET_TYPE = target,

                //資産種別
                ASSET_TYPE = (AssetType)this.AssetComboBox.SelectedValue,

                //集計種別
                AGGREGATE_TYPE = this.GetAggregateType()
            };
        }
        #endregion

        #region API
        /// <summary>
        /// 台数集計結果取得
        /// </summary>
        /// <returns></returns>
        private List<NumberAggregateModel> GetSumList()
        {
            //APIで取得
            var res = HttpUtil.PostResponse<NumberAggregateModel>(ControllerType.NumberAggregate, this.searchCondition);

            //レスポンスが取得できたかどうか
            var list = new List<NumberAggregateModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <returns></returns>
        private List<TestCarCommonModel> GetTestCarList()
        {
            //APIで取得
            var res = HttpUtil.PostResponse<TestCarCommonModel>(ControllerType.DesignatedMonthNumber, this.searchCondition);

            //レスポンスが取得できたかどうか
            var list = new List<TestCarCommonModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion

        #region MultiRow 共通操作

        /// <summary>
        /// MulutiRow設定アイコンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListConfigPictureBox_Click(object sender, EventArgs e)
        {
            var formName = this.FormTitle + this.TestCaeTabControl.SelectedIndex;
            var customTemplate = this.CustomuTemplateMap[this.TestCaeTabControl.SelectedTab];
            
            // 表示設定画面表示
            base.ShowDisplayForm(formName, customTemplate);
        }

        /// <summary>
        /// MulutiRowソートアイコンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListSortPictureBox_Click(object sender, EventArgs e)
        {
            var customTemplate = this.CustomuTemplateMap[this.TestCaeTabControl.SelectedTab];

            // ソート指定画面表示
            base.ShowSortForm(customTemplate);
        }

        #endregion
    }
}
