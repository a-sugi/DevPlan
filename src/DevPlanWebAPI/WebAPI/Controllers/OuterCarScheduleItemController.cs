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
    /// 外製車予約スケジュール項目
    /// </summary>
    public class OuterCarScheduleItemController : BaseAPIController<OuterCarScheduleItemLogic, OuterCarScheduleItemGetOutModel>
    {
        /// <summary>
        /// 外製車予約スケジュール項目検索
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]OuterCarScheduleItemGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<OuterCarScheduleItemGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new OuterCarScheduleItemGetOutModel
                {
                    GENERAL_CODE = Convert.ToString(dr["GENERAL_CODE"]),
                    CAR_GROUP = Convert.ToString(dr["CAR_GROUP"]),
                    CATEGORY = Convert.ToString(dr["CATEGORY"]),
                    SORT_NO = Convert.ToDouble(dr["SORT_NO"]),
                    PARALLEL_INDEX_GROUP = Convert.ToInt32(dr["PARALLEL_INDEX_GROUP"]),
                    SCHEDULE_ID = Convert.ToInt64(dr["ID"]),
                    CATEGORY_ID = Convert.ToInt64(dr["CATEGORY_ID"]),
                    CLOSED_DATE = dr["CLOSED_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CLOSED_DATE"]),
                    FLAG_要予約許可 = Convert.ToInt16(dr["FLAG_要予約許可"]),
                    最終予約可能日 = dr["最終予約可能日"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["最終予約可能日"]),
                    メーカー名 = Convert.ToString(dr["メーカー名"]),
                    外製車名 = Convert.ToString(dr["外製車名"]),
                    登録ナンバー = Convert.ToString(dr["登録ナンバー"]),
                    FLAG_ナビ付 = Convert.ToInt16(dr["FLAG_ナビ付"]),
                    FLAG_ETC付 = Convert.ToInt16(dr["FLAG_ETC付"]),
                    管理票NO = dr["管理票NO"] == DBNull.Value ? null : Convert.ToString(dr["管理票NO"]),
                    駐車場番号 = dr["駐車場番号"] == DBNull.Value ? null : Convert.ToString(dr["駐車場番号"]),
                    XEYE_EXIST = dr["XEYE_EXIST"] == DBNull.Value ? null: Convert.ToString(dr["XEYE_EXIST"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 外製車予約スケジュール項目登録
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]OuterCarScheduleItemPostInModel val)
        {
            db.Begin();

            var ret = new OuterCarScheduleItemGetOutModel();
            var flg = base.GetLogic().PostData(val, ret);

            if (flg == false)
            {
                db.Rollback();
            }
            else
            {
                db.Commit();
            }

            return Ok(base.GetResponse(new List<OuterCarScheduleItemGetOutModel>() { ret }));
        }

        /// <summary>
        /// 外製車予約スケジュール項目更新
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]OuterCarScheduleItemPutInModel val)
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
        /// 外製車予約スケジュール項目削除
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]OuterCarScheduleItemDeleteInModel val)
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
