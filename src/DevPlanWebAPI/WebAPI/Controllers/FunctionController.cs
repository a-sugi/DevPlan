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
    /// 機能マスター
    /// </summary>
    public class FunctionController : BaseAPIController<FunctionLogic, FunctionOutModel>
    {
        /// <summary>
        /// 機能マスター取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]FunctionInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            var list = new List<FunctionOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new FunctionOutModel
                {
                    FUNCTION_NAME = Convert.ToString(dr["FUNCTION_NAME"]),
                    FUNCTION_ID = Convert.ToInt64(dr["ID"]),
                });
            }

            return Ok(base.GetResponse(list));

        }
    }
}
