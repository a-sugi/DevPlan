namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// メーカー名検索入力モデルクラス
    /// </summary>
    public class MakerNameSearchInModel
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
    /// メーカー名検索出力モデルクラス
    /// </summary>
    public class MakerNameSearchOutModel
    {
        /// <summary>
        /// メーカー名
        /// </summary>
        public string メーカー名 { get; set; }
    }
}