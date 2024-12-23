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
    /// 業務（月次計画）スケジュール項目
    /// </summary>
    public class MonthlyWorkScheduleItemController : BaseAPIController<MonthlyWorkScheduleItemLogic, MonthlyWorkScheduleItemGetOutModel>
    {
        /// <summary>
        /// 業務（月次計画）スケジュール項目検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]MonthlyWorkScheduleItemGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<MonthlyWorkScheduleItemGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new MonthlyWorkScheduleItemGetOutModel
                {
                    CATEGORY_ID = Convert.ToInt64(dr["CATEGORY_ID"]),
                    DEV_CATEGORY_ID = dr["DEV_CATEGORY_ID"] != DBNull.Value 
                        ? Convert.ToInt64(dr["DEV_CATEGORY_ID"]) : (long?)null,
                    CATEGORY = Convert.ToString(dr["CATEGORY"]),
                    GENERAL_CODE = Convert.ToString(dr["GENERAL_CODE"]),
                    SECTION_GROUP_ID = Convert.ToString(dr["SECTION_GROUP_ID"]),
                    SECTION_GROUP_CODE = Convert.ToString(dr["SECTION_GROUP_CODE"]),
                    担当者 = Convert.ToString(dr["担当者"]),
                    備考 = Convert.ToString(dr["備考"]),
                    対象月 = Convert.ToDateTime(dr["対象月"]),
                    FLAG_月頭月末 = Convert.ToInt16(dr["FLAG_月頭月末"]),
                    FLAG_月報専用項目 = Convert.ToInt16(dr["FLAG_月報専用項目"]),
                    SORT_NO = dr["SORT_NO"] != DBNull.Value ? Convert.ToDouble(dr["SORT_NO"]) : (double?)0,
                    PARALLEL_INDEX_GROUP = Convert.ToInt32(dr["PARALLEL_INDEX_GROUP"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 業務（月次計画）スケジュール項目登録
        /// </summary>
        /// <param name="val">月次計画項目登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]MonthlyWorkScheduleItemPostInModel val)
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
        /// 業務（月次計画）スケジュール項目更新
        /// </summary>
        /// <param name="val">月次計画項目更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]MonthlyWorkScheduleItemPutInModel val)
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
        /// 業務（月次計画）スケジュール項目削除
        /// </summary>
        /// <param name="val">月次計画項目削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]MonthlyWorkScheduleItemDeleteInModel val)
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
