using DevPlanWebAPI.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    #region お気に入り検索モデル
    /// <summary>
    /// お気に入り検索モデルクラス
    /// </summary>
    public class FavoriteSearchInModel
    {
        /// <summary>
        /// ユーザーID(必須)
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// データ区分(必須)
        /// </summary>
        [Required]
        [StringLength(2)]
        [Display(Name = "データ区分")]
        public string CLASS_DATA { get; set; }
    }
    #endregion

    #region お気に入り検索結果モデル
    /// <summary>
    /// お気に入り検索結果モデルクラス
    /// </summary>
    public class FavoriteSearchOutModel
    {
        /// <summary>
        /// ID(必須)
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long ID { get; set; }
        /// <summary>
        /// タイトル(必須)
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "タイトル")]
        public string TITLE { get; set; }
        /// <summary>
        /// 登録日(必須)
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "登録日")]
        public DateTime? INPUT_DATETIME { get; set; }
        /// <summary>
        /// データ区分(必須)
        /// </summary>
        [Required]
        [StringLength(2)]
        [Display(Name = "データ区分")]
        public string CLASS_DATA { get; set; }
        /// <summary>
        /// 利用許可フラグ(必須)
        /// </summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "利用許可フラグ")]
        public int PERMIT_FLG { get; set; }
    }
    #endregion

    #region お気に入り更新モデル
    /// <summary>
    /// お気に入り更新モデルクラス
    /// </summary>
    public class FavoriteUpdateModel
    {
        /// <summary>
        /// ID(必須)
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long ID { get; set; }
        /// <summary>
        /// タイトル(必須)
        /// </summary>
        [Required]
        [StringLength(1000)]
        [Display(Name = "タイトル")]
        public string TITLE { get; set; }
        /// <summary>
        /// ユーザーID(必須)
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// データ区分(必須)
        /// </summary>
        [Required]
        [StringLength(2)]
        [Display(Name = "データ区分")]
        public string CLASS_DATA { get; set; }
    }
    #endregion

    #region お気に入り削除モデル
    /// <summary>
    /// お気に入り削除モデルクラス
    /// </summary>
    public class FavoriteDeleteModel
    {
        /// <summary>
        /// ID(必須)
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long ID { get; set; }
        /// <summary>
        /// データ区分(必須)
        /// </summary>
        [Required]
        [StringLength(2)]
        [Display(Name = "データ区分")]
        public string CLASS_DATA { get; set; }
    }
    #endregion
}