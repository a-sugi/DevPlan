using DevPlan.Presentation.Common;
using DevPlan.UICommon.Enum;
using System;
using System.Drawing;

namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    /// <summary>
    /// カーシェア日程　凡例フォーム。
    /// </summary>
    /// <remarks>
    /// 凡例フォーム基底クラスを利用し、
    /// カーシェア日程用凡例フォームを作成します。
    /// </remarks>
    public partial class CarChareLegendForm : LegendForm
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// 画面を初期化します。
        /// </remarks>
        public CarChareLegendForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォーム初期表示イベント。
        /// </summary>
        /// <remarks>
        /// 画面に配置されているラベルへカラーを設定します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarChareLegendForm_Shown(object sender, EventArgs e)
        {
            base.ScheduleColorGroupBox.Visible = false;

            this.HonyoyakuSiyoutyuuLabel.ForeColor = CalendarScheduleColorEnum.HonyoyakuSiyoutyuu.MainColor;
            this.HonyoyakuSiyouHenkyakuLabel.ForeColor = CalendarScheduleColorEnum.HonyoyakuSiyouHenkyaku.MainColor;
            this.HonyoyakuSiyouMihenkyakuLabel.ForeColor = CalendarScheduleColorEnum.HonyoyakuSiyouMihenkyaku.MainColor;
            this.HonyoyakuEndLabel.ForeColor = CalendarScheduleColorEnum.HonyoyakuEnd.MainColor;
            this.HonyoyakuFutureLabel.ForeColor = CalendarScheduleColorEnum.HonyoyakuFuture.MainColor;
            this.YoyakuKyokaHonyoyakuLabel.ForeColor = CalendarScheduleColorEnum.YoyakuKyokaHonyoyaku.MainColor;
            this.OtherLabel.ForeColor = CalendarScheduleColorEnum.Other.MainColor;

            this.HonyoyakuSiyoutyuu2Label.BackColor = CalendarScheduleColorEnum.HonyoyakuSiyoutyuu.MainColor;
            this.HonyoyakuSiyouHenkyaku2Label.BackColor = CalendarScheduleColorEnum.HonyoyakuSiyouHenkyaku.MainColor;
            this.HonyoyakuSiyouMihenkyaku2Label.BackColor = CalendarScheduleColorEnum.HonyoyakuSiyouMihenkyaku.MainColor;
            this.HonyoyakuEnd2Label.BackColor = CalendarScheduleColorEnum.HonyoyakuEnd.MainColor;
            this.HonyoyakuFuture2Label.BackColor = CalendarScheduleColorEnum.HonyoyakuFuture.MainColor;
            this.YoyakuKyokaHonyoyaku2Label.BackColor = CalendarScheduleColorEnum.YoyakuKyokaHonyoyaku.MainColor;
            this.Other2Label.BackColor = CalendarScheduleColorEnum.Other.MainColor;
        }
    }
}
