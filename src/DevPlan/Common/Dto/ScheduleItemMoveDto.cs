namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 項目コピー・移動（項目移動）モデルクラス（入力）
    /// </summary>
    public class ScheduleItemMoveInModel
    {
        /// <summary>
        /// 項目ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 移動先開発符号（もしくは移動先車系）
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 項目種別
        /// </summary>
        public ScheduleItemType SCHEDULE_ITEM_TYPE { get; set; }
    }

    /// <summary>
    /// 項目種別Enum
    /// </summary>
    public enum ScheduleItemType
    {
        /// <summary>
        /// カーシェア
        /// </summary>
        CarShare = 0,

        /// <summary>
        /// 外製車
        /// </summary>
        OuterCar = 1,

        /// <summary>
        /// 試験車
        /// </summary>
        TestCar = 2
    }
}