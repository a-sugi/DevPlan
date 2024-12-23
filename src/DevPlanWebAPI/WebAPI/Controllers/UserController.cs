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
    /// ユーザー検索
    /// </summary>
    public class UserController : BaseAPIController<UserLogic, UserSearchOutModel>
    {
        /// <summary>
        /// ユーザー取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]UserSearchInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));

        }
    }
}
