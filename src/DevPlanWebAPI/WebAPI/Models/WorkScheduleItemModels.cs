using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 業務スケジュール項目検索入力モデルクラス
    /// </summary>
    public class WorkScheduleItemGetInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(15)]
        [Display(Name = "GENERAL_CODE")]
        public string GENERAL_CODE { get; set; }
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
        /// 状態（Open/Close）
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "OPEN_CLOSE_FLAG")]
        public int? OPEN_CLOSE_FLAG { get; set; }
        /// <summary>
        /// Close日（開始）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "CLOSED_DATE_START")]
        public DateTime? CLOSED_DATE_START { get; set; }
        /// <summary>
        /// Close日（終了）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "CLOSED_DATE_END")]
        public DateTime? CLOSED_DATE_END { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "SCHEDULE_ID")]
        public long SCHEDULE_ID { get; set; }
    }
    /// <summary>
    /// 業務スケジュール検索出力モデルクラス
    /// </summary>
    public class WorkScheduleItemGetOutModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 職場グループコード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public double? SORT_NO { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
    }
    /// <summary>
    /// 業務スケジュール項目登録入力モデルクラス
    /// </summary>
    public class WorkScheduleItemPostInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        [Required]
        [StringLength(15)]
        [Display(Name = "GENERAL_CODE")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        [Required]
        [StringLength(Const.ScheduleCategoryLength)]
        [Display(Name = "CATEGORY")]
        public string CATEGORY { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "SECTION_GROUP_ID")]
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        [Required]
        [Range(Const.ScheduleSortMin, Const.ScheduleSortMax)]
        [Display(Name = "SORT_NO")]
        public double SORT_NO { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        [Required]
        [Range(Const.ScheduleRowMin, Const.ScheduleRowMax)]
        [Display(Name = "PARALLEL_INDEX_GROUP")]
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "PERSONEL_ID")]
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 業務スケジュール項目更新入力モデルクラス
    /// </summary>
    public class WorkScheduleItemPutInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "CATEGORY_ID")]
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        [Required]
        [StringLength(Const.ScheduleCategoryLength)]
        [Display(Name = "CATEGORY")]
        public string CATEGORY { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "SECTION_GROUP_ID")]
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        [Required]
        [Range(Const.ScheduleSortMin, Const.ScheduleSortMax)]
        [Display(Name = "SORT_NO")]
        public double? SORT_NO { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        [Required]
        [Range(Const.ScheduleRowMin, Const.ScheduleRowMax)]
        [Display(Name = "PARALLEL_INDEX_GROUP")]
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "PERSONEL_ID")]
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 業務スケジュール項目削除入力モデルクラス
    /// </summary>
    public class WorkScheduleItemDeleteInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "CATEGORY_ID")]
        public long CATEGORY_ID { get; set; }
    }
}