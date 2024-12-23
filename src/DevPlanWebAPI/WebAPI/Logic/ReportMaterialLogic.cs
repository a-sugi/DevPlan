using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>週報データの操作</remarks>
    public class ReportMaterialLogic : BaseLogic
    {
        string[] DevelopmentScheduleViews =
        {
            "WORK_SCHEDULE_ITEM",               // 業務計画表
            "TESTCAR_SCHEDULE_ITEM",            // 試験車日程
            "CARSHARING_SCHEDULE_ITEM",         // カーシェア日程
            "OUTERCAR_SCHEDULE_ITEM",           // 外製車日程
            "PU_CONTROL_DEVELOPMENT_ITEM",      // pu制御開発
            "CAR_DEVELOPMENT_SCHEDULE_ITEM"     // 進捗履歴
        };

        /// <summary>
        /// 情報元検索
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        public DataTable Get(InfoListInModel val)
        {
            switch (val.CLASS_DATA)
            {
                case 1:
                    //週報画面　情報元進捗履歴
                    return GetWeeklyInfoFromHistory(val);
                case 2:
                    //週報画面　情報元週報(部)
                    return GetDepartmentInfoFromWeekly(val);
                case 3:
                    //週報画面　情報元週報(課)
                    return GetSectionInfoFromWeekly(val);
                case 4:
                    //週報画面　情報元週報(担当)(週報画面)
                    return GetSectionGroupInfoFromWeekly(val);
                case 5:
                    //月報画面　情報元月報
                    return GetMonthlyInfoFromMonthly(val);
                case 6:
                    //週報画面　情報元週報(担当)(月報画面)
                    return GetSectionGroupInfoFromWeeklyM(val);
            }

            return null;
        }

        /// <summary>
        /// 情報元進捗履歴の検索
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        private DataTable GetWeeklyInfoFromHistory(InfoListInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            string startSql = "SELECT * FROM ( ";
            sql.Append(startSql);

            //DEVELOPMENT_SCHEDULEのView全てから検索する
            foreach (string view in DevelopmentScheduleViews)
            {
                if(startSql.Length < sql.Length)
                {
                    sql.Append(" UNION ALL ");
                }

                sql.Append("SELECT ");
                sql.Append("試験計画_課題フォローリスト.LISTED_DATE ");
                sql.Append(",SECTION_GROUP_DATA.SECTION_GROUP_CODE ");
                sql.Append("," + view + ".GENERAL_CODE ");
                sql.Append("," + view + ".CATEGORY ");
                sql.Append(",試験計画_課題フォローリスト.CURRENT_SITUATION ");
                sql.Append(",試験計画_課題フォローリスト.FUTURE_SCHEDULE ");

                sql.Append(",試験計画_課題フォローリスト.OPEN_CLOSE ");
                sql.Append("," + view + ".SELECT_KEYWORD ");
                sql.Append(",PL.NAME AS PERSONEL_NAME ");
                sql.Append(",試験計画_課題フォローリスト.INPUT_DATETIME ");

                sql.Append("FROM ");
                sql.Append("SECTION_GROUP_DATA, ");
                sql.Append(view + ", ");
                sql.Append("試験計画_課題フォローリスト ");
                sql.Append("LEFT JOIN PERSONEL_LIST PL ");
                sql.Append("ON 試験計画_課題フォローリスト.INPUT_PERSONEL_ID = PL.PERSONEL_ID ");

                sql.Append(" WHERE ");

                sql.Append(view + ".SECTION_GROUP_ID = SECTION_GROUP_DATA.SECTION_GROUP_ID AND ");

                sql.Append(view + ".SECTION_GROUP_ID = :SECTION_GROUP_ID AND ");
                sql.Append(view + ".ID = 試験計画_課題フォローリスト.CATEGORY_ID AND ");

                sql.Append("試験計画_課題フォローリスト.LISTED_DATE BETWEEN :FIRST_DAY AND :LAST_DAY ");
            }

            sql.Append(" ) ORDER BY LISTED_DATE, SECTION_GROUP_CODE, GENERAL_CODE ASC ");

            prms.Add(new BindModel
            {
                Name = (":SECTION_GROUP_ID"),
                Type = OracleDbType.Varchar2,
                Object = val.SECTION_GROUP_ID,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":FIRST_DAY",
                Type = OracleDbType.Date,
                Object = val.FIRST_DAY,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":LAST_DAY",
                Type = OracleDbType.Date,
                Object = val.LAST_DAY,
                Direct = ParameterDirection.Input
            });

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 情報元週報(部)の検索
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        private DataTable GetDepartmentInfoFromWeekly(InfoListInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //週報データを取得
            sql.Append("SELECT ");
            sql.Append("試験計画_週報.WEEKEND_DATE ");
            sql.Append(",試験計画_週報.SECTION_GROUP_CODE_情報元 AS SECTION_GROUP_CODE ");
            sql.Append(",試験計画_週報.GENERAL_CODE ");
            sql.Append(",試験計画_週報.CATEGORY ");
            sql.Append(",試験計画_週報.CURRENT_SITUATION ");
            sql.Append(",試験計画_週報.FUTURE_SCHEDULE ");
            sql.Append("FROM ");
            sql.Append("試験計画_週報 ");
            sql.Append(",SECTION_GROUP_DATA ");

            sql.Append("WHERE ");
            sql.Append("試験計画_週報.SECTION_GROUP_ID = SECTION_GROUP_DATA.SECTION_GROUP_ID AND ");
            sql.Append("試験計画_週報.作成単位 = :作成単位 AND ");
            prms.Add(new BindModel
            {
                Name = ":作成単位",
                Type = OracleDbType.Varchar2,
                Object = val.作成単位,
                Direct = ParameterDirection.Input
            });
            sql.Append(":FIRST_DAY <= 試験計画_週報.WEEKEND_DATE AND ");
            prms.Add(new BindModel
            {
                Name = ":FIRST_DAY",
                Type = OracleDbType.Date,
                Object = val.FIRST_DAY,
                Direct = ParameterDirection.Input
            });
            sql.Append("試験計画_週報.WEEKEND_DATE < (:LAST_DAY + 1) AND ");
            prms.Add(new BindModel
            {
                Name = ":LAST_DAY",
                Type = OracleDbType.Date,
                Object = val.LAST_DAY,
                Direct = ParameterDirection.Input
            });
            sql.Append("試験計画_週報.DEPARTMENT_ID = :DEPARTMENT_ID ");
            prms.Add(new BindModel
            {
                Name = ":DEPARTMENT_ID",
                Type = OracleDbType.Varchar2,
                Object = val.DEPARTMENT_ID,
                Direct = ParameterDirection.Input
            });

            //並べ替え
            sql.Append("ORDER BY ");
            sql.Append("試験計画_週報.WEEKEND_DATE ");
            sql.Append(",試験計画_週報.SECTION_GROUP_CODE_情報元 ");
            sql.Append(",試験計画_週報.GENERAL_CODE ");
            sql.Append("ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 情報元週報(課)の検索
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        private DataTable GetSectionInfoFromWeekly(InfoListInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //週報データを取得
            sql.Append("SELECT ");
            sql.Append("試験計画_週報.WEEKEND_DATE ");
            sql.Append(",試験計画_週報.SECTION_GROUP_CODE_情報元 AS SECTION_GROUP_CODE ");
            sql.Append(",試験計画_週報.GENERAL_CODE ");
            sql.Append(",試験計画_週報.CATEGORY ");
            sql.Append(",試験計画_週報.CURRENT_SITUATION ");
            sql.Append(",試験計画_週報.FUTURE_SCHEDULE ");
            sql.Append("FROM ");
            sql.Append("試験計画_週報 ");
            sql.Append(",SECTION_GROUP_DATA ");

            sql.Append("WHERE ");
            sql.Append("試験計画_週報.SECTION_GROUP_ID = SECTION_GROUP_DATA.SECTION_GROUP_ID AND ");
            sql.Append("試験計画_週報.作成単位 = :作成単位 AND ");
            prms.Add(new BindModel
            {
                Name = ":作成単位",
                Type = OracleDbType.Varchar2,
                Object = val.作成単位,
                Direct = ParameterDirection.Input
            });
            sql.Append(":FIRST_DAY <= 試験計画_週報.WEEKEND_DATE AND ");
            prms.Add(new BindModel
            {
                Name = ":FIRST_DAY",
                Type = OracleDbType.Date,
                Object = val.FIRST_DAY,
                Direct = ParameterDirection.Input
            });
            sql.Append("試験計画_週報.WEEKEND_DATE < (:LAST_DAY + 1) AND ");
            prms.Add(new BindModel
            {
                Name = ":LAST_DAY",
                Type = OracleDbType.Date,
                Object = val.LAST_DAY,
                Direct = ParameterDirection.Input
            });
            sql.Append("試験計画_週報.DEPARTMENT_ID = :DEPARTMENT_ID AND ");
            prms.Add(new BindModel
            {
                Name = ":DEPARTMENT_ID",
                Type = OracleDbType.Varchar2,
                Object = val.DEPARTMENT_ID,
                Direct = ParameterDirection.Input
            });
            sql.Append("試験計画_週報.SECTION_ID = :SECTION_ID ");
            prms.Add(new BindModel
            {
                Name = ":SECTION_ID",
                Type = OracleDbType.Varchar2,
                Object = val.SECTION_ID,
                Direct = ParameterDirection.Input
            });

            //並べ替え
            sql.Append("ORDER BY ");
            sql.Append("試験計画_週報.WEEKEND_DATE ");
            sql.Append(",試験計画_週報.SECTION_GROUP_CODE_情報元 ");
            sql.Append(",試験計画_週報.GENERAL_CODE ");
            sql.Append("ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 情報元週報(担当)の検索
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        private DataTable GetSectionGroupInfoFromWeekly(InfoListInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //週報データを取得
            sql.Append("SELECT ");
            sql.Append("試験計画_週報.WEEKEND_DATE ");
            sql.Append(",試験計画_週報.SECTION_GROUP_CODE_情報元 AS SECTION_GROUP_CODE ");
            sql.Append(",試験計画_週報.GENERAL_CODE ");
            sql.Append(",試験計画_週報.CATEGORY ");
            sql.Append(",試験計画_週報.CURRENT_SITUATION ");
            sql.Append(",試験計画_週報.FUTURE_SCHEDULE ");
            sql.Append("FROM ");
            sql.Append("試験計画_週報 ");
            sql.Append(",SECTION_GROUP_DATA ");

            sql.Append("WHERE ");
            sql.Append("試験計画_週報.SECTION_GROUP_ID = SECTION_GROUP_DATA.SECTION_GROUP_ID AND ");
            sql.Append("試験計画_週報.作成単位 = :作成単位 AND ");
            prms.Add(new BindModel
            {
                Name = ":作成単位",
                Type = OracleDbType.Varchar2,
                Object = val.作成単位,
                Direct = ParameterDirection.Input
            });
            if (val.DEPARTMENT_ID != null)
            {
                sql.Append("試験計画_週報.DEPARTMENT_ID = :DEPARTMENT_ID AND ");
                prms.Add(new BindModel
                {
                    Name = ":DEPARTMENT_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.DEPARTMENT_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.SECTION_ID != null)
            {
                sql.Append("試験計画_週報.SECTION_ID = :SECTION_ID AND ");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (null != val.SECTION_GROUP_ID)
            {
                sql.Append("試験計画_週報.SECTION_GROUP_ID = :SECTION_GROUP_ID AND ");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_GROUP_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_GROUP_ID,
                    Direct = ParameterDirection.Input
                });
            }
            sql.Append(":FIRST_DAY <= 試験計画_週報.WEEKEND_DATE AND ");
            prms.Add(new BindModel
            {
                Name = ":FIRST_DAY",
                Type = OracleDbType.Date,
                Object = val.FIRST_DAY,
                Direct = ParameterDirection.Input
            });
            sql.Append("試験計画_週報.WEEKEND_DATE < (:LAST_DAY + 1)");
            prms.Add(new BindModel
            {
                Name = ":LAST_DAY",
                Type = OracleDbType.Date,
                Object = val.LAST_DAY,
                Direct = ParameterDirection.Input
            });

            //並べ替え
            sql.Append("ORDER BY ");
            sql.Append("試験計画_週報.WEEKEND_DATE ");
            sql.Append(",試験計画_週報.SECTION_GROUP_CODE_情報元 ");
            sql.Append(",試験計画_週報.GENERAL_CODE ");
            sql.Append("ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 情報元月報(月報画面)の検索
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        private DataTable GetMonthlyInfoFromMonthly(InfoListInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //週報データを取得
            sql.Append("SELECT ");
            sql.Append("試験計画_月次報告.MONTH_FIRST_DAY ");
            sql.Append(",試験計画_月次報告.GENERAL_CODE ");
            sql.Append(",試験計画_月次報告.CURRENT_SITUATION ");
            sql.Append(",試験計画_月次報告.FUTURE_SCHEDULE ");
            sql.Append(",SECTION_DATA.SECTION_CODE ");
            sql.Append("FROM ");
            sql.Append("試験計画_月次報告 ");
            sql.Append(",SECTION_DATA ");

            sql.Append("WHERE ");

            sql.Append("SECTION_DATA.DEPARTMENT_ID = :DEPARTMENT_ID AND ");
            prms.Add(new BindModel
            {
                Name = ":DEPARTMENT_ID",
                Type = OracleDbType.Varchar2,
                Object = val.DEPARTMENT_ID,
                Direct = ParameterDirection.Input
            });

            sql.Append("SECTION_DATA.SECTION_ID = 試験計画_月次報告.SECTION_ID AND ");
            sql.Append(":FIRST_DAY <= 試験計画_月次報告.MONTH_FIRST_DAY AND ");
            prms.Add(new BindModel
            {
                Name = ":FIRST_DAY",
                Type = OracleDbType.Date,
                Object = val.FIRST_DAY,
                Direct = ParameterDirection.Input
            });
            sql.Append("試験計画_月次報告.MONTH_FIRST_DAY < (:LAST_DAY + 1) ");
            prms.Add(new BindModel
            {
                Name = ":LAST_DAY",
                Type = OracleDbType.Date,
                Object = val.LAST_DAY,
                Direct = ParameterDirection.Input
            });

            //並べ替え
            sql.Append("ORDER BY ");
            sql.Append("試験計画_月次報告.MONTH_FIRST_DAY ");
            sql.Append(",SECTION_DATA.SECTION_CODE ");
            sql.Append("ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 情報元週報(担当)(月報画面)の検索
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        private DataTable GetSectionGroupInfoFromWeeklyM(InfoListInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //週報データを取得
            sql.Append("SELECT ");
            sql.Append("試験計画_週報.WEEKEND_DATE ");
            sql.Append(",試験計画_週報.SECTION_GROUP_CODE_情報元 AS SECTION_GROUP_CODE ");
            sql.Append(",試験計画_週報.GENERAL_CODE ");
            sql.Append(",試験計画_週報.CATEGORY ");
            sql.Append(",試験計画_週報.CURRENT_SITUATION ");
            sql.Append(",試験計画_週報.FUTURE_SCHEDULE ");
            sql.Append("FROM ");
            sql.Append("試験計画_週報 ");
            sql.Append(",SECTION_GROUP_DATA ");

            sql.Append("WHERE ");
            sql.Append("試験計画_週報.SECTION_GROUP_ID = SECTION_GROUP_DATA.SECTION_GROUP_ID AND ");
            if (val.DEPARTMENT_ID != null)
            {
                sql.Append("試験計画_週報.DEPARTMENT_ID = :DEPARTMENT_ID AND ");
                prms.Add(new BindModel
                {
                    Name = ":DEPARTMENT_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.DEPARTMENT_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.SECTION_ID != null)
            {
                sql.Append("SECTION_GROUP_DATA.SECTION_ID = :SECTION_ID AND ");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (null != val.SECTION_GROUP_ID)
            {
                sql.Append("試験計画_週報.SECTION_GROUP_ID = :SECTION_GROUP_ID AND ");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_GROUP_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_GROUP_ID,
                    Direct = ParameterDirection.Input
                });
            }
            sql.Append(":FIRST_DAY <= 試験計画_週報.WEEKEND_DATE AND ");
            prms.Add(new BindModel
            {
                Name = ":FIRST_DAY",
                Type = OracleDbType.Date,
                Object = val.FIRST_DAY,
                Direct = ParameterDirection.Input
            });
            sql.Append("試験計画_週報.WEEKEND_DATE <= (:LAST_DAY + 1)");
            prms.Add(new BindModel
            {
                Name = ":LAST_DAY",
                Type = OracleDbType.Date,
                Object = val.LAST_DAY,
                Direct = ParameterDirection.Input
            });

            //並べ替え
            sql.Append("ORDER BY ");
            sql.Append("試験計画_週報.WEEKEND_DATE ");
            sql.Append(",試験計画_週報.SORT_NO ");

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}