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
    /// メーカー名
    /// </summary>
    public class MakerNameController : BaseAPIController<MakerNameLogic, MakerNameSearchOutModel>
    {
        /// <summary>
        /// メーカー名取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]MakerNameSearchInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03002));

            }

            var list = new List<MakerNameSearchOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new MakerNameSearchOutModel
                {
                    メーカー名 = Convert.ToString(dr["メーカー名"])
                });
            }

            return Ok(base.GetResponse(list));

        }
    }
}
