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
    /// 管理票ラベル印刷業務ロジッククラス
    /// </summary>
    public class ControlSheetLabelPrintLogic : TestCarBaseLogic
    {
        /// <summary>
        /// 取得
        /// </summary>
        /// <returns>List</returns>
        public List<TestCarCommonModel> Get(TestCarCommonSearchModel val)
        {
            var parm = new List<BindModel>();

            //共通SQL
            var sql = base.GetBaseTestCarSql(parm);

            //条件（試験車基本情報）
            if (val?.データID > 0)
            {
                sql.AppendLine("    AND KK.\"データID\" = :データID");

                parm.Add(new BindModel { Name = ":データID", Object = val.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input });
            }

            SetStringWhere(sql, parm, "KK", "管理票NO", val?.管理票NO);

            if (val?.廃却フラグ == false)
            {
                //未廃却
                sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            }
            else
            {
                //全て
                sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            }

            //条件（試験車履歴情報）
            sql.AppendLine("    AND KR.\"発行年月日\" IS NOT NULL");

            if (!string.IsNullOrWhiteSpace(val?.管理票発行有無))
            {
                sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

                parm.Add(new BindModel { Name = ":管理票発行有無", Object = val.管理票発行有無, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });
            }

            if (val?.START_DATE != null)
            {
                sql.AppendLine("    AND KR.\"発行年月日\" >= :START_DATE");

                parm.Add(new BindModel { Name = ":START_DATE", Object = val.START_DATE, Type = OracleDbType.Date, Direct = ParameterDirection.Input });
            }

            if (val?.END_DATE != null)
            {
                sql.AppendLine("    AND KR.\"発行年月日\" <= :END_DATE");

                parm.Add(new BindModel { Name = ":END_DATE", Object = val.END_DATE, Type = OracleDbType.Date, Direct = ParameterDirection.Input });
            }

            SetStringWhere(sql, parm, "KR", "開発符号", val?.開発符号);

            SetStringWhere(sql, parm, "KR", "試作時期", val?.試作時期);

            SetStringWhere(sql, parm, "KR", "車体番号", val?.車体番号);

            SetStringWhere(sql, parm, "KR", "号車", val?.号車);

            SetStringWhere(sql, parm, "KR", "車体番号", val?.車体番号);

            SetStringWhere(sql, parm, "KR", "仕向地", val?.仕向地);

            SetStringWhere(sql, parm, "KR", "メーカー名", val?.メーカー名);

            SetStringWhere(sql, parm, "KR", "外製車名", val?.外製車名);

            //種別（固定資産／資産外／リース）
            if (val?.種別 != null)
            {
                sql.AppendLine("    AND KR.\"種別\" = :種別");

                parm.Add(new BindModel { Name = ":種別", Object = val.種別, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });
            }

            //条件（DEPARTMENT_DATA）
            if (!string.IsNullOrWhiteSpace(val?.ESTABLISHMENT))
            {
                sql.AppendLine("    AND DD.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                parm.Add(new BindModel { Name = ":ESTABLISHMENT", Object = val.ESTABLISHMENT, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });
            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    KR.\"発行年月日\" DESC");
            sql.AppendLine("    ,KK.\"管理票NO\" DESC");

            //取得
            return db.ReadModelList<TestCarCommonModel>(sql.ToString(), parm);
        }

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
                results.Add(base.UpdateTestCarBaseInfo(list, (x => new List<BindModel>
                {
                    new BindModel { Name = ":管理ラベル発行有無", Type = OracleDbType.Int32, Object = x.管理ラベル発行有無, Direct = ParameterDirection.Input },
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