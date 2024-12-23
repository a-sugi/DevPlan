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
    /// 一括本予約
    /// </summary>
    public partial class TestCarScheduleReserveForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "一括本予約"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>開発符号</summary>
        public string GeneralCode { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="favorite">お気に入り</param>
        public TestCarScheduleReserveForm()
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
        private void TestCarScheduleReserveForm_Load(object sender, EventArgs e)
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

        }
        #endregion

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () => ScheduleReserveCommit());

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

        #region 一括本予約API呼び出し
        /// <summary>
        /// 一括本予約API呼び出し
        /// </summary>
        /// <returns></returns>
        private void ScheduleReserveCommit()
        {
            // 入力内容のチェック
            if (InputItemCheck())
            {
                var prm = new TestCarScheduleReserveModel
                {
                    GENERAL_CODE = this.GeneralCode,
                    TARGET_START_DATE = StartDateTimePicker.SelectedDate.Value,
                    TARGET_END_DATE = EndDateTimePicker.SelectedDate

                };

                var res = HttpUtil.PutResponse<TestCarScheduleReserveModel>(ControllerType.TestCarScheduleReserve, prm);

                // 更新処理が正常終了
                if (res.Status == Const.StatusSuccess)
                {
                    Messenger.Info(Resources.KKM00002);
                    base.FormClose(DialogResult.OK);
                }

            }

        }
        #endregion
    }
}
