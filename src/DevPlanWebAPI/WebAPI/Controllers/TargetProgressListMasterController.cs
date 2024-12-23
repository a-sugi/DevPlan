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
    /// 目標進度リストマスタ
    /// </summary>
    public class TargetProgressListMasterController : BaseAPIController<TargetProgressListMasterLogic, TargetProgressListMasterModel>
    {
        #region 目標進度リストマスター取得
        /// <summary>
        /// 目標進度リストマスター取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TargetProgressListMasterSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTargetProgressListMaster(cond)));

        }
        #endregion

        #region 目標進度リストマスター登録
        /// <summary>
        /// 目標進度リストマスター登録
        /// </summary>
        /// <param name="list">目標進度_項目マスター</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<TargetProgressListMasterModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().MergeTargetProgressListMaster(list)));

        }
        #endregion
    }
}
