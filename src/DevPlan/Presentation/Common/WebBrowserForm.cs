using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.IO;


using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.UICommon.Config;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Enum;
using System.Configuration;

namespace DevPlan.Presentation.Common
{
    public partial class WebBrowserForm : BaseForm
    {
        private string loginUrlString = new AppConfigAccessor().GetAppSetting("XeyeLoginURL");
        private string topUrlString = new AppConfigAccessor().GetAppSetting("XeyeTopURL");
        private string allSearchUrlString = new AppConfigAccessor().GetAppSetting("XeyeSearchURL");

        public ChromiumWebBrowser chromeBrowser;

        private string naviGate;
        private string CurrentAddress;
        
        public override string FormTitle { get { return "外部画面表示"; } }

        public WebBrowserForm(string url)
        {
            naviGate = url;
            if (allSearchUrlString != naviGate)
            {
                naviGate = naviGate + "#_cvsMap";
            }
            InitializeComponent();
            InitializeChromium();
        }

        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebBrowserForm_Load(object sender, EventArgs e)
        {
            
        }

        public void InitializeChromium()
        {
            if (!Cef.IsInitialized)
            {
                //CEFの設定
                CefSettings settings = new CefSettings();
                //日本語設定
                settings.Locale = "ja";
                settings.AcceptLanguageList = "ja-JP";

                //Debug.logの制御
                settings.LogSeverity = LogSeverity.Disable;

                //GPUCache や blob_storageの制御
                var appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                settings.UserDataPath = appPath;

                // 異常終了対策
                CefSharpSettings.ShutdownOnExit = false;

                // 高DPI対策
                //2023/09/05 Delete Start 杉浦 CefSharpバージョンアップ(暫定対応)
                //Cef.EnableHighDPISupport();
                //2023/09/05 Delete End 杉浦 CefSharpバージョンアップ(暫定対応)

                //ブラウザ起動パス
                settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                       Environment.Is64BitProcess ? "x64" : "x86",
                                                       "CefSharp.BrowserSubprocess.exe");

                //CEF設定の反映
                Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
            }

            //ブラウザキック
            chromeBrowser = new ChromiumWebBrowser(naviGate);
            this.Controls.Add(chromeBrowser);
            chromeBrowser.BringToFront();
            chromeBrowser.Dock = DockStyle.Fill;

            //ブラウザセッティング
            BrowserSettings browserSettings = new BrowserSettings();
            //2023/09/05 Delete Start 杉浦 CefSharpバージョンアップ(暫定対応)
            //browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            //browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            //2023/09/05 Delete End 杉浦 CefSharpバージョンアップ(暫定対応)
            chromeBrowser.BrowserSettings = browserSettings;

            //イベントハンドラ
            chromeBrowser.LoadingStateChanged += ChromeBrowser_LoadingStateChanged;
        }

        private void ChromeBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            try
            {
                if (!e.IsLoading)
                {
                    if (chromeBrowser != null)
                    {
                        CurrentAddress = chromeBrowser.Address;
                        if (CurrentAddress.Contains(loginUrlString))
                        {
                            //ログインIDとパスを取得
                            var item = getXeyeData();

                            //ログインID
                            chromeBrowser.ExecuteScriptAsync(string.Format("document.getElementById('_txtUserId').value = '{0}';", item.ID));

                            //パスワード
                            chromeBrowser.ExecuteScriptAsync(string.Format("document.getElementById('_pwdPassword').value = '{0}';", item.PASS));

                            //submit
                            string jsScript = "var btn = document.getElementsByTagName('button');" +
                                              "for(var i = 0; i < btn.length; i++){ " +
                                              "  if(btn[i].getAttribute('type') == 'button') btn[i].click(); " +
                                              "}";
                            chromeBrowser.ExecuteScriptAsync(jsScript);
                        }
                        else if (CurrentAddress.Contains(topUrlString))
                        {
                            chromeBrowser.Load(naviGate);
                        }
                        else
                        {
                            //ニュートラル状態では何もしない
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() => { MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error); }));
            }
        }

        private XeyeDataOutModel getXeyeData()
        {
            //XEYEのログインIDとパスワードを取得する
            var res = HttpUtil.GetResponse<XeyeDataOutModel>(ControllerType.XeyeData);
            var xeyeId = new List<XeyeDataOutModel>();
            xeyeId.AddRange(res.Results);

            return xeyeId.FirstOrDefault();
        }

    }
}
