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
    /// 業務（月次計画）ロジッククラス
    /// </summary>
    /// <remarks>業務（月次計画）スケジュール項目の操作</remarks>
    public class MonthlyWorkScheduleItemLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 業務（月次計画）スケジュール項目の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(MonthlyWorkScheduleItemGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    MWSI.CATEGORY_ID");
            sql.AppendLine("    ,WSI.CATEGORY_ID AS DEV_CATEGORY_ID");
            sql.AppendLine("    ,MWSI.CATEGORY");
            sql.AppendLine("    ,MWSI.GENERAL_CODE");
            sql.AppendLine("    ,MWSI.SECTION_GROUP_ID");
            sql.AppendLine("    ,SGD.SECTION_GROUP_CODE");
            sql.AppendLine("    ,MWSI.対象月");
            sql.AppendLine("    ,MWSI.担当者");
            sql.AppendLine("    ,MWSI.備考");
            sql.AppendLine("    ,NVL(MWSI.FLAG_月報専用項目, 0) FLAG_月報専用項目");
            sql.AppendLine("    ,NVL(MWSI.FLAG_月頭月末, 1) FLAG_月頭月末");
            sql.AppendLine("    ,MWSI.SORT_NO");
            sql.AppendLine("    ,NVL(MWSI.PARALLEL_INDEX, 1) PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,MWSI.CLOSED_DATE");
            sql.AppendLine("    ,MWSI.DEVELOPMENT_SCHEDULE_ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    MONTHLY_WORK_SCHEDULE_ITEM MWSI");
            sql.AppendLine("    INNER JOIN SECTION_GROUP_DATA SGD");
            sql.AppendLine("        ON MWSI.SECTION_GROUP_ID = SGD.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN WORK_SCHEDULE_ITEM WSI");
            sql.AppendLine("        ON MWSI.DEVELOPMENT_SCHEDULE_ID = WSI.ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    MWSI.FLAG_CLASS = '試験計画'");
            sql.AppendLine("    AND MWSI.CATEGORY_ID IS NOT NULL");

            // 開発符号
            if (val != null && !string.IsNullOrWhiteSpace(val.GENERAL_CODE))
            {
                sql.AppendLine("    AND MWSI.GENERAL_CODE = :GENERAL_CODE");

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
                sql.AppendLine("    AND MWSI.対象月 = :対象月");

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
            
            // 月頭月末
            if (val != null && val.FLAG_月頭月末 > 0)
            {
                sql.AppendLine("    AND MWSI.FLAG_月頭月末 = :FLAG_月頭月末");

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

            // スケジュールID
            if (val != null && val.SCHEDULE_ID > 0)
            {
                sql.AppendLine("    AND MWSI.ID = :SCHEDULE_ID");

                prms.Add(new BindModel
                {
                    Name = ":SCHEDULE_ID",
                    Type = OracleDbType.Int64,
                    Object = val.SCHEDULE_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    MWSI.SECTION_GROUP_ID ASC");
            sql.AppendLine("    ,MWSI.SORT_NO ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 業務（月次計画）スケジュール項目の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(MonthlyWorkScheduleItemPostInModel val)
        {
            var common = new CommonLogic();
            common.SetDBAccess(base.db);

            // シーケンス採番
            var scheduleid = common.GetScheduleNewID(1);

            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

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
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     :GENERAL_CODE");
            sql.AppendLine("    ,:CATEGORY");
            if (val != null && val.SORT_NO > 0)
            {
                sql.AppendLine("    ,:SORT_NO");
            }
            else
            {
                sql.AppendLine("    ,(SELECT NVL(MAX(SORT_NO), 0) + 1 FROM MONTHLY_WORK_SCHEDULE_ITEM WHERE 対象月 = :対象月 AND FLAG_月頭月末 = :FLAG_月頭月末)");
            }
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
            sql.AppendLine("    ,1");
            sql.AppendLine("    ,:FLAG_月頭月末");
            sql.AppendLine("    ,:CATEGORY_ID");
            sql.AppendLine(")");

            // 開発符号
            prms.Add(new BindModel
            {
                Name = ":GENERAL_CODE",
                Type = OracleDbType.Varchar2,
                Object = !string.IsNullOrWhiteSpace(val.GENERAL_CODE) ? val.GENERAL_CODE : null,
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
                // 対象月：必須
                prms.Add(new BindModel
                {
                    Name = ":対象月",
                    Type = OracleDbType.Date,
                    Object = val.対象月,
                    Direct = ParameterDirection.Input
                });

                // FLAG_月頭月末：必須
                prms.Add(new BindModel
                {
                    Name = ":FLAG_月頭月末",
                    Type = OracleDbType.Int16,
                    Object = val.FLAG_月頭月末,
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

            // 対象月：必須
            prms.Add(new BindModel
            {
                Name = ":対象月",
                Type = OracleDbType.Date,
                Object = val.対象月,
                Direct = ParameterDirection.Input
            });

            // 担当者
            prms.Add(new BindModel
            {
                Name = ":担当者",
                Type = OracleDbType.Varchar2,
                Object = !string.IsNullOrWhiteSpace(val.担当者) ? val.担当者 : null,
                Direct = ParameterDirection.Input
            });

            // 備考
            prms.Add(new BindModel
            {
                Name = ":備考",
                Type = OracleDbType.Varchar2,
                Object = !string.IsNullOrWhiteSpace(val.備考) ? val.備考 : null,
                Direct = ParameterDirection.Input
            });

            // FLAG_月頭月末：必須
            prms.Add(new BindModel
            {
                Name = ":FLAG_月頭月末",
                Type = OracleDbType.Int16,
                Object = val.FLAG_月頭月末,
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

            return common.UpdateScheduleItemSortNoByMonthly(5, val.対象月, val.FLAG_月頭月末, val.SECTION_GROUP_ID);
        }

        /// <summary>
        /// 業務（月次計画）スケジュール項目の更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(MonthlyWorkScheduleItemPutInModel val)
        {
            var common = new CommonLogic();
            common.SetDBAccess(base.db);

            var item = common.GetScheduleItemRow(val.CATEGORY_ID, 5);

            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    MONTHLY_WORK_SCHEDULE_ITEM");
            sql.AppendLine("SET");
            sql.AppendLine("     CATEGORY = :CATEGORY");
            sql.AppendLine("    ,SECTION_GROUP_ID = :SECTION_GROUP_ID");
            sql.AppendLine("    ,SORT_NO = :SORT_NO");
            sql.AppendLine("    ,PARALLEL_INDEX = :PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,担当者 = :担当者");
            sql.AppendLine("    ,備考 = :備考");
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

            // 担当者
            prms.Add(new BindModel
            {
                Name = ":担当者",
                Type = OracleDbType.Varchar2,
                Object = !string.IsNullOrWhiteSpace(val.担当者) ? val.担当者 : null,
                Direct = ParameterDirection.Input
            });

            // 備考
            prms.Add(new BindModel
            {
                Name = ":備考",
                Type = OracleDbType.Varchar2,
                Object = !string.IsNullOrWhiteSpace(val.備考) ? val.備考 : null,
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
                if (!common.UpdateScheduleItemSortNoByMonthly(5, (DateTime?)item["対象月"], (int?)item["FLAG_月頭月末"], (string)item["SECTION_GROUP_ID"])) return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    MONTHLY_WORK_SCHEDULE");
            sql.AppendLine("SET");
            sql.AppendLine("     CATEGORY = :CATEGORY");
            sql.AppendLine("    ,SECTION_GROUP_ID = :SECTION_GROUP_ID");
            sql.AppendLine("    ,担当者 = :担当者");
            sql.AppendLine("    ,備考 = :備考");
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

            // 担当者
            prms.Add(new BindModel
            {
                Name = ":担当者",
                Type = OracleDbType.Varchar2,
                Object = !string.IsNullOrWhiteSpace(val.担当者) ? val.担当者 : null,
                Direct = ParameterDirection.Input
            });

            // 備考
            prms.Add(new BindModel
            {
                Name = ":備考",
                Type = OracleDbType.Varchar2,
                Object = !string.IsNullOrWhiteSpace(val.備考) ? val.備考 : null,
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
        /// 業務（月次計画）スケジュール項目の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(MonthlyWorkScheduleItemDeleteInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    MONTHLY_WORK_SCHEDULE_ITEM");
            sql.AppendLine("WHERE");
            sql.AppendLine("    MONTHLY_WORK_SCHEDULE_ITEM.ID = :ID");

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
            sql.AppendLine("    MONTHLY_WORK_SCHEDULE");
            sql.AppendLine("WHERE");
            sql.AppendLine("    MONTHLY_WORK_SCHEDULE.CATEGORY_ID = :CATEGORY_ID");

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