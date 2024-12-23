using System.Web.Http;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// お気に入り（カーシェア）
    /// </summary>
    public class CarShareFavoriteController : BaseAPIController<CarShareFavoriteLogic, CarShareFavoriteSearchOutModel>
    {
        #region お気に入り（カーシェア）取得
        /// <summary>
        /// お気に入り（カーシェア）取得
        /// </summary>
        /// <param name="search">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarShareFavoriteSearchInModel search)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(search)));
        }
        #endregion

        #region お気に入り（カーシェア）追加
        /// <summary>
        /// お気に入り（カーシェア）追加
        /// </summary>
        /// <param name="share">お気に入り（カーシェア）登録情報</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(CarShareFavoriteItemModel share)
        {
            return Ok(base.GetResponse(base.GetLogic().Insert(share)));
        }
        #endregion

    }
}