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
    /// 試験車(管理票)ロジッククラス
    /// </summary>
    /// <remarks>試験車(管理票)の操作</remarks>
    public class ControlSheetTestCarLogic : TestCarBaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 試験車(管理票)の取得
        /// </summary>
        /// <returns>List</returns>
        public List<TestCarCommonModel> GetData(TestCarCommonSearchModel val)
        {
            var parm = new List<BindModel>();

            //共通SQL
            var sql = base.GetBaseTestCarSql(parm);

            //条件（試験車基本情報）
            if (val?.データID > 0)
            {
                sql.AppendLine("    AND KK.\"データID\" = :データID");

                parm.Add(new BindModel { Name = ":データID", Object = val.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input });
            }

            SetStringWhere(sql, parm, "KK", "管理票NO", val?.管理票NO);

            if (val?.廃却フラグ == true)
            {
                //Update Start 2022/04/14 杉浦 廃却条件変更
                //sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NOT NULL");
                sql.AppendLine("    AND (KK.\"研実管理廃却申請受理日\" IS NOT NULL");
                sql.AppendLine("    AND KK.\"車両搬出日\" IS NOT NULL");
                sql.AppendLine("    AND KS.\"処分コード\" IS NOT NULL)");
                //Update End 2022/04/14 杉浦 廃却条件変更
            }
            else if (val?.廃却フラグ == false)
            {
                //Update Start 2022/04/14 杉浦 廃却条件変更
                //sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
                sql.AppendLine("    AND (KK.\"研実管理廃却申請受理日\" IS NULL");
                sql.AppendLine("    OR KK.\"車両搬出日\" IS NULL");
                sql.AppendLine("    OR KS.\"処分コード\" IS NULL)");
                //Update End 2022/04/14 杉浦 廃却条件変更
            }

            //条件（試験車履歴情報）
            if (!string.IsNullOrWhiteSpace(val?.管理票発行有無))
            {
                sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

                parm.Add(new BindModel { Name = ":管理票発行有無", Object = val.管理票発行有無, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });
            }

            if (val?.START_DATE != null)
            {
                sql.AppendLine("    AND KR.\"発行年月日\" >= :START_DATE");

                parm.Add(new BindModel { Name = ":START_DATE", Object = val.START_DATE, Type = OracleDbType.Date, Direct = ParameterDirection.Input });
            }

            if (val?.END_DATE != null)
            {
                sql.AppendLine("    AND KR.\"発行年月日\" <= :END_DATE");

                parm.Add(new BindModel { Name = ":END_DATE", Object = val.END_DATE, Type = OracleDbType.Date, Direct = ParameterDirection.Input });
            }

            //Append Start 2022/01/11 杉浦 使用履歴設定画面の追加
            if (val?.管理責任部署 != null)
            {
                var sections = new List<string>();

                for (int i = 0; i < val?.管理責任部署.Count(); i++)
                {
                    sections.Add(string.Format("KR.\"管理責任部署\" = :管理責任部署{0}", i.ToString()));

                    parm.Add(new BindModel
                    {
                        Name = ":管理責任部署" + i.ToString(),
                        Type = OracleDbType.Varchar2,
                        Object = val?.管理責任部署[i],
                        Direct = ParameterDirection.Input
                    });
                }

                sql.AppendLine(string.Format("    AND ({0})", string.Join(" OR ", sections)));
            }
            //Append End 2022/01/11 杉浦 使用履歴設定画面の追加

            SetStringWhere(sql, parm, "KR", "開発符号", val?.開発符号);

            SetStringWhere(sql, parm, "KR", "試作時期", val?.試作時期);

            SetStringWhere(sql, parm, "KR", "車体番号", val?.車体番号);

            SetStringWhere(sql, parm, "KR", "号車", val?.号車);

            SetStringWhere(sql, parm, "KR", "仕向地", val?.仕向地);

            SetStringWhere(sql, parm, "KR", "メーカー名", val?.メーカー名);

            SetStringWhere(sql, parm, "KR", "外製車名", val?.外製車名);

            //種別（固定資産／資産外／リース）
            if (val?.種別 != null)
            {
                sql.AppendLine("    AND KR.\"種別\" = :種別");

                parm.Add(new BindModel { Name = ":種別", Object = val.種別, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });
            }

            //条件（DEPARTMENT_DATA）
            if (!string.IsNullOrWhiteSpace(val?.ESTABLISHMENT))
            {
                sql.AppendLine("    AND DD.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                parm.Add(new BindModel { Name = ":ESTABLISHMENT", Object = val.ESTABLISHMENT, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });
            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    KK.\"管理票NO\"");

            //取得
            return db.ReadModelList<TestCarCommonModel>(sql.ToString(), parm);
        }

        /// <summary>
        /// 試験車(管理票)の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(TestCarCommonBaseModel val, ref TestCarCommonModel returns)
        {
            StringBuilder sql = new StringBuilder();
            List<BindModel> prms = new List<BindModel>();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験車基本情報 (");
            sql.AppendLine("     データID");
            sql.AppendLine("    ,管理票NO");
            sql.AppendLine("    ,管理ラベル発行有無");
            sql.AppendLine("    ,車系");
            sql.AppendLine("    ,車型");
            sql.AppendLine("    ,型式符号");
            sql.AppendLine("    ,駐車場番号");
            sql.AppendLine("    ,リースNO");
            sql.AppendLine("    ,リース満了日");
            sql.AppendLine("    ,研実管理廃却申請受理日");
            sql.AppendLine("    ,廃却見積日");
            sql.AppendLine("    ,廃却決済承認年月");
            sql.AppendLine("    ,車両搬出日");
            sql.AppendLine("    ,メモ");
            sql.AppendLine("    ,正式取得日");
            sql.AppendLine("    ,月例点検省略有無");
            //Append Start 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            sql.AppendLine("    ,衝突試験済");
            //Append End 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            sql.AppendLine(") VALUES (");
            //Append Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
            //sql.AppendLine("     (SELECT NVL(MAX(データID), 0) + 1 FROM 試験車基本情報)");
            sql.AppendLine("     (SELECT MAX(A.ID) FROM (SELECT NVL(MAX(データID), 0) + 1 ID FROM 試験車基本情報 UNION SELECT NVL(MAX(データID), 0) + 1 ID FROM DUMMY_TESTCAR) A)");
            //Append Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
            sql.AppendLine("    ,(SELECT CONCAT(TO_CHAR(SYSDATE, 'RR'),LPAD(NVL(MAX(SUBSTR(管理票NO, 3, 4)), '0') + 1, 4, '0')) FROM 試験車基本情報 WHERE 管理票NO LIKE CONCAT(TO_CHAR(SYSDATE, 'RR'), '%'))");
            sql.AppendLine("    ,:管理ラベル発行有無");
            sql.AppendLine("    ,:車系");
            sql.AppendLine("    ,:車型");
            sql.AppendLine("    ,:型式符号");
            sql.AppendLine("    ,:駐車場番号");
            sql.AppendLine("    ,:リースNO");
            sql.AppendLine("    ,:リース満了日");
            sql.AppendLine("    ,:研実管理廃却申請受理日");
            sql.AppendLine("    ,:廃却見積日");
            sql.AppendLine("    ,:廃却決済承認年月");
            sql.AppendLine("    ,:車両搬出日");
            sql.AppendLine("    ,:メモ");
            sql.AppendLine("    ,:正式取得日");
            sql.AppendLine("    ,:月例点検省略有無");
            //Append Start 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            sql.AppendLine("    ,:衝突試験済");
            //Append End 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            sql.AppendLine(") RETURNING");
            sql.AppendLine("    データID INTO :newid");

            prms = new List<BindModel>()
            {
                new BindModel { Name = ":管理ラベル発行有無", Type = OracleDbType.Int32, Object = val.管理ラベル発行有無, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車系", Type = OracleDbType.Varchar2, Object = val.車系, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車型", Type = OracleDbType.Varchar2, Object = val.車型, Direct = ParameterDirection.Input },
                new BindModel { Name = ":型式符号", Type = OracleDbType.Varchar2, Object = val.型式符号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":駐車場番号", Type = OracleDbType.Varchar2, Object = val.駐車場番号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":リースNO", Type = OracleDbType.Varchar2, Object = val.リースNO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":リース満了日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.リース満了日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":研実管理廃却申請受理日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.研実管理廃却申請受理日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":廃却見積日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.廃却見積日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":廃却決済承認年月", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.廃却決済承認年月), Direct = ParameterDirection.Input },
                new BindModel { Name = ":車両搬出日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車両搬出日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":メモ", Type = OracleDbType.Varchar2, Object = val.メモ, Direct = ParameterDirection.Input },
                new BindModel { Name = ":正式取得日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.正式取得日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":月例点検省略有無", Type = OracleDbType.Int32, Object = val.月例点検省略有無, Direct = ParameterDirection.Input },
                //Append Start 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                new BindModel { Name = ":衝突試験済", Type = OracleDbType.Int32, Object = val.衝突試験済, Direct = ParameterDirection.Input },
                //Append End 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            };

            // 採番ID：戻り値設定
            db.Returns = new List<BindModel>();
            db.Returns.Add(new BindModel
            {
                Name = ":newid",
                Type = OracleDbType.Int32,
                Direct = ParameterDirection.Input
            });

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            returns.データID = Convert.ToInt32(db.Returns.Where(r => r.Name == ":newid").FirstOrDefault().Object.ToString());

            sql = new StringBuilder();

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
            sql.AppendLine("    ,固定資産NO");
            sql.AppendLine("    ,登録ナンバー");
            sql.AppendLine("    ,車検登録日");
            sql.AppendLine("    ,車検期限");
            sql.AppendLine("    ,廃艦日");
            sql.AppendLine("    ,試験着手日");
            sql.AppendLine("    ,試験着手証明文書");
            sql.AppendLine("    ,工事区分NO");
            sql.AppendLine("    ,FLAG_ナビ付");
            sql.AppendLine("    ,FLAG_ETC付");
            sql.AppendLine("    ,EVデバイス");
            sql.AppendLine("    ,初年度登録年月");
            //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
            sql.AppendLine("    ,自動車ﾘｻｲｸﾙ法");
            sql.AppendLine("    ,A_C冷媒種類");
            //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     :データID");
            sql.AppendLine("    ,1");
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
            sql.AppendLine("    ,:固定資産NO");
            sql.AppendLine("    ,:登録ナンバー");
            sql.AppendLine("    ,:車検登録日");
            sql.AppendLine("    ,:車検期限");
            sql.AppendLine("    ,:廃艦日");
            sql.AppendLine("    ,:試験着手日");
            sql.AppendLine("    ,:試験着手証明文書");
            sql.AppendLine("    ,:工事区分NO");
            sql.AppendLine("    ,:FLAG_ナビ付");
            sql.AppendLine("    ,:FLAG_ETC付");
            sql.AppendLine("    ,:EVデバイス");
            sql.AppendLine("    ,:初年度登録年月");
            //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
            sql.AppendLine("    ,:自動車ﾘｻｲｸﾙ法");
            sql.AppendLine("    ,:A_C冷媒種類");
            //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
            sql.AppendLine(")");

            prms = new List<BindModel>()
            {
                new BindModel { Name = ":データID", Type = OracleDbType.Int32, Object = returns.データID, Direct = ParameterDirection.Input },
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
                new BindModel { Name = ":固定資産NO", Type = OracleDbType.Varchar2, Object = val.固定資産NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":登録ナンバー", Type = OracleDbType.Varchar2, Object = val.登録ナンバー, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車検登録日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車検登録日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":車検期限", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車検期限), Direct = ParameterDirection.Input },
                new BindModel { Name = ":廃艦日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.廃艦日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":試験着手日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.試験着手日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":試験着手証明文書", Type = OracleDbType.Varchar2, Object = val.試験着手証明文書, Direct = ParameterDirection.Input },
                new BindModel { Name = ":工事区分NO", Type = OracleDbType.Varchar2, Object = val.工事区分NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAG_ナビ付", Type = OracleDbType.Int16, Object = val.FLAG_ナビ付, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAG_ETC付", Type = OracleDbType.Int16, Object = val.FLAG_ETC付, Direct = ParameterDirection.Input },
                new BindModel { Name = ":EVデバイス", Type = OracleDbType.Varchar2, Object = val.EVデバイス, Direct = ParameterDirection.Input },
                new BindModel { Name = ":初年度登録年月", Type = OracleDbType.Date, Object = DateTimeUtil.GetLastDate(val.初年度登録年月), Direct = ParameterDirection.Input },
                
                //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
                new BindModel { Name = ":自動車ﾘｻｲｸﾙ法", Type = OracleDbType.Varchar2, Object = val.自動車ﾘｻｲｸﾙ法, Direct = ParameterDirection.Input },
                new BindModel { Name = ":A_C冷媒種類", Type = OracleDbType.Varchar2, Object = val.A_C冷媒種類, Direct = ParameterDirection.Input }
                //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
        };

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("固定資産情報 (");
            sql.AppendLine("     データID");
            sql.AppendLine("    ,処分コード");
            sql.AppendLine("    ,処分予定年月");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     :データID");
            sql.AppendLine("    ,:処分コード");
            sql.AppendLine("    ,:処分予定年月");
            sql.AppendLine(")");

            prms = new List<BindModel>()
            {
                new BindModel { Name = ":データID", Type = OracleDbType.Int32, Object = returns.データID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":処分コード", Type = OracleDbType.Varchar2, Object = val.処分コード, Direct = ParameterDirection.Input },
                new BindModel { Name = ":処分予定年月", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.処分予定年月), Direct = ParameterDirection.Input }
            };

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            sql = base.GetBaseTestCarSql();

            sql.AppendLine("    AND KK.\"データID\" = :データID");

            prms = new List<BindModel>()
            {
                new BindModel { Name = ":データID", Type = OracleDbType.Int32, Object = returns.データID, Direct = ParameterDirection.Input },
            };

            var list = db.ReadModelList<TestCarCommonModel>(sql.ToString(), prms);

            if (list != null && list.Any() == true)
            {
                returns = list.FirstOrDefault();

                return true;
            }

            return false;
        }

        /// <summary>
        /// 試験車(管理票)の更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(TestCarCommonModel val)
        {
            List<TestCarCommonModel> list = new List<TestCarCommonModel>() { val };

            // 試験車基本情報
            if (!base.UpdateTestCarBaseInfo(list, (x => new List<BindModel>
            {
                new BindModel { Name = ":管理ラベル発行有無", Type = OracleDbType.Int32, Object = val.管理ラベル発行有無, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車系", Type = OracleDbType.Varchar2, Object = val.車系, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車型", Type = OracleDbType.Varchar2, Object = val.車型, Direct = ParameterDirection.Input },
                new BindModel { Name = ":型式符号", Type = OracleDbType.Varchar2, Object = val.型式符号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":駐車場番号", Type = OracleDbType.Varchar2, Object = val.駐車場番号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":リースNO", Type = OracleDbType.Varchar2, Object = val.リースNO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":リース満了日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.リース満了日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":研実管理廃却申請受理日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.研実管理廃却申請受理日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":廃却見積日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.廃却見積日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":廃却決済承認年月", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.廃却決済承認年月), Direct = ParameterDirection.Input },
                new BindModel { Name = ":車両搬出日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車両搬出日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":メモ", Type = OracleDbType.Varchar2, Object = val.メモ, Direct = ParameterDirection.Input },
                new BindModel { Name = ":正式取得日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.正式取得日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":月例点検省略有無", Type = OracleDbType.Int32, Object = val.月例点検省略有無, Direct = ParameterDirection.Input },
                //Append Start 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
                new BindModel { Name = ":衝突試験済", Type = OracleDbType.Int32, Object = val.衝突試験済, Direct = ParameterDirection.Input },
                //Append End 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
            }
            ))) return false;

            // 試験車履歴情報
            if (!base.UpdateTestCarHistoryInfo(list, (x => new List<BindModel>
            {
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
                new BindModel { Name = ":固定資産NO", Type = OracleDbType.Varchar2, Object = val.固定資産NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":登録ナンバー", Type = OracleDbType.Varchar2, Object = val.登録ナンバー, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車検登録日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車検登録日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":車検期限", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車検期限), Direct = ParameterDirection.Input },
                new BindModel { Name = ":廃艦日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.廃艦日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":試験着手日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.試験着手日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":試験着手証明文書", Type = OracleDbType.Varchar2, Object = val.試験着手証明文書, Direct = ParameterDirection.Input },
                new BindModel { Name = ":工事区分NO", Type = OracleDbType.Varchar2, Object = val.工事区分NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAG_ナビ付", Type = OracleDbType.Int16, Object = val.FLAG_ナビ付, Direct = ParameterDirection.Input },
                new BindModel { Name = ":FLAG_ETC付", Type = OracleDbType.Int16, Object = val.FLAG_ETC付, Direct = ParameterDirection.Input },
                new BindModel { Name = ":EVデバイス", Type = OracleDbType.Varchar2, Object = val.EVデバイス, Direct = ParameterDirection.Input },
                new BindModel { Name = ":初年度登録年月", Type = OracleDbType.Date, Object = DateTimeUtil.GetLastDate(val.初年度登録年月), Direct = ParameterDirection.Input },
                //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
                new BindModel { Name = ":自動車ﾘｻｲｸﾙ法", Type = OracleDbType.Varchar2, Object = val.自動車ﾘｻｲｸﾙ法, Direct = ParameterDirection.Input },
                new BindModel { Name = ":A_C冷媒種類", Type = OracleDbType.Varchar2, Object = val.A_C冷媒種類, Direct = ParameterDirection.Input }
                //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法と冷媒種類)
            }
            ))) return false;

            // 固定資産情報
            if (!base.UpdateFixedAssetInfo(list, (x => new List<BindModel>
            {
                new BindModel { Name = ":処分コード", Type = OracleDbType.Varchar2, Object = val.処分コード, Direct = ParameterDirection.Input },
                new BindModel { Name = ":処分予定年月", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.処分予定年月), Direct = ParameterDirection.Input }
            }
            ))) return false;

            return true;
        }

        /// <summary>
        /// 試験車(管理票)の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(TestCarCommonBaseModel val)
        {
            StringBuilder sql = new StringBuilder();
            List<BindModel> prms = new List<BindModel>();

            prms.Add(new BindModel
            {
                Name = ":データID",
                Type = OracleDbType.Int32,
                Object = val.データID,
                Direct = ParameterDirection.Input
            });

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験車基本情報");
            sql.AppendLine("WHERE");
            sql.AppendLine("    データID = :データID");

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験車履歴情報");
            sql.AppendLine("WHERE");
            sql.AppendLine("    データID = :データID");

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    固定資産情報");
            sql.AppendLine("WHERE");
            sql.AppendLine("    データID = :データID");

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }


            sql = new StringBuilder();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    開本内移管履歴情報");
            sql.AppendLine("WHERE");
            sql.AppendLine("    データID = :データID");

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            return true;
        }
    }
}