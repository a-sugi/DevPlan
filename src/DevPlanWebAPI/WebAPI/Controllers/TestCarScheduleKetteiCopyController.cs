using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 試験車スケジュール 本予約コピーコントローラー。
    /// </summary>
    public class TestCarScheduleKetteiCopyController : BaseAPIController<TestCarLogic, TestCarScheduleModel>
    {
        /// <summary>
        /// 試験車スケジュール本予約コピー処理。
        /// </summary>
        /// <remarks>
        /// 渡されたデータを仮予約として更新した後、
        /// 同データを本予約・最終調整結果として登録を行います。
        /// [0]の指定をしていますが、ListじゃなくなるとBaseまで直すことになりそうなので
        /// [0]指定をしています（ので１レコードが前提の処理です）
        /// </remarks>
        /// <param name="list">試験車スケジュール</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<TestCarScheduleModel> list)
        {
            var model = list[0];
            //既にコピーされているか確認を行う。
            if (base.GetLogic().IsExistKetteiCopy(model.GENERAL_CODE, model.ID, model.CATEGORY_ID))
            {
                return Ok(base.GetResponse(MessageType.KKE03024));
            }

            model.予約種別 = "仮予約";

            // 仮予約者のIDを事前取得
            var kari = base.GetLogic().GetTestCarSchedule(new TestCarScheduleSearchModel()
            {
                ID = model.ID
            }).FirstOrDefault();

            //仮予約スケジュールを更新
            var ret = base.GetLogic().UpdateTestCarSchedule(list);

            if (ret)
            {
                var scheduleData = base.GetLogic().GetMaxScopeSchedule(
                    model.GENERAL_CODE, model.CATEGORY_ID.Value, "決定", model.START_DATE.Value, model.END_DATE.Value, model.PARALLEL_INDEX_GROUP.Value);

                model.予約種別 = "本予約";
                model.試験車日程種別 = "決定";
                model.コピー元_ID = model.ID;
                model.INPUT_PERSONEL_ID = kari.INPUT_PERSONEL_ID;

                if (!(scheduleData.Count() == 0 || scheduleData.Where(x => x.PARALLEL_INDEX_GROUP == model.PARALLEL_INDEX_GROUP).Count() == 0))
                {
                    List<TestCarScheduleModel> updateRowNoList = new List<TestCarScheduleModel>();

                    foreach (var item in scheduleData.Where(x => x.PARALLEL_INDEX_GROUP > model.PARALLEL_INDEX_GROUP).ToList())
                    {
                        var data = base.GetLogic().GetTestCarSchedule(new TestCarScheduleSearchModel()
                        {
                            ID = item.ID
                        }).First();

                        data.PARALLEL_INDEX_GROUP += 1;
                        updateRowNoList.Add(data);
                    }

                    if (base.GetLogic().UpdateTestCarSchedule(updateRowNoList))
                    {
                        model.PARALLEL_INDEX_GROUP += 1;
                    }
                    else
                    {
                        return Ok(base.GetResponse(MessageType.KKE03001));
                    }
                }
            }
            else
            {
                return Ok(base.GetResponse(MessageType.KKE03001));
            }
            return Ok(base.GetResponse(base.GetLogic().InsertTestCarSchedule(list)));
        }
    }
}