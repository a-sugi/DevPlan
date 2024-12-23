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
    /// 設計チェック状況ロジッククラス
    /// </summary>
    /// <remarks>設計チェック状況の操作</remarks>
    public class DesignCheckProgressLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 設計チェック状況の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(DesignCheckProgressGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     試験計画_DCHK_状況.対象車両_ID");
            sql.AppendLine("    ,試験計画_DCHK_状況.指摘_ID");
            sql.AppendLine("    ,試験計画_DCHK_状況.状況");
            sql.AppendLine("    ,試験計画_DCHK_対象車両.開催日_ID");
            sql.AppendLine("    ,試験計画_DCHK_対象車両.試験車_ID");
            sql.AppendLine("    ,試験計画_DCHK_試験車.管理票NO");
            sql.AppendLine("    ,試験計画_DCHK_試験車.試験車名");
            sql.AppendLine("    ,試験計画_DCHK_試験車.グレード");
            sql.AppendLine("    ,試験計画_DCHK_試験車.試験目的");
            sql.AppendLine("FROM");
            sql.AppendLine("    (SELECT * FROM 試験計画_DCHK_状況 WHERE ID IN(SELECT MIN(ID) FROM 試験計画_DCHK_状況 GROUP BY 指摘_ID, 対象車両_ID)) 試験計画_DCHK_状況");    // 不正データ対応
            sql.AppendLine("    INNER JOIN 試験計画_DCHK_対象車両");
            sql.AppendLine("    ON 試験計画_DCHK_状況.対象車両_ID = 試験計画_DCHK_対象車両.ID");
            sql.AppendLine("    INNER JOIN 試験計画_DCHK_試験車");
            sql.AppendLine("    ON 試験計画_DCHK_対象車両.試験車_ID = 試験計画_DCHK_試験車.ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // 開催日ID
            if (val != null && val.開催日_ID > 0)
            {
                sql.AppendLine("    AND 試験計画_DCHK_対象車両.開催日_ID = :開催日_ID");

                prms.Add(new BindModel
                {
                    Name = ":開催日_ID",
                    Type = OracleDbType.Int32,
                    Object = val.開催日_ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 指摘ID
            if (val != null && val.指摘_ID > 0)
            {
                sql.AppendLine("    AND 試験計画_DCHK_状況.指摘_ID = :指摘_ID");

                prms.Add(new BindModel
                {
                    Name = ":指摘_ID",
                    Type = OracleDbType.Int32,
                    Object = val.指摘_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     試験計画_DCHK_試験車.管理票NO ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 設計チェック状況の作成（更新）
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(List<DesignCheckProgressPostInModel> list)
        {
            var progresss = this.GetCheckData(new DesignCheckProgressGetInModel { 開催日_ID = list.FirstOrDefault()?.開催日_ID });

            StringBuilder isql = new StringBuilder();

            isql.AppendLine("INSERT INTO");
            isql.AppendLine("試験計画_DCHK_状況 (");
            isql.AppendLine("     ID");
            isql.AppendLine("    ,対象車両_ID");
            isql.AppendLine("    ,指摘_ID");
            isql.AppendLine("    ,状況");
            isql.AppendLine("    ,完了確認日");
            isql.AppendLine("    ,部品納入日");
            isql.AppendLine(") VALUES (");
            isql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_DCHK_状況)");
            isql.AppendLine("    ,:対象車両_ID");
            isql.AppendLine("    ,:指摘_ID");
            isql.AppendLine("    ,:状況");
            isql.AppendLine("    ,:完了確認日");
            isql.AppendLine("    ,:部品納入日");
            isql.AppendLine(")");

            StringBuilder usql = new StringBuilder();

            usql.AppendLine("UPDATE");
            usql.AppendLine("    試験計画_DCHK_状況");
            usql.AppendLine("SET");
            usql.AppendLine("     状況 = :状況");
            usql.AppendLine("    ,完了確認日 = :完了確認日");
            usql.AppendLine("    ,部品納入日 = :部品納入日");
            usql.AppendLine("WHERE");
            usql.AppendLine("    対象車両_ID = :対象車両_ID");
            usql.AppendLine("    AND 指摘_ID = :指摘_ID");

            foreach (var val in list)
            {
                var exists = progresss?.Select(string.Format("開催日_ID = {0} AND 対象車両_ID = {1} AND 指摘_ID = {2}", val?.開催日_ID, val?.対象車両_ID, val?.指摘_ID))?.Any() == true;

                List<BindModel> prms = new List<BindModel>();

                // 対象車両ID：必須
                prms.Add(new BindModel
                {
                    Name = ":対象車両_ID",
                    Type = OracleDbType.Int32,
                    Object = val.対象車両_ID,
                    Direct = ParameterDirection.Input
                });

                // 指摘ID：必須
                prms.Add(new BindModel
                {
                    Name = ":指摘_ID",
                    Type = OracleDbType.Int32,
                    Object = val.指摘_ID,
                    Direct = ParameterDirection.Input
                });

                // 状況：必須
                prms.Add(new BindModel
                {
                    Name = ":状況",
                    Type = OracleDbType.Varchar2,
                    Object = val.状況,
                    Direct = ParameterDirection.Input
                });

                // 完了確認日：null許可
                prms.Add(new BindModel
                {
                    Name = ":完了確認日",
                    Type = OracleDbType.Date,
                    Object = getDateTime(val.完了確認日),
                    Direct = ParameterDirection.Input
                });

                // 部品納入日：null許可
                prms.Add(new BindModel
                {
                    Name = ":部品納入日",
                    Type = OracleDbType.Date,
                    Object = getDateTime(val.部品納入日),
                    Direct = ParameterDirection.Input
                });

                if (exists)
                { 
                    // 重複行がある場合は更新
                    if (!db.UpdateData(usql.ToString(), prms))
                        return false;
                }
                else
                {
                    // 重複行がない場合は追加
                    if (!db.InsertData(isql.ToString(), prms))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 設計チェック状況の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(List<DesignCheckProgressDeleteInModel> list)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_状況");
            sql.AppendLine("WHERE");
            sql.AppendLine("    対象車両_ID = :対象車両_ID");
            sql.AppendLine("    AND 指摘_ID = :指摘_ID");

            foreach (var val in list)
            {
                List<BindModel> prms = new List<BindModel>();

                // 対象車両ID：必須
                prms.Add(new BindModel
                {
                    Name = ":対象車両_ID",
                    Type = OracleDbType.Int32,
                    Object = val.対象車両_ID,
                    Direct = ParameterDirection.Input
                });

                // 指摘ID：必須
                prms.Add(new BindModel
                {
                    Name = ":指摘_ID",
                    Type = OracleDbType.Int32,
                    Object = val.指摘_ID,
                    Direct = ParameterDirection.Input
                });

                if (!db.DeleteData(sql.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 設計チェック状況の取得（チェック用：不正データ対応）
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetCheckData(DesignCheckProgressGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     試験計画_DCHK_状況.対象車両_ID");
            sql.AppendLine("    ,試験計画_DCHK_状況.指摘_ID");
            sql.AppendLine("    ,試験計画_DCHK_状況.状況");
            sql.AppendLine("    ,試験計画_DCHK_指摘リスト.開催日_ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_状況");
            sql.AppendLine("    INNER JOIN 試験計画_DCHK_指摘リスト");
            sql.AppendLine("    ON 試験計画_DCHK_状況.指摘_ID = 試験計画_DCHK_指摘リスト.ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

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

            sql.AppendLine("FOR UPDATE");

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}