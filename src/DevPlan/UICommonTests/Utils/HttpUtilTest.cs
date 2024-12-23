using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UICommonTests.Utils
{
    [TestClass()]
    public class HttpUtilTest
    {
        [TestMethod()]
        public void HttpRequestTest()
        {
            try
            {
                //コントロールがない(404) or サーバーに繋がらない（IIS以前に途切れる場合。AggregateException）
                //Infoログ＆StatusFailureの組み合わせ。
                var res = HttpUtil.PostResponse<LoginOutModel>((ControllerType)144444, true);

                Assert.Fail();//例外が出ないとエラー。
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        [TestMethod()]
        public void HttpRequestTest3()
        {
            //システム例外
            try
            {
                HttpUtil.GetResponse<LoginOutModel>(WebRequestMethods.Http.Get, "aiuoe", null, false);
                Assert.Fail();//例外が出ないとエラー。
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        [TestMethod()]
        public void HttpReqestTest4()
        {
            try
            {
                //コントロールはあるけどメソッドが無い。400になる。
                var res = HttpUtil.GetResponse<LoginOutModel>(ControllerType.Login, true);
                Assert.Fail();//例外が出ないとエラー。
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }

        }

        [TestMethod()]
        public void HttpReqestTest5()
        {
            try
            {
                //レスポンスが取得できない。 ログに出力されるmethodで判断できる・・。
                var res = HttpUtil.GetResponse<LoginOutModel>("", "", null, false);
                Assert.Fail();//例外が出ないとエラー。
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        [TestMethod()]
        public void HttpRequestTest6()
        {
            //URL制限オーバー。
            var res = HttpUtil.GetResponse<LoginOutModel>(WebRequestMethods.Http.Get,
                @"http://localhost:8080/grewaaaaaaaaaaaaaaaaaaa?aaaaaaaaaaaaaaaaaaaaaaaagrewaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXgrewaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXgrewaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXgrewaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXgrewaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXgrewaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXgrewaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXgrewaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXgrewaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXgrewaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaagjeaigjoXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                null, false);
            Assert.AreEqual(Const.StatusFailure, res.Status);
        }

        [TestMethod()]
        public void HttpRequestTest7()
        {
            //HttpUtilにメッセージボックス表示依頼を出す。これはログは出ない（ログインできませんでした等、業務系のエラーなので）
            var res = HttpUtil.PostResponse<LoginOutModel>(ControllerType.Login, true);
            Assert.AreEqual(Const.StatusFailure, res.Status);
        }

        private static void ErrorMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(String.Format(" * ********* {0} **********", DateTime.Now));
            if (ex.InnerException != null)
            {
                sb.Append("Inner Exception Type: ");
                sb.AppendLine(ex.InnerException.GetType().ToString());
                sb.Append("Inner Exception: ");
                sb.AppendLine(ex.InnerException.Message);
                sb.Append("Inner Source: ");
                sb.AppendLine(ex.InnerException.Source);
                if (ex.InnerException.StackTrace != null)
                {
                    sb.AppendLine("Inner Stack Trace: ");
                    sb.AppendLine(ex.InnerException.StackTrace);
                }
            }
            sb.Append("Exception Type: ");
            sb.AppendLine(ex.GetType().ToString());
            sb.AppendLine("Exception: " + ex.Message);
            sb.AppendLine("Source: " + ex.Source);
            sb.AppendLine("Stack Trace: ");
            if (ex.StackTrace != null)
            {
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine();
            }

            Console.WriteLine("■↓ログに出力される例外");
            Console.WriteLine(ex);

            Console.WriteLine("■↓以下はエラーメッセージボックスに出るex");
            Console.WriteLine(sb.ToString());
        }

        [TestMethod()]
        public void HttpRequestTest2()
        {
            try
            {
                //500とか返したければこちらでテスト。

                //Statusがnull
                var res = HttpUtil.GetResponse<LoginOutModel>(WebRequestMethods.Http.Get,
                    @"http://localhost:8089/api/values",
                    null, false);

                Assert.Fail();//例外が出ないとエラー。
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        [TestMethod()]
        public void HttpRequestTest2_2()
        {
            try
            {
                //Resultが空（JsonConvert失敗）
                var res = HttpUtil.GetResponse<LoginOutModel>(WebRequestMethods.Http.Get,
                    @"http://localhost:8089/api/values/111",
                    null, false);

                Assert.Fail();//例外が出ないとエラー。
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        [TestMethod()]
        public void HttpRequestTest2_3()
        {
            try
            {
                //Statusが空欄
                var res = HttpUtil.GetResponse<LoginOutModel>(WebRequestMethods.Http.Get,
                        @"http://localhost:8089/api/Default",
                        null, false);

                Assert.Fail();//例外が出ないとエラー。
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }
    }
}
