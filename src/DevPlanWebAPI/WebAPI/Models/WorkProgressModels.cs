using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 車種別進捗状況一覧項目検索条件クラス
    /// <summary>
    /// 車種別進捗状況一覧検索条件クラス
    /// </summary>
    [Serializable]
    public class WorkProgressItemSearchModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long? ID { get; set; }

        /// <summary>開発符号</summary>
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

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

        /// <summary>ステータス</summary>
        [StringLength(10)]
        [Display(Name = "ステータス")]
        public string OPEN_CLOSE { get; set; }
        #endregion
    }
    #endregion

    #region 車種別進捗状況一覧項目クラス
    /// <summary>
    /// 車種別進捗状況一覧項目クラス
    /// </summary>
    [Serializable]
    public class WorkProgressItemModel
    {
        #region プロパティ
        /// <summary>担当名</summary>
        [StringLength(20)]
        [Display(Name = "担当名")]
        public string SECTION_GROUP_CODE { get; set; }

        /// <summary>開発符号</summary>
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>項目名</summary>
        [StringLength(Const.ScheduleCategoryLength)]
        [Display(Name = "項目名")]
        public string CATEGORY { get; set; }

        /// <summary>ステータス</summary>
        [StringLength(10)]
        [Display(Name = "ステータス")]
        public string OPEN_CLOSE { get; set; }

        /// <summary>終了日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "終了日")]
        public DateTime? CLOSED_DATE { get; set; }

        /// <summary>日付</summary>
        [DataType(DataType.Date)]
        [Display(Name = "日付")]
        public DateTime? LISTED_DATE { get; set; }

        /// <summary>現状</summary>
        [StringLength(2000)]
        [Display(Name = "ステータス")]
        public string CURRENT_SITUATION { get; set; }

        /// <summary>今後の予定</summary>
        [StringLength(2000)]
        [Display(Name = "今後の予定")]
        public string FUTURE_SCHEDULE { get; set; }

        /// <summary>キーワード</summary>
        [StringLength(200)]
        [Display(Name = "キーワード")]
        public string SELECT_KEYWORD { get; set; }

        /// <summary>編集者</summary>
        [StringLength(20)]
        [Display(Name = "編集者")]
        public string NAME { get; set; }

        /// <summary>編集日時</summary>
        [DataType(DataType.Date)]
        [Display(Name = "編集日時")]
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>ID</summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "ID")]
        public long ID { get; set; }
        
        ///// <summary>ソート順</summary>
        //[Range(0, int.MaxValue)]
        //[Display(Name = "ソート順")]
        //public int? SORT_NO { get; set; }
        #endregion
    }
    #endregion
}