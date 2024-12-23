using System.Data;
using System.Text;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using System;
using System.Linq;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>ロール権限関連業務ロジック</remarks>
    public class RollAuthorityLogic : BaseLogic
    {
        private Func<string, Object> convDBNull = str => string.IsNullOrWhiteSpace(str) ? DBNull.Value : (object)str;

        /// <summary>
        /// ロール権限の取得
        /// </summary>
        /// <returns>List</returns>
        public List<RollAuthorityGetOutModel> GetData(RollAuthorityGetInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    ATH.ID");
            sql.AppendLine("   ,ATH.DEPARTMENT_ID");
            sql.AppendLine("   ,ATH.SECTION_ID");
            sql.AppendLine("   ,ATH.SECTION_GROUP_ID");
            sql.AppendLine("   ,ATH.OFFICIAL_POSITION");
            sql.AppendLine("   ,ATH.PERSONEL_ID");
            sql.AppendLine("   ,ATH.ROLL_ID");
            sql.AppendLine("   ,DPT.DEPARTMENT_NAME");
            sql.AppendLine("   ,DPT.DEPARTMENT_CODE");
            sql.AppendLine("   ,DPT.DEPARTMENT_ID");
            sql.AppendLine("   ,SCT.SECTION_NAME");
            sql.AppendLine("   ,SCT.SECTION_CODE");
            sql.AppendLine("   ,SCT.SECTION_ID");
            sql.AppendLine("   ,SGP.SECTION_GROUP_NAME");
            sql.AppendLine("   ,SGP.SECTION_GROUP_CODE");
            sql.AppendLine("   ,SGP.SECTION_GROUP_ID");
            sql.AppendLine("   ,PSN.NAME");
            sql.AppendLine("   ,PSN.PERSONEL_ID");
            sql.AppendLine("FROM");
            sql.AppendLine("        ROLL_AUTH ATH");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        DEPARTMENT_DATA DPT");
            sql.AppendLine("    ON");
            sql.AppendLine("        ATH.DEPARTMENT_ID = DPT.DEPARTMENT_ID");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        SECTION_DATA SCT");
            sql.AppendLine("    ON");
            sql.AppendLine("        ATH.SECTION_ID = SCT.SECTION_ID");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        SECTION_GROUP_DATA SGP");
            sql.AppendLine("    ON");
            sql.AppendLine("        ATH.SECTION_GROUP_ID = SGP.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        PERSONEL_LIST PSN");
            sql.AppendLine("    ON");
            sql.AppendLine("        ATH.PERSONEL_ID = PSN.PERSONEL_ID");
            sql.AppendLine("WHERE 0 = 0");

            // ロールID
            if (val.ROLL_ID != null)
            {
                sql.AppendLine("AND ATH.ROLL_ID = :ROLL_ID");

                prms.Add(new BindModel { Name = ":ROLL_ID", Type = OracleDbType.Int64, Object = val.ROLL_ID, Direct = ParameterDirection.Input });
            }

            // 種別（0:人, 1:部署・役職）
            if (val.TYPE == 0)
            {
                // ユーザーID
                if (val.PERSONEL_ID != null)
                {
                    sql.AppendLine("     AND (ATH.DEPARTMENT_ID IS NULL          AND ATH.SECTION_ID IS NULL       AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION IS NULL              AND ATH.PERSONEL_ID = :PERSONEL_ID)");

                    prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
                }

                sql.AppendLine("    AND ATH.PERSONEL_ID IS NOT NULL");
            }
            else if (val.TYPE == 1)
            {
                // 部ID・課ID・担当ID・役職
                if (val.DEPARTMENT_ID != null || val.SECTION_ID != null || val.SECTION_GROUP_ID != null || val.OFFICIAL_POSITION != null)
                {
                    sql.AppendLine("    AND (");

                    // 部署・役職
                    sql.AppendLine("        (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID = :SECTION_ID AND ATH.SECTION_GROUP_ID = :SECTION_GROUP_ID AND ATH.OFFICIAL_POSITION = :OFFICIAL_POSITION AND ATH.PERSONEL_ID IS NULL)");
                    sql.AppendLine("     OR (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID = :SECTION_ID AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION = :OFFICIAL_POSITION AND ATH.PERSONEL_ID IS NULL)");
                    sql.AppendLine("     OR (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID IS NULL       AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION = :OFFICIAL_POSITION AND ATH.PERSONEL_ID IS NULL)");

                    // 部署
                    sql.AppendLine("     OR (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID = :SECTION_ID AND ATH.SECTION_GROUP_ID = :SECTION_GROUP_ID AND ATH.OFFICIAL_POSITION IS NULL              AND ATH.PERSONEL_ID IS NULL)");
                    sql.AppendLine("     OR (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID = :SECTION_ID AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION IS NULL              AND ATH.PERSONEL_ID IS NULL)");
                    sql.AppendLine("     OR (ATH.DEPARTMENT_ID = :DEPARTMENT_ID AND ATH.SECTION_ID IS NULL       AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION IS NULL              AND ATH.PERSONEL_ID IS NULL)");

                    // 役職
                    sql.AppendLine("     OR (ATH.DEPARTMENT_ID IS NULL          AND ATH.SECTION_ID IS NULL       AND ATH.SECTION_GROUP_ID IS NULL             AND ATH.OFFICIAL_POSITION = :OFFICIAL_POSITION AND ATH.PERSONEL_ID IS NULL)");

                    sql.AppendLine("    )");

                    prms.Add(new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = val.DEPARTMENT_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":OFFICIAL_POSITION", Type = OracleDbType.Varchar2, Object = val.OFFICIAL_POSITION, Direct = ParameterDirection.Input });
                }

                sql.AppendLine("    AND ATH.PERSONEL_ID IS NULL");
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    DPT.SORT_NO");
            sql.AppendLine("   ,SCT.SORT_NO");
            sql.AppendLine("   ,SGP.SORT_NO");
            sql.AppendLine("   ,PSN.PERSONEL_ID");
            sql.AppendLine("   ,ATH.ROLL_ID");

            return db.ReadModelList<RollAuthorityGetOutModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// ロール権限の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(List<RollAuthorityPostInModel> list)
        {
            if (list?.Count <= 0) return false;

            var symbol = list.FirstOrDefault();

            var del_sql = new StringBuilder();

            del_sql.AppendLine("DELETE");
            del_sql.AppendLine("FROM");
            del_sql.AppendLine("    ROLL_AUTH");
            del_sql.AppendLine("WHERE");
            del_sql.AppendLine("    ROLL_ID = :ROLL_ID");

            // 種別（0:人, 1:部署・役職）
            if (symbol.TYPE == 0)
            {
                del_sql.AppendLine("    AND PERSONEL_ID IS NOT NULL");
            }
            else if (symbol.TYPE == 1)
            {
                del_sql.AppendLine("    AND PERSONEL_ID IS NULL");
            }

            //トランザクション開始
            db.Begin();

            //パラメータ設定
            var del_prms = new List<BindModel>()
            {
                new BindModel { Name = ":ROLL_ID", Type = OracleDbType.Int64, Object = symbol.ROLL_ID, Direct = ParameterDirection.Input }
            };

            //削除の実行
            if (!db.DeleteData(del_sql.ToString(), del_prms))
            {
                db.Rollback();
                return false;
            }

            var sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("ROLL_AUTH (");
            sql.AppendLine("    ID");
            sql.AppendLine("   ,DEPARTMENT_ID");
            sql.AppendLine("   ,SECTION_ID");
            sql.AppendLine("   ,SECTION_GROUP_ID");
            sql.AppendLine("   ,OFFICIAL_POSITION");
            sql.AppendLine("   ,PERSONEL_ID");
            sql.AppendLine("   ,ROLL_ID");
            sql.AppendLine("   ,INPUT_DATETIME");
            sql.AppendLine("   ,INPUT_PERSONEL_ID");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("    (SELECT NVL(MAX(ID), 0) + 1 FROM ROLL_AUTH)");
            sql.AppendLine("   ,:DEPARTMENT_ID");
            sql.AppendLine("   ,:SECTION_ID");
            sql.AppendLine("   ,:SECTION_GROUP_ID");
            sql.AppendLine("   ,:OFFICIAL_POSITION");
            sql.AppendLine("   ,:PERSONEL_ID");
            sql.AppendLine("   ,:ROLL_ID");
            sql.AppendLine("   ,SYSDATE");
            sql.AppendLine("   ,:INPUT_PERSONEL_ID");
            sql.AppendLine(")");

            foreach (var val in list)
            {
                var prms = new List<BindModel>();

                //パラメータ設定
                if (val.TYPE == 0)
                {
                    prms.Add(new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = DBNull.Value, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_ID", Type = OracleDbType.Varchar2, Object = DBNull.Value, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = DBNull.Value, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":OFFICIAL_POSITION", Type = OracleDbType.Varchar2, Object = DBNull.Value, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
                }
                else if (val.TYPE == 1)
                {
                    prms.Add(new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = convDBNull(val.DEPARTMENT_ID), Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_ID", Type = OracleDbType.Varchar2, Object = convDBNull(val.SECTION_ID), Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = convDBNull(val.SECTION_GROUP_ID), Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":OFFICIAL_POSITION", Type = OracleDbType.Varchar2, Object = convDBNull(val.OFFICIAL_POSITION), Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = DBNull.Value, Direct = ParameterDirection.Input });
                }

                prms.Add(new BindModel { Name = ":ROLL_ID", Type = OracleDbType.Int64, Object = val.ROLL_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input });

                //追加の実行
                if (!db.InsertData(sql.ToString(), prms))
                {
                    db.Rollback();
                    return false;
                }
            }

            //コミット
            db.Commit();

            return true;
        }

        /// <summary>
        /// ロール権限の付替
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(List<RollAuthorityPutInModel> list)
        {
            if (list?.Count <= 0) return false;

            var symbol = list.FirstOrDefault();

            var del_sql = new StringBuilder();

            del_sql.AppendLine("DELETE");
            del_sql.AppendLine("FROM");
            del_sql.AppendLine("    ROLL_AUTH");
            del_sql.AppendLine("WHERE");
            del_sql.AppendLine("    0 = 0");

            // 種別（0:人, 1:部署・役職）
            if (list.Any(x => x.TYPE == 0))
            {
                del_sql.AppendLine("    AND PERSONEL_ID = :PERSONEL_ID");
            }
            else if (list.Any(x => x.TYPE == 1))
            {
                // 部ID
                if (!string.IsNullOrWhiteSpace(symbol.DEPARTMENT_ID))
                {
                    del_sql.AppendLine("    AND DEPARTMENT_ID = :DEPARTMENT_ID");
                }
                else
                {
                    del_sql.AppendLine("    AND DEPARTMENT_ID IS NULL");
                }

                // 課ID
                if (!string.IsNullOrWhiteSpace(symbol.SECTION_ID))
                {
                    del_sql.AppendLine("    AND SECTION_ID = :SECTION_ID");
                }
                else
                {
                    del_sql.AppendLine("    AND SECTION_ID IS NULL");
                }

                // 担当ID
                if (!string.IsNullOrWhiteSpace(symbol.SECTION_GROUP_ID))
                {
                    del_sql.AppendLine("    AND SECTION_GROUP_ID = :SECTION_GROUP_ID");
                }
                else
                {
                    del_sql.AppendLine("    AND SECTION_GROUP_ID IS NULL");
                }

                // 役職
                if (!string.IsNullOrWhiteSpace(symbol.OFFICIAL_POSITION))
                {
                    del_sql.AppendLine("    AND OFFICIAL_POSITION = :OFFICIAL_POSITION");
                }
                else
                {
                    del_sql.AppendLine("    AND OFFICIAL_POSITION IS NULL");
                }

                del_sql.AppendLine("    AND PERSONEL_ID IS NULL");
            }

            //トランザクション開始
            db.Begin();

            //パラメータ設定
            var del_prms = new List<BindModel>()
            {
                new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = symbol.DEPARTMENT_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SECTION_ID", Type = OracleDbType.Varchar2, Object = symbol.SECTION_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = symbol.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":OFFICIAL_POSITION", Type = OracleDbType.Varchar2, Object = symbol.OFFICIAL_POSITION, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = symbol.PERSONEL_ID, Direct = ParameterDirection.Input },
            };

            //削除の実行
            if (!db.DeleteData(del_sql.ToString(), del_prms))
            {
                db.Rollback();
                return false;
            }

            var sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("ROLL_AUTH (");
            sql.AppendLine("    ID");
            sql.AppendLine("   ,DEPARTMENT_ID");
            sql.AppendLine("   ,SECTION_ID");
            sql.AppendLine("   ,SECTION_GROUP_ID");
            sql.AppendLine("   ,OFFICIAL_POSITION");
            sql.AppendLine("   ,PERSONEL_ID");
            sql.AppendLine("   ,ROLL_ID");
            sql.AppendLine("   ,INPUT_DATETIME");
            sql.AppendLine("   ,INPUT_PERSONEL_ID");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("    (SELECT NVL(MAX(ID), 0) + 1 FROM ROLL_AUTH)");
            sql.AppendLine("   ,:DEPARTMENT_ID");
            sql.AppendLine("   ,:SECTION_ID");
            sql.AppendLine("   ,:SECTION_GROUP_ID");
            sql.AppendLine("   ,:OFFICIAL_POSITION");
            sql.AppendLine("   ,:PERSONEL_ID");
            sql.AppendLine("   ,:ROLL_ID");
            sql.AppendLine("   ,SYSDATE");
            sql.AppendLine("   ,:INPUT_PERSONEL_ID");
            sql.AppendLine(")");

            foreach (var val in list)
            {
                var prms = new List<BindModel>();

                //パラメータ設定
                if (val.TYPE == 0)
                {
                    prms.Add(new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = DBNull.Value, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_ID", Type = OracleDbType.Varchar2, Object = DBNull.Value, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = DBNull.Value, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":OFFICIAL_POSITION", Type = OracleDbType.Varchar2, Object = DBNull.Value, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
                }
                else if (val.TYPE == 1)
                {
                    prms.Add(new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = convDBNull(val.DEPARTMENT_ID), Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_ID", Type = OracleDbType.Varchar2, Object = convDBNull(val.SECTION_ID), Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = convDBNull(val.SECTION_GROUP_ID), Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":OFFICIAL_POSITION", Type = OracleDbType.Varchar2, Object = convDBNull(val.OFFICIAL_POSITION), Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = DBNull.Value, Direct = ParameterDirection.Input });
                }

                prms.Add(new BindModel { Name = ":ROLL_ID", Type = OracleDbType.Int64, Object = val.ROLL_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input });

                //追加の実行
                if (!db.InsertData(sql.ToString(), prms))
                {
                    db.Rollback();
                    return false;
                }
            }

            //コミット
            db.Commit();

            return true;
        }

        /// <summary>
        /// ロール権限の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(RollAuthorityDeleteInModel val)
        {
            // いずれか必須
            if (!val.GetType().GetProperties().Any(x => x.GetValue(val, null) != null))
            {
                return false;
            }

            var sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    ROLL_AUTH");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // 種別（0:人, 1:部署・役職）
            if (val.TYPE == 0)
            {
                if (!string.IsNullOrWhiteSpace(val.PERSONEL_ID))
                {
                    sql.AppendLine("    AND PERSONEL_ID = :PERSONEL_ID");
                }
                else
                {
                    sql.AppendLine("    AND PERSONEL_ID IS NOT NULL");
                }
            }
            else if (val.TYPE == 1)
            {
                if (!string.IsNullOrWhiteSpace(val.DEPARTMENT_ID) ||
                    !string.IsNullOrWhiteSpace(val.SECTION_ID) ||
                    !string.IsNullOrWhiteSpace(val.SECTION_GROUP_ID) ||
                    !string.IsNullOrWhiteSpace(val.OFFICIAL_POSITION))
                {
                    // 部ID
                    if (!string.IsNullOrWhiteSpace(val.DEPARTMENT_ID))
                    {
                        sql.AppendLine("    AND DEPARTMENT_ID = :DEPARTMENT_ID");
                    }
                    else
                    {
                        sql.AppendLine("    AND DEPARTMENT_ID IS NULL");
                    }

                    // 課ID
                    if (!string.IsNullOrWhiteSpace(val.SECTION_ID))
                    {
                        sql.AppendLine("    AND SECTION_ID = :SECTION_ID");
                    }
                    else
                    {
                        sql.AppendLine("    AND SECTION_ID IS NULL");
                    }

                    // 担当ID
                    if (!string.IsNullOrWhiteSpace(val.SECTION_GROUP_ID))
                    {
                        sql.AppendLine("    AND SECTION_GROUP_ID = :SECTION_GROUP_ID");
                    }
                    else
                    {
                        sql.AppendLine("    AND SECTION_GROUP_ID IS NULL");
                    }

                    // 役職
                    if (!string.IsNullOrWhiteSpace(val.OFFICIAL_POSITION))
                    {
                        sql.AppendLine("    AND OFFICIAL_POSITION = :OFFICIAL_POSITION");
                    }
                    else
                    {
                        sql.AppendLine("    AND OFFICIAL_POSITION IS NULL");
                    }
                }

                sql.AppendLine("    AND PERSONEL_ID IS NULL");
            }

            // ロールID
            if (val.ROLL_ID > 0)
            {
                sql.AppendLine("    AND ROLL_ID = :ROLL_ID");
            }

            //パラメータ設定
            var prms = new List<BindModel>()
            {
                new BindModel { Name = ":ROLL_ID", Type = OracleDbType.Int64, Object = val.ROLL_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = val.DEPARTMENT_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SECTION_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":OFFICIAL_POSITION", Type = OracleDbType.Varchar2, Object = val.OFFICIAL_POSITION, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input }
            };

            //トランザクション開始
            db.Begin();

            if (!db.DeleteData(sql.ToString(), prms))
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