using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// トラック予約ユーザー操作ロジッククラス
    /// </summary>
    public class TruckUserLogic : BaseLogic
    {
        /// <summary>
        /// トラック予約管理者ユーザー取得処理
        /// </summary>
        /// <returns></returns>
        public List<TruckManagementUserModel> GetTruckManagementUser()
        {
            string sql = @"
SELECT
  トラック_管理者.PERSONEL_ID
  , トラック_管理者.TEL
  , PERSONEL_LIST.NAME
FROM
  トラック_管理者 
  INNER JOIN PERSONEL_LIST 
    ON トラック_管理者.PERSONEL_ID = PERSONEL_LIST.PERSONEL_ID
";

            return db.ReadModelList<TruckManagementUserModel>(sql, null);
        }

        /// <summary>
        /// トラック管理者情報更新処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateTruckManagementUser(TruckManagementUserModel model)
        {
            if (model == null) { return false; }

            var sql = @"
UPDATE
    トラック_管理者
SET
    PERSONEL_ID = :PERSONEL_ID,
    TEL = :TEL
";
            db.Begin();

            var results = new List<bool>();


            var prms = new List<BindModel>
            {
                new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = model.PERSONEL_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":TEL", Type = OracleDbType.Varchar2, Object = model.TEL, Direct = ParameterDirection.Input },
            };

            results.Add(db.UpdateData(sql, prms));

            var flg = results.All(x => x == true);
            if (flg == true)
            {
                db.Commit();
            }
            else
            {
                db.Rollback();
            }

            return flg;
        }
    }
}