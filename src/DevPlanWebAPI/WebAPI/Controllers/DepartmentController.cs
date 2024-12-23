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
    /// 部
    /// </summary>
    public class DepartmentController : BaseAPIController<DepartmentLogic, DepartmentSearchOutModel>
    {
        #region 取得
        /// <summary>
        /// 部取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DepartmentSearchInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));

            }

            var list = new List<DepartmentSearchOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new DepartmentSearchOutModel
                {
                    DEPARTMENT_ID = Convert.ToString(dr["DEPARTMENT_ID"]),
                    DEPARTMENT_CODE = Convert.ToString(dr["DEPARTMENT_CODE"]),
                    DEPARTMENT_NAME = Convert.ToString(dr["DEPARTMENT_NAME"]),
                    ESTABLISHMENT = Convert.ToString(dr["ESTABLISHMENT"]),
                    SORT_NO = Convert.IsDBNull(dr["SORT_NO"]) ? 0 : Convert.ToInt64(dr["SORT_NO"])
                });
            }

            return Ok(base.GetResponse(list));
            
        }
        #endregion
    }
}
