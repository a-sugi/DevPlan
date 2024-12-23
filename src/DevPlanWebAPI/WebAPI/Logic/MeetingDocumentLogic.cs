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
    /// 検討会資料業務ロジッククラス
    /// </summary>
    public class MeetingDocumentLogic : BaseLogic
    {
        #region 検討会資料取得
        /// <summary>
        /// 検討会資料取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<MeetingDocumentModel> GetMeetingDocument(MeetingDocumentSearchModel cond)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    *");
            sql.AppendLine("FROM");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             A.\"ID\"");
            sql.AppendLine("            ,A.\"MEETING_FLAG\"");
            sql.AppendLine("            ,DECODE(A.\"MEETING_FLAG\",0,'検討会',1,'定例会') AS \"MEETING_NAME\"");
            sql.AppendLine("            ,A.\"MONTH\"");
            sql.AppendLine("            ,A.\"NAME\""); 
            sql.AppendLine("            ,A.\"EDIT_TERM_START\"");
            sql.AppendLine("            ,A.\"EDIT_TERM_END\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"MEETING_DOCUMENT_NAME\" A");
            sql.AppendLine("        ORDER BY");
            sql.AppendLine("             A.\"MONTH\" DESC");
            sql.AppendLine("            ,A.\"MEETING_FLAG\"");
            sql.AppendLine("            ,A.\"ID\" DESC");
            sql.AppendLine("    ) A");
            sql.AppendLine("WHERE 0 = 0");

            //IDが指定されているかどうか
            if (cond.ID != null && cond.ID.Any() == true)
            {
                sql.Append("    AND A.\"ID\" IN (NULL");

                for (var i = 0; i < cond.ID.Count(); i++)
                {
                    var name = string.Format(":ID{0}", i);

                    sql.AppendFormat(",{0}", name);

                    prms.Add(new BindModel { Name = name, Type = OracleDbType.Int32, Object = cond.ID.ElementAt(i), Direct = ParameterDirection.Input });

                }

                sql.Append(")");
                sql.AppendLine();

            }
            else
            {
                //行数
                if (cond.ROW_COUNT != null)
                {
                    sql.AppendLine("    AND ROWNUM <= :ROW_COUNT");

                    prms.Add(new BindModel { Name = ":ROW_COUNT", Type = OracleDbType.Int32, Object = cond.ROW_COUNT, Direct = ParameterDirection.Input });

                }

            }

            //取得
            return db.ReadModelList<MeetingDocumentModel>(sql.ToString(), prms);

        }

        #endregion

        #region 検討会資料追加
        /// <summary>
        /// 検討会資料追加
        /// </summary>
        /// <param name="list">検討会資料</param>
        /// <returns></returns>
        public bool InsertMeetingDocument(List<MeetingDocumentModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //MEETING_DOCUMENT_NAMEを追加
                results.Add(this.InsertMeetingDocumentName(list));

                //試験計画_資料名をコピー
                results.Add(this.CopySiryou(list));

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
        /// MEETING_DOCUMENT_NAMEを追加
        /// </summary>
        /// <param name="list">検討会資料</param>
        /// <returns></returns>
        private bool InsertMeetingDocumentName(List<MeetingDocumentModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO \"MEETING_DOCUMENT_NAME\"");
            sql.AppendLine("(");
            sql.AppendLine("     \"ID\"");
            sql.AppendLine("    ,\"MEETING_FLAG\"");
            sql.AppendLine("    ,\"MONTH\"");
            sql.AppendLine("    ,\"NAME\"");
            sql.AppendLine("    ,\"EDIT_TERM_START\"");
            sql.AppendLine("    ,\"EDIT_TERM_END\"");
            sql.AppendLine("    ,\"INPUT_DATETIME\"");
            sql.AppendLine("    ,\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,\"CHANGE_DATETIME\"");
            sql.AppendLine("    ,\"CHANGE_PERSONEL_ID\"");
            sql.AppendLine(") ");
            sql.AppendLine("VALUES");
            sql.AppendLine("( ");
            sql.AppendLine("     :ID");
            sql.AppendLine("    ,:MEETING_FLAG");
            sql.AppendLine("    ,:MONTH");
            sql.AppendLine("    ,:NAME");            
            sql.AppendLine("    ,:EDIT_TERM_START");
            sql.AppendLine("    ,:EDIT_TERM_END");
            sql.AppendLine("    ,SYSTIMESTAMP");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID");
            sql.AppendLine("    ,SYSTIMESTAMP");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID");
            sql.AppendLine(")");

            var results = new List<bool>();

            foreach (var siryou in list)
            {
                //MEETING_DOCUMENT_NAMEのID
                siryou.ID = this.GetMeetingDocumentNameNewID();

                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = siryou.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MEETING_FLAG", Type = OracleDbType.Int16, Object = siryou.MEETING_FLAG, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MONTH", Type = OracleDbType.Date, Object = siryou.MONTH, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":NAME", Type = OracleDbType.Varchar2, Object = siryou.NAME, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":EDIT_TERM_START", Type = OracleDbType.Date, Object = siryou.EDIT_TERM_START, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":EDIT_TERM_END", Type = OracleDbType.Date, Object = siryou.EDIT_TERM_END, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = siryou.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input }

                };

                //追加
                results.Add(db.InsertData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }

        /// <summary>
        /// MEETING_DOCUMENT_NAMEのIDを取得
        /// </summary>
        /// <returns></returns>
        private int GetMeetingDocumentNameNewID()
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    CAST((NVL(MAX(A.\"ID\"),0) + 1) AS NUMBER(5)) AS \"ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"MEETING_DOCUMENT_NAME\" A");

            //取得
            return db.ReadModelList<MeetingDocumentModel>(sql.ToString(), null).First().ID;

        }

        /// <summary>
        /// 試験計画_資料をコピー
        /// </summary>
        /// <param name="list">検討会資料</param>
        /// <returns></returns>
        private bool CopySiryou(List<MeetingDocumentModel> list)
        {
            var now = DateTime.Now;

            var copyList = list.Where(x => x.資料_ID != null).ToList();

            //コピー対象が無ければ終了
            if (copyList.Any() == false)
            {
                return true;

            }

            var results = new List<bool>();

            list.ForEach(siryouName =>
            {
                //コピー元を取得
                var siryouList = this.GetMeetingDocumentDetail(new MeetingDocumentDetailSearchModel { 資料_ID = siryouName.資料_ID });

                //取得データを編集
                siryouList.ForEach(siryou =>
                {
                    //ID
                    siryou.ID = 0;

                    //メンテ済部署
                    siryou.メンテ部署_CODE = null;

                    //確認完了予定日/完了日
                    siryou.完了日程情報 = null;

                    //資料ID
                    siryou.資料_ID = siryouName.ID;

                });

                //試験計画_資料を登録
                results.Add(this.MergeSiryou(siryouList, false));

            });

            return results.All(x => x == true);

        }
        #endregion

        #region 検討会資料更新
        /// <summary>
        /// 検討会資料更新
        /// </summary>
        /// <param name="list">検討会資料</param>
        /// <returns></returns>
        public bool UpdateMeetingDocument(List<MeetingDocumentModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //MEETING_DOCUMENT_NAMEを更新
                results.Add(this.UpdateMeetingDocumentName(list));

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
        /// MEETING_DOCUMENT_NAMEを更新
        /// </summary>
        /// <param name="list">検討会資料</param>
        /// <returns></returns>
        private bool UpdateMeetingDocumentName(List<MeetingDocumentModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    \"MEETING_DOCUMENT_NAME\" ");
            sql.AppendLine("SET");
            sql.AppendLine("     \"MEETING_FLAG\" = :MEETING_FLAG");
            sql.AppendLine("    ,\"MONTH\" = :MONTH");
            sql.AppendLine("    ,\"NAME\" = :NAME");
            sql.AppendLine("    ,\"EDIT_TERM_START\" = :EDIT_TERM_START");
            sql.AppendLine("    ,\"EDIT_TERM_END\" = :EDIT_TERM_END");
            sql.AppendLine("    ,\"CHANGE_DATETIME\" = SYSTIMESTAMP");
            sql.AppendLine("    ,\"CHANGE_PERSONEL_ID\" = :INPUT_PERSONEL_ID");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND \"ID\" = :ID");

            var results = new List<bool>();

            foreach (var siryou in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":MEETING_FLAG", Type = OracleDbType.Int16, Object = siryou.MEETING_FLAG, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MONTH", Type = OracleDbType.Date, Object = siryou.MONTH, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":NAME", Type = OracleDbType.Varchar2, Object = siryou.NAME, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":EDIT_TERM_START", Type = OracleDbType.Date, Object = siryou.EDIT_TERM_START, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":EDIT_TERM_END", Type = OracleDbType.Date, Object = siryou.EDIT_TERM_END, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = siryou.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ID", Type = OracleDbType.Int32, Object = siryou.ID, Direct = ParameterDirection.Input }

                };

                //更新
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region 検討会資料削除
        /// <summary>
        /// 検討会資料削除
        /// </summary>
        /// <param name="list">検討会資料</param>
        /// <returns></returns>
        public bool DeleteMeetingDocument(List<MeetingDocumentModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //削除対象があるかどうか
            if (list != null && list.Any() == true)
            {
                var idList = list.Select(x => x.ID).ToList();

                //試験計画_資料を全て削除
                results.Add(this.DeleteSiryouAllBySiryouID(idList));

                //MEETING_DOCUMENT_NAMEを全て削除
                results.Add(this.DeleteMeetingDocumentNameAll(idList));

            }

            //削除が成功したかどうか
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
        /// 試験計画_資料を全て削除(資料ID)
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteSiryouAllBySiryouID(List<int> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_資料\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.Append("    AND \"資料_ID\" IN (NULL");

            //IDで削除
            for (var i = 0; i < idList.Count(); i++)
            {
                var name = string.Format(":ID{0}", i);

                sql.AppendFormat(",{0}", name);

                prms.Add(new BindModel { Name = name, Type = OracleDbType.Int32, Object = idList.ElementAt(i), Direct = ParameterDirection.Input });

            }

            sql.AppendLine(")");

            return db.DeleteData(sql.ToString(), prms);

        }

        /// <summary>
        /// MEETING_DOCUMENT_NAMEを全て削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteMeetingDocumentNameAll(List<int> idList)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"MEETING_DOCUMENT_NAME\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.Append("    AND \"ID\" IN (NULL");

            //IDで削除
            for (var i = 0; i < idList.Count(); i++)
            {
                var name = string.Format(":ID{0}", i);

                sql.AppendFormat(",{0}", name);

                prms.Add(new BindModel { Name = name, Type = OracleDbType.Int32, Object = idList.ElementAt(i), Direct = ParameterDirection.Input });

            }

            sql.AppendLine(")");

            return db.DeleteData(sql.ToString(), prms);

        }
        #endregion

        #region 検討会課題詳細取得
        /// <summary>
        /// 検討会課題履歴取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<MeetingDocumentDetailModel> GetMeetingDocumentDetail(MeetingDocumentDetailSearchModel cond)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     B.\"ID\"");
            sql.AppendLine("    ,A.\"ID\" AS \"資料_ID\"");
            sql.AppendLine("    ,B.\"CATEGORY_ID\"");
            sql.AppendLine("    ,A.\"NAME\"");
            sql.AppendLine("    ,A.\"MONTH\"");
            sql.AppendLine("    ,NVL(B.\"OPEN_CLOSE\",'open') AS \"OPEN_CLOSE\"");
            sql.AppendLine("    ,B.\"メンテ部署_CODE\"");
            sql.AppendLine("    ,B.\"GENERAL_CODE\"");
            sql.AppendLine("    ,B.\"部記号\"");
            sql.AppendLine("    ,B.\"部別項目番号\"");
            sql.AppendLine("    ,B.\"試験部署_CODE\"");
            sql.AppendLine("    ,B.\"CATEGORY\"");
            sql.AppendLine("    ,B.\"CURRENT_SITUATION\"");
            sql.AppendLine("    ,B.\"確認方法\"");
            sql.AppendLine("    ,B.\"状況\"");
            sql.AppendLine("    ,B.\"FUTURE_SCHEDULE\"");
            sql.AppendLine("    ,B.\"関連設計\"");
            sql.AppendLine("    ,B.\"影響部品\"");
            sql.AppendLine("    ,B.\"出図日程\"");
            sql.AppendLine("    ,B.\"完了日程情報\"");
            sql.AppendLine("    ,B.\"コスト変動\"");
            sql.AppendLine("    ,B.\"質量変動\"");
            sql.AppendLine("    ,B.\"投資変動\"");
            sql.AppendLine("    ,B.\"SORT_NO\"");
            sql.AppendLine("    ,B.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    ,B.\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("    ,C.\"NAME\" AS \"INPUT_NAME\"");
            sql.AppendLine("    ,B.\"INPUT_DATETIME\"");
            sql.AppendLine("    ,CAST(NVL(C.\"CATEGORY_ID_COUNT\",0) AS NUMBER(10)) AS \"CATEGORY_ID_COUNT\"");
            sql.AppendLine("    ,DECODE(NVL(C.\"CATEGORY_ID_COUNT\",1),1,'-','表示') AS \"HISTORY_NAME\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"MEETING_DOCUMENT_NAME\" A");
            sql.AppendLine("    INNER JOIN \"試験計画_資料\" B");
            sql.AppendLine("    ON A.\"ID\" = B.\"資料_ID\"");
            sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" C");
            sql.AppendLine("    ON B.\"INPUT_PERSONEL_ID\" = C.\"PERSONEL_ID\"");
            sql.AppendLine("    INNER JOIN");
            sql.AppendLine("                (");
            sql.AppendLine("                    SELECT");
            sql.AppendLine("                         A.\"CATEGORY_ID\"");
            sql.AppendLine("                        ,COUNT(*) AS \"CATEGORY_ID_COUNT\"");
            sql.AppendLine("                    FROM");
            sql.AppendLine("                        \"試験計画_資料\" A");
            sql.AppendLine("                    WHERE 0 = 0");
            sql.AppendLine("                        AND A.\"CATEGORY_ID\" IS NOT NULL");
            sql.AppendLine("                    GROUP BY");
            sql.AppendLine("                        A.\"CATEGORY_ID\"");
            sql.AppendLine("                ) C");
            sql.AppendLine("    ON B.\"CATEGORY_ID\" = C.\"CATEGORY_ID\"");
            sql.AppendLine("WHERE 0 = 0");

            //カテゴリーID
            if (cond.CATEGORY_ID != null)
            {
                sql.AppendLine("    AND B.\"CATEGORY_ID\" = :CATEGORY_ID");

                prms.Add(new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = cond.CATEGORY_ID, Direct = ParameterDirection.Input });

            }
            else
            {
                //資料_ID
                if (cond.資料_ID != null)
                {
                    sql.AppendLine("    AND B.\"資料_ID\" = :資料_ID");

                    prms.Add(new BindModel { Name = ":資料_ID", Type = OracleDbType.Int32, Object = cond.資料_ID, Direct = ParameterDirection.Input });

                }

                //開発符号
                if (string.IsNullOrWhiteSpace(cond.GENERAL_CODE) == false)
                {
                    sql.AppendLine("    AND B.\"GENERAL_CODE\" = :GENERAL_CODE");

                    prms.Add(new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = cond.GENERAL_CODE, Direct = ParameterDirection.Input });


                }

                //ステータス
                if (string.IsNullOrWhiteSpace(cond.OPEN_CLOSE) == false)
                {
                    sql.AppendLine("    AND NVL(B.\"OPEN_CLOSE\",'open') = :OPEN_CLOSE");

                    prms.Add(new BindModel { Name = ":OPEN_CLOSE", Type = OracleDbType.Varchar2, Object = cond.OPEN_CLOSE, Direct = ParameterDirection.Input });

                }

                //担当課
                if (string.IsNullOrWhiteSpace(cond.試験部署_CODE) == false)
                {
                    sql.AppendLine("    AND B.\"試験部署_CODE\" LIKE '%' || :試験部署_CODE || '%'");

                    prms.Add(new BindModel { Name = ":試験部署_CODE", Type = OracleDbType.Varchar2, Object = cond.試験部署_CODE, Direct = ParameterDirection.Input });

                }

                //状況
                var jyoukyouList = cond.状況;
                if (jyoukyouList != null && jyoukyouList.Any() == true)
                {
                    sql.AppendLine("    AND");
                    sql.AppendLine("        (");

                    //状況の記号が指定されているかどうか
                    var jyoukyouKigou = jyoukyouList.Where(x => string.IsNullOrWhiteSpace(x) == false).ToArray();
                    if (jyoukyouKigou.Any() == true)
                    {
                        sql.Append("            B.\"状況\" IN (NULL");

                        for (var i = 0; i < jyoukyouKigou.Count(); i++)
                        {
                            var name = string.Format(":状況{0}", i);

                            sql.AppendFormat(",{0}", name);

                            prms.Add(new BindModel { Name = name, Type = OracleDbType.Varchar2, Object = jyoukyouKigou.ElementAt(i), Direct = ParameterDirection.Input });

                        }

                        sql.AppendLine(")");

                    }

                    //空文字が指定されているかどうか
                    if (jyoukyouList.Any(x => string.IsNullOrWhiteSpace(x) == true) == true)
                    {
                        //状況の記号が指定されているかどうか
                        if (jyoukyouKigou.Any() == true)
                        {
                            sql.AppendLine("            OR");

                        }

                        sql.AppendLine("            B.\"状況\" IS NULL");

                    }

                    sql.AppendLine("        )");


                }

            }

            sql.AppendLine("ORDER BY");

            //カテゴリーID
            if (cond.CATEGORY_ID == null)
            {
                sql.AppendLine("     B.\"GENERAL_CODE\"");
                sql.AppendLine("    ,B.\"部記号\"");
                sql.AppendLine("    ,B.\"部別項目番号\"");

            }
            else
            {
                sql.AppendLine("     A.\"MONTH\" DESC");
                sql.AppendLine("    ,B.\"INPUT_DATETIME\" DESC");

            }

            //取得
            return db.ReadModelList<MeetingDocumentDetailModel>(sql.ToString(), prms);

        }
        #endregion

        #region 検討会資料詳細更新
        /// <summary>
        /// 検討会資料更新
        /// </summary>
        /// <param name="list">検討会資料詳細</param>
        /// <returns></returns>
        public bool UpdateMeetingDocumentDetail(List<MeetingDocumentDetailModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //試験計画_資料を登録
                results.Add(this.MergeSiryou(list.Where(x => x.DELETE_FLG == false).ToList(), true));

                //試験計画_資料を削除
                var idList = list.Where(x => x.DELETE_FLG == true).Select(x => x.ID).ToList();
                results.Add(this.DeleteSiryouAllByID(idList));

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
        /// 試験計画_資料を登録
        /// </summary>
        /// <param name="list">検討会資料詳細</param>
        /// <param name="isNewCategoryId">カテゴリーID採番可否</param>
        /// <returns></returns>
        private bool MergeSiryou(List<MeetingDocumentDetailModel> list, bool isNewCategoryId)
        {
            //対象が無ければ終了
            if (list == null || list.Any() == false)
            {
                return true;

            }

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"試験計画_資料\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             (NVL(MAX(\"ID\"),0) + 1) AS \"NEW_ID\"");
            sql.AppendLine("            ,(NVL(MAX(\"CATEGORY_ID\"),0) + 1) AS \"NEW_CATEGORY_ID\"");
            sql.AppendLine("            ,:ID AS \"ID\"");
            sql.AppendLine("            ,:CATEGORY AS \"CATEGORY\"");
            sql.AppendLine("            ,:CURRENT_SITUATION AS \"CURRENT_SITUATION\"");
            sql.AppendLine("            ,:FUTURE_SCHEDULE AS \"FUTURE_SCHEDULE\"");
            sql.AppendLine("            ,:INPUT_PERSONEL_ID AS \"INPUT_PERSONEL_ID\"");
            sql.AppendLine("            ,:INPUT_DATETIME AS \"INPUT_DATETIME\"");
            sql.AppendLine("            ,:SORT_NO AS \"SORT_NO\"");
            sql.AppendLine("            ,:GENERAL_CODE AS \"GENERAL_CODE\"");
            sql.AppendLine("            ,:SECTION_GROUP_ID AS \"SECTION_GROUP_ID\"");
            sql.AppendLine("            ,:資料_ID AS \"資料_ID\"");
            sql.AppendLine("            ,:関連設計 AS \"関連設計\"");
            sql.AppendLine("            ,:完了日程情報 AS \"完了日程情報\"");
            sql.AppendLine("            ,:試験部署_CODE AS \"試験部署_CODE\"");
            sql.AppendLine("            ,:CATEGORY_ID AS \"CATEGORY_ID\"");
            sql.AppendLine("            ,:OPEN_CLOSE AS \"OPEN_CLOSE\"");
            sql.AppendLine("            ,:メンテ部署_CODE AS \"メンテ部署_CODE\"");
            sql.AppendLine("            ,:確認方法 AS \"確認方法\"");
            sql.AppendLine("            ,:状況 AS \"状況\"");
            sql.AppendLine("            ,:影響部品 AS \"影響部品\"");
            sql.AppendLine("            ,:出図日程 AS \"出図日程\"");
            sql.AppendLine("            ,:コスト変動 AS \"コスト変動\"");
            sql.AppendLine("            ,:質量変動 AS \"質量変動\"");
            sql.AppendLine("            ,:投資変動 AS \"投資変動\"");
            sql.AppendLine("            ,:部記号 AS \"部記号\"");
            sql.AppendLine("            ,:部別項目番号 AS \"部別項目番号\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"試験計画_資料\" B");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"ID\" = B.\"ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("         A.\"CATEGORY\" = B.\"CATEGORY\"");
            sql.AppendLine("        ,A.\"CURRENT_SITUATION\" = B.\"CURRENT_SITUATION\"");
            sql.AppendLine("        ,A.\"FUTURE_SCHEDULE\" = B.\"FUTURE_SCHEDULE\"");
            sql.AppendLine("        ,A.\"INPUT_PERSONEL_ID\" = B.\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("        ,A.\"INPUT_DATETIME\" = B.\"INPUT_DATETIME\"");
            sql.AppendLine("        ,A.\"SORT_NO\" = B.\"SORT_NO\"");
            sql.AppendLine("        ,A.\"GENERAL_CODE\" = B.\"GENERAL_CODE\"");
            sql.AppendLine("        ,A.\"SECTION_GROUP_ID\" = B.\"SECTION_GROUP_ID\"");
            sql.AppendLine("        ,A.\"関連設計\" = B.\"関連設計\"");
            sql.AppendLine("        ,A.\"完了日程情報\" = B.\"完了日程情報\"");
            sql.AppendLine("        ,A.\"試験部署_CODE\" = B.\"試験部署_CODE\"");
            sql.AppendLine("        ,A.\"OPEN_CLOSE\" = B.\"OPEN_CLOSE\"");
            sql.AppendLine("        ,A.\"メンテ部署_CODE\" = B.\"メンテ部署_CODE\"");
            sql.AppendLine("        ,A.\"確認方法\" = B.\"確認方法\"");
            sql.AppendLine("        ,A.\"状況\" = B.\"状況\"");
            sql.AppendLine("        ,A.\"影響部品\" = B.\"影響部品\"");
            sql.AppendLine("        ,A.\"出図日程\" = B.\"出図日程\"");
            sql.AppendLine("        ,A.\"コスト変動\" = B.\"コスト変動\"");
            sql.AppendLine("        ,A.\"質量変動\" = B.\"質量変動\"");
            sql.AppendLine("        ,A.\"投資変動\" = B.\"投資変動\"");
            sql.AppendLine("        ,A.\"部記号\" = B.\"部記号\"");
            sql.AppendLine("        ,A.\"部別項目番号\" = B.\"部別項目番号\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"ID\"");
            sql.AppendLine("        ,\"CATEGORY\"");
            sql.AppendLine("        ,\"CURRENT_SITUATION\"");
            sql.AppendLine("        ,\"FUTURE_SCHEDULE\"");
            sql.AppendLine("        ,\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("        ,\"INPUT_DATETIME\"");
            sql.AppendLine("        ,\"SORT_NO\"");
            sql.AppendLine("        ,\"GENERAL_CODE\"");
            sql.AppendLine("        ,\"SECTION_GROUP_ID\"");
            sql.AppendLine("        ,\"資料_ID\"");
            sql.AppendLine("        ,\"関連設計\"");
            sql.AppendLine("        ,\"完了日程情報\"");
            sql.AppendLine("        ,\"試験部署_CODE\"");
            sql.AppendLine("        ,\"CATEGORY_ID\"");
            sql.AppendLine("        ,\"OPEN_CLOSE\"");
            sql.AppendLine("        ,\"メンテ部署_CODE\"");
            sql.AppendLine("        ,\"確認方法\"");
            sql.AppendLine("        ,\"状況\"");
            sql.AppendLine("        ,\"影響部品\"");
            sql.AppendLine("        ,\"出図日程\"");
            sql.AppendLine("        ,\"コスト変動\"");
            sql.AppendLine("        ,\"質量変動\"");
            sql.AppendLine("        ,\"投資変動\"");
            sql.AppendLine("        ,\"部記号\"");
            sql.AppendLine("        ,\"部別項目番号\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"NEW_ID\"");
            sql.AppendLine("        ,B.\"CATEGORY\"");
            sql.AppendLine("        ,B.\"CURRENT_SITUATION\"");
            sql.AppendLine("        ,B.\"FUTURE_SCHEDULE\"");
            sql.AppendLine("        ,B.\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("        ,B.\"INPUT_DATETIME\"");
            sql.AppendLine("        ,B.\"SORT_NO\"");
            sql.AppendLine("        ,B.\"GENERAL_CODE\"");
            sql.AppendLine("        ,B.\"SECTION_GROUP_ID\"");
            sql.AppendLine("        ,B.\"資料_ID\"");
            sql.AppendLine("        ,B.\"関連設計\"");
            sql.AppendLine("        ,B.\"完了日程情報\"");
            sql.AppendLine("        ,B.\"試験部署_CODE\"");
            sql.AppendFormat("        ,B.\"{0}\"", isNewCategoryId == true ? "NEW_CATEGORY_ID" : "CATEGORY_ID");
            sql.AppendLine("        ,B.\"OPEN_CLOSE\"");
            sql.AppendLine("        ,B.\"メンテ部署_CODE\"");
            sql.AppendLine("        ,B.\"確認方法\"");
            sql.AppendLine("        ,B.\"状況\"");
            sql.AppendLine("        ,B.\"影響部品\"");
            sql.AppendLine("        ,B.\"出図日程\"");
            sql.AppendLine("        ,B.\"コスト変動\"");
            sql.AppendLine("        ,B.\"質量変動\"");
            sql.AppendLine("        ,B.\"投資変動\"");
            sql.AppendLine("        ,B.\"部記号\"");
            sql.AppendLine("        ,B.\"部別項目番号\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var siryou in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = siryou.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CATEGORY", Type = OracleDbType.Varchar2, Object = siryou.CATEGORY, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CURRENT_SITUATION", Type = OracleDbType.Varchar2, Object = siryou.CURRENT_SITUATION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FUTURE_SCHEDULE", Type = OracleDbType.Varchar2, Object = siryou.FUTURE_SCHEDULE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = siryou.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_DATETIME", Type = OracleDbType.Date, Object = siryou.INPUT_DATETIME, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SORT_NO", Type = OracleDbType.Single, Object = siryou.SORT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = siryou.GENERAL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = siryou.SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":資料_ID", Type = OracleDbType.Int32, Object = siryou.資料_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":関連設計", Type = OracleDbType.Varchar2, Object = siryou.関連設計, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":完了日程情報", Type = OracleDbType.Varchar2, Object = siryou.完了日程情報, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験部署_CODE", Type = OracleDbType.Varchar2, Object = siryou.試験部署_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = siryou.CATEGORY_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":OPEN_CLOSE", Type = OracleDbType.Varchar2, Object = siryou.OPEN_CLOSE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":メンテ部署_CODE", Type = OracleDbType.Varchar2, Object = siryou.メンテ部署_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":確認方法", Type = OracleDbType.Varchar2, Object = siryou.確認方法, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":状況", Type = OracleDbType.Varchar2, Object = siryou.状況, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":影響部品", Type = OracleDbType.Varchar2, Object = siryou.影響部品, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":出図日程", Type = OracleDbType.Varchar2, Object = siryou.出図日程, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":コスト変動", Type = OracleDbType.Varchar2, Object = siryou.コスト変動, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":質量変動", Type = OracleDbType.Varchar2, Object = siryou.質量変動, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":投資変動", Type = OracleDbType.Varchar2, Object = siryou.投資変動, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":部記号", Type = OracleDbType.Varchar2, Object = siryou.部記号, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":部別項目番号", Type = OracleDbType.Int32, Object = siryou.部別項目番号, Direct = ParameterDirection.Input },

                };

                //登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }

        /// <summary>
        /// 試験計画_資料を全て削除(ID)
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns>削除可否</returns>
        private bool DeleteSiryouAllByID(List<long> idList)
        {
            //対象が無ければ終了
            if (idList == null || idList.Any() == false)
            {
                return true;

            }

            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"試験計画_資料\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.Append("    AND \"ID\" IN (NULL");

            //IDで削除
            for (var i = 0; i < idList.Count(); i++)
            {
                var name = string.Format(":ID{0}", i);

                sql.AppendFormat(",{0}", name);

                prms.Add(new BindModel { Name = name, Type = OracleDbType.Int64, Object = idList.ElementAt(i), Direct = ParameterDirection.Input });

            }

            sql.AppendLine(")");

            return db.DeleteData(sql.ToString(), prms);

        }
        #endregion

    }
}