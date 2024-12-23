using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace DevPlanWebAPI.Common
{
    /// <summary>
    /// エクセル共通クラス
    /// </summary>
    public class XlsUtil : IDisposable
    {
        #region エクセル形式
        /// <summary>エクセル形式</summary>
        public enum XlsFormat : int
        {
            /// <summary>Xls</summary>
            Xls,

            /// <summary>Xlsx</summary>
            Xlsx
        }
        #endregion

        #region メンバ変数
        private IWorkbook book;
        #endregion

        #region プロパティ
        /// <summary>エクセル形式</summary>
        public XlsFormat Format { get; private set; }

        /// <summary>エクセルストリーム</summary>
        public Stream XlsStream { get; private set; } = null;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public XlsUtil() : this(XlsFormat.Xlsx)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="format">エクセル形式</param>
        public XlsUtil(XlsFormat format)
        {
            //エクセル形式
            this.Format = format;

            //ブックの設定
            this.SetWorkbook();

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">パス</param>
        public XlsUtil(string path) : this(File.Open(path, FileMode.Open))
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">バイト配列</param>
        public XlsUtil(byte[] list) : this(new MemoryStream(list))
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="stream">ストリーム</param>
        public XlsUtil(Stream stream)
        {
            //ポジションを先頭に設定
            stream.Position = 0;

            //エクセルストリーム
            this.XlsStream = stream;

            //ブックの設定
            this.SetWorkbook();

        }
        #endregion

        #region 破棄
        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            //ブックがあるかどうか
            if (this.book != null)
            {
                //クローズして破棄
                this.book.Close();
                this.book = null;

            }

        }
        #endregion

        #region ブックの設定
        /// <summary>
        /// ブックの設定
        /// </summary>
        private void SetWorkbook()
        {
            //エクセルのストリームがあるかどうか
            if (this.XlsStream == null)
            {
                //Xlsxかどうか
                if (this.Format == XlsFormat.Xlsx)
                {
                    this.book = new XSSFWorkbook();

                }
                else
                {
                    this.book = new HSSFWorkbook();

                }

            }
            else
            {
                //ストリームから取得
                this.book = WorkbookFactory.Create(this.XlsStream);

            }

        }
        #endregion

        #region シートに書き込み
        /// <summary>
        /// シートに書き込み
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="sheetName">シート名</param>
        /// <param name="list">リスト</param>
        /// <param name="func">変換</param>
        /// <param name="startRow">開始位置</param>
        public void WriteSheet<T>(string sheetName, IEnumerable<T> list, Func<T, IDictionary<string, string>> func, int startRow = 0)
        {
            var index = startRow;

            //リストがあるかどうか
            if (list != null && list.Any() == true)
            {
                foreach (var row in list)
                {
                    //行
                    ++index;

                    //行データに置き換え
                    var map = func(row).ToDictionary(x => (x.Key + index), x => x.Value);

                    //シートに書き込み
                    this.WriteSheet(sheetName, map);

                }

            }

        }

        /// <summary>
        /// シートに書き込み
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="map">値</param>
        public void WriteSheet(string sheetName, IDictionary<string, string> map)
        {
            var sheet = this.GetSheet(sheetName);

            //書き込み対象があるかどうか
            if (map != null && map.Any() == true)
            {
                foreach (var kv in map)
                {
                    //セルに書き込み
                    var cr = new CellReference(kv.Key);
                    var row = CellUtil.GetRow(cr.Row, sheet);
                    var cell = CellUtil.GetCell(row, cr.Col);
                    cell.SetCellValue(kv.Value);

                }

            }

        }
        #endregion

        #region シートの取得
        /// <summary>
        /// シートの取得
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <returns></returns>
        private ISheet GetSheet(string sheetName)
        {
            //シートが存在しているかどうか
            var sheet = this.book.GetSheet(sheetName);
            if (sheet == null)
            {
                //シート作成
                sheet = this.book.CreateSheet(sheetName);

            }

            return sheet;

        }
        /// <summary>
        /// シートの取得
        /// </summary>
        /// <param name="sheetNumber">シート番号</param>
        /// <returns></returns>
        private ISheet GetSheet(int sheetNumber)
        {
            var sheet = this.book.GetSheetAt(sheetNumber);

            return sheet;

        }
        #endregion

        #region 書式の設定
        /// <summary>
        /// 横位置の設定(左寄せ)
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="range">範囲</param>
        public void SetAlignmentLeft(string sheetName, string range)
        {
            //セルの書式設定
            this.SetCellStyle(sheetName, range, (style =>
            {
                //横位置の設定
                style.Alignment = HorizontalAlignment.Left;

            }));

        }

        /// <summary>
        /// 横位置の設定(中寄せ)
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="range">範囲</param>
        public void SetAlignmentCenter(string sheetName, string range)
        {
            //セルの書式設定
            this.SetCellStyle(sheetName, range, (style =>
            {
                //横位置の設定
                style.Alignment = HorizontalAlignment.Center;

            }));

        }

        /// <summary>
        /// 横位置の設定(右寄せ)
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="range">範囲</param>
        public void SetAlignmentRight(string sheetName, string range)
        {
            //セルの書式設定
            this.SetCellStyle(sheetName, range, (style =>
            {
                //横位置の設定
                style.Alignment = HorizontalAlignment.Right;

            }));

        }

        /// <summary>
        /// 罫線の設定
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="range">範囲</param>
        public void SetBorder(string sheetName, string range)
        {
            //セルの書式設定
            this.SetCellStyle(sheetName, range, (style =>
            {
                //罫線の設定
                style.BorderTop = BorderStyle.Thin;
                style.BorderBottom = BorderStyle.Thin;
                style.BorderLeft = BorderStyle.Thin;
                style.BorderRight = BorderStyle.Thin;

            }));

        }

        /// <summary>
        /// セルの書式設定
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="range">範囲</param>
        /// <param name="action">セルの書式設定</param>
        private void SetCellStyle(string sheetName, string range, Action<ICellStyle> action)
        {
            var sheet = this.GetSheet(sheetName);

            var cellRange = CellRangeAddress.ValueOf(range);

            //指定されたレンジのセルに設定
            for (var i = cellRange.FirstRow; i <= cellRange.LastRow; i++)
            {
                var row = CellUtil.GetRow(i, sheet);

                for (var j = cellRange.FirstColumn; j <= cellRange.LastColumn; j++)
                {
                    var cell = CellUtil.GetCell(row, j);

                    //元の書式を取得
                    var style = book.CreateCellStyle();
                    style.CloneStyleFrom(cell.CellStyle);

                    //書式設定
                    action(style);

                    //書式反映
                    cell.CellStyle = style;

                }

            }

        }
        #endregion

        #region 列幅の自動調整
        /// <summary>
        /// 列幅の自動調整
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="range">範囲</param>
        public void AutoSizeColumn(string sheetName, string range)
        {
            var sheet = this.GetSheet(sheetName);

            var cellRange = CellRangeAddress.ValueOf(range);

            //列幅の自動調整
            for (var i = cellRange.FirstColumn; i <= cellRange.LastColumn; i++)
            {
                sheet.AutoSizeColumn(i);

            }

        }
        #endregion

        //#region ブックの保存
        ///// <summary>
        ///// ブックの保存
        ///// </summary>
        ///// <param name="path">パス</param>
        //public void Save(string path)
        //{
        //    //フォルダを作成
        //    FileUtil.CreateDirectory(path);

        //    //保存
        //    using (var fs = new FileStream(path, FileMode.OpenOrCreate))
        //    {
        //        this.book.Write(fs);

        //    }

        //}
        //#endregion

        #region 列インデックスの文字列を取得
        /// <summary>
        /// 列インデックスの文字列を取得
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns></returns>
        public string GetColumnAddress(int index)
        {
            return CellReference.ConvertNumToColString(index);

        }
        #endregion

        #region セル値を文字列で取得
        /// <summary>
        /// セル値を文字列で取得
        /// </summary>
        /// <param name="cell">セル</param>
        /// <returns></returns>
        private string GetCellStringValue(ICell cell)
        {
            var value = string.Empty;

            switch (cell?.CellType)
            {
                // 文字列
                case CellType.String:

                    value = cell.StringCellValue;

                    break;

                // 数値
                case CellType.Numeric:

                    // セルが日付情報が単なる数値かを判定
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        // 日付型
                        value = cell.DateCellValue.ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    else
                    {
                        // 数値型
                        value = cell.NumericCellValue.ToString();
                    }

                    break;

                // 論理
                case CellType.Boolean:

                    value = cell.BooleanCellValue.ToString();

                    break;

                // ブランク
                case CellType.Blank:

                    value = cell.ToString();

                    break;

                // 数式
                case CellType.Formula:

                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.String:

                            value = cell.StringCellValue;

                            break;

                        case CellType.Numeric:

                            if (DateUtil.IsCellDateFormatted(cell))
                            {
                                value = cell.DateCellValue.ToString("yyyy/MM/dd HH:mm:ss");
                            }
                            else
                            {
                                value = cell.NumericCellValue.ToString();
                            }

                            break;

                        case CellType.Boolean:

                            value = cell.BooleanCellValue.ToString();

                            break;

                        case CellType.Error:

                            value = cell.ErrorCellValue.ToString();

                            break;

                        default:

                            break;
                    }

                    break;

                // エラー
                case CellType.Error:

                    value = cell.ErrorCellValue.ToString();

                    break;

                default:

                    break;
            }

            return value;
        }
        #endregion
        
        #region 行情報リストを取得
        /// <summary>
        /// 行情報リストを取得
        /// </summary>
        /// <param name="sheetNumber">シート番号</param>
        /// <param name="startRowNumber">開始行番号</param>
        /// <param name="endRowNumber">最終行番号</param>
        /// <param name="startCellNumber">開始セル番号</param>
        /// <param name="endCellNumber">最終セル番号</param>
        public List<List<string>> GetRowsList(int sheetNumber, int? startRowNumber = null, int? endRowNumber = null, int? startCellNumber = null, int? endCellNumber = null)
        {
            var list = new List<List<string>>();
            var sheet = this.GetSheet(sheetNumber);

            var startrownum = startRowNumber == null ? sheet?.FirstRowNum : startRowNumber;
            var endrownum = endRowNumber == null ? sheet?.LastRowNum : endRowNumber;

            for (var i = startrownum; i <= endrownum; i++)
            {
                var row = sheet?.GetRow((int)i);
                var cells = new List<string>();

                var startcellnum = startCellNumber == null ? row?.FirstCellNum : startCellNumber;
                var endcellnum = endCellNumber == null ? row?.LastCellNum : endCellNumber;

                for (var j = startcellnum; j <= endcellnum; j++)
                {
                    var cell = sheet.GetRow((int)i).GetCell((int)j);

                    cells.Add(GetCellStringValue(cell));
                }

                list.Add(cells);
            }

            return list;
        }
        #endregion

        #region 行情報リストを取得
        /// <summary>
        /// 行情報リストを取得
        /// </summary>
        /// <param name="sheetNumber">シート番号</param>
        /// <param name="startRowNumber">開始行番号</param>
        /// <param name="endRowNumber">最終行番号</param>
        /// <param name="columns">カラム番号(英字)</param>
        public List<List<string>> GetRowsList(int sheetNumber, int? startRowNumber = null, int? endRowNumber = null, List<string> columns = null)
        {
            var list = new List<List<string>>();
            var sheet = this.GetSheet(sheetNumber);

            var startrownum = startRowNumber == null ? sheet?.FirstRowNum + 1 : startRowNumber;
            var endrownum = endRowNumber == null ? sheet?.LastRowNum + 1 : endRowNumber;

            for (var i = startrownum; i <= endrownum; i++)
            {
                var row = sheet?.GetRow((int)i);
                var cells = new List<string>();

                foreach(string column in columns)
                {
                    var c = new CellReference(column + i.ToString());
                    var r = CellUtil.GetRow(c.Row, sheet);
                    var cell = CellUtil.GetCell(r, c.Col);

                    //セル値を保存
                    cells.Add(GetCellStringValue(cell));
                }

                list.Add(cells);
            }

            return list;
        }
        #endregion

        #region 行のコピー
        /// <summary>
        /// 行のコピー
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="range">コピー元範囲</param>
        /// <param name="targetRow">コピー先</param>
        /// <param name="reiteration">繰り返し数</param>
        public void CopyRow(string sheetName, string range, int targetRow, int reiteration)
        {
            //コピー元
            var cellRange = CellRangeAddress.ValueOf(range);

            //コピー元行数
            int rows = (cellRange.LastRow - cellRange.FirstRow + 1);

            var sheet = this.GetSheet(sheetName);
            for (int i = 0; i < reiteration; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    //行のコピー
                    sheet.CopyRow(cellRange.FirstRow + j, targetRow + j + (rows * i));
                }
            }
        }
        #endregion

        #region セルの結合
        /// <summary>
        /// セルの結合
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="range">結合範囲</param>
        public void CombineCell(string sheetName, string range)
        {
            var sheet = this.GetSheet(sheetName);
            var cellRange = CellRangeAddress.ValueOf(range);
            sheet.AddMergedRegion(new CellRangeAddress(cellRange.FirstRow, cellRange.LastRow, cellRange.FirstColumn, cellRange.LastColumn));
        }
        #endregion

        #region 改ページ設定
        /// <summary>
        /// 改ページ設定
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="range"></param>
        public void SetRowBread(string sheetName, string range)
        {
            var sheet = this.GetSheet(sheetName);
            var cellRange = CellRangeAddress.ValueOf(range);
            sheet.SetRowBreak(cellRange.LastRow);
        }
        #endregion
    }
}
