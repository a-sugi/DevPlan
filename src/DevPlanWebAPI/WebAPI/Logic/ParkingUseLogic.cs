using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Linq;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 駐車場管理ロジッククラス
    /// </summary>
    public class ParkingUseLogic : BaseLogic
    {
        #region 駐車場管理検索
        /// <summary>
        /// 駐車場管理検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<ParkingUseModel> Get(ParkingUseSearchModel cond)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    A.\"データID\"");
            sql.AppendLine("    ,A.\"LOCATION_NO\"");
            sql.AppendLine("    ,A.\"AREA_NO\"");
            sql.AppendLine("    ,A.\"SECTION_NO\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    PARKING_USE A");
            sql.AppendLine("WHERE 0 = 0");

            //パラメータ
            var prms = new List<BindModel>();

            if (cond.データID != null)
            {
                sql.AppendLine("    AND A.\"データID\" = :データID ");
                prms.Add(new BindModel
                {
                    Name = ":データID",
                    Type = OracleDbType.Int64,
                    Object = cond.データID,
                    Direct = ParameterDirection.Input
                });
            }

            //取得
            return db.ReadModelList<ParkingUseModel>(sql.ToString(), prms);
        }
        #endregion

        #region 駐車場管理登録
        /// <summary>
        /// 駐車場管理登録
        /// </summary>
        /// <param name="list">駐車場管理項目</param>
        /// <returns>登録可否</returns>
        public bool Post(List<ParkingUseModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //駐車場管理削除
                results.Add(DeleteItem(list));

                //駐車場管理登録
                results.Add(InsertDealItem(list));
            }

            //登録が成功したかどうか
            var flg = results.All(x => x == true);
            if (flg == true)
            {
                //コミット
                db.Commit();
            }
            else
            {
                //ロールバック
                db.Rollback();
            }

            return flg;
        }
        #endregion

        /// <summary>
        /// 駐車場管理登録
        /// </summary>
        /// <param name="list">駐車場管理登録</param>
        /// <returns>登録可否</returns>
        private bool InsertDealItem(List<ParkingUseModel> list)
        {
            var results = new List<bool>();

            foreach (var item in list)
            {
                //SQL
                var sql = new StringBuilder();

                sql.AppendLine("INSERT INTO");
                sql.AppendLine("\"PARKING_USE\"(");
                sql.AppendLine("    \"データID\"");
                sql.AppendLine("    ,\"LOCATION_NO\"");
                sql.AppendLine("    ,\"AREA_NO\"");
                sql.AppendLine("    ,\"SECTION_NO\"");
                sql.AppendLine("    ,\"INPUT_DATETIME\"");
                sql.AppendLine("    ,\"INPUT_PERSONEL_ID\"");
                sql.AppendLine(") VALUES (");
                sql.AppendLine("    :データID");
                sql.AppendLine("    ,:LOCATION_NO");
                sql.AppendLine("    ,:AREA_NO");
                sql.AppendLine("    ,:SECTION_NO");
                sql.AppendLine("    ,SYSDATE");
                sql.AppendLine("    ,:INPUT_PERSONEL_ID");
                sql.AppendLine(")");

                //パラメータ
                var prms = new List<BindModel>
                {
                     new BindModel
                     {
                         Name = ":データID",
                         Type = OracleDbType.Int64,
                         Object = item.データID,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":LOCATION_NO",
                         Type = OracleDbType.Int64,
                         Object = item.LOCATION_NO,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":AREA_NO",
                         Type = OracleDbType.Int64,
                         Object = item.AREA_NO,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":SECTION_NO",
                         Type = OracleDbType.Int64,
                         Object = item.SECTION_NO ?? 0,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":INPUT_PERSONEL_ID",
                         Type = OracleDbType.Varchar2,
                         Object = item.INPUT_PERSONEL_ID,
                         Direct = ParameterDirection.Input
                     },
                };

                results.Add(db.InsertData(sql.ToString(), prms));
            }

            return results.All(x => x == true);
        }

        #region 駐車場管理削除
        /// <summary>
        /// 駐車場管理削除
        /// </summary>
        /// <param name="list">駐車場管理項目</param>
        /// <returns>削除可否</returns>
        public bool Delete(List<ParkingUseModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //削除対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //区画状態更新「空き」
                results.Add(UpdateItem(list));

                //駐車場管理削除
                results.Add(DeleteItem(list));
            }

            //削除が成功したかどうか
            var flg = results.All(x => x == true);
            if (flg == true)
            {
                //コミット
                db.Commit();
            }
            else
            {
                //ロールバック
                db.Rollback();
            }

            return flg;
        }

        /// <summary>
        /// 駐車場区画ステータス更新
        /// </summary>
        /// <param name="list">駐車場管理項目</param>
        /// <returns>削除可否</returns>
        private bool UpdateItem(List<ParkingUseModel> list)
        {
            var results = new List<bool>();
            ParkingSectionLogic ct = new ParkingSectionLogic();
            ct.SetDBAccess(this.db);
            var inputList = new List<ParkingModel>();

            foreach (ParkingUseModel data in list)
            {
                //データIDに該当するデータを検索
                var cond = new ParkingUseSearchModel { データID = data.データID };
                var useData = this.Get(cond);

                if (useData == null || useData.Count == 0)
                {
                    continue;
                }

                var use = useData.First();

                if (use.LOCATION_NO == null || use.AREA_NO == null ||
                    use.SECTION_NO == null)
                {
                    continue;
                }

                //検索したデータのステータスを「空き」にする
                var inputData = new ParkingModel
                {
                    LOCATION_NO = use.LOCATION_NO,
                    AREA_NO = use.AREA_NO,
                    SECTION_NO = use.SECTION_NO,
                    STATUS = 0,
                    INPUT_PERSONEL_ID = data.INPUT_PERSONEL_ID,
                };

                inputList.Add(inputData);
            }

            return ct.UpdateItem(inputList);
        }

        /// <summary>
        /// 駐車場管理削除
        /// </summary>
        /// <param name="list">駐車場管理項目</param>
        /// <returns>削除可否</returns>
        private bool DeleteItem(List<ParkingUseModel> list)
        {
            // SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"PARKING_USE\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    \"データID\" = :データID");

            var results = new List<bool>();

            foreach (var item in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":データID",
                         Type = OracleDbType.Int64,
                         Object = item.データID,
                         Direct = ParameterDirection.Input
                     }
                };

                results.Add(db.UpdateData(sql.ToString(), prms));
            }

            return results.All(x => x == true);
        }
        #endregion
    }
}