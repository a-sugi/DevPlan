using System;

namespace DevPlan.UICommon.Dto
{
    #region お気に入り（カーシェア）検索入力モデルクラス
    /// <summary>
    /// お気に入り（カーシェア）検索入力
    /// </summary>
    public class CarShareFavoriteSearchInModel
    {
        /// <summary>
        /// お気に入りID
        /// </summary>
        public long ID { get; set; }
    }
    #endregion

    #region お気に入り（カーシェア）検索出力モデルクラス
    /// <summary>
    /// お気に入り（カーシェア）検索出力モデルクラス
    /// </summary>
    public class CarShareFavoriteSearchOutModel
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
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
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

    #region お気に入り（カーシェア）登録モデルクラス
    /// <summary>
    /// お気に入り（カーシェア）登録モデルクラス
    /// </summary>
    public class CarShareFavoriteItemModel
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