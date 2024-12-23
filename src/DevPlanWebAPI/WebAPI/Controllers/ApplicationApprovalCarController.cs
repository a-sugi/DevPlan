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
    /// 処理待ち車両リスト
    /// </summary>
    public class ApplicationApprovalCarController : BaseAPIController<ApplicationApprovalCarLogic, ApplicationApprovalCarModel>
    {
        #region 取得
        /// <summary>
        /// 処理待ち車両リスト検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(ApplicationApprovalCarSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCar(cond)));

        }
        #endregion
    }
}
