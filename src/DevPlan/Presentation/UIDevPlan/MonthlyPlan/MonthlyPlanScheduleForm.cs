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

namespace DevPlan.Presentation.UIDevPlan.MonthlyPlan
{
    /// <summary>
    /// 月次計画表スケジュール詳細
    /// </summary>
    public partial class MonthlyPlanScheduleForm : BaseSubForm
    {
        #region メンバ変数
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "スケジュール詳細（月次）"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>スケジュール情報</summary>
        public ScheduleModel<MonthlyWorkScheduleGetOutModel> SCHEDULE { get; set; }

        /// <summary>更新権限</summary>
        public bool IS_UPDATE { get; set; }

        /// <summary>指定月</summary>
        public DateTime BASE_MONTH { get; set; }

        /// <summary>月頭月末</summary>
        public int MONTH_FIRST_END { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MonthlyPlanScheduleForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 画面ロード
        /// <summary>
        /// MonthlyPlanDetailForm_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyPlanDetailForm_Load(object sender, EventArgs e)
        {
            // 初期化
            this.Init();
        }
        #endregion

        #region 初期化
        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            //初期表示フォーカス
            this.ActiveControl = this.TitleTextBox;

            if (this.SCHEDULE == null) return;

            if (!this.SCHEDULE.ScheduleEdit.Equals(ScheduleEditType.Update))
            {
                this.DeleteButton.Visible = false;
            }

            // 区分（1：なし、2：■、3：▲、4：◎）
            this.DefaultRadioButton.Checked = true;
            if (SCHEDULE.Status == 2) this.SquareRadioButton.Checked = true;
            if (SCHEDULE.Status == 3) this.TriangleRadioButton.Checked = true;
            if (SCHEDULE.Status == 4) this.DoubleCircleRadioButton.Checked = true;

            // タイトル
            this.TitleTextBox.Text = SCHEDULE.SubTitle;

            // 開始日
            this.StartDayDateTimePicker.Text = SCHEDULE.StartDate.ToString();

            // 終了日
            this.EndDayDateTimePicker.Text = SCHEDULE.EndDate.ToString();

            // ステータス
            if (SCHEDULE.IsClose) this.StatusCheckBox.Checked = true;

            // 更新権限
            if (!IS_UPDATE)
            {
                this.EntryButton.Visible = false;
                this.DeleteButton.Visible = false;
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
            // スケジュール更新
            if (this.SCHEDULE != null && this.SCHEDULE.ScheduleEdit.Equals(ScheduleEditType.Update))
            {
                //スケジュール項目のチェック
                if (this.IsEntrySchedule(ScheduleEditType.Update))
                {
                    var res = this.PutData();

                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        Messenger.Info(Resources.KKM00002); // 登録（更新）完了

                        base.FormOkClose();
                    }
                }
            }
            // スケジュール登録
            else
            {
                //スケジュール項目のチェック
                if (this.IsEntrySchedule(ScheduleEditType.Insert))
                {
                    var res = this.PostData();

                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        Messenger.Info(Resources.KKM00002); // 登録（更新）完了

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

            // スケジュール削除
            if (this.SCHEDULE != null && this.SCHEDULE.ScheduleEdit.Equals(ScheduleEditType.Update))
            {
                //スケジュール項目のチェック
                if (this.IsEntrySchedule(ScheduleEditType.Delete))
                {
                    var res = this.DeleteData();

                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        Messenger.Info(Resources.KKM00003); // 削除完了

                        base.FormOkClose();
                    }
                }
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

            var targetstart = BASE_MONTH;
            var targetend = BASE_MONTH.AddMonths(1).AddDays(-1);

            //期間の大小チェック・対象期間チェック
            map[this.EndDayDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                //期間Fromと期間Toがすべて入力してある場合のみチェック
                if (this.StartDayDateTimePicker.Value != null && this.EndDayDateTimePicker.Value != null)
                {
                    var start = this.StartDayDateTimePicker.SelectedDate.Value;
                    var end = this.EndDayDateTimePicker.SelectedDate.Value;

                    //開始日が終了日より大きい場合はエラー
                    if (start > end)
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM00018;

                        //背景色を変更
                        this.StartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                        this.EndDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    }
                    //期間外が対象月の場合はエラー
                    else if (end < targetstart || start > targetend || (start < targetstart && end < targetend))
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM00018;

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
            var item = this.ScheduleItemGet(SCHEDULE.CategoryID);

            if (item == null)
            {
                //存在していない場合はエラー
                Messenger.Info(Resources.KKM00021);

                return false;
            }

            // 承認済みかどうか
            var data = this.GetApprovalData(item.SECTION_GROUP_ID, this.BASE_MONTH, this.MONTH_FIRST_END);

            if (data?.FLAG_承認 == 1)
            {
                //承認済みの場合は変更不可
                Messenger.Info(Resources.KKM03020);

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
                        this.SCHEDULE.Schedule = schedule;
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

                    var start = this.StartDayDateTimePicker.SelectedDate;
                    var end = this.EndDayDateTimePicker.SelectedDate;

                    //検索条件
                    var cond = new MonthlyWorkScheduleGetInModel
                    {
                        //カテゴリーID
                        CATEGORY_ID = this.SCHEDULE.CategoryID,
                        //行番号
                        PARALLEL_INDEX_GROUP = this.SCHEDULE.RowNo
                    };

                    //スケジュールで重複した期間が存在する場合はエラー
                    if (this.ScheduleListGet(cond).Where(x => x.SCHEDULE_ID != SCHEDULE.ID).Any(x =>
                        (x.START_DATE <= start && start <= x.END_DATE) || (x.START_DATE <= end && end <= x.END_DATE) ||
                        (start <= x.START_DATE && x.START_DATE <= end) || (start <= x.END_DATE && x.END_DATE <= end)) == true)
                    {
                        Messenger.Warn(Resources.KKM03005);

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
        /// <returns>MonthlyWorkScheduleGetOutModel</returns>
        private MonthlyWorkScheduleGetOutModel GetData()
        {
            MonthlyWorkScheduleGetOutModel schedule = null;

            //検索条件
            var cond = new MonthlyWorkScheduleGetInModel { SCHEDULE_ID = this.SCHEDULE.ID };

            //APIで取得
            var res = HttpUtil.GetResponse<MonthlyWorkScheduleGetInModel, MonthlyWorkScheduleGetOutModel>(ControllerType.MonthlyWorkSchedule, cond);

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
        private ResponseDto<MonthlyWorkSchedulePostInModel> PostData()
        {
            var symbol = 1;
            if (this.SquareRadioButton.Checked) symbol = 2;
            if (this.TriangleRadioButton.Checked) symbol = 3;
            if (this.DoubleCircleRadioButton.Checked) symbol = 4;

            var data = new MonthlyWorkSchedulePostInModel
            {
                // カテゴリーID
                CATEGORY_ID = this.SCHEDULE.CategoryID,
                // 作業完了
                END_FLAG = this.StatusCheckBox.Checked ? 1 : 0,
                // 期間（開始）
                START_DATE = Convert.ToDateTime(this.StartDayDateTimePicker.Text),
                // 期間（終了）
                END_DATE = Convert.ToDateTime(this.EndDayDateTimePicker.Text),
                // 行番号
                PARALLEL_INDEX_GROUP = this.SCHEDULE.RowNo,
                // スケジュール区分
                SYMBOL = symbol,
                // 説明
                DESCRIPTION = this.TitleTextBox.Text,
                // パーソナルID
                PERSONEL_ID = SessionDto.UserId
            };

            return HttpUtil.PostResponse<MonthlyWorkSchedulePostInModel>(ControllerType.MonthlyWorkSchedule, data);
        }
        #endregion

        #region データ更新処理
        /// <summary>
        /// データ更新処理
        /// </summary>
        private ResponseDto<MonthlyWorkSchedulePutInModel> PutData()
        {
            var symbol = 1;
            if (this.SquareRadioButton.Checked) symbol = 2;
            if (this.TriangleRadioButton.Checked) symbol = 3;
            if (this.DoubleCircleRadioButton.Checked) symbol = 4;

            var data = new MonthlyWorkSchedulePutInModel
            {
                // スケジュールID
                SCHEDULE_ID = this.SCHEDULE.ID,
                // 行番号
                PARALLEL_INDEX_GROUP = this.SCHEDULE.RowNo,
                // 作業完了
                END_FLAG = this.StatusCheckBox.Checked ? 1 : 0,
                // 期間（開始）
                START_DATE = Convert.ToDateTime(this.StartDayDateTimePicker.Text),
                // 期間（終了）
                END_DATE = Convert.ToDateTime(this.EndDayDateTimePicker.Text),
                // スケジュール区分
                SYMBOL = symbol,
                // 説明
                DESCRIPTION = this.TitleTextBox.Text,
                // パーソナルID
                PERSONEL_ID = SessionDto.UserId
            };

            return HttpUtil.PutResponse<MonthlyWorkSchedulePutInModel>(ControllerType.MonthlyWorkSchedule, data);
        }
        #endregion

        #region データ削除処理
        /// <summary>
        /// データ削除処理
        /// </summary>
        private ResponseDto<MonthlyWorkScheduleDeleteInModel> DeleteData()
        {
            var data = new MonthlyWorkScheduleDeleteInModel
            {
                // スケジュールID
                SCHEDULE_ID = this.SCHEDULE.ID
            };

            return HttpUtil.DeleteResponse<MonthlyWorkScheduleDeleteInModel>(ControllerType.MonthlyWorkSchedule, data);
        }
        #endregion

        #region スケジュール項目の取得
        /// <summary>
        /// スケジュール項目の取得
        /// </summary>
        /// <returns>MonthlyWorkScheduleItemGetOutModel</returns>
        private MonthlyWorkScheduleItemGetOutModel ScheduleItemGet(long scheduleid)
        {
            MonthlyWorkScheduleItemGetOutModel item = null;

            //検索条件
            var cond = new MonthlyWorkScheduleItemGetInModel { SCHEDULE_ID = scheduleid };

            //APIで取得
            var res = HttpUtil.GetResponse<MonthlyWorkScheduleItemGetInModel, MonthlyWorkScheduleItemGetOutModel>(ControllerType.MonthlyWorkScheduleItem, cond);

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
        /// <returns>MonthlyWorkScheduleGetOutModel</returns>
        private List<MonthlyWorkScheduleGetOutModel> ScheduleListGet(MonthlyWorkScheduleGetInModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<MonthlyWorkScheduleGetInModel, MonthlyWorkScheduleGetOutModel>(ControllerType.MonthlyWorkSchedule, cond);

            return (res.Results).ToList();
        }
        #endregion

        #region 承認データの取得
        /// <summary>
        /// 承認データの取得
        /// </summary>
        private MonthlyWorkApprovalGetOutModel GetApprovalData(string sectiongroupid, DateTime basemonth, int monthfirstend)
        {
            var list = new List<MonthlyWorkApprovalGetOutModel>();

            //パラメータ設定
            var itemCond = new MonthlyWorkApprovalGetInModel
            {
                // 所属グループID
                SECTION_GROUP_ID = sectiongroupid,
                // 対象月
                対象月 = basemonth,
                // 月頭月末
                FLAG_月頭月末 = monthfirstend
            };

            //Get実行
            var res = HttpUtil.GetResponse<MonthlyWorkApprovalGetInModel, MonthlyWorkApprovalGetOutModel>(ControllerType.MonthlyWorkApproval, itemCond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                list.AddRange(res.Results);
            }

            return list.FirstOrDefault();
        }
        #endregion
    }
}
