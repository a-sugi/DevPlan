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
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>業務スケジュール項目の操作</remarks>
    public class WorkScheduleItemLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 業務スケジュール項目の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(WorkScheduleItemGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     WORK_SCHEDULE_ITEM.GENERAL_CODE");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.CATEGORY");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.SORT_NO");
            sql.AppendLine("    ,NVL(WORK_SCHEDULE_ITEM.PARALLEL_INDEX_GROUP, 1) PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.ID");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.SECTION_GROUP_ID");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.FLAG_CLASS");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.CATEGORY_ID");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.CLOSED_DATE");
            sql.AppendLine("    ,CASE WHEN WORK_SCHEDULE_ITEM.CLOSED_DATE IS NULL THEN 'open'");
            sql.AppendLine("     ELSE 'close' END AS OPEN_CLOSE");
            sql.AppendLine("    ,SECTION_GROUP_DATA.SECTION_ID");
            sql.AppendLine("    ,SECTION_GROUP_DATA.SECTION_GROUP_ID");
            sql.AppendLine("    ,SECTION_GROUP_DATA.SECTION_GROUP_CODE");
            sql.AppendLine("FROM");
            sql.AppendLine("    WORK_SCHEDULE_ITEM");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA");
            sql.AppendLine("    ON WORK_SCHEDULE_ITEM.SECTION_GROUP_ID = SECTION_GROUP_DATA.SECTION_GROUP_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    WORK_SCHEDULE_ITEM.CATEGORY_ID IS NOT NULL");

            // 開発符号
            if (val != null && !string.IsNullOrWhiteSpace(val.GENERAL_CODE))
            {
                sql.AppendLine("    AND WORK_SCHEDULE_ITEM.GENERAL_CODE = :GENERAL_CODE");

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

            // スケジュールID
            if (val != null && val.SCHEDULE_ID > 0)
            {
                sql.AppendLine("    AND WORK_SCHEDULE_ITEM.ID = :SCHEDULE_ID");

                prms.Add(new BindModel
                {
                    Name = ":SCHEDULE_ID",
                    Type = OracleDbType.Int64,
                    Object = val.SCHEDULE_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    WORK_SCHEDULE_ITEM.SECTION_GROUP_ID ASC");
            sql.AppendLine("    ,WORK_SCHEDULE_ITEM.SORT_NO ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 業務スケジュール項目の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(WorkScheduleItemPostInModel val)
        {
            var common = new CommonLogic();
            common.SetDBAccess(base.db);

            // シーケンス採番
            var scheduleid = common.GetScheduleNewID();

            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("WORK_SCHEDULE_ITEM(");
            sql.AppendLine("     GENERAL_CODE");
            sql.AppendLine("    ,CATEGORY");
            sql.AppendLine("    ,SORT_NO");
            sql.AppendLine("    ,PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,ID");
            sql.AppendLine("    ,SECTION_GROUP_ID");
            sql.AppendLine("    ,FLAG_SEPARATOR");
            sql.AppendLine("    ,FLAG_CLASS");
            sql.AppendLine("    ,INPUT_DATETIME");
            sql.AppendLine("    ,INPUT_PERSONEL_ID");
            sql.AppendLine("    ,INPUT_LOGIN_ID");
            sql.AppendLine("    ,CATEGORY_ID");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     :GENERAL_CODE");
            sql.AppendLine("    ,:CATEGORY");
            if (val != null && val.SORT_NO > 0)
            {
                sql.AppendLine("    ,:SORT_NO");
            }
            else
            {
                sql.AppendLine("    ,(SELECT NVL(MAX(SORT_NO), 0) + 1 FROM WORK_SCHEDULE_ITEM WHERE GENERAL_CODE = :GENERAL_CODE)");
            }
            sql.AppendLine("    ,:PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,:SCHEDULE_ID");
            sql.AppendLine("    ,:SECTION_GROUP_ID");
            sql.AppendLine("    ,1");
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
                Object = val.GENERAL_CODE,
                Direct = ParameterDirection.Input
            });

            // カテゴリー：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY",
                Type = OracleDbType.Varchar2,
                Object = val.CATEGORY,
                Direct = ParameterDirection.Input
            });

            if (val != null && val.SORT_NO > 0)
            {
                // 並び順
                prms.Add(new BindModel
                {
                    Name = ":SORT_NO",
                    Type = OracleDbType.Decimal,
                    Object = val.SORT_NO,
                    Direct = ParameterDirection.Input
                });
            }
            else
            {
                // 開発符号：必須
                prms.Add(new BindModel
                {
                    Name = ":GENERAL_CODE",
                    Type = OracleDbType.Varchar2,
                    Object = val.GENERAL_CODE,
                    Direct = ParameterDirection.Input
                });
            }

            // 行数：必須
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
            
            // 所属グループID：必須
            prms.Add(new BindModel
            {
                Name = ":SECTION_GROUP_ID",
                Type = OracleDbType.Varchar2,
                Object = val.SECTION_GROUP_ID,
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
                Object = scheduleid,
                Direct = ParameterDirection.Input
            });

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            return common.UpdateScheduleItemSortNoByGeneralCode(0, val.GENERAL_CODE);
        }

        /// <summary>
        /// 業務スケジュール項目の更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(WorkScheduleItemPutInModel val)
        {
            var common = new CommonLogic();
            common.SetDBAccess(base.db);

            var item = common.GetScheduleItemRow(val.CATEGORY_ID, 0);

            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    WORK_SCHEDULE_ITEM");
            sql.AppendLine("SET");
            sql.AppendLine("     CATEGORY = :CATEGORY");
            sql.AppendLine("    ,SECTION_GROUP_ID = :SECTION_GROUP_ID");
            sql.AppendLine("    ,SORT_NO = :SORT_NO");
            sql.AppendLine("    ,PARALLEL_INDEX_GROUP = :PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,CHANGE_DATETIME = SYSDATE");
            sql.AppendLine("    ,INPUT_PERSONEL_ID = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,INPUT_LOGIN_ID = :INPUT_LOGIN_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            // カテゴリーID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            // カテゴリー：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY",
                Type = OracleDbType.Varchar2,
                Object = val.CATEGORY,
                Direct = ParameterDirection.Input
            });

            // 所属グループID：必須
            prms.Add(new BindModel
            {
                Name = ":SECTION_GROUP_ID",
                Type = OracleDbType.Varchar2,
                Object = val.SECTION_GROUP_ID,
                Direct = ParameterDirection.Input
            });

            // 並び順：必須
            prms.Add(new BindModel
            {
                Name = ":SORT_NO",
                Type = OracleDbType.Decimal,
                Object = val.SORT_NO,
                Direct = ParameterDirection.Input
            });

            // 行数：必須
            prms.Add(new BindModel
            {
                Name = ":PARALLEL_INDEX_GROUP",
                Type = OracleDbType.Int32,
                Object = val.PARALLEL_INDEX_GROUP,
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

            if (!db.UpdateData(sql.ToString(), prms))
            {
                return false;
            }

            if ((Convert.ToDecimal(val.SORT_NO) - System.Math.Floor(Convert.ToDecimal(val.SORT_NO)) != 0))
            {
                if (!common.UpdateScheduleItemSortNoByGeneralCode(0, item["GENERAL_CODE"].ToString())) return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    WORK_SCHEDULE");
            sql.AppendLine("SET");
            sql.AppendLine("     CATEGORY = :CATEGORY");
            sql.AppendLine("    ,SECTION_GROUP_ID = :SECTION_GROUP_ID");
            sql.AppendLine("    ,CHANGE_DATETIME = SYSDATE");
            sql.AppendLine("    ,INPUT_PERSONEL_ID = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,INPUT_LOGIN_ID = :INPUT_LOGIN_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    CATEGORY_ID = :CATEGORY_ID");

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
                Object = val.CATEGORY,
                Direct = ParameterDirection.Input
            });

            // 所属グループID：必須
            prms.Add(new BindModel
            {
                Name = ":SECTION_GROUP_ID",
                Type = OracleDbType.Varchar2,
                Object = val.SECTION_GROUP_ID,
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
        /// 業務スケジュール項目の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(WorkScheduleItemDeleteInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    WORK_SCHEDULE_ITEM");
            sql.AppendLine("WHERE");
            sql.AppendLine("    WORK_SCHEDULE_ITEM.ID = :ID");

            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            if (!db.DeleteData(sql.ToString(), prms))
            { 
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    WORK_SCHEDULE");
            sql.AppendLine("WHERE");
            sql.AppendLine("    WORK_SCHEDULE.CATEGORY_ID = :CATEGORY_ID");

            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            return db.DeleteData(sql.ToString(), prms);
        }
    }
}