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
    /// <remarks>週報承認データの操作</remarks>
    public class WeeklyReportApprovalLogic : BaseLogic
    {
        /// <summary>
        /// 週報承認データの取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DataTable</returns>
        public List<WeeklyReportApproveOutModel> Get(WeeklyReportApproveInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //週報承認データを取得
            sql.Append("SELECT ");
            sql.Append("試験計画_週報承認.FLAG_承認 ");
            sql.Append(",試験計画_週報承認.承認日時 ");
            sql.Append(",SECTION_DATA.SECTION_CODE ");
            sql.Append(",PERSONEL_LIST.NAME ");
            sql.Append("FROM ");
            sql.Append("試験計画_週報承認 ");
            sql.Append(",PERSONEL_LIST ");
            sql.Append(",SECTION_GROUP_DATA ");
            sql.Append(",SECTION_DATA ");

            sql.Append("WHERE ");
            sql.Append("(試験計画_週報承認.WEEKEND_DATE = :WEEKEND_DATE) and ");
            prms.Add(new BindModel
            {
                Name = ":WEEKEND_DATE",
                Type = OracleDbType.Date,
                Object = val.WEEKEND_DATE,
                Direct = ParameterDirection.Input
            });

            //部IDが指定されている場合
            if (val.DEPARTMENT_ID != null)
            {
                sql.Append("(試験計画_週報承認.DEPARTMENT_ID = :DEPARTMENT_ID) and ");

                prms.Add(new BindModel
                {
                    Name = ":DEPARTMENT_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.DEPARTMENT_ID,
                    Direct = ParameterDirection.Input
                });
            }

            //課IDが指定されている場合
            if (val.SECTION_ID != null)
            {
                sql.Append("(試験計画_週報承認.SECTION_ID = :SECTION_ID) and ");

                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }

            //担当IDが指定されている場合
            if (val.SECTION_GROUP_ID != null)
            {
                sql.Append("(試験計画_週報承認.SECTION_GROUP_ID = :SECTION_GROUP_ID) and ");

                prms.Add(new BindModel
                {
                    Name = ":SECTION_GROUP_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_GROUP_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.Append("(試験計画_週報承認.承認者_PERSONEL_ID = PERSONEL_LIST.PERSONEL_ID) and ");
            sql.Append("(PERSONEL_LIST.SECTION_GROUP_ID = SECTION_GROUP_DATA.SECTION_GROUP_ID) and ");
            sql.Append("(SECTION_GROUP_DATA.SECTION_ID = SECTION_DATA.SECTION_ID) ");

            //昇順に並べ替え
            sql.Append("ORDER BY ");
            sql.Append("試験計画_週報承認.承認日時 DESC ");

            return db.ReadModelList<WeeklyReportApproveOutModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// 週報承認データの登録
        /// </summary>
        /// <param name="val"></param>
        /// <returns>bool</returns>
        public bool Post(WeeklyReportApproveModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //使用されているIDの最大値を取得する。
            sql.Append("select MAX(ID) from 試験計画_週報承認");
            DataTable dt = db.ReadDataTable(sql.ToString(), null);
            if (dt.Rows[0].ItemArray[0].ToString().Length == 0)
            {
                //取得できない場合は終了
                return false;
            }

            //新規に登録するデータのIDを作成（最大値＋１）
            int newId = Convert.ToInt32((dt.Rows[0].ItemArray[0]).ToString()) + 1;

            sql.Clear();
            sql.Append("INSERT INTO ");
            sql.Append("試験計画_週報承認( ");
            sql.Append("ID ");
            sql.Append(",FLAG_承認 ");
            sql.Append(",承認日時 ");
            sql.Append(",承認者_PERSONEL_ID ");
            sql.Append(",WEEKEND_DATE ");
            sql.Append(",作成単位 ");
            if (val.DEPARTMENT_ID != null && val.作成単位 == "部")
            {
                sql.Append(",DEPARTMENT_ID ");
            }
            if (val.SECTION_ID != null && val.作成単位 == "課")
            {
                sql.Append(",SECTION_ID ");
            }
            if (val.SECTION_GROUP_ID != null && val.作成単位 == "担当")
            {
                sql.Append(",SECTION_GROUP_ID ");
            }
            sql.Append(") VALUES ( ");
            sql.Append(":ID ");
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Decimal,
                Object = newId,
                Direct = ParameterDirection.Input
            });
            sql.Append(",:FLAG_承認 ");
            prms.Add(new BindModel
            {
                Name = ":FLAG_承認",
                Type = OracleDbType.Int16,
                Object = val.FLAG_承認,
                Direct = ParameterDirection.Input
            });
            sql.Append(",SYSDATE ");
            sql.Append(",:承認者_PERSONEL_ID ");
            prms.Add(new BindModel
            {
                Name = ":承認者_PERSONEL_ID",
                Type = OracleDbType.Varchar2,
                Object = val.承認者_PERSONEL_ID,
                Direct = ParameterDirection.Input
            });
            sql.Append(",:WEEKEND_DATE ");
            prms.Add(new BindModel
            {
                Name = ":WEEKEND_DATE",
                Type = OracleDbType.Date,
                Object = val.WEEKEND_DATE,
                Direct = ParameterDirection.Input
            });
            sql.Append(",:作成単位 ");
            prms.Add(new BindModel
            {
                Name = ":作成単位",
                Type = OracleDbType.Varchar2,
                Object = val.作成単位,
                Direct = ParameterDirection.Input
            });
            if (val.DEPARTMENT_ID != null && val.作成単位 == "部")
            {
                sql.Append(",:DEPARTMENT_ID ");
                prms.Add(new BindModel
                {
                    Name = ":DEPARTMENT_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.DEPARTMENT_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.SECTION_ID != null && val.作成単位 == "課")
            {
                sql.Append(",:SECTION_ID ");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.SECTION_GROUP_ID != null && val.作成単位 == "担当") {
                sql.Append(",:SECTION_GROUP_ID ");
                prms.Add(new BindModel
                {
                    Name = ":SECTION_GROUP_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_GROUP_ID,
                    Direct = ParameterDirection.Input
                });
            }
            sql.Append(") ");

            //試験計画_週報承認テーブルへ登録
            return db.InsertData(sql.ToString(), prms);
        }

        /// <summary>
        /// 週報承認データの登録
        /// </summary>
        /// <param name="val"></param>
        /// <returns>bool</returns>
        public bool Put(WeeklyReportApproveModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            //試験計画_週報テーブルの承認に関連する項目を更新
            sql.Append("UPDATE ");
            sql.Append("試験計画_週報 ");
            sql.Append("SET ");
            if (val.FLAG_承認 == 1)
            {
                sql.Append("ISSUED_PERSONEL_ID = :ISSUED_PERSONEL_ID ");
                sql.Append(",ISSUED_DATETIME = SYSDATE ");
                prms.Add(new BindModel
                {
                    Name = ":ISSUED_PERSONEL_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.承認者_PERSONEL_ID,
                    Direct = ParameterDirection.Input
                });
            }
            else
            {
                sql.Append("ISSUED_PERSONEL_ID = NULL ");
                sql.Append(",ISSUED_DATETIME = NULL ");
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

            return db.UpdateData(sql.ToString(), prms);
        }
    }
}