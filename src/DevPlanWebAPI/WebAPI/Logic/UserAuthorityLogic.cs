using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 機能権限検索ロジッククラス
    /// </summary>
    /// <remarks>機能権限データの操作</remarks>
    public class UserAuthorityLogic : BaseLogic
    {
        /// <summary>
        /// 機能権限の取得
        /// </summary>
        /// <returns>List</returns>
        public List<UserAuthorityOutModel> GetData(UserAuthorityInModel val)
        {
            var isFunction = val.FUNCTION_ID > 0;

            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    MST.FUNCTION_ID");
            sql.AppendLine("   ,MAX(MST.READ_FLG) READ_FLG");
            sql.AppendLine("   ,MAX(MST.UPDATE_FLG) UPDATE_FLG");
            sql.AppendLine("   ,MAX(MST.EXPORT_FLG) EXPORT_FLG");
            sql.AppendLine("   ,MAX(MST.MANAGEMENT_FLG) MANAGEMENT_FLG");
            sql.AppendLine("   ,MAX(MST.PRINTSCREEN_FLG) PRINTSCREEN_FLG");
            sql.AppendLine("   ,MAX(MST.CARSHARE_OFFICE_FLG) CARSHARE_OFFICE_FLG");
            sql.AppendLine("   ,MAX(MST.ALL_GENERAL_CODE_FLG) ALL_GENERAL_CODE_FLG");
            sql.AppendLine("   ,MAX(MST.SKS_FLG) SKS_FLG");
            sql.AppendLine("   ,MAX(MST.JIBU_UPDATE_FLG) JIBU_UPDATE_FLG");
            sql.AppendLine("   ,MAX(MST.JIBU_EXPORT_FLG) JIBU_EXPORT_FLG");
            sql.AppendLine("   ,MAX(MST.JIBU_MANAGEMENT_FLG) JIBU_MANAGEMENT_FLG");
            sql.AppendLine("   ,LISTAGG(MST.ROLL_ID, ',') WITHIN GROUP (ORDER BY MST.ROLL_ID) ROLL_IDS");
            sql.AppendLine("FROM");
            sql.AppendLine("    ROLL_AUTH ATH");
            sql.AppendLine("    INNER JOIN ROLL_MST MST");
            sql.AppendLine("    ON ATH.ROLL_ID = MST.ROLL_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // 機能ID
            if (isFunction)
            {
                sql.AppendLine("    AND MST.FUNCTION_ID = :FUNCTION_ID");
            }

            sql.AppendLine("    AND (");

            // 部署・役職
            sql.AppendLine("        (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID = :SECTION_ID AND ATH.SECTION_GROUP_ID = :SECTION_GROUP_ID AND ATH.OFFICIAL_POSITION = :OFFICIAL_POSITION AND PERSONEL_ID IS NULL)");
            sql.AppendLine("     OR (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID = :SECTION_ID AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION = :OFFICIAL_POSITION AND PERSONEL_ID IS NULL)");
            sql.AppendLine("     OR (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID IS NULL       AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION = :OFFICIAL_POSITION AND PERSONEL_ID IS NULL)");

            // 部署
            sql.AppendLine("     OR (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID = :SECTION_ID AND ATH.SECTION_GROUP_ID = :SECTION_GROUP_ID AND ATH.OFFICIAL_POSITION IS NULL              AND PERSONEL_ID IS NULL)");
            sql.AppendLine("     OR (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID = :SECTION_ID AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION IS NULL              AND PERSONEL_ID IS NULL)");
            sql.AppendLine("     OR (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID IS NULL       AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION IS NULL              AND PERSONEL_ID IS NULL)");

            // 役職
            sql.AppendLine("     OR (ATH.DEPARTMENT_ID IS NULL          AND ATH.SECTION_ID IS NULL       AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION = :OFFICIAL_POSITION AND PERSONEL_ID IS NULL)");

            // ユーザーID
            sql.AppendLine("     OR (ATH.DEPARTMENT_ID IS NULL          AND ATH.SECTION_ID IS NULL       AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION IS NULL              AND PERSONEL_ID = :PERSONEL_ID)");

            sql.AppendLine("    )");

            sql.AppendLine("GROUP BY");
            sql.AppendLine("    MST.FUNCTION_ID");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    MST.FUNCTION_ID");

            var prms = new List<BindModel>();

            // 機能ID
            if (isFunction)
            {
                prms.Add(new BindModel { Name = ":FUNCTION_ID", Type = OracleDbType.Int64, Object = val.FUNCTION_ID, Direct = ParameterDirection.Input });
            }

            prms.Add(new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = val.DEPARTMENT_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":SECTION_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":OFFICIAL_POSITION", Type = OracleDbType.Varchar2, Object = val.OFFICIAL_POSITION, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });

            return db.ReadModelList<UserAuthorityOutModel>(sql.ToString(), prms);
        }
    }
}