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
    /// 試験車（試験車管理）ロジッククラス
    /// </summary>
    /// <remarks>試験車（試験車管理）の操作</remarks>
    public class DesignCheckSystemTestCarLogic : BaseLogic
    {
        /// <summary>
        /// 試験車（試験車管理）の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public List<TestCarCommonModel> GetData(DesignCheckTestCarGetInModel cond)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     KR.開発符号");
            sql.AppendLine("    ,KR.試作時期");
            sql.AppendLine("    ,KR.号車");
            sql.AppendLine("    ,KR.仕向地");
            sql.AppendLine("    ,KR.排気量");
            sql.AppendLine("    ,KR.E_G型式");
            sql.AppendLine("    ,KR.駆動方式");
            sql.AppendLine("    ,KR.トランスミッション");
            sql.AppendLine("    ,KR.車体色");
            sql.AppendLine("    ,GC.CAR_GROUP 車系");
            sql.AppendLine("    ,KK.駐車場番号");
            sql.AppendLine("    ,KK.車型");
            sql.AppendLine("    ,KK.管理票NO");
            sql.AppendLine("    ,KR.データID");
            sql.AppendLine("    ,KR.履歴NO");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE LOWER(DP.ESTABLISHMENT)");
            sql.AppendLine("            WHEN 'g' THEN '群馬'");
            sql.AppendLine("            WHEN 't' THEN '東京'");
            sql.AppendLine("        END");
            sql.AppendLine("     ) AS ESTABLISHMENT");
            sql.AppendLine("FROM");
            sql.AppendLine("    VIEW_試験車基本情報 KK");
            sql.AppendLine("    LEFT JOIN 試験車履歴情報 KR");
            sql.AppendLine("    ON KK.データID = KR.データID");
            sql.AppendLine("    INNER JOIN (SELECT データID, MAX(履歴NO) 履歴NO FROM 試験車履歴情報 GROUP BY データID) MX");
            sql.AppendLine("    ON KR.データID = MX.データID");
            sql.AppendLine("    AND KR.履歴NO = MX.履歴NO");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA SG");
            sql.AppendLine("    ON KR.管理責任部署 = SG.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN SECTION_DATA SC");
            sql.AppendLine("    ON SG.SECTION_ID = SC.SECTION_ID");
            sql.AppendLine("    LEFT JOIN DEPARTMENT_DATA DP");
            sql.AppendLine("    ON SC.DEPARTMENT_ID = DP.DEPARTMENT_ID");
            sql.AppendLine("    LEFT JOIN GENERAL_CODE GC");
            sql.AppendLine("    ON KR.開発符号 = GC.GENERAL_CODE");

            sql.AppendLine("WHERE 0 = 0");

            // 廃却車両は取得しない
            sql.AppendLine("    AND (KK.廃却決済承認年月 IS NULL)");
            sql.AppendLine("    AND (KK.車両搬出日 IS NULL)");

            // 開発符号・試作時期・号車がすべてNULLは取得しない
            sql.AppendLine("    AND (開発符号 IS NOT NULL OR 試作時期 IS NOT NULL OR 号車 IS NOT NULL)");

            // 設計チェック登録済みは取得しない
            sql.AppendLine("    AND NOT EXISTS");
            sql.AppendLine("        (");
            sql.AppendLine("            SELECT");
            sql.AppendLine("                *");
            sql.AppendLine("            FROM");
            sql.AppendLine("                試験計画_DCHK_試験車 KD");
            sql.AppendLine("            WHERE 0 = 0");
            sql.AppendLine("                AND KD.管理票NO IS NOT NULL");
            sql.AppendLine("                AND KD.管理票NO = KK.管理票NO");
            sql.AppendLine("                AND CASE WHEN INSTR(KD.試験車名, ' ', 1, 1) = 0 THEN KD.試験車名 WHEN KD.試験車名 IS NOT NULL THEN SUBSTR(KD.試験車名, 1, INSTR(KD.試験車名, ' ', 1, 1) - 1) ELSE NULL END = KR.開発符号");
            sql.AppendLine("                AND CASE WHEN INSTR(KD.試験車名, ' ', 1, 1) = 0 THEN NULL WHEN INSTR(KD.試験車名, ' ', 1, 2) = 0 THEN SUBSTR(KD.試験車名, INSTR(KD.試験車名, ' ', 1, 1) + 1, LENGTHB(KD.試験車名) - INSTR(KD.試験車名, ' ', 1, 1)) WHEN KD.試験車名 IS NOT NULL THEN SUBSTR(KD.試験車名, INSTR(KD.試験車名, ' ', 1, 1) + 1, INSTR(KD.試験車名, ' ', 1, 2) - INSTR(KD.試験車名, ' ', 1, 1) - 1) ELSE NULL END = KR.試作時期");
            sql.AppendLine("                AND CASE WHEN INSTR(KD.試験車名, ' ', 1, 1) = 0 THEN NULL WHEN INSTR(KD.試験車名, ' ', 1, 2) = 0 THEN NULL WHEN KD.試験車名 IS NOT NULL THEN SUBSTR(KD.試験車名, INSTR(KD.試験車名, ' ', 1, 2) + 1) ELSE NULL END = KR.号車");





            sql.AppendLine("        )");

            // 管理票NO
            if (cond != null && !string.IsNullOrWhiteSpace(cond.管理票NO))
            {
                sql.AppendLine("    AND KK.管理票NO LIKE :管理票NO");

                prms.Add(new BindModel
                {
                    Name = ":管理票NO",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + cond.管理票NO + "%",
                    Direct = ParameterDirection.Input
                });
            }

            // 開発符号
            if (cond != null && !string.IsNullOrWhiteSpace(cond.開発符号))
            {
                sql.AppendLine("    AND KR.開発符号 LIKE :開発符号");

                prms.Add(new BindModel
                {
                    Name = ":開発符号",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + cond.開発符号 + "%",
                    Direct = ParameterDirection.Input
                });
            }

            // 試作時期
            if (cond != null && !string.IsNullOrWhiteSpace(cond.試作時期))
            {
                sql.AppendLine("    AND KR.試作時期 LIKE :試作時期");

                prms.Add(new BindModel
                {
                    Name = ":試作時期",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + cond.試作時期 + "%",
                    Direct = ParameterDirection.Input
                });
            }

            // 号車
            if (cond != null && !string.IsNullOrWhiteSpace(cond.号車))
            {
                sql.AppendLine("    AND KR.号車 LIKE :号車");

                prms.Add(new BindModel
                {
                    Name = ":号車",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + cond.号車 + "%",
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    NLSSORT(KR.開発符号, 'NLS_SORT=JAPANESE_M') NULLS FIRST");
            sql.AppendLine("   ,NLSSORT(KR.試作時期, 'NLS_SORT=JAPANESE_M') NULLS FIRST");
            sql.AppendLine("   ,NLSSORT(KR.号車, 'NLS_SORT=JAPANESE_M') NULLS FIRST");
            sql.AppendLine("   ,NLSSORT(KR.履歴NO, 'NLS_SORT=JAPANESE_M') NULLS FIRST");

            return db.ReadModelList<TestCarCommonModel>(sql.ToString(), prms);
        }
    }
}