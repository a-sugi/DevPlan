using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 月次計画承認検索入力モデルクラス
    /// </summary>
    public class MonthlyWorkApprovalGetInModel
    {
        /// <summary>
        /// 所属グループID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "SECTION_GROUP_ID")]
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 対象月
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "対象月")]
        public DateTime? 対象月 { get; set; }
        /// <summary>
        /// 月頭月末
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_月頭月末")]
        public int? FLAG_月頭月末 { get; set; }
    }
    /// <summary>
    /// 月次計画承認検索出力モデルクラス
    /// </summary>
    public class MonthlyWorkApprovalGetOutModel
    {
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 対象月
        /// </summary>
        public DateTime? 対象月 { get; set; }
        /// <summary>
        /// 月頭月末
        /// </summary>
        public int? FLAG_月頭月末 { get; set; }
        /// <summary>
        /// 承認
        /// </summary>
        public int? FLAG_承認 { get; set; }
        /// <summary>
        /// 承認日時
        /// </summary>
        public DateTime? 承認日時 { get; set; }
        /// <summary>
        /// 承認者ID
        /// </summary>
        public string 承認者_PERSONEL_ID { get; set; }
        /// <summary>
        /// 承認者 
        /// </summary>
        public string 承認者_NAME { get; set; }
        /// <summary>
        /// 職場コード
        /// </summary>
        public string 承認者_SECTION_CODE { get; set; }
    }
    /// <summary>
    /// 月次計画承認登録入力モデルクラス
    /// </summary>
    public class MonthlyWorkApprovalPostInModel
    {
        /// <summary>
        /// 所属グループID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "SECTION_GROUP_ID")]
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 対象月
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "対象月")]
        public DateTime? 対象月 { get; set; }
        /// <summary>
        /// 月頭月末
        /// </summary>
        [Required]
        [Range(0, 9)]
        [Display(Name = "FLAG_月頭月末")]
        public int? FLAG_月頭月末 { get; set; }
        /// <summary>
        /// 承認
        /// </summary>
        [Required]
        [Range(0, 9)]
        [Display(Name = "FLAG_承認")]
        public int? FLAG_承認 { get; set; }
        /// <summary>
        /// 承認者ID
        /// </summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "承認者_PERSONEL_ID")]
        public string 承認者_PERSONEL_ID { get; set; }
    }
}