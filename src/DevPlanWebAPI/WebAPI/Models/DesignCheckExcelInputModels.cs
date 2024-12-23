using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    //Append Start 2021/06/24 張晋華 開発計画表設計チェック機能改修

    /// <summary>
    /// 設計チェック指摘インポートモデルクラス
    /// </summary>
    public class DesignCheckExcelInputModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int? ID { get; set; }

        /// <summary>
        /// 開催日ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "開催日_ID")]
        public int? 開催日_ID { get; set; }

        /// <summary>
        /// 登録日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "登録日")]
        public DateTime? 登録日 { get; set; }

        /// <summary>
        /// 登録者_ID
        /// </summary>
        [StringLength(40)]
        [Display(Name = "登録者_ID")]
        public string 登録者_ID { get; set; }
    }
    //Append End 2021/06/24 張晋華 開発計画表設計チェック機能改修
}