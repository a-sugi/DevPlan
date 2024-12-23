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
    public class RollController : BaseAPIController<RollLogic, RollModel>
    {
        /// <summary>
        /// ロール検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]RollGetInModel val)
        {
            var list = base.GetLogic().GetData(val);

            if (list == null || list.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            return Ok(base.GetResponse(list));
        }

        /// <summary>
        /// ロール登録
        /// </summary>
        /// <param name="list">登録データ</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<RollPostInModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().PostData(list)));
        }

        /// <summary>
        /// ロール更新
        /// </summary>
        /// <param name="list">更新データ</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<RollPutInModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().PutData(list)));
        }

        /// <summary>
        /// ロール削除
        /// </summary>
        /// <param name="val">削除条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(RollDeleteInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteData(val)));
        }
    }
}
