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
    /// 機能権限名
    /// </summary>
    public class FunctionAuthorityNameController : BaseAPIController<FunctionAuthorityNameLogic, FunctionAuthorityNameOutModel>
    {
        #region 取得
        /// <summary>
        /// 機能権限名取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]FunctionAuthorityNameInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));
            }

            var list = new List<FunctionAuthorityNameOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                var wkTARGET = Convert.ToString(dr["DEPARTMENT_NAME"]);
                wkTARGET += (Convert.ToString(dr["SECTION_NAME"]) == "" ? "" : " /" + Convert.ToString(dr["SECTION_NAME"]));
                wkTARGET += (Convert.ToString(dr["SECTION_GROUP_NAME"]) == "" ? "" : " /" + Convert.ToString(dr["SECTION_GROUP_NAME"]));
                wkTARGET += (Convert.ToString(dr["NAME"]) == "" ? "" : " /" + Convert.ToString(dr["NAME"]));
                list.Add(new FunctionAuthorityNameOutModel
                {
                    TARGET = wkTARGET,
                    DEPARTMENT_ID = Convert.ToString(dr["DEPARTMENT_ID"]),
                    SECTION_ID = Convert.ToString(dr["SECTION_ID"]),
                    SECTION_GROUP_ID = Convert.ToString(dr["SECTION_GROUP_ID"]),
                    PERSONEL_ID = Convert.ToString(dr["PERSONEL_ID"]),
                    LAYER = Convert.ToInt16(dr["LAYER"])
                });
            }

            return Ok(base.GetResponse(list));
        }
        #endregion
    }
}
