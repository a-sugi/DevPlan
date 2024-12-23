using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{

    #region 目標進度リストマスタ検索条件クラス
    /// <summary>
    /// 目標進度リストマスタ検索条件クラス
    /// </summary>
    [Serializable]
    public class TargetProgressListMasterSearchModel
    {
        /// <summary>性能名_ID</summary>
        [Required]
        [Display(Name = "性能名_ID")]
        [Range(0, 99999)]
        public int? 性能名_ID { get; set; }

    }
    #endregion

    #region 目標進度リストマスタクラス
    /// <summary>
    /// 目標進度リストマスタクラス
    /// </summary>
    [Serializable]
    public class TargetProgressListMasterModel
    {
        /// <summary>ID</summary>
        [Required]
        [Display(Name = "ID")]
        [Range(0, 9999999999)]
        public long ID { get; set; }

        /// <summary>大項目</summary>
        [StringLength(100)]
        [Display(Name = "大項目")]
        public string 大項目 { get; set; }

        /// <summary>中項目</summary>
        [StringLength(100)]
        [Display(Name = "中項目")]
        public string 中項目 { get; set; }

        /// <summary>小項目</summary>
        [StringLength(100)]
        [Display(Name = "小項目")]
        public string 小項目 { get; set; }

        /// <summary>目標値</summary>
        [StringLength(200)]
        [Display(Name = "目標値")]
        public string 目標値 { get; set; }

        /// <summary>性能名_ID</summary>
        [Required]
        [Display(Name = "性能名_ID")]
        [Range(0, 99999)]
        public int? 性能名_ID { get; set; }

        /// <summary>SORT_NO</summary>
        [Display(Name = "SORT_NO")]
        [Range(0D, 99999999999999999999D)]
        public decimal? SORT_NO { get; set; }

        /// <summary>TS_NO</summary>
        [StringLength(200)]
        [Display(Name = "TS_NO")]
        public string TS_NO { get; set; }

        /// <summary>編集日時</summary>
        [DataType(DataType.Date)]
        [Display(Name = "編集日時")]
        public DateTime? 編集日時 { get; set; }

        /// <summary>編集者_ID</summary>
        [StringLength(10)]
        [Display(Name = "編集者_ID")]
        public string 編集者_ID { get; set; }

        /// <summary>編集者_NAME</summary>
        public string 編集者_NAME { get; set; }

        /// <summary>関連課_CODE</summary>
        [StringLength(100)]
        [Display(Name = "関連課_CODE")]
        public string 関連課_CODE { get; set; }

        /// <summary>削除フラグ</summary>
        public bool DELETE_FLG { get; set; }

    }
    #endregion

}