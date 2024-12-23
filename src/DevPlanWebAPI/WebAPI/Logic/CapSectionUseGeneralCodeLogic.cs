using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// CAP部署（開発符号）検索ロジッククラス
    /// </summary>
    public class CapSectionUseGeneralCodeLogic : BaseLogic
    {
        /// <summary>
        /// CAP部署（開発符号）の取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        /// <remarks>
        /// CAPで課（専門部門）が使用している開発符号の閲覧権限の取得
        /// </remarks>
        public DataTable GetData(CapSectionUseGeneralCodeInModel val)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("		B.\"専門部署名\"");
            sql.AppendLine("		, CASE WHEN ATH.GENERAL_CODE IS NOT NULL THEN 1 ELSE 0 END PERMIT_FLG ");
            sql.AppendLine("    FROM");
            sql.AppendLine("       \"試験計画_CAP_項目\" A ");
            sql.AppendLine("    INNER JOIN \"試験計画_CAP_対応\" B ");
            sql.AppendLine("        ON A.\"ID\" = B.\"項目_ID\" ");
            sql.AppendLine("        AND B.\"専門部署名\" IS NOT NULL ");
            sql.AppendLine("        AND A.GENERAL_CODE IS NOT NULL ");
            sql.AppendLine("    LEFT JOIN 試験計画_他部署閲覧許可 ATH ");
            sql.AppendLine("        ON A.GENERAL_CODE = ATH.GENERAL_CODE ");
            sql.AppendLine("        AND ATH.PERSONEL_ID = :PERSONEL_ID ");
            sql.AppendLine("        AND ATH.PERMISSION_PERIOD_START <= TRUNC(SYSDATE) ");
            sql.AppendLine("        AND ATH.PERMISSION_PERIOD_END >= TRUNC(SYSDATE)");
            sql.AppendLine("	ORDER BY");
            sql.AppendLine("		B.\"専門部署名\"");

            prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}