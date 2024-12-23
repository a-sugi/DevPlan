using System;

namespace DevPlan.UICommon.Utils
{
    public class DateTimeUtil
    {
        #region 日付型を文字列に変換
        /// <summary>
        /// 日時型を年月文字列(yyyy/MM)に変換
        /// </summary>
        /// <param name="date">日時</param>
        /// <returns>yyyy/MM/dd</returns>

        public static string ConvertMonthString(DateTime? date)
        {
            return ConvertFormatString(date, Const.FormatMonth);

        }

        /// <summary>
        /// 日時型を日付文字列(yyyy/MM/dd)に変換
        /// </summary>
        /// <param name="date">日時</param>
        /// <returns>yyyy/MM/dd</returns>

        public static string ConvertDateString(DateTime? date)
        {
            return ConvertFormatString(date, Const.FormatDate);

        }

        /// <summary>
        /// 日時型を日付文字列(yyyy/MM/dd HH:mm:ss)に変換
        /// </summary>
        /// <param name="date">日時</param>
        /// <returns>yyyy/MM/dd HH:mm:ss</returns>

        public static string ConvertDateTimeString(DateTime? date)
        {
            return ConvertFormatString(date, Const.FormatDateTime);

        }

        /// <summary>
        /// 日時型を文字列に変換
        /// </summary>
        /// <param name="date">日時</param>
        /// <param name="format">書式</param>
        /// <returns></returns>
        public static string ConvertFormatString(DateTime? date, string format)
        {
            return date == null ? "" : date.Value.ToString(format);

        }
        #endregion

        #region 直近の指定した日付の指定した曜日の日付を取得
        /// <summary>
        /// 直近の指定した日付の月曜日の日付を取得
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMostMondayFirst()
        {
            return GetMostMondayFirst(DateTime.Today);

        }

        /// <summary>
        /// 直近の指定した日付の月曜日の日付を取得
        /// </summary>
        /// <param name="day">基準日</param>
        /// <returns></returns>
        public static DateTime GetMostMondayFirst(DateTime day)
        {
            return GetMostWeekFirst(day, DayOfWeek.Monday);

        }

        /// <summary>
        /// 直近の指定した日付の指定した曜日の日付を取得
        /// </summary>
        /// <param name="day">基準日</param>
        /// <param name="week">曜日</param>
        /// <returns></returns>
        public static DateTime GetMostWeekFirst(DateTime day, DayOfWeek week)
        {
            var setDay = day.Date;

            //直近の過去の指定された曜日の日付を取得
            while (setDay.DayOfWeek != week)
            {
                setDay = setDay.AddDays(-1);

            }

            return setDay;

        }
        #endregion

        #region 日本時間を取得
        /// <summary>
        /// 日本時間を取得
        /// </summary>
        /// <returns></returns>
        public static DateTime GetJstNow()
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);

        }
        #endregion

        #region 日付を取得
        /// <summary>
        /// 月初の日付を取得
        /// </summary>
        /// <returns></returns>
        public static DateTime? GetFirstDay()
        {
            return GetFirstDay(DateTime.Now);

        }

        /// <summary>
        /// 月初の日付を取得
        /// </summary>
        /// <param name="date">日付</param>
        /// <returns></returns>
        public static DateTime? GetFirstDay(DateTime? date)
        {
            if (date == null)
            {
                return null;

            }

            var day = date.Value;

            return new DateTime(day.Year, day.Month, 1);

        }

        /// <summary>
        /// 月末の日付を取得
        /// </summary>
        /// <returns></returns>
        public static DateTime? GetLastDay()
        {
            return GetLastDay(DateTime.Now);

        }

        /// <summary>
        /// 月末の日付を取得
        /// </summary>
        /// <param name="date">日付</param>
        /// <returns></returns>
        public static DateTime? GetLastDay(DateTime? date)
        {
            if (date == null)
            {
                return null;

            }

            var day = date.Value;

            return new DateTime(day.Year, day.Month, 1).AddMonths(1).AddDays(-1);

        }
        #endregion

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

        /// <summary>
        /// n営業日後の取得（土日を祝日扱いとする）
        /// </summary>
        /// <param name="date">演算日</param>
        /// <returns>営業日</returns>
        public static DateTime GetBusinessDay(DateTime date, int addDays)
        {
            int count = 0;
            DateTime tmp = date;
            while(true)
            {
                tmp = tmp.AddDays(1);

                if (CheckBusinessDay(tmp) == true)
                {
                    count++;
                }

                if (count >= addDays)
                {
                    //n営業日到達で終了
                    break;
                }
            }

            return tmp;
        }

        /// <summary>
        /// 平日／休日判定
        /// </summary>
        /// <param name="date">検索日</param>
        /// <returns></returns>
        public static bool CheckBusinessDay(DateTime date)
        {
            bool bussinesDay = true;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    bussinesDay = true;
                    break;

                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    bussinesDay = false;
                    break;
            }

            return bussinesDay;
        }
    }
}
