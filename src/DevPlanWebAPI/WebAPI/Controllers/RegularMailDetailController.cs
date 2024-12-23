using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// メール原文修正コントローラー
    /// </summary>
    public class RegularMailDetailController : BaseAPIController<RegularMailDetailLogic, RegularMailDetailModel>
    {
        /// <summary>
        /// メール原文修正画面情報取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]RegularMailDetailSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetRegularMailDetailList(cond)));
        }

        /// <summary>
        /// メール原文修正画面情報更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(RegularMailDetailModel model)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateRegularMailDetail(model)));
        }
    }

    /// <summary>
    /// メール原文特殊文字コントローラー
    /// </summary>
    public class RegularMailDetailMasterController : BaseAPIController<RegularMailDetailLogic, RegularMailDetailMasterModel>
    {
        /// <summary>
        /// メール原文修正画面特殊文字取得
        /// </summary>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get()
        {
            return Ok(base.GetResponse(base.GetLogic().GetRegularMailDetailMasterList()));
        }
    }
}
