using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    #region 車検・点検リスト発行検索条件クラス
    /// <summary>
    /// 車検・点検リスト発行検索条件クラス
    /// </summary>
    public class CarInspectionSearchModel : TestCarCommonSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 点検区分
        /// </summary>
        [Display(Name = "点検区分")]
        public short?[] 点検区分 { get; set; }

        /// <summary>
        /// 車検区分1
        /// </summary>
        [Display(Name = "車検区分1")]
        public string[] 車検区分1 { get; set; }
        #endregion
    }
    #endregion

    #region 車検・点検リスト発行クラス
    /// <summary>
    /// 車検・点検リスト発行検索条件クラス
    /// </summary>
    public class CarInspectionModel : TestCarCommonModel
    {
        #region プロパティ
        /// <summary>
        /// 点検区分
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "点検区分")]
        public short? 点検区分 { get; set; }

        /// <summary>
        /// 点検区分名
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "点検区分名")]
        public string 点検区分名 { get; set; }

        /// <summary>
        /// 点検期限
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "点検期限")]
        public DateTime? 点検期限 { get; set; }
        #endregion
    }
    #endregion
}