using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>カーシェア予約済一覧</remarks>
    public class CarShareReserveLogic : BaseLogic
    {
        string[] DevelopmentScheduleViews =
        {
            "CARSHARING_SCHEDULE",
            "OUTERCAR_SCHEDULE"
        };

        /// <summary>
        /// カーシェア予約済一覧データの取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<CarShareReservationModel> Get(CarShareReservationSearchModel val)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            sql.AppendLine("SELECT * FROM (");

            //カーシェア(CARSHARING_SCHEDULE)
            sql.AppendLine("    SELECT");
            sql.AppendLine("        CASE ");
            sql.AppendLine("         WHEN \"SCHEDULE_TO_XEYE\".\"物品コード\" IS NOT NULL THEN ' Map'");
            sql.AppendLine("         ELSE NULL");
            sql.AppendLine("        END AS XEYE_EXIST ");
            sql.AppendLine("        ,\"CARSHARING_SCHEDULE\".\"GENERAL_CODE\" AS \"CAR_GROUP\"");
            sql.AppendLine("        ,\"試験車履歴情報\".\"開発符号\"");
            sql.AppendLine("        ,\"試験車履歴情報\".\"メーカー名\"");
            //Update Start 2022/01/11 杉浦 トラック予約一覧を追加
            //sql.AppendLine("        ,\"試験車履歴情報\".\"外製車名\"");
            sql.AppendLine("        ,\"試験車履歴情報\".\"外製車名\" \"車名\"");
            //Update End 2022/01/11 杉浦 トラック予約一覧を追加
            //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
            sql.AppendLine("        , CASE WHEN \"CARSHARING_SCHEDULE\".\"FLAG_CLASS\" IS NULL THEN NULL ELSE REPLACE(\"CARSHARING_SCHEDULE\".\"FLAG_CLASS\", '日程', '') END \"種別\"");
            //Append End 2022/01/11 杉浦 トラック予約一覧を追加
            sql.AppendLine("        ,\"VIEW_試験車基本情報\".\"駐車場番号\"");
            sql.AppendLine("        ,\"CARSHARING_SCHEDULE\".\"START_DATE\"");
            sql.AppendLine("        ,\"CARSHARING_SCHEDULE\".\"END_DATE\"");
            sql.AppendLine("        ,\"PERSONEL_LIST\".\"NAME\"");
            //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
            sql.AppendLine("        , NULL 依頼者");
            sql.AppendLine("        , NULL 発送者");
            sql.AppendLine("        , NULL 受領者");
            sql.AppendLine("        , NULL 運転者A");
            sql.AppendLine("        , NULL 運転者B");
            //Append End 2022/01/11 杉浦 トラック予約一覧を追加
            sql.AppendLine("        ,\"CARSHARING_SCHEDULE\".\"DESCRIPTION\"");
            sql.AppendLine("        ,\"試験計画_外製車日程_車両リスト\".\"管理票番号\"");
            sql.AppendLine("        ,\"CARSHARING_SCHEDULE\".\"FLAG_CLASS\"");
            sql.AppendLine("        ,\"CARSHARING_SCHEDULE_ITEM\".\"ID\"");
            sql.AppendLine("        ,\"試験計画_外製車日程_目的行先\".\"行先\"");
            sql.AppendLine("    FROM");
            sql.AppendLine("        \"CARSHARING_SCHEDULE\"");
            sql.AppendLine("        INNER JOIN \"試験計画_外製車日程_車両リスト\" ON \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\" = \"試験計画_外製車日程_車両リスト\".\"CATEGORY_ID\"");
            sql.AppendLine("        INNER JOIN \"試験計画_外製車日程_目的行先\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("        INNER JOIN \"試験計画_外製車日程_貸返備考\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_貸返備考\".\"SCHEDULE_ID\"");
            sql.AppendLine("        LEFT JOIN \"VIEW_試験車基本情報\" ON \"試験計画_外製車日程_車両リスト\".\"管理票番号\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("        LEFT JOIN \"試験車履歴情報\" ON \"試験車履歴情報\".\"データID\" = \"VIEW_試験車基本情報\".\"データID\"");
            sql.AppendLine("        INNER JOIN \"PERSONEL_LIST\" ON \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = \"PERSONEL_LIST\".\"PERSONEL_ID\"");
            sql.AppendLine("        INNER JOIN \"GENERAL_CODE\" ON \"CARSHARING_SCHEDULE\".\"GENERAL_CODE\" = \"GENERAL_CODE\".\"GENERAL_CODE\"");
            sql.AppendLine("        INNER JOIN \"CARSHARING_SCHEDULE_ITEM\" ON \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\" = \"CARSHARING_SCHEDULE_ITEM\".\"ID\"");
            sql.AppendLine("        LEFT JOIN \"SCHEDULE_TO_XEYE\" ON \"SCHEDULE_TO_XEYE\".\"物品名2\" = \"VIEW_試験車基本情報\".\"管理票NO\"");

            sql.AppendLine("    WHERE 0 = 0");
            //sql.AppendLine("        AND  TRUNC(ADD_MONTHS(SYSDATE, -1), 'MM') <= \"CARSHARING_SCHEDULE\".\"END_DATE\""); //前月の１日以降

            if (val.予約者_ID != null)
            {
                sql.AppendLine("        AND \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = :予約者_ID");
            }

            if (val.FLAG_RESERVE == 1)
            {
                sql.AppendLine("        AND \"CARSHARING_SCHEDULE\".\"START_DATE\" >= TO_DATE(SYSDATE)");
            }

            //if (val.LEND == val.FLAG_実使用 && val.FLAG_実使用 == val.FLAG_返却済)
            //{
            //    //ステータスが全て同値の場合、条件なし
            //}
            //else
            //{
            //    List<string> sqls = new List<string>();

            //    if (val.LEND == 1)
            //    {
            //        sqls.Add("((\"試験計画_外製車日程_目的行先\".\"FLAG_実使用\" IS NULL) OR (\"試験計画_外製車日程_目的行先\".\"FLAG_実使用\" = 0))");
            //    }
            //    if (val.FLAG_実使用 == 1)
            //    {
            //        sqls.Add("((\"試験計画_外製車日程_目的行先\".\"FLAG_実使用\" = 1) AND (\"試験計画_外製車日程_貸返備考\".\"FLAG_返却済\" IS NULL OR \"試験計画_外製車日程_貸返備考\".\"FLAG_返却済\" = 0))");
            //    }
            //    if (val.FLAG_返却済 == 1)
            //    {
            //        sqls.Add("((\"試験計画_外製車日程_目的行先\".\"FLAG_実使用\" = 1) AND (\"試験計画_外製車日程_貸返備考\".\"FLAG_返却済\" = 1))");
            //    }

            //    if (0 < sqls.Count)
            //    {
            //        sql.AppendLine("        AND (");

            //        for (int i = 0; i < sqls.Count; i++)
            //        {
            //            if (i == 0)
            //            {
            //                sql.AppendLine("            " + sqls[i]);
            //            }
            //            else
            //            {
            //                sql.AppendLine("            OR " + sqls[i]);
            //            }
            //        }

            //        sql.AppendLine("        )");
            //    }
            //}

            sql.AppendLine("UNION");

            //外製車(OUTERCAR_SCHEDULE)
            sql.AppendLine("    SELECT");
            sql.AppendLine("        CASE ");
            sql.AppendLine("         WHEN \"SCHEDULE_TO_XEYE\".\"物品コード\" IS NOT NULL THEN ' Map'");
            sql.AppendLine("         ELSE NULL");
            sql.AppendLine("        END AS XEYE_EXIST ");
            sql.AppendLine("        ,\"OUTERCAR_SCHEDULE\".\"GENERAL_CODE\" AS \"CAR_GROUP\"");
            sql.AppendLine("        ,\"OUTERCAR_SCHEDULE\".\"GENERAL_CODE\" AS \"開発符号\"");
            sql.AppendLine("        ,\"試験車履歴情報\".\"メーカー名\"");
            //Update Start 2022/01/11 杉浦 トラック予約一覧を追加
            //sql.AppendLine("        ,\"試験車履歴情報\".\"外製車名\"");
            sql.AppendLine("        ,\"試験車履歴情報\".\"外製車名\" \"車名\"");
            //Update End 2022/01/11 杉浦 トラック予約一覧を追加
            //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
            sql.AppendLine("        , CASE WHEN \"OUTERCAR_SCHEDULE\".\"FLAG_CLASS\" IS NULL THEN NULL ELSE REPLACE(\"OUTERCAR_SCHEDULE\".\"FLAG_CLASS\", '日程', '') END \"種別\"");
            //Append End 2022/01/11 杉浦 トラック予約一覧を追加
            sql.AppendLine("        ,\"VIEW_試験車基本情報\".\"駐車場番号\"");
            sql.AppendLine("        ,\"OUTERCAR_SCHEDULE\".\"START_DATE\"");
            sql.AppendLine("        ,\"OUTERCAR_SCHEDULE\".\"END_DATE\"");
            sql.AppendLine("        ,\"PERSONEL_LIST\".\"NAME\"");
            //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
            sql.AppendLine("        , NULL 依頼者");
            sql.AppendLine("        , NULL 発送者");
            sql.AppendLine("        , NULL 受領者");
            sql.AppendLine("        , NULL 運転者A");
            sql.AppendLine("        , NULL 運転者B");
            //Append End 2022/01/11 杉浦 トラック予約一覧を追加
            sql.AppendLine("        ,\"OUTERCAR_SCHEDULE\".\"DESCRIPTION\"");
            sql.AppendLine("        ,\"試験計画_外製車日程_車両リスト\".\"管理票番号\"");
            sql.AppendLine("        ,\"OUTERCAR_SCHEDULE\".\"FLAG_CLASS\"");
            sql.AppendLine("        ,\"OUTERCAR_SCHEDULE_ITEM\".\"ID\"");
            sql.AppendLine("        ,\"試験計画_外製車日程_目的行先\".\"行先\"");
            sql.AppendLine("    FROM");
            sql.AppendLine("        \"OUTERCAR_SCHEDULE\"");
            sql.AppendLine("        INNER JOIN \"試験計画_外製車日程_車両リスト\" ON \"OUTERCAR_SCHEDULE\".\"CATEGORY_ID\" = \"試験計画_外製車日程_車両リスト\".\"CATEGORY_ID\"");
            sql.AppendLine("        INNER JOIN \"試験計画_外製車日程_目的行先\" ON \"OUTERCAR_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("        LEFT JOIN \"VIEW_試験車基本情報\" ON \"試験計画_外製車日程_車両リスト\".\"管理票番号\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("        LEFT JOIN \"試験車履歴情報\" ON \"試験車履歴情報\".\"データID\" = \"VIEW_試験車基本情報\".\"データID\"");
            sql.AppendLine("        INNER JOIN \"PERSONEL_LIST\" ON \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = \"PERSONEL_LIST\".\"PERSONEL_ID\"");
            sql.AppendLine("        INNER JOIN \"GENERAL_CODE\" ON \"OUTERCAR_SCHEDULE\".\"GENERAL_CODE\" = \"GENERAL_CODE\".\"GENERAL_CODE\"");
            sql.AppendLine("        INNER JOIN \"OUTERCAR_SCHEDULE_ITEM\" ON \"OUTERCAR_SCHEDULE\".\"CATEGORY_ID\" = \"OUTERCAR_SCHEDULE_ITEM\".\"ID\"");
            sql.AppendLine("        LEFT JOIN \"SCHEDULE_TO_XEYE\" ON \"SCHEDULE_TO_XEYE\".\"物品名2\" = \"VIEW_試験車基本情報\".\"管理票NO\"");

            sql.AppendLine("    WHERE 0 = 0");
            //sql.AppendLine("        AND  TRUNC(ADD_MONTHS(SYSDATE, -1), 'MM') <= \"OUTERCAR_SCHEDULE\".\"END_DATE\""); //前月の１日以降

            if (val.予約者_ID != null)
            {
                sql.AppendLine("        AND \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = :予約者_ID");
            }

            if (val.FLAG_RESERVE == 1)
            {
                sql.AppendLine("        AND \"OUTERCAR_SCHEDULE\".\"START_DATE\" >= TO_DATE(SYSDATE)");
            }

            //if (val.LEND == val.FLAG_実使用 && val.FLAG_実使用 == val.FLAG_返却済)
            //{
            //    //ステータスが全て同値の場合、条件なし
            //}
            //else
            //{
            //    List<string> sqls = new List<string>();

            //    if (val.LEND == 1)
            //    {
            //        sqls.Add("(SYSDATE < \"OUTERCAR_SCHEDULE\".\"START_DATE\")");
            //    }
            //    if (val.FLAG_実使用 == 1)
            //    {
            //        sqls.Add("((\"OUTERCAR_SCHEDULE\".\"START_DATE\" <= SYSDATE) AND (SYSDATE <= \"OUTERCAR_SCHEDULE\".\"END_DATE\"))");
            //    }
            //    if (val.FLAG_返却済 == 1)
            //    {
            //        sqls.Add("(\"OUTERCAR_SCHEDULE\".\"END_DATE\" < SYSDATE)");
            //    }

            //    if (0 < sqls.Count)
            //    {
            //        sql.AppendLine("        AND (");

            //        for (int i = 0; i < sqls.Count; i++)
            //        {
            //            if (i == 0)
            //            {
            //                sql.AppendLine("            " + sqls[i]);
            //            }
            //            else
            //            {
            //                sql.AppendLine("            OR " + sqls[i]);
            //            }
            //        }

            //        sql.AppendLine("        )");
            //    }
            //}

            //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
            sql.AppendLine("UNION");
            sql.AppendLine("    SELECT");
            sql.AppendLine("        NULL XEYE_EXIST");
            sql.AppendLine("        , NULL CAR_GROUP");
            sql.AppendLine("        , NULL 開発符号");
            sql.AppendLine("        , NULL メーカー名");
            sql.AppendLine("        , ITEM.車両名 車名");
            sql.AppendLine("        , CASE YOYAKU.FLAG_定期便 WHEN 1 THEN 'トラック定期便' ELSE '各トラック便' END \"種別\"");
            sql.AppendLine("        , NULL \"駐車場番号\"");
            sql.AppendLine("        , CASE YOYAKU.FLAG_定期便 ");
            sql.AppendLine("              WHEN 1 THEN TRUNC(YOYAKU.予約開始時間) + TO_NUMBER(SUBSTR(TIMES.DEPARTURE_TIME, 1, INSTR(TIMES.DEPARTURE_TIME, ':', 1, 1) - 1)) / 24 + TO_NUMBER( SUBSTR( TIMES.DEPARTURE_TIME, INSTR(TIMES.DEPARTURE_TIME, ':', 1, 1) + 1)) / 1440 ");
            sql.AppendLine("              ELSE YOYAKU.予約開始時間 ");
            sql.AppendLine("            END START_DATE");
            sql.AppendLine("        , CASE YOYAKU.FLAG_定期便 WHEN 1 THEN NULL ELSE YOYAKU.予約終了時間 END \"END_DATE\"");
            sql.AppendLine("        , 予約者名 \"NAME\"");
            sql.AppendLine("        , 定期便依頼者名 依頼者");
            sql.AppendLine("        , 発送者名 発送者");
            sql.AppendLine("        , 受領者名 受領者");
            sql.AppendLine("        , 運転者A名 運転者A");
            sql.AppendLine("        , 運転者B名 運転者B");
            sql.AppendLine("        , CASE YOYAKU.FLAG_定期便 WHEN 1 THEN YOYAKU.備考 ELSE YOYAKU.使用目的 END \"DESCRIPTION\"");
            sql.AppendLine("        , NULL 管理票番号");
            sql.AppendLine("        , NULL \"FLAG_CLASS\"");
            sql.AppendLine("        , ITEM.\"ID\"");
            sql.AppendLine("        , CASE YOYAKU.FLAG_定期便 ");
            sql.AppendLine("            WHEN 1 THEN CASE ");
            sql.AppendLine("                WHEN ITEM.始発場所 = '群馬' THEN");
            sql.AppendLine("                    CASE MOD(PARALLEL_INDEX_GROUP, 2) WHEN 1 THEN");
            //Update Start 2022/03/10 杉浦
            //sql.AppendLine("                        'SKC'");
            //sql.AppendLine("                    ELSE '群馬'");
            sql.AppendLine("                        '群馬→SKC'");
            sql.AppendLine("                    ELSE 'SKC→群馬'");
            //Update End 2022/03/10 杉浦
            sql.AppendLine("                    END");
            sql.AppendLine("                ELSE");
            sql.AppendLine("                    CASE MOD(PARALLEL_INDEX_GROUP, 2) WHEN 1 THEN");
            //Update Start 2022/03/10 杉浦
            //sql.AppendLine("                        '群馬'");
            //sql.AppendLine("                    ELSE 'SKC'");
            sql.AppendLine("                        'SKC→群馬'");
            sql.AppendLine("                    ELSE '群馬→SKC'");
            //Update End 2022/03/10 杉浦
            sql.AppendLine("                    END");
            sql.AppendLine("                END");
            sql.AppendLine("            ELSE SECTIONS.発着地 ");
            sql.AppendLine("            END \"行先\" ");
            sql.AppendLine("    FROM");
            sql.AppendLine("        トラック_予約状況 YOYAKU ");
            sql.AppendLine("        INNER JOIN トラック_情報 ITEM ");
            sql.AppendLine("            ON YOYAKU.トラック_ID = ITEM.ID ");
            sql.AppendLine("        LEFT JOIN TRUCK_REGULAR_TIME_MST TIMES ");
            sql.AppendLine("            ON ITEM.REGULAR_TIME_ID = TIMES.REGULAR_ID ");
            sql.AppendLine("            AND TIMES.TIME_ID = TO_NUMBER(TO_CHAR(YOYAKU.予約開始時間, 'HH24')) ");
            sql.AppendLine("        LEFT JOIN トラック_定期便発送者受領者 USERS ");
            sql.AppendLine("            ON USERS.予約_ID = YOYAKU.ID ");
            sql.AppendLine("        LEFT JOIN (SELECT LISTAGG(発着地, '～') WITHIN GROUP (order by SORT_NO) 発着地, 予約_ID FROM トラック_発着地  GROUP BY 予約_ID) SECTIONS ");
            sql.AppendLine("            ON SECTIONS.予約_ID = YOYAKU.ID");
            sql.AppendLine("    WHERE 0 = 0");

            if (val.予約者_ID != null)
            {
                sql.AppendLine("        AND YOYAKU.予約者_ID = :予約者_ID");
            }
            if (val.FLAG_RESERVE == 1)
            {
                sql.AppendLine("        AND YOYAKU.\"予約開始時間\" >= TO_DATE(SYSDATE)");
            }
            //Append End 2022/01/11 杉浦 トラック予約一覧を追加

            sql.AppendLine(" ) ORDER BY START_DATE, END_DATE");

            if (val.予約者_ID != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":予約者_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.予約者_ID,
                    Direct = ParameterDirection.Input
                });
            }

            return db.ReadModelList<CarShareReservationModel>(sql.ToString(), prms);
        }
    }
}