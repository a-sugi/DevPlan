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
    /// 駐車場区画
    /// </summary>
    public class ParkingSectionController : BaseAPIController<ParkingSectionLogic, ParkingModel>
    {
        #region 駐車場区画検索
        /// <summary>
        /// 駐車場区画検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ParkingSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(cond)));
        }
        #endregion

        #region 駐車場区画更新
        /// <summary>
        /// 駐車場区画更新
        /// </summary>
        /// <param name="list">駐車場区画項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<ParkingModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().Put(list)));
        }
        #endregion
    }
}
