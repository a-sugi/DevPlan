using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UC.MultiRow;

using GrapeCity.Win.MultiRow;

namespace DevPlan.Presentation.UITestCar.ControlSheet
{
    /// <summary>
    /// 試験車インポート
    /// </summary>
    public partial class ControlSheetImportForm : BaseTestCarForm
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
        private bool IsBind { get; set; } = false;

        /// <summary>検索条件</summary>
        private ProductionCarSearchModel ListSearchCond { get; set; }

        /// <summary>取得リスト</summary>
        private List<ProductionCarCommonModel> DataList { get; set; } = new List<ProductionCarCommonModel>();

        /// <summary>インポートリスト</summary>
        private List<ProductionCarCommonModel> ImportList { get; set; } = new List<ProductionCarCommonModel>();

        /// <summary>反映リスト</summary>
        private List<ProductionCarCommonModel> EntryList { get; set; } = new List<ProductionCarCommonModel>();

        /// <summary>Excelファイル名</summary>
        private string ExcelFileName = "試作車登録シート_{0:yyyyMMddHHmmss}";

        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "試験車インポート"; } }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region 定義
        /// <summary>検索条件パネルの高さ</summary>
        private const int CondHeight = 90;

        /// <summary>Excelフィルター</summary>
        private const string ExcelFilter = "Excel ブック(*.xlsx;*.xls)|*.xlsx;*.xls";
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControlSheetImportForm()
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
        private void ControlSheetImportForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // バインドフラグ
                this.IsBind = true;

                try
                {
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

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';

            var today = DateTime.Today;

            // 反映日
            this.StartDateTimePicker.Value = new DateTime(today.AddMonths(-1).Year, today.AddMonths(-1).Month, 1);
            this.EndDateTimePicker.Value = null;

            // 開発符号
            FormControlUtil.SetComboBoxItem(this.GeneralCodeComboBox, GetGeneralCodeList());
            this.GeneralCodeComboBox.SelectedValue = string.Empty;

            // 作成日
            this.ImportStartDateTimePicker.Value = new DateTime(today.AddMonths(-1).Year, today.AddMonths(-1).Month, 1);
            this.ImportEndDateTimePicker.Value = null;

            // 更新権限なし
            if (!isUpdate)
            {
                // 削除ボタン
                this.DeleteButton.Visible = isUpdate;

                // インポートボタン
                this.ImportButton.Visible = isUpdate;
            }

            // 出力権限なし
            if (!isExport)
            {
                // エクスポートボタン
                this.ExportButton.Visible = isExport;
            }

            // 管理権限なし
            if (!isManagement)
            {
                // 反映ボタン
                this.EntryButton.Visible = isManagement;
            }
        }

        /// <summary>
        /// MultiRow初期化
        /// </summary>
        private void InitMultiRow()
        {
            var template = new ControlSheetImportMultiRowTemplate();

            // カスタムテンプレート適用
            this.CustomTemplate.RowCountLabel = this.RowCountLabel;
            this.CustomTemplate.MultiRow = this.ProductionCarMultiRow;
            this.CustomTemplate.ConfigDispLayList = base.GetUserDisplayConfiguration(template);
            this.ProductionCarMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(template);

            // 選択列のフィルターアイテム設定
            var headerCell = this.ProductionCarMultiRow.Template.ColumnHeaders[0].Cells["columnHeaderCell1"] as ColumnHeaderCell;
            headerCell.DropDownContextMenuStrip.Items.RemoveAt(headerCell.DropDownContextMenuStrip.Items.Count - 1);
            headerCell.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });

            // データーソース
            this.ProductionCarMultiRow.DataSource = this.DataSource;
        }
        #endregion

        #region 画面の表示
        /// <summary>
        /// 画面の表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetImportForm_Shown(object sender, EventArgs e)
        {
            // 初期表示フォーカス
            this.ActiveControl = this.GeneralCodeComboBox;
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

            // チェックボックスの初期化
            this.CheckBoxAll.Checked = false;

            // 検索条件のセット
            this.SetSearchCondition();

            // 描画停止
            this.ProductionCarMultiRow.SuspendLayout();

            // データの取得
            this.DataList = this.GetDataList(this.ListSearchCond);

            // バインドフラグ
            this.IsBind = true;

            // データバインド
            this.CustomTemplate.SetDataSource(this.DataList, this.DataSource);

            // レイアウトの設定
            this.SetLayout();

            // バインドフラグ
            this.IsBind = false;

            // 描画再開
            this.ProductionCarMultiRow.ResumeLayout();

            // 設計チェックが取得できたかどうか
            this.MessageLabel.Text = this.DataList?.Any() == true ? "" : Resources.KKM00005;

            if (isKeepScroll)
            {
                return;
            }

            // スクロールの初期化
            this.ProductionCarMultiRow.FirstDisplayedLocation = new Point(0, 0);

            // 一覧を未選択状態に設定
            this.ProductionCarMultiRow.CurrentCell = null;
        }

        /// <summary>
        /// レイアウトの設定
        /// </summary>
        private void SetLayout()
        {
            var isImportList = this.ImportList.Any();
            var isEntryList = this.EntryList.Any();

            foreach (var row in this.ProductionCarMultiRow.Rows)
            {
                // インポート成功
                if (isImportList && this.ImportList.Any(x => x.GENERAL_CODE == row.Cells["開発符号"].Value?.ToString() && x.PROTOTYPE_PERIOD == row.Cells["試作時期"].Value?.ToString() && x.VEHICLE == row.Cells["号車"].Value?.ToString()))
                {
                    foreach (var cell in row.Cells.Where(x => x.Visible == true && x.Name != "RowHeader"))
                    {
                        cell.Style.BackColor = Color.LightBlue;
                    }

                    continue;
                }

                // 反映成功
                if (isEntryList && this.EntryList.Any(x => x.GENERAL_CODE == row.Cells["開発符号"].Value?.ToString() && x.PROTOTYPE_PERIOD == row.Cells["試作時期"].Value?.ToString() && x.VEHICLE == row.Cells["号車"].Value?.ToString() && !string.IsNullOrWhiteSpace(row.Cells["反映日"].Value?.ToString())))
                {
                    foreach (var cell in row.Cells.Where(x => x.Visible == true && x.Name != "RowHeader"))
                    {
                        cell.Style.BackColor = Color.LightBlue;
                    }

                    continue;
                }

                // 反映済み
                if (!string.IsNullOrWhiteSpace(row.Cells["反映日"]?.Value?.ToString()))
                {
                    foreach (var cell in row.Cells.Where(x => x.Visible == true && x.Name != "RowHeader"))
                    {
                        cell.Style.BackColor = Color.WhiteSmoke;
                    }
                }
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

            // 期間（反映日）の大小チェック
            map[this.StartDateTimePicker] = (c, name) =>
            {
                var start = this.StartDateTimePicker.SelectedDate;
                var end = this.EndDateTimePicker.SelectedDate;

                var errMsg = "";

                // 済みがチェックされていない場合は未チェック
                if (!this.CompleteYesRadioButton.Checked)
                {
                    return errMsg;
                }

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

            // 期間（作成日）の大小チェック
            map[this.ImportStartDateTimePicker] = (c, name) =>
            {
                var start = this.ImportStartDateTimePicker.SelectedDate;
                var end = this.ImportEndDateTimePicker.SelectedDate;

                var errMsg = "";

                // 期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー
                if (start != null && end != null && start > end)
                {
                    // エラーメッセージ
                    errMsg = Resources.KKM00018;

                    // 背景色を変更
                    this.ImportStartDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.ImportEndDateTimePicker.BackColor = Const.ErrorBackColor;
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
            bool? completeflg = null;

            if (this.CompleteYesRadioButton.Checked) completeflg = true;
            if (this.CompleteNoRadioButton.Checked) completeflg = false;

            this.ListSearchCond = new ProductionCarSearchModel()
            {
                START_DATE = this.StartDateTimePicker.SelectedDate,
                END_DATE = this.EndDateTimePicker.SelectedDate,
                GENERAL_CODE = this.GeneralCodeComboBox.Text,
                PROTOTYPE_PERIOD = this.PrototypeTimingTextBox.Text,
                VEHICLE = this.VehicleTextBox.Text,
                IMPORT_START_DATE = this.ImportStartDateTimePicker.SelectedDate,
                IMPORT_END_DATE = this.ImportEndDateTimePicker.SelectedDate,
                FLAG_ENTRY = completeflg
            };
        }
        #endregion

        #region 検索条件のクリア（入力値）
        /// <summary>
        /// 検索条件のクリア（入力値）
        /// </summary>
        private void ClearForm()
        {
            var today = DateTime.Today;

            // 状態
            this.CompleteNoRadioButton.Checked = true;

            // 完成年月日
            this.StartDateTimePicker.Value = new DateTime(today.AddMonths(-1).Year, today.AddMonths(-1).Month, 1);
            this.EndDateTimePicker.Value = null;

            // 開発符号
            this.GeneralCodeComboBox.SelectedValue = string.Empty;

            // 試作時期
            this.PrototypeTimingTextBox.Text = string.Empty;

            // 号車
            this.VehicleTextBox.Text = string.Empty;

            // 作成日
            this.ImportStartDateTimePicker.Value = new DateTime(today.AddMonths(-1).Year, today.AddMonths(-1).Month, 1);
            this.ImportEndDateTimePicker.Value = null;
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

        #region 状態マウスクリック
        /// <summary>
        /// 状態（完成日）マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompleteYesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var flg = ((RadioButton)sender).Checked;

            this.StartDateTimePicker.Enabled = flg;
            this.EndDateTimePicker.Enabled = flg;

            this.ImportButton.Enabled = !flg;
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
            // グリッドのセット
            FormControlUtil.FormWait(this, () => this.SetDataList());
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
            var list = new List<ProductionCarDeleteInModel>();

            var supplement = string.Empty;

            // 削除指定チェック
            foreach (var row in this.ProductionCarMultiRow.Rows.Where(x => Convert.ToBoolean(x.Cells["TargetCheckBoxColumn"].Value) == true))
            {
                if (row.Cells["反映日"].Value == null)
                {
                    list.Add(new ProductionCarDeleteInModel() { ID = Convert.ToInt64(row.Cells["ID"].Value) });
                }
                else
                {
                    supplement = string.Concat(Const.CrLf, Const.CrLf, "※試験車情報に反映済みのデータは削除できません。");
                }
            }

            // 削除対象があるかどうか
            if (list.Any() == false)
            {
                // 選択項目なしメッセージ
                Messenger.Warn(Resources.KKM00009 + supplement);

                return;
            }

            // 確認メッセージ
            if (Messenger.Confirm(Resources.KKM00008 + supplement) != DialogResult.Yes)
            {
                return;
            }

            // 削除処理
            FormControlUtil.FormWait(this, () =>
            {
                // 削除実行
                if (this.DeleteDataList(list) != true)
                {
                    return;
                }

                // 削除完了メッセージ
                Messenger.Info(Resources.KKM00003);

                // グリッドのセット
                this.SetDataList();
            });
        }
        #endregion

        #region 製作一覧インポートボタンクリック
        /// <summary>
        /// 製作一覧インポートボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductionListImportButton_Click(object sender, EventArgs e)
        {
            var frm = new OpenFileDialog() { Filter = ExcelFilter, FilterIndex = 2 };

            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                // Excel ファイル判定
                if (!FileUtil.IsFileExcel(frm.FileName))
                {
                    Messenger.Warn(Resources.TCM03018); // ファイルアクセス失敗

                    return;
                }

                // ファイルの読み込み
                if (this.ImportFile(frm.FileName))
                {
                    // 登録実行
                    if (this.PostImportList() != true)
                    {
                        return;
                    }

                    // インポートデータなし
                    if (this.ImportList.Any() == false)
                    {
                        Messenger.Warn(string.Concat(Resources.TCM03008, Const.CrLf, Const.CrLf, "※試験車情報に反映済みのデータは取り込みできません。"));

                        return;
                    }

                    Messenger.Info(Resources.KKM00002); // 登録完了

                    // ファイル名の保存
                    this.ExcelFileName = frm.FileName;

                    // グリッドのセット
                    this.SetDataList();

                    // インポートデータ初期化
                    this.ImportList = new List<ProductionCarCommonModel>();
                }
            });
        }
        #endregion

        #region 製作一覧エクスポートボタンクリック
        /// <summary>
        /// 製作一覧エクスポートボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // チェック行のみ対象
                var rows = this.ProductionCarMultiRow.Rows.Where(x => Convert.ToBoolean(x.Cells["TargetCheckBoxColumn"].Value) == true);

                if (rows == null || rows.Count() <= 0)
                {
                    Messenger.Warn(Resources.KKM00009);

                    return;
                }

                // ファイルの書き込み
                if (this.ExportFile(rows) != true)
                {
                    return;
                };
            });
        }
        #endregion

        #region 反映ボタンクリック
        /// <summary>
        /// 反映ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            // 反映データ初期化
            this.EntryList = new List<ProductionCarCommonModel>();

            var supplement = string.Empty;

            // 反映指定チェック
            foreach (var row in this.ProductionCarMultiRow.Rows.Where(x => Convert.ToBoolean(x.Cells["TargetCheckBoxColumn"].Value) == true))
            {
                if (row.Cells["反映日"].Value == null)
                {
                    this.EntryList.Add(row.DataBoundItem as ProductionCarCommonModel);
                }
                else
                {
                    supplement = string.Concat(Const.CrLf, Const.CrLf, "※試験車情報に反映済みのデータは反映できません。");
                }
            }

            // 反映対象があるかどうか
            if (this.EntryList.Any() == false)
            {
                // 選択項目なしメッセージ
                Messenger.Warn(Resources.KKM00009 + supplement);

                return;
            }

            // 確認メッセージ
            if (Messenger.Confirm(Resources.TCM00005.Replace("実行", "試験車情報に反映") + supplement) != DialogResult.Yes)
            {
                return;
            }

            // 反映処理
            FormControlUtil.FormWait(this, () =>
            {
                // 反映実行
                if (this.EntryTestCar(this.EntryList))
                {
                    // 完了メッセージ
                    Messenger.Info(Resources.TCM00006); // 実行完了

                    // グリッドのセット
                    this.SetDataList();

                    // 反映データ初期化
                    this.EntryList = new List<ProductionCarCommonModel>();
                }
            });
        }
        #endregion

        #region 試験車情報テーブル反映処理
        /// <summary>
        /// 試験車情報テーブル反映処理
        /// </summary>
        private bool EntryTestCar(List<ProductionCarCommonModel> list)
        {
            // API 実行
            var res = HttpUtil.PostResponse<ProductionCarCommonModel>(ControllerType.CarManagement, list);

            if (res.Status.Equals(Const.StatusSuccess))
            {
                return true;
            }

            return false;
        }
        #endregion

        #endregion

        #region グリッドのイベント

        #region セルのクリック
        /// <summary>
        /// セルのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductionCarMultiRow_CellClick(object sender, CellEventArgs e)
        {
            // 全選択チェックボックスクリックの場合
            if (e.CellIndex == 0 && e.RowIndex == -1)
            {
                // 選択チェックボックスの表示を更新する
                this.CheckBoxAll.Checked = !this.CheckBoxAll.Checked;
            }
        }
        #endregion

        #region グリッドの描画
        /// <summary>
        /// グリッドの描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductionCarMultiRow_CellPainting(object sender, CellPaintingEventArgs e)
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
                    Point point = new Point((bmp.Width - CheckBoxAll.Width) / 2, (bmp.Height - CheckBoxAll.Height) / 2);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    CheckBoxAll.DrawToBitmap(bmp, new Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

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

        #region 全チェック変更
        /// <summary>
        /// 全チェック変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // バインドフラグ
                this.IsBind = true;

                // CheckBoxCell 不具合対応
                this.ProductionCarMultiRow.CurrentCell = null;

                // 全ての行の選択を設定
                foreach (var row in this.ProductionCarMultiRow.Rows)
                {
                    //Append Start 2020/01/27 杉浦
                    // 非表示行は未処理
                    if (!row.Visible) continue;
                    //Append End 2020/01/27 杉浦

                    row.Cells["TargetCheckBoxColumn"].Value = this.CheckBoxAll.Checked;
                }

                // バインドフラグ
                this.IsBind = false;
            });
        }
        #endregion

        #endregion

        #region データの操作
        /// <summary>
        /// 一覧データの取得
        /// </summary>
        /// <param name="cond"></param>
        private List<ProductionCarCommonModel> GetDataList(ProductionCarSearchModel cond)
        {
            // APIで取得
            var res = HttpUtil.GetResponse<ProductionCarSearchModel, ProductionCarCommonModel>(ControllerType.ProductionCar, cond);

            // レスポンスチェック
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return null;
            }

            return res.Results?.ToList();
        }

        /// <summary>
        /// 一覧データの削除
        /// </summary>
        /// <param name="list"></param>
        private bool DeleteDataList(List<ProductionCarDeleteInModel> list)
        {
            return HttpUtil.DeleteResponse<ProductionCarDeleteInModel>(ControllerType.ProductionCar, list).Status.Equals(Const.StatusSuccess);
        }

        /// <summary>
        /// インポートデータの登録（更新）
        /// </summary>
        private bool PostImportList()
        {
            var res = HttpUtil.PostResponse<ProductionCarCommonModel, ProductionCarCommonModel>(ControllerType.ProductionCar, this.ImportList);

            this.ImportList = res?.Results?.ToList();

            return res?.Results.Count() >= 0;
        }
        #endregion

        #region マスタデータの取得・検索

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

        #region 開発符号情報（製作車）検索
        /// <summary>
        /// 開発符号情報（製作車）検索
        /// </summary>
        private List<CommonMasterModel> GetGeneralCodeList()
        {
            // Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>(ControllerType.ProductionCarGeneralCodeInfo, null)?.Results?.ToList();
        }
        #endregion
        
        #endregion

        #region Excelインポート

        #region ファイルの読み込み
        /// <summary>
        /// ファイルの読み込み
        /// </summary>
        /// <param name="path"></param>
        private bool ImportFile(string path)
        {
            Func<string, DateTime?> getDateTime = dt => string.IsNullOrWhiteSpace(dt) ? null : (DateTime?)Convert.ToDateTime(dt);
            Func<int, string, string> cnvLineFormat = (no, msg) => string.Concat(String.Format("{0, 3}", no), "行：", msg);

            var list = new List<ProductionCarCommonModel>();
            var exculude = new StringBuilder();
            var error = new StringBuilder();
            var rowno = 7;

            using (var xls = new XlsUtil(path))
            {
                foreach (var cell in xls.GetRowsList(0, rowno, null, null, 29).Where(x => x.Count > 29))
                {
                    rowno++;

                    // 空行は行カウントのみでスキップ
                    if (!cell.Any(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        continue;
                    }

                    // 入力チェック
                    var tuple = TupleImportDataCheck(cell);

                    // 除外
                    if (tuple.Item1.Any() == true)
                    {
                        tuple.Item1.ForEach(x => exculude.AppendLine(cnvLineFormat(rowno, x)));

                        continue;
                    }

                    // エラー
                    if (tuple.Item2.Any() == true)
                    {
                        tuple.Item2.ForEach(x => error.AppendLine(cnvLineFormat(rowno, x)));

                        continue;
                    }

                    list.Add(new ProductionCarCommonModel
                    {
                        SERIAL_NO = cell[0],
                        ISSUE_NO = cell[1],
                        REVISION_NO = cell[2],
                        GENERAL_CODE = cell[3],
                        PROTOTYPE_PERIOD = cell[4],
                        VEHICLE = cell[5],
                        CAR_MODEL = cell[6],
                        MODEL_CODE = cell[7],
                        VEHICLE_NO = cell[8],
                        TEST_PURPOSE = cell[9],
                        SECTION_GROUP_CODE = cell[10],
                        COMPLETE_REQUEST_DATE = getDateTime(cell[11]?.Replace("～", "")),
                        COMPLETE_DATE = getDateTime(cell[12]),
                        FIXED_ASSET_NO = cell[13],
                        RESEARCH_NO = cell[14],
                        CONSTRUCT_NO = cell[15],
                        CAR_GROUP = cell[16],
                        CAR_TYPE = cell[17],
                        MAKER_NAME = cell[18],
                        DESTINATION = cell[19],
                        NAME_REMARKS = cell[20],
                        DISPOSAL_PLAN_MONTH = getDateTime(cell[21]),
                        MANAGEMENT_NO = cell[22],
                        METHOD = cell[28],
                        TARGET = cell[29],
                        INPUT_PERSONEL_ID = SessionDto.UserId
                    });
                }
            }

            // エラー
            if (error.Length > 0)
            {
                Messenger.Warn(error.ToString());

                return false;
            }

            if (list.Any() == false)
            {
                Messenger.Warn(string.Concat(Resources.TCM03008, exculude.Length > 0 ? Const.CrLf + Const.CrLf + exculude.ToString() : ""));

                return false;
            }
            
            // インポートリストのセット
            this.ImportList = list;

            return true;
        }
        #endregion

        #region インポートデータのチェック
        /// <summary>
        /// インポートデータのチェック
        /// </summary>
        private Tuple<List<string>, List<string>> TupleImportDataCheck(List<string> cell)
        {
            var result = Tuple.Create(new List<string>(), new List<string>());

            #region チェックデリゲード

            // 必須チェック
            Func<string, string, bool> isRequired = (name, value) =>
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    result.Item2.Add(string.Format(Resources.KKM00001, name));

                    return false;
                }

                return true;
            };

            // 数値チェック
            Func<string, string, bool> isInteger = (name, value) =>
            {
                // null許可
                if (string.IsNullOrWhiteSpace(value)) return true;

                int ret;
                if (!int.TryParse(value, out ret))
                {
                    result.Item2.Add(string.Format(Resources.KKM00025, name));

                    return false;
                }

                return true;
            };

            // 桁数チェック（Byte）
            Func<string, string, int, bool> isWide = (name, value, length) =>
            {
                if (StringUtil.SjisByteLength(value) > length)
                {
                    result.Item2.Add(string.Concat(string.Format(Resources.KKM00027, name), "半角数字で入力してください。"));

                    return false;
                }

                return true;
            };

            // 日付チェック
            Func<string, string, bool> isDate = (name, value) =>
            {
                // null許可
                if (string.IsNullOrWhiteSpace(value)) return true;

                var dt = new DateTime();

                value += value.Length == 7 ? "/01" : ""; 

                if (!DateTime.TryParse(value, out dt))
                {
                    result.Item2.Add(string.Concat(string.Format(Resources.KKM00026, name), "yyyy/mm/dd形式で入力してください。"));

                    return false;
                }

                if (dt.Year < 2000)
                {
                    result.Item2.Add(string.Format(string.Format(Resources.KKM00026, name), "2000年以降で入力してください。"));

                    return false;
                }

                return true;
            };

            #endregion

            // 反映日チェック
            if (!string.IsNullOrWhiteSpace(cell[26]))
            {
                result.Item1.Add("反映日が入力されているため取り込まれません。");

                return result;
            }

            // 取込対象チェック
            if (string.IsNullOrWhiteSpace(cell[29]))
            {
                result.Item1.Add("取込対象が入力されていないため取り込まれません。");

                return result;
            }

            // 複合キー(開発符号・試作時期・号車)チェック
            if (string.IsNullOrWhiteSpace(cell[3]) || string.IsNullOrWhiteSpace(cell[4]) || string.IsNullOrWhiteSpace(cell[5]))
            {
                // 複合キー
                result.Item2.Add(string.Format(Resources.KKM00001, "開発符号・試作時期・号車"));

                return result;
            }

            // 数値・桁数・形式チェック
            isWide("通しNO", cell[0], 10);
            isWide("発行NO", cell[1], 20);
            isWide("改訂NO", cell[2], 10);
            isWide("開発符号", cell[3], 20);
            isWide("試作時期", cell[4], 20);
            isWide("号車", cell[5], 50);
            isWide("車種", cell[6], 50);
            isWide("型式・仕向・OP", cell[7], 255);
            isWide("打刻NO", cell[8], 30);
            isWide("試験目的", cell[9], 255);
            isWide("使用部署", cell[10], 20);
            isDate("完成希望日", cell[11]?.Replace("～", ""));
            isDate("完成日", cell[12]);
            isWide("固定資産NO", cell[13], 10);
            isWide("研命ナンバー", cell[14], 50);
            isWide("工事区分NO", cell[15], 10);
            isWide("車系", cell[16], 10);
            isWide("車型", cell[17], 10);
            isWide("メーカー名", cell[18], 50);
            isWide("仕向地", cell[19], 50);
            isWide("名称備考", cell[20], 255);
            isDate("処分予定年月", cell[21]);
            isWide("管理票NO", cell[22], 10);
            isInteger("管理票NO", cell[22]);
            isWide("制作方法", cell[28], 10);
            isWide("取込対象", cell[29], 10);

            return result;
        }
        #endregion

        #endregion

        #region Excel エクスポート

        #region ファイルの書き込み
        /// <summary>
        /// ファイルの書き込み
        /// </summary>
        /// <param name="rows"></param>
        private bool ExportFile(IEnumerable<Row> rows)
        {
            #region 変換デリゲード

            // 日付変換
            Func<object, string, string> cnvDateString = (value, format) =>
            {
                if (value == null)
                {
                    return string.Empty;
                }

                return Convert.ToDateTime(value).ToString(format);
            };

            #endregion

            var file = Properties.Resources.ProductionCarExportTemplate;
            var fileName = this.ExcelFileName;
            var filePath = string.Empty;

            // ファイル保存ダイアログ
            using (var sfd = new SaveFileDialog
            {
                Title = "試作車登録シート出力",
                Filter = ExcelFilter,
                FileName = string.Format(fileName, DateTime.Now)
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                filePath = sfd.FileName;
            }

            FormControlUtil.FormWait(this, () =>
            {
                // Excelユーティリティ
                using (var xls = new XlsUtil(file))
                {
                    // シートの設定
                    xls.SetSheet(0);

                    // 書込開始行
                    var index = 8;

                    // Excel書込
                    foreach (var row in rows)
                    {
                        xls.WriteSheet(new Dictionary<string, dynamic>()
                        {
                            { "A" + index, row.Cells["通しNO"].Value },
                            { "B" + index, row.Cells["発行NO"].Value },
                            { "C" + index, row.Cells["改訂NO"].Value },
                            { "D" + index, row.Cells["開発符号"].Value },
                            { "E" + index, row.Cells["試作時期"].Value },
                            { "F" + index, row.Cells["号車"].Value },
                            { "G" + index, row.Cells["車種"].Value },
                            { "H" + index, row.Cells["型式仕向OP"].Value },
                            { "I" + index, row.Cells["車体NO"].Value },
                            { "J" + index, row.Cells["試験目的"].Value },
                            { "K" + index, row.Cells["使用部署"].Value },
                            { "L" + index, cnvDateString(row.Cells["完成希望日"].Value, "yyyy/MM/dd") },
                            { "M" + index, cnvDateString(row.Cells["完成日"].Value, "yyyy/MM/dd") },
                            { "N" + index, row.Cells["固定資産NO"].Value },
                            { "O" + index, row.Cells["研命ナンバー"].Value },
                            { "P" + index, row.Cells["工事区分NO"].Value },
                            { "Q" + index, row.Cells["車系"].Value },
                            { "R" + index, row.Cells["車型"].Value },
                            { "S" + index, row.Cells["メーカー名"].Value },
                            { "T" + index, row.Cells["仕向地"].Value },
                            { "U" + index, row.Cells["名称備考"].Value },
                            { "V" + index, cnvDateString(row.Cells["処分予定年月"].Value, "yyyy/MM") },
                            { "W" + index, row.Cells["管理票NO"].Value },
                            { "X" + index, row.Cells["履歴NO"].Value?.ToString() },
                            { "Y" + index, cnvDateString(row.Cells["作成日"].Value, "yyyy/MM/dd") },
                            { "Z" + index, row.Cells["作成者"].Value },
                            { "AA" + index, cnvDateString(row.Cells["反映日"].Value, "yyyy/MM/dd") },
                            { "AB" + index, row.Cells["反映者"].Value },
                            { "AC" + index, row.Cells["制作方法"].Value },
                            { "AD" + index, row.Cells["取込対象"].Value },
                            { "AE" + index, row.Cells["エラーメッセージ"].Value },
                        });

                        index++;
                    }

                    // Excel保存
                    xls.Save(filePath);
                }
            });

            return true;
        }
        #endregion

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

            // レイアウト調整
            SetLayout();
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
