using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Config;
using DevPlan.Presentation.UC.MultiRow;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 指摘一覧
    /// </summary>
    public partial class DesignCheckPointListForm : BaseForm
    {
        #region メンバ変数

        private const string TreatmentWhenHelpMsg = "織込日が決まっている場合は織込日、\r\n織込日未定の場合はいつまでに決めるのか、\r\n未調整の場合いつまでに調整するのか等、\r\n何らかの日付を必ず入力してください。";

        private const string progressHelpMsg = "処置記号を\r\n入力してください。";

        private const string NoneText = "－";

        /// <summary>
        /// クラス生成クラス
        /// </summary>
        private PointFactory Factory;

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; } = false;

        /// <summary>編集フラグ</summary>
        private bool IsEdit { get; set; } = false;

        /// <summary>再読込フラグ</summary>
        private bool IsRefresh { get; set; } = false;

        /// <summary>検索条件</summary>
        private DesignCheckPointGetInModel ListSearchCond { get; set; }

        /// <summary>
        /// ADユーザー情報
        /// </summary>
        private Dictionary<string, ADUserInfo> ADUserDictionary;

        #endregion

        #region プロパティ

        /// <summary>
        /// 指摘追加により追加された場合の仮の指摘ID
        /// </summary>
        public const int TEMP_POINT_ID = 0;

        /// <summary>画面名</summary>
        public override string FormTitle { get { return "指摘一覧"; } }

        /// <summary>設計チェック</summary>
        public DesignCheckGetOutModel DesignCheck { get; set; } = null;

        /// <summary>
        /// 親画面の再描画
        /// </summary>
        public System.Action ParentFormRefresh { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignCheckPointListForm()
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
        private void DesignCheckPointListForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 初期化
                Factory = new PointFactory(
                    PointMultiRow,
                    RowCountLabel,
                    DesignCheck,
                    base.GetFunction(FunctionID.DesignCheck),
                    () => { IsEdit = true; PointMultiRow.Refresh(); });

                // AD情報の取得
                ADUserDictionary = ADUserInfoData.Dictionary;

                // 画面初期化
                this.InitForm();

                // 設計チェック指摘一覧設定
                this.SetDesignCheckPointList();

                // 担当者セルクリックイベント登録
                Factory.BtnUser.SingleAction = () =>
                {
                    FormControlUtil.FormWait(this, () =>
                    {
                        this.PointMultiRow.CurrentCell.ReadOnly = true;
                        var detail = Factory.MultiRowContoller.GetModel(this.PointMultiRow.CurrentCell.RowIndex);

                        //Append Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
                        if (detail.試作管理NO == null)
                        {
                            //Append End 2021/07/20 杉浦 開発計画表設計チェック機能改修
                            using (var form = new UserListForm { UserAuthority = Factory.Authority.UserAuthorityModel, SectionCode = detail.担当課名, StatusCode = "a", IsSearchLimit = false })
                            {
                                if (form.ShowDialog(this) == DialogResult.OK)
                                {
                                    detail.担当課名 = form.User.SECTION_CODE;
                                    detail.担当者名 = form.User.NAME;
                                    detail.担当者_ID = form.User.PERSONEL_ID;

                                    // 担当者TEL
                                    this.SetADUserInfo(this.PointMultiRow.CurrentRow);

                                    //編集フラグを有効化
                                    this.IsEdit = true;

                                    //グリッドリフレッシュ
                                    this.PointMultiRow.Refresh();
                                }
                            }
                            //Append Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
                        }
                        //Append End 2021/07/20 杉浦 開発計画表設計チェック機能改修

                    });
                };

                // 担当者セルダブルクリックイベント登録
                Factory.BtnUser.DoubleAction = () =>
                {

                    //Append Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
                    var detail = Factory.MultiRowContoller.GetModel(this.PointMultiRow.CurrentCell.RowIndex);
                    if (detail.試作管理NO == null)
                    {
                        //Append End 2021/07/20 杉浦 開発計画表設計チェック機能改修
                        this.PointMultiRow.CurrentCell.ReadOnly = false;
                        this.PointMultiRow.BeginEdit(true);
                        //Append Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
                    }
                    //Append End 2021/07/20 杉浦 開発計画表設計チェック機能改修
                };

                //Append Start 2021/06/24 張晋華 開発計画表設計チェック機能改修

                //パラメータ設定
                var cond = new DesignCheckExcelInputModel
                {
                    //開催日_ID
                    開催日_ID = (int)this.DesignCheck.ID
                };

                //指摘追加ボタン活性/非活性の判断
                var res = HttpUtil.GetResponse<DesignCheckExcelInputModel, DesignCheckExcelInputModel>(ControllerType.DesignCheckExcelInput, cond);

                //EXCEL_INPUTに開催日_IDが登録されていないと非活性にする
                if (res == null || res.Status != Const.StatusSuccess)
                {
                    RowAddButton.Enabled = false;
                }

                //Append End 2021/06/24 張晋華 開発計画表設計チェック機能改修
            });
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //バインド中ON
            this.IsBind = true;

            try
            {
                // 開催日・設計チェック名セット
                this.SetHeaderData();

                // ステータス
                this.StatusOpenCheckBox.Checked = true;
                this.StatusCloseCheckBox.Checked = true;

                // 試験車
                this.TestCarComboBox.SelectedIndex = -1;

                // 状況
                this.ProgSymbolComboBox.SelectedIndex = -1;

                // ボタン表示制御
                this.RowAddButton.Visible = Factory.Authority.IsVisible(RowAddButton);
                this.CarAddButton.Visible = Factory.Authority.IsVisible(CarAddButton);
                this.RowDeleteButton.Visible = Factory.Authority.IsVisible(RowDeleteButton);
                this.EntryButton.Visible = Factory.Authority.IsVisible(EntryButton);
                this.DownloadButton.Visible = Factory.Authority.IsVisible(DownloadButton);
                this.DesignCheckUserListButton.Visible = Factory.Authority.IsVisible(DesignCheckUserListButton);
            }
            finally
            {
                //バインド中OFF
                this.IsBind = false;
            }
        }

        /// <summary>
        /// AD情報のセット
        /// </summary>
        /// <param name="row"></param>
        private void SetADUserInfo(Row row)
        {
            if (ADUserDictionary == null)
            {
                return;
            }

            if (row.Cells["UserIDTextBoxColumn"].Value == null)
            {
                return;
            }

            var personelId = Convert.ToString(row.Cells["UserIDTextBoxColumn"].Value);
            var searchPersonelId = personelId.PadLeft(5, '0').Substring(0, 5);

            var val = new ADUserInfo();
            var key = string.Format("{0}_{1}", searchPersonelId, Convert.ToString(row.Cells["UserTextBoxColumn"].Value)).Replace(" ", "").Replace("　", "");

            ADUserDictionary.TryGetValue(key, out val);

            row["UserTelTextBoxColumn"].Value = string.Empty;

            // 内線番号あり
            if (val != null || val?.Tel != null)
            {
                row["UserTelTextBoxColumn"].Value = val.Tel;
            }
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckPointListForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = StatusOpenCheckBox;
            this.StatusOpenCheckBox.Focus();
        }
        #endregion

        #region 検索条件ボタンクリック
        /// <summary>
        /// 検索条件ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            var flg = !this.SearchConditionTableLayoutPanel.Visible;

            //検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, new[] { this.RowCountLabel }, 77);

            //検索結果メッセージ
            this.ListDataLabel.Visible = flg;

            //凡例文言
            this.TestCarSituationGroupBox.Visible = flg;
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
                //画面を変更していて登録するかどうか
                if (this.EditDesignCheckPointEntry() == true)
                {
                    // AD情報の取得
                    ADUserDictionary = ADUserInfoData.Dictionary;

                    //設計チェック指摘一覧設定
                    this.SetDesignCheckPointList();

                    Factory.MultiRowContoller.ScrollFirstRow();
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
            // 画面初期化
            this.InitForm();
        }
        #endregion

        #region 設計チェック指摘一覧イベント

        #region マウスイン
        /// <summary>
        /// マウスイン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellMouseEnter(object sender, CellEventArgs e)
        {
            //行ヘッダー以外は終了
            if (e.RowIndex != -1)
            {
                return;
            }

            // 処置内容（いつまでに？）
            if (e.CellName == "ch_TreatmentWhen")
            {
                //ツールチップを表示
                this.SituationToolTip.Active = false;
                this.SituationToolTip.Active = true;
                this.SituationToolTip.Show(TreatmentWhenHelpMsg, this.PointMultiRow);
            }
        }
        #endregion

        #region マウスアウト
        /// <summary>
        /// マウスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellMouseLeave(object sender, CellEventArgs e)
        {
            //ツールチップを非表示
            this.SituationToolTip.Hide(this.PointMultiRow);
        }
        #endregion

        #region セルクリック
        /// <summary>
        /// セルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellClick(object sender, CellEventArgs e)
        {
            // 全選択チェックボックスクリックの場合
            if ((e.CellName == "ch_Target") && (e.RowIndex == -1) && Factory.Authority.IsCan("TargetCheckBoxColumn", ""))
            {
                // 選択チェックボックスの表示を更新する
                this.CheckBoxAll.Checked = !this.CheckBoxAll.Checked;
            }
        }
        #endregion

        #region マウスキーダウンイベント
        /// <summary>
        /// マウスキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellMouseDown(object sender, CellMouseEventArgs e)
        {
            // 通常の左クリック以外受け付けない
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            // 指摘Noの最初の行以外なら終了
            if (Factory.MultiRowContoller.IsFirstPointRow(e.RowIndex) == false)
            {
                return;
            }

            // 編集モード中なら終了
            if (this.PointMultiRow.IsCurrentCellInEditMode)
            {
                return;
            }

            // 各ボタン処理
            FormControlUtil.FormWait(this, () => Factory.GetBtnInstance(e.CellName)?.Action(Factory.MultiRowContoller.GetModel(e.RowIndex)));
        }
        #endregion

        #region 値変更後
        /// <summary>
        /// 値変更後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellValueChanged(object sender, CellEventArgs e)
        {
            // バインド中のイベントは処理しない
            if (this.IsBind == true)
            {
                return;
            }

            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = this.PointMultiRow.Rows[e.RowIndex];

            // 編集フラグを有効化
            if (e.CellName != "TargetCheckBoxColumn" && e.CellName != "HistoryLinkColumn")
            {
                this.IsEdit = true;
            }

            // バインドフラグOn
            this.IsBind = true;

            try
            {
                // ステータス
                if (e.CellName == "StatusComboBoxColumn")
                {
                    Factory.MultiRowContoller.ChangeStatusEnable(row);
                }

                // 担当者
                if (e.CellName == "UserTextBoxColumn")
                {
                    Factory.StaffController.Notice(row);
                }
            }
            finally
            {
                // バインドフラグOff
                this.IsBind = false;
            }
        }
        #endregion

        #region 一覧描画（全選択チェックボックス描画）
        /// <summary>
        /// 一覧描画
        /// （全選択チェックボックス描画）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellPainting(object sender, CellPaintingEventArgs e)
        {
            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if ((e.CellIndex == 0) && (e.RowIndex == -1))
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

        #region データエラー
        /// <summary>
        /// データエラー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_DataError(object sender, DataErrorEventArgs e)
        {
            if (e.CellName == "StatusComboBoxColumn" || e.CellName == "ProgressComboBoxColumn" ||
                e.CellName == "TreatmentCheckBoxColumn" || e.CellName == "TreatmentOKCheckBoxColumn" ||
                e.CellName == "RepairCheckBoxColumn" || e.CellName == "ApprovalOKCheckBoxColumn")
            {
                //例外無視
                e.Cancel = false;
            }
        }
        #endregion

        #region チェックボックスセル変更時
        /// <summary>
        /// チェックボックスセル変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellEditedFormattedValueChanged(object sender, CellEditedFormattedValueChangedEventArgs e)
        {
            // チェックボックスセルを変更すると内部バリデーションで値エラーが起こるのでエラーを無効化して値を強制設定する
            if (e.CellName == "TreatmentCheckBoxColumn" || e.CellName == "TreatmentOKCheckBoxColumn" ||
                e.CellName == "RepairCheckBoxColumn" || e.CellName == "ApprovalOKCheckBoxColumn")
            {
                if (Convert.ToBoolean(this.PointMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].EditedFormattedValue) == true)
                {
                    this.PointMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].Value = (int?)1;
                }
                else
                {
                    this.PointMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].Value = null;
                }
            }
        }
        #endregion

        #region セル結合時
        /// <summary>
        /// セル結合時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_QueryCellMergeState(object sender, QueryCellMergeStateEventArgs e)
        {
            if (e.ShouldMerge == true)
            {
                var itemIdIndex = this.PointMultiRow.Columns["IDTextBoxColumn"].Index;

                if (e.QueryCell.CellIndex != itemIdIndex)
                {
                    CellPosition newQueryCell = new CellPosition(e.QueryCell.RowIndex, itemIdIndex);
                    CellPosition newTargetCell = new CellPosition(e.TargetCell.RowIndex, itemIdIndex);

                    // 項目IDが同じ場合セル結合させる
                    e.ShouldMerge = this.PointMultiRow.IsMerged(newQueryCell, newTargetCell);
                }
            }
        }
        #endregion

        #endregion

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //設計チェック指摘一覧が登録できたかどうか
                if (this.EntryDesignCheckPoint() == true)
                {
                    //設計チェック指摘一覧設定
                    this.SetDesignCheckPointList();
                }
            });
        }
        #endregion

        #region 参加者一覧ボタンクリック
        private void DesignCheckUserListButton_Click(object sender, EventArgs e)
        {
            Factory.UserListFormContoroller.OpenReadOnly();
        }
        #endregion

        #region Excel出力ボタンクリック
        /// <summary>
        /// Excel出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            // 出力列（選択チェックボックス列以外の表示している列）
            var OutTargetCols = Factory.MultiRowContoller.ColInfoList.Where((x) => x.Visible == true && x.RowCellName != "TargetCheckBoxColumn");

            var util = new MultiRowUtil(this.PointMultiRow);
            util.Excel.GetCols = () => this.PointMultiRow.Columns.Cast<Column>().Where(x => OutTargetCols.Any((y) => y.RowCellName == x.Name) == true);
            util.Excel.GetHeads = () => OutTargetCols.Select((x) => this.PointMultiRow.ColumnHeaders[0].Cells[x.HeaderName].Value.ToString()).ToList();
            util.Excel.TreatmentRows = (x) => x.Select((y) => { y.InsertRange(0, new string[] { OpenDayLabel.Text, DesignCheckNameLabel.Text }); return y; }).ToList();
            util.Excel.TreatmentHeaders = (x) => { x.InsertRange(0, new string[] { "開催日", "設計チェック名" }); return x; };
            util.Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
        }
        #endregion

        #region フォームクローズ前
        /// <summary>
        /// フォームクローズ前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckPointListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //設計チェック指摘一覧の編集は終了
                this.PointMultiRow.EndEdit();

                //画面を変更していて登録するかどうか
                if (this.EditDesignCheckPointEntry() == false)
                {
                    //登録に失敗した場合は閉じさせない
                    e.Cancel = true;
                    return;
                }

                // 参加者一覧画面も閉じる
                Factory.UserListFormContoroller.Close();

                // 更新の場合のみ
                if (this.IsRefresh)
                {
                    // 親画面の再描画
                    ParentFormRefresh();
                }
            });
        }
        #endregion

        #region 開催日・設計チェック名セット
        /// <summary>
        /// 開催日・設計チェック名セット
        /// </summary>
        private void SetHeaderData()
        {
            // 開催日
            this.OpenDayLabel.Text = DateTimeUtil.ConvertDateString(this.DesignCheck?.開催日);

            // 設計チェック名
            this.DesignCheckNameLabel.Text = this.DesignCheck?.名称;
            this.DesignCheckNameLabel.Text += this.DesignCheck?.回 > 0 ? " " + this.DesignCheck?.回 + "回目" : string.Empty;
        }
        #endregion

        #region 状況コンボボックス設定
        /// <summary>
        /// 状況コンボボックス設定
        /// </summary>
        private void SetProgSymbolComboBox()
        {
            var isBind = this.IsBind;

            //バインド中ON
            this.IsBind = true;

            var tmp = ProgSymbolComboBox.SelectedValue;

            // 状況記号コンボ
            ProgSymbolComboBox.DisplayMember = "記号";
            ProgSymbolComboBox.ValueMember = "記号";
            FormControlUtil.SetComboBoxItem(
                ProgSymbolComboBox,
                Factory.MultiRowContoller.ModelList
                    .Where((x) => string.IsNullOrEmpty(x.状況記号) == false && x.DELETE_FLG != true)
                    .Select((x) => x.状況記号)
                    .Distinct()
                    .Select((x) => new DesignCheckProgressSymbolGetOutModel() { 記号 = x })
                    .ToList(),
                true);

            // 選択値設定
            if (tmp == null)
            {
                ProgSymbolComboBox.SelectedIndex = -1;
            }
            else
            {
                ProgSymbolComboBox.SelectedValue = tmp;
            }

            //バインド可否を元の状態に復元
            this.IsBind = isBind;
        }
        #endregion

        #region 試験車名のコンボボックス設定
        /// <summary>
        /// 試験車名のコンボボックス設定
        /// </summary>
        private void SetTestCarNameComboBox()
        {
            var isBind = this.IsBind;

            //バインド中ON
            this.IsBind = true;

            var tmp = TestCarComboBox.SelectedValue;

            // 試験車名
            this.TestCarComboBox.ValueMember = "試験車_ID";
            this.TestCarComboBox.DisplayMember = "試験車名";
            FormControlUtil.SetComboBoxItem(
                this.TestCarComboBox,
                Factory.MultiRowContoller.ModelList
                    .Where((x) => x.試験車_ID != null && x.DELETE_FLG != true)
                    .Select((x) => new { 試験車_ID = x.試験車_ID, 試験車名 = x.試験車名 })
                    .Distinct()
                    .Select((x) => new DesignCheckCarGetOutModel() { 試験車_ID = x.試験車_ID.Value, 試験車名 = x.試験車名 })
                    .ToList(),
                true);

            // 選択値設定
            if (tmp == null)
            {
                TestCarComboBox.SelectedIndex = -1;
            }
            else
            {
                TestCarComboBox.SelectedValue = tmp;
            }

            //バインド可否を元の状態に復元
            this.IsBind = isBind;
        }
        #endregion

        #region 設計チェック指摘一覧設定
        /// <summary>
        /// 設計チェック指摘一覧設定
        /// </summary>
        private void SetDesignCheckPointList()
        {
            // バインド中ON
            this.IsBind = true;

            try
            {
                // 初期化
                this.CheckBoxAll.Checked = false;

                // 設計チェック指摘一覧取得
                this.Factory.MultiRowContoller.ModelList = this.GetDesignCheckPointList();

                // 設計チェック指摘一覧設定
                this.Factory.MultiRowContoller.Bind();

                // 設計チェック指摘一覧調整
                this.AdjustDesignCheckPointList();

                // 設計チェック指摘一覧が取得できたかどうか
                this.ListDataLabel.Text = Factory.MultiRowContoller.ModelList.Any() == true ? "" : Resources.KKM00005;

                // 一覧を未選択状態に設定
                this.PointMultiRow.CurrentCell = null;

                // 試験車名のコンボボックス設定
                this.SetTestCarNameComboBox();

                // 状況のコンボボックス設定
                this.SetProgSymbolComboBox();

                // 編集フラグ初期化
                this.IsEdit = false;
            }
            finally
            {
                // バインド中OFF
                this.IsBind = false;
            }
        }

        /// <summary>
        /// 設計チェック指摘一覧調整
        /// </summary>
        private void AdjustDesignCheckPointList()
        {
            foreach (var row in this.PointMultiRow.Rows)
            {
                // 担当者TELが登録されていない場合
                if (string.IsNullOrWhiteSpace(Convert.ToString(row.Cells["UserTelTextBoxColumn"].Value)))
                {
                    // 担当者TEL
                    SetADUserInfo(row);
                }

                // 編集許可設定
                Factory.MultiRowContoller.SetRowEnable(row);

                // 履歴が無い場合は履歴はリンクを解除
                if (Convert.ToInt32(row.Cells["HistoryCountColumn"].Value) <= 0)
                {
                    row.Cells["HistoryLinkColumn"].Value = NoneText;

                    var linkCell = row.Cells["HistoryLinkColumn"] as LinkLabelCell;
                    linkCell.LinkBehavior = LinkBehavior.NeverUnderline;
                    linkCell.LinkColor = Color.Black;
                }

                // 背景色設定
                Factory.MultiRowContoller.SetRowBackColor(row, Convert.ToInt32(row.Cells["StatusComboBoxColumn"].Value));
            }
        }
        #endregion

        #region 変更設計チェック指摘一覧登録可否
        /// <summary>
        /// 変更の登録確認、及び登録処理（true：変更がない・登録しないを選択・正常登録完了、false：登録失敗）
        /// 後続処理続行用
        /// </summary>
        /// <returns></returns>
        private bool EditDesignCheckPointEntry()
        {
            //画面を変更していないか登録するかどうか
            if (this.IsEdit == false || (this.IsEdit == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                return true;
            }

            //設計チェック指摘一覧の登録
            return this.EntryDesignCheckPoint();
        }

        /// <summary>
        /// 変更の登録確認、及び登録処理（true：登録実行、false：変更がない・登録しないを選択）
        /// 単発処理用
        /// </summary>
        /// <returns></returns>
        private bool Entry()
        {
            if (this.IsEdit == false || (this.IsEdit == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                return false;
            }

            FormControlUtil.FormWait(this, () =>
            {
                // 登録
                if (this.EntryDesignCheckPoint() == true)
                {
                    // 再描画
                    this.SetDesignCheckPointList();
                }
            });

            return true;
        }
        #endregion

        #region データの取得
        /// <summary>
        /// 設計チェックの取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private DesignCheckGetOutModel GetDesignCheckList(DesignCheckGetInModel cond)
        {
            var list = new List<DesignCheckGetOutModel>();

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckGetInModel, DesignCheckGetOutModel>(ControllerType.DesignCheck, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            return list?.FirstOrDefault();
        }

        /// <summary>
        /// 設計チェック指摘一覧の取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckPointGetOutModel> GetDesignCheckPointList()
        {
            bool? status = null;
            int? testCarID = null;
            string progSymbol = "";

            //Openのみ選択かどうか
            if (this.StatusOpenCheckBox.Checked == true && this.StatusCloseCheckBox.Checked == false)
            {
                status = this.StatusOpenCheckBox.Checked;
            }
            //Closeのみ選択かどうか
            else if (this.StatusOpenCheckBox.Checked == false && this.StatusCloseCheckBox.Checked == true)
            {
                status = !this.StatusCloseCheckBox.Checked;
            }

            //試験車名を選択しているかどうか
            if (this.TestCarComboBox.SelectedIndex >= 0 && this.TestCarComboBox.SelectedValue != null)
            {
                testCarID = Convert.ToInt32(this.TestCarComboBox.SelectedValue.ToString());
            }

            // 状況記号
            if (this.ProgSymbolComboBox.SelectedIndex >= 0 && this.ProgSymbolComboBox.SelectedValue != null)
            {
                progSymbol = this.ProgSymbolComboBox.SelectedValue.ToString();
            }

            //パラメータ設定
            var cond = new DesignCheckPointGetInModel
            {
                // 開催日_ID
                開催日_ID = (int)this.DesignCheck.ID,

                // 試験車ID
                試験車_ID = testCarID,

                // 状況記号
                状況記号 = progSymbol,

                // ステータス
                OPEN_FLG = status,

                // 最新フラグ
                NEW_FLG = true
            };

            //設計チェック指摘一覧検索条件
            this.ListSearchCond = cond;

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckPointGetInModel, DesignCheckPointGetOutModel>(ControllerType.DesignCheckPoint, cond);

            //レスポンスが取得できたかどうか
            var list = new List<DesignCheckPointGetOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;
        }
        #endregion

        #region 設計チェック指摘一覧の登録
        /// <summary>
        /// 設計チェック指摘一覧の登録
        /// </summary>
        /// <returns></returns>
        private bool EntryDesignCheckPoint()
        {
            //設計チェック指摘一覧のチェック
            if (this.IsEntryDesignCheckPoint() == false)
            {
                return false;
            }

            // 登録対象の取得
            var list = this.GetEntryDesignCheckPoint();

            //登録対象があるかどうか
            if (list == null || list.Any() == false)
            {
                return false;
            }

            //設計チェック指摘一覧登録
            var res = HttpUtil.PostResponse(ControllerType.DesignCheckPoint, list);

            //レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            //登録後メッセージ
            Messenger.Info(Resources.KKM00002);

            // 再読込フラグ
            this.IsRefresh = true;

            return true;
        }

        /// <summary>
        /// 登録する設計チェック指摘一覧を取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckPointPostInModel> GetEntryDesignCheckPoint()
        {
            var pointList = new List<DesignCheckPointPostInModel>();

            var pointId = 0;

            var data = new DesignCheckPointPostInModel();

            foreach (var row in this.PointMultiRow.Rows)
            {
                // 初回の指摘の場合
                if (pointId != (int?)row.Cells["IDTextBoxColumn"].Value)
                {
                    // 指摘を追加
                    data = new DesignCheckPointPostInModel()
                    {
                        // 指摘ID
                        ID = (int?)row.Cells["IDTextBoxColumn"].Value,
                        // 開催日ID
                        開催日_ID = (int)this.DesignCheck.ID,
                        // 指摘NO
                        指摘NO = (int?)row.Cells["NoTextBoxColumn"]?.Value > 0 ? (int?)row.Cells["NoTextBoxColumn"]?.Value : (int?)null,
                        // FLAG_CLOSE
                        FLAG_CLOSE = (int?)row.Cells["StatusComboBoxColumn"]?.Value == 1 ? 1 : (int?)null,
                        // 指摘部品
                        部品 = (string)row.Cells["PartsTextBoxColumn"]?.Value,
                        // 状況
                        状況 = (string)row.Cells["SituationTextBoxColumn"]?.Value,
                        // FLAG_処置不要
                        FLAG_処置不要 = (int?)row.Cells["TreatmentCheckBoxColumn"]?.Value == 1 ? 1 : (int?)null,
                        // 処置課
                        処置課 = (string)row.Cells["TreatmentSectionTextBoxColumn"]?.Value,
                        // 処置対象
                        処置対象 = (string)row.Cells["TreatmentTargetTextBoxColumn"]?.Value,
                        // 処置方法
                        処置方法 = (string)row.Cells["TreatmentHowTextBoxColumn"]?.Value,
                        // FLAG_調整済
                        FLAG_調整済 = (int?)row.Cells["TreatmentOKCheckBoxColumn"]?.Value == 1 ? 1 : (int?)null,
                        // 処置調整
                        処置調整 = (string)row.Cells["TreatmentWhoTextBoxColumn"]?.Value,
                        // 織込日程
                        織込日程 = (DateTime?)row.Cells["TreatmentWhenCalendarColumn"]?.Value,
                        // FLAG_試作改修
                        FLAG_試作改修 = (int?)row.Cells["RepairCheckBoxColumn"]?.Value == 1 ? 1 : (int?)null,
                        // 部品納入日
                        部品納入日 = (DateTime?)row.Cells["PartsGetDayCalendarColumn"]?.Value,
                        // FLAG_上司承認
                        FLAG_上司承認 = (int?)row.Cells["ApprovalOKCheckBoxColumn"]?.Value == 1 ? 1 : (int?)null,
                        // 担当課名
                        担当課名 = (string)row.Cells["SectionTextBoxColumn"]?.Value,
                        // 担当課_ID
                        担当課_ID = null,
                        // 担当者_ID
                        担当者_ID = Factory.StaffController.GetId(row),
                        // 担当者_TEL
                        担当者_TEL = (string)row.Cells["UserTelTextBoxColumn"]?.Value,
                        // 編集者_ID
                        編集者_ID = SessionDto.UserId,
                    };

                    // 登録データの追加
                    pointList.Add(data);
                }

                // 対象車両IDがある場合
                if (row.Cells["CarIDTextBoxColumn"].Value != null)
                {
                    // 状況の追加
                    data.PROGRESS_LIST.Add(new DesignCheckProgressPostInModel()
                    {
                        // 開催日ID
                        開催日_ID = (int)this.DesignCheck.ID,
                        // 対象車両ID
                        対象車両_ID = (int)row.Cells["CarIDTextBoxColumn"].Value,
                        // 指摘ID
                        指摘_ID = (int)row.Cells["IDTextBoxColumn"].Value,
                        // 状況
                        状況 = (string)row.Cells["ProgressComboBoxColumn"].Value,
                        // 完了確認日
                        完了確認日 = (DateTime?)row.Cells["EndDayCalendarColumn"].Value,
                        // 部品納入日
                        部品納入日 = (DateTime?)row.Cells["PartsGetDayCalendarColumn"].Value
                    });
                }

                // 指摘IDの退避
                pointId = (int)row.Cells["IDTextBoxColumn"].Value;
            }

            return pointList;
        }
        #endregion

        #region 設計チェック指摘一覧のチェック
        /// <summary>
        /// 設計チェック指摘一覧のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsEntryDesignCheckPoint()
        {
            //編集していなければ終了
            if (this.IsEdit == false)
            {
                return false;
            }

            //登録対象が無ければ終了
            if (this.PointMultiRow.RowCount <= 0)
            {
                return false;
            }

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }

            //設計チェックが存在しているかどうか
            if (this.GetDesignCheckList(new DesignCheckGetInModel { ID = this.ListSearchCond.開催日_ID }) == null)
            {
                //存在していない場合はエラー
                Messenger.Warn(Resources.KKM00021);
                return false;
            }

            return true;

        }
        #endregion

        #region 全チェックON/OFF処理
        /// <summary>
        /// 全チェックON/OFF処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                PointMultiRow.CurrentCell = null;

                // OPEN行のみチェック
                foreach (var row in PointMultiRow.Rows.Where((x) => Convert.ToInt32(x.Cells["StatusComboBoxColumn"].Value) != MultiRowContoller.StatusClose).ToList())
                {
                    row.Cells["TargetCheckBoxColumn"].Value = CheckBoxAll.Checked;
                }
            });
        }
        #endregion

        #region 対象車追加ボタン押下
        /// <summary>
        /// 対象車追加ボタン押下
        /// </summary>
        private void CarAddButton_Click(object sender, EventArgs e)
        {
            var list = new List<DesignCheckPointGetOutModel>();

            foreach (var dgr in this.PointMultiRow.Rows)
            {
                var chk = dgr.Cells["TargetCheckBoxColumn"];

                // チェックONのチェックボックス行のみを追加対象とする
                if (chk.Visible == true && chk.Value != null && Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(Factory.MultiRowContoller.GetModel(dgr.Index));
                }
            }

            // 未選択の場合はエラー
            if (list.Count() <= 0)
            {
                Messenger.Warn(Resources.KKM00009);

                return;
            }

            // 複数選択の場合はエラー
            if (list.Count() > 1)
            {
                Messenger.Warn(Resources.KKM00019);

                return;
            }

            // 変更登録確認
            if (Entry())
            {
                return;
            }

            using (var form = new PointingTargetCarAddForm() { DesignCheckPoint = list.FirstOrDefault(), UserAuthority = Factory.Authority.UserAuthorityModel })
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // 再描画
                    FormControlUtil.FormWait(this, this.SetDesignCheckPointList);
                }
            }
        }
        #endregion

        #region 指摘追加ボタン押下
        /// <summary>
        /// 指摘追加ボタン押下
        /// </summary>
        private void RowAddButton_Click(object sender, EventArgs e)
        {
            // 変更登録確認
            if (Entry())
            {
                return;
            }

            // 新規行に対象車を追加
            if (this.Factory.PointAddDelController.IsAddTargetCar(Factory.Authority.UserAuthorityModel))
            {
                FormControlUtil.FormWait(this, () =>
                {
                    // 再描画
                    this.SetDesignCheckPointList();

                    // 追加行を選択状態に設定
                    var newRowIndex = Factory.MultiRowContoller.GetLastPointRowIndex();
                    this.Factory.MultiRowContoller.SelectRow(newRowIndex);
                    this.Factory.MultiRowContoller.ScrollRow(newRowIndex);

                    // コピーの確認
                    if (this.Factory.PointAddDelController.IsCopy())
                    {
                        // 編集フラグ
                        this.IsEdit = true;
                    }
                });

                // 再読込フラグ
                this.IsRefresh = true;
            }
        }
        #endregion

        #region 指摘削除ボタン押下
        /// <summary>
        /// 指摘削除ボタン押下
        /// </summary>
        private void RowDeleteButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                if (Factory.PointAddDelController.Del())
                {
                    // DB削除により行がなくなった場合
                    if (this.PointMultiRow.RowCount <= 0)
                    {
                        // 編集中フラグ
                        this.IsEdit = false;
                    }

                    // 再読込フラグ
                    this.IsRefresh = true;

                    // 試験車名のコンボボックス設定
                    this.SetTestCarNameComboBox();

                    // 状況のコンボボックス設定
                    this.SetProgSymbolComboBox();
                }
            });
        }
        #endregion

        private void AutoFitLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 行高さ調整
            this.Factory.MultiRowContoller.AdjustmentRowsVertical();
        }

        #region 状況記号（設計チェック）取得クラス
        /// <summary>
        /// 状況記号（設計チェック）取得クラス
        /// </summary>
        private class ProgressSymbol
        {
            /// <summary>
            /// 状況記号（設計チェック）取得
            /// </summary>
            /// <returns></returns>
            public List<DesignCheckProgressSymbolGetOutModel> Get()
            {
                var ret = new List<DesignCheckProgressSymbolGetOutModel>();

                var res = HttpUtil.GetResponse<DesignCheckProgressSymbolGetInModel, DesignCheckProgressSymbolGetOutModel>
                            (ControllerType.DesignCheckProgressSymbol, new DesignCheckProgressSymbolGetInModel());

                if (res != null && res.Status == Const.StatusSuccess)
                {
                    ret = res.Results.ToList();
                }

                return ret;
            }

            /// <summary>
            /// 状況記号（設計チェック）取得
            /// </summary>
            /// <returns></returns>
            public List<DesignCheckProgressSymbolGetOutModel> GetFormatedDescription()
            {
                var list = Get();
                list.FindAll((x) => x.ID != 0).ForEach((x) => x.説明 = string.Format("     {0}        ： {1}", x.記号, x.説明));

                return list;
            }
        }
        #endregion

        #region MultiRowを制御するクラス
        /// <summary>
        /// MultiRowを制御するクラス
        /// </summary>
        private class MultiRowContoller : BaseMultiRowContoller
        {
            #region 内部変数

            private GcMultiRow _mlr;
            private List<DesignCheckPointGetOutModel> _ModelList;
            private Authority _Auth;

            /// <summary>
            /// 内部保持用設定済テンプレート
            /// </summary>
            private Template _Tmp;

            /// <summary>
            /// カスタムテンプレート
            /// </summary>
            private CustomTemplate CustomTemplate = new CustomTemplate();

            /// <summary>
            /// カスタムテンプレートセルスタイル
            /// </summary>
            private static CustomMultiRowCellStyle customMultiRowCellStyle = new CustomMultiRowCellStyle();

            #region 列情報
            /// <summary>
            /// 列情報
            /// </summary>
            private List<ColInfo> ColInfos = new List<ColInfo>()
            {
                // 選択チェック
                new ColInfo() { Index = 0, RowCellName = "TargetCheckBoxColumn", HiddenCellName = "HiddenTarget" , Visible = true, HeaderName = "ch_Target" },

                // 指摘No
                new ColInfo() { Index = 1, RowCellName = "NoTextBoxColumn", HiddenCellName = "HiddenNo" , Visible = true, HeaderName = "ch_No" },

                //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
                // 試作管理No
                new ColInfo() { Index = 2, RowCellName = "MgrNoTextBoxColumn", HiddenCellName = "HiddenMgrNo" , Visible = true, HeaderName = "ch_MgrNo" },
                //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修
           
                //Delete Start 2021/06/29 張晋華 開発計画表設計チェック機能改修
                //// ステータス
                //new ColInfo() { Index = 2, RowCellName = "StatusComboBoxColumn", HiddenCellName = "HiddenStatus" , Visible = true, HeaderName = "ch_Status" },

                //// 履歴
                //new ColInfo() { Index = 3, RowCellName = "HistoryLinkColumn", HiddenCellName = "HiddenHistory" , Visible = true, HeaderName = "ch_History" },

                //// 指摘部品
                //new ColInfo() { Index = 4, RowCellName = "PartsTextBoxColumn", HiddenCellName = "HiddenParts" , Visible = true, HeaderName = "ch_Parts" },

                //// 状況
                //new ColInfo() { Index = 5, RowCellName = "SituationTextBoxColumn", HiddenCellName = "HiddenSituation" , Visible = true, HeaderName = "ch_Situation" },

                //// 処置しないチェック
                //new ColInfo() { Index = 6, RowCellName = "TreatmentCheckBoxColumn", HiddenCellName = "HiddenTreatment" , Visible = true, HeaderName = "ch_Treatment" },

                //// [処置内容]どこの部署が?
                //new ColInfo() { Index = 7, RowCellName = "TreatmentSectionTextBoxColumn", HiddenCellName = "HiddenTreatmentSection" , Visible = true, HeaderName = "ch_TreatmentSection" },

                //// [処置内容]何を
                //new ColInfo() { Index = 8, RowCellName = "TreatmentTargetTextBoxColumn", HiddenCellName = "HiddenTreatmentTarget" , Visible = true, HeaderName = "ch_TreatmentTarget" },

                //// [処置内容]どのように
                //new ColInfo() { Index = 9, RowCellName = "TreatmentHowTextBoxColumn", HiddenCellName = "HiddenTreatmentHow" , Visible = true, HeaderName = "ch_TreatmentHow" },

                //// [処置内容]調整:済
                //new ColInfo() { Index = 10, RowCellName = "TreatmentOKCheckBoxColumn", HiddenCellName = "HiddenTreatmentOK" , Visible = true, HeaderName = "ch_TreatmentOK" },

                //// [処置内容]誰と
                //new ColInfo() { Index = 11, RowCellName = "TreatmentWhoTextBoxColumn", HiddenCellName = "HiddenTreatmentWho" , Visible = true, HeaderName = "ch_TreatmentWho" },

                //// [処置内容]いつまでに
                //new ColInfo() { Index = 12, RowCellName = "TreatmentWhenCalendarColumn", HiddenCellName = "HiddenTreatmentWhen" , Visible = true, HeaderName = "ch_TreatmentWhen" },

                //// 要試作改修
                //new ColInfo() { Index = 13, RowCellName = "RepairCheckBoxColumn", HiddenCellName = "HiddenRepair" , Visible = true, HeaderName = "ch_Repair" },

                //// 部品納入日
                //new ColInfo() { Index = 14, RowCellName = "PartsGetDayCalendarColumn", HiddenCellName = "HiddenPartsGetDay" , Visible = true, HeaderName = "ch_PartsGetDay" },

                //// 完了確認日
                //new ColInfo() { Index = 15, RowCellName = "EndDayCalendarColumn", HiddenCellName = "" , Visible = true, IsNonFilter = true, HeaderName = "ch_EndDay" },

                //// 担当課長承認
                //new ColInfo() { Index = 16, RowCellName = "ApprovalOKCheckBoxColumn", HiddenCellName = "HiddenApprovalOK" , Visible = true, HeaderName = "ch_ApprovalOK" },

                //// 試験車名
                //new ColInfo() { Index = 17, RowCellName = "TestCarNameTextBoxColumn", HiddenCellName = "", Visible = true, IsNonFilter = true, HeaderName = "ch_TestCarName" },

                //// 設計チェック時状況
                //new ColInfo() { Index = 18, RowCellName = "ProgressComboBoxColumn", HiddenCellName = "", Visible = true, IsNonFilter = true, HeaderName = "ch_Progress" },

                //// 担当課
                //new ColInfo() { Index = 19, RowCellName = "SectionTextBoxColumn", HiddenCellName = "HiddenSection" , Visible = true, HeaderName = "ch_Section" },

                //// 担当者
                //new ColInfo() { Index = 20, RowCellName = "UserTextBoxColumn", HiddenCellName = "HiddenUser" , Visible = true, HeaderName = "ch_User" },

                //// 担当者TEL
                //new ColInfo() { Index = 21, RowCellName = "UserTelTextBoxColumn", HiddenCellName = "HiddenUserTel" , Visible = true, HeaderName = "ch_UserTel" },

                //// 編集日
                //new ColInfo() { Index = 22, RowCellName = "EditDateTextColumn", HiddenCellName = "HiddenEditDate" , Visible = true, HeaderName = "ch_EditDate" },

                //// 編集者
                //new ColInfo() { Index = 23, RowCellName = "EditUserNameTextBoxColumn", HiddenCellName = "HiddenEditUserName" , Visible = true, HeaderName = "ch_EditUserName" },

                //// 以下非表示列

                //// 開催日_ID
                //new ColInfo() { Index = 24, RowCellName = "OpenDayIDColumn", HiddenCellName = "" , Visible = false },

                //// 担当者_ID
                //new ColInfo() { Index = 25, RowCellName = "UserIDTextBoxColumn", HiddenCellName = "" , Visible = false },

                //// 履歴数
                //new ColInfo() { Index = 26, RowCellName = "HistoryCountColumn", HiddenCellName = "" , Visible = false },

                //// 指摘ID
                //new ColInfo() { Index = 27, RowCellName = "IDTextBoxColumn", HiddenCellName = "" , Visible = false },

                //// 対象車両ID
                //new ColInfo() { Index = 28, RowCellName = "CarIDTextBoxColumn", HiddenCellName = "" , Visible = false },
                //Delete End 2021/06/29 張晋華 開発計画表設計チェック機能改修

                //Update Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
                // ステータス
                new ColInfo() { Index = 3, RowCellName = "StatusComboBoxColumn", HiddenCellName = "HiddenStatus" , Visible = true, HeaderName = "ch_Status" },

                //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
                // 試作ステータス
                new ColInfo() { Index = 4, RowCellName = "MgrStatusComboBoxColumn", HiddenCellName = "HiddenMgrStatus" , Visible = true, HeaderName = "ch_MgrStatus" },
                //Append End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)

                // 履歴
                new ColInfo() { Index = 5, RowCellName = "HistoryLinkColumn", HiddenCellName = "HiddenHistory" , Visible = true, HeaderName = "ch_History" },

                // 指摘部品
                new ColInfo() { Index = 6, RowCellName = "PartsTextBoxColumn", HiddenCellName = "HiddenParts" , Visible = true, HeaderName = "ch_Parts" },

                // 状況
                new ColInfo() { Index = 7, RowCellName = "SituationTextBoxColumn", HiddenCellName = "HiddenSituation" , Visible = true, HeaderName = "ch_Situation" },

                // 処置しないチェック
                new ColInfo() { Index = 8, RowCellName = "TreatmentCheckBoxColumn", HiddenCellName = "HiddenTreatment" , Visible = true, HeaderName = "ch_Treatment" },

                // [処置内容]どこの部署が?
                new ColInfo() { Index = 9, RowCellName = "TreatmentSectionTextBoxColumn", HiddenCellName = "HiddenTreatmentSection" , Visible = true, HeaderName = "ch_TreatmentSection" },

                // [処置内容]何を
                new ColInfo() { Index = 10, RowCellName = "TreatmentTargetTextBoxColumn", HiddenCellName = "HiddenTreatmentTarget" , Visible = true, HeaderName = "ch_TreatmentTarget" },

                // [処置内容]どのように
                new ColInfo() { Index = 11, RowCellName = "TreatmentHowTextBoxColumn", HiddenCellName = "HiddenTreatmentHow" , Visible = true, HeaderName = "ch_TreatmentHow" },

                // [処置内容]調整:済
                new ColInfo() { Index = 12, RowCellName = "TreatmentOKCheckBoxColumn", HiddenCellName = "HiddenTreatmentOK" , Visible = true, HeaderName = "ch_TreatmentOK" },

                // [処置内容]誰と
                new ColInfo() { Index = 13, RowCellName = "TreatmentWhoTextBoxColumn", HiddenCellName = "HiddenTreatmentWho" , Visible = true, HeaderName = "ch_TreatmentWho" },

                // [処置内容]いつまでに
                new ColInfo() { Index = 14, RowCellName = "TreatmentWhenCalendarColumn", HiddenCellName = "HiddenTreatmentWhen" , Visible = true, HeaderName = "ch_TreatmentWhen" },

                // 要試作改修
                new ColInfo() { Index = 15, RowCellName = "RepairCheckBoxColumn", HiddenCellName = "HiddenRepair" , Visible = true, HeaderName = "ch_Repair" },

                // 部品納入日
                new ColInfo() { Index = 16, RowCellName = "PartsGetDayCalendarColumn", HiddenCellName = "HiddenPartsGetDay" , Visible = true, HeaderName = "ch_PartsGetDay" },

                // 完了確認日
                new ColInfo() { Index = 17, RowCellName = "EndDayCalendarColumn", HiddenCellName = "" , Visible = true, IsNonFilter = true, HeaderName = "ch_EndDay" },

                // 担当課長承認
                new ColInfo() { Index = 18, RowCellName = "ApprovalOKCheckBoxColumn", HiddenCellName = "HiddenApprovalOK" , Visible = true, HeaderName = "ch_ApprovalOK" },

                // 試験車名
                new ColInfo() { Index = 19, RowCellName = "TestCarNameTextBoxColumn", HiddenCellName = "", Visible = true, IsNonFilter = true, HeaderName = "ch_TestCarName" },

                // 設計チェック時状況
                new ColInfo() { Index = 20, RowCellName = "ProgressComboBoxColumn", HiddenCellName = "", Visible = true, IsNonFilter = true, HeaderName = "ch_Progress" },

                // 担当課
                new ColInfo() { Index = 21, RowCellName = "SectionTextBoxColumn", HiddenCellName = "HiddenSection" , Visible = true, HeaderName = "ch_Section" },

                // 担当者
                new ColInfo() { Index = 22, RowCellName = "UserTextBoxColumn", HiddenCellName = "HiddenUser" , Visible = true, HeaderName = "ch_User" },

                // 担当者TEL
                new ColInfo() { Index = 23, RowCellName = "UserTelTextBoxColumn", HiddenCellName = "HiddenUserTel" , Visible = true, HeaderName = "ch_UserTel" },

                // 編集日
                new ColInfo() { Index = 24, RowCellName = "EditDateTextColumn", HiddenCellName = "HiddenEditDate" , Visible = true, HeaderName = "ch_EditDate" },

                // 編集者
                new ColInfo() { Index = 25, RowCellName = "EditUserNameTextBoxColumn", HiddenCellName = "HiddenEditUserName" , Visible = true, HeaderName = "ch_EditUserName" },

                // 以下非表示列

                // 開催日_ID
                new ColInfo() { Index = 26, RowCellName = "OpenDayIDColumn", HiddenCellName = "" , Visible = false },

                // 担当者_ID
                new ColInfo() { Index = 27, RowCellName = "UserIDTextBoxColumn", HiddenCellName = "" , Visible = false },

                // 履歴数
                new ColInfo() { Index = 28, RowCellName = "HistoryCountColumn", HiddenCellName = "" , Visible = false },

                // 指摘ID
                new ColInfo() { Index = 29, RowCellName = "IDTextBoxColumn", HiddenCellName = "" , Visible = false },

                // 対象車両ID
                new ColInfo() { Index = 30, RowCellName = "CarIDTextBoxColumn", HiddenCellName = "" , Visible = false },
                //Update End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
            };
            #endregion

            #endregion

            #region 公開プロパティ

            /// <summary>
            /// データソースの元になる内部保持の設計チェック指摘モデルリスト
            /// </summary>
            public List<DesignCheckPointGetOutModel> ModelList
            {
                get
                {
                    return this._ModelList;
                }

                set
                {
                    this._ModelList = value;
                }
            }

            /// <summary>
            /// 列情報
            /// </summary>
            public List<ColInfo> ColInfoList { get { return this.ColInfos; } }

            /// <summary>
            /// オープン値
            /// </summary>
            public const int StatusOpen = 0;

            /// <summary>
            /// クローズ値
            /// </summary>
            public const int StatusClose = 1;

            /// <summary>
            /// ステータス値と表示文言の組み合わせ
            /// </summary>
            public readonly Dictionary<int?, string> StatusMap = new Dictionary<int?, string>
            {
                { StatusOpen, "OPEN" },
                { StatusClose, "CLOSE" }
            };

            /// <summary>
            /// ステータス値と表示文言の組み合わせ
            /// </summary>
            public readonly Dictionary<int?, string> StatusMap2 = new Dictionary<int?, string>
            {
                { StatusOpen, "未" },
                { StatusClose, "済" }
            };

            #endregion

            #region コンストラクタ
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="mlr">MultiRow</param>
            /// <param name="rowCountLabel">行カウント表示ラベル</param>
            /// <param name="auth">権限管理クラス</param>
            public MultiRowContoller(GcMultiRow mlr, Label rowCountLabel, Authority auth) : base(mlr)
            {
                _mlr = mlr;
                _ModelList = new List<DesignCheckPointGetOutModel>();
                _Auth = auth;

                var scl = new DeviceUtil().GetScalingFactor();
                var tmp = new DesignCheckPointListMultiRowTemplete();

                // ステータス
                var status = tmp.Row.Cells["StatusComboBoxColumn"] as ComboBoxCell;
                status.ValueMember = "Key";
                status.DisplayMember = "Value";
                status.DataSource = this.StatusMap.ToArray();

                //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
                // 試作部ステータス
                var Mgrstatus = tmp.Row.Cells["MgrStatusComboBoxColumn"] as ComboBoxCell;
                Mgrstatus.ValueMember = "Key";
                Mgrstatus.DisplayMember = "Value";
                Mgrstatus.DataSource = this.StatusMap2.ToArray();
                //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)

                // 設計チェック状況
                var prog = tmp.Row.Cells["ProgressComboBoxColumn"] as ComboBoxCell;
                prog.ValueMember = "記号";
                prog.DisplayMember = "説明";
                prog.DataSource = new ProgressSymbol().GetFormatedDescription();
                prog.DropDownWidth = Convert.ToInt32(330 * scl);

                // テンプレート設定
                this.CustomTemplate.ColumnHeaderHeight = 70;
                this.CustomTemplate.RowCountLabel = rowCountLabel;
                this.CustomTemplate.MultiRow = this._mlr;
                this._mlr.Template = this.CustomTemplate.SetContextMenuTemplate(tmp);

                foreach (var cell in this._mlr.Template.ColumnHeaders[0].Cells)
                {
                    // フィルタ消し
                    if (this.ColInfos.Where((y) => y.IsNonFilter == true).Any((y) => y.HeaderName == cell.Name))
                    {
                        var hCell = cell as ColumnHeaderCell;

                        hCell.DropDownContextMenuStrip = null;
                    }

                    // フィルタテキスト読み取り専用
                    if (cell is FilteringTextBoxCell)
                    {
                        var fCell = cell as FilteringTextBoxCell;

                        if (this.ColInfos.Where((y) => y.IsNonFilter == true).Any((y) => y.RowCellName == fCell.FilteringCellName))
                        {
                            fCell.ReadOnly = true;
                            fCell.Style.BackColor = Color.WhiteSmoke;
                        }
                    }
                }

                // 隠しセル
                foreach (var info in this.ColInfos.FindAll((x) => string.IsNullOrEmpty(x.HiddenCellName) == false))
                {
                    this._mlr.Template.Row.Cells[info.HiddenCellName].Size = this._mlr.Template.Row.Cells[info.RowCellName].Size;
                    this._mlr.Template.Row.Cells[info.HiddenCellName].Location = this._mlr.Template.Row.Cells[info.RowCellName].Location;
                    this._mlr.Template.Row.Cells[info.HiddenCellName].Style.Font = this._mlr.Template.Row.Cells[info.RowCellName].Style.Font;
                    this._mlr.Template.Row.Cells[info.HiddenCellName].Style.BackColor = this._mlr.Template.Row.Cells[info.RowCellName].Style.BackColor;
                    this._mlr.Template.Row.Cells[info.HiddenCellName].Style.ForeColor = this._mlr.Template.Row.Cells[info.RowCellName].Style.ForeColor;
                }

                // 選択列のフィルターアイテム設定
                var cellCheck = _mlr.Template.ColumnHeaders[0].Cells["ch_Target"] as ColumnHeaderCell;
                cellCheck.DropDownContextMenuStrip.Items.RemoveAt(cellCheck.DropDownContextMenuStrip.Items.Count - 1);
                cellCheck.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // フィルターアイテム設定：処置しない
                var cellTreatment = _mlr.Template.ColumnHeaders[0].Cells["ch_Treatment"] as ColumnHeaderCell;
                cellTreatment.DropDownContextMenuStrip.Items.RemoveAt(cellTreatment.DropDownContextMenuStrip.Items.Count - 1);
                cellTreatment.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("処置しない", "処置する") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // フィルターアイテム設定：調整：済
                var cellTreatmentOK = _mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentOK"] as ColumnHeaderCell;
                cellTreatmentOK.DropDownContextMenuStrip.Items.RemoveAt(cellTreatmentOK.DropDownContextMenuStrip.Items.Count - 1);
                cellTreatmentOK.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("済", "未") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // フィルターアイテム設定：要試作改修
                var cellRepair = _mlr.Template.ColumnHeaders[0].Cells["ch_Repair"] as ColumnHeaderCell;
                cellRepair.DropDownContextMenuStrip.Items.RemoveAt(cellRepair.DropDownContextMenuStrip.Items.Count - 1);
                cellRepair.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("要", "否") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // フィルターアイテム設定：担当課長承認
                var cellApprovalOK = _mlr.Template.ColumnHeaders[0].Cells["ch_ApprovalOK"] as ColumnHeaderCell;
                cellApprovalOK.DropDownContextMenuStrip.Items.RemoveAt(cellApprovalOK.DropDownContextMenuStrip.Items.Count - 1);
                cellApprovalOK.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("済", "未") { MaxCount = CustomTemplate.FilterItemMaxCount });

                Color correspondenceColor = Color.Khaki;
                Color otherColor = Color.LightGray;
                Color foreColor = Color.Black;

                // 処置内容列
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentSection"].Style.BackColor = otherColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentSection"].Style.ForeColor = foreColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentTarget"].Style.BackColor = otherColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentTarget"].Style.ForeColor = foreColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentHow"].Style.BackColor = otherColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentHow"].Style.ForeColor = foreColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentOK"].Style.BackColor = otherColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentOK"].Style.ForeColor = foreColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentWho"].Style.BackColor = otherColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentWho"].Style.ForeColor = foreColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentWhen"].Style.BackColor = otherColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentWhen"].Style.ForeColor = foreColor;

                // 試験車列
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TestCarName"].Style.BackColor = correspondenceColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_TestCarName"].Style.ForeColor = foreColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_Progress"].Style.BackColor = correspondenceColor;
                this._mlr.Template.ColumnHeaders[0].Cells["ch_Progress"].Style.ForeColor = foreColor;

                // 設定テンプレートの保持
                this._Tmp = this._mlr.Template;

                // ダブルバッファリング有効
                this._mlr.GetType().InvokeMember(
                   "DoubleBuffered",
                   System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                   null,
                   this._mlr,
                   new object[] { true });

                // コンボボックスセルのテキスト編集不可処理
                _mlr.EditingControlShowing += new EventHandler<EditingControlShowingEventArgs>(_mlr_EditingControlShowing);
            }
            #endregion

            #region コンボボックスセルのテキスト編集不可処理

            private void _mlr_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
            {
                // コンボボックス型セルのイベント設定
                if (e.Control is ComboBoxEditingControl)
                {
                    e.Control.KeyDown -= new KeyEventHandler(EditingControl_KeyDown);
                    e.Control.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
                    e.Control.KeyPress -= new KeyPressEventHandler(EditingControl_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(EditingControl_KeyPress);
                }
            }

            private void EditingControl_KeyDown(object sender, KeyEventArgs e)
            {
                // コンボボックス型セルでDeleteキーを禁止する
                if (e.KeyCode == Keys.Delete)
                {
                    e.Handled = true;
                }
            }

            private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
            {
                // コンボボックス型セルで文字入力を禁止する
                e.Handled = true;
            }

            #endregion

            #region 未選択状態にします。
            /// <summary>
            /// 未選択状態にします。
            /// </summary>
            public void Unselected()
            {
                this._mlr.CurrentCell = null;
            }
            #endregion

            #region 行を選択状態にします。
            /// <summary>
            /// 行を選択状態にします。
            /// </summary>
            /// <param name="index"></param>
            public void SelectRow(int index)
            {
                this._mlr.CurrentCell = null;
                this._mlr.Rows[index].Selected = true;
            }
            #endregion

            #region 最終指摘行Indexを取得します。
            /// <summary>
            /// 最終指摘行Indexを取得します。
            /// </summary>
            public int GetLastPointRowIndex()
            {
                for (var i = 1; i < _mlr.RowCount; i++)
                {
                    if (IsFirstPointRow(_mlr.RowCount - i))
                    {
                        return _mlr.RowCount - i;
                    }
                }

                return 0;
            }
            #endregion

            #region 最初の行までスクロールします。
            /// <summary>
            /// 最初の行までスクロールします。
            /// </summary>
            public void ScrollFirstRow()
            {
                if (this._mlr.RowCount > 0)
                {
                    CellPosition newPosition = new CellPosition(0, "RowHeader");

                    this._mlr.FirstDisplayedCellPosition = newPosition;
                }
            }
            #endregion

            #region 対象行までスクロールします。
            /// <summary>
            /// 対象行までスクロールします。
            /// </summary>
            public void ScrollRow(int index)
            {
                if (this._mlr.RowCount > 0)
                {
                    CellPosition newPosition = new CellPosition(index, this._mlr.FirstDisplayedCellPosition.CellIndex);

                    this._mlr.FirstDisplayedCellPosition = newPosition;
                }
            }
            #endregion

            #region データソースにバインドします。
            /// <summary>
            /// データソースにバインドします。
            /// </summary>
            public void Bind()
            {
                // 表示位置取得
                SaveScrollPoint();

                // 初期化
                this._mlr.Template = this._Tmp;

                var target = new List<DesignCheckPointGetOutModel>();

                foreach (var data in this._ModelList)
                {
                    // 削除行はバインド対象外
                    if (data.DELETE_FLG == true) continue;

                    data.FLAG_CLOSE = data.FLAG_CLOSE == 1 ? 1 : 0;

                    target.Add(data);
                }

                // データバインド
                this.CustomTemplate.SetDataSource(target);

                // セル結合処理
                foreach (var row in this._mlr.Rows)
                {
                    this.ColInfos

                        // マージ対象列
                        .FindAll((x) => string.IsNullOrEmpty(x.HiddenCellName) == false)

                        .ForEach((x) =>
                        {
                            // 各IDの先頭行のみ表示
                            row.Cells[x.RowCellName].Visible = IsFirstPointRow(row.Index);

                            // 各IDの先頭行のみ下罫線消す
                            row.Cells[x.RowCellName].Style.Border = GetBorder(row.Index);

                            // 各IDの先頭行のみ非表示
                            row.Cells[x.HiddenCellName].Visible = !IsFirstPointRow(row.Index);

                            // 隠しセル選択不可
                            row.Cells[x.HiddenCellName].Selectable = false;
                        });

                    // 行の高さ調整
                    row.Cells["SituationTextBoxColumn"].PerformVerticalAutoFit();
                }

                // 一覧を未選択状態に設定
                this._mlr.CurrentCell = null;

                // グリッドリフレッシュ
                this._mlr.Refresh();

                //Append Start 2021/07/29 杉浦 設計チェックインポート
                //グリッドの表示周りの編集
                AdjustDesignCheckPointList();
                //Append End 2021/07/29 杉浦 設計チェックインポート

                // 元の表示位置までスクロール
                LoadScrollPoint();
            }
            #endregion

            #region チェックボックスで選択している行に対応する設計チェック指摘モデルリストを取得します。
            /// <summary>
            /// チェックボックスで選択している行に対応する設計チェック指摘モデルリストを取得します。
            /// </summary>
            /// <returns></returns>
            public List<DesignCheckPointGetOutModel> GetSelectedModels()
            {
                var ret = new List<DesignCheckPointGetOutModel>();

                foreach (var dgr in this._mlr.Rows)
                {
                    var chk = dgr.Cells["TargetCheckBoxColumn"];

                    // チェックONのチェックボックス行
                    if (chk.Visible == true && chk.Value != null && Convert.ToBoolean(chk.Value) == true)
                    {
                        ret.Add(GetModel(dgr.Index));
                    }
                }

                return ret;
            }
            #endregion

            #region 行に対応する設計チェック指摘モデルを取得します。
            /// <summary>
            /// 行に対応する設計チェック指摘モデルを取得します。
            /// </summary>
            /// <param name="rowIndex">行番号</param>
            /// <returns></returns>
            public DesignCheckPointGetOutModel GetModel(int rowIndex)
            {
                return this.GetModel(this._mlr.Rows[rowIndex]);
            }

            /// <summary>
            /// 行に対応する設計チェック指摘モデルを取得します。
            /// </summary>
            /// <param name="row">行</param>
            /// <returns></returns>
            public DesignCheckPointGetOutModel GetModel(Row row)
            {
                var id = Convert.ToInt32(row.Cells["IDTextBoxColumn"].Value);

                return this._ModelList.First(x => x.ID == id);

            }
            #endregion

            #region 再描画します。
            /// <summary>
            /// バインドされているソースを元に再描画します。
            /// </summary>
            public void Refresh()
            {
                this._mlr.Refresh();
            }
            #endregion

            #region 該当行がその指摘Noの最初の行か？
            /// <summary>
            /// 該当行がその指摘Noの最初の行か？
            /// </summary>
            /// <param name="rowIndex"></param>
            /// <returns></returns>
            public bool IsFirstPointRow(int rowIndex)
            {
                if (rowIndex == 0)
                {
                    return true;
                }

                var now = this._mlr.Rows[rowIndex].Cells["IDTextBoxColumn"].Value;
                var bef = this._mlr.Rows[rowIndex - 1].Cells["IDTextBoxColumn"].Value;

                return Convert.ToString(now) != Convert.ToString(bef);
            }
            #endregion

            #region 行の編集許可制御を設定します。

            /// <summary>
            /// その行の編集許可制御を設定します。
            /// </summary>
            public void SetRowEnable(Row row)
            {
                foreach (var col in this.ColInfos.Where(x => x.RowCellName != "StatusComboBoxColumn"))
                {
                    row.Cells[col.RowCellName].ReadOnly =
                        IsReadOnly(
                            Convert.ToInt32(row.Cells["StatusComboBoxColumn"].Value),
                            col.RowCellName,
                            //Update Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
                            //Convert.ToString(row.Cells["SectionTextBoxColumn"].Value));
                            Convert.ToString(row.Cells["SectionTextBoxColumn"].Value),
                            Convert.ToString(row.Cells["MgrNoTextBoxColumn"].Value));
                    //Update End 2021/07/20 杉浦 開発計画表設計チェック機能改修
                }

                // ステータス列は権限のみで制御
                row.Cells["StatusComboBoxColumn"].ReadOnly = _Auth.IsCan("StatusComboBoxColumn", Convert.ToString(row.Cells["SectionTextBoxColumn"].Value)) == false;

                //Append Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
                row.Cells["TargetCheckBoxColumn"].ReadOnly = _Auth.IsCan("TargetCheckBoxColumn", Convert.ToString(row.Cells["TargetCheckBoxColumn"].Value)) == false;
                //Append End 2021/07/20 杉浦 開発計画表設計チェック機能改修

                //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
                row.Cells["MgrNoTextBoxColumn"].ReadOnly = true;
                //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修

                //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
                row.Cells["MgrStatusComboBoxColumn"].ReadOnly = true;
                //Append End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)

                // 対象車両なしの場合は完了確認日・状況編集不可
                if (string.IsNullOrEmpty(Convert.ToString(row.Cells["TestCarNameTextBoxColumn"].Value)))
                {
                    row.Cells["TestCarNameTextBoxColumn"].Style.BackColor = Color.LightGray;
                    row.Cells["ProgressComboBoxColumn"].Style.BackColor = Color.LightGray;

                    row.Cells["EndDayCalendarColumn"].ReadOnly = true;
                    row.Cells["ProgressComboBoxColumn"].ReadOnly = true;
                }
            }

            /// <summary>
            /// ステータス変更による同一指摘行の編集許可制御を設定します。
            /// </summary>
            public void ChangeStatusEnable(Row row)
            {
                var statusValue = Convert.ToInt32(row.Cells["StatusComboBoxColumn"].Value);

                foreach (var rw in this.GetSamePointRows(row))
                {
                    // 背景色
                    SetRowBackColor(rw, statusValue);

                    foreach (var col in this.ColInfos.Where(x => x.RowCellName != "StatusComboBoxColumn"))
                    {
                        //Update Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
                        //rw.Cells[col.RowCellName].ReadOnly = IsReadOnly(statusValue, col.RowCellName, Convert.ToString(rw.Cells["SectionTextBoxColumn"].Value));
                        rw.Cells[col.RowCellName].ReadOnly = IsReadOnly(statusValue, col.RowCellName, Convert.ToString(rw.Cells["SectionTextBoxColumn"].Value), Convert.ToString(row.Cells["MgrNoTextBoxColumn"].Value));
                        //Update End 2021/07/20 杉浦 開発計画表設計チェック機能改修
                    }
                }
            }

            #endregion

            #region その設計チェック指摘モデルに対応する行を取得します。
            /// <summary>
            /// その設計チェック指摘モデルに対応する行を取得します。
            /// </summary>
            /// <param name="model">設計チェック指摘モデル</param>
            /// <returns></returns>
            public Row GetRow(DesignCheckPointGetOutModel model)
            {
                return this._mlr.Rows.Single((x) => Convert.ToInt32(x.Cells["IDTextBoxColumn"].Value) == model.ID && Convert.ToInt32(x.Cells["CarIDTextBoxColumn"].Value) == Convert.ToInt32(model.対象車両_ID));
            }
            #endregion

            #region 最終行を取得します。
            /// <summary>
            /// 最終行を取得します。
            /// </summary>
            /// <returns></returns>
            public Row GetEndRow()
            {
                return _mlr.RowCount > 0 ? _mlr.Rows[_mlr.RowCount - 1] : null;
            }
            #endregion

            #region 行を取得します。
            /// <summary>
            /// 行を取得します。
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public Row GetRow(int index)
            {
                return _mlr.RowCount > 0 ? _mlr.Rows[index] : null;
            }
            #endregion

            #region ステータスによる背景色を行に設定します。
            /// <summary>
            /// ステータスによる背景色を行に設定します。
            /// </summary>
            /// <param name="row"></param>
            /// <param name="statusValue"></param>
            public void SetRowBackColor(Row row, int statusValue)
            {
                var isNoneTestCar = string.IsNullOrEmpty(Convert.ToString(row.Cells["TestCarNameTextBoxColumn"].Value));

                row.Cells
                    .Where((x) => x.Name != "RowHeader").ToList()
                    .ForEach((x) =>
                    {
                        if (x.Name == "TestCarNameTextBoxColumn" || x.Name == "ProgressComboBoxColumn")
                        {
                            x.Style.BackColor = statusValue == StatusClose || isNoneTestCar ? Color.LightGray : customMultiRowCellStyle.DataCellStyle.BackColor;
                        }
                        else
                        {
                            x.Style.BackColor = statusValue == StatusClose ? Color.LightGray : customMultiRowCellStyle.DataCellStyle.BackColor;
                        }
                    });
            }
            #endregion

            //Append Start 2021/07/29 杉浦 設計チェックインポート
            #region 設計チェック指摘一覧調整
            /// <summary>
            /// 設計チェック指摘一覧調整
            /// </summary>
            private void AdjustDesignCheckPointList()
            {
                foreach (var row in this._mlr.Rows)
                {
                    // 編集許可設定
                    SetRowEnable(row);

                    // 履歴が無い場合は履歴はリンクを解除
                    if (Convert.ToInt32(row.Cells["HistoryCountColumn"].Value) <= 0)
                    {
                        row.Cells["HistoryLinkColumn"].Value = NoneText;

                        var linkCell = row.Cells["HistoryLinkColumn"] as LinkLabelCell;
                        linkCell.LinkBehavior = LinkBehavior.NeverUnderline;
                        linkCell.LinkColor = Color.Black;
                    }

                    // 背景色設定
                    SetRowBackColor(row, Convert.ToInt32(row.Cells["StatusComboBoxColumn"].Value));
                }
            }
            #endregion
            //Append End 2021/07/29 杉浦 設計チェックインポート

            //Append Start 2021/08/20 杉浦 設計チェック請負
            #region 設計チェック指摘一覧高さ調整
            /// <summary>
            /// 設計チェック指摘一覧調整
            /// </summary>
            public void AdjustmentRowsVertical()
            {
                foreach (var row in this._mlr.Rows)
                {
                    var lengthList = new List<DesignCheckLengthCompareModel>();
                    lengthList.Add(new DesignCheckLengthCompareModel { name = "TreatmentTargetTextBoxColumn", size = row.Cells["TreatmentTargetTextBoxColumn"].Value.ToString().Length });
                    lengthList.Add(new DesignCheckLengthCompareModel { name = "TreatmentHowTextBoxColumn", size = row.Cells["TreatmentHowTextBoxColumn"].Value.ToString().Length });

                    var maxIdx = lengthList.Select((val, idx) => new { V = val.size, I = idx }).Aggregate((max, working) => (max.V > working.V) ? max : working).I;

                    row.Cells[lengthList[maxIdx].name].PerformVerticalAutoFit();
                }
            }
            #endregion
            //Append End 2021/08/20 杉浦 設計チェック請負

            #region 内部メソッド

            //Update Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
            /// <summary>
            /// ステータスと権限から読み取り専用か判定します。
            /// </summary>
            /// <param name="statusValue"></param>
            /// <param name="cellName"></param>
            /// <param name="sectionName"></param>
            /// <param name="MgrNo"></param>
            /// <returns></returns>
            //private bool IsReadOnly(int statusValue, string cellName, string sectionName)
            //{
            //    // ステータスがクローズならステータス以外の列は編集不可、オープンなら権限に従う
            //    return statusValue == StatusClose ? true : _Auth.IsCan(cellName, sectionName) == false;
            //}
            private bool IsReadOnly(int statusValue, string cellName, string sectionName, string MgrNo)
            {
                // ステータスがクローズならステータス以外の列は編集不可、オープンなら権限に従う
                return !string.IsNullOrEmpty(MgrNo) ? true : statusValue == StatusClose ? true : _Auth.IsCan(cellName, sectionName) == false;
            }
            //Update End 2021/07/20 杉浦 開発計画表設計チェック機能改修

            /// <summary>
            /// 対象行と同じ指摘IDの行を取得します。
            /// </summary>
            /// <param name="row"></param>
            /// <returns></returns>
            private IEnumerable<Row> GetSamePointRows(Row row)
            {
                return this._mlr.Rows.Where((x) => x.Cells["IDTextBoxColumn"].Value.ToString() == row.Cells["IDTextBoxColumn"].Value.ToString());
            }

            /// <summary>
            /// Borderを取得します。
            /// </summary>
            /// <param name="rowIndex"></param>
            /// <returns></returns>
            private Border GetBorder(int rowIndex)
            {
                var border = new Border(
                                new Line(LineStyle.Thin, Color.Silver),
                                new Line(LineStyle.Thin, Color.Silver),
                                new Line(LineStyle.Thin, Color.Silver),
                                new Line(LineStyle.Thin, Color.Silver));

                if (rowIndex == -1 || rowIndex == _mlr.RowCount - 1) return border;

                var now = _mlr.Rows[rowIndex].Cells["IDTextBoxColumn"].Value.ToString();
                var aft = _mlr.Rows[rowIndex + 1].Cells["IDTextBoxColumn"].Value.ToString();
                
                if (now != aft)
                {
                    return border;
                }
                else
                {
                    border.Bottom = Line.Empty;
                    return border;
                }
            }

            #endregion
        }
        #endregion

        #region 列情報クラス
        /// <summary>
        /// 列情報クラス
        /// </summary>
        private class ColInfo
        {
            public int Index { get; set; }

            /// <summary>
            /// その列の行に表示するセル名
            /// </summary>
            public string RowCellName { get; set; }

            /// <summary>
            /// RowCellNameに対応する隠しセル名（セル結合用）
            /// </summary>
            public string HiddenCellName { get; set; }

            /// <summary>
            /// その列を表示するか？
            /// </summary>
            public bool Visible { get; set; }

            /// <summary>
            /// 列ヘッダー名
            /// </summary>
            public string HeaderName { get; set; }

            /// <summary>
            /// 列ヘッダーフィルターを表示しないか？
            /// </summary>
            public bool IsNonFilter { get; set; }
        }
        #endregion

        #region 指摘の追加と削除を制御するクラス
        /// <summary>
        /// 指摘の追加と削除を制御するクラス
        /// </summary>
        private class PointAddDelController
        {
            private MultiRowContoller _gridCon;
            private int _holdDayId;

            #region コンストラクタ
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="gridCon">データグリッドビュー制御クラス</param>
            /// <param name="holdDayId">開催日ID</param>
            public PointAddDelController(MultiRowContoller gridCon, int holdDayId)
            {
                this._gridCon = gridCon;
                this._holdDayId = holdDayId;
            }
            #endregion

            #region 新規行に対象車追加します。
            /// <summary>
            /// 新規行に対象車追加します。
            /// </summary>
            /// <param name="userAuthority"></param>
            /// <returns></returns>
            public bool IsAddTargetCar(UserAuthorityOutModel userAuthority)
            {
                var target = new DesignCheckPointGetOutModel()
                {
                    // 指摘ID
                    ID = DesignCheckPointListForm.TEMP_POINT_ID,

                    // 開催日_ID
                    開催日_ID = this._holdDayId,
                };

                using (var form = new PointingTargetCarAddForm() { DesignCheckPoint = target, UserAuthority = userAuthority })
                {
                    return form.ShowDialog() == DialogResult.OK;
                }
            }
            #endregion

            #region コピーの確認をします。
            /// <summary>
            /// コピーの確認をします。
            /// </summary>
            public bool IsCopy()
            {
                // コピーの確認
                if (Messenger.Confirm(Resources.KKM01017) == DialogResult.Yes)
                {
                    // コピー画面表示
                    using (var form = new DesignCheckCopyForm())
                    {
                        if (form.ShowDialog() != DialogResult.OK)
                        {
                            return false;
                        }

                        var copys = form.Copys;

                        // 貼り付け先の行
                        var row = _gridCon.GetRow(_gridCon.GetLastPointRowIndex());

                        foreach (var copy in copys)
                        {
                            row.Cells[copy.Key].Value = copy.Value;

                            if (copy.Key == "SituationTextBoxColumn")
                            {
                                // 高さ自動調整
                                row.Cells[copy.Key].PerformVerticalAutoFit();
                            }
                        }

                        // 担当者セル処理
                        new StaffController().CopyProcess(row);

                        return true;
                    }
                }

                return false;
            }
            #endregion

            #region 指摘を削除します。
            /// <summary>
            /// 指摘を削除します。
            /// </summary>
            /// <returns>bool</returns>
            public bool Del()
            {
                // 削除対象ID取得
                var targetPointIds = this._gridCon.GetSelectedModels().Where((x) => x?.ID != null).Select((x) => x.ID.Value).ToList();

                // 削除対象があるかどうか
                if (targetPointIds.Any() == false)
                {
                    //選択項目なしメッセージ
                    Messenger.Warn(Resources.KKM00009);
                    return false;
                }

                // 削除可否を問い合わせ
                if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
                {
                    return false;
                }

                // DBから削除実行（正の数がDB登録しているIDのため）
                var dbDelList = targetPointIds.Where((x) => x > 0).Select((x) => new DesignCheckPointDeleteInModel() { ID = x }).ToList();
                if (Delete(dbDelList))
                {
                    // DBから削除はしているが、登録はDBにしていないのでDBから再取得はできないためモデルリストも削除処理する（論理削除なのは他画面から持ってきた名残らしい）
                    targetPointIds.ForEach(x => this._gridCon.ModelList.Where(y => y.ID == x).ToList().ForEach(z => z.DELETE_FLG = true));
                    this._gridCon.Bind();

                    // 削除完了メッセージ
                    Messenger.Info(Resources.KKM00003);

                    return true;
                }

                return false;
            }
            #endregion

            #region 内部メソッド

            /// <summary>
            /// DBから指摘の削除
            /// </summary>
            /// <returns></returns>
            private bool Delete(List<DesignCheckPointDeleteInModel> list)
            {
                if (list == null || list.Any() == false)
                {
                    return true;
                }

                //設計チェック詳細登録
                var res = HttpUtil.DeleteResponse(ControllerType.DesignCheckPoint, list);

                //レスポンスが取得できたかどうか
                if (res == null || res.Status != Const.StatusSuccess)
                {
                    return false;
                }

                return true;
            }

            #endregion

        }
        #endregion

        #region クラス生成管理クラス
        /// <summary>
        /// クラス生成管理クラス
        /// </summary>
        private class PointFactory : Factory
        {
            /// <summary>
            /// MultiRowを制御するクラス
            /// </summary>
            public MultiRowContoller MultiRowContoller { get; private set; }

            /// <summary>
            /// 指摘の追加と削除を制御するクラス
            /// </summary>
            public PointAddDelController PointAddDelController { get; private set; }

            public PointFactory(GcMultiRow mlr, Label rowCountLabel, DesignCheckGetOutModel designCheck, UserAuthorityOutModel userAuthorityModel, System.Action successProc = null)
                : base(designCheck, userAuthorityModel, successProc)
            {
                MultiRowContoller = new MultiRowContoller(mlr, rowCountLabel, Authority);
                PointAddDelController = new PointAddDelController(MultiRowContoller, designCheck.ID.Value);
            }
        }
        #endregion

        #region MultiRow編集開始イベント（共通化検討中）
        /// <summary>
        /// MultiRow編集開始イベント（共通化検討中）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellBeginEdit(object sender, CellBeginEditEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var cell = ((GcMultiRow)sender).CurrentCell;

            if (cell is TextBoxCell && ((TextBoxCell)cell).Style.Multiline != MultiRowTriState.False)
            {
                cell.VerticalResize(100);
            }
        }
        #endregion

        #region MultiRow編集終了イベント（共通化検討中）
        /// <summary>
        /// MultiRow編集終了イベント（共通化検討中）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellEndEdit(object sender, CellEndEditEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var cell = ((GcMultiRow)sender).CurrentCell;

            if (cell is TextBoxCell && ((TextBoxCell)cell).Style.Multiline != MultiRowTriState.False)
            {
                cell.PerformVerticalAutoFit();
            }
        }
        #endregion
    }
}