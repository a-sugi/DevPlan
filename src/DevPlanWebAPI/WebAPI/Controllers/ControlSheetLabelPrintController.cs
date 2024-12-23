using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 管理票ラベル印刷
    /// </summary>
    public class ControlSheetLabelPrintController : BaseAPIController<ControlSheetLabelPrintLogic, TestCarCommonModel>
    {
        /// <summary>
        /// 管理票ラベル印刷検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarCommonSearchModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(val)));
        }
        
        /// <summary>
                 /// 管理票ラベル印刷更新
                 /// </summary>
                 /// <param name="list">試験車共通モデル</param>
                 /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<TestCarCommonModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().Put(list)));
        }
    }
}
