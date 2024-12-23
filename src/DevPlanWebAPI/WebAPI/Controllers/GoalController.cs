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
    /// 行先
    /// </summary>
    public class GoalController : BaseAPIController<GoalLogic, GoalSearchOutModel>
    {
        /// <summary>
        /// 行先取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]GoalSearchInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));

            }

            var list = new List<GoalSearchOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new GoalSearchOutModel
                {
                    行先 = Convert.ToString(dr["行先"]),
                    SORT_NO = Convert.ToInt16(dr["SORT_NO"])
                });
            }

            return Ok(base.GetResponse(list));

        }
    }
}
