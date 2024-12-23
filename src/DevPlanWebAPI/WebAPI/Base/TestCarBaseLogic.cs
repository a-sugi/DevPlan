using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Base
{
    /// <summary>
    /// 試験車基底業務ロジッククラス
    /// </summary>
    public class TestCarBaseLogic : BaseLogic
    {
        #region メンバ変数
        /// <summary>カンマ</summary>
        protected const string Comma = ",";

        /// <summary>アスタリスク</summary>
        protected const string Asterisk = "*";

        /// <summary>部分一致</summary>
        protected const string Like = "%";

        /// <summary>変換関数</summary>
        protected const string ConvertFunc = "UPPER(TO_SINGLE_BYTE(UTL_I18N.TRANSLITERATE({0},'hwkatakana_fwkatakana')))";

        private Dictionary<string, int> paramNameMap = new Dictionary<string, int>();
        #endregion

        #region 試験車情報の共通SQL取得
        /// <summary>
        /// 試験車情報の共通SQL取得
        /// </summary>
        /// <param name="testCarHistoryWhere">試験車履歴情報の取得条件</param>
        /// <param name="isHistoryMax">試験車情報の最新取得可否</param>
        /// <returns></returns>
        protected StringBuilder GetBaseTestCarSql(List<BindModel> testCarHistoryWhere = null, bool isHistoryMax = true)
        {
            return GetBaseTestCarSql(null, null, testCarHistoryWhere, isHistoryMax);

        }

        /// <summary>
        /// 試験車情報の共通SQL取得
        /// </summary>
        /// <param name="addColumn">追加で取得する列</param>
        /// <param name="addTable">追加で取得するテーブル</param>
        /// <param name="testCarHistoryWhere">試験車履歴情報の取得条件</param>
        /// <param name="isHistoryMax">試験車情報の最新取得可否</param>
        /// <param name="isWithSI">部署関連With句利用可否</param>
        /// <param name="addHint">ヒント文</param>
        /// <returns></returns>
        protected StringBuilder GetBaseTestCarSql(Action<StringBuilder> addColumn, Action<StringBuilder> addTable, List<BindModel> testCarHistoryWhere = null, bool isHistoryMax = true, bool isWithSI = false, string addHint = null)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT").AppendLine(addHint != null ? string.Format(" {0}", addHint) : "");
            sql.AppendLine("     KK.\"データID\"");
            sql.AppendLine("    ,KK.\"管理票NO\"");
            sql.AppendLine("    ,KK.\"管理ラベル発行有無\"");
            sql.AppendLine("    ,DECODE(KK.\"管理ラベル発行有無\",1,'済',NULL) AS \"管理ラベル発行\"");
            sql.AppendLine("    ,KK.\"車系\"");
            sql.AppendLine("    ,KK.\"車型\"");
            sql.AppendLine("    ,KK.\"型式符号\"");
            sql.AppendLine("    ,KK.\"駐車場番号\"");
            sql.AppendLine("    ,KK.\"リースNO\"");
            sql.AppendLine("    ,KK.\"リース満了日\"");
            sql.AppendLine("    ,KK.\"研実管理廃却申請受理日\"");
            sql.AppendLine("    ,KK.\"廃却見積日\"");
            sql.AppendLine("    ,KK.\"廃却決済承認年月\"");
            sql.AppendLine("    ,KK.\"車両搬出日\"");
            sql.AppendLine("    ,KK.\"廃却見積額\"");
            sql.AppendLine("    ,KK.\"貸与先\"");
            sql.AppendLine("    ,KK.\"貸与返却予定期限\"");
            sql.AppendLine("    ,KK.\"貸与返却日\"");
            sql.AppendLine("    ,KK.\"メモ\"");
            sql.AppendLine("    ,KK.\"正式取得日\"");
            sql.AppendLine("    ,KK.\"棚卸実施日\"");
            //Update Start 2022/02/04 杉浦 月例点検一括省略
            //sql.AppendLine("    ,KK.\"月例点検省略有無\"");
            sql.AppendLine("    ,CASE WHEN NVL(EMI.\"FLAG_月例点検\", 0) = 1 AND KR.\"登録ナンバー\" IS NULL THEN EMI.\"FLAG_月例点検\" ELSE KK.\"月例点検省略有無\" END \"月例点検省略有無\"");
            //Update End 2022/02/04 杉浦 月例点検一括省略
            //Append Start 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            sql.AppendLine("    ,KK.\"衝突試験済\"");
            //Append End 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            //Append Start 2022/03/24 杉浦 月例点検一括省略
            sql.AppendLine("    ,KK.\"月例点検省略有無\" \"月例点検省略有無_BASE\"");
            sql.AppendLine("    ,NVL(EMI.\"FLAG_月例点検\", 0) \"月例点検省略対象部署\"");
            //Append End 2022/03/24 杉浦 月例点検一括省略
            sql.AppendLine("    ,KR.\"履歴NO\"");
            sql.AppendLine("    ,KR.\"管理票発行有無\"");
            sql.AppendLine("    ,KR.\"発行年月日\"");
            sql.AppendLine("    ,KR.\"開発符号\"");
            sql.AppendLine("    ,KR.\"試作時期\"");
            sql.AppendLine("    ,KR.\"号車\"");
            sql.AppendLine("    ,KR.\"仕向地\"");
            sql.AppendLine("    ,KR.\"メーカー名\"");
            sql.AppendLine("    ,KR.\"外製車名\"");
            sql.AppendLine("    ,KR.\"名称備考\"");
            sql.AppendLine("    ,KR.\"車体番号\"");
            sql.AppendLine("    ,KR.\"E_G番号\"");
            sql.AppendLine("    ,KR.\"E_G型式\"");
            sql.AppendLine("    ,KR.\"排気量\"");
            sql.AppendLine("    ,KR.\"トランスミッション\"");
            sql.AppendLine("    ,KR.\"駆動方式\"");
            sql.AppendLine("    ,KR.\"グレード\"");
            sql.AppendLine("    ,KR.\"車体色\"");
            sql.AppendLine("    ,KR.\"試験目的\"");
            sql.AppendLine("    ,KR.\"受領日\"");
            sql.AppendLine("    ,KR.\"受領部署\"");
            if (!isWithSI)
            {
                sql.AppendLine("    ,JD.\"DEPARTMENT_ID\" AS \"受領部署_DEPARTMENT_ID\"");
                sql.AppendLine("    ,JD.\"DEPARTMENT_CODE\" AS \"受領部署_DEPARTMENT_CODE\"");
                sql.AppendLine("    ,JS.\"SECTION_ID\" AS \"受領部署_SECTION_ID\"");
                sql.AppendLine("    ,JS.\"SECTION_CODE\" AS \"受領部署_SECTION_CODE\"");
            }
            else
            {
                sql.AppendLine("    ,JG.\"DEPARTMENT_ID\" AS \"受領部署_DEPARTMENT_ID\"");
                sql.AppendLine("    ,JG.\"DEPARTMENT_CODE\" AS \"受領部署_DEPARTMENT_CODE\"");
                sql.AppendLine("    ,JG.\"SECTION_ID\" AS \"受領部署_SECTION_ID\"");
                sql.AppendLine("    ,JG.\"SECTION_CODE\" AS \"受領部署_SECTION_CODE\"");
            }
            sql.AppendLine("    ,JG.\"SECTION_GROUP_ID\" AS \"受領部署_SECTION_GROUP_ID\"");
            sql.AppendLine("    ,JG.\"SECTION_GROUP_CODE\" AS \"受領部署_SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,KR.\"受領者\"");
            sql.AppendLine("    ,JP.\"NAME\" AS \"受領者_NAME\"");
            sql.AppendLine("    ,KR.\"受領時走行距離\"");
            sql.AppendLine("    ,KR.\"完成日\"");
            sql.AppendLine("    ,KR.\"管理責任部署\"");
            if (!isWithSI)
            {
                sql.AppendLine("    ,DD.\"DEPARTMENT_ID\"");
                sql.AppendLine("    ,DD.\"DEPARTMENT_CODE\"");
                sql.AppendLine("    ,DD.\"ESTABLISHMENT\"");
                sql.AppendLine("    ,SD.\"SECTION_ID\"");
                sql.AppendLine("    ,SD.\"SECTION_CODE\"");
            }
            else
            {
                sql.AppendLine("    ,SG.\"DEPARTMENT_ID\"");
                sql.AppendLine("    ,SG.\"DEPARTMENT_CODE\"");
                sql.AppendLine("    ,SG.\"ESTABLISHMENT\"");
                sql.AppendLine("    ,SG.\"SECTION_ID\"");
                sql.AppendLine("    ,SG.\"SECTION_CODE\"");
            }
            sql.AppendLine("    ,SG.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    ,SG.\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,KR.\"研命ナンバー\"");
            sql.AppendLine("    ,KR.\"研命期間\"");
            sql.AppendLine("    ,KR.\"固定資産NO\"");
            sql.AppendLine("    ,KR.\"登録ナンバー\"");
            sql.AppendLine("    ,KR.\"車検登録日\"");
            sql.AppendLine("    ,KR.\"車検期限\"");
            sql.AppendLine("    ,KR.\"廃艦日\"");
            sql.AppendLine("    ,KR.\"保険NO\"");
            sql.AppendLine("    ,KR.\"保険加入日\"");
            sql.AppendLine("    ,KR.\"保険解約日\"");
            sql.AppendLine("    ,KR.\"保険料\"");
            sql.AppendLine("    ,KR.\"自動車税\"");
            sql.AppendLine("    ,KR.\"移管依頼NO\"");
            sql.AppendLine("    ,KR.\"三鷹移管先部署\"");
            sql.AppendLine("    ,KR.\"三鷹移管年月\"");
            sql.AppendLine("    ,KR.\"三鷹移管先固資NO\"");
            sql.AppendLine("    ,KR.\"試験着手日\"");
            sql.AppendLine("    ,KR.\"試験着手証明文書\"");
            sql.AppendLine("    ,KR.\"工事区分NO\"");
            sql.AppendLine("    ,KR.\"FLAG_中古\"");
            sql.AppendLine("    ,KR.\"FLAG_ナビ付\"");
            sql.AppendLine("    ,KR.\"FLAG_ETC付\"");
            sql.AppendLine("    ,KR.\"EVデバイス\"");
            sql.AppendLine("    ,KR.\"初年度登録年月\"");
            sql.AppendLine("    ,KR.\"資産種別\"");
            sql.AppendLine("    ,KR.\"種別\"");
            sql.AppendLine("    ,KR.\"使用期限\"");
            sql.AppendLine("    ,KR.\"名称\"");
            sql.AppendLine("    ,KS.\"勘定科目\"");
            sql.AppendLine("    ,KS.\"子資産\"");
            sql.AppendLine("    ,KS.\"所得年月\"");
            sql.AppendLine("    ,KS.\"設置場所\"");
            sql.AppendLine("    ,KS.\"耐用年数\"");
            sql.AppendLine("    ,KS.\"取得価額\"");
            sql.AppendLine("    ,KS.\"減価償却累計額\"");
            sql.AppendLine("    ,KS.\"期末簿価\"");
            sql.AppendLine("    ,KS.\"資産タイプ\"");
            sql.AppendLine("    ,KS.\"固定資産税\"");
            sql.AppendLine("    ,KS.\"原価部門\"");
            sql.AppendLine("    ,KS.\"管理部署\"");
            sql.AppendLine("    ,KS.\"資産計上部署\"");
            sql.AppendLine("    ,KS.\"事業所コード\"");
            sql.AppendLine("    ,KS.\"処分コード\"");
            sql.AppendLine("    ,KS.\"処分予定年月\"");
            sql.AppendLine("    ,KS.\"処分数\"");
            sql.AppendLine("    ,KS.\"処分区分\"");
            sql.AppendLine("    ,KS.\"除却年度\"");
            sql.AppendLine("    ,KS.\"除却明細名称\"");
            sql.AppendLine("    ,KH.\"車名\"");
            sql.AppendLine("    ,SK.\"車区分\"");
            sql.AppendLine("    ,SK.\"車検区分1\"");
            sql.AppendLine("    ,SK.\"車検区分2\"");
            sql.AppendLine("    ,KI.\"開本内移管履歴NO\"");
            sql.AppendLine("    ,KI.\"開本内移管日\"");
            sql.AppendLine("    ,KI.\"内容\"");
            sql.AppendLine("    ,CASE ");
            sql.AppendLine("      WHEN X.物品コード IS NOT NULL THEN 'あり'");
            sql.AppendLine("      ELSE 'なし'");
            sql.AppendLine("     END AS XEYE_EXIST ");
            sql.AppendLine("    ,CASE WHEN KK.\"研実管理廃却申請受理日\" IS NULL AND KK.\"車両搬出日\" IS NULL AND KS.\"処分コード\" IS NULL AND KR.\"使用期限\" IS NOT NULL AND KR.\"種別\" != 'リース' AND LAST_DAY(ADD_MONTHS(KR.\"使用期限\",-3)) + 20 <= SYSDATE THEN '廃却要' ELSE '' END 廃却勧告");

            sql.AppendLine("    ,KR.\"自動車ﾘｻｲｸﾙ法\"");
            sql.AppendLine("    ,KR.\"A_C冷媒種類\"");
            //追加で取得する列
            addColumn?.Invoke(sql);

            sql.AppendLine("FROM");
            sql.AppendLine("    \"試験車基本情報\" KK");
            sql.AppendLine("    INNER JOIN");
            sql.AppendLine("                (--KR");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         A.\"データID\"");
            sql.AppendLine("                        ,A.\"履歴NO\"");
            sql.AppendLine("                        ,A.\"管理票発行有無\"");
            sql.AppendLine("                        ,A.\"発行年月日\"");
            sql.AppendLine("                        ,A.\"開発符号\"");
            sql.AppendLine("                        ,A.\"試作時期\"");
            sql.AppendLine("                        ,A.\"号車\"");
            sql.AppendLine("                        ,A.\"仕向地\"");
            sql.AppendLine("                        ,A.\"メーカー名\"");
            sql.AppendLine("                        ,A.\"外製車名\"");
            sql.AppendLine("                        ,A.\"名称備考\"");
            sql.AppendLine("                        ,A.\"車体番号\"");
            sql.AppendLine("                        ,A.\"E_G番号\"");
            sql.AppendLine("                        ,A.\"E_G型式\"");
            sql.AppendLine("                        ,A.\"排気量\"");
            sql.AppendLine("                        ,A.\"トランスミッション\"");
            sql.AppendLine("                        ,A.\"駆動方式\"");
            sql.AppendLine("                        ,A.\"グレード\"");
            sql.AppendLine("                        ,A.\"車体色\"");
            sql.AppendLine("                        ,A.\"試験目的\"");
            sql.AppendLine("                        ,A.\"受領日\"");
            sql.AppendLine("                        ,A.\"受領部署\"");
            sql.AppendLine("                        ,A.\"受領者\"");
            sql.AppendLine("                        ,A.\"受領時走行距離\"");
            sql.AppendLine("                        ,A.\"完成日\"");
            sql.AppendLine("                        ,A.\"管理責任部署\"");
            sql.AppendLine("                        ,A.\"研命ナンバー\"");
            sql.AppendLine("                        ,A.\"研命期間\"");
            sql.AppendLine("                        ,A.\"固定資産NO\"");
            sql.AppendLine("                        ,A.\"登録ナンバー\"");
            sql.AppendLine("                        ,A.\"車検登録日\"");
            sql.AppendLine("                        ,A.\"車検期限\"");
            sql.AppendLine("                        ,A.\"廃艦日\"");
            sql.AppendLine("                        ,A.\"保険NO\"");
            sql.AppendLine("                        ,A.\"保険加入日\"");
            sql.AppendLine("                        ,A.\"保険解約日\"");
            sql.AppendLine("                        ,A.\"保険料\"");
            sql.AppendLine("                        ,A.\"自動車税\"");
            sql.AppendLine("                        ,A.\"移管依頼NO\"");
            sql.AppendLine("                        ,A.\"三鷹移管先部署\"");
            sql.AppendLine("                        ,A.\"三鷹移管年月\"");
            sql.AppendLine("                        ,A.\"三鷹移管先固資NO\"");
            sql.AppendLine("                        ,A.\"試験着手日\"");
            sql.AppendLine("                        ,A.\"試験着手証明文書\"");
            sql.AppendLine("                        ,A.\"工事区分NO\"");
            sql.AppendLine("                        ,A.\"FLAG_中古\"");
            sql.AppendLine("                        ,A.\"FLAG_ナビ付\"");
            sql.AppendLine("                        ,A.\"FLAG_ETC付\"");
            sql.AppendLine("                        ,A.\"EVデバイス\"");
            sql.AppendLine("                        ,A.\"初年度登録年月\"");
            sql.AppendLine("                        ,CAST((");
            sql.AppendLine("                            CASE");
            sql.AppendLine("                                WHEN A.\"登録ナンバー\" IS NULL AND A.\"固定資産NO\" IS NULL AND A.\"三鷹移管先固資NO\" IS NULL THEN 3");
            sql.AppendLine("                                WHEN A.\"登録ナンバー\" IS NULL AND A.\"固定資産NO\" IS NULL AND A.\"三鷹移管先固資NO\" IS NOT NULL THEN 2");
            sql.AppendLine("                                WHEN A.\"登録ナンバー\" IS NULL AND A.\"固定資産NO\" IS NOT NULL THEN 2");
            sql.AppendLine("                                WHEN A.\"登録ナンバー\" IS NOT NULL AND A.\"固定資産NO\" IS NULL AND A.\"三鷹移管先固資NO\" IS NULL THEN 4");
            sql.AppendLine("                                WHEN A.\"登録ナンバー\" IS NOT NULL AND A.\"固定資産NO\" IS NULL AND A.\"三鷹移管先固資NO\" IS NOT NULL THEN 1");
            sql.AppendLine("                                WHEN A.\"登録ナンバー\" IS NOT NULL AND A.\"固定資産NO\" IS NOT NULL THEN 1");
            sql.AppendLine("                            END");
            sql.AppendLine("                        ) AS NUMBER(1)) AS \"資産種別\"");
            sql.AppendLine("                        ,(");
            sql.AppendLine("                            CASE");
            sql.AppendLine("                                WHEN A.\"固定資産NO\" IS NOT NULL THEN '固定資産'");
            sql.AppendLine("                                WHEN A.\"登録ナンバー\" IS NOT NULL THEN 'リース'");
            sql.AppendLine("                                ELSE '資産外'");
            sql.AppendLine("                            END");
            sql.AppendLine("                        ) AS \"種別\"");
            sql.AppendLine("                        ,(");
            sql.AppendLine("                            CASE");
            sql.AppendLine("                                WHEN A.\"固定資産NO\" IS NOT NULL THEN C.\"処分予定年月\"");
            sql.AppendLine("                                WHEN A.\"登録ナンバー\" IS NOT NULL THEN B.\"リース満了日\"");
            sql.AppendLine("                                ELSE C.\"処分予定年月\"");
            sql.AppendLine("                            END");
            sql.AppendLine("                        ) AS \"使用期限\"");
            sql.AppendLine("                        ,(");
            sql.AppendLine("                            CASE");
            sql.AppendLine("                                WHEN A.\"開発符号\" IS NULL THEN A.\"メーカー名\" || ' ' || A.\"外製車名\"");
            sql.AppendLine("                                ELSE A.\"開発符号\" || ' ' || A.\"試作時期\" || ' ' || A.\"号車\"");
            sql.AppendLine("                            END");
            sql.AppendLine("                        ) AS 名称");
            sql.AppendLine("                        ,C.\"処分コード\"");
            //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
            sql.AppendLine("                        ,A.\"自動車ﾘｻｲｸﾙ法\"");
            sql.AppendLine("                        ,A.\"A_C冷媒種類\"");
            //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"試験車履歴情報\" A");
            sql.AppendLine("                        INNER JOIN \"試験車基本情報\" B");
            sql.AppendLine("                        ON A.\"データID\" = B.\"データID\"");
            sql.AppendLine("                        INNER JOIN \"固定資産情報\" C");
            sql.AppendLine("                        ON A.\"データID\" = C.\"データID\"");

            //最新の履歴を取得するかどうか
            if (isHistoryMax == true)
            {
                sql.AppendLine("                        INNER JOIN");
                sql.AppendLine("                                    (");
                sql.AppendLine("                                        SELECT");
                sql.AppendLine("                                             A.\"データID\"");
                sql.AppendLine("                                            ,MAX(A.\"履歴NO\") AS \"履歴NO\"");
                sql.AppendLine("                                        FROM");
                sql.AppendLine("                                            \"試験車履歴情報\" A");
                sql.AppendLine("                                        WHERE 0 = 0");

                //試験車履歴情報の取得条件があるなら設定
                if (testCarHistoryWhere != null && testCarHistoryWhere.Any() == true)
                {
                    testCarHistoryWhere.ForEach(x => sql.AppendFormat("                                            AND A.\"{0}\" = :{0}", x.Name.Replace(":", "")).AppendLine());

                }

                sql.AppendLine("                                        GROUP BY");
                sql.AppendLine("                                            A.\"データID\"");
                sql.AppendLine("                                    ) C");
                sql.AppendLine("                        ON A.\"データID\" = C.\"データID\"");
                sql.AppendLine("                        AND A.\"履歴NO\" = C.\"履歴NO\"");

            }

            sql.AppendLine("                ) KR");
            sql.AppendLine("    ON KK.\"データID\" = KR.\"データID\"");
            sql.AppendLine("    INNER JOIN \"固定資産情報\" KS");
            sql.AppendLine("    ON KK.\"データID\" = KS.\"データID\"");
            sql.AppendLine("    LEFT JOIN \"開発符号情報\" KH");
            sql.AppendLine("    ON KR.\"開発符号\" = KH.\"開発符号\"");
            sql.AppendLine("    LEFT JOIN \"車系情報\" SK");
            sql.AppendLine("    ON KK.\"車系\" = SK.\"車系\"");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("                (--KI");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         A.\"データID\"");
            sql.AppendLine("                        ,A.\"開本内移管履歴NO\"");
            sql.AppendLine("                        ,A.\"開本内移管日\"");
            sql.AppendLine("                        ,A.\"内容\"");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"開本内移管履歴情報\" A");
            sql.AppendLine("                        INNER JOIN");
            sql.AppendLine("                                    (");
            sql.AppendLine("                                        SELECT");
            sql.AppendLine("                                             A.\"データID\"");
            sql.AppendLine("                                            ,MAX(A.\"開本内移管履歴NO\") AS \"開本内移管履歴NO\"");
            sql.AppendLine("                                        FROM");
            sql.AppendLine("                                            開本内移管履歴情報 A");
            sql.AppendLine("                                        WHERE 0 = 0");
            sql.AppendLine("                                        GROUP BY");
            sql.AppendLine("                                            A.\"データID\"");
            sql.AppendLine("                                    ) B");
            sql.AppendLine("                        ON A.\"データID\" = B.\"データID\"");
            sql.AppendLine("                        AND A.\"開本内移管履歴NO\" = B.\"開本内移管履歴NO\"");
            sql.AppendLine("                ) KI");
            sql.AppendLine("    ON KK.\"データID\" = KI.\"データID\"");
            if (!isWithSI)
            {
                sql.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" SG");
                sql.AppendLine("    ON KR.\"管理責任部署\" = SG.\"SECTION_GROUP_ID\"");
                sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" SD");
                sql.AppendLine("    ON SG.\"SECTION_ID\" = SD.\"SECTION_ID\"");
                sql.AppendLine("    LEFT JOIN \"DEPARTMENT_DATA\" DD");
                sql.AppendLine("    ON SD.\"DEPARTMENT_ID\" = DD.\"DEPARTMENT_ID\"");
                sql.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" JG");
                sql.AppendLine("    ON KR.\"受領部署\" = JG.\"SECTION_GROUP_ID\"");
                sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" JS");
                sql.AppendLine("    ON JG.\"SECTION_ID\" = JS.\"SECTION_ID\"");
                sql.AppendLine("    LEFT JOIN \"DEPARTMENT_DATA\" JD");
                sql.AppendLine("    ON JS.\"DEPARTMENT_ID\" = JD.\"DEPARTMENT_ID\"");
                sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" JP");
                sql.AppendLine("    ON KR.\"受領者\" = JP.\"PERSONEL_ID\"");
            }
            else
            {
                sql.AppendLine("    LEFT JOIN SI SG");
                sql.AppendLine("    ON KR.\"管理責任部署\" = SG.\"SECTION_GROUP_ID\"");
                sql.AppendLine("    LEFT JOIN SI JG");
                sql.AppendLine("    ON KR.\"受領部署\" = JG.\"SECTION_GROUP_ID\"");
                sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" JP");
                sql.AppendLine("    ON KR.\"受領者\" = JP.\"PERSONEL_ID\"");
            }
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_TO_XEYE\" X");
            sql.AppendLine("    ON X.\"物品名2\" = KK.\"管理票NO\"");

            //Append Start 2022/02/04 杉浦 月例点検一括省略
            sql.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" SG2");
            sql.AppendLine("    ON KR.\"管理責任部署\" = SG2.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" SD2");
            sql.AppendLine("    ON SG2.\"SECTION_ID\" = SD2.\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"EXCEPT_MONTHLY_INSPECTION\" EMI");
            sql.AppendLine("    ON SD2.\"SECTION_ID\" = EMI.\"SECTION_GROUP_ID\"");
            //Append End 2022/02/04 杉浦 月例点検一括省略

            //追加で取得するテーブル
            addTable?.Invoke(sql);

            sql.AppendLine("WHERE 0 = 0");

            return sql;

        }
        #endregion

        #region 文字列の抽出条件を設定
        /// <summary>
        /// 文字列の抽出条件を設定
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parm">パラメータ</param>
        /// <param name="tableName">テーブル名</param>
        /// <param name="columnName">列名</param>
        /// <param name="value">値</param>
        protected void SetStringWhere(StringBuilder sql, List<BindModel> parm, string tableName, string columnName, string value)
        {
            //値が無ければ終了
            if (string.IsNullOrWhiteSpace(value) == true)
            {
                return;

            }

            //未設定の列名なら初期化
            if (this.paramNameMap.ContainsKey(columnName) == false)
            {
                this.paramNameMap[columnName] = 0;

            }
            
            var j = 0;

            sql.AppendLine("    AND");
            sql.AppendLine("        (");

            foreach (var s in value.Split(new[] { Comma }, StringSplitOptions.None))
            {
                //初回かどうか
                if (j++ != 0)
                {
                    sql.AppendLine("            OR");

                }

                var sb = new StringBuilder();

                //テーブル名があるかどうか
                if (string.IsNullOrEmpty(tableName) == false)
                {
                    sb.AppendFormat("\"{0}\".", tableName);

                }

                //列名
                sb.AppendFormat("\"{0}\"", columnName);

                //変換関数セット
                this.SetConvert(ref sb);
                sb.Insert(0, "            ");

                //完全一致させるかどうか
                var p = s;
                if (p.Contains(Asterisk) == false)
                {
                    sb.Append(" = ");

                }
                else
                {
                    p = p.Replace(Asterisk, Like);

                    sb.Append(" LIKE ");

                }

                //パラメータ
                var name = string.Format(":{0}{1}", columnName, ++this.paramNameMap[columnName]);
                sb.AppendFormat(ConvertFunc, name).AppendLine();

                //パラメータ追加
                parm.Add(new BindModel { Name = name, Object = p, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });

                //SQLに条件を追加
                sql.Append(sb);

            }

            sql.AppendLine("        )");

        }

        /// <summary>
        /// 文字列の変換関数をセット
        /// </summary>
        /// <param name="sql">SQL</param>
        protected void SetConvert(ref StringBuilder sql)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(ConvertFunc, sql);

            sql = sb;

        }
        #endregion

        #region 更新
        /// <summary>
        /// 試験車基本情報の更新
        /// </summary>
        /// <param name="list">試験車</param>
        /// <param name="func">更新対象取得</param>
        /// <returns></returns>
        protected bool UpdateTestCarBaseInfo(IEnumerable<TestCarCommonModel> list, Func<TestCarCommonModel, IEnumerable<BindModel>> func)
        {
            var results = new List<bool>();

            //対象がない場合は終了
            if (list == null || list.Any() == false)
            {
                return true;

            }

            foreach (var testCar in list)
            {
                //更新
                results.Add(base.Update("試験車基本情報", func(testCar), new[]
                {
                    new BindModel { Name = ":データID", Object = testCar.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input }

                }));

            }

            //登録が成功したかどうか
            return results.All(x => x == true);

        }

        /// <summary>
        /// 試験車履歴情報の更新
        /// </summary>
        /// <param name="list">試験車</param>
        /// <param name="func">更新対象取得</param>
        /// <returns></returns>
        protected bool UpdateTestCarHistoryInfo(IEnumerable<TestCarCommonModel> list, Func<TestCarCommonModel, IEnumerable<BindModel>> func)
        {
            var results = new List<bool>();

            //対象がない場合は終了
            if (list == null || list.Any() == false)
            {
                return true;

            }

            foreach (var testCar in list)
            {
                //更新
                results.Add(base.Update("試験車履歴情報", func(testCar), new[]
                {
                    new BindModel { Name = ":データID", Object = testCar.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":履歴NO", Object = testCar.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input }

                }));

            }

            //登録が成功したかどうか
            return results.All(x => x == true);

        }

        /// <summary>
        /// 固定資産情報の更新
        /// </summary>
        /// <param name="list">試験車</param>
        /// <param name="func">更新対象取得</param>
        /// <returns></returns>
        protected bool UpdateFixedAssetInfo(IEnumerable<TestCarCommonModel> list, Func<TestCarCommonModel, IEnumerable<BindModel>> func)
        {
            var results = new List<bool>();

            //対象がない場合は終了
            if (list == null || list.Any() == false)
            {
                return true;

            }

            foreach (var testCar in list)
            {
                //更新
                results.Add(base.Update("固定資産情報", func(testCar), new[]
                {
                    new BindModel { Name = ":データID", Object = testCar.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input }

                }));

            }

            //登録が成功したかどうか
            return results.All(x => x == true);

        }

        /// <summary>
        /// 使用履歴情報の更新
        /// </summary>
        /// <param name="list">試験車履歴</param>
        /// <param name="func">更新対象取得</param>
        /// <param name="isSyounin">承認フラグ</param>
        /// <returns></returns>
        protected bool UpdateUseHistoryInfo(IEnumerable<TestCarUseHistoryModel> list, Func<TestCarUseHistoryModel, IEnumerable<BindModel>> func, bool isSyounin = false)
        {
            var results = new List<bool>();

            var now = DateTime.Now;

            //対象がない場合は終了
            if (list == null || list.Any() == false)
            {
                return true;

            }

            foreach (var history in list)
            {
                //編集日と編集者を設定
                var update = func(history).ToList();
                if (!isSyounin)
                {
                    update.Add(new BindModel { Name = ":編集日", Object = now, Type = OracleDbType.Date, Direct = ParameterDirection.Input });
                    update.Add(new BindModel { Name = ":編集者", Type = OracleDbType.Varchar2, Object = history.編集者, Direct = ParameterDirection.Input });
                }

                //更新
                results.Add(base.Update("使用履歴情報", update, new[]
                {
                    new BindModel { Name = ":データID", Object = history.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":履歴NO", Object = history.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SEQNO", Object = history.SEQNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input }

                }));

            }

            //登録が成功したかどうか
            return results.All(x => x == true);

        }
        #endregion
    }
}