using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>試験車注意喚起検索</remarks>
    public class TestCarReminderLogic : BaseLogic
    {
        #region 試験車注意喚起の取得
        /// <summary>
        /// 試験車注意喚起の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public List<TestCarReminderSearchOutModel> GetData(TestCarReminderSearchInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     A.GENERAL_CODE");
            sql.AppendLine("    ,A.ALERT_FLAG");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_試験車日程_注意喚起 A");
            sql.AppendLine("WHERE 0 = 0");

            //開発符号
            if (val.GENERAL_CODE != null)
            {
                sql.AppendLine("    AND A.GENERAL_CODE = :GENERAL_CODE");

                prms.Add(new BindModel { Name = "GENERAL_CODE", Type = OracleDbType.Varchar2, Object = val.GENERAL_CODE, Direct = ParameterDirection.Input });

            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    A.GENERAL_CODE");

            //取得
            return db.ReadModelList<TestCarReminderSearchOutModel>(sql.ToString(), prms);

        }
        #endregion

        #region 試験車注意喚起の更新
        /// <summary>
        /// 試験車注意喚起の更新
        /// </summary>
        /// <param name="list">注意喚起</param>
        /// <returns></returns>
        public bool PutData(List<TestCarReminderSearchOutModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //試験計画_試験車日程_注意喚起を登録
            results.Add(this.MergeTyuuiKanki(list));

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
        /// 試験計画_試験車日程_注意喚起を登録
        /// </summary>
        /// <param name="list">注意喚起</param>
        /// <returns>更新可否</returns>
        private bool MergeTyuuiKanki(List<TestCarReminderSearchOutModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"試験計画_試験車日程_注意喚起\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             :GENERAL_CODE AS \"GENERAL_CODE\"");
            sql.AppendLine("            ,:ALERT_FLAG AS \"ALERT_FLAG\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            DUAL");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"GENERAL_CODE\" = B.\"GENERAL_CODE\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("        \"ALERT_FLAG\" = B.\"ALERT_FLAG\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"GENERAL_CODE\"");
            sql.AppendLine("        ,\"ALERT_FLAG\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"GENERAL_CODE\"");
            sql.AppendLine("        ,B.\"ALERT_FLAG\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var tyuui in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = tyuui.GENERAL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ALERT_FLAG", Type = OracleDbType.Byte, Object = tyuui.ALERT_FLAG, Direct = ParameterDirection.Input }

                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }

        #endregion

    }
}