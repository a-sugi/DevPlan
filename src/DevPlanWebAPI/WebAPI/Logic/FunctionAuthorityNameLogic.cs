using System.Data;
using System.Text;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>機能権限一覧検索</remarks>
    public class FunctionAuthorityNameLogic : BaseLogic
    {
        /// <summary>
        /// 機能権限一覧データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(FunctionAuthorityNameInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("     DEPARTMENT_DATA.DEPARTMENT_NAME");
            sql.AppendLine("    ,SECTION_DATA.SECTION_NAME");
            sql.AppendLine("    ,SECTION_GROUP_DATA.SECTION_GROUP_NAME");
            sql.AppendLine("    ,PERSONEL_LIST.NAME");
            sql.AppendLine("    ,DEPARTMENT_DATA.DEPARTMENT_ID");
            sql.AppendLine("    ,SECTION_DATA.SECTION_ID");
            sql.AppendLine("    ,SECTION_GROUP_DATA.SECTION_GROUP_ID");
            sql.AppendLine("    ,PERSONEL_LIST.PERSONEL_ID");
            sql.AppendLine("    ,CASE");
            sql.AppendLine("     WHEN FUNCTION_AUTH.PERSONEL_ID IS NOT NULL THEN 4");       // 4:ユーザー権限
            sql.AppendLine("     WHEN FUNCTION_AUTH.SECTION_GROUP_ID IS NOT NULL THEN 3");  // 3:担当権限
            sql.AppendLine("     WHEN FUNCTION_AUTH.SECTION_ID IS NOT NULL THEN 2");        // 2:課権限
            sql.AppendLine("     WHEN FUNCTION_AUTH.DEPARTMENT_ID IS NOT NULL THEN 1");     // 1:部権限
            sql.AppendLine("     ELSE 0 END LAYER");                                        // 0:無効
            sql.AppendLine("    ,FUNCTION_AUTH.DEPARTMENT_ID");
            sql.AppendLine("    ,FUNCTION_AUTH.SECTION_ID");
            sql.AppendLine("    ,FUNCTION_AUTH.SECTION_GROUP_ID");
            sql.AppendLine("    ,FUNCTION_AUTH.PERSONEL_ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    FUNCTION_AUTH");
            sql.AppendLine("    LEFT JOIN DEPARTMENT_DATA ON FUNCTION_AUTH.DEPARTMENT_ID = DEPARTMENT_DATA.DEPARTMENT_ID");
            sql.AppendLine("    LEFT JOIN SECTION_DATA ON FUNCTION_AUTH.SECTION_ID = SECTION_DATA.SECTION_ID");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA ON FUNCTION_AUTH.SECTION_GROUP_ID = SECTION_GROUP_DATA.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN PERSONEL_LIST ON FUNCTION_AUTH.PERSONEL_ID = PERSONEL_LIST.PERSONEL_ID");
            sql.AppendLine("WHERE 0 = 0");

            if (!string.IsNullOrWhiteSpace(val.DEPARTMENT_ID))
            {
                sql.AppendLine("    AND DEPARTMENT_DATA.DEPARTMENT_ID = :DEPARTMENT_ID");
                prms.Add(new BindModel
                {
                    Name = ":DEPARTMENT_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.DEPARTMENT_ID,
                    Direct = ParameterDirection.Input
                });
            }

            if (!string.IsNullOrWhiteSpace(val.SECTION_ID))
            {
                sql.AppendLine("    AND SECTION_DATA.SECTION_ID = :SECTION_ID");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }

            if (!string.IsNullOrWhiteSpace(val.SECTION_GROUP_ID))
            {
                sql.AppendLine("    AND SECTION_GROUP_DATA.SECTION_GROUP_ID = :SECTION_GROUP_ID");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_GROUP_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_GROUP_ID,
                    Direct = ParameterDirection.Input
                });
            }

            if (!string.IsNullOrWhiteSpace(val.PERSONEL_NAME))
            {
                sql.AppendLine("AND ( PERSONEL_LIST.NAME LIKE :NAME )");
                prms.Add(new BindModel
                {
                    Name = ":NAME",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.PERSONEL_NAME + "%",
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    DEPARTMENT_DATA.DEPARTMENT_ID");
            sql.AppendLine("    ,SECTION_DATA.SECTION_ID");
            sql.AppendLine("    ,SECTION_GROUP_DATA.SECTION_GROUP_ID");
            sql.AppendLine("    ,PERSONEL_LIST.PERSONEL_ID");

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}