using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.UITestCar.ControlSheet;
using DevPlan.Presentation.UITestCar.Othe.TestCarHistory;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using System.Diagnostics;
using DevPlan.UICommon.Util;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;

namespace DevPlan.Presentation.UITestCar.Othe.ControlSheet
{
    /// <summary>
    /// 管理票検索結果
    /// </summary>
    public partial class ControlSheetListForm : BaseTestCarForm
    {
        #region メンバ変数
        /// <summary>
        /// バインドソース
        /// </summary>
        private BindingSource DataSource = new BindingSource();

        /// <summary>
        /// カスタムテンプレート
        /// </summary>
        private CustomTemplate CustomTemplate = new CustomTemplate();

        /// <summary>
        /// バインド中可否
        /// </summary>
        private bool IsBind = false;

        private List<ControlSheetSearchConditionModel> controlSheetSearchConditionList = null;
        private const int CondHeight = 60;
        private const string DefaultName = "**********";
        private const short ConfigRow = -1;
        private const string Desc = " desc";
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "管理票検索結果"; } }

        /// <summary>ユーザー検索条件</summary>
        public ControlSheetSearchConditionModel UserSearchCondition { get; set; } = null;

        /// <summary>検索条件</summary>
        public ControlSheetModel SearchCondition { get; set; } = null;

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControlSheetListForm()
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
        private void ControlSheetForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //バインド可否
                this.IsBind = true;

                // 権限の取得
                this.UserAuthority = base.GetFunction(FunctionID.TestCarManagement);

                //画面初期化
                this.InitForm();

                //グリッドビュー初期化
                this.InitMultiRow();

                //バインド可否
                this.IsBind = false;
            });
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            //管理所在地
            FormControlUtil.SetComboBoxItem(this.AffiliationComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.Affiliation).Results, false);
            this.AffiliationComboBox.SelectedValue = SessionDto.Affiliation;
            this.ActiveControl = this.AffiliationComboBox;

            //検索条件の設定
            this.SetUserSearchCondition();

            //ダウンロードボタン
            this.DownloadButton.Visible = isExport;

        }

        /// <summary>
        /// MultiRow初期化
        /// </summary>
        private void InitMultiRow()
        {
            var template = new ControlSheetListMultiRowTemplate();

            // カスタムテンプレート適用
            this.CustomTemplate.RowCountLabel = this.RowCountLabel;
            this.CustomTemplate.MultiRow = this.TestCarListMultiRow;
            this.CustomTemplate.ConfigDispLayList = base.GetUserDisplayConfiguration(template);
            this.TestCarListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(template);

            // データーソース
            this.TestCarListMultiRow.DataSource = this.DataSource;

            // カラムへのタグ付け
            this.TestCarListMultiRow.Columns["管理票NO"].Tag = "ExcelCellType(Numbers)";

            //試験車一覧グリッドビューの設定
            this.SetTestCarListGrid(null);
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetListForm_Shown(object sender, EventArgs e)
        {
            // 一覧を未選択状態に設定
            this.TestCarListMultiRow.CurrentCell = null;

            this.ActiveControl = this.SearchButton;
            this.SearchButton.Focus();

            // 管理票検索画面表示
            this.ShowSearchForm(this.UserSearchCondition);
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
            //検索条件がないかかバインド中なら終了
            if (this.SearchCondition == null || this.IsBind == true)
            {
                return;

            }

            //試験車一覧の設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList(this.SearchCondition));

        }
        #endregion

        #region 検索条件選択
        /// <summary>
        /// 検索条件選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //バインド中なら終了
            if (this.IsBind == true)
            {
                return;

            }

            //検索条件が先頭行かどうか
            if (this.SearchConditionComboBox.SelectedIndex == 0)
            {
                //試験車一覧グリッドビューの設定
                this.SetTestCarListGrid(null);
                return;

            }

            var cond = this.SearchConditionComboBox.SelectedItem as ControlSheetSearchConditionModel;

            //設定の保存
            this.ConfigurationSaveButton.Enabled = cond.ユーザーID == SessionDto.UserId;

            //試験車一覧の設定
            FormControlUtil.FormWait(this, () =>
            {
                this.SetTestCarList(new ControlSheetModel
                {
                    //ユーザー検索条件
                    ControlSheetSearchConditionList = this.controlSheetSearchConditionList.Where(x => x.ユーザーID == cond.ユーザーID && x.条件名 == cond.条件名).OrderBy(x => x.行番号).ToList()

                });
            });

            //試験車情報画面表示
            this.ShowTestCarForm();

        }
        #endregion

        #region 他のユーザーの条件も表示選択
        /// <summary>
        /// 他のユーザーの条件も表示選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllUserCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //バインド可否
                this.IsBind = true;

                //検索条件の設定
                this.SetUserSearchCondition();

                //他のユーザーの条件を表示しない場合は設定の保存を押下可に設定
                if (this.AllUserCheckBox.Checked == false)
                {
                    //設定の保存
                    this.ConfigurationSaveButton.Enabled = true;

                }

                //バインド可否
                this.IsBind = false;

            });

        }
        #endregion

        #region 検索画面へボタンクリック
        /// <summary>
        /// 検索画面へボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            //管理票検索画面表示
            this.ShowSearchForm(this.SearchConditionComboBox.SelectedItem as ControlSheetSearchConditionModel);

        }
        #endregion

        #region 設定の保存ボタンクリック
        /// <summary>
        /// 設定の保存ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigurationSaveButton_Click(object sender, EventArgs e)
        {
            using (var form = new ControlSheetSaveForm { CustumTemplate = this.CustomTemplate, SearchConditionNameEnabled = false })
            {
                //保存済み検索条件を選択しているかどうか
                if (this.SearchConditionComboBox.SelectedIndex > 0)
                {
                    //検索条件
                    form.SearchConditionList.AddRange(this.SearchCondition.ControlSheetSearchConditionList);
                }

                //表示
                form.ShowDialog(this);
            }
        }
        #endregion

        #region 試験車使用履歴ボタンクリック
        /// <summary>
        /// 試験車使用履歴ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarHistoryButton_Click(object sender, EventArgs e)
        {
            //一覧を選択しているかどうか
            var cell = this.TestCarListMultiRow.CurrentCell;
            if (cell == null)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            //試験車使用履歴画面表示
            new FormUtil(new TestCarHistoryForm { TestCar = this.GetTestCarByMultiRow(cell.RowIndex), UserAuthority = this.UserAuthority }).SingleFormShow(this);
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
            return this.TestCarListMultiRow.Rows[rowIndex].DataBoundItem as TestCarCommonModel;
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
            var util = new MultiRowUtil(this.TestCarListMultiRow);
            util.Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
        }
        #endregion

        #region 検索条件の設定
        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetUserSearchCondition()
        {
            //ユーザー検索条件リスト
            this.controlSheetSearchConditionList = base.GetUserSearchConditionList(this.AllUserCheckBox.Checked == true ? null : SessionDto.UserId);

            //ユーザー検索条件
            var list = base.GetUserSearchConditionNameList(this.controlSheetSearchConditionList);

            //保存済み検索条件
            FormControlUtil.SetComboBoxItem(this.SearchConditionComboBox, list);

        }
        #endregion

        #region 試験車一覧の設定
        /// <summary>
        /// 試験車一覧の設定
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <param name="flg">スクロール位置</param>
        private void SetTestCarList(ControlSheetModel cond, bool flg = false)
        {
            // 検索条件のセット
            this.SearchCondition = cond;

            // 試験車一覧グリッドビューの設定
            this.SetTestCarListGrid(cond.ControlSheetSearchConditionList);

            // 描画停止
            this.TestCarListMultiRow.SuspendLayout();

            // データの取得
            var list = this.GetTestCarList(cond);

            // バインドフラグ
            this.IsBind = true;

            // データバインド
            this.CustomTemplate.SetDataSource(list, this.DataSource);

            // バインドフラグ
            this.IsBind = false;

            // 描画再開
            this.TestCarListMultiRow.ResumeLayout();

            // 検索結果文言
            this.SearchResultLabel.Text = (list == null || list.Any() == false) ? Resources.KKM00005 : "";

            if (flg)
            {
                return;
            }

            // スクロールの初期化
            this.TestCarListMultiRow.FirstDisplayedLocation = new Point(0, 0);

            // 一覧を未選択状態に設定
            this.TestCarListMultiRow.CurrentCell = null;
        }
        #endregion

        #region 試験車一覧グリッドビューの設定
        /// <summary>
        /// 試験車一覧グリッドビューの設定
        /// </summary>
        /// <param name="list">列</param>
        private void SetTestCarListGrid(IEnumerable<ControlSheetSearchConditionModel> list)
        {
            // 設定対象が無ければ初期設定
            if (list == null || list.Any() == false)
            {
                // 試験車一覧グリッドビューの設定
                list = this.controlSheetSearchConditionList.Where(x => x.ユーザーID == SessionDto.UserId && x.条件名 == DefaultName && x.行番号 == ConfigRow).ToArray();
            }

            // 表示設定があるかどうか
            var config = list.FirstOrDefault(x => x.行番号 == ConfigRow);
            if (config != null)
            {
                var display = this.CustomTemplate.Display;

                var displayList = new List<MultiRowDisplayModel>();

                var i = 1;

                foreach (var name in (config.TEXT ?? "").Split(',').Select(x => x.Trim()))
                {
                    // 表示対象の列かどうか
                    var col = display.FirstOrDefault(x => x.HeaderText.Trim().ToUpper() == name.ToUpper());
                    if (col != null)
                    {
                        //表示
                        col.Visible = true;

                        //表示順
                        col.DisplayIndex = i++;

                        displayList.Add(col);
                    }
                }

                // 表示
                this.CustomTemplate.Display = displayList;

                var sort = this.CustomTemplate.GetSortTarget();

                var sortList = new List<MultiRowSortModel>();

                foreach (var name in (config.CONJUNCTION ?? "").Split(',').Select(x => x.Trim()))
                {
                    // ソート対象の列かどうか
                    var col = sort.FirstOrDefault(x => x.Name.Trim().ToLower() == name.ToLower().Replace(Desc, "").Trim());
                    if (col != null)
                    {
                        // 降順可否
                        col.IsDesc = name.ToLower().EndsWith(Desc);

                        sortList.Add(col);
                    }
                }

                // ソート
                this.CustomTemplate.Sort = sortList;
            }
        }
        #endregion

        #region 管理票検索画面表示
        /// <summary>
        /// 管理票検索画面表示
        /// </summary>
        /// <param name="selected">選択検索条件</param>
        /// <param name="isNoneResult">処理結果無可否</param>
        private void ShowSearchForm(ControlSheetSearchConditionModel selected, bool isNoneResult = false)
        {
            //バインド可否
            this.IsBind = true;

            //検索条件がなしで選択検索条件があるかどうか
            if (this.SearchCondition == null && selected != null)
            {
                this.SearchCondition = new ControlSheetModel
                {
                    //ユーザー検索条件
                    ControlSheetSearchConditionList = this.controlSheetSearchConditionList.Where(x => x.ユーザーID == selected.ユーザーID && x.条件名 == selected.条件名 && x.行番号 != ConfigRow).OrderBy(x => x.行番号).ToArray()

                };

            }

            using (var form = new ControlSheetSearchForm
            {
                //他ユーザー検索条件表示可否
                IsAllUser = this.AllUserCheckBox.Checked,

                //カスタムテンプレート
                CustomTemplate = this.CustomTemplate,

                //選択検索条件
                SelectedUserSearchCondition = selected,

                //検索条件
                SearchCondition = this.SearchCondition,

                //処理結果無可否
                IsNoneResult = isNoneResult

            })
            {
                //OKかどうか
                var isSearch = form.ShowDialog(this) == DialogResult.OK;
                if (isSearch == true)
                {
                    //検索条件を選択したかどうか
                    selected = form.SelectedUserSearchCondition;
                    if (selected != null)
                    {
                        //他ユーザーの条件も表示
                        this.AllUserCheckBox.Checked = form.IsAllUser;

                    }

                    //試験車一覧の設定
                    FormControlUtil.FormWait(this, () => this.SetTestCarList(form.SearchCondition));

                    //一覧があるかどうか
                    if (this.TestCarListMultiRow.RowCount == 0)
                    {
                        //管理票検索画面表示
                        this.ShowSearchForm(selected, true);
                        return;

                    }

                }

                //検索条件の設定
                this.SetUserSearchCondition();

                //検索条件を選択したかどうか
                if (selected != null)
                {
                    foreach (ControlSheetSearchConditionModel cond in this.SearchConditionComboBox.Items)
                    {
                        //選択状態に設定
                        if (cond.ユーザーID == selected.ユーザーID && cond.条件名 == selected.条件名)
                        {
                            this.SearchConditionComboBox.SelectedItem = cond;
                            break;

                        }

                    }

                }

                //検索したかどうか
                if (isSearch == true)
                {
                    //試験車情報画面表示
                    this.ShowTestCarForm();

                }

            }

            //バインド可否
            this.IsBind = false;

        }
        #endregion

        #region 試験車情報画面表示
        /// <summary>
        /// 試験車情報画面表示
        /// </summary>
        private void ShowTestCarForm()
        {
            //1件のみ表示かどうか
            if (this.TestCarListMultiRow.RowCount == 1)
            {
                //試験車情報画面表示
                this.ShowTestCarForm(this.GetTestCarByMultiRow(0));
            }
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

        #region API
        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<TestCarCommonModel> GetTestCarList(ControlSheetModel cond)
        {
            //管理票検索条件が無ければ初期化
            if (cond.ControlSheetSearch == null)
            {
                cond.ControlSheetSearch = new ControlSheetSearchModel();

            }

            //ユーザー検索条件があれば有効な条件のみ設定
            if (cond.ControlSheetSearchConditionList != null)
            {
                cond.ControlSheetSearchConditionList = cond.ControlSheetSearchConditionList.Where(x => x.行番号 >= 0).ToArray();

            }

            //管理所在地
            var syozoku = this.AffiliationComboBox.SelectedValue;
            cond.ControlSheetSearch.ESTABLISHMENT = syozoku == null ? "" : syozoku.ToString();

            //APIで取得
            var res = HttpUtil.PostResponse<TestCarCommonModel>(ControllerType.ControlSheet, cond);

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
        /// MulutiRowソートアイコンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListSortPictureBox_Click(object sender, EventArgs e)
        {
            // ソート指定画面表示
            base.ShowSortForm(this.CustomTemplate);
        }

        #endregion
    }
}
