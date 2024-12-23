using System.Data;
using System.Text;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Models;
using System;
using System.Linq;
using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    /// <remarks>処理待ち車両業務ロジック</remarks>
    public class PendingCarLogic : BaseLogic
    {
        /// <summary>
        /// 処理待ち車両の検索
        /// </summary>
        /// <returns>List</returns>
        public List<PendingCarModel> GetData(PendingCarSearchModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("    KK.データID");
            sql.AppendLine("   ,KK.管理票NO");
            sql.AppendLine("   ,KR.履歴NO");
            sql.AppendLine("   ,KR.開発符号");
            sql.AppendLine("   ,KR.試作時期");
            sql.AppendLine("   ,KR.号車");
            sql.AppendLine("   ,KR.メーカー名");
            sql.AppendLine("   ,KR.外製車名");
            sql.AppendLine("   ,SR.承認状況");
            sql.AppendLine("   ,SM.承認要件名");
            sql.AppendLine("   ,SR.管理責任課名");
            sql.AppendLine("   ,SR.管理責任部署名");
            sql.AppendLine("   ,KR.管理責任部署");
            sql.AppendLine("   ,SG_移管先.SECTION_GROUP_CODE 移管先部署名");
            sql.AppendLine("   ,SR.移管先部署ID");
            sql.AppendLine("   ,SR.承認者レベル");
            sql.AppendLine("   ,SR.管理部署承認");
            sql.AppendLine("   ,SS.承認日");
            sql.AppendLine("   ,SR.STEPNO 履歴STEPNO");
            sql.AppendLine("   ,SS.STEPNO 承認STEPNO");
            sql.AppendLine("   ,SR.編集日");
            sql.AppendLine("FROM");
            sql.AppendLine("        試験車基本情報 KK");
            sql.AppendLine("    INNER JOIN ");
            sql.AppendLine("        試験車履歴情報 KR");
            sql.AppendLine("    ON");
            sql.AppendLine("        KK.データID = KR.データID");
            sql.AppendLine("    INNER JOIN");
            sql.AppendLine("        使用履歴情報 SR");
            sql.AppendLine("    ON");
            sql.AppendLine("        KR.データID = SR.データID");
            sql.AppendLine("        AND KR.履歴NO = SR.履歴NO");
            sql.AppendLine("        AND (SR.SEQNO = (SELECT MAX(SEQNO) FROM 使用履歴承認情報 SRA WHERE SR.データID = SRA.データID AND SR.履歴NO = SRA.履歴NO GROUP BY SR.データID, SR.履歴NO))");
            sql.AppendLine("    INNER JOIN");
            sql.AppendLine("        承認要件マスタ SM");
            sql.AppendLine("    ON");
            sql.AppendLine("        SR.承認要件コード = SM.承認要件コード");
            sql.AppendLine("    INNER JOIN");
            sql.AppendLine("        使用履歴承認情報 SS");
            sql.AppendLine("    ON");
            sql.AppendLine("        SR.データID = SS.データID");
            sql.AppendLine("        AND SR.履歴NO = SS.履歴NO");
            sql.AppendLine("        AND SR.SEQNO = SS.SEQNO");
            sql.AppendLine("        AND (SR.STEPNO + 1 = SS.STEPNO OR ( NOT EXISTS (SELECT STEPNO FROM 使用履歴承認情報 SSA WHERE SR.データID = SSA.データID AND SR.履歴NO = SSA.履歴NO AND SR.SEQNO = SSA.SEQNO AND SR.STEPNO + 1 = SSA.STEPNO) AND SR.STEPNO = 1 AND SS.STEPNO = 1))");
            sql.AppendLine("    INNER JOIN ");
            sql.AppendLine("        固定資産情報 FS");
            sql.AppendLine("    ON");
            sql.AppendLine("        KK.データID = FS.データID");

            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        SECTION_GROUP_DATA SG_管理責任");
            sql.AppendLine("    ON");
            sql.AppendLine("        KR.管理責任部署 = SG_管理責任.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        SECTION_DATA SC_管理責任");
            sql.AppendLine("    ON");
            sql.AppendLine("        SG_管理責任.SECTION_ID = SC_管理責任.SECTION_ID");

            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        SECTION_GROUP_DATA SG_移管先");
            sql.AppendLine("    ON");
            sql.AppendLine("        SR.移管先部署ID = SG_移管先.SECTION_GROUP_ID");
            sql.AppendLine("    LEFT JOIN");
            sql.AppendLine("        SECTION_DATA SC_移管先");
            sql.AppendLine("    ON");
            sql.AppendLine("        SG_移管先.SECTION_ID = SC_移管先.SECTION_ID");

            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND SR.STEPNO <> 0");
            //廃却済み車両は表示しない
            sql.AppendLine("    AND KK.研実管理廃却申請受理日 IS NULL");
            sql.AppendLine("    AND KK.廃却決済承認年月 IS NULL ");
            sql.AppendLine("    AND KK.車両搬出日 IS NULL");
            sql.AppendLine("    AND FS.処分コード IS NULL");

            if (val.PERSONEL_ID != null)
            {
                // ユーザー情報の取得
                var user = base.GetLogic<UserLogic>().GetData(new UserSearchInModel { PERSONEL_ID = new[] { val.PERSONEL_ID }, MANAGE_FLG = true }).FirstOrDefault();

                // アクセスレベルの取得
                var accessLevel = GetApprovalLevel(user);

                sql.AppendLine("    AND (0 = 1");

                // 管理部署種別が一般の場合
                if (user.管理部署種別.Contains(Const.ManagementGroupCode.Ippan) || val.IPPAN_FLAG == true)
                {
                    // アクセスレベルが一般, 担当の場合
                    if (accessLevel == Const.AccessLevelCode.Ippan || accessLevel == Const.AccessLevelCode.Tanto)
                    {
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 = '{0}' AND SR.承認者レベル = :承認者レベル AND SR.承認状況 NOT LIKE '%移管先部署%' AND SG_管理責任.SECTION_GROUP_ID = :担当ID)", Const.ManagementGroupCode.Ippan));
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 = '{0}' AND SR.承認者レベル = :承認者レベル AND SR.承認状況 LIKE '%移管先部署%' AND SG_移管先.SECTION_GROUP_ID = :担当ID)", Const.ManagementGroupCode.Ippan));
                    }

                    // アクセスレベルが課長の場合
                    if (accessLevel == Const.AccessLevelCode.Syokusei)
                    {
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 = '{0}' AND SR.承認者レベル = :承認者レベル AND SR.承認状況 NOT LIKE '%移管先部署%' AND SC_管理責任.SECTION_ID = :課ID)", Const.ManagementGroupCode.Ippan));
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 = '{0}' AND SR.承認者レベル = :承認者レベル AND SR.承認状況 LIKE '%移管先部署%' AND SC_移管先.SECTION_ID = :課ID)", Const.ManagementGroupCode.Ippan));
                    }
                }

                // 管理部署種別が研実の場合
                if (user.管理部署種別.Contains(Const.ManagementGroupCode.Kenjitu) && val.IPPAN_FLAG == false)
                {
                    // 一般部署承認はすべて可能
                    sql.AppendLine(string.Format("        OR (SR.管理部署承認 = '{0}')", Const.ManagementGroupCode.Ippan));
                    sql.AppendLine(string.Format("        OR (SR.管理部署承認 = '{0}')", Const.ManagementGroupCode.Ippan));

                    // アクセスレベルが一般の場合
                    if (accessLevel == Const.AccessLevelCode.Ippan)
                    {
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 LIKE '%{0}%' AND SR.管理部署承認 LIKE '%' || :所属 || '%' AND SR.承認者レベル = :承認者レベル)", Const.ManagementGroupCode.Kenjitu));

                        // 管理部署課長承認の代理が所属に関わらず可能
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 LIKE '%{0}%' AND SR.承認者レベル = '{1}')", Const.ManagementGroupCode.Kanri, Const.AccessLevelCode.Syokusei));
                    }

                    // アクセスレベルが担当, 課長の場合
                    if (accessLevel == Const.AccessLevelCode.Tanto || accessLevel == Const.AccessLevelCode.Syokusei)
                    {
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 LIKE '%{0}%' AND SR.管理部署承認 LIKE '%' || :所属 || '%' AND SR.承認者レベル = :承認者レベル)", Const.ManagementGroupCode.Kenjitu));

                        // 研実部署担当者,担当承認の代理が所属に関わらず可能
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 LIKE '%{0}%' AND SR.承認者レベル > :承認者レベル)", Const.ManagementGroupCode.Kenjitu));
                    }
                }

                // 管理部署種別が管理の場合
                if (user.管理部署種別.Contains(Const.ManagementGroupCode.Kanri) && val.IPPAN_FLAG == false)
                {
                    // 一般部署承認はすべて可能
                    sql.AppendLine(string.Format("        OR (SR.管理部署承認 = '{0}')", Const.ManagementGroupCode.Ippan));
                    sql.AppendLine(string.Format("        OR (SR.管理部署承認 = '{0}')", Const.ManagementGroupCode.Ippan));

                    // アクセスレベルが一般の場合
                    if (accessLevel == Const.AccessLevelCode.Ippan)
                    {
                        // 管理部署課長承認の代理が所属に関わらず可能
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 LIKE '%{0}%' AND SR.承認者レベル = '{1}')", Const.ManagementGroupCode.Kanri, Const.AccessLevelCode.Syokusei));
                    }

                    // アクセスレベルが担当,課長の場合
                    if (accessLevel == Const.AccessLevelCode.Tanto || accessLevel == Const.AccessLevelCode.Syokusei)
                    {
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 LIKE '%{0}%' AND SR.管理部署承認 LIKE '%' || :所属 || '%' AND SR.承認者レベル = :承認者レベル)", Const.ManagementGroupCode.Kanri));

                        // 研実部署担当者,担当承認の代理が所属に関わらず可能
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 LIKE '%{0}%' AND SR.承認者レベル > :承認者レベル)", Const.ManagementGroupCode.Kenjitu));

                        // 管理部署担当者,担当承認の代理が所属に関わらず可能
                        sql.AppendLine(string.Format("        OR (SR.管理部署承認 LIKE '%{0}%' AND SR.承認者レベル > :承認者レベル)", Const.ManagementGroupCode.Kanri));
                    }
                }

                sql.AppendLine("    )");


                prms.Add(new BindModel { Name = ":PERSONEL_ID", Type = OracleDbType.Char, Object = val.PERSONEL_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":承認者レベル", Type = OracleDbType.Char, Object = accessLevel, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":所属", Type = OracleDbType.Char, Object = user.ESTABLISHMENT, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":課ID", Type = OracleDbType.Char, Object = user.SECTION_ID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = ":担当ID", Type = OracleDbType.Char, Object = user.SECTION_GROUP_ID, Direct = ParameterDirection.Input });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    SR.編集日 DESC");
            sql.AppendLine("   ,KK.管理票NO DESC");
            sql.AppendLine("   ,KR.履歴NO DESC");

            return db.ReadModelList<PendingCarModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// 承認者レベルを取得
        /// </summary>
        /// <param name="user">ユーザー</param>
        /// <returns></returns>
        private string GetApprovalLevel(UserSearchOutModel user)
        {
            var level = user.ACCESS_LEVEL.Substring(0, 1);

            // SKC(FTS)は担当者(一般)扱い
            if (level == Const.AccessLevelCode.Skc)
            {
                level = Const.AccessLevelCode.Ippan;
            }

            return level;
        }
    }
}