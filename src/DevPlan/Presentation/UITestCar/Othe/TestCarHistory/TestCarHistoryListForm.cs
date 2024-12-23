using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Util;
using DevPlan.Presentation.UC.MultiRow;

namespace DevPlan.Presentation.UITestCar.Othe.TestCarHistory
{
    /// <summary>
    /// 試験車使用履歴一覧
    /// </summary>
    public partial class TestCarHistoryListForm : BaseTestCarForm
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

        /// <summary>
        /// 検索条件
        /// </summary>
        private TestCarCommonSearchModel SearchCondition = null;
        #endregion

        #region 定義
        private const int CondHeight = 90;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "試験車使用履歴一覧"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCarHistoryListForm()
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
        private void TestCarHistoryListForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // バインド可否
                this.IsBind = true;

                // 権限
                this.UserAuthority = base.GetFunction(FunctionID.TestCarManagement);

                // 画面初期化
                this.InitForm();

                // MultiRow初期化
                this.InitMultiRow();

                // バインド可否
                this.IsBind = false;

                // 試験車一覧の設定
                this.SetTestCarList();
            });

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isExport = this.UserAuthority.EXPORT_FLG == '1';

            //管理所在地
            FormControlUtil.SetComboBoxItem(this.AffiliationComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.Affiliation).Results, false);
            this.AffiliationComboBox.SelectedValue = SessionDto.Affiliation;
            this.ActiveControl = this.AffiliationComboBox;

            //開発符号
            FormControlUtil.SetComboBoxItem(this.GeneralCodeComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.GeneralCodeInfo).Results);

            //ダウンロードボタン
            this.DownloadButton.Visible = isExport;
        }

        /// <summary>
        /// MultiRow初期化
        /// </summary>
        private void InitMultiRow()
        {
            var template = new TestCarHistoryListMultiRowTemplate();

            // カスタムテンプレート適用
            this.CustomTemplate.RowCountLabel = this.RowCountLabel;
            this.CustomTemplate.MultiRow = this.TestCarListMultiRow;
            this.CustomTemplate.ConfigDispLayList = base.GetUserDisplayConfiguration(template);
            this.TestCarListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(template);

            // データーソース
            this.TestCarListMultiRow.DataSource = this.DataSource;
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
            //検索条件表示設定
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
            if ( this.IsBind == true)
            {
                return;
            }

            // 試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
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
            // 試験車一覧の設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
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
            //管理所在地
            this.AffiliationComboBox.SelectedValue = SessionDto.Affiliation;

            //開発符号
            this.GeneralCodeComboBox.Text = "";

            //管理票NO
            this.ManagementNoTextBox.Text = "";

            //車体番号
            this.CarBodyNoTextBox.Text = "";

            //試作時期
            this.TrialProductionSeasonTextBox.Text = "";

            //号車
            this.CarTextBox.Text = "";

            //メーカー名
            this.MakerNameTextBox.Text = "";

            //外製車名
            this.OuterCarNameTextBox.Text = "";
        }
        #endregion

        #region 試験車一覧
        /// <summary>
        /// 試験車一覧セルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarListMultiRow_CellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            //無効な行の場合は終了
            if (e.RowIndex < 0)
            {
                return;
            }

            //試験車使用履歴画面表示
            this.ShowTestCarHistoryForm(this.TestCarListMultiRow.Rows[e.RowIndex].DataBoundItem as TestCarCommonModel);
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
                new MultiRowUtil(this.TestCarListMultiRow).Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
            });
        }
        #endregion

        #region 試験車一覧設定
        /// <summary>
        /// 試験車一覧設定
        /// </summary>
        /// <param name="isKeepScroll"></param>
        private void SetTestCarList(bool isKeepScroll = false)
        {
            // 検索条件のチェック
            if (!this.IsSearchConditionCheck()) return;

            // 検索条件のセット
            this.SetSearchCondition();

            // 描画停止
            this.TestCarListMultiRow.SuspendLayout();

            // データの取得
            var list = this.GetTestCarList(this.SearchCondition);

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

            // 1件かどうか
            if (this.TestCarListMultiRow.RowCount == 1)
            {
                // 試験車使用履歴画面表示
                this.ShowTestCarHistoryForm(list.First());
            }

            if (isKeepScroll)
            {
                return;
            }

            // スクロールの初期化
            this.TestCarListMultiRow.FirstDisplayedLocation = new Point(0, 0);

            // 一覧を未選択状態に設定
            this.TestCarListMultiRow.CurrentCell = null;
        }

        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchConditionCheck()
        {
            // 入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);

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

            this.SearchCondition = new TestCarCommonSearchModel
            {
                //管理所在地
                ESTABLISHMENT = getValue(this.AffiliationComboBox),

                //開発符号
                開発符号 = this.GeneralCodeComboBox.Text,

                //管理票NO
                管理票NO = this.ManagementNoTextBox.Text,

                //車体番号
                車体番号 = this.CarBodyNoTextBox.Text,

                //試作時期
                試作時期 = this.TrialProductionSeasonTextBox.Text,

                //号車
                号車 = this.CarTextBox.Text,

                //メーカー名
                メーカー名 = this.MakerNameTextBox.Text,

                //外製車名
                外製車名 = this.OuterCarNameTextBox.Text

            };
        }
        #endregion

        #region 試験車使用履歴画面表示
        /// <summary>
        /// 試験車使用履歴画面表示
        /// </summary>
        /// <param name="testCar">試験車</param>
        private void ShowTestCarHistoryForm(TestCarCommonModel testCar)
        {
            FormControlUtil.FormWait(this, () =>
            {
                new FormUtil(new TestCarHistoryForm { TestCar = testCar, UserAuthority = this.UserAuthority }).SingleFormShow(this);
            });
        }
        #endregion

        #region API
        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<TestCarCommonModel> GetTestCarList(TestCarCommonSearchModel cond)
        {
            //APIで取得
            var res = HttpUtil.PostResponse<TestCarCommonModel>(ControllerType.TestCarHistory, cond);

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
