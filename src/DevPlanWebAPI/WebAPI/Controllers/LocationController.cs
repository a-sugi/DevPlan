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
    /// 所在地
    /// </summary>
    public class LocationController : BaseAPIController<LocationLogic, LocationSearchOutModel>
    {
        /// <summary>
        /// 所在地取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]LocationSearchInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));

        }
    }
}
