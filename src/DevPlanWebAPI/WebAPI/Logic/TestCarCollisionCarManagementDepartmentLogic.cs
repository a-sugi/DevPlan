using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 試験車衝突車管理部署業務ロジッククラス
    /// </summary>
    public class TestCarCollisionCarManagementDepartmentLogic : TestCarBaseLogic
    {
        #region 取得
        /// <summary>
        /// 試験車衝突車管理部署取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TestCarCollisionCarManagementDepartmentModel> GetData(TestCarCollisionCarManagementDepartmentSearchModel cond)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"DEPARTMENT_ID\"");
            sql.AppendLine("    ,A.\"DEPARTMENT_CODE\"");
            sql.AppendLine("    ,B.\"SECTION_ID\"");
            sql.AppendLine("    ,B.\"SECTION_CODE\"");

            //担当を取得するかどうか
            if (cond.IS_SECTION_GROUP == true)
            {
                sql.AppendLine("    ,C.\"SECTION_GROUP_ID\"");
                sql.AppendLine("    ,C.\"SECTION_GROUP_CODE\"");

            }

            sql.AppendLine("FROM");
            sql.AppendLine("    \"DEPARTMENT_DATA\" A");
            sql.AppendLine("    INNER JOIN \"SECTION_DATA\" B");
            sql.AppendLine("    ON A.\"DEPARTMENT_ID\" = B.\"DEPARTMENT_ID\"");

            //担当を取得するかどうか
            if (cond.IS_SECTION_GROUP == true)
            {
                sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" C");
                sql.AppendLine("    ON B.\"SECTION_ID\" = C.\"SECTION_ID\"");

            }

            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND EXISTS");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                        *");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"試験車_衝突車管理部署\" D");
            sql.AppendLine("                    WHERE 0 = 0");
            sql.AppendLine("                        AND B.\"SECTION_ID\" = D.\"SECTION_ID\"");
            sql.AppendLine("                )");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"SORT_NO\"");
            sql.AppendLine("    ,B.\"SORT_NO\"");

            //担当を取得するかどうか
            if (cond.IS_SECTION_GROUP == true)
            {
                sql.AppendLine("    ,C.\"SORT_NO\"");

            }

            return db.ReadModelList<TestCarCollisionCarManagementDepartmentModel>(sql.ToString(), null);

        }
        #endregion
    }
}