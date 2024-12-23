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
    /// 承認状況
    /// </summary>
    public class ApprovalStatusController : BaseAPIController<ApplicationApprovalCarLogic, ApprovalStepModel>
    {
        #region 取得
        /// <summary>
        /// 承認状況検索
        /// </summary>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetApprovalStep(isComplete: true)));

        }
        #endregion

    }
}
