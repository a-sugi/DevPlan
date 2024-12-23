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
    /// カーシェア業務ロジッククラス
    /// </summary>
    public class CarShareLogic : BaseLogic
    {
        #region メンバ変数
        private const int TableID = 2;
        #endregion

        #region カーシェアスケジュール項目取得
        /// <summary>
        /// カーシェアスケジュール項目取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<CarShareScheduleItemModel> GetCarShareScheduleItem(CarShareScheduleItemSearchModel cond)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"ID\"");
            sql.AppendLine("    ,J.\"CAR_GROUP\"");
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
            sql.AppendLine("    ,B.\"FLAG_要予約許可\"");
            sql.AppendLine("    ,C.\"駐車場番号\"");
            sql.AppendLine("    ,K.\"最終予約可能日\"");
            sql.AppendLine("    ,G.\"FLAG_ナビ付\"");
            sql.AppendLine("    ,G.\"FLAG_ETC付\"");
            sql.AppendLine("    ,L.\"INPUT_DEPARTMENT_ID\"");
            sql.AppendLine("    ,L.\"INPUT_SECTION_ID\"");
            sql.AppendLine("    ,L.\"INPUT_SECTION_GROUP_ID\"");
            sql.AppendLine("    ,CASE ");
            sql.AppendLine("      WHEN X.物品コード IS NOT NULL THEN 'GPS搭載'");
            sql.AppendLine("      ELSE NULL");
            sql.AppendLine("     END AS XEYE_EXIST ");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"CARSHARING_SCHEDULE_ITEM\" A");
            sql.AppendLine("    LEFT JOIN (SELECT DISTINCT CATEGORY_ID,管理票番号,FLAG_要予約許可 FROM 試験計画_外製車日程_車両リスト) B");
            sql.AppendLine("    ON A.\"ID\" = B.\"CATEGORY_ID\"");
            sql.AppendLine("    LEFT JOIN \"VIEW_試験車基本情報\" C");
            sql.AppendLine("    ON B.\"管理票番号\" = C.\"管理票NO\"");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         D.\"データID\"");
            sql.AppendLine("                        ,D.\"管理責任部署\" AS \"SECTION_GROUP_ID\"");
            sql.AppendLine("                        ,D.\"仕向地\"");
            sql.AppendLine("                        ,D.\"トランスミッション\"");
            sql.AppendLine("                        ,D.\"FLAG_ナビ付\"");
            sql.AppendLine("                        ,D.\"FLAG_ETC付\"");
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
            sql.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" H");
            sql.AppendLine("    ON G.\"SECTION_GROUP_ID\" = H.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" I");
            sql.AppendLine("    ON H.\"SECTION_ID\" = I.\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"GENERAL_CODE\" J");
            sql.AppendLine("    ON A.\"GENERAL_CODE\" = J.\"GENERAL_CODE\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_外製車日程_最終予約日\" K");
            sql.AppendLine("    ON A.\"ID\" = K.\"CATEGORY_ID\"");
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_CAR\" L");
            sql.AppendLine("    ON A.\"ID\" = L.\"CATEGORY_ID\"");
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
                //空車期間が入力されているかどうか
                if (cond.BLANK_START_DATE != null && cond.BLANK_END_DATE != null)
                {
                    sql.AppendLine("    AND NOT EXISTS");
                    sql.AppendLine("                    (");
                    sql.AppendLine("                        SELECT");
                    sql.AppendLine("                            *");
                    sql.AppendLine("                        FROM");
                    sql.AppendLine("                            \"CARSHARING_SCHEDULE\" L");
                    sql.AppendLine("                            LEFT JOIN \"試験計画_外製車日程_目的行先\" M");
                    sql.AppendLine("                            ON L.ID = M.SCHEDULE_ID");
                    sql.AppendLine("                        WHERE 0 = 0");
                    sql.AppendLine("                            AND M.\"予約者_ID\" != :予約者_ID");
                    sql.AppendLine("                            AND A.\"ID\" = L.\"CATEGORY_ID\"");
                    sql.AppendLine("                            AND");
                    sql.AppendLine("                                (");
                    sql.AppendLine("                                    (L.\"START_DATE\" < :START_DATE AND :START_DATE < L.\"END_DATE\")");
                    sql.AppendLine("                                    OR");
                    sql.AppendLine("                                    (L.\"START_DATE\" < :END_DATE AND :END_DATE < L.\"END_DATE\")");
                    sql.AppendLine("                                    OR");
                    sql.AppendLine("                                    (:START_DATE < L.\"START_DATE\" AND L.\"START_DATE\" < :END_DATE)");
                    sql.AppendLine("                                    OR");
                    sql.AppendLine("                                    (:START_DATE < L.\"END_DATE\" AND L.\"END_DATE\" < :END_DATE)");
                    sql.AppendLine("                                    OR");
                    sql.AppendLine("                                    (L.\"START_DATE\" = :START_DATE AND L.\"END_DATE\" = :END_DATE)");
                    sql.AppendLine("                                )");
                    sql.AppendLine("                    )");

                    prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = cond.BLANK_START_DATE, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = cond.BLANK_END_DATE, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":予約者_ID", Type = OracleDbType.Varchar2, Object = cond.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input });

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

                //車系
                if (string.IsNullOrWhiteSpace(cond.CAR_GROUP) == false)
                {
                    sql.AppendLine("    AND A.\"GENERAL_CODE\" = :CAR_GROUP");
                    prms.Add(new BindModel { Name = ":CAR_GROUP", Type = OracleDbType.Varchar2, Object = cond.CAR_GROUP, Direct = ParameterDirection.Input });

                }

                //車型
                if (string.IsNullOrWhiteSpace(cond.車型) == false)
                {
                    sql.AppendLine("    AND C.\"車型\" = :車型");
                    prms.Add(new BindModel { Name = ":車型", Type = OracleDbType.Varchar2, Object = cond.車型, Direct = ParameterDirection.Input });

                }

                //管理票番号
                if (string.IsNullOrWhiteSpace(cond.管理票番号) == false)
                {
                    sql.AppendLine("    AND B.\"管理票番号\" = :管理票番号");
                    prms.Add(new BindModel { Name = ":管理票番号", Type = OracleDbType.Varchar2, Object = cond.管理票番号, Direct = ParameterDirection.Input });

                }

                //駐車場番号
                if (string.IsNullOrWhiteSpace(cond.駐車場番号) == false)
                {
                    sql.AppendLine("    AND C.\"駐車場番号\" LIKE '%' || :駐車場番号 || '%'");
                    prms.Add(new BindModel { Name = ":駐車場番号", Type = OracleDbType.Varchar2, Object = cond.駐車場番号, Direct = ParameterDirection.Input });

                }

                //所在地
                if (string.IsNullOrWhiteSpace(cond.所在地) == false)
                {
                    sql.AppendLine("    AND C.\"所在地\" = :所在地");
                    prms.Add(new BindModel { Name = ":所在地", Type = OracleDbType.Varchar2, Object = cond.所在地, Direct = ParameterDirection.Input });

                }

                //仕向地
                if (string.IsNullOrWhiteSpace(cond.仕向地) == false)
                {
                    sql.AppendLine("    AND G.\"仕向地\" = :仕向地");
                    prms.Add(new BindModel { Name = ":仕向地", Type = OracleDbType.Varchar2, Object = cond.仕向地, Direct = ParameterDirection.Input });

                }

                //FLAG_ETC付
                if (cond.FLAG_ETC付 != null)
                {
                    sql.AppendLine("    AND G.\"FLAG_ETC付\" = :FLAG_ETC付");
                    prms.Add(new BindModel { Name = ":FLAG_ETC付", Type = OracleDbType.Int16, Object = cond.FLAG_ETC付, Direct = ParameterDirection.Input });

                }

                //トランスミッション
                if (string.IsNullOrWhiteSpace(cond.トランスミッション) == false)
                {
                    sql.AppendLine("    AND G.\"トランスミッション\" = :トランスミッション");
                    prms.Add(new BindModel { Name = ":トランスミッション", Type = OracleDbType.Varchar2, Object = cond.トランスミッション, Direct = ParameterDirection.Input });

                }

            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    A.\"SORT_NO\"");

            //取得
            return db.ReadModelList<CarShareScheduleItemModel>(sql.ToString(), prms);

        }
        #endregion

        #region カーシェアスケジュール項目追加
        /// <summary>
        /// カーシェアスケジュール項目追加
        /// </summary>
        /// <param name="list">カーシェアスケジュール項目</param>
        /// <returns>追加可否</returns>
        public bool InsertCarShareScheduleItem(List<CarShareScheduleItemModel> list)
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
                results.Add(this.UpdateCarShareScheduleItemSortNo(list));

                //試験計画_外製車日程_車両リストを登録
                results.Add(this.MergeCarList(list));

                //試験計画_外製車日程_最終予約日を登録
                results.Add(this.MergLastReserve(list));

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
        /// <param name="list">カーシェアスケジュール項目</param>
        /// <returns>追加可否</returns>
        private bool InsertScheduleItem(List<CarShareScheduleItemModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"CARSHARING_SCHEDULE_ITEM\"");
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
            sql.AppendLine("    ,'カーシェア日程' AS \"FLAG_CLASS\"");
            sql.AppendLine("    ,1 AS \"FLAG_SEPARATOR\"");
            sql.AppendLine("    ,SYSTIMESTAMP AS \"INPUT_DATETIME\"");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID AS \"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID AS \"INPUT_LOGIN_ID\"");
            sql.AppendLine("    ,SYSTIMESTAMP AS \"CHANGE_DATETIME\"");
            sql.AppendLine("    ,:ID AS \"CATEGORY_ID\"");
            sql.AppendLine("    ,:PARALLEL_INDEX_GROUP AS \"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"CARSHARING_SCHEDULE_ITEM\" A");
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

        #region カーシェアスケジュール項目更新
        /// <summary>
        /// カーシェアスケジュール項目更新
        /// </summary>
        /// <param name="list">カーシェアスケジュール項目</param>
        /// <returns>更新可否</returns>
        public bool UpdateCarShareScheduleItem(List<CarShareScheduleItemModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //DEVELOPMENT_SCHEDULEを更新(スケジュール項目)
                results.Add(this.UpdateScheduleItem(list));

                //DEVELOPMENT_SCHEDULEのソート順を設定
                results.Add(this.UpdateCarShareScheduleItemSortNo(list));
                
                //試験計画_外製車日程_車両リストを登録
                results.Add(this.MergeCarList(list));

                //試験計画_外製車日程_最終予約日を登録
                results.Add(this.MergLastReserve(list));

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
        /// DEVELOPMENT_SCHEDULEを更新(スケジュール項目)
        /// </summary>
        /// <param name="list">カーシェアスケジュール項目</param>
        /// <returns>更新可否</returns>
        private bool UpdateScheduleItem(List<CarShareScheduleItemModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"CARSHARING_SCHEDULE_ITEM\"");
            sql.AppendLine("SET");
            sql.AppendLine("     \"GENERAL_CODE\" = :GENERAL_CODE");
            sql.AppendLine("    ,\"CATEGORY\" = :CATEGORY");
            sql.AppendLine("    ,\"SORT_NO\" = :SORT_NO");
            sql.AppendLine("    ,\"SECTION_GROUP_ID\" = :SECTION_GROUP_ID");
            sql.AppendLine("    ,\"INPUT_PERSONEL_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,\"INPUT_LOGIN_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,\"CHANGE_DATETIME\" = SYSTIMESTAMP");
            sql.AppendLine("    ,\"PARALLEL_INDEX_GROUP\" = :PARALLEL_INDEX_GROUP");
            //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
            sql.AppendLine("    ,\"REPLACEMENT_TEXT\" = :入れ替え中車両");
            //Append End 2022/01/17 杉浦 入れ替え中車両の処理
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :ID");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = schedule.GENERAL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CATEGORY", Type = OracleDbType.Varchar2, Object = schedule.CATEGORY, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SORT_NO", Type = OracleDbType.Decimal, Object = schedule.SORT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = schedule.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = schedule.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = schedule.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input },
                    //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
                    new BindModel { Name = ":入れ替え中車両", Type = OracleDbType.Varchar2, Object = schedule.入れ替え中車両, Direct = ParameterDirection.Input },
                    //Append End 2022/01/17 杉浦 入れ替え中車両の処理
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input }

                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region カーシェアスケジュール項目削除
        /// <summary>
        /// カーシェアスケジュール削除
        /// </summary>
        /// <param name="list">カーシェアスケジュール項目</param>
        /// <returns>削除可否</returns>
        public bool DeleteCarShareScheduleItem(List<CarShareScheduleItemModel> list)
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

                //試験計画_外製車日程_最終予約日を全て削除
                results.Add(this.DeleteLastReserveAll(idList));

                //試験計画_外製車日程_目的行先を全て削除
                results.Add(this.DeleteMokutekiYukisakiAll(idList));

                //試験計画_外製車日程_貸返備考を全て削除
                results.Add(this.DeleteKasihenBikouAll(idList));

                //試験計画_課題フォローリストを全て削除
                results.Add(this.DeleteKadaiFollowListAll(idList));

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
        /// 試験計画_外製車日程_最終予約日を全て削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteLastReserveAll(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_外製車日程_最終予約日\"");
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
        /// 試験計画_外製車日程_目的行先を全て削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteMokutekiYukisakiAll(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_外製車日程_目的行先\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND EXISTS");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                        *");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"CARSHARING_SCHEDULE\" B");
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
        /// 試験計画_外製車日程_貸返備考を全て削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteKasihenBikouAll(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_外製車日程_貸返備考\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND EXISTS");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                        *");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"CARSHARING_SCHEDULE\" B");
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
            sql.AppendLine("    \"CARSHARING_SCHEDULE_ITEM\"");
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
            sql.AppendLine("    \"CARSHARING_SCHEDULE\"");
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

        #region カーシェアスケジュール取得
        /// <summary>
        /// カーシェアスケジュール取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<CarShareScheduleModel> GetCarShareSchedule(CarShareScheduleSearchModel cond)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"ID\"");
            sql.AppendLine("    ,H.\"CAR_GROUP\"");
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
            sql.AppendLine("    ,A.\"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("    ,B.\"目的\"");
            sql.AppendLine("    ,B.\"行先\"");
            sql.AppendLine("    ,B.\"TEL\"");
            sql.AppendLine("    ,B.\"FLAG_実使用\"");
            sql.AppendLine("    ,B.\"FLAG_空時間貸出可\"");
            sql.AppendLine("    ,B.\"予約者_ID\"");
            sql.AppendLine("    ,E.\"SECTION_CODE\" AS \"予約者_SECTION_CODE\"");
            sql.AppendLine("    ,C.\"NAME\" AS \"予約者_NAME\"");
            sql.AppendLine("    ,F.\"FLAG_返却済\"");
            sql.AppendLine("    ,G.\"FLAG_要予約許可\"");
            //Append Start 2022/02/21 杉浦 入れ替え中車両の処理
            sql.AppendLine("    ,I.\"REPLACEMENT_TEXT\"");
            //Append End 2022/02/21 杉浦 入れ替え中車両の処理
            sql.AppendLine("FROM");
            sql.AppendLine("    \"CARSHARING_SCHEDULE\" A");
            sql.AppendLine("    LEFT JOIN \"試験計画_外製車日程_目的行先\" B");
            sql.AppendLine("    ON A.\"ID\" = B.\"SCHEDULE_ID\"");
            sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" C");
            sql.AppendLine("    ON B.\"予約者_ID\" = C.\"PERSONEL_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" D");
            sql.AppendLine("    ON C.\"SECTION_GROUP_ID\" = D.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" E");
            sql.AppendLine("    ON D.\"SECTION_ID\" = E.\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_外製車日程_貸返備考\" F");
            sql.AppendLine("    ON A.\"ID\" = F.\"SCHEDULE_ID\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_外製車日程_車両リスト\" G");
            sql.AppendLine("    ON A.\"CATEGORY_ID\" = G.\"CATEGORY_ID\"");
            sql.AppendLine("    LEFT JOIN \"GENERAL_CODE\" H");
            sql.AppendLine("    ON A.\"GENERAL_CODE\" = H.\"GENERAL_CODE\"");
            //Append Start 2022/02/21 杉浦 入れ替え中車両の処理
            sql.AppendLine("    LEFT JOIN \"CARSHARING_SCHEDULE_ITEM\" I");
            sql.AppendLine("    ON A.\"CATEGORY_ID\" = I.\"ID\"");
            //Append End 2022/02/21 杉浦 入れ替え中車両の処理

            sql.AppendLine("WHERE 0 = 0");

            //IDが指定されているかどうか
            if (cond.ID != null)
            {
                sql.AppendLine("    AND A.\"ID\" = :ID");
                prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = cond.ID, Direct = ParameterDirection.Input });

            }
            else
            {
                var categoryIdList = new List<long?>();

                //カテゴリーIDが指定されているかどうか
                if (cond.CATEGORY_ID == null)
                {
                    //カーシェアスケジュール項目の検索条件に変換
                    var itemCond = JsonConvert.DeserializeObject<CarShareScheduleItemSearchModel>(JsonConvert.SerializeObject(cond));
                    categoryIdList.AddRange(this.GetCarShareScheduleItem(itemCond).Where(x => x.CATEGORY_ID != null).Select(x => x.CATEGORY_ID));

                }
                else
                {
                    categoryIdList.Add(cond.CATEGORY_ID);

                }

                sql.Append("    AND A.\"CATEGORY_ID\" IN (NULL");

                //カテゴリーID
                if (categoryIdList != null && categoryIdList.Any() == true)
                {
                    for (var i = 0; i < categoryIdList.Count(); i++)
                    {
                        var name = string.Format(":CATEGORY_ID{0}", i);

                        sql.AppendFormat(",{0}", name);

                        prms.Add(new BindModel { Name = name, Type = OracleDbType.Int64, Object = categoryIdList.ElementAt(i), Direct = ParameterDirection.Input });

                    }

                }

                sql.AppendLine(")");

                var startDate = cond.START_DATE == null ? null : (DateTime?)cond.START_DATE.Value.Date;
                var endDate = cond.END_DATE == null ? null : (DateTime?)cond.END_DATE.Value.Date;

                //期間がすべて入力されているかどうか
                if (cond.START_DATE != null && cond.END_DATE != null)
                {
                    sql.AppendLine("    AND");
                    sql.AppendLine("        (");
                    sql.AppendLine("            :START_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                    sql.AppendLine("            OR");
                    sql.AppendLine("            :END_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                    sql.AppendLine("            OR");
                    sql.AppendLine("            A.\"START_DATE\" BETWEEN :START_DATE AND (:END_DATE + 1)");
                    sql.AppendLine("            OR");
                    sql.AppendLine("            A.\"END_DATE\" BETWEEN :START_DATE AND (:END_DATE + 1)");
                    sql.AppendLine("        )");

                    prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

                }
                else
                {
                    //期間(From)
                    if (cond.START_DATE != null)
                    {
                        sql.AppendLine("    AND");
                        sql.AppendLine("        (");
                        sql.AppendLine("            :START_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                        sql.AppendLine("            OR");
                        sql.AppendLine("            A.\"START_DATE\" >= :START_DATE");
                        sql.AppendLine("        )");

                        prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input });

                    }

                    //期間(To)
                    if (cond.END_DATE != null)
                    {
                        sql.AppendLine("    AND");
                        sql.AppendLine("        (");
                        sql.AppendLine("            :END_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                        sql.AppendLine("            OR");
                        sql.AppendLine("            A.\"END_DATE\" < (:END_DATE + 1)");
                        sql.AppendLine("        )");

                        prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

                    }

                }

                //行番号
                if (cond.PARALLEL_INDEX_GROUP != null)
                {
                    sql.AppendLine("    AND A.\"PARALLEL_INDEX_GROUP\" = :PARALLEL_INDEX_GROUP");
                    prms.Add(new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = cond.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input });

                }

            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"CATEGORY_ID\"");
            sql.AppendLine("    ,A.\"ID\"");

            //取得
            return db.ReadModelList<CarShareScheduleModel>(sql.ToString(), prms);

        }
        #endregion

        #region カーシェアスケジュール追加
        /// <summary>
        /// カーシェアスケジュール追加
        /// </summary>
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns>追加可否</returns>
        public bool InsertCarShareSchedule(List<CarShareScheduleModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //DEVELOPMENT_SCHEDULEを追加
                results.Add(this.InsertSchedule(list));

                //試験計画_外製車日程_目的行先を登録
                results.Add(this.MergeMokutekiYukisaki(list));

                //試験計画_外製車日程_貸返備考を登録
                results.Add(this.MergeKasihenBikou(list));

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
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns>追加可否</returns>
        private bool InsertSchedule(List<CarShareScheduleModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"CARSHARING_SCHEDULE\"");
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
            sql.AppendLine("    ,\"INPUT_DATETIME\"");
            sql.AppendLine("    ,\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,\"INPUT_LOGIN_ID\"");
            sql.AppendLine("    ,\"CHANGE_DATETIME\"");
            sql.AppendLine("    ,\"CATEGORY_ID\"");
            sql.AppendLine("    ,\"予約種別\"");
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
            sql.AppendLine("    ,'カーシェア日程' AS \"FLAG_CLASS\"");
            sql.AppendLine("    ,SYSTIMESTAMP AS \"INPUT_DATETIME\"");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID AS \"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID AS \"INPUT_LOGIN_ID\"");
            sql.AppendLine("    ,SYSTIMESTAMP AS \"CHANGE_DATETIME\"");
            sql.AppendLine("    ,:CATEGORY_ID AS \"CATEGORY_ID\"");
            sql.AppendLine("    ,:予約種別 AS \"予約種別\"");
            sql.AppendLine("    ,:PARALLEL_INDEX_GROUP AS \"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    DUAL");

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
                    new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = schedule.START_DATE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = schedule.END_DATE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":DESCRIPTION", Type = OracleDbType.Varchar2, Object = schedule.DESCRIPTION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SYMBOL", Type = OracleDbType.Int16, Object = schedule.SYMBOL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = schedule.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = schedule.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = schedule.CATEGORY_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約種別", Type = OracleDbType.Varchar2, Object = schedule.予約種別, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = schedule.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input }

                };

                //追加
                results.Add(db.InsertData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region カーシェアスケジュール更新
        /// <summary>
        /// カーシェアスケジュール更新
        /// </summary>
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns>更新可否</returns>
        public bool UpdateCarShareSchedule(List<CarShareScheduleModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //DEVELOPMENT_SCHEDULEを更新
                results.Add(this.UpdateSchedule(list));

                //試験計画_外製車日程_目的行先を登録
                results.Add(this.MergeMokutekiYukisaki(list));

                //試験計画_外製車日程_貸返備考を登録
                results.Add(this.MergeKasihenBikou(list));

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
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns>更新可否</returns>
        private bool UpdateSchedule(List<CarShareScheduleModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"CARSHARING_SCHEDULE\"");
            sql.AppendLine("SET");
            sql.AppendLine("     \"START_DATE\" = :START_DATE");
            sql.AppendLine("    ,\"END_DATE\" = :END_DATE");
            sql.AppendLine("    ,\"DESCRIPTION\" = :DESCRIPTION");
            sql.AppendLine("    ,\"SYMBOL\" = :SYMBOL");
            sql.AppendLine("    ,\"SECTION_GROUP_ID\" = :SECTION_GROUP_ID");
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

                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = schedule.START_DATE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = schedule.END_DATE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":DESCRIPTION", Type = OracleDbType.Varchar2, Object = schedule.DESCRIPTION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SYMBOL", Type = OracleDbType.Int16, Object = schedule.SYMBOL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = schedule.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
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

        #region カーシェアスケジュール削除
        /// <summary>
        /// カーシェアスケジュール削除
        /// </summary>
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns>削除可否</returns>
        public bool DeleteCarShareSchedule(List<CarShareScheduleModel> list)
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

                //試験計画_外製車日程_目的行先を削除
                results.Add(this.DeleteMokutekiYukisaki(idList));

                //試験計画_外製車日程_貸返備考を削除
                results.Add(this.DeleteKasihenBikou(idList));

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
            sql.AppendLine("    \"CARSHARING_SCHEDULE\"");
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
        /// 試験計画_外製車日程_目的行先を削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteMokutekiYukisaki(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_外製車日程_目的行先\"");
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
        /// 試験計画_外製車日程_貸返備考を削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteKasihenBikou(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_外製車日程_貸返備考\"");
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

        #region カーシェアスケジュール項目のソート順更新
        /// <summary>
        /// カーシェアスケジュール項目のソート順更新
        /// </summary>
        /// <param name="list">カーシェアスケジュール項目</param>
        /// <returns>更新可否</returns>
        private bool UpdateCarShareScheduleItemSortNo(List<CarShareScheduleItemModel> list)
        {
            var results = new List<bool>();

            //更新対象の全ての車系の並び順を変更
            foreach (var carGroup in list.Where(x => string.IsNullOrWhiteSpace(x.GENERAL_CODE) == false).Select(x => x.GENERAL_CODE).Distinct())
            {
                //更新
                results.Add(base.GetLogic<CommonLogic>().UpdateScheduleItemSortNoByGeneralCode(TableID, carGroup));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 試験計画_外製車日程_車両リストを登録
        /// <summary>
        /// 試験計画_外製車日程_車両リストを登録
        /// </summary>
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns>更新可否</returns>
        private bool MergeCarList(List<CarShareScheduleItemModel> list)
        {
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
            sql.AppendLine("            ,:FLAG_要予約許可 AS \"FLAG_要予約許可\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"試験計画_外製車日程_車両リスト\"");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"CATEGORY_ID\" = B.\"CATEGORY_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("         \"管理票番号\" = B.\"管理票番号\"");
            sql.AppendLine("        ,\"FLAG_要予約許可\" = B.\"FLAG_要予約許可\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"ID\"");
            sql.AppendLine("        ,\"CATEGORY_ID\"");
            sql.AppendLine("        ,\"管理票番号\"");
            sql.AppendLine("        ,\"FLAG_要予約許可\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"ID\"");
            sql.AppendLine("        ,B.\"CATEGORY_ID\"");
            sql.AppendLine("        ,B.\"管理票番号\"");
            sql.AppendLine("        ,B.\"FLAG_要予約許可\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理票番号", Type = OracleDbType.Varchar2, Object = schedule.管理票番号, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_要予約許可", Type = OracleDbType.Int16, Object = schedule.FLAG_要予約許可, Direct = ParameterDirection.Input },

                };

                //登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 試験計画_外製車日程_最終予約日を登録
        /// <summary>
        /// 試験計画_外製車日程_最終予約日を登録
        /// </summary>
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns>更新可否</returns>
        private bool MergLastReserve(List<CarShareScheduleItemModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"試験計画_外製車日程_最終予約日\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             (NVL(MAX(\"ID\"),0) + 1) AS \"ID\"");
            sql.AppendLine("            ,:ID AS \"CATEGORY_ID\"");
            sql.AppendLine("            ,:最終予約可能日 AS \"最終予約可能日\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"試験計画_外製車日程_最終予約日\"");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"CATEGORY_ID\" = B.\"CATEGORY_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("        \"最終予約可能日\" = B.\"最終予約可能日\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"ID\"");
            sql.AppendLine("        ,\"CATEGORY_ID\"");
            sql.AppendLine("        ,\"最終予約可能日\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"ID\"");
            sql.AppendLine("        ,B.\"CATEGORY_ID\"");
            sql.AppendLine("        ,B.\"最終予約可能日\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":最終予約可能日", Type = OracleDbType.Date, Object = schedule.最終予約可能日, Direct = ParameterDirection.Input }

                };

                //登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 試験計画_外製車日程_目的行先を登録
        /// <summary>
        /// 試験計画_外製車日程_目的行先を登録
        /// </summary>
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns></returns>
        private bool MergeMokutekiYukisaki(List<CarShareScheduleModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"試験計画_外製車日程_目的行先\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             (NVL(MAX(\"ID\"),0) + 1) AS \"ID\"");
            sql.AppendLine("            ,:SCHEDULE_ID AS \"SCHEDULE_ID\"");
            sql.AppendLine("            ,:目的 AS \"目的\"");
            sql.AppendLine("            ,:行先 AS \"行先\"");
            sql.AppendLine("            ,:TEL AS \"TEL\"");
            sql.AppendLine("            ,NULL AS \"FLAG_実使用\"");
            sql.AppendLine("            ,:予約者_ID AS \"予約者_ID\"");
            sql.AppendLine("            ,NULL AS \"駐車場番号\"");
            sql.AppendLine("            ,NULL AS \"管理票番号\"");
            sql.AppendLine("            ,:FLAG_空時間貸出可 AS \"FLAG_空時間貸出可\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"試験計画_外製車日程_目的行先\"");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"SCHEDULE_ID\" = B.\"SCHEDULE_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("         \"目的\" = B.\"目的\"");
            sql.AppendLine("        ,\"行先\" = B.\"行先\"");
            sql.AppendLine("        ,\"TEL\" = B.\"TEL\"");
            sql.AppendLine("        ,\"FLAG_空時間貸出可\" = B.\"FLAG_空時間貸出可\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"ID\"");
            sql.AppendLine("        ,\"SCHEDULE_ID\"");
            sql.AppendLine("        ,\"目的\"");
            sql.AppendLine("        ,\"行先\"");
            sql.AppendLine("        ,\"TEL\"");
            sql.AppendLine("        ,\"FLAG_実使用\"");
            sql.AppendLine("        ,\"予約者_ID\"");
            sql.AppendLine("        ,\"駐車場番号\"");
            sql.AppendLine("        ,\"管理票番号\"");
            sql.AppendLine("        ,\"FLAG_空時間貸出可\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"ID\"");
            sql.AppendLine("        ,B.\"SCHEDULE_ID\"");
            sql.AppendLine("        ,B.\"目的\"");
            sql.AppendLine("        ,B.\"行先\"");
            sql.AppendLine("        ,B.\"TEL\"");
            sql.AppendLine("        ,B.\"FLAG_実使用\"");
            sql.AppendLine("        ,B.\"予約者_ID\"");
            sql.AppendLine("        ,B.\"駐車場番号\"");
            sql.AppendLine("        ,B.\"管理票番号\"");
            sql.AppendLine("        ,B.\"FLAG_空時間貸出可\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":SCHEDULE_ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":目的", Type = OracleDbType.Varchar2, Object = schedule.目的, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":行先", Type = OracleDbType.Varchar2, Object = schedule.行先, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":TEL", Type = OracleDbType.Varchar2, Object = schedule.TEL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約者_ID", Type = OracleDbType.Varchar2, Object = schedule.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_空時間貸出可", Type = OracleDbType.Int32, Object = schedule.FLAG_空時間貸出可, Direct = ParameterDirection.Input }

                };

                //登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);
        }
        #endregion

        #region 試験計画_外製車日程_貸返備考を登録
        /// <summary>
        /// 試験計画_外製車日程_貸返備考を登録
        /// </summary>
        /// <param name="list">カーシェアスケジュール</param>
        /// <returns></returns>
        private bool MergeKasihenBikou(List<CarShareScheduleModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"試験計画_外製車日程_貸返備考\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             (NVL(MAX(\"ID\"),0) + 1) AS \"ID\"");
            sql.AppendLine("            ,:SCHEDULE_ID AS \"SCHEDULE_ID\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"試験計画_外製車日程_貸返備考\"");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"SCHEDULE_ID\" = B.\"SCHEDULE_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"ID\"");
            sql.AppendLine("        ,\"SCHEDULE_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"ID\"");
            sql.AppendLine("        ,B.\"SCHEDULE_ID\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var schedule in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":SCHEDULE_ID", Type = OracleDbType.Int64, Object = schedule.ID, Direct = ParameterDirection.Input }

                };

                //登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 作業履歴削除
        /// <summary>
        /// 試験計画_課題フォローリストを全て削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>追加可否</returns>
        private bool DeleteKadaiFollowListAll(List<long> idList)
        {
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

        //Append Start 2021/08/25 矢作

        #region カーシェアスケジュール複数取得
        /// <summary>
        /// カーシェアスケジュール複数取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<CarShareScheduleModel> GetCarShareScheduleList(CarShareScheduleSearchListModel cond)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"ID\"");
            sql.AppendLine("    ,H.\"CAR_GROUP\"");
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
            sql.AppendLine("    ,A.\"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("    ,B.\"目的\"");
            sql.AppendLine("    ,B.\"行先\"");
            sql.AppendLine("    ,B.\"TEL\"");
            sql.AppendLine("    ,B.\"FLAG_実使用\"");
            sql.AppendLine("    ,B.\"FLAG_空時間貸出可\"");
            sql.AppendLine("    ,B.\"予約者_ID\"");
            sql.AppendLine("    ,E.\"SECTION_CODE\" AS \"予約者_SECTION_CODE\"");
            sql.AppendLine("    ,C.\"NAME\" AS \"予約者_NAME\"");
            sql.AppendLine("    ,F.\"FLAG_返却済\"");
            sql.AppendLine("    ,G.\"FLAG_要予約許可\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"CARSHARING_SCHEDULE\" A");
            sql.AppendLine("    LEFT JOIN \"試験計画_外製車日程_目的行先\" B");
            sql.AppendLine("    ON A.\"ID\" = B.\"SCHEDULE_ID\"");
            sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" C");
            sql.AppendLine("    ON B.\"予約者_ID\" = C.\"PERSONEL_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" D");
            sql.AppendLine("    ON C.\"SECTION_GROUP_ID\" = D.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" E");
            sql.AppendLine("    ON D.\"SECTION_ID\" = E.\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_外製車日程_貸返備考\" F");
            sql.AppendLine("    ON A.\"ID\" = F.\"SCHEDULE_ID\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_外製車日程_車両リスト\" G");
            sql.AppendLine("    ON A.\"CATEGORY_ID\" = G.\"CATEGORY_ID\"");
            sql.AppendLine("    LEFT JOIN \"GENERAL_CODE\" H");
            sql.AppendLine("    ON A.\"GENERAL_CODE\" = H.\"GENERAL_CODE\"");

            sql.AppendLine("WHERE 0 = 0");

            //IDが指定されているかどうか
            if (cond.IDList != null)
            {
                sql.AppendLine("    AND A.\"ID\" IN( NULL ");
                foreach (var id in cond.IDList)
                {
                    sql.Append("," + id.ToString());
                }
                sql.Append(")");
            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"CATEGORY_ID\"");
            sql.AppendLine("    ,A.\"ID\"");

            //取得
            return db.ReadModelList<CarShareScheduleModel>(sql.ToString(), prms);

        }
        #endregion

        //Append End 2021/08/25 矢作

    }
}