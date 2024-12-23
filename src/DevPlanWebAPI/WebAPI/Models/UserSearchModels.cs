using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// ユーザー検索入力モデルクラス
    /// </summary>
    public class UserSearchInModel
    {
        /// <summary>
        /// 社員コード
        /// </summary>
        public string[] PERSONEL_ID { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string[] DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string[] SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        public string[] SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 請負関係課ID
        /// </summary>
        public string[] SECTION_RELATIONAL_ID { get; set; }
        /// <summary>
        /// ユーザー名
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ユーザー名")]
        public string PERSONEL_NAME { get; set; }
        /// <summary>
        /// 部名
        /// </summary>
        [StringLength(40)]
        [Display(Name = "部名")]
        public string DEPARTMENT_NAME { get; set; }
        /// <summary>
        /// 課名
        /// </summary>
        [StringLength(40)]
        [Display(Name = "課名")]
        public string SECTION_NAME { get; set; }
        /// <summary>
        /// 担当名
        /// </summary>
        [StringLength(40)]
        [Display(Name = "担当名")]
        public string SECTION_GROUP_NAME { get; set; }
        /// <summary>
        /// ユーザーステータスコード
        /// </summary>
        [StringLength(1)]
        [Display(Name = "STATUS_CODE")]
        public string STATUS_CODE { get; set; }
        /// <summary>
        /// 管理部署別利用フラグ(true：利用する、false：利用しない)
        /// </summary>
        [Display(Name = "MANAGE_FLG")]
        public bool MANAGE_FLG { get; set; } = false;
        /// <summary>
        /// 大小文字区別フラグ(true：区別する、false：区別しない)
        /// </summary>
        [Display(Name = "DISTINCT_FLG")]
        public bool DISTINCT_FLG { get; set; } = true;
    }
    /// <summary>
    /// ユーザー検索出力モデルクラス
    /// </summary>
    public class UserSearchOutModel
    {
        /// <summary>
        /// 社員コード
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 氏名
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 課グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// アクセス権限
        /// </summary>
        public string ACCESS_LEVEL { get; set; }
        /// <summary>
        /// 職位
        /// </summary>
        public string OFFICIAL_POSITION { get; set; }
        /// <summary>
        /// 課グループコード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 課グループ名
        /// </summary>
        public string SECTION_GROUP_NAME { get; set; }
        /// <summary>
        /// 請負関係課ID
        /// </summary>
        public string SECTION_RELATIONAL_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 課名
        /// </summary>
        public string SECTION_NAME { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 部名
        /// </summary>
        public string DEPARTMENT_NAME { get; set; }
        /// <summary>
        /// 研実フラグ
        /// </summary>
        public int FLAG_KENJITSU { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public string ESTABLISHMENT { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string MAIL_ADDRESS { get; set; }

        /// <summary>
        /// 管理部署種別
        /// </summary>
        public string[] 管理部署種別 { get; set; }

        /// <summary>
        /// ユーザーステータスコード
        /// </summary>        
        public string STATUS_CODE { get; set; }
    }

    /// <summary>
    /// 管理部署モデル
    /// </summary>
    public class ManagementDepartmentModel
    {
        /// <summary>
        /// 担当ID
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "担当ID")]
        public string 担当ID { get; set; }

        /// <summary>
        /// 管理部署種別
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "管理部署種別")]
        public string 管理部署種別 { get; set; }

    }

}