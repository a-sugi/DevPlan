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
    /// 試験車業務ロジッククラス
    /// </summary>
    public class TestCarLogic : BaseLogic
    {
        #region メンバ変数
        private const int TableID = 1;
        private readonly int? WorkComplete = 100;

        private const string Youbou = "要望";
        private const string Chousei = "１次調整";
        private const string Kettei = "決定";
        #endregion

        #region 試験車スケジュール項目取得
        /// <summary>
        /// 試験車スケジュール項目取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TestCarScheduleItemModel> GetTestCarScheduleItem(TestCarScheduleItemSearchModel cond)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"ID\"");
            sql.AppendLine("    ,A.\"GENERAL_CODE\"");
            sql.AppendLine("    ,CASE");
            sql.AppendLine("      WHEN X.物品コード IS NOT NULL THEN A.\"CATEGORY\" || '\nGPS搭載'");
            sql.AppendLine("      ELSE A.\"CATEGORY\"");
            sql.AppendLine("     END AS CATEGORY ");
            sql.AppendLine("    ,A.\"SORT_NO\"");
            sql.AppendLine("    ,A.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    ,A.\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,A.\"CATEGORY_ID\"");
            sql.AppendLine("    ,A.\"CLOSED_DATE\"");
            sql.AppendLine("    ,A.\"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("    ,B.\"管理票番号\"");
            sql.AppendLine("    ,C.\"車系\"");
            sql.AppendLine("    ,C.\"駐車場番号\"");
            sql.AppendLine("    ,G.\"試作時期\"");
            sql.AppendLine("    ,G.\"号車\"");
            sql.AppendLine("    ,G.\"FLAG_ナビ付\"");
            sql.AppendLine("    ,G.\"FLAG_ETC付\"");
            sql.AppendLine("    ,CASE ");
            sql.AppendLine("      WHEN X.物品コード IS NOT NULL THEN 'GPS搭載'");
            sql.AppendLine("      ELSE NULL");
            sql.AppendLine("     END AS XEYE_EXIST ");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"TESTCAR_SCHEDULE_ITEM\" A");
            sql.AppendLine("    LEFT JOIN (SELECT DISTINCT CATEGORY_ID,管理票番号,FLAG_要予約許可 FROM 試験計画_外製車日程_車両リスト) B");
            sql.AppendLine("    ON A.\"ID\" = B.\"CATEGORY_ID\"");
            sql.AppendLine("    LEFT JOIN \"VIEW_試験車基本情報\" C");
            sql.AppendLine("    ON B.\"管理票番号\" = C.\"管理票NO\"");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         D.\"データID\"");
            sql.AppendLine("                        ,D.\"試作時期\"");
            sql.AppendLine("                        ,D.\"号車\"");
            sql.AppendLine("                        ,D.\"仕向地\"");
            sql.AppendLine("                        ,D.FLAG_ナビ付");
            sql.AppendLine("                        ,D.FLAG_ETC付");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"試験車履歴情報\" D");
            sql.AppendLine("                        INNER JOIN");
            sql.AppendLine("                                    (");
            sql.AppendLine("                                        SELECT");
            sql.AppendLine("                                             E.\"データID\"");
            sql.AppendLine("                                            ,MAX(E.\"履歴NO\") AS \"履歴NO\"");
            sql.AppendLine("                                        FROM");
            sql.AppendLine("                                            \"試験車履歴情報\" E");
            sql.AppendLine("                                        GROUP BY");
            sql.AppendLine("                                            E.\"データID\"");
            sql.AppendLine("                                    ) F");
            sql.AppendLine("                        ON D.\"データID\" = F.\"データID\"");
            sql.AppendLine("                        AND D.\"履歴NO\" = F.\"履歴NO\"");
            sql.AppendLine("                ) G");
            sql.AppendLine("    ON C.\"データID\" = G.\"データID\"");
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_TO_XEYE\" X");
            sql.AppendLine("    ON X.\"物品名2\" = C.\"管理票NO\"");
            sql.AppendLine("WHERE 0 = 0");

            //IDが指定されているかどうか
            if (cond.ID != null && cond.ID.Any() == true)
            {
                sql.Append("    AND A.\"ID\" IN (NULL");

                for (var i = 0; i < cond.ID.Count(); i++)
                {
                    var name = string.Format(":ID{0}", i);

                    sql.AppendFormat(",{0}", name);

                    prms.Add(new BindModel { Name = name, Type = OracleDbType.Int64, Object = cond.ID.ElementAt(i), Direct = ParameterDirection.Input });

                }

                sql.Append(")");
                sql.AppendLine();

            }
            else
            {
                //開発符号
                if (string.IsNullOrWhiteSpace(cond.GENERAL_CODE) == false)
                {
                    sql.AppendLine("    AND A.\"GENERAL_CODE\" = :GENERAL_CODE");
                    prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = cond.GENERAL_CODE, Direct = ParameterDirection.Input });

                }

                //試作時期
                if (string.IsNullOrWhiteSpace(cond.試作時期) == false)
                {
                    sql.AppendLine("    AND G.\"試作時期\" LIKE '%' || :試作時期 || '%'");
                    prms.Add(new BindModel { Name = ":試作時期", Type = OracleDbType.Varchar2, Object = cond.試作時期, Direct = ParameterDirection.Input });

                }

                //号車
                if (string.IsNullOrWhiteSpace(cond.号車) == false)
                {
                    sql.AppendLine("    AND G.\"号車\" LIKE '%' || :号車 || '%'");
                    prms.Add(new BindModel { Name = ":号車", Type = OracleDbType.Varchar2, Object = cond.号車, Direct = ParameterDirection.Input });

                }

                //車型
                if (string.IsNullOrWhiteSpace(cond.車型) == false)
                {
                    sql.AppendLine("    AND C.\"車型\" = :車型");
                    prms.Add(new BindModel { Name = ":車型", Type = OracleDbType.Varchar2, Object = cond.車型, Direct = ParameterDirection.Input });

                }

                //仕向地
                if (string.IsNullOrWhiteSpace(cond.仕向地) == false)
                {
                    sql.AppendLine("    AND G.\"仕向地\" = :仕向地");
                    prms.Add(new BindModel { Name = ":仕向地", Type = OracleDbType.Varchar2, Object = cond.仕向地, Direct = ParameterDirection.Input });

                }

                //Openフラグ
                switch (cond.OPEN_FLG)
                {
                    case true:
                        sql.AppendLine("    AND A.\"CLOSED_DATE\" IS NULL");
                        break;

                    case false:
                        sql.AppendLine("    AND A.\"CLOSED_DATE\" IS NOT NULL");
                        break;
                }

            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    A.\"SORT_NO\"");

            //取得
            return db.ReadModelList<TestCarScheduleItemModel>(sql.ToString(), prms);

        }
        #endregion

        #region 試験車スケジュール項目追加
        /// <summary>
        /// 試験車スケジュール項目追加
        /// </summary>
        /// <param name="list">試験車スケジュール項目</param>
        /// <returns>追加可否</returns>
        public bool InsertTestCarScheduleItem(List<TestCarScheduleItemModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //DEVELOPMENT_SCHEDULEを追加
                results.Add(this.InsertScheduleItem(list));

                //DEVELOPMENT_SCHEDULEのソート順を設定
                results.Add(this.UpdateTestCarScheduleItemSortNo(list));

                //試験計画_外製車日程_車両リストを登録
                results.Add(this.MergeCarList(list));

            }

            //追加が成功したかどうか
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
        /// DEVELOPMENT_SCHEDULEを追加
        /// </summary>
        /// <param name="list">試験車スケジュール項目</param>
        /// <returns>追加可否</returns>
        private bool InsertScheduleItem(List<TestCarScheduleItemModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"TESTCAR_SCHEDULE_ITEM\"");
            sql.AppendLine("(");
            sql.AppendLine("     \"ID\"");
            sql.AppendLine("    ,\"GENERAL_CODE\"");
            sql.AppendLine("    ,\"CATEGORY\"");
            sql.AppendLine("    ,\"SORT_NO\"");
            sql.AppendLine("    ,\"SECTION_GROUP_ID\"");
            sql.AppendLine("    ,\"FLAG_CLASS\"");
            sql.AppendLine("    ,\"FLAG_SEPARATOR\"");
            sql.AppendLine("    ,\"INPUT_DATETIME\"");
            sql.AppendLine("    ,\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,\"INPUT_LOGIN_ID\"");
            sql.AppendLine("    ,\"CHANGE_DATETIME\"");
            sql.AppendLine("    ,\"CATEGORY_ID\"");
            sql.AppendLine("    ,\"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine(")");
            sql.AppendLine("SELECT");
            sql.AppendLine("     :ID AS \"ID\"");
            sql.AppendLine("    ,:GENERAL_CODE AS \"GENERAL_CODE\"");
            sql.AppendLine("    ,:CATEGORY AS \"CATEGORY\"");
            sql.AppendLine("    ,DECODE(NVL(:SORT_NO,0),0,(NVL(MAX(A.\"SORT_NO\"),0) + 1),:SORT_NO) AS \"SORT_NO\"");
            sql.AppendLine("    ,:SECTION_GROUP_ID AS \"SECTION_GROUP_ID\"");
            sql.AppendLine("    ,'試験車日程' AS \"FLAG_CLASS\"");
            sql.AppendLine("    ,1 AS \"FLAG_SEPARATOR\"");
            sql.AppendLine("    ,SYSTIMESTAMP AS \"INPUT_DATETIME\"");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID AS \"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID AS \"INPUT_LOGIN_ID\"");
            sql.AppendLine("    ,SYSTIMESTAMP AS \"CHANGE_DATETIME\"");
            sql.AppendLine("    ,:ID AS \"CATEGORY_ID\"");
            sql.AppendLine("    ,:PARALLEL_INDEX_GROUP AS \"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"TESTCAR_SCHEDULE_ITEM\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"GENERAL_CODE\" = :GENERAL_CODE");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //スケジュールのID
                schedule.ID = base.GetLogic<CommonLogic>().GetScheduleNewID();

                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = schedule.GENERAL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CATEGORY", Type = OracleDbType.Varchar2, Object = schedule.CATEGORY, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SORT_NO", Type = OracleDbType.Decimal, Object = schedule.SORT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = schedule.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = schedule.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = schedule.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input }
                };

                //追加
                results.Add(db.InsertData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 試験車スケジュール項目更新
        /// <summary>
        /// 試験車スケジュール項目更新
        /// </summary>
        /// <param name="list">試験車スケジュール項目</param>
        /// <returns>更新可否</returns>
        public bool UpdateTestCarScheduleItem(List<TestCarScheduleItemModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //DEVELOPMENT_SCHEDULEを更新
                results.Add(this.UpdateScheduleItem(list));

                //DEVELOPMENT_SCHEDULEのソート順を設定
                results.Add(this.UpdateTestCarScheduleItemSortNo(list));

                //試験計画_外製車日程_車両リストを登録
                results.Add(this.MergeCarList(list));

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
        /// DEVELOPMENT_SCHEDULEを更新
        /// </summary>
        /// <param name="list">試験車スケジュール項目</param>
        /// <returns>更新可否</returns>
        private bool UpdateScheduleItem(List<TestCarScheduleItemModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"TESTCAR_SCHEDULE_ITEM\"");
            sql.AppendLine("SET");
            sql.AppendLine("     \"CATEGORY\" = :CATEGORY");
            sql.AppendLine("    ,\"SORT_NO\" = :SORT_NO");
            sql.AppendLine("    ,\"SECTION_GROUP_ID\" = :SECTION_GROUP_ID");
            sql.AppendLine("    ,\"INPUT_PERSONEL_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,\"INPUT_LOGIN_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,\"CHANGE_DATETIME\" = SYSTIMESTAMP");
            sql.AppendLine("    ,\"PARALLEL_INDEX_GROUP\" = :PARALLEL_INDEX_GROUP");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :ID");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":CATEGORY", Type = OracleDbType.Varchar2, Object = schedule.CATEGORY, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SORT_NO", Type = OracleDbType.Decimal, Object = schedule.SORT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = schedule.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = schedule.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = schedule.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input }

                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 試験車スケジュール項目削除
        /// <summary>
        /// 試験車スケジュール削除
        /// </summary>
        /// <param name="list">試験車スケジュール項目</param>
        /// <returns>削除可否</returns>
        public bool DeleteTestCarScheduleItem(List<TestCarScheduleItemModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //削除対象があるかどうか
            if (list != null && list.Any() == true)
            {
                var idList = list.Select(x => x.ID).ToList();

                //試験計画_外製車日程_車両リストを全て削除
                results.Add(this.DeleteCarListAll(idList));

                //SCHEDULE_CARを全て削除
                results.Add(this.DeleteScheduleCarAll(idList));

                //試験計画_試験車日程_完了記録を全て削除
                results.Add(this.DeleteKanryouKirokuAll(idList));

                //試験計画_試験車日程_設定者を全て削除
                results.Add(this.DeleteSetteisyaAll(idList));

                //試験計画_課題フォローリストを全て削除
                results.Add(base.GetLogic<WorkHistoryLogic>().DeleteKadaiFollowListAllByCategoryId(idList));

                //DEVELOPMENT_SCHEDULEを全て削除
                results.Add(this.DeleteScheduleItemAll(idList));
                results.Add(this.DeleteScheduleAll(idList));

            }

            //削除が成功したかどうか
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
        /// 試験計画_外製車日程_車両リストを全て削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteCarListAll(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_外製車日程_車両リスト\"");
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

        /// <summary>
        /// SCHEDULE_CARを全て削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteScheduleCarAll(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"SCHEDULE_CAR\"");
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

        /// <summary>
        /// 試験計画_試験車日程_完了記録を全て削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteKanryouKirokuAll(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_試験車日程_完了記録\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND EXISTS");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                        *");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"TESTCAR_SCHEDULE\" B");
            sql.AppendLine("                    WHERE 0 = 0");
            sql.AppendLine("                        AND A.\"SCHEDULE_ID\" =  B.\"ID\"");

            sql.Append("                        AND B.\"CATEGORY_ID\" IN (NULL");

            //IDで削除
            for (var i = 0; i < idList.Count(); i++)
            {
                var name = string.Format(":ID{0}", i);

                sql.AppendFormat(",{0}", name);

                prms.Add(new BindModel { Name = name, Type = OracleDbType.Int64, Object = idList.ElementAt(i), Direct = ParameterDirection.Input });

            }

            sql.AppendLine(")");

            sql.AppendLine("                )");

            return db.DeleteData(sql.ToString(), prms);

        }

        /// <summary>
        /// 試験計画_試験車日程_設定者を全て削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteSetteisyaAll(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_試験車日程_設定者\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND EXISTS");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                        *");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"TESTCAR_SCHEDULE\" B");
            sql.AppendLine("                    WHERE 0 = 0");
            sql.AppendLine("                        AND A.\"SCHEDULE_ID\" =  B.\"ID\"");

            sql.Append("                        AND B.\"CATEGORY_ID\" IN (NULL");

            //IDで削除
            for (var i = 0; i < idList.Count(); i++)
            {
                var name = string.Format(":ID{0}", i);

                sql.AppendFormat(",{0}", name);

                prms.Add(new BindModel { Name = name, Type = OracleDbType.Int64, Object = idList.ElementAt(i), Direct = ParameterDirection.Input });

            }

            sql.AppendLine(")");

            sql.AppendLine("                )");

            return db.DeleteData(sql.ToString(), prms);

        }

        /// <summary>
        /// DEVELOPMENT_SCHEDULEを全て削除(スケジュール項目)
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteScheduleItemAll(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"TESTCAR_SCHEDULE_ITEM\"");
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

        /// <summary>
        /// DEVELOPMENT_SCHEDULEを全て削除(スケジュール)
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteScheduleAll(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"TESTCAR_SCHEDULE\"");
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

        #region 試験車スケジュール取得
        /// <summary>
        /// 試験車スケジュール取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TestCarScheduleModel> GetTestCarSchedule(TestCarScheduleSearchModel cond)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql1 = new StringBuilder();
            sql1.AppendLine("SELECT");
            sql1.AppendLine("     A.\"ID\"");
            sql1.AppendLine("    ,A.\"GENERAL_CODE\"");
            sql1.AppendLine("    ,A.\"START_DATE\"");
            sql1.AppendLine("    ,A.\"END_DATE\"");
            sql1.AppendLine("    ,A.\"DESCRIPTION\"");
            sql1.AppendLine("    ,A.\"SYMBOL\"");
            sql1.AppendLine("    ,A.\"SECTION_GROUP_ID\"");
            sql1.AppendLine("    ,A.\"INPUT_DATETIME\"");
            sql1.AppendLine("    ,A.\"INPUT_PERSONEL_ID\"");
            sql1.AppendLine("    ,A.\"CATEGORY_ID\"");
            sql1.AppendLine("    ,A.\"予約種別\"");
            sql1.AppendLine("    ,A.\"試験車日程種別\"");
            sql1.AppendLine("    ,A.\"PARALLEL_INDEX_GROUP\"");
            sql1.AppendLine("    ,B.\"試験内容\"");
            sql1.AppendLine("    ,B.\"オドメータ\"");
            sql1.AppendLine("    ,B.\"脱着部品\"");
            sql1.AppendLine("    ,B.\"変更箇所\"");
            sql1.AppendLine("    ,B.\"車両保管場所\"");
            sql1.AppendLine("    ,CASE WHEN NVL(F.\"完了日\",B.\"完了日\") IS NOT NULL AND TO_CHAR(NVL(F.\"完了日\",B.\"完了日\"),'YYYY/MM/DD') = '1900/01/01' THEN NULL");
            sql1.AppendLine("     ELSE NVL(F.\"完了日\",B.\"完了日\") END AS \"完了日\"");
            sql1.AppendLine("    ,B.\"鍵保管場所\"");
            sql1.AppendLine("    ,B.\"FOLLOWLIST_ID\"");
            sql1.AppendLine("    ,C.\"予約者_ID\"");
            sql1.AppendLine("    ,H.\"SECTION_CODE\" AS \"予約者_SECTION_CODE\"");
            sql1.AppendLine("    ,D.\"NAME\" AS \"予約者_NAME\"");
            sql1.AppendLine("    ,C.\"設定者_ID\"");
            sql1.AppendLine("    ,J.\"SECTION_CODE\" AS \"設定者_SECTION_CODE\"");
            sql1.AppendLine("    ,E.\"NAME\" AS \"設定者_NAME\"");
            sql1.AppendLine("FROM");
            sql1.AppendLine("    \"TESTCAR_SCHEDULE\" A");
            sql1.AppendLine("    LEFT JOIN \"試験計画_試験車日程_完了記録\" B");
            sql1.AppendLine("    ON A.\"ID\" = B.\"SCHEDULE_ID\"");
            sql1.AppendLine("    LEFT JOIN \"試験計画_試験車日程_設定者\" C");
            sql1.AppendLine("    ON A.\"ID\" = C.\"SCHEDULE_ID\"");
            sql1.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" D");
            sql1.AppendLine("    ON C.\"予約者_ID\" = D.\"PERSONEL_ID\"");
            sql1.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" G");
            sql1.AppendLine("    ON D.\"SECTION_GROUP_ID\" = G.\"SECTION_GROUP_ID\"");
            sql1.AppendLine("    LEFT JOIN \"SECTION_DATA\" H");
            sql1.AppendLine("    ON G.\"SECTION_ID\" = H.\"SECTION_ID\"");
            sql1.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" E");
            sql1.AppendLine("    ON C.\"設定者_ID\" = E.\"PERSONEL_ID\"");
            sql1.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" I");
            sql1.AppendLine("    ON E.\"SECTION_GROUP_ID\" = I.\"SECTION_GROUP_ID\"");
            sql1.AppendLine("    LEFT JOIN \"SECTION_DATA\" J");
            sql1.AppendLine("    ON I.\"SECTION_ID\" = J.\"SECTION_ID\"");
            sql1.AppendLine("    LEFT JOIN");
            sql1.AppendLine("                (");
            sql1.AppendLine("                    SELECT");
            sql1.AppendLine("                         A.\"CATEGORY_ID\"");
            sql1.AppendLine("                        ,B.\"ID\" AS \"ID1\"");
            sql1.AppendLine("                        ,C.\"ID\" AS \"ID2\"");
            sql1.AppendLine("                        ,D.\"ID\" AS \"ID3\"");
            sql1.AppendLine("                        ,E.\"完了日\"");
            sql1.AppendLine("                    FROM");
            sql1.AppendLine("                        \"TESTCAR_SCHEDULE\" A");
            sql1.AppendLine("                        LEFT JOIN \"TESTCAR_SCHEDULE\" B");
            sql1.AppendLine("                        ON A.\"CATEGORY_ID\" = B.\"CATEGORY_ID\"");
            sql1.AppendLine("                        AND A.\"コピー元_ID\" = B.\"ID\"");
            sql1.AppendLine("                        AND B.\"試験車日程種別\" = '要望'");
            sql1.AppendLine("                        LEFT JOIN \"TESTCAR_SCHEDULE\" C");
            sql1.AppendLine("                        ON A.\"CATEGORY_ID\" = C.\"CATEGORY_ID\"");
            sql1.AppendLine("                        AND A.\"コピー元_ID\" = C.\"ID\"");
            sql1.AppendLine("                        AND C.\"試験車日程種別\" = '１次調整'");
            sql1.AppendLine("                        LEFT JOIN \"TESTCAR_SCHEDULE\" D");
            sql1.AppendLine("                        ON A.\"CATEGORY_ID\" = D.\"CATEGORY_ID\"");
            sql1.AppendLine("                        AND C.\"コピー元_ID\" = D.\"ID\"");
            sql1.AppendLine("                        AND D.\"試験車日程種別\" = '要望'");
            sql1.AppendLine("                        INNER JOIN \"試験計画_試験車日程_完了記録\" E");
            sql1.AppendLine("                        ON A.\"ID\" = E.\"SCHEDULE_ID\"");
            sql1.AppendLine("                    WHERE 0 = 0");
            sql1.AppendLine("                        AND A.\"試験車日程種別\" = '決定'");
            sql1.AppendLine("                        AND COALESCE(B.\"ID\",C.\"ID\",D.\"ID\") IS NOT NULL");
            sql1.AppendLine("                        AND E.\"完了日\" IS NOT NULL");

            var sql2 = new StringBuilder();
            sql2.AppendLine("                ) F");
            sql2.AppendLine("    ON A.\"CATEGORY_ID\" = F.\"CATEGORY_ID\"");
            sql2.AppendLine("    AND A.\"ID\" IN (F.\"ID1\",F.\"ID2\",F.\"ID3\")");
            sql2.AppendLine("WHERE 0 = 0");

            //IDが指定されているかどうか
            if (cond.ID != null)
            {
                sql1.AppendLine("                        AND :ID IN (A.\"ID\",B.\"ID\",C.\"ID\",D.\"ID\")");

                sql2.AppendLine("    AND A.\"ID\" = :ID");

                prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = cond.ID, Direct = ParameterDirection.Input });

            }
            else
            {
                var categoryIdList = new List<long?>();

                // サブクエリのカテゴリーID指定用
                var sql1b = new StringBuilder();
                var sql1c = new StringBuilder();
                var sql1d = new StringBuilder();

                //カテゴリーIDが指定されているかどうか
                if (cond.CATEGORY_ID == null)
                {
                    //試験車スケジュール項目の検索条件に変換
                    var itemCond = JsonConvert.DeserializeObject<TestCarScheduleItemSearchModel>(JsonConvert.SerializeObject(cond));
                    categoryIdList.AddRange(this.GetTestCarScheduleItem(itemCond).Where(x => x.CATEGORY_ID != null).Select(x => x.CATEGORY_ID));

                }
                else
                {
                    categoryIdList.Add(cond.CATEGORY_ID);

                }

                sql1.Append("                        AND A.\"CATEGORY_ID\" IN (NULL");

                sql1b.Append("                        AND B.\"CATEGORY_ID\" IN (NULL");

                sql1c.Append("                        AND C.\"CATEGORY_ID\" IN (NULL");

                sql1d.Append("                        AND D.\"CATEGORY_ID\" IN (NULL");

                sql2.Append("    AND A.\"CATEGORY_ID\" IN (NULL");

                //カテゴリーID
                if (categoryIdList != null && categoryIdList.Any() == true)
                {
                    for (var i = 0; i < categoryIdList.Count(); i++)
                    {
                        var name = string.Format(":CATEGORY_ID{0}", i);

                        sql1.AppendFormat(",{0}", name);

                        sql1b.AppendFormat(",{0}", name);

                        sql1c.AppendFormat(",{0}", name);

                        sql1d.AppendFormat(",{0}", name);

                        sql2.AppendFormat(",{0}", name);

                        prms.Add(new BindModel { Name = name, Type = OracleDbType.Int64, Object = categoryIdList.ElementAt(i), Direct = ParameterDirection.Input });

                    }

                }

                sql1.AppendLine(")");

                sql1b.AppendLine(")");

                sql1c.AppendLine(")");

                sql1d.AppendLine(")");

                // サブクエリのカテゴリーID指定を追加
                sql1.Append(sql1b).Append(sql1c).Append(sql1d);

                sql2.AppendLine(")");

                var startDate = cond.START_DATE == null ? null : (DateTime?)cond.START_DATE.Value.Date;
                var endDate = cond.END_DATE == null ? null : (DateTime?)cond.END_DATE.Value.Date;

                //期間がすべて入力されているかどうか
                if (cond.START_DATE != null && cond.END_DATE != null)
                {
                    sql2.AppendLine("    AND");
                    sql2.AppendLine("        (");
                    sql2.AppendLine("            :START_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                    sql2.AppendLine("            OR");
                    sql2.AppendLine("            :END_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                    sql2.AppendLine("            OR");
                    sql2.AppendLine("            A.\"START_DATE\" BETWEEN :START_DATE AND (:END_DATE + 1)");
                    sql2.AppendLine("            OR");
                    sql2.AppendLine("            A.\"END_DATE\" BETWEEN :START_DATE AND (:END_DATE + 1)");
                    sql2.AppendLine("        )");

                    prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

                }
                else
                {
                    //期間(From)
                    if (cond.START_DATE != null)
                    {
                        sql2.AppendLine("    AND");
                        sql2.AppendLine("        (");
                        sql2.AppendLine("            :START_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                        sql2.AppendLine("            OR");
                        sql2.AppendLine("            A.\"START_DATE\" >= :START_DATE");
                        sql2.AppendLine("        )");

                        prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input });

                    }

                    //期間(To)
                    if (cond.END_DATE != null)
                    {
                        sql2.AppendLine("    AND");
                        sql2.AppendLine("        (");
                        sql2.AppendLine("            :END_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                        sql2.AppendLine("            OR");
                        sql2.AppendLine("            A.\"END_DATE\" < (:END_DATE + 1)");
                        sql2.AppendLine("        )");

                        prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

                    }

                }

                //試験車日程種別
                if (string.IsNullOrWhiteSpace(cond.試験車日程種別) == false)
                {
                    if (cond.IsGetKettei)
                    {
                        sql2.AppendLine("    AND (A.\"試験車日程種別\" = :試験車日程種別 OR");
                        sql2.AppendLine("(A.\"試験車日程種別\" = '決定'))");
                    }
                    else
                    {
                        sql2.AppendLine("    AND A.\"試験車日程種別\" = :試験車日程種別");
                    }
                    prms.Add(new BindModel { Name = ":試験車日程種別", Type = OracleDbType.Varchar2, Object = cond.試験車日程種別, Direct = ParameterDirection.Input });
                }

                //行番号
                if (cond.PARALLEL_INDEX_GROUP != null)
                {
                    sql2.AppendLine("    AND A.\"PARALLEL_INDEX_GROUP\" = :PARALLEL_INDEX_GROUP");
                    prms.Add(new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = cond.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input });

                }

                //設定者_ID
                if (cond.設定者_ID != null)
                {
                    sql2.AppendLine("    AND C.\"設定者_ID\" = :設定者_ID");
                    prms.Add(new BindModel { Name = ":設定者_ID", Type = OracleDbType.Varchar2, Object = cond.設定者_ID, Direct = ParameterDirection.Input });
                }
            }

            //ソート順
            sql2.AppendLine("ORDER BY");
            sql2.AppendLine("     A.\"CATEGORY_ID\"");
            sql2.AppendLine("    ,A.\"ID\"");

            //取得
            var sql = sql1.ToString() + sql2.ToString();
            return db.ReadModelList<TestCarScheduleModel>(sql, prms);

        }

        /// <summary>
        /// コピー元試験車スケジュール取得
        /// </summary>
        /// <param name="cond">コピー条件</param>
        /// <returns></returns>
        public List<TestCarScheduleModel> GetCopyTestCarSchedule(TestCarScheduleCopyModel cond)
        {
            //パラメータ
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = cond.GENERAL_CODE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = cond.TARGET_START_DATE.Date, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SOURCE_STATUS", Type = OracleDbType.Varchar2, Object = cond.SOURCE_STATUS, Direct = ParameterDirection.Input }

            };

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"ID\"");
            sql.AppendLine("    ,A.\"GENERAL_CODE\"");
            sql.AppendLine("    ,A.\"START_DATE\"");
            sql.AppendLine("    ,A.\"END_DATE\"");
            sql.AppendLine("    ,A.\"DESCRIPTION\"");
            sql.AppendLine("    ,A.\"SYMBOL\"");
            sql.AppendLine("    ,A.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    ,A.\"INPUT_DATETIME\"");
            sql.AppendLine("    ,A.\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,A.\"CATEGORY_ID\"");
            sql.AppendLine("    ,A.\"予約種別\"");
            sql.AppendLine("    ,A.\"試験車日程種別\"");
            sql.AppendLine("    ,A.\"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("    ,C.\"予約者_ID\"");
            sql.AppendLine("    ,C.\"設定者_ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"TESTCAR_SCHEDULE\" A");
            sql.AppendLine("    LEFT JOIN \"試験計画_試験車日程_設定者\" C");
            sql.AppendLine("    ON A.\"ID\" = C.\"SCHEDULE_ID\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"GENERAL_CODE\" = :GENERAL_CODE");
            sql.AppendLine("    AND");
            sql.AppendLine("        (");
            sql.AppendLine("            :START_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
            sql.AppendLine("            OR");
            sql.AppendLine("            A.\"START_DATE\" >= :START_DATE");
            sql.AppendLine("        )");
            sql.AppendLine("    AND A.\"試験車日程種別\" = :SOURCE_STATUS");

            //対象期間(終了)
            if (cond.TARGET_END_DATE != null)
            {
                var endDate = cond.TARGET_END_DATE.Value.Date;

                sql.AppendLine("    AND");
                sql.AppendLine("        (");
                sql.AppendLine("            :END_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                sql.AppendLine("            OR");
                sql.AppendLine("            A.\"END_DATE\" < (:END_DATE + 1)");
                sql.AppendLine("        )");

                prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

            }

            //コピー先ステータス
            if (string.IsNullOrWhiteSpace(cond.TARGET_STATUS) == false)
            {
                sql.AppendLine("    AND NOT EXISTS");
                sql.AppendLine("                    (");
                sql.AppendLine("                        SELECT");
                sql.AppendLine("                            *");
                sql.AppendLine("                        FROM");
                sql.AppendLine("                            \"TESTCAR_SCHEDULE\" B");
                sql.AppendLine("                        WHERE 0 = 0");
                sql.AppendLine("                            AND B.\"GENERAL_CODE\" = :GENERAL_CODE");
                sql.AppendLine("                            AND B.\"試験車日程種別\" = :TARGET_STATUS");
                sql.AppendLine("                            AND B.\"コピー元_ID\" = A.\"ID\"");
                sql.AppendLine("                    )");

                prms.Add(new BindModel { Name = ":TARGET_STATUS", Type = OracleDbType.Varchar2, Object = cond.TARGET_STATUS, Direct = ParameterDirection.Input });

                //コピー元が使用部署要望案かどうか
                if (cond.SOURCE_STATUS == Youbou)
                {
                    sql.AppendLine("    AND NOT EXISTS");
                    sql.AppendLine("                    (");
                    sql.AppendLine("                        SELECT");
                    sql.AppendLine("                            *");
                    sql.AppendLine("                        FROM");
                    sql.AppendLine("                            \"TESTCAR_SCHEDULE\" B");
                    sql.AppendLine("                        WHERE 0 = 0");
                    sql.AppendLine("                            AND B.\"GENERAL_CODE\" = :GENERAL_CODE");
                    sql.AppendLine("                            AND B.\"試験車日程種別\" IN (:NONE_STATUS1,:NONE_STATUS2)");
                    sql.AppendLine("                            AND B.\"コピー元_ID\" = A.\"ID\"");
                    sql.AppendLine("                    )");

                    prms.Add(new BindModel { Name = ":NONE_STATUS1", Type = OracleDbType.Varchar2, Object = Chousei, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":NONE_STATUS2", Type = OracleDbType.Varchar2, Object = Kettei, Direct = ParameterDirection.Input });

                }

            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"CATEGORY_ID\"");
            sql.AppendLine("    ,A.\"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("    ,A.\"START_DATE\"");
            sql.AppendLine("    ,A.\"END_DATE\"");

            //取得
            return db.ReadModelList<TestCarScheduleModel>(sql.ToString(), prms);

        }

        #endregion

        #region 試験車スケジュール追加
        /// <summary>
        /// 試験車スケジュール追加
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns>追加可否</returns>
        public bool InsertTestCarSchedule(List<TestCarScheduleModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //DEVELOPMENT_SCHEDULEを追加
                results.Add(this.InsertSchedule(list));

                //試験計画_課題フォローリストを追加
                results.Add(this.InsertKadaiFollowList(list));

                //試験計画_試験車日程_完了記録を登録
                results.Add(this.MergeKanryouKiroku(list));

                //試験計画_試験車日程_設定者を登録
                results.Add(this.MergeSetteisya(list));

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
        /// DEVELOPMENT_SCHEDULEを追加
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns>追加可否</returns>
        private bool InsertSchedule(List<TestCarScheduleModel> list)
        {
            var now = DateTime.Now;

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"TESTCAR_SCHEDULE\"");
            sql.AppendLine("( ");
            sql.AppendLine("     \"ID\"");
            sql.AppendLine("    ,\"GENERAL_CODE\"");
            sql.AppendLine("    ,\"START_DATE\"");
            sql.AppendLine("    ,\"END_DATE\"");
            sql.AppendLine("    ,\"DESCRIPTION\"");
            sql.AppendLine("    ,\"SORT_NO\"");
            sql.AppendLine("    ,\"SYMBOL\"");
            sql.AppendLine("    ,\"SECTION_GROUP_ID\"");
            sql.AppendLine("    ,\"FLAG_CLASS\"");
            sql.AppendLine("    ,\"ACHIEVEMENT_INDEX\"");
            sql.AppendLine("    ,\"ENFORCEMENT_INDEX\"");
            sql.AppendLine("    ,\"INPUT_DATETIME\"");
            sql.AppendLine("    ,\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,\"INPUT_LOGIN_ID\"");
            sql.AppendLine("    ,\"CHANGE_DATETIME\"");
            sql.AppendLine("    ,\"CATEGORY_ID\"");
            sql.AppendLine("    ,\"予約種別\"");
            sql.AppendLine("    ,\"試験車日程種別\"");
            sql.AppendLine("    ,\"コピー元_ID\"");
            sql.AppendLine("    ,\"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine(") ");
            sql.AppendLine("SELECT");
            sql.AppendLine("     :ID AS \"ID\"");
            sql.AppendLine("    ,:GENERAL_CODE AS \"GENERAL_CODE\"");
            sql.AppendLine("    ,:START_DATE AS \"START_DATE\"");
            sql.AppendLine("    ,:END_DATE AS \"END_DATE\"");
            sql.AppendLine("    ,:DESCRIPTION AS \"DESCRIPTION\"");
            sql.AppendLine("    ,1 AS \"SORT_NO\"");
            sql.AppendLine("    ,:SYMBOL AS \"SYMBOL\"");
            sql.AppendLine("    ,:SECTION_GROUP_ID AS \"SECTION_GROUP_ID\"");
            sql.AppendLine("    ,'試験車日程' AS \"FLAG_CLASS\"");
            sql.AppendLine("    ,:WORK_COMPLETE AS \"ACHIEVEMENT_INDEX\"");
            sql.AppendLine("    ,:WORK_COMPLETE AS \"ENFORCEMENT_INDEX\"");
            sql.AppendLine("    ,:INPUT_DATETIME AS \"INPUT_DATETIME\"");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID AS \"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID AS \"INPUT_LOGIN_ID\"");
            sql.AppendLine("    ,SYSTIMESTAMP AS \"CHANGE_DATETIME\"");
            sql.AppendLine("    ,:CATEGORY_ID AS \"CATEGORY_ID\"");
            sql.AppendLine("    ,:予約種別 AS \"予約種別\"");
            sql.AppendLine("    ,:試験車日程種別 AS \"試験車日程種別\"");
            sql.AppendLine("    ,:コピー元_ID AS \"コピー元_ID\"");
            sql.AppendLine("    ,:PARALLEL_INDEX_GROUP AS \"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    DUAL");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //スケジュールのID
                schedule.ID = base.GetLogic<CommonLogic>().GetScheduleNewID();

                //作業完了フラグ
                var workComplete = schedule.完了日 != null ? WorkComplete : null;

                //登録日時
                var inputDate = schedule.INPUT_DATETIME ?? now;

                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = schedule.GENERAL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = schedule.START_DATE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = schedule.END_DATE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":DESCRIPTION", Type = OracleDbType.Varchar2, Object = schedule.DESCRIPTION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SYMBOL", Type = OracleDbType.Int16, Object = schedule.SYMBOL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":WORK_COMPLETE", Type = OracleDbType.Int16, Object = workComplete, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_DATETIME", Type = OracleDbType.Date, Object = inputDate, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = schedule.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = schedule.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = schedule.CATEGORY_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約種別", Type = OracleDbType.Varchar2, Object = schedule.予約種別, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験車日程種別", Type = OracleDbType.Varchar2, Object = schedule.試験車日程種別, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":コピー元_ID", Type = OracleDbType.Int64, Object = schedule.コピー元_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = schedule.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input }

                };

                //追加
                results.Add(db.InsertData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 試験車スケジュール更新
        /// <summary>
        /// 試験車スケジュール更新
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns>更新可否</returns>
        public bool UpdateTestCarSchedule(List<TestCarScheduleModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //DEVELOPMENT_SCHEDULEを更新
                results.Add(this.UpdateSchedule(list));

                //試験計画_課題フォローリストを追加
                results.Add(this.InsertKadaiFollowList(list));

                //試験計画_試験車日程_完了記録を登録
                results.Add(this.MergeKanryouKiroku(list));

                //試験計画_試験車日程_設定者を登録
                results.Add(this.MergeSetteisya(list));

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
        /// DEVELOPMENT_SCHEDULEを更新
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns>更新可否</returns>
        private bool UpdateSchedule(List<TestCarScheduleModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"TESTCAR_SCHEDULE\"");
            sql.AppendLine("SET");
            sql.AppendLine("     \"START_DATE\" = :START_DATE");
            sql.AppendLine("    ,\"END_DATE\" = :END_DATE");
            sql.AppendLine("    ,\"DESCRIPTION\" = :DESCRIPTION");
            sql.AppendLine("    ,\"SYMBOL\" = :SYMBOL");
            sql.AppendLine("    ,\"SECTION_GROUP_ID\" = :SECTION_GROUP_ID");
            sql.AppendLine("    ,\"ACHIEVEMENT_INDEX\" = :WORK_COMPLETE");
            sql.AppendLine("    ,\"ENFORCEMENT_INDEX\" = :WORK_COMPLETE");
            sql.AppendLine("    ,\"INPUT_PERSONEL_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,\"INPUT_LOGIN_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,\"CHANGE_DATETIME\" = SYSTIMESTAMP");
            sql.AppendLine("    ,\"PARALLEL_INDEX_GROUP\" = :PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,\"予約種別\" = :予約種別");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :ID");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //作業完了フラグ
                var workComplete = schedule.完了日 != null ? WorkComplete : null;

                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = schedule.START_DATE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = schedule.END_DATE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":DESCRIPTION", Type = OracleDbType.Varchar2, Object = schedule.DESCRIPTION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SYMBOL", Type = OracleDbType.Int16, Object = schedule.SYMBOL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = schedule.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":WORK_COMPLETE", Type = OracleDbType.Int16, Object = workComplete, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = schedule.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = schedule.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約種別", Type = OracleDbType.Varchar2, Object = schedule.予約種別, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input }

                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 試験車スケジュール削除
        /// <summary>
        /// 試験車スケジュール削除
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns>削除可否</returns>
        public bool DeleteTestCarSchedule(List<TestCarScheduleModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //削除対象があるかどうか
            if (list != null && list.Any() == true)
            {
                var idList = list.Select(x => x.ID).ToList();

                //DEVELOPMENT_SCHEDULEを削除
                results.Add(this.DeleteSchedule(idList));

                //試験計画_試験車日程_完了記録を削除
                results.Add(this.DeleteKanryouKiroku(idList));

                //試験計画_試験車日程_設定者を削除
                results.Add(this.DeleteSetteisya(idList));

            }

            //削除が成功したかどうか
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
        /// DEVELOPMENT_SCHEDULEを削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteSchedule(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"TESTCAR_SCHEDULE\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.Append("    AND \"ID\" IN (NULL");

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

        /// <summary>
        /// 試験計画_試験車日程_完了記録を削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteKanryouKiroku(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_試験車日程_完了記録\"");
            sql.AppendLine("WHERE 0 = 0");

            sql.Append("    AND \"SCHEDULE_ID\" IN (NULL");

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

        /// <summary>
        /// 試験計画_試験車日程_設定者を削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteSetteisya(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_試験車日程_設定者\"");
            sql.AppendLine("WHERE 0 = 0");

            sql.Append("    AND \"SCHEDULE_ID\" IN (NULL");

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

        #region 試験車スケジュール項目のソート順更新
        /// <summary>
        /// 試験車スケジュール項目のソート順更新
        /// </summary>
        /// <param name="list">試験車スケジュール項目</param>
        /// <returns>更新可否</returns>
        private bool UpdateTestCarScheduleItemSortNo(List<TestCarScheduleItemModel> list)
        {
            var results = new List<bool>();

            //更新対象の全ての開発符号の並び順を変更
            foreach (var generalCode in list.Where(x => string.IsNullOrWhiteSpace(x.GENERAL_CODE) == false).Select(x => x.GENERAL_CODE).Distinct())
            {
                //更新
                results.Add(base.GetLogic<CommonLogic>().UpdateScheduleItemSortNoByGeneralCode(TableID, generalCode));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 試験計画_外製車日程_車両リストを登録
        /// <summary>
        /// 試験計画_外製車日程_車両リストを登録
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns>更新可否</returns>
        private bool MergeCarList(List<TestCarScheduleItemModel> list)
        {
            //登録対象がない場合はOK
            var entryList = list.Where(x => string.IsNullOrWhiteSpace(x.管理票番号) == false);
            if (entryList.Any() == false)
            {
                return true;

            }

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"試験計画_外製車日程_車両リスト\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             (NVL(MAX(\"ID\"),0) + 1) AS \"ID\"");
            sql.AppendLine("            ,:ID AS \"CATEGORY_ID\"");
            sql.AppendLine("            ,:管理票番号 AS \"管理票番号\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"試験計画_外製車日程_車両リスト\"");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"CATEGORY_ID\" = B.\"CATEGORY_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("        \"管理票番号\" = B.\"管理票番号\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"ID\"");
            sql.AppendLine("        ,\"CATEGORY_ID\"");
            sql.AppendLine("        ,\"管理票番号\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"ID\"");
            sql.AppendLine("        ,B.\"CATEGORY_ID\"");
            sql.AppendLine("        ,B.\"管理票番号\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var schedule in entryList)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理票番号", Type = OracleDbType.Varchar2, Object = schedule.管理票番号, Direct = ParameterDirection.Input }

                };

                //登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 試験計画_試験車日程_完了記録を登録
        /// <summary>
        /// 試験計画_試験車日程_完了記録を登録
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns></returns>
        private bool MergeKanryouKiroku(List<TestCarScheduleModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"試験計画_試験車日程_完了記録\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             (NVL(MAX(\"ID\"),0) + 1) AS \"ID\"");
            sql.AppendLine("            ,:SCHEDULE_ID AS \"SCHEDULE_ID\"");
            sql.AppendLine("            ,:オドメータ AS \"オドメータ\"");
            sql.AppendLine("            ,:試験内容 AS \"試験内容\"");
            sql.AppendLine("            ,:脱着部品 AS \"脱着部品\"");
            sql.AppendLine("            ,:変更箇所 AS \"変更箇所\"");
            sql.AppendLine("            ,:車両保管場所 AS \"車両保管場所\"");
            sql.AppendLine("            ,:FOLLOWLIST_ID AS \"FOLLOWLIST_ID\"");
            sql.AppendLine("            ,:完了日 AS \"完了日\"");
            sql.AppendLine("            ,:鍵保管場所 AS \"鍵保管場所\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"試験計画_試験車日程_完了記録\"");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"SCHEDULE_ID\" = B.\"SCHEDULE_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("         \"オドメータ\" = B.\"オドメータ\"");
            sql.AppendLine("        ,\"試験内容\" = B.\"試験内容\"");
            sql.AppendLine("        ,\"脱着部品\" = B.\"脱着部品\"");
            sql.AppendLine("        ,\"変更箇所\" = B.\"変更箇所\"");
            sql.AppendLine("        ,\"車両保管場所\" = B.\"車両保管場所\"");
            sql.AppendLine("        ,\"FOLLOWLIST_ID\" = B.\"FOLLOWLIST_ID\"");
            sql.AppendLine("        ,\"完了日\" = B.\"完了日\"");
            sql.AppendLine("        ,\"鍵保管場所\" = B.\"鍵保管場所\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"ID\"");
            sql.AppendLine("        ,\"SCHEDULE_ID\"");
            sql.AppendLine("        ,\"オドメータ\"");
            sql.AppendLine("        ,\"試験内容\"");
            sql.AppendLine("        ,\"脱着部品\"");
            sql.AppendLine("        ,\"変更箇所\"");
            sql.AppendLine("        ,\"車両保管場所\"");
            sql.AppendLine("        ,\"FOLLOWLIST_ID\"");
            sql.AppendLine("        ,\"完了日\"");
            sql.AppendLine("        ,\"鍵保管場所\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"ID\"");
            sql.AppendLine("        ,B.\"SCHEDULE_ID\"");
            sql.AppendLine("        ,B.\"オドメータ\"");
            sql.AppendLine("        ,B.\"試験内容\"");
            sql.AppendLine("        ,B.\"脱着部品\"");
            sql.AppendLine("        ,B.\"変更箇所\"");
            sql.AppendLine("        ,B.\"車両保管場所\"");
            sql.AppendLine("        ,B.\"FOLLOWLIST_ID\"");
            sql.AppendLine("        ,B.\"完了日\"");
            sql.AppendLine("        ,B.\"鍵保管場所\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":SCHEDULE_ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":オドメータ", Type = OracleDbType.Varchar2, Object = schedule.オドメータ, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験内容", Type = OracleDbType.Varchar2, Object = schedule.試験内容, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":脱着部品", Type = OracleDbType.Varchar2, Object = schedule.脱着部品, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":変更箇所", Type = OracleDbType.Varchar2, Object = schedule.変更箇所, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":車両保管場所", Type = OracleDbType.Varchar2, Object = schedule.車両保管場所, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FOLLOWLIST_ID", Type = OracleDbType.Decimal, Object = schedule.FOLLOWLIST_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":完了日", Type = OracleDbType.Date, Object = schedule.完了日, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":鍵保管場所", Type = OracleDbType.Varchar2, Object = schedule.鍵保管場所, Direct = ParameterDirection.Input }

                };

                //登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);
        }
        #endregion

        #region 試験計画_試験車日程_設定者を登録
        /// <summary>
        /// 試験計画_試験車日程_設定者を登録
        /// </summary>
        /// <param name="list">試験車スケジュール</param>
        /// <returns></returns>
        private bool MergeSetteisya(List<TestCarScheduleModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"試験計画_試験車日程_設定者\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             (NVL(MAX(\"ID\"),0) + 1) AS \"ID\"");
            sql.AppendLine("            ,:SCHEDULE_ID AS \"SCHEDULE_ID\"");
            sql.AppendLine("            ,:設定者_ID AS \"設定者_ID\"");
            sql.AppendLine("            ,:予約者_ID AS \"予約者_ID\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"試験計画_試験車日程_設定者\"");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"SCHEDULE_ID\" = B.\"SCHEDULE_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("         \"設定者_ID\" = B.\"設定者_ID\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"ID\"");
            sql.AppendLine("        ,\"SCHEDULE_ID\"");
            sql.AppendLine("        ,\"設定者_ID\"");
            sql.AppendLine("        ,\"予約者_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"ID\"");
            sql.AppendLine("        ,B.\"SCHEDULE_ID\"");
            sql.AppendLine("        ,B.\"設定者_ID\"");
            sql.AppendLine("        ,B.\"予約者_ID\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":SCHEDULE_ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":設定者_ID", Type = OracleDbType.Varchar2, Object = schedule.設定者_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約者_ID", Type = OracleDbType.Varchar2, Object = schedule.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input }

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
        /// <param name="list">試験車スケジュール</param>
        /// <returns>追加可否</returns>
        private bool InsertKadaiFollowList(List<TestCarScheduleModel> list)
        {
            const string SaisyuuTyouseiKekka = "決定";

            //作業履歴の作成対象がない場合は終了
            var target = list.Where(x => x.試験車日程種別 == SaisyuuTyouseiKekka && x.完了日 != null && x.FOLLOWLIST_ID == null).ToArray();
            if (target.Any() == false)
            {
                return true;

            }

            var results = new List<bool>();

            //試験車日程種別が最終調整結果で完了日が入力済で課題フォローリストIDがNULLの場合は作業履歴に追加
            foreach (var x in target)
            {
                //試験計画_課題フォローリストのID取得
                x.FOLLOWLIST_ID = base.GetLogic<WorkHistoryLogic>().GetTestPlaTaskFollowListID();

                //作業履歴の文言組み立て
                var msg = new StringBuilder();
                msg.AppendFormat("【{0}】{1: yyyy/MM/dd HH}時 ～ {2: yyyy/MM/dd HH}時", x.DESCRIPTION, x.START_DATE, x.END_DATE).AppendLine();
                msg.AppendFormat("ODO:{0}", x.オドメータ).AppendLine();
                msg.AppendFormat("試験内容：{0}", x.試験内容).AppendLine();
                msg.AppendFormat("脱着部品：{0}", x.脱着部品).AppendLine();
                msg.AppendFormat("変更箇所：{0}", x.変更箇所).AppendLine();
                msg.AppendFormat("車両保管場所：{0}", x.車両保管場所).AppendLine();
                msg.AppendFormat("鍵保管場所：{0}", x.鍵保管場所);

                //試験計画_課題フォローリスト追加
                results.Add(base.GetLogic<WorkHistoryLogic>().InsertTestPlaTaskFollowList(new WorkHistoryModel
                {
                    //ID
                    ID = x.FOLLOWLIST_ID.Value,

                    //カテゴリーID
                    CATEGORY_ID = x.CATEGORY_ID,

                    //作業履歴
                    CURRENT_SITUATION = msg.ToString(),

                    //入力者パーソナルID
                    INPUT_PERSONEL_ID = x.INPUT_PERSONEL_ID,

                    //OPEN/CLOSE
                    OPEN_CLOSE = Const.Open,

                    //日付
                    LISTED_DATE = x.完了日,

                    //ソート順
                    SORT_NO = 1

                }));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 試験車検索データの取得
        /// <summary>
        /// 試験車データの取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public IEnumerable<TestCarSearchOutModel> GetTestCarSearch(TestCarSearchInModel cond)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.管理票NO");
            sql.AppendLine("    ,F.CAR_GROUP AS 車系");
            sql.AppendLine("    ,A.駐車場番号");
            sql.AppendLine("    ,A.車型");
            sql.AppendLine("    ,A.リース満了日");
            sql.AppendLine("    ,A.型式符号");
            sql.AppendLine("    ,A.リースNO");
            sql.AppendLine("    ,A.メモ");
            sql.AppendLine("    ,B.開発符号");
            sql.AppendLine("    ,B.試作時期");
            sql.AppendLine("    ,B.号車");
            sql.AppendLine("    ,B.仕向地");
            sql.AppendLine("    ,B.排気量");
            sql.AppendLine("    ,B.E_G型式");
            sql.AppendLine("    ,B.駆動方式");
            sql.AppendLine("    ,B.トランスミッション");
            sql.AppendLine("    ,B.車体色");
            sql.AppendLine("    ,B.登録ナンバー");
            sql.AppendLine("    ,B.FLAG_ナビ付");
            sql.AppendLine("    ,CASE B.FLAG_ナビ付 WHEN 0 THEN '無' WHEN 1 THEN '有' ELSE '' END ナビ");
            sql.AppendLine("    ,B.FLAG_ETC付");
            sql.AppendLine("    ,CASE B.FLAG_ETC付 WHEN 0 THEN '無' WHEN 1 THEN '有' ELSE '' END ETC");
            sql.AppendLine("    ,B.グレード");
            sql.AppendLine("    ,B.メーカー名");
            sql.AppendLine("    ,B.外製車名");
            sql.AppendLine("    ,B.名称備考");
            sql.AppendLine("    ,B.登録ナンバー");
            sql.AppendLine("    ,B.廃艦日");
            sql.AppendLine("    ,B.試験目的");
            sql.AppendLine("    ,B.車体番号");
            sql.AppendLine("    ,B.E_G番号");
            sql.AppendLine("    ,B.固定資産NO");
            sql.AppendLine("    ,B.研命ナンバー");
            sql.AppendLine("    ,B.研命期間");
            sql.AppendLine("    ,B.車検登録日");
            sql.AppendLine("    ,B.車検期限");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("            WHEN B.MONTH_DIFF = 0 THEN (B.車検期限 - B.TODAY) || '日'");
            sql.AppendLine("            WHEN B.MONTH_DIFF > 0 THEN '約' || B.MONTH_DIFF || 'ヶ月'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 車検期限まで残り");
            sql.AppendLine("    ,D.SECTION_CODE AS 課");
            sql.AppendLine("    ,G.処分予定年月");
            sql.AppendLine("    ,C.SECTION_GROUP_CODE AS 担当");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE LOWER(E.ESTABLISHMENT)");
            sql.AppendLine("            WHEN 'g' THEN '群馬'");
            sql.AppendLine("            WHEN 't' THEN '東京'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS ESTABLISHMENT");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("            WHEN D.SECTION_ID = '367' THEN 'カーシェア'");
            sql.AppendLine("            WHEN B.名称備考 LIKE '%占有車%' THEN '専用車'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 分類");
            sql.AppendLine("FROM ");
            sql.AppendLine("    VIEW_試験車基本情報 A");
            sql.AppendLine("    INNER JOIN");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         A.データID");
            sql.AppendLine("                        ,A.開発符号");
            sql.AppendLine("                        ,A.試作時期");
            sql.AppendLine("                        ,A.号車");
            sql.AppendLine("                        ,A.仕向地");
            sql.AppendLine("                        ,A.排気量");
            sql.AppendLine("                        ,A.E_G型式");
            sql.AppendLine("                        ,A.駆動方式");
            sql.AppendLine("                        ,A.トランスミッション");
            sql.AppendLine("                        ,A.車体色");
            sql.AppendLine("                        ,A.登録ナンバー");
            sql.AppendLine("                        ,A.FLAG_ナビ付");
            sql.AppendLine("                        ,A.FLAG_ETC付");
            sql.AppendLine("                        ,A.グレード");
            sql.AppendLine("                        ,A.管理責任部署");
            sql.AppendLine("                        ,A.名称備考");
            sql.AppendLine("                        ,A.メーカー名");
            sql.AppendLine("                        ,A.外製車名");
            sql.AppendLine("                        ,A.廃艦日");
            sql.AppendLine("                        ,A.試験目的");
            sql.AppendLine("                        ,A.車体番号");
            sql.AppendLine("                        ,A.E_G番号");
            sql.AppendLine("                        ,A.固定資産NO");
            sql.AppendLine("                        ,A.研命ナンバー");
            sql.AppendLine("                        ,A.研命期間");
            sql.AppendLine("                        ,A.車検登録日");
            sql.AppendLine("                        ,A.車検期限");
            sql.AppendLine("                        ,TRUNC(SYSDATE) AS TODAY");
            sql.AppendLine("                        ,TRUNC(MONTHS_BETWEEN(TRUNC(A.車検期限),TRUNC(SYSDATE))) AS MONTH_DIFF");
            sql.AppendLine("                        ,A.受領者");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        試験車履歴情報 A");
            sql.AppendLine("                        INNER JOIN");
            sql.AppendLine("                                    (");
            sql.AppendLine("                                        SELECT");
            sql.AppendLine("                                             A.データID");
            sql.AppendLine("                                            ,MAX(A.履歴NO) AS 履歴NO");
            sql.AppendLine("                                        FROM");
            sql.AppendLine("                                            試験車履歴情報 A");
            sql.AppendLine("                                        WHERE 0 = 0");
            sql.AppendLine("                                        GROUP BY");
            sql.AppendLine("                                            A.データID");
            sql.AppendLine("                                    ) B");
            sql.AppendLine("                        ON A.データID = B.データID");
            sql.AppendLine("                        AND A.履歴NO = B.履歴NO");
            sql.AppendLine("                    WHERE 0 = 0");
            sql.AppendLine("                ) B");
            sql.AppendLine("    ON A.データID = B.データID");
            sql.AppendLine("    INNER JOIN SECTION_GROUP_DATA C");
            sql.AppendLine("    ON B.管理責任部署 = C.SECTION_GROUP_ID");
            sql.AppendLine("    INNER JOIN SECTION_DATA D");
            sql.AppendLine("    ON C.SECTION_ID = D.SECTION_ID");
            sql.AppendLine("    INNER JOIN DEPARTMENT_DATA E");
            sql.AppendLine("    ON D.DEPARTMENT_ID = E.DEPARTMENT_ID");
            sql.AppendLine("    LEFT JOIN GENERAL_CODE F");
            sql.AppendLine("    ON B.開発符号 = F.GENERAL_CODE");
            sql.AppendLine("    LEFT JOIN 固定資産情報 G");
            sql.AppendLine("    ON A.データID = G.データID");
            //Append Start 2022/03/03 杉浦 試験車日程の車も登録する
            sql.AppendLine("    LEFT JOIN ");
            sql.AppendLine("    (");
            sql.AppendLine("         SELECT");
            sql.AppendLine("             CASE WHEN TESTCAR_ITEM_ID IS NOT NULL AND OUTERCAR_ITEM_ID IS NOT NULL AND CARSHARING_ITEM_ID IS NOT NULL THEN 1 ELSE NULL END SIGN");
            sql.AppendLine("             , 管理票番号");
            sql.AppendLine("         FROM");
            sql.AppendLine("            ( ");
            sql.AppendLine("                SELECT");
            sql.AppendLine("                      A.管理票番号");
            sql.AppendLine("                    , MAX(CASE D.SEQ WHEN 1 THEN D.ID ELSE NULL END) TESTCAR_ITEM_ID");
            sql.AppendLine("                    , MAX(CASE D.SEQ WHEN 1 THEN D.CATEGORY_ID ELSE NULL END) TESTCAR_CATEGORY_ID");
            sql.AppendLine("                    , MAX(CASE D.SEQ WHEN 2 THEN D.ID ELSE NULL END) OUTERCAR_ITEM_ID");
            sql.AppendLine("                    , MAX( CASE D.SEQ  WHEN 2 THEN D.CATEGORY_ID ELSE NULL END) OUTERCAR_CATEGORY_ID");
            sql.AppendLine("                    , MAX(CASE D.SEQ WHEN 3 THEN D.ID ELSE NULL END) CARSHARING_ITEM_ID");
            sql.AppendLine("                    , MAX( CASE D.SEQ WHEN 3 THEN D.CATEGORY_ID ELSE NULL END) CARSHARING_CATEGORY_ID ");
            sql.AppendLine("                FROM");
            sql.AppendLine("                    SCHEDULE_CAR A ");
            sql.AppendLine("                    LEFT JOIN ( ");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                              D.* ");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            SCHEDULE_CAR A ");
            sql.AppendLine("                            LEFT JOIN ( SELECT 1 SEQ, ID, CATEGORY_ID FROM TESTCAR_SCHEDULE_ITEM) D ");
            sql.AppendLine("                                ON A.CATEGORY_ID = D.ID AND D.ID IS NOT NULL ");
            sql.AppendLine("                        UNION ALL ");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                              E.* ");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            SCHEDULE_CAR A ");
            sql.AppendLine("                             LEFT JOIN ( SELECT 2 SEQ, ID, CATEGORY_ID FROM OUTERCAR_SCHEDULE_ITEM) E ");
            sql.AppendLine("                                ON A.CATEGORY_ID = E.ID AND E.ID IS NOT NULL ");
            sql.AppendLine("                        UNION ALL ");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                              F.* ");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            SCHEDULE_CAR A ");
            sql.AppendLine("                             LEFT JOIN ( SELECT 3 SEQ, ID, CATEGORY_ID FROM CARSHARING_SCHEDULE_ITEM) F ");
            sql.AppendLine("                                ON A.CATEGORY_ID = F.ID AND F.ID IS NOT NULL ");
            sql.AppendLine("                    )D ");
            sql.AppendLine("                        ON A.CATEGORY_ID = D.ID AND D.ID IS NOT NULL ");
            sql.AppendLine("                    GROUP BY A.管理票番号 ");
            sql.AppendLine("             )CAR ");
            sql.AppendLine("            LEFT JOIN 試験計画_外製車日程_最終予約日 ");
            sql.AppendLine("              ON ( ");
            sql.AppendLine("                 CAR.TESTCAR_CATEGORY_ID = 試験計画_外製車日程_最終予約日.CATEGORY_ID OR CAR.OUTERCAR_CATEGORY_ID = 試験計画_外製車日程_最終予約日.CATEGORY_ID OR CAR.CARSHARING_CATEGORY_ID = 試験計画_外製車日程_最終予約日.CATEGORY_ID");
            sql.AppendLine("         ) ");
            sql.AppendLine("         WHERE 0 = 0");
            sql.AppendLine("             AND CAR.管理票番号 IS NOT NULL");
            sql.AppendLine("             AND (試験計画_外製車日程_最終予約日.最終予約可能日 > TRUNC(SYSDATE) - 1 ");
            sql.AppendLine("             OR 試験計画_外製車日程_最終予約日.最終予約可能日 IS NULL) ");
            sql.AppendLine("                    ) L");
            sql.AppendLine("     ON L.管理票番号 = A.管理票NO");
            //Append End 2022/03/03 杉浦 試験車日程の車も登録する
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.廃却決済承認年月 IS NULL");
            sql.AppendLine("    AND A.車両搬出日 IS NULL");
            //スケジュールに登録されてる車両は除外
            //Update Start 2022/03/03 杉浦 試験車日程の車も登録する
            //sql.AppendLine("    AND NOT EXISTS");
            //sql.AppendLine("                    (");
            //sql.AppendLine("                        SELECT");
            //sql.AppendLine("                            *");
            //sql.AppendLine("                        FROM");
            //sql.AppendLine("                            SCHEDULE_CAR CAR");
            //sql.AppendLine("                        WHERE 0 = 0");
            //sql.AppendLine("                            AND CAR.管理票番号 IS NOT NULL");
            //sql.AppendLine("                            AND CAR.管理票番号 = A.管理票NO");
            //sql.AppendLine("                    )");
            sql.AppendLine("    AND L.SIGN IS NULL");
            //Update Start 2022/03/03 杉浦 試験車日程の車も登録する
            //試験車日程に登録されてる車両は除外
            /*
            sql.AppendLine("                    (");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                            *");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            TESTCAR_SCHEDULE_ITEM A");
            sql.AppendLine("                            INNER JOIN SCHEDULE_CAR B");
            sql.AppendLine("                            INNER JOIN 試験計画_外製車日程_車両リスト B");
            sql.AppendLine("                            ON A.ID = B.CATEGORY_ID");
            sql.AppendLine("                        WHERE 0 = 0");
            sql.AppendLine("                            AND B.管理票番号 = A.管理票NO");
            sql.AppendLine("                    )");
            */
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     E.ESTABLISHMENT NULLS FIRST");
            sql.AppendLine("    ,F.CAR_GROUP NULLS FIRST");
            sql.AppendLine("    ,B.開発符号  NULLS FIRST");
            sql.AppendLine("    ,B.試作時期  NULLS FIRST");
            sql.AppendLine("    ,B.号車  NULLS FIRST");

            var prms = new List<BindModel>();

            return db.ReadModelList<TestCarSearchOutModel>(sql.ToString(), prms);

        }
        #endregion

        #region 試験車スケジュールコピー
        /// <summary>
        /// 試験車スケジュールコピー
        /// </summary>
        /// <param name="cond">コピー条件</param>
        /// <returns></returns>
        public MessageType CopyTestCarSchedule(TestCarScheduleCopyModel cond)
        {
            var results = new List<bool>();

            var flg = false;

            //トランザクション開始
            db.Begin();

            try
            {
                //コピー元のデータがあるかどうか
                var scheduleList = this.GetCopyTestCarSchedule(cond);
                if (scheduleList.Any() == false)
                {
                    return MessageType.KKE03002;

                }

                //更新するスケジュール項目があれば更新
                var updateItemList = this.SetTestCarScheduleRowNo(cond, scheduleList);
                if (updateItemList.Any() == true)
                {
                    //DEVELOPMENT_SCHEDULEを更新
                    results.Add(this.UpdateScheduleItem(updateItemList));

                }

                //DEVELOPMENT_SCHEDULEを追加
                results.Add(this.InsertSchedule(scheduleList));

                //試験計画_試験車日程_設定者を登録
                scheduleList.ForEach(x => x.INPUT_PERSONEL_ID = x.予約者_ID);
                results.Add(this.MergeSetteisya(scheduleList));

                //処理が全て成功したかどうか
                flg = results.All(x => x == true);

            }
            finally
            {
                //登録が成功したかどうか
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

            }

            return flg == true ? MessageType.None : MessageType.KKE03001;

        }

        /// <summary>
        /// スケジュールの行番号を設定
        /// </summary>
        /// <param name="cond">コピー条件</param>
        /// <param name="copyScheduleList">コピー元スケジュール</param>
        /// <returns></returns>
        private List<TestCarScheduleItemModel> SetTestCarScheduleRowNo(TestCarScheduleCopyModel cond, List<TestCarScheduleModel> copyScheduleList)
        {
            var idList = copyScheduleList.Select(x => x.CATEGORY_ID).Distinct().ToArray();

            var itemList = this.GetTestCarScheduleItem(new TestCarScheduleItemSearchModel { GENERAL_CODE = cond.GENERAL_CODE, ID = idList });
            
            var minDate = copyScheduleList.Min(x => x.START_DATE);
            var scheduleList = this.GetCopyTestCarSchedule(new TestCarScheduleCopyModel { GENERAL_CODE = cond.GENERAL_CODE, TARGET_START_DATE = minDate.Value, TARGET_END_DATE = cond.TARGET_END_DATE, SOURCE_STATUS = cond.TARGET_STATUS });

            var updateItemList = new List<TestCarScheduleItemModel>();

            foreach (var item in itemList)
            {
                var target = scheduleList.Where(x => x.CATEGORY_ID == item.ID).ToList();

                var copyList = copyScheduleList.Where(x => x.CATEGORY_ID == item.ID).ToArray();

                Func<int?, DateTime, DateTime, bool> isScheduleEntry = (row, startDay, endDay) =>
                !target.Any(
                    x => x.PARALLEL_INDEX_GROUP == row &&
                    (
                        (x.START_DATE.Value.Date <= startDay && startDay <= x.END_DATE.Value.Date) ||
                        (x.START_DATE.Value.Date <= endDay && endDay <= x.END_DATE.Value.Date) ||
                        (startDay <= x.START_DATE.Value.Date && endDay >= x.START_DATE.Value.Date) ||
                        (startDay <= x.END_DATE.Value.Date && endDay >= x.END_DATE.Value.Date)
                    )
                );

                foreach (var schedule in copyList)
                {
                    var start = schedule.START_DATE.Value.Date;
                    var end = schedule.END_DATE.Value.Date;

                    //試験車日程種別
                    schedule.試験車日程種別 = cond.TARGET_STATUS;

                    //コピー元_ID
                    schedule.コピー元_ID = schedule.ID;

                    //同じ行に重複したスケジュールがあるかどうか
                    if (isScheduleEntry(schedule.PARALLEL_INDEX_GROUP, start, end) == false)
                    {
                        var isRowAdd = true;

                        var maxParallelIndex = target.Select(x => x.PARALLEL_INDEX_GROUP).Max();

                        for (var row = 1; row <= maxParallelIndex; row++)
                        {
                            //同じ行に重複したスケジュールがあるかどうか
                            if (isScheduleEntry(row, start, end) == true)
                            {
                                //行番号
                                schedule.PARALLEL_INDEX_GROUP = row;

                                isRowAdd = false;
                                break;

                            }

                        }

                        //行追加が必要ならスケジュール項目の行を追加
                        if (isRowAdd == true)
                        {
                            //行数
                            item.PARALLEL_INDEX_GROUP++;

                            //最大行（なので行追加仕様変更により↑は未使用）
                            maxParallelIndex += 1;

                            //行番号
                            schedule.PARALLEL_INDEX_GROUP = maxParallelIndex;

                            //更新対象に未追加なら追加
                            if (updateItemList.Any(x => x.ID == item.ID) == false)
                            {
                                updateItemList.Add(item);

                            }

                        }

                    }

                    //コピー元のスケジュールを追加
                    target.Add(schedule);

                }

            }

            return updateItemList;

        }
        #endregion

        /// <summary>
        /// 指定された期間が含まれているスケジュール一覧抽出処理。
        /// </summary>
        /// <remarks>
        /// 指定された期間が含まれているスケジュールを抽出します。
        /// </remarks>
        /// <param name="generalCode">検索対象開発符号</param>
        /// <param name="categoryId">検索対象カテゴリID</param>
        /// <param name="type">検索対象試験車日程種別</param>
        /// <param name="startDate">検索対象開始日時</param>
        /// <param name="endDate">検索対象終了日時</param>
        /// <param name="rowNo">検索対象行番号</param>
        /// <returns></returns>
        public List<TestCarScheduleModel> GetMaxScopeSchedule(string generalCode, long categoryId, string type, DateTime startDate, DateTime endDate, int rowNo)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    ID");
            sql.AppendLine(",   PARALLEL_INDEX_GROUP");
            sql.AppendLine("FROM ");
            sql.AppendLine("    TESTCAR_SCHEDULE");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND");
            sql.AppendLine("    GENERAL_CODE = :GENERAL_CODE");
            sql.AppendLine("    AND");
            sql.AppendLine("    CATEGORY_ID = :CATEGORY_ID");
            sql.AppendLine("    AND");
            sql.AppendLine("    FLAG_CLASS = '試験車日程'");
            sql.AppendLine("    AND");
            sql.AppendLine("    試験車日程種別 = :試験車日程種別");
            sql.AppendLine("    AND");
            sql.AppendLine("    PARALLEL_INDEX_GROUP >= :PARALLEL_INDEX_GROUP");
            sql.AppendLine("    AND");
            sql.AppendLine("    (");
            sql.AppendLine("        (");
            sql.AppendLine("            START_DATE <= :START_DATE AND END_DATE >= :START_DATE");
            sql.AppendLine("        ) OR (");
            sql.AppendLine("            START_DATE <= :END_DATE AND END_DATE >= :END_DATE");
            sql.AppendLine("        ) OR (");
            sql.AppendLine("            :START_DATE <= START_DATE AND :END_DATE >= START_DATE");
            sql.AppendLine("        ) OR (");
            sql.AppendLine("            :START_DATE <= END_DATE AND :END_DATE >= END_DATE");
            sql.AppendLine("        )");
            sql.AppendLine("    )");

            var prms = new List<BindModel>
            {
                new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = generalCode, Direct = ParameterDirection.Input },
                new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Varchar2, Object = categoryId, Direct = ParameterDirection.Input },
                new BindModel { Name = ":試験車日程種別", Type = OracleDbType.Varchar2, Object = type, Direct = ParameterDirection.Input },
                new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input },
                new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = rowNo, Direct = ParameterDirection.Input }
            };

            return db.ReadModelList<TestCarScheduleModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// 本予約コピーが既に行われているか確認を行う。
        /// </summary>
        /// <remarks>
        /// 本予約コピーが既に行われているか確認を行います。
        /// </remarks>
        /// <param name="generalCode">GENERAL_CODE</param>
        /// <param name="id">スケジュールID</param>
        /// <param name="categoryId">CategoryID</param>
        /// <returns>既に行われている場合はTrue</returns>
        public bool IsExistKetteiCopy(string generalCode, long id, long? categoryId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    ID");
            sql.AppendLine("FROM ");
            sql.AppendLine("    TESTCAR_SCHEDULE");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND");
            sql.AppendLine("    GENERAL_CODE = :GENERAL_CODE");
            sql.AppendLine("    AND");
            sql.AppendLine("    FLAG_CLASS = '試験車日程'");
            sql.AppendLine("    AND");
            sql.AppendLine("    試験車日程種別 = '決定'");
            sql.AppendLine("    AND");
            sql.AppendLine("    コピー元_ID = :ID");

            var prms = new List<BindModel>
            {
                new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = generalCode, Direct = ParameterDirection.Input },
                new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = id, Direct = ParameterDirection.Input }
            };

            if(categoryId != null)
            {
                sql.AppendLine("    AND");
                sql.AppendLine("    CATEGORY_ID = :CATEGORY_ID");

                prms.Add(new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = categoryId, Direct = ParameterDirection.Input });
            }

            var ret = db.ReadModelList<TestCarScheduleModel>(sql.ToString(), prms);

            return (ret.Count != 0);
        }

        /// <summary>
        /// 試験車日程スケジュール作業簡易入力一覧取得
        /// </summary>
        public List<TestCarCompleteScheduleModel> GetTestCarCompleteSchedule(TestCarCompleteScheduleSearchModel cond)
        {
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":FROM_DATE", Type = OracleDbType.Date, Object = cond.FromDate, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SYS_DATE", Type = OracleDbType.Date, Object = DateTime.Today, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = cond.PERSONEL_ID, Direct = ParameterDirection.Input }
            };

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("  F.CATEGORY_ID");
            sql.AppendLine(", F.CATEGORY");
            sql.AppendLine(", A.START_DATE");
            sql.AppendLine(", A.END_DATE");
            sql.AppendLine(", A.DESCRIPTION");
            sql.AppendLine(", G.GENERAL_CODE");
            sql.AppendLine(", G.CAR_GROUP");
            sql.AppendLine(", A.ID");
            sql.AppendLine(", B.予約者_ID");
            sql.AppendLine(", E.SECTION_CODE 予約者_SECTION_CODE");
            sql.AppendLine(", C.NAME 予約者_NAME");
            sql.AppendLine("FROM");
            sql.AppendLine("  TESTCAR_SCHEDULE A ");
            sql.AppendLine("  LEFT JOIN 試験計画_試験車日程_設定者 B ");
            sql.AppendLine("    ON A.ID = B.SCHEDULE_ID ");
            sql.AppendLine("  LEFT JOIN PERSONEL_LIST C ");
            sql.AppendLine("    ON B.予約者_ID = C.PERSONEL_ID ");
            sql.AppendLine("  LEFT JOIN SECTION_GROUP_DATA D ");
            sql.AppendLine("    ON C.SECTION_GROUP_ID = D.SECTION_GROUP_ID ");
            sql.AppendLine("  LEFT JOIN SECTION_DATA E ");
            sql.AppendLine("    ON D.SECTION_ID = E.SECTION_ID ");
            sql.AppendLine("  LEFT JOIN TESTCAR_SCHEDULE_ITEM F ");
            sql.AppendLine("    ON A.CATEGORY_ID = F.CATEGORY_ID ");
            sql.AppendLine("  LEFT JOIN GENERAL_CODE G ");
            sql.AppendLine("    ON A.GENERAL_CODE = G.GENERAL_CODE ");
            sql.AppendLine("{0}");
            sql.AppendLine("  LEFT JOIN 試験計画_試験車日程_完了記録 H ");
            sql.AppendLine("    ON A.ID = H.SCHEDULE_ID ");
            sql.AppendLine("WHERE");
            sql.AppendLine("  A.試験車日程種別 = '決定'");
            sql.AppendLine("  AND H.完了日 IS NULL ");

            if (cond.設定者_ID != null)
            {
                sql.AppendLine("  AND B.\"設定者_ID\" = :設定者_ID");
                prms.Add(new BindModel { Name = ":設定者_ID", Type = OracleDbType.Varchar2, Object = cond.設定者_ID, Direct = ParameterDirection.Input });
            }

            if (cond.予約者_ID != null)
            {
                sql.AppendLine("  AND B.\"予約者_ID\" = :予約者_ID");
                prms.Add(new BindModel { Name = ":予約者_ID", Type = OracleDbType.Varchar2, Object = cond.予約者_ID, Direct = ParameterDirection.Input });
            }

            sql.AppendLine("{1}");
            sql.AppendLine("  AND A.END_DATE > :FROM_DATE ");
            sql.AppendLine("  AND A.END_DATE < :SYS_DATE ");
            sql.AppendLine("  {2} ");
            sql.AppendLine("ORDER BY G.SORT_NUMBER, A.START_DATE");

            var generalCodePeriodSql1 = new StringBuilder();
            var generalCodePeriodSql2 = new StringBuilder();
            if (cond.GeneralCodeFlg == false)
            {
                generalCodePeriodSql1.AppendLine("LEFT JOIN 試験計画_他部署閲覧許可 ");
                generalCodePeriodSql1.AppendLine(" ON A.GENERAL_CODE = 試験計画_他部署閲覧許可.GENERAL_CODE  ");

                generalCodePeriodSql2.AppendLine("AND 試験計画_他部署閲覧許可.PERSONEL_ID = :PERSONEL_ID");
                generalCodePeriodSql2.AppendLine("AND 試験計画_他部署閲覧許可.PERMISSION_PERIOD_START <= TRUNC(SYSDATE)");
                generalCodePeriodSql2.AppendLine("AND 試験計画_他部署閲覧許可.PERMISSION_PERIOD_END >= TRUNC(SYSDATE)");                
            }

            var generalCodeSql = new StringBuilder();
            if (cond.GENERAL_CODE != null)
            {
                generalCodeSql.AppendLine("AND A.GENERAL_CODE = :GENERAL_CODE");
                prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = cond.GENERAL_CODE, Direct = ParameterDirection.Input });
            }

            return db.ReadModelList<TestCarCompleteScheduleModel>(
                string.Format(sql.ToString(), generalCodePeriodSql1.ToString(), generalCodePeriodSql2.ToString(), generalCodeSql.ToString()), prms);
        }
    }
}