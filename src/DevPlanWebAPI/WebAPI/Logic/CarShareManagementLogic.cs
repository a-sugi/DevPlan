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
    /// カーシェア管理一覧業務ロジッククラス
    /// </summary>
    public class CarShareManagementLogic : BaseLogic
    {
        #region メンバ変数
        #endregion

        #region カーシェア管理一覧項目取得
        /// <summary>
        /// カーシェア管理一覧項目取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<CarShareManagementItemModel> GetCarShareManagementItem(CarShareManagementItemSearchModel cond)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     F.\"駐車場番号\"");
            sql.AppendLine("     ,A.\"GENERAL_CODE\" AS \"車系\"");
            sql.AppendLine("     ,A.\"START_DATE\"");
            sql.AppendLine("     ,A.\"END_DATE\"");
            sql.AppendLine("     ,A.\"予約種別\"");
            sql.AppendLine("     ,G.\"開発符号\" AS \"GENERAL_CODE\"");
            sql.AppendLine("     ,G.\"登録ナンバー\"");
            sql.AppendLine("     ,A.\"DESCRIPTION\"");
            sql.AppendLine("     ,I.\"SECTION_CODE\"");
            sql.AppendLine("     ,K.\"NAME\"");
            //Delete Start 2021/10/12 杉浦 カーシェア一覧追加要望
            ////Append Start 2021/06/14 杉浦
            //sql.AppendLine("     ,J.\"SECTION_CODE\" PREV_SECTION_CODE");
            //sql.AppendLine("     ,J.\"NAME\" PREV_NAME");
            ////Append End 2021/06/14 杉浦
            //Delete End 2021/10/12 杉浦 カーシェア一覧追加要望
            sql.AppendLine("     ,C.\"FLAG_準備済\"");
            sql.AppendLine("     ,B.\"FLAG_実使用\"");
            sql.AppendLine("     ,C.\"FLAG_返却済\"");
            sql.AppendLine("     ,C.\"FLAG_給油済\"");
            sql.AppendLine("     ,C.\"貸出備考\"");
            sql.AppendLine("     ,C.\"返却備考\"");
            //Update Start 2021/10/12 杉浦 カーシェア一覧追加要望
            ////Update Start 2021/06/14 杉浦
            sql.AppendLine("     ,B.\"目的\"");
            sql.AppendLine("     ,B.\"行先\"");
            //sql.AppendLine("     ,CASE WHEN J.\"NAME\" IS NULL THEN B.\"目的\" ELSE B.\"目的\" || '\n(' || J.\"目的\" || ')' END 目的");
            //sql.AppendLine("     ,CASE WHEN J.\"NAME\" IS NULL THEN B.\"行先\" ELSE B.\"行先\" || '\n(' || J.\"行先\" || ')' END 行先");
            ////Update End 2021/06/14 杉浦
            //Update End 2021/10/12 杉浦 カーシェア一覧追加要望
            sql.AppendLine("     ,B.\"TEL\"");
            //Delete Start 2021/10/12 杉浦 カーシェア一覧追加要望
            ////Append Start 2021/06/14 杉浦
            //sql.AppendLine("     ,J.\"TEL\"PREV_TEL");
            ////Update End 2021/06/14 杉浦
            //Delete End 2021/10/12 杉浦 カーシェア一覧追加要望
            //Append Start 2021/10/12 杉浦 カーシェア一覧追加要望
            sql.AppendLine("     ,TO_CHAR(J.\"END_DATE\", 'DD') || '・' || TO_CHAR(J.\"END_DATE\", 'HH24MI') || ' ' || J.\"SECTION_CODE\" || ' ' || J.\"NAME\" PREV_RESERVE");
            //Delete End 2021/10/12 杉浦 カーシェア一覧追加要望
            //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
            sql.AppendLine("     ,D.\"REPLACEMENT_TEXT\" \"入れ替え中車両\"");
            //Append End 2022/01/17 杉浦 入れ替え中車両の処理
            sql.AppendLine("     ,G.\"FLAG_ETC付\"");
            sql.AppendLine("     ,G.\"FLAG_ナビ付\"");
            sql.AppendLine("     ,G.\"仕向地\"");
            sql.AppendLine("     ,G.\"排気量\"");
            sql.AppendLine("     ,G.\"トランスミッション\"");
            sql.AppendLine("     ,G.\"駆動方式\"");
            sql.AppendLine("     ,F.\"車型\"");
            sql.AppendLine("     ,G.\"グレード\"");
            sql.AppendLine("     ,G.\"車体色\"");
            sql.AppendLine("     ,E.\"管理票番号\"");
            sql.AppendLine("     ,B.\"SCHEDULE_ID\"");
            sql.AppendLine("     ,A.\"CATEGORY_ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"CARSHARING_SCHEDULE\" A");
            sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_目的行先\" B ON A.\"ID\" = B.\"SCHEDULE_ID\"");
            sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_貸返備考\" C ON A.\"ID\" = C.\"SCHEDULE_ID\"");
            sql.AppendLine("    INNER JOIN \"CARSHARING_SCHEDULE_ITEM\" D ON A.\"CATEGORY_ID\" = D.\"ID\"");
            sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_最終予約日\" ON D.\"CATEGORY_ID\" = \"試験計画_外製車日程_最終予約日\".\"CATEGORY_ID\"");
            sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_車両リスト\" E ON D.\"CATEGORY_ID\" = E.\"CATEGORY_ID\"");
            sql.AppendLine("    INNER JOIN \"VIEW_試験車基本情報\" F ON E.\"管理票番号\" = F.\"管理票NO\"");
            sql.AppendLine("    INNER JOIN \"試験車履歴情報\" G ON F.\"データID\" = G.\"データID\"");
            sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" K ON B.\"予約者_ID\" = K.\"PERSONEL_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" H ON K.\"SECTION_GROUP_ID\" = H.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_DATA\" I ON H.\"SECTION_ID\" = I.\"SECTION_ID\"");
            //Append Start 2021/06/14 杉浦
            sql.AppendLine("    LEFT JOIN ");
            sql.AppendLine("        (SELECT ");
            //Update Start 2021/10/12 杉浦 カーシェア管理画面追加修正
            //sql.AppendLine("            TMP.* ");
            sql.AppendLine("            TMP1.ID BASE_ID ");
            sql.AppendLine("            , TMP2.* ");
            //Update End 2021/10/12 杉浦 カーシェア管理画面追加修正
            sql.AppendLine("         FROM ");
            //Append Start 2021/10/12 杉浦 カーシェア管理画面追加修正
            sql.AppendLine("            (SELECT ");
            sql.AppendLine("                A.\"CATEGORY_ID\"");
            sql.AppendLine("                ,A.\"ID\"");
            sql.AppendLine("                ,ROW_NUMBER() OVER (PARTITION BY A.\"CATEGORY_ID\" ORDER BY A.\"END_DATE\" DESC) ROW_NUM");
            sql.AppendLine("             FROM ");
            sql.AppendLine("                \"CARSHARING_SCHEDULE\" A");
            sql.AppendLine("             WHERE");
            sql.AppendLine("                A.\"START_DATE\" <= :PASSED_DATE + 1");
            sql.AppendLine("             ORDER BY A.END_DATE DESC");
            sql.AppendLine("             ) TMP1 ");
            sql.AppendLine("             LEFT JOIN ");
            //Append End 2021/10/12 杉浦 カーシェア管理画面追加修正
            sql.AppendLine("            (SELECT ");
            sql.AppendLine("                A.\"END_DATE\"");
            sql.AppendLine("                ,C.\"NAME\"");
            sql.AppendLine("                ,B.\"TEL\"");
            sql.AppendLine("                ,B.\"行先\"");
            sql.AppendLine("                ,B.\"目的\"");
            sql.AppendLine("                ,E.\"SECTION_CODE\"");
            sql.AppendLine("                ,A.\"CATEGORY_ID\"");
            //Append Start 2021/10/12 杉浦 カーシェア管理画面追加修正
            sql.AppendLine("                ,A.\"ID\"");
            //Append End 2021/10/12 杉浦 カーシェア管理画面追加修正
            sql.AppendLine("                ,ROW_NUMBER() OVER (PARTITION BY A.\"CATEGORY_ID\" ORDER BY A.\"END_DATE\" DESC) ROW_NUM");
            sql.AppendLine("             FROM ");
            sql.AppendLine("                \"CARSHARING_SCHEDULE\" A");
            sql.AppendLine("                INNER JOIN \"試験計画_外製車日程_目的行先\" B ON A.\"ID\" = B.\"SCHEDULE_ID\"");
            sql.AppendLine("                INNER JOIN \"PERSONEL_LIST\" C ON B.\"予約者_ID\" = C.\"PERSONEL_ID\"");
            sql.AppendLine("                INNER JOIN \"SECTION_GROUP_DATA\" D ON C.\"SECTION_GROUP_ID\" = D.\"SECTION_GROUP_ID\"");
            sql.AppendLine("                INNER JOIN \"SECTION_DATA\" E ON D.\"SECTION_ID\" = E.\"SECTION_ID\"");
            sql.AppendLine("             WHERE");
            //Update Start 2021/10/12 杉浦 カーシェア管理画面追加修正
            //sql.AppendLine("                A.\"END_DATE\" < :PASSED_DATE");
            //sql.AppendLine("             ORDER BY A.END_DATE DESC");
            //sql.AppendLine("             ) TMP");
            //sql.AppendLine("        WHERE TMP.ROW_NUM = 1) J ");
            //sql.AppendLine("    ON A.CATEGORY_ID = J.CATEGORY_ID ");
            sql.AppendLine("                A.\"START_DATE\" <= :PASSED_DATE + 1");
            sql.AppendLine("             ORDER BY A.CATEGORY_ID,  A.END_DATE DESC");
            sql.AppendLine("             ) TMP2");
            sql.AppendLine("        ON TMP1.CATEGORY_ID = TMP2.CATEGORY_ID AND TMP1.ROW_NUM + 1 = TMP2.ROW_NUM) J ");
            sql.AppendLine("    ON A.ID = J.BASE_ID ");
            //Update End 2021/10/12 杉浦 カーシェア管理画面追加修正
            //Append End 2021/06/14 杉浦
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND (\"試験計画_外製車日程_最終予約日\".\"最終予約可能日\" IS NULL OR \"試験計画_外製車日程_最終予約日\".\"最終予約可能日\" >= TRUNC(A.END_DATE))");
            
            //所在地
            if (cond.所在地 != null)
            {
                sql.AppendLine("    AND F.\"所在地\" = :所在地");
                prms.Add(new BindModel
                {
                    Name = ":所在地",
                    Type = OracleDbType.Varchar2,
                    Object = cond.所在地,
                    Direct = ParameterDirection.Input
                });
            }
            //貸出リスト
            if (cond.START_DATE != null)
            {
                sql.AppendLine("    AND :START_DATE <= A.\"START_DATE\"");
                sql.AppendLine("    AND A.\"START_DATE\" < (:START_DATE + 1)");
                prms.Add(new BindModel
                {
                    Name = ":START_DATE",
                    Type = OracleDbType.Date,
                    Object = cond.START_DATE,
                    Direct = ParameterDirection.Input
                });
            }
            //返却リスト
            if (cond.END_DATE != null)
            {
                sql.AppendLine("    AND :END_DATE <= A.\"END_DATE\"");
                sql.AppendLine("    AND A.\"END_DATE\" < (:END_DATE + 1)");
                prms.Add(new BindModel
                {
                    Name = ":END_DATE",
                    Type = OracleDbType.Date,
                    Object = cond.END_DATE,
                    Direct = ParameterDirection.Input
                });
            }
            //準備状況
            if (cond.FLAG_準備済 != null)
            {
                if (cond.FLAG_準備済 == 1)
                {
                    sql.AppendLine("    AND C.\"FLAG_準備済\" = 1");
                }
                else
                {
                    sql.AppendLine("    AND (NVL(C.\"FLAG_準備済\", 0) = 0)");
                }
            }
            //貸出状況
            if (cond.FLAG_実使用 != null)
            {
                if (cond.FLAG_実使用 == 1)
                {
                    sql.AppendLine("    AND B.\"FLAG_実使用\" = 1");
                }
                else
                {
                    sql.AppendLine("    AND (NVL(B.\"FLAG_実使用\", 0) = 0)");
                }
            }
            //返却状況
            if (cond.FLAG_返却済 != null)
            {
                if (cond.FLAG_返却済 == 1)
                {
                    sql.AppendLine("    AND C.\"FLAG_返却済\" = 1");
                }
                else
                {
                    sql.AppendLine("    AND (NVL(C.\"FLAG_返却済\", 0) = 0)");
                }
            }
            //給油状況
            if (cond.FLAG_給油済 != null)
            {
                if (cond.FLAG_給油済 == 1)
                {
                    sql.AppendLine("    AND C.\"FLAG_給油済\" = 1");
                }
                else
                {
                    sql.AppendLine("    AND (NVL(C.\"FLAG_給油済\", 0) = 0)");
                }
            }
            //予約者
            if (cond.NAME != null)
            {
                sql.AppendLine("    AND K.\"NAME\" LIKE '%' || :NAME || '%'");
                prms.Add(new BindModel
                {
                    Name = ":NAME",
                    Type = OracleDbType.Varchar2,
                    Object = cond.NAME,
                    Direct = ParameterDirection.Input
                });
            }
            //駐車場番号
            if (cond.駐車場番号 != null)
            {
                sql.AppendLine("    AND F.\"駐車場番号\" LIKE '%' || :駐車場番号 || '%'");
                prms.Add(new BindModel
                {
                    Name = ":駐車場番号",
                    Type = OracleDbType.Varchar2,
                    Object = cond.駐車場番号,
                    Direct = ParameterDirection.Input
                });
            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"START_DATE\"");
            sql.AppendLine("    ,F.\"駐車場番号\"");
            sql.AppendLine("    ,A.\"GENERAL_CODE\"");
            sql.AppendLine("    ,G.\"開発符号\"");
            sql.AppendLine("    ,E.\"管理票番号\"");

            //Append Start 2021/06/14 杉浦
            prms.Add(new BindModel
            {
                Name = ":PASSED_DATE",
                Type = OracleDbType.Date,
                Object = cond.START_DATE == null ? cond.END_DATE == null ? DateTime.Today : cond.END_DATE : cond.START_DATE,
                Direct = ParameterDirection.Input
            });
            //Append End 2021/06/14 杉浦

            //取得
            return db.ReadModelList<CarShareManagementItemModel>(sql.ToString(), prms);
        }
        #endregion

        #region カーシェア管理一覧項目更新
        /// <summary>
        /// カーシェア管理一覧項目更新
        /// </summary>
        /// <param name="list">カーシェア管理一覧項目</param>
        /// <returns>更新可否</returns>
        public bool UpdateCarShareManagementItem(List<CarShareManagementItemModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //目的行先を更新
                results.Add(this.UpdateDestinationItem(list));

                //貸返備考を更新
                results.Add(this.UpdateNoteItem(list));

                //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
                //入れ替え中車両データ(項目データ)更新
                results.Add(this.UpdateCarSharingScheduleItem(list));
                //Append ENd 2022/01/17 杉浦 入れ替え中車両の処理
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
        /// 目的行先を更新
        /// </summary>
        /// <param name="list">カーシェア管理一覧項目</param>
        /// <returns>更新可否</returns>
        private bool UpdateDestinationItem(List<CarShareManagementItemModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"試験計画_外製車日程_目的行先\"");
            sql.AppendLine("SET");
            sql.AppendLine("    \"FLAG_実使用\" = :FLAG_実使用");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"SCHEDULE_ID\" = :SCHEDULE_ID");

            var results = new List<bool>();

            foreach (var cond in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":SCHEDULE_ID",
                         Type = OracleDbType.Int64,
                         Object = cond.SCHEDULE_ID,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":FLAG_実使用",
                         Type = OracleDbType.Int16,
                         Object = cond.FLAG_実使用,
                         Direct = ParameterDirection.Input
                     }
                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));
            }

            return results.All(x => x == true);
        }

        /// <summary>
        /// 貸返備考を更新
        /// </summary>
        /// <param name="list">カーシェア管理一覧項目</param>
        /// <returns>更新可否</returns>
        private bool UpdateNoteItem(List<CarShareManagementItemModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"試験計画_外製車日程_貸返備考\"");
            sql.AppendLine("SET");
            sql.AppendLine("    \"FLAG_準備済\" = :FLAG_準備済");
            sql.AppendLine("    ,\"FLAG_返却済\" = :FLAG_返却済");
            sql.AppendLine("    ,\"FLAG_給油済\" = :FLAG_給油済");
            sql.AppendLine("    ,\"貸出備考\" = :貸出備考");
            sql.AppendLine("    ,\"返却備考\" = :返却備考");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"SCHEDULE_ID\" = :SCHEDULE_ID");

            var results = new List<bool>();

            foreach (var cond in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":SCHEDULE_ID",
                         Type = OracleDbType.Int64,
                         Object = cond.SCHEDULE_ID,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":FLAG_準備済",
                         Type = OracleDbType.Int16,
                         Object = cond.FLAG_準備済,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":FLAG_返却済",
                         Type = OracleDbType.Int16,
                         Object = cond.FLAG_返却済,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":FLAG_給油済",
                         Type = OracleDbType.Int16,
                         Object = cond.FLAG_給油済,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":貸出備考",
                         Type = OracleDbType.Varchar2,
                         Object = cond.貸出備考,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":返却備考",
                         Type = OracleDbType.Varchar2,
                         Object = cond.返却備考,
                         Direct = ParameterDirection.Input
                     }
                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));
            }

            return results.All(x => x == true);
        }
        #endregion

        //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
        #region スケジュール項目更新
        /// <summary>
        /// スケジュール項目更新
        /// </summary>
        /// <param name="list">カーシェア管理一覧項目</param>
        /// <returns>更新可否</returns>
        private bool UpdateCarSharingScheduleItem(List<CarShareManagementItemModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"CARSHARING_SCHEDULE_ITEM\"");
            sql.AppendLine("SET");
            sql.AppendLine("    \"REPLACEMENT_TEXT\" = :入れ替え中車両");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :CATEGORY_ID");

            var results = new List<bool>();

            foreach (var cond in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":CATEGORY_ID",
                         Type = OracleDbType.Int64,
                         Object = cond.CATEGORY_ID,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":入れ替え中車両",
                         Type = OracleDbType.Varchar2,
                         Object = cond.入れ替え中車両,
                         Direct = ParameterDirection.Input
                     },
                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));
            }

            return results.All(x => x == true);
        }
        #endregion
        //Append End 2022/01/17 杉浦 入れ替え中車両の処理

        /// <summary>
        /// 稼働率算出ロジック。
        /// </summary>
        /// <remarks>
        /// 稼働率計算の元となるデータを抽出し、LINQを利用して集計を行います。
        /// </remarks>
        /// <param name="cond">検索条件</param>
        /// <returns>稼働率クラス</returns>
        public List<CarShareManagementRatePrintOutModel> GetTest(CarShareManagementRatePrintSearchModel cond)
        {
            const string GUNMA_TEXT = "群馬";
            const string SKC_TEXT = "SKC";

            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("  GENE_DATA.ID");
            sql.AppendLine("  , GENE_DATA.所在地");
            sql.AppendLine("  , GENE_DATA.正式取得日");
            sql.AppendLine("  , GENE_DATA.CLOSED_DATE");
            sql.AppendLine("  , SCHEDULE_DATA.GENERAL_CODE");
            sql.AppendLine("  , SCHEDULE_DATA.START_DATE");
            sql.AppendLine("  , SCHEDULE_DATA.END_DATE");
            sql.AppendLine("  , MOKUTEKI_DATA.FLAG_実使用 ");
            sql.AppendLine("FROM");
            sql.AppendLine("  ( ");
            sql.AppendLine("    SELECT DISTINCT");
            sql.AppendLine("      A.ID");
            sql.AppendLine("      , F.所在地 ");
            sql.AppendLine("	  , F.正式取得日");
            sql.AppendLine("	  , A.CLOSED_DATE");
            sql.AppendLine("    FROM");
            sql.AppendLine("      CARSHARING_SCHEDULE_ITEM A ");
            sql.AppendLine("      INNER JOIN GENERAL_CODE B ");
            sql.AppendLine("        ON A.GENERAL_CODE = B.GENERAL_CODE ");
            sql.AppendLine("      INNER JOIN 試験計画_外製車日程_車両リスト E ");
            sql.AppendLine("        ON A.CATEGORY_ID = E.CATEGORY_ID ");
            sql.AppendLine("      INNER JOIN VIEW_試験車基本情報 F ");
            sql.AppendLine("        ON E.管理票番号 = F.管理票NO ");
            sql.AppendLine("    WHERE");
            sql.AppendLine("      F.正式取得日 < :END_DATE ");
            sql.AppendLine("      AND ( ");
            sql.AppendLine("        A.CLOSED_DATE IS NULL ");
            sql.AppendLine("        OR A.CLOSED_DATE > :START_DATE");
            sql.AppendLine("      )");
            sql.AppendLine("  ) GENE_DATA ");
            sql.AppendLine("  LEFT JOIN ( ");
            sql.AppendLine("    SELECT");
            sql.AppendLine("      * ");
            sql.AppendLine("    FROM");
            sql.AppendLine("      CARSHARING_SCHEDULE ");
            sql.AppendLine("    WHERE");
            sql.AppendLine("      START_DATE >= :START_DATE");
            sql.AppendLine("	  AND START_DATE <= :END_DATE");//予約開始が入力年月の範囲内＝予約
            sql.AppendLine("  ) SCHEDULE_DATA ");
            sql.AppendLine("    ON GENE_DATA.ID = SCHEDULE_DATA.CATEGORY_ID ");
            sql.AppendLine("  LEFT JOIN 試験計画_外製車日程_目的行先 MOKUTEKI_DATA ");
            sql.AppendLine("    ON SCHEDULE_DATA.ID = MOKUTEKI_DATA.SCHEDULE_ID");
            
            var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":START_DATE",
                         Type = OracleDbType.Date,
                         Object = cond.SearchStartDate,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":END_DATE",
                         Type = OracleDbType.Date,
                         Object = cond.SearchEndDate,
                         Direct = ParameterDirection.Input
                     }
                };
            
            var results = new List<bool>();
            var getData = db.ReadModelList<CarShareManagementRatePrintDataModel>(sql.ToString(), prms);
            
            var ret = new List<CarShareManagementRatePrintOutModel>();

            foreach (var d in EachDay(cond.SearchStartDate, cond.SearchEndDate))
            {
                var ddd = new CarShareManagementRatePrintOutModel()
                {
                    RateDate = d,
                    KadouCount = 0,
                    HoyuuCount = 0,
                    KadouRate = 0,
                    KadouGunmaCount = 0,
                    HoyuuGunmaCount = 0,
                    KadouGunmaRate = 0,
                    KadouSKCCount = 0,
                    HoyuuSKCCount = 0,
                    KadouSKCRate = 0
                };

                var hoyu = getData.Where(x => x.正式取得日.Date < d && (x.CLOSED_DATE == null || x.CLOSED_DATE.Value.Date > d));
                ddd.HoyuuCount = hoyu.GroupBy(x => x.ID).Count();

                var kadou = hoyu.Where(x => x.START_DATE != null && x.END_DATE != null &&
                    x.START_DATE.Value.Date <= d && x.END_DATE.Value.Date >= d && x.FLAG_実使用 == 1);
                ddd.KadouCount = kadou.GroupBy(x => x.ID).Count();

                ddd.KadouRate = (double)ddd.KadouCount / ddd.HoyuuCount;

                #region 群馬とSKCの絞り込み

                ddd.HoyuuGunmaCount = hoyu.Where(x => x.所在地 == GUNMA_TEXT).GroupBy(x => x.ID).Count();
                ddd.KadouGunmaCount = kadou.Where(x => x.所在地 == GUNMA_TEXT).GroupBy(x => x.ID).Count();
                ddd.KadouGunmaRate = (double)ddd.KadouGunmaCount / ddd.HoyuuGunmaCount;

                ddd.HoyuuSKCCount = hoyu.Where(x => x.所在地 == SKC_TEXT).GroupBy(x => x.ID).Count();
                ddd.KadouSKCCount = kadou.Where(x => x.所在地 == SKC_TEXT).GroupBy(x => x.ID).Count();
                ddd.KadouSKCRate = (double)ddd.KadouSKCCount / ddd.HoyuuSKCCount;

                #endregion

                ret.Add(ddd);
            }

            return ret;
        }

        /// <summary>
        /// 日付のIEnumerableを作成。
        /// </summary>
        /// <param name="from"></param>
        /// <param name="thru"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}