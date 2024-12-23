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
    /// 開発符号情報(製作車)業務ロジッククラス
    /// </summary>
    public class ProductionCarGeneralCodeInfoLogic : TestCarBaseLogic
    {
        /// <summary>
        /// 開発符号情報(製作車)検索
        /// </summary>
        /// <returns></returns>
        public List<CommonMasterModel> GetMaster()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("     GENERAL_CODE AS CODE");
            sql.AppendLine("    ,GENERAL_CODE AS NAME");
            sql.AppendLine("FROM");
            sql.AppendLine("     PRODUCTION_CAR");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    NAME");

            return db.ReadModelList<CommonMasterModel>(sql.ToString(), null);
        }
    }
}