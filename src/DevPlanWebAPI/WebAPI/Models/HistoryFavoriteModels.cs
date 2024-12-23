using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    #region お気に入り（履歴関連）検索入力モデルクラス
    /// <summary>
    /// お気に入り（履歴関連）検索入力
    /// </summary>
    public class HistoryFavoriteSearchInModel : IValidatableObject
    {
        /// <summary>
        /// お気に入りID
        /// </summary>
        [Range(0L, 9999999999L)]
        [Display(Name = "お気に入りID")]
        public long? ID { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 履歴区分
        /// </summary>
        [StringLength(1)]
        [Display(Name = "履歴区分")]
        public string HISTORY_CODE { get; set; }

        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //必須条件チェック①
            if (ID == null)
            {
                //必須条件チェック②
                if (string.IsNullOrWhiteSpace(PERSONEL_ID) || string.IsNullOrWhiteSpace(HISTORY_CODE))
                {
                    yield return new ValidationResult("ユーザーIDと履歴区分が必要です。", new[] { "PERSONEL_ID", "HISTORY_CODE" });
                }
            }
        }
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
        [Range(0L, 9999999999L)]
        [Display(Name = "お気に入りID")]
        public long ID { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "タイトル")]
        public string TITLE { get; set; }

        /// <summary>
        /// 履歴ID
        /// </summary>
        [Display(Name = "履歴ID")]
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
        [Required]
        [StringLength(100)]
        [Display(Name = "タイトル")]
        public string TITLE { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 履歴区分
        /// </summary>
        [Required]
        [StringLength(1)]
        [Display(Name = "履歴区分")]
        public string HISTORY_CODE { get; set; }
        /// <summary>
        /// 履歴ID
        /// </summary>
        [Required]
        [Range(0L, 9999999999L)]
        [Display(Name = "履歴ID")]
        public long HISTORY_ID { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(20)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 作成ユーザーID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "作成ユーザーID")]
        public string INPUT_PERSONEL_ID { get; set; }
    }
    #endregion
}