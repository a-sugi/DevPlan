using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;
using DevPlanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 設計チェック詳細
    /// </summary>
    public class DesignCheckDetailController : BaseAPIController<DesignCheckDetailLogic, DesignCheckDetailGetOutModel>
    {
        /// <summary>
        /// 設計チェック詳細検索
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri]DesignCheckDetailGetInModel val)
        {
            var dt = base.GetLogic().GetData(val);

            if (dt == null || dt.Rows.Count < 0)
            {
                return Ok(base.GetResponse());
            }

            var resurlts = new List<DesignCheckDetailGetOutModel>();
            foreach (DataRow dr in dt.Rows)
            {
                resurlts.Add(new DesignCheckDetailGetOutModel
                {
                    開催日 = Convert.ToDateTime(dr["開催日"]),
                    名称 = Convert.ToString(dr["名称"]),
                    //UPDATE START 2021/08/02 杉浦 設計チェック修正対応
                    //指摘NO = Convert.ToInt32(dr["指摘NO"]),
                    指摘NO = string.IsNullOrEmpty(dr["指摘NO"].ToString()) ? null : (int?)Convert.ToInt32(dr["指摘NO"]),
                    試作管理NO = string.IsNullOrEmpty(dr["試作管理NO"].ToString()) ? null : (int?)Convert.ToInt32(dr["試作管理NO"]),
                    //UPDATE End 2021/08/02 杉浦  設計チェック修正対応
                    ステータス = Convert.ToString(dr["ステータス"]),
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
                    担当課名 = Convert.ToString(dr["担当課名"]),
                    担当者_ID = Convert.ToString(dr["担当者_ID"]),
                    担当者名 = Convert.ToString(dr["担当者名"]),
                    担当者_TEL = Convert.ToString(dr["担当者_TEL"]),
                    試験車名 = Convert.ToString(dr["試験車名"]),
                    状況記号 = Convert.ToString(dr["状況記号"]),
                    指摘ID = Convert.ToInt32(dr["指摘ID"]),
                    試験車_ID = dr["試験車_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["試験車_ID"]),
                });
            }

            return Ok(base.GetResponse(resurlts));
        }
    }
}