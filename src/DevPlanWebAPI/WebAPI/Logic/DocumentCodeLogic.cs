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
    /// <remarks>資料分類コード検索</remarks>
    public class DocumentCodeLogic : BaseLogic
    {
        /// <summary>
        /// 資料分類コードの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DocumentCodeModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.分類コード");
            sql.AppendLine("     ,A.意味");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_資料分類コード A");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.ID");

            return db.ReadModelList<DocumentCodeModel>(sql.ToString(), prms);
        }
    }
}