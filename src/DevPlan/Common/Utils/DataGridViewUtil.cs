using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using System.Diagnostics;

namespace DevPlan.UICommon.Utils
{
    /// <summary>
    /// データグリッドビュー共通クラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataGridViewUtil<T>
    {
        #region 定数

        public const string FileName = "{0}_{1:yyyyMMddHHmmss}";

        public const string Filter = "Excel ブック (*.xlsx)|*.xlsx;";

        private const int MaxCount = 1000;

        private const int FilterXlsx = 1;
        private const int FilterCsv = 2;

        #endregion

        #region メンバ変数

        public DataGridView dataGridView = null;

        private List<DataGridViewComboBoxColumn> comboBoxList = new List<DataGridViewComboBoxColumn>();

        private List<DataGridViewColumn> displayList = new List<DataGridViewColumn>();

        private BindingSource bindingSource = new BindingSource();

        private IEnumerable<DataGridViewSortModel> sortList = null;

        private IEnumerable<DataGridViewDisplayModel> display = null;

        #endregion

        #region プロパティ
        /// <summary>行</summary>
        public DataGridViewRowCollection Rows { get { return this.dataGridView.Rows; } }

        /// <summary>ソート可否</summary>
        public bool IsSort { get; private set; }

        /// <summary>ソート可否</summary>
        public bool IsColumnResize { get; set; } = true;

        /// <summary>ソート可否</summary>
        public bool IsRowResize { get; set; } = true;

        /// <summary>データソース</summary>
        public IEnumerable<T> DataSource
        {
            get { return this.bindingSource.DataSource as IEnumerable<T>; }
            set { this.SetDataSource(value); }

        }

        /// <summary>ソート順</summary>
        public IEnumerable<DataGridViewSortModel> Sort
        {
            get { return this.sortList; }
            set
            {
                var list = this.DataSource;

                var sortList = value;

                //並び順があるかどうか
                if (sortList == null || sortList.Any() == false)
                {
                    this.sortList = null;

                }
                else
                {
                    this.sortList = sortList;

                }

                //データがあるかどうか
                this.SetDataSource(list != null && list.Any() == true ? list.ToList() : null, false, true, false);

            }

        }

        /// <summary>表示</summary>
        public IEnumerable<DataGridViewDisplayModel> Display
        {
            get
            {
                return this.displayList.Where(x => string.IsNullOrWhiteSpace(x.HeaderText) == false).Select(x => new DataGridViewDisplayModel
                {
                    //列名
                    Name = x.Name,

                    //ヘッダー名
                    HeaderText = x.HeaderText,

                    //データプロパティ名
                    DataPropertyName = x.DataPropertyName,

                    //列固定可否
                    Frozen = x.Frozen,

                    //表示
                    Visible = x.Visible,

                    //表示順
                    DisplayIndex = x.DisplayIndex

                }).ToArray();

            }
            set
            {
                //要素があるかどうか
                var list = value;
                if (list == null || list.Any() == false)
                {
                    list = null;

                }
                else
                {
                    //描画停止
                    this.dataGridView.SuspendLayout();

                    //先頭の列までスクロール
                    this.dataGridView.HorizontalScrollingOffset = int.MaxValue;

                    //列を初期化
                    this.displayList.ForEach(x =>
                    {
                        x.Visible = false;
                        x.Frozen = string.IsNullOrWhiteSpace(x.HeaderText) == true ? x.Frozen :false;

                    });

                    //表示する列の設定
                    var i = 0;
                    foreach (var d in this.displayList.Where(x => string.IsNullOrWhiteSpace(x.HeaderText) == true).OrderBy(x => x.DisplayIndex))
                    {
                        var col = displayList.First(x => x.Name == d.Name);

                        //表示
                        col.Visible = true;

                        //表示順
                        col.DisplayIndex = i++;

                    }

                    foreach (var d in list.Where(x => x.Visible == true).OrderBy(x => x.DisplayIndex))
                    {
                        var col = displayList.First(x => x.Name == d.Name);

                        //表示
                        col.Visible = d.Visible;

                        //列固定
                        col.Frozen = d.Frozen;

                        //表示順
                        col.DisplayIndex = i++;

                    }

                    //列の幅を再設定
                    this.dataGridView.AutoResizeColumns(this.dataGridView.RowCount < MaxCount ? DataGridViewAutoSizeColumnsMode.AllCells : DataGridViewAutoSizeColumnsMode.DisplayedCells);

                    //ソートが有効かどうか
                    if (this.IsSort == true)
                    {
                        //先頭の列幅を調整
                        this.displayList.OrderBy(x => x.DisplayIndex).First().Width += 1;

                    }

                    //描画再開
                    this.dataGridView.ResumeLayout();

                }

                this.display = list;

            }

        }

        /// <summary>編集可否</summary>
        public bool IsEdit { get; private set; } = false;

        /// <summary>編集データ</summary>
        public List<T> EditList { get; private set; } = new List<T>();

        /// <summary>行数</summary>
        public int RowCount { get { return this.dataGridView.RowCount; } }

        /// <summary>
        /// 出力行データを加工します。
        /// </summary>
        public Func<List<dynamic>, List<dynamic>> TreatmentRows { get; set; }
        
        /// <summary>
        /// 出力列ヘッダーを加工します。
        /// </summary>
        public Func<List<string>, List<string>> TreatmentHeaders { get; set; }
        #endregion

        #region コンストラクタ

        /// <summary>
        /// ファイルダウンロード専用の暫定コンストラクタ
        /// </summary>
        public DataGridViewUtil()
        { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataGridView">データグリッドビュー</param>
        /// <param name="isSort">ソート可否</param>
        public DataGridViewUtil(DataGridView dataGridView, bool isSort = true)
        {
            //ソート可否
            this.IsSort = isSort;

            //データグリッドビュー
            this.dataGridView = dataGridView;

            //データーソース
            this.dataGridView.DataSource = this.bindingSource;

            //ダブルバァッファリング有効化
            var type = typeof(DataGridView);
            var pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGridView, true);

            var autoSizeColumnsMode = this.dataGridView.AutoSizeColumnsMode;

            //データグリッドビューの設定
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.AllowDrop = false;
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = autoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill ? DataGridViewAutoSizeColumnsMode.Fill : DataGridViewAutoSizeColumnsMode.None;
            this.dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.dataGridView.MultiSelect = false;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

            //データグリッドビューの列ヘッダーの設定
            this.dataGridView.ColumnHeadersVisible = true;
            this.dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            //データグリッドビューの行ヘッダーの設定
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //データグリッドビューのセルの設定
            this.dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            var sortMode = this.IsSort == true ? DataGridViewColumnSortMode.Programmatic : DataGridViewColumnSortMode.NotSortable;

            foreach (DataGridViewColumn col in this.dataGridView.Columns)
            {
                var autoSizeMode = col.AutoSizeMode;

                //列幅がFILLか継承無し以外は自動調整なし
                switch (autoSizeMode)
                {
                    case DataGridViewAutoSizeColumnMode.Fill:
                    case DataGridViewAutoSizeColumnMode.NotSet:
                        break;

                    default:
                        autoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        break;

                }

                //列の設定
                col.AutoSizeMode = autoSizeMode;
                col.Resizable = DataGridViewTriState.False;
                col.SortMode = sortMode;

                //ヘッダーの改行コードを置き換え
                var headerText = col.HeaderText;
                col.HeaderText = headerText.Replace(@"\n", Const.CrLf);

                //改行がある場合は折り返し
                col.HeaderCell.Style.WrapMode = col.HeaderText != headerText ? DataGridViewTriState.True : DataGridViewTriState.False;

                //コンボボックスの列かどうか
                if (col is DataGridViewComboBoxColumn)
                {
                    var cmbCol = col as DataGridViewComboBoxColumn;

                    //コンボボックスの列の設定
                    cmbCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                    //コンボボックスの列を取得
                    this.comboBoxList.Add(cmbCol);

                }

                //表示していてる列かどうか
                if (col.Visible == true)
                {
                    displayList.Add(col);

                }

            }

            //列ヘッダーの高さを再設定
            this.dataGridView.AutoResizeColumnHeadersHeight();

            //列の幅を再設定
            this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            //先頭列の幅を調整
            this.displayList.First(x => x.Visible == true).Width += 1;

            //行の高さを再設定
            this.dataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            //イベントの設定
            this.dataGridView.CellClick += this.DataGridView_CellClick;
            this.dataGridView.CellValidating += this.DataGridView_CellValidating;
            this.dataGridView.CellValueChanged += this.DataGridView_CellValueChanged;
            this.dataGridView.CurrentCellDirtyStateChanged += dataGridView_CurrentCellDirtyStateChanged;
            this.dataGridView.DataError += this.DataGridView_DataError;
            this.dataGridView.EditingControlShowing += this.DataGridView_EditingControlShowing;

        }
        #endregion

        #region データソースの設定
        /// <summary>
        /// データソースの設定
        /// </summary>
        /// <param name="list">データ</param>
        /// <param name="isColumnResize">列リサイズ可否</param>
        /// <param name="isRowResize">行リサイズ可否</param>
        /// <param name="initEdit">編集初期化</param>
        private void SetDataSource(IEnumerable<T> list, bool isColumnResize = true, bool isRowResize = true, bool initEdit = true)
        {
            var bindingList = new BindingList<T>();

            //描画停止
            this.dataGridView.SuspendLayout();

            //データがあるかどうか
            if (list == null || list.Any() == false)
            {
                bindingList = null;

            }
            else
            {
                //ソート順を設定
                bindingList = new BindingList<T>(this.GetSortData(list, this.Sort).ToList());

            }

            //列の表示状態を取得
            var colMap = new Dictionary<DataGridViewColumn, bool>();
            foreach (DataGridViewColumn col in this.dataGridView.Columns)
            {
                colMap[col] = col.Visible;

            }

            //データソース
            this.bindingSource.DataSource = bindingList;

            //列の表示状態を設定
            foreach (DataGridViewColumn col in this.dataGridView.Columns)
            {
                col.Visible = colMap[col];

            }

            //一覧を未選択状態にセット
            this.dataGridView.CurrentCell = null;

            //ソートのグリフ設定
            this.SetSortGlyphDirection(this.Sort);

            //列のリサイズをするかどうか
            if (isColumnResize == true && IsColumnResize == true)
            {
                //列の幅を再設定
                this.dataGridView.AutoResizeColumns(this.dataGridView.RowCount < MaxCount ? DataGridViewAutoSizeColumnsMode.AllCells : DataGridViewAutoSizeColumnsMode.DisplayedCells);

                //先頭の列幅を調整
                this.displayList.OrderBy(x => x.DisplayIndex).First().Width += 1;

            }

            //行のリサイズをするかどうか
            if (isRowResize == true && IsRowResize == true)
            {
                //行の高さを再設定
                this.dataGridView.AutoResizeRows(this.dataGridView.RowCount < MaxCount ? DataGridViewAutoSizeRowsMode.AllCells : DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders);

            }

            //編集状態を初期化するかどうか
            if (initEdit == true)
            {
                //編集可否
                this.IsEdit = false;

                //編集データ
                this.EditList = new List<T>();

            }

            //描画再開
            this.dataGridView.ResumeLayout();

        }
        #endregion

        #region イベント
        /// <summary>
        /// データグリッドセルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //ソート無効なら終了
            if (this.IsSort == false)
            {
                return;

            }

            //列ヘッダー以外か無効な列なら終了
            if (e.RowIndex != -1 || e.ColumnIndex < 0)
            {
                return;

            }

            //バインドしていない列なら終了
            var col = this.dataGridView.Columns[e.ColumnIndex];
            if (string.IsNullOrWhiteSpace(col.DataPropertyName) == true)
            {
                return;

            }

            //一覧を未選択状態にセット
            this.dataGridView.CurrentCell = null;

            var isDesc = false;

            //ソート順が無いか1つかどうか
            if (this.Sort != null && this.Sort.Any() == true && this.Sort.Count() == 1)
            {
                //指定済みなら並び順を反転
                var sort = this.Sort.First();
                if (sort.Name == col.HeaderText)
                {
                    isDesc = !sort.IsDesc;

                }

            }

            //ソート順
            this.Sort = new[] { new DataGridViewSortModel { Name = col.HeaderText, DataPropertyName = col.DataPropertyName, IsDesc = isDesc } };

        }

        /// <summary>
        /// データグリッドセル値検証
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            //列の取得
            var row = this.dataGridView.Rows[e.RowIndex];
            var col = this.dataGridView.Columns[e.ColumnIndex];
            var type = col.ValueType;

            //読み取り専用なら検証なし
            if (this.dataGridView.ReadOnly == true || row.ReadOnly == true || col.ReadOnly == true)
            {
                return;

            }
            //テキストボックスの列以外は検証なし
            else if ((col is DataGridViewTextBoxColumn) == false)
            {
                return;

            }
            //日付型は検証なし
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return;

            }
            //文字列型は検証なし
            else if (type == typeof(string))
            {
                return;

            }

            //編集コントロールが取得できなければ終了
            var edit = this.dataGridView.EditingControl;
            if (edit == null)
            {
                return;

            }

            //背景色をデフォルトに設定
            var cell = this.dataGridView[e.ColumnIndex, e.RowIndex];
            cell.Style.BackColor = Const.DefaultBackColor;

            //入力されているかどうか
            var value = e.FormattedValue;
            var s = value == null ? "" : value.ToString();
            if (string.IsNullOrWhiteSpace(s) == false)
            {
                //数値に変換できるかどうか
                var i = 0;
                var flg = int.TryParse(s, out i);
                if (flg == false)
                {
                    //背景色変更
                    edit.BackColor = Const.ErrorBackColor;
                    cell.Style.BackColor = Const.ErrorBackColor;

                    //入力エラー文言表示
                    Messenger.Warn(string.Format(Resources.KKM00025, col.HeaderText.Replace(Const.CrLf, "")));

                }

                //キャンセル可否
                e.Cancel = !flg;

            }

        }

        /// <summary>
        /// データグリッドセル値変更後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行か列の場合は何もしない
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            //バインドしていない列の場合は終了
            var col = this.dataGridView.Columns[e.ColumnIndex];
            if (string.IsNullOrWhiteSpace(col.DataPropertyName) == true)
            {
                return;

            }

            //編集可否
            this.IsEdit = true;

            //設定済の行かどうか
            var row = this.DataSource.ElementAt(e.RowIndex);
            if (this.EditList.Contains(row) == false)
            {
                this.EditList.Add(row);

            }

        }

        /// <summary>
        /// データグリッドセル値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //コミット済みなら終了
            if (this.dataGridView.IsCurrentCellDirty == false)
            {
                return;

            }

            //チェックボックスの列なら編集内容をコミット
            var col = this.dataGridView.Columns[this.dataGridView.CurrentCell.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                this.dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                return;

            }

        }

        /// <summary>
        /// データグリッド値エラー時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //コンボボックスの列かどうか
            var col = this.dataGridView.Columns[e.ColumnIndex];
            if (this.comboBoxList.Any(x => x.Name == col.Name) == true)
            {
                //例外は無視
                e.ThrowException = false;

                //キャンセル
                e.Cancel = true;

            }
            else
            {
                //例外はスロー
                e.ThrowException = true;

            }

        }

        /// <summary>
        /// データグリッド編集コントロール表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var cell = this.dataGridView.CurrentCell;

            var col = cell.OwningColumn;
            var type = col.ValueType;

            //テキストボックスの列以外は終了
            if ((col is DataGridViewTextBoxColumn) == false)
            {
                return;

            }
            //文字列型は検証なし
            else if (type == typeof(string))
            {
                return;

            }
            //書式が設定されてなければ終了
            else if (string.IsNullOrWhiteSpace(col.DefaultCellStyle.Format) == true)
            {
                return;

            }

            //編集テキストボックスが取得できなければ終了
            var text = e.Control as DataGridViewTextBoxEditingControl;
            if (text == null)
            {
                return;

            }

            //書式適用前の値を設定
            text.Text = cell.Value == null ? "" : cell.Value.ToString();

        }
        #endregion

        #region データソースの変更を通知
        /// <summary>
        /// データソースの変更を通知
        /// </summary>
        /// <param name="index">インデックス</param>
        public void DataSourceResetItem(int index)
        {
            //変更を通知
            this.bindingSource.ResetItem(index);

            //編集可否
            this.IsEdit = true;

            //設定済の行かどうか
            var row = this.DataSource.ElementAt(index);
            if (this.EditList.Contains(row) == false)
            {
                this.EditList.Add(row);

            }

        }
        #endregion

        #region ファイルに書き込み
        /// <summary>
        /// ファイルに書き込み
        /// </summary>
        /// <param name="format">書式</param>
        /// <param name="args">値</param>
        public void SaveFile(string format = FileName, params object[] args)
        {
            this.SaveFile(string.Format(format, args));

        }

        /// <summary>
        /// ファイルの作成
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        public void SaveFile(string fileName)
        {
            var rows = this.dataGridView.Rows.Cast<DataGridViewRow>().ToArray();

            //データがあるかどうか
            if (rows == null || rows.Any() == false)
            {
                Messenger.Warn(Resources.TCM03008);

            }
            else
            {
                using (var sfd = new SaveFileDialog { Filter = Filter, FileName = fileName })
                {
                    //選択されたかどうか
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        //ファイルがロックされているかどうか
                        if (FileUtil.IsFileLocked(sfd.FileName) == true)
                        {
                            Messenger.Warn(Resources.TCM03022);
                            return;

                        }

                        var columns = this.dataGridView.Columns.Cast<DataGridViewColumn>().Where(x => x.Visible == true && string.IsNullOrWhiteSpace(x.HeaderText) == false).OrderBy(x => x.DisplayIndex).ToArray();

                        //拡張子ごとの分岐
                        switch (sfd.FilterIndex)
                        {
                            //XLSX
                            case FilterXlsx:
                                //XLSXに書き込み
                                this.SaveXlsx(sfd.FileName, columns, rows);
                                break;

                            //CSV
                            case FilterCsv:
                                //CSVに書き込み
                                this.SaveCsv(sfd.FileName, columns, rows);
                                break;

                        }

                    }

                }

            }

        }

        /// <summary>
        /// 共通XLSXに書き込み
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="columns">列</param>
        /// <param name="rows">行</param>
        private void SaveXlsx(string path, IEnumerable<DataGridViewColumn> columns, IEnumerable<DataGridViewRow> rows)
        {
            
            using (var xls = new XlsUtil())
            {
                var sheetName = "Sheet1";

                var cols = GetHeaderText(columns);

                var headers = cols.Select((x, i) => new { Key = string.Format("{0}1", xls.GetColumnAddress(i)), Value = x }).ToArray();

                var lastColumn = xls.GetColumnAddress(headers.Count() - 1);

                var headerRange = string.Format("A1:{0}1", lastColumn);

                //ヘッダーを書き込み
                var headerMap = headers.ToDictionary(x => x.Key, x => x.Value);
                xls.WriteSheet(sheetName, headerMap);

                //罫線の設定
                xls.SetBorder(sheetName, headerRange);
                xls.CopyRow(sheetName, headerRange, string.Format("A2:{0}{1}", lastColumn, rows.Count() + 1));

                //シートに書き込み
                xls.WriteSheets(sheetName, rows, (row => this.GetRowData(columns, row).Select((x, i) => new { Key = xls.GetColumnAddress(i), Value = x }).ToDictionary(x => x.Key, x => x.Value)), 1);

                //ヘッダーを中寄
                xls.SetAlignmentCenter(sheetName, string.Format("A1:{0}1", lastColumn));

                //列幅の自動調整
                xls.AutoSizeColumn(sheetName, string.Format("A:{0}", lastColumn));

                //Excelフッターの書き込み
                xls.SetFooter(sheetName, string.Format("社外転用禁止\n{0} {1} {2}", DateTime.Now.ToString("yyyy/MM/dd HH:mm"), SessionDto.SectionCode, SessionDto.UserName));

                //ブックの保存
                xls.Save(path);

            }

        }

        /// <summary>
        /// CSVに書き込み
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="columns">列</param>
        /// <param name="rows">行</param>
        private void SaveCsv(string path, IEnumerable<DataGridViewColumn> columns, IEnumerable<DataGridViewRow> rows)
        {
            using (var csv = new CsvUtil(path))
            {
                //CSV作成
                var headers = GetHeaderText(columns);
                csv.WriterList(headers, rows, (row => this.GetRowData(columns, row)));

            }

        }
        #endregion

        #region 列ヘッダー文字列取得
        /// <summary>
        /// 列ヘッダー文字列取得
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        private List<string> GetHeaderText(IEnumerable<DataGridViewColumn> columns)
        {
            List<string> ret = columns.Select((x) => x.HeaderText).ToList();

            if (TreatmentHeaders != null)
            {
                ret = TreatmentHeaders.Invoke(ret);
            }

            return ret;
        }
        #endregion

        #region 行データを取得
        /// <summary>
        /// 行データを取得
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="row">行</param>
        /// <returns></returns>
        private List<dynamic> GetRowData(IEnumerable<DataGridViewColumn> columns, DataGridViewRow row)
        {
            var list = new List<dynamic>();

            foreach (var col in columns)
            {
                var cell = row.Cells[col.Name];

                var cellValue = string.IsNullOrWhiteSpace(col.DefaultCellStyle.Format) == false ? cell.EditedFormattedValue : cell.Value;

                
                if(col.Tag != null && col.Tag.ToString() == "ExcelCellType(Numbers)")
                {
                    double d;
                    if (cellValue == null) list.Add("");
                    else if (double.TryParse(cellValue.ToString(), out d)) list.Add(d);
                    else list.Add(cellValue.ToString());
                }
                else
                {
                    list.Add(cellValue == null ? "" : cellValue.ToString());
                }

            }

            if (TreatmentRows != null)
            {
                list = TreatmentRows.Invoke(list);
            }

            return list;

        }
        #endregion

        #region ソート
        /// <summary>
        /// ソート対象を取得
        /// </summary>
        /// <returns></returns>
        public List<DataGridViewSortModel> GetSortTarget()
        {
            var map = new Dictionary<DataGridViewSortModel, int>();

            foreach (DataGridViewColumn col in this.dataGridView.Columns)
            {
                //表示されていて列名が表示されていてバインドした列が対象
                if (col.Visible == true && string.IsNullOrWhiteSpace(col.HeaderText) == false && string.IsNullOrWhiteSpace(col.DataPropertyName) == false)
                {
                    map[new DataGridViewSortModel { Name = col.HeaderText, DataPropertyName = col.DataPropertyName, IsDesc = false }] = col.DisplayIndex;

                }

            }

            return map.OrderBy(x => x.Value).Select(x => x.Key).ToList();

        }

        /// <summary>
        /// ソートしたデータを取得
        /// </summary>
        /// <param name="target">対象</param>
        /// <param name="sortList">ソート順</param>
        /// <returns></returns>
        private IEnumerable<T> GetSortData(IEnumerable<T> target, IEnumerable<DataGridViewSortModel> sortList)
        {
            //データが無ければ終了
            if (target == null || target.Any() == false || sortList == null || sortList.Any() == false)
            {
                return target;

            }

            Func<DataGridViewSortModel, Func<T, object>> expression = s => item => item.GetType().GetProperty(s.DataPropertyName).GetValue(item);

            //最初のソート順
            var sort = sortList.First();
            var keySelector = expression(sort);
            var list = sort.IsDesc == false ? target.OrderBy(keySelector) : target.OrderByDescending(keySelector);

            foreach (var s in sortList.Skip(1))
            {
                //2つ目以降のソート順を設定
                var selector = expression(s);
                list = s.IsDesc == false ? list.ThenBy(selector) : list.ThenByDescending(selector);

            }

            return list;

        }

        /// <summary>
        /// ソートのグリフ設定
        /// </summary>
        /// <param name="sortList">ソート順</param>
        private void SetSortGlyphDirection(IEnumerable<DataGridViewSortModel> sortList)
        {
            //ソート対象が無ければ終了
            if (sortList == null || sortList.Any() == false)
            {
                return;

            }

            foreach (DataGridViewColumn  col in this.dataGridView.Columns)
            {
                //グリフ初期化
                col.HeaderCell.SortGlyphDirection = SortOrder.None;

            }

            //ソートのグリフ設定
            foreach (var s in sortList)
            {
                var col = this.dataGridView.Columns.Cast<DataGridViewColumn>().First(x => x.HeaderText == s.Name);
                col.HeaderCell.SortGlyphDirection = s.IsDesc == true ? SortOrder.Descending : SortOrder.Ascending;

            }

        }
        #endregion
    }

}
