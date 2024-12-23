using System;

namespace DevPlanWebAPI.Common
{
    /// <summary>
    /// 日付型変換クラス
    /// </summary>
    public class DateTimeUtil
    {
        /// <summary>
        /// DateTime?型の日付を文字列に変換
        /// </summary>
        /// <returns></returns>
        public static string ConvertToString(DateTime? date, string format)
        {
            if (date == null)
            {
                return string.Empty;
            }
            else
            {
                return ((DateTime)date).ToString(format);
            }
        }

        /// <summary>
        /// string型をDateTime?型に変換（日付）
        /// </summary>
        /// <param name="date">yyyyMMdd yyMMdd yyyy/MM/dd yyyy/M/d yy/M/d yy/MM/dd</param>
        /// <returns></returns>
        public static DateTime? ConvertDateStringToDateTime(string date)
        {
            if (string.IsNullOrEmpty(date)) return null;
            date = date.Replace(".", "/");
            string[] array = date.Split(Convert.ToChar("/"));
            if (array.Length == 1)
            {
                if (date.Length == 6) date = "20" + date;
                if (date.Length == 8) date = date.Insert(4, "/").Insert(7, "/");
                if (date.Length != 10)
                {
                    return null;
                }
            }
            else if (array.Length == 3)
            {
                if (array[0].Length == 2) array[0] = "20" + array[0];
                date = array[0] + "/" + array[1] + "/" + array[2];
            }
            else
            {
                return null;
            }

            DateTime ret;
            if (DateTime.TryParse(date, out ret))
            {
                return ret;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 文字の書式変更（日付）
        /// </summary>
        /// <param name="date">yyyyMMdd yyMMdd yyyy/MM/dd yyyy/M/d yy/M/d yy/MM/dd</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatDate(string date, string format)
        {
            if (string.IsNullOrEmpty(date)) return null;
            DateTime? dt = ConvertDateStringToDateTime(date);

            if (dt.HasValue)
            {
                return ((DateTime)dt).ToString(format);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 文字の書式変更（日付）
        /// </summary>
        /// <param name="date">yyyyMMdd yyMMdd yyyy/MM/dd yyyy/M/d yy/M/d yy/MM/dd</param>
        /// <returns>yyyy/MM/dd</returns>
        public static string FormatDateWithSlash(string date)
        {
            return FormatDate(date, "yyyy/MM/dd");
        }

        /// <summary>
        /// 文字の書式変更（日付）
        /// </summary>
        /// <param name="date">yyyyMMdd yyMMdd yyyy/MM/dd yyyy/M/d yy/M/d yy/MM/dd</param>
        /// <returns>yyyyMMdd</returns>
        public static string FormatDateWithoutSlash(string date)
        {
            return FormatDate(date, "yyyyMMdd");
        }

        /// <summary>
        /// 文字の書式変更（日付 年月）
        /// </summary>
        /// <param name="date">yyyyMM yyMM yyyy/MM yyyy/M yy/M yy/MM</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatDateMonth(string date, string format)
        {
            if (string.IsNullOrEmpty(date)) return string.Empty;
            string[] array = date.Split(Convert.ToChar("/"));
            if (array.Length == 1)
            {
                if (date.Length == 4) date = "20" + date;
                date += "01";
            }
            else if (array.Length == 2)
            {
                if (array[0].Length == 2) array[0] = "20" + array[0];
                date = array[0] + array[1].PadLeft(2, Convert.ToChar("0")) + "01";
            }
            else
            {
                return string.Empty;
            }
            return FormatDate(date, format);
        }

        /// <summary>
        /// 文字の書式変更（日付 年月）
        /// </summary>
        /// <param name="date">yyyyMM yyMM yyyy/MM yyyy/M yy/M yy/MM</param>
        /// <returns>yyyy/MM</returns>
        public static string FormatDateMonthWithSlash(string date)
        {
            return FormatDateMonth(date, "yyyy/MM");
        }

        /// <summary>
        /// 文字の書式変更（日付 年月）
        /// </summary>
        /// <param name="date">yyyyMM yyMM yyyy/MM yyyy/M yy/M yy/MM</param>
        /// <returns>yyyyMM</returns>
        public static string FormatDateMonthWithoutSlash(string date)
        {
            return FormatDateMonth(date, "yyyyMM");
        }

        #region 日付だけを取得
        /// <summary>
        /// 日付だけを取得
        /// </summary>
        /// <param name="dt">日時</param>
        /// <returns></returns>
        public static DateTime? GetDate(DateTime? dt)
        {
            return dt == null ? null : (DateTime?)dt.Value.Date;

        }
        #endregion

        #region 月末の日付を取得
        /// <summary>
        /// 月末の日付を取得
        /// </summary>
        /// <param name="dt">日時</param>
        public static DateTime? GetLastDate(DateTime? dt)
        {
            if (dt == null)
            {
                return null;

            }

            var date = dt.Value.Date;

            return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);

        }
        #endregion

    }
}
