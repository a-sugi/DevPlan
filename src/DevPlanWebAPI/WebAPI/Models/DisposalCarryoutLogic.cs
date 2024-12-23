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
    /// 廃却車両搬出日業務ロジッククラス
    /// </summary>
    public class DisposalCarryoutLogic : TestCarBaseLogic
    {
        #region メンバ変数
        #endregion

        #region 取得
        public List<TestCarCommonModel> GetData()
        {
            var parm = new List<BindModel>
            {
                new BindModel { Name = "管理票発行有無", Object = "済", Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

            };

            //SQL
            var sql = base.GetBaseTestCarSql(parm);

            //条件
            sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NOT NULL");
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = @管理票発行有無");
            sql.AppendLine("    AND DD.\"ESTABLISHMENT\" = @ESTABLISHMENT");

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     KK.\"管理票NO\"");

            //取得
            return db.ReadModelList<TestCarCommonModel>(sql.ToString(), parm);

        }
        #endregion

        #region 更新
        /// <summary>
        /// 試験車の更新
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        public bool UpdateData(IEnumerable<TestCarCommonModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //試験車基本情報を更新
                results.Add(base.UpdateTestCarBaseInfo(list, (x => new List<BindModel>
                {
                    new BindModel { Name = ":廃却見積日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(x.廃却見積日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":廃却見積額", Type = OracleDbType.Int32, Object = x.廃却見積額, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":車両搬出日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(x.車両搬出日), Direct = ParameterDirection.Input },

                })));

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
        #endregion
    }
}