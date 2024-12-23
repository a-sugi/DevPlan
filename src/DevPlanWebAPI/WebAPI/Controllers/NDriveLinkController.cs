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
    /// 写真・動画検索API
    /// </summary>
    public class NDriveLinkController : BaseAPIController<NDriveLinkLogic, NDriveLinkOutModel>
    {
        /// <summary>
        /// 写真・動画取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]NDriveLinkInModel val)
        {
            var dt = base.GetLogic().GetNDriveLinkListData(val);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            var nDriveLinks = new List<NDriveLinkOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                nDriveLinks.Add(new NDriveLinkOutModel
                {
                    ID = Convert.ToInt64(dr["ID"]),
                    CAP_ID = Convert.ToInt64(dr["CAP_ID"]),
                    TITLE = Convert.ToString(dr["TITLE"]),
                    URL = Convert.ToString(dr["URL"]),
                    INPUT_DATETIME = Convert.ToDateTime(dr["CHANGE_DATETIME"]),
                    NAME = Convert.ToString(dr["NAME"]),
                    DATETITLE = string.Format("{0:yyyy/MM/dd} {1}", dr["CHANGE_DATETIME"], dr["TITLE"])
                });
            }

            return Ok(base.GetResponse(nDriveLinks));

        }

        /// <summary>
        /// 写真・動画登録
        /// </summary>
        /// <param name="val">写真・動画登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]NDriveLinkRegistInModel val)
        {
            db.Begin();
            var flg = base.GetLogic().PostNDriveLinkListData(val);

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
        /// 写真・動画更新
        /// </summary>
        /// <param name="val">写真・動画更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]NDriveLinkUpdateInModel val)
        {
            db.Begin();

            var flg = base.GetLogic().PutNDriveLinkListData(val);
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
        /// 写真・動画削除
        /// </summary>
        /// <param name="val">写真・動画削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]NDriveLinkDeleteInModel val)
        {
            db.Begin();

            var flg = base.GetLogic().DeleteNDriveLinkListData(val);
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
