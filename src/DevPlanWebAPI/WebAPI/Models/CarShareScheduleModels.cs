using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region カーシェアスケジュール項目検索条件クラス
    /// <summary>
    /// カーシェアスケジュール項目検索条件クラス
    /// </summary>
    [Serializable]
    public class CarShareScheduleItemSearchModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [MaxLength(1000)]
        [Display(Name = "ID")]
        public long?[] ID { get; set; }

        /// <summary>空車期間(From)</summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "空車期間(From)")]
        public DateTime? BLANK_START_DATE { get; set; }

        /// <summary>空車期間(To)</summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "空車期間(To)")]
        public DateTime? BLANK_END_DATE { get; set; }

        /// <summary>Openフラグ</summary>        
        [Display(Name = "Openフラグ")]
        public bool? OPEN_FLG { get; set; }

        /// <summary>車系</summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string CAR_GROUP { get; set; }

        /// <summary>管理票No</summary>
        [StringLength(10)]
        [Display(Name = "管理票No")]
        public string 管理票番号 { get; set; }

        /// <summary>駐車場番号</summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>所在地</summary>
        [StringLength(20)]
        [Display(Name = "所在地")]
        public string 所在地 { get; set; }

        /// <summary>車型</summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }

        /// <summary>仕向地</summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ETC付")]
        public short? FLAG_ETC付 { get; set; }

        /// <summary>トランスミッション</summary>
        [StringLength(50)]
        [Display(Name = "トランスミッション")]
        public string トランスミッション { get; set; }

        /// <summary>予約者ID</summary>
        public string INPUT_PERSONEL_ID { get; set; }
        #endregion

        #region 検証
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //ID未指定時で車系未指定はエラー
            if ((this.ID == null || this.ID.Any() == false) && string.IsNullOrEmpty(this.CAR_GROUP) == true)
            {
                yield return new ValidationResult("車系が未入力です。", new[] { "CAR_GROUP" });

            }

        }
        #endregion
    }
    #endregion

    #region カーシェアスケジュール検索条件クラス
    /// <summary>
    /// カーシェアスケジュール検索条件クラス
    /// </summary>
    [Serializable]
    public class CarShareScheduleSearchModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long? ID { get; set; }

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


        /// <summary>空車期間(From)</summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "空車期間(From)")]
        public DateTime? BLANK_START_DATE { get; set; }

        /// <summary>空車期間(To)</summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "空車期間(To)")]
        public DateTime? BLANK_END_DATE { get; set; }

        /// <summary>Openフラグ</summary>        
        [Display(Name = "Openフラグ")]
        public bool? OPEN_FLG { get; set; }

        /// <summary>車系</summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string CAR_GROUP { get; set; }

        /// <summary>管理票No</summary>
        [StringLength(10)]
        [Display(Name = "管理票No")]
        public string 管理票番号 { get; set; }

        /// <summary>駐車場番号</summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>所在地</summary>
        [StringLength(20)]
        [Display(Name = "所在地")]
        public string 所在地 { get; set; }

        /// <summary>車型</summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }

        /// <summary>仕向地</summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ETC付")]
        public short? FLAG_ETC付 { get; set; }

        /// <summary>トランスミッション</summary>
        [StringLength(50)]
        [Display(Name = "トランスミッション")]
        public string トランスミッション { get; set; }

        #endregion
    }
    #endregion

    #region カーシェアスケジュール項目クラス
    /// <summary>
    /// カーシェアスケジュール項目クラス
    /// </summary>
    [Serializable]
    public class CarShareScheduleItemModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>車系</summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string CAR_GROUP { get; set; }

        /// <summary>開発符号</summary>
        [Required]
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>車両名</summary>
        [StringLength(Const.ScheduleCategoryLength)]
        [Display(Name = "車両名")]
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

        /// <summary>FLAG_要予約許可</summary>
        [Range(0, 9)]
        [Display(Name = "要予約許可")]
        public short? FLAG_要予約許可 { get; set; }

        /// <summary>駐車場番号</summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>最終予約可能日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "最終予約可能日")]
        public DateTime? 最終予約可能日 { get; set; }

        /// <summary>FLAG_ナビ付</summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ナビ付")]
        public short? FLAG_ナビ付 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ETC付")]
        public short? FLAG_ETC付 { get; set; }

        /// <summary>入力者部ID</summary>
        [StringLength(10)]
        [Display(Name = "入力者部ID")]
        public string INPUT_DEPARTMENT_ID { get; set; }

        /// <summary>入力者課ID</summary>
        [StringLength(20)]
        [Display(Name = "入力者課ID")]
        public string INPUT_SECTION_ID { get; set; }

        /// <summary>入力者担当ID</summary>
        [StringLength(20)]
        [Display(Name = "入力者担当ID")]
        public string INPUT_SECTION_GROUP_ID { get; set; }

        /// <summary>XEYE_EXIST</summary>
        [StringLength(10)]
        [Display(Name = "XEYE_EXIST")]
        public string XEYE_EXIST { get; set; }

        //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
        /// <summary>入れ替え中車両</summary>
        [Display(Name = "入れ替え中車両")]
        public string 入れ替え中車両 { get; set; }
        //Append End 2022/01/17 杉浦 入れ替え中車両の処理

        #endregion
    }
    #endregion

    #region カーシェアスケジュールクラス
    /// <summary>
    /// カーシェアスケジュールクラス
    /// </summary>
    [Serializable]
    public class CarShareScheduleModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>車系</summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string CAR_GROUP { get; set; }

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

        /// <summary>タイトル</summary>
        [Required]
        [StringLength(Const.ScheduleCommentLength)]
        [Display(Name = "タイトル")]
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

        /// <summary>行番号</summary>
        [Required]
        [Range(Const.ScheduleRowMin, Const.ScheduleRowMax)]
        [Display(Name = "行番号")]
        public int? PARALLEL_INDEX_GROUP { get; set; }

        /// <summary>目的</summary>
        [StringLength(100)]
        [Display(Name = "目的")]
        public string 目的 { get; set; }

        /// <summary>行先</summary>
        [StringLength(100)]
        [Display(Name = "行先")]
        public string 行先 { get; set; }

        /// <summary>TEL</summary>
        [StringLength(100)]
        [Display(Name = "TEL")]
        public string TEL { get; set; }

        /// <summary>利用実績</summary>
        [Range(0, 9)]
        [Display(Name = "利用実績")]
        public short? FLAG_実使用 { get; set; }

        /// <summary>空き時間貸出</summary>
        [Range(0, 9)]
        [Display(Name = "空き時間貸出")]
        public short? FLAG_空時間貸出可 { get; set; }

        /// <summary>予約者_ID</summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "予約者_ID")]
        public string 予約者_ID { get; set; }

        /// <summary>予約者_課コード</summary>
        public string 予約者_SECTION_CODE { get; set; }

        /// <summary>予約者_NAME</summary>
        public string 予約者_NAME { get; set; }

        /// <summary>FLAG_返却済</summary>
        [Range(0, 9)]
        [Display(Name = "返却済")]
        public short? FLAG_返却済 { get; set; }

        /// <summary>FLAG_要予約許可</summary>
        [Range(0, 9)]
        [Display(Name = "要予約許可")]
        public short? FLAG_要予約許可 { get; set; }

        //Append Start 2022/02/21 杉浦 入れ替え中車両の処理
        public string REPLACEMENT_TEXT { get; set; }
        //Append End 2022/02/21 杉浦 入れ替え中車両の処理
        #endregion
    }
    #endregion

    //Append Start 2021/08/25 矢作

    #region カーシェアスケジュール複数取得用クラス
    /// <summary>
    /// カーシェアスケジュール項目検索条件クラス
    /// </summary>
    [Serializable]
    public class CarShareScheduleSearchListModel
    {
        #region プロパティ
        /// <summary>IDリスト</summary>
        public long[] IDList { get; set; }
        #endregion
    }
    #endregion

    //Append End 2021/08/25 矢作
}