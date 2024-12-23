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
    /// 試験車使用履歴
    /// </summary>
    public class TestCarUseHistoryController : BaseAPIController<ApplicationApprovalCarLogic, TestCarUseHistoryModel>
    {
        #region 取得
        /// <summary>
        /// 試験車使用履歴検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarUseHistorySearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetSiyouRireki(cond)));

        }
        #endregion

        #region 登録
        /// <summary>
        /// 試験車使用履歴申請
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<ApplicationApprovalCarModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().ApplyTestCarUseHistory(list)));
        }
        #endregion

        #region 更新
        /// <summary>
        /// 試験車使用履歴承認
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<ApplicationApprovalCarModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().ApprovalTestCarUseHistory(list)));

        }
        #endregion

        #region 削除
        /// <summary>
        /// 試験車使用履歴中止
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<ApplicationApprovalCarModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().StopTestCarUseHistory(list)));

        }
        #endregion
    }
}
