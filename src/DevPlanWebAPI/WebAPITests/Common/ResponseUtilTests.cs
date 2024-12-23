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
    public class ResponseUtilTests
    {

        #region GetResponse() 取得データなしメソッド のテスト
        [TestMethod()]
        public void GetResponseTest()
        {
            // 初期設定
            var messageType = MessageType.KKE03001;
            var expected = new Models.ResponseModel<object>
            {
                //ステータス
                Status = "failure",

                //エラーコード
                ErrorCode = Enum.GetName(typeof(MessageType), messageType),

                //エラー内容
                ErrorMessage = "予期しないエラーが発生しました。システム管理者に連絡してください。",

                //取得データ
                Results = Enumerable.Empty<object>(),
            };

            // メソッド実行
            var result = ResponseUtil.GetResponse<object>(messageType);

            // 結果検証
            Assert.AreEqual(expected.Status, result.Status);
            Assert.AreEqual(expected.ErrorCode, result.ErrorCode);
            Assert.AreEqual(expected.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(expected.Results, result.Results);
        }


        [TestMethod()]
        public void GetResponseTest1()
        {
            // 初期設定
            var messageType = MessageType.KKE03015;
            var columnnames = new string[] { "カラム１", "カラム２" };
            var expected = new Models.ResponseModel<object>
            {
                //ステータス
                Status = "failure",

                //エラーコード
                ErrorCode = Enum.GetName(typeof(MessageType), messageType),

                //エラー内容
                ErrorMessage = "日付範囲に誤りがあります。" + columnnames[0] + "に"
                        + columnnames[1] + "を超える日付は入力できません。",

                //取得データ
                Results = Enumerable.Empty<object>(),
            };

            // メソッド実行
            var result = ResponseUtil.GetResponse<object>(messageType, columnnames);

            // 結果検証
            Assert.AreEqual(expected.Status, result.Status);
            Assert.AreEqual(expected.ErrorCode, result.ErrorCode);
            Assert.AreEqual(expected.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(expected.Results, result.Results);
        }

        [TestMethod()]
        public void GetResponseTest2()
        {
            // 初期設定
            var messageType = MessageType.None;
            var expected = new Models.ResponseModel<object>
            {
                //ステータス
                Status = "failure",

                //エラーコード
                ErrorCode = "",

                //エラー内容
                ErrorMessage = "",

                //取得データ
                Results = Enumerable.Empty<object>(),
            };

            // メソッド実行
            var result = ResponseUtil.GetResponse<object>(messageType);

            // 結果検証
            Assert.AreEqual(expected.Status, result.Status);
            Assert.AreEqual(expected.ErrorCode, result.ErrorCode);
            Assert.AreEqual(expected.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(expected.Results, result.Results);
        }
        #endregion

        #region GetResponse() 取得データありメソッド のテスト
        [TestMethod()]
        public void GetResponseTest3()
        {
            // 初期設定
            var expected = new Models.ResponseModel<object>
            {
                //ステータス
                Status = "failure",

                //エラーコード
                ErrorCode = "KKE03001",

                //エラー内容
                ErrorMessage = "予期しないエラーが発生しました。システム管理者に連絡してください。",

                //取得データ
                Results = Enumerable.Empty<object>(),
            };

            // メソッド実行
            var result = ResponseUtil.GetResponse<object>(null, MessageType.None);

            // 結果検証
            Assert.AreEqual(expected.Status, result.Status);
            Assert.AreEqual(expected.ErrorCode, result.ErrorCode);
            Assert.AreEqual(expected.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(expected.Results, result.Results);
        }

        [TestMethod()]
        public void GetResponseTest4()
        {
            // 初期設定
            var expected = new Models.ResponseModel<object>
            {
                //ステータス
                Status = "failure",

                //エラーコード
                ErrorCode = "KKE03002",

                //エラー内容
                ErrorMessage = "該当データが見つかりませんでした。",

                //取得データ
                Results = Enumerable.Empty<object>(),
            };

            // メソッド実行
            var result = ResponseUtil.GetResponse<object>(Enumerable.Empty<object>(), MessageType.None);

            // 結果検証
            Assert.AreEqual(expected.Status, result.Status);
            Assert.AreEqual(expected.ErrorCode, result.ErrorCode);
            Assert.AreEqual(expected.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(expected.Results, result.Results);
        }

        [TestMethod()]
        public void GetResponseTest5()
        {
            // 初期設定
            var messageType = MessageType.KKE03004;
            var expected = new Models.ResponseModel<object>
            {
                //ステータス
                Status = "failure",

                //エラーコード
                ErrorCode = "KKE03004",

                //エラー内容
                ErrorMessage = "パスワードに誤りがあります。",

                //取得データ
                Results = Enumerable.Empty<object>(),
            };

            // メソッド実行
            var result = ResponseUtil.GetResponse<object>(Enumerable.Empty<object>(), messageType);

            // 結果検証
            Assert.AreEqual(expected.Status, result.Status);
            Assert.AreEqual(expected.ErrorCode, result.ErrorCode);
            Assert.AreEqual(expected.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(expected.Results, result.Results);
        }

        [TestMethod()]
        public void GetResponseTest6()
        {
            // 初期設定
            var messageType = MessageType.None;
            var list = new List<string>();
            list.Add("取得データ１");
            list.Add("取得データ２");
            var expected = new Models.ResponseModel<object>
            {
                //ステータス
                Status = "success",

                //エラーコード
                ErrorCode = "",

                //エラー内容
                ErrorMessage = "",

                //取得データ
                Results = list,
            };

            // メソッド実行
            var result = ResponseUtil.GetResponse<object>(list, messageType);

            // 結果検証
            Assert.AreEqual(expected.Status, result.Status);
            Assert.AreEqual(expected.ErrorCode, result.ErrorCode);
            Assert.AreEqual(expected.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(expected.Results, result.Results);
        }
        #endregion

        #region GetResponse() データ登録メソッド のテスト
        [TestMethod()]
        public void GetResponseTest7()
        {
            // 初期設定
            var messageType = MessageType.KKE03001;
            var expected = new Models.ResponseModel<object>
            {
                //ステータス
                Status = "success",

                //エラーコード
                ErrorCode = Enum.GetName(typeof(MessageType), messageType),

                //エラー内容
                ErrorMessage = "予期しないエラーが発生しました。システム管理者に連絡してください。",

                //取得データ
                Results = Enumerable.Empty<object>(),
            };

            // メソッド実行
            var result = ResponseUtil.GetResponse<object>(true, messageType);

            // 結果検証
            Assert.AreEqual(expected.Status, result.Status);
            Assert.AreEqual(expected.ErrorCode, result.ErrorCode);
            Assert.AreEqual(expected.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(expected.Results, result.Results);
        }

        [TestMethod()]
        public void GetResponseTest8()
        {
            // 初期設定
            var messageType = MessageType.None;
            var expected = new Models.ResponseModel<object>
            {
                //ステータス
                Status = "success",

                //エラーコード
                ErrorCode = "",

                //エラー内容
                ErrorMessage = "",

                //取得データ
                Results = Enumerable.Empty<object>(),
            };

            // メソッド実行
            var result = ResponseUtil.GetResponse<object>(true, messageType);

            // 結果検証
            Assert.AreEqual(expected.Status, result.Status);
            Assert.AreEqual(expected.ErrorCode, result.ErrorCode);
            Assert.AreEqual(expected.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(expected.Results, result.Results);
        }

        [TestMethod()]
        public void GetResponseTest9()
        {
            // 初期設定
            var messageType = MessageType.None;
            var expected = new Models.ResponseModel<object>
            {
                //ステータス
                Status = "failure",

                //エラーコード
                ErrorCode = "KKE03001",

                //エラー内容
                ErrorMessage = "予期しないエラーが発生しました。システム管理者に連絡してください。",

                //取得データ
                Results = Enumerable.Empty<object>(),
            };

            // メソッド実行
            var result = ResponseUtil.GetResponse<object>(false, messageType);

            // 結果検証
            Assert.AreEqual(expected.Status, result.Status);
            Assert.AreEqual(expected.ErrorCode, result.ErrorCode);
            Assert.AreEqual(expected.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(expected.Results, result.Results);
        }
        #endregion
    }
}