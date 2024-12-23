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
    /// 週報承認コントローラー
    /// </summary>
    public class WeeklyReportApprovalController : BaseAPIController<WeeklyReportApprovalLogic, WeeklyReportApproveOutModel>
    {
        /// <summary>
        /// 週報承認取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]WeeklyReportApproveInModel val)
        {
            //検索実行
            return Ok(base.GetResponse(base.GetLogic().Get(val)));

        }

        /// <summary>
        /// 週報承認登録
        /// </summary>
        /// <param name="val">週報承認登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]WeeklyReportApproveModel[] val)
        {
            db.Begin();

            var flg = true;

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

            if (flg)
            {
                //登録実行
                flg = base.GetLogic().Post(val[0]);
            }

            if (flg == false)
            {
                //処理異常の場合、ロールバック
                db.Rollback();
            }
            else
            {
                {
                    //処理正常の場合、コミット
                    db.Commit();
                }
            }

            return Ok(base.GetResponse(flg));
        }
    }
}
