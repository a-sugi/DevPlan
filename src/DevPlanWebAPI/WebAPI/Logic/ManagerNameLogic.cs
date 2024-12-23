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
    /// <remarks>月報データの操作</remarks>
    public class ManagerNameLogic : BaseLogic
    {
        /// <summary>
        /// 部長名の取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        public List<ManagerNameModel> Get(ManagerNameSearchModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT MANAGER_NAME FROM 試験計画_月次報告 ");
            sql.Append("WHERE ");
            sql.Append("DEPARTMENT_ID = :DEPARTMENT_ID and ");
            prms.Add(new BindModel
            {
                Name = ":DEPARTMENT_ID",
                Type = OracleDbType.Varchar2,
                Object = val.DEPARTMENT_ID,
                Direct = ParameterDirection.Input
            });
            sql.Append("MANAGER_NAME IS NOT NULL ");
            sql.Append("ORDER BY ISSUED_DATETIME DESC");

            return db.ReadModelList<ManagerNameModel>(sql.ToString(), prms);
        }
    }
}