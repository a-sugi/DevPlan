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
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.UIDevPlan.MeetingDocument
{
    /// <summary>
    /// 検討会基本情報登録
    /// </summary>
    public partial class MeetingDocumentDetailForm : BaseSubForm
    {
        #region メンバ変数
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "検討会基本情報登録"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>検討会資料</summary>
        public MeetingDocumentModel MeetingDocument { get; set; }

        /// <summary>検討会資料編集区分</summary>
        public MeetingDocumentEditType MeetingDocumentEdit { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MeetingDocumentDetailForm()
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
        private void CarShareScheduleDetailForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

            //検討会資料画面セット
            this.SetMeetingDocumentForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //開催日
            this.HeldDayDateTimePicker.Value = null;

            //資料名
            FormControlUtil.SetComboBoxItem(this.DocumentNameComboBox, HttpUtil.GetResponse<MeetingDocumentNameModel>(ControllerType.MeetingDocumentName).Results);

            //更新開始日
            this.StartDayDateTimePicker.Value = null;

            //更新終了日
            this.EndDayDateTimePicker.Value = null;

        }

        /// <summary>
        /// スケジュール画面セット
        /// </summary>
        private void SetMeetingDocumentForm()
        {
            //資料種別
            FormControlUtil.SetRadioButtonValue(this.DocumentTypePanel, this.MeetingDocument.MEETING_FLAG);

            //開催日
            this.HeldDayDateTimePicker.Value = this.MeetingDocument.MONTH;

            //資料名
            this.DocumentNameComboBox.SelectedIndex = -1;
            this.DocumentNameComboBox.Text = this.MeetingDocument.NAME;

            //更新開始日
            this.StartDayDateTimePicker.Value = this.MeetingDocument.EDIT_TERM_START;

            //更新終了日
            this.EndDayDateTimePicker.Value = this.MeetingDocument.EDIT_TERM_END;

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
                //検討会資料のチェック
                if (this.IsEntryMeetingDocument() == true)
                {
                    //検討会資料の登録
                    this.EntryMeetingDocument();

                }

            });

        }
        #endregion

        #region 検討会資料のチェック
        /// <summary>
        /// 検討会資料のチェック
        /// </summary>
        /// <returns>チェック可否</returns>
        private bool IsEntryMeetingDocument()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            //期間の大小チェック
            map[this.EndDayDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                var start = this.StartDayDateTimePicker.SelectedDate;
                var end = this.EndDayDateTimePicker.SelectedDate;

                //更新開始日と更新終了日が入力で開始日が終了日より大きい場合はエラー
                if (start != null && end != null && start > end)
                {
                    //エラーメッセージ
                    errMsg = Resources.KKM00018;

                    //背景色を変更
                    this.StartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.EndDayDateTimePicker.BackColor = Const.ErrorBackColor;

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

            //検討会資料編集区分ごとの分岐
            switch (this.MeetingDocumentEdit)
            {
                //更新
                //コピー
                case MeetingDocumentEditType.Update:
                case MeetingDocumentEditType.Copy:
                    //検討会資料が存在しているかどうか
                    if (this.GetMeetingDocument() == null)
                    {
                        //存在していない場合はエラー
                        Messenger.Warn(Resources.KKM00021);
                        return false;

                    }
                    break;

            }

            return true;

        }
        #endregion

        #region 検討会資料の登録
        /// <summary>
        /// 検討会資料の登録
        /// </summary>
        private void EntryMeetingDocument()
        {
            ResponseDto<MeetingDocumentModel> res = null;

            var list = new[] { this.GetEntryMeetingDocument() };

            //検討会資料編集区分ごとの分岐
            switch (this.MeetingDocumentEdit)
            {
                //追加
                case MeetingDocumentEditType.Insert:
                    res = HttpUtil.PostResponse(ControllerType.MeetingDocument, list);
                    break;

                //更新
                case MeetingDocumentEditType.Update:
                    res = HttpUtil.PutResponse(ControllerType.MeetingDocument, list);
                    break;

                //コピー
                case MeetingDocumentEditType.Copy:
                    res = HttpUtil.PostResponse(ControllerType.MeetingDocument, list);
                    break;

            }

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //登録後メッセージ
                Messenger.Info(Resources.KKM00002);

                //フォームクローズ
                base.FormOkClose();

            }

        }

        /// <summary>
        /// 登録検討会資料クラス取得
        /// </summary>
        /// <returns></returns>
        private MeetingDocumentModel GetEntryMeetingDocument()
        {
            var meetingDocument = this.MeetingDocument;

            //資料種別
            meetingDocument.MEETING_FLAG = short.Parse(FormControlUtil.GetRadioButtonValue(this.DocumentTypePanel));

            //開催日
            meetingDocument.MONTH = this.HeldDayDateTimePicker.SelectedDate.Value;

            //資料名
            meetingDocument.NAME = this.DocumentNameComboBox.Text;

            //更新開始日
            meetingDocument.EDIT_TERM_START = this.StartDayDateTimePicker.SelectedDate;

            //更新終了日
            meetingDocument.EDIT_TERM_END = this.EndDayDateTimePicker.SelectedDate;

            //編集者パーソナルID
            meetingDocument.INPUT_PERSONEL_ID = SessionDto.UserId;

            //検討会資料編集区分がコピーかどうか
            if (this.MeetingDocumentEdit == MeetingDocumentEditType.Copy)
            {
                //資料_ID
                meetingDocument.資料_ID = meetingDocument.ID;

            }
            else
            {
                //資料_ID
                meetingDocument.資料_ID = null;

            }

            return meetingDocument;

        }
        #endregion

        #region データの取得
        /// <summary>
        /// 検討会資料の取得
        /// </summary>
        /// <returns></returns>
        private MeetingDocumentModel GetMeetingDocument()
        {
            return this.GetMeetingDocument(new MeetingDocumentSearchModel { ID = new[] { (int?)this.MeetingDocument.ID } });

        }

        /// <summary>
        /// 検討会資料の取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private MeetingDocumentModel GetMeetingDocument(MeetingDocumentSearchModel cond)
        {
            var list = new List<MeetingDocumentModel>();

            //APIで取得
            var res = HttpUtil.GetResponse<MeetingDocumentSearchModel, MeetingDocumentModel>(ControllerType.MeetingDocument, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list.FirstOrDefault();

        }
        #endregion
    }
}
