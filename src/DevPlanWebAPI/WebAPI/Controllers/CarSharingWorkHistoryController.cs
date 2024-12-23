﻿using System;
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
    /// 作業履歴(試験車)
    /// </summary>
    public class CarSharingWorkHistoryController : BaseAPIController<CarSharingWorkHistoryLogic, WorkCarSharingHistoryModel>
    {
        #region 作業履歴取得
        /// <summary>
        /// 作業履歴取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarSharingWorkHistorySearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetCarSharingWorkHistory(cond)));

        }
        #endregion

        #region 作業履歴(試験車)
        /// <summary>
        /// 作業履歴(試験車)
        /// </summary>
        /// <param name="list">作業履歴</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<WorkCarSharingHistoryModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateWorkHistory(list)));

        }
        #endregion

    }
}