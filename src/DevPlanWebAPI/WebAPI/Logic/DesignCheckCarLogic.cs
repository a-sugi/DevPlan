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
    /// 設計チェック対象車ロジッククラス
    /// </summary>
    /// <remarks>設計チェック対象車の操作</remarks>
    public class DesignCheckCarLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 設計チェック対象車の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(DesignCheckCarGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("     VD.ID");
            sql.AppendLine("    ,VD.開催日_ID");
            sql.AppendLine("    ,VD.試験車_ID");
            sql.AppendLine("    ,TD.管理票NO");
            sql.AppendLine("    ,TD.試験車名");
            sql.AppendLine("    ,TD.グレード");
            sql.AppendLine("    ,TD.試験目的");
            sql.AppendLine("    ,KK.車系");
            sql.AppendLine("    ,KK.駐車場番号");
            sql.AppendLine("    ,KK.車型");
            sql.AppendLine("    ,CASE WHEN INSTR(TD.試験車名, ' ', 1, 1) = 0 THEN TD.試験車名 WHEN TD.試験車名 IS NOT NULL THEN SUBSTR(TD.試験車名, 1, INSTR(TD.試験車名, ' ', 1, 1) - 1) ELSE KR.開発符号 END 開発符号");
            sql.AppendLine("    ,CASE WHEN INSTR(TD.試験車名, ' ', 1, 1) = 0 THEN NULL WHEN INSTR(TD.試験車名, ' ', 1, 2) = 0 THEN SUBSTR(TD.試験車名, INSTR(TD.試験車名, ' ', 1, 1) + 1, LENGTHB(TD.試験車名) - INSTR(TD.試験車名, ' ', 1, 1)) WHEN TD.試験車名 IS NOT NULL THEN SUBSTR(TD.試験車名, INSTR(TD.試験車名, ' ', 1, 1) + 1, INSTR(TD.試験車名, ' ', 1, 2) - INSTR(TD.試験車名, ' ', 1, 1) - 1) ELSE KR.試作時期 END 試作時期");
            sql.AppendLine("    ,CASE WHEN INSTR(TD.試験車名, ' ', 1, 1) = 0 THEN NULL WHEN INSTR(TD.試験車名, ' ', 1, 2) = 0 THEN NULL WHEN TD.試験車名 IS NOT NULL THEN SUBSTR(TD.試験車名, INSTR(TD.試験車名, ' ', 1, 2) + 1) ELSE KR.号車 END 号車");
            sql.AppendLine("    ,KR.仕向地");
            sql.AppendLine("    ,KR.排気量");
            sql.AppendLine("    ,KR.E_G型式");
            sql.AppendLine("    ,KR.駆動方式");
            sql.AppendLine("    ,KR.トランスミッション");
            sql.AppendLine("    ,KR.車体色");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_対象車両 VD");
            sql.AppendLine("    LEFT JOIN 試験計画_DCHK_試験車 TD");
            sql.AppendLine("    ON VD.試験車_ID = TD.ID");
            sql.AppendLine("    LEFT JOIN VIEW_試験車基本情報 KK");
            sql.AppendLine("    ON TD.管理票NO = KK.管理票NO");
            sql.AppendLine("    LEFT JOIN 試験車履歴情報 KR");
            sql.AppendLine("    ON KK.データID = KR.データID");
            sql.AppendLine("    LEFT JOIN (SELECT データID, MAX(履歴NO) 履歴NO FROM 試験車履歴情報 GROUP BY データID) MX");
            sql.AppendLine("    ON KR.データID = MX.データID");
            sql.AppendLine("    AND KR.履歴NO = MX.履歴NO");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // 開催日ID
            if (val != null && val.開催日_ID > 0)
            {
                sql.AppendLine("    AND VD.開催日_ID = :開催日_ID");

                prms.Add(new BindModel
                {
                    Name = ":開催日_ID",
                    Type = OracleDbType.Int32,
                    Object = val.開催日_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    NLSSORT(TD.試験車名, 'NLS_SORT=JAPANESE_M') NULLS FIRST");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 設計チェック対象車の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(List<DesignCheckCarPostInModel> list)
        {
            var testCarLogic = new DesignCheckTestCarLogic();
            testCarLogic.SetDBAccess(base.db);

            var cars = this.GetData(new DesignCheckCarGetInModel { 開催日_ID = list.FirstOrDefault()?.開催日_ID });

            var sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験計画_DCHK_対象車両 (");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,開催日_ID");
            sql.AppendLine("    ,試験車_ID");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_DCHK_対象車両)");
            sql.AppendLine("    ,:開催日_ID");
            sql.AppendLine("    ,:試験車_ID");
            sql.AppendLine(")");

            foreach (var val in list)
            {
                // 設計チェック試験車登録済み
                if (val.試験車_ID > 0)
                {
                    var inModel = new DesignCheckTestCarPutInModel()
                    {
                        ID = val.試験車_ID,
                        管理票NO = val.管理票NO,
                        開発符号 = val.開発符号,
                        試作時期 = val.試作時期,
                        号車 = val.号車
                    };

                    var outModel = new DesignCheckTestCarGetOutModel();

                    // 試験車（設計チェック）の更新
                    if (!testCarLogic.PutData(inModel))
                    {
                        return false;
                    }

                    // 重複行は追加しない
                    if (cars?.Select(string.Format("開催日_ID = {0} AND 試験車_ID = {1}", val?.開催日_ID, val?.試験車_ID))?.Any() == true)
                    {
                        continue;
                    }
                }
                else
                {
                    var inModel = new DesignCheckTestCarPostInModel()
                    {
                        管理票NO = val.管理票NO,
                        開発符号 = val.開発符号,
                        試作時期 = val.試作時期,
                        号車 = val.号車
                    };

                    var outModel = new DesignCheckTestCarGetOutModel();

                    // 試験車（設計チェック）の作成
                    if (!testCarLogic.PostData(inModel, ref outModel))
                    {
                        return false;
                    }

                    // 採番IDのセット
                    val.試験車_ID = outModel.ID;
                }

                var prms = new List<BindModel>();

                // 開催日ID：必須
                prms.Add(new BindModel
                {
                    Name = ":開催日_ID",
                    Type = OracleDbType.Int32,
                    Object = val.開催日_ID,
                    Direct = ParameterDirection.Input
                });

                // 試験車ID：必須
                prms.Add(new BindModel
                {
                    Name = ":試験車_ID",
                    Type = OracleDbType.Int32,
                    Object = val.試験車_ID,
                    Direct = ParameterDirection.Input
                });

                if (!db.InsertData(sql.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 設計チェック対象車の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(List<DesignCheckCarDeleteInModel> list)
        {
            var sql_prg = new StringBuilder();

            sql_prg.AppendLine("DELETE");
            sql_prg.AppendLine("FROM");
            sql_prg.AppendLine("    試験計画_DCHK_状況");
            sql_prg.AppendLine("WHERE");
            sql_prg.AppendLine("    対象車両_ID IN ");
            sql_prg.AppendLine("    (");
            sql_prg.AppendLine("        SELECT");
            sql_prg.AppendLine("            ID");
            sql_prg.AppendLine("        FROM");
            sql_prg.AppendLine("            試験計画_DCHK_対象車両");
            sql_prg.AppendLine("        WHERE");
            sql_prg.AppendLine("            開催日_ID = :開催日_ID");
            sql_prg.AppendLine("            AND 試験車_ID = :試験車_ID");
            sql_prg.AppendLine("    )");

            var sql_car = new StringBuilder();

            sql_car.AppendLine("DELETE");
            sql_car.AppendLine("FROM");
            sql_car.AppendLine("    試験計画_DCHK_対象車両");
            sql_car.AppendLine("WHERE");
            sql_car.AppendLine("    開催日_ID = :開催日_ID");
            sql_car.AppendLine("    AND 試験車_ID = :試験車_ID");

            foreach (var val in list)
            {
                var prms = new List<BindModel>();

                // 開催日ID：必須
                prms.Add(new BindModel
                {
                    Name = ":開催日_ID",
                    Type = OracleDbType.Int32,
                    Object = val.開催日_ID,
                    Direct = ParameterDirection.Input
                });

                // 試験車ID：必須
                prms.Add(new BindModel
                {
                    Name = ":試験車_ID",
                    Type = OracleDbType.Int32,
                    Object = val.試験車_ID,
                    Direct = ParameterDirection.Input
                });

                // 状況削除
                if (!db.DeleteData(sql_prg.ToString(), prms))
                {
                    return false;
                }

                // 対象車両削除
                if (!db.DeleteData(sql_car.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }
    }
}