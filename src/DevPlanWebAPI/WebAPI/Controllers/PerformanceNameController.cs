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
    /// 性能名一覧
    /// </summary>
    public class PerformanceNameController : BaseAPIController<PerformanceNameLogic, PerformanceNameOutModel>
    {
        /// <summary>
        /// 性能名取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]PerformanceNameInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            var list = new List<PerformanceNameOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new PerformanceNameOutModel
                {
                    ID = Convert.ToString(dr["ID"]),
                    性能名 = Convert.ToString(dr["性能名"]),
                });
            }

            return Ok(base.GetResponse(list));

        }
    }
}
