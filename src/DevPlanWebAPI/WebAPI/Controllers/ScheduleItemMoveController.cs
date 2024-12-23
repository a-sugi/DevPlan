using System;
using System.Web.Http;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 項目移動
    /// </summary>
    public class ScheduleItemMoveController : BaseAPIController<ScheduleItemMoveLogic, bool>
    {
        /// <summary>
        /// 項目移動更新
        /// </summary>
        /// <param name="model">項目コピー・移動（項目移動）モデルクラス（入力）</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]ScheduleItemMoveInModel model)
        {
            return Ok(base.GetResponse(base.GetLogic().ScheculeItemMove(model)));
        }
    }
}
