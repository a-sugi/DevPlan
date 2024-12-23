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
    /// <remarks>ロール名検索</remarks>
    public class RollNameLogic : BaseLogic
    {
        /// <summary>
        /// ロール名データの取得
        /// </summary>
        /// <returns>List</returns>
        public List<RollModel> GetData(RollGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    ROLL_ID");
            sql.AppendLine("   ,ROLL_NAME");
            sql.AppendLine("FROM");
            sql.AppendLine("    ROLL_MST");
            sql.AppendLine("    WHERE ROLL_NAME IS NOT NULL");

            if (val.ROLL_ID != null)
            {
                sql.AppendLine("    AND ROLL_ID = :ROLL_ID");
                prms.Add(new BindModel
                {
                    Name = ":ROLL_ID",
                    Type = OracleDbType.Int64,
                    Object = val.ROLL_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("    GROUP BY ROLL_ID, ROLL_NAME");
            sql.AppendLine("    ORDER BY ROLL_ID");

            return db.ReadModelList<RollModel>(sql.ToString(), prms);
        }
    }
}