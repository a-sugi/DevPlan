using System;

namespace DevPlan.UICommon.Dto
{
    #region API 共通モデル
    /// <summary>
    /// 月次計画承認検索入力モデルクラス
    /// </summary>
    public class MonthlyWorkApprovalGetInModel
    {
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
    }
    /// <summary>
    /// 月次計画承認検索出力モデルクラス
    /// </summary>
    public class MonthlyWorkApprovalGetOutModel
    {
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
        /// 承認
        /// </summary>
        public int? FLAG_承認 { get; set; }
        /// <summary>
        /// 承認日時
        /// </summary>
        public DateTime? 承認日時 { get; set; }
        /// <summary>
        /// 承認者ID
        /// </summary>
        public string 承認者_PERSONEL_ID { get; set; }
        /// <summary>
        /// 承認者 
        /// </summary>
        public string 承認者_NAME { get; set; }
        /// <summary>
        /// 職場コード
        /// </summary>
        public string 承認者_SECTION_CODE { get; set; }
    }
    /// <summary>
    /// 月次計画承認登録入力モデルクラス
    /// </summary>
    public class MonthlyWorkApprovalPostInModel
    {
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
        /// 承認
        /// </summary>
        public int? FLAG_承認 { get; set; }
        /// <summary>
        /// 承認者ID
        /// </summary>
        public string 承認者_PERSONEL_ID { get; set; }
    }
    #endregion
}