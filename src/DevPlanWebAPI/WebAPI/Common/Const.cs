using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Common
{
    /// <summary>定数クラス</summary>
    public class Const
    {
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
        public static readonly Encoding SJis = Encoding.GetEncoding("shift_jis");

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

        #region 桁数
        /// <summary>ログインIDの文字数</summary>
        public const int LoginIdLength = 20;

        /// <summary>パーソナルIDの文字数</summary>
        public const int PersonelIdLength = 10;

        /// <summary>課グループIDの文字数</summary>
        public const int SectionGroupIdLength = 10;

        /// <summary>スケジュールID最小値</summary>
        public const long ScheduleIdMin = 0L;

        /// <summary>スケジュールID最大値</summary>
        public const long ScheduleIdMax = 9999999999L;

        /// <summary>開発符号の最大文字数</summary>
        public const int GeneralCodeLength = 15;

        /// <summary>スケジュールソート順最小値</summary>
        public const double ScheduleSortMin = 0D;

        /// <summary>スケジュールソート順最大値</summary>
        public const double ScheduleSortMax = 999999999.9D;

        /// <summary>スケジュール行最小値</summary>
        public const int ScheduleRowMin = 0;

        /// <summary>スケジュール行最大値</summary>
        public const int ScheduleRowMax = 99999;

        /// <summary>スケジュールのカテゴリーの最大文字数</summary>
        public const int ScheduleCategoryLength = 1000;

        /// <summary>スケジュールのコメントの最大文字数</summary>
        public const int ScheduleCommentLength = 500;

        /// <summary>スケジュールシンボル最小値</summary>
        public const short ScheduleSymbolMin = 0;

        /// <summary>スケジュールシンボル最大値</summary>
        public const short ScheduleSymbolMax = 99;

        /// <summary>スケジュールの予約種別の文字数</summary>
        public const int ScheduleReservationTypeLength = 10;

        /// <summary>スケジュール履歴ID最小値</summary>
        public const double ScheduleHistoryIdMin = 0D;

        /// <summary>スケジュール履歴ID最大値</summary>
        public const double ScheduleHistoryIdMax = 99999999999999999999D;
        #endregion

        #region 課題フォローリスト
        /// <summary>Open</summary>
        public const string Open = "open";

        /// <summary>Close</summary>
        public const string Close = "close";
        #endregion

        #region クラスデータ区分
        /// <summary>デフォルト</summary>
        public const string ClassDefault = "00";

        /// <summary>業務計画表</summary>
        public const string OperationPlan = "01";

        /// <summary>月次計画表</summary>
        public const string MonthlyPlan = "02";

        /// <summary>試験車日程</summary>
        public const string TestCar = "03";

        /// <summary>カーシェア日程</summary>
        public const string CarShare = "04";

        /// <summary>外製車日程</summary>
        public const string OuterCar = "05";

        /// <summary>進捗履歴</summary>
        public const string PlanHistory = "06";

        /// <summary>作業履歴(試験車)</summary>
        public const string TestCarHistory = "07";

        /// <summary>作業履歴(外製車)</summary>
        public const string OuterCarHistory = "08";
        
        /// <summary>作業履歴(カーシェア車)</summary>
        public const string CarShareHistory = "09";
        #endregion

        #region 総括部署
        /// <summary>総括部署(課ID)</summary>
        public static readonly string[] SoukatsuSectionIds = { "19" };
        
        /// <summary>総括部署除外(担当ID)</summary>
        public static readonly string[] SoukatsuNotSectionGroupIds = { "141" };
        #endregion

        #region カーシェアスタッフ
        /// <summary>カーシェアスタッフ(担当ID)</summary>
        public static readonly string[] CarShareStaffSectionGroupIds = { "1496", "1497" };
        #endregion

        #region 業務コード
        /// <summary>管理部署種別</summary>
        public class ManagementGroupCode
        {
            /// <summary>一般</summary>
            public const string Ippan = "0";

            /// <summary>研実</summary>
            public const string Kenjitu = "1";

            /// <summary>管理</summary>
            public const string Kanri = "2";
        }

        /// <summary>アクセスレベルコード</summary>
        public class AccessLevelCode
        {
            /// <summary>職制</summary>
            public const string Syokusei = "0";

            /// <summary>担当</summary>
            public const string Tanto = "1";

            /// <summary>一般</summary>
            public const string Ippan = "2";

            /// <summary>SKC(FTS)</summary>
            public const string Skc = "3";

            /// <summary>パート</summary>
            public const string Part = "4";

            /// <summary>派遣・外部委託</summary>
            public const string Haken = "5";

            /// <summary>シニア</summary>
            public const string Senior = "6";
        }
        #endregion
    }
}