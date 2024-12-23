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
    /// 設計チェック状況
    /// </summary>
    public class DesignCheckProgressController : BaseAPIController<DesignCheckProgressLogic, DesignCheckProgressGetOutModel>
    {
        /// <summary>
        /// 設計チェック状況検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DesignCheckProgressGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<DesignCheckProgressGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new DesignCheckProgressGetOutModel
                {
                    開催日_ID = Convert.ToInt32(dr["開催日_ID"]),
                    指摘_ID = Convert.ToInt32(dr["指摘_ID"]),
                    状況 = Convert.ToString(dr["状況"]),
                    対象車両_ID = Convert.ToInt32(dr["対象車両_ID"]),
                    試験車_ID = Convert.ToInt32(dr["試験車_ID"]),
                    管理票NO = Convert.ToString(dr["管理票NO"]),
                    試験車名 = Convert.ToString(dr["試験車名"]),
                    グレード = Convert.ToString(dr["グレード"]),
                    試験目的 = Convert.ToString(dr["試験目的"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 設計チェック状況登録
        /// </summary>
        /// <param name="list">設計チェック状況登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]List<DesignCheckProgressPostInModel> list)
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
        /// 設計チェック状況削除
        /// </summary>
        /// <param name="list">設計チェック状況削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]List<DesignCheckProgressDeleteInModel> list)
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
