using DevPlan.Presentation.Common;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevPlan.UICommon.Utils.Calendar;

namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    public partial class CalendarPrintExcelForm : CalendarPrintExcelBaseForm
    {
        private CalendarTemplateTypeSafeEnum templateType;
        private List<TestCarScheduleItemModel> scheduleItemList;
        private TestCarScheduleSearchModel testCarScheduleSearchCond;

        public CalendarPrintExcelForm()
        {
            InitializeComponent();
        }

        public CalendarPrintExcelForm(List<TestCarScheduleItemModel> list, TestCarScheduleSearchModel testCarScheduleSearchCond, CalendarTemplateTypeSafeEnum calendarMode) : this()
        {
            this.scheduleItemList = list;
            this.testCarScheduleSearchCond = testCarScheduleSearchCond;
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

            this.testCarScheduleSearchCond.START_DATE = base.StartDate;
            this.testCarScheduleSearchCond.END_DATE = base.EndDate;

            var res = HttpUtil.GetResponse<TestCarScheduleSearchModel, TestCarScheduleModel>(ControllerType.TestCarSchedule, this.testCarScheduleSearchCond);

            var scheduleList = new List<TestCarScheduleModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                scheduleList.AddRange(res.Results);
            }
            
            #region 最大行数以上のデータについては仮登録の下へまとめて表示する。
            if (this.testCarScheduleSearchCond.IsGetKettei)
            {
                var list = scheduleList.ToList();
                if (list.Count() > 0)
                {
                    var readOnlyRowCount = list.Max(x => x.PARALLEL_INDEX_GROUP).Value + Const.ScheduleItemRowMax;
                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (list[i].試験車日程種別 == Const.Kettei) //最終調整結果については全て読み取り専用行以降とする。
                        {
                            list[i].PARALLEL_INDEX_GROUP = list[i].PARALLEL_INDEX_GROUP + readOnlyRowCount;
                        }
                    }
                }
            }
            #endregion

            Func<TestCarScheduleModel, string> getSubTitle =
                schedule => string.Format("{0}{1}", (schedule.予約種別 == Const.Kariyoyaku ? Const.Kari : ""), schedule.DESCRIPTION);

            var outItem = new Dictionary<long, string>();//項目名リスト。
            var outSchedule = new Dictionary<long, List<ExcelCalendarInfo>>();//スケジュールリスト。

            for (var j = 0; j < this.scheduleItemList.Count; j++)
            {
                var setteiData = scheduleList.Where(v => v.CATEGORY_ID == this.scheduleItemList[j].ID).ToList();

                if (setteiData.Count >= 1)
                {
                    var list = new List<ExcelCalendarInfo>();
                    foreach (var schedule in setteiData)
                    {
                        var info = new ExcelCalendarInfo()
                        {
                            StartDate = schedule.START_DATE.Value,
                            EndDate = schedule.END_DATE.Value,
                            RowNo = schedule.PARALLEL_INDEX_GROUP.Value,
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

                        sb.AppendLine(string.Format("{0} {1}", schedule.設定者_SECTION_CODE, schedule.設定者_NAME));
                        sb.AppendFormat(format, schedule.INPUT_DATETIME.Value);

                        info.ToolTip = sb.ToString();

                        list.Add(info);
                    }
                    outSchedule.Add(this.scheduleItemList[j].ID, list);
                }
                var item = this.scheduleItemList[j];

                if (item != null)
                {
                    outItem.Add(item.ID, item.CATEGORY);
                }
            }

            base.Output(this.templateType, outItem, outSchedule, new Dictionary<long, DateTime>());
        }
    }
}
