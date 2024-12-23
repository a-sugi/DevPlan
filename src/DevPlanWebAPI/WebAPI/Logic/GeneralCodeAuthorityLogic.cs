using System.Collections.Generic;
using System.Data;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using System.Linq;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 開発符号権限ロジッククラス
    /// </summary>
    /// <remarks>開発符号権限データの操作</remarks>
    public class GeneralCodeAuthorityLogic : BaseLogic
    {
        #region 開発符号権限データの取得
        /// <summary>
        /// 開発符号権限データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public List<GeneralCodeAuthorityOutModel> GetData(GeneralCodeAuthorityInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    DPT.DEPARTMENT_ID");
            sql.AppendLine("   ,DPT.DEPARTMENT_CODE");
            sql.AppendLine("   ,DPT.DEPARTMENT_NAME");
            sql.AppendLine("   ,GRP.請負関係課_ID SECTION_RELATIONAL_ID");
            sql.AppendLine("   ,SCT.SECTION_ID");
            sql.AppendLine("   ,SCT.SECTION_CODE");
            sql.AppendLine("   ,SCT.SECTION_NAME");
            sql.AppendLine("   ,GRP.SECTION_GROUP_ID");
            sql.AppendLine("   ,GRP.SECTION_GROUP_CODE");
            sql.AppendLine("   ,GRP.SECTION_GROUP_NAME");
            sql.AppendLine("   ,PSN.PERSONEL_ID");
            sql.AppendLine("   ,PSN.NAME");
            sql.AppendLine("   ,PSN.STATUS_CODE");
            sql.AppendLine("   ,PSN.COMPANY");
            sql.AppendLine("   ,PSN.ACCESS_LEVEL");
            sql.AppendLine("   ,PSN.OFFICIAL_POSITION");
            sql.AppendLine("   ,GNR.CAR_GROUP");
            sql.AppendLine("   ,GNR.GENERAL_CODE");
            sql.AppendLine("   ,GNR.UNDER_DEVELOPMENT");
            sql.AppendLine("   ,ATH.PERMISSION_PERIOD_START");
            sql.AppendLine("   ,ATH.PERMISSION_PERIOD_END");
            sql.AppendLine("FROM ");
            sql.AppendLine("    試験計画_他部署閲覧許可 ATH");
            sql.AppendLine("    INNER JOIN PERSONEL_LIST PSN");
            sql.AppendLine("    ON PSN.PERSONEL_ID = ATH.PERSONEL_ID");
            sql.AppendLine("    INNER JOIN SECTION_GROUP_DATA GRP");
            sql.AppendLine("    ON PSN.SECTION_GROUP_ID = GRP.SECTION_GROUP_ID");
            sql.AppendLine("    INNER JOIN SECTION_DATA SCT");
            sql.AppendLine("    ON GRP.SECTION_ID = SCT.SECTION_ID");
            sql.AppendLine("    INNER JOIN DEPARTMENT_DATA DPT");
            sql.AppendLine("    ON SCT.DEPARTMENT_ID = DPT.DEPARTMENT_ID");
            sql.AppendLine("    INNER JOIN GENERAL_CODE GNR");
            sql.AppendLine("    ON GNR.GENERAL_CODE = ATH.GENERAL_CODE");

            sql.AppendLine("WHERE 0 = 0");

            //部ID
            if (val.DEPARTMENT_ID != null)
            {
                sql.AppendLine("    AND DPT.DEPARTMENT_ID = :DEPARTMENT_ID");
                prms.Add(new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = val.DEPARTMENT_ID, Direct = ParameterDirection.Input });
            }

            //課ID && 請負関係課ID
            if (!string.IsNullOrWhiteSpace(val.SECTION_ID) || !string.IsNullOrWhiteSpace(val.SECTION_RELATIONAL_ID))
            {
                sql.AppendLine("    AND (");

                if (!string.IsNullOrWhiteSpace(val.SECTION_ID))
                {
                    sql.AppendLine("        SCT.SECTION_ID = :SECTION_ID");
                    prms.Add(new BindModel { Name = ":SECTION_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_ID, Direct = ParameterDirection.Input });
                }

                if (!string.IsNullOrWhiteSpace(val.SECTION_ID) && !string.IsNullOrWhiteSpace(val.SECTION_RELATIONAL_ID))
                {
                    sql.AppendLine("        OR");
                }

                if (!string.IsNullOrWhiteSpace(val.SECTION_RELATIONAL_ID))
                {
                    sql.AppendLine("        GRP.請負関係課_ID = :SECTION_RELATIONAL_ID");
                    prms.Add(new BindModel { Name = ":SECTION_RELATIONAL_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_RELATIONAL_ID, Direct = ParameterDirection.Input });
                }

                sql.AppendLine("    )");
            }

            //担当ID
            if (!string.IsNullOrWhiteSpace(val.SECTION_GROUP_ID))
            {
                sql.AppendLine("    AND GRP.SECTION_GROUP_ID = :SECTION_GROUP_ID");
                prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID, Direct = ParameterDirection.Input });
            }

            //ユーザーID
            if (!string.IsNullOrWhiteSpace(val.PERSONEL_ID))
            {
                sql.AppendLine("    AND PSN.PERSONEL_ID = :PERSONEL_ID");
                prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
            }

            //ユーザーステータスコード
            if (!string.IsNullOrWhiteSpace(val.STATUS_CODE))
            {
                sql.AppendLine("    AND PSN.STATUS_CODE = :STATUS_CODE");
                prms.Add(new BindModel { Name = ":STATUS_CODE", Type = OracleDbType.Varchar2, Object = val.STATUS_CODE, Direct = ParameterDirection.Input });
            }

            //開発フラグ
            if (!string.IsNullOrWhiteSpace(val.UNDER_DEVELOPMENT))
            {
                sql.AppendLine("    AND GNR.UNDER_DEVELOPMENT = :UNDER_DEVELOPMENT");
                prms.Add(new BindModel { Name = ":UNDER_DEVELOPMENT", Type = OracleDbType.Int16, Object = val.UNDER_DEVELOPMENT, Direct = ParameterDirection.Input });
            }

            //先開フラグ
            if (!val.PRE_FLG)
            {
                sql.AppendLine("    AND GNR.CAR_GROUP <> '先開'");
            }

            //派遣・外注フラグ
            if (!val.BP_FLG)
            {
                sql.AppendLine("    AND PSN.ACCESS_LEVEL <> '50'");
            }

            //ORDER BY
            sql.AppendLine("ORDER BY DPT.DEPARTMENT_ID");
            sql.AppendLine("    ,SCT.SECTION_ID");
            sql.AppendLine("    ,GRP.SECTION_GROUP_ID");
            sql.AppendLine("    ,PSN.PERSONEL_ID");
            sql.AppendLine("    ,GNR.SORT_NUMBER");
            sql.AppendLine("    ,GNR.GENERAL_CODE");

            return db.ReadModelList<GeneralCodeAuthorityOutModel>(sql.ToString(), prms);
        }
        #endregion

        #region 開発符号権限データの作成
        /// <summary>
        /// 開発符号権限データの作成
        /// </summary>
        /// <returns>bool</returns>
        public bool Post(List<GeneralCodeAuthorityEntryModel> list)
        {
            var sql = new StringBuilder();
            var sql_flg = new StringBuilder();

            //登録対象があるかどうか
            if (list == null && list.Any() != true)
            {
                return false;
            }

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("    試験計画_他部署閲覧許可");
            sql.AppendLine("    (");
            sql.AppendLine("         PERSONEL_ID");
            sql.AppendLine("        ,GENERAL_CODE");
            sql.AppendLine("        ,PERMISSION_PERIOD_START");
            sql.AppendLine("        ,PERMISSION_PERIOD_END");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("        :PERSONEL_ID,");
            sql.AppendLine("        :GENERAL_CODE,");
            sql.AppendLine("        :PERMISSION_PERIOD_START,");
            sql.AppendLine("        :PERMISSION_PERIOD_END,");
            sql.AppendLine("    )");

            sql_flg.AppendLine("UPDATE");
            sql_flg.AppendLine("    試験計画_権限解除状況");
            sql_flg.AppendLine("SET");
            sql_flg.AppendLine("     他部署閲覧権限 = 0");
            sql_flg.AppendLine("WHERE");
            sql_flg.AppendLine("    PERSONEL_ID = :PERSONEL_ID");

            //トランザクション開始
            db.Begin();

            var id = string.Empty;

            //配列処理
            foreach (var val in list)
            {
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = val.GENERAL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PERMISSION_PERIOD_START", Type = OracleDbType.Date, Object = val.PERMISSION_PERIOD_START, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PERMISSION_PERIOD_END", Type = OracleDbType.Date, Object = val.PERMISSION_PERIOD_END, Direct = ParameterDirection.Input }
                };

                //登録
                if (!db.InsertData(sql.ToString(), prms))
                {
                    //ロールバック
                    db.Rollback();

                    return false;
                }

                if (id == val.PERSONEL_ID)
                {
                    continue;
                }

                //状況テーブル更新
                if (!db.UpdateData(sql_flg.ToString(), prms))
                {
                    //ロールバック
                    db.Rollback();

                    return false;
                }

                // 退避
                id = val.PERSONEL_ID;
            }

            //コミット
            db.Commit();

            return true;
        }
        #endregion

        #region 開発符号権限データの登録更新
        /// <summary>
        /// 開発符号権限データの登録更新
        /// </summary>
        /// <returns>bool</returns>
        public bool MergeData(List<GeneralCodeAuthorityEntryModel> list)
        {
            var sql = new StringBuilder();
            var sql_flg = new StringBuilder();
            //Append Start 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする
            var sql_tmp = new StringBuilder();
            //Append End 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする

            //登録対象があるかどうか
            if (list == null || list.Any() != true)
            {
                return false;
            }

            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    試験計画_他部署閲覧許可 A");
            sql.AppendLine("        USING");
            sql.AppendLine("        (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             :PERSONEL_ID PERSONEL_ID");
            sql.AppendLine("            ,:GENERAL_CODE GENERAL_CODE");
            sql.AppendLine("        FROM");
            sql.AppendLine("            DUAL");
            sql.AppendLine("        ) B");
            sql.AppendLine("    ON");
            sql.AppendLine("    (");
            sql.AppendLine("            A.PERSONEL_ID = B.PERSONEL_ID");
            sql.AppendLine("        AND A.GENERAL_CODE = B.GENERAL_CODE");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("         PERMISSION_PERIOD_START = :PERMISSION_PERIOD_START");
            sql.AppendLine("        ,PERMISSION_PERIOD_END = :PERMISSION_PERIOD_END");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         PERSONEL_ID");
            sql.AppendLine("        ,GENERAL_CODE");
            sql.AppendLine("        ,PERMISSION_PERIOD_START");
            sql.AppendLine("        ,PERMISSION_PERIOD_END");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("        :PERSONEL_ID,");
            sql.AppendLine("        :GENERAL_CODE,");
            sql.AppendLine("        :PERMISSION_PERIOD_START,");
            sql.AppendLine("        :PERMISSION_PERIOD_END");
            sql.AppendLine("    )");

            sql_flg.AppendLine("UPDATE");
            sql_flg.AppendLine("    試験計画_権限解除状況");
            sql_flg.AppendLine("SET");
            sql_flg.AppendLine("     他部署閲覧権限 = 0");
            sql_flg.AppendLine("WHERE");
            sql_flg.AppendLine("    PERSONEL_ID = :PERSONEL_ID");

            //Append Start 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする
            sql_tmp.AppendLine("MERGE INTO");
            sql_tmp.AppendLine("    試験計画_派遣者閲覧可能車種 A");
            sql_tmp.AppendLine("        USING");
            sql_tmp.AppendLine("        (");
            sql_tmp.AppendLine("        SELECT");
            sql_tmp.AppendLine("             :PERSONEL_ID PERSONEL_ID");
            sql_tmp.AppendLine("            ,:GENERAL_CODE GENERAL_CODE");
            sql_tmp.AppendLine("        FROM");
            sql_tmp.AppendLine("            DUAL");
            sql_tmp.AppendLine("        ) B");
            sql_tmp.AppendLine("    ON");
            sql_tmp.AppendLine("    (");
            sql_tmp.AppendLine("            A.PERSONEL_ID = B.PERSONEL_ID");
            sql_tmp.AppendLine("        AND A.GENERAL_CODE = B.GENERAL_CODE");
            sql_tmp.AppendLine("    )");
            sql_tmp.AppendLine("WHEN MATCHED THEN");
            sql_tmp.AppendLine("    UPDATE SET");
            sql_tmp.AppendLine("         PERMISSION_PERIOD_START = :PERMISSION_PERIOD_START");
            sql_tmp.AppendLine("        ,PERMISSION_PERIOD_END = :PERMISSION_PERIOD_END");
            sql_tmp.AppendLine("WHEN NOT MATCHED THEN");
            sql_tmp.AppendLine("    INSERT");
            sql_tmp.AppendLine("    (");
            sql_tmp.AppendLine("         PERSONEL_ID");
            sql_tmp.AppendLine("        ,GENERAL_CODE");
            sql_tmp.AppendLine("        ,PERMISSION_PERIOD_START");
            sql_tmp.AppendLine("        ,PERMISSION_PERIOD_END");
            sql_tmp.AppendLine("    )");
            sql_tmp.AppendLine("    VALUES");
            sql_tmp.AppendLine("    (");
            sql_tmp.AppendLine("        :PERSONEL_ID,");
            sql_tmp.AppendLine("        :GENERAL_CODE,");
            sql_tmp.AppendLine("        :PERMISSION_PERIOD_START,");
            sql_tmp.AppendLine("        :PERMISSION_PERIOD_END");
            sql_tmp.AppendLine("    )");
            //Append End 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする

            //トランザクション開始
            db.Begin();

            var id = string.Empty;

            //配列処理
            foreach (var val in list)
            {
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = val.GENERAL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PERMISSION_PERIOD_START", Type = OracleDbType.Date, Object = val.PERMISSION_PERIOD_START, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PERMISSION_PERIOD_END", Type = OracleDbType.Date, Object = val.PERMISSION_PERIOD_END, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input }
                };

                //権限テーブル更新
                if (!db.UpdateData(sql.ToString(), prms))
                {
                    //ロールバック
                    db.Rollback();

                    return false;
                }

                //Append Start 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする
                if (val.ACCESS_LEVEL == "50")
                {
                    //派遣者閲覧可能車種テーブル更新
                    if (!db.UpdateData(sql_tmp.ToString(), prms))
                    {
                        //ロールバック
                        db.Rollback();

                        return false;
                    }
                }
                //Append End 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする

                if (id == val.PERSONEL_ID)
                {
                    continue;
                }

                //状況テーブル更新
                if (!db.UpdateData(sql_flg.ToString(), prms))
                {
                    //ロールバック
                    db.Rollback();

                    return false;
                }

                // 退避
                id = val.PERSONEL_ID;
            }

            //コミット
            db.Commit();

            return true;
        }
        #endregion

        #region 開発符号権限データの削除
        /// <summary>
        /// 開発符号権限データの削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(List<GeneralCodeAuthorityDeleteModel> GeneralCodes)
        {
            var sql = new StringBuilder();

            //削除対象があるかどうか
            if (GeneralCodes == null || GeneralCodes.Any() != true)
            {
                return false;
            }

            sql.AppendLine("DELETE FROM");
            sql.AppendLine("        試験計画_他部署閲覧許可");
            sql.AppendLine("    WHERE 0 = 0");


            //ユーザーID
            if (!string.IsNullOrWhiteSpace(GeneralCodes[0].PERSONEL_ID))
            {
                sql.AppendLine("    AND PERSONEL_ID = :PERSONEL_ID");
            }

            //開発符号
            if (!string.IsNullOrWhiteSpace(GeneralCodes[0].GENERAL_CODE))
            {
                sql.AppendLine("    AND GENERAL_CODE = :GENERAL_CODE");
            }

            //Append Start 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする
            var sql_sub = new StringBuilder();
            sql_sub.AppendLine("DELETE FROM");
            sql_sub.AppendLine("        試験計画_派遣者閲覧可能車種");
            sql_sub.AppendLine("    WHERE 0 = 0");
            //ユーザーID
            if (!string.IsNullOrWhiteSpace(GeneralCodes[0].PERSONEL_ID))
            {
                sql.AppendLine("    AND PERSONEL_ID = :PERSONEL_ID");
            }

            //開発符号
            if (!string.IsNullOrWhiteSpace(GeneralCodes[0].GENERAL_CODE))
            {
                sql.AppendLine("    AND GENERAL_CODE = :GENERAL_CODE");
            }
            //Append End 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする

            //トランザクション開始
            db.Begin();

            //配列処理
            foreach (var LoopF in GeneralCodes)
            {
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = LoopF.PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = LoopF.GENERAL_CODE, Direct = ParameterDirection.Input }
                };

                //削除
                if (db.DeleteData(sql.ToString(), prms))
                {
                    //ロールバック
                    db.Rollback();

                    return false;
                }
                
                //派遣者閲覧可能車種削除
                //削除
                if (db.DeleteData(sql_sub.ToString(), prms))
                {
                    //ロールバック
                    db.Rollback();

                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}