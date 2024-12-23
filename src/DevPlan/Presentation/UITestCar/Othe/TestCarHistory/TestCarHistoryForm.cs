using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.Presentation.UITestCar.ControlSheet;
using DevPlan.Presentation.UITestCar.Othe.ApplicationApprovalCar;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using System.Text.RegularExpressions;
using DevPlan.UICommon.Util;

namespace DevPlan.Presentation.UITestCar.Othe.TestCarHistory
{
    /// <summary>
    /// 試験車使用履歴
    /// </summary>
    public partial class TestCarHistoryForm : BaseSubForm
    {
        #region メンバ変数
        private DataGridViewUtil<TestCarUseHistoryModel> gridUtil = null;

        private bool isBind = false;

        private List<TestCarCommonModel> testCarList = null;

        private const string GetureiTenken = "C";

        private const string Nyuuryokumati = "入力待ち";

        private const string Zumi = "済";

        private const string Tantousya = "2";

        private const string None = "0";


        private const string Filter = "Excel ブック (*.xlsx)|*.xlsx;";

        private const string ControlSheetFileName = "試験車管理票_{0}_{1:yyyyMMdd}";

        private const string SheetName = "試験車管理票";

        private const string Gunma = "g";
        private const string Toukyou = "t";

        private const int MergeStart = 21;
        private const int RowStart = 23;

        private const int InitPageCount = 12;
        private const int NextPageCount = 19;

        private const int RowCount = 2;

        private const string Touroku = "登録";

        private readonly Dictionary<string, string> NameMap = new Dictionary<string, string>
        {
            { "0", "課長承認" },
            { "1", "担当承認" },
            { "2", "担当者承認" }

        };

        private readonly Color HeaderBackColor = Color.LightBlue;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "試験車使用履歴"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>試験車情報</summary>
        public TestCarCommonModel TestCar { get; set; } = new TestCarCommonModel();

        /// <summary>試験車作業履歴</summary>
        public WorkHistoryModel WorkHistory { get; set; } = null;

        /// <summary>月例点検可否</summary>
        public bool IsMonthlyInspection { get; set; } = true;

        /// <summary>親画面リロードデリゲート</summary>
        public Action<bool> Reload { get; set; } = (bool isPast) => { };

        /// <summary>子画面クローズデリゲート</summary>
        public Action Shut { get; set; } = () => { };

        //Add START 2020/12/07 No.63844 杉浦
        /// <summary>親画面アクティベートデリゲート</summary>
        public Action Activate { get; set; } = () => { };
        //Add END 2020/12/07 No.63844 杉浦


        //Append Start 2021/07/26 矢作
        /// <summary>ページ数</summary>
        public int? CurrentPage { get; set; }
        //Append End 2021/07/26 矢作

        public List<DateTime?> 処理日リスト { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCarHistoryForm()
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
        private void TestCarHistoryForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //バインド可否
                this.isBind = true;

                //画面初期化
                this.InitForm();

                //グリッドビュー初期化
                this.InitGridView();

                //試験車設定
                this.SetTestCar();

                //試験車使用履歴設定
                this.SetTestCarUseHistory();

                //バインド可否
                this.isBind = false;

            });

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //試験車取得
            this.testCarList = this.GetTestCarList();

            //履歴
            FormControlUtil.SetComboBoxItem(this.HistoryNoComboBox, this.testCarList, false);
            this.HistoryNoComboBox.SelectedValue = this.TestCar.履歴NO;

            //Append Start 2021/07/26 矢作
            if (!this.CurrentPage.Equals(null))
            {
                this.HistoryNoComboBox.SelectedValue = this.CurrentPage;
            }
            //Append End 2021/07/26 矢作

            this.ActiveControl = this.HistoryNoComboBox;

            //試験車情報表示
            this.TestCarShowButton.Visible = this.Owner.GetType() != typeof(ControlSheetIssueForm);

            // 使用履歴簡易入力からの遷移の場合
            if (this.WorkHistory != null)
            {
                //試験車情報表示
                this.TestCarShowButton.Visible = false;
            }

            //承認済みデータを修正
            this.EditCheckBox.Visible = SessionDto.ManagementDepartmentType.Any(x => x == Const.Kenjitu || x == Const.Kanri);

        }

        /// <summary>
        /// グリッドビュー初期化
        /// </summary>
        private void InitGridView()
        {
            //データグリッドビュー初期化
            this.gridUtil = new DataGridViewUtil<TestCarUseHistoryModel>(this.ListDataGridView, false);
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarHistoryForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.ListDataGridView.CurrentCell = null;

        }
        #endregion

        #region フォームクローズ前
        /// <summary>
        /// フォームクローズ前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarHistoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //更新権限ありで画面を変更していて登録するかどうか
                //Update START 2021/04/20 杉浦 子画面を閉じるときに登録をしないならリロードをしない
                ////Update START 2020/12/07 No.63844 杉浦
                ////if (this.UserAuthority.UPDATE_FLG == '1' && this.IsEditEntry() == false)
                //if (this.UserAuthority.UPDATE_FLG == '1' && this.IsEditEntryForClose() == false)
                ////Update END 2020/12/07 No.63844 杉浦
                //{
                //    e.Cancel = true;

                //}
                if (this.UserAuthority.UPDATE_FLG == '1')
                {
                    var check = this.IsEditEntryForClose();
                    if (check == "0")
                    {
                        e.Cancel = true;
                    }
                    else if (check == "1")
                    {
                        //試験車設定
                        this.SetTestCar();

                        //試験車使用履歴設定
                        this.SetTestCarUseHistory();

                        //親画面アクティベート
                        this.Activate();
                    }
                    else
                    {
                        //試験車設定
                        this.SetTestCar();

                        //試験車使用履歴設定
                        this.SetTestCarUseHistory();

                        //親画面リロード
                        bool isPast = false;
                        if (this.処理日リスト.Any(x => x.Value < DateTime.Today)) isPast = true;
                        this.Reload(isPast);

                        //親画面アクティベート
                        this.Activate();
                    }
                }
                //Update End 2021/04/20 杉浦 子画面を閉じるときに登録をしないならリロードをしない
                //Append START 2020/12/07 No.63843 杉浦
                else
                {
                    //試験車設定
                    this.SetTestCar();

                    //試験車使用履歴設定
                    this.SetTestCarUseHistory();

                    //親画面リロード
                    bool isPast = false;
                    if (this.処理日リスト.Any(x => x.Value < DateTime.Today)) isPast = true;
                    this.Reload(isPast);

                    //親画面アクティベート
                    this.Activate();
                }
                //Append END 2020/12/07 No.63843 杉浦

                this.Shut();
            });

        }
        #endregion

        #region 履歴選択
        /// <summary>
        /// 履歴選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryNoComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //バインド中か初期状態なら終了
            if (this.isBind == true || this.HistoryNoComboBox.SelectedIndex == -1)
            {
                return;

            }

            FormControlUtil.FormWait(this, () =>
            {
                //更新権限ありで画面を変更していて登録するかどうか
                if (this.UserAuthority.UPDATE_FLG == '1' && this.IsEditEntry() == false)
                {
                    //バインド可否
                    this.isBind = true;

                    //元の履歴を選択
                    this.HistoryNoComboBox.SelectedValue = this.TestCar.履歴NO;

                    //バインド可否
                    this.isBind = false;

                    return;

                }

                //試験車設定
                this.SetTestCar();

                //試験車使用履歴設定
                this.SetTestCarUseHistory();

            });

        }
        #endregion

        #region 試験車情報表示クリック
        /// <summary>
        /// 試験車情報表示クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarShowButton_Click(object sender, EventArgs e)
        {
            new FormUtil(new ControlSheetIssueForm { TestCar = this.TestCar, UserAuthority = this.UserAuthority }).SingleFormShow(this);
        }
        #endregion

        #region 承認済みデータを修正選択
        /// <summary>
        /// 承認済みデータを修正選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //試験車使用履歴編集可否設定
            this.SetTestCarUseHistoryEdit();

        }
        #endregion

        #region 試験車使用履歴
        /// <summary>
        /// 試験車使用履歴セルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            var col = this.ListDataGridView.Columns[e.ColumnIndex];
            var row = this.ListDataGridView.Rows[e.RowIndex];

            //読取専用か読取専用の行か使用部署以外の列なら終了
            if (this.ListDataGridView.ReadOnly == true || row.ReadOnly == true || col.Name != this.UseDepartmentColumn.Name)
            {
                return;

            }

            using (var form = new SectionGroupListForm())
            {
                // カーシェア事務所でない場合は初期値セット
                if (this.UserAuthority.CARSHARE_OFFICE_FLG != '1')
                {
                    form.DEPARTMENT_ID = SessionDto.DepartmentID;
                    form.SECTION_ID = SessionDto.SectionID;
                }

                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var history = this.GetTestCarUseHistory(row);

                    //使用課名
                    history.使用課名 = form.SECTION_CODE;

                    //使用部署名
                    history.使用部署名 = form.SECTION_GROUP_CODE;

                    //使用部署
                    history.使用部署 = string.Format("{0} {1}", form.SECTION_CODE, form.SECTION_GROUP_CODE);

                    //変更を通知
                    this.gridUtil.DataSourceResetItem(e.RowIndex);

                }

            }

        }

        /// <summary>
        /// 試験車使用履歴セルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            var col = this.ListDataGridView.Columns[e.ColumnIndex];
            var row = this.ListDataGridView.Rows[e.RowIndex];

            //編集可能な行の列の場合は終了
            if (row.ReadOnly == false && col.ReadOnly == false)
            {
                return;

            }

            //使用履歴承認状況表示
            using (var form = new TestCarUseHistoryApprovalForm { TestCarUseHistory = this.GetTestCarUseHistory(row) })
            {
                form.ShowDialog(this);

            }

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
            FormControlUtil.FormWait(this, () =>
            {
                //試験車使用履歴登録
                if (this.EntryTestCarHistory() == true)
                {
                    //試験車設定
                    this.SetTestCar();

                    //試験車使用履歴設定
                    this.SetTestCarUseHistory();

                    // 親画面リロード
                    bool isPast = false;
                    if (this.処理日リスト.Any(x => x.Value < DateTime.Today)) isPast = true;
                    this.Reload(isPast);
                }

            });

        }
        #endregion

        #region 印刷ボタンクリック
        /// <summary>
        /// 印刷ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //管理票XLSの印刷
                this.SaveXls();

            });

        }

        /// <summary>
        /// 管理票XLSの印刷
        /// </summary>
        private void SaveXls()
        {
            var path = "";

            using (var sfd = new SaveFileDialog { Filter = Filter, FileName = string.Format(ControlSheetFileName, this.TestCar.管理票NO, DateTime.Now) })
            {
                //OKかどうか
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;

                }

                path = sfd.FileName;

            }

            Func<DateTime?, string> getDate = d => d == null ? "" : d.Value.ToString("yyyy年MM月dd日");
            Func<DateTime?, string> getMonth = d => d == null ? "" : d.Value.ToString("yyyy年MM月");
            Func<DateTime?, string> getKenmeiKikan = d => d == null ? "" : d.Value.ToString("yyyy年MM月末日");

            Func<TestCarCommonModel, string> getKenmei = testcar =>
            {
                var kenmei = testcar.研命ナンバー;

                var sb = new StringBuilder();

                //研命ナンバーがあるかどうか
                if (string.IsNullOrWhiteSpace(kenmei) == false)
                {
                    switch (kenmei.Substring(0, 1))
                    {
                        case "G":
                            sb.Append("技許可");
                            break;

                        case "A":
                            sb.Append("技試命");
                            break;

                        case "B":
                            sb.Append("技調命");
                            break;

                        case "T":
                            sb.Append("技命");
                            break;

                        default:
                            sb.Append("技改命");
                            break;

                    }

                    //5～6文字かどうか
                    var lenth = kenmei.Length;
                    if (lenth == 5 || lenth == 6)
                    {
                        sb.Append(kenmei.Substring(1, 2));
                        sb.AppendFormat("No.{0}", kenmei.Substring(3, 2));

                    }
                    else if (lenth >= 7)
                    {
                        sb.Append(kenmei.Substring(1, 3));
                        sb.AppendFormat("No.{0}", kenmei.Substring(4, 3));

                    }

                }

                return sb.ToString();

            };

            Func<TestCarCommonModel, string> getCarName = testcar =>
            {
                var carName = "";
                if (string.IsNullOrWhiteSpace(testcar.開発符号) == true)
                {
                    carName = string.Format("{0}-{1}", testcar.メーカー名, testcar.外製車名);

                }
                else
                {
                    carName = string.Format("{0}-{1}-{2}号車", testcar.開発符号, testcar.試作時期, testcar.号車);

                }

                return carName;

            };

            using (var xls = new XlsUtil(Resources.TestCarSheet))
            {
                //確認部署
                var kakuninBusyo = "";
                switch (this.TestCar.ESTABLISHMENT)
                {
                    case Gunma:
                        kakuninBusyo = "第一管理課";
                        break;

                    case Toukyou:
                        kakuninBusyo = "第二管理課";
                        break;

                }

                //試験車情報の書込
                xls.WriteSheet(SheetName, new Dictionary<string, string>
                {
                    { "C2", string.Format("駐車場番号:{0}", this.TestCar.駐車場番号) },
                    { "P2", this.TestCar.種別 },

                    { "C4", getKenmei(this.TestCar) },
                    { "M4", this.TestCar.管理票NO },

                    { "G5", getCarName(this.TestCar) },
                    
                    { "M6", getDate(this.TestCar.発行年月日) },

                    { "D8", this.TestCar.車型 },
                    { "E8", string.Format("{0}cc", this.TestCar.排気量) },
                    { "H8", this.TestCar.型式符号 },
                    { "K8", getDate(this.TestCar.正式取得日 ?? this.TestCar.受領日) },
                    { "M8", string.Format("{0} {1}", this.TestCar.SECTION_CODE, this.TestCar.SECTION_GROUP_CODE) },
                    { "P8", kakuninBusyo },

                    { "D9", this.TestCar.トランスミッション },
                    { "E9", this.TestCar.駆動方式 },

                    { "D10", this.TestCar.車体番号 },
                    { "K10", getKenmeiKikan(this.TestCar.研命期間) },

                    { "D13", this.TestCar.E_G番号 },
                    { "K13", this.TestCar.試験目的 },
                    { "Q13", this.TestCar.固定資産NO },

                    { "O15", this.TestCar.リースNO },

                    { "D16", this.TestCar.登録ナンバー },
                    { "H16", this.TestCar.車体色 },
                    { "K16", getMonth(this.TestCar.使用期限) },

                    { "L17", this.TestCar.名称備考 },

                    { "D18", getDate(this.TestCar.車検登録日) },
                    { "I18", getDate(this.TestCar.試験着手日) },
                    { "L18", this.TestCar.試験着手証明文書 },
                    { "D19", getDate(this.TestCar.廃艦日) },
                    { "I19", this.TestCar.メモ },

                    { "D20", this.TestCar.保険NO }
                    

                });

                //1つ前の試験車の履歴があるかどうか
                var prev = this.testCarList.Where(x => x.履歴NO < this.TestCar.履歴NO).OrderBy(x => x.履歴NO).LastOrDefault();
                if (prev != null)
                {
                    //試験車情報の書込
                    xls.WriteSheet(SheetName, new Dictionary<string, string>
                    {
                        { "C7", getKenmei(prev) },
                        { "G7", getCarName(prev) },

                        { "D12", prev.車体番号 },
                        { "K12", getKenmeiKikan(prev.研命期間) },

                        { "D15", prev.E_G番号 },
                        { "K15", prev.試験目的 },
                        { "Q15", prev.固定資産NO }

                    });

                }

                var list = this.GetTestCarUseHistoryList().Where(x => x.STEPNO <= 0).Select((x, i) => new { x, i }).OrderByDescending(x => x.i).Select(x => x.x).ToArray();
                var count = list.Count();

                var row = 1;

                var start = MergeStart;
                var end = MergeStart + InitPageCount * RowCount + 1;

                //行コピー
                var copyCount = (count <= InitPageCount ? InitPageCount : InitPageCount + ((count - InitPageCount) / NextPageCount + ((count - InitPageCount) % NextPageCount == 0 ? 0 : 1)) * NextPageCount) - 1;
                for (var i = 0; i <= copyCount; i++)
                {
                    var row1 = RowStart + (RowCount * (i + 1));
                    var row2 = row1 + 1;

                    //最後の行かどうか
                    if (i != copyCount)
                    {
                        xls.CopyRow(SheetName, "23:24", string.Format("{0}:{1}", row1, row2));

                    }

                    //1ページの行数かどうか
                    if (row == InitPageCount)
                    {
                        //セルの結合
                        xls.MergeCell(SheetName, string.Format("B{0}:B{1}", start, end));

                        //改ページを設定
                        xls.SetRowBreak(SheetName, "A" + (row1 - 1));

                        start = row1;

                    }
                    //2ページ以降の行数かどうか
                    else if ((row - InitPageCount) % NextPageCount == 0)
                    {
                        //セルの結合
                        xls.MergeCell(SheetName, string.Format("B{0}:B{1}", start, row1 - 1));

                        //改ページを設定
                        xls.SetRowBreak(SheetName, "A" + (row1 - 1));

                        start = row1;

                    }

                    row++;

                }

                row = 0;
                foreach (var history in list)
                {
                    var row1 = RowStart + (row++ * RowCount);
                    var row2 = row1 + 1;

                    //明細の書き込み
                    xls.WriteSheet(SheetName, new Dictionary<string, string>
                    {
                        { "C" + row1, DateTimeUtil.ConvertDateString(history.処理日) },
                        { "E" + row1, history.管理責任課名 },
                        { "E" + row2, history.管理責任部署名 },
                        { "G" + row1, history.使用課名 },
                        { "G" + row2, history.使用部署名 },
                        { "I" + row1, history.試験内容 },
                        { "O" + row1, history.工事区分NO },
                        { "Q" + row1, history.実走行距離 }

                    });

                }

                //保存
                xls.Save(path);

            }

        }
        #endregion

        #region 試験車設定
        /// <summary>
        /// 試験車設定
        /// </summary>
        private void SetTestCar()
        {
            //試験車
            this.TestCar = this.testCarList.First(x => x.履歴NO == (int)this.HistoryNoComboBox.SelectedValue);

            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            //最新の履歴以外は編集不可
            var isMax = this.TestCar.履歴NO == this.testCarList.Max(x => x.履歴NO);
            if (isMax == false)
            {
                isUpdate = false;

            }

            //試験着手日
            this.TestCommencementDayDateTimePicker.Value = this.TestCar.試験着手日;

            //試験着手証明文書
            this.TestCommencementDocumentTextBox.Text = this.TestCar.試験着手証明文書;

            //開発符号
            this.GeneralCodeLabel.Text = this.TestCar.開発符号;

            //管理票NO
            this.ManagementNoLabel.Text = this.TestCar.管理票NO;

            //名称
            this.NameLabel.Text = this.TestCar.名称;

            //車体NO
            this.CarBodyLabel.Text = this.TestCar.車体番号;

            //登録NO
            this.EntryNoLabel.Text = this.TestCar.登録ナンバー;

            //駐車場NO
            this.ParkingNumberLabel.Text = this.TestCar.駐車場番号;

            //種別
            this.TypeLabel.Text = this.TestCar.種別;

            //取得日
            this.GetDateLabel.Text = DateTimeUtil.ConvertDateString(this.TestCar.正式取得日);

            //使用期限
            this.UsePeriodLabel.Text = DateTimeUtil.ConvertDateString(this.TestCar.使用期限);

            //試験着手日
            this.TestCommencementDayDateTimePicker.Enabled = isUpdate == false ? false : this.TestCar.研実管理廃却申請受理日 == null;

            //試験着手証明文書
            this.TestCommencementDocumentTextBox.Enabled = isUpdate;

            //試験車使用履歴
            this.ListDataGridView.ReadOnly = !isMax;

            //登録ボタン
            this.EntryButton.Visible = isMax;

            //廃却申請勧告
            this.CautionTableLayoutPanel.Visible = this.CautionLabel.Visible = 
                this.TestCar.研実管理廃却申請受理日 == null && 
                this.TestCar.車両搬出日 == null &&
                this.TestCar.処分コード == null &&
                this.TestCar.使用期限 != null && 
                this.TestCar.種別 != "リース" && 
                this.TestCar.使用期限.Value.AddMonths(-2).AddDays(-this.TestCar.使用期限.Value.Day + 20) <= DateTime.Now;

            //月例点検ラベル
            this.MonthlyInspectionLabel.Visible = this.TestCar.月例点検省略有無 == 1;

        }
        #endregion

        #region 試験車使用履歴設定
        /// <summary>
        /// 試験車使用履歴設定
        /// </summary>
        private void SetTestCarUseHistory()
        {
            // 作業履歴簡易入力遷移フラグ
            var isWorkHistory = false;

            //試験車使用履歴取得
            var list = this.GetTestCarUseHistoryList();

            //月例点検入力で使用履歴が入力可能で最新の履歴NOかどうか
            if (this.IsMonthlyInspection == true && this.ListDataGridView.ReadOnly == false && this.TestCar.履歴NO == this.testCarList.Max(x => x.履歴NO))
            {
                var last = list.FirstOrDefault(x => x.SEQNO == list.Max(y => y.SEQNO));

                //承認中の使用履歴がなしで管理票発行済かどうか
                if ((last == null || last.STEPNO <= 0) && this.TestCar.管理票発行有無 == Zumi)
                {
                    var kanriKa = this.TestCar.SECTION_CODE;
                    var kanriTantou = this.TestCar.SECTION_GROUP_CODE;

                    var siyouKa = kanriKa;
                    var siyouTantou = kanriTantou;

                    //最新の使用履歴があるかどうか
                    if (last != null)
                    {
                        siyouKa = last.使用課名;
                        siyouTantou = last.使用部署名;

                    }

                    //先頭に追加
                    list.Insert(0, new TestCarUseHistoryModel
                    {
                        //データID
                        データID = this.TestCar.データID,

                        //履歴NO
                        履歴NO = this.TestCar.履歴NO,

                        //SEQNO
                        SEQNO = 0,

                        //承認要件コード
                        承認要件コード = GetureiTenken,

                        //STEPNO
                        STEPNO = -1,

                        //承認状況
                        承認状況 = Nyuuryokumati,

                        //承認者レベル
                        承認者レベル = Tantousya,

                        //管理部署承認
                        管理部署承認 = None,

                        //処理日
                        処理日 = DateTime.Today,

                        //管理責任課名
                        管理責任課名 = kanriKa,

                        //管理責任部署名
                        管理責任部署名 = kanriTantou,

                        //管理責任部署
                        管理責任部署 = string.Format("{0} {1}", kanriKa, kanriTantou),

                        //使用課名
                        使用課名 = siyouKa,

                        //使用部署名
                        使用部署名 = siyouTantou,

                        //管理所在地
                        ESTABLISHMENT = this.TestCar.ESTABLISHMENT,

                        //使用部署
                        使用部署 = string.Format("{0} {1}", siyouKa, siyouTantou),

                        //工事区分NO
                        工事区分NO = this.TestCar.工事区分NO,

                        //編集者
                        編集者 = SessionDto.UserId,

                        //ユーザーID
                        PERSONEL_ID = SessionDto.UserId,

                        //試験内容
                        試験内容 = this.WorkHistory?.試験内容,

                        //実走行距離
                        実走行距離 = this.WorkHistory?.実走行距離,

                    });

                    if (this.WorkHistory != null)
                    {
                        isWorkHistory = true;
                    }
                }

            }

            //試験車使用履歴セット
            this.gridUtil.DataSource = list;

            //使用履歴簡易入力から遷移した場合
            if (isWorkHistory)
            {
                // 変更を通知
                this.gridUtil.DataSourceResetItem(0);

                // 試験車作業履歴情報の初期化
                this.WorkHistory = null;
            }

            //試験車使用履歴編集可否設定
            this.SetTestCarUseHistoryEdit();

            //登録
            this.EntryButton.Text = Touroku;

            //承認対象の試験車使用履歴があるかどうか
            var syounin = list.LastOrDefault(x => x.STEPNO > 0);
            if (syounin != null && this.NameMap.ContainsKey(syounin.承認者レベル) == true)
            {
                //登録
                this.EntryButton.Text = this.NameMap[syounin.承認者レベル];

            }

        }

        /// <summary>
        /// 試験車使用履歴編集可否設定
        /// </summary>
        private void SetTestCarUseHistoryEdit()
        {
            var list = new List<bool>();

            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            var isUser = !SessionDto.ManagementDepartmentType.Any(x => x == Const.Kenjitu || x == Const.Kanri);

            foreach (DataGridViewRow row in this.ListDataGridView.Rows)
            {
                var history = this.GetTestCarUseHistory(row);

                //SEQ
                row.Cells[this.SeqColumn.Name].Value = history.SEQNO <= 0 ? "" : history.SEQNO.ToString();

                //読取専用なら次へ
                if (this.ListDataGridView.ReadOnly == true)
                {
                    continue;

                }

                //ユーザーか承認済みデータを修正しない場合は承認完了したデータは編集不可
                row.ReadOnly = (isUser == true || this.EditCheckBox.Checked == false) && history.STEPNO == 0;

                //読み取り専用の行で編集するかどうか
                if (row.ReadOnly == false)
                {
                    //試験内容は承認完了で月例点検以外は編集不可
                    row.Cells[this.TestContentColumn.Name].ReadOnly = history.STEPNO == 0 && history.承認要件コード != GetureiTenken;

                }

                list.Add(!row.ReadOnly);

            }

            //更新権限なしで編集ボタンが表示されているかどうか
            if (isUpdate == false && this.EntryButton.Visible == true)
            {
                this.EntryButton.Visible = list.Contains(true);

            }

        }
        #endregion

        #region 試験車使用履歴の取得
        /// <summary>
        /// 試験車使用履歴の取得
        /// </summary>
        /// <param name="row">行</param>
        /// <returns></returns>
        private TestCarUseHistoryModel GetTestCarUseHistory(DataGridViewRow row)
        {
            var dataId = Convert.ToInt32(row.Cells[this.DataIdColumn.Name].Value);
            var historyNo = Convert.ToInt32(row.Cells[this.HistoryNoColumn.Name].Value);
            var seqNo = Convert.ToInt32(row.Cells[this.SeqNoColumn.Name].Value);

            return this.gridUtil.DataSource.First(x => x.データID == dataId && x.履歴NO == historyNo && x.SEQNO == seqNo);

        }
        #endregion

        #region 変更登録可否
        /// <summary>
        /// 変更登録可否
        /// </summary>
        /// <returns></returns>
        private bool IsEditEntry()
        {
            //登録ボタンが表示されているかどうか
            if (this.EntryButton.Visible == false)
            {
                return true;

            }

            //使用履歴の編集は終了
            this.ListDataGridView.EndEdit();

            //使用履歴を編集しているかどうか
            var isEdit = this.gridUtil.IsEdit;
            if (isEdit == false)
            {
                var siken = this.TestCar.試験着手日;
                siken = siken == null ? null : (DateTime?)siken.Value.Date;

                //試験着手日か試験着手証明文書を変更しているかどうか
                isEdit = this.TestCommencementDayDateTimePicker.SelectedDate != siken || this.TestCommencementDocumentTextBox.Text != (this.TestCar.試験着手証明文書 ?? "");

            }

            //画面を変更していないか登録するかどうか
            if (isEdit == false || (isEdit == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                return true;

            }

            //試験車使用履歴登録
            return this.EntryTestCarHistory(false);

        }

        //Append START 2020/12/07 No.63843 杉浦
        /// <summary>
        /// 変更登録可否(フォームクローズ用)
        /// </summary>
        /// <returns></returns>
        //Update Start 2021/04/20 杉浦 子画面を閉じるときに登録をしないならリロードをしない
        //private bool IsEditEntryForClose()
        private string IsEditEntryForClose()
        //Update End 2021/04/20 杉浦 子画面を閉じるときに登録をしないならリロードをしない
        {
            //登録ボタンが表示されているかどうか
            if (this.EntryButton.Visible == false)
            {
                //Update Start 2021/04/20 杉浦 子画面を閉じるときに登録をしないならリロードをしない
                //return true;
                return "1";
                //Update End 2021/04/20 杉浦 子画面を閉じるときに登録をしないならリロードをしない

            }

            //使用履歴の編集は終了
            this.ListDataGridView.EndEdit();

            //使用履歴を編集しているかどうか
            var isEdit = this.gridUtil.IsEdit;
            if (isEdit == false)
            {
                var siken = this.TestCar.試験着手日;
                siken = siken == null ? null : (DateTime?)siken.Value.Date;

                //試験着手日か試験着手証明文書を変更しているかどうか
                isEdit = this.TestCommencementDayDateTimePicker.SelectedDate != siken || this.TestCommencementDocumentTextBox.Text != (this.TestCar.試験着手証明文書 ?? "");

            }

            //画面を変更していないか登録するかどうか
            if (isEdit == false || (isEdit == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                //Update Start 2021/04/20 杉浦 子画面を閉じるときに登録をしないならリロードをしない
                //return true;
                return "1";
                //Update End 2021/04/20 杉浦 子画面を閉じるときに登録をしないならリロードをしない
            }

            //試験車使用履歴登録
            //Update Start 2021/04/20 杉浦 子画面を閉じるときに登録をしないならリロードをしない
            //return this.EntryTestCarHistory();
            if (this.EntryTestCarHistory())
            {
                return "2";
            }
            else
            {
                return "0";
            }
            //Update End 2021/04/20 杉浦 子画面を閉じるときに登録をしないならリロードをしない
            
        }
        //Append END   2020/12/07 No.63843 杉浦
        #endregion

        #region 試験車使用履歴登録
        /// <summary>
        /// 試験車使用履歴登録
        /// </summary>
        /// <returns></returns>
        private bool EntryTestCarHistory(bool isMainFlow = true)
        {
            // 承認フラグ
            var isSyounin = this.EntryButton.Text != Touroku;

            var isEdit = this.gridUtil.IsEdit;
            if (isEdit == false)
            {
                var siken = this.TestCar.試験着手日;
                siken = siken == null ? null : (DateTime?)siken.Value.Date;

                //試験着手日か試験着手証明文書を変更しているかどうか
                isEdit = this.TestCommencementDayDateTimePicker.SelectedDate != siken || this.TestCommencementDocumentTextBox.Text != (this.TestCar.試験着手証明文書 ?? "");

            }

            // 編集なし・承認ではない場合は以降の処理は行わない
            if (isEdit == false && isSyounin == false)
                return false;

            // 最新の試験車情報を取得
            var testCar = this.GetTestCarList()?.FirstOrDefault();

            // 月例点検省略フラグ
            var isMonthlyInspectionOmit = testCar.月例点検省略有無 == 1;

            var historyList = (this.gridUtil.RowCount == 0 ? Enumerable.Empty<TestCarUseHistoryModel>() : this.gridUtil.DataSource).ToArray();

            Func<int, int, bool> setCurrentCell = (x, y) =>
            {
                this.ListDataGridView.Focus();
                this.ListDataGridView.CurrentCell = this.ListDataGridView[x, y];

                return false;
            };

            //2件以上で編集しているかどうか
            if (this.ListDataGridView.RowCount > 1 && this.gridUtil.IsEdit == true)
            {
                //月例点検承認省略フローではなく、処理日の入力がOKかどうか
                if (!isMonthlyInspectionOmit && this.IsTestCarUseHistoryExist().Any(x => x.CHECK_RESULT == CheckResultType.MonthlyInput) == true)
                {
                    Messenger.Warn(Resources.TCM03019);
                    return false;

                }

                foreach (var edit in this.gridUtil.EditList)
                {
                    var rowindex = historyList.Select((x, i) => new { Content = x, Index = i }).FirstOrDefault(y => y.Content.SEQNO == edit.SEQNO).Index;

                    // 日付
                    if (edit.処理日 == null)
                    {
                        Messenger.Warn(string.Format(Resources.KKM00001, "日付"));
                        return setCurrentCell(this.ListDataGridView.Columns[this.ProcessingDateColumn.Name].Index, rowindex);
                    }

                    // 使用部署
                    if (string.IsNullOrWhiteSpace(edit.使用部署))
                    {
                        Messenger.Warn(string.Format(Resources.KKM00001, "使用部署"));
                        return setCurrentCell(this.ListDataGridView.Columns[this.UseDepartmentColumn.Name].Index, rowindex);
                    }

                    // 試験内容
                    else if (string.IsNullOrWhiteSpace(edit.試験内容))
                    {
                        Messenger.Warn(string.Format(Resources.KKM00001, "試験内容"));
                        return setCurrentCell(this.ListDataGridView.Columns[this.TestContentColumn.Name].Index, rowindex);
                    }

                    // 実走行距離
                    if (string.IsNullOrWhiteSpace(edit.実走行距離))
                    {
                        Messenger.Warn(string.Format(Resources.KKM00001, "実走行距離"));
                        return setCurrentCell(this.ListDataGridView.Columns[this.RunningDistanceColumn.Name].Index, rowindex);
                    }
                    else if (!Regex.IsMatch(edit.実走行距離, "^[0-9]{1,25}$"))
                    {
                        Messenger.Warn(string.Format(Resources.KKM00032, "実走行距離"));
                        return setCurrentCell(this.ListDataGridView.Columns[this.RunningDistanceColumn.Name].Index, rowindex);
                    }

                    //数値に変換できなければ次へ
                    var current = 0;
                    if (int.TryParse(edit.実走行距離, out current) == false)
                    {
                        continue;

                    }

                    var prevContent = historyList.Select((x, i) => new { Content = x, Index = i })?.FirstOrDefault(y => y.Index == rowindex + 1)?.Content;

                    //直前レコードがなければ次へ
                    if (prevContent == null)
                    {
                        continue;

                    }

                    //数値に変換できなければ次へ
                    var prev = 0;
                    if (int.TryParse(prevContent.実走行距離, out prev) == false)
                    {
                        continue;

                    }

                    //登録するか問い合わせ
                    if (current < prev && Messenger.Confirm(Resources.TCM03020) == DialogResult.Yes)
                    {
                        return setCurrentCell(this.ListDataGridView.Columns[this.RunningDistanceColumn.Name].Index, rowindex);
                    }

                }

            }

            //月例点検入力があるかどうか、メイン業務フローかどうか
            var list = historyList.Where(x => x.STEPNO == -1).ToArray();
            if (list.Any() == true && isMainFlow)
            {
                var target = list.Select(x => new ApplicationApprovalCarModel
                {
                    //表示種別
                    TARGET_TYPE = ApplicationApprovalCarTargetType.MonthlyInspection,

                    //データID
                    データID = this.TestCar.データID,

                    //履歴NO
                    履歴NO = this.TestCar.履歴NO,

                    //STEPNO
                    STEPNO = x.STEPNO,

                    //承認要件コード
                    承認要件コード = x.承認要件コード,

                    //管理部署承認
                    管理部署承認 = x.管理部署承認,

                    //承認状況
                    承認状況 = x.承認状況,

                    //承認者レベル
                    承認者レベル = x.承認者レベル,

                    //移管先部署_課ID
                    移管先部署_SECTION_ID = x.移管先部署_SECTION_ID,

                    //移管先部署ID
                    移管先部署ID = x.移管先部署ID,

                    //課ID
                    SECTION_ID = this.TestCar.SECTION_ID,

                    //担当ID
                    SECTION_GROUP_ID = this.TestCar.SECTION_GROUP_ID,

                    //駐車場指定
                    駐車場指定 = x.駐車場指定,

                    //ユーザーID
                    PERSONEL_ID = SessionDto.UserId,

                    //アクセス権限
                    ACCESS_LEVEL = x.承認者レベル,

                    //編集者
                    編集者 = SessionDto.UserId,

                    //チェック結果
                    CHECK_RESULT = CheckResultType.Ok

                }).ToList();

                //試験車使用履歴権限チェック
                if (this.IsTestCarUseHistoryAuthority(target).Any(x => x.CHECK_RESULT != CheckResultType.Ok))
                {
                    Messenger.Warn(Resources.TCM03009);
                    return false;

                }

                //承認中のデータがあるかどうか
                var max = historyList.FirstOrDefault(x => x.SEQNO > 0 && x.SEQNO == historyList.Max(y => y.SEQNO));
                if (max != null && max.STEPNO > 0)
                {
                    Messenger.Warn(Resources.TCM03021);
                    return false;

                }

            }

            //承認対象があるかどうか、メイン業務フローかどうか
            list = historyList.Where(x => x.STEPNO > 0).ToArray();
            if (list.Any() == true && isMainFlow)
            {
                var target = list.Where(x => x.STEPNO > 0).Select(x => new ApplicationApprovalCarModel
                {
                    //データID
                    データID = this.TestCar.データID,

                    //履歴NO
                    履歴NO = this.TestCar.履歴NO,

                    //SEQNO
                    SEQNO = x.SEQNO,

                    //STEPNO
                    STEPNO = x.STEPNO,

                    //承認要件コード
                    承認要件コード = x.承認要件コード,

                    //管理部署承認
                    管理部署承認 = x.管理部署承認,

                    //承認状況
                    承認状況 = x.承認状況,

                    //承認者レベル
                    承認者レベル = x.承認者レベル,

                    //移管先部署_課ID
                    移管先部署_SECTION_ID = x.移管先部署_SECTION_ID,

                    //移管先部署ID
                    移管先部署ID = x.移管先部署ID,

                    //課ID
                    SECTION_ID = this.TestCar.SECTION_ID,

                    //担当ID
                    SECTION_GROUP_ID = this.TestCar.SECTION_GROUP_ID,

                    //駐車場指定
                    駐車場指定 = x.駐車場指定,

                    //ユーザーID
                    PERSONEL_ID = SessionDto.UserId,

                    //アクセス権限
                    ACCESS_LEVEL = x.承認者レベル,

                    //編集者
                    編集者 = x.編集者,

                    //チェック結果
                    CHECK_RESULT = CheckResultType.Ok

                }).ToList();

                //試験車使用履歴権限チェック
                if (this.IsTestCarUseHistoryAuthority(target).Any(x => x.CHECK_RESULT != CheckResultType.Ok))
                {
                    Messenger.Warn(Resources.TCM03009);
                    return false;

                }

                //試験車使用履歴変更チェック
                if (this.IsTestCarUseHistoryChange(target).Any(x => x.CHECK_RESULT != CheckResultType.Ok))
                {
                    Messenger.Warn(Resources.TCM03012);
                    return false;

                }

                using (var form = new ParkingNumberDesignationForm { UserAuthority = this.UserAuthority })
                {
                    foreach (var history in target)
                    {
                        //駐車場指定かどうか
                        if (history.駐車場指定 == 1)
                        {
                            //試験車
                            form.TestCar = new ApplicationApprovalCarModel
                            {
                                //管理票NO
                                管理票NO = this.TestCar.管理票NO,

                                //駐車場番号
                                駐車場番号 = this.TestCar.駐車場番号,

                                //データID
                                データID = this.TestCar.データID,

                            };

                            //Append Start 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
                            bool parkingSet = false;
                            bool activeCheck = false;
                            if ((history.承認要件コード == "E" && history.STEPNO == 2) || (history.承認要件コード == "H" && history.STEPNO == 7))
                            {
                                parkingSet = true;
                                activeCheck = true;
                            }
                            else if ((history.承認要件コード == "F" && history.STEPNO == 3) || (history.承認要件コード == "G" && history.STEPNO == 10)) parkingSet = true;
                            else if (history.承認要件コード == "B" || history.承認要件コード == "C" || history.承認要件コード == "D") parkingSet = true;
                            //Append End 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて

                            //OKかどうか
                            //Append Start 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
                            if (parkingSet)
                            {
                                form.ActiveCheck = activeCheck;
                                //Append End 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
                                if (form.ShowDialog(this) != DialogResult.OK)
                                {
                                    return false;

                                }
                                else
                                {
                                    //駐車場番号
                                    var h = this.gridUtil.DataSource.First(x => x.データID == history.データID && x.履歴NO == history.履歴NO && x.SEQNO == history.SEQNO);
                                    h.駐車場番号 = form.TestCar.入力駐車場番号;

                                }
                                //Append Start 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
                            }
                            //Append End 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて

                        }

                    }

                }

            }

            //試験車使用履歴更新
            if (this.UpdatetTestCarHistory(this.GetEntryTestCarHistory(isMainFlow, isMonthlyInspectionOmit)) == false)
            {
                return false;
            }

            // 月例点検承認省略の場合
            if (isMonthlyInspectionOmit && isMainFlow && this.gridUtil.EditList.Any(x => x.STEPNO == -1))
            {
                // 試験車使用履歴登録
                if (this.InsertTestCarUseHistory(this.gridUtil.DataSource.Where(x => x.STEPNO == -1).Select(x => new ApplicationApprovalCarModel
                {
                    //データID
                    データID = this.TestCar.データID,

                    //履歴NO
                    履歴NO = this.TestCar.履歴NO,

                    //SEQNO
                    SEQNO = x.SEQNO,

                    //承認要件コード
                    承認要件コード = x.承認要件コード,

                    //STEPNO
                    STEPNO = 0,

                    //承認状況
                    承認状況 = "済",

                    //承認者レベル
                    承認者レベル = null,

                    //管理部署承認
                    管理部署承認 = null,

                    //処理日
                    処理日 = x.処理日,

                    //管理責任課名
                    SECTION_CODE = x.管理責任課名,

                    //管理責任部署名
                    SECTION_GROUP_CODE = x.管理責任部署名,

                    //使用課名
                    使用課名 = x.使用課名,

                    //使用部署名
                    使用部署名 = x.使用部署名,

                    //管理所在地
                    ESTABLISHMENT = x.ESTABLISHMENT,

                    //試験内容
                    試験内容 = x.試験内容,

                    //工事区分NO
                    工事区分NO = x.工事区分NO,

                    //実走行距離
                    実走行距離 = x.実走行距離,

                    //編集者
                    編集者 = x.編集者,

                    //移管先部署ID
                    移管先部署ID = null,

                    //駐車場番号
                    駐車場番号 = null,

                    //チェック結果
                    CHECK_RESULT = CheckResultType.Ok,

                    //登録種別
                    ADD_TYPE = AddType.History

                }).ToList()) == false)
                {
                    return false;
                }
            }

            this.処理日リスト = this.gridUtil.DataSource.Where(x => x.STEPNO == -1).Select(x => x.処理日).ToList();

            //登録後メッセージ
            Messenger.Info(Resources.KKM00002);

            return true;

        }

        /// <summary>
        /// 登録可否判定
        /// </summary>
        /// <returns></returns>
        private bool IsEntry()
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

        /// <summary>
        /// 登録試験車使用履歴取得
        /// </summary>
        /// <returns></returns>
        private TestCarHistoryModel GetEntryTestCarHistory(bool isMainFlow = true, bool isMonthlyInspectionOmit = false)
        {
            var history = new TestCarHistoryModel();

            //試験着手日か試験着手日が変更されているかどうか
            if (this.TestCommencementDayDateTimePicker.SelectedDate != this.TestCar.試験着手日 || this.TestCommencementDocumentTextBox.Text != this.TestCar.試験着手証明文書)
            {
                //試験車
                history.TestCar = this.TestCar;

                //試験着手日
                history.TestCar.試験着手日 = this.TestCommencementDayDateTimePicker.SelectedDate;

                //試験着手証明文書
                history.TestCar.試験着手証明文書 = this.TestCommencementDocumentTextBox.Text;

            }

            //試験車使用履歴があるかどうか
            if (this.gridUtil.RowCount > 0)
            {
                // メイン業務フロー（新規・承認・編集を全て行う）
                if (isMainFlow)
                {
                    //試験車使用履歴の承認をするか変更したかどうか
                    var list = this.gridUtil.DataSource.Where(x => x.STEPNO > 0 || this.gridUtil.EditList.Contains(x) == true).ToList();

                    if (list.Any() == true)
                    {
                        //承認以外の編集の場合は編集者を変更、ログインユーザーID未セットの場合はセット
                        list.ForEach(x => { if (x.STEPNO <= 0) x.編集者 = SessionDto.UserId; if (x.PERSONEL_ID == null) x.PERSONEL_ID = SessionDto.UserId; } );

                        // 月例点検承認省略の場合
                        if (isMonthlyInspectionOmit)
                        {
                            // 月例点検（新規追加）は除外
                            list.RemoveAll(x => x.STEPNO == -1);
                        }

                        //試験車使用履歴
                        history.TestCarUseHistoryList = list;

                    }

                }

                // データ編集フロー（既存データの編集のみ行う）
                else
                {
                    //試験車使用履歴を変更したかどうか
                    var list = this.gridUtil.DataSource.Where(x => this.gridUtil.EditList.Contains(x) == true).ToList();

                    //月例点検（新規追加）は除外
                    list.RemoveAll(x => x.STEPNO == -1);

                    if (list.Any() == true)
                    {
                        // STEPNOを便宜上承認済みに変更、編集者を変更、ログインID未セットの場合はセット
                        list.ForEach(x => { x.STEPNO = 0; x.編集者 = SessionDto.UserId; if (x.PERSONEL_ID == null) x.PERSONEL_ID = SessionDto.UserId; });

                        //試験車使用履歴
                        history.TestCarUseHistoryList = list;

                    }

                }

            }

            return history;

        }
        #endregion

        #region API
        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <returns></returns>
        private List<TestCarCommonModel> GetTestCarList()
        {
            var cond = new TestCarCommonSearchModel
            {
                //データID
                データID = this.TestCar.データID

            };

            //APIで取得
            var res = HttpUtil.PostResponse<TestCarCommonModel>(ControllerType.TestCarHistory, cond);

            //レスポンスが取得できたかどうか
            var list = new List<TestCarCommonModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// 試験車使用履歴取得
        /// </summary>
        /// <returns></returns>
        private List<TestCarUseHistoryModel> GetTestCarUseHistoryList()
        {
            var cond = new TestCarUseHistorySearchModel
            {
                //データID
                データID = this.TestCar.データID,

                //履歴NO
                履歴NO = this.TestCar.履歴NO

            };

            //APIで取得
            var res = HttpUtil.GetResponse<TestCarUseHistorySearchModel, TestCarUseHistoryModel>(ControllerType.TestCarUseHistory, cond);

            //レスポンスが取得できたかどうか
            var list = new List<TestCarUseHistoryModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results.OrderByDescending(x => (x.処理日 ?? DateTime.MaxValue).Date).ThenByDescending(x => x.SEQNO));

            }

            return list;

        }

        /// <summary>
        /// 試験車使用履歴存在チェック
        /// </summary>
        /// <returns></returns>
        private List<TestCarUseHistoryModel> IsTestCarUseHistoryExist()
        {
            //APIで取得
            var res = HttpUtil.PostResponse(ControllerType.TestCarUseHistoryExistCheck, this.gridUtil.EditList);

            //レスポンスが取得できたかどうか
            var results = new List<TestCarUseHistoryModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                results.AddRange(res.Results);

            }

            return results;

        }

        /// <summary>
        /// 試験車使用履歴権限チェック
        /// </summary>
        /// <param name="list">試験車使用履歴</param>
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
        /// 試験車使用履歴変更チェック
        /// </summary>
        /// <param name="list">試験車使用履歴</param>
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
        /// 試験車使用履歴登録
        /// </summary>
        /// <param name="useHistory">試験車使用履歴</param>
        /// <returns></returns>
        private bool InsertTestCarUseHistory(List<ApplicationApprovalCarModel> useHistory)
        {
            var res = HttpUtil.PostResponse(ControllerType.TestCarUseHistory, useHistory);

            return res != null && res.Status == Const.StatusSuccess;

        }

        /// <summary>
        /// 試験車使用履歴更新
        /// </summary>
        /// <param name="history">試験車使用履歴</param>
        /// <returns></returns>
        private bool UpdatetTestCarHistory(TestCarHistoryModel history)
        {
            var list = new[] { history };

            var res = HttpUtil.PutResponse(ControllerType.TestCarHistory, list);

            return res != null && res.Status == Const.StatusSuccess;

        }
        #endregion


    }
}
