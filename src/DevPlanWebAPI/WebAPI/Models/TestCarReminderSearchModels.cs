using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 試験車注意喚起検索入力モデルクラス
    /// </summary>
    public class TestCarReminderSearchInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        [Required]
        [StringLength(15)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }
    }
    /// <summary>
    /// 試験車注意喚起検索出力モデルクラス
    /// </summary>
    public class TestCarReminderSearchOutModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        [Required]
        [StringLength(15)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試験車注意喚起フラグ
        /// </summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "試験車注意喚起フラグ")]
        public int ALERT_FLAG { get; set; }
    }
}