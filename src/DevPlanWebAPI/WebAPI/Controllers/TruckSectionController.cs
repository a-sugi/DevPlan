using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// トラックルートマスタコントローラー
    /// </summary>
    public class TruckSectionController : BaseAPIController<TruckMasterLogic, TruckSectionModel>
    {
        /// <summary>
        /// トラック利用行先取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetTruckSection()));
        }
    }
}
