using DevPlan.Presentation.Base;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    /// <summary>
    /// 使用履歴・作業完了未記録一覧フォーム
    /// </summary>
    public partial class TestCarHistoryErrorForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "注意"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>エラーリスト</summary>
        public List<HistoryError> HistoryErrorList { get; internal set; }

        /// <summary>カレンダーセル項目ごとの描画後の処理のデリゲート</summary>
        public Action<Result> Reload { get; set; } = (item) => { };

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCarHistoryErrorForm()
        {
            InitializeComponent();

            // TODO : タスクバーへ表示する（本来はもうサブではなくBaseFormへ切り替え予定）
            this.ShowInTaskbar = true;
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarHistoryErrorForm_Load(object sender, EventArgs e)
        {
            var res = HttpUtil.GetResponse<CarManagerSearchModel, CarManagerModel>(ControllerType.CarManager,
                    new CarManagerSearchModel { STATUS = "全" });

            var list = new List<CarManagerModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            this.ErrorMessageLabel.Text = string.Format(this.ErrorMessageLabel.Text, SessionDto.UserName, list.FirstOrDefault()?.REMARKS);

            this.historyErrorListMultiRowTemplate1.GeneralCodeTextBoxCell.DataField = nameof(HistoryError.GeneralCode);
            this.historyErrorListMultiRowTemplate1.CarNameTextBoxCell.DataField = nameof(HistoryError.CarName);
            this.historyErrorListMultiRowTemplate1.StartEndDateTextBoxCell.DataField = nameof(HistoryError.StartEndDate);
            this.historyErrorListMultiRowTemplate1.DescriptionTextBoxCell.DataField = nameof(HistoryError.Description);

            this.HistoryErrorListMultiRow.Template = this.historyErrorListMultiRowTemplate1;
            this.HistoryErrorListMultiRow.DataSource = HistoryErrorList;

            for (int i = 0; i < this.HistoryErrorListMultiRow.RowCount; i++)
            {
                this.HistoryErrorListMultiRow.Rows[i][0].PerformVerticalAutoFit();
            }

            this.HistoryErrorListMultiRow.ClearSelection();
        }
        
        /// <summary>
        /// セルダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryErrorListMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                var row = ((GcMultiRow)sender).Rows[e.RowIndex];
                var selectItem = ((HistoryError)row.DataBoundItem);
                
                Reload(new Result()
                {
                    GeneralCode = selectItem.GeneralCode,
                    CategoryId = selectItem.CategoryId,
                    FirstDateTime = selectItem.StartDateTime
                });
            }
        }

        /// <summary>
        /// フォーム初回表示時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarHistoryErrorForm_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }
    }

    /// <summary>
    /// 作業履歴エラークラス
    /// </summary>
    public class HistoryError
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long ScheduleId { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string GeneralCode { get; set; }

        /// <summary>
        /// 車両名
        /// </summary>
        public string CarName { get; set; }

        /// <summary>
        /// 予約コメント
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 予約期間
        /// </summary>
        public string StartEndDate { get; set; }

        /// <summary>
        /// カテゴリID
        /// </summary>
        public long CategoryId { get; internal set; }

        /// <summary>
        /// スケジュール開始日
        /// </summary>
        public DateTime StartDateTime { get; internal set; }
    }   
    
    /// <summary>
    /// 選択されたスケジュール情報
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GeneralCode { get; internal set; }

        /// <summary>
        /// カテゴリID
        /// </summary>
        public long CategoryId { get; internal set; }

        /// <summary>
        /// スケジュール表示開始
        /// </summary>
        public DateTime FirstDateTime { get; internal set; }
    } 
}
