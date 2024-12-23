using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;

using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 部長名コントローラー
    /// </summary>
    public class ManagerNameController : BaseAPIController<ManagerNameLogic, ManagerNameModel>
    {
        /// <summary>
        /// 部長名取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ManagerNameSearchModel val)
        {
            //検索実行
            return Ok(base.GetResponse(base.GetLogic().Get(val)));

        }
    }
}
