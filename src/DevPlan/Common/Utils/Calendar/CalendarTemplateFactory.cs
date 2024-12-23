using System;
using DevPlan.UICommon.Utils.Calendar.Templates;
using GrapeCity.Win.CalendarGrid;

namespace DevPlan.UICommon.Utils.Calendar
{
    /// <summary>
    /// カレンダーテンプレートファクトリークラス。
    /// </summary>
    /// <remarks>
    /// カレンダーテンプレートオブジェクトを管理します。
    /// </remarks>
    public class CalendarTemplateFactory
    {
        public CalendarTemplate CreateTemplate(CalendarSettings calendarSetting)
        {
            var type = calendarSetting.CalendarMode;

            if (type == CalendarTemplateTypeSafeEnum.DEFAULT)
            {
                return new DefaultCalendarTemplate(calendarSetting.Style[type]);
            }
            else if (type == CalendarTemplateTypeSafeEnum.EXPANSION1)
            {
                return new Expansion1CalendarTemplate(calendarSetting.Style[type]);
            }
            else if (type == CalendarTemplateTypeSafeEnum.EXPANSION2)
            {
                return new Expansion2CalendarTemplate(calendarSetting.Style[type]);
            }
            else
            {
                throw new Exception("カレンダー設定が見つかりませんでした。");
            }
        }
    }
}
