using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon.Config;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Util;

namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    /// <summary>
    /// 使用履歴簡易入力
    /// </summary>
    public partial class TestCarUseHistoryInputForm : BaseSubForm
    {
        #region メンバ変数
        /// <summary>
        /// カスタムテンプレート
        /// </summary>
        private CustomTemplate customTemplate = new CustomTemplate();

        /// <summary>作業履歴リスト</summary>
        private List<WorkHistoryModel> WorkHistoryList { get; set; } = new List<WorkHistoryModel>();
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "使用履歴簡易入力"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>開発符号</summary>
        public string GeneralCode { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCarUseHistoryInputForm()
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
        private void TestCarUseHistoryInputForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 画面初期化
                this.InitForm();

                // MultiRow初期化
                this.InitMultiRow();

                // 一覧設定
                this.SetWorkHistoryList();
            });

            //Append Start 2021/07/12 矢作
            // 情報列
            var info = new string[] { "最新履歴_SEQNO", "最新履歴_承認状況", "最新履歴_日付", "最新履歴_試験内容", "最新履歴_実走行距離" };

            // 課題列の色設定
            //foreach (var row in this.ListMultiRow.Rows)
            //{
            //    row.Cells[13].Style.BackColor = Color.LightCyan;
            //    row.Cells[14].Style.BackColor = Color.LightCyan;
            //    row.Cells[15].Style.BackColor = Color.LightCyan;
            //    row.Cells[16].Style.BackColor = Color.LightCyan;
            //    row.Cells[17].Style.BackColor = Color.LightCyan;
            //}
            //Append End 2021/07/12 矢作
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            // 反映ボタン
            this.EntryButton.Visible = isUpdate;

            //Append Start 2021/07/12 矢作

            // 情報列
            var info = new string[] { "最新履歴_SEQNO", "最新履歴_承認状況", "最新履歴_日付", "最新履歴_試験内容", "最新履歴_実走行距離" };

            // 課題列の色設定
            foreach (var row in this.ListMultiRow.Rows)
            {
                row.Cells[5].Style.BackColor = Color.Aqua;
            }

            //Append End 2021/07/12 矢作
        }

        /// <summary>
        /// MultiRow初期化
        /// </summary>
        private void InitMultiRow()
        {
            // テンプレート設定
            this.customTemplate.RowCountLabel = this.RowCountLabel;
            this.customTemplate.MultiRow = this.ListMultiRow;

            this.ListMultiRow.Template = this.customTemplate.SetContextMenuTemplate(new TestCarUseHistoryInputMultiRowTemplate());

            // 選択列のフィルターアイテム設定
            var headerCell = this.ListMultiRow.Template.ColumnHeaders[0].Cells["columnHeaderCell1"] as ColumnHeaderCell;
            headerCell.DropDownContextMenuStrip.Items.RemoveAt(headerCell.DropDownContextMenuStrip.Items.Count - 1);
            headerCell.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });
        }
        #endregion

        #region 画面初期表示時
        /// <summary>
        /// 画面初期表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarUseHistoryInputForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.ListMultiRow.CurrentCell = null;
        }
        #endregion

        #region 一覧設定
        /// <summary>
        /// 作業履歴一覧設定
        /// </summary>
        /// <param name="isNewSearch"></param>
        private void SetWorkHistoryList(bool isNewSearch = true)
        {
            // 描画停止
            this.ListMultiRow.SuspendLayout();

            if (isNewSearch)
            {
                this.WorkHistoryList = this.GetTestCarWorkHistoryList(new string[] { this.GeneralCode });
            }

            // 複製の作成
            var copyList = new List<WorkHistoryModel>(this.WorkHistoryList);

            // 調整
            copyList.RemoveAll(x => x.月例点検省略有無 == (this.MonthlyInputCheckBox.Checked ? "XX" : "不要"));

            // データバインド
            this.customTemplate.SetDataSource(copyList);

            // 試験車が取得できたかどうか
            if (copyList?.Any() == true)
            {
                // リストデータラベルのセット
                this.ListDataLabel.Text = string.Empty;

                // レイアウトの設定
                this.SetLayout();
            }
            else
            {
                // リストデータラベルのセット
                this.ListDataLabel.Text = Resources.KKM00005;
            }

            // 一覧を未選択状態に設定
            this.ListMultiRow.CurrentCell = null;

            // 描画再開
            this.ListMultiRow.ResumeLayout();
        }

        /// <summary>
        /// レイアウトの設定
        /// </summary>
        private void SetLayout()
        {
            foreach (var row in this.ListMultiRow.Rows)
            {
                if (row.Index > 0)
                {
                    var border = new Border();
                    
                    if (row.Cells["DataIdColumn"].Value.ToString() != this.ListMultiRow.Rows[row.Index - 1].Cells["DataIdColumn"].Value.ToString())
                    {
                        // 下線設定
                        border.Bottom = new Line(LineStyle.Thin, Color.Gray);
                    }

                    this.ListMultiRow.Rows[row.Index - 1].Border = border;
                }

                // 表示高調整
                row.Cells["CategoryColumn"].PerformVerticalAutoFit();
                row.Cells["CurrentSituationColumn"].PerformVerticalAutoFit();
            }
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
            FormControlUtil.FormWait(this, () =>
            {
                // 試験車使用履歴を起動できたかどうか
                if (this.OpenTestCarUseHistory() == false)
                {
                    Messenger.Warn(Resources.KKM00009);
                    return;
                }
            });
        }

        /// <summary>
        /// 試験車使用履歴の起動
        /// </summary>
        private bool OpenTestCarUseHistory()
        {
            var ret = false;

            // 試験車管理権限の取得
            var authority = this.GetFunctionList(FunctionID.TestCarManagement).FirstOrDefault();

            // 選択行の格納
            foreach (var row in this.ListMultiRow.Rows.Where(x => (x.Cells["SelectedColumn"].FormattedValue.ToString()) == "True" ? true : false))
            {
                var dataID = (int)row.Cells["DataIdColumn"].Value;
                var historyNo = (int)row.Cells["HistoryNoColumn"].Value;

                // 試験車使用履歴画面表示
                new FormUtil(new UITestCar.Othe.TestCarHistory.TestCarHistoryForm
                {
                    TestCar = new TestCarCommonModel { データID = dataID, 履歴NO = historyNo },
                    WorkHistory = (WorkHistoryModel)row.DataBoundItem,
                    UserAuthority = authority,
                    Reload = (bool isPast) => FormControlUtil.FormWait(this, () => this.SetWorkHistoryList()),
                    //Append START 2020/12/07 No.63844 杉浦
                    Activate = () => this.Activate()
                    //Append END   2020/12/07 No.63844 杉浦
                })
                .SingleFormShow(this, false);

                ret = true;
            }

            return ret;
        }
        #endregion

        #region データの取得
        /// <summary>
        /// 作業履歴(試験車)取得
        /// </summary>
        /// <returns></returns>
        private List<WorkHistoryModel> GetTestCarWorkHistoryList(string[] codes)
        {
            // 条件設定
            var cond = new TestCarWorkHistorySearchModel()
            {
                GENERAL_CODE = codes,
                NECESSARY_INSPECTION_FLAG = true,
            };

            // APIで取得
            var res = HttpUtil.GetResponse<TestCarWorkHistorySearchModel, WorkHistoryModel>(ControllerType.TestCarWorkHistory, cond);

            // レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                return res.Results.ToList();
            }

            return new List<WorkHistoryModel>();
        }
        #endregion

        #region 全選択チェックボックス描画処理
        /// <summary>
        /// 全選択チェックボックス描画処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListMultiRow_CellPainting(object sender, GrapeCity.Win.MultiRow.CellPaintingEventArgs e)
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
                    Point point = new Point((bmp.Width - AllSelectCheckBox.Width) / 2, (bmp.Height - AllSelectCheckBox.Height) / 2);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    AllSelectCheckBox.DrawToBitmap(bmp, new Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

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

        #region MultiRowセルクリックイベント
        /// <summary>
        /// MultiRowセルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListMultiRow_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            // チェックボックスの表示を更新する
            if (e.CellIndex == 0 && e.RowIndex == -1)
            {
                AllSelectCheckBox.Checked = !AllSelectCheckBox.Checked;
            }

            // 無効セルは終了
            if (e.CellIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            var row = this.ListMultiRow.Rows[e.RowIndex];
            var cell = row.Cells[e.CellIndex];

            if (cell.Name == "SelectedColumn" || (cell.Name != "SelectedColumn" && cell.ReadOnly == true))
            {
                // チェックボックスの値を更新する
                row.Cells["SelectedColumn"].Value = !(row.Cells["SelectedColumn"].FormattedValue.ToString() == "True" ? true : false);
            }

            var isChecked = row.Cells["SelectedColumn"].FormattedValue.ToString() == "True";

            // 選択チェックボックスが未チェックの場合は終了
            if (isChecked == false)
            {
                return;
            }

            var Id = (decimal)row.Cells["IdColumn"].Value;
            var dataId = (int)row.Cells["DataIdColumn"].Value;

            foreach (var same in this.ListMultiRow.Rows.Where(x => (int)x.Cells["DataIdColumn"].Value == dataId && (decimal)x.Cells["IdColumn"].Value != Id))
            {
                // 同一車両を未選択に変更
                same.Cells["SelectedColumn"].Value = false;
            }
        }
        #endregion

        #region MultiRowセル結合イベント
        /// <summary>
        /// MultiRowセル結合イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListMultiRow_QueryCellMergeState(object sender, QueryCellMergeStateEventArgs e)
        {
            if (e.ShouldMerge == true)
            {
                var itemIdName = "DataIdColumn";

                if (e.QueryCell.CellName != itemIdName)
                {
                    var newQueryValue = this.ListMultiRow.Rows[e.QueryCell.RowIndex].Cells[itemIdName].Value;
                    var newTargetValue = this.ListMultiRow.Rows[e.TargetCell.RowIndex].Cells[itemIdName].Value;

                    // データIDが同じ場合セル結合させる
                    e.ShouldMerge = newQueryValue?.ToString() == newTargetValue?.ToString();
                }
            }
        }
        #endregion

        #region 全選択チェックボックス本体変更時
        /// <summary>
        /// 全選択チェックボックス本体変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
             {
                 var dataId = 0;

                 // CheckBoxCell 不具合対応
                 this.ListMultiRow.CurrentCell = null;

                 foreach (var row in this.ListMultiRow.Rows)
                 {
                     //Append Start 2020/01/27 杉浦
                     // 非表示行は未処理
                     if (!row.Visible) continue;
                     //Append End 2020/01/27 杉浦

                     if (dataId != (int)row.Cells["DataIdColumn"].Value)
                     {
                         // 車両の一番上を選択・非選択
                         row.Cells["SelectedColumn"].Value = this.AllSelectCheckBox.Checked;
                     }
                     else
                     {
                         // 上記以外はすべて非選択
                         row.Cells["SelectedColumn"].Value = false;
                     }

                     // データIDの退避
                     dataId = (int)row.Cells["DataIdColumn"].Value;
                 }
             });            
        }
        #endregion

        #region 月例点検不要チェックボックスチェンジイベント
        /// <summary>
        /// 月例点検不要チェックボックスチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyInputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // 一覧設定
            FormControlUtil.FormWait(this, () => this.SetWorkHistoryList(false));
        }
        #endregion


        //Append Start 2021/07/12 矢作
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NewestHistoryCheckBox.Checked)
            {
                //this.ListMultiRow.Template.Width = 1400;
                //foreach (var row in this.ListMultiRow.Rows)
                //{
                //    row.Cells[13].Visible = true;
                //    row.Cells[14].Visible = true;
                //    row.Cells[15].Visible = true;
                //    row.Cells[16].Visible = true;
                //    row.Cells[17].Visible = true;
                //}
                this.ListMultiRow.Columns[13].Expand();
                this.ListMultiRow.Columns[14].Expand();
                this.ListMultiRow.Columns[15].Expand();
                this.ListMultiRow.Columns[16].Expand();
                this.ListMultiRow.Columns[17].Expand();
                //this.ListMultiRow.Template.Width = 1600;
            }
            else
            {
                //foreach (var row in this.ListMultiRow.Rows)
                //{
                //    row.Cells[13].Visible = false;
                //    row.Cells[14].Visible = false;
                //    row.Cells[15].Visible = false;
                //    row.Cells[16].Visible = false;
                //    row.Cells[17].Visible = false;
                //}
                this.ListMultiRow.Columns[13].Collapse();
                this.ListMultiRow.Columns[14].Collapse();
                this.ListMultiRow.Columns[15].Collapse();
                this.ListMultiRow.Columns[16].Collapse();
                this.ListMultiRow.Columns[17].Collapse();
                //this.ListMultiRow.Template.Width = 1220;
            }



            ////

            //Size cellSize = new Size(120, 700);
            ////this.ListMultiRow.Rows[0].Cells[1].Size = cellSize;
            //Template temp = ListMultiRow.Template;

            //ListMultiRow.Rows[0].Cells[10].Value = "ABC";

            //ListMultiRow.Template = temp;
            ////


        }
        //Append End 2021/07/12 矢作
    }
}
