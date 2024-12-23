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
    /// 設計チェック指摘
    /// </summary>
    public class DesignCheckPointController : BaseAPIController<DesignCheckPointLogic, DesignCheckPointGetOutModel>
    {
        /// <summary>
        /// 設計チェック指摘検索
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Get([FromUri]DesignCheckPointGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<DesignCheckPointGetOutModel>();

            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new DesignCheckPointGetOutModel
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    開催日_ID = Convert.ToInt32(dr["開催日_ID"]),
                    //Update Start 2021/06/11 張晋華 開発計画表設計チェック機能改修
                    指摘NO = dr["指摘NO"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["指摘NO"]),
                    //Update End 2021/06/11 張晋華 開発計画表設計チェック機能改修
                    //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
                    試作管理NO = dr["試作管理NO"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["試作管理NO"]),
                    //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修
                    FLAG_CLOSE = dr["FLAG_CLOSE"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_CLOSE"]),
                    //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
                    FLAG_試作CLOSE = dr["FLAG_試作CLOSE"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_試作CLOSE"]),
                    //Append End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
                    部品 = Convert.ToString(dr["部品"]),
                    状況 = Convert.ToString(dr["状況"]),
                    FLAG_処置不要 = dr["FLAG_処置不要"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_処置不要"]),
                    処置課 = Convert.ToString(dr["処置課"]),
                    処置対象 = Convert.ToString(dr["処置対象"]),
                    処置方法 = Convert.ToString(dr["処置方法"]),
                    FLAG_調整済 = dr["FLAG_調整済"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_調整済"]),
                    処置調整 = Convert.ToString(dr["処置調整"]),
                    織込日程 = dr["織込日程"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["織込日程"]),
                    FLAG_試作改修 = dr["FLAG_試作改修"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_試作改修"]),
                    部品納入日 = dr["部品納入日"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["部品納入日"]),
                    完了確認日 = dr["完了確認日"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["完了確認日"]),
                    FLAG_上司承認 = dr["FLAG_上司承認"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_上司承認"]),
                    試験車名 = Convert.ToString(dr["試験車名"]),
                    試験車_ID = dr["試験車_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["試験車_ID"]),
                    対象車両_ID = dr["対象車両_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["対象車両_ID"]),
                    状況記号 = Convert.ToString(dr["状況記号"]),
                    担当課名 = Convert.ToString(dr["担当課名"]),
                    担当課_ID = Convert.ToString(dr["担当課_ID"]),
                    担当者_ID = Convert.ToString(dr["担当者_ID"]),
                    担当者名 = Convert.ToString(dr["担当者名"]),
                    担当者_TEL = Convert.ToString(dr["担当者_TEL"]),
                    編集者日 = dr["編集者日"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["編集者日"]),
                    編集者_ID = Convert.ToString(dr["編集者_ID"]),
                    編集者名 = Convert.ToString(dr["編集者名"]),
                    履歴作成日 = dr["履歴作成日"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["履歴作成日"]),
                    FLAG_最新 = dr["FLAG_最新"] == DBNull.Value ? (int?)null : Convert.ToInt16(dr["FLAG_最新"]),
                    HISTORY_COUNT = Convert.ToInt64(dr["HISTORY_COUNT"] == DBNull.Value ? (int?)0 : dr["HISTORY_COUNT"])
                });
            }

            return Ok(base.GetResponse(resurlts));
        }

        /// <summary>
        /// 設計チェック指摘登録
        /// </summary>
        /// <param name="list">設計チェック指摘登録モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]List<DesignCheckPointPostInModel> list)
        {
            db.Begin();

            var returns = new List<DesignCheckPointPostOutModel>();

            var flg = base.GetLogic().PostData(list, ref returns);

            if (flg == false)
            {
                db.Rollback();
                return Ok(base.GetResponse(flg));
            }
            else
            {
                db.Commit();

                if (returns.Count == 0)
                {
                    return Ok(base.GetResponse(flg));
                }
                else
                {
                    return Ok(base.GetResponse(returns));
                }
            }
        }

        /// <summary>
        /// 設計チェック指摘削除
        /// </summary>
        /// <param name="list">設計チェック指摘削除モデル</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Delete([FromBody]List<DesignCheckPointDeleteInModel> list)
        {
            db.Begin();

            var flg = base.GetLogic().DeleteData(list);

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
    }
}
