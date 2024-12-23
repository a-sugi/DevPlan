using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// ロール権限検索入力モデルクラス
    /// </summary>
    public class RollAuthorityGetInModel
    {
        /// <summary>
        /// 部ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課ID")]
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当ID")]
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 役職
        /// </summary>
        [StringLength(10)]
        [Display(Name = "役職")]
        public string OFFICIAL_POSITION { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// ロールID
        /// </summary>
        [Range(0, 9999999999)]
        [Display(Name = "ロールID")]
        public long? ROLL_ID { get; set; }
        /// <summary>
        /// 種別(0:人, 1:部署・役職)
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "種別")]
        public int? TYPE { get; set; }
    }
    /// <summary>
    /// ロール権限検索出力モデルクラス
    /// </summary>
    public class RollAuthorityGetOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 部名
        /// </summary>
        public string DEPARTMENT_NAME { get; set; }
        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 課名
        /// </summary>
        public string SECTION_NAME { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 担当名
        /// </summary>
        public string SECTION_GROUP_NAME { get; set; }
        /// <summary>
        /// 担当コード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 役職
        /// </summary>
        public string OFFICIAL_POSITION { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// ユーザー名
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// ロールID
        /// </summary>
        public long ROLL_ID { get; set; }
    }
    /// <summary>
    /// ロール権限登録入力モデルクラス
    /// </summary>
    public class RollAuthorityPostInModel
    {
        /// <summary>
        /// 部ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課ID")]
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当ID")]
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 役職
        /// </summary>
        [StringLength(10)]
        [Display(Name = "役職")]
        public string OFFICIAL_POSITION { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// ロールID
        /// </summary>
        [Required]
        [Range(0, 9999999999)]
        [Display(Name = "ロールID")]
        public long ROLL_ID { get; set; }
        /// <summary>
        /// ユーザーID(登録者)
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string INPUT_PERSONEL_ID { get; set; }
        /// <summary>
        /// 種別(0:人, 1:部署・役職)
        /// </summary>
        [Required]
        [Range(0, 9)]
        [Display(Name = "種別")]
        public int TYPE { get; set; }
    }
    /// <summary>
    /// ロール権限付替入力モデルクラス
    /// </summary>
    public class RollAuthorityPutInModel : RollAuthorityPostInModel
    {
    }
    /// <summary>
    /// ロール権限削除入力モデルクラス
    /// </summary>
    public class RollAuthorityDeleteInModel
    {
        /// <summary>
        /// 部ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課ID")]
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当ID")]
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 役職
        /// </summary>
        [StringLength(10)]
        [Display(Name = "役職")]
        public string OFFICIAL_POSITION { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// ロールID
        /// </summary>
        [Range(0, 9999999999)]
        [Display(Name = "ロールID")]
        public long? ROLL_ID { get; set; }
        /// <summary>
        /// 種別(0:人, 1:部署・役職)
        /// </summary>
        [Required]
        [Range(0, 9)]
        [Display(Name = "種別")]
        public int TYPE { get; set; }
    }
}