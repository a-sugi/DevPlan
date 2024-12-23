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
    /// 情報元一覧コントローラー
    /// </summary>
    public class ReportMaterialController : BaseAPIController<ReportMaterialLogic, InfoListOutModel>
    {
        /// <summary>
        /// 情報元一覧取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]InfoListInModel val)
        {
            var results = new List<InfoListOutModel>();

            //検索実行
            var dt = base.GetLogic().Get(val);

            if (dt == null || dt.Rows.Count == 0)
            {
                return Ok(base.GetResponse(results));
            }

            //検索結果をListに設定
            if (dt.Rows[0].Table.Columns.Contains("WEEKEND_DATE"))
            {
                //週報
                foreach (DataRow dr in dt.Rows)
                {
                    results.Add(new InfoListOutModel
                    {
                        LISTED_DATE = Convert.ToDateTime(dr["WEEKEND_DATE"]),
                        SECTION_GROUP_CODE = Convert.ToString(dr["SECTION_GROUP_CODE"]),
                        GENERAL_CODE = Convert.ToString(dr["GENERAL_CODE"]),
                        CATEGORY = Convert.ToString(dr["CATEGORY"]),
                        CURRENT_SITUATION = Convert.ToString(dr["CURRENT_SITUATION"]),
                        FUTURE_SCHEDULE = Convert.ToString(dr["FUTURE_SCHEDULE"]),
                    });
                }
            }
            else if (dt.Rows[0].Table.Columns.Contains("LISTED_DATE"))
            {
                //フォローリスト
                foreach (DataRow dr in dt.Rows)
                {
                    results.Add(new InfoListOutModel
                    {
                        LISTED_DATE = Convert.ToDateTime(dr["LISTED_DATE"]),
                        SECTION_GROUP_CODE = Convert.ToString(dr["SECTION_GROUP_CODE"]),
                        GENERAL_CODE = Convert.ToString(dr["GENERAL_CODE"]),
                        CATEGORY = Convert.ToString(dr["CATEGORY"]),
                        CURRENT_SITUATION = Convert.ToString(dr["CURRENT_SITUATION"]),
                        FUTURE_SCHEDULE = Convert.ToString(dr["FUTURE_SCHEDULE"]),
                        INPUT_DATETIME = Convert.ToString(dr["INPUT_DATETIME"]),
                        OPEN_CLOSE = Convert.ToString(dr["OPEN_CLOSE"]),
                        PERSONEL_NAME = Convert.ToString(dr["PERSONEL_NAME"]),
                        SELECT_KEYWORD = Convert.ToString(dr["SELECT_KEYWORD"])
                    });
                }
            }
            else if (dt.Rows[0].Table.Columns.Contains("MONTH_FIRST_DAY"))
            {
                //月報
                foreach (DataRow dr in dt.Rows)
                {
                    results.Add(new InfoListOutModel
                    {
                        LISTED_DATE = Convert.ToDateTime(dr["MONTH_FIRST_DAY"]),
                        CATEGORY = Convert.ToString(dr["GENERAL_CODE"]),
                        CURRENT_SITUATION = Convert.ToString(dr["CURRENT_SITUATION"]),
                        FUTURE_SCHEDULE = Convert.ToString(dr["FUTURE_SCHEDULE"]),
                        SECTION_CODE = Convert.ToString(dr["SECTION_CODE"]),
                    });
                }
            }

            return Ok(base.GetResponse(results));
        }
    }
}
