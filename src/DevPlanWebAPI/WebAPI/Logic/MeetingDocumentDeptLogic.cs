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
    /// 検討会資料部記号ロジッククラス
    /// </summary>
    public class MeetingDocumentDeptLogic : BaseLogic
    {
        #region 検討会資料部記号取得
        /// <summary>
        /// 検討会資料部記号取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<MeetingDocumentDeptModel> GetMeetingDocumentDept(MeetingDocumentDeptSearchModel cond)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"部記号\"");
            sql.AppendLine("    ,A.\"部名\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"試験計画_資料_部記号\" A");

            return db.ReadModelList<MeetingDocumentDeptModel>(sql.ToString(), null);

        }
        #endregion

    }
}