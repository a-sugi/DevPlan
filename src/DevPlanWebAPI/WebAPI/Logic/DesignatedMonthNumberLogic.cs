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
    /// 指定月台数リスト業務ロジッククラス
    /// </summary>
    public class DesignatedMonthNumberLogic : TestCarBaseLogic
    {
        #region メンバ変数
        #endregion

        #region 試験車取得
        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TestCarCommonModel> GetTestCar(DesignatedMonthNumberSearchModel cond)
        {
            var prms = new List<BindModel>();

            var historyParm = new List<BindModel>
            {
                new BindModel { Name = ":管理票発行有無", Object = "済", Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

            };

            var sql = new StringBuilder();

            var from = DateTimeUtil.GetDate(cond.START_DATE);
            var to = DateTimeUtil.GetDate(cond.END_DATE);

            //取得種別ごとの分岐
            switch (cond.TARGET_TYPE)
            {
                //保有台数
                case DesignatedMonthNumberTargetType.Possession:
                    //SQL
                    sql = base.GetBaseTestCarSql(historyParm);

                    //条件
                    sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

                    //期間(From)
                    if (from != null)
                    {
                        sql.AppendLine("    AND KK.\"正式取得日\" < (:START_DATE + 1)");
                        sql.AppendLine("    AND");
                        sql.AppendLine("        (");
                        sql.AppendLine("            KK.\"廃却決済承認年月\" IS NULL");
                        sql.AppendLine("            OR");
                        sql.AppendLine("            KK.\"廃却決済承認年月\" > :START_DATE");
                        sql.AppendLine("        )");

                        prms.Add(new BindModel { Name = ":START_DATE", Object = from, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }
                    else
                    {
                        sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");

                    }

                    //パラメータ
                    prms.AddRange(historyParm);
                    break;

                //新規作成
                case DesignatedMonthNumberTargetType.New:
                    //SQL
                    sql = base.GetBaseTestCarSql(isHistoryMax: false);

                    //期間(From)
                    if (from != null)
                    {
                        sql.AppendLine("    AND KK.\"正式取得日\" >= :START_DATE");
                        sql.AppendLine("    AND KR.\"完成日\" >= :START_DATE");

                        prms.Add(new BindModel { Name = ":START_DATE", Object = from, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }

                    //期間(From)
                    if (to != null)
                    {
                        sql.AppendLine("    AND KK.\"正式取得日\" < (:END_DATE + 1)");
                        sql.AppendLine("    AND KR.\"完成日\" < (:END_DATE + 1)");

                        prms.Add(new BindModel { Name = ":END_DATE", Object = to, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }
                    break;

                //改修
                case DesignatedMonthNumberTargetType.Refurbishment:
                    //SQL
                    sql = base.GetBaseTestCarSql(isHistoryMax: false);

                    //条件
                    sql.AppendLine("    AND KR.\"履歴NO\" <> 1");

                    //期間(From)
                    if (from != null)
                    {
                        sql.AppendLine("    AND KR.\"完成日\" >= :START_DATE");

                        prms.Add(new BindModel { Name = ":START_DATE", Object = from, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }

                    //期間(From)
                    if (to != null)
                    {
                        sql.AppendLine("    AND KR.\"完成日\" < (:END_DATE + 1)");

                        prms.Add(new BindModel { Name = ":END_DATE", Object = to, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }
                    break;

                //廃却
                case DesignatedMonthNumberTargetType.Disposal:
                    //SQL
                    sql = base.GetBaseTestCarSql(historyParm);

                    //条件
                    sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

                    //期間(From)
                    if (from != null)
                    {
                        sql.AppendLine("    AND KK.\"廃却決済承認年月\" >= :START_DATE");

                        prms.Add(new BindModel { Name = ":START_DATE", Object = from, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }

                    //期間(From)
                    if (to != null)
                    {
                        sql.AppendLine("    AND KK.\"廃却決済承認年月\" < (:END_DATE + 1)");

                        prms.Add(new BindModel { Name = ":END_DATE", Object = to, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }

                    //パラメータ
                    prms.AddRange(historyParm);
                    break;

            }

            //試算種別
            if (cond.ASSET_TYPE != AssetType.All)
            {
                sql.AppendLine("    AND KR.\"資産種別\" = :資産種別");

                prms.Add(new BindModel { Name = ":資産種別", Object = cond.ASSET_TYPE, Type = OracleDbType.Int32, Direct = ParameterDirection.Input });

            }

            //検索条件
            base.SetStringWhere(sql, prms, "DD", "ESTABLISHMENT", cond.ESTABLISHMENT);
            base.SetStringWhere(sql, prms, "KK", "管理票NO", cond.管理票NO);
            base.SetStringWhere(sql, prms, "SD", "SECTION_ID", cond.SECTION_ID);
            base.SetStringWhere(sql, prms, "SG", "SECTION_GROUP_ID", cond.SECTION_GROUP_ID);
            base.SetStringWhere(sql, prms, "KR", "開発符号", cond.開発符号);
            base.SetStringWhere(sql, prms, "KR", "仕向地", cond.仕向地);
            base.SetStringWhere(sql, prms, "KR", "試作時期", cond.試作時期);
            base.SetStringWhere(sql, prms, "KR", "号車", cond.号車);
            base.SetStringWhere(sql, prms, "KR", "車体番号", cond.車体番号);
            base.SetStringWhere(sql, prms, "KR", "固定資産NO", cond.固定資産NO);

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    KK.\"管理票NO\"");

            //取得
            return db.ReadModelList<TestCarCommonModel>(sql.ToString(), prms);

        }
        #endregion

        #region 台数集計結果取得
        /// <summary>
        /// 台数集計結果取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        internal List<NumberAggregateModel> GetTestCarAggregate(DesignatedMonthNumberSearchModel cond)
        {
            var prms = new List<BindModel>();

            var from = DateTimeUtil.GetDate(cond.START_DATE);
            var to = DateTimeUtil.GetDate(cond.END_DATE);

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"DEPARTMENT_CODE\"");
            sql.AppendLine("    ,A.\"SECTION_CODE\"");
            sql.AppendLine("    ,A.\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,NVL(SUM(B.\"WHITE_NUMBER\"),0) AS \"WHITE_NUMBER\"");
            sql.AppendLine("    ,NVL(SUM(B.\"NO_NUMBER\"),0) AS \"NO_NUMBER\"");
            sql.AppendLine("    ,NVL(SUM(B.\"OUT_ASSET\"),0) AS \"OUT_ASSET\"");
            sql.AppendLine("    ,NVL(SUM(B.\"LEASE\"),0) AS \"LEASE\"");
            sql.AppendLine("    ,NVL(SUM(B.\"WHITE_NUMBER\"),0) + NVL(SUM(B.\"NO_NUMBER\"),0) + NVL(SUM(B.\"OUT_ASSET\"),0) + NVL(SUM(B.\"LEASE\"),0) AS \"TOTAL\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             A.\"DEPARTMENT_CODE\"");
            sql.AppendLine("            ,B.\"SECTION_CODE\"");
            sql.AppendLine("            ,C.\"SECTION_GROUP_CODE\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"DEPARTMENT_DATA\" A");
            sql.AppendLine("            INNER JOIN \"SECTION_DATA\" B");
            sql.AppendLine("            ON A.\"DEPARTMENT_ID\" = B.\"DEPARTMENT_ID\"");
            sql.AppendLine("            INNER JOIN \"SECTION_GROUP_DATA\" C");
            sql.AppendLine("            ON B.\"SECTION_ID\" = C.\"SECTION_ID\"");
            sql.AppendLine("        WHERE 0 = 0");

            //検索条件
            base.SetStringWhere(sql, prms, "A", "ESTABLISHMENT", cond.ESTABLISHMENT);
            base.SetStringWhere(sql, prms, "B", "SECTION_ID", cond.SECTION_ID);
            base.SetStringWhere(sql, prms, "C", "SECTION_GROUP_ID", cond.SECTION_GROUP_ID);

            sql.AppendLine("        GROUP BY");
            sql.AppendLine("             A.\"DEPARTMENT_CODE\"");
            sql.AppendLine("            ,B.\"SECTION_CODE\"");
            sql.AppendLine("            ,C.\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ) A");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         A.\"DEPARTMENT_CODE\"");
            sql.AppendLine("                        ,B.\"SECTION_CODE\"");
            sql.AppendLine("                        ,C.\"SECTION_GROUP_CODE\"");
            sql.AppendLine("                        ,D.\"WHITE_NUMBER\"");
            sql.AppendLine("                        ,D.\"NO_NUMBER\"");
            sql.AppendLine("                        ,D.\"OUT_ASSET\"");
            sql.AppendLine("                        ,D.\"LEASE\"");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"DEPARTMENT_DATA\" A");
            sql.AppendLine("                        INNER JOIN \"SECTION_DATA\" B");
            sql.AppendLine("                        ON A.\"DEPARTMENT_ID\" = B.\"DEPARTMENT_ID\"");
            sql.AppendLine("                        INNER JOIN \"SECTION_GROUP_DATA\" C");
            sql.AppendLine("                        ON B.\"SECTION_ID\" = C.\"SECTION_ID\"");
            sql.AppendLine("                        INNER JOIN");
            sql.AppendLine("                                    (");
            sql.AppendLine("                                        SELECT");
            sql.AppendLine("                                             B.\"管理責任部署\" AS \"SECTION_GROUP_ID\"");
            sql.AppendLine("                                            ,SUM(");
            sql.AppendLine("                                                CASE");
            sql.AppendLine("                                                    WHEN B.\"登録ナンバー\" IS NOT NULL AND (B.\"固定資産NO\" IS NOT NULL OR B.\"三鷹移管先固資NO\" IS NOT NULL) THEN 1");
            sql.AppendLine("                                                    ELSE 0");
            sql.AppendLine("                                                END");
            sql.AppendLine("                                            ) AS \"WHITE_NUMBER\"");
            sql.AppendLine("                                            ,SUM(");
            sql.AppendLine("                                                CASE");
            sql.AppendLine("                                                    WHEN B.\"登録ナンバー\" IS NULL AND (B.\"固定資産NO\" IS NOT NULL OR B.\"三鷹移管先固資NO\" IS NOT NULL) THEN 1");
            sql.AppendLine("                                                    ELSE 0");
            sql.AppendLine("                                                END");
            sql.AppendLine("                                            ) AS \"NO_NUMBER\"");
            sql.AppendLine("                                            ,SUM(");
            sql.AppendLine("                                                CASE");
            sql.AppendLine("                                                    WHEN B.\"登録ナンバー\" IS NULL AND B.\"固定資産NO\" IS NULL AND B.\"三鷹移管先固資NO\" IS NULL THEN 1");
            sql.AppendLine("                                                    ELSE 0");
            sql.AppendLine("                                                END");
            sql.AppendLine("                                            ) AS \"OUT_ASSET\"");
            sql.AppendLine("                                            ,SUM(");
            sql.AppendLine("                                                CASE");
            sql.AppendLine("                                                    WHEN B.\"登録ナンバー\" IS NOT NULL AND B.\"固定資産NO\" IS NULL AND B.\"三鷹移管先固資NO\" IS NULL THEN 1");
            sql.AppendLine("                                                    ELSE 0");
            sql.AppendLine("                                                END");
            sql.AppendLine("                                            ) AS \"LEASE\"");
            sql.AppendLine("                                        FROM");
            sql.AppendLine("                                            \"試験車基本情報\" A");
            sql.AppendLine("                                            INNER JOIN \"試験車履歴情報\" B");
            sql.AppendLine("                                            ON A.\"データID\" = B.\"データID\"");

            //取得種別ごとの分岐
            switch (cond.TARGET_TYPE)
            {
                //保有台数、廃却
                case DesignatedMonthNumberTargetType.Possession:
                case DesignatedMonthNumberTargetType.Disposal:
                    sql.AppendLine("                                            INNER JOIN");
                    sql.AppendLine("                                                        (");
                    sql.AppendLine("                                                            SELECT");
                    sql.AppendLine("                                                                 A.\"データID\"");
                    sql.AppendLine("                                                                ,MAX(A.\"履歴NO\") AS \"履歴NO\"");
                    sql.AppendLine("                                                            FROM");
                    sql.AppendLine("                                                                \"試験車履歴情報\" A");
                    sql.AppendLine("                                                            WHERE 0 = 0");
                    sql.AppendLine("                                                                AND A.\"管理票発行有無\" = '済'");
                    sql.AppendLine("                                                            GROUP BY");
                    sql.AppendLine("                                                                A.\"データID\"");
                    sql.AppendLine("                                                        ) C");
                    sql.AppendLine("                                            ON B.\"データID\" = C.\"データID\"");
                    sql.AppendLine("                                            AND B.\"履歴NO\" = C.\"履歴NO\"");
                    break;

            }
            
            sql.AppendLine("                                        WHERE 0 = 0");

            //取得種別ごとの分岐
            switch (cond.TARGET_TYPE)
            {
                //保有台数
                case DesignatedMonthNumberTargetType.Possession:
                    //期間(From)
                    if (from != null)
                    {
                        sql.AppendLine("                                            AND A.\"正式取得日\" < (:START_DATE + 1)");
                        sql.AppendLine("                                            AND");
                        sql.AppendLine("                                                (");
                        sql.AppendLine("                                                    A.\"廃却決済承認年月\" IS NULL");
                        sql.AppendLine("                                                    OR");
                        sql.AppendLine("                                                    A.\"廃却決済承認年月\" > :START_DATE");
                        sql.AppendLine("                                                )");

                        prms.Add(new BindModel { Name = ":START_DATE", Object = from, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }
                    else
                    {
                        sql.AppendLine("                                            AND A.\"廃却決済承認年月\" IS NULL");

                    }
                    break;

                //新規作成
                case DesignatedMonthNumberTargetType.New:
                    //期間(From)
                    if (from != null)
                    {
                        sql.AppendLine("                                            AND A.\"正式取得日\" >= :START_DATE");
                        sql.AppendLine("                                            AND B.\"完成日\" >= :START_DATE");

                        prms.Add(new BindModel { Name = ":START_DATE", Object = from, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }

                    //期間(From)
                    if (to != null)
                    {
                        sql.AppendLine("                                            AND A.\"正式取得日\" < (:END_DATE + 1)");
                        sql.AppendLine("                                            AND B.\"完成日\" < (:END_DATE + 1)");

                        prms.Add(new BindModel { Name = ":END_DATE", Object = to, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }
                    break;

                //改修
                case DesignatedMonthNumberTargetType.Refurbishment:
                    //条件
                    sql.AppendLine("                                            AND B.\"履歴NO\" <> 1");

                    //期間(From)
                    if (from != null)
                    {
                        sql.AppendLine("                                            AND B.\"完成日\" >= :START_DATE");

                        prms.Add(new BindModel { Name = ":START_DATE", Object = from, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }

                    //期間(From)
                    if (to != null)
                    {
                        sql.AppendLine("                                            AND B.\"完成日\" < (:END_DATE + 1)");

                        prms.Add(new BindModel { Name = ":END_DATE", Object = to, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }
                    break;

                //廃却
                case DesignatedMonthNumberTargetType.Disposal:
                    //期間(From)
                    if (from != null)
                    {
                        sql.AppendLine("                                            AND A.\"廃却決済承認年月\" >= :START_DATE");

                        prms.Add(new BindModel { Name = ":START_DATE", Object = from, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }

                    //期間(From)
                    if (to != null)
                    {
                        sql.AppendLine("                                            AND A.\"廃却決済承認年月\" < (:END_DATE + 1)");

                        prms.Add(new BindModel { Name = ":END_DATE", Object = to, Type = OracleDbType.Date, Direct = ParameterDirection.Input });

                    }
                    break;

            }

            //検索条件
            base.SetStringWhere(sql, prms, "A", "管理票NO", cond.管理票NO);
            base.SetStringWhere(sql, prms, "B", "管理責任部署", cond.SECTION_GROUP_ID);
            base.SetStringWhere(sql, prms, "B", "開発符号", cond.開発符号);
            base.SetStringWhere(sql, prms, "B", "仕向地", cond.仕向地);
            base.SetStringWhere(sql, prms, "B", "試作時期", cond.試作時期);
            base.SetStringWhere(sql, prms, "B", "号車", cond.号車);
            base.SetStringWhere(sql, prms, "B", "車体番号", cond.車体番号);
            base.SetStringWhere(sql, prms, "B", "固定資産NO", cond.固定資産NO);

            sql.AppendLine("                                        GROUP BY");
            sql.AppendLine("                                            B.\"管理責任部署\"");
            sql.AppendLine("                                    ) D");
            sql.AppendLine("                        ON C.\"SECTION_GROUP_ID\" = D.\"SECTION_GROUP_ID\"");
            sql.AppendLine("                    WHERE 0 = 0");

            //検索条件
            base.SetStringWhere(sql, prms, "A", "ESTABLISHMENT", cond.ESTABLISHMENT);
            base.SetStringWhere(sql, prms, "B", "SECTION_ID", cond.SECTION_ID);
            base.SetStringWhere(sql, prms, "C", "SECTION_GROUP_ID", cond.SECTION_GROUP_ID);

            sql.AppendLine("                ) B");
            sql.AppendLine("    ON NVL(A.\"DEPARTMENT_CODE\",CHR(0)) = NVL(B.\"DEPARTMENT_CODE\",CHR(0))");
            sql.AppendLine("    AND NVL(A.\"SECTION_CODE\",CHR(0)) = NVL(B.\"SECTION_CODE\",CHR(0))");
            sql.AppendLine("    AND NVL(A.\"SECTION_GROUP_CODE\",CHR(0)) = NVL(B.\"SECTION_GROUP_CODE\",CHR(0))");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("GROUP BY");
            sql.AppendLine("     A.\"DEPARTMENT_CODE\"");
            sql.AppendLine("    ,A.\"SECTION_CODE\"");
            sql.AppendLine("    ,A.\"SECTION_GROUP_CODE\"");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"DEPARTMENT_CODE\"");
            sql.AppendLine("    ,A.\"SECTION_CODE\"");
            sql.AppendLine("    ,A.\"SECTION_GROUP_CODE\"");

            //取得
            var list = db.ReadModelList<NumberAggregateModel>(sql.ToString(), prms);

            switch (cond.AGGREGATE_TYPE)
            {
                //部
                case AggregateType.Department:
                    list = list.GroupBy(x => new { x.DEPARTMENT_CODE }).Select(x => new NumberAggregateModel
                    {
                        //部コード
                        DEPARTMENT_CODE = x.Key.DEPARTMENT_CODE,

                        //白ナンバー
                        WHITE_NUMBER = x.Sum(y => y.WHITE_NUMBER ?? 0),

                        //ナンバー無し
                        NO_NUMBER = x.Sum(y => y.NO_NUMBER ?? 0),

                        //試算外
                        OUT_ASSET = x.Sum(y => y.OUT_ASSET ?? 0),

                        //リース
                        LEASE = x.Sum(y => y.LEASE ?? 0),

                        //合計
                        TOTAL = x.Sum(y => y.TOTAL ?? 0)

                    }).OrderBy(x => string.IsNullOrWhiteSpace(x.DEPARTMENT_CODE)).ThenBy(x => x.DEPARTMENT_CODE).ToList();
                    break;

                //課
                case AggregateType.Section:
                    list = list.GroupBy(x => new { x.DEPARTMENT_CODE, x.SECTION_CODE }).Select(x => new NumberAggregateModel
                    {
                        //部コード
                        DEPARTMENT_CODE = x.Key.DEPARTMENT_CODE,

                        //課コード
                        SECTION_CODE = x.Key.SECTION_CODE,

                        //白ナンバー
                        WHITE_NUMBER = x.Sum(y => y.WHITE_NUMBER ?? 0),

                        //ナンバー無し
                        NO_NUMBER = x.Sum(y => y.NO_NUMBER ?? 0),

                        //試算外
                        OUT_ASSET = x.Sum(y => y.OUT_ASSET ?? 0),

                        //リース
                        LEASE = x.Sum(y => y.LEASE ?? 0),

                        //合計
                        TOTAL = x.Sum(y => y.TOTAL ?? 0)

                    }).OrderBy(x => string.IsNullOrWhiteSpace(x.DEPARTMENT_CODE)).ThenBy(x => string.IsNullOrWhiteSpace(x.SECTION_CODE))
                    .ThenBy(x => x.DEPARTMENT_CODE).ThenBy(x => x.SECTION_CODE).ToList();
                    break;

            }

            //返却
            return list;

        }
        #endregion

        #region 更新
        /// <summary>
        /// 試験車の更新
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        public bool UpdateData(IEnumerable<TestCarCommonModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //試験車基本情報を更新
                results.Add(base.UpdateTestCarBaseInfo(list, (x => new List<BindModel>
                {
                    new BindModel { Name = ":駐車場番号", Type = OracleDbType.Varchar2, Object = x.駐車場番号, Direct = ParameterDirection.Input }

                })));

                //試験車履歴情報を更新
                results.Add(base.UpdateTestCarHistoryInfo(list, (x => new List<BindModel>
                {
                    new BindModel { Name = ":開発符号", Type = OracleDbType.Varchar2, Object = x.開発符号, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試作時期", Type = OracleDbType.Varchar2, Object = x.試作時期, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":号車", Type = OracleDbType.Varchar2, Object = x.号車, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":仕向地", Type = OracleDbType.Varchar2, Object = x.仕向地, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":外製車名", Type = OracleDbType.Varchar2, Object = x.外製車名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":車体番号", Type = OracleDbType.Varchar2, Object = x.車体番号, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験目的", Type = OracleDbType.Varchar2, Object = x.試験目的, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":完成日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(x.完成日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":研命ナンバー", Type = OracleDbType.Varchar2, Object = x.研命ナンバー, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":登録ナンバー", Type = OracleDbType.Varchar2, Object = x.登録ナンバー, Direct = ParameterDirection.Input }

                })));

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
        #endregion
    }
}