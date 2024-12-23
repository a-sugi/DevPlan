using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 試験車スケジュール一括本予約クラス
    /// <summary>
    /// 試験車スケジュール一括本予約クラス
    /// </summary>
    [Serializable]
    public class TestCarScheduleReserveModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        [Required]
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>対象期間（開始）</summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "対象期間（開始）")]
        public DateTime TARGET_START_DATE { get; set; }

        /// <summary>対象期間（終了）</summary>
        [DataType(DataType.Date)]
        [Display(Name = "対象期間（終了）")]
        public DateTime? TARGET_END_DATE { get; set; }

        /// <summary>入力者パーソナルID</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "入力者パーソナルID")]
        public string INPUT_PERSONEL_ID { get; set; }
        #endregion
    }
    #endregion
}