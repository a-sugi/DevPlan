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
    /// <remarks>CAP対策予定</remarks>
    public class CapScheduleLogic : BaseLogic
    {
        /// <summary>
        /// CAP対策予定データの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CapScheduleModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.対策予定");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_CAP_対策予定 A");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.SORT_NO");

            return db.ReadModelList<CapScheduleModel>(sql.ToString(), prms);
        }
    }
}