using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 操作ログコントローラー
    /// </summary>
    [CommonClassFilter()]
    public class OperationLogController : ApiController
    {
        #region 操作ログ登録
        /// <summary>
        /// 操作ログ登録
        /// </summary>
        /// <param name="val">操作ログモデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult GET(OperationLogModel val)
        {
            return Ok(ResponseUtil.GetResponse<object>(true, MessageType.None));
        }
        #endregion
    }
}
