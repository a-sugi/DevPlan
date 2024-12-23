namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// NPOIモデルクラス
    /// </summary>
    public class RangeAddressModel
    {
        /// <summary>
        /// メッセージID
        /// </summary>
        public int FirstRow { get; set; }
        /// <summary>
        /// メッセージ
        /// </summary>
        public int LastRow { get; set; }
        /// <summary>
        /// メッセージ
        /// </summary>
        public int FirstColumn { get; set; }
        /// <summary>
        /// メッセージ
        /// </summary>
        public int LastColumn { get; set; }
    }
}