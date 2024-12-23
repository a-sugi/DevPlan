using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
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

using DevPlan.Presentation.UIDevPlan.Announce;
using DevPlan.Presentation.UIDevPlan.BrowsingAuthority;
using DevPlan.Presentation.UIDevPlan.CapAndProduct;
using DevPlan.Presentation.UIDevPlan.CarShare;
using DevPlan.Presentation.UIDevPlan.FunctionAuthority;
using DevPlan.Presentation.UIDevPlan.MeetingDocument;
using DevPlan.Presentation.UIDevPlan.MonthlyPlan;
using DevPlan.Presentation.UIDevPlan.MonthlyReport;
using DevPlan.Presentation.UIDevPlan.OperationPlan;
using DevPlan.Presentation.UIDevPlan.OuterCar;
using DevPlan.Presentation.UIDevPlan.ProgressList;
using DevPlan.Presentation.UIDevPlan.TestCarSchedule;
using DevPlan.Presentation.UIDevPlan.WeeklyReport;
using DevPlan.Presentation.UIDevPlan.WorkProgress;
using DevPlan.Presentation.UIDevPlan.DesignCheck;

using DevPlan.Presentation.UITestCar.ControlSheet;
using DevPlan.Presentation.UITestCar.Disposal;
using DevPlan.Presentation.UITestCar.Othe.ApplicationApprovalCar;
using DevPlan.Presentation.UITestCar.Othe.CarInspection;
using DevPlan.Presentation.UITestCar.Othe.DesignatedMonthNumber;
using DevPlan.Presentation.UITestCar.Othe.TestCarHistory;

using ControlSheet = DevPlan.Presentation.UITestCar.Othe.ControlSheet;
using System.Net;
using System.Threading.Tasks;
using System.Configuration;
using DevPlan.Presentation.UIDevPlan.TruckSchedule;
using DevPlan.UICommon.Util;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.Base
{
    /// <summary>
    /// 基底フォーム
    /// </summary>
    public partial class BaseForm : Form
    {
        #region インスタンス
        /// <summary>ロガー(log4net)</summary>
        protected ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region 定義
        /// <summary>ユーザー検索条件初期名称</summary>
        private const string DefaultName = "**********";

        /// <summary>ヘルプサイト</summary>
        private const string HelpSite = "HELP_SITE";

        /// <summary>モーダル同時起動画面</summary>
        private readonly string[] ModalKickWindows = { "試験車日程", "管理票検索結果", "設計チェック" };

        /// <summary>検索条件表示切替ボタン</summary>
        protected readonly Dictionary<bool, string> SearchConditionButtonText = new Dictionary<bool, string>()
        {
            { true, "-" },
            { false, "+" }
        };
        #endregion
        
        #region メンバ変数
        /// <summary>
        /// 業務計画表のお気に入りメニュー
        /// </summary>
        public ContextMenuStrip OperationPlanButtonFavoriteItem = new ContextMenuStrip();

        /// <summary>
        /// 月次計画表のお気に入りメニュー
        /// </summary>
        public ContextMenuStrip MonthlyPlanButtonFavoriteItem = new ContextMenuStrip();

        /// <summary>
        /// 車種別進捗状況一覧のお気に入りメニュー
        /// </summary>
        public ContextMenuStrip ProgressStatusListButtonFavoriteItem = new ContextMenuStrip();

        /// <summary>
        /// 試験車日程のお気に入りメニュー
        /// </summary>
        public ContextMenuStrip TestCarScheduleButtonFavoriteItem = new ContextMenuStrip();

        /// <summary>
        /// 外製車日程のお気に入りメニュー
        /// </summary>
        public ContextMenuStrip OtherMakersCarButtonFavoriteItem = new ContextMenuStrip();

        /// <summary>
        /// カーシェア日程のお気に入りメニュー
        /// </summary>
        public ContextMenuStrip CarSharePlanButtonFavoriteItem = new ContextMenuStrip();

        /// <summary>
        /// お知らせデータグリッド
        /// </summary>
        private DataGridView announceDataGridView = null;

        /// <summary>
        /// 機能権限リスト
        /// </summary>
        public List<UserAuthorityOutModel> FunctionAuthList = new List<UserAuthorityOutModel>();
        
        /// <summary>
        /// お気に入りリスト
        /// </summary>
        public List<FavoriteSearchOutModel> FavoriteList = new List<FavoriteSearchOutModel>();
        #endregion

        #region プロパティ
        /// <summary>システム名</summary>
        public virtual string SystemName { get { return Properties.Resources.SystemName; } }

        /// <summary>画面名</summary>
        public virtual string FormTitle { get { return "タイトルが設定されていません"; } }

        /// <summary>横幅</summary>
        public virtual int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public virtual int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public virtual int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public virtual int FormMinHeight { get { return this.MinimumSize.Height; } }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseForm()
        {
            InitializeComponent();

            //ダブルバァッファリング有効化
            this.DoubleBuffered = true;

        }
        #endregion

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Load(object sender, EventArgs e)
        {
            // 画面初期化
            this.Init();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            // タイトル設定
            this.SetTitle(this.FormTitle);

            // ステータスラベル設定
            this.SetStatusLabel();

            // デバッグデザイン設定
            this.SetDebugDesign();

            // デザインモードでない場合
            if (!DesignMode)
            {
                // 機能権限取得
                this.FunctionAuthList = this.GetFunctionList();

                // 画面サイズプロパティ設定
                this.MinimumSize = new Size(this.FormMinWidth, this.FormMinHeight);
                this.Width = this.FormWidth;
                this.Height = this.FormHeight;

                // 共通メニュー項目初期化
                this.InitCommonMenu();

                // 設定メニュー項目初期化
                this.InitConfigMenu();
            }
            
            // モーダル同時起動画面は除く
            if (Array.IndexOf(ModalKickWindows, this.FormTitle) < 0)
            {
                // 初期表示時直後クリック禁止
                this.SetTimerFormEnabled();
            }

            // TabStopプロパティ初期設定
            FormControlUtil.InitTabStopProperty(this);

            // Labelプロパティ初期設定
            FormControlUtil.InitLabel(this);
        }

        /// <summary>
        /// タイトル設定
        /// </summary>
        /// <param name="title"></param>
        protected void SetTitle(string title)
        {
            this.Text = string.Format("{0} - {1}", title, this.SystemName);
        }

        /// <summary>
        /// ステータスラベル設定
        /// </summary>
        protected void SetStatusLabel()
        {
            var version = string.Empty;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
            {
                var ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                version = string.Format("{0}.{1}.{2}.{3}", ver.Major.ToString(), ver.Minor.ToString(), ver.Build.ToString(), ver.Revision.ToString());
            }

            this.GuideToolStripStatusLabel.Text = string.Format("{0} Ver {1} ({2})", SystemName, version, SessionDto.UserName);
        }

        /// <summary>
        /// デバッグデザイン設定
        /// </summary>
        protected void SetDebugDesign()
        {
#if DEBUG
            this.Text += " (Trial)";

            this.ContentsPanel.BackColor = Color.FromArgb(255, 230, 255);
#endif
        }

        /// <summary>
        /// フォーム非活性一時設定
        /// </summary>
        protected async void SetTimerFormEnabled()
        {
            this.Enabled = false;
            await Task.Delay(200);
            this.Enabled = true;
        }
        #endregion

        #region 共通イベント
        /// <summary>
        /// キー押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_KeyPress(object sender, KeyPressEventArgs e)
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

        /// <summary>
        /// 右(閉じる)ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region 共通メニュー

        #region メインメニュー
        /// <summary>
        /// メイン（お気に入り含む）メニュークリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommonMenu_Click(object sender, EventArgs e)
        {
            var tsmi = sender as ToolStripMenuItem;

            var id = tsmi?.Tag as long?;

            switch (tsmi.Name)
            {
                // 業務計画表
                case "OperationPlanToolStripMenuItem":
                    RunOperationPlan();
                    break;

                // 業務計画表（お気に入り）
                case "FavoriteOperatonPlanToolStripMenuItem":
                case "OperationPlanButton":
                    if (IsFavoriteEnable("01", id))
                    {
                        RunOperationPlan(id);
                    }
                    else
                    {
                        Messenger.Warn(Resources.KKM00020);
                    }
                    break;

                // 月次計画表
                case "MonthlyPlanToolStripMenuItem":
                    RunMonthlyPlan();
                    break;

                // 月次計画表(お気に入り)
                case "FavoriteMonthlyPlanToolStripMenuItem":
                case "MonthlyPlanButton":
                    if (IsFavoriteEnable("02", id))
                    {
                        RunMonthlyPlan(id);
                    }
                    else
                    {
                        Messenger.Warn(Resources.KKM00020);
                    }
                    break;

                // 目標進度リスト
                case "TargetProgressToolStripMenuItem":
                    RunTargetProgress();
                    break;

                // 車種別進捗状況一覧
                case "ProgressStatusListToolStripMenuItem":
                    RunProgressStatusList();
                    break;

                // 車種別進捗状況一覧(お気に入り)
                case "FavoriteProgressStatusListToolStripMenuItem":
                case "ProgressStatusListButton":
                    if (IsFavoriteEnable("06", id))
                    {
                        RunProgressStatusList(id);
                    }
                    else
                    {
                        Messenger.Warn(Resources.KKM00020);
                    }
                    break;

                // カーシェア一覧
                case "CarShareToolStripMenuItem":
                    RunCarShare();
                    break;

                // 試験車日程
                case "TestCarScheduleToolStripMenuItem":
                    RunTestCarSchedule();
                    break;

                // 試験車日程（メニュー、お気に入りの２パターンあり）
                case "FavoriteTestCarScheduleToolStripMenuItem":
                case "TestCarScheduleButton":
                    if (IsFavoriteEnable("03", id))
                    {
                        RunTestCarSchedule(id);
                    }
                    else
                    {
                        Messenger.Warn(Resources.KKM00020);
                    }

                    break;

                // 外製車日程
                case "OtherMakersCarToolStripMenuItem":
                    RunOtherMakersCar();
                    break;

                // 外製車日程（メニュー、お気に入りの２パターンあり）
                case "FavoriteOtherMakersCarToolStripMenuItem":
                case "OtherMakersCarButton":
                    RunOtherMakersCar(id);
                    break;

                // カーシェア日程
                case "CarSharePlanToolStripMenuItem":
                    RunCarSharePlan();
                    break;

                // カーシェア日程（メニュー、お気に入りの２パターンあり）
                case "FavoriteCarSharePlanToolStripMenuItem":
                case "CarSharePlanButton":
                    RunCarSharePlan(id);
                    break;

                // 作業履歴(試験車)
                case "FavoriteTestCarScheduleHistoryToolStripMenuItem":
                    if (IsFavoriteEnable("07", id))
                    {
                        RunTestCarScheduleHistory(id);
                    }
                    else
                    {
                        Messenger.Warn(Resources.KKM00020);
                    }

                    break;

                // 作業履歴(外製車)
                case "FavoriteOtherMakersCarHistoryToolStripMenuItem":
                    RunOtherMakersCarHistory(id);
                    break;

                // 作業履歴(カーシェア)
                case "FavoriteCarSharePlanHistoryToolStripMenuItem":
                    RunCarSharePlanHistory(id);
                    break;

                // カーシェア管理
                case "CarShareManageToolStripMenuItem":
                    RunCarShareManage();
                    break;

                // トラック予約表
                case "TruckScheduleToolStripMenuItem":
                    RunTruckSchedule();
                    break;

                // 週報
                case "WeeklyReportToolStripMenuItem":
                    RunWeeklyReport();
                    break;

                // 月報
                case "MonthlyReportToolStripMenuItem":
                    RunMonthlyReport();
                    break;

                // 検討会資料
                case "DiscussionPaperToolStripMenuItem":
                    RunDiscussionPaper();
                    break;

                // ＣＡＰ・商品力
                case "CapAgendaToolStripMenuItem":
                    RunCapAgenda();
                    break;

                // 設計チェック
                case "DesignCheckToolStripMenuItem":
                    RunDesignCheck();
                    break;

                // お知らせ一覧
                case "AnnounceToolStripMenuItem":
                    RunAnnounce();
                    break;

                // ロール
                case "RollToolStripMenuItem":
                    RunRoll();
                    break;

                // 機能権限
                case "FunctionAuthToolStripMenuItem":
                    RunFunctionAuth();
                    break;

                // 閲覧権限設定
                case "GeneralCodeBrowsingToolStripMenuItem":
                    RunGeneralCodeBrowsing();
                    break;

                // 試験車リスト
                case "TestCarListManagerToolStripMenuItem":
                    RunControlSheetList();
                    break;

                // ラベル印刷
                case "LabelPrintManagerToolStripMenuItem":
                    RunControlLabelIssue();
                    break;

                // 廃却期限リスト
                case "DisposalPeriodManagerToolStripMenuItem":
                case "DisposalPeriodToolStripMenuItem":
                    RunDisposalPeriod();
                    break;

                // 廃却車両搬出日入力
                case "DisposalCarryoutManagerToolStripMenuItem":
                    RunDisposalCarryout();
                    break;

                // 処理待ち車両リスト
                case "ApplicationApprovalManagerCarToolStripMenuItem":
                case "ApplicationApprovalCarToolStripMenuItem":
                    RunApplicationApprovalCar();
                    break;

                // 試験車使用履歴
                case "TestCarUseHistoryManagerToolStripMenuItem":
                case "TestCarUseHistoryToolStripMenuItem":
                    RunTestCarHistory();
                    break;

                // 管理票検索
                case "ControlSheetManagerToolStripMenuItem":
                case "ControlSheetToolStripMenuItem":
                    RunControlSheet();
                    break;

                // 指定月台数リスト
                case "DesignatedMonthNumberManagerToolStripMenuItem":
                case "DesignatedMonthNumberToolStripMenuItem":
                    RunDesignatedMonthNumber();
                    break;

                // 車検・点検リスト発行
                case "CarInspectionManagerToolStripMenuItem":
                case "CarInspectionToolStripMenuItem":
                    RunCarInspectionForm();
                    break;
                // Append Start 2021/01/25 杉浦
                case "XeyeToolStripMenuItem":
                    string url = new AppConfigAccessor().GetAppSetting("XeyeSearchURL");
                    RunXeye(url);
                    break;
                case "XeyeReadCsvToolStripMenuItem":
                    RunReadXeyeCsv();
                    break;
                // Append End 2021/01/25 杉浦

            }
        }

        /// <summary>
        /// お気に入りが使用可能かどうかチェック
        /// </summary>
        /// <returns></returns>
        private bool IsFavoriteEnable(string classData, long? favoriteID)
        {
            // 試験車日程・作業履歴(試験車)以外は未チェック
            if (classData != "03" || classData != "07") return true;

            // 全閲覧権限
            var isAllGeneralCode = this.FunctionAuthList.FirstOrDefault(x => x.FUNCTION_ID == (int)FunctionID.TestCar).ALL_GENERAL_CODE_FLG == '1';

            foreach (FavoriteSearchOutModel fsom in this.FavoriteList)
            {
                if (fsom.CLASS_DATA == classData && fsom.ID == favoriteID && (isAllGeneralCode || fsom.PERMIT_FLG == 1))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region お気に入りメニュー
        /// <summary>
        /// お気に入りメニュードロップダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookmarkToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //共通メニュー項目初期化
            InitCommonMenu();
        }

        /// <summary>
        /// お気に入り編集ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteEditButton_Click(object sender, EventArgs e)
        {
            FavoriteEditProcess(((ToolStripButton)sender).Tag?.ToString());
        }

        #region お気に入りメニュー
        /// <summary>
        /// お気に入り編集処理
        /// </summary>
        private void FavoriteEditProcess(string classdata)
        {
            // お気に入り検索
            using (var form = new FavoriteListForm(new FavoriteSearchInModel() { CLASS_DATA = classdata }))
            {
                var functions = form.GetGridDataList();

                // お気に入りデータがない場合はメッセージ表示後終了
                if (functions.Count == 0)
                {
                    Messenger.Info(Resources.KKM00036);

                    return;
                }

                // OKの場合はお気に入りデータ再設定
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // お気に入り取得
                    this.FavoriteList = GetFavoriteList();
                }
            }
        }
        #endregion

        #region 表示メニュー
        /// <summary>
        /// 縦に並べて表示メニュークリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerticalDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int h = Screen.GetWorkingArea(this).Height;
            int w = Screen.GetWorkingArea(this).Width;
            int cnt = Application.OpenForms.Cast<Form>().Where(f => f is BaseForm).Count();
            if (cnt == 1) return;
            int top = 0;
            int bnd = h / (cnt - 1);

            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "LoginForm") continue;

                if (frm is BaseForm == false)
                {
                    this.SetFormCenterParent(frm);
                    frm.Activate();
                    continue;
                }

                var bfrm = frm as BaseForm;

                if (bfrm.Name == "MenuForm")
                {
                    this.SetFormDefaultPosition(bfrm);
                }
                else
                {
                    bfrm.WindowState = FormWindowState.Normal;
                    bfrm.Width = w;
                    bfrm.Height = bnd >= bfrm.FormMinHeight ? bnd : bfrm.FormMinHeight;
                    bfrm.Top = top;
                    bfrm.Left = 0;
                    top += bnd;
                    frm.Activate();
                }
            }
        }

        /// <summary>
        /// 横に並べて表示メニュークリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorizontalDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int h = Screen.GetWorkingArea(this).Height;
            int w = Screen.GetWorkingArea(this).Width;
            int cnt = Application.OpenForms.Cast<Form>().Where(f => f is BaseForm).Count();
            if (cnt == 1) return;
            int left = 0;
            int bnd = w / (cnt - 1);

            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "LoginForm") continue;

                if (frm is BaseForm == false)
                {
                    this.SetFormCenterParent(frm);
                    frm.Activate();
                    continue;
                }

                var bfrm = frm as BaseForm;

                if (bfrm.Name == "MenuForm")
                {
                    this.SetFormDefaultPosition(bfrm);
                }
                else
                {
                    bfrm.WindowState = FormWindowState.Normal;
                    bfrm.Width = bnd >= bfrm.FormMinWidth ? bnd : bfrm.FormMinWidth;
                    bfrm.Height = h;
                    bfrm.Top = 0;
                    bfrm.Left = left;
                    left += bnd;
                    frm.Activate();
                }
            }
        }

        /// <summary>
        /// 画面を初期値の位置とサイズに再表示する
        /// </summary>
        /// <param name="frm"></param>
        private void SetFormDefaultPosition(BaseForm frm)
        {
            int h = Screen.GetWorkingArea(this).Height;
            int w = Screen.GetWorkingArea(this).Width;
            frm.Width = frm.FormWidth;
            frm.Height = frm.FormHeight;
            frm.Top = (h - frm.Height) / 2;
            frm.Left = (w - frm.Width) / 2;
        }

        /// <summary>
        /// サブ画面を親の中央位置に再表示する
        /// </summary>
        /// <param name="frm"></param>
        private void SetFormCenterParent(Form frm)
        {
            var x = frm.Owner.Width > frm.Width ? (frm.Owner.Width - frm.Width) / 2 : frm.Owner.Width / 2;
            var y = frm.Owner.Height > frm.Width ? (frm.Owner.Height - frm.Height) / 2 : frm.Owner.Height / 2;

            frm.Location = new Point(frm.Owner.Location.X + x, frm.Owner.Location.Y + y);
        }
        #endregion

        #region 設定メニュー
        /// <summary>
        /// 設定メニュー初期化
        /// </summary>
        protected void InitConfigMenu()
        {
            // 設定（自動ログイン）
            this.AppConfigAutoLoginToolStripMenuItem.Checked = Properties.Settings.Default.AutoLogin;

            // 設定（自部署処理待ち）
            this.AppConfigMyPendingCarToolStripMenuItem.Visible = this.IsFunctionEnable(FunctionID.TestCarManagement) && SessionDto.ManagementDepartmentType.Any(x => x == Const.Kenjitu || x == Const.Kanri);
            this.AppConfigMyPendingCarToolStripMenuItem.Checked = Properties.Settings.Default.MyPendingCar;
        }

        /// <summary>
        /// 設定メニュードロップダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppConfigToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            // 設定メニュー項目初期化
            InitConfigMenu();
        }

        /// <summary>
        /// 次回以降は自動ログインするメニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppConfigAutoLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var login = new LoginForm();

            login.SetAutoLogin(this.AppConfigAutoLoginToolStripMenuItem.Checked, login.Decrypt(SessionDto.LoginId, "SESSION"), login.Decrypt(SessionDto.Password, "SESSION"));
        }

        /// <summary>
        /// 処理待ち状況確認を自部署のみ表示するメニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        virtual protected void AppConfigMyPendingCarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MyPendingCar = this.AppConfigMyPendingCarToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region ヘルプメニュー
        /// <summary>
        /// ヘルプメニュークリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // var helpFile = Properties.Settings.Default.HelpFile;
            // Configファイルから取得に変更
            var helpFile = ConfigurationManager.AppSettings[HelpSite];
            try
            {
                Process.Start(helpFile);
            }
            catch
            {
                // ファイルが見つからなかった時は何もしない
            }
        }
        #endregion

        #region ログオフメニュー
        /// <summary>
        /// ログオフメニュークリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Messenger.Confirm(Resources.KKM02001) == DialogResult.Yes)
            {
                LogoffProcess();
            }
        }

        /// <summary>
        /// ログオフ処理
        /// </summary>
        public void LogoffProcess()
        {
            // ログオフ時に自動ログイン情報を初期化
            Properties.Settings.Default.AutoLogin = false;
            Properties.Settings.Default.UserID = string.Empty;
            Properties.Settings.Default.Password = string.Empty;
            Properties.Settings.Default.Save();

            // 再起動
            Application.Restart();
        }
        #endregion

        #region 共通メニュー項目初期化
        /// <summary>
        /// 共通メニュー項目初期化
        /// </summary>
        public void InitCommonMenu()
        {
            // お気に入り取得
            this.FavoriteList = GetFavoriteList();

            // お気に入りメニューの設定
            // 業務計画表
            this.AddContextMenu(this.FavoriteOperatonPlanToolStripMenuItem);

            // 月次計画表
            this.AddContextMenu(this.FavoriteMonthlyPlanToolStripMenuItem);

            // 車種別進捗状況一覧
            this.AddContextMenu(this.FavoriteProgressStatusListToolStripMenuItem);

            // 試験車日程
            this.AddContextMenu(this.FavoriteTestCarScheduleToolStripMenuItem);

            // 外製車日程
            this.AddContextMenu(this.FavoriteOtherMakersCarToolStripMenuItem);

            // カーシェア日程
            this.AddContextMenu(this.FavoriteCarSharePlanToolStripMenuItem);

            // 作業履歴(試験車)
            this.AddContextMenu(this.FavoriteTestCarScheduleHistoryToolStripMenuItem);

            // 作業履歴(外製車)
            this.AddContextMenu(this.FavoriteOtherMakersCarHistoryToolStripMenuItem);

            // 作業履歴(カーシェア)
            this.AddContextMenu(this.FavoriteCarSharePlanHistoryToolStripMenuItem);

            // 管理票検索
            this.AddContextMenu(this.FavoriteControlSheetToolStripMenuItem);

            var flg = false;

            // 業務計画表　機能権限チェック
            flg = this.IsFunctionEnable(FunctionID.Plan);
            this.OperationPlanToolStripMenuItem.Visible = flg;
            this.FavoriteOperatonPlanToolStripMenuItem.Visible = flg && this.FavoriteOperatonPlanToolStripMenuItem.DropDownItems.Count > 0;

            // 月次計画表　機能権限チェック
            this.MonthlyPlanToolStripMenuItem.Visible = flg;
            this.FavoriteMonthlyPlanToolStripMenuItem.Visible = flg && this.FavoriteMonthlyPlanToolStripMenuItem.DropDownItems.Count > 0;

            // 車種別進捗状況一覧　機能権限チェック
            this.ProgressStatusListToolStripMenuItem.Visible = flg;
            this.FavoriteProgressStatusListToolStripMenuItem.Visible = flg && this.FavoriteProgressStatusListToolStripMenuItem.DropDownItems.Count > 0;

            // 目標進度リスト　機能権限チェック
            this.TargetProgressToolStripMenuItem.Visible = this.IsFunctionEnable(FunctionID.GoalProgress);

            // 試験車日程　機能権限チェック
            flg = this.IsFunctionEnable(FunctionID.TestCar);
            this.TestCarScheduleToolStripMenuItem.Visible = flg;
            this.FavoriteTestCarScheduleToolStripMenuItem.Visible = flg && this.FavoriteTestCarScheduleToolStripMenuItem.DropDownItems.Count > 0;

            // 作業履歴(試験車)　機能権限チェック
            this.FavoriteTestCarScheduleHistoryToolStripMenuItem.Visible = flg && this.FavoriteTestCarScheduleHistoryToolStripMenuItem.DropDownItems.Count > 0;

            // 車両検索　機能権限チェック
            flg = this.IsFunctionEnable(FunctionID.CarSearch);
            this.CarShareToolStripMenuItem.Visible = flg;

            // 外製車日程　機能権限チェック
            flg = this.IsFunctionEnable(FunctionID.OuterCar);
            this.OtherMakersCarToolStripMenuItem.Visible = flg;
            this.FavoriteOtherMakersCarToolStripMenuItem.Visible = flg && this.FavoriteOtherMakersCarToolStripMenuItem.DropDownItems.Count > 0;

            // 作業履歴(外製車)　機能権限チェック
            this.FavoriteOtherMakersCarHistoryToolStripMenuItem.Visible = flg && this.FavoriteOtherMakersCarHistoryToolStripMenuItem.DropDownItems.Count > 0;

            // カーシェア日程　機能権限チェック
            flg = this.IsFunctionEnable(FunctionID.CarShare);
            this.CarSharePlanToolStripMenuItem.Visible = flg;
            this.FavoriteCarSharePlanToolStripMenuItem.Visible = flg && this.FavoriteCarSharePlanToolStripMenuItem.DropDownItems.Count > 0;

            // 作業履歴(カーシェア)　機能権限チェック
            this.FavoriteCarSharePlanHistoryToolStripMenuItem.Visible = flg && this.FavoriteCarSharePlanHistoryToolStripMenuItem.DropDownItems.Count > 0;

            // カーシェア事務所　機能権限チェック
            this.CarShareManageToolStripMenuItem.Visible = this.IsFunctionEnable(FunctionID.CarShareOffice);

            // トラック予約表　機能権限チェック
            this.TruckScheduleToolStripMenuItem.Visible = this.IsFunctionEnable(FunctionID.Truck);

            // Append Start 2021/01/25 杉浦
            // 全国車両位置検索
            this.AddContextMenu(this.XeyeToolStripMenuItem);
            // Append End 2021/01/25 杉浦

            // 週報　機能権限チェック
            this.WeeklyReportToolStripMenuItem.Visible = this.IsFunctionEnable(FunctionID.WeeklyReport);

            // 月報　機能権限チェック
            this.MonthlyReportToolStripMenuItem.Visible = this.IsFunctionEnable(FunctionID.MonthlyReport);

            // 検討会資料　機能権限チェック
            this.DiscussionPaperToolStripMenuItem.Visible = this.IsFunctionEnable(FunctionID.ConsiderationDocument);

            // ＣＡＰ・商品力　機能権限チェック
            this.CapAgendaToolStripMenuItem.Visible = this.IsFunctionEnable(FunctionID.CAP);

            // 設計チェック　機能権限チェック
            this.DesignCheckToolStripMenuItem.Visible = this.IsFunctionEnable(FunctionID.DesignCheck);

            // ロール　機能権限チェック
            var isRoll = this.IsFunctionEnable(FunctionID.Roll);
            this.RollToolStripMenuItem.Visible = isRoll;

            // 機能権限設定　機能権限チェック
            var isFunctionAuth = this.IsFunctionEnable(FunctionID.FunctionAuthority);
            this.FunctionAuthToolStripMenuItem.Visible = isFunctionAuth;

            // 閲覧権限設定　機能権限チェック
            var isGeneralCodeBrowsing = this.IsFunctionEnable(FunctionID.BrowsingAuthority);
            this.GeneralCodeBrowsingToolStripMenuItem.Visible = isGeneralCodeBrowsing;

            // お知らせ管理　機能権限チェック
            var isAnnounce = this.IsFunctionEnable(FunctionID.Notice);
            this.AnnounceToolStripMenuItem.Visible = isAnnounce;

            // Append Start 2021/01/25 杉浦
            // Xeye取込
            var isXeyeRead = this.IsFunctionEnable(FunctionID.XeyeReadCsv);
            this.XeyeReadCsvToolStripMenuItem.Visible = isXeyeRead;
            // Append End 2021/01/25 杉浦
            
            // 設定メニュー
            this.ConfigToolStripMenuItem.Visible = isRoll || isFunctionAuth || isGeneralCodeBrowsing || isAnnounce;

            // 試験車管理
            var isTestCar = this.IsFunctionEnable(FunctionID.TestCarManagement);
            var isTestCarManagement = this.IsFunctionEnable(FunctionID.TestCarManagement, true);

            // 試験車管理(管理者)
            this.TestCarManagerToolStripMenuItem.Visible = isTestCarManagement;

            // 試験車管理
            this.TestCarToolStripMenuItem.Visible = isTestCar;

            // 管理票検索
            this.FavoriteControlSheetToolStripMenuItem.Visible = isTestCar && this.FavoriteControlSheetToolStripMenuItem.DropDownItems.Count > 0;

            // 20180424 Ron Edit Start
            HidePartMenu();
            // 20180424 Ron Edit End
        }

        /// <summary>
        /// お気に入りメニュー追加
        /// </summary>
        /// <param name="menu"></param>
        public void AddContextMenu(ToolStripMenuItem menu)
        {
            Action<string> set = cd =>
            {
                // 初期化アイテム
                var list = new List<ToolStripItem>();

                foreach (ToolStripItem item in menu.DropDownItems)
                {
                    if (item is ToolStripSeparator || item is ToolStripButton) continue;

                    list.Add(item);
                }

                // アイテム初期化
                list.ForEach(x => menu.DropDownItems.Remove(x));

                foreach (var f in this.FavoriteList.Where(x => x.CLASS_DATA == cd).Select((v, i) => new { Value = v, Index = i }))
                {
                    var item = new ToolStripMenuItem(f.Value.TITLE) { Name = menu.Name, Tag = f.Value.ID };
                    item.Click += CommonMenu_Click;

                    menu.DropDownItems.Insert(f.Index, item);
                }
            };

            //業務計画表
            if (menu == this.FavoriteOperatonPlanToolStripMenuItem)
            {
                set("01");
            }
            //月次計画表
            else if (menu == this.FavoriteMonthlyPlanToolStripMenuItem)
            {
                set("02");
            }
            //車種別進捗状況一覧
            else if (menu == this.FavoriteProgressStatusListToolStripMenuItem)
            {
                set("06");
            }
            //試験車日程
            else if (menu == this.FavoriteTestCarScheduleToolStripMenuItem)
            {
                set("03");
            }
            //外製車日程
            else if (menu == this.FavoriteOtherMakersCarToolStripMenuItem)
            {
                set("05");
            }
            //カーシェア日程
            else if (menu == this.FavoriteCarSharePlanToolStripMenuItem)
            {
                set("04");
            }
            //作業履歴(試験車)
            else if (menu == this.FavoriteTestCarScheduleHistoryToolStripMenuItem)
            {
                set("07");
            }
            //作業履歴(外製車)
            else if (menu == this.FavoriteOtherMakersCarHistoryToolStripMenuItem)
            {
                set("08");
            }
            //作業履歴(カーシェア)
            else if (menu == this.FavoriteCarSharePlanHistoryToolStripMenuItem)
            {
                set("09");
            }
            //管理票検索
            else if (menu == this.FavoriteControlSheetToolStripMenuItem)
            {
                //管理票メニュー項目初期化
                this.InitControlSheetMenuItem(this.FavoriteControlSheetToolStripMenuItem.DropDownItems);
            }
        }

        /// <summary>
        /// 未使用メニュー設定
        /// </summary>
        private void HidePartMenu()
        {
            //メニュ
            this.OperationPlanToolStripMenuItem.Visible = false;
            this.MonthlyPlanToolStripMenuItem.Visible = false;
            this.ProgressStatusListToolStripMenuItem.Visible = false;
            this.TargetProgressToolStripMenuItem.Visible = false;
            this.WeeklyReportToolStripMenuItem.Visible = false;
            this.MonthlyReportToolStripMenuItem.Visible = false;
            this.DiscussionPaperToolStripMenuItem.Visible = false;

            //お気に入り
            this.FavoriteOperatonPlanToolStripMenuItem.Visible = false;
            this.FavoriteMonthlyPlanToolStripMenuItem.Visible = false;
            this.FavoriteProgressStatusListToolStripMenuItem.Visible = false;
        }
        #endregion

        #region 管理票メニュー項目初期化
        /// <summary>
        /// 管理票メニュー項目初期化
        /// </summary>
        /// <param name="items">メニュー</param>
        protected void InitControlSheetMenuItem(ToolStripItemCollection items)
        {
            //メニュー初期化
            items.Clear();

            //ユーザー検索条件があるかどうか
            var list = this.GetUserSearchConditionNameList(SessionDto.UserId);
            if (list.Any() == true)
            {
                //コンテキストメニュー設定
                foreach (var cond in list)
                {
                    var item = new ToolStripMenuItem { Text = cond.条件名, Tag = cond };
                    item.Click += (o, ev) => this.RunControlSheet(cond);

                    items.Add(item);

                }

            }

        }
        #endregion

        #endregion

        #region 共通フォーム起動

        #region 開発計画表フォームの起動
        /// <summary>
        /// 業務計画表フォームの起動
        /// </summary>
        /// <param name="id">お気に入りID</param>
        public void RunOperationPlan(long? id = null)
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.Plan);

            var frm = new OperationPlanForm { FAVORITE_ID = id };
            frm.Show();
        }

        /// <summary>
        /// 月次計画表フォームの起動
        /// </summary>
        /// <param name="id">お気に入りID</param>
        public void RunMonthlyPlan(long? id = null)
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.Plan);

            var frm = new MonthlyPlanForm { FAVORITE_ID = id };
            frm.Show();
        }

        /// <summary>
        /// 試験車日程フォームの起動
        /// </summary>
        /// <param name="id">お気に入りID</param>
        public void RunTestCarSchedule(long? id = null)
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCar);

            var frm = new TestCarScheduleForm { FavoriteID = id };
            frm.Show();
        }

        /// <summary>
        /// 目標進度リストフォームの起動
        /// </summary>
        /// <param name="id">お気に入りID</param>
        public void RunTargetProgress(long? id = null)
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.GoalProgress);

            var frm = new ProgressListForm();
            frm.Show();
        }

        /// <summary>
        /// カーシェア一覧
        /// </summary>
        public void RunCarShare()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.CarSearch);

            var frm = new CarShareForm();
            frm.Show();
        }

        /// <summary>
        /// 車種別進捗状況一覧フォームの起動
        /// </summary>
        /// <param name="id">お気に入りID</param>
        public void RunProgressStatusList(long? id = null)
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.Plan);

            var frm = new WorkProgressForm();
            frm.Show();
        }

        /// <summary>
        /// 週報フォームの起動
        /// </summary>
        public void RunWeeklyReport()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.WeeklyReport);

            var frm = new WeeklyReportForm();
            frm.Show();
        }

        /// <summary>
        /// 月報フォームの起動
        /// </summary>
        public void RunMonthlyReport()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.MonthlyReport);

            var frm = new MonthlyReportForm();
            frm.Show();
        }

        /// <summary>
        /// 外製車日程フォームの起動
        /// </summary>
        /// <param name="id">お気に入りID</param>
        public void RunOtherMakersCar(long? id = null)
        {
            if (this.GetFunction(FunctionID.OuterCar).MANAGEMENT_FLG == '0')
            {
                using (var form = new CautionForm())
                {
                    form.ShowDialog(this);
                }
            }

            // 操作ログ登録
            SetOperationLog(FunctionID.OuterCar);

            var frm = new OuterCarForm { FavoriteID = id };
            frm.Show();
        }

        /// <summary>
        /// カーシェア日程フォームの起動
        /// </summary>
        /// <param name="id">お気に入りID</param>
        public void RunCarSharePlan(long? id = null)
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.CarShare);

            var frm = new CarShareScheduleForm { FavoriteID = id };
            frm.Show();
        }

        /// <summary>
        /// 作業履歴(試験車)フォームの起動
        /// </summary>
        /// <param name="id">お気に入りID</param>
        public void RunTestCarScheduleHistory(long? id = null)
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCar);

            new FormUtil(new UIDevPlan.TestCarSchedule.TestCarHistoryForm { UserAuthority = GetFunction(FunctionID.TestCar), FavoriteID = id }).SingleFormShow(this);
        }

        /// <summary>
        /// 作業履歴(外製車)フォームの起動
        /// </summary>
        /// <param name="id">お気に入りID</param>
        public void RunOtherMakersCarHistory(long? id = null)
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.OuterCar);

            new FormUtil(new OuterCarHistoryForm { UserAuthority = GetFunction(FunctionID.OuterCar), FavoriteID = id }).SingleFormShow(this);
        }

        /// <summary>
        /// 作業履歴(カーシェア)フォームの起動
        /// </summary>
        /// <param name="id">お気に入りID</param>
        public void RunCarSharePlanHistory(long? id = null)
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.CarShare);

            new FormUtil(new CarShareHistoryForm { UserAuthority = GetFunction(FunctionID.CarShare), FavoriteID = id }).SingleFormShow(this);
        }
        
        /// <summary>
        /// カーシェア管理フォームの起動
        /// </summary>
        public void RunCarShareManage()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.CarShareOffice);

            var frm = new CarShareManagementForm();
            frm.Show();
        }

        /// <summary>
        /// トラック予約表フォームの起動
        /// </summary>
        public void RunTruckSchedule()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.Truck);

            //（TODO：未実装）
            var frm = new TruckScheduleForm();
            frm.Show();
        }

        /// <summary>
        /// 検討会資料フォームの起動
        /// </summary>
        public void RunDiscussionPaper()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.ConsiderationDocument);

            var frm = new MeetingDocumentDetailListForm();
            frm.Show();
        }

        /// <summary>
        /// ＣＡＰ・商品力フォームの起動
        /// </summary>
        public void RunCapAgenda()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.CAP);

            var frm = new CapAndProductForm();
            frm.Show();
        }

        /// <summary>
        /// 設計チェックフォームの起動
        /// </summary>
        public void RunDesignCheck()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.DesignCheck);

            //Update Start 2023/02/28 杉浦 ボタン押下時にPC端末を決定する
            //var frm = new DesignCheckListForm();
            string pcAuthKind = string.Empty;
            using (var pcAuth = new DesignCheckPcAuthForm())
            {
                if (pcAuth.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                pcAuthKind = pcAuth.PcAuthKind;
            }
            var frm = new DesignCheckListForm() { PcAuthKind = pcAuthKind };
            //Update End 2023/02/28 杉浦 ボタン押下時にPC端末を決定する

            frm.Show();
        }

        /// <summary>
        /// ロール設定フォームの起動
        /// </summary>
        public void RunRoll()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.Roll);

            var frm = new FunctionAuthorityRollSettingForm();
            frm.Show();

            //閉じたタイミングで再取得
            frm.Closed += (sender, e) => this.SetAnnounce();
        }

        /// <summary>
        /// 機能権限設定フォームの起動
        /// </summary>
        public void RunFunctionAuth()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.FunctionAuthority);

            //var frm = new FunctionAuthorityForm();
            var frm = new FunctionAuthorityRollForm();
            frm.Show();

            //閉じたタイミングで再取得
            frm.Closed += (sender, e) => this.SetAnnounce();
        }

        /// <summary>
        /// 閲覧権限設定フォームの起動
        /// </summary>
        public void RunGeneralCodeBrowsing()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.BrowsingAuthority);

            var frm = new BrowsingAuthorityForm();
            frm.Show();

            //閉じたタイミングで再取得
            frm.Closed += (sender, e) => this.SetAnnounce();
        }

        /// <summary>
        /// お知らせ一覧フォームの起動
        /// </summary>
        public void RunAnnounce()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.Notice);

            var frm = new AnnounceListForm();
            frm.Show();

            //閉じたタイミングで再取得
            frm.Closed += (sender, e) => this.SetAnnounce();

        }
        //Append Start 2022/01/11 杉浦 使用履歴設定画面の追加
        /// <summary>
        /// 使用履歴設定フォームの起動
        /// </summary>
        public void RunUseHistory()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new ExceptMonthlyInspectionForm() { UserAuthority = GetFunction(FunctionID.TestCarManagement) };
            frm.Show();
        }
        //Append End 2022/01/11 杉浦 使用履歴設定画面の追加
        #endregion

        #region 試験車管理フォームの起動
        /// <summary>
        /// Xeyeページへの接続フォームの起動
        /// </summary>
        /// <param name="url"></param>
        /// <param name="arg"></param>
        public void RunXeye(string url)
        {
            var frm = new WebBrowserForm(url);
            frm.Show();

            //閉じたタイミングで再取得
            frm.Closed += (sender, e) => this.SetAnnounce();
        }

        /// <summary>
        /// ログオフメニュークリック
        /// </summary>
        protected void RunControlSheetList()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new UITestCar.ControlSheet.ControlSheetListForm();
            frm.Show();

        }

        /// <summary>
        /// ラベル印刷フォーム起動
        /// </summary>
        protected void RunControlLabelIssue()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new ControlLabelIssueForm();
            frm.Show();

        }

        /// <summary>
        /// 廃却期限リストフォーム起動
        /// </summary>
        protected void RunDisposalPeriod()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new DisposalDeadlineListForm();
            frm.Show();

        }

        /// <summary>
        /// 廃却車両搬出日入力フォーム起動
        /// </summary>
        protected void RunDisposalCarryout()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new DisposalDepartureInputForm();
            frm.Show();

        }

        /// <summary>
        /// 処理待ち車両リストフォーム起動
        /// </summary>
        protected void RunApplicationApprovalCar()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new ApplicationApprovalCarForm();
            frm.Show();
        }

        /// <summary>
        /// 試験車使用履歴フォーム起動
        /// </summary>
        protected void RunTestCarHistory()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new TestCarHistoryListForm();
            frm.Show();

        }

        /// <summary>
        /// 管理票検索フォーム起動
        /// </summary>
        /// <param name="cond">検索条件</param>
        protected void RunControlSheet(ControlSheetSearchConditionModel cond = null)
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new ControlSheet.ControlSheetListForm { UserSearchCondition = cond };
            frm.Show();

        }

        /// <summary>
        /// 指定月台数リストフォーム起動
        /// </summary>
        protected void RunDesignatedMonthNumber()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new DesignatedMonthNumberForm();
            frm.Show();

        }

        /// <summary>
        /// 車検・点検リストフォーム起動
        /// </summary>
        protected void RunCarInspectionForm()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new CarInspectionForm();
            frm.Show();

        }

        /// <summary>
        /// 処理待ち状況フォーム起動
        /// </summary>
        public void RunPendingCar()
        {
            // 試験車管理の参照権限がない場合は終了
            if (!IsFunctionEnable(FunctionID.TestCarManagement))
            {
                return;
            }

            // 処理待ち情報の取得
            var list = this.GetPendingCarList();

            // 処理待ち車両がない場合は終了
            if (list == null || !list.Any())
            {
                return;
            }

            // 操作ログ登録
            SetOperationLog(FunctionID.TestCarManagement);

            var frm = new PendingCarForm() { UserAuthority = this.GetFunctionList(FunctionID.TestCarManagement).FirstOrDefault(), TempDataList = list };
            frm.Show();

            // 前面表示
            frm.Activate();

        }

        /// <summary>
        /// Xeyeデータ取込フォームの起動
        /// </summary>
        public void RunReadXeyeCsv()
        {
            // 操作ログ登録
            SetOperationLog(FunctionID.XeyeReadCsv);

            var frm = new ReadCsvForm();
            frm.Show();
        }
        #endregion

        #endregion

        #region 共通機能

        #region お知らせ一覧
        /// <summary>
        /// お知らせ設定
        /// </summary>
        /// <param name="grid">グリッド</param>
        protected void SetAnnounce(DataGridView grid)
        {
            //グリッドをセットしているかどうか
            if (this.announceDataGridView == null)
            {
                //操作対象のグリッド
                this.announceDataGridView = grid;

                //列の自動生成は無効
                grid.AutoGenerateColumns = false;

            }

            //お知らせ設定
            this.SetAnnounce();

        }

        /// <summary>
        /// お知らせ設定
        /// </summary>
        protected void SetAnnounce()
        {
            //操作対象のグリッドがあれば設定
            if (this.announceDataGridView != null)
            {
                this.announceDataGridView.DataSource = this.GetAnnounceListByOpen();

            }

        }

        /// <summary>
        /// お知らせ一覧取得(公開中)
        /// </summary>
        /// <returns></returns>
        protected List<InformationOutModel> GetAnnounceListByOpen()
        {
            var cond = new InformationInModel
            {
                STATUS = 1
            };

            return this.GetAnnounceList(cond);

        }
        #endregion

        #region 機能権限
        /// <summary>
        /// 機能権限チェック
        /// </summary>
        /// <param name="function">機能ID</param>
        /// <param name="isManagement">管理権限参照可否</param>
        /// <returns></returns>
        public bool IsFunctionEnable(FunctionID function, bool isManagement = false)
        {
            var id = (int)function;

            var kengen = this.FunctionAuthList.FirstOrDefault(x => x.FUNCTION_ID == id && x.READ_FLG == '1');

            //管理権限も見るかどうか
            if (isManagement == false)
            {
                return kengen != null;
            }

            //管理権限も参照
            return kengen?.MANAGEMENT_FLG == '1';
        }
        #endregion

        #region 検索条件表示設定
        /// <summary>
        /// 検索条件表示設定
        /// </summary>
        /// <param name="button">検索条件ボタン</param>
        /// <param name="table">検索条件</param>
        /// <param name="panel">一覧</param>
        /// <param name="height">高さ</param>
        protected void SearchConditionVisible(Button button, TableLayoutPanel table, Panel panel, int height)
        {
            this.SearchConditionVisible(button, table, panel, null, null, height);
        }

        /// <summary>
        /// 検索条件表示設定
        /// </summary>
        /// <param name="button">検索条件ボタン</param>
        /// <param name="table">検索条件</param>
        /// <param name="panel">一覧</param>
        /// <param name="outCtrl">設定外コントロール</param>
        /// <param name="height">高さ</param>
        protected void SearchConditionVisible(Button button, TableLayoutPanel table, Panel panel, IEnumerable<Control> outCtrl, int height)
        {
            this.SearchConditionVisible(button, table, panel, outCtrl, null, height);
        }

        /// <summary>
        /// 検索条件表示設定
        /// </summary>
        /// <param name="button">検索条件ボタン</param>
        /// <param name="table">検索条件</param>
        /// <param name="panel">一覧</param>
        /// <param name="outCtrl">設定外コントロール</param>
        /// <param name="inCtrl">設定内コントロール</param>
        /// <param name="height">高さ</param>
        protected void SearchConditionVisible(Button button, TableLayoutPanel table, Panel panel, IEnumerable<Control> outCtrl, IEnumerable<Control> inCtrl, int height)
        {
            var flg = !table.Visible;

            // スケール調整
            height = Convert.ToInt32(height* new DeviceUtil().GetScalingFactor());

            // 設定内コントロールがあるかどうか
            if (inCtrl != null && inCtrl.Any() == true)
            {
                foreach (var c in inCtrl)
                {
                    c.Visible = flg;
                }
            }
            
            // 検索条件を非表示にするかどうか
            if (flg == true)
            {
                height *= -1;
            }

            // 縦位置
            var location = panel.Location;
            location.Y -= height;
            panel.Location = location;

            // 縦幅
            var size = panel.Size;
            size.Height += height;
            panel.Size = size;

            // 検索条件ボタン
            button.Text = this.SearchConditionButtonText[flg];

            // 検索条件
            table.Visible = flg;

            // 設定外コントロールがあるかどうか
            if (outCtrl != null && outCtrl.Any() == true)
            {
                foreach (var c in outCtrl)
                {
                    var l = c.Location;
                    l.Y -= height;
                    c.Location = l;
                }
            }
        }
        #endregion

        #region ユーザー検索条件
        /// <summary>
        /// ユーザー検索条件の名前を取得
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns></returns>
        protected List<ControlSheetSearchConditionModel> GetUserSearchConditionNameList(string userId)
        {
            return this.GetUserSearchConditionNameList(this.GetUserSearchConditionList(userId));

        }

        /// <summary>
        /// ユーザー検索条件の名前を取得
        /// </summary>
        /// <param name="list">ユーザー検索条件</param>
        /// <returns></returns>
        protected List<ControlSheetSearchConditionModel> GetUserSearchConditionNameList(List<ControlSheetSearchConditionModel> list)
        {
            return list
                .Where(x => x.条件名 != DefaultName)
                .Select(x => new { ユーザーID = x.ユーザーID, 条件名 = x.条件名 })
                .Distinct()
                .Select(x => new ControlSheetSearchConditionModel { ユーザーID = x.ユーザーID, 条件名 = x.条件名 })
                .ToList();

        }
        #endregion

        #endregion

        #region MultiRow

        #region 子画面表示
        /// <summary>
        /// 表示設定画面表示
        /// </summary>
        /// <param name="template">MultiRowカスタムテンプレート</param>
        protected void ShowDisplayForm(CustomTemplate template)
        {
            this.ShowDisplayForm(this.FormTitle, template);
        }

        /// <summary>
        /// 表示設定画面表示
        /// </summary>
        /// <param name="formName">画面名</param>
        /// <param name="template">MultiRowカスタムテンプレート</param>
        protected void ShowDisplayForm(string formName, CustomTemplate template)
        {
            using (var form = new MultiRowDisplayForm { FormName = formName, Display = template.Display })
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // 表示反映
                    template.Display = form.Display;
                }
            }
        }

        /// <summary>
        /// ソート指定画面表示
        /// </summary>
        /// <param name="template">MultiRowカスタムテンプレート</param>
        protected void ShowSortForm(CustomTemplate template)
        {
            using (var form = new MultiRowSortForm { Sort = template.Sort, SortTarget = template.GetSortTarget() })
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // ソート反映
                    template.Sort = form.Sort;
                }
            }
        }
        #endregion

        #endregion

        #region WebAPI
        /// <summary>
        /// 機能権限一覧取得
        /// </summary>
        /// <param name="id">機能ID</param>
        /// <returns></returns>
        public List<UserAuthorityOutModel> GetFunctionList(FunctionID? id = null)
        {
            // ログインセッション情報がある場合
            if (SessionDto.UserAuthorityList != null)
            {
                return id == null 
                    ? SessionDto.UserAuthorityList 
                    : SessionDto.UserAuthorityList.Where(x => x.FUNCTION_ID == (int)id)?.ToList();
            }

            var list = new List<UserAuthorityOutModel>();

            var cond = new UserAuthorityInModel
            {
                //部ID
                DEPARTMENT_ID = SessionDto.DepartmentID,

                //課ID
                SECTION_ID = SessionDto.SectionID,

                //担当ID
                SECTION_GROUP_ID = SessionDto.SectionGroupID,

                //役職
                OFFICIAL_POSITION = SessionDto.OfficialPosition,

                //ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                //機能ID
                FUNCTION_ID = id == null ? null : (int?)id.Value

            };

            //APIで取得
            var res = HttpUtil.GetResponse<UserAuthorityInModel, UserAuthorityOutModel>(ControllerType.UserAuthority, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// 権限の取得
        /// </summary>
        /// <param name="id">機能ID</param>
        /// <returns></returns>
        protected UserAuthorityOutModel GetFunction(FunctionID id)
        {
            return this.GetFunctionList(id).Select(x => { x.ROLL_ID_LIST = x.ROLL_IDS.Split(',').ToList(); return x; }).FirstOrDefault() ?? new UserAuthorityOutModel
            {
                //機能ID
                FUNCTION_ID = (int)id,

                //参照フラグ
                READ_FLG = '0',

                //更新フラグ
                UPDATE_FLG = '0',

                //出力フラグ
                EXPORT_FLG = '0',

                //管理フラグ
                MANAGEMENT_FLG = '0',

                //プリントスクリーンフラグ
                PRINTSCREEN_FLG = '0',

                //カーシェア事務所フラグ
                CARSHARE_OFFICE_FLG = '0',

                //全閲覧権限フラグ
                ALL_GENERAL_CODE_FLG = '0',
                
                //SKSフラグ
                SKS_FLG = '0',

                //自部署編集フラグ
                JIBU_UPDATE_FLG = '0',

                //自部署印刷フラグ
                JIBU_EXPORT_FLG = '0',

                //自部署管理フラグ
                JIBU_MANAGEMENT_FLG = '0',

                //ロールID(カンマ区切り)
                ROLL_IDS = string.Empty
            };

        }

        /// <summary>
        /// お気に入り一覧取得
        /// </summary>
        /// <returns></returns>
        public List<FavoriteSearchOutModel> GetFavoriteList()
        {
            var cond = new FavoriteSearchInModel
            {
                PERSONEL_ID = SessionDto.UserId,
                CLASS_DATA = "00"
            };

            var list = new List<FavoriteSearchOutModel>();

            var res = HttpUtil.GetResponse<FavoriteSearchInModel, FavoriteSearchOutModel>(ControllerType.Favorite, cond);

            if (res != null && res.Status == Const.StatusSuccess && res.Results != null && res.Results.Any() == true)
            {
                list.AddRange(res.Results);

            }

            return list;
        }

        /// <summary>
        /// お知らせ一覧取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        protected List<InformationOutModel> GetAnnounceList(InformationInModel cond)
        {
            var list = new List<InformationOutModel>();
            var res = HttpUtil.GetResponse<InformationInModel, InformationOutModel>(ControllerType.Information, cond);

            if (res != null && res.Status == Const.StatusSuccess && res.Results != null && res.Results.Any() == true)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// ユーザー検索条件取得
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="name">条件名</param>
        /// <returns></returns>
        protected List<ControlSheetSearchConditionModel> GetUserSearchConditionList(string userId = null, string name = null)
        {
            var cond = new ControlSheetSearchConditionSearchModel
            {
                //ユーザーID
                ユーザーID = userId,

                //条件名
                条件名 = name

            };

            //APIで取得
            var res = HttpUtil.GetResponse<ControlSheetSearchConditionSearchModel, ControlSheetSearchConditionModel>(ControllerType.ControlSheetSearchCondition, cond);

            //レスポンスが取得できたかどうか
            var list = new List<ControlSheetSearchConditionModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// 操作ログ登録
        /// </summary>
        /// <param name="functionid">機能ID</param>
        /// <returns>実行結果</returns>
        public void SetOperationLog(FunctionID functionid)
        {
            var info = new string[]
            {
                // IP(PC)
                (Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) as IPAddress)?.ToString(),
                // PC名(PC)
                Environment.MachineName,
                // OSバージョン(PC)
                Environment.OSVersion.ToString(),
                // ログオン名(PC)
                Environment.UserName,
                // ログインID(APP)
                SessionDto.UserId,
                // 機能ID
                functionid.ToString()
            };

            //APIで登録
            Task.Run(() => { HttpUtil.GetResponse<OperationLogModel, object>(ControllerType.OperationLog, new OperationLogModel { INFO = string.Join(",", info) }); });
        }

        /// <summary>
        /// 処理待ち状況の取得
        /// </summary>
        /// <returns></returns>
        private List<PendingCarModel> GetPendingCarList()
        {
            // APIで取得
            var res = HttpUtil.GetResponse<PendingCarSearchModel, PendingCarModel>
                (ControllerType.PendingCar, new PendingCarSearchModel { PERSONEL_ID = SessionDto.UserId, IPPAN_FLAG = Properties.Settings.Default.MyPendingCar });

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return null;
            }

            return res.Results.ToList();
        }

        /// <summary>
        /// ユーザー表示設定情報取得（テンプレート定義）
        /// </summary>
        /// <param name="template">テンプレート</param>
        /// <returns></returns>
        protected List<MultiRowDisplayModel> GetUserDisplayConfiguration(Template template)
        {
            return this.GetUserDisplayConfiguration(this.FormTitle, template);
        }

        /// <summary>
        /// ユーザー表示設定情報取得（テンプレート定義）
        /// </summary>
        /// <param name="formName">画面名</param>
        /// <param name="template">テンプレート</param>
        /// <returns></returns>
        protected List<MultiRowDisplayModel> GetUserDisplayConfiguration(string formName, Template template)
        {
            var list = new List<MultiRowDisplayModel>();

            var cond = new UserDisplayConfigurationSearchModel
            {
                // ユーザーID
                ユーザーID = SessionDto.UserId,

                // 画面名
                画面名 = formName

            };

            // APIで取得
            var res = HttpUtil.GetResponse<UserDisplayConfigurationSearchModel, UserDisplayConfigurationModel>(ControllerType.UserDisplayConfiguration, cond);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return list;
            }

            // 取得データの格納
            var data = res.Results.FirstOrDefault();

            // 取得できたかどうか
            if (data == null)
            {
                return list;
            }

            var headerCells = template.ColumnHeaders[0].Cells;
            var dataCells = template.Row;

            foreach (var displayName in (data.表示列名 ?? "").Split(',').Select(x => x.Trim()))
            {
                // 表示対象の列かどうか
                var col = headerCells.FirstOrDefault(x => x.Value.ToString().Trim().ToLower() == displayName.ToLower());
                if (col != null)
                {
                    list.Add(new MultiRowDisplayModel
                    {
                        Name = col.Name,
                        HeaderText = col.Value.ToString(),
                        DataPropertyName = dataCells.Cells[col.CellIndex].DataField,
                        Visible = true,
                        DisplayIndex = col.CellIndex,
                    });
                }
            }

            if (!string.IsNullOrEmpty(data.非表示列名))
            {
                foreach (var displayName in (data.非表示列名 ?? "").Split(',').Select(x => x.Trim()))
                {
                    // 非表示の列かどうか
                    var col = headerCells.FirstOrDefault(x => x.Value.ToString().Trim().ToLower() == displayName.ToLower());
                    if (col != null)
                    {
                        list.Add(new MultiRowDisplayModel
                        {
                            Name = col.Name,
                            HeaderText = col.Value.ToString(),
                            DataPropertyName = dataCells.Cells[col.CellIndex].DataField,
                            Visible = false,
                            DisplayIndex = col.CellIndex,
                        });
                    }
                }
            }

            // 表示設定対象外列の追加
            foreach (var col in headerCells.Where(x => string.IsNullOrWhiteSpace((string)x.Value) == true && x.Width > 0))
            {
                list.Insert(col.CellIndex, new MultiRowDisplayModel
                {
                    Name = col.Name,
                    HeaderText = string.Empty,
                    DataPropertyName = dataCells.Cells[col.CellIndex].DataField,
                    Visible = col.Visible,
                    DisplayIndex = col.CellIndex,
                });
            }

            // 表示順再設定
            var i = 0;
            list.ForEach(x => x.DisplayIndex = i++);

            return list;
        }

        /// <summary>
        /// ユーザー表示設定情報取得（連想配列定義）
        /// </summary>
        /// <param name="display">表示列</param>
        /// <returns></returns>
        protected List<MultiRowDisplayModel> GetUserDisplayConfiguration(Dictionary<string, CustomMultiRowCellStyle> display)
        {
            return this.GetUserDisplayConfiguration(this.FormTitle, display);
        }

        /// <summary>
        /// ユーザー表示設定情報取得（連想配列定義）
        /// </summary>
        /// <param name="formName">画面名</param>
        /// <param name="display">表示列</param>
        /// <returns></returns>
        protected List<MultiRowDisplayModel> GetUserDisplayConfiguration(string formName, Dictionary<string, CustomMultiRowCellStyle> display)
        {
            var list = new List<MultiRowDisplayModel>();

            var cond = new UserDisplayConfigurationSearchModel
            {
                // ユーザーID
                ユーザーID = SessionDto.UserId,

                // 画面名
                画面名 = formName
            };

            // APIで取得
            var res = HttpUtil.GetResponse<UserDisplayConfigurationSearchModel, UserDisplayConfigurationModel>(ControllerType.UserDisplayConfiguration, cond);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return list;
            }

            // 取得データの格納
            var data = res.Results.FirstOrDefault();

            // 取得できたかどうか
            if (data == null)
            {
                return list;
            }

            var headerName = "Header{0}";

            foreach (var displayName in (data.表示列名 ?? "").Split(',').Select(x => x.Trim()))
            {
                // 表示対象の列かどうか
                var col = display.FirstOrDefault(x => x.Value.HeaderText.Trim().ToLower() == displayName.ToLower());
                if (col.Key != null)
                {
                    list.Add(new MultiRowDisplayModel
                    {
                        Name = string.Format(headerName, col.Key),
                        HeaderText = col.Value.HeaderText,
                        DataPropertyName = col.Key,
                        Visible = true,
                        DisplayIndex = col.Value.DisplayIndex,
                    });
                }
            }

            foreach (var displayName in (data.非表示列名 ?? "").Split(',').Select(x => x.Trim()))
            {
                // 非表示の列かどうか
                var col = display.FirstOrDefault(x => x.Value.HeaderText.Trim().ToLower() == displayName.ToLower());
                if (col.Key != null)
                {
                    list.Add(new MultiRowDisplayModel
                    {
                        Name = string.Format(headerName, col.Key),
                        HeaderText = col.Value.HeaderText,
                        DataPropertyName = col.Key,
                        Visible = false,
                        DisplayIndex = col.Value.DisplayIndex,
                    });
                }
            }

            // 表示順再設定
            var i = 0;
            list.ForEach(x => x.DisplayIndex = i++);

            return list;
        }

        #endregion

        #endregion

    }
}
