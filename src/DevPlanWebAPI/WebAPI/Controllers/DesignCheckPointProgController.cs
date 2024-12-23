using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 設計チェック指摘と状況の同時登録
    /// </summary>
    public class DesignCheckPointProgController : BaseAPIController<DesignCheckPointProgLogic, object>
    {
        /// <summary>
        /// 設計チェック指摘・状況登録
        /// </summary>
        /// <param name="list">設計チェック指摘と状況登録入力モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]List<DesignCheckProgressPostInModel> list)
        {
            db.Begin();

            var flg = base.GetLogic().PostData(list);

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