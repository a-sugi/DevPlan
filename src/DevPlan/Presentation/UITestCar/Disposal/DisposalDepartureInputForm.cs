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
    public partial class DisposalDepartureInputForm : BaseTestCarForm
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

        private const string ChangeColumn = "変更";
        private const string OutCarChangeColumn = "車両搬出日変更";

        private const string ChangeValue = "1";

        /// <summary>
        /// 検索条件の高さ
        /// </summary>
        private int CondHeight = 90;

        private IEnumerable<TestCarCommonModel> ItemList { get; set; }
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "廃却車両搬出日入力"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DisposalDepartureInputForm()
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
        private void DisposalDepartureInputForm_Load(object sender, EventArgs e)
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
                    // バインド可否
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
            var isUpdate = UserAuthority.UPDATE_FLG == '1';
            var isExport = UserAuthority.EXPORT_FLG == '1';

            // 管理所在地
            FormControlUtil.SetComboBoxItem(this.EstablishmentComboBox, this.GetAffiliationList(), false);

            // MultiRow
            this.DisposalDepartureMultiRow.ReadOnly = !isUpdate;

            // 登録ボタン
            this.EntryButton.Visible = isUpdate;

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
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>(ControllerType.Affiliation, null)?.Results?.ToList();
        }
        #endregion

        #region MultiRowの初期化
        /// <summary>
        /// MultiRowの初期化
        /// </summary>
        private void InitMultiRow()
        {
            var template = new DisposalDepartureInputMultiRowTemplate();

            // カスタムテンプレート適用
            this.CustomTemplate.IsValidate = true;
            this.CustomTemplate.RowCountLabel = this.RowCountLabel;
            this.CustomTemplate.MultiRow = this.DisposalDepartureMultiRow;
            this.CustomTemplate.ConfigDispLayList = base.GetUserDisplayConfiguration(template);
            this.DisposalDepartureMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(template);
            
            // データーソース
            this.DisposalDepartureMultiRow.DataSource = this.DataSource;
        }
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

            // 描画停止
            this.DisposalDepartureMultiRow.SuspendLayout();

            // データの取得
            this.ItemList = this.GetItemList();

            // バインドフラグ
            this.IsBind = true;

            // データバインド
            this.CustomTemplate.SetDataSource(this.ItemList, this.DataSource);

            // バインドフラグ
            this.IsBind = false;

            // 描画再開
            this.DisposalDepartureMultiRow.ResumeLayout();

            // メッセージの設定
            this.SetMessageLabel(this.ItemList == null || !this.ItemList.Any() ? Resources.KKM00005 : string.Empty);

            if (isKeepScroll)
            {
                return;
            }

            // スクロールの初期化
            this.DisposalDepartureMultiRow.FirstDisplayedLocation = new Point(0, 0);

            // 一覧を未選択状態に設定
            this.DisposalDepartureMultiRow.CurrentCell = null;
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
                new MultiRowUtil(this.DisposalDepartureMultiRow).Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
            });
        }
        #endregion

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 登録処理
                if (this.SaveItem(false) == true)
                {
                    // 登録完了メッセージ
                    Messenger.Info(Resources.KKM00002);

                    // データのセット
                    FormControlUtil.FormWait(this, () => this.SetDataList(true));
                }
            });
        }
        #endregion

        #region グリッドセルダブルクリック
        /// <summary>
        /// グリッドセルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarDataGridView_CellDoubleClick(object sender, CellEventArgs e)
        {
            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = this.DisposalDepartureMultiRow.Rows[e.RowIndex];
            var cell = row.Cells[e.CellIndex];

            // 編集カラムの場合は終了
            if (cell.ReadOnly != true)
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

        #region セル値変更
        /// <summary>
        /// セル値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarDataGridView_CellValueChanged(object sender, CellEventArgs e)
        {
            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = this.DisposalDepartureMultiRow.Rows[e.RowIndex];
            var cell = row.Cells[e.CellIndex];

            // 読取カラムの場合は終了
            if (cell.ReadOnly == true)
            {
                return;
            }

            // 変更フラグを設定
            row.Cells[ChangeColumn].Value = ChangeValue;

            // 車両搬出日が
            if (cell.Name == "車両搬出日" && cell.Value != null)
            {
                // 車両搬出日変更フラグを設定
                row.Cells[OutCarChangeColumn].Value = ChangeValue;
            }
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

            //検索条件
            var cond = new TestCarCommonSearchModel
            {
                // 管理所在地
                ESTABLISHMENT = this.EstablishmentComboBox.SelectedValue == null ? null : this.EstablishmentComboBox.SelectedValue.ToString(),

                // 期間
                START_DATE = this.FromNullableDateTimePicker.SelectedDate,

                // 
                END_DATE = this.ToNullableDateTimePicker.SelectedDate,

                // 管理票NO
                管理票NO = this.ControlSheetTextBox.Text == "" ? null : this.ControlSheetTextBox.Text,

                // 車体番号
                車体番号 = this.CarNoTextBox.Text == "" ? null : this.CarNoTextBox.Text,

                // 固定資産NO
                固定資産NO = this.FixedAssetsTextBox.Text == "" ? null : this.FixedAssetsTextBox.Text,
            };

            // APIで取得
            var res = HttpUtil.GetResponse<TestCarCommonSearchModel, TestCarCommonModel>(ControllerType.DisposalCarryout, cond);

            // レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            // 返却
            return list;
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="isMessage"></param>
        /// <returns></returns>
        private bool SaveItem(bool isMessage)
        {
            var isUpdate = UserAuthority.UPDATE_FLG == '1';

            // 更新権限がない場合は終了
            if (isUpdate == false)
            {
                return false;
            }

            var list = new List<TestCarCommonModel>();
            var parkingList = new List<ParkingUseModel>();

            for (int i = 0; i < this.DisposalDepartureMultiRow.Rows.Count; i++)
            {
                var row = DisposalDepartureMultiRow.Rows[i];

                if (row.Cells[ChangeColumn].Value != null && row.Cells[ChangeColumn].Value.ToString() == ChangeValue)
                {
                    // 更新用リストに追加
                    list.Add(GetTestCarByMultiRow(i));
                }

                if (row.Cells[OutCarChangeColumn].Value != null && row.Cells[OutCarChangeColumn].Value.ToString() == ChangeValue)
                {
                    // 車両搬出日が入力されている場合
                    if (row.Cells["車両搬出日"].Value != null)
                    {
                        // 更新用リストに追加
                        parkingList.Add(new ParkingUseModel { データID = Convert.ToInt32(row["データID"].Value) });
                    }
                }
            }

            // 対象データがない場合
            if (list.Any() == false)
            {
                // 登録確認が必要でない場合
                if (isMessage == false)
                {
                    Messenger.Warn(Resources.TCM03008);
                }

                return false;
            }

            // 登録確認が必要な場合
            if (isMessage == true)
            {
                // 登録確認ダイアログ
                if (Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes)
                {
                    return false;
                }
            }

            // 駐車場データの削除
            if (this.DeleteParkingUse(parkingList) == false)
            {
                return false;
            }

            // 車両搬出日テータの更新
            return this.PutDisposalCarryout(list);
        }
        #endregion

        #region データの操作
        /// <summary>
        /// 車両搬出日データの更新
        /// </summary>
        /// <param name="list">更新リスト</param>
        /// <returns></returns>
        private bool PutDisposalCarryout(List<TestCarCommonModel> list)
        {
            // 廃却データ更新
            var res = HttpUtil.PutResponse<TestCarCommonModel>(ControllerType.DisposalCarryout, list);

            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 駐車場データの削除
        /// </summary>
        /// <param name="list">更新リスト</param>
        /// <returns></returns>
        private bool DeleteParkingUse(List<ParkingUseModel> list)
        {
            // 駐車場データ更新
            var resParking = HttpUtil.DeleteResponse<ParkingUseModel>(ControllerType.ParkingUse, list);

            if (resParking == null || resParking.Status != Const.StatusSuccess)
            {
                return false;
            }

            return true;
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
            return this.DisposalDepartureMultiRow.Rows[index].DataBoundItem as TestCarCommonModel;
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

        #region 画面終了時処理
        /// <summary>
        /// 画面終了時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisposalForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 登録処理
                if (this.SaveItem(true) == true)
                {
                    // 登録完了メッセージ
                    Messenger.Info(Resources.KKM00002);
                }
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
            // 管理所在地
            this.EstablishmentComboBox.SelectedValue = SessionDto.Affiliation;

            // 期間
            this.FromNullableDateTimePicker.Value = null;
            this.ToNullableDateTimePicker.Value = null;

            // 管理票NO
            this.ControlSheetTextBox.Text = string.Empty;

            // 固定資産NO
            this.FixedAssetsTextBox.Text = string.Empty;

            // 車体番号
            this.CarNoTextBox.Text = string.Empty;
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
