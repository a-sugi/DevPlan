using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// PC端末権限検索入力モデルクラス
    /// </summary>
    public class PCAuthorityGetInModel
    {
        /// <summary>
        /// NetBIOS名
        /// </summary>
        [StringLength(20)]
        [Display(Name = "PC_NAME")]
        public string PC_NAME { get; set; }
        /// <summary>
        /// 機能ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int? FUNCTION_ID { get; set; }
    }
    /// <summary>
    /// PC端末権限検索出力モデルクラス
    /// </summary>
    public class PCAuthorityGetOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// NetBIOS名
        /// </summary>
        public string PC_NAME { get; set; }
        /// <summary>
        /// 機能ID
        /// </summary>
        public int FUNCTION_ID { get; set; }
        /// <summary>
        /// 機能名
        /// </summary>
        public string FUNCTION_NAME { get; set; }
    }
}