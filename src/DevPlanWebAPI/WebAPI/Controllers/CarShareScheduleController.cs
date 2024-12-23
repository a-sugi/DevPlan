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
    /// カーシェアスケジュール
    /// </summary>
    public class CarShareScheduleController : BaseAPIController<CarShareLogic, CarShareScheduleModel>
    {
        #region カーシェアスケジュール取得
        /// <summary>
        /// カーシェアスケジュール取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarShareScheduleSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetCarShareSchedule(cond)));

        }
        #endregion

        #region カーシェアスケジュール追加
        /// <summary>
        /// カーシェアスケジュール追加
        /// </summary>
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<CarShareScheduleModel> list)
        {
            base.GetLogic().InsertCarShareSchedule(list);
            return Ok(base.GetResponse(list));

        }
        #endregion

        #region カーシェアスケジュール更新
        /// <summary>
        /// カーシェアスケジュール更新
        /// </summary>
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<CarShareScheduleModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateCarShareSchedule(list)));

        }
        #endregion

        #region カーシェアスケジュール削除
        /// <summary>
        /// カーシェアスケジュール削除
        /// </summary>
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<CarShareScheduleModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteCarShareSchedule(list)));

        }
        #endregion
    }
}