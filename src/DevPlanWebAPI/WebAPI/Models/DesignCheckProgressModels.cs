using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 設計チェック状況検索入力モデルクラス
    /// </summary>
    public class DesignCheckProgressGetInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "開催日_ID")]
        public int? 開催日_ID { get; set; }
        /// <summary>
        /// 指摘ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "指摘_ID")]
        public int? 指摘_ID { get; set; }
    }
    /// <summary>
    /// 設計チェック状況検索出力モデルクラス
    /// </summary>
    public class DesignCheckProgressGetOutModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int 開催日_ID { get; set; }
        /// <summary>
        /// 指摘ID
        /// </summary>
        public int 指摘_ID { get; set; }
        /// <summary>
        /// 状況
        /// </summary>
        public string 状況 { get; set; }
        /// <summary>
        /// 対象車両ID
        /// </summary>
        public int 対象車両_ID { get; set; }
        /// <summary>
        /// 試験車ID
        /// </summary>
        public int 試験車_ID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 試験車名
        /// </summary>
        public string 試験車名 { get; set; }
        /// <summary>
        /// グレード
        /// </summary>
        public string グレード { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        public string 試験目的 { get; set; }
    }
    /// <summary>
    /// 設計チェック状況登録入力モデルクラス
    /// </summary>
    public class DesignCheckProgressPostInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "開催日_ID")]
        public int 開催日_ID { get; set; }
        /// <summary>
        /// 対象車両ID
        /// </summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "対象車両_ID")]
        public int 対象車両_ID { get; set; }
        /// <summary>
        /// 指摘ID
        /// </summary>
        [Required]
        [Range(-99999, 99999)]
        [Display(Name = "指摘_ID")]
        public int 指摘_ID { get; set; }
        /// <summary>
        /// 状況
        /// </summary>
        [StringLength(10)]
        [Display(Name = "状況")]
        public string 状況 { get; set; }
        /// <summary>
        /// 完了確認日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "完了確認日")]
        public DateTime? 完了確認日 { get; set; }
        /// <summary>
        /// 部品納入日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "部品納入日")]
        public DateTime? 部品納入日 { get; set; }
        /// <summary>
        /// 編集者_ID（設計チェック指摘と状況の同時登録用）
        /// </summary>
        [StringLength(10)]
        [Display(Name = "編集者_ID")]
        public string 編集者_ID { get; set; }
    }
    /// <summary>
    /// 設計チェック状況削除入力モデルクラス
    /// </summary>
    public class DesignCheckProgressDeleteInModel
    {
        /// <summary>
        /// 対象車両ID
        /// </summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "対象車両_ID")]
        public int 対象車両_ID { get; set; }
        /// <summary>
        /// 指摘ID
        /// </summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "指摘_ID")]
        public int 指摘_ID { get; set; }
    }
}