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
    /// CAP指摘分類
    /// </summary>
    public class CapIdentificationController : BaseAPIController<CapIdentificationLogic, object>
    {
        #region 取得
        /// <summary>
        /// CAP指摘分類取得
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
