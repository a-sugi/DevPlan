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
    /// カレンダー稼働日非稼働日コントローラー。
    /// </summary>
    public class CalendarKadouController : BaseAPIController<CalendarKadouLogic, CalendarKadouModel>
    {
        /// <summary>
        /// カレンダー稼働日非稼働日取得。
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CalendarKadouSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(cond)));
        }
    }
}