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
    /// <remarks>CAP指摘分類</remarks>
    public class CapIdentificationLogic : BaseLogic
    {
        /// <summary>
        /// CAP指摘分類データの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CapIdentificationModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.指摘分類");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_CAP_指摘分類 A");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.SORT_NO");

            return db.ReadModelList<CapIdentificationModel>(sql.ToString(), prms);
        }
    }
}