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
    /// <remarks>CAP仕向地</remarks>
    public class CapLocationLogic : BaseLogic
    {
        /// <summary>
        /// CAP仕向地データの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CapLocationModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.仕向");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_仕向 A");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.SORT_NO");

            return db.ReadModelList<CapLocationModel>(sql.ToString(), prms);
        }
    }
}