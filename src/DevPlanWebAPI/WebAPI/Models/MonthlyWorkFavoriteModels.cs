using System;
using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    #region お気に入り（月次計画）検索入力モデルクラス
    /// <summary>
    /// お気に入り（業務計画）検索入力
    /// </summary>
    public class MonthlyWorkFavoriteSearchInModel
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

    #region お気に入り（月次計画）検索出力モデルクラス
    /// <summary>
    /// お気に入り（業務計画）検索出力モデルクラス
    /// </summary>
    public class MonthlyWorkFavoriteSearchOutModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(20)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 所属区分
        /// </summary>
        [Display(Name = "所属区分")]
        public string CLASS_KBN { get; set; }
        /// <summary>
        /// 所属ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "所属ID")]
        public string CLASS_ID { get; set; }
        /// <summary>
        /// 担当者
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当者")]
        public string MANAGER_NAME { get; set; }
        /// <summary>
        /// ステータス_OPENフラグ
        /// </summary>
        [Display(Name = "ステータス_OPENフラグ")]
        public string STATUS_OPEN_FLG { get; set; }
        /// <summary>
        /// ステータス_CLOSEフラグ
        /// </summary>
        [Display(Name = "ステータス_CLOSEフラグ")]
        public string STATUS_CLOSE_FLG { get; set; }
        /// <summary>
        /// 指定月
        /// </summary>
        [Display(Name = "指定月")]
        public DateTime? TARGET_MONTH { get; set; }
        /// <summary>
        /// 表示対象
        /// </summary>
        [Display(Name = "表示対象")]
        public string DISPLAY_KBN { get; set; }
    }
    #endregion

    #region お気に入り（月次計画）登録モデルクラス
    /// <summary>
    /// お気に入り（業務計画）登録モデルクラス
    /// </summary>
    public class MonthlyWorkFavoriteItemModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "タイトル")]
        public string TITLE { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(15)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 所属区分
        /// </summary>
        [Display(Name = "所属区分")]
        public char CLASS_KBN { get; set; }
        /// <summary>
        /// 所属ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "所属ID")]
        public string CLASS_ID { get; set; }
        /// <summary>
        /// 担当者
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当者")]
        public string MANAGER_NAME { get; set; }
        /// <summary>
        /// ステータス_OPENフラグ
        /// </summary>
        [Display(Name = "ステータス_OPENフラグ")]
        public char STATUS_OPEN_FLG { get; set; }
        /// <summary>
        /// ステータス_CLOSEフラグ
        /// </summary>
        [Display(Name = "ステータス_CLOSEフラグ")]
        public char STATUS_CLOSE_FLG { get; set; }
        /// <summary>
        /// 指定月
        /// </summary>
        [Display(Name = "指定月")]
        public DateTime? TARGET_MONTH { get; set; }
        /// <summary>
        /// 表示対象
        /// </summary>
        [Display(Name = "表示対象")]
        public char DISPLAY_KBN { get; set; }
    }
    #endregion
}