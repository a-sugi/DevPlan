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
    /// 設計チェック参加者ロジッククラス
    /// </summary>
    /// <remarks>設計チェック参加者の操作</remarks>
    public class DesignCheckUserLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 設計チェック参加者の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(DesignCheckUserGetInModel val, bool join = true, bool wait = false)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     試験計画_DCHK_参加者.ID");
            sql.AppendLine("    ,試験計画_DCHK_参加者.開催日_ID");
            if (join)
            {
                sql.AppendLine("    ,CASE WHEN 試験計画_DCHK_参加者.PERSONEL_ID IS NULL THEN 試験計画_DCHK_参加者.参加者名 ELSE PERSONEL_LIST.NAME END NAME");
                sql.AppendLine("    ,PERSONEL_LIST.PERSONEL_ID");
                sql.AppendLine("    ,CASE WHEN 試験計画_DCHK_参加者.PERSONEL_ID IS NULL THEN 試験計画_DCHK_参加者.参加時_所属担当_ID ELSE SG1.SECTION_GROUP_ID END SECTION_GROUP_ID");
                sql.AppendLine("    ,CASE WHEN 試験計画_DCHK_参加者.PERSONEL_ID IS NULL THEN SG2.SECTION_GROUP_CODE ELSE SG1.SECTION_GROUP_CODE END SECTION_GROUP_CODE");
                sql.AppendLine("    ,CASE WHEN 試験計画_DCHK_参加者.PERSONEL_ID IS NULL THEN 試験計画_DCHK_参加者.参加時_所属課_ID ELSE SC1.SECTION_ID END SECTION_ID");
                sql.AppendLine("    ,CASE WHEN 試験計画_DCHK_参加者.PERSONEL_ID IS NULL THEN SC2.SECTION_CODE ELSE SC1.SECTION_CODE END SECTION_CODE");
                sql.AppendLine("    ,CASE WHEN 試験計画_DCHK_参加者.PERSONEL_ID IS NULL THEN 試験計画_DCHK_参加者.参加時_所属部_ID ELSE DP1.DEPARTMENT_ID END DEPARTMENT_ID");
                sql.AppendLine("    ,CASE WHEN 試験計画_DCHK_参加者.PERSONEL_ID IS NULL THEN DP2.DEPARTMENT_CODE ELSE DP1.DEPARTMENT_CODE END DEPARTMENT_CODE");
            }
            else
            {
                sql.AppendLine("    ,試験計画_DCHK_参加者.参加者名  NAME");
                sql.AppendLine("    ,試験計画_DCHK_参加者.PERSONEL_ID");
                sql.AppendLine("    ,試験計画_DCHK_参加者.参加時_所属担当_ID SECTION_GROUP_ID");
                sql.AppendLine("    ,試験計画_DCHK_参加者.参加時_所属課_ID SECTION_ID");
                sql.AppendLine("    ,試験計画_DCHK_参加者.参加時_所属部_ID DEPARTMENT_ID");
            }
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_参加者");
            if (join)
            {
                sql.AppendLine("    LEFT JOIN PERSONEL_LIST");
                sql.AppendLine("    ON 試験計画_DCHK_参加者.PERSONEL_ID = PERSONEL_LIST.PERSONEL_ID");
                sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA SG1");
                sql.AppendLine("    ON PERSONEL_LIST.SECTION_GROUP_ID = SG1.SECTION_GROUP_ID");
                sql.AppendLine("    LEFT JOIN SECTION_DATA SC1");
                sql.AppendLine("    ON SG1.SECTION_ID = SC1.SECTION_ID");
                sql.AppendLine("    LEFT JOIN DEPARTMENT_DATA DP1");
                sql.AppendLine("    ON SC1.DEPARTMENT_ID = DP1.DEPARTMENT_ID");
                sql.AppendLine("    LEFT JOIN DEPARTMENT_DATA DP2");
                sql.AppendLine("    ON 試験計画_DCHK_参加者.参加時_所属部_ID = DP2.DEPARTMENT_ID");
                sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA SG2");
                sql.AppendLine("    ON 試験計画_DCHK_参加者.参加時_所属担当_ID = SG2.SECTION_GROUP_ID");
                sql.AppendLine("    LEFT JOIN SECTION_DATA SC2");
                sql.AppendLine("    ON 試験計画_DCHK_参加者.参加時_所属課_ID = SC2.SECTION_ID");
            }
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // 開催日ID
            if (val != null && val.開催日_ID > 0)
            {
                sql.AppendLine("    AND 試験計画_DCHK_参加者.開催日_ID = :開催日_ID");

                prms.Add(new BindModel
                {
                    Name = ":開催日_ID",
                    Type = OracleDbType.Int32,
                    Object = val.開催日_ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 参加者ID
            if (val != null && !string.IsNullOrWhiteSpace(val.PERSONEL_ID))
            {
                sql.AppendLine("    AND 試験計画_DCHK_参加者.PERSONEL_ID = :PERSONEL_ID");

                prms.Add(new BindModel
                {
                    Name = ":PERSONEL_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.PERSONEL_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            if (join)
            {
                sql.AppendLine("    DP1.SORT_NO ASC,");
                sql.AppendLine("    SC1.SORT_NO ASC,");
                sql.AppendLine("    SG1.SORT_NO ASC,");
                sql.AppendLine("    DEPARTMENT_CODE ASC,");
            }
            sql.AppendLine("    NAME ASC");

            if (wait)
            {
                sql.AppendLine("FOR UPDATE");
            }

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 設計チェック参加者の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(List<DesignCheckUserPostInModel> list)
        {
            var users = this.GetData(new DesignCheckUserGetInModel { 開催日_ID = list.FirstOrDefault()?.開催日_ID }, false, true);

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験計画_DCHK_参加者 (");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,開催日_ID");
            sql.AppendLine("    ,PERSONEL_ID");
            sql.AppendLine("    ,参加者名");
            sql.AppendLine("    ,参加時_所属部_ID");
            sql.AppendLine("    ,参加時_所属課_ID");
            sql.AppendLine("    ,参加時_所属担当_ID");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_DCHK_参加者)");
            sql.AppendLine("    ,:開催日_ID");
            sql.AppendLine("    ,:PERSONEL_ID");
            sql.AppendLine("    ,:参加者名");
            sql.AppendLine("    ,:参加時_所属部_ID");
            sql.AppendLine("    ,:参加時_所属課_ID");
            sql.AppendLine("    ,:参加時_所属担当_ID");
            sql.AppendLine(")");

            var userLogic = new UserLogic();
            userLogic.SetDBAccess(base.db);

            foreach (var val in list)
            {
                if (string.IsNullOrWhiteSpace(val.PERSONEL_ID) == false)
                {
                    // PERSONEL_IDで確認
                    var exists = users?.Select(string.Format("開催日_ID = {0} AND PERSONEL_ID = '{1}'", val?.開催日_ID, val?.PERSONEL_ID))?.Any() == true;

                    // 重複行は追加しない
                    if (exists) continue;
                }
                else
                {
                    // 参加者情報で確認
                    var exists = users.AsEnumerable().Any((x) =>
                                    x.Field<int>("開催日_ID") == val.開催日_ID &&
                                    (x.Field<string>("NAME") ?? "") == val.NAME &&
                                    (x.Field<string>("DEPARTMENT_ID") ?? "") == val.DEPARTMENT_ID &&
                                    (x.Field<string>("SECTION_ID") ?? "") == val.SECTION_ID &&
                                    (x.Field<string>("SECTION_GROUP_ID") ?? "") == val.SECTION_GROUP_ID);


                    // 重複行は追加しない
                    if (exists) continue;
                }

                List<BindModel> prms = new List<BindModel>();

                // 開催日ID：必須
                prms.Add(new BindModel
                {
                    Name = ":開催日_ID",
                    Type = OracleDbType.Int32,
                    Object = val.開催日_ID,
                    Direct = ParameterDirection.Input
                });

                // 参加者ID
                prms.Add(new BindModel
                {
                    Name = ":PERSONEL_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.PERSONEL_ID,
                    Direct = ParameterDirection.Input
                });

                // 参加者名：必須
                prms.Add(new BindModel
                {
                    Name = ":参加者名",
                    Type = OracleDbType.Varchar2,
                    Object = val.NAME,
                    Direct = ParameterDirection.Input
                });

                // 参加時_所属部_ID
                prms.Add(new BindModel
                {
                    Name = ":参加時_所属部_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.DEPARTMENT_ID,
                    Direct = ParameterDirection.Input
                });

                // 参加時_所属課_ID
                prms.Add(new BindModel
                {
                    Name = ":参加時_所属課_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });

                // 参加時_所属担当_ID
                prms.Add(new BindModel
                {
                    Name = ":参加時_所属担当_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_GROUP_ID,
                    Direct = ParameterDirection.Input
                });

                if (!db.InsertData(sql.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 設計チェック参加者の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(List<DesignCheckUserDeleteInModel> list)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_参加者");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            foreach (var val in list)
            {
                List<BindModel> prms = new List<BindModel>();

                // ID：必須
                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Int64,
                    Object = val.ID,
                    Direct = ParameterDirection.Input
                });

                if (!db.DeleteData(sql.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }
    }
}