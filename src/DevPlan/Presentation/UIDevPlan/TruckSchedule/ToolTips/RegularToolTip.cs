using DevPlan.UICommon.Dto;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule.ToolTips
{
    /// <summary>
    /// 定期便スケジュールツールチップ
    /// </summary>
    public class RegularToolTip : DevPlanToolTip
    {
        /// <summary>
        /// 表示対象データ
        /// </summary>
        private TruckScheduleModel data;

        /// <summary>
        /// 機密車文言
        /// </summary>
        private const string KIMITU = "【機密車立会い】";

        /// <summary>
        /// 機密車文言サイズ
        /// </summary>
        SizeF stringSizeKimitu;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="schedule">表示対象スケジュール</param>
        /// <param name="departurePointList">定期便発着地マスタ情報</param>
        public RegularToolTip(TruckScheduleModel schedule, List<DeparturePoint> departurePointList) : base(410)
        {
            this.data = schedule;

            base.s = @"[予約者]{0}
[依頼者]{1}
[発着]{2}
[利用時間]{3}

[発送者]{4}
[受領者]{5}
[搬送車両名]{6}
[備考]{7}
";
            this.mainContentHoseiHeightCount = 0;
            using (var checkCanvas = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(checkCanvas))
            {
                if (schedule.FLAG_機密車 == 1)
                {
                    stringSizeKimitu = g.MeasureString(KIMITU, font);
                }
            }
            this.mainContentHoseiHeightCount = (int)stringSizeKimitu.Height;

            string hassousya = "";
            string juryousya = "";
            if(schedule.ShipperRecipientUserList != null)
            {
                foreach (var item in schedule.ShipperRecipientUserList)
                {
                    if (string.IsNullOrWhiteSpace(item.発送者名) == false)
                    {
                        hassousya += item.発送者名 + "(" + item.発送者_TEL + ") ";
                    }
                    if (string.IsNullOrWhiteSpace(item.受領者名) == false)
                    {
                        juryousya += item.受領者名 + "(" + item.受領者_TEL + ") ";
                    }
                }
            }

            if(schedule.予約開始時間 != null)
            {
                string time =
                    schedule.予約開始時間.Value.ToString("MM/dd(ddd) " + schedule.DEPARTURE_TIME + "発");

                var departurePoint = departurePointList.Where(x => x.TruckId == schedule.トラック_ID && x.DepartureTime == schedule.DEPARTURE_TIME);
                if (departurePoint.FirstOrDefault() != null)
                {
                    base.s = string.Format(base.s, schedule.予約者名, schedule.定期便依頼者名 + "(" + schedule.定期便依頼者_TEL + ")",
                        departurePoint.FirstOrDefault().Text3,
                        time, hassousya, juryousya, schedule.搬送車両名, schedule.備考);
                }
            }
        }

        /// <summary>
        /// ツールヒント描画処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void DevPlanToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            base.DevPlanToolTip_Draw(sender, e);
            
            if (this.data.FLAG_機密車 == 1)
            {
                var x = 3;
                e.Graphics.DrawString(KIMITU, font, Brushes.Red,
                new Rectangle(new Point(x, 5), new Size((int)stringSizeKimitu.Width + 10, (int)stringSizeKimitu.Height)));
            }
        }
    }
}
