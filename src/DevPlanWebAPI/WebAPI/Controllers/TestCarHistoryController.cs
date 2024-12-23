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
    /// 試験車履歴
    /// </summary>
    public class TestCarHistoryController : BaseAPIController<TestCarHistoryLogic, TestCarCommonModel>
    {
        #region 取得
        /// <summary>
        /// 試験車履歴取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(TestCarCommonSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCarHistory(cond)));

        }
        #endregion

        #region 更新
        /// <summary>
        /// 試験車履歴更新
        /// </summary>
        /// <param name="list">試験車履歴</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<TestCarHistoryModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateTestCarHistory(list)));

        }
        #endregion
    }
}
