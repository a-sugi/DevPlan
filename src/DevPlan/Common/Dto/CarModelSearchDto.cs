namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 車型検索入力モデルクラス
    /// </summary>
    public class CarModelInModel
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
        /// 研実管理廃却フラグ
        /// </summary>
        public int 研実管理廃却フラグ { get; set; }
        /// <summary>
        /// 車両搬出フラグ
        /// </summary>
        public int 車両搬出フラグ { get; set; }
        /// <summary>
        /// 廃却決済承認フラグ
        /// </summary>
        public int 廃却決済承認フラグ { get; set; }
    }
    /// <summary>
    /// 車型検索出力モデルクラス
    /// </summary>
    public class CarModelOutModel
    {
        /// <summary>
        /// 車型
        /// </summary>
        public string 車型 { get; set; }
    }
}