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
    /// トランスミッション情報
    /// </summary>
    public class TransmissionInfoController : BaseAPIController<TransmissionInfoLogic, CommonMasterModel>
    {
        #region 取得
        /// <summary>
        /// トランスミッション情報取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetMaster()));

        }
        #endregion

    }
}
