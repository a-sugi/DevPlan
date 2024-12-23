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
    /// <remarks>性能名一覧検索</remarks>
    public class PerformanceNameLogic : BaseLogic
    {
        /// <summary>
        /// 性能名一覧データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(PerformanceNameInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,性能名");
            sql.AppendLine("FROM");
            sql.AppendLine("    目標進度_性能名");
            sql.AppendLine("WHERE 0 = 0");

            if (!string.IsNullOrWhiteSpace(val.SECTION_ID))
            {
                sql.AppendLine("    AND SECTION_ID = :SECTION_ID");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    ID");

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}