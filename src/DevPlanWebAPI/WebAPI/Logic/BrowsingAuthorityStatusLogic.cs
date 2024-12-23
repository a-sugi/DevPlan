using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 閲覧権限状況ロジッククラス
    /// </summary>
    /// <remarks>試験計画_権限解除状況の操作</remarks>
    public class BrowsingAuthorityStatusLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 閲覧権限状況の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public List<BrowsingAuthorityStatusOutModel> GetData(BrowsingAuthorityStatusInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     PERSONEL_ID");
            sql.AppendLine("    ,COMPANY");
            sql.AppendLine("    ,他部署閲覧権限");
            sql.AppendLine("    ,派遣者閲覧権限");
            sql.AppendLine("    ,開発日程閲覧権限");
            sql.AppendLine("    ,外製車日程閲覧権限");
            sql.AppendLine("    ,PU制御開発日程閲覧権限");
            sql.AppendLine("    ,メッセージ非表示");
            sql.AppendLine("    ,解除日");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_権限解除状況");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // ユーザーID
            if (!string.IsNullOrWhiteSpace(val.PERSONEL_ID))
            {
                sql.AppendLine("    AND PERSONEL_ID = :PERSONEL_ID");

                prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     PERSONEL_ID ASC");
            sql.AppendLine("    ,解除日 DESC");

            return db.ReadModelList<BrowsingAuthorityStatusOutModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// 閲覧権限状況の更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(BrowsingAuthorityStatusPutModel val)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    試験計画_権限解除状況");
            sql.AppendLine("SET");
            sql.AppendLine("     メッセージ非表示 = :メッセージ非表示");
            sql.AppendLine("WHERE");
            sql.AppendLine("    PERSONEL_ID = :PERSONEL_ID");

            prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":メッセージ非表示", Type = OracleDbType.Int16, Object = val.メッセージ非表示, Direct = ParameterDirection.Input });

            return　db.UpdateData(sql.ToString(), prms);
        }
    }
}