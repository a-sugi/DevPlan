using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 定時間日の設定コントローラー。
    /// </summary>
    public class FixedTimeDaySettingController : BaseAPIController<TruckMasterLogic, FixedTimeDaySettingModel>
    {
        //定時間日の検索、登録、更新

        /// <summary>
        /// 定時間日検査
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]FixedTimeDaySettingSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().FixedTimeDaySetting(cond)));
        }

        [CommonMethodFilter]
        public IHttpActionResult Put(List<FixedTimeDaySettingModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateFixedTimeDaySetting(list)));
        }

        [CommonMethodFilter]
        public IHttpActionResult Delete(List<FixedTimeDaySettingModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteFixedTimeDay(list)));
        }
    }
}
