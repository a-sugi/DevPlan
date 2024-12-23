namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// TM検索入力モデルクラス
    /// </summary>
    public class TransmissionSearchInModel
    {
        /// <summary>
        /// 事業ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 職場ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 自社フラグ
        /// </summary>
        public int 自社フラグ { get; set; }
        /// <summary>
        /// ナンバー登録フラグ
        /// </summary>
        public int ナンバー登録フラグ { get; set; }
        /// <summary>
        /// 占有者フラグ
        /// </summary>
        public int 占有者フラグ { get; set; }
        /// <summary>
        /// 最新履歴フラグ
        /// </summary>
        public int 最新履歴フラグ { get; set; }
    }
    /// <summary>
    /// TM検索出力モデルクラス
    /// </summary>
    public class TransmissionSearchOutModel
    {
        /// <summary>
        /// トランスミッション
        /// </summary>
        public string トランスミッション { get; set; }
    }
}