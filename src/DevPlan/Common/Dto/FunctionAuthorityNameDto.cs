namespace DevPlan.UICommon.Dto
{
    #region 機能権限一覧検索入力モデル
    /// <summary>
    /// 機能権限一覧検索入力モデルクラス
    /// </summary>
    public class FunctionAuthorityNameInModel
    {
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// ユーザー名
        /// </summary>
        public string PERSONEL_NAME { get; set; }
    }
    #endregion

    # region 機能権限一覧検索出力モデル
    /// <summary>
    /// 機能権限一覧検索出力モデルクラス
    /// </summary>
    public class FunctionAuthorityNameOutModel
    {
        /// <summary>
        /// 権限対象名
        /// </summary>
        public string TARGET { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 権限レイヤー
        /// </summary>
        public short LAYER { get; set; }
    }
    #endregion
}