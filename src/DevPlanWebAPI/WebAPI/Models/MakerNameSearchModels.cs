using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// メーカー名検索入力モデルクラス
    /// </summary>
    public class MakerNameSearchInModel
    {
        /// <summary>
        /// 事業ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "事業ID")]
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 職場ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "職場ID")]
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 自社フラグ
        /// </summary>
        [Range(0, 1)]
        [Display(Name = "自社フラグ")]
        public int 自社フラグ { get; set; }
        /// <summary>
        /// ナンバー登録フラグ
        /// </summary>
        [Range(0, 1)]
        [Display(Name = "ナンバー登録フラグ")]
        public int ナンバー登録フラグ { get; set; }
        /// <summary>
        /// 占有者フラグ
        /// </summary>
        [Range(0, 1)]
        [Display(Name = "占有者フラグ")]
        public int 占有者フラグ { get; set; }
        /// <summary>
        /// 最新履歴フラグ
        /// </summary>
        [Range(0, 1)]
        [Display(Name = "最新履歴フラグ")]
        public int 最新履歴フラグ { get; set; }
    }
    /// <summary>
    /// メーカー名検索出力モデルクラス
    /// </summary>
    public class MakerNameSearchOutModel
    {
        /// <summary>
        /// メーカー名(必須)
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "メーカー名")]
        public string メーカー名 { get; set; }
    }
}