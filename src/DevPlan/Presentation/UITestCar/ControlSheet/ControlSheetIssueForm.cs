using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UC;
using DevPlan.Presentation.UITestCar.Othe.TestCarHistory;
using DevPlan.Presentation.UITestCar.ControlSheet.Label;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Util;

namespace DevPlan.Presentation.UITestCar.ControlSheet
{
    /// <summary>
    /// 試験車情報（管理票発行）
    /// </summary>
    public partial class ControlSheetIssueForm : BaseSubForm
    {
        #region メンバ変数
        private readonly List<ComboBoxDto> ControlSheetIssueOrList = new List<ComboBoxDto>()
        {
            new ComboBoxDto { ID = "済", CODE = "済", NAME = "発行済" },
            new ComboBoxDto { ID = "未", CODE = "未", NAME = "未発行" }
        };

        private readonly List<ComboBoxDto> GunmaKoujiNoList = new List<ComboBoxDto>()
        {
            new ComboBoxDto { ID = "40", CODE = "40", NAME = "40：改良研究" },
            new ComboBoxDto { ID = "41", CODE = "41", NAME = "41：総務移管" }
        };

        private readonly List<ComboBoxDto> TokyoKoujiNoList = new List<ComboBoxDto>()
        {
        //Update Start 2024/01/21 杉浦 プルダウン変更
            //new ComboBoxDto { ID = "62", CODE = "62", NAME = "62：改良研究" },
            //new ComboBoxDto { ID = "41", CODE = "41", NAME = "41：試験研究" }
            new ComboBoxDto { ID = "62", CODE = "62", NAME = "62：改良研究" },
            new ComboBoxDto { ID = "41", CODE = "41", NAME = "41：試験研究" },
            new ComboBoxDto { ID = "35G", CODE = "35G", NAME = "35G：設備" }
        //Update End 2024/01/21 杉浦 プルダウン変更
        };

        private readonly List<ComboBoxDto> OtherKoujiNoList = new List<ComboBoxDto>()
        {
            new ComboBoxDto { ID = "40", CODE = "40", NAME = "40：改良研究" },
            new ComboBoxDto { ID = "41", CODE = "41", NAME = "41：試験研究" },
            new ComboBoxDto { ID = "62", CODE = "62", NAME = "62：改良研究" }
        };

        private readonly List<ComboBoxDto> SyobunCodeList = new List<ComboBoxDto>()
        {
            new ComboBoxDto { ID = "H", CODE = "H", NAME = "H" },       // 廃却
            new ComboBoxDto { ID = "SI", CODE = "SI", NAME = "SI" },    // 総務移管
            new ComboBoxDto { ID = "HI", CODE = "HI", NAME = "HI" },    // 本社移管
            new ComboBoxDto { ID = "B", CODE = "B", NAME = "B" }        // 売却
        };

        //Update Start 2023/01/25 杉浦 試験車情報画面と受領表の書式変更
        //private readonly List<ComboBoxDto> EVDeviceList = new List<ComboBoxDto>()
        //{
        //    new ComboBoxDto { ID = "PHEV", CODE = "PHEV", NAME = "PHEV" },
        //    new ComboBoxDto { ID = "MHEV", CODE = "MHEV", NAME = "MHEV" },
        //    new ComboBoxDto { ID = "EV", CODE = "EV", NAME = "EV" },
        //    new ComboBoxDto { ID = "HEV", CODE = "HEV", NAME = "HEV" }
        //};
        private readonly List<ComboBoxDto> EVDeviceList = new List<ComboBoxDto>()
        {
            new ComboBoxDto { ID = "BEV", CODE = "BEV", NAME = "BEV" },
            new ComboBoxDto { ID = "MHEV", CODE = "MHEV", NAME = "MHEV" },
            new ComboBoxDto { ID = "PHEV", CODE = "PHEV", NAME = "PHEV" },
            new ComboBoxDto { ID = "SHEV", CODE = "SHEV", NAME = "SHEV" },
        //Append Start 2024/01/21 杉浦 プルダウン変更
            new ComboBoxDto { ID = "REV", CODE = "REV", NAME = "REV" },
        //Append End 2024/01/21 杉浦 プルダウン変更
        };
        //Update End 2023/01/25 杉浦 試験車情報画面と受領表の書式変更

        private readonly string AddRisaicleChikectString = "ﾘｻｲｸﾙ券取得済（{0}）";
        private readonly string AddCPoundSendString = "C/# 石刷りFM1へ送付済（{0}）";
        private readonly string AddControlSheetSendString = "廃却：{0}";

        private readonly string HistoryNoButtonString = "HistoryNoButton";

        private const string ControlSheetExtension = ".xlsx";

        private const string ControlSheetIssueCode = "A";       // 発行
        private const string ScrappingApplicationCode = "D";    // 廃却申請

        private readonly Dictionary<bool, string> MaskingToggleButtonTextMap = new Dictionary<bool, string>()
        {
            { true, "閲覧モードへ" },
            { false, "編集モードへ" }
        };

        private const string SheetName = "受領票";

        //駐車場番号検索結果
        private ParkingModel ParkingInfo = new ParkingModel();

        //Append Start 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
        private List<TestCarCollisionCarManagementDepartmentModel> testCarCollisionSectionGroup = null;
        //Append End 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい

        //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
        private readonly List<ComboBoxDto> CarRecycleList = new List<ComboBoxDto>()
        {
            new ComboBoxDto { ID = "対象外", CODE = "対象外", NAME = "対象外" },
            new ComboBoxDto { ID = "対象", CODE = "対象", NAME = "対象" }
        };
        private readonly List<ComboBoxDto> RefrigerantTypeList = new List<ComboBoxDto>()
        {
            new ComboBoxDto { ID = "Freon", CODE = "Freon", NAME = "HFC 134a フロン" },
            new ComboBoxDto { ID = "Refrigerant", CODE = "Refrigerant", NAME = "HFO 1234yf 新冷媒" },
             new ComboBoxDto { ID = "nofilling", CODE = "nofilling", NAME = "充填なし" }
        };
        //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "試験車情報"; } }

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

        /// <summary>試験車情報（初回）</summary>
        private TestCarCommonModel TestCarDefault { get; set; } = new TestCarCommonModel();

        /// <summary>試験車履歴情報リスト</summary>
        private List<ControlSheetTestCarHistoryGetOutModel> TestCarHistoryList { get; set; } = new List<ControlSheetTestCarHistoryGetOutModel>();

        /// <summary>試験車使用履歴情報リスト</summary>
        private List<TestCarUseHistoryModel> TestCarUseHistoryList { get; set; } = new List<TestCarUseHistoryModel>();

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }

        /// <summary>管理所在地</summary>
        private string ControlEstablishment { get; set; }

        /// <summary>受領部コード</summary>
        private string ReceiptDepartmentCode { get; set; }

        /// <summary>受領課コード</summary>
        private string ReceiptSectionCode { get; set; }

        /// <summary>ページ情報（履歴NO）</summary>
        private PageInfoModel Page { get; set; } = new PageInfoModel();

        /// <summary>親画面リロードデリゲート</summary>
        public Action Reload { get; set; } = () => { };

        /// <summary>閲覧モード専用フラグ</summary>
        public bool IsViewMode { get; set; } = false;

        /// <summary>駐車場移動モードフラグ</summary>
        public bool IsMoveMode { get; set; } = false;

        /// <summary>管理表発行済判別フラグ</summary>
        public string isAdministration { get; set; }

        /// <summary>管理表文言</summary>
        public string adaministrationText { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControlSheetIssueForm()
        {
            InitializeComponent();

        }
        #endregion

        #region 画面のロード
        /// <summary>
        /// 画面のロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetIssueForm_Load(object sender, EventArgs e)
        {
            //Append Start 2021/07/08 矢作
            // 最初に表示する履歴NOのページ
            int pageNo = this.TestCar.履歴NO;
            //Append End 2021/07/08 矢作

            // 試験車情報の取得
            if (this.TestCar?.データID > 0)
            {
                // 試験車情報
                this.TestCar = this.GetData(new TestCarCommonSearchModel() { データID = this.TestCar.データID })?.FirstOrDefault();

                // 初回取得時データの退避
                this.TestCarDefault = this.TestCar.Clone();

                // 試験車使用履歴情報
                this.TestCarUseHistoryList = this.GetUseHistoryData(new TestCarUseHistorySearchModel { データID = (int)this.TestCar.データID });
            }

            // 画面の初期化
            this.InitForm();

            //Append Start 2021/07/08 矢作
            // 履歴NOのページに移動
            if (pageNo != 0)
            {
                this.MoveHistoryPage(pageNo);
            }
            //Append End 2021/07/08 矢作
        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            //バインド中ON
            this.IsBind = true;

            try
            {
                // コンボボックスセット
                this.SetComboBox();

                // コントローラセット
                this.SetControler();

                // フォーム値セット
                this.SetFormData();

                // 履歴NOラベルのセット
                this.SetHistoryNoButton();

                // コントローラのマスクセット
                this.SetControlerMask();

                // 駐車場リンクボタンのセット
                this.SetParkButton();

                //Append Start 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                this.SetCollisionCheck();
                //Append End 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            }
            finally
            {
                //バインド中OFF
                this.IsBind = false;
            }
        }

        /// <summary>
        /// 画面の表示
        /// </summary>
        private void ControlSheetIssueForm_Shown(object sender, EventArgs e)
        {
            // 初期表示フォーカス
            this.ActiveControl = this.BaseControlNoTextBox;
        }
        #endregion

        #region 画面の設定・取得

        #region コンボボックスのセット
        /// <summary>
        /// コンボボックスのセット
        /// </summary>
        private void SetComboBox()
        {
            // 車系
            FormControlUtil.SetComboBoxItem(this.BaseCarSeriesComboBox, GetCarGroupList());

            // 車型
            FormControlUtil.SetComboBoxItem(this.BaseCarTypeComboBox, GetCarTypeList());

            // 発行有無
            FormControlUtil.SetComboBoxItem(this.HistoryControlSheetIssueOrComboBox, ControlSheetIssueOrList, false);

            // 開発符号
            FormControlUtil.SetComboBoxItem(this.HistoryGeneralCodeComboBox, GetGeneralCodeList());

            // 試作時期
            //Update Start 2024/04/18 杉浦 プルダウンの中身を削除
            //FormControlUtil.SetComboBoxItem(this.HistoryPrototypeTimingComboBox, GetPrototypeSeasonList());
            FormControlUtil.SetComboBoxItem(this.HistoryPrototypeTimingComboBox, new List<CommonMasterModel>());
            //Update End 2024/04/18 杉浦 プルダウンの中身を削除

            // 仕向地
            FormControlUtil.SetComboBoxItem(this.HistoryShimukechiComboBox, GetSimuketiList());

            // メーカー名
            FormControlUtil.SetComboBoxItem(this.HistoryMakerNameComboBox, GetMakerNameList());

            // E_G型式
            FormControlUtil.SetComboBoxItem(this.HistoryEGTypeComboBox, GetEngineModelList());

            // 排気量
            FormControlUtil.SetComboBoxItem(this.HistoryDisplacementComboBox, GetDisplacementList());

            // T/M
            FormControlUtil.SetComboBoxItem(this.HistoryTMComboBox, GetTMList());

            // EVデバイス
            FormControlUtil.SetComboBoxItem(this.HistoryEVDeviceComboBox, EVDeviceList);

            // 駆動方式
            FormControlUtil.SetComboBoxItem(this.HistoryDriveSystemComboBox, GetDriveMethodList());

            // グレード
            FormControlUtil.SetComboBoxItem(this.HistoryGradeComboBox, GetGradeList());

            // 車体色
            FormControlUtil.SetComboBoxItem(this.HistoryCarColorComboBox, GetCarBodyColorList());

            // 管理責任部署
            FormControlUtil.SetComboBoxItem(this.HistoryControlSectionGroupComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto { CODE = this.TestCar?.管理責任部署, NAME = this.TestCar?.SECTION_GROUP_CODE } });

            this.HistoryControlSectionGroupComboBox.SelectedValue = this.TestCar?.管理責任部署 ?? string.Empty;

            // 工事区分NO（管理所在地による分岐）
            SetHistoryControlSectionGroupComboBox();

            // 受領先
            FormControlUtil.SetComboBoxItem(this.HistoryReceiptDestinationComboBox, GetRecipientList());

            // 受領部署
            FormControlUtil.SetComboBoxItem(this.HistoryReceiptSectionGroupComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto { CODE = this.TestCar?.受領部署, NAME = this.TestCar?.受領部署_SECTION_GROUP_CODE } });

            this.HistoryReceiptSectionGroupComboBox.SelectedValue = this.TestCar?.受領部署 ?? string.Empty;

            // 受領者
            FormControlUtil.SetComboBoxItem(this.HistoryReceiptUserComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto { CODE = this.TestCar?.受領者, NAME = this.TestCar?.受領者_NAME } });

            this.HistoryReceiptUserComboBox.SelectedValue = this.TestCar?.受領者 ?? string.Empty;

            // 処分コード
            FormControlUtil.SetComboBoxItem(this.FixedDisposalCodeComboBox, SyobunCodeList);

            //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
            // 自動車リサイクル法
            FormControlUtil.SetComboBoxItem(this.CarRecycleComboBox, CarRecycleList);

            // A/C冷媒種類
            FormControlUtil.SetComboBoxItem(this.RefrigerantTypeComboBox, RefrigerantTypeList);
            //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
        }
        #endregion

        #region コントローラのセット
        /// <summary>
        /// コントローラのセット
        /// </summary>
        private void SetControler()
        {
            var isNew = !(this.TestCar?.データID > 0);
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagementDepartment = SessionDto.ManagementDepartmentType.Any(x => x == Const.Kenjitu || x == Const.Kanri);
            var isViewMode = this.IsViewMode;
            var isMoveMode = this.IsMoveMode;
            //Append Start 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            //試験車衝突車管理部署
            this.testCarCollisionSectionGroup = this.testCarCollisionSectionGroup ?? this.GetTestCarCollisionSectionGroup();
            var isSystemManager = this.UserAuthority.ROLL_ID_LIST.Contains("10"); //システム管理者か?
            var isCollisionUser = this.testCarCollisionSectionGroup.Any(x => x.SECTION_ID == SessionDto.SectionID) == true; //衝突管理部署所属者か?
            var isCollisionTestCar = this.testCarCollisionSectionGroup.Any(x => x.SECTION_GROUP_ID == this.TestCar.管理責任部署) == true; //衝突管理部署の管理している車か?
            var isTestCarCollisionSectionGroup = ( isSystemManager || isCollisionUser) && isCollisionTestCar;
            //Append End 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい

            // 登録・更新
            if (true)
            {
                // 前送・先送ボタン
                this.BeforeHistoryButton.Visible = !isNew;
                this.AfterHistoryButton.Visible = !isNew;

                // 登録(クリア)ボタン
                this.EntryClearButton.Visible = isNew;

                // 登録(コピー)ボタン
                this.EntryCopyButton.Visible = isNew;

                // 編集トグルボタン
                this.MaskingCheckBox.Visible = !isNew;

                // 更新ボタン
                this.EditButton.Visible = !isNew;

                // 管理票発行ボタン
                this.IssueButton.Visible = !isNew;

                // 新規履歴作成ボタン
                this.ControlSheetHistoryEntryButton.Visible = !isNew;

                // 試験車使用履歴ボタン
                this.TestCarUseHistoryButton.Visible = !isNew;

                // ラベル印刷ボタン
                this.ControlLabelIssueButton.Visible = !isNew;

                // 管理票印刷
                this.ReceiptSlipIssueButton.Visible = !isNew;

                //Append Start 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                if (!isTestCarCollisionSectionGroup)
                {
                    this.CollisionFinishedCheckBox.Visible = false;
                }
                //Append End 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            }

            // 更新権限なし
            if (!isUpdate)
            {
                // 登録(クリア)ボタン
                this.EntryClearButton.Visible = isUpdate;

                // 登録(コピー)ボタン
                this.EntryCopyButton.Visible = isUpdate;

                // 編集トグルボタン
                this.MaskingCheckBox.Visible = isUpdate;

                // 更新ボタン
                this.EditButton.Visible = isUpdate;

                // 管理票発行ボタン
                this.IssueButton.Visible = isUpdate;

                // 新規履歴作成ボタン
                this.ControlSheetHistoryEntryButton.Visible = isUpdate;
            }

            // 出力権限なし
            if (!isExport)
            {
                // ラベル印刷ボタン
                this.ControlLabelIssueButton.Visible = isExport;

                // 管理票印刷
                this.ReceiptSlipIssueButton.Visible = isExport;
            }

            // 試験車管理部署権限なし
            if (!isManagementDepartment)
            {
                // 編集トグルボタン
                this.MaskingCheckBox.Visible = isManagementDepartment;
            }

            // ウィンドウ制御
            if (this.Owner.GetType() == typeof(TestCarHistoryForm))
            {
                // 試験車使用履歴ボタン
                this.TestCarUseHistoryButton.Visible = false;
            }

            // 閲覧専用モード
            if (isViewMode)
            {
                // 編集トグルボタン
                this.MaskingCheckBox.Visible = false;
            }

            // 駐車場移動モード
            if (isMoveMode)
            {
                // 試験車使用履歴ボタン
                this.TestCarUseHistoryButton.Visible = false;
            }
        }
        #endregion

        #region コントローラのマスクセット
        /// <summary>
        /// コントローラのマスクセット
        /// </summary>
        private void SetControlerMask(bool flg = false)
        {
            var isNew = !(this.TestCar?.データID > 0);
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            if (isNew) return;

            var list = new List<Control>();

            // 除外：編集ボタン
            list.Add(this.MaskingCheckBox);
            // 除外：前送りボタン
            list.Add(this.BeforeHistoryButton);
            // 除外：後送りボタン
            list.Add(this.AfterHistoryButton);

            // 除外：履歴番号ボタン
            for (var i = 1; i <= this.Page.PageCount; i++)
            {
                list.Add(new System.Windows.Forms.Button() { Name = string.Concat(HistoryNoButtonString, i.ToString()) });
            }

            // 除外：管理票NO
            list.Add(this.BaseControlNoTextBox);
            // 除外：受領先
            list.Add(this.HistoryReceiptDestinationComboBox);

            // 一括マスク
            this.SetMaskingControls(this.ListFormMainPanel, flg, list);

            // 追加：更新ボタン
            this.EditButton.Visible = flg;

            // 追加：管理票発行ボタン
            this.IssueButton.Visible = flg;

            // 追加：新規履歴作成ボタン
            this.ControlSheetHistoryEntryButton.Visible = flg;

            // 追加：試験車使用履歴
            this.TestCarUseHistoryButton.Enabled = !flg;

            // 追加：ラベル印刷
            this.ControlLabelIssueButton.Enabled = !flg;

            // 追加：受領票印刷
            this.ReceiptSlipIssueButton.Enabled = !flg;
        }
        #endregion

        #region フォーム値のセット
        /// <summary>
        /// フォーム値のセット
        /// </summary>
        private void SetFormData()
        {
            // 基本情報
            this.SetBaseFormData();

            // 履歴情報
            this.SetHistoryFormData();

            // 固定資産情報
            this.SetFixedFormData();

            // 移管履歴情報
            this.SetUseHistoryFormData();
        }
        #endregion

        #region フォーム値（試験車基本情報）のセット
        /// <summary>
        /// フォーム値（試験車基本情報）のセット
        /// </summary>
        private void SetBaseFormData()
        {
            // 管理票NO
            this.BaseControlNoTextBox.Text = this.TestCar?.管理票NO;

            // 正式取得日
            this.BaseGetDayDateTimePicker.Value = this.TestCar?.正式取得日;
            this.BaseGetDayDateTimePicker.Checked = false;

            // 車系
            this.BaseCarSeriesComboBox.Text = this.TestCar?.車系;

            // 車型
            this.BaseCarTypeComboBox.Text = this.TestCar?.車型;

            // 駐車場番号
            this.BaseParkingNoComboBox.Items.Clear();
            this.BaseParkingNoComboBox.Items.Add(this.TestCar?.駐車場番号 ?? "");
            this.BaseParkingNoComboBox.SelectedIndex = 0;

            // 型式符号
            this.BaseTypeCodeTextBox.Text = this.TestCar?.型式符号;

            // リースNO
            this.BaseLeaseNoTextBox.Text = this.TestCar?.リースNO;

            // リース満了日
            this.BaseLeaseDayDateTimePicker.Value = this.TestCar?.リース満了日;

            // 研実管理廃却申請受理日
            this.BaseDisposalKenjitsuReceiptDayDateTimePicker.Value = this.TestCar?.研実管理廃却申請受理日;

            // 廃却見積日
            this.BaseDisposalEstimatedDayDateTimePicker.Value = this.TestCar?.廃却見積日;

            // 廃却決済承認年月
            this.BaseDisposalApprovalMonthDateTimePicker.Value = this.TestCar?.廃却決済承認年月;

            // 車両搬出年月日
            this.BaseCarCarryingOutDayDateTimePicker.Value = this.TestCar?.車両搬出日;

            // メモ
            this.BaseMemoTextBox.Text = this.TestCar?.メモ;

            // 月例点検省略有無
            this.BaseMonthlyInspectionOmitCheckBox.Checked = this.TestCar?.月例点検省略有無 == 1;

            //Append Start 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            // 衝突試験済
            this.CollisionFinishedCheckBox.Checked = this.TestCar?.衝突試験済 == 1;
            //Update Start 2021/08/20 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            //this.CollisionFinishedCheckBox.Enabled = !(this.CollisionFinishedCheckBox.Checked == true);
            this.CollisionFinishedCheckBox.Enabled = !(this.CollisionFinishedCheckBox.Checked == true || this.TestCar?.車両搬出日 != null);
            //Update End 2021/08/20 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            //Append End 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
        }
        #endregion

        #region フォーム値（試験車履歴情報）のセット
        /// <summary>
        /// フォーム値（試験車履歴情報）のセット
        /// </summary>
        private void SetHistoryFormData()
        {
            // 使用履歴情報の取得
            var usehistory = this.TestCarUseHistoryList.Where(x => x.履歴NO == (int)this.TestCar?.履歴NO).ToList();

            // 管理票発行有無
            this.HistoryControlSheetIssueOrComboBox.SelectedValue = this.TestCar?.管理票発行有無 ?? "未";
            this.isAdministration = this.TestCar?.管理票発行有無 ?? "未";

            // 発行年月日
            this.HistoryControlSheetIssueDayDateTimePicker.Value = this.TestCar?.発行年月日;

            // 開発符号
            this.HistoryGeneralCodeComboBox.Text = this.TestCar?.開発符号;

            // 試作時期
            this.HistoryPrototypeTimingComboBox.Text = this.TestCar?.試作時期;

            // 号車
            this.HistoryVehicleTextBox.Text = this.TestCar?.号車;

            // 仕向地
            this.HistoryShimukechiComboBox.Text = this.TestCar?.仕向地;

            // メーカー名
            this.HistoryMakerNameComboBox.Text = this.TestCar?.メーカー名;

            // 外製車名
            this.HistoryOuterCarNameTextBox.Text = this.TestCar?.外製車名;

            // 名称備考
            this.HistoryNameRemarksTextBox.Text = this.TestCar?.名称備考;

            // 車体番号
            this.HistoryVehicleNoTextBox.Text = this.TestCar?.車体番号;

            // 試験目的
            this.HistoryTestPurposeTextBox.Text = this.TestCar?.試験目的;

            // E_G番号
            this.HistoryEGNotextBox.Text = this.TestCar?.E_G番号;

            // E_G型式
            this.HistoryEGTypeComboBox.Text = this.TestCar?.E_G型式;

            // 排気量
            this.HistoryDisplacementComboBox.Text = this.TestCar?.排気量;

            // T/M
            this.HistoryTMComboBox.Text = this.TestCar?.トランスミッション;

            // EVデバイス
            this.HistoryEVDeviceComboBox.Text = this.TestCar?.EVデバイス;

            // 駆動方式
            this.HistoryDriveSystemComboBox.Text = this.TestCar?.駆動方式;

            // グレード
            this.HistoryGradeComboBox.Text = this.TestCar?.グレード;

            // 車体色
            this.HistoryCarColorComboBox.Text = this.TestCar?.車体色;

            // ETC付
            this.HistoryETCCheckBox.Checked = this.TestCar?.FLAG_ETC付 == 1;

            // ナビ付
            this.HistoryNaviCheckBox.Checked = this.TestCar?.FLAG_ナビ付 == 1;

            // 受領時走行距離
            this.HistoryMileageTextBox.Text = this.TestCar?.受領時走行距離;

            // 完成日
            this.HistoryCompletionDayDateTimePicker.Value = this.TestCar?.完成日;

            // 管理責任部署
            FormControlUtil.SetComboBoxItem(this.HistoryControlSectionGroupComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto { CODE = this.TestCar?.管理責任部署, NAME = this.TestCar?.SECTION_GROUP_CODE } });

            this.HistoryControlSectionGroupComboBox.SelectedValue = this.TestCar?.管理責任部署 ?? string.Empty;

            // 研命ナンバー
            this.HistoryKenmeiPlateNumberTextBox.Text = this.TestCar?.研命ナンバー;

            // 固定資産NO
            this.HistoryFixedAssetNoTextBox.Text = this.TestCar?.固定資産NO;

            // 登録ナンバー
            this.HistoryLicensePlateNumberTextBox.Text = this.TestCar?.登録ナンバー;

            // 初年度登録年月
            this.FirstEntryDateDateTimePicker.Value = this.TestCar?.初年度登録年月;

            // 登録年月日
            this.HistoryLicenseDayDateTimePicker.Value = this.TestCar?.車検登録日;

            // 車検期限
            this.HistoryInspectionDayDateTimePicker.Value = this.TestCar?.車検期限;

            // 廃艦年月日
            this.HistoryAbolitionShipsDayDateTimePicker.Value = this.TestCar?.廃艦日;

            // 試験着手日
            this.HistoryTestStartDayDateTimePicker.Value = this.TestCar?.試験着手日;

            // 試験着手証明文書
            this.HistoryTestStartCertificateTextBox.Text = this.TestCar?.試験着手証明文書;

            // 工事区分NO
            this.HistoryConstructionNoComboBox.Text = this.TestCar?.工事区分NO;

            // 受領先
            this.HistoryReceiptDestinationComboBox.SelectedValue
                = usehistory?.Where(x => x?.SEQNO == usehistory?.Where(y => y.承認要件コード == ControlSheetIssueCode)?.Max(max => max?.SEQNO)
                && x?.承認要件コード == ControlSheetIssueCode)?.FirstOrDefault()?.試験内容 ?? string.Empty;

            // 受領日
            this.HistoryReceiptDayDateTimePicker.Value = this.TestCar?.受領日;

            // 受領部署
            FormControlUtil.SetComboBoxItem(this.HistoryReceiptSectionGroupComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto { CODE = this.TestCar?.受領部署, NAME = this.TestCar?.受領部署_SECTION_GROUP_CODE } }, false);

            this.HistoryReceiptSectionGroupComboBox.SelectedValue = this.TestCar?.受領部署 ?? string.Empty;

            // 受領者
            FormControlUtil.SetComboBoxItem(this.HistoryReceiptUserComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto { CODE = this.TestCar?.受領者, NAME = this.TestCar?.受領者_NAME } }, false);

            this.HistoryReceiptUserComboBox.SelectedValue = this.TestCar?.受領者 ?? string.Empty;

            //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
            // 自動車ﾘｻｲｸﾙ法
            this.CarRecycleComboBox.Text = this.TestCar?.自動車ﾘｻｲｸﾙ法;

            // A/C冷媒種類
            this.RefrigerantTypeComboBox.Text = this.TestCar?.A_C冷媒種類;
            //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
        }
        #endregion

        #region フォーム値（試験車履歴情報：参照）のセット
        /// <summary>
        /// フォーム値（試験車履歴情報：参照）のセット
        /// </summary>
        private void SetHistoryFormViewData()
        {
            var history = this.TestCarHistoryList.Where(x => x.履歴NO == this.Page.CurrentPage).FirstOrDefault();

            var usehistory = this.TestCarUseHistoryList.Where(x => x.履歴NO == (int)history.履歴NO).ToList();

            // 管理票発行有無
            this.HistoryControlSheetIssueOrComboBox.SelectedValue = history?.管理票発行有無 ?? "未";
            this.isAdministration = this.TestCar?.管理票発行有無 ?? "未";

            // 発行年月日
            this.HistoryControlSheetIssueDayDateTimePicker.Value = history?.発行年月日;

            // 開発符号
            this.HistoryGeneralCodeComboBox.Text = history?.開発符号;

            // 試作時期
            this.HistoryPrototypeTimingComboBox.Text = history?.試作時期;

            // 号車
            this.HistoryVehicleTextBox.Text = history?.号車;

            // 仕向地
            this.HistoryShimukechiComboBox.Text = history?.仕向地;

            // メーカー名
            this.HistoryMakerNameComboBox.Text = history?.メーカー名;

            // 外製車名
            this.HistoryOuterCarNameTextBox.Text = history?.外製車名;

            // 名称備考
            this.HistoryNameRemarksTextBox.Text = history?.名称備考;

            // 車体番号
            this.HistoryVehicleNoTextBox.Text = history?.車体番号;

            // 試験目的
            this.HistoryTestPurposeTextBox.Text = history?.試験目的;

            // E_G番号
            this.HistoryEGNotextBox.Text = history?.E_G番号;

            // E_G型式
            this.HistoryEGTypeComboBox.Text = history?.E_G型式;

            // 排気量
            this.HistoryDisplacementComboBox.Text = history?.排気量;

            // T/M
            this.HistoryTMComboBox.Text = history?.トランスミッション;

            // EVデバイス
            this.HistoryEVDeviceComboBox.Text = history?.EVデバイス;

            // 駆動方式
            this.HistoryDriveSystemComboBox.Text = history?.駆動方式;

            // グレード
            this.HistoryGradeComboBox.Text = history?.グレード;

            // 車体色
            this.HistoryCarColorComboBox.Text = history?.車体色;

            // ETC付
            this.HistoryETCCheckBox.Checked = history?.FLAG_ETC付 == 1;

            // ナビ付
            this.HistoryNaviCheckBox.Checked = history?.FLAG_ナビ付 == 1;

            // 受領時走行距離
            this.HistoryMileageTextBox.Text = history?.受領時走行距離;

            // 完成日
            this.HistoryCompletionDayDateTimePicker.Value = history?.完成日;

            // 管理責任部署
            FormControlUtil.SetComboBoxItem(this.HistoryControlSectionGroupComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto { CODE = history?.管理責任部署, NAME = history?.管理責任部署コード } });

            this.HistoryControlSectionGroupComboBox.SelectedValue = history?.管理責任部署 ?? string.Empty;

            // 研命ナンバー
            this.HistoryKenmeiPlateNumberTextBox.Text = history?.研命ナンバー;

            // 固定資産NO
            this.HistoryFixedAssetNoTextBox.Text = history?.固定資産NO;

            // 登録ナンバー
            this.HistoryLicensePlateNumberTextBox.Text = history?.登録ナンバー;

            // 初年度登録年月
            this.FirstEntryDateDateTimePicker.Value = history?.初年度登録年月;

            // 登録年月日
            this.HistoryLicenseDayDateTimePicker.Value = history?.車検登録日;

            // 車検期限
            this.HistoryInspectionDayDateTimePicker.Value = history?.車検期限;

            // 廃艦年月日
            this.HistoryAbolitionShipsDayDateTimePicker.Value = history?.廃艦日;

            // 試験着手日
            this.HistoryTestStartDayDateTimePicker.Value = history?.試験着手日;

            // 試験着手証明文書
            this.HistoryTestStartCertificateTextBox.Text = history?.試験着手証明文書;

            // 工事区分NO
            this.HistoryConstructionNoComboBox.Text = history?.工事区分NO;

            // 受領先
            this.HistoryReceiptDestinationComboBox.SelectedValue
                = usehistory?.Where(x => x?.SEQNO == usehistory?.Where(y => y.承認要件コード == ControlSheetIssueCode)?.Max(max => max?.SEQNO)
                && x?.承認要件コード == ControlSheetIssueCode)?.FirstOrDefault()?.試験内容 ?? string.Empty;

            // 受領日
            this.HistoryReceiptDayDateTimePicker.Value = history?.受領日;

            // 受領部署
            FormControlUtil.SetComboBoxItem(this.HistoryReceiptSectionGroupComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto { CODE = history?.受領部署, NAME = history?.受領部署コード } });

            this.HistoryReceiptSectionGroupComboBox.SelectedValue = history?.受領部署 ?? string.Empty;

            // 受領者
            FormControlUtil.SetComboBoxItem(this.HistoryReceiptUserComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto { CODE = history?.受領者, NAME = history?.受領者名 } });

            this.HistoryReceiptUserComboBox.SelectedValue = history?.受領者 ?? string.Empty;

            //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
            // 自動車ﾘｻｲｸﾙ法
            this.CarRecycleComboBox.Text = history?.自動車ﾘｻｲｸﾙ法;

            // A/C冷媒種類
            this.RefrigerantTypeComboBox.Text = history?.A_C冷媒種類;
            //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
        }
        #endregion

        #region フォーム値（試験車履歴情報）のセット
        /// <summary>
        /// フォーム値（固定資産情報）のセット
        /// </summary>
        private void SetFixedFormData()
        {
            // 処分予定年月
            this.FixedDisposalPlanDayDateTimePicker.Value = this.TestCar?.処分予定年月;

            // 処分コード
            this.FixedDisposalCodeComboBox.SelectedValue = this.TestCar?.処分コード ?? string.Empty;
        }
        #endregion

        #region フォーム値（移管履歴）のセット
        /// <summary>
        /// フォーム値（移管履歴）のセット
        /// </summary>
        private void SetUseHistoryFormData()
        {
            // 一覧初期化
            this.ListDataGridView.DataSource = null;

            // 列の自動生成可否
            this.ListDataGridView.AutoGenerateColumns = false;

            // データのバインド
            if (this.TestCar?.データID > 0)
            {
                var ikan = new string[] { "E", "F", "G", "H" };
                this.ListDataGridView.DataSource = this.TestCarUseHistoryList.Where(x => ikan.Contains(x.承認要件コード) && x.STEPNO == 0 && x.承認状況 == "済").ToList();
            }

            // 列と行の幅を調整
            this.ListDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            this.ListDataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            // 一覧を未選択状態に設定
            this.ListDataGridView.CurrentCell = null;
        }
        #endregion

        #region フォーム値の取得
        /// <summary>
        /// フォーム値の取得
        /// </summary>
        private void GetFormData()
        {
            var isNew = !(this.TestCar?.データID > 0);

            // データID
            this.TestCar.データID = isNew ? 0 : Convert.ToInt32(this.TestCar?.データID);

            // 管理票NO
            this.TestCar.管理票NO = isNew ? "0" : this.BaseControlNoTextBox?.Text;

            // 正式取得日
            this.TestCar.正式取得日 = this.BaseGetDayDateTimePicker.SelectedDate;

            // 車系
            this.TestCar.車系 = this.BaseCarSeriesComboBox.Text;

            // 車型
            this.TestCar.車型 = this.BaseCarTypeComboBox.Text;

            // 駐車場番号
            this.TestCar.駐車場番号 = this.BaseParkingNoComboBox.Text;

            // 型式符号
            this.TestCar.型式符号 = this.BaseTypeCodeTextBox.Text;

            // リースNO
            this.TestCar.リースNO = this.BaseLeaseNoTextBox.Text;

            // リース満了日
            this.TestCar.リース満了日 = this.BaseLeaseDayDateTimePicker.SelectedDate;

            // 研実管理廃却申請受理日
            this.TestCar.研実管理廃却申請受理日 = this.BaseDisposalKenjitsuReceiptDayDateTimePicker.SelectedDate;

            // 廃却見積日
            this.TestCar.廃却見積日 = this.BaseDisposalEstimatedDayDateTimePicker.SelectedDate;

            // 廃却決済承認年月
            this.TestCar.廃却決済承認年月 = this.BaseDisposalApprovalMonthDateTimePicker.SelectedDate?.AddMonths(1).AddDays(-1);

            // 車両搬出年月日
            this.TestCar.車両搬出日 = this.BaseCarCarryingOutDayDateTimePicker.SelectedDate;

            // メモ
            this.TestCar.メモ = this.BaseMemoTextBox.Text;

            // 月例点検省略有無
            //Update Start 2022/03/24 杉浦 月例点検省略部署管理
            //this.TestCar.月例点検省略有無 = this.BaseMonthlyInspectionOmitCheckBox.Checked ? 1 : (int?)null;
            this.TestCar.月例点検省略有無 = this.TestCar.月例点検省略対象部署 != 1 ? this.BaseMonthlyInspectionOmitCheckBox.Checked ? 1 : (int?)null : this.TestCar.月例点検省略有無_BASE;
            //Update End 2022/03/24 月例点検省略部署管理

            //Append Start 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            // 衝突試験済
            this.TestCar.衝突試験済 = this.CollisionFinishedCheckBox.Checked ? 1 : (int?)null;
            //Append End 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい

            // 履歴NO
            this.TestCar.履歴NO = isNew ? 0 : Convert.ToInt32(this.TestCar.履歴NO);

            // 管理票発行有無
            this.TestCar.管理票発行有無 = this.HistoryControlSheetIssueOrComboBox.SelectedValue.ToString();

            // 発行年月日
            this.TestCar.発行年月日 = this.HistoryControlSheetIssueDayDateTimePicker.SelectedDate;

            // 開発符号
            this.TestCar.開発符号 = this.HistoryGeneralCodeComboBox.Text;

            // 試作時期
            this.TestCar.試作時期 = this.HistoryPrototypeTimingComboBox.Text;

            // 号車
            this.TestCar.号車 = this.HistoryVehicleTextBox.Text;

            // 仕向地
            this.TestCar.仕向地 = this.HistoryShimukechiComboBox.Text;

            // メーカー名
            this.TestCar.メーカー名 = this.HistoryMakerNameComboBox.Text;

            // 外製車名
            this.TestCar.外製車名 = this.HistoryOuterCarNameTextBox.Text;

            // 名称備考
            this.TestCar.名称備考 = this.HistoryNameRemarksTextBox.Text;

            // 車体番号
            this.TestCar.車体番号 = this.HistoryVehicleNoTextBox.Text;

            // 試験目的
            this.TestCar.試験目的 = this.HistoryTestPurposeTextBox.Text;

            // E_G番号
            this.TestCar.E_G番号 = this.HistoryEGNotextBox.Text;

            // E_G型式
            this.TestCar.E_G型式 = this.HistoryEGTypeComboBox.Text;

            // 排気量
            this.TestCar.排気量 = this.HistoryDisplacementComboBox.Text;

            // T/M
            this.TestCar.トランスミッション = this.HistoryTMComboBox.Text;

            // EVデバイス
            this.TestCar.EVデバイス = this.HistoryEVDeviceComboBox.Text;

            // 駆動方式
            this.TestCar.駆動方式 = this.HistoryDriveSystemComboBox.Text;

            // グレード
            this.TestCar.グレード = this.HistoryGradeComboBox.Text;

            // 車体色
            this.TestCar.車体色 = this.HistoryCarColorComboBox.Text;

            // ETC付
            this.TestCar.FLAG_ETC付 = (short?)(this.HistoryETCCheckBox.Checked ? 1 : 0);

            // ナビ付
            this.TestCar.FLAG_ナビ付 = (short?)(this.HistoryNaviCheckBox.Checked ? 1 : 0);

            // 受領時走行距離
            this.TestCar.受領時走行距離 = this.HistoryMileageTextBox.Text;

            // 完成日
            this.TestCar.完成日 = this.HistoryCompletionDayDateTimePicker.SelectedDate;

            // 管理責任部署
            this.TestCar.管理責任部署 = this.HistoryControlSectionGroupComboBox?.SelectedValue?.ToString();
            this.TestCar.SECTION_GROUP_CODE = this.HistoryControlSectionGroupComboBox?.Text?.ToString();

            // 研命ナンバー
            this.TestCar.研命ナンバー = this.HistoryKenmeiPlateNumberTextBox.Text;

            // 固定資産NO
            this.TestCar.固定資産NO = this.HistoryFixedAssetNoTextBox.Text;

            // 登録ナンバー
            this.TestCar.登録ナンバー = this.HistoryLicensePlateNumberTextBox.Text;

            // 初年度登録年月
            this.TestCar.初年度登録年月 = this.FirstEntryDateDateTimePicker.SelectedDate?.AddMonths(1).AddDays(-1);

            // 登録年月日
            this.TestCar.車検登録日 = this.HistoryLicenseDayDateTimePicker.SelectedDate;

            // 車検期限
            this.TestCar.車検期限 = this.HistoryInspectionDayDateTimePicker.SelectedDate;

            // 廃艦年月日
            this.TestCar.廃艦日 = this.HistoryAbolitionShipsDayDateTimePicker.SelectedDate;

            // 試験着手日
            this.TestCar.試験着手日 = this.HistoryTestStartDayDateTimePicker.SelectedDate;

            // 試験着手証明文書
            this.TestCar.試験着手証明文書 = this.HistoryTestStartCertificateTextBox.Text;

            // 工事区分NO
            this.TestCar.工事区分NO = this.HistoryConstructionNoComboBox.SelectedIndex > 0
                ? this.HistoryConstructionNoComboBox?.SelectedValue.ToString()
                : this.HistoryConstructionNoComboBox?.Text;

            // 受領日
            this.TestCar.受領日 = this.HistoryReceiptDayDateTimePicker.SelectedDate;

            // 受領部署
            this.TestCar.受領部署 = this.HistoryReceiptSectionGroupComboBox?.SelectedValue?.ToString();
            this.TestCar.受領部署_SECTION_GROUP_CODE = this.HistoryReceiptSectionGroupComboBox?.Text?.ToString();

            // 受領者
            this.TestCar.受領者 = this.HistoryReceiptUserComboBox?.SelectedValue?.ToString();
            this.TestCar.受領者_NAME = this.HistoryReceiptUserComboBox?.Text?.ToString();

            // 処分予定年月
            this.TestCar.処分予定年月 = this.FixedDisposalPlanDayDateTimePicker.SelectedDate?.AddMonths(1).AddDays(-1);

            // 処分コード
            this.TestCar.処分コード = this.FixedDisposalCodeComboBox?.SelectedValue?.ToString();

            //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
            // 自動車ﾘｻｲｸﾙ法
            this.TestCar.自動車ﾘｻｲｸﾙ法 = this.CarRecycleComboBox.Text;

            // A/C冷媒種類
            this.TestCar.A_C冷媒種類 = this.RefrigerantTypeComboBox.Text;
            //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
        }
        #endregion

        #region 履歴NOボタンのセット
        /// <summary>
        /// 履歴NOボタンのセット
        /// </summary>
        private void SetHistoryNoButton()
        {
            // ページ情報のセット
            this.Page.PageCount = this.TestCar.履歴NO <= 0 ? 1 : this.TestCar.履歴NO;
            this.Page.CurrentPage = this.Page.PageCount;

            var x = this.RirekiLabel.Location.X + this.RirekiLabel.Width + 3;
            var y = this.RirekiLabel.Location.Y - 4;
            var s = 12;
            var w = 18;
            var h = 15;

            for (var i = 1; i <= this.Page.PageCount; i++)
            {
                var button = new System.Windows.Forms.Button();

                button.Name = string.Concat(HistoryNoButtonString, i.ToString());
                button.Text = i.ToString();

                if (i > 10) x = x + 3;

                button.Location = new Point(x, y);
                button.Size = new Size(w, h);
                button.AutoSize = true;
                button.TabStop = false;

                button.Click += HistoryNoButton_Click;

                button.Font = i == this.Page.PageCount ? new Font(button.Font, FontStyle.Bold) : button.Font;

                this.ListFormMainPanel.Controls.Add(button);

                x = x + s + w;
            }

            // 履歴
            if (0 < this.Page.PageCount)
            {
                this.TestCarHistoryList = this.GetHistoryData();
            }
        }
        #endregion

        private void SetParkButton()
        {
            if (!string.IsNullOrEmpty(this.TestCar.XEYE_EXIST) && (this.TestCar.XEYE_EXIST).Equals("あり"))
            {
                this.GpsGoButton.Text = "Map";
                this.GpsGoButton.Enabled = true;
            }
            else
            {
                this.GpsGoButton.Text = null;
                this.GpsGoButton.Enabled = false;
            }
        }

        //Append Start 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
        private void SetCollisionCheck()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            this.testCarCollisionSectionGroup = this.testCarCollisionSectionGroup ?? this.GetTestCarCollisionSectionGroup();
            var isCollisionUser = this.testCarCollisionSectionGroup.Any(x => x.SECTION_ID == SessionDto.SectionID) == true;
            var isCollisionTestCar = this.testCarCollisionSectionGroup.Any(x => x.SECTION_GROUP_ID == this.TestCar.管理責任部署) == true;
            var isTestCarCollisionSectionGroup = (isManagement || isCollisionUser) && isCollisionTestCar;

            //Update Start 2021/08/20 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            //if (isTestCarCollisionSectionGroup) this.CollisionFinishedCheckBox.Enabled = !(this.CollisionFinishedCheckBox.Checked == true);
            if (isTestCarCollisionSectionGroup) this.CollisionFinishedCheckBox.Enabled = !(this.CollisionFinishedCheckBox.Checked == true || this.TestCar?.車両搬出日 != null);
            //Update End 2021/08/20 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
        }
        //Append End 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい

        #endregion

        #region イベント

        #region 編集チェックボックスのクリック
        /// <summary>
        /// 編集チェックボックスのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaskingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.IsBind) return;

            var flg = ((CheckBox)sender).Checked;

            var isAcctive = this.Page.CurrentPage == this.Page.PageCount;
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            if (!flg)
            {
                var ctllist = new List<Control>();

                FormControlUtil.GetInputControls(this.ListFormMainPanel, ctllist, true);

                // フォーム値の取得
                if (isAcctive) this.GetFormData();

                foreach (var ctl in ctllist)
                {
                    if (ctl.Name == string.Empty) continue;

                    var isChange = this.CompareObjectValue(this.TestCar, this.TestCarDefault, ctl);

                    // 変更がある場合
                    if (isChange && isUpdate)
                    {
                        // 編集中の確認
                        if (Messenger.Confirm(Resources.TCM00009) != DialogResult.Yes)
                        {
                            this.IsBind = true;

                            // キャンセル
                            try { ((CheckBox)sender).Checked = !flg; } finally { this.IsBind = false; }

                            return;
                        }

                        // 入力項目のチェック
                        if (!this.IsEntryDataCheck())
                        {
                            this.IsBind = true;

                            // キャンセル
                            try { ((CheckBox)sender).Checked = !flg; } finally { this.IsBind = false; }

                            return;
                        }

                        // フォーム値の再取得
                        this.GetFormData();

                        var res = this.PutData();

                        if (res.Status.Equals(Const.StatusSuccess))
                        {
                            // 試験車情報
                            this.TestCar = this.GetData(new TestCarCommonSearchModel() { データID = this.TestCar.データID })?.FirstOrDefault();

                            // フォーム値セット
                            this.SetFormData();

                            // 試験車情報（初回）の更新
                            this.TestCarDefault = this.TestCar.Clone();

                            Messenger.Info(Resources.TCM00008); // 更新完了
                        }
                    }
                }
            }

            this.MaskingCheckBox.Text = MaskingToggleButtonTextMap[flg];

            this.SetControlerMask(flg);
            this.SetParkButton();

            //Append Start 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            this.SetCollisionCheck();
            //Append End 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
        }
        #endregion

        #region 履歴NOのクリック
        /// <summary>
        /// 履歴NOのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryNoButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var moveno = Convert.ToInt32(button.Text);

            // 遷移なしの場合は終了
            if (moveno == this.Page.CurrentPage)
            {
                return;
            }

            this.MoveHistoryPage(moveno);
        }
        #endregion

        #region 正式取得日のバリデーション
        /// <summary>
        /// 正式取得日のバリデーション
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseGetDayDateTimePicker_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // バインド中は終了
            if (this.IsBind)
            {
                return;
            }

            // 変更なしの場合は終了
            if (!this.BaseGetDayDateTimePicker.Checked)
            {
                return;
            }

            var isNew = !(this.TestCar?.データID > 0);
            var isMask = !this.MaskingCheckBox.Checked;

            // 閲覧中は終了
            if (isMask && !isNew)
            {
                return;
            }

            // 過去日が入力された場合
            if (this.BaseGetDayDateTimePicker.Value != null && ((TimeSpan)(DateTime.Now - (DateTime)this.BaseGetDayDateTimePicker.Value)).TotalDays >= 1)
            {
                // プロパティ変更
                this.BaseGetDayDateTimePicker.BackColor = Const.ErrorBackColor;

                if (Messenger.Confirm(Resources.KKM02016) == DialogResult.No)
                {
                    this.BaseGetDayDateTimePicker.Value = this.TestCar.正式取得日;

                    // プロパティ初期化
                    this.BaseGetDayDateTimePicker.BackColor = Const.DefaultBackColor;
                    this.BaseGetDayDateTimePicker.Checked = false;

                    e.Cancel = true;

                    return;
                }

                this.TestCar.正式取得日 = this.BaseGetDayDateTimePicker.SelectedDate;
            }

            // プロパティ初期化
            this.BaseGetDayDateTimePicker.BackColor = Const.DefaultBackColor;
            this.BaseGetDayDateTimePicker.Checked = false;
            
            // 履歴NO1が表示されている場合
            if (this.Page.CurrentPage == 1)
            {
                // フォーム値のセット
                this.HistoryCompletionDayDateTimePicker.Value = this.BaseGetDayDateTimePicker.Value;

                // 履歴が複数ない場合 ※データのリアルタイム更新なし
                if (this.TestCarHistoryList.Count <= 1)
                {
                    return;
                }
            }

            // 履歴NO1の完成日に正式取得日をセット
            this.TestCarHistoryList.FirstOrDefault(x => x.履歴NO == 1).完成日 = this.BaseGetDayDateTimePicker.SelectedDate;

            var model = BeanUtil.CreateAs<ControlSheetTestCarHistoryGetOutModel, ControlSheetTestCarHistoryPutInModel>
                (this.TestCarHistoryList.FirstOrDefault(x => x.履歴NO == 1));

            // データをリアルタイム更新
            this.PutHistoryData(new List<ControlSheetTestCarHistoryPutInModel> { model });
        }
        #endregion

        #region 管理責任部署のマウスクリック
        /// <summary>
        /// 管理責任部署のマウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryControlSectionGroupComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            // 左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var isMask = !this.MaskingCheckBox.Checked;
            var isNew = !(this.TestCar?.データID > 0);

            // 新規作成でない閲覧中は終了
            if (!isNew && isMask)
            {
                return;
            }

            using (var form = new SectionGroupListForm())
            {
                // 担当検索画面がOKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // 管理所在地（所属）の退避
                    this.ControlEstablishment = form.ESTABLISHMENT;

                    // 管理責任部署
                    FormControlUtil.SetComboBoxItem(this.HistoryControlSectionGroupComboBox
                        , new List<ComboBoxDto>() { new ComboBoxDto() { CODE = form.SECTION_GROUP_ID, NAME = form.SECTION_GROUP_CODE } });

                    this.HistoryControlSectionGroupComboBox.SelectedValue = form.SECTION_GROUP_ID;

                    // 工事区分NOの再セット
                    SetHistoryControlSectionGroupComboBox();
                }
            }
        }
        #endregion

        #region 受領部署のマウスクリック
        /// <summary>
        /// 受領部署のマウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryReceiptSectionGroupComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            // 左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var isMask = !this.MaskingCheckBox.Checked;
            var isNew = !(this.TestCar?.データID > 0);

            // 新規作成でない閲覧中は終了
            if (!isNew && isMask)
            {
                return;
            }

            using (var form = new SectionGroupListForm())
            {
                // 担当検索画面がOKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // 部コードの退避
                    this.ReceiptDepartmentCode = form.DEPARTMENT_CODE;

                    // 課コードの退避
                    this.ReceiptSectionCode = form.SECTION_CODE;

                    // 受領部署
                    FormControlUtil.SetComboBoxItem(this.HistoryReceiptSectionGroupComboBox
                    , new List<ComboBoxDto>() { new ComboBoxDto() { CODE = form.SECTION_GROUP_ID, NAME = form.SECTION_GROUP_CODE } });

                    this.HistoryReceiptSectionGroupComboBox.SelectedValue = form.SECTION_GROUP_ID;
                }
            }
        }
        #endregion

        #region 受領者のマウスクリック
        /// <summary>
        /// 受領者のマウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryReceiptUserComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            // 左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var isMask = !this.MaskingCheckBox.Checked;
            var isNew = !(this.TestCar?.データID > 0);

            // 新規作成でない閲覧中は終了
            if (!isNew && isMask)
            {
                return;
            }

            // 受領部署未選択時は終了
            if (string.IsNullOrWhiteSpace(this.HistoryReceiptSectionGroupComboBox.Text))
            {
                return;
            }

            using (var form = new UserListForm
            {
                DepartmentCode = this.ReceiptDepartmentCode ?? this.TestCar.受領部署_DEPARTMENT_CODE,
                SectionCode = this.ReceiptSectionCode ?? this.TestCar.受領部署_SECTION_CODE,
                SectionGroupCode = this.HistoryReceiptSectionGroupComboBox.Text,
                UserAuthority = this.UserAuthority
            })
            {
                // ユーザー検索画面がOKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // バインドON
                    this.IsBind = true;

                    // 受領者
                    FormControlUtil.SetComboBoxItem(this.HistoryReceiptUserComboBox
                    , new List<ComboBoxDto>() { new ComboBoxDto() { ID = form.User.PERSONEL_ID, CODE = form.User.PERSONEL_ID, NAME = form.User.NAME } });

                    // バインドOFF
                    this.IsBind = false;

                    this.HistoryReceiptUserComboBox.SelectedValue = form.User.PERSONEL_ID;
                }
            }
        }
        #endregion

        #region 前送りボタンクリック
        /// <summary>
        /// 前送りボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeHistoryButton_Click(object sender, EventArgs e)
        {
            var moveno = this.Page.CurrentPage - 1;

            // 遷移なしの場合は終了
            if (moveno < 1) return;

            this.MoveHistoryPage(moveno);
        }
        #endregion

        #region 後送りボタンクリック
        /// <summary>
        /// 後送りボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfterHistoryButton_Click(object sender, EventArgs e)
        {
            var moveno = this.Page.CurrentPage + 1;

            // 遷移なしの場合は終了
            if (moveno > this.Page.PageCount) return;

            this.MoveHistoryPage(moveno);
        }
        #endregion

        #region リサイクル券済ボタンクリック
        /// <summary>
        /// リサイクル券済ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRecycleOKButton_Click(object sender, EventArgs e)
        {
            this.BaseMemoTextBox.Text = string.Concat(
                this.BaseMemoTextBox.Text,
                string.IsNullOrWhiteSpace(this.BaseMemoTextBox.Text) ? string.Empty : "\r\n",
                string.Format(AddRisaicleChikectString, DateTime.Now.ToString("yy/MM/dd")));
        }
        #endregion

        #region C/#送付済ボタンクリック
        /// <summary>
        /// C/#送付済ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCHashSendingOKButton_Click(object sender, EventArgs e)
        {
            this.BaseMemoTextBox.Text = string.Concat(
                this.BaseMemoTextBox.Text,
                string.IsNullOrWhiteSpace(this.BaseMemoTextBox.Text) ? string.Empty : "\r\n",
                string.Format(AddCPoundSendString, DateTime.Now.ToString("yy/MM/dd")));
        }
        #endregion

        #region 廃却済ボタンクリック
        /// <summary>
        /// 廃却済ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDisposalDateMemoButton_Click(object sender, EventArgs e)
        {
            this.BaseMemoTextBox.Text = string.Concat(
                this.BaseMemoTextBox.Text,
                string.IsNullOrWhiteSpace(this.BaseMemoTextBox.Text) ? string.Empty : "\r\n",
                string.Format(AddControlSheetSendString, DateTime.Now.ToString("yyyy/M/dd")));
        }
        #endregion

        #region 登録(クリア)ボタンクリック
        /// <summary>
        /// 登録(クリア)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryClearButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //管理表タイプ取得
                if (this.HistoryControlSheetIssueOrComboBox.SelectedValue.ToString() == "済" && isAdministration == "未")
                {
                    GetPrintType();
                    if (string.IsNullOrEmpty(this.adaministrationText))
                    {
                        Messenger.Info(Resources.KKM01023); // 管理票の種類を選択してください
                        return;
                    }
                }

                // 入力項目のチェック
                if (!this.IsEntryDataCheck()) return;

                // フォーム値の取得
                this.GetFormData();

                var res = this.PostData();

                bool historyInsert = true;
                if (res.Status.Equals(Const.StatusSuccess))
                {
                    if(this.TestCar.管理票発行有無 == "済" && isAdministration == "未")
                    {
                        historyInsert = InsertHistory(res.Results.FirstOrDefault());
                    }
                }

                if (res.Status.Equals(Const.StatusSuccess) && historyInsert)
                {
                    var message = string.Concat(Resources.KKM00002, "\r\n管理票NO：", res.Results.FirstOrDefault().管理票NO);

                    Messenger.Info(message); // 登録完了

                    // データ初期化
                    this.TestCar = new TestCarCommonModel();

                    // フォーム値セット
                    this.SetFormData();

                    // 親画面リロード
                    this.Reload();
                }
            });
        }
        #endregion

        #region 登録(コピー)ボタンクリック
        /// <summary>
        /// 登録(コピー)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryCopyButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //管理表タイプ取得
                if (this.HistoryControlSheetIssueOrComboBox.SelectedValue.ToString() == "済" && isAdministration == "未")
                {
                    GetPrintType();
                    if (string.IsNullOrEmpty(this.adaministrationText))
                    {
                        Messenger.Info(Resources.KKM01023); // 管理票の種類を選択してください
                        return;
                    }
                }

                // 入力項目のチェック
                if (!this.IsEntryDataCheck()) return;

                // フォーム値の取得
                this.GetFormData();

                var res = this.PostData();

                bool historyInsert = true;
                if (res.Status.Equals(Const.StatusSuccess))
                {
                    if (this.TestCar.管理票発行有無 == "済" && isAdministration == "未")
                    {
                        historyInsert = InsertHistory(res.Results.FirstOrDefault());
                    }
                }

                if (res.Status.Equals(Const.StatusSuccess) && historyInsert)
                {
                    this.TestCar = res.Results.First();

                    //新規作成なので初期化（クリアボタンの初期化に合わせる）
                    this.TestCar.データID = 0;

                    Messenger.Info(string.Concat(Resources.KKM00002, "\r\n管理票NO：", this.TestCar.管理票NO)); // 登録完了

                    // フォーム値セット
                    this.SetFormData();

                    // データ調整
                    this.TestCar.車体番号 = "";

                    // フォーム値調整
                    this.HistoryVehicleNoTextBox.Text = this.TestCar.車体番号;

                    // 親画面リロード
                    this.Reload();
                }
            });
        }
        #endregion

        #region 更新ボタンクリック
        /// <summary>
        /// 更新ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, EventArgs e)
        {
            // 必須データチェック
            if (!(this.TestCar?.データID > 0) ||
                this.TestCar?.管理票NO == null ||
                this.TestCar?.履歴NO == null) return;

            FormControlUtil.FormWait(this, () =>
            {
                //管理表タイプ取得
                if (this.HistoryControlSheetIssueOrComboBox.SelectedValue.ToString() == "済" && isAdministration == "未")
                {
                    GetPrintType();
                    if (string.IsNullOrEmpty(this.adaministrationText))
                    {
                        Messenger.Info(Resources.KKM01023); // 管理票の種類を選択してください
                        return;
                    }
                }
                // 入力項目のチェック
                if (!this.IsEntryDataCheck()) return;

                // 更新確認
                if (Messenger.Confirm(Resources.TCM00007) != DialogResult.Yes)
                {
                    return;
                }

                // フォーム値の取得
                this.GetFormData();

                var res = this.PutData();

                bool historyInsert = true;
                if (res.Status.Equals(Const.StatusSuccess))
                {
                    if (this.TestCar.管理票発行有無 == "済" && isAdministration == "未")
                    {
                        historyInsert = InsertHistory(this.TestCar);
                    }
                }

                if (res.Status.Equals(Const.StatusSuccess) && historyInsert)
                {
                    // 試験車情報
                    this.TestCar = this.GetData(new TestCarCommonSearchModel() { データID = this.TestCar.データID })?.FirstOrDefault();

                    // フォーム値セット
                    this.SetFormData();

                    // 試験車情報（初回）の更新
                    this.TestCarDefault = this.TestCar.Clone();

                    Messenger.Info(Resources.TCM00008); // 更新完了

                    // 親画面リロード
                    this.Reload();

                    // 駐車場移動モードの場合
                    if (this.IsMoveMode)
                    {
                        // 本画面を閉じる
                        this.Close();
                    }
                }
            });
        }
        #endregion

        #region 発行ボタンクリック
        /// <summary>
        /// 発行ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IssueButton_Click(object sender, EventArgs e)
        {
            // 必須データチェック
            if (!(this.TestCar?.データID > 0) ||
                this.TestCar?.管理票NO == null ||
                this.TestCar?.履歴NO == null) return;

            FormControlUtil.FormWait(this, () =>
            {
                // 入力項目のチェック
                if (!this.IsEntryDataCheck(1)) return;

                // フォーム値の取得
                this.GetFormData();

                // 管理票発行画面
                using (var form = new ControlSheetIssueEntryForm() { TestCar = this.TestCar, UserAuthority = this.UserAuthority })
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        // データの取得
                        var cond = new TestCarCommonSearchModel() { データID = this.TestCar.データID };
                        this.TestCar = GetData(cond)?.FirstOrDefault();
                        this.isAdministration = this.TestCar?.管理票発行有無 ?? "未";

                        // 試験車情報（初回）の更新
                        this.TestCarDefault = this.TestCar.Clone();

                        // フォーム値（試験車履歴情報）のセット
                        SetHistoryFormData();
                    }
                }
            });
        }
        #endregion

        #region 新規履歴作成ボタンクリック
        /// <summary>
        /// 新規履歴作成ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetHistoryEntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 入力項目のチェック
                if (!this.IsEntryDataCheck(2)) return;

                // 新規履歴作成画面
                using (var form = new ControlSheetHistoryEntryForm() { TestCar = this.TestCar, UserAuthority = this.UserAuthority })
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        // データの取得
                        var cond = new TestCarCommonSearchModel() { データID = this.TestCar.データID };
                        this.TestCar = GetData(cond)?.FirstOrDefault();

                        // 試験車情報（初回）の更新
                        this.TestCarDefault = this.TestCar.Clone();

                        // フォーム値（試験車履歴情報）のセット
                        this.SetHistoryFormData();

                        // 履歴NOラベルのセット
                        this.SetHistoryNoButton();

                        // 最新ページへ移動
                        this.MoveHistoryPage(this.TestCar.履歴NO);
                    }
                }
            });
        }
        #endregion

        #region 試験車使用履歴ボタンクリック
        /// <summary>
        /// 試験車使用履歴ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarUseHistoryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //Update Start 2021/07/26 矢作
                // 試験車使用履歴画面
                // new FormUtil(new TestCarHistoryForm { TestCar = this.TestCar, UserAuthority = this.UserAuthority }).SingleFormShow(this);
                new FormUtil(new TestCarHistoryForm { TestCar = this.TestCar, UserAuthority = this.UserAuthority, CurrentPage = this.Page.CurrentPage }).SingleFormShow(this);
                //Update End 2021/07/26 矢作
            });
        }
        #endregion

        #region ラベル印刷ボタンクリック
        /// <summary>
        /// ラベル印刷ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlLabelIssueButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //ラベル印刷
                FormControlUtil.FormWait(this, () => this.PrintLabel());
            });
        }
        #endregion

        #region 受領票印刷ボタンクリック
        /// <summary>
        /// 受領票印刷ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiptSlipIssueButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //受領票EXCEL出力
                this.SaveXls();
            });
        }
        #endregion

        #region フォームクローズ前
        /// <summary>
        /// フォームクローズ前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetIssueForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var isNew = !(this.TestCar?.データID > 0);
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isAcctive = this.Page.CurrentPage == this.Page.PageCount;
            var isMask = !this.MaskingCheckBox.Checked;

            // 新規作成の場合は終了
            if (isNew) return;

            var ctllist = new List<Control>();

            FormControlUtil.GetInputControls(this.ListFormMainPanel, ctllist, true);

            // フォーム値の取得
            if (isAcctive) this.GetFormData();

            foreach (var ctl in ctllist)
            {
                if (ctl.Name == string.Empty) continue;

                var isChange = !isMask ? this.CompareObjectValue(this.TestCar, this.TestCarDefault, ctl) : false;

                // 変更がある場合
                if (isChange && isUpdate)
                {
                    // 編集中の確認
                    if (Messenger.Confirm(Resources.TCM00009) != DialogResult.Yes)
                    {
                        //一時確保している駐車場があれば開放する。
                        this.ClearParkingInfo();

                        return;
                    }

                    // マスク解除
                    if (isMask) this.MaskingCheckBox.Checked = true;

                    // 最新履歴NOへ遷移
                    this.MoveHistoryPage(this.Page.PageCount);

                    // 入力項目のチェック
                    if (!this.IsEntryDataCheck())
                    {
                        e.Cancel = true;

                        return;
                    }

                    // フォーム値の再取得
                    this.GetFormData();

                    var res = this.PutData();

                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        Messenger.Info(Resources.TCM00008); // 更新完了
                        break;

                    }
                }
            }
        }
        #endregion

        #endregion

        #region トランザクションデータの操作

        #region データのチェック
        /// <summary>
        /// データのチェック
        /// </summary>
        /// <returns>チェック可否</returns>
        private bool IsEntryDataCheck(int type = 0)
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            // 管理票発行・新規履歴作成
            if (type > 0)
            {
                // 廃却申請チェック
                if (this.TestCar?.データID > 0)
                {
                    var baseinfo = GetBaseData().FirstOrDefault();

                    if (baseinfo?.研実管理廃却申請受理日 != null)
                    {
                        Messenger.Warn(Resources.TCM03002);

                        this.BaseDisposalKenjitsuReceiptDayDateTimePicker.BackColor = Const.ErrorBackColor;

                        return false;
                    }
                }

                // 承認処理中チェック
                if (this.TestCar?.データID > 0)
                {
                    // 試験車使用履歴情報
                    this.TestCarUseHistoryList = this.GetUseHistoryData(new TestCarUseHistorySearchModel { データID = (int)this.TestCar.データID });

                    var useinfo = this.TestCarUseHistoryList?.Where(x => x.履歴NO == (int)this.TestCar?.履歴NO && x.STEPNO > 0)?.ToList();

                    if (useinfo.Any() == true)
                    {
                        Messenger.Warn(Resources.TCM03021);

                        return false;
                    }
                }
            }

            // 新規履歴作成のみ
            if (type == 2)
            {
                // 管理責任部署が選択されている場合
                if (this.HistoryControlSectionGroupComboBox.SelectedValue != null)
                {
                    var row = GetSectionGrouptList()?.FirstOrDefault();

                    if (row?.SECTION_ID == "116" || row?.SECTION_ID == "14")
                    {
                        if (string.IsNullOrWhiteSpace(this.HistoryFixedAssetNoTextBox.Text) &&
                            string.IsNullOrWhiteSpace(this.HistoryLicensePlateNumberTextBox.Text))
                        {
                            Messenger.Warn(Resources.TCM03026);

                            this.HistoryFixedAssetNoTextBox.BackColor = Const.ErrorBackColor;
                            this.HistoryLicensePlateNumberTextBox.BackColor = Const.ErrorBackColor;

                            return false;
                        }
                    }
                }

                // 新規履歴作成では以降のチェックは行わない
                return true;
            }

            // 車体番号重複チェック
            if (!string.IsNullOrWhiteSpace(this.HistoryVehicleNoTextBox.Text))
            {
                var cond = new TestCarCommonSearchModel() { 車体番号 = this.HistoryVehicleNoTextBox.Text };
                var list = GetData(cond);

                // 同一車体番号がある場合は更新不可
                var listinfo = list.Where(x => x.管理票NO != this.BaseControlNoTextBox.Text && x.車体番号 == this.HistoryVehicleNoTextBox.Text).FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(listinfo?.管理票NO))
                {
                    Messenger.Warn(string.Format(Resources.TCM03003, listinfo?.管理票NO));

                    this.HistoryVehicleNoTextBox.BackColor = Const.ErrorBackColor;

                    return false;
                }
            }

            // 管理票発行
            if (type == 1)
            {
                // 完成日チェック
                if (!string.IsNullOrWhiteSpace(this.HistoryFixedAssetNoTextBox.Text) &&
                this.HistoryKenmeiPlateNumberTextBox.Text?.StartsWith(ControlSheetIssueCode) == true)
                {
                    // 未入力はエラー
                    if (this.HistoryCompletionDayDateTimePicker.SelectedDate == null)
                    {
                        Messenger.Warn(Resources.TCM03004);

                        this.HistoryCompletionDayDateTimePicker.BackColor = Const.ErrorBackColor;

                        return false;
                    }

                    var date = (DateTime)this.HistoryCompletionDayDateTimePicker.SelectedDate;

                    // エラーなしの場合は処分予定月を強制セット
                    this.FixedDisposalPlanDayDateTimePicker.Value
                        = new DateTime(date.AddYears(1).Year, date.Month, 1);
                }
            }

            // 発行年月日チェック
            map[this.HistoryControlSheetIssueDayDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                // 管理票発行済みでない場合は未チェック
                if (this.HistoryControlSheetIssueOrComboBox?.SelectedValue.ToString() != "済")
                    return errMsg;

                // 未入力不可
                if (this.HistoryControlSheetIssueDayDateTimePicker.SelectedDate == null)
                {
                    //エラーメッセージ
                    errMsg = string.Format(Resources.KKM00001, name);
                }

                return errMsg;
            };

            // 受領日チェック
            map[this.HistoryReceiptDayDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                // 管理票発行済みでない場合は未チェック
                if (this.HistoryControlSheetIssueOrComboBox?.SelectedValue.ToString() != "済")
                    return errMsg;

                // 未入力不可
                if (this.HistoryReceiptDayDateTimePicker.SelectedDate == null)
                {
                    //エラーメッセージ
                    errMsg = string.Format(Resources.KKM00001, name);
                }

                return errMsg;
            };

            // 工事区分NO
            map[this.HistoryConstructionNoComboBox] = (c, name) =>
            {
                var errMsg = "";

                // 未入力・未選択の場合は未チェック
                if (string.IsNullOrWhiteSpace(this.HistoryConstructionNoComboBox?.Text))
                    return errMsg;

                var text = this.HistoryConstructionNoComboBox?.SelectedIndex > 0
                    ? this.HistoryConstructionNoComboBox?.SelectedValue?.ToString()
                    : this.HistoryConstructionNoComboBox?.Text;

                // バイト数チェック
                if (StringUtil.SjisByteLength(text) > 10)
                {
                    //エラーメッセージ
                    errMsg = string.Format(Resources.KKM00027, name);
                }

                return errMsg;
            };

            // 車検期限
            map[this.HistoryInspectionDayDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                // 登録ナンバーが入力されていて車検期限が未入力はNG
                if (string.IsNullOrWhiteSpace(this.HistoryLicensePlateNumberTextBox.Text) == false && this.HistoryInspectionDayDateTimePicker.SelectedDate == null)
                {
                    //エラーメッセージ
                    errMsg = string.Format(Resources.TCM03028, name);
                }

                return errMsg;

            };

            // 研実管理廃却申請受理日
            map[this.BaseDisposalKenjitsuReceiptDayDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                if (this.TestCarUseHistoryList == null || this.TestCarUseHistoryList.Count <= 0)
                    return errMsg;

                var maxUseHistory = this.TestCarUseHistoryList.OrderByDescending(x => x.SEQNO).FirstOrDefault();

                // 廃却申請承認済みで研実管理廃却申請受理日が未入力はNG
                if (maxUseHistory.承認要件コード == ScrappingApplicationCode && maxUseHistory.STEPNO == 0 && this.BaseDisposalKenjitsuReceiptDayDateTimePicker.SelectedDate == null)
                {
                    //エラーメッセージ
                    errMsg = string.Format(Resources.TCM03002 + Resources.KKM00001, name);
                }

                return errMsg;

            };

            // 入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);

            if (msg != "")
            {
                Messenger.Warn(msg);

                return false;
            }

            return true;
        }
        #endregion

        #region 管理表タイプ取得
        /// <summary>
        /// 管理表タイプ取得
        /// </summary>
        private void GetPrintType()
        {
            //出力する受領票を選択
            ReceiptForm.CarKind receiptKind;
            using (var rec = new ReceiptForm
            { data = this.TestCarDefault, printType = "administration" })
            {
                if (rec.ShowDialog() != DialogResult.OK)
                {
                    this.adaministrationText = "";
                    return;
                }

                receiptKind = rec.ReceiptKind;
            }

            switch (receiptKind)
            {
                case ReceiptForm.CarKind.TestCarG:
                case ReceiptForm.CarKind.TestCarT:
                    this.adaministrationText = "試作部から受領";
                    break;
                case ReceiptForm.CarKind.FixedAssetG:
                case ReceiptForm.CarKind.OtherT:
                    this.adaministrationText = "受領";
                    break;
                default:
                    return;
            }
        }
        #endregion

        #region 作業履歴の登録
        /// <summary>
        /// 作業履歴の登録
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool InsertHistory(TestCarCommonModel item)
        {
            var manage = new List<SectionGroupModel>();
            if (!string.IsNullOrEmpty(this.TestCar.管理責任部署))
            {
                manage = GetSectionGroupData(this.TestCar.管理責任部署);
            }
            else
            {
                manage.Add(new SectionGroupModel() { SECTION_CODE = null, DEPARTMENT_CODE = null });
            }

            //受領部署取得
            var receipt = new List<SectionGroupModel>();
            if (!string.IsNullOrEmpty(this.TestCar.受領部署))
            {
                receipt = GetSectionGroupData(this.TestCar.受領部署);
            }
            else
            {
                receipt.Add(new SectionGroupModel() { SECTION_CODE = null, DEPARTMENT_CODE = null });
            }

            //Append Start 2021/05/07 杉浦 管理表発行を行った際に表示される日付が、旧版仕様と異なっている
            //処理日取得
            var date = new DateTime();
            if (item.受領日.HasValue)
            {
                date = item.受領日.Value;
            }
            else
            {
                date = DateTime.Today;
            }
            //Append End 2021/05/07 杉浦 管理表発行を行った際に表示される日付が、旧版仕様と異なっている

            var list = new List<ApplicationApprovalCarModel>();
            list.Add(new ApplicationApprovalCarModel
            {
                //データID
                データID = item.データID,

                //履歴NO
                履歴NO = item.履歴NO,

                //SEQNO
                SEQNO = 1,

                //承認要件コード
                承認要件コード = "A",

                //STEPNO
                STEPNO = 0,

                //承認状況
                承認状況 = "済",

                //承認者レベル
                承認者レベル = null,

                //管理部署承認
                管理部署承認 = null,

                //処理日
                //Update Start 2021/05/07 杉浦 管理表発行を行った際に表示される日付が、旧版仕様と異なっている
                //処理日 = DateTime.Today,
                処理日 = date,
                //Update End 2021/05/07 杉浦 管理表発行を行った際に表示される日付が、旧版仕様と異なっている

                //管理責任課名
                SECTION_CODE = manage[0].SECTION_CODE,

                //管理責任部署名
                SECTION_GROUP_CODE = manage[0].DEPARTMENT_CODE,

                //使用課名
                使用課名 = receipt[0].SECTION_CODE,

                //使用部署名
                使用部署名 = receipt[0].DEPARTMENT_CODE,

                //管理所在地
                ESTABLISHMENT = item.ESTABLISHMENT,

                //試験内容
                試験内容 = this.adaministrationText,

                //工事区分NO
                工事区分NO = item.工事区分NO,

                //実走行距離
                実走行距離 = item.受領時走行距離,

                //編集者
                編集者 = SessionDto.UserId,

                //移管先部署ID
                移管先部署ID = null,

                //駐車場番号
                駐車場番号 = null,

                //チェック結果
                CHECK_RESULT = CheckResultType.Ok,

                //登録種別
                ADD_TYPE = AddType.History

            });

            // 試験車使用履歴登録
            if (this.InsertTestCarUseHistory(list) == false)
            {
                return false;
            }

            return true;
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
        #endregion

        #region 部署データ取得(IDあり)
        /// <summary>
        /// 部署データ取得(IDあり)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<SectionGroupModel> GetSectionGroupData(string id)
        {
            var cond = new SectionGroupSearchModel();
            cond.SECTION_GROUP_ID = id;
            var res = HttpUtil.GetResponse<SectionGroupSearchModel, SectionGroupModel>(ControllerType.SectionGroup, cond);

            return (res.Results).ToList();
        }
        #endregion

        //Append Start 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
        #region 試験車衝突車管理部署取得
        /// <summary>
        /// 試験車衝突車管理部署取得
        /// </summary>
        /// <returns></returns>
        private List<TestCarCollisionCarManagementDepartmentModel> GetTestCarCollisionSectionGroup()
        {
            var cond = new TestCarCollisionCarManagementDepartmentSearchModel { IS_SECTION_GROUP = true };

            //APIで取得
            var res = HttpUtil.GetResponse<TestCarCollisionCarManagementDepartmentSearchModel, TestCarCollisionCarManagementDepartmentModel>(ControllerType.TestCarCollisionCarManagementDepartment, cond);

            //レスポンスが取得できたかどうか
            var list = new List<TestCarCollisionCarManagementDepartmentModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion
        //Append End 2021/07/06 杉浦 要望対応_衝突試験済のチェックボックスを追加したい

        #region

        #endregion

        #region データ取得処理
        /// <summary>
        /// データ（試験車情報）取得処理
        /// </summary>
        private List<TestCarCommonModel> GetData(TestCarCommonSearchModel cond)
        {
            //Get実行
            var res = HttpUtil.GetResponse<TestCarCommonSearchModel, TestCarCommonModel>
                (ControllerType.ControlSheetTestCar, cond);

            return (res.Results).ToList();
        }
        /// <summary>
        /// データ（試験車基本情報）取得処理
        /// </summary>
        private List<ControlSheetTestCarBasicGetOutModel> GetBaseData()
        {
            var cond = new ControlSheetTestCarBasicGetInModel
            {
                データID = (int)this.TestCar?.データID
            };

            //Get実行
            var res = HttpUtil.GetResponse<ControlSheetTestCarBasicGetInModel, ControlSheetTestCarBasicGetOutModel>
                (ControllerType.ControlSheetTestCarBasic, cond);

            return (res.Results).ToList();
        }
        /// <summary>
        /// データ（試験車履歴情報）取得処理
        /// </summary>
        private List<ControlSheetTestCarHistoryGetOutModel> GetHistoryData()
        {
            var cond = new ControlSheetTestCarHistoryGetInModel
            {
                データID = (int)this.TestCar?.データID
            };

            //Get実行
            var res = HttpUtil.GetResponse<ControlSheetTestCarHistoryGetInModel, ControlSheetTestCarHistoryGetOutModel>
                (ControllerType.ControlSheetTestCarHistory, cond);

            return (res.Results).ToList();
        }
        /// <summary>
        /// データ（使用履歴情報）取得処理
        /// </summary>
        private List<TestCarUseHistoryModel> GetUseHistoryData(TestCarUseHistorySearchModel cond)
        {
            //Get実行
            var res = HttpUtil.GetResponse<TestCarUseHistorySearchModel, TestCarUseHistoryModel>
                (ControllerType.TestCarUseHistory, cond);

            return (res.Results).ToList();
        }
        #endregion

        #region データ登録処理
        /// <summary>
        /// データ登録処理
        /// </summary>
        private ResponseDto<TestCarCommonModel> PostData()
        {
            return HttpUtil.PostResponse<TestCarCommonModel>(ControllerType.ControlSheetTestCar, this.TestCar);
        }
        #endregion

        #region データ更新処理
        /// <summary>
        /// データ更新処理
        /// </summary>
        private ResponseDto<TestCarCommonModel> PutData()
        {
            if (this.BaseCarCarryingOutDayDateTimePicker.Value == null)
            {
                //車両搬出年月日が入力されていない
                if (this.CompareObjectValue(this.TestCar, this.TestCarDefault, this.BaseParkingNoComboBox) == true)
                {
                    //駐車場番号の変更あり(空に変更されることはない)

                    //駐車場選択情報を削除
                    this.DeleteParkingInfo();

                    if (this.ParkingInfo.SECTION_NO != null)
                    {
                        //区画を選択（エリアを選択した場合は駐車場選択情報の更新はなし）

                        //駐車場選択情報を登録
                        this.AddParkingInfo();

                    }
                }
            }
            else
            {
                //車両搬出年月日が入力されている
                //一時確保している駐車場があれば開放する。
                this.ClearParkingInfo();

                //駐車場選択情報を削除
                this.DeleteParkingInfo();

                //駐車場番号の更新情報を初期化
                this.TestCar.駐車場番号 = "";

                //駐車場番号の更新情報を初期化
                this.BaseParkingNoComboBox.Items.Clear();
            }

            //試験車情報を更新
            var res = HttpUtil.PutResponse<TestCarCommonModel>(ControllerType.ControlSheetTestCar, this.TestCar);

            if (!res.Status.Equals(Const.StatusSuccess))
            {
                //登録失敗した場合、駐車場選択情報を削除
                this.DeleteParkingInfo();
            }

            //一時確保している駐車場情報を開放する。
            this.ParkingInfo = new ParkingModel();

            return res;
        }

        /// <summary>
        /// データ更新処理（ラベル印刷）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private ResponseDto<TestCarCommonModel> PutLabelData(List<TestCarCommonModel> list)
        {
            return HttpUtil.PutResponse<TestCarCommonModel>(ControllerType.ControlSheetLabelPrint, list);
        }

        /// <summary>
        /// データ更新処理（試験車履歴情報）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private ResponseDto<ControlSheetTestCarHistoryPutInModel> PutHistoryData(List<ControlSheetTestCarHistoryPutInModel> list)
        {
            return HttpUtil.PutResponse(ControllerType.ControlSheetTestCarHistory, list);
        }
        #endregion

        #endregion

        #region マスタデータの取得・検索

        #region 車系検索
        /// <summary>
        /// 車系検索
        /// </summary>
        private List<CommonMasterModel> GetCarGroupList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.CarGroupInfo, null)?.Results?.ToList();
        }
        #endregion

        #region グレード検索
        /// <summary>
        /// グレード検索
        /// </summary>
        private List<CommonMasterModel> GetGradeList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.GradeInfo, null)?.Results?.ToList();
        }
        #endregion

        #region エンジン型式検索
        /// <summary>
        /// エンジン型式検索
        /// </summary>
        private List<CommonMasterModel> GetEngineModelList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.EngineModelInfo, null)?.Results?.ToList();
        }
        #endregion

        #region 車型検索
        /// <summary>
        /// 車型検索
        /// </summary>
        private List<CommonMasterModel> GetCarTypeList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.CarModelInfo, null)?.Results?.ToList();
        }
        #endregion

        #region 駐車場番号検索
        /// <summary>
        /// 駐車場番号検索
        /// </summary>
        private List<CommonMasterModel> GetParkingNoList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.ParkingNumberInfo, null)?.Results?.ToList();
        }
        #endregion

        #region 開発符号検索
        /// <summary>
        /// 開発符号検索
        /// </summary>
        private List<CommonMasterModel> GetGeneralCodeList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.GeneralCodeInfo, null)?.Results?.ToList();
        }
        #endregion

        #region 試作時期検索
        /// <summary>
        /// 試作時期検索
        /// </summary>
        private List<CommonMasterModel> GetPrototypeSeasonList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.PrototypeSeasonInfo, null)?.Results?.ToList();
        }
        #endregion

        #region メーカー名検索
        /// <summary>
        /// メーカー名検索
        /// </summary>
        private List<CommonMasterModel> GetMakerNameList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.MakerNameInfo, null)?.Results?.ToList();
        }
        #endregion

        #region 仕向地検索
        /// <summary>
        /// 仕向地検索
        /// </summary>
        private List<CommonMasterModel> GetSimuketiList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.DestinationInfo, null)?.Results?.ToList();
        }
        #endregion

        #region 排気量検索
        /// <summary>
        /// 排気量検索
        /// </summary>
        private List<CommonMasterModel> GetDisplacementList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.DisplacementInfo, null)?.Results?.ToList();
        }
        #endregion

        #region トランスミッション検索
        /// <summary>
        /// トランスミッション検索
        /// </summary>
        private List<CommonMasterModel> GetTMList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.TransmissionInfo, null)?.Results?.ToList();
        }
        #endregion

        #region 駆動方式検索
        /// <summary>
        /// 駆動方式検索
        /// </summary>
        private List<CommonMasterModel> GetDriveMethodList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.DriveMethodInfo, null)?.Results?.ToList();
        }
        #endregion

        #region 車体色検索
        /// <summary>
        /// 車体色検索
        /// </summary>
        private List<CommonMasterModel> GetCarBodyColorList()
        {
            //Get実行
            return HttpUtil.GetResponse<CommonMasterModel, CommonMasterModel>
                (ControllerType.CarBodyColorInfo, null)?.Results?.ToList();
        }
        #endregion

        #region 受領先検索
        /// <summary>
        /// 受領先検索
        /// </summary>
        private List<RecipientGetOutModel> GetRecipientList()
        {
            //Get実行
            return HttpUtil.GetResponse<RecipientGetInModel, RecipientGetOutModel>
                (ControllerType.Recipient, null)?.Results?.ToList();
        }
        #endregion

        #region 担当検索
        private List<SectionGroupModel> GetSectionGrouptList()
        {
            // パラメータ設定
            var itemCond = new SectionGroupSearchModel
            {
                // 担当ID
                SECTION_GROUP_ID = (string)this.HistoryControlSectionGroupComboBox.SelectedValue?.ToString()
            };

            // Get実行
            return HttpUtil.GetResponse<SectionGroupSearchModel, SectionGroupModel>
                (ControllerType.SectionGroup, itemCond)?.Results?.ToList();
        }
        #endregion

        #endregion

        #region ローカルメソッド

        #region コントロールにマスクを設定・解除する
        /// <summary>
        /// 子コントロールにマスクを設定・解除する
        /// </summary>
        /// <param name="ctl">Control</param>
        /// <param name="flg">Boolean(true:設定, false:解除)</param>
        /// <param name="list">除外リスト</param>
        /// <returns></returns>
        private void SetMaskingControls(Control ctl, Boolean flg, List<Control> list = null)
        {
            var ctllist = new List<Control>();

            FormControlUtil.GetInputControls(ctl, ctllist, true);

            foreach (var c in ctllist)
            {
                if (list != null && list.Exists(no => no.Name == c.Name)) continue;

                this.SetMaskingControl(c, flg);
            }
        }
        /// <summary>
        /// コントロールにマスクを設定・解除する
        /// </summary>
        /// <param name="ctl">Control</param>
        /// <param name="flg">Boolean(true:設定, false:解除)</param>
        /// <returns></returns>
        private void SetMaskingControl(Control ctl, Boolean flg)
        {
            if (ctl is TextBox)
            {
                ((TextBox)ctl).ReadOnly = !flg;
                ((TextBox)ctl).BackColor = !flg ? SystemColors.Control : Color.White;
            }
            else if (ctl is ComboBox)
            {
                if (((ComboBox)ctl).DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    ((ComboBox)ctl).Enabled = flg;
                }
                else
                {
                    ((ComboBox)ctl).FlatStyle = !flg ? FlatStyle.Flat : FlatStyle.Standard;
                    ((ComboBox)ctl).BackColor = !flg ? SystemColors.Control : Color.White;
                }
            }
            else if (ctl is RadioButton)
            {
                ((RadioButton)ctl).Enabled = flg;
            }
            else if (ctl is CheckBox)
            {
                ((CheckBox)ctl).Enabled = flg;
            }
            else if (ctl is DateTimePicker)
            {
                ((DateTimePicker)ctl).BackColor = !flg ? SystemColors.Control : Color.White;
            }
            else if (ctl is Button)
            {
                ((Button)ctl).Enabled = flg;
            }
            else if (ctl is DataGridView)
            {
                ((DataGridView)ctl).DefaultCellStyle.BackColor = !flg ? SystemColors.Control : Color.White;
            }
        }
        #endregion

        #region 工事区分NOコンボボックスのセット
        /// <summary>
        /// 工事区分NOのコンボボックスのセット
        /// </summary>
        private void SetHistoryControlSectionGroupComboBox()
        {
            var estalist = this.TestCar?.ESTABLISHMENT == "g" ? GunmaKoujiNoList : OtherKoujiNoList;
            estalist = this.TestCar?.ESTABLISHMENT == "t" ? TokyoKoujiNoList : estalist;

            var nowcode = this.HistoryConstructionNoComboBox.SelectedIndex > 0
                ? this.HistoryConstructionNoComboBox?.SelectedValue.ToString()
                : this.HistoryConstructionNoComboBox?.Text;

            FormControlUtil.SetComboBoxItem(this.HistoryConstructionNoComboBox, estalist);

            this.HistoryConstructionNoComboBox.Text = nowcode;
        }
        #endregion

        #region 試験車(試験車履歴情報)フォームのプロパティ設定
        /// <summary>
        /// 試験車(試験車履歴情報)フォームのプロパティ設定
        /// </summary>
        /// <param name="flg"></param>
        private void SetHistoryControlProperty(bool flg)
        {
            var isMask = !this.MaskingCheckBox.Checked;

            // 閲覧中は終了
            if (isMask)
            {
                return;
            }

            var ctllist = new List<Control>();

            FormControlUtil.GetInputControls(this.HistoryTableLayoutPanel, ctllist, true);

            var maxhistory = this.TestCar;
            var nowhistory = this.TestCarHistoryList.Where(x => x.履歴NO == this.Page.CurrentPage).FirstOrDefault();

            foreach (var ctl in ctllist)
            {
                if (ctl.Name == string.Empty) continue;

                var isChange = this.CompareObjectValue(maxhistory, nowhistory, ctl);

                if (ctl is TextBox)
                {
                    ctl.BackColor = isChange && !flg ? Color.LightPink : Color.White;
                }
                else if (ctl is MaskedTextBox)
                {
                    ctl.BackColor = isChange && !flg ? Color.LightPink : Color.White;
                }
                else if (ctl is ComboBox)
                {
                    ((ComboBox)ctl).FlatStyle = isChange && !flg && !isMask ? FlatStyle.Flat : FlatStyle.Standard;
                    ctl.BackColor = isChange && !flg ? Color.LightPink : Color.White;
                }
                else if (ctl is CheckBox)
                {
                    ctl.BackColor = isChange && !flg ? Color.LightPink : Color.White;
                }
                else if (ctl is DateTimePicker)
                {
                    ctl.BackColor = isChange && !flg ? Color.LightPink : Color.White;
                }
                else if (ctl is NullableDateTimePicker)
                {
                    ctl.BackColor = isChange && !flg ? Color.LightPink : Color.White;
                }
            }
        }
        #endregion

        #region 履歴NOボタンのプロパティ設定
        /// <summary>
        /// 履歴NOボタンのプロパティ設定
        /// </summary>
        private void SetHistoryNoButtonlProperty()
        {
            var ctllist = new List<Control>();

            FormControlUtil.GetInputControls(this.ListFormMainPanel, ctllist, true);

            foreach (var ctl in ctllist)
            {
                if (ctl.Name == string.Empty) continue;

                if (ctl is Button)
                {
                    if (!ctl.Name.StartsWith(HistoryNoButtonString, StringComparison.OrdinalIgnoreCase)) continue;

                    var button = (Button)ctl;
                    button.Font = Convert.ToInt32(button.Text) == this.Page.CurrentPage
                        ? new Font(button.Font, FontStyle.Bold) : new Font(button.Font, FontStyle.Regular);
                }
            }
        }
        #endregion

        #region 履歴ページング処理
        /// <summary>
        /// 履歴ページング処理
        /// </summary>
        /// <param name="movepage"></param>
        private void MoveHistoryPage(int movepage)
        {
            // 初遷移・履歴追加後のみAPIアクセス
            if (this.TestCarHistoryList.Count() != this.Page.PageCount)
            {
                this.TestCarHistoryList = this.GetHistoryData();
            }

            // 最新履歴からの遷移の場合は入力データの退避
            if (this.Page.CurrentPage == this.Page.PageCount)
            {
                this.GetFormData();
            }

            // 移動先ページのセット
            this.Page.CurrentPage = movepage;

            var isNew = !(this.TestCar?.データID > 0);
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isAcctive = this.Page.CurrentPage == this.Page.PageCount;
            var isMask = !this.MaskingCheckBox.Checked;
            //Append Start 2022/04/21 杉浦 システム管理者で出るはずのない管理票画面での「編集モード」へのボタンが出てしまっている
            var isManagementDepartment = SessionDto.ManagementDepartmentType.Any(x => x == Const.Kenjitu || x == Const.Kanri);
            //Append End 2022/04/21 杉浦 システム管理者で出るはずのない管理票画面での「編集モード」へのボタンが出てしまっている

            // 最新履歴への遷移の場合
            if (isAcctive)
            {
                // フォーム値のセット
                this.SetHistoryFormData();
            }
            // 最新履歴以外への遷移の場合
            else
            {
                // フォーム値のセット
                this.SetHistoryFormViewData();
            }

            // フォームのプロパティ設定
            this.SetHistoryControlProperty(isAcctive);

            // 履歴NOボタンのプロパティ設定
            this.SetHistoryNoButtonlProperty();

            // 編集トグルボタン
            //Update Start 2022/04/21 杉浦 システム管理者で出るはずのない管理票画面での「編集モード」へのボタンが出てしまっている
            //this.MaskingCheckBox.Visible = !isNew && isAcctive && isUpdate;
            this.MaskingCheckBox.Visible = !isNew && isAcctive && isUpdate && isManagementDepartment; //TODO: システム管理者が編集モード出せるのはこれが原因
            //Update End 2022/04/21 杉浦 システム管理者で出るはずのない管理票画面での「編集モード」へのボタンが出てしまっている

            // 更新ボタン
            this.EditButton.Visible = !isNew && isAcctive && !isMask && isUpdate;

            // 管理票発行ボタン
            this.IssueButton.Visible = !isNew && isAcctive && !isMask && isUpdate;

            // 新規履歴作成ボタン
            this.ControlSheetHistoryEntryButton.Visible = !isNew && isAcctive && !isMask && isUpdate;
        }
        #endregion

        #region タグ名の取得
        /// <summary>
        /// タグ名の取得
        /// </summary>
        /// <param name="model1"></param>
        /// <param name="model2"></param>
        /// <param name="name"></param>
        private string GetTagName(Control ctl)
        {
            Func<string, string> getTagValue = s => Regex.Replace(s, @"^.+?\((.+)\)$", "$1");

            Func<object, string[]> split = o =>
            {
                var value = o == null ? string.Empty : o.ToString();

                var list = new List<string>();

                var tag = "";

                foreach (var s in value.Split(';'))
                {
                    tag += s;

                    if (Regex.IsMatch(s, @"([a-z]|\))$") == true)
                    {
                        list.Add(tag);

                        tag = "";
                    }
                    else
                    {
                        tag += ";";
                    }
                }

                return list.ToArray();
            };

            var tagList = split(ctl.Tag);

            var itemName = tagList.FirstOrDefault(x => x.Contains(Const.ItemName) == true);

            return itemName == null ? "" : getTagValue(itemName);
        }
        #endregion

        #region オブジェクト値比較
        /// <summary>
        /// オブジェクト値比較
        /// </summary>
        /// <param name="model1"></param>
        /// <param name="model2"></param>
        /// <param name="ctl"></param>
        private bool CompareObjectValue<T1, T2>(T1 model1, T2 model2, Control ctl)
        {
            var isMask = this.MaskingCheckBox.Checked;

            var name = this.GetTagName(ctl);

            if (string.IsNullOrWhiteSpace(name)) return false;

            Func<Type, string, PropertyInfo> getPropertyInfo = (t, n) => t.GetProperty(n);

            var property1 = getPropertyInfo(model1.GetType(), name);
            var property2 = getPropertyInfo(model2.GetType(), name);

            var test = property1?.GetMethod?.Invoke(model1, new object[0]);

            var val1 = property1?.GetMethod?.Invoke(model1, new object[0])?.ToString()?.Trim() ?? string.Empty;
            var val2 = property2?.GetMethod?.Invoke(model2, new object[0])?.ToString()?.Trim() ?? string.Empty;

            // 日付
            if (val1 != val2 && (ctl is DateTimePicker || ctl is NullableDateTimePicker))
            {
                var format = string.Empty;

                if (ctl is DateTimePicker)
                {
                    format = ((DateTimePicker)ctl).CustomFormat;
                }
                else if (ctl is NullableDateTimePicker)
                {
                    format = ((NullableDateTimePicker)ctl).CustomFormat;
                }

                DateTime dt1;
                DateTime dt2;

                if (DateTime.TryParse(val1, out dt1) && DateTime.TryParse(val2, out dt2))
                {
                    return dt1.ToString(format) != dt2.ToString(format);
                }
            }
            // チェックボックス
            else if (val1 != val2 && ctl is CheckBox)
            {
                val1 = val1 == null || val1 == "0" || val1 == string.Empty ? string.Empty : "1";
                val2 = val2 == null || val2 == "0" || val2 == string.Empty ? string.Empty : "1";
            }

            return val1 != val2;
        }
        #endregion

        #endregion

        #region 印刷・出力

        #region ラベル印刷
        /// <summary>
        /// ラベル印刷
        /// </summary>
        private void PrintLabel()
        {
            // 出力情報の設定
            var data = this.TestCarDefault;

            try
            {
                // ラベル印刷実行
                new LabelPrint().Print(data);
            }
            catch (Exception ex)
            {
                //エラーメッセージ表示
                Messenger.Error(Resources.TCM03023, ex);
                return;
            }

            // DB登録処理
            data.管理ラベル発行有無 = 1;
            this.PutLabelData(new List<TestCarCommonModel> { data });
        }
        #endregion

        #region EXCEL出力
        /// <summary>
        /// EXCEL出力
        /// </summary>
        private void SaveXls()
        {
            //管理所在地がない場合、メッセージを出力して終了
            if (string.IsNullOrWhiteSpace(this.TestCarDefault.ESTABLISHMENT) == true)
            {
                Messenger.Warn(Resources.TCM03027);
                return;
            }

            //出力する受領票を選択
            ReceiptForm.CarKind receiptKind;
            using (var rec = new ReceiptForm
            { data = this.TestCarDefault, printType = "receipt" })
            {
                if (rec.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                receiptKind = rec.ReceiptKind;
            }

            // 受領票出力
            this.SaveReceipt(receiptKind);
        }
        #endregion

        #region 受領票EXCEL出力
        /// <summary>
        /// 受領票EXCEL出力
        /// </summary>
        /// <param name="receiptKind">受領票種別</param>
        private void SaveReceipt(ReceiptForm.CarKind receiptKind)
        {
            // 出力情報
            var data = this.TestCarDefault;
            var history = this.TestCarHistoryList.Where(x => x.履歴NO == this.Page.CurrentPage).FirstOrDefault();

            // 資産種別によって出力する受領票を決定
            byte[] file;
            string fileName;
            string filePath;

            switch (receiptKind)
            {
                case ReceiptForm.CarKind.TestCarG:
                    file = Resources.ReceiptTestCarG;
                    fileName = string.Format("受領票_試作車_{0}_{1}.xlsx", data.管理票NO, DateTime.Now.ToString("yyyyMMdd"));
                    break;
                case ReceiptForm.CarKind.FixedAssetG:
                    file = Resources.ReceiptFixedAssetG;
                    if (data.種別 == "リース")
                    {
                        fileName = string.Format("受領票_リース_{0}_{1}.xlsx", data.管理票NO, DateTime.Now.ToString("yyyyMMdd"));
                    }
                    else
                    {
                        fileName = string.Format("受領票_生産車_{0}_{1}.xlsx", data.管理票NO, DateTime.Now.ToString("yyyyMMdd"));
                    }
                    break;
                case ReceiptForm.CarKind.TestCarT:
                    file = Resources.ReceiptTestCarT;
                    fileName = string.Format("受領票_試作車_{0}_{1}.xlsx", data.管理票NO, DateTime.Now.ToString("yyyyMMdd"));
                    break;
                case ReceiptForm.CarKind.OtherT:
                    file = Resources.ReceiptOtherT;
                    fileName = string.Format("受領票_試作車以外_{0}_{1}.xlsx", data.管理票NO, DateTime.Now.ToString("yyyyMMdd"));
                    break;
                default:
                    return;
            }

            using (var sfd = new SaveFileDialog
            {
                Title = "受領票出力",
                Filter = "Excel ブック (*.xlsx)|*.xlsx;",
                FileName = fileName
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                }
                else
                {
                    return;
                }
            }

            FormControlUtil.FormWait(this, () =>
            {
                using (var xls = new XlsUtil(file))
                {
                    //書込

                    //管理部署
                    string manageSection = "";
                    string sendTo = "";
                    if (string.IsNullOrWhiteSpace(data.SECTION_CODE) == false &&
                        string.IsNullOrWhiteSpace(data.SECTION_GROUP_CODE) == false)
                    {
                        manageSection = string.Format("{0}課 {1}", data.SECTION_CODE, data.SECTION_GROUP_CODE);
                        sendTo = string.Format("{0} 殿", manageSection);
                    }
                    //車種
                    string kind1 = string.Format("{0}_{1}_{2}", this.HistoryGeneralCodeComboBox.Text,
                            this.HistoryPrototypeTimingComboBox.Text, this.HistoryVehicleTextBox.Text);
                    string sendTokind1 = string.Format("【  {0}  】", kind1);

                    string kind2 = "";
                    if (data.種別 == "リース")
                    {
                        kind2 = string.Format("{0}_{1}_ﾘｰｽ", this.HistoryMakerNameComboBox.Text, this.HistoryOuterCarNameTextBox.Text);
                    }
                    else
                    {
                        kind2 = string.Format("{0}_生産", this.HistoryGeneralCodeComboBox.Text);
                    }
                    string sendTokind2 = string.Format("【  {0}  】", kind2);

                    //仕向地
                    string location = "";
                    if (string.IsNullOrWhiteSpace(this.HistoryShimukechiComboBox.Text) == false)
                    {
                        location = string.Format("仕向地({0})", this.HistoryShimukechiComboBox.Text);
                    }
                    //完成日
                    string compDate1 = "";
                    if (this.HistoryCompletionDayDateTimePicker.SelectedDate != null)
                    {
                        compDate1 = string.Format("{0:M月d日}", HistoryCompletionDayDateTimePicker.SelectedDate);
                    }
                    string compDate2 = "";
                    if (this.HistoryCompletionDayDateTimePicker.SelectedDate != null)
                    {
                        compDate2 = string.Format("{0:yy/M/d}", HistoryCompletionDayDateTimePicker.SelectedDate);
                    }
                    //登録日
                    string saveDate = "";
                    if (this.HistoryLicenseDayDateTimePicker.SelectedDate != null)
                    {
                        saveDate = string.Format("{0:M月d日}", HistoryLicenseDayDateTimePicker.SelectedDate);
                    }

                    switch (receiptKind)
                    {
                        case ReceiptForm.CarKind.TestCarG:
                            //試作車（群馬）
                            xls.WriteSheet(SheetName, new Dictionary<string, string>
                            {
                                //管理部署
                                { "B3", manageSection },
                                //車種
                                { "B6", kind1 },
                                //仕向地
                                { "C5", location },
                                //車体色
                                { "B15", this.HistoryCarColorComboBox.Text },
                                //完成日
                                { "V15", compDate1},
                                //排気量
                                { "M21", string.Format("{0} cc", this.HistoryDisplacementComboBox.Text) },
                                //宛名
                                { "B42", sendTo },
                                //車種・号車
                                { "B45", sendTokind1 },
                                //駐車場No.
                                { "U18", data.SECTION_CODE },
                            });

                            //T/M形式
                            if (string.IsNullOrWhiteSpace(this.HistoryTMComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "D9", this.HistoryTMComboBox.Text },
                                    { "D10", "" },
                                });

                                //セルを結合
                                xls.MergeCell(SheetName, "D9:T10");
                            }
                            else
                            {
                                //値無し

                                //セルを結合
                                xls.MergeCell(SheetName, "D9:T9");
                                xls.MergeCell(SheetName, "D10:T10");
                            }

                            //駆動方式
                            if (string.IsNullOrWhiteSpace(this.HistoryDriveSystemComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "D12", this.HistoryDriveSystemComboBox.Text },
                                });
                            }
                            else
                            {
                                //値無し
                            }

                            //車型
                            if (string.IsNullOrWhiteSpace(this.BaseCarTypeComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "B18", this.BaseCarTypeComboBox.Text },
                                    { "B19", "" },
                                });

                                //セルを結合
                                xls.MergeCell(SheetName, "B18:C19");
                            }
                            else
                            {
                                //値無し

                                //セルを結合
                                xls.MergeCell(SheetName, "B18:C18");
                                xls.MergeCell(SheetName, "B19:C19");
                            }

                            //E/G形式
                            if (string.IsNullOrWhiteSpace(this.HistoryEGTypeComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "C21", this.HistoryEGTypeComboBox.Text },
                                });
                            }
                            else
                            {
                                //値無し
                            }

                            //電動デバイス
                            if (string.IsNullOrWhiteSpace(this.HistoryEVDeviceComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "D21", this.HistoryEVDeviceComboBox.Text },
                                    { "D22", "" },
                                });

                                //セルを結合
                                xls.MergeCell(SheetName, "D21:L22");
                            }
                            else
                            {
                                //値無し

                                //セルを結合
                                xls.MergeCell(SheetName, "D21:L21");
                                xls.MergeCell(SheetName, "D22:L22");
                            }

                            break;

                        case ReceiptForm.CarKind.FixedAssetG:
                            //生産車（群馬）
                            xls.WriteSheet(SheetName, new Dictionary<string, string>
                            {
                                //管理部署
                                { "B3", manageSection },
                                //車種
                                { "B6", kind2 },
                                //仕向地
                                { "C5", location },
                                //車体色
                                { "B15", this.HistoryCarColorComboBox.Text },
                                //登録日
                                { "U15", saveDate },
                                //登録No
                                { "U18", this.HistoryLicensePlateNumberTextBox.Text },
                                //排気量
                                { "M21", string.Format("{0} cc", this.HistoryDisplacementComboBox.Text) },
                                //宛名
                                { "B42", sendTo },
                                //車種・号車
                                { "B45", sendTokind2 },
                            });

                            //T/M形式
                            if (string.IsNullOrWhiteSpace(this.HistoryTMComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "D9", this.HistoryTMComboBox.Text },
                                    { "D10", "" },
                                });

                                //セルを結合
                                xls.MergeCell(SheetName, "D9:T10");
                            }
                            else
                            {
                                //値無し

                                //セルを結合
                                xls.MergeCell(SheetName, "D9:T9");
                                xls.MergeCell(SheetName, "D10:T10");
                            }

                            //駆動方式
                            if (string.IsNullOrWhiteSpace(this.HistoryDriveSystemComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "D12", this.HistoryDriveSystemComboBox.Text },
                                });
                            }
                            else
                            {
                                //値無し
                            }

                            //車型
                            if (string.IsNullOrWhiteSpace(this.BaseCarTypeComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "B18", this.BaseCarTypeComboBox.Text },
                                    { "B19", "" },
                                });

                                //セルを結合
                                xls.MergeCell(SheetName, "B18:C19");
                            }
                            else
                            {
                                //値無し

                                //セルを結合
                                xls.MergeCell(SheetName, "B18:C18");
                                xls.MergeCell(SheetName, "B19:C19");
                            }

                            //E/G形式
                            if (string.IsNullOrWhiteSpace(this.HistoryEGTypeComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "D21", this.HistoryEGTypeComboBox.Text },
                                });
                            }
                            else
                            {
                                //値無し
                            }

                            //電動デバイス
                            if (string.IsNullOrWhiteSpace(this.HistoryEVDeviceComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "D24", this.HistoryEVDeviceComboBox.Text },
                                    { "D25", "" },
                                });

                                //セルを結合
                                xls.MergeCell(SheetName, "D24:L25");
                            }
                            else
                            {
                                //値無し

                                //セルを結合
                                xls.MergeCell(SheetName, "D24:L24");
                                xls.MergeCell(SheetName, "D25:L25");
                            }

                            break;

                        case ReceiptForm.CarKind.TestCarT:
                            //試作車（三鷹）

                            //提出期限
                            DateTime period = DateTimeUtil.GetBusinessDay(DateTime.Today, 4);

                            //履歴１の情報
                            //改修車両（履歴２以降車両）のみ表示
                            //履歴１のみの場合は『-』
                            string carKind = "-";
                            string fixedAssetNo = "旧資産№＿＿＿＿＿";
                            if (this.TestCarHistoryList != null && 1 < this.TestCarHistoryList.Count &&
                                this.TestCarHistoryList.Min(y => y.履歴NO) != this.Page.CurrentPage)
                            {
                                var firstHistory = this.TestCarHistoryList.Where(x => x.履歴NO == this.TestCarHistoryList.Max(y => y.履歴NO) - 1).FirstOrDefault();
                                carKind = string.Format("{0}_{1} {2}", firstHistory.開発符号, firstHistory.試作時期, firstHistory.号車);
                                fixedAssetNo = string.Format("旧資産№{0}", firstHistory.固定資産NO);
                            }
                            string cc = "";
                            if (!string.IsNullOrWhiteSpace(this.HistoryDisplacementComboBox.Text))
                            {
                                cc = string.Format("{0} cc", this.HistoryDisplacementComboBox.Text);
                            }

                            //使用期限算出
                            //Delete Start 2023/01/25 杉浦 試験車情報画面と受領表の書式変更
                            //string limitDate;
                            //if (data.完成日.HasValue)
                            //{
                            //    limitDate = string.Format("{0:yyyy年MM月}", data.完成日.Value.AddYears(1).AddMonths(-1));
                            //}
                            //else
                            //{
                            //    limitDate = "";
                            //}
                            //Delete End 2023/01/25 杉浦 試験車情報画面と受領表の書式変更

                            xls.WriteSheet(SheetName, new Dictionary<string, string>
                            {
                                //管理部署
                                { "B3", manageSection },
                                //提出期限
                                { "S3", string.Format("{0:yy/M/d}", period) },
                                //完成日
                                { "AA3", compDate2 },
                                //車種
                                { "B6", kind1 },
                                //仕向地
                                { "M5", this.HistoryShimukechiComboBox.Text },
                                //改修前車種
                                { "S6", carKind },
                                //旧資産No.
                                { "AA5", fixedAssetNo },
                                //車体色
                                { "AA15", this.HistoryCarColorComboBox.Text },
                                //排気量
                                { "S15", cc },
                                //管理票No
                                { "AJ9", this.BaseControlNoTextBox.Text },
                                { "AK44", this.BaseControlNoTextBox.Text },
                                //駐車場
                                { "AJ18", "フリーエリア" },
                                //宛名
                                { "B30", sendTo },
                                //車種・号車
                                { "B33", sendTokind1 },
                                //使用期限
                                //Delete Start 2023/01/25 杉浦 試験車情報画面と受領表の書式変更
                                //{ "AJ21", limitDate },
                                //Delete End 2023/01/25 杉浦 試験車情報画面と受領表の書式変更
                            });

                            //電動デバイス
                            if (string.IsNullOrWhiteSpace(this.HistoryEVDeviceComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "S21", this.HistoryEVDeviceComboBox.Text }
                                });
                            }

                            //セルを結合
                            xls.MergeCell(SheetName, "S21:AI22");

                            //T/M形式
                            if (string.IsNullOrWhiteSpace(this.HistoryTMComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "S18", this.HistoryTMComboBox.Text },
                                    { "S19", "" }
                                });
                            }

                            //セルを結合
                            xls.MergeCell(SheetName, "S18:AI18");
                            xls.MergeCell(SheetName, "S19:AI19");


                            //車型
                            if (string.IsNullOrWhiteSpace(this.BaseCarTypeComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "B15", this.BaseCarTypeComboBox.Text }
                                });
                            }

                            //駆動方式
                            if (string.IsNullOrWhiteSpace(this.HistoryDriveSystemComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "AA12", this.HistoryDriveSystemComboBox.Text }
                                });
                            }

                            //E/G形式
                            if (string.IsNullOrWhiteSpace(this.HistoryEGTypeComboBox.Text) == false)
                            {
                                //値有り

                                xls.WriteSheet(SheetName, new Dictionary<string, string>
                                {
                                    { "S12", this.HistoryEGTypeComboBox.Text }
                                });
                            }

                            break;

                        case ReceiptForm.CarKind.OtherT:
                            //試作車以外（三鷹）
                            xls.WriteSheet(SheetName, new Dictionary<string, string>
                            {
                                //管理部署
                                { "B3", manageSection },
                                { "M27", manageSection },
                                //車種
                                { "B6", kind1 },
                                //仕向地
                                { "C5", location },
                                //管理票No
                                { "U9", this.BaseControlNoTextBox.Text },
                                //研命ナンバー
                                { "U12", this.HistoryKenmeiPlateNumberTextBox.Text },
                                //資産No.(固定資産NO)
                                { "U15", this.HistoryFixedAssetNoTextBox.Text },
                            });

                            break;
                    }

                    //保存
                    xls.Save(filePath);
                }
            });
        }
        #endregion

        #endregion

        #region 駐車場番号

        #region 駐車場番号コンボボックスクリック
        /// <summary>
        /// 駐車場番号コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseParkingNoComboBox_Click(object sender, EventArgs e)
        {
            var isMask = this.MaskingCheckBox.Checked;
            var isNew = !(this.TestCar?.データID > 0);

            // 新規作成でない閲覧中は終了
            if (!isNew && !isMask)
            {
                return;
            }

            //一時確保している駐車場があれば開放する。
            this.ClearParkingInfo();

            using (
                var form = new ParkingSelectForm
                {
                    UserAuthority = this.UserAuthority,
                    Data = this.ParkingInfo,
                    IsSelectMode = IsMoveMode
                })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //エリアまたは区画が選択された
                    this.BaseParkingNoComboBox.Items.Clear();
                    this.BaseParkingNoComboBox.Items.Add(form.Data.NAME);
                    this.BaseParkingNoComboBox.SelectedIndex = 0;
                    this.ParkingInfo = form.Data;
                    //Append Start 2021/07/20 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                    if (this.CollisionFinishedCheckBox.Checked)
                    {
                        this.CollisionFinishedCheckBox.Checked = false;
                        this.CollisionFinishedCheckBox.Enabled = true;
                    }
                    //Append End 2021/07/20 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                }
                else
                {
                    //選択されなかった
                }
            }
        }
        #endregion

        #region 一時確保している駐車場の開放
        /// <summary>
        /// 一時確保している駐車場の開放
        /// </summary>
        private void ClearParkingInfo()
        {
            //一時確保している駐車場があれば開放する。（PARKING_SECTIONのSTATUSを0）
            if (this.ParkingInfo.SECTION_NO != null)
            {
                List<ParkingModel> list = new List<ParkingModel>();
                var cond = new ParkingModel
                {
                    LOCATION_NO = this.ParkingInfo.LOCATION_NO,
                    AREA_NO = this.ParkingInfo.AREA_NO,
                    SECTION_NO = this.ParkingInfo.SECTION_NO,
                    STATUS = 0,
                    INPUT_PERSONEL_ID = SessionDto.UserId,
                };
                list.Add(cond);

                var res = HttpUtil.PutResponse<ParkingModel>(ControllerType.ParkingSection, list);
            }

            //保存情報をクリア
            this.ParkingInfo = new ParkingModel();
        }
        #endregion

        #region 駐車場選択情報削除
        /// <summary>
        /// 駐車場選択情報削除
        /// </summary>
        /// <returns></returns>
        private bool DeleteParkingInfo()
        {
            //駐車場選択情報を削除（PARKING_SECTIONのSTATUSを0,PARKING_USEから情報を削除）
            List<ParkingUseModel> list = new List<ParkingUseModel>();
            var cond = new ParkingUseModel
            {
                データID = this.TestCar.データID,
                INPUT_PERSONEL_ID = SessionDto.UserId,
            };
            list.Add(cond);

            var res = HttpUtil.DeleteResponse<ParkingUseModel>(ControllerType.ParkingUse, list);

            return res.Status.Equals(Const.StatusSuccess);
        }
        #endregion

        #region 駐車場選択情報登録
        /// <summary>
        /// 駐車場選択情報登録
        /// </summary>
        /// <returns></returns>
        private bool AddParkingInfo()
        {
            //駐車場選択情報を登録
            List<ParkingUseModel> list = new List<ParkingUseModel>();
            var cond = new ParkingUseModel
            {
                データID = this.TestCar.データID,
                LOCATION_NO = this.ParkingInfo.LOCATION_NO,
                AREA_NO = this.ParkingInfo.AREA_NO,
                SECTION_NO = this.ParkingInfo.SECTION_NO,
                INPUT_PERSONEL_ID = SessionDto.UserId,
            };
            list.Add(cond);

            var res = HttpUtil.PostResponse<ParkingUseModel>(ControllerType.ParkingUse, list);
            return res.Status.Equals(Const.StatusSuccess);
        }
        #endregion

        #endregion

        private void GpsGoButton_Click(object sender, EventArgs e)
        {
            //管理票Noを取得する
            ScheduleToXeyeSearchModel searchModel = new ScheduleToXeyeSearchModel();
            searchModel.物品名2 = this.TestCar.管理票NO;

            // XeyeのIDを取得する
            var res = HttpUtil.GetResponse<ScheduleToXeyeSearchModel, ScheduleToXeyeOutModel>(ControllerType.Xeye, searchModel);
            var xeyeId = new List<ScheduleToXeyeOutModel>();
            xeyeId.AddRange(res.Results);

            // Xeyeページに接続するフォームを起動
            var frm = new WebBrowserForm(xeyeId[0].備考);
            frm.Show();
        }

        //Append Start 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
        private void CollisionFinishedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Update Start 2021/07/28 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            //if (!string.IsNullOrEmpty(this.TestCar.駐車場番号) && this.CollisionFinishedCheckBox.Checked)
            if (!string.IsNullOrEmpty(this.BaseParkingNoComboBox.Text) && this.CollisionFinishedCheckBox.Checked)
            //Update End 2021/07/28 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            {
                //Append Start 2021/07/20 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                if (Messenger.Confirm(Resources.KKM02017) == DialogResult.Yes)
                {
                    //Append End 2021/07/20 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                    //駐車場番号の更新情報を初期化
                    this.TestCar.駐車場番号 = "";
                    //駐車場番号の更新情報を初期化
                    this.BaseParkingNoComboBox.Items.Clear();

                    //Update Start 2021/07/28 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                    //this.PutData();
                    this.GetFormData();

                    var res = this.PutData();

                    this.CollisionFinishedCheckBox.Enabled = false;

                    //Update End 2021/07/28 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                    //Append Start 2021/07/20 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                }
                else
                {
                    this.CollisionFinishedCheckBox.Checked = false;
                }
                //Append End 2021/07/20 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            }
        }
        //Append End 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
    }
}
