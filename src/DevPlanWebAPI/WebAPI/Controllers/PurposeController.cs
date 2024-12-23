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
    /// 目的
    /// </summary>
    public class PurposeController : BaseAPIController<PurposeLogic, PurposeSearchOutModel>
    {
        /// <summary>
        /// 目的取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]PurposeSearchInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));

            }

            var list = new List<PurposeSearchOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new PurposeSearchOutModel
                {
                    目的 = Convert.ToString(dr["目的"]),
                    SORT_NO = Convert.ToInt16(dr["SORT_NO"])
                });
            }

            return Ok(base.GetResponse(list));

        }
    }
}
