using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 受領先情報ロジッククラス
    /// </summary>
    /// <remarks>受領先情報の操作</remarks>
    public class RecipientLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 受領先情報の取得
        /// </summary>
        /// <returns>IEnumerable</returns>
        public IEnumerable<RecipientGetOutModel> GetData()
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    受領先");
            sql.AppendLine("FROM");
            sql.AppendLine("    受領先情報");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    受領先 ASC");

            return db.ReadModelList<RecipientGetOutModel>(sql.ToString(), prms);
        }
    }
}