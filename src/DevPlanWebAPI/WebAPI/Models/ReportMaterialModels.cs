using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 情報元一覧検索モデルクラス
    /// </summary>
    [Serializable]
    public class InfoListInModel
    {
        /// <summary>
        /// 検索区分
        /// </summary>
        [Required]
        [Range(1, 6)]
        [Display(Name = "検索区分")]
        public int CLASS_DATA { get; set; }
        /// <summary>
        /// 検索開始日
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "検索開始日")]
        public DateTime? FIRST_DAY { get; set; }
        /// <summary>
        /// 検索終了日
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "検索終了日")]
        public DateTime? LAST_DAY { get; set; }
        /// <summary>
        /// 作成単位
        /// </summary>
        [StringLength(10)]
        [Display(Name = "作成単位")]
        public string 作成単位 { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "課ID")]
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "担当ID")]
        public string SECTION_GROUP_ID { get; set; }
    }

    /// <summary>
    /// 情報元一覧検索結果モデルクラス
    /// </summary>
    public class InfoListOutModel
    {
        /// <summary>
        /// 日付
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "検索終了日")]
        public DateTime? LISTED_DATE { get; set; }
        /// <summary>
        /// 課
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課")]
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 担当
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当")]
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(15)]
        [Display(Name = "担当")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 項目
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "項目")]
        public string CATEGORY { get; set; }
        /// <summary>
        /// 現状
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "現状")]
        public string CURRENT_SITUATION { get; set; }
        /// <summary>
        /// 今後の予定
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "今後の予定")]
        public string FUTURE_SCHEDULE { get; set; }

        /// <summary>
        /// 選択キーワード
        /// </summary>
        [StringLength(200)]
        [Display(Name = "選択キーワード")]
        public string SELECT_KEYWORD { get; set; }

        /// <summary>
        /// OPEN/CLOSE
        /// </summary>
        [StringLength(10)]
        [Display(Name = "OPEN/CLOSE")]
        public string OPEN_CLOSE { get; set; }

        /// <summary>
        /// INPUT_PERSONEL_IDのNAME
        /// </summary>
        [StringLength(20)]
        [Display(Name = "編集者")]
        public string PERSONEL_NAME { get; set; }

        /// <summary>
        /// INPUT_DATETIME
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "編集日時")]
        public string INPUT_DATETIME { get; set; }
    }
}