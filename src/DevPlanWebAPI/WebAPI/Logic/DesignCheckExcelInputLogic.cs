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
    //Append Start 2021/06/24 張晋華 開発計画表設計チェック機能改修

    /// <summary>
    /// 設計チェックEXCLE取込判断
    /// </summary>
    public class DesignCheckExcelInputLogic : BaseLogic
    {
        #region 取得
        /// <summary>
        /// EXCELを取り込んだかどうかの取得
        /// </summary>
        public DataTable GetData(DesignCheckExcelInputModel val)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            sql.AppendLine("SELECT");
            sql.AppendLine("    ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    EXCEL_INPUT");
            sql.AppendLine("WHERE");
            sql.AppendLine("    開催日_ID = :開催日_ID");

            prms.Add(new BindModel
            {
                Name = ":開催日_ID",
                Type = OracleDbType.Int32,
                Object = val.開催日_ID,
                Direct = ParameterDirection.Input
            });

            return db.ReadDataTable(sql.ToString(), prms);
        }
        #endregion

        #region 登録
        /// <summary>
        /// EXCEL_INPUTに開催日_ID、登録日、登録者_IDを登録する
        /// </summary>
        public bool PostData(List<DesignCheckExcelInputModel> list, ref List<DesignCheckExcelInputModel> res)
        {
            var ret = false;

            //開催日_ID
            var kaisaibi = list.FirstOrDefault()?.開催日_ID;

            //既存データの取得
            var pointList = this.GetData(new DesignCheckExcelInputModel { 開催日_ID = kaisaibi });

            //データ登録
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("EXCEL_INPUT (");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,開催日_ID");
            sql.AppendLine("    ,登録日");
            sql.AppendLine("    ,登録者_ID");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM EXCEL_INPUT)");
            sql.AppendLine("    ,:開催日_ID");
            sql.AppendLine("    ,SYSDATE");
            sql.AppendLine("    ,:登録者_ID");
            sql.AppendLine(")");

            //同じ開催日_IDが存在しない場合→新規登録
            if (pointList.Rows.Count == 0)
            {
                var prms = new List<BindModel>()
                    {
                        new BindModel { Name = ":開催日_ID", Type = OracleDbType.Int32, Object = list[0].開催日_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":登録者_ID", Type = OracleDbType.Varchar2, Object = list[0].登録者_ID, Direct = ParameterDirection.Input },
                    };

                ret = db.InsertData(sql.ToString(), prms);

                res.Add(new DesignCheckExcelInputModel()
                {
                    開催日_ID = list[0].開催日_ID,
                });
            }

            return ret;
        }
        #endregion
    }
    //Append End 2021/06/24 張晋華 開発計画表設計チェック機能改修
}