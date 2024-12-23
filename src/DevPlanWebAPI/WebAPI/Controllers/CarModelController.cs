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
    /// 車型
    /// </summary>
    public class CarModelController : BaseAPIController<CarModelLogic, CarModelOutModel>
    {
        #region 取得
        /// <summary>
        /// 車型取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarModelInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));

            }

            var list = new List<CarModelOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new CarModelOutModel
                {
                    車型 = Convert.ToString(dr["車型"])
                });
            }

            return Ok(base.GetResponse(list));

        }
        #endregion
    }
}
