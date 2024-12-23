using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 車両使用者検索入力モデルクラス
    /// </summary>
    public class CarSearchInModel
    {
        /// <summary>
        /// 事業コード
        /// </summary>
        [StringLength(40)]
        [Display(Name = "事業コード")]
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課コード")]
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 課グループコード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課グループコード")]
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 名前
        /// </summary>
        [StringLength(20)]
        [Display(Name = "名前コード")]
        public string NAME { get; set; }
    }
    /// <summary>
    /// 車両使用者検索出力モデルクラス
    /// </summary>
    public class CarSearchOutModel
    {
        /// <summary>
        /// 事業コード
        /// </summary>
        [StringLength(40)]
        [Display(Name = "事業コード")]
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課コード")]
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 課グループコード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課グループコード")]
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 名前
        /// </summary>
        [StringLength(20)]
        [Display(Name = "名前コード")]
        public string NAME { get; set; }
        /// <summary>
        /// 使用者ID(必須)
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "使用者ID")]
        public string PERSONEL_ID { get; set; }
    }
}