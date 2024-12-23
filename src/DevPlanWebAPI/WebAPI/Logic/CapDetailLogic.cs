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
    /// CAP評価車両詳細ロジッククラス
    /// </summary>
    public class CapDetailLogic : BaseLogic
    {
        #region CAP評価車両詳細
        /// <summary>
        /// CAP評価車両詳細
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<CapDetailModel> Get(CapDetailSearchModel cond)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.E_G型式");
            sql.AppendLine("    ,A.\"排気量\"");
            sql.AppendLine("    ,A.\"トランスミッション\"");
            sql.AppendLine("    ,A.\"駆動方式\"");
            sql.AppendLine("    ,A.\"グレード\"");
            sql.AppendLine("    ,A.\"車体番号\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験車履歴情報 A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"開発符号\" = :開発符号");
            sql.AppendLine("    AND A.\"号車\" = :号車");

            //パラメータ
            var prms = new List<BindModel>
            {
                new BindModel
                {
                    Name = ":開発符号",
                    Type = OracleDbType.Varchar2,
                    Object = cond.開発符号,
                    Direct = ParameterDirection.Input
                },
                new BindModel
                {
                    Name = ":号車",
                    Type = OracleDbType.Varchar2,
                    Object = cond.号車,
                    Direct = ParameterDirection.Input
                }
            };

            //取得
            return db.ReadModelList<CapDetailModel>(sql.ToString(), prms);

        }
        #endregion
    }
}