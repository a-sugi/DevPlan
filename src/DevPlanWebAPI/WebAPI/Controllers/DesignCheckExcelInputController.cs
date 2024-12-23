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
    //Append Start 2021/06/24 張晋華 開発計画表設計チェック機能改修

    /// <summary>
    /// EXCEL_INPUT
    /// </summary>
    public class DesignCheckExcelInputController : BaseAPIController<DesignCheckExcelInputLogic, DesignCheckExcelInputModel>
    {
        #region 取得
        /// <summary>
        /// EXCELを取り込んだかどうかの取得
        /// </summary>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DesignCheckExcelInputModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var results = new List<DesignCheckExcelInputModel>();

            foreach (DataRow dr in dt.Rows)
            {
                results.Add(new DesignCheckExcelInputModel
                {
                    ID = Convert.ToInt32(dr["ID"]),
                });
            }

            return Ok(base.GetResponse(results));
        }
        #endregion

        #region 登録
        /// <summary>
        /// EXCEL_INPUTに開催日_ID、登録日、登録者_IDを登録する
        /// </summary>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]List<DesignCheckExcelInputModel> list)
        {
            db.Begin();

            var res = new List<DesignCheckExcelInputModel>();

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
    //Append End 2021/06/24 張晋華 開発計画表設計チェック機能改修
}