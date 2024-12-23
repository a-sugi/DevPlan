using System;

namespace DevPlan.UICommon.Dto
{
    #region 週報承認検索モデルクラス
    /// <summary>
    /// 週報承認検索モデルクラス
    /// </summary>
    [Serializable]
    public class WeeklyReportApproveInModel
    {
        #region プロパティ
        /// <summary>
        /// 期間
        /// </summary>
        public DateTime? WEEKEND_DATE { get; set; }
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

    #region 週報承認検索結果モデルクラス
    /// <summary>
    /// 週報承認検索結果モデルクラス
    /// </summary>
    public class WeeklyReportApproveOutModel
    {
        /// <summary>
        /// FLAG_承認
        /// </summary>
        public short? FLAG_承認 { get; set; }
        /// <summary>
        /// 承認日時
        /// </summary>
        public DateTime? 承認日時 { get; set; }
        /// <summary>
        /// 承認者所属
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 承認者名
        /// </summary>
        public string NAME { get; set; }

    }
    #endregion

    #region 週報承認項目クラス
    /// <summary>
    /// 週報承認項目クラス
    /// </summary>
    [Serializable]
    public class WeeklyReportApproveModel
    {
        #region プロパティ
        /// <summary>
        /// 週報ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// FLAG_承認
        /// </summary>
        public short? FLAG_承認 { get; set; }
        /// <summary>
        /// 承認者
        /// </summary>
        public string 承認者_PERSONEL_ID { get; set; }
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
}
