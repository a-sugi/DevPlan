using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Filters
{
    /// <summary>
    /// 共通クラスフィルタ
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CommonMethodFilterAttribute : ActionFilterAttribute
    {
        #region メンバ変数
        private Logging logger = new Logging();
        #endregion

        /// <summary>
        /// アクションの事前処理
        /// </summary>
        /// <param name="actionContext">アクション実行コンテキスト</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //モデルの検証がOKかどうか
            if (actionContext.ModelState.IsValid == false)
            {
                //エラーになったパラメータをログ出力
                var errMsg = actionContext.ModelState.SelectMany(x => x.Value.Errors, (x, e) => e.ErrorMessage).ToArray();
                logger.WriteLog(logger.Warn, string.Join(Const.CrLf, errMsg));

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ResponseUtil.GetResponse<object>(MessageType.KKE03020));

            }

        }

        /// <summary>
        /// アクションの事後処理
        /// </summary>
        /// <param name="actionExecutedContext">アクション実行コンテキスト</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        }
    }
}