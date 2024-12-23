//
// 業務計画表システム
// KKA15010 機能権限マスタ編集API
// 作成者 : 岸　義将
// 作成日 : 2017/02/21

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
    /// 機能権限
    /// </summary>
    public class FunctionAuthorityController : BaseAPIController<FunctionAuthorityLogic, FunctionAuthorityOutModel>
    {
        #region 取得
        /// <summary>
        /// 機能権限取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]FunctionAuthorityInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));

        }
        #endregion

        #region 機能権限登録
        /// <summary>
        /// 機能権限登録
        /// </summary>
        /// <param name="val">機能権限リスト</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<FunctionAuthorityRegistModel> val)
        {
            return Ok(base.GetResponse(base.GetLogic().Post(val)));
        }
        #endregion

        #region 機能権限更新
        /// <summary>
        /// 機能権限更新
        /// </summary>
        /// <param name="val">機能権限リスト</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<FunctionAuthorityUpdateModel> val)
        {
            return Ok(base.GetResponse(base.GetLogic().PutData(val)));
        }
        #endregion

        #region 機能権限削除
        /// <summary>
        /// 機能権限削除
        /// </summary>
        /// <param name="val">機能権限リスト</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<FunctionAuthorityDeleteModel> val)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteData(val)));
        }
        #endregion
    }
}
