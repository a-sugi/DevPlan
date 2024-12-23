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
    /// ユーザー検索マスター
    /// </summary>
    public class ControlSheetSearchMasterController : BaseAPIController<ControlSheetLogic, CommonMasterModel>
    {
        #region 取得
        /// <summary>
        /// ユーザー検索マスター検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ControlSheetSearchMasterModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetControlSheetSearchMaster(cond)));

        }
        #endregion

    }
}
