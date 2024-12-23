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
    /// 月例点検一括省略
    /// </summary>
    public class ExceptMonthlyInspectionController : BaseAPIController<ExceptMonthlyInspectionLogic, ExceptMonthlyInspectionOutModels>
    {
        /// <summary>
        /// 月例点検一括省略取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetData()));

        }
        /// <summary>
        /// 月例点検一括省略追加
        /// </summary>
        /// <param name="list">月例点検一括省略対象</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<ExceptMonthlyInspectionUpdateModels> list)
        {
            return Ok(base.GetResponse(base.GetLogic().PostData(list)));
        }
    }
}
