using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 試験車（設計チェック）
    /// </summary>
    public class DesignCheckTestCarController : BaseAPIController<DesignCheckTestCarLogic, DesignCheckTestCarGetOutModel>
    {
        /// <summary>
        /// 試験車（設計チェック）検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DesignCheckTestCarGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<DesignCheckTestCarGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new DesignCheckTestCarGetOutModel
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    管理票NO = Convert.ToString(dr["管理票NO"]),
                    ESTABLISHMENT = Convert.ToString(dr["ESTABLISHMENT"]),
                    試験車名 = Convert.ToString(dr["試験車名"]),
                    グレード = Convert.ToString(dr["グレード"]),
                    試験目的 = Convert.ToString(dr["試験目的"]),
                    車系 = Convert.ToString(dr["車系"]),
                    開発符号 = !string.IsNullOrWhiteSpace(dr["開発符号"]?.ToString()) ? Convert.ToString(dr["開発符号"]) : parseTestCarName(0, dr["試験車名"]?.ToString()),
                    試作時期 = !string.IsNullOrWhiteSpace(dr["試作時期"]?.ToString()) ? Convert.ToString(dr["試作時期"]) : parseTestCarName(1, dr["試験車名"]?.ToString()),
                    号車 = !string.IsNullOrWhiteSpace(dr["号車"]?.ToString()) ? Convert.ToString(dr["号車"]) : parseTestCarName(2, dr["試験車名"]?.ToString()),
                    駐車場番号 = Convert.ToString(dr["駐車場番号"]),
                    仕向地 = Convert.ToString(dr["仕向地"]),
                    排気量 = Convert.ToString(dr["排気量"]),
                    E_G型式 = Convert.ToString(dr["E_G型式"]),
                    駆動方式 = Convert.ToString(dr["駆動方式"]),
                    トランスミッション = Convert.ToString(dr["トランスミッション"]),
                    車型 = Convert.ToString(dr["車型"]),
                    車体色 = Convert.ToString(dr["車体色"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 試験車（設計チェック）登録
        /// </summary>
        /// <param name="val">試験車（設計チェック）登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]DesignCheckTestCarPostInModel val)
        {
            db.Begin();

            var returns = new DesignCheckTestCarGetOutModel();

            var flg = base.GetLogic().PostData(val, ref returns);

            if (flg == false)
            {
                db.Rollback();

                return Ok(base.GetResponse(flg));
            }

            db.Commit();

            var resurlts = new List<DesignCheckTestCarGetOutModel>();

            resurlts.Add(returns);

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 試験車（設計チェック）更新
        /// </summary>
        /// <param name="val">試験車（設計チェック）更新モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Put([FromBody]DesignCheckTestCarPutInModel val)
        {
            db.Begin();

            var flg = base.GetLogic().PutData(val);

            if (flg == false)
            {
                db.Rollback();
            }
            else
            {
                db.Commit();
            }

            return Ok(base.GetResponse(flg));
        }

        /// <summary>
        /// 試験車（設計チェック）削除
        /// </summary>
        /// <param name="val">試験車（設計チェック）削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]DesignCheckTestCarDeleteInModel val)
        {
            db.Begin();

            var flg = base.GetLogic().DeleteData(val);

            if (flg == false)
            {
                db.Rollback();
            }
            else
            {
                db.Commit();
            }

            return Ok(base.GetResponse(flg));
        }

        private string parseTestCarName(int index, string testcarname)
        {
            var list = testcarname.Split(' ');

            return list.Length > index ? list[index] : string.Empty;
        }
    }
}
