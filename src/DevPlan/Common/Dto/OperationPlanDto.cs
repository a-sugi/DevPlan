using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// ガントチャート形式のスケジュール格納DTO
    /// </summary>
    public class OperationPlanDto
    {
        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 日数
        /// </summary>
        public int DayCount
        {
            get { return StartDate == null || EndDate == null ? 0 : (EndDate.Value - StartDate.Value).Days + 1; }
        }

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// サブタイトル
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 状況
        /// </summary>
        public string Status { get; set; }
    }

    #region API 共通モデル
    /// <summary>
    /// 業務スケジュール項目検索入力モデルクラス
    /// </summary>
    public class WorkScheduleItemGetInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 所属
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 状態（Open/Close）
        /// </summary>
        public int? OPEN_CLOSE_FLAG { get; set; }
        /// <summary>
        /// Close日（開始）
        /// </summary>
        public DateTime? CLOSED_DATE_START { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// Close日（終了）
        /// </summary>
        public DateTime? CLOSED_DATE_END { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
    }
    /// <summary>
    /// 業務スケジュール検索出力モデルクラス
    /// </summary>
    public class WorkScheduleItemGetOutModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }

        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 職場グループコード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public double? SORT_NO { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
    }
    /// <summary>
    /// 業務スケジュール項目登録入力モデルクラス
    /// </summary>
    public class WorkScheduleItemPostInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public double SORT_NO { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 業務スケジュール項目更新入力モデルクラス
    /// </summary>
    public class WorkScheduleItemPutInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public double? SORT_NO { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 業務スケジュール項目削除入力モデルクラス
    /// </summary>
    public class WorkScheduleItemDeleteInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
    }
    /// <summary>
    /// 業務スケジュール検索入力モデルクラス
    /// </summary>
    public class WorkScheduleGetInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 所属
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 状態（Open/Close）
        /// </summary>
        public int? OPEN_CLOSE_FLAG { get; set; }
        /// <summary>
        /// Close日（開始）
        /// </summary>
        public DateTime? CLOSED_DATE_START { get; set; }
        /// <summary>
        /// Close日（終了）
        /// </summary>
        public DateTime? CLOSED_DATE_END { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        public DateTime? DATE_START { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        public DateTime? DATE_END { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
    }
    /// <summary>
    /// 業務スケジュール検索出力モデルクラス
    /// </summary>
    public class WorkScheduleGetOutModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// スケジュール区分
        /// </summary>
        public int SYMBOL { get; set; }
        /// <summary>
        /// 作業完了
        /// </summary>
        public int? END_FLAG { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime? INPUT_DATETIME { get; set; }
    }
    /// <summary>
    /// 業務スケジュール登録入力モデルクラス
    /// </summary>
    public class WorkSchedulePostInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// 作業完了
        /// </summary>
        public int END_FLAG { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// スケジュール区分
        /// </summary>
        public int SYMBOL { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 業務スケジュール更新入力モデルクラス
    /// </summary>
    public class WorkSchedulePutInModel
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// 作業完了
        /// </summary>
        public int END_FLAG { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// スケジュール区分
        /// </summary>
        public int SYMBOL { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 業務スケジュール削除入力モデルクラス
    /// </summary>
    public class WorkScheduleDeleteInModel
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
    }
    #endregion
}
