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
    /// 車両管理
    /// </summary>
    public class CarManagementController : BaseAPIController<CarManagementLogic, TestCarCommonModel>
    {
        #region 管理車両登録
        /// <summary>
        /// 管理車両登録
        /// </summary>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]List<ProductionCarCommonModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().Post(list)));
        }
        #endregion
    }
}
