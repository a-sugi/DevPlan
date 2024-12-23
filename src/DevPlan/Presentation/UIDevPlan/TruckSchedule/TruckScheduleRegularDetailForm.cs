using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UIDevPlan.TruckSchedule.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Utils.Calendar.Templates;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevPlan.UICommon.Utils.Calendar;


namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// 定期便予約画面
    /// </summary>
    public partial class TruckScheduleRegularDetailForm : BaseSubForm
    {
        /// <summary>
        /// スケジュール情報
        /// </summary>
        internal ScheduleModel<TruckScheduleModel> Schedule;

        /// <summary>
        /// 画面名
        /// </summary>
        public override string FormTitle { get { return "定期便予約"; } }

        /// <summary>
        /// 権限
        /// </summary>
        public UserAuthorityOutModel UserAuthority { get; private set; }

        /// <summary>
        /// スケジュール項目情報
        /// </summary>
        public TruckScheduleItemModel ScheduleItem { get; internal set; }

        /// <summary>定期便発着地リスト</summary>
        public List<DeparturePoint> DeparturePointList { private get; set; }

        /// <summary>結果メッセージ</summary>
        public string ReturnMessage { get; set; } = string.Empty;

        /// <summary>
        /// 削除メッセージテキスト
        /// </summary>
        public string DeleteMessage { get;  set; }

        /// <summary>
        /// 定時マスタ
        /// </summary>
        public List<FixedTimeDaySettingModel> FixList { get; internal set; }

        /// <summary>
        /// 現在選択されているカレンダーのテンプレートモード
        /// </summary>
        public CalendarTemplateTypeSafeEnum CalendarMode { get; internal set; }

        /// <summary>
        /// 管理者ユーザーかどうか
        /// </summary>
        private bool adminUser;
        
        /// <summary>選択可能スケジュール最大日</summary>
        public DateTime SelectMaxDate { private get; set; }

        /// <summary>選択可能スケジュール最小日</summary>
        public DateTime SelectMinDate { private get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TruckScheduleRegularDetailForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckScheduleRegularDetailForm_Load(object sender, EventArgs e)
        {
            this.UserAuthority = base.GetFunction(FunctionID.Truck);
            adminUser = this.UserAuthority.MANAGEMENT_FLG == '1';

            ReceiptUserMultiRow.AllowAutoExtend = true;
            ReceiptUserMultiRow.AllowClipboard = false;
            ReceiptUserMultiRow.AllowUserToAddRows = false;
            ReceiptUserMultiRow.AllowUserToDeleteRows = false;
            ReceiptUserMultiRow.MultiSelect = false;
            ReceiptUserMultiRow.VerticalScrollMode = ScrollMode.Pixel;
            ReceiptUserMultiRow.SplitMode = SplitMode.None;

            ReceiptUserMultiRow.ShortcutKeyManager.Unregister(Keys.Tab);
            ReceiptUserMultiRow.ShortcutKeyManager.Register(new CustomMoveToNextControl(), Keys.Tab);

            ShippingMultiRow.AllowAutoExtend = true;
            ShippingMultiRow.AllowClipboard = false;
            ShippingMultiRow.AllowUserToAddRows = false;
            ShippingMultiRow.AllowUserToDeleteRows = false;
            ShippingMultiRow.MultiSelect = false;
            ShippingMultiRow.VerticalScrollMode = ScrollMode.Pixel;
            ShippingMultiRow.SplitMode = SplitMode.None;

            ShippingMultiRow.ShortcutKeyManager.Unregister(Keys.Tab);
            ShippingMultiRow.ShortcutKeyManager.Register(new CustomMoveToNextControl(), Keys.Tab);

            this.receiptUserMultiRowTemplate1.RecipientNameTextBoxCell.DataField = nameof(UserData.Name);
            this.receiptUserMultiRowTemplate1.RecipientTelTextBoxCell.DataField = nameof(UserData.Tel);
            this.shippingMultiRowTemplate1.ShippingTextBoxCell.DataField = nameof(UserData.Name);
            this.shippingMultiRowTemplate1.ShippingTelTextBoxCell.DataField = nameof(UserData.Tel);

            this.ReceiptUserMultiRow.Template = this.receiptUserMultiRowTemplate1;        
            this.ShippingMultiRow.Template = this.shippingMultiRowTemplate1;

            var receiptUserlist = new List<UserData>();
            var shippingUserlist = new List<UserData>();

            if(adminUser == false)
            {
                ReservationRadioButton.Enabled = false;
            }            

            //利用時間ドロップダウンの新規設定
            var headerRes = HttpUtil.GetResponse<TruckRegularTimeModel>(ControllerType.TruckRegularTime);
            var headerList = new List<TruckRegularTimeModel>();
            if (headerRes != null && headerRes.Status == Const.StatusSuccess)
            {
                headerList.AddRange(headerRes.Results);
            }

            List<ComboBoxSetting> src = new List<ComboBoxSetting>();
            foreach (var time in headerList.Where(x => x.IS_RESERVATION == 1 && x.REGULAR_ID == this.ScheduleItem.REGULAR_TIME_ID).OrderBy(x => x.TIME_ID))
            {
                src.Add(new ComboBoxSetting(time.TIME_ID.ToString(), time.DEPARTURE_TIME));
            }            
            ComboBoxSetting.SetComboBox(UtilizationTimeComboBox, src);

            UtilizationTimeNullableDateTimePicker.Value = this.Schedule.StartDate;
            
            TrackNameLabel.Text = this.ScheduleItem.車両名;

            if (Expansion2CalendarTemplate.timeHeaderList.Contains(this.Schedule.StartDate.Value.Hour) == false)
            {
                UtilizationTimeComboBox.SelectedValue = (this.Schedule.StartDate.Value.Hour + 1).ToString();
            }
            else
            {
                UtilizationTimeComboBox.SelectedValue = this.Schedule.StartDate.Value.Hour.ToString();
            }

            if (this.Schedule.ScheduleEdit == ScheduleEditType.Insert)
            {
                ComboBoxSetting.SetComboBox(ReservedPersonComboBox, SessionDto.UserId, UserInfo.GetUserSectionFullName(SessionDto.SectionCode, SessionDto.UserName));
                ComboBoxSetting.SetComboBox(RequesterNameComboBox, SessionDto.UserId, UserInfo.GetUserSectionFullName(SessionDto.SectionCode, SessionDto.UserName));

                //Update Start 2021/10/14 矢作
                //RequesterTelTextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.TEL, SessionDto.UserId, SessionDto.UserName);

                //var tel = new ADUtil().GetUserData(ADUtilTypeEnum.TEL, SessionDto.UserId, SessionDto.UserName);
                RequesterTelTextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, SessionDto.UserId, SessionDto.UserName);

                var tel = new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, SessionDto.UserId, SessionDto.UserName);
                //Update End 2021/10/14 矢作

                receiptUserlist.Add(new UserData()
                {
                    PersonelId = SessionDto.UserId,
                    Name = UserInfo.GetUserSectionFullName(SessionDto.SectionCode, SessionDto.UserName),
                    Tel = tel
                });

                shippingUserlist.Add(new UserData()
                {
                    PersonelId = SessionDto.UserId,
                    Name = UserInfo.GetUserSectionFullName(SessionDto.SectionCode, SessionDto.UserName),
                    Tel = tel
                });

                this.ReceiptUserMultiRow.DataSource = receiptUserlist;
                this.ShippingMultiRow.DataSource = shippingUserlist;

                this.DeleteButton.Visible = false;
                this.ReservedPersonComboBox.Enabled = false;                   
            }
            else
            {
                if (this.Schedule.Schedule.FLAG_仮予約 != 1)
                {
                    ReservationRadioButton.Checked = true;
                    TentativeReservationRadioButton.Checked = false;
                }
                else
                {
                    ReservationRadioButton.Checked = false;
                    TentativeReservationRadioButton.Checked = true;
                }

                ComboBoxSetting.SetComboBox(ReservedPersonComboBox, this.Schedule.Schedule.予約者_ID, this.Schedule.Schedule.予約者名);
                ComboBoxSetting.SetComboBox(RequesterNameComboBox, this.Schedule.Schedule.定期便依頼者_ID, this.Schedule.Schedule.定期便依頼者名);

                RequesterTelTextBox.Text = this.Schedule.Schedule.定期便依頼者_TEL;
                CarNameTextBox.Text = this.Schedule.Schedule.搬送車両名;
                ConfidentialComboBox.SelectedIndex = this.Schedule.Schedule.FLAG_機密車 == 1 ? 1 : 0;
                RemarksTextBox.Text = this.Schedule.Schedule.備考;
                EditorUserLabel.Text = this.Schedule.Schedule.修正者名 + "　" + this.Schedule.Schedule.予約修正日時.Value.ToString("yyyy/MM/dd HH:mm:ss");

                foreach (var user in Schedule.Schedule.ShipperRecipientUserList)
                {
                    if (string.IsNullOrWhiteSpace(user.受領者名) == false)
                    {
                        receiptUserlist.Add(new UserData()
                        {
                            PersonelId = user.受領者_ID,
                            Name = user.受領者名,
                            Tel = user.受領者_TEL
                        });
                    }

                    if (string.IsNullOrWhiteSpace(user.発送者名) == false)
                    {
                        shippingUserlist.Add(new UserData()
                        {
                            PersonelId = user.発送者_ID,
                            Name = user.発送者名,
                            Tel = user.発送者_TEL
                        });
                    }
                }

                this.ReceiptUserMultiRow.DataSource = receiptUserlist;
                this.ShippingMultiRow.DataSource = shippingUserlist;
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

            if (!isEntry)
            {
                FormControlUtil.SetMaskingControls(this.tableLayoutPanel1, false);
            }

            this.ActiveControl = ReservedPersonComboBox;
            this.ReceiptUserMultiRow.ClearSelection();
            this.ShippingMultiRow.ClearSelection();
            SendKeys.Send("{RIGHT}");

            this.ShippingMultiRow.CellContentButtonClick += ShippingMultiRow_CellContentButtonClick;
            this.ReceiptUserMultiRow.CellContentButtonClick += ReceiptUserMultiRow_CellContentButtonClick;
            this.ShippingMultiRow.CellValueChanged += ShippingMultiRow_CellValueChanged;
            this.ReceiptUserMultiRow.CellValueChanged += ReceiptUserMultiRow_CellValueChanged;

            if (adminUser == false)
            {
                this.ReservedPersonComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                this.RequesterNameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            CheckUserAddDeleteButtonEnabled(this.ReceiptUserMultiRow);
            CheckUserAddDeleteButtonEnabled(this.ShippingMultiRow);
        }

        /// <summary>
        /// 受領者グリッド編集時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiptUserMultiRow_CellValueChanged(object sender, CellEventArgs e)
        {
            // Append Start 2024 / 04 / 09 杉浦 定期便予約確認メールの仕様変更
            var userList = (List<UserData>)this.ShippingMultiRow.DataSource;
            //Append End 2024/04/09 杉浦　定期便予約確認メールの仕様変更
            if (e.CellName == receiptUserMultiRowTemplate1.RecipientNameTextBoxCell.Name)
            {
                //Append Start 2024/04/09 杉浦　定期便予約確認メールの仕様変更
                userList[e.RowIndex].PersonelId = null;
                this.ReceiptUserMultiRow.DataSource = userList;
                //Append End 2024/04/09 杉浦　定期便予約確認メールの仕様変更
                CheckUserAddDeleteButtonEnabled(ReceiptUserMultiRow);
            }
        }

        /// <summary>
        /// 発送者グリッド編集時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShippingMultiRow_CellValueChanged(object sender, CellEventArgs e)
        {
            //Append Start 2024/04/09 杉浦　定期便予約確認メールの仕様変更
            var userList = (List<UserData>)this.ShippingMultiRow.DataSource;
            //Append End 2024/04/09 杉浦　定期便予約確認メールの仕様変更
            if (e.CellName == shippingMultiRowTemplate1.ShippingTextBoxCell.Name)
            {
                // Append Start 2024 / 04 / 09 杉浦 定期便予約確認メールの仕様変更
                userList[e.RowIndex].PersonelId = null;
                this.ShippingMultiRow.DataSource = userList;
                //Append End 2024/04/09 杉浦　定期便予約確認メールの仕様変更
                CheckUserAddDeleteButtonEnabled(ShippingMultiRow);
            }
        }

        /// <summary>
        /// 行追加行削除ボタン変更処理
        /// </summary>
        /// <param name="grid">チェック対象のグリッド</param>
        private void CheckUserAddDeleteButtonEnabled(GcMultiRow grid)
        {
            bool isEdit = false;
            bool isDelete = false;
            var userList = (List<UserData>)grid.DataSource;

            if (userList.Count <= 1)
            {
                isDelete = false;
            }
            else
            {
                isDelete = true;
            }

            if (userList.Count == 1 && string.IsNullOrWhiteSpace(userList[0].Name))
            {
                isEdit = false;                
            }
            else
            {
                isEdit = true;
            }

            if (grid == ShippingMultiRow)
            {
                this.ShippingRowAddButton.Enabled = isEdit;
                this.ShippingRowDeleteButton.Enabled = isDelete;
            }
            else if (grid == ReceiptUserMultiRow)
            {
                this.ReceiptRowAddButton.Enabled = isEdit;
                this.ReceiptRowDeleteButton.Enabled = isDelete;
            }
        }

        /// <summary>
        /// 発送者グリッドフォーカスアウト処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShippingMultiRow_LostFocus(object sender, EventArgs e)
        {
            var list = ((List<UserData>)this.ShippingMultiRow.DataSource);

            if (list.Count <= 0 || string.IsNullOrWhiteSpace(list[0].Name))
            {
                ShippingRowAddButton.Enabled = false;
            }
            else
            {
                ShippingRowAddButton.Enabled = true;
            }
        }

        /// <summary>
        /// 受領者グリッドビューボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiptUserMultiRow_CellContentButtonClick(object sender, CellEventArgs e)
        {
            GcMultiRow gcMultiRow = sender as GcMultiRow;
            Cell currentCell = gcMultiRow.Rows[e.RowIndex].Cells[e.CellIndex];

            if (currentCell.Name == receiptUserMultiRowTemplate1.RecipientButtonCell.Name)
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
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        this.receiptUserMultiRowTemplate1.RecipientNameTextBoxCell.DataField = nameof(UserData.Name);
                        this.receiptUserMultiRowTemplate1.RecipientTelTextBoxCell.DataField = nameof(UserData.Tel);
                        this.ReceiptUserMultiRow.Template = this.receiptUserMultiRowTemplate1;

                        SetUser(form.User, this.ReceiptUserMultiRow, e.RowIndex);
                    }
                }
            }
        }

        /// <summary>
        /// 発送者グリッドビューボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShippingMultiRow_CellContentButtonClick(object sender, CellEventArgs e)
        {
            GcMultiRow gcMultiRow = sender as GcMultiRow;
            Cell currentCell = gcMultiRow.Rows[e.RowIndex].Cells[e.CellIndex];

            if (currentCell.Name == shippingMultiRowTemplate1.ShippingButtonCell.Name)
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
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        this.shippingMultiRowTemplate1.ShippingTextBoxCell.DataField = nameof(UserData.Name);
                        this.shippingMultiRowTemplate1.ShippingTelTextBoxCell.DataField = nameof(UserData.Tel);
                        this.ShippingMultiRow.Template = this.shippingMultiRowTemplate1;

                        SetUser(form.User, this.ShippingMultiRow, e.RowIndex);
                    }
                }
            }
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
                    if (this.Schedule.Schedule.FLAG_仮予約 != 1)
                    {
                        UserInfo.SetTruckManageUser();
                        Messenger.Info(string.Format(Resources.KKM01013, UserInfo.UserName, UserInfo.UserTel));
                        return;
                    }
                    else
                    {
                        Messenger.Info(Resources.KKM01014);
                        return;
                    }
                }

                if (MessageBox.Show(DeleteMessage, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (IsEntrySchedule(ScheduleEditType.Delete) == true)
                    {
                        EntrySchedule(ScheduleEditType.Delete);
                    }
                }
            });
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
                    var editSchedule = Schedule.Clone();                   
                    UpdateRegTime(editSchedule);
                    editSchedule.RowNo = Expansion2CalendarTemplate.timeHeaderList.IndexOf(editSchedule.StartDate.Value.Hour);

                    if (TruckData.UpdateScheduleCheck(editSchedule, new List<TruckScheduleModel>(), adminUser, SelectMaxDate, SelectMinDate) == false)
                    {
                        return;
                    }
                    
                    if (TruckData.CheckRegSchedule(DeparturePointList, FixList, CalendarMode, editSchedule, adminUser, true) == false)
                    {
                        return;
                    }
                    EntrySchedule(this.Schedule.ScheduleEdit);
                }
            });    
        }
        
        private void EntrySchedule(ScheduleEditType type)
        {
            ResponseDto<TruckScheduleModel> res = null;

            TruckScheduleModel data = new TruckScheduleModel();

            data.予約者_ID = ((ComboBoxSetting)ReservedPersonComboBox.SelectedItem)?.Key;
            data.予約者名 = ComboBoxSetting.GetValue(ReservedPersonComboBox);

            UpdateRegTime(this.Schedule);

            data.予約開始時間 = this.Schedule.StartDate;
            data.予約終了時間 = this.Schedule.EndDate;

            data.トラック_ID = this.ScheduleItem.ID;

            data.定期便依頼者_ID = ((ComboBoxSetting)RequesterNameComboBox.SelectedItem)?.Key;
            data.定期便依頼者名 = ComboBoxSetting.GetValue(RequesterNameComboBox);

            data.定期便依頼者_TEL = RequesterTelTextBox.Text;

            data.搬送車両名 = CarNameTextBox.Text;

            data.FLAG_機密車 = (ConfidentialComboBox.SelectedIndex == 1) ? 1 : 0;
            data.備考 = RemarksTextBox.Text;

            data.予約修正日時 = DateTime.Now;
            data.修正者_ID = SessionDto.UserId;
            data.修正者名 = UserInfo.GetUserSectionFullName(SessionDto.SectionCode, SessionDto.UserName);

            this.Schedule.RowNo = Expansion2CalendarTemplate.timeHeaderList.IndexOf(data.予約開始時間.Value.Hour);
            data.PARALLEL_INDEX_GROUP = this.Schedule.RowNo;
            data.FLAG_定期便 = 1;

            if (TentativeReservationRadioButton.Checked)
            {
                data.FLAG_仮予約 = 1;
            }
            else
            {
                data.FLAG_仮予約 = 0;
            }

            data.ShipperRecipientUserList = new List<TruckShipperRecipientUserModel>();
            var receiptUserList = (List<UserData>)this.ReceiptUserMultiRow.DataSource;
            var shippingUserlist = (List<UserData>)this.ShippingMultiRow.DataSource;
            var maxCount = (receiptUserList.Count > shippingUserlist.Count ? receiptUserList.Count : shippingUserlist.Count);

            int sortNo = 1;
            for (int i = 0; i < maxCount; i++)
            {
                var item = new TruckShipperRecipientUserModel()
                {
                    SORT_NO = sortNo,
                    予約_ID = data.ID
                };

                if (receiptUserList.Count > i)
                {
                    item.受領者_ID = receiptUserList[i].PersonelId;
                    item.受領者名 = receiptUserList[i].Name;
                    item.受領者_TEL = receiptUserList[i].Tel;
                }
                if (shippingUserlist.Count > i)
                {
                    item.発送者_ID = shippingUserlist[i].PersonelId;
                    item.発送者名 = shippingUserlist[i].Name;
                    item.発送者_TEL = shippingUserlist[i].Tel;
                }

                data.ShipperRecipientUserList.Add(item);

                sortNo++;
            }

            if (this.Schedule != null)
            {
                data.ID = this.Schedule.ID;
            }

            if (type != ScheduleEditType.Delete)
            {
                TruckData.IsZangyo(this.DeparturePointList, Schedule, int.Parse(UtilizationTimeComboBox.SelectedValue.ToString()), this.UserAuthority);
            }

            switch (type)
            {
                case ScheduleEditType.Insert:
                case ScheduleEditType.Paste:
                    res = HttpUtil.PostResponse(ControllerType.TruckSchedule, new[] { data });
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
                    if (data.FLAG_仮予約 == 1 && adminUser == false)
                    {
                        UserInfo.SetTruckManageUser();
                        Messenger.Info(string.Format(Resources.KKM01012, UserInfo.UserName, UserInfo.UserTel));
                    }

                    ReturnMessage = string.Format("定期便 {0}発のスケジュールを登録しました。",
                        ((ComboBoxSetting)UtilizationTimeComboBox.SelectedItem).Value);
                }
                else
                {
                    Messenger.Info(Resources.KKM00003);
                }
                this.Schedule.Schedule = res.Results.OfType<TruckScheduleModel>().FirstOrDefault();
                base.FormOkClose();
            }
        }

        /// <summary>
        /// 予約開始時終了時間は選択された時間帯から算出。（ドロップダウンで隠し持ったTIME_IDが開始時間。開始時間に＋２時間したのを終了時間とする。
        /// </summary>
        private void UpdateRegTime(ScheduleModel<TruckScheduleModel> updateSchedule)
        {
            var startTime = double.Parse(UtilizationTimeComboBox.SelectedValue.ToString());

            updateSchedule.StartDate = DateTime.Parse(UtilizationTimeNullableDateTimePicker.Value.ToString()).Date.AddHours(startTime);
            if(startTime + 2 == 24)
            {
                updateSchedule.EndDate = DateTime.Parse(UtilizationTimeNullableDateTimePicker.Value.ToString()).Date.AddHours(23).AddMinutes(59);
            }
            else
            {
                updateSchedule.EndDate = DateTime.Parse(UtilizationTimeNullableDateTimePicker.Value.ToString()).Date.AddHours(startTime + 2);
            }

        }

        private bool IsEntrySchedule(ScheduleEditType scheduleEdit)
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();
            
            map[this.ReceiptUserMultiRow] = (c, name) =>
            {
                var errMsg = "";

                var list = (List<UserData>)this.ReceiptUserMultiRow.DataSource;
                if (list.Count() <= 0)
                {
                    errMsg = string.Format(Resources.KKM00001, "受領者");
                }
                return errMsg;
            };

            map[this.ShippingMultiRow] = (c, name) =>
            {
                var errMsg = "";

                var list = (List<UserData>)this.ShippingMultiRow.DataSource;
                if (list.Count() <= 0)
                {
                    errMsg = string.Format(Resources.KKM00001, "発送者");
                }
                return errMsg;
            };

            var msg = Validator.GetFormInputErrorMessage(this, map);
            
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 受領者行追加処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiptRowAddButton_Click(object sender, EventArgs e)
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
                //ユーザー検索画面表示
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    this.receiptUserMultiRowTemplate1.RecipientNameTextBoxCell.DataField = nameof(UserData.Name);
                    this.receiptUserMultiRowTemplate1.RecipientTelTextBoxCell.DataField = nameof(UserData.Tel);
                    this.ReceiptUserMultiRow.Template = this.receiptUserMultiRowTemplate1;

                    SetUser(form.User, this.ReceiptUserMultiRow, ((List<UserData>)this.ReceiptUserMultiRow.DataSource).Count);
                }
            }
        }

        /// <summary>
        /// 受領者行削除処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiptRowDeleteButton_Click(object sender, EventArgs e)
        {
            if (this.ReceiptUserMultiRow.CurrentCell == null || (this.ReceiptUserMultiRow.CurrentCell is FilteringTextBoxCell))
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
                var list = (List<UserData>)this.ReceiptUserMultiRow.DataSource;

                list.RemoveAt(this.ReceiptUserMultiRow.CurrentCell.RowIndex);
                ReceiptUserMultiRow.DataSource = null;
                ReceiptUserMultiRow.DataSource = list;

                CheckUserAddDeleteButtonEnabled(ReceiptUserMultiRow);
            }
        }

        /// <summary>
        /// 発送者行追加処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShippingRowAddButton_Click(object sender, EventArgs e)
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
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    this.shippingMultiRowTemplate1.ShippingTextBoxCell.DataField = nameof(UserData.Name);
                    this.shippingMultiRowTemplate1.ShippingTelTextBoxCell.DataField = nameof(UserData.Tel);
                    this.ShippingMultiRow.Template = this.shippingMultiRowTemplate1;

                    SetUser(form.User, this.ShippingMultiRow, ((List<UserData>)this.ShippingMultiRow.DataSource).Count);
                }
            }
        }

        /// <summary>
        /// 発送者行削除処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShippingRowDeleteButton_Click(object sender, EventArgs e)
        {
            if (this.ShippingMultiRow.CurrentCell == null || (this.ShippingMultiRow.CurrentCell is FilteringTextBoxCell))
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
                var list = (List<UserData>)this.ShippingMultiRow.DataSource;

                list.RemoveAt(this.ShippingMultiRow.CurrentCell.RowIndex);
                ShippingMultiRow.DataSource = null;
                ShippingMultiRow.DataSource = list;

                CheckUserAddDeleteButtonEnabled(ShippingMultiRow);
            }
        }

        /// <summary>
        /// 予約者コンボボックスドロップ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservedPersonComboBox_DropDown(object sender, EventArgs e)
        {
            //権限はロードのときに。
            this.UserAuthority = base.GetFunction(FunctionID.Truck);
            
            using (var form = new UserListForm
            {
                DepartmentCode = SessionDto.DepartmentCode,
                SectionCode = SessionDto.SectionCode,
                UserAuthority = this.UserAuthority,
                StatusCode = "a",
                IsSearchLimit = false
            })
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ComboBoxSetting.SetComboBox(ReservedPersonComboBox, form.User.PERSONEL_ID, UserInfo.GetUserSectionFullName(form.User.SECTION_CODE, form.User.NAME));
                }
                SendKeys.Send("{ENTER}");
            }
        }

        /// <summary>
        /// 依頼者コンボボックスドロップ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RequesterNameComboBox_DropDown(object sender, EventArgs e)
        {
            //権限はロードのときに。
            this.UserAuthority = base.GetFunction(FunctionID.Truck);
            
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
                    ComboBoxSetting.SetComboBox(RequesterNameComboBox, form.User.PERSONEL_ID, UserInfo.GetUserSectionFullName(form.User.SECTION_CODE, form.User.NAME));              
                }
                SendKeys.Send("{ENTER}");
                if (result == DialogResult.OK)
                {
                    //Update Start 2021/10/14 矢作
                    //RequesterTelTextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.TEL, form.User.PERSONEL_ID, form.User.NAME);
                    RequesterTelTextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, form.User.PERSONEL_ID, form.User.NAME);
                    //Update End 2021/10/14 矢作
                }
            }

        }

        /// <summary>
        /// ユーザー検索画面から取得したユーザー情報をグリッドへ展開
        /// </summary>
        /// <param name="user">ユーザー情報</param>
        /// <param name="grid">設定対象グリッドオブジェクト</param>
        /// <param name="editRowNo">編集対象行インデックス</param>
        private void SetUser(UserSearchOutModel user, GcMultiRow grid, int editRowIndex)
        {
            //Update Start 2021/10/14 矢作
            //string tel = new ADUtil().GetUserData(ADUtilTypeEnum.TEL, user.PERSONEL_ID, user.NAME);
            string tel = new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, user.PERSONEL_ID, user.NAME);
            //Update End 2021/10/14 矢作

            var userlist = new List<UserData>();

            if (grid.DataSource != null)
            {
                userlist.AddRange((List<UserData>)grid.DataSource);
            }
            var name = UserInfo.GetUserSectionFullName(user.SECTION_CODE, user.NAME);
            if (userlist.Count > editRowIndex)
            {
                userlist[editRowIndex].PersonelId = user.PERSONEL_ID;
                userlist[editRowIndex].Name = name;
                userlist[editRowIndex].Tel = tel;
            }
            else
            {
                userlist.Add(new UserData()
                {
                    PersonelId = user.PERSONEL_ID,
                    Name = name,
                    Tel = tel
                });
            }

            grid.DataSource = userlist;

            grid.Rows[editRowIndex].Cells[1].Selected = true;
            grid.BeginEdit(false);

            CheckUserAddDeleteButtonEnabled(grid);
        }
    }
    
    /// <summary>
    /// グリッドに設定するユーザー情報格納クラス。
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// ID
        /// </summary>
        public string PersonelId { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string Tel { get; set; }
    }    
}
