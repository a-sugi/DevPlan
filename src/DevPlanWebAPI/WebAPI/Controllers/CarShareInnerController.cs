using System.Web.Http;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// カーシェア内製車
    /// </summary>
    public class CarShareInnerController : BaseAPIController<CarShareInnerLogic, CarShareInnerSearchOutModel>
    {
        #region 取得
        /// <summary>
        /// カーシェア内製車取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarShareInnerSearchInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));
        }
        #endregion
    }
}
