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
    /// 担当検索
    /// </summary>
    public class SectionGroupController : BaseAPIController<SectionGroupLogic, SectionGroupSearchOutModel>
    {
        /// <summary>
        /// 担当取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]SectionGroupSearchInModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(cond)));

        }
    }
}
