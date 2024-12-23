using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.UC
{
    /// <summary>
    /// 年月固定DateTimePicker
    /// </summary>
    /// <remarks>
    /// 年月の指定のみ行えるDateTimePickerです。
    /// </remarks>
    public class YerarMonthDateTimePicker : DateTimePicker
    {
        bool calendar = false;
        Timer calendarTimer = new Timer();
        int yyyy;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public YerarMonthDateTimePicker() : base()
        {
            calendarTimer.Interval = 1;
            calendarTimer.Tick += CalendarTimer_Tick;
            this.CustomFormat = "yyyy/MM";
            this.Format = DateTimePickerFormat.Custom;
        }

        /// <summary>
        /// OnCloseUpイベント。
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnCloseUp(EventArgs eventargs)
        {
            calendarTimer.Enabled = false;
            calendar = false;

            base.OnCloseUp(eventargs);
        }

        /// <summary>
        /// Tickイベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarTimer_Tick(object sender, EventArgs e)
        {
            calendarTimer.Enabled = false;

            this.Select();
            SendKeys.SendWait("^{UP}");
        }

        /// <summary>
        /// OnDropDownイベント。
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnDropDown(EventArgs eventargs)
        {
            calendarTimer.Enabled = true;
            calendar = true;
            yyyy = ((DateTime)this.Value).Year;

            base.OnDropDown(eventargs);
        }

        /// <summary>
        /// OnValueChangedイベント。
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnValueChanged(EventArgs eventargs)
        {
            if (calendar)
            {
                if (yyyy != ((DateTime)this.Value).Year)
                {
                    yyyy = ((DateTime)this.Value).Year;
                }
                else
                {
                    this.Select();
                    SendKeys.SendWait("{ENTER}");
                }
            }
            base.OnValueChanged(eventargs);
        }
    }
}
