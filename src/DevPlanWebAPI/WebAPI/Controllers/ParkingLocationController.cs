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
    /// 駐車場所在地
    /// </summary>
    public class ParkingLocationController : BaseAPIController<ParkingLocationLogic, ParkingModel>
    {
        #region 駐車場所在地検索
        /// <summary>
        /// 駐車場所在地検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ParkingSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(cond)));
        }
        #endregion
    }
}
