namespace DevPlan.UICommon.Dto
{
    #region お気に入り（履歴関連）検索入力モデルクラス
    /// <summary>
    /// お気に入り（履歴関連）検索入力
    /// </summary>
    public class HistoryFavoriteSearchInModel
    {
        /// <summary>
        /// お気に入りID
        /// </summary>
        public long? ID { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 履歴区分
        /// </summary>
        public string HISTORY_CODE { get; set; }
    }
    #endregion

    #region お気に入り（履歴関連）検索出力モデルクラス
    /// <summary>
    /// お気に入り（履歴関連）検索出力モデルクラス
    /// </summary>
    public class HistoryFavoriteSearchOutModel
    {
        /// <summary>
        /// お気に入りID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        public string TITLE { get; set; }

        /// <summary>
        /// 履歴ID
        /// </summary>
        public long HISTORY_ID { get; set; }
    }
    #endregion

    #region お気に入り（履歴関連）登録モデルクラス
    /// <summary>
    /// お気に入り（履歴関連）登録モデルクラス
    /// </summary>
    public class HistoryFavoriteItemModel
    {
        /// <summary>
        /// タイトル
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 履歴区分
        /// </summary>
        public string HISTORY_CODE { get; set; }
        /// <summary>
        /// 履歴ID
        /// </summary>
        public long HISTORY_ID { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 作成ユーザーID
        /// </summary>
        public string INPUT_PERSONEL_ID { get; set; }
    }
    #endregion
}