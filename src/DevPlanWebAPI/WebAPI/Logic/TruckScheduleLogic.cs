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
    /// トラック予約ロジッククラス
    /// </summary>
    public class TruckScheduleLogic : BaseLogic
    {
        /// <summary>
        /// 項目の並び順更新
        /// </summary>
        /// <returns></returns>
        public bool UpdateScheduleItemSortNo()
        {
            var prms = new List<BindModel>();

            var sql = @"
MERGE 
INTO トラック_情報 UPDATE_TBL 
  USING ( 
    SELECT
      ID
      , ROW_NUMBER() OVER (ORDER BY SORT_NO) NEW_NUMBER 
    FROM
      トラック_情報
  ) NEW_TBL 
    ON (UPDATE_TBL.ID = NEW_TBL.ID) WHEN MATCHED THEN UPDATE 
SET
  UPDATE_TBL.SORT_NO = NEW_TBL.NEW_NUMBER
";
            return db.UpdateData(sql.ToString(), null);

        }

        /// <summary>
        /// トラックスケジュール数取得処理
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public List<TruckScheduleCountModel> GetScheduleCount(TruckScheduleSearchModel cond)
        {
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":TruckId", Type = OracleDbType.Int32, Object = cond.TruckId, Direct = ParameterDirection.Input }
            };

            return db.ReadModelList<TruckScheduleCountModel>(@"
SELECT
  COUNT(*) SCHEDULE_COUNT
FROM
  トラック_予約状況 
WHERE
  トラック_ID = :TruckId
", prms);
        }

        /// <summary>
        /// 項目SerialNumber取得処理
        /// </summary>
        /// <returns></returns>
        public List<TruckScheduleItemModel> GetScheduleItem()
        {
            return db.ReadModelList<TruckScheduleItemModel>(@"
SELECT
  MIN(SERIAL_NUMBER + 1) AS SERIAL_NUMBER 
FROM
  トラック_情報 
WHERE
  SERIAL_NUMBER + 1 NOT IN ( 
    SELECT
      SERIAL_NUMBER 
    FROM
      トラック_情報 
    WHERE
      SERIAL_NUMBER IS NOT NULL
  ) 
", null);
        }

        /// <summary>
        /// トラック予約項目取得処理
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public List<TruckScheduleItemModel> GetTruckScheduleItem(TruckScheduleItemSearchModel cond)
        {
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = cond.ID, Direct = ParameterDirection.Input }                
            };

            #region トラック予約項目SQL

            string sql = @"
SELECT
    ITEM.ID,
    車両名,
    保管場所,
    FLAG_定期便,
    種類,
    備考,
    登録番号,
    FLAG_ディーゼル規制,
    始発場所,
    SORT_NO,
    予約可能開始日,
	COLOR.COLOR_CODE_R,
	COLOR.COLOR_CODE_G,
	COLOR.COLOR_CODE_B,
	COLOR.FONT_COLOR_CODE_R,
	COLOR.FONT_COLOR_CODE_G,
	COLOR.FONT_COLOR_CODE_B,
	REGULAR_TIME_ID,
    FLAG_CHECK,
    SERIAL_NUMBER
FROM
  トラック_情報 ITEM 
  LEFT JOIN TRUCK_REGULAR_MST COLOR 
    ON ITEM.REGULAR_TIME_ID = COLOR.ID
WHERE
    0 = 0
";
            #endregion

            if (cond.ID > 0)
            {
                sql += @"
AND ITEM.ID = :ID
";
            }

            if(cond.BEFORE_DATE != null)
            {
                sql += @"
AND 予約可能開始日 <= :BEFORE_DATE
";
                prms.Add(new BindModel { Name = ":BEFORE_DATE", Type = OracleDbType.Date, Object = cond.BEFORE_DATE, Direct = ParameterDirection.Input });                
            }

            if (cond.AFTER_DATE != null)
            {
                sql += @"
AND (予約可能開始日 > :AFTER_DATE OR 予約可能開始日 IS NULL)
";
                prms.Add(new BindModel { Name = ":AFTER_DATE", Type = OracleDbType.Date, Object = cond.AFTER_DATE, Direct = ParameterDirection.Input });
            }

            if (cond.IsRegular)
            {
                sql += @"
AND FLAG_定期便 = :FLAG_定期便
";
                prms.Add(new BindModel { Name = ":FLAG_定期便", Type = OracleDbType.Int32, Object = 1, Direct = ParameterDirection.Input });
            }
            
            if (cond.EMPTY_START_DATE != null && cond.EMPTY_END_DATE != null)
            {
                sql += @"
AND NOT EXISTS
                (
                    SELECT
                        *
                    FROM
                    (
                    SELECT
                      YOYAKU.*
                      , CASE 
                        WHEN TIMES.DEPARTURE_TIME IS NOT NULL 
                          THEN TO_DATE( 
                          CONCAT( 
                            TO_CHAR(YOYAKU.予約開始時間, 'YYYY/MM/DD ')
                            , TIMES.DEPARTURE_TIME
                          ),'YYYY/MM/DD HH24:MI:SS'
                        ) 
                        ELSE YOYAKU.予約開始時間 
                        END 開始時間 
                    FROM
                      トラック_予約状況 YOYAKU 
                      INNER JOIN トラック_情報 ITEM 
                        ON YOYAKU.トラック_ID = ITEM.ID 
                      LEFT JOIN TRUCK_REGULAR_TIME_MST TIMES 
                        ON ITEM.REGULAR_TIME_ID = TIMES.REGULAR_ID 
                        AND TIMES.TIME_ID = TO_NUMBER(TO_CHAR(YOYAKU.予約開始時間, 'HH24'))
                    ) L
                    WHERE 0 = 0
                        AND L.修正者_ID != :修正者ID
                        AND ITEM.ID = L.トラック_ID
                        AND
                            (
                                (L.開始時間 < :START_DATE AND :START_DATE < L.予約終了時間)
                                OR
                                (L.開始時間 < :END_DATE AND :END_DATE < L.予約終了時間)
                                OR
                                (:START_DATE < L.開始時間 AND L.開始時間 < :END_DATE)
                                OR
                                (:START_DATE < L.予約終了時間 AND L.予約終了時間 < :END_DATE)
                                OR
                                (L.開始時間 = :START_DATE AND L.予約終了時間 = :END_DATE)
                            )
                )
";
                prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = cond.EMPTY_START_DATE, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = cond.EMPTY_END_DATE, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":修正者ID", Type = OracleDbType.Varchar2, Object = cond.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input });
            }

            if (cond.MinSortNo != null)
            {
                sql += @"
AND SORT_NO >= :MinSortNo
";
                prms.Add(new BindModel { Name = ":MinSortNo", Type = OracleDbType.Double, Object = cond.MinSortNo, Direct = ParameterDirection.Input });
            }
            if (cond.MaxSortNo != null)
            {
                sql += @"
AND SORT_NO <= :MaxSortNo
";
                prms.Add(new BindModel { Name = ":MaxSortNo", Type = OracleDbType.Double, Object = cond.MaxSortNo, Direct = ParameterDirection.Input });
            }
            if (cond.REGULAR_TIME_ID != null)
            {
                sql += @"
AND REGULAR_TIME_ID = :REGULAR_TIME_ID
";
                prms.Add(new BindModel { Name = ":REGULAR_TIME_ID", Type = OracleDbType.Int16, Object = cond.REGULAR_TIME_ID, Direct = ParameterDirection.Input });
            }

            sql += @"
ORDER BY SORT_NO
";

            var itemList = db.ReadModelList<TruckScheduleItemModel>(sql, prms);

            List<string> txt = new List<string>();

            for (int j = 9312; j <= 9331; j++)
            {
                txt.Add(Convert.ToString(j, 16));
            }

            foreach (var item in itemList.Where(x => x.SERIAL_NUMBER > 0))
            {
                var value = txt[item.SERIAL_NUMBER - 1];
                item.Serial = char.ConvertFromUtf32(Convert.ToInt32(value, 16));
            }

            return itemList;
        }

        /// <summary>
        /// トラック予約項目登録処理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">トラック予約定期便用連番</param>
        /// <returns></returns>
        public bool InsertItem(TruckScheduleItemModel model, int code = 0)
        {
            var results = new List<bool>();

            db.Begin();

            if (model != null)
            {
                var itemId = GetMaxItemId().First().ID;

                #region トラック項目Insert

                string sql = @"
INSERT 
INTO トラック_情報( 
  ID
  , NO
  , 車両名
  , 保管場所
  , FLAG_定期便
  , FLAG_予約可
  , 種類
  , 備考
  , 登録番号
  , FLAG_ディーゼル規制
  , 始発場所
  , SORT_NO
  , 予約可能開始日
  , REGULAR_TIME_ID
  , FLAG_CHECK
  , SERIAL_NUMBER
) 
VALUES ( 
  :ID
  , (SELECT MAX(NO) + 1 FROM トラック_情報)             --旧システム用固定
  , :車両名
  , :保管場所
  , :FLAG_定期便
  , 1                                             --旧システム用固定
  , :種類
  , :備考
  , :登録番号
  , :FLAG_ディーゼル規制
  , :始発場所
  , :SORT_NO
  , :予約可能開始日
  , :REGULAR_TIME_ID
  , :FLAG_CHECK
  , :SERIAL_NUMBER
) 
";
                #endregion
                
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = itemId, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":車両名", Type = OracleDbType.Varchar2, Object = model.車両名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":保管場所", Type = OracleDbType.Varchar2, Object = model.保管場所, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_定期便", Type = OracleDbType.Int32, Object = model.FLAG_定期便, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":種類", Type = OracleDbType.Varchar2, Object = model.種類, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":備考", Type = OracleDbType.Varchar2, Object = model.備考, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":登録番号", Type = OracleDbType.Varchar2, Object = model.登録番号, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_ディーゼル規制", Type = OracleDbType.Int32, Object = model.FLAG_ディーゼル規制, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":始発場所", Type = OracleDbType.Varchar2, Object = model.始発場所, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SORT_NO", Type = OracleDbType.Double, Object = model.SORT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約可能開始日", Type = OracleDbType.Date, Object = model.予約可能開始日, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_CHECK", Type = OracleDbType.Int32, Object = model.FLAG_CHECK, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SERIAL_NUMBER", Type = OracleDbType.Int32, Object = code, Direct = ParameterDirection.Input }
                };

                if (model.REGULAR_TIME_ID > 0)
                {
                    prms.Add(new BindModel { Name = ":REGULAR_TIME_ID", Type = OracleDbType.Int32, Object = model.REGULAR_TIME_ID, Direct = ParameterDirection.Input });
                }
                else
                {
                    prms.Add(new BindModel { Name = ":REGULAR_TIME_ID", Type = OracleDbType.Int32, Object = null, Direct = ParameterDirection.Input });
                }

                results.Add(db.InsertData(sql.ToString(), prms));

                var mailComments = GetTruckMailComment();

                #region メール内容

                sql = @"
INSERT 
INTO トラック_定期便_メール内容
(ID, TRACK_ID, 予約種別, 件名, 本文) 
VALUES
((SELECT MAX(ID) + 1 FROM トラック_定期便_メール内容), :TRACK_ID, :予約種別, :件名, :本文)
";
                #endregion

                {
                    var kariList = mailComments.Where(x => x.種別 == "仮").OrderBy(x => x.ID);

                    foreach (var item in kariList.Where(x => x.Content != ""))
                    {
                        prms = new List<BindModel>
                        {
                            new BindModel { Name = ":TRACK_ID", Type = OracleDbType.Int32, Object = itemId, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":予約種別", Type = OracleDbType.Varchar2, Object = item.種別, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":件名", Type = OracleDbType.Varchar2, Object =
                            kariList.Where(x=>x.Subject != "").First().Subject, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":本文", Type = OracleDbType.Varchar2, Object =
                            item.Content, Direct = ParameterDirection.Input }
                        };

                        results.Add(db.InsertData(sql.ToString(), prms));
                    }
                }

                {
                    var honList = mailComments.Where(x => x.種別 == "本").OrderBy(x => x.ID);

                    foreach (var item in honList.Where(x => x.Content != ""))
                    {
                        prms = new List<BindModel>
                        {
                            new BindModel { Name = ":TRACK_ID", Type = OracleDbType.Int32, Object = itemId, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":予約種別", Type = OracleDbType.Varchar2, Object = item.種別, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":件名", Type = OracleDbType.Varchar2, Object =
                            honList.Where(x=>x.Subject != "").First().Subject, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":本文", Type = OracleDbType.Varchar2, Object =
                            item.Content, Direct = ParameterDirection.Input }
                        };

                        results.Add(db.InsertData(sql.ToString(), prms));
                    }
                }

                results.Add(UpdateScheduleItemSortNo());
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
        /// トラック予約項目更新
        /// </summary>
        /// <returns></returns>
        public bool UpdateItem(List<TruckScheduleItemModel> list)
        {
            var results = new List<bool>();

            db.Begin();

            if (list != null)
            {
                string sql = @"
UPDATE トラック_情報 
SET
    車両名 = :車両名
  , 保管場所 = :保管場所
  , FLAG_定期便 = :FLAG_定期便
  , 種類 = :種類
  , 備考 = :備考
  , 登録番号 = :登録番号
  , FLAG_ディーゼル規制 = :FLAG_ディーゼル規制
  , 始発場所 = :始発場所
  , 予約可能開始日 = :予約可能開始日
  , REGULAR_TIME_ID = :REGULAR_TIME_ID
  , SORT_NO = :SORT_NO
  , FLAG_CHECK = : FLAG_CHECK
WHERE
  ID = :ID
";
                foreach (var model in list)
                {
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = model.ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":車両名", Type = OracleDbType.Varchar2, Object = model.車両名, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":保管場所", Type = OracleDbType.Varchar2, Object = model.保管場所, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":FLAG_定期便", Type = OracleDbType.Int32, Object = model.FLAG_定期便, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":種類", Type = OracleDbType.Varchar2, Object = model.種類, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":備考", Type = OracleDbType.Varchar2, Object = model.備考, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":登録番号", Type = OracleDbType.Varchar2, Object = model.登録番号, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":FLAG_ディーゼル規制", Type = OracleDbType.Int32, Object = model.FLAG_ディーゼル規制, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":始発場所", Type = OracleDbType.Varchar2, Object = model.始発場所, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":SORT_NO", Type = OracleDbType.Double, Object = model.SORT_NO, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":FLAG_CHECK", Type = OracleDbType.Int32, Object = model.FLAG_CHECK, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":予約可能開始日", Type = OracleDbType.Date, Object = model.予約可能開始日, Direct = ParameterDirection.Input }
                    };

                    if (model.REGULAR_TIME_ID > 0)
                    {
                        prms.Add(new BindModel { Name = ":REGULAR_TIME_ID", Type = OracleDbType.Int32, Object = model.REGULAR_TIME_ID, Direct = ParameterDirection.Input });
                    }
                    else
                    {
                        prms.Add(new BindModel { Name = ":REGULAR_TIME_ID", Type = OracleDbType.Int32, Object = null, Direct = ParameterDirection.Input });
                    }

                    results.Add(db.UpdateData(sql.ToString(), prms));
                    results.Add(UpdateScheduleItemSortNo());
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
        /// トラック予約項目削除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteItem(TruckScheduleItemModel model)
        {
            var results = new List<bool>();

            db.Begin();

            if (model != null)
            {
                {
                    string sql = @"
DELETE 
FROM
  トラック_情報
WHERE
  ID = :ID
";
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = model.ID, Direct = ParameterDirection.Input }
                    };

                    results.Add(db.DeleteData(sql.ToString(), prms));
                }

                {
                    string sql = @"
DELETE 
FROM
  トラック_定期便ルート
WHERE
  TRACK_ID = :ID
";
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = model.ID, Direct = ParameterDirection.Input }
                    };

                    results.Add(db.DeleteData(sql.ToString(), prms));
                }

                {
                    string sql = @"
DELETE 
FROM
  トラック_定期便_メール内容
WHERE
  TRACK_ID = :ID
";
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = model.ID, Direct = ParameterDirection.Input }
                    };

                    results.Add(db.DeleteData(sql.ToString(), prms));
                }

                {
                    string sql = @"
DELETE 
FROM
  トラック_定期便休止時間帯
WHERE
  トラック_ID = :ID
";
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = model.ID, Direct = ParameterDirection.Input }
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
        /// トラック予約メール定型文取得処理
        /// </summary>
        /// <returns></returns>
        private List<TruckCommentModel> GetTruckMailComment()
        {
            return db.ReadModelList<TruckCommentModel>(
    @"
select
  ID,
  コメント種別,
  定型文
from
  トラック_コメント 
where
  コメント種別 = '定期便メール（仮）件名' 
  OR コメント種別 = '定期便メール（仮）本文' 
  OR コメント種別 = '定期便メール（本）件名' 
  OR コメント種別 = '定期便メール（本）本文'
", null);
        }

        /// <summary>
        /// トラック予約項目最新番号取得
        /// </summary>
        /// <returns></returns>
        private List<TruckScheduleItemModel> GetMaxItemId()
        {
            return db.ReadModelList<TruckScheduleItemModel>(
    @"
SELECT MAX(ID) + 1 ID FROM トラック_情報
", null);
        }

        /// <summary>
        /// トラック予約スケジュール取得処理
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TruckScheduleDetailsModel> GetTruckSchedule(TruckScheduleSearchModel cond)
        {
            var prms = new List<BindModel>();

            #region トラック予約スケジュール取得SQL
            string sql = @"
SELECT
  YOYAKU.ID
  , トラック_ID
  , 使用目的
  , 予約開始時間
  , 予約終了時間
  , 空き時間状況
  , 予約者_ID
  , 修正者_ID
  , ""予約・修正日時"" 予約修正日時
  , 運転者A_ID
  , 運転者B_ID
  , 運転者A_TEL
  , 運転者B_TEL
  , FLAG_仮予約
  , 定期便依頼者_ID
  , 定期便依頼者_TEL
  , YOYAKU.FLAG_定期便
  , 搬送車両名
  , YOYAKU.備考
  , 予約者名
  , 運転者A名
  , 運転者B名
  , 定期便依頼者名
  , 修正者名
  , PARALLEL_INDEX_GROUP
  , FLAG_機密車
  , COLOR.COLOR_CODE_R
  , COLOR.COLOR_CODE_G
  , COLOR.COLOR_CODE_B
  , COLOR.FONT_COLOR_CODE_R
  , COLOR.FONT_COLOR_CODE_G
  , COLOR.FONT_COLOR_CODE_B
  , ITEM.車両名
  , TIMES.DEPARTURE_TIME
FROM
  トラック_予約状況 YOYAKU
  INNER JOIN トラック_情報 ITEM
    ON YOYAKU.トラック_ID = ITEM.ID
  LEFT JOIN TRUCK_REGULAR_MST COLOR
    ON ITEM.REGULAR_TIME_ID = COLOR.ID
  LEFT JOIN TRUCK_REGULAR_TIME_MST TIMES
    ON ITEM.REGULAR_TIME_ID = TIMES.REGULAR_ID
    AND TIMES.TIME_ID = TO_NUMBER(TO_CHAR(YOYAKU.予約開始時間, 'HH24'))
WHERE
  0 = 0
";
            #endregion

            sql = SetWhere(cond, prms, sql);
            sql += @"
ORDER BY 予約開始時間
";
            var ret = db.ReadModelList<TruckScheduleDetailsModel>(sql, prms);

            return ret;
        }


        /// <summary>
        /// トラック予約発送者受領者取得処理
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TruckShipperRecipientUserModel> GetTruckShipperRecipientUser(TruckScheduleSearchModel cond)
        {
            var prms = new List<BindModel>();

            #region トラック予約発送者受領者取得SQL

            string sql = @"
SELECT
  USERS.ID
  , 予約_ID
  , 発送者_ID
  , 発送者_TEL
  , 受領者_ID
  , 受領者_TEL
  , SORT_NO
  , 発送者名
  , 受領者名 
FROM
  トラック_定期便発送者受領者 USERS 
  INNER JOIN トラック_予約状況 YOYAKU 
    ON USERS.予約_ID = YOYAKU.ID
WHERE
    0 = 0
";
            #endregion

            sql = SetWhere(cond, prms, sql);
            sql += @"
ORDER BY SORT_NO
";
            return db.ReadModelList<TruckShipperRecipientUserModel>(sql, prms);
        }

        /// <summary>
        /// トラック予約発着地取得処理
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public List<TruckScheduleSectionModel> GetSection(TruckScheduleSearchModel cond)
        {
            var prms = new List<BindModel>();

            #region トラック予約発着地取得SQL

            string sql = @"
SELECT
  SECTIONS.ID
  , SECTIONS.予約_ID
  ,SECTIONS.発着地
  ,SECTIONS.FLAG_空荷
  ,SECTIONS.SORT_NO
FROM
  トラック_発着地 SECTIONS 
  INNER JOIN トラック_予約状況 YOYAKU 
    ON SECTIONS.予約_ID = YOYAKU.ID
WHERE
    0 = 0
";
            #endregion

            sql = SetWhere(cond, prms, sql);

            sql += @"
ORDER BY SORT_NO
";
            return db.ReadModelList<TruckScheduleSectionModel>(sql, prms);
        }

        /// <summary>
        /// トラック予約スケジュール条件式構築処理
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="prms"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static string SetWhere(TruckScheduleSearchModel cond, List<BindModel> prms, string sql)
        {
            if (cond.ID > 0)
            {
                sql += @"
AND YOYAKU.ID = :ID
";
                prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = cond.ID, Direct = ParameterDirection.Input });
            }

            if (cond.TruckId != null)
            {
                sql += @"
AND トラック_ID = :トラック_ID
";
                prms.Add(new BindModel { Name = ":トラック_ID", Type = OracleDbType.Int32, Object = cond.TruckId, Direct = ParameterDirection.Input });
            }

            if (cond.IsRegular)
            {
                sql += @"
AND YOYAKU.FLAG_定期便 = :FLAG_定期便
";
                prms.Add(new BindModel { Name = ":FLAG_定期便", Type = OracleDbType.Int32, Object = 1, Direct = ParameterDirection.Input });
            }

            if (cond.PARALLEL_INDEX_GROUP != null)
            {
                sql += @"
AND YOYAKU.PARALLEL_INDEX_GROUP = :PARALLEL_INDEX_GROUP
";
                prms.Add(new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = cond.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input });
            }

            var startDate = cond.START_DATE == null ? null : (DateTime?)cond.START_DATE.Value.Date;
            var endDate = cond.END_DATE == null ? null : (DateTime?)cond.END_DATE.Value.Date;
            if (cond.START_DATE != null && cond.END_DATE != null)
            {
                sql += @"
AND
    (
        :START_DATE BETWEEN 予約開始時間 AND 予約終了時間
        OR
        :END_DATE BETWEEN 予約開始時間 AND 予約終了時間
        OR
        予約開始時間 BETWEEN :START_DATE AND (:END_DATE + 1)
        OR
        予約終了時間 BETWEEN :START_DATE AND (:END_DATE + 1)
)";
                prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

            }
            else
            {
                if (cond.START_DATE != null)
                {
                    sql += @"
AND
    (
        :START_DATE BETWEEN 予約開始時間 AND 予約終了時間
        OR
        予約開始時間 >= :START_DATE
    )";
                    prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input });

                }

                if (cond.END_DATE != null)
                {
                    sql += @"
AND
    (
        :END_DATE BETWEEN 予約開始時間 AND 予約終了時間
        OR
        予約終了時間 < (:END_DATE + 1)
)";
                    prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

                }

            }

            if (cond.IsGetKettei)
            {
                sql += @"
AND YOYAKU.FLAG_仮予約 != 1
AND YOYAKU.FLAG_定期便 = 1
";
            }

            return sql;
        }

        /// <summary>
        /// トラック予約スケジュール登録処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertSchedule(TruckScheduleModel model)
        {
            var results = new List<bool>();

            db.Begin();

            if (model != null)
            {
                #region トラック予約状況Insert

                string sql = @"
INSERT 
INTO トラック_予約状況( 
  ID
  , トラック_ID
  , 使用目的
  , 予約開始時間
  , 予約終了時間
  , 空き時間状況
  , 予約者_ID
  , 修正者_ID
  , ""予約・修正日時""
  , 運転者A_ID
  , 運転者B_ID
  , 運転者A_TEL
  , 運転者B_TEL
  , FLAG_仮予約
  , 定期便依頼者_ID
  , 定期便依頼者_TEL
  , FLAG_定期便
  , 搬送車両名
  , 備考
  , 予約者名
  , 運転者A名
  , 運転者B名
  , 定期便依頼者名
  , 修正者名
  , PARALLEL_INDEX_GROUP
  , FLAG_機密車
) 
VALUES ( 
  :ID
  , :トラック_ID
  , :使用目的
  , :予約開始時間
  , :予約終了時間
  , :空き時間状況
  , :予約者_ID
  , :修正者_ID
  , :予約修正日時
  , :運転者A_ID
  , :運転者B_ID
  , :運転者A_TEL
  , :運転者B_TEL
  , :FLAG_仮予約
  , :定期便依頼者_ID
  , :定期便依頼者_TEL
  , :FLAG_定期便
  , :搬送車両名
  , :備考
  , :予約者名
  , :運転者A名
  , :運転者B名
  , :定期便依頼者名
  , :修正者名
  , :PARALLEL_INDEX_GROUP
  , :FLAG_機密車
) 
";

                #endregion

                model.ID = base.GetLogic<CommonLogic>().GetScheduleNewID();

                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = model.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":トラック_ID", Type = OracleDbType.Varchar2, Object = model.トラック_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":使用目的", Type = OracleDbType.Varchar2, Object = model.使用目的, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約開始時間", Type = OracleDbType.Date, Object = model.予約開始時間, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約終了時間", Type = OracleDbType.Date, Object = model.予約終了時間, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":空き時間状況", Type = OracleDbType.Varchar2, Object = model.空き時間状況, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約者_ID", Type = OracleDbType.Varchar2, Object = model.予約者_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":修正者_ID", Type = OracleDbType.Varchar2, Object = model.修正者_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約修正日時", Type = OracleDbType.Date, Object = model.予約修正日時, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者A_ID", Type = OracleDbType.Varchar2, Object = model.運転者A_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者B_ID", Type = OracleDbType.Varchar2, Object = model.運転者B_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者A_TEL", Type = OracleDbType.Varchar2, Object = model.運転者A_TEL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者B_TEL", Type = OracleDbType.Varchar2, Object = model.運転者B_TEL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_仮予約", Type = OracleDbType.Int32, Object = model.FLAG_仮予約, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":定期便依頼者_ID", Type = OracleDbType.Varchar2, Object = model.定期便依頼者_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":定期便依頼者_TEL", Type = OracleDbType.Varchar2, Object = model.定期便依頼者_TEL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_定期便", Type = OracleDbType.Varchar2, Object = model.FLAG_定期便, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":搬送車両名", Type = OracleDbType.Varchar2, Object = model.搬送車両名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":備考", Type = OracleDbType.Varchar2, Object = model.備考, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約者名", Type = OracleDbType.Varchar2, Object = model.予約者名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者A名", Type = OracleDbType.Varchar2, Object = model.運転者A名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者B名", Type = OracleDbType.Varchar2, Object = model.運転者B名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":定期便依頼者名", Type = OracleDbType.Varchar2, Object = model.定期便依頼者名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":修正者名", Type = OracleDbType.Varchar2, Object = model.修正者名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = model.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_機密車", Type = OracleDbType.Int32, Object = model.FLAG_機密車, Direct = ParameterDirection.Input }
                };

                results.Add(db.InsertData(sql.ToString(), prms));

                // 発送者受領者
                if (model.ShipperRecipientUserList != null)
                {
                    results = UpdateShipperRecipientUserList(model, results);
                }

                //発着地
                if (model.SectionList != null)
                {
                    results = UpdateSectionList(model, results);
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
        /// 発着地Insert処理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="results"></param>
        /// <param name="prms"></param>
        /// <returns></returns>
        private List<bool> UpdateSectionList(TruckScheduleModel model, List<bool> results)
        {
            #region 発着地SQL

            string sql = @"
INSERT 
INTO トラック_発着地(ID, 予約_ID, 発着地, FLAG_空荷, SORT_NO) 
VALUES ((SELECT MAX(ID) + 1 FROM トラック_発着地), :予約_ID, :発着地, :FLAG_空荷, :SORT_NO)
";
            #endregion

            foreach (var item in model.SectionList)
            {
                //パラメータ
                var prms = new List<BindModel>
                        {
                            new BindModel { Name = ":予約_ID", Type = OracleDbType.Int32, Object = model.ID, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":発着地", Type = OracleDbType.Varchar2, Object = item.発着地, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":FLAG_空荷", Type = OracleDbType.Int32, Object = item.FLAG_空荷, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":SORT_NO", Type = OracleDbType.Int32, Object = item.SORT_NO, Direct = ParameterDirection.Input }
                        };

                results.Add(db.InsertData(sql.ToString(), prms));
            }

            return results;
        }

        /// <summary>
        /// 発送者受領者登録処理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="results"></param>
        /// <param name="prms"></param>
        /// <returns></returns>
        private List<bool> UpdateShipperRecipientUserList(TruckScheduleModel model, List<bool> results)
        {
            #region 発送者受領者SQL

            string sql = @"
INSERT 
INTO トラック_定期便発送者受領者( 
  ID
  , 予約_ID
  , 発送者_ID
  , 発送者_TEL
  , 受領者_ID
  , 受領者_TEL
  , SORT_NO
  , 発送者名
  , 受領者名
) 
VALUES ( 
  (SELECT MAX(ID) + 1 FROM トラック_定期便発送者受領者)
  , :予約_ID
  , :発送者_ID
  , :発送者_TEL
  , :受領者_ID
  , :受領者_TEL
  , :SORT_NO
  , :発送者名
  , :受領者名
) 
";
            #endregion

            foreach (var item in model.ShipperRecipientUserList)
            {
                //パラメータ
                var prms = new List<BindModel>
                        {
                            new BindModel { Name = ":予約_ID", Type = OracleDbType.Int32, Object = model.ID, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":発送者_ID", Type = OracleDbType.Varchar2, Object = item.発送者_ID, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":発送者_TEL", Type = OracleDbType.Varchar2, Object = item.発送者_TEL, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":受領者_ID", Type = OracleDbType.Varchar2, Object = item.受領者_ID, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":受領者_TEL", Type = OracleDbType.Varchar2, Object = item.受領者_TEL, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":SORT_NO", Type = OracleDbType.Varchar2, Object = item.SORT_NO, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":発送者名", Type = OracleDbType.Varchar2, Object = item.発送者名, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":受領者名", Type = OracleDbType.Varchar2, Object = item.受領者名, Direct = ParameterDirection.Input }
                        };

                results.Add(db.InsertData(sql.ToString(), prms));
            }

            return results;
        }

        /// <summary>
        /// トラック予約スケジュール更新処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSchedule(TruckScheduleModel model)
        {
            var results = new List<bool>();

            db.Begin();

            if (model != null)
            {
                string sql = @"
UPDATE トラック_予約状況 
SET
    使用目的 = :使用目的
  , 予約開始時間 = :予約開始時間
  , 予約終了時間 = :予約終了時間
  , 空き時間状況 = :空き時間状況
  , 予約者_ID = :予約者_ID
  , 修正者_ID = :修正者_ID
  , ""予約・修正日時"" = SYSDATE
  , 運転者A_ID = :運転者A_ID
  , 運転者B_ID = :運転者B_ID
  , 運転者A_TEL = :運転者A_TEL
  , 運転者B_TEL = :運転者B_TEL
  , FLAG_仮予約 = :FLAG_仮予約
  , 定期便依頼者_ID = :定期便依頼者_ID
  , 定期便依頼者_TEL = :定期便依頼者_TEL
  , 搬送車両名 = :搬送車両名
  , 備考 = :備考
  , 予約者名 = :予約者名
  , 運転者A名 = :運転者A名
  , 運転者B名 = :運転者B名
  , 定期便依頼者名 = :定期便依頼者名
  , 修正者名 = :修正者名
  , PARALLEL_INDEX_GROUP = :PARALLEL_INDEX_GROUP
  , FLAG_機密車 = :FLAG_機密車 
WHERE
  ID = :ID
";
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = model.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":使用目的", Type = OracleDbType.Varchar2, Object = model.使用目的, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約開始時間", Type = OracleDbType.Date, Object = model.予約開始時間, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約終了時間", Type = OracleDbType.Date, Object = model.予約終了時間, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":空き時間状況", Type = OracleDbType.Varchar2, Object = model.空き時間状況, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約者_ID", Type = OracleDbType.Varchar2, Object = model.予約者_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":修正者_ID", Type = OracleDbType.Varchar2, Object = model.修正者_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者A_ID", Type = OracleDbType.Varchar2, Object = model.運転者A_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者B_ID", Type = OracleDbType.Varchar2, Object = model.運転者B_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者A_TEL", Type = OracleDbType.Varchar2, Object = model.運転者A_TEL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者B_TEL", Type = OracleDbType.Varchar2, Object = model.運転者B_TEL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_仮予約", Type = OracleDbType.Int32, Object = model.FLAG_仮予約, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":定期便依頼者_ID", Type = OracleDbType.Varchar2, Object = model.定期便依頼者_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":定期便依頼者_TEL", Type = OracleDbType.Varchar2, Object = model.定期便依頼者_TEL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":搬送車両名", Type = OracleDbType.Varchar2, Object = model.搬送車両名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":備考", Type = OracleDbType.Varchar2, Object = model.備考, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":予約者名", Type = OracleDbType.Varchar2, Object = model.予約者名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者A名", Type = OracleDbType.Varchar2, Object = model.運転者A名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":運転者B名", Type = OracleDbType.Varchar2, Object = model.運転者B名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":定期便依頼者名", Type = OracleDbType.Varchar2, Object = model.定期便依頼者名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":修正者名", Type = OracleDbType.Varchar2, Object = model.修正者名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PARALLEL_INDEX_GROUP", Type = OracleDbType.Int32, Object = model.PARALLEL_INDEX_GROUP, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_機密車", Type = OracleDbType.Int32, Object = model.FLAG_機密車, Direct = ParameterDirection.Input }
                };

                results.Add(db.UpdateData(sql.ToString(), prms));

                // 発送者受領者
                if (model.ShipperRecipientUserList != null)
                {
                    results = DeleteShipperRecipientUserList(model, results);
                    results = UpdateShipperRecipientUserList(model, results);
                }

                //発着地
                if (model.SectionList != null)
                {
                    results = DeleteSectionList(model, results);
                    results = UpdateSectionList(model, results);
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
        /// 発着地削除処理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private List<bool> DeleteSectionList(TruckScheduleModel model, List<bool> results)
        {
            string sql = @"
DELETE 
FROM
  トラック_発着地 
WHERE
  予約_ID = :予約_ID
";
            var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":予約_ID", Type = OracleDbType.Int32, Object = model.ID, Direct = ParameterDirection.Input }
                    };

            results.Add(db.DeleteData(sql.ToString(), prms));

            return results;
        }

        /// <summary>
        /// 定期便発送者受領者削除処理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private List<bool> DeleteShipperRecipientUserList(TruckScheduleModel model, List<bool> results)
        {
            string sql = @"
DELETE 
FROM
  トラック_定期便発送者受領者 
WHERE
  予約_ID = :予約_ID
";
            var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":予約_ID", Type = OracleDbType.Int32, Object = model.ID, Direct = ParameterDirection.Input }
                    };

            results.Add(db.DeleteData(sql.ToString(), prms));

            return results;
        }

        /// <summary>
        /// トラック予約スケジュール削除処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteSchedule(TruckScheduleModel model)
        {
            var results = new List<bool>();

            db.Begin();

            if (model != null)
            {
                string sql = @"
DELETE
FROM
  トラック_予約状況 
WHERE
  ID = :ID
";
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = model.ID, Direct = ParameterDirection.Input }
                };

                results.Add(db.DeleteData(sql.ToString(), prms));

                // 発送者受領者
                if (model.ShipperRecipientUserList != null)
                {
                    results = DeleteShipperRecipientUserList(model, results);
                }

                //発着地
                if (model.SectionList != null)
                {
                    results = DeleteSectionList(model, results);
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
    }
}