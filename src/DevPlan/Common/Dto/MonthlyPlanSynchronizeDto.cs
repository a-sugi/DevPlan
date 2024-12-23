using System;

namespace DevPlan.UICommon.Dto
{
    #region API 共通モデル
    /// <summary>
    /// 月次計画同期登録入力モデルクラス
    /// </summary>
    public class MonthlyWorkSynchronizePostInModel
    {
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
        /// 月頭月末
        /// </summary>
        public int? FLAG_月頭月末 { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }
    #endregion
}