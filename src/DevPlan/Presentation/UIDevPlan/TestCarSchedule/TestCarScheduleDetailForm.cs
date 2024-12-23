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
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    /// <summary>
    /// 試験車スケジュール詳細画面
    /// </summary>
    public partial class TestCarScheduleDetailForm : BaseSubForm
    {
        #region メンバ変数
        private const string Title = "スケジュール詳細 {0}";

        private const string StartTimeDefault = "06";
        private const string EndTimeDefault = "22";

        private const int RemoveStartRow = 3;
        private const int RemoveRow = 7;

        private int DeleteTableHeight = 0;

        private Func<DateTime?, string> getYMDH = dt => { return dt != null ? ((DateTime)dt).ToString("yyyy/MM/dd H時") : string.Empty; };
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return this.GetFormName(); } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>スケジュール</summary>
        public ScheduleModel<TestCarScheduleModel> Schedule { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>結果メッセージ</summary>
        public string ReturnMessage { get; set; } = string.Empty;

        /// <summary>項目活性フラグ</summary>
        private bool IsEnable { get; set; } = true;

        //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
        public string KanriNo { get; set; }
        //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCarScheduleDetailForm()
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
        private void TestCarScheduleDetailForm_Load(object sender, EventArgs e)
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
            var schedule = this.Schedule.Schedule;

            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            //初期表示フォーカス
            this.ActiveControl = this.ScheduleNameTextBox;

            //本予約
            this.ReservationPanel.Enabled = isManagement;

            //期間From
            this.StartTimeComboBox.Items.Clear();
            this.StartTimeComboBox.Items.AddRange(Const.ScheduleDetailStartTimeList);
            
            //期間To
            this.EndTimeComboBox.Items.Clear();
            this.EndTimeComboBox.Items.AddRange(Const.ScheduleDetailEndTimeList);

            //試験車日程種別ごとの分岐
            switch (schedule.試験車日程種別)
            {
                //使用部署要望案
                case Const.Youbou:
                    var isEntry = isUpdate;

                    //更新権限ありで管理権限なしかどうか
                    if (isUpdate == true && isManagement == false)
                    {
                        //Update Start 2023/09/11 杉浦 仮予約者が本予約の編集を不可とするよう修正
                        //isEntry = (this.Schedule.ScheduleEdit == ScheduleEditType.Insert || schedule.INPUT_PERSONEL_ID == SessionDto.UserId);
                        isEntry = (this.Schedule.ScheduleEdit == ScheduleEditType.Insert || this.Schedule.IsEdit);
                        //Update End 2023/09/11 杉浦  仮予約者が本予約の編集を不可とするよう修正
                    }

                    //登録ボタン
                    this.EntryButton.Visible = isEntry;

                    //削除ボタン
                    this.DeleteButton.Visible = isEntry;

                    //項目活性フラグ
                    IsEnable = isEntry;
                    break;

                //SJSB調整案
                case Const.Tyousei:
                    //登録ボタン
                    this.EntryButton.Visible = isManagement;

                    //削除ボタン
                    this.DeleteButton.Visible = isManagement;

                    //SJSB調整案では本予約は行わない。
                    this.ReservationRadioButton.Enabled = false;

                    //項目活性フラグ
                    IsEnable = isManagement;
                    break;

                //最終調整結果
                case Const.Kettei:
                    //区分
                    this.TypePanel.Enabled = isManagement;

                    //スケジュール名
                    this.ScheduleNameTextBox.Enabled = isManagement;

                    //期間
                    this.DatePanel.Enabled = isManagement;

                    //車輛使用者
                    this.CarUserComboBox.Enabled = isManagement;

                    //登録ボタン
                    this.EntryButton.Visible = isUpdate;

                    //削除ボタン
                    this.DeleteButton.Visible = isManagement || schedule.INPUT_PERSONEL_ID == SessionDto.UserId;

                    //項目活性フラグ
                    IsEnable = isUpdate;
                    break;

            }

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

            //試験車日程種別が最終調整結果かどうか
            if (schedule.試験車日程種別 != Const.Kettei)
            {
                //描画停止
                this.DetailTableLayoutPanel.SuspendLayout();

                //作業完了日から鍵保管場所までを削除
                for (var i = 0; i < RemoveRow; i++)
                {
                    //行のコントロールを取得
                    var del1 = this.DetailTableLayoutPanel.GetControlFromPosition(0, RemoveStartRow);
                    var del2 = this.DetailTableLayoutPanel.GetControlFromPosition(1, RemoveStartRow);

                    DeleteTableHeight += del1.Height;

                    //行のコントロールを削除
                    this.DetailTableLayoutPanel.Controls.Remove(del1);
                    this.DetailTableLayoutPanel.Controls.Remove(del2);

                    //削除した行から下の行のコントロールを1行上に移動
                    for (var j = RemoveStartRow + 1; j < this.DetailTableLayoutPanel.RowCount; j++)
                    {
                        //移動対象のコントロール取得
                        var move1 = this.DetailTableLayoutPanel.GetControlFromPosition(0, j);
                        var move2 = this.DetailTableLayoutPanel.GetControlFromPosition(1, j);

                        //移動対象のコントロール移動
                        if (move1 != null)
                        {
                            this.DetailTableLayoutPanel.SetCellPosition(move1, new TableLayoutPanelCellPosition(0, j - 1));

                        }

                        //移動対象のコントロール移動
                        if (move2 != null)
                        {
                            this.DetailTableLayoutPanel.SetCellPosition(move2, new TableLayoutPanelCellPosition(1, j - 1));

                        }

                    }

                    //最終行を削除
                    this.DetailTableLayoutPanel.RowStyles.RemoveAt(this.DetailTableLayoutPanel.RowCount - 1);
                    this.DetailTableLayoutPanel.RowCount--;

                }

                //コントロールの縦幅を調整
                this.DetailTableLayoutPanel.Height -= DeleteTableHeight;
                this.ListFormMainPanel.Height -= DeleteTableHeight;
                this.MinimumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height - DeleteTableHeight);
                this.Height -= DeleteTableHeight;

                //描画再開
                this.DetailTableLayoutPanel.ResumeLayout();

                // 更新不可の場合
                if (!IsEnable)
                {
                    // 全入力項目の非活性
                    FormControlUtil.SetMaskingControls(this.DetailTableLayoutPanel, false);
                }
            }
        }

        /// <summary>
        /// スケジュール画面セット
        /// </summary>
        private void SetScheduleForm()
        {
            var schedule = this.Schedule.Schedule;

            //予約状態
            FormControlUtil.SetRadioButtonValue(this.ReservationPanel, schedule.予約種別);

            //区分
            FormControlUtil.SetRadioButtonValue(this.TypePanel, schedule.SYMBOL);

            //スケジュール名
            this.ScheduleNameTextBox.Text = schedule.DESCRIPTION;

            //作業完了日
            this.WorkCompletionDateTimePicker.Value = null;

            //試験車日程種別が最終調整結果の場合のみ完了日をセット
            if (schedule.試験車日程種別 == Const.Kettei)
            {
                this.WorkCompletionDateTimePicker.Value = schedule.完了日;

            }

            //オドメータ
            this.OdometerTextBox.Text = schedule.オドメータ;

            //試験実施内容
            this.TestContentTextBox.Text = schedule.試験内容;

            //脱着部品と復元有無
            this.DesorptionPpartsTextBox.Text = schedule.脱着部品;

            //変更箇所有無(ソフト含む）と内容
            this.ChangePlaceTextBox.Text = schedule.変更箇所;

            //車両保管場所
            this.CarStorageLocationTextBox.Text = schedule.車両保管場所;

            //鍵保管場所
            this.KeyStorageLocationTextBox.Text = schedule.鍵保管場所;

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

            var sectionCode = SessionDto.SectionCode;

            //Update Start 2021/10/14 矢作
            //var name = SessionDto.UserName;
            var name = SessionDto.UserName + "　" + new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, SessionDto.UserId, SessionDto.UserName);
            //Update End 2021/10/14 矢作

            //更新時は前回設定者の情報を設定
            if (this.Schedule.ScheduleEdit == ScheduleEditType.Update)
            {
                sectionCode = schedule.予約者_SECTION_CODE;
                //Update Start 2021/10/14 矢作
                //name = schedule.予約者_NAME;
                //Update Start 2021/12/06 杉浦 表示される番号がマッチしていない
                //name = schedule.予約者_NAME + "　" + new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, SessionDto.UserId, SessionDto.UserName);
                name = schedule.予約者_NAME + "　" + new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, schedule.予約者_ID, schedule.予約者_NAME);
                //Update End 2021/12/06 杉浦 表示される番号がマッチしていない
                //Update End 2021/10/14 矢作
            }

            //予約者
            this.ReservedPersonLabel.Text = string.Format("{0} {1}", sectionCode, name);

            //社員IDが設定されているかどうか
            var personelId = this.Schedule.ScheduleEdit == ScheduleEditType.Insert ? SessionDto.UserId : schedule.設定者_ID;
            if (string.IsNullOrWhiteSpace(personelId) == false)
            {
                var cond = new UserSearchModel
                {
                    //社員コード
                    PERSONEL_ID = personelId

                };

                //APIで検索
                var res = HttpUtil.GetResponse<UserSearchModel, UserSearchOutModel>(ControllerType.User, cond);

                foreach (var i in res.Results)
                {
                    i.DISPLAY_NAME2 = i.DISPLAY_NAME + " " + new ADUtil().GetUserData(ADUtilTypeEnum.MOBILE, i.PERSONEL_ID, i.NAME);
                }

                FormControlUtil.SetComboBoxItem(this.CarUserComboBox, res?.Results, false);

                if(this.CarUserComboBox.Items.Count > 0)
                {
                    this.CarUserComboBox.SelectedIndex = 0;
                }

            }

        }
        #endregion

        #region 画面名を取得
        /// <summary>
        /// 画面名を取得
        /// </summary>
        /// <returns>画面名</returns>
        private string GetFormName()
        {
            var name = "";

            //試験車日程種別ごとの分岐
            switch (this.Schedule.Schedule.試験車日程種別)
            {
                //使用部署要望案
                case Const.Youbou:
                    name = "使用部署要望案";
                    break;

                //SJSB調整案
                case Const.Tyousei:
                    name = "SJSB調整案";
                    break;

                //最終調整結果
                case Const.Kettei:
                    name = "最終調整結果";
                    break;
            }

            return string.Format(Title, name);

        }
        #endregion

        #region 作業完了日変更
        /// <summary>
        /// 作業完了日変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkCompletionDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            // 全項目非活性の場合は後処理不要
            if (!IsEnable || this.Schedule == null) return;
            
            var schedule = this.Schedule.Schedule;

            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            var flg = (this.Schedule?.Schedule?.完了日 == null || this.WorkCompletionDateTimePicker.Value == null);

            //オドメータ
            this.OdometerTextBox.Enabled = flg;

            //試験実施内容
            this.TestContentTextBox.Enabled = flg;

            //脱着部品と復元有無
            this.DesorptionPpartsTextBox.Enabled = flg;

            //変更箇所有無(ソフト含む）と内容
            this.ChangePlaceTextBox.Enabled = flg;

            //車両保管場所
            this.CarStorageLocationTextBox.Enabled = flg;

            //鍵保管場所
            this.KeyStorageLocationTextBox.Enabled = flg;

            //期間From
            this.StartDayDateTimePicker.Enabled = flg;
            this.StartTimeComboBox.Enabled = flg;

            //期間To
            this.EndDayDateTimePicker.Enabled = flg;
            this.EndTimeComboBox.Enabled = flg;

            //車両使用者
            this.CarUserComboBox.Enabled = schedule.試験車日程種別 == Const.Kettei && !isManagement ? false : flg;
        }
        #endregion

        #region 車両使用者マウスクリック
        /// <summary>
        /// 車両使用者マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarUserComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            using (var form = new UserListForm { DepartmentCode = SessionDto.DepartmentCode, SectionCode = SessionDto.SectionCode, UserAuthority = this.UserAuthority, StatusCode = "a" })
            {
                //ユーザー検索画面表示
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //ユーザーをセット
                    FormControlUtil.SetComboBoxItem(this.CarUserComboBox, new[] { form.User }, false);
                    this.CarUserComboBox.SelectedIndex = 0;

                }

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
            FormControlUtil.FormWait(this, () =>
            {
                var type = this.Schedule.ScheduleEdit;
               
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
            var schedule = this.Schedule.Schedule;

            var map = new Dictionary<Control, Func<Control, string, string>>();

            //期間の大小チェック
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

            //試験車日程種別ごとの分岐
            switch (this.Schedule.Schedule.試験車日程種別)
            {
                //最終調整結果
                case Const.Kettei:
                    //作業完了日が入力されているかどうか
                    if (this.WorkCompletionDateTimePicker.Value != null)
                    {
                        Func<TextBox, string, string> isRequired = (text, name) => string.IsNullOrWhiteSpace(text.Text) == false ? "" : string.Format(Resources.KKM00001, name);

                        //オドメータ
                        map[this.OdometerTextBox] = (c, name) => isRequired((c as TextBox), name);

                        //試験実施内容
                        map[this.TestContentTextBox] = (c, name) => isRequired((c as TextBox), name);

                        //脱着部品と復元有無
                        map[this.DesorptionPpartsTextBox] = (c, name) => isRequired((c as TextBox), name);

                        //変更箇所有無(ソフト含む）と内容
                        map[this.ChangePlaceTextBox] = (c, name) => isRequired((c as TextBox), name);

                        //車両保管場所
                        map[this.CarStorageLocationTextBox] = (c, name) => isRequired((c as TextBox), name);

                        //鍵保管場所
                        map[this.KeyStorageLocationTextBox] = (c, name) => isRequired((c as TextBox), name);

                    }
                    break;

            }

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
                    schedule = this.GetTestCarSchedule();
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
                    var cond = new TestCarScheduleSearchModel
                    {
                        //カテゴリーID
                        CATEGORY_ID = this.Schedule.CategoryID,

                        //試験車日程種別
                        試験車日程種別 = this.Schedule.Schedule.試験車日程種別,

                        //期間(From)
                        START_DATE = this.StartDayDateTimePicker.SelectedDate,

                        //期間(To)
                        END_DATE = this.EndDayDateTimePicker.SelectedDate,

                        //行番号
                        PARALLEL_INDEX_GROUP = this.Schedule.RowNo

                    };

                    //スケジュールで重複した期間が存在する場合はエラー
                    if (this.GetTestCarScheduleList(cond).Any(x => x.ID != this.Schedule.ID) == true)
                    {
                        Messenger.Warn(Resources.KKM03017);
                        return false;

                    }

                    //Delete Start 2022/03/16 杉浦 チェック削除
                    ////Append Start 2022/02/03 杉浦 試験車日程の車も登録する
                    ////Append Start 2022/03/08 杉浦 不具合修正
                    //if (!string.IsNullOrEmpty(KanriNo))
                    //{
                    //    //Append End 2022/03/08 杉浦 不具合修正
                    //    //検索条件
                    //    var cond2 = new AllScheduleSearchModel
                    //    {
                    //        //管理票番号
                    //        管理票番号 = KanriNo,

                    //        //期間(From)
                    //        START_DATE = this.StartDayDateTimePicker.SelectedDate,

                    //        //期間(To)
                    //        END_DATE = this.EndDayDateTimePicker.SelectedDate,

                    //    };

                    //    //スケジュールで重複した期間が存在する場合はエラー
                    //    var list = this.GetAllScheduleList(cond2).Where(x => x.ID != this.Schedule.ID).ToList();
                    //    if (list.Any() == true)
                    //    {
                    //        var start = this.StartDayDateTimePicker.SelectedDate.Value.AddHours(int.Parse(this.StartTimeComboBox.Text));
                    //        var end = this.EndDayDateTimePicker.SelectedDate.Value.AddHours(int.Parse(this.EndTimeComboBox.Text));
                    //        var scheduleType = list.Where(x => (x.START_DATE.Value <= start && start <= x.END_DATE.Value) ||
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
                    //            //Messenger.Warn(("同時刻に" + text + "に既に登録がある為、登録出来ません。"));
                    //            Messenger.Warn((text + "の同時刻に既に登録がある為、登録出来ません。"));
                    //            //Update Start 2022/03/08 杉浦 不具合修正
                    //            return false;
                    //        }

                    //    }
                    //    //Append Start 2022/03/08 杉浦 不具合修正
                    //}
                    ////Append End 2022/03/08 杉浦 不具合修正
                    ////Append End 2022/02/03 杉浦 試験車日程の車も登録する
                    //Delete End 2022/03/16 杉浦 チェック削除
                    break;

            }

            if (type == ScheduleEditType.Insert &&
                FormControlUtil.GetRadioButtonValue(this.ReservationPanel) == "本予約" && this.Schedule.Schedule.試験車日程種別 == Const.Youbou)
            {
                Messenger.Info(Resources.KKM01005);
                return false;
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
        /// <remarks>
        /// スケジュールの登録を行います。
        /// スケジュール登録前には試験車日程種別および予約種別を確認し、
        /// 要望案かつ仮予約から要望案の本予約に変わった場合は本予約コピーの呼び出しを、
        /// 仮予約ではなくすぐに本予約とされた場合は試験車日程種別を決定として登録を行います。
        /// </remarks>
        /// <param name="type">スケジュール編集区分</param>
        private void EntrySchedule(ScheduleEditType type)
        {
            ResponseDto<TestCarScheduleModel> res = null;

            var oldTestCarType = this.Schedule.Schedule.試験車日程種別;
            var oldTestCarYoyakuType = this.Schedule.Schedule.予約種別;

            var data = this.GetEntryTestCarSchedule();

            bool isKetteiCopy = false;//変えるところが増えてしまうので、いったんこの方法にて・・・。本予約コピーが必要なデータフラグ。
            if (data.予約種別 == "本予約" && oldTestCarYoyakuType == "仮予約" && oldTestCarType == Const.Youbou && type == ScheduleEditType.Update)
            {
                isKetteiCopy = true;
            }

            var msgList = new List<string> { Resources.KKM00002 };

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //追加
                case ScheduleEditType.Insert:
                    res = HttpUtil.PostResponse(ControllerType.TestCarSchedule, new[] { data });
                    this.Schedule.Schedule = res.Results.OfType<TestCarScheduleModel>().FirstOrDefault();
                    break;

                //更新
                case ScheduleEditType.Update:
                    if (isKetteiCopy)
                    {
                        res = HttpUtil.PostResponse(ControllerType.TestCarScheduleKetteiCopy, new[] { data });
                    }
                    else
                    {
                        res = HttpUtil.PutResponse(ControllerType.TestCarSchedule, new[] { data });
                    }

                    break;

                //削除
                case ScheduleEditType.Delete:
                    //削除完了メッセージに変更
                    msgList.Clear();
                    msgList.Add(Resources.KKM00003);

                    res = HttpUtil.DeleteResponse(ControllerType.TestCarSchedule, new[] { data });
                    break;

            }

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //登録後メッセージ

                if (isKetteiCopy)
                {
                    Messenger.Info(Resources.KKM01004);
                }
                else
                {
                    ReturnMessage = string.Format(Resources.KKM01019, getYMDH(data.START_DATE), getYMDH(data.END_DATE), string.Join(Const.CrLf, msgList.ToArray()));
                    this.Schedule.ScheduleEdit = type;
                }

                //フォームクローズ
                base.FormOkClose();

            }

        }

        /// <summary>
        /// 登録試験車スケジュールクラス取得
        /// </summary>
        /// <returns></returns>
        private TestCarScheduleModel GetEntryTestCarSchedule()
        {
            var schedule = this.Schedule.Schedule;

            Func<DateTime?, string, DateTime?> getDateTime = (dt, time) => dt == null ? null : (DateTime?)dt.Value.AddHours(int.Parse(time));

            //予約状態
            schedule.予約種別 = FormControlUtil.GetRadioButtonValue(this.ReservationPanel);

            //区分
            schedule.SYMBOL = int.Parse(FormControlUtil.GetRadioButtonValue(this.TypePanel));

            //スケジュール名
            schedule.DESCRIPTION = this.ScheduleNameTextBox.Text;

            //作業完了日
            schedule.完了日 = this.WorkCompletionDateTimePicker.SelectedDate;

            //オドメータ
            schedule.オドメータ = this.OdometerTextBox.Text;

            //試験実施内容
            schedule.試験内容 = this.TestContentTextBox.Text;

            //脱着部品と復元有無
            schedule.脱着部品 = this.DesorptionPpartsTextBox.Text;

            //変更箇所有無(ソフト含む）と内容
            schedule.変更箇所 = this.ChangePlaceTextBox.Text;

            //車両保管場所
            schedule.車両保管場所 = this.CarStorageLocationTextBox.Text;

            //鍵保管場所
            schedule.鍵保管場所 = this.KeyStorageLocationTextBox.Text;

            //期間From
            schedule.START_DATE = getDateTime(this.StartDayDateTimePicker.SelectedDate, this.StartTimeComboBox.Text);

            //期間To
            schedule.END_DATE = getDateTime(this.EndDayDateTimePicker.SelectedDate, this.EndTimeComboBox.Text);

            //車両使用者
            if(this.CarUserComboBox.SelectedIndex >= 0)
            {
                schedule.設定者_ID = this.CarUserComboBox.SelectedValue.ToString();
            }

            //ユーザー情報設定
            schedule.SetUserInfo();

            return schedule;

        }
        #endregion

        #region データの取得
        /// <summary>
        /// スケジュールの取得
        /// </summary>
        /// <returns></returns>
        private TestCarScheduleModel GetTestCarSchedule()
        {
            return this.GetTestCarScheduleList(new TestCarScheduleSearchModel { ID = this.Schedule.ID }).FirstOrDefault();

        }

        /// <summary>
        /// スケジュールの取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<TestCarScheduleModel> GetTestCarScheduleList(TestCarScheduleSearchModel cond)
        {
            var list = new List<TestCarScheduleModel>();

            //APIで取得
            var res = HttpUtil.GetResponse<TestCarScheduleSearchModel, TestCarScheduleModel>(ControllerType.TestCarSchedule, cond);

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
    }
}
