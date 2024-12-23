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
    /// 製作車
    /// </summary>
    public class ProductionCarController : BaseAPIController<ProductionCarLogic, ProductionCarCommonModel>
    {
        /// <summary>
        /// 製作車検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]ProductionCarSearchModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));
        }

        /// <summary>
        /// 製作車登録
        /// </summary>
        /// <param name="list">製作車登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]List<ProductionCarPostInModel> list)
        {
            db.Begin();

            var res = new List<ProductionCarCommonModel>();
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

        /// <summary>
        /// 製作車更新
        /// </summary>
        /// <param name="list">製作車更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]List<ProductionCarPutInModel> list)
        {
            db.Begin();

            var flg = base.GetLogic().PutData(list);

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

        /// <summary>
        /// 製作車削除
        /// </summary>
        /// <param name="list">製作車削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]List<ProductionCarDeleteInModel> list)
        {
            db.Begin();

            var flg = base.GetLogic().DeleteData(list);

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
