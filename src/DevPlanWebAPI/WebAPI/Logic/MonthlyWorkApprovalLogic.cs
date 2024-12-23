using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 月次計画承認ロジッククラス
    /// </summary>
    /// <remarks>月次計画承認の操作</remarks>
    public class MonthlyWorkApprovalLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 月次計画承認の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(MonthlyWorkApprovalGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    MWA.SECTION_GROUP_ID");
            sql.AppendLine("    ,MWA.対象月");
            sql.AppendLine("    ,NVL(MWA.FLAG_月頭月末, 1) FLAG_月頭月末");
            sql.AppendLine("    ,NVL(MWA.FLAG_承認, 0) FLAG_承認");
            sql.AppendLine("    ,MWA.承認日時");
            sql.AppendLine("    ,MWA.承認者_PERSONEL_ID");
            sql.AppendLine("    ,PRSN.NAME 承認者_NAME");
            sql.AppendLine("    ,PRSN.SECTION_GROUP_ID 承認者_SECTION_GROUP_ID");
            sql.AppendLine("    ,SD.SECTION_CODE 承認者_SECTION_CODE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_月報承認 MWA");
            sql.AppendLine("    INNER JOIN PERSONEL_LIST PRSN");
            sql.AppendLine("        ON MWA.承認者_PERSONEL_ID = PRSN.PERSONEL_ID");
            sql.AppendLine("    INNER JOIN SECTION_GROUP_DATA SGD");
            sql.AppendLine("        ON PRSN.SECTION_GROUP_ID = SGD.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN SECTION_DATA SD");
            sql.AppendLine("        ON SGD.SECTION_ID = SD.SECTION_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    MWA.SECTION_GROUP_ID IS NOT NULL");
            sql.AppendLine("    AND MWA.対象月 IS NOT NULL");

            // 所属グループID
            if (val != null && !string.IsNullOrWhiteSpace(val.SECTION_GROUP_ID))
            {
                sql.AppendLine("    AND MWA.SECTION_GROUP_ID = :SECTION_GROUP_ID");

                prms.Add(new BindModel
                {
                    Name = ":SECTION_GROUP_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_GROUP_ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 対象月
            if (val != null && val.対象月 != null)
            {
                sql.AppendLine("    AND MWA.対象月 = :対象月");

                prms.Add(new BindModel
                {
                    Name = ":対象月",
                    Type = OracleDbType.Date,
                    Object = getDateTime(val.対象月),
                    Direct = ParameterDirection.Input
                });
            }

            // 月頭月末
            if (val != null && val.FLAG_月頭月末 > 0)
            {
                sql.AppendLine("    AND MWA.FLAG_月頭月末 = :FLAG_月頭月末");

                prms.Add(new BindModel
                {
                    Name = ":FLAG_月頭月末",
                    Type = OracleDbType.Int32,
                    Object = val.FLAG_月頭月末,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    MWA.対象月 ASC");
            sql.AppendLine("    ,MWA.FLAG_月頭月末 ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 月次計画承認の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(MonthlyWorkApprovalPostInModel val)
        {
            var table = this.GetData(new MonthlyWorkApprovalGetInModel { 対象月 = val.対象月, FLAG_月頭月末 = val.FLAG_月頭月末, SECTION_GROUP_ID = val.SECTION_GROUP_ID });

            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            // 月次計画承認の更新
            if (table?.Rows?.Count > 0)
            {
                sql.AppendLine("UPDATE");
                sql.AppendLine("    試験計画_月報承認");
                sql.AppendLine("SET");
                sql.AppendLine("     FLAG_承認 = :FLAG_承認");

                // 承認時のみ
                if (val?.FLAG_承認 == 1)
                {
                    sql.AppendLine("    ,承認日時 = SYSDATE");
                    sql.AppendLine("    ,承認者_PERSONEL_ID = :承認者_PERSONEL_ID");
                }

                sql.AppendLine("WHERE");
                sql.AppendLine("    対象月 = :対象月");
                sql.AppendLine("    AND FLAG_月頭月末 = :FLAG_月頭月末");
                sql.AppendLine("    AND SECTION_GROUP_ID = :SECTION_GROUP_ID");

                prms.Add(new BindModel { Name = ":FLAG_承認", Type = OracleDbType.Int16, Object = val.FLAG_承認, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":承認者_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.承認者_PERSONEL_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":対象月", Type = OracleDbType.Date, Object = val.対象月, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":FLAG_月頭月末", Type = OracleDbType.Int16, Object = val.FLAG_月頭月末, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID, Direct = ParameterDirection.Input });

                if (!db.UpdateData(sql.ToString(), prms))
                {
                    return false;
                }
            }
            // 月次計画承認の追加
            else
            {
                sql.AppendLine("INSERT INTO");
                sql.AppendLine("試験計画_月報承認(");
                sql.AppendLine("    ID");
                sql.AppendLine("    ,対象月");
                sql.AppendLine("    ,FLAG_月頭月末");
                sql.AppendLine("    ,FLAG_承認");
                sql.AppendLine("    ,承認日時");
                sql.AppendLine("    ,承認者_PERSONEL_ID");
                sql.AppendLine("    ,SECTION_GROUP_ID");
                sql.AppendLine(") VALUES (");
                sql.AppendLine("    (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_月報承認)");
                sql.AppendLine("    ,:対象月");
                sql.AppendLine("    ,:FLAG_月頭月末");
                sql.AppendLine("    ,:FLAG_承認");
                sql.AppendLine("    ,SYSDATE");
                sql.AppendLine("    ,:承認者_PERSONEL_ID");
                sql.AppendLine("    ,:SECTION_GROUP_ID");
                sql.AppendLine(")");

                prms.Add(new BindModel { Name = ":対象月", Type = OracleDbType.Date, Object = val.対象月, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":FLAG_月頭月末", Type = OracleDbType.Int16, Object = val.FLAG_月頭月末, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":FLAG_承認", Type = OracleDbType.Int16, Object = val.FLAG_承認, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":承認者_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.承認者_PERSONEL_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID, Direct = ParameterDirection.Input });

                if (!db.InsertData(sql.ToString(), prms))
                {
                    return false;
                }
            }

            // 月頭の承認以外は以降の処理は行わない
            if (val.FLAG_月頭月末 != 1 || val.FLAG_承認 != 1) return true;

            var itemLogic = new MonthlyWorkScheduleItemLogic();
            itemLogic.SetDBAccess(base.db);

            var item = itemLogic.GetData(new MonthlyWorkScheduleItemGetInModel { 対象月 = val.対象月, FLAG_月頭月末 = 2, SECTION_GROUP_ID = val.SECTION_GROUP_ID });

            // 初回以外（すでに月末データがある場合）は以降の処理は行わない
            if (item?.Rows?.Count > 0) return true;

            item = itemLogic.GetData(new MonthlyWorkScheduleItemGetInModel { 対象月 = val.対象月, FLAG_月頭月末 = 1, SECTION_GROUP_ID = val.SECTION_GROUP_ID });

            var common = new CommonLogic();
            common.SetDBAccess(base.db);

            var scheduleLogic = new MonthlyWorkScheduleLogic();
            scheduleLogic.SetDBAccess(base.db);

            foreach (DataRow dr in item.Rows)
            {
                // シーケンス採番
                var categoryid = common.GetScheduleNewID(1);

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
                sql.AppendLine("    ,:SORT_NO");
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
                sql.AppendLine("    ,:FLAG_月報専用項目");
                sql.AppendLine("    ,2");
                sql.AppendLine("    ,:CATEGORY_ID");
                sql.AppendLine("    ,:CLOSED_DATE");
                sql.AppendLine("    ,:DEVELOPMENT_SCHEDULE_ID");
                sql.AppendLine(")");

                prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = dr["GENERAL_CODE"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":CATEGORY", Type = OracleDbType.Varchar2, Object = dr["CATEGORY"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":SORT_NO", Type = OracleDbType.Decimal, Object = dr["SORT_NO"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = dr["PARALLEL_INDEX_GROUP"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":SCHEDULE_ID", Type = OracleDbType.Int64, Object = categoryid, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = dr["SECTION_GROUP_ID"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.承認者_PERSONEL_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":INPUT_LOGIN_ID", Type = OracleDbType.Varchar2, Object = val.承認者_PERSONEL_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":対象月", Type = OracleDbType.Date, Object = dr["対象月"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":担当者", Type = OracleDbType.Varchar2, Object = dr["担当者"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":備考", Type = OracleDbType.Varchar2, Object = dr["備考"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":FLAG_月報専用項目", Type = OracleDbType.Int16, Object = (decimal)dr["FLAG_月報専用項目"] == 1 ? 1 : (Int16?)null, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = categoryid, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":CLOSED_DATE", Type = OracleDbType.Date, Object = dr["CLOSED_DATE"], Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":DEVELOPMENT_SCHEDULE_ID", Type = OracleDbType.Int64, Object = dr["DEVELOPMENT_SCHEDULE_ID"], Direct = ParameterDirection.Input });

                if (!db.InsertData(sql.ToString(), prms))
                {
                    return false;
                }

                var schedule = scheduleLogic.GetData(new MonthlyWorkScheduleGetInModel { 対象月 = val.対象月, FLAG_月頭月末 = 1, SECTION_GROUP_ID = val.SECTION_GROUP_ID, CATEGORY_ID = (long)dr["CATEGORY_ID"] });

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
                    sql.AppendLine("    ,:FLAG_月報専用項目");
                    sql.AppendLine("    ,2");
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
                    prms.Add(new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.承認者_PERSONEL_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":INPUT_LOGIN_ID", Type = OracleDbType.Varchar2, Object = val.承認者_PERSONEL_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":対象月", Type = OracleDbType.Date, Object = dr["対象月"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":担当者", Type = OracleDbType.Varchar2, Object = dr["担当者"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":備考", Type = OracleDbType.Varchar2, Object = dr["備考"], Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":FLAG_月報専用項目", Type = OracleDbType.Int16, Object = (decimal)sdr["FLAG_月報専用項目"] == 1 ? 1 : (Int16?)null, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":NEW_CATEGORY_ID", Type = OracleDbType.Int64, Object = categoryid, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":DEVELOPMENT_SCHEDULE_ID", Type = OracleDbType.Int64, Object = sdr["DEVELOPMENT_SCHEDULE_ID"], Direct = ParameterDirection.Input });
 
                    if (!db.InsertData(sql.ToString(), prms))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}