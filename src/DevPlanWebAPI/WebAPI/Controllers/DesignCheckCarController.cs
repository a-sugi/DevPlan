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
    /// 設計チェック対象車
    /// </summary>
    public class DesignCheckCarController : BaseAPIController<DesignCheckCarLogic, DesignCheckCarGetOutModel>
    {
        /// <summary>
        /// 設計チェック対象車検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DesignCheckCarGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<DesignCheckCarGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new DesignCheckCarGetOutModel
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    開催日_ID = Convert.ToInt32(dr["開催日_ID"]),
                    試験車_ID = Convert.ToInt32(dr["試験車_ID"]),
                    管理票NO = Convert.ToString(dr["管理票NO"]),
                    試験車名 = Convert.ToString(dr["試験車名"]),
                    グレード = Convert.ToString(dr["グレード"]),
                    試験目的 = Convert.ToString(dr["試験目的"]),
                    開発符号 = !string.IsNullOrWhiteSpace(dr["開発符号"]?.ToString()) ? Convert.ToString(dr["開発符号"]) : parseTestCarName(0, dr["試験車名"]?.ToString()),
                    試作時期 = !string.IsNullOrWhiteSpace(dr["試作時期"]?.ToString()) ? Convert.ToString(dr["試作時期"]) : parseTestCarName(1, dr["試験車名"]?.ToString()),
                    号車 = !string.IsNullOrWhiteSpace(dr["号車"]?.ToString()) ? Convert.ToString(dr["号車"]) : parseTestCarName(2, dr["試験車名"]?.ToString()),
                    排気量 = Convert.ToString(dr["排気量"]),
                    E_G型式 = Convert.ToString(dr["E_G型式"]),
                    駆動方式 = Convert.ToString(dr["駆動方式"]),
                    トランスミッション = Convert.ToString(dr["トランスミッション"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 設計チェック対象車登録
        /// </summary>
        /// <param name="list">設計チェック対象車登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]List<DesignCheckCarPostInModel> list)
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
        /// 設計チェック対象車削除
        /// </summary>
        /// <param name="list">設計チェック対象車削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]List<DesignCheckCarDeleteInModel> list)
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

        private string parseTestCarName(int index, string testcarname)
        {
            var list = testcarname.Split(' ');

            return list.Length > index ? list[index] : string.Empty;
        }
    }
}
