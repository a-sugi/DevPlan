using System.Web.Http;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// お気に入り（履歴関連）
    /// </summary>
    public class HistoryFavoriteController : BaseAPIController<HistoryFavoriteLogic, HistoryFavoriteSearchOutModel>
    {
        #region お気に入り（履歴関連）取得
        /// <summary>
        /// お気に入り（履歴関連）取得
        /// </summary>
        /// <param name="search">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]HistoryFavoriteSearchInModel search)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(search)));
        }
        #endregion

        #region お気に入り（履歴関連）登録
        /// <summary>
        /// お気に入り（履歴関連）登録
        /// </summary>
        /// <param name="plan">お気に入り（履歴関連）登録情報</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(HistoryFavoriteItemModel plan)
        {
            return Ok(base.GetResponse(base.GetLogic().Insert(plan)));
        }
        #endregion

    }
}