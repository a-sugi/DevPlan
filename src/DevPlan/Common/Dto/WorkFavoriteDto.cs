using System;

namespace DevPlan.UICommon.Dto
{
    #region お気に入り（業務計画）検索入力モデルクラス
    /// <summary>
    /// お気に入り（業務計画）検索入力
    /// </summary>
    public class WorkFavoriteSearchInModel
    {
        /// <summary>
        /// お気に入りID
        /// </summary>
        public long ID { get; set; }
    }
    #endregion

    #region お気に入り（業務計画）検索出力モデルクラス
    /// <summary>
    /// お気に入り（業務計画）検索出力モデルクラス
    /// </summary>
    public class WorkFavoriteSearchOutModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 所属区分
        /// </summary>
        public string CLASS_KBN { get; set; }
        /// <summary>
        /// 所属ID
        /// </summary>
        public string CLASS_ID { get; set; }
        /// <summary>
        /// ステータス_OPENフラグ
        /// </summary>
        public string STATUS_OPEN_FLG { get; set; }
        /// <summary>
        /// ステータス_CLOSEフラグ
        /// </summary>
        public string STATUS_CLOSE_FLG { get; set; }
    }
    #endregion

    #region お気に入り（業務計画）登録モデルクラス
    /// <summary>
    /// お気に入り（業務計画）登録モデルクラス
    /// </summary>
    public class WorkFavoriteItemModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 所属区分
        /// </summary>
        public char CLASS_KBN { get; set; }
        /// <summary>
        /// 所属ID
        /// </summary>
        public string CLASS_ID { get; set; }
        /// <summary>
        /// ステータス_OPENフラグ
        /// </summary>
        public char STATUS_OPEN_FLG { get; set; }
        /// <summary>
        /// ステータス_CLOSEフラグ
        /// </summary>
        public char STATUS_CLOSE_FLG { get; set; }
    }
    #endregion
}