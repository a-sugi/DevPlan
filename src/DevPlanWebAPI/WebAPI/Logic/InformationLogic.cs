//
// 業務計画表システム
// KKA00130～160 お知らせAPI
// 作成者 : 岸　義将
// 作成日 : 2017/02/13

using System.Collections.Generic;
using System.Data;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// お知らせAPI
    /// </summary>
    /// <remarks>お知らせデータの操作</remarks>
    public class InformationLogic : BaseLogic
    {
        /// <summary>
        /// お知らせデータの取得
        /// </summary>
        /// <remarks>お知らせデータの操作</remarks>
        /// <returns>DataTable</returns>
        public DataTable GetAnnounceListData(InformationInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("        ID ");
            sql.AppendLine("        ,TITLE ");
            sql.AppendLine("        ,URL ");
            sql.AppendLine("        ,RELEASE_START_DATE ");
            sql.AppendLine("        ,RELEASE_END_DATE ");
            sql.AppendLine("        ,CHANGE_DATETIME ");
            sql.AppendLine(@"        ,PERSONEL_LIST.""NAME""");
            sql.AppendLine("    FROM");
            sql.AppendLine("        INFORMATION");
            sql.AppendLine("        LEFT OUTER JOIN PERSONEL_LIST ON INFORMATION.CHANGE_PERSONEL_ID = PERSONEL_LIST.PERSONEL_ID");
            sql.AppendLine("    WHERE");
            sql.AppendLine("        0 = 0");

            // ID指定
            if (val != null && val.ID > 0)
            {
                sql.AppendLine("        AND ID= :ID");
                prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = val.ID, Direct = ParameterDirection.Input });
            }

            // ステータス指定
            // 1:公開中のみ
            if (val != null && val.STATUS == 1)
                sql.AppendLine("        AND RELEASE_START_DATE <= TRUNC(SYSDATE) AND TRUNC(SYSDATE) <= RELEASE_END_DATE");

            // 2:全て

            // 3:公開前のみ
            if (val != null && val.STATUS == 3)
                sql.AppendLine("        AND TRUNC(SYSDATE) < RELEASE_START_DATE AND TRUNC(SYSDATE) <= RELEASE_END_DATE");

            // 4:公開終了のみ
            if (val != null && val.STATUS == 4)
                sql.AppendLine("        AND RELEASE_END_DATE < TRUNC(SYSDATE)");

            // 5:公開前と公開中
            if (val != null && val.STATUS == 5)
                sql.AppendLine("        AND TRUNC(SYSDATE) <= RELEASE_END_DATE");

            // 6:公開前と公開終了
            if (val != null && val.STATUS == 6)
            {
                sql.AppendLine("        AND (TRUNC(SYSDATE) < RELEASE_START_DATE AND TRUNC(SYSDATE) <= RELEASE_END_DATE)");
                sql.AppendLine("        OR  (RELEASE_END_DATE < TRUNC(SYSDATE))");
            }
            // 7:公開中と公開終了
            if (val != null && val.STATUS == 7)
            {
                sql.AppendLine("        AND (RELEASE_START_DATE <= TRUNC(SYSDATE) AND TRUNC(SYSDATE) <= RELEASE_END_DATE)");
                sql.AppendLine("        OR  (RELEASE_END_DATE < TRUNC(SYSDATE))");
            }

            sql.AppendLine("    ORDER BY ");
            sql.AppendLine("        RELEASE_START_DATE DESC, ");
            sql.AppendLine("        RELEASE_END_DATE DESC, ");
            sql.AppendLine("        CHANGE_DATETIME ASC ");

            DataTable dt = db.ReadDataTable(sql.ToString(), prms);
            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// お知らせデータの作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostAnnounceListData(InformationRegistInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"INSERT INTO");
            sql.AppendLine(@"    ""INFORMATION""");
            sql.AppendLine(@"    (");
            sql.AppendLine(@"        ""ID"",");
            sql.AppendLine(@"        ""TITLE"",");
            sql.AppendLine(@"        ""URL"",");
            sql.AppendLine(@"        ""RELEASE_START_DATE"",");
            sql.AppendLine(@"        ""RELEASE_END_DATE"",");
            sql.AppendLine(@"        ""INPUT_DATETIME"",");
            sql.AppendLine(@"        ""INPUT_PERSONEL_ID"",");
            sql.AppendLine(@"        ""CHANGE_DATETIME"",");
            sql.AppendLine(@"        ""CHANGE_PERSONEL_ID""");
            sql.AppendLine(@"    )");
            sql.AppendLine(@"    VALUES");
            sql.AppendLine(@"    (");
            sql.AppendLine(@"        NVL2((SELECT MAX(""ID"") FROM ""INFORMATION""), (SELECT MAX(""ID"") + 1 FROM ""INFORMATION""), 1),");
            sql.AppendLine(@"        :TITLE,");
            sql.AppendLine(@"        :URL,");
            sql.AppendLine(@"        :RELEASESTARTDATE,");
            sql.AppendLine(@"        :RELEASEENDDATE,");
            sql.AppendLine(@"        SYSDATE,");
            sql.AppendLine(@"        :CHANGEPERSONELID,");
            sql.AppendLine(@"        SYSDATE,");
            sql.AppendLine(@"        :CHANGEPERSONELID");
            sql.AppendLine(@"    )");

            prms.Add(new BindModel { Name = ":TITLE", Type = OracleDbType.Varchar2, Object = val.TITLE, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":URL", Type = OracleDbType.Varchar2, Object = val.URL, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":RELEASESTARTDATE", Type = OracleDbType.Date, Object = val.RELEASE_START_DATE, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":RELEASEENDDATE", Type = OracleDbType.Date, Object = val.RELEASE_END_DATE, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":CHANGEPERSONELID", Type = OracleDbType.Varchar2, Object = val.CHANGE_PERSONEL_ID, Direct = ParameterDirection.Input });

            return db.InsertData(sql.ToString(), prms);
        }
        
        /// <summary>
        /// お知らせデータの更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutAnnounceListData(InformationUpdateInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"UPDATE");
            sql.AppendLine(@"    ""INFORMATION""");
            sql.AppendLine(@"        SET");
            sql.AppendLine(@"            ""TITLE"" = :TITLE,");
            sql.AppendLine(@"            ""URL"" = :URL,");
            sql.AppendLine(@"            ""RELEASE_START_DATE"" = :RELEASESTARTDATE,");
            sql.AppendLine(@"            ""RELEASE_END_DATE"" = :RELEASEENDDATE,");
            sql.AppendLine(@"            ""CHANGE_DATETIME"" = SYSDATE,");
            sql.AppendLine(@"            ""CHANGE_PERSONEL_ID"" = :CHANGEPERSONELID");
            sql.AppendLine(@"        WHERE");
            sql.AppendLine(@"            ""ID"" = :ID");

            prms.Add(new BindModel { Name = ":TITLE", Type = OracleDbType.Varchar2, Object = val.TITLE, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":URL", Type = OracleDbType.Varchar2, Object = val.URL, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":RELEASESTARTDATE", Type = OracleDbType.Date, Object = val.RELEASE_START_DATE, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":RELEASEENDDATE", Type = OracleDbType.Date, Object = val.RELEASE_END_DATE, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":CHANGEPERSONELID", Type = OracleDbType.Varchar2, Object = val.CHANGE_PERSONEL_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = val.ID, Direct = ParameterDirection.Input });

            //Debug.WriteLine(sql);
            return db.UpdateData(sql.ToString(), prms);
        }

        /// <summary>
        /// お知らせデータの削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteAnnounceListData(InformationDeleteInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"DELETE FROM");
            sql.AppendLine(@"        ""INFORMATION""");
            sql.AppendLine(@"    WHERE");
            sql.AppendLine(@"        ""ID"" = :ID");

            prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = val.ID, Direct = ParameterDirection.Input });

            //Debug.WriteLine(sql);
            return db.DeleteData(sql.ToString(), prms);
        }
        
    }
}