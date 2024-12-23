using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UITestCar.Othe.TestCarHistory;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Util;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;

namespace DevPlan.Presentation.UITestCar.ControlSheet
{
    /// <summary>
    /// 試験車リスト（管理票リスト）
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
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "試験車リスト"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>検索条件</summary>
        private TestCarCommonSearchModel ListSearchCond { get; set; }

        /// <summary>取得リスト</summary>
        private List<TestCarCommonModel> DataList { get; set; } = new List<TestCarCommonModel>();
        #endregion

        #region 定義
        /// <summary>検索条件パネルの高さ</summary>
        private const int CondHeight = 150;
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

        #region 画面のセット

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetList_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // バインドフラグ
                this.IsBind = true;

                try
                {
                    // 権限の取得
                    this.UserAuthority = base.GetFunction(FunctionID.TestCarManagement);

                    // 画面の初期化
                    this.InitForm();

                    // MultiRowの初期化
                    this.InitMultiRow();

                    // データのセット
                    this.SetDataList();
                }
                finally
                {
                    // バインドフラグ
                    this.IsBind = false;
                }
            });
        }
        #endregion

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';

            var today = DateTime.Today;

            // 初期表示フォーカス
            this.ActiveControl = this.EstablishmentComboBox;

            // 管理所在地
            FormControlUtil.SetComboBoxItem(this.EstablishmentComboBox, GetAffiliationList(), false);
            this.EstablishmentComboBox.SelectedValue = SessionDto.Affiliation;

            // 発行年月日
            this.StartDateTimePicker.Value = null;
            this.EndDateTimePicker.Value = null;

            // 開発符号
            FormControlUtil.SetComboBoxItem(this.GeneralCodeComboBox, GetGeneralCodeList());
            this.GeneralCodeComboBox.SelectedValue = string.Empty;

            // 更新権限なし
            if (!isUpdate)
            {
                // 削除ボタン
                this.DeleteButton.Visible = isUpdate;

                // 試験車の新規作成ボタン
                this.ControlSheetIssueButton.Visible = isUpdate;
            }

            // 出力権限なし
            if (!isExport)
            {
                // ダウンロードボタン
                this.DownloadButton.Visible = isExport;
            }
        }
        #endregion

        #region MultiRowの初期化
        /// <summary>
        /// MultiRowの初期化
        /// </summary>
        private void InitMultiRow()
        {
            var template = new ControlSheetListMultiRowTemplate();

            // カスタムテンプレート適用
            this.CustomTemplate.RowCountLabel = this.RowCountLabel;
            this.CustomTemplate.MultiRow = this.ControlSheetMultiRow;
            this.CustomTemplate.ConfigDispLayList = base.GetUserDisplayConfiguration(template);
            this.ControlSheetMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(template);

            // データーソース
            this.ControlSheetMultiRow.DataSource = this.DataSource;
        }
        #endregion

        #endregion

        #region 一覧設定
        /// <summary>
        /// 一覧設定
        /// </summary>
        /// <param name="isKeepScroll"></param>
        private void SetDataList(bool isKeepScroll = false)
        {
            // 検索条件チェック
            if (!this.IsSearchConditionCheck()) return;

            // 検索条件のセット
            this.SetSearchCondition();

            // 描画停止
            this.ControlSheetMultiRow.SuspendLayout();

            // データの取得
            this.DataList = this.GetDataList(this.ListSearchCond);
            
            // バインドフラグ
            this.IsBind = true;

            // データバインド
            this.CustomTemplate.SetDataSource(this.DataList, this.DataSource);

            // バインドフラグ
            this.IsBind = false;

            // 描画再開
            this.ControlSheetMultiRow.ResumeLayout();

            // リストが取得できたかどうか
            this.MessageLabel.Text = this.DataList.Any() == true ? "" : Resources.KKM00005;

            if (isKeepScroll)
            {
                return;
            }

            // スクロールの初期化
            this.ControlSheetMultiRow.FirstDisplayedLocation = new Point(0, 0);

            // 一覧を未選択状態に設定
            this.ControlSheetMultiRow.CurrentCell = null;
        }
        #endregion

        #region 検索条件のチェック
        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchConditionCheck()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            var start = this.StartDateTimePicker.SelectedDate;
            var end = this.EndDateTimePicker.SelectedDate;

            // 期間の大小チェック
            map[this.StartDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                // 期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー
                if (start != null && end != null && start > end)
                {
                    // エラーメッセージ
                    errMsg = Resources.KKM00018;

                    // 背景色を変更
                    this.StartDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.EndDateTimePicker.BackColor = Const.ErrorBackColor;
                }

                return errMsg;
            };

            // 入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);

            if (msg != "")
            {
                Messenger.Warn(msg);

                return false;
            }

            return true;
        }
        #endregion

        #region 検索条件のセット
        /// <summary>
        /// 検索条件のセット
        /// </summary>
        /// <returns></returns>
        private void SetSearchCondition()
        {
            string hakkoutxt = string.Empty;
            bool? haikyakuflg = null;

            if (this.IssueYesRadioButton.Checked) hakkoutxt = "済";
            if (this.IssueNoRadioButton.Checked) hakkoutxt = "未";

            if (this.DisposalYesRadioButton.Checked) haikyakuflg = true;
            if (this.DisposalNoRadioButton.Checked) haikyakuflg = false;

            this.ListSearchCond = new TestCarCommonSearchModel()
            {
                ESTABLISHMENT = (string)this.EstablishmentComboBox.SelectedValue,
                START_DATE = this.StartDateTimePicker.SelectedDate,
                END_DATE = this.EndDateTimePicker.SelectedDate,
                管理票NO = this.ControlNoTextBox.Text,
                開発符号 = this.GeneralCodeComboBox.Text,
                試作時期 = this.PrototypeTimingTextBox.Text,
                車体番号 = this.VehicleNotextBox.Text,
                号車 = this.VehicleTextBox.Text,
                仕向地 = this.ShimukechiTextBox.Text,
                メーカー名 = this.MakerNameTextBox.Text,
                外製車名 = this.OuterCarNameTextBox.Text,
                管理票発行有無 = hakkoutxt,
                廃却フラグ = haikyakuflg
            };
        }

        /// <summary>
        /// 検索条件のクリア
        /// </summary>
        /// <returns></returns>
        private void ClearSearchCondition()
        {
            this.ListSearchCond = new TestCarCommonSearchModel();
        }
        #endregion

        #region 検索条件のクリア（入力値）
        /// <summary>
        /// 検索条件のクリア（入力値）
        /// </summary>
        private void ClearForm()
        {
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            var today = DateTime.Today;

            // 管理所在地
            this.EstablishmentComboBox.SelectedValue = SessionDto.Affiliation;

            // 管理票発行
            this.IssueNoRadioButton.Checked = true;

            // 発行年月日
            this.StartDateTimePicker.Value = null;
            this.EndDateTimePicker.Value = null;

            // 管理票NO
            this.ControlNoTextBox.Text = string.Empty;

            // 開発符号
            this.GeneralCodeComboBox.Text = string.Empty;
            this.GeneralCodeComboBox.SelectedValue = string.Empty;

            // 試作時期
            this.PrototypeTimingTextBox.Text = string.Empty;

            // 号車
            this.VehicleTextBox.Text = string.Empty;

            // 車体番号
            this.VehicleNotextBox.Text = string.Empty;
            
            // 仕向地
            this.ShimukechiTextBox.Text = string.Empty;

            // メーカー名
            this.MakerNameTextBox.Text = string.Empty;

            // 外製車名
            this.OuterCarNameTextBox.Text = string.Empty;

            // 廃却
            this.DisposalNoRadioButton.Checked = true;
        }
        #endregion

        #region 画面のイベント

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

        #region 検索ボタンクリック
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // データのセット
                this.SetDataList();

                // 1件の場合（検索ボタン押下時のみ）
                if (this.DataList?.Count == 1)
                {
                    // 試験車情報画面
                    new FormUtil(new ControlSheetIssueForm { TestCar = this.DataList.FirstOrDefault(), UserAuthority = this.UserAuthority }).SingleFormShow(this);
                }
            });
        }
        #endregion

        #region クリアボタンクリック
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // 検索条件の入力クリア
            this.ClearForm();
        }
        #endregion

        #region 削除ボタンクリック
        /// <summary>
        /// 削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // グリッドの選択確認
            if (this.ControlSheetMultiRow.CurrentCell == null)
            {
                Messenger.Warn(Resources.KKM00009);
                return;
            }

            // 対象の取得
            var row = this.ControlSheetMultiRow.Rows[this.ControlSheetMultiRow.CurrentCell.RowIndex];
            var id = Convert.ToInt32(row.Cells["データID"].Value);
            var target = this.DataList.Single(x => x.データID == id);

            // チェックデータの取得
            var checklist = this.GetDataList(new TestCarCommonSearchModel { データID = id });

            // 対象データなし
            if (!checklist.Any())
            {
                Messenger.Warn(Resources.KKM00021);
                return;
            }

            // 使用履歴情報の取得
            var usehistory = GetUseHistoryData(new TestCarUseHistorySearchModel { データID = id, 履歴NO = 1 });

            // 使用済み
            if (usehistory.Any())
            {
                Messenger.Warn(Resources.TCM03016);
                return;
            }

            // 削除の確認
            if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
            {
                return;
            }
            
            // 削除処理
            FormControlUtil.FormWait(this, () =>
            {
                // 削除処理
                if (this.DeleteDataList(id) != true)
                {
                    return;
                }

                // 削除完了メッセージ
                Messenger.Info(Resources.KKM00003);

                // 取得リストの再バインド
                this.SetDataList(true);
            });
        }
        #endregion

        #region 試験車新規作成ボタンクリック
        /// <summary>
        /// 試験車新規作成ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetIssueButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 試験車情報画面
                new FormUtil(new ControlSheetIssueForm { UserAuthority = this.UserAuthority, Reload = () => this.SetDataList() }).SingleFormShow(this);
            });
        }
        #endregion

        #region 試験車使用履歴ボタンクリック
        /// <summary>
        /// 試験車使用履歴ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarUseHistoryButton_Click(object sender, EventArgs e)
        {
            // グリッドの選択確認
            if (this.ControlSheetMultiRow.CurrentCell == null)
            {
                Messenger.Warn(Resources.KKM00009);
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                var row = this.ControlSheetMultiRow.Rows[this.ControlSheetMultiRow.CurrentCell.RowIndex];
                var id = Convert.ToInt32(row.Cells["データID"].Value);
                var target = this.DataList.Single(x => x.データID == id);

                // 試験車使用履歴画面
                new FormUtil(new TestCarHistoryForm { TestCar = target, UserAuthority = this.UserAuthority }).SingleFormShow(this);
            });
        }
        #endregion

        #region 試験車インポートボタンクリック
        /// <summary>
        /// 試験車インポートボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 試験車インポート画面
                new ControlSheetImportForm() { UserAuthority = this.UserAuthority }.Show();
            });
        }
        #endregion

        #region ダウンロードボタンクリック
        /// <summary>
        /// ダウンロードボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                new MultiRowUtil(this.ControlSheetMultiRow).Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
            });
        }
        #endregion

        #endregion

        #region グリッドのイベント

        #region グリッドのダブルクリック
        /// <summary>
        /// グリッドのダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                // 試験車情報画面
                new FormUtil(new ControlSheetIssueForm { TestCar = GetTestCarByMultiRow(e.RowIndex), UserAuthority = this.UserAuthority }).SingleFormShow(this);
            });
        }
        #endregion

        #endregion

        #region データの操作
        /// <summary>
        /// 一覧データの取得
        /// </summary>
        private List<TestCarCommonModel> GetDataList(TestCarCommonSearchModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<TestCarCommonSearchModel, TestCarCommonModel>(ControllerType.ControlSheetTestCar, cond);

            var list = new List<TestCarCommonModel>();

            // レスポンスのセット
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            return list;
        }
        /// <summary>
        /// データ（使用履歴情報）取得処理
        /// </summary>
        private List<TestCarUseHistoryModel> GetUseHistoryData(TestCarUseHistorySearchModel cond)
        {
            //Get実行
            var res = HttpUtil.GetResponse<TestCarUseHistorySearchModel, TestCarUseHistoryModel>
                (ControllerType.TestCarUseHistory, cond);

            return (res.Results).ToList();
        }

        /// <summary>
        /// 一覧データの削除
        /// </summary>
        private bool DeleteDataList(int id)
        {
            return HttpUtil.DeleteResponse<TestCarCommonModel>
                (ControllerType.ControlSheetTestCar, new TestCarCommonModel() {　データID = id }).Status.Equals(Const.StatusSuccess);
        }

        #endregion

        #region マスタデータの取得・検索

        #region 所属（管理所在地）検索
        /// <summary>
        /// 所属（管理所在地）検索
        /// </summary>
        private List<CommonMasterModel> GetAffiliationList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.Affiliation, null)?.Results?.ToList();
        }
        #endregion

        #region 開発符号情報（管理票）検索
        /// <summary>
        /// 開発符号情報（管理票）検索
        /// </summary>
        private List<CommonMasterModel> GetGeneralCodeList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.GeneralCodeInfo, null)?.Results?.ToList();
        }
        #endregion

        #endregion

        #region 試験車情報を一覧から取得
        /// <summary>
        /// 試験車情報を一覧から取得
        /// </summary>
        /// <param name="index">行番号</param>
        /// <returns></returns>
        private TestCarCommonModel GetTestCarByMultiRow(int index)
        {
            return this.ControlSheetMultiRow.Rows[index].DataBoundItem as TestCarCommonModel;
        }
        #endregion

        #region 管理所在地変更時に検索実行
        /// <summary>
        /// 管理所在地変更時に検索実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EstablishmentComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //バインド中なら終了
            if (this.IsBind == true)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                // データのセット
                this.SetDataList();

                // 1件の場合（検索ボタン押下時のみ）
                if (this.DataList?.Count == 1)
                {
                    // 試験車情報画面
                    new FormUtil(new ControlSheetIssueForm { TestCar = this.DataList.FirstOrDefault(), UserAuthority = this.UserAuthority }).SingleFormShow(this);
                }
            });
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
            // 表示設定画面表示
            base.ShowDisplayForm(this.CustomTemplate);
        }

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
