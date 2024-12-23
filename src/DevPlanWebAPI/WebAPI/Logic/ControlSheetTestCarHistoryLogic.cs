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
    /// 試験車履歴情報(管理票)ロジッククラス
    /// </summary>
    /// <remarks>試験車履歴情報(管理票)の操作</remarks>
    public class ControlSheetTestCarHistoryLogic : TestCarBaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 試験車履歴情報(管理票)の取得
        /// </summary>
        /// <returns>IEnumerable</returns>
        public IEnumerable<ControlSheetTestCarHistoryGetOutModel> GetData(ControlSheetTestCarHistoryGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     KR.データID");
            sql.AppendLine("    ,KR.履歴NO");
            sql.AppendLine("    ,KR.管理票発行有無");
            sql.AppendLine("    ,KR.発行年月日");
            sql.AppendLine("    ,KR.開発符号");
            sql.AppendLine("    ,KR.試作時期");
            sql.AppendLine("    ,KR.号車");
            sql.AppendLine("    ,KR.仕向地");
            sql.AppendLine("    ,KR.メーカー名");
            sql.AppendLine("    ,KR.外製車名");
            sql.AppendLine("    ,KR.名称備考");
            sql.AppendLine("    ,KR.車体番号");
            sql.AppendLine("    ,KR.E_G番号");
            sql.AppendLine("    ,KR.E_G型式");
            sql.AppendLine("    ,KR.排気量");
            sql.AppendLine("    ,KR.トランスミッション");
            sql.AppendLine("    ,KR.駆動方式");
            sql.AppendLine("    ,KR.グレード");
            sql.AppendLine("    ,KR.車体色");
            sql.AppendLine("    ,KR.試験目的");
            sql.AppendLine("    ,KR.受領日");
            sql.AppendLine("    ,KR.受領部署");
            sql.AppendLine("    ,JG.SECTION_GROUP_NAME 受領部署名");
            sql.AppendLine("    ,JG.SECTION_GROUP_CODE 受領部署コード");
            sql.AppendLine("    ,KR.受領者");
            sql.AppendLine("    ,PL.NAME 受領者名");
            sql.AppendLine("    ,KR.受領時走行距離");
            sql.AppendLine("    ,KR.完成日");
            sql.AppendLine("    ,KR.管理責任部署");
            sql.AppendLine("    ,SG.SECTION_GROUP_NAME 管理責任部署名");
            sql.AppendLine("    ,SG.SECTION_GROUP_CODE 管理責任部署コード");
            sql.AppendLine("    ,KR.研命ナンバー");
            sql.AppendLine("    ,KR.研命期間");
            sql.AppendLine("    ,KR.固定資産NO");
            sql.AppendLine("    ,KR.登録ナンバー");
            sql.AppendLine("    ,KR.車検登録日");
            sql.AppendLine("    ,KR.車検期限");
            sql.AppendLine("    ,KR.廃艦日");
            sql.AppendLine("    ,KR.保険NO");
            sql.AppendLine("    ,KR.保険加入日");
            sql.AppendLine("    ,KR.保険解約日");
            sql.AppendLine("    ,KR.保険料");
            sql.AppendLine("    ,KR.自動車税");
            sql.AppendLine("    ,KR.移管依頼NO");
            sql.AppendLine("    ,KR.三鷹移管先部署");
            sql.AppendLine("    ,KR.三鷹移管年月");
            sql.AppendLine("    ,KR.三鷹移管先固資NO");
            sql.AppendLine("    ,KR.試験着手日");
            sql.AppendLine("    ,KR.試験着手証明文書");
            sql.AppendLine("    ,KR.工事区分NO");
            sql.AppendLine("    ,KR.FLAG_中古");
            sql.AppendLine("    ,KR.FLAG_ナビ付");
            sql.AppendLine("    ,KR.FLAG_ETC付");
            sql.AppendLine("    ,KR.EVデバイス");
            sql.AppendLine("    ,KR.初年度登録年月");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験車履歴情報 KR");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA SG");
            sql.AppendLine("    ON KR.管理責任部署 = SG.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN SECTION_GROUP_DATA JG");
            sql.AppendLine("    ON KR.受領部署 = JG.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN PERSONEL_LIST PL");
            sql.AppendLine("    ON KR.受領者 = PL.PERSONEL_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // データID：必須
            sql.AppendLine("    AND KR.データID = :データID");

            prms.Add(new BindModel
            {
                Name = ":データID",
                Type = OracleDbType.Int32,
                Object = val.データID,
                Direct = ParameterDirection.Input
            });

            // 履歴NO
            if (val?.履歴NO > 0)
            {
                sql.AppendLine("    AND KR.履歴NO = :履歴NO");

                prms.Add(new BindModel
                {
                    Name = ":履歴NO",
                    Type = OracleDbType.Int32,
                    Object = val.履歴NO,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     データID ASC");
            sql.AppendLine("    ,履歴NO ASC");

            return db.ReadModelList<ControlSheetTestCarHistoryGetOutModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// 試験車履歴情報(管理票)の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(ControlSheetTestCarHistoryPostInModel val, TestCarCommonModel returns = null)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験車履歴情報 (");
            sql.AppendLine("     データID");
            sql.AppendLine("    ,履歴NO");
            sql.AppendLine("    ,管理票発行有無");
            sql.AppendLine("    ,発行年月日");
            sql.AppendLine("    ,開発符号");
            sql.AppendLine("    ,試作時期");
            sql.AppendLine("    ,号車");
            sql.AppendLine("    ,仕向地");
            sql.AppendLine("    ,メーカー名");
            sql.AppendLine("    ,外製車名");
            sql.AppendLine("    ,名称備考");
            sql.AppendLine("    ,車体番号");
            sql.AppendLine("    ,E_G番号");
            sql.AppendLine("    ,E_G型式");
            sql.AppendLine("    ,排気量");
            sql.AppendLine("    ,トランスミッション");
            sql.AppendLine("    ,駆動方式");
            sql.AppendLine("    ,グレード");
            sql.AppendLine("    ,車体色");
            sql.AppendLine("    ,試験目的");
            sql.AppendLine("    ,受領日");
            sql.AppendLine("    ,受領部署");
            sql.AppendLine("    ,受領者");
            sql.AppendLine("    ,受領時走行距離");
            sql.AppendLine("    ,完成日");
            sql.AppendLine("    ,管理責任部署");
            sql.AppendLine("    ,研命ナンバー");
            sql.AppendLine("    ,研命期間");
            sql.AppendLine("    ,固定資産NO");
            sql.AppendLine("    ,登録ナンバー");
            sql.AppendLine("    ,車検登録日");
            sql.AppendLine("    ,車検期限");
            sql.AppendLine("    ,廃艦日");
            sql.AppendLine("    ,保険NO");
            sql.AppendLine("    ,保険加入日");
            sql.AppendLine("    ,保険解約日");
            sql.AppendLine("    ,移管依頼NO");
            sql.AppendLine("    ,試験着手日");
            sql.AppendLine("    ,試験着手証明文書");
            sql.AppendLine("    ,工事区分NO");
            sql.AppendLine("    ,FLAG_中古");
            sql.AppendLine("    ,FLAG_ナビ付");
            sql.AppendLine("    ,FLAG_ETC付");
            sql.AppendLine("    ,EVデバイス");
            sql.AppendLine("    ,初年度登録年月");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     :データID");
            sql.AppendLine("    ,(SELECT NVL(MAX(履歴NO), 0) + 1 FROM 試験車履歴情報 WHERE データID = :データID)");
            sql.AppendLine("    ,:管理票発行有無");
            sql.AppendLine("    ,:発行年月日");
            sql.AppendLine("    ,:開発符号");
            sql.AppendLine("    ,:試作時期");
            sql.AppendLine("    ,:号車");
            sql.AppendLine("    ,:仕向地");
            sql.AppendLine("    ,:メーカー名");
            sql.AppendLine("    ,:外製車名");
            sql.AppendLine("    ,:名称備考");
            sql.AppendLine("    ,:車体番号");
            sql.AppendLine("    ,:E_G番号");
            sql.AppendLine("    ,:E_G型式");
            sql.AppendLine("    ,:排気量");
            sql.AppendLine("    ,:トランスミッション");
            sql.AppendLine("    ,:駆動方式");
            sql.AppendLine("    ,:グレード");
            sql.AppendLine("    ,:車体色");
            sql.AppendLine("    ,:試験目的");
            sql.AppendLine("    ,:受領日");
            sql.AppendLine("    ,:受領部署");
            sql.AppendLine("    ,:受領者");
            sql.AppendLine("    ,:受領時走行距離");
            sql.AppendLine("    ,:完成日");
            sql.AppendLine("    ,:管理責任部署");
            sql.AppendLine("    ,:研命ナンバー");
            sql.AppendLine("    ,:研命期間");
            sql.AppendLine("    ,:固定資産NO");
            sql.AppendLine("    ,:登録ナンバー");
            sql.AppendLine("    ,:車検登録日");
            sql.AppendLine("    ,:車検期限");
            sql.AppendLine("    ,:廃艦日");
            sql.AppendLine("    ,:保険NO");
            sql.AppendLine("    ,:保険加入日");
            sql.AppendLine("    ,:保険解約日");
            sql.AppendLine("    ,:移管依頼NO");
            sql.AppendLine("    ,:試験着手日");
            sql.AppendLine("    ,:試験着手証明文書");
            sql.AppendLine("    ,:工事区分NO");
            sql.AppendLine("    ,:FLAG_中古");
            sql.AppendLine("    ,:FLAG_ナビ付");
            sql.AppendLine("    ,:FLAG_ETC付");
            sql.AppendLine("    ,:EVデバイス");
            sql.AppendLine("    ,:初年度登録年月");
            sql.AppendLine(") RETURNING");
            sql.AppendLine("    履歴NO INTO :newno");

            List<BindModel> prms = new List<BindModel>()
            {
                new BindModel { Name = ":データID", Type = OracleDbType.Int16, Object = val.データID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理票発行有無", Type = OracleDbType.Varchar2, Object = val.管理票発行有無, Direct = ParameterDirection.Input },
                new BindModel { Name = ":発行年月日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.発行年月日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":開発符号", Type = OracleDbType.Varchar2, Object = val.開発符号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":試作時期", Type = OracleDbType.Varchar2, Object = val.試作時期, Direct = ParameterDirection.Input },
                new BindModel { Name = ":号車", Type = OracleDbType.Varchar2, Object = val.号車, Direct = ParameterDirection.Input },
                new BindModel { Name = ":仕向地", Type = OracleDbType.Varchar2, Object = val.仕向地, Direct = ParameterDirection.Input },
                new BindModel { Name = ":メーカー名", Type = OracleDbType.Varchar2, Object = val.メーカー名, Direct = ParameterDirection.Input },
                new BindModel { Name = ":外製車名", Type = OracleDbType.Varchar2, Object = val.外製車名, Direct = ParameterDirection.Input },
                new BindModel { Name = ":名称備考", Type = OracleDbType.Varchar2, Object = val.名称備考, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車体番号", Type = OracleDbType.Varchar2, Object = val.車体番号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":E_G番号", Type = OracleDbType.Varchar2, Object = val.E_G番号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":E_G型式", Type = OracleDbType.Varchar2, Object = val.E_G型式, Direct = ParameterDirection.Input },
                new BindModel { Name = ":排気量", Type = OracleDbType.Varchar2, Object = val.排気量, Direct = ParameterDirection.Input },
                new BindModel { Name = ":トランスミッション", Type = OracleDbType.Varchar2, Object = val.トランスミッション, Direct = ParameterDirection.Input },
                new BindModel { Name = ":駆動方式", Type = OracleDbType.Varchar2, Object = val.駆動方式, Direct = ParameterDirection.Input },
                new BindModel { Name = ":グレード", Type = OracleDbType.Varchar2, Object = val.グレード, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車体色", Type = OracleDbType.Varchar2, Object = val.車体色, Direct = ParameterDirection.Input },
                new BindModel { Name = ":試験目的", Type = OracleDbType.Varchar2, Object = val.試験目的, Direct = ParameterDirection.Input },
                new BindModel { Name = ":受領日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.受領日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":受領部署", Type = OracleDbType.Varchar2, Object = val.受領部署, Direct = ParameterDirection.Input },
                new BindModel { Name = ":受領者", Type = OracleDbType.Varchar2, Object = val.受領者, Direct = ParameterDirection.Input },
                new BindModel { Name = ":受領時走行距離", Type = OracleDbType.Varchar2, Object = val.受領時走行距離, Direct = ParameterDirection.Input },
                new BindModel { Name = ":完成日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.完成日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理責任部署", Type = OracleDbType.Varchar2, Object = val.管理責任部署, Direct = ParameterDirection.Input },
                new BindModel { Name = ":研命ナンバー", Type = OracleDbType.Varchar2, Object = val.研命ナンバー, Direct = ParameterDirection.Input },
                new BindModel { Name = ":研命期間", Type = OracleDbType.Date, Object = DateTimeUtil.GetLastDate(val.研命期間), Direct = ParameterDirection.Input },
                new BindModel { Name = ":固定資産NO", Type = OracleDbType.Varchar2, Object = val.固定資産NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":登録ナンバー", Type = OracleDbType.Varchar2, Object = val.登録ナンバー, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車検登録日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車検登録日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":車検期限", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車検期限), Direct = ParameterDirection.Input },
                new BindModel { Name = ":廃艦日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.廃艦日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":保険NO", Type = OracleDbType.Varchar2, Object = val.保険NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":保険加入日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.保険加入日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":保険解約日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.保険解約日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":移管依頼NO", Type = OracleDbType.Varchar2, Object = val.移管依頼NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":試験着手日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.試験着手日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":試験着手証明文書", Type = OracleDbType.Varchar2, Object = val.試験着手証明文書, Direct = ParameterDirection.Input },
                new BindModel { Name = ":工事区分NO", Type = OracleDbType.Varchar2, Object = val.工事区分NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAG_中古", Type = OracleDbType.Int16, Object = val.FLAG_中古, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAG_ナビ付", Type = OracleDbType.Int16, Object = val.FLAG_ナビ付, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAG_ETC付", Type = OracleDbType.Int16, Object = val.FLAG_ETC付, Direct = ParameterDirection.Input },
                new BindModel { Name = ":EVデバイス", Type = OracleDbType.Varchar2, Object = val.EVデバイス, Direct = ParameterDirection.Input },
                new BindModel { Name = ":初年度登録年月", Type = OracleDbType.Date, Object = DateTimeUtil.GetLastDate(val.初年度登録年月), Direct = ParameterDirection.Input }
            };

            // 戻り値設定
            db.Returns = new List<BindModel>()
            {
                new BindModel { Name = ":newno", Type = OracleDbType.Int16, Direct = ParameterDirection.Output }
            };

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            // 戻り値の格納
            if(returns != null)
            {
                returns.履歴NO = Convert.ToInt16(db.Returns.Where(r => r.Name == ":newno").FirstOrDefault().Object.ToString());
            }
            
            return true;
        }

        /// <summary>
        /// 試験車履歴情報(管理票)の更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(List<ControlSheetTestCarHistoryPutInModel> list)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    試験車履歴情報");
            sql.AppendLine("SET");
            sql.AppendLine("     管理票発行有無 = :管理票発行有無");
            sql.AppendLine("    ,発行年月日 = :発行年月日");
            sql.AppendLine("    ,開発符号 = :開発符号");
            sql.AppendLine("    ,試作時期 = :試作時期");
            sql.AppendLine("    ,号車 = :号車");
            sql.AppendLine("    ,仕向地 = :仕向地");
            sql.AppendLine("    ,メーカー名 = :メーカー名");
            sql.AppendLine("    ,外製車名 = :外製車名");
            sql.AppendLine("    ,名称備考 = :名称備考");
            sql.AppendLine("    ,車体番号 = :車体番号");
            sql.AppendLine("    ,E_G番号 = :E_G番号");
            sql.AppendLine("    ,E_G型式 = :E_G型式");
            sql.AppendLine("    ,排気量 = :排気量");
            sql.AppendLine("    ,トランスミッション = :トランスミッション");
            sql.AppendLine("    ,駆動方式 = :駆動方式");
            sql.AppendLine("    ,グレード = :グレード");
            sql.AppendLine("    ,車体色 = :車体色");
            sql.AppendLine("    ,試験目的 = :試験目的");
            sql.AppendLine("    ,受領日 = :受領日");
            sql.AppendLine("    ,受領部署 = :受領部署");
            sql.AppendLine("    ,受領者 = :受領者");
            sql.AppendLine("    ,受領時走行距離 = :受領時走行距離");
            sql.AppendLine("    ,完成日 = :完成日");
            sql.AppendLine("    ,管理責任部署 = :管理責任部署");
            sql.AppendLine("    ,研命ナンバー = :研命ナンバー");
            sql.AppendLine("    ,研命期間 = :研命期間");
            sql.AppendLine("    ,固定資産NO = :固定資産NO");
            sql.AppendLine("    ,登録ナンバー = :登録ナンバー");
            sql.AppendLine("    ,車検登録日 = :車検登録日");
            sql.AppendLine("    ,車検期限 = :車検期限");
            sql.AppendLine("    ,廃艦日 = :廃艦日");
            sql.AppendLine("    ,保険NO = :保険NO");
            sql.AppendLine("    ,保険加入日 = :保険加入日");
            sql.AppendLine("    ,保険解約日 = :保険解約日");
            sql.AppendLine("    ,移管依頼NO = :移管依頼NO");
            sql.AppendLine("    ,試験着手日 = :試験着手日");
            sql.AppendLine("    ,試験着手証明文書 = :試験着手証明文書");
            sql.AppendLine("    ,工事区分NO = :工事区分NO");
            sql.AppendLine("    ,FLAG_中古 = :FLAG_中古");
            sql.AppendLine("    ,FLAG_ナビ付 = :FLAG_ナビ付");
            sql.AppendLine("    ,FLAG_ETC付 = :FLAG_ETC付");
            sql.AppendLine("    ,EVデバイス = :EVデバイス");
            sql.AppendLine("    ,初年度登録年月 = :初年度登録年月");
            sql.AppendLine("WHERE");
            sql.AppendLine("    データID = :データID");
            sql.AppendLine("    AND 履歴NO = :履歴NO");

            foreach (var val in list)
            {
                List<BindModel> prms = new List<BindModel>()
                {
                    new BindModel { Name = ":データID", Type = OracleDbType.Int16, Object = val.データID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":履歴NO", Type = OracleDbType.Int16, Object = val.履歴NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理票発行有無", Type = OracleDbType.Varchar2, Object = val.管理票発行有無, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":発行年月日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.発行年月日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":開発符号", Type = OracleDbType.Varchar2, Object = val.開発符号, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試作時期", Type = OracleDbType.Varchar2, Object = val.試作時期, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":号車", Type = OracleDbType.Varchar2, Object = val.号車, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":仕向地", Type = OracleDbType.Varchar2, Object = val.仕向地, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":メーカー名", Type = OracleDbType.Varchar2, Object = val.メーカー名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":外製車名", Type = OracleDbType.Varchar2, Object = val.外製車名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":名称備考", Type = OracleDbType.Varchar2, Object = val.名称備考, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":車体番号", Type = OracleDbType.Varchar2, Object = val.車体番号, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":E_G番号", Type = OracleDbType.Varchar2, Object = val.E_G番号, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":E_G型式", Type = OracleDbType.Varchar2, Object = val.E_G型式, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":排気量", Type = OracleDbType.Varchar2, Object = val.排気量, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":トランスミッション", Type = OracleDbType.Varchar2, Object = val.トランスミッション, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":駆動方式", Type = OracleDbType.Varchar2, Object = val.駆動方式, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":グレード", Type = OracleDbType.Varchar2, Object = val.グレード, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":車体色", Type = OracleDbType.Varchar2, Object = val.車体色, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験目的", Type = OracleDbType.Varchar2, Object = val.試験目的, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":受領日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.受領日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":受領部署", Type = OracleDbType.Varchar2, Object = val.受領部署, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":受領者", Type = OracleDbType.Varchar2, Object = val.受領者, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":受領時走行距離", Type = OracleDbType.Varchar2, Object = val.受領時走行距離, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":完成日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.完成日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理責任部署", Type = OracleDbType.Varchar2, Object = val.管理責任部署, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":研命ナンバー", Type = OracleDbType.Varchar2, Object = val.研命ナンバー, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":研命期間", Type = OracleDbType.Date, Object = DateTimeUtil.GetLastDate(val.研命期間), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":固定資産NO", Type = OracleDbType.Varchar2, Object = val.固定資産NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":登録ナンバー", Type = OracleDbType.Varchar2, Object = val.登録ナンバー, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":車検登録日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車検登録日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":車検期限", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車検期限), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":廃艦日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.廃艦日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":保険NO", Type = OracleDbType.Varchar2, Object = val.保険NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":保険加入日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.保険加入日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":保険解約日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.保険解約日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":移管依頼NO", Type = OracleDbType.Varchar2, Object = val.移管依頼NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験着手日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.試験着手日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験着手証明文書", Type = OracleDbType.Varchar2, Object = val.試験着手証明文書, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":工事区分NO", Type = OracleDbType.Varchar2, Object = val.工事区分NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_中古", Type = OracleDbType.Int16, Object = val.FLAG_中古, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_ナビ付", Type = OracleDbType.Int16, Object = val.FLAG_ナビ付, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FLAG_ETC付", Type = OracleDbType.Int16, Object = val.FLAG_ETC付, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":EVデバイス", Type = OracleDbType.Varchar2, Object = val.EVデバイス, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":初年度登録年月", Type = OracleDbType.Date, Object = DateTimeUtil.GetLastDate(val.初年度登録年月), Direct = ParameterDirection.Input }
                };

                if (!db.UpdateData(sql.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 試験車履歴情報(管理票)の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(ControlSheetTestCarHistoryDeleteInModel val)
        {
            StringBuilder sql = new StringBuilder();
            List<BindModel> prms = new List<BindModel>();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験車履歴情報");
            sql.AppendLine("WHERE");
            sql.AppendLine("    データID = :データID");

            prms.Add(new BindModel
            {
                Name = ":データID",
                Type = OracleDbType.Int32,
                Object = val.データID,
                Direct = ParameterDirection.Input
            });

            if (val?.履歴NO > 0)
            {
                sql.AppendLine("    AND 履歴NO = :履歴NO");

                prms.Add(new BindModel
                {
                    Name = ":履歴NO",
                    Type = OracleDbType.Int32,
                    Object = val.履歴NO,
                    Direct = ParameterDirection.Input
                });
            }

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            return true;
        }
    }
}