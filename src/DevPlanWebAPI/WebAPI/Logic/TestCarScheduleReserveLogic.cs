using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 試験車スケジュール一括本予約ロジッククラス
    /// </summary>
    public class TestCarScheduleReserveLogic : BaseLogic
    {
        #region 試験車スケジュール一括本予約
        /// <summary>
        /// 試験車スケジュール一括本予約
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns></returns>
        public bool UpdateTestCarScheduleItem(TestCarScheduleReserveModel val)
        {
            //パラメータ
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = val.GENERAL_CODE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":TARGETSTARTDATE", Type = OracleDbType.Date, Object = val.TARGET_START_DATE, Direct = ParameterDirection.Input }

            };

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("        \"TESTCAR_SCHEDULE\"");
            sql.AppendLine("    SET");
            sql.AppendLine("        \"予約種別\" = '本予約',");
            sql.AppendLine("        \"CHANGE_DATETIME\" = SYSTIMESTAMP");
            sql.AppendLine("    WHERE");
            sql.AppendLine("        \"TESTCAR_SCHEDULE\".\"ID\" IN");
            sql.AppendLine("        (");
            sql.AppendLine("            SELECT");
            sql.AppendLine("                    \"TESTCAR_SCHEDULE\".\"ID\"");
            sql.AppendLine("                FROM");
            sql.AppendLine("                    \"TESTCAR_SCHEDULE\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_試験車日程_設定者\"");
            sql.AppendLine("                    ON \"TESTCAR_SCHEDULE\".\"ID\" = \"試験計画_試験車日程_設定者\".\"SCHEDULE_ID\"");
            sql.AppendLine("                WHERE 0 = 0");
            sql.AppendLine("                    AND \"予約種別\" = '仮予約'");
            sql.AppendLine("                    AND \"試験車日程種別\" = '決定'");
            sql.AppendLine("                    AND \"GENERAL_CODE\" = :GENERAL_CODE");
            sql.AppendLine("                    AND :TARGETSTARTDATE <= TO_TIMESTAMP(TRUNC(END_DATE))");

            if (val.TARGET_END_DATE != null)
            {
                sql.AppendLine("                    AND TO_TIMESTAMP(TRUNC(END_DATE)) <= :TARGETENDDATE");

                prms.Add(new BindModel { Name = ":TARGETENDDATE", Type = OracleDbType.Date, Object = val.TARGET_END_DATE, Direct = ParameterDirection.Input });

            }


            if (string.IsNullOrWhiteSpace(val.INPUT_PERSONEL_ID) == false)
            {
                sql.AppendLine("                    AND \"試験計画_試験車日程_設定者\".\"予約者_ID\" = :PERSONELID");

                prms.Add(new BindModel { Name = ":PERSONELID", Type = OracleDbType.Varchar2, Object = val.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input });

            }
                        
            sql.AppendLine("        )");

            //トランザクション開始
            db.Begin();

            //登録が成功したかどうか
            var flg = db.UpdateData(sql.ToString(), prms);
            if (flg == true)
            {
                //コミット
                db.Commit();
            }
            else
            {
                //ロールバック
                db.Rollback();
            }

            return flg;

        }
        #endregion
    }
}