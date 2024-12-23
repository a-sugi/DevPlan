using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 検討会資料名検索条件クラス
    /// <summary>
    /// 検討会資料名検索条件クラス
    /// </summary>
    [Serializable]
    public class MeetingDocumentNameSearchModel
    {
        #region プロパティ
        #endregion

    }
    #endregion

    #region 検討会資料名クラス
    /// <summary>
    /// 検討会資料名クラス
    /// </summary>
    [Serializable]
    public class MeetingDocumentNameModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        /// <summary>資料名</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "資料名")]
        public string 資料名 { get; set; }
        #endregion

    }
    #endregion

}