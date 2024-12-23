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
    public class DisposaDisposalPeriodLogic : TestCarBaseLogic
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
            sql.AppendLine("    AND ((KR.\"種別\" = \'固定資産\' AND KK.\"研実管理廃却申請受理日\" IS NULL)");
            sql.AppendLine("        OR (KR.\"種別\" <> \'固定資産\' AND KK.\"車両搬出日\" IS NULL))");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = @管理票発行有無");
            sql.AppendLine("    AND DD.\"ESTABLISHMENT\" = @ESTABLISHMENT");

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     KR.\"種別\"");
            sql.AppendLine("    ,KK.\"管理票NO\"");
            sql.AppendLine("    ,SD.\"SECTION_CODE\"");
            sql.AppendLine("    ,SG.\"SECTION_GROUP_CODE\"");

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
            ///不要？

            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //試験車履歴情報を更新
                results.Add(base.UpdateTestCarHistoryInfo(list, (x => new List<BindModel>
                {
                    new BindModel { Name = ":開発符号", Type = OracleDbType.Varchar2, Object = x.開発符号, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試作時期", Type = OracleDbType.Varchar2, Object = x.試作時期, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":号車", Type = OracleDbType.Varchar2, Object = x.号車, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":仕向地", Type = OracleDbType.Varchar2, Object = x.仕向地, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":外製車名", Type = OracleDbType.Varchar2, Object = x.外製車名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":車体番号", Type = OracleDbType.Varchar2, Object = x.車体番号, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":登録ナンバー", Type = OracleDbType.Varchar2, Object = x.登録ナンバー, Direct = ParameterDirection.Input },

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