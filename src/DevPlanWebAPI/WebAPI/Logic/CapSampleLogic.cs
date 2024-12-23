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
    /// <remarks>CAP供試品</remarks>
    public class CapSampleLogic : BaseLogic
    {
        /// <summary>
        /// CAP供試品データの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CapSampleModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.供試品");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_CAP_供試品 A");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.SORT_NO");

            return db.ReadModelList<CapSampleModel>(sql.ToString(), prms);
        }
    }
}