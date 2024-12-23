using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;
using System;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>共通</remarks>
    public class CommonLogic : BaseLogic
    {
        #region メンバ変数
        private static readonly string[] Tablenames = { "WORK_SCHEDULE_ITEM", "TESTCAR_SCHEDULE_ITEM", "CARSHARING_SCHEDULE_ITEM", "OUTERCAR_SCHEDULE_ITEM", "CAR_DEVELOPMENT_SCHEDULE_ITEM", "MONTHLY_WORK_SCHEDULE_ITEM" };
        private static readonly string[] Sequentialames = { "SEQ_DEVELOPMENT_SCHEDULE_1", "SEQ_MONTHLY_WORK_SCHEDULE_1" };
        #endregion

        /// <summary>
        /// スケジュールIDの取得
        /// </summary>
        /// <returns>long</returns>
        public long GetScheduleNewID(int seqId = 0)
        {
            var seqName = Sequentialames[seqId];

            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendFormat("    {0}", seqName).AppendLine(".NEXTVAL");
            sql.AppendLine("FROM");
            sql.AppendLine("    DUAL");

            return (long)db.ReadDataTable(sql.ToString(), prms).Rows[0].Field<decimal>("NEXTVAL");

        }

        /// <summary>
        /// スケジュール項目の並び順更新(開発符号)
        /// </summary>
        /// <param name="tableId">テーブルID</param>
        /// <param name="generalCode">開発符号</param>
        /// <returns></returns>
        public bool UpdateScheduleItemSortNoByGeneralCode(int tableId, string generalCode)
        {
            if (Tablenames[tableId] == null)
            {
                return false;
            }

            var tableName = Tablenames[tableId];

            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendFormat("    \"{0}\" A", tableName).AppendLine();
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             \"ID\"");
            sql.AppendLine("            ,ROW_NUMBER() OVER (PARTITION BY \"GENERAL_CODE\" ORDER BY SORT_NO) AS \"SORT_NO\"");
            sql.AppendLine("        FROM");
            sql.AppendFormat("            \"{0}\"", tableName).AppendLine();
            sql.AppendLine("        WHERE 0 = 0");

            //開発符号
            if (string.IsNullOrWhiteSpace(generalCode) == false)
            {
                sql.AppendLine("            AND \"GENERAL_CODE\" = :GENERAL_CODE");
                prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = generalCode, Direct = ParameterDirection.Input });

            }

            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"ID\" = B.\"ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("        A.\"SORT_NO\" = B.\"SORT_NO\"");
            sql.AppendLine("    WHERE 0 = 0");
            sql.AppendLine("        AND NVL(A.\"SORT_NO\",0) <> NVL(B.\"SORT_NO\",0)");

            return db.UpdateData(sql.ToString(), prms);

        }

        /// <summary>
        /// スケジュール項目の並び順更新(対象月・FLAG_月頭月末)
        /// </summary>
        /// <param name="tableId">テーブルID</param>
        /// <param name="month">month</param>
        /// <param name="flg">flg</param>
        /// <param name="sectiongroupid">sectiongroupid</param>
        /// <returns></returns>
        public bool UpdateScheduleItemSortNoByMonthly(int tableId, DateTime? month, int? flg, string sectiongroupid)
        {
            if (Tablenames[tableId] == null)
            {
                return false;
            }

            var tableName = Tablenames[tableId];

            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendFormat("    \"{0}\" A", tableName).AppendLine();
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             \"ID\"");
            sql.AppendLine("            ,ROW_NUMBER() OVER (PARTITION BY \"対象月\", \"FLAG_月頭月末\", \"SECTION_GROUP_ID\" ORDER BY SORT_NO) AS \"SORT_NO\"");
            sql.AppendLine("        FROM");
            sql.AppendFormat("            \"{0}\"", tableName).AppendLine();
            sql.AppendLine("        WHERE 0 = 0");

            //対象月
            if (month != null)
            {
                sql.AppendLine("            AND \"対象月\" = :対象月");
                prms.Add(new BindModel { Name = ":対象月", Type = OracleDbType.Date, Object = month, Direct = ParameterDirection.Input });

            }

            //FLAG_月頭月末
            if (flg == 1 || flg == 2)
            {
                sql.AppendLine("            AND \"FLAG_月頭月末\" = :FLAG_月頭月末");
                prms.Add(new BindModel { Name = ":FLAG_月頭月末", Type = OracleDbType.Int16, Object = flg, Direct = ParameterDirection.Input });
            }

            //所属グループID
            if (!string.IsNullOrWhiteSpace(sectiongroupid))
            {
                sql.AppendLine("            AND \"SECTION_GROUP_ID\" = :SECTION_GROUP_ID");
                prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = sectiongroupid, Direct = ParameterDirection.Input });
            }

            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"ID\" = B.\"ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("        A.\"SORT_NO\" = B.\"SORT_NO\"");
            sql.AppendLine("    WHERE 0 = 0");
            sql.AppendLine("        AND A.\"SORT_NO\" <> B.\"SORT_NO\"");

            return db.UpdateData(sql.ToString(), prms);

        }

        /// <summary>
        /// スケジュール項目情報の取得
        /// </summary>
        /// <returns>DataRow</returns>
        public DataRow GetScheduleItemRow(long scheduleid, int tableid)
        {
            if (scheduleid <= 0)
            {
                return null;
            }

            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            if (Tablenames[tableid] == null)
            {
                return null;
            }

            sql.AppendLine("SELECT");
            sql.AppendLine("    *");
            sql.AppendLine("FROM");
            sql.AppendFormat("    {0}", Tablenames[tableid]).AppendLine();
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND ID = :ID ");

            // スケジュールID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Double,
                Object = scheduleid,
                Direct = ParameterDirection.Input
            });

            var dt = db.ReadDataTable(sql.ToString(), prms);

            return dt.Rows.Count == 0 ? null : dt.Rows[0];
        }
    }
}