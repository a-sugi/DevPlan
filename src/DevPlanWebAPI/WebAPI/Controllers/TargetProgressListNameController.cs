//
// 業務計画表システム
// 目標進度リスト名API
// 作成者 : 岸　義将
// 作成日 : 2017/03/15

using System;
using System.Collections.Generic;
using System.Web.Http;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 目標進度リスト名API コントローラ
    /// </summary>
    public class TargetProgressListNameController : BaseAPIController<TargetProgressListNameLogic, TargetProgressListNameSearchOutModel>
    {
        #region 目標進度リスト名検索
        /// <summary>
        ///  目標進度リスト名検索
        /// </summary>
        /// <param name="val">目標進度リスト項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TargetProgressListNameSearchInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));
        }
        #endregion

        #region 目標進度リスト名更新
        /// <summary>
        /// 目標進度リスト名更新
        /// </summary>
        /// <param name="val">目標進度リスト項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(TargetProgressListNameUpdateInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().PutData(val)));

        }
        #endregion
    }
}
