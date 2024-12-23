using System;
using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
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
        [Required]
        [Range(0L, 9999999999L)]
        [Display(Name = "お気に入りID")]
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
        [StringLength(100)]
        [Display(Name = "タイトル")]
        public string TITLE { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// 管理表No.
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理表No.")]
        public string MANAGEMENT_NO{ get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string PARKING_NO { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        [StringLength(20)]
        [Display(Name = "所在地")]
        public string PLACE { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string DESTINATION { get; set; }
        /// <summary>
        /// ETC_有
        /// </summary>
        [StringLength(1)]
        [Display(Name = "ETC_有")]
        public string ETC_ARI_FLG { get; set; }
        /// <summary>
        /// ETC_無
        /// </summary>
        [StringLength(1)]
        [Display(Name = "ETC_無")]
        public string ETC_NASHI_FLG { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        [StringLength(50)]
        [Display(Name = "トランスミッション")]
        public string TRANSMISSION { get; set; }
        /// <summary>
        /// ステータス_OPENフラグ
        /// </summary>
        [StringLength(1)]
        [Display(Name = "ステータス_OPENフラグ")]
        public string STATUS_OPEN_FLG { get; set; }
        /// <summary>
        /// ステータス_CLOSEフラグ
        /// </summary>
        [StringLength(1)]
        [Display(Name = "ステータス_CLOSEフラグ")]
        public string STATUS_CLOSE_FLG { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "登録日")]
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
        [StringLength(100)]
        [Display(Name = "タイトル")]
        public string TITLE { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// 管理表No.
        /// </summary>
        [StringLength(10)]
        [RegularExpression("^[0-9]{1,10}$")]
        [Display(Name = "管理表No.")]
        public string MANAGEMENT_NO { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string PARKING_NO { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        [StringLength(20)]
        [Display(Name = "所在地")]
        public string PLACE { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string DESTINATION { get; set; }
        /// <summary>
        /// ETC_有
        /// </summary>
        [StringLength(1)]
        [Display(Name = "ETC_有")]
        public string ETC_ARI_FLG { get; set; }
        /// <summary>
        /// ETC_無
        /// </summary>
        [StringLength(1)]
        [Display(Name = "ETC_無")]
        public string ETC_NASHI_FLG { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        [StringLength(50)]
        [Display(Name = "トランスミッション")]
        public string TRANSMISSION { get; set; }
        /// <summary>
        /// ステータス_OPENフラグ
        /// </summary>
        [StringLength(1)]
        [Display(Name = "ステータス_OPENフラグ")]
        public string STATUS_OPEN_FLG { get; set; }
        /// <summary>
        /// ステータス_CLOSEフラグ
        /// </summary>
        [StringLength(1)]
        [Display(Name = "ステータス_CLOSEフラグ")]
        public string STATUS_CLOSE_FLG { get; set; }
    }
    #endregion
}