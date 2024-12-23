using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 設計チェック参加者検索入力モデルクラス
    /// </summary>
    public class DesignCheckUserGetInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "開催日_ID")]
        public int? 開催日_ID { get; set; }
        /// <summary>
        /// 参加者ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "PERSONEL_ID")]
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 設計チェック参加者検索出力モデルクラス
    /// </summary>
    public class DesignCheckUserGetOutModel
    {
        /// <summary>
        /// 参加ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int 開催日_ID { get; set; }
        /// <summary>
        /// 参加者ID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 参加者名
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 担当コード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
    }
    /// <summary>
    /// 設計チェック参加者登録入力モデルクラス
    /// </summary>
    public class DesignCheckUserPostInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "開催日_ID")]
        public int 開催日_ID { get; set; }

        /// <summary>
        /// 参加者ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "PERSONEL_ID")]
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 参加者名
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "NAME")]
        public string NAME { get; set; }

        /// <summary>
        /// 参加時_所属部_ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "DEPARTMENT_ID")]
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 参加時_所属課_ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "SECTION_ID")]
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 参加時_所属担当_ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "SECTION_GROUP_ID")]
        public string SECTION_GROUP_ID { get; set; }
    }
    /// <summary>
    /// 設計チェック参加者削除入力モデルクラス
    /// </summary>
    public class DesignCheckUserDeleteInModel
    {
        /// <summary>
        /// 参加ID
        /// </summary>
        [Required]
        [Range(0, 9999999999)]
        [Display(Name = "ID")]
        public long ID { get; set; }
    }
}