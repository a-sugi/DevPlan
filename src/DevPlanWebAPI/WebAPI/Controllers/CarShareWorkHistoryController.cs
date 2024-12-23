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
    /// 作業履歴(カーシェア)
    /// </summary>
    public class CarShareWorkHistoryController : BaseAPIController<CarShareWorkHistoryLogic, WorkHistoryModel>
    {
        #region 作業履歴更新
        /// <summary>
        /// 作業履歴更新
        /// </summary>
        /// <param name="list">作業履歴</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<WorkHistoryModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateWorkHistory(list)));

        }
        #endregion

    }
}
