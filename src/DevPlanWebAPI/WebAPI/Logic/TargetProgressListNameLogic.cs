using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 目標進度リスト名API
    /// </summary>
    /// <remarks>目標進度リスト名検索</remarks>
    public class TargetProgressListNameLogic : BaseLogic
    {
        #region 目標進度リスト名検索
        /// <summary>
        /// 目標進度リスト名検索
        /// </summary>
        /// <param name="val">APIが受け取ったパラメータ</param>
        /// <returns></returns>
        public List<TargetProgressListNameSearchOutModel> GetData(TargetProgressListNameSearchInModel val)
        {
            #region <<< 目標進度リスト名検索SQL文の組立およびパラメータの設定 >>>
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = val.DEPARTMENT_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input }
            };

            var sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT");            
            sql.AppendLine("     \"GENERAL_CODE\".\"GENERAL_CODE\"");

            switch (val.PROCESS_CATEGORY)
            {
                case "1":
                    sql.AppendLine("    ,\"目標進度_チェックリスト名\".\"ID\" AS \"CHECKLIST_NAME_ID\"");
                    sql.AppendLine("    ,\"目標進度_チェックリスト名\".\"性能名_ID\"");
                    sql.AppendLine("    ,(\"目標進度_チェックリスト名\".\"GENERAL_CODE\" || '-' || \"目標進度_性能名\".\"性能名\" || DECODE(\"目標進度_チェックリスト名\".\"仕様\",NULL,NULL,'-' || \"目標進度_チェックリスト名\".\"仕様\")) AS \"CHECKLIST_NAME\"");
                    break;

                case "2":
                    sql.AppendLine("    ,\"DEPARTMENT_DATA\".\"DEPARTMENT_ID\"");
                    sql.AppendLine("    ,\"DEPARTMENT_DATA\".\"SORT_NO\"");
                    sql.AppendLine("    ,\"目標進度_チェックリスト名\".\"GENERAL_CODE\" || '-' || \"DEPARTMENT_DATA\".\"DEPARTMENT_CODE\" AS \"CHECKLIST_NAME\"");
                    break;

                case "3":
                    sql.AppendLine("    ,\"DEPARTMENT_DATA\".\"DEPARTMENT_ID\"");
                    sql.AppendLine("    ,\"SECTION_DATA\".\"SECTION_ID\"");
                    sql.AppendLine("    ,\"DEPARTMENT_DATA\".\"SORT_NO\"");
                    sql.AppendLine("    ,\"SECTION_DATA\".\"SORT_NO\"");
                    sql.AppendLine("    ,\"目標進度_チェックリスト名\".\"GENERAL_CODE\" || '-' || \"SECTION_DATA\".\"SECTION_CODE\" AS \"CHECKLIST_NAME\"");
                    break;
            }

            sql.AppendLine("FROM");
            sql.AppendLine("    \"目標進度_チェックリスト名\"");
            sql.AppendLine("    LEFT JOIN \"目標進度_性能名\"");
            sql.AppendLine("    ON \"目標進度_チェックリスト名\".\"性能名_ID\" = \"目標進度_性能名\".\"ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_DATA\"");
            sql.AppendLine("    ON \"目標進度_性能名\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"DEPARTMENT_DATA\"");
            sql.AppendLine("    ON \"SECTION_DATA\".\"DEPARTMENT_ID\" = \"DEPARTMENT_DATA\".\"DEPARTMENT_ID\"");
            sql.AppendLine("    LEFT JOIN \"GENERAL_CODE\"");
            sql.AppendLine("    ON \"目標進度_チェックリスト名\".\"GENERAL_CODE\" = \"GENERAL_CODE\".\"GENERAL_CODE\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_他部署閲覧許可 ATH\"");
            sql.AppendLine("    ON \"GENERAL_CODE\".\"GENERAL_CODE\" = \"ATH\".\"GENERAL_CODE\"");
            sql.AppendLine("WHERE  0 = 0");
            sql.AppendLine("    AND \"GENERAL_CODE\".\"UNDER_DEVELOPMENT\" = 1");
            sql.AppendLine("    AND \"ATH\".\"PERSONEL_ID\" = :PERSONEL_ID");
            sql.AppendLine("    AND \"DEPARTMENT_DATA\".\"FLAG_EXIST\" = 1");
            sql.AppendLine("    AND \"SECTION_DATA\".\"FLAG_EXIST\" = 1");

            // 自部署の場合に条件追加
            if (val.DIVISION_CATEGORY == "1")
            {
                sql.AppendLine("    AND EXISTS");
                sql.AppendLine("                (");
                sql.AppendLine("                    SELECT");
                sql.AppendLine("                        *");
                sql.AppendLine("                    FROM");
                sql.AppendLine("                        \"SECTION_DATA\" A");
                sql.AppendLine("                    WHERE 0 = 0");
                sql.AppendLine("                        AND A.\"DEPARTMENT_ID\" = :DEPARTMENT_ID");
                sql.AppendLine("                        AND A.\"SECTION_ID\" = \"目標進度_性能名\".\"SECTION_ID\"");
                sql.AppendLine("                )");

            }

            //目標進度リスト名ID
            if (val.CHECKLIST_NAME_ID != null)
            {
                sql.AppendLine("    AND \"目標進度_チェックリスト名\".\"ID\" = :CHECKLIST_NAME_ID");

                prms.Add(new BindModel { Name = ":CHECKLIST_NAME_ID", Type = OracleDbType.Int32, Object = val.CHECKLIST_NAME_ID, Direct = ParameterDirection.Input });

            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    \"GENERAL_CODE\".\"GENERAL_CODE\"");

            switch (val.PROCESS_CATEGORY)
            {
                case "1":
                    sql.AppendLine("    ,\"目標進度_チェックリスト名\".\"性能名_ID\"");
                    sql.AppendLine("    ,\"CHECKLIST_NAME\"");
                    break;

                case "2":
                    sql.AppendLine("    ,\"DEPARTMENT_DATA\".\"SORT_NO\"");
                    break;

                case "3":
                    sql.AppendLine("    ,\"DEPARTMENT_DATA\".\"SORT_NO\"");
                    sql.AppendLine("    ,\"SECTION_DATA\".\"SORT_NO\"");
                    break;

            }
            #endregion

            return db.ReadModelList<TargetProgressListNameSearchOutModel>(sql.ToString(), prms);

        }
        #endregion

        #region 目標進度_チェックリスト　項目更新
        /// <summary>
        /// 目標進度_チェックリスト　項目更新
        /// </summary>
        /// <param name="val">目標進度_チェックリスト項目</param>
        /// <returns>更新可否</returns>
        public bool PutData(TargetProgressListNameUpdateInModel val)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //「目標進度_チェックリスト名」テーブルを更新
            results.Add(this.UpdateTargetProgressListName(val));

            //登録が成功したかどうか
            var flg = results.All(x => x == true);
            if (flg == true)
            {
                //コミット
                db.Commit();

            }
            else
            {
                //ロールバック
                db.Rollback();

            }

            return flg;

        }

        /// <summary>
        /// 「目標進度_チェックリスト名」テーブルを更新
        /// </summary>
        /// <param name="val">「目標進度_チェックリスト名」テーブルを更新</param>
        /// <returns>更新可否</returns>
        private bool UpdateTargetProgressListName(TargetProgressListNameUpdateInModel val)
        {
            var results = new List<bool>();

            //更新
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"目標進度_チェックリスト名\"");
            sql.AppendLine("SET");
            sql.AppendLine("     \"仕様\" = :NEW_LISTNAME");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :CHECKLIST_NAME_ID");

            //パラメータ
            var prms = new List<BindModel>();
            prms.Add(new BindModel { Name = ":NEW_LISTNAME", Type = OracleDbType.Varchar2, Object = val.NEW_LISTNAME, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":CHECKLIST_NAME_ID", Type = OracleDbType.Int32, Object = val.CHECKLIST_NAME_ID, Direct = ParameterDirection.Input });

            results.Add(db.UpdateData(sql.ToString(), prms));

            return results.All(x => x == true);

        }
        #endregion
    }
}