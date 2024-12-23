using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 開発符号情報業務ロジッククラス
    /// </summary>
    public class GeneralCodeInfoLogic : TestCarBaseLogic
    {
        #region 取得
        /// <summary>
        /// 開発符号情報取得
        /// </summary>
        /// <returns></returns>
        public List<CommonMasterModel> GetMaster()
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("     \"開発符号\" AS CODE");
            sql.AppendLine("    ,\"開発符号\" AS NAME");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"試験車履歴情報\" A");
            sql.AppendLine("    INNER JOIN");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         A.\"データID\"");
            sql.AppendLine("                        ,MAX(A.\"履歴NO\") AS \"履歴NO\"");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"試験車履歴情報\" A");
            sql.AppendLine("                    WHERE 0 = 0");
            sql.AppendLine("                    GROUP BY");
            sql.AppendLine("                        A.\"データID\"");
            sql.AppendLine("                ) B");
            sql.AppendLine("    ON A.\"データID\" = B.\"データID\"");
            sql.AppendLine("    AND A.\"履歴NO\" = B.\"履歴NO\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"開発符号\" IS NOT NULL");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    \"NAME\"");

            return db.ReadModelList<CommonMasterModel>(sql.ToString(), null);

        }
        #endregion
    }
}