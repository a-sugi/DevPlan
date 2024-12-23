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
    /// 業務スケジュール項目
    /// </summary>
    public class WorkScheduleController : BaseAPIController<WorkScheduleLogic, WorkScheduleGetOutModel>
    {
        /// <summary>
        /// 業務スケジュール項目検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]WorkScheduleGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<WorkScheduleGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new WorkScheduleGetOutModel
                {
                    CATEGORY_ID = Convert.ToInt64(dr["CATEGORY_ID"]),
                    CATEGORY = Convert.ToString(dr["CATEGORY"]),
                    PARALLEL_INDEX_GROUP = Convert.ToInt32(dr["PARALLEL_INDEX_GROUP"]),
                    START_DATE = Convert.ToDateTime(dr["START_DATE"]),
                    END_DATE = Convert.ToDateTime(dr["END_DATE"]),
                    SCHEDULE_ID = Convert.ToInt64(dr["ID"]),
                    DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                    SYMBOL = Convert.ToInt16(dr["SYMBOL"]),
                    END_FLAG = dr["ACHIEVEMENT_INDEX"] == DBNull.Value
                        && dr["ENFORCEMENT_INDEX"] == DBNull.Value ? 0 : 1,
                    INPUT_DATETIME = Convert.ToDateTime(dr["INPUT_DATETIME"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 業務スケジュール項目登録
        /// </summary>
        /// <param name="val">業務計画スケジュール登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]WorkSchedulePostInModel val)
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
        /// 業務スケジュール項目更新
        /// </summary>
        /// <param name="val">業務計画スケジュール登録更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]WorkSchedulePutInModel val)
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
        /// 業務スケジュール項目削除
        /// </summary>
        /// <param name="val">業務計画スケジュール削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]WorkScheduleDeleteInModel val)
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
