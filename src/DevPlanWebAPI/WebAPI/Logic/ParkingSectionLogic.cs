using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Linq;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 駐車場区画ロジッククラス
    /// </summary>
    public class ParkingSectionLogic : BaseLogic
    {
        #region 駐車場区画検索
        /// <summary>
        /// 駐車場区画検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<ParkingModel> Get(ParkingSearchModel cond)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            //Update Start 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
            //sql.AppendLine("     A.\"LOCATION_NO\"");
            //sql.AppendLine("    ,A.\"AREA_NO\"");
            //sql.AppendLine("    ,A.\"SECTION_NO\"");
            //sql.AppendLine("    ,A.\"NAME\"");
            //sql.AppendLine("    ,A.\"STATUS\"");
            //sql.AppendLine("    ,(");
            //sql.AppendLine("        CASE");
            //sql.AppendLine("        WHEN A.\"STATUS\" = 1 THEN '使用中'");
            //sql.AppendLine("        ELSE '空き'");
            //sql.AppendLine("        END");
            //sql.AppendLine("    ) AS \"STATUS_CODE\"");
            //sql.AppendLine("    ,A.\"SORT_NO\"");
            //sql.AppendLine("    ,B.\"データID\"");
            //sql.AppendLine("    ,B.\"管理票NO\"");

            ////Append Start 2021/10/07 矢作
            //sql.AppendLine("    ,C.\"NAME\" AS \"AREA_NAME\"");
            ////Append End 2021/10/07 矢作

            //sql.AppendLine("FROM");
            //sql.AppendLine("        PARKING_SECTION A");
            //sql.AppendLine("    LEFT JOIN");
            //sql.AppendLine("        VIEW_試験車基本情報 B");
            //sql.AppendLine("    ON");
            //sql.AppendLine("        A.NAME = B.駐車場番号");

            ////Append Start 2021/10/07 矢作
            //sql.AppendLine("    LEFT JOIN");
            //sql.AppendLine("        PARKING_AREA C");
            //sql.AppendLine("    ON");
            //sql.AppendLine("        A.AREA_NO = C.AREA_NO AND A.LOCATION_NO = C.LOCATION_NO");
            ////Append End 2021/10/07 矢作
            sql.AppendLine("     A.\"LOCATION_NO\"");
            sql.AppendLine("    ,A.\"AREA_NO\"");
            sql.AppendLine("    ,B.\"SECTION_NO\"");
            sql.AppendLine("    ,B.\"NAME\"");
            sql.AppendLine("    ,B.\"STATUS\"");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("        WHEN B.\"SECTION_NO\" IS NULL THEN NULL");
            sql.AppendLine("        WHEN B.\"STATUS\" = 1 THEN '使用中'");
            sql.AppendLine("        ELSE '空き'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS \"STATUS_CODE\"");
            sql.AppendLine("    ,A.\"SORT_NO\"");
            sql.AppendLine("    ,B.\"SORT_NO\"");
            //Update Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
            //sql.AppendLine("    ,C.\"データID\"");
            //sql.AppendLine("    ,C.\"管理票NO\"");
            sql.AppendLine("    ,CASE WHEN C.\"データID\" IS NULL THEN D.\"データID\" ELSE C.\"データID\" END データID");
            sql.AppendLine("    ,CASE WHEN C.管理票NO IS NULL AND D.\"データID\" IS NOT NULL THEN 'Dummy' ELSE C.管理票NO END 管理票NO");
            //Update End 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
            sql.AppendLine("    ,A.\"NAME\" AS \"AREA_NAME\"");
            sql.AppendLine("FROM");
            sql.AppendLine("        PARKING_AREA A");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        PARKING_SECTION B");
            sql.AppendLine("    ON");
            sql.AppendLine("        A.AREA_NO = B.AREA_NO AND A.LOCATION_NO = B.LOCATION_NO");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        VIEW_試験車基本情報 C");
            sql.AppendLine("    ON");
            sql.AppendLine("        B.NAME = C.駐車場番号");
            //Update End 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
            //Append Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        (SELECT DA.データID, DB.LOCATION_NO,DB.AREA_NO, DB.SECTION_NO FROM DUMMY_TESTCAR DA LEFT JOIN PARKING_USE DB ON DA.データID = DB.データID AND DA.DELETE_FLG <> '1') D");
            sql.AppendLine("    ON");
            sql.AppendLine("        B.SECTION_NO = D.SECTION_NO AND B.AREA_NO = D.AREA_NO AND B.LOCATION_NO = D.LOCATION_NO");
            //Append End 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更

            sql.AppendLine("WHERE 0 = 0");

            //パラメータ
            var prms = new List<BindModel>();

            if (cond.LOCATION_NO != null)
            {
                sql.AppendLine("    AND A.\"LOCATION_NO\" = :LOCATION_NO");
                prms.Add(new BindModel
                {
                    Name = ":LOCATION_NO",
                    Type = OracleDbType.Int64,
                    Object = cond.LOCATION_NO,
                    Direct = ParameterDirection.Input
                });
            }
            if (cond.AREA_NO != null)
            {
                sql.AppendLine("    AND A.\"AREA_NO\" = :AREA_NO");
                prms.Add(new BindModel
                {
                    Name = ":AREA_NO",
                    Type = OracleDbType.Int64,
                    Object = cond.AREA_NO,
                    Direct = ParameterDirection.Input
                });
            }
            if (cond.SECTION_NO != null)
            {
                //Update Start 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
                //sql.AppendLine("    AND A.\"SECTION_NO\" = :SECTION_NO");
                sql.AppendLine("    AND B.\"SECTION_NO\" = :SECTION_NO");
                //Update End 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
                prms.Add(new BindModel
                {
                    Name = ":SECTION_NO",
                    Type = OracleDbType.Int64,
                    Object = cond.SECTION_NO,
                    Direct = ParameterDirection.Input
                });
            }
            if (cond.STATUS != null)
            {
                if (cond.STATUS == 1)
                {
                    //Update Start 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
                    //sql.AppendLine("    AND A.\"STATUS\" = 1");
                    sql.AppendLine("    AND B.\"STATUS\" = 1");
                    //Update End 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
                }
                //Append Start 2021/11/09 杉浦 エリアラジオボタン追加
                else if(cond.STATUS == 2)
                {
                    sql.AppendLine("    AND B.\"STATUS\" IS NULL");
                }
                //Append End 2021/11/09 杉浦 エリアラジオボタン追加
                else
                {
                    //Update Start 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
                    //sql.AppendLine("    AND (A.\"STATUS\" IS NULL OR A.\"STATUS\" <> 1)");
                    sql.AppendLine("    AND ( (B.\"STATUS\" IS NULL OR B.\"STATUS\" <> 1) AND B.\"SECTION_NO\" IS NOT NULL )");
                    //Update End 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
                }
            }

            //並べ替え
            sql.Append("ORDER BY ");
            sql.Append("A.\"LOCATION_NO\"");
            sql.Append(",A.\"AREA_NO\"");
            sql.Append(",A.\"SORT_NO\"");
            //Append Start 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
            sql.Append(",B.\"SORT_NO\"");
            //Append End 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める

            //取得
            return db.ReadModelList<ParkingModel>(sql.ToString(), prms);
        }
        #endregion

        #region 駐車場区画更新
        /// <summary>
        /// 駐車場区画更新
        /// </summary>
        /// <param name="list">駐車場区画項目</param>
        /// <returns>更新可否</returns>
        public bool Put(List<ParkingModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //更新対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //項目更新
                results.Add(UpdateItem(list));
            }

            //Append Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
            //空き⇒使用中にステータス変更する際の追加登録
            var list2 = list.Where(x => x.STATUS == 1 && x.管理票NO == "Dummy").ToList();
            if (list2 != null & list2.Any() == true)
            {
                //項目追加
                results.Add(InsertItem(list2));
            }

            //使用中⇒空きのステータス変更時のデータ削除
            var list3 = list.Where(x => x.STATUS == 0 && x.管理票NO == "Dummy").ToList();
            if (list3 != null && list3.Any() == true)
            {
                //項目削除
                results.Add(DeleteUseItem(list3));
            }
            //Append End 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更

            //更新が成功したかどうか
            var flg = results.All(x => x == true);
            if (flg == true)
            {
                //コミット
                db.Commit();
            }
            else
            {
                //ロールバック
                db.Rollback();
            }

            return flg;
        }

        /// <summary>
        /// 駐車場区画更新
        /// </summary>
        /// <param name="list">駐車場区画項目</param>
        /// <returns>更新可否</returns>
        public bool UpdateItem(List<ParkingModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"PARKING_SECTION\"");
            sql.AppendLine("SET");
            sql.AppendLine("    \"STATUS\" = :STATUS");
            sql.AppendLine("    ,\"INPUT_DATETIME\" = SYSDATE");
            sql.AppendLine("    ,\"INPUT_PERSONEL_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"LOCATION_NO\" = :LOCATION_NO");
            sql.AppendLine("    AND \"AREA_NO\" = :AREA_NO");
            sql.AppendLine("    AND \"SECTION_NO\" = :SECTION_NO");
            var results = new List<bool>();

            foreach (var item in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":STATUS",
                         Type = OracleDbType.Int16,
                         Object = item.STATUS,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":LOCATION_NO",
                         Type = OracleDbType.Int64,
                         Object = item.LOCATION_NO,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":AREA_NO",
                         Type = OracleDbType.Int64,
                         Object = item.AREA_NO,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":SECTION_NO",
                         Type = OracleDbType.Int64,
                         Object = item.SECTION_NO,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":INPUT_PERSONEL_ID",
                         Type = OracleDbType.Varchar2,
                         Object = item.INPUT_PERSONEL_ID,
                         Direct = ParameterDirection.Input
                     },
                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));
            }

            return results.All(x => x == true);
        }
        #endregion

        //Append Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
        #region PARKING_USEとダミーテーブルへの登録
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool InsertItem(List<ParkingModel> list)
        {
            var results = new List<bool>();

            foreach (var item in list)
            {
                // PARKING_USEへの登録
                //SQL
                var sql = new StringBuilder();

                sql.AppendLine("INSERT INTO");
                sql.AppendLine("\"DUMMY_TESTCAR\"(");
                sql.AppendLine("    \"データID\"");
                sql.AppendLine("    ,\"DELETE_FLG\"");
                sql.AppendLine("    ,\"INPUT_DATETIME\"");
                sql.AppendLine("    ,\"INPUT_PERSONEL_ID\"");
                sql.AppendLine(") VALUES (");
                sql.AppendLine("    (SELECT MAX(A.ID) FROM (SELECT NVL(MAX(データID), 0) + 1 ID FROM 試験車基本情報 UNION SELECT NVL(MAX(データID), 0) + 1 ID FROM DUMMY_TESTCAR) A)");
                sql.AppendLine("    ,'0'");
                sql.AppendLine("    ,SYSDATE");
                sql.AppendLine("    ,:INPUT_PERSONEL_ID");
                sql.AppendLine(") RETURNING");
                sql.AppendLine("    データID INTO :newid");

                //パラメータ
                var prms = new List<BindModel>
                {
                     new BindModel
                     {
                         Name = ":INPUT_PERSONEL_ID",
                         Type = OracleDbType.Varchar2,
                         Object = item.INPUT_PERSONEL_ID,
                         Direct = ParameterDirection.Input
                     },
                };
                db.Returns.Add(new BindModel
                {
                    Name = ":newid",
                    Type = OracleDbType.Int32,
                    Direct = ParameterDirection.Input
                });

                results.Add(db.InsertData(sql.ToString(), prms));
                var dataId = Convert.ToInt32(db.Returns.Where(r => r.Name == ":newid").FirstOrDefault().Object.ToString());

                // PARKING_USEへの登録
                //SQL
                var sql2 = new StringBuilder();

                sql2.AppendLine("INSERT INTO");
                sql2.AppendLine("\"PARKING_USE\"(");
                sql2.AppendLine("    \"データID\"");
                sql2.AppendLine("    ,\"LOCATION_NO\"");
                sql2.AppendLine("    ,\"AREA_NO\"");
                sql2.AppendLine("    ,\"SECTION_NO\"");
                sql2.AppendLine("    ,\"INPUT_DATETIME\"");
                sql2.AppendLine("    ,\"INPUT_PERSONEL_ID\"");
                sql2.AppendLine(") VALUES (");
                sql2.AppendLine("    :データID");
                sql2.AppendLine("    ,:LOCATION_NO");
                sql2.AppendLine("    ,:AREA_NO");
                sql2.AppendLine("    ,:SECTION_NO");
                sql2.AppendLine("    ,SYSDATE");
                sql2.AppendLine("    ,:INPUT_PERSONEL_ID");
                sql2.AppendLine(")");

                //パラメータ
                var prms2 = new List<BindModel>
                {
                     new BindModel
                     {
                         Name = ":データID",
                         Type = OracleDbType.Int64,
                         Object = dataId,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":LOCATION_NO",
                         Type = OracleDbType.Int64,
                         Object = item.LOCATION_NO,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":AREA_NO",
                         Type = OracleDbType.Int64,
                         Object = item.AREA_NO,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":SECTION_NO",
                         Type = OracleDbType.Int64,
                         Object = item.SECTION_NO ?? 0,
                         Direct = ParameterDirection.Input
                     },
                     new BindModel
                     {
                         Name = ":INPUT_PERSONEL_ID",
                         Type = OracleDbType.Varchar2,
                         Object = item.INPUT_PERSONEL_ID,
                         Direct = ParameterDirection.Input
                     },
                };

                results.Add(db.InsertData(sql2.ToString(), prms2));
            }

            return results.All(x => x == true);
        }
        #endregion

        #region PARKING_USE削除
        /// <summary>
        /// 駐車場管理削除
        /// </summary>
        /// <param name="list">駐車場管理項目</param>
        /// <returns>削除可否</returns>
        private bool DeleteUseItem(List<ParkingModel> list)
        {
            var results = new List<bool>();

            foreach (var item in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                 {
                     new BindModel
                     {
                         Name = ":データID",
                         Type = OracleDbType.Int64,
                         Object = item.データID,
                         Direct = ParameterDirection.Input
                     }
                };
                var resultItem = db.ReadModelList<TestCarDummyModel>(@"
SELECT
  *
FROM
  PARKING_USE 
WHERE
  データID = :データID
", prms).FirstOrDefault();

                if (resultItem != null && !string.IsNullOrEmpty(resultItem.データID))
                {
                    // SQL
                    var sql2 = new StringBuilder();
                    sql2.AppendLine("DELETE FROM");
                    sql2.AppendLine("    \"PARKING_USE\"");
                    sql2.AppendLine("WHERE");
                    sql2.AppendLine("    \"データID\" = :データID");

                    results.Add(db.UpdateData(sql2.ToString(), prms));

                    //SQL
                    var sql3 = new StringBuilder();
                    sql3.AppendLine("UPDATE");
                    sql3.AppendLine("    \"DUMMY_TESTCAR\"");
                    sql3.AppendLine("SET");
                    sql3.AppendLine("    \"DELETE_FLG\" = '1'");
                    sql3.AppendLine("WHERE 0 = 0");
                    sql3.AppendLine("    AND \"データID\" = :データID");

                    //更新
                    results.Add(db.UpdateData(sql3.ToString(), prms));

                }
                else
                {
                    results.Add(true);
                }
            }

            return results.All(x => x == true);
        }
        #endregion
        //Append End 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更

    }
}