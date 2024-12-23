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
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>外製車日程の操作</remarks>
    public class OuterCarScheduleLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 外製車日程の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(OuterCarScheduleGetInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     A.GENERAL_CODE");
            sql.AppendLine("    ,A.CATEGORY");
            sql.AppendLine("    ,A.START_DATE");
            sql.AppendLine("    ,A.END_DATE");
            sql.AppendLine("    ,A.ID");
            sql.AppendLine("    ,A.CATEGORY_ID");
            sql.AppendLine("    ,A.DESCRIPTION");
            sql.AppendLine("    ,A.SYMBOL");
            sql.AppendLine("    ,A.予約種別");
            sql.AppendLine("    ,A.PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,A.INPUT_DATETIME");
            sql.AppendLine("    ,B.目的");
            sql.AppendLine("    ,B.行先");
            sql.AppendLine("    ,B.TEL");
            sql.AppendLine("    ,NVL(B.FLAG_実使用, 0) FLAG_実使用");
            sql.AppendLine("    ,B.予約者_ID");
            sql.AppendLine("    ,E.SECTION_CODE 予約者_SECTION_CODE");
            sql.AppendLine("    ,C.NAME 予約者_NAME");
            sql.AppendLine("    ,B.管理票番号");
            sql.AppendLine("    ,B.駐車場番号");
            sql.AppendLine("    ,NVL(B.FLAG_空時間貸出可, 0) FLAG_空時間貸出可");
            sql.AppendLine("    ,F.\"FLAG_要予約許可\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    OUTERCAR_SCHEDULE A");
            sql.AppendLine("    LEFT JOIN 試験計画_外製車日程_目的行先 B");
            sql.AppendLine("    ON A.ID = B.SCHEDULE_ID");
            sql.AppendLine("    LEFT JOIN PERSONEL_LIST C");
            sql.AppendLine("    ON B.予約者_ID = C.PERSONEL_ID");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA D");
            sql.AppendLine("    ON D.SECTION_GROUP_ID = C.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN SECTION_DATA E");
            sql.AppendLine("    ON E.SECTION_ID = D.SECTION_ID");
            sql.AppendLine("    LEFT JOIN 試験計画_外製車日程_車両リスト F");
            sql.AppendLine("    ON A.CATEGORY_ID = F.CATEGORY_ID");
            sql.AppendLine("    LEFT JOIN VIEW_試験車基本情報 G");
            sql.AppendLine("    ON F.管理票番号 = G.管理票NO");
            sql.AppendLine("    LEFT JOIN 試験車履歴情報 H");
            sql.AppendLine("    ON G.データID = H.データID");
            sql.AppendLine("    LEFT JOIN (SELECT データID, MAX(履歴NO) 履歴NO FROM 試験車履歴情報 GROUP BY データID) I");
            sql.AppendLine("    ON H.データID = I.データID");
            sql.AppendLine("    AND H.履歴NO = I.履歴NO");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA J");
            sql.AppendLine("    ON H.管理責任部署 = J.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN SECTION_DATA K");
            sql.AppendLine("    ON J.SECTION_ID = K.SECTION_ID");
            sql.AppendLine("WHERE 0 = 0");

            // 車系
            if (val != null && !string.IsNullOrWhiteSpace(val.車系))
            {
                sql.AppendLine("    AND A.GENERAL_CODE = :車系");

                prms.Add(new BindModel
                {
                    Name = ":車系",
                    Type = OracleDbType.Varchar2,
                    Object = val.車系,
                    Direct = ParameterDirection.Input
                });
            }

            // 管理票NO
            if (val != null && !string.IsNullOrWhiteSpace(val.管理票NO))
            {
                sql.AppendLine("    AND G.管理票NO LIKE :管理票NO");

                prms.Add(new BindModel
                {
                    Name = ":管理票NO",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.管理票NO + "%",
                    Direct = ParameterDirection.Input
                });
            }

            // 駐車場番号
            if (val != null && !string.IsNullOrWhiteSpace(val.駐車場番号))
            {
                sql.AppendLine("    AND G.駐車場番号 LIKE :駐車場番号");

                prms.Add(new BindModel
                {
                    Name = ":駐車場番号",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.駐車場番号 + "%",
                    Direct = ParameterDirection.Input
                });
            }

            // 所在地
            if (val != null && string.IsNullOrWhiteSpace(val.所在地) == false)
            {
                sql.AppendLine("    AND G.所在地 = :所在地");
                prms.Add(new BindModel { Name = ":所在地", Type = OracleDbType.Varchar2, Object = val.所在地, Direct = ParameterDirection.Input });

            }

            // 車型
            if (val != null && !string.IsNullOrWhiteSpace(val.車型))
            {
                sql.AppendLine("    AND G.車型 = :車型");

                prms.Add(new BindModel
                {
                    Name = ":車型",
                    Type = OracleDbType.Varchar2,
                    Object = val.車型,
                    Direct = ParameterDirection.Input
                });
            }

            // 仕向地
            if (val != null && !string.IsNullOrWhiteSpace(val.仕向地))
            {
                sql.AppendLine("    AND H.仕向地 = :仕向地");

                prms.Add(new BindModel
                {
                    Name = ":仕向地",
                    Type = OracleDbType.Varchar2,
                    Object = val.仕向地,
                    Direct = ParameterDirection.Input
                });
            }

            // メーカー名
            if (val != null && !string.IsNullOrWhiteSpace(val.メーカー名))
            {
                sql.AppendLine("    AND H.メーカー名 = :メーカー名");

                prms.Add(new BindModel
                {
                    Name = ":メーカー名",
                    Type = OracleDbType.Varchar2,
                    Object = val.メーカー名,
                    Direct = ParameterDirection.Input
                });
            }

            // 外製車名
            if (val != null && !string.IsNullOrWhiteSpace(val.外製車名))
            {
                sql.AppendLine("    AND H.外製車名 LIKE :外製車名");

                prms.Add(new BindModel
                {
                    Name = ":外製車名",
                    Type = OracleDbType.Varchar2,
                    Object = "%" + val.外製車名 + "%",
                    Direct = ParameterDirection.Input
                });
            }

            // FLAG_ETC付（0:無、1:有、2:両方）
            if (val != null && val.FLAG_ETC付 != null)
            {
                if (val.FLAG_ETC付 == 0)
                {
                    sql.AppendLine("    AND H.FLAG_ETC付 = 0");
                }
                else if (val.FLAG_ETC付 == 1)
                {
                    sql.AppendLine("    AND H.FLAG_ETC付 = 1");
                }
                else if (val.FLAG_ETC付 == 2)
                {
                    sql.AppendLine("    AND H.FLAG_ETC付 IN (0, 1)");
                }
            }

            // トランスミッション
            if (val != null && !string.IsNullOrWhiteSpace(val.トランスミッション))
            {
                sql.AppendLine("    AND H.トランスミッション = :トランスミッション");

                prms.Add(new BindModel
                {
                    Name = ":トランスミッション",
                    Type = OracleDbType.Varchar2,
                    Object = val.トランスミッション,
                    Direct = ParameterDirection.Input
                });
            }

            // 期間（開始・終了）
            if (val != null && (val.DATE_START != null || val.DATE_END != null))
            {
                var startDate = val.DATE_START == null ? null : (DateTime?)val.DATE_START.Value.Date;
                var endDate = val.DATE_END == null ? null : (DateTime?)val.DATE_END.Value.Date;

                //期間がすべて入力されているかどうか
                if (val.DATE_START != null && val.DATE_END != null)
                {
                    sql.AppendLine("    AND");
                    sql.AppendLine("        (");
                    sql.AppendLine("            :START_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                    sql.AppendLine("            OR");
                    sql.AppendLine("            :END_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                    sql.AppendLine("            OR");
                    sql.AppendLine("            A.\"START_DATE\" BETWEEN :START_DATE AND (:END_DATE + 1)");
                    sql.AppendLine("            OR");
                    sql.AppendLine("            A.\"END_DATE\" BETWEEN :START_DATE AND (:END_DATE + 1)");
                    sql.AppendLine("        )");

                    prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input });
                    prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

                }
                else
                {
                    //期間(From)
                    if (val.DATE_START != null)
                    {
                        sql.AppendLine("    AND");
                        sql.AppendLine("        (");
                        sql.AppendLine("            :START_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                        sql.AppendLine("            OR");
                        sql.AppendLine("            A.\"START_DATE\" >= :START_DATE");
                        sql.AppendLine("        )");

                        prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = startDate, Direct = ParameterDirection.Input });

                    }

                    //期間(To)
                    if (val.DATE_END != null)
                    {
                        sql.AppendLine("    AND");
                        sql.AppendLine("        (");
                        sql.AppendLine("            :END_DATE BETWEEN A.\"START_DATE\" AND A.\"END_DATE\"");
                        sql.AppendLine("            OR");
                        sql.AppendLine("            A.\"END_DATE\" < (:END_DATE + 1)");
                        sql.AppendLine("        )");

                        prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = endDate, Direct = ParameterDirection.Input });

                    }
                }
            }
            
            // 行番号
            if (val != null && val.PARALLEL_INDEX_GROUP > 0)
            {
                sql.AppendLine("    AND A.PARALLEL_INDEX_GROUP = :PARALLEL_INDEX_GROUP");

                prms.Add(new BindModel
                {
                    Name = ":PARALLEL_INDEX_GROUP",
                    Type = OracleDbType.Int32,
                    Object = val.PARALLEL_INDEX_GROUP,
                    Direct = ParameterDirection.Input
                });
            }

            // スケジュールID
            if (val != null && val.SCHEDULE_ID > 0)
            {
                sql.AppendLine("    AND A.ID = :SCHEDULE_ID");

                prms.Add(new BindModel
                {
                    Name = ":SCHEDULE_ID",
                    Type = OracleDbType.Int64,
                    Object = val.SCHEDULE_ID,
                    Direct = ParameterDirection.Input
                });
            }

            // カテゴリーID
            if (val != null && val.CATEGORY_ID > 0)
            {
                sql.AppendLine("    AND A.CATEGORY_ID = :CATEGORY_ID");

                prms.Add(new BindModel
                {
                    Name = ":CATEGORY_ID",
                    Type = OracleDbType.Int64,
                    Object = val.CATEGORY_ID,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    A.CATEGORY_ID ASC");
            sql.AppendLine("    ,A.ID ASC");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 外製車日程の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(OuterCarSchedulePostInModel val, ref OuterCarScheduleGetOutModel retModel)
        {
            var common = new CommonLogic();
            common.SetDBAccess(base.db);
            
            retModel.SCHEDULE_ID = common.GetScheduleNewID();

            var item = common.GetScheduleItemRow(val.CATEGORY_ID, 3);
            retModel.INPUT_DATETIME = DateTime.Now;
            
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("OUTERCAR_SCHEDULE(");
            sql.AppendLine("    GENERAL_CODE");
            sql.AppendLine("    ,CATEGORY");
            sql.AppendLine("    ,START_DATE");
            sql.AppendLine("    ,END_DATE");
            sql.AppendLine("    ,SORT_NO");
            sql.AppendLine("    ,PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,ID");
            sql.AppendLine("    ,SYMBOL");
            sql.AppendLine("    ,DESCRIPTION");
            sql.AppendLine("    ,SECTION_GROUP_ID");
            sql.AppendLine("    ,FLAG_CLASS");
            sql.AppendLine("    ,INPUT_DATETIME");
            sql.AppendLine("    ,INPUT_PERSONEL_ID");
            sql.AppendLine("    ,INPUT_LOGIN_ID");
            sql.AppendLine("    ,CATEGORY_ID");
            sql.AppendLine("    ,予約種別");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("    :GENERAL_CODE");
            sql.AppendLine("    ,:CATEGORY");
            sql.AppendLine("    ,:START_DATE");
            sql.AppendLine("    ,:END_DATE");
            sql.AppendLine("    ,(SELECT NVL(MAX(SORT_NO), 0) + 1 FROM OUTERCAR_SCHEDULE WHERE CATEGORY_ID = :CATEGORY_ID)");
            sql.AppendLine("    ,:PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,:SCHEDULE_ID");
            sql.AppendLine("    ,:SYMBOL");
            sql.AppendLine("    ,:DESCRIPTION");
            sql.AppendLine("    ,:SECTION_GROUP_ID");
            sql.AppendLine("    ,'外製車日程'");
            sql.AppendLine("    ,SYSDATE");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID");
            sql.AppendLine("    ,:INPUT_LOGIN_ID");
            sql.AppendLine("    ,:CATEGORY_ID");
            sql.AppendLine("    ,:予約種別");
            sql.AppendLine(")");

            // 開発符号：必須
            prms.Add(new BindModel
            {
                Name = ":GENERAL_CODE",
                Type = OracleDbType.Varchar2,
                Object = item["GENERAL_CODE"],
                Direct = ParameterDirection.Input
            });
            retModel.GENERAL_CODE = item["GENERAL_CODE"].ToString();

            // カテゴリーID：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });
            retModel.CATEGORY_ID = val.CATEGORY_ID;

            // カテゴリー：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY",
                Type = OracleDbType.Varchar2,
                Object = item["CATEGORY"],
                Direct = ParameterDirection.Input
            });
            retModel.CATEGORY = item["CATEGORY"].ToString();

            // 期間（開始）：必須
            prms.Add(new BindModel
            {
                Name = ":START_DATE",
                Type = OracleDbType.Date,
                Object = val.START_DATE,
                Direct = ParameterDirection.Input
            });
            retModel.START_DATE = val.START_DATE;

            // 期間（終了）：必須
            prms.Add(new BindModel
            {
                Name = ":END_DATE",
                Type = OracleDbType.Date,
                Object = val.END_DATE,
                Direct = ParameterDirection.Input
            });
            retModel.END_DATE = val.END_DATE;

            // 行番号：必須
            prms.Add(new BindModel
            {
                Name = ":PARALLEL_INDEX_GROUP",
                Type = OracleDbType.Int32,
                Object = val.PARALLEL_INDEX_GROUP,
                Direct = ParameterDirection.Input
            });
            retModel.PARALLEL_INDEX_GROUP = val.PARALLEL_INDEX_GROUP;

            // スケジュールID：必須
            prms.Add(new BindModel
            {
                Name = ":SCHEDULE_ID",
                Type = OracleDbType.Int64,
                Object = retModel.SCHEDULE_ID,
                Direct = ParameterDirection.Input
            });

            // スケジュール区分：必須
            prms.Add(new BindModel
            {
                Name = ":SYMBOL",
                Type = OracleDbType.Int16,
                Object = val.SYMBOL,
                Direct = ParameterDirection.Input
            });
            retModel.SYMBOL = val.SYMBOL;

            // 説明：必須
            prms.Add(new BindModel
            {
                Name = ":DESCRIPTION",
                Type = OracleDbType.Varchar2,
                Object = val.DESCRIPTION,
                Direct = ParameterDirection.Input
            });
            retModel.DESCRIPTION = val.DESCRIPTION;

            // 所属グループID：必須
            prms.Add(new BindModel
            {
                Name = ":SECTION_GROUP_ID",
                Type = OracleDbType.Varchar2,
                Object = item["SECTION_GROUP_ID"],
                Direct = ParameterDirection.Input
            });

            // パーソナルID：必須
            prms.Add(new BindModel
            {
                Name = ":INPUT_PERSONEL_ID",
                Type = OracleDbType.Varchar2,
                Object = val.PERSONEL_ID,
                Direct = ParameterDirection.Input
            });

            // パーソナルID：必須
            prms.Add(new BindModel
            {
                Name = ":INPUT_LOGIN_ID",
                Type = OracleDbType.Varchar2,
                Object = val.PERSONEL_ID,
                Direct = ParameterDirection.Input
            });

            // カテゴリーID：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            // 予約種別：必須
            prms.Add(new BindModel
            {
                Name = ":予約種別",
                Type = OracleDbType.Varchar2,
                Object = val.予約種別,
                Direct = ParameterDirection.Input
            });
            retModel.予約種別 = val.予約種別;

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験計画_外製車日程_目的行先(");
            sql.AppendLine("    ID");
            sql.AppendLine("    ,SCHEDULE_ID");
            sql.AppendLine("    ,目的");
            sql.AppendLine("    ,行先");
            sql.AppendLine("    ,TEL");
            sql.AppendLine("    ,FLAG_空時間貸出可");
            sql.AppendLine("    ,予約者_ID");
            sql.AppendLine("    ,駐車場番号");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("    (SELECT NVL(MAX(ID), 0) + 1 FROM 試験計画_外製車日程_目的行先)");
            sql.AppendLine("    ,:SCHEDULE_ID");
            sql.AppendLine("    ,:目的");
            sql.AppendLine("    ,:行先");
            sql.AppendLine("    ,:TEL");
            sql.AppendLine("    ,:FLAG_空時間貸出可");
            sql.AppendLine("    ,:予約者_ID");
            sql.AppendLine("    ,:駐車場番号");
            sql.AppendLine(")");

            // スケジュールID：必須
            prms.Add(new BindModel
            {
                Name = ":SCHEDULE_ID",
                Type = OracleDbType.Int64,
                Object = retModel.SCHEDULE_ID,
                Direct = ParameterDirection.Input
            });

            // 目的：必須
            prms.Add(new BindModel
            {
                Name = ":目的",
                Type = OracleDbType.Varchar2,
                Object = val.目的,
                Direct = ParameterDirection.Input
            });
            retModel.目的 = val.目的;

            // 行先：必須
            prms.Add(new BindModel
            {
                Name = ":行先",
                Type = OracleDbType.Varchar2,
                Object = val.行先,
                Direct = ParameterDirection.Input
            });
            retModel.行先 = val.行先;

            // TEL：必須
            prms.Add(new BindModel
            {
                Name = ":TEL",
                Type = OracleDbType.Varchar2,
                Object = val.TEL,
                Direct = ParameterDirection.Input
            });
            retModel.TEL = val.TEL;

            // FLAG_空時間貸出可：必須
            prms.Add(new BindModel
            {
                Name = ":FLAG_空時間貸出可",
                Type = OracleDbType.Varchar2,
                Object = val.FLAG_空時間貸出可,
                Direct = ParameterDirection.Input
            });
            retModel.FLAG_空時間貸出可 = val.FLAG_空時間貸出可;

            // 予約者_ID：必須
            prms.Add(new BindModel
            {
                Name = ":予約者_ID",
                Type = OracleDbType.Varchar2,
                Object = val.予約者_ID,
                Direct = ParameterDirection.Input
            });
            retModel.予約者_ID = val.予約者_ID;

            // 駐車場番号
            prms.Add(new BindModel
            {
                Name = ":駐車場番号",
                Type = OracleDbType.Varchar2,
                Object = val.駐車場番号,
                Direct = ParameterDirection.Input
            });
            retModel.駐車場番号 = val.駐車場番号;

            return db.InsertData(sql.ToString(), prms);
        }

        /// <summary>
        /// 外製車日程の更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(OuterCarSchedulePutInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    OUTERCAR_SCHEDULE");
            sql.AppendLine("SET");
            sql.AppendLine("    START_DATE = :START_DATE");
            sql.AppendLine("    ,END_DATE = :END_DATE");
            sql.AppendLine("    ,SYMBOL = :SYMBOL");
            sql.AppendLine("    ,DESCRIPTION = :DESCRIPTION");
            sql.AppendLine("    ,PARALLEL_INDEX_GROUP = :PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,CHANGE_DATETIME = SYSDATE");
            sql.AppendLine("    ,INPUT_PERSONEL_ID = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,INPUT_LOGIN_ID = :INPUT_LOGIN_ID");
            sql.AppendLine("    ,予約種別 = :予約種別");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :SCHEDULE_ID");

            // スケジュールID：必須
            prms.Add(new BindModel
            {
                Name = ":SCHEDULE_ID",
                Type = OracleDbType.Int64,
                Object = val.SCHEDULE_ID,
                Direct = ParameterDirection.Input
            });

            // 期間（開始）：必須
            prms.Add(new BindModel
            {
                Name = ":START_DATE",
                Type = OracleDbType.Date,
                Object = val.START_DATE,
                Direct = ParameterDirection.Input
            });

            // 期間（終了）：必須
            prms.Add(new BindModel
            {
                Name = ":END_DATE",
                Type = OracleDbType.Date,
                Object = val.END_DATE,
                Direct = ParameterDirection.Input
            });

            // スケジュール区分：必須
            prms.Add(new BindModel
            {
                Name = ":SYMBOL",
                Type = OracleDbType.Int16,
                Object = val.SYMBOL,
                Direct = ParameterDirection.Input
            });

            // 説明：必須
            prms.Add(new BindModel
            {
                Name = ":DESCRIPTION",
                Type = OracleDbType.Varchar2,
                Object = val.DESCRIPTION,
                Direct = ParameterDirection.Input
            });

            // 行番号：必須
            prms.Add(new BindModel
            {
                Name = ":PARALLEL_INDEX_GROUP",
                Type = OracleDbType.Int32,
                Object = val.PARALLEL_INDEX_GROUP,
                Direct = ParameterDirection.Input
            });

            // パーソナルID：必須
            prms.Add(new BindModel
            {
                Name = ":INPUT_PERSONEL_ID",
                Type = OracleDbType.Varchar2,
                Object = val.PERSONEL_ID,
                Direct = ParameterDirection.Input
            });

            // パーソナルID：必須
            prms.Add(new BindModel
            {
                Name = ":INPUT_LOGIN_ID",
                Type = OracleDbType.Varchar2,
                Object = val.PERSONEL_ID,
                Direct = ParameterDirection.Input
            });

            // 予約種別：必須
            prms.Add(new BindModel
            {
                Name = ":予約種別",
                Type = OracleDbType.Varchar2,
                Object = val.予約種別,
                Direct = ParameterDirection.Input
            });

            if (!db.UpdateData(sql.ToString(), prms))
            {
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    試験計画_外製車日程_目的行先");
            sql.AppendLine("SET");
            sql.AppendLine("    目的 = :目的");
            sql.AppendLine("    ,行先 = :行先");
            sql.AppendLine("    ,TEL = :TEL");
            sql.AppendLine("    ,FLAG_空時間貸出可 = :FLAG_空時間貸出可");
            sql.AppendLine("    ,駐車場番号 = :駐車場番号");
            sql.AppendLine("WHERE");
            sql.AppendLine("    SCHEDULE_ID = :SCHEDULE_ID");

            // スケジュールID：必須
            prms.Add(new BindModel
            {
                Name = ":SCHEDULE_ID",
                Type = OracleDbType.Int64,
                Object = val.SCHEDULE_ID,
                Direct = ParameterDirection.Input
            });

            // 目的：必須
            prms.Add(new BindModel
            {
                Name = ":目的",
                Type = OracleDbType.Varchar2,
                Object = val.目的,
                Direct = ParameterDirection.Input
            });

            // 行先：必須
            prms.Add(new BindModel
            {
                Name = ":行先",
                Type = OracleDbType.Varchar2,
                Object = val.行先,
                Direct = ParameterDirection.Input
            });

            // TEL：必須
            prms.Add(new BindModel
            {
                Name = ":TEL",
                Type = OracleDbType.Varchar2,
                Object = val.TEL,
                Direct = ParameterDirection.Input
            });

            // FLAG_空時間貸出可：必須
            prms.Add(new BindModel
            {
                Name = ":FLAG_空時間貸出可",
                Type = OracleDbType.Varchar2,
                Object = val.FLAG_空時間貸出可,
                Direct = ParameterDirection.Input
            });

            // 駐車場番号：必須
            prms.Add(new BindModel
            {
                Name = ":駐車場番号",
                Type = OracleDbType.Varchar2,
                Object = val.駐車場番号,
                Direct = ParameterDirection.Input
            });

            return db.UpdateData(sql.ToString(), prms);
        }

        /// <summary>
        /// 外製車日程の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(OuterCarScheduleDeleteInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    OUTERCAR_SCHEDULE");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :SCHEDULE_ID");

            prms.Add(new BindModel
            {
                Name = ":SCHEDULE_ID",
                Type = OracleDbType.Int64,
                Object = val.SCHEDULE_ID,
                Direct = ParameterDirection.Input
            });

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験計画_外製車日程_目的行先");
            sql.AppendLine("WHERE");
            sql.AppendLine("    SCHEDULE_ID = :SCHEDULE_ID");

            prms.Add(new BindModel
            {
                Name = ":SCHEDULE_ID",
                Type = OracleDbType.Int64,
                Object = val.SCHEDULE_ID,
                Direct = ParameterDirection.Input
            });

            return db.DeleteData(sql.ToString(), prms);
        }
    }
}