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
    /// 廃却車両搬出日入力
    /// </summary>
    public class DisposalCarryoutController : BaseAPIController<DisposalCarryoutLogic, TestCarCommonModel>
    {
        #region 廃却車両搬出日検索
        /// <summary>
        /// 廃却車両搬出日検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarCommonSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(cond)));
        }
        #endregion

        #region 廃却車両搬出日更新
        /// <summary>
        /// 廃却車両搬出日更新
        /// </summary>
        /// <param name="list">更新項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<TestCarCommonModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().Put(list)));
        }
        #endregion
    }
}
