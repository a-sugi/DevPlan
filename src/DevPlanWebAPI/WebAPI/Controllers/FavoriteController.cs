using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// お気に入りコントローラー
    /// </summary>
    public class FavoriteController : BaseAPIController<FavoriteLogic, FavoriteSearchOutModel>
    {
        #region お気に入り取得
        /// <summary>
        /// お気に入り取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]FavoriteSearchInModel val)
        {
            //検索結果をListに設定
            var results = new List<FavoriteSearchOutModel>();

            //データ区分が"00"の場合、全てを取得
            if (val.CLASS_DATA == Const.ClassDefault)
            {
                //01:業務計画表～09:作業履歴(カーシェア車)までを順番に取得する
                foreach (var classData in Enumerable.Range(1, 9).Select(x => x.ToString("D2")))
                {
                    //データ区分を変更
                    val.CLASS_DATA = classData;

                    //検索実行
                    results.AddRange(base.GetLogic().GetData(val));
                }
            }
            else
            {
                //検索実行
                results.AddRange(base.GetLogic().GetData(val));
            }

            //ソート
            var list = results.OrderBy(x => x.CLASS_DATA).ThenByDescending(x => x.INPUT_DATETIME);

            return Ok(base.GetResponse(list));

        }
        #endregion

        #region お気に入り更新
        /// <summary>
        /// お気に入り更新
        /// </summary>
        /// <param name="favorites">お気に入り更新情報</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<FavoriteUpdateModel> favorites)
        {
            return Ok(base.GetResponse(base.GetLogic().PutData(favorites)));
        }
        #endregion

        #region お気に入り削除
        /// <summary>
        /// お気に入り削除
        /// </summary>
        /// <param name="favorites">お気に入り削除情報</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<FavoriteDeleteModel> favorites)
        {
            //トランザクション開始
            db.Begin();

            var flg = base.GetLogic().Delete(favorites);
            if (flg == false)
            {
                //処理異常の場合、ロールバック
                db.Rollback();
            }
            else
            {
                //処理正常の場合、コミット
                db.Commit();
            }

            return Ok(base.GetResponse(flg));
        }
        #endregion
    }
}
