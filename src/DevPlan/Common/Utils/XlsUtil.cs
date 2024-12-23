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

using DevPlan.UICommon;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Properties;

namespace DevPlan.UICommon.Utils
{
    /// <summary>
    /// エクセル共通クラス
    /// </summary>
    public class XlsUtil : IDisposable
    {
        #region メンバ変数
        protected IWorkbook book;

        protected ISheet sheet;
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
        /// シートに書き込み（文字列）
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
        /// シートに書き込み(文字列・数値混在用)
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="sheetName">シート名</param>
        /// <param name="list">リスト</param>
        /// <param name="func">変換</param>
        /// <param name="startRow">開始位置</param>
        public void WriteSheets<T>(string sheetName, IEnumerable<T> list, Func<T, IDictionary<string, dynamic>> func, int startRow = 0)
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
        /// シートに書き込み（数値）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheetName"></param>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <param name="startRow"></param>
        public void WriteSheet<T>(string sheetName, IEnumerable<T> list, Func<T, IDictionary<string, double>> func, int startRow = 0)
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
        /// シートに書き込み（数値）
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="map"></param>
        public void WriteSheet(string sheetName, IDictionary<string, double> map)
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

        /// <summary>
        /// シートに書き込み（文字列）
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

        /// <summary>
        /// シートに書き込み
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="map">値</param>
        public void WriteSheet(string sheetName, IDictionary<string, dynamic> map)
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

        /// <summary>
        /// シートに書き込み
        /// </summary>
        /// <param name="map">値</param>
        public void WriteSheet(IDictionary<string, dynamic> map)
        {
            foreach (var kv in map)
            {
                if (kv.Key == null || kv.Value == null)
                {
                    continue;
                }

                // セルに書き込み
                var cr = new CellReference(kv.Key);
                var row = CellUtil.GetRow(cr.Row, this.sheet);
                var cell = CellUtil.GetCell(row, cr.Col);
                cell.SetCellValue(kv.Value as string);
            }
        }

        /// <summary>
        /// シートに書き込み(数式)
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="map">値</param>
        public void WriteSheetFormula(string sheetName, IDictionary<string, string> map)
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
                    cell.SetCellFormula(kv.Value);
                }

            }

        }
        #endregion

        #region シートの設定
        /// <summary>
        /// シートの設定
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <returns></returns>
        public void SetSheet(string sheetName)
        {
            this.sheet = this.book.GetSheet(sheetName);

            // シートが存在しているかどうか
            if (this.sheet == null)
            {
                //シート作成
                this.sheet = this.book.CreateSheet(sheetName);
            }
        }
        /// <summary>
        /// シートの設定
        /// </summary>
        /// <param name="sheetNumber">シート番号</param>
        /// <returns></returns>
        public void SetSheet(int sheetNumber)
        {
            this.sheet = this.book.GetSheetAt(sheetNumber);
        }
        #endregion

        #region シートの取得
        /// <summary>
        /// シートの取得
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <returns></returns>
        protected ISheet GetSheet(string sheetName)
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

        #region シートの選択
        /// <summary>
        /// シートの選択
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="flg">選択フラグ</param>
        /// /// <returns></returns>
        public void Select(string sheetName, bool flg = true)
        {
            var sheet = this.book.GetSheet(sheetName);
            //Append Start 2020/12/24 杉浦
            //再計算の実施
            sheet.ForceFormulaRecalculation = true;
            //Update End 2020/12/24 杉浦

            sheet.IsSelected = flg;
        }
        /// <summary>
        /// シートの選択
        /// </summary>
        /// <param name="sheetNumber">シート番号</param>
        /// <param name="flg">選択フラグ</param>
        /// /// <returns></returns>
        public void Select(int sheetNumber, bool flg = true)
        {
            var sheet = this.book.GetSheetAt(sheetNumber);

            sheet.IsSelected = flg;
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

        #region ブックの保存
        /// <summary>
        /// ブックの保存
        /// </summary>
        /// <param name="path">パス</param>
        public void Save(string path)
        {
            //ファイルがロックされているかどうか
            if (FileUtil.IsFileLocked(path) == true)
            {
                Messenger.Warn(Resources.KKM00044);
                return;

            }

            //フォルダを作成
            FileUtil.CreateDirectory(path);

            //保存
            using (var fs = new FileStream(path, FileMode.Create))
            {
                this.book.Write(fs);

                if (fs.CanWrite == true)
                {
                    fs.Flush();

                }

            }

            Messenger.Info(Resources.KKM00046); // 保存完了
        }
        #endregion

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
        /// <param name="sheetIndex">シートインデックス</param>
        /// <param name="rowIndex">行インデックス</param>
        /// <param name="colIndex">列インデックス</param>
        /// <returns></returns>
        public string GetCellStringValue(int sheetIndex, int rowIndex, int colIndex)
        {
            var sheet = GetSheet(sheetIndex);
            var cell = sheet.GetRow(rowIndex)?.GetCell(colIndex);

            if (cell == null)
            {
                return "";
            }

            return GetCellStringValue(cell);
        }

        /// <summary>
        /// セル値を文字列で取得
        /// </summary>
        /// <param name="cell">セル</param>
        /// <returns></returns>
        private string GetCellStringValue(ICell cell)
        {
            var value = "";

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
                        value = DateTimeUtil.ConvertDateTimeString(cell.DateCellValue);

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

                // 数式
                case CellType.Formula:

                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.String:
                            value = cell.StringCellValue;
                            break;

                        case CellType.Numeric:
                            // セルが日付情報が単なる数値かを判定
                            if (DateUtil.IsCellDateFormatted(cell))
                            {
                                value = DateTimeUtil.ConvertDateTimeString(cell.DateCellValue);

                            }
                            else
                            {
                                value = cell.NumericCellValue.ToString();

                            }
                            break;

                        case CellType.Boolean:
                            value = cell.BooleanCellValue.ToString();
                            break;

                        default:
                            value = "";
                            break;

                    }
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

        #region 行のコピー
        /// <summary>
        /// 行のコピー
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="copyRange">コピー元範囲</param>
        /// <param name="targetRange">コピー先範囲</param>
        public void CopyRow(string sheetName, string copyRange, string targetRange)
        {
            var fromRange = CellRangeAddress.ValueOf(copyRange);

            var toRange = CellRangeAddress.ValueOf(targetRange);

            var sheet = this.GetSheet(sheetName);

            var rowCount = fromRange.LastRow - fromRange.FirstRow + 1;

            var i = 0;

            for (var row = toRange.FirstRow; row <= toRange.LastRow; row++)
            {
                var from = fromRange.FirstRow;

                //コピー元が1行かどうか
                if (rowCount != 1)
                {
                    //コピー元の行をリセット
                    if (i > rowCount)
                    {
                        i = 0;

                    }
                    else
                    {
                        from = from + i++;

                    }

                }

                //行のコピー
                sheet.CopyRow(from, row);

                //行の高さを設定
                var fromRow = CellUtil.GetRow(from, sheet);
                var toRow = CellUtil.GetRow(row, sheet);
                toRow.Height = fromRow.Height;

            }

        }
        #endregion

        #region セルの結合
        /// <summary>
        /// セルの結合
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="range">範囲</param>
        public void MergeCell(string sheetName, string range)
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
        /// <param name="range">範囲</param>
        public void SetRowBreak(string sheetName, string range)
        {
            var sheet = this.GetSheet(sheetName);
            var cellRange = CellRangeAddress.ValueOf(range);
            sheet.SetRowBreak(cellRange.LastRow);

        }
        #endregion

        #region フッターの設定
        /// <summary>
        /// フッターの設定
        /// </summary>
        /// <param name="sheetName">シート名</param>
        /// <param name="text">表示内容</param>
        public void SetFooter(string sheetName, string text)
        {
            var sheet = this.GetSheet(sheetName);
            sheet.Footer.Right = text;
        }
        #endregion
    }

    #region CAPエクスポート用クラス
    /// <summary>
    /// CAPエクスポート用クラス
    /// </summary>
    public class CapExportXls : XlsUtil
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list"></param>
        public CapExportXls(byte[] list) : base(list)
        {

        }
        #endregion

        #region 行リスト⇒列リストに変換します。
        /// <summary>
        /// 行リスト⇒列リストに変換します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <param name="startRow"></param>
        /// <returns></returns>
        private Tuple<List<List<string>>, Dictionary<string, string>> ConvertRowsToCols<T>(IEnumerable<T> list, Func<T, IDictionary<string, string>> func, int startRow)
        {
            // 行リスト⇒列リストに変換
            var keyCols = new List<string[]>();

            // 値リスト
            var vals = new Dictionary<string, string>();

            // 列数
            var colCount = func(list.First()).Count;

            // 行数
            var rowCount = list.Count();

            // 列リスト
            for (var i = 0; i < colCount; i++)
            {
                // 初期化
                var index = startRow;

                var col = new string[rowCount];

                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    //行
                    ++index;

                    //行データ
                    var map = func(list.ToList()[rowIndex]).ToDictionary(x => (x.Key + index), x => x.Value);
                    col[rowIndex] = map.Keys.ToList()[i];
                    vals.Add(col[rowIndex], map[col[rowIndex]]);
                }

                keyCols.Add(col);
            }

            return Tuple.Create(keyCols.Select((x) => x.ToList()).ToList(), vals);
        }
        #endregion

        #region 報告書シート
        /// <summary>
        /// 報告書シート書式設定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <param name="startRow"></param>
        public void SetStyleReport<T>(IEnumerable<T> list, Func<T, IDictionary<string, string>> func, int startRow = 0)
        {
            var sheet = GetSheet("報告書");

            // 変換
            var tuple = ConvertRowsToCols(list, func, startRow);

            // 列リスト
            var colList = tuple.Item1;

            // 値リスト
            var vals = tuple.Item2;

            #region 報告書シート列インデックス
            const int REPORT_COLUMN_車種 = 0;
            const int REPORT_COLUMN_No = 1;
            //Update Start 2020/12/24 杉浦
            //Update Start 2021/07/05 矢作
            //const int REPORT_COLUMN_重要度 = 2;
            //const int REPORT_COLUMN_項目 = 3;
            //const int REPORT_COLUMN_詳細 = 4;
            //const int REPORT_COLUMN_供試車 = 5;
            //const int REPORT_COLUMN_CAP結果 = 6;
            //const int REPORT_COLUMN_フォロー状況 = 7;
            //const int REPORT_COLUMN_CAP時期 = 8;
            //const int REPORT_COLUMN_試験部署 = 9;
            //const int REPORT_COLUMN_試験対応策 = 10;
            //const int REPORT_COLUMN_試験日程 = 11;
            //const int REPORT_COLUMN_設計部署 = 12;
            //const int REPORT_COLUMN_設計対応策 = 13;
            //const int REPORT_COLUMN_供試品 = 14;
            //const int REPORT_COLUMN_設計日程 = 15;
            //const int REPORT_COLUMN_織込時期 = 16;

            //const int REPORT_COLUMN_CAP種別 = 2;
            //const int REPORT_COLUMN_重要度 = 3;
            //const int REPORT_COLUMN_項目 = 4;
            //const int REPORT_COLUMN_詳細 = 5;
            //const int REPORT_COLUMN_供試車 = 6;
            //const int REPORT_COLUMN_CAP結果 = 7;
            //const int REPORT_COLUMN_フォロー状況 = 8;
            //const int REPORT_COLUMN_CAP時期 = 9;
            //const int REPORT_COLUMN_試験部署 = 10;
            //const int REPORT_COLUMN_試験対応策 = 11;
            //const int REPORT_COLUMN_試験日程 = 12;
            //const int REPORT_COLUMN_設計部署 = 13;
            //const int REPORT_COLUMN_設計対応策 = 14;
            //const int REPORT_COLUMN_供試品 = 15;
            //const int REPORT_COLUMN_設計日程 = 16;
            //const int REPORT_COLUMN_織込時期 = 17;

            const int REPORT_COLUMN_CAP種別 = 2;
            const int REPORT_COLUMN_重要度 = 3;
            const int REPORT_COLUMN_項目 = 4;
            const int REPORT_COLUMN_詳細 = 5;
            const int REPORT_COLUMN_供試車 = 6;
            const int REPORT_COLUMN_CAP結果 = 7;
            const int REPORT_COLUMN_フォロー状況 = 8;
            const int REPORT_COLUMN_CAP時期 = 9;
            const int REPORT_COLUMN_方向付け確定期限 = 10;
            const int REPORT_COLUMN_試験部署 = 11;
            const int REPORT_COLUMN_試験対応策 = 12;
            const int REPORT_COLUMN_試験日程 = 13;
            const int REPORT_COLUMN_設計部署 = 14;
            const int REPORT_COLUMN_設計対応策 = 15;
            const int REPORT_COLUMN_供試品 = 16;
            const int REPORT_COLUMN_設計日程 = 17;
            const int REPORT_COLUMN_織込時期 = 18;

            //Update End 2020/12/24 杉浦
            //Update End 2021/07/05 矢作
            #endregion

            #region 項目データか？
            Func<int, bool> IsItemData = (index) =>
               {
                   return
                   index == REPORT_COLUMN_車種 ||
                   index == REPORT_COLUMN_No ||
                   //Append Start 2020/12/24 杉浦
                   index == REPORT_COLUMN_CAP種別 ||
                   //Append End 2020/12/24 杉浦
                   index == REPORT_COLUMN_重要度 ||
                   index == REPORT_COLUMN_項目 ||
                   index == REPORT_COLUMN_詳細 ||
                   index == REPORT_COLUMN_供試車 ||
                   index == REPORT_COLUMN_CAP結果 ||
                   index == REPORT_COLUMN_フォロー状況 ||
                   index == REPORT_COLUMN_CAP時期;
               };
            #endregion

            #region 文字位置は左寄せか？
            Func<int, bool> IsAlignmentLeft = (index) =>
              {
                  return index == REPORT_COLUMN_項目 || index == REPORT_COLUMN_詳細 || index == REPORT_COLUMN_試験対応策 || index == REPORT_COLUMN_設計対応策;
              };
            #endregion

            #region 背景パターン設定対象か？
            Func<int, string, bool> IsTargetBackPatten = (index, val) =>
                {
                    if (val != " ")
                    {
                        return false;
                    }

                    return
                    index == REPORT_COLUMN_試験対応策 ||
                    index == REPORT_COLUMN_試験日程 ||
                    index == REPORT_COLUMN_設計対応策 ||
                    index == REPORT_COLUMN_供試品 ||
                    index == REPORT_COLUMN_設計日程 ||
                    index == REPORT_COLUMN_織込時期;
                };
            #endregion

            #region 罫線設定
            Func<int, ICellStyle, ICellStyle> setBorder = (i, style) =>
            {
                style.BorderTop = BorderStyle.Thin;
                style.BorderBottom = BorderStyle.Thin;

                //Update Start 2020/12/24 杉浦
                //if (i == REPORT_COLUMN_No || i == REPORT_COLUMN_重要度 || i == REPORT_COLUMN_項目 || i == REPORT_COLUMN_詳細 || i == REPORT_COLUMN_試験対応策 || i == REPORT_COLUMN_試験日程 ||
                if (i == REPORT_COLUMN_No || i == REPORT_COLUMN_CAP種別 || i == REPORT_COLUMN_重要度 || i == REPORT_COLUMN_項目 || i == REPORT_COLUMN_詳細 || i == REPORT_COLUMN_試験対応策 || i == REPORT_COLUMN_試験日程 ||
                    //Update End 2020/12/24 杉浦
                    //Update Start 2021/07/05 矢作
                    //i == REPORT_COLUMN_設計対応策 || i == REPORT_COLUMN_供試品 || i == REPORT_COLUMN_設計日程 || i == REPORT_COLUMN_フォロー状況 || i == REPORT_COLUMN_CAP時期)
                    i == REPORT_COLUMN_設計対応策 || i == REPORT_COLUMN_供試品 || i == REPORT_COLUMN_設計日程 || i == REPORT_COLUMN_フォロー状況 || i == REPORT_COLUMN_CAP時期 || i == REPORT_COLUMN_方向付け確定期限)
                    //Update End 2021/07/05 矢作
                {
                    style.BorderLeft = BorderStyle.Thin;
                    return style;
                }

                if (i == REPORT_COLUMN_車種 || i == REPORT_COLUMN_供試車 || i == REPORT_COLUMN_試験部署 || i == REPORT_COLUMN_設計部署 || i == REPORT_COLUMN_CAP結果)
                {
                    style.BorderLeft = BorderStyle.Medium;
                    return style;
                }

                if (i == REPORT_COLUMN_織込時期)
                {
                    style.BorderRight = BorderStyle.Medium;
                    style.BorderLeft = BorderStyle.Thin;
                    return style;
                }

                style.BorderLeft = BorderStyle.Thin;
                style.BorderRight = BorderStyle.Thin;
                return style;
            };
            #endregion

            #region セルスタイル
            // 各列のセルスタイル作成
            var styleList = new List<ICellStyle>();
            for (var i = 0; i < colList.Count; i++)
            {
                var style = book.CreateCellStyle();

                // 罫線
                setBorder(i, style);

                // フォント
                style.WrapText = true;
                IFont font = book.CreateFont();
                font.FontHeightInPoints = 10;
                font.FontName = "ＭＳ Ｐゴシック";
                style.SetFont(font);

                // 文字位置
                style.Alignment = IsAlignmentLeft(i) ? HorizontalAlignment.Left : HorizontalAlignment.Center;
                style.VerticalAlignment = VerticalAlignment.Top;

                styleList.Add(style);
            }
            #endregion

            // セル結合範囲
            var margeRange = new List<int[]>();

            // 最終行
            int lastRow = CellRangeAddress.ValueOf(string.Format("{0}:{1}", colList[0].Last(), colList[0].Last())).LastRow;

            // 列ごとに書式設定
            for (var i = 0; i < colList.Count; i++)
            {
                // セル範囲
                var colCells = colList[i].TakeWhile((x, y) => y == 0 || (vals[colList[REPORT_COLUMN_No][y]] == vals[colList[REPORT_COLUMN_No][y - 1]] && vals[colList[REPORT_COLUMN_車種][y]] == vals[colList[REPORT_COLUMN_車種][y - 1]]));

                var margeRangeCount = 0;
                var margeIndex = 0;

                do
                {
                    var start = colCells.First();
                    var end = colCells.Last();

                    //No・車種でセル結合範囲を決める
                    if (i == REPORT_COLUMN_車種 || i == REPORT_COLUMN_No)
                    {
                        // 結合範囲の開始と終了のペアを格納
                        margeRange.Add(new int[] { colList[i].FindIndex((x) => x == start), colList[i].FindIndex((x) => x == end) });
                    }

                    // セル結合対象の場合
                    //Update Start 2020/12/24 杉浦
                    //if (i == REPORT_COLUMN_重要度 || i == REPORT_COLUMN_項目 || i == REPORT_COLUMN_詳細 || i == REPORT_COLUMN_供試車 ||
                    if (i == REPORT_COLUMN_CAP種別 || i == REPORT_COLUMN_重要度 || i == REPORT_COLUMN_項目 || i == REPORT_COLUMN_詳細 || i == REPORT_COLUMN_供試車 ||
                    //Update End 2020/12/24 杉浦
                        i == REPORT_COLUMN_CAP結果 || i == REPORT_COLUMN_フォロー状況 || i == REPORT_COLUMN_CAP時期)
                    {
                        start = colList[i][margeRange[margeRangeCount][0]];
                        end = colList[i][margeRange[margeRangeCount][1]];
                    }

                    var range = string.Format("{0}:{1}", start, end);
                    var cellRange = CellRangeAddress.ValueOf(range);

                    if (IsItemData(i))
                    {
                        // セル結合
                        sheet.AddMergedRegion(new CellRangeAddress(cellRange.FirstRow, cellRange.LastRow, cellRange.FirstColumn, cellRange.LastColumn));

                        //Update Start 2021/03/19 杉浦 CAPの出力EXCELでフィルタを掛けた際のデータ数がおかしい
                        //Delete Start 2021/02/15 杉浦
                        //// データ削除（報告書利用部署アプリ対応）
                        //for (var j = cellRange.FirstRow + 1; j <= cellRange.LastRow; j++)
                        //{
                        //    CellUtil.GetCell(CellUtil.GetRow(j, sheet), cellRange.FirstColumn).SetCellValue(string.Empty);
                        //}
                        //Delete End 2021/02/15 杉浦 CAPの出力EXCELでフィルタを掛けた際のデータ数がおかしい
                        // データ削除（報告書利用部署アプリ対応）
                        //Update Start 2021/06/25 Excelの絞り込み単行対応を車種・指摘No・種別・重要度のみにしてほしい
                        //if (i == REPORT_COLUMN_車種 || i == REPORT_COLUMN_No || i == REPORT_COLUMN_CAP種別 || i == REPORT_COLUMN_重要度 || i == REPORT_COLUMN_項目 || i == REPORT_COLUMN_詳細 || i == REPORT_COLUMN_供試車)
                        if (i == REPORT_COLUMN_車種 || i == REPORT_COLUMN_No || i == REPORT_COLUMN_CAP種別 || i == REPORT_COLUMN_重要度)
                        //Update End 2021/06/25 Excelの絞り込み単行対応を車種・指摘No・種別・重要度のみにしてほしい
                        {
                            for (var j = cellRange.FirstRow + 1; j <= cellRange.LastRow; j++)
                            {
                                CellUtil.GetCell(CellUtil.GetRow(j, sheet), cellRange.FirstColumn).SetCellValue(string.Empty);
                            }
                        }
                        //Update End 2021/03/19 杉浦 CAPの出力EXCELでフィルタを掛けた際のデータ数がおかしい
                    }

                    // 書式設定
                    for (var ii = cellRange.FirstRow; ii <= cellRange.LastRow; ii++)
                    {
                        var row = CellUtil.GetRow(ii, sheet);

                        var cell = CellUtil.GetCell(row, i + 1);

                        // その列の書式を取得
                        var style = styleList[i];

                        // 背景パターン
                        if (IsTargetBackPatten(i, cell.StringCellValue))
                        {
                            var tmp = book.CreateCellStyle();
                            tmp.CloneStyleFrom(style);
                            tmp.FillPattern = FillPattern.LeastDots;
                            style = tmp;
                        }

                        // 最終行
                        if (ii == lastRow)
                        {
                            var tmp = book.CreateCellStyle();
                            tmp.CloneStyleFrom(style);
                            tmp.BorderBottom = BorderStyle.Medium;
                            style = tmp;
                        }

                        // 設定
                        cell.CellStyle = style;
                    }

                    // 次のセル範囲取得
                    if (i == REPORT_COLUMN_車種 || i == REPORT_COLUMN_No)
                    {
                        margeIndex += colCells.Count();
                        colCells = colList[i].SkipWhile((x) => x != end).Skip(1).TakeWhile((x, y) => y == 0 || (vals[colList[REPORT_COLUMN_No][y + margeIndex]] == vals[colList[REPORT_COLUMN_No][y + margeIndex - 1]] && vals[colList[REPORT_COLUMN_車種][y + margeIndex]] == vals[colList[REPORT_COLUMN_車種][y + margeIndex - 1]]));
                    }
                    else
                    {
                        colCells = colList[i].SkipWhile((x) => x != end).Skip(1).TakeWhile((x, y) => y == 0);
                    }

                    margeRangeCount++;

                } while (colCells.Any());
            }
        }
        #endregion

        #region 既存確認シート
        /// <summary>
        /// 既存確認シート書式設定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <param name="startRow"></param>
        public void SetStyleExistConfirm<T>(IEnumerable<T> list, Func<T, IDictionary<string, string>> func, int startRow = 0)
        {
            var sheet = GetSheet("既存確認");

            // 列リスト
            var colList = ConvertRowsToCols(list, func, startRow).Item1;

            #region フォント
            Func<int, IFont, IFont> getFont = (i, font) =>
            {
                // 基本
                font.FontHeightInPoints = 10;

                return font;
            };
            #endregion

            #region 罫線
            Func<int, ICellStyle, ICellStyle> setBorder = (i, style) =>
            {
                style.BorderTop = BorderStyle.Thin;
                style.BorderBottom = BorderStyle.Thin;

                //Update Start 2022/04/21 杉浦 CAP既存確認の罫線がおかしい
                //if (i == 1 || i == 2 || i == 3 || i == 4 || i == 6 || i == 8 || i == 10 || i == 11 || i == 12 || i == 13 || i == 14)
                if (i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 7 || i == 9 || i == 11 || i == 12 || i == 13 || i == 14 || i == 15)
                //Update End 2022/04/21 杉浦 CAP既存確認の罫線がおかしい
                {
                    style.BorderLeft = BorderStyle.Thin;
                    return style;
                }

                //Update Start 2022/04/21 杉浦 CAP既存確認の罫線がおかしい
                //if (i == 0 || i == 5 || i == 7 || i == 9)
                if (i == 0 || i == 6 || i == 8 || i == 10)
                //Update End 2022/04/21 杉浦 CAP既存確認の罫線がおかしい
                {
                    style.BorderLeft = BorderStyle.Medium;
                    return style;
                }

                //Update Start 2022/04/21 杉浦 CAP既存確認の罫線がおかしい
                //if (i == 15)
                if (i == 16)
                //Update End 2022/04/21 杉浦 CAP既存確認の罫線がおかしい
                {
                    style.BorderRight = BorderStyle.Medium;
                    style.BorderLeft = BorderStyle.Thin;
                    return style;
                }

                return style;
            };
            #endregion

            #region セルスタイル

            // 各列のセルスタイル作成
            var styleList = new List<ICellStyle>();
            for (var i = 0; i < colList.Count; i++)
            {
                var style = book.CreateCellStyle();

                // 折り返し
                style.WrapText = true;

                // フォント
                IFont font = book.CreateFont();
                font.FontName = "ＭＳ Ｐゴシック";
                style.SetFont(getFont(i, font));

                // 罫線
                setBorder(i, style);

                // 文字位置
                style.Alignment = i == 1 || i == 2 ? HorizontalAlignment.Center : HorizontalAlignment.Left;
                style.VerticalAlignment = VerticalAlignment.Top;

                styleList.Add(style);
            }

            // 背景色設定済の各列のセルスタイル作成
            var styleListClose = styleList.Select((x) =>
             {
                 var tmp = book.CreateCellStyle();
                 tmp.CloneStyleFrom(x);
                 tmp.FillPattern = FillPattern.SolidForeground;
                 tmp.FillForegroundColor = IndexedColors.Grey25Percent.Index;
                 return tmp;
             }).ToList();

            #endregion

            // 列ごとに書式設定
            for (var i = 0; i < colList.Count; i++)
            {
                var range = string.Format("{0}:{1}", colList[i].First(), colList[i].Last());
                var cellRange = CellRangeAddress.ValueOf(range);

                // 書式設定
                for (var ii = cellRange.FirstRow; ii <= cellRange.LastRow; ii++)
                {
                    var row = CellUtil.GetRow(ii, sheet);

                    for (var j = cellRange.FirstColumn; j <= cellRange.LastColumn; j++)
                    {
                        var cell = CellUtil.GetCell(row, j);

                        // その列の書式を取得
                        var style = styleList[i];

                        // 最終行
                        if (ii == cellRange.LastRow)
                        {
                            var tmp = book.CreateCellStyle();
                            tmp.CloneStyleFrom(style);
                            tmp.BorderBottom = BorderStyle.Medium;
                            style = tmp;
                        }

                        // 設定
                        cell.CellStyle = style;
                    }

                    // 行の高さ
                    row.Height = 280;
                }
            }
        }
        #endregion
    }
    #endregion
}
