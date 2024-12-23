using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 週報検索モデルクラス
    /// </summary>
    [Serializable]
    public class WeeklyInModel
    {
        /// <summary>
        /// 期間
        /// </summary>
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

    /// <summary>
    /// 週報検索結果モデルクラス
    /// </summary>
    [Serializable]
    public class WeeklyOutModel
    {
        /// <summary>
        /// 週報ID
        /// </summary>
        [Range(0L, long.MaxValue)]
        [Display(Name = "ID")]
        public long ID { get; set; }
        /// <summary>
        /// 担当
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当")]
        public string SECTION_GROUP_CODE_情報元 { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(15)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 項目
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "項目")]
        public string CATEGORY { get; set; }
        /// <summary>
        /// 現状
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "現状")]
        public string CURRENT_SITUATION { get; set; }
        /// <summary>
        /// 今後の予定
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "今後の予定")]
        public string FUTURE_SCHEDULE { get; set; }
        /// <summary>
        /// ソート番号
        /// </summary>
        [Range(0F, float.MaxValue)]
        [Display(Name = "ソート順")]
        public float? SORT_NO { get; set; }
        /// <summary>
        /// 期間
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "期間")]
        public DateTime? WEEKEND_DATE { get; set; }
        /// <summary>
        /// 作成単位
        /// </summary>
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
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string INPUT_PERSONEL_ID { get; set; }
        /// <summary>
        /// 承認者ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "承認者ID")]
        public string ISSUED_PERSONEL_ID { get; set; }
        /// <summary>
        /// 承認日時
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "承認日時")]
        public DateTime? ISSUED_DATETIME { get; set; }
    }
}