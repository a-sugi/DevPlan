using System;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using log4net;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.Common;
using System.Collections.Generic;
using DevPlan.Presentation.Base;

namespace DevPlan.Presentation
{
    /// <summary>
    /// LoginForm - ログイン
    /// </summary>
    public partial class LoginForm : Form
    {
        #region メンバ変数
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly int[] NoneId = (new[] { FunctionID.Login, FunctionID.FunctionAuthority, FunctionID.Notice }).Select(x => (int)x).ToArray();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        /// <summary>
        /// フォーム表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            // 自動ログインが無効な場合
            if (!Properties.Settings.Default.AutoLogin)
            {
                // 最前化
                this.TopMost = true;
                this.TopMost = false;
            }
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            var SystemName = Properties.Resources.SystemName;
            var s = SystemName + " ";


#if DEBUG
            var color = Color.FromArgb(255, 230, 255);
            this.Text += " (Trial)";
            this.BackColor = color;
#endif

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                s += "Ver " + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                s += string.Format("Ver {0}", asm.GetName().Version);
            }
            SystemNameLabel.Text = s;


            //項目初期化
            this.UserIDTextBox.Text = string.Empty;
            this.PasswordTextBox.Text = string.Empty;

            // 自動ログインが有効な場合
            if (Properties.Settings.Default.AutoLogin)
            {
                if (LoginLogic(Decrypt(Properties.Settings.Default.UserID, "KONTENA"),
                    Decrypt(Properties.Settings.Default.Password, "KONTENA")))
                {
                    ShowMainMenu();
                }
            }
            else
            {
                this.Activate();

                this.UserIDTextBox.Text = Environment.UserName;
                this.ActiveControl = PasswordTextBox;
            }
        }

        /// <summary>
        /// ログインボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, EventArgs e)
        {
            var flg = false;

            FormControlUtil.FormWait(this, () => flg = LoginLogic(UserIDTextBox.Text, PasswordTextBox.Text));

            if (flg == true)
            {
                ShowMainMenu();
            }
        }

        /// <summary>
        /// パスワードの変更ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordChangeButton_Click(object sender, EventArgs e)
        {
            var flg = false;

            FormControlUtil.FormWait(this, () => flg = PasswordChangeStartUpLogic(UserIDTextBox.Text));

            if (!flg) return;

            using (var frm = new PasswordChangeForm() { LoginID = this.UserIDTextBox.Text })
            {
                frm.ShowDialog(this);
            }
        }

        /// <summary>
        /// メインメニューの表示
        /// </summary>
        private void ShowMainMenu()
        {
            using (MenuForm frm = new MenuForm())
            {
                this.Hide();
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        private bool LoginLogic(string LoginID, string password)
        {
            #region <<< 入力チェック >>>
            var inputerror = new List<string>();

            // ユーザーID
            if (string.IsNullOrWhiteSpace(LoginID))
            {
                // 未入力
                inputerror.Add(Resources.KKM03014);
            }
            else if (LoginID.Length < 1 || LoginID.Length > 50)
            {
                // 桁数不正
                inputerror.Add(Resources.KKM03014);
            }
            // パスワード
            if (string.IsNullOrWhiteSpace(password))
            {
                // 未入力
                inputerror.Add(Resources.KKM03013);
            }
            else if (password.Length < 8 || password.Length > 13)
            {
                // 桁数不正
                inputerror.Add(Resources.KKM03013);
            }

            if (inputerror.Count() > 0)
            {
                Messenger.Warn(string.Join(Const.CrLf, inputerror));
                return false;
            }
            #endregion

            #region <<< APIサーバにログイン認証の問いあわせ >>>
            // Postメソッド発行用パラメータ
            var cond = new LoginInModel
            {
                LoginID = LoginID?.ToUpper(),
                InputPassword = password
            };

            // ログイン認証
            var loginResult = HttpUtil.PostResponse<LoginOutModel>(ControllerType.Login, cond, false);

            // 認証失敗で処理終了
            if (loginResult.Status != Const.StatusSuccess)
            {
                // ログイン認証NGの場合
                if (loginResult.ErrorCode == ApiMessageType.KKE03021.ToString())
                {
                    Messenger.Warn(Resources.KKM03001);
                    return false;
                }
                // ネットワーク障害系エラーの場合
                else if (loginResult.Status == Const.StatusFailure && string.IsNullOrEmpty(loginResult.ErrorMessage))
                {
                    Messenger.Warn(Resources.KKM00037);
                    return false;
                }
                // システムエラー発生時
                else
                {
                    Messenger.Warn(Resources.KKM03000);
                    return false;
                }

            }
            #endregion

            var isPassword = false;

            // 取得できたユーザ情報を共通プロパティに設定
            var login = loginResult.Results.FirstOrDefault();
            if (login != null)
            {
                SessionDto.UserName = login.UserName;
                SessionDto.UserId = login.PersonelID;
                SessionDto.AccessLevel = login.AccessLevel;
                isPassword = password == login.Password;
            }

            // パスワード不一致（処理終了）
            if (isPassword == false)
            {
                Messenger.Warn(Resources.KKM03001);
                return false;
            }

            // ユーザ検索API Getメソッドに渡すパラメータ
            var itemcond = new UserSearchModel
            {
                PERSONEL_ID = SessionDto.UserId,
                MANAGE_FLG = true
            };

            // ユーザー情報取得
            var userInfoResult = HttpUtil.GetResponse<UserSearchModel, UserSearchOutModel>(ControllerType.User, itemcond, false);

            // ユーザー情報取得の取得失敗（処理終了）
            if (userInfoResult.Status != Const.StatusSuccess || userInfoResult.Results == null || userInfoResult.Results.Any() == false)
            {
                Messenger.Warn(Resources.KKM03001);
                return false;
            }

            var user = userInfoResult.Results.First();

            // ログインセッション情報の格納
            SessionDto.SectionGroupID = user.SECTION_GROUP_ID;
            SessionDto.OfficialPosition = user.OFFICIAL_POSITION;
            SessionDto.SectionGroupCode = user.SECTION_GROUP_CODE;
            SessionDto.SectionGroupName = user.SECTION_GROUP_NAME;
            SessionDto.SectionID = user.SECTION_ID;
            SessionDto.SectionCode = user.SECTION_CODE;
            SessionDto.SectionName = user.SECTION_NAME;
            SessionDto.DepartmentID = user.DEPARTMENT_ID;
            SessionDto.DepartmentCode = user.DEPARTMENT_CODE;
            SessionDto.DepartmentName = user.DEPARTMENT_NAME;
            SessionDto.KenjitsuFlag = user.FLAG_KENJITSU;
            SessionDto.Affiliation = user.ESTABLISHMENT ?? "";
            SessionDto.ManagementDepartmentType = user.管理部署種別;

            // ログイン権限の問い合わせ
            var functionAuthCond = new UserAuthorityInModel
            {
                DEPARTMENT_ID = SessionDto.DepartmentID,
                SECTION_ID = SessionDto.SectionID,
                SECTION_GROUP_ID = SessionDto.SectionGroupID,
                PERSONEL_ID = SessionDto.UserId,
                OFFICIAL_POSITION = SessionDto.OfficialPosition
            };

            // 権限情報の取得
            var functionAuthResult = HttpUtil.GetResponse<UserAuthorityInModel, UserAuthorityOutModel>(ControllerType.UserAuthority, functionAuthCond, false);

            // 権限情報の取得失敗（処理終了）
            if (functionAuthResult.Status != Const.StatusSuccess || functionAuthResult.Results.Any(x => x.FUNCTION_ID == (int)FunctionID.Login && x.READ_FLG == '1') == false)
            {
                //ログイン認証OKでログイン認証権限が付与されていない場合
                Messenger.Warn(Resources.KKM03002);
                return false;
            }

            // ログインセッション情報の格納（ユーザー権限）
            SessionDto.UserAuthorityList = functionAuthResult.Results.Select(x => { x.ROLL_ID_LIST = x.ROLL_IDS.Split(',').ToList(); return x; }).ToList();

            // 自動ログインの設定
            if (AutoLoginCheckBox.Checked)
            {
                SetAutoLogin(AutoLoginCheckBox.Checked, UserIDTextBox.Text, PasswordTextBox.Text);
            }

            // 全閲覧権限なし
            if (SessionDto.UserAuthorityList.Any(x => x.FUNCTION_ID == (int)FunctionID.TestCar && x.ALL_GENERAL_CODE_FLG != '1'))
            {
                var browsingAuthCond = new BrowsingAuthorityStatusInModel
                {
                    PERSONEL_ID = SessionDto.UserId
                };

                var BrowsingAuthResult = HttpUtil.GetResponse<BrowsingAuthorityStatusInModel, BrowsingAuthorityStatusOutModel>(ControllerType.BrowsingAuthorityStatus, browsingAuthCond, false);

                if (BrowsingAuthResult.Status == Const.StatusSuccess && BrowsingAuthResult.Results.Any(x => x.メッセージ非表示 != 1 && x.他部署閲覧権限 == 1))
                {
                    // 権限解除注意喚起画面の表示
                    using (var form = new BrowsingAuthorityLossAlertForm())
                    {
                        form.ShowDialog(this);
                    }
                }
            }

            // ログインセッション情報の格納（セッション管理）
            SetSessionInfo(LoginID, password);

            // 操作ログ登録
            new BaseForm().SetOperationLog(FunctionID.Login);

            // 全ての処理が成功したら、Trueを返却
            return true;
        }

        /// <summary>
        /// 自動ログイン設定
        /// </summary>
        /// <param name="flg">自動ログイン設定フラグ</param>
        /// <param name="id">ユーザーID</param>
        /// <param name="pass">パスワード</param>
        public void SetAutoLogin(bool flg, string id, string pass)
        {
            var publicKey = CreateKeys("KONTENA");

            Properties.Settings.Default.AutoLogin = flg;
            Properties.Settings.Default.UserID = flg ? Encrypt(id, publicKey) : string.Empty;
            Properties.Settings.Default.Password = flg ? Encrypt(pass, publicKey) : string.Empty;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 自動ログイン設定
        /// </summary>
        /// <param name="flg">自動ログイン設定フラグ</param>
        /// <param name="id">ユーザーID</param>
        /// <param name="pass">パスワード</param>
        private void SetSessionInfo(string id, string pass)
        {
            var publicKey = CreateKeys("SESSION");

            SessionDto.LoginId = Encrypt(id, publicKey);
            SessionDto.Password = Encrypt(pass, publicKey);
            SessionDto.LoginDateTime = SessionDto.ActiveDateTime = DateTime.Now;
        }

        /// <summary>
        /// パスワード変更開始処理
        /// </summary>
        private bool PasswordChangeStartUpLogic(string userID)
        {
            #region <<< 入力チェック >>>
            var inputerror = new List<string>();

            // ユーザーID
            if (string.IsNullOrWhiteSpace(userID))
            {
                // 未入力
                inputerror.Add(Resources.KKM03014);
            }
            else if (userID.Length < 1 || userID.Length > 50)
            {
                // 桁数不正
                inputerror.Add(Resources.KKM03014);
            }

            if (inputerror.Count() > 0)
            {
                Messenger.Warn(string.Join(Const.CrLf, inputerror));
                return false;
            }
            #endregion

            #region <<< APIサーバにログイン認証の問いあわせ >>>
            // Postメソッド発行用パラメータ
            var cond = new LoginInModel
            {
                LoginID = userID
            };

            // ログイン認証
            var loginResult = HttpUtil.PostResponse<LoginOutModel>(ControllerType.Login, cond, false);

            // 認証失敗で処理終了
            if (loginResult.Status != Const.StatusSuccess)
            {
                // ログイン認証NGの場合
                if (loginResult.ErrorCode == ApiMessageType.KKE03021.ToString())
                {
                    Messenger.Warn(string.Format(Resources.KKM03038, "ユーザーID"));
                    return false;
                }
                // ネットワーク障害系エラーの場合
                else if (loginResult.Status == Const.StatusFailure && string.IsNullOrEmpty(loginResult.ErrorMessage))
                {
                    Messenger.Warn(Resources.KKM00037);
                    return false;
                }
                // システムエラー発生時
                else
                {
                    Messenger.Warn(Resources.KKM03000);
                    return false;
                }
            }
            #endregion

            // 全ての処理が成功したら、Trueを返却
            return true;
        }

        #region <<< パスワード暗号化処理 >>>
        /// <summary>
        /// 公開鍵と秘密鍵を作成し、キーコンテナに格納する
        /// </summary>
        /// <param name="containerName">キーコンテナ名</param>
        /// <returns>作成された公開鍵(XML形式)</returns>
        public static string CreateKeys(string containerName)
        {
            //CspParametersオブジェクトの作成
            System.Security.Cryptography.CspParameters cp =
                new System.Security.Cryptography.CspParameters();
            //キーコンテナ名を指定する
            cp.KeyContainerName = containerName;
            //CspParametersを指定してRSACryptoServiceProviderオブジェクトを作成
            System.Security.Cryptography.RSACryptoServiceProvider rsa =
                new System.Security.Cryptography.RSACryptoServiceProvider(cp);

            //公開鍵をXML形式で取得して返す
            return rsa.ToXmlString(false);
        }

        /// <summary>
        /// 公開鍵を使って文字列を暗号化する
        /// </summary>
        /// <param name="str">暗号化する文字列</param>
        /// <param name="publicKey">暗号化に使用する公開鍵(XML形式)</param>
        /// <returns>暗号化された文字列</returns>
        public static string Encrypt(string str, string publicKey)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa =
                new System.Security.Cryptography.RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] encryptedData = rsa.Encrypt(data, false);
            return System.Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// キーコンテナに格納された秘密鍵を使って、文字列を復号化する
        /// </summary>
        /// <param name="str">Encryptメソッドにより暗号化された文字列</param>
        /// <param name="containerName">キーコンテナ名</param>
        /// <returns>復号化された文字列</returns>
        public string Decrypt(string str, string containerName)
        {
            //CspParametersオブジェクトの作成
            System.Security.Cryptography.CspParameters cp =
                new System.Security.Cryptography.CspParameters();
            //キーコンテナ名を指定する
            cp.KeyContainerName = containerName;
            //CspParametersを指定してRSACryptoServiceProviderオブジェクトを作成
            System.Security.Cryptography.RSACryptoServiceProvider rsa =
                new System.Security.Cryptography.RSACryptoServiceProvider(cp);

            //復号化する
            byte[] data = System.Convert.FromBase64String(str);
            byte[] decryptedData = rsa.Decrypt(data, false);
            return System.Text.Encoding.UTF8.GetString(decryptedData);
        }

        /// <summary>
        /// 指定されたキーコンテナを削除する
        /// </summary>
        /// <param name="containerName">キーコンテナ名</param>
        public static void DeleteKeys(string containerName)
        {
            //CspParametersオブジェクトの作成
            System.Security.Cryptography.CspParameters cp =
                new System.Security.Cryptography.CspParameters();
            //キーコンテナ名を指定する
            cp.KeyContainerName = containerName;
            //CspParametersを指定してRSACryptoServiceProviderオブジェクトを作成
            System.Security.Cryptography.RSACryptoServiceProvider rsa =
                new System.Security.Cryptography.RSACryptoServiceProvider(cp);

            //キーコンテナを削除
            rsa.PersistKeyInCsp = false;
            rsa.Clear();
        }


        /// <summary>
        /// ハッシュ値の取得
        /// </summary>
        /// <param name="SourceText"></param>
        /// <returns></returns>
        private string GetHashCode(string SourceText)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(SourceText);
            System.Security.Cryptography.SHA256CryptoServiceProvider SHA =
                new System.Security.Cryptography.SHA256CryptoServiceProvider();
            byte[] bs = SHA.ComputeHash(data);
            SHA.Clear();

            System.Text.StringBuilder hashed = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                hashed.Append(b.ToString("x2"));
            }

            //結果を戻り値に設定
            string HashedText = Convert.ToString(hashed);

            return HashedText;
        }
        #endregion

        /// <summary>
        /// フォームクローズ(Cancel)
        /// </summary>
        protected void FormCancelClose()
        {
            this.FormClose(DialogResult.Cancel);

        }

        /// <summary>
        /// フォームクローズ
        /// </summary>
        /// <param name="result">ダイアログ結果</param>
        protected void FormClose(DialogResult result)
        {
            //ダイアログ結果
            this.DialogResult = result;

            this.Close();

        }

        /// <summary>
        /// 閉じるボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            //フォームクローズ(Cancel)
            this.FormCancelClose();
        }

        /// <summary>
        /// Handles the KeyPress event of the LoginForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Control c = this.ActiveControl;
                if ((c is TextBox && !((TextBox)c).Multiline) || c is MaskedTextBox || c is CheckBox || c is ComboBox)
                {
                    SendKeys.Send("{TAB}");
                    e.Handled = true;
                }
            }

        }
    }
}
