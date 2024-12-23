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
    /// 台数集計結果
    /// </summary>
    public class NumberAggregateController : BaseAPIController<DesignatedMonthNumberLogic, NumberAggregateModel>
    {
        #region 取得
        /// <summary>
        /// 台数集計結果検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(DesignatedMonthNumberSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCarAggregate(cond)));

        }
        #endregion

    }
}
