using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using DevPlanWebAPI.Common;
using System.Collections;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>CAP課題</remarks>
    public class CapLogic : BaseLogic
    {
        private readonly DateTime ConstDate = new DateTime(2001, 1, 1);

        #region CAP課題検索
        /// <summary>
        /// CAP課題検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<CapModel> Get(CapSearchModel cond)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            //SQL
            sql.AppendLine("SELECT");
            sql.AppendLine("     B.\"ID\"");
            sql.AppendLine("     ,B.\"FLAG_CAP確認\"");
            sql.AppendLine("     ,B.\"回答期限\"");
            sql.AppendLine("     ,B.\"FLAG_上司承認\"");
            sql.AppendLine("     ,D.DEPARTMENT_CODE 専門部名"); // 部コード
            sql.AppendLine("     ,B.\"専門部署名\"");    // 課コード
            sql.AppendLine("     ,B.\"対策予定\"");
            sql.AppendLine("     ,B.\"対応策\"");
            sql.AppendLine("     ,B.対策案編集日");
            sql.AppendLine("     ,B.対策案編集者_ID");
            sql.AppendLine("     ,(SELECT L.NAME FROM PERSONEL_LIST L WHERE B.対策案編集者_ID = L.PERSONEL_ID) AS 対策案編集者");
            sql.AppendLine("     ,B.\"事前把握有無\"");
            sql.AppendLine("     ,B.\"分類\"");
            sql.AppendLine("     ,F.\"意味\" AS 分類意味");
            sql.AppendLine("     ,B.\"評価レベル\"");
            sql.AppendLine("     ,G.\"意味\" AS 評価意味");
            sql.AppendLine("     ,G.\"項目\" AS 評価項目");
            sql.AppendLine("     ,G.\"レベル基準\"");
            sql.AppendLine("     ,G.\"判断イメージ\"");
            sql.AppendLine("     ,B.\"完了日程\"");
            sql.AppendLine("     ,B.\"供試品\"");
            sql.AppendLine("     ,B.\"出図日程\"");
            sql.AppendLine("     ,B.\"織込時期\"");
            sql.AppendLine("     ,B.\"編集日\"");
            sql.AppendLine("     ,B.\"編集者_ID\"");
            //Append Start 2021/04/19 杉浦 背景色を戻す処理の追加
            sql.AppendLine("     ,B.\"編集日\" AS BK_編集日");
            sql.AppendLine("     ,B.\"編集者_ID\" BK_編集者_ID");
            //Append End 2021/04/19 杉浦 背景色を戻す処理の追加
            sql.AppendLine("     ,B.\"作成日\"");
            sql.AppendLine("     ,B.\"FLAG_最新\"");
            sql.AppendLine("     ,(SELECT L.NAME FROM PERSONEL_LIST L WHERE B.編集者_ID = L.PERSONEL_ID) AS 編集者");
            //Append Start 2021/04/19 杉浦 背景色を戻す処理の追加
            sql.AppendLine("     ,(SELECT L.NAME FROM PERSONEL_LIST L WHERE B.編集者_ID = L.PERSONEL_ID) AS BK_編集者");
            //Append End 2021/04/19 杉浦 背景色を戻す処理の追加
            sql.AppendLine("     ,B.\"回答期限設定日\"");
            sql.AppendLine("     ,B.\"承認日\"");
            sql.AppendLine("     ,B.\"承認者_ID\"");
            sql.AppendLine("     ,(SELECT L.NAME FROM PERSONEL_LIST L WHERE B.承認者_ID = L.PERSONEL_ID) AS 承認者");
            sql.AppendLine("     ,B.\"項目_ID\"");
            sql.AppendLine("     ,B.\"親対応_ID\"");
            sql.AppendLine("     ,B.\"修正カラム\"");
            sql.AppendLine("     ,CAST(NVL(A.\"FLAG_CLOSE\", 0) AS NUMBER(1)) AS \"FLAG_CLOSE\"");
            sql.AppendLine("     ,A.\"NO\"");
            sql.AppendLine("     ,A.\"CAP種別\"");
            sql.AppendLine("     ,A.\"重要度\"");
            sql.AppendLine("     ,E.\"説明\"");
            sql.AppendLine("     ,A.\"項目\"");
            sql.AppendLine("     ,A.\"詳細\"");
            sql.AppendLine("     ,A.\"評価車両\"");
            sql.AppendLine("     ,A.\"仕向地\"");
            sql.AppendLine("     ,A.\"CAP確認結果\"");
            //Append Start 2021/05/18 杉浦 CAP確認結果に「写真・動画」列を追加
            sql.AppendLine("     ,H.\"TITLE\" \"N_DRIVE_LINK\"");
            //Append End 2021/05/18 杉浦 CAP確認結果に「写真・動画」列を追加
            sql.AppendLine("     ,A.\"フォロー状況\"");
            sql.AppendLine("     ,A.\"織込時期\" AS \"織込時期_項目\"");
            sql.AppendLine("     ,A.\"指摘分類\"");
            //Append Start 2021/06/15 矢作
            sql.AppendLine("     ,B.\"方向付け確定期限\"");
            //Append End 2021/06/15 矢作

            sql.AppendLine("     ,A.GENERAL_CODE");
            sql.AppendLine("     ,CASE WHEN ATH.GENERAL_CODE IS NOT NULL THEN 1 ELSE 0 END PERMIT_FLG");

            if (cond.ID != null)
            {
                sql.AppendLine("     ,(CAST(");
                sql.AppendLine("         (SELECT");
                sql.AppendLine("             COUNT(*)");
                sql.AppendLine("         FROM");
                sql.AppendLine("             試験計画_CAP_対応 P");
                sql.AppendLine("         WHERE");
                sql.AppendLine("             P.\"親対応_ID\" = B.\"親対応_ID\"");
                sql.AppendLine("         ) AS NUMBER(10)");
                sql.AppendLine("      )) AS \"過去履歴件数\"");
            }
            sql.AppendLine("FROM");
            sql.AppendLine("    \"試験計画_CAP_項目\" A");
            sql.AppendLine("    INNER JOIN \"試験計画_CAP_対応\" B ON A.\"ID\" = B.\"項目_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" C ON B.\"専門部署名\" = C.\"SECTION_CODE\"");
            sql.AppendLine("    LEFT JOIN \"DEPARTMENT_DATA\" D ON C.\"DEPARTMENT_ID\" = D.\"DEPARTMENT_ID\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_CAP_重要度\" E ON A.\"重要度\" = E.\"重要度\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_資料分類コード\" F ON B.\"分類\" = F.\"分類コード\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_資料評価レベル\" G ON B.\"評価レベル\" = G.\"評価レベル\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_他部署閲覧許可\" ATH");
            sql.AppendLine("        ON A.GENERAL_CODE = ATH.GENERAL_CODE");
            sql.AppendLine("        AND ATH.PERSONEL_ID = :PERSONEL_ID");
            sql.AppendLine("        AND ATH.PERMISSION_PERIOD_START <= TRUNC(SYSDATE)");
            sql.AppendLine("        AND ATH.PERMISSION_PERIOD_END >= TRUNC(SYSDATE)");
            //Append Start 2021/05/18 杉浦 CAP確認結果に「写真・動画」列を追加
            sql.AppendLine("    LEFT JOIN (SELECT CAP_ID, LISTAGG(TITLE, '\n') WITHIN GROUP(ORDER BY ID) TITLE FROM N_DRIVE_LINK GROUP BY CAP_ID) H");
            sql.AppendLine("        ON B.ID = H.CAP_ID");
            //Append End 2021/05/18 杉浦 CAP確認結果に「写真・動画」列を追加

            // 一覧用
            if (cond.項目_ID == null || cond.FLAG_最新 != null)
            {
                sql.AppendLine("    LEFT JOIN \"GENERAL_CODE\" GNR");
                sql.AppendLine("        ON A.GENERAL_CODE = GNR.GENERAL_CODE");
            }

            // 関連課フラグ（関連課題は専門部署名が指定されている場合のみ）
            if (cond.FLAG_関連課 == 1 && cond.専門部署名 != null)
            {
                sql.AppendLine("    LEFT JOIN (SELECT DISTINCT X.NO, X.GENERAL_CODE, Y.専門部署名 FROM 試験計画_CAP_項目 X, 試験計画_CAP_対応 Y WHERE X.ID = Y.項目_ID) RRT");
                sql.AppendLine("        ON A.NO = RRT.NO");
                sql.AppendLine("        AND A.GENERAL_CODE = RRT.GENERAL_CODE");
            }

            sql.AppendLine("WHERE 0 = 0");

            // 社員コード（パーソナルID）
            prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = cond.PERSONEL_ID, Direct = ParameterDirection.Input });

            if (cond.ID != null)
            {
                sql.AppendLine("    AND B.\"ID\" = :ID ");
                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Int64,
                    Object = cond.ID,
                    Direct = ParameterDirection.Input
                });
            }
            else if (cond.項目_ID != null && cond.FLAG_最新 == null)
            {
                //履歴用

                sql.AppendLine("    AND A.\"ID\" = :項目_ID ");
                prms.Add(new BindModel
                {
                    Name = ":項目_ID",
                    Type = OracleDbType.Int64,
                    Object = cond.項目_ID,
                    Direct = ParameterDirection.Input
                });
            }
            else
            {
                #region 一覧用

                //項目_ID（登録時のコピー用）
                if (cond.項目_ID != null)
                { 
                    sql.AppendLine("    AND A.\"ID\" = :項目_ID ");
                    prms.Add(new BindModel
                    {
                        Name = "項目_ID",
                        Type = OracleDbType.Int64,
                        Object = cond.項目_ID,
                        Direct = ParameterDirection.Input
                    });
                }

                //車種
                if (cond.GENERAL_CODE != null)
                {
                    sql.AppendLine("    AND(");

                    for (int i = 0; i < cond.GENERAL_CODE.Count(); i++)
                    {
                        if (i == 0)
                        {
                            sql.AppendLine("        A.\"GENERAL_CODE\" = :GENERAL_CODE" + i.ToString());
                        }
                        else
                        {
                            sql.AppendLine("        OR A.\"GENERAL_CODE\" = :GENERAL_CODE" + i.ToString());
                        }

                        prms.Add(new BindModel
                        {
                            Name = ":GENERAL_CODE" + i.ToString(),
                            Type = OracleDbType.Varchar2,
                            Object = cond.GENERAL_CODE[i],
                            Direct = ParameterDirection.Input
                        });
                    }

                    sql.AppendLine("    )");
                }

                //最新
                sql.AppendLine("    AND B.\"FLAG_最新\" = \'1\'");

                //回答期限
                if (cond.回答期限FROM != null)
                {
                    sql.AppendLine("    AND :回答期限FROM <= B.\"回答期限\"");
                    prms.Add(new BindModel
                    {
                        Name = ":回答期限FROM",
                        Type = OracleDbType.Date,
                        Object = cond.回答期限FROM,
                        Direct = ParameterDirection.Input
                    });
                }
                if (cond.回答期限TO != null)
                {
                    sql.AppendLine("    AND B.\"回答期限\" <= :回答期限TO");
                    prms.Add(new BindModel
                    {
                        Name = ":回答期限TO",
                        Type = OracleDbType.Date,
                        Object = cond.回答期限TO.Value.AddDays(1).AddSeconds(-1),
                        Direct = ParameterDirection.Input
                    });
                }

                //専門部署
                if (cond.専門部署名 != null)
                {
                    var sections = new List<string>();

                    // 参照テーブル（関連課の有無で変更）
                    var table = cond.FLAG_関連課 == 1 ? "RRT" : "B";

                    for (int i = 0; i < cond.専門部署名.Count(); i++)
                    {
                        sections.Add(string.Format("{0}.\"専門部署名\" = :専門部署名{1}", table, i.ToString()));

                        prms.Add(new BindModel
                        {
                            Name = ":専門部署名" + i.ToString(),
                            Type = OracleDbType.Varchar2,
                            Object = cond.専門部署名[i],
                            Direct = ParameterDirection.Input
                        });
                    }

                    sql.AppendLine(string.Format("    AND ({0})", string.Join(" OR ", sections)));
                }

                //重要度
                if (cond.重要度 != null)
                {
                    sql.AppendLine("    AND A.\"重要度\" = :重要度");
                    prms.Add(new BindModel
                    {
                        Name = ":重要度",
                        Type = OracleDbType.Varchar2,
                        Object = cond.重要度,
                        Direct = ParameterDirection.Input
                    });
                }

                //ステータス
                if (cond.FLAG_CLOSE == 1)
                {
                    sql.AppendLine("    AND A.\"FLAG_CLOSE\" = 1");
                }
                else if (cond.FLAG_CLOSE == 0)
                {
                    sql.AppendLine("    AND (NVL(A.\"FLAG_CLOSE\", 0) = 0)");
                }

                //部署承認
                if (cond.FLAG_上司承認 == 1)
                {
                    sql.AppendLine("    AND B.\"FLAG_上司承認\" = 1");
                }
                else if (cond.FLAG_上司承認 == 0)
                {
                    sql.AppendLine("    AND (NVL(B.\"FLAG_上司承認\", 0) = 0)");
                }

                //CAP確認
                if (cond.FLAG_CAP確認 == 1)
                {
                    sql.AppendLine("    AND B.\"FLAG_CAP確認\" = 1");
                }
                else if (cond.FLAG_CAP確認 == 0)
                {
                    sql.AppendLine("    AND (NVL(B.\"FLAG_CAP確認\", 0) = 0)");
                }

                //種別
                if (cond.CAP種別 != null)
                {
                    sql.AppendLine("    AND A.\"CAP種別\" = :CAP種別");
                    prms.Add(new BindModel
                    {
                        Name = ":CAP種別",
                        Type = OracleDbType.Varchar2,
                        Object = cond.CAP種別,
                        Direct = ParameterDirection.Input
                    });
                }

                //仕向地
                if (cond.仕向地 != null)
                {
                    sql.AppendLine("    AND A.\"仕向地\" = :仕向地");
                    prms.Add(new BindModel
                    {
                        Name = ":仕向地",
                        Type = OracleDbType.Varchar2,
                        Object = cond.仕向地,
                        Direct = ParameterDirection.Input
                    });
                }

                //完了日程
                if (cond.完了日程 == 1)
                {
                    sql.AppendLine("    AND B.\"完了日程\" = :完了日程");
                    prms.Add(new BindModel
                    {
                        Name = ":完了日程",
                        Type = OracleDbType.Date,
                        Object = ConstDate,
                        Direct = ParameterDirection.Input
                    });
                }
                else if (cond.完了日程 == 0)
                {
                    sql.AppendLine("    AND (B.\"完了日程\" IS NULL OR B.\"完了日程\" <> :完了日程)");
                    prms.Add(new BindModel
                    {
                        Name = ":完了日程",
                        Type = OracleDbType.Date,
                        Object = ConstDate,
                        Direct = ParameterDirection.Input
                    });
                }

                //出図日程
                if (cond.出図日程 == 1)
                {
                    sql.AppendLine("    AND B.\"出図日程\" = :出図日程");
                    prms.Add(new BindModel
                    {
                        Name = ":出図日程",
                        Type = OracleDbType.Date,
                        Object = ConstDate,
                        Direct = ParameterDirection.Input
                    });
                }
                else if (cond.出図日程 == 0)
                {
                    sql.AppendLine("    AND (B.\"出図日程\" IS NULL OR B.\"出図日程\" <> :出図日程)");
                    prms.Add(new BindModel
                    {
                        Name = ":出図日程",
                        Type = OracleDbType.Date,
                        Object = ConstDate,
                        Direct = ParameterDirection.Input
                    });
                }

                //対策予定
                if (cond.対策予定 != null)
                {
                    sql.AppendLine("    AND B.\"対策予定\" = :対策予定");
                    prms.Add(new BindModel
                    {
                        Name = ":対策予定",
                        Type = OracleDbType.Varchar2,
                        Object = cond.対策予定,
                        Direct = ParameterDirection.Input
                    });
                }

                //指摘分類
                if (cond.指摘分類 != null)
                {
                    sql.AppendLine("    AND A.\"指摘分類\" = :指摘分類");
                    prms.Add(new BindModel
                    {
                        Name = ":指摘分類",
                        Type = OracleDbType.Varchar2,
                        Object = cond.指摘分類,
                        Direct = ParameterDirection.Input
                    });
                }

                //供試品
                if (cond.供試品 != null)
                {
                    sql.AppendLine("    AND B.\"供試品\" = :供試品");
                    prms.Add(new BindModel
                    {
                        Name = ":供試品",
                        Type = OracleDbType.Varchar2,
                        Object = cond.供試品,
                        Direct = ParameterDirection.Input
                    });
                }

                //織込時期
                if (cond.織込時期 != null)
                {
                    sql.AppendLine("    AND B.\"織込時期\" = :織込時期");
                    prms.Add(new BindModel
                    {
                        Name = ":織込時期",
                        Type = OracleDbType.Varchar2,
                        Object = cond.織込時期,
                        Direct = ParameterDirection.Input
                    });
                }

                #endregion
            }

            //ソート順
            //項目_IDが指定されているかどうか
            if (cond.項目_ID != null && cond.FLAG_最新 == null)
            {
                //履歴用
                sql.AppendLine("ORDER BY");
                sql.AppendLine("    B.\"FLAG_最新\" DESC");
                sql.AppendLine("    ,B.\"作成日\" DESC");
                sql.AppendLine("    ,B.\"ID\"");
            }
            else
            {
                //一覧用
                sql.AppendLine("ORDER BY");
                sql.AppendLine("    GNR.UNDER_DEVELOPMENT DESC NULLS LAST");
                sql.AppendLine("    ,GNR.SORT_NUMBER NULLS LAST");
                sql.AppendLine("    ,GNR.CAR_GROUP NULLS LAST");
                sql.AppendLine("    ,A.\"GENERAL_CODE\" NULLS LAST");
                sql.AppendLine("    ,A.\"NO\"");
                sql.AppendLine("    ,B.\"ID\"");
            }

            //取得
            return db.ReadModelList<CapModel>(sql.ToString(), prms);
        }
        #endregion

        #region CAP課題登録
        /// <summary>
        /// CAP課題登録
        /// </summary>
        /// <param name="list">CAP課題項目</param>
        /// <returns>登録可否</returns>
        public bool Post(List<CapModel> list)
        {
            if (list.Any() == false)
            {
                return true;
            }

            //トランザクション開始
            db.Begin();

            var flg = new Factory(db).GetPost(list).RunSql(list);

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

        #region CAP課題更新
        /// <summary>
        /// CAP課題更新
        /// </summary>
        /// <param name="list">CAP課題項目</param>
        /// <returns>更新可否</returns>
        public bool Put(List<CapModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //更新対象があるかどうか
            if (list != null && list.Any() == true)
            {
                var insertList = list.Where((x) => x.ID < 0).ToList();
                var updateList = list.Where((x) => x.ID > 0).ToList();

                if (insertList.Any())
                {
                    // CAP課題対応登録（行追加分）
                    results.Add(new Factory(db).GetPost(insertList).RunSql(insertList));
                }

                // CAP課題項目更新
                results.Add(UpdateItem(updateList));

                // CAP課題対応更新
                results.Add(UpdateDealItem(updateList));
            }

            //更新が成功したかどうか
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
        /// CAP課題項目更新
        /// </summary>
        /// <param name="list">CAP課題項目</param>
        /// <returns>更新可否</returns>
        private bool UpdateItem(List<CapModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"試験計画_CAP_項目\"");
            sql.AppendLine("SET");
            sql.AppendLine("    \"FLAG_CLOSE\" = :FLAG_CLOSE");
            sql.AppendLine("    ,\"NO\" = :NO");
            sql.AppendLine("    ,\"CAP種別\" = :CAP種別");
            sql.AppendLine("    ,\"重要度\" = :重要度");
            sql.AppendLine("    ,\"項目\" = :項目");
            sql.AppendLine("    ,\"詳細\" = :詳細");
            sql.AppendLine("    ,\"評価車両\" = :評価車両");
            sql.AppendLine("    ,\"仕向地\" = :仕向地");
            sql.AppendLine("    ,\"CAP確認結果\" = :CAP確認結果");
            sql.AppendLine("    ,\"フォロー状況\" = :フォロー状況");
            sql.AppendLine("    ,\"織込時期\" = :織込時期");
            sql.AppendLine("    ,\"指摘分類\" = :指摘分類");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :項目_ID");
            var results = new List<bool>();

            foreach (var item in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":FLAG_CLOSE",
                         Type = OracleDbType.Int16,
                         Object = item.FLAG_CLOSE,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":NO",
                         Type = OracleDbType.Int64,
                         Object = item.NO,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":CAP種別",
                         Type = OracleDbType.Varchar2,
                         Object = item.CAP種別,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":重要度",
                         Type = OracleDbType.Varchar2,
                         Object = item.重要度,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":項目",
                         Type = OracleDbType.Varchar2,
                         Object = item.項目,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":詳細",
                         Type = OracleDbType.Varchar2,
                         Object = item.詳細,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":評価車両",
                         Type = OracleDbType.Varchar2,
                         Object = item.評価車両,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":仕向地",
                         Type = OracleDbType.Varchar2,
                         Object = item.仕向地,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":CAP確認結果",
                         Type = OracleDbType.Varchar2,
                         Object = item.CAP確認結果,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":フォロー状況",
                         Type = OracleDbType.Varchar2,
                         Object = item.フォロー状況,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":織込時期",
                         Type = OracleDbType.Varchar2,
                         Object = item.織込時期_項目,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":指摘分類",
                         Type = OracleDbType.Varchar2,
                         Object = item.指摘分類,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":項目_ID",
                         Type = OracleDbType.Long,
                         Object = item.項目_ID,
                         Direct = ParameterDirection.Input
                     }
                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));
            }

            return results.All(x => x == true);
        }

        /// <summary>
        /// CAP課題対応更新
        /// </summary>
        /// <param name="list">CAP課題項目</param>
        /// <returns>更新可否</returns>
        private bool UpdateDealItem(List<CapModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"試験計画_CAP_対応\"");
            sql.AppendLine("SET");
            sql.AppendLine("    \"FLAG_CAP確認\" = :FLAG_CAP確認");
            sql.AppendLine("    ,\"回答期限\" = :回答期限");
            sql.AppendLine("    ,\"FLAG_上司承認\" = :FLAG_上司承認");
            sql.AppendLine("    ,\"専門部署名\" = CASE WHEN 編集日 IS NULL OR 編集日 < :編集日 THEN :専門部署名 ELSE 専門部署名 END");
            sql.AppendLine("    ,\"対策予定\" = CASE WHEN 編集日 IS NULL OR 編集日 < :編集日 THEN :対策予定 ELSE 対策予定 END");
            sql.AppendLine("    ,\"対応策\" = CASE WHEN 編集日 IS NULL OR 編集日 < :編集日 THEN :対応策 ELSE 対応策 END");
            sql.AppendLine("    ,\"事前把握有無\" = CASE WHEN 編集日 IS NULL OR 編集日 < :編集日 THEN :事前把握有無 ELSE 事前把握有無 END");
            sql.AppendLine("    ,\"分類\" = CASE WHEN 編集日 IS NULL OR 編集日 < :編集日 THEN :分類 ELSE 分類 END");
            sql.AppendLine("    ,\"評価レベル\" = CASE WHEN 編集日 IS NULL OR 編集日 < :編集日 THEN :評価レベル ELSE 評価レベル END");
            sql.AppendLine("    ,\"完了日程\" = CASE WHEN 編集日 IS NULL OR 編集日 < :編集日 THEN :完了日程 ELSE 完了日程 END");
            sql.AppendLine("    ,\"供試品\" = CASE WHEN 編集日 IS NULL OR 編集日 < :編集日 THEN :供試品 ELSE 供試品 END");
            sql.AppendLine("    ,\"出図日程\" = CASE WHEN 編集日 IS NULL OR 編集日 < :編集日 THEN :出図日程 ELSE 出図日程 END");
            sql.AppendLine("    ,\"織込時期\" = CASE WHEN 編集日 IS NULL OR 編集日 < :編集日 THEN :織込時期 ELSE 織込時期 END");

            //Append Start 2021/06/15 矢作
            sql.AppendLine("    ,\"方向付け確定期限\" = :方向付け確定期限");
            //Append End 2021/06/15 矢作

            sql.AppendLine("    ,\"回答期限設定日\" = :回答期限設定日");
            sql.AppendLine("    ,\"編集日\" = :編集日");
            sql.AppendLine("    ,\"編集者_ID\" = :編集者_ID");
            sql.AppendLine("    ,\"承認日\" = :承認日");
            sql.AppendLine("    ,\"承認者_ID\" = :承認者_ID");
            sql.AppendLine("    ,\"修正カラム\" = :修正カラム");
            sql.AppendLine("    ,\"対策案編集者_ID\" = CASE WHEN 対策案編集日 IS NULL OR 対策案編集日 < :対策案編集日 THEN :対策案編集者_ID ELSE 対策案編集者_ID END");
            sql.AppendLine("    ,\"対策案編集日\" = CASE WHEN 対策案編集日 IS NULL OR 対策案編集日 < :対策案編集日 THEN :対策案編集日 ELSE 対策案編集日 END");

            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"項目_ID\" = :項目_ID");
            sql.AppendLine("    AND \"ID\" = :対応_ID");
            sql.AppendLine("    AND \"FLAG_最新\" = \'1\'");

            var results = new List<bool>();

            foreach (var item in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":FLAG_CAP確認",
                         Type = OracleDbType.Int16,
                         Object = item.FLAG_CAP確認,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":回答期限",
                         Type = OracleDbType.Date,
                         Object = item.回答期限,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":FLAG_上司承認",
                         Type = OracleDbType.Int16,
                         Object = item.FLAG_上司承認,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":専門部署名",
                         Type = OracleDbType.Varchar2,
                         Object = item.専門部署名,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":対策予定",
                         Type = OracleDbType.Varchar2,
                         Object = item.対策予定,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":対応策",
                         Type = OracleDbType.Varchar2,
                         Object = item.対応策,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":事前把握有無",
                         Type = OracleDbType.Varchar2,
                         Object = item.事前把握有無,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":分類",
                         Type = OracleDbType.Int16,
                         Object = item.分類,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":評価レベル",
                         Type = OracleDbType.Varchar2,
                         Object = item.評価レベル,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":完了日程",
                         Type = OracleDbType.Date,
                         Object = item.完了日程,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":供試品",
                         Type = OracleDbType.Varchar2,
                         Object = item.供試品,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":出図日程",
                         Type = OracleDbType.Date,
                         Object = item.出図日程,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":織込時期",
                         Type = OracleDbType.Varchar2,
                         Object = item.織込時期,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":回答期限設定日",
                         Type = OracleDbType.Date,
                         Object = item.回答期限設定日,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":項目_ID",
                         Type = OracleDbType.Long,
                         Object = item.項目_ID,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":対応_ID",
                         Type = OracleDbType.Long,
                         Object = item.ID,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":編集者_ID",
                         Type = OracleDbType.Varchar2,
                         Object = item.編集者_ID,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":承認者_ID",
                         Type = OracleDbType.Varchar2,
                         Object = item.承認者_ID,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":承認日",
                         Type = OracleDbType.Date,
                         Object = item.承認日,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":編集日",
                         Type = OracleDbType.Date,
                         Object = item.編集日,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":修正カラム",
                         Type = OracleDbType.Varchar2,
                         Object = item.修正カラム,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":対策案編集者_ID",
                         Type = OracleDbType.Varchar2,
                         Object = item.対策案編集者_ID,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":対策案編集日",
                         Type = OracleDbType.Date,
                         Object = item.対策案編集日,
                         Direct = ParameterDirection.Input
                     },
                     
                     //Append Start 2021/06/15 矢作
                     new BindModel
                     {
                         Name = ":方向付け確定期限",
                         Type = OracleDbType.Date,
                         Object = item.方向付け確定期限,
                         Direct = ParameterDirection.Input
                     },
                     //Append End 2021/06/15 矢作
                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));
            }

            return results.All(x => x == true);
        }
        #endregion

        #region CAP課題削除
        /// <summary>
        /// CAP課題削除
        /// </summary>
        /// <param name="list">CAP課題項目</param>
        /// <returns>削除可否</returns>
        public bool Delete(List<CapModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //削除対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //CAP課題対応削除
                results.Add(DeleteDealItem(list));
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
        /// CAP課題対応削除
        /// </summary>
        /// <param name="list">CAP課題項目</param>
        /// <returns>削除可否</returns>
        private bool DeleteDealItem(List<CapModel> list)
        {
            // 削除SQL
            var sql = "DELETE FROM 試験計画_CAP_対応 WHERE ID = :ID";

            var results = new List<bool>();

            foreach (var item in list)
            {
                // パラメータ
                var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":ID",
                         Type = OracleDbType.Int64,
                         Object = item.ID,
                         Direct = ParameterDirection.Input
                     }
                };

                // 削除
                results.Add(db.DeleteData(sql, prms));
            }

            return results.All(x => x == true);
        }
        #endregion

        #region クラス生成クラス
        /// <summary>
        /// クラス生成クラス
        /// </summary>
        private class Factory
        {
            private DBAccess _db;

            /// <summary>
            /// クラス生成クラスのコンストラクタ
            /// </summary>
            /// <param name="db"></param>
            public Factory(DBAccess db)
            {
                _db = db;
            }

            /// <summary>
            /// 登録クラスを生成
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            public BasePost GetPost(List<CapModel> list)
            {
                if (list[0].親対応_ID == null)
                {
                    if (list[0].編集者_ID == null)
                    {
                        // インポートの場合
                        return new Import(_db);
                    }

                    // 行追加の場合
                    return new RowAdd(_db);
                }

                // 履歴作成の場合
                return new HisAdd(_db);
            }
        }
        #endregion

        #region 登録クラス
        /// <summary>
        /// 登録クラス
        /// </summary>
        private abstract class BasePost
        {
            protected DBAccess _db;

            public BasePost(DBAccess db)
            {
                _db = db;
            }

            #region 公開メソッド

            /// <summary>
            /// 登録SQLを実行します。
            /// </summary>
            /// <param name="list">CAP課題項目</param>
            /// <returns>登録可否</returns>
            public bool RunSql(List<CapModel> list)
            {
                return InsertItem(list) && InsertDeal(list);
            }

            #endregion

            #region 試験計画_CAP_項目に登録
            /// <summary>
            /// 試験計画_CAP_項目に登録
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            protected virtual bool InsertItem(List<CapModel> list)
            {
                // 基本は登録しない。
                return true;
            }
            #endregion

            #region 試験計画_CAP_対応テーブルに登録
            /// <summary>
            /// 試験計画_CAP_対応テーブルに登録
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            private bool InsertDeal(List<CapModel> list)
            {
                var sql = new StringBuilder();

                #region SQL
                sql.AppendLine("INSERT INTO");
                sql.AppendLine("\"試験計画_CAP_対応\"(");
                sql.AppendLine("    \"ID\"");
                sql.AppendLine("    ,\"項目_ID\"");
                sql.AppendLine("    ,\"FLAG_CAP確認\"");
                sql.AppendLine("    ,\"回答期限\"");
                sql.AppendLine("    ,\"FLAG_上司承認\"");
                sql.AppendLine("    ,\"専門部署名\"");
                sql.AppendLine("    ,\"対策予定\"");
                sql.AppendLine("    ,\"対応策\"");
                sql.AppendLine("    ,\"事前把握有無\"");
                sql.AppendLine("    ,\"分類\"");
                sql.AppendLine("    ,\"評価レベル\"");
                sql.AppendLine("    ,\"完了日程\"");
                sql.AppendLine("    ,\"供試品\"");
                sql.AppendLine("    ,\"出図日程\"");
                sql.AppendLine("    ,\"織込時期\"");
                sql.AppendLine("    ,\"回答期限設定日\"");
                sql.AppendLine("    ,\"承認日\"");
                sql.AppendLine("    ,\"承認者_ID\"");
                sql.AppendLine("    ,\"修正カラム\"");
                sql.AppendLine("    ,\"編集日\"");
                sql.AppendLine("    ,\"編集者_ID\"");
                sql.AppendLine("    ,\"作成日\"");
                sql.AppendLine("    ,\"FLAG_最新\"");
                sql.AppendLine("    ,\"親対応_ID\"");
                sql.AppendLine("    ,\"対策案編集者_ID\"");
                sql.AppendLine("    ,\"対策案編集日\"");
                //Append Start 2021/06/15 矢作
                sql.AppendLine("    ,\"方向付け確定期限\"");
                //Append End 2021/06/15 矢作

                sql.AppendLine(") VALUES (");
                sql.AppendLine("    (SELECT MAX(ID) + 1 FROM 試験計画_CAP_対応)");
                sql.AppendLine("    ,:項目_ID");
                sql.AppendLine("    ,:FLAG_CAP確認");
                sql.AppendLine("    ,:回答期限");
                sql.AppendLine("    ,:FLAG_上司承認");
                sql.AppendLine("    ,:専門部署名");
                sql.AppendLine("    ,:対策予定");
                sql.AppendLine("    ,:対応策");
                sql.AppendLine("    ,:事前把握有無");
                sql.AppendLine("    ,:分類");
                sql.AppendLine("    ,:評価レベル");
                sql.AppendLine("    ,:完了日程");
                sql.AppendLine("    ,:供試品");
                sql.AppendLine("    ,:出図日程");
                sql.AppendLine("    ,:織込時期");
                sql.AppendLine("    ,:回答期限設定日");
                sql.AppendLine("    ,:承認日");
                sql.AppendLine("    ,:承認者_ID");
                sql.AppendLine("    ,:修正カラム");
                sql.AppendLine("    ,:編集日");
                sql.AppendLine("    ,:編集者_ID");
                sql.AppendLine("    ,SYSDATE");
                sql.AppendLine("    ,:FLAG_最新");
                sql.AppendLine(SetSqlParentDealId());
                sql.AppendLine("    ,:対策案編集者_ID");
                sql.AppendLine("    ,:対策案編集日");
                //Append Start 2021/06/15 矢作
                sql.AppendLine("    ,:方向付け確定期限");
                //Append End 2021/06/15 矢作

                sql.AppendLine(")");
                #endregion

                var results = new List<bool>();

                foreach (var item in list)
                {
                    #region パラメータセット
                    var prms = new List<BindModel>
                    {
                         new BindModel
                         {
                             Name = ":FLAG_CAP確認",
                             Type = OracleDbType.Int16,
                             Object = item.FLAG_CAP確認,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":回答期限",
                             Type = OracleDbType.Date,
                             Object = item.回答期限,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":FLAG_上司承認",
                             Type = OracleDbType.Int16,
                             Object = item.FLAG_上司承認,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":専門部署名",
                             Type = OracleDbType.Varchar2,
                             Object = item.専門部署名,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":対策予定",
                             Type = OracleDbType.Varchar2,
                             Object = item.対策予定,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":対応策",
                             Type = OracleDbType.Varchar2,
                             Object = item.対応策,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":事前把握有無",
                             Type = OracleDbType.Varchar2,
                             Object = item.事前把握有無,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":分類",
                             Type = OracleDbType.Int16,
                             Object = item.分類,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":評価レベル",
                             Type = OracleDbType.Varchar2,
                             Object = item.評価レベル,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":完了日程",
                             Type = OracleDbType.Date,
                             Object = item.完了日程,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":供試品",
                             Type = OracleDbType.Varchar2,
                             Object = item.供試品,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":出図日程",
                             Type = OracleDbType.Date,
                             Object = item.出図日程,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":織込時期",
                             Type = OracleDbType.Varchar2,
                             Object = item.織込時期,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":回答期限設定日",
                             Type = OracleDbType.Date,
                             Object = item.回答期限設定日,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":編集者_ID",
                             Type = OracleDbType.Varchar2,
                             Object = item.編集者_ID,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":承認者_ID",
                             Type = OracleDbType.Varchar2,
                             Object = item.承認者_ID,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":承認日",
                             Type = OracleDbType.Date,
                             Object = item.承認日,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":項目_ID",
                             Type = OracleDbType.Long,
                             Object = item.項目_ID,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":FLAG_最新",
                             Type = OracleDbType.Int16,
                             Object = item.FLAG_最新,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":親対応_ID",
                             Type = OracleDbType.Long,
                             Object = item.親対応_ID,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":修正カラム",
                             Type = OracleDbType.Varchar2,
                             Object = item.修正カラム,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":編集日",
                             Type = OracleDbType.Date,
                             Object = item.編集日,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":対策案編集者_ID",
                             Type = OracleDbType.Varchar2,
                             Object = item.対策案編集者_ID,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":対策案編集日",
                             Type = OracleDbType.Date,
                             Object = item.対策案編集日,
                             Direct = ParameterDirection.Input
                         },

                         //Append Start 2021/06/15 矢作
                         new BindModel
                         {
                             Name = ":方向付け確定期限",
                             Type = OracleDbType.Date,
                             Object = item.方向付け確定期限,
                             Direct = ParameterDirection.Input
                         },
                         //Append End 2021/06/15 矢作
                    };
                    #endregion

                    // SQL実行
                    results.Add(_db.InsertData(sql.ToString(), prms));
                }

                return results.All(x => x == true);
            }
            #endregion

            #region 試験計画_CAP_対応テーブルの親対応_IDのSQLを生成
            /// <summary>
            /// 試験計画_CAP_対応テーブルの親対応_IDのSQLを生成
            /// </summary>
            /// <returns></returns>
            protected virtual string SetSqlParentDealId()
            {
                return "    ,(SELECT MAX(ID) + 1 FROM 試験計画_CAP_対応)";
            }
            #endregion

            #region NOと開発符号から項目IDを取得してセットします。
            /// <summary>
            /// NOと開発符号から項目IDを取得してセットします。
            /// </summary>
            /// <param name="list"></param>
            protected void SetItemId(List<CapModel> list)
            {
                var sql = "SELECT ID FROM 試験計画_CAP_項目 WHERE NO = :NO AND GENERAL_CODE = :GENERAL_CODE";

                foreach (var item in list)
                {
                    var prms = new List<BindModel>
                     {
                         new BindModel
                         {
                             Name = ":NO",
                             Type = OracleDbType.Int64,
                             Object = item.NO,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":GENERAL_CODE",
                             Type = OracleDbType.Varchar2,
                             Object = item.GENERAL_CODE,
                             Direct = ParameterDirection.Input
                         },
                    };

                    var reader = _db.Reader(sql, prms);
                    reader.Read();
                    item.項目_ID = reader.GetInt64(0);
                }
            }
            #endregion
        }
        #endregion

        #region 履歴クラス
        /// <summary>
        /// 履歴クラス
        /// </summary>
        private class HisAdd : BasePost
        {
            public HisAdd(DBAccess db) : base(db)
            {
            }

            protected override string SetSqlParentDealId()
            {
                return "    ,:親対応_ID";
            }
        }
        #endregion

        #region 行追加クラス
        /// <summary>
        /// 行追加クラス
        /// </summary>
        private class RowAdd : BasePost
        {
            public RowAdd(DBAccess db) : base(db)
            {
            }

            #region 試験計画_CAP_項目に登録
            /// <summary>
            /// 試験計画_CAP_項目に登録
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            protected override bool InsertItem(List<CapModel> list)
            {
                var itemList = list
                    .Select((x) =>
                        new
                        {
                            項目_ID = x.項目_ID,
                            NO = x.NO,
                            GENERAL_CODE = x.GENERAL_CODE,
                            項目 = x.項目,
                            詳細 = x.詳細,
                            重要度 = x.重要度,
                            評価車両 = x.評価車両,
                            CAP確認結果 = x.CAP確認結果,
                            フォロー状況 = x.フォロー状況,
                            織込時期_項目 = x.織込時期_項目,
                            指摘分類 = x.指摘分類,
                            CAP種別 = x.CAP種別,
                            FLAG_CLOSE = x.FLAG_CLOSE,
                            仕向地 = x.仕向地,
                        })

                    // 重複項目は削除
                    .Distinct()

                    .Select((x) =>
                        new CapModel()
                        {
                            項目_ID = x.項目_ID,
                            NO = x.NO,
                            GENERAL_CODE = x.GENERAL_CODE,
                            項目 = x.項目,
                            詳細 = x.詳細,
                            重要度 = x.重要度,
                            評価車両 = x.評価車両,
                            CAP確認結果 = x.CAP確認結果,
                            フォロー状況 = x.フォロー状況,
                            織込時期_項目 = x.織込時期_項目,
                            指摘分類 = x.指摘分類,
                            CAP種別 = x.CAP種別,
                            FLAG_CLOSE = x.FLAG_CLOSE,
                            仕向地 = x.仕向地,
                        })

                    // 既に登録されているものは除く
                    .Where((x) => IsExistItem(x) == false)

                    .ToList();

                // 全て登録されていれば終了
                if (itemList.Any() == false)
                {
                    return true;
                }

                var sql = new StringBuilder();

                sql.AppendLine("INSERT INTO 試験計画_CAP_項目(");
                sql.AppendLine("    ID");
                sql.AppendLine("    , NO");
                sql.AppendLine("    , GENERAL_CODE");
                sql.AppendLine("    , 項目");
                sql.AppendLine("    , 詳細");
                sql.AppendLine("    , 重要度");
                sql.AppendLine("    , 評価車両");
                sql.AppendLine("    , CAP確認結果");
                sql.AppendLine("    , フォロー状況");
                sql.AppendLine("    , 織込時期");
                sql.AppendLine("    , 指摘分類");
                sql.AppendLine("    , CAP種別");
                sql.AppendLine("    , FLAG_CLOSE");
                sql.AppendLine("    , 親項目_ID");
                sql.AppendLine("    , 仕向地");
                sql.AppendLine(")");
                sql.AppendLine("VALUES(");
                sql.AppendLine("    (SELECT MAX(ID) + 1 FROM 試験計画_CAP_項目)");
                sql.AppendLine("    , :NO");
                sql.AppendLine("    , :GENERAL_CODE");
                sql.AppendLine("    , :項目");
                sql.AppendLine("    , :詳細");
                sql.AppendLine("    , :重要度");
                sql.AppendLine("    , :評価車両");
                sql.AppendLine("    , :CAP確認結果");
                sql.AppendLine("    , :フォロー状況");
                sql.AppendLine("    , :織込時期");
                sql.AppendLine("    , :指摘分類");
                sql.AppendLine("    , :CAP種別");
                sql.AppendLine("    , :FLAG_CLOSE");
                sql.AppendLine("    , NULL");
                sql.AppendLine("    , :仕向地");
                sql.AppendLine(") ");

                var results = new List<bool>();

                foreach (var item in itemList)
                {
                    //パラメータ
                    var prms = new List<BindModel>
                     {
                         new BindModel
                         {
                             Name = ":NO",
                             Type = OracleDbType.Int64,
                             Object = item.NO,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":GENERAL_CODE",
                             Type = OracleDbType.Varchar2,
                             Object = item.GENERAL_CODE,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":項目",
                             Type = OracleDbType.Varchar2,
                             Object = item.項目,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":詳細",
                             Type = OracleDbType.Varchar2,
                             Object = item.詳細,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":重要度",
                             Type = OracleDbType.Varchar2,
                             Object = item.重要度,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":評価車両",
                             Type = OracleDbType.Varchar2,
                             Object = item.評価車両,
                             Direct = ParameterDirection.Input
                         },
                         
                         new BindModel
                         {
                             Name = ":CAP確認結果",
                             Type = OracleDbType.Varchar2,
                             Object = item.CAP確認結果,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":フォロー状況",
                             Type = OracleDbType.Varchar2,
                             Object = item.フォロー状況,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":織込時期",
                             Type = OracleDbType.Varchar2,
                             Object = item.織込時期_項目,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":指摘分類",
                             Type = OracleDbType.Varchar2,
                             Object = item.指摘分類,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":CAP種別",
                             Type = OracleDbType.Varchar2,
                             Object = item.CAP種別,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":FLAG_CLOSE",
                             Type = OracleDbType.Int16,
                             Object = item.FLAG_CLOSE,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":仕向地",
                             Type = OracleDbType.Varchar2,
                             Object = item.仕向地,
                             Direct = ParameterDirection.Input
                         },
                    };

                    results.Add(_db.InsertData(sql.ToString(), prms));
                }

                // 項目ID更新
                SetItemId(list);

                return results.All(x => x == true);
            }
            #endregion

            #region DBに存在する項目レコードか？
            /// <summary>
            /// DBに存在する項目レコードか？
            /// </summary>
            /// <param name="model"></param>
            /// <returns></returns>
            private bool IsExistItem(CapModel model)
            {
                var sql = "SELECT ID FROM 試験計画_CAP_項目 WHERE ID = :ID";

                var prms = new List<BindModel>
                     {
                         new BindModel
                         {
                             Name = ":ID",
                             Type = OracleDbType.Int64,
                             Object = model.項目_ID,
                             Direct = ParameterDirection.Input
                         },
                    };

                return _db.Reader(sql, prms).HasRows;
            }
            #endregion
        }
        #endregion

        #region インポートクラス
        /// <summary>
        /// インポートクラス
        /// </summary>
        private class Import : BasePost
        {
            public Import(DBAccess db) : base(db)
            {
            }

            #region 試験計画_CAP_項目に登録
            /// <summary>
            /// 試験計画_CAP_項目に登録
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            protected override bool InsertItem(List<CapModel> list)
            {
                // 重複項目は削除
                var itemList = list
                    .Select((x) =>
                        new
                        {
                            NO = x.NO,
                            GENERAL_CODE = x.GENERAL_CODE,
                            項目 = x.項目,
                            詳細 = x.詳細,
                            重要度 = x.重要度,
                            評価車両 = x.評価車両,
                            仕向地 = x.仕向地,
                            CAP種別 = x.CAP種別
                        })
                    .Distinct()
                    .Select((x) =>
                        new CapModel()
                        {
                            NO = x.NO,
                            GENERAL_CODE = x.GENERAL_CODE,
                            項目 = x.項目,
                            詳細 = x.詳細,
                            重要度 = x.重要度,
                            評価車両 = x.評価車両,
                            仕向地 = x.仕向地,
                            CAP種別 = x.CAP種別
                        })
                    .ToList();

                var sql = new StringBuilder();

                sql.AppendLine("INSERT INTO 試験計画_CAP_項目(");
                sql.AppendLine("    ID");
                sql.AppendLine("    , NO");
                sql.AppendLine("    , GENERAL_CODE");
                sql.AppendLine("    , 項目");
                sql.AppendLine("    , 詳細");
                sql.AppendLine("    , 重要度");
                sql.AppendLine("    , 評価車両");
                sql.AppendLine("    , CAP確認結果");
                sql.AppendLine("    , フォロー状況");
                sql.AppendLine("    , 織込時期");
                sql.AppendLine("    , 指摘分類");
                sql.AppendLine("    , CAP種別");
                sql.AppendLine("    , FLAG_CLOSE");
                sql.AppendLine("    , 親項目_ID");
                sql.AppendLine("    , 仕向地");
                sql.AppendLine(")");
                sql.AppendLine("VALUES(");
                sql.AppendLine("    (SELECT MAX(ID) + 1 FROM 試験計画_CAP_項目)");
                sql.AppendLine("    , :NO");
                sql.AppendLine("    , :GENERAL_CODE");
                sql.AppendLine("    , :項目");
                sql.AppendLine("    , :詳細");
                sql.AppendLine("    , :重要度");
                sql.AppendLine("    , :評価車両");
                sql.AppendLine("    , NULL");
                sql.AppendLine("    , NULL");
                sql.AppendLine("    , NULL");
                sql.AppendLine("    , NULL");
                sql.AppendLine("    , :CAP種別");
                sql.AppendLine("    , NULL");
                sql.AppendLine("    , NULL");
                sql.AppendLine("    , :仕向地");
                sql.AppendLine(") ");

                var results = new List<bool>();

                foreach (var item in itemList)
                {
                    //パラメータ
                    var prms = new List<BindModel>
                     {
                         new BindModel
                         {
                             Name = ":NO",
                             Type = OracleDbType.Int64,
                             Object = item.NO,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":GENERAL_CODE",
                             Type = OracleDbType.Varchar2,
                             Object = item.GENERAL_CODE,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":CAP種別",
                             Type = OracleDbType.Varchar2,
                             Object = item.CAP種別,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":重要度",
                             Type = OracleDbType.Varchar2,
                             Object = item.重要度,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":項目",
                             Type = OracleDbType.Varchar2,
                             Object = item.項目,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":詳細",
                             Type = OracleDbType.Varchar2,
                             Object = item.詳細,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":評価車両",
                             Type = OracleDbType.Varchar2,
                             Object = item.評価車両,
                             Direct = ParameterDirection.Input
                         },
                         new BindModel
                         {
                             Name = ":仕向地",
                             Type = OracleDbType.Varchar2,
                             Object = item.仕向地,
                             Direct = ParameterDirection.Input
                         },
                    };

                    results.Add(_db.InsertData(sql.ToString(), prms));
                }

                // 項目ID取得
                SetItemId(list);

                return results.All(x => x == true);
            }
            #endregion
        }
        #endregion
    }
}