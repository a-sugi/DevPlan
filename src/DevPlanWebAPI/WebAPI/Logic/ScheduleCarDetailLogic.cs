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
    /// <remarks>試験車日程・カーシェア日程・外製車日程</remarks>
    public class ScheduleCarDetailLogic : BaseLogic
    {
        #region スケジュール利用車データの取得
        /// <summary>
        /// スケジュール利用車データの取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<ScheduleCarDetailModel> Get(ScheduleCarDetailSearchModel val)
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    CATEGORY_ID");
            sql.AppendLine("   ,CAR_CLASS");
            sql.AppendLine("   ,管理票番号");
            sql.AppendLine("   ,車型");
            sql.AppendLine("   ,型式符号");
            sql.AppendLine("   ,メーカー名");
            sql.AppendLine("   ,外製車名");
            sql.AppendLine("   ,型式符号");
            sql.AppendLine("   ,駐車場番号");
            sql.AppendLine("   ,リースNO");
            sql.AppendLine("   ,開発符号");
            sql.AppendLine("   ,試作時期");
            sql.AppendLine("   ,号車");
            sql.AppendLine("   ,仕向地");
            sql.AppendLine("   ,E_G型式");
            sql.AppendLine("   ,排気量");
            sql.AppendLine("   ,トランスミッション");
            sql.AppendLine("   ,駆動方式");
            sql.AppendLine("   ,グレード");
            sql.AppendLine("   ,車体色");
            sql.AppendLine("   ,固定資産NO");
            sql.AppendLine("   ,リース満了日");
            sql.AppendLine("   ,処分予定年月");
            sql.AppendLine("   ,登録ナンバー");
            sql.AppendLine("   ,FLAG_ナビ付");
            sql.AppendLine("   ,FLAG_ETC付");
            sql.AppendLine("   ,備考");
            sql.AppendLine("   ,INPUT_DEPARTMENT_ID");
            sql.AppendLine("   ,INPUT_SECTION_ID");
            sql.AppendLine("   ,INPUT_SECTION_GROUP_ID");
            sql.AppendLine("FROM");
            sql.AppendLine("    SCHEDULE_CAR");
            sql.AppendLine("WHERE 0 = 0");

            if (val.CATEGORY_ID != null)
            {
                sql.AppendLine("AND CATEGORY_ID = :CATEGORY_ID");
            }

            if (val.管理票番号 != null)
            {
                sql.AppendLine("AND 管理票番号 = :管理票番号");
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    CATEGORY_ID");

            var prms = new List<BindModel>()
            {
                new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = val.CATEGORY_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理票番号", Type = OracleDbType.Varchar2, Object = val.管理票番号, Direct = ParameterDirection.Input },
            };

            return db.ReadModelList<ScheduleCarDetailModel>(sql.ToString(), prms);
        }
        #endregion

        #region スケジュール利用車データの作成
        /// <summary>
        /// スケジュール利用車データの作成
        /// </summary>
        /// <param name="val"></param>
        /// <returns>bool</returns>
        public bool Post(ScheduleCarDetailModel val)
        {
            var sql = new StringBuilder();

            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    SCHEDULE_CAR SINK");
            sql.AppendLine("USING(");
            sql.AppendLine("    SELECT");
            sql.AppendLine("        :CATEGORY_ID CATEGORY_ID");
            sql.AppendLine("    FROM");
            sql.AppendLine("        DUAL");
            sql.AppendLine(") SRC");
            sql.AppendLine("ON(");
            sql.AppendLine("    SINK.CATEGORY_ID = SRC.CATEGORY_ID");
            sql.AppendLine(")");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("        CAR_CLASS = :CAR_CLASS");
            sql.AppendLine("       ,管理票番号 = :管理票番号");
            sql.AppendLine("       ,車型 = :車型");
            sql.AppendLine("       ,型式符号 = :型式符号");
            sql.AppendLine("       ,メーカー名 = :メーカー名");
            sql.AppendLine("       ,外製車名 = :外製車名");
            sql.AppendLine("       ,駐車場番号 = :駐車場番号");
            sql.AppendLine("       ,リースNO = :リースNO");
            sql.AppendLine("       ,開発符号 = :開発符号");
            sql.AppendLine("       ,試作時期 = :試作時期");
            sql.AppendLine("       ,号車 = :号車");
            sql.AppendLine("       ,仕向地 = :仕向地");
            sql.AppendLine("       ,E_G型式 = :E_G型式");
            sql.AppendLine("       ,排気量 = :排気量");
            sql.AppendLine("       ,トランスミッション = :トランスミッション");
            sql.AppendLine("       ,駆動方式 = :駆動方式");
            sql.AppendLine("       ,グレード = :グレード");
            sql.AppendLine("       ,車体色 = :車体色");
            sql.AppendLine("       ,固定資産NO = :固定資産NO");
            sql.AppendLine("       ,処分予定年月 = :処分予定年月");
            sql.AppendLine("       ,リース満了日 = :リース満了日");
            sql.AppendLine("       ,登録ナンバー = :登録ナンバー");
            sql.AppendLine("       ,FLAG_ナビ付 = :FLAG_ナビ付");
            sql.AppendLine("       ,FLAG_ETC付 = :FLAG_ETC付");
            sql.AppendLine("       ,備考 = :備考");
            sql.AppendLine("       ,CHANGE_DATETIME = SYSDATE");
            sql.AppendLine("       ,CHANGE_PERSONEL_ID = :PERSONEL_ID");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT (");
            sql.AppendLine("        CATEGORY_ID");
            sql.AppendLine("       ,CAR_CLASS");
            sql.AppendLine("       ,管理票番号");
            sql.AppendLine("       ,車型");
            sql.AppendLine("       ,型式符号");
            sql.AppendLine("       ,メーカー名");
            sql.AppendLine("       ,外製車名");
            sql.AppendLine("       ,駐車場番号");
            sql.AppendLine("       ,リースNO");
            sql.AppendLine("       ,開発符号");
            sql.AppendLine("       ,試作時期");
            sql.AppendLine("       ,号車");
            sql.AppendLine("       ,仕向地");
            sql.AppendLine("       ,E_G型式");
            sql.AppendLine("       ,排気量");
            sql.AppendLine("       ,トランスミッション");
            sql.AppendLine("       ,駆動方式");
            sql.AppendLine("       ,グレード");
            sql.AppendLine("       ,車体色");
            sql.AppendLine("       ,固定資産NO");
            sql.AppendLine("       ,登録ナンバー");
            sql.AppendLine("       ,処分予定年月");
            sql.AppendLine("       ,リース満了日");
            sql.AppendLine("       ,FLAG_ナビ付");
            sql.AppendLine("       ,FLAG_ETC付");
            sql.AppendLine("       ,備考");
            sql.AppendLine("       ,INPUT_DEPARTMENT_ID");
            sql.AppendLine("       ,INPUT_SECTION_ID");
            sql.AppendLine("       ,INPUT_SECTION_GROUP_ID");
            sql.AppendLine("       ,INPUT_DATETIME");
            sql.AppendLine("       ,INPUT_PERSONEL_ID");
            sql.AppendLine(")");
            sql.AppendLine("VALUES (");
            sql.AppendLine("        :CATEGORY_ID");
            sql.AppendLine("       ,:CAR_CLASS");
            sql.AppendLine("       ,:管理票番号");
            sql.AppendLine("       ,:車型");
            sql.AppendLine("       ,:型式符号");
            sql.AppendLine("       ,:メーカー名");
            sql.AppendLine("       ,:外製車名");
            sql.AppendLine("       ,:駐車場番号");
            sql.AppendLine("       ,:リースNO");
            sql.AppendLine("       ,:開発符号");
            sql.AppendLine("       ,:試作時期");
            sql.AppendLine("       ,:号車");
            sql.AppendLine("       ,:仕向地");
            sql.AppendLine("       ,:E_G型式");
            sql.AppendLine("       ,:排気量");
            sql.AppendLine("       ,:トランスミッション");
            sql.AppendLine("       ,:駆動方式");
            sql.AppendLine("       ,:グレード");
            sql.AppendLine("       ,:車体色");
            sql.AppendLine("       ,:固定資産NO");
            sql.AppendLine("       ,:登録ナンバー");
            sql.AppendLine("       ,:処分予定年月");
            sql.AppendLine("       ,:リース満了日");
            sql.AppendLine("       ,:FLAG_ナビ付");
            sql.AppendLine("       ,:FLAG_ETC付");
            sql.AppendLine("       ,:備考");
            sql.AppendLine("       ,:INPUT_DEPARTMENT_ID");
            sql.AppendLine("       ,:INPUT_SECTION_ID");
            sql.AppendLine("       ,:INPUT_SECTION_GROUP_ID");
            sql.AppendLine("       ,SYSDATE");
            sql.AppendLine("       ,:PERSONEL_ID");
            sql.AppendLine(")");

            // トランザクション開始
            db.Begin();

            //パラメータ設定
            var prms = new List<BindModel>()
            {
                new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = val.CATEGORY_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":CAR_CLASS", Type = OracleDbType.Int16, Object = val.CAR_CLASS, Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理票番号", Type = OracleDbType.Varchar2, Object = val.管理票番号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車型", Type = OracleDbType.Varchar2, Object = val.車型, Direct = ParameterDirection.Input },
                new BindModel { Name = ":型式符号", Type = OracleDbType.Varchar2, Object = val.型式符号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":メーカー名", Type = OracleDbType.Varchar2, Object = val.メーカー名, Direct = ParameterDirection.Input },
                new BindModel { Name = ":外製車名", Type = OracleDbType.Varchar2, Object = val.外製車名, Direct = ParameterDirection.Input },
                new BindModel { Name = ":駐車場番号", Type = OracleDbType.Varchar2, Object = val.駐車場番号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":リースNO", Type = OracleDbType.Varchar2, Object = val.リースNO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":開発符号", Type = OracleDbType.Varchar2, Object = val.開発符号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":試作時期", Type = OracleDbType.Varchar2, Object = val.試作時期, Direct = ParameterDirection.Input },
                new BindModel { Name = ":号車", Type = OracleDbType.Varchar2, Object = val.号車, Direct = ParameterDirection.Input },
                new BindModel { Name = ":仕向地", Type = OracleDbType.Varchar2, Object = val.仕向地, Direct = ParameterDirection.Input },
                new BindModel { Name = ":E_G型式", Type = OracleDbType.Varchar2, Object = val.E_G型式, Direct = ParameterDirection.Input },
                new BindModel { Name = ":排気量", Type = OracleDbType.Varchar2, Object = val.排気量, Direct = ParameterDirection.Input },
                new BindModel { Name = ":トランスミッション", Type = OracleDbType.Varchar2, Object = val.トランスミッション, Direct = ParameterDirection.Input },
                new BindModel { Name = ":駆動方式", Type = OracleDbType.Varchar2, Object = val.駆動方式, Direct = ParameterDirection.Input },
                new BindModel { Name = ":グレード", Type = OracleDbType.Varchar2, Object = val.グレード, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車体色", Type = OracleDbType.Varchar2, Object = val.車体色, Direct = ParameterDirection.Input },
                new BindModel { Name = ":固定資産NO", Type = OracleDbType.Varchar2, Object = val.固定資産NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":処分予定年月", Type = OracleDbType.Varchar2, Object = val.処分予定年月, Direct = ParameterDirection.Input },
                new BindModel { Name = ":リース満了日", Type = OracleDbType.Varchar2, Object = val.リース満了日, Direct = ParameterDirection.Input },
                new BindModel { Name = ":登録ナンバー", Type = OracleDbType.Varchar2, Object = val.登録ナンバー, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAG_ナビ付", Type = OracleDbType.Int16, Object = val.FLAG_ナビ付, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAG_ETC付", Type = OracleDbType.Int16, Object = val.FLAG_ETC付, Direct = ParameterDirection.Input },
                new BindModel { Name = ":INPUT_DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_DEPARTMENT_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":INPUT_SECTION_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_SECTION_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":INPUT_SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":備考", Type = OracleDbType.Varchar2, Object = val.備考, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input }
            };

            if (!db.InsertData(sql.ToString(), prms))
            {
                db.Rollback();
                return false;
            }

            //コミット
            db.Commit();

            return true;
        }
        #endregion

        #region スケジュール利用車データの更新（管理移譲）
        /// <summary>
        /// スケジュール利用車データの更新（管理移譲）
        /// </summary>
        /// <returns>bool</returns>
        public bool Put(ScheduleCarDetailModel val)
        {
            var sql_trans = new StringBuilder();

            sql_trans.AppendLine("UPDATE");
            sql_trans.AppendLine("    SCHEDULE_CAR");
            sql_trans.AppendLine("SET");
            sql_trans.AppendLine("    INPUT_DEPARTMENT_ID = :INPUT_DEPARTMENT_ID");
            sql_trans.AppendLine("   ,INPUT_SECTION_ID = :INPUT_SECTION_ID");
            sql_trans.AppendLine("   ,INPUT_SECTION_GROUP_ID = :INPUT_SECTION_GROUP_ID");
            sql_trans.AppendLine("   ,CHANGE_DATETIME = SYSDATE");
            sql_trans.AppendLine("   ,CHANGE_PERSONEL_ID = :PERSONEL_ID");
            sql_trans.AppendLine("WHERE");
            sql_trans.AppendLine("    CATEGORY_ID = :CATEGORY_ID");

            var sql_init = new StringBuilder();

            sql_init.AppendLine("UPDATE");
            sql_init.AppendLine("    試験計画_外製車日程_車両リスト");
            sql_init.AppendLine("SET");
            sql_init.AppendLine("    FLAG_要予約許可 = NULL");
            sql_init.AppendLine("WHERE");
            sql_init.AppendLine("    CATEGORY_ID = :CATEGORY_ID");

            //パラメータ設定
            var prms = new List<BindModel>()
            {
                new BindModel { Name = ":CATEGORY_ID", Type = OracleDbType.Int64, Object = val.CATEGORY_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":INPUT_DEPARTMENT_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_DEPARTMENT_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":INPUT_SECTION_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_SECTION_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":INPUT_SECTION_GROUP_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_SECTION_GROUP_ID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input }
            };
            
            // トランザクション開始
            db.Begin();

            if (!db.UpdateData(sql_trans.ToString(), prms))
            {
                db.Rollback();
                return false;
            }

            if (!db.UpdateData(sql_init.ToString(), prms))
            {
                db.Rollback();
                return false;
            }

            //コミット
            db.Commit();

            return true;
        }
        #endregion
    }
}