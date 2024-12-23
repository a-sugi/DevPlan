using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.UICommon.Properties;

namespace DevPlan.UICommon
{
    public class Messenger
    {
        #region メンバ変数
        private const string Question = "確認";
        private const string Err = "エラー";
        private const string Information = "お知らせ";
        private const string Warning = "警告";
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Messenger()
        {
        }

        /// <summary>
        /// 確認
        /// </summary>
        /// <param name="message"></param>
        /// <param name="defaultButton"></param>
        public static DialogResult Confirm(string message, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button2)
        {
            return MessageBox.Show(message, Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton);
        }

        /// <summary>
        /// 確認（OKキャンセル）
        /// </summary>
        /// <param name="message"></param>
        /// <param name="defaultButton"></param>
        public static DialogResult ConfirmOkCancel(string message, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button2)
        {
            return MessageBox.Show(message, Question, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, defaultButton);
        }

        //Append Start 2021/05/14 杉浦 CAP_登録確認子画面に「キャンセル」を追加
        /// <summary>
        /// 確認（YesNoキャンセル）
        /// </summary>
        /// <param name="message"></param>
        /// <param name="defaultButton"></param>
        public static DialogResult ConfirmYesNoCancel(string message, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button2)
        {
            return MessageBox.Show(message, Question, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, defaultButton);
        }
        //Append End 2021/05/14 杉浦 CAP_登録確認子画面に「キャンセル」を追加

        /// <summary>
        /// お知らせ
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// 
        public static void Info(string message)
        {
            ShowMessageBox(message, Information, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// 
        public static void Warn(string message)
        {
            ShowMessageBox(message, Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// エラー
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="exception">例外</param>
        /// 
        public static void Error(string message, Exception exception)
        {
            var msg = message;
            if (exception != null) msg = msg + "\r\n" + exception.Message;
            ShowMessageBox(message, Err, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 致命的エラー
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="exception">例外</param>
        /// 
        public static void Fatal(string message, Exception exception)
        {
            var msg = message;
            if (exception != null) msg = msg + "\r\n" + exception.Message;
            ShowMessageBox(msg, Err, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

        }

        /// <summary>
        /// メッセージボックス表示
        /// </summary>
        /// <param name="text">文言</param>
        /// <param name="caption">タイトル</param>
        /// <param name="button">ボタン</param>
        /// <param name="icon">アイコン</param>
        /// <param name="defaultButton">デフォルトボタン</param>
        /// 
        private static void ShowMessageBox(string text, string caption, MessageBoxButtons button, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            MessageBox.Show(text, caption, button, icon, defaultButton);

        }

    }
}
