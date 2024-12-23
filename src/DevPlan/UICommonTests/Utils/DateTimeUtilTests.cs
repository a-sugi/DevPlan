using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevPlan.UICommon.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Utils.Tests
{
    [TestClass()]
    public class DateTimeUtilTests
    {

        #region ConvertMonthString()のテスト
        // パターン網羅はConvertFormatString()のテストで確認
        [TestMethod()]
        public void ConvertMonthStringTest()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertMonthString(new DateTime(2017, 1, 3, 5, 35, 2, 300));

            // 結果検証
            Assert.AreEqual("2017/01", result);
        }
        #endregion

        #region ConvertMonthString()のテスト
        // パターン網羅はConvertFormatString()のテストで確認
        [TestMethod()]
        public void ConvertDateStringTest()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateString(new DateTime(2017, 1, 3, 5, 35, 2, 300));

            // 結果検証
            Assert.AreEqual("2017/01/03", result);
        }
        #endregion

        #region ConvertMonthString()のテスト
        // パターン網羅はConvertFormatString()のテストで確認
        [TestMethod()]
        public void ConvertDateTimeStringTest()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateTimeString(new DateTime(2017, 1, 3, 5, 35, 2, 300));

            // 結果検証
            Assert.AreEqual("2017/01/03 05:35:02", result);
        }
        #endregion

        #region ConvertFormatString()のテスト
        [TestMethod()]
        public void ConvertFormatStringTest()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertFormatString(new DateTime(2017, 1, 3, 5, 35, 2, 300), "yyyy/MM/dd H");

            // 結果検証
            Assert.AreEqual("2017/01/03 5", result);
        }
        #endregion

        #region ConvertMonthString()のテスト
        // パターン網羅はConvertFormatString()のテストで確認
        [TestMethod()]
        public void ConvertFormatStringTest1()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertFormatString(null, "yyyy/MM/dd H");

            // 結果検証
            Assert.AreEqual("", result);
        }

        [TestMethod()]
        public void ConvertFormatStringTest2()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertFormatString(new DateTime(2017, 1, 3, 5, 35, 2, 300), "");

            // 結果検証
            Assert.AreEqual("17/01/03 5:35:02", result);
        }

        [TestMethod()]
        public void ConvertFormatStringTest3()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertFormatString(new DateTime(2017, 1, 3, 5, 35, 2, 300), null);

            // 結果検証
            Assert.AreEqual("17/01/03 5:35:02", result);
        }
        #endregion

        #region GetMostMondayFirst()のテスト
        // パターン網羅はConvertFormatString()のテストで確認
        [TestMethod()]
        public void GetMostMondayFirstTest()
        {
            // メソッド実行
            var result = DateTimeUtil.GetMostMondayFirst();

            // 結果検証
            Assert.AreEqual(DateTime.Now.Date.AddDays(-(DateTime.Now.DayOfWeek - DayOfWeek.Monday)), result);
        }

        [TestMethod()]
        public void GetMostMondayFirstTest1()
        {
            // メソッド実行
            var result = DateTimeUtil.GetMostMondayFirst(new DateTime(2017, 1, 20));

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 16), result);
        }
        #endregion

        #region GetMostWeekFirst()のテスト
        [TestMethod()]
        public void GetMostWeekFirstTest()
        {
            // メソッド実行
            var result = DateTimeUtil.GetMostWeekFirst(new DateTime(2017, 1, 20), DayOfWeek.Saturday);

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 14), result);
        }
        [TestMethod()]
        public void GetMostWeekFirstTest1()
        {
            // メソッド実行
            var result = DateTimeUtil.GetMostWeekFirst(new DateTime(2017, 1, 20), DayOfWeek.Sunday);

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 15), result);
        }

        [TestMethod()]
        public void GetMostWeekFirstTest2()
        {
            // メソッド実行
            var result = DateTimeUtil.GetMostWeekFirst(new DateTime(2017, 1, 20), DayOfWeek.Monday);

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 16), result);
        }

        [TestMethod()]
        public void GetMostWeekFirstTest3()
        {
            // メソッド実行
            var result = DateTimeUtil.GetMostWeekFirst(new DateTime(2017, 1, 20), DayOfWeek.Tuesday);

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 17), result);
        }

        [TestMethod()]
        public void GetMostWeekFirstTest4()
        {
            // メソッド実行
            var result = DateTimeUtil.GetMostWeekFirst(new DateTime(2017, 1, 20), DayOfWeek.Wednesday);

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 18), result);
        }

        [TestMethod()]
        public void GetMostWeekFirstTest5()
        {
            // メソッド実行
            var result = DateTimeUtil.GetMostWeekFirst(new DateTime(2017, 1, 20), DayOfWeek.Thursday);

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 19), result);
        }

        [TestMethod()]
        public void GetMostWeekFirstTest6()
        {
            // メソッド実行
            var result = DateTimeUtil.GetMostWeekFirst(new DateTime(2017, 1, 20), DayOfWeek.Friday);

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 20), result);
        }
        #endregion

        #region GetJstNow()のテスト
        [TestMethod()]
        public void GetJstNowTest()
        {
            // メソッド実行
            var result = DateTimeUtil.GetJstNow();

            // 結果検証
            Assert.AreEqual(DateTime.Now, result);
        }
        #endregion

        #region ConvertDateStringToDateTime()のテスト
        // yyyyMMdd yyMMdd yyyy/MM/dd yyyy/M/d yy/M/d yy/MM/dd
        [TestMethod()]
        public void ConvertDateStringToDateTimeTest()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime("20170105");

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 5), result);
        }

        [TestMethod()]
        public void ConvertDateStringToDateTimeTest1()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime("170105");

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 5), result);
        }

        [TestMethod()]
        public void ConvertDateStringToDateTimeTest2()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime("2017/01/05");

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 5), result);
        }

        [TestMethod()]
        public void ConvertDateStringToDateTimeTest3()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime("2017/1/5");

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 5), result);
        }

        [TestMethod()]
        public void ConvertDateStringToDateTimeTest4()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime("17/01/05");

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 5), result);
        }

        [TestMethod()]
        public void ConvertDateStringToDateTimeTest5()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime("17/1/5");

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 5), result);
        }

        [TestMethod()]
        public void ConvertDateStringToDateTimeTest6()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime("2017.1.5");

            // 結果検証
            Assert.AreEqual(new DateTime(2017, 1, 5), result);
        }

        [TestMethod()]
        public void ConvertDateStringToDateTimeTest7()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime(null);

            // 結果検証
            Assert.AreEqual(null, result);
        }

        [TestMethod()]
        public void ConvertDateStringToDateTimeTest8()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime("aaaaaa");

            // 結果検証
            Assert.AreEqual(null, result);
        }

        [TestMethod()]
        public void ConvertDateStringToDateTimeTest9()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime("bbbbbbbbbb");

            // 結果検証
            Assert.AreEqual(null, result);
        }

        [TestMethod()]
        public void ConvertDateStringToDateTimeTest10()
        {
            // メソッド実行
            var result = DateTimeUtil.ConvertDateStringToDateTime("9999999999");

            // 結果検証
            Assert.AreEqual(null, result);
        }
        #endregion

        #region FormatDate()のテスト
        // yyyyMMdd yyMMdd yyyy/MM/dd yyyy/M/d yy/M/d yy/MM/dd
        [TestMethod()]
        public void FormatDateTest()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDate("20170105", "yyMMdd");

            // 結果検証
            Assert.AreEqual("170105", result);
        }

        [TestMethod()]
        public void FormatDateTest1()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDate("170105", "yyyy/MM/dd");

            // 結果検証
            Assert.AreEqual("2017/01/05", result);
        }

        [TestMethod()]
        public void FormatDateTest2()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDate("2017/01/05", "yyyy/M/d");

            // 結果検証
            Assert.AreEqual("2017/1/5", result);
        }

        [TestMethod()]
        public void FormatDateTest3()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDate("2017/1/5", "yy/M/d");

            // 結果検証
            Assert.AreEqual("17/1/5", result);
        }

        [TestMethod()]
        public void FormatDateTest4()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDate("17/1/5", "yy/MM/dd");

            // 結果検証
            Assert.AreEqual("17/01/05", result);
        }

        [TestMethod()]
        public void FormatDateTest5()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDate("17/01/05", "yyyyMMdd");

            // 結果検証
            Assert.AreEqual("20170105", result);
        }

        [TestMethod()]
        public void FormatDateTest6()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDate("17/01/05", "aaaaaa");

            // 結果検証
            Assert.AreEqual("aaaaaa", result);
        }

        [TestMethod()]
        public void FormatDateTest7()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDate("aaaaaa", "yyyyMMdd");

            // 結果検証
            Assert.AreEqual("", result);
        }
        #endregion

        #region FormatDateWithSlash()のテスト
        // yyyyMMdd yyMMdd yyyy/MM/dd yyyy/M/d yy/M/d yy/MM/dd
        [TestMethod()]
        public void FormatDateWithSlashTest()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithSlash("20170105");

            // 結果検証
            Assert.AreEqual("2017/01/05", result);
        }

        [TestMethod()]
        public void FormatDateWithSlashTest1()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithSlash("170105");

            // 結果検証
            Assert.AreEqual("2017/01/05", result);
        }

        [TestMethod()]
        public void FormatDateWithSlashTest2()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithSlash("2017/01/05");

            // 結果検証
            Assert.AreEqual("2017/01/05", result);
        }

        [TestMethod()]
        public void FormatDateWithSlashTest3()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithSlash("2017/1/5");

            // 結果検証
            Assert.AreEqual("2017/01/05", result);
        }

        [TestMethod()]
        public void FormatDateWithSlashTest4()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithSlash("17/1/5");

            // 結果検証
            Assert.AreEqual("2017/01/05", result);
        }

        [TestMethod()]
        public void FormatDateWithSlashTest5()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithSlash("17/01/05");

            // 結果検証
            Assert.AreEqual("2017/01/05", result);
        }

        [TestMethod()]
        public void FormatDateWithSlashTest6()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithSlash("aaaaaa");

            // 結果検証
            Assert.AreEqual("", result);
        }

        [TestMethod()]
        public void FormatDateWithSlashTest7()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithSlash("");

            // 結果検証
            Assert.AreEqual(null, result);
        }

        [TestMethod()]
        public void FormatDateWithSlashTest8()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithSlash(null);

            // 結果検証
            Assert.AreEqual(null, result);
        }
        #endregion

        #region FormatDateWithoutSlash()のテスト
        // yyyyMMdd yyMMdd yyyy/MM/dd yyyy/M/d yy/M/d yy/MM/dd
        [TestMethod()]
        public void FormatDateWithoutSlashTest()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithoutSlash("20170105");

            // 結果検証
            Assert.AreEqual("20170105", result);
        }

        [TestMethod()]
        public void FormatDateWithoutSlashTest2()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithoutSlash("170105");

            // 結果検証
            Assert.AreEqual("20170105", result);
        }

        [TestMethod()]
        public void FormatDateWithoutSlashTest3()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithoutSlash("2017/01/05");

            // 結果検証
            Assert.AreEqual("20170105", result);
        }

        [TestMethod()]
        public void FormatDateWithoutSlashTest4()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithoutSlash("2017/1/5");

            // 結果検証
            Assert.AreEqual("20170105", result);
        }

        [TestMethod()]
        public void FormatDateWithoutSlashTest5()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithoutSlash("17/1/5");

            // 結果検証
            Assert.AreEqual("20170105", result);
        }

        [TestMethod()]
        public void FormatDateWithoutSlashTest6()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithoutSlash("17/01/05");

            // 結果検証
            Assert.AreEqual("20170105", result);
        }

        [TestMethod()]
        public void FormatDateWithoutSlashTest7()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithoutSlash("123456");

            // 結果検証
            Assert.AreEqual("", result);
        }

        [TestMethod()]
        public void FormatDateWithoutSlashTest8()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithoutSlash("");

            // 結果検証
            Assert.AreEqual(null, result);
        }

        [TestMethod()]
        public void FormatDateWithoutSlashTest9()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateWithoutSlash(null);

            // 結果検証
            Assert.AreEqual(null, result);
        }
        #endregion

        #region FormatDateMonth()のテスト
        // yyyyMM yyMM yyyy/MM yyyy/M yy/M yy/MM
        [TestMethod()]
        public void FormatDateMonthTest()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("201701", "yyMM");

            // 結果検証
            Assert.AreEqual("1701", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest1()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("1701", "yyyy/MM");

            // 結果検証
            Assert.AreEqual("2017/01", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest2()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("2017/01", "yyyy/M");

            // 結果検証
            Assert.AreEqual("2017/1", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest3()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("2017/1", "yy/M");

            // 結果検証
            Assert.AreEqual("17/1", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest4()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("17/1", "yy/MM");

            // 結果検証
            Assert.AreEqual("17/01", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest5()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("17/01", "yyyyMM");

            // 結果検証
            Assert.AreEqual("201701", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest6()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("aaaaaa", "yyyy/MM");

            // 結果検証
            Assert.AreEqual("", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest7()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("", "yyyy/MM");

            // 結果検証
            Assert.AreEqual("", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest8()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth(null, "yyyy/MM");

            // 結果検証
            Assert.AreEqual("", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest9()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("17/01", "aaaaaa");

            // 結果検証
            Assert.AreEqual("aaaaaa", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest10()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("17/01", "");

            // 結果検証
            Assert.AreEqual("17/01/01 0:00:00", result);
        }

        [TestMethod()]
        public void FormatDateMonthTest11()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonth("17/01", null);

            // 結果検証
            Assert.AreEqual("17/01/01 0:00:00", result);
        }
        #endregion

        #region FormatDateMonthWithSlash()のテスト
        // yyyyMM yyMM yyyy/MM yyyy/M yy/M yy/MM
        // FormatDateMonthTestでパターン網羅済なので1パターンのみ実施
        [TestMethod()]
        public void FormatDateMonthWithSlashTest()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonthWithSlash("201701");

            // 結果検証
            Assert.AreEqual("2017/01", result);
        }
        #endregion

        #region FormatDateMonthWithoutSlash()のテスト
        // yyyyMM yyMM yyyy/MM yyyy/M yy/M yy/MM
        // FormatDateMonthTestでパターン網羅済なので1パターンのみ実施
        [TestMethod()]
        public void FormatDateMonthWithoutSlashTest()
        {
            // メソッド実行
            var result = DateTimeUtil.FormatDateMonthWithoutSlash("2017/01");

            // 結果検証
            Assert.AreEqual("201701", result);
        } 
        #endregion
    }
}