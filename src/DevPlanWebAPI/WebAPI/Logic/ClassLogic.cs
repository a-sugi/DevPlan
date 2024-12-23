using System.Data;
using System.Text;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>種別検索</remarks>
    public class ClassLogic : BaseLogic
    {
        /// <summary>
        /// 種別データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public List<ClassSearchOutModel> GetData(ClassSearchInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,種別");
            sql.AppendLine("    ,SORT_NO");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_試験車履歴_種別");
            sql.AppendLine("WHERE 0 = 0");
            if (val.ID != null)
            {
                sql.AppendLine("    AND 試験計画_試験車履歴_種別.ID = :ID");
                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Int32,
                    Object = val.ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.種別 != null)
            {
                sql.AppendLine("    AND 試験計画_試験車履歴_種別.種別 = :種別");
                prms.Add(new BindModel
                {
                    Name = ":種別",
                    Type = OracleDbType.Varchar2,
                    Object = val.種別,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.SORT_NO != null)
            {
                sql.AppendLine("    AND 試験計画_試験車履歴_種別.SORT_NO = :SORT_NO");
                prms.Add(new BindModel
                {
                    Name = ":SORT_NO",
                    Type = OracleDbType.Int32,
                    Object = val.SORT_NO,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    SORT_NO");

            return db.ReadModelList<ClassSearchOutModel>(sql.ToString(), prms);

        }

    }
}