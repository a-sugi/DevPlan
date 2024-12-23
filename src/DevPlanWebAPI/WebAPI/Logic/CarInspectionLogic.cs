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
    /// 車検・点検リスト発行業務ロジッククラス
    /// </summary>
    public class CarInspectionLogic : TestCarBaseLogic
    {
        #region メンバ変数
        private const string Keikamotu = "軽貨物";
        private const string KogataKamotu = "小型貨物";
        private const string OogataKamotu = "大型貨物";

        private const int SyakenSyuuki1 = 12;
        private const int SyakenSyuuki2 = 24;

        private const int TenkenSyuuki1 = 12;
        private const int TenkenSyuukiHalf = 6;

        private const short Syaken = 1;
        private const short Tenken6 = 3;
        private const short Tenken12 = 2;

        private Dictionary<short, string> TenkenKubunMap = new Dictionary<short, string>
        {
            { 1, "車検" },
            { 2, "12ヶ月点検" },
            { 3, "6ヶ月点検" }

        };
        #endregion

        #region 取得
        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<CarInspectionModel> GetTestCar(CarInspectionSearchModel cond)
        {
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":管理票発行有無", Object = "済", Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

            };

            //SQL
            var sql = base.GetBaseTestCarSql(prms);

            //条件
            sql.AppendLine("    AND KR.\"車検期限\" IS NOT NULL");
            sql.AppendLine("    AND KK.\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND KR.\"廃艦日\" IS NULL");
            sql.AppendLine("    AND KR.\"管理票発行有無\" = :管理票発行有無");

            //廃却条件追加
            sql.AppendLine("    AND KK.\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND KR.\"処分コード\" IS NULL");

            //検索条件
            base.SetStringWhere(sql, prms, "DD", "ESTABLISHMENT", cond.ESTABLISHMENT);
            base.SetStringWhere(sql, prms, "KK", "管理票NO", cond.管理票NO);
            base.SetStringWhere(sql, prms, "SD", "SECTION_ID", cond.SECTION_ID);
            base.SetStringWhere(sql, prms, "SG", "SECTION_GROUP_ID", cond.SECTION_GROUP_ID);
            base.SetStringWhere(sql, prms, "KR", "開発符号", cond.開発符号);
            base.SetStringWhere(sql, prms, "KR", "外製車名", cond.外製車名);
            base.SetStringWhere(sql, prms, "KR", "試作時期", cond.試作時期);
            base.SetStringWhere(sql, prms, "KR", "号車", cond.号車);
            base.SetStringWhere(sql, prms, "KK", "駐車場番号", cond.駐車場番号);
            base.SetStringWhere(sql, prms, "KR", "登録ナンバー", cond.登録ナンバー);

            //車系
            if (cond.車検区分1 != null && cond.車検区分1.Any() == true)
            {
                sql.AppendLine("    AND");
                sql.AppendLine("        (");

                sql.Append("            SK.\"車検区分1\" IN (NULL");

                //車検区分1設定
                var i = 1;
                foreach (var x in cond.車検区分1.Where(x => string.IsNullOrWhiteSpace(x) == false).Distinct())
                {
                    var name = string.Format(":車検区分{0}", i++);
                    sql.AppendFormat(",{0}", name);

                    prms.Add(new BindModel { Name = name, Object = x, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });

                }

                sql.AppendLine(")");

                //空白が指定されているかどうか
                if (cond.車検区分1.Any(x => string.IsNullOrWhiteSpace(x) == true) == true)
                {
                    sql.AppendLine("            OR");
                    sql.AppendLine("            SK.\"車検区分1\" IS NULL");

                }

                sql.AppendLine("        )");

            }

            //ソート順
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     SD.\"SECTION_CODE\"");
            sql.AppendLine("    ,SG.\"SECTION_GROUP_CODE\"");

            //取得
            var target = db.ReadModelList<CarInspectionModel>(sql.ToString(), prms);

            var from = cond.START_DATE.Value.Date;
            var to = cond.END_DATE.Value.Date;

            var list = new List<CarInspectionModel>();

            Func<DateTime, int, DateTime?> getAfter = (day, add) =>
            {
                var d = day;

                while (d <= to)
                {
                    //車検期限の範囲内の日付かどうか
                    if (from <= d && d <= to)
                    {
                        return d;

                    }

                    //加算
                    d = d.AddMonths(add);

                }

                return null;

            };

            Func<DateTime, int, DateTime?> getBefore = (day, add) =>
            {
                var d = day;

                while (d >= from)
                {
                    //車検期限の範囲内の日付かどうか
                    if (from <= d && d <= to)
                    {
                        return d;

                    }

                    //加算
                    d = d.AddMonths(add * -1);

                }

                return null;

            };

            foreach (var x in target)
            {
                var syakenKigen = x.車検期限.Value.Date;

                var tenkenKubun = Syaken;
                var tenkenKigen = default(DateTime?);
                
                //車検区分1ごとの分岐
                switch (x.車検区分1)
                {
                    //小型貨物
                    //大型貨物
                    case KogataKamotu:
                    case OogataKamotu:
                        //車検期限の範囲内の日付かどうか
                        tenkenKigen = getAfter(syakenKigen, SyakenSyuuki1);
                        break;

                    default:
                        //車検区分1があるかどうか
                        if (string.IsNullOrWhiteSpace(x.車検区分1) == true)
                        {
                            break;

                        }

                        //車検期限の範囲内の日付かどうか
                        tenkenKigen = getAfter(syakenKigen, SyakenSyuuki2);
                        break;

                }

                //設定しているかどうか
                if (tenkenKigen == null)
                {
                    //車検期限の範囲内の日付かどうか
                    tenkenKigen = getAfter(syakenKigen, TenkenSyuuki1) ?? getBefore(syakenKigen, TenkenSyuuki1);
                    if (tenkenKigen != null)
                    {
                        tenkenKubun = Tenken12;

                    }

                }

                //設定なしで外製車名があるかどうか
                if (tenkenKigen == null && string.IsNullOrWhiteSpace(x.外製車名) == false)
                {
                    //車検期限の範囲内の日付かどうか
                    tenkenKigen = getAfter(syakenKigen, TenkenSyuukiHalf) ?? getBefore(syakenKigen, TenkenSyuukiHalf);
                    if (tenkenKigen != null)
                    {
                        tenkenKubun = Tenken6;

                    }

                }

                //設定しているかどうか
                if (tenkenKigen != null)
                {
                    //点検区分
                    if (cond.点検区分 != null && cond.点検区分.Any() == true && cond.点検区分.Contains(tenkenKubun) == false)
                    {
                        continue;

                    }

                    //点検区分
                    x.点検区分 = tenkenKubun;

                    //点検区分名
                    x.点検区分名 = TenkenKubunMap[tenkenKubun];

                    //点検期限
                    x.点検期限 = tenkenKigen;

                    list.Add(x);

                }

            }

            //返却
            return list;

        }
        #endregion
    }
}