using DevPlan.Presentation.Base;
using System;
using System.Windows.Forms;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 仮予約注意喚起画面
    /// </summary>
    public partial class ReservationAlertForm : BaseSubForm
    {
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "仮予約です"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// 画面の初期化を行います。
        /// </remarks>
        public ReservationAlertForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OKボタン押下処理。
        /// </summary>
        /// <remarks>
        /// DialogResultへOkを設定し、ウィンドウを閉じます。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
