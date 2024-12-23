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
    /// 廃却期限リスト業務ロジッククラス
    /// </summary>
    public class DisposalPeriodLogic : TestCarBaseLogic
    {
        #region メンバ変数
        #endregion

        #region 検索
        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        public List<TestCarCommonModel> Get(TestCarCommonSearchModel cond)
        {
            var parm = new List<BindModel>
            {
                new BindModel { Name = ":管理票発行有無", Object = "済", Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
            };

            //SQL
            var sql = base.GetBaseTestCarSql(parm);

            //条件
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

            //管理所在地
            if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
            {
                sql.AppendLine("    AND DD.\"ESTABLISHMENT\" = :ESTABLISHMENT");
                parm.Add(new BindModel { Name = ":ESTABLISHMENT", Object = cond.ESTABLISHMENT, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });
            }

            //期間
            if (cond.START_DATE != null)
            {
                sql.AppendLine("    AND :START_DATE <= KR.\"使用期限\"");
                parm.Add(new BindModel
                {
                    Name = ":START_DATE",
                    Type = OracleDbType.Date,
                    Object = cond.START_DATE,
                    Direct = ParameterDirection.Input
                });
            }
            if (cond.END_DATE != null)
            {
                sql.AppendLine("    AND KR.\"使用期限\" <= :END_DATE");
                parm.Add(new BindModel
                {
                    Name = ":END_DATE",
                    Type = OracleDbType.Date,
                    Object = cond.END_DATE,
                    Direct = ParameterDirection.Input
                });
            }

            //申請状況
            if (cond.APPLICATION_STATUS != null)
            {
                if (cond.APPLICATION_STATUS == "1")
                {
                    //未申請
                    sql.AppendLine("    AND ((KR.\"種別\" = \'固定資産\' AND KK.\"車両搬出日\" IS NULL  AND KS.\"処分コード\" IS NULL)");
                    sql.AppendLine("        OR (KR.\"種別\" <> \'固定資産\' AND KK.\"研実管理廃却申請受理日\" IS NULL))");
                }
                else if (cond.APPLICATION_STATUS == "2")
                {
                    //申請済
                    sql.AppendLine("    AND ((KR.\"種別\" = \'固定資産\' AND ( KK.\"車両搬出日\" IS NOT NULL  OR KS.\"処分コード\" IS NOT NULL ))");
                    sql.AppendLine("        OR (KR.\"種別\" <> \'固定資産\' AND KK.\"研実管理廃却申請受理日\" IS NOT NULL))");
                }
            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     KR.\"種別\"");
            sql.AppendLine("    ,KK.\"管理票NO\"");

            //取得
            return db.ReadModelList<TestCarCommonModel>(sql.ToString(), parm);
        }
        #endregion
    }
}