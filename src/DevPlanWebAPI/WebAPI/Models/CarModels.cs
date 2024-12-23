using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region カーシェア一覧検索条件クラス
    /// <summary>
    /// カーシェア一覧検索条件クラス
    /// </summary>
    [Serializable]
    public class CarSearchModel
    {
        #region プロパティ
        /// <summary>車両区分</summary>
        [Range(1, 7)]
        [Display(Name = "車両区分")]
        public int CAR_CLASS { get; set; }

        /// <summary>空車期間FROM</summary>
        [DataType(DataType.Date)]
        [Display(Name = "空車期間FROM")]
        public DateTime? EMPTY_DATE_FROM { get; set; }

        /// <summary>空車期間TO</summary>
        [DataType(DataType.Date)]
        [Display(Name = "空車期間TO")]
        public DateTime? EMPTY_DATE_TO { get; set; }

        /// <summary>予約方法</summary>
        [Range(0, 1)]
        [Display(Name = "予約方法")]
        public short? RESERVATION { get; set; }

        /// <summary>車系</summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string 車系 { get; set; }

        /// <summary>駐車場番号</summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>所在地</summary>
        [StringLength(20)]
        [Display(Name = "所在地")]
        public string 所在地 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        [Range(0, 1)]
        [Display(Name = "FLAG_ETC付")]
        public int? FLAG_ETC付 { get; set; }

        /// <summary>トランスミッション</summary>
        [StringLength(20)]
        [Display(Name = "トランスミッション")]
        public string トランスミッション { get; set; }

        #endregion
    }
    #endregion

    #region カーシェア予約済一覧項目クラス
    /// <summary>
    /// カーシェア予約済一覧項目クラス
    /// </summary>
    [Serializable]
    public class CarModel
    {
        #region プロパティ
        /// <summary> 今どこ </summary>
        [StringLength(10)]
        [Display(Name = "今どこ")]
        public string 今どこ { get; set; }

        /// <summary>車系</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "車系")]
        public string 車系 { get; set; }

        /// <summary>開発符号</summary>
        [Required]
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string 開発符号 { get; set; }

        /// <summary>メーカー名</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "メーカー名")]
        public string メーカー名 { get; set; }

        /// <summary>外製車名</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "外製車名")]
        public string 外製車名 { get; set; }

        /// <summary>SECTION_CODE</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "SECTION_CODE")]
        public string SECTION_CODE { get; set; }

        /// <summary>SECTION_GROUP_CODE</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "SECTION_GROUP_CODE")]
        public string SECTION_GROUP_CODE { get; set; }

        /// <summary>FLAG_空時間貸出可</summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "FLAG_空時間貸出可")]
        public short? FLAG_空時間貸出可 { get; set; }

        /// <summary>登録ナンバー</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "登録ナンバー")]
        public string 登録ナンバー { get; set; }

        /// <summary>駐車場番号</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>所在地</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "所在地")]
        public string 所在地 { get; set; }

        /// <summary>FLAG_要予約許可</summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "FLAG_要予約許可")]
        public short? FLAG_要予約許可 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "FLAG_ETC付")]
        public short? FLAG_ETC付 { get; set; }

        /// <summary>FLAG_ナビ付</summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "FLAG_ナビ付")]
        public short? FLAG_ナビ付 { get; set; }

        /// <summary>仕向地</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }

        /// <summary>排気量</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "排気量")]
        public string 排気量 { get; set; }

        /// <summary>E_G型式</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "E_G型式")]
        public string E_G型式 { get; set; }

        /// <summary>駆動方式</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "駆動方式")]
        public string 駆動方式 { get; set; }

        /// <summary>トランスミッション</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "トランスミッション")]
        public string トランスミッション { get; set; }

        /// <summary>車型</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }

        /// <summary>グレード</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "グレード")]
        public string グレード { get; set; }

        /// <summary>型式符号</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "型式符号")]
        public string 型式符号 { get; set; }

        /// <summary>車体色</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "車体色")]
        public string 車体色 { get; set; }

        /// <summary>試作時期</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "試作時期")]
        public string 試作時期 { get; set; }

        /// <summary>リース満了日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "リース満了日")]
        public DateTime? リース満了日 { get; set; }

        /// <summary>処分予定年月</summary>
        [DataType(DataType.Date)]
        [Display(Name = "処分予定年月")]
        public DateTime? 処分予定年月 { get; set; }

        /// <summary>管理票番号</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "管理票番号")]
        public string 管理票番号 { get; set; }

        /// <summary>車体番号</summary>
        [Required]
        [StringLength(30)]
        [Display(Name = "車体番号")]
        public string 車体番号 { get; set; }

        /// <summary>E_G番号</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "E_G番号")]
        public string E_G番号 { get; set; }

        /// <summary>固定資産NO</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "固定資産NO")]
        public string 固定資産NO { get; set; }

        /// <summary>リースNO</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "リースNO")]
        public string リースNO { get; set; }

        /// <summary>研命ナンバー</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "研命ナンバー")]
        public string 研命ナンバー { get; set; }

        /// <summary>研命期間</summary>
        [DataType(DataType.Date)]
        [Display(Name = "研命期間")]
        public DateTime? 研命期間 { get; set; }

        /// <summary>車検登録日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "車検登録日")]
        public DateTime? 車検登録日 { get; set; }

        /// <summary>車検期限</summary>
        [DataType(DataType.Date)]
        [Display(Name = "車検期限")]
        public DateTime? 車検期限 { get; set; }

        /// <summary>車検期限まで残り</summary>
        [Display(Name = "車検期限まで残り")]
        public string 車検期限まで残り { get; set; }

        /// <summary>廃艦日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "廃艦日")]
        public DateTime? 廃艦日 { get; set; }

        /// <summary>号車</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "号車")]
        public string 号車 { get; set; }

        /// <summary>名称備考</summary>
        [Required]
        [StringLength(255)]
        [Display(Name = "名称備考")]
        public string 名称備考 { get; set; }

        /// <summary>試験目的</summary>
        [Required]
        [StringLength(255)]
        [Display(Name = "試験目的")]
        public string 試験目的 { get; set; }

        /// <summary>メモ</summary>
        [Required]
        [StringLength(1000)]
        [Display(Name = "メモ")]
        public string メモ { get; set; }

        /// <summary>FLAG_返却済</summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "FLAG_返却済")]
        public short? FLAG_返却済 { get; set; }

        /// <summary>FLAG_実使用</summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "FLAG_実使用")]
        public short? FLAG_実使用 { get; set; }

        /// <summary>ID</summary>
        [Range(0L, long.MaxValue)]
        [Display(Name = "ID")]
        public long? ID { get; set; }

        /// <summary>FLAG_CLASS</summary>
        [StringLength(20)]
        [Display(Name = "FLAG_CLASS")]
        public string FLAG_CLASS { get; set; }

        /// <summary>予約現況</summary>
        [StringLength(1000)]
        [Display(Name = "予約現況")]
        public string 予約現況 { get; set; }

        /// <summary>車検区分1</summary>
        [StringLength(50)]
        [Display(Name = "車検区分1")]
        public string 車検区分1 { get; set; }

        /// <summary>部コード</summary>
        [StringLength(50)]
        [Display(Name = "DEPARTMENT_CODE")]
        public string DEPARTMENT_CODE { get; set; }

        /// <summary>分類</summary>
        [StringLength(50)]
        [Display(Name = "分類")]
        public string 分類 { get; set; }

        #endregion
    }
    #endregion

    #region 予約現況クラス
    /// <summary>
    /// 予約現況クラス
    /// </summary>
    [Serializable]
    public class YoyakuGenkyouModel
    {
        #region プロパティ
        /// <summary>開始日時</summary>
        [DataType(DataType.Date)]
        [Display(Name = "START_DATE")]
        public DateTime? START_DATE { get; set; }

        /// <summary>終了日時</summary>
        [DataType(DataType.Date)]
        [Display(Name = "END_DATE")]
        public DateTime? END_DATE { get; set; }

        /// <summary>予約者</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "NAME")]
        public string NAME { get; set; }

        /// <summary>TEL</summary>
        [StringLength(100)]
        [Display(Name = "TEL")]
        public string TEL { get; set; }

        /// <summary>行先</summary>
        [StringLength(100)]
        [Display(Name = "行先")]
        public string 行先 { get; set; }

        /// <summary>SECTION_CODE</summary>
        [StringLength(20)]
        [Display(Name = "SECTION_CODE")]
        public string SECTION_CODE { get; set; }
        #endregion
    }
    #endregion
}