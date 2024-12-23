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
    /// 課
    /// </summary>
    public class SectionController : BaseAPIController<SectionLogic, SectionSearchOutModel>
    {
        /// <summary>
        /// 課取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]SectionSearchInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));

            }

            var list = new List<SectionSearchOutModel>();

            foreach (DataRow dr in dt.Rows)
            {

                list.Add(new SectionSearchOutModel
                {
                    DEPARTMENT_ID = Convert.ToString(dr["DEPARTMENT_ID"]),
                    DEPARTMENT_CODE = Convert.ToString(dr["DEPARTMENT_CODE"]),
                    DEPARTMENT_NAME = Convert.ToString(dr["DEPARTMENT_NAME"]),
                    ESTABLISHMENT = Convert.ToString(dr["ESTABLISHMENT"]),
                    SECTION_ID = Convert.ToString(dr["SECTION_ID"]),
                    SECTION_CODE = Convert.ToString(dr["SECTION_CODE"]),
                    SECTION_NAME = Convert.ToString(dr["SECTION_NAME"]),
                    SORT_NO_SECTION_DATA = Convert.ToInt16(dr["SORT_NO_SECTION_DATA"]),
                    SORT_NO_DEPARTMENT_DATA = Convert.ToInt64(dr["SORT_NO_DEPARTMENT_DATA"]),
                    FLAG_KENJITSU = dr["FLAG_KENJITSU"] == DBNull.Value ? (short?)null : Convert.ToInt16(dr["FLAG_KENJITSU"]),
                });

            }

            return Ok(base.GetResponse(list));

        }
    }
}
