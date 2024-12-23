using DevPlan.UICommon.Enum;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 凡例フォーム。
    /// </summary>
    /// <remarks>
    /// 凡例を表示するフォームです。
    /// 独自に凡例を追加で表示する必要がある場合は継承してください。
    /// </remarks>
    public partial class LegendForm : Form
    {
        /// <summary>
        /// マウス位置保持フィールド。
        /// </summary>
        private Point mousePoint;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// 画面初期化を行います。
        /// </remarks>
        public LegendForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// マウスダウンイベント。
        /// </summary>
        /// <remarks>
        /// 左クリックの場合、マウスの位置を保持します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegendForm_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mousePoint = new Point(e.X, e.Y);
            }
        }

        /// <summary>
        /// マウス移動イベント。
        /// </summary>
        /// <remarks>
        /// 左クリックによる移動の場合、現在のフォームの位置を移動します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegendForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        /// <summary>
        /// フォーム初期表示イベント。
        /// </summary>
        /// <remarks>
        /// フォーム初期表示時、ラベルへ色の設定及びコントロールへ
        /// イベントを設定します。
        /// （フォームドラッグアンドドロップによる移動処理で使ってます）
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegendForm_Shown(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                c.MouseMove += LegendForm_MouseMove;
                c.MouseDown += LegendForm_MouseDown;

                foreach (Control c2 in c.Controls)
                {
                    c2.MouseMove += LegendForm_MouseMove;
                    c2.MouseDown += LegendForm_MouseDown;
                }
            }

            // 外製車日程
            if (this.Name.Contains("OuterCarForm"))
            {
                this.SagyoKanryoLabel.Visible = false;
                this.SagyoKanryoNameLabel.Visible = false;

                // フォームの高さ調整
                this.Height -= 30;
            }

            this.YoyakuKyokaLabel.ForeColor = CalendarScheduleColorEnum.TentativeReservationColor.MainColor;
            this.YoyakuHuyouLabel.ForeColor = CalendarScheduleColorEnum.NoneReservationColor.MainColor;

            this.SagyoKanryoLabel.BackColor = CalendarScheduleColorEnum.CloseColor.MainColor;
        }
    }
}
