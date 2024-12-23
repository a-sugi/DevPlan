//
// 業務計画表システム
// KKA03010 目標進度リストAPI
// 作成者 : 岸　義将
// 作成日 : 2017/03/13

using System;
using System.Collections.Generic;
using System.Web.Http;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 目標進度リスト
    /// </summary>
    public class TargetProgressListController : BaseAPIController<TargetProgressListLogic, TargetProgressListSearchOutModel>
    {
        #region 目標進度リスト取得
        /// <summary>
        ///  目標進度リスト取得
        /// </summary>
        /// <param name="val">入力パラメータ</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]TargetProgressListSearchInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().GetData(val)));
        }
        #endregion

        #region 目標進度リスト追加
        /// <summary>
        /// 目標進度リスト追加
        /// </summary>
        /// <param name="list">目標進度リスト</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post(List<TargetProgressListRegistInModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().PostData(list)));

        }
        #endregion


        #region 目標進度リスト更新
        /// <summary>
        /// 目標進度リスト更新
        /// </summary>
        /// <param name="list">目標進度リスト</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put(List<TargetProgressListUpdateInModel> list)
        {
            return Ok(base.GetResponse(base.GetLogic().PutData(list)));

        }
        #endregion

        #region 目標進度リスト削除
        /// <summary>
        /// 目標進度リスト削除
        /// </summary>
        /// <param name="val">目標進度リスト削除</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]TargetProgressListDeleteInModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().DeleteData(val)));

        }
        #endregion
    }
}
