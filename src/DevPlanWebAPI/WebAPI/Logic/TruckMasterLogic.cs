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
    /// トラックマスタ系データ取得ロジッククラス
    /// </summary>
    public class TruckMasterLogic : BaseLogic
    {
        /// <summary>
        /// よく使う目的地一覧取得処理。
        /// </summary>
        /// <returns></returns>
        public List<FrequentlyUsedDestinationsModel> GetFrequentlyUsedDestinationsList()
        {
            return db.ReadModelList<FrequentlyUsedDestinationsModel>(
                "SELECT ルート,ルート名 FROM トラック_ルート候補 ORDER BY SORT_NO", null);
        }

        /// <summary>
        /// 定時間日検索処理
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public List<FixedTimeDaySettingModel> FixedTimeDaySetting(FixedTimeDaySettingSearchModel cond)
        {
            var prms = new List<BindModel>();

            string sql = @"
SELECT
  ""DATE""
  , 時間帯
  , トラック_ID
  , FLAG_定時間日
  , FLAG_運休日
FROM
  トラック_定期便休止時間帯
WHERE
  (時間帯 IS NOT NULL OR FLAG_運休日 = '1')
  AND :START_DATE <= トラック_定期便休止時間帯.""DATE""
  AND トラック_定期便休止時間帯.""DATE"" <= :END_DATE
";
            if (cond.トラック_ID > 0)
            {
                sql += @"
AND トラック_ID = :トラック_ID
";
                prms.Add(new BindModel
                {
                    Name = ":トラック_ID",
                    Type = OracleDbType.Int32,
                    Object = cond.トラック_ID,
                    Direct = ParameterDirection.Input
                });
            }

            prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = cond.START_DATE, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = cond.END_DATE, Direct = ParameterDirection.Input });
            
            return db.ReadModelList<FixedTimeDaySettingModel>(sql, prms);
        }
        
        /// <summary>
        /// 定時間日更新処理
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateFixedTimeDaySetting(List<FixedTimeDaySettingModel> list)
        {
            var results = new List<bool>();

            db.Begin();

            if (list != null)
            {
                foreach (var model in list)
                {
                    string sql = @"
MERGE INTO
    トラック_定期便休止時間帯 A
USING
    (
        SELECT
             :KeyDate AS ""DATE""
            ,:時間帯 AS 時間帯
            ,:トラック_ID AS トラック_ID
            ,:FLAG_定時間日 AS FLAG_定時間日
            ,:FLAG_運休日 AS FLAG_運休日
        FROM
            DUAL
    ) B
ON
    (0 = 0
        AND A.""DATE"" = B.""DATE""
        AND A.トラック_ID = B.トラック_ID
    )
WHEN MATCHED THEN
    UPDATE SET
         時間帯 = B.時間帯
        ,FLAG_定時間日 = B.FLAG_定時間日
        ,FLAG_運休日 = B.FLAG_運休日
WHEN NOT MATCHED THEN
    INSERT
    (
         ""DATE""
        ,時間帯
        ,トラック_ID
        ,FLAG_定時間日
        ,FLAG_運休日
    )
    VALUES
    (
         B.""DATE""
        ,B.時間帯
        ,B.トラック_ID
        ,B.FLAG_定時間日
        ,B.FLAG_運休日
    )
";
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":KeyDate", Type = OracleDbType.Date, Object = model.DATE, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":トラック_ID", Type = OracleDbType.Int32, Object = model.トラック_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":時間帯", Type = OracleDbType.Varchar2, Object = model.時間帯, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":FLAG_運休日", Type = OracleDbType.Int32, Object = model.FLAG_運休日, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":FLAG_定時間日", Type = OracleDbType.Int32, Object = model.FLAG_定時間日, Direct = ParameterDirection.Input }
                    };

                    results.Add(db.UpdateData(sql, prms));
                }
            }

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
        /// 定時間日削除処理
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteFixedTimeDay(List<FixedTimeDaySettingModel> list)
        {
            var results = new List<bool>();

            db.Begin();

            if (list != null)
            {
                foreach (var model in list)
                {
                    string sql = @"
DELETE 
FROM
  トラック_定期便休止時間帯
WHERE
  ""DATE"" = :KeyDate 
  AND トラック_ID = :トラック_ID
";
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":KeyDate", Type = OracleDbType.Date, Object = model.DATE, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":トラック_ID", Type = OracleDbType.Int32, Object = model.トラック_ID, Direct = ParameterDirection.Input }
                    };

                    results.Add(db.DeleteData(sql.ToString(), prms));
                }
            }

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
        /// トラック予約行き先候補マスタ取得処理。
        /// </summary>
        /// <returns></returns>
        public List<TruckSectionModel> GetTruckSection()
        {
            return db.ReadModelList<TruckSectionModel>(
    "SELECT 行き先 FROM トラック_行き先候補 ORDER BY SORT_NO", null);
        }

        /// <summary>
        /// トラック予約コメントデータ取得処理
        /// </summary>
        /// <returns></returns>
        public List<TruckCommentModel> GetTruckComment()
        {
            return db.ReadModelList<TruckCommentModel>(
    "SELECT ID,コメント種別,コメント,タイトル,定型文 FROM トラック_コメント", null);
        }

        /// <summary>
        /// 定期便時間帯マスタ取得処理
        /// </summary>
        /// <returns></returns>
        public List<TruckRegularTimeModel> GetTruckRegularTimeMst()
        {
            var prms = new List<BindModel>();

            string sql = @"
SELECT
  TIMES.REGULAR_ID,
  TIMES.TIME_ID,
  TIMES.DEPARTURE_TIME,
  TIMES.IS_RESERVATION,
  MST.COLOR_CODE_R,
  MST.COLOR_CODE_G,
  MST.COLOR_CODE_B,
  MST.FONT_COLOR_CODE_R,
  MST.FONT_COLOR_CODE_G,
  MST.FONT_COLOR_CODE_B,
  MST.DISPLAY_NAME
FROM
  TRUCK_REGULAR_TIME_MST TIMES
  INNER JOIN TRUCK_REGULAR_MST MST 
    ON TIMES.REGULAR_ID = MST.ID
ORDER BY TIMES.REGULAR_ID,TIMES.TIME_ID
";
            var data = db.ReadModelList<TruckRegularTimeModel>(sql, prms);
            var checkList = new List<TruckRegularTimeModel>(data);
            
            List<int> timeHeaderList = new List<int> { 6, 8, 10, 12, 14, 16, 18, 20, 22 };

            // TODO 共通でエラーログ出力できる機構が整えばエラーログ出力が良し
            foreach (var group in checkList.GroupBy(x => x.REGULAR_ID))
            {
                if (group.Count() != 9)
                {
                    data.RemoveAll(x => x.REGULAR_ID == group.Key);
                }
            }
            foreach (var time in checkList)
            {
                if (timeHeaderList.Contains(time.TIME_ID) == false)
                {
                    data.RemoveAll(x => x.REGULAR_ID == time.REGULAR_ID);
                }
            }
            return data;
        }
    }
}