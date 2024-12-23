using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Base
{
    /// <summary>
    /// 基底業務ロジッククラス
    /// </summary>
    public class BaseLogic
    {
        #region メンバ変数
        /// <summary>DB接続</summary>
        protected DBAccess db;

        /// <summary>IN句の上限</summary>
        protected const int InMax = 1000;

        private Dictionary<Type, BaseLogic> logicMap = new Dictionary<Type, BaseLogic>();
        #endregion

        #region パブリック
        /// <summary>
        /// DB接続設定
        /// </summary>
        /// <param name="db">DB接続</param>
        public void SetDBAccess(DBAccess db)
        {
            this.db = db;

        }

        /// <summary>
        /// 業務ロジッククラス取得
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <returns></returns>
        public T GetLogic<T>() where T : BaseLogic, new()
        {
            var type = typeof(T);

            //未取得なら取得
            if (this.logicMap.ContainsKey(type) == false)
            {
                var logic = new T();
                logic.SetDBAccess(this.db);

                this.logicMap[type] = logic;

            }

            return (T)this.logicMap[type];

        }
        #endregion

        #region DML共通
        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <param name="add">追加対象</param>
        /// <returns></returns>
        protected bool Insert(string tableName, IEnumerable<BindModel> add)
        {
            var prms = new List<BindModel>();

            var column = new StringBuilder();
            var value = new StringBuilder();

            //列の設定
            foreach (var b in add)
            {
                var name = b.Name.Replace(":", "");
                var join = (prms.Any() == false ? " " : ",");

                column.AppendFormat("    {0}\"{1}\"", join, name).AppendLine();
                value.AppendFormat("    {0}:{1}", join, name).AppendLine();

                prms.Add(b);

            }

            //SQL
            var sql = new StringBuilder();
            sql.AppendFormat("INSERT INTO \"{0}\"", tableName).AppendLine();
            sql.AppendLine("(");
            sql.Append(column.ToString());
            sql.AppendLine(")");
            sql.AppendLine("VALUES");
            sql.AppendLine("(");
            sql.Append(value.ToString());
            sql.AppendLine(")");

            return db.InsertData(sql.ToString(), prms);

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <param name="update">更新対象</param>
        /// <param name="where">更新条件</param>
        /// <returns></returns>
        protected bool Update(string tableName, IEnumerable<BindModel> update, IEnumerable<BindModel> where)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendFormat("    \"{0}\"", tableName).AppendLine();

            //更新対象の列
            sql.AppendLine("SET");
            foreach (var b in update)
            {
                sql.AppendFormat("    {0}\"{1}\" = :{1}", (prms.Any() == false ? " " : ","), b.Name.Replace(":", "")).AppendLine();

                prms.Add(b);

            }

            //更新条件があれば設定
            sql.AppendLine("WHERE 0 = 0");
            if (where != null && where.Any() == true)
            {
                foreach (var b in where)
                {
                    sql.AppendFormat("    AND \"{0}\" = :{0}", b.Name.Replace(":", "")).AppendLine();

                    prms.Add(b);

                }

            }

            return db.UpdateData(sql.ToString(), prms);

        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <param name="update">更新対象</param>
        /// <param name="where">更新条件</param>
        /// <returns></returns>
        protected bool Merge(string tableName, IEnumerable<BindModel> update, IEnumerable<BindModel> where)
        {
            var prms = update.Concat(where).ToList();

            var i = 0;

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendFormat("    \"{0}\" A", tableName).AppendLine();
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             0");

            foreach (var b in prms)
            {
                sql.AppendFormat("            ,:{0} AS \"{0}\"", b.Name.Replace(":", "")).AppendLine();

            }

            sql.AppendLine("        FROM");
            sql.AppendLine("            \"DUAL\"");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");

            //条件
            foreach (var b in where)
            {
                sql.AppendFormat("        AND A.\"{0}\" = B.\"{0}\"", b.Name.Replace(":", "")).AppendLine();

            }

            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");

            //更新対象の列
            i = 0;
            foreach (var b in update)
            {
                sql.AppendFormat("        {0}\"{1}\" = B.\"{1}\"", (i++ == 0 ? " " : ","), b.Name.Replace(":", "")).AppendLine();

            }

            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");

            //追加対象の列
            i = 0;
            foreach (var b in prms)
            {
                sql.AppendFormat("        {0}\"{1}\"", (i++ == 0 ? " " : ","), b.Name.Replace(":", "")).AppendLine();

            }

            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");

            //追加対象の値
            i = 0;
            foreach (var b in prms)
            {
                sql.AppendFormat("        {0}B.\"{1}\"", (i++ == 0 ? " " : ","), b.Name.Replace(":", "")).AppendLine();

            }

            sql.AppendLine("    )");

            return db.UpdateData(sql.ToString(), prms);

        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <param name="where">削除条件</param>
        /// <returns></returns>
        protected bool Delete(string tableName, IEnumerable<BindModel> where)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendFormat("    \"{0}\"", tableName).AppendLine();

            //削除条件があれば設定
            sql.AppendLine("WHERE 0 = 0");
            if (where != null && where.Any() == true)
            {
                foreach (var b in where)
                {
                    sql.AppendFormat("    AND \"{0}\" = :{0}", b.Name.Replace(":", "")).AppendLine();

                    prms.Add(b);

                }

            }

            return db.DeleteData(sql.ToString(), prms);

        }
        #endregion
    }
}