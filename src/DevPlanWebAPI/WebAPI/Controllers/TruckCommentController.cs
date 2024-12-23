using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// トラック予約コメントマスタコントローラー
    /// </summary>
    public class TruckCommentController : BaseAPIController<TruckMasterLogic, TruckCommentModel>
    {
        /// <summary>
        /// トラック予約定期便時間帯マスタ取得
        /// </summary>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetTruckComment()));
        }
    }
}
