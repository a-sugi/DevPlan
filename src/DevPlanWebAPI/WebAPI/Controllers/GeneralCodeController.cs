//
// 業務計画表システム
// KKA00050 開発符号検索API
// 作成者 : 岸　義将
// 作成日 : 2017/02/09

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
    /// <summary>
    /// 開発符号
    /// </summary>
    public class GeneralCodeController : BaseAPIController<GeneralCodeLogic, GeneralCodeSearchOutModel>
    {
        /// <summary>
        /// 開発符号取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]GeneralCodeSearchInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            var list = new List<GeneralCodeSearchOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new GeneralCodeSearchOutModel
                {
                    CAR_GROUP = Convert.ToString(dr["CAR_GROUP"]),
                    GENERAL_CODE = Convert.ToString(dr["GENERAL_CODE"]),
                    SORT_NUMBER = Convert.ToInt64(dr["SORT_NUMBER"]),
                    UNDER_DEVELOPMENT = Convert.ToString(dr["UNDER_DEVELOPMENT"]),
                    BASE_GENERAL_CODE = Convert.ToString(dr["BASE_GENERAL_CODE"]),
                    PERMIT_FLG = Convert.ToInt16(dr["PERMIT_FLG"])
                });
            }

            return Ok(base.GetResponse(list));

        }
    }
}
