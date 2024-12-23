using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 行先検索入力モデルクラス
    /// </summary>
    public class GoalSearchInModel
    {
        /// <summary>
        /// 行先
        /// </summary>
        [StringLength(20)]
        [Display(Name = "行先")]
        public string 行先 { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        [Range(0L, 999L)]
        [Display(Name = "並び順")]
        public short SORT_NO { get; set; }
    }
    /// <summary>
    /// 行先検索出力モデルクラス
    /// </summary>
    public class GoalSearchOutModel
    {
        /// <summary>
        /// 行先
        /// </summary>
        [StringLength(20)]
        [Display(Name = "行先")]
        public string 行先 { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        [Range(0L, 999L)]
        [Display(Name = "並び順")]
        public short SORT_NO { get; set; }
    }
}