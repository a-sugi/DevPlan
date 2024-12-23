﻿using System;
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
    /// 週報コントローラー
    /// </summary>
    public class WeeklyReportController : BaseAPIController<WeeklyReportLogic, WeeklyOutModel>
    {
        /// <summary>
        /// 週報取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]WeeklyInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(val)));
        }

        /// <summary>
        /// 週報登録
        /// </summary>
        /// <param name="val">週報登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]WeeklyOutModel[] val)
        {
            var flg = true;

            db.Begin();

            //指定数分処理を行う。
            foreach (var model in val)
            {
                //登録実行
                if (!base.GetLogic().Post(model))
                {
                    flg = false;
                    break;
                }
            }

            if (flg == false)
            {
                //処理異常の場合、ロールバック
                db.Rollback();
            }
            else
            {
                //処理正常の場合、コミット
                db.Commit();
            }

            return Ok(base.GetResponse(flg));
        }

        /// <summary>
        /// 週報更新
        /// </summary>
        /// <param name="val">週報更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]WeeklyOutModel[] val)
        {
            var flg = true;

            db.Begin();

            //指定数分処理を行う。
            foreach (var model in val)
            {
                //更新実行
                if (!base.GetLogic().Put(model))
                {
                    flg = false;
                    break;
                }
            }

            if (flg == false)
            {
                //処理異常の場合、ロールバック
                db.Rollback();
            }
            else
            {
                //処理正常の場合、コミット
                db.Commit();
            }

            return Ok(base.GetResponse(flg));
        }

        /// <summary>
        /// 週報削除
        /// </summary>
        /// <param name="val">週報削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]WeeklyOutModel[] val)
        {
            db.Begin();

            var flg = true;

            //指定数分処理を行う。
            foreach (var model in val)
            {
                //削除実行
                if (!base.GetLogic().Delete(model))
                {
                    flg = false;
                    break;
                }
            }

            if (flg == false)
            {
                //処理異常の場合、ロールバック
                db.Rollback();

            }
            else
            {
                //処理正常の場合、コミット
                db.Commit();
            }

            return Ok(base.GetResponse(flg));
        }
    }
}
