using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 日程項目表示設定情報ロジッククラス
    /// </summary>
    public class ScheduleItemDisplayConfigurationLogic : TestCarBaseLogic
    {
        #region 取得
        /// <summary>
        /// 日程項目表示設定情報取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<ScheduleItemDisplayConfigurationModel> GetData(ScheduleItemDisplayConfigurationSearchModel cond)
        {
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":開発符号", Object = cond.開発符号, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }
            };

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"開発符号\"");
            sql.AppendLine("    ,A.\"表示列名\"");
            sql.AppendLine("    ,A.\"非表示列名\"");
            sql.AppendLine("    ,A.\"固定列数\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"日程項目表示設定情報\" A");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"開発符号\" = :開発符号");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     A.\"開発符号\"");

            return db.ReadModelList<ScheduleItemDisplayConfigurationModel>(sql.ToString(), prms);

        }
        #endregion

        #region 登録
        /// <summary>
        /// 日程項目表示設定情報登録
        /// </summary>
        /// <param name="list">ユーザー表示設定</param>
        /// <returns></returns>
        public bool MergeData(IEnumerable<ScheduleItemDisplayConfigurationModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                foreach (var user in list)
                {
                    //日程項目表示設定情報登録
                    results.Add(base.Merge("日程項目表示設定情報",
                    new[]
                    {
                        new BindModel { Name = ":表示列名", Object = user.表示列名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":非表示列名", Object = user.非表示列名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                        new BindModel { Name = ":固定列数", Object = user.固定列数, Type = OracleDbType.Int32, Direct = ParameterDirection.Input }

                    },
                    new[]
                    {
                        new BindModel { Name = ":開発符号", Object = user.開発符号, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                    }));

                }

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
        #endregion

        #region 削除
        /// <summary>
        /// 日程項目表示設定情報削除
        /// </summary>
        /// <param name="list">ユーザー表示設定</param>
        /// <returns></returns>
        public bool DeleteData(IEnumerable<ScheduleItemDisplayConfigurationModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                foreach (var user in list)
                {
                    //日程項目表示設定情報削除
                    results.Add(base.Delete("日程項目表示設定情報", new[]
                    {
                        new BindModel { Name = ":開発符号", Object = user.開発符号, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                    }));

                }

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
        #endregion

    }
}