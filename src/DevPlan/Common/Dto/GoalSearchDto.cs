namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 行先検索入力モデルクラス
    /// </summary>
    public class GoalSearchInModel
    {
        /// <summary>
        /// 行先
        /// </summary>
        public string 行先 { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public short SORT_NO { get; set; }
    }
    /// <summary>
    /// 行先検索出力モデルクラス
    /// </summary>
    public class GoalSearchOutModel
    {
        /// <summary>
        /// 行先
        /// </summary>
        public string 行先 { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public short SORT_NO { get; set; }
    }
}