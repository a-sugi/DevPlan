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
using DevPlan.Presentation.UC.MultiRow;

namespace DevPlan.Presentation.UITestCar.Othe.ControlSheet
{
    /// <summary>
    /// 検索条件名設定
    /// </summary>
    public partial class ControlSheetSaveForm : BaseSubForm
    {
        #region メンバ変数
        private DataGridViewUtil<MultiRowDisplayModel> hiddenGridUtil = null;
        private DataGridViewUtil<MultiRowDisplayModel> displayGridUtil = null;

        private const string DefaultName = "**********";
        private const short ConfigRow = -1;
        private const string Desc = " DESC";
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "検索条件名設定"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>画面名</summary>
        public string FormName { get; set; }

        /// <summary>カスタムテンプレート</summary>
        public CustomTemplate CustumTemplate { get; set; }

        /// <summary>選択検索条件</summary>
        public ControlSheetSearchConditionModel SelectedUserSearchCondition { get; set; }

        /// <summary>検索条件</summary>
        public List<ControlSheetSearchConditionModel> SearchConditionList { get; set; } = new List<ControlSheetSearchConditionModel>();

        /// <summary>検索条件名可否</summary>
        public bool SearchConditionNameEnabled
        {
            get { return this.SearchConditionNameTextBox.Enabled; }
            set { this.SearchConditionNameTextBox.Enabled = value; }

        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControlSheetSaveForm()
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
        private void ControlSheetSaveForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            Func<IEnumerable<MultiRowDisplayModel>, List<MultiRowDisplayModel>> get = list => list.Any() == false ? null : list.OrderBy(x => x.DisplayIndex).ToList();

            var display = this.CustumTemplate.Display.ToList();

            var sortList = this.CustumTemplate.Sort == null ? new List<MultiRowSortModel>() : this.CustumTemplate.Sort.ToList();

            //検索条件の設定情報があるかどうか
            if (this.SearchConditionList != null && this.SearchConditionList.Any(x => x.行番号 == ConfigRow) == true)
            {
                var config = this.SearchConditionList.First(x => x.行番号 == ConfigRow);

                var i = 1;

                //列を非表示時化
                display.ForEach(x => x.Visible = false);

                foreach (var name in (config.TEXT ?? "").Split(',').Select(x => x.Trim()))
                {
                    //表示対象の列かどうか
                    var col = display.FirstOrDefault(x => x.HeaderText.Trim().ToUpper() == name.ToUpper());
                    if (col != null)
                    {
                        //表示
                        col.Visible = true;

                        //表示順
                        col.DisplayIndex = i++;

                    }

                }

                //ソート順設定
                sortList.Clear();

                foreach (var name in (config.CONJUNCTION ?? "").Split(',').Select(x => x.Trim()))
                {
                    //ソート対象の列かどうか
                    var col = display.FirstOrDefault(x => x.HeaderText.Trim().ToLower() == name.ToLower().Replace(Desc, "").Trim());
                    if (col != null)
                    {
                        sortList.Add(new MultiRowSortModel
                        {
                            //列名
                            Name = col.HeaderText,

                            //データプロパティ名
                            DataPropertyName = col.DataPropertyName,

                            //降順可否
                            IsDesc = name.ToLower().EndsWith(Desc)

                        });

                    }

                }

            }

            var displayList = get(display.Where(x => x.Visible == true));

            //ソート順があるかどうか
            if (sortList != null && sortList.Any() == true)
            {
                var i = 1;

                foreach (var sort in sortList)
                {
                    //存在する列かどうか
                    var col = displayList.FirstOrDefault(x => x.HeaderText.ToUpper() == sort.Name.ToUpper());
                    if (col != null)
                    {
                        //ソート順
                        col.SortNo = i++;

                        //昇順可否
                        col.IsAsc = !sort.IsDesc;

                    }

                }

            }

            //グリッドの初期化
            this.hiddenGridUtil = new DataGridViewUtil<MultiRowDisplayModel>(this.HiddenDataGridView, false);
            this.displayGridUtil = new DataGridViewUtil<MultiRowDisplayModel>(this.DisplayDataGridView, false);

            //データソース
            this.hiddenGridUtil.DataSource = get(display.Where(x => x.Visible == false));
            this.displayGridUtil.DataSource = displayList;

            //ソート順
            this.SortColumn.Tag = "Regex(^[0-9]{1,3}$);ItemName(ソート順)";

            //検索条件名
            this.SearchConditionNameTextBox.Text = this.SearchConditionList.FirstOrDefault()?.条件名;

            var isDisplay = displayList.Any();

            //上下のボタン
            this.UpButton.Enabled = isDisplay;
            this.DownButton.Enabled = isDisplay;

            //左右のボタン
            this.RightButton.Enabled = this.HiddenDataGridView.RowCount > 0;
            this.LeftButton.Enabled = isDisplay;

            //先頭行選択
            this.DisplayDataGridView.Rows[0].Selected = true;

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

            var hiddenList = this.hiddenGridUtil.DataSource == null ? new List<MultiRowDisplayModel>() : this.hiddenGridUtil.DataSource.ToList();
            var displayList = this.displayGridUtil.DataSource == null ? new List<MultiRowDisplayModel>() : this.displayGridUtil.DataSource.ToList();

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
            this.hiddenGridUtil.DataSource = null;
            this.displayGridUtil.DataSource = null;

            //データソース
            this.hiddenGridUtil.DataSource = hiddenList;
            this.displayGridUtil.DataSource = displayList;

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

            var hiddenList = this.hiddenGridUtil.DataSource == null ? new List<MultiRowDisplayModel>() : this.hiddenGridUtil.DataSource.ToList();
            var displayList = this.displayGridUtil.DataSource == null ? new List<MultiRowDisplayModel>() : this.displayGridUtil.DataSource.ToList();

            //選択した列を取得
            var rowIndex = this.DisplayDataGridView.SelectedCells[0].RowIndex;
            var col = displayList.First(x => x.Name == this.DisplayDataGridView[this.DisplayNameColumn.Name, rowIndex].Value.ToString());

            var scrollingRowIndex = this.GetScrollingRowIndex(this.DisplayDataGridView, rowIndex);

            //表示設定
            col.Visible = false;

            //並び順
            col.SortNo = null;

            //表示対象の切り替え
            hiddenList.Add(col);
            displayList.Remove(col);

            //データソース初期化
            this.hiddenGridUtil.DataSource = null;
            this.displayGridUtil.DataSource = null;

            //データソース
            this.hiddenGridUtil.DataSource = hiddenList;
            this.displayGridUtil.DataSource = displayList;

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

            var displayList = this.displayGridUtil.DataSource == null ? new List<MultiRowDisplayModel>() : this.displayGridUtil.DataSource.ToList();

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
            this.displayGridUtil.DataSource = null;

            //データソース
            this.displayGridUtil.DataSource = list;

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

            var displayList = this.displayGridUtil.DataSource == null ? new List<MultiRowDisplayModel>() : this.displayGridUtil.DataSource.ToList();

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
            this.displayGridUtil.DataSource = null;

            //データソース
            this.displayGridUtil.DataSource = list;

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

                //検索条件名可否が有効かどうか
                if (this.SearchConditionNameEnabled == true)
                {
                    //登録済みの検索条件で上書きしない場合は終了
                    if (this.GetUserSearchConditionList().Any() == true && Messenger.Confirm(string.Format(Resources.TCM00004, "検索条件名")) != DialogResult.Yes)
                    {
                        return;

                    }

                }

                var hiddenList = this.hiddenGridUtil.DataSource == null ? new List<MultiRowDisplayModel>() : this.hiddenGridUtil.DataSource.ToList();
                var displayList = this.displayGridUtil.DataSource == null ? new List<MultiRowDisplayModel>() : this.displayGridUtil.DataSource.ToList();

                var list = displayList.Concat(hiddenList).ToList();

                //表示順設定
                var i = 0;
                list.ForEach(x => x.DisplayIndex = i++);

                //ユーザー表示設定情報登録
                if (this.EntryUserSearchCondition(list) == true)
                {
                    //表示設定
                    this.CustumTemplate.Display = list;

                    //並び順
                    this.CustumTemplate.Sort = list.Where(x => x.Visible == true && x.SortNo != null).OrderBy(x => x.SortNo).Select(x => new MultiRowSortModel
                    {
                        //列名
                        Name = x.HeaderText,

                        //データプロパティ名
                        DataPropertyName = x.DataPropertyName,

                        //降順可否
                        IsDesc = !x.IsAsc

                    }).ToList();

                    //選択検索条件
                    this.SelectedUserSearchCondition = new ControlSheetSearchConditionModel { ユーザーID = SessionDto.UserId, 条件名 = this.SearchConditionNameTextBox.Text };

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
                var errMsg = new List<string>();

                //表示対象の列があるかどうか
                var displayList = this.displayGridUtil.DataSource == null ? new List<MultiRowDisplayModel>() : this.displayGridUtil.DataSource.ToList();
                if (displayList.Any() == false)
                {
                    //エラーメッセージ
                    errMsg.Add(Resources.TCM00001);

                }

                //同じソート順を指定しているかどうか
                var sortList = displayList.Where(x => x.SortNo != null).Select(x => x.SortNo.Value).ToArray();
                if (sortList.Count() != sortList.Distinct().Count())
                {
                    //エラーメッセージ
                    errMsg.Add(Resources.TCM00003);

                }

                return string.Join(Const.CrLf, errMsg.ToArray());

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
        /// ユーザー検索条件取得
        /// </summary>
        /// <returns></returns>
        private List<ControlSheetSearchConditionModel> GetUserSearchConditionList()
        {
            var cond = new ControlSheetSearchConditionSearchModel
            {
                //ユーザーID
                ユーザーID = SessionDto.UserId,

                //条件名
                条件名 = this.SearchConditionNameTextBox.Text

            };

            //APIで取得
            var res = HttpUtil.GetResponse<ControlSheetSearchConditionSearchModel, ControlSheetSearchConditionModel>(ControllerType.ControlSheetSearchCondition, cond);

            //レスポンスが取得できたかどうか
            var list = new List<ControlSheetSearchConditionModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// ユーザー検索条件登録
        /// </summary>
        /// <param name="target">ユーザー表示設定</param>
        /// <returns></returns>
        private bool EntryUserSearchCondition(IEnumerable<MultiRowDisplayModel> target)
        {
            //登録
            var res = HttpUtil.PutResponse(ControllerType.ControlSheetSearchCondition, this.GetUserSearchCondition(target));

            return res != null && res.Status == Const.StatusSuccess;

        }

        /// <summary>
        /// ユーザー検索条件取得
        /// </summary>
        /// <param name="target">ユーザー表示設定</param>
        /// <returns></returns>
        private List<ControlSheetSearchConditionModel> GetUserSearchCondition(IEnumerable<MultiRowDisplayModel> target)
        {
            var list = new List<ControlSheetSearchConditionModel>();

            //有効な検索条件を追加
            list.AddRange(this.SearchConditionList.Where(x => x.行番号 >= 0));

            //設定情報を追加
            list.Add(new ControlSheetSearchConditionModel
            {
                //行番号
                行番号 = ConfigRow,

                //表示列
                TEXT = string.Join(",", target.Where(x => x.Visible == true).Select(x => x.HeaderText).ToArray()),

                //ソート順
                CONJUNCTION = string.Join(",", target.Where(x => x.Visible == true && x.SortNo != null).OrderBy(x => x.SortNo).Select(x => x.HeaderText + (x.IsAsc == true ? "" : Desc)).ToArray()),

            });

            //条件名
            var name = string.IsNullOrWhiteSpace(this.SearchConditionNameTextBox.Text) == false ? this.SearchConditionNameTextBox.Text : DefaultName;

            //キーの設定
            list.ForEach(cond =>
            {
                //ユーザーID
                cond.ユーザーID = SessionDto.UserId;

                //条件名
                cond.条件名 = name;

            });

            return list;

        }
        #endregion

    }
}
