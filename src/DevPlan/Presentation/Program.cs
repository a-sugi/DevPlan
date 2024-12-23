using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Diagnostics;
using System.Globalization;
using System.IO;

using log4net;

using DevPlan.Presentation.Common;
using DevPlan.UICommon.Utils;

namespace Presentation
{
    static class Program
    {
        private static log4net.ILog logger = null;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            try
            {
                FirstInstration();

                PcInfoLog(logger);

                //例外発生時のハンドリング
                Application.ThreadException += (sender, e) => ShowExceptionForm(sender, e.Exception);
                Thread.GetDomain().UnhandledException += (sender, e) => ShowExceptionForm(sender, e.ExceptionObject as Exception);

                //GregorianCalendarをデフォルトとする。
                CultureInfo cultureInfo = new CultureInfo("ja-JP");
                cultureInfo.DateTimeFormat.Calendar = new GregorianCalendar();
                Application.CurrentCulture = cultureInfo;

                //高DPI対応
                new DeviceUtil().SetProcessHighDPI();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                AppDomain.CurrentDomain.AssemblyResolve += Resolver;
                Application.Run(new DevPlan.Presentation.LoginForm());
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

            }
        }
        
        /// <summary>
        /// 初回インストール処理
        /// </summary>
        private static void FirstInstration()
        {
            if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            {
                logger.Info("First time installation.");

                string appPath = Application.StartupPath;
                string winPath = Environment.GetEnvironmentVariable("WINDIR");

                Process proc = new Process();
                System.IO.Directory.SetCurrentDirectory(appPath);

                proc.EnableRaisingEvents = false;
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                proc.StartInfo.FileName = winPath + @"\Microsoft.NET\Framework\v4.0.30319\ngen.exe";
                proc.StartInfo.Arguments = "uninstall " + Application.ProductName + " /nologo /silent";

                logger.Info("Uninstallation of negen.");
                proc.Start();
                proc.WaitForExit();

                proc.StartInfo.FileName = winPath + @"\Microsoft.NET\Framework\v4.0.30319\ngen.exe";
                proc.StartInfo.Arguments = "install " + Application.ProductName + " /nologo /silent";

                logger.Info("Installation of negen.");
                proc.Start();
                proc.WaitForExit();
                logger.Info("Successful installation of negen.");

            }
        }

        /// <summary>
        /// 起動時にPC情報をログ出力する
        /// </summary>
        /// <param name="logger"></param>
        private static void PcInfoLog(ILog logger)
        {
            //OSの情報を取得する
            var os = Environment.OSVersion;

            //OSの情報を表示する
            logger.Info("Version = " + os.ToString());

            if (Environment.Is64BitOperatingSystem)
                logger.Info("Bit = 64bit OS");
            else
                logger.Info("Bit = 32bit OS");

            logger.Info("Resolution = " + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
                + "x" + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);

            logger.Info("CurrentCulture = " + System.Threading.Thread.CurrentThread.CurrentCulture.ToString());

            var mainAssembly = Assembly.GetEntryAssembly();
            logger.Info("ConverterVersion = " + mainAssembly.GetName().Version);
        }

        /// <summary>
        /// 例外エラー時のページを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        private static void ShowExceptionForm(object sender, Exception ex)
        {
            logger.Error(ex.Message, ex);

            if (ex.HResult == -2147467261 && ex.GetType() == typeof(NullReferenceException))
            {
                //CalendarGrid内部エラーにつきログのみ出力。
            }
            else
            {
                using (var frm = new ErrorMessage())
                {
                    frm.ex = ex;
                    frm.ShowDialog();

                };
            }
        }

        // Will attempt to load missing assembly from either x86 or x64 subdir
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("CefSharp"))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                       Environment.Is64BitProcess ? "x64" : "x86",
                                                       assemblyName);

                return File.Exists(archSpecificPath)
                           ? Assembly.LoadFile(archSpecificPath)
                           : null;
            }

            return null;
        }
    }
}
