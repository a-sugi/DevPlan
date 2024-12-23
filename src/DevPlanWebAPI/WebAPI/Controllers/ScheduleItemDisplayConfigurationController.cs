using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;

using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// ユーザー表示設定情報
    /// </summary>
    public class ScheduleItemDisplayConfigurationController : BaseAPIController<ScheduleItemDisplayConfigurationLogic, ScheduleItemDisplayConfigurationModel>
    {
        #region 取得
        /// <summary>
        /// ユーザー表示設定情報取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ScheduleItemDisplayConfigurationSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(cond)));

        }
        #endregion

        #region 登録
        /// <summary>
        /// ユーザー表示設定情報登録
        /// </summary>
        /// <param name="list">ユーザー表示設定</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(IEnumerable<ScheduleItemDisplayConfigurationModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().MergeData(list)));

        }
        #endregion

        #region 削除
        /// <summary>
        /// ユーザー表示設定情報削除
        /// </summary>
        /// <param name="list">ユーザー表示設定</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(IEnumerable<ScheduleItemDisplayConfigurationModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteData(list)));

        }
        #endregion

    }
}
