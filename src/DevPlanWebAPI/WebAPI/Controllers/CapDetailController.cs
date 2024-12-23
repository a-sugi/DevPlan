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
    /// CAP評価車両詳細
    /// </summary>
    public class CapDetailController : BaseAPIController<CapDetailLogic, CapDetailModel>
    {
        #region CAP評価車両詳細取得
        /// <summary>
        /// CAP評価車両詳細取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CapDetailSearchModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(val)));
        }
        #endregion
    }
}