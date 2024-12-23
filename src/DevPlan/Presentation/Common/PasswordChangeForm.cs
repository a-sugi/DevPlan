using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// パスワード変更
    /// </summary>
    public partial class PasswordChangeForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "パスワード変更"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>ログインID</summary>
        public string LoginID { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PasswordChangeForm()
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
        private void PasswordChangeForm_Load(object sender, EventArgs e)
        {
            // 初期表示フォーカス
            this.ActiveControl = this.PasswordTextBox;
        }
        #endregion

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            // 入力チェック
            if (this.IsEntryPasswordChange())
            {
                // 入力データの登録
                FormControlUtil.FormWait(this, this.EntryPasswordChange);
            }
        }
        #endregion

        #region 登録データのチェック
        /// <summary>
        /// 登録データのチェック
        /// </summary>
        /// <returns>チェック可否</returns>
        private bool IsEntryPasswordChange()
        {
            var error = new List<string>();

            Func<string, bool> isHalf = str =>
            {
                return Encoding.GetEncoding("Shift_JIS").GetByteCount(str) == str.Length;
            };

            // 古いパスワード
            if (string.IsNullOrWhiteSpace(this.PasswordTextBox.Text))
            {
                // 未入力
                error.Add(string.Format(Resources.KKM00001, "古いパスワード"));
            }
            else
            {
                // ログイン認証
                var login = GetLogin();

                if (login == null || login.Password != this.PasswordTextBox.Text)
                {
                    error.Add(string.Format(Resources.KKM03038, "古いパスワード"));
                }
            }

            // 新しいパスワード
            if (string.IsNullOrWhiteSpace(this.NewPasswordTextBox.Text))
            {
                // 未入力
                error.Add(Resources.KKM03013.Replace("パスワード", "新しいパスワード"));
            }
            else if (!isHalf(this.NewPasswordTextBox.Text))
            {
                // 書式不正(半角以外)
                error.Add(string.Format(Resources.KKM00032, "新しいパスワード"));
            }
            else if (this.NewPasswordTextBox.Text.Length < 8 || this.NewPasswordTextBox.Text.Length > 13)
            {
                // 桁数不正
                error.Add(Resources.KKM03013.Replace("パスワード", "新しいパスワード"));
            }

            // 新しいパスワード(確認)
            if (this.NewPasswordTextBox.Text != this.NewPasswordConfTextBox.Text)
            {
                error.Add(string.Format(Resources.KKM03038, "新しいパスワード(確認)"));
            }

            if (error.Count() > 0)
            {
                Messenger.Warn(string.Join(Const.CrLf, error));
                return false;
            }

            return true;
        }
        #endregion

        #region データの取得
        /// <summary>
        /// ログイン認証
        /// </summary>
        private LoginOutModel GetLogin()
        {
            // パラメータ設定
            var cond = new LoginInModel()
            {
                LoginID = this.LoginID?.ToUpper(),
            };

            // APIで取得
            var res = HttpUtil.PostResponse<LoginOutModel>(ControllerType.Login, cond, false);

            return res.Results?.FirstOrDefault();
        }
        #endregion

        #region データの更新
        /// <summary>
        /// パスワード変更
        /// </summary>
        private void EntryPasswordChange()
        {
            var val = new LoginPasswordChangeModel()
            {
                LOGIN_ID = this.LoginID?.ToUpper(),
                PASSWORD = this.PasswordTextBox.Text,
                NEW_PASSWORD = this.NewPasswordTextBox.Text
            };

            // 更新
            var res = HttpUtil.PutResponse<LoginPasswordChangeModel>(ControllerType.Login, val);

            // レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                // 登録後メッセージ
                Messenger.Info(Resources.KKM00002);

                // フォームクローズ
                base.FormOkClose();
            }
        }
        #endregion
    }
}
