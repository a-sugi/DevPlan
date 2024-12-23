using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 外製車日程項目検索入力モデルクラス
    /// </summary>
    public class OuterCarScheduleItemGetInModel
    {
        /// <summary>ID</summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long? ID { get; set; }

        /// <summary>
        /// 空車期間FROM
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "EMPTY_START_DATE")]
        public DateTime? EMPTY_START_DATE { get; set; }
        /// <summary>
        /// 空車期間TO
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "EMPTY_END_DATE")]
        public DateTime? EMPTY_END_DATE { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string 車系 { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "メーカー名")]
        public string メーカー名 { get; set; }
        /// <summary>
        /// 外製車名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "外製車名")]
        public string 外製車名 { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理票NO")]
        public string 管理票NO { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        [StringLength(20)]
        [Display(Name = "所在地")]
        public string 所在地 { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }
        /// <summary>
        /// FLAG_ETC付
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ETC付")]
        public int? FLAG_ETC付 { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        [StringLength(50)]
        [Display(Name = "トランスミッション")]
        public string トランスミッション { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "SCHEDULE_ID")]
        public long SCHEDULE_ID { get; set; }
        /// <summary>Openフラグ</summary>        
        [Display(Name = "Openフラグ")]
        public bool? OPEN_FLG { get; set; }
        /// <summary>
        /// 予約者パーソナルID
        /// </summary>
        public string INPUT_PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 外製車日程項目検索出力モデルクラス
    /// </summary>
    public class OuterCarScheduleItemGetOutModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }

        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public double? SORT_NO { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? CLOSED_DATE { get; set; }
        /// <summary>
        /// FLAG_要予約許可
        /// </summary>
        public int? FLAG_要予約許可 { get; set; }
        /// <summary>
        /// 最終予約可能日
        /// </summary>
        public DateTime? 最終予約可能日 { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        public string メーカー名 { get; set; }
        /// <summary>
        /// 外製車名
        /// </summary>
        public string 外製車名 { get; set; }
        /// <summary>
        /// 登録ナンバー
        /// </summary>
        public string 登録ナンバー { get; set; }
        /// <summary>
        /// FLAG_ナビ付
        /// </summary>
        public int? FLAG_ナビ付 { get; set; }
        /// <summary>
        /// FLAG_ETC付
        /// </summary>
        public int? FLAG_ETC付 { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// XEYE_EXIST
        /// </summary>
        public string XEYE_EXIST { get; set; }
    }
    /// <summary>
    /// 外製車日程項目登録入力モデルクラス
    /// </summary>
    public class OuterCarScheduleItemPostInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        [Required]
        [StringLength(15)]
        [Display(Name = "GENERAL_CODE")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 管理票番号
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理票番号")]
        public string 管理票番号 { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        [Required]
        [StringLength(Const.ScheduleCategoryLength)]
        [Display(Name = "CATEGORY")]
        public string CATEGORY { get; set; }
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
        /// 所属グループID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "SECTION_GROUP_ID")]
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "PERSONEL_ID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 最終予約可能日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "最終予約可能日")]
        public DateTime? 最終予約可能日 { get; set; }
        /// <summary>
        /// FLAG_要予約許可
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_要予約許可")]
        public int? FLAG_要予約許可 { get; set; }
    }
    /// <summary>
    /// 外製車日程項目更新入力モデルクラス
    /// </summary>
    public class OuterCarScheduleItemPutInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "CATEGORY_ID")]
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [Required]
        [StringLength(15)]
        [Display(Name = "GENERAL_CODE")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 管理票番号
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理票番号")]
        public string 管理票番号 { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        [Required]
        [StringLength(Const.ScheduleCategoryLength)]
        [Display(Name = "CATEGORY")]
        public string CATEGORY { get; set; }
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
        /// <summary>
        /// 最終予約可能日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "最終予約可能日")]
        public DateTime? 最終予約可能日 { get; set; }
        /// <summary>
        /// 要予約許可
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "要予約許可")]
        public int? FLAG_要予約許可 { get; set; }
    }
    /// <summary>
    /// 外製車日程項目削除入力モデルクラス
    /// </summary>
    public class OuterCarScheduleItemDeleteInModel
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