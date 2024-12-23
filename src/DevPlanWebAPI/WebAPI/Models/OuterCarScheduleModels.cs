using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 外製車日程検索入力モデルクラス
    /// </summary>
    public class OuterCarScheduleGetInModel
    {
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
        //Update Start 2023/03/10 杉浦 外製車日程の駐車場番号の受け取りサイズを拡張する
        //[StringLength(10)]
        [StringLength(50)]
        //Update End 2023/03/10 杉浦 外製車日程の駐車場番号の受け取りサイズを拡張する
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
    /// 外製車日程検索出力モデルクラス
    /// </summary>
    public class OuterCarScheduleGetOutModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
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
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// スケジュール区分
        /// </summary>
        public int SYMBOL { get; set; }
        /// <summary>
        /// 予約種別
        /// </summary>
        public string 予約種別 { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime? INPUT_DATETIME { get; set; }
        /// <summary>
        /// 目的
        /// </summary>
        public string 目的 { get; set; }
        /// <summary>
        /// 行先
        /// </summary>
        public string 行先 { get; set; }
        /// <summary>
        /// TEL
        /// </summary>
        public string TEL { get; set; }
        /// <summary>
        /// FLAG_実使用
        /// </summary>
        public int? FLAG_実使用 { get; set; }
        /// <summary>
        /// 予約者_ID
        /// </summary>
        public string 予約者_ID { get; set; }
        /// <summary>
        /// 予約者_SECTION_CODE
        /// </summary>
        public string 予約者_SECTION_CODE { get; set; }
        /// <summary>
        /// 予約者_NAME
        /// </summary>
        public string 予約者_NAME { get; set; }
        /// <summary>
        /// 管理票番号
        /// </summary>
        public string 管理票番号 { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// FLAG_空時間貸出可
        /// </summary>
        public int? FLAG_空時間貸出可 { get; set; }

        /// <summary>FLAG_要予約許可</summary>
        public short? FLAG_要予約許可 { get; set; }
    }
    /// <summary>
    /// 外製車日程登録入力モデルクラス
    /// </summary>
    public class OuterCarSchedulePostInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "CATEGORY_ID")]
        public long CATEGORY_ID { get; set; }
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
        /// 予約種別
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "予約種別")]
        public string 予約種別 { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
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
        /// 目的
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "目的")]
        public string 目的 { get; set; }
        /// <summary>
        /// 行先
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "行先")]
        public string 行先 { get; set; }
        /// <summary>
        /// 使用者TEL
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "TEL")]
        public string TEL { get; set; }
        /// <summary>
        /// 空時間貸出可フラグ
        /// </summary>
        [Required]
        [Range(0, 9)]
        [Display(Name = "FLAG_空時間貸出可")]
        public int FLAG_空時間貸出可 { get; set; }
        /// <summary>
        /// 予約者ID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "予約者_ID")]
        public string 予約者_ID { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        //Update Start 2023/03/10 杉浦 外製車日程の駐車場番号の受け取りサイズを拡張する
        //[StringLength(10)]
        [StringLength(50)]
        //Update End 2023/03/10 杉浦 外製車日程の駐車場番号の受け取りサイズを拡張する
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }
    }
    /// <summary>
    /// 外製車日程更新入力モデルクラス
    /// </summary>
    public class OuterCarSchedulePutInModel
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "SCHEDULE_ID")]
        public long SCHEDULE_ID { get; set; }
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
        /// 予約種別
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "予約種別")]
        public string 予約種別 { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "PERSONEL_ID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 目的
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "目的")]
        public string 目的 { get; set; }
        /// <summary>
        /// 行先
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "行先")]
        public string 行先 { get; set; }
        /// <summary>
        /// 使用者TEL
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "TEL")]
        public string TEL { get; set; }
        /// <summary>
        /// 空時間貸出可フラグ
        /// </summary>
        [Required]
        [Range(0, 9)]
        [Display(Name = "FLAG_空時間貸出可")]
        public int FLAG_空時間貸出可 { get; set; }
        /// <summary>
        /// 予約者ID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "予約者_ID")]
        public string 予約者_ID { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        //Update Start 2023/03/10 杉浦 外製車日程の駐車場番号の受け取りサイズを拡張する
        //[StringLength(10)]
        [StringLength(50)]
        //Update End 2023/03/10 杉浦 外製車日程の駐車場番号の受け取りサイズを拡張する
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }
    }
    /// <summary>
    /// 外製車日程削除入力モデルクラス
    /// </summary>
    public class OuterCarScheduleDeleteInModel
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