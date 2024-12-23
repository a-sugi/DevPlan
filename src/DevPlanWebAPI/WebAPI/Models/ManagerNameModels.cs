using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 部長名検索モデルクラス
    /// </summary>
    public class ManagerNameSearchModel
    {
        /// <summary>
        /// 部ID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string DEPARTMENT_ID { get; set; }
    }

    /// <summary>
    /// 部長名検索結果モデルクラス
    /// </summary>
    public class ManagerNameModel
    {
        /// <summary>
        /// 部長名
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "部長名")]
        public string MANAGER_NAME { get; set; }
    }
}