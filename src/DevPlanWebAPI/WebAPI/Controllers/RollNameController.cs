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
    /// ロール名
    /// </summary>
    public class RollNameController : BaseAPIController<RollNameLogic, RollModel>
    {
        /// <summary>
        /// ロール名取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]RollGetInModel val)
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
