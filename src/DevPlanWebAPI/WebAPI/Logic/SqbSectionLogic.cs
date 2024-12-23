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
    /// <remarks>SQB部署</remarks>
    public class SqbSectionLogic : BaseLogic
    {
        /// <summary>
        /// SQB部署データの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SqbSectionModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.DEPARTMENT_ID");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_SQB部署 A");

            return db.ReadModelList<SqbSectionModel>(sql.ToString(), prms);
        }
    }
}