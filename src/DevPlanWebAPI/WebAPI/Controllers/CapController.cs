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
    /// CAP
    /// </summary>
    public class CapController : BaseAPIController<CapLogic, CapModel>
    {
        #region CAP課題検索
        /// <summary>
        /// CAP課題検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CapSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(cond)));
        }
        #endregion

        #region CAP課題登録
        /// <summary>
        /// CAP課題登録
        /// </summary>
        /// <param name="list">CAP課題項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<CapModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().Post(list)));
        }
        #endregion

        #region CAP課題更新
        /// <summary>
        /// CAP課題更新
        /// </summary>
        /// <param name="list">CAP課題項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<CapModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().Put(list)));
        }
        #endregion

        #region CAP課題削除
        /// <summary>
        /// CAP課題削除
        /// </summary>
        /// <param name="list">CAP課題項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<CapModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().Delete(list)));
        }
        #endregion
    }
}
