using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>お気に入りデータの操作</remarks>
    public class FavoriteLogic : BaseLogic
    {
        #region メンバ変数
        private Dictionary<string, string> FavoriteTableName
            = new Dictionary<string, string>()
            {
                { Const.OperationPlan,  "FAVORITE_DEVPLAN"},        // 業務計画表
                { Const.MonthlyPlan,    "FAVORITE_DEVPLAN_MONTH"},  // 月次計画表
                { Const.TestCar,        "FAVORITE_TEST_CAR"},       // 試験車日程
                { Const.CarShare,       "FAVORITE_CAR_SHARE"},      // カーシェア
                { Const.OuterCar,       "FAVORITE_OUTSOURCE_CAR"},  // 外製車日程
                { Const.PlanHistory,    "FAVORITE_HISTORY"},        // 進捗履歴
                { Const.TestCarHistory, "FAVORITE_HISTORY"},        // 作業履歴(試験車)
                { Const.OuterCarHistory,"FAVORITE_HISTORY"},        // 作業履歴(外製車)
                { Const.CarShareHistory,"FAVORITE_HISTORY"}         // 作業履歴(カーシェア)
            };
        #endregion

        #region お気に入りデータの取得
        /// <summary>
        /// お気に入りデータの取得
        /// </summary>
        /// <param name="cond"></param>
        /// <returns>DataTable</returns>
        public List<FavoriteSearchOutModel> GetData(FavoriteSearchInModel cond)
        {
            var sql = new StringBuilder();

            //SQL生成
            sql.AppendLine("WITH");
            sql.AppendLine("  ATH AS (");
            sql.AppendLine("    SELECT");
            sql.AppendLine("         A.PERSONEL_ID");
            sql.AppendLine("        ,B.CAR_GROUP");
            sql.AppendLine("        ,B.GENERAL_CODE");
            sql.AppendLine("        ,A.PERMISSION_PERIOD_START");
            sql.AppendLine("        ,A.PERMISSION_PERIOD_END");
            sql.AppendLine("    FROM");
            sql.AppendLine("        試験計画_他部署閲覧許可 A");
            sql.AppendLine("        INNER JOIN GENERAL_CODE B");
            sql.AppendLine("        ON A.GENERAL_CODE = B.GENERAL_CODE");
            sql.AppendLine("    WHERE");
            sql.AppendLine("        A.PERSONEL_ID = :PERSONEL_ID");
            sql.AppendLine("        AND A.PERMISSION_PERIOD_START <= TRUNC(SYSDATE)");
            sql.AppendLine("        AND A.PERMISSION_PERIOD_END >= TRUNC(SYSDATE)");
            sql.AppendLine(")");

            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("     FAV.ID");
            sql.AppendLine("    ,FAV.TITLE");
            sql.AppendLine("    ,FAV.INPUT_DATETIME");
            sql.AppendLine("    ,:CLASS_DATA AS CLASS_DATA");
            sql.AppendLine("    ,CASE ");

            // 月次計画表
            if (cond.CLASS_DATA == Const.MonthlyPlan)
            {
                sql.AppendLine("        WHEN FAV.GENERAL_CODE IS NULL THEN 1");
            }

            sql.AppendLine("        WHEN ATH.GENERAL_CODE IS NOT NULL THEN 1");
            sql.AppendLine("        ELSE 0");
            sql.AppendLine("     END PERMIT_FLG");
            sql.AppendLine("FROM");
            sql.AppendLine(string.Format("    {0} FAV", this.FavoriteTableName[cond.CLASS_DATA]));
            sql.AppendLine("    LEFT JOIN ATH");
            sql.AppendLine("    ON FAV.PERSONEL_ID = ATH.PERSONEL_ID");

            // データ区分
            switch (cond.CLASS_DATA)
            {
                case Const.OperationPlan:   // 業務計画表
                case Const.TestCar:         // 試験車日程
                case Const.PlanHistory:     // 進捗履歴
                case Const.TestCarHistory:  // 作業履歴（試験車）

                    sql.AppendLine("    AND FAV.GENERAL_CODE = ATH.GENERAL_CODE");

                    break;

                case Const.CarShare:        // カーシェア日程
                case Const.OuterCar:        // 外製車日程
                case Const.OuterCarHistory: // 作業履歴（外製車）
                case Const.CarShareHistory: // 作業履歴（カーシェア）

                    sql.AppendLine("    AND FAV.CAR_GROUP = ATH.CAR_GROUP");

                    break;
            }

            sql.AppendLine("WHERE");

            // ユーザーID
            sql.AppendLine("    FAV.PERSONEL_ID = :PERSONEL_ID");

            // データ区分
            switch (cond.CLASS_DATA)
            {
                case Const.PlanHistory:    // 進捗履歴

                    sql.AppendLine("    AND FAV.HISTORY_CODE = '1'");

                    break;

                case Const.TestCarHistory: // 作業履歴（試験車）

                    sql.AppendLine("    AND FAV.HISTORY_CODE = '2'");

                    break;

                case Const.OuterCarHistory: // 作業履歴（外製車）

                    sql.AppendLine("    AND FAV.HISTORY_CODE = '3'");

                    break;

                case Const.CarShareHistory: // 作業履歴（カーシェア）

                    sql.AppendLine("    AND FAV.HISTORY_CODE = '4'");

                    break;
            }

            var prms = new List<BindModel>()
            {
                new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = cond.PERSONEL_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":CLASS_DATA", Type = OracleDbType.Varchar2, Object = cond.CLASS_DATA, Direct = ParameterDirection.Input }
            };

            //取得
            return db.ReadModelList<FavoriteSearchOutModel>(sql.ToString(), prms)?.GroupBy(x => x.ID)?.Select(group => group.First())?.ToList() ?? new List<FavoriteSearchOutModel>();
        }
        #endregion

        #region お気に入りデータの更新
        /// <summary>
        /// お気に入りデータの更新
        /// </summary>
        /// <param name="favorites">更新データ</param>
        /// <returns>bool</returns>
        public bool PutData(List<FavoriteUpdateModel> favorites)
        {
            //トランザクション開始
            db.Begin();

            var results = new List<bool>();
            //更新対象があるかどうか
            if (favorites != null && favorites.Any() == true)
            {
                var sql = new StringBuilder();

                sql.AppendLine("UPDATE");
                sql.AppendLine(string.Format("    {0}", this.FavoriteTableName[favorites[0].CLASS_DATA]));
                sql.AppendLine("SET");
                sql.AppendLine("    TITLE = :TITLE");
                sql.AppendLine("    ,CHANGE_DATETIME = SYSDATE");
                sql.AppendLine("    ,CHANGE_PERSONEL_ID = :CHANGE_PERSONEL_ID");
                sql.AppendLine("WHERE");
                sql.AppendLine("    ID = :ID");

                foreach (var tyuui in favorites)
                {
                    //パラメータ
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = tyuui.ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":TITLE", Type = OracleDbType.Varchar2, Object = tyuui.TITLE, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":CHANGE_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = tyuui.PERSONEL_ID, Direct = ParameterDirection.Input }
                    };

                    //更新
                    results.Add(db.UpdateData(sql.ToString(), prms));

                }
            }

            //更新が成功したかどうか
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

        #region お気に入りテーブルの削除
        /// <summary>
        /// お気に入りテーブルの削除
        /// </summary>
        /// <param name="favorites">削除データ</param>
        /// <returns>bool</returns>
        public bool Delete(List<FavoriteDeleteModel> favorites)
        {
            var prms = new List<BindModel>();
            var results = new List<bool>();

            //削除対象があるかどうか
            if (favorites != null && favorites.Any() == true)
            {
                var sql = new StringBuilder();

                sql.AppendLine("DELETE FROM");
                sql.AppendLine(string.Format("    {0}", this.FavoriteTableName[favorites[0].CLASS_DATA]));
                sql.AppendLine("WHERE");
                sql.AppendLine("    ID IN(NULL ");

                for (var i = 0; i < favorites.Count(); i++)
                {
                    var name = string.Format(":ID{0}", i);
                    sql.AppendFormat(",{0}", name);
                    prms.Add(new BindModel
                    {
                        Name = name,
                        Type = OracleDbType.Int64,
                        Object = favorites[i].ID,
                        Direct = ParameterDirection.Input
                    });
                }
                sql.AppendLine(")");

                results.Add(db.DeleteData(sql.ToString(), prms));
            }

            //削除が成功したかどうか
            var flg = results.All(x => x == true);

            return flg;
        }
        #endregion
    }
}