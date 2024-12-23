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
    /// <remarks>CAP織込時期</remarks>
    public class CapStageLogic : BaseLogic
    {
        /// <summary>
        /// CAP織込時期データの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CapStageModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     ID,");
            sql.AppendLine("     織込時期,");
            sql.AppendLine("     SORT_NO,");
            sql.AppendLine("     FLAG_DISP");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_CAP_織込時期");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    SORT_NO");

            return db.ReadModelList<CapStageModel>(sql.ToString(), prms);
        }
    }
}