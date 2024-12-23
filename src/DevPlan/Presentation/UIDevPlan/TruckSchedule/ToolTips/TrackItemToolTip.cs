using DevPlan.UICommon.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule.ToolTips
{
    /// <summary>
    /// トラック予約項目ツールチップ
    /// </summary>
    public class TrackItemToolTip : DevPlanToolTip
    {
        /// <summary>
        /// 表示対象スケジュール項目情報
        /// </summary>
        private TruckScheduleItemModel scheduleItem;

        /// <summary>
        /// 車両名文言
        /// </summary>
        private string syaryou;

        /// <summary>
        /// ディーゼル規制文言
        /// </summary>
        private string kisei;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="item">表示対象スケジュール項目</param>
        public TrackItemToolTip(TruckScheduleItemModel item) : base(900)
        {
            this.scheduleItem = item;

            syaryou = @"車両名：{0}

";
            kisei = @"ディーゼル規制：";

            base.s = syaryou + kisei + @"　　　　保管場所：{1}　　登録番号：{2}　種類：{3}

備考：
{4}

";
            base.s = string.Format(base.s,
                item.車両名,
                item.保管場所,
                item.登録番号,
                item.種類,
                item.備考);
        }

        /// <summary>
        /// ツールヒント描画処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void DevPlanToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            base.DevPlanToolTip_Draw(sender, e);
            
            string diesel = "";
            Brush brushe;
            SizeF rectSize;

            if (scheduleItem.FLAG_ディーゼル規制 == 0)
            {
                diesel = "不適合";
                brushe = Brushes.Red;
            }
            else
            {
                diesel = "適合";
                brushe = Brushes.Blue;                
            }

            rectSize = e.Graphics.MeasureString(diesel, font);
            SizeF stringSizeKisei = e.Graphics.MeasureString(kisei, font);
            SizeF stringSizeSyaryou = e.Graphics.MeasureString(syaryou, font);

            e.Graphics.FillRectangle(brushe,
                stringSizeKisei.Width,
                stringSizeSyaryou.Height + 2,
                rectSize.Width + 2,
                rectSize.Height + 2);

            Rectangle rect = new Rectangle(
                new Point((int)stringSizeKisei.Width + 3, (int)stringSizeSyaryou.Height + 4), 
                new Size((int)rectSize.Width + 10, (int)rectSize.Height));
            e.Graphics.DrawString(diesel, font, Brushes.White, rect);
        }
    }
}
