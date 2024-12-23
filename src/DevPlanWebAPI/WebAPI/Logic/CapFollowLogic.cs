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
    /// <remarks>CAPフォロー状況</remarks>
    public class CapFollowLogic : BaseLogic
    {
        /// <summary>
        /// CAPフォロー状況データの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CapFollowModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.フォロー状況");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_CAP_フォロー状況 A");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.SORT_NO");

            return db.ReadModelList<CapFollowModel>(sql.ToString(), prms);
        }
    }
}