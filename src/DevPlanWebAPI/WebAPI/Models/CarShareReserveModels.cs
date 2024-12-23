using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region カーシェア予約済一覧検索条件クラス
    /// <summary>
    /// カーシェア予約済一覧検索条件クラス
    /// </summary>
    [Serializable]
    public class CarShareReservationSearchModel
    {
        #region プロパティ
        /// <summary>予約者ID</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "予約者_ID")]
        public string 予約者_ID { get; set; }

        ///// <summary>貸出前</summary>
        //[Required]
        //[Range(0, 1)]
        //[Display(Name = "貸出前")]
        //public int? LEND { get; set; }

        ///// <summary>貸出中</summary>
        //[Required]
        //[Range(0, 1)]
        //[Display(Name = "貸出中")]
        //public int? FLAG_実使用 { get; set; }

        ///// <summary>返却済</summary>
        //[Required]
        //[Range(0, 1)]
        //[Display(Name = "返却済")]
        //public int? FLAG_返却済 { get; set; }
        
        /// <summary>予約済み</summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "予約済み")]
        public int? FLAG_RESERVE { get; set; }

        #endregion
    }
    #endregion

    #region カーシェア予約済一覧項目クラス
    /// <summary>
    /// カーシェア予約済一覧項目クラス
    /// </summary>
    [Serializable]
    public class CarShareReservationModel
    {
        #region プロパティ
        /// <summary>車系</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "XEYE_EXIST")]
        public string XEYE_EXIST { get; set; }

        /// <summary>車系</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "車系")]
        public string CAR_GROUP { get; set; }

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
        //Update Start 2022/01/11 杉浦 トラック予約一覧を追加
        //[Display(Name = "外製車名")]
        //public string 外製車名 { get; set; }
        [Display(Name = "車名")]
        public string 車名 { get; set; }
        //Update Start 2022/01/11 杉浦 トラック予約一覧を追加

        //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
        /// <summary>種別</summary>
        [Display(Name = "種別")]
        public string 種別 { get; set; }
        //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
        
        /// <summary>駐車場番号</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>貸出開始日時</summary>
        [DataType(DataType.Date)]
        [Display(Name = "貸出開始日時")]
        public DateTime? START_DATE { get; set; }

        /// <summary>貸出終了日時</summary>
        [DataType(DataType.Date)]
        [Display(Name = "貸出終了日時")]
        public DateTime? END_DATE { get; set; }

        /// <summary>予約者</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "予約者")]
        public string NAME { get; set; }

        //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
        /// <summary>種別</summary>
        [Display(Name = "依頼者")]
        public string 依頼者 { get; set; }

        /// <summary>種別</summary>
        [Display(Name = "発送者")]
        public string 発送者 { get; set; }

        /// <summary>種別</summary>
        [Display(Name = "受領者")]
        public string 受領者 { get; set; }

        /// <summary>種別</summary>
        [Display(Name = "運転者A")]
        public string 運転者A { get; set; }

        /// <summary>種別</summary>
        [Display(Name = "運転者B")]
        public string 運転者B { get; set; }
        //Append Start 2022/01/11 杉浦 トラック予約一覧を追加

        /// <summary>予約表示</summary>
        [Required]
        [StringLength(500)]
        [Display(Name = "予約表示")]
        public string DESCRIPTION { get; set; }

        /// <summary>管理票番号</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "管理票番号")]
        public string 管理票番号 { get; set; }

        /// <summary>FLAG_CLASS</summary>
        [StringLength(20)]
        [Display(Name = "FLAG_CLASS")]
        public string FLAG_CLASS { get; set; }

        /// <summary>ID</summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>行先</summary>
        [StringLength(100)]
        [Display(Name = "行先")]
        public string 行先 { get; set; }
        #endregion
    }
    #endregion

    //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
    #region トラック予約一覧検索条件モデルクラス
    /// <summary>
    /// トラック予約一覧検索条件モデルクラス
    /// </summary>
    [Serializable]
    public class TruckReserveInModel
    {
        #region プロパティ
        /// <summary>予約者ID</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "予約者_ID")]
        public string 予約者_ID { get; set; }

        /// <summary>依頼者_ID</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "依頼者_ID")]
        public string 依頼者_ID { get; set; }

        /// <summary>発送者_ID</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "発送者_ID")]
        public string 発送者_ID { get; set; }

        /// <summary>受領者_ID</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "受領者_ID")]
        public string 受領者_ID { get; set; }

        /// <summary>運転者A_ID</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "運転者A_ID")]
        public string 運転者A_ID { get; set; }

        /// <summary>運転者B_ID</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "運転者B_ID")]
        public string 運転者B_ID { get; set; }

        /// <summary>予約済み</summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "予約済み")]
        public int? FLAG_RESERVE { get; set; }

        #endregion
    }
    //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
    #endregion
}