namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 目的検索入力モデルクラス
    /// </summary>
    public class PurposeSearchInModel
    {
        /// <summary>
        /// 目的
        /// </summary>
        public string 目的 { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public short SORT_NO { get; set; }
    }
    /// <summary>
    /// 目的検索出力モデルクラス
    /// </summary>
    public class PurposeSearchOutModel
    {
        /// <summary>
        /// 目的
        /// </summary>
        public string 目的 { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public short SORT_NO { get; set; }
    }
}