using System.Web.Http;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// お気に入り（試験車）
    /// </summary>
    public class TestCarFavoriteController : BaseAPIController<TestCarFavoriteLogic, TestCarFavoriteSearchOutModel>
    {
        #region お気に入り（試験車）取得
        /// <summary>
        /// お気に入り（試験車）取得
        /// </summary>
        /// <param name="search">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TestCarFavoriteSearchInModel search)
        {
            return Ok(base.GetResponse(base.GetLogic().Get(search)));
        }
        #endregion

        #region お気に入り（試験車）追加
        /// <summary>
        /// お気に入り（試験車）追加
        /// </summary>
        /// <param name="plan">お気に入り（試験車）登録情報</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(TestCarFavoriteItemModel plan)
        {
            return Ok(base.GetResponse(base.GetLogic().Insert(plan)));
        }
        #endregion

    }
}