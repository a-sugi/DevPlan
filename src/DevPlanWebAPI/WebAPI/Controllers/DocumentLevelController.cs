﻿using System;
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
    /// 資料評価レベル検索
    /// </summary>
    public class DocumentLevelController : BaseAPIController<DocumentLevelLogic, object>
    {
        /// <summary>
        /// 資料評価レベル検索
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().Get()));
        }
    }
}