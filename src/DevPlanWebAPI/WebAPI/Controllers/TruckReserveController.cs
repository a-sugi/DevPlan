using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using Newtonsoft.Json;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// トラック予約済一覧
    /// </summary>
    public class TruckReserveController : BaseAPIController<TruckReserveLogic, CarShareReservationModel>
    {
        #region トラック予約済一覧取得
        /// <summary>
        /// トラック予約済一覧検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TruckReserveInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(val)));
        }
        #endregion
    }
}