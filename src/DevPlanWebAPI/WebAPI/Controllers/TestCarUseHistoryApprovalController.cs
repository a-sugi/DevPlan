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
    /// 試験車使用履歴承認状況
    /// </summary>
    public class TestCarUseHistoryApprovalController : BaseAPIController<ApplicationApprovalCarLogic, TestCarUseHistoryApprovalModel>
    {
        #region 取得
        /// <summary>
        /// 試験車使用履歴承認状況検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarUseHistorySearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCarUseHistoryApproval(cond)));

        }
        #endregion

    }
}
