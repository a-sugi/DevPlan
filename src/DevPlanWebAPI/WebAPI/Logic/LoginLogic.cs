using System;
using System.Data;
using System.DirectoryServices;
using System.Text;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// ログイン認証ロジッククラス
    /// </summary>
    /// <remarks>ログイン認証</remarks>
    public class LoginLogic : BaseLogic
    {
        /// <summary>
        /// ログイン情報の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable PostData(LoginInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT ");
            sql.Append("NAME ");
            sql.Append(",PERSONEL_ID ");
            sql.Append(",ACCESS_LEVEL ");
            sql.Append(",PASSWORD ");
            sql.Append("FROM ");
            sql.Append("PERSONEL_LIST ");
            sql.Append("WHERE ");
            sql.Append("LOGIN_ID = :LOGINID");

            prms.Add(new BindModel
            {
                Name = ":LOGINID",
                Type = OracleDbType.Varchar2,
                Object = val.LoginID,
                Direct = ParameterDirection.Input
            });

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// Active Directory LDAP認証
        /// </summary>
        /// <param name="val">入力パラメータ</param>
        /// <returns></returns>
        public bool IsAuthenticated(LoginInModel val)
        {
            string _path = "客先のディレクトリパスを設定する";
            string _filterAttribute;

            string domainAndUsername = val.DomainName + @"\" + val.LoginID;

            using (DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, val.InputPassword))
            {
                try
                {
                    //Bind to the native AdsObject to force authentication.
                    object obj = entry.NativeObject;

                    DirectorySearcher search = new DirectorySearcher(entry);

                    search.Filter = "(SAMAccountName=" + val.LoginID + ")";
                    search.PropertiesToLoad.Add("cn");
                    SearchResult result = search.FindOne();

                    if (null == result)
                    {
                        return false;
                    }

                    //Update the new path to the user in the directory.
                    _path = result.Path;
                    _filterAttribute = (string)result.Properties["cn"][0];
                }
                catch (Exception ex)
                {
                    throw new Exception("Error authenticating user. " + ex.Message);
                }

                return true;
            }
        }
        /// <summary>
        /// パスワードの更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(LoginPasswordChangeModel val)
        {
            var sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    PERSONEL_LIST");
            sql.AppendLine("SET");
            sql.AppendLine("    PASSWORD = :NEW_PASSWORD");
            sql.AppendLine("WHERE");
            sql.AppendLine("    LOGIN_ID = :LOGIN_ID");
            sql.AppendLine("    AND PASSWORD = :PASSWORD");

            //トランザクション開始
            db.Begin();

            var prms = new List<BindModel>();

            //パラメータ設定
            prms.Add(new BindModel { Name = ":LOGIN_ID", Type = OracleDbType.Varchar2, Object = val.LOGIN_ID, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":PASSWORD", Type = OracleDbType.Varchar2, Object = val.PASSWORD, Direct = ParameterDirection.Input });
            prms.Add(new BindModel { Name = ":NEW_PASSWORD", Type = OracleDbType.Varchar2, Object = val.NEW_PASSWORD, Direct = ParameterDirection.Input });

            if (!db.UpdateData(sql.ToString(), prms))
            {
                db.Rollback();
                return false;
            }

            //コミット
            db.Commit();

            return true;
        }
    }
}