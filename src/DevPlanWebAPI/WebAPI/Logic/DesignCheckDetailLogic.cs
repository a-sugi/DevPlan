using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 設計チェック詳細
    /// </summary>
    public class DesignCheckDetailLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 設計チェック詳細の検索
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public DataTable GetData(DesignCheckDetailGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("      試験計画_DCHK_開催日.開催日");
            sql.AppendLine("    , CASE WHEN 試験計画_DCHK_開催日.回 IS NULL THEN 試験計画_DCHK_開催日.名称 ELSE 試験計画_DCHK_開催日.名称 || ' ' || 試験計画_DCHK_開催日.回 || '回' END AS 名称");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.指摘NO");
            //APPEND Start 2021/08/02 杉浦 設計チェック修正対応
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.試作管理NO");
            //APPEND End 2021/08/02 杉浦 設計チェック修正対応
            sql.AppendLine("    , CASE WHEN NVL(試験計画_DCHK_指摘リスト.FLAG_CLOSE, 0) = 0 THEN 'OPEN' ELSE 'CLOSE' END AS ステータス");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.部品");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.状況");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.FLAG_処置不要");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.処置課");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.処置対象");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.処置方法");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.FLAG_調整済");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.処置調整");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.織込日程");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.FLAG_試作改修");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.部品納入日");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.担当課名");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.担当者_ID");
            sql.AppendLine("    , CASE WHEN PERSONEL_LIST.NAME IS NULL THEN 試験計画_DCHK_指摘リスト.担当者_ID ELSE PERSONEL_LIST.NAME END 担当者名");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.担当者_TEL");
            sql.AppendLine("    , 試験計画_DCHK_試験車.試験車名");
            sql.AppendLine("    , 試験計画_DCHK_状況.状況 状況記号");
            sql.AppendLine("    , 試験計画_DCHK_指摘リスト.ID AS 指摘ID");
            sql.AppendLine("    , 試験計画_DCHK_試験車.ID 試験車_ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_DCHK_開催日");
            sql.AppendLine("    LEFT JOIN 試験計画_DCHK_指摘リスト");
            sql.AppendLine("    ON 試験計画_DCHK_開催日.ID = 試験計画_DCHK_指摘リスト.開催日_ID");
            sql.AppendLine("    LEFT JOIN (SELECT * FROM 試験計画_DCHK_状況 WHERE ID IN(SELECT MIN(ID) FROM 試験計画_DCHK_状況 GROUP BY 指摘_ID, 対象車両_ID)) 試験計画_DCHK_状況");
            sql.AppendLine("    ON 試験計画_DCHK_指摘リスト.ID = 試験計画_DCHK_状況.指摘_ID");
            sql.AppendLine("    LEFT JOIN 試験計画_DCHK_対象車両");
            sql.AppendLine("    ON 試験計画_DCHK_状況.対象車両_ID = 試験計画_DCHK_対象車両.ID");
            sql.AppendLine("    LEFT JOIN 試験計画_DCHK_試験車");
            sql.AppendLine("    ON 試験計画_DCHK_対象車両.試験車_ID = 試験計画_DCHK_試験車.ID");
            sql.AppendLine("    LEFT JOIN PERSONEL_LIST");
            sql.AppendLine("    ON 試験計画_DCHK_指摘リスト.担当者_ID = PERSONEL_LIST.PERSONEL_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");
            sql.AppendLine("    AND NVL(試験計画_DCHK_指摘リスト.FLAG_最新, 0) = 1");

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

            // 担当課名
            if (val != null && !string.IsNullOrWhiteSpace(val.担当課名))
            {
                sql.AppendLine("    AND 試験計画_DCHK_指摘リスト.担当課名 = :担当課名");

                prms.Add(new BindModel
                {
                    Name = ":担当課名",
                    Type = OracleDbType.Varchar2,
                    Object = val.担当課名,
                    Direct = ParameterDirection.Input
                });
            }

            // オープンフラグ（true：Open、false：Close）
            if (val != null && val.OPEN_FLG == true)
            {
                sql.AppendLine("    AND NVL(試験計画_DCHK_指摘リスト.FLAG_CLOSE, 0) <> 1");
            }
            else if (val != null && val.OPEN_FLG == false)
            {
                sql.AppendLine("    AND NVL(試験計画_DCHK_指摘リスト.FLAG_CLOSE, 0) = 1");
            }

            // 開発符号
            if (val != null && !string.IsNullOrWhiteSpace(val.開発符号))
            {
                sql.AppendLine("    AND UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(試験計画_DCHK_試験車.試験車名)), 'kana_fwkatakana') like '%' || UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(:開発符号)), 'kana_fwkatakana') || '%'");

                prms.Add(new BindModel
                {
                    Name = ":開発符号",
                    Type = OracleDbType.Varchar2,
                    Object = val.開発符号,
                    Direct = ParameterDirection.Input
                });
            }

            // 試作時期
            if (val != null && !string.IsNullOrWhiteSpace(val.試作時期))
            {
                sql.AppendLine("    AND UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(試験計画_DCHK_試験車.試験車名)), 'kana_fwkatakana') like '%' || UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(:試作時期)), 'kana_fwkatakana') || '%'");

                prms.Add(new BindModel
                {
                    Name = ":試作時期",
                    Type = OracleDbType.Varchar2,
                    Object = val.試作時期,
                    Direct = ParameterDirection.Input
                });
            }

            // 号車
            if (val != null && !string.IsNullOrWhiteSpace(val.号車))
            {
                sql.AppendLine("    AND UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(試験計画_DCHK_試験車.試験車名)), 'kana_fwkatakana') like '%' || UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(:号車)), 'kana_fwkatakana') || '%'");

                prms.Add(new BindModel
                {
                    Name = ":号車",
                    Type = OracleDbType.Varchar2,
                    Object = val.号車,
                    Direct = ParameterDirection.Input
                });
            }

            // 部品
            if (val != null && !string.IsNullOrWhiteSpace(val.部品))
            {
                sql.AppendLine("    AND UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(試験計画_DCHK_指摘リスト.部品)), 'kana_fwkatakana') like '%' || UTL_I18N.TRANSLITERATE(UPPER(TO_MULTI_BYTE(:部品)), 'kana_fwkatakana') || '%'");

                prms.Add(new BindModel
                {
                    Name = ":部品",
                    Type = OracleDbType.Varchar2,
                    Object = val.部品,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("      試験計画_DCHK_開催日.開催日 DESC");
            sql.AppendLine("     ,試験計画_DCHK_指摘リスト.指摘NO ASC");
            sql.AppendLine("     ,試験計画_DCHK_状況.対象車両_ID ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }
    }
}