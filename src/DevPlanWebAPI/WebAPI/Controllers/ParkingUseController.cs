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
    /// 駐車場管理
    /// </summary>
    public class ParkingUseController : BaseAPIController<ParkingUseLogic, ParkingUseModel>
    {
        #region 駐車場管理検索
        /// <summary>
        /// 駐車場管理検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ParkingUseSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(cond)));
        }
        #endregion

        #region 駐車場管理登録
        /// <summary>
        /// 駐車場管理登録
        /// </summary>
        /// <param name="list">駐車場管理項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<ParkingUseModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().Post(list)));
        }
        #endregion

        #region 駐車場管理削除
        /// <summary>
        /// 駐車場管理削除
        /// </summary>
        /// <param name="list">駐車場管理項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<ParkingUseModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().Delete(list)));
        }
        #endregion
    }
}
