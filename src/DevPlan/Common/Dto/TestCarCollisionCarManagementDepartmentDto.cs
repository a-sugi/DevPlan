using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
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
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 担当コード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        #endregion
    }
    #endregion

}
