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
    public class XeyeController : BaseAPIController<XeyeLogic, XeyeSearchOutModel>
    {
        #region 取得
        /// <summary>
        /// XeyeID取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]XeyeSearchInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));

        }
        #endregion

        #region 登録
        /// <summary>
        /// Xeyeデータ登録
        /// </summary>
        /// <param name="list">CSVデータ</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<XeyeSearchOutModel> list)
        {
            db.Begin();

            var res = new List<XeyeSearchOutModel>();
            var flg = base.GetLogic().PostData(list, ref res);

            if (flg == false)
            {
                db.Rollback();
            }
            else
            {
                db.Commit();
            }

            return Ok(base.GetResponse(res));
        }
        #endregion
    }
}