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

using System.Configuration;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 廃却車両搬出日業務ロジッククラス
    /// </summary>
    public class DisposalCarryoutLogic : TestCarBaseLogic
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
            sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NOT NULL");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

            //管理所在地
            if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
            {
                sql.AppendLine("    AND DD.\"ESTABLISHMENT\" = :ESTABLISHMENT");
                parm.Add(new BindModel { Name = ":ESTABLISHMENT", Object = cond.ESTABLISHMENT, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });
            }

            //期間
            if (cond.START_DATE == null && cond.END_DATE == null)
            {
                sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            }
            else
            {
                if (cond.START_DATE != null)
                {
                    sql.AppendLine("    AND :START_DATE <= KK.\"車両搬出日\"");
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
                    sql.AppendLine("    AND KK.\"車両搬出日\" <= :END_DATE");
                    parm.Add(new BindModel
                    {
                        Name = ":END_DATE",
                        Type = OracleDbType.Date,
                        Object = cond.END_DATE,
                        Direct = ParameterDirection.Input
                    });
                }
            }

            //管理票NO
            SetStringWhere(sql, parm, "KK", "管理票NO", cond?.管理票NO);

            //車体番号
            SetStringWhere(sql, parm, "KR", "車体番号", cond?.車体番号);

            //固定資産NO
            SetStringWhere(sql, parm, "KR", "固定資産NO", cond?.固定資産NO);

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     KK.\"管理票NO\"");

            //取得
            return db.ReadModelList<TestCarCommonModel>(sql.ToString(), parm);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="list">更新項目</param>
        /// <returns>更新可否</returns>
        public bool Put(IEnumerable<TestCarCommonModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //試験車基本情報を更新
                results.Add(base.UpdateTestCarBaseInfo(list, (x => 
                {
                    var bindList = new List<BindModel>
                    {
                        new BindModel { Name = ":廃却見積日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(x.廃却見積日), Direct = ParameterDirection.Input },
                        new BindModel { Name = ":廃却見積額", Type = OracleDbType.Int32, Object = x.廃却見積額, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":車両搬出日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(x.車両搬出日), Direct = ParameterDirection.Input },
                    };

                    if (x.車両搬出日 != null)
                    {
                        bindList.Add(new BindModel { Name = ":駐車場番号", Type = OracleDbType.Date, Object = null, Direct = ParameterDirection.Input });
                    }

                    return bindList;
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