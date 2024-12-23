using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 車種別進捗業務ロジッククラス
    /// </summary>
    public class WorkProgressLogic : BaseLogic
    {
        #region 車種別進捗状況一覧項目取得
        /// <summary>
        /// 車種別進捗状況一覧項目取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<WorkProgressItemModel> GetWorkProgressItem(WorkProgressItemSearchModel cond)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     C.\"SECTION_GROUP_CODE\"");
            sql.AppendLine("     ,A.\"GENERAL_CODE\"");
            sql.AppendLine("     ,A.\"CATEGORY\"");
            sql.AppendLine("     ,B.\"OPEN_CLOSE\"");
            sql.AppendLine("     ,A.\"CLOSED_DATE\"");
            sql.AppendLine("     ,B.\"LISTED_DATE\"");
            sql.AppendLine("     ,B.\"CURRENT_SITUATION\"");
            sql.AppendLine("     ,B.\"FUTURE_SCHEDULE\"");
            sql.AppendLine("     ,A.\"SELECT_KEYWORD\"");
            sql.AppendLine("     ,F.\"NAME\"");
            sql.AppendLine("     ,B.\"INPUT_DATETIME\"");
            //sql.AppendLine("     ,B.\"SORT_NO\"");
            sql.AppendLine("     ,A.\"ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"WORK_SCHEDULE_ITEM\" A");
//            sql.AppendLine("    INNER JOIN \"試験計画_課題フォローリスト\" B ON A.\"ID\" = B.\"CATEGORY_ID\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_課題フォローリスト\" B ON A.\"ID\" = B.\"CATEGORY_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" C ON A.\"SECTION_GROUP_ID\" = C.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_DATA\" D ON C.\"SECTION_ID\" = D.\"SECTION_ID\"");
            sql.AppendLine("    INNER JOIN \"DEPARTMENT_DATA\" E ON D.\"DEPARTMENT_ID\" = E.\"DEPARTMENT_ID\"");
//            sql.AppendLine("    INNER JOIN \"PERSONEL_LIST\" F ON B.\"INPUT_PERSONEL_ID\" = F.\"PERSONEL_ID\"");
            sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" F ON B.\"INPUT_PERSONEL_ID\" = F.\"PERSONEL_ID\"");
            sql.AppendLine("WHERE 0 = 0");

            //IDが指定されているかどうか
            if (cond.ID != null)
            {
                sql.Append("    AND A.\"ID\" = :ID ");
                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Varchar2,
                    Object = cond.ID,
                    Direct = ParameterDirection.Input
                });

                //ソート順
                sql.AppendLine("ORDER BY");
                sql.AppendLine("    B.\"SORT_NO\"");
            }
            else
            {
                //開発符号
                if (cond.GENERAL_CODE != null)
                {
                    sql.AppendLine("    AND A.\"GENERAL_CODE\" = :GENERAL_CODE");
                    prms.Add(new BindModel
                    {
                        Name = ":GENERAL_CODE",
                        Type = OracleDbType.Varchar2,
                        Object = cond.GENERAL_CODE,
                        Direct = ParameterDirection.Input
                    });
                }
                //ソート番号
                sql.AppendLine("    AND B.\"SORT_NO\" = \'1\'");
                //部ID
                if (cond.DEPARTMENT_ID != null)
                {
                    sql.AppendLine("    AND E.\"DEPARTMENT_ID\" = :DEPARTMENT_ID");
                    prms.Add(new BindModel
                    {
                        Name = ":DEPARTMENT_ID",
                        Type = OracleDbType.Varchar2,
                        Object = cond.DEPARTMENT_ID,
                        Direct = ParameterDirection.Input
                    });
                }
                //課ID
                else if (cond.SECTION_ID != null)
                {
                    sql.AppendLine("    AND D.\"SECTION_ID\" = :SECTION_ID");
                    prms.Add(new BindModel
                    {
                        Name = ":SECTION_ID",
                        Type = OracleDbType.Varchar2,
                        Object = cond.SECTION_ID,
                        Direct = ParameterDirection.Input
                    });
                }
                //担当ID
                else if (cond.SECTION_GROUP_ID != null)
                {
                    sql.AppendLine("    AND C.\"SECTION_GROUP_ID\" = :SECTION_GROUP_ID");
                    prms.Add(new BindModel
                    {
                        Name = ":SECTION_GROUP_ID",
                        Type = OracleDbType.Varchar2,
                        Object = cond.SECTION_GROUP_ID,
                        Direct = ParameterDirection.Input
                    });
                }
                //ステータス
                if (cond.OPEN_CLOSE != null && cond.OPEN_CLOSE == "close")
                {
                    sql.AppendLine("    AND B.\"OPEN_CLOSE\" = \'close\'");
                }
                else if (cond.OPEN_CLOSE != null && cond.OPEN_CLOSE == "open")
                {
                    sql.AppendLine("    AND B.\"OPEN_CLOSE\" <> \'close\'");
                }

                //ソート順
                sql.AppendLine("ORDER BY");
                sql.AppendLine("    B.\"LISTED_DATE\" DESC");
            }

            //取得
            return db.ReadModelList<WorkProgressItemModel>(sql.ToString(), prms);
        }
        #endregion

        #region 車種別進捗状況一覧項目更新
        /// <summary>
        /// 車種別進捗状況一覧項目更新
        /// </summary>
        /// <param name="list">車種別進捗状況一覧項目</param>
        /// <returns>更新可否</returns>
        public bool UpdateWorkProgressItem(List<WorkProgressItemModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //DEVELOPMENT_SCHEDULEを更新
                results.Add(this.UpdateScheduleItem(list));
            }

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
        /// 車種別進捗状況一覧項目更新
        /// </summary>
        /// <param name="list">車種別進捗状況一覧項目</param>
        /// <returns>更新可否</returns>
        private bool UpdateScheduleItem(List<WorkProgressItemModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"WORK_SCHEDULE_ITEM\"");
            sql.AppendLine("SET");
            sql.AppendLine("    \"SELECT_KEYWORD\" = :SELECT_KEYWORD");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :ID");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":SELECT_KEYWORD",
                         Type = OracleDbType.Varchar2,
                         Object = schedule.SELECT_KEYWORD,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":ID",
                         Type = OracleDbType.Double,
                         Object = schedule.ID,
                         Direct = ParameterDirection.Input
                     }
                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));
            }

            return results.All(x => x == true);
        }
        #endregion
    }
}