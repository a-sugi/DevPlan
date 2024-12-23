using System;

namespace DevPlan.UICommon.Dto
{
    #region お気に入り（外製車）検索入力モデルクラス
    /// <summary>
    /// お気に入り（外製車）検索入力
    /// </summary>
    public class OuterCarFavoriteSearchInModel
    {
        /// <summary>
        /// お気に入りID
        /// </summary>
        public long ID { get; set; }
    }
    #endregion

    #region お気に入り（外製車）検索出力モデルクラス
    /// <summary>
    /// お気に入り（外製車）検索出力モデルクラス
    /// </summary>
    public class OuterCarFavoriteSearchOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        public string MAKER { get; set; }
        /// <summary>
        /// 車名
        /// </summary>
        public string CAR_NAME { get; set; }
        /// <summary>
        /// 管理表No.
        /// </summary>
        public string MANAGEMENT_NO{ get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string PARKING_NO { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        public string PLACE { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string DESTINATION { get; set; }
        /// <summary>
        /// ETC_有
        /// </summary>
        public string ETC_ARI_FLG { get; set; }
        /// <summary>
        /// ETC_無
        /// </summary>
        public string ETC_NASHI_FLG { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        public string TRANSMISSION { get; set; }
        /// <summary>
        /// ステータス_OPENフラグ
        /// </summary>
        public string STATUS_OPEN_FLG { get; set; }
        /// <summary>
        /// ステータス_CLOSEフラグ
        /// </summary>
        public string STATUS_CLOSE_FLG { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime INPUT_DATETIME { get; set; }
    }
    #endregion

    #region お気に入り（外製車）登録モデルクラス
    /// <summary>
    /// お気に入り（外製車）登録モデルクラス
    /// </summary>
    public class OuterCarFavoriteItemModel
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
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        public string MAKER { get; set; }
        /// <summary>
        /// 車名
        /// </summary>
        public string CAR_NAME { get; set; }
        /// <summary>
        /// 管理表No.
        /// </summary>
        public string MANAGEMENT_NO { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string PARKING_NO { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        public string PLACE { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string DESTINATION { get; set; }
        /// <summary>
        /// ETC_有
        /// </summary>
        public string ETC_ARI_FLG { get; set; }
        /// <summary>
        /// ETC_無
        /// </summary>
        public string ETC_NASHI_FLG { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        public string TRANSMISSION { get; set; }
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
}