using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 表示設定
    /// </summary>
    public partial class GridViewDisplayForm : BaseSubForm
    {
        #region メンバ変数
        private BindingSource hiddenDataSource = new BindingSource();

        private BindingSource displayDataSource = new BindingSource();
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "表示設定"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>画面名</summary>
        public string FormName { get; set; }

        /// <summary>表示設定</summary>
        public IEnumerable<GridViewDisplayModel> Display { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GridViewDisplayForm()
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
        private void MultiRowSortForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            Func<IEnumerable<GridViewDisplayModel>, List<GridViewDisplayModel>> get = list => list.Any() == false ? null : list.OrderBy(x => x.DisplayIndex).ToList();

            //一覧に設定
            this.HiddenDataGridView.AutoGenerateColumns = false;
            this.DisplayDataGridView.AutoGenerateColumns = false;

            //一覧に設定
            this.HiddenDataGridView.DataSource = hiddenDataSource;
            this.DisplayDataGridView.DataSource = displayDataSource;

            //データソース
            this.hiddenDataSource.DataSource = get(this.Display.Where(x => x.Visible == false));
            this.displayDataSource.DataSource = get(this.Display.Where(x => x.Visible == true));

            var isDisplay = this.DisplayDataGridView.RowCount > 0;

            //上下のボタン
            this.UpButton.Enabled = isDisplay;
            this.DownButton.Enabled = isDisplay;

            //左右のボタン
            this.RightButton.Enabled = this.HiddenDataGridView.RowCount > 0;
            this.LeftButton.Enabled = isDisplay;

            //表示しない列があれば初期選択
            this.ActiveControl = this.HiddenDataGridView.RowCount > 0 ? this.HiddenDataGridView : this.DisplayDataGridView;

        }
        #endregion

        #region 右ボタンクリック
        /// <summary>
        /// 右ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightButton_Click(object sender, EventArgs e)
        {
            //データが無ければ終了
            if (this.HiddenDataGridView.RowCount == 0)
            {
                return;

            }

            //対象を選択しているかどうか
            if (this.HiddenDataGridView.SelectedCells.Count == 0)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            var hiddenList = this.hiddenDataSource.DataSource as List<MultiRowDisplayModel> ?? new List<MultiRowDisplayModel>();
            var displayList = this.displayDataSource.DataSource as List<MultiRowDisplayModel> ?? new List<MultiRowDisplayModel>();

            //選択した列を取得
            var rowIndex = this.HiddenDataGridView.SelectedCells[0].RowIndex;
            var col = hiddenList.First(x => x.Name == this.HiddenDataGridView[this.HiddenNameColumn.Name, rowIndex].Value.ToString());

            var scrollingRowIndex = this.GetScrollingRowIndex(this.HiddenDataGridView, rowIndex);

            //表示設定
            col.Visible = true;

            //表示対象の切り替え
            hiddenList.Remove(col);
            displayList.Add(col);

            //データソース初期化
            this.hiddenDataSource.DataSource = null;
            this.displayDataSource.DataSource = null;

            //データソース
            this.hiddenDataSource.DataSource = hiddenList;
            this.displayDataSource.DataSource = displayList;

            //一覧を未選択状態に設定
            this.HiddenDataGridView.CurrentCell = null;
            this.DisplayDataGridView.CurrentCell = null;

            //対象行を選択
            var displayIndex = this.DisplayDataGridView.RowCount - 1;
            this.DisplayDataGridView.Rows[displayIndex].Selected = true;

            //左右ボタン
            this.RightButton.Enabled = hiddenList.Any();
            this.LeftButton.Enabled = displayList.Any();

            //上下のボタン
            var isCount = displayList.Count() > 1;
            this.UpButton.Enabled = isCount;
            this.DownButton.Enabled = isCount;

            //一覧があるかどうか
            if (this.HiddenDataGridView.RowCount > 0)
            {
                //同じ行があるなら選択
                if (this.HiddenDataGridView.RowCount <= rowIndex)
                {
                    rowIndex = this.HiddenDataGridView.RowCount - 1;

                }
                this.HiddenDataGridView.Rows[rowIndex].Selected = true;

                //対象行を表示
                this.HiddenDataGridView.FirstDisplayedScrollingRowIndex = scrollingRowIndex;

                //上下のボタン
                this.UpButton.Enabled = displayIndex != 0;
                this.DownButton.Enabled = displayIndex != displayList.Count() - 1;

            }

        }
        #endregion

        #region 左ボタンクリック
        /// <summary>
        /// 左ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftButton_Click(object sender, EventArgs e)
        {
            //データが無ければ終了
            if (this.DisplayDataGridView.RowCount == 0)
            {
                return;

            }

            //対象を選択しているかどうか
            if (this.DisplayDataGridView.SelectedCells.Count == 0)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            var hiddenList = this.hiddenDataSource.DataSource as List<MultiRowDisplayModel> ?? new List<MultiRowDisplayModel>();
            var displayList = this.displayDataSource.DataSource as List<MultiRowDisplayModel> ?? new List<MultiRowDisplayModel>();

            //選択した列を取得
            var rowIndex = this.DisplayDataGridView.SelectedCells[0].RowIndex;
            var col = displayList.First(x => x.Name == this.DisplayDataGridView[this.DisplayNameColumn.Name, rowIndex].Value.ToString());

            var scrollingRowIndex = this.GetScrollingRowIndex(this.DisplayDataGridView, rowIndex);

            //表示設定
            col.Visible = false;

            //表示対象の切り替え
            hiddenList.Add(col);
            displayList.Remove(col);

            //データソース初期化
            this.hiddenDataSource.DataSource = null;
            this.displayDataSource.DataSource = null;

            //データソース
            this.hiddenDataSource.DataSource = hiddenList;
            this.displayDataSource.DataSource = displayList;

            //一覧を未選択状態に設定
            this.HiddenDataGridView.CurrentCell = null;
            this.DisplayDataGridView.CurrentCell = null;

            //対象行を選択
            this.HiddenDataGridView.Rows[this.HiddenDataGridView.RowCount - 1].Selected = true;

            //左右ボタン
            this.RightButton.Enabled = hiddenList.Any();
            this.LeftButton.Enabled = displayList.Any();

            //上下のボタン
            var isCount = displayList.Count() > 1;
            this.UpButton.Enabled = isCount;
            this.DownButton.Enabled = isCount;

            //一覧があるかどうか
            if (this.DisplayDataGridView.RowCount > 0)
            {
                //同じ行があるなら選択
                if (this.DisplayDataGridView.RowCount <= rowIndex)
                {
                    rowIndex = this.DisplayDataGridView.RowCount - 1;

                }
                this.DisplayDataGridView.Rows[rowIndex].Selected = true;

                //対象行を表示
                this.DisplayDataGridView.FirstDisplayedScrollingRowIndex = scrollingRowIndex;

                //上下のボタン
                this.UpButton.Enabled = rowIndex != 0;
                this.DownButton.Enabled = rowIndex != displayList.Count() - 1;

            }

        }
        #endregion

        #region 表示列
        /// <summary>
        /// 表示列セルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            //先頭行は上ボタン無効
            this.UpButton.Enabled = e.RowIndex > 0;

            //最終行は下ボタン無効
            this.DownButton.Enabled = e.RowIndex + 1 != DisplayDataGridView.RowCount;

        }

        /// <summary>
        /// 表示行の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex + 1 != DisplayDataGridView.RowCount)
            {
                bool fullDisplayed = true;

                if(DisplayDataGridView.Rows[e.RowIndex + 1].Displayed == false)
                {
                    //次行が表示されていない
                    fullDisplayed = false;
                }
                else
                {
                    //次行の範囲が全て表示されているか
                    fullDisplayed =
                        DisplayDataGridView.GetRowDisplayRectangle(e.RowIndex + 1, false).Height
                        == DisplayDataGridView.GetRowDisplayRectangle(e.RowIndex + 1, true).Height;
                }

                //次行の表示
                if (fullDisplayed == false)
                {
                    DisplayDataGridView.FirstDisplayedScrollingRowIndex += 1;
                }
            }
        }
        #endregion

        #region 上ボタンクリック
        /// <summary>
        /// 上ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpButton_Click(object sender, EventArgs e)
        {
            //データが無いか一番上なら終了
            if (this.DisplayDataGridView.RowCount == 0 || (this.DisplayDataGridView.SelectedCells.Count > 0 && this.DisplayDataGridView.SelectedCells[0].RowIndex == 0))
            {
                return;

            }

            //対象を選択しているかどうか
            if (this.DisplayDataGridView.SelectedCells.Count == 0)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            var displayList = this.displayDataSource.DataSource as List<MultiRowDisplayModel>;

            var rowIndex = this.DisplayDataGridView.SelectedCells[0].RowIndex;
            var targetCol = displayList.First(x => x.Name == this.DisplayDataGridView[this.DisplayNameColumn.Name, rowIndex - 1].Value.ToString());
            var selectCol = displayList.First(x => x.Name == this.DisplayDataGridView[this.DisplayNameColumn.Name, rowIndex].Value.ToString());

            var list = new List<MultiRowDisplayModel>();

            foreach (var col in displayList.Where(x => x.Name != selectCol.Name))
            {
                //移動対象の列かどうか
                if (targetCol.Name == col.Name)
                {
                    list.Add(selectCol);
                    list.Add(targetCol);
                    continue;

                }
                
                list.Add(col);

            }

            var index = rowIndex - 1;

            var scrollingRowIndex = this.GetScrollingRowIndex(this.DisplayDataGridView, index);

            //データソース初期化
            this.displayDataSource.DataSource = null;

            //データソース
            this.displayDataSource.DataSource = list;

            //一覧を未選択状態に設定
            this.DisplayDataGridView.CurrentCell = null;

            //対象行を選択
            this.DisplayDataGridView.Rows[index].Selected = true;

            //対象行を表示
            this.DisplayDataGridView.FirstDisplayedScrollingRowIndex = scrollingRowIndex;

            //上下のボタン
            this.UpButton.Enabled = index > 0;
            this.DownButton.Enabled = true;

        }
        #endregion

        #region 下ボタンクリック
        /// <summary>
        /// 下ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownButton_Click(object sender, EventArgs e)
        {
            //データが無いか一番下なら終了
            if (this.DisplayDataGridView.RowCount == 0 || (this.DisplayDataGridView.SelectedCells.Count > 0 && this.DisplayDataGridView.SelectedCells[0].RowIndex == this.DisplayDataGridView.RowCount - 1))
            {
                return;

            }

            //対象を選択しているかどうか
            if (this.DisplayDataGridView.SelectedCells.Count == 0)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            var displayList = this.displayDataSource.DataSource as List<MultiRowDisplayModel>;

            var rowIndex = this.DisplayDataGridView.SelectedCells[0].RowIndex;
            var targetCol = displayList.First(x => x.Name == this.DisplayDataGridView[this.DisplayNameColumn.Name, rowIndex + 1].Value.ToString());
            var selectCol = displayList.First(x => x.Name == this.DisplayDataGridView[this.DisplayNameColumn.Name, rowIndex].Value.ToString());

            var list = new List<MultiRowDisplayModel>();

            foreach (var col in displayList.Where(x => x.Name != selectCol.Name))
            {
                //移動対象の列かどうか
                if (targetCol.Name == col.Name)
                {
                    list.Add(targetCol);
                    list.Add(selectCol);
                    continue;

                }

                list.Add(col);

            }

            var index = rowIndex + 1;

            var scrollingRowIndex = this.GetScrollingRowIndex(this.DisplayDataGridView, index);

            //データソース初期化
            this.displayDataSource.DataSource = null;

            //データソース
            this.displayDataSource.DataSource = list;

            //一覧を未選択状態に設定
            this.DisplayDataGridView.CurrentCell = null;

            //対象行を選択
            this.DisplayDataGridView.Rows[index].Selected = true;

            //対象行を表示
            this.DisplayDataGridView.FirstDisplayedScrollingRowIndex = scrollingRowIndex;

            //上下のボタン
            this.UpButton.Enabled = true;
            this.DownButton.Enabled = index != list.Count() - 1;

        }
        #endregion

        #region OKボタンクリック
        /// <summary>
        /// OKボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //入力チェック
                if (this.IsDisplayConfiguration() == false)
                {
                    return;

                }

                var hiddenList = this.hiddenDataSource.DataSource as List<MultiRowDisplayModel> ?? new List<MultiRowDisplayModel>();
                var displayList = this.displayDataSource.DataSource as List<MultiRowDisplayModel> ?? new List<MultiRowDisplayModel>();

                var list = displayList.Concat(hiddenList).ToList();

                //表示順設定
                var i = 0;
                list.ForEach(x => x.DisplayIndex = i++);

                //ユーザー表示設定情報登録
                if (this.EntryUserDisplay(list) == true)
                {
                    //表示設定
                    this.Display = list;

                    //フォームクローズ
                    base.FormOkClose();

                }

            });

        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool IsDisplayConfiguration()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            map[this.DisplayDataGridView] = (c, name) =>
            {
                var errMsg = "";

                //表示対象の列があるかどうか
                var displayList = this.displayDataSource.DataSource as List<MultiRowDisplayModel> ?? new List<MultiRowDisplayModel>();
                if (displayList.Any() == false)
                {
                    //エラーメッセージ
                    errMsg = Resources.TCM00001;


                }

                return errMsg;

            };

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);

            }

            return msg == "";

        }
        #endregion

        #region 標準に戻すボタンクリック
        /// <summary>
        /// 標準に戻すボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //ユーザー表示設定情報削除
                if (this.DeleteUserDisplay() == true)
                {
                    var i = 0;

                    var list = this.Display.Select(x =>
                    {
                        //表示
                        x.Visible = true;

                        //表示順
                        x.DisplayIndex = i++;

                        return x;

                    }).ToList();

                    //データソース
                    this.hiddenDataSource.DataSource = null;
                    this.displayDataSource.DataSource = list;

                    //表示設定
                    this.Display = list;

                    //フォームクローズ
                    base.FormOkClose();

                }

            });

        }
        #endregion

        #region スクロールする行を取得
        /// <summary>
        /// スクロールする行を取得
        /// </summary>
        /// <param name="grid">グリッド</param>
        /// <param name="index">対象行</param>
        /// <returns></returns>
        private int GetScrollingRowIndex(DataGridView grid, int index)
        {
            var firstRow = grid.Rows.GetFirstRow(DataGridViewElementStates.Displayed);
            var lastRow = grid.Rows.GetLastRow(DataGridViewElementStates.Displayed);

            var scrollingRowIndex = grid.FirstDisplayedScrollingRowIndex;

            firstRow += (grid.GetRowDisplayRectangle(firstRow, false).Height == grid.GetRowDisplayRectangle(firstRow, true).Height ? 0 : 1);
            lastRow -= (grid.GetRowDisplayRectangle(lastRow, false).Height == grid.GetRowDisplayRectangle(lastRow, true).Height ? 0 : 1);

            //スクロール無しで表示できるなら終了
            if (firstRow <= index && index <= lastRow)
            {
                return scrollingRowIndex;

            }

            if (lastRow < index)
            {
                scrollingRowIndex = firstRow + index - lastRow;

            }
            else if (index < firstRow)
            {
                scrollingRowIndex = index;

            }

            return scrollingRowIndex;

        }
        #endregion

        #region API
        /// <summary>
        /// ユーザー表示設定情報登録
        /// </summary>
        /// <param name="target">ユーザー表示設定</param>
        /// <returns></returns>
        private bool EntryUserDisplay(IEnumerable<MultiRowDisplayModel> target)
        {
            Func<IEnumerable<MultiRowDisplayModel>, string> get = l => l.Any() == false ? null : string.Join(",", l.OrderBy(x => x.DisplayIndex).Select(x => x.HeaderText).ToArray());

            var list = new[] { new UserDisplayConfigurationModel
            {
                //ユーザーID
                ユーザーID = SessionDto.UserId,

                //画面名
                画面名 = this.FormName,

                //表示列名
                表示列名 = get(target.Where(x => x.Visible == true)),

                //非表示列名
                非表示列名 = get(target.Where(x => x.Visible == false)),

            } };

            //登録
            var res = HttpUtil.PutResponse(ControllerType.UserDisplayConfiguration, list);

            return res != null && res.Status == Const.StatusSuccess;

        }

        /// <summary>
        /// ユーザー表示設定情報削除
        /// </summary>
        /// <returns></returns>
        private bool DeleteUserDisplay()
        {
            var list = new[] { new UserDisplayConfigurationModel { ユーザーID = SessionDto.UserId, 画面名 = this.FormName } };

            //削除
            var res = HttpUtil.DeleteResponse(ControllerType.UserDisplayConfiguration, list);

            return res != null && res.Status == Const.StatusSuccess;

        }
        #endregion
    }
}
