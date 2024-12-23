namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 車両使用者検索入力モデルクラス
    /// </summary>
    public class CarSearchInModel
    {
        /// <summary>
        /// 事業コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 課グループコード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 名前
        /// </summary>
        public string NAME { get; set; }
    }
    /// <summary>
    /// 車両使用者検索出力モデルクラス
    /// </summary>
    public class CarSearchOutModel
    {
        /// <summary>
        /// 事業コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 課グループコード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 名前
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 使用者ID
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }
}