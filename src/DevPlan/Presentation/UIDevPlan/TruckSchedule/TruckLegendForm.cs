using DevPlan.Presentation.Common;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// トラック予約凡例画面
    /// </summary>
    public partial class TruckLegendForm : LegendForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TruckLegendForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォーム表示後処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            Point[] ps = {
                new Point(5, 2),
                new Point(90, 2),
                new Point(95, 20),
                new Point(90, 40),
                new Point(5, 40),
                new Point(0, 20),
                new Point(5, 2)};

            this.NormalColorPanel.BackColor = CalendarScheduleColorEnum.TruckNormalColor.MainColor;
            SetHonColor(ps, NormalColorBox1, CalendarScheduleColorEnum.DefaultColor);
            
            int itemLocationX = 32;
            int scheduleLocationX = 112;
            int scheduleLocationY = 45;
            int count = 0;

            var headerRes = HttpUtil.GetResponse<TruckRegularTimeModel>(ControllerType.TruckRegularTime);
            var regularTimeList = new List<TruckRegularTimeModel>();
            if (headerRes != null && headerRes.Status == Const.StatusSuccess)
            {
                regularTimeList.AddRange(headerRes.Results);
            }
            var colorList = regularTimeList.GroupBy(x => x.REGULAR_ID);

            foreach (var data in colorList)
            {
                var item = data.ElementAt(0);
                var colorKey = CalendarScheduleColorEnum.GetValues(item.ItemColor, item.FontColor);

                this.TeikiItemPanel.Controls.Add(new Panel()
                {
                    BackColor = colorKey.MainColor,
                    BorderStyle = BorderStyle.FixedSingle,
                    Size = new Size(26, 26),
                    Location = new Point(3 + (itemLocationX * count), 3)
                });

                {
                    var picBox = new PictureBox()
                    {
                        Size = new Size(111, 44),
                        Location = new Point(3 + (scheduleLocationX * count), 3)
                    };
                    SetKariColor(ps, picBox, colorKey);
                    this.TeikiPanel.Controls.Add(picBox);
                }

                {
                    var picBox = new PictureBox()
                    {
                        Size = new Size(111, 44),
                        Location = new Point(3 + (scheduleLocationX * count), 3 + (scheduleLocationY))
                    };
                    this.TeikiPanel.Controls.Add(picBox);
                    SetHonColor(ps, picBox, colorKey);
                    this.TeikiPanel.Controls.Add(picBox);
                }

                count += 1;
            }            
        }

        /// <summary>
        /// 本予約カラーセット
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="colorBox"></param>
        private void SetHonColor(Point[] ps, PictureBox colorBox, CalendarScheduleColorEnum color)
        {
            Pen honPen = new Pen(Color.Red, 3);

            Bitmap canvas = new Bitmap(colorBox.Width, colorBox.Height);
            Graphics g = Graphics.FromImage(canvas);

            g.FillPolygon(new SolidBrush(color.MainColor), ps);
            g.DrawPolygon(honPen, ps);
            g.Dispose();

            colorBox.Image = canvas;
            colorBox.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        /// <summary>
        /// 仮予約カラーセット
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="colorBox"></param>
        /// <param name="color"></param>
        private void SetKariColor(Point[] ps, PictureBox colorBox, CalendarScheduleColorEnum color)
        {
            Bitmap canvas = new Bitmap(colorBox.Width, colorBox.Height);
            Graphics g = Graphics.FromImage(canvas);

            g.FillPolygon(new SolidBrush(color.MainColor), ps);
            g.DrawPolygon(new Pen(Color.Black, 1), ps);
            g.DrawString("(仮)", ControlFont.DefaultFont.Font, new SolidBrush(color.FontColor), new Point(5, 15));
            g.Dispose();

            colorBox.Image = canvas;
            colorBox.SizeMode = PictureBoxSizeMode.AutoSize;
        }
    }
}
