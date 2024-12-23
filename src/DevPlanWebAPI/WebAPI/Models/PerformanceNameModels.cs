using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 性能名一覧入力モデルクラス
    /// </summary>
    public class PerformanceNameInModel
    {
        /// <summary>
        /// 課ID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "課ID")]
        public string SECTION_ID { get; set; }
    }
    /// <summary>
    /// 性能名一覧出力モデルクラス
    /// </summary>
    public class PerformanceNameOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 性能名
        /// </summary>
        public string 性能名 { get; set; }
    }
}