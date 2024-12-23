using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 稼働率算出コントローラー。
    /// </summary>
    public class CarShareManagementRatePrintController : BaseAPIController<CarShareManagementLogic, CarShareManagementRatePrintOutModel>
    {
        /// <summary>
        /// 稼働率算出取得処理。
        /// </summary>
        /// <remarks>
        /// 指定された条件を元に稼働率算出を算出します。
        /// </remarks>
        /// <param name="val"></param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarShareManagementRatePrintSearchModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTest(val)));
        }
    }
}