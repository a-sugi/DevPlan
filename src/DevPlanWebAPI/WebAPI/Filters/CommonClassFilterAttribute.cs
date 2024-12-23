using System;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace DevPlanWebAPI.Filters
{
    /// <summary>
    /// 共通クラスフィルタ
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class CommonClassFilter : ActionFilterAttribute
    {
        
        private Logging Log = new Logging();

        /// <summary>
        /// アクションの事前処理
        /// </summary>
        /// <param name="actionContext">アクション実行コンテキスト</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var log = new
            {
                LogType = "OnActionStart",
                Controller = actionContext.ControllerContext.Controller.ToString(),
                Method = actionContext.Request.Method.Method,
                RequestUri = HttpUtility.UrlDecode(actionContext.Request.RequestUri.ToString()),
//                RequestHeaders = actionContext.Request.Headers,
            };

            Log.WriteLog(Log.Info, JsonConvert.SerializeObject(log));

            base.OnActionExecuting(actionContext);
        }

        /// <summary>
        /// アクションの事後処理
        /// </summary>
        /// <param name="actionExecutedContext">アクション実行コンテキスト</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var response = actionExecutedContext.Response;
            if (response != null)
            {
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    NoCache = true
                };

            }

            // 戻りデータのログ出力は不要
            Func<string, string> getNoneResults = s => Regex.Replace(s, @"\""results\"":.*,\""pageInfo\""", @"""pageInfo""");

            var log = new
            {
                LogType = "OnActionEnd",
                HTTPStatus = (response == null) ? "" : response.StatusCode.ToString(),
            };

            Log.WriteLog(Log.Info, JsonConvert.SerializeObject(log));


            var traceLog = new
            {
                Request = actionExecutedContext.Request.ToString(),
                Parameter = actionExecutedContext.ActionContext.ActionArguments,
                Response = (response == null || response.Content == null) ? "" : getNoneResults(response.Content.ReadAsStringAsync().Result),
            };

            Log.WriteLog(Log.Trace, JsonConvert.SerializeObject(traceLog));


            base.OnActionExecuted(actionExecutedContext);
        }

        /// <summary>
        /// アクションの事前処理（Async）
        /// </summary>
        /// <param name="actionContext">アクション実行コンテキスト</param>
        /// <param name="cancellationToken">接続トークン</param>
        /// <returns>Task</returns>
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        /// <summary>
        /// アクションの事後処理（Async）
        /// </summary>
        /// <param name="actionExecutedContext">アクション実行コンテキスト</param>
        /// <param name="cancellationToken">接続トークン</param>
        /// <returns>Task</returns>
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}