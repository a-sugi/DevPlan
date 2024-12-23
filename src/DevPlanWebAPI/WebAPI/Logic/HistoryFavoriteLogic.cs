using System.Collections.Generic;
using System.Data;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// お気に入り（履歴関連）業務ロジッククラス
    /// </summary>
    public class HistoryFavoriteLogic : BaseLogic
    {
        #region お気に入り（履歴関連）検索結果取得
        /// <summary>
        /// お気に入り（履歴関連）検索結果取得
        /// </summary>
        /// <param name="search">検索条件</param>
        /// <returns></returns>
        public IEnumerable<HistoryFavoriteSearchOutModel> Get(HistoryFavoriteSearchInModel search)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,TITLE");
            sql.AppendLine("    ,HISTORY_ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    FAVORITE_HISTORY");
            sql.AppendLine("WHERE 0=0");

            if (search.ID != null)
            {
                //お気に入りID
                sql.AppendLine("    AND ID = :ID");
                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Int64,
                    Object = search.ID,
                    Direct = ParameterDirection.Input
                });
            }
            else
            {
                //ユーザーID
                sql.AppendLine("    AND PERSONEL_ID = :PERSONEL_ID");
                prms.Add(new BindModel
                {
                    Name = ":PERSONEL_ID",
                    Type = OracleDbType.Varchar2,
                    Object = search.PERSONEL_ID,
                    Direct = ParameterDirection.Input
                });

                //履歴区分
                sql.AppendLine("    AND HISTORY_CODE = :HISTORY_CODE");
                prms.Add(new BindModel
                {
                    Name = ":HISTORY_CODE",
                    Type = OracleDbType.Varchar2,
                    Object = search.HISTORY_CODE,
                    Direct = ParameterDirection.Input
                });
            }

            //取得
            return db.ReadModelList<HistoryFavoriteSearchOutModel>(sql.ToString(), prms);

        }
        #endregion

        #region お気に入り（履歴関連）項目追加
        /// <summary>
        /// お気に入り（履歴関連）項目追加
        /// </summary>
        /// <param name="favorite">お気に入り（履歴関連）項目</param>
        /// <returns>追加可否</returns>
        public bool Insert(HistoryFavoriteItemModel favorite)
        {
            //トランザクション開始
            db.Begin();

            //登録
            var flg = this.InsertFavoriteHistory(favorite);

            //登録が成功したかどうか
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
        /// お気に入り（履歴関連）項目追加
        /// </summary>
        /// <param name="favorite">お気に入り（履歴関連）項目</param>
        /// <returns>追加可否</returns>
        public bool InsertFavoriteHistory(HistoryFavoriteItemModel favorite)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO FAVORITE_HISTORY");
            sql.AppendLine("(");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,TITLE");
            sql.AppendLine("    ,PERSONEL_ID");
            sql.AppendLine("    ,HISTORY_CODE");
            sql.AppendLine("    ,HISTORY_ID");
            sql.AppendLine("    ,CAR_GROUP");
            sql.AppendLine("    ,GENERAL_CODE");
            sql.AppendLine("    ,INPUT_DATETIME");
            sql.AppendLine("    ,INPUT_PERSONEL_ID");
            sql.AppendLine("    ,CHANGE_DATETIME");
            sql.AppendLine("    ,CHANGE_PERSONEL_ID");
            sql.AppendLine(")");
            sql.AppendLine("VALUES");
            sql.AppendLine("(");
            sql.AppendLine("     DEVPLAN.SEQ_FAVORITE_HISTORY_1.NEXTVAL");
            sql.AppendLine("    ,:TITLE");
            sql.AppendLine("    ,:PERSONEL_ID");
            sql.AppendLine("    ,:HISTORY_CODE");
            sql.AppendLine("    ,:HISTORY_ID");
            sql.AppendLine("    ,:CAR_GROUP");
            sql.AppendLine("    ,:GENERAL_CODE");
            sql.AppendLine("    ,SYSTIMESTAMP");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID");
            sql.AppendLine("    ,SYSTIMESTAMP");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID");
            sql.AppendLine(")");

            //パラメータ
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":TITLE", Type = OracleDbType.Varchar2, Object = favorite.TITLE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = favorite.PERSONEL_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":HISTORY_CODE", Type = OracleDbType.Varchar2, Object = favorite.HISTORY_CODE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":HISTORY_ID", Type = OracleDbType.Int64, Object = favorite.HISTORY_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":CAR_GROUP", Type = OracleDbType.Varchar2, Object = favorite.CAR_GROUP, Direct = ParameterDirection.Input },
                new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = favorite.GENERAL_CODE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = favorite.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input }
            };

            return db.InsertData(sql.ToString(), prms);

        }

        #endregion
    }
}