using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 月次計画同期ロジッククラス
    /// </summary>
    /// <remarks>月次計画同期の操作</remarks>
    public class MonthlyWorkSynchronizeLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 月次計画同期データの作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(MonthlyWorkSynchronizePostInModel val)
        {
            var itemLogic = new MonthlyWorkScheduleItemLogic();
            itemLogic.SetDBAccess(base.db);

            // 既存の月次スケジュール項目の取得
            var table = itemLogic.GetData(new MonthlyWorkScheduleItemGetInModel { 対象月 = val.対象月, FLAG_月頭月末 = val.FLAG_月頭月末, SECTION_GROUP_ID = val.SECTION_GROUP_ID, SECTION_ID = val.SECTION_ID });

            var deletelist = new List<MonthlyWorkScheduleItemGetOutModel>();

            foreach (DataRow dr in table.Rows)
            {
                // 月報専用項目は除外
                if (Convert.ToInt32(dr["FLAG_月報専用項目"]) == 1) continue;

                // 削除する月次スケジュールの退避
                deletelist.Add(new MonthlyWorkScheduleItemGetOutModel {
                    DEV_CATEGORY_ID = (long?)dr["DEVELOPMENT_SCHEDULE_ID"],
                    担当者 = dr["担当者"] == DBNull.Value ? null : (string)dr["担当者"],
                    備考 = dr["備考"] == DBNull.Value ? null : (string)dr["備考"]
                });

                itemLogic.DeleteData(new MonthlyWorkScheduleItemDeleteInModel { CATEGORY_ID = (long)dr["CATEGORY_ID"] });
            }

            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            // 業務スケジュール項目（同期元）の取得
            var item = this.GetSynchronizeData(new WorkScheduleGetInModel
            {
                SECTION_ID = val.SECTION_ID,
                SECTION_GROUP_ID = val.SECTION_GROUP_ID,
                DATE_START = new DateTime((int)val.対象月?.Year, (int)val.対象月?.Month, 1),
                DATE_END = new DateTime((int)val.対象月?.Year, (int)val.対象月?.Month, 1).AddMonths(1).AddDays(-1)
            });

            // 業務スケジュール項目（同期元）がない場合は以降の処理は行わない
            if (item?.Rows?.Count <= 0) return true;

            var common = new CommonLogic();
            common.SetDBAccess(base.db);

            var scheduleLogic = new WorkScheduleLogic();
            scheduleLogic.SetDBAccess(base.db);

            foreach (DataRow dr in item.Rows)
            {
                // シーケンス採番
                var categoryid = common.GetScheduleNewID(1);

                var name = deletelist?.Where(x => x.DEV_CATEGORY_ID == (long?)dr["ID"])?.FirstOrDefault()?.担当者;
                var remarks = deletelist?.Where(x => x.DEV_CATEGORY_ID == (long?)dr["ID"])?.FirstOrDefault()?.備考;

                prms = new List<BindModel>();
                sql = new StringBuilder();

                // 月次計画スケジュール項目の作成
                sql.AppendLine("INSERT INTO");
                sql.AppendLine("MONTHLY_WORK_SCHEDULE_ITEM(");
                sql.AppendLine("     GENERAL_CODE");
                sql.AppendLine("    ,CATEGORY");
                sql.AppendLine("    ,SORT_NO");
                sql.AppendLine("    ,PARALLEL_INDEX");
                sql.AppendLine("    ,ID");
                sql.AppendLine("    ,SECTION_GROUP_ID");
                sql.AppendLine("    ,FLAG_SEPARATOR");
                sql.AppendLine("    ,FLAG_CLASS");
                sql.AppendLine("    ,INPUT_DATETIME");
                sql.AppendLine("    ,INPUT_PERSONEL_ID");
                sql.AppendLine("    ,INPUT_LOGIN_ID");
                sql.AppendLine("    ,対象月");
                sql.AppendLine("    ,担当者");
                sql.AppendLine("    ,備考");
                sql.AppendLine("    ,FLAG_月報専用項目");
                sql.AppendLine("    ,FLAG_月頭月末");
                sql.AppendLine("    ,CATEGORY_ID");
                sql.AppendLine("    ,CLOSED_DATE");
                sql.AppendLine("    ,DEVELOPMENT_SCHEDULE_ID");
                sql.AppendLine(") VALUES (");
                sql.AppendLine("     :GENERAL_CODE");
                sql.AppendLine("    ,:CATEGORY");
                sql.AppendLine("    ,(SELECT NVL(MAX(SORT_NO), 0) + 1 FROM MONTHLY_WORK_SCHEDULE_ITEM WHERE 対象月 = :対象月 AND FLAG_月頭月末 = :FLAG_月頭月末 AND SECTION_GROUP_ID = :SECTION_GROUP_ID)");
                sql.AppendLine("    ,:PARALLEL_INDEX_GROUP");
                sql.AppendLine("    ,:SCHEDULE_ID");
                sql.AppendLine("    ,:SECTION_GROUP_ID");
                sql.AppendLine("    ,1");
                sql.AppendLine("    ,'試験計画'");
                sql.AppendLine("    ,SYSDATE");
                sql.AppendLine("    ,:INPUT_PERSONEL_ID");
                sql.AppendLine("    ,:INPUT_LOGIN_ID");
                sql.AppendLine("    ,:対象月");
                sql.AppendLine("    ,:担当者");
                sql.AppendLine("    ,:備考");
                sql.AppendLine("    ,NULL");
                sql.AppendLine("    ,:FLAG_月頭月末");
                sql.AppendLine("    ,:CATEGORY_ID");
                sql.AppendLine("    ,:CLOSED_DATE");
                sql.AppendLine("    ,:DEVELOPMENT_SCHEDULE_ID");
                sql.AppendLine(")");

                prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = dr["GENERAL_CODE"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":CATEGORY", Type = OracleDbType.Varchar2, Object = dr["CATEGORY"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = dr["PARALLEL_INDEX_GROUP"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":SCHEDULE_ID", Type = OracleDbType.Int64, Object = categoryid, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = dr["SECTION_GROUP_ID"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":INPUT_LOGIN_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":対象月", Type = OracleDbType.Date, Object = val.対象月, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":担当者", Type = OracleDbType.Varchar2, Object = name, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":備考", Type = OracleDbType.Varchar2, Object = remarks, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":FLAG_月頭月末", Type = OracleDbType.Int16, Object = (int?)val.FLAG_月頭月末 == 2 ? 2 : 1, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = categoryid, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":CLOSED_DATE", Type = OracleDbType.Date, Object = dr["CLOSED_DATE"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":DEVELOPMENT_SCHEDULE_ID", Type = OracleDbType.Int64, Object = dr["ID"], Direct = ParameterDirection.Input });

                if (!db.InsertData(sql.ToString(), prms))
                {
                    return false;
                }

                // 業務スケジュール（同期元）の取得
                var schedule = scheduleLogic.GetData(new WorkScheduleGetInModel { DATE_START = val.対象月, DATE_END = new DateTime((int)val.対象月?.Year, (int)val.対象月?.Month, (int)((DateTime?)val.対象月?.AddMonths(1))?.AddDays(-1).Day), SECTION_GROUP_ID = val.SECTION_GROUP_ID, CATEGORY_ID = (long)dr["CATEGORY_ID"] });

                foreach (DataRow sdr in schedule.Rows)
                {
                    // シーケンス採番
                    var scheduleid = common.GetScheduleNewID(1);

                    prms = new List<BindModel>();
                    sql = new StringBuilder();

                    // 月次計画スケジュールの作成
                    sql.AppendLine("INSERT INTO");
                    sql.AppendLine("MONTHLY_WORK_SCHEDULE(");
                    sql.AppendLine("     GENERAL_CODE");
                    sql.AppendLine("    ,CATEGORY");
                    sql.AppendLine("    ,START_DATE");
                    sql.AppendLine("    ,END_DATE");
                    sql.AppendLine("    ,SORT_NO");
                    sql.AppendLine("    ,PARALLEL_INDEX");
                    sql.AppendLine("    ,ID");
                    sql.AppendLine("    ,SYMBOL");
                    sql.AppendLine("    ,DESCRIPTION");
                    sql.AppendLine("    ,SECTION_GROUP_ID");
                    sql.AppendLine("    ,ACHIEVEMENT_INDEX");
                    sql.AppendLine("    ,ENFORCEMENT_INDEX");
                    sql.AppendLine("    ,FLAG_CLASS");
                    sql.AppendLine("    ,INPUT_DATETIME");
                    sql.AppendLine("    ,INPUT_PERSONEL_ID");
                    sql.AppendLine("    ,INPUT_LOGIN_ID");
                    sql.AppendLine("    ,対象月");
                    sql.AppendLine("    ,担当者");
                    sql.AppendLine("    ,備考");
                    sql.AppendLine("    ,FLAG_月報専用項目");
                    sql.AppendLine("    ,FLAG_月頭月末");
                    sql.AppendLine("    ,CATEGORY_ID");
                    sql.AppendLine("    ,DEVELOPMENT_SCHEDULE_ID");
                    sql.AppendLine(") VALUES (");
                    sql.AppendLine("     :GENERAL_CODE");
                    sql.AppendLine("    ,:CATEGORY");
                    sql.AppendLine("    ,:START_DATE");
                    sql.AppendLine("    ,:END_DATE");
                    sql.AppendLine("    ,:SORT_NO");
                    sql.AppendLine("    ,:PARALLEL_INDEX_GROUP");
                    sql.AppendLine("    ,:SCHEDULE_ID");
                    sql.AppendLine("    ,:SYMBOL");
                    sql.AppendLine("    ,:DESCRIPTION");
                    sql.AppendLine("    ,:SECTION_GROUP_ID");
                    sql.AppendLine("    ,:ACHIEVEMENT_INDEX");
                    sql.AppendLine("    ,:ENFORCEMENT_INDEX");
                    sql.AppendLine("    ,'試験計画'");
                    sql.AppendLine("    ,SYSDATE");
                    sql.AppendLine("    ,:INPUT_PERSONEL_ID");
                    sql.AppendLine("    ,:INPUT_LOGIN_ID");
                    sql.AppendLine("    ,:対象月");
                    sql.AppendLine("    ,:担当者");
                    sql.AppendLine("    ,:備考");
                    sql.AppendLine("    ,NULL");
                    sql.AppendLine("    ,:FLAG_月頭月末");
                    sql.AppendLine("    ,:NEW_CATEGORY_ID");
                    sql.AppendLine("    ,:DEVELOPMENT_SCHEDULE_ID");
                    sql.AppendLine(")");

                    prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = dr["GENERAL_CODE"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":CATEGORY", Type = OracleDbType.Varchar2, Object = sdr["CATEGORY"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = sdr["START_DATE"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = sdr["END_DATE"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SORT_NO", Type = OracleDbType.Decimal, Object = sdr["SORT_NO"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = sdr["PARALLEL_INDEX_GROUP"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SCHEDULE_ID", Type = OracleDbType.Int64, Object = scheduleid, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SYMBOL", Type = OracleDbType.Int16, Object = sdr["SYMBOL"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":DESCRIPTION", Type = OracleDbType.Varchar2, Object = sdr["DESCRIPTION"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":ACHIEVEMENT_INDEX", Type = OracleDbType.Int16, Object = sdr["ACHIEVEMENT_INDEX"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":ENFORCEMENT_INDEX", Type = OracleDbType.Int16, Object = sdr["ENFORCEMENT_INDEX"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":INPUT_LOGIN_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":対象月", Type = OracleDbType.Date, Object = val.対象月, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":担当者", Type = OracleDbType.Varchar2, Object = name, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":備考", Type = OracleDbType.Varchar2, Object = remarks, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":NEW_CATEGORY_ID", Type = OracleDbType.Int64, Object = categoryid, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":FLAG_月頭月末", Type = OracleDbType.Int16, Object = (int?)val.FLAG_月頭月末 == 2 ? 2 : 1, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":DEVELOPMENT_SCHEDULE_ID", Type = OracleDbType.Int64, Object = sdr["ID"], Direct = ParameterDirection.Input });

                    if (!db.InsertData(sql.ToString(), prms))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 月次計画同期データの取得
        /// </summary>
        /// <returns>bool</returns>
        private DataTable GetSynchronizeData(WorkScheduleGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("     WORK_SCHEDULE_ITEM.GENERAL_CODE");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.CATEGORY");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.SORT_NO");
            sql.AppendLine("    ,NVL(WORK_SCHEDULE_ITEM.PARALLEL_INDEX_GROUP, 1) PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.ID");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.SECTION_GROUP_ID");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.FLAG_CLASS");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.CATEGORY_ID");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.CLOSED_DATE");
            sql.AppendLine("FROM");
            sql.AppendLine("    WORK_SCHEDULE_ITEM");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA");
            sql.AppendLine("    ON WORK_SCHEDULE_ITEM.SECTION_GROUP_ID = SECTION_GROUP_DATA.SECTION_GROUP_ID");
            sql.AppendLine("    INNER JOIN WORK_SCHEDULE");
            sql.AppendLine("    ON WORK_SCHEDULE_ITEM.ID = WORK_SCHEDULE.CATEGORY_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    WORK_SCHEDULE_ITEM.CATEGORY_ID IS NOT NULL");

            // 所属ID
            if (val != null && !string.IsNullOrWhiteSpace(val.SECTION_ID))
            {
                sql.AppendLine("    AND SECTION_GROUP_DATA.SECTION_ID = :SECTION_ID");

                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 所属グループID
            if (val != null && !string.IsNullOrWhiteSpace(val.SECTION_GROUP_ID))
            {
                sql.AppendLine("    AND WORK_SCHEDULE_ITEM.SECTION_GROUP_ID = :SECTION_GROUP_ID");

                prms.Add(new BindModel
                {
                    Name = ":SECTION_GROUP_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_GROUP_ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 期間（開始・終了）
            if (val != null && (val.DATE_START != null || val.DATE_END != null))
            {
                sql.AppendLine("    AND (");
                sql.AppendLine("    (");

                if (val.DATE_START != null)
                {
                    sql.AppendLine("    WORK_SCHEDULE.START_DATE >= :DATE_START");

                    prms.Add(new BindModel
                    {
                        Name = ":DATE_START",
                        Type = OracleDbType.Date,
                        Object = getDateTime(val.DATE_START),
                        Direct = ParameterDirection.Input
                    });
                }

                if (val.DATE_END != null)
                {
                    if (val.DATE_START != null) sql.AppendLine("AND");

                    sql.AppendLine("    WORK_SCHEDULE.START_DATE <= :DATE_END");

                    prms.Add(new BindModel
                    {
                        Name = ":DATE_END",
                        Type = OracleDbType.Date,
                        Object = getDateTime(val.DATE_END),
                        Direct = ParameterDirection.Input
                    });
                }

                sql.AppendLine("    )");
                sql.AppendLine("    OR");
                sql.AppendLine("    (");

                if (val.DATE_START != null)
                {
                    sql.AppendLine("    WORK_SCHEDULE.END_DATE >= :DATE_START");

                    prms.Add(new BindModel
                    {
                        Name = ":DATE_START",
                        Type = OracleDbType.Date,
                        Object = getDateTime(val.DATE_START),
                        Direct = ParameterDirection.Input
                    });
                }

                if (val.DATE_END != null)
                {
                    if (val.DATE_START != null) sql.AppendLine("AND");

                    sql.AppendLine("    WORK_SCHEDULE.END_DATE <= :DATE_END");

                    prms.Add(new BindModel
                    {
                        Name = ":DATE_END",
                        Type = OracleDbType.Date,
                        Object = getDateTime(val.DATE_END),
                        Direct = ParameterDirection.Input
                    });
                }

                sql.AppendLine("    )");
                sql.AppendLine("    )");
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    WORK_SCHEDULE_ITEM.ID ASC");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.SORT_NO ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}