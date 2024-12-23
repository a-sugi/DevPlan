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
    /// 試験車（設計チェック）ロジッククラス
    /// </summary>
    /// <remarks>試験車（設計チェック）の操作</remarks>
    public class DesignCheckTestCarLogic : BaseLogic
    {
        /// <summary>
        /// 試験車（設計チェック）の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(DesignCheckTestCarGetInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     A.ID");
            sql.AppendLine("    ,A.管理票NO");
            sql.AppendLine("    ,A.試験車名");
            sql.AppendLine("    ,A.グレード");
            sql.AppendLine("    ,A.試験目的");
            sql.AppendLine("    ,G.CAR_GROUP 車系");
            sql.AppendLine("    ,B.駐車場番号");
            sql.AppendLine("    ,B.車型");
            sql.AppendLine("    ,CASE WHEN INSTR(A.試験車名, ' ', 1, 1) = 0 THEN A.試験車名 WHEN A.試験車名 IS NOT NULL THEN SUBSTR(A.試験車名, 1, INSTR(A.試験車名, ' ', 1, 1) - 1) ELSE C.開発符号 END 開発符号");
            sql.AppendLine("    ,CASE WHEN INSTR(A.試験車名, ' ', 1, 1) = 0 THEN NULL WHEN INSTR(A.試験車名, ' ', 1, 2) = 0 THEN SUBSTR(A.試験車名, INSTR(A.試験車名, ' ', 1, 1) + 1, LENGTHB(A.試験車名) - INSTR(A.試験車名, ' ', 1, 1)) WHEN A.試験車名 IS NOT NULL THEN SUBSTR(A.試験車名, INSTR(A.試験車名, ' ', 1, 1) + 1, INSTR(A.試験車名, ' ', 1, 2) - INSTR(A.試験車名, ' ', 1, 1) - 1) ELSE C.試作時期 END 試作時期");
            sql.AppendLine("    ,CASE WHEN INSTR(A.試験車名, ' ', 1, 1) = 0 THEN NULL WHEN INSTR(A.試験車名, ' ', 1, 2) = 0 THEN NULL WHEN A.試験車名 IS NOT NULL THEN SUBSTR(A.試験車名, INSTR(A.試験車名, ' ', 1, 2) + 1) ELSE C.号車 END 号車");
            sql.AppendLine("    ,C.仕向地");
            sql.AppendLine("    ,C.排気量");
            sql.AppendLine("    ,C.E_G型式");
            sql.AppendLine("    ,C.駆動方式");
            sql.AppendLine("    ,C.トランスミッション");
            sql.AppendLine("    ,C.車体色");
            sql.AppendLine("    ,C.ESTABLISHMENT");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_試験車 A");
            sql.AppendLine("    LEFT JOIN VIEW_試験車基本情報 B");
            sql.AppendLine("    ON A.管理票NO = B.管理票NO");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         A.データID");
            sql.AppendLine("                        ,A.開発符号");
            sql.AppendLine("                        ,A.試作時期");
            sql.AppendLine("                        ,A.号車");
            sql.AppendLine("                        ,A.仕向地");
            sql.AppendLine("                        ,A.排気量");
            sql.AppendLine("                        ,A.E_G型式");
            sql.AppendLine("                        ,A.駆動方式");
            sql.AppendLine("                        ,A.トランスミッション");
            sql.AppendLine("                        ,A.車体色");
            sql.AppendLine("                        ,(");
            sql.AppendLine("                            CASE LOWER(E.ESTABLISHMENT)");
            sql.AppendLine("                                WHEN 'g' THEN '群馬'");
            sql.AppendLine("                                WHEN 't' THEN '東京'");
            sql.AppendLine("                            END");
            sql.AppendLine("                        ) AS ESTABLISHMENT");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        試験車履歴情報 A");
            sql.AppendLine("                        INNER JOIN");
            sql.AppendLine("                                    (");
            sql.AppendLine("                                        SELECT");
            sql.AppendLine("                                             A.データID");
            sql.AppendLine("                                            ,MAX(A.履歴NO) AS 履歴NO");
            sql.AppendLine("                                        FROM");
            sql.AppendLine("                                            試験車履歴情報 A");
            sql.AppendLine("                                        GROUP BY");
            sql.AppendLine("                                            A.データID");
            sql.AppendLine("                                    ) B");
            sql.AppendLine("                        ON A.データID = B.データID");
            sql.AppendLine("                        AND A.履歴NO = B.履歴NO");
            sql.AppendLine("                        LEFT JOIN SECTION_GROUP_DATA C");
            sql.AppendLine("                        ON A.管理責任部署 = C.SECTION_GROUP_ID");
            sql.AppendLine("                        LEFT JOIN SECTION_DATA D");
            sql.AppendLine("                        ON C.SECTION_ID = D.SECTION_ID");
            sql.AppendLine("                        LEFT JOIN DEPARTMENT_DATA E");
            sql.AppendLine("                        ON D.DEPARTMENT_ID = E.DEPARTMENT_ID");
            sql.AppendLine("                ) C");
            sql.AppendLine("    ON B.データID = C.データID");
            sql.AppendLine("    LEFT JOIN GENERAL_CODE G");
            sql.AppendLine("    ON CASE WHEN INSTR(A.試験車名, ' ', 1, 1) = 0 THEN A.試験車名 WHEN A.試験車名 IS NOT NULL THEN SUBSTR(A.試験車名, 1, INSTR(A.試験車名, ' ', 1, 1) - 1) ELSE C.開発符号 END = G.GENERAL_CODE");
            sql.AppendLine("WHERE 0 = 0");

            // 登録済フラグ
            if (val != null && val.ENTRY_FLG == true)
            {
                sql.AppendLine("    AND A.管理票NO IS NOT NULL");
            }
            else if (val != null && val.ENTRY_FLG == false)
            {
                sql.AppendLine("    AND A.管理票NO IS NULL");
            }

            // 開催日ID
            if (val != null && val.ID > 0)
            {
                sql.AppendLine("    AND A.ID = :ID");

                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Int32,
                    Object = val.ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 管理票NO
            if (val != null && !string.IsNullOrWhiteSpace(val.管理票NO))
            {
                sql.AppendLine("    AND A.管理票NO LIKE :管理票NO");

                prms.Add(new BindModel
                {
                    Name = ":管理票NO",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.管理票NO + "%",
                    Direct = ParameterDirection.Input
                });
            }

            // 開発符号
            if (val != null && !string.IsNullOrWhiteSpace(val.開発符号))
            {
                sql.AppendLine("    AND C.開発符号 LIKE :開発符号");

                prms.Add(new BindModel
                {
                    Name = ":開発符号",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.開発符号 + "%",
                    Direct = ParameterDirection.Input
                });
            }

            // 試作時期
            if (val != null && !string.IsNullOrWhiteSpace(val.試作時期))
            {
                sql.AppendLine("    AND C.試作時期 LIKE :試作時期");

                prms.Add(new BindModel
                {
                    Name = ":試作時期",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.試作時期 + "%",
                    Direct = ParameterDirection.Input
                });
            }

            // 号車
            if (val != null && !string.IsNullOrWhiteSpace(val.号車))
            {
                sql.AppendLine("    AND C.号車 LIKE :号車");

                prms.Add(new BindModel
                {
                    Name = ":号車",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.号車 + "%",
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    NLSSORT(A.試験車名, 'NLS_SORT=JAPANESE_M') NULLS FIRST");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 試験車（設計チェック）の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(DesignCheckTestCarPostInModel val, ref DesignCheckTestCarGetOutModel returns)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験計画_DCHK_試験車 (");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,管理票NO");
            sql.AppendLine("    ,試験車名");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_DCHK_試験車)");
            sql.AppendLine("    ,:管理票NO");
            sql.AppendLine("    ,:試験車名");
            sql.AppendLine(") RETURNING");
            sql.AppendLine("    ID INTO :NEWID");

            returns.試験車名 = LeftB(string.Format("{0} {1} {2}", val?.開発符号?.Replace(" ",""), val?.試作時期?.Replace(" ", ""), val?.号車?.Replace(" ", "")), Encoding.GetEncoding("Shift_JIS"), 20);

            // 管理票NO
            prms.Add(new BindModel
            {
                Name = ":管理票NO",
                Type = OracleDbType.Varchar2,
                Object = val.管理票NO,
                Direct = ParameterDirection.Input
            });
            
            // 試験車名
            prms.Add(new BindModel
            {
                Name = ":試験車名",
                Type = OracleDbType.Varchar2,
                Object = returns.試験車名,
                Direct = ParameterDirection.Input
            });

            // 採番ID：戻り値設定
            db.Returns = new List<BindModel>();
            db.Returns.Add(new BindModel
            {
                Name = ":NEWID",
                Type = OracleDbType.Int32,
                Direct = ParameterDirection.Input
            });

            var ret = db.InsertData(sql.ToString(), prms);

            returns.ID = Convert.ToInt32(db.Returns.First(r => r.Name == ":NEWID").Object.ToString());
            returns.開発符号 = val.開発符号;
            returns.試作時期 = val.試作時期;
            returns.号車 = val.号車;

            return ret;
        }

        /// <summary>
        /// 試験車（設計チェック）の更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(DesignCheckTestCarPutInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    試験計画_DCHK_試験車");
            sql.AppendLine("SET");
            sql.AppendLine("     管理票NO = :管理票NO");
            sql.AppendLine("    ,試験車名 = :試験車名");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");


            var name = LeftB(string.Format("{0} {1} {2}", val?.開発符号?.Replace(" ", ""), val?.試作時期?.Replace(" ", ""), val?.号車?.Replace(" ", "")), Encoding.GetEncoding("Shift_JIS"), 20);

            // 管理票NO
            prms.Add(new BindModel
            {
                Name = ":管理票NO",
                Type = OracleDbType.Varchar2,
                Object = val.管理票NO,
                Direct = ParameterDirection.Input
            });

            // 試験車名
            prms.Add(new BindModel
            {
                Name = ":試験車名",
                Type = OracleDbType.Varchar2,
                Object = name,
                Direct = ParameterDirection.Input
            });

            // 試験車ID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int32,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });

            return db.UpdateData(sql.ToString(), prms);
        }

        /// <summary>
        /// 試験車（設計チェック）の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(DesignCheckTestCarDeleteInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_試験車");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            // 試験車ID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int32,
                Object = val.ID,
                Direct = ParameterDirection.Input
            });
            return db.DeleteData(sql.ToString(), prms);

        }

        private string LeftB(string s, Encoding encoding, int maxByteCount)
        { 
            var bytes = encoding.GetBytes(s); 

            if (bytes.Length <= maxByteCount) return s; 

            var result = s.Substring(0, encoding.GetString(bytes, 0, maxByteCount).Length); 

            while (encoding.GetByteCount(result) > maxByteCount) 
            { 
                result = result.Substring(0, result.Length - 1); 
            }

            return result; 
        }
    }
}