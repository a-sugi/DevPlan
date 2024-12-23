using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>週報データの操作</remarks>
    public class WeeklyReportLogic : BaseLogic
    {
        /// <summary>
        /// 週報データの取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        //public DataTable Get(WeeklyInModel val)
        public List<WeeklyOutModel> Get(WeeklyInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();
            DateTime weekendDate = new DateTime();

            //日時の指定がない場合、他の検索条件に一致する最新の日時を取得
            if (val.WEEKEND_DATE == null)
            {
                sql.Append("SELECT MAX(WEEKEND_DATE) FROM 試験計画_週報 ");
                sql.Append("WHERE ");
                if (val.DEPARTMENT_ID != null)
                {
                    sql.Append("DEPARTMENT_ID = :DEPARTMENT_ID and ");
                    prms.Add(new BindModel
                    {
                        Name = ":DEPARTMENT_ID",
                        Type = OracleDbType.Varchar2,
                        Object = val.DEPARTMENT_ID,
                        Direct = ParameterDirection.Input
                    });
                }
                if (val.SECTION_ID != null)
                {
                    sql.Append("SECTION_ID = :SECTION_ID and ");
                    prms.Add(new BindModel
                    {
                        Name = ":SECTION_ID",
                        Type = OracleDbType.Varchar2,
                        Object = val.SECTION_ID,
                        Direct = ParameterDirection.Input
                    });
                }
                if (val.SECTION_GROUP_ID != null)
                {
                    sql.Append("SECTION_GROUP_ID = :SECTION_GROUP_ID and ");
                    prms.Add(new BindModel
                    {
                        Name = ":SECTION_GROUP_ID",
                        Type = OracleDbType.Varchar2,
                        Object = val.SECTION_GROUP_ID,
                        Direct = ParameterDirection.Input
                    });
                }
                sql.Append("作成単位 = :作成単位");
                prms.Add(new BindModel
                {
                    Name = ":作成単位",
                    Type = OracleDbType.Varchar2,
                    Object = val.作成単位,
                    Direct = ParameterDirection.Input
                });

                //日時取得
                DataTable dt = db.ReadDataTable(sql.ToString(), prms);
                if(dt.Rows[0].ItemArray[0].ToString().Length == 0)
                {
                    //取得できない場合は終了
                    return (new List<WeeklyOutModel>());
                }
                weekendDate = Convert.ToDateTime((dt.Rows[0].ItemArray[0]).ToString());
            }

            //週報データを取得
            sql.Clear();
            prms.Clear();
            sql.Append("SELECT ");
            sql.Append("試験計画_週報.ID ");
            sql.Append(",試験計画_週報.GENERAL_CODE ");
            sql.Append(",試験計画_週報.CATEGORY ");
            sql.Append(",試験計画_週報.CURRENT_SITUATION ");
            sql.Append(",試験計画_週報.FUTURE_SCHEDULE ");
            sql.Append(",試験計画_週報.SORT_NO ");
            sql.Append(",試験計画_週報.WEEKEND_DATE ");
            sql.Append(",試験計画_週報.ISSUED_PERSONEL_ID ");
            sql.Append(",試験計画_週報.ISSUED_DATETIME ");
            sql.Append(",試験計画_週報.SECTION_GROUP_CODE_情報元 ");
            sql.Append("FROM ");
            sql.Append("試験計画_週報 ");
            sql.Append("WHERE ");
            sql.Append("試験計画_週報.作成単位 = :作成単位 and ");
            prms.Add(new BindModel
            {
                Name = ":作成単位",
                Type = OracleDbType.Varchar2,
                Object = val.作成単位,
                Direct = ParameterDirection.Input
            });
            if (val.DEPARTMENT_ID != null)
            {
                sql.Append("試験計画_週報.DEPARTMENT_ID = :DEPARTMENT_ID and ");
                prms.Add(new BindModel
                {
                    Name = ":DEPARTMENT_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.DEPARTMENT_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.SECTION_ID != null)
            {
                sql.Append("試験計画_週報.SECTION_ID = :SECTION_ID and ");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.SECTION_GROUP_ID != null)
            {
                sql.Append("試験計画_週報.SECTION_GROUP_ID = :SECTION_GROUP_ID and ");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_GROUP_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_GROUP_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.Append("試験計画_週報.WEEKEND_DATE = :WEEKEND_DATE and ");

            if (val.WEEKEND_DATE != null)
            {
                //指定された日時で検索
                prms.Add(new BindModel
                {
                    Name = ":WEEKEND_DATE",
                    Type = OracleDbType.Date,
                    Object = val.WEEKEND_DATE,
                    Direct = ParameterDirection.Input
                });
            }
            else
            {
                //日時が指定されていない場合、最新日時で検索
                prms.Add(new BindModel
                {
                    Name = ":WEEKEND_DATE",
                    Type = OracleDbType.Date,
                    Object = weekendDate,
                    Direct = ParameterDirection.Input
                });
            }

            sql.Append("(試験計画_週報.FLAG_参照制限 is null or 試験計画_週報.FLAG_参照制限 = 0) ");

            //並べ替え
            sql.Append("ORDER BY ");
            sql.Append("試験計画_週報.SORT_NO");

            return db.ReadModelList<WeeklyOutModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// 週報データの登録
        /// </summary>
        /// <param name="val"></param>
        /// <returns>bool</returns>
        public bool Post(WeeklyOutModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //使用されているIDの最大値を取得
            sql.Append("select MAX(ID) from 試験計画_週報");
            DataTable dt = db.ReadDataTable(sql.ToString(), null);
            if (dt.Rows[0].ItemArray[0].ToString().Length == 0)
            {
                //取得できない場合は終了
                return false;
            }

            //新規に登録するデータのIDを作成（最大値＋１）
            int newId = Convert.ToInt32((dt.Rows[0].ItemArray[0]).ToString()) + 1;

            //週報データを登録
            sql.Clear();
            sql.Append("INSERT INTO ");
            sql.Append("試験計画_週報( ");
            sql.Append("ID ");
            sql.Append(",GENERAL_CODE ");
            sql.Append(",CATEGORY ");
            sql.Append(",CURRENT_SITUATION ");
            sql.Append(",FUTURE_SCHEDULE ");
            sql.Append(",SORT_NO ");
            sql.Append(",WEEKEND_DATE ");
            sql.Append(",作成単位 ");
            sql.Append(",DEPARTMENT_ID ");
            sql.Append(",SECTION_ID ");
            sql.Append(",SECTION_GROUP_ID ");
            sql.Append(",INPUT_PERSONEL_ID ");
            sql.Append(",INPUT_DATETIME ");
            sql.Append(",SECTION_GROUP_CODE_情報元 ");
            if (val.ISSUED_PERSONEL_ID != null)
            {
                sql.Append(",ISSUED_PERSONEL_ID ");
            }
            if (val.ISSUED_DATETIME != null)
            {
                sql.Append(",ISSUED_DATETIME ");
            }
            sql.Append(") VALUES ( ");
            sql.Append(":ID ");
            sql.Append(",:GENERAL_CODE ");
            sql.Append(",:CATEGORY ");
            sql.Append(",:CURRENT_SITUATION ");
            sql.Append(",:FUTURE_SCHEDULE ");
            sql.Append(",:SORT_NO ");
            sql.Append(",:WEEKEND_DATE ");
            sql.Append(",:作成単位 ");
            sql.Append(",:DEPARTMENT_ID ");
            sql.Append(",:SECTION_ID ");
            sql.Append(",:SECTION_GROUP_ID ");
            sql.Append(",:INPUT_PERSONEL_ID ");
            sql.Append(",SYSDATE ");
            sql.Append(",:SECTION_GROUP_CODE_情報元 ");
            if (val.ISSUED_PERSONEL_ID != null)
            {
                sql.Append(",:ISSUED_PERSONEL_ID ");
            }
            if (val.ISSUED_DATETIME != null)
            {
                sql.Append(",:ISSUED_DATETIME ");
            }
            sql.Append(") ");
            
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Decimal,
                Object = newId,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":GENERAL_CODE",
                Type = OracleDbType.Varchar2,
                Object = val.GENERAL_CODE,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":CATEGORY",
                Type = OracleDbType.Varchar2,
                Object = val.CATEGORY,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":CURRENT_SITUATION",
                Type = OracleDbType.Varchar2,
                Object = val.CURRENT_SITUATION,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":FUTURE_SCHEDULE",
                Type = OracleDbType.Varchar2,
                Object = val.FUTURE_SCHEDULE,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":SORT_NO",
                Type = OracleDbType.Decimal,
                Object = val.SORT_NO,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":WEEKEND_DATE",
                Type = OracleDbType.Date,
                Object = val.WEEKEND_DATE,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":作成単位",
                Type = OracleDbType.Varchar2,
                Object = val.作成単位,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":DEPARTMENT_ID",
                Type = OracleDbType.Varchar2,
                Object = val.DEPARTMENT_ID,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":SECTION_ID",
                Type = OracleDbType.Varchar2,
                Object = val.SECTION_ID,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":SECTION_GROUP_ID",
                Type = OracleDbType.Varchar2,
                Object = val.SECTION_GROUP_ID,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":INPUT_PERSONEL_ID",
                Type = OracleDbType.Varchar2,
                Object = val.INPUT_PERSONEL_ID,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":SECTION_GROUP_CODE_情報元",
                Type = OracleDbType.Varchar2,
                Object = val.SECTION_GROUP_CODE_情報元,
                Direct = ParameterDirection.Input
            });
            if (val.ISSUED_PERSONEL_ID != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":ISSUED_PERSONEL_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.ISSUED_PERSONEL_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.ISSUED_DATETIME != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":ISSUED_DATETIME",
                    Type = OracleDbType.Date,
                    Object = val.ISSUED_DATETIME,
                    Direct = ParameterDirection.Input
                });
            }

            return db.InsertData(sql.ToString(), prms);
        }

        /// <summary>
        /// 週報データの更新
        /// </summary>
        /// <param name="val"></param>
        /// <returns>bool</returns>
        public bool Put(WeeklyOutModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //IDを指定して更新
            sql.Append("UPDATE ");
            sql.Append("試験計画_週報 ");
            sql.Append("SET ");
            sql.Append("CATEGORY = :CATEGORY ");
            sql.Append(",CURRENT_SITUATION = :CURRENT_SITUATION ");
            sql.Append(",FUTURE_SCHEDULE = :FUTURE_SCHEDULE ");
            sql.Append(",SORT_NO = :SORT_NO ");
            sql.Append("WHERE ");
            sql.Append("ID = :ID ");

            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Decimal,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":CATEGORY",
                Type = OracleDbType.Varchar2,
                Object = val.CATEGORY,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":CURRENT_SITUATION",
                Type = OracleDbType.Varchar2,
                Object = val.CURRENT_SITUATION,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":FUTURE_SCHEDULE",
                Type = OracleDbType.Varchar2,
                Object = val.FUTURE_SCHEDULE,
                Direct = ParameterDirection.Input
            });
            prms.Add(new BindModel
            {
                Name = ":SORT_NO",
                Type = OracleDbType.Decimal,
                Object = val.SORT_NO,
                Direct = ParameterDirection.Input
            });

            return db.UpdateData(sql.ToString(), prms);
        }

        /// <summary>
        /// 週報テーブルの削除
        /// </summary>
        /// <param name="val"></param>
        /// <returns>bool</returns>
        public bool Delete(WeeklyOutModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //IDを指定して削除
            sql.Append("DELETE ");
            sql.Append("FROM ");
            sql.Append("試験計画_週報 ");
            sql.Append("WHERE ");
            sql.Append("ID = :ID ");

            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Decimal,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });

            return db.DeleteData(sql.ToString(), prms);
        }
    }
}