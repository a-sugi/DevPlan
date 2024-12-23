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
    /// 状況記号（設計チェック）ロジッククラス
    /// </summary>
    /// <remarks>状況記号（設計チェック）の操作</remarks>
    public class DesignCheckProgressSymbolLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 状況記号（設計チェック）の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(DesignCheckProgressSymbolGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,記号");
            sql.AppendLine("    ,説明");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_状況記号");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // 開催日ID
            if (val != null && val.ID > 0)
            {
                sql.AppendLine("    AND ID = :ID");

                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Int32,
                    Object = val.ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     ID ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}