using DevPlan.Presentation.Base;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// メール原文修正画面
    /// </summary>
    public partial class RegularMailDetailForm : BaseSubForm
    {
        /// <summary>
        /// 画面名
        /// </summary>
        public override string FormTitle { get { return "メール原文修正"; } }

        /// <summary>
        /// スケジュール項目
        /// </summary>
        public ScheduleItemModel<TruckScheduleItemModel> ScheduleItem { get; internal set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RegularMailDetailForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegularMailDetailForm_Load(object sender, EventArgs e)
        {
            this.CarNameLabel.Text = ScheduleItem.ScheduleItem.車両名;

            var res = HttpUtil.GetResponse<RegularMailDetailMasterModel>(ControllerType.RegularMailDetailMaster, null);
            var list = new List<RegularMailDetailMasterModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }
            this.regularMailDetailMultiRowTemplate1.RegularMailTextBoxCell.DataField = nameof(RegularMailDetailMasterModel.文字列);
            this.RegularMailDetailMultiRow.Template = this.regularMailDetailMultiRowTemplate1;            
            this.RegularMailDetailMultiRow.DataSource = list;
            
            List<ComboBoxSetting> src = new List<ComboBoxSetting>();
            src.Add(new ComboBoxSetting(ReservationStautsEnum.KARI_YOYAKU.ShortName, ReservationStautsEnum.KARI_YOYAKU.Key));
            src.Add(new ComboBoxSetting(ReservationStautsEnum.HON_YOYAKU.ShortName, ReservationStautsEnum.HON_YOYAKU.Key));
            ComboBoxSetting.SetComboBox(TypeComboBox, src);
            
            this.LoadButton.Click += GetMail;
            this.TypeComboBox.SelectionChangeCommitted += GetMail;
            
            this.ActiveControl = TypeComboBox;

            GetMail(null, null);
        }

        /// <summary>
        /// 登録ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            var msg = GetErrorMessage();
            if (msg != "")
            {
                Messenger.Warn(msg);
            }
            else
            {
                var val = new RegularMailDetailModel
                {
                    TRACK_ID = ScheduleItem.ScheduleItem.ID,
                    予約種別 = ReservationStautsEnum.ShortNameOf(TypeComboBox.SelectedValue.ToString()).ShortName,
                    件名 = this.MailTitleTextBox.Text,
                    本文 = this.MailContentTextBox.Text
                };

                var res = HttpUtil.PutResponse<RegularMailDetailModel>(ControllerType.RegularMailDetail, val);
                if (res == null || res.Status != Const.StatusSuccess)
                {
                    Messenger.Warn(res.ErrorMessage);
                }
                else
                {
                    Messenger.Info(Resources.KKM00002);
                }
            }
        }

        /// <summary>
        /// メール情報取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetMail(object sender, EventArgs e)
        {
            var cond = new RegularMailDetailSearchModel()
            {
                RegularType = ReservationStautsEnum.ShortNameOf(TypeComboBox.SelectedValue.ToString()).ShortName,
                TrackId = ScheduleItem.ID
            };
            var res = HttpUtil.GetResponse<RegularMailDetailSearchModel, RegularMailDetailModel>(ControllerType.RegularMailDetail, cond);

            var list = new List<RegularMailDetailModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

                string mailContent = "";
                foreach (var item in list)
                {
                    mailContent += item.本文;
                }

                this.MailTitleTextBox.Text = list[0].件名;
                this.MailContentTextBox.Text = mailContent.Replace("\n", "\r\n");
            }
            else
            {
                Messenger.Warn(res.ErrorMessage);
            }

            GetErrorMessage();
        }

        /// <summary>
        /// メール原文修正画面入力チェックおよびコントロール色リセット
        /// </summary>
        private string GetErrorMessage()
        {
            return Validator.GetFormInputErrorMessage(this);
        }

        /// <summary>
        /// グリッドビュー編集状態イベント
        /// </summary>
        /// <remarks>
        /// テキストボックスが編集状態となった場合、ReadOnlyとします。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegularMailDetailMultiRow_EditingControlShowing(object sender, GrapeCity.Win.MultiRow.EditingControlShowingEventArgs e)
        {
            if (e.Control is TextBoxEditingControl)
            {
                TextBoxEditingControl editor = (TextBoxEditingControl)e.Control;
                editor.ReadOnly = true;
            }
        }
    }
}
