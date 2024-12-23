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

namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    /// <summary>
    /// カーシェアスケジュール詳細画面
    /// </summary>
    public partial class CarShareScheduleDetailForm : BaseSubForm
    {
        #region メンバ変数
        private const string StartTimeDefault = "06";
        private const string EndTimeDefault = "22";

        private const string AkijikanKasidasiOk = "(空き時間貸出可)";

        private Func<DateTime?, string> getYMDH = dt => { return dt != null ? ((DateTime)dt).ToString("yyyy/MM/dd H時") : string.Empty; };
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "スケジュール詳細(カーシェア)"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>スケジュール項目</summary>
        private CarShareScheduleItemModel Item { get; set; }

        /// <summary>スケジュール</summary>
        public ScheduleModel<CarShareScheduleModel> Schedule { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>選択可能スケジュール最大日</summary>
        public DateTime SelectMaxDate { private get; set; }

        /// <summary>選択可能スケジュール最小日</summary>
        public DateTime SelectMinDate { private get; set; }

        /// <summary>結果メッセージ</summary>
        public string ReturnMessage { get; set; } = string.Empty;

        //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
        public string KanriNo { get; set; }
        //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>        
        public CarShareScheduleDetailForm()
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

            //スケジュール画面セット
            this.SetScheduleForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var item = this.GetCarShareScheduleItem();
            var schedule = this.Schedule.Schedule;

            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isJibuManagement = this.UserAuthority.JIBU_MANAGEMENT_FLG == '1';
            var isCarShareOffice = this.UserAuthority.CARSHARE_OFFICE_FLG == '1';

            //初期表示フォーカス
            this.ActiveControl = this.PurposeComboBox;

            //期間From
            this.StartTimeComboBox.Items.Clear();
            this.StartTimeComboBox.Items.AddRange(Const.ScheduleDetailStartTimeList);

            //期間To
            this.EndTimeComboBox.Items.Clear();
            this.EndTimeComboBox.Items.AddRange(Const.ScheduleDetailEndTimeList);

            //目的
            FormControlUtil.SetComboBoxItem(this.PurposeComboBox, HttpUtil.GetResponse<PurposeSearchOutModel>(ControllerType.Purpose).Results);

            //行先
            FormControlUtil.SetComboBoxItem(this.DestinationComboBox, HttpUtil.GetResponse<GoalSearchOutModel>(ControllerType.Goal).Results);

            //本予約
            if (string.IsNullOrWhiteSpace(item.INPUT_SECTION_ID))//ＳＪＳＢ項目
            {
                if (!isManagement)
                {
                    //管理権限なし
                    this.ReservationPanel.Enabled = false;
                }
                else
                {
                    //管理権限あり
                    this.ReservationPanel.Enabled = true;
                }
            }
            else
            {
                if (!isManagement && !isJibuManagement && !isCarShareOffice)
                {
                    //管理権限なし
                    this.ReservationPanel.Enabled = false;
                }
                else
                {
                    //管理権限あり
                    this.ReservationPanel.Enabled = true;
                }
            }

            //■の利用可否
            if (isCarShareOffice == false)
            {
                this.SquareRadioButton.Visible = false;
            }

            //更新権限ありで管理権限なしかどうか
            var isEntry = isUpdate;
            if (isEntry == true && isManagement == false && this.Schedule.ScheduleEdit != ScheduleEditType.Insert)
            {
                if (isJibuManagement == false && isCarShareOffice == false)
                {
                    isEntry = schedule.予約者_ID == SessionDto.UserId;
                }
                else if (isJibuManagement == true || isCarShareOffice == true)
                {
                    isEntry = !string.IsNullOrWhiteSpace(item.INPUT_SECTION_ID) || (string.IsNullOrWhiteSpace(item.INPUT_SECTION_ID) && schedule.予約者_ID == SessionDto.UserId);
                }
            }

            //編集不可の場合はボタンを表示しない。
            if (this.Schedule.IsEdit == false && this.Schedule.ScheduleEdit != ScheduleEditType.Insert)
            {
                isEntry = false;
            }

            //登録ボタン
            this.EntryButton.Visible = isEntry;

            //削除ボタン
            this.DeleteButton.Visible = isEntry;

            //スケジュール編集区分ごとの分岐
            switch (this.Schedule.ScheduleEdit)
            {
                //追加
                case ScheduleEditType.Insert:
                    //期間From
                    this.StartTimeComboBox.Text = StartTimeDefault;

                    //期間To
                    this.EndTimeComboBox.Text = EndTimeDefault;

                    //削除ボタン
                    this.DeleteButton.Visible = false;
                    break;

            }

            // 編集不可の場合
            if (!isEntry)
            {
                // 全入力項目の非活性
                FormControlUtil.SetMaskingControls(this.DetailTableLayoutPanel, false);
            }
        }

        /// <summary>
        /// スケジュール画面セット
        /// </summary>
        private void SetScheduleForm()
        {
            var item = this.Item;
            var schedule = this.Schedule.Schedule;

            //車両名
            this.CarNameLabel.Text = item?.CATEGORY;

            //区分
            FormControlUtil.SetRadioButtonValue(this.TypePanel, schedule.SYMBOL);

            //期間From
            var start = this.Schedule.StartDate;
            if (start != null)
            {
                //期間Fromにセット
                var startDay = start.Value;
                this.StartDayDateTimePicker.Value = startDay.Date;
                this.StartTimeComboBox.Text = startDay.ToString("HH");

            }
            else
            {
                //開始日が入力されてなければ初期化
                this.StartDayDateTimePicker.Value = null;
                this.StartTimeComboBox.SelectedIndex = 0;

            }

            //期間To
            var end = this.Schedule.EndDate;
            if (end != null)
            {
                //期間Fromにセット
                var endDay = end.Value;
                this.EndDayDateTimePicker.Value = endDay.Date;
                this.EndTimeComboBox.Text = endDay.ToString("HH");

            }
            else
            {
                //開始日が入力されてなければ初期化
                this.EndDayDateTimePicker.Value = null;
                this.EndTimeComboBox.SelectedIndex = 0;

            }

            //目的
            this.PurposeComboBox.SelectedIndex = -1;
            this.PurposeComboBox.Text = schedule.目的;

            //行先
            this.DestinationComboBox.SelectedIndex = -1;
            this.DestinationComboBox.Text = schedule.行先;

            //タイトル
            this.ScheduleNameLabel.Text = schedule.DESCRIPTION;

            //使用者TEL
            this.TelTextBox.Text = schedule.TEL;

            //空き時間
            //Update Start 2024/02/21 杉浦 空き時間貸出の初期値を「可」に変更
            //FormControlUtil.SetRadioButtonValue(this.BrankTimeLoanPanel, schedule.FLAG_空時間貸出可);
            if (this.Schedule.ScheduleEdit == ScheduleEditType.Insert)
            {
                FormControlUtil.SetRadioButtonValue(this.BrankTimeLoanPanel, 1);
            }
            else
            {
                FormControlUtil.SetRadioButtonValue(this.BrankTimeLoanPanel, schedule.FLAG_空時間貸出可);
            }
            //Update End 2024/02/21 杉浦 空き時間貸出の初期値を「可」に変更

            //新規登録かどうか
            if (this.Schedule.ScheduleEdit == ScheduleEditType.Insert)
            {
                //本予約
                this.ReservationRadioButton.Checked = (item?.FLAG_要予約許可 ?? 0) == 0;
                
                //管理権限がある場合は本予約をデフォルトとする。
                if(this.UserAuthority.MANAGEMENT_FLG == '1')
                {
                    this.ReservationRadioButton.Checked = true;
                }
            }
            else
            {
                //ステータス
                FormControlUtil.SetRadioButtonValue(this.ReservationPanel, schedule.予約種別);

            }

            var sectionCode = SessionDto.SectionCode;
            var name = SessionDto.UserName;
            //Append Start 2021/10/14 矢作
            var id = SessionDto.UserId;
            //Append End 2021/10/14 矢作

            //更新時は前回設定者の情報を設定
            if (this.Schedule.ScheduleEdit == ScheduleEditType.Update)
            {
                sectionCode = schedule.予約者_SECTION_CODE;
                name = schedule.予約者_NAME;
                //Append Start 2021/10/14 矢作
                id = schedule.予約者_ID;
                //Append End 2021/10/14 矢作
            }

            //予約者
            //Update Start 2021/10/14 矢作
            //this.ReservedPersonLabel.Text = string.Format("{0} {1}", sectionCode, name);
            this.ReservedPersonLabel.Text = string.Format("{0} {1}", sectionCode, name) + 
                "　" + new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, id, name);

            //使用者TEL
            var telNo = new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, id, name);
            //Update Start 2022/10/28 杉浦 電話番号優先度を変更
            //this.TelTextBox.Text = telNo;
            this.TelTextBox.Text = string.IsNullOrEmpty(this.TelTextBox.Text) ? telNo : this.TelTextBox.Text;
            //Update End 2022/10/28 杉浦 電話番号優先度を変更
            //Update End 2021/10/14 矢作

            //駐車場番号
            this.ParkingNoLabel.Text = item?.駐車場番号;

            //利用実績
            this.UsageRecordLabel.Text = schedule.FLAG_実使用 == 1 ? "利用あり" : "利用なし";

            //Append Start 2022/02/21 杉浦 入れ替え中車両の処理
            if (!string.IsNullOrEmpty(schedule.REPLACEMENT_TEXT))
            {
                this.ReplacementLabel.Visible = true;
                this.ReplacementLabel.Text = "【車両入替発生】\n" + schedule.REPLACEMENT_TEXT;
                this.CarNameLabel.Enabled = false;
                this.CarNameLabel.BackColor = Color.LightGray;
            }
            else
            {
                this.ReplacementLabel.Visible = false;
            }
            //Append End 2022/02/21 杉浦 入れ替え中車両の処理

        }
        #endregion

        #region 目的と行先と空き時間貸出の変更時
        /// <summary>
        /// 目的と行先のテキスト変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MokutekiYukisakiAkijikan_Changed(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            //タイトルの組み立て
            sb.AppendFormat("{0}@{1}", this.PurposeComboBox.Text, this.DestinationComboBox.Text);

            //空き時間貸出が可を選択しているかどうか
            if (this.BrankTimeLoanOkRadioButton.Checked == true)
            {
                sb.Append(AkijikanKasidasiOk);

            }

            //タイトル
            this.ScheduleNameLabel.Text = sb.ToString();

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
                var type = this.Schedule.ScheduleEdit;

                //スケジュール項目取得
                this.GetCarShareScheduleItem();

                //スケジュールのチェック
                if (this.IsEntrySchedule(type) == true)
                {
                    //スケジュールの登録
                    this.EntrySchedule(type);
                }

            });

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
            FormControlUtil.FormWait(this, () =>
            {
                //削除可否を問い合わせ
                if (Messenger.Confirm(Resources.KKM00007) == DialogResult.Yes)
                {
                    var type = ScheduleEditType.Delete;

                    //スケジュールのチェック
                    if (this.IsEntrySchedule(type) == true)
                    {
                        //スケジュールの登録
                        this.EntrySchedule(type);

                    }

                }

            });

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
            var item = this.Item;

            var schedule = this.Schedule.Schedule;

            var map = new Dictionary<Control, Func<Control, string, string>>();

            var start = this.GetDateTime(StartDayDateTimePicker, this.StartTimeComboBox);
            var end = this.GetDateTime(this.EndDayDateTimePicker, this.EndTimeComboBox);

            //期間の大小チェック
            map[this.EndTimeComboBox] = (c, name) =>
            {
                var errMsg = "";

                //期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー
                if (start != null && end != null && start >= end)
                {
                    //エラーメッセージ
                    errMsg = Resources.KKM00043;

                    //背景色を変更
                    this.StartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.EndDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    FormControlUtil.SetComboBoxBackColor(this.StartTimeComboBox, Const.ErrorBackColor);
                    FormControlUtil.SetComboBoxBackColor(this.EndTimeComboBox, Const.ErrorBackColor);

                }

                return errMsg;

            };

            map[this.StartDayDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                //最終予約日があるかどうか
                if (item != null && item.最終予約可能日 != null)
                {
                    var limit = item.最終予約可能日.Value.Date.AddDays(1);

                    //開始日が最終予約日を超過しているかどうか
                    if (start != null && start >= limit)
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM03010;

                        //背景色を変更
                        this.StartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                        FormControlUtil.SetComboBoxBackColor(this.StartTimeComboBox, Const.ErrorBackColor);

                    }

                    //終了日が最終予約日を超過しているかどうか
                    if (end != null && end >= limit)
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM03010;

                        //背景色を変更
                        this.EndDayDateTimePicker.BackColor = Const.ErrorBackColor;
                        FormControlUtil.SetComboBoxBackColor(this.EndTimeComboBox, Const.ErrorBackColor);

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

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //更新
                //削除
                case ScheduleEditType.Update:
                case ScheduleEditType.Delete:
                    //スケジュールが存在しているかどうか
                    schedule = this.GetCarShareSchedule();
                    if (schedule == null)
                    {
                        //存在していない場合はエラー
                        Messenger.Warn(Resources.KKM00021);
                        return false;

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
                    //検索条件
                    var cond = new CarShareScheduleSearchModel
                    {
                        //カテゴリーID
                        CATEGORY_ID = this.Schedule.CategoryID,

                        //期間(From)
                        START_DATE = start,

                        //期間(To)
                        END_DATE = end

                    };

                    var list = this.GetCarShareScheduleList(cond);

                    #region 項目作成権限の確認
                    var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
                    var isJibuManagement = this.UserAuthority.JIBU_MANAGEMENT_FLG == '1';

                    var managementEdit = false;
                    if (string.IsNullOrWhiteSpace(item.INPUT_SECTION_ID))//ＳＪＳＢ項目
                    {
                        if (!isManagement)
                        {
                            //管理権限なし
                            managementEdit = false;
                        }
                        else
                        {
                            //管理権限あり
                            managementEdit = true;
                        }
                    }
                    else
                    {
                        if (!isManagement && !isJibuManagement)
                        {
                            //管理権限なし
                            managementEdit = false;
                        }
                        else
                        {
                            //管理権限あり
                            managementEdit = true;
                        }
                    }
                    #endregion

                    // メイン画面のほうの条件を直すのも忘れず。
                    if (managementEdit == false &&
                    //Update Start 2021/10/15 杉浦 一般権限の予約可能期間を操作日から2ヶ月になるようにしてほしい
                        //(start.Value.Date < SelectMinDate || start.Value.Date > SelectMaxDate || end.Value.Date < SelectMinDate))
                        (start.Value.Date < SelectMinDate || start.Value.Date > SelectMaxDate || end.Value.Date < SelectMinDate || end.Value.Date > SelectMaxDate))
                    //Update Start 2021/10/15 杉浦 一般権限の予約可能期間を操作日から2ヶ月になるようにしてほしい
                    {
                        Messenger.Warn(string.Format(Resources.KKM01009, 2));
                        return false;
                    }

                    if (list.Where(x => x.ID != schedule.ID && x.PARALLEL_INDEX_GROUP == this.Schedule.RowNo).Any(x =>
                        (x.START_DATE.Value.Date <= start.Value.Date && start.Value.Date <= x.END_DATE.Value.Date) ||
                        (x.START_DATE.Value.Date <= end.Value.Date && end.Value.Date <= x.END_DATE.Value.Date) ||
                        (start.Value.Date <= x.START_DATE.Value.Date && x.START_DATE.Value.Date <= end.Value.Date) ||
                        (start.Value.Date <= x.END_DATE.Value.Date && x.END_DATE.Value.Date <= end.Value.Date)) == true)
                    {
                        //同一行でスケジュールで重複した日付の期間が存在する場合はエラー
                        Messenger.Warn(Resources.KKM03017);
                        return false;
                    }
                    else if (list.Where(x => x.ID != schedule.ID).Any(x =>
                        (x.START_DATE <= start && start < x.END_DATE) || (x.START_DATE < end && end <= x.END_DATE) ||
                        (start <= x.START_DATE && x.START_DATE < end) || (start < x.END_DATE && x.END_DATE <= end)) == true)
                    {
                        //スケジュールで重複した期間が存在する場合はエラー
                        Messenger.Warn(Resources.KKM03005);
                        return false;
                    }
                    else if (managementEdit == false && (end.Value.Date - start.Value.Date).Days >= 5)
                    {
                        Messenger.Info(Resources.KKM01001);
                        return false;
                    }

                    //Delete Start 2022/03/16 杉浦 チェック削除
                    ////Append Start 2022/03/08 杉浦 不具合修正
                    //if (!string.IsNullOrEmpty(KanriNo))
                    //{
                    //    //Append End 2022/03/08 杉浦 不具合修正
                    //    //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
                    //    //検索条件
                    //    var cond2 = new AllScheduleSearchModel
                    //    {
                    //        //管理票番号
                    //        管理票番号 = KanriNo,

                    //        //期間(From)
                    //        START_DATE = start,

                    //        //期間(To)
                    //        END_DATE = end

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
                    //            //Messenger.Warn(("同時間に" + text + "に既に登録がある為、登録出来ません。"));
                    //            Messenger.Warn((text + "の同時刻に既に登録がある為、登録出来ません。"));
                    //            //Update End 2022/03/08 杉浦 不具合修正
                    //            return false;
                    //        }

                    //    }
                    //    //Append End 2022/02/03 杉浦 試験車日程の車も登録する
                    //    //Append Start 2022/03/08 杉浦 不具合修正
                    //}
                    ////Append End 2022/03/08 杉浦 不具合修正
                    //Delete End 2022/03/16 杉浦 チェック削除


                    break;

            }

            //スケジュール再設定
            this.Schedule.Schedule = schedule;

            return true;

        }
        #endregion

        #region スケジュールの登録
        /// <summary>
        /// スケジュールの登録
        /// </summary>
        /// <param name="type">スケジュール編集区分</param>
        private void EntrySchedule(ScheduleEditType type)
        {
            ResponseDto<CarShareScheduleModel> res = null;

            var list = new[] { this.GetEntryCarShareSchedule() };

            var msg = Resources.KKM00002;

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //追加
                case ScheduleEditType.Insert:
                    res = HttpUtil.PostResponse(ControllerType.CarShareSchedule, list);
                    this.Schedule.Schedule = res.Results.OfType<CarShareScheduleModel>().FirstOrDefault();
                    break;

                //更新
                case ScheduleEditType.Update:
                    res = HttpUtil.PutResponse(ControllerType.CarShareSchedule, list);
                    break;

                //削除
                case ScheduleEditType.Delete:
                    msg = Resources.KKM00003;
                    res = HttpUtil.DeleteResponse(ControllerType.CarShareSchedule, list);
                    break;

            }

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                // メッセージ設定
                ReturnMessage = string.Format(Resources.KKM01019, getYMDH(list.FirstOrDefault().START_DATE), getYMDH(list.FirstOrDefault().END_DATE), msg);

                this.Schedule.ScheduleEdit = type;

                //フォームクローズ
                base.FormOkClose();

            }

        }

        /// <summary>
        /// 登録カーシェアスケジュールクラス取得
        /// </summary>
        /// <returns></returns>
        private CarShareScheduleModel GetEntryCarShareSchedule()
        {
            var schedule = this.Schedule.Schedule;

            Func<Panel, short?> getRadioButtonValue = p =>
            {
                var value = FormControlUtil.GetRadioButtonValue(p);

                return string.IsNullOrWhiteSpace(value) ? null : (short?)short.Parse(value);

            };

            //区分
            schedule.SYMBOL = getRadioButtonValue(this.TypePanel);

            //タイトル
            schedule.DESCRIPTION = this.ScheduleNameLabel.Text;

            //期間From
            schedule.START_DATE = this.GetDateTime(this.StartDayDateTimePicker, this.StartTimeComboBox);

            //期間To
            schedule.END_DATE = this.GetDateTime(this.EndDayDateTimePicker, this.EndTimeComboBox);

            //目的
            schedule.目的 = this.PurposeComboBox.Text;

            //行先
            schedule.行先 = this.DestinationComboBox.Text;

            //使用者TEL
            schedule.TEL = this.TelTextBox.Text;

            //空き時間
            schedule.FLAG_空時間貸出可 = getRadioButtonValue(this.BrankTimeLoanPanel);

            //ステータス
            schedule.予約種別 = FormControlUtil.GetRadioButtonValue(this.ReservationPanel);

            //ユーザー情報設定
            schedule.SetUserInfo();

            return schedule;

        }
        #endregion

        #region データの取得
        /// <summary>
        /// スケジュール項目の取得
        /// </summary>
        /// <returns></returns>
        private CarShareScheduleItemModel GetCarShareScheduleItem()
        {
            var list = new List<CarShareScheduleItemModel>();

            var cond = new CarShareScheduleSearchModel { CAR_GROUP = this.Schedule.Schedule.CAR_GROUP, ID = this.Schedule.CategoryID };

            //APIで取得
            var res = HttpUtil.GetResponse<CarShareScheduleSearchModel, CarShareScheduleItemModel>(ControllerType.CarShareScheduleItem, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            //スケジュール項目
            this.Item = list.FirstOrDefault();

            return this.Item;

        }

        /// <summary>
        /// スケジュールの取得
        /// </summary>
        /// <returns></returns>
        private CarShareScheduleModel GetCarShareSchedule()
        {
            return this.GetCarShareScheduleList(new CarShareScheduleSearchModel { ID = this.Schedule.ID }).FirstOrDefault();

        }

        /// <summary>
        /// スケジュールの取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<CarShareScheduleModel> GetCarShareScheduleList(CarShareScheduleSearchModel cond)
        {
            var list = new List<CarShareScheduleModel>();

            //APIで取得
            var res = HttpUtil.GetResponse<CarShareScheduleSearchModel, CarShareScheduleModel>(ControllerType.CarShareSchedule, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

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
