using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// ガントチャート形式のスケジュール格納DTO
    /// </summary>
    public class MonthlyPlanDto
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
    /// 業務（月次計画）スケジュール項目検索入力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleItemGetInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 所属ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 対象月
        /// </summary>
        public DateTime? 対象月 { get; set; }
        /// <summary>
        /// 状態（Open/Close）
        /// </summary>
        public int? OPEN_CLOSE_FLAG { get; set; }
        /// <summary>
        /// 月頭月末
        /// </summary>
        public int? FLAG_月頭月末 { get; set; }
        /// <summary>
        /// 担当者
        /// </summary>
        public string 担当者 { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
    }
    /// <summary>
    /// 業務（月次計画）スケジュール検索出力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleItemGetOutModel
    {
        /// <summary>
        /// カテゴリーID（月次）
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// カテゴリーID（業務）
        /// </summary>
        public long? DEV_CATEGORY_ID { get; set; }
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
        /// 担当者
        /// </summary>
        public string 担当者 { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string 備考 { get; set; }
        /// <summary>
        /// 対象月
        /// </summary>
        public DateTime? 対象月 { get; set; }
        /// <summary>
        /// 月頭月末
        /// </summary>
        public int? FLAG_月頭月末 { get; set; }
        /// <summary>
        /// 月報専用項目フラグ
        /// </summary>
        public int? FLAG_月報専用項目 { get; set; }
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
    /// 業務（月次計画）スケジュール項目登録入力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleItemPostInModel
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
        /// <summary>
        /// 担当者
        /// </summary>
        public string 担当者 { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string 備考 { get; set; }
        /// <summary>
        /// 対象月
        /// </summary>
        public DateTime? 対象月 { get; set; }
        /// <summary>
        /// 月頭月末
        /// </summary>
        public int? FLAG_月頭月末 { get; set; }
    }
    /// <summary>
    /// 業務（月次計画）スケジュール項目更新入力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleItemPutInModel
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
        /// <summary>
        /// 担当者
        /// </summary>
        public string 担当者 { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string 備考 { get; set; }
    }
    /// <summary>
    /// 業務（月次計画）スケジュール項目削除入力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleItemDeleteInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
    }
    /// <summary>
    /// 業務（月次計画）スケジュール検索入力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleGetInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 所属ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 対象月
        /// </summary>
        public DateTime? 対象月 { get; set; }
        /// <summary>
        /// 状態（Open/Close）
        /// </summary>
        public int? OPEN_CLOSE_FLAG { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        public DateTime? DATE_START { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        public DateTime? DATE_END { get; set; }
        /// <summary>
        /// 月頭月末
        /// </summary>
        public int? FLAG_月頭月末 { get; set; }
        /// <summary>
        /// 担当者
        /// </summary>
        public string 担当者 { get; set; }
        /// <summary>
        /// 行番号
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
    /// 業務（月次計画）スケジュール検索出力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleGetOutModel
    {
        /// <summary>
        /// カテゴリーID（月次）
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// カテゴリーID（業務）
        /// </summary>
        public long DEV_CATEGORY_ID { get; set; }
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
    /// 業務（月次計画）スケジュール登録入力モデルクラス
    /// </summary>
    public class MonthlyWorkSchedulePostInModel
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
    /// 業務（月次計画）スケジュール更新入力モデルクラス
    /// </summary>
    public class MonthlyWorkSchedulePutInModel
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
    /// 業務（月次計画）スケジュール削除入力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleDeleteInModel
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
    }
    #endregion
}