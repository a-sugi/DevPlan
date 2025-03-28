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
    /// 種別
    /// </summary>
    public class ClassController : BaseAPIController<ClassLogic, ClassSearchOutModel>
    {
        #region 取得
        /// <summary>
        /// 種別取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ClassSearchInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));

        }
        #endregion
    }
}
