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
    /// <remarks>車型検索</remarks>
    public class CarModelLogic : BaseLogic
    {
        /// <summary>
        /// 車型データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(CarModelInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT ");
            sql.AppendLine("    VIEW_試験車基本情報.車型 ");
            sql.AppendLine("FROM ");
            sql.AppendLine("    DEPARTMENT_DATA");
            sql.AppendLine("    INNER JOIN SECTION_DATA ON DEPARTMENT_DATA.DEPARTMENT_ID = SECTION_DATA.DEPARTMENT_ID");
            sql.AppendLine("    INNER JOIN SECTION_GROUP_DATA ON SECTION_DATA.SECTION_ID = SECTION_GROUP_DATA.SECTION_ID");
            sql.AppendLine("    INNER JOIN 試験車履歴情報 ON SECTION_GROUP_DATA.SECTION_GROUP_ID = 試験車履歴情報.管理責任部署");
            sql.AppendLine("    INNER JOIN VIEW_試験車基本情報 ON VIEW_試験車基本情報.データID = 試験車履歴情報.データID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    DEPARTMENT_DATA.FLAG_EXIST = 1");
            sql.AppendLine("    AND SECTION_DATA.FLAG_EXIST = 1");
            sql.AppendLine("    AND SECTION_GROUP_DATA.FLAG_EXIST = 1");
            sql.AppendLine("    AND VIEW_試験車基本情報.車型 IS NOT NULL");
            sql.AppendLine("    AND 試験車履歴情報.データID IS NOT NULL");

            if (val.DEPARTMENT_ID != null)
            {
                sql.AppendLine("    AND DEPARTMENT_DATA.DEPARTMENT_ID = :DEPARTMENT_ID");
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
                sql.AppendLine("    AND SECTION_DATA.SECTION_ID = :SECTION_ID");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("GROUP BY");
            sql.AppendLine("    VIEW_試験車基本情報.車型");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    VIEW_試験車基本情報.車型");

            return db.ReadDataTable(sql.ToString(), prms);
        }

    }
}