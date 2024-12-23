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
    public class CarShareScheduleFromListController : BaseAPIController<CarShareLogic, CarShareScheduleModel>
    {
        #region カーシェアスケジュール複数取得
        /// <summary>
        /// カーシェアスケジュール複数取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarShareScheduleSearchListModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetCarShareScheduleList(cond)));

        }
        #endregion
    }
}