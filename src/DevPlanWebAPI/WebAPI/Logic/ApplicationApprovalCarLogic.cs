using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 処理待ち車両リスト業務ロジッククラス
    /// </summary>
    public class ApplicationApprovalCarLogic : TestCarBaseLogic
    {
        #region メンバ変数
        private readonly List<BindModel> TestCarHistory = new List<BindModel>
        {
            new BindModel { Name = ":管理票発行有無", Object = "済", Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

        };

        private UserSearchOutModel user;

        private readonly Action<StringBuilder> AddColumnSiyouRireki;
        private readonly Action<StringBuilder> AddTableSiyouRireki;
        private readonly Action<StringBuilder> AddColumnBusyo;
        private readonly Action<StringBuilder> AddTableBusyo;

        private const string IkansakiBusyo = "移管先部署";

        private const string Gunma = "g";
        private const string Toukyou = "t";
        private const string All = "n";

        private const string Katyou = Const.AccessLevelCode.Syokusei;
        private const string Tantou = Const.AccessLevelCode.Tanto;
        private const string Tantousya = Const.AccessLevelCode.Ippan;
        private const string Fts = Const.AccessLevelCode.Skc;

        private const string None = "0";

        private const string Tyuusi = "J";

        private const string GunmaToukyou = "gt";

        private const string Haikyaku = "^[D]$";
        private const string Ikan = "^[EFGH]$";
        private const string GtIkan = "^[GH]$";

        private const string Ippan = "[" + Const.ManagementGroupCode.Ippan + "]";
        private const string Kenjitu = "[" + Const.ManagementGroupCode.Kenjitu + "]";
        private const string Kanri = "[" + Const.ManagementGroupCode.Kanri + "]";
        private const string KenjituKanri = "[" + Const.ManagementGroupCode.Kenjitu + Const.ManagementGroupCode.Kanri + "]";

        private const string GunmaKairyouKenkyuu = "40";
        private const string ToukyouKairyouKenkyuu = "62";

        private const string TyuusiMsg = "{0}中止";

        private readonly Dictionary<string, string> TyuusiMap = new Dictionary<string, string>()
        {
            { "D", "K" },
            { "E", "L" },
            { "F", "M" },
            { "G", "N" },
            { "H", "O" }

        };
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ApplicationApprovalCarLogic()
        {
            //使用履歴の取得で追加する列
            this.AddColumnSiyouRireki = (sql =>
            {
                sql.AppendLine("    ,SR.\"SEQNO\"");
                sql.AppendLine("    ,SR.\"承認状況\"");
                sql.AppendLine("    ,SR.\"STEPNO\"");
                sql.AppendLine("    ,SR.\"承認者レベル\"");
                sql.AppendLine("    ,SR.\"管理部署承認\"");
                sql.AppendLine("    ,SR.\"移管先部署ID\"");
                sql.AppendLine("    ,SR.\"承認要件コード\"");
                sql.AppendLine("    ,SR.\"駐車場番号\" AS \"入力駐車場番号\"");
                sql.AppendLine("    ,SR.\"承認要件名\"");
                sql.AppendLine("    ,SR.\"駐車場指定\"");

            });

            //使用履歴の取得で追加するテーブル
            this.AddTableSiyouRireki = (sql =>
            {
                sql.AppendLine("    INNER JOIN");
                sql.AppendLine("                (--SR");
                sql.AppendLine("                    SELECT");
                sql.AppendLine("                         A.\"データID\"");
                sql.AppendLine("                        ,A.\"履歴NO\"");
                sql.AppendLine("                        ,A.\"SEQNO\"");
                sql.AppendLine("                        ,A.\"承認要件コード\"");
                sql.AppendLine("                        ,A.\"STEPNO\"");
                sql.AppendLine("                        ,A.\"承認状況\"");
                sql.AppendLine("                        ,A.\"承認者レベル\"");
                sql.AppendLine("                        ,A.\"管理部署承認\"");
                sql.AppendLine("                        ,A.\"処理日\"");
                sql.AppendLine("                        ,A.\"管理責任課名\"");
                sql.AppendLine("                        ,A.\"管理責任部署名\"");
                sql.AppendLine("                        ,A.\"使用課名\"");
                sql.AppendLine("                        ,A.\"使用部署名\"");
                sql.AppendLine("                        ,A.\"試験内容\"");
                sql.AppendLine("                        ,A.\"工事区分NO\"");
                sql.AppendLine("                        ,A.\"実走行距離\"");
                sql.AppendLine("                        ,A.\"編集日\"");
                sql.AppendLine("                        ,A.\"編集者\"");
                sql.AppendLine("                        ,A.\"移管先部署ID\"");
                sql.AppendLine("                        ,A.\"駐車場番号\"");
                sql.AppendLine("                        ,SM.\"承認要件名\"");
                sql.AppendLine("                        ,SS.\"駐車場指定\"");
                sql.AppendLine("                    FROM");
                sql.AppendLine("                        \"使用履歴情報\" A");
                sql.AppendLine("                        INNER JOIN");
                sql.AppendLine("                                    (");
                sql.AppendLine("                                        SELECT");
                sql.AppendLine("                                             A.\"データID\"");
                sql.AppendLine("                                            ,A.\"履歴NO\"");
                sql.AppendLine("                                            ,MAX(A.\"SEQNO\") AS \"SEQNO\"");
                sql.AppendLine("                                        FROM");
                sql.AppendLine("                                            \"使用履歴情報\" A");
                sql.AppendLine("                                        GROUP BY");
                sql.AppendLine("                                             A.\"データID\"");
                sql.AppendLine("                                            ,A.\"履歴NO\"");
                sql.AppendLine("                                    ) B");
                sql.AppendLine("                        ON A.\"データID\" = B.\"データID\"");
                sql.AppendLine("                        AND A.\"履歴NO\" = B.\"履歴NO\"");
                sql.AppendLine("                        AND A.\"SEQNO\" = B.\"SEQNO\"");
                sql.AppendLine("                        INNER JOIN \"承認要件マスタ\" SM");
                sql.AppendLine("                        ON A.\"承認要件コード\" = SM.\"承認要件コード\"");
                sql.AppendLine("                        LEFT JOIN \"承認STEPマスタ\" SS");
                sql.AppendLine("                        ON A.\"承認要件コード\" = SS.\"承認要件コード\"");
                sql.AppendLine("                        AND A.\"STEPNO\" = SS.\"STEPNO\"");
                sql.AppendLine("                ) SR");
                sql.AppendLine("    ON KK.\"データID\" = SR.\"データID\"");
                sql.AppendLine("    AND KR.\"履歴NO\" = SR.\"履歴NO\"");

            });

            //所属部署の取得で追加する列
            this.AddColumnBusyo = (sql =>
            {
                sql.AppendLine("    ,SI.\"DEPARTMENT_ID\" AS \"移管先部署_DEPARTMENT_ID\"");
                sql.AppendLine("    ,SI.\"DEPARTMENT_CODE\" AS \"移管先部署_DEPARTMENT_CODE\"");
                sql.AppendLine("    ,SI.\"SECTION_ID\" AS \"移管先部署_SECTION_ID\"");
                sql.AppendLine("    ,SI.\"SECTION_CODE\" AS \"移管先部署_SECTION_CODE\"");
                sql.AppendLine("    ,SI.\"SECTION_GROUP_ID\" AS \"移管先部署_SECTION_GROUP_ID\"");
                sql.AppendLine("    ,SI.\"SECTION_GROUP_CODE\" AS \"移管先部署_SECTION_GROUP_CODE\"");

            });

            //所属部署の取得で追加するテーブル
            this.AddTableBusyo = (sql =>
            {
                sql.AppendLine("    LEFT JOIN");
                sql.AppendLine("                (--SI");
                sql.AppendLine("                    SELECT");
                sql.AppendLine("                         SG.\"SECTION_GROUP_ID\"");
                sql.AppendLine("                        ,SG.\"SECTION_GROUP_CODE\"");
                sql.AppendLine("                        ,SD.\"SECTION_ID\"");
                sql.AppendLine("                        ,SD.\"SECTION_CODE\"");
                sql.AppendLine("                        ,DD.\"DEPARTMENT_ID\"");
                sql.AppendLine("                        ,DD.\"DEPARTMENT_CODE\"");
                sql.AppendLine("                        ,DD.\"ESTABLISHMENT\"");
                sql.AppendLine("                    FROM");
                sql.AppendLine("                        \"SECTION_GROUP_DATA\" SG");
                sql.AppendLine("                        LEFT JOIN \"SECTION_DATA\" SD");
                sql.AppendLine("                        ON SG.\"SECTION_ID\" = SD.\"SECTION_ID\"");
                sql.AppendLine("                        LEFT JOIN \"DEPARTMENT_DATA\" DD");
                sql.AppendLine("                        ON SD.\"DEPARTMENT_ID\" = DD.\"DEPARTMENT_ID\"");
                sql.AppendLine("                ) SI");
                sql.AppendLine("    ON SR.\"移管先部署ID\" = SI.\"SECTION_GROUP_ID\"");

            });
        }
        #endregion

        #region 試験車取得
        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<ApplicationApprovalCarModel> GetTestCar(ApplicationApprovalCarSearchModel cond)
        {
            //ユーザー
            this.user = base.GetLogic<UserLogic>().GetData(new UserSearchInModel { PERSONEL_ID = new[] { cond.PERSONEL_ID }, MANAGE_FLG = true }).FirstOrDefault();
            var syozokuCd = this.GetSyozokuCode(this.user.ESTABLISHMENT);
            var kanri = this.GetKanriBusyoSyubetu(this.user);

            var prms = new List<BindModel>(TestCarHistory.ToArray());

            //抽出条件
            var where = this.GetWhere(prms, cond);

            //試験着手日
            var sqlSikenTyakusyu = this.GetSikenTyakusyu(where, prms, cond);

            //月例点検
            var sqlGetureiTenken = this.GetGetureiTenken(where, prms, cond);

            //廃却申請
            var sqlHaikyakuSinsei = this.GetHaikyakuSinsei(where, prms, cond);

            //移管
            var sqlIkan = this.GetIkan(where, prms, cond);

            //処理中(管理部署)
            var sqlSyorityuu = this.GetSyorityuu(where, prms, cond);

            //処理中(移管先部署)
            var sqlSyorityuuIkan = this.GetSyorityuu(where, prms, cond, true);

            //処理済(廃却)
            var sqlSyorizumiHaikyaku = this.GetSyorizumiHaikyaku(where, prms, cond);

            //管理部署
            var sqlKanriBusyo = this.GetKanriBusyo(where, prms, cond);

            var sqlList = new List<StringBuilder>();

            //取得種別ごとの分岐
            switch (cond.TARGET_TYPE)
            {
                //試験着手日
                case ApplicationApprovalCarTargetType.TestStartDay:
                    //表示種別が管理部署用かどうか
                    if (cond.VIEW_TYPE != ViewType.Management)
                    {
                        sqlList.Add(sqlSikenTyakusyu);
                        sqlList.Add(sqlSyorityuu);

                    }
                    else
                    {
                        sqlList.Add(sqlKanriBusyo);

                    }
                    break;

                //月例点検
                case ApplicationApprovalCarTargetType.MonthlyInspection:
                    //表示種別が管理部署用かどうか
                    if (cond.VIEW_TYPE != ViewType.Management)
                    {
                        sqlList.Add(sqlGetureiTenken);

                        //2ヶ月以上未入力も取得するかどうか
                        if ((cond.MONTHLY_INPUT ?? false) == false)
                        {
                            sqlList.Add(sqlSyorityuu);

                        }

                    }
                    else
                    {
                        sqlList.Add(sqlKanriBusyo);

                    }
                    break;

                //廃却申請
                case ApplicationApprovalCarTargetType.DispositionApplication:
                    //表示種別ごとの分岐
                    switch (cond.VIEW_TYPE)
                    {
                        //管理責任部署用
                        case ViewType.ManagementResponsibility:
                            sqlList.Add(sqlHaikyakuSinsei);
                            sqlList.Add(sqlSyorityuu);
                            break;

                        //管理部署用
                        case ViewType.Management:
                            sqlList.Add(sqlSyorizumiHaikyaku);
                            sqlList.Add(sqlKanriBusyo);
                            break;

                        //全て
                        case ViewType.All:
                            sqlList.Add(sqlHaikyakuSinsei);
                            sqlList.Add(sqlSyorizumiHaikyaku);
                            sqlList.Add(sqlSyorityuu);
                            break;

                    }
                    break;

                //T技本内移管
                //G技本内移管
                case ApplicationApprovalCarTargetType.ToukyouTransfer:
                case ApplicationApprovalCarTargetType.GunmaTransfer:
                    //表示種別が管理部署用かどうか
                    if (cond.VIEW_TYPE != ViewType.Management)
                    {
                        sqlList.Add(sqlIkan);
                        sqlList.Add(sqlSyorityuu);

                    }
                    else
                    {
                        sqlList.Add(sqlKanriBusyo);

                    }
                    break;
                //G→T移管
                case ApplicationApprovalCarTargetType.GtTransfer:
                    //表示種別ごとの分岐
                    switch (cond.VIEW_TYPE)
                    {
                        //管理責任部署用
                        case ViewType.ManagementResponsibility:
                            sqlList.Add(sqlIkan);
                            sqlList.Add(sqlSyorityuuIkan);
                            break;

                        //管理部署用
                        case ViewType.Management:
                            sqlList.Add(sqlKanriBusyo);
                            break;

                        //全て
                        case ViewType.All:
                            sqlList.Add(sqlIkan);

                            //管理所在地が東京かどうか
                            if (cond.ESTABLISHMENT == "t")
                                sqlList.Add(sqlSyorityuuIkan);
                            else
                                sqlList.Add(sqlSyorityuu);

                            break;

                    }
                    break;

                //T→G移管
                case ApplicationApprovalCarTargetType.TgTransfer:
                    //表示種別ごとの分岐
                    switch (cond.VIEW_TYPE)
                    {
                        //管理責任部署用
                        case ViewType.ManagementResponsibility:
                            sqlList.Add(sqlIkan);
                            sqlList.Add(sqlSyorityuuIkan);
                            break;

                        //管理部署用
                        case ViewType.Management:
                            sqlList.Add(sqlKanriBusyo);
                            break;

                        //全て
                        case ViewType.All:
                            sqlList.Add(sqlIkan);
                            
                            //管理所在地が群馬かどうか
                            if (cond.ESTABLISHMENT == "g")
                                sqlList.Add(sqlSyorityuuIkan);
                            else
                                sqlList.Add(sqlSyorityuu);

                            break;

                    }
                    break;

                //全て
                case ApplicationApprovalCarTargetType.All:
                    //表示種別ごとの分岐
                    switch (cond.VIEW_TYPE)
                    {
                        //管理責任部署用
                        case ViewType.ManagementResponsibility:
                            sqlList.Add(sqlGetureiTenken);
                            sqlList.Add(sqlHaikyakuSinsei);
                            sqlList.Add(sqlIkan);
                            sqlList.Add(sqlSyorityuu);
                            break;

                        //管理部署用
                        case ViewType.Management:
                            sqlList.Add(sqlSyorizumiHaikyaku);
                            sqlList.Add(sqlKanriBusyo);
                            break;

                        //全て
                        case ViewType.All:
                            sqlList.Add(sqlGetureiTenken);
                            sqlList.Add(sqlHaikyakuSinsei);
                            sqlList.Add(sqlSyorizumiHaikyaku);
                            sqlList.Add(sqlIkan);
                            sqlList.Add(sqlSyorityuu);
                            break;

                    }
                    break;

            }

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    *");
            sql.AppendLine("FROM");
            sql.AppendLine("    (");
            sql.Append(string.Join("UNION ALL" + Const.CrLf, sqlList.Select(x => x.ToString()).ToArray()));
            sql.AppendLine("    ) A");
            sql.AppendLine("WHERE 0 = 0");

            //承認要件コードの条件を設定
            this.SetSyouninYoukenCdWhere(sql, cond, "A");

            //条件
            base.SetStringWhere(sql, prms, "A", "承認状況", cond.承認状況);

            //ユーザーのみ取得かどうか
            if (cond.USER_ONLY == true)
            {
                var level = this.GetAccessLevel(this.user);
                var isKatyou = level == Katyou;

                //パラメータ
                this.AddParameter(prms, ":承認者レベル", level, OracleDbType.Varchar2);

                sql.AppendLine("    AND A.\"STEPNO\" <> 0");
                sql.AppendLine("    AND");
                sql.AppendLine("        (");
                sql.AppendLine("            CASE");
                sql.AppendLine("                WHEN A.\"STEPNO\" < 0 THEN DECODE(:承認者レベル,'0',0,1)");
                sql.AppendLine("                WHEN A.\"承認者レベル\" = :承認者レベル THEN 1");
                sql.AppendLine("                ELSE 0");
                sql.AppendLine("            END");
                sql.AppendLine("        ) = 1");

                //表示種別ごとの分岐
                switch (cond.VIEW_TYPE)
                {
                    //管理責任部署用
                    case ViewType.ManagementResponsibility:
                        //パラメータ
                        this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);

                        sql.AppendLine("    AND A.\"管理部署承認\" = '0'");
                        sql.AppendLine("    AND");
                        sql.AppendLine("        (");
                        sql.AppendLine("            (");
                        sql.AppendLine("                NVL(A.\"承認状況\",CHR(0)) LIKE '%移管先部署%'");
                        sql.AppendLine("                AND");
                        sql.AppendLine("                A.\"移管先部署_SECTION_ID\" = :USER_SECTION_ID");

                        //担当かどうか
                        if (level == Tantou)
                        {
                            sql.AppendLine("                AND");
                            sql.AppendLine("                A.\"移管先部署ID\" = :USER_SECTION_GROUP_ID");

                            this.AddParameter(prms, ":USER_SECTION_GROUP_ID", this.user.SECTION_GROUP_ID, OracleDbType.Varchar2);

                        }

                        sql.AppendLine("            )");
                        sql.AppendLine("            OR");
                        sql.AppendLine("            (");
                        sql.AppendLine("                NVL(A.\"承認状況\",CHR(0)) NOT LIKE '%移管先部署%'");
                        sql.AppendLine("                AND");
                        sql.AppendLine("                A.\"SECTION_ID\" = :USER_SECTION_ID");

                        //担当かどうか
                        if (level == Tantou)
                        {
                            sql.AppendLine("                AND");
                            sql.AppendLine("                A.\"SECTION_GROUP_ID\" = :USER_SECTION_GROUP_ID");

                        }

                        sql.AppendLine("            )");
                        sql.AppendLine("        )");
                        break;

                    //管理部署用
                    case ViewType.Management:
                        sql.AppendLine("    AND A.\"STEPNO\" >= 0");
                        sql.AppendLine("    AND A.\"管理部署承認\" <> '0'");
                        sql.AppendLine("    AND REGEXP_LIKE(A.\"管理部署承認\",:管理部署承認)");
                        sql.AppendLine("    AND LOWER(A.\"管理部署承認\") LIKE '%' || :USER_SYOZOKU || '%'");

                        //パラメータ
                        this.AddParameter(prms, ":管理部署承認", kanri, OracleDbType.Varchar2);
                        this.AddParameter(prms, ":USER_SYOZOKU", user.ESTABLISHMENT, OracleDbType.Varchar2);
                        break;

                    //全て
                    case ViewType.All:
                        //パラメータ
                        this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);

                        sql.AppendLine("    AND");
                        sql.AppendLine("        (");
                        sql.AppendLine("            (");
                        sql.AppendLine("                NVL(A.\"承認状況\",CHR(0)) LIKE '%移管先部署%'");
                        sql.AppendLine("                AND");
                        sql.AppendLine("                A.\"移管先部署_SECTION_ID\" = :USER_SECTION_ID");

                        //担当かどうか
                        if (level == Tantou)
                        {
                            sql.AppendLine("                AND");
                            sql.AppendLine("                A.\"移管先部署ID\" = :USER_SECTION_GROUP_ID");

                            this.AddParameter(prms, ":USER_SECTION_GROUP_ID", this.user.SECTION_GROUP_ID, OracleDbType.Varchar2);

                        }

                        sql.AppendLine("            )");
                        sql.AppendLine("            OR");
                        sql.AppendLine("            (");
                        sql.AppendLine("                NVL(A.\"承認状況\",CHR(0)) NOT LIKE '%移管先部署%'");
                        sql.AppendLine("            )");
                        sql.AppendLine("        )");
                        break;

                }

            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"SECTION_CODE\"");
            sql.AppendLine("    ,A.\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,A.\"管理票NO\"");

            //サブクエリの取得
            var txt = sql.ToString();
            var kr = Regex.Match(txt, @"\(--KR.+?\) KR", RegexOptions.Singleline);
            var ki = Regex.Match(txt, @"\(--KI.+?\) KI", RegexOptions.Singleline);
            var sr = Regex.Match(txt, @"\(--SR.+?\) SR", RegexOptions.Singleline);
            var si = Regex.Match(txt, @"\(--SI.+?\) SI", RegexOptions.Singleline);

            //WITH句の設定
            var with = new StringBuilder();
            with.AppendLine("WITH");
            with.AppendLine("KR AS");
            with.Append(kr.Value.Replace(") KR", ")"));
            with.AppendLine(",");
            with.AppendLine("KI AS");
            with.Append(ki.Value.Replace(") KI", ")"));

            //使用履歴があるかどうか
            if (sr.Length > 0)
            {
                with.AppendLine(",");
                with.AppendLine("SR AS");
                with.Append(sr.Value.Replace(") SR", ")"));

                txt = txt.Replace(sr.Value, "SR");

            }

            //所属部署があるかどうか
            if (si.Length > 0)
            {
                with.AppendLine(",");
                with.AppendLine("SI AS");
                with.Append(si.Value.Replace(") SI", ")"));

                txt = txt.Replace(si.Value, "SI");

            }
            with.AppendLine();

            //サブクエリを置き換え
            txt = txt.Replace(kr.Value, "KR");
            txt = txt.Replace(ki.Value, "KI");

            //SQL再設定
            sql.Clear();
            sql.Append(with);
            sql.Append(txt);

            //取得
            var target = db.ReadModelList<ApplicationApprovalCarModel>(sql.ToString(), prms);

            var results = new List<ApplicationApprovalCarModel>();

            //全てを取得かどうか
            if (cond.TARGET_TYPE == ApplicationApprovalCarTargetType.All)
            {
                target.ForEach(x =>
                {
                    //同一管理票NOの場合は除外
                    if (results.Any(y => y.管理票NO == x.管理票NO) == false)
                    {
                        results.Add(x);

                    }

                });

            }
            else
            {
                results = target;

            }

            return results;

        }

        /// <summary>
        /// 抽出条件の取得
        /// </summary>
        /// <param name="prms">パラメータ</param>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private StringBuilder GetWhere(List<BindModel> prms, ApplicationApprovalCarSearchModel cond)
        {
            var where = new StringBuilder();

            //条件
            base.SetStringWhere(where, prms, "KR", "開発符号", cond.開発符号);
            base.SetStringWhere(where, prms, "KK", "管理票NO", cond.管理票NO);
            base.SetStringWhere(where, prms, "SG", "SECTION_ID", cond.SECTION_ID);
            base.SetStringWhere(where, prms, "SG", "SECTION_GROUP_ID", cond.SECTION_GROUP_ID);
            base.SetStringWhere(where, prms, "KR", "試作時期", cond.試作時期);
            base.SetStringWhere(where, prms, "KR", "号車", cond.号車);
            base.SetStringWhere(where, prms, "KR", "車体番号", cond.車体番号);
            base.SetStringWhere(where, prms, "KR", "固定資産NO", cond.固定資産NO);

            return where;

        }

        /// <summary>
        /// 試験着手日のSQL取得
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="prms">パラメータ</param>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private StringBuilder GetSikenTyakusyu(StringBuilder where, List<BindModel> prms, ApplicationApprovalCarSearchModel cond)
        {
            var sql = base.GetBaseTestCarSql(
                (s =>
                {
                    s.AppendLine("    ,CAST(1 AS NUMBER(8)) AS \"SEQNO\"");
                    s.AppendLine("    ,NULL AS \"承認状況\"");
                    s.AppendLine("    ,CAST(-1 AS NUMBER(8)) AS \"STEPNO\"");
                    s.AppendLine("    ,'2' AS \"承認者レベル\"");
                    s.AppendLine("    ,'0' AS \"管理部署承認\"");
                    s.AppendLine("    ,NULL AS \"移管先部署ID\"");
                    s.AppendLine("    ,'B' AS \"承認要件コード\"");
                    s.AppendLine("    ,NULL AS \"入力駐車場番号\"");
                    s.AppendLine("    ,NULL AS \"承認要件名\"");
                    s.AppendLine("    ,NULL AS \"駐車場指定\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_CODE\"");

                }), null, TestCarHistory, true, true);

            //条件
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NULL");
            sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND KR.\"試験着手日\" IS NULL");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

            //表示種別ごとの分岐
            switch (cond.VIEW_TYPE)
            {
                //管理責任部署用
                case ViewType.ManagementResponsibility:
                    sql.AppendLine("    AND SG.\"SECTION_ID\" = :USER_SECTION_ID");

                    this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                    break;

                //全て
                case ViewType.All:
                    //管理所在地
                    if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                    {
                        sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                        this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                    }
                    break;

            }

            //抽出条件
            sql.Append(where);

            return sql;

        }

        /// <summary>
        /// 月例点検のSQL取得
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="prms">パラメータ</param>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private StringBuilder GetGetureiTenken(StringBuilder where, List<BindModel> prms, ApplicationApprovalCarSearchModel cond)
        {
            var sql = new StringBuilder();

            //2ヶ月以上未入力も取得するかどうか
            if ((cond.MONTHLY_INPUT ?? false) == false)
            {
                //今月未入力
                sql = base.GetBaseTestCarSql(
                    (s =>
                    {
                        s.AppendLine("    ,SR.\"SEQNO\"");
                        s.AppendLine("    ,SR.\"承認状況\"");
                        s.AppendLine("    ,CAST(-1 AS NUMBER(8)) AS \"STEPNO\"");
                        s.AppendLine("    ,'2' AS \"承認者レベル\"");
                        s.AppendLine("    ,'0' AS \"管理部署承認\"");
                        s.AppendLine("    ,NULL AS \"移管先部署ID\"");
                        s.AppendLine("    ,'C' AS \"承認要件コード\"");
                        s.AppendLine("    ,SR.\"駐車場番号\" AS \"入力駐車場番号\"");
                        s.AppendLine("    ,SR.\"承認要件名\"");
                        s.AppendLine("    ,SR.\"駐車場指定\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_ID\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_CODE\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_SECTION_ID\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_SECTION_CODE\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_ID\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_CODE\"");
                    }),
                    (s =>
                    {
                        AddTableSiyouRireki(s);

                        s.AppendLine("    INNER JOIN");
                        s.AppendLine("                (");
                        s.AppendLine("                    SELECT");
                        s.AppendLine("                         A.\"データID\"");
                        s.AppendLine("                        ,A.\"履歴NO\"");
                        s.AppendLine("                        ,MAX(A.\"処理日\") AS \"処理日\"");
                        s.AppendLine("                    FROM");
                        s.AppendLine("                        \"使用履歴情報\" A");
                        s.AppendLine("                    WHERE 0 = 0");
                        s.AppendLine("                        AND A.\"処理日\" > LAST_DAY(ADD_MONTHS(SYSDATE,-2))");
                        s.AppendLine("                    GROUP BY");
                        s.AppendLine("                         A.\"データID\"");
                        s.AppendLine("                        ,A.\"履歴NO\"");
                        s.AppendLine("                ) SR_MAX");
                        s.AppendLine("    ON SR.\"データID\" = SR_MAX.\"データID\"");
                        s.AppendLine("    AND SR.\"履歴NO\" = SR_MAX.\"履歴NO\"");

                    }), TestCarHistory, true, true);

                //条件
                sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
                sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NULL");
                sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
                sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
                sql.AppendLine("    AND SR.\"STEPNO\" = 0");
                sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");
                sql.AppendLine("    AND LAST_DAY(ADD_MONTHS(SYSDATE,-1)) >= SR_MAX.\"処理日\"");

                //表示種別ごとの分岐
                switch (cond.VIEW_TYPE)
                {
                    //管理責任部署用
                    case ViewType.ManagementResponsibility:
                        sql.AppendLine("    AND SG.\"SECTION_ID\" = :USER_SECTION_ID");

                        this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                        break;

                    //全て
                    case ViewType.All:
                        //管理所在地
                        if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                        {
                            sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                            this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                        }
                        break;

                }

                //抽出条件
                sql.Append(where);

                //使用履歴なし
                sql.AppendLine("UNION ALL");
                sql.Append(base.GetBaseTestCarSql(
                    (s =>
                    {
                        s.AppendLine("    ,CAST(1 AS NUMBER(8)) AS \"SEQNO\"");
                        s.AppendLine("    ,NULL AS \"承認状況\"");
                        s.AppendLine("    ,CAST(-1 AS NUMBER(8)) AS \"STEPNO\"");
                        s.AppendLine("    ,'2' AS \"承認者レベル\"");
                        s.AppendLine("    ,'0' AS \"管理部署承認\"");
                        s.AppendLine("    ,NULL AS \"移管先部署ID\"");
                        s.AppendLine("    ,'C' AS \"承認要件コード\"");
                        s.AppendLine("    ,NULL AS \"入力駐車場番号\"");
                        s.AppendLine("    ,NULL AS \"承認要件名\"");
                        s.AppendLine("    ,NULL AS \"駐車場指定\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_ID\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_CODE\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_SECTION_ID\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_SECTION_CODE\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_ID\"");
                        s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_CODE\"");

                    }), null, TestCarHistory, true, true));

                //条件
                sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
                sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NULL");
                sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
                sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
                sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");
                sql.AppendLine("    AND NOT EXISTS");
                sql.AppendLine("                    (");
                sql.AppendLine("                        SELECT");
                sql.AppendLine("                            *");
                sql.AppendLine("                        FROM");
                sql.AppendLine("                            \"使用履歴情報\" A");
                sql.AppendLine("                        WHERE 0 = 0");
                sql.AppendLine("                            AND A.\"データID\" = KR.\"データID\"");
                sql.AppendLine("                            AND A.\"履歴NO\" = KR.\"履歴NO\"");
                sql.AppendLine("                    )");

                //表示種別ごとの分岐
                switch (cond.VIEW_TYPE)
                {
                    //管理責任部署用
                    case ViewType.ManagementResponsibility:
                        sql.AppendLine("    AND SG.\"SECTION_ID\" = :USER_SECTION_ID");

                        this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                        break;

                    //全て
                    case ViewType.All:
                        //管理所在地
                        if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                        {
                            sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                            this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                        }
                        break;

                }

                //抽出条件
                sql.Append(where);

                sql.AppendLine("UNION ALL");

            }

            //2ヶ月以上未入力
            sql.Append(base.GetBaseTestCarSql(
                (s =>
                {
                    s.AppendLine("    ,SR.\"SEQNO\"");
                    s.AppendLine("    ,SR.\"承認状況\"");
                    s.AppendLine("    ,CAST(-2 AS NUMBER(8)) AS \"STEPNO\"");
                    s.AppendLine("    ,'2' AS \"承認者レベル\"");
                    s.AppendLine("    ,'0' AS \"管理部署承認\"");
                    s.AppendLine("    ,NULL AS \"移管先部署ID\"");
                    s.AppendLine("    ,'C' AS \"承認要件コード\"");
                    s.AppendLine("    ,SR.\"駐車場番号\" AS \"入力駐車場番号\"");
                    s.AppendLine("    ,SR.\"承認要件名\"");
                    s.AppendLine("    ,SR.\"駐車場指定\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_CODE\"");

                }),
                (s =>
                {
                    AddTableSiyouRireki(s);
                    AddTableBusyo(s);

                    s.AppendLine("    INNER JOIN");
                    s.AppendLine("                (");
                    s.AppendLine("                    SELECT");
                    s.AppendLine("                         A.\"データID\"");
                    s.AppendLine("                        ,A.\"履歴NO\"");
                    s.AppendLine("                        ,MAX(A.\"処理日\") AS \"処理日\"");
                    s.AppendLine("                    FROM");
                    s.AppendLine("                        \"使用履歴情報\" A");
                    s.AppendLine("                    WHERE 0 = 0");
                    s.AppendLine("                    GROUP BY");
                    s.AppendLine("                         A.\"データID\"");
                    s.AppendLine("                        ,A.\"履歴NO\"");
                    s.AppendLine("                ) SR_MAX");
                    s.AppendLine("    ON SR.\"データID\" = SR_MAX.\"データID\"");
                    s.AppendLine("    AND SR.\"履歴NO\" = SR_MAX.\"履歴NO\"");

                }), TestCarHistory, true, true, "/*+ LEADING (JS JD SM) USE_NL(JD SM) */"));

            //条件
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NULL");
            sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND SR.\"STEPNO\" = 0");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");
            sql.AppendLine("    AND LAST_DAY(ADD_MONTHS(SYSDATE,-2)) >= SR_MAX.\"処理日\"");

            //表示種別ごとの分岐
            switch (cond.VIEW_TYPE)
            {
                //管理責任部署用
                case ViewType.ManagementResponsibility:
                    sql.AppendLine("    AND SG.\"SECTION_ID\" = :USER_SECTION_ID");

                    this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                    break;

                //全て
                case ViewType.All:
                    //管理所在地
                    if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                    {
                        sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                        this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                    }
                    break;

            }

            //抽出条件
            sql.Append(where);

            return sql;

        }

        /// <summary>
        /// 廃却申請のSQL取得
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="prms">パラメータ</param>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private StringBuilder GetHaikyakuSinsei(StringBuilder where, List<BindModel> prms, ApplicationApprovalCarSearchModel cond)
        {
            //使用履歴あり
            var sql = base.GetBaseTestCarSql(
                (s =>
                {
                    s.AppendLine("    ,SR.\"SEQNO\"");
                    s.AppendLine("    ,SR.\"承認状況\"");
                    s.AppendLine("    ,CAST(-1 AS NUMBER(8)) AS \"STEPNO\"");
                    s.AppendLine("    ,'2' AS \"承認者レベル\"");
                    s.AppendLine("    ,'0' AS \"管理部署承認\"");
                    s.AppendLine("    ,NULL AS \"移管先部署ID\"");
                    s.AppendLine("    ,'D' AS \"承認要件コード\"");
                    s.AppendLine("    ,SR.\"駐車場番号\" AS \"入力駐車場番号\"");
                    s.AppendLine("    ,SR.\"承認要件名\"");
                    s.AppendLine("    ,SR.\"駐車場指定\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_CODE\"");

                }), 
                (s => 
                {
                    AddTableSiyouRireki(s);

                }), 
                TestCarHistory, true, true);

            //条件
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NULL");
            sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND SR.\"STEPNO\" = 0");
            sql.AppendLine("    AND KR.\"種別\" <> 'リース'");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

            //期限前車両を取得するかどうか
            if ((cond.EARLY_CAR ?? false) == false)
            {
                sql.AppendLine("    AND (TRUNC(ADD_MONTHS(KR.\"使用期限\",-2),'MM') + 19) <= SYSDATE");

            }

            //表示種別ごとの分岐
            switch (cond.VIEW_TYPE)
            {
                //管理責任部署用
                case ViewType.ManagementResponsibility:
                    sql.AppendLine("    AND SG.\"SECTION_ID\" = :USER_SECTION_ID");

                    this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                    break;

                //全て
                case ViewType.All:
                    //管理所在地
                    if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                    {
                        sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                        this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                    }
                    break;

            }

            //抽出条件
            sql.Append(where);

            //使用履歴なし
            sql.AppendLine("UNION ALL");
            sql.Append(base.GetBaseTestCarSql(
                (s =>
                {
                    s.AppendLine("    ,CAST(1 AS NUMBER(8)) AS \"SEQNO\"");
                    s.AppendLine("    ,NULL AS \"承認状況\"");
                    s.AppendLine("    ,CAST(-1 AS NUMBER(8)) AS \"STEPNO\"");
                    s.AppendLine("    ,'2' AS \"承認者レベル\"");
                    s.AppendLine("    ,'0' AS \"管理部署承認\"");
                    s.AppendLine("    ,NULL AS \"移管先部署ID\"");
                    s.AppendLine("    ,'D' AS \"承認要件コード\"");
                    s.AppendLine("    ,NULL AS \"入力駐車場番号\"");
                    s.AppendLine("    ,NULL AS \"承認要件名\"");
                    s.AppendLine("    ,NULL AS \"駐車場指定\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_CODE\"");

                }), null, TestCarHistory, true, true));

            //条件
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NULL");
            sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND KR.\"種別\" <> 'リース'");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");
            sql.AppendLine("    AND NOT EXISTS");
            sql.AppendLine("                    (");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                            *");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            \"使用履歴情報\" A");
            sql.AppendLine("                        WHERE 0 = 0");
            sql.AppendLine("                            AND A.\"データID\" = KR.\"データID\"");
            sql.AppendLine("                            AND A.\"履歴NO\" = KR.\"履歴NO\"");
            sql.AppendLine("                    )");

            //期限前車両を取得するかどうか
            if ((cond.EARLY_CAR ?? false) == false)
            {
                sql.AppendLine("    AND (TRUNC(ADD_MONTHS(KR.\"使用期限\",-2),'MM') + 19) <= SYSDATE");

            }

            //表示種別ごとの分岐
            switch (cond.VIEW_TYPE)
            {
                //管理責任部署用
                case ViewType.ManagementResponsibility:
                    sql.AppendLine("    AND SG.\"SECTION_ID\" = :USER_SECTION_ID");

                    this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                    break;

                //全て
                case ViewType.All:
                    //管理所在地
                    if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                    {
                        sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                        this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                    }
                    break;
            }

            //抽出条件
            sql.Append(where);

            return sql;

        }

        /// <summary>
        /// 移管のSQL取得
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="prms">パラメータ</param>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private StringBuilder GetIkan(StringBuilder where, List<BindModel> prms, ApplicationApprovalCarSearchModel cond)
        {
            var syozokuCd = this.GetSyozokuCode(cond.ESTABLISHMENT);
            var syozoku = ":SYOZOKU";

            //全てを選択なら管理責任部署の所属コードを設定
            if (syozokuCd == All)
            {
                syozoku = "SG.\"ESTABLISHMENT\"";

            }
            else
            {
                //所属
                this.AddParameter(prms, ":SYOZOKU", syozokuCd, OracleDbType.Varchar2);

            }

            //使用履歴あり
            var sql = base.GetBaseTestCarSql(
                (s =>
                {
                    s.AppendLine("    ,SR.\"SEQNO\"");
                    s.AppendLine("    ,SR.\"承認状況\"");
                    s.AppendLine("    ,CAST(0 AS NUMBER(8)) AS \"STEPNO\"");
                    s.AppendLine("    ,'2' AS \"承認者レベル\"");
                    s.AppendLine("    ,'0' AS \"管理部署承認\"");
                    s.AppendLine("    ,NULL AS \"移管先部署ID\"");
                    s.AppendFormat("    ,('X' || {0}) AS \"承認要件コード\"", syozoku).AppendLine();
                    s.AppendLine("    ,SR.\"駐車場番号\" AS \"入力駐車場番号\"");
                    s.AppendLine("    ,SR.\"承認要件名\"");
                    s.AppendLine("    ,SR.\"駐車場指定\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_CODE\"");

                }),
                (s =>
                {
                    AddTableSiyouRireki(s);

                }),
                TestCarHistory, true, true);

            //条件
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NULL");
            sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND SR.\"STEPNO\" = 0");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

            //表示種別ごとの分岐
            switch (cond.VIEW_TYPE)
            {
                //管理責任部署用
                case ViewType.ManagementResponsibility:
                    sql.AppendLine("    AND SG.\"SECTION_ID\" = :USER_SECTION_ID");

                    this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                    break;

                //全て
                case ViewType.All:
                    //管理所在地
                    if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                    {
                        sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                        this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                    }
                    break;

            }

            //抽出条件
            sql.Append(where);

            //使用履歴なし
            sql.AppendLine("UNION ALL");
            sql.Append(base.GetBaseTestCarSql(
                (s =>
                {
                    s.AppendLine("    ,CAST(1 AS NUMBER(8)) AS \"SEQNO\"");
                    s.AppendLine("    ,NULL AS \"承認状況\"");
                    s.AppendLine("    ,CAST(0 AS NUMBER(8)) AS \"STEPNO\"");
                    s.AppendLine("    ,'2' AS \"承認者レベル\"");
                    s.AppendLine("    ,'0' AS \"管理部署承認\"");
                    s.AppendLine("    ,NULL AS \"移管先部署ID\"");
                    s.AppendFormat("    ,('X' || {0}) AS \"承認要件コード\"", syozoku).AppendLine();
                    s.AppendLine("    ,NULL AS \"入力駐車場番号\"");
                    s.AppendLine("    ,NULL AS \"承認要件名\"");
                    s.AppendLine("    ,NULL AS \"駐車場指定\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_CODE\"");

                }), null, TestCarHistory, true, true));

            //条件
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NULL");
            sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");
            sql.AppendLine("    AND NOT EXISTS");
            sql.AppendLine("                    (");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                            *");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            \"使用履歴情報\" A");
            sql.AppendLine("                        WHERE 0 = 0");
            sql.AppendLine("                            AND A.\"データID\" = KR.\"データID\"");
            sql.AppendLine("                            AND A.\"履歴NO\" = KR.\"履歴NO\"");
            sql.AppendLine("                    )");

            //表示種別ごとの分岐
            switch (cond.VIEW_TYPE)
            {
                //管理責任部署用
                case ViewType.ManagementResponsibility:
                    sql.AppendLine("    AND SG.\"SECTION_ID\" = :USER_SECTION_ID");

                    this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                    break;

                //全て
                case ViewType.All:
                    //管理所在地
                    if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                    {
                        sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                        this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                    }
                    break;

            }

            //抽出条件
            sql.Append(where);

            //管理責任部署を選択か全てで所属も全て以外のみ処理中(移管先部署)を取得
            if (cond.VIEW_TYPE == ViewType.ManagementResponsibility || (cond.VIEW_TYPE == ViewType.All && syozokuCd != All))
            {
                //処理中(移管先部署)
                sql.AppendLine("UNION ALL");
                sql.Append(base.GetBaseTestCarSql(
                    (s =>
                    {
                        AddColumnSiyouRireki(s);
                        AddColumnBusyo(s);

                    }), 
                    (s =>
                    {
                        AddTableSiyouRireki(s);
                        AddTableBusyo(s);

                    }),
                    TestCarHistory, true, true));

                //条件
                sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
                sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
                sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
                sql.AppendLine("    AND SR.\"STEPNO\" <> 0");
                sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

                //表示種別ごとの分岐
                switch (cond.VIEW_TYPE)
                {
                    //管理責任部署用
                    case ViewType.ManagementResponsibility:
                        sql.AppendLine("    AND SG.\"SECTION_ID\" <> :USER_SECTION_ID");
                        sql.AppendLine("    AND SI.\"SECTION_ID\" = :USER_SECTION_ID");

                        this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                        break;

                    //全て
                    case ViewType.All:
                        //管理所在地
                        if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                        {
                            sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                            this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                        }

                        //所属ごとの分岐
                        switch (syozokuCd)
                        {
                            //群馬
                            case Gunma:
                                sql.AppendLine("    AND SR.\"承認要件コード\" = 'H'");
                                break;

                            //東京
                            case Toukyou:
                                sql.AppendLine("    AND SR.\"承認要件コード\" = 'G'");
                                break;

                        }
                        break;

                }

                //承認要件コードの条件を設定
                this.SetSyouninYoukenCdWhere(sql, cond);

                //抽出条件
                sql.Append(where);

            }

            return sql;

        }

        /// <summary>
        /// 処理中のSQL取得
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="prms">パラメータ</param>
        /// <param name="cond">検索条件</param>
        /// <param name="isIkan">移管先部署フラグ</param>
        /// <returns></returns>
        private StringBuilder GetSyorityuu(StringBuilder where, List<BindModel> prms, ApplicationApprovalCarSearchModel cond, bool isIkan = false)
        {
            var sql = base.GetBaseTestCarSql(
                (s =>
                {
                    AddColumnSiyouRireki(s);
                    AddColumnBusyo(s);

                }),
                (s =>
                {
                    AddTableSiyouRireki(s);
                    AddTableBusyo(s);

                }),
                TestCarHistory, true, true);

            //条件
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND SR.\"STEPNO\" <> 0");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

            //表示種別ごとの分岐
            switch (cond.VIEW_TYPE)
            {
                //管理責任部署用
                case ViewType.ManagementResponsibility:
                    sql.AppendLine("    AND SR.\"承認要件コード\" NOT IN ('A')");
                    sql.AppendLine("    AND SG.\"SECTION_ID\" = :USER_SECTION_ID");

                    this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                    break;

                //全て
                case ViewType.All:
                    //管理所在地
                    if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                    {
                        if (!isIkan)
                        {
                            sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");
                        }
                        else
                        {
                            sql.AppendLine("    AND SI.\"ESTABLISHMENT\" = :ESTABLISHMENT");
                        }

                        this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                    }
                    break;

            }

            //承認要件コードの条件を設定
            this.SetSyouninYoukenCdWhere(sql, cond);

            //抽出条件
            sql.Append(where);

            return sql;

        }

        /// <summary>
        /// 処理済(廃却)のSQL取得
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="prms">パラメータ</param>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private StringBuilder GetSyorizumiHaikyaku(StringBuilder where, List<BindModel> prms, ApplicationApprovalCarSearchModel cond)
        {
            var sql = base.GetBaseTestCarSql(
                (s =>
                {
                    s.AppendLine("    ,SR.\"SEQNO\"");
                    s.AppendLine("    ,SR.\"承認状況\"");
                    s.AppendLine("    ,SR.\"STEPNO\"");
                    s.AppendLine("    ,SR.\"承認者レベル\"");
                    s.AppendLine("    ,SR.\"管理部署承認\"");
                    s.AppendLine("    ,NULL AS \"移管先部署ID\"");
                    s.AppendLine("    ,SR.\"承認要件コード\"");
                    s.AppendLine("    ,SR.\"駐車場番号\" AS \"入力駐車場番号\"");
                    s.AppendLine("    ,SR.\"承認要件名\"");
                    s.AppendLine("    ,SR.\"駐車場指定\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_DEPARTMENT_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_CODE\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_ID\"");
                    s.AppendLine("    ,NULL AS \"移管先部署_SECTION_GROUP_CODE\"");

                }),
                (s =>
                {
                    AddTableSiyouRireki(s);
                    AddTableBusyo(s);

                }),
                TestCarHistory, true, true);

            //条件
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KK.\"研実管理廃却申請受理日\" IS NOT NULL");
            sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND SR.\"STEPNO\" = 0");
            sql.AppendLine("    AND SR.\"承認要件コード\" = 'D'");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

            //表示種別ごとの分岐
            switch (cond.VIEW_TYPE)
            {
                //管理部署用
                case ViewType.Management:
                    sql.AppendLine("    AND SG.\"SECTION_ID\" = :USER_SECTION_ID");

                    this.AddParameter(prms, ":USER_SECTION_ID", this.user.SECTION_ID, OracleDbType.Varchar2);
                    break;

                //全て
                case ViewType.All:
                    //管理所在地
                    if (string.IsNullOrWhiteSpace(cond.ESTABLISHMENT) == false)
                    {
                        sql.AppendLine("    AND SG.\"ESTABLISHMENT\" = :ESTABLISHMENT");

                        this.AddParameter(prms, ":ESTABLISHMENT", cond.ESTABLISHMENT, OracleDbType.Varchar2);

                    }
                    break;

            }

            //抽出条件
            sql.Append(where);

            return sql;

        }

        /// <summary>
        /// 管理部署のSQL取得
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="prms">パラメータ</param>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private StringBuilder GetKanriBusyo(StringBuilder where, List<BindModel> prms, ApplicationApprovalCarSearchModel cond)
        {
            var sql = base.GetBaseTestCarSql(
                (s =>
                {
                    AddColumnSiyouRireki(s);
                    AddColumnBusyo(s);

                }),
                (s =>
                {
                    AddTableSiyouRireki(s);
                    AddTableBusyo(s);

                }),
                TestCarHistory, true, true);

            //条件
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KS.\"処分コード\" IS NULL");
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND SR.\"STEPNO\" <> 0");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");
            sql.AppendLine("    AND SR.\"管理部署承認\" LIKE '%' || :USER_SYOZOKU || '%'");

            //承認要件コードの条件を設定
            this.SetSyouninYoukenCdWhere(sql, cond);

            sql.AppendLine("    AND");
            sql.AppendLine("        (");

            var i = 0;
            foreach (var syubetu in this.user.管理部署種別)
            {
                if (i != 0)
                {
                    sql.AppendLine("            OR");
                }

                var name = string.Format(":KANRI{0}", i++);

                sql.AppendFormat("            SR.\"管理部署承認\" LIKE '%' || {0} || '%'", name).AppendLine();

                this.AddParameter(prms, name, syubetu, OracleDbType.Varchar2);

            }

            sql.AppendLine("        )");

            //パラメータ
            this.AddParameter(prms, ":USER_SYOZOKU", user.ESTABLISHMENT, OracleDbType.Varchar2);

            //抽出条件
            sql.Append(where);

            return sql;

        }

        /// <summary>
        /// 承認要件コードの条件を設定
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="cond">検索条件</param>
        /// <param name="tableName">テーブル名</param>
        private void SetSyouninYoukenCdWhere(StringBuilder sql, ApplicationApprovalCarSearchModel cond, string tableName = "SR")
        {
            var where = new StringBuilder();

            //取得種別ごとの分岐
            switch (cond.TARGET_TYPE)
            {
                //試験着手日
                case ApplicationApprovalCarTargetType.TestStartDay:
                    where.AppendLine("    AND SR.\"承認要件コード\" IN ('B')");
                    break;

                //月例点検
                case ApplicationApprovalCarTargetType.MonthlyInspection:
                    where.AppendLine("    AND SR.\"承認要件コード\" IN ('C')");
                    break;

                //廃却申請
                case ApplicationApprovalCarTargetType.DispositionApplication:
                    where.AppendLine("    AND SR.\"承認要件コード\" IN ('D')");
                    break;

                //T技本内移管
                case ApplicationApprovalCarTargetType.ToukyouTransfer:
                    where.AppendLine("    AND SR.\"承認要件コード\" IN ('F','Xt')");
                    break;

                //G技本内移管
                case ApplicationApprovalCarTargetType.GunmaTransfer:
                    where.AppendLine("    AND SR.\"承認要件コード\" IN ('E','Xg')");
                    break;

                //G→T移管
                case ApplicationApprovalCarTargetType.GtTransfer:
                    where.AppendLine("    AND SR.\"承認要件コード\" IN ('G','Xg')");

                    //東京かどうか
                    if (cond.ESTABLISHMENT == Toukyou)
                    {
                        where.AppendLine("    AND SR.\"承認要件コード\" NOT IN ('X','Xg','Xt')");

                    }
                    break;

                //T→G移管
                case ApplicationApprovalCarTargetType.TgTransfer:
                    where.AppendLine("    AND SR.\"承認要件コード\" IN ('H','Xt')");

                    //群馬かどうか
                    if (cond.ESTABLISHMENT == Gunma)
                    {
                        where.AppendLine("    AND SR.\"承認要件コード\" NOT IN ('X','Xg','Xt')");

                    }
                    break;

                //全て
                case ApplicationApprovalCarTargetType.All:
                    where.AppendLine("    AND NOT");
                    where.AppendLine("            (");
                    where.AppendLine("                SR.\"承認要件コード\" IN ('B')");
                    where.AppendLine("                AND");
                    where.AppendLine("                SR.\"STEPNO\" >= 0");
                    where.AppendLine("            )");

                    //管理所在地ごとの分岐
                    switch (cond.ESTABLISHMENT)
                    {
                        //群馬
                        case Gunma:
                            where.AppendLine("    AND SR.\"承認要件コード\" NOT IN ('H')");
                            break;

                        //東京
                        case Toukyou:
                            where.AppendLine("    AND SR.\"承認要件コード\" NOT IN ('G')");
                            break;

                    }
                    break;

            }

            //テーブル名の置換
            where.Replace("SR", tableName);

            //条件
            sql.Append(where);

        }

        /// <summary>
        /// パラメータの追加
        /// </summary>
        /// <param name="prms">パラメータ</param>
        /// <param name="name">名前</param>
        /// <param name="value">値</param>
        /// <param name="type">型</param>
        private void AddParameter(List<BindModel> prms, string name, object value, OracleDbType type)
        {
            //未追加なら追加
            if (prms.Any(x => x.Name == name) == false)
            {
                prms.Add(new BindModel { Name = name, Object = value, Type = type, Direct = ParameterDirection.Input });

            }

        }
        #endregion

        #region 使用履歴情報取得
        /// <summary>
        /// 使用履歴取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TestCarUseHistoryModel> GetSiyouRireki(TestCarUseHistorySearchModel cond)
        {
            return this.GetSiyouRireki(new ApplicationApprovalCarModel { データID = cond.データID, 履歴NO = cond.履歴NO }, false);

        }

        /// <summary>
        /// 使用履歴情報
        /// </summary>
        /// <param name="testCar">試験車</param>
        /// <param name="isNew">最新取得可否</param>
        /// <param name="code">承認要件コード</param>
        /// <returns></returns>
        private List<TestCarUseHistoryModel> GetSiyouRireki(ApplicationApprovalCarModel testCar, bool isNew = true, string code = null)
        {
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":データID", Object = testCar.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input }

            };

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"データID\"");
            sql.AppendLine("    ,A.\"履歴NO\"");
            sql.AppendLine("    ,A.\"SEQNO\"");
            sql.AppendLine("    ,A.\"承認要件コード\"");
            sql.AppendLine("    ,C.\"承認要件名\"");
            sql.AppendLine("    ,A.\"STEPNO\"");
            sql.AppendLine("    ,A.\"承認状況\"");
            sql.AppendLine("    ,A.\"承認者レベル\"");
            sql.AppendLine("    ,A.\"管理部署承認\"");
            sql.AppendLine("    ,A.\"処理日\"");
            sql.AppendLine("    ,A.\"管理責任課名\"");
            sql.AppendLine("    ,A.\"管理責任部署名\"");
            sql.AppendLine("    ,(A.\"管理責任課名\" || ' ' || A.\"管理責任部署名\") AS \"管理責任部署\"");
            sql.AppendLine("    ,A.\"使用課名\"");
            sql.AppendLine("    ,A.\"使用部署名\"");
            sql.AppendLine("    ,(A.\"使用課名\" || ' ' || A.\"使用部署名\") AS \"使用部署\"");
            sql.AppendLine("    ,A.\"試験内容\"");
            sql.AppendLine("    ,A.\"工事区分NO\"");
            sql.AppendLine("    ,A.\"実走行距離\"");
            sql.AppendLine("    ,A.\"編集日\"");
            sql.AppendLine("    ,A.\"編集者\"");
            sql.AppendLine("    ,D.\"NAME\" AS \"編集者_NAME\"");
            sql.AppendLine("    ,A.\"移管先部署ID\"");
            sql.AppendLine("    ,H.\"DEPARTMENT_ID\" AS \"移管先部署_DEPARTMENT_ID\"");
            sql.AppendLine("    ,H.\"DEPARTMENT_CODE\" AS \"移管先部署_DEPARTMENT_CODE\"");
            sql.AppendLine("    ,G.\"SECTION_ID\" AS \"移管先部署_SECTION_ID\"");
            sql.AppendLine("    ,G.\"SECTION_CODE\" AS \"移管先部署_SECTION_CODE\"");
            sql.AppendLine("    ,F.\"SECTION_GROUP_ID\" AS \"移管先部署_SECTION_GROUP_ID\"");
            sql.AppendLine("    ,F.\"SECTION_GROUP_CODE\" AS \"移管先部署_SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,A.\"駐車場番号\"");
            sql.AppendLine("    ,E.\"駐車場指定\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"使用履歴情報\" A");

            //最新の使用履歴情報のみ取得するかどうか
            if (isNew == true)
            {
                sql.AppendLine("    INNER JOIN");
                sql.AppendLine("                (");
                sql.AppendLine("                    SELECT");
                sql.AppendLine("                         A.\"データID\"");
                sql.AppendLine("                        ,A.\"履歴NO\"");
                sql.AppendLine("                        ,MAX(A.\"SEQNO\") AS \"SEQNO\"");
                sql.AppendLine("                    FROM");
                sql.AppendLine("                        \"使用履歴情報\" A");
                sql.AppendLine("                    WHERE 0 = 0");
                sql.AppendLine("                        AND A.\"データID\" = :データID");
                sql.AppendLine("                        AND A.\"履歴NO\" = :履歴NO");
                sql.AppendLine("                    GROUP BY");
                sql.AppendLine("                         A.\"データID\"");
                sql.AppendLine("                        ,A.\"履歴NO\"");
                sql.AppendLine("                ) B");
                sql.AppendLine("    ON A.\"データID\" = B.\"データID\"");
                sql.AppendLine("    AND A.\"履歴NO\" = B.\"履歴NO\"");
                sql.AppendLine("    AND A.\"SEQNO\" = B.\"SEQNO\"");

            }

            sql.AppendLine("    INNER JOIN \"承認要件マスタ\" C");
            sql.AppendLine("    ON A.\"承認要件コード\" = C.\"承認要件コード\"");
            sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" D");
            sql.AppendLine("    ON A.\"編集者\" = D.\"PERSONEL_ID\"");
            sql.AppendLine("    LEFT JOIN \"承認STEPマスタ\" E");
            sql.AppendLine("    ON A.\"承認要件コード\" = E.\"承認要件コード\"");
            sql.AppendLine("    AND A.\"STEPNO\" = E.\"STEPNO\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_GROUP_DATA\" F");
            sql.AppendLine("    ON A.\"移管先部署ID\" = F.\"SECTION_GROUP_ID\"");
            sql.AppendLine("    LEFT JOIN \"SECTION_DATA\" G");
            sql.AppendLine("    ON F.\"SECTION_ID\" = G.\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"DEPARTMENT_DATA\" H");
            sql.AppendLine("    ON G.\"DEPARTMENT_ID\" = H.\"DEPARTMENT_ID\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"データID\" = :データID");

            //履歴NO
            if (testCar.履歴NO > 0)
            {
                sql.AppendLine("    AND A.\"履歴NO\" = :履歴NO");

                prms.Add(new BindModel { Name = ":履歴NO", Object = testCar.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input });

            }

            //承認要件コード
            if (string.IsNullOrWhiteSpace(code) == false)
            {
                prms.Add(new BindModel { Name = ":承認要件コード", Object = code, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });

            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"データID\"");
            sql.AppendLine("    ,A.\"履歴NO\"");
            sql.AppendLine("    ,A.\"SEQNO\"");

            //取得
            return db.ReadModelList<TestCarUseHistoryModel>(sql.ToString(), prms);

        }

        /// <summary>
        /// 処理日の最大を取得
        /// </summary>
        /// <param name="history">試験車使用履歴</param>
        /// <param name="isFull">全件チェック</param>
        /// <returns></returns>
        public DateTime? GetMaxSyoriDay(TestCarUseHistoryModel history, bool isFull = false)
        {
            return this.GetMaxSyoriDay(new ApplicationApprovalCarModel() { データID = history.データID, 履歴NO = history.履歴NO, SEQNO = history.SEQNO }, isFull);

        }

        /// <summary>
        /// 処理日の最大を取得
        /// </summary>
        /// <param name="testCar">試験車</param>
        /// <param name="isFull">全件チェック</param>
        /// <returns></returns>
        public DateTime? GetMaxSyoriDay(ApplicationApprovalCarModel testCar, bool isFull = false)
        {
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":データID", Object = testCar.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                new BindModel { Name = ":履歴NO", Object = testCar.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                new BindModel { Name = ":OWNSEQNO", Object = testCar.SEQNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input }

            };

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    MAX(A.\"処理日\") AS 処理日");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"使用履歴情報\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"データID\" = :データID");
            sql.AppendLine("    AND A.\"履歴NO\" = :履歴NO");
            if (!isFull)
            {
                sql.AppendLine("    AND A.\"SEQNO\" <> :OWNSEQNO");
            }
            sql.AppendLine("    AND A.\"STEPNO\" = 0");
            sql.AppendLine("    AND A.\"承認要件コード\" NOT IN ('J','K','L','M','N','O')");
            sql.AppendLine("    AND NOT EXISTS");
            sql.AppendLine("                    (");
            sql.AppendLine("                        SELECT");
            sql.AppendLine("                            *");
            sql.AppendLine("                        FROM");
            sql.AppendLine("                            \"使用履歴情報\" B");
            sql.AppendLine("                        WHERE 0 = 0");
            sql.AppendLine("                            AND B.\"データID\" = :データID");
            sql.AppendLine("                            AND B.\"履歴NO\" = :履歴NO");
            sql.AppendLine("                            AND B.\"承認要件コード\" IN ('K','L','M','N','O')");
            sql.AppendLine("                            AND A.\"SEQNO\" = (B.\"SEQNO\" - 1)");
            sql.AppendLine("                    )");

            //発行中止があれば除外
            var i = 0;
            foreach (var tyuusi in this.GetSiyouRireki(testCar, code: Tyuusi))
            {
                sql.AppendLine("    AND NOT EXISTS");
                sql.AppendLine("                    (");
                sql.AppendLine("                        SELECT");
                sql.AppendLine("                            *");
                sql.AppendLine("                        FROM");
                sql.AppendLine("                            (");
                sql.AppendLine("                                SELECT");
                sql.AppendLine("                                    MAX(B.\"SEQNO\") AS \"SEQNO\"");
                sql.AppendLine("                                FROM");
                sql.AppendLine("                                    \"使用履歴情報\" B");
                sql.AppendLine("                                WHERE 0 = 0");
                sql.AppendLine("                                    AND B.\"データID\" = :データID");
                sql.AppendLine("                                    AND B.\"履歴NO\" = :履歴NO");
                sql.AppendLine("                                    AND B.\"承認要件コード\" IN ('A')");
                sql.AppendFormat("                                    AND B.\"SEQNO\" < :{0}", i).AppendLine();
                sql.AppendLine("                            ) C");
                sql.AppendLine("                        WHERE 0 = 0");
                sql.AppendLine("                            AND A.\"SEQNO\" = C.\"SEQNO\"");
                sql.AppendLine("                    )");

                prms.Add(new BindModel { Name = string.Format(":{0}", i++), Object = tyuusi.SEQNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input });

            }

            //取得
            var max = db.ReadModelList<ApplicationApprovalCarModel>(sql.ToString(), prms).FirstOrDefault();
            return DateTimeUtil.GetDate(max?.処理日);

        }

        /// <summary>
        /// 使用履歴情報SEQNO取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private int GetTestCarUseHistorySeqNo(TestCarUseHistoryModel cond)
        {
            var parm = new List<BindModel>
            {
                new BindModel { Name = ":データID", Object = cond.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                new BindModel { Name = ":履歴NO", Object = cond.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input }

            };

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    CAST((NVL(MAX(A.\"SEQNO\"),0) + 1) AS NUMBER(8))AS \"SEQNO\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"使用履歴情報\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"データID\" = :データID");
            sql.AppendLine("    AND A.\"履歴NO\" = :履歴NO");

            //取得
            return db.ReadModelList<TestCarUseHistoryModel>(sql.ToString(), parm).First().SEQNO;

        }
        #endregion

        #region 承認STEPマスタ取得
        /// <summary>
        /// 承認STEPマスタ取得
        /// </summary>
        /// <param name="code">承認要件コード</param>
        /// <param name="isComplete">済取得可否</param>
        /// <returns></returns>
        public List<ApprovalStepModel> GetApprovalStep(string code = null, bool isComplete = false)
        {
            var parm = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"承認要件コード\"");
            sql.AppendLine("    ,A.\"STEPNO\"");
            sql.AppendLine("    ,A.\"STEP名\"");
            sql.AppendLine("    ,A.\"承認者レベル\"");
            sql.AppendLine("    ,A.\"管理部署承認\"");
            sql.AppendLine("    ,A.\"駐車場指定\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"承認STEPマスタ\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"承認要件コード\" NOT IN ('A')");

            //承認要件コード
            if (string.IsNullOrWhiteSpace(code) == false)
            {
                sql.AppendLine("    AND A.\"承認要件コード\" = :承認要件コード");

                parm.Add(new BindModel { Name = ":承認要件コード", Object = code, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });

            }

            //済を取得するかどうか
            if (isComplete == true)
            {
                sql.AppendLine("UNION ALL");
                sql.AppendLine("SELECT");
                sql.AppendLine("     NULL AS \"承認要件コード\"");
                sql.AppendLine("    ,0 AS \"STEPNO\"");
                sql.AppendLine("    ,'済' AS \"STEP名\"");
                sql.AppendLine("    ,NULL AS \"承認者レベル\"");
                sql.AppendLine("    ,NULL AS \"管理部署承認\"");
                sql.AppendLine("    ,NULL AS \"駐車場指定\"");
                sql.AppendLine("FROM");
                sql.AppendLine("    DUAL");

            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     \"承認要件コード\"");
            sql.AppendLine("    ,\"STEPNO\"");

            return db.ReadModelList<ApprovalStepModel>(sql.ToString(), parm);

        }
        #endregion

        #region 試験車使用履歴承認状況取得
        /// <summary>
        /// 試験車使用履歴承認状況取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TestCarUseHistoryApprovalModel> GetTestCarUseHistoryApproval(TestCarUseHistorySearchModel cond)
        {
            var parm = new List<BindModel>
            {
                new BindModel { Name = ":データID", Object = cond.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                new BindModel { Name = ":履歴NO", Object = cond.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SEQNO", Object = cond.SEQNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input }

            };

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"データID\"");
            sql.AppendLine("    ,A.\"履歴NO\"");
            sql.AppendLine("    ,A.\"SEQNO\"");
            sql.AppendLine("    ,A.\"STEPNO\"");
            sql.AppendLine("    ,C.\"承認要件コード\"");
            sql.AppendLine("    ,D.\"承認要件名\"");
            sql.AppendLine("    ,A.\"承認STEP名\"");
            sql.AppendLine("    ,A.\"承認者レベル\"");
            sql.AppendLine("    ,A.\"管理部署承認\"");
            sql.AppendLine("    ,A.\"承認日\"");
            sql.AppendLine("    ,A.\"承認者\"");
            sql.AppendLine("    ,B.\"NAME\" AS \"承認者_NAME\"");
            sql.AppendLine("    ,A.\"承認者所属課\"");
            sql.AppendLine("    ,A.\"承認者所属担当\" ");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"使用履歴承認情報\" A");
            sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" B");
            sql.AppendLine("    ON A.\"承認者\" = B.\"PERSONEL_ID\"");
            sql.AppendLine("    LEFT JOIN \"使用履歴情報\" C");
            sql.AppendLine("    ON A.\"データID\" = C.\"データID\"");
            sql.AppendLine("    AND A.\"履歴NO\" = C.\"履歴NO\"");
            sql.AppendLine("    AND A.\"SEQNO\" = C.\"SEQNO\"");
            sql.AppendLine("    LEFT JOIN \"承認要件マスタ\" D");
            sql.AppendLine("    ON C.\"承認要件コード\" = D.\"承認要件コード\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"データID\" = :データID");
            sql.AppendLine("    AND A.\"履歴NO\" = :履歴NO");
            sql.AppendLine("    AND A.\"SEQNO\" = :SEQNO");

            //STEPNO
            if (cond.STEPNO != null)
            {
                sql.AppendLine("    AND A.\"STEPNO\" = :STEPNO");

                parm.Add(new BindModel { Name = ":STEPNO", Object = cond.STEPNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input });

            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"データID\"");
            sql.AppendLine("    ,A.\"履歴NO\"");
            sql.AppendLine("    ,A.\"SEQNO\"");
            sql.AppendLine("    ,A.\"STEPNO\"");

            //取得
            return db.ReadModelList<TestCarUseHistoryApprovalModel>(sql.ToString(), parm);

        }
        #endregion

        #region 試験車使用履歴権限チェック
        /// <summary>
        /// 試験車使用履歴権限チェック
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        public List<ApplicationApprovalCarModel> IsTestCarUseHistoryAuthority(List<ApplicationApprovalCarModel> list)
        {
            //対象があるかどうか
            if (list != null && list.Any() == true)
            {
                var userMap = base.GetLogic<UserLogic>().GetData(new UserSearchInModel { PERSONEL_ID = list.Select(x => x.PERSONEL_ID).Distinct().ToArray(), MANAGE_FLG = true }).ToDictionary(x => x.PERSONEL_ID, x => x);

                foreach (var testCar in list)
                {
                    var user = userMap[testCar.PERSONEL_ID];
                    var accessLevel = this.GetAccessLevel(user);
                    var kanri = this.GetKanriBusyoSyubetu(user);

                    var isIppan = kanri == Ippan;

                    var isUser = isIppan == true || (isIppan == false && testCar.VIEW_TYPE == ViewType.ManagementResponsibility);

                    var kengen = testCar.ACCESS_LEVEL.Substring(0, 1);

                    var syounin = (testCar.管理部署承認 ?? "");

                    //チェック結果
                    testCar.CHECK_RESULT = CheckResultType.Ok;

                    //STEPNOが0以上かどうか
                    if (testCar.STEPNO >= 0)
                    {
                        //承認者レベルがOKかどうか
                        if (testCar.承認者レベル != kengen)
                        {
                            //チェック結果
                            testCar.CHECK_RESULT = CheckResultType.NoApproval;
                            continue;

                        }

                    }
                    else
                    {
                        kengen = accessLevel;

                    }

                    //権限が同じかどうか
                    if (kengen != accessLevel)
                    {
                        //チェック結果
                        testCar.CHECK_RESULT = CheckResultType.NoAuthority;

                    }
                    else
                    {
                        //ユーザー権限かどうか
                        if (isUser == true)
                        {
                            //管理部署承認が必要かどうか
                            if (testCar.管理部署承認 != None)
                            {
                                //チェック結果
                                testCar.CHECK_RESULT = CheckResultType.NoAuthority;

                            }
                            else
                            {
                                //月例点検かどうか
                                if (testCar.TARGET_TYPE != ApplicationApprovalCarTargetType.MonthlyInspection)
                                {
                                    //移管先部署の承認かどうか
                                    if ((testCar.承認状況 ?? "").Contains(IkansakiBusyo) == true)
                                    {
                                        //移管先とユーザーの課IDが同じかどうか
                                        if (testCar.移管先部署_SECTION_ID != user.SECTION_ID)
                                        {
                                            //チェック結果
                                            testCar.CHECK_RESULT = CheckResultType.NoAuthority;
                                            continue;

                                        }

                                        //ユーザーが担当で移管先とユーザーの担当IDが同じかどうか
                                        if (accessLevel == Tantou && testCar.移管先部署ID != user.SECTION_GROUP_ID)
                                        {
                                            //チェック結果
                                            testCar.CHECK_RESULT = CheckResultType.NoAuthority;
                                            continue;

                                        }

                                    }
                                    else
                                    {
                                        //担当かどうか
                                        if (accessLevel == Tantou)
                                        {
                                            //管理責任部署とユーザーの課IDと担当IDが同じかどうか
                                            testCar.CHECK_RESULT = testCar.SECTION_ID == user.SECTION_ID && testCar.SECTION_GROUP_ID == user.SECTION_GROUP_ID ? CheckResultType.Ok : CheckResultType.NoAuthority;

                                        }
                                        else
                                        {
                                            //管理責任部署とユーザーの課IDが同じかどうか
                                            testCar.CHECK_RESULT = testCar.SECTION_ID == user.SECTION_ID ? CheckResultType.Ok : CheckResultType.NoAuthority;

                                        }

                                    }

                                }

                            }

                        }
                        else
                        {
                            //ユーザーの管理部署種別と所属が管理部署承認かどうか
                            testCar.CHECK_RESULT = Regex.IsMatch(syounin, kanri) == true && syounin.Contains(user.ESTABLISHMENT) == true ? CheckResultType.Ok : CheckResultType.NoAuthority;

                        }

                    }

                    //チェック結果がOKかユーザーのみ対象かどうか
                    if (testCar.CHECK_RESULT == CheckResultType.Ok || (testCar.USER_ONLY ?? false) == true)
                    {
                        continue;

                    }

                    //管理部署承認が必要かどうか
                    if (testCar.管理部署承認 == None)
                    {
                        //ユーザー権限かどうか
                        if (isUser == false)
                        {
                            //チェック結果
                            testCar.CHECK_RESULT = CheckResultType.Ok;
                            continue;

                        }
                        else
                        {
                            //月例点検かどうか
                            if (testCar.TARGET_TYPE != ApplicationApprovalCarTargetType.MonthlyInspection)
                            {
                                //移管先部署の承認かどうか
                                if ((testCar.承認状況 ?? "").Contains(IkansakiBusyo) == true)
                                {
                                    //移管先とユーザーの課IDが同じかどうか
                                    if (testCar.移管先部署_SECTION_ID != user.SECTION_ID)
                                    {
                                        //チェック結果
                                        testCar.CHECK_RESULT = CheckResultType.NoAuthority;
                                        continue;

                                    }

                                }
                                else
                                {
                                    //管理責任部署とユーザーの課IDが同じかどうか
                                    if (testCar.SECTION_ID != user.SECTION_ID)
                                    {
                                        //チェック結果
                                        testCar.CHECK_RESULT = CheckResultType.NoAuthority;
                                        continue;

                                    }

                                }

                            }

                            //一般以外のユーザーの承認者レベルかユーザーと承認者レベルが担当かどうか
                            if (testCar.承認者レベル.CompareTo(accessLevel) > 0 || (accessLevel == Tantou && testCar.承認者レベル == Tantou))
                            {
                                //チェック結果
                                testCar.CHECK_RESULT = CheckResultType.Ok;
                                continue;

                            }
                            else
                            {
                                //チェック結果
                                testCar.CHECK_RESULT = CheckResultType.NoAuthority;
                                continue;

                            }

                        }

                    }
                    else
                    {
                        //ユーザーかどうか
                        if (isUser == true)
                        {
                            //チェック結果
                            testCar.CHECK_RESULT = CheckResultType.NoAuthority;
                            continue;

                        }

                        //一般以外のユーザーの承認者レベルかユーザーが担当で承認者レベルが課長かどうか
                        if (testCar.承認者レベル.CompareTo(accessLevel) > 0 || (accessLevel == Tantousya && testCar.承認者レベル == Katyou))
                        {
                            //チェック結果
                            testCar.CHECK_RESULT = CheckResultType.Ok;
                            continue;

                        }
                        else
                        {
                            //チェック結果
                            testCar.CHECK_RESULT = CheckResultType.NoAuthority;
                            continue;

                        }

                    }

                }

            }

            return list;

        }
        #endregion

        #region 試験車使用履歴入力チェック
        /// <summary>
        /// 試験車使用履歴入力チェック
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        public List<ApplicationApprovalCarModel> IsTestCarUseHistoryInput(List<ApplicationApprovalCarModel> list)
        {
            var today = DateTime.Today;
            var zengetu = new DateTime(today.Year, today.Month, 1).AddMonths(-1);

            //対象があるかどうか
            if (list != null && list.Any(x => x.CHECK_RESULT == CheckResultType.Ok) == true)
            {
                foreach (var testCar in list)
                {
                    //チェック結果がOK以外、または月例点検承認省略は次へ
                    if (testCar.CHECK_RESULT != CheckResultType.Ok || testCar.月例点検省略有無 == 1)
                    {
                        continue;

                    }

                    //処理日があるかどうか
                    var day = this.GetMaxSyoriDay(testCar, true);
                    if (day != null)
                    {
                        //処理日の最大が前月以前ならエラー
                        var ym = new DateTime(day.Value.Year, day.Value.Month, 1);

                        //過去の不正データ対応（例:0006年→2006年）
                        if (ym < new DateTime(2000, 1, 1))
                        {
                            var culture = new System.Globalization.CultureInfo("ja-JP");
                            culture.Calendar.TwoDigitYearMax = 2099;

                            ym = DateTime.Parse(ym.ToString("yy/MM/dd"), culture);
                        }

                        if (ym < zengetu)
                        {
                            testCar.CHECK_RESULT = CheckResultType.MonthlyInput;

                        }

                    }

                }

            }

            return list;

        }
        #endregion

        #region 試験車使用履歴変更チェック
        /// <summary>
        /// 試験車使用履歴変更チェック
        /// </summary>
        /// <param name="list">試験車</param>param>
        /// <returns></returns>
        public List<ApplicationApprovalCarModel> IsTestCarUseHistoryChange(List<ApplicationApprovalCarModel> list)
        {
            //対象があるかどうか
            if (list != null && list.Any(x => x.CHECK_RESULT == CheckResultType.Ok) == true)
            {
                foreach (var testCar in list)
                {
                    //チェック結果がOK以外は次へ
                    if (testCar.CHECK_RESULT != CheckResultType.Ok)
                    {
                        continue;

                    }

                    //使用履歴があるかどうか
                    var siyouRireki = this.GetSiyouRireki(testCar).FirstOrDefault();
                    if (siyouRireki != null)
                    {
                        //SEQNOが同じかどうか
                        if (testCar.SEQNO != siyouRireki.SEQNO)
                        {
                            testCar.CHECK_RESULT = CheckResultType.Update;
                            continue;

                        }

                        //処理対象かどうか
                        if (testCar.STEPNO <= 0)
                        {
                            //処理中かどうか
                            if (siyouRireki.STEPNO > 0)
                            {
                                testCar.CHECK_RESULT = CheckResultType.Update;
                                continue;

                            }

                        }
                        else
                        {
                            //承認要件コートが異なるかSTEPNOが異なるかどうか
                            if (testCar.承認要件コード != siyouRireki.承認要件コード || testCar.STEPNO != siyouRireki.STEPNO)
                            {
                                testCar.CHECK_RESULT = CheckResultType.Update;
                                continue;

                            }

                        }

                    }

                }

            }

            return list;

        }
        #endregion

        #region 試験車使用履歴申請
        /// <summary>
        /// 試験車使用履歴申請
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        public bool ApplyTestCarUseHistory(List<ApplicationApprovalCarModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //対象があるかどうか
            if (list != null && list.Any(x => x.CHECK_RESULT == CheckResultType.Ok) == true)
            {
                var target = list.Where(x => x.CHECK_RESULT == CheckResultType.Ok).ToList();

                //使用履歴情報の追加
                results.Add(this.InsertSiyouRireki(target));
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
        /// 使用履歴情報の追加
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        private bool InsertSiyouRireki(List<ApplicationApprovalCarModel> list)
        {
            return this.InsertUseHistoryInfo(list.Select(x =>
            {
                var kanriKa = x.SECTION_CODE;
                var kanriTantou = x.SECTION_GROUP_CODE;

                var siyouKa = x.使用課名 ?? string.Empty;
                var siyouTantou = x.使用部署名 ?? string.Empty;

                //履歴があるかどうか
                if (x.SEQNO != 1)
                {
                    //1つ前の履歴から取得
                    var history = this.GetSiyouRireki(x, isNew: false).FirstOrDefault(y => y.SEQNO == x.SEQNO - 1);
                    if (history != null)
                    {
                        siyouKa = history.使用課名;
                        siyouTantou = history.使用部署名;

                    }

                }
                else
                {
                    siyouKa = kanriKa;
                    siyouTantou = kanriTantou;

                }

                return new TestCarUseHistoryModel
                {
                    //データID
                    データID = x.データID,

                    //履歴NO
                    履歴NO = x.履歴NO,

                    //SEQNO
                    SEQNO = x.SEQNO,

                    //承認要件コード
                    承認要件コード = x.承認要件コード,

                    //STEPNO
                    STEPNO = x.STEPNO,

                    //承認状況
                    承認状況 = x.承認状況,

                    //承認者レベル
                    承認者レベル = x.承認者レベル,

                    //管理部署承認
                    管理部署承認 = x.管理部署承認,

                    //処理日
                    処理日 = x.処理日,

                    //管理責任課名
                    管理責任課名 = kanriKa,

                    //管理責任部署名
                    管理責任部署名 = kanriTantou,

                    //管理所在地
                    ESTABLISHMENT = x.ESTABLISHMENT,

                    //使用課名
                    使用課名 = siyouKa,

                    //使用部署名
                    使用部署名 = siyouTantou,

                    //試験内容
                    試験内容 = x.試験内容,

                    //工事区分NO
                    工事区分NO = x.工事区分NO,

                    //実走行距離
                    実走行距離 = x.実走行距離,

                    //編集者
                    編集者 = x.編集者,

                    //移管先部署ID
                    移管先部署ID = x.移管先部署ID,

                    //駐車場番号
                    駐車場番号 = x.入力駐車場番号,

                    //登録種別
                    ADD_TYPE = x.ADD_TYPE

                };

            }));
        }
        #endregion

        #region 試験車使用履歴承認
        /// <summary>
        /// 試験車使用履歴承認
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        public bool ApprovalTestCarUseHistory(IEnumerable<ApplicationApprovalCarModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //対象があるかどうか
            if (list != null && list.Any(x => x.CHECK_RESULT == CheckResultType.Ok) == true)
            {
                var target = list.Where(x => x.CHECK_RESULT == CheckResultType.Ok).ToList();

                //使用履歴承認情報の更新（使用履歴情報の更新も含む）
                results.Add(this.UpdateSiyouRirekiSyounin(target));

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
        /// 使用履歴承認情報の更新（使用履歴情報の更新も含む）
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        private bool UpdateSiyouRirekiSyounin(List<ApplicationApprovalCarModel> list)
        {
            //使用履歴承認情報の更新（使用履歴情報の更新も含む）
            return this.UpdateUseHistoryApprovalInfo(list.Select(x => new TestCarUseHistoryModel
            {
                //データID
                データID = x.データID,

                //履歴NO
                履歴NO = x.履歴NO,

                //SEQNO
                SEQNO = x.SEQNO,

                //承認要件コード
                承認要件コード = x.承認要件コード,

                //STEPNO
                STEPNO = x.STEPNO,

                //工事区分NO
                工事区分NO = x.工事区分NO,

                //編集者
                編集者 = x.編集者,

                //移管先部署ID
                移管先部署ID = x.移管先部署ID,

                //駐車場番号
                駐車場番号 = x.入力駐車場番号,

                //ユーザーID
                PERSONEL_ID = x.PERSONEL_ID

            }), true);

        }
        #endregion

        #region 試験車使用履歴中止
        /// <summary>
        /// 試験車使用履歴申請
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        public bool StopTestCarUseHistory(List<ApplicationApprovalCarModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //対象があるかどうか
            if (list != null && list.Any(x => x.CHECK_RESULT == CheckResultType.Ok) == true)
            {
                var target = list.Where(x => x.CHECK_RESULT == CheckResultType.Ok).ToList();

                //使用履歴情報の削除
                results.Add(this.DeleteSiyouRireki(target));

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
        /// 使用履歴情報の削除
        /// </summary>
        /// <param name="list">試験車</param>
        /// <returns></returns>
        private bool DeleteSiyouRireki(List<ApplicationApprovalCarModel> list)
        {
            //使用履歴情報の削除
            return this.DeleteUseHistoryInfo(list.Select(x => new TestCarUseHistoryModel
            {
                //データID
                データID = x.データID,

                //履歴NO
                履歴NO = x.履歴NO,

                //SEQNO
                SEQNO = x.SEQNO,

                //承認要件コード
                承認要件コード = x.承認要件コード,

                //STEPNO
                STEPNO = x.STEPNO,

                //工事区分NO
                工事区分NO = x.工事区分NO,

                //編集者
                編集者 = x.編集者

            }));

        }
        #endregion

        #region 使用履歴情報の追加
        /// <summary>
        /// 使用履歴情報の追加（使用履歴承認情報を含む）
        /// </summary>
        /// <param name="list">使用履歴情報</param>
        /// <returns></returns>
        public bool InsertUseHistoryInfo(IEnumerable<TestCarUseHistoryModel> list)
        {
            var results = new List<bool>();

            //対象がない場合は終了
            if (list == null || list.Any() == false)
            {
                return true;

            }

            var map = new Dictionary<string, List<ApprovalStepModel>>();

            var now = DateTime.Now;

            foreach (var history in list)
            {
                // 使用履歴情報追加のみの場合
                if (history.ADD_TYPE == AddType.History)
                {
                    results.Add(InsertUseHistoryInfo(history));

                    continue;
                }

                //未取得の承認要件コードなら取得
                if (map.ContainsKey(history.承認要件コード) == false)
                {
                    map[history.承認要件コード] = this.GetApprovalStep(history.承認要件コード);

                }

                var masterList = map[history.承認要件コード].Select(x =>
                {
                    var stepName = x.STEP名;

                    var syounin = x.管理部署承認;

                    //gtかどうか
                    if (syounin.Contains(GunmaToukyou) == true)
                    {
                        var syozoku = (history.ESTABLISHMENT ?? "");

                        stepName = string.Format("{0}{1}", syozoku.ToUpperInvariant(), stepName);

                        syounin = syounin.Replace(GunmaToukyou, syozoku);

                    }

                    return new ApprovalStepModel
                    {
                        //STEPNO
                        STEPNO = x.STEPNO,

                        //STEP名
                        STEP名 = stepName,

                        //承認者レベル
                        承認者レベル = x.承認者レベル,

                        //管理部署承認
                        管理部署承認 = syounin,

                    };

                }).ToArray();

                var seqNo = this.GetTestCarUseHistorySeqNo(new TestCarUseHistoryModel { データID = history.データID, 履歴NO = history.履歴NO });

                foreach (var master in masterList)
                {
                    //使用履歴承認情報の追加
                    results.Add(base.Insert("使用履歴承認情報", new[]
                    {
                        new BindModel { Name = ":データID", Object = history.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":履歴NO", Object = history.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":SEQNO", Object = seqNo, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":STEPNO", Object = master.STEPNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":承認STEP名", Object = master.STEP名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":承認者レベル", Object = master.承認者レベル, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":管理部署承認", Object = master.管理部署承認, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                    }));

                }

                var m = masterList.First();

                //使用履歴情報の追加
                results.Add(base.Insert("使用履歴情報", new[]
                {
                    new BindModel { Name = ":データID", Object = history.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":履歴NO", Object = history.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SEQNO", Object = seqNo, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":承認要件コード", Object = history.承認要件コード, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":STEPNO", Object = m.STEPNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":承認状況", Object = m.STEP名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":承認者レベル", Object = m.承認者レベル, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理部署承認", Object = m.管理部署承認, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":処理日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(history.処理日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理責任課名", Object = history.管理責任課名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理責任部署名", Object = history.管理責任部署名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":使用課名", Object = history.使用課名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":使用部署名", Object = history.使用部署名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験内容", Object = history.試験内容, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":工事区分NO", Object = history.工事区分NO, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":実走行距離", Object = history.実走行距離, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":移管先部署ID", Object = history.移管先部署ID, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":編集日", Type = OracleDbType.Date, Object = now, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":編集者", Object = history.編集者, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                }));

            }

            //登録が成功したかどうか
            return results.All(x => x == true);

        }
        /// <summary>
        /// 使用履歴情報の追加
        /// </summary>
        /// <param name="val">使用履歴情報</param>
        /// <returns></returns>
        public bool InsertUseHistoryInfo(TestCarUseHistoryModel val)
        {
            var results = new List<bool>();

            //対象がない場合は終了
            if (val == null)
            {
                return true;

            }

            var map = new Dictionary<string, List<ApprovalStepModel>>();

            var now = DateTime.Now;

            var seqNo = this.GetTestCarUseHistorySeqNo(new TestCarUseHistoryModel { データID = val.データID, 履歴NO = val.履歴NO });

            //使用履歴情報の追加
            return base.Insert("使用履歴情報", new[]
            {
                new BindModel { Name = ":データID", Object = val.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                new BindModel { Name = ":履歴NO", Object = val.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                new BindModel { Name = ":SEQNO", Object = seqNo, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                new BindModel { Name = ":承認要件コード", Object = val.承認要件コード, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":STEPNO", Object = val.STEPNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                new BindModel { Name = ":承認状況", Object = val.承認状況, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":承認者レベル", Object = val.承認者レベル, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理部署承認", Object = val.管理部署承認, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":処理日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.処理日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理責任課名", Object = val.管理責任課名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理責任部署名", Object = val.管理責任部署名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":使用課名", Object = val.使用課名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":使用部署名", Object = val.使用部署名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":試験内容", Object = val.試験内容, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":工事区分NO", Object = val.工事区分NO, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":実走行距離", Object = val.実走行距離, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":移管先部署ID", Object = val.移管先部署ID, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                new BindModel { Name = ":編集日", Type = OracleDbType.Date, Object = now, Direct = ParameterDirection.Input },
                new BindModel { Name = ":編集者", Object = val.編集者, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

            });

        }
        #endregion

        #region 使用履歴承認情報の更新
        /// <summary>
        /// 使用履歴承認情報の更新
        /// </summary>
        /// <param name="list">使用履歴情報</param>
        /// <param name="isSyounin">承認フラグ</param>
        /// <returns></returns>
        public bool UpdateUseHistoryApprovalInfo(IEnumerable<TestCarUseHistoryModel> list, bool isSyounin = false)
        {
            var results = new List<bool>();

            //対象がない場合は終了
            if (list == null || list.Any() == false)
            {
                return true;

            }

            var now = DateTime.Now;

            var map = new Dictionary<string, UserSearchOutModel>();

            foreach (var history in list)
            {
                //未取得のユーザーなら取得
                if (map.ContainsKey(history.PERSONEL_ID) == false)
                {
                    map[history.PERSONEL_ID] = base.GetLogic<UserLogic>().GetData(new UserSearchInModel { PERSONEL_ID = new[] { history.PERSONEL_ID } }).First();

                }

                var user = map[history.PERSONEL_ID];

                //使用履歴承認情報の更新
                results.Add(base.Update("使用履歴承認情報",
                new[]
                {
                    new BindModel { Name = ":承認日", Object = now, Type = OracleDbType.Date, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":承認者", Object = user.PERSONEL_ID, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":承認者所属課", Object = user.SECTION_CODE, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":承認者所属担当", Object = user.SECTION_GROUP_CODE, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                },
                new[]
                {
                    new BindModel { Name = ":データID", Object = history.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":履歴NO", Object = history.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SEQNO", Object = history.SEQNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":STEPNO", Object = history.STEPNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },

                }));

                //次の承認があるかどうか
                var next = this.GetTestCarUseHistoryApproval(new TestCarUseHistorySearchModel { データID = history.データID, 履歴NO = history.履歴NO, SEQNO = history.SEQNO, STEPNO = history.STEPNO + 1 }).FirstOrDefault();
                if (next != null)
                {
                    //使用履歴情報の更新
                    results.Add(base.UpdateUseHistoryInfo(new[] { history }, (x =>
                    {
                        var pram = new List<BindModel>
                        {
                            new BindModel { Name = ":STEPNO", Object = next.STEPNO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":承認状況", Object = next.承認STEP名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":承認者レベル", Object = next.承認者レベル, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                            new BindModel { Name = ":管理部署承認", Object = next.管理部署承認, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                        };

                        //駐車場番号があるかどうか
                        if (string.IsNullOrEmpty(x.駐車場番号) == false)
                        {
                            pram.Add(new BindModel { Name = ":駐車場番号", Object = x.駐車場番号, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });

                        }

                        return pram;

                    }), isSyounin));

                }
                else
                {
                    //使用履歴情報の更新
                    results.Add(base.UpdateUseHistoryInfo(new[] { history }, (x => new List<BindModel>
                    {
                        new BindModel { Name = ":STEPNO", Object = 0, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":承認状況", Object = "済", Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":承認者レベル", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":管理部署承認", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":移管先部署ID", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":駐車場番号", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                    }), isSyounin));

                    var testCar = new[]
                    {
                        new TestCarCommonModel
                        {
                            //データID
                            データID = history.データID,

                            //履歴NO
                            履歴NO = history.履歴NO,

                        }

                    };

                    //廃却かどうか
                    if (Regex.IsMatch(history.承認要件コード, Haikyaku) == true)
                    {
                        //試験車基本情報の更新
                        results.Add(base.UpdateTestCarBaseInfo(testCar, (x => new List<BindModel>
                        {
                            new BindModel { Name = ":研実管理廃却申請受理日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(now), Direct = ParameterDirection.Input }

                        })));

                    }
                    //移管かどうか
                    else if (Regex.IsMatch(history.承認要件コード, Ikan) == true)
                    {
                        //試験車基本情報の更新
                        results.Add(base.UpdateTestCarBaseInfo(testCar, (x => new List<BindModel>
                        {
                            new BindModel { Name = ":駐車場番号", Type = OracleDbType.Varchar2, Object = history.駐車場番号, Direct = ParameterDirection.Input }

                        })));

                        //試験車履歴情報の更新
                        results.Add(base.UpdateTestCarHistoryInfo(testCar, (x =>
                        {
                            var target = new List<BindModel>
                            {
                                new BindModel { Name = ":管理責任部署", Type = OracleDbType.Varchar2, Object = history.移管先部署ID, Direct = ParameterDirection.Input }

                            };

                            //GT間移管かどうか
                            if (Regex.IsMatch(history.承認要件コード, GtIkan) == true)
                            {
                                //工事区分NOごとの分岐
                                switch (history.工事区分NO)
                                {
                                    //改良研究(群馬)
                                    case GunmaKairyouKenkyuu:
                                        target.Add(new BindModel { Name = ":工事区分NO", Type = OracleDbType.Varchar2, Object = ToukyouKairyouKenkyuu, Direct = ParameterDirection.Input });
                                        break;

                                    //改良研究(東京)
                                    case ToukyouKairyouKenkyuu:
                                        target.Add(new BindModel { Name = ":工事区分NO", Type = OracleDbType.Varchar2, Object = GunmaKairyouKenkyuu, Direct = ParameterDirection.Input });
                                        break;
                                }

                            }

                            return target;

                        })));

                    }

                }

            }

            //登録が成功したかどうか
            return results.All(x => x == true);

        }
        #endregion

        #region 使用履歴情報の削除
        /// <summary>
        /// 使用履歴情報の削除
        /// </summary>
        /// <param name="list">使用履歴情報</param>
        /// <returns></returns>
        private bool DeleteUseHistoryInfo(IEnumerable<TestCarUseHistoryModel> list)
        {
            var results = new List<bool>();

            //対象がない場合は終了
            if (list == null || list.Any() == false)
            {
                return true;

            }

            var now = DateTime.Now;

            foreach (var history in list)
            {
                //使用履歴情報の更新
                results.Add(base.UpdateUseHistoryInfo(new[] { history }, (x => new List<BindModel>
                {
                    new BindModel { Name = ":STEPNO", Object = 0, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":承認者レベル", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理部署承認", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },

                })));

                //使用履歴情報の追加
                results.Add(base.Insert("使用履歴情報", new[]
                {
                    new BindModel { Name = ":データID", Object = history.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":履歴NO", Object = history.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SEQNO", Object = history.SEQNO + 1, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":承認要件コード", Object =　TyuusiMap[history.承認要件コード], Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":STEPNO", Object = 0, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":承認状況", Object = '済', Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":承認者レベル", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理部署承認", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":処理日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(now), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理責任課名", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":管理責任部署名", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":使用課名", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":使用部署名", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験内容", Object = string.Format(TyuusiMsg, history.承認要件名), Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":工事区分NO", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":実走行距離", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":移管先部署ID", Object = null, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":編集日", Type = OracleDbType.Date, Object = now, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":編集者", Object = history.編集者, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                }));

                //廃却で承認完了しているかどうか
                if (Regex.IsMatch(history.承認要件コード, Haikyaku) == true && history.STEPNO == 0)
                {
                    var testCar = new[]
                    {
                        new TestCarCommonModel
                        {
                            //データID
                            データID = history.データID,
                    
                            //履歴NO
                            履歴NO = history.履歴NO,

                        }

                    };

                    //試験車基本情報の更新
                    results.Add(base.UpdateTestCarBaseInfo(testCar, (x => new List<BindModel>
                    {
                        new BindModel { Name = ":研実管理廃却申請受理日", Type = OracleDbType.Date, Object = null, Direct = ParameterDirection.Input }

                    })));

                }

            }

            //登録が成功したかどうか
            return results.All(x => x == true);

        }
        #endregion

        #region ユーザーの権限を取得
        /// <summary>
        /// 所属のコードを取得
        /// </summary>
        /// <param name="syozoku">所属</param>
        /// <returns></returns>
        private string GetSyozokuCode(string syozoku)
        {
            //群馬と東京以外は全て扱い
            switch (syozoku)
            {
                //群馬
                //東京
                case Gunma:
                case Toukyou:
                    return syozoku;

                default:
                    return All;

            }

        }

        /// <summary>
        /// アクセスレベルを取得
        /// </summary>
        /// <param name="user">ユーザー</param>
        /// <returns></returns>
        private string GetAccessLevel(UserSearchOutModel user)
        {
            var level = user.ACCESS_LEVEL.Substring(0, 1);

            //FTSは担当者扱い
            if (level == Fts)
            {
                level = Tantousya;


            }

            return level;

        }

        /// <summary>
        /// 管理部署種別を取得
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GetKanriBusyoSyubetu(UserSearchOutModel user)
        {
            var syubetu = user.管理部署種別;

            //管理部署種別が無い場合は一般
            if (syubetu == null || syubetu.Any() == false)
            {
                return Ippan;

            }
            //1件のみかどうか
            else if (syubetu.Count() == 1)
            {
                var s = syubetu.First();

                //研実かどうか
                if (Regex.IsMatch(s, Kenjitu) == true)
                {
                    return Kenjitu;

                }
                //管理かどうか
                else if (Regex.IsMatch(s, Kanri) == true)
                {
                    return Kanri;

                }
                //該当なしは一般
                else
                {
                    return Ippan;

                }

            }
            else
            {
                //研実と管理かどうかかどうか
                if (syubetu.Any(x => Regex.IsMatch(x, Kenjitu) == true) == true && syubetu.Any(x => Regex.IsMatch(x, Kanri) == true) == true)
                {
                    return KenjituKanri;

                }
                //研実かどうか
                else if (syubetu.Any(x => Regex.IsMatch(x, Kenjitu) == true) == true)
                {
                    return Kenjitu;

                }
                //管理かどうか
                else if (syubetu.Any(x => Regex.IsMatch(x, Kanri) == true) == true)
                {
                    return Kanri;

                }
                //該当なしは一般
                else
                {
                    return Ippan;

                }

            }

        }
        #endregion
    }
}