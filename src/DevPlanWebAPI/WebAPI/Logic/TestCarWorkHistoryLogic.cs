using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 作業履歴(試験車)ビジネスロジック
    /// </summary>
    public class TestCarWorkHistoryLogic : BaseLogic
    {
        #region メンバ変数
        private WorkHistoryLogic workHistory = null;
        #endregion

        #region 作業履歴の取得
        /// <summary>
        /// 作業履歴(試験車)取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<WorkHistoryModel> GetTestCarWorkHistory(TestCarWorkHistorySearchModel cond)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            // SQL
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.ID");
            sql.AppendLine("    ,A.CATEGORY_ID");
            sql.AppendLine("    ,F.CATEGORY");
            sql.AppendLine("    ,A.CURRENT_SITUATION");
            sql.AppendLine("    ,REPLACE(REPLACE(SUBSTR(REGEXP_SUBSTR(A.CURRENT_SITUATION, '試験内容(:|：).*', 1, 1), 6, 50), CHR(13), ''), CHR(10), '') 試験内容");
            sql.AppendLine("    ,REPLACE(REPLACE(SUBSTR(REGEXP_SUBSTR(A.CURRENT_SITUATION, 'ODO(:|：).*', 1, 1, 'i'), 5, 25), CHR(13), ''), CHR(10), '') 実走行距離");
            sql.AppendLine("    ,A.FUTURE_SCHEDULE");
            sql.AppendLine("    ,A.INPUT_PERSONEL_ID");
            sql.AppendLine("    ,E.SECTION_CODE AS INPUT_SECTION_CODE");
            sql.AppendLine("    ,C.NAME AS INPUT_NAME");
            sql.AppendLine("    ,A.INPUT_DATETIME");
            sql.AppendLine("    ,A.OPEN_CLOSE");
            sql.AppendLine("    ,TRUNC(A.LISTED_DATE) AS LISTED_DATE");
            sql.AppendLine("    ,A.SORT_NO");
            sql.AppendLine("    ,A.IMPORTANT_ITEM");
            sql.AppendLine("    ,A.MARK");
            sql.AppendLine("    ,A.INPUT_LOGIN_ID");
            sql.AppendLine("    ,A.種別_ID");
            sql.AppendLine("    ,B.種別");
            sql.AppendLine("    ,A.SCHEDULE_ID");
            sql.AppendLine("    ,I.データID");
            sql.AppendLine("    ,I.履歴NO");
            sql.AppendLine("    ,H.管理票NO");
            //Update Start 2022/02/04 杉浦 月例点検一括省略
            //sql.AppendLine("    ,CASE WHEN H.月例点検省略有無 = 1 THEN '不要' ELSE '要' END 月例点検省略有無");
            sql.AppendLine("    ,CASE WHEN NVL(EMI.FLAG_月例点検, 0) = 1 AND M.登録ナンバー IS NULL THEN '不要' ELSE CASE WHEN H.月例点検省略有無 = 1 THEN '不要' ELSE '要' END END 月例点検省略有無");
            //Update End 2022/02/04 杉浦 月例点検一括省略
            sql.AppendLine("    ,K.CAR_GROUP");
            sql.AppendLine("    ,K.GENERAL_CODE");

            //Append Start 2021/07/12 矢作

            sql.AppendLine("    ,L.SEQNO AS 最新履歴_SEQNO");
            sql.AppendLine("    ,L.承認状況 AS 最新履歴_承認状況");
            sql.AppendLine("    ,L.処理日 AS 最新履歴_日付");
            sql.AppendLine("    ,L.試験内容 AS 最新履歴_試験内容");
            sql.AppendLine("    ,L.実走行距離 AS 最新履歴_実走行距離");

            //Append End 2021/07/12 矢作

            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_課題フォローリスト A");
            sql.AppendLine("    LEFT JOIN 試験計画_試験車履歴_種別 B");
            sql.AppendLine("    ON A.種別_ID = B.ID");
            sql.AppendLine("    LEFT JOIN PERSONEL_LIST C");
            sql.AppendLine("    ON A.INPUT_PERSONEL_ID = C.PERSONEL_ID");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA D");
            sql.AppendLine("    ON C.SECTION_GROUP_ID = D.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN SECTION_DATA E");
            sql.AppendLine("    ON D.SECTION_ID = E.SECTION_ID");
            sql.AppendLine("    INNER JOIN TESTCAR_SCHEDULE_ITEM F");
            sql.AppendLine("    ON A.CATEGORY_ID = F.CATEGORY_ID");
            sql.AppendLine("    LEFT JOIN SCHEDULE_CAR G");
            sql.AppendLine("    ON F.CATEGORY_ID = G.CATEGORY_ID");
            sql.AppendLine("    LEFT JOIN VIEW_試験車基本情報 H");
            sql.AppendLine("    ON G.管理票番号 = H.管理票NO");
            sql.AppendLine("    LEFT JOIN (SELECT データID, MAX(履歴NO) AS 履歴NO FROM 試験車履歴情報 GROUP BY データID) I");
            sql.AppendLine("    ON H.データID = I.データID");
            sql.AppendLine("    LEFT JOIN (SELECT 使用履歴情報.* FROM 使用履歴情報, (SELECT データID, MAX(SEQNO) SEQNO FROM 使用履歴情報 GROUP BY データID) 最大履歴情報 WHERE 使用履歴情報.データID = 最大履歴情報.データID AND 使用履歴情報.SEQNO = 最大履歴情報.SEQNO) J");
            sql.AppendLine("    ON I.データID = J.データID");
            sql.AppendLine("    AND I.履歴NO = J.履歴NO");
            sql.AppendLine("    LEFT JOIN GENERAL_CODE K");
            sql.AppendLine("    ON F.GENERAL_CODE = K.GENERAL_CODE");

            //Append Start 2021/07/12 矢作

            sql.AppendLine("    LEFT JOIN (");
            sql.AppendLine("		SELECT");
            sql.AppendLine("			L1.データID,");
            sql.AppendLine("			L1.履歴NO,");
            sql.AppendLine("			L1.SEQNO,");
            sql.AppendLine("			L1.承認状況,");
            sql.AppendLine("			L1.処理日,");
            sql.AppendLine("			L1.試験内容,");
            sql.AppendLine("			L1.実走行距離");
            sql.AppendLine("		FROM");
            sql.AppendLine("		使用履歴情報 L1");
            sql.AppendLine("			LEFT JOIN(");
            sql.AppendLine("				SELECT");
            sql.AppendLine("					 データID");
            sql.AppendLine("					,MAX(SEQNO) AS SEQMAX");
            sql.AppendLine("				FROM 使用履歴情報");
            sql.AppendLine("				GROUP BY データID");
            sql.AppendLine("				) L2");
            sql.AppendLine("			ON L1.SEQNO = L2.SEQMAX");
            sql.AppendLine("		WHERE L1.データID = L2.データID");
            sql.AppendLine("	) L");
            sql.AppendLine("    ON L.データID = I.データID");
            //Append Start 2022/02/04 杉浦 月例点検一括省略
            sql.AppendLine("	LEFT JOIN 試験車履歴情報 M");
            sql.AppendLine("	ON H.データID = M.データID");
            sql.AppendLine("	AND I.履歴NO = M.履歴NO");
            sql.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" SG2");
            sql.AppendLine("    ON M.\"管理責任部署\" = SG2.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" SD2");
            sql.AppendLine("    ON SG2.\"SECTION_ID\" = SD2.\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN EXCEPT_MONTHLY_INSPECTION EMI");
            sql.AppendLine("    ON SD2.\"SECTION_ID\" = EMI.SECTION_GROUP_ID");
            //Append End 2022/02/04 杉浦 月例点検一括省略

            //Append End 2021/07/12 矢作

            sql.AppendLine("WHERE 0 = 0");

            // 開発符号
            if (cond.GENERAL_CODE != null && cond.GENERAL_CODE.Count() > 0)
            {
                sql.AppendLine("    AND (0 = 1");

                for (var i = 0; i < cond.GENERAL_CODE?.Count(); i++)
                {
                    sql.AppendLine(string.Format("        OR F.GENERAL_CODE = :GENERAL_CODE{0}", i));

                    prms.Add(new BindModel { Name = string.Format(":GENERAL_CODE{0}", i), Type = OracleDbType.Char, Object = cond.GENERAL_CODE[i], Direct = ParameterDirection.Input });
                }

                sql.AppendLine("    )");
            }

            // 要月例点検フラグ
            if (cond.NECESSARY_INSPECTION_FLAG)
            {
                sql.AppendLine("    AND H.研実管理廃却申請受理日 IS NULL");
                sql.AppendLine("    AND I.データID IS NOT NULL");
                sql.AppendLine("    AND I.履歴NO IS NOT NULL");
                sql.AppendLine("    AND J.処理日 < TRUNC(LAST_DAY(ADD_MONTHS(SYSDATE,-1))+1)");
                sql.AppendLine("    AND J.STEPNO = 0");
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     F.GENERAL_CODE ASC");
            sql.AppendLine("    ,A.CATEGORY_ID DESC");
            sql.AppendLine("    ,A.LISTED_DATE DESC");
            sql.AppendLine("    ,A.SORT_NO");
            sql.AppendLine("    ,A.ID");

            return db.ReadModelList<WorkHistoryModel>(sql.ToString(), prms);
        }
        #endregion

        #region 作業履歴の更新
        /// <summary>
        /// 作業履歴の更新
        /// </summary>
        /// <param name="list">作業履歴</param>
        /// <returns></returns>
        public bool UpdateWorkHistory(List<WorkHistoryModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //作業履歴の登録
                results.Add(this.GetWorkHistoryLogic().MergeWorkHistory(list));

                //DEVELOPMENT_SCHEDULEを更新
                results.Add(this.UpdateScheduleItem(list));

            }

            //登録が成功したかどうか
            var flg = results.All(x => x == true);
            if (flg == true)
            {
                //コミット
                db.Commit();

            }
            else
            {
                //ロールバック
                db.Rollback();

            }

            return flg;

        }

        /// <summary>
        /// DEVELOPMENT_SCHEDULEを更新
        /// </summary>
        /// <param name="list">作業履歴</param>
        /// <returns></returns>
        private bool UpdateScheduleItem(List<WorkHistoryModel> list)
        {
            var now = (DateTime?)DateTime.Now;

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"TESTCAR_SCHEDULE_ITEM\"");
            sql.AppendLine("SET");
            sql.AppendLine("     \"INPUT_PERSONEL_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,\"INPUT_LOGIN_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,\"CHANGE_DATETIME\" = SYSTIMESTAMP");
            sql.AppendLine("    ,\"CLOSED_DATE\" = :CLOSED_DATE");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :ID");

            var results = new List<bool>();

            //IDごとに更新
            foreach (var id in list.Where(x => x.CATEGORY_ID != null).Select(x => x.CATEGORY_ID.Value).Distinct())
            {
                var workHistory = list.First(x => x.CATEGORY_ID == id);

                var closedDate = workHistory.OPEN_CLOSE == Const.Close ? now : null;

                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = workHistory.CATEGORY_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = workHistory.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CLOSED_DATE", Type = OracleDbType.Date, Object = closedDate, Direct = ParameterDirection.Input }
                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 業務ロジックの取得
        /// <summary>
        /// 作業履歴ロジックの取得
        /// </summary>
        /// <returns></returns>
        private WorkHistoryLogic GetWorkHistoryLogic()
        {
            //未取得なら取得
            if (this.workHistory == null)
            {
                this.workHistory = new WorkHistoryLogic();
                workHistory.SetDBAccess(base.db);

            }

            return this.workHistory;

        }
        #endregion

    }
}