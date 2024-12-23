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
    /// 試験車使用履歴存在チェック
    /// </summary>
    public class TestCarUseHistoryExistCheckController : BaseAPIController<TestCarHistoryLogic, TestCarUseHistoryModel>
    {
        #region 取得
        /// <summary>
        /// 試験車使用履歴存在チェック
        /// </summary>
        /// <param name="list">試験車使用履歴</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<TestCarUseHistoryModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().IsTestCarUseHistoryExistCheck(list)));

        }
        #endregion

    }
}
