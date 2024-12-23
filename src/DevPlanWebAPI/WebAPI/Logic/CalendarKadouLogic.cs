using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DevPlanWebAPI.Base;
using System.Text;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// カレンダー稼働日非稼働日連携ロジック。
    /// </summary>
    public class CalendarKadouLogic : BaseLogic
    {
        /// <summary>
        /// カレンダー稼働日非稼働日取得。
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public IEnumerable<CalendarKadouModel> Get(CalendarKadouSearchModel cond)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("  CALENDAR_DATE");
            sql.AppendLine("  , KADOBI_KBN ");
            sql.AppendLine("FROM");
            sql.AppendLine("  RHAC1860 ");
            sql.AppendLine("WHERE");
            sql.AppendLine("  CALENDAR_DATE >= :FIRST_DATE ");
            sql.AppendLine("  AND CALENDAR_DATE <= :LAST_DATE");

            var prms = new List<BindModel>
            {
                new BindModel
                {
                    Name = ":FIRST_DATE",
                    Type = OracleDbType.Decimal,
                    Object = cond.FIRST_DATE.ToString("yyyyMMdd"),
                    Direct = ParameterDirection.Input
                },
                new BindModel
                {
                    Name = ":LAST_DATE",
                    Type = OracleDbType.Decimal,
                    Object = cond.LAST_DATE.ToString("yyyyMMdd"),
                    Direct = ParameterDirection.Input
                }
            };

            return db.ReadModelList<CalendarKadouModel>(sql.ToString(), prms);
        }
    }
}