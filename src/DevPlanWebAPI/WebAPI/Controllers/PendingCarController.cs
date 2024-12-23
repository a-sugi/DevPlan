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
    /// 処理待ち車両
    /// </summary>
    public class PendingCarController : BaseAPIController<PendingCarLogic, PendingCarModel>
    {
        /// <summary>
        /// 処理待ち車両検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]PendingCarSearchModel val)
        {
            var list = base.GetLogic().GetData(val);

            if (list == null || list.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            return Ok(base.GetResponse(list));
        }
    }
}
