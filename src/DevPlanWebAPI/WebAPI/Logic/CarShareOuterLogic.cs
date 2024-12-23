using System.Data;
using System.Text;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>カーシェア外製車検索</remarks>
    public class CarShareOuterLogic : BaseLogic
    {
        /// <summary>
        /// カーシェア外製車データの取得
        /// </summary>
        /// <returns>DataTable</returns>
        public IEnumerable<CarShareOuterSearchOutModel> GetData(CarShareOuterSearchInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     H.車系");
            sql.AppendLine("    ,G.開発符号");
            sql.AppendLine("    ,G.メーカー名");
            sql.AppendLine("    ,G.外製車名");
            sql.AppendLine("    ,B.SECTION_NAME");
            sql.AppendLine("    ,J.NAME AS 担当");
            sql.AppendLine("    ,'SYS' as 予約方法");
            sql.AppendLine("    ,G.登録ナンバー");
            sql.AppendLine("    ,H.駐車場番号");
            sql.AppendLine("    ,H.所在地");
            sql.AppendLine("    ,G.FLAG_ETC付");
            sql.AppendLine("    ,CASE G.FLAG_ETC付 WHEN 0 THEN '無' WHEN 1 THEN '有' ELSE '' END ETC");
            sql.AppendLine("    ,G.FLAG_ナビ付");
            sql.AppendLine("    ,CASE G.FLAG_ナビ付 WHEN 0 THEN '無' WHEN 1 THEN '有' ELSE '' END ナビ");
            sql.AppendLine("    ,G.仕向地");
            sql.AppendLine("    ,G.排気量");
            sql.AppendLine("    ,G.E_G型式");
            sql.AppendLine("    ,G.駆動方式");
            sql.AppendLine("    ,G.トランスミッション");
            sql.AppendLine("    ,H.車型");
            sql.AppendLine("    ,G.グレード");
            sql.AppendLine("    ,H.型式符号");
            sql.AppendLine("    ,G.車体色");
            sql.AppendLine("    ,G.試作時期");
            sql.AppendLine("    ,H.リース満了日");
            sql.AppendLine("    ,I.処分予定年月");
            sql.AppendLine("    ,H.管理票NO");
            sql.AppendLine("    ,G.車体番号");
            sql.AppendLine("    ,G.E_G番号");
            sql.AppendLine("    ,G.固定資産NO");
            sql.AppendLine("    ,H.リースNO");
            sql.AppendLine("    ,G.研命ナンバー");
            sql.AppendLine("    ,G.研命期間");
            sql.AppendLine("    ,G.車検登録日");
            sql.AppendLine("    ,G.車検期限");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("            WHEN G.MONTH_DIFF = 0 THEN (G.車検期限 - G.TODAY) || '日'");
            sql.AppendLine("            WHEN G.MONTH_DIFF > 0 THEN '約' || G.MONTH_DIFF || 'ヶ月'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 車検期限まで残り");
            sql.AppendLine("    ,G.廃艦日");
            sql.AppendLine("    ,G.号車");
            sql.AppendLine("    ,G.名称備考");
            sql.AppendLine("    ,G.試験目的");
            sql.AppendLine("    ,H.メモ");
            sql.AppendLine("FROM");
            sql.AppendLine("    DEPARTMENT_DATA A");
            sql.AppendLine("    INNER JOIN SECTION_DATA B ON A.DEPARTMENT_ID = B.DEPARTMENT_ID");
            sql.AppendLine("    INNER JOIN SECTION_GROUP_DATA C ON B.SECTION_ID = C.SECTION_ID");
            sql.AppendLine("    INNER JOIN");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             D.データID");
            sql.AppendLine("            ,D.履歴NO");
            sql.AppendLine("            ,D.管理責任部署");
            sql.AppendLine("            ,D.開発符号");
            sql.AppendLine("            ,D.登録ナンバー");
            sql.AppendLine("            ,D.外製車名");
            sql.AppendLine("            ,D.メーカー名");
            sql.AppendLine("            ,D.試験目的");
            sql.AppendLine("            ,D.FLAG_ETC付");
            sql.AppendLine("            ,D.FLAG_ナビ付");
            sql.AppendLine("            ,D.仕向地");
            sql.AppendLine("            ,D.排気量");
            sql.AppendLine("            ,D.E_G型式");
            sql.AppendLine("            ,D.駆動方式");
            sql.AppendLine("            ,D.トランスミッション");
            sql.AppendLine("            ,D.グレード");
            sql.AppendLine("            ,D.車体色");
            sql.AppendLine("            ,D.試作時期");
            sql.AppendLine("            ,D.車体番号");
            sql.AppendLine("            ,D.E_G番号");
            sql.AppendLine("            ,D.固定資産NO");
            sql.AppendLine("            ,D.研命ナンバー");
            sql.AppendLine("            ,D.研命期間");
            sql.AppendLine("            ,D.車検登録日");
            sql.AppendLine("            ,TRUNC(D.車検期限) AS 車検期限");
            sql.AppendLine("            ,D.廃艦日");
            sql.AppendLine("            ,D.号車");
            sql.AppendLine("            ,D.名称備考");
            sql.AppendLine("            ,D.受領者");
            sql.AppendLine("            ,TRUNC(SYSDATE) AS TODAY");
            sql.AppendLine("            ,TRUNC(MONTHS_BETWEEN(TRUNC(D.車検期限),TRUNC(SYSDATE))) AS MONTH_DIFF");
            sql.AppendLine("        FROM");
            sql.AppendLine("            試験車履歴情報 D");
            sql.AppendLine("            INNER JOIN");
            sql.AppendLine("                        (");
            sql.AppendLine("                            SELECT");
            sql.AppendLine("                                 E.データID");
            sql.AppendLine("                                ,MAX(E.履歴NO) AS 履歴NO");
            sql.AppendLine("                            FROM");
            sql.AppendLine("                                試験車履歴情報 E");
            sql.AppendLine("                            GROUP BY");
            sql.AppendLine("                                E.データID");
            sql.AppendLine("                        ) F    ");
            sql.AppendLine("            ON D.データID = F.データID");
            sql.AppendLine("            AND D.履歴NO = F.履歴NO");
            sql.AppendLine("    ) G");
            sql.AppendLine("    ON G.管理責任部署 = C.SECTION_GROUP_ID");
            sql.AppendLine("    INNER JOIN VIEW_試験車基本情報 H ON H.データID = G.データID");
            sql.AppendLine("    INNER JOIN 固定資産情報 I ON H.データID = I.データID");
            sql.AppendLine("    LEFT JOIN PERSONEL_LIST J ON J.PERSONEL_ID = G.受領者");
            //Append Start 2022/02/24 杉浦 試験車日程の車も登録する
            sql.AppendLine("    LEFT JOIN ");
            sql.AppendLine("    (");
            sql.AppendLine("         SELECT DISTINCT");
            sql.AppendLine("             CASE WHEN TESTCAR_ITEM_ID IS NOT NULL AND OUTERCAR_ITEM_ID IS NOT NULL AND CARSHARING_ITEM_ID IS NOT NULL THEN 1 ELSE NULL END SIGN");
            sql.AppendLine("             , 管理票番号");
            sql.AppendLine("         FROM");
            sql.AppendLine("            ( ");
            sql.AppendLine("                SELECT");
            sql.AppendLine("                      A.管理票番号");
            sql.AppendLine("                    , MAX(CASE D.SEQ WHEN 1 THEN D.ID ELSE NULL END) TESTCAR_ITEM_ID");
            sql.AppendLine("                    , MAX(CASE D.SEQ WHEN 1 THEN D.CATEGORY_ID ELSE NULL END) TESTCAR_CATEGORY_ID");
            sql.AppendLine("                    , MAX(CASE D.SEQ WHEN 2 THEN D.ID ELSE NULL END) OUTERCAR_ITEM_ID");
            sql.AppendLine("                    , MAX( CASE D.SEQ  WHEN 2 THEN D.CATEGORY_ID ELSE NULL END) OUTERCAR_CATEGORY_ID");
            sql.AppendLine("                    , MAX(CASE D.SEQ WHEN 3 THEN D.ID ELSE NULL END) CARSHARING_ITEM_ID");
            sql.AppendLine("                    , MAX( CASE D.SEQ WHEN 3 THEN D.CATEGORY_ID ELSE NULL END) CARSHARING_CATEGORY_ID ");
            sql.AppendLine("                FROM");
            sql.AppendLine("                    SCHEDULE_CAR A ");
            sql.AppendLine("                    LEFT JOIN ( ");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                              D.* ");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            SCHEDULE_CAR A ");
            sql.AppendLine("                            LEFT JOIN ( SELECT 1 SEQ, ID, CATEGORY_ID FROM TESTCAR_SCHEDULE_ITEM) D ");
            sql.AppendLine("                                ON A.CATEGORY_ID = D.ID AND D.ID IS NOT NULL ");
            sql.AppendLine("                        UNION ALL ");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                              E.* ");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            SCHEDULE_CAR A ");
            sql.AppendLine("                             LEFT JOIN ( SELECT 2 SEQ, ID, CATEGORY_ID FROM OUTERCAR_SCHEDULE_ITEM) E ");
            sql.AppendLine("                                ON A.CATEGORY_ID = E.ID AND E.ID IS NOT NULL ");
            sql.AppendLine("                        UNION ALL ");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                              F.* ");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            SCHEDULE_CAR A ");
            sql.AppendLine("                             LEFT JOIN ( SELECT 3 SEQ, ID, CATEGORY_ID FROM CARSHARING_SCHEDULE_ITEM) F ");
            sql.AppendLine("                                ON A.CATEGORY_ID = F.ID AND F.ID IS NOT NULL ");
            sql.AppendLine("                    )D ");
            sql.AppendLine("                        ON A.CATEGORY_ID = D.ID AND D.ID IS NOT NULL ");
            sql.AppendLine("                    GROUP BY A.管理票番号 ");
            sql.AppendLine("             )CAR ");
            sql.AppendLine("            LEFT JOIN 試験計画_外製車日程_最終予約日 ");
            sql.AppendLine("              ON ( ");
            sql.AppendLine("                 CAR.TESTCAR_CATEGORY_ID = 試験計画_外製車日程_最終予約日.CATEGORY_ID OR CAR.OUTERCAR_CATEGORY_ID = 試験計画_外製車日程_最終予約日.CATEGORY_ID OR CAR.CARSHARING_CATEGORY_ID = 試験計画_外製車日程_最終予約日.CATEGORY_ID");
            sql.AppendLine("         ) ");
            sql.AppendLine("         WHERE 0 = 0");
            sql.AppendLine("             AND CAR.管理票番号 IS NOT NULL");
            sql.AppendLine("             AND (試験計画_外製車日程_最終予約日.最終予約可能日 > TRUNC(SYSDATE) - 1 ");
            sql.AppendLine("             OR 試験計画_外製車日程_最終予約日.最終予約可能日 IS NULL) ");
            sql.AppendLine("                    ) K");
            sql.AppendLine("     ON K.管理票番号 = H.管理票NO");
            //Append End 2022/02/24 杉浦 試験車日程の車も登録する
            sql.AppendLine("WHERE");
            sql.AppendLine("    H.廃却決済承認年月 IS NULL");
            sql.AppendLine("    AND H.車両搬出日 IS NULL");
            sql.AppendLine("    AND H.研実管理廃却申請受理日 IS NULL");
            sql.AppendLine("    AND G.メーカー名 <> 'スバル'");
            //Append Start 2023/10/21 メーカー名の絞り込み追加
            sql.AppendLine("    AND G.メーカー名 <> 'ｽﾊﾞﾙ'");
            //Append End 2023/10/21 メーカー名の絞り込み追加
            sql.AppendLine("    AND G.登録ナンバー IS NOT NULL");

            // カーシェア日程での利用では管理責任部署追加
            sql.AppendLine(string.Format("    AND (C.SECTION_ID IN ('{0}')", string.Join("','", Const.SoukatsuSectionIds)));
            if (val.CARSHARE_FLG)
            {
                sql.AppendLine(string.Format("        OR C.SECTION_GROUP_ID IN ('{0}')", string.Join("','", Const.CarShareStaffSectionGroupIds)));
            }
            sql.AppendLine("        )");

            sql.AppendLine(string.Format("    AND C.SECTION_GROUP_ID NOT IN ('{0}')", string.Join("','", Const.SoukatsuNotSectionGroupIds)));

            //スケジュールに登録されてる車両は除外
            //Update Start 2022/02/24 杉浦 試験車日程の車も登録する
            //sql.AppendLine("    AND NOT EXISTS");
            //sql.AppendLine("                    (");
            //sql.AppendLine("                        SELECT");
            //sql.AppendLine("                            *");
            //sql.AppendLine("                        FROM");
            //sql.AppendLine("                            SCHEDULE_CAR CAR");
            //sql.AppendLine("    LEFT JOIN 試験計画_外製車日程_最終予約日 ON CAR.CATEGORY_ID = 試験計画_外製車日程_最終予約日.CATEGORY_ID");
            //sql.AppendLine("                        WHERE 0 = 0");
            //sql.AppendLine("                            AND CAR.管理票番号 IS NOT NULL");
            //sql.AppendLine("                            AND CAR.管理票番号 = H.管理票NO");
            //sql.AppendLine(" AND (試験計画_外製車日程_最終予約日.最終予約可能日 > TRUNC(SYSDATE) - 1 ");
            //sql.AppendLine(" OR 試験計画_外製車日程_最終予約日.最終予約可能日 IS NULL) ");
            //sql.AppendLine("                    )");
            sql.AppendLine("    AND K.SIGN IS NULL");
            //Update End 2022/02/24 杉浦 試験車日程の車も登録する
            //カーシェア日程・外製車日程に登録されてる車両は除外
            /*
            sql.AppendLine("    AND NOT EXISTS");
            sql.AppendLine("                    (");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                            *");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            CARSHARING_SCHEDULE_ITEM A");
            sql.AppendLine("                            INNER JOIN 試験計画_外製車日程_車両リスト B");
            sql.AppendLine("                            ON A.ID = B.CATEGORY_ID");
            sql.AppendLine("                        WHERE 0 = 0");
            sql.AppendLine("                            AND B.管理票番号 IS NOT NULL");
            sql.AppendLine("                            AND B.管理票番号 = H.管理票NO");
            sql.AppendLine("                    )");
            sql.AppendLine("    AND NOT EXISTS");
            sql.AppendLine("                    (");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                            *");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            OUTERCAR_SCHEDULE_ITEM A");
            sql.AppendLine("                            INNER JOIN 試験計画_外製車日程_車両リスト B");
            sql.AppendLine("                            ON A.ID = B.CATEGORY_ID");
            sql.AppendLine("                        WHERE 0 = 0");
            sql.AppendLine("                            AND B.管理票番号 IS NOT NULL");
            sql.AppendLine("                            AND B.管理票番号 = H.管理票NO");
            sql.AppendLine("                    )");
            */

sql.AppendLine("ORDER BY");
            sql.AppendLine("     H.駐車場番号");
            sql.AppendLine("    ,H.車系");
            sql.AppendLine("    ,G.開発符号");
            sql.AppendLine("    ,H.管理票NO");

            return db.ReadModelList<CarShareOuterSearchOutModel>(sql.ToString(), prms);
        }
    }
}