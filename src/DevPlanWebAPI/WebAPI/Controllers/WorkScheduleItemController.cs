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
    /// 業務スケジュール
    /// </summary>
    public class WorkScheduleItemController : BaseAPIController<WorkScheduleItemLogic, WorkScheduleItemGetOutModel>
    {
        /// <summary>
        /// 業務スケジュール検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]WorkScheduleItemGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<WorkScheduleItemGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new WorkScheduleItemGetOutModel
                {
                    CATEGORY_ID = Convert.ToInt64(dr["CATEGORY_ID"]),
                    CATEGORY = Convert.ToString(dr["CATEGORY"]),
                    GENERAL_CODE = Convert.ToString(dr["GENERAL_CODE"]),
                    SECTION_GROUP_ID = Convert.ToString(dr["SECTION_GROUP_ID"]),
                    SECTION_GROUP_CODE = Convert.ToString(dr["SECTION_GROUP_CODE"]),
                    SORT_NO = Convert.ToDouble(dr["SORT_NO"]),
                    PARALLEL_INDEX_GROUP = Convert.ToInt32(dr["PARALLEL_INDEX_GROUP"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 業務スケジュール登録
        /// </summary>
        /// <param name="val">業務計画スケジュール項目登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]WorkScheduleItemPostInModel val)
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
        /// 業務スケジュール更新
        /// </summary>
        /// <param name="val">業務計画スケジュール項目更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]WorkScheduleItemPutInModel val)
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
        /// 業務スケジュール削除
        /// </summary>
        /// <param name="val">業務計画スケジュール項目削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]WorkScheduleItemDeleteInModel val)
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
