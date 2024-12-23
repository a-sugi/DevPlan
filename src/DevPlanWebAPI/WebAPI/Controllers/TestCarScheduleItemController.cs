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
    /// 試験車スケジュール項目
    /// </summary>
    public class TestCarScheduleItemController : BaseAPIController<TestCarLogic, TestCarScheduleItemModel>
    {
        #region 試験車スケジュール項目取得
        /// <summary>
        /// 試験車スケジュール項目取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarScheduleItemSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCarScheduleItem(cond)));

        }
        #endregion

        #region 試験車スケジュール項目追加
        /// <summary>
        /// 試験車スケジュール項目追加
        /// </summary>
        /// <param name="list">試験車スケジュール項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<TestCarScheduleItemModel> list)
        {
            var flg = base.GetLogic().InsertTestCarScheduleItem(list);
            return Ok(base.GetResponse(list));
        }
        #endregion

        #region 試験車スケジュール項目更新
        /// <summary>
        /// 試験車スケジュール項目更新
        /// </summary>
        /// <param name="list">試験車スケジュール項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<TestCarScheduleItemModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateTestCarScheduleItem(list)));

        }
        #endregion

        #region 試験車スケジュール項目削除
        /// <summary>
        /// 試験車スケジュール項目削除
        /// </summary>
        /// <param name="list">試験車スケジュール項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<TestCarScheduleItemModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteTestCarScheduleItem(list)));

        }
        #endregion
    }
}