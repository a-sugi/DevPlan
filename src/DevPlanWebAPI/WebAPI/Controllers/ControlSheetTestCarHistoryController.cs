using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 試験車履歴情報(管理票)
    /// </summary>
    public class ControlSheetTestCarHistoryController : BaseAPIController<ControlSheetTestCarHistoryLogic, ControlSheetTestCarHistoryGetOutModel>
    {
        /// <summary>
        /// 試験車履歴情報(管理票)検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ControlSheetTestCarHistoryGetInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));
        }

        /// <summary>
        /// 試験車履歴情報(管理票)登録
        /// </summary>
        /// <param name="val">試験車履歴情報(管理票)登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]ControlSheetTestCarHistoryPostInModel val)
        {
            db.Begin();

            var flg = base.GetLogic().PostData(val);

            if (flg == false)
            {
                db.Rollback();
            }
            else
            {
                db.Commit();
            }

            return Ok(base.GetResponse(flg));
        }

        /// <summary>
        /// 試験車履歴情報(管理票)更新
        /// </summary>
        /// <param name="list">試験車履歴情報(管理票)更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]List<ControlSheetTestCarHistoryPutInModel> list)
        {
            db.Begin();

            var flg = base.GetLogic().PutData(list);

            if (flg == false)
            {
                db.Rollback();
            }
            else
            {
                db.Commit();
            }

            return Ok(base.GetResponse(flg));
        }

        /// <summary>
        /// 試験車履歴情報(管理票)削除
        /// </summary>
        /// <param name="val">試験車履歴情報(管理票)削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]ControlSheetTestCarHistoryDeleteInModel val)
        {
            db.Begin();

            var flg = base.GetLogic().DeleteData(val);

            if (flg == false)
            {
                db.Rollback();
            }
            else
            {
                db.Commit();
            }

            return Ok(base.GetResponse(flg));
        }
    }
}
