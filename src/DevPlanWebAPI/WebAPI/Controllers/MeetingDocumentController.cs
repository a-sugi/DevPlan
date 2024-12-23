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
    /// 検討会資料
    /// </summary>
    public class MeetingDocumentController : BaseAPIController<MeetingDocumentLogic, MeetingDocumentModel>
    {
        #region 検討会資料取得
        /// <summary>
        /// 検討会資料取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]MeetingDocumentSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetMeetingDocument(cond)));

        }
        #endregion

        #region 検討会資料追加
        /// <summary>
        /// 検討会資料追加
        /// </summary>
        /// <param name="list">検討会資料</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<MeetingDocumentModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().InsertMeetingDocument(list)));

        }
        #endregion

        #region 検討会資料更新
        /// <summary>
        /// 検討会資料更新
        /// </summary>
        /// <param name="list">検討会資料</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<MeetingDocumentModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateMeetingDocument(list)));

        }
        #endregion

        #region 検討会資料削除
        /// <summary>
        /// 検討会資料削除
        /// </summary>
        /// <param name="list">検討会資料</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<MeetingDocumentModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteMeetingDocument(list)));

        }
        #endregion
    }
}