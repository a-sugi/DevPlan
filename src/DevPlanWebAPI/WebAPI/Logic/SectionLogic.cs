using System.Collections.Generic;
using System.Data;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>課検索</remarks>
    public class SectionLogic : BaseLogic
    {
        /// <summary>
        /// 課データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(SectionSearchInModel val)
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.DEPARTMENT_NAME");
            sql.AppendLine("    ,A.DEPARTMENT_CODE");
            sql.AppendLine("    ,A.DEPARTMENT_ID");
            sql.AppendLine("    ,A.ESTABLISHMENT");
            sql.AppendLine("    ,A.SORT_NO AS SORT_NO_DEPARTMENT_DATA");
            sql.AppendLine("    ,B.SECTION_NAME");
            sql.AppendLine("    ,B.SECTION_CODE");
            sql.AppendLine("    ,B.SECTION_ID");
            sql.AppendLine("    ,B.SORT_NO AS SORT_NO_SECTION_DATA");
            sql.AppendLine("    ,A.FLAG_KENJITSU");
            sql.AppendLine("FROM ");
            sql.AppendLine("    DEPARTMENT_DATA A");
            sql.AppendLine("    INNER JOIN SECTION_DATA B");
            sql.AppendLine("    ON A.DEPARTMENT_ID = B.DEPARTMENT_ID");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.FLAG_EXIST = 1");
            sql.AppendLine("    AND B.FLAG_EXIST = 1");

            if (val.DEPARTMENT_ID != null)
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
            if (val.DEPARTMENT_CODE != null)
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
            if (val.SECTION_ID != null )
            {
                sql.AppendLine("    AND B.SECTION_ID = :SECTION_ID");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.SECTION_CODE != null)
            {
                sql.AppendLine("    AND B.SECTION_CODE LIKE :SECTION_CODE");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_CODE",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.SECTION_CODE + "%",
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.SORT_NO");
            sql.AppendLine("    ,B.SORT_NO");

            return db.ReadDataTable(sql.ToString(), prms);

        }

    }
}