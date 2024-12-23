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
    /// <remarks>CAP種別</remarks>
    public class CapKindLogic : BaseLogic
    {
        /// <summary>
        /// CAP種別データの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CapKindModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.種別");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_CAP_種別 A");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.SORT_NO");

            return db.ReadModelList<CapKindModel>(sql.ToString(), prms);
        }
    }
}