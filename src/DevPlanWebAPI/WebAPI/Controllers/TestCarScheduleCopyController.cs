using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 試験車スケジュール要望案コピー
    /// </summary>
    public class TestCarScheduleCopyController : BaseAPIController<TestCarLogic, TestCarScheduleModel>
    {
        /// <summary>
        /// 試験車スケジュール要望案コピー登録
        /// </summary>
        /// <param name="cond">コピー条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]TestCarScheduleCopyModel cond)
        {
            //スケジュールコピー
            var msg = base.GetLogic().CopyTestCarSchedule(cond);
            return Ok(base.GetResponse((msg == MessageType.None), msg));

        }
    }
}
