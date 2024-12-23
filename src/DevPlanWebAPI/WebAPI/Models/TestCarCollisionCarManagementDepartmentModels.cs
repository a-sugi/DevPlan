using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    #region 試験車衝突車管理部署検索クラス
    /// <summary>
    /// 試験車衝突車管理部署検索クラス
    /// </summary>
    public class TestCarCollisionCarManagementDepartmentSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 担当取得可否
        /// </summary>
        public bool? IS_SECTION_GROUP { get; set; }
        #endregion

    }
    #endregion

    #region 試験車衝突車管理部署クラス
    /// <summary>
    /// 試験車衝突車管理部署クラス
    /// </summary>
    public class TestCarCollisionCarManagementDepartmentModel
    {
        #region プロパティ
        /// <summary>
        /// 部ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 部コード
        /// </summary>
        [StringLength(40)]
        [Display(Name = "部コード")]
        public string DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課ID")]
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 課コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課コード")]
        public string SECTION_CODE { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当ID")]
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 担当コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当コード")]
        public string SECTION_GROUP_CODE { get; set; }
        #endregion
    }
    #endregion
}