using System;
using System.ComponentModel.DataAnnotations;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 試験車スケジュール要望案コピークラス
    /// <summary>
    /// 試験車スケジュール要望案コピークラス
    /// </summary>
    [Serializable]
    public class TestCarScheduleCopyModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        [Required]
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>コピー元ステータス</summary>
        [Required]
        [MaxLength(20)]
        [Display(Name = "コピー元ステータス")]
        public string SOURCE_STATUS { get; set; }

        /// <summary>コピー先ステータス</summary>
        [Required]
        [MaxLength(20)]
        [Display(Name = "コピー先ステータス")]
        public string TARGET_STATUS { get; set; }

        /// <summary>対象期間(開始)</summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "対象期間(開始)")]
        public DateTime TARGET_START_DATE { get; set; }

        /// <summary>対象期間(終了)</summary>
        [DataType(DataType.Date)]
        [Display(Name = "対象期間(終了)")]
        public DateTime? TARGET_END_DATE { get; set; }
        #endregion
    }
    #endregion
}