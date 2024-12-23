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
    /// 月次計画承認
    /// </summary>
    public class MonthlyWorkApprovalController : BaseAPIController<MonthlyWorkApprovalLogic, MonthlyWorkApprovalGetOutModel>
    {
        /// <summary>
        /// 月次計画承認検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]MonthlyWorkApprovalGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<MonthlyWorkApprovalGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new MonthlyWorkApprovalGetOutModel
                {
                    SECTION_GROUP_ID = Convert.ToString(dr["SECTION_GROUP_ID"]),
                    対象月 = Convert.ToDateTime(dr["対象月"]),
                    FLAG_月頭月末 = Convert.ToInt16(dr["FLAG_月頭月末"]),
                    FLAG_承認 = Convert.ToInt16(dr["FLAG_承認"]),
                    承認日時 = Convert.ToDateTime(dr["承認日時"]),
                    承認者_PERSONEL_ID = Convert.ToString(dr["承認者_PERSONEL_ID"]),
                    承認者_NAME = Convert.ToString(dr["承認者_NAME"]),
                    承認者_SECTION_CODE = Convert.ToString(dr["承認者_SECTION_CODE"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 月次計画承認登録
        /// </summary>
        /// <param name="val">月次計画承認登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]MonthlyWorkApprovalPostInModel val)
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
    }
}
