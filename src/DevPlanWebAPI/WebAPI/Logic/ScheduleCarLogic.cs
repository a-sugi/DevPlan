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
    public class ScheduleCarLogic : BaseLogic
    {
        #region スケジュール利用車データの取得
        /// <summary>
        /// スケジュール利用車データの取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<ScheduleCarModel> Get(ScheduleCarSearchModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    B.ID");
            sql.AppendLine("   ,A.CATEGORY_ID");
            sql.AppendLine("   ,C.GENERAL_CODE");
            sql.AppendLine("   ,A.管理票番号");
            sql.AppendLine("   ,B.利用可能部署_ID");
            sql.AppendLine("   ,B.部署単位");
            sql.AppendLine("   ,B.FLAG_要予約許可");
            sql.AppendLine("   ,B.FLAG_鍵別管理");
            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
            sql.AppendLine("   ,D.ID TESTCAR_ITEM_ID");
            sql.AppendLine("   ,D.GENERAL_CODE TESTCAR_ITEM_CODE");
            sql.AppendLine("   ,E.ID OUTERCAR_ITEM_ID");
            sql.AppendLine("   ,E.GENERAL_CODE OUTERCAR_ITEM_CODE");
            sql.AppendLine("   ,F.ID CARSHARING_ITEM_ID");
            sql.AppendLine("   ,F.GENERAL_CODE CARSHARING_ITEM_CODE");
            //Append End 2022/02/03 杉浦 試験車日程の車も登録する
            sql.AppendLine("FROM");
            sql.AppendLine("        SCHEDULE_CAR A");
            sql.AppendLine("    LEFT JOIN 試験計画_外製車日程_最終予約日 ON A.CATEGORY_ID = 試験計画_外製車日程_最終予約日.CATEGORY_ID");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        試験計画_外製車日程_車両リスト B");
            sql.AppendLine("    ON A.CATEGORY_ID = B.CATEGORY_ID");
            sql.AppendLine("    AND A.管理票番号 = B.管理票番号");
            sql.AppendLine("    LEFT JOIN (SELECT ID, GENERAL_CODE FROM TESTCAR_SCHEDULE_ITEM");
            sql.AppendLine("               UNION ALL");
            sql.AppendLine("               SELECT ID, GENERAL_CODE FROM OUTERCAR_SCHEDULE_ITEM");
            sql.AppendLine("               UNION ALL");
            sql.AppendLine("               SELECT ID, GENERAL_CODE FROM CARSHARING_SCHEDULE_ITEM) C");
            sql.AppendLine("    ON A.CATEGORY_ID = C.ID");
            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
            sql.AppendLine("    LEFT JOIN (SELECT ID, GENERAL_CODE FROM TESTCAR_SCHEDULE_ITEM) D");
            sql.AppendLine("    ON A.CATEGORY_ID = D.ID");
            sql.AppendLine("    LEFT JOIN (SELECT ID, GENERAL_CODE FROM OUTERCAR_SCHEDULE_ITEM) E");
            sql.AppendLine("    ON A.CATEGORY_ID = E.ID");
            sql.AppendLine("    LEFT JOIN (SELECT ID, GENERAL_CODE FROM CARSHARING_SCHEDULE_ITEM) F");
            sql.AppendLine("    ON A.CATEGORY_ID = F.ID");
            //Append End 2022/02/03 杉浦 試験車日程の車も登録する

            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine(" AND (試験計画_外製車日程_最終予約日.最終予約可能日 > TRUNC(SYSDATE) - 1 ");
            sql.AppendLine(" OR 試験計画_外製車日程_最終予約日.最終予約可能日 IS NULL) ");

            if (val.CATEGORY_ID != null)
            {
                sql.AppendLine("AND A.CATEGORY_ID = :CATEGORY_ID");
            }

            if (val.管理票番号 != null)
            {
                sql.AppendLine("AND A.管理票番号 = :管理票番号");
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    A.CATEGORY_ID");
            sql.AppendLine("   ,A.管理票番号");

            prms.Add(new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = val.CATEGORY_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":管理票番号", Type = OracleDbType.Varchar2, Object = val.管理票番号, Direct = ParameterDirection.Input });

            return db.ReadModelList<ScheduleCarModel>(sql.ToString(), prms);
        }
        #endregion
    }
}