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
    /// CAPフォロー状況
    /// </summary>
    public class CapFollowController : BaseAPIController<CapFollowLogic, object>
    {
        #region 取得
        /// <summary>
        /// CAPフォロー状況取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().Get()));
        }
        #endregion
    }
}
