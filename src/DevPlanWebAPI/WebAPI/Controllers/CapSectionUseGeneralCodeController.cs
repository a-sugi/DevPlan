using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// CAP部署（開発符号）
    /// </summary>
    public class CapSectionUseGeneralCodeController : BaseAPIController<CapSectionUseGeneralCodeLogic, CapSectionUseGeneralCodeOutModel>
    {
        /// <summary>
        /// CAP部署（開発符号）の取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CapSectionUseGeneralCodeInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            var list = new List<CapSectionUseGeneralCodeOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new CapSectionUseGeneralCodeOutModel
                {
                    専門部署名 = Convert.ToString(dr["専門部署名"]),
                    PERMIT_FLG = Convert.ToInt16(dr["PERMIT_FLG"])
                });
            }

            return Ok(base.GetResponse(list));
        }
    }
}