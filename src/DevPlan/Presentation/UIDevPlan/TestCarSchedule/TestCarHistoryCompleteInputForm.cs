using DevPlan.Presentation.Base;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    /// <summary>
    /// 試験車日程作業完了簡易入力フォーム
    /// </summary>
    public partial class TestCarHistoryCompleteInputForm : BaseSubForm
    {
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "作業完了簡易入力"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>初期表示開発符号</summary>
        public string GeneralCode { get; internal set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; internal set; }

        /// <summary>選択されたスケジュール情報</summary>
        public Result ResultScheduleInfo { get; private set; }

        /// <summary>当フォーム呼び出し元の再読込が必要の場合True</summary>
        internal bool isRequiredReload;

        /// <summary>入力チェックにてエラーとなった行Index</summary>
        private int? errorRowIndex = null;

        /// <summary>現在選択されている行Index</summary>
        private int? selectIndex = null;

        /// <summary>内容を展開する場合True</summary>
        private bool isAutoFit;

        /// <summary>編集中フラグ</summary>
        private bool isEdit = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCarHistoryCompleteInputForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarHistoryCompleteInputForm_Load(object sender, EventArgs e)
        {
            this.StartDateDateTimePicker.Value = DateTime.Today.AddDays(-90);
            this.WorkCompletionDateTimePicker.Value = null;

            this.historyMultiRowTemplate1.CarGroupTextBoxCell.DataField = nameof(TestCarHistoryComplete.CarGroup);
            this.historyMultiRowTemplate1.GeneralCodeTextBoxCell.DataField = nameof(TestCarHistoryComplete.GeneralCode);
            this.historyMultiRowTemplate1.CarNameTextBoxCell.DataField = nameof(TestCarHistoryComplete.CarName);
            this.historyMultiRowTemplate1.StartDateTextBoxCell.DataField = nameof(TestCarHistoryComplete.StartDate);
            this.historyMultiRowTemplate1.EndDateTextBoxCell.DataField = nameof(TestCarHistoryComplete.EndDate);
            this.historyMultiRowTemplate1.DescriptionTextBoxCell.DataField = nameof(TestCarHistoryComplete.Description);
            this.historyMultiRowTemplate1.ReservedPersonTextBoxCell.DataField = nameof(TestCarHistoryComplete.ReservedPerson);

            this.HistoryMultiRow.Template = this.historyMultiRowTemplate1;

            this.HistoryMultiRow.Template = new CustomTemplate()
            { MultiRow = HistoryMultiRow }.SetContextMenuTemplate(this.HistoryMultiRow.Template);
            
            InitForm(true, false);

            this.WorkCompletionDateTimePicker.ValueChanged += WorkCompletionDateTimePicker_ValueChanged;
        }

        private void WorkCompletionDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            this.isEdit = true;
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm(bool isGeneralCode, bool isAllUser)
        {
            var scheduleList = new List<TestCarCompleteScheduleModel>();
            {
                var search = new TestCarCompleteScheduleSearchModel()
                {
                    FromDate = this.StartDateDateTimePicker.Value,
                    PERSONEL_ID = SessionDto.UserId,
                    GeneralCodeFlg = this.UserAuthority.ALL_GENERAL_CODE_FLG == '1'
                };

                if (isGeneralCode)
                {
                    search.GENERAL_CODE = this.GeneralCode;
                }

                if (!isAllUser)
                {
                    search.予約者_ID = SessionDto.UserId;
                }

                var res = HttpUtil.GetResponse<TestCarCompleteScheduleSearchModel, TestCarCompleteScheduleModel>(
                    ControllerType.TestCarHistoryComplete, search);

                if (res != null && res.Status == Const.StatusSuccess)
                {
                    scheduleList.AddRange(res.Results);
                }
            }

            var dataList = new List<TestCarHistoryComplete>();
            foreach (var item in scheduleList)
            {
                dataList.Add(new TestCarHistoryComplete()
                {
                    Id = item.ID,
                    CategoryId = item.CATEGORY_ID,
                    StartDateTime = item.START_DATE,
                    CarGroup = item.CAR_GROUP,
                    GeneralCode = item.GENERAL_CODE,
                    CarName = item.CATEGORY,
                    Description = item.DESCRIPTION,
                    StartDate = item.START_DATE.ToString("yy/MM/dd HH:mm"),
                    EndDate = item.END_DATE.ToString("yy/MM/dd HH:mm"),
                    ReservedPerson = item.予約者_SECTION_CODE + " " + item.予約者_NAME
                });
            }
            
            var list = new List<TextBox>()
            {
                this.OdometerTextBox,
                this.TestContentTextBox,
                this.DesorptionPpartsTextBox,
                this.DesorptionPpartsTextBox,
                this.ChangePlaceTextBox,
                this.CarStorageLocationTextBox,
                this.KeyStorageLocationTextBox
            };

            this.WorkCompletionDateTimePicker.Value = null;
            isEdit = false;

            foreach (var item in list)
            {
                item.BackColor = Const.DefaultBackColor;
                item.Text = string.Empty;
                item.Modified = false;
            }

            this.HistoryMultiRow.ClearSelection();
            this.HistoryMultiRow.DataSource = dataList;

            this.HistoryMultiRow.CurrentCell = null;

            if (isAutoFit)
            {
                FormControlUtil.FormWait(this, () =>
                {
                    foreach (var row in this.HistoryMultiRow.Rows)
                    {
                        row.Cells[this.historyMultiRowTemplate1.CarNameTextBoxCell.Name].PerformVerticalAutoFit();
                    }
                });
            }
        }

        /// <summary>
        /// スケジュールデータ画面セット
        /// </summary>
        /// <param name="schedule"></param>
        private void SetSchedule(TestCarScheduleModel schedule)
        {
            this.WorkCompletionDateTimePicker.Value = schedule.完了日 ?? this.WorkCompletionDateTimePicker.Value;
            this.OdometerTextBox.Text = schedule.オドメータ ?? this.OdometerTextBox.Text;
            this.TestContentTextBox.Text = schedule.試験内容 ?? this.TestContentTextBox.Text;
            this.DesorptionPpartsTextBox.Text = schedule.脱着部品 ?? this.DesorptionPpartsTextBox.Text;
            this.ChangePlaceTextBox.Text = schedule.変更箇所 ?? this.ChangePlaceTextBox.Text;
            this.CarStorageLocationTextBox.Text = schedule.車両保管場所 ?? this.CarStorageLocationTextBox.Text;
            this.KeyStorageLocationTextBox.Text = schedule.鍵保管場所 ?? this.KeyStorageLocationTextBox.Text;
            
            this.ActiveControl = this.HistoryMultiRow;
        }

        /// <summary>
        /// スケジュールデータ更新有無チェック
        /// </summary>
        private bool IsEditSchedule()
        {
            var list = new List<TextBox>()
            {
                this.OdometerTextBox,
                this.TestContentTextBox,
                this.DesorptionPpartsTextBox,
                this.DesorptionPpartsTextBox,
                this.ChangePlaceTextBox,
                this.CarStorageLocationTextBox,
                this.KeyStorageLocationTextBox
            };
            
            foreach(var item in list)
            {
                if (item.Modified)
                {
                    return true;
                }
            }

            return isEdit;
        }

        /// <summary>
        /// 他車系表示チェックボックスクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InitForm(!SearchCheckBox.Checked, SearchUserCheckBox.Checked);
        }

        /// <summary>
        /// 他予約者も表示するチェックボックスチェック時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchUserCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InitForm(!SearchCheckBox.Checked, SearchUserCheckBox.Checked);
        }

        /// <summary>
        /// 登録ボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            if (this.HistoryMultiRow.CurrentCell == null || this.HistoryMultiRow.CurrentCell is FilteringTextBoxCell)
            {
                Messenger.Warn(Resources.KKM00009);
                return;
            }

            var map = new Dictionary<Control, Func<Control, string, string>>();

            if (this.WorkCompletionDateTimePicker.Value != null)
            {
                Func<TextBox, string, string> isRequired = (text, name) =>
                    string.IsNullOrWhiteSpace(text.Text) == false ? "" : string.Format(Resources.KKM00001, name);

                map[this.OdometerTextBox] = (c, name) => isRequired((c as TextBox), name);
                map[this.TestContentTextBox] = (c, name) => isRequired((c as TextBox), name);
                map[this.DesorptionPpartsTextBox] = (c, name) => isRequired((c as TextBox), name);
                map[this.ChangePlaceTextBox] = (c, name) => isRequired((c as TextBox), name);
                map[this.CarStorageLocationTextBox] = (c, name) => isRequired((c as TextBox), name);
                map[this.KeyStorageLocationTextBox] = (c, name) => isRequired((c as TextBox), name);
            }
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                errorRowIndex = selectIndex;
                return;
            }

            var item = ((TestCarHistoryComplete)this.HistoryMultiRow.Rows[this.HistoryMultiRow.CurrentCell.RowIndex].DataBoundItem);
            var schedule = GetSchedule(item.Id);
            schedule.完了日 = this.WorkCompletionDateTimePicker.SelectedDate;
            schedule.オドメータ = this.OdometerTextBox.Text;
            schedule.試験内容 = this.TestContentTextBox.Text;
            schedule.脱着部品 = this.DesorptionPpartsTextBox.Text;
            schedule.変更箇所 = this.ChangePlaceTextBox.Text;
            schedule.車両保管場所 = this.CarStorageLocationTextBox.Text;
            schedule.鍵保管場所 = this.KeyStorageLocationTextBox.Text;

            var res = HttpUtil.PutResponse(ControllerType.TestCarSchedule, new[] { schedule });            
            if (res != null && res.Status == Const.StatusSuccess)
            {
                Messenger.Info(Resources.KKM00002);
                isRequiredReload = true;

                InitForm(!SearchCheckBox.Checked, SearchUserCheckBox.Checked);
            }
        }

        /// <summary>
        /// 一覧表示行クリック時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryMultiRow_CellContentClick(object sender, CellEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                selectIndex = e.RowIndex;
                errorRowIndex = null;
                var item = (TestCarHistoryComplete)((GcMultiRow)sender).Rows[selectIndex.Value].DataBoundItem;
                var schedule = GetSchedule(item.Id);
                SetSchedule(schedule);
            }
        }

        /// <summary>
        /// スケジュールデータ取得処理
        /// </summary>
        /// <param name="id"></param>
        private TestCarScheduleModel GetSchedule(long id)
        {
            var res = HttpUtil.GetResponse<TestCarScheduleSearchModel, TestCarScheduleModel>(ControllerType.TestCarSchedule,
                    new TestCarScheduleSearchModel()
                    {
                        ID = id
                    });
            if (res != null && res.Status == Const.StatusSuccess)
            {
                return res.Results.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 閉じるボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void CloseButton_Click(object sender, EventArgs e)
        {
            if (IsEditSchedule())
            {
                if (Messenger.Confirm(Resources.KKM00006) == DialogResult.Yes)
                {
                    EntryButton_Click(null, null);
                }
                else
                {
                    base.CloseButton_Click(sender, e);
                }
            }
            else
            {
                base.CloseButton_Click(sender, e);
            }
        }

        /// <summary>
        /// 検索日時変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartDateDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            InitForm(!SearchCheckBox.Checked, SearchUserCheckBox.Checked);
        }

        /// <summary>
        /// 検索日時変更時処理（キーボード入力不可）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartDateDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// 内容を展開する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoFitLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            isAutoFit = true;
            FormControlUtil.FormWait(this, () =>
            {
                foreach (var row in this.HistoryMultiRow.Rows)
                {
                    row.Cells[this.historyMultiRowTemplate1.CarNameTextBoxCell.Name].PerformVerticalAutoFit();
                }
            });
        }

        /// <summary>
        /// セルダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                var row = ((GcMultiRow)sender).Rows[e.RowIndex];
                var selectItem = ((TestCarHistoryComplete)row.DataBoundItem);

                this.ResultScheduleInfo = new Result()
                {
                    GeneralCode = selectItem.GeneralCode,
                    CategoryId = selectItem.CategoryId,
                    FirstDateTime = selectItem.StartDateTime
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// 選択行変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryMultiRow_RowLeave(object sender, CellEventArgs e)
        {
            if (e.Scope == CellScope.Row && this.HistoryMultiRow.ContainsFocus)
            {
                if (IsEditSchedule())
                {
                    if (Messenger.Confirm(Resources.KKM00006) == DialogResult.Yes)
                    {
                        EntryButton_Click(null, null);
                    }
                }
            }
        }

        /// <summary>
        /// 行フォーカス取得時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryMultiRow_RowEnter(object sender, CellEventArgs e)
        {
            if (errorRowIndex.HasValue)
            {
                this.HistoryMultiRow.AddSelection(errorRowIndex.Value);
                errorRowIndex = null;
            }
            else
            {
                selectIndex = e.RowIndex;
            }
        }
    }

    /// <summary>
    /// 作業完了簡易グリッドデータ
    /// </summary>
    internal class TestCarHistoryComplete
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string GeneralCode { get; set; }

        /// <summary>
        /// 車系
        /// </summary>
        public string CarGroup { get; set; }

        /// <summary>
        /// 車両名
        /// </summary>
        public string CarName { get; set; }

        /// <summary>
        /// 期間開始
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 期間終了
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// スケジュール名
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 予約者
        /// </summary>
        public string ReservedPerson { get; set; }

        /// <summary>
        /// カテゴリID
        /// </summary>
        public long CategoryId { get; internal set; }

        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime StartDateTime { get; internal set; }
    }
}
