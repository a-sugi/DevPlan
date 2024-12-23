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
    /// <remarks>役職検索</remarks>
    public class OfficialPositionLogic : BaseLogic
    {
        /// <summary>
        /// 役職データの取得
        /// </summary>
        /// <returns>List</returns>
        public List<OfficialPositionModel> GetData(OfficialPositionGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    OFFICIAL_POSITION");
            sql.AppendLine("FROM");
            sql.AppendLine("    PERSONEL_LIST");
            sql.AppendLine("    WHERE OFFICIAL_POSITION IS NOT NULL");

            if (val.OFFICIAL_POSITION != null)
            {
                sql.AppendLine("    AND OFFICIAL_POSITION = :OFFICIAL_POSITION");
                prms.Add(new BindModel
                {
                    Name = ":OFFICIAL_POSITION",
                    Type = OracleDbType.Varchar2,
                    Object = val.OFFICIAL_POSITION,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("    GROUP BY OFFICIAL_POSITION");
            sql.AppendLine("    ORDER BY OFFICIAL_POSITION");

            return db.ReadModelList<OfficialPositionModel>(sql.ToString(), prms);
        }
    }
}