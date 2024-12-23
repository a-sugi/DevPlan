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
    /// 試験車衝突車管理部署
    /// </summary>
    public class TestCarCollisionCarManagementDepartmentController : BaseAPIController<TestCarCollisionCarManagementDepartmentLogic, TestCarCollisionCarManagementDepartmentModel>
    {
        #region 取得
        /// <summary>
        /// 試験車衝突車管理部署取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarCollisionCarManagementDepartmentSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(cond)));

        }
        #endregion

    }
}
