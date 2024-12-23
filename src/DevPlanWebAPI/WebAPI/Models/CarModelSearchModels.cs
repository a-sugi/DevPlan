using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 車型検索入力モデルクラス
    /// </summary>
    public class CarModelInModel
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
        /// <summary>
        /// 研実管理廃却フラグ
        /// </summary>
        [Range(0, 1)]
        [Display(Name = "研実管理廃却フラグ")]
        public int 研実管理廃却フラグ { get; set; }
        /// <summary>
        /// 車両搬出フラグ
        /// </summary>
        [Range(0, 1)]
        [Display(Name = "車両搬出フラグ")]
        public int 車両搬出フラグ { get; set; }
        /// <summary>
        /// 廃却決済承認フラグ
        /// </summary>
        [Range(0, 1)]
        [Display(Name = "廃却決済承認フラグ")]
        public int 廃却決済承認フラグ { get; set; }
    }
    /// <summary>
    /// 車型検索出力モデルクラス
    /// </summary>
    public class CarModelOutModel
    {
        /// <summary>
        /// 車型(必須)
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }
    }
}