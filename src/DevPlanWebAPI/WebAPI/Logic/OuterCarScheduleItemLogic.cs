using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>外製車日程項目の操作</remarks>
    public class OuterCarScheduleItemLogic : BaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 外製車日程項目の取得
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetData(OuterCarScheduleItemGetInModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     A.GENERAL_CODE");
            sql.AppendLine("    ,I.CAR_GROUP");
            sql.AppendLine("    ,CASE");
            sql.AppendLine("      WHEN X.物品コード IS NOT NULL THEN A.\"CATEGORY\" || '\nGPS搭載'");
            sql.AppendLine("      ELSE A.\"CATEGORY\"");
            sql.AppendLine("     END AS CATEGORY ");
            sql.AppendLine("    ,NVL(A.SORT_NO, 1) SORT_NO");
            sql.AppendLine("    ,NVL(A.PARALLEL_INDEX_GROUP, 1) PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,A.ID");
            sql.AppendLine("    ,A.CATEGORY_ID");
            sql.AppendLine("    ,A.CLOSED_DATE");
            sql.AppendLine("    ,NVL(B.FLAG_要予約許可, 0) FLAG_要予約許可");
            sql.AppendLine("    ,H.最終予約可能日");
            sql.AppendLine("    ,D.メーカー名");
            sql.AppendLine("    ,D.外製車名");
            sql.AppendLine("    ,D.登録ナンバー");
            sql.AppendLine("    ,NVL(D.FLAG_ナビ付, 0) FLAG_ナビ付");
            sql.AppendLine("    ,NVL(D.FLAG_ETC付, 0) FLAG_ETC付");
            sql.AppendLine("    ,C.管理票NO");
            sql.AppendLine("    ,C.駐車場番号");
            sql.AppendLine("    ,CASE ");
            sql.AppendLine("      WHEN X.物品コード IS NOT NULL THEN 'GPS搭載'");
            sql.AppendLine("      ELSE NULL");
            sql.AppendLine("     END AS XEYE_EXIST ");
            sql.AppendLine("FROM");
            sql.AppendLine("    OUTERCAR_SCHEDULE_ITEM A");
            sql.AppendLine("    LEFT JOIN 試験計画_外製車日程_車両リスト B");
            sql.AppendLine("    ON A.ID = B.CATEGORY_ID");
            sql.AppendLine("    LEFT JOIN VIEW_試験車基本情報 C");
            sql.AppendLine("    ON B.管理票番号 = C.管理票NO");
            sql.AppendLine("    LEFT JOIN 試験車履歴情報 D");
            sql.AppendLine("    ON C.データID = D.データID");
            sql.AppendLine("    LEFT JOIN (SELECT データID, MAX(履歴NO) 履歴NO FROM 試験車履歴情報 GROUP BY データID) E");
            sql.AppendLine("    ON D.データID = E.データID");
            sql.AppendLine("    AND D.履歴NO = E.履歴NO");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA F");
            sql.AppendLine("    ON D.管理責任部署 = F.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN SECTION_DATA G");
            sql.AppendLine("    ON F.SECTION_ID = G.SECTION_ID");
            sql.AppendLine("    LEFT JOIN 試験計画_外製車日程_最終予約日 H");
            sql.AppendLine("    ON A.ID = H.CATEGORY_ID");
            sql.AppendLine("    LEFT JOIN GENERAL_CODE I");
            sql.AppendLine("    ON A.GENERAL_CODE = I.GENERAL_CODE");
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_TO_XEYE\" X");
            sql.AppendLine("    ON X.\"物品名2\" = C.\"管理票NO\"");
            sql.AppendLine("WHERE 0 = 0");

            //ID
            if (val != null && val.ID != null)
            {
                sql.AppendLine("    AND A.ID = :ID");
                prms.Add(new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = val.ID, Direct = ParameterDirection.Input });

            }

            //空車期間が入力されているかどうか
            if (val != null && val.EMPTY_START_DATE != null && val.EMPTY_END_DATE != null)
            {
                sql.AppendLine("    AND NOT EXISTS");
                sql.AppendLine("                    (");
                sql.AppendLine("                        SELECT");
                sql.AppendLine("                            *");
                sql.AppendLine("                        FROM");
                sql.AppendLine("                            OUTERCAR_SCHEDULE J");
                sql.AppendLine("                            LEFT JOIN \"試験計画_外製車日程_目的行先\" M");
                sql.AppendLine("                            ON J.ID = M.SCHEDULE_ID");
                sql.AppendLine("                        WHERE 0 = 0");
                sql.AppendLine("                            AND M.\"予約者_ID\" != :予約者_ID");
                sql.AppendLine("                            AND A.ID = J.CATEGORY_ID");
                sql.AppendLine("                            AND");
                sql.AppendLine("                                (");
                sql.AppendLine("                                    (J.START_DATE < :START_DATE AND :START_DATE < J.END_DATE)");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (J.START_DATE < :END_DATE AND :END_DATE < J.END_DATE)");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (:START_DATE < J.START_DATE AND J.START_DATE < :END_DATE)");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (:START_DATE < J.END_DATE AND J.END_DATE < :END_DATE)");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (J.START_DATE = :START_DATE AND J.END_DATE = :END_DATE)");
                sql.AppendLine("                                )");
                sql.AppendLine("                    )");

                prms.Add(new BindModel { Name = ":START_DATE", Type = OracleDbType.Date, Object = val.EMPTY_START_DATE, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":END_DATE", Type = OracleDbType.Date, Object = val.EMPTY_END_DATE, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":予約者_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input });
            }

            //Openフラグ
            switch (val.OPEN_FLG)
            {
                case true:
                    sql.AppendLine("    AND A.CLOSED_DATE IS NULL");
                    break;

                case false:
                    sql.AppendLine("    AND A.CLOSED_DATE IS NOT NULL");
                    break;
            }

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
                sql.AppendLine("    AND C.管理票NO LIKE :管理票NO");

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
                sql.AppendLine("    AND C.駐車場番号 LIKE :駐車場番号");

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
                sql.AppendLine("    AND C.所在地 = :所在地");
                prms.Add(new BindModel { Name = ":所在地", Type = OracleDbType.Varchar2, Object = val.所在地, Direct = ParameterDirection.Input });

            }

            // 車型
            if (val != null && !string.IsNullOrWhiteSpace(val.車型))
            {
                sql.AppendLine("    AND C.車型 = :車型");

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
                sql.AppendLine("    AND D.仕向地 = :仕向地");

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
                sql.AppendLine("    AND D.メーカー名 = :メーカー名");

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
                sql.AppendLine("    AND D.外製車名 LIKE :外製車名");

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
                    sql.AppendLine("    AND D.FLAG_ETC付 = 0");
                }
                else if (val.FLAG_ETC付 == 1)
                {
                    sql.AppendLine("    AND D.FLAG_ETC付 = 1");
                }
                else if (val.FLAG_ETC付 == 2)
                {
                    sql.AppendLine("    AND D.FLAG_ETC付 IN (0, 1)");
                }
            }

            // トランスミッション
            if (val != null && !string.IsNullOrWhiteSpace(val.トランスミッション))
            {
                sql.AppendLine("    AND D.トランスミッション = :トランスミッション");

                prms.Add(new BindModel
                {
                    Name = ":トランスミッション",
                    Type = OracleDbType.Varchar2,
                    Object = val.トランスミッション,
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

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    A.SORT_NO");

            return db.ReadDataTable(sql.ToString(), prms);
        }

        /// <summary>
        /// 外製車日程項目の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(OuterCarScheduleItemPostInModel val, OuterCarScheduleItemGetOutModel ret)
        {
            var common = new CommonLogic();
            common.SetDBAccess(base.db);

            // シーケンス採番
            ret.CATEGORY_ID = common.GetScheduleNewID();

            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("OUTERCAR_SCHEDULE_ITEM(");
            sql.AppendLine("    GENERAL_CODE");
            sql.AppendLine("    ,CATEGORY");
            sql.AppendLine("    ,SORT_NO");
            sql.AppendLine("    ,PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,ID");
            sql.AppendLine("    ,SECTION_GROUP_ID");
            sql.AppendLine("    ,FLAG_SEPARATOR");
            sql.AppendLine("    ,FLAG_CLASS");
            sql.AppendLine("    ,INPUT_DATETIME");
            sql.AppendLine("    ,INPUT_PERSONEL_ID");
            sql.AppendLine("    ,INPUT_LOGIN_ID");
            sql.AppendLine("    ,CATEGORY_ID");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("    :GENERAL_CODE");
            sql.AppendLine("    ,:CATEGORY");
            if (val != null && val.SORT_NO > 0)
            {
                sql.AppendLine("    ,:SORT_NO");
            }
            else
            {
                sql.AppendLine("    ,(SELECT NVL(MAX(SORT_NO), 0) + 1 FROM OUTERCAR_SCHEDULE_ITEM WHERE GENERAL_CODE = :GENERAL_CODE)");
            }
            sql.AppendLine("    ,:PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,:SCHEDULE_ID");
            sql.AppendLine("    ,:SECTION_GROUP_ID");
            sql.AppendLine("    ,1");
            sql.AppendLine("    ,'外製車日程'");
            sql.AppendLine("    ,SYSDATE");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID");
            sql.AppendLine("    ,:INPUT_LOGIN_ID");
            sql.AppendLine("    ,:CATEGORY_ID");
            sql.AppendLine(")");

            // 開発符号：必須
            prms.Add(new BindModel
            {
                Name = ":GENERAL_CODE",
                Type = OracleDbType.Varchar2,
                Object = val.GENERAL_CODE,
                Direct = ParameterDirection.Input
            });

            // カテゴリー：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY",
                Type = OracleDbType.Varchar2,
                Object = val.CATEGORY,
                Direct = ParameterDirection.Input
            });

            if (val != null && val.SORT_NO > 0)
            {
                // 並び順
                prms.Add(new BindModel
                {
                    Name = ":SORT_NO",
                    Type = OracleDbType.Decimal,
                    Object = val.SORT_NO,
                    Direct = ParameterDirection.Input
                });
            }
            else
            {
                // 開発符号：必須
                prms.Add(new BindModel
                {
                    Name = ":GENERAL_CODE",
                    Type = OracleDbType.Varchar2,
                    Object = val.GENERAL_CODE,
                    Direct = ParameterDirection.Input
                });
            }

            // 行数：必須
            prms.Add(new BindModel
            {
                Name = ":PARALLEL_INDEX_GROUP",
                Type = OracleDbType.Int32,
                Object = val.PARALLEL_INDEX_GROUP,
                Direct = ParameterDirection.Input
            });

            // スケジュールID：必須
            prms.Add(new BindModel
            {
                Name = ":SCHEDULE_ID",
                Type = OracleDbType.Int64,
                Object = ret.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });
            
            // 所属グループID：必須
            prms.Add(new BindModel
            {
                Name = ":SECTION_GROUP_ID",
                Type = OracleDbType.Varchar2,
                Object = val.SECTION_GROUP_ID,
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
                Object = ret.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            if (!common.UpdateScheduleItemSortNoByGeneralCode(3, val.GENERAL_CODE))
            {
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験計画_外製車日程_最終予約日(");
            sql.AppendLine("    ID");
            sql.AppendLine("    ,CATEGORY_ID");
            sql.AppendLine("    ,最終予約可能日");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("    (SELECT MAX(ID) + 1 FROM 試験計画_外製車日程_最終予約日)");
            sql.AppendLine("    ,:CATEGORY_ID");
            sql.AppendLine("    ,:最終予約可能日");
            sql.AppendLine(")");

            // カテゴリーID：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = ret.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            // 最終予約可能日
            prms.Add(new BindModel
            {
                Name = ":最終予約可能日",
                Type = OracleDbType.Date,
                Object = getDateTime(val.最終予約可能日),
                Direct = ParameterDirection.Input
            });

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験計画_外製車日程_車両リスト(");
            sql.AppendLine("    ID");
            sql.AppendLine("    ,CATEGORY_ID");
            sql.AppendLine("    ,管理票番号");
            sql.AppendLine("    ,FLAG_要予約許可");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("    (SELECT MAX(ID) + 1 FROM 試験計画_外製車日程_車両リスト)");
            sql.AppendLine("    ,:CATEGORY_ID");
            sql.AppendLine("    ,:管理票番号");
            sql.AppendLine("    ,:FLAG_要予約許可");
            sql.AppendLine(")");

            // カテゴリーID：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = ret.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            // 管理票番号：必須
            prms.Add(new BindModel
            {
                Name = ":管理票番号",
                Type = OracleDbType.Varchar2,
                Object = val.管理票番号,
                Direct = ParameterDirection.Input
            });

            int? flag = val.FLAG_要予約許可 != null && val.FLAG_要予約許可 == 1 ? 1 : (int?)null;
            // FLAG_要予約許可：必須
            prms.Add(new BindModel
            {
                Name = ":FLAG_要予約許可",
                Type = OracleDbType.Int32,
                Object = flag,
                Direct = ParameterDirection.Input
            });

            return db.InsertData(sql.ToString(), prms);
        }

        /// <summary>
        /// 外製車日程項目の更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(OuterCarScheduleItemPutInModel val)
        {
            var common = new CommonLogic();
            common.SetDBAccess(base.db);

            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    OUTERCAR_SCHEDULE_ITEM");
            sql.AppendLine("SET");
            sql.AppendLine("    GENERAL_CODE = :GENERAL_CODE");
            sql.AppendLine("    ,CATEGORY = :CATEGORY");
            sql.AppendLine("    ,SORT_NO = :SORT_NO");
            sql.AppendLine("    ,PARALLEL_INDEX_GROUP = :PARALLEL_INDEX_GROUP");
            sql.AppendLine("    ,CHANGE_DATETIME = SYSDATE");
            sql.AppendLine("    ,INPUT_PERSONEL_ID = :INPUT_PERSONEL_ID");
            sql.AppendLine("    ,INPUT_LOGIN_ID = :INPUT_LOGIN_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            // カテゴリーID：必須
            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            // 開発符号：必須
            prms.Add(new BindModel
            {
                Name = ":GENERAL_CODE",
                Type = OracleDbType.Varchar2,
                Object = val.GENERAL_CODE,
                Direct = ParameterDirection.Input
            });
            
            // カテゴリー：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY",
                Type = OracleDbType.Varchar2,
                Object = val.CATEGORY,
                Direct = ParameterDirection.Input
            });

            // 並び順：必須
            prms.Add(new BindModel
            {
                Name = ":SORT_NO",
                Type = OracleDbType.Decimal,
                Object = val.SORT_NO,
                Direct = ParameterDirection.Input
            });

            // 行数：必須
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

            if (!db.UpdateData(sql.ToString(), prms))
            {
                return false;
            }

            if ((Convert.ToDecimal(val.SORT_NO) - Math.Floor(Convert.ToDecimal(val.SORT_NO)) != 0))
            {
                if (!common.UpdateScheduleItemSortNoByGeneralCode(3, val.GENERAL_CODE)) return false;
            }
            
            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    試験計画_外製車日程_最終予約日");
            sql.AppendLine("SET");
            sql.AppendLine("    最終予約可能日 = :最終予約可能日");
            sql.AppendLine("WHERE");
            sql.AppendLine("    CATEGORY_ID = :CATEGORY_ID");

            // カテゴリーID：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            // 最終予約可能日：必須
            prms.Add(new BindModel
            {
                Name = ":最終予約可能日",
                Type = OracleDbType.Date,
                Object = getDateTime(val.最終予約可能日),
                Direct = ParameterDirection.Input
            });

            if (!db.UpdateData(sql.ToString(), prms))
            {
                return false;
            }

            prms = new List<BindModel>();
            sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    試験計画_外製車日程_車両リスト");
            sql.AppendLine("SET");
            sql.AppendLine("    管理票番号 = :管理票番号");
            sql.AppendLine("    ,FLAG_要予約許可 = :FLAG_要予約許可");
            sql.AppendLine("WHERE");
            sql.AppendLine("    CATEGORY_ID = :CATEGORY_ID");

            // カテゴリーID：必須
            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            // 管理票番号
            prms.Add(new BindModel
            {
                Name = ":管理票番号",
                Type = OracleDbType.Varchar2,
                Object = val.管理票番号,
                Direct = ParameterDirection.Input
            });

            int? flag = val.FLAG_要予約許可 != null && val.FLAG_要予約許可 == 1 ? 1 : (int?)null;
            // FLAG_要予約許可：必須
            prms.Add(new BindModel
            {
                Name = ":FLAG_要予約許可",
                Type = OracleDbType.Int32,
                Object = flag,
                Direct = ParameterDirection.Input
            });

            return db.UpdateData(sql.ToString(), prms);
        }

        /// <summary>
        /// 外製車日程項目の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(OuterCarScheduleItemDeleteInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    OUTERCAR_SCHEDULE_ITEM");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            prms.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
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
            sql.AppendLine("    OUTERCAR_SCHEDULE");
            sql.AppendLine("WHERE");
            sql.AppendLine("    CATEGORY_ID = :CATEGORY_ID");

            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
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
            sql.AppendLine("    試験計画_外製車日程_最終予約日");
            sql.AppendLine("WHERE");
            sql.AppendLine("    CATEGORY_ID = :CATEGORY_ID");

            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
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
            sql.AppendLine("    試験計画_外製車日程_車両リスト");
            sql.AppendLine("WHERE");
            sql.AppendLine("    CATEGORY_ID = :CATEGORY_ID");

            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
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
            sql.AppendLine("    SCHEDULE_CAR");
            sql.AppendLine("WHERE");
            sql.AppendLine("    CATEGORY_ID = :CATEGORY_ID");

            prms.Add(new BindModel
            {
                Name = ":CATEGORY_ID",
                Type = OracleDbType.Int64,
                Object = val.CATEGORY_ID,
                Direct = ParameterDirection.Input
            });

            return db.DeleteData(sql.ToString(), prms);
        }
    }
}