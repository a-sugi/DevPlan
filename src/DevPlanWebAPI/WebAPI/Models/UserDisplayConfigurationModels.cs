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
    public class UserDisplayConfigurationSearchModel
    {
        #region プロパティ
        /// <summary>ユーザーID</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string ユーザーID { get; set; }

        /// <summary>画面名</summary>
        [Required]
        [StringLength(30)]
        [Display(Name = "画面名")]
        public string 画面名 { get; set; }
        #endregion

    }
    #endregion

    #region ユーザー表示設定情報クラス
    /// <summary>
    /// ユーザー表示設定情報クラス
    /// </summary>
    public class UserDisplayConfigurationModel
    {
        #region プロパティ
        /// <summary>ユーザーID</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string ユーザーID { get; set; }

        /// <summary>画面名</summary>
        [Required]
        [StringLength(30)]
        [Display(Name = "画面名")]
        public string 画面名 { get; set; }

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