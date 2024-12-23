using System.Data;
using System.Text;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using System.Diagnostics;
using System.Linq;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>XeyeId検索</remarks>
    public class XeyeLogic : BaseLogic
    {
        /// <summary>
        /// Xeyeデータの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public List<XeyeSearchOutModel> GetData(XeyeSearchInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     物品コード");
            sql.AppendLine("    ,物品名");
            sql.AppendLine("    ,物品名2");
            sql.AppendLine("    ,物品名カナ");
            sql.AppendLine("    ,物品区分コード");
            sql.AppendLine("    ,備考");
            sql.AppendLine("    ,ソート順");
            sql.AppendLine("    ,\"1：有効／0：無効\" 有効無効");
            sql.AppendLine("FROM");
            sql.AppendLine("    SCHEDULE_TO_XEYE");
            sql.AppendLine("WHERE SCHEDULE_TO_XEYE.物品名2 = :物品名2");
            prms.Add(new BindModel
            {
                Name = ":物品名2",
                Type = OracleDbType.Varchar2,
                Object = val.物品名2,
                Direct = ParameterDirection.Input
            });

            return db.ReadModelList<XeyeSearchOutModel>(sql.ToString(), prms);

        }

        /// <summary>
        /// XeyeIdの取得
        /// </summary>
        /// <returns></returns>
        public List<XeyeDataSearchOutModel> GetXeyeData()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     ID ");
            sql.AppendLine("    ,PASS ");
            sql.AppendLine("    ,INPUT_DATETIME ");
            sql.AppendLine("    ,CHANGE_DATETIME ");
            sql.AppendLine("FROM ");
            sql.AppendLine("    XEYE_DATA ");
            sql.AppendLine("WHERE 0 = 0 ");


            return db.ReadModelList<XeyeDataSearchOutModel>(sql.ToString(), null);
        }

        /// <summary>
        /// CSVデータの取込
        /// </summary>
        /// <param name="list">CSVデータ</param>
        /// <returns></returns>
        public bool PostData(List<XeyeSearchOutModel> list, ref List<XeyeSearchOutModel> res)
        {
            //Truncate Table処理
            var sql = new StringBuilder();
            sql.AppendLine("TRUNCATE TABLE SCHEDULE_TO_XEYE");
            if (!db.UpdateData(sql.ToString(), null))
            {
                return false;
            }

            //データ登録処理
            var sql2 = new StringBuilder();

            sql2.AppendLine("INSERT INTO SCHEDULE_TO_XEYE (");
            sql2.AppendLine("     物品コード");
            sql2.AppendLine("    ,物品名");
            sql2.AppendLine("    ,物品名2");
            sql2.AppendLine("    ,物品名カナ");
            sql2.AppendLine("    ,物品区分コード");
            sql2.AppendLine("    ,備考");
            sql2.AppendLine("    ,ソート順");
            sql2.AppendLine(") VALUES (");
            sql2.AppendLine("     :物品コード");
            sql2.AppendLine("    ,:物品名");
            sql2.AppendLine("    ,:物品名2");
            sql2.AppendLine("    ,:物品名カナ");
            sql2.AppendLine("    ,:物品区分コード");
            sql2.AppendLine("    ,:備考");
            sql2.AppendLine("    ,:ソート順");
            sql2.AppendLine(")");

            foreach (var val in list)
            {
                var prms = new List<BindModel>()
                {
                    new BindModel { Name = ":物品コード", Type = OracleDbType.Varchar2, Object = val.物品コード, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":物品名", Type = OracleDbType.Varchar2, Object = val.物品名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":物品名2", Type = OracleDbType.Varchar2, Object = val.物品名2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":物品名カナ", Type = OracleDbType.Varchar2, Object = val.物品名カナ, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":物品区分コード", Type = OracleDbType.Varchar2, Object = val.物品区分コード, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":備考", Type = OracleDbType.Varchar2, Object = val.備考, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ソート順", Type = OracleDbType.Varchar2, Object = val.ソート順, Direct = ParameterDirection.Input }
                };

                if (!db.InsertData(sql2.ToString(), prms))
                {
                    return false;
                }

                if (db.ResultCount > 0)
                {
                    res.Add(new XeyeSearchOutModel()
                    {
                        物品コード = val.物品コード,
                        物品名 = val.物品名,
                        物品名2 = val.物品名2,
                        物品名カナ = val.物品名カナ,
                        物品区分コード = val.物品区分コード,
                        備考 = val.備考,
                        ソート順 = val.ソート順
                    });
                }
            }
            return true;
        }
    }
}