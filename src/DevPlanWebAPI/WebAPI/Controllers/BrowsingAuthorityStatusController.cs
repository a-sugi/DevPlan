using System.Collections.Generic;
using System.Web.Http;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 開発符号権限
    /// </summary>
    public class BrowsingAuthorityStatusController : BaseAPIController<BrowsingAuthorityStatusLogic, BrowsingAuthorityStatusOutModel>
    {
        #region 検索
        /// <summary>
        ///閲覧権限状況取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]BrowsingAuthorityStatusInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));
        }
        #endregion

        # region 更新
        /// <summary>
        /// 閲覧権限状況更新
        /// </summary>
        /// <param name="val">更新データ</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(BrowsingAuthorityStatusPutModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().PutData(val)));
        }
        #endregion
    }
}
