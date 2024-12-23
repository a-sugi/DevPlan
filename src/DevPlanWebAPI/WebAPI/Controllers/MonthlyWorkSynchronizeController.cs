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
    /// 月次計画表同期
    /// </summary>
    public class MonthlyWorkSynchronizeController : BaseAPIController<MonthlyWorkSynchronizeLogic, MonthlyWorkSynchronizePostOutModel>
    {
        /// <summary>
        /// 月次計画表同期
        /// </summary>
        /// <param name="val">月次計画同期登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]MonthlyWorkSynchronizePostInModel val)
        {
            db.Begin();

            var flg = base.GetLogic().PostData(val);

            if (flg == false)
            {
                db.Rollback();
            }
            else
            {
                db.Commit();
            }

            return Ok(base.GetResponse(flg));
        }
    }
}
