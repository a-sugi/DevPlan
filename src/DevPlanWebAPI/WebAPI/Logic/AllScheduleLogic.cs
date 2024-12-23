using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>試験車日程・カーシェア日程・外製車日程</remarks>
    public class AllScheduleLogic : BaseLogic
    {
        #region スケジュール利用車データの取得
        /// <summary>
        /// スケジュール利用車データの取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<AllScheduleModel> Get(AllScheduleSearchModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("      B.* ");
            sql.AppendLine("FROM");
            sql.AppendLine("    SCHEDULE_CAR A ");
            sql.AppendLine("    INNER JOIN (SELECT");
            sql.AppendLine("      * ");
            sql.AppendLine("FROM");
            sql.AppendLine("    ( ");
            sql.AppendLine("        SELECT");
            sql.AppendLine("              '1' \"SCHEDULE_TYPE\"");
            sql.AppendLine("            , A.\"ID\"");
            sql.AppendLine("            , A.\"試験車日程種別\"");
            sql.AppendLine("            , A.\"予約種別\"");
            sql.AppendLine("            , A.\"GENERAL_CODE\"");
            sql.AppendLine("            , A.\"START_DATE\"");
            sql.AppendLine("            , A.\"END_DATE\"");
            sql.AppendLine("            , A.\"CATEGORY_ID\"");
            sql.AppendLine("            , A.\"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"TESTCAR_SCHEDULE\" A ");
            sql.AppendLine("        UNION ");
            sql.AppendLine("        SELECT");
            sql.AppendLine("              '2' \"SCHEDULE_TYPE\"");
            sql.AppendLine("            , A.ID");
            sql.AppendLine("            , A.\"試験車日程種別\"");
            sql.AppendLine("            , A.\"予約種別\"");
            sql.AppendLine("            , A.GENERAL_CODE");
            sql.AppendLine("            , A.START_DATE");
            sql.AppendLine("            , A.END_DATE");
            sql.AppendLine("            , A.CATEGORY_ID");
            sql.AppendLine("            , A.PARALLEL_INDEX_GROUP ");
            sql.AppendLine("        FROM");
            sql.AppendLine("            OUTERCAR_SCHEDULE A ");
            sql.AppendLine("        UNION ");
            sql.AppendLine("        SELECT");
            sql.AppendLine("              '3' \"SCHEDULE_TYPE\"");
            sql.AppendLine("            , A.\"ID\"");
            sql.AppendLine("            , A.\"試験車日程種別\"");
            sql.AppendLine("            , A.\"予約種別\"");
            sql.AppendLine("            , A.\"GENERAL_CODE\"");
            sql.AppendLine("            , A.\"START_DATE\"");
            sql.AppendLine("            , A.\"END_DATE\"");
            sql.AppendLine("            , A.\"CATEGORY_ID\"");
            sql.AppendLine("            , A.\"PARALLEL_INDEX_GROUP\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"CARSHARING_SCHEDULE\" A");
            sql.AppendLine("    ) ");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    \"SCHEDULE_TYPE\"");
            sql.AppendLine("    ,\"CATEGORY_ID\"");
            sql.AppendLine("    ,\"ID\")B");
            sql.AppendLine("ON A.CATEGORY_ID = B.CATEGORY_ID");
            sql.AppendLine("WHERE 0=0");

            if (val.管理票番号 != null)
            {
                sql.AppendLine("AND A.管理票番号 = :管理票番号");
                prms.Add(new BindModel { Name = ":管理票番号", Type = OracleDbType.Varchar2, Object = val.管理票番号, Direct = ParameterDirection.Input });
            }

            var startDate = val.START_DATE == null ? null : (DateTime?)val.START_DATE.Value.Date;
            var endDate = val.END_DATE == null ? null : (DateTime?)val.END_DATE.Value.Date;
            //期間がすべて入力されているかどうか
            if (val.START_DATE != null && val.END_DATE != null)
            {
                sql.AppendLine("    AND");
                sql.AppendLine("        (");
                sql.AppendLine("            :START_DATE BETWEEN B.\"START_DATE\" AND B.\"END_DATE\"");
                sql.AppendLine("            OR");
                sql.AppendLine("            :END_DATE BETWEEN B.\"START_DATE\" AND B.\"END_DATE\"");
                sql.AppendLine("            OR");
                sql.AppendLine("            B.\"START_DATE\" BETWEEN :START_DATE AND (:END_DATE + 1)");
                sql.AppendLine("            OR");
                sql.AppendLine("            B.\"END_DATE\" BETWEEN :START_DATE AND (:END_DATE + 1)");
                sql.AppendLine("        )");

                prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

            }
            else
            {
                //期間(From)
                if (val.START_DATE != null)
                {
                    sql.AppendLine("    AND");
                    sql.AppendLine("        (");
                    sql.AppendLine("            :START_DATE BETWEEN A.\"START_DATE\" AND B.\"END_DATE\"");
                    sql.AppendLine("            OR");
                    sql.AppendLine("            B.\"START_DATE\" >= :START_DATE");
                    sql.AppendLine("        )");

                    prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input });

                }

                //期間(To)
                if (val.END_DATE != null)
                {
                    sql.AppendLine("    AND");
                    sql.AppendLine("        (");
                    sql.AppendLine("            :END_DATE BETWEEN A.\"START_DATE\" AND B.\"END_DATE\"");
                    sql.AppendLine("            OR");
                    sql.AppendLine("            B.\"END_DATE\" < (:END_DATE + 1)");
                    sql.AppendLine("        )");

                    prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

                }

            }

            return db.ReadModelList<AllScheduleModel>(sql.ToString(), prms);
        }
        #endregion
    }
}