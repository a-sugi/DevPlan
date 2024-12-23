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
    /// カーシェアスケジュール項目
    /// </summary>
    public class CarShareScheduleItemController : BaseAPIController<CarShareLogic, CarShareScheduleItemModel>
    {
        #region カーシェアスケジュール項目取得
        /// <summary>
        /// カーシェアスケジュール項目取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarShareScheduleItemSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetCarShareScheduleItem(cond)));

        }
        #endregion

        #region カーシェアスケジュール項目追加
        /// <summary>
        /// カーシェアスケジュール項目追加
        /// </summary>
        /// <param name="list">カーシェアスケジュール項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<CarShareScheduleItemModel> list)
        {
            var flg = base.GetLogic().InsertCarShareScheduleItem(list);
            return Ok(base.GetResponse(list));

        }
        #endregion

        #region カーシェアスケジュール項目更新
        /// <summary>
        /// カーシェアスケジュール項目更新
        /// </summary>
        /// <param name="list">カーシェアスケジュール項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<CarShareScheduleItemModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateCarShareScheduleItem(list)));

        }
        #endregion

        #region カーシェアスケジュール項目削除
        /// <summary>
        /// カーシェアスケジュール項目削除
        /// </summary>
        /// <param name="list">カーシェアスケジュール項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<CarShareScheduleItemModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteCarShareScheduleItem(list)));

        }
        #endregion
    }
}