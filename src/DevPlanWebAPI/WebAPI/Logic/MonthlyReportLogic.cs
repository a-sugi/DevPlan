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
    /// <remarks>月報データの操作</remarks>
    public class MonthlyReportLogic : BaseLogic
    {
        /// <summary>
        /// 月報データの取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        //public DataTable Get(MonthlyInModel val)
        public List<MonthlyOutModel> Get(MonthlyInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();
            DateTime monthlyFirstDay = new DateTime();

            //日時の指定がない場合、他の検索条件に一致する最新の日時を取得
            if (val.MONTH_FIRST_DAY == null)
            {
                sql.Append("SELECT MAX(MONTH_FIRST_DAY) FROM 試験計画_月次報告 ");
                sql.Append("WHERE ");

                if (val.DEPARTMENT_ID != null)
                {
                    sql.Append("DEPARTMENT_ID = :DEPARTMENT_ID");
                    prms.Add(new BindModel
                    {
                        Name = ":DEPARTMENT_ID",
                        Type = OracleDbType.Varchar2,
                        Object = val.DEPARTMENT_ID,
                        Direct = ParameterDirection.Input
                    });
                }
                else if (val.SECTION_ID != null)
                {
                    sql.Append("SECTION_ID = :SECTION_ID");
                    prms.Add(new BindModel
                    {
                        Name = ":SECTION_ID",
                        Type = OracleDbType.Varchar2,
                        Object = val.SECTION_ID,
                        Direct = ParameterDirection.Input
                    });
                }

                //日時取得
                DataTable dt = db.ReadDataTable(sql.ToString(), prms);
                if(dt == null || dt.Rows[0].ItemArray[0].ToString().Length == 0)
                {
                    //取得できない場合は終了
                    return (new List<MonthlyOutModel>());
                }
                monthlyFirstDay = Convert.ToDateTime((dt.Rows[0].ItemArray[0]).ToString());
            }

            //月報データを取得
            sql.Clear();
            prms.Clear();
            sql.Append("SELECT ");
            sql.Append("ID ");
            sql.Append(",GENERAL_CODE ");
            sql.Append(",CURRENT_SITUATION ");
            sql.Append(",FUTURE_SCHEDULE ");
            sql.Append(",SORT_NO ");
            sql.Append(",MONTH_FIRST_DAY ");
            sql.Append("FROM ");
            sql.Append("試験計画_月次報告 ");    
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

            sql.Append("MONTH_FIRST_DAY = :MONTH_FIRST_DAY ");

            if (val.MONTH_FIRST_DAY != null)
            {
                //指定された日時で検索
                prms.Add(new BindModel
                {
                    Name = ":MONTH_FIRST_DAY",
                    Type = OracleDbType.Date,
                    Object = val.MONTH_FIRST_DAY,
                    Direct = ParameterDirection.Input
                });
            }
            else
            {
                //日時が指定されていない場合、最新日時で検索
                prms.Add(new BindModel
                {
                    Name = ":MONTH_FIRST_DAY",
                    Type = OracleDbType.Date,
                    Object = monthlyFirstDay,
                    Direct = ParameterDirection.Input
                });
            }

            //並べ替え
            sql.Append("ORDER BY ");
            sql.Append("SORT_NO ASC ");

            return db.ReadModelList<MonthlyOutModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// 月報データの登録
        /// </summary>
        /// <param name="val"></param>
        /// <returns>bool</returns>
        public bool Post(MonthlyOutModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //使用されているIDの最大値を取得
            sql.Append("select MAX(ID) from 試験計画_月次報告");
            DataTable dt = db.ReadDataTable(sql.ToString(), null);
            if (dt.Rows[0].ItemArray[0].ToString().Length == 0)
            {
                //取得できない場合は終了
                return false;
            }

            //新規に登録するデータのIDを作成（最大値＋１）
            int newId = Convert.ToInt32((dt.Rows[0].ItemArray[0]).ToString()) + 1;

            //月報データを登録
            sql.Clear();
            sql.Append("INSERT INTO ");
            sql.Append("試験計画_月次報告( ");
            sql.Append("ID ");
            sql.Append(",GENERAL_CODE ");
            sql.Append(",CURRENT_SITUATION ");
            sql.Append(",FUTURE_SCHEDULE ");
            sql.Append(",SORT_NO ");
            sql.Append(",MONTH_FIRST_DAY ");
            sql.Append(",DEPARTMENT_ID ");
            sql.Append(",SECTION_ID ");
            if (val.MANAGER_NAME != null)
            {
                sql.Append(",MANAGER_NAME ");
            }
            sql.Append(") VALUES ( ");
            sql.Append(":ID ");
            sql.Append(",:GENERAL_CODE ");
            sql.Append(",:CURRENT_SITUATION ");
            sql.Append(",:FUTURE_SCHEDULE ");
            sql.Append(",:SORT_NO ");
            sql.Append(",:MONTH_FIRST_DAY ");
            sql.Append(",:DEPARTMENT_ID ");
            sql.Append(",:SECTION_ID ");
            if (val.MANAGER_NAME != null)
            {
                sql.Append(",:MANAGER_NAME ");
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
                Name = ":MONTH_FIRST_DAY",
                Type = OracleDbType.Date,
                Object = val.MONTH_FIRST_DAY,
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
            if (val.MANAGER_NAME != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":MANAGER_NAME",
                    Type = OracleDbType.Varchar2,
                    Object = val.MANAGER_NAME,
                    Direct = ParameterDirection.Input
                });
            }
            return db.InsertData(sql.ToString(), prms);
        }

        /// <summary>
        /// 月報データの更新
        /// </summary>
        /// <param name="val"></param>
        /// <returns>bool</returns>
        public bool Put(MonthlyOutModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //IDを指定して更新
            sql.Append("UPDATE ");
            sql.Append("試験計画_月次報告 ");
            sql.Append("SET ");
            sql.Append("GENERAL_CODE = :GENERAL_CODE ");
            sql.Append(",CURRENT_SITUATION = :CURRENT_SITUATION ");
            sql.Append(",FUTURE_SCHEDULE = :FUTURE_SCHEDULE ");
            sql.Append(",SORT_NO = :SORT_NO ");
            if (val.MANAGER_NAME != null)
            {
                sql.Append(",MANAGER_NAME = :MANAGER_NAME ");
            }
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
                Name = ":GENERAL_CODE",
                Type = OracleDbType.Varchar2,
                Object = val.GENERAL_CODE,
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
            if (null != val.MANAGER_NAME)
            {
                prms.Add(new BindModel
                {
                    Name = ":MANAGER_NAME",
                    Type = OracleDbType.Varchar2,
                    Object = val.MANAGER_NAME,
                    Direct = ParameterDirection.Input
                });
            }

            return db.UpdateData(sql.ToString(), prms);
        }

        /// <summary>
        /// 月報テーブルの削除
        /// </summary>
        /// <param name="val"></param>
        /// <returns>bool</returns>
        public bool Delete(MonthlyOutModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //IDを指定して削除
            sql.Append("DELETE ");
            sql.Append("FROM ");
            sql.Append("試験計画_月次報告 ");
            sql.Append("WHERE ");
            sql.Append("ID = :ID ");

            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int32,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });

            return db.DeleteData(sql.ToString(), prms);
        }
    }
}