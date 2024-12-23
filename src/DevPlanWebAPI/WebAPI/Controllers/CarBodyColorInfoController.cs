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
    /// 車体色情報
    /// </summary>
    public class CarBodyColorInfoController : BaseAPIController<CarBodyColorInfoLogic, CommonMasterModel>
    {
        #region 取得
        /// <summary>
        /// 車体色情報取得
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
