using System.Data;
using System.Text;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using System;
using System.Linq;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>ロール関連業務ロジック</remarks>
    public class RollLogic : BaseLogic
    {
        /// <summary>
        /// ロールの検索
        /// </summary>
        /// <returns>List</returns>
        public List<RollModel> GetData(RollGetInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    RLL.ROLL_ID");
            sql.AppendLine("   ,RLL.ROLL_NAME");
            sql.AppendLine("   ,RLL.FUNCTION_ID");
            sql.AppendLine("   ,FNC.FUNCTION_NAME");
            sql.AppendLine("   ,RLL.READ_FLG");
            sql.AppendLine("   ,RLL.UPDATE_FLG");
            sql.AppendLine("   ,RLL.EXPORT_FLG");
            sql.AppendLine("   ,RLL.MANAGEMENT_FLG");
            sql.AppendLine("   ,RLL.PRINTSCREEN_FLG");
            sql.AppendLine("   ,RLL.CARSHARE_OFFICE_FLG");
            sql.AppendLine("   ,RLL.ALL_GENERAL_CODE_FLG");
            sql.AppendLine("   ,RLL.SKS_FLG");
            sql.AppendLine("   ,RLL.JIBU_UPDATE_FLG");
            sql.AppendLine("   ,RLL.JIBU_EXPORT_FLG");
            sql.AppendLine("   ,RLL.JIBU_MANAGEMENT_FLG");
            sql.AppendLine("FROM");
            sql.AppendLine("        ROLL_MST RLL");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        FUNCTION_MST FNC");
            sql.AppendLine("    ON");
            sql.AppendLine("        RLL.FUNCTION_ID = FNC.ID");
            sql.AppendLine("WHERE 0 = 0");

            if (val.ROLL_ID != null)
            {
                sql.AppendLine("AND RLL.ROLL_ID = :ROLL_ID");
                prms.Add(new BindModel { Name = ":ROLL_ID", Type = OracleDbType.Int64, Object = val.ROLL_ID, Direct = ParameterDirection.Input });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    RLL.ROLL_ID");
            sql.AppendLine("   ,RLL.FUNCTION_ID");

            return db.ReadModelList<RollModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// ロールの作成
        /// </summary>
        /// <returns>bool</returns>
        public List<RollModel> PostData(List<RollPostInModel> list)
        {
            var sql = new StringBuilder();
            var ret = new List<RollModel>();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("ROLL_MST (");
            sql.AppendLine("    ID");
            sql.AppendLine("   ,ROLL_ID");
            sql.AppendLine("   ,ROLL_NAME");
            sql.AppendLine("   ,FUNCTION_ID");
            sql.AppendLine("   ,READ_FLG");
            sql.AppendLine("   ,UPDATE_FLG");
            sql.AppendLine("   ,EXPORT_FLG");
            sql.AppendLine("   ,MANAGEMENT_FLG");
            sql.AppendLine("   ,PRINTSCREEN_FLG");
            sql.AppendLine("   ,CARSHARE_OFFICE_FLG");
            sql.AppendLine("   ,ALL_GENERAL_CODE_FLG");
            sql.AppendLine("   ,SKS_FLG");
            sql.AppendLine("   ,JIBU_UPDATE_FLG");
            sql.AppendLine("   ,JIBU_EXPORT_FLG");
            sql.AppendLine("   ,JIBU_MANAGEMENT_FLG");
            sql.AppendLine("   ,INPUT_DATETIME");
            sql.AppendLine("   ,INPUT_PERSONEL_ID");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("    (SELECT NVL(MAX(ID), 0) + 1 FROM ROLL_MST)");
            sql.AppendLine("   ,:ROLL_ID");
            sql.AppendLine("   ,:ROLL_NAME");
            sql.AppendLine("   ,:FUNCTION_ID");
            sql.AppendLine("   ,:READ_FLG");
            sql.AppendLine("   ,:UPDATE_FLG");
            sql.AppendLine("   ,:EXPORT_FLG");
            sql.AppendLine("   ,:MANAGEMENT_FLG");
            sql.AppendLine("   ,:PRINTSCREEN_FLG");
            sql.AppendLine("   ,:CARSHARE_OFFICE_FLG");
            sql.AppendLine("   ,:ALL_GENERAL_CODE_FLG");
            sql.AppendLine("   ,:SKS_FLG");
            sql.AppendLine("   ,:JIBU_UPDATE_FLG");
            sql.AppendLine("   ,:JIBU_EXPORT_FLG");
            sql.AppendLine("   ,:JIBU_MANAGEMENT_FLG");
            sql.AppendLine("   ,SYSDATE");
            sql.AppendLine("   ,:INPUT_PERSONEL_ID");
            sql.AppendLine(")");

            // トランザクション開始
            db.Begin();

            // ロールIDの採番
            var newid = GetNewRollId();

            foreach (var val in list)
            {
                var prms = new List<BindModel>();

                //パラメータ設定
                prms.Add(new BindModel { Name = ":ROLL_ID", Type = OracleDbType.Int64, Object = newid, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":ROLL_NAME", Type = OracleDbType.Varchar2, Object = val.ROLL_NAME, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":FUNCTION_ID", Type = OracleDbType.Int64, Object = val.FUNCTION_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":READ_FLG", Type = OracleDbType.Char, Object = val.READ_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":UPDATE_FLG", Type = OracleDbType.Char, Object = val.UPDATE_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":EXPORT_FLG", Type = OracleDbType.Char, Object = val.EXPORT_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":MANAGEMENT_FLG", Type = OracleDbType.Char, Object = val.MANAGEMENT_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":PRINTSCREEN_FLG", Type = OracleDbType.Char, Object = val.PRINTSCREEN_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":CARSHARE_OFFICE_FLG", Type = OracleDbType.Char, Object = val.CARSHARE_OFFICE_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":ALL_GENERAL_CODE_FLG", Type = OracleDbType.Char, Object = val.ALL_GENERAL_CODE_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":SKS_FLG", Type = OracleDbType.Char, Object = val.SKS_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":JIBU_UPDATE_FLG", Type = OracleDbType.Char, Object = val.JIBU_UPDATE_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":JIBU_EXPORT_FLG", Type = OracleDbType.Char, Object = val.JIBU_EXPORT_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":JIBU_MANAGEMENT_FLG", Type = OracleDbType.Char, Object = val.JIBU_MANAGEMENT_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input });

                if (!db.InsertData(sql.ToString(), prms))
                {
                    db.Rollback();
                    return null;
                }

                ret.Add(new RollModel { ROLL_ID = newid, FUNCTION_ID = val.FUNCTION_ID });
            }

            //コミット
            db.Commit();

            return ret;
        }

        /// <summary>
        /// ロールの更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(List<RollPutInModel> list)
        {
            var sql = new StringBuilder();

            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    ROLL_MST SINK");
            sql.AppendLine("USING(");
            sql.AppendLine("    SELECT");
            sql.AppendLine("        :ROLL_ID ROLL_ID");
            sql.AppendLine("       ,:FUNCTION_ID FUNCTION_ID");
            sql.AppendLine("    FROM");
            sql.AppendLine("        DUAL");
            sql.AppendLine(") SRC");
            sql.AppendLine("ON(");
            sql.AppendLine("    SINK.ROLL_ID = SRC.ROLL_ID");
            sql.AppendLine("AND SINK.FUNCTION_ID = SRC.FUNCTION_ID");
            sql.AppendLine(")");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("        ROLL_NAME = :ROLL_NAME");
            sql.AppendLine("       ,READ_FLG = :READ_FLG");
            sql.AppendLine("       ,UPDATE_FLG = :UPDATE_FLG");
            sql.AppendLine("       ,EXPORT_FLG = :EXPORT_FLG");
            sql.AppendLine("       ,MANAGEMENT_FLG = :MANAGEMENT_FLG");
            sql.AppendLine("       ,PRINTSCREEN_FLG = :PRINTSCREEN_FLG");
            sql.AppendLine("       ,CARSHARE_OFFICE_FLG = :CARSHARE_OFFICE_FLG");
            sql.AppendLine("       ,ALL_GENERAL_CODE_FLG = :ALL_GENERAL_CODE_FLG");
            sql.AppendLine("       ,SKS_FLG = :SKS_FLG");
            sql.AppendLine("       ,JIBU_UPDATE_FLG = :JIBU_UPDATE_FLG");
            sql.AppendLine("       ,JIBU_EXPORT_FLG = :JIBU_EXPORT_FLG");
            sql.AppendLine("       ,JIBU_MANAGEMENT_FLG = :JIBU_MANAGEMENT_FLG");
            sql.AppendLine("       ,CHANGE_DATETIME = SYSDATE");
            sql.AppendLine("       ,CHANGE_PERSONEL_ID = :CHANGE_PERSONEL_ID");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT (");
            sql.AppendLine("        ID");
            sql.AppendLine("       ,ROLL_ID");
            sql.AppendLine("       ,ROLL_NAME");
            sql.AppendLine("       ,FUNCTION_ID");
            sql.AppendLine("       ,READ_FLG");
            sql.AppendLine("       ,UPDATE_FLG");
            sql.AppendLine("       ,EXPORT_FLG");
            sql.AppendLine("       ,MANAGEMENT_FLG");
            sql.AppendLine("       ,PRINTSCREEN_FLG");
            sql.AppendLine("       ,CARSHARE_OFFICE_FLG");
            sql.AppendLine("       ,ALL_GENERAL_CODE_FLG");
            sql.AppendLine("       ,SKS_FLG");
            sql.AppendLine("       ,JIBU_UPDATE_FLG");
            sql.AppendLine("       ,JIBU_EXPORT_FLG");
            sql.AppendLine("       ,JIBU_MANAGEMENT_FLG");
            sql.AppendLine("       ,INPUT_DATETIME");
            sql.AppendLine("       ,INPUT_PERSONEL_ID");
            sql.AppendLine(")");
            sql.AppendLine("VALUES (");
            sql.AppendLine("        (SELECT NVL(MAX(ID), 0) + 1 FROM ROLL_MST)");
            sql.AppendLine("       ,:ROLL_ID");
            sql.AppendLine("       ,:ROLL_NAME");
            sql.AppendLine("       ,:FUNCTION_ID");
            sql.AppendLine("       ,:READ_FLG");
            sql.AppendLine("       ,:UPDATE_FLG");
            sql.AppendLine("       ,:EXPORT_FLG");
            sql.AppendLine("       ,:MANAGEMENT_FLG");
            sql.AppendLine("       ,:PRINTSCREEN_FLG");
            sql.AppendLine("       ,:CARSHARE_OFFICE_FLG");
            sql.AppendLine("       ,:ALL_GENERAL_CODE_FLG");
            sql.AppendLine("       ,:SKS_FLG");
            sql.AppendLine("       ,:JIBU_UPDATE_FLG");
            sql.AppendLine("       ,:JIBU_EXPORT_FLG");
            sql.AppendLine("       ,:JIBU_MANAGEMENT_FLG");
            sql.AppendLine("       ,SYSDATE");
            sql.AppendLine("       ,:CHANGE_PERSONEL_ID");
            sql.AppendLine(")");

            //トランザクション開始
            db.Begin();

            foreach (var val in list)
            {
                var prms = new List<BindModel>();

                //パラメータ設定
                prms.Add(new BindModel { Name = ":ROLL_ID", Type = OracleDbType.Int64, Object = val.ROLL_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":ROLL_NAME", Type = OracleDbType.Varchar2, Object = val.ROLL_NAME, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":FUNCTION_ID", Type = OracleDbType.Int64, Object = val.FUNCTION_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":READ_FLG", Type = OracleDbType.Char, Object = val.READ_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":UPDATE_FLG", Type = OracleDbType.Char, Object = val.UPDATE_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":EXPORT_FLG", Type = OracleDbType.Char, Object = val.EXPORT_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":MANAGEMENT_FLG", Type = OracleDbType.Char, Object = val.MANAGEMENT_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":PRINTSCREEN_FLG", Type = OracleDbType.Char, Object = val.PRINTSCREEN_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":CARSHARE_OFFICE_FLG", Type = OracleDbType.Char, Object = val.CARSHARE_OFFICE_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":ALL_GENERAL_CODE_FLG", Type = OracleDbType.Char, Object = val.ALL_GENERAL_CODE_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":SKS_FLG", Type = OracleDbType.Char, Object = val.SKS_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":JIBU_UPDATE_FLG", Type = OracleDbType.Char, Object = val.JIBU_UPDATE_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":JIBU_EXPORT_FLG", Type = OracleDbType.Char, Object = val.JIBU_EXPORT_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":JIBU_MANAGEMENT_FLG", Type = OracleDbType.Char, Object = val.JIBU_MANAGEMENT_FLG, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":CHANGE_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.CHANGE_PERSONEL_ID, Direct = ParameterDirection.Input });

                if (!db.UpdateData(sql.ToString(), prms))
                {
                    db.Rollback();
                    return false;
                }
            }

            //コミット
            db.Commit();

            return true;
        }

        /// <summary>
        /// ロールの削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(RollDeleteInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    ROLL_MST");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ROLL_ID = :ROLL_ID");

            //パラメータ設定
            prms.Add(new BindModel { Name = ":ROLL_ID", Type = OracleDbType.Int64, Object = val.ROLL_ID, Direct = ParameterDirection.Input });

            //トランザクション開始
            db.Begin();

            if (!db.DeleteData(sql.ToString(), prms))
            {
                db.Rollback();
                return false;
            }

            //コミット
            db.Commit();

            return true;
        }

        /// <summary>
        /// ロールの検索
        /// </summary>
        /// <returns>List</returns>
        public long GetNewRollId()
        {
            return (long)db.ReadDataTable("SELECT MAX(NVL(ROLL_ID, 0)) + 1 NEWID FROM ROLL_MST", null).Rows[0].Field<decimal>("NEWID");
        }
    }
}