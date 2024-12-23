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
    /// エンジン型式情報業務ロジッククラス
    /// </summary>
    public class EngineModelInfoLogic : TestCarBaseLogic
    {
        #region 取得
        /// <summary>
        /// エンジン型式情報取得
        /// </summary>
        /// <returns></returns>
        public List<CommonMasterModel> GetMaster()
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    \"E_G型式\" AS NAME");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"E_G型式情報\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    \"NAME\"");

            return db.ReadModelList<CommonMasterModel>(sql.ToString(), null);

        }
        #endregion
    }
}