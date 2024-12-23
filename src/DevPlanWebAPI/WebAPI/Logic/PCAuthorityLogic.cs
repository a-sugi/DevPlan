using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// PC端末権限ロジッククラス
    /// </summary>
    /// <remarks>PC端末権限の操作</remarks>
    public class PCAuthorityLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// PC端末権限の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(PCAuthorityGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     PC_AUTH.ID");
            sql.AppendLine("    ,PC_AUTH.PC_NAME");
            sql.AppendLine("    ,PC_AUTH.FUNCTION_ID");
            sql.AppendLine("    ,FUNCTION_MST.FUNCTION_NAME");
            sql.AppendLine("FROM");
            sql.AppendLine("    PC_AUTH");
            sql.AppendLine("    INNER JOIN FUNCTION_MST");
            sql.AppendLine("    ON PC_AUTH.FUNCTION_ID = FUNCTION_MST.ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // NetBIOS名
            if (val != null && !string.IsNullOrWhiteSpace(val.PC_NAME))
            {
                sql.AppendLine("    AND PC_AUTH.PC_NAME = :PC_NAME");

                prms.Add(new BindModel
                {
                    Name = ":PC_NAME",
                    Type = OracleDbType.Varchar2,
                    Object = val.PC_NAME,
                    Direct = ParameterDirection.Input
                });
            }

            // 機能ID
            if (val != null && val.FUNCTION_ID > 0)
            {
                sql.AppendLine("    AND PC_AUTH.FUNCTION_ID = :FUNCTION_ID");

                prms.Add(new BindModel
                {
                    Name = ":FUNCTION_ID",
                    Type = OracleDbType.Int32,
                    Object = val.FUNCTION_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     PC_AUTH.FUNCTION_ID ASC");
            sql.AppendLine("    ,PC_AUTH.PC_NAME ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}