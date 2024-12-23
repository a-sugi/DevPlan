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
    /// ユーザー検索項目
    /// </summary>
    public class ControlSheetSearchItemController : BaseAPIController<ControlSheetLogic, UserSearchItemModel>
    {
        #region 取得
        /// <summary>
        /// ユーザー検索項目取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetUserSearchItem()));

        }
        #endregion

    }
}
