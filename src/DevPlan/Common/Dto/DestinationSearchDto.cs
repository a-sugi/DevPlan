namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 仕向地検索入力モデルクラス
    /// </summary>
    public class DestinationInModel
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
    /// 仕向地検索出力モデルクラス
    /// </summary>
    public class DestinationOutModel
    {
        /// <summary>
        /// 仕向地
        /// </summary>
        public string 仕向地 { get; set; }
    }
}