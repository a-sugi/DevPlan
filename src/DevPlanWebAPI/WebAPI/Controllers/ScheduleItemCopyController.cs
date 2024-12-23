//
// 業務計画表システム
// KKA00110 項目コピー・移動（項目コピー）API
// 作成者 : 岸　義将
// 作成日 : 2017/02/10

using System;
using System.Web.Http;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 項目コピー
    /// </summary>
    public class ScheduleItemCopyController : BaseAPIController<ScheduleItemCopyLogic, LoginOutModel>
    {
        /// <summary>
        /// 項目コピー登録
        /// </summary>
        /// <param name="val">入力パラメータ(Json形式で渡す)</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public IHttpActionResult Post([FromBody]ScheduleItemCopyInModel val)
        {
            // 必須パラメータのチェック
            if (string.IsNullOrWhiteSpace(val.GENERAL_CODE))
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03005, "GENERAL_CODE"));
            }

            if (string.IsNullOrWhiteSpace(val.SECTION_GROUP_ID))
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03005, "SECTION_GROUP_ID"));
            }

            if (string.IsNullOrWhiteSpace(val.PERSONEL_ID))
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03005, "PERSONEL_ID"));
            }

            if (val.TABLE_NUMBER < 1 || val.TABLE_NUMBER > 6)
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03017, "TABLE_NUMBER"));
            }

            try
            {
                // コピー元データの取得
                var dt = base.GetLogic().GetSourceItems(val);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dr = dt.Rows[0];

                    var category = Convert.ToString(dr["CATEGORY"]);
                    var flagSeparator = Convert.ToString(dr["FLAG_SEPARATOR"]);
                    var flagClass = Convert.ToString(dr["FLAG_CLASS"]);
                    var rowCount = Convert.ToInt32(dr["PARALLEL_INDEX_GROUP"]);

                    // コピー先に同じ項目名がないかチェック
                    var destData = base.GetLogic().CheckSameDatas(val, category, flagClass);
                    if (destData != null && destData.Rows.Count == 0)
                    {
                        var flg = base.GetLogic().ItemCopy(val, category, flagSeparator, flagClass, rowCount);

                        // DBトランザクション開始
                        db.Begin();

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
                    else
                    {
                        // コピー先に同じ項目が存在している
                        return Ok(base.GetResponse(Common.MessageType.KKE03019));
                    }
                }
                else
                {
                    // コピー元が正しく取得できなかった
                    return Ok(base.GetResponse(Common.MessageType.KKE03018));
                }
            }
            catch
            {
                return Ok(base.GetResponse(Common.MessageType.KKE03001));
            }
        }
    }
}
