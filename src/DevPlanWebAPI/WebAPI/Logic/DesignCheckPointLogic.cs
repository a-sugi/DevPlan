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
    /// 設計チェック指摘ロジッククラス
    /// </summary>
    /// <remarks>設計チェック指摘の操作</remarks>
    public class DesignCheckPointLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 設計チェック指摘の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(DesignCheckPointGetInModel val, bool join = true, bool wait = false)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     試験計画_DCHK_指摘リスト.ID");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.開催日_ID");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.指摘NO");
            //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.試作管理NO");
            //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.FLAG_CLOSE");
            //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.FLAG_試作CLOSE");
            //Append End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.部品");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.状況");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.FLAG_処置不要");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.処置課");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.処置対象");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.処置方法");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.FLAG_調整済");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.処置調整");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.織込日程");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.FLAG_試作改修");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.部品納入日");
            sql.AppendLine("    ,CASE WHEN NVL(試験計画_DCHK_指摘リスト.FLAG_最新, 0) = 1 THEN 試験計画_DCHK_状況.完了確認日 ELSE 試験計画_DCHK_指摘リスト.完了確認日 END 完了確認日");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.FLAG_上司承認");
            sql.AppendLine("    ,試験計画_DCHK_試験車.試験車名");
            sql.AppendLine("    ,試験計画_DCHK_試験車.ID 試験車_ID");
            sql.AppendLine("    ,試験計画_DCHK_状況.対象車両_ID");
            sql.AppendLine("    ,試験計画_DCHK_状況.状況 状況記号");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.担当課名");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.担当課_ID");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.担当者_ID");
            if (join)
            {
                sql.AppendLine("    ,CASE WHEN PL1.NAME IS NULL THEN 試験計画_DCHK_指摘リスト.担当者_ID ELSE PL1.NAME END 担当者名");
            }
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.担当者_TEL");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.編集者日");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.編集者_ID");
            if (join)
            {
                //Update Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
                //sql.AppendLine("    ,PL2.NAME 編集者名");
                sql.AppendLine("    ,CASE WHEN PL2.NAME IS NULL THEN 試験計画_DCHK_指摘リスト.編集者_ID ELSE PL2.NAME END 編集者名");
                //Update End 2021/06/03 張晋華 開発計画表設計チェック機能改修
            }
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.履歴作成日");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.FLAG_最新");
            if (join)
            {
                sql.AppendLine("    ,履歴.HISTORY_COUNT");
            }
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_指摘リスト");
            if (join)
            {
                sql.AppendLine("    LEFT JOIN (SELECT * FROM 試験計画_DCHK_状況 WHERE ID IN(SELECT MIN(ID) FROM 試験計画_DCHK_状況 GROUP BY 指摘_ID, 対象車両_ID)) 試験計画_DCHK_状況");    // 不正データ対応
            }
            else
            {
                sql.AppendLine("    LEFT JOIN 試験計画_DCHK_状況");
            }
            sql.AppendLine("    ON 試験計画_DCHK_指摘リスト.ID = 試験計画_DCHK_状況.指摘_ID");
            sql.AppendLine("    LEFT JOIN 試験計画_DCHK_対象車両");
            sql.AppendLine("    ON 試験計画_DCHK_状況.対象車両_ID = 試験計画_DCHK_対象車両.ID");
            sql.AppendLine("    LEFT JOIN 試験計画_DCHK_試験車");
            sql.AppendLine("    ON 試験計画_DCHK_対象車両.試験車_ID = 試験計画_DCHK_試験車.ID");
            if (join)
            {
                //Update Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
                sql.AppendLine("    LEFT JOIN (SELECT 開催日_ID, 指摘NO, 試作管理NO, COUNT(1) HISTORY_COUNT FROM 試験計画_DCHK_指摘リスト WHERE NVL(FLAG_最新, 0) <> 1 GROUP BY 開催日_ID, 指摘NO, 試作管理NO) 履歴");
                sql.AppendLine("    ON 試験計画_DCHK_指摘リスト.開催日_ID = 履歴.開催日_ID");
                sql.AppendLine("    AND (試験計画_DCHK_指摘リスト.指摘NO IS NULL OR 試験計画_DCHK_指摘リスト.指摘NO = 履歴.指摘NO)");
                sql.AppendLine("    AND (試験計画_DCHK_指摘リスト.試作管理NO IS NULL OR 試験計画_DCHK_指摘リスト.試作管理NO = 履歴.試作管理NO)");
                //Update End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
                sql.AppendLine("    LEFT JOIN PERSONEL_LIST PL1");
                sql.AppendLine("    ON 試験計画_DCHK_指摘リスト.担当者_ID = PL1.PERSONEL_ID");
                sql.AppendLine("    LEFT JOIN PERSONEL_LIST PL2");
                sql.AppendLine("    ON 試験計画_DCHK_指摘リスト.編集者_ID = PL2.PERSONEL_ID");
            }
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // 指摘ID
            if (val != null && val.ID > 0)
            {
                sql.AppendLine("    AND 試験計画_DCHK_指摘リスト.ID = :ID");

                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Int32,
                    Object = val.ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 開催日ID
            if (val != null && val.開催日_ID > 0)
            {
                sql.AppendLine("    AND 試験計画_DCHK_指摘リスト.開催日_ID = :開催日_ID");

                prms.Add(new BindModel
                {
                    Name = ":開催日_ID",
                    Type = OracleDbType.Int32,
                    Object = val.開催日_ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 指摘NO
            if (val != null && val.指摘NO > 0)
            {
                sql.AppendLine("    AND 試験計画_DCHK_指摘リスト.指摘NO = :指摘NO");

                prms.Add(new BindModel
                {
                    Name = ":指摘NO",
                    Type = OracleDbType.Int32,
                    Object = val.指摘NO,
                    Direct = ParameterDirection.Input
                });
            }

            //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
            // 試作管理NO
            if (val != null && val.試作管理NO > 0)
            {
                sql.AppendLine("    AND 試験計画_DCHK_指摘リスト.試作管理NO = :試作管理NO");

                prms.Add(new BindModel
                {
                    Name = ":試作管理NO",
                    Type = OracleDbType.Int32,
                    Object = val.試作管理NO,
                    Direct = ParameterDirection.Input
                });
            }
            //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修

            // 試験車ID
            if (val != null && val.試験車_ID > 0)
            {
                sql.AppendLine("    AND 試験計画_DCHK_対象車両.試験車_ID = :試験車_ID");

                prms.Add(new BindModel
                {
                    Name = ":試験車_ID",
                    Type = OracleDbType.Int32,
                    Object = val.試験車_ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 状況記号
            if (val != null && !string.IsNullOrWhiteSpace(val.状況記号))
            {
                sql.AppendLine("    AND 試験計画_DCHK_状況.状況 = :状況記号");

                prms.Add(new BindModel
                {
                    Name = ":状況記号",
                    Type = OracleDbType.Varchar2,
                    Object = val.状況記号,
                    Direct = ParameterDirection.Input
                });
            }

            // 担当課名
            if (val != null && !string.IsNullOrWhiteSpace(val.担当課名))
            {
                sql.AppendLine("    AND 試験計画_DCHK_指摘リスト.担当課名 = :担当課名");

                prms.Add(new BindModel
                {
                    Name = ":担当課名",
                    Type = OracleDbType.Varchar2,
                    Object = val.担当課名,
                    Direct = ParameterDirection.Input
                });
            }

            // 担当課長承認フラグ（true：承認済、false：未承認）
            if (val != null && val.APPROVAL_FLG == true)
            {
                sql.AppendLine("    AND NVL(試験計画_DCHK_指摘リスト.FLAG_上司承認, 0) = 1");
            }
            else if (val != null && val.APPROVAL_FLG == false)
            {
                sql.AppendLine("    AND NVL(試験計画_DCHK_指摘リスト.FLAG_上司承認, 0) <> 1");
            }

            // オープンフラグ（true：Open、false：Close）
            if (val != null && val.OPEN_FLG == true)
            {
                sql.AppendLine("    AND NVL(試験計画_DCHK_指摘リスト.FLAG_CLOSE, 0) <> 1");
            }
            else if (val != null && val.OPEN_FLG == false)
            {
                sql.AppendLine("    AND NVL(試験計画_DCHK_指摘リスト.FLAG_CLOSE, 0) = 1");
            }

            // 最新フラグ（true：最新、false：履歴）
            if (val != null && val.NEW_FLG == true)
            {
                sql.AppendLine("    AND NVL(試験計画_DCHK_指摘リスト.FLAG_最新, 0) = 1");
            }
            else if (val != null && val.NEW_FLG == false)
            {
                sql.AppendLine("    AND NVL(試験計画_DCHK_指摘リスト.FLAG_最新, 0) <> 1");
            }

            sql.AppendLine("ORDER BY");
            //Append Start 2021/06/14 張晋華 開発計画表設計チェック機能改修
            sql.AppendLine("     試験計画_DCHK_指摘リスト.指摘NO ASC NULLS FIRST");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.試作管理NO ASC");
            sql.AppendLine("    ,試験計画_DCHK_試験車.ID ASC");
            //Append End 2021/06/14 張晋華 開発計画表設計チェック機能改修
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.FLAG_最新 DESC");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.ID DESC");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.編集者日 DESC");
            sql.AppendLine("    ,NLSSORT(試験計画_DCHK_試験車.試験車名, 'NLS_SORT=JAPANESE_M') NULLS FIRST");

            if (wait)
            {
                sql.AppendLine("FOR UPDATE");
            }

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 設計チェック指摘の作成（更新）
        /// </summary>
        /// <param name="list"></param>
        /// <param name="returns"></param>
        /// <returns>bool</returns>
        public bool PostData(List<DesignCheckPointPostInModel> list, ref List<DesignCheckPointPostOutModel> returns)
        {
            // 指摘リストの取得
            var pointList = this.GetData(new DesignCheckPointGetInModel { 開催日_ID = (int)list.FirstOrDefault()?.開催日_ID, NEW_FLG = true }, false, true);

            // 履歴登録
            var hsql = new StringBuilder();

            // 履歴登録SQL
            hsql.AppendLine("INSERT INTO");
            hsql.AppendLine("試験計画_DCHK_指摘リスト (");
            hsql.AppendLine("     ID");
            hsql.AppendLine("    ,開催日_ID");
            hsql.AppendLine("    ,指摘NO");
            //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
            hsql.AppendLine("    ,試作管理NO");
            //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修
            hsql.AppendLine("    ,FLAG_CLOSE");
            //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
            hsql.AppendLine("    ,FLAG_試作CLOSE");
            //Append End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
            hsql.AppendLine("    ,部品");
            hsql.AppendLine("    ,状況");
            hsql.AppendLine("    ,FLAG_処置不要");
            hsql.AppendLine("    ,処置課");
            hsql.AppendLine("    ,処置対象");
            hsql.AppendLine("    ,処置方法");
            hsql.AppendLine("    ,FLAG_調整済");
            hsql.AppendLine("    ,処置調整");
            hsql.AppendLine("    ,織込日程");
            hsql.AppendLine("    ,FLAG_試作改修");
            hsql.AppendLine("    ,部品納入日");
            hsql.AppendLine("    ,完了確認日");  // 履歴のみ変更前の状況の完了確認日を登録
            hsql.AppendLine("    ,FLAG_上司承認");
            hsql.AppendLine("    ,担当課名");
            hsql.AppendLine("    ,担当課_ID");
            hsql.AppendLine("    ,担当者_ID");
            hsql.AppendLine("    ,担当者_TEL");
            hsql.AppendLine("    ,編集者日");
            hsql.AppendLine("    ,編集者_ID");
            hsql.AppendLine("    ,親_ID");
            hsql.AppendLine("    ,履歴作成日");
            hsql.AppendLine("    ,FLAG_最新");
            hsql.AppendLine(") VALUES (");
            hsql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_DCHK_指摘リスト)");
            hsql.AppendLine("    ,:開催日_ID");
            hsql.AppendLine("    ,:指摘NO");
            //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
            hsql.AppendLine("    ,:試作管理NO");
            //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修
            hsql.AppendLine("    ,:FLAG_CLOSE");
            //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
            hsql.AppendLine("    ,:FLAG_試作CLOSE");
            //Append End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
            hsql.AppendLine("    ,:部品");
            hsql.AppendLine("    ,:状況");
            hsql.AppendLine("    ,:FLAG_処置不要");
            hsql.AppendLine("    ,:処置課");
            hsql.AppendLine("    ,:処置対象");
            hsql.AppendLine("    ,:処置方法");
            hsql.AppendLine("    ,:FLAG_調整済");
            hsql.AppendLine("    ,:処置調整");
            hsql.AppendLine("    ,:織込日程");
            hsql.AppendLine("    ,:FLAG_試作改修");
            hsql.AppendLine("    ,:部品納入日");
            hsql.AppendLine("    ,:完了確認日");  // 履歴のみ変更前の状況の完了確認日を登録
            hsql.AppendLine("    ,:FLAG_上司承認");
            hsql.AppendLine("    ,:担当課名");
            hsql.AppendLine("    ,:担当課_ID");
            hsql.AppendLine("    ,:担当者_ID");
            hsql.AppendLine("    ,:担当者_TEL");
            hsql.AppendLine("    ,:編集者日");
            hsql.AppendLine("    ,:編集者_ID");
            hsql.AppendLine("    ,:親_ID");
            hsql.AppendLine("    ,SYSDATE");
            hsql.AppendLine("    ,0");
            hsql.AppendLine(")");

            // 指摘登録
            var isql = new StringBuilder();

            // 指摘登録SQL
            isql.AppendLine("INSERT INTO");
            isql.AppendLine("試験計画_DCHK_指摘リスト (");
            isql.AppendLine("     ID");
            isql.AppendLine("    ,開催日_ID");
            isql.AppendLine("    ,指摘NO");
            isql.AppendLine("    ,FLAG_CLOSE");
            isql.AppendLine("    ,部品");
            isql.AppendLine("    ,状況");
            isql.AppendLine("    ,FLAG_処置不要");
            isql.AppendLine("    ,処置課");
            isql.AppendLine("    ,処置対象");
            isql.AppendLine("    ,処置方法");
            isql.AppendLine("    ,FLAG_調整済");
            isql.AppendLine("    ,処置調整");
            isql.AppendLine("    ,織込日程");
            isql.AppendLine("    ,FLAG_試作改修");
            isql.AppendLine("    ,部品納入日");
            isql.AppendLine("    ,FLAG_上司承認");
            isql.AppendLine("    ,担当課名");
            isql.AppendLine("    ,担当課_ID");
            isql.AppendLine("    ,担当者_ID");
            isql.AppendLine("    ,担当者_TEL");
            isql.AppendLine("    ,編集者日");
            isql.AppendLine("    ,編集者_ID");
            isql.AppendLine("    ,FLAG_最新");
            isql.AppendLine(") VALUES (");
            isql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_DCHK_指摘リスト)");
            isql.AppendLine("    ,:開催日_ID");
            isql.AppendLine("    ,(SELECT NVL(MAX(指摘NO), 0) + 1 FROM 試験計画_DCHK_指摘リスト WHERE 開催日_ID = :開催日_ID AND FLAG_最新 = 1)");
            isql.AppendLine("    ,:FLAG_CLOSE");
            isql.AppendLine("    ,:部品");
            isql.AppendLine("    ,:状況");
            isql.AppendLine("    ,:FLAG_処置不要");
            isql.AppendLine("    ,:処置課");
            isql.AppendLine("    ,:処置対象");
            isql.AppendLine("    ,:処置方法");
            isql.AppendLine("    ,:FLAG_調整済");
            isql.AppendLine("    ,:処置調整");
            isql.AppendLine("    ,:織込日程");
            isql.AppendLine("    ,:FLAG_試作改修");
            isql.AppendLine("    ,:部品納入日");
            isql.AppendLine("    ,:FLAG_上司承認");
            isql.AppendLine("    ,:担当課名");
            isql.AppendLine("    ,:担当課_ID");
            isql.AppendLine("    ,:担当者_ID");
            isql.AppendLine("    ,:担当者_TEL");
            isql.AppendLine("    ,SYSDATE");
            isql.AppendLine("    ,:編集者_ID");
            isql.AppendLine("    ,1");
            isql.AppendLine(") RETURNING");
            isql.AppendLine("    ID INTO :newid");

            // 指摘更新
            var usql = new StringBuilder();

            // 指摘更新SQL
            usql.AppendLine("UPDATE");
            usql.AppendLine("    試験計画_DCHK_指摘リスト");
            usql.AppendLine("SET");
            usql.AppendLine("     FLAG_CLOSE = :FLAG_CLOSE");
            usql.AppendLine("    ,部品 = :部品");
            usql.AppendLine("    ,状況 = :状況");
            usql.AppendLine("    ,FLAG_処置不要 = :FLAG_処置不要");
            usql.AppendLine("    ,処置課 = :処置課");
            usql.AppendLine("    ,処置対象 = :処置対象");
            usql.AppendLine("    ,処置方法 = :処置方法");
            usql.AppendLine("    ,FLAG_調整済 = :FLAG_調整済");
            usql.AppendLine("    ,処置調整 = :処置調整");
            usql.AppendLine("    ,織込日程 = :織込日程");
            usql.AppendLine("    ,FLAG_試作改修 = :FLAG_試作改修");
            usql.AppendLine("    ,部品納入日 = :部品納入日");
            usql.AppendLine("    ,FLAG_上司承認 = :FLAG_上司承認");
            usql.AppendLine("    ,担当課名 = :担当課名");
            usql.AppendLine("    ,担当課_ID = :担当課_ID");
            usql.AppendLine("    ,担当者_ID = :担当者_ID");
            usql.AppendLine("    ,担当者_TEL = :担当者_TEL");
            usql.AppendLine("    ,編集者日 = SYSDATE");
            usql.AppendLine("    ,編集者_ID = :編集者_ID");
            usql.AppendLine("WHERE");
            usql.AppendLine("    ID = :ID");
            usql.AppendLine("    AND 開催日_ID = :開催日_ID");
            //Delete Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
            //usql.AppendLine("    AND 指摘NO = :指摘NO");
            //Delete End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

            foreach (var val in list)
            {
                var isExists = false;
                var isChange = false;

                DataRow row = null;

                //Update Start 2021/06/14 張晋華 開発計画表設計チェック機能改修
                // 既存データがあり、指摘IDが採番されている場合
                if (pointList != null && val.ID > 0 /*&& val.指摘NO > 0*/)
                //Update End 2021/06/14 張晋華 開発計画表設計チェック機能改修
                {
                    // 存在・更新チェック
                    foreach (var dr in pointList.AsEnumerable().Where(x => x.Field<int>("ID") == (int)val.ID))
                    {
                        // 存在フラグ
                        isExists = true;

                        // 変更フラグ
                        isChange =
                            (dr["FLAG_CLOSE"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_CLOSE"])) != val.FLAG_CLOSE ||
                            (string)(dr["部品"] == DBNull.Value ? string.Empty : dr["部品"]) != val.部品 ||
                            (string)(dr["状況"] == DBNull.Value ? string.Empty : dr["状況"]) != val.状況 ||
                            (dr["FLAG_処置不要"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_処置不要"])) != val.FLAG_処置不要 ||
                            (string)(dr["処置課"] == DBNull.Value ? string.Empty : dr["処置課"]) != val.処置課 ||
                            (string)(dr["処置対象"] == DBNull.Value ? string.Empty : dr["処置対象"]) != val.処置対象 ||
                            (string)(dr["処置方法"] == DBNull.Value ? string.Empty : dr["処置方法"]) != val.処置方法 ||
                            (dr["FLAG_調整済"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_調整済"])) != val.FLAG_調整済 ||
                            (string)(dr["処置調整"] == DBNull.Value ? string.Empty : dr["処置調整"]) != val.処置調整 ||
                            (dr["織込日程"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["織込日程"])) != val.織込日程 ||
                            (dr["FLAG_試作改修"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_試作改修"])) != val.FLAG_試作改修 ||
                            (dr["部品納入日"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["部品納入日"])) != val.部品納入日 ||
                            (dr["FLAG_上司承認"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_上司承認"])) != val.FLAG_上司承認 ||
                            (string)(dr["担当課名"] == DBNull.Value ? string.Empty : dr["担当課名"]) != val.担当課名 ||
                            (string)(dr["担当者_ID"] == DBNull.Value ? string.Empty : dr["担当者_ID"]) != val.担当者_ID ||
                            (string)(dr["担当者_TEL"] == DBNull.Value ? string.Empty : dr["担当者_TEL"]) != val.担当者_TEL;
                            //Delete Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
                            //val.PROGRESS_LIST.Any(x => x.対象車両_ID == Convert.ToInt16(dr["対象車両_ID"]) && (dr["完了確認日"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["完了確認日"])) != x.完了確認日);
                            //Delete End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

                        row = dr;

                        // 変更がある場合
                        if (isChange)
                        {
                            break;
                        }
                    }
                }

                // 履歴バインド変数
                var hprms = new List<BindModel>();

                if (isChange)
                {
                    hprms.Add(new BindModel { Name = ":FLAG_CLOSE", Type = OracleDbType.Int16, Object = row["FLAG_CLOSE"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":部品", Type = OracleDbType.Varchar2, Object = row["部品"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":状況", Type = OracleDbType.Varchar2, Object = row["状況"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":FLAG_処置不要", Type = OracleDbType.Int16, Object = row["FLAG_処置不要"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":処置課", Type = OracleDbType.Varchar2, Object = row["処置課"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":処置対象", Type = OracleDbType.Varchar2, Object = row["処置対象"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":処置方法", Type = OracleDbType.Varchar2, Object = row["処置方法"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":FLAG_調整済", Type = OracleDbType.Int16, Object = row["FLAG_調整済"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":処置調整", Type = OracleDbType.Varchar2, Object = row["処置調整"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":織込日程", Type = OracleDbType.Date, Object = row["織込日程"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":FLAG_試作改修", Type = OracleDbType.Int16, Object = row["FLAG_試作改修"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":部品納入日", Type = OracleDbType.Date, Object = row["部品納入日"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":完了確認日", Type = OracleDbType.Date, Object = row["完了確認日"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":FLAG_上司承認", Type = OracleDbType.Int16, Object = row["FLAG_上司承認"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":担当課名", Type = OracleDbType.Varchar2, Object = row["担当課名"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":担当課_ID", Type = OracleDbType.Varchar2, Object = row["担当課_ID"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":担当者_ID", Type = OracleDbType.Varchar2, Object = row["担当者_ID"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":担当者_TEL", Type = OracleDbType.Varchar2, Object = row["担当者_TEL"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":編集者日", Type = OracleDbType.Date, Object = row["編集者日"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":編集者_ID", Type = OracleDbType.Varchar2, Object = row["編集者_ID"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":親_ID", Type = OracleDbType.Int32, Object = row["ID"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":開催日_ID", Type = OracleDbType.Int32, Object = row["開催日_ID"], Direct = ParameterDirection.Input });
                    hprms.Add(new BindModel { Name = ":指摘NO", Type = OracleDbType.Int32, Object = row["指摘NO"], Direct = ParameterDirection.Input });
                    //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
                    hprms.Add(new BindModel { Name = ":試作管理NO", Type = OracleDbType.Int32, Object = row["試作管理NO"], Direct = ParameterDirection.Input });
                    //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修
                    //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
                    hprms.Add(new BindModel { Name = ":FLAG_試作CLOSE", Type = OracleDbType.Int16, Object = row["FLAG_試作CLOSE"], Direct = ParameterDirection.Input });
                    //Append End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
                }

                // 更新・登録バインド変数
                var cprms = new List<BindModel>();

                if (isExists)
                {
                    cprms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = row["ID"], Direct = ParameterDirection.Input });
                }
                cprms.Add(new BindModel { Name = ":FLAG_CLOSE", Type = OracleDbType.Int16, Object = val.FLAG_CLOSE, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":部品", Type = OracleDbType.Varchar2, Object = val.部品, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":状況", Type = OracleDbType.Varchar2, Object = val.状況, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":FLAG_処置不要", Type = OracleDbType.Int16, Object = val.FLAG_処置不要, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":処置課", Type = OracleDbType.Varchar2, Object = val.処置課, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":処置対象", Type = OracleDbType.Varchar2, Object = val.処置対象, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":処置方法", Type = OracleDbType.Varchar2, Object = val.処置方法, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":FLAG_調整済", Type = OracleDbType.Int16, Object = val.FLAG_調整済, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":処置調整", Type = OracleDbType.Varchar2, Object = val.処置調整, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":織込日程", Type = OracleDbType.Date, Object = getDateTime(val.織込日程), Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":FLAG_試作改修", Type = OracleDbType.Int16, Object = val.FLAG_試作改修, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":部品納入日", Type = OracleDbType.Date, Object = getDateTime(val.部品納入日), Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":FLAG_上司承認", Type = OracleDbType.Int16, Object = val.FLAG_上司承認, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":担当課名", Type = OracleDbType.Varchar2, Object = val.担当課名, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":担当課_ID", Type = OracleDbType.Varchar2, Object = val.担当課_ID, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":担当者_ID", Type = OracleDbType.Varchar2, Object = val.担当者_ID, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":担当者_TEL", Type = OracleDbType.Varchar2, Object = val.担当者_TEL, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":編集者_ID", Type = OracleDbType.Varchar2, Object = val.編集者_ID, Direct = ParameterDirection.Input });
                cprms.Add(new BindModel { Name = ":開催日_ID", Type = OracleDbType.Int32, Object = val.開催日_ID, Direct = ParameterDirection.Input });
                //Delete Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
                //cprms.Add(new BindModel { Name = ":指摘NO", Type = OracleDbType.Int32, Object = val.指摘NO, Direct = ParameterDirection.Input });
                //Delete End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

                var pointId = (int)val.ID;

                if (isExists)
                {
                    if (isChange)
                    {
                        // 指摘履歴の作成
                        if (!db.InsertData(hsql.ToString(), hprms))
                        {
                            return false;
                        }

                        // 指摘の更新
                        if (!db.UpdateData(usql.ToString(), cprms))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    // 採番ID：戻り値設定
                    db.Returns = new List<BindModel>();
                    db.Returns.Add(new BindModel
                    {
                        Name = ":newid",
                        Type = OracleDbType.Int32,
                        Direct = ParameterDirection.Input
                    });

                    // 指摘の作成
                    if (!db.InsertData(isql.ToString(), cprms))
                    {
                        return false;
                    }

                    if (val.ID < 0)
                    {
                        // 指摘IDの取得
                        pointId = Convert.ToInt32(db.Returns.First((x) => x.Name == ":newid").Object.ToString());
                        returns.Add(new DesignCheckPointPostOutModel() { TEMP_ID = (int)val.ID, ID = pointId });
                    }
                }

                // 状況がある場合
                if (val.PROGRESS_LIST.Count > 0)
                {
                    var logic = new DesignCheckProgressLogic();
                    logic.SetDBAccess(this.db);

                    // 状況の作成（更新）
                    if (!logic.PostData(val.PROGRESS_LIST.Select(x => { x.指摘_ID = pointId; return x; }).ToList()))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 設計チェック指摘の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(List<DesignCheckPointDeleteInModel> list)
        {
            StringBuilder ssql = new StringBuilder();

            ssql.AppendLine("DELETE");
            ssql.AppendLine("FROM");
            ssql.AppendLine("    試験計画_DCHK_指摘リスト");
            ssql.AppendLine("WHERE");
            //Delete Start 2021/07/05 張晋華 開発計画表設計チェック機能改修(保守対応)
            //ssql.AppendLine("    (開催日_ID, 指摘NO) IN (SELECT 開催日_ID, 指摘NO FROM 試験計画_DCHK_指摘リスト WHERE ID = :指摘_ID)");
            //Delete End 2021/07/05 張晋華 開発計画表設計チェック機能改修(保守対応)
            //Append Start 2021/07/05 張晋華 開発計画表設計チェック機能改修(保守対応)
            ssql.AppendLine("    ID = :指摘_ID");
            ssql.AppendLine("    OR 親_ID = :指摘_ID");
            //Appen End 2021/07/05 張晋華 開発計画表設計チェック機能改修(保守対応)

            StringBuilder jsql = new StringBuilder();

            jsql.AppendLine("DELETE");
            jsql.AppendLine("FROM");
            jsql.AppendLine("    試験計画_DCHK_状況");
            jsql.AppendLine("WHERE");
            jsql.AppendLine("    指摘_ID = :指摘_ID");

            foreach (var val in list)
            {
                List<BindModel> prms = new List<BindModel>();

                // 指摘ID：必須
                prms.Add(new BindModel
                {
                    Name = ":指摘_ID",
                    Type = OracleDbType.Int32,
                    Object = val.ID,
                    Direct = ParameterDirection.Input
                });

                if (!db.DeleteData(ssql.ToString(), prms))
                {
                    return false;
                }

                if (!db.DeleteData(jsql.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }
    }
}