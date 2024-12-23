using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 車系ロジッククラス
    /// </summary>
    /// <remarks>車系検索</remarks>
    public class CarGroupLogic : BaseLogic
    {
        /// <summary>
        /// 車系データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(CarGroupSearchInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("     GNR.CAR_GROUP");
            sql.AppendLine("    ,MIN(GNR.SORT_NUMBER) AS NUM");
            sql.AppendLine("    ,CASE WHEN MAX(ATH.GENERAL_CODE) IS NOT NULL THEN 1 ELSE 0 END PERMIT_FLG");
            sql.AppendLine("FROM");
            sql.AppendLine("    GENERAL_CODE GNR");
            sql.AppendLine("    LEFT JOIN 試験計画_他部署閲覧許可 ATH");
            sql.AppendLine("    ON GNR.GENERAL_CODE = ATH.GENERAL_CODE");
            sql.AppendLine("    AND ATH.PERSONEL_ID = :PERSONEL_ID");
            sql.AppendLine("    AND ATH.PERMISSION_PERIOD_START <= TRUNC(SYSDATE)");
            sql.AppendLine("    AND ATH.PERMISSION_PERIOD_END >= TRUNC(SYSDATE)");

            // カーシェア日程
            if (val.FUNCTION_CLASS == Const.CarShare)
            {
                sql.AppendLine("        INNER JOIN CARSHARING_SCHEDULE_ITEM SCH");
                sql.AppendLine("        ON GNR.GENERAL_CODE = SCH.GENERAL_CODE");
            }
            // 外製車日程
            else if (val.FUNCTION_CLASS == Const.OuterCar)
            {
                sql.AppendLine("        INNER JOIN OUTERCAR_SCHEDULE_ITEM SCH");
                sql.AppendLine("        ON GNR.GENERAL_CODE = SCH.GENERAL_CODE");
            }

            sql.AppendLine("WHERE 0 = 0");

            if (val.UNDER_DEVELOPMENT != null)
            {
                sql.AppendLine("    AND GNR.UNDER_DEVELOPMENT = :UNDER_DEVELOPMENT");
            }

            sql.AppendLine("    AND GNR.GENERAL_CODE LIKE '%系'");
            sql.AppendLine("GROUP BY");
            sql.AppendLine("    GNR.CAR_GROUP");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    NUM");

            prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":UNDER_DEVELOPMENT", Type = OracleDbType.Varchar2, Object = val.UNDER_DEVELOPMENT, Direct = ParameterDirection.Input });

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}