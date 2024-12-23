using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.Presentation.UITestCar.Othe.TestCarHistory;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Util;
using GrapeCity.Win.MultiRow;
using DevPlan.Presentation.UC.MultiRow;

namespace DevPlan.Presentation.UITestCar.Othe.ApplicationApprovalCar
{
    /// <summary>
    /// 処理待ち車両リスト
    /// </summary>
    public partial class ApplicationApprovalCarForm : BaseTestCarForm
    {
        #region メンバ変数
        /// <summary>
        /// バインドソース
        /// </summary>
        private BindingSource DataSource = new BindingSource();

        /// <summary>
        /// カスタムテンプレート
        /// </summary>
        private CustomTemplate CustomTemplate = new CustomTemplate();

        /// <summary>
        /// バインド中可否
        /// </summary>
        private bool IsBind = false;

        /// <summary>
        /// 検索条件
        /// </summary>
        private ApplicationApprovalCarSearchModel searchCondition = null;

        /// <summary>
        /// 検索リスト
        /// </summary>
        private List<ApprovalStepModel> stepList = null;
        #endregion

        #region 定義
        private const int CondHeight = 90;

        private const string Katyou = "0";
        private const string Tantou = "1";
        private const string Tantousya = "2";

        private const string Gunma = "g";
        private const string Toukyou = "t";

        private const string SikenTyakusyu = "B";
        private const string GetureiTenken = "C";
        private const string Haikyaku = "D";
        private const string GunmaIkan = "E";
        private const string ToukyouIkan = "F";
        private const string GunmaToukyouIkan = "G";
        private const string ToukyouGunmaIkan = "H";

        private readonly Dictionary<ViewType, string> ViewMap = new Dictionary<ViewType, string>
        {
            { ViewType.ManagementResponsibility, "管理責任部署" },
            { ViewType.Management, "管理部署" },
            { ViewType.All, "全て" }

        };

        private readonly Dictionary<ApplicationApprovalCarTargetType, string> TargetAllMap = new Dictionary<ApplicationApprovalCarTargetType, string>
        {
            { ApplicationApprovalCarTargetType.TestStartDay, "試験着手日" },
            { ApplicationApprovalCarTargetType.MonthlyInspection, "月例点検" },
            { ApplicationApprovalCarTargetType.DispositionApplication, "廃却申請" },
            { ApplicationApprovalCarTargetType.GunmaTransfer, "G技本内移管" },
            { ApplicationApprovalCarTargetType.ToukyouTransfer, "T技本内移管" },
            { ApplicationApprovalCarTargetType.GtTransfer, "G→T移管" },
            { ApplicationApprovalCarTargetType.TgTransfer, "T→G移管" },
            { ApplicationApprovalCarTargetType.All, "全て" }

        };

        private readonly Dictionary<ApplicationApprovalCarTargetType, string> TargetGunmaMap = new Dictionary<ApplicationApprovalCarTargetType, string>
        {
            { ApplicationApprovalCarTargetType.TestStartDay, "試験着手日" },
            { ApplicationApprovalCarTargetType.MonthlyInspection, "月例点検" },
            { ApplicationApprovalCarTargetType.DispositionApplication, "廃却申請" },
            { ApplicationApprovalCarTargetType.GunmaTransfer, "G技本内移管" },
            { ApplicationApprovalCarTargetType.GtTransfer, "G→T移管" },
            { ApplicationApprovalCarTargetType.TgTransfer, "T→G移管" },
            { ApplicationApprovalCarTargetType.All, "全て" }

        };

        private readonly Dictionary<ApplicationApprovalCarTargetType, string> TargetToukyouMap = new Dictionary<ApplicationApprovalCarTargetType, string>
        {
            { ApplicationApprovalCarTargetType.TestStartDay, "試験着手日" },
            { ApplicationApprovalCarTargetType.MonthlyInspection, "月例点検" },
            { ApplicationApprovalCarTargetType.DispositionApplication, "廃却申請" },
            { ApplicationApprovalCarTargetType.ToukyouTransfer, "T技本内移管" },
            { ApplicationApprovalCarTargetType.GtTransfer, "G→T移管" },
            { ApplicationApprovalCarTargetType.TgTransfer, "T→G移管" },
            { ApplicationApprovalCarTargetType.All, "全て" }

        };

        private readonly Dictionary<ApplicationApprovalCarTargetType, string> SyouninMap = new Dictionary<ApplicationApprovalCarTargetType, string>
        {
            { ApplicationApprovalCarTargetType.TestStartDay, SikenTyakusyu },
            { ApplicationApprovalCarTargetType.MonthlyInspection, GetureiTenken },
            { ApplicationApprovalCarTargetType.DispositionApplication, Haikyaku },
            { ApplicationApprovalCarTargetType.GunmaTransfer, GunmaIkan },
            { ApplicationApprovalCarTargetType.ToukyouTransfer, ToukyouIkan },
            { ApplicationApprovalCarTargetType.GtTransfer, GunmaToukyouIkan },
            { ApplicationApprovalCarTargetType.TgTransfer, ToukyouGunmaIkan },

        };
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "処理待ち車両リスト"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ApplicationApprovalCarForm()
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
        private void ApplicationApprovalCarForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // バインド可否
                this.IsBind = true;

                // 権限
                this.UserAuthority = base.GetFunction(FunctionID.TestCarManagement);

                // 画面初期化
                this.InitForm();

                // MultiRow初期化
                this.InitMultiRow();

                // バインド可否
                this.IsBind = false;

                // 管理所在地の初期選択
                this.AffiliationComboBox.SelectedValue = SessionDto.Affiliation;
            });
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isKanri = SessionDto.ManagementDepartmentType.Any(x => x == Const.Kenjitu || x == Const.Kanri);

            var viewMap = this.ViewMap;

            //管理権限があるかどうか
            if (isKanri == false)
            {
                viewMap = viewMap.Where(x => x.Key == ViewType.ManagementResponsibility).ToDictionary(x => x.Key, x => x.Value);

            }

            //管理所在地
            FormControlUtil.SetComboBoxItem(this.AffiliationComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.Affiliation).Results, false);
            this.AffiliationComboBox.Enabled = isKanri;
            this.AffiliationComboBox.SelectedValue = "";

            //開発符号
            FormControlUtil.SetComboBoxItem(this.GeneralCodeComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.GeneralCodeInfo).Results);

            //表示対象
            FormControlUtil.SetComboBoxItem(this.ViewTargetComboBox, viewMap, false);
            this.ViewTargetComboBox.Enabled = isKanri;
            this.ViewTargetComboBox.SelectedValue = isKanri == true ? ViewType.Management : ViewType.ManagementResponsibility;

            //取得対象
            FormControlUtil.SetComboBoxItem(this.TargetComboBox, this.TargetAllMap, false);
            this.TargetComboBox.SelectedValue = ApplicationApprovalCarTargetType.All;

            //承認状況の設定
            this.SetSyouninJyoukyou();

            //ダウンロードボタン
            this.DownloadButton.Visible = isExport;
        }

        /// <summary>
        /// MultiRowの初期化
        /// </summary>
        private void InitMultiRow()
        {
            var template = new ApplicationApprovalCarTemplate();

            // カスタムテンプレート適用
            this.CustomTemplate.RowCountLabel = this.RowCountLabel;
            this.CustomTemplate.MultiRow = this.TestCarListMultiRow;
            this.CustomTemplate.ConfigDispLayList = base.GetUserDisplayConfiguration(template);
            this.TestCarListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(template);

            // データーソース
            this.TestCarListMultiRow.DataSource = this.DataSource;

            // カラムへのタグ付け
            this.TestCarListMultiRow.Columns["管理票NO"].Tag = "ExcelCellType(Numbers)";
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationApprovalCarForm_Shown(object sender, EventArgs e)
        {
            // 一覧を未選択状態に設定
            this.TestCarListMultiRow.CurrentCell = null;

            this.ActiveControl = this.AffiliationComboBox;
            this.AffiliationComboBox.Focus();
        }
        #endregion

        #region 検索条件ボタンクリック
        /// <summary>
        /// 検索条件ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            // 検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, null, new List<Control> { this.ButtonPanel }, CondHeight);
        }
        #endregion

        #region 管理所在地選択
        /// <summary>
        /// 管理所在地選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffiliationComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //表示対象
            this.ViewTargetPanel.Enabled = (this.AffiliationComboBox.SelectedValue as string) == SessionDto.Affiliation;

            //バインド中なら終了
            if (this.IsBind == true)
            {
                return;
            }

            //画面の表示設定
            this.SetFormVisible();

            //検索条件の設定
            this.SetConditionEnabled();

            //試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
        }

        /// <summary>
        /// 検索条件使用可否の設定
        /// </summary>
        private void SetConditionEnabled()
        {
            //処理中は管理所在地選択のイベントを無効化
            this.IsBind = true;

            var isKanri = SessionDto.ManagementDepartmentType.Any(x => x == Const.Kenjitu || x == Const.Kanri);

            //ユーザーの所属と同じかどうか
            if (this.ViewTargetPanel.Enabled == false)
            {
                var message = isKanri ? Resources.TCM01001 : Resources.TCM01001.Replace("表示対象は「全て」に、", "表示対象の");

                //条件変更可否を問い合わせ
                if (Messenger.ConfirmOkCancel(message, MessageBoxDefaultButton.Button1) != DialogResult.OK)
                {
                    //ユーザーの所属に変更
                    this.AffiliationComboBox.SelectedValue = SessionDto.Affiliation;

                }
                else
                {

                    //表示対象
                    this.ViewTargetComboBox.SelectedValue = isKanri ? ViewType.All : ViewType.ManagementResponsibility;

                    //全て
                    this.AllRadioButton.Checked = true;

                }

            }

            var target = (ApplicationApprovalCarTargetType)this.TargetComboBox.SelectedValue;

            var map = this.TargetAllMap;

            //管理所在地ごとの分岐
            var syozoku = this.AffiliationComboBox.SelectedValue as string;
            switch (syozoku)
            {
                //群馬
                case Gunma:
                    map = this.TargetGunmaMap;
                    break;

                //東京
                case Toukyou:
                    map = this.TargetToukyouMap;
                    break;

            }

            //取得対象
            FormControlUtil.SetComboBoxItem(this.TargetComboBox, map, false);

            //同一項目があれば選択
            var isTarget = map.ContainsKey(target);
            if (isTarget == true)
            {
                this.TargetComboBox.SelectedValue = target;

            }

            //処理後は管理所在地選択のイベントを有効化
            this.IsBind = false;

            //同一項目が無ければ全てを選択
            if (isTarget == false)
            {
                this.TargetComboBox.SelectedValue = ApplicationApprovalCarTargetType.All;

            }

        }
        #endregion

        #region 課マウスクリック
        /// <summary>
        /// 課マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            using (var form = new SectionListForm { DEPARTMENT_ID = SessionDto.DepartmentID })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var value = new ComboBoxDto
                    {
                        CODE = form.SECTION_ID,
                        NAME = form.SECTION_CODE
                    };

                    //担当をセット
                    FormControlUtil.SetComboBoxItem(this.SectionComboBox, new[] { value }, false);
                    this.SectionComboBox.SelectedIndex = 0;

                }

            }

        }
        #endregion

        #region 担当マウスクリック
        /// <summary>
        /// 担当マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            using (var form = new SectionGroupListForm { DEPARTMENT_ID = SessionDto.DepartmentID, SECTION_ID = SessionDto.SectionID })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var value = new ComboBoxDto
                    {
                        CODE = form.SECTION_GROUP_ID,
                        NAME = form.SECTION_GROUP_CODE
                    };

                    //担当をセット
                    FormControlUtil.SetComboBoxItem(this.SectionGroupComboBox, new[] { value }, false);
                    this.SectionGroupComboBox.SelectedIndex = 0;

                }

            }

        }
        #endregion

        #region 表示対象選択
        /// <summary>
        /// 表示対象選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewTargetRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // バインド中は終了
            if (this.IsBind) return;

            //未選択なら終了
            var radio = sender as RadioButton;
            if (radio.Checked == false)
            {
                return;

            }

            //画面の表示設定
            this.SetFormVisible();

            //試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
        }
        #endregion

        #region 検索ボタンクリック
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            //試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
        }
        #endregion

        #region 取得対象選択
        /// <summary>
        /// 取得対象選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //バインド中なら終了
            if (this.IsBind == true)
            {
                return;

            }

            //画面の表示設定
            this.SetFormVisible();

            //承認状況の設定
            this.SetSyouninJyoukyou();

            //試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
        }
        #endregion

        #region クリアボタンクリック
        /// <summary>
        /// クリアボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            var isKanri = SessionDto.ManagementDepartmentType.Any(x => x == Const.Kenjitu || x == Const.Kanri);

            //承認状況
            this.ApprovalComboBox.SelectedIndex = 0;

            //開発符号
            this.GeneralCodeComboBox.SelectedIndex = 0;

            //管理票NO
            this.ManagementNoTextBox.Text = "";

            //課
            FormControlUtil.ClearComboBoxDataSource(this.SectionComboBox);

            //担当
            FormControlUtil.ClearComboBoxDataSource(this.SectionGroupComboBox);

            //試作時期
            this.TrialProductionSeasonTextBox.Text = "";

            //号車
            this.CarTextBox.Text = "";

            //車体番号
            this.CarBodyNoTextBox.Text = "";

            //固定資産NO
            this.FixedAssetTextBox.Text = "";

            //表示対象
            this.ViewTargetComboBox.SelectedValue = isKanri == true ? ViewType.Management : ViewType.ManagementResponsibility;

        }
        #endregion

        #region 2ヶ月以上未入力を表示選択
        /// <summary>
        /// 2ヶ月以上未入力を表示選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
        }
        #endregion

        #region 期限前車両も表示を表示選択
        /// <summary>
        /// 期限前車両も表示を表示選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EarlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
        }
        #endregion

        #region 全選択変更
        /// <summary>
        /// 全選択変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // バインドフラグ
                this.IsBind = true;

                // 全ての行の選択を設定(変更不可は除く)
                foreach (var row in this.TestCarListMultiRow.Rows)
                {
                    row.Cells["SelectedColumn"].Value = this.AllSelectCheckBox.Checked;
                }

                // バインドフラグ
                this.IsBind = false;
            });
        }
        #endregion

        #region 試験車一覧のイベント
        /// <summary>
        /// セルのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarListMultiRow_CellClick(object sender, CellEventArgs e)
        {
            // 全選択チェックボックスクリックの場合
            if (e.CellIndex == 0 && e.RowIndex == -1)
            {
                // 選択チェックボックスの表示を更新する
                this.AllSelectCheckBox.Checked = !this.AllSelectCheckBox.Checked;
            }
        }

        /// <summary>
        /// 試験車一覧セルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarListMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            //無効な列か行の場合は終了
            if (e.CellIndex < 0 || e.RowIndex < 0)
            {
                return;

            }

            var flg = (ApplicationApprovalCarTargetType)this.TargetComboBox.SelectedValue == ApplicationApprovalCarTargetType.MonthlyInspection;
            var testCar = this.GeTestCarByRow(e.RowIndex);

            //試験車使用履歴画面表示
            new FormUtil(new TestCarHistoryForm { TestCar = testCar, UserAuthority = this.UserAuthority, IsMonthlyInspection = flg }).SingleFormShow(this);
        }

        /// <summary>
        /// グリッドの描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarListMultiRow_CellPainting(object sender, CellPaintingEventArgs e)
        {
            if (this.IsBind)
            {
                return;
            }

            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if (e.CellIndex == 0 && e.RowIndex == -1)
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point point = new Point((bmp.Width - AllSelectCheckBox.Width) / 2, (bmp.Height - AllSelectCheckBox.Height) / 2);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    AllSelectCheckBox.DrawToBitmap(bmp, new Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    point = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds);
                    e.Graphics.DrawImage(bmp, point);
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region 申請ボタンクリック
        /// <summary>
        /// 申請ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //処理対象の試験車があるかどうか
                var list = this.GetApplyTestCar();

                if (list != null && list.Any() == true)
                {
                    //試験車使用履歴申請
                    if (this.TestCarUseHistoryApply(list) == true)
                    {
                        //試験車一覧設定
                        this.SetTestCarList(list);
                    }
                }
            });
        }

        /// <summary>
        /// 申請対象の試験車を取得
        /// </summary>
        /// <returns></returns>
        private List<ApplicationApprovalCarModel> GetApplyTestCar()
        {
            var today = DateTime.Today;

            //選択している試験車があるかどうか
            var list = this.GetSelectedTestCar();
            if (list == null || list.Any() == false)
            {
                Messenger.Warn(Resources.KKM00009);
                return null;

            }

            var syozoku = "";
            var syounin = "";

            //取得対象ごとの分岐
            switch ((ApplicationApprovalCarTargetType)this.TargetComboBox.SelectedValue)
            {
                //廃却申請
                case ApplicationApprovalCarTargetType.DispositionApplication:
                    syounin = Haikyaku;
                    break;

                //G技本内移管
                case ApplicationApprovalCarTargetType.GunmaTransfer:
                    syounin = GunmaIkan;
                    syozoku = Gunma;
                    break;

                //T→G移管
                case ApplicationApprovalCarTargetType.TgTransfer:
                    syounin = ToukyouGunmaIkan;
                    syozoku = Gunma;
                    break;

                //T技本内移管
                case ApplicationApprovalCarTargetType.ToukyouTransfer:
                    syounin = ToukyouIkan;
                    syozoku = Toukyou;
                    break;

                //G→T移管
                case ApplicationApprovalCarTargetType.GtTransfer:
                    syounin = GunmaToukyouIkan;
                    syozoku = Toukyou;
                    break;

                default:
                    return null;

            }

            var viewType = (ViewType)this.ViewTargetComboBox.SelectedValue;

            //申請対象のデータのみ設定
            var target = list.Where(x => x.STEPNO < 1 && x.研実管理廃却申請受理日 == null).Select(x =>
            {
                //廃却かどうか
                if (x.承認要件コード != Haikyaku)
                {
                    x.承認要件コード = syounin;

                }

                //表示種別
                x.VIEW_TYPE = viewType;

                //ユーザーID
                x.PERSONEL_ID = SessionDto.UserId;

                //アクセス権限
                x.ACCESS_LEVEL = Tantousya;

                //ユーザーのみ取得
                x.USER_ONLY = this.OnlyRadioButton.Checked;

                //処理日
                x.処理日 = today;

                //編集者
                x.編集者 = SessionDto.UserId;
                
                return x;

            }).ToList();

            //試験車使用履歴権限チェック
            var results = this.IsTestCarUseHistoryAuthority(target);

            //試験車使用履歴入力チェック
            results = this.IsTestCarUseHistoryInput(results);

            //試験車使用履歴変更チェック
            results = this.IsTestCarUseHistoryChange(results);

            using (var haikyakuForm = new DisposalApplicationForm())
            using (var ikanForm = new TransferApplicationForm())
            {
                results.ForEach(x =>
                {
                    //チェック結果がOKかどうか
                    if (x.CHECK_RESULT != CheckResultType.Ok)
                    {
                    }
                    //廃却かどうか
                    else if (x.承認要件コード == Haikyaku)
                    {
                        //試験車
                        haikyakuForm.TestCar = x;

                        //OKかどうか
                        if (haikyakuForm.ShowDialog(this) != DialogResult.OK)
                        {
                            //処理対象外
                            x.CHECK_RESULT = CheckResultType.None;

                        }

                    }
                    else
                    {
                        //試験車
                        ikanForm.TestCar = x;

                        //所属
                        ikanForm.Affiliation = syozoku;

                        //OKかどうか
                        if (ikanForm.ShowDialog(this) != DialogResult.OK)
                        {
                            //処理対象外
                            x.CHECK_RESULT = CheckResultType.None;

                        }

                    }

                });

            }

            return results;

        }
        #endregion

        #region 承認ボタンクリック
        /// <summary>
        /// 承認ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Approval_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                var button = sender as Button;

                var kengen = "";

                //担当者承認
                if (this.PersonApprovalButton.Name == button.Name)
                {
                    kengen = Tantousya;
                }
                //担当承認
                else if (this.ChargeApproval.Name == button.Name)
                {
                    kengen = Tantou;
                }
                //課長承認
                else if (this.ManagerApproval.Name == button.Name)
                {
                    kengen = Katyou;
                }

                //処理対象の試験車があるかどうか
                var list = this.GetApprovalTestCar(kengen);
                if (list != null && list.Any() == true)
                {
                    //試験車使用履歴承認
                    if (this.TestCarUseHistoryApproval(list) == true)
                    {
                        //試験車一覧設定
                        this.SetTestCarList(list);
                    }
                }
            });
        }

        /// <summary>
        /// 承認対象の試験車を取得
        /// </summary>
        /// <param name="kengen">権限</param>
        /// <returns></returns>
        private List<ApplicationApprovalCarModel> GetApprovalTestCar(string kengen)
        {
            var today = DateTime.Today;

            //選択している試験車があるかどうか
            var list = this.GetSelectedTestCar();
            if (list == null || list.Any() == false)
            {
                Messenger.Warn(Resources.KKM00009);
                return null;

            }

            var viewType = (ViewType)this.ViewTargetComboBox.SelectedValue;

            //承認対象のデータのみ設定
            var target = list.Where(x => x.STEPNO > 0).Select(x =>
            {
                //表示種別
                x.VIEW_TYPE = viewType;

                //ユーザーID
                x.PERSONEL_ID = SessionDto.UserId;

                //アクセス権限
                x.ACCESS_LEVEL = kengen;

                //ユーザーのみ取得
                x.USER_ONLY = this.OnlyRadioButton.Checked;

                //処理日
                x.処理日 = today;

                //編集者
                x.編集者 = SessionDto.UserId;

                return x;

            }).ToList();

            //試験車使用履歴権限チェック
            var results = this.IsTestCarUseHistoryAuthority(target);

            //試験車使用履歴変更チェック
            results = this.IsTestCarUseHistoryChange(results);

            using (var form = new ParkingNumberDesignationForm { UserAuthority = this.UserAuthority })
            {
                results.ForEach(x =>
                {
                    //チェック結果がOKで駐車場指定かどうか
                    if (x.CHECK_RESULT == CheckResultType.Ok && x.駐車場指定 == 1)
                    {
                        //試験車
                        form.TestCar = x;

                        //Append Start 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
                        bool parkingSet = false;
                        bool activeCheck = false;
                        if ((x.承認要件コード == "E" && x.STEPNO == 2) || (x.承認要件コード == "H" && x.STEPNO == 7))
                        {
                            parkingSet = true;
                            activeCheck = true;
                        }
                        else if ((x.承認要件コード == "F" && x.STEPNO == 3) || (x.承認要件コード == "G" && x.STEPNO == 10)) parkingSet = true;
                        else if (x.承認要件コード == "B" || x.承認要件コード == "C" || x.承認要件コード == "D") parkingSet = true;
                        //Append End 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて

                        //OKかどうか
                        //Append Start 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
                        if (parkingSet)
                        {
                            form.ActiveCheck = activeCheck;
                            //Append End 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
                            if (form.ShowDialog(this) != DialogResult.OK)
                            {
                                //処理対象外
                                x.CHECK_RESULT = CheckResultType.None;

                            }
                            //Append Start 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
                        }
                        //Append End 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて


                    }

                });

            }

            return results;

        }
        #endregion

        #region 中止ボタンクリック
        /// <summary>
        /// 中止ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //処理対象の試験車があるかどうか
                var list = this.GetStopTestCar();
                if (list != null && list.Any() == true)
                {
                    //試験車使用履歴中止
                    if (this.TestCarUseHistoryStop(list) == true)
                    {
                        //試験車一覧設定
                        this.SetTestCarList(list);
                    }
                }
            });
        }

        /// <summary>
        /// 中止対象の試験車を取得
        /// </summary>
        /// <returns></returns>
        private List<ApplicationApprovalCarModel> GetStopTestCar()
        {
            var today = DateTime.Today;

            //選択している試験車があるかどうか
            var list = this.GetSelectedTestCar();
            if (list == null || list.Any() == false)
            {
                Messenger.Warn(Resources.KKM00009);
                return null;

            }

            //中止可否を判定
            var targetType = (ApplicationApprovalCarTargetType)this.TargetComboBox.SelectedValue;
            list.ForEach(x =>
            {
                var flg = true;

                var syounin = "";

                //取得対象ごとの分岐
                switch (targetType)
                {
                    //廃却申請
                    case ApplicationApprovalCarTargetType.DispositionApplication:
                        flg = x.STEPNO < 0;
                        break;

                    //G技本内移管
                    //T→G移管
                    //T技本内移管
                    //G→T移管
                    case ApplicationApprovalCarTargetType.GunmaTransfer:
                    case ApplicationApprovalCarTargetType.TgTransfer:
                    case ApplicationApprovalCarTargetType.ToukyouTransfer:
                    case ApplicationApprovalCarTargetType.GtTransfer:
                        flg = x.STEPNO <= 0;

                        switch (targetType)
                        {
                            //G技本内移管
                            case ApplicationApprovalCarTargetType.GunmaTransfer:
                                syounin = GunmaIkan;
                                break;

                            //T→G移管
                            case ApplicationApprovalCarTargetType.TgTransfer:
                                syounin = ToukyouGunmaIkan;
                                break;

                            //T技本内移管
                            case ApplicationApprovalCarTargetType.ToukyouTransfer:
                                syounin = ToukyouIkan;
                                break;

                            //G→T移管
                            case ApplicationApprovalCarTargetType.GtTransfer:
                                syounin = GunmaToukyouIkan;
                                break;

                        }
                        break;
                }

                //中止可能かどうか
                x.CHECK_RESULT = flg == true ? CheckResultType.NotStop : CheckResultType.Ok;

                //廃却かどうか
                if (x.承認要件コード != Haikyaku)
                {
                    x.承認要件コード = syounin;

                }

                //編集者
                x.編集者 = SessionDto.UserId;

            });

            //試験車使用履歴変更チェック
            return this.IsTestCarUseHistoryChange(list);

        }
        #endregion

        #region エクセル出力ボタンクリック
        /// <summary>
        /// エクセル出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                new MultiRowUtil(this.TestCarListMultiRow).Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
            });
        }
        #endregion

        #region 画面の表示設定
        /// <summary>
        /// 画面の表示設定
        /// </summary>
        private void SetFormVisible()
        {
            var level = string.IsNullOrWhiteSpace(SessionDto.AccessLevel) == true ? "" : SessionDto.AccessLevel.Substring(0, 1);

            var isSinsei = false;
            var isTantousya = false;
            var isTantou = false;
            var isKatyou = false;
            var isTyuusi = false;

            System.Action syounin = () =>
            {
                //自分で処理する分のみを選択しているかどうか
                if (this.OnlyRadioButton.Checked == true)
                {
                    switch (level)
                    {
                        //課長
                        case Katyou:
                            isKatyou = true;
                            break;

                        //担当
                        case Tantou:
                            isTantou = true;
                            break;

                        //担当者
                        case Tantousya:
                            isTantousya = true;
                            break;

                    }

                }
                else
                {
                    //承認のボタンは全てOK
                    isTantousya = true;
                    isTantou = true;
                    isKatyou = true;

                }

            };

            System.Action tyuusi = () => isTyuusi = SessionDto.ManagementDepartmentType.Contains(Const.Ippan) == false && (level == Katyou || level == Tantou);

            //Append Start 2023/07/19 杉浦 2ヶ月以上未記入のチェックは月例点検時のみ表示
            this.MonthlyCheckBox.Checked = false;
            //Append End 2023/07/19 杉浦 2ヶ月以上未記入のチェックは月例点検時のみ表示

            //2ヶ月以上未入力を表示
            this.MonthlyCheckBox.Visible = false;

            //期限前車両も表示
            this.EarlyCheckBox.Visible = false;

            //取得対象ごとの分岐
            switch ((ApplicationApprovalCarTargetType)this.TargetComboBox.SelectedValue)
            {
                //試験着手日
                case ApplicationApprovalCarTargetType.TestStartDay:
                    //承認ボタン使用可否
                    syounin();
                    break;

                //月例点検
                case ApplicationApprovalCarTargetType.MonthlyInspection:
                    //2ヶ月以上未入力を表示
                    this.MonthlyCheckBox.Visible = true;

                    //承認ボタン使用可否
                    syounin();
                    break;

                //廃却申請
                case ApplicationApprovalCarTargetType.DispositionApplication:
                    //期限前車両も表示
                    this.EarlyCheckBox.Visible = true;

                    //申請ボタン
                    isSinsei = true;

                    //承認ボタン使用可否
                    syounin();

                    //中止ボタン使用可否
                    tyuusi();
                    break;

                //T技本内移管
                //G技本内移管
                //G→T移管
                //T→G移管
                case ApplicationApprovalCarTargetType.ToukyouTransfer:
                case ApplicationApprovalCarTargetType.GunmaTransfer:
                case ApplicationApprovalCarTargetType.GtTransfer:
                case ApplicationApprovalCarTargetType.TgTransfer:
                    //申請ボタン
                    isSinsei = true;

                    //承認ボタン使用可否
                    syounin();

                    //中止ボタン使用可否
                    tyuusi();
                    break;

                //全て
                case ApplicationApprovalCarTargetType.All:
                    //Delete Start 2023/04/12 杉浦 2ヶ月以上未記入のチェックは月例点検時のみ表示
                    ////2ヶ月以上未入力を表示
                    //this.MonthlyCheckBox.Visible = true;
                    //Delete End 2023/04/12 杉浦 2ヶ月以上未記入のチェックは月例点検時のみ表示

                    //期限前車両も表示
                    this.EarlyCheckBox.Visible = true;

                    //承認ボタン使用可否
                    syounin();
                    break;

            }

            //申請
            this.ApplicationButton.Visible = isSinsei;

            //担当者承認
            this.PersonApprovalButton.Visible = isTantousya;

            //担当承認
            this.ChargeApproval.Visible = isTantou;

            //課長承認
            this.ManagerApproval.Visible = isKatyou;

            //中止
            this.StopButton.Visible = isTyuusi;

        }
        #endregion

        #region 試験車一覧設定
        /// <summary>
        /// 試験車一覧設定
        /// </summary>
        private void SetTestCarList(bool isKeepScroll = false)
        {
            // 検索条件チェック
            if (!this.IsSearchConditionCheck()) return;

            // チェックボックスの初期化
            this.AllSelectCheckBox.Checked = false;

            // 検索条件の取得
            this.searchCondition = this.GetSearchConditon();

            // 描画停止
            this.TestCarListMultiRow.SuspendLayout();

            // データの取得
            var list = this.GetTestCarList(this.searchCondition);

            // バインドフラグ
            this.IsBind = true;

            // データバインド
            this.CustomTemplate.SetDataSource(list, this.DataSource);

            // バインドフラグ
            this.IsBind = false;

            // 描画再開
            this.TestCarListMultiRow.ResumeLayout();

            // 検索結果文言
            this.SearchResultLabel.Text = (list == null || list.Any() == false) ? Resources.KKM00005 : "";

            if (isKeepScroll)
            {
                return;
            }

            // スクロールの初期化
            this.TestCarListMultiRow.FirstDisplayedLocation = new Point(0, 0);

            // 一覧を未選択状態に設定
            this.TestCarListMultiRow.CurrentCell = null;
        }

        /// <summary>
        /// 試験車一覧設定
        /// </summary>
        /// <param name="list">チェック結果</param>
        private void SetTestCarList(List<ApplicationApprovalCarModel> list)
        {
            // 試験車一覧設定
            this.SetTestCarList();

            // チェック結果画面表示
            this.ViewReturnError(list);
        }

        /// <summary>
        /// チェック結果の表示
        /// </summary>
        /// <param name="list">チェック結果</param>
        private void ViewReturnError(List<ApplicationApprovalCarModel> list)
        {
            var none = new[] { CheckResultType.None, CheckResultType.Ok };

            var targets = list?.Where(x => none.Contains(x.CHECK_RESULT) == false)?.ToList();

            // リストがなければ終了
            if (targets == null || targets.Count <= 0)
            {
                return;
            }

            foreach (var row in this.TestCarListMultiRow.Rows)
            {
                var testCar = targets.FirstOrDefault(x => x.データID == (int)row.Cells["DataIdColumn"].Value);

                // 対象がない場合
                if (testCar == null)
                {
                    continue;
                }

                // 選択
                row.Cells["SelectedColumn"].Value = true;

                // エラー行の設定
                foreach (var cell in row.Cells)
                {
                    cell.Style.BackColor = Const.ErrorBackColor;
                }

                // 対象行を表示
                this.TestCarListMultiRow.FirstDisplayedCellPosition = new CellPosition(row.Index, "RowHeader");

                // エラー結果ごとの分岐
                switch (testCar.CHECK_RESULT)
                {
                    // 承認不可
                    case CheckResultType.NoApproval:
                        Messenger.Warn(Resources.TCM03010);
                        break;

                    // 権限無し
                    case CheckResultType.NoAuthority:
                        Messenger.Warn(Resources.TCM03009);
                        break;

                    // 月例点検入力
                    case CheckResultType.MonthlyInput:
                        Messenger.Warn(Resources.TCM03011);
                        break;

                    // 更新
                    case CheckResultType.Update:
                        Messenger.Warn(Resources.TCM03012);
                        break;

                    // 中止不可
                    case CheckResultType.NotStop:
                        Messenger.Warn(Resources.TCM03014);
                        break;
                }
            }
        }

        /// <summary>
        /// 検索条件を取得
        /// </summary>
        /// <returns></returns>
        private ApplicationApprovalCarSearchModel GetSearchConditon()
        {
            Func<ComboBox, string> getValue = cmb => cmb.SelectedIndex < 0 || cmb.SelectedValue == null ? null : cmb.SelectedValue.ToString();

            return new ApplicationApprovalCarSearchModel
            {
                //管理所在地
                ESTABLISHMENT = getValue(this.AffiliationComboBox),

                //承認状況
                承認状況 = getValue(this.ApprovalComboBox),

                //開発符号
                開発符号 = this.GeneralCodeComboBox.Text,

                //管理票NO
                管理票NO = this.ManagementNoTextBox.Text,

                //課ID
                SECTION_ID = getValue(this.SectionComboBox),

                //担当ID
                SECTION_GROUP_ID = getValue(this.SectionGroupComboBox),

                //試作時期
                試作時期 = this.TrialProductionSeasonTextBox.Text,

                //号車
                号車 = this.CarTextBox.Text,

                //車体番号
                車体番号 = this.CarBodyNoTextBox.Text,

                //固定資産NO
                固定資産NO = this.FixedAssetTextBox.Text,

                //表示種別
                VIEW_TYPE = (ViewType)this.ViewTargetComboBox.SelectedValue,

                //ユーザーのみ取得
                USER_ONLY = this.OnlyRadioButton.Checked,

                //ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                //月例入力
                MONTHLY_INPUT = this.MonthlyCheckBox.Checked,

                //期限前車両
                EARLY_CAR = this.EarlyCheckBox.Checked,

                //取得種別
                TARGET_TYPE = (ApplicationApprovalCarTargetType)this.TargetComboBox.SelectedValue,
            };
        }

        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchConditionCheck()
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);

                return false;
            }

            return true;
        }
        #endregion

        #region 試験車を画面から取得
        /// <summary>
        /// 選択している試験車を取得
        /// </summary>
        /// <returns></returns>
        private List<ApplicationApprovalCarModel> GetSelectedTestCar()
        {
            var list = new List<ApplicationApprovalCarModel>();

            foreach (var row in this.TestCarListMultiRow.Rows.Where(x => Convert.ToBoolean(x.Cells["SelectedColumn"].Value) == true))
            {
                list.Add(this.GeTestCarByRow(row.Index));
            }

            return list;
        }

        /// <summary>
        /// 試験車を行から取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private ApplicationApprovalCarModel GeTestCarByRow(int index)
        {
            return this.TestCarListMultiRow.Rows[index].DataBoundItem as ApplicationApprovalCarModel;
        }
        #endregion

        #region 承認状況の設定
        /// <summary>
        /// 承認状況の設定
        /// </summary>
        private void SetSyouninJyoukyou()
        {
            var list = this.stepList ?? HttpUtil.GetResponse<ApprovalStepModel>(ControllerType.ApprovalStatus).Results;

            var type = (ApplicationApprovalCarTargetType)this.TargetComboBox.SelectedValue;

            //全てを選択しているかどうか
            if (type != ApplicationApprovalCarTargetType.All)
            {
                list = list.Where(x => x.承認要件コード == SyouninMap[type] || string.IsNullOrWhiteSpace(x.承認要件コード) == true);

            }
            else
            {
                list = list.Select(x => x.STEP名).Distinct().Select(x => new ApprovalStepModel { STEP名 = x });

            }

            var text = this.ApprovalComboBox.Text;

            //承認状況
            var target = list.ToArray();
            FormControlUtil.SetComboBoxItem(this.ApprovalComboBox, target);

            //選択していた項目があるかどうか
            if (target.Any(x => x.STEP名 == text) == false)
            {
                this.ApprovalComboBox.SelectedIndex = 0;

            }
            else
            {
                this.ApprovalComboBox.Text = text;

            }

        }
        #endregion

        #region API
        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<ApplicationApprovalCarModel> GetTestCarList(ApplicationApprovalCarSearchModel cond)
        {
            //APIで取得
            var res = HttpUtil.PostResponse<ApplicationApprovalCarModel>(ControllerType.ApplicationApprovalCar, cond);

            //レスポンスが取得できたかどうか
            var list = new List<ApplicationApprovalCarModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// 試験車使用履歴権限チェック
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        private List<ApplicationApprovalCarModel> IsTestCarUseHistoryAuthority(List<ApplicationApprovalCarModel> list)
        {
            //APIで取得
            var res = HttpUtil.PostResponse(ControllerType.TestCarUseHistoryAuthorityCheck, list);

            //レスポンスが取得できたかどうか
            var results = new List<ApplicationApprovalCarModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                results.AddRange(res.Results);

            }

            return results;

        }

        /// <summary>
        /// 試験車使用履歴入力チェック
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        private List<ApplicationApprovalCarModel> IsTestCarUseHistoryInput(List<ApplicationApprovalCarModel> list)
        {
            //対象が無ければ終了
            if (list.Any(x => x.CHECK_RESULT == CheckResultType.Ok) == false)
            {
                return list;

            }

            //APIで取得
            var res = HttpUtil.PostResponse(ControllerType.TestCarUseHistoryInputCheck, list);

            //レスポンスが取得できたかどうか
            var results = new List<ApplicationApprovalCarModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                results.AddRange(res.Results);

            }

            return results;

        }

        /// <summary>
        /// 試験車使用履歴変更チェック
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        private List<ApplicationApprovalCarModel> IsTestCarUseHistoryChange(List<ApplicationApprovalCarModel> list)
        {
            //対象が無ければ終了
            if (list.Any(x => x.CHECK_RESULT == CheckResultType.Ok) == false)
            {
                return list;

            }

            //APIで取得
            var res = HttpUtil.PostResponse(ControllerType.TestCarUseHistoryChangeCheck, list);

            //レスポンスが取得できたかどうか
            var results = new List<ApplicationApprovalCarModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                results.AddRange(res.Results);

            }

            return results;

        }

        /// <summary>
        /// 試験車使用履歴申請
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        private bool TestCarUseHistoryApply(List<ApplicationApprovalCarModel> list)
        {
            //対象が無ければ終了
            var target = list.Where(x => x.CHECK_RESULT == CheckResultType.Ok).ToArray();
            if (target.Any() == false)
            {
                return true;

            }

            //登録
            var res = HttpUtil.PostResponse(ControllerType.TestCarUseHistory, target);

            //レスポンスが取得できたかどうか
            return (res != null && res.Status == Const.StatusSuccess);

        }

        /// <summary>
        /// 試験車使用履歴承認
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        private bool TestCarUseHistoryApproval(List<ApplicationApprovalCarModel> list)
        {
            //対象が無ければ終了
            var target = list.Where(x => x.CHECK_RESULT == CheckResultType.Ok).ToArray();
            if (target.Any() == false)
            {
                return true;

            }

            //登録
            var res = HttpUtil.PutResponse(ControllerType.TestCarUseHistory, target);

            //レスポンスが取得できたかどうか
            return (res != null && res.Status == Const.StatusSuccess);

        }

        /// <summary>
        /// 試験車使用履歴中止
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        private bool TestCarUseHistoryStop(List<ApplicationApprovalCarModel> list)
        {
            //対象が無ければ終了
            var target = list.Where(x => x.CHECK_RESULT == CheckResultType.Ok).ToArray();
            if (target.Any() == false)
            {
                return true;

            }

            //登録
            var res = HttpUtil.DeleteResponse(ControllerType.TestCarUseHistory, target);

            //レスポンスが取得できたかどうか
            return (res != null && res.Status == Const.StatusSuccess);

        }
        #endregion

        #region MultiRow 共通操作

        /// <summary>
        /// MulutiRow設定アイコンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListConfigPictureBox_Click(object sender, EventArgs e)
        {
            // 表示設定画面表示
            base.ShowDisplayForm(this.CustomTemplate);
        }

        /// <summary>
        /// MulutiRowソートアイコンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListSortPictureBox_Click(object sender, EventArgs e)
        {
            // ソート指定画面表示
            base.ShowSortForm(this.CustomTemplate);
        }

        #endregion
    }
}
