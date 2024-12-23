using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using Newtonsoft.Json;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// スケジュール利用車
    /// </summary>
    public class ScheduleCarController : BaseAPIController<ScheduleCarLogic, ScheduleCarModel>
    {
        #region スケジュール利用車の取得
        /// <summary>
        /// スケジュール利用車の取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ScheduleCarSearchModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(val)));
        }
        #endregion
    }
}