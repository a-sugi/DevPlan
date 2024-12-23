using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.UITestCar.Othe.ApplicationApprovalCar
{
    /// <summary>
    /// 廃却申請
    /// </summary>
    public partial class DisposalApplicationForm : BaseSubForm
    {
        #region メンバ変数
        private const string HaikyakuSinsei = "廃却申請";
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "廃却申請"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        public ApplicationApprovalCarModel TestCar { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DisposalApplicationForm()
        {
            InitializeComponent();

        }
        #endregion

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisposalApplicationForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //管理票NO
            this.ManagementNoLabel.Text = this.TestCar.管理票NO;

            //実走行距離
            this.RunningDistanceTextBox.Text = this.TestCar.実走行距離;
            this.ActiveControl = this.RunningDistanceTextBox;

        }
        #endregion

        #region OKボタンクリック
        /// <summary>
        /// OKボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;

            }

            //試験内容
            this.TestCar.試験内容 = HaikyakuSinsei;

            //実走行距離
            this.TestCar.実走行距離 = this.RunningDistanceTextBox.Text;

            //移管先部署ID
            this.TestCar.移管先部署ID = null;

            //フォームクローズ
            this.FormOkClose();

        }
        #endregion
    }
}
