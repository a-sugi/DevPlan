using System;

namespace DevPlan.UICommon.Dto
{
    #region お気に入り（試験車）検索入力モデルクラス
    /// <summary>
    /// お気に入り（試験車）検索入力
    /// </summary>
    public class TestCarFavoriteSearchInModel
    {
        /// <summary>
        /// お気に入りID
        /// </summary>
        public long ID { get; set; }
    }
    #endregion

    #region お気に入り（試験車）検索出力モデルクラス
    /// <summary>
    /// お気に入り（試験車）検索出力モデルクラス
    /// </summary>
    public class TestCarFavoriteSearchOutModel
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
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string TRIAL_TIME { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string GOSHA { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string DESTINATION { get; set; }
        /// <summary>
        /// ステータス区分(1:使用部署要望案, 2:SJSB調整案, 3:最終調整結果)
        /// </summary>
        public string STATUS_KBN { get; set; }
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

    #region お気に入り（試験車）登録モデルクラス
    /// <summary>
    /// お気に入り（試験車）登録モデルクラス
    /// </summary>
    public class TestCarFavoriteItemModel
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
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string TRIAL_TIME { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string GOSHA { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string DESTINATION { get; set; }
        /// <summary>
        /// ステータス区分(1:使用部署要望案, 2:SJSB調整案, 3:最終調整結果)
        /// </summary>
        public string STATUS_KBN { get; set; }
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