using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// トラックスケジュール数コントローラー
    /// </summary>
    public class TruckScheduleCountController : BaseAPIController<TruckScheduleLogic, TruckScheduleCountModel>
    {
        /// <summary>
        /// トラックスケジュール数取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TruckScheduleSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetScheduleCount(cond)));
        }
    }
}
