using System.Data;
using System.Text;
using System.Collections.Generic;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>機能マスター一覧検索</remarks>
    public class FunctionLogic : BaseLogic
    {
        /// <summary>
        /// 機能マスター一覧データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(FunctionInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     FUNCTION_NAME");
            sql.AppendLine("    ,ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    FUNCTION_MST");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    ID");

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}