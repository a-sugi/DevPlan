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
    /// <remarks>資料評価レベル検索</remarks>
    public class DocumentLevelLogic : BaseLogic
    {
        /// <summary>
        /// 資料評価レベルの取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DocumentLevelModel> Get()
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.評価レベル");
            sql.AppendLine("     ,A.項目");
            sql.AppendLine("     ,A.レベル基準");
            sql.AppendLine("     ,A.判断イメージ");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_資料評価レベル A");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.ID");

            return db.ReadModelList<DocumentLevelModel>(sql.ToString(), prms);
        }
    }
}