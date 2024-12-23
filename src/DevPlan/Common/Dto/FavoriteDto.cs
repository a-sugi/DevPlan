using System;

namespace DevPlan.UICommon.Dto
{
    #region お気に入り検索モデルクラス
    /// <summary>お気に入り検索モデルクラス</summary>
    public class FavoriteSearchInModel
    {
        /// <summary>
        /// ユーザーID(必須)
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// データ区分(必須)
        /// </summary>
        public string CLASS_DATA { get; set; }
    }
    #endregion

    #region お気に入り検索結果モデルクラス
    /// <summary>お気に入り検索結果モデルクラス</summary>
    public class FavoriteSearchOutModel
    {
        /// <summary>
        /// ID(必須)
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// タイトル(必須)
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime? INPUT_DATETIME { get; set; }
        /// <summary>
        /// データ区分(必須)
        /// </summary>
        public string CLASS_DATA { get; set; }
        /// <summary>
        /// 利用許可フラグ(必須)
        /// </summary>
        public int PERMIT_FLG { get; set; }
    }
    #endregion

    #region お気に入り更新モデルクラス
    /// <summary>
    /// お気に入り更新モデルクラス
    /// </summary>
    public class FavoriteUpdateModel
    {
        /// <summary>
        /// ID(必須)
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// タイトル(必須)
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// ユーザーID(必須)
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// データ区分(必須)
        /// </summary>
        public string CLASS_DATA { get; set; }
    }
    #endregion

    #region お気に入り削除モデルクラス
    /// <summary>
    /// お気に入り削除モデルクラス
    /// </summary>
    public class FavoriteDeleteModel
    {
        /// <summary>
        /// ID(必須)
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// データ区分(必須)
        /// </summary>
        public string CLASS_DATA { get; set; }
    }
    #endregion
}