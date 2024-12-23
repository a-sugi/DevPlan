using System;
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
    /// 車種別進捗状況一覧項目
    /// </summary>
    public class WorkProgressController : BaseAPIController<WorkProgressLogic, WorkProgressItemModel>
    {
        #region 車種別進捗状況一覧項目取得
        /// <summary>
        /// 車種別進捗状況一覧項目取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]WorkProgressItemSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetWorkProgressItem(cond)));
        }
        #endregion

        #region 車種別進捗状況一覧項目更新
        /// <summary>
        /// 車種別進捗状況一覧項目更新
        /// </summary>
        /// <param name="list">車種別進捗状況一覧項目</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<WorkProgressItemModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateWorkProgressItem(list)));
        }
        #endregion
    }
}