using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using Newtonsoft.Json;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 車両
    /// </summary>
    public class CarController : BaseAPIController<CarLogic, CarModel>
    {
        #region 車両取得
        /// <summary>
        /// 車両取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarSearchModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(val)));
        }
        #endregion
    }
}