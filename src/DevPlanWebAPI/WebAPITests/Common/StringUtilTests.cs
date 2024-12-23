using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevPlanWebAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlanWebAPI.Common.Tests
{
    [TestClass()]
    public class StringUtilTests
    {

        #region LenB()のテスト
        [TestMethod()]
        public void LenBTest()
        {
            // メソッド実行
            var result = StringUtil.LenB("");

            // 結果検証
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void LenBTest2()
        {
            // メソッド実行
            var result = StringUtil.LenB("あいうえお");

            // 結果検証
            Assert.AreEqual(10, result);
        }

        [TestMethod()]
        public void LenBTest3()
        {
            // メソッド実行
            var result = StringUtil.LenB("abcde");

            // 結果検証
            Assert.AreEqual(5, result);
        }

        [TestMethod()]
        public void LenBTest4()
        {
            // メソッド実行
            var result = StringUtil.LenB("～");

            // 結果検証
            Assert.AreEqual(2, result);
        }

        [TestMethod()]
        public void LenBTest5()
        {
            // メソッド実行
            var result = StringUtil.LenB("①");

            // 結果検証
            Assert.AreEqual(2, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LenBTest6()
        {
            // メソッド実行
            StringUtil.LenB(null);
        }
        #endregion

        #region LeftB()のテスト
        [TestMethod()]
        public void LeftBTest()
        {
            // メソッド実行
            var result = StringUtil.LeftB("あいうえお", 4);

            // 結果検証
            Assert.AreEqual("あい", result);
        }

        [TestMethod()]
        public void LeftBTest2()
        {
            // メソッド実行
            var result = StringUtil.LeftB("12345", 3);

            // 結果検証
            Assert.AreEqual("123", result);
        }

        [TestMethod()]
        public void LeftBTest3()
        {
            // メソッド実行
            var result = StringUtil.LeftB("1あ2い3う4え5お", 7);

            // 結果検証
            Assert.AreEqual("1あ2い3", result);
        }

        [TestMethod()]
        public void LeftBTest4()
        {
            // メソッド実行
            var result = StringUtil.LeftB("123", 10);

            // 結果検証
            Assert.AreEqual("123", result);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LeftBTest5()
        {
            // メソッド実行
            StringUtil.LeftB(null, 10);
        }
        #endregion

        #region MidB()のテスト
        [TestMethod()]
        public void MidBTest()
        {
            // メソッド実行
            var result = StringUtil.MidB("あいうえお", 5);

            // 結果検証
            Assert.AreEqual("うえお", result);
        }

        [TestMethod()]
        public void MidBTest1()
        {
            // メソッド実行
            var result = StringUtil.MidB("12345", 3);

            // 結果検証
            Assert.AreEqual("345", result);
        }

        [TestMethod()]
        public void MidBTest2()
        {
            // メソッド実行
            var result = StringUtil.MidB("12345", 100);

            // 結果検証
            Assert.AreEqual("", result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MidBTest3()
        {
            // メソッド実行
            StringUtil.MidB(null, 100);
        }
        #endregion

        #region MidB()（オーバーライドメソッド）のテスト
        [TestMethod()]
        public void MidBTestOL()
        {
            // メソッド実行
            var result = StringUtil.MidB("あいうえお", 5, 2);

            // 結果検証
            Assert.AreEqual("う", result);
        }

        [TestMethod()]
        public void MidBTestOL1()
        {
            // メソッド実行
            var result = StringUtil.MidB("12345", 3, 4);

            // 結果検証
            Assert.AreEqual("345", result);
        }

        [TestMethod()]
        public void MidBTestOL2()
        {
            // メソッド実行
            var result = StringUtil.MidB("12345", 100, 100);

            // 結果検証
            Assert.AreEqual("", result);
        }
        [TestMethod()]
        public void MidBTestOL3()
        {
            // メソッド実行
            var result = StringUtil.MidB("1234567890", 3, 100);

            // 結果検証
            Assert.AreEqual("34567890", result);
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MidBTestOL4()
        {
            // メソッド実行
            StringUtil.MidB(null, 100, 100);
        }
        #endregion

        #region RightB()のテスト
        [TestMethod()]
        public void RightBTest()
        {
            // メソッド実行
            var result = StringUtil.RightB("あいうえお", 4);

            // 結果検証
            Assert.AreEqual("えお", result);

        }

        [TestMethod()]
        public void RightBTest1()
        {
            // メソッド実行
            var result = StringUtil.RightB("12345", 3);

            // 結果検証
            Assert.AreEqual("345", result);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RightBTest2()
        {
            // メソッド実行
            StringUtil.RightB("あいうえお", 100);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RightBTest3()
        {
            // メソッド実行
            StringUtil.RightB(null, 100);
        }
        #endregion

        #region IsHiragana()のテスト
        [TestMethod()]
        public void IsHiraganaTest()
        {
            // メソッド実行
            var result = StringUtil.IsHiragana("あいうえお");

            // 結果検証
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsHiraganaTest1()
        {
            // メソッド実行
            var result = StringUtil.IsHiragana("アイウエオ");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsHiraganaTest2()
        {
            // メソッド実行
            var result = StringUtil.IsHiragana("12345");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsHiraganaTest3()
        {
            // メソッド実行
            var result = StringUtil.IsHiragana("あ1い2う3え4お");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsHiraganaTest4()
        {
            // メソッド実行
            var result = StringUtil.IsHiragana("　");

            // 結果検証
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsHiraganaTest5()
        {
            // メソッド実行
            var result = StringUtil.IsHiragana(" ");

            // 結果検証
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsHiraganaTest6()
        {
            // メソッド実行
            var result = StringUtil.IsHiragana("!#$%&'()");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void IsHiraganaTest7()
        {
            // メソッド実行
            StringUtil.IsHiragana(null);
        }
        #endregion

        #region IsKatakana()のテスト
        [TestMethod()]
        public void IsKatakanaTest()
        {
            // メソッド実行
            var result = StringUtil.IsKatakana("アイウエオ");

            // 結果検証
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsKatakanaTest1()
        {
            // メソッド実行
            var result = StringUtil.IsKatakana("あいうえお");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsKatakanaTest2()
        {
            // メソッド実行
            var result = StringUtil.IsKatakana("ｱｲｳｴｵ");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsKatakanaTest3()
        {
            // メソッド実行
            var result = StringUtil.IsKatakana("あ1い2う3え4お");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsKatakanaTest4()
        {
            // メソッド実行
            var result = StringUtil.IsKatakana("　");

            // 結果検証
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsKatakanaTest5()
        {
            // メソッド実行
            var result = StringUtil.IsKatakana(" ");

            // 結果検証
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsKatakanaTest6()
        {
            // メソッド実行
            var result = StringUtil.IsKatakana("!#$%&'()");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void IsKatakanaTest7()
        {
            // メソッド実行
            StringUtil.IsKatakana(null);
        }
        #endregion

        #region IsHanKatakana()のテスト
        [TestMethod()]
        public void IsHanKatakanaTest()
        {
            // メソッド実行
            var result = StringUtil.IsHanKatakana("ｱｲｳｴｵ");

            // 結果検証
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsHanKatakanaTest1()
        {
            // メソッド実行
            var result = StringUtil.IsHanKatakana("あいうえお");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsHanKatakanaTest2()
        {
            // メソッド実行
            var result = StringUtil.IsHanKatakana("アイウエオ");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsHanKatakanaTest3()
        {
            // メソッド実行
            var result = StringUtil.IsHanKatakana("あ1い2う3え4お");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsHanKatakanaTest4()
        {
            // メソッド実行
            var result = StringUtil.IsHanKatakana("　");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsHanKatakanaTest5()
        {
            // メソッド実行
            var result = StringUtil.IsHanKatakana(" ");

            // 結果検証
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsHanKatakanaTest6()
        {
            // メソッド実行
            var result = StringUtil.IsHanKatakana("!#$%&'()");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsHanKatakanaTest7()
        {
            // メソッド実行
            StringUtil.IsHanKatakana(null);
        }
        #endregion

        #region IsHankaku()のテスト
        [TestMethod()]
        public void IsHankakuTest()
        {
            // メソッド実行
            var result = StringUtil.IsHankaku("1234567890-^\\!\"#$%&'()=~|qwertyuiop@[QWERTYUIOP`{asdfghjkl;:]ASDFGHJKL+*}zxcvbnm,./\\ZXCVBNM<>?_");

            // 結果検証
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsHankakuTest1()
        {
            // メソッド実行
            var result = StringUtil.IsHankaku("あいう");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsHankakuTest2()
        {
            // メソッド実行
            var result = StringUtil.IsHankaku("あいう");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsHankakuTest3()
        {
            // メソッド実行
            var result = StringUtil.IsHankaku("ＡＢＣ");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsHankakuTest4()
        {
            // メソッド実行
            StringUtil.IsHankaku(null);
        }
        #endregion

        #region IsNumber()のテスト
        [TestMethod()]
        public void IsNumberTest()
        {
            // メソッド実行
            var result = StringUtil.IsNumber("1234567890");

            // 結果検証
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsNumberTest1()
        {
            // メソッド実行
            var result = StringUtil.IsNumber("１２３４５６７８９０");

            // 結果検証
            Assert.IsFalse(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNumberTest2()
        {
            // メソッド実行
            StringUtil.IsNumber(null);
        }
        #endregion

        #region ConvertHanKatakanaToZenKatakana()のテスト
        [TestMethod()]
        public void ConvertHanKatakanaToZenKatakanaTest()
        {
            // メソッド実行
            var result = StringUtil.ConvertHanKatakanaToZenKatakana("あいうアイウｴｵｶ12345!\"#$%");

            // 結果検証
            Assert.AreEqual("あいうアイウエオカ12345!\"#$%", result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConvertHanKatakanaToZenKatakanaTest1()
        {
            // メソッド実行
            StringUtil.ConvertHanKatakanaToZenKatakana(null);
        } 
        #endregion
    }
}