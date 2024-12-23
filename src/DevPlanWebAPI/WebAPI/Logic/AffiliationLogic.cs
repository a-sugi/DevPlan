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
    /// 所属業務ロジッククラス
    /// </summary>
    public class AffiliationLogic : TestCarBaseLogic
    {
        #region 取得
        /// <summary>
        /// 所属取得
        /// </summary>
        /// <returns></returns>
        public List<CommonMasterModel> GetMaster()
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     'g' AS \"CODE\"");
            sql.AppendLine("    ,'群馬' AS \"NAME\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    DUAL");
            sql.AppendLine("UNION ALL");
            sql.AppendLine("SELECT");
            sql.AppendLine("     't' AS \"CODE\"");
            sql.AppendLine("    ,'東京' AS \"NAME\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    DUAL");
            sql.AppendLine("UNION ALL");
            sql.AppendLine("SELECT");
            sql.AppendLine("     NULL AS \"CODE\"");
            sql.AppendLine("    ,'全て' AS \"NAME\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    DUAL");

            return db.ReadModelList<CommonMasterModel>(sql.ToString(), null);

        }
        #endregion
    }
}