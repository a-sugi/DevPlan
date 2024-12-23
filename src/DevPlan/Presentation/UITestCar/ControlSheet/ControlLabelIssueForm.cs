using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UITestCar.ControlSheet.Label;
using DevPlan.UICommon.Util;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;

namespace DevPlan.Presentation.UITestCar.ControlSheet
{
    /// <summary>
    /// ラベル印刷
    /// </summary>
    public partial class ControlLabelIssueForm : BaseTestCarForm
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

        private const int CondHeight = 150;

        private IEnumerable<TestCarCommonModel> ItemList { get; set; }
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "ラベル印刷"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>試験車情報</summary>
        public TestCarCommonModel TestCar { get; set; } = new TestCarCommonModel();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControlLabelIssueForm()
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
        private void ControlLabelIssueForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //バインド可否
                this.IsBind = true;

                // 権限の取得
                this.UserAuthority = base.GetFunction(FunctionID.TestCarManagement);

                // 画面の初期化
                this.InitForm();

                // グリッドの初期化
                this.InitMultiRow();

                // グリッドのセット
                this.SetDataList();

                //バインド可否
                this.IsBind = false;
            });
        }
        #endregion

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            //検索条件初期化
            this.InitSearchItems();

            //権限によりボタン表示変更
            //出力権限のないログインユーザの場合、ダウンロードボタン、ラベル印刷ボタンは表示しない。
            if (UserAuthority.EXPORT_FLG != '1')
            {
                this.DownloadButton.Visible = false;
                this.ProgressListButton.Visible = false;
            }

            //初期表示フォーカス
            this.ActiveControl = EstablishmentComboBox;
        }
        #endregion

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

        #region MultiRowの初期化
        /// <summary>
        /// MultiRowの初期化
        /// </summary>
        private void InitMultiRow()
        {
            var template = new ControlLabelIssueMultiRowTemplate();

            // カスタムテンプレート適用
            this.CustomTemplate.RowCountLabel = this.RowCountLabel;
            this.CustomTemplate.MultiRow = this.ControlLabelMultiRow;
            this.CustomTemplate.ConfigDispLayList = base.GetUserDisplayConfiguration(template);

            this.ControlLabelMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(template);
            
            // 選択列のフィルターアイテム設定
            var headerCell = this.ControlLabelMultiRow.Template.ColumnHeaders[0].Cells["columnHeaderCell1"] as ColumnHeaderCell;
            headerCell.DropDownContextMenuStrip.Items.RemoveAt(headerCell.DropDownContextMenuStrip.Items.Count - 1);
            headerCell.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });

            // データーソース
            this.ControlLabelMultiRow.DataSource = this.DataSource;
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

            // チェックボックス初期化
            this.AllCheckBox.Checked = false;

            // 描画停止
            this.ControlLabelMultiRow.SuspendLayout();

            // データの取得
            this.ItemList = this.GetItemList();

            // バインドフラグ
            this.IsBind = true;

            // データバインド
            this.CustomTemplate.SetDataSource(this.ItemList, this.DataSource);

            // バインドフラグ
            this.IsBind = false;

            // 描画再開
            this.ControlLabelMultiRow.ResumeLayout();

            // メッセージラベルの設定
            this.SetMessageLabel(this.ItemList == null || !this.ItemList.Any() ? Resources.KKM00005 : string.Empty);

            if (isKeepScroll)
            {
                return;
            }

            // スクロールの初期化
            this.ControlLabelMultiRow.FirstDisplayedLocation = new Point(0, 0);

            // 一覧を未選択状態に設定
            this.ControlLabelMultiRow.CurrentCell = null;
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

            // 期間の大小チェック
            map[this.FromNullableDateTimePicker] = (c, name) =>
            {
                var start = this.FromNullableDateTimePicker.SelectedDate;
                var end = this.ToNullableDateTimePicker.SelectedDate;

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
            // データのセット
            FormControlUtil.FormWait(this, () =>　this.SetDataList());
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
                new MultiRowUtil(this.ControlLabelMultiRow).Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
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
            //検索条件初期化
            this.InitSearchItems();
        }
        #endregion

        #region ラベル印刷ボタンクリック
        /// <summary>
        /// ラベル印刷ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListButton_Click(object sender, EventArgs e)
        {
            // ラベル印刷
            FormControlUtil.FormWait(this, () => this.PrintLabel());
        }
        #endregion

        #region ラベル印刷
        /// <summary>
        /// ラベル印刷
        /// </summary>
        private void PrintLabel()
        {
            //選択行があるかチェック
            var selectFlag = false;
            for (var i = 0; i < this.ControlLabelMultiRow.Rows.Count; i++)
            {
                //チェックボックスONの行をラベル印刷する
                if (Convert.ToBoolean(this.ControlLabelMultiRow.Rows[i].Cells["TargetCheckBoxColumn"].Value))
                {
                    selectFlag = true;
                    break;
                }
            }

            if (selectFlag == false)
            {
                //選択項目がない場合メッセージ表示
                Messenger.Info(Resources.TCM03029);
                return;
            }

            try
            {
                // ラベル印刷
                var label = new LabelPrint();

                //全行検索
                for (var i = 0; i < this.ControlLabelMultiRow.Rows.Count; i++)
                {
                    //チェックボックスONの行をラベル印刷する
                    if (Convert.ToBoolean(this.ControlLabelMultiRow.Rows[i].Cells["TargetCheckBoxColumn"].Value))
                    {
                        // ラベル印刷実行
                        label.Print(GetTestCarByDataGridView(i));
                    }
                }
            }
            catch (Exception ex)
            {
                //エラーメッセージ表示
                Messenger.Error(Resources.TCM03023, ex);
                return;
            }

            //登録処理
            if (this.SaveItem())
            {
                //再描画
                this.SetDataList();
            }
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <returns></returns>
        private bool SaveItem()
        {
            //データ更新
            return this.UpdateItem();
        }
        #endregion

        #region データ更新
        /// <summary>
        /// データ更新
        /// </summary>
        /// <returns></returns>
        private bool UpdateItem()
        {
            var list = new List<TestCarCommonModel>();

            for (var i = 0; i < this.ControlLabelMultiRow.Rows.Count; i++)
            {
                //チェックボックスONの行を更新する
                if (Convert.ToBoolean(this.ControlLabelMultiRow.Rows[i].Cells["TargetCheckBoxColumn"].Value))
                {
                    //行情報取得
                    TestCarCommonModel data = GetTestCarByDataGridView(i);

                    //更新用リストに追加
                    data.管理ラベル発行有無 = 1;
                    list.Add(data);
                }
            }

            if (list.Count == 0)
            {
                //選択行なし
                Messenger.Warn(Resources.KKM00009);
                return false;
            }

            //返却
            return this.UpdateItem(list);
        }
        #endregion

        #region データ更新
        /// <summary>
        /// データ更新
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private bool UpdateItem(List<TestCarCommonModel> cond)
        {
            var res = HttpUtil.PutResponse<TestCarCommonModel>(ControllerType.ControlSheetLabelPrint, cond);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 試験車情報を一覧から取得
        /// <summary>
        /// 試験車情報を一覧から取得
        /// </summary>
        /// <param name="index">行番号</param>
        /// <returns></returns>
        private TestCarCommonModel GetTestCarByDataGridView(int index)
        {
            return this.ControlLabelMultiRow.Rows[index].DataBoundItem as TestCarCommonModel;
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

            FormControlUtil.FormWait(this, () =>
            {
                // 試験車情報画面
                new FormUtil(new ControlSheetIssueForm { TestCar = GetTestCarByDataGridView(e.RowIndex), UserAuthority = this.UserAuthority }).SingleFormShow(this);
            });
        }
        #endregion

        #region 全選択チェックボックス描画
        /// <summary>
        /// 全選択チェックボックス描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarDataGridView_CellPainting(object sender, CellPaintingEventArgs e)
        {
            if (this.IsBind)
            {
                return;
            }

            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if (e.CellIndex == 0 && e.RowIndex == -1)
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point point = new Point((bmp.Width - AllCheckBox.Width) / 2, (bmp.Height - AllCheckBox.Height) / 2);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    AllCheckBox.DrawToBitmap(bmp, new Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    point = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds);
                    e.Graphics.DrawImage(bmp, point);
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region 全選択チェックボックス選択
        /// <summary>
        /// 全選択チェックボックス選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarDataGridView_CellClick(object sender, CellEventArgs e)
        {
            // 全選択チェックボックスクリックの場合
            if (e.CellIndex == 0 && e.RowIndex == -1)
            {
                // 選択チェックボックスの表示を更新する
                this.AllCheckBox.Checked = !this.AllCheckBox.Checked;
            }
        }
        #endregion

        #region 全選択チェックボックス操作
        /// <summary>
        /// 全選択チェックボックス操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // バインドフラグ
                this.IsBind = true;

                // CheckBoxCell 不具合対応
                this.ControlLabelMultiRow.CurrentCell = null;

                // 全ての行の選択を設定
                foreach (var row in this.ControlLabelMultiRow.Rows)
                {
                    //Append Start 2020/01/27 杉浦
                    // 非表示行は未処理
                    if (!row.Visible) continue;
                    //Append End 2020/01/27 杉浦
                    row.Cells["TargetCheckBoxColumn"].Value = this.AllCheckBox.Checked;
                }

                // バインドフラグ
                this.IsBind = false;
            });
        }
        #endregion

        #endregion

        #region データの取得
        /// <summary>
        /// データの取得
        /// </summary>
        /// <returns></returns>
        private List<TestCarCommonModel> GetItemList()
        {
            //廃却
            bool? disposalFlag = null;
            if (this.DisposalOffRadioButton.Checked)
            {
                //未(false)
                disposalFlag = Convert.ToBoolean(this.DisposalOffRadioButton.Tag.ToString());
            }
            else if (this.DisposalAllRadioButton.Checked)
            {
                //全て(true)
                disposalFlag = Convert.ToBoolean(this.DisposalAllRadioButton.Tag.ToString());
            }

            //資産
            var asset = "";
            if (this.AssetOutRadioButton.Checked)
            {
                //資産外
                asset = this.AssetOutRadioButton.Tag.ToString();
            }
            else if (this.AssetRadioButton.Checked)
            {
                //固定資産
                asset = this.AssetRadioButton.Tag.ToString();
            }
            else if (this.LeaseRadioButton.Checked)
            {
                //リース
                asset = this.LeaseRadioButton.Tag.ToString();
            }

            var cond = new TestCarCommonSearchModel
            {
                //管理所在地
                ESTABLISHMENT = this.EstablishmentComboBox.SelectedValue == null ? null : this.EstablishmentComboBox.SelectedValue.ToString(),

                //期間
                START_DATE = this.FromNullableDateTimePicker.SelectedDate,
                END_DATE = this.ToNullableDateTimePicker.SelectedDate,

                //管理票NO
                管理票NO = this.ControlSheetTextBox.Text,

                //開発符号
                開発符号 = this.GeneralCodeComboBox.Text,

                //試作時期
                試作時期 = this.TrialTextBox.Text,

                //車体番号
                車体番号 = this.CarNoTextBox.Text,

                //号車
                号車 = this.CarTextBox.Text,

                //仕向地
                仕向地 = this.LocationTextBox.Text,

                //メーカー名
                メーカー名 = this.MakerTextBox.Text,

                //外製車名
                外製車名 = this.OuterCarTextBox.Text,

                //廃却
                廃却フラグ = disposalFlag,

                //種別（固定資産, 資産外, リース）
                種別 = asset,
            };

            //Get実行
            var res = HttpUtil.GetResponse<TestCarCommonSearchModel, TestCarCommonModel>
                        (ControllerType.ControlSheetLabelPrint, cond);

            return (res.Results).ToList();
        }
        #endregion

        #region 検索条件初期化
        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void InitSearchItems()
        {
            //管理所在地
            FormControlUtil.SetComboBoxItem(this.EstablishmentComboBox, this.GetAffiliationList(), false);
            this.EstablishmentComboBox.SelectedValue = SessionDto.Affiliation;

            //期間FROM
            this.FromNullableDateTimePicker.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1);

            //期間TO
            this.ToNullableDateTimePicker.Value = null;

            //管理表NO
            this.ControlSheetTextBox.Text = null;

            //開発符号
            FormControlUtil.SetComboBoxItem(this.GeneralCodeComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.GeneralCodeInfo).Results);

            //試作時期
            this.TrialTextBox.Text = null;

            //車体番号
            this.CarNoTextBox.Text = null;

            //号車
            this.CarTextBox.Text = null;

            //仕向地
            this.LocationTextBox.Text = null;

            //メーカー名
            this.MakerTextBox.Text = null;

            //外製車名
            this.OuterCarTextBox.Text = null;

            //廃却
            this.DisposalOffRadioButton.Select();

            //資産
            this.AssetOutRadioButton.Select();
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
