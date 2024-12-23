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
    /// 試験車取得
    /// </summary>
    public class TestCarController : BaseAPIController<TestCarLogic, TestCarSearchOutModel>
    {
        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarSearchInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCarSearch(val)));

        }
    }
}
