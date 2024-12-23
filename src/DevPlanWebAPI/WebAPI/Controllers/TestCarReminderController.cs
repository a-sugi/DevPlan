using System.Collections.Generic;
using System.Web.Http;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 試験車注意喚起
    /// </summary>
    public class TestCarReminderController : BaseAPIController<TestCarReminderLogic, TestCarReminderSearchOutModel>
    {
        /// <summary>
        /// 試験車注意喚起取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarReminderSearchInModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(cond)));

        }

        /// <summary>
        /// 試験車注意喚起更新
        /// </summary>
        /// <param name="list">注意喚起</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<TestCarReminderSearchOutModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().PutData(list)));

        }

    }
}
