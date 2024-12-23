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
    /// 試験車（試験車管理）
    /// </summary>
    public class DesignCheckSystemTestCarController : BaseAPIController<DesignCheckSystemTestCarLogic, TestCarCommonModel>
    {
        #region 取得
        /// <summary>
        /// 試験車（試験車管理）取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DesignCheckTestCarGetInModel cond)
        {
            var list = base.GetLogic().GetData(cond);

            if (list == null || list.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            return Ok(base.GetResponse(list));
        }
        #endregion
    }
}
