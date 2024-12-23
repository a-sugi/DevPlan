using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using Newtonsoft.Json;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// スケジュール利用車詳細
    /// </summary>
    public class ScheduleCarDetailController : BaseAPIController<ScheduleCarDetailLogic, ScheduleCarDetailModel>
    {
        #region スケジュール利用車詳細の取得
        /// <summary>
        /// スケジュール利用車詳細の取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ScheduleCarDetailSearchModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(val)));
        }
        #endregion

        #region スケジュール利用車詳細の登録
        /// <summary>
        /// スケジュール利用車詳細の登録
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(ScheduleCarDetailModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().Post(val)));
        }
        #endregion

        #region スケジュール利用車詳細の移譲
        /// <summary>
        /// スケジュール利用車詳細の移譲
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(ScheduleCarDetailModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().Put(val)));
        }
        #endregion
    }
}