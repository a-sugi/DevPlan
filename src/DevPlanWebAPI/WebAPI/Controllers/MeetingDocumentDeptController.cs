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
    /// 検討会資料部記号
    /// </summary>
    public class MeetingDocumentDeptController : BaseAPIController<MeetingDocumentDeptLogic, MeetingDocumentDeptModel>
    {
        #region 検討会資料部記号取得
        /// <summary>
        /// 検討会資料部記号取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]MeetingDocumentDeptSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetMeetingDocumentDept(cond)));

        }
        #endregion

    }
}