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
    /// ロール
    /// </summary>
    public class RollAuthorityController : BaseAPIController<RollAuthorityLogic, RollAuthorityGetOutModel>
    {
        /// <summary>
        /// ロール権限検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]RollAuthorityGetInModel val)
        {
            var list = base.GetLogic().GetData(val);

            if (list == null || list.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            return Ok(base.GetResponse(list));
        }

        /// <summary>
        /// ロール権限登録
        /// </summary>
        /// <param name="list">登録データ</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<RollAuthorityPostInModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().PostData(list)));
        }

        /// <summary>
        /// ロール権限付替
        /// </summary>
        /// <param name="list">登録データ</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<RollAuthorityPutInModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().PutData(list)));
        }

        /// <summary>
        /// ロール削除
        /// </summary>
        /// <param name="val">削除条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(RollAuthorityDeleteInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteData(val)));
        }
    }
}
