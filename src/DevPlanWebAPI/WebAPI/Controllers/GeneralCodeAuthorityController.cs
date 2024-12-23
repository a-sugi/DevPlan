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
    public class GeneralCodeAuthorityController : BaseAPIController<GeneralCodeAuthorityLogic, GeneralCodeAuthorityOutModel>
    {
        #region 検索
        /// <summary>
        ///開発符号権限取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]GeneralCodeAuthorityInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));
        }
        #endregion

        #region 登録
        /// <summary>
        /// 開発符号権限登録
        /// </summary>
        /// <param name="val">開発符号リスト</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<GeneralCodeAuthorityEntryModel> val)
        {
            return Ok(base.GetResponse(base.GetLogic().Post(val)));
        }
        #endregion

        # region 更新
        /// <summary>
        /// 開発符号権限更新
        /// </summary>
        /// <param name="val">開発符号リスト</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<GeneralCodeAuthorityEntryModel> val)
        {
            return Ok(base.GetResponse(base.GetLogic().MergeData(val)));
        }
        #endregion

        #region 削除
        /// <summary>
        /// 開発符号権限削除
        /// </summary>
        /// <param name="val">開発符号リスト</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<GeneralCodeAuthorityDeleteModel> val)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteData(val)));
        }
        #endregion
    }
}
