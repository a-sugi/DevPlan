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
    /// 状況記号（設計チェック）
    /// </summary>
    public class DesignCheckProgressSymbolController : BaseAPIController<DesignCheckProgressSymbolLogic, DesignCheckProgressSymbolGetOutModel>
    {
        /// <summary>
        /// 状況記号（設計チェック）検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DesignCheckProgressSymbolGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<DesignCheckProgressSymbolGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new DesignCheckProgressSymbolGetOutModel
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    記号 = Convert.ToString(dr["記号"]),
                    説明 = Convert.ToString(dr["説明"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }
    }
}
