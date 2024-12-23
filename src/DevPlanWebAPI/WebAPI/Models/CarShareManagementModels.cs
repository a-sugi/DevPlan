using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region カーシェア管理一覧項目検索条件クラス
    /// <summary>
    /// カーシェア管理一覧項目検索条件クラス
    /// </summary>
    [Serializable]
    public class CarShareManagementItemSearchModel
    {
        #region プロパティ
        /// <summary>所在地</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "所在地")]
        public string 所在地 { get; set; }

        /// <summary>検索日付（貸出リスト）</summary>
        [DataType(DataType.Date)]
        [Display(Name = "検索日付（貸出リスト）")]
        public DateTime? START_DATE { get; set; }

        /// <summary>検索日付（返却リスト）</summary>
        [DataType(DataType.Date)]
        [Display(Name = "検索日付（返却リスト）")]
        public DateTime? END_DATE { get; set; }

        /// <summary>準備状況</summary>
        [Range(0, 1)]
        [Display(Name = "準備状況")]
        public short? FLAG_準備済 { get; set; }

        /// <summary>貸出状況</summary>
        [Range(0, 1)]
        [Display(Name = "貸出状況")]
        public short? FLAG_実使用 { get; set; }

        /// <summary>返却状況</summary>
        [Range(0, 1)]
        [Display(Name = "返却状況")]
        public short? FLAG_返却済 { get; set; }

        /// <summary>給油状況</summary>
        [Range(0, 1)]
        [Display(Name = "給油状況")]
        public short? FLAG_給油済 { get; set; }

        /// <summary>予約者</summary>
        [StringLength(20)]
        [Display(Name = "予約者")]
        public string NAME { get; set; }

        /// <summary>駐車場番号</summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }
        #endregion
    }
    #endregion

    #region 車種別進捗状況一覧項目クラス
    /// <summary>
    /// 車種別進捗状況一覧項目クラス
    /// </summary>
    [Serializable]
    public class CarShareManagementItemModel
    {
        #region プロパティ
        /// <summary>スケジュールID</summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "スケジュールID")]
        public long SCHEDULE_ID { get; set; }

        /// <summary>駐車場番号</summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>
        /// 車系
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string 車系 { get; set; }

        /// <summary>貸出日時</summary>
        [DataType(DataType.Date)]
        [Display(Name = "貸出日時")]
        public DateTime? START_DATE { get; set; }

        /// <summary>返却予定日時</summary>
        [DataType(DataType.Date)]
        [Display(Name = "返却予定日時")]
        public DateTime? END_DATE { get; set; }

        /// <summary>予約種別</summary>
        [StringLength(Const.ScheduleReservationTypeLength)]
        [Display(Name = "予約種別")]
        public string 予約種別 { get; set; }

        /// <summary>開発符号</summary>
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>登録ナンバー</summary>
        [StringLength(50)]
        [Display(Name = "登録ナンバー")]
        public string 登録ナンバー { get; set; }

        /// <summary> 説明 </summary>
        [StringLength(Const.ScheduleCommentLength)]
        [Display(Name = "DESCRIPTION")]
        public string DESCRIPTION { get; set; }

        /// <summary> 予約者所属課 </summary>
        [StringLength(20)]
        [Display(Name = "予約者所属課")]
        public string SECTION_CODE { get; set; }

        /// <summary>予約者</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "NAME")]
        public string NAME { get; set; }

        //Delete Start 2021/10/12 杉浦 カーシェア一覧追加要望
        ////Append Start 2021/06/14 杉浦
        ///// <summary> 前回予約者所属課 </summary>
        //[StringLength(20)]
        //[Display(Name = "前回予約者所属課")]
        //public string PREV_SECTION_CODE { get; set; }

        ///// <summary>予約者</summary>
        //[StringLength(Const.PersonelIdLength)]
        //[Display(Name = "PREV_NAME")]
        //public string PREV_NAME { get; set; }
        ////Append End 2021/06/14 杉浦
        //Delete Start 2021/10/12 杉浦 カーシェア一覧追加要望

        /// <summary>準備状況</summary>
        [Range(0, 1)]
        [Display(Name = "準備済")]
        public short? FLAG_準備済 { get; set; }

        /// <summary>貸出状況</summary>
        [Range(0, 1)]
        [Display(Name = "貸出済")]
        public short? FLAG_実使用 { get; set; }

        /// <summary>返却状況</summary>
        [Range(0, 1)]
        [Display(Name = "返却済")]
        public short? FLAG_返却済 { get; set; }

        /// <summary>給油状況</summary>
        [Range(0, 1)]
        [Display(Name = "給油済")]
        public short? FLAG_給油済 { get; set; }

        /// <summary>貸出備考</summary>
        [StringLength(500)]
        [Display(Name = "貸出備考")]
        public string 貸出備考 { get; set; }

        /// <summary>返却備考</summary>
        [StringLength(500)]
        [Display(Name = "返却備考")]
        public string 返却備考 { get; set; }

        /// <summary>目的</summary>
        [StringLength(100)]
        [Display(Name = "目的")]
        public string 目的 { get; set; }

        /// <summary>行先</summary>
        [StringLength(100)]
        [Display(Name = "行先")]
        public string 行先 { get; set; }

        /// <summary>使用者Tel</summary>
        [StringLength(100)]
        [Display(Name = "使用者Tel")]
        public string TEL { get; set; }

        //Update Start 2021/10/12 杉浦 カーシェア一覧追加要望
        ////Append Start 2021/06/14 杉浦
        ///// <summary>前回使用者Tel</summary>
        //[StringLength(100)]
        //[Display(Name = "前回使用者Tel")]
        //public string PREV_TEL { get; set; }
        ////Append End 2021/06/14 杉浦
        /// <summary>前回の予約者</summary>
        [StringLength(100)]
        [Display(Name = "前回の予約者")]
        public string PREV_RESERVE { get; set; }
        //Update End 2021/10/12 杉浦 カーシェア一覧追加要望

        //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
        [StringLength(200)]
        [Display(Name = "入れ替え中車両")]
        public string 入れ替え中車両 { get; set; }
        //Append End 2022/01/17 杉浦 入れ替え中車両の処理

        /// <summary>FLAG_ETC付</summary>
        [Range(0, 1)]
        [Display(Name = "FLAG_ETC付")]
        public short? FLAG_ETC付 { get; set; }

        /// <summary>FLAG_ナビ付</summary>
        [Range(0, 1)]
        [Display(Name = "FLAG_ナビ付")]
        public short? FLAG_ナビ付 { get; set; }

        /// <summary>仕向地</summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }

        /// <summary>排気量</summary>
        [StringLength(50)]
        [Display(Name = "排気量")]
        public string 排気量 { get; set; }

        /// <summary>トランスミッション</summary>
        [StringLength(20)]
        [Display(Name = "トランスミッション")]
        public string トランスミッション { get; set; }

        /// <summary>駆動方式</summary>
        [StringLength(50)]
        [Display(Name = "駆動方式")]
        public string 駆動方式 { get; set; }

        /// <summary>車型</summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }

        /// <summary>グレード</summary>
        [StringLength(50)]
        [Display(Name = "グレード")]
        public string グレード { get; set; }

        /// <summary>車体色</summary>
        [StringLength(50)]
        [Display(Name = "車体色")]
        public string 車体色 { get; set; }

        /// <summary>管理票No.</summary>
        [StringLength(10)]
        [Display(Name = "管理票No.")]
        public string 管理票番号 { get; set; }

        /// <summary>CATEGORY_ID</summary>
        [Display(Name = "CATEGORY_ID")]
        public long CATEGORY_ID { get; set; }
        #endregion
    }
    #endregion

    /// <summary>
    /// 稼働率算出データ保持クラス。
    /// </summary>
    [Serializable]
    public class CarShareManagementRatePrintDataModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 所在地
        /// </summary>
        public string 所在地 { get; set; }

        /// <summary>
        /// 正式取得日
        /// </summary>
        public DateTime 正式取得日 { get; set; }

        /// <summary>
        /// CLOSED_DATE
        /// </summary>
        public DateTime? CLOSED_DATE { get; set; }

        /// <summary>
        /// GENERAL_CODE
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// CATEGORY
        /// </summary>
        public string CATEGORY { get; set; }

        /// <summary>
        /// START_DATE
        /// </summary>
        public DateTime? START_DATE { get; set; }

        /// <summary>
        /// END_DATE
        /// </summary>
        public DateTime? END_DATE { get; set; }

        /// <summary>
        /// 実使用
        /// </summary>
        public short? FLAG_実使用 { get; set; }
    }

    /// <summary>
    /// 稼働率算出データ保持クラス（集計後）
    /// </summary>
    [Serializable]
    public class CarShareManagementRatePrintOutModel
    {
        /// <summary>
        /// 稼働率年月（日）
        /// </summary>
        public DateTime RateDate { get; set; }

        /// <summary>
        /// 稼働台数（全体）
        /// </summary>
        public int KadouCount { get; set; }

        /// <summary>
        /// 保有台数（全体）
        /// </summary>
        public int HoyuuCount { get; set; }

        /// <summary>
        /// 稼働率（全体）
        /// </summary>
        public double KadouRate { get; set; }

        /// <summary>
        /// 稼働台数（群馬）
        /// </summary>
        public int KadouGunmaCount { get; set; }

        /// <summary>
        /// 保有台数（群馬）
        /// </summary>
        public int HoyuuGunmaCount { get; set; }

        /// <summary>
        /// 稼働率（群馬）
        /// </summary>
        public double KadouGunmaRate { get; set; }

        /// <summary>
        /// 稼働台数（SKC）
        /// </summary>
        public int KadouSKCCount { get; set; }

        /// <summary>
        /// 保有台数（SKC）
        /// </summary>
        public int HoyuuSKCCount { get; set; }

        /// <summary>
        /// 稼働率（SKC）
        /// </summary>
        public double KadouSKCRate { get; set; }
    }

    /// <summary>
    /// 稼働率算出検索条件保持クラス。
    /// </summary>
    [Serializable]
    public class CarShareManagementRatePrintSearchModel
    {
        /// <summary>検索対象開始日</summary>
        public DateTime SearchStartDate { get; set; }

        /// <summary>検索対象終了日</summary>
        public DateTime SearchEndDate { get; set; }
    }
}