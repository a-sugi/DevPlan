using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// よく使う目的地コントローラー
    /// </summary>
    public class FrequentlyUsedDestinationsController : BaseAPIController<TruckMasterLogic, FrequentlyUsedDestinationsModel>
    {
        /// <summary>
        /// よく使う目的地取得
        /// </summary>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetFrequentlyUsedDestinationsList()));
        }
    }   
}
