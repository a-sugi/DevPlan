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
    /// 管理票発行
    /// </summary>
    public partial class ControlSheetIssueEntryForm : BaseSubForm
    {
        #region メンバ変数
        private const string ControlSheetIssueCode = "A";
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "管理票発行"; } }

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

        /// <summary>管理表文言</summary>
        public string adaministrationText { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControlSheetIssueEntryForm()
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
        private void ControlSheetIssueEntryForm_Load(object sender, EventArgs e)
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
            this.ActiveControl = this.HistoryReceiptDayDateTimePicker;

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
                // 受領先
//                FormControlUtil.SetComboBoxItem(this.HistoryReceiptDestinationComboBox, GetRecipientList());
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

            // 発行ボタン
            this.IssueButton.Visible = isUpdate;
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
            // 使用履歴情報の取得
            var usehistory = GetUseHistoryData(new TestCarUseHistorySearchModel
            {
                データID = (int)this.TestCar?.データID,
                履歴NO = (int)this.TestCar?.履歴NO
            });
            
            // 発行年月日
            this.HistoryControlSheetIssueDayDateTimePicker.Value = this.TestCar?.発行年月日 ?? DateTime.Now.Date;

            // 受領先
/*            this.HistoryReceiptDestinationComboBox.SelectedValue
                = usehistory?.Where(x => x?.SEQNO == usehistory?.Where(y => y.承認要件コード == ControlSheetIssueCode).Max(max => max?.SEQNO)
                && x?.承認要件コード == ControlSheetIssueCode).FirstOrDefault().試験内容;*/

            // 受領日
            this.HistoryReceiptDayDateTimePicker.Value = this.TestCar?.受領日;
        }
        #endregion

        #region フォーム値の取得
        /// <summary>
        /// フォーム値の取得
        /// </summary>
        private void GetFormData()
        {
            // 管理票発行有無（固定）
            this.TestCar.管理票発行有無 = "済";

            // 発行年月日
            this.TestCar.発行年月日 = this.HistoryControlSheetIssueDayDateTimePicker.SelectedDate;

            // 受領先
            // 未使用

            // 受領日
            this.TestCar.受領日 = this.HistoryReceiptDayDateTimePicker.SelectedDate;
        }
        #endregion

        #endregion

        #region イベント

        #region 発行ボタンクリック
        /// <summary>
        /// 発行ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IssueButton_Click(object sender, EventArgs e)
        {
            //管理表タイプ取得
            GetPrintType();
            if (string.IsNullOrEmpty(this.adaministrationText))
            {
                Messenger.Info(Resources.KKM01023); // 管理票の種類を選択してください
                return;
            }

            // 入力項目のチェック
            if (!this.IsEntryDataCheck()) return;

            // フォーム値の取得
            this.GetFormData();

            var res = this.PutData();

            bool historyInsert = true;
            if (res.Status.Equals(Const.StatusSuccess))
            {
                historyInsert = InsertHistory(this.TestCar);
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
            if (!string.IsNullOrWhiteSpace(this.TestCar?.車体番号))
            {
                var cond = new TestCarCommonSearchModel() { 車体番号 = this.TestCar?.車体番号 };
                var list = GetData(cond);

                // 同一車体番号がある場合は更新不可
                var listinfo = list.Where(x => x.管理票NO != this.TestCar.管理票NO && x.車体番号 == this.TestCar?.車体番号).FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(listinfo?.管理票NO))
                {
                    Messenger.Warn(string.Format(Resources.TCM03003, listinfo?.管理票NO));

                    return false;
                }
            }

            // 完成日チェック
            if (!string.IsNullOrWhiteSpace(this.TestCar?.固定資産NO) &&
                this.TestCar?.研命ナンバー?.StartsWith("A") == true)
            {
                // 未入力はエラー
                if (this.TestCar?.完成日 == null)
                {
                    Messenger.Warn(Resources.TCM03004);

                    return false;
                }

                var date = (DateTime)this.TestCar?.完成日;
            }

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
            }else{
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
                SECTION_GROUP_CODE = manage[0].SECTION_GROUP_CODE,

                //使用課名
                使用課名 = receipt[0].SECTION_CODE,

                //使用部署名
                使用部署名 = receipt[0].SECTION_GROUP_CODE,

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

        #region データ更新処理
        /// <summary>
        /// データ更新処理
        /// </summary>
        private ResponseDto<TestCarCommonModel> PutData()
        {
            return HttpUtil.PutResponse<TestCarCommonModel>(ControllerType.ControlSheetTestCar, this.TestCar);
        }
        #endregion
        
        #endregion


        #region マスタデータの取得・検索

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
    }
}
