using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    #region 試験車使用履歴検索クラス
    /// <summary>
    /// 試験車使用履歴検索クラス
    /// </summary>
    public class TestCarUseHistorySearchModel
    {
        #region プロパティ
        /// <summary>
        /// データID
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int データID { get; set; }

        /// <summary>
        /// 履歴NO
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "履歴NO")]
        public int 履歴NO { get; set; }

        /// <summary>
        /// SEQNO
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "SEQNO")]
        public int? SEQNO { get; set; }

        /// <summary>
        /// STEPNO
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "STEPNO")]
        public int? STEPNO { get; set; }
        #endregion
    }
    #endregion

    #region 試験車履歴クラス
    /// <summary>
    /// 試験車履歴クラス
    /// </summary>
    public class TestCarHistoryModel
    {
        #region プロパティ
        /// <summary>試験車</summary>
        public TestCarCommonModel TestCar { get; set; }

        /// <summary>試験車使用履歴</summary>
        public IEnumerable<TestCarUseHistoryModel> TestCarUseHistoryList { get; set; }
        #endregion
    }
    #endregion
}