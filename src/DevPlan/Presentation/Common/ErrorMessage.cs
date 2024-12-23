using DevPlan.Presentation.Properties;
using DevPlan.UICommon;
using DevPlan.UICommon.Config;
using DevPlan.UICommon.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace DevPlan.Presentation.Common
{
    public partial class ErrorMessage : Form
    {
        internal Exception ex = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ErrorMessage()
        {
            InitializeComponent();
        }

        private void ErrorMessage_Load(object sender, EventArgs e)
        {
            //システムのエラーアイコン(WIN32: IDI_ERROR)
            Bitmap canvas = new Bitmap(PictureBox.Width, PictureBox.Height);
            Graphics g = Graphics.FromImage(canvas);
            g.DrawIcon(SystemIcons.Error, 0, 0);
            g.Dispose();
            PictureBox.Image = canvas;

            MessageLabel.Text = UICommon.Properties.Resources.KKM03000.Replace("管理者に連絡して下さい。", "「Report an Error」を押下し､ご連絡下さい｡");

            this.Text = Resources.SystemName;

            DetailTextBox.Visible = true;

            if (ex != null)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(String.Format("********** {0} **********", DateTime.Now));
                if (ex.InnerException != null)
                {
                    sb.Append("Inner Exception Type: ");
                    sb.AppendLine(ex.InnerException.GetType().ToString());
                    sb.Append("Inner Exception: ");
                    sb.AppendLine(ex.InnerException.Message);
                    sb.Append("Inner Source: ");
                    sb.AppendLine(ex.InnerException.Source);
                    if (ex.InnerException.StackTrace != null)
                    {
                        sb.AppendLine("Inner Stack Trace: ");
                        sb.AppendLine(ex.InnerException.StackTrace);
                    }
                }
                sb.Append("Exception Type: ");
                sb.AppendLine(ex.GetType().ToString());
                sb.AppendLine("Exception: " + ex.Message);
                sb.AppendLine("Source: " + ex.Source);
                sb.AppendLine("Stack Trace: ");
                if (ex.StackTrace != null)
                {
                    sb.AppendLine(ex.StackTrace);
                    sb.AppendLine();
                }

                DetailTextBox.Text = sb.ToString();
            }
                
        }

        private void DetailButton_Click(object sender, EventArgs e)
        {
            DetailTextBox.Visible = !DetailTextBox.Visible;

            if (DetailTextBox.Visible)
            {
                DetailButton.Text = "Hide";
                this.Height = this.Height + DetailTextBox.Size.Height;
            }
            else
            {
                DetailButton.Text = "Details";
                this.Height = this.Height - DetailTextBox.Size.Height;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "log_" + DateTime.Now.ToString("yyyyMMddHHmms");

            sfd.Filter = "zip file(*.zip)|*.zip";
            sfd.RestoreDirectory = true;
            sfd.OverwritePrompt = true;
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FormControlUtil.FormWait(this, () =>
                    {
                        // 1. 実行中のメイン・アセンブリのフル・パスを取得する
                        Assembly asm = Assembly.GetEntryAssembly();
                        string fullPath = asm.Location;

                        // 2. フル・パスからディレクトリ・パス部分を抽出する
                        string dirPath = Path.GetDirectoryName(fullPath) + @"/logs";


                        if (ZipUtil.FolderCompression(dirPath, sfd.FileName))
                            Messenger.Info("Export completed(FileName:" + sfd.FileName + ")");
                    });
                }
                catch
                {
                    // 未処理
                }
            }
        }

        private void ReportButton_Click(object sender, EventArgs e)
        {
            // アセンブリ情報
            var assembly = Assembly.GetEntryAssembly();

            // ファイル名
            var fileName = string.Format("log_{0}.zip", DateTime.Now.ToString("yyyyMMddHHmms"));

            // ディレクトリパス
            var dirPath = Path.GetDirectoryName(assembly.Location) + @"\logs";

            // ファイルパス
            var filePath = Path.Combine(Path.GetDirectoryName(assembly.Location), fileName);

            var sb = new StringBuilder();

            sb.AppendLine("システムエラーが発生しました。");
            sb.AppendLine("詳細は添付ファイルを確認してください。");
            sb.AppendLine("");
            sb.AppendLine(string.Format("PC端末：{0}", Environment.MachineName));
            sb.AppendLine(string.Format("ログオン：{0}", Environment.UserName));
            sb.AppendLine("");
            sb.AppendLine("********** 管理者に伝言がある場合は以下にご記入ください。**********");
            sb.AppendLine("");

            // メール内容
            var mail = new
            {
                mailto = new AppConfigAccessor().GetAppSetting("SupportMail"),
                subject = string.Format("[{0}] システムエラーが発生しました", assembly.GetName().Name),
                body = sb.ToString()
            };

            try
            {
                FormControlUtil.FormWait(this, () =>
                {
                    // Zipファイルの作成
                    if (ZipUtil.FolderCompression(dirPath, fileName))
                    {
                        var mapi = new MapiUtil();

                        mapi.AddAttachment(filePath);
                        mapi.AddRecipientTo(mail.mailto);

                        // メーラーの起動
                        mapi.SendMailPopup(mail.subject, mail.body);
                    }
                });
            }
            catch
            {
                // 未処理
            }
            finally
            {
                // Zipファイルの削除
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}
