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
    /// PC端末権限
    /// </summary>
    public class PCAuthorityController : BaseAPIController<PCAuthorityLogic, PCAuthorityGetOutModel>
    {
        /// <summary>
        /// PC端末権限取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]PCAuthorityGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<PCAuthorityGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new PCAuthorityGetOutModel
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    PC_NAME = Convert.ToString(dr["PC_NAME"]),
                    FUNCTION_ID = Convert.ToInt32(dr["FUNCTION_ID"]),
                    FUNCTION_NAME = Convert.ToString(dr["FUNCTION_NAME"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }
    }
}
