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
    /// 試験車日程 作業簡易入力
    /// </summary>
    public class TestCarHistoryCompleteController : BaseAPIController<TestCarLogic, TestCarCompleteScheduleModel>
    {
        /// <summary>
        /// 試験車日程作業簡易入力一覧取得
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarCompleteScheduleSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCarCompleteSchedule(cond)));
        }
    }
}
