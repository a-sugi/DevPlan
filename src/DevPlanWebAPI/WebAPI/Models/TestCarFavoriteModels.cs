using System;
using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
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
        [Required]
        [Range(0L, 9999999999L)]
        [Display(Name = "お気に入りID")]
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
        [StringLength(20)]
        [Display(Name = "タイトル")]
        public string TITLE { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(20)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        [StringLength(20)]
        [Display(Name = "試作時期")]
        public string TRIAL_TIME { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        [StringLength(50)]
        [Display(Name = "号車")]
        public string GOSHA { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        [StringLength(50)]
        [Display(Name = "車型")]
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string DESTINATION { get; set; }
        /// <summary>
        /// ステータス区分(1:使用部署要望案, 2:SJSB調整案, 3:最終調整結果)
        /// </summary>
        [StringLength(1)]
        [Display(Name = "ステータス区分")]
        public string STATUS_KBN { get; set; }
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

    #region お気に入り（試験車）登録モデルクラス
    /// <summary>
    /// お気に入り（試験車）登録モデルクラス
    /// </summary>
    public class TestCarFavoriteItemModel
    {
        /// <summary>
        /// タイトル
        /// </summary>
        [StringLength(30)]
        [Display(Name = "タイトル")]
        public string TITLE { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(20)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        [StringLength(20)]
        [Display(Name = "試作時期")]
        public string TRIAL_TIME { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        [StringLength(50)]
        [Display(Name = "号車")]
        public string GOSHA { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        [StringLength(50)]
        [Display(Name = "車型")]
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string DESTINATION { get; set; }
        /// <summary>
        /// ステータス区分(1:使用部署要望案, 2:SJSB調整案, 3:最終調整結果)
        /// </summary>
        [StringLength(1)]
        [Display(Name = "ステータス区分")]
        public string STATUS_KBN { get; set; }
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