using DevPlan.Presentation.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Utils.Calendar;
using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Enum;

namespace DevPlan.Presentation.UIDevPlan.OuterCar
{
    public partial class CalendarPrintExcelForm : CalendarPrintExcelBaseForm
    {
        private CalendarTemplateTypeSafeEnum templateType;
        private List<OuterCarScheduleItemGetOutModel> scheduleItemList;
        private OuterCarScheduleGetInModel outerCarScheduleSearchCond;

        public CalendarPrintExcelForm()
        {
            InitializeComponent();
        }

        public CalendarPrintExcelForm(List<OuterCarScheduleItemGetOutModel> list, OuterCarScheduleGetInModel outerCarScheduleSearchCond,
            CalendarTemplateTypeSafeEnum calendarMode) : this()
        {
            this.scheduleItemList = list;
            this.outerCarScheduleSearchCond = outerCarScheduleSearchCond;
            this.templateType = calendarMode;
        }

        private void ExcelOutputButton_Click(object sender, EventArgs e)
        {
            if (base.CanOutputPeriod() == false)
            {
                Messenger.Warn(Resources.KKM02003);
                return;
            }

            if (this.scheduleItemList.Count <= 0)
            {
                Messenger.Warn(Resources.KKM03039);
                return;
            }

            this.outerCarScheduleSearchCond.DATE_START = base.StartDate;
            this.outerCarScheduleSearchCond.DATE_END = base.EndDate;

            var res = HttpUtil.GetResponse<OuterCarScheduleGetInModel, OuterCarScheduleGetOutModel>(ControllerType.OuterCarSchedule, this.outerCarScheduleSearchCond);

            var scheduleList = new List<OuterCarScheduleGetOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                scheduleList.AddRange(res.Results);
            }

            Func<OuterCarScheduleGetOutModel, string> getSubTitle =
                schedule => string.Format("{0}{1}", (schedule.予約種別 == Const.Kariyoyaku ? Const.Kari : ""), schedule.DESCRIPTION);

            var outItem = new Dictionary<long, string>();//項目名リスト。
            var outSchedule = new Dictionary<long, List<ExcelCalendarInfo>>();//スケジュールリスト。
            var closeItemDic = new Dictionary<long, DateTime>();

            for (var j = 0; j < this.scheduleItemList.Count; j++)
            {
                var setteiData = scheduleList.Where(v => v.CATEGORY_ID == this.scheduleItemList[j].CATEGORY_ID).ToList();

                if (setteiData.Count >= 1)
                {
                    var list = new List<ExcelCalendarInfo>();
                    foreach (var schedule in setteiData)
                    {
                        var info = new ExcelCalendarInfo()
                        {
                            StartDate = schedule.START_DATE.Value,
                            EndDate = schedule.END_DATE.Value,
                            RowNo = schedule.PARALLEL_INDEX_GROUP,
                            SubTitle = getSubTitle(schedule)
                        };

                        var sb = new StringBuilder();
                        if (SymbolMapTypeSafeEnum.KeyOf(schedule.SYMBOL) != null)
                        {
                            sb.Append(SymbolMapTypeSafeEnum.KeyOf(schedule.SYMBOL).Mark);
                        }
                        sb.AppendLine(schedule.DESCRIPTION);

                        string format = "{0:yyyy/MM/dd HH:mm}";
                        sb.AppendFormat(format, schedule.START_DATE.Value);
                        sb.Append("～");
                        sb.AppendFormat(format, schedule.END_DATE.Value).AppendLine();

                        sb.AppendLine(string.Format("{0} {1}({2})", schedule.予約者_SECTION_CODE, schedule.予約者_NAME, schedule.TEL));
                        sb.AppendFormat(format, schedule.INPUT_DATETIME.Value);

                        info.ToolTip = sb.ToString();

                        list.Add(info);
                    }
                    outSchedule.Add(this.scheduleItemList[j].CATEGORY_ID, list);
                }
                var item = this.scheduleItemList[j];
               

                if (item != null)
                {
                    outItem.Add(item.CATEGORY_ID, item.CATEGORY);

                    if (item.最終予約可能日 != null && item.最終予約可能日.Value < this.EndDate)
                    {
                        var closeDate = item.最終予約可能日.Value.AddDays(1).Date;

                        closeItemDic.Add(item.CATEGORY_ID, (this.StartDate.Date > closeDate) ? this.StartDate.Date : closeDate);
                    }
                }
            }

            base.Output(this.templateType, outItem, outSchedule, closeItemDic);
        }
    }
}
