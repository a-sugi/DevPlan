//
// 業務計画表システム
// KKA00130～160 お知らせAPI
// 作成者 : 岸　義将
// 作成日 : 2017/02/13

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
    /// お知らせ検索API
    /// </summary>
    public class InformationController : BaseAPIController<InformationLogic, InformationOutModel>
    {
        /// <summary>
        /// お知らせ取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]InformationInModel val)
        {
            var dt = base.GetLogic().GetAnnounceListData(val);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            var announces = new List<InformationOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                announces.Add(new InformationOutModel
                {
                    ID = Convert.ToInt64(dr["ID"]),
                    TITLE = Convert.ToString(dr["TITLE"]),
                    URL = Convert.ToString(dr["URL"]),
                    RELEASE_START_DATE = Convert.ToDateTime(dr["RELEASE_START_DATE"]),
                    RELEASE_END_DATE = Convert.ToDateTime(dr["RELEASE_END_DATE"]),
                    INPUT_DATETIME = Convert.ToDateTime(dr["CHANGE_DATETIME"]),
                    NAME = Convert.ToString(dr["NAME"]),
                    DATETITLE = string.Format("{0:yyyy/MM/dd} {1}", dr["CHANGE_DATETIME"], dr["TITLE"])
                });
            }

            return Ok(base.GetResponse(announces));

        }

        /// <summary>
        /// お知らせ登録
        /// </summary>
        /// <param name="val">お知らせ登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]InformationRegistInModel val)
        {
            db.Begin();
            var flg = base.GetLogic().PostAnnounceListData(val);

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
        /// お知らせ更新
        /// </summary>
        /// <param name="val">お知らせ更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]InformationUpdateInModel val)
        {
            db.Begin();

            var flg = base.GetLogic().PutAnnounceListData(val);
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
        /// お知らせ削除
        /// </summary>
        /// <param name="val">お知らせ削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]InformationDeleteInModel val)
        {
            db.Begin();

            var flg = base.GetLogic().DeleteAnnounceListData(val);
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
