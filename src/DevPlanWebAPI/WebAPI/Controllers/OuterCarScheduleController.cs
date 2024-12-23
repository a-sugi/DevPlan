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
    /// 外製車予約スケジュール
    /// </summary>
    public class OuterCarScheduleController : BaseAPIController<OuterCarScheduleLogic, OuterCarScheduleGetOutModel>
    {
        /// <summary>
        /// 外製車予約スケジュール検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]OuterCarScheduleGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<OuterCarScheduleGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new OuterCarScheduleGetOutModel
                {
                    GENERAL_CODE = Convert.ToString(dr["GENERAL_CODE"]),
                    CATEGORY = Convert.ToString(dr["CATEGORY"]),
                    PARALLEL_INDEX_GROUP = Convert.ToInt32(dr["PARALLEL_INDEX_GROUP"]),
                    START_DATE = Convert.ToDateTime(dr["START_DATE"]),
                    END_DATE = Convert.ToDateTime(dr["END_DATE"]),
                    SCHEDULE_ID = Convert.ToInt64(dr["ID"]),
                    CATEGORY_ID = Convert.ToInt64(dr["CATEGORY_ID"]),
                    DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                    SYMBOL = Convert.ToInt16(dr["SYMBOL"]),
                    予約種別 = Convert.ToString(dr["予約種別"]),
                    INPUT_DATETIME = Convert.ToDateTime(dr["INPUT_DATETIME"]),
                    目的 = dr["目的"] == DBNull.Value ? null: Convert.ToString(dr["目的"]),
                    行先 = dr["行先"] == DBNull.Value ? null : Convert.ToString(dr["行先"]),
                    TEL = dr["TEL"] == DBNull.Value ? null : Convert.ToString(dr["TEL"]),
                    FLAG_実使用 = Convert.ToInt16(dr["FLAG_実使用"]),
                    予約者_ID = dr["予約者_ID"] == DBNull.Value ? null : Convert.ToString(dr["予約者_ID"]),
                    予約者_SECTION_CODE = dr["予約者_SECTION_CODE"] == DBNull.Value ? null : Convert.ToString(dr["予約者_SECTION_CODE"]),
                    予約者_NAME = dr["予約者_NAME"] == DBNull.Value ? null : Convert.ToString(dr["予約者_NAME"]),
                    管理票番号 = dr["管理票番号"] == DBNull.Value ? null : Convert.ToString(dr["管理票番号"]),
                    駐車場番号 = dr["駐車場番号"] == DBNull.Value ? null : Convert.ToString(dr["駐車場番号"]),
                    FLAG_空時間貸出可 = Convert.ToInt16(dr["FLAG_空時間貸出可"]),
                    FLAG_要予約許可 = dr["FLAG_要予約許可"] == DBNull.Value ? null : (short?)dr["FLAG_要予約許可"]
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 外製車予約スケジュール登録
        /// </summary>
        /// <param name="val">外製車日程項目登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]OuterCarSchedulePostInModel val)
        {
            db.Begin();

            OuterCarScheduleGetOutModel retModel = new OuterCarScheduleGetOutModel();
            var flg = base.GetLogic().PostData(val, ref retModel);

            if (flg == false)
            {
                db.Rollback();
            }
            else
            {
                db.Commit();
            }

            return Ok(base.GetResponse(new List<OuterCarScheduleGetOutModel>() { retModel }));
        }

        /// <summary>
        /// 外製車予約スケジュール更新
        /// </summary>
        /// <param name="val">外製車日程項目更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]OuterCarSchedulePutInModel val)
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
        /// 外製車予約スケジュール削除
        /// </summary>
        /// <param name="val">外製車日程項目削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]OuterCarScheduleDeleteInModel val)
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
