using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Properties;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.UICommon.Utils
{
    /// <summary>
    /// MultiRowを使用したユーティリティクラス
    /// </summary>
    public class MultiRowUtil
    {
        #region メンバ変数

        private GcMultiRow multiRow;

        #endregion

        #region 定数

        private const string FileName = "{0}_{1:yyyyMMddHHmmss}";

        private const string Filter = "Excel ブック (*.xlsx)|*.xlsx;";

        private const int MaxCount = 1000;

        private const int FilterXlsx = 1;
        private const int FilterCsv = 2;

        #endregion

        #region 公開プロパティ

        /// <summary>
        /// Excel出力オブジェクト
        /// </summary>
        public ToExcel Excel { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="multiRow"></param>
        public MultiRowUtil(GcMultiRow multiRow)
        {
            this.multiRow = multiRow;
            this.Excel = new ToExcel(this.multiRow);
        }
        #endregion

        #region Excel出力
        /// <summary>
        /// Excel出力のカプセル化クラス
        /// </summary>
        public class ToExcel
        {
            #region メンバ変数

            private GcMultiRow multiRow;

            #endregion

            #region 公開プロパティ

            /// <summary>
            /// 出力列取得デリゲート（指定しない場合は既定のロジックで取得します。）
            /// ※並び順はGetCols、GetHeads共に合わせること
            /// </summary>
            public Func<IEnumerable<Column>> GetCols { get; set; }

            /// <summary>
            /// 出力列ヘッダー文字列取得デリゲート（指定しない場合は既定のロジックで取得します。）
            /// ※並び順はGetCols、GetHeads共に合わせること
            /// </summary>
            public Func<List<string>> GetHeads { get; set; }

            /// <summary>
            /// 出力行取得デリゲート（指定しない場合は全行となります。）
            /// </summary>
            public Func<IEnumerable<Row>> GetRows { get; set; }

            /// <summary>
            /// 出力行データを加工します。
            /// </summary>
            public Func<List<List<dynamic>>, List<List<dynamic>>> TreatmentRows { get; set; }

            /// <summary>
            /// 出力列ヘッダーを加工します。
            /// </summary>
            public Func<List<string>, List<string>> TreatmentHeaders { get; set; }

            #endregion

            #region コンストラクタ
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="multiRow"></param>
            public ToExcel(GcMultiRow multiRow)
            {
                this.multiRow = multiRow;
            }
            #endregion

            #region 出力対象データが存在するか？
            /// <summary>
            /// 出力対象データが存在するか？
            /// </summary>
            /// <returns></returns>
            public bool HasData()
            {
                if (this.Rows().Count() == 0)
                {
                    Messenger.Warn(Resources.TCM03008);
                    return false;
                }

                return true;
            }
            #endregion

            #region 出力先パス取得
            /// <summary>
            /// 出力先パス取得
            /// </summary>
            /// <param name="fileName">ファイル名</param>
            /// <returns></returns>
            public string GetPath(string fileName)
            {
                using (var sfd = new SaveFileDialog { Filter = Filter, FileName = fileName })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // ファイルがロックされている場合
                        if (FileUtil.IsFileLocked(sfd.FileName) == true)
                        {
                            Messenger.Warn(Resources.TCM03022);
                            return "";
                        }

                        return sfd.FileName;
                    }

                    return "";
                }
            }
            #endregion

            #region 出力
            /// <summary>
            /// Excel出力
            /// </summary>
            /// <param name="format">書式</param>
            /// <param name="args">値</param>
            public void Out(string format = FileName, params object[] args)
            {
                this.Out(string.Format(format, args));
            }

            /// <summary>
            /// Excel出力
            /// </summary>
            /// <param name="fileName">ファイル名</param>
            public void Out(string fileName)
            {
                if (this.HasData() == true)
                {
                    var filePath = this.GetPath(fileName);
                    if (string.IsNullOrEmpty(filePath) == false)
                    {
                        var columns = Columns().OrderBy(x => x.Index);
                        var headers = Headers(columns);

                        using (var xls = new XlsUtil())
                        {
                            this.CovertToExcel(xls, filePath, Rows(Rows().Select((x) => GetData(columns, x)).ToList()), headers);
                        }
                    }
                }
            }
            #endregion

            #region 内部メソッド

            /// <summary>
            /// 出力列取得
            /// </summary>
            /// <returns></returns>
            private IEnumerable<Column> Columns()
            {
                if (this.GetCols != null)
                {
                    return this.GetCols.Invoke();
                }

                return this.multiRow.Columns.Cast<Column>()
                                .Where(x =>
                                    x.IsCollapsed == false &&
                                    string.IsNullOrWhiteSpace(Convert.ToString(this.multiRow.ColumnHeaders[0].Cells[x.Index].Value)) == false &&
                                    this.multiRow.ColumnHeaders[0].Cells[x.Index].Visible == true &&
                                    x.Name != "RowHeader");
            }

            /// <summary>
            /// 出力列ヘッダー文字列取得
            /// </summary>
            /// <param name="cols"></param>
            /// <returns></returns>
            private List<string> Headers(IEnumerable<Column> cols)
            {
                List<string> ret;

                if (this.GetHeads != null)
                {
                    ret = this.GetHeads.Invoke();
                }
                else
                {
                    ret = cols.Select((x) => this.multiRow.ColumnHeaders[0].Cells[x.Index].Value.ToString()).ToList();
                }

                if (TreatmentHeaders != null)
                {
                    return TreatmentHeaders.Invoke(ret);
                }

                return cols.Select((x) => this.multiRow.ColumnHeaders[0].Cells[x.Index].Value.ToString()).ToList();
            }

            /// <summary>
            /// 出力行取得
            /// </summary>
            /// <returns></returns>
            private IEnumerable<Row> Rows()
            {
                if (this.GetRows != null)
                {
                    return this.GetRows.Invoke();
                }

                return this.multiRow.Rows.Cast<Row>();
            }

            /// <summary>
            /// 出力行取得
            /// </summary>
            /// <param name="rows"></param>
            /// <returns></returns>
            private List<List<dynamic>> Rows(List<List<dynamic>> rows)
            {
                if (TreatmentRows != null)
                {
                    return TreatmentRows.Invoke(rows);
                }

                return rows;
            }

            /// <summary>
            /// 対象行と対象列から出力データを取得
            /// </summary>
            /// <param name="columns">列</param>
            /// <param name="row">行</param>
            /// <returns></returns>
            private List<dynamic> GetData(IEnumerable<Column> columns, Row row)
            {
                var list = new List<dynamic>();

                foreach (var col in columns)
                {
                    var cell = row.Cells[col.Name];

                    var cellValue = string.IsNullOrWhiteSpace(col.DefaultCellStyle.Format) == false ? cell.EditedFormattedValue : cell.Value;

                    var trg = new string[] { "yyyy/MM/dd", "yyyy/M/d", "yy/MM/dd", "yy/M/d", "MM/dd", "M/d" };

                    // Excel出力(数値フォーマット)
                    if (col.Tag != null && col.Tag.ToString() == "ExcelCellType(Numbers)")
                    {
                        double d;
                        if (cellValue == null) list.Add("");
                        else if (double.TryParse(cellValue.ToString(), out d)) list.Add(d);
                        else list.Add(cellValue.ToString());
                    }
                    // Excel出力(日付フォーマット)
                    else
                    {
                        if (cell is TextBoxCell)
                        {
                            var text = (TextBoxCell)cell;
                            cellValue = trg.Contains(text.Style.Format) && cellValue != null ? Convert.ToDateTime(text.Value).ToString("yy/M/d") : cellValue;
                        }
                        else if (cell is DateTimePickerCell)
                        {
                            var date = (DateTimePickerCell)cell;

                            if (date.Format == DateTimePickerFormat.Short)
                            {
                                cellValue = cellValue != null ? Convert.ToDateTime(date.Value).ToString("yyyy/MM/dd") : cellValue;
                            }
                            else if (date.Format == DateTimePickerFormat.Custom)
                            {
                                //Update Start 2021/10/12 杉浦 カーシェア車Excel出力処理
                                //cellValue = trg.Contains(date.CustomFormat) && cellValue != null ? Convert.ToDateTime(date.Value).ToString("yy/M/d") : cellValue;
                                //Update Start 2021/12/21 杉浦 Excel出力処理修正
                                //cellValue = trg.Contains(date.CustomFormat) && !string.IsNullOrEmpty(cellValue.ToString()) ? Convert.ToDateTime(date.Value).ToString("yy/M/d") : cellValue;
                                cellValue = trg.Contains(date.CustomFormat) && (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString())) ? Convert.ToDateTime(date.Value).ToString("yy/M/d") : cellValue;
                                //Update End 2021/12/21 杉浦 Excel出力処理修正
                                //Update End 2021/10/12 杉浦 カーシェア車Excel出力処理
                            }
                        }

                        list.Add(cellValue == null ? "" : cellValue.ToString());

                    }
                }

                return list;

            }

            /// <summary>
            /// 共通Excelに変換
            /// </summary>
            /// <param name="path">ファイル名</param>
            /// <param name="rows">データ</param>
            /// <param name="headers">ヘッダー</param>
            /// <param name="startRow">データ行の開始位置</param>
            private void CovertToExcel(XlsUtil xls, string path, List<List<dynamic>> rows, List<string> headers)
            {
                var sheetName = "Sheet1";

                var lastColumn = xls.GetColumnAddress(headers.Count() - 1);

                var headerRange = string.Format("A1:{0}1", lastColumn);

                // ヘッダーを書き込み
                var headerMap = headers
                    .Select((x, i) => new { Key = string.Format("{0}1", xls.GetColumnAddress(i)), Value = x })
                    .ToDictionary(x => x.Key, x => x.Value);
                xls.WriteSheet(sheetName, headerMap);

                // 罫線の設定
                xls.SetBorder(sheetName, headerRange);
                xls.CopyRow(sheetName, headerRange, string.Format("A2:{0}{1}", lastColumn, rows.Count() + 1));

                // シートに書き込み
                var rowNo = 2;
                foreach (var row in rows)
                {
                    var rowMap = row
                        .Select((x, i) => new { Key = string.Format("{0}{1}", xls.GetColumnAddress(i), rowNo.ToString()), Value = x })
                        .ToDictionary(x => x.Key, x => x.Value);
                    xls.WriteSheet(sheetName, rowMap);

                    rowNo++;
                }

                // ヘッダーを中寄
                xls.SetAlignmentCenter(sheetName, string.Format("A1:{0}1", lastColumn));

                // 列幅の自動調整
                xls.AutoSizeColumn(sheetName, string.Format("A:{0}", lastColumn));

                // フッターの設定
                xls.SetFooter(sheetName, string.Format("社外転用禁止\n{0} {1} {2}", DateTime.Now.ToString("yyyy/MM/dd HH:mm"), SessionDto.SectionCode, SessionDto.UserName));

                // ブックの保存
                xls.Save(path);
            }

            #endregion
        }
        #endregion
    }
}
