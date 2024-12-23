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
    /// 設計チェック参加者
    /// </summary>
    public class DesignCheckUserController : BaseAPIController<DesignCheckUserLogic, DesignCheckUserGetOutModel>
    {
        /// <summary>
        /// 設計チェック参加者検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DesignCheckUserGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<DesignCheckUserGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new DesignCheckUserGetOutModel
                {
                    ID = Convert.ToInt64(dr["ID"]),
                    開催日_ID = Convert.ToInt32(dr["開催日_ID"]),
                    PERSONEL_ID = Convert.ToString(dr["PERSONEL_ID"]),
                    NAME = Convert.ToString(dr["NAME"]),
                    DEPARTMENT_ID = Convert.ToString(dr["DEPARTMENT_ID"]),
                    DEPARTMENT_CODE = Convert.ToString(dr["DEPARTMENT_CODE"]),
                    SECTION_ID = Convert.ToString(dr["SECTION_ID"]),
                    SECTION_CODE = Convert.ToString(dr["SECTION_CODE"]),
                    SECTION_GROUP_ID = Convert.ToString(dr["SECTION_GROUP_ID"]),
                    SECTION_GROUP_CODE = Convert.ToString(dr["SECTION_GROUP_CODE"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 設計チェック参加者登録
        /// </summary>
        /// <param name="list">設計チェック参加者登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]List<DesignCheckUserPostInModel> list)
        {
            db.Begin();

            var flg = base.GetLogic().PostData(list);

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
        /// 設計チェック参加者削除
        /// </summary>
        /// <param name="list">設計チェック参加者削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]List<DesignCheckUserDeleteInModel> list)
        {
            db.Begin();

            var flg = base.GetLogic().DeleteData(list);

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
