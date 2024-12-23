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
    /// <remarks>目的検索</remarks>
    public class PurposeLogic : BaseLogic
    {
        /// <summary>
        /// 目的データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(PurposeSearchInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("試験計画_外製車日程_目的マスタ.目的");
            sql.AppendLine(",試験計画_外製車日程_目的マスタ.SORT_NO");
            sql.AppendLine("FROM");
            sql.AppendLine("試験計画_外製車日程_目的マスタ");
            sql.AppendLine("WHERE 0 = 0");
            if (val.目的 != null)
            {
                sql.AppendLine("AND ( 試験計画_外製車日程_目的マスタ.目的 LIKE :目的 )");
                prms.Add(new BindModel
                {
                    Name = ":目的",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.目的 + "%",
                    Direct = ParameterDirection.Input
                });
            }
            if (val.SORT_NO != 0)
            {
                sql.AppendLine("AND ( 試験計画_外製車日程_目的マスタ.SORT_NO = :SORT_NO )");
                prms.Add(new BindModel
                {
                    Name = ":SORT_NO",
                    Type = OracleDbType.Int16,
                    Object = val.SORT_NO,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY 試験計画_外製車日程_目的マスタ.SORT_NO");

            return db.ReadDataTable(sql.ToString(), prms);
        }

    }
}