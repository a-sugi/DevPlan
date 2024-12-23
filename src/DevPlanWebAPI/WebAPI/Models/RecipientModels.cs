using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 受領先情報検索入力モデルクラス
    /// </summary>
    public class RecipientGetInModel
    {
    }
    /// <summary>
    /// 受領先情報検索出力モデルクラス
    /// </summary>
    public class RecipientGetOutModel
    {
        /// <summary>
        /// 受領先
        /// </summary>
        public string 受領先 { get; set; }
    }
}