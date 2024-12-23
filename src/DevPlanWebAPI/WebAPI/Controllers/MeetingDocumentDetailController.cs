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
    /// 検討会資料詳細
    /// </summary>
    public class MeetingDocumentDetailController : BaseAPIController<MeetingDocumentLogic, MeetingDocumentDetailModel>
    {
        #region 検討会資料詳細取得
        /// <summary>
        /// 検討会資料詳細取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]MeetingDocumentDetailSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetMeetingDocumentDetail(cond)));

        }
        #endregion

        #region 検討会資料詳細更新
        /// <summary>
        /// 検討会資料詳細更新
        /// </summary>
        /// <param name="list">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<MeetingDocumentDetailModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateMeetingDocumentDetail(list)));

        }
        #endregion

    }
}