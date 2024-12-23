using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    #region ユーザー表示設定情報検索条件クラス
    /// <summary>
    /// ユーザー表示設定情報クラス
    /// </summary>
    public class ScheduleItemDisplayConfigurationSearchModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        [Required]
        [Display(Name = "開発符号")]
        public string 開発符号 { get; set; }
        #endregion

    }
    #endregion

    #region ユーザー表示設定情報クラス
    /// <summary>
    /// ユーザー表示設定情報クラス
    /// </summary>
    public class ScheduleItemDisplayConfigurationModel
    {
        #region プロパティ
        /// <summary>ユーザーID</summary>
        [Required]
        [Display(Name = "開発符号")]
        public string 開発符号 { get; set; }

        /// <summary>表示列名</summary>
        [StringLength(1000)]
        [Display(Name = "表示列名")]
        public string 表示列名 { get; set; }

        /// <summary>非表示列名</summary>
        [StringLength(1000)]
        [Display(Name = "非表示列名")]
        public string 非表示列名 { get; set; }

        /// <summary>固定列数</summary>
        [Range(0, 999)]
        [Display(Name = "固定列数")]
        public short? 固定列数 { get; set; }
        #endregion

    }
    #endregion
}