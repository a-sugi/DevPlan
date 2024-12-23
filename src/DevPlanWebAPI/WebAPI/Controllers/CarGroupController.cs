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
    /// 車系
    /// </summary>
    public class CarGroupController : BaseAPIController<CarGroupLogic, CarGroupSearchOutModel>
    {
        #region 取得
        /// <summary>
        /// 車系取得
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarGroupSearchInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            var resurlts = new List<CarGroupSearchOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new CarGroupSearchOutModel
                {
                    CAR_GROUP = Convert.ToString(dr["CAR_GROUP"]),
                    SORT_NUMBER = Convert.ToInt32(dr["NUM"]),
                    PERMIT_FLG = Convert.ToInt32(dr["PERMIT_FLG"])
                });
            }

            return Ok(base.GetResponse(resurlts));

        }
        #endregion

    }
}
