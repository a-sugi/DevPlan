using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 受領先情報
    /// </summary>
    public class RecipientController : BaseAPIController<RecipientLogic, RecipientGetOutModel>
    {
        /// <summary>
        /// 受領先情報検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]RecipientGetInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData()));
        }
    }
}
