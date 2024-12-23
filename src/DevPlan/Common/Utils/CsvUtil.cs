using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using CsvHelper;

namespace DevPlan.UICommon.Utils
{
    /// <summary>
    /// CSV共通クラス
    /// </summary>
    public class CsvUtil : IDisposable
    {
        #region プロパティ
        /// <summary>CSV書き込み</summary>
        private CsvWriter Csv { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="csvPath">CSVパス</param>
        public CsvUtil(string csvPath)
            : this(csvPath, Const.Sjis, true)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="csvPath">CSVパス</param>
        /// <param name="encoding">文字コード</param>
        public CsvUtil(string csvPath, Encoding encoding)
            : this(csvPath, encoding, true)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="csvPath">CSVパス</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="isQuote">括り</param>
        public CsvUtil(string csvPath, Encoding encoding, bool isQuote)
        {
            //フォルダを作成
            FileUtil.CreateDirectory(csvPath);

            //CSV書き込みセット
            this.SetCsvWriter(new StreamWriter(csvPath, false, encoding), isQuote);

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="isQuote">括り</param>
        public CsvUtil(Stream stream)
            : this(stream, Const.Sjis, true)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="encoding">文字コード</param>
        public CsvUtil(Stream stream, Encoding encoding)
            : this(stream, encoding, true)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="isQuote">括り</param>
        public CsvUtil(Stream stream, Encoding encoding, bool isQuote)
            : this(new StreamWriter(stream, encoding), isQuote)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sw">ストリーム</param>
        /// <param name="isQuote">括り</param>
        public CsvUtil(StreamWriter sw, bool isQuote)
        {
            //CSV書き込みセット
            this.SetCsvWriter(sw, isQuote);

        }

        /// <summary>
        /// CSV書き込みセット
        /// </summary>
        /// <param name="sw">ストリーム</param>
        /// <param name="isQuote">括り</param>
        private void SetCsvWriter(StreamWriter sw, bool isQuote)
        {
            //CSV書き込み
            sw.AutoFlush = true;
            this.Csv = new CsvWriter(sw);

            //括り
            this.Csv.Configuration.QuoteAllFields = isQuote;

        }
        #endregion

        #region 破棄
        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            //ストリームがあるかどうか
            if (this.Csv != null)
            {
                //クローズして破棄
                this.Csv.Dispose();
                this.Csv = null;

            }
        }
        #endregion

        #region CSVを作成
        /// <summary>
        /// CSVを作成
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="list">リスト</param>
        /// <param name="funcs">変換</param>
        public void WriterList<T>(IEnumerable<T> list, params Func<T, IEnumerable<dynamic>>[] funcs)
        {
            this.WriterList(null, list, funcs);

        }

        /// <summary>
        /// CSVを作成
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="headers">ヘッダー</param>
        /// <param name="list">リスト</param>
        /// <param name="funcs">変換</param>
        public void WriterList<T>(IEnumerable<string> headers, IEnumerable<T> list, params Func<T, IEnumerable<dynamic>>[] funcs)
        {
            //ボディ部を変換
            var body = new List<IEnumerable<dynamic>>();

            foreach (var x in list)
            {
                foreach (var func in funcs)
                {
                    body.Add(func(x));
                }
            }

            this.Writer(headers, body);

        }

        /// <summary>
        /// CSVを作成
        /// </summary>
        /// <param name="body">ボディ</param>
        public void Writer(IEnumerable<IEnumerable<dynamic>> body)
        {
            this.Writer(null, body);

        }

        /// <summary>
        /// CSVを作成
        /// </summary>
        /// <param name="headers">ヘッダー</param>
        /// <param name="body">ボディ</param>
        public void Writer(IEnumerable<string> headers, IEnumerable<IEnumerable<dynamic>> body)
        {
            //ヘッダーがあれば書き込み
            if (headers != null && headers.Any() == true)
            {
                foreach (var x in headers)
                {
                    this.Csv.WriteField(x);

                }

                this.Csv.NextRecord();

            }

            //ボディがあれば書き込み
            if (body != null && body.Any() == true)
            {
                foreach (var row in body)
                {
                    foreach (var x in row)
                    {
                        this.Csv.WriteField(x);

                    }

                    this.Csv.NextRecord();

                }
                
            }

        }
        #endregion
    }
}
