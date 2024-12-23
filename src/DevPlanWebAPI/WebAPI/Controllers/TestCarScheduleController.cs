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
using DevPlanWebAPI.Properties;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 試験車スケジュール
    /// </summary>
    public class TestCarScheduleController : BaseAPIController<TestCarLogic, TestCarScheduleModel>
    {
        #region 試験車スケジュール取得
        /// <summary>
        /// 試験車スケジュール取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarScheduleSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCarSchedule(cond)));

        }
        #endregion

        #region 試験車スケジュール追加
        /// <summary>
        /// 試験車スケジュール追加
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<TestCarScheduleModel> list)
        {
            base.GetLogic().InsertTestCarSchedule(list);
            return Ok(base.GetResponse(list));

        }
        #endregion

        #region 試験車スケジュール更新
        /// <summary>
        /// 試験車スケジュール更新
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<TestCarScheduleModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateTestCarSchedule(list)));

        }
        #endregion

        #region 試験車スケジュール削除
        /// <summary>
        /// 試験車スケジュール削除
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<TestCarScheduleModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteTestCarSchedule(list)));

        }
        #endregion
    }
}