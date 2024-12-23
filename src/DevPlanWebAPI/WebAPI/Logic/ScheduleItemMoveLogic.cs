using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 項目移動ロジッククラス
    /// </summary>
    /// <remarks>項目データの操作</remarks>
    public class ScheduleItemMoveLogic : BaseLogic
    {
        /// <summary>
        /// 項目（関連スケジュール含む）移動処理
        /// </summary>
        /// <param name="model">項目コピー・移動（項目移動）モデルクラス（入力）</param>
        /// <returns>更新可否</returns>
        public bool ScheculeItemMove(ScheduleItemMoveInModel model)
        {
            var results = new List<bool>();

            if (model != null)
            {
                db.Begin();

                string itemTableName = "";
                if (model.SCHEDULE_ITEM_TYPE == ScheduleItemType.CarShare)
                {
                    itemTableName = "CARSHARING_SCHEDULE";
                }
                else if (model.SCHEDULE_ITEM_TYPE == ScheduleItemType.OuterCar)
                {
                    itemTableName = "OUTERCAR_SCHEDULE";
                }
                else if (model.SCHEDULE_ITEM_TYPE == ScheduleItemType.TestCar)
                {
                    itemTableName = "TESTCAR_SCHEDULE";
                }
                else
                {
                    return false;
                }

                var prms = new List<BindModel>();
                prms.Add(new BindModel { Name = ":NEW_GENERAL_CODE", Type = OracleDbType.Varchar2, Object = model.GENERAL_CODE, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Varchar2, Object = model.ID, Direct = ParameterDirection.Input });

                var sql = new StringBuilder();
                sql.AppendLine(@"UPDATE");
                sql.AppendLine(itemTableName);
                sql.AppendLine(@"SET GENERAL_CODE = :NEW_GENERAL_CODE");
                sql.AppendLine(@"WHERE CATEGORY_ID = :ID");

                results.Add(db.UpdateData(sql.ToString(), prms));

                sql.Clear();
                sql.AppendLine(@"SELECT");
                sql.AppendLine(@"MAX(SORT_NO) MAX_NO ");
                sql.AppendLine(@"FROM ");
                sql.AppendLine(itemTableName + "_ITEM");
                sql.AppendLine("WHERE");
                sql.AppendLine("GENERAL_CODE = :NEW_GENERAL_CODE");

                var dt = db.ReadDataTable(sql.ToString(), prms);
                int startSortCount = int.Parse(dt.Rows[0][0].ToString()) + 1;

                sql.Clear();
                sql.AppendLine("UPDATE");
                sql.AppendLine(itemTableName + "_ITEM");
                sql.AppendLine("SET");
                sql.AppendLine(@"SORT_NO = :START_SORT_NO");
                sql.AppendLine(@",GENERAL_CODE = :NEW_GENERAL_CODE");
                sql.AppendLine(@"WHERE ID = :ID");

                prms.Add(new BindModel { Name = ":START_SORT_NO", Type = OracleDbType.Int32, Object = startSortCount, Direct = ParameterDirection.Input });

                results.Add(db.UpdateData(sql.ToString(), prms));
            }

            var flg = results.All(x => x == true);
            if (flg)
            {
                db.Commit();
            }
            else
            {
                db.Rollback();
            }

            return flg;
        }
    }
}