using DevPlan.UICommon.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule.Reports
{
    /// <summary>
    /// トラック予約印刷抽象基底クラス
    /// </summary>
    public abstract class BaseReports
    {
        /// <summary>
        /// 印刷用フォント生成ヘルパメソッド
        /// </summary>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        protected Font GetPrintFont(int fontSize)
        {
            return new Font("ＭＳ ゴシック", fontSize, FontStyle.Regular, GraphicsUnit.Point);
        }

        /// <summary>
        /// フッター印刷ヘルパメソッド
        /// </summary>
        /// <param name="e"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void SetFooter(PrintPageEventArgs e, int x, int y)
        {
            StringFormat footerFormat = new StringFormat();
            footerFormat.Alignment = StringAlignment.Far;

            e.Graphics.DrawString("社外転用禁止 " + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "　" +
                SessionDto.SectionCode + "　" + SessionDto.UserName,
                GetPrintFont(10), Brushes.Black, x, y, footerFormat);
        }
    }
}
