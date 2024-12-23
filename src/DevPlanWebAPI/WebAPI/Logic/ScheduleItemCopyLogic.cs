//
// 業務計画表システム
// KKA00110 項目コピー・移動（項目コピー）API
// 作成者 : 岸　義将
// 作成日 : 2017/02/10

using System.Collections.Generic;
using System.Data;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// KKA00110 項目コピー・移動（項目コピー）ロジッククラス
    /// </summary>
    /// <remarks>項目データの操作</remarks>
    public class ScheduleItemCopyLogic : BaseLogic
    {
        /// <summary>
        /// 項目テーブル一覧
        /// </summary>
        private Dictionary<int, string> TargetItemTable = new Dictionary<int, string>()
        {
            { 1, "WORK_SCHEDULE_ITEM" },
            { 2, "TESTCAR_SCHEDULE_ITEM" },
            { 3, "CARSHARING_SCHEDULE_ITEM" },
            { 4, "OUTERCAR_SCHEDULE_ITEM" },
            { 5, "PU_CONTROL_DEVELOPMENT_ITEM" },
            { 6, "CAR_DEVELOPMENT_SCHEDULE_ITEM" }
        };

        /// <summary>
        /// コピー元データ取得
        /// </summary>
        /// <param name="val">APIが受け取ったパラメータ</param>
        /// <returns></returns>
        public DataTable GetSourceItems(ScheduleItemCopyInModel val)
        {
            #region <<< コピー元データ取得SQL文の組立およびパラメータの設定 >>>
            var sql = new StringBuilder();
            sql.AppendLine(@"SELECT");
            sql.AppendLine(@"        ""CATEGORY"",");
            sql.AppendLine(@"        ""ID"",");
            sql.AppendLine(@"        ""FLAG_SEPARATOR"",");
            sql.AppendLine(@"        ""FLAG_CLASS"",");
            sql.AppendLine(@"        NVL(""PARALLEL_INDEX_GROUP"",0) AS ""PARALLEL_INDEX_GROUP""");
            sql.AppendLine(@"    FROM");
            sql.AppendLine(string.Format(@"        ""{0}""", TargetItemTable[val.TABLE_NUMBER]));
            sql.AppendLine(@"    WHERE");
            sql.AppendLine(@"        ""ID"" = :ID");

            var prms = new List<BindModel>
            {
                new BindModel { Name = ":ID", Type = OracleDbType.Decimal, Object = val.ID, Direct = ParameterDirection.Input }

            };
            #endregion

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// コピー先に同じ項目名がないかチェック
        /// </summary>
        /// <param name="val">APIに入力されたパラメータ群</param>
        /// <param name="Category">カテゴリ</param>
        /// <param name="Flag_Class">フラグ</param>
        /// <returns></returns>
        public DataTable CheckSameDatas(ScheduleItemCopyInModel val, string Category, string Flag_Class)
        {
            #region <<< コピー先データ取得SQL文の組立およびパラメータの設定 >>>
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"SELECT");
            sql.AppendLine(@"        ""GENERAL_CODE"",");
            sql.AppendLine(@"        ""CATEGORY"",");
            sql.AppendLine(@"        ""SECTION_GROUP_ID"",");
            sql.AppendLine(@"        ""FLAG_CLASS""");
            sql.AppendLine(@"    FROM");
            sql.AppendLine(string.Format(@"        ""{0}""", TargetItemTable[val.TABLE_NUMBER]));
            sql.AppendLine(@"    WHERE");
            sql.AppendLine(@"        ""GENERAL_CODE"" = :GENERALCODE AND");
            sql.AppendLine(@"        ""CATEGORY"" = :CATEGORY AND");
            sql.AppendLine(@"        ""SECTION_GROUP_ID"" = :SECTIONGROUPID AND");
            sql.AppendLine(@"        ""FLAG_CLASS"" = :FLAGCLASS");

            List<BindModel> prms = new List<BindModel>();
            prms.Add(new BindModel { Name = ":GENERALCODE", Type = OracleDbType.Varchar2, Object = val.GENERAL_CODE, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":CATEGORY", Type = OracleDbType.Varchar2, Object = Category, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":SECTIONGROUPID", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":FLAGCLASS", Type = OracleDbType.Varchar2, Object = Flag_Class, Direct = ParameterDirection.Input });
            #endregion

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// データコピー
        /// </summary>
        /// <returns>DataTable</returns>
        public bool ItemCopy(ScheduleItemCopyInModel val, string category, string flgSeparator, string flgClass, int rowCount)
        {
            #region <<< コピー先データ作成SQL文の組立およびパラメータの設定 >>>
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"INSERT INTO");
            sql.AppendLine(string.Format(@"        ""{0}""", TargetItemTable[val.TABLE_NUMBER]));
            sql.AppendLine(@"         (");
            sql.AppendLine(@"              ""GENERAL_CODE"",");
            sql.AppendLine(@"              ""CATEGORY"",");
            sql.AppendLine(@"              ""SORT_NO"", ");
            sql.AppendLine(@"              ""ID"", ");
            sql.AppendLine(@"              ""SECTION_GROUP_ID"", ");
            sql.AppendLine(@"              ""FLAG_SEPARATOR"", ");
            sql.AppendLine(@"              ""FLAG_CLASS"", ");
            sql.AppendLine(@"              ""INPUT_DATETIME"", ");
            sql.AppendLine(@"              ""INPUT_PERSONEL_ID"", ");
            sql.AppendLine(@"              ""INPUT_LOGIN_ID"", ");
            sql.AppendLine(@"              ""CATEGORY_ID"",");
            sql.AppendLine(@"              ""PARALLEL_INDEX_GROUP""");
            sql.AppendLine(@"         ) VALUES");
            sql.AppendLine(@"         (");
            sql.AppendLine(@"               :GENERALCODE,");
            sql.AppendLine(@"               :CATEGORY,");
            sql.AppendLine(string.Format(@"                   (SELECT NVL(MAX(SORT_NO), 0) + 1 FROM {0} WHERE GENERAL_CODE = :GENERALCODE),", TargetItemTable[val.TABLE_NUMBER]));
            sql.AppendLine(@"               SEQ_DEVELOPMENT_SCHEDULE_1.NEXTVAL,");
            sql.AppendLine(@"               :SECTIONGROUPID,");
            sql.AppendLine(@"               :FLAGSEPARATOR,");
            sql.AppendLine(@"               :FLAGCLASS,");
            sql.AppendLine(@"               SYSDATE,");
            sql.AppendLine(@"               :PERSONELID,");
            sql.AppendLine(@"               :PERSONELID,");
            sql.AppendLine(@"               SEQ_DEVELOPMENT_SCHEDULE_1.NEXTVAL,");
            sql.AppendLine(@"               :PARALLEL_INDEX_GROUP");
            sql.AppendLine(@"         )");

            var prms = new List<BindModel>
            {
                new BindModel { Name = ":GENERALCODE", Type = OracleDbType.Varchar2, Object = val.GENERAL_CODE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":CATEGORY", Type = OracleDbType.Varchar2, Object = category, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SECTIONGROUPID", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAGSEPARATOR", Type = OracleDbType.Varchar2, Object = flgSeparator, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAGCLASS", Type = OracleDbType.Varchar2, Object = flgClass, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PERSONELID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = rowCount, Direct = ParameterDirection.Input }
            };
            #endregion

            return db.InsertData(sql.ToString(), prms);
        }

    }
}