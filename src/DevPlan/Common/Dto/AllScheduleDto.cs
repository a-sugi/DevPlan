using System;

namespace DevPlan.UICommon.Dto
{
    #region スケジュール利用車検索条件クラス
    /// <summary>
    /// スケジュール利用車検索条件クラス
    /// </summary>
    public class AllScheduleSearchModel
    {
        /// <summary>管理票番号</summary>
        public string 管理票番号 { get; set; }

        /// <summary>期間(From)</summary>
        public DateTime? START_DATE { get; set; }

        /// <summary>期間(To)</summary>
        public DateTime? END_DATE { get; set; }
    }
    #endregion

    #region スケジュール利用車項目クラス
    /// <summary>
    /// スケジュール利用車項目クラス
    /// </summary>
    public class AllScheduleModel
    {
        /// <summary>日程区分</summary>
        public string SCHEDULE_TYPE { get; set; }

        /// <summary>ID</summary>
        public long ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>開始日</summary>
        public DateTime? START_DATE { get; set; }

        /// <summary>終了日</summary>
        public DateTime? END_DATE { get; set; }

        /// <summary>カテゴリーID</summary>
        public long? CATEGORY_ID { get; set; }

        /// <summary>行番号</summary>
        public int? PARALLEL_INDEX_GROUP { get; set; }
    }
    #endregion
}