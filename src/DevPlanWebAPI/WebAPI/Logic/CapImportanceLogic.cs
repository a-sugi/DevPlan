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
    /// <remarks>CAP重要度</remarks>
    public class CapImportanceLogic : BaseLogic
    {
        /// <summary>
        /// CAP重要度データの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CapImportanceModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.重要度");
            sql.AppendLine("     ,A.説明");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_CAP_重要度 A");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.SORT_NO");

            return db.ReadModelList<CapImportanceModel>(sql.ToString(), prms);
        }
    }
}