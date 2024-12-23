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
    /// 試験車使用履歴変更チェック
    /// </summary>
    public class TestCarUseHistoryChangeCheckController : BaseAPIController<ApplicationApprovalCarLogic, ApplicationApprovalCarModel>
    {
        #region 取得
        /// <summary>
        /// 試験車使用履歴権限チェック
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<ApplicationApprovalCarModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().IsTestCarUseHistoryChange(list)));

        }
        #endregion

    }
}
