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
    /// 検討会資料名ロジッククラス
    /// </summary>
    public class MeetingDocumentNameLogic : BaseLogic
    {
        #region 検討会資料名取得
        /// <summary>
        /// 検討会資料名取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<MeetingDocumentNameModel> GetMeetingDocumentName(MeetingDocumentNameSearchModel cond)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"ID\"");
            sql.AppendLine("    ,A.\"資料名\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"試験計画_資料名\" A");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    A.\"ID\"");

            return db.ReadModelList<MeetingDocumentNameModel>(sql.ToString(), null);

        }
        #endregion

    }
}