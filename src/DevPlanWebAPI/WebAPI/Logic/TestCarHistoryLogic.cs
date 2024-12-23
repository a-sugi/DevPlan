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
    /// 試験車履歴業務ロジッククラス
    /// </summary>
    public class TestCarHistoryLogic : TestCarBaseLogic
    {
        #region 試験車履歴取得
        /// <summary>
        /// 試験車履歴取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TestCarCommonModel> GetTestCarHistory(TestCarCommonSearchModel cond)
        {
            var prms = new List<BindModel>();

            var sql = new StringBuilder();

            //データID
            if (cond.データID != null)
            {
                //SQL
                sql = base.GetBaseTestCarSql(isHistoryMax: false);

                sql.AppendLine("    AND KK.\"データID\" = :データID");

                prms.Add(new BindModel { Name = ":データID", Object = cond.データID, Type = OracleDbType.Int32, Direct = ParameterDirection.Input });

                //履歴NO
                if (cond.履歴NO != null)
                {
                    sql.AppendLine("    AND KR.\"履歴NO\" = :履歴NO");

                    prms.Add(new BindModel { Name = ":履歴NO", Object = cond.履歴NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input });

                }

                //ソート順
                sql.AppendLine("ORDER BY");
                sql.AppendLine("     KK.\"データID\"");
                sql.AppendLine("    ,KR.\"履歴NO\"");

            }
            else
            {
                //SQL
                sql = base.GetBaseTestCarSql();

                //条件
                sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");

                //検索条件
                base.SetStringWhere(sql, prms, "DD", "ESTABLISHMENT", cond.ESTABLISHMENT);
                base.SetStringWhere(sql, prms, "KR", "開発符号", cond.開発符号);
                base.SetStringWhere(sql, prms, "KK", "管理票NO", cond.管理票NO);
                base.SetStringWhere(sql, prms, "KR", "車体番号", cond.車体番号);
                base.SetStringWhere(sql, prms, "KR", "試作時期", cond.試作時期);
                base.SetStringWhere(sql, prms, "KR", "号車", cond.号車);
                base.SetStringWhere(sql, prms, "KR", "メーカー名", cond.メーカー名);
                base.SetStringWhere(sql, prms, "KR", "外製車名", cond.外製車名);

                //ソート順
                sql.AppendLine("ORDER BY");
                sql.AppendLine("    KK.\"管理票NO\"");

            }

            //取得
            return db.ReadModelList<TestCarCommonModel>(sql.ToString(), prms);

        }
        #endregion

        #region 試験車使用履歴存在チェック
        /// <summary>
        /// 試験車使用履歴存在チェック
        /// </summary>
        /// <param name="list">試験車使用履歴</param>
        /// <returns></returns>
        public List<TestCarUseHistoryModel> IsTestCarUseHistoryExistCheck(List<TestCarUseHistoryModel> list)
        {
            var today = DateTime.Today;

            //対象があるかどうか
            if (list != null && list.Any() == true)
            {
                foreach (var history in list)
                {
                    //チェック結果
                    history.CHECK_RESULT = CheckResultType.Ok;

                    //処理日が未入力は次へ
                    //Update Start 杉浦 処理日未入力の際のエラー処理の作成
                    //var syoribi = history.処理日.Value;
                    //if (syoribi == null)
                    //{
                    //    continue;

                    //}
                    if (history.処理日 == null)
                    {
                        continue;

                    }
                    var syoribi = history.処理日.Value;
                    //Update End 杉浦 処理日未入力の際のエラー処理の作成

                    //年月だけ取得
                    syoribi = new DateTime(syoribi.Year, syoribi.Month, 1);

                    //処理日があるかどうか
                    var day = base.GetLogic<ApplicationApprovalCarLogic>().GetMaxSyoriDay(history);
                    if (day != null)
                    {
                        //処理日の最大年月より入力した処理日が大きいならエラー
                        var ym = new DateTime(day.Value.Year, day.Value.Month, 1).AddMonths(1);
                        
                        //過去の不正データ対応（例:0006年→2006年）
                        if (ym < new DateTime(2000, 1, 1))
                        {
                            var culture = new System.Globalization.CultureInfo("ja-JP");
                            culture.Calendar.TwoDigitYearMax = 2099;

                            ym = DateTime.Parse(ym.ToString("yy/MM/dd"), culture);
                        }

                        if (ym < syoribi)
                        {
                            history.CHECK_RESULT = CheckResultType.MonthlyInput;

                        }

                    }

                }

            }

            return list;

        }
        #endregion

        #region 試験車履歴更新
        /// <summary>
        /// 試験車履歴更新
        /// </summary>
        /// <param name="list">試験車履歴</param>
        /// <returns></returns>
        public bool UpdateTestCarHistory(IEnumerable<TestCarHistoryModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //試験車履歴情報の更新
                results.Add(base.UpdateTestCarHistoryInfo(list.Where(x => x.TestCar != null).Select(x => x.TestCar), (x => new List<BindModel>
                {
                    new BindModel { Name = ":試験着手証明文書", Type = OracleDbType.Varchar2, Object = x.試験着手証明文書, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験着手日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(x.試験着手日), Direct = ParameterDirection.Input }

                })));

                var historyInfo = list.Where(x => x.TestCarUseHistoryList != null && x.TestCarUseHistoryList.Any() == true).SelectMany(x => x.TestCarUseHistoryList).ToArray();

                //使用履歴情報の追加
                results.Add(base.GetLogic<ApplicationApprovalCarLogic>().InsertUseHistoryInfo(historyInfo.Where(x => x.STEPNO == -1)));

                //使用履歴情報の更新
                results.Add(base.UpdateUseHistoryInfo(historyInfo.Where(x => x.STEPNO >= 0), (x => new List<BindModel>
                {
                    new BindModel { Name = ":処理日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(x.処理日), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":使用課名", Type = OracleDbType.Varchar2, Object = x.使用課名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":使用部署名", Type = OracleDbType.Varchar2, Object = x.使用部署名, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":試験内容", Type = OracleDbType.Varchar2, Object = x.試験内容, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":実走行距離", Type = OracleDbType.Varchar2, Object = x.実走行距離, Direct = ParameterDirection.Input }

                })));

                //試験車使用履歴承認
                base.GetLogic<ApplicationApprovalCarLogic>().UpdateUseHistoryApprovalInfo(historyInfo.Where(x => x.STEPNO > 0), true);

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
        #endregion

    }
}