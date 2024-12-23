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
    /// <remarks>部検索</remarks>
    public class DepartmentLogic : BaseLogic
    {
        /// <summary>
        /// 部データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(DepartmentSearchInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     A.DEPARTMENT_NAME");
            sql.AppendLine("    ,A.DEPARTMENT_CODE");
            sql.AppendLine("    ,A.DEPARTMENT_ID");
            sql.AppendLine("    ,A.ESTABLISHMENT");
            sql.AppendLine("    ,A.SORT_NO");
            sql.AppendLine("FROM");
            sql.AppendLine("    DEPARTMENT_DATA A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.FLAG_EXIST = 1");

            if (string.IsNullOrWhiteSpace(val.DEPARTMENT_ID) == false)
            {
                sql.AppendLine("    AND A.DEPARTMENT_ID = :DEPARTMENT_ID");
                prms.Add(new BindModel
                {
                    Name = ":DEPARTMENT_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.DEPARTMENT_ID,
                    Direct = ParameterDirection.Input
                });
            }

            if (string.IsNullOrWhiteSpace(val.DEPARTMENT_CODE) == false)
            {
                sql.AppendLine("    AND A.DEPARTMENT_CODE LIKE :DEPARTMENT_CODE");
                prms.Add(new BindModel
                {
                    Name = ":DEPARTMENT_CODE",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.DEPARTMENT_CODE + "%",
                    Direct = ParameterDirection.Input
                });
            }

            if (val.ESTABLISHMENT != null)
            {
                sql.AppendLine("    AND A.ESTABLISHMENT = :ESTABLISHMENT");
                prms.Add(new BindModel { Name = ":ESTABLISHMENT", Type = OracleDbType.Varchar2, Object = val.ESTABLISHMENT, Direct = ParameterDirection.Input });

            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    A.SORT_NO");

            return db.ReadDataTable(sql.ToString(), prms);
        }

    }
}