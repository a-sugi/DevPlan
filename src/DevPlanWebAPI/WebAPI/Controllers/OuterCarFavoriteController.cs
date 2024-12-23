using System.Web.Http;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// お気に入り（外製車）
    /// </summary>
    public class OuterCarFavoriteController : BaseAPIController<OuterCarFavoriteLogic, OuterCarFavoriteSearchOutModel>
    {
        #region お気に入り（外製車）取得
        /// <summary>
        /// お気に入り（外製車）取得
        /// </summary>
        /// <param name="search">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]OuterCarFavoriteSearchInModel search)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(search)));
        }
        #endregion

        #region お気に入り（外製車）追加
        /// <summary>
        /// お気に入り（外製車）追加
        /// </summary>
        /// <param name="outsource">お気に入り（外製車）登録情報</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(OuterCarFavoriteItemModel outsource)
        {
            return Ok(base.GetResponse(base.GetLogic().Insert(outsource)));
        }
        #endregion

    }
}