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
    /// <remarks>トラック予約済一覧</remarks>
    public class TruckReserveLogic : BaseLogic
    {
        string[] DevelopmentScheduleViews =
        {
            "CARSHARING_SCHEDULE",
            "OUTERCAR_SCHEDULE"
        };

        /// <summary>
        /// トラック予約済一覧データの取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<CarShareReservationModel> Get(TruckReserveInModel val)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            sql.AppendLine("SELECT * FROM (");
            
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
                sql.AppendLine("        AND 予約者_ID = :予約者_ID");
                prms.Add(new BindModel
                {
                    Name = ":予約者_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.予約者_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.依頼者_ID != null)
            {
                sql.AppendLine("        AND 定期便依頼者_ID = :依頼者_ID");
                prms.Add(new BindModel
                {
                    Name = ":依頼者_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.依頼者_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.発送者_ID != null)
            {
                sql.AppendLine("        AND 発送者_ID = :発送者_ID");
                prms.Add(new BindModel
                {
                    Name = ":発送者_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.発送者_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.受領者_ID != null)
            {
                sql.AppendLine("        AND 受領者_ID = :受領者_ID");
                prms.Add(new BindModel
                {
                    Name = ":受領者_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.受領者_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.運転者A_ID != null)
            {
                sql.AppendLine("        AND 運転者A_ID = :運転者A_ID");
                prms.Add(new BindModel
                {
                    Name = ":運転者A_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.運転者A_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.運転者B_ID != null)
            {
                sql.AppendLine("        AND 運転者B_ID = :運転者B_ID");
                prms.Add(new BindModel
                {
                    Name = ":運転者B_ID",
                    Type = OracleDbType.Varchar2,
                    Object = val.運転者B_ID,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.FLAG_RESERVE == 1)
            {
                sql.AppendLine("        AND YOYAKU.\"予約開始時間\" >= TO_DATE(SYSDATE)");
            }
            
            sql.AppendLine(" ) ORDER BY START_DATE, END_DATE");
            

            return db.ReadModelList<CarShareReservationModel>(sql.ToString(), prms);
        }
    }
}