using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// トラック予約（スケジュール）
    /// </summary>
    public class TruckScheduleController : BaseAPIController<TruckScheduleLogic, TruckScheduleModel>
    {
        /// <summary>
        /// トラック予約スケジュール取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TruckScheduleSearchModel cond)
        {
            var scheduleList = base.GetLogic().GetTruckSchedule(cond);

            var ret = new List<TruckScheduleModel>();

            if (scheduleList != null)
            {
                var usersList = base.GetLogic().GetTruckShipperRecipientUser(cond);
                var sectionList = base.GetLogic().GetSection(cond);

                foreach (var schedule in scheduleList)
                {
                    ret.Add(new TruckScheduleModel(schedule)
                    {
                        ShipperRecipientUserList = usersList.Where(x => x.予約_ID == schedule.ID).ToList(),
                        SectionList = sectionList.Where(x => x.予約_ID == schedule.ID).ToList()
                    });
                }
            }

            return Ok(base.GetResponse(ret));
        }

        /// <summary>
        /// トラック予約スケジュール登録
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<TruckScheduleModel> list)
        {
            base.GetLogic().InsertSchedule(list.FirstOrDefault());
            return Ok(base.GetResponse(list));
        }

        /// <summary>
        /// トラック予約スケジュール更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<TruckScheduleModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateSchedule(list.FirstOrDefault())));

        }

        /// <summary>
        /// トラック予約スケジュール削除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<TruckScheduleModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteSchedule(list.FirstOrDefault())));
        }
    }
}
