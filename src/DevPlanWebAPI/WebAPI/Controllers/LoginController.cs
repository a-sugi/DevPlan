using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;

using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// ログイン認証コントローラー
    /// </summary>
    public class LoginController : BaseAPIController<LoginLogic, LoginOutModel>
    {
        /// <summary>
        /// ログイン認証
        /// </summary>
        /// <param name="val">入力パラメータ(Json形式で渡す)</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]LoginInModel val)
        {
            var logins = new List<LoginOutModel>();

            var dt = base.GetLogic().PostData(val);

            #region <<< ユーザーが見つからない場合 >>>
            if (dt == null || dt.Rows.Count <= 0)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03021));
            }
            #endregion


            // 認証OK時にResultsを返す
            foreach (DataRow dr in dt.Rows)
            {
                logins.Add(new LoginOutModel
                {
                    UserName = Convert.ToString(dr["NAME"]),
                    PersonelID = Convert.ToString(dr["PERSONEL_ID"]),
                    AccessLevel = Convert.ToString(dr["ACCESS_LEVEL"]),
                    Password = Convert.ToString(dr["PASSWORD"])
                });
            }

            return Ok(base.GetResponse(logins));
        }

        /// <summary>
        /// パスワード変更
        /// </summary>
        /// <param name="val">更新データ</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]LoginPasswordChangeModel val)
        {
            return Ok(base.GetResponse(base.GetLogic().PutData(val)));
        }
    }
}
