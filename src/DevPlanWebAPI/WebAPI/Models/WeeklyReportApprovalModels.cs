using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 週報承認検索モデルクラス
    /// </summary>
    [Serializable]
    public class WeeklyReportApproveInModel
    {
        /// <summary>
        /// 期間
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "期間")]
        public DateTime? WEEKEND_DATE { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "課ID")]
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "担当ID")]
        public string SECTION_GROUP_ID { get; set; }
    }

    /// <summary>
    /// 週報承認検索結果モデルクラス
    /// </summary>
    [Serializable]
    public class WeeklyReportApproveOutModel
    {
        /// <summary>
        /// FLAG_承認
        /// </summary>
        [Range(0, short.MaxValue)]
        [Display(Name = "FLAG_承認")]
        public short? FLAG_承認 { get; set; }
        /// <summary>
        /// 承認日時
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "承認日時")]
        public DateTime? 承認日時 { get; set; }
        /// <summary>
        /// 承認者所属
        /// </summary>
        [StringLength(20)]
        [Display(Name = "承認者所属")]
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 承認者名
        /// </summary>
        [StringLength(20)]
        [Display(Name = "承認者名")]
        public string NAME { get; set; }
    }

    /// <summary>
    /// 週報承認登録モデルクラス
    /// </summary>
    [Serializable]
    public class WeeklyReportApproveModel
    {
        /// <summary>
        /// 週報ID
        /// </summary>
        [Required]
        [Range(0L, long.MaxValue)]
        [Display(Name = "ID")]
        public long ID { get; set; }
        /// <summary>
        /// FLAG_承認
        /// </summary>
        [Required]
        [Range(0, short.MaxValue)]
        [Display(Name = "FLAG_承認")]
        public short? FLAG_承認 { get; set; }
        /// <summary>
        /// 承認者
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "承認者")]
        public string 承認者_PERSONEL_ID { get; set; }
        /// <summary>
        /// 期間
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "期間")]
        public DateTime? WEEKEND_DATE { get; set; }
        /// <summary>
        /// 作成単位
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "作成単位")]
        public string 作成単位 { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "課ID")]
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "担当ID")]
        public string SECTION_GROUP_ID { get; set; }
    }
}