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
    /// 仕向地
    /// </summary>
    public class DestinationController : BaseAPIController<DestinationLogic, DestinationOutModel>
    {
        #region 取得
        /// <summary>
        /// 仕向地取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DestinationInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));

            }

            var list = new List<DestinationOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new DestinationOutModel
                {
                    仕向地 = Convert.ToString(dr["仕向地"])
                });
            }

            return Ok(base.GetResponse(list));

        }
        #endregion
    }
}
