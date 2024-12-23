using System;

namespace DevPlan.UICommon.Dto
{
    #region 週報項目検索条件クラス
    /// <summary>
    /// 週報項目検索条件クラス
    /// </summary>
    [Serializable]
    public class WeeklyReportSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 期間
        /// </summary>
        public DateTime? WEEKEND_DATE { get; set; }
        /// <summary>
        /// 作成単位
        /// </summary>
        public string 作成単位 { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        #endregion
    }
    #endregion

    #region 週報項目条件クラス
    /// <summary>
    /// 週報項目条件クラス
    /// </summary>
    [Serializable]
    public class WeeklyReportModel
    {
        #region プロパティ
        /// <summary>
        /// 週報ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 担当
        /// </summary>
        public string SECTION_GROUP_CODE_情報元 { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 項目
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 現状
        /// </summary>
        public string CURRENT_SITUATION { get; set; }
        /// <summary>
        /// 今後の予定
        /// </summary>
        public string FUTURE_SCHEDULE { get; set; }
        /// <summary>
        /// ソート番号
        /// </summary>
        public float? SORT_NO { get; set; }
        /// <summary>
        /// 期間
        /// </summary>
        public DateTime? WEEKEND_DATE { get; set; }
        /// <summary>
        /// 作成単位
        /// </summary>
        public string 作成単位 { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string INPUT_PERSONEL_ID { get; set; }
        /// <summary>
        /// 承認者ID
        /// </summary>
        public string ISSUED_PERSONEL_ID { get; set; }
        /// <summary>
        /// 承認日時
        /// </summary>
        public DateTime? ISSUED_DATETIME { get; set; }
        #endregion
    }
    #endregion
}
