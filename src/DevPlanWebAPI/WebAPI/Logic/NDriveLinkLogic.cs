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
    /// 写真・動画API
    /// </summary>
    /// <remarks>お知らせデータの操作</remarks>
    public class NDriveLinkLogic : BaseLogic
    {
        /// <summary>
        /// 写真・動画データの取得
        /// </summary>
        /// <remarks>写真・動画データの操作</remarks>
        /// <returns>DataTable</returns>
        public DataTable GetNDriveLinkListData(NDriveLinkInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("        ID ");
            sql.AppendLine("        ,CAP_ID ");
            sql.AppendLine("        ,TITLE ");
            sql.AppendLine("        ,URL ");
            sql.AppendLine("        ,CHANGE_DATETIME ");
            sql.AppendLine(@"       ,PERSONEL_LIST.""NAME""");
            sql.AppendLine("    FROM");
            sql.AppendLine("        N_DRIVE_LINK");
            sql.AppendLine("        LEFT OUTER JOIN PERSONEL_LIST ON N_DRIVE_LINK.CHANGE_PERSONEL_ID = PERSONEL_LIST.PERSONEL_ID");
            sql.AppendLine("    WHERE");
            sql.AppendLine("        0 = 0");

            // ID指定
            if (val != null && val.ID > 0)
            {
                sql.AppendLine("        AND ID= :ID");
                prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = val.ID, Direct = ParameterDirection.Input });
            }
            // CAP_ID指定
            if (val != null && val.CAP_ID > 0)
            {
                sql.AppendLine("        AND CAP_ID= :CAP_ID");
                prms.Add(new BindModel { Name = ":CAP_ID", Type = OracleDbType.Int64, Object = val.CAP_ID, Direct = ParameterDirection.Input });
            }

            sql.AppendLine("    ORDER BY ");
            sql.AppendLine("        CHANGE_DATETIME ASC ");

            DataTable dt = db.ReadDataTable(sql.ToString(), prms);
            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 写真・動画データの作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostNDriveLinkListData(NDriveLinkRegistInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"INSERT INTO");
            sql.AppendLine(@"    ""N_DRIVE_LINK""");
            sql.AppendLine(@"    (");
            sql.AppendLine(@"        ""ID"",");
            sql.AppendLine(@"        ""CAP_ID"",");
            sql.AppendLine(@"        ""TITLE"",");
            sql.AppendLine(@"        ""URL"",");
            sql.AppendLine(@"        ""INPUT_DATETIME"",");
            sql.AppendLine(@"        ""INPUT_PERSONEL_ID"",");
            sql.AppendLine(@"        ""CHANGE_DATETIME"",");
            sql.AppendLine(@"        ""CHANGE_PERSONEL_ID""");
            sql.AppendLine(@"    )");
            sql.AppendLine(@"    VALUES");
            sql.AppendLine(@"    (");
            sql.AppendLine(@"        NVL2((SELECT MAX(""ID"") FROM ""N_DRIVE_LINK""), (SELECT MAX(""ID"") + 1 FROM ""N_DRIVE_LINK""), 1),");
            sql.AppendLine(@"        :CAP_ID,");
            sql.AppendLine(@"        :TITLE,");
            sql.AppendLine(@"        :URL,");
            sql.AppendLine(@"        SYSDATE,");
            sql.AppendLine(@"        :CHANGEPERSONELID,");
            sql.AppendLine(@"        SYSDATE,");
            sql.AppendLine(@"        :CHANGEPERSONELID");
            sql.AppendLine(@"    )");

            prms.Add(new BindModel { Name = ":CAP_ID", Type = OracleDbType.Int64, Object = val.CAP_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":TITLE", Type = OracleDbType.Varchar2, Object = val.TITLE, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":URL", Type = OracleDbType.Varchar2, Object = val.URL, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":CHANGEPERSONELID", Type = OracleDbType.Varchar2, Object = val.CHANGE_PERSONEL_ID, Direct = ParameterDirection.Input });

            return db.InsertData(sql.ToString(), prms);
        }

        /// <summary>
        /// 写真・動画データの更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutNDriveLinkListData(NDriveLinkUpdateInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"UPDATE");
            sql.AppendLine(@"    ""N_DRIVE_LINK""");
            sql.AppendLine(@"        SET");
            sql.AppendLine(@"            ""TITLE"" = :TITLE,");
            sql.AppendLine(@"            ""URL"" = :URL,");
            sql.AppendLine(@"            ""CHANGE_DATETIME"" = SYSDATE,");
            sql.AppendLine(@"            ""CHANGE_PERSONEL_ID"" = :CHANGEPERSONELID");
            sql.AppendLine(@"        WHERE");
            sql.AppendLine(@"            ""ID"" = :ID");

            prms.Add(new BindModel { Name = ":TITLE", Type = OracleDbType.Varchar2, Object = val.TITLE, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":URL", Type = OracleDbType.Varchar2, Object = val.URL, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":CHANGEPERSONELID", Type = OracleDbType.Varchar2, Object = val.CHANGE_PERSONEL_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = val.ID, Direct = ParameterDirection.Input });

            //Debug.WriteLine(sql);
            return db.UpdateData(sql.ToString(), prms);
        }

        /// <summary>
        /// 写真・動画データの削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteNDriveLinkListData(NDriveLinkDeleteInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"DELETE FROM");
            sql.AppendLine(@"        ""N_DRIVE_LINK""");
            sql.AppendLine(@"    WHERE");
            sql.AppendLine(@"        ""ID"" = :ID");

            prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = val.ID, Direct = ParameterDirection.Input });

            //Debug.WriteLine(sql);
            return db.DeleteData(sql.ToString(), prms);
        }
        
    }
}