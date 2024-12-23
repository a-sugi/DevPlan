using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region スケジュール利用車検索条件クラス
    /// <summary>
    /// スケジュール利用車検索条件クラス
    /// </summary>
    [Serializable]
    public class AllScheduleSearchModel
    {
        /// <summary>管理票番号</summary>
        [StringLength(10)]
        [Display(Name = "管理票番号")]
        public string 管理票番号 { get; set; }

        /// <summary>期間(From)</summary>
        [DataType(DataType.Date)]
        [Display(Name = "期間(From)")]
        public DateTime? START_DATE { get; set; }

        /// <summary>期間(To)</summary>
        [DataType(DataType.Date)]
        [Display(Name = "期間(To)")]
        public DateTime? END_DATE { get; set; }
    }
    #endregion

    #region スケジュール利用車項目クラス
    /// <summary>
    /// スケジュール利用車項目クラス
    /// </summary>
    [Serializable]
    public class AllScheduleModel
    {
        /// <summary>日程区分</summary>
        [Display(Name = "日程区分")]
        public string SCHEDULE_TYPE { get; set; }

        /// <summary>ID</summary>
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>開発符号</summary>
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>開始日</summary>
        [Display(Name = "開始日")]
        public DateTime? START_DATE { get; set; }

        /// <summary>終了日</summary>
        [Display(Name = "終了日")]
        public DateTime? END_DATE { get; set; }

        /// <summary>カテゴリーID</summary>
        [Display(Name = "カテゴリーID")]
        public long? CATEGORY_ID { get; set; }

        /// <summary>行番号</summary>
        [Display(Name = "行番号")]
        public int? PARALLEL_INDEX_GROUP { get; set; }
    }
    #endregion
}