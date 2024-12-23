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
    /// 日程表ツールチップカスタマイズクラス
    /// </summary>
    public class DevPlanToolTip : ToolTip
    {
        /// <summary>
        /// フォント情報
        /// </summary>
        protected Font font;

        /// <summary>
        /// ツールチップ横幅サイズ
        /// </summary>
        private int width;

        /// <summary>
        /// ツールチップ縦幅サイズ
        /// </summary>
        private int height;

        /// <summary>
        /// ツールチップ表示文言
        /// </summary>
        protected string s;

        /// <summary>
        /// メイン文章描画位置
        /// </summary>
        protected int mainContentHoseiHeightCount;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="toolTipWidth">ツールチップ横幅サイズ</param>
        public DevPlanToolTip(int toolTipWidth)
        {
            this.OwnerDraw = true;
            this.Draw += new DrawToolTipEventHandler(DevPlanToolTip_Draw);
            this.Popup += new PopupEventHandler(DevPlanToolTip_Popup);

            this.width = toolTipWidth;
            this.font = new Font("ＭＳ ゴシック", 9);
        }

        /// <summary>
        /// ツールヒント描画前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevPlanToolTip_Popup(object sender, PopupEventArgs e)
        {
            int w;
            using (var checkCanvas = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(checkCanvas))
            {
                var orgWidth = (int)g.MeasureString(this.s, this.font).Width;

                if (orgWidth < this.width)
                {
                    w = orgWidth;
                }
                else
                {
                    w = this.width;
                }

                height = (int)g.MeasureString(this.s, this.font, w, StringFormat.GenericDefault).Height;
            }

            e.ToolTipSize = new Size(w + 10, height + 5 + mainContentHoseiHeightCount);//+30は補正
        }

        /// <summary>
        /// ツールヒント描画処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void DevPlanToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();

            Rectangle rect = new Rectangle(new Point(4, 5 + mainContentHoseiHeightCount),
                new Size(this.width, this.height + mainContentHoseiHeightCount));

            e.Graphics.DrawString(this.s, font, Brushes.Black, rect);
        }
    }
}
