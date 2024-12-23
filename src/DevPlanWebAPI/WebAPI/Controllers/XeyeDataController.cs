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
    public class XeyeDataController : BaseAPIController<XeyeLogic, XeyeDataSearchOutModel>
    {
        #region 取得
        /// <summary>
        /// XeyeID取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            var result = Ok(base.GetResponse(base.GetLogic().GetXeyeData()));
            return result;

        }
        #endregion
    }
}