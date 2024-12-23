using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon
{
    /// <summary>定数クラス</summary>
    public class Const
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static Const()
        {
            //お気に入りの最大登録数
            FavoriteEntryMax = int.Parse(ConfigurationManager.AppSettings["FavoriteEntryMax"]);

            //スケジュール項目の最大行数
            ScheduleItemRowMax = int.Parse(ConfigurationManager.AppSettings["ScheduleItemRowMax"]);

        }
        #endregion

        #region 制御文字
        /// <summary>TAB</summary>
        public const string Tab = "\t";

        /// <summary>CR</summary>
        public const string Cr = "\r";

        /// <summary>LF</summary>
        public const string Lf = "\n";

        /// <summary>CRLF</summary>
        public const string CrLf = "\r\n";
        #endregion

        #region 文字コード
        /// <summary>SJIS</summary>
        public static readonly Encoding Sjis = Encoding.GetEncoding("shift_jis");

        /// <summary>UTF-8(BOMあり)</summary>
        public static readonly Encoding Utf8 = new UTF8Encoding(true);

        /// <summary>UTF-8(BOMなし)</summary>
        public static readonly Encoding Utf8N = new UTF8Encoding(false);
        #endregion

        #region カルチャー
        /// <summary>日本語-日本</summary>
        public static readonly CultureInfo JaJp = new CultureInfo("ja-JP");

        /// <summary>英語-米国</summary>
        public static readonly CultureInfo EnUs = new CultureInfo("en-US");
        #endregion

        #region APIのステータス
        /// <summary>失敗値（共通）</summary>
        public const string StatusFailure = "failure";

        /// <summary>偽値（共通）</summary>
        public const string StatusFalse = "false";

        /// <summary>成功値（共通）</summary>
        public const string StatusSuccess = "success";

        /// <summary>真値（共通）</summary>
        public const string StatusTrue = "true";
        #endregion

        #region 入力チェックで使用するタグ
        /// <summary>項目名</summary>
        public const string ItemName = "ItemName";

        /// <summary>必須</summary>
        public const string Required = "Required";

        /// <summary>全角文字列</summary>
        public const string Wide = "Wide";

        /// <summary>半角文字列</summary>
        public const string Narrow = "Narrow";

        /// <summary>数値</summary>
        public const string Number = "Number";

        /// <summary>日付</summary>
        public const string Date = "Date";

        /// <summary>年月</summary>
        public const string Month = "Month";

        /// <summary>正規表現</summary>
        public const string Regex = "Regex";

        /// <summary>バイト数</summary>
        public const string Byte = "Byte";
        #endregion

        #region MultiRowのヘッダで使用するタグ
        /// <summary>表示順</summary>
        public const string DisplayIndex = "DisplayIndex";

        /// <summary>データ名</summary>
        public const string DataField = "DataField";
        #endregion

        #region 色
        /// <summary>コントロールの背景色</summary>
        public static readonly Color ControlBackColor = Color.FromKnownColor(KnownColor.Control);

        /// <summary>デフォルトの背景色</summary>
        public static readonly Color DefaultBackColor = Color.FromKnownColor(KnownColor.Window);

        /// <summary>エラー時の背景色</summary>
        public static readonly Color ErrorBackColor = Color.FromArgb(0xFF, 0xFF, 0x9B);
        #endregion

        #region 環境変数
        /// <summary>お気に入りの最大登録数</summary>
        public static readonly int FavoriteEntryMax;

        /// <summary>スケジュール項目の最大行数</summary>
        public static readonly int ScheduleItemRowMax;
        #endregion

        #region 書式
        /// <summary>年月</summary>
        public const string FormatMonth = "yyyy/MM";

        /// <summary>月日</summary>
        public const string FormatMonthDate = "MM/dd";

        /// <summary>日付</summary>
        public const string FormatDate = "yyyy/MM/dd";

        /// <summary>日時</summary>
        public const string FormatDateTime = "yyyy/MM/dd HH:mm:ss";

        #endregion

        #region 配列
        /// <summary>スケジュール詳細画面の開始時刻</summary>
        public static readonly string[] ScheduleDetailStartTimeList = (new[] { "" }).Concat(Enumerable.Range(6, 16).Select(x => x.ToString("D2"))).ToArray();
        /// <summary>スケジュール詳細画面の終了時刻</summary>
        public static readonly string[] ScheduleDetailEndTimeList = (new[] { "" }).Concat(Enumerable.Range(7, 16).Select(x => x.ToString("D2"))).ToArray();
        #endregion

        #region 試験車日程
        /// <summary>使用部署要望案</summary>
        public const string Youbou = "要望";

        /// <summary>SJSB調整案</summary>
        public const string Tyousei = "１次調整";

        /// <summary>最終調整結果</summary>
        public const string Kettei = "決定";

        /// <summary>仮予約</summary>
        public const string Kariyoyaku = "仮予約";

        /// <summary>仮</summary>
        public const string Kari = "(仮)";
        #endregion

        #region お気に入り
        /// <summary>お気に入り(全て)</summary>
        public const string FavoriteAll = "00";

        /// <summary>お気に入り(業務計画表)</summary>
        public const string FavoriteWork = "01";

        /// <summary>お気に入り(月次計画表)</summary>
        public const string FavoriteMonthlyWork = "02";

        /// <summary>お気に入り(試験車日程)</summary>
        public const string FavoriteTestCar = "03";

        /// <summary>お気に入り(カーシェア)</summary>
        public const string FavoriteCarShare = "04";

        /// <summary>お気に入り(外製車日程)</summary>
        public const string FavoriteOuterCar = "05";

        /// <summary>お気に入り(進捗履歴)</summary>
        public const string FavoriteWorkProgressHistory = "06";

        /// <summary>お気に入り(作業履歴(試験車))</summary>
        public const string FavoriteTestCarWorkHistory = "07";

        /// <summary>お気に入り(作業履歴(外製車))</summary>
        public const string FavoriteOuterCarWorkHistory = "08";

        /// <summary>お気に入り(作業履歴(カーシェア車))</summary>
        public const string FavoriteCarShareWorkHistory = "09";
        #endregion

        #region 履歴区分
        /// <summary>進捗履歴</summary>
        public const string HistoryWorkProgress = "1";

        /// <summary>作業履歴(試験車)</summary>
        public const string HistoryTestCar = "2";

        /// <summary>作業履歴(外製車)</summary>
        public const string HistoryOuterCar = "3";

        /// <summary>作業履歴(カーシェア車)</summary>
        public const string HistoryCarShare = "4";
        #endregion

        #region 拡張子
        /// <summary>TXT</summary>
        public const string Txt = ".txt";

        /// <summary>CSV</summary>
        public const string Csv = ".csv";

        /// <summary>ZIP</summary>
        public const string Zip = ".zip";

        /// <summary>XLS</summary>
        public const string Xls = ".xls";

        /// <summary>XLSX</summary>
        public const string Xlsx = ".xlsx";
        #endregion

        #region 管理部署種別
        /// <summary>一般</summary>
        public const string Ippan = "0";

        /// <summary>研実</summary>
        public const string Kenjitu = "1";

        /// <summary>管理</summary>
        public const string Kanri = "2";
        #endregion

    }
}
