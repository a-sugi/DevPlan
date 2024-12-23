namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// ログイン認証モデルクラス（入力）
    /// </summary>
    public class LoginInModel
    {
        /// <summary>
        /// ドメイン名
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// ログインID(Active Directory認証用)
        /// </summary>
        public string LoginID { get; set; }

        /// <summary>
        /// パスワード(Active Directory認証用)
        /// </summary>
        public string InputPassword { get; set; }
    }

    /// <summary>
    /// ログイン認証モデルクラス（出力）
    /// </summary>
    public class LoginOutModel
    {
        /// <summary>
        /// ユーザー名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 社員コード(ログインIDとは異なる)
        /// </summary>
        public string PersonelID { get; set; }

        /// <summary>
        /// アクセスレベル
        /// </summary>
        public string AccessLevel { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 認証結果
        /// </summary>
        public string AuthResult { get; set; }

    }

    /// <summary>
    /// パスワード変更モデルクラス
    /// </summary>
    public class LoginPasswordChangeModel
    {
        /// <summary>
        /// ログインID
        /// </summary>
        public string LOGIN_ID { get; set; }

        /// <summary>
        /// パスワード(現在)
        /// </summary>
        public string PASSWORD { get; set; }

        /// <summary>
        /// パスワード(新規)
        /// </summary>
        public string NEW_PASSWORD { get; set; }
    }
}