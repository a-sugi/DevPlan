using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 開発符号検索ロジッククラス
    /// </summary>
    /// <remarks>開発符号データの操作</remarks>
    public class GeneralCodeLogic : BaseLogic
    {
        /// <summary>
        /// 開発符号の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(GeneralCodeSearchInModel val)
        {
            var isEnableCarGroup = !string.IsNullOrWhiteSpace(val.CAR_GROUP);
            var isEnableGeneralCode = !string.IsNullOrWhiteSpace(val.GENERAL_CODE);
            var isEnableUnderDevelopment = !string.IsNullOrWhiteSpace(val.UNDER_DEVELOPMENT);
            var isEnablePersonelID = !string.IsNullOrWhiteSpace(val.PERSONEL_ID);

            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("        GNR.CAR_GROUP");
            sql.AppendLine("       ,GNR.GENERAL_CODE");
            sql.AppendLine("       ,GNR.SORT_NUMBER");
            sql.AppendLine("       ,GNR.UNDER_DEVELOPMENT");
            sql.AppendLine("       ,GNR.BASE_GENERAL_CODE");
            sql.AppendLine("       ,CASE WHEN ATH.GENERAL_CODE IS NOT NULL THEN 1 ELSE 0 END PERMIT_FLG");
            sql.AppendLine("    FROM");
            sql.AppendLine("        GENERAL_CODE GNR");
            sql.AppendLine("        LEFT JOIN 試験計画_他部署閲覧許可 ATH");
            sql.AppendLine("        ON GNR.GENERAL_CODE = ATH.GENERAL_CODE");
            sql.AppendLine("        AND ATH.PERSONEL_ID = :PERSONEL_ID");
            sql.AppendLine("        AND ATH.PERMISSION_PERIOD_START <= TRUNC(SYSDATE)");
            sql.AppendLine("        AND ATH.PERMISSION_PERIOD_END >= TRUNC(SYSDATE)");

            // 試験車日程
            if (val.FUNCTION_CLASS == Const.TestCar)
            {
                sql.AppendLine("        INNER JOIN TESTCAR_SCHEDULE_ITEM SCH");
                sql.AppendLine("        ON GNR.GENERAL_CODE = SCH.GENERAL_CODE");
            }

            // WHERE句生成
            sql.Append(new WhereString(val).Get());

            sql.AppendLine("    ORDER BY");
            sql.AppendLine("        GNR.UNDER_DEVELOPMENT DESC,");
            sql.AppendLine("        GNR.SORT_NUMBER,");
            sql.AppendLine("        GNR.CAR_GROUP,");            
            sql.AppendLine("        GNR.GENERAL_CODE");

            if (isEnableCarGroup)
            {
                prms.Add(new BindModel { Name = ":CAR_GROUP", Type = OracleDbType.Varchar2, Object = val.CAR_GROUP, Direct = ParameterDirection.Input });
            }

            if (isEnableGeneralCode)
            {
                prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = val.GENERAL_CODE, Direct = ParameterDirection.Input });
            }

            if (isEnableUnderDevelopment)
            {
                prms.Add(new BindModel { Name = ":UNDER_DEVELOPMENT", Type = OracleDbType.Int16, Object = val.UNDER_DEVELOPMENT, Direct = ParameterDirection.Input });
            }

            if (isEnablePersonelID)
            {
                prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
            }

            return db.ReadDataTable(sql.ToString(), prms);
        }

        #region ファクトリ
        /// <summary>
        /// ファクトリクラス
        /// </summary>
        private class Factory
        {
            private GeneralCodeSearchInModel _val;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="val"></param>
            public Factory(GeneralCodeSearchInModel val)
            {
                this._val = val;
            }

            /// <summary>
            /// 検索区分クラスを生成します。
            /// </summary>
            /// <returns></returns>
            public ClassPart GetClassData()
            {
                switch (this._val.CLASS_DATA)
                {
                    case 1:
                        return new ClassPerfect(this._val);

                    default:
                        return new ClassPart(this._val);
                }
            }

            /// <summary>
            /// 大文字､小文字､全角､半角を区別検索クラスを生成します。
            /// </summary>
            /// <returns></returns>
            public DiffOn GetDiffData()
            {
                switch (this._val.DIFF_DATA)
                {
                    case 1:
                        return new DiffOff();

                    default:
                        return new DiffOn();
                }
            }
        }
        #endregion

        #region WHERE句を生成するクラス
        /// <summary>
        /// WHERE句を生成するクラス
        /// </summary>
        private class WhereString
        {
            protected GeneralCodeSearchInModel _val;

            private ClassPart classData;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="val"></param>
            public WhereString(GeneralCodeSearchInModel val)
            {
                this._val = val;
                this.classData = new Factory(val).GetClassData();
            }

            /// <summary>
            /// WHERE句を生成します。
            /// </summary>
            /// <returns></returns>
            public string Get()
            {
                var sql = new StringBuilder();

                var isEnableCarGroup = !string.IsNullOrWhiteSpace(this._val.CAR_GROUP);
                var isEnableGeneralCode = !string.IsNullOrWhiteSpace(this._val.GENERAL_CODE);
                var isEnableUnderDevelopment = !string.IsNullOrWhiteSpace(this._val.UNDER_DEVELOPMENT);

                sql.AppendLine("    WHERE");
                sql.AppendLine("            0 = 0");

                // 車系
                if (isEnableCarGroup)
                {
                    sql.AppendLine(this.classData.GetCarGroup());
                }

                // 開発符号
                if (isEnableGeneralCode)
                {
                    sql.AppendLine(this.classData.GetGeneralCode());
                }

                // 開発フラグ(0:開発外、1:開発中)
                if (isEnableUnderDevelopment)
                {
                    sql.AppendLine("        AND GNR.UNDER_DEVELOPMENT = :UNDER_DEVELOPMENT");
                }

                // 先開フラグ(0:先開以外、1:先開含む)
                if (this._val.INCLUDE_PRECEDING_DEVELOPMENT == "0")
                {
                    sql.AppendLine("        AND GNR.CAR_GROUP <> '先開' ");
                }

                return sql.ToString();
            }
        }
        #endregion

        #region 検索区分のカプセル化クラス
        /// <summary>
        /// 検索区分のカプセル化クラス（既定は部分一致検索）
        /// </summary>
        private class ClassPart
        {
            const string BEFORE_WORD = "LIKE '%' ||";
            const string AFTER_WORD = "|| '%'";

            protected DiffOn _diff;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="val"></param>
            public ClassPart(GeneralCodeSearchInModel val)
            {
                this._diff = new Factory(val).GetDiffData();
            }

            /// <summary>
            /// 条件文「車系」を取得します。
            /// </summary>
            /// <returns></returns>
            public virtual string GetCarGroup()
            {
                return string.Format(this._diff.GetCarGroup(), BEFORE_WORD, AFTER_WORD);
            }

            /// <summary>
            /// 条件文「開発符号」を取得します。
            /// </summary>
            /// <returns></returns>
            public virtual string GetGeneralCode()
            {
                return string.Format(this._diff.GetGeneralCode(), BEFORE_WORD, AFTER_WORD);
            }
        }

        /// <summary>
        /// 完全一致検索クラス
        /// </summary>
        private class ClassPerfect : ClassPart
        {
            const string BEFORE_WORD = "=";
            const string AFTER_WORD = "";

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="val"></param>
            public ClassPerfect(GeneralCodeSearchInModel val) : base(val)
            {
            }

            /// <summary>
            /// 条件文「車系」を取得します。
            /// </summary>
            /// <returns></returns>
            public override string GetCarGroup()
            {
                return string.Format(this._diff.GetCarGroup(), BEFORE_WORD, AFTER_WORD);
            }

            /// <summary>
            /// 条件文「開発符号」を取得します。
            /// </summary>
            /// <returns></returns>
            public override string GetGeneralCode()
            {
                return string.Format(this._diff.GetGeneralCode(), BEFORE_WORD, AFTER_WORD);
            }
        }
        #endregion

        #region 大文字､小文字､全角､半角を区別して検索のカプセル化クラス
        /// <summary>
        /// 大文字､小文字､全角､半角を区別して検索のカプセル化クラス（既定は区別する）
        /// </summary>
        private class DiffOn
        {
            const string BASE_STRING = "        AND @Column {0} :@Variable {1}";

            /// <summary>
            /// 条件文「車系」を取得します。
            /// </summary>
            /// <returns></returns>
            public virtual string GetCarGroup()
            {
                return BASE_STRING.Replace("@Column", "GNR.CAR_GROUP").Replace("@Variable", "CAR_GROUP");
            }

            /// <summary>
            /// 条件文「開発符号」を取得します。
            /// </summary>
            /// <returns></returns>
            public virtual string GetGeneralCode()
            {
                return BASE_STRING.Replace("@Column", "GNR.GENERAL_CODE").Replace("@Variable", "GENERAL_CODE");
            }
        }

        /// <summary>
        /// 大文字､小文字､全角､半角を区別しないで検索クラス
        /// </summary>
        private class DiffOff: DiffOn
        {
            const string BASE_STRING = "        AND UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(@Column)), 'hwkatakana_fwkatakana') {0} UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(:@Variable)), 'hwkatakana_fwkatakana') {1}";

            /// <summary>
            /// 条件文「車系」を取得します。
            /// </summary>
            /// <returns></returns>
            public override string GetCarGroup()
            {
                return BASE_STRING.Replace("@Column", "GNR.CAR_GROUP").Replace("@Variable", "CAR_GROUP");
            }

            /// <summary>
            /// 条件文「開発符号」を取得します。
            /// </summary>
            /// <returns></returns>
            public override string GetGeneralCode()
            {
                return BASE_STRING.Replace("@Column", "GNR.GENERAL_CODE").Replace("@Variable", "GENERAL_CODE");
            }
        }
        #endregion
    }
}