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
    //Append Start 2021/08/20 杉浦 設計チェック請負
    /// <summary>
    /// 設計チェックインポート
    /// </summary>
    public class DesignCheckImportGetPathController : BaseAPIController<DesignCheckImportLogic, DesignCheckPathModel>
    {
        #region 検索
        /// <summary>
        /// 設計チェック検索
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            var result = Ok(base.GetResponse(base.GetLogic().GetPathData()));
            return result;
        }
        #endregion
    }
    //Append End 2021/08/20 杉浦 設計チェック請負
}