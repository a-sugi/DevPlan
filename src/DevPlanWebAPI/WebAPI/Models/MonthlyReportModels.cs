using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 月報検索モデルクラス
    /// </summary>
    [Serializable]
    public class MonthlyInModel
    {
        /// <summary>
        /// 対象月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "対象月")]
        public DateTime? MONTH_FIRST_DAY { get; set; }
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
    }

    /// <summary>
    /// 月報検索結果モデルクラス
    /// </summary>
    [Serializable]
    public class MonthlyOutModel
    {
        /// <summary>
        /// 月報ID
        /// </summary>
        [Range(0, int.MaxValue)]
        [Display(Name = "月報ID")]
        public int ID { get; set; }
        /// <summary>
        /// 項目
        /// </summary>
        [StringLength(200)]
        [Display(Name = "項目")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 進度状況
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "進度状況")]
        public string CURRENT_SITUATION { get; set; }
        /// <summary>
        /// 見通し・対応策・日程
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "見通し・対応策・日程")]
        public string FUTURE_SCHEDULE { get; set; }
        /// <summary>
        /// ソート番号
        /// </summary>
        [Range(0F, float.MaxValue)]
        [Display(Name = "ソート順")]
        public float? SORT_NO { get; set; }
        /// <summary>
        /// 対象月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "対象月")]
        public DateTime? MONTH_FIRST_DAY { get; set; }
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
        /// ユーザーID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 部長名
        /// </summary>
        [StringLength(20)]
        [Display(Name = "部長名")]
        public string MANAGER_NAME { get; set; }
    }
}