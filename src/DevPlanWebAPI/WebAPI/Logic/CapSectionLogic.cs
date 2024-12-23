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
    /// <remarks>CAP部署</remarks>
    public class CapSectionLogic : BaseLogic
    {
        /// <summary>
        /// CAP部署データの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CapSectionModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.SECTION_ID");
            sql.AppendLine("     ,A.SECTION_GROUP_ID");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_CAP部署 A");

            return db.ReadModelList<CapSectionModel>(sql.ToString(), prms);
        }
    }
}