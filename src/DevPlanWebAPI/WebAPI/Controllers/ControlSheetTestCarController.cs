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
    /// 試験車(管理票)
    /// </summary>
    public class ControlSheetTestCarController : BaseAPIController<ControlSheetTestCarLogic, TestCarCommonModel>
    {
        /// <summary>
        /// 試験車(管理票)検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarCommonSearchModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));
        }

        /// <summary>
        /// 試験車(管理票)登録
        /// </summary>
        /// <param name="val">試験車共通モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]TestCarCommonBaseModel val)
        {
            db.Begin();

            var returns = new TestCarCommonModel();

            var flg = base.GetLogic().PostData(val, ref returns);

            if (flg == false)
            {
                db.Rollback();

                return Ok(base.GetResponse(flg));
            }

            db.Commit();

            var resurlts = new List<TestCarCommonModel>();

            resurlts.Add(returns);

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 試験車(管理票)更新
        /// </summary>
        /// <param name="val">試験車共通モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]TestCarCommonModel val)
        {
            db.Begin();

            var flg = base.GetLogic().PutData(val);

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
        /// 試験車(管理票)削除
        /// </summary>
        /// <param name="val">試験車(管理票)削除入力モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]TestCarCommonBaseModel val)
        {
            db.Begin();

            var flg = base.GetLogic().DeleteData(val);

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
