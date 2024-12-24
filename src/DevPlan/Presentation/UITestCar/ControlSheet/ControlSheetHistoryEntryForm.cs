using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UC;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UITestCar.ControlSheet
{
    /// <summary>
    /// 新規履歴作成
    /// </summary>
    public partial class ControlSheetHistoryEntryForm : BaseSubForm
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

        private const string ControlSheetIssueCode = "A";

        //Append Start 2024/01/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
        private readonly List<ComboBoxDto> CarRecycleList = new List<ComboBoxDto>()
        {
            new ComboBoxDto { ID = "対象外", CODE = "対象外", NAME = "対象外" },
            new ComboBoxDto { ID = "対象", CODE = "対象", NAME = "対象" }
        };
        private readonly List<ComboBoxDto> RefrigerantTypeList = new List<ComboBoxDto>()
        {
            new ComboBoxDto { ID = "Freon", CODE = "Freon", NAME = "HFC 134a フロン" },
            new ComboBoxDto { ID = "Refrigerant", CODE = "Refrigerant", NAME = "HFO 1234yf 新冷媒" }

        };
        //Append Start 2024/01/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "新規履歴作成"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>試験車情報</summary>
        public TestCarCommonModel TestCar { get; set; }

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }

        /// <summary>管理所在地</summary>
        private string ControlEstablishment { get; set; }

        /// <summary>受領部コード</summary>
        private string ReceiptDepartmentCode { get; set; }

        /// <summary>受領課コード</summary>
        private string ReceiptSectionCode { get; set; }

        /// <summary>管理表文言</summary>
        public string adaministrationText { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControlSheetHistoryEntryForm()
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
        private void ControlSheetHistoryEntryForm_Load(object sender, EventArgs e)
        {
            // 画面の初期化
            this.InitForm();
        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            // 初期表示フォーカス
            this.ActiveControl = this.HistoryControlSheetIssueOrComboBox;

            // コンボボックスセット
            this.SetComboBox();

            // コントローラセット
            this.SetControler();

            // フォーム値セット
            this.SetFormData();
        }
        #endregion

        #region 画面の設定・取得

        #region コンボボックスのセット
        /// <summary>
        /// コンボボックスのセット
        /// </summary>
        private void SetComboBox()
        {
            //バインド中ON
            this.IsBind = true;

            try
            {
                // 発行有無
                FormControlUtil.SetComboBoxItem(this.HistoryControlSheetIssueOrComboBox, ControlSheetIssueOrList, false);

                // 開発符号
                FormControlUtil.SetComboBoxItem(this.HistoryGeneralCodeComboBox, GetGeneralCodeList());

                // 試作時期
                FormControlUtil.SetComboBoxItem(this.HistoryPrototypeTimingComboBox, GetPrototypeSeasonList());

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
                    , new List<ComboBoxDto>() { new ComboBoxDto { CODE = this.TestCar?.管理責任部署, NAME = this.TestCar?.管理責任部署 } });

                this.HistoryControlSectionGroupComboBox.SelectedValue = this.TestCar?.管理責任部署 ?? string.Empty;

                // 工事区分NO（管理所在地による分岐）
                SetHistoryControlSectionGroupComboBox();

                // 受領先
                FormControlUtil.SetComboBoxItem(this.HistoryReceiptDestinationComboBox, GetRecipientList());

                // 受領部署
                FormControlUtil.SetComboBoxItem(this.HistoryReceiptSectionGroupComboBox
                    , new List<ComboBoxDto>() { new ComboBoxDto { CODE = this.TestCar?.受領部署, NAME = this.TestCar?.受領部署 } });

                this.HistoryReceiptSectionGroupComboBox.SelectedValue = this.TestCar?.受領部署 ?? string.Empty;

                // 受領者
                FormControlUtil.SetComboBoxItem(this.HistoryReceiptUserComboBox
                    , new List<ComboBoxDto>() { new ComboBoxDto { CODE = this.TestCar?.受領者, NAME = this.TestCar?.受領者_NAME } });

                this.HistoryReceiptUserComboBox.SelectedValue = this.TestCar?.受領者 ?? string.Empty;

                //Append Start 2024/01/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                // 自動車ﾘｻｲｸﾙ法
                FormControlUtil.SetComboBoxItem(this.CarRecycleComboBox, CarRecycleList);

                // A/C冷媒種類
                FormControlUtil.SetComboBoxItem(this.RefrigerantTypeComboBox, RefrigerantTypeList);
                //Append End 2024/01/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
            }
            finally
            {
                //バインド中OFF
                this.IsBind = false;
            }
        }
        #endregion

        #region コントローラのセット
        /// <summary>
        /// コントローラのセット
        /// </summary>
        private void SetControler()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';

            // 登録ボタン
            this.EntryButton.Visible = isUpdate;
        }
        #endregion

        #region フォーム値のセット
        /// <summary>
        /// フォーム値のセット
        /// </summary>
        private void SetFormData()
        {
            // 履歴情報
            this.SetHistoryFormData();
        }
        #endregion

        #region フォーム値（試験車履歴情報）のセット
        /// <summary>
        /// フォーム値（試験車履歴情報）のセット
        /// </summary>
        private void SetHistoryFormData()
        {
            // 履歴NO
            this.HistoryNoLabel.Text = (this.TestCar?.履歴NO + 1).ToString();

            // 管理票発行有無
            this.HistoryControlSheetIssueOrComboBox.SelectedValue = "未";

            // 発行年月日
            this.HistoryControlSheetIssueDayDateTimePicker.Value = string.Empty;

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
            this.HistoryTestPurposeTextBox.Text = string.Empty;

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

            // 受領時走行距離
            this.HistoryMileageTextBox.Text = string.Empty;

            // 完成日
            this.HistoryCompletionDayDateTimePicker.Value = null;

            // 管理責任部署
            FormControlUtil.SetComboBoxItem(this.HistoryControlSectionGroupComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto() });

            this.HistoryControlSectionGroupComboBox.SelectedValue = string.Empty;

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
            this.HistoryReceiptDestinationComboBox.SelectedValue = string.Empty;

            // 受領日
            this.HistoryReceiptDayDateTimePicker.Value = null;

            // 受領部署
            FormControlUtil.SetComboBoxItem(this.HistoryReceiptSectionGroupComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto() }, false);

            this.HistoryReceiptSectionGroupComboBox.SelectedValue = string.Empty;

            // 受領者
            FormControlUtil.SetComboBoxItem(this.HistoryReceiptUserComboBox
                , new List<ComboBoxDto>() { new ComboBoxDto() }, false);

            this.HistoryReceiptUserComboBox.SelectedValue = string.Empty;

            //Append Start 2024/01/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
            // 自動車ﾘｻｲｸﾙ法
            this.CarRecycleComboBox.Text = this.TestCar?.自動車ﾘｻｲｸﾙ法;

            // A/C冷媒種類
            this.RefrigerantTypeComboBox.Text = this.TestCar?.A_C冷媒種類;
            //Append End 2024/01/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
        }
        #endregion

        #region フォーム値の取得
        /// <summary>
        /// フォーム値の取得
        /// </summary>
        private void GetFormData()
        {
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

            // 受領時走行距離
            this.TestCar.受領時走行距離 = this.HistoryMileageTextBox.Text;

            // 完成日
            this.TestCar.完成日 = this.HistoryCompletionDayDateTimePicker.SelectedDate;

            // 管理責任部署
            this.TestCar.管理責任部署 = this.HistoryControlSectionGroupComboBox?.SelectedValue?.ToString();

            // 管理所在地 TODO
            this.TestCar.ESTABLISHMENT = "";

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

            // 受領先
            // 別テーブル TODO

            // 受領日
            this.TestCar.受領日 = this.HistoryReceiptDayDateTimePicker.SelectedDate;

            // 受領部署
            this.TestCar.受領部署 = this.HistoryReceiptSectionGroupComboBox?.SelectedValue?.ToString();

            // 受領者
            this.TestCar.受領者 = this.HistoryReceiptUserComboBox?.SelectedValue?.ToString();

            // 部ID
            this.TestCar.DEPARTMENT_ID = SessionDto.DepartmentID;

            // 課ID
            this.TestCar.SECTION_ID = SessionDto.SectionID;

            // 担当ID
            this.TestCar.SECTION_GROUP_ID = SessionDto.SectionGroupID;

            //Append Start 2024/01/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
            //自動車ﾘｻｲｸﾙ法
            this.TestCar.自動車ﾘｻｲｸﾙ法 = this.CarRecycleComboBox?.SelectedValue?.ToString();

            //A_C冷媒種類
            this.TestCar.A_C冷媒種類 = this.RefrigerantTypeComboBox?.SelectedValue?.ToString();
            //Append End 2024/01/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
        }
        #endregion

        #endregion

        #region イベント

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

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            //管理表タイプ取得
            if (this.HistoryControlSheetIssueOrComboBox.SelectedValue.ToString() == "済")
            {
                GetPrintType();
                if (string.IsNullOrEmpty(this.adaministrationText))
                {
                    Messenger.Info(Resources.KKM01023); // 登録完了
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
                if (this.TestCar.管理票発行有無 == "済")
                {
                    historyInsert = InsertHistory(this.TestCar);
                }
            }

            if (res.Status.Equals(Const.StatusSuccess) && historyInsert)
            {
                Messenger.Info(Resources.KKM00002); // 登録完了

                base.FormOkClose();
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
        private bool IsEntryDataCheck()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            // 廃却申請チェック
            if (this.TestCar?.データID > 0)
            {
                var baseinfo = GetBaseData().FirstOrDefault();

                if (baseinfo?.研実管理廃却申請受理日 != null)
                {
                    Messenger.Warn(Resources.TCM03002);

                    return false;
                }
            }

            // 承認処理中チェック
            if (this.TestCar?.データID > 0)
            {
                // 試験車使用履歴情報
                var useinfo = this.GetUseHistoryData(
                    new TestCarUseHistorySearchModel { データID = (int)this.TestCar.データID, 履歴NO = (int)this.TestCar?.履歴NO })?.Where(x => x.STEPNO > 0)?.ToList();

                if (useinfo.Any() == true)
                {
                    Messenger.Warn(Resources.TCM03021);

                    return false;
                }
            }

            // 車体番号重複チェック
            if (!string.IsNullOrWhiteSpace(this.HistoryVehicleNoTextBox.Text))
            {
                var cond = new TestCarCommonSearchModel() { 車体番号 = this.HistoryVehicleNoTextBox.Text };
                var list = GetData(cond);

                // 同一車体番号がある場合は更新不可
                var listinfo = list.Where(x => x.管理票NO != this.TestCar.管理票NO && x.車体番号 == this.HistoryVehicleNoTextBox.Text).FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(listinfo?.管理票NO))
                {
                    Messenger.Warn(string.Format(Resources.TCM03003, listinfo?.管理票NO));

                    this.HistoryVehicleNoTextBox.BackColor = Const.ErrorBackColor;

                    return false;
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

                //登録ナンバーが入力されていて車検期限が未入力はNG
                if (string.IsNullOrWhiteSpace(this.HistoryLicensePlateNumberTextBox.Text) == false && this.HistoryInspectionDayDateTimePicker.SelectedDate == null)
                {
                    //エラーメッセージ
                    errMsg = string.Format(Resources.TCM03028, name);

                }

                return errMsg;

            };

            //入力がOKかどうか
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
            { data = this.TestCar, printType = "administration" })
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
            //管理責任部署取得
            var manage = new List<SectionGroupModel>();
            if (!string.IsNullOrEmpty(this.TestCar.管理責任部署))
            {
                manage = GetSectionGroupData(this.TestCar.管理責任部署);
            }else
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
                履歴NO = item.履歴NO + 1,

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
            return HttpUtil.PostResponse<TestCarCommonModel>(ControllerType.ControlSheetTestCarHistory, this.TestCar);
        }
        #endregion

        #endregion

        #region マスタデータの取得・検索

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

        #endregion

        #region ローカルメソッド

        #region 工事区分NOコンボボックスのセット
        /// <summary>
        /// 工事区分NOのコンボボックスのセット
        /// </summary>
        private void SetHistoryControlSectionGroupComboBox()
        {
            var estalist = this.TestCar?.ESTABLISHMENT == "g" ? GunmaKoujiNoList : OtherKoujiNoList;
            estalist = this.TestCar?.ESTABLISHMENT == "t" ? TokyoKoujiNoList : estalist;

            var nowcode = this.HistoryConstructionNoComboBox?.Text;

            FormControlUtil.SetComboBoxItem(this.HistoryConstructionNoComboBox, estalist);

            this.HistoryConstructionNoComboBox.Text = nowcode;
        }
        #endregion

        #endregion
    }
}
