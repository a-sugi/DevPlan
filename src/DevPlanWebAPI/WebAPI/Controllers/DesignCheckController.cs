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
    /// 設計チェック
    /// </summary>
    public class DesignCheckController : BaseAPIController<DesignCheckLogic, DesignCheckGetOutModel>
    {
        /// <summary>
        /// 設計チェック検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DesignCheckGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<DesignCheckGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new DesignCheckGetOutModel
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    開催日 = Convert.ToDateTime(dr["開催日"]),
                    回 = dr["回"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["回"]),
                    名称 = Convert.ToString(dr["名称"]),
                    OPEN_COUNT = Convert.ToInt64(dr["OPEN_COUNT"]),
                    CLOSE_COUNT = Convert.ToInt64(dr["CLOSE_COUNT"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 設計チェック登録
        /// </summary>
        /// <param name="val">設計チェック登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]DesignCheckPostInModel val)
        {
            db.Begin();

            int? newid = null;

            var flg = base.GetLogic().PostData(val, ref newid);

            if (flg == false)
            {
                db.Rollback();

                return Ok(base.GetResponse(flg));
            }

            db.Commit();

            var resurlts = new List<DesignCheckGetOutModel>();

            resurlts.Add(new DesignCheckGetOutModel
            {
                ID = Convert.ToInt32(newid)
            });

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 設計チェック更新
        /// </summary>
        /// <param name="val">設計チェック更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]DesignCheckPutInModel val)
        {
            db.Begin();

            var flg = base.GetLogic().PutData(val);

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
        /// 設計チェック削除
        /// </summary>
        /// <param name="val">設計チェック削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]DesignCheckDeleteInModel val)
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
