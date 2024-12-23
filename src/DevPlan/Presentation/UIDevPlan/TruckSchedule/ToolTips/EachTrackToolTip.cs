using DevPlan.UICommon.Dto;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule.ToolTips
{
    /// <summary>
    /// 通常便ツールチップ
    /// </summary>
    public class EachTrackToolTip : DevPlanToolTip
    {
        /// <summary>
        /// 表示対象データ
        /// </summary>
        private TruckScheduleModel data;

        /// <summary>
        /// 空荷文言
        /// </summary>
        private string kara;

        /// <summary>
        /// 機密車文言
        /// </summary>
        private const string KIMITU = "【機密車】";

        /// <summary>
        /// 空き時間文言
        /// </summary>
        private const string AKI = "【空き時間あり】";

        /// <summary>
        /// 空荷文言サイズ
        /// </summary>
        SizeF stringSizeKarani;

        /// <summary>
        /// 機密車文言サイズ
        /// </summary>
        SizeF stringSizeKimitu;

        /// <summary>
        /// 空き時間文言サイズ
        /// </summary>
        SizeF stringSizeAki;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="schedule"></param>
        public EachTrackToolTip(TruckScheduleModel schedule) : base(400)
        {
            this.data = schedule;
            base.s = @"[予約者]{0}
[運転者A]{1}
[発着地]{2}
[利用時間]{3}

[使用目的]{4}
";

            string section = "";
            if (schedule.SectionList != null)
            {
                foreach (var item in schedule.SectionList)
                {
                    section += (!string.IsNullOrWhiteSpace(section) ? "～" : "") + item.発着地;
                }

                for (int j = 0; j < schedule.SectionList.Count; j++)
                {
                    if (schedule.SectionList[j].FLAG_空荷 == 1)
                    {
                        kara += "【空荷】→" + schedule.SectionList[j + 1].発着地 + Environment.NewLine;
                    }
                }

                // あらかじめ長さを確定させておく（ツールチップの高さは描画前に決定）
                this.mainContentHoseiHeightCount = 0;
                using (var checkCanvas = new Bitmap(1, 1))
                using (var g = Graphics.FromImage(checkCanvas))
                {
                    if (string.IsNullOrWhiteSpace(kara) == false)
                    {
                        stringSizeKarani = g.MeasureString(kara, font);
                    }

                    if (schedule.FLAG_機密車 == 1)
                    {
                        stringSizeKimitu = g.MeasureString(KIMITU, font);
                    }

                    if (string.IsNullOrWhiteSpace(schedule.空き時間状況) == false)
                    {
                        stringSizeAki = g.MeasureString(AKI, font);
                    }
                }

                if (stringSizeKarani.Height <= (stringSizeKimitu.Height + stringSizeAki.Height))
                {
                    this.mainContentHoseiHeightCount = (int)stringSizeKimitu.Height + (int)stringSizeAki.Height;
                }
                else if (stringSizeKarani.Height > (stringSizeKimitu.Height + stringSizeAki.Height))
                {
                    this.mainContentHoseiHeightCount = (int)stringSizeKarani.Height;
                }
                else
                {
                    this.mainContentHoseiHeightCount = 0;
                }
            }

            if (schedule.予約開始時間 != null && schedule.予約終了時間 != null)
            {
                base.s = string.Format(base.s, schedule.予約者名, schedule.運転者A名 + "(" + schedule.運転者A_TEL + ")", section,
                    schedule.予約開始時間.Value.ToString("MM/dd HH:ss") + " ～ " + schedule.予約終了時間.Value.ToString("MM/dd HH:ss"), schedule.使用目的);
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

            if (string.IsNullOrWhiteSpace(kara) == false)
            {
                e.Graphics.DrawString(kara, font, Brushes.Blue,
                    new Rectangle(new Point(3, 5), new Size((int)stringSizeKarani.Width + 5, (int)stringSizeKarani.Height)));
            }

            var x = 3;
            if (string.IsNullOrWhiteSpace(kara) == false)
            {
                x = (int)stringSizeKarani.Width + 5;
            }

            if (this.data.FLAG_機密車 == 1)
            {
                e.Graphics.DrawString(KIMITU, font, Brushes.Red,
                new Rectangle(new Point(x, 5), new Size((int)stringSizeKimitu.Width + 5, (int)stringSizeKimitu.Height)));
            }

            if (string.IsNullOrWhiteSpace(this.data.空き時間状況) == false)
            {
                int y = 5;
                
                if (this.data.FLAG_機密車 == 1)
                {
                    y = (int)stringSizeKimitu.Height + 5;
                }

                e.Graphics.DrawString(AKI, font, Brushes.Blue,
                new Rectangle(new Point(x, y), new Size((int)stringSizeAki.Width + 5, (int)stringSizeAki.Height)));
            }
        }
    }
}
