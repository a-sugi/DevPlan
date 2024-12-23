using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 車両管理担当
    /// </summary>
    public class CarManagerController : BaseAPIController<CarManagerLogic, CarManagerModel>
    {
        #region 車両管理担当検索
        /// <summary>
        /// 車両管理担当
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]CarManagerSearchModel cond)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(cond)));
        }
        #endregion

        #region 車両管理担当追加
        /// <summary>
        /// 車両管理担当追加
        /// </summary>
        /// <param name="list">登録データ</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<CarManagerModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().InsertData(list)));
        }
        #endregion

        #region 車両管理担当更新
        /// <summary>
        /// 車両管理担当更新
        /// </summary>
        /// <param name="list">更新データ</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<CarManagerModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().UpdateData(list)));
        }
        #endregion

        #region 車両管理担当削除
        /// <summary>
        /// 車両管理担当削除
        /// </summary>
        /// <param name="list">削除データ</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete(List<CarManagerModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteData(list)));
        }
        #endregion
    }
}