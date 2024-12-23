using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// カーシェア管理
    /// </summary>
    public class CarShareManagementController : BaseAPIController<CarShareManagementLogic, CarShareManagementItemModel>
    {
        #region カーシェア管理取得
        /// <summary>
        /// カーシェア管理取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarShareManagementItemSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetCarShareManagementItem(cond)));
        }
        #endregion

        #region カーシェア管理更新
        /// <summary>
        /// カーシェア管理更新
        /// </summary>
        /// <param name="list">カーシェア管理一覧項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<CarShareManagementItemModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateCarShareManagementItem(list)));
        }
        #endregion
    }
}