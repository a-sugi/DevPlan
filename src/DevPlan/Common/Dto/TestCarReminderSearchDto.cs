namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 注意喚起検索入力モデルクラス
    /// </summary>
    public class TestCarReminderSearchInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
    }
    /// <summary>
    /// 注意喚起検索出力モデルクラス
    /// </summary>
    public class TestCarReminderSearchOutModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 注意喚起フラグ
        /// </summary>
        public int ALERT_FLAG { get; set; }
    }
}