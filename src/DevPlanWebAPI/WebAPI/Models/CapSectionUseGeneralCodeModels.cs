using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// CAP部署（開発符号）モデルクラス（入力）
    /// </summary>
    public class CapSectionUseGeneralCodeInModel
    {
        /// <summary>
        /// 社員コード（パーソナルID）
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
    }

    /// <summary>
    /// CAP部署（開発符号）モデルクラス（出力）
    /// </summary>
    public class CapSectionUseGeneralCodeOutModel
    {
        /// <summary>
        /// 専門部署名
        /// </summary>
        public string 専門部署名 { get; set; }

        /// <summary>
        /// 権限フラグ(0:閲覧権限なし 1:閲覧権限あり)
        /// </summary>
        public short PERMIT_FLG { get; set; }
    }
}