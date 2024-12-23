using DevPlan.Presentation.Base;
using DevPlan.Presentation.UIDevPlan.TruckSchedule.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// 定期便予約確認メール画面
    /// </summary>
    public partial class RegularMailForm : BaseSubForm
    {
        /// <summary>
        /// 送信メール情報リスト
        /// </summary>
        private List<RegularMail> MailDatalist;

        /// <summary>
        /// 画面名
        /// </summary>
        public override string FormTitle { get { return "定期便予約確認メール"; } }

        /// <summary>
        /// メール送信対象積込日
        /// </summary>
        public DateTime RegularDate { private get; set; }

        /// <summary>
        /// スケジュール情報
        /// </summary>
        private List<TruckScheduleModel> ScheduleData { get; set; }

        /// <summary>
        /// 検索トラックID
        /// </summary>
        public long TruckId { private get; set; }

        /// <summary>
        /// 発着地文言リスト
        /// </summary>
        internal List<DeparturePoint> DeparturePointList { get; set; }

        /// <summary>
        /// ADUtil内部保持フィールド
        /// </summary>
        private ADUtil adUtil;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RegularMailForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegularMailForm_Load(object sender, EventArgs e)
        {
            this.adUtil = new ADUtil();

            #region multirow

            RegularMailMultiRow.AllowAutoExtend = true;
            RegularMailMultiRow.AllowClipboard = false;
            RegularMailMultiRow.AllowUserToAddRows = false;
            RegularMailMultiRow.AllowUserToDeleteRows = false;
            RegularMailMultiRow.MultiSelect = false;
            RegularMailMultiRow.VerticalScrollMode = ScrollMode.Pixel;
            RegularMailMultiRow.SplitMode = SplitMode.None;

            RegularMailMultiRow.ShortcutKeyManager.Unregister(Keys.Tab);
            RegularMailMultiRow.ShortcutKeyManager.Register(new CustomMoveToNextControl(), Keys.Tab);

            #endregion

            this.LoadingDateLabel2.Text = this.RegularDate.ToString("yyyy/MM/dd");

            var cond = new TruckScheduleSearchModel()
            {
                START_DATE = this.RegularDate,
                END_DATE = this.RegularDate,
                IsRegular = true,
                TruckId = this.TruckId
            };
            var res = HttpUtil.GetResponse<TruckScheduleSearchModel, TruckScheduleModel>(ControllerType.TruckSchedule, cond);

            ScheduleData = new List<TruckScheduleModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                ScheduleData.AddRange(res.Results);
            }
            
            this.regularMailMultiRowTemplate1.LoadingTimeTextBoxCell.DataField = nameof(RegularMail.LoadingTime);
            this.regularMailMultiRowTemplate1.TransportVehicleNameTextBoxCell.DataField = nameof(RegularMail.TransportVehicleName);
            this.regularMailMultiRowTemplate1.ShipperTextBoxCell.DataField = nameof(RegularMail.Shipper);
            this.regularMailMultiRowTemplate1.RecipientTextBoxCell.DataField = nameof(RegularMail.Recipient);
            this.regularMailMultiRowTemplate1.MailCheckBoxCell.DataField = nameof(RegularMail.Check);

            DynamicCellStyle dynamicCellStyle1 = new DynamicCellStyle();
            dynamicCellStyle1.ConditionHandler = new DynamicCellStyleConditionHandler(MyCondition);
            this.regularMailMultiRowTemplate1.Row.DefaultCellStyle = dynamicCellStyle1;
            
            this.RegularMailMultiRow.Template = this.regularMailMultiRowTemplate1;

            MailDatalist = new List<RegularMail>();

            foreach (var item in ScheduleData)
            {
                string hassousya = "";
                string juryousya = "";
                foreach (var users in item.ShipperRecipientUserList)
                {
                    if (string.IsNullOrWhiteSpace(users.発送者名) == false)
                    {
                        if (hassousya != "") { hassousya += Environment.NewLine; }
                        hassousya += users.発送者名 + "(" + users.発送者_TEL + ") ";
                    }
                    if (string.IsNullOrWhiteSpace(users.受領者名) == false)
                    {
                        if (juryousya != "") { juryousya += Environment.NewLine; }
                        juryousya += users.受領者名 + "(" + users.受領者_TEL + ") ";
                    }
                }

                MailDatalist.Add(new RegularMail()
                {
                    LoadingTime = DeparturePointList.Where(
                        x => x.DepartureTime == item.DEPARTURE_TIME && x.TruckId == item.トラック_ID).First().Text1 + " " + item.DEPARTURE_TIME + "発",
                    TransportVehicleName = item.搬送車両名,
                    Shipper = hassousya,
                    Recipient = juryousya,
                    Check = true,
                    Data = item,
                    Type = item.FLAG_仮予約
                });
            }

            RegularMailMultiRow.DataSource = MailDatalist;            
            for (int i = 0; i < this.RegularMailMultiRow.RowCount; i++)
            {
                RegularMailMultiRow.Rows[i][0].PerformVerticalAutoFit();                
            }

            this.ActiveControl = OpenMailButton;
        }

        /// <summary>
        /// グリッド行色変更処理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private CellStyle MyCondition(DynamicCellStyleContext context)
        {
            var cellStyle1 = new CellStyle();
            if (MailDatalist[context.RowIndex].Type == 1)
            {
                cellStyle1.BackColor = Color.LightGray;
            }
            else
            {
                cellStyle1.BackColor = Color.White;
            }
            return cellStyle1;
        }

        /// <summary>
        /// 送信確認クリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMailButton_Click(object sender, EventArgs e)
        {
            var processDataList = new List<SendRegularMail>();
            var gridData = (List<RegularMail>)RegularMailMultiRow.DataSource;
            
            foreach (var mailItem in gridData.Where (x=>x.Check))
            {
                var schedule = mailItem.Data;
                var cond = new RegularMailDetailSearchModel();

                cond.RegularType = ReservationStautsEnum.FlagOf(schedule.FLAG_仮予約.ToString()).ShortName;
                cond.TrackId = schedule.トラック_ID;

                var res = HttpUtil.GetResponse<RegularMailDetailSearchModel, RegularMailDetailModel>(ControllerType.RegularMailDetail, cond);

                var list = new List<RegularMailDetailModel>();
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    list.AddRange(res.Results);
                }

                string mailContent = "";
                foreach (var item in list)
                {
                    mailContent += item.本文;
                }

                //発送者受領者作成
                var sendUserList = new List<string>();
                var shipperUserList = new List<string>();
                var recipientUserList = new List<string>();

                foreach (var users in schedule.ShipperRecipientUserList)
                {
                    if (sendUserList.Contains(users.発送者名) == false && string.IsNullOrWhiteSpace(users.発送者名) == false)
                    {
                        GetUserMail(sendUserList, users.発送者_ID);
                        shipperUserList.Add(users.発送者名);
                    }

                    if (sendUserList.Contains(users.受領者名) == false && string.IsNullOrWhiteSpace(users.受領者名) == false)
                    {
                        GetUserMail(sendUserList, users.受領者_ID);
                        recipientUserList.Add(users.受領者名);
                    }
                }

                if (sendUserList.Contains(schedule.予約者名) == false && string.IsNullOrWhiteSpace(schedule.予約者名) == false)
                {
                    GetUserMail(sendUserList, schedule.予約者_ID);
                }

                if (sendUserList.Contains(schedule.定期便依頼者名) == false && string.IsNullOrWhiteSpace(schedule.定期便依頼者名) == false)
                {
                    GetUserMail(sendUserList, schedule.定期便依頼者_ID);
                }

                processDataList.Add(new SendRegularMail()
                {
                    To = string.Join(";", sendUserList),
                    Title = list[0].件名,
                    Content = mailContent.Replace("\n", "%0D%0A").Replace("\r\n", "%0D%0A")
                    .Replace("＆積込日＆", schedule.予約開始時間.Value.ToString("yy/MM/dd"))
                    .Replace("＆積込時間＆", schedule.DEPARTURE_TIME)
                    .Replace("＆積込場所＆", DeparturePointList.Where(x => x.DepartureTime == schedule.DEPARTURE_TIME && x.TruckId == schedule.トラック_ID).First().Text1)
                    .Replace("＆搬送車両名＆", schedule.GetFullCarName())
                    .Replace("＆発送者＆", string.Join(",", shipperUserList))
                    .Replace("＆受領者＆", string.Join(",", recipientUserList))
                    .Replace("＆連絡先課名＆", SessionDto.SectionName)
                    .Replace("＆連絡先氏名＆", SessionDto.UserName)
                    .Replace("＆発信者課名＆", SessionDto.SectionName)
                    .Replace("＆発信者氏名＆", SessionDto.UserName)
                });
            }

            foreach(var mail in processDataList)
            {
                Process.Start(string.Format("mailto:{0}?subject={1}&body={2}", mail.To, mail.Title, mail.Content));
            }
        }

        private void GetUserMail(List<string> addList, string personelId)
        {
            //Append Start 2024/04/09 杉浦　定期便予約確認メールの仕様変更
            if (personelId != null)
            {
                //Append End 2024/04/09 杉浦　定期便予約確認メールの仕様変更
                var res = HttpUtil.GetResponse<UserSearchModel, UserSearchOutModel>
                    (ControllerType.User, new UserSearchModel() { PERSONEL_ID = personelId });
                var list = new List<UserSearchOutModel>();
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    list.AddRange(res.Results);
                }

                var userMail = this.adUtil.GetUserData(ADUtilTypeEnum.MAIL, personelId, list.FirstOrDefault().NAME);

                if (addList.Contains(userMail) == false)
                {
                    addList.Add(userMail);
                }
                //Append Start 2024/04/09 杉浦　定期便予約確認メールの仕様変更
            }
            //Append End 2024/04/09 杉浦　定期便予約確認メールの仕様変更
        }

    }

    /// <summary>
    /// メール送信情報格納クラス
    /// </summary>
    public class SendRegularMail
    {
        /// <summary>
        /// 件名
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 本文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 宛先
        /// </summary>
        public string To { get; set; }
    }


    /// <summary>
    /// 定期便予約確認メール値保持クラス
    /// </summary>
    public class RegularMail
    {
        /// <summary>
        /// チェック
        /// </summary>
        public bool Check { get; set; }

        /// <summary>
        /// 発送者
        /// </summary>
        public string Shipper { get; set; }

        /// <summary>
        /// 積込時間
        /// </summary>
        public string LoadingTime { get; set; }

        /// <summary>
        /// 受領者
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// 搬送車両名
        /// </summary>
        public string TransportVehicleName { get; set; }

        /// <summary>
        /// スケジュールデータ
        /// </summary>
        public TruckScheduleModel Data { get; set; }

        /// <summary>
        /// 仮予約フラグ
        /// </summary>
        public int Type { get; set; }
    }
}
