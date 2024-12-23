using System;

namespace DevPlan.UICommon.Dto
{
    #region 月報項目検索条件クラス
    /// <summary>
    /// 月報項目検索条件クラス
    /// </summary>
    [Serializable]
    public class MonthyReportSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 対象月
        /// </summary>
        public DateTime? MONTH_FIRST_DAY { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        #endregion
    }
    #endregion

    #region 月報項目条件クラス
    /// <summary>
    /// 月報項目条件クラス
    /// </summary>
    [Serializable]
    public class MonthlyReportModel
    {
        #region プロパティ
        /// <summary>
        /// 月報ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 項目
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 進度状況
        /// </summary>
        public string CURRENT_SITUATION { get; set; }
        /// <summary>
        /// 見通し・対応策・日程
        /// </summary>
        public string FUTURE_SCHEDULE { get; set; }
        /// <summary>
        /// ソート番号
        /// </summary>
        public float? SORT_NO { get; set; }
        /// <summary>
        /// 対象月
        /// </summary>
        public DateTime? MONTH_FIRST_DAY { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 部長名
        /// </summary>
        public string MANAGER_NAME { get; set; }
        #endregion
    }
    #endregion
}
