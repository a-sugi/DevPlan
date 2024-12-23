using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 設計チェック指摘と状況の同時登録ロジッククラス
    /// </summary>
    public class DesignCheckPointProgLogic : BaseLogic
    {
        /// <summary>
        /// 設計チェック指摘・状況の作成
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool PostData(List<DesignCheckProgressPostInModel> list)
        {
            // 指摘
            StringBuilder pointSql = new StringBuilder();
            pointSql.AppendLine("INSERT INTO");
            pointSql.AppendLine("試験計画_DCHK_指摘リスト (");
            pointSql.AppendLine("     ID");
            pointSql.AppendLine("    ,開催日_ID");
            pointSql.AppendLine("    ,指摘NO");
            pointSql.AppendLine("    ,編集者日");
            pointSql.AppendLine("    ,編集者_ID");
            pointSql.AppendLine("    ,FLAG_最新");
            pointSql.AppendLine(") VALUES (");
            pointSql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_DCHK_指摘リスト)");
            pointSql.AppendLine("    ,:開催日_ID");
            pointSql.AppendLine("    ,(SELECT NVL(MAX(指摘NO), 0) + 1 FROM 試験計画_DCHK_指摘リスト WHERE 開催日_ID = :開催日_ID AND FLAG_最新 = 1)");
            pointSql.AppendLine("    ,SYSDATE");
            pointSql.AppendLine("    ,:編集者_ID");
            pointSql.AppendLine("    ,1");
            pointSql.AppendLine(") RETURNING");
            pointSql.AppendLine("    ID INTO :newid");

            var param = new List<BindModel>()
            {
                new BindModel
                {
                    Name = ":開催日_ID",
                    Type = OracleDbType.Int32,
                    Object = list.First().開催日_ID,
                    Direct = ParameterDirection.Input
                },

                new BindModel
                {
                    Name = ":編集者_ID",
                    Type = OracleDbType.Varchar2,
                    Object = list.First().編集者_ID,
                    Direct = ParameterDirection.Input
                },
            };

            // 採番ID：戻り値設定
            db.Returns = new List<BindModel>();
            db.Returns.Add(new BindModel
            {
                Name = ":newid",
                Type = OracleDbType.Int32,
                Direct = ParameterDirection.Input
            });

            if (!db.InsertData(pointSql.ToString(), param))
            {
                return false;
            }

            // 指摘_ID取得
            var pointId = Convert.ToInt32(db.Returns.First((x) => x.Name == ":newid").Object.ToString());

            // 状況
            StringBuilder progSql = new StringBuilder();
            progSql.AppendLine("INSERT INTO");
            progSql.AppendLine("試験計画_DCHK_状況 (");
            progSql.AppendLine("     ID");
            progSql.AppendLine("    ,対象車両_ID");
            progSql.AppendLine("    ,指摘_ID");
            //Append Start 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
            progSql.AppendLine("    ,状況");
            //Append End 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
            progSql.AppendLine(") VALUES (");
            progSql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_DCHK_状況)");
            progSql.AppendLine("    ,:対象車両_ID");
            progSql.AppendLine("    ,:指摘_ID");
            //Append Start 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
            progSql.AppendLine("    ,:状況");
            //Append End 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
            progSql.AppendLine(")");

            foreach (var model in list)
            {
                var prms = new List<BindModel>()
                {
                    // 対象車両ID：必須
                    new BindModel
                    {
                        Name = ":対象車両_ID",
                        Type = OracleDbType.Int32,
                        Object = model.対象車両_ID,
                        Direct = ParameterDirection.Input
                    },

                    // 指摘ID：必須
                    new BindModel
                    {
                        Name = ":指摘_ID",
                        Type = OracleDbType.Int32,
                        Object = pointId,
                        Direct = ParameterDirection.Input
                    },

                    //Append Start 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
                    // 状況：必須
                    new BindModel
                    {
                        Name = ":状況",
                        Type = OracleDbType.Varchar2,
                        Object = model.状況,
                        Direct = ParameterDirection.Input
                    },
                    //Append End 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
                };

                if (!db.InsertData(progSql.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }
    }
}