using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Config;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 指摘コピー
    /// </summary>
    public partial class DesignCheckCopyForm : BaseSubDialogForm
    {
        #region メンバ変数

        /// <summary>
        /// MultiRowを制御するクラス
        /// </summary>
        private MultiRowContoller GridContoller;

        /// <summary>
        /// CopyMultiRowを制御するクラス
        /// </summary>
        private CopyMultiRowContoller CopyContoller;

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>
        /// 期間コントロール管理クラス
        /// </summary>
        private BetweenController BetweenController;

        /// <summary>
        /// バインド中フラグ
        /// </summary>
        private bool IsBind = false;

        #endregion

        #region プロパティ

        /// <summary>画面名</summary>
        public override string FormTitle { get { return "指摘コピー"; } }

        /// <summary>
        /// コピー設定値
        /// </summary>
        public Dictionary<string, object> Copys { get { return this.CopyContoller.GetCopyValues(); } }

        /// <summary>
        /// コピーを貼り付けるために選択された行
        /// </summary>
        public Row Original { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignCheckCopyForm()
        {
            InitializeComponent();
        }
        #endregion

        #region ロード時
        /// <summary>
        /// ロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckCopyForm_Load(object sender, EventArgs e)
        {
            // 権限
            this.UserAuthority = base.GetFunction(FunctionID.DesignCheck);

            // 初期化
            this.GridContoller = new MultiRowContoller(this.PointMultiRow, this.RowCountLabel);
            this.CopyContoller = new CopyMultiRowContoller(this.CopyMultiRow);
            this.BetweenController = new BetweenController(this.FromNullableDateTimePicker, this.ToNullableDateTimePicker);

            // 画面初期化
            this.InitForm();

            // スクロールの同期設定
            new SyncScroll(PointMultiRow, CopyMultiRow);

            // コピー元を設定
            if (Original != null)
            {
                Original.Cells.ToList().ForEach((x) => CopyContoller.SetCellValue(x.Name, x.Value));
            }

            // 検索実行
            this.SearchButton_Click(null, null);
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            // バインドフラグ
            this.IsBind = true;

            try
            {
                // 開催日
                BetweenController.SetSart(new DateTime(DateTime.Today.AddMonths(-3).Year, DateTime.Today.AddMonths(-3).Month, 1));

                // ステータス
                this.StatusOpenCheckBox.Checked = true;
                this.StatusCloseCheckBox.Checked = true;
            }
            finally
            {
                // バインドフラグ
                this.IsBind = false;
            }
        }
        #endregion

        #region 画面表示後
        /// <summary>
        /// 画面表示後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckCopyForm_Shown(object sender, EventArgs e)
        {
            this.PointMultiRow.Select();
        }
        #endregion

        #region 検索ボタン押下
        /// <summary>
        /// 検索ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (IsSearchDesignCheck())
            {
                FormControlUtil.FormWait(this, () =>
                {
                    bool? status = null;

                    // Openのみ選択かどうか
                    if (this.StatusOpenCheckBox.Checked == true && this.StatusCloseCheckBox.Checked == false)
                    {
                        status = this.StatusOpenCheckBox.Checked;
                    }
                    // Closeのみ選択かどうか
                    else if (this.StatusOpenCheckBox.Checked == false && this.StatusCloseCheckBox.Checked == true)
                    {
                        status = !this.StatusCloseCheckBox.Checked;
                    }

                    var cond = new DesignCheckDetailGetInModel()
                    {
                        OPEN_START_DATE = this.FromNullableDateTimePicker.SelectedDate,
                        OPEN_END_DATE = this.ToNullableDateTimePicker.SelectedDate,
                        OPEN_FLG = status,
                    };

                    this.GridContoller.GetData(cond);

                    this.IsBind = true;

                    this.GridContoller.Bind();

                    this.IsBind = false;

                    this.ListDataLabel.Text = this.GridContoller.ModelList.Any() == true ? "" : Resources.KKM00005;

                    this.PointMultiRow.Select();
                });
            }
        }
        #endregion

        #region 検索条件のチェック
        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchDesignCheck()
        {
            var msg = Validator.GetFormInputErrorMessage(this, BetweenController.GetRule());

            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }

            return true;
        }
        #endregion

        #region クリアボタン押下
        /// <summary>
        /// クリアボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.InitForm();
        }
        #endregion

        #region 検索条件ボタン押下
        /// <summary>
        /// 検索条件ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            var flg = !this.SearchConditionTableLayoutPanel.Visible;

            //検索条件表示設定
            SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.ScrollPanel, new[] { this.RowCountLabel }, 70);

            //検索結果メッセージ
            this.ListDataLabel.Visible = flg;
        }

        /// <summary>
        /// 検索条件表示設定
        /// </summary>
        /// <param name="button">検索条件ボタン</param>
        /// <param name="table">検索条件</param>
        /// <param name="panel">一覧</param>
        /// <param name="ctrl">コントロール</param>
        /// <param name="height">高さ</param>
        protected void SearchConditionVisible(Button button, TableLayoutPanel table, Panel panel, IEnumerable<Control> ctrl, int height)
        {
            var SearchConditionButtonText = new Dictionary<bool, string>() { { true, "-" }, { false, "+" } };
            var flg = !table.Visible;

            //検索条件を非表示にするかどうか
            if (flg == true)
            {
                height *= -1;

            }

            //縦位置
            var location = panel.Location;
            location.Y -= height;
            panel.Location = location;

            //縦幅
            var size = panel.Size;
            size.Height += height;
            panel.Size = size;

            //検索条件ボタン
            button.Text = SearchConditionButtonText[flg];

            //検索条件
            table.Visible = flg;

            //コントロールがあるかどうか
            if (ctrl != null && ctrl.Any() == true)
            {
                foreach (var c in ctrl)
                {
                    var l = c.Location;
                    l.Y -= height;
                    c.Location = l;
                }
            }
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
            // 無効な行か列の場合は終了
            //UPDATE Start 2021/08/02 杉浦 設計チェック修正対応
            //if (e.RowIndex < 0 || e.CellIndex < 4 || e.CellName == "RowHeader")
            if (e.RowIndex < 0 || e.CellIndex < 5 || e.CellName == "RowHeader")
            //UPDATE End 2021/08/02 杉浦 設計チェック修正対応
            {
                return;
            }

            // コピー値の設定
            this.CopyContoller.SetCellValue(e.CellName, this.PointMultiRow.CurrentCell.Value);

            // 担当者の場合は担当者_IDもコピーする
            if (e.CellName == "UserTextBoxColumn")
            {
                this.CopyContoller.SetCellValue("UserIDTextBoxColumn", this.PointMultiRow.Rows[this.PointMultiRow.CurrentCell.RowIndex].Cells["UserIDTextBoxColumn"].Value);
            }
        }
        #endregion

        #region セルリサイズ
        /// <summary>
        /// セルリサイズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellResizeCompleted(object sender, CellEventArgs e)
        {
            // バインド中は終了
            if (this.IsBind)
            {
                return;
            }

            // 行ヘッダ以外は終了
            if (e.RowIndex != -1)
            {
                return;
            }

            var src = this.PointMultiRow[0, e.CellIndex];
            var dst = this.CopyMultiRow[0, e.CellIndex];

            dst.HorizontalResize(src.Width - dst.Width);
        }
        #endregion

        #region コピーボタン押下
        /// <summary>
        /// コピーボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (this.CopyContoller.GetCopyValues().Any() == false)
            {
                Messenger.Warn(Resources.TCM03008);
                return;
            }

            FormOkClose();
        }
        #endregion

        #region コピーMultiRowを制御するクラス
        /// <summary>
        /// コピーMultiRowを制御するクラス
        /// </summary>
        private class CopyMultiRowContoller
        {
            #region 内部変数

            private GcMultiRow _mlr;

            /// <summary>
            /// カスタムテンプレート
            /// </summary>
            private CustomTemplate CustomTemplate = new CustomTemplate();

            #endregion

            #region コンストラクタ
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="mlr">MultiRow</param>
            public CopyMultiRowContoller(GcMultiRow mlr)
            {
                _mlr = mlr;

                var headerHeight = 54;

                // テンプレート設定
                this.CustomTemplate.MultiRow = this._mlr;
                this.CustomTemplate.ColumnHeaderHeight = headerHeight;
                var tmp = this.CustomTemplate.SetContextMenuTemplate(new DesignCheckCopyMultiRowTemplate());

                // フィルタテキストは削除
                //UPDATE Start 2021/08/02 杉浦 設計チェック修正対応
                //tmp.ColumnHeaders[0].Cells.Remove(20, 19);
                tmp.ColumnHeaders[0].Cells.Remove(21, 20);
                //UPDATE End 2021/08/02 杉浦 設計チェック修正対応

                // 列ヘッダの設定
                tmp.ColumnHeaders[0].Cells["CornerHeader"].Size = new Size(tmp.ColumnHeaders[0].Cells["CornerHeader"].Width, headerHeight);
                tmp.ColumnHeaders[0].Height = headerHeight;
                tmp.ColumnHeaders[0].Cells.Cast<ColumnHeaderCell>().ToList().ForEach((x) => x.DropDownContextMenuStrip = null);
                tmp.ColumnHeaders[0].Cells["ch_OpenDay"].Style.BackColor = Color.LightGray;
                tmp.ColumnHeaders[0].Cells["ch_OpenDay"].Style.ForeColor = Color.Black;
                tmp.ColumnHeaders[0].Cells["ch_DesignCheckName"].Style.BackColor = Color.LightGray;
                tmp.ColumnHeaders[0].Cells["ch_DesignCheckName"].Style.ForeColor = Color.Black;
                tmp.ColumnHeaders[0].Cells["ch_No"].Style.BackColor = Color.LightGray;
                tmp.ColumnHeaders[0].Cells["ch_No"].Style.ForeColor = Color.Black;
                tmp.ColumnHeaders[0].Cells["ch_Status"].Style.BackColor = Color.LightGray;
                tmp.ColumnHeaders[0].Cells["ch_Status"].Style.ForeColor = Color.Black;

                // 行の設定
                tmp.Row.Height = CustomTemplate.TemplateRowHeight * 2;
                tmp.Row.Cells.Cast<Cell>().ToList().ForEach((x) => { x.Selectable = false; x.Size = new Size(x.Width, CustomTemplate.TemplateRowHeight * 2); x.MinimumSize = new Size(0, CustomTemplate.TemplateRowHeight * 2); });
                tmp.Row.Cells["OpenDayTextColumn"].Style.BackColor = Color.LightGray;
                tmp.Row.Cells["DesignCheckNameTextBoxColumn"].Style.BackColor = Color.LightGray;
                tmp.Row.Cells["NoTextBoxColumn"].Style.BackColor = Color.LightGray;
                //Append Start 2021/08/02 杉浦 設計チェック修正対応
                tmp.Row.Cells["MgrNoTextBoxColumn"].Style.BackColor = Color.LightGray;
                //Append End 2021/08/02 杉浦 設計チェック修正対応
                tmp.Row.Cells["StatusComboBoxColumn"].Style.BackColor = Color.LightGray;

                this._mlr.Template = tmp;

                this._mlr.RowCount = 1;
                this._mlr.CurrentCell = null;
            }
            #endregion

            #region 値を対象セルに設定します。
            /// <summary>
            /// 値を対象セルに設定します。
            /// </summary>
            /// <param name="cellName"></param>
            /// <param name="value"></param>
            public void SetCellValue(string cellName, object value)
            {
                // 対象でないセルは処理しない
                if (_mlr.Rows[0].Cells.Any((x) => x.Name == cellName) == false)
                {
                    return;
                }

                if (_mlr.Rows[0].Cells[cellName].Value != null && _mlr.Rows[0].Cells[cellName].Value.ToString() == Convert.ToString(value))
                {
                    // 同じ値をクリックした場合は消す
                    _mlr.Rows[0].Cells[cellName].Value = null;
                }
                else
                {
                    _mlr.Rows[0].Cells[cellName].Value = value;
                }

                _mlr.Rows[0].Cells[5].PerformVerticalAutoFit();
            }
            #endregion

            #region コピー値として設定された値を取得します。
            /// <summary>
            /// コピー値として設定された値を取得します。
            /// </summary>
            /// <returns></returns>
            public Dictionary<string, object> GetCopyValues()
            {
                // CellIndexで並び替えしておかないと貼り付け側で「担当者」と「担当者_ID」で整合性が取れなくなる可能性がある
                return _mlr.Rows[0].Cells.Cast<Cell>().Where((x) => x.Value != null).OrderBy((x) => x.CellIndex).ToDictionary((x) => x.Name, (x) => x.Value);
            }
            #endregion
        }
        #endregion

        #region MultiRowを制御するクラス
        /// <summary>
        /// MultiRowを制御するクラス
        /// </summary>
        private class MultiRowContoller
        {
            #region 内部変数

            private GcMultiRow _mlr;

            /// <summary>
            /// 設計チェック詳細モデル全リスト
            /// </summary>
            private List<DesignCheckDetailGetOutModel> _ModelAllList;

            /// <summary>
            /// カスタムテンプレート
            /// </summary>
            private CustomTemplate CustomTemplate = new CustomTemplate();

            #endregion

            #region 公開プロパティ

            /// <summary>
            /// データソースの元になる内部保持の設計チェック詳細モデルリスト
            /// </summary>
            public List<DesignCheckDetailGetOutModel> ModelList
            {
                get
                {
                    return this._ModelAllList;
                }

                set
                {
                    this._ModelAllList = value;
                }
            }

            #endregion

            #region コンストラクタ
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="mlr">MultiRow</param>
            /// <param name="rowCountLabel">行カウント表示ラベル</param>
            public MultiRowContoller(GcMultiRow mlr, Label rowCountLabel)
            {
                _mlr = mlr;
                _ModelAllList = new List<DesignCheckDetailGetOutModel>();

                // テンプレート設定
                this.CustomTemplate.ColumnHeaderHeight = 70;
                this.CustomTemplate.RowCountLabel = rowCountLabel;
                this.CustomTemplate.MultiRow = this._mlr;
                var tmp = this.CustomTemplate.SetContextMenuTemplate(new DesignCheckCopyMultiRowTemplate());
                tmp.ColumnHeaders[0].Cells["ch_OpenDay"].Style.BackColor = Color.LightGray;
                tmp.ColumnHeaders[0].Cells["ch_OpenDay"].Style.ForeColor = Color.Black;
                tmp.ColumnHeaders[0].Cells["ch_DesignCheckName"].Style.BackColor = Color.LightGray;
                tmp.ColumnHeaders[0].Cells["ch_DesignCheckName"].Style.ForeColor = Color.Black;
                tmp.ColumnHeaders[0].Cells["ch_No"].Style.BackColor = Color.LightGray;
                tmp.ColumnHeaders[0].Cells["ch_No"].Style.ForeColor = Color.Black;
                tmp.ColumnHeaders[0].Cells["ch_Status"].Style.BackColor = Color.LightGray;
                tmp.ColumnHeaders[0].Cells["ch_Status"].Style.ForeColor = Color.Black;
                tmp.Row.Cells["OpenDayTextColumn"].Style.BackColor= Color.LightGray;
                tmp.Row.Cells["DesignCheckNameTextBoxColumn"].Style.BackColor = Color.LightGray;
                tmp.Row.Cells["NoTextBoxColumn"].Style.BackColor = Color.LightGray;
                //Append Start 2021/08/02 杉浦 設計チェック修正対応
                tmp.Row.Cells["MgrNoTextBoxColumn"].Style.BackColor = Color.LightGray;
                //Append End 2021/08/02 杉浦 設計チェック修正対応
                tmp.Row.Cells["StatusComboBoxColumn"].Style.BackColor = Color.LightGray;
                this._mlr.Template = tmp;

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

                // ダブルバッファリング有効
                this._mlr.GetType().InvokeMember(
                   "DoubleBuffered",
                   System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                   null,
                   this._mlr,
                   new object[] { true });
            }
            #endregion

            #region データソースにバインドします。
            /// <summary>
            /// データソースにバインドします。
            /// </summary>
            public void Bind()
            {
                // フィルタリングクリア
                this._mlr.ClearAllFilters();

                // 初期化
                this._mlr.Rows.Clear();

                // データセット
                this.SetData();

                // 表示件数ラベル更新
                this.CustomTemplate.SetCountLabel();

                // 行の高さ調整
                this._mlr.Rows.ToList().ForEach((x) => x.Cells["SituationTextBoxColumn"].PerformVerticalAutoFit());

                // 一覧を未選択状態に設定
                this._mlr.CurrentCell = null;

                // グリッドリフレッシュ
                this._mlr.Refresh();
            }
            #endregion

            #region 設計チェック詳細データを取得します。
            /// <summary>
            /// 設計チェック詳細データを取得します。
            /// </summary>
            /// <param name="cond"></param>
            public void GetData(DesignCheckDetailGetInModel cond)
            {
                // 初期化
                this._ModelAllList.Clear();

                // APIで取得
                var res = HttpUtil.GetResponse<DesignCheckDetailGetInModel, DesignCheckDetailGetOutModel>(ControllerType.DesignCheckDetail, cond);

                if (res != null && res.Status == Const.StatusSuccess)
                {
                    this._ModelAllList.AddRange(res.Results);
                }
            }
            #endregion

            #region 内部メソッド

            #region 取得データを表示用に加工してバインドします。
            /// <summary>
            /// 取得データを表示用に加工してバインドします。
            /// </summary>
            private void SetData()
            {
                var list = new List<object[]>();

                // 指摘ID単位で表示行を作成
                foreach (var id in this._ModelAllList.Select((x) => x.指摘ID).Distinct())
                {
                    //UPDATE Start 2021/08/02 杉浦 設計チェック修正対応
                    //var row = new object[19];
                    var row = new object[20];
                    //UPDATE End 2021/08/02 杉浦 設計チェック修正対応

                    var model = this._ModelAllList.First((x) => x.指摘ID == id);

                    row[0] = model.開催日;
                    row[1] = model.名称;
                    row[2] = model.指摘NO;
                    //UPDATE Start 2021/08/02 杉浦 設計チェック修正対応
                    //row[3] = model.ステータス;
                    //row[4] = model.部品;
                    //row[5] = model.状況;
                    //row[6] = model.FLAG_処置不要;
                    //row[7] = model.処置課;
                    //row[8] = model.処置対象;
                    //row[9] = model.処置方法;
                    //row[10] = model.FLAG_調整済;
                    //row[11] = model.処置調整;
                    //row[12] = model.織込日程;
                    //row[13] = model.FLAG_試作改修;
                    //row[14] = model.部品納入日;
                    //row[15] = model.担当課名;
                    //row[16] = model.担当者名;
                    //row[17] = model.担当者_TEL;
                    //row[18] = model.担当者_ID;
                    row[3] = model.試作管理NO;
                    row[4] = model.ステータス;
                    row[5] = model.部品;
                    row[6] = model.状況;
                    row[7] = model.FLAG_処置不要;
                    row[8] = model.処置課;
                    row[9] = model.処置対象;
                    row[10] = model.処置方法;
                    row[11] = model.FLAG_調整済;
                    row[12] = model.処置調整;
                    row[13] = model.織込日程;
                    row[14] = model.FLAG_試作改修;
                    row[15] = model.部品納入日;
                    row[16] = model.担当課名;
                    row[17] = model.担当者名;
                    row[18] = model.担当者_TEL;
                    row[19] = model.担当者_ID;
                    //UPDATE End 2021/08/02 杉浦 設計チェック修正対応

                    // バインド
                    this._mlr.Rows.Add(row);
                }
            }
            #endregion

            #endregion
        }

        #endregion

        #region MultiRowのスクロールの同期クラス
        /// <summary>
        /// MultiRowのスクロールの同期クラス
        /// </summary>
        private class SyncScroll
        {
            GcMultiRow _mlr1;
            GcMultiRow _mlr2;

            /// <summary>
            /// イベントの処理が繰り返さないようにコードによるスクロールかどうかをフラグで判別する
            /// </summary>
            private bool _scrollByEvent = false;

            /// <summary>
            /// スクロールさせる値
            /// </summary>
            private int _ScrollValue;

            /// <summary>
            /// MultiRowのスクロールの同期クラスのコンストラクタ
            /// </summary>
            /// <param name="mlr1"></param>
            /// <param name="mlr2"></param>
            public SyncScroll(GcMultiRow mlr1, GcMultiRow mlr2)
            {
                _mlr1 = mlr1;
                _mlr2 = mlr2;

                _mlr1.Scroll += _mlr1_Scroll;
                _mlr2.Scroll += _mlr2_Scroll;
            }

            /// <summary>
            /// スクロールイベント１
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void _mlr1_Scroll(object sender, ScrollEventArgs e)
            {
                if (_scrollByEvent == false)
                {
                    if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                    {
                        _ScrollValue = e.NewValue;

                        // _mlr2のScrollイベントを発生させる
                        if (_mlr2.FirstDisplayedCellPosition.CellName == "RowHeader")
                        {
                            _mlr2.FirstDisplayedCellPosition = new CellPosition(0, 2);
                        }
                        else
                        {
                            _mlr2.FirstDisplayedCellPosition = new CellPosition(0, "RowHeader");
                        }
                    }
                }
                else
                {
                    _scrollByEvent = false;
                }
            }

            /// <summary>
            /// スクロールイベント２
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void _mlr2_Scroll(object sender, ScrollEventArgs e)
            {
                if (_scrollByEvent == false)
                {
                    if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                    {
                        e.NewValue = _ScrollValue;
                    }
                }
                else
                {
                    _scrollByEvent = false;
                }
            }
        }
        #endregion
    }
}
