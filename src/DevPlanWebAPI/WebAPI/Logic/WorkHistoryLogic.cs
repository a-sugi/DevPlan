using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;


using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 作業履歴ロジッククラス
    /// </summary>
    public class WorkHistoryLogic : BaseLogic
    {
        #region 作業履歴取得
        /// <summary>
        /// 作業履歴取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<WorkHistoryModel> GetWorkHistory(WorkHistorySearchModel cond)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"ID\"");
            sql.AppendLine("    ,A.\"CATEGORY_ID\"");
            sql.AppendLine("    ,A.\"CURRENT_SITUATION\"");
            sql.AppendLine("    ,A.\"FUTURE_SCHEDULE\"");
            sql.AppendLine("    ,A.\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,E.\"SECTION_CODE\" AS \"INPUT_SECTION_CODE\"");
            sql.AppendLine("    ,C.\"NAME\" AS \"INPUT_NAME\"");
            sql.AppendLine("    ,A.\"INPUT_DATETIME\"");
            sql.AppendLine("    ,A.\"OPEN_CLOSE\"");
            sql.AppendLine("    ,TRUNC(A.\"LISTED_DATE\") AS \"LISTED_DATE\"");
            sql.AppendLine("    ,A.\"SORT_NO\"");
            sql.AppendLine("    ,A.\"IMPORTANT_ITEM\"");
            sql.AppendLine("    ,A.\"MARK\"");
            sql.AppendLine("    ,A.\"INPUT_LOGIN_ID\"");
            sql.AppendLine("    ,A.\"種別_ID\"");
            sql.AppendLine("    ,A.\"SCHEDULE_ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"試験計画_課題フォローリスト\" A");
            sql.AppendLine("    LEFT JOIN \"試験計画_試験車履歴_種別\" B");
            sql.AppendLine("    ON A.\"種別_ID\" = B.\"ID\"");
            sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" C");
            sql.AppendLine("    ON A.\"INPUT_PERSONEL_ID\" = C.\"PERSONEL_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" D");
            sql.AppendLine("    ON C.\"SECTION_GROUP_ID\" = D.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" E");
            sql.AppendLine("    ON D.\"SECTION_ID\" = E.\"SECTION_ID\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"CATEGORY_ID\" = :CATEGORY_ID");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     \"LISTED_DATE\" DESC");
            sql.AppendLine("    ,\"SORT_NO\"");
            sql.AppendLine("    ,\"ID\"");

            //パラメータ
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = cond.CATEGORY_ID, Direct = ParameterDirection.Input }

            };

            return db.ReadModelList<WorkHistoryModel>(sql.ToString(), prms);

        }

        /// <summary>
        /// 試験計画_課題フォローリストのID取得
        /// </summary>
        /// <returns>試験計画_課題フォローリストのID</returns>
        public decimal GetTestPlaTaskFollowListID()
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    (NVL(MAX(\"ID\"),0) + 1) AS \"ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"試験計画_課題フォローリスト\"");

            return db.ReadModelList<WorkHistoryModel>(sql.ToString(), null).First().ID;

        }
        #endregion

        #region 作業履歴の登録
        /// <summary>
        /// 作業履歴の登録
        /// </summary>
        /// <param name="list">作業履歴</param>
        /// <returns>更新可否</returns>
        /// <remarks>メソッド内でのトランザクションなし</remarks>
        public bool MergeWorkHistory(List<WorkHistoryModel> list)
        {
            var results = new List<bool>();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //試験計画_課題フォローリストを登録
                results.Add(this.MergeKadaiFollowList(list.Where(x => x.DELETE_FLG == false).ToList()));

                //試験計画_課題フォローリストを削除
                results.Add(this.DeleteKadaiFollowListAllById(list.Where(x => x.DELETE_FLG == true).Select(x => x.ID).ToList()));

            }

            return results.All(x => x == true);

        }

        /// <summary>
        /// 作業履歴登録
        /// </summary>
        /// <param name="list">作業履歴</param>
        /// <returns>登録可否</returns>
        private bool MergeKadaiFollowList(List<WorkHistoryModel> list)
        {
            //対象が無ければ終了
            if (list == null || list.Any() == false)
            {
                return true;

            }

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"試験計画_課題フォローリスト\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             (NVL(MAX(\"ID\"),0) + 1) AS \"NEW_ID\"");
            sql.AppendLine("            ,:ID AS \"ID\"");
            sql.AppendLine("            ,:CATEGORY_ID AS \"CATEGORY_ID\"");
            sql.AppendLine("            ,:CURRENT_SITUATION AS \"CURRENT_SITUATION\"");
            sql.AppendLine("            ,:FUTURE_SCHEDULE AS \"FUTURE_SCHEDULE\"");
            sql.AppendLine("            ,:INPUT_PERSONEL_ID AS \"INPUT_PERSONEL_ID\"");
            sql.AppendLine("            ,:INPUT_DATETIME AS \"INPUT_DATETIME\"");
            sql.AppendLine("            ,:OPEN_CLOSE AS \"OPEN_CLOSE\"");
            sql.AppendLine("            ,:LISTED_DATE AS \"LISTED_DATE\"");
            sql.AppendLine("            ,:SORT_NO AS \"SORT_NO\"");
            sql.AppendLine("            ,:IMPORTANT_ITEM AS \"IMPORTANT_ITEM\"");
            sql.AppendLine("            ,:MARK AS \"MARK\"");
            sql.AppendLine("            ,:INPUT_PERSONEL_ID AS \"INPUT_LOGIN_ID\"");
            sql.AppendLine("            ,:種別_ID AS \"種別_ID\"");
            sql.AppendLine("            ,:SCHEDULE_ID AS \"SCHEDULE_ID\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"試験計画_課題フォローリスト\"");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"ID\" = B.\"ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("         A.\"CURRENT_SITUATION\" = B.\"CURRENT_SITUATION\"");
            sql.AppendLine("        ,A.\"FUTURE_SCHEDULE\" = B.\"FUTURE_SCHEDULE\"");
            sql.AppendLine("        ,A.\"INPUT_PERSONEL_ID\" = B.\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("        ,A.\"INPUT_DATETIME\" = B.\"INPUT_DATETIME\"");
            sql.AppendLine("        ,A.\"OPEN_CLOSE\" = B.\"OPEN_CLOSE\"");
            sql.AppendLine("        ,A.\"LISTED_DATE\" = B.\"LISTED_DATE\"");
            sql.AppendLine("        ,A.\"SORT_NO\" = B.\"SORT_NO\"");
            sql.AppendLine("        ,A.\"IMPORTANT_ITEM\" = B.\"IMPORTANT_ITEM\"");
            sql.AppendLine("        ,A.\"MARK\" = B.\"MARK\"");
            sql.AppendLine("        ,A.\"INPUT_LOGIN_ID\" = B.\"INPUT_LOGIN_ID\"");
            sql.AppendLine("        ,A.\"種別_ID\" = B.\"種別_ID\"");
            sql.AppendLine("        ,A.\"SCHEDULE_ID\" = B.\"SCHEDULE_ID\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"ID\"");
            sql.AppendLine("        ,\"CATEGORY_ID\"");
            sql.AppendLine("        ,\"CURRENT_SITUATION\"");
            sql.AppendLine("        ,\"FUTURE_SCHEDULE\"");
            sql.AppendLine("        ,\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("        ,\"INPUT_DATETIME\"");
            sql.AppendLine("        ,\"OPEN_CLOSE\"");
            sql.AppendLine("        ,\"LISTED_DATE\"");
            sql.AppendLine("        ,\"SORT_NO\"");
            sql.AppendLine("        ,\"IMPORTANT_ITEM\"");
            sql.AppendLine("        ,\"MARK\"");
            sql.AppendLine("        ,\"INPUT_LOGIN_ID\"");
            sql.AppendLine("        ,\"種別_ID\"");
            sql.AppendLine("        ,\"SCHEDULE_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"NEW_ID\"");
            sql.AppendLine("        ,B.\"CATEGORY_ID\"");
            sql.AppendLine("        ,B.\"CURRENT_SITUATION\"");
            sql.AppendLine("        ,B.\"FUTURE_SCHEDULE\"");
            sql.AppendLine("        ,B.\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("        ,B.\"INPUT_DATETIME\"");
            sql.AppendLine("        ,B.\"OPEN_CLOSE\"");
            sql.AppendLine("        ,B.\"LISTED_DATE\"");
            sql.AppendLine("        ,B.\"SORT_NO\"");
            sql.AppendLine("        ,B.\"IMPORTANT_ITEM\"");
            sql.AppendLine("        ,B.\"MARK\"");
            sql.AppendLine("        ,B.\"INPUT_LOGIN_ID\"");
            sql.AppendLine("        ,B.\"種別_ID\"");
            sql.AppendLine("        ,B.\"SCHEDULE_ID\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var history in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Decimal, Object = history.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Decimal, Object = history.CATEGORY_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CURRENT_SITUATION", Type = OracleDbType.Varchar2, Object = history.CURRENT_SITUATION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FUTURE_SCHEDULE", Type = OracleDbType.Varchar2, Object = history.FUTURE_SCHEDULE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = history.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_DATETIME", Type = OracleDbType.Date, Object = history.INPUT_DATETIME, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":OPEN_CLOSE", Type = OracleDbType.Varchar2, Object = history.OPEN_CLOSE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":LISTED_DATE", Type = OracleDbType.Date, Object = history.LISTED_DATE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SORT_NO", Type = OracleDbType.Int32, Object = history.SORT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":IMPORTANT_ITEM", Type = OracleDbType.Int32, Object = history.IMPORTANT_ITEM, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MARK", Type = OracleDbType.Varchar2, Object = history.MARK, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":種別_ID", Type = OracleDbType.Int32, Object = history.種別_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SCHEDULE_ID", Type = OracleDbType.Decimal, Object = history.SCHEDULE_ID, Direct = ParameterDirection.Input }

                };

                //登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 作業履歴追加
        /// <summary>
        /// 試験計画_課題フォローリスト追加
        /// </summary>
        /// <param name="history">試験計画_課題フォローリスト</param>
        /// <returns>追加可否</returns>
        public bool InsertTestPlaTaskFollowList(WorkHistoryModel history)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"試験計画_課題フォローリスト\"");
            sql.AppendLine("( ");
            sql.AppendLine("     \"ID\"");
            sql.AppendLine("    ,\"CATEGORY_ID\"");
            sql.AppendLine("    ,\"CURRENT_SITUATION\"");
            sql.AppendLine("    ,\"FUTURE_SCHEDULE\"");
            sql.AppendLine("    ,\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,\"INPUT_DATETIME\"");
            sql.AppendLine("    ,\"OPEN_CLOSE\"");
            sql.AppendLine("    ,\"LISTED_DATE\"");
            sql.AppendLine("    ,\"SORT_NO\"");
            sql.AppendLine("    ,\"IMPORTANT_ITEM\"");
            sql.AppendLine("    ,\"MARK\"");
            sql.AppendLine("    ,\"INPUT_LOGIN_ID\"");
            sql.AppendLine("    ,\"種別_ID\"");
            sql.AppendLine("    ,\"SCHEDULE_ID\"");
            sql.AppendLine(") ");
            sql.AppendLine("VALUES");
            sql.AppendLine("( ");
            sql.AppendLine("     :ID");
            sql.AppendLine("    ,:CATEGORY_ID");
            sql.AppendLine("    ,:CURRENT_SITUATION");
            sql.AppendLine("    ,:FUTURE_SCHEDULE");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID");
            sql.AppendLine("    ,SYSTIMESTAMP");
            sql.AppendLine("    ,:OPEN_CLOSE");
            sql.AppendLine("    ,:LISTED_DATE");
            sql.AppendLine("    ,:SORT_NO");
            sql.AppendLine("    ,:IMPORTANT_ITEM");
            sql.AppendLine("    ,:MARK");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID");
            sql.AppendLine("    ,:種別_ID");
            sql.AppendLine("    ,:SCHEDULE_ID");
            sql.AppendLine(") ");

            //パラメータ
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":ID", Type = OracleDbType.Decimal, Object = history.ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = history.CATEGORY_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":CURRENT_SITUATION", Type = OracleDbType.Varchar2, Object = history.CURRENT_SITUATION, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FUTURE_SCHEDULE", Type = OracleDbType.Varchar2, Object = history.FUTURE_SCHEDULE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = history.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":OPEN_CLOSE", Type = OracleDbType.Varchar2, Object = history.OPEN_CLOSE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":LISTED_DATE", Type = OracleDbType.Date, Object = history.LISTED_DATE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SORT_NO", Type = OracleDbType.Int32, Object = history.SORT_NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":IMPORTANT_ITEM", Type = OracleDbType.Int32, Object = history.IMPORTANT_ITEM, Direct = ParameterDirection.Input },
                new BindModel { Name = ":MARK", Type = OracleDbType.Varchar2, Object = history.MARK, Direct = ParameterDirection.Input },
                new BindModel { Name = ":種別_ID", Type = OracleDbType.Int32, Object = history.種別_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SCHEDULE_ID", Type = OracleDbType.Int64, Object = history.SCHEDULE_ID, Direct = ParameterDirection.Input }

            };

            //追加
            return db.InsertData(sql.ToString(), prms);

        }
        #endregion

        #region 作業履歴削除
        /// <summary>
        /// 試験計画_課題フォローリストを全て削除(ID)
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>追加可否</returns>
        public bool DeleteKadaiFollowListAllById(List<decimal> idList)
        {
            //対象が無ければ終了
            if (idList == null || idList.Any() == false)
            {
                return true;

            }

            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_課題フォローリスト\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.Append("    AND \"ID\" IN (NULL");

            //IDで削除
            for (var i = 0; i < idList.Count(); i++)
            {
                var name = string.Format(":ID{0}", i);

                sql.AppendFormat(",{0}", name);

                prms.Add(new BindModel { Name = name, Type = OracleDbType.Decimal, Object = idList.ElementAt(i), Direct = ParameterDirection.Input });

            }

            sql.AppendLine(")");

            return db.DeleteData(sql.ToString(), prms);

        }

        /// <summary>
        /// 試験計画_課題フォローリストを全て削除(カテゴリーID)
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>追加可否</returns>
        public bool DeleteKadaiFollowListAllByCategoryId(List<long> idList)
        {
            //対象が無ければ終了
            if (idList == null || idList.Any() == false)
            {
                return true;

            }

            var prms = new List<BindModel>();
            
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_課題フォローリスト\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.Append("    AND \"CATEGORY_ID\" IN (NULL");

            //IDで削除
            for (var i = 0; i < idList.Count(); i++)
            {
                var name = string.Format(":ID{0}", i);

                sql.AppendFormat(",{0}", name);

                prms.Add(new BindModel { Name = name, Type = OracleDbType.Int64, Object = idList.ElementAt(i), Direct = ParameterDirection.Input });

            }

            sql.AppendLine(")");

            return db.DeleteData(sql.ToString(), prms);

        }
        #endregion

    }
}