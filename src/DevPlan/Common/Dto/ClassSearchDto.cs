namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 種別検索入力モデルクラス
    /// </summary>
    public class ClassSearchInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 種別
        /// </summary>
        public string 種別 { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public int? SORT_NO { get; set; }
    }

    /// <summary>
    /// 種別検索出力モデルクラス
    /// </summary>
    public class ClassSearchOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// 種別
        /// </summary>
        public string 種別 { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public int? SORT_NO { get; set; }
    }
}