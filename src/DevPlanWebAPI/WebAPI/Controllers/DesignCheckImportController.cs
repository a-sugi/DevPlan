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

    //Append Start 2021/05/17 張晋華 開発計画表設計チェック機能改修

    /// <summary>
    /// 設計チェックインポート
    /// </summary>
    public class DesignCheckImportController : BaseAPIController<DesignCheckImportLogic, DesignCheckImportModel>
    {
        #region 登録
        /// <summary>
        /// インポートしたExcelデータ登録
        /// </summary>
        /// <param name="list">Excelデータ</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]List<DesignCheckImportModel> list)
        {
            db.Begin();

            var res = new List<DesignCheckImportModel>();
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
    //Append End 2021/05/17 張晋華 開発計画表設計チェック機能改修
}