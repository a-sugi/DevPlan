using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 目標進度リスト名検索クラス(INPUT)
    /// <summary>
    /// 目標進度リスト名検索クラス(INPUT)
    /// </summary>
    [Serializable]
    public class TargetProgressListNameSearchInModel
    {
        /// <summary>部ID</summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string DEPARTMENT_ID { get; set; }

        /// <summary>パーソナルID</summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(20)]
        [Display(Name = "パーソナルID")]
        public string PERSONEL_ID { get; set; }

        /// <summary>処理種別(1:性能別 2:部別 3:課別 省略時は1)</summary>
        [DataType(DataType.Text)]
        [StringLength(1)]
        [Range(typeof(string), "1", "3")]
        [Display(Name = "処理種別")]
        public string PROCESS_CATEGORY { get; set; } = "1";

        /// <summary>部署選択(1:自部署 2:全部署 省略時は1)</summary>
        [DataType(DataType.Text)]
        [StringLength(1)]
        [Range(typeof(string), "1", "2")]
        [Display(Name = "部署選択")]
        public string DIVISION_CATEGORY { get; set; } = "1";

        /// <summary>目標進度リスト名ID</summary>
        [Range(0, 99999)]
        [Display(Name = "目標進度リスト名ID")]
        public int? CHECKLIST_NAME_ID { get; set; }

    }
    #endregion

    #region 目標進度リスト名検索クラス(OUTPUT)
    /// <summary>
    /// 目標進度リスト名検索クラス(OUTPUT)
    /// </summary>
    public class TargetProgressListNameSearchOutModel
    {
        /// <summary>チェックリスト名ID</summary>
        public int CHECKLIST_NAME_ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>部ID</summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>課ID</summary>
        public string SECTION_ID { get; set; }

        /// <summary>チェックリスト名</summary>
        public string CHECKLIST_NAME { get; set; }
    }
    #endregion

    #region 目標進度リスト名更新クラス(INPUT)
    /// <summary>
    /// 目標進度リスト名更新クラス(INPUT)
    /// </summary>
    [Serializable]
    public class TargetProgressListNameUpdateInModel
    {
        #region プロパティ
        /// <summary>目標進度リスト名ID</summary>
        [Range(0, 99999)]
        [Display(Name = "目標進度リスト名ID")]
        public int? CHECKLIST_NAME_ID { get; set; }

        /// <summary>GENERAL_CODE</summary>
        [DataType(DataType.Text)]
        [StringLength(15)]
        [Display(Name = "GENERAL_CODE")]
        public string GENERAL_CODE { get; set; }

        /// <summary>性能名_ID</summary>
        [Range(0, int.MaxValue)]
        [Display(Name = "性能名_ID")]
        public int? 性能名_ID { get; set; }

        /// <summary>新目標進度リスト名(仕様)</summary>
        [DataType(DataType.Text)]
        [StringLength(40)]
        [Display(Name = "新目標進度リスト名")]
        public string NEW_LISTNAME { get; set; }
        #endregion
    }
    #endregion
}