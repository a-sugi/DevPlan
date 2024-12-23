using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UIDevPlan.TruckSchedule.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// 各トラック予約画面
    /// </summary>
    public partial class TruckScheduleDetailForm : BaseSubForm
    {
        /// <summary>
        /// 画面名
        /// </summary>
        public override string FormTitle { get { return "各トラック予約"; } }

        /// <summary>
        /// 権限
        /// </summary>
        public UserAuthorityOutModel UserAuthority { get; private set; }

        /// <summary>
        /// スケジュール情報
        /// </summary>
        public ScheduleModel<TruckScheduleModel> Schedule { get; set; }

        /// <summary>
        /// スケジュール項目情報
        /// </summary>
        public TruckScheduleItemModel ScheduleItem { get; internal set; }

        /// <summary>
        /// よく使う目的地
        /// </summary>
        private List<FrequentlyUsedDestinations> destinationList = new List<FrequentlyUsedDestinations>();

        /// <summary>結果メッセージ</summary>
        public string ReturnMessage { get; set; } = string.Empty;

        /// <summary>CKリスト</summary>
        public List<TruckScheduleModel> CKList { get; internal set; }

        /// <summary>
        /// 管理者ユーザーかどうか
        /// </summary>
        private bool adminUser;

        /// <summary>
        /// 入力予約開始日時
        /// </summary>
        private DateTime InputStartDate
        {
            get
            {
                return DateTime.Parse(this.StartDayDateTimePicker.Value.ToString()).Date.AddHours(double.Parse(this.StartTimeComboBox.SelectedValue.ToString()));
            }
        }

        /// <summary>
        /// 入力予約終了日時
        /// </summary>
        private DateTime InputEndDate
        {
            get
            {
                return DateTime.Parse(this.EndDayDateTimePicker.Value.ToString()).Date.AddHours(double.Parse(this.EndTimeComboBox.SelectedValue.ToString()));
            }
        }

        /// <summary>選択可能スケジュール最大日</summary>
        public DateTime SelectMaxDate { private get; set; }

        /// <summary>選択可能スケジュール最小日</summary>
        public DateTime SelectMinDate { private get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TruckScheduleDetailForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckScheduleDetailForm_Load(object sender, EventArgs e)
        {
            this.UserAuthority = base.GetFunction(FunctionID.Truck);
            adminUser = this.UserAuthority.MANAGEMENT_FLG == '1';

            FreeTimeMultiRow.AllowAutoExtend = true;
            FreeTimeMultiRow.AllowClipboard = false;
            FreeTimeMultiRow.AllowUserToAddRows = false;
            FreeTimeMultiRow.AllowUserToDeleteRows = false;
            FreeTimeMultiRow.MultiSelect = false;
            FreeTimeMultiRow.VerticalScrollMode = ScrollMode.Pixel;
            FreeTimeMultiRow.SplitMode = SplitMode.None;

            FreeTimeMultiRow.ShortcutKeyManager.Unregister(Keys.Tab);
            FreeTimeMultiRow.ShortcutKeyManager.Register(new CustomMoveToNextControl(), Keys.Tab);

            SectionMultiRow.AllowAutoExtend = true;
            SectionMultiRow.AllowClipboard = false;
            SectionMultiRow.AllowUserToAddRows = false;
            SectionMultiRow.AllowUserToDeleteRows = false;
            SectionMultiRow.MultiSelect = false;
            SectionMultiRow.VerticalScrollMode = ScrollMode.Pixel;
            SectionMultiRow.SplitMode = SplitMode.None;

            SectionMultiRow.ShortcutKeyManager.Unregister(Keys.Tab);
            SectionMultiRow.ShortcutKeyManager.Register(new CustomMoveToNextControl(), Keys.Tab);
            
            this.CarNameLabel.Text = this.ScheduleItem.車両名;
            
            this.StartDayDateTimePicker.Value = this.Schedule.StartDate.Value.Date;
            this.EndDayDateTimePicker.Value = this.Schedule.EndDate.Value.Date;
            

            if (this.Schedule.ScheduleEdit == ScheduleEditType.Insert)
            {
                destinationList.Add(new FrequentlyUsedDestinations()
                {
                    Arrival = "",
                    Departure = ""
                });

                ComboBoxSetting.SetComboBox(ReservedPersonComboBox, SessionDto.UserId, UserInfo.GetUserSectionFullName(SessionDto.SectionCode, SessionDto.UserName));
            }
            else
            {
                ComboBoxSetting.SetComboBox(ReservedPersonComboBox, Schedule.Schedule.予約者_ID, Schedule.Schedule.予約者名);
                ComboBoxSetting.SetComboBox(DriverAComboBox, Schedule.Schedule.運転者A_ID, Schedule.Schedule.運転者A名);
                ComboBoxSetting.SetComboBox(DriverBComboBox, Schedule.Schedule.運転者B_ID, Schedule.Schedule.運転者B名);

                this.DriverATextBox.Text = Schedule.Schedule.運転者A_TEL;               
                this.DriverBTextBox.Text = Schedule.Schedule.運転者B_TEL;

                this.EditUserLabel.Text = Schedule.Schedule.修正者名 + "　" + Schedule.Schedule.予約修正日時.Value.ToString("yyyy/MM/dd HH:mm:ss");
                this.PurposeOfUseTextBox.Text = Schedule.Schedule.使用目的;

                YesRadioButton.Checked = Schedule.Schedule.FLAG_機密車 == 1;
                NoRadioButton.Checked = Schedule.Schedule.FLAG_機密車 == 0;

                this.StartTimeComboBox.SelectedItem = this.Schedule.StartDate.Value.Hour.ToString();
                this.EndTimeComboBox.SelectedItem = this.Schedule.EndDate.Value.Hour.ToString();

                for (int i = 1; i < Schedule.Schedule.SectionList.Count(); i++)
                {
                    destinationList.Add(new FrequentlyUsedDestinations()
                    {
                        Departure = Schedule.Schedule.SectionList[i - 1].発着地,
                        Arrival = Schedule.Schedule.SectionList[i].発着地,
                        EmptyCheck = Schedule.Schedule.SectionList[i - 1].FLAG_空荷 == 1 ? true : false
                    });
                }                
            }
            
            bool isEntry = true;

            if (this.Schedule.ScheduleEdit == ScheduleEditType.Insert)
            {
                this.DeleteButton.Visible = false;
            }
            else
            {
                var status = UserInfo.CheckScheduleEdit(this.Schedule.Schedule, this.UserAuthority);

                if (status.IsEdit == false && this.Schedule.ScheduleEdit != ScheduleEditType.Insert)
                {
                    isEntry = false;
                }
            }
            this.EntryButton.Visible = isEntry;
            
            //時間ドロップダウンの更新            
            List<ComboBoxSetting> srcStart = new List<ComboBoxSetting>();
            foreach (var time in Const.ScheduleDetailStartTimeList)
            {
                srcStart.Add(new ComboBoxSetting(time, time));
            }
            ComboBoxSetting.SetComboBox(StartTimeComboBox, srcStart);

            List<ComboBoxSetting> srcEnd = new List<ComboBoxSetting>();
            foreach (var time in Const.ScheduleDetailEndTimeList)
            {
                srcEnd.Add(new ComboBoxSetting(time, time));
            }
            ComboBoxSetting.SetComboBox(EndTimeComboBox, srcEnd);

            this.StartTimeComboBox.SelectedValue = this.Schedule.StartDate != null ? this.Schedule.StartDate.Value.ToString("HH") : "";
            this.EndTimeComboBox.SelectedValue = this.Schedule.EndDate != null ? this.Schedule.EndDate.Value.ToString("HH") : "";
            
            SetFreeTime();

            var res = HttpUtil.GetResponse<TruckSectionModel>(ControllerType.TruckSection, null);

            var list = new List<TruckSectionModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            foreach(var root in list)
            {
                this.sectionMultiRowTemplate1.DestinationComboBoxCell.Items.Add(root.行き先);
                this.sectionMultiRowTemplate1.PointOfDepartureComboBoxCell.Items.Add(root.行き先);
            }

            this.SectionMultiRow.Template = this.sectionMultiRowTemplate1;
            
            this.sectionMultiRowTemplate1.DestinationComboBoxCell.DataField = nameof(FrequentlyUsedDestinations.Arrival);
            this.sectionMultiRowTemplate1.PointOfDepartureComboBoxCell.DataField = nameof(FrequentlyUsedDestinations.Departure);
            this.sectionMultiRowTemplate1.EmptyCheckBoxCell.DataField = nameof(FrequentlyUsedDestinations.EmptyCheck);
            this.SectionMultiRow.Template = this.sectionMultiRowTemplate1;

            SectionMultiRow.DataSource = destinationList;

            SectionMultiRow.CellValueChanged += SectionMultiRow_CellValueChanged;

            if (adminUser == false)
            {
                this.DriverBComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                this.DriverAComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                this.ReservedPersonComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            CheckAddDeleteButtonEnabled();

            this.ActiveControl = ReservedPersonComboBox;
            this.FreeTimeMultiRow.ClearSelection();
            this.SectionMultiRow.ClearSelection();
            SendKeys.Send("{RIGHT}");

            if (!isEntry)
            {
                FormControlUtil.SetMaskingControls(this.DetailTableLayoutPanel, false);
            }
        }

        /// <summary>
        /// 運行区間グリッド変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionMultiRow_CellValueChanged(object sender, CellEventArgs e)
        {
            CheckAddDeleteButtonEnabled();
        }

        /// <summary>
        /// よく使う目的地ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrequentlyUsedDestinationsButton_Click(object sender, EventArgs e)
        {
            using (var form = new FrequentlyUsedDestinationsForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    this.sectionMultiRowTemplate1.DestinationComboBoxCell.DataField = nameof(FrequentlyUsedDestinations.Arrival);
                    this.sectionMultiRowTemplate1.PointOfDepartureComboBoxCell.DataField = nameof(FrequentlyUsedDestinations.Departure);
                    this.SectionMultiRow.Template = this.sectionMultiRowTemplate1;

                    this.destinationList.Clear();
                    this.destinationList = form.DestinationList;

                    SectionMultiRow.DataSource = this.destinationList;

                    CheckAddDeleteButtonEnabled();
                }
            }
        }

        /// <summary>
        /// 行追加ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowAddButton_Click(object sender, EventArgs e)
        {
            this.destinationList.Add(new FrequentlyUsedDestinations()
            {
                Departure = "",
                Arrival = ""
            });

            SectionMultiRow.DataSource = null;
            SectionMultiRow.DataSource = destinationList;

            if (this.SectionMultiRow.Rows.Count > 1)
            {
                var max = this.SectionMultiRow.Rows.Count - 1;
                this.SectionMultiRow.Rows[max].Cells[0].Value = this.SectionMultiRow.Rows[max - 1].Cells[3].Value;
                this.SectionMultiRow.Rows[max].Cells[0].ReadOnly = true;
            }

            CheckAddDeleteButtonEnabled();
        }

        /// <summary>
        /// 行削除ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowDeleteButton_Click(object sender, EventArgs e)
        {
            if (this.SectionMultiRow.CurrentCell == null || (this.SectionMultiRow.CurrentCell is FilteringTextBoxCell))
            {
                Messenger.Warn(Resources.KKM00009);
                return;
            }

            if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
            {
                return;
            }
            else
            {
                this.destinationList.RemoveAt(this.SectionMultiRow.CurrentCell.RowIndex);
                SectionMultiRow.DataSource = null;
                SectionMultiRow.DataSource = destinationList;

                CheckAddDeleteButtonEnabled();
            }
        }

        /// <summary>
        /// 行追加行削除ボタン変更処理
        /// </summary>
        private void CheckAddDeleteButtonEnabled()
        {
            bool isEdit = false;
            bool isDelete = false;
            var list = (List<FrequentlyUsedDestinations>)SectionMultiRow.DataSource;

            if (list.Count <= 1)
            {
                isDelete = false;
            }
            else
            {
                isDelete = true;
            }
        
            if (list.Count == 1 && string.IsNullOrWhiteSpace(list[0].Arrival) && string.IsNullOrWhiteSpace(list[0].Departure))
            {
                isEdit = false;
            }
            else
            {
                isEdit = true;
            }

            this.RowAddButton.Enabled = isEdit;
            this.RowDeleteButton.Enabled = isDelete;
        }

        /// <summary>
        /// 登録ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                if (IsEntrySchedule(this.Schedule.ScheduleEdit) == true)
                {
                    EntrySchedule(this.Schedule.ScheduleEdit);
                }
            });
        }

        private void EntrySchedule(ScheduleEditType type)
        {
            ResponseDto<TruckScheduleModel> res = null;

            TruckScheduleModel data = new TruckScheduleModel();
            
            data.予約者_ID = ((ComboBoxSetting)this.ReservedPersonComboBox.SelectedItem)?.Key;
            data.予約者名 = ComboBoxSetting.GetValue(ReservedPersonComboBox);

            data.運転者A_ID = ((ComboBoxSetting)this.DriverAComboBox.SelectedItem)?.Key;
            data.運転者A名 = ComboBoxSetting.GetValue(DriverAComboBox);            
            data.運転者A_TEL = this.DriverATextBox.Text;

            data.運転者B_ID = ((ComboBoxSetting)this.DriverBComboBox.SelectedItem)?.Key;
            data.運転者B名 = ComboBoxSetting.GetValue(DriverBComboBox);            
            data.運転者B_TEL = this.DriverBTextBox.Text;

            data.修正者名 = SessionDto.UserName;
            data.使用目的 = this.PurposeOfUseTextBox.Text;

            data.FLAG_機密車 = (YesRadioButton.Checked ? 1 : 0);

            data.予約開始時間 = this.InputStartDate;
            data.予約終了時間 = this.InputEndDate;


            //発着地の設定
            data.SectionList = new List<TruckScheduleSectionModel>();

            int sortno = 0;
            foreach (var item in destinationList)
            {
                sortno++;

                data.SectionList.Add(new TruckScheduleSectionModel()
                {
                    予約_ID = data.ID,
                    SORT_NO = sortno,
                    発着地 = item.Departure,
                    FLAG_空荷 = item.EmptyCheck == true ? 1 : 0
                });

                if (sortno == destinationList.Count)
                {
                    data.SectionList.Add(new TruckScheduleSectionModel()
                    {
                        予約_ID = data.ID,
                        SORT_NO = sortno + 1,
                        発着地 = item.Arrival,
                        FLAG_空荷 = 0
                    });
                }
            }

            data.トラック_ID = this.ScheduleItem.ID;
            data.予約修正日時 = DateTime.Now;
            data.修正者_ID = SessionDto.UserId;
            data.修正者名 = UserInfo.GetUserSectionFullName(SessionDto.SectionCode, SessionDto.UserName);

            data.PARALLEL_INDEX_GROUP = this.Schedule.RowNo;
            
            var timeList = (List<FreeTimeData>)this.FreeTimeMultiRow.DataSource;
            foreach (var time in timeList.Where(x => x.FreeTimeCheck == true))
            {
                data.空き時間状況 += time.FreeDateFormat + "|" + time.FreeTimeFormat + ",";
            }

            if (this.Schedule != null)
            {
                data.ID = this.Schedule.ID;
            }

            switch (type)
            {
                case ScheduleEditType.Insert:
                case ScheduleEditType.Paste:
                    res = HttpUtil.PostResponse(ControllerType.TruckSchedule, new[] { data });
                    this.Schedule.Schedule = res.Results.OfType<TruckScheduleModel>().FirstOrDefault();
                    break;

                case ScheduleEditType.Update:
                    res = HttpUtil.PutResponse(ControllerType.TruckSchedule, new[] { data });
                    break;

                case ScheduleEditType.Delete:                    
                    res = HttpUtil.DeleteResponse(ControllerType.TruckSchedule, new[] { data });
                    break;
            }

            if (res != null && res.Status == Const.StatusSuccess)
            {
                if (type != ScheduleEditType.Delete)
                {
                    Func<DateTime?, string> getYMDH = dt => { return dt != null ? ((DateTime)dt).ToString("yyyy/MM/dd H時") : string.Empty; };
                    ReturnMessage = string.Format(Resources.KKM01019, getYMDH(data.予約開始時間), getYMDH(data.予約終了時間), Resources.KKM00002);
                }
                else
                {
                    Messenger.Info(Resources.KKM00003);
                }
                base.FormOkClose();
            }
        }

        private bool IsEntrySchedule(ScheduleEditType scheduleEdit)
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            map[this.SectionMultiRow] = (c, name) =>
            {
                var errMsg = "";

                var list = (List<FrequentlyUsedDestinations>)this.SectionMultiRow.DataSource;
                if (list.Count <= 0)
                {
                    errMsg = string.Format(Resources.KKM00001, "運行区間");
                }

                return errMsg;
            };

            // Append Start 2021/01/26 杉浦
            map[this.EndTimeComboBox] = (c, name) =>
            {
                var errMsg = "";

                //期間Fromと期間Toがすべて入力してある場合のみチェック
                if (this.StartDayDateTimePicker.Value != null && this.StartTimeComboBox.Text != "" &&
                this.EndDayDateTimePicker.Value != null && this.EndTimeComboBox.Text != "")
                {
                    var start = this.StartDayDateTimePicker.SelectedDate.Value.AddHours(int.Parse(this.StartTimeComboBox.Text));
                    var end = this.EndDayDateTimePicker.SelectedDate.Value.AddHours(int.Parse(this.EndTimeComboBox.Text));

                    //開始日が終了日より大きい場合はエラー
                    if (start >= end)
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM00043;

                        //背景色を変更
                        this.StartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                        this.EndDayDateTimePicker.BackColor = Const.ErrorBackColor;
                        FormControlUtil.SetComboBoxBackColor(this.StartTimeComboBox, Const.ErrorBackColor);
                        FormControlUtil.SetComboBoxBackColor(this.EndTimeComboBox, Const.ErrorBackColor);
                    }
                }
                return errMsg;
            };
            // Append End 2021/01/26 杉浦

            var msg = Validator.GetFormInputErrorMessage(this, map);
            
            if (YesRadioButton.Checked == false && NoRadioButton.Checked == false)
            {
                msg = "機密車の" + Resources.KKM00009 + ((msg != "") ? Environment.NewLine + msg : "");
                YesRadioButton.BackColor = Const.ErrorBackColor;
                NoRadioButton.BackColor = Const.ErrorBackColor;
            }
            else
            {
                YesRadioButton.BackColor = Const.DefaultBackColor;
                NoRadioButton.BackColor = Const.DefaultBackColor;
            }

            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }

            var schedule = this.Schedule.Schedule;
            var checkList = GetSchedule(new TruckScheduleSearchModel()
            {
                TruckId = this.ScheduleItem.ID,
                START_DATE = this.InputStartDate,
                END_DATE = this.InputEndDate
            });
            
            if (adminUser == false && 
                (InputStartDate.Date < SelectMinDate || InputStartDate.Date > SelectMaxDate || InputEndDate.Date < SelectMinDate || InputEndDate.Date > SelectMaxDate))
            {
                //その項目に対し権限がない場合は３ヶ月制限実施。権限がある場合は先に予約までできるようにするため。
                Messenger.Warn(string.Format(Resources.KKM01009, 3));
                return false;
            }
            else if (checkList.Where(x => x.ID != schedule.ID && x.PARALLEL_INDEX_GROUP == this.Schedule.RowNo).Any(x =>
                (x.予約開始時間.Value.Date <= InputStartDate.Date && InputStartDate.Date <= x.予約終了時間.Value.Date) ||
                (x.予約開始時間.Value.Date <= InputEndDate.Date && InputEndDate.Date <= x.予約終了時間.Value.Date) ||
                (InputStartDate.Date <= x.予約開始時間.Value.Date && x.予約開始時間.Value.Date <= InputEndDate.Date) ||
                (InputStartDate.Date <= x.予約終了時間.Value.Date && x.予約終了時間.Value.Date <= InputEndDate.Date)) == true)
            {
                //同一行でスケジュールで重複した日付の期間が存在する場合はエラー
                Messenger.Warn(Resources.KKM03017);
                return false;
            }
            else if (checkList.Where(x => x.ID != schedule.ID).Any(x =>
                (x.予約開始時間 <= InputStartDate && InputStartDate < x.予約終了時間) || (x.予約開始時間 < InputEndDate && InputEndDate <= x.予約終了時間) ||
                (InputStartDate <= x.予約開始時間 && x.予約開始時間 < InputEndDate) || (InputStartDate < x.予約終了時間 && x.予約終了時間 <= InputEndDate)) == true)
            {
                //スケジュールで重複した期間が存在する場合はエラー
                Messenger.Warn(Resources.KKM03005);
                return false;
            }
            else if (CKList.Where(x => x.トラック_ID == schedule.トラック_ID).Any(x =>
                (x.予約開始時間 <= InputStartDate && InputStartDate < x.予約終了時間) || (x.予約開始時間 < InputEndDate && InputEndDate <= x.予約終了時間) ||
                (InputStartDate <= x.予約開始時間 && x.予約開始時間 < InputEndDate) || (InputStartDate < x.予約終了時間 && x.予約終了時間 <= InputEndDate)) == true)
            {
                //CKリストに存在する場合はエラー
                Messenger.Warn(Resources.KKM03043);
                return false;
            }

            return true;
        }

        /// <summary>
        /// スケジュールデータ取得処理
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        private List<TruckScheduleModel> GetSchedule(TruckScheduleSearchModel cond)
        {
            var res = HttpUtil.GetResponse<TruckScheduleSearchModel, TruckScheduleModel>(ControllerType.TruckSchedule, cond);

            var modelList = new List<TruckScheduleModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                modelList.AddRange(res.Results);
            }
            return modelList;
        }

        /// <summary>
        /// 削除ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                var status = UserInfo.CheckScheduleEdit(this.Schedule.Schedule, this.UserAuthority);
                if (status.IsDelete == false)
                {
                    Messenger.Info(Resources.KKM01015);
                    return;
                }

                if (MessageBox.Show(
                    string.Format(Resources.KKM02007,
                    this.Schedule.Schedule.車両名,
                    this.Schedule.Schedule.予約開始時間.Value.ToString("M/d（ddd）HH:ss"),
                    this.Schedule.Schedule.予約終了時間.Value.ToString("M/d（ddd）HH:ss")
                    ), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (IsEntrySchedule(ScheduleEditType.Delete) == true)
                    {
                        EntrySchedule(ScheduleEditType.Delete);
                    }
                }
            });
        }

        /// <summary>
        /// 予約者ドロップダウン時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservedPersonComboBox_DropDown(object sender, EventArgs e)
        {
            using (var form = new UserListForm { DepartmentCode = SessionDto.DepartmentCode,
                SectionCode = SessionDto.SectionCode, UserAuthority = this.UserAuthority, StatusCode = "a", IsSearchLimit = false })
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ComboBoxSetting.SetComboBox(ReservedPersonComboBox, form.User.PERSONEL_ID, UserInfo.GetUserSectionFullName(form.User.SECTION_CODE, form.User.NAME));                    
                }
                SendKeys.Send("{ENTER}");
            }
        }
        
        /// <summary>
        /// 運転者A変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DriverAComboBox_DropDown(object sender, EventArgs e)
        {
            using (var form = new UserListForm
            {
                DepartmentCode = SessionDto.DepartmentCode,
                SectionCode = SessionDto.SectionCode,
                UserAuthority = this.UserAuthority,
                StatusCode = "a",
                IsSearchLimit = false
            })
            {
                var result = form.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ComboBoxSetting.SetComboBox(DriverAComboBox, form.User.PERSONEL_ID, UserInfo.GetUserSectionFullName(form.User.SECTION_CODE, form.User.NAME));                    
                }
                SendKeys.Send("{ENTER}");
                if (result == DialogResult.OK)
                {
                    //Update Start 2021/10/14 矢作
                    //this.DriverATextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.TEL, form.User.PERSONEL_ID, form.User.NAME);
                    this.DriverATextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, form.User.PERSONEL_ID, form.User.NAME);
                    //Update End 2021/10/14 矢作
                }
            }
        }

        /// <summary>
        /// 運転者B変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DriverBComboBox_DropDown(object sender, EventArgs e)
        {
            using (var form = new UserListForm
            {
                DepartmentCode = SessionDto.DepartmentCode,
                SectionCode = SessionDto.SectionCode,
                UserAuthority = this.UserAuthority,
                StatusCode = "a",
                IsSearchLimit = false
            })
            {
                var result = form.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ComboBoxSetting.SetComboBox(DriverBComboBox, form.User.PERSONEL_ID, UserInfo.GetUserSectionFullName(form.User.SECTION_CODE, form.User.NAME));
                }
                SendKeys.Send("{ENTER}");
                if (result == DialogResult.OK)
                {
                    //Update Start 2021/10/14 矢作
                    //this.DriverBTextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.TEL, form.User.PERSONEL_ID, form.User.NAME);
                    this.DriverBTextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, form.User.PERSONEL_ID, form.User.NAME);
                    //Update End 2021/10/14 矢作
                }
            }
        }

        /// <summary>
        /// 開始日変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartDayDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SetFreeTime();
        }
      
        /// <summary>
        /// 終了日変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndDayDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SetFreeTime();
        }

        /// <summary>
        /// 開始時間変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartTimeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetFreeTime();
        }

        /// <summary>
        /// 終了時間変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndTimeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetFreeTime();
        }

        private void SetFreeTime()
        {
            //日付切り替え時処理。空き時間再算出処理を行う。
            if (StartTimeComboBox.SelectedValue != null && (string)StartTimeComboBox.SelectedValue != "" &&
               EndTimeComboBox.SelectedValue != null && (string)EndTimeComboBox.SelectedValue != "" &&
                this.StartDayDateTimePicker.SelectedDate != null && this.EndDayDateTimePicker.SelectedDate != null)
            {
                var startDate = this.StartDayDateTimePicker.SelectedDate.Value.
                    Date.AddHours(double.Parse(StartTimeComboBox.SelectedValue.ToString()));

                var endDate = this.EndDayDateTimePicker.SelectedDate.Value.
                    Date.AddHours(double.Parse(EndTimeComboBox.SelectedValue.ToString()) - 1);

                var list = new List<FreeTimeData>();

                for (DateTime date = startDate; date <= endDate; date = date.AddHours(1))
                {
                    if (date.Hour <= 22 && date.Hour >= 6)
                    {
                        list.Add(new FreeTimeData()
                        {
                            FreeDate = date,
                            FreeDateFormat = date.Date.ToString("M/d"),
                            FreeTimeFormat = date.ToShortTimeString()
                        });
                    }
                }

                //設定後に空き時間データがある場合チェックをいれる

                if (this.Schedule.Schedule != null && this.Schedule.Schedule.空き時間状況 != null)
                {
                    var timelist = this.Schedule.Schedule.空き時間状況.Split(',').ToList();

                    foreach (var time in timelist)
                    {
                        var d = time.Split('|');
                        foreach (var checkTime in list.Where(x => x.FreeDateFormat == d[0] && x.FreeTimeFormat == d[1]))
                        {
                            checkTime.FreeTimeCheck = true;
                        }
                    }
                }

                this.freeTimeMultiRowTemplate1.FreeTimeTimeTextBoxCell.DataField = nameof(FreeTimeData.FreeTimeFormat);
                this.freeTimeMultiRowTemplate1.FreeTimeTextBoxCell.DataField = nameof(FreeTimeData.FreeDateFormat);
                this.freeTimeMultiRowTemplate1.FreeTimeCheckBoxCell.DataField = nameof(FreeTimeData.FreeTimeCheck);
                this.FreeTimeMultiRow.Template = this.freeTimeMultiRowTemplate1;

                FreeTimeMultiRow.DataSource = list;
            }
        }

        /// <summary>
        /// 運行区間グリッド変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionMultiRow_CellEditedFormattedValueChanged(object sender, GrapeCity.Win.MultiRow.CellEditedFormattedValueChangedEventArgs e)
        {
            //コンボボックスチェンジイベント記載
            GcMultiRow gcMultiRow = sender as GcMultiRow;
            Cell currentCell = gcMultiRow.Rows[e.RowIndex].Cells[e.CellIndex];

            if (currentCell is ComboBoxCell && currentCell.Name == "DestinationComboBoxCell")
            {
                if (this.SectionMultiRow.Rows.Count > 1 && this.SectionMultiRow.Rows.Count - 1 > e.RowIndex)
                {
                    var max = e.RowIndex;

                    if (this.SectionMultiRow.Rows[max + 1].Cells[0].Value != currentCell.EditedFormattedValue)
                    {
                        this.SectionMultiRow.Rows[max + 1].Cells[0].ReadOnly = false;
                        this.SectionMultiRow.Rows[max + 1].Cells[0].Value = currentCell.EditedFormattedValue;
                        this.SectionMultiRow.Rows[max + 1].Cells[0].ReadOnly = true;
                    }
                }
                CheckAddDeleteButtonEnabled();
            }
        }
    }

    /// <summary>
    /// 空き時間格納クラス
    /// </summary>
    public class FreeTimeData
    {
        /// <summary>
        /// 空き時間
        /// </summary>
        public DateTime FreeDate { get; set; }

        /// <summary>
        /// 空き時間（MM/dd）
        /// </summary>
        public string FreeDateFormat { get; set; }

        /// <summary>
        /// 空き時間（時間）
        /// </summary>
        public string FreeTimeFormat { get; set; }

        /// <summary>
        /// 空き時間チェック
        /// </summary>
        public bool FreeTimeCheck { get; set; }
    }
}

