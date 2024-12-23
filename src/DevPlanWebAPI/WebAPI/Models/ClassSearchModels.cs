using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 種別検索入力モデルクラス
    /// </summary>
    public class ClassSearchInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int? ID { get; set; }
        /// <summary>
        /// 種別
        /// </summary>
        [StringLength(30)]
        [Display(Name = "種別")]
        public string 種別 { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "並び順")]
        public int? SORT_NO { get; set; }
    }
    /// <summary>
    /// 種別検索出力モデルクラス
    /// </summary>
    public class ClassSearchOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        /// <summary>
        /// 種別
        /// </summary>
        [StringLength(30)]
        [Display(Name = "種別")]
        public string 種別 { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "並び順")]
        public int? SORT_NO { get; set; }
    }
}