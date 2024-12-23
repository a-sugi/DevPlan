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
    /// ユーザー検索条件
    /// </summary>
    public class ControlSheetSearchConditionController : BaseAPIController<ControlSheetLogic, ControlSheetSearchConditionModel>
    {
        #region 取得
        /// <summary>
        /// ユーザー検索条件検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ControlSheetSearchConditionSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetUserSearchCondition(cond)));

        }
        #endregion

        #region 登録
        /// <summary>
        /// ユーザー検索条件登録
        /// </summary>
        /// <param name="list">ユーザー検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<ControlSheetSearchConditionModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().MergeUserSearchCondition(list)));

        }
        #endregion

        #region 削除
        /// <summary>
        /// ユーザー検索条件削除
        /// </summary>
        /// <param name="list">ユーザー検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<ControlSheetSearchConditionModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteUserSearchCondition(list)));

        }
        #endregion

    }
}
