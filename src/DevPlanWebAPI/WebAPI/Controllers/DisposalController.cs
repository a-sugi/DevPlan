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
    /// 廃却
    /// </summary>
    public class DisposalController : BaseAPIController<ControlSheetLogic, TestCarCommonModel>
    {
        #region 廃却検索
        /// <summary>
        /// 廃却検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
//        [CommonMethodFilter]
//        public IHttpActionResult Get()
//        public IHttpActionResult Get([FromUri]CapSearchModel cond)
//        {
////            return Ok(base.GetResponse(base.GetLogic().GetTest()));
//            return Ok(base.GetResponse(base.GetLogic().Get(cond)));
//        }
        #endregion

        #region 廃却更新
        /// <summary>
        /// 廃却更新
        /// </summary>
        /// <param name="list">CAP課題項目</param>
        /// <returns>IHttpActionResult</returns>
//        [CommonMethodFilter]
//        public IHttpActionResult Post(ControlSheetModel cond)
//        public IHttpActionResult Put(List<TestCarCommonModel> list)
//        {
////            return Ok(base.GetResponse(base.GetLogic().GetTestCar(cond)));
//            return Ok(base.GetResponse(base.GetLogic().Put(list)));
//        }
        #endregion

    }
}
