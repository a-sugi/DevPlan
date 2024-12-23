using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;


using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;


namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 作業履歴(カーシェア)ビジネスロジック
    /// </summary>
    public class CarShareWorkHistoryLogic : BaseLogic
    {
        #region メンバ変数
        private WorkHistoryLogic workHistory = null;
        #endregion

        #region 作業履歴の更新
        /// <summary>
        /// 作業履歴の更新
        /// </summary>
        /// <param name="list">作業履歴</param>
        /// <returns></returns>
        public bool UpdateWorkHistory(List<WorkHistoryModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //作業履歴の登録
                results.Add(this.GetWorkHistoryLogic().MergeWorkHistory(list));

                //DEVELOPMENT_SCHEDULEを更新
                results.Add(this.UpdateScheduleItem(list));

            }

            //登録が成功したかどうか
            var flg = results.All(x => x == true);
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

        /// <summary>
        /// DEVELOPMENT_SCHEDULEを更新
        /// </summary>
        /// <param name="list">作業履歴</param>
        /// <returns></returns>
        private bool UpdateScheduleItem(List<WorkHistoryModel> list)
        {
            var now = (DateTime?)DateTime.Now;

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"CARSHARING_SCHEDULE_ITEM\"");
            sql.AppendLine("SET");
            sql.AppendLine("     \"INPUT_PERSONEL_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,\"INPUT_LOGIN_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,\"CHANGE_DATETIME\" = SYSTIMESTAMP");
            sql.AppendLine("    ,\"CLOSED_DATE\" = :CLOSED_DATE");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :ID");

            var results = new List<bool>();

            //IDごとに更新
            foreach (var id in list.Where(x => x.CATEGORY_ID != null).Select(x => x.CATEGORY_ID.Value).Distinct())
            {
                var workHistory = list.First(x => x.CATEGORY_ID == id);

                var closedDate = workHistory.OPEN_CLOSE == Const.Close ? now : null;

                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = workHistory.CATEGORY_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = workHistory.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CLOSED_DATE", Type = OracleDbType.Date, Object = closedDate, Direct = ParameterDirection.Input }
                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 業務ロジックの取得
        /// <summary>
        /// 作業履歴ロジックの取得
        /// </summary>
        /// <returns></returns>
        private WorkHistoryLogic GetWorkHistoryLogic()
        {
            //未取得なら取得
            if (this.workHistory == null)
            {
                this.workHistory = new WorkHistoryLogic();
                workHistory.SetDBAccess(base.db);

            }

            return this.workHistory;

        }
        #endregion

    }
}