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
    /// 業務（月次計画）ロジッククラス
    /// </summary>
    /// <remarks>業務（月次計画）スケジュールの操作</remarks>
    public class MonthlyWorkScheduleLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 業務（月次計画）スケジュールの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(MonthlyWorkScheduleGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    MWS.CATEGORY_ID");
            sql.AppendLine("    ,WS.CATEGORY_ID DEV_CATEGORY_ID");
            sql.AppendLine("    ,MWS.CATEGORY");
            sql.AppendLine("    ,NVL(MWS.PARALLEL_INDEX, 1) PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,MWS.START_DATE");
            sql.AppendLine("    ,MWS.END_DATE");
            sql.AppendLine("    ,MWS.ID");
            sql.AppendLine("    ,MWS.DESCRIPTION");
            sql.AppendLine("    ,MWS.SYMBOL");
            sql.AppendLine("    ,MWS.ACHIEVEMENT_INDEX");
            sql.AppendLine("    ,MWS.ENFORCEMENT_INDEX");
            sql.AppendLine("    ,MWS.INPUT_DATETIME");
            sql.AppendLine("    ,MWSI.CLOSED_DATE");
            sql.AppendLine("    ,CASE WHEN MWS.CLOSED_DATE IS NULL THEN 'open'");
            sql.AppendLine("     ELSE 'close' END AS OPEN_CLOSE");
            sql.AppendLine("    ,MWS.SORT_NO");
            sql.AppendLine("    ,NVL(MWS.FLAG_月報専用項目, 0) FLAG_月報専用項目");
            sql.AppendLine("    ,NVL(MWS.FLAG_月頭月末, 1) FLAG_月頭月末");
            sql.AppendLine("    ,MWS.DEVELOPMENT_SCHEDULE_ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    MONTHLY_WORK_SCHEDULE MWS");
            sql.AppendLine("    LEFT JOIN MONTHLY_WORK_SCHEDULE_ITEM MWSI");
            sql.AppendLine("        ON MWS.CATEGORY_ID = MWSI.ID");
            sql.AppendLine("    INNER JOIN SECTION_GROUP_DATA SGD");
            sql.AppendLine("        ON MWS.SECTION_GROUP_ID = SGD.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN WORK_SCHEDULE WS");
            sql.AppendLine("        ON MWS.DEVELOPMENT_SCHEDULE_ID = WS.ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    MWS.CATEGORY_ID IS NOT NULL");
            sql.AppendLine("    AND MWS.ID IS NOT NULL");
            sql.AppendLine("    AND MWS.FLAG_CLASS = '試験計画'");

            // 開発符号
            if (val != null && !string.IsNullOrWhiteSpace(val.GENERAL_CODE))
            {
                sql.AppendLine("    AND MWS.GENERAL_CODE = :GENERAL_CODE");

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
                sql.AppendLine("    AND SGD.SECTION_ID = :SECTION_ID");

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
                sql.AppendLine("    AND MWSI.SECTION_GROUP_ID = :SECTION_GROUP_ID");

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
                sql.AppendLine("    AND MWS.対象月 = :対象月");

                prms.Add(new BindModel
                {
                    Name = ":対象月",
                    Type = OracleDbType.Date,
                    Object = getDateTime(val.対象月),
                    Direct = ParameterDirection.Input
                });
            }

            // 状態（Open/Close）
            if (val != null && val.OPEN_CLOSE_FLAG != null)
            {
                if (val.OPEN_CLOSE_FLAG == 0)
                {
                    sql.AppendLine("    AND MWSI.CLOSED_DATE IS NULL");
                }
                else if (val.OPEN_CLOSE_FLAG == 1)
                {
                    sql.AppendLine("    AND MWSI.CLOSED_DATE IS NOT NULL");
                }
            }

            // 期間（開始・終了）
            if (val != null && (val.DATE_START != null || val.DATE_END != null))
            {
                sql.AppendLine("    AND (");
                sql.AppendLine("    (");

                if (val.DATE_START != null)
                {
                    sql.AppendLine("    MWS.START_DATE >= :DATE_START");

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

                    sql.AppendLine("    MWS.START_DATE <= :DATE_END");

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
                    sql.AppendLine("    MWS.END_DATE >= :DATE_START");

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

                    sql.AppendLine("    MWS.END_DATE <= :DATE_END");

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

            // 月頭月末
            if (val != null && val.FLAG_月頭月末 > 0)
            {
                sql.AppendLine("    AND MWS.FLAG_月頭月末 = :FLAG_月頭月末");

                prms.Add(new BindModel
                {
                    Name = ":FLAG_月頭月末",
                    Type = OracleDbType.Int32,
                    Object = val.FLAG_月頭月末,
                    Direct = ParameterDirection.Input
                });
            }

            // 担当者
            if (val != null && !string.IsNullOrWhiteSpace(val.担当者))
            {
                sql.AppendLine("    AND MWSI.担当者 LIKE :担当者");

                prms.Add(new BindModel
                {
                    Name = ":担当者",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.担当者 + "%",
                    Direct = ParameterDirection.Input
                });
            }

            // 行番号
            if (val != null && val.PARALLEL_INDEX_GROUP > 0)
            {
                sql.AppendLine("    AND MWS.PARALLEL_INDEX = :PARALLEL_INDEX_GROUP");

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
                sql.AppendLine("    AND MWS.ID = :SCHEDULE_ID");

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
                sql.AppendLine("    AND MWS.CATEGORY_ID = :CATEGORY_ID");

                prms.Add(new BindModel
                {
                    Name = ":CATEGORY_ID",
                    Type = OracleDbType.Int64,
                    Object = val.CATEGORY_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    MWS.CATEGORY_ID ASC");
            sql.AppendLine("    ,MWS.ID ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 業務（月次計画）スケジュールの作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(MonthlyWorkSchedulePostInModel val)
        {
            var common = new CommonLogic();
            common.SetDBAccess(base.db);

            var scheduleid = common.GetScheduleNewID(1);

            var item = common.GetScheduleItemRow(val.CATEGORY_ID, 5);

            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

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
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     :GENERAL_CODE");
            sql.AppendLine("    ,:CATEGORY");
            sql.AppendLine("    ,:START_DATE");
            sql.AppendLine("    ,:END_DATE");
            sql.AppendLine("    ,(SELECT NVL(MAX(SORT_NO), 0) + 1 FROM MONTHLY_WORK_SCHEDULE WHERE CATEGORY_ID = :CATEGORY_ID)");
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
            sql.AppendLine("    ,1");
            sql.AppendLine("    ,:FLAG_月頭月末");

            sql.AppendLine("    ,:CATEGORY_ID");
            sql.AppendLine(")");

            // 開発符号
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

            // 対象月：必須
            prms.Add(new BindModel
            {
                Name = ":対象月",
                Type = OracleDbType.Date,
                Object = item["対象月"],
                Direct = ParameterDirection.Input
            });

            // 担当者
            prms.Add(new BindModel
            {
                Name = ":担当者",
                Type = OracleDbType.Varchar2,
                Object = item["担当者"],
                Direct = ParameterDirection.Input
            });

            // 備考
            prms.Add(new BindModel
            {
                Name = ":備考",
                Type = OracleDbType.Varchar2,
                Object = item["備考"],
                Direct = ParameterDirection.Input
            });

            // FLAG_月頭月末：必須
            prms.Add(new BindModel
            {
                Name = ":FLAG_月頭月末",
                Type = OracleDbType.Int16,
                Object = item["FLAG_月頭月末"],
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
        /// 業務（月次計画）スケジュールの更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(MonthlyWorkSchedulePutInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    MONTHLY_WORK_SCHEDULE");
            sql.AppendLine("SET");
            sql.AppendLine("     START_DATE = :START_DATE");
            sql.AppendLine("    ,END_DATE = :END_DATE");
            sql.AppendLine("    ,SYMBOL = :SYMBOL");
            sql.AppendLine("    ,DESCRIPTION = :DESCRIPTION");
            sql.AppendLine("    ,PARALLEL_INDEX = :PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,ACHIEVEMENT_INDEX = :ACHIEVEMENT_INDEX");
            sql.AppendLine("    ,ENFORCEMENT_INDEX = :ENFORCEMENT_INDEX");
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
        /// 業務（月次計画）スケジュールの削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(MonthlyWorkScheduleDeleteInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    MONTHLY_WORK_SCHEDULE");
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