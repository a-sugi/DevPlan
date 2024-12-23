using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 駐車カウントロジッククラス
    /// </summary>
    public class ParkingCountLogic : BaseLogic
    {
        #region 駐車カウント取得
        /// <summary>
        /// 駐車カウント取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<ParkingCountModel> Get(ParkingCountSearchModel cond)
        {
            //SQL
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    駐車場番号");
            sql.AppendLine("   ,COUNT(*) COUNT");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験車基本情報");
            sql.AppendLine("WHERE 0 = 0");

            //パラメータ
            var prms = new List<BindModel>();

            if (!string.IsNullOrWhiteSpace(cond.駐車場番号))
            {
                sql.AppendLine("    AND 駐車場番号 = :駐車場番号 ");
                prms.Add(new BindModel
                {
                    Name = ":駐車場番号",
                    Type = OracleDbType.Varchar2,
                    Object = cond.駐車場番号,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("GROUP BY ");
            sql.AppendLine("    駐車場番号");

            sql.AppendLine("ORDER BY ");
            sql.AppendLine("    駐車場番号");

            //取得
            return db.ReadModelList<ParkingCountModel>(sql.ToString(), prms);
        }
        #endregion
    }
}