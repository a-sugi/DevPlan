using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// トラック管理者情報
    /// </summary>
    public class TruckManagementUserController : BaseAPIController<TruckUserLogic, TruckManagementUserModel>
    {
        /// <summary>
        /// トラック管理者情報取得
        /// </summary>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetTruckManagementUser()));
        }

        /// <summary>
        /// トラック管理者情報更新
        /// </summary>
        /// <param name="model">更新情報</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(TruckManagementUserModel model)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateTruckManagementUser(model)));
        }
    }
}
