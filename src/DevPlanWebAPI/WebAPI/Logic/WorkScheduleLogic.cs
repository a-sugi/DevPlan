using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>業務スケジュールの操作</remarks>
    public class WorkScheduleLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 業務スケジュールの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(WorkScheduleGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     WORK_SCHEDULE.CATEGORY");
            sql.AppendLine("    ,NVL(WORK_SCHEDULE.PARALLEL_INDEX_GROUP, 1) PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,WORK_SCHEDULE.SORT_NO");
            sql.AppendLine("    ,WORK_SCHEDULE.START_DATE");
            sql.AppendLine("    ,WORK_SCHEDULE.END_DATE");
            sql.AppendLine("    ,WORK_SCHEDULE.ID");
            sql.AppendLine("    ,WORK_SCHEDULE.SYMBOL");
            sql.AppendLine("    ,WORK_SCHEDULE.DESCRIPTION");
            sql.AppendLine("    ,WORK_SCHEDULE.CATEGORY_ID");
            sql.AppendLine("    ,WORK_SCHEDULE.ACHIEVEMENT_INDEX");
            sql.AppendLine("    ,WORK_SCHEDULE.ENFORCEMENT_INDEX");
            sql.AppendLine("    ,WORK_SCHEDULE.INPUT_DATETIME");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.CLOSED_DATE");
            sql.AppendLine("    ,CASE WHEN WORK_SCHEDULE_ITEM.CLOSED_DATE IS NULL THEN 'open'");
            sql.AppendLine("     ELSE 'close' END AS OPEN_CLOSE");
            sql.AppendLine("FROM");
            sql.AppendLine("    WORK_SCHEDULE");
            sql.AppendLine("    LEFT JOIN WORK_SCHEDULE_ITEM");
            sql.AppendLine("    ON WORK_SCHEDULE.CATEGORY_ID = WORK_SCHEDULE_ITEM.ID");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA");
            sql.AppendLine("    ON WORK_SCHEDULE.SECTION_GROUP_ID = SECTION_GROUP_DATA.SECTION_GROUP_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    WORK_SCHEDULE.CATEGORY_ID IS NOT NULL");

            // 開発符号
            if (val != null && !string.IsNullOrWhiteSpace(val.GENERAL_CODE))
            {
                sql.AppendLine("    AND WORK_SCHEDULE.GENERAL_CODE = :GENERAL_CODE");

                prms.Add(new BindModel
                {
                    Name = ":GENERAL_CODE",
                    Type = OracleDbType.Varchar2,
                    Object = val.GENERAL_CODE,
                    Direct = ParameterDirection.Input
                });
            }

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

            // 状態（Open/Close）
            if (val != null && val.OPEN_CLOSE_FLAG != null)
            {
                if (val.OPEN_CLOSE_FLAG == 0)
                {
                    sql.AppendLine("    AND WORK_SCHEDULE_ITEM.CLOSED_DATE IS NULL");
                }
                else if (val.OPEN_CLOSE_FLAG == 1)
                {
                    sql.AppendLine("    AND WORK_SCHEDULE_ITEM.CLOSED_DATE IS NOT NULL");
                }

                if (val.OPEN_CLOSE_FLAG == 1 || val.OPEN_CLOSE_FLAG == 2)
                {
                    // Close 日（開始）
                    if (val != null && val.CLOSED_DATE_START != null)
                    {
                        sql.AppendLine("    AND (WORK_SCHEDULE_ITEM.CLOSED_DATE >= :CLOSED_DATE_START");
                        if (val.OPEN_CLOSE_FLAG == 2)
                        {
                            sql.AppendLine("    OR WORK_SCHEDULE_ITEM.CLOSED_DATE IS NULL");
                        }
                        sql.AppendLine("    )");

                        prms.Add(new BindModel
                        {
                            Name = ":CLOSED_DATE_START",
                            Type = OracleDbType.Date,
                            Object = getDateTime(val.CLOSED_DATE_START),
                            Direct = ParameterDirection.Input
                        });
                    }

                    // Close 日（終了）
                    if (val != null && val.CLOSED_DATE_END != null)
                    {
                        sql.AppendLine("    AND (WORK_SCHEDULE_ITEM.CLOSED_DATE <= :CLOSED_DATE_END");
                        if (val.OPEN_CLOSE_FLAG == 2)
                        {
                            sql.AppendLine("    OR WORK_SCHEDULE_ITEM.CLOSED_DATE IS NULL");
                        }
                        sql.AppendLine("    )");

                        prms.Add(new BindModel
                        {
                            Name = ":CLOSED_DATE_END",
                            Type = OracleDbType.Date,
                            Object = getDateTime(val.CLOSED_DATE_END),
                            Direct = ParameterDirection.Input
                        });
                    }
                }
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

            // 行番号
            if (val != null && val.PARALLEL_INDEX_GROUP > 0)
            {
                sql.AppendLine("    AND WORK_SCHEDULE.PARALLEL_INDEX_GROUP = :PARALLEL_INDEX_GROUP");

                prms.Add(new BindModel
                {
                    Name = ":PARALLEL_INDEX_GROUP",
                    Type = OracleDbType.Int32,
                    Object = val.PARALLEL_INDEX_GROUP,
                    Direct = ParameterDirection.Input
                });
            }

            // スケジュールID
            if (val != null && val.SCHEDULE_ID > 0)
            {
                sql.AppendLine("    AND WORK_SCHEDULE.ID = :SCHEDULE_ID");

                prms.Add(new BindModel
                {
                    Name = ":SCHEDULE_ID",
                    Type = OracleDbType.Int64,
                    Object = val.SCHEDULE_ID,
                    Direct = ParameterDirection.Input
                });
            }

            // カテゴリーID
            if (val != null && val.CATEGORY_ID > 0)
            {
                sql.AppendLine("    AND WORK_SCHEDULE.CATEGORY_ID = :CATEGORY_ID");

                prms.Add(new BindModel
                {
                    Name = ":CATEGORY_ID",
                    Type = OracleDbType.Int64,
                    Object = val.CATEGORY_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     WORK_SCHEDULE.CATEGORY_ID ASC");
            sql.AppendLine("    ,WORK_SCHEDULE.ID ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 業務スケジュールの作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(WorkSchedulePostInModel val)
        {
            var common = new CommonLogic();
            common.SetDBAccess(base.db);

            var scheduleid = common.GetScheduleNewID();

            var item = common.GetScheduleItemRow(val.CATEGORY_ID, 0);

            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("WORK_SCHEDULE(");
            sql.AppendLine("     GENERAL_CODE");
            sql.AppendLine("    ,CATEGORY");
            sql.AppendLine("    ,START_DATE");
            sql.AppendLine("    ,END_DATE");
            sql.AppendLine("    ,SORT_NO");
            sql.AppendLine("    ,PARALLEL_INDEX_GROUP");
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
            sql.AppendLine("    ,CATEGORY_ID");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     :GENERAL_CODE");
            sql.AppendLine("    ,:CATEGORY");
            sql.AppendLine("    ,:START_DATE");
            sql.AppendLine("    ,:END_DATE");
            sql.AppendLine("    ,(SELECT NVL(MAX(SORT_NO), 0) + 1 FROM WORK_SCHEDULE WHERE CATEGORY_ID = :CATEGORY_ID)");
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
            sql.AppendLine("    ,:CATEGORY_ID");
            sql.AppendLine(")");

            // 開発符号：必須
            prms.Add(new BindModel
            {
                Name = ":GENERAL_CODE",
                Type = OracleDbType.Varchar2,
                Object = item["GENERAL_CODE"],
                Direct = ParameterDirection.Input
            });

            // カテゴリーID：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            // カテゴリー：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY",
                Type = OracleDbType.Varchar2,
                Object = item["CATEGORY"],
                Direct = ParameterDirection.Input
            });

            // 作業完了：必須
            var index = val.END_FLAG == 1 ? (int?)100 : null;
            prms.Add(new BindModel
            {
                Name = ":ACHIEVEMENT_INDEX",
                Type = OracleDbType.Int16,
                Object = index,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":ENFORCEMENT_INDEX",
                Type = OracleDbType.Int16,
                Object = index,
                Direct = ParameterDirection.Input
            });

            // 期間（開始）：必須
            prms.Add(new BindModel
            {
                Name = ":START_DATE",
                Type = OracleDbType.Date,
                Object = getDateTime(val.START_DATE),
                Direct = ParameterDirection.Input
            });

            // 期間（終了）：必須
            prms.Add(new BindModel
            {
                Name = ":END_DATE",
                Type = OracleDbType.Date,
                Object = getDateTime(val.END_DATE),
                Direct = ParameterDirection.Input
            });

            // 行番号：必須
            prms.Add(new BindModel
            {
                Name = ":PARALLEL_INDEX_GROUP",
                Type = OracleDbType.Int32,
                Object = val.PARALLEL_INDEX_GROUP,
                Direct = ParameterDirection.Input
            });

            // スケジュールID：必須
            prms.Add(new BindModel
            {
                Name = ":SCHEDULE_ID",
                Type = OracleDbType.Int64,
                Object = scheduleid,
                Direct = ParameterDirection.Input
            });

            // スケジュール区分：必須
            prms.Add(new BindModel
            {
                Name = ":SYMBOL",
                Type = OracleDbType.Int16,
                Object = val.SYMBOL,
                Direct = ParameterDirection.Input
            });

            // 説明：必須
            prms.Add(new BindModel
            {
                Name = ":DESCRIPTION",
                Type = OracleDbType.Varchar2,
                Object = val.DESCRIPTION,
                Direct = ParameterDirection.Input
            });

            // 所属グループID：必須
            prms.Add(new BindModel
            {
                Name = ":SECTION_GROUP_ID",
                Type = OracleDbType.Varchar2,
                Object = item["SECTION_GROUP_ID"],
                Direct = ParameterDirection.Input
            });

            // パーソナルID：必須
            prms.Add(new BindModel
            {
                Name = ":INPUT_PERSONEL_ID",
                Type = OracleDbType.Varchar2,
                Object = val.PERSONEL_ID,
                Direct = ParameterDirection.Input
            });

            // パーソナルID：必須
            prms.Add(new BindModel
            {
                Name = ":INPUT_LOGIN_ID",
                Type = OracleDbType.Varchar2,
                Object = val.PERSONEL_ID,
                Direct = ParameterDirection.Input
            });

            // カテゴリーID：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            return db.InsertData(sql.ToString(), prms);
        }

        /// <summary>
        /// 業務スケジュールの更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(WorkSchedulePutInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    WORK_SCHEDULE");
            sql.AppendLine("SET");
            sql.AppendLine("     START_DATE = :START_DATE");
            sql.AppendLine("    ,END_DATE = :END_DATE");
            sql.AppendLine("    ,SYMBOL = :SYMBOL");
            sql.AppendLine("    ,DESCRIPTION = :DESCRIPTION");
            sql.AppendLine("    ,PARALLEL_INDEX_GROUP = :PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,ACHIEVEMENT_INDEX = :ACHIEVEMENT_INDEX");
            sql.AppendLine("    ,ENFORCEMENT_INDEX = :ENFORCEMENT_INDEX");
            sql.AppendLine("    ,CHANGE_DATETIME = SYSDATE");
            sql.AppendLine("    ,INPUT_PERSONEL_ID = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,INPUT_LOGIN_ID = :INPUT_LOGIN_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            // スケジュールID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int64,
                Object = val.SCHEDULE_ID,
                Direct = ParameterDirection.Input
            });

            // 行番号：必須
            prms.Add(new BindModel
            {
                Name = ":PARALLEL_INDEX_GROUP",
                Type = OracleDbType.Int32,
                Object = val.PARALLEL_INDEX_GROUP,
                Direct = ParameterDirection.Input
            });

            // 作業完了：必須
            var index = val.END_FLAG == 1 ? (int?)100 : null;
            prms.Add(new BindModel
            {
                Name = ":ACHIEVEMENT_INDEX",
                Type = OracleDbType.Int16,
                Object = index,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":ENFORCEMENT_INDEX",
                Type = OracleDbType.Int16,
                Object = index,
                Direct = ParameterDirection.Input
            });

            // 期間（開始）：必須
            prms.Add(new BindModel
            {
                Name = ":START_DATE",
                Type = OracleDbType.Date,
                Object = getDateTime(val.START_DATE),
                Direct = ParameterDirection.Input
            });

            // 期間（終了）：必須
            prms.Add(new BindModel
            {
                Name = ":END_DATE",
                Type = OracleDbType.Date,
                Object = getDateTime(val.END_DATE),
                Direct = ParameterDirection.Input
            });

            // スケジュール区分：必須
            prms.Add(new BindModel
            {
                Name = ":SYMBOL",
                Type = OracleDbType.Int16,
                Object = val.SYMBOL,
                Direct = ParameterDirection.Input
            });

            // 説明：必須
            prms.Add(new BindModel
            {
                Name = ":DESCRIPTION",
                Type = OracleDbType.Varchar2,
                Object = val.DESCRIPTION,
                Direct = ParameterDirection.Input
            });

            // パーソナルID：必須
            prms.Add(new BindModel
            {
                Name = ":INPUT_PERSONEL_ID",
                Type = OracleDbType.Varchar2,
                Object = val.PERSONEL_ID,
                Direct = ParameterDirection.Input
            });

            // パーソナルID：必須
            prms.Add(new BindModel
            {
                Name = ":INPUT_LOGIN_ID",
                Type = OracleDbType.Varchar2,
                Object = val.PERSONEL_ID,
                Direct = ParameterDirection.Input
            });

            return db.UpdateData(sql.ToString(), prms);
        }

        /// <summary>
        /// 業務スケジュールの削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(WorkScheduleDeleteInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    WORK_SCHEDULE");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int64,
                Object = val.SCHEDULE_ID,
                Direct = ParameterDirection.Input
            });

            return db.DeleteData(sql.ToString(), prms);
        }
    }
}