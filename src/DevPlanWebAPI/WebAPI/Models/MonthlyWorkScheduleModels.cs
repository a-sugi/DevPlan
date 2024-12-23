using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 業務（月次計画）スケジュール検索入力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleGetInModel
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
        /// 対象月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "対象月")]
        public DateTime? 対象月 { get; set; }
        /// <summary>
        /// 状態（Open/Close）
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "OPEN_CLOSE_FLAG")]
        public int? OPEN_CLOSE_FLAG { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "DATE_START")]
        public DateTime? DATE_START { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "DATE_END")]
        public DateTime? DATE_END { get; set; }
        /// <summary>
        /// 月頭月末
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_月頭月末")]
        public int? FLAG_月頭月末 { get; set; }
        /// <summary>
        /// 担当者
        /// </summary>
        [StringLength(100)]
        [Display(Name = "担当者")]
        public string 担当者 { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        [Range(Const.ScheduleRowMin, Const.ScheduleRowMax)]
        [Display(Name = "PARALLEL_INDEX_GROUP")]
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "SCHEDULE_ID")]
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// カテゴリーID
        /// </summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "CATEGORY_ID")]
        public long CATEGORY_ID { get; set; }
    }
    /// <summary>
    /// 業務（月次計画）スケジュール検索出力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleGetOutModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// スケジュール区分
        /// </summary>
        public int SYMBOL { get; set; }
        /// <summary>
        /// 作業完了
        /// </summary>
        public int? END_FLAG { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime? INPUT_DATETIME { get; set; }
    }
    /// <summary>
    /// 業務（月次計画）スケジュール登録入力モデルクラス
    /// </summary>
    public class MonthlyWorkSchedulePostInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "CATEGORY_ID")]
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// 作業完了
        /// </summary>
        [Required]
        [Range(0, 9)]
        [Display(Name = "END_FLAG")]
        public int END_FLAG { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "START_DATE")]
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "END_DATE")]
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        [Required]
        [Range(Const.ScheduleRowMin, Const.ScheduleRowMax)]
        [Display(Name = "PARALLEL_INDEX_GROUP")]
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// スケジュール区分
        /// </summary>
        [Required]
        [Range(Const.ScheduleSymbolMin, Const.ScheduleSymbolMax)]
        [Display(Name = "SYMBOL")]
        public int SYMBOL { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        [Required]
        [StringLength(Const.ScheduleCommentLength)]
        [Display(Name = "DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "PERSONEL_ID")]
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 業務（月次計画）スケジュール更新入力モデルクラス
    /// </summary>
    public class MonthlyWorkSchedulePutInModel
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "SCHEDULE_ID")]
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        [Required]
        [Range(Const.ScheduleRowMin, Const.ScheduleRowMax)]
        [Display(Name = "PARALLEL_INDEX_GROUP")]
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// 作業完了
        /// </summary>
        [Required]
        [Range(0, 9)]
        [Display(Name = "END_FLAG")]
        public int END_FLAG { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "START_DATE")]
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "END_DATE")]
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// スケジュール区分
        /// </summary>
        [Required]
        [Range(Const.ScheduleSymbolMin, Const.ScheduleSymbolMax)]
        [Display(Name = "SYMBOL")]
        public int SYMBOL { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        [Required]
        [StringLength(Const.ScheduleCommentLength)]
        [Display(Name = "DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "PERSONEL_ID")]
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 業務（月次計画）スケジュール削除入力モデルクラス
    /// </summary>
    public class MonthlyWorkScheduleDeleteInModel
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "SCHEDULE_ID")]
        public long SCHEDULE_ID { get; set; }
    }
}