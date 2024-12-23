using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 状況記号（設計チェック）検索入力モデルクラス
    /// </summary>
    public class DesignCheckProgressSymbolGetInModel
    {
        /// <summary>
        /// 状況記号ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int? ID { get; set; }
    }
    /// <summary>
    /// 状況記号（設計チェック）検索出力モデルクラス
    /// </summary>
    public class DesignCheckProgressSymbolGetOutModel
    {
        /// <summary>
        /// 状況記号ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 記号
        /// </summary>
        public string 記号 { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string 説明 { get; set; }
    }
}