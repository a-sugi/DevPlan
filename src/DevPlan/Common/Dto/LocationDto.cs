namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 所在地検索入力モデルクラス
    /// </summary>
    public class LocationSearchInModel
    {
        /// <summary>
        /// 事業ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 職場ID
        /// </summary>
        public string SECTION_ID { get; set; }
    }
    /// <summary>
    /// 所在地検索出力モデルクラス
    /// </summary>
    public class LocationSearchOutModel
    {
        /// <summary>
        /// 所在地
        /// </summary>
        public string 所在地 { get; set; }
    }
}