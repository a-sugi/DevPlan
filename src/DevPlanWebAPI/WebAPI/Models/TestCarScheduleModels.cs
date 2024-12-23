using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 試験車スケジュール項目検索条件クラス
    /// <summary>
    /// 試験車スケジュール項目検索条件クラス
    /// </summary>
    [Serializable]
    public class TestCarScheduleItemSearchModel : IValidatableObject
    {
        #region プロパティ
        /// <summary>ID</summary>
        [MaxLength(1000)]
        [Display(Name = "ID")]
        public long?[] ID { get; set; }

        /// <summary>開発符号</summary>
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>試作時期</summary>
        [StringLength(20)]
        [Display(Name = "試作時期")]
        public string 試作時期 { get; set; }

        /// <summary>号車</summary>
        [StringLength(50)]
        [Display(Name = "号車")]
        public string 号車 { get; set; }

        /// <summary>車型</summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }

        /// <summary>仕向地</summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }

        /// <summary>Openフラグ</summary>
        [Display(Name = "Openフラグ")]
        public bool? OPEN_FLG { get; set; }
        #endregion

        #region 検証
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //ID未指定時で開発符号未指定はエラー
            if ((this.ID == null || this.ID.Any() == false) && string.IsNullOrEmpty(this.GENERAL_CODE) == true)
            {
                yield return new ValidationResult("開発符号が未入力です。", new[] { "GENERAL_CODE" });

            }

        }
        #endregion
    }
    #endregion
    
    #region 試験車スケジュール検索条件クラス
    /// <summary>
    /// 試験車スケジュール検索条件クラス
    /// </summary>
    [Serializable]
    public class TestCarScheduleSearchModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long? ID { get; set; }

        /// <summary>試験車日程種別</summary>
        [StringLength(10)]
        [Display(Name = "試験車日程種別")]
        public string 試験車日程種別 { get; set; }

        /// <summary>カテゴリーID</summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "カテゴリーID")]
        public long? CATEGORY_ID { get; set; }

        /// <summary>期間(From)</summary>
        [DataType(DataType.Date)]
        [Display(Name = "期間(From)")]
        public DateTime? START_DATE { get; set; }

        /// <summary>期間(To)</summary>
        [DataType(DataType.Date)]
        [Display(Name = "期間(To)")]
        public DateTime? END_DATE { get; set; }

        /// <summary>行番号</summary>
        [Range(Const.ScheduleRowMin, Const.ScheduleRowMax)]
        [Display(Name = "行番号")]
        public int? PARALLEL_INDEX_GROUP { get; set; }


        /// <summary>開発符号</summary>
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>試作時期</summary>
        [StringLength(20)]
        [Display(Name = "試作時期")]
        public string 試作時期 { get; set; }

        /// <summary>号車</summary>
        [StringLength(50)]
        [Display(Name = "号車")]
        public string 号車 { get; set; }

        /// <summary>車型</summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }

        /// <summary>仕向地</summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }

        /// <summary>Openフラグ</summary>
        [Display(Name = "Openフラグ")]
        public bool? OPEN_FLG { get; set; }

        /// <summary>本予約取得フラグ</summary>
        /// <remarks>本予約をセットで取得する場合はTrueを指定</remarks>
        public bool IsGetKettei { get; set; }

        /// <summary>設定者_ID</summary>
        [StringLength(20)]
        public string 設定者_ID { get; set; }
        #endregion
    }
    #endregion

    #region 試験車スケジュール項目クラス
    /// <summary>
    /// 試験車スケジュール項目クラス
    /// </summary>
    [Serializable]
    public class TestCarScheduleItemModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>開発符号</summary>
        [Required]
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>試験車名</summary>
        [Required]
        [StringLength(Const.ScheduleCategoryLength)]
        [Display(Name = "試験車名")]
        public string CATEGORY { get; set; }

        /// <summary>ソート順</summary>
        [Range(Const.ScheduleSortMin, Const.ScheduleSortMax)]
        [Display(Name = "ソート順")]
        public double? SORT_NO { get; set; }

        /// <summary>課グループID</summary>
        [Required]
        [StringLength(Const.SectionGroupIdLength)]
        [Display(Name = "課グループID")]
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>入力者パーソナルID</summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "入力者パーソナルID")]
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>終了日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "終了日")]
        public DateTime? CLOSED_DATE { get; set; }

        /// <summary>カテゴリーID</summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "カテゴリーID")]
        public long? CATEGORY_ID { get; set; }

        /// <summary>行数</summary>
        [Required]
        [Range(Const.ScheduleRowMin, Const.ScheduleRowMax)]
        [Display(Name = "行数")]
        public int? PARALLEL_INDEX_GROUP { get; set; }

        /// <summary>管理票番号</summary>
        [StringLength(10)]
        [Display(Name = "管理票番号")]
        public string 管理票番号 { get; set; }

        /// <summary>駐車場番号</summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>車系</summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string 車系 { get; set; }

        /// <summary>試作時期</summary>
        [StringLength(20)]
        [Display(Name = "試作時期")]
        public string 試作時期 { get; set; }

        /// <summary>号車</summary>
        [StringLength(50)]
        [Display(Name = "号車")]
        public string 号車 { get; set; }

        /// <summary>FLAG_ナビ付</summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ナビ付")]
        public short? FLAG_ナビ付 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ETC付")]
        public short? FLAG_ETC付 { get; set; }

        /// <summary>XEYE_EXIST</summary>
        [StringLength(10)]
        [Display(Name = "XEYE_EXIST")]
        public string XEYE_EXIST { get; set; }
        #endregion
    }
    #endregion

    #region 試験車スケジュールクラス
    /// <summary>
    /// 試験車スケジュールクラス
    /// </summary>
    [Serializable]
    public class TestCarScheduleModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>開発符号</summary>
        [Required]
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>開始日</summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "開始日")]
        public DateTime? START_DATE { get; set; }

        /// <summary>終了日</summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "終了日")]
        public DateTime? END_DATE { get; set; }

        /// <summary>スケジュール名</summary>
        [Required]
        [StringLength(Const.ScheduleCommentLength)]
        [Display(Name = "スケジュール名")]
        public string DESCRIPTION { get; set; }

        /// <summary>区分</summary>
        [Required]
        [Range(Const.ScheduleSymbolMin, Const.ScheduleSymbolMax)]
        [Display(Name = "区分")]
        public short? SYMBOL { get; set; }

        /// <summary>課グループID</summary>
        [Required]
        [StringLength(Const.SectionGroupIdLength)]
        [Display(Name = "課グループID")]
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>登録日時</summary>
        [Display(Name = "登録日時")]
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>入力者パーソナルID</summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "入力者パーソナルID")]
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>カテゴリーID</summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "カテゴリーID")]
        public long? CATEGORY_ID { get; set; }

        /// <summary>予約種別</summary>
        [Required]
        [StringLength(Const.ScheduleReservationTypeLength)]
        [Display(Name = "予約種別")]
        public string 予約種別 { get; set; }

        /// <summary>試験車日程種別</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "試験車日程種別")]
        public string 試験車日程種別 { get; set; }

        /// <summary>コピー元_ID</summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "コピー元_ID")]
        public long? コピー元_ID { get; set; }

        /// <summary>行番号</summary>
        [Required]
        [Range(Const.ScheduleRowMin, Const.ScheduleRowMax)]
        [Display(Name = "行番号")]
        public int? PARALLEL_INDEX_GROUP { get; set; }

        /// <summary>試験内容</summary>
        [StringLength(500)]
        [Display(Name = "試験内容")]
        public string 試験内容 { get; set; }

        /// <summary>完了日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "完了日")]
        public DateTime? 完了日 { get; set; }

        /// <summary>オドメータ</summary>
        [StringLength(20)]
        [Display(Name = "オドメータ")]
        public string オドメータ { get; set; }

        /// <summary>脱着部品</summary>
        [StringLength(500)]
        [Display(Name = "脱着部品")]
        public string 脱着部品 { get; set; }

        /// <summary>変更箇所</summary>
        [StringLength(500)]
        [Display(Name = "変更箇所")]
        public string 変更箇所 { get; set; }

        /// <summary>車両保管場所</summary>
        [StringLength(500)]
        [Display(Name = "車両保管場所")]
        public string 車両保管場所 { get; set; }

        /// <summary>鍵保管場所</summary>
        [StringLength(500)]
        [Display(Name = "鍵保管場所")]
        public string 鍵保管場所 { get; set; }

        /// <summary>設定者_ID</summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "設定者_ID")]
        public string 設定者_ID { get; set; }

        /// <summary>設定者_課コード</summary>
        public string 設定者_SECTION_CODE { get; set; }

        /// <summary>設定者_NAME</summary>
        public string 設定者_NAME { get; set; }

        /// <summary>予約者_ID</summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "予約者_ID")]
        public string 予約者_ID { get; set; }

        /// <summary>予約者_課コード</summary>
        public string 予約者_SECTION_CODE { get; set; }

        /// <summary>予約者_NAME</summary>
        public string 予約者_NAME { get; set; }

        /// <summary>課題フォローリストID</summary>
        [Range(Const.ScheduleHistoryIdMin, Const.ScheduleHistoryIdMax)]
        [Display(Name = "課題フォローリストID")]
        public decimal? FOLLOWLIST_ID { get; set; }
        #endregion
    }
    #endregion
}