using System.Data;
using System.Text;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>所在地検索</remarks>
    public class LocationLogic : BaseLogic
    {
        /// <summary>
        /// 所在地データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public List<LocationSearchOutModel> GetData(LocationSearchInModel val)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    '群馬' AS \"所在地\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    DUAL");
            sql.AppendLine("UNION ALL");
            sql.AppendLine("SELECT");
            sql.AppendLine("    'SKC' AS \"所在地\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    DUAL");

            return db.ReadModelList<LocationSearchOutModel>(sql.ToString(), prms);
        }

    }
}