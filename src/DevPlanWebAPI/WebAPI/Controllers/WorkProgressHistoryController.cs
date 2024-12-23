using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 進捗履歴
    /// </summary>
    public class WorkProgressHistoryController : BaseAPIController<WorkProgressHistoryLogic, WorkHistoryModel>
    {
        #region 進捗履歴更新
        /// <summary>
        /// 進捗履歴更新
        /// </summary>
        /// <param name="list">進捗履歴</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<WorkHistoryModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateWorkHistory(list)));

        }
        #endregion

    }
}
