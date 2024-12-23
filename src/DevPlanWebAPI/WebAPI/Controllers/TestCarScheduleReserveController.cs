using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 試験車スケジュール一括本予約
    /// </summary>
    public class TestCarScheduleReserveController : BaseAPIController<TestCarScheduleReserveLogic, TestCarSearchOutModel>
    {
        /// <summary>
        /// 試験車スケジュール一括本予約更新
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]TestCarScheduleReserveModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateTestCarScheduleItem(val)));

        }
    }
}
