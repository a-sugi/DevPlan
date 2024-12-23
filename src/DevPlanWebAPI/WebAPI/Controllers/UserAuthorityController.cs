using System.Web.Http;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 機能権限検索API
    /// </summary>
    public class UserAuthorityController : BaseAPIController<UserAuthorityLogic, UserAuthorityOutModel>
    {
        /// <summary>
        /// 機能権限取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]UserAuthorityInModel val)
        {
            // 必須パラメータの確認
            if (string.IsNullOrWhiteSpace(val.DEPARTMENT_ID) ||
                string.IsNullOrWhiteSpace(val.SECTION_ID) ||
                string.IsNullOrWhiteSpace(val.SECTION_GROUP_ID) ||
                string.IsNullOrWhiteSpace(val.PERSONEL_ID))
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03020));
            }

            return Ok(base.GetResponse(base.GetLogic().GetData(val)));

        }
    }
}
