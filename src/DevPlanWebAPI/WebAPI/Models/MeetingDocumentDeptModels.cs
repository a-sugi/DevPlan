using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    #region 検討会資料部記号検索条件クラス
    /// <summary>
    /// 検討会資料部記号検索条件クラス
    /// </summary>
    [Serializable]
    public class MeetingDocumentDeptSearchModel
    {
        #region プロパティ
        #endregion

    }
    #endregion

    #region 検討会資料部記号クラス
    /// <summary>
    /// 検討会資料部記号クラス
    /// </summary>
    [Serializable]
    public class MeetingDocumentDeptModel
    {
        #region プロパティ
        /// <summary>部記号</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "部記号")]
        public string 部記号 { get; set; }

        /// <summary>部名</summary>
        [StringLength(10)]
        [Display(Name = "部名")]
        public string 部名 { get; set; }
        #endregion

    }
    #endregion

}