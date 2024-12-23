using System.Collections.Generic;
using System.Data;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// お気に入り（カーシェア）業務ロジッククラス
    /// </summary>
    public class CarShareFavoriteLogic : BaseLogic
    {
        #region お気に入り（カーシェア）検索結果取得
        /// <summary>
        /// お気に入り（カーシェア）検索結果取得
        /// </summary>
        /// <param name="search">検索条件</param>
        /// <returns></returns>
        public IEnumerable<CarShareFavoriteSearchOutModel> Get(CarShareFavoriteSearchInModel search)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    ID");
            sql.AppendLine("    ,TITLE");
            sql.AppendLine("    ,PERSONEL_ID");
            sql.AppendLine("    ,CAR_GROUP");
            sql.AppendLine("    ,MANAGEMENT_NO");
            sql.AppendLine("    ,PARKING_NO");
            sql.AppendLine("    ,PLACE");
            sql.AppendLine("    ,CAR_TYPE");
            sql.AppendLine("    ,DESTINATION");
            sql.AppendLine("    ,ETC_ARI_FLG");
            sql.AppendLine("    ,ETC_NASHI_FLG");
            sql.AppendLine("    ,TRANSMISSION");
            sql.AppendLine("    ,STATUS_OPEN_FLG");
            sql.AppendLine("    ,STATUS_CLOSE_FLG");
            sql.AppendLine("    ,INPUT_DATETIME");
            sql.AppendLine("    ,INPUT_PERSONEL_ID");
            sql.AppendLine("    ,CHANGE_DATETIME");
            sql.AppendLine("    ,CHANGE_PERSONEL_ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    FAVORITE_CAR_SHARE");
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
            return db.ReadModelList<CarShareFavoriteSearchOutModel>(sql.ToString(), prms);

        }
        #endregion

        #region お気に入り（カーシェア）項目追加
        /// <summary>
        /// お気に入り（カーシェア）項目追加
        /// </summary>
        /// <param name="favorite">お気に入り（カーシェア）項目</param>
        /// <returns>追加可否</returns>
        public bool Insert(CarShareFavoriteItemModel favorite)
        {
            //トランザクション開始
            db.Begin();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO FAVORITE_CAR_SHARE");
            sql.AppendLine("(");
            sql.AppendLine("    ID");
            sql.AppendLine("    ,TITLE");
            sql.AppendLine("    ,PERSONEL_ID");
            sql.AppendLine("    ,CAR_GROUP");
            sql.AppendLine("    ,MANAGEMENT_NO");
            sql.AppendLine("    ,PARKING_NO");
            sql.AppendLine("    ,PLACE");
            sql.AppendLine("    ,CAR_TYPE");
            sql.AppendLine("    ,DESTINATION");
            sql.AppendLine("    ,ETC_ARI_FLG");
            sql.AppendLine("    ,ETC_NASHI_FLG");
            sql.AppendLine("    ,TRANSMISSION");
            sql.AppendLine("    ,STATUS_OPEN_FLG");
            sql.AppendLine("    ,STATUS_CLOSE_FLG");
            sql.AppendLine("    ,INPUT_DATETIME");
            sql.AppendLine("    ,INPUT_PERSONEL_ID");
            sql.AppendLine("    ,CHANGE_DATETIME");
            sql.AppendLine("    ,CHANGE_PERSONEL_ID");
            sql.AppendLine(")");
            sql.AppendLine("VALUES");
            sql.AppendLine("(");
            sql.AppendLine("     DEVPLAN.SEQ_FAVORITE_CAR_SHARE_1.NEXTVAL");
            sql.AppendLine("    ,:TITLE");
            sql.AppendLine("    ,:PERSONEL_ID");
            sql.AppendLine("    ,:CAR_GROUP");
            sql.AppendLine("    ,:MANAGEMENT_NO");
            sql.AppendLine("    ,:PARKING_NO");
            sql.AppendLine("    ,:PLACE");
            sql.AppendLine("    ,:CAR_TYPE");
            sql.AppendLine("    ,:DESTINATION");
            sql.AppendLine("    ,:ETC_ARI_FLG");
            sql.AppendLine("    ,:ETC_NASHI_FLG");
            sql.AppendLine("    ,:TRANSMISSION");
            sql.AppendLine("    ,:STATUS_OPEN_FLG");
            sql.AppendLine("    ,:STATUS_CLOSE_FLG");
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
                new BindModel { Name = ":CAR_GROUP", Type = OracleDbType.Varchar2, Object = favorite.CAR_GROUP, Direct = ParameterDirection.Input },
                new BindModel { Name = ":MANAGEMENT_NO", Type = OracleDbType.Varchar2, Object = favorite.MANAGEMENT_NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PARKING_NO", Type = OracleDbType.Varchar2, Object = favorite.PARKING_NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PLACE", Type = OracleDbType.Varchar2, Object = favorite.PLACE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":CAR_TYPE", Type = OracleDbType.Varchar2, Object = favorite.CAR_TYPE, Direct = ParameterDirection.Input },
                new BindModel { Name = ":DESTINATION", Type = OracleDbType.Varchar2, Object = favorite.DESTINATION, Direct = ParameterDirection.Input },
                new BindModel { Name = ":ETC_ARI_FLG", Type = OracleDbType.Char, Object = favorite.ETC_ARI_FLG, Direct = ParameterDirection.Input },
                new BindModel { Name = ":ETC_NASHI_FLG", Type = OracleDbType.Char, Object = favorite.ETC_NASHI_FLG, Direct = ParameterDirection.Input },
                new BindModel { Name = ":TRANSMISSION", Type = OracleDbType.Varchar2, Object = favorite.TRANSMISSION, Direct = ParameterDirection.Input },
                new BindModel { Name = ":STATUS_OPEN_FLG", Type = OracleDbType.Varchar2, Object = favorite.STATUS_OPEN_FLG, Direct = ParameterDirection.Input },
                new BindModel { Name = ":STATUS_CLOSE_FLG", Type = OracleDbType.Varchar2, Object = favorite.STATUS_CLOSE_FLG, Direct = ParameterDirection.Input }
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