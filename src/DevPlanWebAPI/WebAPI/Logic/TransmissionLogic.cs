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
    /// <remarks>TM検索</remarks>
    public class TransmissionLogic : BaseLogic
    {
        /// <summary>
        /// トランスミッションデータの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(TransmissionSearchInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("試験車履歴情報.トランスミッション");
            sql.AppendLine("FROM");
            sql.AppendLine("試験車履歴情報");
            sql.AppendLine(",DEPARTMENT_DATA");
            sql.AppendLine(",SECTION_DATA");
            sql.AppendLine(",SECTION_GROUP_DATA");
            sql.AppendLine("WHERE ( DEPARTMENT_DATA.DEPARTMENT_ID = SECTION_DATA.DEPARTMENT_ID )");
            sql.AppendLine("AND ( SECTION_DATA.SECTION_ID = SECTION_GROUP_DATA.SECTION_ID )");
            sql.AppendLine("AND ( SECTION_GROUP_DATA.SECTION_GROUP_ID = 試験車履歴情報.管理責任部署 )");
            sql.AppendLine("AND ( ( DEPARTMENT_DATA.FLAG_EXIST = 1 )");
            sql.AppendLine("AND ( SECTION_DATA.FLAG_EXIST = 1 )");
            sql.AppendLine("AND ( SECTION_GROUP_DATA.FLAG_EXIST = 1 ) )");
            sql.AppendLine("AND ( 試験車履歴情報.トランスミッション IS NOT NULL )");

            if (val.DEPARTMENT_ID != null)
            {
                sql.AppendLine("AND ( DEPARTMENT_DATA.DEPARTMENT_ID = :DEPARTMENT_ID )");
                prms.Add(new BindModel
                {
                    Name = ":DEPARTMENT_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.DEPARTMENT_ID,
                    Direct = ParameterDirection.Input
                });
            }

            if (val.SECTION_ID != null)
            {
                sql.AppendLine("AND ( SECTION_DATA.SECTION_ID = :SECTION_ID )");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("GROUP BY 試験車履歴情報.トランスミッション");
            sql.AppendLine("ORDER BY 試験車履歴情報.トランスミッション");

            return db.ReadDataTable(sql.ToString(), prms);
        }

    }
}