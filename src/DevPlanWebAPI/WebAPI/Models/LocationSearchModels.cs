using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 所在地検索入力モデルクラス
    /// </summary>
    public class LocationSearchInModel
    {
        /// <summary>
        /// 事業ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "事業ID")]
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 職場ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "職場ID")]
        public string SECTION_ID { get; set; }
    }
    /// <summary>
    /// 所在地検索出力モデルクラス
    /// </summary>
    public class LocationSearchOutModel
    {
        /// <summary>
        /// 所在地
        /// </summary>
        public string 所在地 { get; set; }
    }
}