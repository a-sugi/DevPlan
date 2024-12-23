using DevPlan.Presentation.Base;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using System;
using System.Windows.Forms;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 権限解除注意喚起画面
    /// </summary>
    public partial class BrowsingAuthorityLossAlertForm : BaseSubForm
    {
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "権限解除注意喚起"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>権限名</summary>
        public string AuthName { get; set; } = "他部署情報閲覧権限";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// 画面の初期化を行います。
        /// </remarks>
        public BrowsingAuthorityLossAlertForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowsingAuthorityLossAlertForm_Load(object sender, EventArgs e)
        {
            // 画面の初期化
            this.InitForm();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        public void InitForm()
        {
            // メッセージ文字列置換
            this.MessageLabel.Text = string.Format(this.MessageLabel.Text, AuthName);
        }

        /// <summary>
        /// フォームアンロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowsingAuthorityLossAlertForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.NotMessageCheckBox.Checked)
            {
                // メッセージ非表示フラグの更新
                this.EntryBrowsingAuthorityStatus();
            }
        }
        
        /// <summary>
        /// メッセージ非表示フラグの更新
        /// </summary>
        /// <returns>実行結果</returns>
        private bool EntryBrowsingAuthorityStatus()
        {
            var val = new BrowsingAuthorityStatusPutModel
            {
                PERSONEL_ID = SessionDto.UserId,
                メッセージ非表示 = 1
            };

            // Put実行
            var res = HttpUtil.PutResponse<BrowsingAuthorityStatusPutModel>(ControllerType.BrowsingAuthorityStatus, val);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            return true;
        }
    }
}
