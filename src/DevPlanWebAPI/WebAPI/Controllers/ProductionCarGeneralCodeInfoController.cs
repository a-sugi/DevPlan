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
    /// 開発符号情報(製作車)
    /// </summary>
    public class ProductionCarGeneralCodeInfoController : BaseAPIController<ProductionCarGeneralCodeInfoLogic, CommonMasterModel>
    {
        /// <summary>
        /// 開発符号情報(製作車)検索
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetMaster()));
        }
    }
}
