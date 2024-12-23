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
    /// 管理票検索
    /// </summary>
    public class ControlSheetController : BaseAPIController<ControlSheetLogic, TestCarCommonModel>
    {
        #region 取得
        /// <summary>
        /// 管理票検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(ControlSheetModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCar(cond)));

        }
        #endregion

    }
}
