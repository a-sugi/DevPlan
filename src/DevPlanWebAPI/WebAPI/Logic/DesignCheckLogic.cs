using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 設計チェックロジッククラス
    /// </summary>
    /// <remarks>設計チェックの操作</remarks>
    public class DesignCheckLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 設計チェックの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(DesignCheckGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     試験計画_DCHK_開催日.ID");
            sql.AppendLine("    ,試験計画_DCHK_開催日.開催日");
            sql.AppendLine("    ,試験計画_DCHK_開催日.回");
            sql.AppendLine("    ,試験計画_DCHK_開催日.名称");
            sql.AppendLine("    ,COUNT(CASE WHEN 試験計画_DCHK_指摘リスト.ID IS NOT NULL AND NVL(試験計画_DCHK_指摘リスト.FLAG_CLOSE, 0) <> 1 THEN 1 END) OPEN_COUNT");
            sql.AppendLine("    ,COUNT(CASE WHEN 試験計画_DCHK_指摘リスト.ID IS NOT NULL AND NVL(試験計画_DCHK_指摘リスト.FLAG_CLOSE, 0) = 1 THEN 1 END) CLOSE_COUNT");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_開催日");
            sql.AppendLine("    LEFT JOIN 試験計画_DCHK_指摘リスト");
            sql.AppendLine("    ON 試験計画_DCHK_開催日.ID = 試験計画_DCHK_指摘リスト.開催日_ID");
            sql.AppendLine("    AND 試験計画_DCHK_指摘リスト.FLAG_最新 = 1");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // 開催日ID
            if (val != null && val.ID > 0)
            {
                sql.AppendLine("    AND 試験計画_DCHK_開催日.ID = :ID");

                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Int32,
                    Object = val.ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 開催日（開始）
            if (val != null && val.OPEN_START_DATE != null)
            {
                sql.AppendLine("    AND 試験計画_DCHK_開催日.開催日 >= :OPEN_START_DATE");

                prms.Add(new BindModel
                {
                    Name = ":OPEN_START_DATE",
                    Type = OracleDbType.Date,
                    Object = getDateTime(val.OPEN_START_DATE),
                    Direct = ParameterDirection.Input
                });
            }

            // 開催日（終了）
            if (val != null && val.OPEN_END_DATE != null)
            {
                sql.AppendLine("    AND 試験計画_DCHK_開催日.開催日 <= :OPEN_END_DATE");

                prms.Add(new BindModel
                {
                    Name = ":OPEN_END_DATE",
                    Type = OracleDbType.Date,
                    Object = getDateTime(val.OPEN_END_DATE),
                    Direct = ParameterDirection.Input
                });
            }

            // 設計チェック名称
            if (val != null && !string.IsNullOrWhiteSpace(val.名称))
            {
                sql.AppendLine("    AND UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(試験計画_DCHK_開催日.名称)), 'kana_fwkatakana') like '%' || UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(:名称)), 'kana_fwkatakana') || '%'");

                prms.Add(new BindModel
                {
                    Name = ":名称",
                    Type = OracleDbType.Varchar2,
                    Object = val.名称,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("GROUP BY");
            sql.AppendLine("     試験計画_DCHK_開催日.ID");
            sql.AppendLine("    ,試験計画_DCHK_開催日.開催日");
            sql.AppendLine("    ,試験計画_DCHK_開催日.回");
            sql.AppendLine("    ,試験計画_DCHK_開催日.名称");

            var havings = new List<string>();

            // オープンフラグ
            if (val != null && val.OPEN_FLG == true)
            {
                havings.Add("     COUNT(CASE WHEN 試験計画_DCHK_指摘リスト.ID IS NOT NULL AND NVL(試験計画_DCHK_指摘リスト.FLAG_CLOSE, 0) <> 1 THEN 1 END) > 0");
            }

            // クローズフラグ
            if (val != null && val.CLOSE_FLG == true)
            {
                havings.Add("     COUNT(CASE WHEN 試験計画_DCHK_指摘リスト.ID IS NOT NULL AND NVL(試験計画_DCHK_指摘リスト.FLAG_CLOSE, 0) = 1 THEN 1 END) > 0");
            }

            if (havings.Any())
            {
                sql.AppendLine("HAVING");
                sql.AppendLine(string.Join(" OR ", havings));
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     試験計画_DCHK_開催日.開催日 DESC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 設計チェックの作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(DesignCheckPostInModel val, ref int? newid)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験計画_DCHK_開催日 (");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,開催日");
            sql.AppendLine("    ,回");
            sql.AppendLine("    ,名称");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_DCHK_開催日)");
            sql.AppendLine("    ,:開催日");
            sql.AppendLine("    ,:回");
            sql.AppendLine("    ,:名称");
            sql.AppendLine(") RETURNING");
            sql.AppendLine("    ID INTO :newid");

            // 開催日：必須
            prms.Add(new BindModel
            {
                Name = ":開催日",
                Type = OracleDbType.Date,
                Object = val.開催日,
                Direct = ParameterDirection.Input
            });

            // 開催回：必須
            prms.Add(new BindModel
            {
                Name = ":回",
                Type = OracleDbType.Int32,
                Object = val.回,
                Direct = ParameterDirection.Input
            });

            // 設計チェック名称：必須
            prms.Add(new BindModel
            {
                Name = ":名称",
                Type = OracleDbType.Varchar2,
                Object = val.名称,
                Direct = ParameterDirection.Input
            });

            // 採番ID：戻り値設定
            db.Returns = new List<BindModel>();
            db.Returns.Add(new BindModel
            {
                Name = ":newid",
                Type = OracleDbType.Int32,
                Direct = ParameterDirection.Input
            });

            var ret = db.InsertData(sql.ToString(), prms);

            newid = Convert.ToInt32(db.Returns.Where(r => r.Name == ":newid").FirstOrDefault().Object.ToString());

            return ret;
        }

        /// <summary>
        /// 設計チェックの更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(DesignCheckPutInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    試験計画_DCHK_開催日");
            sql.AppendLine("SET");
            sql.AppendLine("     開催日 = :開催日");
            sql.AppendLine("    ,回 = :回");
            sql.AppendLine("    ,名称 = :名称");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            // 開催日ID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int32,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });

            // 開催日：必須
            prms.Add(new BindModel
            {
                Name = ":開催日",
                Type = OracleDbType.Date,
                Object = val.開催日,
                Direct = ParameterDirection.Input
            });

            // 開催回：必須
            prms.Add(new BindModel
            {
                Name = ":回",
                Type = OracleDbType.Int32,
                Object = val.回,
                Direct = ParameterDirection.Input
            });

            // 設計チェック名称：必須
            prms.Add(new BindModel
            {
                Name = ":名称",
                Type = OracleDbType.Varchar2,
                Object = val.名称,
                Direct = ParameterDirection.Input
            });

            return db.UpdateData(sql.ToString(), prms);
        }

        /// <summary>
        /// 設計チェックの削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(DesignCheckDeleteInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_開催日");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            // 開催日ID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int32,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_指摘リスト");
            sql.AppendLine("WHERE");
            sql.AppendLine("    開催日_ID = :ID");

            // 開催日ID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int32,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_状況");
            sql.AppendLine("WHERE");
            sql.AppendLine("    対象車両_ID IN (SELECT ID FROM 試験計画_DCHK_対象車両 WHERE 開催日_ID = :ID)");

            // 開催日ID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int32,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_対象車両");
            sql.AppendLine("WHERE");
            sql.AppendLine("    開催日_ID = :ID");

            // 開催日ID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int32,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_参加者");
            sql.AppendLine("WHERE");
            sql.AppendLine("    開催日_ID = :ID");

            // 開催日ID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int32,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            //Append Start 2021/06/24 張晋華 開発計画表設計チェック機能改修

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    EXCEL_INPUT");
            sql.AppendLine("WHERE");
            sql.AppendLine("    開催日_ID = :ID");

            // 開催日ID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int32,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            //Append End 2021/06/24 張晋華 開発計画表設計チェック機能改修

            return true;
        }
    }
}