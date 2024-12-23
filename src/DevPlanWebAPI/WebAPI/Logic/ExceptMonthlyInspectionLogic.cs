using System.Data;
using System.Text;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using System.Linq;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>担当検索</remarks>
    public class ExceptMonthlyInspectionLogic : BaseLogic
    {
        /// <summary>
        /// 担当データの取得
        /// </summary>
        /// <returns></returns>
        public List<ExceptMonthlyInspectionOutModels> GetData()
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     NVL(C.FLAG_月例点検,0) FLAG_月例点検");
            sql.AppendLine("    ,A.COMPANY_NAME");
            sql.AppendLine("    ,A.DEPARTMENT_NAME");
            sql.AppendLine("    ,A.DEPARTMENT_CODE");
            sql.AppendLine("    ,A.DEPARTMENT_ID");
            sql.AppendLine("    ,A.ESTABLISHMENT");
            sql.AppendLine("    ,A.SORT_NO AS DEPARTMENT_SORT_NO");
            sql.AppendLine("    ,B.SECTION_NAME");
            sql.AppendLine("    ,B.SECTION_CODE");
            sql.AppendLine("    ,B.SECTION_ID");
            sql.AppendLine("    ,B.SORT_NO AS SECTION_SORT_NO");
            sql.AppendLine("FROM");
            sql.AppendLine("    DEPARTMENT_DATA A");
            sql.AppendLine("    INNER JOIN SECTION_DATA B");
            sql.AppendLine("    ON A.DEPARTMENT_ID = B.DEPARTMENT_ID");
            sql.AppendLine("    LEFT JOIN EXCEPT_MONTHLY_INSPECTION C");
            sql.AppendLine("    ON B.SECTION_ID = C.SECTION_GROUP_ID");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.FLAG_EXIST = 1");
            sql.AppendLine("    AND B.FLAG_EXIST = 1");

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.SORT_NO");
            sql.AppendLine("    ,B.SORT_NO");

            return db.ReadModelList<ExceptMonthlyInspectionOutModels>(sql.ToString(), null);
        }

        public bool PostData(List<ExceptMonthlyInspectionUpdateModels> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //DEVELOPMENT_SCHEDULEを追加
                results.Add(this.MergeExceptMonthlyInspection(list));
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

        private bool MergeExceptMonthlyInspection(List<ExceptMonthlyInspectionUpdateModels> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE ");
            sql.AppendLine("INTO EXCEPT_MONTHLY_INSPECTION A ");
            sql.AppendLine("    USING ( ");
            sql.AppendLine("        SELECT");
            sql.AppendLine("              :SECTION_GROUP_ID AS SECTION_GROUP_ID");
            sql.AppendLine("            , :FLAG_月例点検    AS FLAG_月例点検");
            sql.AppendLine("            , SYSDATE    AS INPUT_DATETIME ");
            sql.AppendLine("        FROM");
            sql.AppendLine("            DUAL");
            sql.AppendLine("    ) B ");
            sql.AppendLine("	ON ( 0 = 0 AND A.SECTION_GROUP_ID = B.SECTION_GROUP_ID)");
            sql.AppendLine("WHEN MATCHED THEN UPDATE ");
            sql.AppendLine("SET");
            sql.AppendLine("    FLAG_月例点検 = B.FLAG_月例点検");
            sql.AppendLine("    , INPUT_DATETIME = B.INPUT_DATETIME");
            sql.AppendLine(" WHEN NOT MATCHED THEN ");
            sql.AppendLine("INSERT (SECTION_GROUP_ID, FLAG_月例点検, INPUT_DATETIME) ");
            sql.AppendLine("VALUES ( ");
            sql.AppendLine("    B.SECTION_GROUP_ID");
            sql.AppendLine("    , B.FLAG_月例点検");
            sql.AppendLine("    , B.INPUT_DATETIME");
            sql.AppendLine(") ");

            var results = new List<bool>();

            foreach (var item in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = item.SECTION_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_月例点検", Type = OracleDbType.Int64, Object = item.FLAG_月例点検, Direct = ParameterDirection.Input }

                };

                //登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);
        }

    }
}