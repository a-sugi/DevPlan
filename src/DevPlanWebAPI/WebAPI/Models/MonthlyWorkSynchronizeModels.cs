using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 月次計画同期登録入力モデルクラス
    /// </summary>
    public class MonthlyWorkSynchronizePostInModel
    {
        /// <summary>
        /// 所属ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "SECTION_ID")]
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
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
        /// パーソナルID
        /// </summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "PERSONEL_ID")]
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 月次計画同期登録出力モデルクラス
    /// </summary>
    public class MonthlyWorkSynchronizePostOutModel
    {
        /// <summary>
        /// 所属ID
        /// </summary>
        public string SECTION_ID { get; set; }
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
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }
}