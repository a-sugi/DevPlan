using DevPlan.Presentation.Common;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon;
using DevPlan.UICommon.Utils.Calendar;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    /// <summary>
    /// カーシェア日程Excel出力フォーム。
    /// </summary>
    public partial class CalendarPrintExcelForm : CalendarPrintExcelBaseForm
    {
        private CarShareScheduleSearchModel carShareScheduleSearchCond;
        private List<CarShareScheduleItemModel> scheduleItemList;        
        private CalendarTemplateTypeSafeEnum templateType;
        
        /// <summary>
        /// デフォルトコンストラクタ。
        /// </summary>
        /// <remarks>
        /// 画面を初期化します。
        /// </remarks>
        public CalendarPrintExcelForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="scheduleItemList"></param>
        /// <param name="carShareScheduleSearchCond"></param>
        /// <param name="calendarMode"></param>
        public CalendarPrintExcelForm(List<CarShareScheduleItemModel> scheduleItemList, CarShareScheduleSearchModel carShareScheduleSearchCond,
            CalendarTemplateTypeSafeEnum calendarMode) : this()
        {
            this.scheduleItemList = scheduleItemList;
            this.carShareScheduleSearchCond = carShareScheduleSearchCond;
            this.templateType = calendarMode;
        }

        /// <summary>
        /// Excel出力ボタン押下処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            this.carShareScheduleSearchCond.START_DATE = base.StartDate;
            this.carShareScheduleSearchCond.END_DATE = base.EndDate;
                        
            var res = HttpUtil.GetResponse<CarShareScheduleSearchModel, CarShareScheduleModel>(ControllerType.CarShareSchedule, this.carShareScheduleSearchCond);

            var scheduleList = new List<CarShareScheduleModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                scheduleList.AddRange(res.Results);
            }

            Func<CarShareScheduleModel, string> getSubTitle =
                schedule => string.Format("{0}{1}", (schedule.予約種別 == Const.Kariyoyaku ? Const.Kari : ""), schedule.DESCRIPTION);

            var outItem = new Dictionary<long, string>();//項目名リスト。
            var outSchedule = new Dictionary<long, List<ExcelCalendarInfo>>();//スケジュールリスト。
            var closeItemDic = new Dictionary<long, DateTime>();

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

                        sb.AppendLine(string.Format("{0} {1}({2})", schedule.予約者_SECTION_CODE, schedule.予約者_NAME, schedule.TEL));
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

                    if (item.最終予約可能日 != null && item.最終予約可能日.Value < this.EndDate)
                    {
                        var closeDate = item.最終予約可能日.Value.AddDays(1).Date;

                        closeItemDic.Add(item.ID, (this.StartDate.Date > closeDate) ? this.StartDate.Date : closeDate);
                    }
                }
            }

            base.Output(this.templateType, outItem, outSchedule, closeItemDic);
        }
    }
}
