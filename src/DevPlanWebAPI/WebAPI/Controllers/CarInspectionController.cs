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
    /// 車検・点検リスト
    /// </summary>
    public class CarInspectionController : BaseAPIController<CarInspectionLogic, TestCarCommonModel>
    {
        #region 取得
        /// <summary>
        /// 車検・点検リスト検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(CarInspectionSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTestCar(cond)));

        }
        #endregion

    }
}
