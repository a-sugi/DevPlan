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
    /// トランスミッション
    /// </summary>
    public class TransmissionController : BaseAPIController<TransmissionLogic, TransmissionSearchOutModel>
    {
        /// <summary>
        /// トランスミッション取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TransmissionSearchInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));

            }

            var list = new List<TransmissionSearchOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new TransmissionSearchOutModel
                {
                    トランスミッション = Convert.ToString(dr["トランスミッション"])
                });
            }

            return Ok(base.GetResponse(list));

        }
    }
}
