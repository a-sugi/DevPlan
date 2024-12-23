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
    /// <remarks>行先検索</remarks>
    public class GoalLogic : BaseLogic
    {
        /// <summary>
        /// 行先データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(GoalSearchInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("試験計画_外製車日程_行先マスタ.行先");
            sql.AppendLine(",試験計画_外製車日程_行先マスタ.SORT_NO");
            sql.AppendLine("FROM");
            sql.AppendLine("試験計画_外製車日程_行先マスタ");
            sql.AppendLine("WHERE 0 = 0");
            if (val.行先 != null)
            {
                sql.AppendLine("AND 試験計画_外製車日程_行先マスタ.行先 LIKE :行先");
                prms.Add(new BindModel
                {
                    Name = ":行先",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.行先 + "%",
                    Direct = ParameterDirection.Input
                });
            }
            if (val.SORT_NO != 0)
            {
                sql.AppendLine("AND 試験計画_外製車日程_行先マスタ.SORT_NO = :SORT_NO");
                prms.Add(new BindModel
                {
                    Name = ":SORT_NO",
                    Type = OracleDbType.Int16,
                    Object = val.SORT_NO,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY 試験計画_外製車日程_行先マスタ.SORT_NO");

            return db.ReadDataTable(sql.ToString(), prms);
        }

    }
}