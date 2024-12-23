using System.Web.Http;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// カーシェア外製車
    /// </summary>
    public class CarShareOuterController : BaseAPIController<CarShareOuterLogic, CarShareOuterSearchOutModel>
    {
        #region 取得
        /// <summary>
        /// カーシェア外製車取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarShareOuterSearchInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));
        }
        #endregion
    }
}
