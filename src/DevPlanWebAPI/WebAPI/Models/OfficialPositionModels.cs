using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 役職マスタ検索入力モデルクラス
    /// </summary>
    public class OfficialPositionGetInModel
    {
        /// <summary>
        /// 役職
        /// </summary>
        [StringLength(10)]
        [Display(Name = "OFFICIAL_POSITION")]
        public string OFFICIAL_POSITION { get; set; }
    }
    /// <summary>
    /// 役職マスタ検索出力モデルクラス
    /// </summary>
    public class OfficialPositionModel
    {
        /// <summary>
        /// 役職
        /// </summary>
        public string OFFICIAL_POSITION { get; set; }
    }
}