using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Config;
using DevPlan.Presentation.Base;

namespace DevPlan.Presentation
{
    /// <summary>
    /// メニュー
    /// </summary>
    public partial class MenuForm : BaseForm
    {
        #region メンバ変数
        /// <summary>
        /// アイコン名一覧と機能IDの紐付
        /// </summary>
        private Dictionary<Button, FunctionID> buttonListDictionary = null;

        private Button[] mainMenuButtonList = null;

        // 20180424 Ron Edit Start
        private Button[] mainMenuButtonListHide = null;
        // 20180424 Ron Edit End

        private Button[] configButtonList = null;
        private Button[] testCarList = null;
        private Button[] testCarManagementList = null;

        private Button[] isManagementList = null;

        private DeviceUtil deviceUtil = new DeviceUtil();
        private AppConfigAccessor appConfig = new AppConfigAccessor();
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        #endregion

        #region 定義
        //Update Start 2022/07/03 URL格納場所の変更
        //private const string urlOldDevPlanManifesto = "http://gj1tds.gkh.auto3.subaru.net/DevPlan3/DevPlan3Starter.application";
        private string urlOldDevPlanManifesto;
        //Update End 2022/07/03 URL格納場所の変更
        #endregion

        #region プロパティ
        /// <summary>タイトル</summary>
        public override string FormTitle { get { return "メインメニュー"; } }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MenuForm()
        {
            InitializeComponent();

        }
        #endregion

        #region フォームロード
        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_Load(object sender, EventArgs e)
        {
            // AD情報の設定
            this.SetAD(tokenSource.Token);

            // 各項目の初期化
            this.InitForm();

            // グローバルフック開始
            this.GlobalHookStart();
        }

        /// <summary>
        /// 各項目の初期化
        /// </summary>
        private void InitForm()
        {
            //アイコン名一覧と機能IDの紐付
            this.buttonListDictionary = new Dictionary<Button, FunctionID>
            {
                // 業務計画表
                { this.OperationPlanButton, FunctionID.Plan },

                // 月次計画表
                { this.MonthlyPlanButton, FunctionID.Plan },

                // 目標進度リスト
                { this.TargetProgressButton, FunctionID.GoalProgress }, 

                // 車種別進捗状況一覧 
                { this.ProgressStatusListButton, FunctionID.Plan },

                // カーシェア一覧 
                { this.CarShareButton, FunctionID.CarSearch },

                // 試験車日程 
                { this.TestCarScheduleButton, FunctionID.TestCar },

                // 外製車日程 
                { this.OtherMakersCarButton, FunctionID.OuterCar },

                // カーシェア日程
                { this.CarSharePlanButton, FunctionID.CarShare },

                // カーシェア管理
                { this.CarShareManageButton, FunctionID.CarShareOffice },

                // トラック予約表
                { this.TruckScheduleButton, FunctionID.Truck },

                // Xeye車両検索
                { this.XeyeButton, FunctionID.Login },

                // 週報
                { this.WeeklyReportButton, FunctionID.WeeklyReport },

                // 月報
                { this.MonthlyReportButton, FunctionID.MonthlyReport },

                // 検討会資料
                { this.DiscussionPaperButton, FunctionID.ConsiderationDocument },

                // CAP課題一覧
                { this.CapAgendaButton, FunctionID.CAP },

                // 設計チェック一覧
                { this.DesignCheckButton, FunctionID.DesignCheck },

                // ロール
                { this.RollButton, FunctionID.Roll },

                // 機能権限
                { this.FunctionAuthButton, FunctionID.FunctionAuthority },

                // 閲覧権限
                { this.GeneralCodeBrowsingButton, FunctionID.BrowsingAuthority },

                // お知らせ管理
                { this.AnnounceButton, FunctionID.Notice },

                // 管理メニュー
                { this.ConfigButton, FunctionID.FunctionAuthority },

                // 開発計画表(現行システム)
                { this.OldDevPlanButton, FunctionID.Login },

                // メインメニュー
                { this.MainMenuButton, FunctionID.FunctionAuthority },

                //試験車管理(管理者)
                { this.TestCarManagementButton, FunctionID.TestCarManagement },

                //試験車管理
                { this.TestCarButton, FunctionID.TestCarManagement },

                //試験車リスト
                { this.ControlSheetListButton, FunctionID.TestCarManagement },

                //ラベル印刷
                { this.ControlLabelIssueButton, FunctionID.TestCarManagement },

                //廃却期限リスト
                { this.DisposalPeriodButton, FunctionID.TestCarManagement },

                //廃却車両搬出日入力
                { this.DisposalCarryoutButton, FunctionID.TestCarManagement },

                //処理待ち車両リスト
                { this.ApplicationApprovalCarButton, FunctionID.TestCarManagement },

                //試験車使用履歴
                { this.TestCarHistoryButton, FunctionID.TestCarManagement },

                //管理票検索
                { this.ControlSheetButton, FunctionID.TestCarManagement },

                //指定月台数リスト
                { this.DesignatedMonthNumberButton, FunctionID.TestCarManagement },

                //車検・点検リスト発行
                { this.CarInspectionButton, FunctionID.TestCarManagement },

                //メインメニュー(試験車管理)
                { this.TestCarMainMenuButton, FunctionID.TestCarManagement },

                // 機能権限
                { this.XeyeReadCsvButton, FunctionID.XeyeReadCsv },
                
                //Append Start 2022/01/11 杉浦 使用履歴設定画面の追加
                // 使用履歴設定
                { this.SettingUseHistoryButton, FunctionID.TestCarManagement }
                //Append End 2022/01/11 杉浦 使用履歴設定画面の追加

            };


            // 20180424 Ron Edit Start

            ////メインメニューのボタン
            //this.mainMenuButtonList = new[]
            //{
            //    this.OperationPlanButton, this.MonthlyPlanButton, this.ProgressStatusListButton, this.TargetProgressButton, this.CarShareButton, this.TestCarScheduleButton,
            //    this.OtherMakersCarButton, this.CarSharePlanButton, this.CarShareManageButton, this.WeeklyReportButton, this.MonthlyReportButton, this.DiscussionPaperButton,
            //    this.CapAgendaButton, this.DesignCheckButton, this.ConfigButton,
            //    this.TestCarManagementButton, this.TestCarButton

            //};

            //メインメニューのボタン
            this.mainMenuButtonList = new[]
            {
                this.CarShareButton, this.TestCarScheduleButton,
                this.OtherMakersCarButton, this.CarSharePlanButton, this.CarShareManageButton,
                this.TruckScheduleButton, this.XeyeButton, this.CapAgendaButton, this.DesignCheckButton,
                this.ConfigButton, this.OldDevPlanButton,
                this.TestCarManagementButton, this.TestCarButton
            };

            this.mainMenuButtonListHide = new[]
            {
                this.OperationPlanButton, this.MonthlyPlanButton, this.ProgressStatusListButton, this.TargetProgressButton, this.WeeklyReportButton,
                this.MonthlyReportButton, this.DiscussionPaperButton
            };

            // 20180424 Ron Edit End


            //設定メニューのボタン
            //Append Start 2022/01/11 杉浦 使用履歴設定画面の追加
            //this.configButtonList = new[] { this.RollButton, this.FunctionAuthButton, this.GeneralCodeBrowsingButton, this.AnnounceButton, this.XeyeReadCsvButton, this.MainMenuButton };
            this.configButtonList = new[] { this.RollButton, this.FunctionAuthButton, this.GeneralCodeBrowsingButton, this.AnnounceButton, this.XeyeReadCsvButton, this.SettingUseHistoryButton, this.MainMenuButton };
            //Append End 2022/01/11 杉浦 使用履歴設定画面の追加

            //試験車管理メニュー(管理者)のボタン
            this.testCarManagementList = new[]
            {
                this.ControlSheetListButton, this.ControlLabelIssueButton, this.DisposalPeriodButton, this.DisposalCarryoutButton, this.ApplicationApprovalCarButton,
                this.TestCarHistoryButton, this.ControlSheetButton, this.DesignatedMonthNumberButton, this.CarInspectionButton,
                this.TestCarMainMenuButton
            };

            //試験車管理メニューのボタン
            this.testCarList = new[]
            {
                this.DisposalPeriodButton, this.ApplicationApprovalCarButton,
                this.TestCarHistoryButton, this.ControlSheetButton, this.DesignatedMonthNumberButton, this.CarInspectionButton,
                this.TestCarMainMenuButton
            };

            //管理権限参照可否のボタン
            this.isManagementList = new[]
            {
                this.TestCarManagementButton, this.SettingUseHistoryButton

            };

            //スクロール無効化
            this.AutoScrollMinSize = new Size(0, 0);
            this.AutoScroll = false;

            //メニューボタンの設定
            this.SetButtonVisible(this.mainMenuButtonList, true);

            // 20180424 Ron Edit Start
            this.SetButtonVisible(this.mainMenuButtonListHide, false);
            // 20180424 Ron Edit End

            this.SetButtonVisible(this.configButtonList, false);
            this.SetButtonVisible(this.testCarManagementList, false);
            this.SetButtonVisible(this.testCarList, false);

            // お知らせ
            base.SetAnnounce(this.AnnounceDataGridView);
        }
        #endregion

        #region フォーム表示
        /// <summary>
        /// フォーム表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_Shown(object sender, EventArgs e)
        {
            // 処理待ち状況画面の起動
            FormControlUtil.FormWait(this, base.RunPendingCar);
        }
        #endregion

        #region フォームクローズ
        /// <summary>
        /// フォームクローズ中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 非同期タスクのキャンセル
            tokenSource.Cancel();
        }

        /// <summary>
        /// フォームクローズ後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // グローバルフック終了
            this.GlobalHookStop();

            //全ての子画面を閉じる
            Application.Exit();
        }
        #endregion

        #region ボタン表示切替
        /// <summary>
        /// ボタン表示切替
        /// </summary>
        /// <param name="list">ボタン</param>
        /// <param name="visible">表示可否</param>
        private void SetButtonVisible(IEnumerable<Button> list, bool visible)
        {
            foreach (var button in list)
            {
                this.SetButtonVisible(button, visible);
            }

            if (visible == true)
            {
                list.FirstOrDefault(x => x.Visible == true)?.Focus();
            }
        }

        /// <summary>
        /// ボタン表示切替
        /// </summary>
        /// <param name="button">ボタン</param>
        /// <param name="visible">ボタン表示切替</param>
        private bool SetButtonVisible(Button button, bool visible)
        {
            var flg = false;

            if (visible == true)
            {
                button.Visible = flg = base.IsFunctionEnable(this.buttonListDictionary[button], this.isManagementList.Contains(button));
            }
            else
            {
                button.Visible = flg;
            }


            if (visible && !flg)
            {
                if (button == this.ConfigButton || button == this.MainMenuButton)
                {
                    var ids = new List<int>() { (int)FunctionID.Notice, (int)FunctionID.Roll, (int)FunctionID.FunctionAuthority, (int)FunctionID.BrowsingAuthority, (int)FunctionID.XeyeReadCsv };

                    button.Visible = flg = this.FunctionAuthList.Any(x => (ids.Contains(x.FUNCTION_ID) && x.READ_FLG == '1') || (x.FUNCTION_ID == (int)FunctionID.TestCarManagement && x.MANAGEMENT_FLG == '1'));
                }
            }

            return flg;
        }
        #endregion

        #region メニューボタン共通処理
        /// <summary>
        /// メニューボタン共通クリック処理
        /// </summary>
        /// <param name="action"></param>
        private void MenuButtonClickAction(Action action)
        {
            // 待機カーソル
            FormControlUtil.FormWait(this, action);

            // 連続クリック禁止
            this.SetTimerFormEnabled();
        }

        /// <summary>
        /// 右クリックメニュー追加
        /// </summary>
        /// <param name="button"></param>
        private void AddContextMenu(Button button)
        {
            var cms = new ContextMenuStrip();
            var classdata = string.Empty;

            switch (button.Name)
            {
                // 業務計画表
                case "OperationPlanButton":
                    classdata = "01";
                    cms = base.OperationPlanButtonFavoriteItem;
                    break;

                // 月次計画表
                case "MonthlyPlanButton":
                    classdata = "02";
                    cms = base.MonthlyPlanButtonFavoriteItem;
                    break;

                // 車種別進捗状況一覧
                case "ProgressStatusListButton":
                    classdata = "06";
                    cms = base.ProgressStatusListButtonFavoriteItem;
                    break;

                // 試験車日程
                case "TestCarScheduleButton":
                    classdata = "03";
                    cms = base.TestCarScheduleButtonFavoriteItem;
                    break;

                // 外製車日程
                case "OtherMakersCarButton":
                    classdata = "05";
                    cms = base.OtherMakersCarButtonFavoriteItem;
                    break;

                // カーシェア日程
                case "CarSharePlanButton":
                    classdata = "04";
                    cms = base.CarSharePlanButtonFavoriteItem;
                    break;
            }

            button.ContextMenuStrip = cms;
            cms.Items.Clear();

            if (!string.IsNullOrWhiteSpace(classdata))
            {
                // お気に入りリストの再取得
                base.FavoriteList = base.GetFavoriteList();

                // コンテキストメニュ作成
                foreach (var fsom in base.FavoriteList.Where(x => x.CLASS_DATA == classdata))
                {
                    var item = new ToolStripMenuItem(fsom.TITLE, null, base.CommonMenu_Click)
                    {
                        Name = button.Name,
                        Tag = fsom.ID
                    };
                    cms.Items.Add(item);
                }
            }
        }
        #endregion

        #region メニューボタン共通イベント
        /// <summary>
        /// メニューボタンマウスダウン
        /// </summary>
        private void MenuButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 右クリック以外は終了
            if (e.Button != MouseButtons.Right) return;

            // 各ボタンの右クリックメニュー追加
            this.AddContextMenu((Button)sender);
        }
        #endregion

        #region メニューボタンイベント(メイン)
        /// <summary>
        /// 業務計画表ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperationPlanButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunOperationPlan());
        }

        /// <summary>
        /// 月次計画表ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyPlanButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunMonthlyPlan());
        }

        /// <summary>
        /// 車種別進捗状況一覧ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressStatusListButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunProgressStatusList());
        }

        /// <summary>
        /// 目標進度リストボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetProgressButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunTargetProgress());
        }

        /// <summary>
        /// 車両検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunCarShare());
        }

        /// <summary>
        /// 試験車日程ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarScheduleButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunTestCarSchedule());
        }

        /// <summary>
        /// 外製車日程ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherMakersCarButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunOtherMakersCar());
        }

        /// <summary>
        /// カーシェア日程ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarSharePlanButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunCarSharePlan());
        }

        /// <summary>
        /// カーシェア管理ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareManageButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunCarShareManage());
        }

        /// <summary>
        /// トラック予約表ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckScheduleButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunTruckSchedule());
        }

        /// <summary>
        /// 週報ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeeklyReportButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunWeeklyReport());
        }

        /// <summary>
        /// 月報ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyReportButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunMonthlyReport());
        }

        /// <summary>
        /// 検討会資料ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiscussionPaperButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunDiscussionPaper());
        }

        /// <summary>
        /// ＣＡＰ・商品力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapAgendaButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunCapAgenda());
        }

        /// <summary>
        /// 設計チェックボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunDesignCheck());
        }

        /// <summary>
        /// 試験車管理(管理者)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarManagementButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() =>
            {
                //メニューボタンの設定
                this.SetButtonVisible(this.mainMenuButtonList, false);
                this.SetButtonVisible(this.configButtonList, false);
                this.SetButtonVisible(this.testCarList, false);
                this.SetButtonVisible(this.testCarManagementList, true);

                //タイトル設定
                this.SetTitle(this.TestCarManagementButton.Tag.ToString());
            });
        }

        /// <summary>
        /// 試験車管理ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() =>
            {
                //メニューボタンの設定
                this.SetButtonVisible(this.mainMenuButtonList, false);
                this.SetButtonVisible(this.configButtonList, false);
                this.SetButtonVisible(this.testCarManagementList, false);
                this.SetButtonVisible(this.testCarList, true);

                //タイトル設定
                this.SetTitle(this.TestCarButton.Tag.ToString());
            });
        }

        /// <summary>
        /// 設定メニューボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() =>
            {
                //メニューボタンの設定
                this.SetButtonVisible(this.mainMenuButtonList, false);
                this.SetButtonVisible(this.configButtonList, true);
                this.SetButtonVisible(this.testCarManagementList, false);
                this.SetButtonVisible(this.testCarList, false);
            });
        }

        /// <summary>
        /// 開発計画表(旧システム)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OldDevPlanButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() =>
            {
                // ブラウザ起動
                var oWebBrowser = new WebBrowser();
                urlOldDevPlanManifesto = new AppConfigAccessor().GetAppSetting("oldDevPlanPath");
                oWebBrowser.Navigate(new Uri(urlOldDevPlanManifesto));
            });
        }
        /// <summary>
        /// Xeyeボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XeyeButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() =>
            {
                string url = new AppConfigAccessor().GetAppSetting("XeyeSearchURL");
                base.RunXeye(url);
            });
        }
        #endregion

        #region メニューボタン(試験車管理)イベント
        /// <summary>
        /// 試験車リストボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetListButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunControlSheetList());
        }

        /// <summary>
        /// ラベル印刷ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlLabelIssueButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunControlLabelIssue());
        }

        /// <summary>
        /// 廃却期限リストボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisposalPeriodButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunDisposalPeriod());
        }

        /// <summary>
        /// 廃却車両搬出日入力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisposalCarryoutButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunDisposalCarryout());
        }

        /// <summary>
        /// 処理待ち車両リストボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationApprovalCarButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunApplicationApprovalCar());
        }

        /// <summary>
        /// 試験車使用履歴ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarHistoryButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunTestCarHistory());
        }

        /// <summary>
        /// 管理票検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunControlSheet());
        }

        /// <summary>
        /// 管理票検索マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetButton_MouseDown(object sender, MouseEventArgs e)
        {
            //右クリック以外は終了
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            //管理票メニュー項目初期化
            base.InitControlSheetMenuItem(this.ControlSheetContextMenuStrip.Items);

            //項目があれば表示
            if (this.ControlSheetContextMenuStrip.Items.Count > 0)
            {
                //コンテキストメニュー表示
                this.ControlSheetContextMenuStrip.Show(this.ControlSheetButton, e.X, e.Y);
            }
        }

        /// <summary>
        /// 指定月台数リストボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignatedMonthNumberButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunDesignatedMonthNumber());
        }

        /// <summary>
        /// 車検・点検リスト発行ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarInspectionButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunCarInspectionForm());
        }

        /// <summary>
        /// メニューボタン(試験車管理)クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarMainMenuButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() =>
            {
                //メニューボタンの設定
                this.SetButtonVisible(this.mainMenuButtonList, true);
                this.SetButtonVisible(this.configButtonList, false);
                this.SetButtonVisible(this.testCarManagementList, false);
                this.SetButtonVisible(this.testCarList, false);

                //タイトル設定
                this.SetTitle(this.TestCarMainMenuButton.Tag.ToString());
            });
        }
        #endregion

        #region メニューボタン(設定)イベント
        /// <summary>
        /// ロール設定ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunRoll());
        }

        /// <summary>
        /// 機能権限設定ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FunctionAuthButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunFunctionAuth());
        }

        /// <summary>
        /// 閲覧権限設定ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeBrowsingButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunGeneralCodeBrowsing());
        }

        /// <summary>
        /// お知らせ一覧ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnounceButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunAnnounce());
        }
        /// <summary>
        /// Xeyeデータ取込ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XeyeReadCsvButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunReadXeyeCsv());
        }

        //Append Start 2022/01/11 杉浦 使用履歴設定画面の追加
        /// <summary>
        /// 使用履歴設定ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingUseHistoryButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() => base.RunUseHistory());
        }
        //Append End 2022/01/11 杉浦 使用履歴設定画面の追加

        /// <summary>
        /// メインメニューボタン(設定)クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            this.MenuButtonClickAction(() =>
            {
                //メニューボタンの設定
                this.SetButtonVisible(this.mainMenuButtonList, true);
                this.SetButtonVisible(this.configButtonList, false);
                this.SetButtonVisible(this.testCarManagementList, false);
                this.SetButtonVisible(this.testCarList, false);
                this.SetTimerFormEnabled();
            });
        }
        #endregion

        #region お知らせイベント
        /// <summary>
        /// お知らせリストクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnounceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                try
                {
                    var urlCell = this.AnnounceDataGridView.Rows[e.RowIndex].Cells[this.UrlColumn.Name];
                    var url = urlCell.Value == null ? "" : urlCell.Value.ToString();

                    if (string.IsNullOrWhiteSpace(url) == false)
                    {
                        Process.Start(url);
                    }
                }
                catch
                {
                    // リンククリックでエラーが発生した場合は何もしない
                }
            }
        }

        /// <summary>
        /// お知らせツールチップクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnounceLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // お知らせ設定
            base.SetAnnounce();
        }
        #endregion

        #region AD情報の取得
        /// <summary>
        /// AD情報の設定
        /// </summary>
        private async void SetAD(CancellationToken cancelToken)
        {
            try
            {
                await Task.Run(() =>
                {
                    // ADユーティリティ
                    var ad = new ADUtil();

                    ADUserInfoData.Dictionary = ad.AllUserData();

                    return;
                }
                , cancelToken);
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }
        #endregion
        
        #region グローバルフック制御
        /// <summary>
        /// グローバルフック開始
        /// </summary>
        private void GlobalHookStart()
        {
            // デバイス設定
            deviceUtil.IsPrintScreen = base.FunctionAuthList.Any(x => x.PRINTSCREEN_FLG == '1');
            deviceUtil.LoginSessionMonitor = () => LoginSessionMonitor(Convert.ToDouble(appConfig.GetAppSetting("LoginTimeout")));

            // グローバルフック開始
            Task.Run(() => { deviceUtil.SetGlobalHook(); Application.Run(); });
        }

        /// <summary>
        /// グローバルフック終了
        /// </summary>
        private void GlobalHookStop()
        {
            // グローバルフック終了
            deviceUtil.UnGlobalHook();
        }
        #endregion

        #region セッション管理
        /// <summary>
        /// ログインセッション監視
        /// </summary>
        private void LoginSessionMonitor(double timeout = 0)
        {
            var now = DateTime.Now;

            // タイムアウト
            if (((TimeSpan)(now - SessionDto.ActiveDateTime)).TotalSeconds > timeout)
            {
                // マウスフックフック終了
                deviceUtil.UnGlobalHook(false, true);

                // メッセージ表示
                Invoke(new Action(() => Messenger.Info(Resources.KKM01020)));

                // ログオフ
                base.LogoffProcess();

                return;
            }

            // アクティブ日時の更新
            SessionDto.ActiveDateTime = now;
        }
        #endregion
    }
}
