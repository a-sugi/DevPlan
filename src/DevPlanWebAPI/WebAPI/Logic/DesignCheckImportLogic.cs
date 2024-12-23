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
    //Append Start 2021/05/17 張晋華 開発計画表設計チェック機能改修

    /// <summary>
    /// 設計チェックインポート
    /// </summary>
    public class DesignCheckImportLogic : BaseLogic
    {
        #region 取得
        /// <summary>
        /// 指摘リストのデータ取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        public DataTable GetData(DesignCheckImportModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            //Update Start 2021/06/15 張晋華 開発計画表設計チェック機能改修

            sql.AppendLine("SELECT");
            sql.AppendLine("     試験計画_DCHK_指摘リスト.ID");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.指摘NO");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.試作管理NO");
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
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.FLAG_上司承認");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.完了確認日");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.担当課名");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.担当課_ID");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.担当者_ID");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.担当者_TEL");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.編集者日");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.編集者_ID");
            sql.AppendLine("    ,試験計画_DCHK_試験車.試験車名");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_指摘リスト");
            sql.AppendLine("    LEFT JOIN 試験計画_DCHK_状況");
            sql.AppendLine("    ON 試験計画_DCHK_指摘リスト.ID = 試験計画_DCHK_状況.指摘_ID");
            sql.AppendLine("    LEFT JOIN 試験計画_DCHK_対象車両");
            sql.AppendLine("    ON 試験計画_DCHK_状況.対象車両_ID = 試験計画_DCHK_対象車両.ID");
            sql.AppendLine("    LEFT JOIN 試験計画_DCHK_試験車");
            sql.AppendLine("    ON 試験計画_DCHK_対象車両.試験車_ID = 試験計画_DCHK_試験車.ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    試験計画_DCHK_指摘リスト.開催日_ID = :開催日_ID");
            sql.AppendLine("AND");
            sql.AppendLine("    試験計画_DCHK_指摘リスト.FLAG_最新 = 1");

            //Update End 2021/06/15 張晋華 開発計画表設計チェック機能改修

            prms.Add(new BindModel
            {
                Name = ":開催日_ID",
                Type = OracleDbType.Int32,
                Object = val.開催日_ID,
                Direct = ParameterDirection.Input
            });

            return db.ReadDataTable(sql.ToString(), prms);

        }
        #endregion

        #region 登録
        /// <summary>
        /// Excelデータの取込
        /// </summary>
        /// <param name="list">Excelデータ</param>
        /// <returns></returns>
        public bool PostData(List<DesignCheckImportModel> list, ref List<DesignCheckImportModel> res)
        {
            //開催日_ID
            var kaisaibi = list.FirstOrDefault()?.開催日_ID;

            //既存データの取得
            var pointList = this.GetData(new DesignCheckImportModel { 開催日_ID = kaisaibi });

            //指摘登録
            var isql = new StringBuilder();

            //指摘登録SQL
            isql.AppendLine("INSERT INTO");
            isql.AppendLine("試験計画_DCHK_指摘リスト (");
            isql.AppendLine("     ID");
            isql.AppendLine("    ,開催日_ID");
            //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
            isql.AppendLine("    ,試作管理NO");
            //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修
            //Update Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
            isql.AppendLine("    ,FLAG_試作CLOSE");
            //Update End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
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
            isql.AppendLine("    ,完了確認日");
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
            //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
            isql.AppendLine("    ,:試作管理NO");
            //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修
            //Update Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
            isql.AppendLine("    ,:FLAG_試作CLOSE");
            //Update End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
            isql.AppendLine("    ,:部品");
            isql.AppendLine("    ,:状況");
            isql.AppendLine("    ,:FLAG_処置不要");
            isql.AppendLine("    ,:処置課");
            isql.AppendLine("    ,:処置対象");
            isql.AppendLine("    ,:処置方法");
            isql.AppendLine("    ,1"); //固定で全てチェック入れる
            isql.AppendLine("    ,:処置調整");
            isql.AppendLine("    ,:織込日程");
            isql.AppendLine("    ,:FLAG_試作改修");
            isql.AppendLine("    ,:部品納入日");
            isql.AppendLine("    ,:FLAG_上司承認");
            isql.AppendLine("    ,:完了確認日");
            isql.AppendLine("    ,:担当課名");
            isql.AppendLine("    ,:担当課_ID");
            isql.AppendLine("    ,:担当者_ID");
            isql.AppendLine("    ,:担当者_TEL");
            isql.AppendLine("    ,:編集者日");
            isql.AppendLine("    ,:編集者_ID");
            isql.AppendLine("    ,1");
            isql.AppendLine(")");

            //Update Start 2021/06/16 張晋華 開発計画表設計チェック機能改修

            //状況登録
            var csql = new StringBuilder();
            csql.AppendLine("INSERT INTO");
            csql.AppendLine("試験計画_DCHK_状況 (");
            csql.AppendLine("     ID");
            csql.AppendLine("    ,対象車両_ID");
            csql.AppendLine("    ,指摘_ID");
            csql.AppendLine(") VALUES (");
            csql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_DCHK_状況)");
            csql.AppendLine("    ,(SELECT ID FROM 試験計画_DCHK_対象車両 WHERE 試験車_ID IN (SELECT ID FROM 試験計画_DCHK_試験車 WHERE 試験車名 = :試験車名) AND 開催日_ID = :開催日_ID)");
            csql.AppendLine("    ,(SELECT MAX(ID) FROM 試験計画_DCHK_指摘リスト)");
            csql.AppendLine(")");

            foreach (var val in list)
            {
                // 既存データの中に試験車名が同じ場合、同じ試作管理NOは存在するか
                var isSame = false;

                if (pointList.Rows.Count != 0)
                {
                    foreach (var dr in pointList.AsEnumerable().Where(x => x.Field<string>("試験車名") == val.試験車名))
                    {
                        isSame = (dr["試作管理NO"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["試作管理NO"])) == val.試作管理NO;

                        if (isSame)
                        {
                            break;
                        }
                    }
                }

                // 指摘登録
                var prms = new List<BindModel>()
                    {
                        new BindModel { Name = ":試作管理NO", Type = OracleDbType.Int32, Object = val.試作管理NO, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":開催日_ID", Type = OracleDbType.Int32, Object = val.開催日_ID, Direct = ParameterDirection.Input },
                        //Update Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
                        new BindModel { Name = ":FLAG_試作CLOSE", Type = OracleDbType.Int16, Object = val.FLAG_試作CLOSE, Direct = ParameterDirection.Input },
                        //Update End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
                        new BindModel { Name = ":部品", Type = OracleDbType.Varchar2, Object = val.部品, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":状況", Type = OracleDbType.Varchar2, Object = val.状況, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":FLAG_処置不要", Type = OracleDbType.Int16, Object = val.FLAG_処置不要, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":処置課", Type = OracleDbType.Varchar2, Object = val.処置課, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":処置対象", Type = OracleDbType.Varchar2, Object = val.処置対象, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":処置方法", Type = OracleDbType.Varchar2, Object = val.処置方法, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":処置調整", Type = OracleDbType.Varchar2, Object = val.処置調整, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":織込日程", Type = OracleDbType.Date, Object = val.織込日程, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":FLAG_試作改修", Type = OracleDbType.Int16, Object = val.FLAG_試作改修, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":部品納入日", Type = OracleDbType.Date, Object = val.部品納入日, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":FLAG_上司承認", Type = OracleDbType.Int16, Object = val.FLAG_上司承認, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":完了確認日", Type = OracleDbType.Date, Object = val.完了確認日, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":担当課名", Type = OracleDbType.Varchar2, Object = val.担当課名, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":担当課_ID", Type = OracleDbType.Varchar2, Object = val.担当課_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":担当者_ID", Type = OracleDbType.Varchar2, Object = val.担当者_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":担当者_TEL", Type = OracleDbType.Varchar2, Object = val.担当者_TEL, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":編集者日", Type = OracleDbType.Date, Object = val.編集者日, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":編集者_ID", Type = OracleDbType.Varchar2, Object = val.編集者_ID, Direct = ParameterDirection.Input },
                    };

                // 状況登録
                var cprms = new List<BindModel>()
                    {
                        new BindModel { Name = ":試験車名", Type = OracleDbType.Varchar2, Object = val.試験車名, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":開催日_ID", Type = OracleDbType.Int32, Object = val.開催日_ID, Direct = ParameterDirection.Input },
                    };

                // 試作管理NOと試験車名が同じ場合、内容が違っても処理しない
                if (!isSame)
                {
                    // 指摘の作成
                    if (!db.InsertData(isql.ToString(), prms))
                    {
                        return false;
                    }

                    res.Add(new DesignCheckImportModel()
                    {
                        試作管理NO = val.試作管理NO,
                        開催日_ID = val.開催日_ID,
                    });

                    // 状況登録
                    if (!db.InsertData(csql.ToString(), cprms))
                    {
                        return false;
                    }
                }
            }
            //Update End 2021/06/16 張晋華 開発計画表設計チェック機能改修
            return true;
        }
        #endregion

        #region パス参照
        /// <summary>
        /// パスの取得
        /// </summary>
        /// <returns></returns>
        public List<DesignCheckPathModel> GetPathData()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     \"ID\" ");
            sql.AppendLine("    ,\"PATH\" ");
            sql.AppendLine("FROM ");
            sql.AppendLine("    DCHK_PATH ");
            sql.AppendLine("WHERE 0 = 0 ");


            return db.ReadModelList<DesignCheckPathModel>(sql.ToString(), null);
        }
        #endregion
    }
    //Append End 2021/05/17 張晋華 開発計画表設計チェック機能改修
}