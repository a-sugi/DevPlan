//
// 業務計画表システム
// KKA15010 機能権限マスタ編集API
// 作成者 : 岸　義将
// 作成日 : 2017/02/21
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 機能権限マスタ編集ロジッククラス
    /// </summary>
    /// <remarks>機能権限データの操作</remarks>
    public class FunctionAuthorityLogic : BaseLogic
    {
        #region 機能権限の取得
        /// <summary>
        /// 機能権限の取得
        /// </summary>
        /// <returns>DataTable</returns>

        public List<FunctionAuthorityOutModel> GetData(FunctionAuthorityInModel val)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            var flag = false;

            sql.AppendLine("SELECT");
            sql.AppendLine("     M.ID");
            sql.AppendLine("    ,M.FUNCTION_NAME");
            sql.AppendLine("    ,M.ID AS FUNCTION_ID");
            sql.AppendLine("    ,A.DEPARTMENT_ID");
            sql.AppendLine("    ,A.SECTION_ID");
            sql.AppendLine("    ,A.SECTION_GROUP_ID");
            sql.AppendLine("    ,A.PERSONEL_ID");
            sql.AppendLine("    ,NVL(A.READ_FLG,'0') AS READ_FLG");
            sql.AppendLine("    ,NVL(A.UPDATE_FLG,'0') AS UPDATE_FLG");
            sql.AppendLine("    ,NVL(A.EXPORT_FLG,'0') AS EXPORT_FLG");
            sql.AppendLine("    ,NVL(A.MANAGEMENT_FLG,'0') AS MANAGEMENT_FLG");
            sql.AppendLine("FROM ");
            sql.AppendLine("    FUNCTION_MST M");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("(");
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.ID");
            sql.AppendLine("    ,A.DEPARTMENT_ID");
            sql.AppendLine("    ,A.SECTION_ID");
            sql.AppendLine("    ,A.SECTION_GROUP_ID");
            sql.AppendLine("    ,A.PERSONEL_ID");
            sql.AppendLine("    ,A.FUNCTION_ID");
            sql.AppendLine("    ,A.READ_FLG");
            sql.AppendLine("    ,A.UPDATE_FLG");
            sql.AppendLine("    ,A.EXPORT_FLG");
            sql.AppendLine("    ,A.MANAGEMENT_FLG");
            sql.AppendLine("    ,A.INPUT_DATETIME");
            sql.AppendLine("    ,A.INPUT_PERSONEL_ID");
            sql.AppendLine("    ,A.CHANGE_DATETIME");
            sql.AppendLine("    ,A.CHANGE_PERSONEL_ID ");
            sql.AppendLine("FROM");
            sql.AppendLine("    FUNCTION_AUTH A");
            sql.AppendLine("WHERE");

            // INPUTが部ID以下の場合
            if (val.DEPARTMENT_ID != null)
            {
                for (var i = 0; i < val.DEPARTMENT_ID.Count(); i++)
                {
                    if (flag) sql.AppendLine("        OR");
                    sql.AppendLine("        ( A.DEPARTMENT_ID = ");
                    var name = string.Format(":DEPARTMENTID{0}", i);
                    sql.AppendFormat("{0}", name);
                    prms.Add(new BindModel { Name = name, Type = OracleDbType.Varchar2, Object = val.DEPARTMENT_ID.ElementAt(i), Direct = ParameterDirection.Input });
                    flag = true;

                    if (i < val.SECTION_ID.Count())
                    {
                        if (!string.IsNullOrEmpty(val.SECTION_ID[i]))
                        {
                            sql.AppendLine("        AND A.SECTION_ID = ");
                            var name2 = string.Format(":SECTION_ID{0}", i);
                            sql.AppendFormat("{0}", name2);
                            prms.Add(new BindModel { Name = name2, Type = OracleDbType.Varchar2, Object = val.SECTION_ID.ElementAt(i), Direct = ParameterDirection.Input });
                        }
                        else
                        {
                            sql.AppendLine("        AND A.SECTION_ID IS NULL");
                        }
                    }
                    else
                    {
                        sql.AppendLine("        AND A.SECTION_ID IS NULL");
                    }

                    if (i < val.SECTION_GROUP_ID.Count())
                    {
                        if (!string.IsNullOrEmpty(val.SECTION_GROUP_ID[i]))
                        {
                            sql.AppendLine("        AND A.SECTION_GROUP_ID = ");
                            var name3 = string.Format(":SECTION_GROUP_ID{0}", i);
                            sql.AppendFormat("{0}", name3);
                            prms.Add(new BindModel { Name = name3, Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID.ElementAt(i), Direct = ParameterDirection.Input });
                        }
                        else
                        {
                            sql.AppendLine("        AND A.SECTION_GROUP_ID IS NULL");
                        }
                    }
                    else
                    {
                        sql.AppendLine("        AND A.SECTION_GROUP_ID IS NULL");
                    }

                    if (i < val.PERSONEL_ID.Count())
                    {
                        if (!string.IsNullOrEmpty(val.PERSONEL_ID[i]))
                        {
                            sql.AppendLine("        AND A.PERSONEL_ID = ");
                            var name4 = string.Format(":PERSONEL_ID{0}", i);
                            sql.AppendFormat("{0}", name4);
                            prms.Add(new BindModel { Name = name4, Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID.ElementAt(i), Direct = ParameterDirection.Input });
                        }
                        else
                        {
                            sql.AppendLine("        AND A.PERSONEL_ID IS NULL");
                        }
                    }
                    else
                    {
                        sql.AppendLine("        AND A.PERSONEL_ID IS NULL");
                    }
                    sql.AppendLine(")");
                }
            }

            // INPUTが課ID以下の場合
            if (val.DEPARTMENT_ID == null && val.SECTION_ID != null)
            {
                for (var i = 0; i < val.SECTION_ID.Count(); i++)
                {
                    if (flag) sql.AppendLine("        OR");
                    sql.AppendLine("        ( A.SECTION_ID = ");
                    var name = string.Format(":SECTION_ID{0}", i);
                    sql.AppendFormat("{0}", name);
                    prms.Add(new BindModel { Name = name, Type = OracleDbType.Varchar2, Object = val.SECTION_ID.ElementAt(i), Direct = ParameterDirection.Input });
                    flag = true;

                    if (i < val.SECTION_GROUP_ID.Count())
                    {
                        if (!string.IsNullOrEmpty(val.SECTION_GROUP_ID[i]))
                        {
                            sql.AppendLine("        AND A.SECTION_GROUP_ID = ");
                            var name2 = string.Format(":SECTION_GROUP_ID{0}", i);
                            sql.AppendFormat("{0}", name2);
                            prms.Add(new BindModel { Name = name2, Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID.ElementAt(i), Direct = ParameterDirection.Input });
                        }
                        else
                        {
                            sql.AppendLine("        AND A.SECTION_GROUP_ID IS NULL");
                        }
                    }
                    else
                    {
                        sql.AppendLine("        AND A.SECTION_GROUP_ID IS NULL");
                    }

                    if (i < val.PERSONEL_ID.Count())
                    {
                        if (!string.IsNullOrEmpty(val.PERSONEL_ID[i]))
                        {
                            sql.AppendLine("        AND A.PERSONEL_ID = ");
                            var name3 = string.Format(":PERSONEL_ID{0}", i);
                            sql.AppendFormat("{0}", name3);
                            prms.Add(new BindModel { Name = name3, Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID.ElementAt(i), Direct = ParameterDirection.Input });
                        }
                        else
                        {
                            sql.AppendLine("        AND A.PERSONEL_ID IS NULL");
                        }
                    }
                    else
                    {
                        sql.AppendLine("        AND A.PERSONEL_ID IS NULL");
                    }
                    sql.AppendLine(")");
                }
            }

            // INPUTが担当ID以下の場合
            if (val.DEPARTMENT_ID == null && val.SECTION_ID == null && val.SECTION_GROUP_ID != null)
            {
                for (var i = 0; i < val.SECTION_GROUP_ID.Count(); i++)
                {
                    if (flag) sql.AppendLine("        OR");
                    sql.AppendLine("        ( A.SECTION_GROUP_ID = ");
                    var name = string.Format(":SECTIONGROUPID{0}", i);
                    sql.AppendFormat("{0}", name);
                    prms.Add(new BindModel { Name = name, Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID.ElementAt(i), Direct = ParameterDirection.Input });
                    flag = true;

                    if (i < val.PERSONEL_ID.Count())
                    {
                        if (!string.IsNullOrEmpty(val.PERSONEL_ID[i]))
                        {
                            sql.AppendLine("        AND A.PERSONEL_ID = ");
                            var name2 = string.Format(":PERSONEL_ID{0}", i);
                            sql.AppendFormat("{0}", name2);
                            prms.Add(new BindModel { Name = name2, Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID.ElementAt(i), Direct = ParameterDirection.Input });
                        }
                        else
                        {
                            sql.AppendLine("        AND A.PERSONEL_ID IS NULL");
                        }
                    }
                    else
                    {
                        sql.AppendLine("        AND A.PERSONEL_ID IS NULL");
                    }
                    sql.AppendLine(")");
                }
            }

            // INPUTがユーザーIDの場合
            if (val.DEPARTMENT_ID == null && val.SECTION_ID == null && val.SECTION_GROUP_ID == null && val.PERSONEL_ID != null)
            {
                for (var i = 0; i < val.PERSONEL_ID.Count(); i++)
                {
                    if (flag) sql.AppendLine("        OR");
                    sql.AppendLine("        ( A.PERSONEL_ID IN (NULL");

                    var name = string.Format(":PERSONELID{0}", i);
                    sql.AppendFormat("{0}", name);
                    prms.Add(new BindModel { Name = name, Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID.ElementAt(i), Direct = ParameterDirection.Input });
                    sql.AppendLine("))");
                    flag = true;
                }
            }

            sql.AppendLine(") A");
            sql.AppendLine("    ON M.ID = A.FUNCTION_ID");

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    M.ID");

            return db.ReadModelList<FunctionAuthorityOutModel>(sql.ToString(), prms);

        }
        #endregion

        #region 機能権限データの作成
        /// <summary>
        /// 機能権限データの作成
        /// </summary>
        /// <returns>bool</returns>
        public bool Post(List<FunctionAuthorityRegistModel> Functions)
        {
            StringBuilder sql = new StringBuilder();
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (Functions != null && Functions.Any() == true) { }

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("    FUNCTION_AUTH");
            sql.AppendLine("    (");
            sql.AppendLine("        ID,");
            sql.AppendLine("        DEPARTMENT_ID,");
            sql.AppendLine("        SECTION_ID,");
            sql.AppendLine("        SECTION_GROUP_ID,");
            sql.AppendLine("        PERSONEL_ID,");
            sql.AppendLine("        FUNCTION_ID,");
            sql.AppendLine("        READ_FLG,");
            sql.AppendLine("        UPDATE_FLG,");
            sql.AppendLine("        EXPORT_FLG,");
            sql.AppendLine("        MANAGEMENT_FLG,");
            sql.AppendLine("        INPUT_DATETIME,");
            sql.AppendLine("        INPUT_PERSONEL_ID");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("        NVL2((SELECT MAX(ID) FROM FUNCTION_AUTH), (SELECT MAX(ID) + 1 FROM FUNCTION_AUTH), 1),");
            sql.AppendLine("        :DEPARTMENTID,");
            sql.AppendLine("        :SECTIONID,");
            sql.AppendLine("        :SECTIONGROUPID,");
            sql.AppendLine("        :PERSONELID,");
            sql.AppendLine("        :FUNCTIONID,");
            sql.AppendLine("        :READFLG,");
            sql.AppendLine("        :UPDATEFLG,");
            sql.AppendLine("        :EXPORTFLG,");
            sql.AppendLine("        :MANAGEMENTFLG,");
            sql.AppendLine("        SYSDATE,");
            sql.AppendLine("        :INPUTPERSONELID");
            sql.AppendLine("    )");

            //配列処理
            foreach (var LoopF in Functions)
            {
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":DEPARTMENTID", Type = OracleDbType.Varchar2, Object = LoopF.DEPARTMENT_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTIONID", Type = OracleDbType.Varchar2, Object = LoopF.SECTION_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTIONGROUPID", Type = OracleDbType.Varchar2, Object = LoopF.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PERSONELID", Type = OracleDbType.Varchar2, Object = LoopF.PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FUNCTIONID", Type = OracleDbType.Decimal, Object = LoopF.FUNCTION_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":READFLG", Type = OracleDbType.Char, Object = LoopF.READ_FLG, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":UPDATEFLG", Type = OracleDbType.Char, Object = LoopF.UPDATE_FLG, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":EXPORTFLG", Type = OracleDbType.Char, Object = LoopF.EXPORT_FLG, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MANAGEMENTFLG", Type = OracleDbType.Char, Object = LoopF.MANAGEMENT_FLG, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUTPERSONELID", Type = OracleDbType.Varchar2, Object = LoopF.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input }
                };

                //登録
                results.Add(db.InsertData(sql.ToString(), prms));
            }

            //登録が成功したかどうか
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
        #endregion

        #region 機能権限データの更新
        /// <summary>
        /// 機能権限データの更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(List<FunctionAuthorityUpdateModel> list)
        {
            StringBuilder sql = new StringBuilder();
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                sql.AppendLine("UPDATE");
                sql.AppendLine("    FUNCTION_AUTH");
                sql.AppendLine("        SET");
                sql.AppendLine("            READ_FLG = :READFLG,");
                sql.AppendLine("            UPDATE_FLG = :UPDATEFLG,");
                sql.AppendLine("            EXPORT_FLG = :EXPORTFLG,");
                sql.AppendLine("            MANAGEMENT_FLG = :MANAGEMENTFLG,");
                sql.AppendLine("            CHANGE_DATETIME = SYSDATE,");
                sql.AppendLine("            CHANGE_PERSONEL_ID = :CHANGE_PERSONEL_ID");
                sql.AppendLine("        WHERE");
                sql.AppendLine("            FUNCTION_ID = :FUNCTIONID");
                sql.AppendLine("            AND DEPARTMENT_ID = :DEPARTMENTID");
                if (!string.IsNullOrEmpty(list[0].SECTION_ID))
                {
                    sql.AppendLine("            AND SECTION_ID = :SECTIONID");
                }
                else
                {
                    sql.AppendLine("            AND SECTION_ID IS NULL");
                }
                if (!string.IsNullOrEmpty(list[0].SECTION_GROUP_ID))
                {
                    sql.AppendLine("            AND SECTION_GROUP_ID = :SECTIONGROUPID");
                }
                else
                {
                    sql.AppendLine("            AND SECTION_GROUP_ID IS NULL");
                }
                if (!string.IsNullOrEmpty(list[0].PERSONEL_ID))
                {
                    sql.AppendLine("            AND PERSONEL_ID = :PERSONELID");
                }
                else
                {
                    sql.AppendLine("            AND PERSONEL_ID IS NULL");
                }
                //配列処理
                foreach (var LoopF in list)
                {
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":READFLG", Type = OracleDbType.Char, Object = LoopF.READ_FLG, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":UPDATEFLG", Type = OracleDbType.Char, Object = LoopF.UPDATE_FLG, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":EXPORTFLG", Type = OracleDbType.Char, Object = LoopF.EXPORT_FLG, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":MANAGEMENTFLG", Type = OracleDbType.Char, Object = LoopF.MANAGEMENT_FLG, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":CHANGE_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = LoopF.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":FUNCTIONID", Type = OracleDbType.Decimal, Object = LoopF.FUNCTION_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":DEPARTMENTID", Type = OracleDbType.Varchar2, Object = LoopF.DEPARTMENT_ID, Direct = ParameterDirection.Input }
                    };

                    if (!string.IsNullOrEmpty(LoopF.SECTION_ID))
                    {
                        prms.Add(new BindModel { Name = ":SECTIONID", Type = OracleDbType.Varchar2, Object = LoopF.SECTION_ID, Direct = ParameterDirection.Input });
                    }

                    if (!string.IsNullOrEmpty(LoopF.SECTION_GROUP_ID))
                    {
                        prms.Add(new BindModel { Name = ":SECTIONGROUPID", Type = OracleDbType.Varchar2, Object = LoopF.SECTION_GROUP_ID, Direct = ParameterDirection.Input });
                    }

                    if (!string.IsNullOrEmpty(LoopF.PERSONEL_ID))
                    {
                        prms.Add(new BindModel { Name = ":PERSONELID", Type = OracleDbType.Varchar2, Object = LoopF.PERSONEL_ID, Direct = ParameterDirection.Input });
                    }

                    //更新
                    results.Add(db.UpdateData(sql.ToString(), prms));
                }
            }

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
        #endregion

        #region 機能権限データの削除
        /// <summary>
        /// 機能権限データの削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(List<FunctionAuthorityDeleteModel> Functions)
        {
            StringBuilder sql = new StringBuilder();
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //削除対象があるかどうか
            if (Functions != null && Functions.Any() == true)
            {
                sql.AppendLine("DELETE FROM");
                sql.AppendLine("        FUNCTION_AUTH");
                sql.AppendLine("    WHERE");
                sql.AppendLine("        DEPARTMENT_ID = :DEPARTMENTID");
                if (!string.IsNullOrEmpty(Functions[0].SECTION_ID))
                {
                    sql.AppendLine("    AND SECTION_ID = :SECTIONID");
                }
                else
                {
                    sql.AppendLine("    AND SECTION_ID IS NULL");
                }
                if (!string.IsNullOrEmpty(Functions[0].SECTION_GROUP_ID))
                {
                    sql.AppendLine("    AND SECTION_GROUP_ID = :SECTIONGROUPID");
                }
                else
                {
                    sql.AppendLine("    AND SECTION_GROUP_ID IS NULL");
                }
                if (!string.IsNullOrEmpty(Functions[0].PERSONEL_ID))
                {
                    sql.AppendLine("    AND PERSONEL_ID = :PERSONELID");
                }
                else
                {
                    sql.AppendLine("    AND PERSONEL_ID IS NULL");
                }
                sql.AppendLine("    AND FUNCTION_ID = :FUNCTIONID");

                //配列処理
                foreach (var LoopF in Functions)
                {
                    var prms = new List<BindModel>
                    {
                        new BindModel { Name = ":DEPARTMENTID", Type = OracleDbType.Varchar2, Object = LoopF.DEPARTMENT_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":SECTIONID", Type = OracleDbType.Varchar2, Object = LoopF.SECTION_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":SECTIONGROUPID", Type = OracleDbType.Varchar2, Object = LoopF.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":PERSONELID", Type = OracleDbType.Varchar2, Object = LoopF.PERSONEL_ID, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":FUNCTIONID", Type = OracleDbType.Decimal, Object = LoopF.FUNCTION_ID, Direct = ParameterDirection.Input }
                    };

                    //削除
                    results.Add(db.DeleteData(sql.ToString(), prms));
                }
            }

            //削除が成功したかどうか
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
        #endregion
    }
}