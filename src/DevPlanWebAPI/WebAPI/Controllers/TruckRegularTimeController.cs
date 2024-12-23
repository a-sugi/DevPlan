using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// トラック予約定期便時間 
    /// </summary>
    public class TruckRegularTimeController : BaseAPIController<TruckMasterLogic, TruckRegularTimeModel>
    {
        /// <summary>
        /// トラック予約定期便時間帯マスタ取得
        /// </summary>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetTruckRegularTimeMst()));
        }
    }
}
