using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 車両管理担当ロジッククラス
    /// </summary>
    public class CarManagerLogic : BaseLogic
    {
        #region 車両管理担当
        /// <summary>
        /// 車両管理担当
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<CarManagerModel> GetData(CarManagerSearchModel cond)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            if (cond.STATUS != null && cond.STATUS == "全")
            {
                sql.AppendLine("SELECT");
                sql.AppendLine("     GENERAL_CODE");
                sql.AppendLine("    ,TEL");
                sql.AppendLine("    ,STATUS");
                sql.AppendLine("    ,ID");
                sql.AppendLine("    ,CATEGORY_ID");
                sql.AppendLine("    ,REMARKS");
                sql.AppendLine("FROM CONTACT_INFO");
                sql.AppendLine("WHERE STATUS = :STATUS");

                prms.Add(new BindModel { Name = ":STATUS", Type = OracleDbType.Varchar2, Object = cond.STATUS, Direct = ParameterDirection.Input });
            }
            else
            {
                sql.AppendLine("SELECT");
                sql.AppendLine("  A.GENERAL_CODE");
                sql.AppendLine("  , D.SECTION_CODE");
                sql.AppendLine("  , B.PERSONEL_ID");
                sql.AppendLine("  , B.NAME");
                sql.AppendLine("  , A.TEL");
                sql.AppendLine("  , A.STATUS");
                sql.AppendLine("  , A.ID");
                sql.AppendLine("  , A.CATEGORY_ID");
                sql.AppendLine("  , A.REMARKS ");
                sql.AppendLine("FROM");
                sql.AppendLine("(");
                sql.AppendLine("    SELECT");
                sql.AppendLine("      * ");
                sql.AppendLine("    FROM");
                sql.AppendLine("      CONTACT_INFO ");
                sql.AppendLine("    WHERE");
                sql.AppendLine("      STATUS != '全' ");
                sql.AppendLine("      AND GENERAL_CODE = :GENERAL_CODE ");

                if (string.IsNullOrWhiteSpace(cond.CATEGORY_ID) == false)
                {
                    sql.AppendLine("      AND CATEGORY_ID = :CATEGORY_ID");
                    prms.Add(new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Varchar2, Object = cond.CATEGORY_ID, Direct = ParameterDirection.Input });
                }

                sql.AppendLine("      AND FUNCTION_ID = :FUNCTION_ID");
                prms.Add(new BindModel { Name = ":FUNCTION_ID", Type = OracleDbType.Int32, Object = cond.FUNCTION_ID, Direct = ParameterDirection.Input });

                sql.AppendLine("    ORDER BY");
                sql.AppendLine("      STATUS");
                sql.AppendLine("  ) A ");
                sql.AppendLine("  INNER JOIN PERSONEL_LIST B ");
                sql.AppendLine("    ON A.PERSONEL_ID = B.PERSONEL_ID ");
                sql.AppendLine("  INNER JOIN SECTION_GROUP_DATA C ");
                sql.AppendLine("    ON B.SECTION_GROUP_ID = C.SECTION_GROUP_ID ");
                sql.AppendLine("  INNER JOIN SECTION_DATA D ");
                sql.AppendLine("    ON C.SECTION_ID = D.SECTION_ID");

                prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = cond.GENERAL_CODE, Direct = ParameterDirection.Input });
            }
           
            return db.ReadModelList<CarManagerModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// 車両管理担当削除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteData(List<CarManagerModel> list)
        {
            var results = new List<bool>();

            db.Begin();

            if (list != null && list.Any() == true)
            {
                var sql = new StringBuilder();
                var prms = new List<BindModel>();

                foreach (var item in list)
                {
                    sql.Clear();
                    sql.AppendLine("DELETE FROM CONTACT_INFO");
                    sql.AppendLine("WHERE ID = :ID");

                    prms.Clear();
                    prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Varchar2, Object = item.ID, Direct = ParameterDirection.Input });
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
        /// 車両管理担当更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateData(List<CarManagerModel> list)
        {
            var results = new List<bool>();

            db.Begin();

            if (list != null && list.Any() == true)
            {
                var sql = new StringBuilder();
                var prms = new List<BindModel>();

                foreach (var item in list)
                {
                    sql.Clear();
                    sql.AppendLine("UPDATE CONTACT_INFO");
                    sql.AppendLine("SET");
                    sql.AppendLine("GENERAL_CODE = :GENERAL_CODE");
                    sql.AppendLine(",PERSONEL_ID = :PERSONEL_ID");
                    sql.AppendLine(",TEL = :TEL");
                    sql.AppendLine(",STATUS = :STATUS");
                    sql.AppendLine(",REMARKS = :REMARKS");
                    sql.AppendLine(",FUNCTION_ID = :FUNCTION_ID");

                    sql.AppendLine("WHERE ID = :ID");

                    prms.Clear();
                    prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Varchar2, Object = item.ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = item.GENERAL_CODE, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = item.PERSONEL_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":TEL", Type = OracleDbType.Varchar2, Object = item.TEL, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":STATUS", Type = OracleDbType.Varchar2, Object = item.STATUS, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":REMARKS", Type = OracleDbType.Varchar2, Object = item.REMARKS, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":FUNCTION_ID", Type = OracleDbType.Int32, Object = item.FUNCTION_ID, Direct = ParameterDirection.Input });
                    
                    results.Add(db.UpdateData(sql.ToString(), prms));
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
        /// 車両管理担当追加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool InsertData(List<CarManagerModel> list)
        {
            var results = new List<bool>();
            
            db.Begin();
            
            if (list != null && list.Any() == true)
            {
                var sql = new StringBuilder();
                var prms = new List<BindModel>();

                foreach (var item in list)
                {
                    sql.Clear();
                    sql.AppendLine("INSERT");
                    sql.AppendLine("INTO CONTACT_INFO(ID, GENERAL_CODE, PERSONEL_ID, TEL, STATUS, CATEGORY_ID, REMARKS, FUNCTION_ID) ");
                    sql.AppendLine("SELECT");
                    sql.AppendLine("MIN(ID + 1) AS NEW_NO");
                    sql.AppendLine(", :GENERAL_CODE");
                    sql.AppendLine(", :PERSONEL_ID");
                    sql.AppendLine(", :TEL");
                    sql.AppendLine(", :STATUS");
                    sql.AppendLine(", :CATEGORY_ID");
                    sql.AppendLine(", :REMARKS");
                    sql.AppendLine(", :FUNCTION_ID");
                    sql.AppendLine("FROM CONTACT_INFO");
                    sql.AppendLine("WHERE (ID + 1) NOT IN (SELECT ID FROM CONTACT_INFO)");

                    prms.Clear();
                    prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = item.GENERAL_CODE, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = item.PERSONEL_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":TEL", Type = OracleDbType.Varchar2, Object = item.TEL, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":STATUS", Type = OracleDbType.Varchar2, Object = item.STATUS, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Varchar2, Object = item.CATEGORY_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":REMARKS", Type = OracleDbType.Varchar2, Object = item.REMARKS, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":FUNCTION_ID", Type = OracleDbType.Int32, Object = item.FUNCTION_ID, Direct = ParameterDirection.Input });

                    results.Add(db.InsertData(sql.ToString(), prms));
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
        #endregion
    }
}