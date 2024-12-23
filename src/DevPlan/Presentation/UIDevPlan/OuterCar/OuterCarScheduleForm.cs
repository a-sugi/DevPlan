using DevPlan.Presentation.Base;
using DevPlan.Presentation.UC;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.OuterCar
{
    /// <summary>
    /// 外製車日程スケジュール詳細
    /// </summary>
    public partial class OuterCarScheduleForm : BaseSubForm
    {
        #region メンバ変数
        private const string RentalOKAddTitle = "（空き時間貸出可）";

        private Func<DateTime?, string, DateTime?> getDateTime = (dt, time) => dt == null ? null : (DateTime?)dt.Value.AddHours(int.Parse(time == string.Empty ? "0" : time));

        private Func<DateTime?, string> getYMDH = dt => { return dt != null ? ((DateTime)dt).ToString("yyyy/MM/dd H時") : string.Empty; };

        /// <summary>スケジュール項目情報</summary>
        private OuterCarScheduleItemGetOutModel Item { get; set; }
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "スケジュール詳細（外製車）"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>スケジュール情報</summary>
        public ScheduleModel<OuterCarScheduleGetOutModel> Schedule { get; set; }

        /// <summary>管理権限</summary>
        public bool IsManagement { get; set; }

        /// <summary>更新権限</summary>
        public bool IsUpdate { get; set; }

        /// <summary>結果メッセージ</summary>
        public string ReturnMessage { get; set; } = string.Empty;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OuterCarScheduleForm()
        {
            InitializeComponent();

        }
        #endregion

        #region 画面ロード
        /// <summary>
        /// OuterCarDetailForm_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuterCarDetailForm_Load(object sender, EventArgs e)
        {
            // 初期化
            this.Init();

        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            //初期表示フォーカス
            this.ActiveControl = PurposeComboBox;

            if (this.Schedule == null) return;

            if (!this.Schedule.ScheduleEdit.Equals(ScheduleEditType.Update))
            {
                this.DeleteButton.Visible = false;
            }

            this.Item = ScheduleItemGet(this.Schedule.CategoryID);

            if (!string.IsNullOrWhiteSpace(this.Item.管理票NO))
            {
                this.CarNameLabel.Text = this.Item.メーカー名 + " " + this.Item.外製車名 + "（" + this.Item.管理票NO + "）";
            }

            // 区分
            FormControlUtil.SetRadioButtonValue(this.TypePanel, Schedule.Status);

            // タイトル

            // 開始日
            this.StartTimeComboBox.Items.Clear();
            this.StartTimeComboBox.Items.AddRange(Const.ScheduleDetailStartTimeList);

            var startDate = Schedule.StartDate;
            if (startDate != null)
            {
                this.StartTimeComboBox.Text = startDate.Value.ToString("HH");
                this.StartDayDateTimePicker.Value = startDate.Value.Date;
            }
            else
            {
                this.StartTimeComboBox.SelectedIndex = 0;
                this.StartDayDateTimePicker.Value = startDate;
            }

            // 終了日
            this.EndTimeComboBox.Items.Clear();
            this.EndTimeComboBox.Items.AddRange(Const.ScheduleDetailEndTimeList);

            var endDate = Schedule.EndDate;
            if (endDate != null)
            {
                this.EndTimeComboBox.Text = endDate.Value.ToString("HH");
                this.EndDayDateTimePicker.Value = endDate.Value.Date;
            }
            else
            {
                this.EndTimeComboBox.SelectedIndex = 0;
                this.EndDayDateTimePicker.Value = endDate;
            }

            // 目的
            FormControlUtil.SetComboBoxItem(this.PurposeComboBox, GetPurposeList());
            this.PurposeComboBox.SelectedIndex = -1;
            this.PurposeComboBox.TextChanged += this.PurposeComboBox_TextChanged;
            this.PurposeComboBox.Text = Schedule.Schedule?.目的;

            // 行先
            FormControlUtil.SetComboBoxItem(this.DestinationComboBox, GetDestinationList());
            this.DestinationComboBox.SelectedIndex = -1;
            this.DestinationComboBox.TextChanged += this.DestinationComboBox_TextChanged;
            this.DestinationComboBox.Text = Schedule.Schedule?.行先;

            // 使用者TEL
            this.TelTextBox.Text = Schedule.Schedule?.TEL;

            // 空き時間貸出（0：不可、1：可）
            this.RentalNGRadioButton.Checked = true;
            if (Schedule.Schedule?.FLAG_空時間貸出可 == 1)
                this.RentalOKRadioButton.Checked = true;

            //新規登録かどうか
            if (this.Schedule.ScheduleEdit == ScheduleEditType.Insert)
            {
                //本予約
                this.ReservationRadioButton.Checked = (this.Item?.FLAG_要予約許可 ?? 0) == 0;

                //管理権限がある場合は本予約をデフォルトとする。
                if (this.IsManagement)
                {
                    this.ReservationRadioButton.Checked = true;
                }

            }
            else
            {
                //ステータス
                FormControlUtil.SetRadioButtonValue(this.ReservationPanel, this.Schedule.Schedule.予約種別);

            }

            // 予約者
            if (this.Schedule.ScheduleEdit.Equals(ScheduleEditType.Update))
            {
                //Update Start 2021/10/14 矢作
                //this.ReservedPersonLabel.Text = string.Format("{0} {1}", this.Schedule.Schedule?.予約者_SECTION_CODE, this.Schedule.Schedule?.予約者_NAME);
                string mobileNo = new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, this.Schedule.Schedule?.予約者_ID, this.Schedule.Schedule?.予約者_NAME);

                this.ReservedPersonLabel.Text = string.Format("{0} {1}", this.Schedule.Schedule?.予約者_SECTION_CODE, this.Schedule.Schedule?.予約者_NAME) + 
                    "　" + mobileNo;

                this.TelTextBox.Text = string.IsNullOrEmpty(Schedule.Schedule?.TEL) ? mobileNo : Schedule.Schedule?.TEL;
                //Update End 2021/10/14 矢作
            }
            else
            {
                //Update Start 2021/10/14 矢作
                //this.ReservedPersonLabel.Text = string.Format("{0} {1}", SessionDto.SectionCode, SessionDto.UserName);
                string mobileNo = new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, SessionDto.UserId, SessionDto.UserName);

                this.ReservedPersonLabel.Text = string.Format("{0} {1}", SessionDto.SectionCode, SessionDto.UserName) +
                    "　" + mobileNo;

                this.TelTextBox.Text = mobileNo;
                //Update End 2021/10/14 矢作
            }

            // 駐車場番号
            this.ParkingNoLabel.Text = this.Item.駐車場番号;

            // 管理権限
            if (this.IsManagement == false)
            {
                this.ReservationRadioButton.Enabled = false;
                this.TentativeReservationRadioButton.Enabled = false;
            }

            // 更新権限
            if (this.IsUpdate == false)
            {
                this.EntryButton.Visible = false;
                this.DeleteButton.Visible = false;
            }

            //更新権限ありで管理権限なしかどうか
            if (IsUpdate == true && IsManagement == false)
            {
                //新規追加時以外で管理権限が無ければ他のユーザーのスケジュールは操作不可
                if (this.Schedule.ScheduleEdit != ScheduleEditType.Insert && this.Schedule.Schedule.予約者_ID != SessionDto.UserId)
                {
                    this.EntryButton.Visible = false;
                    this.DeleteButton.Visible = false;

                }

            }

            //編集不可の場合はボタンを表示しない。
            if (this.Schedule.IsEdit == false && this.Schedule.ScheduleEdit != ScheduleEditType.Insert)
            {
                this.EntryButton.Visible = false;
                this.DeleteButton.Visible = false;
            }

            // 編集不可の場合
            if (!this.EntryButton.Visible)
            {
                // 全入力項目の非活性
                FormControlUtil.SetMaskingControls(this.DetailTableLayoutPanel, false);
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
            var start = getYMDH(getDateTime(this.StartDayDateTimePicker.SelectedDate, this.StartTimeComboBox.Text));
            var end = getYMDH(getDateTime(this.EndDayDateTimePicker.SelectedDate, this.EndTimeComboBox.Text));

            // スケジュール更新
            if (this.Schedule != null && this.Schedule.ScheduleEdit.Equals(ScheduleEditType.Update))
            {
                //スケジュールのチェック
                if (this.IsEntrySchedule(ScheduleEditType.Update))
                {
                    var res = this.PutData();

                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        ReturnMessage = string.Format(Resources.KKM01019, start, end, Resources.KKM00002); // 登録（更新）完了

                        base.FormOkClose();
                    }
                }
            }
            // スケジュール登録
            else
            {
                //スケジュールのチェック
                if (this.IsEntrySchedule(ScheduleEditType.Insert))
                {
                    var res = this.PostData();

                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        ReturnMessage = string.Format(Resources.KKM01019, start, end, Resources.KKM00002); // 登録（更新）完了

                        this.Schedule.Schedule = res.Results.OfType<OuterCarScheduleGetOutModel>().FirstOrDefault();
                        this.Schedule.ScheduleEdit = ScheduleEditType.Insert;

                        base.FormOkClose();
                    }
                }
             }
        }
        #endregion

        #region 削除ボタンクリック
        /// <summary>
        /// 削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (Messenger.Confirm(Resources.KKM00007)   // 削除確認
                .Equals(DialogResult.No)) return;

            var start = getYMDH(getDateTime(this.StartDayDateTimePicker.SelectedDate, this.StartTimeComboBox.Text));
            var end = getYMDH(getDateTime(this.EndDayDateTimePicker.SelectedDate, this.EndTimeComboBox.Text));

            // スケジュール削除
            if (this.Schedule != null && this.Schedule.ScheduleEdit.Equals(ScheduleEditType.Update))
            {
                //スケジュール項目のチェック
                if (this.IsEntrySchedule(ScheduleEditType.Delete))
                {
                    var res = this.DeleteData();

                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        ReturnMessage = string.Format(Resources.KKM01019, start, end, Resources.KKM00003); // 削除完了

                        base.FormOkClose();
                    }
                }
            }
        }
        #endregion

        #region 目的コンボボックスの変更
        /// <summary>
        /// 目的コンボボックスの変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurposeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.PurposeComboBox.Text)
             || string.IsNullOrWhiteSpace(this.DestinationComboBox.Text))
            {
                this.TitleLabel.Text = string.Empty;
                return;
            }

            this.TitleLabel.Text = this.PurposeComboBox.Text + "@" + this.DestinationComboBox.Text;

            if (this.RentalOKRadioButton.Checked) this.TitleLabel.Text += RentalOKAddTitle;
        }
        #endregion

        #region 行先コンボボックスの変更
        /// <summary>
        /// 行先コンボボックスの変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DestinationComboBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.PurposeComboBox.Text)
             || string.IsNullOrWhiteSpace(this.DestinationComboBox.Text))
            {
                this.TitleLabel.Text = string.Empty;
                return;
            }

            this.TitleLabel.Text = this.PurposeComboBox.Text + "@" + this.DestinationComboBox.Text;

            if (this.RentalOKRadioButton.Checked) this.TitleLabel.Text += RentalOKAddTitle;
        }
        #endregion

        #region 空き時間貸出可チェックボックスのクリック
        /// <summary>
        /// 空き時間貸出可チェックボックスのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RentalOKRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.PurposeComboBox.Text)
             || string.IsNullOrWhiteSpace(this.DestinationComboBox.Text))
            {
                this.TitleLabel.Text = string.Empty;
                return;
            }

            if (this.RentalOKRadioButton.Checked)
            {
                this.TitleLabel.Text = this.PurposeComboBox.Text + "@" + this.DestinationComboBox.Text;
                this.TitleLabel.Text += RentalOKAddTitle;
            }
        }
        #endregion

        #region 空き時間貸出不可チェックボックスのクリック
        /// <summary>
        /// 空き時間貸出不可チェックボックスのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RentalNGRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.PurposeComboBox.Text)
             || string.IsNullOrWhiteSpace(this.DestinationComboBox.Text))
            {
                this.TitleLabel.Text = string.Empty;
                return;
            }

            if (this.RentalNGRadioButton.Checked)
            {
                this.TitleLabel.Text = this.PurposeComboBox.Text + "@" + this.DestinationComboBox.Text;
            }
        }
        #endregion

        #region スケジュールのチェック
        /// <summary>
        /// スケジュールのチェック
        /// </summary>
        /// <param name="type">スケジュール編集区分</param>
        /// <returns>チェック可否</returns>
        private bool IsEntrySchedule(ScheduleEditType type)
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            var start = this.GetDateTime(StartDayDateTimePicker, this.StartTimeComboBox);
            var end = this.GetDateTime(this.EndDayDateTimePicker, this.EndTimeComboBox);

            //期間の大小チェック
            map[this.EndDayDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                //期間Fromと期間Toがすべて入力してある場合のみチェック
                if (this.StartDayDateTimePicker.Value != null && this.StartTimeComboBox.Text != string.Empty
                    && this.EndDayDateTimePicker.Value != null && this.EndTimeComboBox.Text != string.Empty)
                {
                    //開始日が終了日より大きい場合はエラー
                    if (start >= end)
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM00043;

                        //背景色を変更
                        this.StartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                        this.EndDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    }
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

            //スケジュール項目が存在しているかどうか
            var item = this.ScheduleItemGet(Schedule.CategoryID);

            if (item == null)
            {
                //存在していない場合はエラー
                Messenger.Info(Resources.KKM00021);

                return false;
            }
            
            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //更新
                //削除
                case ScheduleEditType.Update:
                case ScheduleEditType.Delete:

                    //データが存在しているかどうか
                    var schedule = this.GetData();

                    if (schedule == null)
                    {
                        //存在していない場合はエラー
                        Messenger.Info(Resources.KKM00021);

                        return false;
                    }
                    else
                    {
                        //スケジュール再設定
                        this.Schedule.Schedule = schedule;
                    }

                    break;
            }

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //追加
                //更新
                case ScheduleEditType.Insert:
                case ScheduleEditType.Update:

                    var errList = new List<string>();

                    //最終予約日が設定されているかどうか
                    if (item != null && item.最終予約可能日 != null)
                    {
                        var limit = item.最終予約可能日.Value.Date.AddDays(1);

                        //開始日か終了日が最終予約日を超過しているかどうか
                        if (start != null && start > limit || end != null && end > limit)
                        {
                            errList.Add(Resources.KKM03010);
                        }
                    }

                    //検索条件
                    var cond = new OuterCarScheduleGetInModel
                    {
                        //カテゴリーID
                        CATEGORY_ID = this.Schedule.CategoryID
                    };

                    // チェックデータの取得
                    var list = this.ScheduleListGet(cond);

                    //同一行でスケジュールで重複した日付の期間が存在する場合はエラー
                    if (list.Where(x => x.SCHEDULE_ID != Schedule.ID && x.PARALLEL_INDEX_GROUP == Schedule.RowNo).Any(x =>
                            (x.START_DATE.Value.Date <= start.Value.Date && start.Value.Date <= x.END_DATE.Value.Date) ||
                            (x.START_DATE.Value.Date <= end.Value.Date && end.Value.Date <= x.END_DATE.Value.Date) ||
                            (start.Value.Date <= x.START_DATE.Value.Date && x.START_DATE.Value.Date <= end.Value.Date) ||
                            (start.Value.Date <= x.END_DATE.Value.Date && x.END_DATE.Value.Date <= end.Value.Date)) == true)
                    {
                        errList.Add(Resources.KKM03017);
                    }

                    //Delete Start 2022/03/16 杉浦 チェック削除
                    ////Append Start 2022/02/03 杉浦 試験車日程の車も登録する
                    ////Append Start 2022/03/08 杉浦 不具合修正
                    //if (!string.IsNullOrEmpty(item.管理票NO))
                    //{
                    //    //Append End 2022/03/08 杉浦 不具合修正
                    //    //検索条件
                    //    var cond2 = new AllScheduleSearchModel
                    //    {
                    //        //管理票番号
                    //        管理票番号 = item.管理票NO,

                    //        //期間(From)
                    //        START_DATE = start.Value.Date,

                    //        //期間(To)
                    //        END_DATE = end.Value.Date,

                    //    };

                    //    //スケジュールで重複した期間が存在する場合はエラー
                    //    var allList = this.GetAllScheduleList(cond2).Where(x => x.ID != this.Schedule.ID).ToList();
                    //    if (allList.Any() == true)
                    //    {
                    //        var scheduleType = allList.Where(x => (x.START_DATE.Value <= start && start <= x.END_DATE.Value) ||
                    //            (x.START_DATE.Value <= end && end <= x.END_DATE.Value) ||
                    //            (start <= x.START_DATE.Value && x.START_DATE.Value <= end) ||
                    //            (start <= x.END_DATE.Value && x.END_DATE.Value <= end)).ToList();

                    //        string text = string.Empty;
                    //        if (scheduleType != null && scheduleType.Count != 0)
                    //        {
                    //            var scheduleStringList = (scheduleType.Select(x => x.SCHEDULE_TYPE)).Distinct().OrderBy(x => x);

                    //            foreach (var d in scheduleStringList)
                    //            {
                    //                var d2 = d == "1" ? "試験車日程" : d == "2" ? "外製車日程" : "カーシェア日程";
                    //                text += d2 + " ";
                    //            }
                    //            //Update Start 2022/03/08 杉浦 不具合修正
                    //            //errList.Add("同時刻に" + text + "に既に登録がある為、登録出来ません。");
                    //            errList.Add(text + "の同時刻に既に登録がある為、登録出来ません。");
                    //            //Update End 2022/03/08 杉浦 不具合修正
                    //        }
                    //    }
                    //    //Append End 2022/02/03 杉浦 試験車日程の車も登録する
                    //    //Append Start 2022/03/08 杉浦 不具合修正
                    //}
                    ////Append End 2022/03/08 杉浦 不具合修正
                    //Delete End 2022/03/16 杉浦 チェック削除


                    //エラーがある場合は終了
                    if (errList.Any() == true)
                    {
                        Messenger.Warn(string.Join(Const.CrLf, errList.ToArray()));

                        return false;
                    }

                    if (IsManagement == false && (end.Value.Date - start.Value.Date).Days >= 5)
                    {
                        Messenger.Info(Resources.KKM01001);
                        return false;
                    }

                    break;
            }

            return true;
        }
        #endregion

        #region データ取得処理
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns>OutercarScheduleGetOutModel</returns>
        private OuterCarScheduleGetOutModel GetData()
        {
            OuterCarScheduleGetOutModel schedule = null;

            //検索条件
            var cond = new OuterCarScheduleGetInModel { SCHEDULE_ID = this.Schedule.ID };

            //APIで取得
            var res = HttpUtil.GetResponse<OuterCarScheduleGetInModel, OuterCarScheduleGetOutModel>(ControllerType.OuterCarSchedule, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                schedule = res.Results.FirstOrDefault();
            }

            return schedule;
        }
        #endregion

        #region データ登録処理
        /// <summary>
        /// データ登録処理
        /// </summary>
        private ResponseDto<OuterCarScheduleGetOutModel> PostData()
        {
            var data = new OuterCarSchedulePostInModel
            {
                // カテゴリーID
                CATEGORY_ID = this.Schedule.CategoryID,
                // 期間（開始）
                START_DATE = getDateTime(this.StartDayDateTimePicker.SelectedDate, this.StartTimeComboBox.Text),
                // 期間（終了）
                END_DATE = getDateTime(this.EndDayDateTimePicker.SelectedDate, this.EndTimeComboBox.Text),
                // 行番号
                PARALLEL_INDEX_GROUP = this.Schedule.RowNo,
                // スケジュール区分
                SYMBOL = int.Parse(FormControlUtil.GetRadioButtonValue(this.TypePanel)),
                // 説明
                DESCRIPTION = this.TitleLabel.Text,
                // 予約種別
                予約種別 = this.ReservationRadioButton.Checked ? "本予約" : "仮予約",
                // 所属グループID
                SECTION_GROUP_ID = SessionDto.SectionGroupID,
                // パーソナルID
                PERSONEL_ID = SessionDto.UserId,
                // 目的
                目的 = this.PurposeComboBox.Text,
                // 行先
                行先 = this.DestinationComboBox.Text,
                // 使用者TEL
                TEL = this.TelTextBox.Text,
                // 空時間貸出可フラグ
                FLAG_空時間貸出可 = this.RentalOKRadioButton.Checked ? 1 : 0,
                // 予約者ID
                予約者_ID = SessionDto.UserId,
                // 駐車場番号
                駐車場番号 = this.ParkingNoLabel.Text
            };

            return HttpUtil.PostResponse<OuterCarScheduleGetOutModel>(ControllerType.OuterCarSchedule, data);
        }
        #endregion

        #region データ更新処理
        /// <summary>
        /// データ更新処理
        /// </summary>
        private ResponseDto<OuterCarSchedulePutInModel> PutData()
        {
            var data = new OuterCarSchedulePutInModel
            {
                // スケジュールID
                SCHEDULE_ID = this.Schedule.ID,
                // 期間（開始）
                START_DATE = getDateTime(this.StartDayDateTimePicker.SelectedDate, this.StartTimeComboBox.Text),
                // 期間（終了）
                END_DATE = getDateTime(this.EndDayDateTimePicker.SelectedDate, this.EndTimeComboBox.Text),
                // 行番号
                PARALLEL_INDEX_GROUP = this.Schedule.RowNo,
                // スケジュール区分
                SYMBOL = int.Parse(FormControlUtil.GetRadioButtonValue(this.TypePanel)),
                // 説明
                DESCRIPTION = this.TitleLabel.Text,
                // 予約種別
                予約種別 = this.ReservationRadioButton.Checked ? "本予約" : "仮予約",
                // パーソナルID
                PERSONEL_ID = SessionDto.UserId,
                // 目的
                目的 = this.PurposeComboBox.Text,
                // 行先
                行先 = this.DestinationComboBox.Text,
                // 使用者TEL
                TEL = this.TelTextBox.Text,
                // 空時間貸出可フラグ
                FLAG_空時間貸出可 = this.RentalOKRadioButton.Checked ? 1 : 0,
                // 予約者ID
                予約者_ID = SessionDto.UserId,
                // 駐車場番号
                駐車場番号 = this.ParkingNoLabel.Text
            };

            return HttpUtil.PutResponse<OuterCarSchedulePutInModel>(ControllerType.OuterCarSchedule, data);
        }
        #endregion

        #region データ削除処理
        /// <summary>
        /// データ削除処理
        /// </summary>
        private ResponseDto<OuterCarScheduleDeleteInModel> DeleteData()
        {
            var data = new OuterCarScheduleDeleteInModel
            {
                // スケジュールID
                SCHEDULE_ID = this.Schedule.ID
            };

            return HttpUtil.DeleteResponse<OuterCarScheduleDeleteInModel>(ControllerType.OuterCarSchedule, data);
        }
        #endregion

        #region スケジュール項目の取得
        /// <summary>
        /// スケジュール項目の取得
        /// </summary>
        /// <returns>OutercarScheduleItemGetOutModel</returns>
        private OuterCarScheduleItemGetOutModel ScheduleItemGet(long scheduleid)
        {
            OuterCarScheduleItemGetOutModel item = null;

            //検索条件
            var cond = new OuterCarScheduleItemGetOutModel { SCHEDULE_ID = scheduleid };

            //APIで取得
            var res = HttpUtil.GetResponse<OuterCarScheduleItemGetOutModel, OuterCarScheduleItemGetOutModel>(ControllerType.OuterCarScheduleItem, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                item = res.Results.FirstOrDefault();
            }

            return item;
        }
        #endregion

        #region スケジュールの取得
        /// <summary>
        /// スケジュールの取得（検索条件）
        /// </summary>
        /// <returns>OutercarScheduleGetOutModel</returns>
        private List<OuterCarScheduleGetOutModel> ScheduleListGet(OuterCarScheduleGetInModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<OuterCarScheduleGetInModel, OuterCarScheduleGetOutModel>(ControllerType.OuterCarSchedule, cond);

            return (res.Results).ToList();
        }

        //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
        /// <summary>
        /// 全スケジュールの取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<AllScheduleModel> GetAllScheduleList(AllScheduleSearchModel cond)
        {
            var list = new List<AllScheduleModel>();

            //APIで取得
            var res = HttpUtil.GetResponse<AllScheduleSearchModel, AllScheduleModel>(ControllerType.AllSchedule, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        //Append End 2022/02/03 杉浦 試験車日程の車も登録する
        #endregion

        #region 目的検索
        /// <summary>
        /// 目的検索
        /// </summary>
        private List<PurposeSearchOutModel> GetPurposeList()
        {
            //パラメータ設定
            var itemCond = new PurposeSearchInModel
            {

            };

            //Get実行
            var res = HttpUtil.GetResponse<PurposeSearchInModel, PurposeSearchOutModel>(ControllerType.Purpose, itemCond);

            return (res.Results).ToList();
        }
        #endregion

        #region 行先検索
        /// <summary>
        /// 行先検索
        /// </summary>
        private List<GoalSearchOutModel> GetDestinationList()
        {
            //パラメータ設定
            var itemCond = new GoalSearchInModel
            {

            };

            //Get実行
            var res = HttpUtil.GetResponse<GoalSearchInModel, GoalSearchOutModel>(ControllerType.Goal, itemCond);

            return (res.Results).ToList();
        }
        #endregion
        
        #region 日時の取得
        /// <summary>
        /// 日時の取得
        /// </summary>
        /// <param name="dtp">日付</param>
        /// <param name="cmb">時刻</param>
        /// <returns>日時</returns>
        private DateTime? GetDateTime(NullableDateTimePicker dtp, ComboBox cmb)
        {
            //日時が入力されているかどうか
            if (dtp.SelectedDate == null || string.IsNullOrWhiteSpace(cmb.Text) == true)
            {
                return null;

            }

            return dtp.SelectedDate.Value.AddHours(int.Parse(cmb.Text));

        }
        #endregion
    }
}
