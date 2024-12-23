using System.Collections.Generic;
using System.Data;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// お気に入り（月次計画）業務ロジッククラス
    /// </summary>
    public class MonthlyWorkFavoriteLogic : BaseLogic
    {
        #region お気に入り（業務計画）検索結果取得
        /// <summary>
        /// お気に入り（月次計画）検索結果取得
        /// </summary>
        /// <param name="search">検索条件</param>
        /// <returns></returns>
        public IEnumerable<MonthlyWorkFavoriteSearchOutModel> Get(MonthlyWorkFavoriteSearchInModel search)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    ID");
            sql.AppendLine("    ,TITLE");
            sql.AppendLine("    ,PERSONEL_ID");
            sql.AppendLine("    ,GENERAL_CODE");
            sql.AppendLine("    ,CLASS_KBN");
            sql.AppendLine("    ,CLASS_ID");
            sql.AppendLine("    ,STATUS_OPEN_FLG");
            sql.AppendLine("    ,MANAGER_NAME");
            sql.AppendLine("    ,STATUS_CLOSE_FLG");
            sql.AppendLine("    ,TARGET_MONTH");
            sql.AppendLine("    ,DISPLAY_KBN");
            sql.AppendLine("    ,INPUT_DATETIME");
            sql.AppendLine("    ,INPUT_PERSONEL_ID");
            sql.AppendLine("    ,CHANGE_DATETIME");
            sql.AppendLine("    ,CHANGE_PERSONEL_ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    FAVORITE_DEVPLAN_MONTH");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int64,
                Object = search.ID,
                Direct = ParameterDirection.Input
            });

            //取得
            return db.ReadModelList<MonthlyWorkFavoriteSearchOutModel>(sql.ToString(), prms);

        }
        #endregion

        #region お気に入り（月次計画）項目追加
        /// <summary>
        /// お気に入り（月次計画  ）項目追加
        /// </summary>
        /// <param name="favorite">お気に入り（業務計画）項目</param>
        /// <returns>追加可否</returns>
        public bool Insert(MonthlyWorkFavoriteItemModel favorite)
        {
            //トランザクション開始
            db.Begin();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO FAVORITE_DEVPLAN_MONTH");
            sql.AppendLine("(");
            sql.AppendLine("    ID");
            sql.AppendLine("    ,TITLE");
            sql.AppendLine("    ,PERSONEL_ID");
            sql.AppendLine("    ,GENERAL_CODE");
            sql.AppendLine("    ,CLASS_KBN");
            sql.AppendLine("    ,CLASS_ID");
            sql.AppendLine("    ,MANAGER_NAME");
            sql.AppendLine("    ,STATUS_OPEN_FLG");
            sql.AppendLine("    ,STATUS_CLOSE_FLG");
            sql.AppendLine("    ,TARGET_MONTH");
            sql.AppendLine("    ,DISPLAY_KBN");
            sql.AppendLine("    ,INPUT_DATETIME");
            sql.AppendLine("    ,INPUT_PERSONEL_ID");
            sql.AppendLine("    ,CHANGE_DATETIME");
            sql.AppendLine("    ,CHANGE_PERSONEL_ID");
            sql.AppendLine(")");
            sql.AppendLine("VALUES");
            sql.AppendLine("(");
            sql.AppendLine("     DEVPLAN.SEQ_FAVORITE_DEVPLAN_MONTH_1.NEXTVAL");
            sql.AppendLine("    ,:TITLE");
            sql.AppendLine("    ,:PERSONEL_ID");
            sql.AppendLine("    ,:GENERAL_CODE");
            sql.AppendLine("    ,:CLASS_KBN");
            sql.AppendLine("    ,:CLASS_ID");
            sql.AppendLine("    ,:MANAGER_NAME");
            sql.AppendLine("    ,:STATUS_OPEN_FLG");
            sql.AppendLine("    ,:STATUS_CLOSE_FLG");
            sql.AppendLine("    ,:TARGET_MONTH");
            sql.AppendLine("    ,:DISPLAY_KBN");
            sql.AppendLine("    ,SYSDATE");
            sql.AppendLine("    ,:PERSONEL_ID");
            sql.AppendLine("    ,SYSDATE");
            sql.AppendLine("    ,:PERSONEL_ID");
            sql.AppendLine(")");

            //パラメータ
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":TITLE", Type = OracleDbType.Varchar2, Object = favorite.TITLE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = favorite.PERSONEL_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = favorite.GENERAL_CODE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":CLASS_KBN", Type = OracleDbType.Char, Object = favorite.CLASS_KBN, Direct = ParameterDirection.Input },
                new BindModel { Name = ":CLASS_ID", Type = OracleDbType.Varchar2, Object = favorite.CLASS_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":MANAGER_NAME", Type = OracleDbType.Varchar2, Object = favorite.MANAGER_NAME, Direct = ParameterDirection.Input },
                new BindModel { Name = ":STATUS_OPEN_FLG", Type = OracleDbType.Char, Object = favorite.STATUS_OPEN_FLG, Direct = ParameterDirection.Input },
                new BindModel { Name = ":STATUS_CLOSE_FLG", Type = OracleDbType.Char, Object = favorite.STATUS_CLOSE_FLG, Direct = ParameterDirection.Input },
                new BindModel { Name = ":TARGET_MONTH", Type = OracleDbType.Date, Object = favorite.TARGET_MONTH, Direct = ParameterDirection.Input },
                new BindModel { Name = ":DISPLAY_KBN", Type = OracleDbType.Char, Object = favorite.DISPLAY_KBN, Direct = ParameterDirection.Input }
            };

            //追加
            var flg = db.InsertData(sql.ToString(), prms);

            //追加が成功したかどうか
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
    }
}