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

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.MeetingDocument
{
    /// <summary>
    /// 確認完了日入力
    /// </summary>
    public partial class CompletionScheduleForm : BaseSubForm
    {
        #region メンバ変数
        private const string Zumi = "済";
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "確認完了日入力"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>検討会資料詳細</summary>
        public MeetingDocumentDetailModel MeetingDocumentDetai { get; set; }

        /// <summary>済入力可否</summary>
        public bool IsCompletion { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CompletionScheduleForm()
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
        private void CompletionScheduleForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var day = DateTime.Today;

            var kanryouNittei = this.MeetingDocumentDetai.完了日程情報;

            //確認完了予定日/完了日が入力されているかどうか
            if (string.IsNullOrWhiteSpace(kanryouNittei) == false)
            {
                var value = kanryouNittei.Replace(Const.CrLf, Const.Lf).Replace(Const.Cr, Const.Lf).Split(new[] { Const.Lf }, StringSplitOptions.None)[0];

                //日付に変換できるかどうか
                var kanryouNitteiDay = DateTimeUtil.ConvertDateStringToDateTime(value);
                if (kanryouNitteiDay != null)
                {
                    day = kanryouNitteiDay.Value.Date;

                }

            }

            //確認完了予定日/完了日
            this.CompletionScheduleDateTimePicker.Value = day;

        }
        #endregion

        #region 登録ボタン
        /// <summary>
        /// 登録ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;

            }

            var sb = new StringBuilder(DateTimeUtil.ConvertDateString(this.CompletionScheduleDateTimePicker.SelectedDate));

            //済入力をするかどうか
            if (this.IsCompletion == true)
            {
                sb.AppendLine();
                sb.Append(Zumi);

            }

            //確認完了予定日/完了日
            this.MeetingDocumentDetai.完了日程情報 = sb.ToString();

            //フォームクローズ
            base.FormOkClose();

        }
        #endregion
    }
}
