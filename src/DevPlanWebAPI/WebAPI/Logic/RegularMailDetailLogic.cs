using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// トラック予約メール情報ロジッククラス
    /// </summary>
    public class RegularMailDetailLogic : BaseLogic
    {
        /// <summary>
        /// 定期便メール内容取得
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public List<RegularMailDetailModel> GetRegularMailDetailList(RegularMailDetailSearchModel cond)
        {
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":TRACK_ID", Type = OracleDbType.Int32, Object = cond.TrackId, Direct = ParameterDirection.Input },
                new BindModel { Name = ":予約種別", Type = OracleDbType.Varchar2, Object = cond.RegularType, Direct = ParameterDirection.Input }
            };

            return db.ReadModelList<RegularMailDetailModel>(@"
SELECT 件名,本文 
FROM トラック_定期便_メール内容 
WHERE TRACK_ID = :TRACK_ID AND 予約種別 = :予約種別 ORDER BY ID", prms);
        }

        /// <summary>
        /// 定期便メール内容更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateRegularMailDetail(RegularMailDetailModel model)
        {
            if (model == null) { return false; }

            var results = new List<bool>();

            {
                var sql = @"
            DELETE FROM トラック_定期便_メール内容 
            WHERE
              予約種別 = :予約種別 AND
              TRACK_ID = :TRACK_ID
            ";
                var prms = new List<BindModel>
                        {
                            new BindModel { Name = ":TRACK_ID", Type = OracleDbType.Varchar2, Object = model.TRACK_ID, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":予約種別", Type = OracleDbType.Varchar2, Object = model.予約種別, Direct = ParameterDirection.Input }
                        };

                results.Add(db.UpdateData(sql, prms));
            }

            {
                var sql = @"
INSERT 
INTO トラック_定期便_メール内容(ID, TRACK_ID, 予約種別, 件名, 本文) 
VALUES ((SELECT MAX(ID) + 1 FROM トラック_定期便_メール内容), :TRACK_ID, :予約種別, :件名, :本文)
            ";

                var maxLength = 1000;
                var length = (int)Math.Ceiling((double)model.本文.Length / maxLength);//切り上げ

                var mailContentList = new List<string>();

                for (int i = 0; i < length; i++)
                {
                    int startIndex = maxLength * i;
                    if (model.本文.Length <= startIndex)
                    {
                        break;
                    }
                    if (model.本文.Length < startIndex + maxLength)
                    {
                        mailContentList.Add(model.本文.Substring(startIndex));
                    }
                    else
                    {
                        mailContentList.Add(model.本文.Substring(startIndex, maxLength));
                    }
                }

                foreach (var content in mailContentList)
                {
                    var prms = new List<BindModel>
                        {
                            new BindModel { Name = ":TRACK_ID", Type = OracleDbType.Varchar2, Object = model.TRACK_ID, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":予約種別", Type = OracleDbType.Varchar2, Object = model.予約種別, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":件名", Type = OracleDbType.Varchar2, Object = model.件名, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":本文", Type = OracleDbType.Varchar2, Object = content, Direct = ParameterDirection.Input },
                        };

                    results.Add(db.UpdateData(sql, prms));
                }
            }

            db.Begin();
            var flg = results.All(x => x == true);
            if (flg == true)
            {
                db.Commit();
            }
            else
            {
                db.Rollback();
            }

            return flg;
        }

        /// <summary>
        /// メール特殊文字取得
        /// </summary>
        /// <returns></returns>
        public List<RegularMailDetailMasterModel> GetRegularMailDetailMasterList()
        {
            return db.ReadModelList<RegularMailDetailMasterModel>(
                "SELECT 文字列 FROM トラック_定期便_メール特殊文字 ORDER BY ID", null);
        }
    }
}