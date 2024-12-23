//
// 業務計画表システム
// KKA03010 目標進度リストAPI
// 作成者 : 岸　義将
// 作成日 : 2017/03/13

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
    /// KKA03010 目標進度リストAPI
    /// </summary>
    /// <remarks>目標進度リスト検索</remarks>
    public class TargetProgressListLogic : BaseLogic
    {
        #region 目標進度リスト取得
        /// <summary>
        /// 目標進度リスト取得
        /// </summary>
        /// <param name="val">APIが受け取ったパラメータ</param>
        /// <returns></returns>
        public List<TargetProgressListSearchOutModel> GetData(TargetProgressListSearchInModel val)
        {
            #region <<< 目標進度リスト取得SQL文の組立およびパラメータの設定 >>>
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = val.GENERAL_CODE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":RELATIONALSECTION", Type = OracleDbType.Varchar2, Object = val.SECTION_CD, Direct = ParameterDirection.Input }

            };

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     E.\"SECTION_CODE\"");
            sql.AppendLine("    ,NVL2(A.\"仕様\", D.\"性能名\" || '－' || A.\"仕様\", D.\"性能名\") AS \"SPEC_NAME\"");
            sql.AppendLine("    ,B.\"大項目\" AS \"LARGE_CLASSIFICATION\"");
            sql.AppendLine("    ,B.\"中項目\" AS \"MIDDLE_CLASSIFICATION\"");
            sql.AppendLine("    ,B.\"小項目\" AS \"SMALL_CLASSIFICATION\"");
            sql.AppendLine("    ,B.\"目標値\" AS \"TARGET_VALUE\"");
            sql.AppendLine("    ,B.\"達成値\" AS \"ACHIEVED_VALUE\"");
            sql.AppendLine("    ,B.\"関連課_CODE\" AS \"RELATIONAL_DIVISION_CODE\"");
            sql.AppendLine("    ,C.\"編集日\" AS \"EDITED_DATE\"");
            sql.AppendLine("    ,G.\"NAME\" AS \"EDITOR_NAME\"");
            sql.AppendLine("    ,A.\"ID\" AS \"CHECKLIST_NAME_ID\"");
            sql.AppendLine("    ,D.\"SECTION_ID\"");
            sql.AppendLine("    ,A.\"性能名_ID\" AS \"SPEC_NAME_ID\"");
            sql.AppendLine("    ,B.\"ID\" AS \"CHECKLIST_ITEMNAME_ID\"");
            sql.AppendLine("    ,A.\"GENERAL_CODE\"");
            sql.AppendLine("    ,B.\"ID\" AS \"ITEMNAME_ID\"");
            sql.AppendLine("    ,E.\"SORT_NO\" AS \"SECTION_SORT_NO\"");
            sql.AppendLine("    ,B.\"SORT_NO\" AS \"LIST_SORT_NO\"");
            sql.AppendLine("    ,B.\"FLAG_DISP\" AS \"ITEM_FLAG_DISP\"");
            sql.AppendLine("    ,E.\"DEPARTMENT_ID\"");
            sql.AppendLine("    ,F.\"DEPARTMENT_CODE\"");
            sql.AppendLine("    ,C.\"編集者_LOGIN_ID\" AS \"EDITOR_LOGIN_ID\"");
            sql.AppendLine("    ,H.\"UNDER_DEVELOPMENT\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"目標進度_チェックリスト名\" A");
            sql.AppendLine("    INNER JOIN \"目標進度_チェックリスト_項目名\" B");
            sql.AppendLine("    ON A.\"ID\" = B.\"チェックリスト名_ID\"");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         A.\"ID\"");
            sql.AppendLine("                        ,A.\"チェックリスト名_ID\"");
            sql.AppendLine("                        ,A.\"チェックリスト項目名_ID\"");
            sql.AppendLine("                        ,A.\"編集日\"");
            sql.AppendLine("                        ,A.\"編集者_ID\"");
            sql.AppendLine("                        ,A.\"編集者_LOGIN_ID\"");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"目標進度_チェックリスト\" A");
            sql.AppendLine("                        INNER JOIN");
            sql.AppendLine("                                    (");
            sql.AppendLine("                                        SELECT");
            sql.AppendLine("                                            MAX(A.ID) AS \"ID\"");
            sql.AppendLine("                                        FROM");
            sql.AppendLine("                                            \"目標進度_チェックリスト\" A");
            sql.AppendLine("                                            INNER JOIN \"目標進度_チェックリスト名\" B");
            sql.AppendLine("                                            ON A.\"チェックリスト名_ID\" = B.\"ID\"");
            sql.AppendLine("                                        WHERE 0 = 0");
            sql.AppendLine("                                            AND A.\"チェックリスト項目名_ID\" IS NOT NULL");
            sql.AppendLine("                                            AND A.\"確認時期_ID\" IS NOT NULL");
            sql.AppendLine("                                            AND B.\"GENERAL_CODE\" = :GENERAL_CODE");
            sql.AppendLine("                                        GROUP BY");
            sql.AppendLine("                                             A.\"チェックリスト名_ID\"");
            sql.AppendLine("                                            ,A.\"チェックリスト項目名_ID\"");
            sql.AppendLine("                                    ) B");
            sql.AppendLine("                        ON A.\"ID\" = B.\"ID\"");
            sql.AppendLine("                ) C");
            sql.AppendLine("    ON C.\"チェックリスト名_ID\" = A.\"ID\"");
            sql.AppendLine("    AND C.\"チェックリスト項目名_ID\" = B.\"ID\"");
            sql.AppendLine("    LEFT JOIN \"目標進度_性能名\" D");
            sql.AppendLine("    ON A.\"性能名_ID\" = D.\"ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" E");
            sql.AppendLine("    ON D.\"SECTION_ID\" = E.\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"DEPARTMENT_DATA\" F");
            sql.AppendLine("    ON E.\"DEPARTMENT_ID\" = F.\"DEPARTMENT_ID\"");
            sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" G");
            sql.AppendLine("    ON C.\"編集者_ID\" = G.\"PERSONEL_ID\"");
            sql.AppendLine("    LEFT JOIN \"GENERAL_CODE\" H");
            sql.AppendLine("    ON A.\"GENERAL_CODE\" = H.\"GENERAL_CODE\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"GENERAL_CODE\" = :GENERAL_CODE");

            //表示モード
            switch (val.DISPLAY_MODE)
            {
                //性能別
                case "1":
                    sql.AppendLine("    AND A.\"ID\" = :ID");
                    prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = val.CHECKLIST_NAME_ID, Direct = ParameterDirection.Input });
                    break;

                //部別
                case "2":
                    sql.AppendLine("    AND E.\"DEPARTMENT_ID\" = :DEPARTMENT_ID");
                    prms.Add(new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = val.DEPARTMENT_ID, Direct = ParameterDirection.Input });
                    break;

                //課別
                case "3":
                    sql.AppendLine("    AND E.\"SECTION_ID\" = :SECTION_ID");
                    prms.Add(new BindModel { Name = ":SECTION_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_ID, Direct = ParameterDirection.Input });
                    break;

            }

            //関連課
            switch (val.RELATION_DISPLAY_FLAG)
            {
                //含まれない
                case "1":
                    sql.AppendLine("    AND");
                    sql.AppendLine("        (");
                    sql.AppendLine("            B.\"関連課_CODE\" IS NULL");
                    sql.AppendLine("            OR");
                    sql.AppendLine("            B.\"関連課_CODE\" NOT LIKE '%' || :RELATIONALSECTION || '%'");
                    sql.AppendLine("        )");
                    break;

                //含む
                case "2":
                    sql.AppendLine("    AND B.\"関連課_CODE\" LIKE '%' || :RELATIONALSECTION || '%'");
                    break;

            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     F.\"SORT_NO\"");
            sql.AppendLine("    ,E.\"SORT_NO\"");
            sql.AppendLine("    ,\"SPEC_NAME\"");
            sql.AppendLine("    ,B.\"SORT_NO\"");

            var list = db.ReadModelList<TargetProgressListSearchOutModel>(sql.ToString(), prms);
            #endregion

            #region 確認時期を取得
            var idList = list.Select(x => x.CHECKLIST_NAME_ID).Distinct().ToList();

            //目標進度_チェックリストの追加
            this.InsertCheckList(idList);

            // 確認時期と確認結果一覧を取得
            var confirmSeason = this.GetConfirmSeason(idList);

            foreach (var target in list)
            {
                var confirmList = confirmSeason.Where(p => p.CHECKLIST_ITEMNAME_ID == target.CHECKLIST_ITEMNAME_ID).OrderBy(x => x.SEASON_SEQUENCE).ToArray();

                var confirmSeasonIdList = new List<long>();
                var checkListIdList = new List<long>();
                var confirmSeasonNameList = new List<string>();
                var confirmResultsList = new List<string>();
                var seasonSequenceList = new List<int>();

                foreach (var confirm in confirmList)
                {
                    confirmSeasonIdList.Add(confirm.CONFIRM_SEASON_ID);
                    checkListIdList.Add(confirm.CHECKLIST_ID);
                    confirmSeasonNameList.Add(confirm.CONFIRM_SEASON_NAME);
                    confirmResultsList.Add(confirm.CONFIRM_RESULTS);
                    seasonSequenceList.Add(confirm.SEASON_SEQUENCE);
                }

                // 取得した確認時期一覧を配列に格納
                target.CONFIRM_SEASON_ID = confirmSeasonIdList.ToArray();
                target.CHECKLIST_ID = checkListIdList.ToArray();
                target.CONFIRM_SEASON_NAME = confirmSeasonNameList.ToArray();
                target.CONFIRM_RESULTS = confirmResultsList.ToArray();
                target.SEASON_SEQUENCE = seasonSequenceList.ToArray();

            }
            #endregion

            return list;

        }
        #endregion

        #region 目標進度_チェックリストの追加
        /// <summary>
        /// 目標進度_チェックリストの追加
        /// </summary>
        /// <param name="idList">チェックリスト名_ID</param>
        /// <returns></returns>
        /// <remarks>データ件数を一致しないデータを補完</remarks>
        private bool InsertCheckList(List<long> idList)
        {
            var prms = new List<BindModel>();

            // トランザクション開始
            db.Begin();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO 目標進度_チェックリスト");
            sql.AppendLine("(");
            sql.AppendLine("     \"ID\"");
            sql.AppendLine("    ,\"確認時期\"");
            sql.AppendLine("    ,\"確認時期順\"");
            sql.AppendLine("    ,\"確認結果\"");
            sql.AppendLine("    ,\"編集日\"");
            sql.AppendLine("    ,\"編集者_ID\"");
            sql.AppendLine("    ,\"チェックリスト名_ID\"");
            sql.AppendLine("    ,\"チェックリスト項目名_ID\"");
            sql.AppendLine("    ,\"確認時期_ID\"");
            sql.AppendLine("    ,\"編集者_LOGIN_ID\"");
            sql.AppendLine(")");
            sql.AppendLine("SELECT");
            sql.AppendLine("     CAST((A.\"MAX_ID\" + B.\"ID\") AS NUMBER(10)) AS \"ID\"");
            sql.AppendLine("    ,B.\"確認時期\"");
            sql.AppendLine("    ,B.\"確認時期順\"");
            sql.AppendLine("    ,B.\"確認結果\"");
            sql.AppendLine("    ,B.\"編集日\"");
            sql.AppendLine("    ,B.\"編集者_ID\"");
            sql.AppendLine("    ,B.\"チェックリスト名_ID\"");
            sql.AppendLine("    ,B.\"チェックリスト項目名_ID\"");
            sql.AppendLine("    ,B.\"確認時期_ID\"");
            sql.AppendLine("    ,B.\"編集者_LOGIN_ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("            MAX(A.\"ID\") AS \"MAX_ID\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"目標進度_チェックリスト\" A");
            sql.AppendLine("    ) A,");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             ROW_NUMBER() OVER (ORDER BY A.ID,B.ID,C.時期順) AS \"ID\"");
            sql.AppendLine("            ,C.\"確認時期名\" AS \"確認時期\"");
            sql.AppendLine("            ,NULL AS \"確認時期順\"");
            sql.AppendLine("            ,NULL AS \"確認結果\"");
            sql.AppendLine("            ,NULL AS \"編集日\"");
            sql.AppendLine("            ,NULL AS \"編集者_ID\"");
            sql.AppendLine("            ,A.\"ID\" AS \"チェックリスト名_ID\"");
            sql.AppendLine("            ,B.\"ID\" AS \"チェックリスト項目名_ID\"");
            sql.AppendLine("            ,C.\"ID\" AS \"確認時期_ID\"");
            sql.AppendLine("            ,NULL AS \"編集者_LOGIN_ID\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"目標進度_チェックリスト名\" A");
            sql.AppendLine("            INNER JOIN \"目標進度_チェックリスト_項目名\" B");
            sql.AppendLine("            ON A.\"ID\" = B.\"チェックリスト名_ID\"");
            sql.AppendLine("            INNER JOIN \"目標進度_確認時期\" C");
            sql.AppendLine("            ON A.\"GENERAL_CODE\" = C.\"GENERAL_CODE\"");
            sql.AppendLine("        WHERE 0 = 0");
            sql.AppendLine("            AND C.\"FLAG_DISP\" = 1");
            sql.AppendLine("            AND NOT EXISTS");
            sql.AppendLine("                            (");
            sql.AppendLine("                                SELECT");
            sql.AppendLine("                                    *");
            sql.AppendLine("                                FROM");
            sql.AppendLine("                                    \"目標進度_チェックリスト\" D");
            sql.AppendLine("                                WHERE 0 = 0");
            sql.AppendLine("                                    AND A.\"ID\" = D.\"チェックリスト名_ID\"");
            sql.AppendLine("                                    AND B.\"ID\" = D.\"チェックリスト項目名_ID\"");
            sql.AppendLine("                                    AND C.\"ID\" = D.\"確認時期_ID\"");
            sql.AppendLine("                            )");

            sql.Append("            AND A.\"ID\" IN (NULL");

            //チェックリスト名_ID
            if (idList != null && idList.Any() == true)
            {
                for (var i = 0; i < idList.Count(); i++)
                {
                    var name = string.Format(":ID{0}", i);

                    sql.AppendFormat(",{0}", name);

                    prms.Add(new BindModel { Name = name, Type = OracleDbType.Int64, Object = idList.ElementAt(i), Direct = ParameterDirection.Input });

                }
            }

            sql.AppendLine(")");

            sql.AppendLine("        ORDER BY");
            sql.AppendLine("             A.\"ID\"");
            sql.AppendLine("            ,B.\"ID\"");
            sql.AppendLine("            ,C.\"時期順\"");
            sql.AppendLine("    ) B");

            //追加が成功したかどうか
            var flg = db.InsertData(sql.ToString(), prms);
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
        #endregion

        #region 確認時期の取得
        /// <summary>
        /// 確認時期の取得
        /// </summary>
        /// <param name="idList">チェックリスト名_ID</param>
        /// <returns>確認時期と結果</returns>
        private List<TargetProgressListConfirmSeasonModel> GetConfirmSeason(List<long> idList)
        {
            var prms = new List<BindModel>();

            //チェックリスト名_ID
            var where = new StringBuilder("                        AND A.\"ID\" IN (NULL");
            if (idList != null && idList.Any() == true)
            {
                for (var i = 0; i < idList.Count(); i++)
                {
                    var name = string.Format(":ID{0}", i);

                    where.AppendFormat(",{0}", name);

                    prms.Add(new BindModel { Name = name, Type = OracleDbType.Int64, Object = idList.ElementAt(i), Direct = ParameterDirection.Input });

                }

            }
            where.Append(")");

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"チェックリスト項目名_ID\" AS \"CHECKLIST_ITEMNAME_ID\"");
            sql.AppendLine("    ,A.\"チェックリスト名_ID\" AS \"CHECKLIST_NAME_ID\"");
            sql.AppendLine("    ,A.\"確認時期_ID\" AS \"CONFIRM_SEASON_ID\"");
            sql.AppendLine("    ,A.\"ID\" AS \"CHECKLIST_ID\"");
            sql.AppendLine("    ,C.\"確認時期名\" AS \"CONFIRM_SEASON_NAME\"");
            sql.AppendLine("    ,A.\"確認結果\" AS \"CONFIRM_RESULTS\"");
            sql.AppendLine("    ,C.\"時期順\" AS \"SEASON_SEQUENCE\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"目標進度_チェックリスト\" A");
            sql.AppendLine("    INNER JOIN");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         MAX(C.\"ID\") AS \"ID\"");
            sql.AppendLine("                        ,C.\"チェックリスト名_ID\"");
            sql.AppendLine("                        ,C.\"チェックリスト項目名_ID\"");
            sql.AppendLine("                        ,C.\"確認時期_ID\"");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"目標進度_チェックリスト名\" A");
            sql.AppendLine("                        INNER JOIN \"目標進度_チェックリスト_項目名\" B");
            sql.AppendLine("                        ON A.\"ID\" = B.\"チェックリスト名_ID\"");
            sql.AppendLine("                        INNER JOIN \"目標進度_チェックリスト\" C");
            sql.AppendLine("                        ON A.\"ID\" = C.\"チェックリスト名_ID\"");
            sql.AppendLine("                        AND B.\"ID\" = C.\"チェックリスト項目名_ID\"");
            sql.AppendLine("                        INNER JOIN \"目標進度_確認時期\" D");
            sql.AppendLine("                        ON A.\"GENERAL_CODE\" = D.\"GENERAL_CODE\"");
            sql.AppendLine("                        AND C.\"確認時期_ID\" = D.\"ID\"");
            sql.AppendLine("                    WHERE 0 = 0");
            sql.AppendLine("                        AND D.\"FLAG_DISP\" = 1");
            sql.AppendLine(where.ToString());
            sql.AppendLine("                    GROUP BY");
            sql.AppendLine("                         C.\"チェックリスト名_ID\"");
            sql.AppendLine("                        ,C.\"チェックリスト項目名_ID\"");
            sql.AppendLine("                        ,C.\"確認時期_ID\"");
            sql.AppendLine("                ) B");
            sql.AppendLine("    ON A.\"ID\" = B.\"ID\"");
            sql.AppendLine("    INNER JOIN");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         A.\"ID\" AS \"チェックリスト名_ID\"");
            sql.AppendLine("                        ,B.\"ID\" AS \"確認時期_ID\"");
            sql.AppendLine("                        ,B.\"確認時期名\"");
            sql.AppendLine("                        ,B.\"時期順\"");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"目標進度_チェックリスト名\" A");
            sql.AppendLine("                        INNER JOIN \"目標進度_確認時期\" B");
            sql.AppendLine("                        ON A.\"GENERAL_CODE\" = B.\"GENERAL_CODE\"");
            sql.AppendLine("                    WHERE 0 = 0");
            sql.AppendLine("                        AND B.\"FLAG_DISP\" = 1");
            sql.AppendLine(where.ToString());
            sql.AppendLine("                ) C");
            sql.AppendLine("    ON A.\"チェックリスト名_ID\" = C.\"チェックリスト名_ID\"");
            sql.AppendLine("    AND A.\"確認時期_ID\" = C.\"確認時期_ID\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"チェックリスト項目名_ID\"");
            sql.AppendLine("    ,C.\"時期順\"");
            sql.AppendLine("    ,A.\"確認時期_ID\"");

            return db.ReadModelList<TargetProgressListConfirmSeasonModel>(sql.ToString(), prms);

        }

        /// <summary>
        /// 目標進度_確認時期の取得
        /// </summary>
        /// <param name="list">目標進度リスト項目</param>
        /// <returns></returns>
        private Dictionary<string, List<TargetProgressListConfirmSeasonModel>> GetMokuhyouSindoKakuninJikiMap(List<TargetProgressListRegistInModel> list)
        {
            var map = new Dictionary<string, List<TargetProgressListConfirmSeasonModel>>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"ID\" AS \"CONFIRM_SEASON_ID\"");
            sql.AppendLine("    ,A.\"確認時期名\" AS \"CONFIRM_SEASON_NAME\"");
            sql.AppendLine("    ,A.\"FLAG_DISP\" AS \"SEASON_FLAG_DISP\"");
            sql.AppendLine("    ,A.\"時期順\" AS \"SEASON_SEQUENCE\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"目標進度_確認時期\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"GENERAL_CODE\" = :GENERAL_CODE");
            sql.AppendLine("    AND A.\"FLAG_DISP\" = 1");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"時期順\"");
            sql.AppendLine("    ,A.\"ID\"");

            foreach (var generalCode in list.Select(x => x.GENERAL_CODE).Distinct())
            {
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = generalCode, Direct = ParameterDirection.Input }

                };

                map[generalCode] = db.ReadModelList<TargetProgressListConfirmSeasonModel>(sql.ToString(), prms);

            }

            return map;

        }
        #endregion

        #region 目標進度リスト項目追加
        /// <summary>
        /// 目標進度リスト項目追加
        /// </summary>
        /// <param name="list">目標進度リスト項目</param>
        /// <returns>追加可否</returns>
        public bool PostData(List<TargetProgressListRegistInModel> list)
        {
            var results = new List<bool>();

            // トランザクション開始
            db.Begin();

            // 登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                var checkListNameMap = new Dictionary<int, int>();
                var checkListGeneralCodeMap = new Dictionary<int, string>();
                var checkListMap = new Dictionary<long, int>();

                //目標進度リストマスター取得
                var masterMap = this.GetMokuhyouSindoListMasterMap(list);

                //目標進度_チェックリスト名追加
                results.Add(this.InsertCheckListName(list, checkListNameMap, checkListGeneralCodeMap));

                //目標進度_チェックリスト_項目名追加
                results.Add(this.InsertCheckListItemName(masterMap, checkListNameMap, checkListMap));

                //目標進度_チェックリスト追加
                results.Add(this.InsertCheckList(list, checkListGeneralCodeMap, checkListMap));

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
        /// 目標進度リストマスターの取得
        /// </summary>
        /// <param name="list">目標進度リスト項目</param>
        /// <returns></returns>
        private Dictionary<int, List<TargetProgressListMasterModel>> GetMokuhyouSindoListMasterMap(List<TargetProgressListRegistInModel> list)
        {
            var logic = new TargetProgressListMasterLogic();
            logic.SetDBAccess(base.db);

            return list.Select(x => x.SPEC_NAME_ID).Distinct().ToDictionary(x => x, y => logic.GetTargetProgressListMaster(new TargetProgressListMasterSearchModel { 性能名_ID = y }));

        }

        /// <summary>
        /// 目標進度_チェックリスト名に追加
        /// </summary>
        /// <param name="list">目標進度リスト項目</param>
        /// <param name="checkListNameMap">目標進度_チェックリスト名キー連想配列</param>
        /// <param name="checkListGeneralCodeMap">目標進度_チェックリスト開発符号連想配列</param>
        /// <returns>追加可否</returns>
        private bool InsertCheckListName(List<TargetProgressListRegistInModel> list, Dictionary<int, int> checkListNameMap, Dictionary<int, string> checkListGeneralCodeMap)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"目標進度_チェックリスト名\"");
            sql.AppendLine("(");
            sql.AppendLine("     \"ID\"");
            sql.AppendLine("    ,\"GENERAL_CODE\"");
            sql.AppendLine("    ,\"性能名_ID\"");
            sql.AppendLine("    ,\"仕様\"");
            sql.AppendLine(")");
            sql.AppendLine("SELECT");
            sql.AppendLine("     :ID AS \"ID\"");
            sql.AppendLine("    ,:GENERAL_CODE AS \"GENERAL_CODE\"");
            sql.AppendLine("    ,:性能名_ID AS \"性能名_ID\"");
            sql.AppendLine("    ,TO_CHAR");
            sql.AppendLine("    (");
            sql.AppendLine("        (");
            sql.AppendLine("            CASE");
            sql.AppendLine("                WHEN C.\"NULL_COUNT\" = 0 THEN NULL");
            sql.AppendLine("                WHEN C.\"NULL_COUNT\" > 0 AND B.\"仕様\" IS NULL THEN 1 ");
            sql.AppendLine("                WHEN B.\"仕様\" <> 1 THEN 1");
            sql.AppendLine("                WHEN A.\"仕様\" >= 1 THEN (A.\"仕様\" + 1) ");
            sql.AppendLine("                ELSE NULL");
            sql.AppendLine("            END");
            sql.AppendLine("        )");
            sql.AppendLine("    ) AS \"仕様\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT ");
            sql.AppendLine("            TO_NUMBER(MIN(A.\"仕様\")) AS \"仕様\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"目標進度_チェックリスト名\" A");
            sql.AppendLine("        WHERE 0 = 0");
            sql.AppendLine("            AND A.\"GENERAL_CODE\" = :GENERAL_CODE");
            sql.AppendLine("            AND A.\"性能名_ID\" = :性能名_ID");
            sql.AppendLine("            AND REGEXP_LIKE(A.\"仕様\", '^[0-9]+$')");
            sql.AppendLine("            AND REGEXP_LIKE(A.\"仕様\", '^[1-9]')");
            sql.AppendLine("            AND NOT EXISTS");
            sql.AppendLine("                            (");
            sql.AppendLine("                                SELECT");
            sql.AppendLine("                                    *");
            sql.AppendLine("                                FROM");
            sql.AppendLine("                                    \"目標進度_チェックリスト名\" B");
            sql.AppendLine("                                WHERE 0 = 0");
            sql.AppendLine("                                    AND B.\"GENERAL_CODE\" = :GENERAL_CODE");
            sql.AppendLine("                                    AND B.\"性能名_ID\" = :性能名_ID");
            sql.AppendLine("                                    AND REGEXP_LIKE(B.\"仕様\", '^[0-9]+$')");
            sql.AppendLine("                                    AND REGEXP_LIKE(B.\"仕様\", '^[1-9]')");
            sql.AppendLine("                                    AND (TO_NUMBER(A.\"仕様\") + 1) = TO_NUMBER(B.\"仕様\")");
            sql.AppendLine("                            )");
            sql.AppendLine("    ) A");
            sql.AppendLine("    ,(");
            sql.AppendLine("        SELECT ");
            sql.AppendLine("            TO_NUMBER(MIN(A.\"仕様\")) AS \"仕様\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"目標進度_チェックリスト名\" A");
            sql.AppendLine("        WHERE 0 = 0");
            sql.AppendLine("            AND A.\"GENERAL_CODE\" = :GENERAL_CODE");
            sql.AppendLine("            AND A.\"性能名_ID\" = :性能名_ID");
            sql.AppendLine("            AND REGEXP_LIKE(A.\"仕様\", '^[0-9]+$')");
            sql.AppendLine("            AND REGEXP_LIKE(A.\"仕様\", '^[1-9]')");
            sql.AppendLine("    ) B");
            sql.AppendLine("    ,(");
            sql.AppendLine("        SELECT ");
            sql.AppendLine("            COUNT(*) AS \"NULL_COUNT\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"目標進度_チェックリスト名\" A");
            sql.AppendLine("        WHERE 0 = 0");
            sql.AppendLine("            AND A.\"GENERAL_CODE\" = :GENERAL_CODE");
            sql.AppendLine("            AND A.\"性能名_ID\" = :性能名_ID");
            sql.AppendLine("            AND A.\"仕様\" IS NULL");
            sql.AppendLine("    ) C");

            var results = new List<bool>();

            foreach (var targetListItem in list)
            {
                var id = this.GetCheckListNameID();

                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = id, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = targetListItem.GENERAL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":性能名_ID", Type = OracleDbType.Int32, Object = targetListItem.SPEC_NAME_ID, Direct = ParameterDirection.Input }
                };

                //追加
                results.Add(db.InsertData(sql.ToString(), prms));

                //追加したIDと性能IDをセット
                checkListNameMap[id] = targetListItem.SPEC_NAME_ID;

                //追加したIDと開発符号をセット
                checkListGeneralCodeMap[id] = targetListItem.GENERAL_CODE;

            }

            return results.All(x => x == true);

        }

        /// <summary>
        /// 目標進度_チェックリスト名のIDを取得
        /// </summary>
        /// <returns></returns>
        private int GetCheckListNameID()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    NVL(MAX(ID),0) + 1 AS \"ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"目標進度_チェックリスト名\"");

            return Convert.ToInt32(db.ReadDataTable(sql.ToString(), null).Rows[0]["ID"]);

        }

        /// <summary>
        /// 目標進度_チェックリスト_項目名に追加
        /// </summary>
        /// <param name="masterMap">目標進度リストマスター連想配列</param>
        /// <param name="checkListNameMap">目標進度_チェックリスト名キー連想配列</param>
        /// <param name="checkListMap">目標進度_チェックリスト_項目名連想配列</param>
        /// <returns>追加可否</returns>
        private bool InsertCheckListItemName(Dictionary<int, List<TargetProgressListMasterModel>> masterMap, Dictionary<int, int> checkListNameMap, Dictionary<long, int> checkListMap)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"目標進度_チェックリスト_項目名\"");
            sql.AppendLine("(");
            sql.AppendLine("     \"ID\"");
            sql.AppendLine("    ,\"チェックリスト名_ID\"");
            sql.AppendLine("    ,\"大項目\"");
            sql.AppendLine("    ,\"中項目\"");
            sql.AppendLine("    ,\"小項目\"");
            sql.AppendLine("    ,\"目標値\"");
            sql.AppendLine("    ,\"SORT_NO\"");
            sql.AppendLine("    ,\"SOURCE\"");
            sql.AppendLine("    ,\"FLAG_DISP\"");
            sql.AppendLine(")");
            sql.AppendLine("VALUES");
            sql.AppendLine("(");
            sql.AppendLine("     :ID");
            sql.AppendLine("    ,:CHECKLIST_NAME_ID");
            sql.AppendLine("    ,:LARGE_CLASSIFICATION");
            sql.AppendLine("    ,:MIDDLE_CLASSIFICATION");
            sql.AppendLine("    ,:SMALL_CLASSIFICATION");
            sql.AppendLine("    ,:TARGET_VALUE");
            sql.AppendLine("    ,:SORT_NO");
            sql.AppendLine("    ,'項目マスター'");
            sql.AppendLine("    ,1");
            sql.AppendLine(")");

            var results = new List<bool>();

            foreach (var kv in checkListNameMap)
            {
                var checkListNameID = kv.Key;
                var masterList = masterMap[kv.Value];

                foreach (var master in masterList)
                {
                    var id = this.GetCheckListItemNameID();

                    //パラメータ
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = id, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":CHECKLIST_NAME_ID", Type = OracleDbType.Int64, Object = checkListNameID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":LARGE_CLASSIFICATION", Type = OracleDbType.Varchar2, Object = master.大項目, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":MIDDLE_CLASSIFICATION", Type = OracleDbType.Varchar2, Object = master.中項目, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":SMALL_CLASSIFICATION", Type = OracleDbType.Varchar2, Object = master.小項目, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":TARGET_VALUE", Type = OracleDbType.Varchar2, Object = master.目標値, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":SORT_NO", Type = OracleDbType.Int64, Object = master.SORT_NO, Direct = ParameterDirection.Input }
                    };

                    //追加
                    results.Add(db.InsertData(sql.ToString(), prms));

                    //追加したIDと目標進度_チェックリスト名のIDをセット
                    checkListMap[id] = checkListNameID;

                }

            }

            return results.All(x => x == true);

        }

        /// <summary>
        /// 目標進度_チェックリスト_項目名のIDを取得
        /// </summary>
        /// <returns></returns>
        private long GetCheckListItemNameID()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    NVL(MAX(ID),0) + 1 AS \"ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"目標進度_チェックリスト_項目名\"");

            return Convert.ToInt64(db.ReadDataTable(sql.ToString(), null).Rows[0]["ID"]);

        }

        /// <summary>
        /// 目標進度_チェックリストに追加
        /// </summary>
        /// <param name="list">目標進度リスト項目</param>
        /// <param name="checkListGeneralCodeMap">目標進度_チェックリスト開発符号連想配列</param>
        /// <param name="checkListMap">目標進度_チェックリスト_項目名連想配列</param>
        /// <returns>追加可否</returns>
        private bool InsertCheckList(List<TargetProgressListRegistInModel> list, Dictionary<int, string> checkListGeneralCodeMap, Dictionary<long, int> checkListMap)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"目標進度_チェックリスト\"");
            sql.AppendLine("(");
            sql.AppendLine("     \"ID\"");
            sql.AppendLine("    ,\"確認時期\"");
            sql.AppendLine("    ,\"編集日\"");
            sql.AppendLine("    ,\"編集者_ID\"");
            sql.AppendLine("    ,\"チェックリスト名_ID\"");
            sql.AppendLine("    ,\"チェックリスト項目名_ID\"");
            sql.AppendLine("    ,\"確認時期_ID\"");
            sql.AppendLine("    ,\"編集者_LOGIN_ID\"");
            sql.AppendLine(")");
            sql.AppendLine("SELECT");
            sql.AppendLine("     (NVL(MAX(ID), 0) + 1) AS \"ID\"");
            sql.AppendLine("    ,:確認時期 AS \"確認時期\"");
            sql.AppendLine("    ,SYSTIMESTAMP AS \"編集日\"");
            sql.AppendLine("    ,:編集者_ID AS \"編集者_ID\"");
            sql.AppendLine("    ,:チェックリスト名_ID AS \"チェックリスト名_ID\"");
            sql.AppendLine("    ,:チェックリスト項目名_ID AS \"チェックリスト項目名_ID\"");
            sql.AppendLine("    ,:確認時期_ID AS \"確認時期_ID\"");
            sql.AppendLine("    ,:編集者_ID AS \"編集者_LOGIN_ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"目標進度_チェックリスト\"");

            var results = new List<bool>();

            var editorID = list.First().EDITOR_ID;

            //目標進度_確認時期取得
            var kakuninJikiMap = this.GetMokuhyouSindoKakuninJikiMap(list);

            foreach (var kv in checkListMap)
            {
                var checkListItemNameID = kv.Key;
                var checkListNameID = kv.Value;
                var generalCode = checkListGeneralCodeMap[checkListNameID];

                foreach (var kakunin in kakuninJikiMap[generalCode])
                {
                    //パラメータ
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":確認時期", Type = OracleDbType.Varchar2, Object = kakunin.CONFIRM_SEASON_NAME, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":編集者_ID", Type = OracleDbType.Varchar2, Object = editorID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":チェックリスト名_ID", Type = OracleDbType.Int64, Object = checkListNameID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":チェックリスト項目名_ID", Type = OracleDbType.Int64, Object = checkListItemNameID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":確認時期_ID", Type = OracleDbType.Int32, Object = kakunin.CONFIRM_SEASON_ID, Direct = ParameterDirection.Input },
                    };

                    //追加
                    results.Add(db.InsertData(sql.ToString(), prms));

                }

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 目標進度リスト項目更新
        /// <summary>
        /// 目標進度_チェックリスト 項目更新
        /// </summary>
        /// <param name="list">目標進度_チェックリスト項目</param>
        /// <returns>更新可否</returns>
        public bool PutData(List<TargetProgressListUpdateInModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                var checkListMap = new Dictionary<long, TargetProgressListUpdateInModel>();

                var insertList = list.Where(x => x.CHECKLIST_ITEMNAME_ID == 0 && x.DISPLAY_FLAG == 1).ToList();

                //目標進度_チェックリスト_項目名を追加
                results.Add(this.InsertCheckListItemName(insertList, checkListMap));

                //目標進度_チェックリストを追加
                results.Add(this.InsertCheckList(checkListMap));


                var updateList = list.Where(x => x.CHECKLIST_ITEMNAME_ID > 0).ToList();

                //目標進度_チェックリスト_項目名を更新
                results.Add(this.UpdateCheckListItemName(updateList));

                //目標進度_チェックリストを更新
                results.Add(this.UpdateCheckList(updateList));


                var deleteIdList = list.Select(x => x.CHECKLIST_NAME_ID).Distinct().ToList();

                //目標進度_チェックリストを削除
                results.Add(this.DeleteCheckList(deleteIdList));

                //目標進度_チェックリスト_項目名を削除
                results.Add(this.DeleteCheckListItemName(deleteIdList));

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
        /// 目標進度_チェックリスト_項目名を追加
        /// </summary>
        /// <param name="list">目標進度_チェックリスト項目</param>
        /// <param name="checkListMap">目標進度_チェックリスト連想配列</param>
        /// <returns></returns>
        private bool InsertCheckListItemName(List<TargetProgressListUpdateInModel> list, Dictionary<long, TargetProgressListUpdateInModel> checkListMap)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"目標進度_チェックリスト_項目名\"");
            sql.AppendLine("(");
            sql.AppendLine("     \"ID\"");
            sql.AppendLine("    ,\"チェックリスト名_ID\"");
            sql.AppendLine("    ,\"大項目\"");
            sql.AppendLine("    ,\"中項目\"");
            sql.AppendLine("    ,\"小項目\"");
            sql.AppendLine("    ,\"目標値\"");
            sql.AppendLine("    ,\"SORT_NO\"");
            sql.AppendLine("    ,\"FLAG_DISP\"");
            sql.AppendLine(")");
            sql.AppendLine("VALUES");
            sql.AppendLine("(");
            sql.AppendLine("     :ID");
            sql.AppendLine("    ,:CHECKLIST_NAME_ID");
            sql.AppendLine("    ,:LARGE_CLASSIFICATION");
            sql.AppendLine("    ,:MIDDLE_CLASSIFICATION");
            sql.AppendLine("    ,:SMALL_CLASSIFICATION");
            sql.AppendLine("    ,:TARGET_VALUE");
            sql.AppendLine("    ,:SORT_NO");
            sql.AppendLine("    ,1");
            sql.AppendLine(")");

            var results = new List<bool>();

            foreach (var mokuhyou in list)
            {
                var id = this.GetCheckListItemNameID();

                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = id, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CHECKLIST_NAME_ID", Type = OracleDbType.Int64, Object = mokuhyou.CHECKLIST_NAME_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":LARGE_CLASSIFICATION", Type = OracleDbType.Varchar2, Object = mokuhyou.LARGE_CLASSIFICATION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MIDDLE_CLASSIFICATION", Type = OracleDbType.Varchar2, Object = mokuhyou.MIDDLE_CLASSIFICATION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SMALL_CLASSIFICATION", Type = OracleDbType.Varchar2, Object = mokuhyou.SMALL_CLASSIFICATION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":TARGET_VALUE", Type = OracleDbType.Varchar2, Object = mokuhyou.TARGET_VALUE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SORT_NO", Type = OracleDbType.Int64, Object = mokuhyou.SORT_NO, Direct = ParameterDirection.Input }
                };

                //追加
                results.Add(db.InsertData(sql.ToString(), prms));

                //追加したIDと目標進度_チェックリスト名のIDをセット
                checkListMap[id] = mokuhyou;

            }

            return results.All(x => x == true);

        }

        /// <summary>
        /// 目標進度_チェックリストを追加
        /// </summary>
        /// <param name="map">目標進度_チェックリスト連想配列</param>
        /// <returns></returns>
        private bool InsertCheckList(Dictionary<long, TargetProgressListUpdateInModel> map)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"目標進度_チェックリスト\"");
            sql.AppendLine("(");
            sql.AppendLine("     \"ID\"");
            sql.AppendLine("    ,\"確認時期\"");
            sql.AppendLine("    ,\"確認結果\"");
            sql.AppendLine("    ,\"編集日\"");
            sql.AppendLine("    ,\"編集者_ID\"");
            sql.AppendLine("    ,\"チェックリスト名_ID\"");
            sql.AppendLine("    ,\"チェックリスト項目名_ID\"");
            sql.AppendLine("    ,\"確認時期_ID\"");
            sql.AppendLine("    ,\"編集者_LOGIN_ID\"");
            sql.AppendLine(")");
            sql.AppendLine("SELECT");
            sql.AppendLine("     (NVL(MAX(ID), 0) + 1) AS \"ID\"");
            sql.AppendLine("    ,:確認時期 AS \"確認時期\"");
            sql.AppendLine("    ,:確認結果 AS \"確認結果\"");
            sql.AppendLine("    ,SYSTIMESTAMP AS \"編集日\"");
            sql.AppendLine("    ,:編集者_ID AS \"編集者_ID\"");
            sql.AppendLine("    ,:チェックリスト名_ID AS \"チェックリスト名_ID\"");
            sql.AppendLine("    ,:チェックリスト項目名_ID AS \"チェックリスト項目名_ID\"");
            sql.AppendLine("    ,:確認時期_ID AS \"確認時期_ID\"");
            sql.AppendLine("    ,:編集者_LOGIN_ID AS \"編集者_LOGIN_ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"目標進度_チェックリスト\"");

            var results = new List<bool>();

            foreach (var kv in map)
            {
                var checkListItemNameID = kv.Key;
                var mokuhyou = kv.Value;

                for (var i = 0; i < mokuhyou.CONFIRM_SEASON_ID.Length; i++)
                {
                    //パラメータ
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":確認時期", Type = OracleDbType.Varchar2, Object = mokuhyou.CONFIRM_SEASON_NAME[i], Direct = ParameterDirection.Input },
                        new BindModel { Name = ":確認結果", Type = OracleDbType.Varchar2, Object = mokuhyou.CONFIRM_RESULTS[i], Direct = ParameterDirection.Input },
                        new BindModel { Name = ":編集者_ID", Type = OracleDbType.Varchar2, Object = mokuhyou.EDITOR_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":チェックリスト名_ID", Type = OracleDbType.Int64, Object = mokuhyou.CHECKLIST_NAME_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":チェックリスト項目名_ID", Type = OracleDbType.Int64, Object = checkListItemNameID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":確認時期_ID", Type = OracleDbType.Int32, Object = mokuhyou.CONFIRM_SEASON_ID[i], Direct = ParameterDirection.Input },
                        new BindModel { Name = ":編集者_LOGIN_ID", Type = OracleDbType.Varchar2, Object = mokuhyou.EDITOR_LOGIN_ID, Direct = ParameterDirection.Input }
                    };

                    //追加
                    results.Add(db.InsertData(sql.ToString(), prms));

                }

            }

            return results.All(x => x == true);

        }


        /// <summary>
        /// 目標進度_チェックリスト_項目名を更新
        /// </summary>
        /// <param name="list">「目標進度_チェックリスト_項目名」テーブルを更新</param>
        /// <returns>更新可否</returns>
        private bool UpdateCheckListItemName(List<TargetProgressListUpdateInModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"目標進度_チェックリスト_項目名\"");
            sql.AppendLine("SET");
            sql.AppendLine("     \"大項目\" = :LARGE_CLASSIFICATION");
            sql.AppendLine("    ,\"中項目\" = :MIDDLE_CLASSIFICATION");
            sql.AppendLine("    ,\"小項目\" = :SMALL_CLASSIFICATION");
            sql.AppendLine("    ,\"SORT_NO\" = :SORT_NO");
            sql.AppendLine("    ,\"目標値\" = :TARGET_VALUE");
            sql.AppendLine("    ,\"達成値\" = :ACHIEVED_VALUE");
            sql.AppendLine("    ,\"FLAG_DISP\" = :DISPLAY_FLAG");
            sql.AppendLine("    ,\"関連課_CODE\" = :RELATIONAL_DIVISION_CODE");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :CHECKLIST_ITEMNAME_ID");

            var results = new List<bool>();

            foreach (var targetListItem in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":LARGE_CLASSIFICATION", Type = OracleDbType.Varchar2, Object = targetListItem.LARGE_CLASSIFICATION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MIDDLE_CLASSIFICATION", Type = OracleDbType.Varchar2, Object = targetListItem.MIDDLE_CLASSIFICATION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SMALL_CLASSIFICATION", Type = OracleDbType.Varchar2, Object = targetListItem.SMALL_CLASSIFICATION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SORT_NO", Type = OracleDbType.Int64, Object = targetListItem.SORT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":TARGET_VALUE", Type = OracleDbType.Varchar2, Object = targetListItem.TARGET_VALUE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ACHIEVED_VALUE", Type = OracleDbType.Varchar2, Object = targetListItem.ACHIEVED_VALUE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":DISPLAY_FLAG", Type = OracleDbType.Int32, Object = targetListItem.DISPLAY_FLAG, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":RELATIONAL_DIVISION_CODE", Type = OracleDbType.Varchar2, Object = targetListItem.RELATIONAL_DIVISION_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CHECKLIST_ITEMNAME_ID", Type = OracleDbType.Int64, Object = targetListItem.CHECKLIST_ITEMNAME_ID, Direct = ParameterDirection.Input }

                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));
            }

            return results.All(x => x == true);

        }

        /// <summary>
        /// 目標進度_チェックリストを更新
        /// </summary>
        /// <param name="list">「目標進度_チェックリスト」テーブルを更新</param>
        /// <returns>更新可否</returns>
        private bool UpdateCheckList(List<TargetProgressListUpdateInModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"目標進度_チェックリスト\"");
            sql.AppendLine("SET");
            sql.AppendLine("     \"確認結果\" = :CONFIRM_RESULTS");
            sql.AppendLine("    ,\"編集日\" = :EDITED_DATE");
            sql.AppendLine("    ,\"編集者_ID\" = :EDITOR_ID");
            sql.AppendLine("    ,\"編集者_LOGIN_ID\" = :EDITOR_LOGIN_ID");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :CHECKLIST_ID");

            var results = new List<bool>();

            foreach (var targetListItem in list)
            {
                for (var i = 0; i < targetListItem.CONFIRM_SEASON_ID.Length; i++)
                {
                    //パラメータ
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":CONFIRM_RESULTS", Type = OracleDbType.Varchar2, Object = targetListItem.CONFIRM_RESULTS[i], Direct = ParameterDirection.Input },
                        new BindModel { Name = ":EDITED_DATE", Type = OracleDbType.Date, Object = targetListItem.EDITED_DATE, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":EDITOR_ID", Type = OracleDbType.Varchar2, Object = targetListItem.EDITOR_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":EDITOR_LOGIN_ID", Type = OracleDbType.Varchar2, Object = targetListItem.EDITOR_LOGIN_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":CHECKLIST_ID", Type = OracleDbType.Int64, Object = targetListItem.CHECKLIST_ID[i], Direct = ParameterDirection.Input },
                    };

                    //更新
                    results.Add(db.UpdateData(sql.ToString(), prms));
                }

            }

            return results.All(x => x == true);

        }

        /// <summary>
        /// 目標進度_チェックリストを削除
        /// </summary>
        /// <param name="idList">チェックリスト名_ID</param>
        /// <returns></returns>
        /// <remarks>削除対象は任意項目のみ</remarks>
        private bool DeleteCheckList(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"目標進度_チェックリスト\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND EXISTS");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                        *");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"目標進度_チェックリスト_項目名\" B");
            sql.AppendLine("                    WHERE 0 = 0");
            sql.AppendLine("                        AND B.\"SOURCE\" IS NULL");
            sql.AppendLine("                        AND B.\"FLAG_DISP\" <> 1");
            sql.AppendLine("                        AND A.\"チェックリスト項目名_ID\" =  B.\"ID\"");

            sql.Append("                        AND B.\"チェックリスト名_ID\" IN (NULL");

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
        /// 目標進度_チェックリスト_項目名を削除
        /// </summary>
        /// <param name="idList">チェックリスト名_ID</param>
        /// <returns></returns>
        /// <remarks>削除対象は任意項目のみ</remarks>
        private bool DeleteCheckListItemName(List<long> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"目標進度_チェックリスト_項目名\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"SOURCE\" IS NULL");
            sql.AppendLine("    AND \"FLAG_DISP\" <> 1");

            sql.Append("    AND \"チェックリスト名_ID\" IN (NULL");

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

        #region 目標進度リスト削除
        /// <summary>
        /// 目標進度リスト削除
        /// </summary>
        /// <param name="val">目標進度リスト項目</param>
        /// <returns>削除可否</returns>
        public bool DeleteData(TargetProgressListDeleteInModel val)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //「目標進度_チェックリスト名」テーブルから削除
            results.Add(this.DeleteCheckListName(val));

            //「目標進度_チェックリスト項目名」テーブルから削除
            results.Add(this.DeleteCheckListItemName(val));

            //「目標進度_チェックリスト」テーブルから削除
            results.Add(this.DeleteCheckList(val));

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
        /// 「目標進度_チェックリスト名」テーブルから削除
        /// </summary>
        /// <param name="val">チェックリスト名ID</param>
        /// <returns>削除可否</returns>
        private bool DeleteCheckListName(TargetProgressListDeleteInModel val)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"目標進度_チェックリスト名\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    \"ID\" = :ID");

            //IDで削除
            prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = val.CHECKLIST_NAME_ID, Direct = ParameterDirection.Input });

            return db.DeleteData(sql.ToString(), prms);

        }

        /// <summary>
        /// 「目標進度_チェックリスト_項目名」テーブルから削除
        /// </summary>
        /// <param name="val">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteCheckListItemName(TargetProgressListDeleteInModel val)
        {
            var prms = new List<BindModel>();

            // SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"目標進度_チェックリスト_項目名\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    \"チェックリスト名_ID\" = :CHECKLIST_NAME_ID");

            // チェックリスト名IDで削除
            prms.Add(new BindModel { Name = ":CHECKLIST_NAME_ID", Type = OracleDbType.Int64, Object = val.CHECKLIST_NAME_ID, Direct = ParameterDirection.Input });

            return db.DeleteData(sql.ToString(), prms);

        }

        /// <summary>
        /// 「目標進度_チェックリスト」テーブルから削除
        /// </summary>
        /// <param name="val">チェックリスト名ID</param>
        /// <returns>削除可否</returns>
        private bool DeleteCheckList(TargetProgressListDeleteInModel val)
        {
            var prms = new List<BindModel>();

            // SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"目標進度_チェックリスト\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    \"チェックリスト名_ID\" = :CHECKLIST_NAME_ID");

            // チェックリスト名IDで削除
            prms.Add(new BindModel { Name = ":CHECKLIST_NAME_ID", Type = OracleDbType.Int64, Object = val.CHECKLIST_NAME_ID, Direct = ParameterDirection.Input });

            return db.DeleteData(sql.ToString(), prms);
        }
        #endregion
    }
}