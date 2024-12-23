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
    /// 指定月台数リスト
    /// </summary>
    public class DesignatedMonthNumberController : BaseAPIController<DesignatedMonthNumberLogic, TestCarCommonModel>
    {
        #region 取得
        /// <summary>
        /// 指定月台数リスト検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(DesignatedMonthNumberSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCar(cond)));

        }
        #endregion

    }
}
