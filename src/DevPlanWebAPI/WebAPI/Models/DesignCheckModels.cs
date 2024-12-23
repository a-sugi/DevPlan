using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 設計チェック検索入力モデルクラス
    /// </summary>
    public class DesignCheckGetInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int? ID { get; set; }
        /// <summary>
        /// 開催日（開始）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "OPEN_START_DATE")]
        public DateTime? OPEN_START_DATE { get; set; }
        /// <summary>
        /// 開催日（終了）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "OPEN_END_DATE")]
        public DateTime? OPEN_END_DATE { get; set; }
        /// <summary>
        /// 設計チェック名称
        /// </summary>
        [StringLength(100)]
        [Display(Name = "名称")]
        public string 名称 { get; set; }
        /// <summary>
        /// オープンフラグ
        /// </summary>
        [Display(Name = "OPEN_FLG")]
        public bool OPEN_FLG { get; set; }
        /// <summary>
        /// クローズフラグ
        /// </summary>
        [Display(Name = "CLOSE_FLG")]
        public bool CLOSE_FLG { get; set; }
    }
    /// <summary>
    /// 設計チェック検索出力モデルクラス
    /// </summary>
    public class DesignCheckGetOutModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// 開催日
        /// </summary>
        public DateTime? 開催日 { get; set; }
        /// <summary>
        /// 開催回
        /// </summary>
        public int? 回 { get; set; }
        /// <summary>
        /// 設計チェック名称
        /// </summary>
        public string 名称 { get; set; }
        /// <summary>
        /// オープン件数
        /// </summary>
        public long? OPEN_COUNT { get; set; }
        /// <summary>
        /// クローズ件数
        /// </summary>
        public long? CLOSE_COUNT { get; set; }
    }
    /// <summary>
    /// 設計チェック登録入力モデルクラス
    /// </summary>
    public class DesignCheckPostInModel
    {
        /// <summary>
        /// 開催日
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "開催日")]
        public DateTime 開催日 { get; set; }
        /// <summary>
        /// 開催回
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "回")]
        public int? 回 { get; set; }
        /// <summary>
        /// 設計チェック名称
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "名称")]
        public string 名称 { get; set; }
    }
    /// <summary>
    /// 設計チェック更新入力モデルクラス
    /// </summary>
    public class DesignCheckPutInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int? ID { get; set; }
        /// <summary>
        /// 開催日
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "開催日")]
        public DateTime 開催日 { get; set; }
        /// <summary>
        /// 開催回
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "回")]
        public int? 回 { get; set; }
        /// <summary>
        /// 設計チェック名称
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "名称")]
        public string 名称 { get; set; }
    }
    /// <summary>
    /// 設計チェック削除入力モデルクラス
    /// </summary>
    public class DesignCheckDeleteInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int ID { get; set; }
    }
}