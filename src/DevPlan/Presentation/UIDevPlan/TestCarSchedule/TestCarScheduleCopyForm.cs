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

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;


namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    /// <summary>
    /// 試験車コピー元データ選択
    /// </summary>
    public partial class TestCarScheduleCopyForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "コピー元データ　選択"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>開発符号</summary>
        public string GeneralCode { get; set; }

        /// <summary>コピー先ステータス </summary>
        public string TargetStatus { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCarScheduleCopyForm()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarScheduleCopyForm_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void InitForm()
        {
            // 初期値はnullに設定する
            StartDateTimePicker.Value = null;

            // 引数として渡されていれば対象期間（終了）をカレンダーに設定する
            EndDateTimePicker.Value = null;

            // 最終調整結果から遷移した時のみ活性化
            AjustRadioButton.Enabled = TargetStatus == Const.Kettei;

            // 遷移元ステータスが「１次調整」なら使用部署要望案をOnに
            RequestRadioButton.Checked = TargetStatus == Const.Tyousei;
            
            // 遷移元ステータスが「最終調整結果」ならSJSB調整案をOnに
            AjustRadioButton.Checked = TargetStatus == Const.Kettei;

        }

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () => ScheduleDataCopyCommit());

        }
        #endregion

        #region 入力項目の内容チェック
        /// <summary>
        /// 入力項目の内容チェック
        /// </summary>
        /// <returns>true/false</returns>
        private bool InputItemCheck()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            //期間の大小チェック
            map[this.EndDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                //対象期間（開始）と対象期間（終了）がすべて入力してある場合のみチェック
                if (this.StartDateTimePicker.Value != null && this.EndDateTimePicker.Value != null)
                {
                    //開始日が終了日より大きい場合はエラー
                    if (this.StartDateTimePicker.SelectedDate.Value > this.EndDateTimePicker.SelectedDate.Value)
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM00018;

                        //背景色を変更
                        this.StartDateTimePicker.BackColor = Const.ErrorBackColor;
                        this.EndDateTimePicker.BackColor = Const.ErrorBackColor;
                    }
                }

                return errMsg;

            };

            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            return true;
        }
        #endregion

        #region コピー元データ選択API呼び出し
        /// <summary>
        /// コピー元データ選択API呼び出し
        /// </summary>
        /// <returns></returns>
        private void ScheduleDataCopyCommit()
        {
            // 入力内容のチェック
            if (InputItemCheck())
            {
                // APIにパラメータを渡す
                var prm = new TestCarScheduleCopyModel
                {
                    GENERAL_CODE = this.GeneralCode,
                    TARGET_START_DATE = StartDateTimePicker.SelectedDate.Value,
                    TARGET_END_DATE = EndDateTimePicker.SelectedDate,
                    SOURCE_STATUS = FormControlUtil.GetRadioButtonValue(this.CopySoucePanel),
                    TARGET_STATUS = TargetStatus
                };

                var res = HttpUtil.PostResponse<TestCarScheduleCopyModel>(ControllerType.TestCarScheduleCopy, prm, false);

                // 更新処理が正常終了
                if (res.Status == Const.StatusSuccess || res.ErrorCode == ApiMessageType.KKE03002.ToString())
                {
                    Messenger.Info(Resources.KKM00002);
                    base.FormOkClose();
                }
                else
                {
                    Messenger.Warn(Resources.KKM03000);
                    base.FormOkClose();
                }
            }
        }
        #endregion
    }
}
