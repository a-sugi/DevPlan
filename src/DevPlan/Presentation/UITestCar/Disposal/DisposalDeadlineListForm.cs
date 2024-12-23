using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

namespace DevPlan.Presentation.UITestCar.Disposal
{
    /// <summary>
    /// 廃却（各画面共通）
    /// </summary>
    public partial class DisposalDeadlineListForm : BaseTestCarForm
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
        /// 検索条件の高さ
        /// </summary>
        private int CondHeight = 60;

        private IEnumerable<TestCarCommonModel> ItemList { get; set; }
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "廃却期限リスト"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DisposalDeadlineListForm()
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
        private void DisposalDeadlineListForm_Load(object sender, EventArgs e)
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

                    // 検索条件の初期化
                    this.InitSearchItems();

                    // MultiRowの初期化
                    this.InitMultiRow();

                    // データのセット
                    this.SetDataList();
                }
                finally
                {
                    //バインド可否
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
            var isExport = UserAuthority.EXPORT_FLG == '1';

            // 管理所在地
            FormControlUtil.SetComboBoxItem(this.EstablishmentComboBox, this.GetAffiliationList(), false);

            // 申請状況
            this.ApplicationComboBox.ValueMember = "CODE";
            this.ApplicationComboBox.DisplayMember = "NAME";
            var applicationlist = new List<ComboBoxDto>()
            {
                new ComboBoxDto { CODE = "1", NAME = "未申請" },
                new ComboBoxDto { CODE = "2", NAME = "申請済" },
                new ComboBoxDto { CODE = null, NAME = "全て" }
            };
            FormControlUtil.SetComboBoxItem(this.ApplicationComboBox, applicationlist, false);

            // Excel出力ボタン
            this.DownloadButton.Visible = isExport;

            // 初期表示フォーカス
            this.ActiveControl = this.EstablishmentComboBox;
        }
        #endregion

        #region 所属（管理所在地）検索
        /// <summary>
        /// 所属（管理所在地）検索
        /// </summary>
        private List<CommonMasterModel> GetAffiliationList()
        {
            // Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>(ControllerType.Affiliation, null)?.Results?.ToList();
        }
        #endregion

        #region MultiRowの初期化
        /// <summary>
        /// MultiRowの初期化
        /// </summary>
        private void InitMultiRow()
        {
            var template = new DisposalDeadlineListMultiRowTemplate();

            // カスタムテンプレート適用
            this.CustomTemplate.RowCountLabel = this.RowCountLabel;
            this.CustomTemplate.MultiRow = this.DisposalDeadlineMultiRow;
            this.CustomTemplate.ConfigDispLayList = base.GetUserDisplayConfiguration(template);
            this.DisposalDeadlineMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(template);

            // データーソース
            this.DisposalDeadlineMultiRow.DataSource = this.DataSource;
        }
        #endregion

        #region 一覧の設定
        /// <summary>
        /// 一覧の設定
        /// </summary>
        /// <param name="isKeepScroll"></param>
        private void SetDataList(bool isKeepScroll = false)
        {
            // 検索条件チェック
            if (!this.IsSearchConditionCheck()) return;

            // 描画停止
            this.DisposalDeadlineMultiRow.SuspendLayout();

            // データの取得
            this.ItemList = this.GetItemList();

            // バインドフラグ
            this.IsBind = true;

            // データバインド
            this.CustomTemplate.SetDataSource(this.ItemList, this.DataSource);

            // バインドフラグ
            this.IsBind = false;

            // 描画再開
            this.DisposalDeadlineMultiRow.ResumeLayout();

            // メッセージの設定
            this.SetMessageLabel(this.ItemList == null || !this.ItemList.Any() ? Resources.KKM00005 : string.Empty);

            if (isKeepScroll)
            {
                return;
            }

            // スクロールの初期化
            this.DisposalDeadlineMultiRow.FirstDisplayedLocation = new Point(0, 0);

            // 一覧を未選択状態に設定
            this.DisposalDeadlineMultiRow.CurrentCell = null;
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

        #region 検索ボタンクリック
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            // データのセット
            FormControlUtil.FormWait(this, () => this.SetDataList());
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
                new MultiRowUtil(this.DisposalDeadlineMultiRow).Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
            });
        }
        #endregion

        #region グリッドセルダブルクリック
        /// <summary>
        /// グリッドセルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisposalDeadlineMultiRow_CellDoubleClick(object sender, CellEventArgs e)
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

        #region 検索条件のチェック
        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchConditionCheck()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            var start = this.FromNullableDateTimePicker.SelectedDate;
            var end = this.ToNullableDateTimePicker.SelectedDate;

            // 期間の大小チェック
            map[this.FromNullableDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                // 期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー
                if (start != null && end != null && start > end)
                {
                    // エラーメッセージ
                    errMsg = Resources.KKM00018;

                    // 背景色を変更
                    this.FromNullableDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.ToNullableDateTimePicker.BackColor = Const.ErrorBackColor;
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

        #region データの取得
        /// <summary>
        /// データの取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<TestCarCommonModel> GetItemList()
        {
            var list = new List<TestCarCommonModel>();

            // 検索条件
            var cond = new TestCarCommonSearchModel
            {
                // 管理所在地
                ESTABLISHMENT = this.EstablishmentComboBox.SelectedValue == null ? null : this.EstablishmentComboBox.SelectedValue.ToString(),

                // 期間
                START_DATE = this.FromNullableDateTimePicker.SelectedDate,

                // 廃却期限リスト
                END_DATE = (Convert.ToDateTime(this.ToNullableDateTimePicker.SelectedDate)).AddMonths(1).AddDays(-1),
            
                // 申請状況
                APPLICATION_STATUS = this.ApplicationComboBox.SelectedValue == null ? null : this.ApplicationComboBox.SelectedValue.ToString(),
            };

            // APIで取得
            var res = HttpUtil.GetResponse<TestCarCommonSearchModel, TestCarCommonModel>(ControllerType.DisposalPeriod, cond);

            // レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            // 返却
            return list;
        }
        #endregion

        #region 試験車情報を一覧から取得
        /// <summary>
        /// 試験車情報を一覧から取得
        /// </summary>
        /// <param name="index">行番号</param>
        /// <returns></returns>
        private TestCarCommonModel GetTestCarByMultiRow(int index)
        {
            return this.DisposalDeadlineMultiRow.Rows[index].DataBoundItem as TestCarCommonModel;
        }
        #endregion

        #region メッセージ表示
        /// <summary>
        /// メッセージ表示
        /// </summary>
        /// <param name="message"></param>
        private void SetMessageLabel(string message)
        {
            this.MessageLabel.Text = message;
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
            // 検索条件初期化（再検索）
            FormControlUtil.FormWait(this, () => this.InitSearchItems());
        }
        #endregion

        #region 検索条件初期化
        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void InitSearchItems()
        {
            //管理所在地
            this.EstablishmentComboBox.SelectedValue = SessionDto.Affiliation;

            //申請状況
            this.ApplicationComboBox.SelectedIndex = 0;

            //期間(当月初～月末)
            this.FromNullableDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.ToNullableDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
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

            // データのセット
            FormControlUtil.FormWait(this, () => this.SetDataList());
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
