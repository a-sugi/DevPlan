using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// トラック予約（項目）コントローラー
    /// </summary>
    public class TruckScheduleItemController : BaseAPIController<TruckScheduleLogic, TruckScheduleItemModel>
    {
        /// <summary>
        /// トラック予約項目取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TruckScheduleItemSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetTruckScheduleItem(cond)));
        }
        
        /// <summary>
        /// トラック予約項目登録
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<TruckScheduleItemModel> list)
        {
            var model = list.FirstOrDefault();

            var teikiNumber = 0;
            if (model.FLAG_定期便 == 1)
            {
                var numberList = base.GetLogic().GetScheduleItem();
                if (numberList.FirstOrDefault().SERIAL_NUMBER > 20 || numberList.FirstOrDefault().SERIAL_NUMBER <= 0)
                {
                    return Ok(base.GetResponse(MessageType.KKE03025));
                }
                else
                {
                    teikiNumber = numberList.FirstOrDefault().SERIAL_NUMBER;
                }
            }

            return Ok(base.GetResponse(base.GetLogic().InsertItem(model, teikiNumber)));
        }

        /// <summary>
        /// トラック予約項目更新
        /// </summary>
        /// <param name="list">トラック予約項目クラスリスト</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<TruckScheduleItemModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateItem(list)));
        }

        /// <summary>
        /// トラック予約項目削除
        /// </summary>
        /// <param name="list">トラック予約項目クラスリスト</param>
        /// <returns></returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<TruckScheduleItemModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteItem(list.FirstOrDefault())));
        }
    }
}
